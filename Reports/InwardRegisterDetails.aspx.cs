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

using System.Data.SqlClient;
using System.Drawing;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;


public partial class Reports_InwardRegisterDetails : System.Web.UI.Page
{
    #region[Variables]
        DMMaterialInwardReg Obj_MaterialInwardReg = new DMMaterialInwardReg();
        MaterialInwardReg Entity_MaterialInwardReg = new MaterialInwardReg();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        decimal Amount = 0;
        decimal NetAmt = 0;
        decimal TaxPer = 0, TaxAmt = 0, DiscPer = 0, DiscAmt = 0;
        string InwardNo = string.Empty;
        // decimal SubTotalQty = 0;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagPrint = false;
    #endregion

    #region[Variables]
    private void FillCombo()
    {
        DataSet dsCombo = new DataSet();
        string COND = string.Empty;
        //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
        //{
            //COND = COND + " AND Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
        //}
            dsCombo = Obj_MaterialInwardReg.FillReportCombo(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
        if (dsCombo.Tables.Count > 0)
        {
            if (dsCombo.Tables[0].Rows.Count > 0)
            {
                ddlSupp.DataSource = dsCombo.Tables[0];
                ddlSupp.DataTextField = "SuplierName";
                ddlSupp.DataValueField = "SuplierId";
                ddlSupp.DataBind();
            }
            if (dsCombo.Tables[1].Rows.Count > 0)
            {
                ddlInwardNo.DataSource = dsCombo.Tables[1];
                ddlInwardNo.DataTextField = "InwardNo";
                ddlInwardNo.DataValueField = "InwardId";
                ddlInwardNo.DataBind();
            }
            if (dsCombo.Tables[2].Rows.Count > 0)
            {
                ddlItemName.DataSource = dsCombo.Tables[2];
                ddlItemName.DataTextField = "Item";
                ddlItemName.DataValueField = "ItemId";
                ddlItemName.DataBind();
            }
            if (dsCombo.Tables[3].Rows.Count > 0)
            {
                ddlCategory.DataSource = dsCombo.Tables[3];
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();
            }

            if (dsCombo.Tables[4].Rows.Count > 0)
            {
                ddlsubcategory.DataSource = dsCombo.Tables[4];
                ddlsubcategory.DataTextField = "SubCategory";
                ddlsubcategory.DataValueField = "SubCategoryId";
                ddlsubcategory.DataBind();
            }

        }
        else
        {
            dsCombo = null;
        }
    }
    private void MakeEmptyForm()
    {
        if (!FlagPrint)
            ImgBtnPrint.Visible = true;
        if (!FlagPrint)
            ImgBtnExport.Visible = true;
        ChkFrmDate.Checked = true;
        txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
        txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        ddlItemName.SelectedIndex = 0;
        ddlInwardNo.SelectedIndex = 0;
        ddlSupp.SelectedIndex = 0;
        FillCombo();
        lblCount.Visible = false;
        SetInitialRow();
        ddlInwardNo.SelectedValue = "0";
    }
    public void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            //dt.Columns.Add(new DataColumn("#", typeof(int)));
            dt.Columns.Add(new DataColumn("InwardNo", typeof(string)));
            dt.Columns.Add(new DataColumn("InwardDate", typeof(string)));
            dt.Columns.Add(new DataColumn("PONO", typeof(string)));
            dt.Columns.Add(new DataColumn("type", typeof(string)));
            dt.Columns.Add(new DataColumn("SuplierName", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("InwardQty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("OrderQty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PendingQty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("InwardRate", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PORate", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Diffrence", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TaxPer", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DiscPer", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DiscAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TaxAmount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("NetAmount", typeof(decimal)));
            dr = dt.NewRow();

            //dr["#"] = 0;
            dr["InwardNo"] = "";
            dr["InwardDate"] = "";
            dr["PONO"] = "";
            dr["type"] = "";
            dr["Unit"] = "";
            dr["SuplierName"] = "";
            dr["ItemName"] = "";
            dr["InwardQty"] = 0;
            dr["OrderQty"] = 0;
            dr["InwardRate"] = 0;
            dr["PORate"] = 0;
            dr["Diffrence"] = 0;
            dr["Amount"] = 0;
            dr["TaxPer"] = 0;
            dr["TaxAmount"] = 0;
            dr["DiscPer"] = 0;
            dr["DiscAmt"] = 0;
            dr["NetAmount"] = 0;
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GridInwordDetails.DataSource = dt;
            GridInwordDetails.DataBind();
            dt = null;
            dr = null;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void ReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsGrd = new DataSet();
            DsGrd = Obj_MaterialInwardReg.GetInwardDetailsReport(RepCondition, out StrError);

            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                if (!FlagPrint)
                    ImgBtnPrint.Visible = true;
                if (!FlagPrint)
                    ImgBtnExport.Visible = true;
                GridInwordDetails.DataSource = DsGrd.Tables[0];
                GridInwordDetails.DataBind();
                lblCount.Text = DsGrd.Tables[0].Rows.Count + " Record Found";
                lblCount.Visible = true;
            }
            else
            {
                GridInwordDetails.DataSource = null;
                GridInwordDetails.DataBind();
                lblCount.Text = DsGrd.Tables[0].Rows.Count + " Record Found";
                lblCount.Visible = true;
                SetInitialRow();
            }
            //Obj_Quot = null;
            DsGrd = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Inward Report'");
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
            MakeEmptyForm();
            SetInitialRow();
        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
          try
        {
            DataSet DsGrd = new DataSet();
            StrCondition = string.Empty;
            if (Convert.ToInt32(RdoType.SelectedValue)!= 2)
            {
                StrCondition = StrCondition + " and IR.InwardThrough =" + Convert.ToInt32(RdoType.SelectedValue);
            }

            if (ChkFrmDate.Checked == true)
            {
                StrCondition = StrCondition + " and IR.InwardDate between '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "' ";
            }
            else
            {
                StrCondition = StrCondition + " AND IR.InwardDate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "' ";
            }

            if (Convert.ToInt32(ddlSupp.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and SM.SuplierId =" + Convert.ToInt32(ddlSupp.SelectedValue);
            }
            if (Convert.ToInt32(ddlInwardNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IR.InwardId=" + Convert.ToInt32(ddlInwardNo.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlItemName.Text) && Convert.ToInt32(ddlItemName.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IM.ItemId=" + Convert.ToInt32(ddlItemName.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlCategory.Text) && Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " And IC.CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlsubcategory.Text) && Convert.ToInt32(ddlsubcategory.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " And IM.SubCategoryId=" + Convert.ToInt32(ddlsubcategory.SelectedValue);
            }
        
            ReportGrid(StrCondition);
        }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }

    }
    protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataSet DsGrd = new DataSet();
            StrCondition = string.Empty;
            if (Convert.ToInt32(RdoType.SelectedValue) != 2)
            {
                StrCondition = StrCondition + " and IR.InwardThrough =" + Convert.ToInt32(RdoType.SelectedValue);
            }
            if (ChkFrmDate.Checked == true)
            {
                StrCondition = StrCondition + " and IR.InwardDate between '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "' ";
            }
            else
            {
                StrCondition = StrCondition + " AND IR.InwardDate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "' ";
            }
            if (Convert.ToInt32(ddlSupp.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and SM.SuplierId =" + Convert.ToInt32(ddlSupp.SelectedValue);
            }
            if (Convert.ToInt32(ddlInwardNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IR.InwardId=" + Convert.ToInt32(ddlInwardNo.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlItemName.Text) && Convert.ToInt32(ddlItemName.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IM.ItemId=" + Convert.ToInt32(ddlItemName.SelectedValue);
            }
           // //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
           // //{
           //     StrCondition = StrCondition + " AND IR.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
           //// }
             DsGrd = Obj_MaterialInwardReg.GetInwardDetailsReport(StrCondition, out StrError);
                if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
                {
                    //========Call Register
                    GridView GridExp = new GridView();
                    DsGrd.Tables[0].Columns.Remove("InwardRate");
                    DsGrd.Tables[0].Columns.Remove("PORate");
                    GridExp.DataSource = DsGrd.Tables[0];
                    GridExp.DataBind();
                    obj_Comman.Export("InwardRegisterDetails.xls", GridExp);
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("No Data Found To Export..!", this.Page);
                    DsGrd.Dispose();
                    GridInwordDetails.DataSource = null;
                    GridInwordDetails.DataBind();
                }
                //ds = null;
            
        }
        catch (ThreadAbortException tex)
        {

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    protected void GridInwordDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow && !string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InwardNo"))))
            {
                // Taxamt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DiscAmnt"));
                Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
                TaxPer += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxPer"));
                TaxAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TaxAmount"));
                DiscPer += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DiscPer"));
                DiscAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DiscAmt"));
                // amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmnt"));
                NetAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
                if ((Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InwardRate")) > Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PORate"))) || (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InwardRate")) < Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PORate"))))
                {
                    //e.Row.ForeColor = Color.Red;
                    for (int y = 0; y < e.Row.Cells.Count; y++)
                    {
                        e.Row.Cells[y].ForeColor = System.Drawing.Color.FromName("Red");
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[13].Text = "Total :";
                e.Row.Cells[14].Text = Amount.ToString("0.00");
                //e.Row.Cells[13].Text = TaxPer.ToString("0.00");
                e.Row.Cells[16].Text = TaxAmt.ToString("0.00");
                //e.Row.Cells[15].Text = DiscPer.ToString("0.00");
                e.Row.Cells[18].Text = DiscAmt.ToString("0.00");
                e.Row.Cells[19].Text = NetAmt.ToString("0.00");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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
            txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
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

    //========Important Required for Excel Export Must=============
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    //========Important Required for Excel Export Must=============
    protected void GridInwordDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.GridInwordDetails.PageIndex = e.NewPageIndex;
            DataSet DsGrd = new DataSet();
            StrCondition = string.Empty;
            if (ChkFrmDate.Checked == true)
            {
                StrCondition = StrCondition + " and IR.InwardDate between '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "' ";
            }
            else
            {
                StrCondition = StrCondition + " AND IR.InwardDate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "' ";
            }

            if (Convert.ToInt32(ddlSupp.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and SM.SuplierId =" + Convert.ToInt32(ddlSupp.SelectedValue);
            }
            if (Convert.ToInt32(ddlInwardNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IR.InwardId=" + Convert.ToInt32(ddlInwardNo.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlItemName.Text) && Convert.ToInt32(ddlItemName.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IM.ItemId=" + Convert.ToInt32(ddlItemName.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlCategory.Text) && Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " And IC.CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
            }
           // //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
           // //{
           //     StrCondition = StrCondition + " AND IR.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
           //// }
            DsGrd = Obj_MaterialInwardReg.GetInwardDetailsReport(StrCondition, out StrError);

            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                GridInwordDetails.DataSource = DsGrd.Tables[0];
                GridInwordDetails.DataBind();
                lblCount.Text = DsGrd.Tables[0].Rows.Count + " Record Found";
                lblCount.Visible = true;
            }
            else
            {
                GridInwordDetails.DataSource = null;
                GridInwordDetails.DataBind();
                lblCount.Text = DsGrd.Tables[0].Rows.Count + " Record Found";
                lblCount.Visible = true;
                SetInitialRow();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string COND = string.Empty;
            DataSet DS = new DataSet();
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
                        ddlItemName.DataSource = DS.Tables[0];
                        ddlItemName.DataTextField = "ItemName";
                        ddlItemName.DataValueField = "ItemId";
                        ddlItemName.DataBind();
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
            DataSet DS = new DataSet();
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
                        ddlItemName.DataSource = DS.Tables[0];
                        ddlItemName.DataTextField = "ItemName";
                        ddlItemName.DataValueField = "ItemId";
                        ddlItemName.DataBind();
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
