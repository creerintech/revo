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

public partial class Transactions_RecipesTemplate : System.Web.UI.Page
{
    #region[Variables]
        DMRecipeTemplate Obj_Recipe= new DMRecipeTemplate();
        RecipeTemplate Entity_Recipe= new RecipeTemplate();
        CommanFunction obj_Comm = new CommanFunction();
        DataSet DS = new DataSet();
        private static DataTable DSItem = new DataTable();
        private static DataTable DSLocation = new DataTable();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        public static int ItemId = 0;
        public static bool chkflag = false;
        decimal TotalQty = 0;
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
        public static decimal Amount = 0,AmtPerUnit = 0;
    #endregion

    #region[UserDefine Functions]

        private void MakeEmptyForm()
        {
            if(!FlagAdd)
              BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            txtMenuName.Text = string.Empty;
            txtAmtPerPlate.Text = string.Empty;
            txtAmtPerPlate.Enabled = false;
            TxtSearch.Text = string.Empty;
            SetInitialRow();
            SetInitialRow_ReportGrid();
            BindReportGrid(StrCondition);
           
        }

        private void MakeControlEmpty()
        {
        ddlItemName.SelectedValue = "0";
        TxtQuantity.Text = "0.0";
        ddlUnit.SelectedValue = "0";
        LblAmount.Text = string.Empty;

        ViewState["GridIndex"] = null;
        ViewState["GridDetails"] = null;
        ImgAddGrid.ImageUrl = "~/Images/Icon/Gridadd.png";
        ImgAddGrid.ToolTip = "Add Grid";
        ddlItemName.Focus();
        }

        public void SetInitialRow()
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
                dt.Columns.Add(new DataColumn("ActualRate", typeof(decimal)));
                dt.Columns.Add(new DataColumn("QtyPerUnit", typeof(decimal)));
                

                dr = dt.NewRow();
                dr["#"] = 0;
                dr["ItemName"] = string.Empty;
                dr["ItemId"] = 0;
                dr["Quantity"] = string.Empty;
                dr["IngredAmt"] = 0.0;
                dr["SubUnitId"] = 0;
                dr["ActualRate"] = 0;
                dr["QtyPerUnit"] = 0;

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
            dt.Columns.Add(new DataColumn("MenuItem", typeof(string)));
            dt.Columns.Add(new DataColumn("AmtPerPlate", typeof(string)));

            dr = dt.NewRow();
            dr["#"] = 0;
            dr["MenuItem"] = string.Empty;
            dr["AmtPerPlate"] = string.Empty;
          
            dt.Rows.Add(dr);
            ReportGrid.DataSource = dt;
            ReportGrid.DataBind();
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
                DS = Obj_Recipe.FillCombo(out StrError);
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
           if (GridDetails.Rows.Count > 0 && !String.IsNullOrEmpty(GridDetails.Rows[0].Cells[2].Text) && !GridDetails.Rows[0].Cells[2].Text.Equals("&nbsp;"))
            {
                flag = true;
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

        return flag;
        }

        private void CalculateAmt()
        {
            int SubUnitId =Convert.ToInt32(ddlUnit.SelectedValue.ToString());
            DataSet dsTemp = new DataSet();
            dsTemp = Obj_Recipe.GetConversnFactr(SubUnitId, out StrError);
            decimal conversnFactr = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
            Amount = ((Convert.ToDecimal((LblAmount.Text)) * Convert.ToDecimal((TxtQuantity.Text))) / conversnFactr);
        }

        private void CalAmtPerUnit()
        {
            int SubUnitId = Convert.ToInt32(ddlUnit.SelectedValue.ToString());
            DataSet dsTemp = new DataSet();
            dsTemp = Obj_Recipe.GetConversnFactr(SubUnitId, out StrError);
            decimal conversnFactr = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
            AmtPerUnit = Convert.ToDecimal((TxtQuantity.Text)) / conversnFactr;
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

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='RecipesTemplate'");
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
            MakeControlEmpty();
            MakeEmptyForm();
        }
    }
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ItemId = Convert.ToInt32(ddlItemName.SelectedValue.ToString());
        DS = Obj_Recipe.GetAvgPurchaseRate(ItemId, out StrError);
        if (DS.Tables.Count > 0 )
        {
            if( DS.Tables[0].Rows.Count > 0)
            {
              LblAmount.Text = DS.Tables[0].Rows[0][0].ToString();
            }
            if (DS.Tables[1].Rows.Count > 0)
            {
                ddlUnit.DataSource = DS.Tables[1];
                ddlUnit.DataTextField = "SubUnit";
                ddlUnit.DataValueField = "SubUnitId";
                ddlUnit.DataBind();
            }
        }
        else
        {
            LblAmount.Text = string.Empty;
        }
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
                  
                        if (dtCurrentTable.Rows.Count == 1 && string.IsNullOrEmpty(dtCurrentTable.Rows[0]["ItemName"].ToString()))
                        {
                            dtCurrentTable.Rows.RemoveAt(0);
                        }
                        if (ViewState["GridIndex"] != null)
                        {
                            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dtCurrentTable.Rows[i]["ItemId"]) == Convert.ToInt32(ddlItemName.SelectedValue))
                                {
                                    DupFlag = true;
                                    k = i;
                                }
                            }
                            if (DupFlag == true)
                            {
                                dtCurrentTable.Rows[k]["ItemName"] = ddlItemName.SelectedItem.Text;
                                dtCurrentTable.Rows[k]["ItemId"] = Convert.ToInt32(ddlItemName.SelectedValue);
                                dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(ddlItemName.SelectedValue);
                                dtCurrentTable.Rows[k]["Quantity"] = TxtQuantity.Text+'-'+ddlUnit.SelectedItem.ToString();
                                dtCurrentTable.Rows[k]["SubUnitId"] = Convert.ToInt32(ddlUnit.SelectedValue);

                                CalculateAmt();
                                dtCurrentTable.Rows[k]["IngredAmt"] = Amount.ToString();
                                dtCurrentTable.Rows[k]["ActualRate"] = Convert.ToDecimal(LblAmount.Text);
                                CalAmtPerUnit();
                                dtCurrentTable.Rows[k]["QtyPerUnit"] = Convert.ToDecimal(AmtPerUnit.ToString());

                                ViewState["CurrentTable"] = dtCurrentTable;
                                GridDetails.DataSource = dtCurrentTable;
                                GridDetails.DataBind();
                                MakeControlEmpty();

                            }
                            else
                            {

                                dtTableRow = dtCurrentTable.NewRow();
                                int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                              
                                dtTableRow["ItemId"] = Convert.ToInt32(ddlItemName.SelectedValue);
                                dtTableRow["ItemName"] = ddlItemName.SelectedItem.Text;
                                dtTableRow["#"] = Convert.ToInt32(ddlItemName.SelectedValue);
                                dtTableRow["Quantity"] = TxtQuantity.Text + '-' + ddlUnit.SelectedItem.ToString();
                                dtTableRow["SubUnitId"] = Convert.ToInt32(ddlUnit.SelectedValue);

                                CalculateAmt();
                                dtTableRow["IngredAmt"] = Amount.ToString();
                                dtTableRow["ActualRate"] = Convert.ToDecimal(LblAmount.Text);
                                CalAmtPerUnit();
                                dtTableRow["QtyPerUnit"] = Convert.ToDecimal(AmtPerUnit.ToString());

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
                                if (Convert.ToInt32(dtCurrentTable.Rows[i]["ItemId"]) == Convert.ToInt32(ddlItemName.SelectedValue))
                                {
                                    DupFlag = true;
                                    k = i;
                                }
                            }
                            if (DupFlag == true)
                            {
                                dtCurrentTable.Rows[k]["ItemName"] = ddlItemName.SelectedItem.Text;
                                dtCurrentTable.Rows[k]["ItemId"] = Convert.ToInt32(ddlItemName.SelectedValue);
                                dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(ddlItemName.SelectedValue);
                                dtCurrentTable.Rows[k]["Quantity"] = TxtQuantity.Text + '-' + ddlUnit.SelectedItem.ToString();
                                dtCurrentTable.Rows[k]["SubUnitId"] = Convert.ToInt32(ddlUnit.SelectedValue);

                                CalculateAmt();
                                dtCurrentTable.Rows[k]["IngredAmt"] = Amount.ToString();
                                dtCurrentTable.Rows[k]["ActualRate"] = Convert.ToDecimal(LblAmount.Text);
                                CalAmtPerUnit();
                                dtCurrentTable.Rows[k]["QtyPerUnit"] = Convert.ToDecimal(AmtPerUnit.ToString());
                                //dtCurrentTable.Rows[k]["QtyPerUnit"] = Amount.ToString();


                                ViewState["CurrentTable"] = dtCurrentTable;
                                GridDetails.DataSource = dtCurrentTable;
                                GridDetails.DataBind();
                                MakeControlEmpty();
                            }
                            else
                            {
                                dtTableRow = dtCurrentTable.NewRow();
                                int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                                dtTableRow["ItemId"] = Convert.ToInt32(ddlItemName.SelectedValue);
                                dtTableRow["ItemName"] = ddlItemName.SelectedItem.Text;
                                
                                dtTableRow["#"] = Convert.ToInt32(ddlItemName.SelectedValue);
                                dtTableRow["Quantity"] = TxtQuantity.Text + '-' + ddlUnit.SelectedItem.ToString();
                                dtTableRow["SubUnitId"] = Convert.ToInt32(ddlUnit.SelectedValue);

                                CalculateAmt();
                                dtTableRow["IngredAmt"] = Amount.ToString();
                                dtTableRow["ActualRate"] = Convert.ToDecimal(LblAmount.Text);
                                CalAmtPerUnit();
                                dtTableRow["QtyPerUnit"] = Convert.ToDecimal(AmtPerUnit.ToString());

                                dtCurrentTable.Rows.Add(dtTableRow);

                                ViewState["CurrentTable"] = dtCurrentTable;
                                GridDetails.DataSource = dtCurrentTable;
                                GridDetails.DataBind();
                                MakeControlEmpty();
                            }
                        }
                }
            }
        }
        catch(Exception ex)  { throw new Exception(ex.Message); }

    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeControlEmpty();
        MakeEmptyForm();
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDtls = 0;
        try
        {
            if (ChkDetails() == true)
            {
                DS = Obj_Recipe.ChkDuplicate(txtMenuName.Text.Trim(), out StrError);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    obj_Comm.ShowPopUpMsg("Record is Already Present..", this.Page);
                    ddlItemName.Focus();
                }
                else
                {
                    Entity_Recipe.MenuItem = txtMenuName.Text.Trim();
                    Entity_Recipe.AmtPerPlate = Convert.ToDecimal(txtAmtPerPlate.Text.Trim());
                   
                    Entity_Recipe.UserId = Convert.ToInt32(Session["UserId"]);
                    Entity_Recipe.LoginDate = DateTime.Now;

                    InsertRow = Obj_Recipe.InsertRecord(ref Entity_Recipe, out StrError);

                    if (InsertRow > 0)
                    {
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtInsert = new DataTable();
                            dtInsert = (DataTable)ViewState["CurrentTable"];
                            for (int i = 0; i < dtInsert.Rows.Count; i++)
                            {
                                Entity_Recipe.RecipeId = InsertRow;
                                Entity_Recipe.ItemId = Convert.ToInt32(dtInsert.Rows[i]["ItemId"].ToString());
                                //---Split Quantity for qty and UnitId---
                                string str = dtInsert.Rows[i]["Quantity"].ToString();
                                String[] qtyUnit = str.Split('-');

                                Entity_Recipe.Qty = Convert.ToDecimal(qtyUnit[0]);
                                Entity_Recipe.SubUnitId = Convert.ToInt32(dtInsert.Rows[i]["SubUnitId"].ToString());
                                Entity_Recipe.IngredAmt = Convert.ToDecimal(dtInsert.Rows[i]["IngredAmt"].ToString());
                                Entity_Recipe.ActualRate = Convert.ToDecimal(dtInsert.Rows[i]["ActualRate"].ToString());
                                Entity_Recipe.QtyPerUnit = Convert.ToDecimal(dtInsert.Rows[i]["QtyPerUnit"].ToString());

                                InsertRowDtls = Obj_Recipe.InsertDetailsRecord(ref Entity_Recipe, out StrError);
                            }
                        }
                        if (InsertRow > 0)
                        {
                            obj_Comm.ShowPopUpMsg("Record Saved Successfully", this.Page);
                            MakeControlEmpty();
                            MakeEmptyForm();
                            Entity_Recipe = null;
                            Obj_Recipe = null;
                        }
                    }
                }
            }
            else
            {
                obj_Comm.ShowPopUpMsg("Please Enter Details ..!", this.Page);
            }


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "IngredAmt"));
            TxtTotalAmt.Text = TotalQty.ToString();
            txtAmtPerPlate.Text = TotalQty.ToString();
        }
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, UpdateRowDtls = 0;
        try
            {
                    if (ViewState["EditID"] != null)
                    {
                        Entity_Recipe.RecipeId = Convert.ToInt32(ViewState["EditID"]);
                    }
                    Entity_Recipe.MenuItem = txtMenuName.Text.Trim();
                    Entity_Recipe.AmtPerPlate = Convert.ToDecimal(txtAmtPerPlate.Text.Trim());

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
                                Entity_Recipe.ItemId = Convert.ToInt32(dtInsert.Rows[i]["ItemId"].ToString());

                                //---Split Quantity for qty and UnitId---
                                string str = dtInsert.Rows[i]["Quantity"].ToString();
                                String[] qtyUnit = str.Split('-');

                                Entity_Recipe.Qty = Convert.ToDecimal(qtyUnit[0]);
                                Entity_Recipe.SubUnitId = Convert.ToInt32(dtInsert.Rows[i]["SubUnitId"].ToString());
                                Entity_Recipe.IngredAmt = Convert.ToDecimal(dtInsert.Rows[i]["IngredAmt"].ToString());
                                Entity_Recipe.ActualRate = Convert.ToDecimal(dtInsert.Rows[i]["ActualRate"].ToString());
                                Entity_Recipe.QtyPerUnit = Convert.ToDecimal(dtInsert.Rows[i]["QtyPerUnit"].ToString());

                                UpdateRowDtls = Obj_Recipe.InsertDetailsRecord(ref Entity_Recipe, out StrError);
                            }
                        }
                        if (UpdateRow > 0)
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
        catch (Exception ex)
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
            if (e.CommandName == "SelectGrid")
            {
                if ((!(string.IsNullOrEmpty(GridDetails.Rows[0].Cells[2].Text))) && (GridDetails.Rows[0].Cells[2].Text.Equals("&nbsp;")))
                {
                    obj_Comm.ShowPopUpMsg("There Is No Record To Edit", this.Page);
                }
                else
                {
                    ImgAddGrid.ImageUrl = "~/Images/Icon/GridUpdate.png";
                    ImgAddGrid.ToolTip = "Update";

                    Index = Convert.ToInt32(e.CommandArgument);

                    ViewState["GridIndex"] = Index;
                    ddlItemName.SelectedValue = GridDetails.Rows[Index].Cells[3].Text;
                    ddlItemName_SelectedIndexChanged(sender, e);
                    string str = GridDetails.Rows[Index].Cells[4].Text;
                    String[] qtyunit = str.Split('-');
                    TxtQuantity.Text = qtyunit[0].ToString();
                    ddlUnit.SelectedValue = GridDetails.Rows[Index].Cells[6].Text;
                }
            }
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
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMRecipeTemplate Obj_Recipe= new DMRecipeTemplate();
        String[] SearchList = Obj_Recipe.GetSuggestedRecord(prefixText);
        return SearchList;
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
                                txtMenuName.Text = DS.Tables[0].Rows[0]["MenuItem"].ToString();
                                txtAmtPerPlate.Text = DS.Tables[0].Rows[0]["AmtPerPlate"].ToString();
                                //TxtTotalAmt.Text = DS.Tables[0].Rows[0]["AmtPerPlate"].ToString();
                            }
                            if (DS.Tables[1].Rows.Count > 0)
                            {
                                GridDetails.DataSource = DS.Tables[1];
                                ViewState["CurrentTable"] = DS.Tables[1];
                                GridDetails.DataBind();
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
                        Entity_Recipe.RecipeId = Convert.ToInt32(e.CommandArgument);
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
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void ReportGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
