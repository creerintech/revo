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

using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DALSQLHelper;
using MayurInventory.DataModel;
using System.Text.RegularExpressions;

public partial class TempFiles_MatTransferLocation1 : System.Web.UI.Page
{
    #region [Private Variables]
        DMTransferLocation Obj_Trans = new DMTransferLocation();
        TransferLocation Entity_Trans = new TransferLocation();
        CommanFunction obj_Comm = new CommanFunction();
        DataSet DS = new DataSet();
        string TransferNum = string.Empty;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        int CategoryID;
        string CategoryName;
        decimal Qty3 = 0;
        public static bool flag = false;
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
    #endregion

    #region[UserDefined Function]

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

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Transfer Register'");
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
                            GrdReport.Visible = false;
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
                            foreach (GridViewRow GRow in GrdReport.Rows)
                            {
                                GRow.FindControl("ImgBtnDelete").Visible = false;
                                GRow.FindControl("ImgBtnEdit").Visible = false;
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
                                foreach (GridViewRow GRow in GrdReport.Rows)
                                {
                                    GRow.FindControl("ImgBtnDelete").Visible = false;
                                    FlagDel = true;
                                }
                            }

                            //Checking Edit Right ========
                            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                            {
                                foreach (GridViewRow GRow in GrdReport.Rows)
                                {
                                    GRow.FindControl("ImgBtnEdit").Visible = false;
                                    FlagEdit = true;
                                }
                            }

                            //Checking Print Right ========
                            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                            {
                                foreach (GridViewRow GRow in GrdReport.Rows)
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
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    //User Right Function===========

    private void SetInitialRowReport()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("#", typeof(int));
        dt.Columns.Add("TransId", typeof(int));
        dt.Columns.Add("TransNo", typeof(string));
        dt.Columns.Add("Date", typeof(string));
        dt.Columns.Add("TransBy", typeof(string));
        dr = dt.NewRow();
        dr["#"] = 0;
        dr["TransId"]=0;
        dr["TransNo"] = "";
        dr["Date"] = "";
        dr["TransBy"] = "";
        dt.Rows.Add(dr);

        ViewState["CurrTable"] = dt;
        GrdReport.DataSource = dt;
        GrdReport.DataBind();
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("#", typeof(int));
        dt.Columns.Add("CategoryId", typeof(int));
        dt.Columns.Add("ItemId", typeof(int));
        dt.Columns.Add("ItemDescID", typeof(int));
        dt.Columns.Add("Item", typeof(string));
        dt.Columns.Add("ItemDesc", typeof(string));
        dt.Columns.Add("Unit", typeof(string));
        dt.Columns.Add("CategoryName", typeof(string));
        dt.Columns.Add("TransFrom", typeof(string));//AvlQtySou
        dt.Columns.Add("AvlQtySou", typeof(decimal));
        dt.Columns.Add("StockLocationID", typeof(int));
        dt.Columns.Add("TransTo", typeof(string));
        dt.Columns.Add("AvlQty", typeof(decimal));
        dt.Columns.Add("Qty", typeof(decimal));
        dt.Columns.Add("rate", typeof(decimal));
   
        dt.Columns.Add("OriQtyatSource", typeof(decimal));
        dt.Columns.Add("OriQtyatDest", typeof(decimal));
        dt.Columns.Add("OriTrnasfer", typeof(decimal));
        dt.Columns.Add("UnitID", typeof(int)); 
        dr = dt.NewRow();
        dr["#"] = 0;
        dr["CategoryId"] = 0;
        dr["CategoryName"] = "";
        dr["ItemId"] = 0;
        dr["ItemDescID"] = 0;
        dr["ItemDesc"] = "";
        dr["Item"] = "";
        dr["Unit"] = "";
        dr["TransFrom"] = string.Empty;
        dr["AvlQtySou"] = 0.00;
        dr["StockLocationID"] = 0;
        dr["TransTo"] = string.Empty;
        dr["AvlQty"] = 0.00;
        dr["Qty"] = 0.00;
        dr["rate"] = 0.00;
        //dr["Notes"] = string.Empty;
        //dr["TransFromID"] = 0;
        dr["OriQtyatSource"] = 0;
        dr["OriQtyatDest"] = 0.0;
        dr["OriTrnasfer"] = 0.0;
        dr["UnitID"] = 0;
       
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        GridDetails.DataSource = dt;
        GridDetails.DataBind();
    }

    private void MakeEmptyForm()
    {
        ViewState["EditId"] = null;
        ViewState["GridIndex"] = null;
        ViewState["QtyAtSource"] = null;
        ViewState["QtyAtDest"] = null;
        ViewState["TrnasferQty"] = null; 
        ViewState["EditFlag"] = null;

        ddlBy.SelectedValue = "0";
        TxtTranNo.Text = string.Empty;
        TRNO.Visible = false;
        TxtTranNo.Enabled = false;
        TxtDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
        txtNotes.Text = string.Empty;
        TxtSearch.Text = string.Empty;

        if(!FlagAdd)
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;

        BtnCancel.Visible = true;
        TransNo();
        SetInitialRow();
       
        ReportGrid(StrCondition);

        #region[UserRights]
        if (FlagEdit)
        {
            foreach (GridViewRow GRow in GrdReport.Rows)
            {
                GRow.FindControl("ImageGridEdit").Visible = false;
            }
        }
        if (FlagDel)
        {
            foreach (GridViewRow GRow in GrdReport.Rows)
            {
                GRow.FindControl("ImgBtnDelete").Visible = false;
            }
        }
        if (FlagPrint)
        {
            foreach (GridViewRow GRow in GrdReport.Rows)
            {
                GRow.FindControl("ImgBtnPrint").Visible = false;
            }
        }
        #endregion
    }

    private void ReportGrid(string RepCondition)
    {
        try
        {
            string COND = string.Empty;
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                COND = COND + " AND TLC.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            DS = Obj_Trans.GetTransferList(RepCondition,COND, out StrError);

            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GrdReport.DataSource = DS;
                GrdReport.DataBind();
            }
            else
            {
                 SetInitialRowReport();
            }
            Obj_Trans = null;
            DS = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    private void MakeControlEmpty()
    {

        ddlCategory.SelectedValue = "0";
        ddlItem.SelectedValue="0";
       //TxtItemName.Text = string.Empty;
       // ddldescription.SelectedValue = "0";
        txtQuantAtDest.Text = lblQuantAtSource.Text = "0.00";
        ddlTansTo.SelectedValue = "0";
        txtTansQty.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtRate.Text = "0.00";
        LBLUNIT.Text = string.Empty;
        ViewState["GridIndex"] = null;
        ViewState["GridDetails"] = null;
        ImgAddGrid.ImageUrl = "~/Images/Icon/Gridadd.png";
        ImgAddGrid.ToolTip = "Add Grid";

    }

    private void FillCombo()
    {
        DS = Obj_Trans.FillCombo(out StrError);
        if (DS.Tables.Count > 0)
        {
            if (DS.Tables[0].Rows.Count > 0)
            {
               //ddlBy.DataSource = DS.Tables[0];
               //ddlBy.DataTextField = "EmpName";
               //ddlBy.DataValueField = "EmployeeId";
               //ddlBy.DataBind();
            }
            if (DS.Tables[1].Rows.Count > 0)
            {
                ddlTansTo.DataSource = DS.Tables[1];
                ddlTansTo.DataTextField = "Location";
                ddlTansTo.DataValueField = "StockLocationID";
                ddlTansTo.DataBind();
            }
            if (DS.Tables[2].Rows.Count > 0)
            {
                ddlCategory.DataSource = DS.Tables[2];
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();
            }
            if (DS.Tables[3].Rows.Count > 0)
            {
                ddlItem.DataSource = DS.Tables[3];
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "ItemId";
                ddlItem.DataBind();
            }
            if (DS.Tables[4].Rows.Count > 0)
            {
                ddlSubCategory.DataSource = DS.Tables[4];
                ddlSubCategory.DataTextField = "SubCategory";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataBind();
            }
        }
    }

    public void TransNo()
    {
        try
        {
            DataSet DsCode = new DataSet();
            DsCode = Obj_Trans.GetTransNo(out StrError);
            if (DsCode.Tables[0].Rows.Count > 0)
            {
                TransferNum = DsCode.Tables[0].Rows[0]["TransNo"].ToString();
            }
            TxtTranNo.Text = TransferNum;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void CheckEmployeeName(Label LableEmpName)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["Location"])))
        {
            lblLocation.Text = Session["Location"].ToString();

        }
        else
        {
            Response.Redirect("~/Masters/Default.aspx");
        }
    }

    private void CheckEmployeeName1(Label LableEmpName)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
        {
            LblEmployee.Text = Session["UserName"].ToString();
        }
        else
        {
            Response.Redirect("~/Masters/Default.aspx");
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillCombo();
            MakeControlEmpty();
            MakeEmptyForm();
            CheckEmployeeName(lblLocation);
            CheckEmployeeName1(LblEmployee);
            CheckUserRight();
        }
    }
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        ReportGrid(StrCondition);
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]

    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMTransferLocation Obj_Trans = new DMTransferLocation();
        String[] SearchList = Obj_Trans.GetSuggestRecord(prefixText);
        return SearchList;
    }

    protected void ImgAddGrid_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedValue == "0")
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            if (flag == true)
            {
                int ItemID = Convert.ToInt32(ddlItem.SelectedValue);
                DS = Obj_Trans.GetCategory(ItemID, out StrError);
                CategoryID = Convert.ToInt32(DS.Tables[0].Rows[0]["CategoryId"]);
                CategoryName = Convert.ToString(DS.Tables[0].Rows[0]["CategoryName"]);

            }
            else
            {
                CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                CategoryName= ddlCategory.SelectedItem.ToString();
            }

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrntTable = (DataTable)ViewState["CurrentTable"];
                bool DupFlag = false;
                int k = 0;
                DataRow dtTableRow = null;
                if (dtCurrntTable.Rows.Count >= 1)
                {
                    decimal Qty1 = Convert.ToDecimal(txtTansQty.Text);
                    //decimal Qty2 = Convert.ToDecimal(ViewState["QtyAtSource"]);
                    string[] getqty = ((ddlUnitConversion.SelectedItem.Text.ToString()).Split('-'));
                    string Qty2 = Regex.Match((ddlUnitConversion.SelectedItem.Text.ToString()), @"-\d+").Value;

                    if (Qty2 == "")
                    {
                        Qty2 = "0";
                    }
                    else
                    {
                         Qty3 = Convert.ToDecimal(Qty2);
                    }
                  
                    decimal Qty4 = Math.Abs(Qty3);
                     int locationID = Convert.ToInt32(Session["CafeteriaId"]);
                    int TransferTo=Convert.ToInt32(ddlTansTo.SelectedValue.ToString());
                    if (locationID != TransferTo)
                    {
                        if (Qty1 <= Qty4)
                        {
                            if (dtCurrntTable.Rows.Count >= 1 && dtCurrntTable.Rows[0]["Item"].ToString().Equals(""))
                            {
                                dtCurrntTable.Rows.RemoveAt(0);
                            }
                            if (ViewState["GridIndex"] != null)
                            {
                                for (int i = 0; i < dtCurrntTable.Rows.Count; i++)
                                {
                                    if ((Convert.ToInt32(dtCurrntTable.Rows[i]["ItemId"]) == Convert.ToInt32(ddlItem.SelectedValue))
                                        && (Convert.ToInt32(dtCurrntTable.Rows[i]["StockLocationID"]) == Convert.ToInt32(ddlTansTo.SelectedValue))
                                        && (Convert.ToInt32(dtCurrntTable.Rows[i]["ItemDescID"]) == Convert.ToInt32(ddldescription.SelectedValue))
                                        )
                                    {
                                        DupFlag = true;
                                        k = i;
                                    }
                                }
                                if (DupFlag == true)
                                {                                   
                                    dtCurrntTable.Rows[k]["CategoryName"] = CategoryName;
                                    dtCurrntTable.Rows[k]["CategoryId"] = CategoryID;                                 
                                    dtCurrntTable.Rows[k]["ItemId"] = Convert.ToInt32(ddlItem.SelectedValue);
                                    dtCurrntTable.Rows[k]["Item"] = ddlItem.SelectedItem;
                                    dtCurrntTable.Rows[k]["ItemDescID"] = Convert.ToInt32(ddldescription.SelectedValue);
                                    dtCurrntTable.Rows[k]["ItemDesc"] = Convert.ToString(ddldescription.SelectedItem.ToString()).Equals("--Select Description--") ? "" : ddldescription.SelectedItem.ToString();
                                    dtCurrntTable.Rows[k]["TransFrom"] = lblLocation.Text;

                                    dtCurrntTable.Rows[k]["StockLocationID"] = ddlTansTo.SelectedValue;
                                    if (Convert.ToBoolean(ViewState["EditFlag"]))
                                    {
                                        dtCurrntTable.Rows[k]["AvlQtySou"] = (Convert.ToDecimal(ViewState["QtyAtSource"])) - Convert.ToDecimal(txtTansQty.Text);
                                        dtCurrntTable.Rows[k]["AvlQty"] = (Convert.ToDecimal(ViewState["QtyAtDest"])) + Convert.ToDecimal(txtTansQty.Text);
                                    }
                                    else
                                    {
                                        dtCurrntTable.Rows[k]["AvlQtySou"] = (Convert.ToDecimal(ViewState["QtyAtSource"])) - Convert.ToDecimal(txtTansQty.Text);
                                        dtCurrntTable.Rows[k]["AvlQty"] = (Convert.ToDecimal(ViewState["QtyAtDest"])) + Convert.ToDecimal(txtTansQty.Text);
                                    }                                  
                                    dtCurrntTable.Rows[k]["TransTo"] = ddlTansTo.SelectedItem;
                                    dtCurrntTable.Rows[k]["Qty"] = txtTansQty.Text;
                                    dtCurrntTable.Rows[k]["rate"] = txtRate.Text;
                                    dtCurrntTable.Rows[k]["Unit"] = ddlUnitConversion.SelectedItem.ToString();
                                    dtCurrntTable.Rows[k]["UnitID"] = Convert.ToInt32(ddlUnitConversion.SelectedValue.ToString());
                                    dtCurrntTable.Rows[k]["OriQtyatSource"] = Convert.ToDecimal(ViewState["QtyAtSource"]);
                                    dtCurrntTable.Rows[k]["OriQtyatDest"] = Convert.ToDecimal(ViewState["QtyAtDest"]);                                  
                                    ViewState["CurrentTable"] = dtCurrntTable;
                                    GridDetails.DataSource = dtCurrntTable;
                                    GridDetails.DataBind();
                                    MakeControlEmpty();
                                }
                                else
                                {
                                    int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                                    dtCurrntTable.Rows[rowindex]["CategoryId"] = CategoryID;
                                    dtCurrntTable.Rows[rowindex]["CategoryName"] = CategoryName;
                                    dtCurrntTable.Rows[rowindex]["Item"] = ddlItem.SelectedItem;
                                    dtCurrntTable.Rows[rowindex]["ItemId"] = ddlItem.SelectedValue;
                                    dtCurrntTable.Rows[rowindex]["ItemDescID"] = Convert.ToInt32(ddldescription.SelectedValue);
                                    dtCurrntTable.Rows[rowindex]["ItemDesc"] = Convert.ToString(ddldescription.SelectedItem.ToString()).Equals("--Select Description--") ? "" : ddldescription.SelectedItem.ToString();
                                    dtCurrntTable.Rows[rowindex]["TransFrom"] = lblLocation.Text;
                                    dtCurrntTable.Rows[rowindex]["AvlQtySou"] = Convert.ToDecimal(lblQuantAtSource.Text) - Convert.ToDecimal(txtTansQty.Text);//lblQuantAtSource.Text;
                                    dtCurrntTable.Rows[rowindex]["StockLocationID"] = ddlTansTo.SelectedValue;
                                    dtCurrntTable.Rows[rowindex]["TransTo"] = ddlTansTo.SelectedItem;
                                    dtCurrntTable.Rows[rowindex]["AvlQty"] = Convert.ToDecimal(txtQuantAtDest.Text) + Convert.ToDecimal(txtTansQty.Text);// txtQuantAtDest.Text;
                                    dtCurrntTable.Rows[rowindex]["Qty"] = txtTansQty.Text;
                                    dtCurrntTable.Rows[rowindex]["rate"] = txtRate.Text;
                                    dtCurrntTable.Rows[rowindex]["Unit"] = ddlUnitConversion.SelectedItem.ToString();
                                    dtCurrntTable.Rows[rowindex]["UnitID"] = Convert.ToInt32(ddlUnitConversion.SelectedValue.ToString());

                                    ViewState["CurrentTable"] = dtCurrntTable;
                                    GridDetails.DataSource = dtCurrntTable;
                                    GridDetails.DataBind();
                                    MakeControlEmpty();
                                }
                            }
                            else
                            {
                                for (int i = 0; i < dtCurrntTable.Rows.Count; i++)
                                {
                                    if ((Convert.ToInt32(dtCurrntTable.Rows[i]["ItemId"]) == Convert.ToInt32(ddlItem.SelectedValue))
                                        && (Convert.ToInt32(dtCurrntTable.Rows[i]["StockLocationID"]) == Convert.ToInt32(ddlTansTo.SelectedValue))
                                        && (Convert.ToInt32(dtCurrntTable.Rows[i]["ItemDescID"]) == Convert.ToInt32(ddldescription.SelectedValue)))
                                    {
                                        DupFlag = true;
                                        k = i;
                                    }
                                }
                                if (DupFlag == true)
                                {
                                    dtCurrntTable.Rows[k]["CategoryName"] = CategoryName;
                                    dtCurrntTable.Rows[k]["CategoryId"] = CategoryID;
                                    //dtCurrntTable.Rows[k]["#"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                                    dtCurrntTable.Rows[k]["ItemId"] = Convert.ToInt32(ddlItem.SelectedValue);
                                    dtCurrntTable.Rows[k]["Item"] = ddlItem.SelectedItem;
                                    dtCurrntTable.Rows[k]["ItemDescID"] = Convert.ToInt32(ddldescription.SelectedValue);
                                    dtCurrntTable.Rows[k]["ItemDesc"] = Convert.ToString(ddldescription.SelectedItem.ToString()).Equals("--Select Description--") ? "" : ddldescription.SelectedItem.ToString();
                                    dtCurrntTable.Rows[k]["TransFrom"] = lblLocation.Text;
                                    dtCurrntTable.Rows[k]["AvlQtySou"] = Convert.ToDecimal(lblQuantAtSource.Text) - Convert.ToDecimal(txtTansQty.Text);//lblQuantAtSource.Text;
                                    dtCurrntTable.Rows[k]["StockLocationID"] = ddlTansTo.SelectedValue;
                                    dtCurrntTable.Rows[k]["TransTo"] = ddlTansTo.SelectedItem;
                                    dtCurrntTable.Rows[k]["AvlQty"] = Convert.ToDecimal(txtQuantAtDest.Text) + Convert.ToDecimal(txtTansQty.Text);// txtQuantAtDest.Text;
                                    dtCurrntTable.Rows[k]["Qty"] = txtTansQty.Text;
                                    dtCurrntTable.Rows[k]["rate"] = txtRate.Text;
                                    dtCurrntTable.Rows[k]["Unit"] = ddlUnitConversion.SelectedItem.ToString();
                                    dtCurrntTable.Rows[k]["UnitID"] = Convert.ToInt32(ddlUnitConversion.SelectedValue.ToString());
                                    ViewState["CurrentTable"] = dtCurrntTable;
                                    GridDetails.DataSource = dtCurrntTable;
                                    GridDetails.DataBind();
                                    MakeControlEmpty();
                                }
                                else
                                {
                                    dtTableRow = dtCurrntTable.NewRow();
                                    dtTableRow["#"] = 0;
                                    dtTableRow["CategoryId"] = CategoryID;//Convert.ToInt32(ddlCategory.SelectedValue);
                                    dtTableRow["CategoryName"] = CategoryName;// ddlCategory.SelectedItem;
                                    dtTableRow["ItemId"] = Convert.ToInt32(ddlItem.SelectedValue);
                                    dtTableRow["Item"] = ddlItem.SelectedItem;
                                    dtTableRow["ItemDescID"] = Convert.ToInt32(ddldescription.SelectedValue);
                                    dtTableRow["ItemDesc"] = Convert.ToString(ddldescription.SelectedItem.ToString()).Equals("--Select Description--") ? "" : ddldescription.SelectedItem.ToString();
                                    dtTableRow["TransFrom"] = lblLocation.Text;
                                    dtTableRow["AvlQtySou"] = Convert.ToDecimal(ViewState["QtyAtSource"]) - Convert.ToDecimal(txtTansQty.Text);
                                    dtTableRow["StockLocationID"] = ddlTansTo.SelectedValue;
                                    dtTableRow["TransTo"] = ddlTansTo.SelectedItem;

                                    dtTableRow["AvlQty"] = Convert.ToDecimal(ViewState["QtyAtDest"]) + Convert.ToDecimal(txtTansQty.Text);
                                    dtTableRow["Qty"] = txtTansQty.Text;
                                    dtTableRow["rate"] = txtRate.Text;
                                    dtTableRow["Unit"] = ddlUnitConversion.SelectedItem.ToString();
                                    dtTableRow["UnitID"] = Convert.ToInt32(ddlUnitConversion.SelectedValue.ToString());
                                    dtTableRow["OriQtyatSource"] = Convert.ToDecimal(ViewState["QtyAtSource"]);
                                    dtTableRow["OriQtyatDest"] = Convert.ToDecimal(ViewState["QtyAtDest"]);

                                    dtCurrntTable.Rows.Add(dtTableRow);

                                    ViewState["CurrentTable"] = dtCurrntTable;
                                    GridDetails.DataSource = dtCurrntTable;
                                    GridDetails.DataBind();
                                    MakeControlEmpty();
                                }
                            }
                        }
                        else
                        {
                            obj_Comm.ShowPopUpMsg("Transfer Quantity Should not be greater than Quantity at Source", this.Page);
                            txtTansQty.Text = string.Empty;
                            txtTansQty.Focus();
                        }
                    }
                    else
                    {
                        obj_Comm.ShowPopUpMsg("Transferred Location Should Not be same", this.Page);
                    }
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
                    obj_Comm.ShowPopUpMsg("There Is No Record To Edit", this.Page);
                }
                else
                {
                    ImgAddGrid.ImageUrl = "~/Images/Icon/GridUpdate.png";
                    ImgAddGrid.ToolTip = "Update";

                    Index = Convert.ToInt32(e.CommandArgument);
                   // ViewState["EditId"] = Convert.ToInt32(e.CommandArgument);
                    ViewState["GridIndex"] = Index;
                    ddlCategory.SelectedValue = GridDetails.Rows[Index].Cells[2].Text;
                    ddlCategory_SelectedIndexChanged(sender,e);
                    ddlItem.SelectedValue = GridDetails.Rows[Index].Cells[4].Text;
                    LBLUNIT.Text = GridDetails.Rows[Index].Cells[6].Text;
                    lblLocation.Text = GridDetails.Rows[Index].Cells[7].Text;
                    ddlTansTo.SelectedValue = GridDetails.Rows[Index].Cells[9].Text;
                    lblQuantAtSource.Text = GridDetails.Rows[Index].Cells[14].Text;
                    txtQuantAtDest.Text = GridDetails.Rows[Index].Cells[15].Text;
                    
                    txtTansQty.Text = GridDetails.Rows[Index].Cells[12].Text;
                    txtRate.Text = GridDetails.Rows[Index].Cells[13].Text;

                    if (Convert.ToBoolean(ViewState["EditFlag"]))
                    {
                        ViewState["QtyAtSource"] =  Convert.ToDecimal(GridDetails.Rows[Index].Cells[14].Text)+Convert.ToDecimal(GridDetails.Rows[Index].Cells[16].Text);
                        ViewState["QtyAtDest"] = Convert.ToDecimal(GridDetails.Rows[Index].Cells[15].Text) - Convert.ToDecimal(GridDetails.Rows[Index].Cells[16].Text); 
                        ViewState["TrnasferQty"] = GridDetails.Rows[Index].Cells[16].Text;
                    }
                    else
                    {
                        ViewState["QtyAtSource"] = GridDetails.Rows[Index].Cells[14].Text;
                        ViewState["QtyAtDest"] = GridDetails.Rows[Index].Cells[15].Text;
                        ViewState["TrnasferQty"] = GridDetails.Rows[Index].Cells[16].Text;
                    }
                 
                  


                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void GridDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //===========Get The Grid View RowIndex See ImgBtnDeleteNew commnad Argumnet for syntax CommandArgument='<%# Eval("#")+ ","+((GridViewRow)Container).RowIndex %>' 
            //int DeleteId = Convert.ToInt32(((ImageButton)GrdBagDtls.Rows[e.RowIndex].Cells[0].FindControl("ImgBtnDelete")).CommandArgument.ToString());
            //Page.RegisterClientScriptBlock("", "<script>DeleteEquipFunction()</script>"); 
            int i = Convert.ToInt32(hiddenbox.Value);
            if (i == 1)
            {
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
            }
        }

        catch (Exception ex) { throw new Exception(ex.Message); }

    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, UpdateRowDtls = 0;
        try
        {
            if (ChkDetails() == true)
            {
                if (ViewState["EditId"] != null)
                {
                    Entity_Trans.TransId = Convert.ToInt32(ViewState["EditId"]);
                }
                Entity_Trans.TransNo = TxtTranNo.Text;
                Entity_Trans.Date = (!string.IsNullOrEmpty(TxtDate.Text)) ? Convert.ToDateTime(TxtDate.Text) : Convert.ToDateTime("1-Jan-1753");
                //Entity_Trans.TransBy = Convert.ToInt32(ddlBy.SelectedValue);
                Entity_Trans.TransBy = Convert.ToInt32(Session["UserID"]);
                Entity_Trans.Notes = txtNotes.Text;

                Entity_Trans.UserID = Convert.ToInt32(Session["UserId"]);
                Entity_Trans.LoginDate = DateTime.Now;

                UpdateRow = Obj_Trans.UpdateRecord(ref Entity_Trans, out StrError);
                if (UpdateRow > 0)
                {
                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dtInsert = new DataTable();
                        dtInsert = (DataTable)ViewState["CurrentTable"];
                        for (int i = 0; i < GridDetails.Rows.Count; i++)
                        {
                            //Entity_Trans.TransId = InsertRow;
                            Entity_Trans.CategoryId = Convert.ToInt32(dtInsert.Rows[i]["CategoryId"].ToString());
                            Entity_Trans.ItemId = Convert.ToInt32(dtInsert.Rows[i]["ItemId"].ToString());
                            Entity_Trans.TransFrom = Convert.ToInt32(Session["CafeteriaId"]);
                            Entity_Trans.QtyAtSource = Convert.ToDecimal(dtInsert.Rows[i]["AvlQtySou"].ToString());
                            Entity_Trans.TransTo = Convert.ToInt32(dtInsert.Rows[i]["StockLocationID"].ToString());
                            Entity_Trans.QtyAtDest = Convert.ToDecimal(dtInsert.Rows[i]["AvlQty"].ToString());
                            Entity_Trans.TransQty = Convert.ToDecimal(dtInsert.Rows[i]["Qty"].ToString());
                            Entity_Trans.rate = Convert.ToDecimal(dtInsert.Rows[i]["rate"].ToString());
                            Entity_Trans.UnitConversionId = Convert.ToInt32(dtInsert.Rows[i]["UnitId"].ToString());
                            Entity_Trans.ItemDescID = Convert.ToInt32(dtInsert.Rows[i]["ItemDescID"].ToString());
                            UpdateRowDtls = Obj_Trans.InsertDetailsRecord(ref Entity_Trans, out StrError);
                        }
                    }
                    if (UpdateRow > 0)
                    {
                        obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                        MakeControlEmpty();
                        MakeEmptyForm();
                        Entity_Trans = null;
                        Obj_Trans = null;
                    }
                }
            }
            else
            {
                obj_Comm.ShowPopUpMsg("Please Enter Details ..!", this.Page);
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private bool ChkDetails()
    {
        bool flag = false;
        try
        {
            if (GridDetails.Rows.Count > 0 && !String.IsNullOrEmpty(GridDetails.Rows[0].Cells[3].Text) && !GridDetails.Rows[0].Cells[3].Text.Equals("&nbsp;"))
            {
              
                flag = true;
            }
         
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

        return flag;
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDtls = 0;
        try
        {
            if (ChkDetails() == true)
            {
                Entity_Trans.TransNo = TxtTranNo.Text;
                Entity_Trans.Date = (!string.IsNullOrEmpty(TxtDate.Text)) ? Convert.ToDateTime(TxtDate.Text) : Convert.ToDateTime("1-Jan-1753");
                //Entity_Trans.TransBy = Convert.ToInt32(ddlBy.SelectedValue);
                Entity_Trans.TransBy =Convert.ToInt32(Session["UserID"]);
                Entity_Trans.Notes = txtNotes.Text;
                Entity_Trans.UserID = Convert.ToInt32(Session["UserID"]);
                Entity_Trans.LoginDate = DateTime.Now;
                InsertRow = Obj_Trans.InsertRecord(ref Entity_Trans,Convert.ToInt32(Session["CafeteriaId"].ToString()), out StrError);
                if (InsertRow > 0)
                {
                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dtInsert = new DataTable();
                        dtInsert = (DataTable)ViewState["CurrentTable"];
                        for (int i = 0; i < dtInsert.Rows.Count; i++)
                        {
                            Entity_Trans.TransId = InsertRow;
                            Entity_Trans.CategoryId = Convert.ToInt32(dtInsert.Rows[i]["CategoryId"].ToString());
                            Entity_Trans.ItemId = Convert.ToInt32(dtInsert.Rows[i]["ItemId"].ToString());
                            Entity_Trans.TransFrom = Convert.ToInt32(Session["CafeteriaId"]);
                            Entity_Trans.QtyAtSource = Convert.ToDecimal(dtInsert.Rows[i]["AvlQtySou"].ToString());
                            Entity_Trans.TransTo = Convert.ToInt32(dtInsert.Rows[i]["StockLocationID"].ToString());
                            Entity_Trans.QtyAtDest = Convert.ToDecimal(dtInsert.Rows[i]["AvlQty"].ToString());
                            Entity_Trans.TransQty = Convert.ToDecimal(dtInsert.Rows[i]["Qty"].ToString());
                            Entity_Trans.rate = Convert.ToDecimal(dtInsert.Rows[i]["rate"].ToString());
                            Entity_Trans.UnitConversionId = Convert.ToInt32(dtInsert.Rows[i]["UnitId"].ToString());
                            Entity_Trans.ItemDescID = Convert.ToInt32(dtInsert.Rows[i]["ItemDescID"].ToString());
                            InsertRowDtls = Obj_Trans.InsertDetailsRecord(ref Entity_Trans, out StrError);
                        }
                    }
                    if (InsertRow > 0)
                    {
                        obj_Comm.ShowPopUpMsg("Record Saved Successfully", this.Page);
                        MakeControlEmpty();
                        MakeEmptyForm();
                        Entity_Trans = null;
                        Obj_Trans = null;
                    }
                }
            }
            else
            {
                obj_Comm.ShowPopUpMsg("Please Enter Details ..!", this.Page);
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeControlEmpty();
        MakeEmptyForm();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32(ddlCategory.SelectedValue);       
        if (ID > 0)
        {
            DS = Obj_Trans.GetItems(ID, out StrError);

            if (DS.Tables[0].Rows.Count > 0)
            {
                ddlSubCategory.DataSource = DS.Tables[0];
                ddlSubCategory.DataTextField = "SubCategory";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataBind();
            }

            if (DS.Tables[1].Rows.Count > 0)
            {
                ddlItem.DataSource = DS.Tables[1];
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "ItemId";
                ddlItem.DataBind();
            }
        }
        else
        {
            DataSet DS2 = new DataSet();
            DS2 = Obj_Trans.FillCombo(out StrError);
            if (DS2.Tables.Count > 0)
            {
                if (DS2.Tables[3].Rows.Count > 0)
                {
                    ddlItem.DataSource = DS2.Tables[3];
                    ddlItem.DataTextField = "ItemName";
                    ddlItem.DataValueField = "ItemId";
                    ddlItem.DataBind();
                }
            }
        }
    }


    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32(ddlSubCategory.SelectedValue);
        if (ID > 0)
        {
            DS = Obj_Trans.GetSubItems(ID, out StrError);
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
            DataSet DS2 = new DataSet();
            DS2 = Obj_Trans.FillCombo(out StrError);
            if (DS2.Tables.Count > 0)
            {
                if (DS2.Tables[3].Rows.Count > 0)
                {
                    ddlItem.DataSource = DS2.Tables[3];
                    ddlItem.DataTextField = "ItemName";
                    ddlItem.DataValueField = "ItemId";
                    ddlItem.DataBind();
                }
            }
        }
    }


    protected void GrdReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "SelectGrid")
            {
                if ((!(string.IsNullOrEmpty(GrdReport.Rows[0].Cells[3].Text))) && (GrdReport.Rows[0].Cells[3].Text.Equals("&nbsp;")))
                {
                    obj_Comm.ShowPopUpMsg("There Is No Record To Edit", this.Page);
                }
                else
                {
                    if (Convert.ToInt32(e.CommandArgument) != 0)
                    {
                        ViewState["EditId"] = Convert.ToInt32(e.CommandArgument);
             
                        DS = Obj_Trans.GetRecordForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                        if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                        {

                            TxtTranNo.Text = DS.Tables[0].Rows[0]["TransNo"].ToString();
                            TRNO.Visible = true;
                            TxtDate.Text = DS.Tables[0].Rows[0]["Date"].ToString();
                            LblEmployee.Text = Session["UserName"].ToString();
                            //ddlBy.SelectedValue = DS.Tables[0].Rows[0]["TransBy"].ToString();
                            txtNotes.Text = DS.Tables[0].Rows[0]["Notes"].ToString();
                            ViewState["EditFlag"] = true;
                        }
                        else
                        {
                            MakeEmptyForm();
                        }
                        if (DS.Tables[1].Rows.Count > 0)
                        {
                            GridDetails.DataSource = DS.Tables[1];
                            GridDetails.DataBind();
                            ViewState["CurrentTable"] = DS.Tables[1];
                        }

                        DS= null;
                        Obj_Trans = null;
                        BtnUpdate.Visible = true;
                        BtnSave.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void GrdReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int DeleteId = 0;
        try
        {
            if ((!(string.IsNullOrEmpty(GrdReport.Rows[0].Cells[3].Text))) && (GrdReport.Rows[0].Cells[3].Text.Equals("&nbsp;")))
            {
                obj_Comm.ShowPopUpMsg("There Is No Record To Delete", this.Page);
            }
            else
            {
                DeleteId = Convert.ToInt32(((ImageButton)GrdReport.Rows[e.RowIndex].Cells[0].FindControl("ImgBtnDelete")).CommandArgument.ToString());
                if (DeleteId != 0)
                {
                    Entity_Trans.TransId = DeleteId;
                    Entity_Trans.UserID = Convert.ToInt32(Session["UserID"]);
                    Entity_Trans.LoginDate = DateTime.Now;

                    int iDelete = Obj_Trans.DeleteRecord(ref Entity_Trans, out StrError);
                    if (iDelete != 0)
                    {
                        obj_Comm.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                        MakeEmptyForm();
                    }
                }
            }
            Entity_Trans = null;
            Obj_Trans = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void ddlTansTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ItemID = Convert.ToInt32(ddlItem.SelectedValue);
        int locationID = Convert.ToInt32(ddlTansTo.SelectedValue);

        DS = Obj_Trans.GetQuantity(ItemID, locationID, out StrError);
        if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            txtQuantAtDest.Text = DS.Tables[0].Rows[0][0].ToString();
            ViewState["QtyAtDest"] = DS.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            txtQuantAtDest.Text = "0.00";
            ViewState["QtyAtDest"]=0;
        }
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ItemID = Convert.ToInt32(ddlItem.SelectedValue);
        int locationID = Convert.ToInt32(Session["CafeteriaId"]);

        DS = Obj_Trans.GetQuantity(ItemID, locationID, out StrError);
        if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            lblQuantAtSource.Text = DS.Tables[0].Rows[0][0].ToString();
            ViewState["QtyAtSource"] = DS.Tables[0].Rows[0][0].ToString();
          
        }
        else
        {
            lblQuantAtSource.Text = "0.00";
            ViewState["QtyAtSource"] = 0;
        }
        //-------Get Rate Of That Item
        DataSet ds = new DataSet();
        ds = Obj_Trans.GetRate(ItemID, out StrError);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtRate.Text = ds.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            txtRate.Text = "0.00";
        }
        DataSet DS1 = new DataSet();
        DS1 = Obj_Trans.GetUnit(ItemID, Convert.ToDecimal(lblQuantAtSource.Text), out StrError);
        if (DS1.Tables.Count > 0 && DS1.Tables[0].Rows.Count > 0)
        {
            LBLUNIT.Text = DS1.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            LBLUNIT.Text = "--";
        }
        if (DS1.Tables.Count > 0 && DS1.Tables[1].Rows.Count > 0)
        {
            ddlUnitConversion.DataSource = DS1.Tables[1];
            ddlUnitConversion.DataTextField = "UnitFactor";
            ddlUnitConversion.DataValueField = "#";
            ddlUnitConversion.DataBind();
        }
        else
        {
            ddlUnitConversion.DataSource = null;
            ddlUnitConversion.DataBind();
        }

        //-------Get Item Description According To Item
        DataSet ds2 = new DataSet();
        ds2 = Obj_Trans.GetItemDesc(ItemID, out StrError);
        if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        {
            ddldescription.DataSource = ds2.Tables[0];
            ddldescription.DataTextField = "ItemDesc";
            ddldescription.DataValueField = "ItemDetailsId";
            ddldescription.DataBind();
        }
        else
        {
            ddldescription.DataSource = null;
            ddldescription.DataBind();
        }
    }
    protected void ddldescription_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds2 = new DataSet();
        int dscid = Convert.ToInt32(ddldescription.SelectedValue);
        int ItemID = Convert.ToInt32(ddlItem.SelectedValue);
        int locationID = Convert.ToInt32(Session["CafeteriaId"]);
        ds2 = Obj_Trans.GetDescUnit(ItemID, dscid, Convert.ToDecimal(lblQuantAtSource.Text),locationID, out StrError);
        if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        {
            ddlUnitConversion.DataSource = ds2.Tables[0];
            ddlUnitConversion.DataTextField = "Unit";
            ddlUnitConversion.DataValueField = "FromUnitID";
            ddlUnitConversion.DataBind();
        }
        else
        {
            ddlUnitConversion.DataSource = null;
            ddlUnitConversion.DataBind();
        }
        if (ds2.Tables.Count > 0 && ds2.Tables[1].Rows.Count > 0)
        {
            lblQuantAtSource.Text = ds2.Tables[1].Rows[0]["Closing"].ToString();
            ViewState["QtyAtSource"] = ds2.Tables[1].Rows[0]["Closing"].ToString();

        }
        else
        {
            lblQuantAtSource.Text = "0.00";
            ViewState["QtyAtSource"] = 0;
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionItemNameList(string prefixText, int count, string contextKey)
    {
        DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
        String[] SearchList = Obj_RequisitionCafeteria.GetSuggestedRecordItems(prefixText,"");
        return SearchList;
    }

    //protected void TxtItemName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
    //        StrCondition = string.Empty;
    //        StrCondition = TxtItemName.Text.Trim();
    //        DataSet Ds = new DataSet();
    //        Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);
    //        if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
    //        {
    //            TxtItemName.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
    //            TxtItemName.ToolTip = Ds.Tables[0].Rows[0]["ItemId"].ToString();
    //            ddlItem.SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();

    //            ddlItem_SelectedIndexChanged(ddlItem as AjaxControlToolkit.ComboBox, EventArgs.Empty);
    //            Ds = null;
    //        }
    //        else
    //        {
    //            TxtItemName.Text = "";
    //            TxtItemName.ToolTip = "0";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
}
