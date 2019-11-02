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
#endregion
public partial class Reports_MaterialReqTemplatePrint : System.Web.UI.Page
{
    #region[Variables]
    DataSet DSdtls = new DataSet();
    DMStockMaster Obj_StockMasterReport = new DMStockMaster();
    DMCompanyMaster obj_CM= new DMCompanyMaster();
    StockMaster Entity_StockMasterReport = new StockMaster();
    CommanFunction obj_Comman = new CommanFunction();
    private string StrError = string.Empty;
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
            DSdtls = Obj_StockMasterReport.GetStockForPrint(POId, out StrError);
            if (DSdtls.Tables.Count > 0)
            {
                if (DSdtls.Tables[0].Rows.Count > 0)
                {
                    lblStockNo.Text = obj_Comman.ToTitleCase(DSdtls.Tables[0].Rows[0]["StockNo"].ToString());
                    lblStockDate.Text = obj_Comman.ToTitleCase(DSdtls.Tables[0].Rows[0]["StockDate"].ToString());
                    lblRefNo.Text = obj_Comman.ToTitleCase(DSdtls.Tables[0].Rows[0]["Refno"].ToString());
                    lblfrom.Text = obj_Comman.ToTitleCase(DSdtls.Tables[0].Rows[0]["RequestFrom"].ToString());
                    LblPrepareBy.Text = obj_Comman.ToTitleCase(DSdtls.Tables[0].Rows[0]["UserName"].ToString());
                    LblIssusedBy.Text = obj_Comman.ToTitleCase(DSdtls.Tables[0].Rows[0]["UserName"].ToString());
                }
                if (DSdtls.Tables[1].Rows.Count > 0)
                {
                    
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
        string fileName = "Stock Assign.pdf";
        fileName = fileName.Replace(' ', '_');
        string imgPath = Server.MapPath("~/Images/MasterPages/MayurLogo.png");
        Response.Clear();


        GeneratePDFNew(imgPath, fileName, "Stock", ReqGrid, this.Page, "");
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

            str = "<font size='12px' face='arial'><table width='100%' align='left' cellspacing='6'>";

            str = str + "<tr><td colspan='5' align='left'><b>" + lblCompanyName.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='left'><b>" + lblCompanyAddress.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='left'><b>Phone No:" + lblPhnNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='left'><b>Fax No:" + lblFaxNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='Center'><b>Assign Stock Register</b></td></tr>";

            str = str + "<tr><td colspan='5' align='Left'></td></tr>";
            str = str + "<tr><td align='right'><b>" + "Stock No. :" + "</b></td><td align='Left'><b>" + lblStockNo.Text + "</b></td><td ></td><td  align='right'><b>" + "Stock Date:" + "</b></td><td align='left'><b>" + lblStockDate.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='Left'></td></tr>";
            str = str + "<tr><td colspan='5' align='Left'></td></tr>";
            document.SetMargins(10, 10, 10, 10);
            document.Open();
            str = str + "<br><tr><td colspan='5' align='right'>" + GridViewToHtml(ReqGrid) + "</td></tr><br>";
            str = str + "<br><tr><td colspan='5'><table width='100%'><tr><td colspan='2'>Prepared By</td><td></td><td colspan='2'>Authorised By</td><td></td><td colspan='2'>Issued By</td><td></td><td colspan='2'>Received By</td></tr></br>";
            str = str + "<br><tr><td colspan='2'></td><td></td><td colspan='2'></td><td></td><td colspan='2'></td><td></td><td colspan='2'></td></tr></br>";
            str = str + "<br><tr><td colspan='2'></td><td></td><td colspan='2'></td><td></td><td colspan='2'></td><td></td><td colspan='2'></td></tr></br>";
            str = str + "<br><tr><td colspan='2'></td><td></td><td colspan='2'></td><td></td><td colspan='2'></td><td></td><td colspan='2'></td></tr></br>";

            str = str + "<br><tr><td colspan='2'>Name & Designation</td><td></td><td colspan='2'>Name & Designation</td><td></td><td colspan='2'>Name & Designation</td><td></td><td colspan='2'>Name & Designation</td></tr></br>";
            str = str + "<br><tr><td colspan='2'>Store Incharge</td><td></td><td colspan='2'>Unit Head / Operation Manager </td><td></td><td colspan='2'>Store Incharge</td></tr></br>";
            str = str + "<br><tr><td colspan='2'></td><td></td><td colspan='2'></td><td></td><td colspan='2'></td><td></td><td colspan='2'></td></tr></br>";
            str = str + "<br><tr><td colspan='2'></td><td></td><td colspan='2'></td><td></td><td colspan='2'></td><td></td><td colspan='2'></td></tr></br>";

            str = str + "</table></td></tr></br>";
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
}
