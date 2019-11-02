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

public partial class Reports_GoodTransferNote : System.Web.UI.Page
{
    #region[Variables]
        DMTransferLocation Obj_Trans = new DMTransferLocation();
        DMCompanyMaster obj_CM = new DMCompanyMaster();
        TransferLocation Entity_Trans = new TransferLocation();
        CommanFunction obj_Comm = new CommanFunction();
        DataSet DS = new DataSet();
        string TransferNum = string.Empty;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        int CategoryID;
        string CategoryName;
        public static bool flag = false;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetCompanyDetails();
            GetDamageDtls();
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

    public void GetDamageDtls()
    {
        try
        {
            int TransId = Convert.ToInt32(Request.QueryString["ID"]);
            DS = Obj_Trans.BindForPrint(TransId, out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    lblTransferNo.Text = obj_Comm.ToTitleCase(DS.Tables[0].Rows[0]["TransNo"].ToString());
                    lblTransferBy.Text = obj_Comm.ToTitleCase( Session["UserName"].ToString());
                    lblTransferDate.Text = obj_Comm.ToTitleCase(DS.Tables[0].Rows[0]["Date"].ToString());
                }
                if (DS.Tables[1].Rows.Count > 0)
                {
                    GridDetails.DataSource = DS.Tables[1];
                    GridDetails.DataBind();
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
        string fileName = "MaterialTranfrLocatn.pdf";
        fileName = fileName.Replace(' ', '_');
        string imgPath = Server.MapPath("~/Images/MasterPages/MayurLogo.png");
        Response.Clear();


        GeneratePDFNew(imgPath, fileName, "das", GridDetails, this.Page, "");
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

            #region[pdf]

            str = "<font size='12px' face='arial'><table width='100%' align='left'><tr><td colspan='4' ></td></tr>";

            str = str + "<tr><td colspan='4' align='left'><b>" + lblCompanyName.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='left'><b>" + lblCompanyAddress.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='left'><b>Phone No:" + lblPhnNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='left'><b>Fax No:" + lblFaxNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='center'><b>Material Transfer Details</b></td></tr>";

            str = str + "<tr><td colspan='4' ></td></tr>";
            str = str + "<tr><td><b>TransferNo :</b><b>" + lblTransferNo.Text + "</b></td><td><b>TransferDate :</b><b>" + lblTransferDate.Text + "<b></td><td colspan='2'></td></tr>";
            str = str + "<tr><td><b>Transfer By :</b><b>" + lblTransferBy.Text + "<b></td><td colspan='3'></td></tr>";
            str = str + "<tr><td colspan='4' align='Left'><b>" + "" + "</b></td></tr>";

            document.SetMargins(10, 10, 10, 10);
            document.Open();
            str = str + "<br><tr><td colspan='4' align='right'>" + GridViewToHtml(GridDetails) + "</td></tr><br>";
            str = str + "<br><tr><td colspan='4'><table Width='100%'><tr><td width='15%'>Prepared By</td><td></td><td width='15%'>Received By</td><td></td><td width='15%'>Authorised By</td></tr></br>";
            str = str + "<br><tr><td></td><td></td><td></td><td></td><td></td></tr></br>";
            str = str + "<br><tr><td></td><td></td><td></td><td></td><td></td></tr></br>";
            str = str + "<br><tr><td></td><td></td><td></td><td></td><td></td></tr></br>";
            str = str + "<br><tr><td>Name &amp; Designation</td><td></td><td>Name &amp; Designation</td><td></td><td>Name &amp; Designation</td></tr></br>";
            str = str + "<br><tr><td>Store Incharge</td><td>&nbsp;</td><td>Unit Manager / Store Incharge</td><td>&nbsp;</td><td>Unit Head / Operation Manager </td></tr></br>";
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
}
