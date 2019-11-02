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
using System.Globalization;

public partial class Transactions_MaterialInwardRegister1 : System.Web.UI.Page
{
    #region[Variables]
        DMMaterialInwardReg Obj_MaterialInwardReg = new DMMaterialInwardReg();
        MaterialInwardReg Entity_MaterialInwardReg = new MaterialInwardReg();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        DataSet DSITEMCATEGORY = new DataSet();
    database db = new database();
        DataTable DSTABLE = new DataTable();
        DataTable DtEditPO = new DataTable();
        private static DataTable dtLoc = new DataTable();
        private static DataTable dtUnit = new DataTable();
        decimal NetAmt = 0;
        string InwardNo = string.Empty;
        private bool flag = false;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
        decimal TaxPer=0, TaxAmt=0, DisPer=0, DisAmt=0;
        ArrayList Al = new ArrayList();
    #endregion

    #region[UserDefineFunctions]

    DataTable GetDataTable(GridView dtg)
        {
        try
        {
        int k = 0;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["CurrentTable"];
        //int r1 = dt.Rows.Count;
        //for (int r = 0; r < r1; r++)
        //{
        //    dt.Rows.RemoveAt(r);
        //}  
        while (dt.Rows.Count > 0)
        {
        dt.Rows.RemoveAt(0);
        }


        //  add each of the data rows to the table
        foreach (GridViewRow row in dtg.Rows)
        {
            if (Convert.ToInt32(GrdInwardPO.Rows[k].Cells[2].Text) > 0)
            {
                if (dt.Rows.Count == 0)
                {
                    DataRow dr;
                    dr = dt.NewRow();
                    dr["#"] = (GrdInwardPO.Rows[k].Cells[0].Text.Equals("&nbsp;")) ? 0 : Convert.ToInt32(GrdInwardPO.Rows[k].Cells[0].Text);
                    dr["ItemId"] = Convert.ToInt32(GrdInwardPO.Rows[k].Cells[2].Text);
                    dr["ItemCode"] = Convert.ToString(GrdInwardPO.Rows[k].Cells[2].Text);
                    dr["Item"] = Convert.ToString(GrdInwardPO.Rows[k].Cells[3].Text).Replace("amp;","");
                    dr["OrderQty"] = Convert.ToDecimal(GrdInwardPO.Rows[k].Cells[4].Text);
                    dr["InwardQty"] = (((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtInwardQty")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtInwardQty")).Text);
                    dr["PendingQty"] = !string.IsNullOrEmpty((((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtPendingQty")).Text).ToString()) ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtPendingQty")).Text);
                    dr["PORate"] = ((GrdInwardPO.Rows[k].Cells[9].Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(GrdInwardPO.Rows[k].Cells[9].Text);
                    dr["InwardRate"] = (((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtInwardRate")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtInwardRate")).Text);
                    dr["Diff"] = ((((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtDifference")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtDifference")).Text);
                    dr["Amount"] = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtAmount")).Text);
                    dr["TaxPer"] = ((((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtTax")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtTax")).Text);
                    dr["TaxAmount"] = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtTaxAmnt")).Text);
                    dr["DiscPer"] = ((((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtDiscPer")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtDiscPer")).Text);
                    dr["DiscAmt"] = ((((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtDiscAmt")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtDiscAmt")).Text);
                    dr["NetAmount"] = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtNetAmnt")).Text);
                    dr["ExpDelDate"] = Convert.ToString(((TextBox)GrdInwardPO.Rows[k].FindControl("txtExpDelDate")).Text);
                    dr["ActDelDate"] = Convert.ToString(((TextBox)GrdInwardPO.Rows[k].FindControl("txtActDelDate")).Text);
                    dr["UnitId"] = Convert.ToInt32(GrdInwardPO.Rows[k].Cells[20].Text);
                    dr["SuplierId"] = Convert.ToInt32(ddlSuplier.SelectedValue);
                    dr["LocationID"] = Convert.ToString(((DropDownList)GrdInwardPO.Rows[k].FindControl("ddlLocation")).Text);
                    dr["LocID"] = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("lblLocID")).Text);
                    dt.Rows.Add(dr);
                    k++;
                }
                else if ((GrdInwardPO.Rows[k].Cells[3].Text.Equals("&nbsp;")))
                {

                }
                else
                {
                    DataRow dr;
                    dr = dt.NewRow();
                    dr["#"] = (GrdInwardPO.Rows[k].Cells[0].Text.Equals("&nbsp;")) ? 0 : Convert.ToInt32(GrdInwardPO.Rows[k].Cells[0].Text);
                    dr["ItemId"] = Convert.ToInt32(GrdInwardPO.Rows[k].Cells[2].Text);
                    dr["ItemCode"] = Convert.ToString(GrdInwardPO.Rows[k].Cells[2].Text);
                    dr["Item"] = Convert.ToString(GrdInwardPO.Rows[k].Cells[3].Text).Replace("amp;", "");
                    dr["OrderQty"] = !string.IsNullOrEmpty((((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtPendingQty")).Text).ToString()) ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtPendingQty")).Text);
                    dr["InwardQty"] = (((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtInwardQty")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtInwardQty")).Text);
                    dr["PendingQty"] = !string.IsNullOrEmpty((((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtPendingQty")).Text).ToString()) ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtPendingQty")).Text);
                    dr["PORate"] = ((GrdInwardPO.Rows[k].Cells[14].Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(GrdInwardPO.Rows[k].Cells[14].Text);
                    dr["InwardRate"] = (((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtInwardRate")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtInwardRate")).Text);
                    dr["Diff"] = ((((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtDifference")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtDifference")).Text);
                    dr["Amount"] = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtAmount")).Text);
                    dr["TaxPer"] = ((((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtTax")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtTax")).Text);
                    dr["TaxAmount"] = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtTaxAmnt")).Text);
                    dr["DiscPer"] = ((((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtDiscPer")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtDiscPer")).Text);
                    dr["DiscAmt"] = ((((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtDiscAmt")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtDiscAmt")).Text);
                    dr["NetAmount"] = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("GrdtxtNetAmnt")).Text);
                    dr["ExpDelDate"] = Convert.ToString(((TextBox)GrdInwardPO.Rows[k].FindControl("txtExpDelDate")).Text);
                    dr["ActDelDate"] = Convert.ToString(((TextBox)GrdInwardPO.Rows[k].FindControl("txtActDelDate")).Text);
                    dr["UnitId"] = Convert.ToInt32(GrdInwardPO.Rows[k].Cells[25].Text);
                    dr["SuplierId"] = Convert.ToInt32(ddlSuplier.SelectedValue);
                    dr["LocationID"] = Convert.ToString(((DropDownList)GrdInwardPO.Rows[k].FindControl("ddlLocation")).Text);
                    dr["LocID"] = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[k].FindControl("lblLocID")).Text);
                    dt.Rows.Add(dr);
                    k++;
                }
            }
        }
        ViewState["CurrentTable"] = dt;
        return dt;
        }
        catch (Exception ex)
        {
        throw new Exception(ex.Message);
        }
        }

    public void BindItemToGrid(DataSet ds)  //For Binding To Grid One By One
    {
        try
        {
            for (int ri = 0; ri < ds.Tables[0].Rows.Count; ri++)//ds.Tables[0].Rows.Count
            {
                #region[GET ENTRY]-----------------------------------------
                if (ViewState["CurrentTable"] != null)
                {
                    bool DupFlag = false;
                    int k = 0;
                    DataTable DTTABLE = (DataTable)ViewState["CurrentTable"];
                    DataRow DTROW = null;
                    if (DTTABLE.Rows.Count > 0)
                    {
                        if (DTTABLE.Rows.Count > 0)
                        {

                            if (ViewState["GridIndex"] == null)
                            {

                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)//ds.Tables[0].Rows.Count
                                {
                                    int ItemID = 0;
                                    string ItemDescID = "";
                                    if (DTTABLE.Rows.Count > i)
                                    {
                                        ItemID = Convert.ToInt32(DTTABLE.Rows[i]["#"]);
                                        ItemDescID = Convert.ToString(DTTABLE.Rows[i]["ItemDesc"]);
                                    }
                                    if (DTTABLE.Rows.Count == 1 && (DTTABLE.Rows[0]["#"].ToString()) == "0")
                                    {
                                        DTTABLE.Rows.RemoveAt(0);
                                    }
                                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["#"]) == Convert.ToInt32(ItemID)
                                        && Convert.ToString(ds.Tables[0].Rows[i]["ItemDesc"]) == Convert.ToString(ItemDescID))
                                    {
                                        DupFlag = true;
                                        k = i;
                                    }

                                    if (DupFlag == true)
                                    {

                                        DTTABLE.Rows[k]["#"] = Convert.ToInt32(DTTABLE.Rows[k]["#"]);
                                        DTTABLE.Rows[k]["ItemId"] = Convert.ToInt32(DTTABLE.Rows[k]["ItemId"]);
                                        DTTABLE.Rows[k]["CategoryName"] = Convert.ToString(DTTABLE.Rows[k]["CategoryName"]);
                                        DTTABLE.Rows[k]["SubCategory"] = Convert.ToString(DTTABLE.Rows[k]["SubCategory"]);
                                        DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(DTTABLE.Rows[k]["ItemCode"]);
                                        DTTABLE.Rows[k]["Item"] = Convert.ToString(DTTABLE.Rows[k]["Item"]);
                                        DTTABLE.Rows[k]["ItemDesc"] = Convert.ToString(DTTABLE.Rows[k]["ItemDesc"]);
                                        DTTABLE.Rows[k]["OrderQty"] = (DTTABLE.Rows[k]["OrderQty"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["OrderQty"]);
                                        DTTABLE.Rows[k]["InwardQty"] = (DTTABLE.Rows[k]["InwardQty"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["InwardQty"]);
                                        DTTABLE.Rows[k]["PendingQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["PendingQty"]);
                                        DTTABLE.Rows[k]["PORate"] = Convert.ToDecimal(DTTABLE.Rows[k]["PORate"]);
                                        DTTABLE.Rows[k]["InwardRate"] = (DTTABLE.Rows[k]["InwardRate"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["InwardRate"]);
                                        DTTABLE.Rows[k]["Diff"] = Convert.ToDecimal(DTTABLE.Rows[k]["Diff"]);
                                        DTTABLE.Rows[k]["Amount"] = Convert.ToDecimal(DTTABLE.Rows[k]["Amount"]);
                                        DTTABLE.Rows[k]["TaxPer"] = (DTTABLE.Rows[k]["TaxPer"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["TaxPer"]);
                                        DTTABLE.Rows[k]["TaxAmount"] = (DTTABLE.Rows[k]["TaxAmount"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["TaxAmount"]);
                                        DTTABLE.Rows[k]["DiscPer"] = (DTTABLE.Rows[k]["DiscPer"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["DiscPer"]);
                                        DTTABLE.Rows[k]["DiscAmt"] = (DTTABLE.Rows[k]["DiscAmt"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["DiscAmt"]);
                                        DTTABLE.Rows[k]["NetAmount"] = Convert.ToDecimal(DTTABLE.Rows[k]["NetAmount"]);
                                        DTTABLE.Rows[k]["ExpDelDate"] = Convert.ToString(DTTABLE.Rows[k]["ExpDelDate"]);
                                        DTTABLE.Rows[k]["ActDelDate"] = Convert.ToString(DTTABLE.Rows[k]["ActDelDate"]);
                                        DTTABLE.Rows[k]["UnitId"] = Convert.ToInt32(DTTABLE.Rows[k]["UnitId"]);
                                        DTTABLE.Rows[k]["SuplierId"] = Convert.ToInt32(DTTABLE.Rows[k]["SuplierId"]);
                                        DTTABLE.Rows[k]["LocationID"] = Convert.ToInt32(DTTABLE.Rows[k]["LocationID"]);
                                        DTTABLE.Rows[k]["LocID"] = Convert.ToDecimal(DTTABLE.Rows[k]["LocID"]);
                                        ViewState["CurrentTable"] = DTTABLE;
                                        GrdInwardPO.DataSource = DTTABLE;
                                        GrdInwardPO.DataBind();
                                        DtEditPO = (DataTable)ViewState["CurrentTable"];
                                    }
                                    else
                                    {
                                        DTROW = DTTABLE.NewRow();
                                        DTROW["#"] = Convert.ToInt32(ds.Tables[0].Rows[k]["#"]);
                                        DTROW["ItemId"] = Convert.ToInt32(ds.Tables[0].Rows[i]["ItemId"]);
                                        DTROW["CategoryName"] = Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                                        DTROW["SubCategory"] = Convert.ToString(ds.Tables[0].Rows[i]["SubCategory"]);
                                        DTROW["ItemDesc"] = Convert.ToString(ds.Tables[0].Rows[i]["ItemDesc"]);
                                        DTROW["ItemCode"] = Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"]);
                                        DTROW["Item"] = Convert.ToString(ds.Tables[0].Rows[i]["Item"]);
                                        DTROW["OrderQty"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderQty"]);
                                        DTROW["InwardQty"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["InwardQty"]);
                                        DTROW["PendingQty"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["PendingQty"]);
                                        DTROW["PORate"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["PORate"]);
                                        DTROW["InwardRate"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["InwardRate"]);
                                        DTROW["Diff"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["Diff"]);
                                        DTROW["Amount"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["Amount"]);
                                        DTROW["TaxPer"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxPer"]);
                                        DTROW["TaxAmount"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]);
                                        DTROW["DiscPer"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["DiscPer"]);
                                        DTROW["DiscAmt"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["DiscAmt"]);
                                        DTROW["NetAmount"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["NetAmount"]);
                                        DTROW["ExpDelDate"] = Convert.ToString(ds.Tables[0].Rows[i]["ExpDelDate"]);
                                        DTROW["ActDelDate"] = Convert.ToString(ds.Tables[0].Rows[i]["ActDelDate"]);
                                        DTROW["UnitId"] = Convert.ToInt32(ds.Tables[0].Rows[i]["UnitId"]);//
                                        DTROW["SuplierId"] = Convert.ToInt32(ddlSuplier.SelectedValue); //Convert.ToInt32(ds.Tables[0].Rows[i]["SuplierId"]);
                                        DTROW["LocationID"] = Convert.ToString(ds.Tables[0].Rows[i]["LocationID"]);
                                        DTROW["LocID"] = Convert.ToString(ds.Tables[0].Rows[i]["LocID"]);
                                        DTTABLE.Rows.Add(DTROW);
                                        ViewState["CurrentTable"] = DTTABLE;
                                        GrdInwardPO.DataSource = DTTABLE;
                                        GrdInwardPO.DataBind();
                                        DtEditPO = (DataTable)ViewState["CurrentTable"];
                                        ViewState["GridIndex"] = k++;
                                    }
                                }
                            }
                            else
                            {
                                for (int ei = 0; ei < ds.Tables[0].Rows.Count; ei++)
                                {
                                    for (int i = 0; i < DTTABLE.Rows.Count; i++)
                                    {
                                        int ItemID = Convert.ToInt32(DTTABLE.Rows[i]["#"]);
                                        string ItemDescID = Convert.ToString(DTTABLE.Rows[i]["ItemDesc"]);
                                        if (Convert.ToInt32(ds.Tables[0].Rows[ei]["#"]) == Convert.ToInt32(ItemID)
                                            && Convert.ToString(ds.Tables[0].Rows[ei]["ItemDesc"]) == Convert.ToString(ItemDescID))//dttable.rows[][]
                                        {
                                            DupFlag = true;
                                            k = i;
                                        }
                                    }


                                    //For Making Current DTTABLE
                                    // GetDataTable(GrdInwardPO);
                                    if (DupFlag == true)
                                    {
                                        int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                                        DTTABLE.Rows[k]["#"] = Convert.ToInt32(DTTABLE.Rows[k]["#"]);
                                        DTTABLE.Rows[k]["ItemId"] = Convert.ToInt32(DTTABLE.Rows[k]["ItemId"]);
                                        DTTABLE.Rows[k]["CategoryName"] = Convert.ToString(DTTABLE.Rows[k]["CategoryName"]);
                                        DTTABLE.Rows[k]["SubCategory"] = Convert.ToString(DTTABLE.Rows[k]["SubCategory"]);
                                        DTTABLE.Rows[k]["ItemDesc"] = Convert.ToString(DTTABLE.Rows[k]["ItemDesc"]);
                                        DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(DTTABLE.Rows[k]["ItemCode"]);
                                        DTTABLE.Rows[k]["Item"] = Convert.ToString(DTTABLE.Rows[k]["Item"]);
                                        DTTABLE.Rows[k]["OrderQty"] = (DTTABLE.Rows[k]["OrderQty"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["OrderQty"]);
                                        DTTABLE.Rows[k]["InwardQty"] = (DTTABLE.Rows[k]["InwardQty"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["InwardQty"]);
                                        DTTABLE.Rows[k]["PendingQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["PendingQty"]);
                                        DTTABLE.Rows[k]["PORate"] = Convert.ToDecimal(DTTABLE.Rows[k]["PORate"]);
                                        DTTABLE.Rows[k]["InwardRate"] = (DTTABLE.Rows[k]["InwardRate"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["InwardRate"]);
                                        DTTABLE.Rows[k]["Diff"] = Convert.ToDecimal(DTTABLE.Rows[k]["Diff"]);
                                        DTTABLE.Rows[k]["Amount"] = Convert.ToDecimal(DTTABLE.Rows[k]["Amount"]);
                                        DTTABLE.Rows[k]["TaxPer"] = (DTTABLE.Rows[k]["TaxPer"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["TaxPer"]);
                                        DTTABLE.Rows[k]["TaxAmount"] = (DTTABLE.Rows[k]["TaxAmount"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["TaxAmount"]);
                                        DTTABLE.Rows[k]["DiscPer"] = (DTTABLE.Rows[k]["DiscPer"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["DiscPer"]);
                                        DTTABLE.Rows[k]["DiscAmt"] = (DTTABLE.Rows[k]["DiscAmt"].ToString()).Equals("") ? 0 : Convert.ToDecimal(DTTABLE.Rows[k]["DiscAmt"]);
                                        DTTABLE.Rows[k]["NetAmount"] = Convert.ToDecimal(DTTABLE.Rows[k]["NetAmount"]);
                                        DTTABLE.Rows[k]["ExpDelDate"] = Convert.ToString(DTTABLE.Rows[k]["ExpDelDate"]);
                                        DTTABLE.Rows[k]["ActDelDate"] = Convert.ToString(DTTABLE.Rows[k]["ActDelDate"]);
                                        DTTABLE.Rows[k]["UnitId"] = Convert.ToInt32(DTTABLE.Rows[k]["UnitId"]);
                                        DTTABLE.Rows[k]["SuplierId"] = Convert.ToInt32(DTTABLE.Rows[k]["SuplierId"]);
                                        DTTABLE.Rows[k]["LocationID"] = Convert.ToInt32(DTTABLE.Rows[k]["LocationID"]);
                                        DTTABLE.Rows[k]["LocID"] = Convert.ToDecimal(DTTABLE.Rows[k]["LocID"]);
                                        ViewState["CurrentTable"] = DTTABLE;
                                        GrdInwardPO.DataSource = DTTABLE;
                                        GrdInwardPO.DataBind();
                                        DtEditPO = (DataTable)ViewState["CurrentTable"];
                                    }
                                    else
                                    {
                                        DTROW = DTTABLE.NewRow();
                                        DTROW["#"] = Convert.ToInt32(ds.Tables[0].Rows[ei]["#"]);
                                        DTROW["ItemId"] = Convert.ToInt32(ds.Tables[0].Rows[ei]["ItemId"]);
                                        DTROW["CategoryName"] = Convert.ToString(ds.Tables[0].Rows[ei]["CategoryName"]);
                                        DTROW["SubCategory"] = Convert.ToString(ds.Tables[0].Rows[ei]["SubCategory"]);
                                        DTROW["ItemDesc"] = Convert.ToString(ds.Tables[0].Rows[ei]["ItemDesc"]);
                                        DTROW["ItemCode"] = Convert.ToString(ds.Tables[0].Rows[ei]["ItemCode"]);
                                        DTROW["Item"] = Convert.ToString(ds.Tables[0].Rows[ei]["Item"]);
                                        DTROW["OrderQty"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["OrderQty"]);
                                        DTROW["InwardQty"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["InwardQty"]);
                                        DTROW["PendingQty"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["PendingQty"]);
                                        DTROW["PORate"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["PORate"]);
                                        DTROW["InwardRate"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["InwardRate"]);
                                        DTROW["Diff"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["Diff"]);
                                        DTROW["Amount"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["Amount"]);
                                        DTROW["TaxPer"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["TaxPer"]);
                                        DTROW["TaxAmount"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["TaxAmount"]);
                                        DTROW["DiscPer"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["DiscPer"]);
                                        DTROW["DiscAmt"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["DiscAmt"]);
                                        DTROW["NetAmount"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["NetAmount"]);
                                        DTROW["ExpDelDate"] = Convert.ToString(ds.Tables[0].Rows[ei]["ExpDelDate"]);
                                        DTROW["ActDelDate"] = Convert.ToString(ds.Tables[0].Rows[ei]["ActDelDate"]);
                                        DTROW["UnitId"] = Convert.ToInt32(ds.Tables[0].Rows[ei]["UnitId"]);
                                        DTROW["SuplierId"] = Convert.ToInt32(ddlSuplier.SelectedValue);// Convert.ToInt32(ds.Tables[0].Rows[ei]["SuplierId"]);  
                                        DTROW["LocationID"] = Convert.ToString(ds.Tables[0].Rows[ei]["LocationID"]);
                                        DTROW["LocID"] = Convert.ToDecimal(ds.Tables[0].Rows[ei]["LocID"]);
                                        DTTABLE.Rows.Add(DTROW);
                                        ViewState["CurrentTable"] = DTTABLE;
                                        GrdInwardPO.DataSource = DTTABLE;
                                        GrdInwardPO.DataBind();
                                        DtEditPO = (DataTable)ViewState["CurrentTable"];
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion------------------------------------------
            }
        }

        catch (Exception)
        {
            throw;
        }

    }

    public void BindItemToGridAll(DataSet ds)  //For Binding To Grid All
        {
        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
        {
        int ItemID_All = Convert.ToInt32(ds.Tables[0].Rows[j]["ItemId"]);
        if (ViewState["CurrentTable"] != null)
        {
        bool DupFlag = false;
        int k = 0;
        DataTable DTTABLE = (DataTable)ViewState["CurrentTable"];
        DataRow DTROW = null;
        if (DTTABLE.Rows.Count > 0)
        {                        
        if (ViewState["GridIndex"] == null)
        {
        for (int i = 0; i < DTTABLE.Rows.Count; i++)
        {
        int ItemID_Single = Convert.ToInt32(DTTABLE.Rows[i]["ItemId"]);
        if (DTTABLE.Rows.Count == 1 && (DTTABLE.Rows[0]["ItemId"].ToString()) == "0")
        {
        DTTABLE.Rows.RemoveAt(0);
        }
        if (Convert.ToInt32(ItemID_Single) == Convert.ToInt32(ItemID_All))
        {
        DupFlag = true;
        k = i;
        }
        }
        if (DupFlag == true)
        {
        DTTABLE.Rows[k]["#"] = Convert.ToInt32(DTTABLE.Rows[k]["#"]);
        DTTABLE.Rows[k]["ItemId"] = Convert.ToInt32(DTTABLE.Rows[k]["ItemId"]);
        DTTABLE.Rows[k]["CategoryName"] = Convert.ToString(DTTABLE.Rows[k]["CategoryName"]);
        DTTABLE.Rows[k]["SubCategory"] = Convert.ToString(DTTABLE.Rows[k]["SubCategory"]);
        DTTABLE.Rows[k]["ItemDesc"] = Convert.ToString(DTTABLE.Rows[k]["ItemDesc"]);
        DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(DTTABLE.Rows[k]["ItemCode"]);
        DTTABLE.Rows[k]["Item"] = Convert.ToString(DTTABLE.Rows[k]["Item"]);
        DTTABLE.Rows[k]["OrderQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["OrderQty"]);
        DTTABLE.Rows[k]["InwardQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["InwardQty"]);
        DTTABLE.Rows[k]["PendingQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["PendingQty"]);
        DTTABLE.Rows[k]["PORate"] = Convert.ToDecimal(DTTABLE.Rows[k]["PORate"]);
        DTTABLE.Rows[k]["InwardRate"] = Convert.ToDecimal(DTTABLE.Rows[k]["InwardRate"]);
        DTTABLE.Rows[k]["Diff"] = Convert.ToDecimal(DTTABLE.Rows[k]["Diff"]);
        DTTABLE.Rows[k]["Amount"] = Convert.ToDecimal(DTTABLE.Rows[k]["Amount"]);
        DTTABLE.Rows[k]["TaxPer"] = Convert.ToDecimal(DTTABLE.Rows[k]["TaxPer"]);
        DTTABLE.Rows[k]["TaxAmount"] = Convert.ToDecimal(DTTABLE.Rows[k]["TaxAmount"]);
        DTTABLE.Rows[k]["DiscPer"] = Convert.ToDecimal(DTTABLE.Rows[k]["DiscPer"]);
        DTTABLE.Rows[k]["DiscAmt"] = Convert.ToDecimal(DTTABLE.Rows[k]["DiscAmt"]);
        DTTABLE.Rows[k]["NetAmount"] = Convert.ToDecimal(DTTABLE.Rows[k]["NetAmount"]);
        DTTABLE.Rows[k]["ExpDelDate"] = Convert.ToString(DTTABLE.Rows[k]["ExpDelDate"]);
        DTTABLE.Rows[k]["ActDelDate"] = Convert.ToString(DTTABLE.Rows[k]["ActDelDate"]);
        DTTABLE.Rows[k]["UnitId"] = Convert.ToInt32(DTTABLE.Rows[k]["UnitId"]);
        DTTABLE.Rows[k]["SuplierId"] = Convert.ToInt32(DTTABLE.Rows[k]["SuplierId"]);
        DTTABLE.Rows[k]["LocationID"] = Convert.ToInt32(DTTABLE.Rows[k]["LocationID"]);
        DTTABLE.Rows[k]["LocID"] = Convert.ToString(DTTABLE.Rows[k]["LocID"]); 
        ViewState["CurrentTable"] = DTTABLE;
        GrdInwardPO.DataSource = DTTABLE;
        GrdInwardPO.DataBind();
        DtEditPO = (DataTable)ViewState["CurrentTable"];
        }
        else
        {
        DTROW = DTTABLE.NewRow();
        DTROW["#"] = Convert.ToInt32(ds.Tables[0].Rows[j]["#"]);
        DTROW["ItemId"] = Convert.ToInt32(ds.Tables[0].Rows[j]["ItemId"]);
        DTROW["CategoryName"] = Convert.ToString(ds.Tables[0].Rows[j]["CategoryName"]);
        DTROW["SubCategory"] = Convert.ToString(ds.Tables[0].Rows[j]["SubCategory"]);
        DTROW["ItemDesc"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemDesc"]);
        DTROW["ItemCode"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemCode"]);
        DTROW["Item"] = Convert.ToString(ds.Tables[0].Rows[j]["Item"]);
        DTROW["OrderQty"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["OrderQty"]);
        DTROW["InwardQty"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["InwardQty"]);
        DTROW["PendingQty"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["PendingQty"]);
        DTROW["PORate"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["PORate"]);
        DTROW["InwardRate"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["InwardRate"]);
        DTROW["Diff"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["Diff"]);
        DTROW["Amount"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["Amount"]);
        DTROW["TaxPer"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["TaxPer"]);
        DTROW["TaxAmount"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["TaxAmount"]);
        DTROW["DiscPer"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["DiscPer"]);
        DTROW["DiscAmt"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["DiscAmt"]);
        DTROW["NetAmount"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["NetAmount"]);
        DTROW["ExpDelDate"] = Convert.ToString(ds.Tables[0].Rows[j]["ExpDelDate"]);
        DTROW["ActDelDate"] = Convert.ToString(ds.Tables[0].Rows[j]["ActDelDate"]);
        DTROW["UnitId"] = Convert.ToInt32(ds.Tables[0].Rows[j]["UnitId"]);
        DTROW["SuplierId"] = Convert.ToInt32(ds.Tables[0].Rows[j]["SuplierId"]);
        DTROW["LocationID"] = Convert.ToInt32(ds.Tables[0].Rows[j]["LocationID"]);
        DTROW["LocID"] = Convert.ToString(ds.Tables[0].Rows[j]["LocID"]);
        DTTABLE.Rows.Add(DTROW);
        ViewState["CurrentTable"] = DTTABLE;
        GrdInwardPO.DataSource = DTTABLE;
        GrdInwardPO.DataBind();
        DtEditPO = (DataTable)ViewState["CurrentTable"];
        int g = k;
        ViewState["GridIndex"] = g++;

        }
        }
        else
        {
        for (int i = 0; i < DTTABLE.Rows.Count; i++)
        {
        int ItemID_Single = Convert.ToInt32(DTTABLE.Rows[i]["ItemId"]);
        if (Convert.ToInt32(ItemID_Single) == Convert.ToInt32(ItemID_All))
        {
        DupFlag = true;
        k = i;
        }
        }
        if (DupFlag == true)
        {
        int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
        DTTABLE.Rows[k]["#"] = Convert.ToInt32(DTTABLE.Rows[k]["#"]);
        DTTABLE.Rows[k]["ItemId"] = Convert.ToInt32(DTTABLE.Rows[k]["ItemId"]);
        DTTABLE.Rows[k]["CategoryName"] = Convert.ToString(DTTABLE.Rows[k]["CategoryName"]);
        DTTABLE.Rows[k]["SubCategory"] = Convert.ToString(DTTABLE.Rows[k]["SubCategory"]);
        DTTABLE.Rows[k]["ItemDesc"] = Convert.ToString(DTTABLE.Rows[k]["ItemDesc"]);
        DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(DTTABLE.Rows[k]["ItemCode"]);
        DTTABLE.Rows[k]["Item"] = Convert.ToString(DTTABLE.Rows[k]["Item"]);
        DTTABLE.Rows[k]["OrderQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["OrderQty"]);
        DTTABLE.Rows[k]["InwardQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["InwardQty"]);
        DTTABLE.Rows[k]["PendingQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["PendingQty"]);
        DTTABLE.Rows[k]["PORate"] = Convert.ToDecimal(DTTABLE.Rows[k]["PORate"]);
        DTTABLE.Rows[k]["InwardRate"] = Convert.ToDecimal(DTTABLE.Rows[k]["InwardRate"]);
        DTTABLE.Rows[k]["Diff"] = Convert.ToDecimal(DTTABLE.Rows[k]["Diff"]);
        DTTABLE.Rows[k]["Amount"] = Convert.ToDecimal(DTTABLE.Rows[k]["Amount"]);
        DTTABLE.Rows[k]["TaxPer"] = Convert.ToDecimal(DTTABLE.Rows[k]["TaxPer"]);
        DTTABLE.Rows[k]["TaxAmount"] = Convert.ToDecimal(DTTABLE.Rows[k]["TaxAmount"]);
        DTTABLE.Rows[k]["DiscPer"] = Convert.ToDecimal(DTTABLE.Rows[k]["DiscPer"]);
        DTTABLE.Rows[k]["DiscAmt"] = Convert.ToDecimal(DTTABLE.Rows[k]["DiscAmt"]);
        DTTABLE.Rows[k]["NetAmount"] = Convert.ToDecimal(DTTABLE.Rows[k]["NetAmount"]);
        DTTABLE.Rows[k]["ExpDelDate"] = Convert.ToString(DTTABLE.Rows[k]["ExpDelDate"]);
        DTTABLE.Rows[k]["ActDelDate"] = Convert.ToString(DTTABLE.Rows[k]["ActDelDate"]);
        DTTABLE.Rows[k]["UnitId"] = Convert.ToInt32(DTTABLE.Rows[k]["UnitId"]);
        DTTABLE.Rows[k]["SuplierId"] = Convert.ToInt32(DTTABLE.Rows[k]["SuplierId"]);
        DTTABLE.Rows[k]["LocationID"] = Convert.ToInt32(DTTABLE.Rows[k]["LocationID"]);
        DTTABLE.Rows[k]["LocID"] = Convert.ToString(DTTABLE.Rows[k]["LocID"]);
        ViewState["CurrentTable"] = DTTABLE;
        GrdInwardPO.DataSource = DTTABLE;
        GrdInwardPO.DataBind();
        DtEditPO = (DataTable)ViewState["CurrentTable"];
        }
        else
        {
        DTROW = DTTABLE.NewRow();
        DTROW["#"] = Convert.ToInt32(ds.Tables[0].Rows[j]["#"]);
        DTROW["ItemId"] = Convert.ToInt32(ds.Tables[0].Rows[j]["ItemId"]);
        DTROW["CategoryName"] = Convert.ToString(ds.Tables[0].Rows[j]["CategoryName"]);
        DTROW["SubCategory"] = Convert.ToString(ds.Tables[0].Rows[j]["SubCategory"]);
        DTROW["ItemDesc"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemDesc"]);
        DTROW["ItemCode"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemCode"]);
        DTROW["Item"] = Convert.ToString(ds.Tables[0].Rows[j]["Item"]);
        DTROW["OrderQty"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["OrderQty"]);
        DTROW["InwardQty"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["InwardQty"]);
        DTROW["PendingQty"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["PendingQty"]);
        DTROW["PORate"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["PORate"]);
        DTROW["InwardRate"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["InwardRate"]);
        DTROW["Diff"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["Diff"]);
        DTROW["Amount"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["Amount"]);
        DTROW["TaxPer"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["TaxPer"]);
        DTROW["TaxAmount"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["TaxAmount"]);
        DTROW["DiscPer"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["DiscPer"]);
        DTROW["DiscAmt"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["DiscAmt"]);
        DTROW["NetAmount"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["NetAmount"]);
        DTROW["ExpDelDate"] = Convert.ToString(ds.Tables[0].Rows[j]["ExpDelDate"]);
        DTROW["ActDelDate"] = Convert.ToString(ds.Tables[0].Rows[j]["ActDelDate"]);
        DTROW["UnitId"] = Convert.ToInt32(ds.Tables[0].Rows[j]["UnitId"]);
        DTROW["SuplierId"] = Convert.ToInt32(ds.Tables[0].Rows[j]["SuplierId"]);
        DTROW["LocationID"] = Convert.ToInt32(ds.Tables[0].Rows[j]["LocationID"]);
        DTROW["LocID"] = Convert.ToString(ds.Tables[0].Rows[j]["LocID"]);
        DTTABLE.Rows.Add(DTROW);
        ViewState["CurrentTable"] = DTTABLE;
        GrdInwardPO.DataSource = DTTABLE;
        GrdInwardPO.DataBind();
        DtEditPO = (DataTable)ViewState["CurrentTable"];
        }
        }
        }
        }
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
                ////Checking Right of users=======
                //{
                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Inward Register'");
                    if (dtRow.Length > 0)
                    {
                        DataTable dt = dtRow.CopyToDataTable();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                    Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false &&
                    Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        Response.Redirect("~/NotAuthUser.aspx");
                    }
                    //Checking View Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                    {
                        ReportGrid.Visible = false;
                    }
                    //Checking Add Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                    {
                        BtnSave.Visible = false;
                        FlagAdd = true;

                    }
                    //Edit /Delete Column Visible ========
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false &&
                    Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        foreach (GridViewRow GRow in ReportGrid.Rows)
                        {
                            GRow.FindControl("ImgBtnDelete").Visible = false;
                            GRow.FindControl("ImageGridEdit").Visible = false;
                            GRow.FindControl("ImgBtnPrint").Visible = false;
                        }
                        //BtnUpdate.Visible = false;
                        FlagDel = true;
                        FlagEdit = true;
                        FlagPrint = true;
                    }
                    else
                    {
                        //Checking Delete Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                        {
                            foreach (GridViewRow GRow in ReportGrid.Rows)
                            {
                                GRow.FindControl("ImgBtnDelete").Visible = false;
                                FlagDel = true;
                            }
                        }

                        //Checking Edit Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                        {
                            foreach (GridViewRow GRow in ReportGrid.Rows)
                            {
                                GRow.FindControl("ImageGridEdit").Visible = false;
                                FlagEdit = true;
                            }
                        }

                        //Checking Print Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                        {
                            foreach (GridViewRow GRow in ReportGrid.Rows)
                            {
                                GRow.FindControl("ImgBtnPrint").Visible = false;
                                FlagPrint = true;
                            }
                        }
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
        catch (ThreadAbortException )
        {
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //User Right Function===========

    private void MakeEmptyForm()
    {
        if (!FlagAdd)
         BtnSave.Visible = true;
        BtnUpdate.Visible = false;

        TxtInwardNo.Enabled = false;
        txtBillDate.Text=TxtInwardDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        rdoInwardType.Enabled = rdoTemplateItem.Enabled = true;
        ddlPONo.Enabled = ImgBtnAddTemplate.Enabled = ImgBtnAddItem.Enabled = ddlTemplate.Enabled = ddlCategory.Enabled = ddlItems.Enabled = ddlSuplier.Enabled = true;
        ddlPONo.SelectedIndex = 0;

        txtSupplierName.Enabled = false;
        txtSupplierName.Text = string.Empty;
        TxtShippingAddress.Text = string.Empty;
        TxtBillingAddress.Text = string.Empty;
        txtInstruction.Text = string.Empty;
        TxtSearch.Text = string.Empty;
        txtst.Text = "0";
        txtVehicle.Text = string.Empty;
        //TimeInSelector.Date = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
        //TimeOutSelector.Date = Convert.ToDateTime(DateTime.Now.ToShortTimeString());

        TimeInSelector.Date = DateTime.Parse(DateTime.Now.ToString());

        // JITU TimeOutSelector.Date = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
        
        //TimeOutSelector.Date = DateTime.Parse(DateTime.Now.ToString());  // as per requirement by bhakti
       
        TimeOutSelector.Date = DateTime.Parse("12:00:00");

        ViewState["EditId"] = null;
        ViewState["flag"] = false;
        ViewState["ID"] = null;
        ViewState["CurrentTable"] = null;
        ViewState["GridIndex"] = null;

        TxtItemName.Text=txtBillNo.Text = string.Empty;
        rdoInwardThrough.SelectedValue = "1";
        
        txtSubTotal.Text = "0";
        txtDiscount.Text = "0";
        txtDiscountPer.Text = "0";
        txtVATAmount.Text = "0";
        txtVATPer.Text = "0";
        txtDekhrekhPer.Text = "0";
        txtDekhrekhAmt.Text = "0";
        txtHamaliPer.Text = "0";
        txtHamaliAmt.Text = "0";
        txtCESSPer.Text = "0";
        txtCESSAmt.Text = "0";
        txtFreightPer.Text = "0";
        txtFreightAmt.Text = "0";
        txtPackingPer.Text = "0";
        txtPackingAmt.Text = "0";
        txtPostagePer.Text = "0";
        txtPostageAmt.Text = "0";
        txtOtherCharges.Text = "0";
        txtNetTotal.Text = "0";
        rdoInwardType.SelectedIndex = 0;
        ddlTemplate.SelectedIndex = ddlCategory.SelectedIndex = ddlItems.SelectedIndex = ddlSuplier.SelectedIndex = 0;
        TRPO.Visible = true;
        TRTemplate.Visible = TRItem.Visible = TRRadio.Visible = false;
        GetInwardNo();
        SetInitialRow();
        SetInitialRow_GrdReqDtls();
        BindReportGrid(StrCondition);

        TR_RateDtls.Visible = false;

        #region[UserRights]
        if (FlagEdit)
        {
            foreach (GridViewRow GRow in ReportGrid.Rows)
            {
                GRow.FindControl("ImageGridEdit").Visible = false;
            }
        }
        if (FlagDel)
        {
            foreach (GridViewRow GRow in ReportGrid.Rows)
            {
                GRow.FindControl("ImgBtnDelete").Visible = false;
            }
        }
        if (FlagPrint)
        {
            foreach (GridViewRow GRow in ReportGrid.Rows)
            {
                GRow.FindControl("ImgBtnPrint").Visible = false;
            }
        }
        #endregion
    }
    
    public void BindReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            DsReport = Obj_MaterialInwardReg.GetReport(RepCondition, out StrError);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                ReportGrid.DataSource = DsReport.Tables[0];
                ReportGrid.DataBind();
            }
            else
            {
                ReportGrid.DataSource = null;
                ReportGrid.DataBind();
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
   
    private void FillCombo()
    {
        try
        {
            Ds = Obj_MaterialInwardReg.FillCombo(out StrError);

            DataTable dt5 = new DataTable();
            
            if (Ds.Tables.Count > 0)
            {

                if (Ds.Tables[5].Rows.Count > 0)
                {
                    dt5 = Ds.Tables[5];
                    ViewState["DSTABLEITEM"] = Ds.Tables[5];
                }

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlPONo.DataSource = Ds.Tables[0];
                    ddlPONo.DataTextField = "PONo";
                    ddlPONo.DataValueField = "POId";
                    ddlPONo.DataBind();
                }
                if (Ds.Tables[1].Rows.Count > 0)
                {
                    ddlTemplate.DataSource = Ds.Tables[1];
                    ddlTemplate.DataValueField = "TemplateID";
                    ddlTemplate.DataTextField = "TemplateName";
                    ddlTemplate.DataBind();
                }
                if (Ds.Tables[2].Rows.Count > 0)
                {
                    ddlCategory.DataSource = Ds.Tables[2];
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataBind();
                }
                if (Ds.Tables[3].Rows.Count > 0)
                {
                    ddlItems.DataSource = Ds.Tables[3];
                    ddlItems.DataValueField = "ItemId";
                    ddlItems.DataTextField = "ItemName";
                    ddlItems.DataBind();

                }
                if (Ds.Tables[4].Rows.Count > 0)
                {
                    ddlSuplier.DataSource = Ds.Tables[4];
                    ddlSuplier.DataValueField = "SuplierId";
                    ddlSuplier.DataTextField = "SuplierName";
                    ddlSuplier.DataBind();
                    ddlSuplierPopUp.DataSource = Ds.Tables[4];
                    ddlSuplierPopUp.DataValueField = "SuplierId";
                    ddlSuplierPopUp.DataTextField = "SuplierName";
                    ddlSuplierPopUp.DataBind();
                }
                if (Ds.Tables[6].Rows.Count > 0)
                {
                    dtLoc = Ds.Tables[6].Copy();
                    Session.Add("INLOCATION", Ds.Tables[6]);
                }

                if (Ds.Tables[7].Rows.Count > 0)
                {
                    dtUnit = Ds.Tables[7].Copy();
                    Session.Add("UNITCONV", Ds.Tables[7]);

                }
               // BindPONo();
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void BindPONo()
    {
        try
        {
            Ds = Obj_MaterialInwardReg.FillComboPONO(out StrError);         
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlPONo.DataSource = Ds.Tables[0];
                    ddlPONo.DataTextField = "PONo";
                    ddlPONo.DataValueField = "POId";
                    ddlPONo.DataBind();
                }                
           }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();

            DataRow dr = null;
            dt.Columns.Add(new DataColumn("#", typeof(int)));
            dt.Columns.Add(new DataColumn("ItemId", typeof(Int32)));
            dt.Columns.Add(new DataColumn("CategoryName", typeof(string)));
            dt.Columns.Add(new DataColumn("SubCategory", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
            dt.Columns.Add(new DataColumn("Item", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
            dt.Columns.Add(new DataColumn("OrderQty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("InwardQty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PendingQty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("InwardRate", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PORate", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Diff", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TaxPer", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TaxAmount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DiscPer", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DiscAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("NetAmount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("ExpDelDate", typeof(string)));
            dt.Columns.Add(new DataColumn("ActDelDate", typeof(string)));
            dt.Columns.Add(new DataColumn("POId",typeof(Int32)));
            dt.Columns.Add(new DataColumn("UnitId", typeof(Int32)));
            dt.Columns.Add(new DataColumn("SuplierId", typeof(Int32)));
            dt.Columns.Add(new DataColumn("LocationID", typeof(Int32)));
            dt.Columns.Add(new DataColumn("LocID", typeof(string)));
            dt.Columns.Add(new DataColumn("PENDINGORDERQTY", typeof(string))); 
     
            dr = dt.NewRow();
            
            dr["#"] = 0;
            dr["ItemId"] = 0;
            dr["CategoryName"] = "";
            dr["SubCategory"] = "";
            dr["ItemCode"] = "";
            dr["Item"] = "";
            dr["ItemDesc"] = "";
            dr["OrderQty"] = 0;
            dr["InwardQty"] = 0;
            dr["PendingQty"] = 0;
            dr["InwardRate"] = 0;
            dr["PORate"] = 0;
            dr["Diff"] = 0.00;
            dr["Amount"] = 0.00;
            dr["TaxPer"] = 0.00;
            dr["TaxAmount"] = 0.00;
            dr["DiscPer"] = 0.00;
            dr["DiscAmt"] = 0.00;
            dr["NetAmount"] = 0.00;
            dr["ExpDelDate"] = System.DateTime.Now.ToString("dd MMM yyy");
            dr["ActDelDate"] = System.DateTime.Now.ToString("dd MMM yyyy");
            dr["POId"] = 0;
            dr["UnitId"] = 0;
            dr["SuplierId"] = 0;
            dr["LocationID"] = 0;
            dr["LocID"] = 0;
            dr["PENDINGORDERQTY"] = "";
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;
            GrdInwardPO.DataSource = dt;
            GrdInwardPO.DataBind();

            dt = null;
            dr = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void SetInitialRow_GrdReqDtls()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("BillDate", typeof(string));
            dt.Columns.Add("InwardNo", typeof(string));
            dt.Columns.Add("SuplierName", typeof(string));
            dt.Columns.Add("InwardRate", typeof(Decimal));

            dr = dt.NewRow();

            dr["BillDate"] = "";
            dr["InwardNo"] = "";
            dr["SuplierName"] = "";
            dr["InwardRate"] = 0;

            dt.Rows.Add(dr);

            ViewState["CurrentTable_RateDtls"] = dt;
            GrdRateDtls.DataSource = dt;
            GrdRateDtls.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void GetInwardNo()
    {
        try
        {
            DataSet DsCode = new DataSet();
            DsCode = Obj_MaterialInwardReg.GetInwardNo(out StrError);
            if (DsCode.Tables.Count>0 &&DsCode.Tables[0].Rows.Count > 0  )
            {
                InwardNo = DsCode.Tables[0].Rows[0]["InwardNo"].ToString();
            }
            TxtInwardNo.Text = InwardNo;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void BindDetails(Int32 POId, string Type)
    {
        DataSet Dschk = new DataSet();

        try
        {
            if (Type=="PO")
            {
                Dschk = Obj_MaterialInwardReg.ChkPoIdExit(POId, out StrError);
                if (Dschk.Tables.Count > 0 && Dschk.Tables[0].Rows.Count > 0)
                {
                    Ds = Obj_MaterialInwardReg.GetDtls_PONowise(POId, out StrError);
                    ViewState["PoidExist"] = true;
                    txtst.Text = "1";
                }
                else
                {
                    Ds = Obj_MaterialInwardReg.GetSupplierDtls_PONowise(POId, out StrError,"P");
                    ViewState["PoidExist"] = false;
                    txtst.Text = "0";
                }
                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    txtSupplierName.Text = Ds.Tables[0].Rows[0]["SuplierName"].ToString();
                    LblSuplierID.Text = Ds.Tables[0].Rows[0]["SuplierId"].ToString();
                    txtSubTotal.Text = Ds.Tables[0].Rows[0]["SubTotal"].ToString();
                    txtDiscount.Text = Ds.Tables[0].Rows[0]["Discount"].ToString();
                    txtVATAmount.Text = Ds.Tables[0].Rows[0]["Vat"].ToString();
                    txtDekhrekhAmt.Text = Ds.Tables[0].Rows[0]["DekhrekhAmt"].ToString();
                    txtHamaliAmt.Text = Ds.Tables[0].Rows[0]["HamaliAmt"].ToString();
                    txtCESSAmt.Text = Ds.Tables[0].Rows[0]["CESSAmt"].ToString();
                    txtFreightAmt.Text = Ds.Tables[0].Rows[0]["FreightAmt"].ToString();
                    txtPackingAmt.Text = Ds.Tables[0].Rows[0]["PackingAmt"].ToString();
                    txtPostageAmt.Text = Ds.Tables[0].Rows[0]["PostageAmt"].ToString();
                    txtOtherCharges.Text = Ds.Tables[0].Rows[0]["OtherCharges"].ToString();
                    txtNetTotal.Text = Ds.Tables[0].Rows[0]["GrandTotal"].ToString();
                }
                else
                {
                    GrdInwardPO.DataSource = null;
                    GrdInwardPO.DataBind();
                }
                if (Ds.Tables.Count > 0 && Ds.Tables[1].Rows.Count > 0)
                {
                    GrdInwardPO.DataSource = Ds.Tables[1];
                    GrdInwardPO.DataBind();
                    ViewState["CurrentTable"] = Ds.Tables[1];

                }
                else
                {
                    GrdInwardPO.DataSource = null;
                    GrdInwardPO.DataBind();

                } 
            }
            //Code Add By Piyush on Dated 23/Jan/2013
            if (Type == "Temp")
            {
                Ds = Obj_MaterialInwardReg.GetSupplierDtls_PONowise(POId, out StrError, "T"); 
                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    BindItemToGridAll(Ds);                    
                }
                else
                {
                    GrdInwardPO.DataSource = null;
                    GrdInwardPO.DataBind();
                }
            }
            if (Type == "ITEM")
            {
                Ds = Obj_MaterialInwardReg.GetSupplierDtls_PONowise(POId, out StrError, "I");
                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    BindItemToGrid(Ds);                    
                }
                else
                {
                    GrdInwardPO.DataSource = null;
                    GrdInwardPO.DataBind();
                }
            }
            // End Code Here
            DataSet DsCUnit = new DataSet();
            if (Type == "PO")
            {
                for (int i = 0; i < GrdInwardPO.Rows.Count; i++)
              
                {
                    int ITEMID = Convert.ToInt32(GrdInwardPO.Rows[i].Cells[2].Text.ToString());
                    int UnitID = Convert.ToInt32(GrdInwardPO.Rows[i].Cells[25].Text.ToString());
                    string ItemDesc =(GrdInwardPO.Rows[i].Cells[7].Text.ToString()).Equals("&nbsp;")?"":Convert.ToString(GrdInwardPO.Rows[i].Cells[7].Text.ToString());
                    decimal Qty = !string.IsNullOrEmpty(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardQty")).Text.ToString()) ? Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardQty")).Text.ToString()) : 0;
                    DsCUnit = Obj_MaterialInwardReg.GetSubUnit(ITEMID,UnitID,Qty,ItemDesc, out StrError);
                    if (DsCUnit.Tables[0].Rows.Count > 0)
                    {
                        AjaxControlToolkit.ComboBox ddlUnit = (AjaxControlToolkit.ComboBox)GrdInwardPO.Rows[i].FindControl("ddlUnit");
                        ddlUnit.DataSource = DsCUnit.Tables[0];
                        ddlUnit.DataTextField = "UnitFactor";
                        ddlUnit.DataValueField = "#";
                        ddlUnit.DataBind();
                        // ddlUnit.SelectedValue = (Convert.ToInt32(dttable1.Rows[e.Row.RowIndex]["UnitConvDtlsId"])).ToString();
                    }
                }
            }
            else
            {
                for (int i = 0; i < GrdInwardPO.Rows.Count; i++)
                //for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                {
                    //int ITEMID = Convert.ToInt32(Ds.Tables[0].Rows[i]["ItemId"].ToString());
                    int ITEMID = Convert.ToInt32(GrdInwardPO.Rows[i].Cells[2].Text.ToString());
                    int UnitID = Convert.ToInt32(GrdInwardPO.Rows[i].Cells[25].Text.ToString());
                    string ItemDesc = (GrdInwardPO.Rows[i].Cells[7].Text.ToString()).Equals("&nbsp;")?"": Convert.ToString(GrdInwardPO.Rows[i].Cells[7].Text.ToString());
                    decimal Qty =!string.IsNullOrEmpty(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardQty")).Text.ToString())?Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardQty")).Text.ToString()):0;
                    DsCUnit = Obj_MaterialInwardReg.GetSubUnit(ITEMID,UnitID,Qty,ItemDesc, out StrError);
                    if (DsCUnit.Tables[0].Rows.Count > 0)
                    {
                        AjaxControlToolkit.ComboBox ddlUnit = (AjaxControlToolkit.ComboBox)GrdInwardPO.Rows[i].FindControl("ddlUnit");
                        ddlUnit.DataSource = DsCUnit.Tables[0];
                        ddlUnit.DataTextField = "UnitFactor";
                        ddlUnit.DataValueField = "#";
                        ddlUnit.DataBind();
                        // ddlUnit.SelectedValue = (Convert.ToInt32(dttable1.Rows[e.Row.RowIndex]["UnitConvDtlsId"])).ToString();
                    }
                }
            }
           

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void CalCulate()
    {
        //DataTable dtGrid = (DataTable)ViewState["CurrentTable"];
        decimal Tax = 0, Taxamnt = 0, Dis = 0, Disamnt = 0, NoOfRowsTax = 0, NoOfRowsDisc = 0;
        for (int i = 0; i < GrdInwardPO.Rows.Count; i++)
        {
            ((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text=!string.IsNullOrEmpty(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text)?(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text):"0";
            ((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text = !string.IsNullOrEmpty(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text) ? (((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text) : "0";
            if (Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text) > 0)
            {
                NoOfRowsTax = NoOfRowsTax + 1;
            }
            if (Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text) > 0)
            {
                NoOfRowsDisc= NoOfRowsDisc + 1;
            }
            Tax += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text);
            Taxamnt += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTaxAmnt")).Text);
            Dis += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text);
            Disamnt += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscAmt")).Text);
        }
       
        if (NoOfRowsDisc > 0 )
        {
            DisPer = Dis / NoOfRowsDisc;
            DisAmt = Disamnt;
        }
        if (NoOfRowsTax > 0)
        {
            TaxPer = Tax / NoOfRowsTax;
            TaxAmt = Taxamnt;
        }
        else
        {
            TaxPer = Tax / 1;
            TaxAmt = Taxamnt;
            DisPer = Dis / 1;
            DisAmt = Disamnt;
        }
        ddlItems.Focus();

    }

    public void Bind_Ten_PurchaseRate_Details(Int32 ItemID)
    {
        try
        {
            Ds = Obj_MaterialInwardReg.Get_Ten_PurchaseRate_Item(ItemID, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                GrdRateDtls.DataSource = Ds.Tables[0];
                GrdRateDtls.DataBind();

                TR_RateDtls.Visible = true;
            }
            else
            {
                SetInitialRow_GrdReqDtls();
                TR_RateDtls.Visible = true;
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {           
           MakeEmptyForm();
         //  CheckUserRight();
           FillCombo();           
        }
        else
        {
            ddlCategory.Focus();
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }

    protected void ddlPONo_SelectedIndexChanged(object sender, EventArgs e)
    {




        int POId = Convert.ToInt32(this.ddlPONo.SelectedValue);

        string ps = "GRN";
        string pnno = db.getDbstatus_Value("select PONo from  PurchaseOrder where POId='" + POId + "'");

        int unicno = Convert.ToInt32(db.getDb_Value("select count(pono) from Gnno where pono= '" + POId + "' "));
        if (unicno == 0)
        {
            unicno = 1;
        }
        else
        {
            unicno++;
        }

        string finalprojectno = ps + "- " + " " + pnno + "/" + unicno;
        TxtInwardNo.Text = finalprojectno;
        BindDetails(POId,"PO");
       
       // NetAmt = 0;
       // ((TextBox)GrdInwardPO.Rows[0].FindControl("GrdtxtInwardQty")).Focus();
    }
   
    protected void GrdtxtInwardQty_TextChanged(object sender, EventArgs e)
        {
        try
       {
           bool flag = false;
           TextBox txt = (TextBox)sender;
           decimal Inward = !string.IsNullOrEmpty(txt.Text) ? Convert.ToDecimal(txt.Text) : 0;
           GridViewRow grd = (GridViewRow)txt.Parent.Parent;
           string str1 = ((TextBox)grd.FindControl("LblOrder")).Text;
           string[] OrderQty1 = str1.Split('-');
           decimal OrderQty = Convert.ToDecimal(OrderQty1[0]);
           string str2 = (((TextBox)grd.FindControl("GrdtxtPendingQty")).Text);
           string[] PendQty1 = str2.Split('-');
           decimal Pending = Convert.ToDecimal(PendQty1[0]);
           DataTable dt = (DataTable)ViewState["CurrentTable"];
           int currentrow = grd.RowIndex;
           decimal oldInwardQty, nowqty;
           Inward = !string.IsNullOrEmpty(txt.Text) ? Convert.ToDecimal(txt.Text) : 0;
           oldInwardQty = Convert.ToDecimal(dt.Rows[currentrow]["InwardQty"].ToString());
           if (rdoInwardType.SelectedValue=="0")
           {
               if (Convert.ToBoolean(ViewState["PoidExist"]) == false)
               {
                   if (Inward <= OrderQty)
                   {
                       Pending = OrderQty - Inward;
                   }
                   else
                   {
                       ShowQtyMsg();
                   }
               }
               if (Convert.ToBoolean(ViewState["PoidExist"]) == true)
               {
                   if (Inward > Pending)
                   {
                       if ((oldInwardQty + Pending) >= Inward)
                       {
                           Pending = (oldInwardQty + Pending) - Inward;
                       }
                       else
                           ShowQtyMsg();
                   }
                   else if (Inward <= Pending)
                   {
                       if (oldInwardQty > Inward)
                       {
                           nowqty = oldInwardQty - Inward;
                           Pending = Convert.ToDecimal(Pending + nowqty);
                       }
                       if (oldInwardQty < Inward)
                       {
                           nowqty = Inward - oldInwardQty;
                           Pending = Convert.ToDecimal(Pending - nowqty);
                       }
                   }
                   else
                   {
                       ShowQtyMsg();
                   }
               } 
           }
          
                ((TextBox)grd.FindControl("GrdtxtPendingQty")).Text = Pending.ToString();
                string inwordrate = ((TextBox)grd.FindControl("GrdtxtInwardRate")).Text;
                decimal Amt = Inward * Convert.ToDecimal(inwordrate);
                ((TextBox)grd.FindControl("GrdtxtAmount")).Text = Amt.ToString();
                Decimal Tax = !string.IsNullOrEmpty((((TextBox)grd.FindControl("GrdtxtTax")).Text)) ? Convert.ToDecimal(((TextBox)grd.FindControl("GrdtxtTax")).Text) : 0;
                decimal TAXAMT = Tax * Amt / 100;
                ((TextBox)grd.FindControl("GrdtxtTaxAmnt")).Text = TAXAMT.ToString();
                ((TextBox)grd.FindControl("GrdtxtNetAmnt")).Text = (TAXAMT + Amt).ToString();
                DataTable dtGrid = (DataTable)ViewState["CurrentTable"];
                for (int i = 0; i < dtGrid.Rows.Count; i++)
                {
                    NetAmt += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].Cells[14].FindControl("GrdtxtNetAmnt")).Text);
                }
                txtSubTotal.Text = txtNetTotal.Text = NetAmt.ToString("0.00");
               // GrdtxtTax_TextChanged(sender, e);    
          //  GrdtxtDiscPer_TextChanged(sender, e);
                ((TextBox)grd.FindControl("GrdtxtInwardRate")).Focus();

          }           
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void ShowQtyMsg()
    {
        obj_Comman.ShowPopUpMsg("Inward Quantity should not be greater than Pending Quantity", this.Page);
        GrdInwardPO.DataSource = ViewState["CurrentTable"];
        GrdInwardPO.DataBind();
    }

    protected void GrdtxtInwardRate_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Decimal inwardrate = !string.IsNullOrEmpty(txt.Text) ? Convert.ToDecimal(txt.Text) : 0;
        GridViewRow grd = (GridViewRow)txt.Parent.Parent;
        string PORate = ((TextBox)grd.FindControl("lblPORate")).Text;
        decimal diff = inwardrate - Convert.ToDecimal(PORate);
        ((TextBox)grd.FindControl("GrdtxtDifference")).Text = diff.ToString();
        string inwordQty = ((TextBox)grd.FindControl("GrdtxtInwardQty")).Text;
        decimal Amt = inwardrate * Convert.ToDecimal(inwordQty);
        ((TextBox)grd.FindControl("GrdtxtAmount")).Text = Amt.ToString();
        Decimal Tax = Convert.ToDecimal(((TextBox)grd.FindControl("GrdtxtTax")).Text);
        decimal TAXAMT=Tax*Amt/100;
        ((TextBox)grd.FindControl("GrdtxtTaxAmnt")).Text = TAXAMT.ToString();
        ((TextBox)grd.FindControl("GrdtxtNetAmnt")).Text=(TAXAMT+Amt).ToString();
        DataTable dtGrid = (DataTable)ViewState["CurrentTable"];
        for (int i = 0; i < dtGrid.Rows.Count; i++)
        {
            NetAmt += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].Cells[14].FindControl("GrdtxtNetAmnt")).Text);
        }
        txtSubTotal.Text = txtNetTotal.Text = NetAmt.ToString("0.00");
      //  GrdtxtTax_TextChanged(sender, e);    
      //  GrdtxtDiscPer_TextChanged(sender, e);
        ((TextBox)grd.FindControl("GrdtxtTax")).Focus();
        ((TextBox)grd.FindControl("GrdtxtTax")).Text="";
    }

    protected void GrdtxtTax_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Decimal Tax = !string.IsNullOrEmpty(txt.Text) ? Convert.ToDecimal(txt.Text) : 0;
        GridViewRow grd = (GridViewRow)txt.Parent.Parent;
        decimal Amt = Convert.ToDecimal((Convert.ToDecimal(((TextBox)grd.FindControl("GrdtxtInwardQty")).Text)) * (Convert.ToDecimal(((TextBox)grd.FindControl("GrdtxtInwardRate")).Text))); //Convert.ToDecimal(((TextBox)grd.FindControl("GrdtxtAmount")).Text);
        decimal TaxAmount = (Tax * (Amt - Convert.ToDecimal(string.IsNullOrEmpty(((TextBox)grd.FindControl("GrdtxtDiscAmt")).Text) ? "0" : ((TextBox)grd.FindControl("GrdtxtDiscAmt")).Text))) / 100;
        ((TextBox)grd.FindControl("GrdtxtTaxAmnt")).Text = TaxAmount.ToString();

        
        decimal NetAmount = Amt + TaxAmount - Convert.ToDecimal(((TextBox)grd.FindControl("GrdtxtDiscAmt")).Text);
        ((TextBox)grd.FindControl("GrdtxtNetAmnt")).Text = NetAmount.ToString();
        decimal subtot=0;
        decimal Vat = 0;
        decimal Disc= 0;
        DataTable dtGrid = (DataTable)ViewState["CurrentTable"];
        for (int i = 0; i < dtGrid.Rows.Count; i++)
        {
            subtot += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].Cells[14].FindControl("GrdtxtAmount")).Text);
            NetAmt += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].Cells[14].FindControl("GrdtxtNetAmnt")).Text);
            Vat += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].Cells[14].FindControl("GrdtxtTaxAmnt")).Text);
            Disc += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].Cells[14].FindControl("GrdtxtDiscAmt")).Text);
        }
        txtSubTotal.Text = subtot.ToString("0.00");
        txtNetTotal.Text = NetAmt.ToString("0.00");
       
        //---Set TaxAmt And TaxPer---
        CalCulate();
       // txtVATPer.Text=TaxPer.ToString("0.00");  for demo
        txtVATAmount.Text=Vat.ToString("0.00");
        txtDiscount.Text = Disc.ToString("0.00");
        ScriptManager.RegisterStartupScript(Page,this.GetType(), "CalcVatdiscPer", "javascript:CalcVatdiscPer();", true);

        ((TextBox)grd.FindControl("GrdtxtDiscPer")).Focus();
        ((TextBox)grd.FindControl("GrdtxtDiscPer")).Text="";

    }

    protected void GrdInwardPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtLoc = (DataTable)Session["INLOCATION"];
                DataTable dtUnit = (DataTable)Session["UNITCONV"];
                ((DropDownList)e.Row.FindControl("ddlLocation")).DataSource = dtLoc;
                ((DropDownList)e.Row.FindControl("ddlLocation")).DataTextField = "LocName";
                ((DropDownList)e.Row.FindControl("ddlLocation")).DataValueField = "LocID";
                ((DropDownList)e.Row.FindControl("ddlLocation")).DataBind();
                ((DropDownList)e.Row.FindControl("ddlLocation")).SelectedValue = ((TextBox)e.Row.FindControl("lblLocID")).Text;

                ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlUnit")).DataSource = dtUnit;
                ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlUnit")).DataTextField = "Unit";
                ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlUnit")).DataValueField = "UnitID";
                ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlUnit")).DataBind();
                ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlUnit")).SelectedValue = e.Row.Cells[25].Text;
            }
         
        }
        catch (Exception)
        {
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDtls = 0;
        ArrayList Suplierlist = new ArrayList();
        int PO_SupId;
        bool Match_Flag = false;
        try
        {
            for (int RowNo = 0; RowNo < GrdInwardPO.Rows.Count; RowNo++)
            {
              //  PO_SupId = Convert.ToInt32(GrdInwardPO.Rows[RowNo].Cells[1].Text);
                PO_SupId = Convert.ToInt32(ddlSuplier.SelectedValue);
                if (Suplierlist.Count == 0)
                {
                    Match_Flag = true;
                }
                else
                {
                    for (int m = 0; m < Suplierlist.Count; m++)
                    {
                        if (Suplierlist[m].ToString() == PO_SupId.ToString())
                        {
                            goto CheckNextSupplier;
                        }
                        else
                        {
                            Match_Flag = true;
                        }
                    }
                }
                if (Match_Flag == true)
                {
                    Suplierlist.Add(PO_SupId);
                    Entity_MaterialInwardReg.InwardNo = TxtInwardNo.Text;
                    Entity_MaterialInwardReg.InwardDate = Convert.ToDateTime(TxtInwardDate.Text);
                    //Add Field by Piyush on dated 23-Jan-2013
                    if (rdoInwardType.SelectedValue == "0")
                    {
                        Entity_MaterialInwardReg.Type = "P";
                        Entity_MaterialInwardReg.POId = Convert.ToInt32(ddlPONo.SelectedValue);
                        PO_SupId = Convert.ToInt32(GrdInwardPO.Rows[0].Cells[26].Text);
                    }
                    else
                    {
                        if (rdoTemplateItem.SelectedValue == "0")
                        {
                            Entity_MaterialInwardReg.Type = "T";
                            Entity_MaterialInwardReg.POId = 0;//Convert.ToInt32(ddlTemplate.SelectedValue);
                            Entity_MaterialInwardReg.SuplierId = PO_SupId;//Add New Field By piyush in dated 24-Jan-2013
                        }
                        else
                        {
                            Entity_MaterialInwardReg.Type = "I";
                            Entity_MaterialInwardReg.POId = 0;//Convert.ToInt32(ddlPONo.SelectedValue);
                            Entity_MaterialInwardReg.SuplierId = Convert.ToInt32(ddlSuplier.SelectedValue);//Add New Field By piyush in dated 24-Jan-2013
                        }
                    }
                    //End Here

                    db.insert("insert into Gnno values('" + TxtInwardNo.Text + "' ,'" + ddlPONo.SelectedValue + "')");

                    Entity_MaterialInwardReg.BillNo = Convert.ToString(txtBillNo.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.InwardThrough = Convert.ToInt32(rdoInwardThrough.SelectedValue);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.BillingAddress = TxtBillingAddress.Text;
                    Entity_MaterialInwardReg.ShippingAddress = TxtShippingAddress.Text;
                   
                    Entity_MaterialInwardReg.SubTotal = Convert.ToDecimal(txtSubTotal.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.DiscountPer = txtDiscountPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtDiscountPer.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.DiscountAmt = txtDiscount.Text.Equals("") ? 0 : Convert.ToDecimal(txtDiscount.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.VatPer = txtVATPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtVATPer.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.VatAmt = txtVATAmount.Text.Equals("") ? 0 : Convert.ToDecimal(txtVATAmount.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.DekhrekhPer = txtDekhrekhPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtDekhrekhPer.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.DekhrekhAmt = txtDekhrekhAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtDekhrekhAmt.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.HamaliPer = txtHamaliPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtHamaliPer.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.HamaliAmt = txtHamaliAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtHamaliAmt.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.CESSPer = txtCESSPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtCESSPer.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.CESSAmt = txtCESSAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtCESSAmt.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.FreightPer = txtFreightPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtFreightPer.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.FreightAmt = txtFreightAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtFreightAmt.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.PackingPer = txtPackingPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtPackingPer.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.PackingAmt = txtPackingAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtPackingAmt.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.PostagePer = txtPostagePer.Text.Equals("") ? 0 : Convert.ToDecimal(txtPostagePer.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.PostageAmt = txtPostageAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtPostageAmt.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.OtherCharges = txtOtherCharges.Text.Equals("") ? 0 : Convert.ToDecimal(txtOtherCharges.Text);//Add New Field By Anand in dated 17-Jan-2013
                    Entity_MaterialInwardReg.GrandTotal = txtNetTotal.Text.Equals("") ? 0 : Convert.ToDecimal(txtNetTotal.Text);//Add New Field By Anand in dated 17-Jan-2013

                    Entity_MaterialInwardReg.BillDate = Convert.ToDateTime(txtBillDate.Text);

                    Entity_MaterialInwardReg.Instruction = txtInstruction.Text;

                    Entity_MaterialInwardReg.Vehical = txtVehicle.Text;
                    Entity_MaterialInwardReg.TimeIn =Convert.ToDateTime(TimeInSelector.Date.ToShortTimeString());//Convert.ToDateTime(txtTimeIn.Text);
                    Entity_MaterialInwardReg.TimeOut = Convert.ToDateTime(TimeOutSelector.Date.ToShortTimeString());


                    Entity_MaterialInwardReg.UserId = Convert.ToInt32(Session["UserId"]);
                    Entity_MaterialInwardReg.LoginDate = Convert.ToDateTime(DateTime.Now);
                    Entity_MaterialInwardReg.UserInwardNo = Convert.ToString(txtUserInwardNo.Text);
                    InsertRow = Obj_MaterialInwardReg.InsertRecord(ref Entity_MaterialInwardReg, out StrError);
                    if (InsertRow > 0)
                    {
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtInsert = new DataTable();
                            for (int i = 0; i < GrdInwardPO.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(GrdInwardPO.Rows[i].Cells[26].Text) == PO_SupId)
                                {
                                    if (Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardQty")).Text) > 0)
                                    {
                                        Entity_MaterialInwardReg.InwardId = InsertRow;
                                        Entity_MaterialInwardReg.ItemId = Convert.ToInt32(GrdInwardPO.Rows[i].Cells[2].Text);
                                        Entity_MaterialInwardReg.InwardQty = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardQty")).Text);
                                        //****---split "10.00-kg" as "10.00"---***
                                        string str1 = (GrdInwardPO.Rows[i].Cells[8].Text);
                                        string[] OrderQty1 = str1.Split('-');
                                        Entity_MaterialInwardReg.OrderQty = Convert.ToDecimal(OrderQty1[0]);
                                        //Entity_MaterialInwardReg.OrderQty = Convert.ToDecimal(GrdInwardPO.Rows[i].Cells[3].Text);

                                        string str2 = (((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtPendingQty")).Text);
                                        string[] PendQty1 = str2.Split('-');
                                        Entity_MaterialInwardReg.PendingQty = !string.IsNullOrEmpty(((PendQty1[0])).ToString()) ? 0 : Convert.ToDecimal(PendQty1[0]);
                                        Entity_MaterialInwardReg.InwardRate = (((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardRate")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardRate")).Text);
                                        Entity_MaterialInwardReg.PORate = ((GrdInwardPO.Rows[i].Cells[14].Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(GrdInwardPO.Rows[i].Cells[14].Text);
                                        Entity_MaterialInwardReg.Diffrence = ((((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDifference")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDifference")).Text);
                                        Entity_MaterialInwardReg.Amount = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtAmount")).Text);
                                        Entity_MaterialInwardReg.TaxPer = ((((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text);
                                        Entity_MaterialInwardReg.TaxAmount = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTaxAmnt")).Text);
                                        Entity_MaterialInwardReg.DiscPer = ((((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text);
                                        Entity_MaterialInwardReg.DiscAmt = ((((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscAmt")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscAmt")).Text);
                                        Entity_MaterialInwardReg.NetAmount = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtNetAmnt")).Text);
                                        Entity_MaterialInwardReg.ExpectedDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());//Convert.ToDateTime(((TextBox)GrdInwardPO.Rows[i].FindControl("txtExpDelDate")).Text);
                                        Entity_MaterialInwardReg.DeliveryDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());// Convert.ToDateTime(((TextBox)GrdInwardPO.Rows[i].FindControl("txtActDelDate")).Text);
                                        Entity_MaterialInwardReg.UnitId = Convert.ToInt32(GrdInwardPO.Rows[i].Cells[25].Text);
                                        Entity_MaterialInwardReg.StockDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                        Entity_MaterialInwardReg.StockLocationID = Convert.ToInt32(Session["TransactionSiteID"]);
                                        Entity_MaterialInwardReg.LocID = Convert.ToInt32(((DropDownList)GrdInwardPO.Rows[i].FindControl("ddlLocation")).SelectedValue);
                                        Entity_MaterialInwardReg.ConversionUnitId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdInwardPO.Rows[i].FindControl("ddlUnit")).SelectedValue);
                                        Entity_MaterialInwardReg.ActualQty = string.IsNullOrEmpty(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtActualQty")).Text) ? 0 : (Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtActualQty")).Text));
                                        Al.Add(Convert.ToInt32(((DropDownList)GrdInwardPO.Rows[i].FindControl("ddlLocation")).SelectedValue));
                                        Entity_MaterialInwardReg.ItemDesc = (Convert.ToString(GrdInwardPO.Rows[i].Cells[7].Text).Equals("&nbsp;")) ? "" : Convert.ToString(GrdInwardPO.Rows[i].Cells[7].Text);
                                        InsertRowDtls = Obj_MaterialInwardReg.InsertDetailsRecord(ref Entity_MaterialInwardReg, out StrError);

                                    }
                                }
                            }
                        }
                    
                        
                    }
                    int co = 0;
                    for (int q = 0; q < Al.Count; q++)
                    {
                        if (Al[q].ToString() == "3")
                        {
                            co++;
                        }
                        else
                        {
                            
                        }
                    }
                    if (co == 0)
                    {
                        Entity_MaterialInwardReg.InwardId = InsertRow;
                        InsertRowDtls = Obj_MaterialInwardReg.DeleteFromOutwardRecord(ref Entity_MaterialInwardReg, out StrError);
                    }
                    if (co == Al.Count)
                    {
                        Entity_MaterialInwardReg.InwardId = InsertRow;
                        InsertRowDtls = Obj_MaterialInwardReg.UpdateForAssignStatus(ref Entity_MaterialInwardReg, out StrError);
                    }
                    Al.Clear();


                }
            CheckNextSupplier: Match_Flag = false; 
            }
            if (InsertRow > 0)
            {
                DataSet id = Obj_MaterialInwardReg.GetSaveInwardNo(InsertRow, out StrError);
                obj_Comman.ShowPopUpMsg("Record Saved Successfully"+"\n"+"For Inward No:-  " + id.Tables[0].Rows[0]["InwardNo"].ToString(), this.Page);
                //MakeControlEmpty();
                MakeEmptyForm();
                FillCombo();
                Entity_MaterialInwardReg = null;
                Obj_MaterialInwardReg = null;
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, UpdateRowDtls = 0;
        try
        {
            if (ViewState["ID"] != null)
            {
                Entity_MaterialInwardReg.InwardId = Convert.ToInt32(ViewState["ID"]);
            }
            Entity_MaterialInwardReg.InwardNo = TxtInwardNo.Text;
            Entity_MaterialInwardReg.InwardDate = Convert.ToDateTime(TxtInwardDate.Text);
            if (rdoInwardType.SelectedValue == "0")
            {
                Entity_MaterialInwardReg.Type = "P";
                Entity_MaterialInwardReg.POId = Convert.ToInt32(LblPONo.Text);
               // Entity_MaterialInwardReg.SuplierId = Convert.ToInt32(ddlSuplier.SelectedValue);
            }
            else
            {
                if (rdoTemplateItem.SelectedValue == "0")
                {
                    Entity_MaterialInwardReg.Type = "T";
                    Entity_MaterialInwardReg.POId = Convert.ToInt32(LblPONo.Text);
                    Entity_MaterialInwardReg.SuplierId = Convert.ToInt32(ddlSuplier.SelectedValue);
                }
                else
                {
                    Entity_MaterialInwardReg.Type = "I";
                    Entity_MaterialInwardReg.POId = 0;//Convert.ToInt32(LblPONo.Text);
                    Entity_MaterialInwardReg.SuplierId = Convert.ToInt32(ddlSuplier.SelectedValue);
                }
            }
            //End Here
            
            Entity_MaterialInwardReg.BillNo = Convert.ToString(txtBillNo.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.InwardThrough = Convert.ToInt32(rdoInwardThrough.SelectedValue);//Add New Field By Anand in dated 17-Jan-2013

            Entity_MaterialInwardReg.BillingAddress = TxtBillingAddress.Text;
            Entity_MaterialInwardReg.ShippingAddress = TxtShippingAddress.Text;

            Entity_MaterialInwardReg.SubTotal = Convert.ToDecimal(txtSubTotal.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.DiscountPer = txtDiscountPer.Text.Equals("") ? 0: Convert.ToDecimal(txtDiscountPer.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.DiscountAmt = txtDiscount.Text.Equals("") ? 0 : Convert.ToDecimal(txtDiscount.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.VatPer = txtVATPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtVATPer.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.VatAmt = txtVATAmount.Text.Equals("") ? 0 : Convert.ToDecimal(txtVATAmount.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.DekhrekhPer = txtDekhrekhPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtDekhrekhPer.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.DekhrekhAmt = txtDekhrekhAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtDekhrekhAmt.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.HamaliPer = txtHamaliPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtHamaliPer.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.HamaliAmt = txtHamaliAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtHamaliAmt.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.CESSPer = txtCESSPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtCESSPer.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.CESSAmt = txtCESSAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtCESSAmt.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.FreightPer = txtFreightPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtFreightPer.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.FreightAmt = txtFreightAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtFreightAmt.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.PackingPer = txtPackingPer.Text.Equals("") ? 0 : Convert.ToDecimal(txtPackingPer.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.PackingAmt = txtPackingAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtPackingAmt.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.PostagePer = txtPostagePer.Text.Equals("") ? 0 : Convert.ToDecimal(txtPostagePer.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.PostageAmt = txtPostageAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtPostageAmt.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.OtherCharges = txtOtherCharges.Text.Equals("") ? 0 : Convert.ToDecimal(txtOtherCharges.Text);//Add New Field By Anand in dated 17-Jan-2013
            Entity_MaterialInwardReg.GrandTotal = txtNetTotal.Text.Equals("") ? 0 : Convert.ToDecimal(txtNetTotal.Text);//Add New Field By Anand in dated 17-Jan-2013

            Entity_MaterialInwardReg.BillDate = Convert.ToDateTime(txtBillDate.Text);

            Entity_MaterialInwardReg.Instruction = txtInstruction.Text;

            Entity_MaterialInwardReg.Vehical = txtVehicle.Text;
            Entity_MaterialInwardReg.TimeIn = Convert.ToDateTime(TimeInSelector.Date.ToShortTimeString());
            Entity_MaterialInwardReg.TimeOut = Convert.ToDateTime(TimeOutSelector.Date.ToShortTimeString());


            Entity_MaterialInwardReg.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_MaterialInwardReg.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            Entity_MaterialInwardReg.UserInwardNo= Convert.ToString(txtUserInwardNo.Text);
            UpdateRow = Obj_MaterialInwardReg.UpdateRecord(ref Entity_MaterialInwardReg, out StrError);
            if (UpdateRow > 0)
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtInsert = new DataTable();
                    dtInsert = (DataTable)ViewState["CurrentTable"];
                    for (int i = 0; i < GrdInwardPO.Rows.Count; i++)
                    {
                        
                        Entity_MaterialInwardReg.ItemId = Convert.ToInt32(GrdInwardPO.Rows[i].Cells[2].Text);
                        Entity_MaterialInwardReg.InwardQty = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardQty")).Text);
                        //****---split "10.00-kg" as "10.00"---***
                        string str1 = (GrdInwardPO.Rows[i].Cells[8].Text);
                        string[] OrderQty1 = str1.Split('-');
                        Entity_MaterialInwardReg.OrderQty = Convert.ToDecimal(OrderQty1[0]);
                        string str2 = (((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtPendingQty")).Text);
                        string[] PendQty1 = str2.Split('-');
                        Entity_MaterialInwardReg.PendingQty = Convert.ToDecimal(PendQty1[0]);
                        Entity_MaterialInwardReg.InwardRate = (((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardRate")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardRate")).Text);
                        Entity_MaterialInwardReg.PORate = ((GrdInwardPO.Rows[i].Cells[14].Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(GrdInwardPO.Rows[i].Cells[14].Text);
                        Entity_MaterialInwardReg.Diffrence = ((((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDifference")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDifference")).Text);
                        Entity_MaterialInwardReg.Amount = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtAmount")).Text);
                        Entity_MaterialInwardReg.TaxPer = ((((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text);
                        Entity_MaterialInwardReg.TaxAmount = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTaxAmnt")).Text);
                        Entity_MaterialInwardReg.DiscPer = ((((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text);
                        Entity_MaterialInwardReg.DiscAmt = ((((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscAmt")).Text)).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscAmt")).Text);
                        Entity_MaterialInwardReg.NetAmount = Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtNetAmnt")).Text);
                        Entity_MaterialInwardReg.ExpectedDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString()); //Convert.ToDateTime(((TextBox)GrdInwardPO.Rows[i].FindControl("txtExpDelDate")).Text);
                        Entity_MaterialInwardReg.DeliveryDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString()); //Convert.ToDateTime(((TextBox)GrdInwardPO.Rows[i].FindControl("txtActDelDate")).Text);

                        Entity_MaterialInwardReg.UnitId = Convert.ToInt32(GrdInwardPO.Rows[i].Cells[25].Text);
                        Entity_MaterialInwardReg.StockDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        Entity_MaterialInwardReg.StockLocationID = Convert.ToInt32(Session["TransactionSiteID"]);
                        Entity_MaterialInwardReg.LocID = Convert.ToInt32(((DropDownList)GrdInwardPO.Rows[i].FindControl("ddlLocation")).SelectedValue);
                        Entity_MaterialInwardReg.ConversionUnitId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdInwardPO.Rows[i].FindControl("ddlUnit")).SelectedValue);
                        Entity_MaterialInwardReg.ActualQty = string.IsNullOrEmpty(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtActualQty")).Text) ? 0 : (Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtActualQty")).Text));
                        Al.Add(Convert.ToInt32(((DropDownList)GrdInwardPO.Rows[i].FindControl("ddlLocation")).SelectedValue));
                        Entity_MaterialInwardReg.ItemDesc = (Convert.ToString(GrdInwardPO.Rows[i].Cells[7].Text).Equals("&nbsp;")) ? "" : Convert.ToString(GrdInwardPO.Rows[i].Cells[7].Text);
                        UpdateRowDtls = Obj_MaterialInwardReg.InsertDetailsRecord(ref Entity_MaterialInwardReg, out StrError);
                    }
                }
                int co = 0;
                for (int q = 0; q < Al.Count; q++)
                {
                    if (Al[q].ToString() == "3")
                    {
                        co++;
                    }
                    else
                    {

                    }
                }
                if (co == 0)
                {
                    Entity_MaterialInwardReg.InwardId = Convert.ToInt32(ViewState["ID"]);
                    UpdateRowDtls = Obj_MaterialInwardReg.DeleteFromOutwardRecord(ref Entity_MaterialInwardReg, out StrError);
                }
                if (co == Al.Count)
                {
                    Entity_MaterialInwardReg.InwardId = Convert.ToInt32(ViewState["ID"]);
                    UpdateRowDtls = Obj_MaterialInwardReg.UpdateForAssignStatus(ref Entity_MaterialInwardReg, out StrError);
                }
                Al.Clear();
                if (UpdateRow > 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Updated Successfully"+"\n"+"For Inward No:-  "+ (TxtInwardNo.Text), this.Page);
                    MakeEmptyForm();
                    FillCombo();
                    Entity_MaterialInwardReg = null;
                    Obj_MaterialInwardReg = null;
                }
            }

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void ReportGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {

                        if (Convert.ToInt32(e.CommandArgument) != 0)
                        {
                            ViewState["ID"] = Convert.ToInt32(e.CommandArgument);
                            Ds = Obj_MaterialInwardReg.GetRecordForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                            {
                                rdoInwardType.Enabled = rdoTemplateItem.Enabled = true;
                                //=============Change Date 8-8-2013=====================================================
                                //ddlPONo.Enabled = ImgBtnAddTemplate.Enabled = ddlTemplate.Enabled = ddlCategory.Enabled = ddlItems.Enabled = ddlSuplier.Enabled = true;
                                //==================================================================
                                ddlPONo.Enabled = ImgBtnAddTemplate.Enabled =  ddlTemplate.Enabled = ddlCategory.Enabled = ddlItems.Enabled =  true;
                                //ImgBtnAddItem.Enabled =
                                TxtInwardNo.Text = Ds.Tables[0].Rows[0]["InwardNo"].ToString();
                                TxtInwardDate.Text = Ds.Tables[0].Rows[0]["InwardDate"].ToString();
                                LblPONo.Text = Ds.Tables[0].Rows[0]["POId"].ToString();
                                txtBillNo.Text = Ds.Tables[0].Rows[0]["BillNo"].ToString();
                                rdoInwardThrough.SelectedValue = Ds.Tables[0].Rows[0]["InwardThrough"].ToString();
                                txtSupplierName.Text = Ds.Tables[0].Rows[0]["SuplierName"].ToString();//ddlSuplier.SelectedText =
                                ddlSuplier.SelectedValue = Convert.ToString(Ds.Tables[0].Rows[0]["SuplierId"].ToString());
                                TxtBillingAddress.Text = Ds.Tables[0].Rows[0]["BillingAddress"].ToString();
                                TxtShippingAddress.Text = Ds.Tables[0].Rows[0]["ShippingAddress"].ToString();
                                LblSuplierID.Text = Ds.Tables[0].Rows[0]["SuplierId"].ToString();
                                txtSubTotal.Text = Ds.Tables[0].Rows[0]["SubTotal"].ToString();
                                txtDiscountPer.Text = Ds.Tables[0].Rows[0]["DiscountPer"].ToString();
                                txtDiscount.Text = Ds.Tables[0].Rows[0]["DiscountAmt"].ToString();
                                txtVATPer.Text = Ds.Tables[0].Rows[0]["VatPer"].ToString();
                                txtVATAmount.Text = Ds.Tables[0].Rows[0]["VatAmt"].ToString();
                                txtDekhrekhPer.Text = Ds.Tables[0].Rows[0]["DekhrekhPer"].ToString();
                                txtDekhrekhAmt.Text = Ds.Tables[0].Rows[0]["DekhrekhAmt"].ToString();
                                txtHamaliPer.Text = Ds.Tables[0].Rows[0]["HamaliPer"].ToString();
                                txtHamaliAmt.Text = Ds.Tables[0].Rows[0]["HamaliAmt"].ToString();
                                txtCESSPer.Text = Ds.Tables[0].Rows[0]["CESSPer"].ToString();
                                txtCESSAmt.Text = Ds.Tables[0].Rows[0]["CESSAmt"].ToString();
                                txtFreightPer.Text = Ds.Tables[0].Rows[0]["FreightPer"].ToString();
                                txtFreightAmt.Text = Ds.Tables[0].Rows[0]["FreightAmt"].ToString();
                                txtPackingPer.Text = Ds.Tables[0].Rows[0]["PackingPer"].ToString();
                                txtPackingAmt.Text = Ds.Tables[0].Rows[0]["PackingAmt"].ToString();
                                txtPostagePer.Text = Ds.Tables[0].Rows[0]["PostagePer"].ToString();
                                txtPostageAmt.Text = Ds.Tables[0].Rows[0]["PostageAmt"].ToString();
                                txtOtherCharges.Text = Ds.Tables[0].Rows[0]["OtherCharges"].ToString();
                                txtNetTotal.Text = Ds.Tables[0].Rows[0]["GrandTotal"].ToString();                                
                                txtVehicle.Text = Ds.Tables[0].Rows[0]["VehicalNo"].ToString();
                                TimeInSelector.Date = Convert.ToDateTime(Ds.Tables[0].Rows[0]["TimeIn"].ToString());
                                TimeOutSelector.Date = Convert.ToDateTime(Ds.Tables[0].Rows[0]["TimeOut"].ToString());
                                txtInstruction.Text = Ds.Tables[0].Rows[0]["Instruction"].ToString();

                                txtBillDate.Text = Ds.Tables[0].Rows[0]["BillDate"].ToString();
                                txtUserInwardNo.Text = Ds.Tables[0].Rows[0]["UserInwardNo"].ToString();
                                txtst.Text = "1";

                                if (Ds.Tables[0].Rows[0]["InwardType"].ToString().Trim().Equals("P"))
                                {
                                    rdoInwardType.SelectedIndex = 0;
                                    BlockControl();
                                }
                                else
                                {
                                    rdoInwardType.SelectedIndex = 1;
                                    //rdoInwardType_SelectedIndexChanged(sender, e);
                                    TRTemplate.Visible = TRItem.Visible = TRRadio.Visible = true;
                                    TRPO.Visible = false;
                                    rdoTemplateItem.SelectedIndex = 0;
                                    ddlTemplate.Enabled = true;
                                    ImgBtnAddTemplate.Enabled = true;
                                    //ImgBtnAddItem.Enabled = false;
                                     //ddlSuplier.Enabled = false;
                                    //ddlCategory.Enabled = ddlItems.Enabled =
                                    if (Ds.Tables[0].Rows[0]["InwardType"].ToString().Trim().Equals("T"))
                                    {
                                        rdoTemplateItem.SelectedIndex = 0;
                                        BlockControl();
                                    }
                                    else
                                    {
                                        rdoTemplateItem.SelectedIndex = 1;
                                        BlockControl();
                                    }
                                }
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            if (Ds.Tables[1].Rows.Count > 0)
                            {
                                GrdInwardPO.DataSource = Ds.Tables[1];
                                GrdInwardPO.DataBind();
                                ViewState["CurrentTable"] = Ds.Tables[1];
                                ViewState["PoidExist"] = true;
                                DataSet DsCUnit = new DataSet();

                                for (int i = 0; i < Ds.Tables[1].Rows.Count; i++)
                                {
                                    int ITEMID = Convert.ToInt32(Ds.Tables[1].Rows[i]["ItemId"].ToString());
                                    string ITEMDESC = Convert.ToString(Ds.Tables[1].Rows[i]["ItemDesc"].ToString());
                                    int UnitID = Convert.ToInt32(Ds.Tables[1].Rows[i]["UnitConvDtlsId"].ToString());
                                    decimal Qty = Convert.ToDecimal(Ds.Tables[1].Rows[i]["InwardQty"].ToString());
                                    DsCUnit = Obj_MaterialInwardReg.GetSubUnit(ITEMID,UnitID,Qty,ITEMDESC, out StrError);
                                    if (DsCUnit.Tables[0].Rows.Count > 0)
                                    {
                                        AjaxControlToolkit.ComboBox ddlUnit = (AjaxControlToolkit.ComboBox)GrdInwardPO.Rows[i].FindControl("ddlUnit");

                                        ddlUnit.DataSource = DsCUnit.Tables[0];
                                        ddlUnit.DataTextField = "UnitFactor";
                                        ddlUnit.DataValueField = "#";
                                        ddlUnit.DataBind();
                                        ddlUnit.SelectedValue = (Convert.ToInt32(Ds.Tables[1].Rows[i]["UnitConvDtlsId"])).ToString();
                                    }
                                }

                            }

                            Ds = null;
                            Obj_MaterialInwardReg = null;
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                        }
                        break;
                    }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }
    
    protected void ReportGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int DeleteId = 0;
        try
        {
            DeleteId = Convert.ToInt32(((ImageButton)ReportGrid.Rows[e.RowIndex].Cells[0].FindControl("ImgBtnDelete")).CommandArgument.ToString());
            if (DeleteId != 0)
            {
                Entity_MaterialInwardReg.InwardId = DeleteId;
                Entity_MaterialInwardReg.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_MaterialInwardReg.LoginDate = DateTime.Now;
                int iDelete = Obj_MaterialInwardReg.DeleteRecord(ref Entity_MaterialInwardReg, out StrError);
                if (iDelete != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                    FillCombo();
                }
            }

            Entity_MaterialInwardReg = null;
            Obj_MaterialInwardReg = null;


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        BindReportGrid(StrCondition);

        TR_RateDtls.Visible = false;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMMaterialInwardReg Obj_MaterialInwardReg = new DMMaterialInwardReg();
        String[] SearchList = Obj_MaterialInwardReg.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void GrdInwardPO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        
        switch (e.CommandName)
        {
            case ("Select"):
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    ViewState["CurrentRow"] = index;
                    int CurrRow = Convert.ToInt32(e.CommandArgument);
                    int ItemId = Convert.ToInt32(GrdInwardPO.Rows[CurrRow].Cells[2].Text);
                    if (ItemId != 0)
                    {
                        Bind_Ten_PurchaseRate_Details(ItemId);
                    }
                }
                break;
        }
    }

    protected void rdoInwardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoInwardType.SelectedValue == "0")
        {
            TRTemplate.Visible = TRItem.Visible = TRRadio.Visible = false;
            TRPO.Visible = true;
            SetInitialRow();

        }
        else
        {
            TRTemplate.Visible = TRItem.Visible = TRRadio.Visible = true;
            TRPO.Visible = false;
            rdoTemplateItem.SelectedIndex = 0;
            ddlTemplate.Enabled = true;
            ImgBtnAddTemplate.Enabled = true;
            ImgBtnAddItem.Enabled = false;
            ddlCategory.Enabled = ddlItems.Enabled = ddlSuplier.Enabled = false;
            LblSuplier.Text = "Not Applicable";
            LblSuplierID.Text = "0";
            SetInitialRow();
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string expression = "[CategoryId] =" + ddlCategory.SelectedValue + " or [CategoryId] =0";
            string sortOrder = "[ItemId] ASC";
            DSTABLE = (DataTable)ViewState["DSTABLEITEM"];
            DataView dv = new DataView(DSTABLE, expression, sortOrder, DataViewRowState.CurrentRows);
            DataTable DSITEM = dv.ToTable("ITEM");
            if (DSITEM.Rows.Count > 0)
            {
                ddlItems.DataSource = DSITEM;
                ddlItems.DataTextField = "ItemName";
                ddlItems.DataValueField = "ItemId";
                ddlItems.DataBind();
                //ddlItems.Focus();
                ddlCategory.Focus();
            }
            else
            {
                
            }
            ddlCategory.Focus();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void ImgBtnAddTemplate_Click(object sender, ImageClickEventArgs e)//Add Fiels By Piyush om dated 23-Jan-2013
    {
        int TemplateId = Convert.ToInt32(this.ddlTemplate.SelectedValue);
        SetInitialRow();
        ViewState["GridIndex"] = null;
        BindDetails(TemplateId,"Temp");
        NetAmt = 0;
        ((TextBox)GrdInwardPO.Rows[0].FindControl("GrdtxtInwardQty")).Focus();
    }

    protected void ImgBtnAddItem_Click(object sender, ImageClickEventArgs e)//Add Fiels By Piyush om dated 23-Jan-2013
    {
        int TemplateId = Convert.ToInt32(this.ddlItems.SelectedValue);
        BindDetails(TemplateId, "ITEM");
        NetAmt = 0;
        //ImgBtnAddItem.Focus();
        ((TextBox)GrdInwardPO.Rows[GrdInwardPO.Rows.Count-1].FindControl("GrdtxtInwardQty")).Focus();
        ((TextBox)GrdInwardPO.Rows[GrdInwardPO.Rows.Count - 1].FindControl("GrdtxtInwardQty")).Text="";
    }

    protected void rdoTemplateItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoTemplateItem.SelectedValue == "0")
        {           
            ddlTemplate.Enabled = true;
            ImgBtnAddTemplate.Enabled = true;
            ImgBtnAddItem.Enabled = false;
            ddlCategory.Enabled = ddlItems.Enabled = ddlSuplier.Enabled = false;
            LblSuplier.Text = "Not Applicable";
            LblSuplierID.Text = "0";
            SetInitialRow();
            //txtBillNo.Focus();
            rdoInwardThrough.Focus();
        }
        else
        {
            //BtnForPopUp_Click(sender, e);
            rdoTemplateItem.SelectedIndex = 1;
            ddlTemplate.Enabled = false;
            ImgBtnAddTemplate.Enabled = false;
            ImgBtnAddItem.Enabled = true;
            ddlCategory.Enabled = ddlItems.Enabled = ddlSuplier.Enabled = true;
            SetInitialRow();
            //LblSuplier.Text = ddlSuplierPopUp.SelectedItem.ToString();
            //LblSuplierID.Text = ddlSuplierPopUp.SelectedValue.ToString();
            ddlSuplier.SelectedIndex = 0;
            //txtBillNo.Focus();
            rdoInwardThrough.Focus();
        }        
    }

    protected void BtnDone_Click(object sender, EventArgs e)
    {
        rdoTemplateItem.SelectedIndex = 1;
        ddlTemplate.Enabled = false;
        ImgBtnAddTemplate.Enabled = false;
        ImgBtnAddItem.Enabled = true;
        ddlCategory.Enabled = ddlItems.Enabled = ddlSuplier.Enabled = true;
        SetInitialRow();
        LblSuplier.Text = ddlSuplierPopUp.SelectedItem.ToString();
        LblSuplierID.Text = ddlSuplierPopUp.SelectedValue.ToString();
        ddlItems.Focus();
    }

    protected void BtnForPopUp_Click(object sender, EventArgs e)
    {
        if (rdoTemplateItem.SelectedValue == "1")
        {
            ddlSuplierPopUp.Focus();
            MPE.Show();
        }
    }

    public void BlockControl()
    {        
        rdoInwardType.Enabled = rdoTemplateItem.Enabled = false;
        //---------------------change date 8-Aug-2013-------------------------------------------------
        //ddlPONo.Enabled=ImgBtnAddTemplate.Enabled= ddlTemplate.Enabled =  ddlSuplier.Enabled = false;
        //---------------------End change date 8-Aug-2013-------------------------------------------------
        ddlPONo.Enabled = ImgBtnAddTemplate.Enabled = ddlTemplate.Enabled =  false;
        //ddlCategory.Enabled = ddlItems.Enabled ==ImgBtnAddItem.Enabled 
    }

    protected void ReportGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[9].Text.Trim().Equals("P"))
            {
                e.Row.Cells[9].Text = "Purchase Order";
            }
            if (e.Row.Cells[9].Text.Trim().Equals("T"))
            {
                e.Row.Cells[9].Text = "Template";
                for (int q = 0; q < e.Row.Cells.Count; q++)
                {
                    e.Row.Cells[q].ForeColor = System.Drawing.Color.SlateBlue;
                }
            }
            if (e.Row.Cells[9].Text.Trim().Equals("I"))
            {
                e.Row.Cells[9].Text = "Particular";
                for (int q = 0; q < e.Row.Cells.Count; q++)
                {
                    e.Row.Cells[q].ForeColor = System.Drawing.Color.FromName("#8181F7");
                }
            }
        }
    }

    protected void GrdtxtDiscPer_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Decimal Tax = !string.IsNullOrEmpty(txt.Text) ? Convert.ToDecimal(txt.Text) : 0;
        GridViewRow grd = (GridViewRow)txt.Parent.Parent;
        decimal Amt = Convert.ToDecimal((Convert.ToDecimal(((TextBox)grd.FindControl("GrdtxtInwardQty")).Text)) * (Convert.ToDecimal(((TextBox)grd.FindControl("GrdtxtInwardRate")).Text))); //Convert.ToDecimal(((TextBox)grd.FindControl("GrdtxtAmount")).Text);
        decimal TaxAmount = Tax * Amt / 100;
        ((TextBox)grd.FindControl("GrdtxtDiscAmt")).Text = TaxAmount.ToString();



        ((TextBox)grd.FindControl("GrdtxtTaxAmnt")).Text = Convert.ToString(((Convert.ToDecimal(string.IsNullOrEmpty(((TextBox)grd.FindControl("GrdtxtTax")).Text) ? "0" : ((TextBox)grd.FindControl("GrdtxtTax")).Text)) * ((Convert.ToDecimal((Convert.ToDecimal(((TextBox)grd.FindControl("GrdtxtInwardQty")).Text)) * (Convert.ToDecimal(((TextBox)grd.FindControl("GrdtxtInwardRate")).Text)))) - Convert.ToDecimal(string.IsNullOrEmpty(((TextBox)grd.FindControl("GrdtxtDiscAmt")).Text) ? "0" : ((TextBox)grd.FindControl("GrdtxtDiscAmt")).Text))) / 100);
        


        decimal Amt1 = Convert.ToDecimal(((TextBox)grd.FindControl("GrdtxtTaxAmnt")).Text);
        decimal NetAmount = Amt - TaxAmount+Amt1;
        ((TextBox)grd.FindControl("GrdtxtNetAmnt")).Text = NetAmount.ToString();
        decimal subtot = 0;
        decimal Vat = 0;
        decimal Disc = 0;
        DataTable dtGrid = (DataTable)ViewState["CurrentTable"];
        for (int i = 0; i < dtGrid.Rows.Count; i++)
        {
            subtot += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].Cells[12].FindControl("GrdtxtAmount")).Text);
            NetAmt += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].Cells[17].FindControl("GrdtxtNetAmnt")).Text);
            Vat += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].Cells[14].FindControl("GrdtxtTaxAmnt")).Text);
            Disc += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].Cells[14].FindControl("GrdtxtDiscAmt")).Text);
        }
        txtSubTotal.Text = subtot.ToString("0.00");
        txtNetTotal.Text = NetAmt.ToString("0.00");
        
       // GrdtxtTax_TextChanged(sender, e);
        
        //---Set Discount---
        CalCulate();
        //txtDiscountPer.Text=DisPer.ToString("0.00");          for trial
        txtDiscount.Text=Disc.ToString("0.00");
        txtVATAmount.Text = Vat.ToString("0.00");
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "CalcVatdiscPer", "javascript:CalcVatdiscPer();", true);
        //ddlItems.Focus();
        ((DropDownList)grd.FindControl("ddlLocation")).Focus();
    }

    protected void GrdInwardPO_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int DeleteId = 0;
        try
        {
            int ind = e.RowIndex;
            decimal Tax = 0, Taxamnt = 0, Dis = 0, Disamnt = 0, NoOfRowsTax = 0, NoOfRowsDisc = 0;
            //DeleteId = Convert.ToInt32(((ImageButton)GrdInwardPO.Rows[e.RowIndex].Cells[0].FindControl("ImgBtnDelete")).CommandArgument.ToString());
            DeleteId = Convert.ToInt32(GrdInwardPO.Rows[e.RowIndex].Cells[2].Text.ToString()); //Convert.ToInt32((GrdInwardPO.Rows[e.RowIndex].Cells[2]).ToString());
            if (DeleteId != 0)
            {
                if (ViewState["CurrentTable"] != null)
                {
                    
                    DataTable dttable = (DataTable)ViewState["CurrentTable"];
                    dttable.Rows.RemoveAt(ind);
                    GrdInwardPO.DataSource = dttable;
                    GrdInwardPO.DataBind();
                    DataSet DsCUnit = new DataSet();
                    for (int i = 0; i < GrdInwardPO.Rows.Count; i++)
                    {
                        int ITEMID = Convert.ToInt32(GrdInwardPO.Rows[i].Cells[2].Text.ToString());
                        int UnitID = Convert.ToInt32(GrdInwardPO.Rows[i].Cells[25].Text.ToString());
                        string ItemDesc = Convert.ToString(GrdInwardPO.Rows[i].Cells[7].Text.ToString());
                        decimal Qty = !string.IsNullOrEmpty(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardQty")).Text.ToString()) ? Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtInwardQty")).Text.ToString()) : 0;
                        DsCUnit = Obj_MaterialInwardReg.GetSubUnit(ITEMID, UnitID, Qty,ItemDesc, out StrError);
                        if (DsCUnit.Tables[0].Rows.Count > 0)
                        {
                            AjaxControlToolkit.ComboBox ddlUnit = (AjaxControlToolkit.ComboBox)GrdInwardPO.Rows[i].FindControl("ddlUnit");
                            ddlUnit.DataSource = DsCUnit.Tables[0];
                            ddlUnit.DataTextField = "UnitFactor";
                            ddlUnit.DataValueField = "#";
                            ddlUnit.DataBind();
                            // ddlUnit.SelectedValue = (Convert.ToInt32(dttable1.Rows[e.Row.RowIndex]["UnitConvDtlsId"])).ToString();
                        }
                    }
                    decimal a = 0, b = 0, c = 0, d = 0, g = 0;
                    if (GrdInwardPO.Rows.Count > 0)
                    {
                        for (int i = 0; i < GrdInwardPO.Rows.Count; i++)
                        {
                            ((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text = !string.IsNullOrEmpty(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text) ? (((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text) : "0";
                            ((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text = !string.IsNullOrEmpty(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text) ? (((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text) : "0";
                            if (Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text) > 0)
                            {
                                NoOfRowsTax = NoOfRowsTax + 1;
                            }
                            if (Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text) > 0)
                            {
                                NoOfRowsDisc = NoOfRowsDisc + 1;
                            }
                            Tax += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTax")).Text);
                            Taxamnt += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtTaxAmnt")).Text);
                            Dis += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscPer")).Text);
                            Disamnt += Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtDiscAmt")).Text);

                            a += string.IsNullOrEmpty((((TextBox)GrdInwardPO.Rows[i].Cells[12].FindControl("GrdtxtAmount")).Text)) ? 0 : Convert.ToDecimal((((TextBox)GrdInwardPO.Rows[i].Cells[12].FindControl("GrdtxtAmount")).Text));
                            b += string.IsNullOrEmpty((((TextBox)GrdInwardPO.Rows[i].Cells[13].FindControl("GrdtxtTax")).Text)) ? 0 : Convert.ToDecimal((((TextBox)GrdInwardPO.Rows[i].Cells[13].FindControl("GrdtxtTax")).Text));
                            c += string.IsNullOrEmpty((((TextBox)GrdInwardPO.Rows[i].Cells[14].FindControl("GrdtxtTaxAmnt")).Text)) ? 0 : Convert.ToDecimal((((TextBox)GrdInwardPO.Rows[i].Cells[14].FindControl("GrdtxtTaxAmnt")).Text));
                            d += string.IsNullOrEmpty((((TextBox)GrdInwardPO.Rows[i].Cells[16].FindControl("GrdtxtDiscPer")).Text)) ? 0 : Convert.ToDecimal((((TextBox)GrdInwardPO.Rows[i].Cells[14].FindControl("GrdtxtDiscPer")).Text));
                            g += string.IsNullOrEmpty((((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtNetAmnt")).Text)) ? 0 : Convert.ToDecimal((((TextBox)GrdInwardPO.Rows[i].FindControl("GrdtxtNetAmnt")).Text));
                        }

                        if (NoOfRowsDisc > 0)
                        {
                            DisPer = Dis / NoOfRowsDisc;
                            DisAmt = Disamnt;
                        }
                        if (NoOfRowsTax > 0)
                        {
                            TaxPer = Tax / NoOfRowsTax;
                            TaxAmt = Taxamnt;
                        }
                        else
                        {
                            TaxPer = Tax / 1;
                            TaxAmt = Taxamnt;
                            DisPer = Dis / 1;
                            DisAmt = Disamnt;
                        }
                        txtSubTotal.Text = a.ToString("0.00");
                        txtNetTotal.Text = g.ToString("0.00");
                        txtVATPer.Text=TaxPer.ToString("0.00");
                        txtVATAmount.Text = Taxamnt.ToString("0.00");
                        txtDiscountPer.Text=DisPer.ToString("0.00");
                        txtDiscount.Text = Disamnt.ToString("0.00");
                    }
                    else
                    {
                        txtSubTotal.Text = "0.00";
                        txtNetTotal.Text ="0.00";
                        txtVATPer.Text="0.00";
                        txtVATAmount.Text ="0.00";
                        txtDiscountPer.Text="0.00";
                        txtDiscount.Text ="0.00";
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void ReportGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.ReportGrid.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            Ds = Obj_MaterialInwardReg.GetReportGridInward(StrCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ReportGrid.DataSource = Ds.Tables[0];
                this.ReportGrid.DataBind();

                //-----For UserRights------
                if (FlagDel && FlagEdit)
                {
                    foreach (GridViewRow GRow in ReportGrid.Rows)
                    {
                        GRow.FindControl("ImgBtnDelete").Visible = false;
                        GRow.FindControl("ImageGridEdit").Visible = false;
                    }
                }
                else if (FlagDel)
                {
                    foreach (GridViewRow GRow in ReportGrid.Rows)
                    {
                        GRow.FindControl("ImgBtnDelete").Visible = false;
                    }
                }
                else if (FlagEdit)
                {
                    foreach (GridViewRow GRow in ReportGrid.Rows)
                    {
                        GRow.FindControl("ImageGridEdit").Visible = false;
                    }
                }
            }
            else
            {
                //SetInitialRow_ReportGrid();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void txtBillNo_TextChanged(object sender, EventArgs e)
    {

        try
        {
            DataSet DS = new DataSet();
            bool billflag = false;
            if (rdoTemplateItem.SelectedValue == "1")
            {
                if (Convert.ToInt32(ddlSuplier.SelectedValue) == 0)
                {
                    billflag = true;
                }
            }
            else
            {
                billflag = false;
            }

            if (billflag==false)
            {
                Ds = Obj_MaterialInwardReg.Get_BillNo(txtBillNo.Text.Trim(), Convert.ToInt32(ddlSuplier.SelectedValue), out StrError);
                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {

                    obj_Comman.ShowPopUpMsg("This Bill No Already Present," + "\n" + " For This Please Refer Inward No:-  " + Ds.Tables[0].Rows[0]["InwardNo"].ToString()+"\n \b"+" Against Suplier:- "+ddlSuplier.SelectedItem, this.Page);
                    txtBillNo.Focus();
                }
                else
                {
                    txtBillDate.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void hyl_Hide_Click(object sender, EventArgs e)
    {
        try
        {
            TR_RateDtls.Visible = false;
        }
        catch (Exception )
        {
        }
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow grd = (GridViewRow)ddl.Parent.Parent;
        ((TextBox)grd.FindControl("lblLocID")).Text = ((DropDownList)grd.FindControl("ddlLocation")).SelectedValue;
        //ddlItems.Focus();
        ((DropDownList)grd.FindControl("ddlLocation")).Focus();
    }

    protected void IMGITEMREFRESH_Click(object sender, ImageClickEventArgs e)//Add Fiels By Piyush om dated 09-AUG-2013
    {
        try
        {
            Ds = Obj_MaterialInwardReg.FillCombo(out StrError);
            ViewState["DSTABLEITEM"] = Ds.Tables[5];
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ddlPONo.DataSource = Ds.Tables[0];
                ddlPONo.DataTextField = "PONo";
                ddlPONo.DataValueField = "POId";
                ddlPONo.DataBind();
            }
            if (Ds.Tables.Count > 0 && Ds.Tables[1].Rows.Count > 0)
            {
                ddlTemplate.DataSource = Ds.Tables[1];
                ddlTemplate.DataValueField = "TemplateID";
                ddlTemplate.DataTextField = "TemplateName";
                ddlTemplate.DataBind();
            }
            if (Ds.Tables.Count > 0 && Ds.Tables[2].Rows.Count > 0)
            {
                ddlCategory.DataSource = Ds.Tables[2];
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataBind();
            }
            if (Ds.Tables.Count > 0 && Ds.Tables[3].Rows.Count > 0)
            {
                ddlItems.DataSource = Ds.Tables[3];
                ddlItems.DataValueField = "ItemId";
                ddlItems.DataTextField = "ItemName";
                ddlItems.DataBind();
            }
            
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void lnkCompany_Click(object sender, EventArgs e)
    {
        try
        {
            string[] filePaths = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Temp/"));
            foreach (string filePath in filePaths)
            System.IO.File.Delete(filePath);
            Random random = new Random();

            if (CompanyLogoUpload.HasFile)
            {
                //--Total No of Files--
                Int64 TotalFiles = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Imgupload")).Count();

                string filename = System.IO.Path.GetFileName(CompanyLogoUpload.FileName);
                filename = TotalFiles + "-" + filename;
                CompanyLogoUpload.SaveAs(Server.MapPath("~/Images/Temp/") + filename);

                //==========USed For Resize Image to Gal Size===================
                System.Drawing.Image GalImage = obj_Comman.ResizeImage(System.Drawing.Image.FromFile(Server.MapPath("~/Images/Temp/") + filename), 200, 200);
                GalImage.Save(Server.MapPath("~/Images/Imgupload/") + filename);
                GalImage = null;
                //==========USed For Resize Image to Gal Size===================   


                lblLogopath.Text = "~/Images/Imgupload/" + filename;
                //ImgDone.Visible = true;
                ImgCompanyLogo.ImageUrl = @"~/Images/Imgupload/" + filename;
                ImgCompanyLogo.DataBind();
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Please Choose File For Upload..", this.Page);
            }
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this.Page);
        }
    }

    protected void lnkCompanyCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ImgCompanyLogo.ImageUrl = "";
            lblLogopath.Text = "";

        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg("Upload status: The file could not be Unloaded. The following error occured: " + ex.Message, this.Page);
        }

    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region[Convert Quantity accordng to UnitFactor]
        //---Coversionfactor---
        AjaxControlToolkit.ComboBox ImgAddSupplier1 = (AjaxControlToolkit.ComboBox)sender;
        GridViewRow row = (GridViewRow)ImgAddSupplier1.NamingContainer;
        int CurrRow = row.RowIndex;
        int UnitConvDtlsIdT = 0,ItemId=0;
        
        for (int i = 0; i < GrdInwardPO.Rows.Count; i++)
        {
            UnitConvDtlsIdT = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdInwardPO.Rows[CurrRow].FindControl("ddlUnit")).SelectedValue);
            TextBox txtActualQty = (TextBox)GrdInwardPO.Rows[CurrRow].FindControl("GrdtxtActualQty");
            TextBox txtPendingOrderQty = (TextBox)GrdInwardPO.Rows[CurrRow].FindControl("GrdtxtUnitOrdQty");

            TextBox TXTINQTY = (TextBox)GrdInwardPO.Rows[CurrRow].FindControl("GrdtxtInwardQty");


            ItemId = Convert.ToInt32(GrdInwardPO.Rows[CurrRow].Cells[2].Text);
            string ItemDesc =(GrdInwardPO.Rows[CurrRow].Cells[7].Text.ToString()).Equals("&nbsp;")?"": Convert.ToString(GrdInwardPO.Rows[CurrRow].Cells[7].Text.ToString());
            TextBox TXTInwardRate = (TextBox)GrdInwardPO.Rows[CurrRow].FindControl("GrdtxtInwardRate");
            int POId = 0;
            if (rdoInwardType.SelectedValue == "0")
            {
                POId = Convert.ToInt32(ddlPONo.SelectedValue);
            }
          //  string unitconvrt = (((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlUnitConvertor")).SelectedItem).ToString();
            DataSet DsTemp = new DataSet();
            DsTemp = Obj_MaterialInwardReg.GetFactor(UnitConvDtlsIdT, ItemId, Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[CurrRow].FindControl("GrdtxtInwardQty")).Text), ItemDesc, string.IsNullOrEmpty(TXTInwardRate.Text) ? 0 : Convert.ToDecimal(TXTInwardRate.Text.Trim()),POId, out StrError);
            if (DsTemp.Tables.Count > 0)
            {
                for (int j = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString())!=1)
                    {
                        txtActualQty.Text = (Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString())).ToString();
                        string[] gvalue = (((AjaxControlToolkit.ComboBox)GrdInwardPO.Rows[CurrRow].FindControl("ddlUnit")).SelectedItem).ToString().Split('-');
                       if (gvalue.Length > 1)
                       {
                           txtPendingOrderQty.Text = gvalue[1].ToString();
                           TXTINQTY.Text = Convert.ToDecimal(gvalue[1].ToString()).ToString("#0.00") ; 
                           TXTInwardRate.Text = DsTemp.Tables[1].Rows[0]["Rate"].ToString();
                       }
                       
                    }
                    else
                    {
                        txtActualQty.Text = (Convert.ToDecimal(((TextBox)GrdInwardPO.Rows[CurrRow].FindControl("GrdtxtInwardQty")).Text)).ToString();
                        string[] gvalue = (((AjaxControlToolkit.ComboBox)GrdInwardPO.Rows[CurrRow].FindControl("ddlUnit")).SelectedItem).ToString().Split('-');
                        if (gvalue.Length > 1)
                        {
                            txtPendingOrderQty.Text = gvalue[1].ToString();
                            TXTINQTY.Text = Convert.ToDecimal(gvalue[1].ToString()).ToString("#0.00"); 
                            TXTInwardRate.Text = DsTemp.Tables[1].Rows[0]["Rate"].ToString();
                        }
                        
                    }
                    
                }
            }
        }
        #endregion
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionItemNameList(string prefixText, int count, string contextKey)
    {
        DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
        String[] SearchList = Obj_RequisitionCafeteria.GetSuggestedRecordItems(prefixText, "");
        return SearchList;
    }

    protected void TxtItemName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            StrCondition = string.Empty;
            StrCondition = TxtItemName.Text.Trim();
            Ds = new DataSet();
            DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
            Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                TxtItemName.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                TxtItemName.ToolTip = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                ddlItems.SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                Ds = null;
            }
            else
            {
                TxtItemName.Text = "";
                TxtItemName.ToolTip = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
