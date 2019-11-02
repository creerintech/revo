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


public partial class Transactions_AssignStock : System.Web.UI.Page
{
    #region [Private Variables]
        DMStockMaster Obj_StockMaster = new DMStockMaster();
        StockMaster Entity_StockMaster = new StockMaster();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        decimal Amount = 0;
        DataTable DtEditPO, dtavlqty;
        public static int ItemId = 0,LocationId=0;
        private string StrCondition = string.Empty;
        private string StrCond= string.Empty;
        private string StrError = string.Empty;
        private static DataTable dtLoc = new DataTable();
        private static DataTable dtunit = new DataTable();
        private static bool FlagPending = false;
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
        private int issusecond;
    #endregion

    #region User Function

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

                DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Issue Register'");
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
                    ReportGrid1.Visible = false;
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
                    foreach (GridViewRow GRow in ReportGrid1.Rows)
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
                        foreach (GridViewRow GRow in ReportGrid1.Rows)
                        {
                            GRow.FindControl("ImgBtnDelete").Visible = false;
                            FlagDel = true;
                        }
                    }

                    //Checking Edit Right ========
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        foreach (GridViewRow GRow in ReportGrid1.Rows)
                        {
                            GRow.FindControl("ImageGridEdit").Visible = false;
                            FlagEdit = true;
                        }
                    }

                    //Checking Print Right ========
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        foreach (GridViewRow GRow in ReportGrid1.Rows)
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

    public void MakeEmptyForm()
    {
        ViewState["EditId"] = null;
        if(!FlagAdd)
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnDelete.Visible = false;

        TxtStockNo.Enabled = false;
        TxtStockNo.Text = string.Empty;
        TxtStockDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        TxtConsumptionDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        TxtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
        TxtToDateR.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        TxtFromDateR.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
        TxtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        RdoType.SelectedIndex = 0;
        TRINW.Visible = true;
        TRCATEGORY.Visible = false;
        TRDiscription.Visible = false;
        TrReq.Visible = false;
        lblRequisitionNo.Visible = false;
        ddlRequisitnNo.Visible = true;
        chk_close.Visible = false;
        chk_close.Checked = false;
        lblclose.Visible = false;
        TxtFromDate.Focus();
        //ReportGrid(StrCondition);
        SetInitialRow();
        SetInitialRowCategoryItem();
        SetInitialRowRequisition();
        GetStockNo();
        BindReportGrid(StrCond);
        TRGRIDINWARD.Visible = true;
        TRGRIDCATEGORYITEM.Visible = TRGRIDREQUISITION.Visible = false;
        LblNo.Text = "";
        TxtRemark.Text = "";
        RdoType.Enabled = true;
        #region[UserRights]
        if (FlagEdit)
        {
            foreach (GridViewRow GRow in ReportGrid1.Rows)
            {
                GRow.FindControl("ImageGridEdit").Visible = false;
            }
        }
        if (FlagDel)
        {
            foreach (GridViewRow GRow in ReportGrid1.Rows)
            {
                GRow.FindControl("ImgBtnDelete").Visible = false;
            }
        }
        if (FlagPrint)
        {
            foreach (GridViewRow GRow in ReportGrid1.Rows)
            {
                GRow.FindControl("ImgBtnPrint").Visible = false;
            }
        }
        #endregion

    }

    private void MakeControlEmpty()
    {
        DDLINV.SelectedValue = "0";
        DDLCATEGORY.SelectedValue = "0";
        DDLITEMS.SelectedValue = "0";
        ddlRequisitnNo.SelectedValue = "0";
        ViewState["GridIndex"] = null;
        ViewState["GridDetails"] = null;
        ViewState["Flag"] = null;
        ViewState["dttavlqty"] = null;
    }

    public void BindReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            string COND = string.Empty;
            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
            //    COND = COND + " AND S.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}
             DsReport = Obj_StockMaster.GetStock(RepCondition,COND, out StrError);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                ReportGrid1.DataSource = DsReport.Tables[0];
                ReportGrid1.DataBind();
            }
            else
            {
                ReportGrid1.DataSource = null;
                ReportGrid1.DataBind();
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void GetStockNo()
    {
        try
        {
            Ds = Obj_StockMaster.GetStockNo(out StrError);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                TxtStockNo.Text = Ds.Tables[0].Rows[0]["StockNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void CkeckAvlQtyForRequisition()
    {
        //-----check For Availableqty <= 0
        for (int i = 0; i < GRDREQUISITION.Rows.Count; i++)
        {
            DropDownList ddl = (DropDownList)GRDREQUISITION.Rows[i].FindControl("ddlLocation");
            ddl.Enabled = false;
            decimal avl = Convert.ToDecimal(GRDREQUISITION.Rows[i].Cells[6].Text.ToString());
            if (avl <= 0)
            {
                TextBox outwrd = (TextBox)GRDREQUISITION.Rows[i].FindControl("txtOutward");
                outwrd.Text = "0.00";
                //outwrd.Enabled = false;
            }
        }
    }

    private void CheckAvlQtyForItemWise()
    {
        //----To check AvlQty <= 0 then disable OutWardQty Textbox
        for (int i = 0; i < GridDetails1.Rows.Count; i++)
        {
            decimal avl = Convert.ToDecimal(GridDetails1.Rows[i].Cells[6].Text.ToString());
            if (avl <= 0)
            {
                TextBox outwrd = (TextBox)GridDetails1.Rows[i].FindControl("txtOutward");
                outwrd.Text = "0.00";
                outwrd.Enabled = true;
            }
        }
    }

    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("ItemId", typeof(Int32));
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("ItemDetailsId", typeof(Int32));
            dt.Columns.Add("ItemDesc", typeof(string));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("Inward", typeof(decimal));
            dt.Columns.Add("Outward", typeof(decimal));
            dt.Columns.Add("Pending", typeof(decimal));
            dt.Columns.Add("Rate", typeof(decimal));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("LocID", typeof(Int32));
            dt.Columns.Add("UnitConvDtlsId", typeof(Int32));
            dr = dt.NewRow();
            dr["#"] = 0;
            dr["ItemId"] = 0;
            dr["Item"] = "";
            dr["ItemDetailsId"] = 0;
            dr["ItemDesc"] = "";
            dr["Unit"] = "";
            dr["Inward"] = 0;
            dr["Outward"] = 0;
            dr["Pending"] = 0; 
            dr["Rate"] = 0;
            dr["Amount"] = 0;
            dr["LocID"] = 0;
            dr["UnitConvDtlsId"] = 0;
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GridDetails1.DataSource = dt;
            GridDetails1.DataBind();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void SetInitialRowCategoryItem()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("ItemId", typeof(Int32));
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("ItemDetailsId", typeof(Int32));
            dt.Columns.Add("ItemDesc", typeof(string));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("Available", typeof(decimal));
            dt.Columns.Add("Outward", typeof(decimal));
            dt.Columns.Add("Rate", typeof(decimal));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("LocID", typeof(Int32));
            dt.Columns.Add("UnitConvDtlsId", typeof(Int32));
           

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["ItemId"] = 0;
            dr["Item"] = "";
            dr["ItemDetailsId"] = 0;
            dr["ItemDesc"] = "";
            dr["Unit"] = "";
            dr["Available"] = 0;
            dr["Outward"] = 0;
            dr["Rate"] = 0;
            dr["Amount"] = 0;
            dr["LocID"] = 0;
            dr["UnitConvDtlsId"] = 0;
            dt.Rows.Add(dr);

            ViewState["CurrentTableCategoryItem"] = dt;
            GRDCATEGORYITEM.DataSource = dt;
            GRDCATEGORYITEM.DataBind();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void SetInitialRowRequisition()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("ItemId", typeof(Int32));
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("ItemDetailsId", typeof(Int32));
            dt.Columns.Add("ItemDesc", typeof(string));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("Available", typeof(decimal));
            dt.Columns.Add("Outward", typeof(decimal));
            dt.Columns.Add("Rate", typeof(decimal));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("LocID", typeof(Int32));
            dt.Columns.Add("Inward", typeof(decimal));
            dt.Columns.Add("UnitConvDtlsId", typeof(Int32));
            dr = dt.NewRow();

            dr["#"] = 0;
            dr["ItemId"] = 0;
            dr["Item"] = "";
            dr["ItemDetailsId"] = 0;
            dr["ItemDesc"] = "";
            dr["Unit"] = "";
            dr["Available"] = 0;
            dr["Outward"] = 0;
            dr["Rate"] = 0;
            dr["Amount"] = 0;
            dr["LocID"] = 0;
            dr["Inward"] = 0;
            dr["UnitConvDtlsId"] = 0;
            dt.Rows.Add(dr);

            ViewState["CurrentTableRequisition"] = dt;
            GRDREQUISITION.DataSource = dt;
            GRDREQUISITION.DataBind();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void FillCombo()
    {
        try
        {
            string COND = string.Empty;
            string s = Session["TransactionSiteID"].ToString();
            if (Session["UserRole"].ToString() == "Administrator")
            {
                COND = COND;
            }
            else
            {
                COND = COND + " AND P.Location=" + Convert.ToInt32(Session["TransactionSiteID"].ToString());
            }
            Ds = Obj_StockMaster.FillCombo(COND, Convert.ToInt32(Session["UserID"]), out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    DDLINV.DataSource = Ds.Tables[0];
                    DDLINV.DataTextField = "InwardNo";
                    DDLINV.DataValueField = "InwardId";
                    DDLINV.DataBind();
                }
                if (Ds.Tables[1].Rows.Count > 0)
                {
                    DDLCATEGORY.DataSource = Ds.Tables[1];
                    DDLCATEGORY.DataTextField = "CategoryName";
                    DDLCATEGORY.DataValueField = "CategoryId";
                    DDLCATEGORY.DataBind();

                }
                if (Ds.Tables[2].Rows.Count > 0)
                {
                    DDLITEMS.DataSource = Ds.Tables[2];
                    DDLITEMS.DataTextField = "ItemName";
                    DDLITEMS.DataValueField = "ItemId";
                    DDLITEMS.DataBind();

                }
                if (Ds.Tables[3].Rows.Count > 0)
                {
                    dtLoc = Ds.Tables[3].Copy();
                }
                if (Ds.Tables[4].Rows.Count > 0)
                {
                    ddlRequisitnNo.DataSource = Ds.Tables[4];
                    ddlRequisitnNo.DataTextField = "RequisitionNo";
                    ddlRequisitnNo.DataValueField = "RequisitionCafeId";
                    ddlRequisitnNo.DataBind();
                }
                if (Ds.Tables[5].Rows.Count > 0)
                {
                    dtunit = Ds.Tables[5].Copy();
                }
                if (Ds.Tables[6].Rows.Count > 0)
                {
                    ddlSubcategory.DataSource = Ds.Tables[6];
                    ddlSubcategory.DataTextField = "SubCategory";
                    ddlSubcategory.DataValueField = "SubCategoryId";
                    ddlSubcategory.DataBind();
                }
            }
        }
        catch (Exception ex) {  }

    }

    private void ReportGrid(string StrCondition,string inwardid, string Query)
    {
        try
        {
            DataSet DsGrd = new DataSet();
            DataSet DsChkId = new DataSet();
            DataTable dt = new DataTable();
            if (Query == "IW")
            {
                DsChkId = Obj_StockMaster.ChkInwardExit(Convert.ToInt32(inwardid), out StrError);
                if (DsChkId.Tables.Count > 0 && DsChkId.Tables[0].Rows.Count > 0)
                {
                    DsGrd = Obj_StockMaster.GetDetailForExitId(Convert.ToInt32(inwardid), out StrError);
                    if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
                    {
                        GridDetails1.DataSource = DsGrd;
                        GridDetails1.DataBind();
                        CheckAvlQtyForItemWise();
                        ViewState["CurrentTable"] = DsGrd;
                        //--------ForceFully Closed Requisition---------
                        if (chk_close.Checked)
                        {
                            if(ReportGrid1.Columns[3].ToString()=="Opened")
                            {
                                string status1 = "Closed";
                                int updateStatus1 = Obj_StockMaster.UpdateStatusFlag(status1, ref Entity_StockMaster, out StrError);
                            }
                        }
                    }
                    else
                    {
                        GridDetails1.DataSource = null;
                        GridDetails1.DataBind();
                        CheckAvlQtyForItemWise();
                        SetInitialRow();
                    }
                    DsGrd = null;
                }
                else
                {
                    StrCondition = "and IR.InwardId = " + inwardid;
                    DsGrd = Obj_StockMaster.GetDetailOnCond(StrCondition, out StrError);
                    if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
                    {
                        GridDetails1.DataSource = DsGrd;
                        GridDetails1.DataBind();
                        CheckAvlQtyForItemWise();
                        ViewState["CurrentTable"] = DsGrd;
                    }
                    else
                    {
                        GridDetails1.DataSource = null;
                        GridDetails1.DataBind();
                        CheckAvlQtyForItemWise();
                        SetInitialRow();
                    }
                    DsGrd = null;
                }
            }
            if (Query == "C")
            {
                DsGrd = Obj_StockMaster.GetDetailOnCondForItem(Convert.ToInt32(inwardid),"", out StrError,"C");
                AddAllItemToGrid(DsGrd);

            }
            if (Query == "I")
            {
                if (ddldesc.SelectedItem.Text == "--Select Description--")
                {
                    DsGrd = Obj_StockMaster.GetDetailOnCondForItem(Convert.ToInt32(inwardid), "", out StrError, "I");
                }
                else
                {
                    DsGrd = Obj_StockMaster.GetDetailOnCondForItem(Convert.ToInt32(inwardid), ddldesc.SelectedItem.Text, out StrError, "I");
                }
                dtavlqty = DsGrd.Tables[1];
                ViewState["dttavlqty"] = DsGrd.Tables[1];
                AddAllItemToGrid(DsGrd);//AddItemToGrid(DsGrd);
            }

            if (Query == "RQ")
            {
                LocationId = Convert.ToInt32(Session["TransactionSiteID"]);
                DsChkId = Obj_StockMaster.ChkInwardExit(Convert.ToInt32(inwardid), out StrError);
                if (DsChkId.Tables.Count > 0 && DsChkId.Tables[0].Rows.Count > 0)
                {
                   // obj_Comman.ShowPopUpMsg("Sorry Stock Is Already Assinged To This Requisition", this.Page);
                    DsGrd = Obj_StockMaster.GetDetailOnCondForRequisitnPresent(StrCondition, inwardid, out StrError);
                    if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
                    {
                        issusecond = 1;
                        GRDREQUISITION.DataSource = DsGrd;
                        GRDREQUISITION.DataBind();
                        CkeckAvlQtyForRequisition();
                        ViewState["CurrentTableRequisition"] = DsGrd;
                    }
                    else
                    {
                        GRDREQUISITION.DataSource = null;
                        GRDREQUISITION.DataBind();

                        CkeckAvlQtyForRequisition();
                        SetInitialRowRequisition();
                    }
                    DsGrd = null;
                }
                else
                {
                    DsGrd = Obj_StockMaster.GetDetailOnCondForRequisitn(StrCondition,LocationId, out StrError);
                    if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
                    {
                        issusecond = 1;
                        GRDREQUISITION.DataSource = DsGrd;
                        GRDREQUISITION.DataBind();
                        CkeckAvlQtyForRequisition();
                        ViewState["CurrentTableRequisition"] = DsGrd;
                    }
                    else
                    {
                        GRDREQUISITION.DataSource = null;
                        GRDREQUISITION.DataBind();
                     
                        CkeckAvlQtyForRequisition();
                        SetInitialRowRequisition();
                    }
                    DsGrd = null;
                }
            }
        
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private bool ChkGridRow()
    {
        bool FlagChk = false;
        if (GridDetails1.Rows.Count > 0 && !string.IsNullOrEmpty(GridDetails1.Rows[0].Cells[1].Text) && !GridDetails1.Rows[0].Cells[1].Text.Equals("&nbsp;"))
        {
            FlagChk = true;
        }
        return FlagChk;
    }

    public void AddAllItemToGrid(DataSet Ds)
    {
        GetDataTable(GRDCATEGORYITEM);
        for (int j = 0; j < Ds.Tables[0].Rows.Count; j++)
        {
            int ItemID_All = Convert.ToInt32(Ds.Tables[0].Rows[j]["ItemID"]);
            string ItemDESC = Convert.ToString(Ds.Tables[0].Rows[j]["ItemDesc"]);
            if (ViewState["CurrentTableCategoryItem"] != null)
            {
                bool DupFlag = false;
                int k = 0;
                DataTable DTTABLE = (DataTable)ViewState["CurrentTableCategoryItem"];
                
                DataRow drCurrentRow = null;
                if (DTTABLE.Rows.Count > 0)
                {                   
                    if (ViewState["GridIndex"] == null)
                    {
                        for (int i = 0; i < DTTABLE.Rows.Count; i++)
                        {
                            int ItemID_Single = Convert.ToInt32(DTTABLE.Rows[i]["ItemID"]);
                            string ItemDESC_D = Convert.ToString(DTTABLE.Rows[i]["ItemDesc"]);
                            if (DTTABLE.Rows.Count == 1 && (DTTABLE.Rows[0]["#"].ToString()) == "0")
                            {
                                DTTABLE.Rows.RemoveAt(0);                                
                            }
                            if (Convert.ToInt32(ItemID_Single) == Convert.ToInt32(ItemID_All) && ItemDESC_D==ItemDESC)
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            DTTABLE.Rows[k]["#"] = Convert.ToString(DTTABLE.Rows[k]["#"]);
                            DTTABLE.Rows[k]["ItemId"] = Convert.ToString(DTTABLE.Rows[k]["ItemId"]);
                            DTTABLE.Rows[k]["Item"] = Convert.ToString(DTTABLE.Rows[k]["Item"]);
                            DTTABLE.Rows[k]["ItemDetailsId"] = Convert.ToString(DTTABLE.Rows[k]["ItemDetailsId"]);
                            DTTABLE.Rows[k]["ItemDesc"] = Convert.ToString(DTTABLE.Rows[k]["ItemDesc"]);
                            DTTABLE.Rows[k]["Unit"] = Convert.ToString(DTTABLE.Rows[k]["Unit"]);
                            DTTABLE.Rows[k]["Available"] = Convert.ToString(DTTABLE.Rows[k]["Available"]);
                            DTTABLE.Rows[k]["Rate"] = Convert.ToDecimal(DTTABLE.Rows[k]["Rate"]);
                            DTTABLE.Rows[k]["Outward"] = Convert.ToDecimal(DTTABLE.Rows[k]["Outward"]);
                            DTTABLE.Rows[k]["LocId"] = Convert.ToDecimal(DTTABLE.Rows[k]["LocId"]);
                            DTTABLE.Rows[k]["Amount"] = Convert.ToDecimal(DTTABLE.Rows[k]["Amount"]);
                            ViewState["CurrentTableCategoryItem"] = DTTABLE;
                            GRDCATEGORYITEM.DataSource = DTTABLE;
                            GRDCATEGORYITEM.DataBind();
                            //----Hide Inward And Pending Qty----
                            CheckAvlQtyForItemWise();

                            DtEditPO = (DataTable)ViewState["CurrentTable"];
                        }
                        else
                        {
                            drCurrentRow = DTTABLE.NewRow();
                            drCurrentRow["#"] = Ds.Tables[0].Rows[j]["#"].ToString();
                            drCurrentRow["ItemID"] = Ds.Tables[0].Rows[j]["ItemId"].ToString();
                            drCurrentRow["Item"] = Ds.Tables[0].Rows[j]["Item"].ToString();

                            drCurrentRow["ItemDetailsId"] = Ds.Tables[0].Rows[j]["ItemDetailsId"].ToString();
                            drCurrentRow["ItemDesc"] = Ds.Tables[0].Rows[j]["ItemDesc"].ToString();


                            drCurrentRow["Unit"] = Ds.Tables[0].Rows[j]["Unit"].ToString();
                            drCurrentRow["Available"] = Ds.Tables[0].Rows[j]["Available"].ToString();
                            drCurrentRow["Rate"] = Ds.Tables[0].Rows[j]["Rate"].ToString();
                            drCurrentRow["Outward"] = Ds.Tables[0].Rows[j]["Outward"].ToString();
                            drCurrentRow["LocId"] = Ds.Tables[0].Rows[j]["LocId"].ToString();
                            drCurrentRow["Amount"] = Ds.Tables[0].Rows[j]["Amount"].ToString();
                            DTTABLE.Rows.Add(drCurrentRow);
                            ViewState["CurrentTableCategoryItem"] = DTTABLE;
                            ViewState["GridDetails1"] = DTTABLE;
                            GRDCATEGORYITEM.DataSource = DTTABLE;
                            GRDCATEGORYITEM.DataBind();
                            //----Hide Inward And Pending Qty----
                            CheckAvlQtyForItemWise();
                            int g = k;
                            ViewState["GridIndex"] = g++;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < DTTABLE.Rows.Count; i++)
                        {
                            int ItemID_Single = Convert.ToInt32(DTTABLE.Rows[i]["ItemID"]);
                            string ItemDESC_Single = Convert.ToString(DTTABLE.Rows[i]["ItemDesc"]);
                            if (Convert.ToInt32(ItemID_Single) == Convert.ToInt32(ItemID_All) && ItemDESC_Single == ItemDESC)
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            // DTROW = DTTABLE.NewRow();
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                            DTTABLE.Rows[k]["#"] = Convert.ToString(DTTABLE.Rows[k]["#"]);
                            DTTABLE.Rows[k]["ItemID"] = Convert.ToString(DTTABLE.Rows[k]["ItemId"]);
                            DTTABLE.Rows[k]["Item"] = Convert.ToString(DTTABLE.Rows[k]["Item"]);
                            DTTABLE.Rows[k]["ItemDetailsId"] = Convert.ToString(DTTABLE.Rows[k]["ItemDetailsId"]);
                            DTTABLE.Rows[k]["ItemDesc"] = Convert.ToString(DTTABLE.Rows[k]["ItemDesc"]);

                            DTTABLE.Rows[k]["Unit"] = Convert.ToString(DTTABLE.Rows[k]["Unit"]);
                            DTTABLE.Rows[k]["Available"] = Convert.ToDecimal(DTTABLE.Rows[k]["Available"]);
                            DTTABLE.Rows[k]["Rate"] = Convert.ToDecimal(DTTABLE.Rows[k]["Rate"]);
                            DTTABLE.Rows[k]["Outward"] = Convert.ToDecimal(DTTABLE.Rows[k]["Outward"]);
                            DTTABLE.Rows[k]["LocId"] = Convert.ToDecimal(DTTABLE.Rows[k]["LocId"]);
                            DTTABLE.Rows[k]["Amount"] = Convert.ToDecimal(DTTABLE.Rows[k]["Amount"]);
                            ViewState["CurrentTableCategoryItem"] = DTTABLE;
                            GRDCATEGORYITEM.DataSource = DTTABLE;
                            GRDCATEGORYITEM.DataBind();
                            //----Hide Inward And Pending Qty----
                            CheckAvlQtyForItemWise();
                            DtEditPO = (DataTable)ViewState["CurrentTable"];
                        }
                        else
                        {
                            
                            drCurrentRow = DTTABLE.NewRow();
                            drCurrentRow["#"] = Ds.Tables[0].Rows[j]["#"].ToString();
                            drCurrentRow["ItemID"] = Ds.Tables[0].Rows[j]["ItemID"].ToString();
                            drCurrentRow["Item"] = Ds.Tables[0].Rows[j]["Item"].ToString();
                            drCurrentRow["ItemDetailsId"] = Ds.Tables[0].Rows[j]["ItemDetailsId"].ToString();
                            drCurrentRow["ItemDesc"] = Ds.Tables[0].Rows[j]["ItemDesc"].ToString();
                            drCurrentRow["Unit"] = Ds.Tables[0].Rows[j]["Unit"].ToString();
                            drCurrentRow["Available"] = Ds.Tables[0].Rows[j]["Available"].ToString();
                            drCurrentRow["Rate"] = Ds.Tables[0].Rows[j]["Rate"].ToString();
                            drCurrentRow["Outward"] = Ds.Tables[0].Rows[j]["Outward"].ToString();
                            drCurrentRow["LocId"] = Ds.Tables[0].Rows[j]["LocId"].ToString();
                            drCurrentRow["Amount"] = Ds.Tables[0].Rows[j]["Amount"].ToString();
                            DTTABLE.Rows.Add(drCurrentRow);
                            //newrowindex++;
                            ViewState["CurrentTableCategoryItem"] = DTTABLE;
                            ViewState["GridDetails1"] = DTTABLE;
                            GRDCATEGORYITEM.DataSource = DTTABLE;
                            GRDCATEGORYITEM.DataBind();
                            //----Hide Inward And Pending Qty----
                            CheckAvlQtyForItemWise();
                            DtEditPO = (DataTable)ViewState["GridDetails1"];
                        }
                    }
                }
            }
        }
    
    }

    public void AddAvlQtyToGrid()
    {
        try
        {
            if (ViewState["dttavlqty"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dttavlqty"];
                for (int q = 0; q < GridDetails1.Rows.Count; q++)
                {
                    if (Convert.ToInt32(GridDetails1.Rows[q].Cells[3].Text) == Convert.ToInt32(dt.Rows[q]["ItemId"].ToString()))
                    {
                        GridDetails1.Rows[q].Cells[3].Text=dt.Rows[q]["Closing"].ToString();
                    }
                }
            }
            else
            {
                for (int q = 0; q < GridDetails1.Rows.Count; q++)
                {  
                        GridDetails1.Rows[q].Cells[5].Text = "0";
                }
            }
        }
        catch (Exception ex)
        {

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
            CheckUserRight();
        }
    }

    protected void ImgAddGrid_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataSet DsGrid = new DataSet();
            StrCondition = string.Empty;
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            if (Convert.ToInt32(DDLINV.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and SuplierId=" + Convert.ToInt32(DDLINV.SelectedValue);
            }
            if (Convert.ToInt32(DDLCATEGORY.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IR.InwardId=" + Convert.ToInt32(DDLCATEGORY.SelectedValue);
            }
            ReportGrid(StrCondition, DDLINV.SelectedValue,"Z");
        }
        catch (Exception ex)
        {
           
        }
    }

    protected void GridDetails1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((DropDownList)e.Row.FindControl("ddlLocation")).DataSource = dtLoc;
                ((DropDownList)e.Row.FindControl("ddlLocation")).DataTextField = "Location";
                ((DropDownList)e.Row.FindControl("ddlLocation")).DataValueField = "StockLocationID";
                ((DropDownList)e.Row.FindControl("ddlLocation")).DataBind();
                ((DropDownList)e.Row.FindControl("ddlLocation")).SelectedValue = ((Label)e.Row.FindControl("lblLocID")).Text;
                int p = e.Row.RowIndex;
                int cafeId = Convert.ToInt32(Session["TransactionSiteID"].ToString());
                DataSet dsAvlqty = new DataSet();
                dsAvlqty = Obj_StockMaster.getAvlQty(Convert.ToInt32(e.Row.Cells[1].Text), cafeId, Convert.ToInt32(((Label)e.Row.FindControl("lblUnitId")).Text), Convert.ToDecimal(e.Row.Cells[6].Text),Convert.ToString(e.Row.Cells[4].Text), out StrError);
          
               
                //((DropDownList)e.Row.FindControl("ddlUnitConversion")).DataSource = dsAvlqty.Tables[0];
                //((DropDownList)e.Row.FindControl("ddlUnitConversion")).DataTextField = "UnitFactor";
                //((DropDownList)e.Row.FindControl("ddlUnitConversion")).DataValueField = "UnitConvDtlsId";
                //((DropDownList)e.Row.FindControl("ddlUnitConversion")).DataBind();
                //((DropDownList)e.Row.FindControl("ddlUnitConversion")).SelectedValue = ((Label)e.Row.FindControl("lblUnitId")).Text;

           
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDtls = 0;
        DataTable dtInsert = new DataTable();
        try
        {
            if (ChkGridRow())
            {
                Entity_StockMaster.StockNo = TxtStockNo.Text;
                Entity_StockMaster.StockAsOn = !string.IsNullOrEmpty(TxtStockDate.Text) ? Convert.ToDateTime(TxtStockDate.Text) : Convert.ToDateTime(DateTime.Now.ToShortDateString());
                Entity_StockMaster.ConsumptionDate = !string.IsNullOrEmpty(TxtConsumptionDate.Text) ? Convert.ToDateTime(TxtConsumptionDate.Text) : Convert.ToDateTime(DateTime.Now.ToShortDateString());
                
                //To Save Type of AssignStock
                if (RdoType.SelectedValue == "IW")
                {
                    Entity_StockMaster.Type = "IW";
                }
                if (RdoType.SelectedValue == "I")
                {
                    Entity_StockMaster.Type = "I";
                }
                if (RdoType.SelectedValue == "RQ")
                {
                    Entity_StockMaster.Type = "RQ";
                }


                Entity_StockMaster.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_StockMaster.LoginDate = DateTime.Now;
                Entity_StockMaster.Remark= TxtRemark.Text.Trim();
                InsertRow = Obj_StockMaster.InsertRecord(ref Entity_StockMaster, Convert.ToInt32(Session["TransactionSiteID"].ToString()), out StrError);
                if (InsertRow > 0)
                {
                    #region[InwardWise]
                    if (RdoType.SelectedValue == "IW")
                    {
                        if (ViewState["CurrentTable"] != null)
                        {
                            for (int i = 0; i < GridDetails1.Rows.Count; i++)
                            {
                                decimal Outwrd =(((TextBox)GridDetails1.Rows[i].FindControl("txtOutward")).Text).Equals("")? 0: Convert.ToDecimal(((TextBox)GridDetails1.Rows[i].FindControl("txtOutward")).Text);
                                if (Outwrd > 0)
                                {
                                    if (Outwrd != 0)
                                    {
                                        Entity_StockMaster.OutwardId = InsertRow;
                                        Entity_StockMaster.InwardId = Convert.ToInt32(((Label)GridDetails1.Rows[i].FindControl("LblEntryId")).Text);
                                        Entity_StockMaster.ItemId = Convert.ToInt32(GridDetails1.Rows[i].Cells[1].Text);
                                        Entity_StockMaster.ItemDetailsId = Convert.ToInt32(GridDetails1.Rows[i].Cells[3].Text);
                                        Entity_StockMaster.ItemDesc = Convert.ToString(GridDetails1.Rows[i].Cells[4].Text);
                                        Entity_StockMaster.InwardQty = !string.IsNullOrEmpty(GridDetails1.Rows[i].Cells[6].Text) ? Convert.ToDecimal(GridDetails1.Rows[i].Cells[6].Text) : 0;
                                        Entity_StockMaster.OutwardQty = Convert.ToDecimal(((TextBox)GridDetails1.Rows[i].FindControl("txtOutward")).Text);
                                        Entity_StockMaster.PendingQty = Convert.ToDecimal(((Label)GridDetails1.Rows[i].FindControl("LblPending")).Text);
                                        Entity_StockMaster.LocationId = Convert.ToInt32(((DropDownList)GridDetails1.Rows[i].FindControl("ddlLocation")).SelectedValue);
                                        Entity_StockMaster.StockLocationID = Convert.ToInt32(((DropDownList)GridDetails1.Rows[i].FindControl("ddlLocation")).SelectedValue);// Convert.ToInt32(Session["TransactionSiteID"].ToString());
                                        Entity_StockMaster.Rate=Convert.ToDecimal(GridDetails1.Rows[i].Cells[7].Text);
                                        Entity_StockMaster.Amount = Convert.ToDecimal(GridDetails1.Rows[i].Cells[10].Text);

                                        Entity_StockMaster.UnitConvDtlsId = 0;// Convert.ToInt32(((DropDownList)GridDetails1.Rows[i].FindControl("ddlUnitConversion")).SelectedValue);
                                       
                                        InsertRowDtls = Obj_StockMaster.InsertDetailsRecord(ref Entity_StockMaster, out StrError);
                                        if (Entity_StockMaster.PendingQty > 0)
                                        {
                                            FlagPending = true;
                                            goto j;
                                        }
                                        else
                                        {
                                            FlagPending = false;
                                        }
                                    }
                                    else
                                    {
                                        Entity_StockMaster.OutwardId = InsertRow;
                                        Entity_StockMaster.UserId = Convert.ToInt32(Session["UserID"]);
                                        Entity_StockMaster.LoginDate = DateTime.Now;
                                        //int iDelete = Obj_StockMaster.DeleteRecord(ref Entity_StockMaster, out StrError);
                                    }
                                }
                                string str;
                                j: str = string.Empty;
                            }
                            if (InsertRow > 0)
                            {
                                int UpdateFlagInward = Obj_StockMaster.UpdateAssignFlagInward(ref Entity_StockMaster, out StrError);
                                obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                                FillCombo();
                                MakeControlEmpty();
                                MakeEmptyForm();
                                Entity_StockMaster = null;
                                Obj_StockMaster = null;
                            }
                        }
                    }
                    #endregion
                    #region[CatgoryItemWise]
                    if (RdoType.SelectedValue == "I")
                    {
                        if (ViewState["CurrentTableCategoryItem"] != null)
                        {
                            for (int i = 0; i < GRDCATEGORYITEM.Rows.Count; i++)
                            {
                                decimal Outwrd =(((TextBox)GRDCATEGORYITEM.Rows[i].FindControl("txtOutward")).Text).Equals("")?0: Convert.ToDecimal(((TextBox)GRDCATEGORYITEM.Rows[i].FindControl("txtOutward")).Text);
                                if (Outwrd > 0)
                                {
                                    if (Outwrd != 0)
                                    {
                                        Entity_StockMaster.OutwardId = InsertRow;
                                        Entity_StockMaster.InwardId = Convert.ToInt32(((Label)GRDCATEGORYITEM.Rows[i].FindControl("LblEntryId")).Text);
                                        Entity_StockMaster.ItemId = Convert.ToInt32(GRDCATEGORYITEM.Rows[i].Cells[1].Text);

                                        Entity_StockMaster.ItemDetailsId = Convert.ToInt32(GRDCATEGORYITEM.Rows[i].Cells[3].Text);
                                        Entity_StockMaster.ItemDesc = Convert.ToString(GRDCATEGORYITEM.Rows[i].Cells[4].Text);

                                        Entity_StockMaster.InwardQty = !string.IsNullOrEmpty(GRDCATEGORYITEM.Rows[i].Cells[6].Text) ? Convert.ToDecimal(GRDCATEGORYITEM.Rows[i].Cells[6].Text) : 0;
                                        Entity_StockMaster.OutwardQty = Convert.ToDecimal(((TextBox)GRDCATEGORYITEM.Rows[i].FindControl("txtOutward")).Text);
                                        //Entity_StockMaster.PendingQty = Convert.ToDecimal(((Label)GRDCATEGORYITEM.Rows[i].FindControl("LblPending")).Text);
                                        Entity_StockMaster.PendingQty = 0;
                                        Entity_StockMaster.LocationId = Convert.ToInt32(((DropDownList)GRDCATEGORYITEM.Rows[i].FindControl("ddlLocation")).SelectedValue);
                                        Entity_StockMaster.StockLocationID = Convert.ToInt32(((DropDownList)GRDCATEGORYITEM.Rows[i].FindControl("ddlLocation")).SelectedValue);// Convert.ToInt32(Session["CafeteriaId"].ToString());
                                        Entity_StockMaster.Rate = Convert.ToDecimal(GRDCATEGORYITEM.Rows[i].Cells[7].Text);
                                        Entity_StockMaster.Amount = Convert.ToDecimal(GRDCATEGORYITEM.Rows[i].Cells[9].Text);
                                        Entity_StockMaster.UnitConvDtlsId = Convert.ToInt32(((DropDownList)GRDCATEGORYITEM.Rows[i].FindControl("ddlUnitConversion")).SelectedValue);
                                        InsertRowDtls = Obj_StockMaster.InsertDetailsRecord(ref Entity_StockMaster, out StrError);
                                      }
                                    }
                                    else
                                    {
                                        Entity_StockMaster.OutwardId = InsertRow;
                                        Entity_StockMaster.UserId = Convert.ToInt32(Session["UserID"]);
                                        Entity_StockMaster.LoginDate = DateTime.Now;
                                       // int iDelete = Obj_StockMaster.DeleteRecord(ref Entity_StockMaster, out StrError);
                                    }
                                }
                            }
                            if (InsertRow > 0)
                            {
                                obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                                FillCombo();
                                MakeControlEmpty();
                                MakeEmptyForm();
                                Entity_StockMaster = null;
                                Obj_StockMaster = null;
                            }
                        }
                    #endregion
                    #region[RequisitionWise]
                    if (RdoType.SelectedValue == "RQ")
                    {
                        int blankcount = 0;
                        int Rowscount = 0;
                        if (ViewState["CurrentTableRequisition"] != null)
                        {
                            for (int i = 0; i < GRDREQUISITION.Rows.Count; i++)
                            {
                                decimal Outwrd = (((TextBox)GRDREQUISITION.Rows[i].FindControl("txtOutward")).Text).Equals("") ? 0 : Convert.ToDecimal(((TextBox)GRDREQUISITION.Rows[i].FindControl("txtOutward")).Text);
                                if (Outwrd > 0)
                                {
                                    if (Outwrd != 0)
                                    {
                                        Entity_StockMaster.OutwardId = InsertRow;
                                        Entity_StockMaster.InwardId = Convert.ToInt32(((Label)GRDREQUISITION.Rows[i].FindControl("LblEntryId")).Text);
                                        Entity_StockMaster.ItemId = Convert.ToInt32(GRDREQUISITION.Rows[i].Cells[1].Text);


                                        Entity_StockMaster.ItemDetailsId = Convert.ToInt32(GRDREQUISITION.Rows[i].Cells[3].Text);
                                        Entity_StockMaster.ItemDesc = Convert.ToString(GRDREQUISITION.Rows[i].Cells[4].Text);


                                        Entity_StockMaster.InwardQty = !string.IsNullOrEmpty(GRDREQUISITION.Rows[i].Cells[7].Text) ? Convert.ToDecimal(GRDREQUISITION.Rows[i].Cells[7].Text) : 0;
                                        Entity_StockMaster.OutwardQty = Convert.ToDecimal(((TextBox)GRDREQUISITION.Rows[i].FindControl("txtOutward")).Text);
                                        //Entity_StockMaster.PendingQty = Convert.ToDecimal(((Label)GRDREQUISITION.Rows[i].FindControl("LblPending")).Text);
                                        Entity_StockMaster.PendingQty = 0;
                                        Entity_StockMaster.LocationId = Convert.ToInt32(((DropDownList)GRDREQUISITION.Rows[i].FindControl("ddlLocation")).SelectedValue);
                                        Entity_StockMaster.StockLocationID = Convert.ToInt32(((DropDownList)GRDREQUISITION.Rows[i].FindControl("ddlLocation")).SelectedValue);// Convert.ToInt32(Session["CafeteriaId"].ToString());
                                        Entity_StockMaster.Rate = Convert.ToDecimal(GRDREQUISITION.Rows[i].Cells[8].Text);
                                        Entity_StockMaster.Amount = Convert.ToDecimal(GRDREQUISITION.Rows[i].Cells[10].Text);
                                        Entity_StockMaster.UnitConvDtlsId = Convert.ToInt32(((DropDownList)GRDREQUISITION.Rows[i].FindControl("ddlUnitConversion")).SelectedValue);
                                        InsertRowDtls = Obj_StockMaster.InsertDetailsRecord(ref Entity_StockMaster, out StrError);
                                        Rowscount++;
                                        if (((Convert.ToDecimal(GRDREQUISITION.Rows[i].Cells[7].Text))-Outwrd) > 0)
                                        {
                                            FlagPending = true;
                                            goto j;
                                        }
                                        else
                                        {
                                            FlagPending = false;
                                        }
                                        //if (FlagPending == false)
                                        //{
                                        //    int updateFlag = Obj_StockMaster.UpdateAssignFlag(ref Entity_StockMaster, out StrError);
                                        //}
                                    }
                                    
                                }
                                else
                                {
                                    Entity_StockMaster.OutwardId = InsertRow;
                                    Entity_StockMaster.UserId = Convert.ToInt32(Session["UserID"]);
                                    Entity_StockMaster.LoginDate = DateTime.Now;
                                    blankcount++;
                                    if (blankcount == GRDREQUISITION.Rows.Count)
                                    {
                                        int iDelete = Obj_StockMaster.DeleteRecord(ref Entity_StockMaster, out StrError);
                                        InsertRow = 0;
                                    }
                                }
                                string str;
                                j: str = string.Empty;
                            }
                            if (InsertRow > 0)
                            {
                                if (FlagPending == false)
                                {
                                    if (Rowscount == GRDREQUISITION.Rows.Count)
                                    {
                                        string status = "Closed";
                                        int updateStatus = Obj_StockMaster.UpdateStatusFlag(status, ref Entity_StockMaster, out StrError);
                                        int updateFlag = Obj_StockMaster.UpdateAssignFlag(ref Entity_StockMaster, out StrError);
                                    }
                                    else
                                    {
                                        string status = "Opened";
                                        int updateStatus = Obj_StockMaster.UpdateStatusFlag(status, ref Entity_StockMaster, out StrError);
                                        int updateFlag = Obj_StockMaster.UpdateAssignFlag(ref Entity_StockMaster, out StrError);
                                    }
                                    
                                }
                                else
                                {
                                    string status = "Opened";
                                    int updateStatus = Obj_StockMaster.UpdateStatusFlag(status, ref Entity_StockMaster, out StrError);
                                    int updateFlag = Obj_StockMaster.UpdateAssignFlag(ref Entity_StockMaster, out StrError);
                                    //--------**Forcefully Closed Requisition**----------
                                    if (chk_close.Checked)
                                    {
                                        string status1 = "Closed";
                                        int updateStatus1 = Obj_StockMaster.UpdateStatusFlag(status1, ref Entity_StockMaster, out StrError);
                                    }
                                }
                                obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                                FillCombo();
                                MakeControlEmpty();
                                MakeEmptyForm();
                                Entity_StockMaster = null;
                                Obj_StockMaster = null;
                            }
                            else
                            {
                                obj_Comman.ShowPopUpMsg("No Quantity For Issue", this.Page);
                            }
                        }
                    }
                    #endregion
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("No Record Found In Grid", this.Page);
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
                Entity_StockMaster.OutwardId = Convert.ToInt32(ViewState["ID"]);
            }
            Entity_StockMaster.StockNo = TxtStockNo.Text;
            Entity_StockMaster.StockAsOn =!string.IsNullOrEmpty(TxtStockDate.Text)? Convert.ToDateTime(TxtStockDate.Text):DateTime.Now;
            Entity_StockMaster.ConsumptionDate = !string.IsNullOrEmpty(TxtConsumptionDate.Text) ? Convert.ToDateTime(TxtConsumptionDate.Text) : DateTime.Now;
            Entity_StockMaster.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_StockMaster.LoginDate = DateTime.Now;
            Entity_StockMaster.Remark = TxtRemark.Text.Trim();
            UpdateRow = Obj_StockMaster.UpdateRecord(ref Entity_StockMaster, out StrError);
            if (UpdateRow > 0)
            {
                #region[OldUpdateCode]
                //if (ViewState["CurrentTable"] != null)
                //{
                //    DataTable dtInsert = new DataTable();
                //    dtInsert = (DataTable)ViewState["CurrentTable"];
                //    for (int i = 0; i < GridDetails1.Rows.Count; i++)
                //    {
                //        decimal Outwrd = Convert.ToDecimal(((TextBox)GridDetails1.Rows[i].FindControl("txtOutward")).Text);
                //        if (Outwrd > 0)
                //        {
                //            //----For InwardId----
                //            if (RdoType.SelectedValue == "IW")
                //            {
                //                Entity_StockMaster.InwardId = Convert.ToInt32(((Label)GridDetails1.Rows[i].FindControl("LblEntryId")).Text);
                //            }
                //            if (RdoType.SelectedValue == "RQ")
                //            {
                //                Entity_StockMaster.InwardId = Convert.ToInt32(((Label)GridDetails1.Rows[i].FindControl("LblEntryId")).Text);
                //            }
                //            if (RdoType.SelectedValue == "I")
                //            {
                //                Entity_StockMaster.InwardId = 0;
                //            }

                //            Entity_StockMaster.ItemId = Convert.ToInt32(GridDetails1.Rows[i].Cells[3].Text);

                //            //---For InwardQty---
                //            if (RdoType.SelectedValue == "I" || RdoType.SelectedValue == "C")
                //            {
                //                Entity_StockMaster.InwardQty = 0;
                //            }
                //            else
                //            {
                //                Entity_StockMaster.InwardQty = Convert.ToDecimal(GridDetails1.Rows[i].Cells[5].Text);
                //            }

                //            // Entity_StockMaster.InwardQty = Convert.ToDecimal(GridDetails1.Rows[i].Cells[4].Text);

                //            //----Check if AvailableQty < OutwordQty
                //            decimal QutWrdQty = 0;
                //            decimal avl = Convert.ToDecimal(GridDetails1.Rows[i].Cells[4].Text.ToString());
                //            if (Outwrd <= avl)
                //            {
                //                Entity_StockMaster.OutwardQty = Convert.ToDecimal(((TextBox)GridDetails1.Rows[i].FindControl("txtOutward")).Text);
                //                QutWrdQty = Entity_StockMaster.OutwardQty;
                //            }
                //            else
                //            {
                //                Entity_StockMaster.OutwardQty = Convert.ToDecimal(GridDetails1.Rows[i].Cells[5].Text.ToString());
                //                QutWrdQty = Entity_StockMaster.OutwardQty;
                //            }

                //            //Calculate Pending
                //            if (RdoType.SelectedValue == "RQ")
                //            {
                //                Entity_StockMaster.PendingQty = (Convert.ToDecimal(GridDetails1.Rows[i].Cells[5].Text)) - (Convert.ToDecimal(QutWrdQty));
                //            }
                //            else
                //            {
                //                Entity_StockMaster.PendingQty = Convert.ToDecimal(((Label)GridDetails1.Rows[i].FindControl("LblPending")).Text);
                //            }

                //            Entity_StockMaster.LocationId = Convert.ToInt32(((DropDownList)GridDetails1.Rows[i].FindControl("ddlLocation")).SelectedValue);

                //            //----Stock Details Entry-----
                //            Entity_StockMaster.StockLocationID = Convert.ToInt32(Session["CafeteriaId"].ToString());
                //            UpdateRowDtls = Obj_StockMaster.InsertDetailsRecord(ref Entity_StockMaster, out StrError);

                //            //Assign Status Flag for Requisition
                //            if (Entity_StockMaster.PendingQty > 0)
                //            {
                //                FlagPending = true;
                //                goto j;
                //            }
                //            else
                //            {
                //                FlagPending = false;
                //            }
                //        }
                //       string str;
                //       j: str = string.Empty;
                //    }
                //    if (UpdateRow > 0)
                //    {
                //        //Update Assign Status Flag In 
                //        if (RdoType.SelectedValue == "RQ")
                //        {
                //            int updateFlag = Obj_StockMaster.UpdateAssignFlag(ref Entity_StockMaster, out StrError);
                //            if (FlagPending == true)
                //            {
                //                string status = "Closed";
                //                int updateStatus = Obj_StockMaster.UpdateStatusFlag(status, ref Entity_StockMaster, out StrError);
                //            }
                //            else
                //            {
                //                string status = "Opened";
                //                int updateStatus = Obj_StockMaster.UpdateStatusFlag(status, ref Entity_StockMaster, out StrError);
                               
                //                //--------**Forcefully Closed Requisition**----------
                //                if (chk_close.Checked)
                //                {
                //                    string status1 = "Closed";
                //                    int updateStatus1 = Obj_StockMaster.UpdateStatusFlag(status1, ref Entity_StockMaster, out StrError);
                //                }
                //            }
                //        }
                //        if (RdoType.SelectedValue == "IW")
                //        {
                //            int UpdateFlagInward = Obj_StockMaster.UpdateAssignFlagInward(ref Entity_StockMaster, out StrError);
                //        }

                //        obj_Comman.ShowPopUpMsg("Record Updated Successfully", this.Page);
                //        FillCombo();
                //        MakeControlEmpty();
                //        MakeEmptyForm();
                //        Entity_StockMaster = null;
                //        Obj_StockMaster = null;
                //    }
                //}
#endregion
                #region[InwardWise]
                if (RdoType.SelectedValue == "IW")
                {
                    if (ViewState["CurrentTable"] != null)
                    {
                        for (int i = 0; i < GridDetails1.Rows.Count; i++)
                        {
                            decimal Outwrd = (((TextBox)GridDetails1.Rows[i].FindControl("txtOutward")).Text).Equals("") ? 0 : Convert.ToDecimal(((TextBox)GridDetails1.Rows[i].FindControl("txtOutward")).Text);
                            if (Outwrd > 0)
                            {
                                if (Outwrd != 0)
                                {

                                   


                                    Entity_StockMaster.InwardId = Convert.ToInt32(((Label)GridDetails1.Rows[i].FindControl("LblEntryId")).Text);
                                    Entity_StockMaster.ItemId = Convert.ToInt32(GridDetails1.Rows[i].Cells[1].Text);
                                    Entity_StockMaster.ItemDetailsId = Convert.ToInt32(GridDetails1.Rows[i].Cells[3].Text);
                                    Entity_StockMaster.ItemDesc = Convert.ToString(GridDetails1.Rows[i].Cells[4].Text);
                                    Entity_StockMaster.InwardQty = !string.IsNullOrEmpty(GridDetails1.Rows[i].Cells[6].Text) ? Convert.ToDecimal(GridDetails1.Rows[i].Cells[6].Text) : 0;
                                    Entity_StockMaster.OutwardQty = Convert.ToDecimal(((TextBox)GridDetails1.Rows[i].FindControl("txtOutward")).Text);
                                    Entity_StockMaster.PendingQty = Convert.ToDecimal(((Label)GridDetails1.Rows[i].FindControl("LblPending")).Text);
                                    Entity_StockMaster.LocationId = Convert.ToInt32(((DropDownList)GridDetails1.Rows[i].FindControl("ddlLocation")).SelectedValue);
                                    Entity_StockMaster.StockLocationID = Convert.ToInt32(((DropDownList)GridDetails1.Rows[i].FindControl("ddlLocation")).SelectedValue);// Convert.ToInt32(Session["CafeteriaId"].ToString());
                                    Entity_StockMaster.Rate = Convert.ToDecimal(GridDetails1.Rows[i].Cells[7].Text);
                                    Entity_StockMaster.Amount = Convert.ToDecimal(GridDetails1.Rows[i].Cells[10].Text);
                                    Entity_StockMaster.UnitConvDtlsId = 0; //Convert.ToInt32(((DropDownList)GridDetails1.Rows[i].FindControl("ddlUnitConversion")).SelectedValue);
                                    UpdateRowDtls = Obj_StockMaster.InsertDetailsRecord(ref Entity_StockMaster, out StrError);
                                    if (Entity_StockMaster.PendingQty > 0)
                                    {
                                        FlagPending = true;
                                        goto j;
                                    }
                                    else
                                    {
                                        FlagPending = false;
                                    }
                                }
                                else
                                {
                                    Entity_StockMaster.OutwardId = UpdateRow;
                                    Entity_StockMaster.UserId = Convert.ToInt32(Session["UserID"]);
                                    Entity_StockMaster.LoginDate = DateTime.Now;
                                    int iDelete = Obj_StockMaster.DeleteRecord(ref Entity_StockMaster, out StrError);
                                }
                            }
                            string str;
                        j: str = string.Empty;
                        }
                        if (UpdateRow > 0)
                        {
                            int UpdateFlagInward = Obj_StockMaster.UpdateAssignFlagInward(ref Entity_StockMaster, out StrError);
                            obj_Comman.ShowPopUpMsg("Record Updated Successfully", this.Page);
                            FillCombo();
                            MakeControlEmpty();
                            MakeEmptyForm();
                            Entity_StockMaster = null;
                            Obj_StockMaster = null;
                        }
                    }
                }
                #endregion
                #region[CatgoryItemWise]
                if (RdoType.SelectedValue == "I")
                {
                    if (ViewState["CurrentTableCategoryItem"] != null)
                    {
                        for (int i = 0; i < GRDCATEGORYITEM.Rows.Count; i++)
                        {
                            decimal Outwrd = (((TextBox)GRDCATEGORYITEM.Rows[i].FindControl("txtOutward")).Text).Equals("") ? 0 : Convert.ToDecimal(((TextBox)GRDCATEGORYITEM.Rows[i].FindControl("txtOutward")).Text);
                            if (Outwrd > 0)
                            {
                                if (Outwrd != 0)
                                {

                                   


                                    Entity_StockMaster.InwardId = Convert.ToInt32(((Label)GRDCATEGORYITEM.Rows[i].FindControl("LblEntryId")).Text);
                                    Entity_StockMaster.ItemId = Convert.ToInt32(GRDCATEGORYITEM.Rows[i].Cells[1].Text);

                                    Entity_StockMaster.ItemDetailsId = Convert.ToInt32(GRDCATEGORYITEM.Rows[i].Cells[3].Text);
                                    Entity_StockMaster.ItemDesc = Convert.ToString(GRDCATEGORYITEM.Rows[i].Cells[4].Text);


                                    Entity_StockMaster.InwardQty = !string.IsNullOrEmpty(GRDCATEGORYITEM.Rows[i].Cells[6].Text) ? Convert.ToDecimal(GRDCATEGORYITEM.Rows[i].Cells[6].Text) : 0;
                                    Entity_StockMaster.OutwardQty = Convert.ToDecimal(((TextBox)GRDCATEGORYITEM.Rows[i].FindControl("txtOutward")).Text);
                                    //Entity_StockMaster.PendingQty = Convert.ToDecimal(((Label)GRDCATEGORYITEM.Rows[i].FindControl("LblPending")).Text);
                                    Entity_StockMaster.PendingQty = 0;
                                    Entity_StockMaster.LocationId = Convert.ToInt32(((DropDownList)GRDCATEGORYITEM.Rows[i].FindControl("ddlLocation")).SelectedValue);
                                    Entity_StockMaster.StockLocationID = Convert.ToInt32(((DropDownList)GRDCATEGORYITEM.Rows[i].FindControl("ddlLocation")).SelectedValue);// Convert.ToInt32(Session["CafeteriaId"].ToString());
                                    Entity_StockMaster.Rate = Convert.ToDecimal(GRDCATEGORYITEM.Rows[i].Cells[7].Text);
                                    Entity_StockMaster.Amount = Convert.ToDecimal(GRDCATEGORYITEM.Rows[i].Cells[9].Text);
                                    Entity_StockMaster.UnitConvDtlsId = Convert.ToInt32(((DropDownList)GRDCATEGORYITEM.Rows[i].FindControl("ddlUnitConversion")).SelectedValue);
                                    UpdateRowDtls = Obj_StockMaster.InsertDetailsRecord(ref Entity_StockMaster, out StrError);
                                }
                            }
                            else
                            {
                                Entity_StockMaster.OutwardId = UpdateRow;
                                Entity_StockMaster.UserId = Convert.ToInt32(Session["UserID"]);
                                Entity_StockMaster.LoginDate = DateTime.Now;
                                int iDelete = Obj_StockMaster.DeleteRecord(ref Entity_StockMaster, out StrError);
                            }
                        }
                    }
                    if (UpdateRow > 0)
                    {
                        obj_Comman.ShowPopUpMsg("Record Updated Successfully", this.Page);
                        FillCombo();
                        MakeControlEmpty();
                        MakeEmptyForm();
                        Entity_StockMaster = null;
                        Obj_StockMaster = null;
                    }
                }
                #endregion
                #region[RequisitionWise]
                if (RdoType.SelectedValue == "RQ")
                {
                    int blankcount = 0;
                    if (ViewState["CurrentTableRequisition"] != null)
                    {
                        for (int i = 0; i < GRDREQUISITION.Rows.Count; i++)
                        {
                            decimal Outwrd = (((TextBox)GRDREQUISITION.Rows[i].FindControl("txtOutward")).Text).Equals("") ? 0 : Convert.ToDecimal(((TextBox)GRDREQUISITION.Rows[i].FindControl("txtOutward")).Text);
                            if (Outwrd > 0)
                            {
                                if (Outwrd != 0)
                                {

                                    Entity_StockMaster.InwardId = Convert.ToInt32(((Label)GRDREQUISITION.Rows[i].FindControl("LblEntryId")).Text);
                                    Entity_StockMaster.ItemId = Convert.ToInt32(GRDREQUISITION.Rows[i].Cells[1].Text);

                                    Entity_StockMaster.ItemDetailsId = Convert.ToInt32(GRDREQUISITION.Rows[i].Cells[3].Text);
                                    Entity_StockMaster.ItemDesc = Convert.ToString(GRDREQUISITION.Rows[i].Cells[4].Text);


                                    Entity_StockMaster.InwardQty = !string.IsNullOrEmpty(GRDREQUISITION.Rows[i].Cells[7].Text) ? Convert.ToDecimal(GRDREQUISITION.Rows[i].Cells[7].Text) : 0;
                                    Entity_StockMaster.OutwardQty = Convert.ToDecimal(((TextBox)GRDREQUISITION.Rows[i].FindControl("txtOutward")).Text);
                                    //Entity_StockMaster.PendingQty = Convert.ToDecimal(((Label)GRDREQUISITION.Rows[i].FindControl("LblPending")).Text);
                                    Entity_StockMaster.PendingQty = 0;
                                    Entity_StockMaster.LocationId = Convert.ToInt32(((DropDownList)GRDREQUISITION.Rows[i].FindControl("ddlLocation")).SelectedValue);
                                    Entity_StockMaster.StockLocationID = Convert.ToInt32(((DropDownList)GRDREQUISITION.Rows[i].FindControl("ddlLocation")).SelectedValue);// Convert.ToInt32(Session["CafeteriaId"].ToString());
                                    Entity_StockMaster.Rate = Convert.ToDecimal(GRDREQUISITION.Rows[i].Cells[8].Text);
                                    Entity_StockMaster.Amount = Convert.ToDecimal(GRDREQUISITION.Rows[i].Cells[10].Text);
                                    Entity_StockMaster.UnitConvDtlsId = Convert.ToInt32(((DropDownList)GRDREQUISITION.Rows[i].FindControl("ddlUnitConversion")).SelectedValue);
                                    UpdateRowDtls = Obj_StockMaster.InsertDetailsRecord(ref Entity_StockMaster, out StrError);
                                    
                                    if (((Convert.ToDecimal(GRDREQUISITION.Rows[i].Cells[7].Text))-Outwrd) > 0)
                                    {
                                        FlagPending = true;
                                        goto j;
                                    }
                                    else
                                    {
                                        FlagPending = false;
                                    }
                                    //if (FlagPending == false)
                                    //{
                                    //    int updateFlag = Obj_StockMaster.UpdateAssignFlag(ref Entity_StockMaster, out StrError);
                                    //}
                                }
                            }
                             else
                            {
                              Entity_StockMaster.OutwardId = UpdateRow;
                              Entity_StockMaster.UserId = Convert.ToInt32(Session["UserID"]);
                              Entity_StockMaster.LoginDate = DateTime.Now;
                              blankcount++;
                              if (blankcount == GRDREQUISITION.Rows.Count )
                              {
                                  int iDelete = Obj_StockMaster.DeleteRecord(ref Entity_StockMaster, out StrError);
                                  UpdateRow = 0;
                              }
                              
                            }
                            string str;
                            j: str = string.Empty;
                        }
                    
                        if (UpdateRow > 0)
                        {
                            if (FlagPending == false)
                            {
                                string status = "Closed";
                                int updateStatus = Obj_StockMaster.UpdateStatusFlag(status, ref Entity_StockMaster, out StrError);
                                int updateFlag = Obj_StockMaster.UpdateAssignFlag(ref Entity_StockMaster, out StrError);
                            }
                            else
                            {
                                string status = "Opened";
                                int updateStatus = Obj_StockMaster.UpdateStatusFlag(status, ref Entity_StockMaster, out StrError);
                                int updateFlag = Obj_StockMaster.UpdateAssignFlag(ref Entity_StockMaster, out StrError);
                                //--------**Forcefully Closed Requisition**----------
                                if (chk_close.Checked)
                                {
                                    string status1 = "Closed";
                                    int updateStatus1 = Obj_StockMaster.UpdateStatusFlag(status1, ref Entity_StockMaster, out StrError);
                                }
                                    obj_Comman.ShowPopUpMsg("Record Updated Successfully", this.Page);
                                    FillCombo();
                                    MakeControlEmpty();
                                    MakeEmptyForm();
                                    Entity_StockMaster = null;
                                    Obj_StockMaster = null;
                                
                            }
                        }
                        else
                        {
                            obj_Comman.ShowPopUpMsg("No Quantity For Issue", this.Page);
                        }
                    }
                }
                #endregion
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        int DeleteId = 0;
        try
        {
            if(ViewState["ID"]!=null)
            {
               DeleteId = Convert.ToInt32(ViewState["ID"]);
            }
            if(DeleteId!=0)
            {
                Entity_StockMaster.OutwardId=DeleteId;
                Entity_StockMaster.UserId = Convert.ToInt32(ViewState["SessionId"]);
                Entity_StockMaster.LoginDate = DateTime.Now;
                int iDelete = Obj_StockMaster.DeleteRecord(ref Entity_StockMaster, out StrError);
                if(iDelete>0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully",this.Page);
                }
                Entity_StockMaster=null;
                Obj_StockMaster=null;

            }
        }
        catch(Exception ex)
        {
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
        MakeControlEmpty();
    }

    protected void ReportGrid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Rdb=string.Empty;
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        if (Convert.ToInt32(e.CommandArgument) != 0)
                        {
                            ViewState["ID"] = Convert.ToInt32(e.CommandArgument);
                            Ds = Obj_StockMaster.GetRecordForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                            {
                                TxtStockNo.Text = Ds.Tables[0].Rows[0]["StockNo"].ToString();
                                TxtStockDate.Text = Ds.Tables[0].Rows[0]["StockAsOn"].ToString();
                                TxtConsumptionDate.Text = Ds.Tables[0].Rows[0]["ConsumptionDate"].ToString();
                                TxtRemark.Text = Ds.Tables[0].Rows[0]["Remark"].ToString();
                                Rdb=Ds.Tables[0].Rows[0]["Type"].ToString();
                                if(Rdb=="IW ")
                                {
                                    RdoType.SelectedIndex = 0;
                                    LblNo.Text = "Inward No: "+Ds.Tables[1].Rows[0]["InwardNo"].ToString();
                                    RdoType_SelectedIndexChanged(sender, e);
                                    TRINW.Visible = false;
                                    RdoType.Enabled = false;
                                }
                                if (Rdb == "I  ")
                                {
                                    RdoType.SelectedIndex = 1;
                                    LblNo.Text = "";
                                    RdoType_SelectedIndexChanged(sender, e);
                                }
                                if (Rdb == "RQ ")
                                {
                                    RdoType.SelectedIndex =2;
                                    RdoType_SelectedIndexChanged(sender, e);
                                    ddlRequisitnNo.Visible = false;
                                    lblRequisitionNo.Visible = true;
                                    LblNo.Text = "Requisition No: "+Ds.Tables[1].Rows[0]["RequisitionNo"].ToString();
                                    lblRequisitionNo.Text = Ds.Tables[1].Rows[0]["RequisitionNo"].ToString();
                                    TrReq.Visible = false;
                                    RdoType.Enabled = false;
                                }
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            if (Ds.Tables[1].Rows.Count > 0)
                            {
                                if (Rdb == "IW ")
                                {
                                    GridDetails1.DataSource = Ds.Tables[1];
                                    GridDetails1.DataBind();
                                }
                               
                                if (Rdb == "I  ")
                                {
                                    GRDCATEGORYITEM.DataSource = Ds.Tables[1];
                                    GRDCATEGORYITEM.DataBind();
                                    GRDCATEGORYITEM.Columns[9].Visible = false;
                                    //GRDCATEGORYITEM.Columns[5].Visible = false;
                                }
                                if (Rdb == "RQ ")
                                {
                                    issusecond = 2;
                                    GRDREQUISITION.DataSource = Ds.Tables[1];
                                    GRDREQUISITION.DataBind();
                                    GRDREQUISITION.Columns[8].Visible = false;
                                    GRDREQUISITION.Columns[5].HeaderText = "Requisition Qty";
                                    //CkeckAvlQtyForRequisition();
                                }
                                ViewState["CurrentTable"] = Ds.Tables[1];
                                ViewState["CurrentTableCategoryItem"] = Ds.Tables[1];
                                ViewState["CurrentTableRequisition"] = Ds.Tables[1];
                            }
                            Ds = null;
                            Obj_StockMaster = null;
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                        }
                        break;
                    }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void ReportGrid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int DeleteId = 0;
        try
        {
            DeleteId = Convert.ToInt32(((ImageButton)ReportGrid1.Rows[e.RowIndex].Cells[0].FindControl("ImgBtnDelete")).CommandArgument.ToString());
            if (DeleteId != 0)
            {
                Entity_StockMaster.OutwardId = DeleteId;
                Entity_StockMaster.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_StockMaster.LoginDate = DateTime.Now;

                int iDelete = Obj_StockMaster.DeleteRecord(ref Entity_StockMaster, out StrError);
                if (iDelete != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }
            
            Entity_StockMaster = null;
            Obj_StockMaster = null;
            //FillCombo();

          
        }
        catch (Exception ex)
        {
        }
    }

    protected void txtOutward_TextChanged(object sender, EventArgs e)
    {
        #region [Comment For Compare validator]-------------------
        TextBox txt = (TextBox)sender;
        Decimal OutWard = !string.IsNullOrEmpty(txt.Text) ? Convert.ToDecimal(txt.Text) : 0;
        GridViewRow grd = (GridViewRow)txt.Parent.Parent;
        Decimal OUTERQTY = !string.IsNullOrEmpty(((TextBox)grd.FindControl("txtUnitWiseOutwardQty")).Text) ? Convert.ToDecimal(((TextBox)grd.FindControl("txtUnitWiseOutwardQty")).Text) : 0;
        if ((OUTERQTY>= OutWard))
        {
            ((Label)grd.FindControl("LblPending")).Text = (Convert.ToDecimal(OUTERQTY) - OutWard).ToString("#0.00");//8 for amount
            grd.Cells[10].Text = (Convert.ToDecimal(grd.Cells[7].Text) * OutWard).ToString("#0.00");
            ((DropDownList)grd.FindControl("ddlLocation")).Focus();
        }
        else
        {
            obj_Comman.ShowPopUpMsg("You have only  " + (Convert.ToDecimal(OUTERQTY)) + " Items to Issuse", this.Page);
            txt.Focus();
        }

        
        #endregion [End Here]------------------------------------
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        BindReportGrid(StrCondition);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMStockMaster Obj_StockMaster = new DMStockMaster();
        String[] SearchList = Obj_StockMaster.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet DsGrid = new DataSet();
            StrCondition = string.Empty;
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
           
            if (Convert.ToInt32(DDLINV.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and IR.InwardId = " + Convert.ToInt32(DDLINV.SelectedValue);
            }         
            ReportGrid(StrCondition, DDLINV.SelectedValue,"IW");
            ((TextBox)GridDetails1.Rows[0].FindControl("txtOutward")).Focus();
        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnShowCateGory_Click(object sender, EventArgs e)
    {
        try
         {
            DataSet DsGrid = new DataSet();
            StrCondition = string.Empty;
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;  
            ReportGrid(StrCondition, DDLCATEGORY.SelectedValue,"C");
            ((TextBox)GRDCATEGORYITEM.Rows[0].FindControl("txtOutward")).Focus();
        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnShowItems_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet DsGrid = new DataSet();
            StrCondition = string.Empty;
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            ReportGrid(StrCondition, DDLITEMS.SelectedValue, "I");
            ((TextBox)GRDCATEGORYITEM.Rows[0].FindControl("txtOutward")).Focus();
        }
        catch (Exception ex)
        {

        }
    }

    protected void RdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RdoType.SelectedValue == "IW")
        {
            TRINW.Visible = true;
            TRCATEGORY.Visible = false;
            TRDiscription.Visible = false;
            lblclose.Visible = false;
            chk_close.Visible = false;
            TrReq.Visible = false;
            MakeControlEmpty();
            SetInitialRow();
            TxtFromDate.Focus();
            TRGRIDINWARD.Visible = true;
            TRGRIDCATEGORYITEM.Visible = TRGRIDREQUISITION.Visible = false;
            //RdoType.Enabled = false;
            
        }
        if (RdoType.SelectedValue == "I")
        {
            TRINW.Visible = false;
            TRCATEGORY.Visible = true;
            TRDiscription.Visible = true;
            lblclose.Visible = false;
            chk_close.Visible = false;
            TrReq.Visible = false;
            MakeControlEmpty();
            SetInitialRow();
            TRGRIDCATEGORYITEM.Visible = true;
            TRGRIDINWARD.Visible = TRGRIDREQUISITION.Visible = false;
            DDLCATEGORY.Focus();
           // RdoType.Enabled = false;

        }
        if (RdoType.SelectedValue == "RQ")
        {
            TrReq.Visible = true;
            TRINW.Visible = false;
            lblclose.Visible = true;
            chk_close.Visible = true;
            TRCATEGORY.Visible = false;
            TRDiscription.Visible = false;
            MakeControlEmpty();
            TxtFromDate.Focus();
            SetInitialRow();
            TRGRIDREQUISITION.Visible = true;
            TRGRIDCATEGORYITEM.Visible = TRGRIDINWARD.Visible = false;
            //RdoType.Enabled = false;
        }
    }

    protected void BtnShowRequisitn_Click1(object sender, EventArgs e)
    {
        try
        {
            DataSet DsGrid = new DataSet();
            StrCondition = string.Empty;
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            //if (!string.IsNullOrEmpty(TxtFromDateR.Text))
            //{
            //    StrCondition = StrCondition + " and RC.RequisitionDate between '" + Convert.ToDateTime(TxtFromDateR.Text);
            //}
            //else
            //{
            //    StrCondition = StrCondition + " and RC.RequisitionDate between '" + DateTime.Now.AddMonths(-1);
            //}
            //if (!string.IsNullOrEmpty(TxtToDateR.Text))
            //{
            //    StrCondition = StrCondition + "' and '" + Convert.ToDateTime(TxtToDateR.Text) + "'";
            //}
            //else
            //{
            //    StrCondition = StrCondition + "' and '" + DateTime.Now + "'";
            //}
            if (Convert.ToInt32(ddlRequisitnNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and RC.RequisitionCafeId = " + Convert.ToInt32(ddlRequisitnNo.SelectedValue);
            }
            ReportGrid(StrCondition, ddlRequisitnNo.SelectedValue, "RQ");
            ((TextBox)GRDREQUISITION.Rows[0].FindControl("txtOutward")).Focus();
        }
        catch (Exception ex)
        {

        }
    }

    protected void ReportGrid1_DataBound(object sender, EventArgs e)
    {
       
    }

    protected void ReportGrid1_RowDataBound(object sender, GridViewRowEventArgs e)   
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ImageGridEdit = (ImageButton)e.Row.Cells[0].FindControl("ImageGridEdit");
                ImageButton ImgBtnDelete = (ImageButton)e.Row.Cells[0].FindControl("ImgBtnDelete");
                string Status = e.Row.Cells[4].Text;
                if (Status == "Closed")
                {
                    ImageGridEdit.Visible = false;
                    ImgBtnDelete.Visible = false;
                    for (int a = 0; a < e.Row.Cells.Count; a++)
                    {
                        e.Row.Cells[a].ForeColor = System.Drawing.Color.FromName("Red");
                    }
                }
           }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    protected void ReportGrid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.ReportGrid1.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            string COND = string.Empty;
            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
                COND = COND + " AND S.Location=" + Convert.ToInt32(Session["TransactionSiteID"].ToString());
            //}
            Ds = Obj_StockMaster.GetStock(StrCondition,COND, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ReportGrid1.DataSource = Ds.Tables[0];
                this.ReportGrid1.DataBind();
                if (FlagDel && FlagEdit)
                {
                    foreach (GridViewRow GRow in ReportGrid1.Rows)
                    {
                        GRow.FindControl("ImgBtnDelete").Visible = false;
                        GRow.FindControl("ImageGridEdit").Visible = false;
                    }
                }
                else if (FlagDel)
                {
                    foreach (GridViewRow GRow in ReportGrid1.Rows)
                    {
                        GRow.FindControl("ImgBtnDelete").Visible = false;
                    }
                }
                else
                {
                    foreach (GridViewRow GRow in ReportGrid1.Rows)
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

    protected void GRDCATEGORYITEM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((DropDownList)e.Row.FindControl("ddlLocation")).DataSource = dtLoc;
            ((DropDownList)e.Row.FindControl("ddlLocation")).DataTextField = "Location";
            ((DropDownList)e.Row.FindControl("ddlLocation")).DataValueField = "StockLocationID";
            ((DropDownList)e.Row.FindControl("ddlLocation")).DataBind();
            ((DropDownList)e.Row.FindControl("ddlLocation")).SelectedValue = Session["TransactionSiteID"].ToString();
            int cafeId = Convert.ToInt32(Session["TransactionSiteID"].ToString());
            DataSet dsAvlqty = new DataSet();
            dsAvlqty = Obj_StockMaster.getAvlQtyItemWise(Convert.ToInt32(e.Row.Cells[1].Text), cafeId, 0, 0, e.Row.Cells[4].Text.Equals("&nbsp;") ? "" : e.Row.Cells[4].Text, out StrError);
            if (dsAvlqty.Tables.Count > 0 && dsAvlqty.Tables[0].Rows.Count > 0)
            {
                ((TextBox)e.Row.FindControl("txtAvlQty")).Text = e.Row.Cells[6].Text = dsAvlqty.Tables[0].Rows[0]["Closing"].ToString();
            }
            ((DropDownList)e.Row.FindControl("ddlUnitConversion")).DataSource = dsAvlqty.Tables[2];
            ((DropDownList)e.Row.FindControl("ddlUnitConversion")).DataTextField = "UnitFactor";
            ((DropDownList)e.Row.FindControl("ddlUnitConversion")).DataValueField = "UnitConvDtlsId";
            ((DropDownList)e.Row.FindControl("ddlUnitConversion")).DataBind();
            ((DropDownList)e.Row.FindControl("ddlUnitConversion")).SelectedValue = ((Label)e.Row.FindControl("lblUnitId")).Text;
        }
    }

    protected void GRDREQUISITION_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((DropDownList)e.Row.FindControl("ddlLocation")).DataSource = dtLoc;
            ((DropDownList)e.Row.FindControl("ddlLocation")).DataTextField = "Location";
            ((DropDownList)e.Row.FindControl("ddlLocation")).DataValueField = "StockLocationID";
            ((DropDownList)e.Row.FindControl("ddlLocation")).DataBind();
            ((DropDownList)e.Row.FindControl("ddlLocation")).SelectedValue = Session["TransactionSiteID"].ToString();
            int cafeId = Convert.ToInt32(Session["TransactionSiteID"].ToString());
            DataSet dsAvlqty = new DataSet();
            dsAvlqty = Obj_StockMaster.getAvlQty(Convert.ToInt32(e.Row.Cells[1].Text), cafeId,!string.IsNullOrEmpty(((Label)e.Row.FindControl("lblUnitId")).Text)?Convert.ToInt32(((Label)e.Row.FindControl("lblUnitId")).Text):0, Convert.ToDecimal(e.Row.Cells[7].Text),Convert.ToString(e.Row.Cells[4].Text), out StrError);
            if (dsAvlqty.Tables.Count > 0 && dsAvlqty.Tables[0].Rows.Count > 0)
            {
                //((TextBox)e.Row.FindControl("txtAvlQty")).Text =e.Row.Cells[4].Text = dsAvlqty.Tables[0].Rows[0]["Closing"].ToString();
                ((TextBox)e.Row.FindControl("txtAvlQty")).Text = e.Row.Cells[6].Text = (Convert.ToDecimal((((TextBox)e.Row.FindControl("txtOutward")).Text).Equals("")?0:Convert.ToDecimal(((TextBox)e.Row.FindControl("txtOutward")).Text)) + Convert.ToDecimal(dsAvlqty.Tables[0].Rows[0]["Closing"].ToString())).ToString();
            }

            ((DropDownList)e.Row.FindControl("ddlUnitConversion")).DataSource = dsAvlqty.Tables[2]; 
            ((DropDownList)e.Row.FindControl("ddlUnitConversion")).DataTextField = "UnitFactor";
            ((DropDownList)e.Row.FindControl("ddlUnitConversion")).DataValueField = "UnitConvDtlsId";
            ((DropDownList)e.Row.FindControl("ddlUnitConversion")).DataBind();
            ((DropDownList)e.Row.FindControl("ddlUnitConversion")).SelectedValue = ((Label)e.Row.FindControl("lblUnitId")).Text;

        }
    } 

    DataTable GetDataTable(GridView dtg)
    {
        try
        {
            int k = 0;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["CurrentTableCategoryItem"];
            while (dt.Rows.Count > 0)
            {
                dt.Rows.RemoveAt(0);
            }


            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                if (Convert.ToInt32(GRDCATEGORYITEM.Rows[k].Cells[1].Text) > 0)
                {
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["#"] = (((Label)GRDCATEGORYITEM.Rows[k].FindControl("LblEntryId")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((Label)GRDCATEGORYITEM.Rows[k].FindControl("LblEntryId")).Text);
                        dr["ItemId"] = Convert.ToInt32(GRDCATEGORYITEM.Rows[k].Cells[1].Text);
                        dr["Item"] = (GRDCATEGORYITEM.Rows[k].Cells[2].Text.Equals("&nbsp;")) ? "" : Convert.ToString(GRDCATEGORYITEM.Rows[k].Cells[2].Text).Replace("amp;", "");

                        dr["Unit"] = Convert.ToString(GRDCATEGORYITEM.Rows[k].Cells[5].Text);
                        dr["Available"] = Convert.ToDecimal(GRDCATEGORYITEM.Rows[k].Cells[6].Text);
                        dr["ItemDetailsId"] = Convert.ToDecimal(GRDCATEGORYITEM.Rows[k].Cells[3].Text);
                        dr["ItemDesc"] = (GRDCATEGORYITEM.Rows[k].Cells[4].Text.Equals("&nbsp;")) ? "" : Convert.ToString(GRDCATEGORYITEM.Rows[k].Cells[4].Text).Replace("amp;", "");
                       
                        dr["Rate"] = Convert.ToDecimal(GRDCATEGORYITEM.Rows[k].Cells[7].Text); ;
                        dr["Outward"] = (((TextBox)GRDCATEGORYITEM.Rows[k].FindControl("txtOutward")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GRDCATEGORYITEM.Rows[k].FindControl("txtOutward")).Text);
                        dr["LocId"] = Convert.ToDecimal(((DropDownList)GRDCATEGORYITEM.Rows[k].FindControl("ddlLocation")).Text);
                        dr["Amount"] = Convert.ToDecimal(GRDCATEGORYITEM.Rows[k].Cells[9].Text);
                        dt.Rows.Add(dr);
                        k++;
                    }
                    else if ((GRDCATEGORYITEM.Rows[k].Cells[2].Text.Equals("&nbsp;")))
                    {

                    }
                    else
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["#"] = (((Label)GRDCATEGORYITEM.Rows[k].FindControl("LblEntryId")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((Label)GRDCATEGORYITEM.Rows[k].FindControl("LblEntryId")).Text);
                        dr["ItemId"] = Convert.ToInt32(GRDCATEGORYITEM.Rows[k].Cells[1].Text);
                        dr["Item"] = (GRDCATEGORYITEM.Rows[k].Cells[2].Text.Equals("&nbsp;")) ? "" : Convert.ToString(GRDCATEGORYITEM.Rows[k].Cells[2].Text).Replace("amp;", "");

                        dr["Unit"] = Convert.ToString(GRDCATEGORYITEM.Rows[k].Cells[5].Text);
                        dr["Available"] = Convert.ToDecimal(GRDCATEGORYITEM.Rows[k].Cells[6].Text);
                        dr["ItemDetailsId"] = Convert.ToDecimal(GRDCATEGORYITEM.Rows[k].Cells[3].Text);
                        dr["ItemDesc"] = (GRDCATEGORYITEM.Rows[k].Cells[4].Text.Equals("&nbsp;")) ? "" : Convert.ToString(GRDCATEGORYITEM.Rows[k].Cells[4].Text).Replace("amp;", "");

                        dr["Rate"] = Convert.ToDecimal(GRDCATEGORYITEM.Rows[k].Cells[7].Text); ;
                        dr["Outward"] = (((TextBox)GRDCATEGORYITEM.Rows[k].FindControl("txtOutward")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GRDCATEGORYITEM.Rows[k].FindControl("txtOutward")).Text);
                        dr["LocId"] = Convert.ToDecimal(((DropDownList)GRDCATEGORYITEM.Rows[k].FindControl("ddlLocation")).Text);
                        dr["Amount"] = Convert.ToDecimal(GRDCATEGORYITEM.Rows[k].Cells[9].Text);
                        dt.Rows.Add(dr);
                        k++;
                    }
                }
                else
                {
                    DataRow dr;
                    dr = dt.NewRow();
                    dr["#"] = 0;
                    dr["ItemId"] = 0;
                    dr["Item"] = "";
                    dr["ItemDetailsId"] = 0;
                    dr["ItemDesc"] = "";
                    dr["Unit"] = "";
                    dr["Available"] = 0;
                    dr["Outward"] = 0;
                    dr["Rate"] = 0;
                    dr["Amount"] = 0;
                    dr["LocID"] = 0;
                    dt.Rows.Add(dr);
                    
                }
            }
            ViewState["CurrentTableCategoryItem"] = dt;
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void txtOutward_TextChanged1(object sender, EventArgs e)
    {
        #region[ForCategoryItemWise]
        TextBox txt = (TextBox)sender;
        Decimal OutWard = !string.IsNullOrEmpty(txt.Text) ? Convert.ToDecimal(txt.Text) : 0;
        GridViewRow grd = (GridViewRow)txt.Parent.Parent;
        if (Convert.ToInt32(((DropDownList)grd.FindControl("ddlUnitConversion")).SelectedValue) > 0)
        {
            string str=((DropDownList)grd.FindControl("ddlUnitConversion")).SelectedItem.ToString();
            decimal s =Convert.ToDecimal(str.Substring(str.IndexOf('-') + 2));
            
            if (((Convert.ToDecimal(grd.Cells[6].Text)*s) >= OutWard))
            {
                //((Label)grd.FindControl("LblPending")).Text = (Convert.ToDecimal(grd.Cells[4].Text) - OutWard).ToString("#0.00");//8 for amount
                if (s > 0)
                {
                    grd.Cells[9].Text = (Convert.ToDecimal(grd.Cells[7].Text) * (OutWard / s)).ToString("#0.00");
                }
                ((DropDownList)grd.FindControl("ddlLocation")).Focus();
            }
            else
            {
                obj_Comman.ShowPopUpMsg("You have only  " + (Convert.ToDecimal(grd.Cells[6].Text)) + " Items to Issuse", this.Page);
                txt.Focus();
            }
        }
        else
        {
            obj_Comman.ShowPopUpMsg("Please Select Unit.", this.Page);
            txt.Text = "0";
            txt.Focus();
        }
        #endregion
    }

    protected void txtOutward_TextChanged2(object sender, EventArgs e)
    {
        #region[ForRequisitionWise]
        TextBox txt = (TextBox)sender;
        Decimal OutWard = !string.IsNullOrEmpty(txt.Text) ? Convert.ToDecimal(txt.Text) : 0;
        GridViewRow grd = (GridViewRow)txt.Parent.Parent;
        
            if ((Convert.ToDecimal(grd.Cells[6].Text) >= OutWard))
            {
                if ((Convert.ToDecimal(grd.Cells[7].Text) >= OutWard))
                {
                    //((Label)grd.FindControl("LblPending")).Text = (Convert.ToDecimal(grd.Cells[4].Text) - OutWard).ToString("#0.00");//8 for amount
                    grd.Cells[10].Text = (Convert.ToDecimal(grd.Cells[6].Text) * OutWard).ToString("#0.00");
                    txt.Focus();
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("You have only  " + (Convert.ToDecimal(grd.Cells[7].Text)) + " Items to Issuse", this.Page);
                    txt.Focus();
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("You have only  " + (Convert.ToDecimal(grd.Cells[6].Text)) + " Items In Stock", this.Page);
                txt.Text = "0.00";
                txt.Focus();
            }
        
        #endregion
    }

    protected void IMREFRESH_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            StrCondition = string.Empty;
            string fromdate = string.Empty;
            string todate = string.Empty;
            if (!string.IsNullOrEmpty(TxtFromDateR.Text))
            {
                fromdate = fromdate + Convert.ToDateTime(TxtFromDateR.Text).ToShortDateString();
            }
            else
            {
                fromdate = fromdate + DateTime.Now.AddMonths(-1).ToShortDateString();
            }
            if (!string.IsNullOrEmpty(TxtToDateR.Text))
            {
                todate = todate + Convert.ToDateTime(TxtToDateR.Text).ToShortDateString();
            }
            else
            {
                todate = todate + DateTime.Now.ToShortDateString();
            }
            Ds = Obj_StockMaster.FillComboRequisition(fromdate,todate,out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlRequisitnNo.DataSource = Ds.Tables[0];
                    ddlRequisitnNo.DataTextField = "RequisitionNo";
                    ddlRequisitnNo.DataValueField = "RequisitionCafeId";
                    ddlRequisitnNo.DataBind();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void DDLCATEGORY_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            StrCondition = string.Empty;
            if (Convert.ToInt32(DDLCATEGORY.SelectedValue)>0)
            {
                StrCondition = StrCondition + " AND CategoryID= " + DDLCATEGORY.SelectedValue;
            }
            Ds = Obj_StockMaster.FillComboItemCategory(StrCondition, out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    DDLITEMS.DataSource = Ds.Tables[0];
                    DDLITEMS.DataTextField = "ItemName";
                    DDLITEMS.DataValueField = "ItemId";
                    DDLITEMS.DataBind();
                }
                if (Ds.Tables[1].Rows.Count > 0)
                {
                    ddlSubcategory.DataSource = Ds.Tables[1];
                    ddlSubcategory.DataTextField = "Name";
                    ddlSubcategory.DataValueField = "Id";
                    ddlSubcategory.DataBind();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void ddlSubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            StrCondition = string.Empty;
            if (Convert.ToInt32(ddlSubcategory.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " AND SubcategoryId= " + ddlSubcategory.SelectedValue;
            }
            Ds = Obj_StockMaster.FillComboItemSUBCategory(StrCondition, out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    DDLITEMS.DataSource = Ds.Tables[0];
                    DDLITEMS.DataTextField = "ItemName";
                    DDLITEMS.DataValueField = "ItemId";
                    DDLITEMS.DataBind();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void DDLITEMS_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsItem_All =new DataSet();
        if (Convert.ToInt32(DDLITEMS.SelectedValue) > 0)
        {
            dsItem_All = Obj_StockMaster.Fill_Discription(Convert.ToInt32(DDLITEMS.SelectedValue), out StrError);


            if (dsItem_All.Tables[0].Rows.Count > 0)
            {
                ddldesc.DataSource = dsItem_All.Tables[0];
                ddldesc.DataTextField = "ItemDesc";
                ddldesc.DataValueField = "ItemDetailsId";
                ddldesc.DataBind();
            }
            else
            {
                ddldesc.SelectedValue = "0";
            }
        }
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
                DDLITEMS.SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();

                DDLITEMS_SelectedIndexChanged(DDLITEMS as AjaxControlToolkit.ComboBox, EventArgs.Empty);
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
