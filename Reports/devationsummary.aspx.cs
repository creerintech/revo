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

public partial class Reports_devationsummary : System.Web.UI.Page
{
    #region[Private variables]
        DMDeviationreport Obj_PO = new DMDeviationreport();
        CommanFunction Obj_Comm = new CommanFunction();
        public static DataSet DsGrd = new DataSet();
        DataSet DS = new DataSet();
        decimal SubTotal, GrandTotal,Discount,Vat = 0;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagPrint = false;
    #endregion

    #region[User Defined Function]
    private void MakeEmptyForm()
    {
        if (!FlagPrint)
            ImgBtnPrint.Visible = true;
        if (!FlagPrint)
            ImgBtnExport.Visible = true;
        ChkFrmDate.Checked = true;
        Fillchkdate();
        txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
        txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        SetInitialRow();
        ddldev.SelectedIndex = 0;
    }

    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
          
            dt.Columns.Add(new DataColumn("#", typeof(Int32)));
            dt.Columns.Add(new DataColumn("devno", typeof(string)));
            dt.Columns.Add(new DataColumn("devdate", typeof(string)));
            dt.Columns.Add(new DataColumn("Item", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(decimal)));
            dt.Columns.Add(new DataColumn("sysclosing", typeof(decimal)));
            dt.Columns.Add(new DataColumn("phyclosing", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Rate", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TotalAmt", typeof(string)));

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["devno"] = "";
            dr["devdate"] = "";
            dr["Item"] = "";
            dr["Unit"] = 0;
            dr["sysclosing"] = 0;
            dr["phyclosing"] = 0;
            dr["Rate"] = 0;
            dr["TotalAmt"] = "";

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GrdReport.DataSource = dt;
            GrdReport.DataBind();
            dt = null;
            dr = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    private void FillCombo()
    {
        try
        {
            string COND = string.Empty;
            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
            //    COND = COND + " AND Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}
            DS = Obj_PO.FillReportCombo(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddldev.DataSource = DS.Tables[0];
                    ddldev.DataTextField = "devno";
                    ddldev.DataValueField = "devid";
                    ddldev.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void ReportGrid(int StrCondition)
    {
        try
        {
            DsGrd = Obj_PO.GetList(StrCondition, out StrError);
            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                if (!FlagPrint)
                    ImgBtnPrint.Visible = true;
                if (!FlagPrint)
                    ImgBtnExport.Visible = true;
                GrdReport.DataSource = DsGrd.Tables[0];
                GrdReport.DataBind();
                lblCount.Text = DsGrd.Tables[0].Rows.Count + " Records Found";
                lblCount.Visible = true;
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
                lblCount.Text = "No Records Found";
                lblCount.Visible = true;
                SetInitialRow();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void Fillchkdate()
    {
        if (ChkFrmDate.Checked == true)
        {
            txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
        }
        else
        {
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
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
                //if (!Session["UserRole"].Equals("Administrator"))
                //{
                    //Checking Right of users=======

                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Deviation Report'");
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
                //}
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
            FillCombo();
            CheckUserRight();
            SetInitialRow();
            MakeEmptyForm();
        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
         try
        {
            DataSet DsGrid = new DataSet();
            StrCondition = string.Empty;

            //if (ChkFrmDate.Checked == true)
            //{
            //    StrCondition = StrCondition + " and PO.PODate between'" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "'And'" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "'";
            //}
            //else
            //{
            //    StrCondition = StrCondition + "and PO.PODate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "'";

            //}
            if (Convert.ToInt32(ddldev.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and PO.POId=" + Convert.ToInt32(ddldev.SelectedValue);
                ReportGrid(Convert.ToInt32(ddldev.SelectedValue));
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("Please Select Deviation No..", this.Page);
            }
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
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
            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                //========Call Register
                GridView GridExp = new GridView();
                GridExp.DataSource = DsGrd.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("Deviation Report.xls", GridExp);
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("No Data Found To Export..!", this.Page);
                DsGrd.Dispose();
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }
            DsGrd = null;
        }
        catch (ThreadAbortException tex)
        {

        }
        catch(Exception ex)
        {
            
        }
    }
    protected void ChkFrmDate_CheckedChanged(object sender, EventArgs e)
    {
        Fillchkdate();
    }
    protected void GrdReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                SubTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmt"));
            }
            if (e.Row.RowType==DataControlRowType.Footer)
            {
                e.Row.Cells[7].Text="Total :  ";
                e.Row.Cells[8].Text = SubTotal.ToString();
            }
        }
        catch(Exception ex)
        {
           
        }
    }
    
}
