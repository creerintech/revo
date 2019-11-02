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
using System.Threading;
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using CrystalDecisions.Shared;

public partial class CrystalPrint_PrintCryRpt : System.Web.UI.Page
{
    #region Variable
    public static string strCondition = string.Empty;
    public static string strError = string.Empty;
    DataSet dslogin = new DataSet();
    DataSet dsCompany = new DataSet();
    ReportDocument CRpt = new ReportDocument();
    static string[] EmpId;
    public string strmessage = string.Empty;
    public string PDFMaster = string.Empty;
    public static int Print_Flag = 0;
    #endregion

    
    protected void Page_InIt(object sender, EventArgs e)
    {
            Print_Flag = 0;
            PrintSalary();
    }

    public void PrintSalary()
    {
        try
        {
            string Flag = Convert.ToString(Request.QueryString["Flag"].ToString());

            #region[PS]





            if (Flag == "PS")
            {
                DMPurchaseOrder obj_PO = new DMPurchaseOrder();
                DMCompanyMaster obj_CM = new DMCompanyMaster();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dsCompany = obj_CM.CompanyDtlsOnPrint(out StrError);
                StrError = string.Empty;
                dslogin = obj_PO.GetPOForPrint(POId, out StrError);
                DataTable dt = new DataTable();
                dt = dslogin.Tables[0];

                DataTable dt3 = new DataTable();
                dt3 = dslogin.Tables[3];

                DataTable dt4 = new DataTable();
                dt4 = dslogin.Tables[4];

                dslogin.Tables[0].TableName = "PurchaseOrderMaster";
                dslogin.Tables[1].TableName = "PO";
                dslogin.Tables[2].TableName = "PurchaseOrderTerms";
                dslogin.Tables[3].TableName = "PurchaseOrderDetails";
                dslogin.Tables[4].TableName = "PurchaseOrderNetAMT";
                dslogin.Tables[5].TableName = "COMPANY";
                dslogin.Tables[7].TableName = "POFROMPAYMENT";
                dslogin.Tables[8].TableName = "SITENAME";

                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[5].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[5].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[5].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[5].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[5].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[5].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;
                if ((Request.QueryString["SFlag"].ToString()) == "Authorised")
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[5].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }
                }
                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[5].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[5].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }


                string URLPAHT = Server.MapPath("~/CrystalPrint/PrintCryRpt.aspx");
                dslogin.Tables[5].Columns.Add("PATHFORGETTINGPRINT", System.Type.GetType("System.String"));
                dslogin.Tables[5].Rows[0]["PATHFORGETTINGPRINT"] = URLPAHT;
                dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["PATHFORGETTINGPRINT"] = URLPAHT;
                //-------------------------------------------------------------------------------------------

                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                   // CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder.rpt"));
                    CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder_OLD.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    string sprintflag = string.Empty;
                    if ((Request.QueryString["SFlag"].ToString()) != "Authorised")
                    {
                        CrystalReportViewer1.DisplayToolbar = false;
                    }
                    else
                    {
                        CrystalReportViewer1.DisplayToolbar = true;
                    }
                    
                    dslogin = null;
                }
                else
                {
                    //CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder.rpt"));
                    CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder_OLD.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "PO - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                    
                }
            }

            #endregion
            #region[PS]





            if (Flag == "PS1")
            {
                Class1 obj_PO = new Class1();
                DMCompanyMaster obj_CM = new DMCompanyMaster();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dsCompany = obj_CM.CompanyDtlsOnPrint(out StrError);
                StrError = string.Empty;
                dslogin = obj_PO.GetPOForPrint(POId, out StrError);
                DataTable dt = new DataTable();
                dt = dslogin.Tables[0];

                DataTable dt3 = new DataTable();
                dt3 = dslogin.Tables[3];

                DataTable dt4 = new DataTable();
                dt4 = dslogin.Tables[4];

                dslogin.Tables[0].TableName = "PurchaseOrderMaster";
                dslogin.Tables[1].TableName = "PO";
                dslogin.Tables[2].TableName = "PurchaseOrderTerms";
                dslogin.Tables[3].TableName = "PurchaseOrderDetails";
                dslogin.Tables[4].TableName = "PurchaseOrderNetAMT";
                dslogin.Tables[5].TableName = "COMPANY";
                dslogin.Tables[7].TableName = "POFROMPAYMENT";
                dslogin.Tables[8].TableName = "SITENAME";

                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[5].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[5].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[5].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[5].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[5].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[5].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;
                if ((Request.QueryString["SFlag"].ToString()) == "Authorised")
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[5].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }
                }
                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[5].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[5].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }


                string URLPAHT = Server.MapPath("~/CrystalPrint/PrintCryRpt.aspx");
                dslogin.Tables[5].Columns.Add("PATHFORGETTINGPRINT", System.Type.GetType("System.String"));
                dslogin.Tables[5].Rows[0]["PATHFORGETTINGPRINT"] = URLPAHT;
                dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["PATHFORGETTINGPRINT"] = URLPAHT;
                //-------------------------------------------------------------------------------------------

                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    // CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder.rpt"));
                    CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder_OLD.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    string sprintflag = string.Empty;
                    if ((Request.QueryString["SFlag"].ToString()) != "Authorised")
                    {
                        CrystalReportViewer1.DisplayToolbar = false;
                    }
                    else
                    {
                        CrystalReportViewer1.DisplayToolbar = true;
                    }

                    dslogin = null;
                }
                else
                {
                    //CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder.rpt"));
                    CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "PO - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);

                }
            }

            #endregion

            #region[PS]





            if (Flag == "PS2")
            {
                servicepo obj_PO = new servicepo();
                DMCompanyMaster obj_CM = new DMCompanyMaster();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dsCompany = obj_CM.CompanyDtlsOnPrint(out StrError);
                StrError = string.Empty;
                dslogin = obj_PO.GetPOForPrint(POId, out StrError);
                DataTable dt = new DataTable();
                dt = dslogin.Tables[0];

                DataTable dt3 = new DataTable();
                dt3 = dslogin.Tables[3];

                DataTable dt4 = new DataTable();
                dt4 = dslogin.Tables[4];

                dslogin.Tables[0].TableName = "PurchaseOrderMaster";
                dslogin.Tables[1].TableName = "PO";
                dslogin.Tables[2].TableName = "PurchaseOrderTerms";
                dslogin.Tables[3].TableName = "PurchaseOrderDetails";
                dslogin.Tables[4].TableName = "PurchaseOrderNetAMT";
                dslogin.Tables[5].TableName = "COMPANY";
                dslogin.Tables[7].TableName = "POFROMPAYMENT";
                dslogin.Tables[8].TableName = "SITENAME";

                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[5].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[5].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[5].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[5].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[5].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[5].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;
                if ((Request.QueryString["SFlag"].ToString()) == "Authorised")
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[5].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }
                }
                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[5].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[5].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }


                string URLPAHT = Server.MapPath("~/CrystalPrint/PrintCryRpt.aspx");
                dslogin.Tables[5].Columns.Add("PATHFORGETTINGPRINT", System.Type.GetType("System.String"));
                dslogin.Tables[5].Rows[0]["PATHFORGETTINGPRINT"] = URLPAHT;
                dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["PATHFORGETTINGPRINT"] = URLPAHT;
                //-------------------------------------------------------------------------------------------

                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    // CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder.rpt"));
                    CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrderservicepo.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    string sprintflag = string.Empty;
                    if ((Request.QueryString["SFlag"].ToString()) != "Authorised")
                    {
                        CrystalReportViewer1.DisplayToolbar = false;
                    }
                    else
                    {
                        CrystalReportViewer1.DisplayToolbar = true;
                    }

                    dslogin = null;
                }
                else
                {
                    //CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder.rpt"));
                    CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrderservicepo.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "PO - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);

                }
            }

            #endregion

            #region[CancelPS]
            if (Flag == "CPS")
            {
                DMPurchaseOrder obj_PO = new DMPurchaseOrder();
                DMCompanyMaster obj_CM = new DMCompanyMaster();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dsCompany = obj_CM.CompanyDtlsOnPrint(out StrError);
                StrError = string.Empty;
                dslogin = obj_PO.GetCPOForPrint(POId, out StrError);
                dslogin.Tables[0].TableName = "PurchaseOrderMaster";
                dslogin.Tables[1].TableName = "PO";
                dslogin.Tables[2].TableName = "PurchaseOrderTerms";
                dslogin.Tables[3].TableName = "PurchaseOrderDetails";
                dslogin.Tables[4].TableName = "PurchaseOrderNetAMT";
                dslogin.Tables[5].TableName = "COMPANY";
                dslogin.Tables[7].TableName = "POFROMPAYMENT";
                dslogin.Tables[8].TableName = "SITENAME";

                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[5].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[5].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[5].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[5].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[5].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[5].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;
                if ((Request.QueryString["SFlag"].ToString()) == "Authorised")
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[5].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }
                }
                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[5].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[5].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }


                string URLPAHT = Server.MapPath("~/CrystalPrint/PrintCryRpt.aspx");
                dslogin.Tables[5].Columns.Add("PATHFORGETTINGPRINT", System.Type.GetType("System.String"));
                dslogin.Tables[5].Rows[0]["PATHFORGETTINGPRINT"] = URLPAHT;
                dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["PATHFORGETTINGPRINT"] = URLPAHT;
                //-------------------------------------------------------------------------------------------

                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    // CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder.rpt"));
                    CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder_OLD_Cancel.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    string sprintflag = string.Empty;
                    if ((Request.QueryString["SFlag"].ToString()) != "Authorised")
                    {
                        CrystalReportViewer1.DisplayToolbar = false;
                    }
                    else
                    {
                        CrystalReportViewer1.DisplayToolbar = true;
                    }

                    dslogin = null;
                }
                else
                {
                    //CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder.rpt"));
                    CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder_OLD_Cancel.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "PO - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);

                }
            }

            #endregion

            #region[VPS]
            if (Flag == "VPS")
            {
                DMPurchaseOrder obj_PO = new DMPurchaseOrder();
                DMCompanyMaster obj_CM = new DMCompanyMaster();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dsCompany = obj_CM.CompanyDtlsOnPrint(out StrError);
                StrError = string.Empty;
                dslogin = obj_PO.GetPOForPrint(POId, out StrError);
                //dslogin.Merge(dsCompany);
                dslogin.Tables[0].TableName = "PurchaseOrderMasterP";
                dslogin.Tables[1].TableName = "POP";
                dslogin.Tables[2].TableName = "PurchaseOrderTermsP";
                dslogin.Tables[6].TableName = "PurchaseOrderDetailsP";
                dslogin.Tables[4].TableName = "PurchaseOrderNetAMTP";
                dslogin.Tables[5].TableName = "COMPANYP";
                dslogin.Tables[7].TableName = "POFROMPAYMENT";
                dslogin.Tables[8].TableName = "SITENAME";
                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[5].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[5].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[5].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[5].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[5].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[5].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[5].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;
                if ((Request.QueryString["SFlag"].ToString()) == "Authorised")
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[5].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }
                }
                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[5].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[5].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[5].Rows[dslogin.Tables[5].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }
                string Path = Server.MapPath(@"~/CrystalPrint/PrintCryRpt.aspx");
                string Path2 = HttpContext.Current.Request.Url.AbsoluteUri;
                int indexofCP = Path2.IndexOf("CrystalPrint");
                string Path3 = Path2.Substring(0, indexofCP-1);
                string Path1 = Path3 + "/CrystalPrint/PrintCryRpt.aspx";
                dslogin.Tables[6].Columns.Add("RequestPath", System.Type.GetType("System.String"));
                dslogin.Tables[6].Rows[0]["RequestPath"] = Path1;
                dslogin.Tables[6].Rows[dslogin.Tables[6].Rows.Count - 1]["RequestPath"] = Path1;
                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/VPurchaseOrder.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/VPurchaseOrder.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "VPO - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                }
            }

            #endregion

            #region[CS]
            if (Flag == "CS")
            {
                DMConsumptionMaster obj_Con = new DMConsumptionMaster();
                DMCompanyMaster obj_CM = new DMCompanyMaster();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dsCompany = obj_CM.CompanyDtlsOnPrint(out StrError);
                StrError = string.Empty;
                dslogin = obj_Con.GetConsumeStockForPrint(POId, out StrError);
              
                dslogin.Tables[0].TableName = "ConsumptionMaster";
                dslogin.Tables[1].TableName = "ConsumptionDtls";
                dslogin.Tables[2].TableName = "COMPANY";

                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;
               
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }
                
                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ConsumptionPrint.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ConsumptionPrint.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "CS - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                }
            }
            #endregion

            #region[Rs]
            if (Flag == "RS")
            {
                DMRequisitionCafeteria obj_RequisitionCafeteria = new DMRequisitionCafeteria();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dslogin = obj_RequisitionCafeteria.BindForReport(POId, out StrError);
                dslogin.Tables[0].TableName = "REQUESTMASTER";
                dslogin.Tables[1].TableName = "REQUESTDETAILS";
                dslogin.Tables[2].TableName = "COMPANY";

                DataTable dt = new DataTable();
                dt = dslogin.Tables[0];

                DataTable dt1 = new DataTable();
                dt1 = dslogin.Tables[1];
                DataTable dt2 = new DataTable();
                dt2 = dslogin.Tables[2];


                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;
             
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }
              
                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/REQUEST.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/REQUEST.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" +"REQUISITION - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                }
            }

            if (Flag == "RS1")
            {
                DMRequisitionCafeteria obj_RequisitionCafeteria = new DMRequisitionCafeteria();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dslogin = obj_RequisitionCafeteria.BindForReport(POId, out StrError);
                dslogin.Tables[0].TableName = "REQUESTMASTER";
                dslogin.Tables[1].TableName = "REQUESTDETAILS";
                dslogin.Tables[2].TableName = "COMPANY";

                DataTable dt = new DataTable();
                dt = dslogin.Tables[0];

                DataTable dt1 = new DataTable();
                dt1 = dslogin.Tables[1];
                DataTable dt2 = new DataTable();
                dt2 = dslogin.Tables[2];


                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                {
                    fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                    brs1 = new BinaryReader(fss1);
                    byte[] imgbyte1 = new byte[fss1.Length + 1];
                    imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                    dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                    brs1.Close();
                    fss1.Close();
                }

                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/REQUESTnewtest.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/REQUESTnewtest.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "REQUISITION - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                }
            }
            #endregion

            #region[VRS]
            if (Flag == "VRS")
            {
                DMRequisitionCafeteria obj_RequisitionCafeteria = new DMRequisitionCafeteria();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dslogin = obj_RequisitionCafeteria.BindForReport(POId, out StrError);
                dslogin.Tables[0].TableName = "REQUESTMASTER";
                dslogin.Tables[1].TableName = "REQUESTDETAILS";
                dslogin.Tables[2].TableName = "COMPANY";

                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                {
                    fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                    brs1 = new BinaryReader(fss1);
                    byte[] imgbyte1 = new byte[fss1.Length + 1];
                    imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                    dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                    brs1.Close();
                    fss1.Close();
                }

                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/VPOREQUEST.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/VPOREQUEST.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "REQUISITION - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                }
            }
            #endregion

            #region[IN]
            if (Flag == "IN")
            {
                DMMaterialInwardReg obj_RequisitionCafeteria = new DMMaterialInwardReg();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dslogin = obj_RequisitionCafeteria.GetInwardForPrint(POId, out StrError);
                dslogin.Tables[0].TableName = "INWARDMASTER";
                dslogin.Tables[1].TableName = "INWARDDETAILS";
                dslogin.Tables[2].TableName = "COMPANY";

                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                {
                    fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                    brs1 = new BinaryReader(fss1);
                    byte[] imgbyte1 = new byte[fss1.Length + 1];
                    imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                    dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                    brs1.Close();
                    fss1.Close();
                }

                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/INWARD.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/INWARD.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "INWARD - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                }
            }
            #endregion

            #region[DM]
            if (Flag == "DM")
            {
                DMMaterialInwardReg obj_RequisitionCafeteria = new DMMaterialInwardReg();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dslogin = obj_RequisitionCafeteria.GetInwardForPrint(POId, out StrError);
                dslogin.Tables[0].TableName = "REQUESTMASTER";
                dslogin.Tables[1].TableName = "REQUESTDETAILS";
                dslogin.Tables[2].TableName = "COMPANY";

                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                {
                    fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                    brs1 = new BinaryReader(fss1);
                    byte[] imgbyte1 = new byte[fss1.Length + 1];
                    imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                    dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                    brs1.Close();
                    fss1.Close();
                }

                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/INWARD.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/INWARD.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "INWARD - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                }
            }
            #endregion

            #region[RC]
            if (Flag == "RC")
            {
                DMRequisitionCancellation obj_RequisitionCancellation= new DMRequisitionCancellation();
                int ReqCancelId = Convert.ToInt32(Request.QueryString["ID"]);
                string StrError = string.Empty;
                dslogin = obj_RequisitionCancellation.GetReqCancelPrint(ReqCancelId, out StrError);
                dslogin.Tables[0].TableName = "CancellatnDtls";
                dslogin.Tables[1].TableName = "ReqDetails";
                dslogin.Tables[2].TableName = "COMPANY";

                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                {
                    fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                    brs1 = new BinaryReader(fss1);
                    byte[] imgbyte1 = new byte[fss1.Length + 1];
                    imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                    dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                    brs1.Close();
                    fss1.Close();
                }

                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/MatRequistnCancel.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/MatRequistnCancel.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "REQUISITION CANCEL - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                }
            }
            #endregion

            #region[IS]
            if (Flag == "IS")
            {
                DMStockMaster obj_RequisitionCafeteria = new DMStockMaster();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dslogin = obj_RequisitionCafeteria.GetStockForPrint(POId, out StrError);
                dslogin.Tables[0].TableName = "MASTER";
                dslogin.Tables[1].TableName = "DETAILS";
                dslogin.Tables[2].TableName = "COMPANY";

                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                {
                    fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                    brs1 = new BinaryReader(fss1);
                    byte[] imgbyte1 = new byte[fss1.Length + 1];
                    imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                    dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                    brs1.Close();
                    fss1.Close();
                }

                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ISSUE.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ISSUE.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "ISSUE - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                }
            }
            #endregion

            #region[Damage]
            if (Flag == "DAMAGE")
            {
                DMDamage obj_Damage = new DMDamage();
                int DamageId = Convert.ToInt32(Request.QueryString["ID"]);
                string StrError = string.Empty;
                dslogin = obj_Damage.BindForPrint(DamageId, out StrError);
                if (dslogin.Tables[0].Rows[0]["DamagedThrough"].ToString() == "0")
                {
                    dslogin.Tables[0].TableName = "InwDamage";
                    dslogin.Tables[1].TableName = "InwDamageDtls";
                    dslogin.Tables[2].TableName = "COMPANY";
                    #region[CompanyDtls]
                    //----------------------------Image Print---------------------------------------------------------------
                    string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                    string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                    string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                    string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                    dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                    dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                    dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                    dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                    Image = Image.Replace("~", "");
                    Image = Image.Replace("/", "\\");

                    ImageSign = ImageSign.Replace("~", "");
                    ImageSign = ImageSign.Replace("/", "\\");

                    ImageSign1 = ImageSign1.Replace("~", "");
                    ImageSign1 = ImageSign1.Replace("/", "\\");

                    ImageSign2 = ImageSign2.Replace("~", "");
                    ImageSign2 = ImageSign2.Replace("/", "\\");


                    FileStream fs;
                    BinaryReader br;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                    {
                        fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                        br = new BinaryReader(fs);
                        byte[] imgbyte = new byte[fs.Length + 1];
                        imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                        dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                        br.Close();
                        fs.Close();
                    }
                    FileStream fss1;
                    BinaryReader brs1;

                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }

                    FileStream fss2;
                    BinaryReader brs2;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                    {
                        fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                        brs2 = new BinaryReader(fss2);
                        byte[] imgbyte2 = new byte[fss2.Length + 1];
                        imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                        dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                        brs2.Close();
                        fss2.Close();
                    }
                    FileStream fss3;
                    BinaryReader brs3;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                    {
                        fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                        brs3 = new BinaryReader(fss3);
                        byte[] imgbyte3 = new byte[fss3.Length + 1];
                        imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                        dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                        brs3.Close();
                        fss3.Close();
                    }
                    #endregion
                    //-------------------------------------------------------------------------------------------
                    if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                    {
                        CRpt.Load(Server.MapPath("~/CrystalPrint/DAMAGE.rpt"));
                        CRpt.SetDataSource(dslogin);
                        CrystalReportViewer1.ReportSource = CRpt;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.DisplayToolbar = true;
                        dslogin = null;
                    }
                    else
                    {
                        CRpt.Load(Server.MapPath("~/CrystalPrint/DAMAGE.rpt"));
                        CRpt.SetDataSource(dslogin);
                        PDFMaster = Server.MapPath(@"~/TempFiles/" + "DAMAGE DATAILS - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                        CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                        Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                    }
                }
                else
                {
                    dslogin.Tables[0].TableName = "ItemDamage";
                    dslogin.Tables[1].TableName = "ItemDamageDtls";
                    dslogin.Tables[2].TableName = "COMPANY";

                    #region[CompanyDtls]
                    //----------------------------Image Print---------------------------------------------------------------
                    string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                    string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                    string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                    string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                    dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                    dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                    dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                    dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                    Image = Image.Replace("~", "");
                    Image = Image.Replace("/", "\\");

                    ImageSign = ImageSign.Replace("~", "");
                    ImageSign = ImageSign.Replace("/", "\\");

                    ImageSign1 = ImageSign1.Replace("~", "");
                    ImageSign1 = ImageSign1.Replace("/", "\\");

                    ImageSign2 = ImageSign2.Replace("~", "");
                    ImageSign2 = ImageSign2.Replace("/", "\\");


                    FileStream fs;
                    BinaryReader br;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                    {
                        fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                        br = new BinaryReader(fs);
                        byte[] imgbyte = new byte[fs.Length + 1];
                        imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                        dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                        br.Close();
                        fs.Close();
                    }
                    FileStream fss1;
                    BinaryReader brs1;

                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }

                    FileStream fss2;
                    BinaryReader brs2;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                    {
                        fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                        brs2 = new BinaryReader(fss2);
                        byte[] imgbyte2 = new byte[fss2.Length + 1];
                        imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                        dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                        brs2.Close();
                        fss2.Close();
                    }
                    FileStream fss3;
                    BinaryReader brs3;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                    {
                        fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                        brs3 = new BinaryReader(fss3);
                        byte[] imgbyte3 = new byte[fss3.Length + 1];
                        imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                        dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;

                        brs3.Close();
                        fss3.Close();
                    }
                    #endregion
                    //-------------------------------------------------------------------------------------------
                    if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                    {
                        CRpt.Load(Server.MapPath("~/CrystalPrint/DAMAGEITEM.rpt"));
                        CRpt.SetDataSource(dslogin);
                        CrystalReportViewer1.ReportSource = CRpt;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.DisplayToolbar = true;
                        dslogin = null;
                    }
                    else
                    {
                        CRpt.Load(Server.MapPath("~/CrystalPrint/DAMAGEITEM.rpt"));
                        CRpt.SetDataSource(dslogin);
                        PDFMaster = Server.MapPath(@"~/TempFiles/" + "DAMAGE DATAILS - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                        CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                        Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                    }
                }
            }
            #endregion

            #region[TRANSFER]
            if (Flag == "TRANSFER")
            {
                DMTransferLocation Obj_Trans = new DMTransferLocation();
                int TransId = Convert.ToInt32(Request.QueryString["ID"]);
                string StrError = string.Empty;
                dslogin = Obj_Trans.BindForPrint(TransId, out StrError);
                dslogin.Tables[0].TableName = "MASTER";
                dslogin.Tables[1].TableName = "DETAILS";
                dslogin.Tables[2].TableName = "COMPANY";
                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                {
                    fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                    brs1 = new BinaryReader(fss1);
                    byte[] imgbyte1 = new byte[fss1.Length + 1];
                    imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                    dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                    brs1.Close();
                    fss1.Close();
                }

                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ISSUE.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ISSUE.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "ISSUE - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                }
            }
            #endregion

            #region[RETURN]
            if (Flag == "RETURN")
            {
                DMReturn Obj_Trans = new DMReturn();
                int TransId = Convert.ToInt32(Request.QueryString["ID"]);
                string StrError = string.Empty;
                dslogin = Obj_Trans.BindForPrint(TransId, out StrError);
                dslogin.Tables[0].TableName = "MASTER";
                dslogin.Tables[1].TableName = "DETAILS";
                dslogin.Tables[3].TableName = "COMPANY";
                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[3].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[3].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[3].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[3].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[3].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[3].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[3].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[3].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[3].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[3].Rows[dslogin.Tables[3].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                {
                    fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                    brs1 = new BinaryReader(fss1);
                    byte[] imgbyte1 = new byte[fss1.Length + 1];
                    imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                    dslogin.Tables[3].Rows[0]["Sign1"] = imgbyte1;
                    dslogin.Tables[3].Rows[dslogin.Tables[3].Rows.Count - 1]["Sign1"] = imgbyte1;


                    brs1.Close();
                    fss1.Close();
                }

                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[3].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[3].Rows[dslogin.Tables[3].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[3].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[3].Rows[dslogin.Tables[3].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ReturnEntry.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ReturnEntry.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "Return - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                }
            }
            #endregion

            #region[RateComparison]
            if (Flag == "RATECOMP")
            {
                DMEditPurchaseOrder obj_PO = new DMEditPurchaseOrder();
                EditPurchaseOrder obj_CM = new EditPurchaseOrder();
                decimal POId = Convert.ToDecimal(Request.QueryString["Rate"]);
                string Cond = " and PO.POId= " + Convert.ToString(Request.QueryString["POID"]);
                string StrError = string.Empty;
                dslogin = obj_PO.GetRateCompare(Cond,POId,Convert.ToInt32(Request.QueryString["POID"]), out StrError);
                
                dslogin.Tables[1].TableName = "DETAILS";
                dslogin.Tables[2].TableName = "COMPANY";
                //----------------------------Image Print---------------------------------------------------------------
                
                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[1].Columns.Add("LatestRate", System.Type.GetType("System.String"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");

                for (int Mrow = 0; Mrow < dslogin.Tables[1].Rows.Count; Mrow++)
                {
                    if (Convert.ToInt32(dslogin.Tables[1].Rows[Mrow]["#"].ToString()) == Convert.ToInt32(Request.QueryString["POID"]))
                    {
                        dslogin.Tables[1].Rows[Mrow]["LatestRate"] = "Yes";
                    }
                    else
                    {
                        dslogin.Tables[1].Rows[Mrow]["LatestRate"] = "No";
                    }
                }

                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;
                if ((Request.QueryString["SFlag"].ToString()) == "Authorised")
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }
                }
                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }


                string URLPAHT = Server.MapPath("~/CrystalPrint/PrintCryRpt.aspx");
                dslogin.Tables[2].Columns.Add("PATHFORGETTINGPRINT", System.Type.GetType("System.String"));
                dslogin.Tables[2].Rows[0]["PATHFORGETTINGPRINT"] = URLPAHT;
                dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["PATHFORGETTINGPRINT"] = URLPAHT;
                //-------------------------------------------------------------------------------------------

                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/OLDRateCompatison.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    string sprintflag = string.Empty;
                    if ((Request.QueryString["SFlag"].ToString()) != "Authorised")
                    {
                        CrystalReportViewer1.DisplayToolbar = false;
                    }
                    else
                    {
                        CrystalReportViewer1.DisplayToolbar = true;
                    }

                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/OLDRateCompatison.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "RateComparison - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);

                }
            }

            #endregion

            #region[RateComparison]
            if (Flag == "RATECOMPV")
            {
                DMEditPurchaseOrder obj_PO = new DMEditPurchaseOrder();
                EditPurchaseOrder obj_CM = new EditPurchaseOrder();
                decimal POId = Convert.ToDecimal(Request.QueryString["Rate"]);
                string Cond = " and PO.POId= " + Convert.ToString(Request.QueryString["POID"]);
                string StrError = string.Empty;
                dslogin = obj_PO.GetRateCompare(Cond, POId, Convert.ToInt32(Request.QueryString["POID"]), out StrError);

                dslogin.Tables[1].TableName = "DETAILS";
                dslogin.Tables[2].TableName = "COMPANY";
                dslogin.Tables[3].TableName = "PurchaseOrderTerms";

                DataTable dt = new DataTable();
                dt = dslogin.Tables[0];

                DataTable dt1 = new DataTable();
                dt1 = dslogin.Tables[1];

                DataTable dt2 = new DataTable();
             
                dt2 = dslogin.Tables[2];

                DataTable dt3 = new DataTable();
                dt3 = dslogin.Tables[3];
                //----------------------------Image Print---------------------------------------------------------------

                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[1].Columns.Add("LatestRate", System.Type.GetType("System.String"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");

                for (int Mrow = 0; Mrow < dslogin.Tables[1].Rows.Count; Mrow++)
                {
                    if (Convert.ToInt32(dslogin.Tables[1].Rows[Mrow]["#"].ToString()) == Convert.ToInt32(Request.QueryString["POID"]))
                    {
                        dslogin.Tables[1].Rows[Mrow]["LatestRate"] = "Yes";
                    }
                    else
                    {
                        dslogin.Tables[1].Rows[Mrow]["LatestRate"] = "No";
                    }
                }

                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;
                if ((Request.QueryString["SFlag"].ToString()) == "Authorised")
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }
                }
                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }


                string URLPAHT = Server.MapPath("~/CrystalPrint/PrintCryRpt.aspx");
                dslogin.Tables[2].Columns.Add("PATHFORGETTINGPRINT", System.Type.GetType("System.String"));
                dslogin.Tables[2].Rows[0]["PATHFORGETTINGPRINT"] = URLPAHT;
                dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["PATHFORGETTINGPRINT"] = URLPAHT;
                //-------------------------------------------------------------------------------------------

                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/OLDRateCompatisonL.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    string sprintflag = string.Empty;
                    if ((Request.QueryString["SFlag"].ToString()) != "Authorised")
                    {
                        CrystalReportViewer1.DisplayToolbar = false;
                    }
                    else
                    {
                        CrystalReportViewer1.DisplayToolbar = true;
                    }

                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/OLDRateCompatisonL.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "RateComparisonP - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);

                    //CRpt.Load(Server.MapPath("~/CrystalPrint/OLDRateCompatisonL.rpt"));
                    //CRpt.SetDataSource(dslogin);
                    //CrystalReportViewer1.ReportSource = CRpt;
                    //CrystalReportViewer1.DataBind();
                    //string sprintflag = string.Empty;


                    CrystalReportViewer1.DisplayToolbar = true;
                    CrystalReportViewer1.DisplayGroupTree = false;
                    //if ((Request.QueryString["SFlag"].ToString()) != "Authorised")
                    //{
                    //    CrystalReportViewer1.DisplayToolbar = false;
                    //}
                    //else
                    //{
                    //    CrystalReportViewer1.DisplayToolbar = true;
                    //}

                    dslogin = null;
                }
            }

            #endregion

            #region [RateComparisionIndent]
            if(Flag == "RATECOMPIND")
            {
                DMIndentEmail obj_PO = new DMIndentEmail();
                IndentEmail obj_CM = new IndentEmail();
                decimal Rate = Convert.ToDecimal(Request.QueryString["Rate"]);
                decimal ReqId = Convert.ToDecimal(Request.QueryString["REQID"]);

                string Cond = " and RC.RequisitionCafeId= " + Convert.ToString(Request.QueryString["REQID"]);
                string StrError = string.Empty;
                dslogin = obj_PO.GetRateCompare(Rate, Convert.ToInt32(Request.QueryString["REQID"]), out StrError);

                dslogin.Tables[1].TableName = "DETAILS";
                dslogin.Tables[2].TableName = "COMPANY";
                //dslogin.Tables[3].TableName = "PurchaseOrderTerms";

                DataTable dt = new DataTable();
                dt = dslogin.Tables[0];

                DataTable dt1 = new DataTable();
                dt1 = dslogin.Tables[1];

                DataTable dt2 = new DataTable();
                dt2 = dslogin.Tables[2];




                //----------------------------Image Print---------------------------------------------------------------

                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[1].Columns.Add("LatestRate", System.Type.GetType("System.String"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");

                for (int Mrow = 0; Mrow < dslogin.Tables[1].Rows.Count; Mrow++)
                {
                    if (Convert.ToInt32(dslogin.Tables[1].Rows[Mrow]["#"].ToString()) == Convert.ToInt32(Request.QueryString["POID"]))
                    {
                        dslogin.Tables[1].Rows[Mrow]["LatestRate"] = "Yes";
                    }
                    else
                    {
                        dslogin.Tables[1].Rows[Mrow]["LatestRate"] = "No";
                    }
                }
                FileStream fss1;
                BinaryReader brs1;
                if ((Request.QueryString["SFlag"].ToString()) == "Authorised")
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }
                }
                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }


                string URLPAHT = Server.MapPath("~/CrystalPrint/PrintCryRpt.aspx");
                dslogin.Tables[2].Columns.Add("PATHFORGETTINGPRINT", System.Type.GetType("System.String"));
                dslogin.Tables[2].Rows[0]["PATHFORGETTINGPRINT"] = URLPAHT;
                dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["PATHFORGETTINGPRINT"] = URLPAHT;
                //-------------------------------------------------------------------------------------------


                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/RateComparisionIndent.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    string sprintflag = string.Empty;
                    if ((Request.QueryString["SFlag"].ToString()) != "Authorised")
                    {
                        CrystalReportViewer1.DisplayToolbar = false;
                    }
                    else
                    {
                        CrystalReportViewer1.DisplayToolbar = true;
                    }

                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/RateComparisionIndent.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "RateComparisonInd - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);




                    CrystalReportViewer1.DisplayToolbar = true;
                    CrystalReportViewer1.DisplayGroupTree = false;


                    dslogin = null;
                }
            }

            #endregion

            #region[Item Master Print Out]
            if (Flag == "IMP")
            {
                DMItemMaster Obj_Trans = new DMItemMaster();
                string TransCate = Convert.ToString(Request.QueryString["SC"]);
                string TransSubCate = Convert.ToString(Request.QueryString["SSC"]);
                string TransItem = Convert.ToString(Request.QueryString["SI"]);
                string cond = "";
                if (!string.IsNullOrEmpty(TransCate))
                {
                    cond = cond + " AND F.CategoryName='" + TransCate + "'";
                    if (!string.IsNullOrEmpty(TransSubCate))
                    {
                        cond = cond + " AND F.SubCategory='" + TransSubCate + "'";
                        if (!string.IsNullOrEmpty(TransItem))
                        {
                            cond = cond + " AND F.ItemName='" + TransItem + "'";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(TransSubCate))
                {
                    cond = cond + " AND F.SubCategory='" + TransSubCate + "'";
                    if (!string.IsNullOrEmpty(TransItem))
                    {
                        cond = cond + " AND F.ItemName='" + TransItem + "'";
                    }
                }
                if (!string.IsNullOrEmpty(TransItem))
                {
                    cond = cond + " AND F.ItemName='" + TransItem + "'";
                }
                string StrError = string.Empty;
                dslogin = Obj_Trans.GetItemForPrint(cond, out StrError);
                dslogin.Tables[0].TableName = "MASTER";
                dslogin.Tables[1].TableName = "COMPANY";
                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[1].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[1].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[1].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[1].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[1].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[1].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[1].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[1].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[1].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[1].Rows[dslogin.Tables[1].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                {
                    fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                    brs1 = new BinaryReader(fss1);
                    byte[] imgbyte1 = new byte[fss1.Length + 1];
                    imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                    dslogin.Tables[1].Rows[0]["Sign1"] = imgbyte1;
                    dslogin.Tables[1].Rows[dslogin.Tables[1].Rows.Count - 1]["Sign1"] = imgbyte1;


                    brs1.Close();
                    fss1.Close();
                }

                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[1].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[1].Rows[dslogin.Tables[1].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[1].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[1].Rows[dslogin.Tables[1].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                //if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                //{
                    //CRpt.Load(Server.MapPath("~/CrystalPrint/ItemMaster.rpt"));
                if (Convert.ToString(Request.QueryString["ACTION"]) == "1")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ItemMaster.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
               else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ItemMasterDtls.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                //}
                //else
                //{
                //    CRpt.Load(Server.MapPath("~/CrystalPrint/ISSUE.rpt"));
                //    CRpt.SetDataSource(dslogin);
                //    PDFMaster = Server.MapPath(@"~/TempFiles/" + "ISSUE - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                //    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                //    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                //}
            }
            #endregion


            #region[Template]
            if (Flag == "TM")
            {
                DMTemplate obj_Template = new DMTemplate();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dslogin = obj_Template.BindForReport(POId, out StrError);
                dslogin.Tables[0].TableName = "TemplateMaster";
                dslogin.Tables[1].TableName = "TemplateDetails";
                dslogin.Tables[2].TableName = "COMPANY";

                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[2].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[2].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[2].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[2].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[2].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[2].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[2].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                {
                    fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                    brs1 = new BinaryReader(fss1);
                    byte[] imgbyte1 = new byte[fss1.Length + 1];
                    imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                    dslogin.Tables[2].Rows[0]["Sign1"] = imgbyte1;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                    brs1.Close();
                    fss1.Close();
                }

                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[2].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[2].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[2].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/TemplateCrRpt.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/TemplateCrRpt.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "TEMPLATE - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                }
            }
            #endregion

            #region[Client Company Master]
            if (Flag == "CCM")
            {
                DMClienCompanyMaster obj_Template = new DMClienCompanyMaster();
                int POId = Convert.ToInt32(Request.QueryString["Id"]);
                string StrError = string.Empty;
                dslogin = obj_Template.GetCCMFORPRINT(POId,1, out StrError);
                dslogin.Tables[0].TableName = "CLIENTCOMPAY";
                dslogin.Tables[1].TableName = "COMPANY";

                //----------------------------Image Print---------------------------------------------------------------
                string Image = dslogin.Tables[1].Rows[0]["CLogo"].ToString();
                string ImageSign = dslogin.Tables[1].Rows[0]["DigitalSignature"].ToString();
                string ImageSign1 = dslogin.Tables[1].Rows[0]["DigitalSignature1"].ToString();
                string ImageSign2 = dslogin.Tables[1].Rows[0]["DigitalSignature2"].ToString();
                dslogin.Tables[1].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[1].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[1].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                dslogin.Tables[1].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                Image = Image.Replace("~", "");
                Image = Image.Replace("/", "\\");

                ImageSign = ImageSign.Replace("~", "");
                ImageSign = ImageSign.Replace("/", "\\");

                ImageSign1 = ImageSign1.Replace("~", "");
                ImageSign1 = ImageSign1.Replace("/", "\\");

                ImageSign2 = ImageSign2.Replace("~", "");
                ImageSign2 = ImageSign2.Replace("/", "\\");


                FileStream fs;
                BinaryReader br;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                    br = new BinaryReader(fs);
                    byte[] imgbyte = new byte[fs.Length + 1];
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                    dslogin.Tables[1].Rows[0]["CompanyLogo"] = imgbyte;
                    dslogin.Tables[1].Rows[dslogin.Tables[2].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                    br.Close();
                    fs.Close();
                }
                FileStream fss1;
                BinaryReader brs1;

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                {
                    fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                    brs1 = new BinaryReader(fss1);
                    byte[] imgbyte1 = new byte[fss1.Length + 1];
                    imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                    dslogin.Tables[1].Rows[0]["Sign1"] = imgbyte1;
                    dslogin.Tables[1].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign1"] = imgbyte1;


                    brs1.Close();
                    fss1.Close();
                }

                FileStream fss2;
                BinaryReader brs2;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                {
                    fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                    brs2 = new BinaryReader(fss2);
                    byte[] imgbyte2 = new byte[fss2.Length + 1];
                    imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                    dslogin.Tables[1].Rows[0]["Sign2"] = imgbyte2;
                    dslogin.Tables[1].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign2"] = imgbyte2;


                    brs2.Close();
                    fss2.Close();
                }
                FileStream fss3;
                BinaryReader brs3;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                {
                    fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                    brs3 = new BinaryReader(fss3);
                    byte[] imgbyte3 = new byte[fss3.Length + 1];
                    imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                    dslogin.Tables[1].Rows[0]["Sign3"] = imgbyte3;
                    dslogin.Tables[1].Rows[dslogin.Tables[2].Rows.Count - 1]["Sign3"] = imgbyte3;


                    brs3.Close();
                    fss3.Close();
                }

                //-------------------------------------------------------------------------------------------
                if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ClientCompanyPrint.rpt"));
                    CRpt.SetDataSource(dslogin);
                    CrystalReportViewer1.ReportSource = CRpt;
                    CrystalReportViewer1.DataBind();
                    CrystalReportViewer1.DisplayToolbar = true;
                    dslogin = null;
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ClientCompanyPrint.rpt"));
                    CRpt.SetDataSource(dslogin);
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "ClientCompay - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster,true);
                }
            }
            #endregion

            #region offer
            if (Flag == "offer")
            {

                try {
                    DMRequisitionCafeteria obj_RequisitionCafeteria = new DMRequisitionCafeteria();
                    string POId = (Request.QueryString["Id"]);
                    string StrError = string.Empty;
                    dslogin = obj_RequisitionCafeteria.BindForReportoffer(POId, out StrError);
                    dslogin.Tables[0].TableName = "REQUESTMASTER";
                     
                    dslogin.Tables[1].TableName = "COMPANY";
                    dslogin.Tables[2].TableName = "REQUESTDETAILS";

                    DataTable dt = new DataTable();
                    dt = dslogin.Tables[0];

                    DataTable dt1 = new DataTable();
                    dt1 = dslogin.Tables[1];
                    DataTable dt2 = new DataTable();
                    dt2 = dslogin.Tables[1];


                    //----------------------------Image Print---------------------------------------------------------------
                    string Image = dslogin.Tables[1].Rows[0]["CLogo"].ToString();
                    string ImageSign = dslogin.Tables[1].Rows[0]["DigitalSignature"].ToString();
                    string ImageSign1 = dslogin.Tables[1].Rows[0]["DigitalSignature1"].ToString();
                    string ImageSign2 = dslogin.Tables[1].Rows[0]["DigitalSignature2"].ToString();
                    dslogin.Tables[1].Columns.Add("CompanyLogo", System.Type.GetType("System.Byte[]"));
                    dslogin.Tables[1].Columns.Add("Sign1", System.Type.GetType("System.Byte[]"));
                    dslogin.Tables[1].Columns.Add("Sign2", System.Type.GetType("System.Byte[]"));
                    dslogin.Tables[1].Columns.Add("Sign3", System.Type.GetType("System.Byte[]"));
                    Image = Image.Replace("~", "");
                    Image = Image.Replace("/", "\\");

                    ImageSign = ImageSign.Replace("~", "");
                    ImageSign = ImageSign.Replace("/", "\\");

                    ImageSign1 = ImageSign1.Replace("~", "");
                    ImageSign1 = ImageSign1.Replace("/", "\\");

                    ImageSign2 = ImageSign2.Replace("~", "");
                    ImageSign2 = ImageSign2.Replace("/", "\\");


                    FileStream fs;
                    BinaryReader br;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Image))
                    {
                        //fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Image, FileMode.Open);

                        //br = new BinaryReader(fs);
                        //byte[] imgbyte = new byte[fs.Length + 1];
                        //imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                        //dslogin.Tables[1].Rows[0]["CompanyLogo"] = imgbyte;
                        //dslogin.Tables[1].Rows[dslogin.Tables[1].Rows.Count - 1]["CompanyLogo"] = imgbyte;


                        //br.Close();
                        //fs.Close();
                    }
                    FileStream fss1;
                    BinaryReader brs1;

                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign))
                    {
                        fss1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign, FileMode.Open);

                        brs1 = new BinaryReader(fss1);
                        byte[] imgbyte1 = new byte[fss1.Length + 1];
                        imgbyte1 = brs1.ReadBytes(Convert.ToInt32((fss1.Length)));

                        dslogin.Tables[1].Rows[0]["Sign1"] = imgbyte1;
                        dslogin.Tables[1].Rows[dslogin.Tables[1].Rows.Count - 1]["Sign1"] = imgbyte1;


                        brs1.Close();
                        fss1.Close();
                    }

                    FileStream fss2;
                    BinaryReader brs2;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign1))
                    {
                        fss2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign1, FileMode.Open);

                        brs2 = new BinaryReader(fss2);
                        byte[] imgbyte2 = new byte[fss2.Length + 1];
                        imgbyte2 = brs2.ReadBytes(Convert.ToInt32((fss2.Length)));

                        dslogin.Tables[1].Rows[0]["Sign2"] = imgbyte2;
                        dslogin.Tables[1].Rows[dslogin.Tables[1].Rows.Count - 1]["Sign2"] = imgbyte2;


                        brs2.Close();
                        fss2.Close();
                    }
                    FileStream fss3;
                    BinaryReader brs3;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ImageSign2))
                    {
                        fss3 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ImageSign2, FileMode.Open);

                        brs3 = new BinaryReader(fss3);
                        byte[] imgbyte3 = new byte[fss3.Length + 1];
                        imgbyte3 = brs3.ReadBytes(Convert.ToInt32((fss3.Length)));

                        dslogin.Tables[1].Rows[0]["Sign3"] = imgbyte3;
                        dslogin.Tables[1].Rows[dslogin.Tables[1].Rows.Count - 1]["Sign3"] = imgbyte3;


                        brs3.Close();
                        fss3.Close();
                    }

                    //-------------------------------------------------------------------------------------------
                    if ((Request.QueryString["PDFFlag"].ToString()) == "NOPDF")
                    {
                        CRpt.Load(Server.MapPath("~/CrystalPrint/offer.rpt"));
                        CRpt.SetDataSource(dslogin);
                        CrystalReportViewer1.ReportSource = CRpt;
                        CrystalReportViewer1.DataBind();
                        CrystalReportViewer1.DisplayToolbar = true;
                        dslogin = null;





                    }
                    else
                    {
                        CRpt.Load(Server.MapPath("~/CrystalPrint/offer.rpt"));
                        CRpt.SetDataSource(dslogin);
                        PDFMaster = Server.MapPath(@"~/TempFiles/" + "REQUISITION - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
                        CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                        Response.Redirect("ShowPDF.aspx?Id=" + PDFMaster);
                    }
                }
                catch(Exception ex)
                {

                }
                }
            #endregion

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        finally
        {
            
        }

    }

}
