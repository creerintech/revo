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

public partial class Transactions_MaterialReturn : System.Web.UI.Page
{
    #region[Variables]
        DMReturn Obj_damage = new DMReturn();
        EReturn Entity_damage = new EReturn();
        CommanFunction obj_Comm = new CommanFunction();
        DataSet DS = new DataSet();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        public static int ItemId = 0;
        public static bool chkflag = false;
        public static bool FlagForCalculationOperation;
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
    #endregion

    #region[UserDefineFunction]

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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Return Register'");
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
                        ReportGridDtls.Visible = false;
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
                        foreach (GridViewRow GRow in ReportGridDtls.Rows)
                        {
                            GRow.FindControl("ImgBtnDelete").Visible = false;
                            GRow.FindControl("ImageGridEdit").Visible = false;
                            GRow.FindControl("ImgBtnPrint").Visible = false;
                        }
                        BtnUpdate.Visible = false;
                        FlagDel = true;
                        FlagEdit = true;
                        FlagPrint = true;
                    }
                    else
                    {
                        //Checking Delete Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                        {
                            foreach (GridViewRow GRow in ReportGridDtls.Rows)
                            {
                                GRow.FindControl("ImgBtnDelete").Visible = false;
                                FlagDel = true;
                            }
                        }

                        //Checking Edit Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                        {
                            foreach (GridViewRow GRow in ReportGridDtls.Rows)
                            {
                                GRow.FindControl("ImageGridEdit").Visible = false;
                                FlagEdit = true;
                            }
                        }

                        //Checking Print Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                        {
                            foreach (GridViewRow GRow in ReportGridDtls.Rows)
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
        ddlInwardNo.Enabled = true;
        FlagForCalculationOperation = false;
        CalendarExtender2.EndDate=CalendarExtender1.EndDate = DateTime.Now;
        Fillcombo();
        lblPreparedBy.Text = Session["UserName"].ToString();
        lblReturndate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
        ddlInwardNo.SelectedValue = "0";
        lblInwardNo.Text = "";
        lblInwardDate.Text = "";
        lblSuppName.Text = "";
        lblQuotationDate.Text = "";
        lblQuotationNo.Text = "";
        lblpono.Text = "";
        lblPODATE.Text = "";
        TxtSearch.Text = "";
        ViewState["EditId"] = null;
        ViewState["DamagedDtls"] = null;
        SetInitialRow();
        BindReportGrid(StrCondition);
        chkReturnDebitNote.Checked = false;
        #region[UserRights]
        if (FlagEdit)
        {
            foreach (GridViewRow GRow in ReportGridDtls.Rows)
            {
                GRow.FindControl("ImageGridEdit").Visible = false;
            }
        }
        if (FlagDel)
        {
            foreach (GridViewRow GRow in ReportGridDtls.Rows)
            {
                GRow.FindControl("ImgBtnDelete").Visible = false;
            }
        }
        if (FlagPrint)
        {
            foreach (GridViewRow GRow in ReportGridDtls.Rows)
            {
                GRow.FindControl("ImgBtnPrint").Visible = false;
            }
        }
        #endregion
        ddlInwardNo.Focus();
    }

    private void MakeControlEmpty()
    {
        ViewState["GridIndex"] = null;
        ViewState["GrdInward"] = null;
        ViewState["ReturnDtls"] = null;
    }

    private void SetInitialRow() 
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("#", typeof(Int32));
        dt.Columns.Add("InwardNo", typeof(string));
        dt.Columns.Add("ItemId", typeof(Int32));
        dt.Columns.Add("ItemCode", typeof(string));
        dt.Columns.Add("ItemName", typeof(string));
        dt.Columns.Add("ItemDesc", typeof(string));
        dt.Columns.Add("UnitId", typeof(Int32));
        dt.Columns.Add("Unit", typeof(string));
        dt.Columns.Add("InwardQty", typeof(decimal));
        dt.Columns.Add("DamageQty", typeof(decimal));
        dt.Columns.Add("PrevReturnQty", typeof(decimal));
        dt.Columns.Add("GrdtxtReturnQty", typeof(decimal));
        dt.Columns.Add("rate", typeof(decimal));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("Reason", typeof(string));
        dr = dt.NewRow();
        dr["#"] = 0;
        dr["InwardNo"] = "";
        dr["ItemId"] = 0;
        dr["ItemCode"] = "";
        dr["ItemName"] = "";
        dr["ItemDesc"] = "";
        dr["UnitId"] = 0;
        dr["Unit"] = "";
        dr["InwardQty"] = 0.0;
        dr["DamageQty"] = 0.0;
        dr["PrevReturnQty"] = 0.0;
        dr["GrdtxtReturnQty"] = 0.0;
        dr["rate"] = 0.0;
        dr["Amount"] = 0.0;
        dr["Reason"] = "";
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        ViewState["ReturnDtls"] = dt;
        GrdInward.DataSource = dt;
        GrdInward.DataBind();
    }

    private void Fillcombo()
    {
        try
        {
             string COND = string.Empty;
            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
             COND = COND + " AND IRD.LocID=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}
             DS = Obj_damage.FillCombo(COND, out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlInwardNo.DataSource = DS.Tables[0];
                    ddlInwardNo.DataTextField = "InwardNo";
                    ddlInwardNo.DataValueField = "InwardId";
                    ddlInwardNo.DataBind();
                }
                if (DS.Tables[1].Rows.Count > 0)
                {
                    ddlSite.DataSource = DS.Tables[1];
                    ddlSite.DataTextField = "Location";
                    ddlSite.DataValueField = "StockLocationID";
                    ddlSite.DataBind();
                    ddlSite.SelectedValue = Convert.ToString(Session["CafeteriaId"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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

    private void BindReportGrid(string StrCondition)
    {
        try
        {
            DataSet DsGrd = new DataSet();
            DataTable dt = new DataTable();
            string COND = string.Empty;
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                COND = COND + " AND RM.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            DsGrd = Obj_damage.GetReturnDetails(StrCondition,COND, out StrError);
            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                ReportGridDtls.DataSource = DsGrd;
                ReportGridDtls.DataBind();
            }
            else
            {
                ReportGridDtls.DataSource = null;
                ReportGridDtls.DataBind();
            }
            DsGrd = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void CheckQty()
    {
        for (int i = 0; i < GrdInward.Rows.Count; i++)
        {
            if ((!string.IsNullOrEmpty(((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).Text) ? Convert.ToDecimal(((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).Text) : 0) <= 0)
            {
                chkflag = true;
            }
            else
            {
                chkflag = false;
                break;
            }
        }
    }

    private void BlockTextBox()
    {
        for (int i = 0; i < GrdInward.Rows.Count; i++)
        {
            if (Convert.ToDecimal(GrdInward.Rows[i].Cells[8].Text) <= (Convert.ToDecimal(GrdInward.Rows[i].Cells[9].Text) + Convert.ToDecimal(GrdInward.Rows[i].Cells[10].Text)))
            {
                ((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).ReadOnly = true;
                ((TextBox)GrdInward.Rows[i].FindControl("TxtReason")).ReadOnly = true;
            }
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MakeEmptyForm();
            CheckEmployeeName(lblPreparedBy);
            CheckUserRight();
        }
    }

    protected void ddlInwardNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DS = Obj_damage.GetInwardDtls(Convert.ToInt32(ddlInwardNo.SelectedValue), out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    lblInwardNo.Text = DS.Tables[0].Rows[0]["InwardNo"].ToString();
                    lblInwardDate.Text = DS.Tables[0].Rows[0]["InwardDate"].ToString();
                    lblSuppName.Text = DS.Tables[0].Rows[0]["SuplierName"].ToString();
                    lblpono.Text = DS.Tables[0].Rows[0]["PONo"].ToString();
                    lblPODATE.Text = DS.Tables[0].Rows[0]["PODate"].ToString();
                    lblQuotationNo.Text = DS.Tables[0].Rows[0]["POQTNO"].ToString();
                   lblQuotationDate.Text = DS.Tables[0].Rows[0]["POQTDATE"].ToString();                    
                }
                else
                {
                    lblInwardNo.Text = "";
                    lblInwardDate.Text = "";
                    lblSuppName.Text = "";
                }
                if (DS.Tables[1].Rows.Count > 0)
                {
                    FlagForCalculationOperation = false;
                    GrdInward.DataSource = DS.Tables[1];
                    GrdInward.DataBind();
                    BlockTextBox();
                    ((TextBox)GrdInward.Rows[0].FindControl("GrdtxtReturnQty")).Focus();
                }
                else
                {
                    SetInitialRow();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDtls = 0;
        try
        {
            CheckQty();
            if (chkflag == false)
            {
                Entity_damage.ReturnDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                Entity_damage.PreparedBy = Convert.ToInt32(Session["UserID"]);
                Entity_damage.InwardId = Convert.ToInt32(ddlInwardNo.SelectedValue);
                Entity_damage.UserID = Convert.ToInt32(Session["UserId"]);
                Entity_damage.LoginDate = DateTime.Now;
                if (chkReturnDebitNote.Checked)
                    Entity_damage.IsDebitNote = 1;
                else
                    Entity_damage.IsDebitNote = 0;
                InsertRow = Obj_damage.InsertRecord(ref Entity_damage,Convert.ToInt32(Session["CafeteriaId"].ToString()), out StrError);
                if (InsertRow > 0)
                {
                    if (ViewState["CurrentTable"] != null)
                    {
                        for (int i = 0; i < GrdInward.Rows.Count; i++)
                        {
                            if (((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).Text != "0" && ((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).Text != String.Empty)
                            {
                                Entity_damage.ReturnId = InsertRow;
                                Entity_damage.ItemId = Convert.ToInt32(GrdInward.Rows[i].Cells[2].Text.ToString());
                                Entity_damage.InwardQty = Convert.ToDecimal(GrdInward.Rows[i].Cells[8].Text.ToString());
                                Entity_damage.DamageQty = Convert.ToDecimal(GrdInward.Rows[i].Cells[9].Text.ToString());
                                Entity_damage.PrevReturnQty = Convert.ToDecimal(GrdInward.Rows[i].Cells[10].Text.ToString());
                                if (((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).Text == " " || string.IsNullOrEmpty(((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).Text))
                                {
                                    Entity_damage.ReturnQty = 0;
                                }
                                else
                                {
                                    Entity_damage.ReturnQty = Convert.ToDecimal(((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).Text);
                                }
                                Entity_damage.InwardRate = Convert.ToDecimal(GrdInward.Rows[i].Cells[12].Text.ToString());
                                Entity_damage.Reason = (((TextBox)GrdInward.Rows[i].FindControl("TxtReason")).Text);

                                //StockDetails
                                Entity_damage.ItemDesc = GrdInward.Rows[i].Cells[5].Text.ToString().Equals("&nbsp;")?"":Convert.ToString(GrdInward.Rows[i].Cells[5].Text.ToString());
                                Entity_damage.StockUnitId = Convert.ToInt32(GrdInward.Rows[i].Cells[6].Text.ToString());
                                Entity_damage.StockDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                Entity_damage.StockLocationID = Convert.ToInt32(Session["CafeteriaId"]);

                                if (chkReturnDebitNote.Checked)
                                    Entity_damage.IsDebitNote = 1;
                                else
                                    Entity_damage.IsDebitNote = 0;

                                InsertRowDtls = Obj_damage.InsertDetailsRecord(ref Entity_damage, out StrError);
                            }
                        }
                    }
                    if (InsertRow > 0)
                    {
                        obj_Comm.ShowPopUpMsg("Record Saved Successfully", this.Page);
                        MakeControlEmpty();
                        MakeEmptyForm();
                        Entity_damage = null;
                        Obj_damage = null;
                    }
                }
            }
            else
            {
                obj_Comm.ShowPopUpMsg("Please Enter Return Qty Atleast 1 Particular..", this.Page);
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, UpdateRowDtls = 0;
        try
        {
            CheckQty();
            if (chkflag == false)
            {
                if (ViewState["EditId"] != null)
                {
                    Entity_damage.ReturnId = Convert.ToInt32(ViewState["EditId"]);
                }
                Entity_damage.PreparedBy = Convert.ToInt32(Session["UserID"]);
                Entity_damage.UserID = Convert.ToInt32(Session["UserId"]);
                Entity_damage.LoginDate = DateTime.Now;
                if (chkReturnDebitNote.Checked)
                    Entity_damage.IsDebitNote = 1;
                else
                    Entity_damage.IsDebitNote = 0;
                UpdateRow = Obj_damage.UpdateRecord(ref Entity_damage, out StrError);
                if (UpdateRow > 0)
                {
                    if (ViewState["CurrentTable"] != null)
                    {
                        for (int i = 0; i < GrdInward.Rows.Count; i++)
                        {
                            if (((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).Text != "0" && ((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).Text != String.Empty)
                            {
                                Entity_damage.ReturnId = Convert.ToInt32(ViewState["EditId"]);
                                Entity_damage.ItemId = Convert.ToInt32(GrdInward.Rows[i].Cells[2].Text.ToString());
                                Entity_damage.InwardQty = Convert.ToDecimal(GrdInward.Rows[i].Cells[8].Text.ToString());
                                Entity_damage.DamageQty = Convert.ToDecimal(GrdInward.Rows[i].Cells[9].Text.ToString());
                                Entity_damage.PrevReturnQty = Convert.ToDecimal(GrdInward.Rows[i].Cells[10].Text.ToString());
                                if (((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).Text == " " || string.IsNullOrEmpty(((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).Text))
                                {
                                    Entity_damage.ReturnQty = 0;
                                }
                                else
                                {
                                    Entity_damage.ReturnQty = Convert.ToDecimal(((TextBox)GrdInward.Rows[i].FindControl("GrdtxtReturnQty")).Text);
                                }
                                Entity_damage.InwardRate = Convert.ToDecimal(GrdInward.Rows[i].Cells[12].Text.ToString());
                                Entity_damage.Reason = (((TextBox)GrdInward.Rows[i].FindControl("TxtReason")).Text);

                                //StockDetails
                                Entity_damage.ItemDesc = Convert.ToString(GrdInward.Rows[i].Cells[5].Text.ToString());
                                Entity_damage.StockUnitId = Convert.ToInt32(GrdInward.Rows[i].Cells[6].Text.ToString());
                                Entity_damage.StockDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                Entity_damage.StockLocationID = Convert.ToInt32(Session["CafeteriaId"]);
                                if (chkReturnDebitNote.Checked)
                                    Entity_damage.IsDebitNote = 1;
                                else
                                    Entity_damage.IsDebitNote = 0;
                                UpdateRowDtls = Obj_damage.InsertDetailsRecord(ref Entity_damage, out StrError);
                            }
                        }
                    }
                    if (UpdateRow > 0)
                    {
                        obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                        MakeControlEmpty();
                        MakeEmptyForm();
                        Entity_damage = null;
                        Obj_damage = null;
                    }
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeControlEmpty();
        MakeEmptyForm();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMReturn obj_Damage = new DMReturn();
        String[] SearchList = obj_Damage.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        BindReportGrid(TxtSearch.Text.Trim());
    }

    protected void ReportGridDtls_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        if (Convert.ToInt32(e.CommandArgument) != 0)
                        {
                            ViewState["EditId"] = Convert.ToInt32(e.CommandArgument);
                            DS = Obj_damage.GetRecordForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                            {
                                TxtReturnNo.Text = DS.Tables[0].Rows[0]["ReturnNo"].ToString();
                                lblReturndate.Text = DS.Tables[0].Rows[0]["ReturnDate"].ToString();
                                lblPreparedBy.Text = DS.Tables[0].Rows[0]["UserName"].ToString();// Session["UserName"].ToString();
                                lblInwardNo.Text = DS.Tables[0].Rows[0]["InwardNo"].ToString();
                                lblInwardDate.Text = DS.Tables[0].Rows[0]["InwardDate"].ToString();
                                lblSuppName.Text = DS.Tables[0].Rows[0]["SuplierName"].ToString();

                                lblpono.Text = DS.Tables[0].Rows[0]["PONo"].ToString();
                                lblPODATE.Text = DS.Tables[0].Rows[0]["PODate"].ToString();
                                lblQuotationNo.Text = DS.Tables[0].Rows[0]["POQTNO"].ToString();
                                lblQuotationDate.Text = DS.Tables[0].Rows[0]["POQTDATE"].ToString();
                                ddlInwardNo.Enabled = false;
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            if (DS.Tables[1].Rows.Count > 0)
                            {
                                FlagForCalculationOperation = true;
                                GrdInward.DataSource = DS.Tables[1];
                                GrdInward.DataBind();
                                ViewState["CurrentTable"] = DS.Tables[1];
                                ViewState["ReturnDtls"] = DS.Tables[1];
                            }
                            DS = null;
                            Obj_damage = null;
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                        }
                        break;
                    }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void ReportGridDtls_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int DeleteId = 0;
        try
        {
            DeleteId = Convert.ToInt32(((ImageButton)ReportGridDtls.Rows[e.RowIndex].Cells[0].FindControl("ImgBtnDelete")).CommandArgument.ToString());
            if (DeleteId != 0)
            {
                Entity_damage.ReturnId = DeleteId;
                Entity_damage.UserID = Convert.ToInt32(Session["UserID"]);
                Entity_damage.LoginDate = DateTime.Now;

                int iDelete = Obj_damage.DeleteRecord(ref Entity_damage, out StrError);
                if (iDelete != 0)
                {
                    obj_Comm.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_damage = null;
            Obj_damage = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void GrdtxtReturnQty_TextChanged(object sender, EventArgs e)
    {
        if (FlagForCalculationOperation == false)
        {
            if (Convert.ToDecimal(((GridViewRow)((TextBox)sender).Parent.Parent).Cells[8].Text) < ((Convert.ToDecimal(((GridViewRow)((TextBox)sender).Parent.Parent).Cells[9].Text)) + (Convert.ToDecimal(((GridViewRow)((TextBox)sender).Parent.Parent).Cells[10].Text)) + (!string.IsNullOrEmpty(((TextBox)sender).Text) ? Convert.ToDecimal(((TextBox)sender).Text) : 0)))
            {
                ((TextBox)sender).Focus();
                ((TextBox)sender).Text = "0.00";
                obj_Comm.ShowPopUpMsg("Can't Return More Quantity Than Inward Qty...", this.Page);
            }
            else
            {
                ((GridViewRow)((TextBox)sender).Parent.Parent).Cells[13].Text = ((!string.IsNullOrEmpty(((TextBox)sender).Text) ? Convert.ToDecimal(((TextBox)sender).Text) : 0) * (Convert.ToDecimal(((GridViewRow)((TextBox)sender).Parent.Parent).Cells[12].Text))).ToString("#0.00");
                ((GridViewRow)((TextBox)sender).Parent.Parent).FindControl("TxtReason").Focus();
            }
        }
        else
        {
            if (Convert.ToDecimal(((GridViewRow)((TextBox)sender).Parent.Parent).Cells[8].Text) < ((Convert.ToDecimal(((GridViewRow)((TextBox)sender).Parent.Parent).Cells[9].Text)) + (!string.IsNullOrEmpty(((TextBox)sender).Text) ? Convert.ToDecimal(((TextBox)sender).Text) : 0)))
            {
                ((TextBox)sender).Focus();
                ((TextBox)sender).Text = "0.00";
                obj_Comm.ShowPopUpMsg("Can't Return More Quantity Than Inward Qty...", this.Page);
            }
            else
            {
                ((GridViewRow)((TextBox)sender).Parent.Parent).Cells[13].Text = ((!string.IsNullOrEmpty(((TextBox)sender).Text) ? Convert.ToDecimal(((TextBox)sender).Text) : 0) * (Convert.ToDecimal(((GridViewRow)((TextBox)sender).Parent.Parent).Cells[12].Text))).ToString("#0.00");
                ((GridViewRow)((TextBox)sender).Parent.Parent).FindControl("TxtReason").Focus();
            }
        }
    }

    protected void ReportGridDtls_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.ReportGridDtls.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            string COND = string.Empty;
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                COND = COND + " AND RM.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            DS = Obj_damage.GetReturnDetails(StrCondition,COND, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                ReportGridDtls.DataSource = DS.Tables[0];
                this.ReportGridDtls.DataBind();
                //-----UserRights------
                if (FlagDel && FlagEdit)
                {
                    foreach (GridViewRow GRow in ReportGridDtls.Rows)
                    {
                        GRow.FindControl("ImgBtnDelete").Visible = false;
                        GRow.FindControl("ImageGridEdit").Visible = false;
                    }
                }
                else if (FlagDel)
                {
                    foreach (GridViewRow GRow in ReportGridDtls.Rows)
                    {
                        GRow.FindControl("ImgBtnDelete").Visible = false;
                    }
                }
                else
                {
                    foreach (GridViewRow GRow in ReportGridDtls.Rows)
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

    protected void BtnShowFilter_Click(object sender, EventArgs e)
    {
        try
        {
            string COND = string.Empty;
            if (Convert.ToInt32(ddlSite.SelectedValue) > 0)
            {
                COND = COND + " AND IRD.LocID=" + ddlSite.SelectedValue.ToString();
            }
            //if (!string.IsNullOrEmpty(TxtFromDate.Text))
            //{
            //    COND = COND + " AND IR.InwardDate>=" + (Convert.ToDateTime(TxtFromDate.Text.Trim())).ToShortDateString();
            //    if (!string.IsNullOrEmpty(TxtToDate.Text))
            //    {
            //        COND = COND + " AND IR.InwardDate<=" + Convert.ToDateTime(TxtToDate.Text.Trim()).ToShortDateString();
            //    }
            //    else
            //    {
            //        COND = COND + " AND IR.InwardDate<=" + DateTime.Now.ToShortDateString();
            //    }
            //}

            if (!string.IsNullOrEmpty(TxtFromDate.Text))
            {
                COND = COND + " AND CAST(CONVERT(NVARCHAR(20),IR.InwardDate,101) as DATETIME)>=CAST(CONVERT(NVARCHAR(20),CAST('" + (Convert.ToDateTime(TxtFromDate.Text.Trim())).ToString("MM-dd-yyyy") + "'AS DATETIME) ,101) as DATETIME)";
                if (!string.IsNullOrEmpty(TxtToDate.Text))
                {
                    COND = COND + " AND CAST(CONVERT(NVARCHAR(20),IR.InwardDate,101) as DATETIME)<=CAST(CONVERT(NVARCHAR(20),CAST('" + Convert.ToDateTime(TxtToDate.Text.Trim()).ToString("MM-dd-yyyy") + "'AS DATETIME) ,101) as DATETIME)";
                }
                else
                {
                    COND = COND + " AND CAST(CONVERT(NVARCHAR(20),IR.InwardDate,101) as DATETIME)<=CAST(CONVERT(NVARCHAR(20),CAST('" + DateTime.Now.ToString("MM-dd-yyyy") + "'AS DATETIME) ,101) as DATETIME)";
                }
            }
            DS = Obj_damage.FillComboONCOND(COND, out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlInwardNo.DataSource = DS.Tables[0];
                    ddlInwardNo.DataTextField = "InwardNo";
                    ddlInwardNo.DataValueField = "InwardId";
                    ddlInwardNo.DataBind();
                }
               
            }
        }
        catch (Exception ex)
        {
            obj_Comm.ShowPopUpMsg(ex.Message,this.Page);
        }
    }
}
