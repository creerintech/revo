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

public partial class Masters_GETFACTORFORITEMS : System.Web.UI.Page
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
    #endregion
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

        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        GridDetails.DataSource = dt;
        GridDetails.DataBind();
    }

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
                    //if (dtCulUnitTable.Rows.Count == 1 && string.IsNullOrEmpty(dtCulUnitTable.Rows[0]["From_UnitName"].ToString()))
                    //{
                    //    dtCulUnitTable.Rows.RemoveAt(0);
                    //}

                    //Save First Calculation Factor (Means : A=How Much B)
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
                        dtTableRow["Factor_Desc"] = Convert.ToString(GridDetails.Rows[k].Cells[9].Text);
                    }

                    dtCulUnitTable.Rows.Add(dtTableRow);

                    //Save First Calculation Factor (Means : B=How Much A)

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
                        dtTableRow["Factor_Desc"] = Convert.ToString(GridDetails.Rows[k].Cells[9].Text);
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetInitialRow();
            SetInitialRow_CalUnit();
        }
    }
    protected void BTNADD_Click(object sender, EventArgs e)
    {
        try
        {
            Ds = Obj_ItemMaster.GetItem("", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                for (int InsertRow = 0; InsertRow <= Ds.Tables[0].Rows.Count - 1; InsertRow++)//Ds.Tables[0].Rows.Count - 1
                {
                    int ItemID =Convert.ToInt32(Ds.Tables[0].Rows[InsertRow]["#"].ToString());
                    DataSet DSS = new DataSet();
                    DSS = Obj_ItemMaster.GetItemDetailsForCalculateFactor(ItemID, out StrError);
                    if (DSS.Tables.Count > 0 && DSS.Tables[0].Rows.Count > 0)
                    {
                        GridDetails.DataSource = DSS.Tables[0];
                        GridDetails.DataBind();
                        Fill_CalulateGrid();
                    }
                    #region[Save Dtls To ItemDtlsUnitCalculation]
                    for (int i = 0; i < GrdUnitCal.Rows.Count; i++)
                    {
                        Entity_ItemMaster.ItemId = ItemID;
                        Entity_ItemMaster.From_Factor = Convert.ToDecimal(GrdUnitCal.Rows[i].Cells[3].Text);
                        Entity_ItemMaster.From_UnitID = Convert.ToInt32(GrdUnitCal.Rows[i].Cells[4].Text);
                        Entity_ItemMaster.To_Factor = Convert.ToDecimal(GrdUnitCal.Rows[i].Cells[6].Text);
                        Entity_ItemMaster.To_UnitID = Convert.ToInt32(GrdUnitCal.Rows[i].Cells[7].Text);
                        Entity_ItemMaster.Factor_Desc = GrdUnitCal.Rows[i].Cells[9].Text.Equals("&nbsp;") ? "" : (GrdUnitCal.Rows[i].Cells[9].Text);
                        int InsertUnitCal = Obj_ItemMaster.Insert_UnitCalculation(ref Entity_ItemMaster, out StrError);
                    }
                    #endregion
                    SetInitialRow();
                    SetInitialRow_CalUnit();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}
