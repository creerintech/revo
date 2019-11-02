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

public partial class Reports_POStatus : System.Web.UI.Page
{
    #region[Private variables]
        DMPurchaseOrder Obj_PO = new DMPurchaseOrder();
        PurchaseOrder Entity_PO = new PurchaseOrder();
        CommanFunction Obj_Comm = new CommanFunction();
        public static DataSet DsGrd = new DataSet();
        DataSet DS = new DataSet();
        decimal TotalQty ,TotalAmt,TotalNetAmt,TotalTax = 0;
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
        SetInitialRowGRDOUTSTANDINGPO();
        lblCount.Text = "";
        rdoPOSTATUSWISE.SelectedValue = "0";
        ddlPurchaseNo.SelectedIndex = ddlInwardNo.SelectedIndex=ddlSite.SelectedIndex=0;
        ddlPurchaseNo.SelectedIndex = 0;
        TRGRDALLPO.Visible = true;
        GRDSUPLIEROSREPORT.Visible=TRGRDOUTSTANDING.Visible = false;
        rdoPOSTATUSWISE_SelectedIndexChanged(rdoPOSTATUSWISE,EventArgs.Empty);
        ddlInwardNo.SelectedValue = "0";

    }

    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("#", typeof(Int32)));
            dt.Columns.Add(new DataColumn("PODate", typeof(string)));
            dt.Columns.Add(new DataColumn("PONo", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
            dt.Columns.Add(new DataColumn("RemarkForPo", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("InwardId", typeof(Int32)));
            dt.Columns.Add(new DataColumn("InwardNo", typeof(string)));
            dt.Columns.Add(new DataColumn("InwardQty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PendingQty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Location", typeof(string)));
            dt.Columns.Add(new DataColumn("SuplierName", typeof(string))); 
            dr = dt.NewRow();

            dr["#"] = 0;
            dr["PODate"] = "";
            dr["PONo"] = "";
            dr["ItemName"] = "";
            dr["ItemDesc"] = "";
            dr["RemarkForPo"] = "";
            dr["Qty"] = 0;
            dr["InwardId"] = 0;
            dr["InwardNo"] = "";
            dr["InwardQty"] = 0;
            dr["PendingQty"] = 0;
            dr["Location"] = 0;
            dr["SuplierName"] = "";
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

    private void SetInitialRowGRDOUTSTANDINGPO()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("#", typeof(Int32)));
            dt.Columns.Add(new DataColumn("SuplierName", typeof(string)));
            dr = dt.NewRow();

            dr["#"] = 0;
            dr["SuplierName"] = "";
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GRDOUTSTANDINGPO.DataSource = dt;
            GRDOUTSTANDINGPO.DataBind();
            dt = null;
            dr = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    private void SetInitialRowGRDOUTSTANDINGITEM()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("#", typeof(Int32)));
            dt.Columns.Add(new DataColumn("SuplierName", typeof(string)));
            dt.Columns.Add(new DataColumn("pono", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
            dt.Columns.Add(new DataColumn("RemarkForPo", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("sumpoqty", typeof(string)));
            dt.Columns.Add(new DataColumn("suminqty", typeof(string)));
            dt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
            dr = dt.NewRow();

            dr["#"] = 0;
            dr["SuplierName"] = "";
            dr["pono"] = "";
            dr["ItemName"] = "";
            dr["ItemDesc"] = "";
            dr["RemarkForPo"] = "";
            dr["Unit"] = "";
            dr["sumpoqty"] = "";
            dr["suminqty"] = "";
            dr["BalanceQty"] = "";
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GRDSUPLIEROSREPORT.DataSource = dt;
            GRDSUPLIEROSREPORT.DataBind();
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

            DS = Obj_PO.FillComboReport(Convert.ToInt32(Session["UserID"]), out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlPurchaseNo.DataSource = DS.Tables[0];
                    ddlPurchaseNo.DataTextField = "PONo";
                    ddlPurchaseNo.DataValueField = "POId";
                    ddlPurchaseNo.DataBind();
                }
                if (DS.Tables[6].Rows.Count > 0)
                {
                    ddlSite.DataSource = DS.Tables[6];
                    ddlSite.DataTextField = "Location";
                    ddlSite.DataValueField = "StockLocationID";
                    ddlSite.DataBind();
                }
                if (DS.Tables[7].Rows.Count > 0)
                {
                    ddlInwardNo.DataSource = DS.Tables[7];
                    ddlInwardNo.DataTextField = "InwardNo";
                    ddlInwardNo.DataValueField = "InwardId";
                    ddlInwardNo.DataBind();
                }

            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void FillComboForOS()
    {
        try
        {

            DS = Obj_PO.FillComboReportForOS(Convert.ToInt32(Session["UserID"]), out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlPurchaseNo.DataSource = DS.Tables[0];
                    ddlPurchaseNo.DataTextField = "PONo";
                    ddlPurchaseNo.DataValueField = "POId";
                    ddlPurchaseNo.DataBind();
                }
                if (DS.Tables[6].Rows.Count > 0)
                {
                    ddlSite.DataSource = DS.Tables[6];
                    ddlSite.DataTextField = "Location";
                    ddlSite.DataValueField = "StockLocationID";
                    ddlSite.DataBind();
                }
                if (DS.Tables[7].Rows.Count > 0)
                {
                    ddlInwardNo.DataSource = DS.Tables[7];
                    ddlInwardNo.DataTextField = "InwardNo";
                    ddlInwardNo.DataValueField = "InwardId";
                    ddlInwardNo.DataBind();
                }

            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void ReportGrid(string StrCondition)
    {
        try
        {
            if (rdoPOSTATUSWISE.SelectedValue == "0")
            {
                DsGrd = Obj_PO.GetPOForStatusReport(StrCondition, out StrError);
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
                    lblCount.Text = DsGrd.Tables[0].Rows.Count + " Records Found";
                    lblCount.Visible = true;
                    SetInitialRow();
                }
            }
            else
            {
                DsGrd = Obj_PO.GetPOForStatusReportOS(StrCondition, out StrError);
                if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
                {
                    if (!FlagPrint)
                        ImgBtnPrint.Visible = true;
                    if (!FlagPrint)
                        ImgBtnExport.Visible = true;
                    GRDOUTSTANDINGPO.DataSource = DsGrd.Tables[0];
                    GRDOUTSTANDINGPO.DataBind();
                    lblCount.Text = DsGrd.Tables[0].Rows.Count + " Records Found";
                    lblCount.Visible = true;
                }
                else
                {
                    GRDOUTSTANDINGPO.DataSource = null;
                    GRDOUTSTANDINGPO.DataBind();
                    lblCount.Text = DsGrd.Tables[0].Rows.Count + " Records Found";
                    lblCount.Visible = true;
                    SetInitialRowGRDOUTSTANDINGPO();
                }
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Purchase Report'");
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
            //CheckUserRight();
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

            if (ChkFrmDate.Checked == true)
            {
                StrCondition = StrCondition + " and PO.PODate between'" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "'And'" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            else
            {
                StrCondition = StrCondition + "and PO.PODate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlPurchaseNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and PO.POId=" + Convert.ToInt32(ddlPurchaseNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlInwardNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IR.InwardId=" + Convert.ToInt32(ddlInwardNo.SelectedValue);
            }
            #region[LOCATION]

            if (Convert.ToInt32(ddlSite.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and SL.StockLocationID=" + Convert.ToInt32(ddlSite.SelectedValue);
            }
            else
            {
                //for (int i = 1; i < ddlSite.Items.Count; i++)
                //{
                //    if (i == 1)
                //    {
                //        if (Convert.ToInt32(ddlSite.Items[i].Value) != 0)
                //        {
                //            StrCondition += " and (SL.StockLocationID = " + Convert.ToInt32(ddlSite.Items[i].Value);
                //        }
                //    }
                //    else
                //    {
                //        if (Convert.ToInt32(ddlSite.Items[i].Value) != 0)
                //        {
                //            StrCondition += " or  SL.StockLocationID = " + Convert.ToInt32(ddlSite.Items[i].Value);
                //        }
                //    }
                //    if (i == ddlSite.Items.Count - 1)
                //    {
                //        if (Convert.ToInt32(ddlSite.Items[i].Value) != 0)
                //        {
                //            StrCondition += " )";
                //        }
                //    }
                //}
            }
            //}
            #endregion
            ReportGrid(StrCondition);
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

    protected void ChkFrmDate_CheckedChanged(object sender, EventArgs e)
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
                Obj_Comm.Export("Purchase Order Status Report.xls", GridExp);
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
                //TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem,"Qty"));
                //TotalAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
                //TotalNetAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));//TotalTax
                //TotalTax += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxAmount"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.Cells[5].Text="Total :";
                //e.Row.Cells[6].Text=TotalQty.ToString();
                //e.Row.Cells[8].Text = TotalAmt.ToString();
                //e.Row.Cells[10].Text = TotalTax.ToString();
                //e.Row.Cells[11].Text = TotalNetAmt.ToString();
            }
        }
        catch(Exception ex)
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
            if (ChkFrmDate.Checked == true)
            {
                StrCondition = StrCondition + " and PO.PODate between'" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "'And'" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            else
            {
                StrCondition = StrCondition + "and PO.PODate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlPurchaseNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and PO.POId=" + Convert.ToInt32(ddlPurchaseNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlInwardNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IR.InwardId=" + Convert.ToInt32(ddlInwardNo.SelectedValue);
            }
            #region[LOCATION]

            if (Convert.ToInt32(ddlSite.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and SL.StockLocationID=" + Convert.ToInt32(ddlSite.SelectedValue);
            }
            else
            {
                for (int i = 1; i < ddlSite.Items.Count; i++)
                {
                    if (i == 1)
                    {
                        if (Convert.ToInt32(ddlSite.Items[i].Value) != 0)
                        {
                            StrCondition += " and (SL.StockLocationID = " + Convert.ToInt32(ddlSite.Items[i].Value);
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(ddlSite.Items[i].Value) != 0)
                        {
                            StrCondition += " or  SL.StockLocationID = " + Convert.ToInt32(ddlSite.Items[i].Value);
                        }
                    }
                    if (i == ddlSite.Items.Count - 1)
                    {
                        if (Convert.ToInt32(ddlSite.Items[i].Value) != 0)
                        {
                            StrCondition += " )";
                        }
                    }
                }
            }
            //}
            #endregion
            DsGrd = Obj_PO.GetPOForDetailReport(StrCondition, out StrError);
            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                ImgBtnPrint.Visible = true;
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

    //========Important Required for Excel Export Must=============
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    //========Important Required for Excel Export Must=============

    protected void rdoPOSTATUSWISE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoPOSTATUSWISE.SelectedValue == "0")
        {
            TRGRDALLPO.Visible = true;
           GRDSUPLIEROSREPORT.Visible= TRGRDOUTSTANDING.Visible = false;
            FillCombo();
            ddlPurchaseNo.Enabled=ddlInwardNo.Enabled=ddlSite.Enabled = true;
        }
        else
        {
            GRDSUPLIEROSREPORT.Visible=TRGRDALLPO.Visible = false;
            TRGRDOUTSTANDING.Visible = true;
            FillComboForOS();
            ReportGrid("");
            ddlPurchaseNo.Enabled=ddlInwardNo.Enabled = ddlSite.Enabled = false;
        }
    }

    protected void GRDOUTSTANDINGPO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        DataSet DSInwardDetails = new DataSet();
                        StrCondition = string.Empty;
                        StrCondition = " AND C.SuplierId="+e.CommandArgument;
                        DSInwardDetails = Obj_PO.GetPOForStatusReportOSSupplierItems(StrCondition, out StrError);
                        if (DSInwardDetails.Tables.Count > 0 && DSInwardDetails.Tables[0].Rows.Count > 0)
                        {
                            GRDSUPLIEROSREPORT.DataSource = DSInwardDetails.Tables[0];
                            GRDSUPLIEROSREPORT.DataBind();
                            MergeRows(GRDSUPLIEROSREPORT);
                            GRDSUPLIEROSREPORT.Visible = true;
                        }
                        else
                        {
                            GRDSUPLIEROSREPORT.DataSource = null;
                            GRDSUPLIEROSREPORT.DataBind();
                            GRDSUPLIEROSREPORT.Visible = false;
                        }
                    }
                    break;

            }
        }
        catch (Exception ex)
        {

        }
    }

    public void MergeRows(GridView gridView)
    {

        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            if (row.Cells[1].Text == previousRow.Cells[1].Text)
            {
                row.Cells[1].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 :
                                       previousRow.Cells[1].RowSpan + 1;
                previousRow.Cells[1].Visible = false;
               
            }
            if (row.Cells[3].Text == previousRow.Cells[3].Text)
            {
                row.Cells[2].RowSpan = previousRow.Cells[2].RowSpan < 2 ? 2 :
                                       previousRow.Cells[2].RowSpan + 1;
                previousRow.Cells[2].Visible = false;

            }
            if (row.Cells[3].Text == previousRow.Cells[3].Text)
            {
                row.Cells[3].RowSpan = previousRow.Cells[3].RowSpan < 2 ? 2 :
                                       previousRow.Cells[3].RowSpan + 1;
                previousRow.Cells[3].Visible = false;

            }
            if (row.Cells[4].Text == previousRow.Cells[4].Text)
            {
                row.Cells[4].RowSpan = previousRow.Cells[4].RowSpan < 2 ? 2 :
                                       previousRow.Cells[4].RowSpan + 1;
                previousRow.Cells[4].Visible = false;

            }

        }
    }
   
}
