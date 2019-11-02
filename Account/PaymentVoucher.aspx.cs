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

using System.Collections.Generic;
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using MayurInventory.DALSQLHelper;
public partial class Account_PaymentVoucher : System.Web.UI.Page
{
    #region[Variables]
    DMPaymentVoucher Obj_Call = new DMPaymentVoucher();
    PaymentVoucher Entity_Call = new PaymentVoucher();
    CommanFunction CommFun = new CommanFunction();
    DMLedgerReport obj_Ledger = new DMLedgerReport();
    DataSet Ds = new DataSet();
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    decimal Amt = (decimal)0.00;
    string TotalAmt;
    public static int StateId = 0, LedgerId = 0;
    public static decimal Outstanding = 0;
    public static decimal OutStdBalance = 0, Debit = 0, Credit = 0, Balance = 0;
    private static bool FlagAdd = false, FlagDel = false, FlagEdit = false, FlagPrint = false;
    #endregion

    #region[User Defined Function]
    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("InvoiceId", typeof(int)));
            dt.Columns.Add(new DataColumn("InvoiceNo", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Outstanding", typeof(decimal)));
            dt.Columns.Add(new DataColumn("ReceivedAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("OutstandingSample", typeof(decimal)));
            dt.Columns.Add(new DataColumn("ReceivedAmtSample", typeof(decimal))); 
            

            dr = dt.NewRow();

            dr["InvoiceId"] = 0;
            dr["InvoiceNo"] = "";

            dr["InvoiceAmt"] = 0;
            dr["Outstanding"] = 0;
            dr["ReceivedAmt"] = 0;
            dr["OutstandingSample"] = 0;
            dr["ReceivedAmtSample"] = 0;
            

            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
            GridInvoice.DataSource = dt;
            GridInvoice.DataBind();

            dt = null;
            dr = null;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void MakeEmptyForm()
    {
        ViewState["EditID"] = null;

        txtDate.Focus();
        if (!FlagAdd)
            BtnSave.Visible = true;
        BtnUpdate.Visible = false;

        //BtnPrint.Visible = false;

        txtVoucherNo.Text = string.Empty;
        txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        ddlLedgerNameTO.SelectedValue = "0";
        ddlLedgerNameFrom.SelectedValue = "0";
        txtCrAmt.Text = "0";
        txtDrAmt.Text = "0";
        txtAmtInWrds.Text = string.Empty;
        TxtTotalCredit.Text = string.Empty;
        TxtTotalDebit.Text = string.Empty;
        txtNarration.Text = string.Empty;

        TxtSearch.Text = string.Empty;
        GetVoucherNo();
        ReportGrid(StrCondition);
        lblOutstandingName.Visible = false;
        MakeControlEmpty();
        SetInitialRow();
        for (int i = 0; i < rdAmtType.Items.Count; i++)
        {
            rdAmtType.Items[i].Selected = false;
        }
        rdAmtType.Enabled = true;
        //ViewState["Name"]=null;
        //ViewState["Date"]=null;
        //ViewState["Amount"] = null;
        

    }
    private void ReportGrid(string strCondition)
    {
        try
        {
            Ds = Obj_Call.GetCreditNote(strCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                GrdReport.DataSource = Ds.Tables[0];
                GrdReport.DataBind();
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }
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
            Ds = Obj_Call.FillComboForGroup();

            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    //---PaymentFrom
                    ddlLedgerNameFrom.DataSource = Ds.Tables[0];
                    ddlLedgerNameFrom.DataValueField = "LedgerID";
                    ddlLedgerNameFrom.DataTextField = "LedgerName";
                    ddlLedgerNameFrom.DataBind();
                }
                if (Ds.Tables[1].Rows.Count > 0)
                {
                    //---PaymentTo
                    ddlLedgerNameTO.DataSource = Ds.Tables[1];
                    ddlLedgerNameTO.DataValueField = "LedgerID";
                    ddlLedgerNameTO.DataTextField = "LedgerName";
                    ddlLedgerNameTO.DataBind();
                }
                
                if (Ds.Tables[2].Rows.Count > 0)
                {
                    //---PaymentTo
                    ddlsite.DataSource = Ds.Tables[2];
                    ddlsite.DataValueField = "StockLocationID";
                    ddlsite.DataTextField = "Location";
                    ddlsite.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void GetVoucherNo()
    {
        try
        {
            DataSet DsCode = new DataSet();
            DsCode = Obj_Call.GetVoucherNo(out StrError);
            if (DsCode.Tables[0].Rows.Count > 0)
            {
                txtVoucherNo.Text = DsCode.Tables[0].Rows[0]["VoucherNo"].ToString();
                txtVoucherNo.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private bool IsValidate()
    {
        try
        {
            if (ddlLedgerNameFrom.SelectedValue == "0")
            {
                ddlLedgerNameFrom.Focus();
                return false;
            }
            else if (ddlLedgerNameTO.SelectedValue == "0")
            {
                ddlLedgerNameTO.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtCrAmt.Text) || Convert.ToDecimal(txtCrAmt.Text) <= 0)
            {
                txtCrAmt.Focus();
                return false;
            }
            else if (Convert.ToInt32(ddlLedgerNameFrom.SelectedValue).Equals(Convert.ToInt32(ddlLedgerNameTO.SelectedValue)))
            {
                ddlLedgerNameFrom.Focus();
                return false;
            }
            else if (rdAmtType.SelectedValue == "0")
            {
                if (GridInvoice.Rows.Count == 0 || string.IsNullOrEmpty(GridInvoice.Rows[0].Cells[1].Text))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    private void AmtInWrd()
    {
        Amt = Convert.ToDecimal(txtDrAmt.Text);
        TotalAmt = WordAmount.convertcurrency(Amt);
        txtAmtInWrds.Text = TotalAmt.ToString();
    }

    private void GetEditRecordList()
    {
        if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
        {
            txtDate.Text = Convert.ToDateTime(Convert.ToDateTime(Ds.Tables[0].Rows[0]["VoucherDate"]).ToShortDateString()).ToString("dd-MMM-yyy");
            txtVoucherNo.Text = Ds.Tables[0].Rows[0]["VoucherNo"].ToString();
            ddlLedgerNameTO.SelectedValue = Ds.Tables[0].Rows[0]["VoucherDebit"].ToString();
            ddlLedgerNameFrom.Text = Ds.Tables[0].Rows[0]["VoucherCredit"].ToString();
            txtCrAmt.Text = Ds.Tables[0].Rows[0]["VoucherAmount"].ToString();
            txtDrAmt.Text = Ds.Tables[0].Rows[0]["VoucherAmount"].ToString();
            txtNarration.Text = Ds.Tables[0].Rows[0]["Narration"].ToString();
            TxtTotalCredit.Text = Ds.Tables[0].Rows[0]["VoucherAmount"].ToString();
            TxtTotalDebit.Text = Ds.Tables[0].Rows[0]["VoucherAmount"].ToString();
            TxtSearch.Text = string.Empty;

            AmtInWrd();
            ViewState["Name"] = ddlLedgerNameTO.SelectedItem.Text;
            ViewState["Amount"] = txtCrAmt.Text;
            ViewState["Date"] = txtDate.Text;

            string str = Ds.Tables[0].Rows[0]["Narration"].ToString();
            if (str.StartsWith("Ref:"))
            {
                rdAmtType.SelectedValue = "0";
            }
            if (str.StartsWith("On Account:"))
            {
                rdAmtType.SelectedValue = "1";
            }
            if (str.StartsWith("Advance:"))
            {
                rdAmtType.SelectedValue = "2";
            }
            if (Convert.ToBoolean(Ds.Tables[0].Rows[0]["IsReference"]) == true)
            {
                GridInvoice.Visible = true;
                if (Ds.Tables[1].Rows.Count > 0)
                {
                    GridInvoice.DataSource = Ds.Tables[1];
                    GridInvoice.DataBind();
                    ViewState["CurrentTable"] = Ds.Tables[1];
                }
            }
            else
            {
                GridInvoice.Visible = false;
            }

            lblOutstandingName.Visible = true;
            StrCondition = StrCondition + "AND (CAST(FLOOR(CAST(VoucherDate as FLOAT)) AS DateTime))<= '" + Convert.ToDateTime(DateTime.Now).ToString("MM-dd-yyyy") + "'";
            Ds = obj_Ledger.GetdetailsLedgerIdOther1n2(Convert.ToInt32(ddlLedgerNameTO.SelectedValue), StrCondition, out StrError);

            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                Debit = Convert.ToDecimal(Ds.Tables[0].Rows[0]["Debit"].ToString());
                Credit = Convert.ToDecimal(Ds.Tables[0].Rows[0]["Credit"].ToString());
                Balance = Debit - Credit;
                if (Balance < 0)
                {
                    Balance = Math.Abs(Balance);
                    //lblOutstandingName.Text = "OutStanding Limit: " + Outstanding.ToString("0.00") + " OutStanding Balance: " + Balance.ToString("0.00") + " CR";
                    lblOutstandingName.Text = " OutStanding Balance: " + Balance.ToString("0.00") + " CR";
                }
                else
                {
                    //lblOutstandingName.Text = "OutStanding Limit: " + Outstanding.ToString("0.00") + " OutStanding Balance: " + Balance.ToString("0.00") + " DR";
                    lblOutstandingName.Text = " OutStanding Balance: " + Balance.ToString("0.00") + " DR";

                }

            }
            else
            {
                GridInvoice.Visible = false;
            }
            rdAmtType.Enabled = false;
        }
        else
        {
            MakeEmptyForm();
        }
        if (!FlagEdit)
            BtnUpdate.Visible = true;


        BtnSave.Visible = false;
        BtnCancel.Visible = true;
    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCombo();
           MakeEmptyForm();
        }
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, InserDetail = 0;
        decimal amt = 0;
        try
        {
            if (IsValidate() == true)
            {
                if (ViewState["EditID"] != null)
                {
                    Entity_Call.VoucherId = Convert.ToInt32(ViewState["EditID"]);
                }

                Entity_Call.VoucherType = 'P';
                // Entity_Call.VoucherTypeId = Convert.ToInt32(txtVoucherNo.Text);
                Entity_Call.VoucherDate = (!string.IsNullOrEmpty(txtDate.Text)) ? Convert.ToDateTime(txtDate.Text) : Convert.ToDateTime("01/01/1753");
                Entity_Call.VoucherDebit = Convert.ToInt32(ddlLedgerNameTO.SelectedValue);
                Entity_Call.VoucherCredit = Convert.ToInt32(ddlLedgerNameFrom.SelectedValue);
                Entity_Call.VoucherAmount = Convert.ToDecimal(txtCrAmt.Text);
                Entity_Call.VoucherNarration = txtNarration.Text;
                Entity_Call.InvoiceCode = 0;
                if (rdAmtType.SelectedValue == "0")
                {
                    for (int i = 0; i < GridInvoice.Rows.Count; i++)
                    {
                        TextBox Txt_ReceivedAmt = (TextBox)GridInvoice.Rows[i].Cells[5].FindControl("ReceivedAmt");
                        amt += Convert.ToDecimal(Txt_ReceivedAmt.Text);
                    }
                    if (amt != Convert.ToDecimal(txtCrAmt.Text))
                    {
                        CommFun.ShowPopUpMsg("Total Received Amount should be equal to Credit amount", this.Page);
                        txtCrAmt.Focus();
                        return;
                    }
                    Entity_Call.IsReference = true;
                }
                else
                {
                    Entity_Call.IsReference = false;
                }
                AmtInWrd();

                Entity_Call.LoginID = Convert.ToInt32(Session["UserID"]);
                Entity_Call.LoginDate = DateTime.Now;

                UpdateRow = Obj_Call.UpdatePaymentVoucher(ref Entity_Call, out StrError);

                if (UpdateRow != 0)
                {
                    for (int i = 0; i < GridInvoice.Rows.Count; i++)
                    {
                        TextBox Txt_Outstanding = (TextBox)GridInvoice.Rows[i].Cells[3].FindControl("Outstanding");
                        TextBox Txt_ReceivedAmt = (TextBox)GridInvoice.Rows[i].Cells[4].FindControl("ReceivedAmt");
                        if (Convert.ToDecimal(Txt_ReceivedAmt.Text) != 0)
                        {
                            Entity_Call.VoucherId = Convert.ToInt32(ViewState["EditID"]);
                            Entity_Call.InvoiceCode = Convert.ToInt32(GridInvoice.Rows[i].Cells[0].Text);
                            Entity_Call.VoucherAmount = Convert.ToDecimal(GridInvoice.Rows[i].Cells[2].Text);
                            Entity_Call.OutStanding = Convert.ToDecimal(Txt_Outstanding.Text);
                            Entity_Call.ReceivedAmt = Convert.ToDecimal(Txt_ReceivedAmt.Text);

                            InserDetail = Obj_Call.InsertVoucherDetail(ref Entity_Call, out StrError);
                        }
                    }
                    if (rdAmtType.SelectedValue == "0")
                    {
                        if (UpdateRow > 0 && InserDetail > 0)
                        {
                            CommFun.ShowPopUpMsg("Record Updated Successfully", this.Page);
                            MakeEmptyForm();
                        }
                    }
                    else
                    {
                        CommFun.ShowPopUpMsg("Record Updated Successfully", this.Page);
                        MakeEmptyForm();
                    }
                }
            }
            else
            {
                CommFun.ShowPopUpMsg("Please Fill All Information Properly ...!", this.Page);
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
        DMPaymentVoucher Obj_CN = new DMPaymentVoucher();
        String[] SearchList = Obj_CN.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InserDetail = 0;
        decimal amt = 0;
        try
        {
            if (IsValidate() == true)
            {
                Entity_Call.VoucherType = 'P';
                Entity_Call.VoucherTypeId = Convert.ToInt32(txtVoucherNo.Text);
                Entity_Call.VoucherDate = (!string.IsNullOrEmpty(txtDate.Text)) ? Convert.ToDateTime(txtDate.Text) : Convert.ToDateTime("01/01/1753");
                Entity_Call.VoucherDebit = Convert.ToInt32(ddlLedgerNameTO.SelectedValue);
                Entity_Call.VoucherCredit = Convert.ToInt32(ddlLedgerNameFrom.SelectedValue);
                Entity_Call.LocationId = Convert.ToInt32(ddlsite.SelectedValue);
                Entity_Call.VoucherAmount = Convert.ToDecimal(txtCrAmt.Text);
                Entity_Call.VoucherNarration = txtNarration.Text;
                Entity_Call.InvoiceCode = 0;
                if (rdAmtType.SelectedValue == "0")
                {
                    for (int i = 0; i < GridInvoice.Rows.Count; i++)
                    {
                        TextBox Txt_ReceivedAmt = (TextBox)GridInvoice.Rows[i].Cells[5].FindControl("ReceivedAmt");
                        amt += Convert.ToDecimal(Txt_ReceivedAmt.Text);
                    }
                    if (amt != Convert.ToDecimal(txtCrAmt.Text))
                    {
                        CommFun.ShowPopUpMsg("Total Received Amount should be equal to Credit amount", this.Page);
                        txtCrAmt.Focus();
                        return;
                    }
                    Entity_Call.IsReference = true;
                }
                else
                {
                    Entity_Call.IsReference = false;
                }
                Entity_Call.LoginID = Convert.ToInt32(Session["UserID"]);
                Entity_Call.LoginDate = DateTime.Now;

                ViewState["Name"] = ddlLedgerNameTO.SelectedItem.Text;
                ViewState["Date"] = Convert.ToDateTime(txtDate.Text).ToString("dd-MMM-yyy");
                ViewState["Amount"] = txtCrAmt.Text;

                InsertRow = Obj_Call.InsertPaymentVoucher(ref Entity_Call, out StrError);

                if (InsertRow != 0)
                {
                    for (int i = 0; i < GridInvoice.Rows.Count; i++)
                    {
                        TextBox Txt_Outstanding = (TextBox)GridInvoice.Rows[i].Cells[3].FindControl("Outstanding");
                        TextBox Txt_ReceivedAmt = (TextBox)GridInvoice.Rows[i].Cells[4].FindControl("ReceivedAmt");
                        if (Convert.ToDecimal(Txt_ReceivedAmt.Text) != 0)
                        {
                            Entity_Call.VoucherId = InsertRow;
                            Entity_Call.InvoiceCode = Convert.ToInt32(GridInvoice.Rows[i].Cells[0].Text);
                            Entity_Call.VoucherAmount = Convert.ToDecimal(GridInvoice.Rows[i].Cells[2].Text);
                            Entity_Call.OutStanding = Convert.ToDecimal(Txt_Outstanding.Text);
                            Entity_Call.ReceivedAmt = Convert.ToDecimal(Txt_ReceivedAmt.Text);

                            InserDetail = Obj_Call.InsertVoucherDetail(ref Entity_Call, out StrError);
                        }
                    }
                    if (rdAmtType.SelectedValue == "0")
                    {
                        if (InsertRow > 0 && InserDetail > 0)
                        {
                            CommFun.ShowPopUpMsg("Record Saved Successfully", this.Page);
                            MakeEmptyForm();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintAftersave", "PrintAftersave();", true);
                        }
                    }
                    else
                    {
                        CommFun.ShowPopUpMsg("Record Saved Successfully", this.Page);
                        MakeEmptyForm();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintAftersave", "PrintAftersave();", true);
                   
                    }
                }
            }
            else
            {
                CommFun.ShowPopUpMsg("Please Fill All Information Properly ...!", this.Page);
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
    protected void ddlLedgerNameTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ToDate = string.Empty;
            string FromDate = string.Empty;
            int LedgerGroupId = 0;
            lblOutstandingName.Visible = true;
           
            StrCondition = StrCondition + "AND (CAST(FLOOR(CAST(VoucherDate as FLOAT)) AS DateTime))<= '" + Convert.ToDateTime(DateTime.Now).ToString("MM-dd-yyyy") + "'";
            Ds = obj_Ledger.GetdetailsLedgerIdOther1n2(Convert.ToInt32(ddlLedgerNameTO.SelectedValue), StrCondition, out StrError);

            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                Debit = Convert.ToDecimal(Ds.Tables[0].Rows[0]["Debit"].ToString());
                Credit = Convert.ToDecimal(Ds.Tables[0].Rows[0]["Credit"].ToString());
                Balance = Debit - Credit;
                if (Balance < 0)
                {
                    Balance = Math.Abs(Balance);
                    //lblOutstandingName.Text = "OutStanding Limit: " + Outstanding.ToString("0.00") + " OutStanding Balance: " + Balance.ToString("0.00") + " CR";
                    lblOutstandingName.Text = " OutStanding Balance: " + Balance.ToString("0.00") + " CR";
                }
                else
                {
                    //lblOutstandingName.Text = "OutStanding Limit: " + Outstanding.ToString("0.00") + " OutStanding Balance: " + Balance.ToString("0.00") + " DR";
                    lblOutstandingName.Text = " OutStanding Balance: " + Balance.ToString("0.00") + " DR";

                }

            }

            if (rdAmtType.SelectedValue == "0")
            {
                if (Convert.ToInt32(ddlLedgerNameTO.SelectedValue) > 0)
                {
                    Ds = Obj_Call.GetoutStanding(Convert.ToInt32(ddlLedgerNameTO.SelectedValue), out StrError);

                    if (Ds.Tables.Count > 0&& Ds.Tables[0].Rows.Count>0)
                    {
                        GridInvoice.DataSource = Ds.Tables[0];
                        GridInvoice.DataBind();
                        GridInvoice.Visible = true;
                    }
                    else
                    {
                        CommFun.ShowPopUpMsg("There is no invoice for selected supplier", this.Page);
                        for (int i = 0; i < rdAmtType.Items.Count; i++)
                        {
                            rdAmtType.Items[i].Selected = false;
                        }
                        rdAmtType.Focus();
                    }
                }
            }
            if (rdAmtType.SelectedValue == "1")
            {
                txtNarration.Text = "On Account:";
                txtNarration.Focus();
                MakeControlEmpty();

            }
            if (rdAmtType.SelectedValue == "2")
            {
                txtNarration.Text = "Advance:";
                txtNarration.Focus();
                MakeControlEmpty();
            }

        }
        catch (Exception ex)
        {
            CommFun.ShowPopUpMsg(ex.Message,this.Page);
        }
    }
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            StrCondition = string.Empty;
            StrCondition = TxtSearch.Text.Trim();
            StrCondition = StrCondition.Replace("[", @"\[");
            Ds = new DataSet();
            Ds = Obj_Call.GetCreditNote(StrCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                GrdReport.DataSource = Ds.Tables[0];
                GrdReport.DataBind();
                Ds = null;
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void GridInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridInvoice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void rdAmtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdAmtType.SelectedValue == "0")
            {
                if (Convert.ToInt32(ddlLedgerNameTO.SelectedValue) > 0)
                {
                    txtNarration.Text = "Ref:";
                    txtNarration.Focus();

                    Ds = Obj_Call.GetoutStanding(Convert.ToInt32(ddlLedgerNameTO.SelectedValue), out StrError);

                    if (Ds.Tables.Count > 0&&Ds.Tables[0].Rows.Count>0)
                    {
                        GridInvoice.DataSource = Ds.Tables[0];
                        GridInvoice.DataBind();
                        GridInvoice.Visible = true;
                    }
                    else
                    {
                        CommFun.ShowPopUpMsg("There is no invoice for selected supplier", this.Page);
                        for (int i = 0; i < rdAmtType.Items.Count; i++)
                        {
                            rdAmtType.Items[i].Selected = false;
                        }
                        rdAmtType.Focus();
                        GridInvoice.Visible = false;
                    }

                }
                else
                {
                    txtNarration.Text = "Ref:";
                    txtNarration.Focus();
                }
            }
            if (rdAmtType.SelectedValue == "1")
            {
                txtNarration.Text = "On Account:";
                txtNarration.Focus();
                MakeControlEmpty();
               
            }
            if (rdAmtType.SelectedValue == "2")
            {
                txtNarration.Text = "Advance:";
                txtNarration.Focus();
                MakeControlEmpty();
            }
        }
        catch (Exception ex)
        {
            CommFun.ShowPopUpMsg(ex.Message,this.Page);
        }
    }

    private void MakeControlEmpty()
    {
        GridInvoice.Visible = false;
        GridInvoice.DataSource = null;
        GridInvoice.DataBind();
    }
    protected void GrdReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        StrCondition = TxtSearch.Text;
        StrCondition = StrCondition.Replace("[", @"\[");
        GrdReport.PageIndex = e.NewPageIndex;
        ReportGrid(StrCondition);
    }
    protected void GrdReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        if (Convert.ToInt32(e.CommandArgument) != 0)
                        {
                            ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);

                            Ds = Obj_Call.GetCreditForEdit(Convert.ToInt32(e.CommandArgument), out StrError);

                            GetEditRecordList();
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
    protected void GrdReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int DeleteId = Convert.ToInt32(((ImageButton)GrdReport.Rows[e.RowIndex].Cells[0].FindControl("ImgBtnDelete")).CommandArgument.ToString());
            Entity_Call.VoucherId = Convert.ToInt32(DeleteId);

            Entity_Call.LoginID = Convert.ToInt32(Session["UserID"]);
            Entity_Call.LoginDate = DateTime.Now;

            int iDelete = Obj_Call.DeletePaymentVoucher(ref Entity_Call, out StrError);

            if (iDelete != 0)
            {
                CommFun.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                MakeEmptyForm();
            }
            Entity_Call = null;
            Obj_Call = null;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
