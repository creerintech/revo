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

public partial class Reports_DamagePrint : System.Web.UI.Page
{
    #region[Variables]
    DataSet DSdtls = new DataSet();
    DMDamage obj_Damage = new DMDamage();
    DMCompanyMaster obj_CM= new DMCompanyMaster();
    Damage Entity_Damage = new Damage();
    CommanFunction Obj_Comm = new CommanFunction();
    private string StrError = string.Empty;
    private static bool Damagethrough = false;
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
            int DamageId = Convert.ToInt32(Request.QueryString["ID"]);
            DSdtls = obj_Damage.BindForPrint(DamageId, out StrError);
            if (DSdtls.Tables.Count > 0)
            {
                lblDamageNo.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["DamageNo"].ToString());
                lblDamageDate.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["DamageDate"].ToString());
                lblPreparedBy.Text = Obj_Comm.ToTitleCase(Session["UserName"].ToString());

                if (DSdtls.Tables[0].Rows.Count > 0)
                {
                    if (DSdtls.Tables[0].Rows[0]["DamagedThrough"].ToString() == "0")
                    {
                        lblInwardNo.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["InwardNo"].ToString());
                        lblInwardDate.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["InwardDate"].ToString());
                        lblPONo.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["PONo"].ToString());
                        lblSuppName.Text = Obj_Comm.ToTitleCase(DSdtls.Tables[0].Rows[0]["SuplierName"].ToString());
                        Damagethrough = true;
                    }
                    else
                    {
                        Damagethrough = false;
                        TR1.Visible = false;
                        TR2.Visible = false;
                    }
                }
                if (DSdtls.Tables[1].Rows.Count > 0)
                {
                    if (DSdtls.Tables[0].Rows[0]["DamagedThrough"].ToString() == "0")
                    {
                        GrdDamage.DataSource = DSdtls.Tables[1];
                        GrdDamage.DataBind();
                    }
                    else
                    {
                        GrdDamage.DataSource = DSdtls.Tables[1];
                        GrdDamage.DataBind();
                        GrdDamage.Columns[3].Visible = false;
                    }
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
        string fileName = "DamageDetails.pdf";
        fileName = fileName.Replace(' ', '_');
        string imgPath = Server.MapPath("~/Images/MasterPages/MayurLogo.png");
        Response.Clear();


        GeneratePDFNew(imgPath, fileName, "das", GrdDamage, this.Page, "");
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
            //str = "<font size='12px' face='arial'><table width='70%' align='left'><tr><td colspan='5' ></td></tr>";
            //str = str + "<tr><td colspan='5' width='30%' align='center'><b>Estimate Details</b></td></tr> <tr><td align='right' >Estimate No:</td><td><b>" + Obj_Comm.ToTitleCase(lblEstimateNo.Text) + "</b></td></tr>";
            //str = str + "<tr><td align='right' >Estimate Date:</td><td><b>" + Obj_Comm.ToTitleCase(lblEstimateDate.Text) + "</b></td></tr>";
            //str = str + "<tr><tr><td align='right' >Order By:</td><td><b>" + Obj_Comm.ToTitleCase(lblOrderBy.Text) + "</b></td></tr>";
            //str = str + "<tr><tr><td align='right' >Customer:</td><td><b>" + Obj_Comm.ToTitleCase(lblCustName.Text) + "</b></td></tr>";
            //str = str + "<tr><tr><td align='right' >Site:</td><td><b>" + Obj_Comm.ToTitleCase(lblSite.Text) + "</b></td></tr>";
            //str = str + "<tr><tr><td align='right' >Exp Delivery Date:</td><td><b>" + Obj_Comm.ToTitleCase(lblExpDelDate.Text) + "</b></td></tr>";
            //str = str + "<tr><tr><td align='right' >Order Status:</td><td><b>" + Obj_Comm.ToTitleCase(lblOrderStatus.Text) + "</b></td></tr>";

            str = "<font size='12px' face='arial'><table width='100%' align='left'><tr><td colspan='4' ></td></tr>";

            str = str + "<tr><td colspan='4' align='left'><b>" + lblCompanyName.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='left'><b>" + lblCompanyAddress.Text + "</b></td></tr>"; 
            str = str + "<tr><td colspan='4' align='left'><b>Phone No:" + lblPhnNo.Text + "</b></td></tr>";
            str = str + "<tr><td colspan='4' align='left'><b>Fax No:" + lblFaxNo.Text + "</b></td></tr>";

            str = str + "<tr><td colspan='4' align='center'><b>Damage Details</b></td></tr>";
            str = str + "<tr><td colspan='4' ></td></tr>";
            str = str + "<tr><td><b>DamageNo :</b><b>" + lblDamageNo.Text + "</b></td><td><b>DamageDate :</b><b>" + lblDamageDate.Text + "<b></td><td colspan='2'></td></tr>";
            str = str + "<tr><td><b>Prepared By :</b><b>"+lblPreparedBy.Text+"<b></td><td colspan='3'></td></tr>";
            if (Damagethrough == true)
            {
                str = str + "<tr><td><b>InwardNo :</b><b>" + lblInwardNo.Text + "</b></td><td><b>InwardDate :</b><b>" + lblInwardDate.Text + "<b></td><td colspan='2'></td></tr>";
                str = str + "<tr><td><b>PONo :</b><b>" + lblPONo.Text + "</b></td><td><b>SuppName :</b><b>" + lblSuppName.Text + "<b></td><td colspan='2'></td></tr>";
            }
            str = str + "<tr><td colspan='4' align='Left'><b>" + "" + "</b></td></tr>";

            document.SetMargins(10, 10, 10, 10);
            document.Open();
            str = str + "<br><tr><td colspan='4' align='right'>" + GridViewToHtml(GrdDamage) + "</td></tr><br>";
            str = str + "<br><tr><td colspan='4'><table><tr><td>Prepared By</td><td></td><td>Accepted By</td><td></td><td>Authorised By</td></tr>";
            str = str + "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            str = str + "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            str = str + "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            str = str + "<tr><td width='100%'><b>Name &amp; Designation<b></td><td></td><td width='100%'><b>Name &amp; Designation<b></td><td></td><td width='100%'><b>Name &amp; Designation<b></td></tr>";
            str = str + "<br><tr><td>Store Incharge</td><td></td><td>Unit Head/Operation Manager</td><td></td><td>Store Incharge</td></tr></br>";
            str = str + "</table></td></tr>";
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
