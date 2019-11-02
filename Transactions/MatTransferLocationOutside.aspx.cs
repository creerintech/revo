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

public partial class TempFiles_MatTransferLocationOutside : System.Web.UI.Page
{
    #region [Private Variables]
        DMTransferLocationOutside Obj_Trans = new DMTransferLocationOutside();
        TransferLocationOutside Entity_Trans = new TransferLocationOutside();
        CommanFunction obj_Comm = new CommanFunction();
        DataSet DS = new DataSet();
        string TransferNum = string.Empty;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        int CategoryID;
        string CategoryName;
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
                    if (!Session["UserRole"].Equals("Administrator"))
                    //Checking Right of users=======
                    {
                        System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                        System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                        dsChkUserRight1 = (DataSet)Session["DataSet"];

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='TransferLocationOutSide'");
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
                                    GRow.FindControl("ImageGridEdit").Visible = false;
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

    private void SetInitialRowReport()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("#", typeof(int));
        dt.Columns.Add("TransId", typeof(int));
        dt.Columns.Add("TransNo", typeof(string));
        dt.Columns.Add("Date", typeof(string));
        dt.Columns.Add("TransBy", typeof(string));
        dt.Columns.Add("Amount", typeof(string));
        dr = dt.NewRow();
        dr["#"] = 0;
        dr["TransId"]=0;
        dr["TransNo"] = "";
        dr["Date"] = "";
        dr["TransBy"] = "";
        dr["Amount"] = "";
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
        dt.Columns.Add("Item", typeof(string));
        dt.Columns.Add("Unit", typeof(string));
        dt.Columns.Add("CategoryName", typeof(string));
        dt.Columns.Add("TransFrom", typeof(string));//AvlQtySou
        dt.Columns.Add("AvlQtySou", typeof(decimal));
        dt.Columns.Add("StockLocationID", typeof(int));
        dt.Columns.Add("TransTo", typeof(string));
        dt.Columns.Add("AvlQty", typeof(decimal));
        dt.Columns.Add("Qty", typeof(decimal));
        dt.Columns.Add("rate", typeof(decimal));
        //dt.Columns.Add("Notes", typeof(string));
        //dt.Columns.Add("ItemId", typeof(int));
        //dt.Columns.Add("TransFromID", typeof(int));
        //dt.Columns.Add("TransToID", typeof(int));
        //dt.Columns.Add("AvlQtyDest", typeof(decimal));
        dr = dt.NewRow();
        dr["#"] = 0;
        dr["CategoryId"] = 0;
        dr["CategoryName"] = "";
        dr["ItemId"] = 0;
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
        //dr["TransToID"] = 0;
        //dr["AvlQtyDest"] = 0.0;
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        GridDetails.DataSource = dt;
        GridDetails.DataBind();
    }

    private void SetInitialRowIN()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("#", typeof(int));
        dt.Columns.Add("CategoryId", typeof(int));
        dt.Columns.Add("ItemId", typeof(int));
        dt.Columns.Add("Item", typeof(string));
        dt.Columns.Add("Unit", typeof(string));
        dt.Columns.Add("CategoryName", typeof(string));
        dt.Columns.Add("TransFrom", typeof(string));//AvlQtySou
        dt.Columns.Add("AvlQtySou", typeof(decimal));
        dt.Columns.Add("StockLocationID", typeof(int));
        dt.Columns.Add("TransTo", typeof(string));
        dt.Columns.Add("AvlQty", typeof(decimal));
        dt.Columns.Add("Qty", typeof(decimal));
        dt.Columns.Add("rate", typeof(decimal));
        //dt.Columns.Add("Notes", typeof(string));
        //dt.Columns.Add("ItemId", typeof(int));
        //dt.Columns.Add("TransFromID", typeof(int));
        //dt.Columns.Add("TransToID", typeof(int));
        //dt.Columns.Add("AvlQtyDest", typeof(decimal));
        dr = dt.NewRow();
        dr["#"] = 0;
        dr["CategoryId"] = 0;
        dr["CategoryName"] = "";
        dr["ItemId"] = 0;
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
        //dr["TransToID"] = 0;
        //dr["AvlQtyDest"] = 0.0;
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        GRDTRANSFERIN.DataSource = dt;
        GRDTRANSFERIN.DataBind();
    }

    private void MakeEmptyForm()
    {
        ViewState["EditId"] = null;
        ViewState["GridIndex"] = null;

        ddlBy.SelectedValue = "0";
        TxtTranNo.Text = string.Empty;
        TRNO.Visible = false;
        TxtTranNo.Enabled = false;
        TxtDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
        txtNotes.Text = string.Empty;
        TxtSearch.Text = string.Empty;
        lblunitout.Text= LBLUNIT.Text = "--";

        if(!FlagAdd)
          BtnSave.Visible = true;
        BtnUpdate.Visible = false;

        BtnCancel.Visible = true;
        TransNo();
        SetInitialRow();
        SetInitialRowIN();
        RdoType.Enabled = true;
        RdoType.SelectedIndex = 0;
        TRTRANSFERIN.Visible = TRTRANSFERINDETAILS.Visible = true;
        TRTRANSFEROUT.Visible = TRTRANSFEROUTDETAILS.Visible = false;
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
            DS = Obj_Trans.GetTransferList(RepCondition, out StrError);
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
        if (RdoType.SelectedValue.Equals("TO"))
        {
            ddlCategory.SelectedValue = "0";
            ddlItem.SelectedValue = "0";
            //txtQuantAtDest.Text = lblQuantAtSource.Text = "0.00";
            txtrate.Text = "0.00";
            ddlTansTo.SelectedValue = "0";
            txtTansQty.Text = string.Empty;
            txtrate.Text = string.Empty;

            ViewState["GridIndex"] = null;
            ViewState["GridDetails"] = null;
            ImgAddGrid.ImageUrl = "~/Images/Icon/Gridadd.png";
            ImgAddGrid.ToolTip = "Add Grid";
        }
        else
        {
            DDLITEMSIN.SelectedValue =DDLCATEGORYIN.SelectedValue = "0";
            
            DDLTRANSFROMIN.SelectedValue = "0";
            LBLUNIT.Text=TXTRATEIN.Text = TXTTRANSFERQTYIN.Text = string.Empty;
            ViewState["GridIndex"] = null;
            ViewState["GridDetails"] = null;
            IMGBTNADDIN.ImageUrl = "~/Images/Icon/Gridadd.png";
            IMGBTNADDIN.ToolTip = "Add Grid";
        }

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

                DDLTRANSFROMIN.DataSource = DS.Tables[1];
                DDLTRANSFROMIN.DataTextField = "Location";
                DDLTRANSFROMIN.DataValueField = "StockLocationID";
                DDLTRANSFROMIN.DataBind();
            }
            if (DS.Tables[2].Rows.Count > 0)
            {
                ddlCategory.DataSource = DS.Tables[2];
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();

                DDLCATEGORYIN.DataSource = DS.Tables[2];
                DDLCATEGORYIN.DataTextField = "CategoryName";
                DDLCATEGORYIN.DataValueField = "CategoryId";
                DDLCATEGORYIN.DataBind();
            }
            if (DS.Tables[3].Rows.Count > 0)
            {
                ddlItem.DataSource = DS.Tables[3];
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "ItemId";
                ddlItem.DataBind();

                DDLITEMSIN.DataSource = DS.Tables[3];
                DDLITEMSIN.DataTextField = "ItemName";
                DDLITEMSIN.DataValueField = "ItemId";
                DDLITEMSIN.DataBind();
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
            LBLTRANSINTO.Text = lblLocation.Text = Session["Location"].ToString();
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
        DMTransferLocationOutside Obj_Trans = new DMTransferLocationOutside();
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
                    decimal Qty2 = Convert.ToDecimal(lblQuantAtSource.Text);
                    int locationID = Convert.ToInt32(Session["CafeteriaId"]);
                    int TransferTo=Convert.ToInt32(ddlTansTo.SelectedValue.ToString());
                    if (locationID != TransferTo)
                    {
                        if (Qty1 <= Qty2)
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
                                        && (Convert.ToInt32(dtCurrntTable.Rows[i]["StockLocationID"]) == Convert.ToInt32(ddlTansTo.SelectedValue)))
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
                                    dtCurrntTable.Rows[k]["TransFrom"] = lblLocation.Text;
                                    dtCurrntTable.Rows[k]["AvlQtySou"] = lblQuantAtSource.Text;
                                    dtCurrntTable.Rows[k]["StockLocationID"] = ddlTansTo.SelectedValue;
                                    dtCurrntTable.Rows[k]["TransTo"] = ddlTansTo.SelectedItem;
                                    //dtCurrntTable.Rows[k]["AvlQty"] = txtQuantAtDest.Text;
                                    dtCurrntTable.Rows[k]["AvlQty"] = 0;
                                    dtCurrntTable.Rows[k]["Qty"] = txtTansQty.Text;
                                    dtCurrntTable.Rows[k]["rate"] = txtrate.Text;
                                    dtCurrntTable.Rows[k]["Unit"] = lblunitout.Text;

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
                                    dtCurrntTable.Rows[rowindex]["TransFrom"] = lblLocation.Text;
                                    dtCurrntTable.Rows[rowindex]["AvlQtySou"] = lblQuantAtSource.Text;
                                    dtCurrntTable.Rows[rowindex]["StockLocationID"] = ddlTansTo.SelectedValue;
                                    dtCurrntTable.Rows[rowindex]["TransTo"] = ddlTansTo.SelectedItem;
                                    //dtCurrntTable.Rows[rowindex]["AvlQty"] = txtQuantAtDest.Text;
                                    dtCurrntTable.Rows[rowindex]["AvlQty"] = 0;
                                    dtCurrntTable.Rows[rowindex]["Qty"] = txtTansQty.Text;
                                    dtCurrntTable.Rows[rowindex]["rate"] = txtrate.Text;
                                    dtCurrntTable.Rows[rowindex]["Unit"] = lblunitout.Text;


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
                                        && (Convert.ToInt32(dtCurrntTable.Rows[i]["StockLocationID"]) == Convert.ToInt32(ddlTansTo.SelectedValue)))
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
                                    dtCurrntTable.Rows[k]["TransFrom"] = lblLocation.Text;
                                    dtCurrntTable.Rows[k]["AvlQtySou"] = lblQuantAtSource.Text;
                                    dtCurrntTable.Rows[k]["StockLocationID"] = ddlTansTo.SelectedValue;
                                    dtCurrntTable.Rows[k]["TransTo"] = ddlTansTo.SelectedItem;
                                    //dtCurrntTable.Rows[k]["AvlQty"] = txtQuantAtDest.Text;
                                    dtCurrntTable.Rows[k]["AvlQty"] =0;
                                    dtCurrntTable.Rows[k]["Qty"] = txtTansQty.Text;
                                    dtCurrntTable.Rows[k]["rate"] = txtrate.Text;
                                    dtCurrntTable.Rows[k]["Unit"] = lblunitout.Text;

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
                                    dtTableRow["TransFrom"] = lblLocation.Text;
                                    dtTableRow["AvlQtySou"] = lblQuantAtSource.Text;
                                    dtTableRow["StockLocationID"] = ddlTansTo.SelectedValue;
                                    dtTableRow["TransTo"] = ddlTansTo.SelectedItem;

                                    //dtTableRow["AvlQty"] = txtQuantAtDest.Text;
                                    dtTableRow["AvlQty"] = 0;
                                    dtTableRow["Qty"] = txtTansQty.Text;
                                    dtTableRow["rate"] = txtrate.Text;
                                    dtTableRow["Unit"] = lblunitout.Text;

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
                        obj_Comm.ShowPopUpMsg("Transferred Location should no be same", this.Page);
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
                    lblLocation.Text = GridDetails.Rows[Index].Cells[7].Text;
                    ddlTansTo.SelectedValue = GridDetails.Rows[Index].Cells[9].Text;
                    lblQuantAtSource.Text = GridDetails.Rows[Index].Cells[8].Text;
                    txtQuantAtDest.Text = GridDetails.Rows[Index].Cells[11].Text;
                    txtTansQty.Text = GridDetails.Rows[Index].Cells[12].Text;
                    txtrate.Text = GridDetails.Rows[Index].Cells[13].Text;
                    lblunitout.Text = GridDetails.Rows[Index].Cells[6].Text;
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
                        int times = 0;
                        if (RdoType.SelectedValue.Equals("TO"))
                        {
                            times=GridDetails.Rows.Count;
                        }
                        else
                        {
                            times=GRDTRANSFERIN.Rows.Count;
                        }
                        for (int i = 0; i < times; i++)
                        {
                            Entity_Trans.CategoryId = Convert.ToInt32(dtInsert.Rows[i]["CategoryId"].ToString());
                            Entity_Trans.ItemId = Convert.ToInt32(dtInsert.Rows[i]["ItemId"].ToString());
                            Entity_Trans.QtyAtSource = Convert.ToDecimal(dtInsert.Rows[i]["AvlQtySou"].ToString());
                            Entity_Trans.QtyAtDest = Convert.ToDecimal(dtInsert.Rows[i]["AvlQty"].ToString());
                            Entity_Trans.TransQty = Convert.ToDecimal(dtInsert.Rows[i]["Qty"].ToString());
                            Entity_Trans.rate = Convert.ToDecimal(dtInsert.Rows[i]["rate"].ToString());
                            if (RdoType.SelectedValue.Equals("TO"))
                            {
                                Entity_Trans.TransFrom = Convert.ToInt32(Session["CafeteriaId"]);
                                Entity_Trans.TransTo = Convert.ToInt32(dtInsert.Rows[i]["StockLocationID"].ToString());
                            }
                            else
                            {
                                Entity_Trans.TransFrom = Convert.ToInt32(dtInsert.Rows[i]["StockLocationID"].ToString());
                                Entity_Trans.TransTo = Convert.ToInt32(Session["CafeteriaId"]);
                            }
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
            if (RdoType.SelectedValue.Equals("TO"))
            {
                if (GridDetails.Rows.Count > 0 && !String.IsNullOrEmpty(GridDetails.Rows[0].Cells[3].Text) && !GridDetails.Rows[0].Cells[3].Text.Equals("&nbsp;"))
                {
                    flag = true;
                }
            }
            else
            {
                if (GRDTRANSFERIN.Rows.Count > 0 && !String.IsNullOrEmpty(GRDTRANSFERIN.Rows[0].Cells[3].Text) && !GRDTRANSFERIN.Rows[0].Cells[3].Text.Equals("&nbsp;"))
                {
                    flag = true;
                }
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
                if (RdoType.SelectedValue.Equals("TI"))
                    Entity_Trans.Type = "I";
                else
                    Entity_Trans.Type = "O";
                InsertRow = Obj_Trans.InsertRecord(ref Entity_Trans, out StrError);
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
                            
                            Entity_Trans.QtyAtSource = Convert.ToDecimal(dtInsert.Rows[i]["AvlQtySou"].ToString());
                            
                            Entity_Trans.QtyAtDest = Convert.ToDecimal(dtInsert.Rows[i]["AvlQty"].ToString());
                            Entity_Trans.TransQty = Convert.ToDecimal(dtInsert.Rows[i]["Qty"].ToString());
                            Entity_Trans.rate = Convert.ToDecimal(dtInsert.Rows[i]["rate"].ToString());
                            if (RdoType.SelectedValue.Equals("TO"))
                            {
                                Entity_Trans.TransFrom = Convert.ToInt32(Session["CafeteriaId"]);
                                Entity_Trans.TransTo = Convert.ToInt32(dtInsert.Rows[i]["StockLocationID"].ToString());
                            }
                            else
                            {
                                Entity_Trans.TransFrom = Convert.ToInt32(dtInsert.Rows[i]["StockLocationID"].ToString());
                                Entity_Trans.TransTo = Convert.ToInt32(Session["CafeteriaId"]);
                            }
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
                            if (DS.Tables[0].Rows[0]["Type"].ToString() == "O")
                            {
                                TxtTranNo.Text = DS.Tables[0].Rows[0]["TransNo"].ToString();
                                TRNO.Visible = true;
                                TxtDate.Text = DS.Tables[0].Rows[0]["Date"].ToString();
                                LblEmployee.Text = Session["UserName"].ToString();
                                //ddlBy.SelectedValue = DS.Tables[0].Rows[0]["TransBy"].ToString();
                                txtNotes.Text = DS.Tables[0].Rows[0]["Notes"].ToString();
                                RdoType.SelectedValue = "TO";
                                RdoType.Enabled = false;
                                RdoType_SelectedIndexChanged(sender, e);
                            }
                            else
                            {
                                TxtTranNo.Text = DS.Tables[0].Rows[0]["TransNo"].ToString();
                                TRNO.Visible = true;
                                TxtDate.Text = DS.Tables[0].Rows[0]["Date"].ToString();
                                LblEmployee.Text = Session["UserName"].ToString();
                                //ddlBy.SelectedValue = DS.Tables[0].Rows[0]["TransBy"].ToString();
                                txtNotes.Text = DS.Tables[0].Rows[0]["Notes"].ToString();
                                RdoType.SelectedValue = "TI";
                                RdoType.Enabled = false;
                                RdoType_SelectedIndexChanged(sender, e);
                            }
                        }
                        else
                        {
                            MakeEmptyForm();
                        }
                        if (DS.Tables[1].Rows.Count > 0)
                        {
                            if (RdoType.SelectedValue.Equals("TI"))
                            {
                                GRDTRANSFERIN.DataSource = DS.Tables[1];
                                GRDTRANSFERIN.DataBind();
                                ViewState["CurrentTable"] = DS.Tables[1];
                            }
                            else
                            {
                                GridDetails.DataSource = DS.Tables[1];
                                GridDetails.DataBind();
                                ViewState["CurrentTable"] = DS.Tables[1];
                            }
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
            //txtQuantAtDest.Text = DS.Tables[0].Rows[0][0].ToString();
        }
        else
        {
           // txtQuantAtDest.Text = "0.00";
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
        }
        else
        {
            lblQuantAtSource.Text = "0.00";
        }
        //-------Get Rate Of That Item
        DataSet ds = new DataSet();
        ds = Obj_Trans.GetRate(ItemID, out StrError);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtrate.Text = ds.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            txtrate.Text = "0.00";
        }
        DataSet DS1 = new DataSet();
        DS1 = Obj_Trans.GetUnit(ItemID, out StrError);
        if (DS1.Tables.Count > 0 && DS1.Tables[0].Rows.Count > 0)
        {
            lblunitout.Text = DS1.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            lblunitout.Text = "--";
        }
    }
    protected void RdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RdoType.SelectedValue.Equals("TI"))
        {
            TRTRANSFERIN.Visible =TRTRANSFERINDETAILS.Visible= true;
            TRTRANSFEROUT.Visible = TRTRANSFEROUTDETAILS.Visible = false;
        }
        else
        {
            TRTRANSFERIN.Visible = TRTRANSFERINDETAILS.Visible = false;
            TRTRANSFEROUT.Visible = TRTRANSFEROUTDETAILS.Visible = true;
        }
    }
    protected void DDLCATEGORYIN_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32(DDLCATEGORYIN.SelectedValue);
        if (ID > 0)
        {
            DS = Obj_Trans.GetItems(ID, out StrError);

            if (DS.Tables[0].Rows.Count > 0)
            {
                DDLITEMSIN.DataSource = DS.Tables[0];
                DDLITEMSIN.DataTextField = "ItemName";
                DDLITEMSIN.DataValueField = "ItemId";
                DDLITEMSIN.DataBind();
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
                    DDLITEMSIN.DataSource = DS2.Tables[3];
                    DDLITEMSIN.DataTextField = "ItemName";
                    DDLITEMSIN.DataValueField = "ItemId";
                    DDLITEMSIN.DataBind();
                }
            }
        }
    }
    protected void IMGBTNADDIN_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (DDLCATEGORYIN.SelectedValue == "0")
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            if (flag == true)
            {
                int ItemID = Convert.ToInt32(DDLITEMSIN.SelectedValue);
                DS = Obj_Trans.GetCategory(ItemID, out StrError);
                CategoryID = Convert.ToInt32(DS.Tables[0].Rows[0]["CategoryId"]);
                CategoryName = Convert.ToString(DS.Tables[0].Rows[0]["CategoryName"]);
            }
            else
            {
                CategoryID = Convert.ToInt32(DDLCATEGORYIN.SelectedValue);
                CategoryName = DDLCATEGORYIN.SelectedItem.ToString();
            }
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrntTable = (DataTable)ViewState["CurrentTable"];
                bool DupFlag = false;
                int k = 0;
                DataRow dtTableRow = null;
                if (dtCurrntTable.Rows.Count >= 1)
                {
                    decimal Qty1 = Convert.ToDecimal(TXTTRANSFERQTYIN.Text);
                    int locationID = Convert.ToInt32(Session["CafeteriaId"]);
                    int TransferFrom = Convert.ToInt32(DDLTRANSFROMIN.SelectedValue.ToString());
                    if (locationID != TransferFrom)
                    {
                        if (Qty1 > 0)
                        {
                            if (dtCurrntTable.Rows.Count >= 1 && dtCurrntTable.Rows[0]["Item"].ToString().Equals(""))
                            {
                                dtCurrntTable.Rows.RemoveAt(0);
                            }
                            if (ViewState["GridIndex"] != null)
                            {
                                for (int i = 0; i < dtCurrntTable.Rows.Count; i++)
                                {
                                    if ((Convert.ToInt32(dtCurrntTable.Rows[i]["ItemId"]) == Convert.ToInt32(DDLITEMSIN.SelectedValue))
                                        && (Convert.ToInt32(dtCurrntTable.Rows[i]["StockLocationID"]) == Convert.ToInt32(DDLTRANSFROMIN.SelectedValue)))
                                    {
                                        DupFlag = true;
                                        k = i;
                                    }
                                }
                                if (DupFlag == true)
                                {
                                    dtCurrntTable.Rows[k]["CategoryName"] = CategoryName;
                                    dtCurrntTable.Rows[k]["CategoryId"] = CategoryID;
                                    dtCurrntTable.Rows[k]["ItemId"] = Convert.ToInt32(DDLITEMSIN.SelectedValue);
                                    dtCurrntTable.Rows[k]["Item"] = DDLITEMSIN.SelectedItem;
                                    dtCurrntTable.Rows[k]["TransFrom"] = DDLTRANSFROMIN.SelectedItem;
                                    dtCurrntTable.Rows[k]["AvlQtySou"] = 0;
                                    dtCurrntTable.Rows[k]["StockLocationID"] = DDLTRANSFROMIN.SelectedValue;
                                    dtCurrntTable.Rows[k]["TransTo"] = LBLTRANSINTO.Text;
                                    dtCurrntTable.Rows[k]["AvlQty"] = 0;
                                    dtCurrntTable.Rows[k]["Qty"] = TXTTRANSFERQTYIN.Text;
                                    dtCurrntTable.Rows[k]["rate"] = TXTRATEIN.Text;
                                    dtCurrntTable.Rows[k]["Unit"] = LBLUNIT.Text;

                                    ViewState["CurrentTable"] = dtCurrntTable;
                                    GRDTRANSFERIN.DataSource = dtCurrntTable;
                                    GRDTRANSFERIN.DataBind();
                                    MakeControlEmpty();
                                }
                                else
                                {
                                    int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                                    dtCurrntTable.Rows[rowindex]["CategoryId"] = CategoryID;
                                    dtCurrntTable.Rows[rowindex]["CategoryName"] = CategoryName;
                                    dtCurrntTable.Rows[rowindex]["Item"] = DDLITEMSIN.SelectedItem;
                                    dtCurrntTable.Rows[rowindex]["ItemId"] = DDLITEMSIN.SelectedValue;
                                    dtCurrntTable.Rows[rowindex]["TransFrom"] = DDLTRANSFROMIN.SelectedItem;
                                    dtCurrntTable.Rows[rowindex]["AvlQtySou"] = 0;
                                    dtCurrntTable.Rows[rowindex]["StockLocationID"] = DDLTRANSFROMIN.SelectedValue;
                                    dtCurrntTable.Rows[rowindex]["TransTo"] = LBLTRANSINTO.Text;
                                    dtCurrntTable.Rows[rowindex]["AvlQty"] = 0;
                                    dtCurrntTable.Rows[rowindex]["Qty"] = TXTTRANSFERQTYIN.Text;
                                    dtCurrntTable.Rows[rowindex]["rate"] = TXTRATEIN.Text;
                                    dtCurrntTable.Rows[rowindex]["Unit"] = LBLUNIT.Text;


                                    ViewState["CurrentTable"] = dtCurrntTable;
                                    GRDTRANSFERIN.DataSource = dtCurrntTable;
                                    GRDTRANSFERIN.DataBind();
                                    MakeControlEmpty();
                                }
                            }
                            else
                            {
                                for (int i = 0; i < dtCurrntTable.Rows.Count; i++)
                                {
                                    if ((Convert.ToInt32(dtCurrntTable.Rows[i]["ItemId"]) == Convert.ToInt32(DDLITEMSIN.SelectedValue))
                                        && (Convert.ToInt32(dtCurrntTable.Rows[i]["StockLocationID"]) == Convert.ToInt32(DDLTRANSFROMIN.SelectedValue)))
                                    {
                                        DupFlag = true;
                                        k = i;
                                    }
                                }
                                if (DupFlag == true)
                                {
                                    dtCurrntTable.Rows[k]["CategoryName"] = CategoryName;
                                    dtCurrntTable.Rows[k]["CategoryId"] = CategoryID;
                                    dtCurrntTable.Rows[k]["ItemId"] = Convert.ToInt32(DDLITEMSIN.SelectedValue);
                                    dtCurrntTable.Rows[k]["Item"] = DDLITEMSIN.SelectedItem;
                                    dtCurrntTable.Rows[k]["TransFrom"] = DDLTRANSFROMIN.SelectedItem;
                                    dtCurrntTable.Rows[k]["AvlQtySou"] = 0;
                                    dtCurrntTable.Rows[k]["StockLocationID"] = DDLTRANSFROMIN.SelectedValue;
                                    dtCurrntTable.Rows[k]["TransTo"] = LBLTRANSINTO.Text;
                                    dtCurrntTable.Rows[k]["AvlQty"] = 0;
                                    dtCurrntTable.Rows[k]["Qty"] = TXTTRANSFERQTYIN.Text;
                                    dtCurrntTable.Rows[k]["rate"] = TXTRATEIN.Text;
                                    dtCurrntTable.Rows[k]["Unit"] = LBLUNIT.Text;

                                    ViewState["CurrentTable"] = dtCurrntTable;
                                    GRDTRANSFERIN.DataSource = dtCurrntTable;
                                    GRDTRANSFERIN.DataBind();
                                    MakeControlEmpty();

                                }
                                else
                                {
                                    dtTableRow = dtCurrntTable.NewRow();
                                    dtTableRow["#"] = 0;
                                    dtTableRow["CategoryId"] = CategoryID;
                                    dtTableRow["CategoryName"] = CategoryName;
                                    dtTableRow["ItemId"] = Convert.ToInt32(DDLITEMSIN.SelectedValue);
                                    dtTableRow["Item"] = DDLITEMSIN.SelectedItem;
                                    dtTableRow["TransFrom"] = DDLTRANSFROMIN.SelectedItem;
                                    dtTableRow["AvlQtySou"] = 0;
                                    dtTableRow["StockLocationID"] = DDLTRANSFROMIN.SelectedValue;
                                    dtTableRow["TransTo"] = LBLTRANSINTO.Text;
                                    dtTableRow["AvlQty"] = 0;
                                    dtTableRow["Qty"] = TXTTRANSFERQTYIN.Text;
                                    dtTableRow["rate"] = TXTRATEIN.Text;
                                    dtTableRow["Unit"] = LBLUNIT.Text;

                                    dtCurrntTable.Rows.Add(dtTableRow);

                                    ViewState["CurrentTable"] = dtCurrntTable;
                                    GRDTRANSFERIN.DataSource = dtCurrntTable;
                                    GRDTRANSFERIN.DataBind();
                                    MakeControlEmpty();
                                }
                            }
                        }
                        else
                        {
                            obj_Comm.ShowPopUpMsg("Transfer Quantity Should not be greater than Zero (0)", this.Page);
                            txtTansQty.Text = string.Empty;
                            txtTansQty.Focus();
                        }
                    }
                    else
                    {
                        obj_Comm.ShowPopUpMsg("Transfer In Location should no be same", this.Page);
                    }
                }

            }

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    protected void DDLITEMSIN_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int ItemID = Convert.ToInt32(DDLITEMSIN.SelectedValue);
            DS = Obj_Trans.GetUnit(ItemID,out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                LBLUNIT.Text = DS.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                LBLUNIT.Text = "--";
            }
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void GRDTRANSFERIN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Index;
            if (e.CommandName == "SelectGrid")
            {
                if ((!(string.IsNullOrEmpty(GRDTRANSFERIN.Rows[0].Cells[3].Text))) && (GRDTRANSFERIN.Rows[0].Cells[3].Text.Equals("&nbsp;")))
                {
                    obj_Comm.ShowPopUpMsg("There Is No Record To Edit", this.Page);
                }
                else
                {
                    IMGBTNADDIN.ImageUrl = "~/Images/Icon/GridUpdate.png";
                    IMGBTNADDIN.ToolTip = "Update";

                    Index = Convert.ToInt32(e.CommandArgument);
                    ViewState["GridIndex"] = Index;
                    DDLCATEGORYIN.SelectedValue = GRDTRANSFERIN.Rows[Index].Cells[2].Text;
                    DDLCATEGORYIN_SelectedIndexChanged(sender, e);
                    DDLITEMSIN.SelectedValue = GRDTRANSFERIN.Rows[Index].Cells[4].Text;
                    DDLTRANSFROMIN.SelectedValue= GRDTRANSFERIN.Rows[Index].Cells[9].Text;
                    LBLTRANSINTO.Text = GRDTRANSFERIN.Rows[Index].Cells[10].Text;
                    //lblQuantAtSource.Text = GRDTRANSFERIN.Rows[Index].Cells[7].Text;
                    //txtQuantAtDest.Text = GRDTRANSFERIN.Rows[Index].Cells[10].Text;
                    TXTTRANSFERQTYIN.Text = GRDTRANSFERIN.Rows[Index].Cells[12].Text;
                    TXTRATEIN.Text = GRDTRANSFERIN.Rows[Index].Cells[13].Text;
                    LBLUNIT.Text = GRDTRANSFERIN.Rows[Index].Cells[6].Text;
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    protected void GRDTRANSFERIN_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = Convert.ToInt32(GRDTRANSFERIN.Rows.Count);
            if (i > 0 && GRDTRANSFERIN.Rows[e.RowIndex].Cells[3].Text != "&nbsp;" && GRDTRANSFERIN.Rows[e.RowIndex].Cells[3].Text != "")
            {
                if (ViewState["CurrentTable"] != null)
                {
                    int id = e.RowIndex;
                    DataTable dt = (DataTable)ViewState["CurrentTable"];

                    dt.Rows.RemoveAt(id);
                    if (dt.Rows.Count > 0)
                    {
                        GRDTRANSFERIN.DataSource = dt;
                        ViewState["CurrentTable"] = dt;
                        GRDTRANSFERIN.DataBind();
                    }
                    else
                    {
                        SetInitialRowIN();
                    }
                    MakeControlEmpty();
                }
            }
        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }
}
