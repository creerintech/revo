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

public partial class Reports_MaterialReqSummary : System.Web.UI.Page
{
    #region[Private Variables]
        DMStockMaster Obj_StockMaster = new DMStockMaster();
        StockMaster Entity_StockMaster = new StockMaster();
        CommanFunction Obj_Comm = new CommanFunction();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        DataSet DS = new DataSet();
        private static bool FlagPrint = false;
        decimal ReqQty, IssuQty, Pending, Rate, Amount;
    #endregion

    #region[User Defined Function]
    private void SetInitialRow()
    {
        DataTable Dt = new DataTable();
        DataRow dr;

        Dt.Columns.Add("Type", typeof(string));
        Dt.Columns.Add("StockNo", typeof(string));
        Dt.Columns.Add("Date", typeof(string));
        Dt.Columns.Add("Employee", typeof(string));
        Dt.Columns.Add("Category", typeof(string));
        Dt.Columns.Add("Unit", typeof(string));
        Dt.Columns.Add("Items", typeof(string));
        Dt.Columns.Add("ItemDesc", typeof(string));
        Dt.Columns.Add("InwardQty", typeof(decimal));
        Dt.Columns.Add("OutwardQty", typeof(decimal));
        Dt.Columns.Add("Location", typeof(string));
        Dt.Columns.Add("InwardNo", typeof(string));
        Dt.Columns.Add("Pending_Qty", typeof(decimal));
        Dt.Columns.Add("Status", typeof(string));
        Dt.Columns.Add("Rate", typeof(decimal));
        Dt.Columns.Add("Amount", typeof(decimal));
        dr = Dt.NewRow();

        dr["Type"] = "";
        dr["StockNo"] = "";
        dr["Date"] = "";
        dr["Employee"] = "";
        dr["Unit"] = "";
        dr["Items"] = "";
        dr["ItemDesc"] = "";
        dr["InwardQty"] = 0;
        dr["OutwardQty"] = 0;
        dr["Location"] = "";
        dr["Category"] = "";
        dr["InwardNo"] = "";
        dr["Pending_Qty"] = 0;
        dr["Status"] = "";
        dr["Rate"] = 0;
        dr["Amount"] = 0;
        Dt.Rows.Add(dr);

        ViewState["CurrentTable"] = Dt;
        GridDetails.DataSource = Dt;
        GridDetails.DataBind();
    }
    public void MakeEmptyForm()
    {
        if (!FlagPrint)
          ImgBtnPrint.Visible = true;
        if (!FlagPrint)
          ImgBtnExcel.Visible = true;
        TxtFromDate.Enabled = TxtToDate.Enabled = true;
        TxtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
        TxtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        lblCount.Text = "";
        ChkFromDate.Checked = true;
        ddlSubCategory.SelectedIndex = ddlStockNo.SelectedIndex = ddlLocation.SelectedIndex = ddlCategory.SelectedIndex = ddlItems.SelectedIndex = 0;
        SetInitialRow();
        RdoType.SelectedIndex = 0;
        ReqQty=IssuQty=Pending=Rate=Amount=0;
       // ReportGrid();

    }
    public void ReportGrid()
    {
        StrCondition = string.Empty;
        try
        {
            if (ChkFromDate.Checked)
            {
                StrCondition += " and OutwardRegister.ConsumptionDate between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and OutwardRegister.ConsumptionDate between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlStockNo.SelectedValue) > 0)
            {
                StrCondition += " and OutwardRegister.OutwardId = " + Convert.ToInt32(ddlStockNo.SelectedValue);
            }
            #region[LOCATION]

                if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and OutwardRegisterDtls.LocationId=" + Convert.ToInt32(ddlLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (OutwardRegisterDtls.LocationId = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or  OutwardRegisterDtls.LocationId = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        if (i == ddlLocation.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
            
            #endregion
            if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
            {
                StrCondition += " and OutwardRegisterDtls.ItemId= " + Convert.ToInt32(ddlItems.SelectedValue);
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and ItemCategory.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                StrCondition += " and ItemMaster.SubCategoryId = " + Convert.ToInt32(ddlSubCategory.SelectedValue);
            }
            if (Convert.ToString(RdoType.SelectedValue) != "All")
            {
                StrCondition += " and OutwardRegister.Type= '" + Convert.ToString(RdoType.SelectedValue)+"'";
            }
            DS = Obj_StockMaster.GetAssignStockForConsumption(StrCondition, out StrError);

            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                if (!FlagPrint)
                  ImgBtnPrint.Visible = true;
                if (!FlagPrint)
                  ImgBtnExcel.Visible = true;
               // ViewState["IScancel"] = DS.Tables[0];
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
               
                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + " Record Found";
                DS = null;

                //ScriptManager.RegisterStartupScript(this,this.GetType(),"starScript","")
            }
            else
            {
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + " Record Found";
                //lblCount.Text = "";
                DS = null;
                SetInitialRow();
                ImgBtnPrint.Visible = false;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void FillCombo()
    {
        try
        {
            string COND = string.Empty;
            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
            //    COND = COND + " AND P.Loc=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}
            DS = Obj_StockMaster.FillStockNoComboForReport(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
            {
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        ddlStockNo.DataSource = DS.Tables[0];
                        ddlStockNo.DataTextField = "StockNo";
                        ddlStockNo.DataValueField = "OutwardId";
                        ddlStockNo.DataBind();
                    }
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        ddlLocation.DataSource = DS.Tables[1];
                        ddlLocation.DataTextField = "Location";
                        ddlLocation.DataValueField = "StockLocationID";
                        ddlLocation.DataBind();
                    }
                    if (DS.Tables[3].Rows.Count > 0)
                    {
                        ddlItems.DataSource = DS.Tables[3];
                        ddlItems.DataTextField = "ItemName";
                        ddlItems.DataValueField = "ItemId";
                        ddlItems.DataBind();
                    }
                    if (DS.Tables[2].Rows.Count > 0)
                    {
                        ddlCategory.DataSource = DS.Tables[2];
                        ddlCategory.DataTextField = "CategoryName";
                        ddlCategory.DataValueField = "CategoryId";
                        ddlCategory.DataBind();
                    }
                    if (DS.Tables[4].Rows.Count > 0)
                    {
                        ddlSubCategory.DataSource = DS.Tables[4];
                        ddlSubCategory.DataTextField = "SubCategory";
                        ddlSubCategory.DataValueField = "SubCategoryId";
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
    public bool ChkDateFormat()
    {
        bool flag = false;
        try
        {
            if (ChkFromDate.Checked == true)
            {
                if (!string.IsNullOrEmpty(TxtFromDate.Text) && !string.IsNullOrEmpty(TxtToDate.Text))
                {
                    if ((Convert.ToDateTime(TxtFromDate.Text) <= DateTime.Now) && (Convert.ToDateTime(TxtToDate.Text) <= DateTime.Now))
                    {
                        if (Convert.ToDateTime(TxtFromDate.Text) <= Convert.ToDateTime(TxtToDate.Text))
                        {
                            flag = true;
                        }
                        else
                        {
                            Obj_Comm.ShowPopUpMsg("From Date Must Be Less Than To Date...", this.Page);
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
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return flag;
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Issue Report'");
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
                        ImgBtnExcel.Visible = false;
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
        if (!IsPostBack)
        {
            FillCombo();
            CheckUserRight();
            MakeEmptyForm();
        }
    }
    protected void ChkFromDate_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkFromDate.Checked == true)
        {
            TxtFromDate.Enabled = TxtToDate.Enabled = true;
        }
        else
        {
            TxtFromDate.Enabled = TxtToDate.Enabled = false;
        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        if (ChkDateFormat() == true)
        {
            ReportGrid();
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
    protected void ImgBtnExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ChkFromDate.Checked)//and RC.RequisitionCafeId=22 and RC.CafeteriaId=4 and RC.RequisitionDate
            {
                StrCondition += " and OutwardRegister.StockAsOn between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and OutwardRegister.StockAsOn between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlStockNo.SelectedValue) > 0)
            {
                StrCondition += " and OutwardRegister.OutwardId = " + Convert.ToInt32(ddlStockNo.SelectedValue);
            }
            #region[LOCATION]

                if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and OutwardRegisterDtls.LocationId=" + Convert.ToInt32(ddlLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (OutwardRegisterDtls.LocationId = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or  OutwardRegisterDtls.LocationId = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        if (i == ddlLocation.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
            
            #endregion
            if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
            {
                StrCondition += " and OutwardRegisterDtls.ItemId= " + Convert.ToInt32(ddlItems.SelectedValue);
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and ItemCategory.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToString(RdoType.SelectedValue) != "All")
            {
                StrCondition += " and OutwardRegister.Type= '" + Convert.ToString(RdoType.SelectedValue) + "'";
            }
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                StrCondition += " and ItemMaster.SubCategoryId = " + Convert.ToInt32(ddlSubCategory.SelectedValue);
            }
            DS = Obj_StockMaster.GetAssignStockForConsumption(StrCondition, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GridView GridExp = new GridView();
                GridExp.DataSource = DS.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("Material Assign Consumption"+"_"+DateTime.Now.ToString("dd-MMM-yyyy")+".xls", GridExp);
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("No Data Found To Export...!", this.Page);
                DS.Dispose();
                GridDetails.DataSource = null;
                GridDetails.DataBind();
                SetInitialRow();
            }
            DS = null;

        }
        catch (ThreadAbortException tex)
        {

        }
        catch (Exception ex)
        {
            Obj_Comm.ShowPopUpMsg(ex.Message, this.Page);
        }
    }

    //========Important Required for Excel Export Must=============
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    //========Important Required for Excel Export Must=============
    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
            for (int rowIndex = GridDetails.Rows.Count - 2;
                                          rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = GridDetails.Rows[rowIndex];
                GridViewRow gvPreviousRow = GridDetails.Rows[rowIndex + 1];
                for (int cellCount = 0; cellCount < gvRow.Cells.Count;
                                                              cellCount++)
                {
                    if (gvRow.Cells[cellCount].Text ==
                                           gvPreviousRow.Cells[1].Text)
                    {
                        if (gvPreviousRow.Cells[cellCount].RowSpan < 2)
                        {
                            gvRow.Cells[1].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[cellCount].RowSpan =
                                gvPreviousRow.Cells[1].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[1].Visible = false;
                    }
                    if (gvRow.Cells[cellCount].Text ==
                                           gvPreviousRow.Cells[2].Text)
                    {
                        if (gvPreviousRow.Cells[cellCount].RowSpan < 2)
                        {
                            gvRow.Cells[2].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[cellCount].RowSpan =
                                gvPreviousRow.Cells[2].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[2].Visible = false;
                    }
                    
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ReqQty += Convert.ToDecimal(e.Row.Cells[10].Text);
                //IssuQty += Convert.ToDecimal(e.Row.Cells[11].Text);
                //Pending += Convert.ToDecimal(e.Row.Cells[12].Text);
                Amount += Convert.ToDecimal(e.Row.Cells[15].Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[14].Text = "Total :";
                //e.Row.Cells[10].Text = ReqQty.ToString("#0.00");
                //e.Row.Cells[11].Text = IssuQty.ToString("#0.00");
                //e.Row.Cells[12].Text = Pending.ToString("#0.00");
                e.Row.Cells[15].Text = Amount.ToString("#0.00");
            }
    }

    protected void GridDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.GridDetails.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;
            if (ChkFromDate.Checked)
            {
                StrCondition += " and OutwardRegister.StockAsOn between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and OutwardRegister.StockAsOn between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlStockNo.SelectedValue) > 0)
            {
                StrCondition += " and OutwardRegister.OutwardId = " + Convert.ToInt32(ddlStockNo.SelectedValue);
            }
            #region[LOCATION]

                if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and OutwardRegisterDtls.LocationId=" + Convert.ToInt32(ddlLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (OutwardRegisterDtls.LocationId = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or  OutwardRegisterDtls.LocationId = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        if (i == ddlLocation.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
           
            #endregion
            if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
            {
                StrCondition += " and OutwardRegisterDtls.ItemId= " + Convert.ToInt32(ddlItems.SelectedValue);
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and ItemCategory.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToString(RdoType.SelectedValue) != "All")
            {
                StrCondition += " and OutwardRegister.Type= '" + Convert.ToString(RdoType.SelectedValue) + "'";
            }
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                StrCondition += " and ItemMaster.SubCategoryId = " + Convert.ToInt32(ddlSubCategory.SelectedValue);
            }
            DS = Obj_StockMaster.GetAssignStockForConsumption(StrCondition, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                ImgBtnPrint.Visible = true;
                ImgBtnExcel.Visible = true;
                // ViewState["IScancel"] = DS.Tables[0];
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();

                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + " Record Found";
                DS = null;

                //ScriptManager.RegisterStartupScript(this,this.GetType(),"starScript","")
            }
            else
            {
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + " Record Found";
                //lblCount.Text = "";
                DS = null;
                SetInitialRow();
                ImgBtnPrint.Visible = false;
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
                        ddlItems.DataSource = DS.Tables[0];
                        ddlItems.DataTextField = "ItemName";
                        ddlItems.DataValueField = "ItemId";
                        ddlItems.DataBind();
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
                        ddlItems.DataSource = DS.Tables[0];
                        ddlItems.DataTextField = "ItemName";
                        ddlItems.DataValueField = "ItemId";
                        ddlItems.DataBind();
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
