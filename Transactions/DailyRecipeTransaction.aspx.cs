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

using AjaxControlToolkit;
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;

public partial class Transactions_DailyRecipeTransaction : System.Web.UI.Page
{
    #region[Variable]
        DMDailyRecipeTransaction Obj_Recipe = new DMDailyRecipeTransaction();
        DailyRecipeTransaction Entity_Recipe = new DailyRecipeTransaction();
        CommanFunction obj_Comm = new CommanFunction();
        DataSet DS = new DataSet();
        private static DataTable DSItem = new DataTable();
        private static DataTable DSLocation = new DataTable();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        public static int RecipeId = 0;
        public static bool chkflag = false;
        decimal TotalQty = 0;
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
        public static decimal Amount = 0;
    #endregion

    #region[UserDefineFunction]

        public void SetInitialRow()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("#", typeof(int)));
                dt.Columns.Add(new DataColumn("RecipeId", typeof(Int32)));
                dt.Columns.Add(new DataColumn("RecipeName", typeof(string)));
                dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
                dt.Columns.Add(new DataColumn("AmtPerPlate", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalAmt", typeof(decimal)));

                dr = dt.NewRow();
                dr["#"] = 0;
                dr["RecipeId"] = 0;
                dr["RecipeName"] = string.Empty;
                dr["Quantity"] = string.Empty;
                dr["AmtPerPlate"] = string.Empty;
                dr["TotalAmt"] =0;

                dt.Rows.Add(dr);
                ViewState["CurrentTable"] = dt;
                GridDetails.DataSource = dt;
                GridDetails.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SetInitialRow_ReportGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("#", typeof(int)));
                dt.Columns.Add(new DataColumn("OrderDate", typeof(string)));
                dt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalOrderCost", typeof(string)));
           
                dr = dt.NewRow();
                dr["#"] = 0;
                dr["OrderDate"] = string.Empty;
                dr["OrderNo"] = string.Empty;
                dr["TotalOrderCost"] = string.Empty;
               
                dt.Rows.Add(dr);
                ReportGrid.DataSource = dt;
                ReportGrid.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SetInitialRow_DetailsGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("#", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemId", typeof(Int32)));
                dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
                dt.Columns.Add(new DataColumn("IngredAmt", typeof(decimal)));
                dt.Columns.Add(new DataColumn("SubUnitId", typeof(Int32)));
                dt.Columns.Add(new DataColumn("RecipeId", typeof(Int32)));
                dt.Columns.Add(new DataColumn("ActualRate", typeof(decimal)));
                dt.Columns.Add(new DataColumn("QtyPerUnit", typeof(decimal)));

                dr = dt.NewRow();

                dr["#"] = 0;
                dr["ItemName"] = string.Empty;
                dr["ItemId"] = 0;
                dr["Quantity"] = string.Empty;
                dr["IngredAmt"] = 0.0;
                dr["SubUnitId"] = 0;
                dr["RecipeId"] = 0;
                dr["ActualRate"] = 0;
                dr["QtyPerUnit"] = 0;

                dt.Rows.Add(dr);

                ViewState["ItemDetails"] = dt;
                GridItemDtls.DataSource = dt;
                GridItemDtls.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    
        public void BindCombo()
        {
            try
            {
                DS = new DataSet();
                DS = Obj_Recipe.FillCombo(RecipeId,out StrError);
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        ddlRecipeName.DataSource = DS.Tables[0];
                        ddlRecipeName.DataTextField = "MenuItem";
                        ddlRecipeName.DataValueField = "RecipeId";
                        ddlRecipeName.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void MakeEmptyForm()
        {
            if(!FlagAdd)
              BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            MakeEmptyFields();
            txtAmtPerPlate.Enabled = false;
            TxtSearch.Text = string.Empty;
            SetInitialRow();
            SetInitialRow_ReportGrid();
            SetInitialRow_DetailsGrid();
            BindReportGrid(StrCondition);
        }

        private void MakeControlEmpty()
        {
            ViewState["GridIndex"] = null;
            ViewState["GridDetails"] = null;
           // ViewState["GridIndexI"] = null;
        }

        private void MakeEmptyFields()
        {
            ddlRecipeName.SelectedValue = "0";
            txtTotalQty.Text = string.Empty;
            txtTotAmt.Text = string.Empty;
            txtAmtPerPlate.Text = string.Empty;
        }

        private void DeleteItemDetails(int RecipeId)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["ItemDetails"];
                if (ViewState["ItemDetails"] != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (RecipeId == Convert.ToInt32(dt.Rows[i]["RecipeId"].ToString()))
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                }
                ViewState["ItemDetails"] = dt;
                GridItemDtls.DataSource = dt;
                GridItemDtls.DataBind();
                if (GridItemDtls.Rows.Count > 0)
                {
                }
                else
                {
                    SetInitialRow_DetailsGrid();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

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

        public void BindReportGrid(string StrCondition)
        {
            try
            {
                DataSet DS = new DataSet();
                DS = Obj_Recipe.GetReportGrid(StrCondition, out StrError);
                if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    ReportGrid.DataSource = DS.Tables[0];
                    ReportGrid.DataBind();
                }
                else
                {
                    SetInitialRow_ReportGrid();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                    if (!Session["UserRole"].Equals("Administrator"))
                    //Checking Right of users=======
                    {
                        System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                        System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                        dsChkUserRight1 = (DataSet)Session["DataSet"];

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='DailyRecipeTransaction'");
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
                    }
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

        
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCombo();
            CheckUserRight();
            MakeEmptyForm();
        }

    }
    protected void ddlRecipeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        RecipeId = Convert.ToInt32(ddlRecipeName.SelectedValue.ToString());
        DS = Obj_Recipe.FillCombo(RecipeId,out StrError);
        if (DS.Tables.Count > 0 && DS.Tables[1].Rows.Count > 0)
        {
            txtAmtPerPlate.Text = DS.Tables[1].Rows[0]["AmtPerPlate"].ToString();
        }
        else
        {
            txtAmtPerPlate.Text = string.Empty;
        }
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        try
         {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataTable dtCurrentTableI = (DataTable)ViewState["ItemDetails"];
                DataRow dtTableRow = null;
                DataRow dtTableRow2 = null;
                bool DupFlag = false;
                bool DupFlagItem = false;
                int k =0,l= 0;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    if (dtCurrentTable.Rows.Count == 1 && string.IsNullOrEmpty(dtCurrentTable.Rows[0]["RecipeName"].ToString()))
                    {
                        dtCurrentTable.Rows.RemoveAt(0);
                    }
                    if (ViewState["GridIndex"] != null)
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dtCurrentTable.Rows[i]["RecipeId"]) == Convert.ToInt32(ddlRecipeName.SelectedValue))
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            dtCurrentTable.Rows[k]["RecipeName"] = ddlRecipeName.SelectedItem.Text;
                            dtCurrentTable.Rows[k]["RecipeId"] = Convert.ToInt32(ddlRecipeName.SelectedValue);
                            dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(ddlRecipeName.SelectedValue);
                            dtCurrentTable.Rows[k]["Quantity"] = txtTotalQty.Text;
                            dtCurrentTable.Rows[k]["AmtPerPlate"] = txtAmtPerPlate.Text;
                            dtCurrentTable.Rows[k]["TotalAmt"] = txtTotAmt.Text;

                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            //MakeControlEmpty();
                           // MakeEmptyFields();

                        }
                        else
                        {
                            dtTableRow = dtCurrentTable.NewRow();
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);

                            dtTableRow["RecipeName"] = ddlRecipeName.SelectedItem.Text;
                            dtTableRow["RecipeId"] = Convert.ToInt32(ddlRecipeName.SelectedValue);
                            dtTableRow["#"] = Convert.ToInt32(ddlRecipeName.SelectedValue);
                            dtTableRow["Quantity"] = txtTotalQty.Text;
                            dtTableRow["AmtPerPlate"] = txtAmtPerPlate.Text;
                            dtTableRow["TotalAmt"] = txtTotAmt.Text;

                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            //MakeControlEmpty();
                           // MakeEmptyFields();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dtCurrentTable.Rows[i]["RecipeId"]) == Convert.ToInt32(ddlRecipeName.SelectedValue))
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            dtCurrentTable.Rows[k]["RecipeName"] = ddlRecipeName.SelectedItem.Text;
                            dtCurrentTable.Rows[k]["RecipeId"] = Convert.ToInt32(ddlRecipeName.SelectedValue);
                            dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(ddlRecipeName.SelectedValue);
                            dtCurrentTable.Rows[k]["Quantity"] = txtTotalQty.Text;
                            dtCurrentTable.Rows[k]["AmtPerPlate"] = txtAmtPerPlate.Text;
                            dtCurrentTable.Rows[k]["TotalAmt"] = txtTotAmt.Text;

                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            //MakeControlEmpty();
                           // MakeEmptyFields();
                        }
                        else
                        {
                            dtTableRow = dtCurrentTable.NewRow();
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                            dtTableRow["RecipeName"] = ddlRecipeName.SelectedItem.Text;
                            dtTableRow["RecipeId"] = Convert.ToInt32(ddlRecipeName.SelectedValue);
                            dtTableRow["#"] = Convert.ToInt32(ddlRecipeName.SelectedValue);
                            dtTableRow["Quantity"] = txtTotalQty.Text;
                            dtTableRow["AmtPerPlate"] = txtAmtPerPlate.Text;
                            dtTableRow["TotalAmt"] = txtTotAmt.Text;

                            dtCurrentTable.Rows.Add(dtTableRow);

                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            //MakeControlEmpty();
                            //MakeEmptyFields();
                        }
                    }
                } 

                //Add ItemDetails Of Recipe
                if (dtCurrentTableI.Rows.Count > 0)
                {
                    DS = Obj_Recipe.GetRecipeDetails(RecipeId, out StrError,Convert.ToInt32(txtTotalQty.Text));
                    if (dtCurrentTableI.Rows.Count == 1 && string.IsNullOrEmpty(dtCurrentTableI.Rows[0]["ItemName"].ToString()))
                    {
                        dtCurrentTableI.Rows.RemoveAt(0);
                    }
                    if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCurrentTableI.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dtCurrentTableI.Rows[i]["RecipeId"]) == Convert.ToInt32(ddlRecipeName.SelectedValue))
                            {
                                DupFlagItem = true;
                                l = i;
                            }
                        }
                        if (DupFlagItem == true)
                        {
                            for (int i = 0; i <= DS.Tables[0].Rows.Count - 1; i++)
                            {
                                dtCurrentTableI.Rows[l]["ItemId"] = DS.Tables[0].Rows[i]["ItemId"].ToString();
                                dtCurrentTableI.Rows[l]["ItemName"] = DS.Tables[0].Rows[i]["ItemName"].ToString();
                                dtCurrentTableI.Rows[l]["#"] = DS.Tables[0].Rows[i]["ItemId"].ToString();
                                dtCurrentTableI.Rows[l]["Quantity"] = DS.Tables[0].Rows[i]["Quantity"].ToString();
                                dtCurrentTableI.Rows[l]["IngredAmt"] = DS.Tables[0].Rows[i]["IngredAmt"].ToString();
                                dtCurrentTableI.Rows[l]["SubUnitId"] = DS.Tables[0].Rows[i]["SubUnitId"].ToString();
                                dtCurrentTableI.Rows[l]["RecipeId"] = DS.Tables[0].Rows[i]["RecipeId"].ToString();
                                dtCurrentTableI.Rows[l]["ActualRate"] = DS.Tables[0].Rows[i]["ActualRate"].ToString();
                                dtCurrentTableI.Rows[l]["QtyPerUnit"] = DS.Tables[0].Rows[i]["QtyPerUnit"].ToString();

                                ViewState["ItemDetails"] = dtCurrentTableI;
                                GridItemDtls.DataSource = dtCurrentTableI;
                                GridItemDtls.DataBind();
                                //MakeControlEmpty();
                                MakeEmptyFields();
                            }
                        }
                        else
                        {
                            for (int i = 0; i <= DS.Tables[0].Rows.Count - 1; i++)
                            {
                                dtTableRow2 = dtCurrentTableI.NewRow();
                                int rowindex = Convert.ToInt32(ViewState["GridIndexI"]);

                                dtTableRow2["ItemId"] = DS.Tables[0].Rows[i]["ItemId"].ToString();
                                dtTableRow2["ItemName"] = DS.Tables[0].Rows[i]["ItemName"].ToString();
                                dtTableRow2["#"] = DS.Tables[0].Rows[i]["ItemId"].ToString();
                                dtTableRow2["Quantity"] = DS.Tables[0].Rows[i]["Quantity"].ToString();
                                dtTableRow2["IngredAmt"] = DS.Tables[0].Rows[i]["IngredAmt"].ToString();
                                dtTableRow2["SubUnitId"] = DS.Tables[0].Rows[i]["SubUnitId"].ToString();
                                dtTableRow2["RecipeId"] = DS.Tables[0].Rows[i]["RecipeId"].ToString();
                                dtTableRow2["ActualRate"] = DS.Tables[0].Rows[i]["ActualRate"].ToString();
                                dtTableRow2["QtyPerUnit"] = DS.Tables[0].Rows[i]["QtyPerUnit"].ToString();

                                dtCurrentTableI.Rows.Add(dtTableRow2);

                                ViewState["ItemDetails"] = dtCurrentTableI;
                                GridItemDtls.DataSource = dtCurrentTableI;
                                GridItemDtls.DataBind();
                                //MakeControlEmpty();
                                MakeEmptyFields();
                            }
                        }
                    }
                }
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
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
                DeleteItemDetails(RecipeId);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Index;
            if (e.CommandName == "Delete")
            {
                if ((!(string.IsNullOrEmpty(GridDetails.Rows[0].Cells[2].Text))) && (GridDetails.Rows[0].Cells[2].Text.Equals("&nbsp;")))
                {
                    obj_Comm.ShowPopUpMsg("There Is No Record To Edit", this.Page);
                }
                else
                {
                    Index = Convert.ToInt32(e.CommandArgument);
                    ViewState["GridIndex"] = Index;
                    RecipeId = Index;
                    DeleteItemDetails(RecipeId);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int InsertRow = 0, InsertRowDtls=0 ,InserStockDtls= 0;
             if (ChkDetails() == true)
            {
                    //Entity_Recipe.OrderDate = Convert.ToDateTime(txtDate.ToString());
                    Entity_Recipe.OrderDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MMM/yyyy"));
                    Entity_Recipe.TotalOrderCost = Convert.ToDecimal(txtTotalOrderCost.Text);

                    Entity_Recipe.UserId = Convert.ToInt32(Session["UserId"]);
                    Entity_Recipe.LoginDate = DateTime.Now;

                    InsertRow = Obj_Recipe.InsertRecord(ref Entity_Recipe, out StrError);

                    if (InsertRow > 0)
                    {
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtInsert = new DataTable();
                            DataTable dtInsertItem = new DataTable();
                            dtInsert = (DataTable)ViewState["CurrentTable"];
                            dtInsertItem = (DataTable)ViewState["ItemDetails"];
                            for (int i = 0; i < dtInsert.Rows.Count; i++)
                            {
                                Entity_Recipe.OrderId = InsertRow;
                                Entity_Recipe.RecipeId = Convert.ToInt32(dtInsert.Rows[i]["RecipeId"].ToString());
                                Entity_Recipe.Qty = Convert.ToDecimal(dtInsert.Rows[i]["Quantity"].ToString());
                                Entity_Recipe.TotalAmt = Convert.ToDecimal(dtInsert.Rows[i]["TotalAmt"].ToString());

                                //---**StockDetails**---
                                InsertRowDtls = Obj_Recipe.InsertDetailsRecord(ref Entity_Recipe, out StrError);

                                    for (int j = 0; j < dtInsertItem.Rows.Count; j++)
                                    {

                                        if (Convert.ToInt32(dtInsert.Rows[i]["RecipeId"].ToString()) == Convert.ToInt32(dtInsertItem.Rows[j]["RecipeId"].ToString()))
                                        {

                                            Entity_Recipe.OrderId = InsertRow;
                                            Entity_Recipe.RecipeId = Convert.ToInt32(dtInsertItem.Rows[j]["RecipeId"].ToString());
                                            Entity_Recipe.ItemId = Convert.ToInt32(GridItemDtls.Rows[j].Cells[1].Text);
                                            //Entity_Recipe.UnitId = DateTime.Now;
                                            Entity_Recipe.StockDate = DateTime.Now;
                                            Entity_Recipe.StockLocationID = Convert.ToInt32(Session["CafeteriaId"]);
                                            Entity_Recipe.QtyPerUnit = Convert.ToDecimal(GridItemDtls.Rows[j].Cells[8].Text);
                                            Entity_Recipe.ActualRate = Convert.ToDecimal(GridItemDtls.Rows[j].Cells[4].Text);

                                            InserStockDtls = Obj_Recipe.InsertStockDetails(ref Entity_Recipe, out StrError);
                                        }
                                    }
                                
                            }
                        }
                        if (InsertRow > 0 && InsertRowDtls > 0)
                        {
                            obj_Comm.ShowPopUpMsg("Record Saved Successfully", this.Page);
                            MakeControlEmpty();
                            MakeEmptyForm();
                            Entity_Recipe = null;
                            Obj_Recipe = null;
                        }
                    }
                
            }
            else
            {
                obj_Comm.ShowPopUpMsg("Please Enter Details ..!", this.Page);
            }
        }
      
        catch(Exception ex)
        { 
            throw new Exception(ex.Message);
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeControlEmpty();
        MakeEmptyForm();
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int UpdateRow = 0, UpdateRowDtls = 0;
          
            if (ViewState["EditID"] != null)
                {
                    Entity_Recipe.OrderId = Convert.ToInt32(ViewState["EditID"]);
                }
                //Entity_Recipe.OrderDate = Convert.ToDateTime(txtDate.ToString());
                Entity_Recipe.OrderDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MMM/yyyy"));
                Entity_Recipe.TotalOrderCost = Convert.ToDecimal(txtTotalOrderCost.Text);

                Entity_Recipe.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_Recipe.LoginDate = DateTime.Now;

                UpdateRow = Obj_Recipe.UpdateRecord(ref Entity_Recipe, out StrError);

                if (UpdateRow > 0)
                {
                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dtInsert = new DataTable();
                        dtInsert = (DataTable)ViewState["CurrentTable"];
                        for (int i = 0; i < dtInsert.Rows.Count; i++)
                        {
                            Entity_Recipe.RecipeId = Convert.ToInt32(dtInsert.Rows[i]["RecipeId"].ToString());
                            Entity_Recipe.Qty = Convert.ToDecimal(dtInsert.Rows[i]["Quantity"].ToString());
                            Entity_Recipe.TotalAmt = Convert.ToDecimal(dtInsert.Rows[i]["TotalAmt"].ToString());

                            UpdateRowDtls = Obj_Recipe.InsertDetailsRecord(ref Entity_Recipe, out StrError);
                        }
                    }
                    if (UpdateRow > 0 && UpdateRowDtls > 0)
                    {
                        obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                        MakeControlEmpty();
                        MakeEmptyForm();
                        Entity_Recipe = null;
                        Obj_Recipe = null;
                    }
                }
            else
            {
                obj_Comm.ShowPopUpMsg("Please Enter Details ..!", this.Page);
            }
        }
        catch(Exception ex)
        { 
            throw new Exception(ex.Message);
        }

    }
    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmt"));
            txtTotalOrderCost.Text = TotalQty.ToString();
        }
    }
    protected void ReportGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                        DS = Obj_Recipe.GetRecordForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                        if (DS.Tables.Count > 0)
                        {
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                txtDate.Text = DS.Tables[0].Rows[0]["OrderDate"].ToString();
                                txtTotalOrderCost.Text = DS.Tables[0].Rows[0]["TotalOrderCost"].ToString();
                            }
                            if (DS.Tables[1].Rows.Count > 0)
                            {
                                GridDetails.DataSource = DS.Tables[1];
                                ViewState["CurrentTable"] = DS.Tables[1];
                                GridDetails.DataBind();
                            }
                            if (DS.Tables[2].Rows.Count > 0)
                            {
                                GridItemDtls.DataSource = DS.Tables[2];
                                ViewState["ItemDetails"] = DS.Tables[2];
                                GridItemDtls.DataBind();
                            }
                            else
                            {
                                SetInitialRow();
                            }
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                        }
                    }
                    break;

                case ("Delete"):
                    {
                        ViewState["DeleteID"] = Convert.ToInt32(e.CommandArgument);
                        Entity_Recipe.OrderId = Convert.ToInt32(e.CommandArgument);
                        Entity_Recipe.UserId = Convert.ToInt32(Session["UserId"]);
                        Entity_Recipe.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        int DeletedRow = Obj_Recipe.DeleteRecord(ref Entity_Recipe, out StrError);
                        if (DeletedRow != 0)
                        {
                            obj_Comm.ShowPopUpMsg("Record Deleted Successfully!", this.Page);
                            MakeEmptyForm();
                        }
                    }
                    break;
            }
        }
        catch( Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        BindReportGrid(StrCondition);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMDailyRecipeTransaction Obj_Recipe = new DMDailyRecipeTransaction();
        String[] SearchList = Obj_Recipe.GetSuggestedRecord(prefixText);
        return SearchList;
    }
}
