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
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DALSQLHelper;
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

public partial class Reports_MaterialReqTemplatePrint : System.Web.UI.Page
{
    #region[Variables]
    DataSet DSdtls = new DataSet();
    DMRequisitionCafeteria obj_RequisitionCafeteria = new DMRequisitionCafeteria();
    DMCompanyMaster obj_CM= new DMCompanyMaster();
    RequisitionCafeteria Entity_Template = new RequisitionCafeteria();
    CommanFunction Obj_Comm = new CommanFunction();
    private string StrError = string.Empty;
    DataTable dttable = new DataTable();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetCompanyDetails();
            GetRequisition();
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
                }
                else
                {
                    lblCompanyName.Text = "";
                    lblCompanyAddress.Text = "";
                    lblPhnNo.Text = "";
                    imgAntTime.ImageUrl = "";
                    lblFaxNo.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void GetRequisition()
    {
        try
        {
            int POId = Convert.ToInt32(Request.QueryString["Id"]);
            DSdtls = obj_RequisitionCafeteria.BindForReport(POId, out StrError);
            ViewState["ImportExel"] = DSdtls.Tables[1];
            if (DSdtls.Tables.Count > 0)
            {
                if (DSdtls.Tables[0].Rows.Count > 0)
                {
                    lblReqNo.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["RequisitionNo"].ToString());
                    lblReqDate.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["RequisitionDate"].ToString());
                    lblEmp.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["UserName"].ToString());
                    LblPreareBy.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["UserName"].ToString());
                    LblAuthorisedBy.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["ApprovedBy"].ToString());
                    
                }
                if (DSdtls.Tables[1].Rows.Count > 0)
                {
                    lblAuthBy.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[1].Rows[0]["Location"].ToString());
                    ReqGrid.DataSource = DSdtls.Tables[1];
                    ReqGrid.DataBind();
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

        string fileName = "MatRequisitionDtls_details.pdf";
        fileName = fileName.Replace(' ', '_');
        string imgPath = Server.MapPath("~/Images/MasterPages/MayurLogo.png");
        Response.Clear();


        GeneratePDFNew(imgPath, fileName, "das", ReqGrid, this.Page, "");
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
            // oPage.Response.Clear();
            PdfWriter.GetInstance(document, oPage.Response.OutputStream);
            // generates the grid first
            StringBuilder strB = new StringBuilder();
            StringBuilder strB1 = new StringBuilder();
            string str = string.Empty;

            //str = "<font size='12px' face='arial'><table width='70%' align='left'><tr><td colspan='5' ></td></tr>";
            //str = str + "<tr><td colspan='5' width='30%' align='center'><b>Estimate Details</b></td></tr> <tr><td align='right' >Estimate No:</td><td><b>" + Obj_Comm.ToTitleCase(lblEstimateNo.Text) + "</b></td></tr>";
            //str = str + "<tr><td align='right' >Estimate Date:</td><td><b>" + Obj_Comm.ToTitleCase(lblEstimateDate.Text) + "</b></td></tr>";
            //str = str + "<tr><tr><td align='right' >Order By:</td><td><b>" + Obj_Comm.ToTitleCase(lblOrderBy.Text) + "</b></td></tr>";
            //str = str + "<tr><tr><td align='right' >Customer:</td><td><b>" + Obj_Comm.ToTitleCase(lblCustName.Text) + "</b></td></tr>";
            //str = str + "<tr><tr><td align='right' >Site:</td><td><b>" + Obj_Comm.ToTitleCase(lblSite.Text) + "</b></td></tr>";
            //str = str + "<tr><tr><td align='right' >Exp Delivery Date:</td><td><b>" + Obj_Comm.ToTitleCase(lblExpDelDate.Text) + "</b></td></tr>";
            //str = str + "<tr><tr><td align='right' >Order Status:</td><td><b>" + Obj_Comm.ToTitleCase(lblOrderStatus.Text) + "</b></td></tr>";

            str = "<font size='12px' face='arial'><table width='90%' align='left'>";

            str = str + "<tr><td colspan='4' align='left'><b>" + lblCompanyName.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='left'><b>" + lblCompanyAddress.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='left'><b>Phone No:" + lblPhnNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='left'><b>Fax No:" + lblFaxNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='Center'><b>Material Requisition</b></td></tr>";

            str = str + "<tr><td align='Right'><b>" + "Requisition No :" + "</b></td><td align='Left'><b>" + lblReqNo.Text + "</b></td><td  align='right'><b>" + "Requisition Date :" + "</b></td><td align='Left'><b>" + lblReqDate.Text + "</b></td></tr>";
            str = str + "<tr><td align='Right'><b></b></td><td align='Left'><b></b></td><td  align='right'><b>" + "Requisition By :" + "</b></td><td align='Left'><b>" + lblEmp.Text + "</b></td></tr>";

            str = str + "<tr><td colspan='4' align='Left'><b>" + "" + "</b></td></tr>";

            //str = str + "<tr><td colspan='3' align='Left'><b></b></td></tr>";
            // str = str + "<tr><td ><b>" + lblSuplier.Text + "</b></td><td colspan='2' align='Left'></td></tr>";
            // str = str + "<tr><td colspan='3' align='Left'><b>" + "" + "</b></td></tr>";

            document.SetMargins(10, 10, 10, 10);
            document.Open();
            str = str + "<br><tr><td colspan='4' align='right'>" + GridViewToHtml(ReqGrid) + "</td></tr><br>";
            str = str + "<br><tr><td colspan='4'><table><tr><td>Prepared By</td><td></td><td>Authorised By</td><td></td><td>Issued By</td><td></td><td>Received By</td></tr>";
            str = str + "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            str = str + "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            str = str + "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            str = str + "<tr><td width='100%'><b>Name &amp; Designation<b></td><td></td><td width='100%'><b>Name &amp; Designation<b></td><td></td><td width='100%'><b>Name &amp; Designation<b></td><td></td><td width='100%'><b>Name &amp; Designation<b></td></tr>";
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
    protected void ImgBtnExport_Click1(object sender, ImageClickEventArgs e)
    {
        Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        ExcelApp.Application.Workbooks.Add(Type.Missing);
        try
        {
            dttable = (DataTable)ViewState["ImportExel"];
            #region[For RequisitionDetails-Excel]

            #region[Image]
            Microsoft.Office.Interop.Excel.Range range = ExcelApp.get_Range("A1", "I1");
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
            Microsoft.Office.Interop.Excel.Range range1 = ExcelApp.get_Range("A2", "I2");
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
            #region[ReportName:Material Requisition Details]
            Microsoft.Office.Interop.Excel.Range range2 = ExcelApp.get_Range("A3", "I3");
            range2.Font.Size = 12;
            range2.Font.Bold = true;
            range2.Locked = true;
            range2.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            range2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            range2.RowHeight = 15;
            range2.Merge(true);
            range2.Value2 = "Material Requisition Details";
            range2.EntireColumn.ColumnWidth = 20;
            range2.EntireRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            range2.Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            #endregion
            #region[RequisitionDetails]
            Microsoft.Office.Interop.Excel.Range range3 = ExcelApp.get_Range("A4", "I4");
            range3.Font.Size = 12;
            range3.Font.Bold = true;
            range3.Locked = true;
            range3.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            range3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            range3.RowHeight = 40;
            range3.Merge(true);
            range3.Value2 = "Requisition No:" + lblReqNo.Text
                           + "\n" + "Requisition Date:" + lblReqDate.Text
                            + "\n" + "Requisition By:" + lblEmp.Text; ;
            range3.EntireColumn.ColumnWidth = 20;
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

            //Microsoft.Office.Interop.Excel.Worksheet wc1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Worksheets.get_Item(1);
            //wc1.Shapes.AddPicture(@"D:\AntLOGO", Microsoft.Office.Core.MsoTriState.msoFalse,
            //Microsoft.Office.Core.MsoTriState.msoCTrue, 10, 10, 100, 100);
            ExcelApp.ActiveWorkbook.SaveCopyAs(@"D:\" + DateTime.Today.ToString("dd MMM yyyy") + ".xls");
            ExcelApp.ActiveWorkbook.Saved = true;
            ExcelApp.Quit();
            #endregion
        }
        catch (ThreadAbortException tex)
        {

        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}
