using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Threading;
using MayurInventory.DALSQLHelper;
using MayurInventory.DataModel;
using MayurInventory.DB;
using MayurInventory.EntityClass;
using MayurInventory.Utility;

public partial class Reports_IssueRegisterReport : System.Web.UI.Page
{
    #region [Private Variables]
        DMIssueRegister Obj_IssueRegister = new DMIssueRegister();
        IssueRegister Entity_IssueRegister = new IssueRegister();
        CommanFunction Obj_Comm = new CommanFunction();
        DataSet DS = new DataSet();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagPrint = false;
    #endregion

    #region[UserDefineFunction]
    public void MakeEmptyForm()
    {
        txtFromDate.Enabled = txtToDate.Enabled = true;
        txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
        txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        ChkFrmDate.Checked = true;
        SetInitialRow();
        FillComboData();
        ReportGrid();
        ddlNo.SelectedIndex = ddlTo.SelectedIndex = 0;
        ChkFrmDate.Focus();
    }
    public void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("IssueNo", typeof(string));
        dt.Columns.Add("IssueDate", typeof(string));
        dt.Columns.Add("RequisitionNo", typeof(string));
        dt.Columns.Add("RequisitionDate", typeof(string));
        dt.Columns.Add("EmpName", typeof(string));

        ////  dt.Columns.Add("Location", typeof(string));
        //      // dt.Columns.Add("Quantity", typeof(decimal));
        //       
        //     
        //      // dt.Columns.Add("ReqQuantity", typeof(decimal));
        //       //dt.Columns.Add("Notes", typeof(string));

        dr = dt.NewRow();

        dr["IssueNo"] = "";
        dr["IssueDate"] = "";
        dr["RequisitionNo"] = "";
        dr["RequisitionDate"] = "";
        dr["EmpName"] = "";
        //dr["Item"] = "";
        //
        //dr["Location"] = "";
        //dr["Quantity"] = 0.00;
        //dr["ReqQuantity"] = 0.00;
        //
        //
        //dr["Notes"] = "";

        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        GrdReport.DataSource = dt;
        GrdReport.DataBind();
    }
    public void FillComboData()
    {
        try
        {
            DS = Obj_IssueRegister.FillComboForReport(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlNo.DataSource = DS.Tables[0];
                    ddlNo.DataTextField = "IssueNo";
                    ddlNo.DataValueField = "#";
                    ddlNo.DataBind();
                }
                if (DS.Tables[1].Rows.Count > 0)
                {
                    ddlTo.DataSource = DS.Tables[1];
                    ddlTo.DataTextField = "Name";
                    ddlTo.DataValueField = "#";
                    ddlTo.DataBind();
                }
            }
            else
            {
                DS = null;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public bool chkDateFormat()
    {
        bool flag = false;
        if (ChkFrmDate.Checked == true)
        {
            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                if ((Convert.ToDateTime(txtFromDate.Text) < DateTime.Now) && (Convert.ToDateTime(txtToDate.Text) < DateTime.Now))
                {
                    if (Convert.ToDateTime(txtFromDate.Text) < Convert.ToDateTime(txtToDate.Text))
                    {
                        flag = true;
                    }
                    else
                    {
                        Obj_Comm.ShowPopUpMsg("From Date Must Be Less Than To Date..", this.Page);
                    }
                }
                else
                {
                    Obj_Comm.ShowPopUpMsg("Please Check Date..", this.Page);
                }
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("Please Select Date..", this.Page);
            }
        }
        else
        {
            flag = true;
        }
        return flag;
    }
    public void ReportGrid()
    {
        StrCondition = string.Empty;
        DataSet dsg = new DataSet();
        if (ChkFrmDate.Checked)
        {
            StrCondition += " and IR.IssueDate Between '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "'";
        }
        if (!ChkFrmDate.Checked)
        {
            StrCondition += " and IR.IssueDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" + Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
        }
        if (Convert.ToInt32(ddlNo.SelectedValue) > 0)
        {
            StrCondition += " and IR.IssueRegisterId = " + Convert.ToInt32(ddlNo.SelectedValue);
        }
        if (Convert.ToInt32(ddlTo.SelectedValue) > 0)
        {
            StrCondition += " and IR.EmployeeId = " + Convert.ToInt32(ddlTo.SelectedValue);
        }
        dsg = Obj_IssueRegister.ShowIssueRegisterReport(StrCondition, out StrError);
        if (dsg.Tables.Count > 0 && dsg.Tables[0].Rows.Count > 0)
        {
            if(!FlagPrint)
            ImgBtnPrint.Visible = ImgBtnExport.Visible = true;
            GrdReport.DataSource = dsg.Tables[0];
            GrdReport.DataBind();
            lblCount.Text = dsg.Tables[0].Rows.Count.ToString() + "  Record Found";
            dsg = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "chkCommissionDate();", true);
        }
        else
        {
            GrdReport.DataSource = dsg.Tables[0];
            GrdReport.DataBind();
            lblCount.Text = "";
            dsg = null;
            SetInitialRow();
            ImgBtnPrint.Visible = ImgBtnExport.Visible = false;
        }
    }
    //User Right Function===========
    public void CheckUserRight()
    {
        try
        {
            #region [USER RIGHT]
            //Checking Session Varialbels========
            if (Session["UserName"] != null && Session["UserRole"] != null)
            {
                //Checking User Role========
                if (!Session["UserRole"].Equals("Administrator"))
                {
                    //Checking Right of users=======

                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='IssueRegisterReport'");
                    if (dtRow.Length > 0)
                    {
                        DataTable dt = dtRow.CopyToDataTable();
                        dsChkUserRight.Tables.Add(dt);
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                        && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        Response.Redirect("~/Masters/NotAuthUser.aspx");
                    }
                    //Checking View Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                    {
                        BtnShow.Visible = false;
                    }
                    //Checking Print Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        ImgBtnPrint.Visible = false;
                        ImgBtnExport.Visible = false;
                        FlagPrint = true;
                    }

                    dsChkUserRight.Dispose();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
            #endregion
        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //User Right Function==========  
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckUserRight();
            MakeEmptyForm();
        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        if (chkDateFormat() == true)
        {
            ReportGrid();
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
    protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            StrCondition = string.Empty;
            DataSet dsg = new DataSet();
            if (ChkFrmDate.Checked)
            {
                StrCondition += " and IR.IssueDate Between '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFrmDate.Checked)
            {
                StrCondition += " and IR.IssueDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" + Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlNo.SelectedValue) > 0)
            {
                StrCondition += " and IR.IssueNo = " + Convert.ToInt32(ddlNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlTo.SelectedValue) > 0)
            {
                StrCondition += " and IR.EmployeeId = " + Convert.ToInt32(ddlTo.SelectedValue);
            }
            dsg = Obj_IssueRegister.ShowIssueRegisterReport(StrCondition, out StrError);
            if (dsg.Tables.Count > 0 && dsg.Tables[0].Rows.Count > 0)
            {
                //========Call Register
                GridView GridExp = new GridView();
                GridExp.DataSource = dsg.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("Material_Issused_Report.xls", GridExp);
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("No Data Found To Export..!", this.Page);
                dsg.Dispose();
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }
            dsg = null;
        }
        catch (ThreadAbortException tex)
        {

        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
        }
    }
    //========Important Required for Excel Export Must=============
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    //========Important Required for Excel Export Must=============
    protected void ChkFrmDate_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkFrmDate.Checked == true)
        {
            txtFromDate.Enabled = txtToDate.Enabled = true;
        }
        else
        {
            txtFromDate.Enabled = txtToDate.Enabled = false;
        }
    }
    protected void GrdReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.GrdReport.PageIndex = e.NewPageIndex;
            DataSet dsg = new DataSet();
            StrCondition = string.Empty;
            if (ChkFrmDate.Checked)
            {
                StrCondition += " and IR.IssueDate Between '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFrmDate.Checked)
            {
                StrCondition += " and IR.IssueDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" + Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlNo.SelectedValue) > 0)
            {
                StrCondition += " and IR.IssueRegisterId = " + Convert.ToInt32(ddlNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlTo.SelectedValue) > 0)
            {
                StrCondition += " and IR.EmployeeId = " + Convert.ToInt32(ddlTo.SelectedValue);
            }
            dsg = Obj_IssueRegister.ShowIssueRegisterReport(StrCondition, out StrError);
            if (dsg.Tables.Count > 0 && dsg.Tables[0].Rows.Count > 0)
            {
                ImgBtnPrint.Visible = ImgBtnExport.Visible = true;
                GrdReport.DataSource = dsg.Tables[0];
                GrdReport.DataBind();
                lblCount.Text = dsg.Tables[0].Rows.Count.ToString() + "  Record Found";
                dsg = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "chkCommissionDate();", true);
            }
            else
            {
                GrdReport.DataSource = dsg.Tables[0];
                GrdReport.DataBind();
                lblCount.Text = "";
                dsg = null;
                SetInitialRow();
                ImgBtnPrint.Visible = ImgBtnExport.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
