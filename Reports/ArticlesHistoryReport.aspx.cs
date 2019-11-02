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


public partial class Reports_ArticlesHistoryReport: System.Web.UI.Page
{
    #region[Variables]
        DMArticleHistoryReport Obj_ArticleHistory = new DMArticleHistoryReport();
        MaterialInwardReg Entity_MaterialInwardReg = new MaterialInwardReg();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        decimal Amount = 0;
        decimal Rate = 0;
        string InwardNo = string.Empty;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagPrint = false;
    #endregion

    #region[UserDefineFunction]
     private void FillCombo()
    {
        DataSet dsCombo = new DataSet();
        dsCombo = Obj_ArticleHistory.FillReportCombo(out StrError);
        if (dsCombo.Tables.Count > 0)
        {
            if (dsCombo.Tables[0].Rows.Count > 0)
            {
                ddlItemName.DataSource = dsCombo.Tables[0];
                ddlItemName.DataTextField = "Item";
                ddlItemName.DataValueField = "ItemId";
                ddlItemName.DataBind();
            }
            if (dsCombo.Tables[1].Rows.Count > 0)
            {
                ddlCategory.DataSource = dsCombo.Tables[1];
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();
            }
            if (dsCombo.Tables[2].Rows.Count > 0)
            {
                ddlSubCategory.DataSource = dsCombo.Tables[2];
                ddlSubCategory.DataTextField = "SubCategory";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataBind();
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
        ddlSubCategory.SelectedIndex = ddlItemName.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;

        lblCount.Visible = false;
        SetInitialRow();
    }
     public void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("InwardDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Category", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("InwardQty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("InwardRate", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));
           
            dr = dt.NewRow();

            dr["InwardDate"] = "";
            dr["Category"] = "";
            dr["ItemName"] = "";
            dr["ItemDesc"] = "";
            dr["Unit"] = "";
            dr["InwardQty"] = 0;
            dr["InwardRate"] = 0;
            dr["Amount"] = 0;
            
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
            DsGrd = Obj_ArticleHistory.GetInwardDetailsReport(RepCondition, out StrError);
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Artical History Report'");
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

            if (ChkFrmDate.Checked == true)
            {
                StrCondition = StrCondition + " and IR.InwardDate between '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "' ";
            }
            else
            {
                StrCondition = StrCondition + " AND IR.InwardDate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "' ";
            }
            if (!string.IsNullOrEmpty(ddlItemName.Text) && Convert.ToInt32(ddlItemName.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IM.ItemId=" + Convert.ToInt32(ddlItemName.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlCategory.Text) && Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " And IC.CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlSubCategory.Text) && Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " And IM.SubCategoryId=" + Convert.ToInt32(ddlSubCategory.SelectedValue);
            }
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                StrCondition = StrCondition + " AND IR.Location =" + Convert.ToInt32(Session["CafeteriaId"].ToString());
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

            if (ChkFrmDate.Checked == true)
            {
                StrCondition = StrCondition + " and IR.InwardDate between '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "' ";
            }
            else
            {
                StrCondition = StrCondition + " AND IR.InwardDate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "' ";
            }
            if (!string.IsNullOrEmpty(ddlItemName.Text) && Convert.ToInt32(ddlItemName.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IM.ItemId=" + Convert.ToInt32(ddlItemName.SelectedValue);
            }
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                StrCondition = StrCondition + " AND IR.Location =" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            if (!string.IsNullOrEmpty(ddlSubCategory.Text) && Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " And IM.SubCategoryId=" + Convert.ToInt32(ddlSubCategory.SelectedValue);
            }
             DsGrd = Obj_ArticleHistory.GetInwardDetailsReport(StrCondition, out StrError);
                if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
                {
                    //========Call Register
                    GridView GridExp = new GridView();
                    GridExp.DataSource = DsGrd.Tables[0];
                    GridExp.DataBind();
                    obj_Comman.Export("ArticlesHistory.xls", GridExp);
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
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            //    Rate += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InwardRate"));
            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //   e.Row.Cells[3].Text = "Total";
            //   e.Row.Cells[6].Text = Amount.ToString("0.00");
            //   e.Row.Cells[5].Text = Rate.ToString("0.00");
            //}
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
            if (!string.IsNullOrEmpty(ddlItemName.Text) && Convert.ToInt32(ddlItemName.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IM.ItemId=" + Convert.ToInt32(ddlItemName.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlCategory.Text) && Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " And IC.CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                StrCondition = StrCondition + " AND IR.Location =" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            if (!string.IsNullOrEmpty(ddlSubCategory.Text) && Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " And IM.SubCategoryId=" + Convert.ToInt32(ddlSubCategory.SelectedValue);
            }
            DsGrd = Obj_ArticleHistory.GetInwardDetailsReport(StrCondition, out StrError);

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
                        ddlSubCategory.DataSource = DS.Tables[1];
                        ddlSubCategory.DataTextField = "Name";
                        ddlSubCategory.DataValueField = "Id";
                        ddlSubCategory.DataBind();
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

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
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
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                COND = COND + " AND SubcategoryId=" + Convert.ToInt32(ddlSubCategory.SelectedValue);
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
