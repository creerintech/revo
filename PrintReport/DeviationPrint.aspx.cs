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
public partial class PrintReport_DeviationPrint : System.Web.UI.Page
{
    #region[Variables]
    DataSet DSdtls = new DataSet();
    DMDeviation obj_Deviation = new DMDeviation();
    DMCompanyMaster obj_CM = new DMCompanyMaster();
    Deviation Entity_Deviation = new Deviation();
    CommanFunction Obj_Comm = new CommanFunction();
    DataTable dttable = new DataTable();
    private string StrError = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetCompanyDetails();
            GetDeviation();
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

    public void GetDeviation()
    {
        try
        {
            int DeviationID = Convert.ToInt32(Request.QueryString["Id"]);
            DSdtls = obj_Deviation.GetInwardForPrint(DeviationID, out StrError);
            if (DSdtls.Tables.Count > 0)
            {
                if (DSdtls.Tables[0].Rows.Count > 0)
                {

                    lblDeviationNo.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["DeviationNo"].ToString());
                    lblDeviationDate.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["DeviationDate"].ToString());
                    lblPreparedBy.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["UserName"].ToString());
                    lblDaviationFrom.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["DevationPeriod"].ToString());                    
                }
                if (DSdtls.Tables[1].Rows.Count > 0)
                {
                    GrdDeviation.DataSource = DSdtls.Tables[1];
                    GrdDeviation.DataBind();
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
        string fileName = "Deviation-Details" + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".pdf";
        fileName = fileName.Replace(' ', '_');
        string imgPath = Server.MapPath("~/Images/MasterPages/MayurLogo.png");
        Response.Clear();

        GeneratePDFNew(imgPath, fileName, "das", GrdDeviation, this.Page, "");
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
            str = str + "<tr><td colspan='5' align='Center'><b>Material Deviation Details </b></td></tr>";

            str = str + "<tr><td align='right'><b>Deviation No:</b></td><td align='left'><b>" + lblDeviationNo.Text + "</b></td><td align='right'><b></b></td><td align='right'><b>Date:</b></td><td align='left'><b>" + lblDeviationDate.Text + "</b></td></tr>";
            str = str + "<tr><td align='right'><b>Deviation By:</b></td><td align='left'><b>" + lblPreparedBy.Text + "</b></td><td align='right'><b></b></td><td align='right'><b></b></td><td align='left'><b></b></td></tr>";
            str = str + "<tr><td align='right'><b>Deviation From:</b></td><td align='left' colspan='4'><b>" + lblDaviationFrom.Text + "</b></td></tr>";

            document.SetMargins(10, 10, 10, 10);
            document.Open();
            str = str + "<br><tr><td colspan='5' align='Center'>" + GridViewToHtml(GrdDeviation) + "</td></tr><br>";
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
        try
        {
            dttable = (DataTable)ViewState["ImportExel"];
            if (dttable.Rows.Count > 0)
            {
                //========Call Register
                GridView GridExp = new GridView();
                GridExp.DataSource = dttable;
                GridExp.DataBind();
                Obj_Comm.Export("Deviation-Details"+"_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".xls", GridExp);
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("No Data Found To Export..!", this.Page);
                dttable = null;
                GrdDeviation.DataSource = null;
                GrdDeviation.DataBind();
            }
        }
        catch (ThreadAbortException tex)
        {

        }
        catch (Exception ex)
        {

        }
    }
}