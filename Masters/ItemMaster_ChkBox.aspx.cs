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
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;

public partial class Masters_ItemMaster : System.Web.UI.Page
{
    #region[Private Variables]
        DMItemMaster Obj_ItemMaster = new DMItemMaster();
        ItemMaster Entity_ItemMaster = new ItemMaster();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        private string StrCondition = string.Empty;
        private string perCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagAdd, FlagDel, FlagEdit = false;
        private static string StrCond = string.Empty;
        private int i=0;
        int str = 0;
        int ItemId=0,UnitId=0;
        public static int countPage=0;
    #endregion

    #region[UserDefine Function]

    private void MakeEmptyForm()
        {
              AccordionPane1.HeaderContainer.Width =665;
            ViewState["EditID"] = null;
            if (!FlagAdd)
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            BtnDelete.Visible = false;
            TxtItemCode.Focus();
            TxtMfgBarcode.Text = string.Empty;
            TxtSearch.Text = string.Empty;
            TxtItemName.Text = string.Empty;
            ddlCategory.SelectedValue = "0";
            ddlSubCategory.SelectedValue = "0";
            TxtTaxPer.Text = "0";
            TxtDelivryPeriod.Text ="0";
            TxtMinStockLevel.Text = string.Empty;
            TxtReOrdLevel.Text = string.Empty;
            TxtMaxStockLevel.Text = string.Empty;
            TxtOpeningStock.Text = string.Empty;
            TXTNetOpeningStock.Text = string.Empty;
            TxtAsOnDate.Text =System.DateTime.Now.ToString("dd-MMM-yyyy");
            ddlStockLocation.SelectedValue = "0";
            ddlUnit.SelectedValue = "0"; 
            //str = 0;
            //countPage=0;             
            SetInitialRow();
            SetInitialRow_SIZE();
            SetInitialRowUnitConversion();
            GetItemCode();
            ReportGrid(StrCondition);
            ChkKitchenAssign.Checked = false;
            TXTUPDATEVALUE.Text = "1";
            ddlsize.SelectedValue = "0";
            TxtSearchCategory.Text = string.Empty;
            TxtSearchSubCategory.Text = string.Empty;

            #region[ForUnitConversion]
            Tr_hyl_Hide.Visible = false;
            TR_UnitConversion.Visible = false;
            #endregion
        }

    private void MakeControlEmpty()
    {
        ddlSupplier.SelectedValue = "0";
        TxtPurchaseRate.Text = "0.0";
        ddlStockLocation.Enabled = ddlSupplier.Enabled = true;
        txtDescription.Text = string.Empty;
        ddlStockLocation.SelectedValue = "0";
        TxtOpeningStock.Text = "0.00";
        TXTUPDATEVALUE.Text = "1";
        ViewState["UnitGridIndex"] = null;
        ViewState["GridIndex"] = null;
        ViewState["GridIndex1"] = null;
        ViewState["GridDetails"] = null;
        ImgAddGrid.ImageUrl = "~/Images/Icon/Gridadd.png";
        ImgAddGrid.ToolTip = "Add Grid";
        ddlSupplier.Focus();
    }

    private void MakeControlEmptyUnitConversion()
    {
        DDLSUBUNIT.SelectedValue=DDLMAINUNIT.SelectedValue = "0";
        TXTSUBUNITQTY.Text=TXTUNITQTY.Text = "0.0";
        TXTUNITQTY.Focus();
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("#", typeof(Int32));
        dt.Columns.Add("Location", typeof(string));
        dt.Columns.Add("SuplierName", typeof(string));
        dt.Columns.Add("SupplierId", typeof(Int32));
        dt.Columns.Add("PurchaseRate", typeof(decimal));
        dt.Columns.Add("OpeningStock", typeof(decimal));
        dt.Columns.Add("LocationId", typeof(decimal));
        dt.Columns.Add("ItemDesc", typeof(string));
        dr = dt.NewRow();

        dr["#"] = 0;
        dr["Location"]="";
        dr["SuplierName"] = "";
        dr["SupplierId"] = 0;
        dr["PurchaseRate"] = 0.00;
        dr["OpeningStock"] = 0.00;
        dr["LocationId"] = 0.00;
        dr["ItemDesc"] = "";


        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        GridDetails.DataSource = dt;
        GridDetails.DataBind();
    }

    private void SetInitialRowUnitConversion()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("#", typeof(Int32));
        dt.Columns.Add("MainUnitFactor", typeof(decimal));
        dt.Columns.Add("UnitID", typeof(Int32));
        dt.Columns.Add("MainUnit", typeof(string));
        dt.Columns.Add("SubUnitFactor", typeof(decimal));
        dt.Columns.Add("SubUnitID", typeof(Int32));
        dt.Columns.Add("SubUnit", typeof(string));
        dr = dt.NewRow();

        dr["#"] = 0;
        dr["MainUnitFactor"] = 0;
        dr["UnitID"] = 0;
        dr["MainUnit"] = "";
        dr["SubUnitFactor"] = 0;
        dr["SubUnitID"] = 0;
        dr["SubUnit"] = "";
        dt.Rows.Add(dr);

        ViewState["UnitConversionTable"] = dt;
        GridUnitConversion.DataSource = dt;
        GridUnitConversion.DataBind();
    }

    private void SetInitialRow_SIZE()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("#", typeof(Int32));
        dt.Columns.Add("SizeId", typeof(Int32));
        dt.Columns.Add("SizeName", typeof(string));
        dr = dt.NewRow();

        dr["#"] = 0;
        dr["SizeId"] = 0;
        dr["SizeName"] = "";

        dt.Rows.Add(dr);

        ViewState["CurrentTableSize"] = dt;
        Grd_Size.DataSource = dt;
        Grd_Size.DataBind();
    }

    private void FillCombo()
    {
        try
        {
            Ds = Obj_ItemMaster.FillCombo(out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlSupplier.DataSource = Ds.Tables[0];
                    ddlSupplier.DataTextField = "SuplierName";
                    ddlSupplier.DataValueField = "SuplierId";
                    ddlSupplier.DataBind();
                }
                if (Ds.Tables[1].Rows.Count > 0)
                {
                    ddlCategory.DataSource = Ds.Tables[1];
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();
                }
                if (Ds.Tables[2].Rows.Count > 0)
                {
                    ddlSubCategory.DataSource = Ds.Tables[2];
                    ddlSubCategory.DataTextField = "SubCategory";
                    ddlSubCategory.DataValueField = "SubCategoryId";
                    ddlSubCategory.DataBind();
                }
                if (Ds.Tables[3].Rows.Count > 0)
                {
                    ddlStockLocation.DataSource = Ds.Tables[3];
                    ddlStockLocation.DataTextField = "Location";
                    ddlStockLocation.DataValueField = "StockLocationID";
                    ddlStockLocation.DataBind();
                }
                if (Ds.Tables[4].Rows.Count > 0)
                {
                    ddlUnit.DataSource = Ds.Tables[4];
                    ddlUnit.DataTextField = "Unit";
                    ddlUnit.DataValueField = "UnitId";
                    ddlUnit.DataBind();

                    DDLMAINUNIT.DataSource = Ds.Tables[4];
                    DDLMAINUNIT.DataTextField = "Unit";
                    DDLMAINUNIT.DataValueField = "UnitId";
                    DDLMAINUNIT.DataBind();

                    DDLSUBUNIT.DataSource = Ds.Tables[4];
                    DDLSUBUNIT.DataTextField = "Unit";
                    DDLSUBUNIT.DataValueField = "UnitId";
                    DDLSUBUNIT.DataBind();

                    Ds.Tables[4].Rows.RemoveAt(0);
                    CHKUNITCONVERSION.DataSource = Ds.Tables[4];
                    CHKUNITCONVERSION.DataTextField = "Unit";
                    CHKUNITCONVERSION.DataValueField = "UnitId";
                    CHKUNITCONVERSION.DataBind();
                }
                if (Ds.Tables[5].Rows.Count > 0)
                {
                    ddlsize.DataSource = Ds.Tables[5];
                    ddlsize.DataTextField = "SizeName";
                    ddlsize.DataValueField = "SizeId";
                    ddlsize.DataBind();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }

    private void BindUnitConversiondtls(int UnitId,int ItemId)
    { 
        DataSet DsTemp=new DataSet();
        try
        {
            DsTemp = Obj_ItemMaster.GetUnitConversionDtls(UnitId,ItemId, 1, out StrError);
            if (DsTemp.Tables.Count > 0 && DsTemp.Tables[0].Rows.Count > 0)
            {
                TR_UnitConversion.Visible = true;
                Tr_hyl_Hide.Visible = true;
                GrdUnitConversion.DataSource = DsTemp.Tables[0];
                GrdUnitConversion.DataBind();
                for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToBoolean(DsTemp.Tables[0].Rows[i]["chkselect"].ToString()) == true)
                    {
                        CheckBox grdCheckSelect = (CheckBox)GrdUnitConversion.Rows[i].FindControl("ChkSelect");
                        grdCheckSelect.Checked = true;
                    }
                    else
                    {
                        CheckBox grdCheckSelect = (CheckBox)GrdUnitConversion.Rows[i].FindControl("ChkSelect");
                        grdCheckSelect.Checked = false;
                    }

                }
               
                               
            }
            else
            {
                TR_UnitConversion.Visible = false;
                Tr_hyl_Hide.Visible = false;
                GrdUnitConversion.DataSource = null;
                GrdUnitConversion.DataBind();
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void ReportGrid(string RepCondition)
    {
        try
        {
            Ds = Obj_ItemMaster.GetItem(RepCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ViewState["CurrentTable1"] = Ds.Tables[0];
                if (Ds.Tables[0].Rows.Count > 16)
                {
                    rptPages.Visible = true;
                    CallPagging();
                }
                else
                {
                    GrdReport.DataSource = Ds.Tables[0];
                    GrdReport.DataBind();
                    rptPages.Visible = false;
                }
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }
            Obj_ItemMaster = null;
            Ds = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public void ReportGridByCategory(string RepCondition,int i)
    {
        try
        {
            Ds = Obj_ItemMaster.GetItemByCategory(RepCondition, i, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ViewState["CurrentTable1"] = Ds.Tables[0];
                if (Ds.Tables[0].Rows.Count > 16)
                {
                    rptPages.Visible = true;
                    CallPagging();
                }
                else
                {
                    GrdReport.DataSource = Ds.Tables[0];
                    GrdReport.DataBind();
                    rptPages.Visible = false;
                }
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }
            Obj_ItemMaster = null;
            Ds = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public void ReportBySubCategory(string RepCondition)
    {
        try
        {
            Ds = Obj_ItemMaster.GetItemBySubCategory(RepCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ViewState["CurrentTable1"] = Ds.Tables[0];
                if (Ds.Tables[0].Rows.Count > 16)
                {
                    rptPages.Visible = true;
                    CallPagging();
                }
                else
                {
                    GrdReport.DataSource = Ds.Tables[0];
                    GrdReport.DataBind();
                    rptPages.Visible = false;
                }
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }
            Obj_ItemMaster = null;
            Ds = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
   
    private void GetItemCode()
    {
        try
        {
            Ds = Obj_ItemMaster.GetItemcode(out StrError);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                TxtItemCode.Text = Ds.Tables[0].Rows[0]["ItemCode"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private bool ChkDetails()
    {
        bool flag = false;
        try
        {
            //for (int i = 0; i < GrdBagDtls.Rows.Count; i++)
            //{
            if (GridDetails.Rows.Count > 0 && !String.IsNullOrEmpty(GridDetails.Rows[0].Cells[2].Text) && !GridDetails.Rows[0].Cells[2].Text.Equals("&nbsp;"))
            {
                flag = true;
            }
            //}

        }
        catch (Exception ex) { throw new Exception(ex.Message); }

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
                if (!Session["UserRole"].Equals("Administrator"))
                {
                    //Checking Right of users=======

                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Item Master'");
                    if (dtRow.Length > 0)
                    {
                        DataTable dt = dtRow.CopyToDataTable();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        Response.Redirect("~/NotAuthUser.aspx");
                    }
                    //Checking View Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                    {
                        GrdReport.Visible = false;
                    }
                    //Checking Add Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                    {
                        BtnSave.Visible = false;
                        FlagAdd = true;

                    }
                    //Edit /Delete Column Visible ========
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        BtnDelete.Visible = false;
                        BtnUpdate.Visible = false;
                        FlagDel = true;
                        FlagEdit = true;
                    }
                    else
                    {
                        //Checking Delete Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                        {
                            BtnDelete.Visible = false;
                            FlagDel = true;
                        }

                        //Checking Edit Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                        {
                            BtnUpdate.Visible = false;
                            FlagEdit = true;
                        }
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
    //User Right Function===========

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckUserRight();
            FillCombo();
            MakeControlEmpty();
            MakeEmptyForm();
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {


        DMItemMaster Obj_ItemMaster = new DMItemMaster();
        String[] SearchList = Obj_ItemMaster.GetSuggestedRecord(prefixText,"","");
        return SearchList;
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeControlEmpty();
        MakeEmptyForm();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDtls = 0, InsertRow1=0;
        try
        {
            if (ChkDetails() == true)
            {
                Ds = Obj_ItemMaster.ChkDuplicate(TxtItemName.Text.Trim(),Convert.ToInt32(ddlCategory.SelectedValue), out StrError);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    obj_Comman.ShowPopUpMsg("Record is Already Present..", this.Page);
                    TxtItemName.Focus();
                }
                else
                {
                    Entity_ItemMaster.ItemCode = TxtItemCode.Text;
                    Entity_ItemMaster.Barcode = TxtMfgBarcode.Text.Trim();
                    Entity_ItemMaster.ItemName = (!string.IsNullOrEmpty(TxtItemName.Text)) ? Convert.ToString(TxtItemName.Text) : "0";
                    Entity_ItemMaster.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                    Entity_ItemMaster.SubcategoryId = Convert.ToInt32(ddlSubCategory.SelectedValue);
                    Entity_ItemMaster.DeliveryPeriod = (!string.IsNullOrEmpty(TxtDelivryPeriod.Text)) ? Convert.ToInt32(TxtDelivryPeriod.Text) : 0;
                    Entity_ItemMaster.TaxPer = (!string.IsNullOrEmpty(TxtTaxPer.Text)) ? Convert.ToDecimal(TxtTaxPer.Text) : 0;
                    Entity_ItemMaster.MinStockLevel = (!string.IsNullOrEmpty(TxtMinStockLevel.Text)) ? Convert.ToString(TxtMinStockLevel.Text) : "0";
                    Entity_ItemMaster.ReorderLevel = (!string.IsNullOrEmpty(TxtReOrdLevel.Text)) ? Convert.ToString(TxtReOrdLevel.Text) : "0";
                    Entity_ItemMaster.MaxStockLevel = (!string.IsNullOrEmpty(TxtMaxStockLevel.Text)) ? Convert.ToString(TxtMaxStockLevel.Text) : "0";
                    Entity_ItemMaster.OpeningStock = (!string.IsNullOrEmpty(TXTNetOpeningStock.Text)) ? Convert.ToDecimal(TXTNetOpeningStock.Text) : 0;
                    Entity_ItemMaster.AsOn = (!string.IsNullOrEmpty(TxtAsOnDate.Text)) ? Convert.ToDateTime(TxtAsOnDate.Text) : Convert.ToDateTime("1-Jan-1753");
                    Entity_ItemMaster.StockLocationID = 0;// Convert.ToInt32(ddlStockLocation.SelectedValue);
                    Entity_ItemMaster.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
                    Entity_ItemMaster.UserId = Convert.ToInt32(Session["UserId"]);
                    Entity_ItemMaster.LoginDate = DateTime.Now;
                    if (ChkKitchenAssign.Checked)
                        Entity_ItemMaster.IsKitchenAssign = 1;
                    else
                        Entity_ItemMaster.IsKitchenAssign = 0;

                    //InsertRow = Obj_ItemMaster.InsertRecord(ref Entity_ItemMaster, out StrError);
                    InsertRow = 1;
                    if (InsertRow > 0)
                    {
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtInsert = new DataTable();
                            dtInsert = (DataTable)ViewState["CurrentTable"];
                            for (int i = 0; i < dtInsert.Rows.Count; i++)
                            {
                                Entity_ItemMaster.ItemId = InsertRow;
                                Entity_ItemMaster.SupplierId = Convert.ToInt32(dtInsert.Rows[i]["SupplierId"].ToString());
                                Entity_ItemMaster.PurchaseRate = Convert.ToDecimal(dtInsert.Rows[i]["PurchaseRate"].ToString());
                                Entity_ItemMaster.OpeningStock = (!string.IsNullOrEmpty(dtInsert.Rows[i]["OpeningStock"].ToString())) ? Convert.ToDecimal((dtInsert.Rows[i]["OpeningStock"].ToString())) : 0;
                                Entity_ItemMaster.StockLocationID = (!string.IsNullOrEmpty(dtInsert.Rows[i]["LocationId"].ToString())) ? Convert.ToInt32((dtInsert.Rows[i]["LocationId"].ToString())) : 0;

                                Entity_ItemMaster.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
                                Entity_ItemMaster.ItemDesc = (!string.IsNullOrEmpty((dtInsert.Rows[i]["ItemDesc"].ToString()))) ? (dtInsert.Rows[i]["ItemDesc"].ToString()) : "";
                                Entity_ItemMaster.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                                Entity_ItemMaster.OpeningStockDetails = (!string.IsNullOrEmpty(dtInsert.Rows[i]["OpeningStock"].ToString())) ? Convert.ToDecimal((dtInsert.Rows[i]["OpeningStock"].ToString())) : 0;
                                Entity_ItemMaster.LocationIDDetails = (!string.IsNullOrEmpty(dtInsert.Rows[i]["LocationId"].ToString())) ? Convert.ToInt32((dtInsert.Rows[i]["LocationId"].ToString())) : 0;

                                //InsertRowDtls = Obj_ItemMaster.InsertDetailsRecord(ref Entity_ItemMaster, out StrError);
                            }
                        }
                        if (ViewState["CurrentTableSize"] != null)
                        {
                            DataTable dtInsert = new DataTable();
                            dtInsert = (DataTable)ViewState["CurrentTableSize"];
                            for (int i = 0; i < dtInsert.Rows.Count; i++)
                            {
                                Entity_ItemMaster.ItemId = InsertRow;
                                Entity_ItemMaster.SupplierId = Convert.ToInt32(dtInsert.Rows[i]["SizeId"].ToString());
                                //InsertRowDtls = Obj_ItemMaster.InsertDetailsSizeRecord(ref Entity_ItemMaster, out StrError);
                            }
                        }

                        #region[Save Dtls To ItemUnitConversionDtls]
                        for (int i = 0; i < GrdUnitConversion.Rows.Count; i++)
                        {
                            CheckBox GrdSelect = (CheckBox)GrdUnitConversion.Rows[i].FindControl("ChkSelect");
                            if (GrdSelect.Checked == true)
                            {
                                Entity_ItemMaster.ItemId = InsertRow;
                                Entity_ItemMaster.UnitConvId = Convert.ToInt32(((Label)GrdUnitConversion.Rows[i].FindControl("LblEntryId")).Text);
                                Entity_ItemMaster.UnitConvDtlsId = Convert.ToInt32(GrdUnitConversion.Rows[i].Cells[4].Text.ToString());
                                //InsertRow1 = Obj_ItemMaster.InsertUnitConvrsnDtls(ref Entity_ItemMaster, out StrError);
                            }
                        }
                        #endregion


                        #region [SAVE TO UNIT CONVERSION NEW]----
                        if (InsertRow > 0)
                        {
                            if (ViewState["UnitConversionTable"] != null)
                            {
                                DataTable dtunitconversion = (DataTable)ViewState["UnitConversionTable"];
                                DataTable dtMainUnitColumns = dtunitconversion.DefaultView.ToTable(false, "UnitID", "MainUnitFactor", "SubUnitID", "SubUnitFactor");
                                DataTable dtMainUnitColumnsUnitID = dtunitconversion.DefaultView.ToTable(false, "UnitID");
                                DataTable dtMainUnitColumnsSubUnitID = dtunitconversion.DefaultView.ToTable(false, "SubUnitID");
                                DataTable dtUnitConversionDtls = new DataTable();
                                dtUnitConversionDtls.Columns.Add("ItemID");
                                dtUnitConversionDtls.Columns.Add("ItemUnitDtlsID");
                                dtUnitConversionDtls.Columns.Add("MainUnitQty");
                                dtUnitConversionDtls.Columns.Add("MainUnitID");
                                dtUnitConversionDtls.Columns.Add("SubUnitQty");
                                dtUnitConversionDtls.Columns.Add("SubUnitID");
                                for (int mainrow = 0; mainrow < dtMainUnitColumnsUnitID.Rows.Count; mainrow++)
                                {
                                    for (int Subrow = 0; Subrow < dtMainUnitColumnsSubUnitID.Rows.Count; Subrow++)
                                    {
                                        DataRow dtUnitConversionDtlsRow = dtUnitConversionDtls.NewRow();
                                        dtUnitConversionDtlsRow["ItemID"] = InsertRow;
                                        dtUnitConversionDtlsRow["ItemUnitDtlsID"] = InsertRow1;
                                        dtUnitConversionDtlsRow["MainUnitQty"] = dtMainUnitColumns.Rows[mainrow]["MainUnitFactor"];
                                        dtUnitConversionDtlsRow["MainUnitID"] = dtMainUnitColumns.Rows[mainrow]["UnitID"];
                                        dtUnitConversionDtlsRow["SubUnitQty"] = dtMainUnitColumns.Rows[Subrow]["SubUnitFactor"];
                                        dtUnitConversionDtlsRow["SubUnitID"] = dtMainUnitColumns.Rows[Subrow]["SubUnitID"];
                                        dtUnitConversionDtls.Rows.Add(dtUnitConversionDtlsRow);
                                    }
                                }
                            }
                        }
                        #endregion ------------------------------

                        if (InsertRow > 0)
                        {
                            obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                            MakeControlEmpty();
                            MakeEmptyForm();
                            Entity_ItemMaster = null;
                            Obj_ItemMaster = null;
                        }
                    }
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Please Enter Details ..!", this.Page);
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void ImgAddGrid_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                DataRow dtTableRow = null;
                bool DupFlag = false;
                int k = 0;
                if (dtCurrentTable.Rows.Count > 0)
                {
                   // if (!((TxtPurchaseRate.Text.Equals("0.0"))))
                   // {
                        if (dtCurrentTable.Rows.Count == 1 && string.IsNullOrEmpty(dtCurrentTable.Rows[0]["SuplierName"].ToString()))
                        {
                            dtCurrentTable.Rows.RemoveAt(0);
                        }
                        if (ViewState["GridIndex"] != null)
                        {
                            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                            {
                                if ((Convert.ToInt32(dtCurrentTable.Rows[i]["SupplierId"]) == Convert.ToInt32(ddlSupplier.SelectedValue)) && ((dtCurrentTable.Rows[i]["Location"]).ToString() == (ddlStockLocation.SelectedItem.ToString()))&&
                                    dtCurrentTable.Rows[i]["ItemDesc"].Equals(txtDescription.Text.Trim()))
                                {
                                    DupFlag = true;
                                    k = i;
                                }
                            }
                            if (DupFlag == true)
                            {
                                dtCurrentTable.Rows[k]["SuplierName"] = ddlSupplier.SelectedItem;
                                dtCurrentTable.Rows[k]["SupplierId"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                dtCurrentTable.Rows[k]["PurchaseRate"] = TxtPurchaseRate.Text;
                                dtCurrentTable.Rows[k]["Location"] = ddlStockLocation.SelectedItem;
                                dtCurrentTable.Rows[k]["OpeningStock"] = TxtOpeningStock.Text;
                                dtCurrentTable.Rows[k]["LocationId"] = Convert.ToInt32(ddlStockLocation.SelectedValue);
                                dtCurrentTable.Rows[k]["ItemDesc"] =(!string.IsNullOrEmpty(txtDescription.Text))? txtDescription.Text:"";
                                ViewState["CurrentTable"] = dtCurrentTable;
                                GridDetails.DataSource = dtCurrentTable;
                                GridDetails.DataBind();
                                MakeControlEmpty();

                            }

                            else
                            {
                                dtTableRow = dtCurrentTable.NewRow();
                                int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                                dtTableRow["SuplierName"] = ddlSupplier.SelectedItem;
                                dtTableRow["SupplierId"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                dtTableRow["#"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                dtTableRow["PurchaseRate"] = TxtPurchaseRate.Text;
                                dtTableRow["Location"] = ddlStockLocation.SelectedItem;
                                dtTableRow["OpeningStock"] = TxtOpeningStock.Text;
                                dtTableRow["LocationId"] = Convert.ToInt32(ddlStockLocation.SelectedValue);
                                dtTableRow["ItemDesc"] = (!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "";
                                dtCurrentTable.Rows.Add(dtTableRow);
                                ViewState["CurrentTable"] = dtCurrentTable;
                                GridDetails.DataSource = dtCurrentTable;
                                GridDetails.DataBind();
                                MakeControlEmpty();
                            }
                           

                        }
                        else
                            {
                            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                            {
                                if ((Convert.ToInt32(dtCurrentTable.Rows[i]["SupplierId"]) == Convert.ToInt32(ddlSupplier.SelectedValue)) && ((dtCurrentTable.Rows[i]["Location"]).ToString() == (ddlStockLocation.SelectedItem.ToString()))&&
                                     dtCurrentTable.Rows[i]["ItemDesc"].Equals(txtDescription.Text.Trim()))
                                {
                                    DupFlag = true;
                                    k = i;
                                }
                            }
                            if (DupFlag == true)
                            {
                                dtCurrentTable.Rows[k]["SuplierName"] = ddlSupplier.SelectedItem;
                                dtCurrentTable.Rows[k]["SupplierId"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                dtCurrentTable.Rows[k]["PurchaseRate"] = TxtPurchaseRate.Text;
                                dtCurrentTable.Rows[k]["Location"] = ddlStockLocation.SelectedItem;
                                dtCurrentTable.Rows[k]["OpeningStock"] = TxtOpeningStock.Text;
                                dtCurrentTable.Rows[k]["LocationId"] = Convert.ToInt32(ddlStockLocation.SelectedValue);
                                dtCurrentTable.Rows[k]["ItemDesc"] = (!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "";
                                ViewState["CurrentTable"] = dtCurrentTable;
                                GridDetails.DataSource = dtCurrentTable;
                                GridDetails.DataBind();
                                MakeControlEmpty();

                            }

                            else
                            {
                                dtTableRow = dtCurrentTable.NewRow();
                                int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                                dtTableRow["SuplierName"] = ddlSupplier.SelectedItem;
                                dtTableRow["SupplierId"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                dtTableRow["#"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                dtTableRow["PurchaseRate"] = TxtPurchaseRate.Text;
                                dtTableRow["Location"] = ddlStockLocation.SelectedItem;
                                dtTableRow["OpeningStock"] = TxtOpeningStock.Text;
                                dtTableRow["LocationId"] = Convert.ToInt32(ddlStockLocation.SelectedValue);
                                dtTableRow["ItemDesc"] = (!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "";
                                dtCurrentTable.Rows.Add(dtTableRow);

                                ViewState["CurrentTable"] = dtCurrentTable;
                                GridDetails.DataSource = dtCurrentTable;
                                GridDetails.DataBind();
                                MakeControlEmpty();
                            }
                           
                        }

                    
                    //else
                    //{
                    //   obj_Comman.ShowPopUpMsg("Purchase Rate Should Be Greater Than Zero",this.Page);
                    //}
                }
            }
        }
        catch(Exception ex)  { throw new Exception(ex.Message); }
    }

    protected void IMGBTNADDUNIT_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hiddenbox1.Value == "1")
            {
                if (ViewState["UnitConversionTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["UnitConversionTable"];
                    DataRow dtTableRow = null;
                    bool DupFlag = false;
                    int k = 0;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        if (dtCurrentTable.Rows.Count == 1 && string.IsNullOrEmpty(dtCurrentTable.Rows[0]["MainUnit"].ToString()))
                        {
                            dtCurrentTable.Rows.RemoveAt(0);
                        }
                        if (ViewState["UnitGridIndex"] != null)
                        {
                            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                            {
                                if ((Convert.ToInt32(dtCurrentTable.Rows[i]["UnitID"]) == Convert.ToInt32(DDLMAINUNIT.SelectedValue)) && ((dtCurrentTable.Rows[i]["SubUnitID"]).ToString() == (DDLSUBUNIT.SelectedValue.ToString())))
                                {
                                    DupFlag = true;
                                    k = i;
                                }
                            }
                            if (DupFlag == true)
                            {
                                dtCurrentTable.Rows[k]["#"] = 0;
                                dtCurrentTable.Rows[k]["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 0;
                                dtCurrentTable.Rows[k]["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
                                dtCurrentTable.Rows[k]["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
                                dtCurrentTable.Rows[k]["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 0;
                                dtCurrentTable.Rows[k]["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
                                dtCurrentTable.Rows[k]["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
                                ViewState["UnitConversionTable"] = dtCurrentTable;
                                GridUnitConversion.DataSource = dtCurrentTable;
                                GridUnitConversion.DataBind();
                                MakeControlEmptyUnitConversion();
                            }

                            else
                            {
                                dtTableRow = dtCurrentTable.NewRow();
                                int rowindex = Convert.ToInt32(ViewState["UnitGridIndex"]);
                                dtTableRow["#"] = 0;
                                dtTableRow["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 0;
                                dtTableRow["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
                                dtTableRow["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
                                dtTableRow["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 0;
                                dtTableRow["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
                                dtTableRow["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
                                dtCurrentTable.Rows.Add(dtTableRow);
                                ViewState["UnitConversionTable"] = dtCurrentTable;
                                GridUnitConversion.DataSource = dtCurrentTable;
                                GridUnitConversion.DataBind();
                                MakeControlEmptyUnitConversion();
                            }


                        }
                        else
                        {
                            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                            {
                                if ((Convert.ToInt32(dtCurrentTable.Rows[i]["UnitID"]) == Convert.ToInt32(DDLMAINUNIT.SelectedValue)) && ((dtCurrentTable.Rows[i]["SubUnitID"]).ToString() == (DDLSUBUNIT.SelectedValue.ToString())))
                                {
                                    DupFlag = true;
                                    k = i;
                                }
                            }
                            if (DupFlag == true)
                            {
                                dtCurrentTable.Rows[k]["#"] = 0;
                                dtCurrentTable.Rows[k]["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 0;
                                dtCurrentTable.Rows[k]["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
                                dtCurrentTable.Rows[k]["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
                                dtCurrentTable.Rows[k]["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 0;
                                dtCurrentTable.Rows[k]["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
                                dtCurrentTable.Rows[k]["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
                                ViewState["UnitConversionTable"] = dtCurrentTable;
                                GridUnitConversion.DataSource = dtCurrentTable;
                                GridUnitConversion.DataBind();
                                MakeControlEmptyUnitConversion();
                            }

                            else
                            {
                                dtTableRow = dtCurrentTable.NewRow();
                                int rowindex = Convert.ToInt32(ViewState["UnitGridIndex"]);
                                dtTableRow["#"] = 0;
                                dtTableRow["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 0;
                                dtTableRow["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
                                dtTableRow["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
                                dtTableRow["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 0;
                                dtTableRow["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
                                dtTableRow["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
                                dtCurrentTable.Rows.Add(dtTableRow);
                                ViewState["UnitConversionTable"] = dtCurrentTable;
                                GridUnitConversion.DataSource = dtCurrentTable;
                                GridUnitConversion.DataBind();
                                MakeControlEmptyUnitConversion();
                            }

                        }


                        //else
                        //{
                        //   obj_Comman.ShowPopUpMsg("Purchase Rate Should Be Greater Than Zero",this.Page);
                        //}
                    }
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, UpdateRowDtls = 0, UpdateRow1=0;
        try
        {
           
                    if (ViewState["EditID"] != null)
                    {
                        Entity_ItemMaster.ItemId = Convert.ToInt32(ViewState["EditID"]);
                    }
                    Entity_ItemMaster.ItemCode = TxtItemCode.Text;
                    Entity_ItemMaster.Barcode = (!string.IsNullOrEmpty(TxtMfgBarcode.Text)) ? Convert.ToString(TxtMfgBarcode.Text) : "0";
                    Entity_ItemMaster.ItemName = (!string.IsNullOrEmpty(TxtItemName.Text)) ? Convert.ToString(TxtItemName.Text) : "0";
                    Entity_ItemMaster.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                    Entity_ItemMaster.SubcategoryId = Convert.ToInt32(ddlSubCategory.SelectedValue);
                    Entity_ItemMaster.DeliveryPeriod = (!string.IsNullOrEmpty(TxtDelivryPeriod.Text)) ? Convert.ToInt32(TxtDelivryPeriod.Text) : 0;
                    Entity_ItemMaster.TaxPer = (!string.IsNullOrEmpty(TxtTaxPer.Text)) ? Convert.ToDecimal(TxtTaxPer.Text) : 0;
                    Entity_ItemMaster.MinStockLevel = (!string.IsNullOrEmpty(TxtMinStockLevel.Text)) ? Convert.ToString(TxtMinStockLevel.Text) : "0";
                    Entity_ItemMaster.ReorderLevel = (!string.IsNullOrEmpty(TxtReOrdLevel.Text)) ? Convert.ToString(TxtReOrdLevel.Text) : "0";
                    Entity_ItemMaster.MaxStockLevel = (!string.IsNullOrEmpty(TxtMaxStockLevel.Text)) ? Convert.ToString(TxtMaxStockLevel.Text) : "0";
                    Entity_ItemMaster.OpeningStock = (!string.IsNullOrEmpty(TxtOpeningStock.Text)) ? Convert.ToDecimal(TxtOpeningStock.Text) : 0;
                    Entity_ItemMaster.AsOn =(!string.IsNullOrEmpty(TxtAsOnDate.Text))?Convert.ToDateTime(TxtAsOnDate.Text):Convert.ToDateTime("1-Jan-1753");
                    Entity_ItemMaster.StockLocationID = 0;// Convert.ToInt32(ddlStockLocation.SelectedValue);
                    Entity_ItemMaster.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);

                    Entity_ItemMaster.UserId = Convert.ToInt32(Session["UserId"]);
                    Entity_ItemMaster.LoginDate = DateTime.Now;
                    if (ChkKitchenAssign.Checked)
                        Entity_ItemMaster.IsKitchenAssign = 1;
                    else
                        Entity_ItemMaster.IsKitchenAssign = 0;
                    UpdateRow = Obj_ItemMaster.UpdateRecord(ref Entity_ItemMaster, out StrError);

                    if (UpdateRow > 0)
                    {
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtInsert = new DataTable();
                            dtInsert = (DataTable)ViewState["CurrentTable"];
                            for (int i = 0; i < dtInsert.Rows.Count; i++)
                            {
                                Entity_ItemMaster.ItemId = Convert.ToInt32(ViewState["EditID"]); 
                                Entity_ItemMaster.SupplierId = Convert.ToInt32(dtInsert.Rows[i]["SupplierId"].ToString());
                                Entity_ItemMaster.PurchaseRate = Convert.ToDecimal(dtInsert.Rows[i]["PurchaseRate"].ToString());
                                Entity_ItemMaster.OpeningStock = (!string.IsNullOrEmpty(dtInsert.Rows[i]["OpeningStock"].ToString())) ? Convert.ToDecimal((dtInsert.Rows[i]["OpeningStock"].ToString())) : 0;
                                Entity_ItemMaster.StockLocationID = (!string.IsNullOrEmpty(dtInsert.Rows[i]["LocationId"].ToString())) ? Convert.ToInt32((dtInsert.Rows[i]["LocationId"].ToString())) : 0;
                                Entity_ItemMaster.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
                                Entity_ItemMaster.ItemDesc = (!string.IsNullOrEmpty((dtInsert.Rows[i]["ItemDesc"].ToString()))) ? (dtInsert.Rows[i]["ItemDesc"].ToString()) : "";
                                Entity_ItemMaster.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                Entity_ItemMaster.OpeningStockDetails = (!string.IsNullOrEmpty(dtInsert.Rows[i]["OpeningStock"].ToString())) ? Convert.ToDecimal((dtInsert.Rows[i]["OpeningStock"].ToString())) : 0;
                                Entity_ItemMaster.LocationIDDetails = (!string.IsNullOrEmpty(dtInsert.Rows[i]["LocationId"].ToString())) ? Convert.ToInt32((dtInsert.Rows[i]["LocationId"].ToString())) : 0;

                                UpdateRowDtls = Obj_ItemMaster.InsertDetailsRecord(ref Entity_ItemMaster, out StrError);
                            }
                        }

                        if (ViewState["CurrentTableSize"] != null)
                        {
                            DataTable dtInsert = new DataTable();
                            dtInsert = (DataTable)ViewState["CurrentTableSize"];
                            for (int i = 0; i < dtInsert.Rows.Count; i++)
                            {
                                Entity_ItemMaster.ItemId = Convert.ToInt32(ViewState["EditID"]);
                                Entity_ItemMaster.SupplierId = Convert.ToInt32(dtInsert.Rows[i]["SizeId"].ToString());
                                UpdateRowDtls = Obj_ItemMaster.InsertDetailsSizeRecord(ref Entity_ItemMaster, out StrError);
                            }
                        }

                        #region[Update Dtls To ItemUnitConversionDtls]
                        for (int i = 0; i < GrdUnitConversion.Rows.Count; i++)
                        {
                            CheckBox GrdSelect = (CheckBox)GrdUnitConversion.Rows[i].FindControl("ChkSelect");
                            if (GrdSelect.Checked == true)
                            {
                                Entity_ItemMaster.ItemId = Convert.ToInt32(ViewState["EditID"]);
                                Entity_ItemMaster.UnitConvId = Convert.ToInt32(((Label)GrdUnitConversion.Rows[i].FindControl("LblEntryId")).Text);
                                Entity_ItemMaster.UnitConvDtlsId = Convert.ToInt32(GrdUnitConversion.Rows[i].Cells[4].Text.ToString());
                                UpdateRow1 = Obj_ItemMaster.InsertUnitConvrsnDtls(ref Entity_ItemMaster, out StrError);
                            }
                        }
                        #endregion

                        if (UpdateRow > 0)
                        {
                            obj_Comman.ShowPopUpMsg("Record Updated Successfully", this.Page);
                            MakeControlEmpty();
                            MakeEmptyForm();
                            Entity_ItemMaster = null;
                            Obj_ItemMaster = null;
                        }
                    }
                }
            
      
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void GrdReport_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        
                        if (Convert.ToInt32(e.CommandArgument) != 0)
                        {
                            ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                            Ds = Obj_ItemMaster.GetItemForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                            {
                                TxtItemCode.Text = Ds.Tables[0].Rows[0]["ItemCode"].ToString();
                                TxtMfgBarcode.Text = Ds.Tables[0].Rows[0]["Barcode"].ToString();
                                TxtItemName.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                                ddlCategory.SelectedValue =Convert.ToInt32(Ds.Tables[0].Rows[0]["CategoryId"]).ToString();
                                ddlSubCategory.SelectedValue = Ds.Tables[0].Rows[0]["SubCategoryId"].ToString();
                                TxtTaxPer.Text = Ds.Tables[0].Rows[0]["TaxPer"].ToString();
                                TxtMinStockLevel.Text = Ds.Tables[0].Rows[0]["MinStockLevel"].ToString();
                                TxtReOrdLevel.Text = Ds.Tables[0].Rows[0]["ReorderLevel"].ToString();
                                TxtMaxStockLevel.Text = Ds.Tables[0].Rows[0]["MaxStockLevel"].ToString();
                                TXTNetOpeningStock.Text = Ds.Tables[0].Rows[0]["OpeningStock"].ToString();
                                TxtDelivryPeriod.Text = Ds.Tables[0].Rows[0]["DeliveryPeriod"].ToString();
                                if (string.IsNullOrEmpty(Ds.Tables[0].Rows[0]["AsOn"].ToString()))
                                {
                                    TxtAsOnDate.Text = string.Empty;
                                }
                                else
                                {
                                    TxtAsOnDate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["AsOn"].ToString()).ToString("dd-MMM-yyyy");
                                }
                                ddlStockLocation.SelectedValue = Ds.Tables[0].Rows[0]["StockLocationID"].ToString();
                               
                                if(Convert.ToInt32(Ds.Tables[0].Rows[0]["IsKitchenAssign"].ToString()) == 1)
                                    ChkKitchenAssign.Checked = true;
                                else
                                    ChkKitchenAssign.Checked = false;
                                ddlUnit.SelectedValue = Ds.Tables[0].Rows[0]["UnitId"].ToString();
                                ItemId = Convert.ToInt32(ViewState["EditID"]);
                                UnitId=Convert.ToInt32(Ds.Tables[0].Rows[0]["UnitId"].ToString());
                                BindUnitConversiondtls(UnitId,ItemId);

                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            if (Ds.Tables[1].Rows.Count > 0)
                            {
                                ViewState["CurrentTable"] = Ds.Tables[1];                               
                               GridDetails.DataSource = Ds.Tables[1];
                               GridDetails.DataBind();
                                
                            }
                            else
                            {
                                SetInitialRow();
                            }

                            if (Ds.Tables[2].Rows.Count > 0)
                            {
                                ViewState["CurrentTableSize"] = Ds.Tables[2];
                                Grd_Size.DataSource = Ds.Tables[2];
                                Grd_Size.DataBind();

                            }
                            else
                            {
                                SetInitialRow_SIZE();
                            }


                            Ds = null;
                            Obj_ItemMaster = null;
                            if (!FlagEdit)
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            if (!FlagDel)
                            BtnDelete.Visible = true;
                            TxtItemName.Focus();
                            MakeControlEmpty();
                        }
                        break;
                    }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Index;
            if (e.CommandName == "SelectGrid")
            {
                if ((!(string.IsNullOrEmpty(GridDetails.Rows[0].Cells[3].Text))) && (GridDetails.Rows[0].Cells[3].Text.Equals("&nbsp;")))
                {
                    obj_Comman.ShowPopUpMsg("There Is No Record To Edit", this.Page);
                }
                else
                {
                    ImgAddGrid.ImageUrl = "~/Images/Icon/GridUpdate.png";
                    ImgAddGrid.ToolTip = "Update";
                    TXTUPDATEVALUE.Text = "0";
                    Index = Convert.ToInt32(e.CommandArgument);

                    ViewState["GridIndex"] = Index;
                    ddlSupplier.SelectedValue = GridDetails.Rows[Index].Cells[5].Text;
                    TxtPurchaseRate.Text = ((TextBox)GridDetails.Rows[Index].FindControl("GrdtxtPurchaseRate")).Text;
                    ddlStockLocation.SelectedValue = GridDetails.Rows[Index].Cells[2].Text;
                    TxtOpeningStock.Text = ((TextBox)GridDetails.Rows[Index].FindControl("GrdtxtOpeningStock")).Text;
                    if (!GridDetails.Rows[Index].Cells[9].Text.Equals("&nbsp;"))
                    {
                        txtDescription.Text = GridDetails.Rows[Index].Cells[9].Text;
                    }
                    else
                    {
                        txtDescription.Text = "";
                    }
                    ddlStockLocation.Enabled = ddlSupplier.Enabled = false;
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

     protected void GridDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //int i =Convert.ToInt32(hiddenbox.Value);
            //if (i == 1)
            //{
                if (ViewState["CurrentTable"] != null)
                {
                    int id = e.RowIndex;
                    DataTable dt = (DataTable)ViewState["CurrentTable"];

                    dt.Rows.RemoveAt(id);
                    if (dt.Rows.Count > 0)
                    {
                        GridDetails.DataSource = dt;
                        ViewState["CurrentTable"] = dt;
                        GridDetails.DataBind();
                    }
                    else
                    {
                        SetInitialRow();
                    }
                    MakeControlEmpty();
                }
        //    }
        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }


     protected void GridUnitConversion_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         try
         {
             if (ViewState["UnitConversionTable"] != null)
             {
                 int id = e.RowIndex;
                 DataTable dt = (DataTable)ViewState["UnitConversionTable"];
                 dt.Rows.RemoveAt(id);
                 if (dt.Rows.Count > 0)
                 {
                     GridUnitConversion.DataSource = dt;
                     ViewState["UnitConversionTable"] = dt;
                     GridUnitConversion.DataBind();
                 }
                 else
                 {
                     SetInitialRowUnitConversion();
                 }
                 MakeControlEmptyUnitConversion();
             }
         }

         catch (Exception ex) { throw new Exception(ex.Message); }
     }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int DeleteId = 0;
            if (ViewState["EditID"] != null)
            {
                DeleteId = Convert.ToInt32(ViewState["EditID"]);
            }
            if (DeleteId != 0)
            {
                Entity_ItemMaster.ItemId = DeleteId;
                Entity_ItemMaster.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_ItemMaster.LoginDate = DateTime.Now;
                Entity_ItemMaster.IsDeleted = true;

                int iDelete = Obj_ItemMaster.DeleteRecord(ref Entity_ItemMaster, out StrError);
                if (iDelete > 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("Item is used in Transaction, Please delete references from Transaction to perform delete operation...!", this.Page);
                }
            }
            Entity_ItemMaster = null;
            Obj_ItemMaster = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    #region [For Repeater Pagging]-----------------------------------------
    public int PageNumber
    {
        get
        {
            if (ViewState["PageNumber"] != null)
                return Convert.ToInt32(ViewState["PageNumber"]);
            else
                return 0;
        }
        set
        {
            ViewState["PageNumber"] = value;
        }
    }
    #endregion [For Repeater Pagging]-----------------------------------------
    protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case ("Page"):
                {

                    #region [Code For Pagging in Repeater]--------------------------------
                    PageNumber = Convert.ToInt32(e.CommandArgument) - 1;// Add For Pdgging by Piyush
                    PagedDataSource pgitems = new PagedDataSource();
                    DataTable dt = new DataTable();
                    dt = (DataTable)ViewState["CurrentTable1"];
                    System.Data.DataView dv = new System.Data.DataView(dt);
                    pgitems.DataSource = dv;
                    pgitems.AllowPaging = true;
                    pgitems.PageSize = 16;
                    pgitems.CurrentPageIndex = PageNumber;
                    if (pgitems.PageCount > 1)
                    {
                        rptPages.Visible = true;
                        System.Collections.ArrayList pages = new System.Collections.ArrayList();
                        for (int i = 0; i < pgitems.PageCount; i++)
                            pages.Add((i + 1).ToString());
                        //Extra Code Here                       
                        System.Collections.ArrayList pages1 = new System.Collections.ArrayList();
                        for (int i = 0; i < 5; i++)
                            pages1.Add((i + 1).ToString());
                        //End Here
                        //rptPages.DataSource = pages1;
                        //rptPages.DataBind();
                        
                    }
                    else
                        GridDetails.Visible = false;
                    GrdReport.DataSource = pgitems;
                    GrdReport.DataBind();

                    break;
                    #endregion [Code For Pagging in Repeater]--------------------------------
                }
               
            case("Next"):
                    {
                        if (e.Item.ItemType == ListItemType.Footer)
                        {
                            ((LinkButton)rptPages.Controls[0].FindControl("btnPrev")).Visible = true;
                           
                            LinkButton pos4 = ((LinkButton)rptPages.Controls[rptPages.Controls.Count - 2].FindControl("btnPage"));
                            if (str == countPage)
                            {
                                ((LinkButton)rptPages.Controls[rptPages.Controls.Count - 1].FindControl("btnNext")).Visible = false;
                            }
                            if (str != Convert.ToInt32(pos4.CommandArgument))
                            {
                                str = Convert.ToInt32(pos4.CommandArgument);
                                System.Collections.ArrayList pages1 = new System.Collections.ArrayList();
                                for (int i = 0; i < 5; i++)
                                {
                                    if (str < countPage)
                                    {
                                        pages1.Add((str + 1).ToString());
                                        str++;
                                    }
                                }
                                rptPages.DataSource = pages1;
                                rptPages.DataBind();

                            }
                        }
                        break; 
                    }
            case ("Prev"):
                    {
                        if (e.Item.ItemType == ListItemType.Header)
                        {
                            LinkButton pos4 = ((LinkButton)rptPages.Controls[rptPages.Controls.Count - 2].FindControl("btnPage"));

                            if ((Convert.ToInt32(pos4.CommandArgument) - 4) == 0 || Convert.ToInt32(pos4.CommandArgument)==5)
                            {
                                ((LinkButton)rptPages.Controls[0].FindControl("btnPrev")).Visible = false;
                            }
                            if (str != Convert.ToInt32(pos4.CommandArgument) && Convert.ToInt32(pos4.CommandArgument)>5)
                            {
                                str = Convert.ToInt32(pos4.CommandArgument);
                                str = str -4;
                                System.Collections.ArrayList pages1 = new System.Collections.ArrayList();
                                for (int i = 0; i < 5; i++)
                                {
                                    if (str < countPage)
                                    {
                                        if (str > 1)
                                        {
                                            pages1.Add((str - 1).ToString());
                                            str--;
                                        }    
                                    }
                                }
                                str = str + 4;
                                pages1.Reverse(0, pages1.Count);
                                rptPages.DataSource = pages1;
                                rptPages.DataBind();
                            }
                        }
                        break;
                    }
        }
    }
    public void CallPagging()
    {
        #region [Code For Pagging in Repeater]--------------------------------
        PageNumber = Convert.ToInt32(1);// Add For Pdgging by Piyush
        PagedDataSource pgitems = new PagedDataSource();
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["CurrentTable1"];
        System.Data.DataView dv = new System.Data.DataView(dt);
        pgitems.DataSource = dv;
        pgitems.AllowPaging = true;
        pgitems.PageSize = 16;
        pgitems.CurrentPageIndex = PageNumber;
        if (pgitems.PageCount > 1)
        {
            rptPages.Visible = true;
            System.Collections.ArrayList pages = new System.Collections.ArrayList();
            for (int i = 0; i < pgitems.PageCount; i++)
                pages.Add((i + 1).ToString());
            //Extra Code Here
            countPage = pgitems.PageCount;
            System.Collections.ArrayList pages1 = new System.Collections.ArrayList();
            for (int i = 0; i < 5; i++)
                pages1.Add((i + 1).ToString());
            //End Here
            rptPages.DataSource = pages1;
            rptPages.DataBind();
        }
        else
        {
            //GridDetails.Visible = false;
        }
        GrdReport.DataSource = pgitems;
        GrdReport.DataBind();
        
        ((LinkButton)rptPages.Controls[0].FindControl("btnPrev")).Visible = false;
        if ((pgitems.PageCount) <= 5)
        {
            ((LinkButton)rptPages.Controls[rptPages.Controls.Count - 1].FindControl("btnNext")).Visible = false;
        }
        #endregion [Code For Pagging in Repeater]--------------------------------
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        rptPages.ItemCommand +=
        new RepeaterCommandEventHandler(rptPages_ItemCommand);
    }

    protected void ImgAddGridSize_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            if (Convert.ToInt32(ddlsize.SelectedValue) > 0)
            {
                if (ViewState["CurrentTableSize"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableSize"];

                    DataRow dtTableRow = null;
                    bool DupFlag = false;
                    int k = 0;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        if (dtCurrentTable.Rows.Count == 1 && string.IsNullOrEmpty(dtCurrentTable.Rows[0]["SizeName"].ToString()))
                        {
                            dtCurrentTable.Rows.RemoveAt(0);
                        }
                        if (ViewState["GridIndex1"] != null)
                        {
                            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                            {
                                if ((Convert.ToInt32(dtCurrentTable.Rows[i]["SizeId"]) == Convert.ToInt32(ddlsize.SelectedValue)) && ((dtCurrentTable.Rows[i]["SizeName"]).ToString() == (ddlsize.SelectedItem.ToString())))
                                {
                                    DupFlag = true;
                                    k = i;
                                }
                            }
                            if (DupFlag == true)
                            {
                                dtCurrentTable.Rows[k]["SizeName"] = ddlsize.SelectedItem;
                                dtCurrentTable.Rows[k]["SizeId"] = Convert.ToInt32(ddlsize.SelectedValue);
                                dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(ddlsize.SelectedValue);
                                ViewState["CurrentTableSize"] = dtCurrentTable;
                                Grd_Size.DataSource = dtCurrentTable;
                                Grd_Size.DataBind();
                            }

                            else
                            {
                                int rowindex = Convert.ToInt32(ViewState["GridIndex1"]);
                                dtTableRow["SizeName"] = ddlsize.SelectedItem;
                                dtTableRow["SizeId"] = Convert.ToInt32(ddlsize.SelectedValue);
                                dtTableRow["#"] = Convert.ToInt32(ddlsize.SelectedValue);
                                dtCurrentTable.Rows.Add(dtTableRow);
                                ViewState["CurrentTableSize"] = dtCurrentTable;
                                Grd_Size.DataSource = dtCurrentTable;
                                Grd_Size.DataBind();
                            }


                        }
                        else
                        {
                            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                            {
                                if ((Convert.ToInt32(dtCurrentTable.Rows[i]["SizeId"]) == Convert.ToInt32(ddlsize.SelectedValue)) && ((dtCurrentTable.Rows[i]["SizeName"]).ToString() == (ddlsize.SelectedItem.ToString())))
                                {
                                    DupFlag = true;
                                    k = i;
                                }
                            }
                            if (DupFlag == true)
                            {
                                dtCurrentTable.Rows[k]["SizeName"] = ddlsize.SelectedItem;
                                dtCurrentTable.Rows[k]["SizeId"] = Convert.ToInt32(ddlsize.SelectedValue);
                                dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(ddlsize.SelectedValue);
                                ViewState["CurrentTableSize"] = dtCurrentTable;
                                Grd_Size.DataSource = dtCurrentTable;
                                Grd_Size.DataBind();


                            }

                            else
                            {
                                dtTableRow = dtCurrentTable.NewRow();
                                int rowindex = Convert.ToInt32(ViewState["GridIndex1"]);
                                dtTableRow["SizeName"] = ddlsize.SelectedItem;
                                dtTableRow["SizeId"] = Convert.ToInt32(ddlsize.SelectedValue);
                                dtTableRow["#"] = Convert.ToInt32(ddlsize.SelectedValue);
                                dtCurrentTable.Rows.Add(dtTableRow);
                                ViewState["CurrentTableSize"] = dtCurrentTable;
                                Grd_Size.DataSource = dtCurrentTable;
                                Grd_Size.DataBind();
                            }

                        }
                    }
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Please select Size for Insert Into Grid", this.Page);

            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void Grd_Size_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            if ((!(string.IsNullOrEmpty(Grd_Size.Rows[0].Cells[3].Text))) && (Grd_Size.Rows[0].Cells[3].Text.Equals("&nbsp;")))
            {
                obj_Comman.ShowPopUpMsg("There Is No Record To Delete", this.Page);
            }
            else
            {
                if (ViewState["CurrentTableSize"] != null)
                {
                    int id = e.RowIndex;
                    DataTable dt = (DataTable)ViewState["CurrentTableSize"];

                    dt.Rows.RemoveAt(id);
                    if (dt.Rows.Count > 0)
                    {
                        GridDetails.DataSource = dt;
                        ViewState["CurrentTableSize"] = dt;
                        GridDetails.DataBind();
                    }
                    else
                    {
                        SetInitialRow_SIZE();
                    }
                    MakeControlEmpty();
                }
            }
        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListCategory(string prefixText, int count, string contextKey)
    {
        DMItemMaster Obj_ItemMaster = new DMItemMaster();
        String[] SearchList = Obj_ItemMaster.GetSuggestedRecordCategory(prefixText);
        return SearchList;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListSubCategory(string prefixText, int count, string contextKey)
    {
        DMItemMaster Obj_ItemMaster = new DMItemMaster();
        String[] SearchList = Obj_ItemMaster.GetSuggestedRecordSubCategory(prefixText,"");
        return SearchList;
    }
    protected void TxtSearchCategory_TextChanged(object sender, EventArgs e)
    {
        
        i = PassString();

        ReportGridByCategory(StrCondition,i);
        TxtSearchSubCategory.Focus();
    }

    private int PassString()
    {
        string a, b, c;
        a = TxtSearchCategory.Text;
        b = TxtSearchSubCategory.Text;
        c = TxtSearch.Text;
        if (!(string.IsNullOrEmpty(a)) && !(string.IsNullOrEmpty(b)) && !(string.IsNullOrEmpty(c)))
        {
            StrCondition = a + b + c;
            //perCondition = "ItemCategory.CategoryName+SubCategory.SubCategory+ItemMaster.ItemName";//Dont Remove
            i = 1;
        }
        else if (!(string.IsNullOrEmpty(a)) && (string.IsNullOrEmpty(b)) && !(string.IsNullOrEmpty(c)))
        {
            StrCondition = a + c;
            // perCondition = "ItemCategory.CategoryName+ItemMaster.ItemName";//Dont Remove
            i = 2;
        }
        else if (!(string.IsNullOrEmpty(a)) && !(string.IsNullOrEmpty(b)) && (string.IsNullOrEmpty(c)))
        {
            StrCondition = a + b;
            //perCondition = "ItemCategory.CategoryName+SubCategory.SubCategory";//Dont Remove
            i = 3;
        }
        else if ((string.IsNullOrEmpty(a)) && !(string.IsNullOrEmpty(b)) && !(string.IsNullOrEmpty(c)))
        {
            StrCondition = b + c;
            // perCondition = "SubCategory.SubCategory+ItemMaster.ItemName";//Dont Remove
            i = 4;
        }
        if (!(string.IsNullOrEmpty(a)) && (string.IsNullOrEmpty(b)) && (string.IsNullOrEmpty(c)))
        {
            StrCondition = a;
            //  perCondition = "ItemCategory.CategoryName";//Dont Remove
            i = 5;
        }
        if ((string.IsNullOrEmpty(a)) && !(string.IsNullOrEmpty(b)) && (string.IsNullOrEmpty(c)))
        {
            StrCondition = b;
            // perCondition = "SubCategory.SubCategory";//Dont Remove
            i = 6;
        }
        if ((string.IsNullOrEmpty(a)) && (string.IsNullOrEmpty(b)) && !(string.IsNullOrEmpty(c)))
        {
            StrCondition = c;
           // perCondition = "ItemMaster.ItemName";//Dont Remove
            i = 7;
        }
        if ((string.IsNullOrEmpty(a)) && (string.IsNullOrEmpty(b)) && (string.IsNullOrEmpty(c)))
        {
            StrCondition = "";
            i = 7;
        }
        return i;
    }

    protected void TxtSearchSubCategory_TextChanged(object sender, EventArgs e)
    {
        i = PassString();

        ReportGridByCategory(StrCondition, i);
        TxtSearch.Focus();
    }
    
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        i = PassString();

        ReportGridByCategory(StrCondition,i);
        GrdReport.Focus();
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet DsTemp = new DataSet();
            int UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
            DsTemp = Obj_ItemMaster.GetUnitConversionDtls(UnitId,0,0,out StrError);
            if (DsTemp.Tables.Count > 0 && DsTemp.Tables[0].Rows.Count > 0)
            {
                TR_UnitConversion.Visible = true;
                Tr_hyl_Hide.Visible = true;
                GrdUnitConversion.DataSource = DsTemp.Tables[0];
                GrdUnitConversion.DataBind();
            }
            else
            {
                TR_UnitConversion.Visible = false;
                Tr_hyl_Hide.Visible = false;
                GrdUnitConversion.DataSource = null;
                GrdUnitConversion.DataBind();
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);

        }
    }
    protected void hyl_Hide_Click(object sender, EventArgs e)
    {
        TR_UnitConversion.Visible = false;
    }

    protected void GrdSelectAllHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox GrdSelectAllHeader1 = (CheckBox)sender;
        GridViewRow row = (GridViewRow)GrdSelectAllHeader1.NamingContainer;
        int CurrRow = row.RowIndex;
        if (GrdSelectAllHeader1.Checked == true)
        {
            for (int j = 0; j < GrdUnitConversion.Rows.Count; j++)
            {
                CheckBox GrdSelectAll = (CheckBox)GrdUnitConversion.Rows[j].FindControl("ChkSelect");
                GrdSelectAll.Checked = true;
            }
        }
        else
        {
            for (int j = 0; j < GrdUnitConversion.Rows.Count; j++)
            {
                CheckBox GrdSelectAll = (CheckBox)GrdUnitConversion.Rows[j].FindControl("ChkSelect");
                GrdSelectAll.Checked = false;
            }
        }
    }
    protected void UnitGridCalculate_Click(object sender, EventArgs e)
    {
        
        DataTable dtUnitConversionDtls = new DataTable();
        dtUnitConversionDtls.Columns.Add("#");
        dtUnitConversionDtls.Columns.Add("MainUnitQty");
        dtUnitConversionDtls.Columns.Add("MainUnitID");
        dtUnitConversionDtls.Columns.Add("MainUnit");
        dtUnitConversionDtls.Columns.Add("SubUnitQty");
        dtUnitConversionDtls.Columns.Add("SubUnitID");
        dtUnitConversionDtls.Columns.Add("SubUnit");
        DataTable dtUnitIDS = new DataTable();
        dtUnitIDS.Columns.Add("MainUnit");
        dtUnitIDS.Columns.Add("MainUnitName");
        dtUnitIDS.Columns.Add("SubUnit");
        dtUnitIDS.Columns.Add("SubUnitName");
        for (int chk = 0; chk < CHKUNITCONVERSION.Items.Count; chk++)
        {
            if (CHKUNITCONVERSION.Items[chk].Selected == true)
            {
                DataRow dtUnitIDSRow = dtUnitIDS.NewRow();
                dtUnitIDSRow["MainUnit"] = CHKUNITCONVERSION.Items[chk].Value.ToString();
                dtUnitIDSRow["MainUnitName"] = CHKUNITCONVERSION.Items[chk].Text.ToString();
                dtUnitIDSRow["SubUnit"] = CHKUNITCONVERSION.Items[chk].Value.ToString();
                dtUnitIDSRow["SubUnitName"] = CHKUNITCONVERSION.Items[chk].Text.ToString();
                dtUnitIDS.Rows.Add(dtUnitIDSRow);
            }
        }
        DataTable dtMainUnitColumnsUnitID = dtUnitIDS.DefaultView.ToTable(false, "MainUnit", "MainUnitName");
        DataTable dtMainUnitColumnsSubUnitID = dtUnitIDS.DefaultView.ToTable(false, "SubUnit", "SubUnitName");
        if (dtMainUnitColumnsUnitID.Rows.Count == 1 && dtMainUnitColumnsSubUnitID.Rows.Count==1)
        {
            DataRow dtUnitConversionDtlsRow = dtUnitConversionDtls.NewRow();
            dtUnitConversionDtlsRow["#"] = "0";
            dtUnitConversionDtlsRow["MainUnitQty"] = "1";
            dtUnitConversionDtlsRow["MainUnitID"] = dtUnitIDS.Rows[0]["MainUnit"];
            dtUnitConversionDtlsRow["MainUnit"] = dtUnitIDS.Rows[0]["MainUnitName"];
            dtUnitConversionDtlsRow["SubUnitQty"] = "1";
            dtUnitConversionDtlsRow["SubUnitID"] = dtUnitIDS.Rows[0]["SubUnit"];
            dtUnitConversionDtlsRow["SubUnit"] = dtUnitIDS.Rows[0]["SubUnitName"];
            dtUnitConversionDtls.Rows.Add(dtUnitConversionDtlsRow);
        }
        for (int mainrow = 0; mainrow < dtMainUnitColumnsUnitID.Rows.Count; mainrow++)
        {
            for (int Subrow = 0; Subrow < dtMainUnitColumnsSubUnitID.Rows.Count; Subrow++)
            {
                if (mainrow != Subrow)
                {
                    DataRow dtUnitConversionDtlsRow = dtUnitConversionDtls.NewRow();
                    dtUnitConversionDtlsRow["#"] = "0";
                    dtUnitConversionDtlsRow["MainUnitQty"] = "1";
                    dtUnitConversionDtlsRow["MainUnitID"] = dtUnitIDS.Rows[mainrow]["MainUnit"];
                    dtUnitConversionDtlsRow["MainUnit"] = dtUnitIDS.Rows[mainrow]["MainUnitName"];
                    dtUnitConversionDtlsRow["SubUnitQty"] = "0";
                    dtUnitConversionDtlsRow["SubUnitID"] = dtUnitIDS.Rows[Subrow]["SubUnit"];
                    dtUnitConversionDtlsRow["SubUnit"] = dtUnitIDS.Rows[Subrow]["SubUnitName"];
                    dtUnitConversionDtls.Rows.Add(dtUnitConversionDtlsRow);
                }
            }
        }
        ViewState["DTUnitConversion"] = dtUnitConversionDtls;
        GridUC.DataSource = dtUnitConversionDtls;
        GridUC.DataBind();
    }
}
