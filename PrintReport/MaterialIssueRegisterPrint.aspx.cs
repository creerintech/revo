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
using System.Drawing;
#region Pdf
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
#endregion
public partial class Reports_MaterialIssueRegisterPrint : System.Web.UI.Page
{
    #region[Variables]
    DataSet DSdtls = new DataSet();
    DMIssueRegister obj_IssueRegister = new DMIssueRegister();
    DMCompanyMaster obj_CM= new DMCompanyMaster();
    IssueRegister Entity_IssueRegister = new IssueRegister();
    CommanFunction Obj_Comm = new CommanFunction();
    private string StrError = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
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

    public void GetPurchaseOrder()
    {
        try
        {
            int POId = Convert.ToInt32(Request.QueryString["Id"]);
            DSdtls = obj_IssueRegister.GetIRForPrint(POId, out StrError);
            if (DSdtls.Tables.Count > 0)
            {
                if (DSdtls.Tables[0].Rows.Count > 0)
                {    
                    lblIssueNo.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["IssueNo"].ToString());
                    lblIssueDate.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["IssueDate"].ToString());
                    lblSuplier.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["Cafeteria"].ToString());
                    lblForRequisition.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["RequisitionNo"].ToString());
                    ReqDate.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["RequisitionDate"].ToString());
                    lblIssBy.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["UserName"].ToString());
                }
                if (DSdtls.Tables[1].Rows.Count > 0)
                {
                    IssueRegGrid.DataSource = DSdtls.Tables[1];
                    IssueRegGrid.DataBind();
                    getLastRow();
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
        string fileName = "Material Issue.pdf";
        fileName = fileName.Replace(' ', '_');
        string imgPath = Server.MapPath("~/Images/MasterPages/MayurLogo.png");
        Response.Clear();


        GeneratePDFNew(imgPath, fileName, "IssueReport", IssueRegGrid, this.Page, "");
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

            str = "<font size='12px' face='arial'><table width='90%' align='left' cellspacing='6'>";

            str = str + "<tr><td colspan='5' align='left'><b>" + lblCompanyName.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='left'><b>" + lblCompanyAddress.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='left'><b>Phone No:" + lblPhnNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='left'><b>Fax No:" + lblFaxNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='Center'><b>Material Issue Register Details </b></td></tr>";

            str = str + "<tr><td colspan='5' align='Left'></td></tr>";
            str = str + "<tr><td align='right'><b>" + "Issue No. :" + "</b></td><td align='Left'><b>" + lblIssueNo.Text + "</b></td><td ></td><td  align='right'><b>" + "For Requisition :" + "</b></td><td align='left'><b>" + lblForRequisition.Text + "</b></td></tr>";
            str = str + "<tr><td align='right'><b>" + "Issue Date :" + "</b></td><td align='Left'><b>" + lblIssueDate.Text + "</b></td><td ></td><td  align='right'><b>" + "For Requisition :" + "</b></td><td align='left'><b>" + ReqDate.Text + "</b></td></tr>";
            str = str + "<tr><td align='right'><b>" + "Issue From :" + "</b></td><td align='Left'><b>" + Label1.Text + "</b></td><td ></td><td  align='right'><b>" + "For Requisition :" + "</b></td><td align='left'><b>" + lblSuplier.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='5' align='Left'></td></tr>";
            str = str + "<tr><td colspan='5' align='Left'></td></tr>";
            document.SetMargins(10, 10, 10, 10);
            document.Open();
            str = str + "<br><tr><td colspan='5' align='right'>" + GridViewToHtml(IssueRegGrid) + "</td></tr><br>";
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
    public void getLastRow()
    {
        try
        {
            if (IssueRegGrid.Rows.Count > 0)
            {
                int rowposition = IssueRegGrid.Rows.Count - 1;
                IssueRegGrid.Rows[rowposition].BackColor = Color.FromName("#EFEFFB");//"#E56E94"
                IssueRegGrid.Rows[rowposition].Cells[2].Font.Bold = true;//.Appearance.ForeColor = Color.Red;
                IssueRegGrid.Rows[rowposition].Cells[2].ForeColor= Color.Black;
                IssueRegGrid.Rows[rowposition].Cells[3].Font.Bold=true;
                IssueRegGrid.Rows[rowposition].Cells[3].ForeColor = Color.Black;
                IssueRegGrid.Rows[rowposition].Cells[4].Font.Bold=true;
                IssueRegGrid.Rows[rowposition].Cells[4].ForeColor = Color.Black;
                IssueRegGrid.Rows[rowposition].Cells[5].Font.Bold = true;
                IssueRegGrid.Rows[rowposition].Cells[5].ForeColor = Color.Black;
            }
        }
        catch (Exception ex)
        {
            
        }
    }
}
