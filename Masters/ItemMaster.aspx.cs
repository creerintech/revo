using System;
using System.Net;
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
using System.IO;

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
    private static string SearchCategory = string.Empty;
    private static string SearchSubCategory = string.Empty;
    private int i = 0;
    private int REP = 0;
    int str = 0;
    int ItemId = 0, UnitId = 0;
    public static int countPage = 0;
    string fileName;
    string file;
    #endregion

    #region[UserDefine Function]

    private void GETDATAINGRID()
    {
        try
        {

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow dtTableRow = null;
                bool DupFlag = false;
                bool DescFlag = false;
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
                            if ((Convert.ToInt32(dtCurrentTable.Rows[i]["SupplierId"]) == Convert.ToInt32(ddlSupplier.SelectedValue)) && ((dtCurrentTable.Rows[i]["Location"]).ToString() == (ddlStockLocation.SelectedItem.ToString())))
                            {
                                DupFlag = true;
                                k = Convert.ToInt32(ViewState["GridIndex"]);
                            }
                            if ((Convert.ToInt32(dtCurrentTable.Rows[i]["UnitID"]) == Convert.ToInt32(DDLMAINUNIT.SelectedValue)) && (Convert.ToInt32(dtCurrentTable.Rows[i]["SubUnitID"]) == Convert.ToInt32(DDLSUBUNIT.SelectedValue)) && ((dtCurrentTable.Rows[i]["ItemDesc"]) == ((!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "")))
                            {
                                DescFlag = true;
                            }
                        }
                        if (DescFlag == false)
                        {
                            if (DupFlag == true)
                            {

                                dtCurrentTable.Rows[k]["SuplierName"] = ddlSupplier.SelectedItem;
                                dtCurrentTable.Rows[k]["SupplierId"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(dtCurrentTable.Rows[k]["#"].ToString());
                                dtCurrentTable.Rows[k]["PurchaseRate"] = TxtPurchaseRate.Text;
                                dtCurrentTable.Rows[k]["Location"] = ddlStockLocation.SelectedItem;
                                dtCurrentTable.Rows[k]["OpeningStock"] = TxtOpeningStock.Text;

                                dtCurrentTable.Rows[k]["MinStockSupp"] = txtMinstockSupp.Text;
                                dtCurrentTable.Rows[k]["MaxStockSupp"] = TxtMaxStockSupp.Text;
                                dtCurrentTable.Rows[k]["ReorderSupp"] = txtReorderSupp.Text;

                                dtCurrentTable.Rows[k]["LocationId"] = Convert.ToInt32(ddlStockLocation.SelectedValue);
                                dtCurrentTable.Rows[k]["ItemDesc"] = (!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "";
                                dtCurrentTable.Rows[k]["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 1;
                                dtCurrentTable.Rows[k]["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
                                dtCurrentTable.Rows[k]["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
                                dtCurrentTable.Rows[k]["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 1;
                                dtCurrentTable.Rows[k]["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
                                dtCurrentTable.Rows[k]["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);

                                dtCurrentTable.Rows[k]["DrawingNo"] = txtDrawingNo.Text;
                                if((ViewState["fileName"]!=null))
                                {
                                dtCurrentTable.Rows[k]["DrawingPath"] = "~/ScannedDrawings/" + ViewState["fileName"].ToString();
                                }

                                ViewState["CurrentTable"] = dtCurrentTable;
                                GridDetails.DataSource = dtCurrentTable;
                                GridDetails.DataBind();
                                DataTable dt = (DataTable)GridDetails.DataSource;
                                Session["ItemData"] = dt;
                                MakeControlEmpty();

                            }

                            else
                            {
                                dtTableRow = dtCurrentTable.NewRow();
                                int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                                dtTableRow["SuplierName"] = ddlSupplier.SelectedItem;
                                dtTableRow["SupplierId"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                dtTableRow["#"] = 0;
                                dtTableRow["PurchaseRate"] = TxtPurchaseRate.Text;
                                dtTableRow["Location"] = ddlStockLocation.SelectedItem;
                                dtTableRow["OpeningStock"] = TxtOpeningStock.Text;
                                dtTableRow["MinStockSupp"] = txtMinstockSupp.Text;
                                dtTableRow["MaxStockSupp"] = TxtMaxStockSupp.Text;
                                dtTableRow["ReorderSupp"] = txtReorderSupp.Text;
                                dtTableRow["LocationId"] = Convert.ToInt32(ddlStockLocation.SelectedValue);
                                dtTableRow["ItemDesc"] = (!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "";
                                dtTableRow["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 1;


                                if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                                {
                                    dtTableRow["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
                                }
                                else
                                {
                                    dtTableRow["UnitID"] = Convert.ToInt32(ddlUnit.SelectedValue);
                                }

                                if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                                {
                                    dtTableRow["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
                                }
                                else
                                {
                                    dtTableRow["MainUnit"] = Convert.ToString(ddlUnit.SelectedItem);
                                }


                                if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                                {
                                    dtTableRow["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
                                }
                                else
                                {
                                    dtTableRow["SubUnitID"] = Convert.ToInt32(ddlUnit.SelectedValue);
                                }

                                if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                                {
                                    dtTableRow["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
                                }
                                else
                                {
                                    dtTableRow["SubUnit"] = Convert.ToString(ddlUnit.SelectedItem);
                                }
                                if (Convert.ToInt32(txtDrawingNo.Text) > 0)
                                {
                                    dtTableRow["DrawingNo"] = txtDrawingNo.Text;
                                }
                                if ((ViewState["fileName"] != null))
                                {
                                    dtTableRow["DrawingPath"] = "~/ScannedDrawings/" + ViewState["fileName"].ToString();
                                }
                                dtTableRow["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 1;
                                dtCurrentTable.Rows.Add(dtTableRow);
                                ViewState["CurrentTable"] = dtCurrentTable;
                                GridDetails.DataSource = dtCurrentTable;
                                GridDetails.DataBind();

                                DataTable dt = (DataTable)GridDetails.DataSource;
                                Session["ItemData"] = dt;
                                MakeControlEmpty();
                            }

                        }
                        else
                        {
                            obj_Comman.ShowPopUpMsg("Please Enter Another Description For This Conversion Entry..", this.Page);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            if ((Convert.ToInt32(dtCurrentTable.Rows[i]["SupplierId"]) == Convert.ToInt32(ddlSupplier.SelectedValue)) && ((dtCurrentTable.Rows[i]["Location"]).ToString() == (ddlStockLocation.SelectedItem.ToString())))
                            {
                                DupFlag = true;
                                k = i;
                            }
                            if ((Convert.ToInt32(dtCurrentTable.Rows[i]["UnitID"]) == Convert.ToInt32(DDLMAINUNIT.SelectedValue)) && (Convert.ToInt32(dtCurrentTable.Rows[i]["SubUnitID"]) == Convert.ToInt32(DDLSUBUNIT.SelectedValue)) && ((dtCurrentTable.Rows[i]["ItemDesc"]) == ((!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "")))
                            {
                                DescFlag = true;
                            }
                        }
                        if (DescFlag == false)
                        {
                            if (DupFlag == true)
                            {

                                dtCurrentTable.Rows[k]["SuplierName"] = ddlSupplier.SelectedItem;
                                dtCurrentTable.Rows[k]["SupplierId"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(dtCurrentTable.Rows[k]["#"].ToString());
                                dtCurrentTable.Rows[k]["PurchaseRate"] = TxtPurchaseRate.Text;
                                dtCurrentTable.Rows[k]["Location"] = ddlStockLocation.SelectedItem;
                                dtCurrentTable.Rows[k]["OpeningStock"] = TxtOpeningStock.Text;
                                dtCurrentTable.Rows[k]["MinStockSupp"] = txtMinstockSupp.Text;
                                dtCurrentTable.Rows[k]["MaxStockSupp"] = TxtMaxStockSupp.Text;
                                dtCurrentTable.Rows[k]["ReorderSupp"] = txtReorderSupp.Text;
                                dtCurrentTable.Rows[k]["LocationId"] = Convert.ToInt32(ddlStockLocation.SelectedValue);
                                dtCurrentTable.Rows[k]["ItemDesc"] = (!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "";
                                dtCurrentTable.Rows[k]["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 1;
                                dtCurrentTable.Rows[k]["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
                                dtCurrentTable.Rows[k]["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
                                dtCurrentTable.Rows[k]["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 1;
                                dtCurrentTable.Rows[k]["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
                                dtCurrentTable.Rows[k]["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
                                ViewState["CurrentTable"] = dtCurrentTable;
                                GridDetails.DataSource = dtCurrentTable;
                                GridDetails.DataBind();
                                DataTable dt = (DataTable)GridDetails.DataSource;
                                Session["ItemData"] = dt;
                                MakeControlEmpty();

                            }

                            else
                            {
                                dtTableRow = dtCurrentTable.NewRow();
                                int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                                dtTableRow["SuplierName"] = ddlSupplier.SelectedItem;
                                dtTableRow["SupplierId"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                dtTableRow["#"] = 0;
                                dtTableRow["PurchaseRate"] = TxtPurchaseRate.Text;
                                dtTableRow["Location"] = ddlStockLocation.SelectedItem;
                                dtTableRow["OpeningStock"] = TxtOpeningStock.Text;
                                dtTableRow["MinStockSupp"] = txtMinstockSupp.Text;
                                dtTableRow["MaxStockSupp"] = TxtMaxStockSupp.Text;
                                dtTableRow["ReorderSupp"] = txtReorderSupp.Text;
                                dtTableRow["LocationId"] = Convert.ToInt32(ddlStockLocation.SelectedValue);
                                dtTableRow["ItemDesc"] = (!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "";
                                dtTableRow["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 1;
                                if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                                {
                                    dtTableRow["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
                                }
                                else
                                {
                                    dtTableRow["UnitID"] = Convert.ToInt32(ddlUnit.SelectedValue);
                                }

                                if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                                {
                                    dtTableRow["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
                                }
                                else
                                {
                                    dtTableRow["MainUnit"] = Convert.ToString(ddlUnit.SelectedItem);
                                }


                                if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                                {
                                    dtTableRow["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
                                }
                                else
                                {
                                    dtTableRow["SubUnitID"] = Convert.ToInt32(ddlUnit.SelectedValue);
                                }

                                if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                                {
                                    dtTableRow["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
                                }
                                else
                                {
                                    dtTableRow["SubUnit"] = Convert.ToString(ddlUnit.SelectedItem);
                                }
                                dtTableRow["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 1;
                                dtCurrentTable.Rows.Add(dtTableRow);
                                ViewState["CurrentTable"] = dtCurrentTable;
                                GridDetails.DataSource = dtCurrentTable;
                                GridDetails.DataBind();
                                DataTable dt = (DataTable)GridDetails.DataSource;
                                Session["ItemData"] = dt;
                                MakeControlEmpty();
                            }
                        }
                        else
                        {
                            obj_Comman.ShowPopUpMsg("Please Enter Another Description For This Conversion Entry..", this.Page);
                        }

                    }
                }
            }
            Fill_CalulateGrid(); // Uncomment When Factor Save using Fron End
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void MakeEmptyForm()
    {
        //AccordionPane1.HeaderContainer.Width =665;
        ViewState["EditID"] = null;
        if (!FlagAdd)
            BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnDelete.Visible = false;
        TxtHSNCode.Text = string.Empty;
        TxtMfgBarcode.Text = string.Empty;
        TxtSearch.Text = string.Empty;
        TxtItemName.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ddlCategory.SelectedValue = "0";
        ddlSubCategory.SelectedValue = "0";
        TxtOpeningStock.Text = TxtMaxStockLevel.Text = TxtReOrdLevel.Text = TxtMinStockLevel.Text = TXTNetOpeningStock.Text = TxtTaxPer.Text = "0";
        TxtDelivryPeriod.Text = "0";
        TxtAsOnDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
        ddlStockLocation.SelectedValue = "0";
        DDLMAINUNIT.SelectedValue = ddlUnit.SelectedValue = "0";
        SetInitialRow();
        SetInitialRow_SIZE();
        SetInitialRow_CalUnit();// Add New Code By Anand on Dated 24 Mar 2014(For Unit Conversion) 
        //SetInitialRowUnitConversion();
        GetItemCode();
        ReportGrid(StrCondition);
        ChkKitchenAssign.Checked = false;
        TXTUPDATEVALUE.Text = "1";
        ddlsize.SelectedValue = "0";
        ViewState["ITEMINUSEORNOT"] = null;
        ddlUnit.Enabled = true;
        DDLSUBUNIT.Enabled = true;
        DDLMAINUNIT.Enabled = false;
        chkClub.Checked = false;
        #region[ForUnitConversion]
        Tr_hyl_Hide.Visible = false;
        TR_UnitConversion.Visible = false;
        #endregion
        TxtItemName.Focus();
    }

    private void MakeControlEmpty()
    {
        ddlSupplier.SelectedValue = "0";
        TxtPurchaseRate.Text = "0.0";
        txtMinstockSupp.Text = "0.0";
        TxtMaxStockSupp.Text = "0.0";
        txtReorderSupp.Text = "0.0";
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
        DDLSUBUNIT.SelectedValue = "0";
        //DDLMAINUNIT.SelectedValue = "0";
        TXTSUBUNITQTY.Text = TXTUNITQTY.Text = "1";
        TXTUNITQTY.Focus();
        ddlSupplier.Focus();


    }

    private void MakeControlEmptyUnitConversion()
    {
        DDLSUBUNIT.SelectedValue = DDLMAINUNIT.SelectedValue = "0";
        TXTSUBUNITQTY.Text = TXTUNITQTY.Text = "1";
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
        dt.Columns.Add("MainUnitFactor", typeof(decimal));
        dt.Columns.Add("UnitID", typeof(Int32));
        dt.Columns.Add("MainUnit", typeof(string));
        dt.Columns.Add("SubUnitFactor", typeof(decimal));
        dt.Columns.Add("SubUnitID", typeof(Int32));
        dt.Columns.Add("SubUnit", typeof(string));
        dt.Columns.Add("DrawingNo", typeof(string));
        dt.Columns.Add("DrawingPath", typeof(string));

        dt.Columns.Add("MinStockSupp", typeof(decimal));
        dt.Columns.Add("MaxStockSupp", typeof(decimal));
        dt.Columns.Add("ReorderSupp", typeof(decimal));

        dr = dt.NewRow();

        dr["#"] = 0;
        dr["Location"] = "";
        dr["SuplierName"] = "";
        dr["SupplierId"] = 0;
        dr["PurchaseRate"] = 0.00;
        dr["OpeningStock"] = 0.00;
        dr["LocationId"] = 0.00;
        dr["ItemDesc"] = "";
        dr["MainUnitFactor"] = 0;
        dr["UnitID"] = 0;
        dr["MainUnit"] = "";
        dr["SubUnitFactor"] = 0;
        dr["SubUnitID"] = 0;
        dr["SubUnit"] = "";
        dr["DrawingNo"] = "";
        dr["DrawingPath"] = "";
        dr["MinStockSupp"] = 0.00;
        dr["MaxStockSupp"] = 0.00;
        dr["ReorderSupp"] = 0.00;
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        GridDetails.DataSource = dt;
        GridDetails.DataBind();
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

                }
                if (Ds.Tables[5].Rows.Count > 0)
                {
                    ddlsize.DataSource = Ds.Tables[5];
                    ddlsize.DataTextField = "SizeName";
                    ddlsize.DataValueField = "SizeId";
                    ddlsize.DataBind();
                }
                if (Ds.Tables[6].Rows.Count > 0)
                {
                    DDLTAXTEMPLATE.DataSource = Ds.Tables[6];
                    DDLTAXTEMPLATE.DataTextField = "TaxName";
                    DDLTAXTEMPLATE.DataValueField = "TaxTemplateID";
                    DDLTAXTEMPLATE.DataBind();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }

    private void BindUnitConversiondtls(int UnitId, int ItemId)
    {
        DataSet DsTemp = new DataSet();
        try
        {
            DsTemp = Obj_ItemMaster.GetUnitConversionDtls(UnitId, ItemId, 1, out StrError);
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
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void BindItemUnitDtls(int UnitId, int ItemId)
    {
        //DataSet DsTemp = new DataSet();
        //try
        //{
        //    DsTemp = Obj_ItemMaster.GetItemUnitDtls(UnitId, ItemId, 1, out StrError);
        //    if (DsTemp.Tables.Count > 0 && DsTemp.Tables[0].Rows.Count > 0)
        //    {
        //        ViewState["UnitConversionTable"] = DsTemp.Tables[0];
        //        GridUnitConversion.DataSource = DsTemp.Tables[0];
        //        GridUnitConversion.DataBind();
        //    }
        //    else
        //    {
        //        GridUnitConversion.DataSource = null;
        //        GridUnitConversion.DataBind();
        //        SetInitialRowUnitConversion();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw new Exception(ex.Message);
        //}
    }

    public void ReportGrid(string RepCondition)
    {
        try
        {
            Ds = Obj_ItemMaster.GetItem(RepCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ViewState["CurrentTable1"] = Ds.Tables[0];
                if (Ds.Tables[0].Rows.Count > 1000)
                {
                 //   rptPages.Visible = true;
                   // CallPagging();
                }
                else
                {
                    GrdReport.DataSource = Ds.Tables[0];
                    GrdReport.DataBind();
                //    rptPages.Visible = false;
                }
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }

            TxtSearchCategory_TextChanged(TxtSearchSubCategory, System.EventArgs.Empty);

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public void ReportGridByCategory(string RepCondition, int i)
    {
        try
        {
            Ds = Obj_ItemMaster.GetItemByCategory(RepCondition, i, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ViewState["CurrentTable1"] = Ds.Tables[0];
                if (Ds.Tables[0].Rows.Count > 1000)
                {
                 //   rptPages.Visible = true;
                 //   CallPagging();
                }
                else
                {
                    GrdReport.DataSource = Ds.Tables[0];
                    GrdReport.DataBind();
                    // rptPages.Visible = false;

                    lblcount.Text= Ds.Tables[0].Rows.Count.ToString();
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
                if (Ds.Tables[0].Rows.Count > 1000)
                {
                  //  rptPages.Visible = true;
                  //  CallPagging();
                }
                else
                {
                    GrdReport.DataSource = Ds.Tables[0];
                    GrdReport.DataBind();
                  //  rptPages.Visible = false;
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

    private bool ChkUnitDetails()
    {
        bool flag = false;
        try
        {
            //if (Convert.ToInt32(GridUnitConversion.Rows[0].Cells[4].Text.ToString()) > 0)
            //{
            //    flag = true;
            //}
            if (Convert.ToInt32(ddlUnit.SelectedValue.ToString()) > 0)
            {
                flag = true;
            }
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
                DMUserLogin obj_Login = new DMUserLogin();
                UserLogin Entity_Login = new UserLogin();
                DataSet dsLogin = new DataSet();
                Entity_Login.UserName = Session["UserName"].ToString();
                dsLogin = obj_Login.GetLoginInfoWrites(ref Entity_Login, out StrError);
                if (dsLogin.Tables[1].Rows.Count > 0)
                {
                    Session.Add("DataSetItem", dsLogin);
                }
                //Checking User Role========
                if (!Session["UserRole"].Equals("Administrator"))
                {
                    //Checking Right of users=======

                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSetItem"];

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
                    Session["UPDATE"] = dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString();
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

    //User Right Function===========
    public void CheckUserRightForUnitConversion()
    {
        try
        {
            DataSet Ds = new DataSet();
            Ds = Obj_ItemMaster.GetSpecialPermission(Convert.ToInt32(Session["UserId"]), "Unit Conversion In Item Master", out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    //AccordionPane1.Enabled = true;
                }
                else
                {
                    // AccordionPane1.Enabled = false;
                }
            }
            else
            {
                // AccordionPane1.Enabled = false;
            }
            Ds = null;

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


    //New Code By Anand on Dated 23 Mar 2014(For Unit Conversion)
    private void SetInitialRow_CalUnit()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("#", typeof(Int32));
        dt.Columns.Add("ItemID", typeof(Int32));
        dt.Columns.Add("From_Factor", typeof(Decimal));
        dt.Columns.Add("From_UnitID", typeof(Int32));
        dt.Columns.Add("From_UnitName", typeof(String));
        dt.Columns.Add("To_Factor", typeof(Decimal));
        dt.Columns.Add("To_UnitID", typeof(Int32));
        dt.Columns.Add("To_UnitName", typeof(String));
        dt.Columns.Add("Factor_Desc", typeof(String));


        dr = dt.NewRow();

        dr["#"] = 0;
        dr["ItemID"] = 0;
        dr["From_Factor"] = 0;
        dr["From_UnitID"] = 0;
        dr["From_UnitName"] = "";
        dr["To_Factor"] = 0;
        dr["To_UnitID"] = 0;
        dr["To_UnitName"] = "";
        dr["Factor_Desc"] = "";

        dt.Rows.Add(dr);

        ViewState["CulUnitTable"] = dt;
        GrdUnitCal.DataSource = dt;
        GrdUnitCal.DataBind();
    }

    private void Fill_CalulateGrid()
    {
        try
        {
            DataTable dtCulUnitTable = (DataTable)ViewState["CulUnitTable"];
            DataRow dtTableRow = null;

            dtCulUnitTable.Rows.Clear();

            if (GridDetails.Rows.Count > 0)
            {
                for (int k = 0; k < GridDetails.Rows.Count; k++)
                {                 
                    dtTableRow = dtCulUnitTable.NewRow();

                    dtTableRow["#"] = 0;
                    dtTableRow["ItemID"] = 0;
                    dtTableRow["From_Factor"] = 1;
                    dtTableRow["From_UnitID"] = Convert.ToInt32(GridDetails.Rows[k].Cells[11].Text);
                    dtTableRow["From_UnitName"] = Convert.ToString(GridDetails.Rows[k].Cells[12].Text);
                    dtTableRow["To_Factor"] = Convert.ToDecimal((Convert.ToDecimal(GridDetails.Rows[k].Cells[13].Text) / Convert.ToDecimal(GridDetails.Rows[k].Cells[10].Text)).ToString("0.0000"));
                    dtTableRow["To_UnitID"] = Convert.ToInt32(GridDetails.Rows[k].Cells[14].Text);
                    dtTableRow["To_UnitName"] = Convert.ToString(GridDetails.Rows[k].Cells[15].Text);
                    if (string.IsNullOrEmpty(GridDetails.Rows[k].Cells[9].Text) == true || GridDetails.Rows[k].Cells[9].Text == "&nbsp;")
                    {
                        dtTableRow["Factor_Desc"] = "";
                    }
                    else
                    {
                        dtTableRow["Factor_Desc"] = Server.HtmlDecode(GridDetails.Rows[k].Cells[9].Text);
                    }

                    dtCulUnitTable.Rows.Add(dtTableRow);                  
                    dtTableRow = dtCulUnitTable.NewRow();

                    dtTableRow["#"] = 0;
                    dtTableRow["ItemID"] = 0;
                    dtTableRow["From_Factor"] = 1;
                    dtTableRow["From_UnitID"] = Convert.ToInt32(GridDetails.Rows[k].Cells[14].Text);
                    dtTableRow["From_UnitName"] = Convert.ToString(GridDetails.Rows[k].Cells[15].Text);
                    dtTableRow["To_Factor"] = Convert.ToDecimal((Convert.ToDecimal(GridDetails.Rows[k].Cells[10].Text) / Convert.ToDecimal(GridDetails.Rows[k].Cells[13].Text)).ToString("0.0000"));
                    dtTableRow["To_UnitID"] = Convert.ToInt32(GridDetails.Rows[k].Cells[11].Text);
                    dtTableRow["To_UnitName"] = Convert.ToString(GridDetails.Rows[k].Cells[12].Text);
                    if (string.IsNullOrEmpty(GridDetails.Rows[k].Cells[9].Text) == true || GridDetails.Rows[k].Cells[9].Text == "&nbsp;")
                    {
                        dtTableRow["Factor_Desc"] = "";
                    }
                    else
                    {
                        dtTableRow["Factor_Desc"] = Server.HtmlDecode(GridDetails.Rows[k].Cells[9].Text);
                    }

                    dtCulUnitTable.Rows.Add(dtTableRow);
                }

                //Add New Code For Unit Reverse Code
                decimal NewFactor1 = 0;
                decimal NewFactor2 = 0;
                if (GridDetails.Rows.Count > 1)
                {
                    for (int i = 0; i < GridDetails.Rows.Count; i++)
                    {
                        for (int j = i; j < GridDetails.Rows.Count; j++)
                        {
                            if (Convert.ToInt32(GridDetails.Rows[i].Cells[14].Text) != Convert.ToInt32(GridDetails.Rows[j].Cells[14].Text))
                            {
                                //Save First Calculation Factor (Means : A=How Much B)

                                NewFactor1 = Convert.ToDecimal(GridDetails.Rows[i].Cells[10].Text) / Convert.ToDecimal(GridDetails.Rows[i].Cells[13].Text);
                                NewFactor2 = Convert.ToDecimal(GridDetails.Rows[j].Cells[13].Text) / Convert.ToDecimal(GridDetails.Rows[j].Cells[10].Text);

                                dtTableRow = dtCulUnitTable.NewRow();

                                dtTableRow["#"] = 0;
                                dtTableRow["ItemID"] = 0;
                                dtTableRow["From_Factor"] = 1;
                                dtTableRow["From_UnitID"] = Convert.ToInt32(GridDetails.Rows[i].Cells[14].Text);
                                dtTableRow["From_UnitName"] = Convert.ToString(GridDetails.Rows[i].Cells[15].Text);
                                dtTableRow["To_Factor"] = Convert.ToDecimal((NewFactor1 * NewFactor2).ToString("0.0000"));
                                dtTableRow["To_UnitID"] = Convert.ToInt32(GridDetails.Rows[j].Cells[14].Text);
                                dtTableRow["To_UnitName"] = Convert.ToString(GridDetails.Rows[j].Cells[15].Text);
                                dtTableRow["Factor_Desc"] = "";

                                dtCulUnitTable.Rows.Add(dtTableRow);



                                //Save First Calculation Factor (Means : B=How Much A)

                                NewFactor1 = Convert.ToDecimal(GridDetails.Rows[i].Cells[13].Text) / Convert.ToDecimal(GridDetails.Rows[i].Cells[10].Text);
                                NewFactor2 = Convert.ToDecimal(GridDetails.Rows[j].Cells[10].Text) / Convert.ToDecimal(GridDetails.Rows[j].Cells[13].Text);

                                dtTableRow = dtCulUnitTable.NewRow();

                                dtTableRow["#"] = 0;
                                dtTableRow["ItemID"] = 0;
                                dtTableRow["From_Factor"] = 1;
                                dtTableRow["From_UnitID"] = Convert.ToInt32(GridDetails.Rows[j].Cells[14].Text);
                                dtTableRow["From_UnitName"] = Convert.ToString(GridDetails.Rows[j].Cells[15].Text);
                                dtTableRow["To_Factor"] = Convert.ToDecimal((NewFactor1 * NewFactor2).ToString("0.0000"));
                                dtTableRow["To_UnitID"] = Convert.ToInt32(GridDetails.Rows[i].Cells[14].Text);
                                dtTableRow["To_UnitName"] = Convert.ToString(GridDetails.Rows[i].Cells[15].Text);
                                dtTableRow["Factor_Desc"] = "";

                                dtCulUnitTable.Rows.Add(dtTableRow);
                            }
                        }
                    }
                }


                //ViewState["CulUnitTable"] = dtCurrentTable;
                GrdUnitCal.DataSource = null;
                GrdUnitCal.DataSource = dtCulUnitTable;
                GrdUnitCal.DataBind();
            }
            else
            {
                //ViewState["CulUnitTable"] = dtCurrentTable;
                GrdUnitCal.DataSource = dtCulUnitTable;
                GrdUnitCal.DataBind();
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    //New Code By Anand on Dated 23 Mar 2014(For Unit Conversion)


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckUserRightForUnitConversion();
           // CheckUserRight();
            FillCombo();
            MakeControlEmpty();
            MakeEmptyForm();
            this.Form.Enctype = "multipart/form-data";
            database db = new database();
            lblcount.Text = db.getDb_Value("select count(*) from ItemMaster").ToString();
            //if (BtnSave.Visible == false)
            //{
            //    fUpload.Visible = false;            
            //}
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMItemMaster Obj_ItemMaster = new DMItemMaster();
        SearchCategory = SearchCategory.ToString();
        SearchSubCategory = SearchSubCategory.ToString();
        String[] SearchList = Obj_ItemMaster.GetSuggestedRecord(prefixText, SearchCategory, SearchSubCategory);
        return SearchList;
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeControlEmpty();
        MakeEmptyForm();
        lblFileup.Text = "";
        txtDrawingNo.Text = "";
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDtls = 0, InsertRow1 = 0, InsertUnitCal = 0;
        try
        {

            Fill_CalulateGrid(); // Uncomment When Factor Save using From End

            if (ChkUnitDetails() == true)
            {
                if (ChkDetails() == true)
                {
                    Ds = Obj_ItemMaster.ChkDuplicate(TxtItemName.Text.Trim(), Convert.ToInt32(ddlCategory.SelectedValue), out StrError);
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
                        Entity_ItemMaster.TaxTemplateID = Convert.ToInt32(DDLTAXTEMPLATE.SelectedValue); 
                        Entity_ItemMaster.TaxDtlsId = Convert.ToInt32(ddlGSTPer.SelectedValue);
                        Entity_ItemMaster.MinStockLevel = (!string.IsNullOrEmpty(TxtMinStockLevel.Text)) ? Convert.ToString(TxtMinStockLevel.Text) : "0";
                        Entity_ItemMaster.ReorderLevel = (!string.IsNullOrEmpty(TxtReOrdLevel.Text)) ? Convert.ToString(TxtReOrdLevel.Text) : "0";
                        Entity_ItemMaster.MaxStockLevel = (!string.IsNullOrEmpty(TxtMaxStockLevel.Text)) ? Convert.ToString(TxtMaxStockLevel.Text) : "0";
                        Entity_ItemMaster.OpeningStock = (!string.IsNullOrEmpty(TXTNetOpeningStock.Text)) ? Convert.ToDecimal(TXTNetOpeningStock.Text) : 0;
                        Entity_ItemMaster.AsOn = (!string.IsNullOrEmpty(TxtAsOnDate.Text)) ? Convert.ToDateTime(TxtAsOnDate.Text) : Convert.ToDateTime("1-Jan-1753");
                        Entity_ItemMaster.StockLocationID = 0;// Convert.ToInt32(ddlStockLocation.SelectedValue);
                        Entity_ItemMaster.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
                        Entity_ItemMaster.UserId = Convert.ToInt32(Session["UserId"]);
                        Entity_ItemMaster.LoginDate = DateTime.Now;
                        Entity_ItemMaster.Remark = txtRemark.Text.Trim();
                        Entity_ItemMaster.HSNCode = TxtHSNCode.Text.Trim();
                        if (ChkKitchenAssign.Checked)
                            Entity_ItemMaster.IsKitchenAssign = 1;
                        else
                            Entity_ItemMaster.IsKitchenAssign = 0;
                        if (chkClub.Checked)
                            Entity_ItemMaster.IsClub = 1;
                        else
                            Entity_ItemMaster.IsClub = 0;

                        InsertRow = Obj_ItemMaster.InsertRecord(ref Entity_ItemMaster, out StrError);

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




                                    Entity_ItemMaster.MinStockSupp = (!string.IsNullOrEmpty(dtInsert.Rows[i]["MinStockSupp"].ToString())) ? Convert.ToDecimal((dtInsert.Rows[i]["MinStockSupp"].ToString())) : 0;
                                    Entity_ItemMaster.MaxStockSupp = (!string.IsNullOrEmpty(dtInsert.Rows[i]["MaxStockSupp"].ToString())) ? Convert.ToDecimal((dtInsert.Rows[i]["MaxStockSupp"].ToString())) : 0;
                                    Entity_ItemMaster.ReorderSupp = (!string.IsNullOrEmpty(dtInsert.Rows[i]["ReorderSupp"].ToString())) ? Convert.ToDecimal((dtInsert.Rows[i]["ReorderSupp"].ToString())) : 0;

                                    Entity_ItemMaster.StockLocationID = (!string.IsNullOrEmpty(dtInsert.Rows[i]["LocationId"].ToString())) ? Convert.ToInt32((dtInsert.Rows[i]["LocationId"].ToString())) : 0;

                                    Entity_ItemMaster.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
                                    Entity_ItemMaster.ItemDesc = (!string.IsNullOrEmpty((dtInsert.Rows[i]["ItemDesc"].ToString()))) ? (dtInsert.Rows[i]["ItemDesc"].ToString()) : "";
                                    Entity_ItemMaster.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                                    Entity_ItemMaster.OpeningStockDetails = (!string.IsNullOrEmpty(dtInsert.Rows[i]["OpeningStock"].ToString())) ? Convert.ToDecimal((dtInsert.Rows[i]["OpeningStock"].ToString())) : 0;
                                    Entity_ItemMaster.LocationIDDetails = (!string.IsNullOrEmpty(dtInsert.Rows[i]["LocationId"].ToString())) ? Convert.ToInt32((dtInsert.Rows[i]["LocationId"].ToString())) : 0;

                                    Entity_ItemMaster.FromQty = Convert.ToDecimal(dtInsert.Rows[i]["MainUnitFactor"].ToString());
                                    Entity_ItemMaster.FromUnitID = Convert.ToInt32(dtInsert.Rows[i]["UnitID"].ToString());
                                    Entity_ItemMaster.ToQty = Convert.ToDecimal(dtInsert.Rows[i]["SubUnitFactor"].ToString());
                                    Entity_ItemMaster.ToUnitID = Convert.ToInt32(dtInsert.Rows[i]["SubUnitID"].ToString());
                                    Entity_ItemMaster.DrawingNo = dtInsert.Rows[i]["DrawingNo"].ToString();
                                    //Entity_ItemMaster.DrawingPath = "ScannedDrawings/" + ViewState["fileName"].ToString();
                                    Entity_ItemMaster.DrawingPath = dtInsert.Rows[i]["DrawingPath"].ToString(); 

                                    InsertRowDtls = Obj_ItemMaster.InsertDetailsRecord(ref Entity_ItemMaster, out StrError);
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
                                    InsertRowDtls = Obj_ItemMaster.InsertDetailsSizeRecord(ref Entity_ItemMaster, out StrError);
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
                                    InsertRow1 = Obj_ItemMaster.InsertUnitConvrsnDtls(ref Entity_ItemMaster, out StrError);
                                }
                            }
                            #endregion
                            //New Code By Anand On Dated 24 Mar 2014(For Unit Conversion in reverse order Also)
                            #region[Save Dtls To ItemDtlsUnitCalculation]
                            for (int i = 0; i < GrdUnitCal.Rows.Count; i++)
                            {
                                Entity_ItemMaster.ItemId = InsertRow;
                                Entity_ItemMaster.From_Factor = Convert.ToDecimal(GrdUnitCal.Rows[i].Cells[3].Text);
                                Entity_ItemMaster.From_UnitID = Convert.ToInt32(GrdUnitCal.Rows[i].Cells[4].Text);
                                Entity_ItemMaster.To_Factor = Convert.ToDecimal(GrdUnitCal.Rows[i].Cells[6].Text);
                                Entity_ItemMaster.To_UnitID = Convert.ToInt32(GrdUnitCal.Rows[i].Cells[7].Text);
                                Entity_ItemMaster.Factor_Desc = GrdUnitCal.Rows[i].Cells[9].Text.Equals("&nbsp;") ? "" : (GrdUnitCal.Rows[i].Cells[9].Text);

                                InsertUnitCal = Obj_ItemMaster.Insert_UnitCalculation(ref Entity_ItemMaster, out StrError);
                            }
                            #endregion
                            //New Code By Anand On Dated 24 Mar 2014(For Unit Conversion in reverse order Also)


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
            else
            {
                obj_Comman.ShowPopUpMsg("Please Select Unit or Enter Conversion Details in Unit Conversion Tab....!", this.Page);
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    //protected void btnpopup_Click(object sender, ImageClickEventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(this, typeof(string), "New Window", "window.open('~ScannedDrawings/" + fileName + "')", true);
    //}
    
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
                        //for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        //{
                        //    if ((Convert.ToInt32(dtCurrentTable.Rows[i]["SupplierId"]) == Convert.ToInt32(ddlSupplier.SelectedValue)) && ((dtCurrentTable.Rows[i]["Location"]).ToString() == (ddlStockLocation.SelectedItem.ToString())))
                        //    {
                        //       // DupFlag = true;
                        //        k = Convert.ToInt32(ViewState["GridIndex"]);
                        //    }
                        //    if ((Convert.ToInt32(dtCurrentTable.Rows[i]["UnitID"]) == Convert.ToInt32(DDLMAINUNIT.SelectedValue)) && (Convert.ToInt32(dtCurrentTable.Rows[i]["SubUnitID"]) == Convert.ToInt32(DDLSUBUNIT.SelectedValue)) && ((dtCurrentTable.Rows[i]["ItemDesc"]) == ((!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "")))
                        //    {
                        //        DupFlag = true;
                        //        k = Convert.ToInt32(ViewState["GridIndex"]);
                        //    }
                        //}


                        //if (DupFlag == true)
                        //{
                        //    PopUpYesNo.Show();
                        //    btnPopUpYes.Focus();

                        //}
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            if ((Convert.ToInt32(dtCurrentTable.Rows[i]["SupplierId"]) == Convert.ToInt32(ddlSupplier.SelectedValue)) && ((dtCurrentTable.Rows[i]["Location"]).ToString() == (ddlStockLocation.SelectedItem.ToString())))
                            {
                                DupFlag = true;
                                k = Convert.ToInt32(ViewState["GridIndex"]);
                            }
                            if ((Convert.ToInt32(dtCurrentTable.Rows[i]["UnitID"]) == Convert.ToInt32(DDLMAINUNIT.SelectedValue)) && (Convert.ToInt32(dtCurrentTable.Rows[i]["SubUnitID"]) == Convert.ToInt32(DDLSUBUNIT.SelectedValue)) && ((dtCurrentTable.Rows[i]["ItemDesc"]) == ((!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "")))
                            {
                                DupFlag = true;
                                k = Convert.ToInt32(ViewState["GridIndex"]);
                            }
                        }


                        if (DupFlag == true)
                        {
                            PopUpYesNo.Show();
                            btnPopUpYes.Focus();

                        }

                        else
                        {
                            dtTableRow = dtCurrentTable.NewRow();
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                            dtTableRow["SuplierName"] = ddlSupplier.SelectedItem;
                            dtTableRow["SupplierId"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                            dtTableRow["#"] = 0;
                            dtTableRow["PurchaseRate"] = TxtPurchaseRate.Text;
                            dtTableRow["Location"] = ddlStockLocation.SelectedItem;
                            dtTableRow["OpeningStock"] = TxtOpeningStock.Text;



                            dtTableRow["MinStockSupp"] = txtMinstockSupp.Text;

                            dtTableRow["MaxStockSupp"] = TxtMaxStockSupp.Text;

                            dtTableRow["ReorderSupp"] = txtReorderSupp.Text;

                            dtTableRow["LocationId"] = Convert.ToInt32(ddlStockLocation.SelectedValue);
                            dtTableRow["ItemDesc"] = (!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "";
                            dtTableRow["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 1;


                            if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                            {
                                dtTableRow["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
                            }
                            else
                            {
                                dtTableRow["UnitID"] = Convert.ToInt32(ddlUnit.SelectedValue);
                            }

                            if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                            {
                                dtTableRow["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
                            }
                            else
                            {
                                dtTableRow["MainUnit"] = Convert.ToString(ddlUnit.SelectedItem);
                            }


                            if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                            {
                                dtTableRow["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
                            }
                            else
                            {
                                dtTableRow["SubUnitID"] = Convert.ToInt32(ddlUnit.SelectedValue);
                            }

                            if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                            {
                                dtTableRow["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
                            }
                            else
                            {
                                dtTableRow["SubUnit"] = Convert.ToString(ddlUnit.SelectedItem);
                            }
                            //if (Convert.ToInt32(txtDrawingNo.Text) > 0)
                            //{
                                dtTableRow["DrawingNo"] = txtDrawingNo.Text;
                            //}
                                if (lblFileup.Text != null)
                                {
                                    dtTableRow["DrawingPath"] =  ViewState["fileName"].ToString();
                                }

                                //if (lblFileup.Text != null)
                                //{
                                //    dtTableRow["DrawingPath"] = lblFileup.Text;
                                //}
                                //else
                                //{
                                //    dtTableRow["DrawingPath"] = "~/ScannedDrawings/" + ViewState["fileName"].ToString();
                                //}


                            //dtTableRow["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
                            // dtTableRow["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
                            dtTableRow["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 1;
                            //dtTableRow["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
                            // dtTableRow["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
                            dtCurrentTable.Rows.Add(dtTableRow);
                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            DataTable dt = (DataTable)GridDetails.DataSource;
                            Session["ItemData"] = dt;
                            MakeControlEmpty();
                        }


                    }
                    else
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            if ((Convert.ToInt32(dtCurrentTable.Rows[i]["SupplierId"]) == Convert.ToInt32(ddlSupplier.SelectedValue)) && ((dtCurrentTable.Rows[i]["Location"]).ToString() == (ddlStockLocation.SelectedItem.ToString())))
                            {
                                //DupFlag = true;
                                k = i;
                            }
                            if ((Convert.ToInt32(dtCurrentTable.Rows[i]["UnitID"]) == Convert.ToInt32(DDLMAINUNIT.SelectedValue)) && (Convert.ToInt32(dtCurrentTable.Rows[i]["SubUnitID"]) == Convert.ToInt32(DDLSUBUNIT.SelectedValue)) && ((dtCurrentTable.Rows[i]["ItemDesc"]) == ((!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "")))
                            {
                                DupFlag = true;
                                k = i;
                            }

                        }
                        //k =Convert.ToInt32(ViewState["GridIndex"]);
                        if (DupFlag == true)
                        {
                            PopUpYesNo.Show();
                            btnPopUpYes.Focus();

                        }

                        else
                        {
                            dtTableRow = dtCurrentTable.NewRow();
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                            dtTableRow["SuplierName"] = ddlSupplier.SelectedItem;
                            dtTableRow["SupplierId"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                            dtTableRow["#"] = 0;
                            dtTableRow["PurchaseRate"] = TxtPurchaseRate.Text;
                            dtTableRow["Location"] = ddlStockLocation.SelectedItem;
                            dtTableRow["OpeningStock"] = TxtOpeningStock.Text;

                            dtTableRow["MinStockSupp"] = txtMinstockSupp.Text;

                            dtTableRow["MaxStockSupp"] = TxtMaxStockSupp.Text;

                            dtTableRow["ReorderSupp"] = txtReorderSupp.Text;
                            dtTableRow["LocationId"] = Convert.ToInt32(ddlStockLocation.SelectedValue);
                            //string desc = (!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "";
                            dtTableRow["ItemDesc"] = (!string.IsNullOrEmpty(txtDescription.Text)) ? txtDescription.Text : "";
                             dtTableRow["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 1;

                            if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                            {
                                dtTableRow["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
                            }
                            else
                            {
                                dtTableRow["UnitID"] = Convert.ToInt32(ddlUnit.SelectedValue);
                            }

                            if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                            {
                                dtTableRow["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
                            }
                            else
                            {
                                dtTableRow["MainUnit"] = Convert.ToString(ddlUnit.SelectedItem);
                            }


                            if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                            {
                                dtTableRow["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
                            }
                            else
                            {
                                dtTableRow["SubUnitID"] = Convert.ToInt32(ddlUnit.SelectedValue);
                            }

                            if (Convert.ToInt32(DDLMAINUNIT.SelectedValue) > 0)
                            {
                                dtTableRow["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
                            }
                            else
                            {
                                dtTableRow["SubUnit"] = Convert.ToString(ddlUnit.SelectedItem);
                            }
                            //if (Convert.ToInt32(txtDrawingNo.Text) > 0)
                            //{
                                dtTableRow["DrawingNo"] = txtDrawingNo.Text;
                           // }
                                //if (lblFileup.Text != null)
                                //{
                                //    dtTableRow["DrawingPath"] = lblFileup.Text;
                                //}
                                //else
                                //{
                                //    dtTableRow["DrawingPath"] = "~/ScannedDrawings/" + ViewState["fileName"].ToString();
                                //}
                                if (lblFileup.Text != null)
                                {
                                    if (ViewState["fileName"] != null)
                                    {
                                        dtTableRow["DrawingPath"] = "~/ScannedDrawings/" + ViewState["fileName"].ToString();
                                    }
                                }
                            dtTableRow["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 1;
                            //dtTableRow["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
                            //dtTableRow["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
                            dtCurrentTable.Rows.Add(dtTableRow);

                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            DataTable dt = (DataTable)GridDetails.DataSource;
                            Session["ItemData"] = dt;
                            MakeControlEmpty();
                            lblFileup.Text = "";
                            txtDrawingNo.Text = "";
                        }

                    }


                    //else
                    //{
                    //   obj_Comman.ShowPopUpMsg("Purchase Rate Should Be Greater Than Zero",this.Page);
                    //}
                }
            }
           // Fill_CalulateGrid(); // Uncomment When Factor Save using Fron End
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void IMGBTNADDUNIT_Click(object sender, ImageClickEventArgs e)
    {
        //try
        //{
        //    if (hiddenbox1.Value == "1")
        //    {
        //        if (ViewState["UnitConversionTable"] != null)
        //        {
        //            DataTable dtCurrentTable = (DataTable)ViewState["UnitConversionTable"];
        //            DataRow dtTableRow = null;
        //            bool DupFlag = false;
        //            int k = 0;
        //            if (dtCurrentTable.Rows.Count > 0)
        //            {
        //                if (dtCurrentTable.Rows.Count == 1 && string.IsNullOrEmpty(dtCurrentTable.Rows[0]["MainUnit"].ToString()))
        //                {
        //                    dtCurrentTable.Rows.RemoveAt(0);
        //                }
        //                if (ViewState["UnitGridIndex"] != null)
        //                {
        //                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
        //                    {
        //                        if ((Convert.ToInt32(dtCurrentTable.Rows[i]["UnitID"]) == Convert.ToInt32(DDLMAINUNIT.SelectedValue)) && ((dtCurrentTable.Rows[i]["SubUnitID"]).ToString() == (DDLSUBUNIT.SelectedValue.ToString())))
        //                        {
        //                            DupFlag = true;
        //                            k = i;
        //                        }
        //                    }
        //                    if (DupFlag == true)
        //                    {
        //                        dtCurrentTable.Rows[k]["#"] = 0;
        //                        dtCurrentTable.Rows[k]["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 0;
        //                        dtCurrentTable.Rows[k]["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
        //                        dtCurrentTable.Rows[k]["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
        //                        dtCurrentTable.Rows[k]["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 0;
        //                        dtCurrentTable.Rows[k]["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
        //                        dtCurrentTable.Rows[k]["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
        //                        ViewState["UnitConversionTable"] = dtCurrentTable;
        //                        GridUnitConversion.DataSource = dtCurrentTable;
        //                        GridUnitConversion.DataBind();
        //                        MakeControlEmptyUnitConversion();
        //                    }

        //                    else
        //                    {
        //                        dtTableRow = dtCurrentTable.NewRow();
        //                        int rowindex = Convert.ToInt32(ViewState["UnitGridIndex"]);
        //                        dtTableRow["#"] = 0;
        //                        dtTableRow["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 0;
        //                        dtTableRow["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
        //                        dtTableRow["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
        //                        dtTableRow["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 0;
        //                        dtTableRow["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
        //                        dtTableRow["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
        //                        dtCurrentTable.Rows.Add(dtTableRow);
        //                        ViewState["UnitConversionTable"] = dtCurrentTable;
        //                        GridUnitConversion.DataSource = dtCurrentTable;
        //                        GridUnitConversion.DataBind();
        //                        MakeControlEmptyUnitConversion();
        //                    }


        //                }
        //                else
        //                {
        //                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
        //                    {
        //                        if ((Convert.ToInt32(dtCurrentTable.Rows[i]["UnitID"]) == Convert.ToInt32(DDLMAINUNIT.SelectedValue)) && ((dtCurrentTable.Rows[i]["SubUnitID"]).ToString() == (DDLSUBUNIT.SelectedValue.ToString())))
        //                        {
        //                            DupFlag = true;
        //                            k = i;
        //                        }
        //                    }
        //                    if (DupFlag == true)
        //                    {
        //                        dtCurrentTable.Rows[k]["#"] = 0;
        //                        dtCurrentTable.Rows[k]["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 0;
        //                        dtCurrentTable.Rows[k]["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
        //                        dtCurrentTable.Rows[k]["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
        //                        dtCurrentTable.Rows[k]["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 0;
        //                        dtCurrentTable.Rows[k]["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
        //                        dtCurrentTable.Rows[k]["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
        //                        ViewState["UnitConversionTable"] = dtCurrentTable;
        //                        GridUnitConversion.DataSource = dtCurrentTable;
        //                        GridUnitConversion.DataBind();
        //                        MakeControlEmptyUnitConversion();
        //                    }

        //                    else
        //                    {
        //                        dtTableRow = dtCurrentTable.NewRow();
        //                        int rowindex = Convert.ToInt32(ViewState["UnitGridIndex"]);
        //                        dtTableRow["#"] = 0;
        //                        dtTableRow["MainUnitFactor"] = !string.IsNullOrEmpty(TXTUNITQTY.Text) ? Convert.ToDecimal(TXTUNITQTY.Text) : 0;
        //                        dtTableRow["UnitID"] = Convert.ToInt32(DDLMAINUNIT.SelectedValue);
        //                        dtTableRow["MainUnit"] = Convert.ToString(DDLMAINUNIT.SelectedItem);
        //                        dtTableRow["SubUnitFactor"] = !string.IsNullOrEmpty(TXTSUBUNITQTY.Text) ? Convert.ToDecimal(TXTSUBUNITQTY.Text) : 0;
        //                        dtTableRow["SubUnitID"] = Convert.ToInt32(DDLSUBUNIT.SelectedValue);
        //                        dtTableRow["SubUnit"] = Convert.ToString(DDLSUBUNIT.SelectedItem);
        //                        dtCurrentTable.Rows.Add(dtTableRow);
        //                        ViewState["UnitConversionTable"] = dtCurrentTable;
        //                        GridUnitConversion.DataSource = dtCurrentTable;
        //                        GridUnitConversion.DataBind();
        //                        MakeControlEmptyUnitConversion();
        //                    }

        //                }


        //                //else
        //                //{
        //                //   obj_Comman.ShowPopUpMsg("Purchase Rate Should Be Greater Than Zero",this.Page);
        //                //}
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, UpdateRowDtls = 0, UpdateRow1 = 0, UpdateUnitCal = 0;
        try
        {
            Fill_CalulateGrid(); // Uncomment When Factor Save using Fron End


            if (ViewState["EditID"] != null)
            {
                Entity_ItemMaster.ItemId = Convert.ToInt32(ViewState["EditID"]);
            }
            Entity_ItemMaster.ItemCode = TxtItemCode.Text;
            Entity_ItemMaster.Barcode = (!string.IsNullOrEmpty(TxtMfgBarcode.Text)) ? Convert.ToString(TxtMfgBarcode.Text) : "0";
            Entity_ItemMaster.ItemName = (!string.IsNullOrEmpty(TxtItemName.Text)) ? Convert.ToString(TxtItemName.Text) : "0";
            Entity_ItemMaster.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
            Entity_ItemMaster.SubcategoryId = Convert.ToInt32(ddlSubCategory.SelectedValue);
            Entity_ItemMaster.TaxDtlsId = Convert.ToInt32(ddlGSTPer.SelectedValue);
            Entity_ItemMaster.TaxTemplateID = Convert.ToInt32(DDLTAXTEMPLATE.SelectedValue); 
            Entity_ItemMaster.DeliveryPeriod = (!string.IsNullOrEmpty(TxtDelivryPeriod.Text)) ? Convert.ToInt32(TxtDelivryPeriod.Text) : 0;
            Entity_ItemMaster.TaxPer = (!string.IsNullOrEmpty(TxtTaxPer.Text)) ? Convert.ToDecimal(TxtTaxPer.Text) : 0;
            Entity_ItemMaster.MinStockLevel = (!string.IsNullOrEmpty(TxtMinStockLevel.Text)) ? Convert.ToString(TxtMinStockLevel.Text) : "0";
            Entity_ItemMaster.ReorderLevel = (!string.IsNullOrEmpty(TxtReOrdLevel.Text)) ? Convert.ToString(TxtReOrdLevel.Text) : "0";
            Entity_ItemMaster.MaxStockLevel = (!string.IsNullOrEmpty(TxtMaxStockLevel.Text)) ? Convert.ToString(TxtMaxStockLevel.Text) : "0";
            Entity_ItemMaster.OpeningStock = (!string.IsNullOrEmpty(TxtOpeningStock.Text)) ? Convert.ToDecimal(TxtOpeningStock.Text) : 0;
            Entity_ItemMaster.AsOn = (!string.IsNullOrEmpty(TxtAsOnDate.Text)) ? Convert.ToDateTime(TxtAsOnDate.Text) : Convert.ToDateTime("1-Jan-1753");
            Entity_ItemMaster.StockLocationID = 0;// Convert.ToInt32(ddlStockLocation.SelectedValue);
            Entity_ItemMaster.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
            Entity_ItemMaster.Remark = txtRemark.Text.Trim();
            Entity_ItemMaster.HSNCode = TxtHSNCode.Text.Trim();

            Entity_ItemMaster.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_ItemMaster.LoginDate = DateTime.Now;
            if (ChkKitchenAssign.Checked)
                Entity_ItemMaster.IsKitchenAssign = 1;
            else
                Entity_ItemMaster.IsKitchenAssign = 0;
            if (chkClub.Checked)
                Entity_ItemMaster.IsClub = 1;
            else
                Entity_ItemMaster.IsClub = 0;
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
                        Entity_ItemMaster.MinStockSupp = (!string.IsNullOrEmpty(dtInsert.Rows[i]["MinStockSupp"].ToString())) ? Convert.ToDecimal((dtInsert.Rows[i]["MinStockSupp"].ToString())) : 0;
                        Entity_ItemMaster.MaxStockSupp = (!string.IsNullOrEmpty(dtInsert.Rows[i]["MaxStockSupp"].ToString())) ? Convert.ToDecimal((dtInsert.Rows[i]["MaxStockSupp"].ToString())) : 0;
                        Entity_ItemMaster.ReorderSupp = (!string.IsNullOrEmpty(dtInsert.Rows[i]["ReorderSupp"].ToString())) ? Convert.ToDecimal((dtInsert.Rows[i]["ReorderSupp"].ToString())) : 0;

                        
                        
                        Entity_ItemMaster.StockLocationID = (!string.IsNullOrEmpty(dtInsert.Rows[i]["LocationId"].ToString())) ? Convert.ToInt32((dtInsert.Rows[i]["LocationId"].ToString())) : 0;
                        Entity_ItemMaster.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
                        Entity_ItemMaster.ItemDesc = (!string.IsNullOrEmpty((dtInsert.Rows[i]["ItemDesc"].ToString()))) ? (dtInsert.Rows[i]["ItemDesc"].ToString()) : "";
                        Entity_ItemMaster.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        Entity_ItemMaster.OpeningStockDetails = (!string.IsNullOrEmpty(dtInsert.Rows[i]["OpeningStock"].ToString())) ? Convert.ToDecimal((dtInsert.Rows[i]["OpeningStock"].ToString())) : 0;
                        Entity_ItemMaster.LocationIDDetails = (!string.IsNullOrEmpty(dtInsert.Rows[i]["LocationId"].ToString())) ? Convert.ToInt32((dtInsert.Rows[i]["LocationId"].ToString())) : 0;
                        Entity_ItemMaster.ItemDetailsId = (!string.IsNullOrEmpty(dtInsert.Rows[i]["#"].ToString())) ? Convert.ToInt32((dtInsert.Rows[i]["#"].ToString())) : 0;
                        Entity_ItemMaster.FromQty = Convert.ToDecimal(dtInsert.Rows[i]["MainUnitFactor"].ToString());
                        Entity_ItemMaster.FromUnitID = Convert.ToInt32(dtInsert.Rows[i]["UnitID"].ToString());
                        Entity_ItemMaster.ToQty = Convert.ToDecimal(dtInsert.Rows[i]["SubUnitFactor"].ToString());
                        Entity_ItemMaster.ToUnitID = Convert.ToInt32(dtInsert.Rows[i]["SubUnitID"].ToString());
                        Entity_ItemMaster.DrawingNo = dtInsert.Rows[i]["DrawingNo"].ToString() ;
                        Entity_ItemMaster.DrawingPath =  dtInsert.Rows[i]["DrawingPath"].ToString() ;


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

                //#region [SAVE TO UNIT CONVERSION NEW]----
                //if (UpdateRow > 0)
                //{

                //    if (ViewState["UnitConversionTable"] != null)
                //    {
                //        int InsertUnitMaster = 0;
                //        int InsertUnitDetails = 0;
                //        DataTable dtunitconversion = (DataTable)ViewState["UnitConversionTable"];
                //        if ((dtunitconversion.Rows.Count > 0) && (dtunitconversion.Rows[0]["UnitID"].ToString() != "0"))
                //        {
                //            DataTable dtMainUnitColumns = dtunitconversion.DefaultView.ToTable(false, "UnitID", "MainUnitFactor", "SubUnitID", "SubUnitFactor");
                //            DataTable dtMainUnitColumnsUnitID = dtunitconversion.DefaultView.ToTable(false, "UnitID");
                //            DataTable dtMainUnitColumnsSubUnitID = dtunitconversion.DefaultView.ToTable(false, "SubUnitID");
                //            DataTable dtUnitConversionDtls = new DataTable();
                //            dtUnitConversionDtls.Columns.Add("ItemID");
                //            dtUnitConversionDtls.Columns.Add("ItemUnitDtlsID");
                //            dtUnitConversionDtls.Columns.Add("MainUnitQty");
                //            dtUnitConversionDtls.Columns.Add("MainUnitID");
                //            dtUnitConversionDtls.Columns.Add("SubUnitQty");
                //            dtUnitConversionDtls.Columns.Add("SubUnitID");
                //            for (int mainrow = 0; mainrow < dtMainUnitColumnsUnitID.Rows.Count; mainrow++)
                //            {
                //                for (int Subrow = 0; Subrow < dtMainUnitColumnsSubUnitID.Rows.Count; Subrow++)
                //                {
                //                    DataRow dtUnitConversionDtlsRow = dtUnitConversionDtls.NewRow();
                //                    dtUnitConversionDtlsRow["ItemID"] = Convert.ToInt32(ViewState["EditID"]);
                //                    dtUnitConversionDtlsRow["ItemUnitDtlsID"] = 0;
                //                    dtUnitConversionDtlsRow["MainUnitQty"] = dtMainUnitColumns.Rows[mainrow]["MainUnitFactor"];
                //                    dtUnitConversionDtlsRow["MainUnitID"] = dtMainUnitColumns.Rows[mainrow]["UnitID"];
                //                    dtUnitConversionDtlsRow["SubUnitQty"] = dtMainUnitColumns.Rows[Subrow]["SubUnitFactor"];
                //                    dtUnitConversionDtlsRow["SubUnitID"] = dtMainUnitColumns.Rows[Subrow]["SubUnitID"];
                //                    dtUnitConversionDtls.Rows.Add(dtUnitConversionDtlsRow);
                //                }
                //            }
                //            #region [INSERT MASTER DATA]
                //            for (int masterrow = 0; masterrow < dtunitconversion.Rows.Count; masterrow++)
                //            {
                //                Entity_ItemMaster.ItemID = Convert.ToInt32(ViewState["EditID"]);
                //                Entity_ItemMaster.FromQty = Convert.ToDecimal(dtunitconversion.Rows[masterrow]["MainUnitFactor"].ToString());
                //                Entity_ItemMaster.FromUnitID = Convert.ToInt32(dtunitconversion.Rows[masterrow]["UnitID"].ToString());
                //                Entity_ItemMaster.ToQty = Convert.ToDecimal(dtunitconversion.Rows[masterrow]["SubUnitFactor"].ToString());
                //                Entity_ItemMaster.ToUnitID = Convert.ToInt32(dtunitconversion.Rows[masterrow]["SubUnitID"].ToString());
                //                InsertUnitMaster = Obj_ItemMaster.InsertItemUnitMaster(ref Entity_ItemMaster, out StrError);
                //                if (InsertUnitMaster > 0)
                //                {
                //                    Entity_ItemMaster.ItemMasterID = InsertUnitMaster;
                //                    Entity_ItemMaster.ItemID = Convert.ToInt32(ViewState["EditID"]);
                //                    Entity_ItemMaster.FromQty = Convert.ToDecimal(dtunitconversion.Rows[masterrow]["MainUnitFactor"].ToString());
                //                    Entity_ItemMaster.FromUnitID = Convert.ToInt32(dtunitconversion.Rows[masterrow]["UnitID"].ToString());
                //                    Entity_ItemMaster.ToQty = Convert.ToDecimal(dtunitconversion.Rows[masterrow]["SubUnitFactor"].ToString());
                //                    Entity_ItemMaster.ToUnitID = Convert.ToInt32(dtunitconversion.Rows[masterrow]["SubUnitID"].ToString());
                //                    InsertUnitDetails = Obj_ItemMaster.InsertItemUnitDetails(ref Entity_ItemMaster, out StrError);
                //                }
                //            }
                //        }
                //        else
                //        {
                //            if (Convert.ToInt32(ddlUnit.SelectedValue.ToString()) > 0)
                //            {
                //                Entity_ItemMaster.ItemID = Convert.ToInt32(ViewState["EditID"]);
                //                Entity_ItemMaster.FromQty = 1;
                //                Entity_ItemMaster.FromUnitID = Convert.ToInt32(ddlUnit.SelectedValue.ToString());
                //                Entity_ItemMaster.ToQty = 1;
                //                Entity_ItemMaster.ToUnitID = Convert.ToInt32(ddlUnit.SelectedValue.ToString());
                //                InsertUnitMaster = Obj_ItemMaster.InsertItemUnitMaster(ref Entity_ItemMaster, out StrError);
                //                if (InsertUnitMaster > 0)
                //                {
                //                    Entity_ItemMaster.ItemMasterID = InsertUnitMaster;
                //                    Entity_ItemMaster.ItemID = Convert.ToInt32(ViewState["EditID"]);
                //                    Entity_ItemMaster.FromQty = 1;
                //                    Entity_ItemMaster.FromUnitID = Convert.ToInt32(ddlUnit.SelectedValue.ToString());
                //                    Entity_ItemMaster.ToQty = 1;
                //                    Entity_ItemMaster.ToUnitID = Convert.ToInt32(ddlUnit.SelectedValue.ToString());
                //                    InsertUnitDetails = Obj_ItemMaster.InsertItemUnitDetails(ref Entity_ItemMaster, out StrError);
                //                }
                //            }
                //        }
                //            #endregion
                //    }
                //    else
                //    {
                //        int InsertUnitDetails = 0;
                //        int InsertUnitMaster = 0;
                //        if (Convert.ToInt32(ddlUnit.SelectedValue.ToString()) > 0)
                //        {

                //            Entity_ItemMaster.ItemID = Convert.ToInt32(ViewState["EditID"]);
                //            Entity_ItemMaster.FromQty = 1;
                //            Entity_ItemMaster.FromUnitID = Convert.ToInt32(ddlUnit.SelectedValue.ToString());
                //            Entity_ItemMaster.ToQty = 1;
                //            Entity_ItemMaster.ToUnitID = Convert.ToInt32(ddlUnit.SelectedValue.ToString());
                //            InsertUnitMaster = Obj_ItemMaster.InsertItemUnitMaster(ref Entity_ItemMaster, out StrError);
                //        }
                //        if (InsertUnitMaster > 0)
                //        {
                //            Entity_ItemMaster.ItemMasterID = InsertUnitMaster;
                //            Entity_ItemMaster.ItemID = Convert.ToInt32(ViewState["EditID"]);
                //            Entity_ItemMaster.FromQty = 1;
                //            Entity_ItemMaster.FromUnitID = Convert.ToInt32(ddlUnit.SelectedValue.ToString());
                //            Entity_ItemMaster.ToQty = 1;
                //            Entity_ItemMaster.ToUnitID = Convert.ToInt32(ddlUnit.SelectedValue.ToString());
                //            InsertUnitDetails = Obj_ItemMaster.InsertItemUnitDetails(ref Entity_ItemMaster, out StrError);
                //        }
                //    }
                //}
                //#endregion ------------------------------

                //New Code By Anand On Dated 24 Mar 2014(For Unit Conversion in reverse order Also)
                #region[Save Dtls To ItemDtlsUnitCalculation]
                for (int i = 0; i < GrdUnitCal.Rows.Count; i++)
                {
                    Entity_ItemMaster.ItemId = Convert.ToInt32(ViewState["EditID"]);
                    Entity_ItemMaster.From_Factor = Convert.ToDecimal(GrdUnitCal.Rows[i].Cells[3].Text);
                    Entity_ItemMaster.From_UnitID = Convert.ToInt32(GrdUnitCal.Rows[i].Cells[4].Text);
                    Entity_ItemMaster.To_Factor = Convert.ToDecimal(GrdUnitCal.Rows[i].Cells[6].Text);
                    Entity_ItemMaster.To_UnitID = Convert.ToInt32(GrdUnitCal.Rows[i].Cells[7].Text);
                    Entity_ItemMaster.Factor_Desc = GrdUnitCal.Rows[i].Cells[9].Text.Equals("&nbsp;") ? "" : (GrdUnitCal.Rows[i].Cells[9].Text);

                    UpdateUnitCal = Obj_ItemMaster.Insert_UnitCalculation(ref Entity_ItemMaster, out StrError);
                }
                #endregion
                //New Code By Anand On Dated 24 Mar 2014(For Unit Conversion in reverse order Also)
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
                            DataTable dt = new DataTable();
                            dt = Ds.Tables[0];

                            DataTable dt1 = new DataTable();
                            dt1 = Ds.Tables[1];
                            DataTable dt2 = new DataTable();
                            dt2 = Ds.Tables[2];
                            DataTable dt3 = new DataTable();
                            dt3 = Ds.Tables[3];
                            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                            {
                                ddlUnit.Enabled = true;
                                TxtItemCode.Text = Ds.Tables[0].Rows[0]["ItemCode"].ToString();
                                TxtMfgBarcode.Text = Ds.Tables[0].Rows[0]["Barcode"].ToString();
                                TxtItemName.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                                ddlCategory.SelectedValue = Convert.ToInt32(Ds.Tables[0].Rows[0]["CategoryId"]).ToString();
                                ddlCategory_SelectedIndexChanged(ddlCategory, System.EventArgs.Empty);
                                ddlSubCategory.SelectedValue = Ds.Tables[0].Rows[0]["SubCategoryId"].ToString();

                                DDLTAXTEMPLATE.SelectedValue = Ds.Tables[0].Rows[0]["TaxTemplateID"].ToString();
                                DDLTAXTEMPLATE_SelectedIndexChanged(DDLTAXTEMPLATE.SelectedValue,System.EventArgs.Empty);
                                ddlGSTPer.SelectedValue = Ds.Tables[0].Rows[0]["TaxDtlsId"].ToString();
                                TxtTaxPer.Text = Ds.Tables[0].Rows[0]["TaxPer"].ToString();
                                TxtMinStockLevel.Text = Ds.Tables[0].Rows[0]["MinStockLevel"].ToString();
                                TxtReOrdLevel.Text = Ds.Tables[0].Rows[0]["ReorderLevel"].ToString();
                                TxtMaxStockLevel.Text = Ds.Tables[0].Rows[0]["MaxStockLevel"].ToString();
                                TXTNetOpeningStock.Text = Ds.Tables[0].Rows[0]["OpeningStock"].ToString();
                                TxtDelivryPeriod.Text = Ds.Tables[0].Rows[0]["DeliveryPeriod"].ToString();
                                txtRemark.Text = Ds.Tables[0].Rows[0]["ItemRemark"].ToString();
                                TxtHSNCode.Text = Ds.Tables[0].Rows[0]["HSNCode"].ToString();

                                if (string.IsNullOrEmpty(Ds.Tables[0].Rows[0]["AsOn"].ToString()))
                                {
                                    TxtAsOnDate.Text = string.Empty;
                                }
                                else
                                {
                                    TxtAsOnDate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["AsOn"].ToString()).ToString("dd-MMM-yyyy");
                                }

                                ddlStockLocation.SelectedValue = Ds.Tables[0].Rows[0]["StockLocationID"].ToString();

                                if (Convert.ToInt32(Ds.Tables[0].Rows[0]["IsKitchenAssign"].ToString()) == 1)
                                    ChkKitchenAssign.Checked = true;
                                else
                                    ChkKitchenAssign.Checked = false;
                                if (Convert.ToInt32(Ds.Tables[0].Rows[0]["IsClub"].ToString()) == 1)
                                    chkClub.Checked = true;
                                else
                                    chkClub.Checked = false;

                                ddlUnit.SelectedValue = Ds.Tables[0].Rows[0]["UnitId"].ToString();
                                ItemId = Convert.ToInt32(ViewState["EditID"]);
                                UnitId = Convert.ToInt32(Ds.Tables[0].Rows[0]["UnitId"].ToString());
                                // DDLMAINUNIT.SelectedValue =   BindUnitConversiondtls(UnitId,ItemId);
                                DDLMAINUNIT.SelectedValue = Ds.Tables[0].Rows[0]["UnitId"].ToString(); ;
                                BindItemUnitDtls(UnitId, ItemId);

                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            if (Ds.Tables[3].Rows.Count > 0)
                            {
                                ViewState["ITEMINUSEORNOT"] = 1;
                                ddlUnit.Enabled = false;
                                DDLMAINUNIT.SelectedValue = Ds.Tables[0].Rows[0]["UnitId"].ToString();
                                DDLSUBUNIT.Enabled = true;
                                DDLMAINUNIT.Enabled = true;

                            }
                            if (Ds.Tables[1].Rows.Count > 0)
                            {
                                ViewState["CurrentTable"] = Ds.Tables[1];
                                GridDetails.DataSource = Ds.Tables[1];
                                GridDetails.DataBind();
                                dt = (DataTable)GridDetails.DataSource;
                                Session["ItemData"] = dt;

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
                            BtnDelete.Visible = BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            CheckUserRight();

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
        //try
        //{
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
                    txtMinstockSupp.Text = ((TextBox)GridDetails.Rows[Index].FindControl("GrdtxtMinStockSupp")).Text;
                    TxtMaxStockSupp.Text = ((TextBox)GridDetails.Rows[Index].FindControl("GrdtxtMaxStockSupp")).Text;
                    txtReorderSupp.Text = ((TextBox)GridDetails.Rows[Index].FindControl("GrdtxtReorderSupp")).Text;
                    if (!GridDetails.Rows[Index].Cells[9].Text.Equals("&nbsp;"))
                    {
                        //string desc = GridDetails.Rows[Index].Cells[9].Text;
                        //string htmlEncoded = Server.HtmlEncode(GridDetails.Rows[Index].Cells[9].Text);
                        txtDescription.Text = Server.HtmlDecode(GridDetails.Rows[Index].Cells[9].Text);
                    }
                    else
                    {
                        txtDescription.Text = "";
                    }
                    ddlStockLocation.Enabled = ddlSupplier.Enabled = false;
                    //fUpload.se= GridDetails.Rows[Index].Cells[15].Text;
                    txtDrawingNo.Text = GridDetails.Rows[Index].Cells[16].Text;
                    //lblFileup.Text = GridDetails.Rows[Index].Cells[17].Text;
                    TXTUNITQTY.Text = GridDetails.Rows[Index].Cells[10].Text;
                    DDLMAINUNIT.SelectedValue = GridDetails.Rows[Index].Cells[11].Text;
                    TXTSUBUNITQTY.Text = GridDetails.Rows[Index].Cells[13].Text;
                    DDLSUBUNIT.SelectedValue = GridDetails.Rows[Index].Cells[14].Text;
                    if (fUpload.FileName == "")
                    {

                    }
                    string str = GridDetails.Rows[Index].Cells[17].Text;
                    string ext = str.Substring(0, str.LastIndexOf("/") + 1);
                    int len = str.Length;
                    int cnt=0;
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i].ToString() == " " && str[i+1].ToString() == " ")
                        {
                        
                        }else{
                            cnt++;
                        }
                    }
                  //  string sub = str.Substring((ext.Length-1 ), cnt-1);
                    string sub = str.Substring(ext.Length);
                    
                    lblFileup.Text = sub + " is uploaded..!!";

                }
            }
            else {
                //Index = Convert.ToInt32(e.CommandArgument);
               // ScriptManager.RegisterStartupScript(this, typeof(string), "New Window", "window.open('~ScannedDrawings/" + ViewState["fileName"].ToString()+ "')", true);
               // string path = "~/ScannedDrawings/" + ViewState["fileName"].ToString();
                //Response.Redirect("~/ScannedDrawings/Kunal Passports.pdf");
                //string path="~/ScannedDrawings/Kunal Passports.pdf";
                //Response.Redirect("~/ScannedDrawings/Kunal Passports.pdf");

            }
        //}
        //catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void GridDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //try
        //{

        //    if (ViewState["CurrentTable"] != null)
        //    {
        //        int id = e.RowIndex;
        //        DataTable dt = (DataTable)ViewState["CurrentTable"];

        //        dt.Rows.RemoveAt(id);
        //        if (dt.Rows.Count > 0)
        //        {
        //            GridDetails.DataSource = dt;
        //            ViewState["CurrentTable"] = dt;
        //            GridDetails.DataBind();
        //            DataTable dt1 = (DataTable)GridDetails.DataSource;
        //            Session["ItemData"] = dt1;
        //        }
        //        else
        //        {
        //            SetInitialRow();
        //        }
        //        MakeControlEmpty();
        //    }
        //    //    }
        //}

        //catch (Exception ex) { throw new Exception(ex.Message); }

        try
        {
            int DeleteId = Convert.ToInt32(((ImageButton)GridDetails.Rows[e.RowIndex].Cells[0].FindControl("ImageBtnDelete")).CommandArgument.ToString());

            if (DeleteId != 0)
            {
                Entity_ItemMaster.ItemDetailsId = DeleteId;
                
                int iDelete = Obj_ItemMaster.DeleteItemDetails(ref Entity_ItemMaster, out StrError);
                if (iDelete != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_ItemMaster = null;
            obj_Comman = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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
                    //GridUnitConversion.DataSource = dt;
                    ViewState["UnitConversionTable"] = dt;
                    //GridUnitConversion.DataBind();
                }
                else
                {
                    // SetInitialRowUnitConversion();
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

            if (ViewState["ITEMINUSEORNOT"] == null)
            {
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
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Cant Delete Record.. \nUsed In Further Process..!", this.Page);
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
    //protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    switch (e.CommandName)
    //    {
    //        case ("Page"):
    //            {

    //                #region [Code For Pagging in Repeater]--------------------------------
    //                PageNumber = Convert.ToInt32(e.CommandArgument) - 1;// Add For Pdgging by Piyush
    //                PagedDataSource pgitems = new PagedDataSource();
    //                DataTable dt = new DataTable();
    //                dt = (DataTable)ViewState["CurrentTable1"];
    //                System.Data.DataView dv = new System.Data.DataView(dt);
    //                pgitems.DataSource = dv;
    //                pgitems.AllowPaging = true;
    //                pgitems.PageSize = 16;
    //                pgitems.CurrentPageIndex = PageNumber;
    //                if (pgitems.PageCount > 1)
    //                {
    //                   // rptPages.Visible = true;
    //                    System.Collections.ArrayList pages = new System.Collections.ArrayList();
    //                    for (int i = 0; i < pgitems.PageCount; i++)
    //                        pages.Add((i + 1).ToString());
    //                    //Extra Code Here                       
    //                    System.Collections.ArrayList pages1 = new System.Collections.ArrayList();
    //                    for (int i = 0; i < 5; i++)
    //                        pages1.Add((i + 1).ToString());
    //                    //End Here
    //                    //rptPages.DataSource = pages1;
    //                    //rptPages.DataBind();

    //                }
    //                else
    //                    GridDetails.Visible = false;
    //                GrdReport.DataSource = pgitems;
    //                GrdReport.DataBind();

    //                break;
    //                #endregion [Code For Pagging in Repeater]--------------------------------
    //            }

    //        case ("Next"):
    //            {
    //                if (e.Item.ItemType == ListItemType.Footer)
    //                {
    //                    ((LinkButton)rptPages.Controls[0].FindControl("btnPrev")).Visible = true;

    //                    LinkButton pos4 = ((LinkButton)rptPages.Controls[rptPages.Controls.Count - 2].FindControl("btnPage"));
    //                    if (str == countPage)
    //                    {
    //                        ((LinkButton)rptPages.Controls[rptPages.Controls.Count - 1].FindControl("btnNext")).Visible = false;
    //                    }
    //                    if (str != Convert.ToInt32(pos4.CommandArgument))
    //                    {
    //                        str = Convert.ToInt32(pos4.CommandArgument);
    //                        System.Collections.ArrayList pages1 = new System.Collections.ArrayList();
    //                        for (int i = 0; i < 5; i++)
    //                        {
    //                            if (str < countPage)
    //                            {
    //                                pages1.Add((str + 1).ToString());
    //                                str++;
    //                            }
    //                        }
    //                        rptPages.DataSource = pages1;
    //                        rptPages.DataBind();

    //                    }
    //                }
    //                break;
    //            }
    //        case ("Prev"):
    //            {
    //                if (e.Item.ItemType == ListItemType.Header)
    //                {
    //                    LinkButton pos4 = ((LinkButton)rptPages.Controls[rptPages.Controls.Count - 2].FindControl("btnPage"));

    //                    if ((Convert.ToInt32(pos4.CommandArgument) - 4) == 0 || Convert.ToInt32(pos4.CommandArgument) == 5)
    //                    {
    //                        ((LinkButton)rptPages.Controls[0].FindControl("btnPrev")).Visible = false;
    //                    }
    //                    if (str != Convert.ToInt32(pos4.CommandArgument) && Convert.ToInt32(pos4.CommandArgument) > 5)
    //                    {
    //                        str = Convert.ToInt32(pos4.CommandArgument);
    //                        str = str - 4;
    //                        System.Collections.ArrayList pages1 = new System.Collections.ArrayList();
    //                        for (int i = 0; i < 5; i++)
    //                        {
    //                            if (str < countPage)
    //                            {
    //                                if (str > 1)
    //                                {
    //                                    pages1.Add((str - 1).ToString());
    //                                    str--;
    //                                }
    //                            }
    //                        }
    //                        str = str + 4;
    //                        pages1.Reverse(0, pages1.Count);
    //                        rptPages.DataSource = pages1;
    //                        rptPages.DataBind();
    //                    }
    //                }
    //                break;
    //            }
    //    }
    //}
    //public void CallPagging()
    //{
    //    #region [Code For Pagging in Repeater]--------------------------------
    //    PageNumber = Convert.ToInt32(1);// Add For Pdgging by Piyush
    //    PagedDataSource pgitems = new PagedDataSource();
    //    DataTable dt = new DataTable();
    //    dt = (DataTable)ViewState["CurrentTable1"];
    //    System.Data.DataView dv = new System.Data.DataView(dt);
    //    pgitems.DataSource = dv;
    //    pgitems.AllowPaging = true;
    //    pgitems.PageSize = 16;
    //    pgitems.CurrentPageIndex = PageNumber;
    //    if (pgitems.PageCount > 1)
    //    {
    //     //   rptPages.Visible = true;
    //        System.Collections.ArrayList pages = new System.Collections.ArrayList();
    //        for (int i = 0; i < pgitems.PageCount; i++)
    //            pages.Add((i + 1).ToString());
    //        //Extra Code Here
    //        countPage = pgitems.PageCount;
    //        System.Collections.ArrayList pages1 = new System.Collections.ArrayList();
    //        for (int i = 0; i < 5; i++)
    //            pages1.Add((i + 1).ToString());
    //        //End Here
    //     //   rptPages.DataSource = pages1;
    //     //   rptPages.DataBind();
    //    }
    //    else
    //    {
    //        //GridDetails.Visible = false;
    //    }
    //    GrdReport.DataSource = pgitems;
    //    GrdReport.DataBind();

    //  //  ((LinkButton)rptPages.Controls[0].FindControl("btnPrev")).Visible = false;
    //    if ((pgitems.PageCount) <= 5)
    //    {
    //      //  ((LinkButton)rptPages.Controls[rptPages.Controls.Count - 1].FindControl("btnNext")).Visible = false;
    //    }
    //    #endregion [Code For Pagging in Repeater]--------------------------------
    //}
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
      //  rptPages.ItemCommand +=
      //  new RepeaterCommandEventHandler(rptPages_ItemCommand);
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
        SearchCategory = SearchCategory.ToString();
        String[] SearchList = Obj_ItemMaster.GetSuggestedRecordSubCategory(prefixText, SearchCategory);
        return SearchList;
    }
    protected void TxtSearchCategory_TextChanged(object sender, EventArgs e)
    {
        i = PassString();
        ReportGridByCategory(StrCondition, i);
        SearchCategory = TxtSearchCategory.Text;
        SearchSubCategory = TxtSearchSubCategory.Text;
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
        SearchCategory = TxtSearchCategory.Text;
        SearchSubCategory = TxtSearchSubCategory.Text;
        TxtSearch.Focus();
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        i = PassString();

        ReportGridByCategory(StrCondition, i);
        GrdReport.Focus();




         





    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //DataSet DsTemp = new DataSet();
            //int UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
            //DsTemp = Obj_ItemMaster.GetUnitConversionDtls(UnitId,0,0,out StrError);
            //if (DsTemp.Tables.Count > 0 && DsTemp.Tables[0].Rows.Count > 0)
            //{
            //    TR_UnitConversion.Visible = true;
            //    Tr_hyl_Hide.Visible = true;
            //    GrdUnitConversion.DataSource = DsTemp.Tables[0];
            //    GrdUnitConversion.DataBind();
            //}
            //else
            //{
            //    TR_UnitConversion.Visible = false;
            //    Tr_hyl_Hide.Visible = false;
            //    GrdUnitConversion.DataSource = null;
            //    GrdUnitConversion.DataBind();
            //}
            DDLMAINUNIT.SelectedValue = ddlUnit.SelectedValue;
            DDLSUBUNIT.SelectedValue = ddlUnit.SelectedValue;
            ddlUnit.Focus();
        }
        catch (Exception ex)
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
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet DSFILTER = new DataSet();
            DMFILTER Obj_Filter = new DMFILTER();
            String FilterData = string.Empty;
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                FilterData = " and F.CategoryId=" + ddlCategory.SelectedValue.ToString();
            }
            DSFILTER = Obj_Filter.FillReportComboCategoryWise(3, 1, FilterData, out StrError);
            if (DSFILTER.Tables.Count > 0)
            {
                if (DSFILTER.Tables[0].Rows.Count > 0)
                {
                    ddlSubCategory.DataSource = DSFILTER.Tables[0];
                    ddlSubCategory.DataTextField = "Name";
                    ddlSubCategory.DataValueField = "ID";
                    ddlSubCategory.DataBind();
                }
            }
            ddlCategory.Focus();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (ViewState["ITEMINUSEORNOT"] != null)
        //    {

        for (int rowIndex = GridDetails.Rows.Count - 1; rowIndex >= 0; rowIndex--)
        {
            GridViewRow gvRow = GridDetails.Rows[rowIndex];
            ImageButton ImageBtnDelete = (ImageButton)GridDetails.Rows[rowIndex].Cells[0].FindControl("ImageBtnDelete");
            //ImageButton ImageGridEdit = (ImageButton)GridDetails.Rows[rowIndex].Cells[0].FindControl("ImageGridEdit");
            if (ViewState["ITEMINUSEORNOT"] != null)
            {
              //  ImageBtnDelete.Visible = false;
                //ImageGridEdit.Visible = false;
            }
            else
            {
                ImageBtnDelete.Visible = true;
                //ImageGridEdit.Visible = true;
            }
        }
    }
    protected void ddlsize_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLMAINUNIT.SelectedValue = ddlsize.SelectedValue;
    }

    protected void PopUpYesNo_Command(object sender, CommandEventArgs e)
    {
        REP = 0;
        if (e.CommandName == "yes")
        {
            //REP = 1;
            GETDATAINGRID();
            lblFileup.Text = "";
            txtDrawingNo.Text = "";
        }
        else
        {
            //REP = 0;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fUpload.HasFile)
        {
            fileName = fUpload.FileName;
            ViewState["fileName"] = fUpload.FileName;
            string exten = Path.GetExtension(fileName);
            //here we have to restrict file type            
            exten = exten.ToLower();
            string[] acceptedFileTypes = new string[5];
            acceptedFileTypes[0] = ".jpg";
            acceptedFileTypes[1] = ".jpeg";
            acceptedFileTypes[2] = ".gif";
            acceptedFileTypes[3] = ".png";
            acceptedFileTypes[4] = ".pdf";
            bool acceptFile = false;
            for (int i = 0; i <= 4; i++)
            {
                if (exten == acceptedFileTypes[i])
                {
                    acceptFile = true;
                }
            }
            if (!acceptFile)
            {
                //lblMsg.Text = "The file you are trying to upload is not a permitted file type!";
            }
            else
            {

                //upload the file onto the server                   
                fUpload.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                lblFileup.Text = fileName + " uploaded..";
            }
        }
        //now need to add file to the database
    }
    protected void ImgBtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/CrystalPrint/PrintCryRpt.aspx?ACTION=1&&Flag=IMP&SC=" + TxtSearchCategory.Text + "&SSC=" + TxtSearchSubCategory.Text + "&SI=" + TxtSearch.Text);
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg("The Following Error Occure While Generating PDF\n" + ex.Message, this.Page);
        }
    }
    protected void ImgBtnPrintSubCategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/CrystalPrint/PrintCryRpt.aspx?ACTION=1&Flag=IMP&SC=" + TxtSearchCategory.Text + "&SSC=" + TxtSearchSubCategory.Text + "&SI=" + TxtSearch.Text);
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg("The Following Error Occure While Generating PDF\n" + ex.Message, this.Page);
        }
    }
    protected void ImgBtnPrintItem_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/CrystalPrint/PrintCryRpt.aspx?ACTION=2&Flag=IMP&SC=" + TxtSearchCategory.Text + "&SSC=" + TxtSearchSubCategory.Text + "&SI=" + TxtSearch.Text);
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg("The Following Error Occure While Generating PDF\n" + ex.Message, this.Page);
        }
    }
    protected void DDLTAXTEMPLATE_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(DDLTAXTEMPLATE.SelectedValue.ToString()) > 0)
            {
                DataSet DSTAXTEMPLATE = new DataSet();
                DSTAXTEMPLATE = Obj_ItemMaster.FillComboForTaxTemplate(Convert.ToInt32(DDLTAXTEMPLATE.SelectedValue.ToString()), out StrError);
                if (DSTAXTEMPLATE.Tables.Count > 0 && DSTAXTEMPLATE.Tables[0].Rows.Count > 0)
                {
                   // TxtTaxPer.Text = DSTAXTEMPLATE.Tables[0].Rows[0]["GST"].ToString();

                    ddlGSTPer.DataSource = DSTAXTEMPLATE.Tables[0];
                    ddlGSTPer.DataTextField = "GST";
                    ddlGSTPer.DataValueField = "TaxDtlsId";
                    ddlGSTPer.DataBind();
                }
            }
            else
            {
                TxtTaxPer.Text = "0.00";
            }
            DDLTAXTEMPLATE.Focus();
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg("The Following Error Occure While Fetching Data\n" + ex.Message, this.Page);
        }
    }
}
