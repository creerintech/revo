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
    DMMaterialInwardReg obj_EstimateMaster = new DMMaterialInwardReg();
    DMCompanyMaster obj_CM = new DMCompanyMaster();
    MaterialInwardReg Entity_EstimateMaster = new MaterialInwardReg();
    CommanFunction Obj_Comm = new CommanFunction();
    DataTable dttable = new DataTable();
    private string StrError = string.Empty;
    private int Cnt1 = 0, Cnt2 = 0,Cnt3 = 0,Cnt4 = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            GetCompanyDetails();
            GetInward();
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
                    LblTinNo.Text=DS.Tables[0].Rows[0]["TinNo"].ToString();
                    lblVatNo.Text=DS.Tables[0].Rows[0]["VatNo"].ToString();
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
    public void GetInward()
    {
        try
        {
            int Inwardid = Convert.ToInt32(Request.QueryString["Id"]);
            DSdtls = obj_EstimateMaster.GetInwardForPrint(Inwardid, out StrError);
            if (DSdtls.Tables.Count > 0)
            {
                if (DSdtls.Tables[0].Rows.Count > 0)
                {

                    lbInwardNo.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["InwardNo"].ToString());
                    lblInwardDate.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["InwardDate"].ToString());
                    lbBillNo.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["BillNo"].ToString());
                    lbInwardThrough.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["InwardThrough"].ToString());
                    lblSupplier.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["SuplierName"].ToString());
                    lblShippingAddress.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["ShippingAddress"].ToString());
                    lblBillingAddress.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["BillingAddress"].ToString());
                    lblSubTotal.Text=Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["SubTotal"].ToString());
                    lblDiscount.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["DiscountAmt"].ToString());
                    lblVat.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["VatAmt"].ToString());
                    lblDekhrekh.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["DekhrekhAmt"].ToString());
                    lblHamali.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["HamaliAmt"].ToString());
                    lblCESS.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["CESSAmt"].ToString());
                    lblFreight.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["FreightAmt"].ToString());
                    lblPacking.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["PackingAmt"].ToString());
                    lblPostage.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["PostageAmt"].ToString());
                    lblOtherCharges.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["OtherCharges"].ToString());
                    lblPONO.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["PONo"].ToString());

                    lblInstruction.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["Instruction"].ToString());

                    lblGrandTotal.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["GrandTotal"].ToString());
                    lblbilldate.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["BillDate"].ToString());
                }
                if (DSdtls.Tables[1].Rows.Count > 0)
                {
                    InwardGrid.DataSource = DSdtls.Tables[1];
                    InwardGrid.DataBind();
                    ViewState["ImportExel"] = DSdtls.Tables[1];
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
        string fileName = "InwardDetails"+"_"+DateTime.Now.ToString("dd-MMM-yyyy")+".pdf";
        fileName = fileName.Replace(' ', '_');
        string imgPath = Server.MapPath("~/Images/MasterPages/MayurLogo.png");
        Response.Clear();

        GeneratePDFNew(imgPath, fileName, "das", InwardGrid, this.Page, "");
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
        Response.Flush();
        Response.End();
    }
    public void GeneratePDFNew(string Imagepath, string fileName, string HeaderName, GridView GridReport, Page oPage, string text)
    {
        var document = new Document(PageSize.A4, 50, 50, 25, 25);
        //  document.SetMargins(20, 20, 20, 20);
        try
        {
            PdfWriter.GetInstance(document, oPage.Response.OutputStream);
            StringBuilder strB = new StringBuilder();
            StringBuilder strB1 = new StringBuilder();
            string str = string.Empty;

            #region[pdfPrint]
            str = "<font size='12px' face='arial'><table width='100%' align='Centre'>";

            str = str + "<tr><td colspan='5' align='left'><b>" + lblCompanyName.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='left'><b>" + lblCompanyAddress.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='left'><b>Phone No:" + lblPhnNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='left'><b>Fax No:" + lblFaxNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='Center'><b>Material Issue Register Details </b></td></tr>";

            str = str + "<tr><td align='right'><b>Inward No:</b></td><td align='left'><b>" + lbInwardNo.Text + "</b></td><td align='right'><b></b></td><td align='right'><b>Inward Date:</b></td><td align='left'><b>" + lblInwardDate.Text + "</b></td></tr>";
            str = str + "<tr><td align='right'><b>Bill No:</b></td><td align='left'><b>" + lbBillNo.Text + "</b></td><td align='right'><b></b></td><td align='right'><b>Inward Through:</b></td><td align='left'><b>"+lbInwardThrough.Text + "</b></td></tr>";
            str = str + "<tr><td align='right'><b>To,</b></td><td align='left'><b></b></td><td align='right'><b></b></td><td align='right'><b>Purchase Order No:</b></td><td align='left'><b>" + lblPONO.Text + "</b></td></tr>";
            str = str + "<tr><td align='right'><b></b></td><td align='left' colspan='3'><b>" + lblSupplier.Text + "</b></td><td align='left'><b></b></td></tr>";
            str = str + "<tr><td align='right'><b></b></td><td align='left' colspan='3'><b></b></td><td align='left'><b></b></td></tr>";
            str = str + "<tr><td align='right'><b>Billing Address:</b></td><td align='left' colspan='4'><b>" + lblBillingAddress.Text + "</b></td></tr>";
            str = str + "<tr><td align='right'><b>Shipping Address:</b></td><td align='left' colspan='4'><b>" + lblShippingAddress.Text + "</b></td></tr>";

            document.SetMargins(10, 10, 10, 10);
            document.Open();
            str = str + "<br><tr><td colspan='5' align='Center'>" + GridViewToHtml(InwardGrid) + "</td></tr><br>";
            str = str + "<tr><td colspan='5' align='Right'><b>" + "SubTotal:" + lblSubTotal.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='Right'><b>" + "Discount:" + lblDiscount.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='Right'><b>" + "VAT:" + lblVat.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='Right'><b>" + "Dekhrekh:" + lblDekhrekh.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='Right'><b>" + "Hamali:" + lblHamali.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='Right'><b>" + "Cess:" + lblCESS.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='Right'><b>" + "Freight:" + lblFreight.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='Right'><b>" + "Packing:" + lblPacking.Text + "</b></td></tr>";
            str = str + "<tr><td align='Right'><b>Tin No:"+LblTinNo.Text+"</b></td><td colspan='4' align='Right'><b>" + "Postage:" + lblPostage.Text + "</b></td></tr>";
            str = str + "<tr><td align='Right'><b>Vat No:"+lblVatNo.Text+"</b></td><td colspan='4' align='Right'><b>" + "Other Charges:" + lblOtherCharges.Text + "</b></td></tr>";
            str = str + "<tr><td align='Right'><b>ServiceTaxNo:"+lblServiceTaxNo.Text+"</b></td><td colspan='4' align='Right'><b>" + "GrandTotal:" + lblGrandTotal.Text + "</b></td></tr>";

            str = str + "<br><tr><td colspan='7'><table width='100%'><tr><td width='15%'>Prepared By</td><td></td><td width='15%'>Authorised By</td><td></td><td width='15%'>Received By</td></tr></br>";
            str = str + "<br><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></br>";
            str = str + "<br><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></br>";
            str = str + "<br><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></br>";
            str = str + "<br><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></br>";
            str = str + "<br><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></br>";
            str = str + "<br><tr><td>Name &amp; Designation</td><td></td><td>Name &amp; Designation</td><td></td><td >Name &amp; Designation</td></tr></br>";
            str = str + "<br><tr><td>Store Incharge</td><td></td><td>Unit Head/Operation Manager</td><td></td><td>Store Incharge</td></tr></br>";
            str = str + "</table></td></tr></br>";
            str = str + "</table></font>";
            #endregion

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
        Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        ExcelApp.Application.Workbooks.Add(Type.Missing);
    
         try
        {
            dttable = (DataTable)ViewState["ImportExel"];
            #region[For CompanyDetails-Excel]
            #region[Image]
                Microsoft.Office.Interop.Excel.Range range = ExcelApp.get_Range("A1", "K1");
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
                //range.CopyPicture(
                //string path = string.Format("http://localhost:3366{0}", base.ResolveUrl("~/Images/dotnetlogo.gif"));

                // place an image at a particular position
                //ExcelApp.Worksheets(1).Shapes.AddPicture(path, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 150, 100);
 
            #endregion
            #region[Company Name]
                Microsoft.Office.Interop.Excel.Range range1 = ExcelApp.get_Range("A2", "K2");
                range1.Font.Size = 12;
                range1.Font.Bold = true;
                range1.Locked = true;
                range1.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                range1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                range1.RowHeight = 65;
                range1.Merge(true);
                range1.Value2 = lblCompanyName.Text +"\n"+lblCompanyAddress.Text+"\n"+"Phone No :"+ lblPhnNo.Text +"\n"+"Fax No :"+lblFaxNo.Text;
                range1.EntireColumn.ColumnWidth = 20;
                range1.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                range1.Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            #endregion
            #region[ReportName:Inward Register]
                Microsoft.Office.Interop.Excel.Range range2 = ExcelApp.get_Range("A3", "K3");
                range2.Font.Size = 12;
                range2.Font.Bold = true;
                range2.Locked = true;
                range2.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                range2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                range2.RowHeight = 15;
                range2.Merge(true);
                range2.Value2 = "Material Inward Register";
                range2.EntireColumn.ColumnWidth = 20;
                range2.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                range2.Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            #endregion
            #region[InwardDetails]
                Microsoft.Office.Interop.Excel.Range range3 = ExcelApp.get_Range("A4", "D4");
                range3.Font.Size = 12;
                range3.Font.Bold = true;
                range3.Locked = true;
                range3.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                range3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                range3.RowHeight = 70;
                range3.Merge(true);
                range3.Value2 ="Inward No:"+lbInwardNo.Text
                               +"\n"+"Bill No :"+lbBillNo.Text
                               +"\n"+"To :"+lblSupplier.Text 
                               +"\n"+"Billing Address :"+lblBillingAddress.Text;
                range3.EntireColumn.ColumnWidth = 20;
            #endregion
            #region[InwardDetails2]
                Microsoft.Office.Interop.Excel.Range range4 = ExcelApp.get_Range("E4", "K4");
                range4.Font.Size = 12;
                range4.Font.Bold = true;
                range4.Locked = true;
                range4.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                range4.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                range4.RowHeight = 70;
                range4.Merge(true);
                range4.Value2 ="Inward Date :" + lblInwardDate.Text
                               + "\n" + "Inward Through :" + lbInwardThrough.Text
                               + "\n" + "Purchase Order No :" + lblPONO.Text
                               + "\n" + "Shipping Address :" + lblShippingAddress.Text;
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
            #region[SubTotal and Other Details]
                Cnt1 = dttable.Rows.Count + 5;
                Cnt2 = Convert.ToInt32(dttable.Columns.Count) - 1;
                Cnt3 = dttable.Rows.Count + 12;
                Cnt4 = Convert.ToInt32(dttable.Columns.Count - 10);
                //1st row
                ExcelApp.Cells[Cnt1 + 3, Cnt2] = "SubTotal :";
                ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1] = lblSubTotal.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt1 + 3, Cnt2], ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1]).Font.Bold = true;
                //2nd row
                Cnt1 = Cnt1 + 1;
                ExcelApp.Cells[Cnt1 + 3, Cnt2] = "Discount :";
                ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1] = lblDiscount.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt1 + 4, Cnt2], ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1]).Font.Bold = true;
                //3rd Row
                Cnt1 = Cnt1 + 1;
                ExcelApp.Cells[Cnt1 + 3, Cnt2] = "Dekhrekh :";
                ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1] = lblDekhrekh.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt1 + 3, Cnt2], ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1]).Font.Bold = true;
                //4th Row
                Cnt1 = Cnt1 + 1;
                ExcelApp.Cells[Cnt1 + 3, Cnt2] = "Hamali :";
                ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1] = lblHamali.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt1 + 3, Cnt2], ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1]).Font.Bold = true;
                //5th Row
                Cnt1 = Cnt1 + 1;
                ExcelApp.Cells[Cnt1 + 3, Cnt2] = "Cess :";
                ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1] = lblCESS.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt1 + 3, Cnt2], ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1]).Font.Bold = true;
                //6th Row
                Cnt1 = Cnt1 + 1;
                ExcelApp.Cells[Cnt1 + 3, Cnt2] = "Freight :";
                ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1] = lblFreight.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt1 + 3, Cnt2], ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1]).Font.Bold = true;
                //7th Row
                Cnt1 = Cnt1 + 1;
                ExcelApp.Cells[Cnt1 + 3, Cnt2] = "Packing :";
                ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1] = lblPacking.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt1 + 7, Cnt2], ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1]).Font.Bold = true;
                //8th Row
                Cnt1 = Cnt1 + 1;
                ExcelApp.Cells[Cnt1 + 3, Cnt2] = "Postage :";
                ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1] = lblPostage.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt1 + 3, Cnt2], ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1]).Font.Bold = true;
                //9th Row
                Cnt1 = Cnt1 + 1;
                ExcelApp.Cells[Cnt1 + 3, Cnt2] = "OtherCharges :";
                ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1] = lblOtherCharges.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt1 + 7, Cnt2], ExcelApp.Cells[Cnt1 + 3, Cnt2 + 3]).Font.Bold = true;
                //10th Row
                Cnt1 = Cnt1 + 1;
                ExcelApp.Cells[Cnt1 + 3, Cnt2] = "GrandTotal :";
                ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1] = lblGrandTotal.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt1 + 3, Cnt2], ExcelApp.Cells[Cnt1 + 3, Cnt2 + 1]).Font.Bold = true;
                //TinNo
                Cnt3 = Cnt3 + 1;
                ExcelApp.Cells[Cnt3 + 3, Cnt4] = "Tin No :";
                ExcelApp.Cells[Cnt3 + 3, Cnt4 + 1] = LblTinNo.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt3 + 3, Cnt4], ExcelApp.Cells[Cnt3 + 3, Cnt4 + 1]).Font.Bold = true;
                //Vat No
                Cnt3 = Cnt3 + 1;
                ExcelApp.Cells[Cnt3 + 3, Cnt4] = "Vat No :";
                ExcelApp.Cells[Cnt3 + 3, Cnt4 + 1] = lblVatNo.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt3 + 3, Cnt4], ExcelApp.Cells[Cnt3 + 3, Cnt4 + 1]).Font.Bold = true;
                //ServiceTaxNo
                Cnt3 = Cnt3 + 1;
                ExcelApp.Cells[Cnt3 + 3, Cnt4] = "Service Tax No :";
                ExcelApp.Cells[Cnt3 + 3, Cnt4 + 1] = lblServiceTaxNo.Text.ToString();
                ExcelApp.get_Range(ExcelApp.Cells[Cnt3 + 3, Cnt4], ExcelApp.Cells[Cnt3 + 3, Cnt4 + 1]).Font.Bold = true;
                
             
                #endregion
            #endregion
         
               //Microsoft.Office.Interop.Excel.Worksheet wc1=(Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Worksheets.get_Item(1);
               //wc1.Shapes.AddPicture(@"D:\AntLOGO", Microsoft.Office.Core.MsoTriState.msoFalse,  
               //Microsoft.Office.Core.MsoTriState.msoCTrue, 10, 10, 100, 100);
               //ExcelApp.ActiveWorkbook.SaveCopyAs(@"D:\" + DateTime.Today.ToString("dd MMM yyyy") + ".xls");
               ExcelApp.ActiveWorkbook.SaveCopyAs(@"D:\" + DateTime.Today.ToString("dd MMM yyyy") + ".xls");
               ExcelApp.ActiveWorkbook.Saved = true;
               ExcelApp.Quit();
        }
        catch (ThreadAbortException tex)
        {

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
