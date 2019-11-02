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

public partial class Reports_ConsumptionReport : System.Web.UI.Page
{
    #region[Private variables]
        DMMayurInventory_Reports Obj_Reports = new DMMayurInventory_Reports();
        MayurInventory_Reports Entity_Reports = new MayurInventory_Reports();
        CommanFunction Obj_Comm = new CommanFunction();
        public static DataSet DsGrd = new DataSet();
        DataSet DS = new DataSet();
        private string StrCondition = string.Empty;
        private string StrRateCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagPrint = false;
        decimal Inward = 0, Outward = 0, Damaged = 0,  Closing = 0, Amount = 0;
    #endregion

    #region[User Defined Function]

    private void MakeEmptyForm()
    {
        ChkFrmDate.Checked = true;
        Fillchkdate();
        //txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
        txtFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        SetInitialRow();
        ddlLocation.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        ddlItemName.SelectedIndex = 0;
        ddlUnit.SelectedIndex = 0;

        RDO_FromTo.Checked = false;
        RDO_GreaterThenEqualTo.Checked = false;
        RDO_LessThenEqualTo.Checked = false;
        RDO_EqualTo.Checked = false;

        txtFromRate.Text = "";
        txtToRate.Text = "";
        txtGreaterRate.Text = "";
        txtLessRate.Text = "";
        txtEqualRate.Text = "";
    }

    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Category", typeof(string)));
            dt.Columns.Add(new DataColumn("ProductCode", typeof(string)));
            dt.Columns.Add(new DataColumn("Product", typeof(string)));
            dt.Columns.Add(new DataColumn("ProductUnit", typeof(string)));
            dt.Columns.Add(new DataColumn("ProductMRP", typeof(decimal)));
            dt.Columns.Add(new DataColumn("StockLocation", typeof(string)));

            dt.Columns.Add(new DataColumn("Inward", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Outward", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Damage", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Closing", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));


            dr = dt.NewRow();

            dr["Category"] = 0;
            dr["ProductCode"] = "";
            dr["Product"] = "";
            dr["ProductUnit"] = "";
            dr["ProductMRP"] = 0;
            dr["StockLocation"] = "";

            dr["Inward"] = 0;
            dr["Outward"] = 0;
            dr["Damage"] = 0;
            dr["Closing"] = 0;
            dr["Amount"] = 0;

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
            //    COND = COND + " AND P.LOC=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}
            DS = Obj_Reports.FillReportCombo(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlCategory.DataSource = DS.Tables[0];
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();
                }
                if (DS.Tables[2].Rows.Count > 0)
                {
                    ddlLocation.DataSource = DS.Tables[2];
                    ddlLocation.DataTextField = "Location";
                    ddlLocation.DataValueField = "StockLocationID";
                    ddlLocation.DataBind();
                }
                if (DS.Tables[3].Rows.Count > 0)
                {
                    ddlUnit.DataSource = DS.Tables[3];
                    ddlUnit.DataTextField = "Unit";
                    ddlUnit.DataValueField = "UnitId";
                    ddlUnit.DataBind();
                }
                if (DS.Tables[4].Rows.Count > 0)
                {
                    ddlItemName.DataSource = DS.Tables[4];
                    ddlItemName.DataTextField = "ItemName";
                    ddlItemName.DataValueField = "ItemId";
                    ddlItemName.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void ReportGrid(string FromDate, string ToDate, string StrCondition)
    {
        try
        {
            DsGrd = Obj_Reports.ConsumptionReport(FromDate, ToDate, StrCondition, out StrError);

            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                GrdReport.DataSource = DsGrd.Tables[0];
                GrdReport.DataBind();
                lblCount.Text = DsGrd.Tables[0].Rows.Count + " Records Found";
                lblCount.Visible = true;
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
                lblCount.Text = DsGrd.Tables[0].Rows.Count + " Records Found";
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
            //txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtFromDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
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

    private bool CheckValidation()
    {
        bool Chackflag = false;
        StrRateCondition = String.Empty;

        if (RDO_FromTo.Checked == true)
        {
            if (string.IsNullOrEmpty(txtFromRate.Text))
            {
                txtFromRate.Text = "0";
            }
            if (string.IsNullOrEmpty(txtToRate.Text))
            {
                txtToRate.Text = "0";
            }
            if (Convert.ToDecimal(txtFromRate.Text) >= 0 && Convert.ToDecimal(txtToRate.Text) >= 0 && Convert.ToDecimal(txtFromRate.Text) <= Convert.ToDecimal(txtToRate.Text))
            {
                StrRateCondition = StrRateCondition + " and (SD.ProductMRP >=" + Convert.ToDecimal(txtFromRate.Text);
                StrRateCondition = StrRateCondition + " and SD.ProductMRP <=" + Convert.ToDecimal(txtToRate.Text) + ") ";

                Chackflag = true;
            }
        }
        else if (RDO_GreaterThenEqualTo.Checked == true)
        {
            if (string.IsNullOrEmpty(txtGreaterRate.Text))
            {
                txtGreaterRate.Text = "0";
            }
            if (Convert.ToDecimal(txtGreaterRate.Text) >= 0)
            {
                StrRateCondition = StrRateCondition + " and SD.ProductMRP >=" + Convert.ToDecimal(txtGreaterRate.Text);

                Chackflag = true;
            }
        }
        else if (RDO_LessThenEqualTo.Checked == true)
        {
            if (string.IsNullOrEmpty(txtLessRate.Text))
            {
                txtLessRate.Text = "0";
            }
            if (Convert.ToDecimal(txtLessRate.Text) >= 0)
            {
                StrRateCondition = StrRateCondition + " and SD.ProductMRP <=" + Convert.ToDecimal(txtLessRate.Text);

                Chackflag = true;
            }
        }
        else if (RDO_EqualTo.Checked == true)
        {
            if (string.IsNullOrEmpty(txtEqualRate.Text))
            {
                txtEqualRate.Text = "0";
            }
            if (Convert.ToDecimal(txtEqualRate.Text) >= 0)
            {
                StrRateCondition = StrRateCondition + " and SD.ProductMRP =" + Convert.ToDecimal(txtEqualRate.Text);

                Chackflag = true;
            }
        }
        else
        {
            Chackflag = true;//If no option is selected...
        }

        return Chackflag;
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='ConsumptionReport'");
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
            FillCombo();
            CheckUserRight();
            SetInitialRow();
            MakeEmptyForm();
            
        }
    }
    protected void ChkFrmDate_CheckedChanged(object sender, EventArgs e)
    {
        Fillchkdate();
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet DsGrid = new DataSet();
            StrCondition = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;

            if (CheckValidation() == true)
            {
                if (ChkFrmDate.Checked == true)
                {
                    FromDate = Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy");
                    ToDate = Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy");
                }
                else
                {
                    FromDate = Convert.ToDateTime("01-01-1975").ToString("MM-dd-yyyy");
                    ToDate = DateTime.Now.ToString("MM-dd-yyyy");
                }
                if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and IC.CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
                }
                if (Convert.ToInt32(ddlItemName.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and IM.ItemId=" + Convert.ToInt32(ddlItemName.SelectedValue);
                }
                if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and SL.StockLocationID=" + Convert.ToInt32(ddlLocation.SelectedValue);
                }
                if (Convert.ToInt32(ddlUnit.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and UM.UnitId=" + Convert.ToInt32(ddlUnit.SelectedValue);
                }

                if (RDO_FromTo.Checked == true)
                {
                    StrCondition = StrCondition + StrRateCondition;
                }
                if (RDO_GreaterThenEqualTo.Checked == true)
                {
                    StrCondition = StrCondition + StrRateCondition;
                }
                if (RDO_LessThenEqualTo.Checked == true)
                {
                    StrCondition = StrCondition + StrRateCondition;
                }
                if (RDO_EqualTo.Checked == true)
                {
                    StrCondition = StrCondition + StrRateCondition;
                }

                ReportGrid(FromDate, ToDate, StrCondition);
                ScriptManager.RegisterStartupScript(this,this.GetType(), "FreGrid", "FreGrid();", true);
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("No Data Found To Export..!", this.Page);
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
                Obj_Comm.Export("Consumption Report - "+DateTime.Now.ToString("dd-MMM-yyyy")+".xls", GridExp);
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
        catch (Exception ex)
        {

        }
    }
    protected void GrdReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Inward += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Inward"));
                Outward += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Outward"));
                Damaged += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Damage"));
                Closing += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Closing"));
                Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "Total";
                e.Row.Cells[7].Text = Inward.ToString("0.00");
                e.Row.Cells[8].Text = Outward.ToString("0.00");
                e.Row.Cells[9].Text = Damaged.ToString("0.00");
                e.Row.Cells[10].Text = Closing.ToString("0.00");
                e.Row.Cells[11].Text = Amount.ToString("0.00");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void GrdReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.GrdReport.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;
            if (ChkFrmDate.Checked == true)
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy");
                ToDate = Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy");
            }
            else
            {
                FromDate = Convert.ToDateTime("01-01-1975").ToString("MM-dd-yyyy");
                ToDate = DateTime.Now.ToString("MM-dd-yyyy");
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IC.CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlItemName.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IM.ItemId=" + Convert.ToInt32(ddlItemName.SelectedValue);
            }
            if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and SL.StockLocationID=" + Convert.ToInt32(ddlLocation.SelectedValue);
            }
            if (Convert.ToInt32(ddlUnit.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and UM.UnitId=" + Convert.ToInt32(ddlUnit.SelectedValue);
            }

            if (RDO_FromTo.Checked == true)
            {
                //StrCondition = StrCondition + " and (SD.ProductMRP >=" + Convert.ToDecimal(txtFromRate.Text);
                //StrCondition = StrCondition + " and SD.ProductMRP >=" + Convert.ToDecimal(txtToRate.Text) + ") ";
                StrCondition = StrCondition + StrRateCondition;
            }
            if (RDO_GreaterThenEqualTo.Checked == true)
            {
                //StrCondition = StrCondition + " and SD.ProductMRP >=" + Convert.ToDecimal(txtGreaterRate.Text);
                StrCondition = StrCondition + StrRateCondition;
            }
            if (RDO_LessThenEqualTo.Checked == true)
            {
                //StrCondition = StrCondition + " and SD.ProductMRP <=" + Convert.ToDecimal(txtLessRate.Text);
                StrCondition = StrCondition + StrRateCondition;
            }
            if (RDO_EqualTo.Checked == true)
            {
                //StrCondition = StrCondition + " and SD.ProductMRP =" + Convert.ToDecimal(txtEqualRate.Text);
                StrCondition = StrCondition + StrRateCondition;
            }

            DsGrd = Obj_Reports.ConsumptionReport(FromDate, ToDate, StrCondition, out StrError);

            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                GrdReport.DataSource = DsGrd.Tables[0];
                GrdReport.DataBind();
                lblCount.Text = DsGrd.Tables[0].Rows.Count + " Records Found";
                lblCount.Visible = true;
            }

            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
                lblCount.Text = DsGrd.Tables[0].Rows.Count + " Records Found";
                lblCount.Visible = true;
                SetInitialRow();
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
