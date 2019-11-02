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


public partial class Reports_DebitNote : System.Web.UI.Page
{
    #region[Variables]
        DataSet DSdtls = new DataSet();
        DMReturn obj_Return = new DMReturn();
        DMCompanyMaster obj_CM= new DMCompanyMaster();
        Return Entity_Return = new Return();
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
            DSdtls = obj_Return.BindForPrint(POId, out StrError);
            if (DSdtls.Tables.Count > 0)
            {
                if (DSdtls.Tables[0].Rows.Count > 0)
                {
                    lblDebitNoteNo.Text = (DSdtls.Tables[0].Rows[0]["DebotNoteNo"].ToString());
                    lblDate.Text = (DSdtls.Tables[0].Rows[0]["ReturnDate"].ToString());
                    lblSuplier.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["VendorName"].ToString());
                    lblSupAddress.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["Address"].ToString());
                    lblSysDate.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["Date"].ToString());
                    lblSysTime.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["Time"].ToString());
                }
                if (DSdtls.Tables[1].Rows.Count > 0)
                {
                    PurOrderGrid.DataSource = DSdtls.Tables[1];
                    PurOrderGrid.DataBind();
                    ViewState["ImportExel"] = DSdtls.Tables[1];
                }
                if (DSdtls.Tables[2].Rows.Count > 0)
                {
                    LblNetAmt.Text = (DSdtls.Tables[2].Rows[0]["Amount"].ToString())+" /-";
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
        string fileName = "DebitNote" + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".pdf";
        fileName = fileName.Replace(' ', '_');
        string imgPath = Server.MapPath("~/Images/MasterPages/MayurLogo.png");
        Response.Clear();

        GeneratePDFNew(imgPath, fileName, "das", PurOrderGrid, this.Page, "");
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
        Response.Flush();
        Response.End();
    }
    public void GeneratePDFNew(string Imagepath, string fileName, string HeaderName, GridView GridReport,  Page oPage, string text)
    {
        var document = new Document(PageSize.A4, 50, 50, 25, 25);
        try
        {
            PdfWriter.GetInstance(document, oPage.Response.OutputStream);
            // generates the grid first
            StringBuilder strB = new StringBuilder();
            StringBuilder strB1 = new StringBuilder();
            string str = string.Empty;

            str = "<font size='12px' face='arial'><table width='100%' align='left' cellspacing='-40' >";

            str = str + "<tr><td colspan='4' align='left'><b>" + lblCompanyName.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='2' align='left'><b>" + lblCompanyAddress.Text + "</b></td><td align='right' ><b>Debit Note No. :</b></td><td align='left' ><b>" + lblDebitNoteNo.Text + "</b></td> </tr>";

            str = str + "<tr><td colspan='2' align='left'><b>Phone No:" + lblPhnNo.Text + "</b></td><td align='right'  ><b>Date :</b></td><td align='left'  ><b>" + lblDate.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='2' align='left'><b>Fax No:" + lblFaxNo.Text + "</b></td><td align='right' ><b>Site Name :</b></td><td align='left' ><b> EON KHARADI</b></td></tr>";
            str = str + "<tr><td colspan='4' align='Center'><b></b></td></tr>";
            str = str + "<tr><td colspan='4' align='Center' style='background-color:Red'><b>Debit Note</b></td></tr>";
            str = str + "<tr><td colspan='4' align='Center'><b></b></td></tr>";
            str = str + "<tr><td colspan='4' align='Left'><b>To,</b></td></tr>";
            str = str + "<tr><td colspan='4' align='Left' ><b>" + lblSuplier.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='3' align='Left'><b>" + lblSupAddress.Text + "</b></td><td  ></td></tr>";
            str = str + "<tr><td colspan='4' align='Left'><b>" + "" + "</b></td></tr>";
            str = str + "<tr><td colspan='1' align='Left'><b>Reason For Debit Note :</b></td><td align='right' colspan='3'></td></tr>";
            str = str + "<tr><td colspan='2' align='left'><b>Date :" + lblDate.Text + " - " + lblSysTime.Text + "</b></td><td colspan='2' align='left'></td></tr>";

            str = str + "<tr><td colspan='4' align='Left'><b></b></td></tr>";
            str = str + "<tr><td colspan='4' align='Left'><b></b></td></tr>";
            document.SetMargins(10, 10, 10, 10);
            document.Open();
            str = str + "<br><tr><td colspan='4' align='Right'>" + GridViewToHtml(PurOrderGrid) + "</td></tr></br>";
            str = str + "<tr><td colspan='4' align='Left'><b>" + "" + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='Left'><b>" + "" + "</b></td></tr>";
            str = str + "<br><tr><td colspan='4' align='right'><b>Net Amount  :" + LblNetAmt.Text + "</b></td></tr></br>";
            str = str + "<br><tr><td colspan='4' align='Left'><b>Note For Any Discrepency in Material :</b></td></tr></br>";
            str = str + "<br><tr><td></td><td></td><td></td><td></td><td></td></tr></br>";
            str = str + "<br><tr><td></td><td></td><td></td><td></td><td></td></tr></br>";
            str = str + "<br><tr><td></td><td></td><td></td><td></td><td></td></tr></br>";
            str = str + "<br><tr><td></td><td></td><td></td><td></td><td></td></tr></br>";
            //------Newly Added ------
            str = str + "<br><tr><td colspan='7'><table width='100%'><tr><td></td><td></td><td></td><td></td><td></td></tr></br>";
            str = str + "<br><tr><td ></td><td></td><td ></td><td></td><td></td></tr></br>";
            str = str + "<br><tr><td>Prepared By</td><td></td><td >Accepted By</td><td></td><td >Authorised By</td></tr></br>";
            str = str + "<br><tr><td></td><td></td><td></td><td></td><td></td></tr></br>";
            str = str + "<br><tr><td></td><td></td><td></td><td></td><td></td></tr></br>";
            str = str + "<br><tr><td></td><td></td><td></td><td></td><td></td></tr></br>";
            str = str + "<br><tr><td></td><td></td><td></td><td></td><td></td></tr></br>";
            str = str + "<br><tr><td>Name &amp; Designation</td><td></td><td>Name &amp; Designation</td><td></td><td>Name &amp; Designation</td></tr></br>";
            str = str + "<br><tr><td>Store Incharge</td><td></td><td>Vendor Seal &amp; Signature</td><td></td><td>Unit Head/Operation Manager</td></tr></br>";
            str = str + "</table></td></tr></br>";
            str = str + "</table></font>";

            strB.Append(str);

            document.SetMargins(20, 20, 20, 20);
            document.Open();
            if (text.Length.Equals(0)) 
            {
                
                using (StringWriter sWriter = new StringWriter(strB))
                {
                    using (HtmlTextWriter htWriter = new HtmlTextWriter(sWriter))
                    {
                     
                    }
                }
            }
            else
            {
                strB.Append(text);
            }
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
