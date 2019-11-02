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

using MayurInventory.DataModel;
using MayurInventory.EntityClass;
using MayurInventory.Utility;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;

using CrystalDecisions.Web;
using System.Threading;
using System.Globalization;
public partial class PrintAccReport_PrintReport : System.Web.UI.Page
{
    #region Private Variable

    DataSet DS = new DataSet();
    ReportDocument CRpt = new ReportDocument();
    DMPaymentVoucher Obj_payment = new DMPaymentVoucher();
    DMSundryCreditorReport obj_SundryLedger = new DMSundryCreditorReport();
    string Flag = "0";
    string PDF = string.Empty;
    string strError = string.Empty;
    string StrCond = string.Empty;
    private string StrCondition = string.Empty;
    private string RepCond = string.Empty;
    string CheckCondition = "";
    int ID = 0, LocationID = 0;
    public static int Print_Flag = 0;
    string PDFMaster = string.Empty;
    string  CheckCondition1 = "", CheckCondition2 = "", LedgerCond = "";
    string CheckConditionFilter = "";
    #endregion

    private void PrintReport()
    {
        try
        {
            Flag = Convert.ToString(Request.QueryString["Flag"]).Trim();
            CheckCondition = Convert.ToString(Request.QueryString["Cond"]);
            CheckConditionFilter = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["CondFilter"])) ? "" : Convert.ToString(Request.QueryString["CondFilter"]);

            switch (Flag)
            {
                //Enquiry Details
                #region[Payment Voucher]
                case "PaymentVoucher":
                    {
                        ID = Convert.ToInt32(Request.QueryString["ID"]);
                        DS = Obj_payment.GetPaymentVoucher(ID, out strError);
                        PDF = Request.QueryString["Flag"].ToString();

                        if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                        {
                            //----------------
                            DataColumn column = new DataColumn("PrintCondition");
                            column.DataType = typeof(string);
                            DS.Tables[0].Columns.Add("PrintCondition");

                            //Add Flag in Each Row
                            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                            {
                                DS.Tables[0].Rows[i]["PrintCondition"] = Flag.ToString();
                                string TotAmt = WordAmount.convertcurrency(Convert.ToDecimal(DS.Tables[0].Rows[0]["VoucherAmount"].ToString()));
                                DataColumn column1 = new DataColumn("Number2Word");
                                column1.DataType = typeof(string);
                                DS.Tables[0].Columns.Add("Number2Word");
                                DS.Tables[0].Rows[DS.Tables[0].Rows.Count - 1]["Number2Word"] = TotAmt.ToString();

                            }

                            DS.Tables[0].TableName = "PaymentVoucher";
                            DS.Tables[1].TableName = "CompanyDetails";
                            DS.Tables[2].TableName = "PaymentDetail";


                            if (Convert.ToBoolean(DS.Tables[0].Rows[0]["IsReference"]))
                            {
                                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                                {
                                    CRpt.Load(Server.MapPath("~/PrintAccReport/CryRptPaymentVoucher.rpt"));
                                    CRpt.SetDataSource(DS);
                                    CRPrint.ReportSource = CRpt;
                                    CRPrint.DataBind();
                                    CRPrint.DisplayToolbar = true;
                                    DS = null;
                                }
                                else
                                {
                                    CRpt.Load(Server.MapPath("~/PrintAccReport/CryRptPaymentVoucher.rpt"));
                                    CRpt.SetDataSource(DS);
                                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "PaymentVoucher - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                                    Response.Redirect("../CrystalPrint/ShowPDF.aspx?Id=" + PDFMaster);
                                }

                            }
                            else
                            {
                                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                                {
                                    CRpt.Load(Server.MapPath("~/PrintAccReport/CryRptPaymentVoucherWithoutDetail.rpt"));
                                    CRpt.SetDataSource(DS);
                                    CRPrint.ReportSource = CRpt;
                                    CRPrint.DataBind();
                                    CRPrint.DisplayToolbar = true;
                                    DS = null;
                                }
                                else
                                {
                                    CRpt.Load(Server.MapPath("~/PrintAccReport/CryRptPaymentVoucherWithoutDetail.rpt"));
                                    CRpt.SetDataSource(DS);
                                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "PaymentVoucher - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                                    Response.Redirect("../CrystalPrint/ShowPDF.aspx?Id=" + PDFMaster);
                                }

                               
                            }
                            //------- Add New Code For Print-----
                            if (Print_Flag != 0)
                            {
                                //CRpt.PrintOptions.PrinterName = "Send To OneNote 2007";
                                //  CRpt.PrintToPrinter(1, false, 0, 0);
                            }
                            //------- Add New Code For Print-----
                        }
                        break;
                    }
                #endregion

                #region[1st Grid for Creditors]
                case "SundryCreditorsGroupSummary":
                    {

                        LedgerCond = Convert.ToString(Request.QueryString["LedgerId"]).Trim();
                        if (LedgerCond == "25")
                        {
                            DS = obj_SundryLedger.Getdetails(LedgerCond, CheckCondition, out strError);
                            this.Page.Title = "Print-SundryCreditors";
                        }
                        else
                        {
                            //DS = obj_SundryLedger.GetdetailsD(LedgerCond, CheckCondition, out strError);
                            //this.Page.Title = "Print-SundryDebtors";
                        }

                        if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                        {
                            //New DataColumn(FilterCondition)By Gayatri 13 June 2013----------------
                            DataColumn column = new DataColumn("FilterCondition");
                            column.DataType = typeof(string);
                            DS.Tables[0].Columns.Add("FilterCondition");
                            //DS.Tables[0].Rows[0]["FilterCondition"] = CheckConditionFilter.ToString();

                            //New DataColumn(RowCount)By Gayatri 26 June 2013----------------

                            DataColumn columnR = new DataColumn("RowCount");
                            columnR.DataType = typeof(string);
                            DS.Tables[0].Columns.Add("RowCount");

                            //End DataColumn(RowCount)-----------------

                            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                            {
                                DS.Tables[0].Rows[i]["FilterCondition"] = CheckConditionFilter.ToString();
                                DS.Tables[0].Rows[i]["RowCount"] = DS.Tables[0].Rows.Count + " Record(s) Found!!";
                            }

                            DS.Tables[0].TableName = "SundryCreditorsGroupSummary";
                            DataColumn column1 = new DataColumn("ForName", typeof(string));
                            DS.Tables[0].Columns.Add("ForName");
                            DataColumn column2 = new DataColumn("Name", typeof(string));
                            DS.Tables[0].Columns.Add("Name");
                            DataColumn column3 = new DataColumn("FromDate", typeof(string));
                            DS.Tables[0].Columns.Add("FromDate");
                            DataColumn column4 = new DataColumn("ToDate", typeof(string));
                            DS.Tables[0].Columns.Add("ToDate");
                            if (Convert.ToInt32(LedgerCond) == 25)
                                DS.Tables[0].Rows[0]["ForName"] = "Sundry Creditors";
                            else
                                DS.Tables[0].Rows[0]["ForName"] = "Sundry Debtors";

                            string[] d = Convert.ToString((Session["LedgerDate"])).Split('*');
                            DS.Tables[0].Rows[0]["FromDate"] = Convert.ToDateTime(d[0]).ToString("dd-MMM-yyyy");
                            DS.Tables[0].Rows[0]["ToDate"] = Convert.ToDateTime(d[1]).ToString("dd-MMM-yyyy");

                            DS.Tables[0].Rows[0]["Name"] = "KARIA " + Convert.ToString(Request.QueryString["FY"]).Trim() + " - " + Convert.ToString(Request.QueryString["EY"]).Trim();
                            DS.Tables[0].TableName = "SundryCreditorsGroupSummary";
                            DS.Tables[1].TableName = "CompanyDetails";
                            CRpt.Load(Server.MapPath("~/PrintAccReport/CryRptSundryCreditors.rpt"));
                            CRpt.SetDataSource(DS);
                            CRPrint.ReportSource = CRpt;
                            CRPrint.DataBind();
                            CRPrint.DisplayToolbar = true;
                        }
                        break;
                    }
                #endregion

                #region[2nd Grid For Creditors]
                case "SundryCreditorsLedgerMonthlySummary":
                    {

                        //StrCondition = string.Empty;
                        string StDate = string.Empty;
                        string EdDate = string.Empty;
                       string RepCond = string.Empty;
                        int NoOfMonths;
                        DataSet DSOpeningBal = new DataSet();
                        RepCond = Convert.ToString(Request.QueryString["LedgerId"]).Trim();
                        StrCondition = StrCondition + "AND VoucherDate<= '" + Convert.ToDateTime(Session["FromDate"]).ToString("MM-dd-yyyy") + "' ";
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
                        //if (Convert.ToString(Request.QueryString["ID"]).Trim() == "25")
                        //{
                        DSOpeningBal = obj_SundryLedger.FindOpeningBal(RepCond, StrCondition, out strError);
                        //}
                        //else
                        //{
                        //     DSOpeningBal = obj_SundryLedger.FindOpeningBalDebtor(RepCond, StrCondition, out strError);
                        //}
                        //-----Set Opening Balance-----
                        if (DSOpeningBal.Tables.Count > 0 && DSOpeningBal.Tables[0].Rows.Count > 0)
                        {
                            dr["#"] = 0;
                            dr["Particulars"] = "Opening balance";
                            dr["Debit"] = Convert.ToDecimal(DSOpeningBal.Tables[0].Rows[0]["Debit"].ToString()).ToString("#0.00");
                            dr["Credit"] = Convert.ToDecimal(DSOpeningBal.Tables[0].Rows[0]["Credit"].ToString()).ToString("#0.00");
                            dr["Closing1"] = DSOpeningBal.Tables[0].Rows[0]["Closing1"].ToString();
                            dr["Closing"] = DSOpeningBal.Tables[0].Rows[0]["Closing"].ToString();
                            dr["ForMonth"] = DSOpeningBal.Tables[0].Rows[0]["ForMonth"].ToString();
                            dr["LedgerID"] = DSOpeningBal.Tables[0].Rows[0]["LedgerID"].ToString();
                            DTLedger.Rows.Add(dr);
                        }
                        //-----For No. Of Months-----
                        NoOfMonths = 12 * ((Convert.ToDateTime(Session["ToDate"])).Year - (Convert.ToDateTime(Session["FromDate"])).Year) + (Convert.ToDateTime(Session["ToDate"])).Month - (Convert.ToDateTime(Session["FromDate"])).Month;
                        for (int i = 0; i <= NoOfMonths; i++)
                        {
                            string Condition = string.Empty;
                            StDate = Convert.ToDateTime(Session["FromDate"]).AddMonths(i).ToString("dd-MM-yyyy");
                            EdDate = (Convert.ToDateTime(Session["FromDate"]).AddMonths(i + 1)).AddDays(-1).ToString("dd-MM-yyyy");
                            Condition = Condition + "AND VoucherDate >= '" + Convert.ToDateTime(StDate).ToString("MM-dd-yyyy") + "' And VoucherDate <= '" + Convert.ToDateTime(EdDate).ToString("MM-dd-yyyy") + "' ";

                            //if (Convert.ToString(Request.QueryString["ID"]).Trim() == "25")
                            //{
                            DSOpeningBal = obj_SundryLedger.DisplayMonthDetails(RepCond, Condition, out strError);
                            //}
                            //else
                            //{
                            //    DSOpeningBal = obj_SundryLedger.DisplayMonthDetailsDebitor(RepCond, StDate, EdDate, out strError);
                            //}
                            dr = DTLedger.NewRow();
                            if (DSOpeningBal.Tables.Count > 0 && DSOpeningBal.Tables[0].Rows.Count > 0)
                            {
                                dr["#"] = Convert.ToInt32(Request.QueryString["LedgerId"]);
                                dr["Particulars"] = Convert.ToDateTime(StDate).ToString("MMM");
                                dr["Debit"] = DSOpeningBal.Tables[0].Rows[0]["Debit"].ToString();
                                dr["Credit"] = DSOpeningBal.Tables[0].Rows[0]["Credit"].ToString();
                                dr["Closing1"] = DSOpeningBal.Tables[0].Rows[0]["Closing1"].ToString();
                                dr["Closing"] = DSOpeningBal.Tables[0].Rows[0]["Closing"].ToString();
                                dr["ForMonth"] = Convert.ToDateTime(StDate).ToString("dd-MM-yyyy");
                                dr["LedgerID"] = DSOpeningBal.Tables[0].Rows[0]["LedgerID"].ToString();
                            }
                            else
                            {
                                dr["#"] = Convert.ToInt32(Request.QueryString["LedgerId"]);
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
                        DS.Tables.Add(DTLedger);
                        this.Page.Title = "Print-SundryCreditorLedgerMonthlySummary";
                        DS.Tables[0].TableName = "SundryCreditorsLedgerMonthlySummary";
                        DataColumn column1 = new DataColumn("ForName", typeof(string));
                        DS.Tables[0].Columns.Add("ForName");
                        DataColumn column2 = new DataColumn("Name", typeof(string));
                        DS.Tables[0].Columns.Add("Name");
                        DataColumn column3 = new DataColumn("TotalClosing", typeof(string));
                        DS.Tables[0].Columns.Add("TotalClosing");
                        DataColumn column4 = new DataColumn("TotalCredit", typeof(decimal));
                        DS.Tables[0].Columns.Add("TotalCredit");
                        DataColumn column5 = new DataColumn("TotalDebit", typeof(decimal));
                        DS.Tables[0].Columns.Add("TotalDebit");
                        DataColumn column6 = new DataColumn("FromDate", typeof(string));
                        DS.Tables[0].Columns.Add("FromDate");
                        DataColumn column7 = new DataColumn("ToDate", typeof(string));
                        DS.Tables[0].Columns.Add("ToDate");
                        decimal sd = 0; decimal sc = 0;
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            //New DataColumn(RowCount)By Gayatri 26 June 2013----------------

                            DataColumn columnR = new DataColumn("RowCount");
                            columnR.DataType = typeof(string);
                            DS.Tables[0].Columns.Add("RowCount");

                            //End DataColumn(RowCount)-----------------

                            for (int q = 0; q < DS.Tables[0].Rows.Count; q++)
                            {
                                sd += Convert.ToDecimal(DS.Tables[0].Rows[q]["Debit"].ToString());
                                sc += Convert.ToDecimal(DS.Tables[0].Rows[q]["Credit"].ToString());
                                DS.Tables[0].Rows[q]["RowCount"] = DS.Tables[0].Rows.Count + " Record(s) Found!!";
                            }
                        }
                        DS.Tables[0].Rows[0]["TotalDebit"] = sd;
                        DS.Tables[0].Rows[DS.Tables[0].Rows.Count - 1]["TotalDebit"] = sd;
                        DS.Tables[0].Rows[0]["TotalCredit"] = sc;
                        DS.Tables[0].Rows[DS.Tables[0].Rows.Count - 1]["TotalCredit"] = sc;
                        DS.Tables[0].Rows[0]["ForName"] = "Ledger Monthly Summary :- " + Convert.ToString(Session["LedgerName"]).Replace("&amp;", "&"); ;
                        DS.Tables[0].Rows[0]["Name"] = "KARIA " + Convert.ToString(Request.QueryString["FY"]).Trim() + " - " + Convert.ToString(Request.QueryString["EY"]).Trim();
                        DS.Tables[0].Rows[0]["TotalClosing"] = DS.Tables[0].Rows[DS.Tables[0].Rows.Count - 1]["Closing"];
                        DS.Tables[0].Rows[DS.Tables[0].Rows.Count - 1]["TotalClosing"] = DS.Tables[0].Rows[DS.Tables[0].Rows.Count - 1]["Closing"];
                        string[] d = Convert.ToString((Session["LedgerDate"])).Split('*');
                        DS.Tables[0].Rows[0]["FromDate"] = Convert.ToDateTime(d[0]).ToString("dd-MMM-yyyy");
                        DS.Tables[0].Rows[0]["ToDate"] = Convert.ToDateTime(d[1]).ToString("dd-MMM-yyyy");
                        DS.Tables[0].TableName = "SundryCreditorsLedgerMonthlySummary";
                        DS.Tables[1].TableName = "CompanyDetails";
                        CRpt.Load(Server.MapPath("~/PrintAccReport/CryRptSundryLedgerMonthly.rpt"));
                        CRpt.SetDataSource(DS);
                        CRPrint.ReportSource = CRpt;
                        CRPrint.DataBind();
                        CRPrint.DisplayToolbar = true;

                        break;
                    }
                #endregion
                
                #region[3rd Grid For Creditors]
                case "SundryCreditorsLedgerVouchers":
                    {
                        LedgerCond = Convert.ToString(Request.QueryString["LedgerId"]).Trim();
                        int Month = Convert.ToInt32(Request.QueryString["Month"]);
                        int Year = Convert.ToInt32(Request.QueryString["Year"]);
                        if (Convert.ToString(Request.QueryString["ID"]).Trim() == "25")
                        {
                            DS = obj_SundryLedger.LedgerEntry(LedgerCond, Month, Year, out strError);
                            this.Page.Title = "Print-SundryCreditorLedgerVoucher";
                        }
                        else
                        {
                            //DS = obj_SundryLedger.LedgerEntryDebitor(LedgerCond, Month, Year, out strError);
                            //this.Page.Title = "Print-SundryDebitorLedgerVoucher";
                        }
                        if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                        {
                            //New DataColumn(FilterCondition)By Gayatri 13 June 2013----------------
                            DataColumn column = new DataColumn("FilterCondition");
                            column.DataType = typeof(string);
                            DS.Tables[0].Columns.Add("FilterCondition");
                            //DS.Tables[0].Rows[0]["FilterCondition"] = CheckConditionFilter.ToString();

                            //New DataColumn(RowCount)By Gayatri 26 June 2013----------------

                            DataColumn columnR = new DataColumn("RowCount");
                            columnR.DataType = typeof(string);
                            DS.Tables[0].Columns.Add("RowCount");

                            //End DataColumn(RowCount)-----------------

                            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                            {
                                DS.Tables[0].Rows[i]["FilterCondition"] = CheckConditionFilter.ToString();
                                DS.Tables[0].Rows[i]["RowCount"] = DS.Tables[0].Rows.Count + " Record(s) Found!!";
                            }

                            DS.Tables[0].TableName = "SundryCreditorsGroupSummary";
                            DataColumn column1 = new DataColumn("ForName", typeof(string));
                            DS.Tables[0].Columns.Add("ForName");
                            DataColumn column2 = new DataColumn("Name", typeof(string));
                            DS.Tables[0].Columns.Add("Name");
                            DataColumn column3 = new DataColumn("MonthName", typeof(string));
                            DS.Tables[0].Columns.Add("MonthName");
                            DataColumn column4 = new DataColumn("OpeningBal", typeof(Decimal));
                            DS.Tables[0].Columns.Add("OpeningBal");
                            DataColumn column5 = new DataColumn("OpeningBalCRDR", typeof(string));
                            DS.Tables[0].Columns.Add("OpeningBalCRDR");
                            DataColumn column6 = new DataColumn("CLOSINGBAL", typeof(string));
                            DS.Tables[0].Columns.Add("CLOSINGBAL");

                            DS.Tables[0].Rows[0]["ForName"] = "Ledger :- " + Convert.ToString(Session["LedgerName"]).Replace("&amp;", "&");

                            DS.Tables[0].Rows[0]["Name"] = "KARIA " + Convert.ToString(Session["FromDate"]) + " - " + Convert.ToString(Session["ToDate"]);
                            DS.Tables[0].Rows[0]["MonthName"] = "For The Month Of " + Convert.ToString(Session["MonthName"]);
                            DS.Tables[0].Rows[0]["OpeningBal"] = Convert.ToDecimal(Session["OpeningBal"]);
                            decimal sd = 0; decimal sc = 0;
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                for (int q = 0; q < DS.Tables[0].Rows.Count; q++)
                                {
                                    sd += Convert.ToDecimal(DS.Tables[0].Rows[q]["Debit"].ToString());
                                    sc += Convert.ToDecimal(DS.Tables[0].Rows[q]["Credit"].ToString());
                                }
                            }
                            if (Convert.ToDecimal(Session["OpeningBal"]) >= 0)
                            {
                                DS.Tables[0].Rows[0]["OpeningBalCRDR"] = Math.Abs(Convert.ToDecimal(DS.Tables[0].Rows[0]["OpeningBal"].ToString())).ToString() + " Dr";
                                DS.Tables[0].Rows[DS.Tables[0].Rows.Count - 1]["OpeningBalCRDR"] = Math.Abs(Convert.ToDecimal(DS.Tables[0].Rows[0]["OpeningBal"].ToString())).ToString() + " Dr";
                            }
                            else
                            {
                                DS.Tables[0].Rows[0]["OpeningBalCRDR"] = Math.Abs(Convert.ToDecimal(DS.Tables[0].Rows[0]["OpeningBal"].ToString())).ToString() + " Cr";
                                DS.Tables[0].Rows[DS.Tables[0].Rows.Count - 1]["OpeningBalCRDR"] = Math.Abs(Convert.ToDecimal(DS.Tables[0].Rows[0]["OpeningBal"].ToString())).ToString() + " Cr";
                            }

                            if (((sd - sc) + Convert.ToDecimal(DS.Tables[0].Rows[0]["OpeningBal"].ToString())) >= 0)
                            {
                                DS.Tables[0].Rows[0]["CLOSINGBAL"] = Math.Abs((sd - sc) + Convert.ToDecimal(DS.Tables[0].Rows[0]["OpeningBal"].ToString())).ToString() + " Dr";
                                DS.Tables[0].Rows[DS.Tables[0].Rows.Count - 1]["CLOSINGBAL"] = Math.Abs((sd - sc) + Convert.ToDecimal(DS.Tables[0].Rows[0]["OpeningBal"].ToString())).ToString() + " Dr";
                            }
                            else
                            {
                                DS.Tables[0].Rows[0]["CLOSINGBAL"] = Math.Abs((sd - sc) + Convert.ToDecimal(DS.Tables[0].Rows[0]["OpeningBal"].ToString())).ToString() + " Cr";
                                DS.Tables[0].Rows[DS.Tables[0].Rows.Count - 1]["CLOSINGBAL"] = Math.Abs((sd - sc) + Convert.ToDecimal(DS.Tables[0].Rows[0]["OpeningBal"].ToString())).ToString() + " Cr";
                            }

                            DS.Tables[0].TableName = "SundryCreditorsLedgerVoucher";
                            DS.Tables[1].TableName = "CompanyDetails";
                            CRpt.Load(Server.MapPath("~/PrintAccReport/CryRptSundryLedgerVoucher.rpt"));
                            CRpt.SetDataSource(DS);
                            CRPrint.ReportSource = CRpt;
                            CRPrint.DataBind();
                            CRPrint.DisplayToolbar = true;
                        }
                        break;
                    }
                #endregion
            }
        }
        catch (ThreadAbortException thex)
        {


        }
        catch (Exception ex)
        {
            
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Print_Flag = 0;
            PrintReport();
        }
        else
        {
            Print_Flag = 1;
            PrintReport();
        }
    }

}
