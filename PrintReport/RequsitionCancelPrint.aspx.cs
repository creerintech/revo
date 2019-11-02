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
#endregion 

public partial class Reports_PurchaseOrderPrint : System.Web.UI.Page
{
    #region[Variables]
    DataSet DSdtls = new DataSet();
    DMRequisitionCancellation obj_ReqCancel = new DMRequisitionCancellation();
    DMCompanyMaster obj_CM= new DMCompanyMaster();
    RequisitionCancellation Entity_ReqCancel = new RequisitionCancellation();
    CommanFunction Obj_Comm = new CommanFunction();
    private string StrError = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            GetCompanyDetails();
            GetDetails();
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

    public void GetDetails()
    {
        try
        {
            int ReqCancelId = Convert.ToInt32(Request.QueryString["ID"]);
            DSdtls = obj_ReqCancel.GetReqCancelPrint(ReqCancelId, out StrError);
            if (DSdtls.Tables.Count > 0)
            {
                if (DSdtls.Tables[0].Rows.Count > 0)
                {
                    lblReqCancelNo.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["RequisitionCancelNo"].ToString());
                    lblCancelBy.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["CancelledBy"].ToString());
                    lblCancelDate.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["CancelledDate"].ToString());
                }
                if (DSdtls.Tables[1].Rows.Count > 0)
                {
                    RequsitionGrid.DataSource = DSdtls.Tables[1];
                    RequsitionGrid.DataBind();
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
        string fileName = "RequisitionCancel.pdf";
        fileName = fileName.Replace(' ', '_');
        string imgPath = Server.MapPath("~/Images/MasterPages/MayurLogo.png");
        Response.Clear();


        GeneratePDFNew(imgPath, fileName, "das", RequsitionGrid, this.Page, "");
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
            str = str + "<tr><td colspan='4' align='Center'><b>Material Requisition Cancellation</b></td></tr>";
            str = str + "<tr><td align='Right'><b>" + "CancelledBy :" + "</b></td><td align='Left'><b>" + lblCancelBy.Text + "</b></td><td  align='right'><b>" + "Cancellation Date :" + "</b></td><td align='Left'><b>" + lblCancelDate.Text + "</b></td></tr>";
            str = str + "<tr><td align='Right'><b>" + "Requisition CancelNo: " + "</b></td><td align='Left'><b>" + lblReqCancelNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='Left'><b>" + "" + "</b></td></tr>";

            //str = str + "<tr><td colspan='3' align='Left'><b></b></td></tr>";
            // str = str + "<tr><td ><b>" + lblSuplier.Text + "</b></td><td colspan='2' align='Left'></td></tr>";
            // str = str + "<tr><td colspan='3' align='Left'><b>" + "" + "</b></td></tr>";

            document.SetMargins(10, 10, 10, 10);
            document.Open();
            str = str + "<br><tr><td colspan='4' align='right'>" + GridViewToHtml(RequsitionGrid) + "</td></tr><br>";
            str = str + "<br><tr><td align='left'>Prepared By:</td><td></td><td>Authorised By:</td><td></td></tr></br>";
            str = str + "<br><tr><td colspan='4'></td></tr></br>";
            str = str + "<br><tr><td colspan='4'></td></tr></br>";
            str = str + "<br><tr><td colspan='4'></td></tr></br>";
            str = str + "<br><tr><td><b>Name & Designation<b></td><td></td><td>Name & Designation</td><td></td></tr></br>";
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
                //iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/MasterPages/MayurLogo.png"));
                //document.Add(gif);
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
