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
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using System.Threading;

public partial class Transactions_MaterialComsumption : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MakeEmptyForm();
            CheckUserRight();
            MakeControlEmpty();
            CheckUserRight();
        }
    }

    #region [Private Variables]
        DMConsumptionMaster Obj_ConsumeMaster = new DMConsumptionMaster();
        ConsumptionMaster Entity_ConsumeMaster = new ConsumptionMaster();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        decimal Amount = 0;
        DataTable DtEditIssue;
        public static int ItemId = 0, LocationId = 0;
        private string StrCondition = string.Empty;
        private string StrCond = string.Empty;
        private string StrError = string.Empty;
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Consumption Register'");
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
        if (!FlagAdd)
            BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        EnableControl(true);
        FillCombo();
        TxtConsumptionNo.Text = string.Empty;
        TxtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        TxtFromDateR.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
        TxtToDateR.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        SetInitialRow();
        GetConsumeNo();
        BindReportGrid(StrCond);
        TRGRIDINWARD.Visible = true;
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

    private void EnableControl(bool value)
    {
        ImageDate.Enabled = TxtDate.Enabled = value;
        ddlIssueNo.Visible = BtnShowIssue.Visible = value;
        LBLISSUENO.Visible = !value;
    }

    private void MakeControlEmpty()
    {
        ddlIssueNo.SelectedValue = "0";
        ViewState["GridIndex"] = null;
        ViewState["GridDetails"] = null;
        ViewState["Flag"] = null;
        ViewState["dttavlqty"] = null;
    }

    private bool ChkGridRowExist()
    {
        bool FlagChk = false;
        if (GridIssueDetails.Rows.Count > 0 && !string.IsNullOrEmpty(GridIssueDetails.Rows[0].Cells[2].Text) && !GridIssueDetails.Rows[0].Cells[2].Text.Equals("&nbsp;"))
        {
            FlagChk = true;
        }
        return FlagChk;
    }

    private bool ChkGridRow()
    {
        bool FlagChk = false;
        if (GridIssueDetails.Rows.Count > 0 && !string.IsNullOrEmpty(GridIssueDetails.Rows[0].Cells[2].Text) && !GridIssueDetails.Rows[0].Cells[2].Text.Equals("&nbsp;") && Convert.ToInt32(GridIssueDetails.Rows[0].Cells[2].Text)>0)
        {
            FlagChk = true;
        }
        return FlagChk;
    }

    public void BindReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            string COND = string.Empty;
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                COND = COND + " AND ConsumptionRegister.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            DsReport = Obj_ConsumeMaster.GetConsumption(RepCondition, COND, out StrError);
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

    private void GetConsumeNo()
    {
        try
        {
            Ds = Obj_ConsumeMaster.GetConsumptionNo(out StrError);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                TxtConsumptionNo.Text = Ds.Tables[0].Rows[0]["ConsumptionNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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
            dt.Columns.Add("Issue", typeof(decimal));
            dt.Columns.Add("Rate", typeof(decimal));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("Consumption", typeof(decimal));
            dt.Columns.Add("ConsumptionAmt", typeof(decimal));
            dt.Columns.Add("Pending", typeof(decimal));
            dt.Columns.Add("PendingAmount", typeof(decimal));
            dt.Columns.Add("LocID", typeof(Int32));
            dt.Columns.Add("UnitId", typeof(Int32));
            dt.Columns.Add("Location", typeof(string));

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["ItemId"] = 0;
            dr["Item"] = "";
            dr["ItemDetailsId"] = 0;
            dr["ItemDesc"] = "";
            dr["Unit"] = "";
            dr["Issue"] = 0;
            dr["Rate"] = 0;
            dr["Amount"] = 0;
            dr["Consumption"] = 0;
            dr["ConsumptionAmt"] = 0;
            dr["Pending"] = 0;
            dr["PendingAmount"] = 0;
            dr["LocID"] = 0;
            dr["UnitId"] = 0;
            dr["Location"] = "";
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
            GridIssueDetails.DataSource = dt;
            GridIssueDetails.DataBind();

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
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                COND = COND + " AND P.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            Ds = Obj_ConsumeMaster.FillCombo(COND, out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlIssueNo.DataSource = Ds.Tables[0];
                    ddlIssueNo.DataTextField = "IssueNo";
                    ddlIssueNo.DataValueField = "IssueId";
                    ddlIssueNo.DataBind();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }
 
    #endregion

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        BindReportGrid(StrCondition);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMConsumptionMaster Obj_StockMaster = new DMConsumptionMaster();
        String[] SearchList = Obj_StockMaster.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void BtnShowIssue_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet DsGrd = new DataSet();
            StrCondition = string.Empty;
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            if (Convert.ToInt32(ddlIssueNo.SelectedValue) > 0)
            {
                DsGrd = Obj_ConsumeMaster.GetDetailOnCondForIssue(Convert.ToInt32(ddlIssueNo.SelectedValue), LocationId, out StrError);
                if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
                {
                    issusecond = 1;
                    GridIssueDetails.DataSource = DsGrd;
                    GridIssueDetails.DataBind();
                    ViewState["CurrentTableRequisition"] = DsGrd;
                    ((TextBox)GridIssueDetails.Rows[0].FindControl("txtGrdConsumption")).Focus();
                }
                else
                {
                    GridIssueDetails.DataSource = null;
                    GridIssueDetails.DataBind();
                    ddlIssueNo.Focus();
                }
                DsGrd = null;
            }
            else
            {
                ddlIssueNo.Focus();
            }
            

            
        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
        MakeControlEmpty();
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, UpdateRowDtls = 0;
        try
        {
            if (ViewState["ID"] != null)
            {
                Entity_ConsumeMaster.ConsumptionId = Convert.ToInt32(ViewState["ID"]);
            }
            Entity_ConsumeMaster.ConsumptionNo = TxtConsumptionNo.Text;
            Entity_ConsumeMaster.ConsumptionAsOn = !string.IsNullOrEmpty(TxtDate.Text) ? Convert.ToDateTime(TxtDate.Text) : DateTime.Now;
            Entity_ConsumeMaster.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_ConsumeMaster.LoginDate = DateTime.Now;
            UpdateRow = Obj_ConsumeMaster.UpdateRecord(ref Entity_ConsumeMaster, out StrError);
            if (UpdateRow > 0)
            {
                
                #region[issueWise]
                    if (ViewState["CurrentTable"] != null)
                    {
                        for (int i = 0; i < GridIssueDetails.Rows.Count; i++)
                        {
                            decimal Outwrd = (((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdConsumption")).Text).Equals("") ? 0 : Convert.ToDecimal(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdConsumption")).Text);
                            if (Outwrd > 0)
                            {
                                if (Outwrd != 0)
                                {
                                    Entity_ConsumeMaster.ConsumptionId = Convert.ToInt32(ViewState["ID"]);
                                    Entity_ConsumeMaster.IssueId = Convert.ToInt32(((Label)GridIssueDetails.Rows[i].FindControl("LblEntryId")).Text);
                                    Entity_ConsumeMaster.ItemId = Convert.ToInt32(GridIssueDetails.Rows[i].Cells[2].Text);
                                    Entity_ConsumeMaster.ItemDetailsId = Convert.ToInt32(GridIssueDetails.Rows[i].Cells[4].Text);
                                    Entity_ConsumeMaster.ItemDesc = Convert.ToString(GridIssueDetails.Rows[i].Cells[5].Text);
                                    Entity_ConsumeMaster.IssueQty = Convert.ToDecimal(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdIssue")).Text);
                                    Entity_ConsumeMaster.ConsumeQty = Convert.ToDecimal(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdConsumption")).Text);
                                    Entity_ConsumeMaster.PendingQty = Convert.ToDecimal(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdPending")).Text);
                                    Entity_ConsumeMaster.LocationId = Convert.ToInt32(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdLocationID")).Text);
                                    Entity_ConsumeMaster.StockLocationID = Convert.ToInt32(Session["CafeteriaId"].ToString());
                                    Entity_ConsumeMaster.Rate = Convert.ToDecimal(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdRate")).Text);
                                    Entity_ConsumeMaster.Amount = Convert.ToDecimal(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdConsumptionAmt")).Text);
                                    Entity_ConsumeMaster.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                    UpdateRowDtls = Obj_ConsumeMaster.InsertDetailsRecord(ref Entity_ConsumeMaster, Convert.ToInt32(0), out StrError);
                                    //UpdateRowDtls = Obj_ConsumeMaster.InsertDetailsRecord(ref Entity_ConsumeMaster, Convert.ToInt32(GridIssueDetails.Rows[i].Cells[16].Text), out StrError);
                                    if (Entity_ConsumeMaster.PendingQty > 0)
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
                                    Entity_ConsumeMaster.ConsumptionId = UpdateRow;
                                    Entity_ConsumeMaster.UserId = Convert.ToInt32(Session["UserID"]);
                                    Entity_ConsumeMaster.LoginDate = DateTime.Now;
                                    //int iDelete = Obj_ConsumeMaster.DeleteRecord(ref Entity_ConsumeMaster, out StrError);
                                }
                            }
                            string str;
                        j: str = string.Empty;
                        }
                        if (UpdateRow > 0)
                        {
                            int UpdateFlagInward = Obj_ConsumeMaster.UpdateAssignFlag(ref Entity_ConsumeMaster, out StrError);
                            obj_Comman.ShowPopUpMsg("Record Updated Successfully", this.Page);
                            FillCombo();
                            MakeControlEmpty();
                            MakeEmptyForm();
                            Entity_ConsumeMaster = null;
                            Obj_ConsumeMaster = null;
                        }
                    }
               
                #endregion
            
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDtls = 0;
        DataTable dtInsert = new DataTable();
        try
        {

            if (ChkGridRow())
            {
                Entity_ConsumeMaster.ConsumptionNo = TxtConsumptionNo.Text;
                Entity_ConsumeMaster.ConsumptionAsOn = !string.IsNullOrEmpty(TxtDate.Text) ? Convert.ToDateTime(TxtDate.Text) : Convert.ToDateTime(DateTime.Now.ToShortDateString());
                Entity_ConsumeMaster.IssueId = Convert.ToInt32(((Label)GridIssueDetails.Rows[0].FindControl("LblEntryId")).Text);
                Entity_ConsumeMaster.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_ConsumeMaster.LoginDate = DateTime.Now;
                InsertRow = Obj_ConsumeMaster.InsertRecord(ref Entity_ConsumeMaster, Convert.ToInt32((Session["CafeteriaId"].ToString())), out StrError);
                if (InsertRow > 0)
                {
                    #region[issueWise]
                    
                        if (ViewState["CurrentTable"] != null)
                        {
                            for (int i = 0; i < GridIssueDetails.Rows.Count; i++)
                            {
                                decimal Outwrd = (((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdConsumption")).Text).Equals("") ? 0 : Convert.ToDecimal(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdConsumption")).Text);
                                if (Outwrd > 0)
                                {
                                    if (Outwrd != 0)
                                    {
                                        Entity_ConsumeMaster.ConsumptionId = InsertRow;
                                        Entity_ConsumeMaster.IssueId = Convert.ToInt32(((Label)GridIssueDetails.Rows[i].FindControl("LblEntryId")).Text);
                                        Entity_ConsumeMaster.ItemId = Convert.ToInt32(GridIssueDetails.Rows[i].Cells[2].Text);

                                        Entity_ConsumeMaster.ItemDetailsId = Convert.ToInt32(GridIssueDetails.Rows[i].Cells[4].Text);
                                        Entity_ConsumeMaster.ItemDesc = Convert.ToString(GridIssueDetails.Rows[i].Cells[5].Text);

                                        Entity_ConsumeMaster.IssueQty = Convert.ToDecimal(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdIssue")).Text);
                                        Entity_ConsumeMaster.ConsumeQty = Convert.ToDecimal(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdConsumption")).Text);
                                        Entity_ConsumeMaster.PendingQty = Convert.ToDecimal(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdPending")).Text);
                                        Entity_ConsumeMaster.LocationId = Convert.ToInt32(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdLocationID")).Text);
                                        Entity_ConsumeMaster.StockLocationID = Convert.ToInt32(Session["CafeteriaId"].ToString());
                                        Entity_ConsumeMaster.Rate = Convert.ToDecimal(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdRate")).Text);
                                        Entity_ConsumeMaster.Amount = Convert.ToDecimal(((TextBox)GridIssueDetails.Rows[i].FindControl("txtGrdConsumptionAmt")).Text);
                                        Entity_ConsumeMaster.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                        //InsertRowDtls = Obj_ConsumeMaster.InsertDetailsRecord(ref Entity_ConsumeMaster,Convert.ToInt32(GridIssueDetails.Rows[i].Cells[16].Text), out StrError);
                                        InsertRowDtls = Obj_ConsumeMaster.InsertDetailsRecord(ref Entity_ConsumeMaster, Convert.ToInt32(0), out StrError);
                                        if (Entity_ConsumeMaster.PendingQty > 0)
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
                                        Entity_ConsumeMaster.ConsumptionId = InsertRow;
                                        Entity_ConsumeMaster.UserId = Convert.ToInt32(Session["UserID"]);
                                        Entity_ConsumeMaster.LoginDate = DateTime.Now;
                                       // int iDelete = Obj_ConsumeMaster.DeleteRecord(ref Entity_ConsumeMaster, out StrError);
                                    }
                                }
                                string str;
                            j: str = string.Empty;
                            }
                            if (InsertRow > 0)
                            {
                                //int UpdateFlagInward = Obj_ConsumeMaster.UpdateAssignFlag(ref Entity_ConsumeMaster, out StrError);
                                obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                                FillCombo();
                                MakeControlEmpty();
                                MakeEmptyForm();
                                Entity_ConsumeMaster = null;
                                Obj_ConsumeMaster = null;
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

    protected void ReportGrid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int DeleteId = 0;
        try
        {
            DeleteId = Convert.ToInt32(((ImageButton)ReportGrid1.Rows[e.RowIndex].Cells[0].FindControl("ImgBtnDelete")).CommandArgument.ToString());
            if (DeleteId != 0)
            {
                Entity_ConsumeMaster.ConsumptionId = DeleteId;
                Entity_ConsumeMaster.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_ConsumeMaster.LoginDate = DateTime.Now;

                int iDelete = Obj_ConsumeMaster.DeleteRecord(ref Entity_ConsumeMaster, out StrError);
                if (iDelete != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }

            Entity_ConsumeMaster = null;
            Obj_ConsumeMaster = null;
        }
        catch (Exception ex)
        {
        }
    }

    protected void ReportGrid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Rdb = string.Empty;
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        if (Convert.ToInt32(e.CommandArgument) != 0)
                        {
                            ViewState["ID"] = Convert.ToInt32(e.CommandArgument);
                            Ds = Obj_ConsumeMaster.GetRecordForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                            {
                                TxtConsumptionNo.Text = Ds.Tables[0].Rows[0]["ConsumptionNo"].ToString();
                                TxtDate.Text = Ds.Tables[0].Rows[0]["ConsumptionDate"].ToString();
                                LBLISSUENO.Text = Ds.Tables[0].Rows[0]["IssueId"].ToString();
                            }
                           
                            if (Ds.Tables.Count > 0 && Ds.Tables[1].Rows.Count > 0)
                            {
                                GridIssueDetails.DataSource = Ds.Tables[1];
                                GridIssueDetails.DataBind();
                                ViewState["CurrentTable"] = Ds.Tables[1];
                                EnableControl(false);
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            Ds = null;
                            Obj_ConsumeMaster = null;
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                        }
                        break;
                    }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void ReportGrid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.ReportGrid1.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            string COND = string.Empty;
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                COND = COND + " AND ConsumptionRegister.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            Ds = Obj_ConsumeMaster.GetConsumption(StrCondition, COND, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ReportGrid1.DataSource = Ds.Tables[0];
                this.ReportGrid1.DataBind();
                //-----For UserRights------
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
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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
            string COND = string.Empty;
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                COND = COND + " AND P.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            Ds = Obj_ConsumeMaster.FillComboIssueCond(fromdate, todate,COND, out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlIssueNo.DataSource = Ds.Tables[0];
                    ddlIssueNo.DataTextField = "IssueNo";
                    ddlIssueNo.DataValueField = "IssueId";
                    ddlIssueNo.DataBind();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
}
