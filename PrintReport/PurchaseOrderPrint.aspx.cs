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
using System.Drawing;

using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;

#region Pdf
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Threading;
#endregion 


public partial class Reports_PurchaseOrderPrint : System.Web.UI.Page
{
    #region[Variables]
        DataSet DSdtls = new DataSet();
        DMPurchaseOrder obj_EstimateMaster = new DMPurchaseOrder();
        DMCompanyMaster obj_CM= new DMCompanyMaster();
        PurchaseOrder Entity_EstimateMaster = new PurchaseOrder();
        CommanFunction Obj_Comm = new CommanFunction();
        private string StrError = string.Empty;
        DataTable dttable = new DataTable();
        DataTable dttable1 = new DataTable();
        private int Cnt1 = 0, Cnt2 = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            GetCompanyDetails();
            GetPurchaseOrder();
        }
    }

    public void GetCompanyDetails()
    {
        try
        {
            DataSet DS = new DataSet();
            DS = obj_CM.CompanyDtlsOnPrint(out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    lblCompanyName.Text = DS.Tables[0].Rows[0]["CompanyName"].ToString();
                    lblCompanyAddress.Text = DS.Tables[0].Rows[0]["CAddress"].ToString();
                    lblPhnNo.Text = DS.Tables[0].Rows[0]["PhoneNo"].ToString();
                    imgAntTime.ImageUrl = DS.Tables[0].Rows[0]["CLogo"].ToString();
                    lblFaxNo.Text = DS.Tables[0].Rows[0]["FaxNo"].ToString();
                    LblTinNo.Text = DS.Tables[0].Rows[0]["TinNo"].ToString();
                    lblVatNo.Text = DS.Tables[0].Rows[0]["VatNo"].ToString();
                    lblServiceTaxNo.Text = DS.Tables[0].Rows[0]["ServiceTaxNo"].ToString();
                }
                else
                {
                    lblCompanyName.Text = "";
                    lblCompanyAddress.Text = "";
                    lblPhnNo.Text = "";
                    imgAntTime.ImageUrl = "";
                    lblFaxNo.Text = "";
                    LblTinNo.Text = "";
                    lblVatNo.Text = "";
                    lblServiceTaxNo.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void GetPurchaseOrder()
    {
        try
        {
            int POId = Convert.ToInt32(Request.QueryString["Id"]);
            DSdtls = obj_EstimateMaster.GetPOForPrint(POId, out StrError);
            if (DSdtls.Tables.Count > 0)
            {
                if (DSdtls.Tables[0].Rows.Count > 0)
                {    
                    //PONo,CONVERT(nvarchar,PODate,106) as PODate,SM.SuplierName 
                    lblPono.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["PONo"].ToString());
                    lblPODate.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["PODate"].ToString());
                    lblSuplier.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["SuplierName"].ToString());
                }
                if (DSdtls.Tables[1].Rows.Count > 0)
                {
                    PurOrderGrid.DataSource = DSdtls.Tables[1];
                    PurOrderGrid.DataBind();
                    ViewState["ImportExel"] = DSdtls.Tables[1];
                }
                if (DSdtls.Tables[2].Rows.Count > 0)
                {
                    TermsGrid.DataSource = DSdtls.Tables[2];
                    TermsGrid.DataBind();
                    ViewState["TermConditn"] = DSdtls.Tables[2];
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void ImgPdf_Click(object sender, ImageClickEventArgs e)
    {
        string fileName = "PurchaseOrderDetails" + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".pdf";
        fileName = fileName.Replace(' ', '_');
        string imgPath = Server.MapPath("~/Images/MasterPages/MayurLogo.png");
        Response.Clear();

        GeneratePDFNew(imgPath, fileName, "das", PurOrderGrid, TermsGrid, this.Page, "");
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
        Response.Flush();
        Response.End();
    }
    public void GeneratePDFNew(string Imagepath, string fileName, string HeaderName, GridView GridReport, GridView GridReport1, Page oPage, string text)
    {
        var document = new Document(PageSize.A4, 50, 50, 25, 25);
        //  document.SetMargins(20, 20, 20, 20);
        try
        {
            // oPage.Response.Clear();
            PdfWriter.GetInstance(document, oPage.Response.OutputStream);
            // generates the grid first
            StringBuilder strB = new StringBuilder();
            StringBuilder strB1 = new StringBuilder();
            string str = string.Empty;

            str = "<font size='12px' face='arial'><table width='90%' align='left'>";

            str = str + "<tr><td colspan='4' align='left'><b>" + lblCompanyName.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='left'><b>" + lblCompanyAddress.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='left'><b>Phone No:" + lblPhnNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='left'><b>Fax No:" + lblFaxNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='Center'><b>Purchase Order</b></td></tr>";

            //str = str + "<tr><td align='Left'><b>" + "Purchase Order No. :" + "</b></td><td align='Left'><b>" + lblPono.Text + "</b></td><td  align='right'><b>" + "PurchaseOrderDate:" + "</b></td><td align='right'><b>" + lblPODate.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='Left'><b>Purchase Order No. :" + lblPono.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='Left'><b>PurchaseOrderDate:" + lblPODate.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='3' align='Left'><b>To,</b></td></tr>";
            str = str + "<tr><td colspan='3' align='Left'><b></b></td></tr>";
            str = str + "<tr><td ><b>" + lblSuplier.Text + "</b></td><td colspan='2' align='Left'></td></tr>";
            str = str + "<tr><td colspan='3' align='Left'><b>" + "" + "</b></td></tr>";
            document.SetMargins(10, 10, 10, 10);
            document.Open();
            str = str + "<br><tr><td colspan='5' align='Right'>" + GridViewToHtml(PurOrderGrid) + "</td></tr><br>";

            str = str + "<br><tr><td colspan='5' align='Left'>" + GridViewToHtml(TermsGrid) + "</td></tr><br>";

            //str = str + "<br><tr><td colspan='5' align='left'><b>Tin No:" + LblTinNo.Text + "</b></td></tr>";
            //str = str + "<br><tr><td colspan='5' align='left'><b>Vat No:" + lblVatNo.Text + "</b></td></tr>";
            //str = str + "<br><tr><td colspan='5' align='left'><b>Service Tax No No:" + lblServiceTaxNo.Text + "</b></td></tr>";

            str = str + "<br><tr><td colspan='5'><table width='100%'><tr><td width='30%'>&nbsp;</td><td>&nbsp;</td><td width='30%'>&nbsp;</td><td>&nbsp;</td><td width='30%'>&nbsp;</td></tr></br>";
            str = str + "<br><tr><td width='30%'>&nbsp;</td><td>&nbsp;</td><td width='30%'>&nbsp;</td><td>&nbsp;</td><td width='30%'>&nbsp;</td></tr></br>";
            str = str + "<br><tr><td width='30%'>Prepared By</td><td></td><td width='30%'>Accepted By</td><td></td><td width='30%'>Authorised By</td></tr></br>";
            str = str + "<br><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></br>";
            str = str + "<br><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></br>";
            str = str + "<br><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td >&nbsp;</td></tr></br>";
            str = str + "<br><tr><td>Name &amp; Designation</td><td></td><td>Name &amp; Designation</td><td></td><td>Name &amp; Designation</td></tr></br>";
            str = str + "<br><tr><td>Store Incharge</td><td>&nbsp;</td><td>Vendor: Seal &amp; Signature</td><td>&nbsp;</td><td>Unit Head/Operation Manager</td></tr></br>";
            str = str + "</table></td></tr>";
            str = str + "</table></font>";

            strB.Append(str);

            document.SetMargins(20, 20, 20, 20);
            document.Open();
            if (text.Length.Equals(0)) // export the text
            {
                //  BindMyGrid();
                using (StringWriter sWriter = new StringWriter(strB))
                {
                    using (HtmlTextWriter htWriter = new HtmlTextWriter(sWriter))
                    {
                        //grdPrice.RenderControl(htWriter);
                    }
                }
            }
            else // export the grid
            {
                strB.Append(text);
            }
            // now read the Grid html one by one and add into the document object
            using (TextReader sReader = new StringReader(strB.ToString()))
            {
                iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/MasterPages/MayurLogo.png"));
                document.Add(gif);
                List<IElement> list = HTMLWorker.ParseToList(sReader, new StyleSheet());
                foreach (IElement elm in list)
                {
                    document.Add(elm);
                }
            }
            // oPage.Response.ContentType = "application/pdf";
            // oPage.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            //  oPage.Response.Flush();
            //  oPage.Response.End();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            document.Close();
        }
    }
    public string GridViewToHtml(GridView gv)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gv.RenderControl(hw);
        return sb.ToString();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
    { 
        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.AddHeader(
        //    "content-disposition", string.Format("attachment; filename={0}", "PurchaseOrder"));
        //HttpContext.Current.Response.ContentType = "application/ms-excel";

       // using (System.IO.StringWriter sw = new System.IO.StringWriter())
       // {
          //  using (HtmlTextWriter htw = new HtmlTextWriter(sw))
           // {
                //Create a form to contain the grid
                //Table table = new Table();
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                try
                {
                    dttable = (DataTable)ViewState["ImportExel"];
                    dttable1 = (DataTable)ViewState["TermConditn"];
                    #region[For CompanyDetails-Excel]

                    #region[Image]
                    Microsoft.Office.Interop.Excel.Range range = ExcelApp.get_Range("A1", "G1");
                    range.Font.Size = 12;
                    range.Font.Bold = true;
                    range.Locked = true;
                    range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    range.RowHeight = 30;
                    range.Merge(true);
                    range.Value2 = imgAntTime.ImageUrl;
                    range.EntireColumn.ColumnWidth = 20;
                    range.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    range.Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    #endregion
                    #region[Company Name]
                    Microsoft.Office.Interop.Excel.Range range1 = ExcelApp.get_Range("A2", "G2");
                    range1.Font.Size = 12;
                    range1.Font.Bold = true;
                    range1.Locked = true;
                    range1.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    range1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    range1.RowHeight = 65;
                    range1.Merge(true);
                    range1.Value2 = lblCompanyName.Text + "\n" + lblCompanyAddress.Text + "\n" + "Phone No :" + lblPhnNo.Text + "\n" + "Fax No :" + lblFaxNo.Text;
                    range1.EntireColumn.ColumnWidth = 20;
                    range1.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    range1.Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    #endregion
                    #region[ReportName:PurchaseOrderDetails]
                    Microsoft.Office.Interop.Excel.Range range2 = ExcelApp.get_Range("A3", "G3");
                    range2.Font.Size = 12;
                    range2.Font.Bold = true;
                    range2.Locked = true;
                    range2.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    range2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    range2.RowHeight = 15;
                    range2.Merge(true);
                    range2.Value2 = "Purchase Order Details";
                    range2.EntireColumn.ColumnWidth = 20;
                    range2.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    range2.Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    #endregion
                    #region[PODetails]
                    Microsoft.Office.Interop.Excel.Range range3 = ExcelApp.get_Range("A4", "D4");
                    range3.Font.Size = 12;
                    range3.Font.Bold = true;
                    range3.Locked = true;
                    range3.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    range3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    range3.RowHeight = 30;
                    range3.Merge(true);
                    range3.Value2 = "To :" + "\n" + lblSuplier.Text;
                    range3.EntireColumn.ColumnWidth = 20;
                    #endregion
                    #region[PODetails2]
                    Microsoft.Office.Interop.Excel.Range range4 = ExcelApp.get_Range("E4", "G4");
                    range4.Font.Size = 12;
                    range4.Font.Bold = true;
                    range4.Locked = true;
                    range4.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    range4.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    range4.RowHeight = 30;
                    range4.Merge(true);
                    range4.Value2 = "PO No :" + lblPono.Text + "\n" + "PO Date:" + lblPODate.Text;
                    range4.EntireColumn.ColumnWidth = 20;
                    #endregion
                    #region[For Adding From Grid To Excel]
                    // Storing header part in Excel
                    for (int i = 1; i < dttable.Columns.Count + 1; i++)
                    {
                        ExcelApp.Cells[5, i] = dttable.Columns[i - 1].ColumnName;
                    }

                    // Storing Each row and column value to excel sheet
                    for (int i = 0; i < dttable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dttable.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 6, j + 1] = dttable.Rows[i][j].ToString();
                        }
                    }
                    #endregion
                    #region[ReportName:Terms And Conditions]
                    Cnt1 = dttable.Rows.Count + 7;
                    Cnt2 = Convert.ToInt32(dttable.Rows.Count) - 1;
                    ExcelApp.Cells[Cnt1 + 1, Cnt2 + 1] = "Terms And Condition";
                    ExcelApp.get_Range(ExcelApp.Cells[Cnt1 + 1, Cnt2 + 1], ExcelApp.Cells[Cnt1 + 1, Cnt2 + 1]).Font.Bold = true;
                    #endregion
                    #region[For Adding From Grid To Excel]
                    if (ViewState["TermConditn"] != null)
                    {
                        if (dttable1.Rows.Count > 0)
                        {
                            // Storing header part in Excel
                            for (int i = 1; i < dttable1.Columns.Count + 1; i++)
                            {
                                if (i == 2)
                                {
                                    Microsoft.Office.Interop.Excel.Range Headr = ExcelApp.get_Range(ExcelApp.Cells[dttable.Rows.Count + 7, 2], ExcelApp.Cells[dttable.Rows.Count + 7, 8]);
                                    Headr.RowHeight = 30;
                                    Headr.Merge(true);
                                    Headr.Value2 = dttable1.Columns[i - 1].ColumnName;
                                }
                                else
                                {
                                    ExcelApp.Cells[dttable.Rows.Count + 7, i] = dttable1.Columns[i - 1].ColumnName;
                                }
                            }
                            // Storing Each row and column value to excel sheet
                            for (int i = 0; i < dttable1.Rows.Count; i++)
                            {
                                for (int j = 0; j < dttable1.Columns.Count; j++)
                                {
                                    if (j == 1)
                                    {
                                        Microsoft.Office.Interop.Excel.Range TermsData = ExcelApp.get_Range(ExcelApp.Cells[dttable.Rows.Count + 8, 2], ExcelApp.Cells[dttable.Rows.Count + 8, 7]);
                                        TermsData.RowHeight = 140;
                                        TermsData.Merge(true);
                                        TermsData.Value2 = dttable1.Rows[i][j].ToString();
                                    }
                                    else
                                    {
                                        //Microsoft.Office.Interop.Excel.Range Term = ExcelApp.get_Range(ExcelApp.Cells[i + dttable.Rows.Count + 8, j + 1],ExcelApp.Cells[i + dttable.Rows.Count + 8, j ]);
                                        ExcelApp.Cells[i + dttable.Rows.Count + 8, j + 1] = dttable1.Rows[i][j].ToString();
                                        //Term.RowHeight = 140;
                                        //Term.Value2 = dttable1.Rows[i][j].ToString();
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        ExcelApp.Cells[dttable.Rows.Count + 7,16] = "No Terms And Conditons";
                    }
                    #endregion
                   
                    //ExcelApp.ActiveWorkbook.SaveCopyAs(@"E:\" + DateTime.Today.ToString("dd MMM yyyy") + ".xls");
                    // Obj_Comm.Export("Purchase Order" + DateTime.Now.ToString("dd-MMM-yyyy") + ".xls", ExcelApp);
                    //HttpContext.Current.Response.Write(sw.ToString());
                    //HttpContext.Current.Response.End();
                    //ExcelApp.ActiveWorkbook.Saved = true;
                    //ExcelApp.Quit();
                    //    }
                    //}

                    //********
                    //  render the table into the htmlwriter
                    //sw.Write(ExcelApp);
                    //htw.Write(ExcelApp);
                    //HttpContext.Current.Response.Write(sw.ToString());
                    //HttpContext.Current.Response.End();

                    ExcelApp.ActiveWorkbook.SaveCopyAs(@"D:\" + DateTime.Today.ToString("dd MMM yyyy") + ".xls");
                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();
                    #endregion
                }
                catch (ThreadAbortException tex)
                {

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
    //}
//}
    }
}
