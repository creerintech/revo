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
using MayurInventory.EntityClass;
using MayurInventory.Utility;
using MayurInventory.DataModel;

public partial class Transactions_MaterialRequistnCancel : System.Web.UI.Page
{
    #region[Variables]
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        static int flag = 0; static int chkbox = 0; static int chkDup = 0; static int Row = 0; static int ID;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        DMRequisitionCancellation obj_DMReqCancellation = new DMRequisitionCancellation();
        RequisitionCancellation Entity_ReqCancellation = new RequisitionCancellation();
        RequisitionCafeteria Entity_Requisition = new RequisitionCafeteria();
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
    #endregion

    #region[User Defines]

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

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Requisition Cancellation'");
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
            catch (ThreadAbortException ex)
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

        TxtCancelDate.Text = string.Empty;
        //lblEmployee.Text = string.Empty;
        TxtCancelReason.Text = string.Empty;
        lblCafeteria.Text = string.Empty;

        RdoType.SelectedValue = "R";
        TxtSearch.Text = string.Empty;
        ChkFrmDate.Checked = true;
        txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
        txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        TxtCancelDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        SetInitialRow();
        SetInitialRow_GridItem();
        GetBindReqDtls(StrCondition);

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

    private void MakeReadOnlyForm(bool False)
    {
        RdoType.Enabled = False;        
        ChkFrmDate.Enabled=False;
        txtFromDate.Enabled = False;
        txtToDate.Enabled = False;
        ddlReqNo.Enabled = False;

        BtnShow.Enabled = False;
        BtnShowReq.Enabled = False;
    }

    private void FillCombo()
    {
        try
        {
            DataSet DSRequisition = new DataSet();
            string cond = string.Empty;
            //if (Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
            //    cond = string.Empty;
            //}
            //else
            //{
            //    cond = " and RC.CafeteriaId="+Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}
            DSRequisition = obj_DMReqCancellation.FillCombo(cond, out StrError);
            if (DSRequisition.Tables.Count > 0)
            {
                if (DSRequisition.Tables[0].Rows.Count > 0)
                {
                    ddlReqNo.DataSource = DSRequisition.Tables[0];
                    ddlReqNo.DataTextField = "RequisitionNo";
                    ddlReqNo.DataValueField = "RequisitionCafeId";
                    ddlReqNo.DataBind();
                    ddlReqNo.Focus();
                }
            }
            else
            {
                DSRequisition = null;
                obj_DMReqCancellation = null;
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("#", typeof(Int32));
        dt.Columns.Add("RequisitionNo", typeof(string));
        dt.Columns.Add("RequisitionDate", typeof(string));
        dt.Columns.Add("Cafeteria", typeof(string));
        dt.Columns.Add("CafeteriaId", typeof(Int32));
        dt.Columns.Add("IsCancel", typeof(Int32));
    

        dr = dt.NewRow();

        dr["#"] = 0;
        dr["RequisitionNo"] = "";
        dr["RequisitionDate"] = "";
        dr["Cafeteria"] = "";
        dr["CafeteriaId"] = 0;
        dr["IsCancel"] = 0;
        
        dt.Rows.Add(dr);

        ViewState["CurrentTable1"] = dt;
        GridDetails.DataSource = dt;
        GridDetails.DataBind();

    }

    private void SetInitialRow_GridItem()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("#", typeof(Int32));        
        dt.Columns.Add("ItemName", typeof(string));
        dt.Columns.Add("ItemDesc", typeof(string));
        dt.Columns.Add("RemarkForPO", typeof(string));
        dt.Columns.Add("ItemCode", typeof(string));
        dt.Columns.Add("MinStockLevel", typeof(string));
        dt.Columns.Add("OrdQty", typeof(string));
        dt.Columns.Add("AvlQty", typeof(string));
        dt.Columns.Add("ItemId", typeof(string));
        dt.Columns.Add("IsCancel", typeof(string));
        dt.Columns.Add("Unit", typeof(string)); 
        dr = dt.NewRow();

        dr["#"] = 0;        
        dr["ItemName"] = "";
        dr["ItemCode"] = "";
        dr["ItemDesc"] = "";
        dr["RemarkForPO"] = "";
        dr["MinStockLevel"] = "";
        dr["OrdQty"] = "";
        dr["AvlQty"] = "";
        dr["ItemId"] = 0;
        dr["IsCancel"] = "";
        dr["Unit"] = ""; 

        dt.Rows.Add(dr);

        ViewState["CurrentTable2"] = dt;
        GridDtlsItem.DataSource = dt;
        GridDtlsItem.DataBind();

    }

    private void ReportToGrid()
    {
        try
        {
            DataSet DsGrd = new DataSet();
            StrCondition = string.Empty;

            if (ChkFrmDate.Checked == true)
            {
                StrCondition = StrCondition + " AND (RequisitionDate between '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "' )";
            }
            else
            {
                StrCondition = StrCondition + " AND (RequisitionDate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "' )";
            } 

            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
            //    //StrCondition = StrCondition + " AND RCF.CafeteriaId=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}
            DsGrd = obj_DMReqCancellation.GetRecordForRequisition("R",StrCondition, out StrError);

            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                GridDetails.DataSource = DsGrd.Tables[0];
                GridDetails.DataBind();
                GridDetails.Visible = true;
            }
            else
            {
                GridDetails.DataSource = null;
                GridDetails.DataBind();
               
            }
            obj_DMReqCancellation = null;
            DsGrd = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void ReportGridItem()
    {
        try
        {
            DataSet DsGrd = new DataSet();
            StrCondition = string.Empty;
            int ReqId=Convert.ToInt32(ddlReqNo.SelectedValue.ToString());
            DsGrd = obj_DMReqCancellation.GetRecordForItem("I",ReqId, out StrError);

            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                GridDtlsItem.DataSource = DsGrd.Tables[0];
                GridDtlsItem.DataBind();
                GridDtlsItem.Visible = true;
            }
            else
            {
                GridDtlsItem.DataSource = null;
                GridDtlsItem.DataBind();

            }
            obj_DMReqCancellation = null;
            DsGrd = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void GetBindReqDtls(string strCondition)
    {
        Ds = new DataSet();
        string COND = string.Empty;
        //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
        //{
        //    COND = COND + " AND RCF.CafeteriaId=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
        //}
        Ds = obj_DMReqCancellation.GetRequisitionCancelationList(strCondition, out StrError);
        if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
        {
            ReportGrid.DataSource = Ds.Tables[0];
            ReportGrid.DataBind();
        }
        else
        {
            ReportGrid.DataSource = null;
            ReportGrid.DataBind();
        }
    }

    private void CheckEmployeeName(Label LableEmpName)
    {        
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
        {
            LableEmpName.Text = Session["UserName"].ToString();
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
            if (RdoType.SelectedValue == "R")
            {
                T1.Visible = true;
                T2.Visible = false;
                GridDtlsItem.Visible = false;
            }
            MakeEmptyForm();

            // Check Session (if Session is null then go to Login Page)
            CheckEmployeeName(lblEmployee);
            CheckUserRight();
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
        RdoType.SelectedValue = "R";
        RdoType_SelectedIndexChanged(sender,e);
        MakeReadOnlyForm(true);
    }
    protected void RdoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RdoType.SelectedValue == "R")
        {
            T1.Visible = true;
            T2.Visible = false;
            GridDetails.Visible = true;
            GridDtlsItem.Visible = false;
        }
        else
        {
            T2.Visible = true;
            T1.Visible = false;
            GridDetails.Visible = false;
            GridDtlsItem.Visible = true;
            FillCombo();
        }

    }
    protected void ChkFrmDate_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkFrmDate.Checked == true)
        {
            txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
        }
        else
        {
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        ReportToGrid();
    }
    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, UpdateIsDelivr = 0, InsertRowDtls = 0;
        try
        {
            //--------**Save Record RequisitionWise**-----------
            if (RdoType.SelectedValue == "R")
            {
                //Entity_ReqCancellation.RequisitionCafeId =0;
                Entity_ReqCancellation.CancelledBy = lblEmployee.Text;
                Entity_ReqCancellation.CancelledDate = (!string.IsNullOrEmpty(TxtCancelDate.Text)) ? Convert.ToDateTime(TxtCancelDate.Text) : Convert.ToDateTime("01/01/1753");
                Entity_ReqCancellation.Reason = TxtCancelReason.Text;

                Entity_ReqCancellation.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_ReqCancellation.LoginDate = DateTime.Now;
                //--**Update Cancel Flag in RequisitionCafeteria**--
                for (int i = 0; i < GridDetails.Rows.Count; i++)
                {
                    Label LblEntryId = (Label)GridDetails.Rows[i].Cells[i].FindControl("LblEntryId");
                    Entity_ReqCancellation.RequisitionCafeId = Convert.ToInt32(LblEntryId.Text);

                    if (((CheckBox)GridDetails.Rows[i].Cells[i].FindControl("GrdSelectAll1")).Checked == true)
                    {                        
                        InsertRow = obj_DMReqCancellation.InsertRecord(ref Entity_ReqCancellation, out StrError);
                    }                    
                }
                if (InsertRow > 0)
                {
                    if (InsertRow > 0)
                    {
                        obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                        MakeEmptyForm();
                        Entity_ReqCancellation = null;
                        obj_DMReqCancellation = null;
                    }
                }
            }
                //--------**Save Record ItemWise**-----------
            else
            {
                int CancelID = 0;
                Entity_ReqCancellation.RequisitionCafeId = Convert.ToInt32(ddlReqNo.SelectedValue);
                Entity_ReqCancellation.CancelledBy = lblEmployee.Text;
                Entity_ReqCancellation.CancelledDate = (!string.IsNullOrEmpty(TxtCancelDate.Text)) ? Convert.ToDateTime(TxtCancelDate.Text) : Convert.ToDateTime("01/01/1753");
                Entity_ReqCancellation.Reason = TxtCancelReason.Text;


                Entity_ReqCancellation.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_ReqCancellation.LoginDate = DateTime.Now;

                InsertRow = obj_DMReqCancellation.InsertRecordForItem(ref Entity_ReqCancellation, out StrError);
                CancelID = InsertRow;
                //--**Update Cancel Flag in RequisitionCafeDetails**--
                for (int i = 0; i < GridDtlsItem.Rows.Count; i++)
                {
                    Label LblEntryId = (Label)GridDtlsItem.Rows[i].Cells[1].FindControl("LblEntryId");
                    Entity_ReqCancellation.RequisitionCancelId = Convert.ToInt32(CancelID);
                    Entity_ReqCancellation.RequisitionCafeId = Convert.ToInt32(LblEntryId.Text);
                    Entity_ReqCancellation.OrdQty = Convert.ToDecimal(GridDtlsItem.Rows[i].Cells[7].Text.ToString());
                    Entity_ReqCancellation.ItemId = Convert.ToInt32(GridDtlsItem.Rows[i].Cells[10].Text.ToString());

                    if (((CheckBox)GridDtlsItem.Rows[i].Cells[i].FindControl("GrdSelectAll2")).Checked == true)
                    {
                        InsertRow = obj_DMReqCancellation.InsertReqCancelDtls(ref Entity_ReqCancellation, out StrError);
                    }
                }
                if (InsertRow > 0)
                {
                    if (InsertRow > 0)
                    {
                        obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                        MakeEmptyForm();
                        Entity_ReqCancellation = null;
                        obj_DMReqCancellation = null;
                    }
                }
            }
        
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void ddlReqNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Ds = new DataSet();
        int ReqId = Convert.ToInt32(ddlReqNo.SelectedValue.ToString());
        Ds = obj_DMReqCancellation.GetReqCafeteria(ReqId, out StrError);
        if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
        {
            lblCafeteria.Text = Ds.Tables[0].Rows[0]["Cafeteria"].ToString();
        }
        else
        {
            lblCafeteria.Text = string.Empty;
        }

    }
    protected void BtnShowReq_Click(object sender, EventArgs e)
    {
        ReportGridItem();
    }

    protected void ReportGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {                        
                        int CurrRow = Convert.ToInt32(e.CommandArgument);

                        Label RequisitionCancelId = ((Label)ReportGrid.Rows[CurrRow].Cells[0].FindControl("LblEstimateId"));
                        ViewState["ID"] = Convert.ToInt32(RequisitionCancelId.Text);

                        int CancelType = ReportGrid.Rows[CurrRow].Cells[6].Text.Equals("False")?0:1;
                        int RequisitionCafeId = Convert.ToInt32(ReportGrid.Rows[CurrRow].Cells[7].Text.ToString());

                        if (CancelType == 0)
                        {
                            Ds = obj_DMReqCancellation.GetRecordForEdit_ItemWise(Convert.ToInt32(RequisitionCancelId.Text), RequisitionCafeId, out StrError);
                        }
                        else
                        {
                            Ds = obj_DMReqCancellation.GetRecordForEdit_RequisitionWise(Convert.ToInt32(RequisitionCancelId.Text), RequisitionCafeId, out StrError);
                        }
                        if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                        {
                            lblEmployee.Text = Ds.Tables[0].Rows[0]["CancelledBy"].ToString();
                            TxtCancelDate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["CancelledDate"]).ToString("dd/MMM/yyyyy");
                            TxtCancelReason.Text = Ds.Tables[0].Rows[0]["Reason"].ToString();

                            RdoType.SelectedValue = Convert.ToBoolean(Ds.Tables[0].Rows[0]["CancelType"]).Equals(false) ? "I" : "R";                            
                        }
                        else
                        {
                            MakeEmptyForm();
                        }
                        if (Ds.Tables[1].Rows.Count > 0)
                        {
                            if (RdoType.SelectedValue == "I")
                            {
                                GridDtlsItem.DataSource = Ds.Tables[1];
                                GridDtlsItem.DataBind();
                                ViewState["CurrentTable1"] = Ds.Tables[1];

                                T2.Visible = true;
                                T1.Visible = false;
                                GridDetails.Visible = false;
                                GridDtlsItem.Visible = true;

                                FillCombo();
                                ddlReqNo.SelectedValue = Ds.Tables[0].Rows[0]["RequisitionCafeId"].ToString();
                                lblCafeteria.Text = Ds.Tables[1].Rows[0]["Cafeteria"].ToString();
                            }
                            else
                            {
                                GridDetails.DataSource = Ds.Tables[1];
                                GridDetails.DataBind();
                                ViewState["CurrentTable1"] = Ds.Tables[1];

                                T1.Visible = true;
                                T2.Visible = false;
                                GridDetails.Visible = true;
                                GridDtlsItem.Visible = false;
                            }
                        }

                        Ds = null;
                       // Obj_StockMaster = null;
                        BtnUpdate.Visible = true;
                        BtnSave.Visible = false;
                        MakeReadOnlyForm(false);
                        
                        break;
                    }
                case ("Delete"):
                    {
                        int CurrRow = Convert.ToInt32(e.CommandArgument);

                        Label RequisitionCancelId = ((Label)ReportGrid.Rows[CurrRow].Cells[0].FindControl("LblEstimateId"));
                        ViewState["ID"] = Convert.ToInt32(RequisitionCancelId.Text);

                        string DeleteCancelType = ReportGrid.Rows[CurrRow].Cells[6].Text.Equals("False") ? "I" : "R";
                        int RequisitionCafeId = Convert.ToInt32(ReportGrid.Rows[CurrRow].Cells[7].Text.ToString());

                        int DeleteDetails = obj_DMReqCancellation.DeleteRequisitionCancel(DeleteCancelType, Convert.ToInt32(RequisitionCancelId.Text), RequisitionCafeId, out StrError);

                        if (DeleteDetails > 0)
                        {
                            obj_Comman.ShowPopUpMsg("Record Deleted Successfully", this.Page);
                            MakeEmptyForm();
                        }
                    }
                    break;
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }
    protected void GridDtlsItem_DataBound(object sender, EventArgs e)
    {
        try
        {
            for (int rowIndex = GridDtlsItem.Rows.Count - 1; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = GridDtlsItem.Rows[rowIndex];

                string CancelStatus = Convert.ToString(GridDtlsItem.Rows[rowIndex].Cells[8].Text);
                if (CancelStatus != "False")
                {
                    ((CheckBox)GridDtlsItem.Rows[rowIndex].Cells[1].FindControl("GrdSelectAll2")).Checked = true;                   
                }
                else
                {
                    ((CheckBox)GridDtlsItem.Rows[rowIndex].Cells[1].FindControl("GrdSelectAll2")).Checked=false;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, UpdateIsDelivr = 0, InsertRowDtls = 0;
        try
        {
            //--------**Save Record RequisitionWise**-----------
            if (RdoType.SelectedValue == "R")
            {
                Entity_ReqCancellation.RequisitionCancelId = Convert.ToInt32(ViewState["ID"]);
                Entity_ReqCancellation.CancelledBy = lblEmployee.Text;
                Entity_ReqCancellation.CancelledDate = (!string.IsNullOrEmpty(TxtCancelDate.Text)) ? Convert.ToDateTime(TxtCancelDate.Text) : Convert.ToDateTime("01/01/1753");
                Entity_ReqCancellation.Reason = TxtCancelReason.Text;


                Entity_ReqCancellation.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_ReqCancellation.LoginDate = DateTime.Now;
                //--**Update Cancel Flag in RequisitionCageteria**--
                for (int i = 0; i < GridDetails.Rows.Count; i++)
                {
                    Label LblEntryId = (Label)GridDetails.Rows[i].Cells[i].FindControl("LblEntryId");
                    CheckBox GrdSelectAll1 = (CheckBox)GridDetails.Rows[i].Cells[i].FindControl("GrdSelectAll1");
                    
                    Entity_ReqCancellation.RequisitionCafeId = Convert.ToInt32(LblEntryId.Text);
                    Entity_ReqCancellation.IsCancel = GrdSelectAll1.Checked.Equals(false) ? 0 : 1;

                    InsertRow = obj_DMReqCancellation.UpdateRequisitionWise(ref Entity_ReqCancellation, out StrError);
                    
                }
                if (InsertRow > 0)
                {
                    if (InsertRow > 0)
                    {
                        obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                        MakeEmptyForm();
                        Entity_ReqCancellation = null;
                        obj_DMReqCancellation = null;
                    }
                }
            }
            //--------**Save Record ItemWise**-----------
            else
            {
                int CountChecked = 0;
                int CancelID = Convert.ToInt32(ViewState["ID"]);
                Entity_ReqCancellation.RequisitionCancelId = Convert.ToInt32(ViewState["ID"]);
                Entity_ReqCancellation.RequisitionCafeId = Convert.ToInt32(ddlReqNo.SelectedValue);
                Entity_ReqCancellation.CancelledBy = lblEmployee.Text;
                Entity_ReqCancellation.CancelledDate = (!string.IsNullOrEmpty(TxtCancelDate.Text)) ? Convert.ToDateTime(TxtCancelDate.Text) : Convert.ToDateTime("01/01/1753");
                Entity_ReqCancellation.Reason = TxtCancelReason.Text;


                Entity_ReqCancellation.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_ReqCancellation.LoginDate = DateTime.Now;

                InsertRow = obj_DMReqCancellation.UpdateRequisitionItemWise(ref Entity_ReqCancellation, out StrError);
                
                //--**Update Cancel Flag in RequisitionCafeDetails**--
                for (int i = 0; i < GridDtlsItem.Rows.Count; i++)
                {
                    Label LblEntryId = (Label)GridDtlsItem.Rows[i].Cells[1].FindControl("LblEntryId");
                    Entity_ReqCancellation.RequisitionCancelId = Convert.ToInt32(CancelID);
                    Entity_ReqCancellation.RequisitionCafeId = Convert.ToInt32(LblEntryId.Text);
                    Entity_ReqCancellation.OrdQty = Convert.ToDecimal(GridDtlsItem.Rows[i].Cells[7].Text.ToString());
                    Entity_ReqCancellation.ItemId = Convert.ToInt32(GridDtlsItem.Rows[i].Cells[10].Text.ToString());

                    if (((CheckBox)GridDtlsItem.Rows[i].Cells[i].FindControl("GrdSelectAll2")).Checked == true)
                    {
                        InsertRow = obj_DMReqCancellation.InsertReqCancelDtls(ref Entity_ReqCancellation, out StrError);
                    }
                    else
                    {
                        CountChecked = CountChecked + 1;
                    }
                }
                if (CountChecked == GridDtlsItem.Rows.Count)
                {

                }
                if (InsertRow > 0)
                {
                    if (InsertRow > 0)
                    {
                        obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                        MakeEmptyForm();
                        Entity_ReqCancellation = null;
                        obj_DMReqCancellation = null;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void GridDetails_DataBound(object sender, EventArgs e)
    {
        try
        {
            for (int rowIndex = GridDetails.Rows.Count - 1; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = GridDetails.Rows[rowIndex];

                string CancelStatus = Convert.ToString(GridDetails.Rows[rowIndex].Cells[6].Text);
                if (CancelStatus != "False")
                {
                    ((CheckBox)GridDetails.Rows[rowIndex].Cells[1].FindControl("GrdSelectAll1")).Checked = true;
                }
                else
                {
                    ((CheckBox)GridDetails.Rows[rowIndex].Cells[1].FindControl("GrdSelectAll1")).Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void ReportGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
        }
    }
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            StrCondition = string.Empty;
            StrCondition = TxtSearch.Text.Trim();
            Ds = new DataSet();
            Ds = obj_DMReqCancellation.GetRequisitionCancelationList(StrCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                //ViewState["CurrentTable"] = Ds.Tables[1];
                ReportGrid.DataSource = Ds.Tables[0];
                ReportGrid.DataBind();
                Ds = null;
            }
            else
            {
                ReportGrid.DataSource = null;
                ReportGrid.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMRequisitionCancellation Obj_RequisitionCafeteria = new DMRequisitionCancellation();
        String[] SearchList = Obj_RequisitionCafeteria.GetSuggestedRecord(prefixText);
        return SearchList;
    }
    protected void ReportGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.ReportGrid.PageIndex = e.NewPageIndex;
            Ds = new DataSet();
            Ds = obj_DMReqCancellation.GetRequisitionCancelationList("", out StrError);
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
                else
                {
                    foreach (GridViewRow GRow in ReportGrid.Rows)
                    {
                        GRow.FindControl("ImageGridEdit").Visible = false;
                    }
                }
            }
            else
            {
               
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void GrdSelectAllHeaderR_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox GrdSelectAllHeader1 = (CheckBox)sender;
        GridViewRow row = (GridViewRow)GrdSelectAllHeader1.NamingContainer;
        int CurrRow = row.RowIndex;
        if (GrdSelectAllHeader1.Checked == true)
        {
            for (int j = 0; j < GridDetails.Rows.Count; j++)
            {
                CheckBox GrdSelectAll = (CheckBox)GridDetails.Rows[j].Cells[1].FindControl("GrdSelectAll1");
                GrdSelectAll.Checked = true;
            }
        }
        else
        {
            for (int j = 0; j < GridDetails.Rows.Count; j++)
            {
                CheckBox GrdSelectAll = (CheckBox)GridDetails.Rows[j].Cells[1].FindControl("GrdSelectAll1");
                GrdSelectAll.Checked = false;
            }
        }
    }

}
