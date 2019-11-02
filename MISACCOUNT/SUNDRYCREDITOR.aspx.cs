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
using System.Threading;
using System.Globalization;

public partial class MISACCOUNT_SUNDRYCREDITOR : System.Web.UI.Page
{
    #region [Private Variables]

    CommanFunction obj_Comman = new CommanFunction();
    DateTimeFormatInfo mfi = new DateTimeFormatInfo();
    DataSet DS = new DataSet();
    private string StrCondition = string.Empty;
    private string StrConditionFilters = string.Empty;
    private string RepCond = string.Empty;
    private string StrError = string.Empty;
    DMSundryCreditorReport obj_Ledger = new DMSundryCreditorReport();
    decimal CrAmt = 0, DrAmt = 0;
    decimal CrAmtMonth = 0, DrAmtMonth = 0;
    decimal CrAmtMonthDtls = 0, DrAmtMonthDtls = 0;
    string CloAmtMonth = "";
    public static int LedgerId = 0;
    string ToDate = string.Empty;
    string FromDate = string.Empty;
    string StDate = string.Empty;
    string EdDate = string.Empty;
    public static int NoOfMonths = 0;
    private static bool FlagPrint = false;

    #endregion

    #region[User Defined Functions]

    private void MakeFormEmpty()
    {
        LblRecordCount.Visible = false;

        SetInitialRow();
        SetInitialRowLedgerSummary();
        SetInitialRowLedgerVoucher();

        EnableDtp(false);

        BtnPrint.Visible = false;
        TR1.Visible = TR2.Visible = false;
        Label1.Text = Label2.Text = Label3.Text = "";
        LblFrmDate.Text = LblToDate.Text = "";
        LblFrmDate.Visible = LblToDate.Visible = TO.Visible = false;
        TR0.Visible = true;
        TR1.Visible = TR2.Visible = false;
        ViewState["LedgerId1"] = null;
        ViewState["LedgerId2"] = null;
        ViewState["LedgerId3"] = null;
        ChkDate.Focus();
        txtFromDate.Text ="01-"+DateTime.Now.ToString("MM-yyyy");
        txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        //ViewState["GridView"] = null;
        //ViewState["LedgerVoucher"] = null;
        //ViewState["LedgerSummary"] = null;
    }

    public void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("#", typeof(int)));
            dt.Columns.Add(new DataColumn("Particulars", typeof(string)));
            dt.Columns.Add(new DataColumn("Debit", typeof(string)));
            dt.Columns.Add(new DataColumn("Credit", typeof(string)));
            dt.Columns.Add(new DataColumn("LedgerID", typeof(string)));

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["Particulars"] = "";
            dr["Debit"] = "";
            dr["Credit"] = "";
            dr["LedgerID"] = "";

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GrdReport.DataSource = dt;
            GrdReport.DataBind();
            dt = null;
            dr = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void SetInitialRowLedgerSummary()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("#", typeof(int)));
            dt.Columns.Add(new DataColumn("Particulars", typeof(string)));
            dt.Columns.Add(new DataColumn("Debit", typeof(string)));
            dt.Columns.Add(new DataColumn("Credit", typeof(string)));
            dt.Columns.Add(new DataColumn("Closing", typeof(string)));
            dt.Columns.Add(new DataColumn("Closing1", typeof(string)));
            dt.Columns.Add(new DataColumn("LedgerID", typeof(string)));
            dt.Columns.Add(new DataColumn("ForMonth", typeof(string)));

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["Particulars"] = "";
            dr["Debit"] = "";
            dr["Credit"] = "";
            dr["Closing"] = "";
            dr["Closing1"] = "";
            dr["LedgerID"] = "";
            dr["ForMonth"] = "";
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GrdLedgersummary.DataSource = dt;
            GrdLedgersummary.DataBind();
            dt = null;
            dr = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void SetInitialRowLedgerVoucher()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("#", typeof(int)));
            dt.Columns.Add(new DataColumn("VchDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Particulars", typeof(string)));
            dt.Columns.Add(new DataColumn("VoucherType", typeof(string)));
            dt.Columns.Add(new DataColumn("VoucherID", typeof(string)));
            dt.Columns.Add(new DataColumn("Debit", typeof(string)));
            dt.Columns.Add(new DataColumn("Credit", typeof(string)));

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["VchDate"] = "";
            dr["Particulars"] = "";
            dr["VoucherType"] = "";
            dr["VoucherID"] = "";
            dr["Debit"] = "";
            dr["Credit"] = "";

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GrdLedgerVoucher.DataSource = dt;
            GrdLedgerVoucher.DataBind();
            dt = null;
            dr = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void ReportGrid()
    {
        try
        {
            StrCondition = string.Empty;
            StrConditionFilters = string.Empty;

            if (ChkDate.Checked == true)
            {
                ToDate = Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy");
                FromDate = Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy");
                StrCondition = StrCondition + " AND (CAST(FLOOR(CAST(VoucherDate as FLOAT)) AS DateTime))<='" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "'";
                Session["LedgerDate"] = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MM-yyyy") + "*" + Convert.ToDateTime(txtToDate.Text).ToString("dd-MM-yyyy");
                StrConditionFilters = "FromDate : " + txtFromDate.Text + "    ToDate : " + txtToDate.Text + "";
            }
            else
            {

                ToDate = Convert.ToDateTime(Session["FinEndDate"].ToString()).ToString("dd-MM-yyyy");
                FromDate = Convert.ToDateTime(Session["FinStartDate"].ToString()).ToString("dd-MM-yyyy");

                if (DateTime.Now > Convert.ToDateTime("03/31/" + DateTime.Now.ToString("yyyy")))
                    StrCondition = StrCondition + " AND (CAST(FLOOR(CAST(VoucherDate as FLOAT)) AS DateTime))<= '03-31-" + DateTime.Now.AddYears(1).ToString("yyyy") + "'";
                else
                    StrCondition = StrCondition + " AND (CAST(FLOOR(CAST(VoucherDate as FLOAT)) AS DateTime))<= '04-01-" + DateTime.Now.ToString() + "'";

                Session["LedgerDate"] = Convert.ToDateTime(Session["FinStartDate"].ToString()).ToString("dd-MM-yyyy") + "*" + Convert.ToDateTime(Session["FinEndDate"].ToString()).ToString("dd-MM-yyyy");
            }
            ViewState["Condition"] = StrCondition;
            ViewState["ConditionFilter"] = StrConditionFilters;

            LedgerId = 25;
            ViewState["LedgerId"] = LedgerId;
            DS = obj_Ledger.Getdetails("25", StrCondition, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GrdReport.DataSource = DS.Tables[0];
                GrdReport.DataBind();
                ViewState["GridView"] = DS.Tables[0];
                if (!FlagPrint)
                {
                    BtnPrintGroupSummary.Visible = true;
                    BtnPrint.Visible = true;
                    BtnExport.Visible = true;
                }
                LblRecordCount.Visible = true;
                LblRecordCount.Text = DS.Tables[0].Rows.Count + " Record(s) Found!!";
                Label1.Visible = true;
                Label1.Text = "KARIA " + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy") + " - " + Convert.ToDateTime(txtToDate.Text).ToString("yyyy");
                TR1.Visible = TR2.Visible = false;
                LblFrmDate.Visible = LblToDate.Visible = TO.Visible = true;
                LblFrmDate.Text = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MM-yyyy");
                LblToDate.Text = Convert.ToDateTime(txtToDate.Text).ToString("dd-MM-yyyy");
                Label3.Text = "Group Summary";
                Label2.Text = "";
               
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
                MakeFormEmpty();
                LblRecordCount.Text = "No Record Found!!";
                LblRecordCount.Visible = true;
                TR1.Visible = TR2.Visible = false;
            }
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void FindOpeningBalance(int LedgerId)
    {
        DataTable DTLedger = new DataTable();
        DataRow dr = null;
        DTLedger.Columns.Add(new DataColumn("#", typeof(int)));
        DTLedger.Columns.Add(new DataColumn("Particulars", typeof(string)));
        DTLedger.Columns.Add(new DataColumn("Debit", typeof(string)));
        DTLedger.Columns.Add(new DataColumn("Credit", typeof(string)));
        DTLedger.Columns.Add(new DataColumn("Closing1", typeof(string)));
        DTLedger.Columns.Add(new DataColumn("Closing", typeof(string)));
        DTLedger.Columns.Add(new DataColumn("ForMonth", typeof(string)));
        DTLedger.Columns.Add(new DataColumn("LedgerID", typeof(string)));
        dr = DTLedger.NewRow();
        try
        {
            StrCondition = string.Empty;
            RepCond = string.Empty;
            if (ChkDate.Checked == true)
            {
                ToDate = Convert.ToDateTime(txtToDate.Text).ToString("dd-MM-yyyy");
                FromDate = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MM-yyyy");
                StrCondition = StrCondition + "AND (CAST(FLOOR(CAST(VoucherDate as FLOAT)) AS DateTime))<= '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' ";
            }
            else
            {
                ToDate = Convert.ToDateTime(Session["FinEndDate"].ToString()).ToString("dd-MM-yyyy");
                FromDate = Convert.ToDateTime(Session["FinStartDate"].ToString()).ToString("dd-MM-yyyy");
                if (DateTime.Now > Convert.ToDateTime("31/03/" + DateTime.Now.ToString("yyyy")))
                    StrCondition = StrCondition + " AND  (CAST(FLOOR(CAST(VoucherDate as FLOAT)) AS DateTime))<= '04/01/" + DateTime.Now.ToString("yyyy") + "'";
                else
                    StrCondition = StrCondition + " AND (CAST(FLOOR(CAST(VoucherDate as FLOAT)) AS DateTime))<= '04/01" + DateTime.Now.AddYears(-1).ToString() + "'";

            }
            RepCond = LedgerId.ToString();
            DS = obj_Ledger.FindOpeningBal(RepCond, StrCondition, out StrError);
            //-----Set Opening Balance-----
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GrdLedgersummary.Rows[0].Cells[2].Text = "Opening balance";
                dr["#"] = 0;
                dr["Particulars"] = "Opening balance";
                dr["Debit"] = DS.Tables[0].Rows[0]["Debit"].ToString();
                dr["Credit"] = DS.Tables[0].Rows[0]["Credit"].ToString();
                dr["Closing1"] = DS.Tables[0].Rows[0]["Closing1"].ToString();
                dr["Closing"] = DS.Tables[0].Rows[0]["Closing"].ToString();
                dr["ForMonth"] = DS.Tables[0].Rows[0]["ForMonth"].ToString();
                dr["LedgerID"] = DS.Tables[0].Rows[0]["LedgerID"].ToString();
                DTLedger.Rows.Add(dr);
            }
            //-----For No. Of Months-----
            //NoOfMonths = 12 * ((Convert.ToDateTime(txtToDate.Text)).Year - (Convert.ToDateTime(txtFromDate.Text)).Year) + (Convert.ToDateTime(txtToDate.Text)).Month - (Convert.ToDateTime(txtFromDate.Text)).Month;
            NoOfMonths = 12 * ((Convert.ToDateTime(ToDate)).Year - (Convert.ToDateTime(FromDate)).Year) + (Convert.ToDateTime(ToDate)).Month - (Convert.ToDateTime(FromDate)).Month;
            for (int i = 0; i <= NoOfMonths; i++)
            {
                string Condition = string.Empty;
                //StDate = Convert.ToDateTime(txtFromDate.Text).AddMonths(i).ToString("dd-MM-yyyy");
                //EdDate = (Convert.ToDateTime(txtFromDate.Text).AddMonths(i + 1)).AddDays(-1).ToString("dd-MM-yyyy");

                StDate = Convert.ToDateTime(FromDate).AddMonths(i).ToString("dd-MM-yyyy");
                EdDate = (Convert.ToDateTime(FromDate).AddMonths(i + 1)).AddDays(-1).ToString("dd-MM-yyyy");
                Condition = Condition + "AND (CAST(FLOOR(CAST(VoucherDate as FLOAT)) AS DateTime)) >= '" + Convert.ToDateTime(StDate).ToString("MM-dd-yyyy") + "' And (CAST(FLOOR(CAST(VoucherDate as FLOAT)) AS DateTime)) <= '" + Convert.ToDateTime(EdDate).ToString("MM-dd-yyyy") + "' ";
                DS = obj_Ledger.DisplayMonthDetails(Convert.ToString(LedgerId), Condition, out StrError);
                dr = DTLedger.NewRow();
                if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    dr["#"] = LedgerId;
                    dr["Particulars"] = Convert.ToDateTime(StDate).ToString("MMM");
                    dr["Debit"] = DS.Tables[0].Rows[0]["Debit"].ToString();
                    dr["Credit"] = DS.Tables[0].Rows[0]["Credit"].ToString();
                    dr["Closing1"] = DS.Tables[0].Rows[0]["Closing1"].ToString();
                    dr["Closing"] = DS.Tables[0].Rows[0]["Closing"].ToString();
                    dr["ForMonth"] = Convert.ToDateTime(StDate).ToString("dd-MM-yyyy");
                    dr["LedgerID"] = DS.Tables[0].Rows[0]["LedgerID"].ToString();
                }
                else
                {
                    dr["#"] = LedgerId;
                    dr["Particulars"] = Convert.ToDateTime(StDate).ToString("MMM");
                    dr["Debit"] = "0.00";
                    dr["Credit"] = "0.00";
                    dr["Closing"] = "0.00";
                    dr["Closing1"] = "0.00";
                    dr["ForMonth"] = Convert.ToDateTime(StDate).ToString("dd-MM-yyyy");
                    dr["LedgerID"] = "0";
                }

                DTLedger.Rows.Add(dr);

                //----Calculation of Debit,Credit n Closing for each Month in 2nd grid--
                int PreviousRow = DTLedger.Rows.Count - 2;
                int CurrentRow = DTLedger.Rows.Count - 1;
                string temp;
                string Debit = DTLedger.Rows[DTLedger.Rows.Count - 1]["Debit"].ToString();
                string Credit = DTLedger.Rows[DTLedger.Rows.Count - 1]["Credit"].ToString();
                string closing = DTLedger.Rows[DTLedger.Rows.Count - 2]["Closing1"].ToString();
                temp = closing.Substring(closing.Length - 2, 2);
                string closingAmnt = closing.Substring(0, closing.Length - 3);
                //string debitAmnt = Debit.Substring(0, Debit.Length - 2);
                //string creditAmnt = Credit.Substring(0, Credit.Length - 2);
                if (temp.Equals("Cr"))
                {
                    DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing1"] = ((Convert.ToDecimal(Debit) - Convert.ToDecimal(Credit)) - Convert.ToDecimal(closingAmnt)).ToString("0.00");
                    if (Convert.ToDecimal(DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing1"]) > 0)
                    {
                        DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing"] = Convert.ToDecimal(DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing1"]) + " Dr";
                    }
                    else if (Convert.ToDecimal(DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing1"]) < 0)
                    {
                        DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing"] = Math.Abs(Convert.ToDecimal(DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing1"])) + " Cr";
                    }
                    else
                    {
                        DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing"] = "0.00" + " Cr";
                    }
                }
                else
                {
                    DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing1"] = ((Convert.ToDecimal(closingAmnt) + (Convert.ToDecimal(Debit) - Convert.ToDecimal(Credit)))).ToString("0.00");
                    if (Convert.ToDecimal(DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing1"]) > 0)
                    {
                        DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing"] = Convert.ToDecimal(DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing1"]) + " Dr";
                    }
                    else if (Convert.ToDecimal(DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing1"]) < 0)
                    {
                        DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing"] = Math.Abs(Convert.ToDecimal(DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing1"])) + " Cr";
                    }
                    else
                    {
                        DTLedger.Rows[DTLedger.Rows.Count - 1]["Closing"] = "0.00" + " Cr";
                    }
                }
            }
            GrdLedgersummary.DataSource = DTLedger;
            ViewState["LedgerSummary"] = DTLedger;
            GrdLedgersummary.DataBind();
            GrdLedgersummary.Columns[6].Visible = false;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void DisplayAllVouchers(string Ledger, int MonthVal, int Balance)
    {
        try
        {
            DS = obj_Ledger.LedgerEntry(Ledger, MonthVal, Balance, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GrdLedgerVoucher.DataSource = DS.Tables[0];
                ViewState["LedgerVoucher"] = DS.Tables[0];
                GrdLedgerVoucher.DataBind();
                Label3.Text = "Ledger Vouchers";
            }
            else
            {
                GrdLedgerVoucher.DataSource = null;
                GrdLedgerVoucher.DataBind();
                SetInitialRowLedgerVoucher();
            }
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void EnableDtp(bool Flag)
    {
        if (Flag)
        {
            txtFromDate.Focus();
            ChkDate.Checked = true;
            txtFromDate.Text = "01-"+DateTime.Now.ToString("MM-yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }
        else
        {
            ChkDate.Checked = false;
            txtFromDate.Text = "01-" + DateTime.Now.ToString("MM-yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        txtFromDate.Enabled = Flag;
        txtToDate.Enabled = Flag;
      
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {           
            MakeFormEmpty();
        }
    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ReportGrid();
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg(ex.Message, this.Page);
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeFormEmpty();
    }

    protected void GrdReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Credit"))))
        {
            CrAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Credit"));
        }
        if (e.Row.RowType == DataControlRowType.DataRow && !string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Debit"))))
        {
            DrAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debit"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total:";
            e.Row.Cells[3].Text = DrAmt.ToString("#0.00");
            e.Row.Cells[4].Text = CrAmt.ToString("#0.00");
        }
    }

    protected void GrdLedgersummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow && !string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Credit"))))
        {
            CrAmtMonth += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Credit"));
        }
        if (e.Row.RowType == DataControlRowType.DataRow && !string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Debit"))))
        {
            DrAmtMonth += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debit"));
        }
        if (e.Row.RowType == DataControlRowType.DataRow && !string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Closing"))))
        {
            CloAmtMonth = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Closing"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total:";
            e.Row.Cells[3].Text = DrAmtMonth.ToString("#0.00");
            e.Row.Cells[4].Text = CrAmtMonth.ToString("#0.00");
            e.Row.Cells[5].Text = CloAmtMonth.ToString();
        }
    }

    protected void GrdReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        if (!string.IsNullOrEmpty(GrdReport.Rows[0].Cells[5].Text.ToString()) &&
                            !GrdReport.Rows[0].Cells[5].Text.ToString().Equals("&nbsp;"))
                        {
                            int CurrRow = Convert.ToInt32(e.CommandArgument);
                            LedgerId = Convert.ToInt32(GrdReport.Rows[CurrRow].Cells[5].Text.ToString());
                            ViewState["LedgerId"] = LedgerId;
                            FindOpeningBalance(LedgerId);
                            TR1.Visible = true;
                            TR0.Visible = TR2.Visible = false;
                            Label2.Text = "For :  " + Convert.ToString(GrdReport.Rows[CurrRow].Cells[2].Text.ToString());
                            Session["LedgerName"] = Convert.ToString(GrdReport.Rows[CurrRow].Cells[2].Text.ToString());

                            BtnPrintLedgerMonthSummary.Visible = true;
                            BtnExportLedgerMonthSummary.Visible = true;
                            BtnPrintLedgerMonthSummary.Focus();
                        }
                        else
                        {
                            obj_Comman.ShowPopUpMsg("There is no record", this.Page);
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

    protected void GrdLedgersummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        int CurrRow = Convert.ToInt32(e.CommandArgument);
                        if (CurrRow > 0)
                        {
                            LedgerId = Convert.ToInt32(GrdLedgersummary.Rows[CurrRow].Cells[7].Text.ToString());
                            string Month = Convert.ToString(GrdLedgersummary.Rows[CurrRow - 1].Cells[6].Text.ToString());
                            string year = " AND MONTH(VoucherDate)=" + (Convert.ToDateTime(GrdLedgersummary.Rows[CurrRow].Cells[8].Text.ToString())).ToString("MM") + " AND YEAR(VoucherDate)=" + (Convert.ToDateTime(GrdLedgersummary.Rows[CurrRow].Cells[8].Text.ToString())).ToString("yyyy") + "";
                            DisplayAllVouchers(LedgerId.ToString(), Convert.ToInt32((Convert.ToDateTime(GrdLedgersummary.Rows[CurrRow].Cells[8].Text.ToString())).ToString("MM")), Convert.ToInt32((Convert.ToDateTime(GrdLedgersummary.Rows[CurrRow].Cells[8].Text.ToString())).ToString("yyyy")));
                            ViewState["Ledger"] = GrdLedgersummary.Rows[CurrRow].Cells[7].Text.ToString();
                            ViewState["MonthVal"] = Convert.ToInt32((Convert.ToDateTime(GrdLedgersummary.Rows[CurrRow].Cells[8].Text.ToString())).ToString("MM"));
                            ViewState["YearVal"] = Convert.ToInt32((Convert.ToDateTime(GrdLedgersummary.Rows[CurrRow].Cells[8].Text.ToString())).ToString("yyyy"));
                            Session["OpeningBal"] = ((GrdLedgersummary.Rows[CurrRow - 1].Cells[6].Text).ToString());
                            TR2.Visible = true;
                            TR0.Visible = TR1.Visible = false;
                            Label3.Text = "Ledger Vouchers For Month  " + (Convert.ToDateTime(GrdLedgersummary.Rows[CurrRow].Cells[8].Text.ToString())).ToString("MMM-yyyy");
                            Session["MonthName"] = (Convert.ToDateTime(GrdLedgersummary.Rows[CurrRow].Cells[8].Text.ToString())).ToString("MMMM-yyyy");
                            BtnLedgerVoucher.Visible = true;
                            BtnExportLedgerVoucher.Visible = true;
                            BtnLedgerVoucher.Focus();
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

    protected void hy2_Hide_Click(object sender, EventArgs e)
    {
        TR0.Visible = TR2.Visible = false;
        TR1.Visible = true;
        Label2.Text = "";
    }

    protected void hyl_Hide_Click(object sender, EventArgs e)
    {
        TR1.Visible = TR2.Visible = false;
        TR0.Visible = true;
    }

    protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        DataSet DsGrd = new DataSet();
        StrCondition = string.Empty;
        try
        {
            if (TR0.Visible)
            {
                if (ChkDate.Checked == true)
                {
                    ToDate = Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy");
                    FromDate = Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy");
                    StrCondition = StrCondition + " AND VoucherDate<='" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "'";
                }
                else
                {
                    ToDate = Convert.ToDateTime(txtToDate.Text).ToString("01-01-1975");
                    FromDate = Convert.ToDateTime(txtFromDate.Text).ToString("01-01-1975");
                    if (DateTime.Now > Convert.ToDateTime("03/31/" + DateTime.Now.ToString("yyyy")))
                        StrCondition = StrCondition + " AND VoucherDate<= '03/31/" + DateTime.Now.AddYears(1).ToString("yyyy") + "'";
                    else
                        StrCondition = StrCondition + " AND VoucherDate<= '04/01" + DateTime.Now.ToString() + "'";
                }

                //DsGrd = Obj_Call.Getdetails("25", StrCondition, out StrError);

                if (DsGrd.Tables[0].Rows.Count > 0)
                {
                    //========Call Register
                    GridView GridExp = new GridView();
                    DsGrd.Tables[0].Columns.Remove("Closing");
                    DsGrd.Tables[0].Columns.Remove("#");
                    DsGrd.Tables[0].Columns.Remove("LedgerID");
                    GridExp.DataSource = DsGrd.Tables[0];
                    GridExp.DataBind();
                    obj_Comman.Export("ListOfSundryCreditors.xls", GridExp);
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("No Data Found To Export..!", this.Page);
                    DsGrd.Dispose();
                    GrdReport.DataSource = null;
                    GrdReport.DataBind();
                }
            }
            else if (TR1.Visible)
            {
                DataTable dtLedgerDetail = (DataTable)ViewState["LedgerSummary"];
                if (dtLedgerDetail.Rows.Count > 0)
                {
                    //========Call Register
                    GridView GridExp = new GridView();
                    dtLedgerDetail.Columns.Remove("Closing1");
                    dtLedgerDetail.Columns.Remove("#");
                    dtLedgerDetail.Columns.Remove("LedgerID");
                    GridExp.DataSource = dtLedgerDetail;
                    GridExp.DataBind();
                    obj_Comman.Export("ListOfSundryCreditorsLedgerDetail.xls", GridExp);
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("No Data Found To Export..!", this.Page);
                    DsGrd.Dispose();
                    GrdLedgersummary.DataSource = null;
                    GrdLedgersummary.DataBind();
                }
            }
            else if (TR2.Visible)
            {
                DataTable dtLedgerVoucher = (DataTable)ViewState["LedgerVoucher"];
                if (dtLedgerVoucher.Rows.Count > 0)
                {
                    //========Call Register
                    GridView GridExp = new GridView();

                    GridExp.DataSource = dtLedgerVoucher;
                    GridExp.DataBind();
                    obj_Comman.Export("ListOfSundryCreditorsLedgerVoucher.xls", GridExp);
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("No Data Found To Export..!", this.Page);
                    DsGrd.Dispose();
                    GrdLedgersummary.DataSource = null;
                    GrdLedgersummary.DataBind();
                }
            }
        }
        catch (ThreadAbortException ex)
        {

        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnPrintGroupSummary_Click(object sender, EventArgs e)
    {
        if (GrdReport.Rows.Count > 0)
        {
            string Condition = ViewState["Condition"].ToString();
            string ConditionFilter = ViewState["ConditionFilter"].ToString();
            string LedgerId = ViewState["LedgerId"].ToString();
            string str = "SundryCreditorsGroupSummary";
            Response.Redirect("../PrintAccReport/PrintReport.aspx?Cond=" + Condition + " &LedgerId=" + LedgerId + " &Flag=" + str + " &EY=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy") + " &FY=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy") + " &CondFilter=" + ConditionFilter + " ");
           // Response.Redirect("~/PrintAccReport/PrintReport.aspx?Cond=" + Condition + " &LedgerId=" + LedgerId + " &Flag=" + str + " &EY=2014 &FY=2013 &CondFilter=" + ConditionFilter + " ");
            Response.End();
        }
    }

    protected void BtnPrintLedgerMonthSummary_Click(object sender, EventArgs e)
    {
        if (GrdLedgersummary.Rows.Count > 0)
        {
            string LedgerId = ViewState["LedgerId"].ToString();
            string ConditionFilter = ViewState["ConditionFilter"].ToString();
            string str = "SundryCreditorsLedgerMonthlySummary";
            Session["ToDate"] = Convert.ToDateTime(txtToDate.Text).ToString("dd-MM-yyyy");
            Session["FromDate"] = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MM-yyyy");
            Response.Redirect("../PrintAccReport/PrintReport.aspx?LedgerId=" + LedgerId + " &Flag=" + str + " &EY=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy") + " &FY=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy") + " &CondFilter=" + ConditionFilter + " &ID=25");
            Response.End();
        }
    }

    protected void BtnLedgerVoucher_Click(object sender, EventArgs e)
    {
        if (GrdLedgerVoucher.Rows.Count > 0)
        {
            string LedgerId = ViewState["Ledger"].ToString();
            string str = "SundryCreditorsLedgerVouchers";
            Session["FromDate"] = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy");
            Session["ToDate"] = Convert.ToDateTime(txtToDate.Text).ToString("yyyy");

            Response.Redirect("../PrintAccReport/PrintReport.aspx?LedgerId=" + LedgerId + "&StrCondition" + StrCondition + " &Flag=" + str + " &Month=" + Convert.ToInt32(ViewState["MonthVal"]) + " &Year=" + Convert.ToInt32(ViewState["YearVal"]) + " &ID=25");
            Response.End();
        }
    }

    protected void GrdLedgerVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Credit"))))
        {
            CrAmtMonthDtls += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Credit"));
        }
        if (e.Row.RowType == DataControlRowType.DataRow && !string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Debit"))))
        {
            DrAmtMonthDtls += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debit"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = "TOTAL:";
            e.Row.Cells[6].Text = DrAmtMonthDtls.ToString("#0.00");
            e.Row.Cells[7].Text = CrAmtMonthDtls.ToString("#0.00");
        }
    }

    protected void ChkDate_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkDate.Checked)
        {
            EnableDtp(true);
        }
        else
        {
            EnableDtp(false);
        }
    }
    protected void BtnExport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["GridView"]!=null)
                {
                    DataTable dtFirstGrid = (DataTable)ViewState["GridView"];
                    //========Call Register
                    GridView GridExp = new GridView();
                    dtFirstGrid.Columns.Remove("Closing");
                    dtFirstGrid.Columns.Remove("#");
                    dtFirstGrid.Columns.Remove("LedgerID");
                    GridExp.DataSource = dtFirstGrid;
                    GridExp.DataBind();
                    obj_Comman.Export("Outstanding_Balance_Sheet.xls", GridExp);
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("No Data Found To Export..!", this.Page);
                    GrdReport.DataSource = null;
                    GrdReport.DataBind();
                }
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    protected void BtnExportLedgerMonthSummary_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["LedgerSummary"] != null)
            {
                DataTable dtLedgerDetail = (DataTable)ViewState["LedgerSummary"];
                if (dtLedgerDetail.Rows.Count > 0)
                {
                    //========Call Register
                    GridView GridExp = new GridView();
                    dtLedgerDetail.Columns.Remove("Closing1");
                    dtLedgerDetail.Columns.Remove("#");
                    dtLedgerDetail.Columns.Remove("LedgerID");
                    GridExp.DataSource = dtLedgerDetail;
                    GridExp.DataBind();
                    obj_Comman.Export("ListOfSundryCreditorsMonthDetail.xls", GridExp);
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("No Data Found To Export..!", this.Page);
                    GrdLedgersummary.DataSource = null;
                    GrdLedgersummary.DataBind();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void BtnExportLedgerVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["LedgerVoucher"] != null)
            {
                DataTable dtLedgerVoucher = (DataTable)ViewState["LedgerVoucher"];
                if (dtLedgerVoucher.Rows.Count > 0)
                {
                    //========Call Register
                    GridView GridExp = new GridView();
                    dtLedgerVoucher.Columns.Remove("VoucherID");
                    GridExp.DataSource = dtLedgerVoucher;
                    GridExp.DataBind();
                    obj_Comman.Export("ListOfSundryCreditorsLedgerVoucher.xls", GridExp);
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("No Data Found To Export..!", this.Page);
                    GrdLedgersummary.DataSource = null;
                    GrdLedgersummary.DataBind();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}
