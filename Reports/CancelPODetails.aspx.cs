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

public partial class Reports_CancelPODetails : System.Web.UI.Page
{
    #region[Private variables]
        DMPurchaseOrder Obj_PO = new DMPurchaseOrder();
        PurchaseOrder Entity_PO = new PurchaseOrder();
        CommanFunction Obj_Comm = new CommanFunction();
        public static DataSet DsGrd = new DataSet();
        DataSet DS = new DataSet();
        decimal TotalQty, TotalAmt, TotalNetAmt, TotalTax, TotalDisc = 0;
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
        lblCount.Text = "";
        ddlSupplier.SelectedIndex = ddlCategory.SelectedIndex=ddlSize.SelectedIndex=ddlUnit.SelectedIndex=ddlItem.SelectedIndex=ddlNo.SelectedIndex=0;
        ddlNo.SelectedIndex = 0;
        RdoType.SelectedValue = "All";
        
    }

    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("#", typeof(Int32)));
            dt.Columns.Add(new DataColumn("PONO", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
            dt.Columns.Add(new DataColumn("RemarkForPo", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("Location", typeof(string)));
            dt.Columns.Add(new DataColumn("Supplier", typeof(string)));
            dt.Columns.Add(new DataColumn("PODate", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Rate", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TaxPer", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TaxAmount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DiscPer", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DiscAmount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("NetAmount", typeof(decimal)));
            //Location

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["PONO"] = "";
            dr["ItemName"] = "";
            dr["ItemDesc"] = "";
            dr["RemarkForPo"] = "";
            dr["Unit"] = "";
            dr["Location"] = "";
            dr["Supplier"] = "";
            dr["PODate"] = "";
            dr["Qty"] = 0;
            dr["Rate"] = 0;
            dr["Amount"] = 0;
            dr["TaxPer"] = 0;
            dr["TaxAmount"] = 0;
            dr["DiscPer"] = 0;
            dr["DiscAmount"] = 0;
            dr["NetAmount"] = 0;


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

            DS = Obj_PO.FillComboReport(Convert.ToInt32(Session["UserID"]), out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlNo.DataSource = DS.Tables[10];
                    ddlNo.DataTextField = "PONo";
                    ddlNo.DataValueField = "POId";
                    ddlNo.DataBind();
                }
                if (DS.Tables[1].Rows.Count > 0)
                {
                    ddlSupplier.DataSource = DS.Tables[1];
                    ddlSupplier.DataTextField = "SuplierName";
                    ddlSupplier.DataValueField = "SuplierId";
                    ddlSupplier.DataBind();
                }
                if (DS.Tables[2].Rows.Count > 0)
                {
                    ddlItem.DataSource = DS.Tables[2];
                    ddlItem.DataTextField = "ItemName";
                    ddlItem.DataValueField = "ItemId";
                    ddlItem.DataBind();
                }

                if (DS.Tables[3].Rows.Count > 0)
                {
                    ddlCategory.DataSource = DS.Tables[3];
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();
                }
                if (DS.Tables[4].Rows.Count > 0)
                {
                    ddlUnit.DataSource = DS.Tables[4];
                    ddlUnit.DataTextField = "Unit";
                    ddlUnit.DataValueField = "UnitId";
                    ddlUnit.DataBind();
                }
                if (DS.Tables[5].Rows.Count > 0)
                {
                    ddlSize.DataSource = DS.Tables[5];
                    ddlSize.DataTextField = "SizeName";
                    ddlSize.DataValueField = "SizeId";
                    ddlSize.DataBind();
                }

                if (DS.Tables[6].Rows.Count > 0)
                {
                    ddlSite.DataSource = DS.Tables[6];
                    ddlSite.DataTextField = "Location";
                    ddlSite.DataValueField = "StockLocationID";
                    ddlSite.DataBind();
                }
                
               if (DS.Tables[9].Rows.Count > 0)
                {
                    ddlsubcategory.DataSource = DS.Tables[9];
                    ddlsubcategory.DataTextField = "CategoryName";
                    ddlsubcategory.DataValueField = "CategoryId";
                    ddlsubcategory.DataBind();
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
            DsGrd = Obj_PO.GetCancelPOForDetailReport(StrCondition, out StrError);
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

            if (ChkFrmDate.Checked == true)
            {
                StrCondition = StrCondition + " and PO.PODate between'" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "'And'" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            else
            {
                StrCondition = StrCondition + "and PO.PODate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and PO.POId=" + Convert.ToInt32(ddlNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlSupplier.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and PO.SuplierId=" + Convert.ToInt32(ddlSupplier.SelectedValue);
            }
            if (Convert.ToInt32(ddlItem.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and POD.ItemId=" + Convert.ToInt32(ddlItem.SelectedValue);
            }

            //---For Category---
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and IC.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlUnit.SelectedValue) > 0)
            {
                StrCondition += " and UM.UnitId = " + Convert.ToInt32(ddlUnit.SelectedValue);
            }
            //if (Convert.ToInt32(ddlSize.SelectedValue) > 0)
            //{
            //    StrCondition += " and ISD.SizeId = " + Convert.ToInt32(ddlSize.SelectedValue);
            //}
            if (RdoType.SelectedValue == "All")
            {
                StrCondition += "And PO.POStatus in ('Generated','Approved','Authorised')";
            }
            if (RdoType.SelectedValue == "Generated")
            {
                StrCondition += " And PO.POStatus = " + "'" + RdoType.SelectedValue + "'";
            }
            if (RdoType.SelectedValue == "Approved")
            {
                StrCondition += " And PO.POStatus = " + "'" + RdoType.SelectedValue + "'";
            }
            if (RdoType.SelectedValue == "Authorised")
            {
                StrCondition += " And PO.POStatus = " + "'" + (RdoType.SelectedValue) + "'";
            }
            if (RdoType.SelectedValue == "Authorised")
            {
                StrCondition += " And RCD.CafeteriaId = " + (ddlSite.SelectedValue) + "'";
            }
            
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
                Obj_Comm.Export("Cancelled Purchase Order Details Report.xls", GridExp);
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
                TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem,"Qty"));
                TotalAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
                TotalNetAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));//TotalTax
                TotalTax += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxAmount"));
                TotalDisc += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DiscAmount"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[9].Text="Total :";
                e.Row.Cells[10].Text=TotalQty.ToString();
                e.Row.Cells[12].Text = TotalAmt.ToString();
                e.Row.Cells[14].Text = TotalTax.ToString();
                e.Row.Cells[16].Text = TotalDisc.ToString();
                e.Row.Cells[17].Text = TotalNetAmt.ToString();
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
            if (Convert.ToInt32(ddlNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and PO.POId=" + Convert.ToInt32(ddlNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlSupplier.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and PO.SuplierId=" + Convert.ToInt32(ddlSupplier.SelectedValue);
            }
            if (Convert.ToInt32(ddlItem.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and POD.ItemId=" + Convert.ToInt32(ddlItem.SelectedValue);
            }

            //---For Category---
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and IC.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlUnit.SelectedValue) > 0)
            {
                StrCondition += " and UM.UnitId = " + Convert.ToInt32(ddlUnit.SelectedValue);
            }
            //if (Convert.ToInt32(ddlSize.SelectedValue) > 0)
            //{
            //    StrCondition += " and ISD.SizeId = " + Convert.ToInt32(ddlSize.SelectedValue);
            //}
            if (RdoType.SelectedValue == "All")
            {
                StrCondition += "And PO.POStatus in ('Generated','Approved','Authorised')";
            }
            if (RdoType.SelectedValue == "Generated")
            {
                StrCondition += " And PO.POStatus = " + "'" + RdoType.SelectedValue + "'";
            }
            if (RdoType.SelectedValue == "Approved")
            {
                StrCondition += " And PO.POStatus = " + "'" + RdoType.SelectedValue + "'";
            }
            if (RdoType.SelectedValue == "Authorised")
            {
                StrCondition += " And PO.POStatus = " + "'" + (RdoType.SelectedValue) + "'";
            }

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
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string COND = string.Empty;
            DMFILTER DMFILTER = new DMFILTER();
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                COND = COND + " AND CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            DS = DMFILTER.FillReportComboCategoryWise(1, 0, COND, out StrError);
            {
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        ddlItem.DataSource = DS.Tables[0];
                        ddlItem.DataTextField = "ItemName";
                        ddlItem.DataValueField = "ItemId";
                        ddlItem.DataBind();
                    }
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        ddlsubcategory.DataSource = DS.Tables[1];
                        ddlsubcategory.DataTextField = "Name";
                        ddlsubcategory.DataValueField = "Id";
                        ddlsubcategory.DataBind();
                    }
                }
                else
                {
                    DS = null;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string COND = string.Empty;
            DMFILTER DMFILTER = new DMFILTER();
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                COND = COND + " AND CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlsubcategory.SelectedValue) > 0)
            {
                COND = COND + " AND SubcategoryId=" + Convert.ToInt32(ddlsubcategory.SelectedValue);
            }
            DS = DMFILTER.FillReportComboCategoryWise(2, 0, COND, out StrError);
            {
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        ddlItem.DataSource = DS.Tables[0];
                        ddlItem.DataTextField = "ItemName";
                        ddlItem.DataValueField = "ItemId";
                        ddlItem.DataBind();
                    }
                }
                else
                {
                    DS = null;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
