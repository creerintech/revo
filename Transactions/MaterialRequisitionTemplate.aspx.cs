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
using AjaxControlToolkit;
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Net.Mail;
using System.Collections.Generic;
using System.Web.Services;

public partial class Transactions_MaterialRequisitionTemplate : System.Web.UI.Page
{
    #region [private variables]
    DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
    DMTemplate Obj_Template = new DMTemplate();
    RequisitionCafeteria Entity_RequisitionCafeteria = new RequisitionCafeteria();
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MayurInventory"].ConnectionString);
    CommanFunction obj_Comman = new CommanFunction();
    DataSet Ds = new DataSet();
    public static int rowindexes = 0;
    int valueofitem = 0;
    private static DataTable DSVendor = new DataTable();
    DataSet DsEdit = new DataSet();
    DataTable DtEditPO;
    public static int EditTemp = 1;
    int CategoryID, insertrow; bool fillgrid = false;
    bool flag = false; bool clear = false; bool flag1 = false; bool ItemDuplicate = false;
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    private static string Lbllocids;
    private static string TOSTRING = string.Empty;
    public decimal Rate = 0;
    public decimal TaxPer = 0;
    public decimal PerAssign = 0;
    public decimal Amount = 0;
    public decimal Subtotal = 0;
    public decimal Quantity = 0;
    public decimal CGST = 0;
    public decimal SGST = 0;
    public decimal IGST = 0;
    database db = new database();
    private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
    #endregion

    #region [User define Function]

    private void GETDATAFORMAIL(int From, int ClientCompanyID)
    {
        try
        {
            DMStockLocation Obj_SL = new DMStockLocation();
            DataSet DS = new DataSet();
            string To = string.Empty;
            string CC = string.Empty;
            string Body = string.Empty;
            int CompanyIndex = 1;
            DS = Obj_SL.GetStockLocation("", out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                for (int INDEX = 0; INDEX < DS.Tables[0].Rows.Count; INDEX++)
                {
                    if (Convert.ToInt32(DS.Tables[0].Rows[INDEX]["#"].ToString()) == Convert.ToInt32(Session["TransactionSiteID"].ToString()))
                    {
                        CompanyIndex = Convert.ToInt32(DS.Tables[0].Rows[INDEX]["CompanyID"].ToString());
                    }
                }
            }

            if (DS.Tables.Count > 0 && DS.Tables[1].Rows.Count > 0)
            {
                DDLKCMPY.DataSource = DS.Tables[1];
                DDLKCMPY.DataValueField = "CompanyId";
                DDLKCMPY.DataTextField = "CompanyName";
                DDLKCMPY.DataBind();
                DDLKCMPY.SelectedValue = CompanyIndex.ToString();
            }
            Obj_SL = null;
            DS = null;
            //  TXTKTO.Text = "";
            TXTKCC.Text = "";
            string description = Server.HtmlDecode("HELLO,<br /><br />PLEASE FIND ATTACHED AN INDENT OF THE MATERIAL REQUIRED BY US. PLEASE QUOTE US YOUR FINAL & LOWEST RATES FOR THE SAME.<br /><br />PLEASE MENTION THE DELIVERY SCHEDULE & ALSO MENTION ALL THE MISCELLANEOUS CHARGES. IF THE MATERIAL REQUIRED IS NOT AVAILABLE, THEN PLEASE SUGGEST ALTERNATIVE MATERIAL.<br /><br />REGARDS,<br />KARIA DEVELOPERS.").Replace("<br />", Environment.NewLine);
            TxtBody.Text = description;
            LBLID.Text = Convert.ToString(ViewState["MailID"]);
            GETPDF(Convert.ToInt32(ViewState["MailID"]));

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string GETPDF(int reqid)
    {
        DMRequisitionCafeteria obj_RequisitionCafeteria = new DMRequisitionCafeteria();
        string StrError = string.Empty;
        string PDFMaster = string.Empty;
        ReportDocument CRpt = new ReportDocument();
        DataSet dslogin = new DataSet();
        dslogin = obj_RequisitionCafeteria.BindForReport(reqid, out StrError);

        dslogin.Tables[0].TableName = "REQUESTMASTER";
        DataTable dt0 = new DataTable();
        dt0 = dslogin.Tables[0];

        dslogin.Tables[1].TableName = "REQUESTDETAILS";
        DataTable dt1 = new DataTable();
        dt1 = dslogin.Tables[1];

        dslogin.Tables[2].TableName = "COMPANY";
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
        CRpt.Load(Server.MapPath("~/CrystalPrint/REQUEST.rpt"));
        CRpt.SetDataSource(dslogin);
        string DATE = DateTime.Now.ToString("dd-MMM-yyyy ss");
        PDFMaster = Server.MapPath(@"~/TempFiles/" + "REQUISITION - " + DATE + ".pdf");
        CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
        CHKATTACHBROUCHER.Checked = true;
        CHKATTACHBROUCHER.Text = "Indent Details";
        CHKATTACHBROUCHER.ToolTip = PDFMaster;

        iframepdf.Attributes.Add("src", "../TempFiles/" + "REQUISITION - " + (DATE) + ".pdf");

        return PDFMaster;
    }

    #region OLD CODE
    //public void SendMail(int reqid)
    //{
    //    try
    //    {
    //        DataSet dslogin = new DataSet();
    //        Ds = new DataSet();
    //        Ds = Obj_RequisitionCafeteria.GETDATAFORMAIL(reqid, out StrError);
    //        TRLOADING.Visible = true;
    //        string path = GETPDF(reqid);

    //        if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
    //        {
    //            //-------------------------------------------------[Mail Code]-------------------------------------------------
    //            //string smtpServer = "smtp.gmail.com";
    //            //string userName = "revomms@gmail.com";
    //            //string password = "revo@123";

    //            string smtpServer = "smtp.mail.yahoo.com";
    //            string userName = "revosolutionpune@yahoo.com";
    //            string password = "revosacred12345";

    //            int cdoBasic = 1;
    //            int cdoSendUsingPort = 2;
    //            MailMessage msg = new MailMessage();
    //            if (userName.Length > 0)
    //            {
    //                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
    //                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 64);
    //                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
    //                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
    //                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", userName);
    //                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);
    //                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", true);

    //                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
    //                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
    //                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
    //                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
    //                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", userName);
    //                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);
    //                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", true);
    //            }
    //            string mystring = string.Empty;
    //            msg.To = TXTKTO.Text;
    //            msg.Cc = TXTKCC.Text;
    //            msg.Subject = TXTKSUBJECT.Text;
    //            msg.Body = TxtBody.Text;
    //         //   msg.From = "revomms@gmail.com";
    //            msg.From = "revosolutionpune@yahoo.com";
    //            String sFile = path;
    //            if (CHKATTACHBROUCHER.Checked == true)
    //            {
    //                msg.Attachments.Add(new System.Web.Mail.MailAttachment(sFile));
    //            }
    //            SmtpMail.SmtpServer = smtpServer;
    //            SmtpMail.Send(msg);
    //            obj_Comman.ShowPopUpMsg("Mail Send..", this.Page);
    //            //File.Delete(MailAttachPath);
    //            TRLOADING.Visible = false;
    //        }
    //        //  -------------------------------------------------[End Mail Code]-------------------------------------------------
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }

    //}

    //User Right Function===========
    #endregion

    //demo 
    //public void SendMail(int reqid)
    //{
    //    bool Flag = false;
    //    MailMessage Mail = new MailMessage();
    //    try
    //    {
    //        //Mail.To.Add("nehejitu@gmail.com");
    //        //Mail.From = new System.Net.Mail.MailAddress("purchase@kariadevelopers.com");
    //        //// Mail.From = new MailAddress("revosolutionpune@yahoo.com");Mail.From = new MailAddress("customercare@elica.in");
    //        //Mail.CC.Add("purchase@kariadevelopers.com");
    //        //Mail.Subject = "Warranty Registration";

    //        //Mail.Body = "Hello Test ";

    //        Ds = Obj_RequisitionCafeteria.GETDATAFORMAIL(reqid, out StrError);
    //        TRLOADING.Visible = true;
    //        string path = GETPDF(reqid);

    //        if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
    //        {
    //            Mail.To.Add("" + TXTKTO.Text + "");
    //            if (!string.IsNullOrEmpty(TXTKCC.Text))
    //            {
    //                Mail.CC.Add("" + TXTKCC.Text + ""); //= TXTKCC.Text;
    //            }
    //            Mail.Subject = TXTKSUBJECT.Text;
    //            Mail.Body = TxtBody.Text;
    //            Mail.From = new MailAddress("emailrevosoltion@gmail.com"); //"purchase@kariadevelopers.com";
    //            if (CHKATTACHBROUCHER.Checked == true)
    //            {
    //                //Mail.Attachments.Add(new System.Web.Mail.MailAttachment(sFile));
    //                //Mail.Attachments.Add(Mail.Attachments());
    //                Mail.Attachments.Add(new Attachment(path));

    //            }

    //            Mail.IsBodyHtml = true;
    //            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);
    //            smtp.Host = "smtp.gmail.com";
    //            smtp.Port = 25;
    //            smtp.UseDefaultCredentials = true;//true

    //            //smtpClient.UseDefaultCredentials = true;
    //            smtp.EnableSsl = true;
    //            smtp.UseDefaultCredentials = true;

    //            smtp.Credentials = new System.Net.NetworkCredential("emailrevosoltion@gmail.com", "revosacred12345");



    //            smtp.EnableSsl = false;//false
    //            smtp.Send(Mail);
    //            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
    //            Flag = true;
    //            int EmailStatus = Obj_RequisitionCafeteria.UpdateEmailStatus(reqid, out StrError);
    //            MakeEmptyForm();
    //            TRLOADING.Visible = false;
    //            //return Flag;
    //            //  -------------------------------------------------[End Mail Code]-------------------------------------------------
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }

    //}
    //end demo

    public void SendMail(int reqid)
    {
        bool Flag = false;
        MailMessage Mail = new MailMessage();
        try
        {
            //Mail.To.Add("nehejitu@gmail.com");
            //Mail.From = new System.Net.Mail.MailAddress("purchase@kariadevelopers.com");
            //// Mail.From = new MailAddress("revosolutionpune@yahoo.com");Mail.From = new MailAddress("customercare@elica.in");
            //Mail.CC.Add("purchase@kariadevelopers.com");
            //Mail.Subject = "Warranty Registration";

            //Mail.Body = "Hello Test ";

            Ds = Obj_RequisitionCafeteria.GETDATAFORMAIL(reqid, out StrError);
            TRLOADING.Visible = true;
            string path = GETPDF(reqid);

            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                //comment by Rohini
                //Mail.To.Add("" + TXTKTO.Text + "");



                if (!string.IsNullOrEmpty(TXTKCC.Text))
                {
                    Mail.CC.Add("revosolutionpune@yahoo.com"); //= TXTKCC.Text;
                }
                Mail.Subject = TXTKSUBJECT.Text;
                Mail.Body = TxtBody.Text;
                Mail.From = new MailAddress("revosolutions.office@gmail.com"); //"purchase@kariadevelopers.com";

                string mailTo = TXTKTO.SelectedValue.ToString();
                Mail.To.Add(new System.Net.Mail.MailAddress(mailTo));
                if (CHKATTACHBROUCHER.Checked == true)
                {
                    //Mail.Attachments.Add(new System.Web.Mail.MailAttachment(sFile));
                    //Mail.Attachments.Add(Mail.Attachments());
                    Mail.Attachments.Add(new Attachment(path));
                    //  Mail.Attachments.Add(new Attachment("~/ScannedDrawings/Kunal Passports.pdf"));
                }

                Mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.rediffmailpro.com", 587);
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;//true
                smtp.Credentials = new System.Net.NetworkCredential("revosolutions.office@gmail.com", "revo@2018");



                smtp.EnableSsl = true;//false
                smtp.Send(Mail);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                Flag = true;
                int EmailStatus = Obj_RequisitionCafeteria.UpdateEmailStatus(reqid, out StrError);
                MakeEmptyForm();
                TRLOADING.Visible = false;
                //return Flag;
                //  -------------------------------------------------[End Mail Code]-------------------------------------------------
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    public void CheckUserRight()
    {
        try
        {
            #region [USER RIGHT]
            //Checking Session Varialbels========
            if (Session["UserName"] != null && Session["UserRole"] != null)
            {
                //Checking User Role========
                //if (!Session["UserRole"].Equals("Administrator"))
                ////Checking Right of users=======
                //{
                System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                dsChkUserRight1 = (DataSet)Session["DataSet"];

                DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Requisition'");
                if (dtRow.Length > 0)
                {
                    DataTable dt = dtRow.CopyToDataTable();
                    dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                }
                if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                    Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false &&
                    Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                {
                    Response.Redirect("~/NotAuthUser.aspx");
                }
                //Checking View Right ========                    
                if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                {
                    ReportGrid.Visible = false;
                }
                //Checking Add Right ========                    
                if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                {
                    BtnSave.Visible = false;
                    FlagAdd = true;

                }
                //Edit /Delete Column Visible ========
                if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false &&
                    Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                {
                    foreach (GridViewRow GRow in ReportGrid.Rows)
                    {
                        GRow.FindControl("ImgBtnDelete").Visible = false;
                        GRow.FindControl("ImageGridEdit").Visible = false;
                        GRow.FindControl("ImgBtnPrint").Visible = false;
                    }
                    //BtnUpdate.Visible = false;
                    FlagDel = true;
                    FlagEdit = true;
                    FlagPrint = true;
                }
                else
                {
                    //Checking Delete Right ========
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                    {
                        foreach (GridViewRow GRow in ReportGrid.Rows)
                        {
                            GRow.FindControl("ImgBtnDelete").Visible = false;
                            FlagDel = true;
                        }
                    }

                    //Checking Edit Right ========
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        foreach (GridViewRow GRow in ReportGrid.Rows)
                        {
                            GRow.FindControl("ImageGridEdit").Visible = false;
                            FlagEdit = true;
                        }
                    }

                    //Checking Print Right ========
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        foreach (GridViewRow GRow in ReportGrid.Rows)
                        {
                            GRow.FindControl("ImgBtnPrint").Visible = false;
                            FlagPrint = true;
                        }
                    }
                }
                dsChkUserRight.Dispose();
                //}
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
            #endregion
        }
        catch (ThreadAbortException ex)
        {
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //User Right Function===========



  


    public void MakeEmptyForm()
    {
        txtReqDate.Text = txtTempDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        CalendarExtender2.EndDate = CalendarExtender1.EndDate = DateTime.Now;
        txtReqNo.Text = string.Empty;
        txtindremark.Text = string.Empty;
        TXTTEMPLATEMULTIPLY.Text = "1";
        // ddlCostCentre.SelectedValue = Session["TransactionSiteID"].ToString();
        ddlCostCentre.SelectedValue = "0";
        ddlCostCentre.Enabled = true;

        if (Session["TransactionSite"] != null)
        {


            lblCafe.Text = Session["TransactionSite"].ToString();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }

        BindComboOfProject();
        bindcategory();
        lblTotalAmt.Text = "0.00";
        rowindexes = 0;
        ViewState["Requisition"] = null;
        ViewState["GridIndex"] = null;
        flag1 = false;
        fillgrid = false;
        TXTREMARK.Text = string.Empty;
        if (!FlagAdd)
            BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        SetInitialRow_GrdRequisition();
        gvCustomers.DataSource = null;
        gvCustomers.DataBind();
        BindCombo();

        #region[UserRights]
        if (FlagEdit)
        {
            foreach (GridViewRow GRow in ReportGrid.Rows)
            {
                GRow.FindControl("ImageGridEdit").Visible = false;
            }
        }
        if (FlagDel)
        {
            foreach (GridViewRow GRow in ReportGrid.Rows)
            {
                GRow.FindControl("ImgBtnDelete").Visible = false;
            }
        }
        if (FlagPrint)
        {
            foreach (GridViewRow GRow in ReportGrid.Rows)
            {
                GRow.FindControl("ImgBtnPrint").Visible = false;
            }
        }
        #endregion        
        ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlItem")).Focus();


    }

    public void SetInitialRow_GrdRequisition()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("GrdSelectAll", typeof(bool));
            //dt.Columns.Add("SubCategory", typeof(string));
            //dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("AvlQty", typeof(decimal));
            dt.Columns.Add("TransitQty", typeof(decimal));
            dt.Columns.Add("AvgRate", typeof(decimal));
            dt.Columns.Add("MinStockLevel", typeof(decimal));
            dt.Columns.Add("MaxStockLevel", typeof(decimal));
            dt.Columns.Add("AvgRateDate", typeof(string));
            dt.Columns.Add("Vendor", typeof(string));
            dt.Columns.Add("Rate", typeof(string));
            dt.Columns.Add("VendorId", typeof(int));
            dt.Columns.Add("ItemID", typeof(int));
            dt.Columns.Add("CategoryId", typeof(int));
            dt.Columns.Add("SubcategoryId", typeof(int));
            dt.Columns.Add("ItemDetailsId", typeof(int));
            dt.Columns.Add("UnitConvDtlsId", typeof(int));
            dt.Columns.Add("txtOrdQty", typeof(string));
            dt.Columns.Add("txtAvlQty", typeof(string));
            dt.Columns.Add("IsCancel", typeof(string));
            dt.Columns.Add("Priority", typeof(string));
            dt.Columns.Add("PriorityID", typeof(string));
            dt.Columns.Add("RequiredDate", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("ItemToolTip", typeof(string));
            dt.Columns.Add("DrawingPath", typeof(string));
            dr = dt.NewRow();
            dr["#"] = 0;
            dr["ItemCode"] = "0";
            dr["GrdSelectAll"] = false;
            //dr["Category"] = string.Empty;
            //dr["SubCategory"] = string.Empty;
            //dr["ItemName"] = string.Empty;
            dr["Location"] = string.Empty;
            dr["AvgRateDate"] = string.Empty;
            dr["Vendor"] = string.Empty;
            dr["AvlQty"] = 0;
            dr["TransitQty"] = 0;
            dr["AvgRate"] = 0;
            dr["MinStockLevel"] = 0;
            dr["MaxStockLevel"] = 0;
            dr["Rate"] = string.Empty;
            dr["VendorId"] = 0;
            dr["ItemID"] = 0;
            dr["CategoryId"] = 0;
            dr["SubcategoryId"] = 0;
            dr["ItemDetailsId"] = 0;
            dr["UnitConvDtlsId"] = 0;
            dr["txtAvlQty"] = "";
            dr["txtOrdQty"] = 1;
            dr["IsCancel"] = "0";
            dr["Priority"] = "";
            dr["PriorityID"] = "";
            dr["RequiredDate"] = "";
            dr["Remark"] = "";
            dr["ItemName"] = "";
            dr["ItemToolTip"] = "";
            dr["DrawingPath"] = "";
            dt.Rows.Add(dr);
            ViewState["Requisition"] = dt;


            GrdRequisition.DataSource = dt;
            GrdRequisition.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void SetInitialRow_ReportGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("#", typeof(int)));
            dt.Columns.Add(new DataColumn("RequisitionNo", typeof(string)));
            dt.Columns.Add(new DataColumn("TemplateTitle", typeof(string)));
            dt.Columns.Add(new DataColumn("RequisitionDate", typeof(string)));
            dt.Columns.Add(new DataColumn("ReqStatus", typeof(string)));
            dt.Columns.Add(new DataColumn("POSTATUS", typeof(string)));
            dt.Columns.Add(new DataColumn("POINFO", typeof(string)));
            // dt.Columns.Add(new DataColumn("EmailStatus"))

            dr = dt.NewRow();
            dr["#"] = 0;
            dr["RequisitionNo"] = string.Empty;
            dr["TemplateTitle"] = string.Empty;
            dr["RequisitionDate"] = string.Empty;
            dr["ReqStatus"] = string.Empty;
            dr["POSTATUS"] = string.Empty;
            dr["POINFO"] = string.Empty;
            dt.Rows.Add(dr);
            ReportGrid.DataSource = dt;
            ReportGrid.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    public void GetAvaliableQuantityForItem(int rowindex)
    {
        try
        {
            int currrow = rowindex;
            DataSet dss = new DataSet();
            DataTable dttable1 = new DataTable();
            dttable1 = (DataTable)ViewState["Requisition"];
            int ITEMID = Convert.ToInt32(dttable1.Rows[currrow]["ItemID"].ToString());
            dss = Obj_RequisitionCafeteria.BindAvaliableQty(ITEMID, Convert.ToInt32(Session["TransactionSiteID"].ToString()), out StrError);
            if (dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                GrdRequisition.Rows[currrow].Cells[8].Text = dss.Tables[0].Rows[0]["Closing"].ToString() + " - " + dss.Tables[1].Rows[0]["Unit"].ToString();
            }
            else
            {
                if (dss.Tables[1].Rows.Count > 0)
                {
                    GrdRequisition.Rows[currrow].Cells[8].Text = "0" + " - " + dss.Tables[1].Rows[0]["Unit"].ToString();
                }
                else
                {
                    GrdRequisition.Rows[currrow].Cells[8].Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void BindCombo()
    {
        try
        {
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.FillCombo(Convert.ToInt32(Session["TransactionSiteID"].ToString()), out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlTemplate.DataSource = Ds.Tables[0];
                    ddlTemplate.DataTextField = "Title";
                    ddlTemplate.DataValueField = "#";
                    ddlTemplate.DataBind();

                }
                if (Ds.Tables[1].Rows.Count > 0)
                {
                    txtReqNo.Text = Ds.Tables[1].Rows[0]["RequisiNo"].ToString();
                    lblReqNo.Text = Ds.Tables[1].Rows[0]["RequisiNo"].ToString();
                }
                if (Ds.Tables[2].Rows.Count > 0)
                {
                    ReportGrid.DataSource = Ds.Tables[2];
                    ReportGrid.DataBind();

                }
                if (Ds.Tables[3].Rows.Count > 0)
                {
                    ViewState["ItemsList"] = Ds.Tables[3];
                    ViewState["ItemsList1"] = Ds.Tables[3];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlItem")).DataSource = Ds.Tables[3];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlItem")).DataTextField = "Item";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlItem")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlItem")).DataBind();


                }
                #region [For Priority]----------------------------------------------------------------------------
                if (Ds.Tables[4].Rows.Count > 0)
                {
                    ViewState["PriorityList"] = Ds.Tables[4];
                    GridTemplatePriority.DataSource = Ds.Tables[4];
                    GridTemplatePriority.DataBind();
                }
                #endregion [end Priority]----------------------------------------------------------------------------

                //if (Ds.Tables[5].Rows.Count > 0)
                //{
                //    ddlCostCentre.DataSource = Ds.Tables[5];
                //    ddlCostCentre.DataTextField = "Location";
                //    ddlCostCentre.DataValueField = "StockLocationID";
                //    ddlCostCentre.DataBind();
                //    //ddlCostCentre.SelectedValue = Convert.ToString(Session["TransactionSiteID"]);
                //}

                if (Ds.Tables[6].Rows.Count > 0)
                {
                    ViewState["CategoryList"] = Ds.Tables[6];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlCategory")).DataSource = Ds.Tables[6];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlCategory")).DataTextField = "Category";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlCategory")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlCategory")).DataBind();
                }
                if (Ds.Tables[7].Rows.Count > 0)
                {
                    ViewState["SubCategoryList"] = Ds.Tables[7];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlSubCategory")).DataSource = Ds.Tables[7];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlSubCategory")).DataTextField = "SubCategory";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlSubCategory")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlSubCategory")).DataBind();
                }

                Ds = null;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void BindComboOfProject()
    {
        try
        {
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.FillComboProject(Convert.ToInt32(Session["UserId"]), out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlCostCentre.DataSource = Ds.Tables[0];
                    ddlCostCentre.DataTextField = "ProjectName";
                    ddlCostCentre.DataValueField = "id";
                    ddlCostCentre.DataBind();

                }
            }
            else
            {
                ddlCostCentre.DataSource = null;
                ddlCostCentre.DataBind();

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }




    }

    public void bindcategory()
    {
        drpcatrgory.DataSource = db.Displaygrid("select CategoryId , CategoryName from ItemCategory");
        drpcatrgory.DataTextField = "CategoryName";
        drpcatrgory.DataValueField = "CategoryId";
        drpcatrgory.DataBind();
        drpcatrgory.Items.Insert(0, "Select Category");

    }

    public void BindTemplateNItem()
    {
        try
        {
            if (Convert.ToBoolean(ViewState["flag"]) == false)
            {
                int rowIndex = 0;
                if (ViewState["Requisition"] != null)
                {
                    //DataTable dtCurrentTable = (DataTable)ViewState["TemplateDetails"];
                    DataTable dtCurrentTable = (DataTable)ViewState["Requisition"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            if ((dtCurrentTable.Rows[rowIndex]["#"].ToString()) == "")
                            {
                                dtCurrentTable.Rows.RemoveAt(rowIndex);
                                //DupFlag = false;
                            }
                            else
                            {
                                Label li = (Label)GrdRequisition.Rows[rowIndex].Cells[18].FindControl("lblVendorId");
                                string lo = li.Text;
                                dtCurrentTable.Rows[rowIndex]["#"] = ((Label)GrdRequisition.Rows[rowIndex].Cells[0].FindControl("LblEntryId")).Text;
                                dtCurrentTable.Rows[rowIndex]["ItemCode"] = GrdRequisition.Rows[rowIndex].Cells[2].Text;
                                dtCurrentTable.Rows[rowIndex]["Location"] = GrdRequisition.Rows[rowIndex].Cells[7].Text;
                                string str = GrdRequisition.Rows[rowIndex].Cells[8].Text;
                                string[] AvlQty = str.Split(' ');
                                dtCurrentTable.Rows[rowIndex]["AvlQty"] = Convert.ToDecimal(AvlQty[0]);
                                dtCurrentTable.Rows[rowIndex]["TransitQty"] = Convert.ToDecimal(GrdRequisition.Rows[rowIndex].Cells[10].Text);
                                dtCurrentTable.Rows[rowIndex]["MinStockLevel"] = Convert.ToDecimal(GrdRequisition.Rows[rowIndex].Cells[11].Text);
                                dtCurrentTable.Rows[rowIndex]["MaxStockLevel"] = Convert.ToDecimal(GrdRequisition.Rows[rowIndex].Cells[12].Text);
                                dtCurrentTable.Rows[rowIndex]["AvgRate"] = Convert.ToDecimal(GrdRequisition.Rows[rowIndex].Cells[13].Text);
                                dtCurrentTable.Rows[rowIndex]["AvgRateDate"] = GrdRequisition.Rows[rowIndex].Cells[14].Text;
                                dtCurrentTable.Rows[rowIndex]["Vendor"] = GrdRequisition.Rows[rowIndex].Cells[15].Text.Replace("&amp;", "&");
                                dtCurrentTable.Rows[rowIndex]["Rate"] = GrdRequisition.Rows[rowIndex].Cells[16].Text;
                                dtCurrentTable.Rows[rowIndex]["VendorId"] = Convert.ToInt32(((Label)GrdRequisition.Rows[rowIndex].Cells[18].FindControl("lblVendorId")).Text);//ToInt32
                                //dtCurrentTable.Rows[rowIndex]["ItemID"] = Convert.ToInt32(((DropDownList)GrdRequisition.Rows[rowIndex].Cells[2].FindControl("ddlItem")).SelectedValue);
                                dtCurrentTable.Rows[rowIndex]["ItemID"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItem")).SelectedValue);
                                dtCurrentTable.Rows[rowIndex]["txtOrdQty"] = ((TextBox)GrdRequisition.Rows[rowIndex].FindControl("txtOrdQty")).Text;
                                dtCurrentTable.Rows[rowIndex]["IsCancel"] = GrdRequisition.Rows[rowIndex].Cells[23].Text;//Change 17 to 19
                                dtCurrentTable.Rows[rowIndex]["Priority"] = ((Label)GrdRequisition.Rows[rowIndex].FindControl("Priority")).Text;//GrdRequisition.Rows[rowIndex].Cells[17].Text;//adding Field here 5/1/13 for Priority
                                                                                                                                                // dtCurrentTable.Rows[rowIndex]["PriorityID"] = Convert.ToInt32(((Label)GrdRequisition.Rows[rowIndex].FindControl("PriorityID")).Text);
                                dtCurrentTable.Rows[rowIndex]["ItemDetailsId"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItemDescription")).SelectedValue);
                                // dtCurrentTable.Rows[rowIndex]["UnitConvDtlsId"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlUnitConvertor")).SelectedValue);
                                dtCurrentTable.Rows[rowIndex]["CategoryId"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlCategory")).SelectedValue);
                                dtCurrentTable.Rows[rowIndex]["SubcategoryId"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlSubCategory")).SelectedValue);
                                // dtCurrentTable.Rows[rowIndex]["ItemName"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItem")).SelectedItem);
                                dtCurrentTable.Rows[rowIndex]["ItemToolTip"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItem")).SelectedValue);

                                rowIndex++;
                            }
                        }
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["#"] = 0;
                        drCurrentRow["ItemCode"] = string.Empty;
                        drCurrentRow["Location"] = string.Empty;
                        drCurrentRow["AvlQty"] = 0.00;
                        drCurrentRow["TransitQty"] = 0;
                        drCurrentRow["MinStockLevel"] = 0.00;
                        drCurrentRow["MaxStockLevel"] = 0.00;
                        drCurrentRow["AvgRate"] = 0.00;
                        drCurrentRow["AvgRateDate"] = string.Empty;
                        drCurrentRow["Vendor"] = string.Empty;
                        drCurrentRow["Rate"] = 0.00;
                        drCurrentRow["VendorId"] = 0.00;
                        drCurrentRow["ItemID"] = 0;
                        drCurrentRow["txtOrdQty"] = 0.00;
                        drCurrentRow["txtAvlQty"] = 0.00;
                        drCurrentRow["IsCancel"] = "0";
                        drCurrentRow["Priority"] = "";//adding Field here 5/1/13 for Priority
                        drCurrentRow["PriorityID"] = 0;
                        drCurrentRow["ItemDetailsId"] = 0;
                        drCurrentRow["UnitConvDtlsId"] = 0;
                        drCurrentRow["CategoryId"] = 0;
                        drCurrentRow["SubcategoryId"] = 0;
                        drCurrentRow["ItemName"] = "";
                        drCurrentRow["ItemToolTip"] = "0";
                        ViewState["Requisition"] = dtCurrentTable;
                        GrdRequisition.DataSource = dtCurrentTable;
                        GrdRequisition.DataBind();
                    }
                    else
                    {
                        Response.Write("ViewState is null");
                    }
                }
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
        }
    }


    public void BindItemToGridAll(DataSet ds)  //For Binding To Grid All
    {

        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
        {
            BindTemplateNItem();
            int ItemID_All = Convert.ToInt32(ds.Tables[0].Rows[j]["ItemID"]);
            int ItemDetailsId_All = Convert.ToInt32(ds.Tables[0].Rows[j]["ItemDetailsId"]);

            if (ViewState["Requisition"] != null)
            {
                bool DupFlag = false;
                int k = 0;
                DataTable DTTABLE = (DataTable)ViewState["Requisition"];

                DataRow drCurrentRow = null;
                if (DTTABLE.Rows.Count > 0)
                {
                    if (ViewState["GridIndex"] == null)
                    {
                        for (int i = 0; i < DTTABLE.Rows.Count; i++)
                        {
                            int ItemID_Single = Convert.ToInt32(DTTABLE.Rows[i]["ItemID"]);
                            int ItemDetailsId_Single = Convert.ToInt32(DTTABLE.Rows[i]["ItemDetailsId"]);
                            if (DTTABLE.Rows.Count == 1 && (DTTABLE.Rows[0]["#"].ToString()) == "0")
                            {
                                DTTABLE.Rows.RemoveAt(0);
                            }
                            if (Convert.ToInt32(ItemID_Single) == Convert.ToInt32(ItemID_All) &&
                                Convert.ToInt32(ItemDetailsId_Single) == Convert.ToInt32(ItemDetailsId_All))
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            DTTABLE.Rows[k]["#"] = Convert.ToString(DTTABLE.Rows[k]["#"]);
                            DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(DTTABLE.Rows[k]["ItemCode"]);
                            DTTABLE.Rows[k]["ItemID"] = Convert.ToString(DTTABLE.Rows[k]["ItemID"]);
                            DTTABLE.Rows[k]["CategoryId"] = Convert.ToString(DTTABLE.Rows[k]["CategoryId"]);
                            DTTABLE.Rows[k]["SubcategoryId"] = Convert.ToString(DTTABLE.Rows[k]["SubcategoryId"]);
                            DTTABLE.Rows[k]["ItemDetailsId"] = Convert.ToString(DTTABLE.Rows[k]["ItemDetailsId"]);
                            DTTABLE.Rows[k]["UnitConvDtlsId"] = Convert.ToString(DTTABLE.Rows[k]["UnitConvDtlsId"]);
                            DTTABLE.Rows[k]["Location"] = Convert.ToString(ddlCostCentre.SelectedItem);
                            DTTABLE.Rows[k]["AvlQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvlQty"]);
                            DTTABLE.Rows[k]["TransitQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["TransitQty"]);
                            DTTABLE.Rows[k]["MinStockLevel"] = Convert.ToDecimal(DTTABLE.Rows[k]["MinStockLevel"]);
                            DTTABLE.Rows[k]["MaxStockLevel"] = Convert.ToDecimal(DTTABLE.Rows[k]["MaxStockLevel"]);
                            DTTABLE.Rows[k]["AvgRate"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvgRate"]);
                            DTTABLE.Rows[k]["AvgRateDate"] = Convert.ToString(DTTABLE.Rows[k]["AvgRateDate"]);
                            DTTABLE.Rows[k]["Vendor"] = Convert.ToString(DTTABLE.Rows[k]["Vendor"]);
                            DTTABLE.Rows[k]["VendorId"] = Convert.ToString(DTTABLE.Rows[k]["VendorId"]);
                            DTTABLE.Rows[k]["Rate"] = Convert.ToString(DTTABLE.Rows[k]["Rate"]);
                            DTTABLE.Rows[k]["txtAvlQty"] = Convert.ToString(DTTABLE.Rows[k]["AvlQty"]);
                            DTTABLE.Rows[k]["txtOrdQty"] = Convert.ToString(DTTABLE.Rows[k]["txtOrdQty"]);
                            DTTABLE.Rows[k]["IsCancel"] = Convert.ToString(DTTABLE.Rows[k]["IsCancel"]);//
                            DTTABLE.Rows[k]["Priority"] = Convert.ToString(DTTABLE.Rows[k]["Priority"]);//adding Field here 5/1/13 for Priority
                            DTTABLE.Rows[k]["PriorityID"] = Convert.ToString(DTTABLE.Rows[k]["PriorityID"]);
                            DTTABLE.Rows[k]["ItemName"] = Convert.ToString(DTTABLE.Rows[k]["ItemName"]);
                            DTTABLE.Rows[k]["ItemToolTip"] = Convert.ToString(DTTABLE.Rows[k]["ItemToolTip"]);
                            DTTABLE.Rows[k]["DrawingPath"] = Convert.ToString(DTTABLE.Rows[k]["DrawingPath"]);
                            ViewState["TemplateDetails"] = DTTABLE;
                            ViewState["Requisition"] = DTTABLE;
                            GrdRequisition.DataSource = DTTABLE;
                            GrdRequisition.DataBind();
                            GetItemAndCheckBox();
                            SetDataInTextBox();
                            DtEditPO = (DataTable)ViewState["Template"];
                        }
                        else
                        {
                            drCurrentRow = DTTABLE.NewRow();
                            drCurrentRow["#"] = Ds.Tables[0].Rows[j]["#"].ToString();
                            drCurrentRow["ItemCode"] = Ds.Tables[0].Rows[j]["ItemCode"].ToString();
                            drCurrentRow["Location"] = Convert.ToString(ddlCostCentre.SelectedItem);// Ds.Tables[0].Rows[j]["Location"].ToString();
                            drCurrentRow["AvlQty"] = Ds.Tables[0].Rows[j]["AvlQty"].ToString();
                            drCurrentRow["TransitQty"] = Ds.Tables[0].Rows[j]["TransitQty"].ToString();
                            drCurrentRow["MinStockLevel"] = Ds.Tables[0].Rows[j]["MinStockLevel"].ToString();
                            drCurrentRow["MaxStockLevel"] = Ds.Tables[0].Rows[j]["MaxStockLevel"].ToString();
                            drCurrentRow["AvgRate"] = Ds.Tables[0].Rows[j]["AvgRate"].ToString();
                            drCurrentRow["AvgRateDate"] = Ds.Tables[0].Rows[j]["AvgRateDate"].ToString();
                            drCurrentRow["Vendor"] = Ds.Tables[0].Rows[j]["Vendor"].ToString();
                            drCurrentRow["Rate"] = Ds.Tables[0].Rows[j]["Rate"].ToString();
                            drCurrentRow["VendorId"] = Ds.Tables[0].Rows[j]["VendorId"].ToString();
                            drCurrentRow["ItemID"] = Ds.Tables[0].Rows[j]["ItemID"].ToString();
                            drCurrentRow["CategoryId"] = Ds.Tables[0].Rows[j]["CategoryId"].ToString();
                            drCurrentRow["SubcategoryId"] = Ds.Tables[0].Rows[j]["SubcategoryId"].ToString();
                            drCurrentRow["ItemDetailsId"] = Ds.Tables[0].Rows[j]["ItemDetailsId"].ToString();
                            drCurrentRow["UnitConvDtlsId"] = Ds.Tables[0].Rows[j]["UnitConvDtlsId"].ToString();
                            drCurrentRow["txtAvlQty"] = Ds.Tables[0].Rows[j]["AvlQty"].ToString();
                            drCurrentRow["txtOrdQty"] = Ds.Tables[0].Rows[j]["txtOrdQty"].ToString();
                            drCurrentRow["IsCancel"] = Ds.Tables[0].Rows[j]["IsCancel"].ToString();//
                            drCurrentRow["Priority"] = Ds.Tables[0].Rows[j]["Priority"].ToString();//adding Field here 5/1/13 for Priority
                            drCurrentRow["PriorityID"] = Ds.Tables[0].Rows[j]["PriorityID"].ToString();//adding Field here 5/1/13 for Priority
                            drCurrentRow["ItemName"] = Ds.Tables[0].Rows[j]["ItemName"].ToString();
                            drCurrentRow["ItemToolTip"] = Ds.Tables[0].Rows[j]["ItemID"].ToString();
                            drCurrentRow["DrawingPath"] = Ds.Tables[0].Rows[j]["DrawingPath"].ToString();
                            DTTABLE.Rows.Add(drCurrentRow);
                            ViewState["Requisition"] = DTTABLE;
                            ViewState["TemplateDetails"] = DTTABLE;
                            GrdRequisition.DataSource = DTTABLE;
                            GrdRequisition.DataBind();
                            GetItemAndCheckBox();
                            SetDataInTextBox();
                            int g = k;
                            ViewState["GridIndex"] = g++;

                        }
                    }
                    else
                    {
                        for (int i = 0; i < DTTABLE.Rows.Count; i++)
                        {
                            int ItemID_Single = Convert.ToInt32(DTTABLE.Rows[i]["ItemID"]);
                            int ItemDetailsId_Single = Convert.ToInt32(DTTABLE.Rows[i]["ItemDetailsId"]);
                            if (Convert.ToInt32(ItemID_Single) == Convert.ToInt32(ItemID_All) &&
                                Convert.ToInt32(ItemDetailsId_Single) == Convert.ToInt32(ItemDetailsId_All))
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                            DTTABLE.Rows[k]["#"] = Convert.ToString(DTTABLE.Rows[k]["#"]);
                            DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(DTTABLE.Rows[k]["ItemCode"]);
                            DTTABLE.Rows[k]["ItemID"] = Convert.ToString(DTTABLE.Rows[k]["ItemID"]);
                            DTTABLE.Rows[k]["CategoryId"] = Convert.ToString(DTTABLE.Rows[k]["CategoryId"]);
                            DTTABLE.Rows[k]["SubcategoryId"] = Convert.ToString(DTTABLE.Rows[k]["SubcategoryId"]);
                            DTTABLE.Rows[k]["ItemDetailsId"] = Convert.ToString(DTTABLE.Rows[k]["ItemDetailsId"]);
                            DTTABLE.Rows[k]["UnitConvDtlsId"] = Convert.ToString(DTTABLE.Rows[k]["UnitConvDtlsId"]);
                            DTTABLE.Rows[k]["Location"] = Convert.ToString(ddlCostCentre.SelectedItem); //Convert.ToString(DTTABLE.Rows[k]["Location"]);
                            DTTABLE.Rows[k]["AvlQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvlQty"]);
                            DTTABLE.Rows[k]["TransitQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["TransitQty"]);
                            DTTABLE.Rows[k]["MinStockLevel"] = Convert.ToDecimal(DTTABLE.Rows[k]["MinStockLevel"]);
                            DTTABLE.Rows[k]["MaxStockLevel"] = Convert.ToDecimal(DTTABLE.Rows[k]["MaxStockLevel"]);
                            DTTABLE.Rows[k]["AvgRate"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvgRate"]);
                            DTTABLE.Rows[k]["AvgRateDate"] = Convert.ToString(DTTABLE.Rows[k]["AvgRateDate"]);
                            DTTABLE.Rows[k]["Vendor"] = Convert.ToString(DTTABLE.Rows[k]["Vendor"]);
                            DTTABLE.Rows[k]["VendorId"] = Convert.ToString(DTTABLE.Rows[k]["VendorId"]);
                            DTTABLE.Rows[k]["Rate"] = Convert.ToString(DTTABLE.Rows[k]["Rate"]);
                            DTTABLE.Rows[k]["txtAvlQty"] = Convert.ToString(DTTABLE.Rows[k]["AvlQty"]);
                            DTTABLE.Rows[k]["txtOrdQty"] = Convert.ToString(DTTABLE.Rows[k]["txtOrdQty"]);
                            DTTABLE.Rows[k]["IsCancel"] = Convert.ToString(DTTABLE.Rows[k]["IsCancel"]);
                            DTTABLE.Rows[k]["Priority"] = Convert.ToString(DTTABLE.Rows[k]["Priority"]);//adding Field here 5/1/13 for Priority
                            DTTABLE.Rows[k]["PriorityID"] = Convert.ToString(DTTABLE.Rows[k]["PriorityID"]);//adding Field here 5/1/13 for Priority
                            DTTABLE.Rows[k]["ItemName"] = Convert.ToString(DTTABLE.Rows[k]["ItemName"]);//adding Field here 5/1/13 for Priority
                            DTTABLE.Rows[k]["ItemToolTip"] = Convert.ToString(DTTABLE.Rows[k]["ItemToolTip"]);//adding Field here 5/1/13 for Priority
                            DTTABLE.Rows[k]["DrawingPath"] = Convert.ToString(DTTABLE.Rows[k]["DrawingPath"]);//adding Field here 5/1/13 for Priority
                            ViewState["TemplateDetails"] = DTTABLE;
                            ViewState["Requisition"] = DTTABLE;
                            GrdRequisition.DataSource = DTTABLE;
                            GrdRequisition.DataBind();
                            GetItemAndCheckBox();
                            SetDataInTextBox();
                            DtEditPO = (DataTable)ViewState["Requisition"];
                        }
                        else
                        {
                            drCurrentRow = DTTABLE.NewRow();
                            drCurrentRow["#"] = Ds.Tables[0].Rows[j]["#"].ToString();
                            drCurrentRow["ItemCode"] = Ds.Tables[0].Rows[j]["ItemCode"].ToString();
                            drCurrentRow["Location"] = Convert.ToString(ddlCostCentre.SelectedItem); //Ds.Tables[0].Rows[j]["Location"].ToString();
                            drCurrentRow["AvlQty"] = Ds.Tables[0].Rows[j]["AvlQty"].ToString();
                            drCurrentRow["TransitQty"] = ds.Tables[0].Rows[j]["TransitQty"].ToString();
                            drCurrentRow["MinStockLevel"] = Ds.Tables[0].Rows[j]["MinStockLevel"].ToString();
                            drCurrentRow["MaxStockLevel"] = Ds.Tables[0].Rows[j]["MaxStockLevel"].ToString();
                            drCurrentRow["AvgRate"] = Ds.Tables[0].Rows[j]["AvgRate"].ToString();
                            drCurrentRow["AvgRateDate"] = Ds.Tables[0].Rows[j]["AvgRateDate"].ToString();
                            drCurrentRow["Vendor"] = Ds.Tables[0].Rows[j]["Vendor"].ToString();
                            drCurrentRow["Rate"] = Ds.Tables[0].Rows[j]["Rate"].ToString();
                            drCurrentRow["VendorId"] = Ds.Tables[0].Rows[j]["VendorId"].ToString();
                            drCurrentRow["ItemID"] = Ds.Tables[0].Rows[j]["ItemID"].ToString();
                            drCurrentRow["CategoryId"] = Ds.Tables[0].Rows[j]["CategoryId"].ToString();
                            drCurrentRow["SubcategoryId"] = Ds.Tables[0].Rows[j]["SubcategoryId"].ToString();
                            drCurrentRow["ItemDetailsId"] = Ds.Tables[0].Rows[j]["ItemDetailsId"].ToString();
                            drCurrentRow["UnitConvDtlsId"] = Ds.Tables[0].Rows[j]["UnitConvDtlsId"].ToString();
                            drCurrentRow["txtAvlQty"] = Ds.Tables[0].Rows[j]["AvlQty"].ToString();
                            drCurrentRow["txtOrdQty"] = Ds.Tables[0].Rows[j]["txtOrdQty"].ToString();
                            drCurrentRow["IsCancel"] = Ds.Tables[0].Rows[j]["IsCancel"].ToString();
                            drCurrentRow["Priority"] = Ds.Tables[0].Rows[j]["Priority"].ToString();//adding Field here 5/1/13 for Priority
                            drCurrentRow["PriorityID"] = Ds.Tables[0].Rows[j]["PriorityID"].ToString();//adding Field here 5/1/13 for Priority
                            drCurrentRow["ItemName"] = Ds.Tables[0].Rows[j]["ItemName"].ToString();
                            drCurrentRow["ItemToolTip"] = Ds.Tables[0].Rows[j]["ItemID"].ToString();
                            drCurrentRow["DrawingPath"] = Ds.Tables[0].Rows[j]["DrawingPath"].ToString();
                            DTTABLE.Rows.Add(drCurrentRow);
                            ViewState["Requisition"] = DTTABLE;
                            ViewState["TemplateDetails"] = DTTABLE;
                            GrdRequisition.DataSource = DTTABLE;
                            GrdRequisition.DataBind();
                            GetItemAndCheckBox();
                            SetDataInTextBox();
                            DtEditPO = (DataTable)ViewState["Requisition"];
                        }
                    }
                }
            }
        }
    }



    public void SetDataInTextBox()
    {
        try
        {
            for (int i = 0; i < GrdRequisition.Rows.Count; i++)
            {
                if (ViewState["Requisition"] != null)
                {
                    string qwe = ((TextBox)GrdRequisition.Rows[i].FindControl("txtAvlQty")).Text;
                    string[] qw = (GrdRequisition.Rows[i].Cells[8].Text.ToString()).Split('-');
                    ((TextBox)GrdRequisition.Rows[i].FindControl("txtAvlQty")).Text = GrdRequisition.Rows[i].Cells[8].ToString();
                }
                else
                {
                    ((TextBox)GrdRequisition.Rows[i].FindControl("txtAvlQty")).Text = "";
                }


            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void GetItemAndCheckBox()
    {
        for (int i = 0; i < GrdRequisition.Rows.Count; i++)
        {
            int grder = Convert.ToInt32(((Label)GrdRequisition.Rows[i].Cells[14].FindControl("lblVendorId")).Text);
            if (Convert.ToInt32(((Label)GrdRequisition.Rows[i].Cells[0].FindControl("LblEntryId")).Text) != 0 || Convert.ToInt32(((Label)GrdRequisition.Rows[i].Cells[18].FindControl("lblVendorId")).Text) != 0)
            {
                DataTable dttable1 = new DataTable();
                dttable1 = (DataTable)ViewState["Requisition"];
                //----For Item----
                if (ViewState["ItemsList1"] != null)
                {
                    DataTable dttable = new DataTable();
                    dttable = (DataTable)ViewState["ItemsList1"];

                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).DataSource = dttable;
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).DataTextField = "Item";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).DataBind();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).SelectedValue = (Convert.ToInt32(dttable1.Rows[i]["ItemID"])).ToString();
                    ((CheckBox)GrdRequisition.Rows[i].Cells[0].FindControl("GrdSelectAll")).Checked = true;
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).Enabled = false;
                    ((TextBox)GrdRequisition.Rows[i].FindControl("TxtItemName")).Enabled = false;
                }
                //----For Category----
                if (ViewState["CategoryList"] != null)
                {
                    DataTable dttable = new DataTable();
                    dttable = (DataTable)ViewState["CategoryList"];

                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataSource = dttable;
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataTextField = "Category";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataBind();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).SelectedValue = (Convert.ToInt32(dttable1.Rows[i]["CategoryId"])).ToString();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).Enabled = false;
                }
                //----For SubCategory----
                if (ViewState["SubCategoryList"] != null)
                {
                    DataTable dttable = new DataTable();
                    dttable = (DataTable)ViewState["SubCategoryList"];

                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataSource = dttable;
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataTextField = "SubCategory";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataBind();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).SelectedValue = (Convert.ToInt32(dttable1.Rows[i]["SubcategoryId"])).ToString();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).Enabled = false;
                }
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItemDescription")).Enabled = false;

            }
            else
            {
                DataTable dttable = new DataTable();
                dttable = (DataTable)ViewState["ItemsList"];
                DataTable dttable1 = new DataTable();
                dttable1 = (DataTable)ViewState["CategoryList"];
                DataTable dttable2 = new DataTable();
                dttable2 = (DataTable)ViewState["SubCategoryList"];

                #region[For Item]
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).DataSource = dttable;
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).DataTextField = "Item";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).DataValueField = "#";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).DataBind();
                ((CheckBox)GrdRequisition.Rows[i].Cells[0].FindControl("GrdSelectAll")).Checked = false;
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].Cells[2].FindControl("ddlItem")).Enabled = true;
                #endregion
                #region[Category]
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataSource = dttable1;
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataTextField = "Category";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataValueField = "#";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataBind();
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).Enabled = true;
                #endregion
                #region[SubCategory]
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataSource = dttable2;
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataTextField = "SubCategory";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataValueField = "#";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataBind();
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).Enabled = true;
                #endregion
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].Cells[2].FindControl("ddlItemDescription")).Enabled = true;
            }


        }
    }

    public void CheckDuplicateEntryItem(DataSet ds, int ItemId, int ItemDtlsID)
    {
        DataTable dtt1 = new DataTable();
        dtt1 = (DataTable)ViewState["Requisition"];
        int r = ItemId;
        int ITemDescId = ItemDtlsID;
        for (int w = 0; w < dtt1.Rows.Count - 1; w++)
        {
            if (ItemDtlsID > 0)
            {
                if ((r == (string.IsNullOrEmpty(dtt1.Rows[w]["ItemID"].ToString()) ? Convert.ToInt32("0") : Convert.ToInt32(dtt1.Rows[w]["ItemID"].ToString()))) &&
                    (ITemDescId == (string.IsNullOrEmpty(dtt1.Rows[w]["ItemDetailsId"].ToString()) ? Convert.ToInt32("0") : Convert.ToInt32(dtt1.Rows[w]["ItemDetailsId"].ToString()))))
                {
                    ItemDuplicate = true;
                    break;
                }
                else
                {
                    ItemDuplicate = false;
                }
            }
        }
    }

    public void MergeData(int rowindex)
    {
        if (ViewState["Requisition"] != null)
        {
            if (ViewState["ParticularItem"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["ParticularItem"];
                DataTable dts = new DataTable();
                dts = (DataTable)ViewState["Requisition"];
                int curindex = rowindex;
                for (int f = 0; f < dt.Rows.Count; f++)
                {
                    DataRow dtrow = dts.NewRow();
                    dts.Rows[curindex]["#"] = 0;
                    dts.Rows[curindex]["ItemCode"] = dt.Rows[0]["ItemCode"];
                    dts.Rows[curindex]["Location"] = dt.Rows[0]["Location"];
                    dts.Rows[curindex]["AvlQty"] = dt.Rows[0]["AvlQty"];
                    dts.Rows[curindex]["TransitQty"] = dt.Rows[0]["TransitQty"];
                    dts.Rows[curindex]["MinStockLevel"] = dt.Rows[0]["MinStockLevel"];
                    dts.Rows[curindex]["MaxStockLevel"] = dt.Rows[0]["MaxStockLevel"];
                    dts.Rows[curindex]["AvgRate"] = dt.Rows[0]["AvgRate"];
                    dts.Rows[curindex]["AvgRateDate"] = dt.Rows[0]["AvgRateDate"];
                    dts.Rows[curindex]["Vendor"] = dt.Rows[0]["Vendor"];
                    dts.Rows[curindex]["Rate"] = dt.Rows[0]["Rate"];
                    dts.Rows[curindex]["VendorId"] = dt.Rows[0]["VendorId"];
                    dts.Rows[curindex]["ItemID"] = dt.Rows[0]["#"];
                    dts.Rows[curindex]["IsCancel"] = dt.Rows[0]["IsCancel"];
                    dts.Rows[curindex]["Priority"] = dt.Rows[0]["Priority"];//adding Field here 5/1/13 for Priority                    
                    dts.Rows[curindex]["ItemName"] = dt.Rows[0]["ItemName"];
                    dts.Rows[curindex]["ItemToolTip"] = dt.Rows[0]["ItemID"];
                    dts.Rows.Add(dtrow);
                    ViewState["Requisition"] = dts;
                    curindex++;
                }
            }
        }
    }

    public void CheckDuplicate()
    {
        Ds = new DataSet();
        Ds = Obj_RequisitionCafeteria.GetDuplicateName(txtReqNo.Text, out StrError);
        if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
        {
            clear = false;
        }
        else
        {
            clear = true;
        }
    }

    public void CheckOrderAmount()//old method
    {
        try
        {
            for (int i = 0; i < GrdRequisition.Rows.Count; i++)
            {
                if (((CheckBox)GrdRequisition.Rows[i].Cells[0].FindControl("GrdSelectAll")).Checked == true)
                {
                    DataTable dtq = (DataTable)ViewState["Requisition"];
                    TextBox tr = (TextBox)GrdRequisition.Rows[i].FindControl("txtOrdQty");
                    string h = tr.Text;
                    string qwe = ((TextBox)GrdRequisition.Rows[i].FindControl("txtOrdQty")).Text;
                    if (Convert.ToInt32(qwe) > Convert.ToInt32(dtq.Rows[i]["AvlQty"].ToString()))///find row position
                    {
                        valueofitem = i + 1;
                        break;
                    }
                    else
                    {
                        valueofitem = 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void CheckOrderAmount_New()
    {
        try
        {
            for (int i = 0; i < GrdRequisition.Rows.Count; i++)
            {
                DataTable dtq = (DataTable)ViewState["Requisition"];
                TextBox tr = (TextBox)GrdRequisition.Rows[i].FindControl("txtOrdQty");
                if (string.IsNullOrEmpty(tr.Text))
                {
                    tr.Text = "0";
                }
                if (Convert.ToDecimal(tr.Text) > 0)
                {

                    if (Convert.ToDecimal(tr.Text) > Convert.ToDecimal(0))
                    {
                        valueofitem = 2;
                    }
                    else
                    {
                        valueofitem = 1;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void FindDataInGrid()
    {
        try
        {
            for (int i = 0; i < 1; i++)
            {
                if (Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[0].FindControl("ddlItem")).SelectedValue) >= 0)
                {
                    fillgrid = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void CheckTextBox()
    {
        for (int f = 0; f < GrdRequisition.Rows.Count; f++)
        {
            if (((CheckBox)GrdRequisition.Rows[f].Cells[0].FindControl("GrdSelectAll")).Checked == true)
            {
                if (((TextBox)GrdRequisition.Rows[f].FindControl("txtOrdQty")).Text == "")
                {
                    flag1 = false;
                }
                else
                {
                    flag1 = false;
                }
            }
        }
    }

    public void CalculateToTotal()
    {
        lblTotalAmt.Text = "0.00";
        for (int w = 0; w < GrdRequisition.Rows.Count; w++)
        {
            decimal ordqty = Convert.ToDecimal(((TextBox)GrdRequisition.Rows[w].FindControl("txtOrdQty")).Text);
            decimal Rate = Convert.ToDecimal(GrdRequisition.Rows[w].Cells[12].Text);
            if (Rate > 0)
            {
                lblTotalAmt.Text = Convert.ToString((Convert.ToDecimal(lblTotalAmt.Text) + Convert.ToDecimal((ordqty) * (Rate))).ToString("0.00"));
            }
            else
            {
                lblTotalAmt.Text = lblTotalAmt.Text;
            }
        }
    }

    DataTable GetDataTable(GridView dtg)
    {
        try
        {
            int k = 0;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["Requisition"];
            while (dt.Rows.Count > 0)
            {
                dt.Rows.RemoveAt(0);
            }
            foreach (GridViewRow row in dtg.Rows)
            {
                if (Convert.ToInt32(GrdRequisition.Rows[k].Cells[2].Text) > 0)
                {
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr;
                        dr = dt.NewRow();

                        dr["#"] = ((Label)GrdRequisition.Rows[k].Cells[0].FindControl("LblEntryId")).Text;
                        dr["ItemCode"] = GrdRequisition.Rows[k].Cells[2].Text;
                        dr["Location"] = GrdRequisition.Rows[k].Cells[7].Text;
                        string str = GrdRequisition.Rows[k].Cells[8].Text;
                        string[] AvlQty = str.Split(' ');
                        dr["AvlQty"] = Convert.ToDecimal(AvlQty[0]);
                        dr["TransitQty"] = Convert.ToDecimal(GrdRequisition.Rows[k].Cells[10].Text);
                        dr["MinStockLevel"] = Convert.ToDecimal(GrdRequisition.Rows[k].Cells[11].Text);
                        dr["MaxStockLevel"] = Convert.ToDecimal(GrdRequisition.Rows[k].Cells[12].Text);
                        dr["AvgRate"] = Convert.ToDecimal(GrdRequisition.Rows[k].Cells[13].Text);
                        dr["AvgRateDate"] = GrdRequisition.Rows[k].Cells[14].Text;
                        dr["Vendor"] = GrdRequisition.Rows[k].Cells[15].Text.Replace("&amp;", "&");
                        dr["Rate"] = GrdRequisition.Rows[k].Cells[16].Text;
                        dr["VendorId"] = Convert.ToInt32(((Label)GrdRequisition.Rows[k].Cells[18].FindControl("lblVendorId")).Text);//ToInt32                        
                        dr["ItemID"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[k].FindControl("ddlItem")).SelectedValue);
                        dr["txtOrdQty"] = ((TextBox)GrdRequisition.Rows[k].FindControl("txtOrdQty")).Text;
                        dr["IsCancel"] = GrdRequisition.Rows[k].Cells[23].Text;//Change 17 to 19
                        dr["Priority"] = ((Label)GrdRequisition.Rows[k].FindControl("Priority")).Text;
                        dr["PriorityID"] = Convert.ToInt32(((Label)GrdRequisition.Rows[k].FindControl("PriorityID")).Text);
                        dr["ItemDetailsId"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[k].FindControl("ddlItemDescription")).SelectedValue);
                        dr["UnitConvDtlsId"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[k].FindControl("ddlUnitConvertor")).SelectedValue);
                        dr["CategoryId"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[k].FindControl("ddlCategory")).SelectedValue);
                        dr["SubcategoryId"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[k].FindControl("ddlSubCategory")).SelectedValue);
                        dt.Rows.Add(dr);
                        ViewState["Requisition"] = dt;
                        k++;
                    }
                    else
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["#"] = ((Label)GrdRequisition.Rows[k].Cells[0].FindControl("LblEntryId")).Text;
                        dr["ItemCode"] = GrdRequisition.Rows[k].Cells[2].Text;
                        dr["Location"] = GrdRequisition.Rows[k].Cells[7].Text;
                        string str = GrdRequisition.Rows[k].Cells[8].Text;
                        string[] AvlQty = str.Split(' ');
                        dr["AvlQty"] = Convert.ToDecimal(AvlQty[0]);
                        dr["TransitQty"] = Convert.ToDecimal(GrdRequisition.Rows[k].Cells[10].Text);
                        dr["MinStockLevel"] = Convert.ToDecimal(GrdRequisition.Rows[k].Cells[11].Text);
                        dr["MaxStockLevel"] = Convert.ToDecimal(GrdRequisition.Rows[k].Cells[12].Text);
                        dr["AvgRate"] = Convert.ToDecimal(GrdRequisition.Rows[k].Cells[13].Text);
                        dr["AvgRateDate"] = GrdRequisition.Rows[k].Cells[14].Text;
                        dr["Vendor"] = GrdRequisition.Rows[k].Cells[15].Text.Replace("&amp;", "&");
                        dr["Rate"] = GrdRequisition.Rows[k].Cells[16].Text;
                        dr["VendorId"] = Convert.ToInt32(((Label)GrdRequisition.Rows[k].Cells[18].FindControl("lblVendorId")).Text);
                        dr["ItemID"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[k].FindControl("ddlItem")).SelectedValue);
                        dr["txtOrdQty"] = ((TextBox)GrdRequisition.Rows[k].FindControl("txtOrdQty")).Text;
                        dr["IsCancel"] = GrdRequisition.Rows[k].Cells[23].Text;//Change 17 to 19
                        dr["Priority"] = ((Label)GrdRequisition.Rows[k].FindControl("Priority")).Text;
                        dr["PriorityID"] = Convert.ToInt32(((Label)GrdRequisition.Rows[k].FindControl("PriorityID")).Text);
                        dr["ItemDetailsId"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[k].FindControl("ddlItemDescription")).SelectedValue);
                        dr["UnitConvDtlsId"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[k].FindControl("ddlUnitConvertor")).SelectedValue);
                        dr["CategoryId"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[k].FindControl("ddlCategory")).SelectedValue);
                        dr["SubcategoryId"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[k].FindControl("ddlSubCategory")).SelectedValue);
                        dt.Rows.Add(dr);
                        ViewState["Requisition"] = dt;
                        k++;
                    }
                }
            }
            ViewState["CurrentTable"] = dt;
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }



    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // CheckUserRight();
            MakeEmptyForm();
        
            TXTKTO.DataSource = db.Displaygrid("select Email from SuplierMaster ");
            TXTKTO.DataTextField = "Email";
            TXTKTO.DataValueField = "Email";
            TXTKTO.DataBind();
            TXTKTO.Items.Insert(0, "Select Supplier Email");
           
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]


    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["MayurInventory"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Email from SuplierMaster where Email   LIKE '%'+@SearchText+'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Email"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            StrCondition = string.Empty;
            StrCondition = TxtSearch.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetRequisition(StrCondition, " and P.CafeteriaId=" + Convert.ToInt32(Session["CafeteriaId"].ToString()), out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ReportGrid.DataSource = Ds.Tables[0];
                ReportGrid.DataBind();
                Ds = null;
            }
            else
            {
                ReportGrid.DataSource = null;
                ReportGrid.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void GrdSelectAllHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox GrdSelectAllHeader1 = (CheckBox)sender;
        GridViewRow row = (GridViewRow)GrdSelectAllHeader1.NamingContainer;
        int CurrRow = row.RowIndex;
        if (GrdSelectAllHeader1.Checked == true)
        {
            for (int j = 0; j < GrdRequisition.Rows.Count; j++)
            {
                CheckBox GrdSelectAll = (CheckBox)GrdRequisition.Rows[j].Cells[1].FindControl("GrdSelectAll");
                GrdSelectAll.Checked = true;
            }
        }
        else
        {
            for (int j = 0; j < GrdRequisition.Rows.Count; j++)
            {
                CheckBox GrdSelectAll = (CheckBox)GrdRequisition.Rows[j].Cells[1].FindControl("GrdSelectAll");
                GrdSelectAll.Checked = false;
            }
        }
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AjaxControlToolkit.ComboBox ddlItems = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd = (GridViewRow)ddlItems.Parent.Parent;
            int currrow = grd.RowIndex;
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(ddlItems.SelectedValue), out StrError);

            DataTable dt = new DataTable();
            dt = Ds.Tables[0];
            DataTable dt1 = new DataTable();
            dt1 = Ds.Tables[1];
            DataTable dt2 = new DataTable();
            dt2 = Ds.Tables[2];
            DataTable dt3 = new DataTable();
            dt3 = Ds.Tables[3];

            ViewState["ParticularItem"] = Ds.Tables[0];
            DataTable dtt = new DataTable();
            dtt = (DataTable)ViewState["ParticularItem"];
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ViewState["TemplateDetails"] = Ds.Tables[0];
                ((Label)GrdRequisition.Rows[currrow].Cells[0].FindControl("LblEntryId")).Text = "0";// Ds.Tables[0].Rows[0]["#"].ToString();
                GrdRequisition.Rows[currrow].Cells[2].Text = Ds.Tables[0].Rows[0]["ItemCode"].ToString();
                GrdRequisition.Rows[currrow].Cells[7].Text = Ds.Tables[0].Rows[0]["Location"].ToString();
                GrdRequisition.Rows[currrow].Cells[8].Text = Ds.Tables[0].Rows[0]["AvlQty"].ToString();
                GrdRequisition.Rows[currrow].Cells[10].Text = Ds.Tables[0].Rows[0]["TransitQty"].ToString();
                GrdRequisition.Rows[currrow].Cells[11].Text = Ds.Tables[0].Rows[0]["MinStockLevel"].ToString();
                GrdRequisition.Rows[currrow].Cells[12].Text = Ds.Tables[0].Rows[0]["MaxStockLevel"].ToString();
                GrdRequisition.Rows[currrow].Cells[13].Text = Ds.Tables[0].Rows[0]["AvgRate"].ToString();
                GrdRequisition.Rows[currrow].Cells[14].Text = Ds.Tables[0].Rows[0]["AvgRateDate"].ToString();
                GrdRequisition.Rows[currrow].Cells[15].Text = Ds.Tables[0].Rows[0]["Vendor"].ToString();
                GrdRequisition.Rows[currrow].Cells[16].Text = Ds.Tables[0].Rows[0]["Rate"].ToString();
                GrdRequisition.Rows[currrow].Cells[23].Text = Ds.Tables[0].Rows[0]["IsCancel"].ToString();
                ((Label)GrdRequisition.Rows[currrow].Cells[18].FindControl("lblVendorId")).Text = Ds.Tables[0].Rows[0]["VendorId"].ToString();
                ((CheckBox)GrdRequisition.Rows[currrow].Cells[0].FindControl("GrdSelectAll")).Checked = true;
                ((TextBox)GrdRequisition.Rows[currrow].FindControl("txtAvlQty")).Text = Ds.Tables[0].Rows[0]["AvlQty"].ToString();
                ((Label)GrdRequisition.Rows[currrow].Cells[24].FindControl("PriorityID")).Text = Ds.Tables[0].Rows[0]["PriorityID"].ToString();
                ((Label)GrdRequisition.Rows[currrow].Cells[21].FindControl("Priority")).Text = Ds.Tables[0].Rows[0]["Priority"].ToString();
                ((TextBox)GrdRequisition.Rows[currrow].FindControl("GrdRemark")).Text = "";
                MergeData(currrow);
            }
            else
            {
                ((Label)GrdRequisition.Rows[currrow].Cells[0].FindControl("LblEntryId")).Text = "0";
                GrdRequisition.Rows[currrow].Cells[2].Text = "0";
                GrdRequisition.Rows[currrow].Cells[7].Text = "";
                GrdRequisition.Rows[currrow].Cells[8].Text = "0";
                GrdRequisition.Rows[currrow].Cells[10].Text = "0";
                GrdRequisition.Rows[currrow].Cells[11].Text = "0";
                GrdRequisition.Rows[currrow].Cells[12].Text = "0";
                GrdRequisition.Rows[currrow].Cells[13].Text = "0";
                GrdRequisition.Rows[currrow].Cells[14].Text = "";
                GrdRequisition.Rows[currrow].Cells[15].Text = "";
                GrdRequisition.Rows[currrow].Cells[16].Text = "";
                GrdRequisition.Rows[currrow].Cells[23].Text = "0";
                GrdRequisition.Rows[currrow].Cells[18].Text = "0";
                GrdRequisition.Rows[currrow].Cells[13].Text = "0";
                ((CheckBox)GrdRequisition.Rows[currrow].Cells[0].FindControl("GrdSelectAll")).Checked = false;
                ((TextBox)GrdRequisition.Rows[currrow].FindControl("txtAvlQty")).Text = "0";
                //((TextBox)GrdRequisition.Rows[currrow].FindControl("Remark")).Text = "";
            }
            GetAvaliableQuantityForItem(currrow);


            #region[Bind ItemDescription and UnitConverSion]
            if (Ds.Tables[2].Rows.Count > 0)
            {
                ViewState["ItemDesCriptionList"] = Ds.Tables[2];
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItemDescription")).DataSource = Ds.Tables[2];
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItemDescription")).DataTextField = "ItemDesc";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItemDescription")).DataValueField = "#";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItemDescription")).DataBind();
                string UNIT = string.Empty;
                for (int i = 1; i < Ds.Tables[2].Rows.Count; i++)
                {
                    UNIT = UNIT + "\n, " + Ds.Tables[2].Rows[i]["ItemDesc"].ToString();
                }
                if (Ds.Tables[2].Rows.Count > 1)
                {
                    obj_Comman.ShowPopUpMsg("For This Particular,\nIndent Can Be Made Using Following Description -\n" + UNIT, this.Page);
                }
            }
            if (Ds.Tables[3].Rows.Count > 0)
            {
                ViewState["UnitConversnList"] = Ds.Tables[3];
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).DataSource = Ds.Tables[3];
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).DataTextField = "UnitFactor";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).DataValueField = "#";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).DataBind();
                if (Ds.Tables[2].Rows.Count <= 1)
                {
                    if (Ds.Tables[3].Rows.Count > 1)
                    {
                        string UNIT1 = string.Empty;
                        for (int i = 0; i < Ds.Tables[3].Rows.Count; i++)
                        {
                            UNIT1 = UNIT1 + "\n, " + Ds.Tables[3].Rows[i]["UnitFactor"].ToString();
                        }
                        obj_Comman.ShowPopUpMsg("For This Particular,\nIndent Can Be Made Using Following UOM -\n" + UNIT1, this.Page);
                    }
                }


            }
            #endregion

            //}
            //else
            //{
            //    // SetInitialRow_GrdRequisition();
            //    obj_Comman.ShowPopUpMsg("Item Already Present!", this.Page);
            //}      
            if (Ds.Tables[2].Rows.Count > 1)
            {
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItemDescription")).Focus();
            }
            else
            {
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).Focus();
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AjaxControlToolkit.ComboBox ddlCategory = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd = (GridViewRow)ddlCategory.Parent.Parent;
            int currrow = grd.RowIndex;
            DataTable dttable1 = new DataTable();
            dttable1 = (DataTable)ViewState["Requisition"];
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetSubCategory(Convert.ToInt32(ddlCategory.SelectedValue), out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["ItemsList"] = Ds.Tables[0];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItem")).DataSource = Ds.Tables[0];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItem")).DataTextField = "Item";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItem")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItem")).DataBind();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItem")).SelectedValue = (Convert.ToInt32(dttable1.Rows[currrow]["ItemID"])).ToString();
                }

                if (Ds.Tables[1].Rows.Count > 0)
                {
                    ViewState["SubCategoryList"] = Ds.Tables[1];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlSubCategory")).DataSource = Ds.Tables[1];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlSubCategory")).DataTextField = "SubCategory";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlSubCategory")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlSubCategory")).DataBind();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlSubCategory")).SelectedValue = (Convert.ToInt32(dttable1.Rows[currrow]["SubcategoryId"])).ToString();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AjaxControlToolkit.ComboBox ddlSubCategory = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd = (GridViewRow)ddlSubCategory.Parent.Parent;
            int currrow = grd.RowIndex;
            DataTable dttable1 = new DataTable();
            dttable1 = (DataTable)ViewState["Requisition"];
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItemsONSubCategory(Convert.ToInt32(ddlSubCategory.SelectedValue), out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["ItemsList"] = Ds.Tables[0];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItem")).DataSource = Ds.Tables[0];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItem")).DataTextField = "Item";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItem")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItem")).DataBind();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItem")).SelectedValue = (Convert.ToInt32(dttable1.Rows[currrow]["ItemID"])).ToString();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlItemDescription_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AjaxControlToolkit.ComboBox ddlItems = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd = (GridViewRow)ddlItems.Parent.Parent;
            AjaxControlToolkit.ComboBox ddlItemDescription = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd1 = (GridViewRow)ddlItemDescription.Parent.Parent;
            int currrow = grd.RowIndex;
            int ItemId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItem")).SelectedValue.ToString());
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(ItemId, out StrError);
            ViewState["ParticularItem"] = Ds.Tables[0];
            DataTable dtt = new DataTable();
            dtt = (DataTable)ViewState["ParticularItem"];
            CheckDuplicateEntryItem(Ds, ItemId, Convert.ToInt32(ddlItems.SelectedValue));
            if (ItemDuplicate == false)
            {
                if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
                {
                    DataSet DSUNIT = new DataSet();
                    DSUNIT = Obj_RequisitionCafeteria.GetUnitOnItemDesc(ItemId, Convert.ToInt32(ddlItems.SelectedValue), out StrError);
                    ViewState["UnitConversnList"] = DSUNIT.Tables[0];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).DataSource = DSUNIT.Tables[0];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).DataTextField = "UnitFactor";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).DataBind();
                    string UNIT = string.Empty;
                    for (int i = 0; i < DSUNIT.Tables[0].Rows.Count; i++)
                    {
                        UNIT = UNIT + "\n, " + DSUNIT.Tables[0].Rows[i]["UnitFactor"].ToString();

                        GrdRequisition.Rows[currrow].Cells[8].Text = DSUNIT.Tables[0].Rows[0]["OpeningStock"].ToString();
                    }
                    if (DSUNIT.Tables[0].Rows.Count > 1)
                    {
                        obj_Comman.ShowPopUpMsg("For This Particular,\nIndent Can Be Made Using Following Units -\n" + UNIT, this.Page);
                    }
                }
                if (Convert.ToInt32(ddlItems.SelectedValue) == 0)
                {
                    DataSet DSUNIT = new DataSet();
                    DSUNIT = Obj_RequisitionCafeteria.GetUnitOnItemDesc(ItemId, 0, out StrError);
                    ViewState["UnitConversnList"] = DSUNIT.Tables[0];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).DataSource = DSUNIT.Tables[0];
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).DataTextField = "UnitFactor";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).DataBind();
                    string UNIT = string.Empty;
                    for (int i = 0; i < DSUNIT.Tables[0].Rows.Count; i++)
                    {
                        UNIT = UNIT + "\n, " + DSUNIT.Tables[0].Rows[i]["UnitFactor"].ToString();

                        GrdRequisition.Rows[currrow].Cells[8].Text = DSUNIT.Tables[0].Rows[0]["OpeningStock"].ToString();
                    }
                    if (DSUNIT.Tables[0].Rows.Count > 1)
                    {
                        obj_Comman.ShowPopUpMsg("For This Particular,\nIndent Can Be Made Using Following Units -\n" + UNIT, this.Page);
                    }
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Item and Item Description is already present!", this.Page);
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlItem")).SelectedValue = "0";
                ddlItems.SelectedValue = "0";
            }
            ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[currrow].FindControl("ddlUnitConvertor")).Focus();

        }
        catch (Exception ex)
        {

        }
    }

    private bool chkItemDesc()
    {

        for (int i = 0; i < GrdRequisition.Rows.Count; i++)
        {
            int a = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItemDescription")).Items.Count.ToString());
            if (a > 1)
            {
                if (Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItemDescription")).SelectedValue.ToString()) <= 0)
                {
                    return false;
                }

            }
        }
        return true;
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int UnitConvDtlsIdT = 0, ItemId = 0; decimal Qty = 0;
        DataSet Ds = new DataSet();
        try
        {
            FindDataInGrid();
            if (fillgrid == true)
            {
                CheckDuplicate();
                CheckOrderAmount_New();
                if (valueofitem == 2)
                {
                    if (clear == true)
                    {
                        CheckTextBox();
                        if (flag1 == true)
                        {
                            Entity_RequisitionCafeteria.RequisitionNo = "" + txtReqNo.Text;
                            string Reqno = "" + txtReqNo.Text;

                            Ds = Obj_RequisitionCafeteria.CkeckDuplicateSaveTime(Reqno, out StrError);
                            if (Ds.Tables[0].Rows.Count > 0)
                            {
                                obj_Comman.ShowPopUpMsg("Duplicate Indent !", this.Page);
                                MakeEmptyForm();
                                return;
                            }
                            Entity_RequisitionCafeteria.RequisitionDate = !string.IsNullOrEmpty(txtReqDate.Text) ? Convert.ToDateTime(txtReqDate.Text.Trim()) : Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            Entity_RequisitionCafeteria.CafeteriaId = Convert.ToInt32(Session["TransactionSiteID"].ToString());//CafeteriaNo
                            Entity_RequisitionCafeteria.UserId = Convert.ToInt32(Session["UserId"]);
                            Entity_RequisitionCafeteria.IsCostCentre = Convert.ToInt32(ddlCostCentre.SelectedValue);
                            Entity_RequisitionCafeteria.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            Entity_RequisitionCafeteria.Remark = TXTREMARK.Text;
                            Entity_RequisitionCafeteria.RemarkIND = txtindremark.Text;
                            insertrow = Obj_RequisitionCafeteria.InsertRequisitionDetails(ref Entity_RequisitionCafeteria, out StrError);
                            int MaxID = insertrow;
                            int insertdtls = 0;
                            if (insertrow != 0)
                            {
                                for (int g = 0; g < GrdRequisition.Rows.Count; g++)
                                {
                                    if (Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text) > 0)
                                    {
                                        Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                        string GetString = (GrdRequisition.Rows[g].Cells[8].Text.ToString());
                                        if (GetString[0].Equals('-'))
                                        {
                                            GetString = GetString.Remove(0, 1);
                                        }
                                        string[] qw1 = GetString.Split('-');
                                        Entity_RequisitionCafeteria.AvlQty = Convert.ToDecimal(qw1[0].ToString());
                                        Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text);
                                        Entity_RequisitionCafeteria.VendorId = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("lblVendorId")).Text);
                                        Entity_RequisitionCafeteria.Rate = Convert.ToDecimal(GrdRequisition.Rows[g].Cells[16].Text);
                                        Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(GrdRequisition.Rows[g].Cells[10].Text);
                                        Entity_RequisitionCafeteria.MinStockLevel = GrdRequisition.Rows[g].Cells[11].Text;
                                        Entity_RequisitionCafeteria.MaxStockLevel = GrdRequisition.Rows[g].Cells[12].Text;


                                        TextBox txtToDate = (TextBox)GrdRequisition.Rows[g].Cells[29].FindControl("txtRequiredDate");

                                        if (string.IsNullOrEmpty(txtToDate.Text))
                                        {
                                            Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                        }
                                        else
                                        {
                                            Entity_RequisitionCafeteria.RequiredDate = Convert.ToDateTime(txtToDate.Text);
                                        }


                                        //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                        Entity_RequisitionCafeteria.TemplateID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("LblEntryId")).Text);
                                        Entity_RequisitionCafeteria.ItemId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlItem")).SelectedValue);
                                        Entity_RequisitionCafeteria.ExpdDate = !string.IsNullOrEmpty(txtTempDate.Text) ? Convert.ToDateTime(txtTempDate.Text) : Convert.ToDateTime(DateTime.Now.AddDays(3));
                                        Entity_RequisitionCafeteria.IsCancel = Convert.ToBoolean(Convert.ToInt32(GrdRequisition.Rows[g].Cells[23].Text));// here 17 replace with 19
                                                                                                                                                         // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                                                                                                                                         //--Newly Added Field--
                                        Entity_RequisitionCafeteria.ItemDetailsId = !String.IsNullOrEmpty(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlItemDescription")).SelectedValue) ? Convert.ToInt32((((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlItemDescription")).SelectedValue)) : 0;
                                        Entity_RequisitionCafeteria.UnitConvDtlsId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlUnitConvertor")).SelectedValue);
                                        Entity_RequisitionCafeteria.RemarkForPO = (((TextBox)GrdRequisition.Rows[g].FindControl("GrdRemark")).Text);

                                        #region[Convert Quantity accordng to UnitFactor]
                                        //---Coversionfactor---
                                        UnitConvDtlsIdT = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlUnitConvertor")).SelectedValue);
                                        ItemId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlItem")).SelectedValue);//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                        string unitconvrt = (((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlUnitConvertor")).SelectedItem).ToString();
                                        DataSet DsTemp = new DataSet();
                                        DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                        if (DsTemp.Tables.Count > 0)
                                        {
                                            for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                            {
                                                if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                {
                                                    Qty = (Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                }
                                                else
                                                {
                                                    Qty = (Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text));
                                                }
                                            }
                                        }

                                        Entity_RequisitionCafeteria.Qty = Qty;
                                        #endregion
                                        insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                    }
                                }
                            }
                            if (insertdtls != 0)
                            {
                                obj_Comman.ShowPopUpMsg("Record Saved Successfully!", this.Page);
                                //SendMail(insertrow);
                                MakeEmptyForm();
                            }
                            else
                            {
                                obj_Comman.ShowPopUpMsg(StrError, this.Page);
                            }
                        }
                        else
                        {
                            obj_Comman.ShowPopUpMsg("Please Enter The Order Quantity", this.Page);
                        }
                    }
                    else
                    {
                        obj_Comman.ShowPopUpMsg("Name Already Present..!", this.Page);
                        MakeEmptyForm();
                    }
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("Please Enter Order Quantity", this.Page);
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Please Selet Atleast 1 Item", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        string abc = ddlCostCentre.SelectedItem.ToString();

        if (abc == "-- Select Project--")
        {
            int UnitConvDtlsIdT = 0, ItemId = 0; decimal Qty = 0;
            try
            {
                if (ViewState["EditID"] != null)
                {
                    Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(ViewState["EditID"]);
                }
                Entity_RequisitionCafeteria.RequisitionDate = !string.IsNullOrEmpty(txtReqDate.Text) ? Convert.ToDateTime(txtReqDate.Text.Trim()) : Convert.ToDateTime(DateTime.Now.ToShortDateString());
                Entity_RequisitionCafeteria.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_RequisitionCafeteria.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                Entity_RequisitionCafeteria.IsCostCentre = Convert.ToInt32(ddlCostCentre.SelectedValue);
                Entity_RequisitionCafeteria.Remark = TXTREMARK.Text;
                Entity_RequisitionCafeteria.RemarkIND = txtindremark.Text;
                insertrow = Obj_RequisitionCafeteria.UpdateRequisitionDetails(ref Entity_RequisitionCafeteria, out StrError);
                int UpdateRow = 0;
                if (insertrow != 0)
                {
                    for (int g = 0; g < GrdRequisition.Rows.Count; g++)
                    {
                        if (((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text == "")
                        {
                            ((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text = "0";
                        }
                        if (Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text) > 0)
                        {
                            Entity_RequisitionCafeteria.TemplateID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("LblEntryId")).Text);
                            // string[] qw1 = (GrdRequisition.Rows[g].Cells[8].Text.ToString()).Split('-');
                            string GetString = (GrdRequisition.Rows[g].Cells[8].Text.ToString());
                            if (GetString[0].Equals('-'))
                            {
                                GetString = GetString.Remove(0, 1);
                            }
                            string[] qw1 = GetString.Split('-');
                            Entity_RequisitionCafeteria.AvlQty = Convert.ToDecimal(qw1[0].ToString());
                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text);
                            Entity_RequisitionCafeteria.VendorId = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("lblVendorId")).Text);
                            Entity_RequisitionCafeteria.Rate = Convert.ToDecimal(GrdRequisition.Rows[g].Cells[16].Text);
                            Entity_RequisitionCafeteria.ItemId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlItem")).SelectedValue);//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                                                                                                                                                             //Entity_RequisitionCafeteria.Qty = Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text);
                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(GrdRequisition.Rows[g].Cells[10].Text);
                            Entity_RequisitionCafeteria.MinStockLevel = GrdRequisition.Rows[g].Cells[11].Text;
                            Entity_RequisitionCafeteria.MaxStockLevel = GrdRequisition.Rows[g].Cells[12].Text;
                            TextBox txtToDate = (TextBox)GrdRequisition.Rows[g].Cells[29].FindControl("txtRequiredDate");

                            if (string.IsNullOrEmpty(txtToDate.Text))
                            {
                                Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                            }
                            else
                            {
                                Entity_RequisitionCafeteria.RequiredDate = Convert.ToDateTime(txtToDate.Text);
                            }
                            //Entity_RequisitionCafeteria.RequiredDate = Convert.ToDateTime(((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);

                            Entity_RequisitionCafeteria.ExpdDate = !string.IsNullOrEmpty(txtTempDate.Text) ? Convert.ToDateTime(txtTempDate.Text) : Convert.ToDateTime(DateTime.Now.AddDays(3));
                            Entity_RequisitionCafeteria.IsCancel = Convert.ToBoolean(Convert.ToInt32(GrdRequisition.Rows[g].Cells[23].Text));//here 17 replace with 19

                            if (((Label)GrdRequisition.Rows[g].Cells[24].FindControl("PriorityID")).Text == " ")
                            {
                                Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].Cells[24].FindControl("PriorityID")).Text);//adding code on 5/1/13 for priority
                            }
                            else
                            {
                                Entity_RequisitionCafeteria.PriorityID = 0;
                            }

                            //--Newly Added Field--
                            Entity_RequisitionCafeteria.ItemDetailsId = !String.IsNullOrEmpty(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlItemDescription")).SelectedValue) ? Convert.ToInt32((((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlItemDescription")).SelectedValue)) : 0;
                            Entity_RequisitionCafeteria.UnitConvDtlsId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlUnitConvertor")).SelectedValue);
                            Entity_RequisitionCafeteria.RemarkForPO = (((TextBox)GrdRequisition.Rows[g].FindControl("GrdRemark")).Text);

                            #region[Convert Quantity accordng to UnitFactor]
                            //---Coversionfactor---
                            UnitConvDtlsIdT = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlUnitConvertor")).SelectedValue);
                            ItemId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlItem")).SelectedValue);//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                            string unitconvrt = (((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlUnitConvertor")).SelectedItem).ToString();
                            DataSet DsTemp = new DataSet();
                            DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                            if (DsTemp.Tables.Count > 0)
                            {
                                for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                {
                                    if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                    {
                                        Qty = (Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                    }
                                    else
                                    {
                                        Qty = (Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text));
                                    }
                                }
                            }

                            Entity_RequisitionCafeteria.Qty = Qty;
                            #endregion

                            UpdateRow = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                        }
                    }
                }
                if (UpdateRow != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Updated Successfully!", this.Page);
                    MakeEmptyForm();
                }
                else
                {
                    obj_Comman.ShowPopUpMsg(StrError, this.Page);
                }
            }
            catch (Exception ex)
            {

            }
        }

        else
        {


            if (chkrevised.Checked == true)
            {

                string newrevisednumber = "";

              string str=  lblReqNo.Text;


                int rv = Convert.ToInt32(db.getDb_Value("select  top 1  rc.revised  from RequisitionCafeteria RC  INNER JOIN RequisitionCafeDtls RCD    ON RC.RequisitionCafeId = RCD.RequisitionCafeId   where  RC.IsCostCentre='" + ddlCostCentre.SelectedValue + "'  and  RCD.TemplateID!='" + "0" + "'  order by rc.RequisitionCafeId desc "));

                rv++;


                string[] split = str.Split('/');

                string str0 = split[0];
                string str1 = split[1];
                string str2 = split[2];
                string str3 = split[3];
                string str4 = split[4];

                newrevisednumber = str0 + " /" + str1 + "/" + str2 + "/" + str3 + "/" + str4 + "/" + "R" + "0" + rv.ToString();


               


                db.insert("UPDATE RC      SET RC.status='" + "0" + "'  from RequisitionCafeteria RC  INNER JOIN RequisitionCafeDtls RCD    ON RC.RequisitionCafeId = RCD.RequisitionCafeId  where  RC.IsCostCentre='" + ddlCostCentre.SelectedValue + "' and  RCD.TemplateID!='" + "0" + "' ");










                string ps = "IND" + "-" + ddlCostCentre.SelectedValue;
                    db.insert("insert into  poprint values('" + ps + "' ,'" + ddlCostCentre.SelectedValue + "')  ");

                





                    int UnitConvDtlsIdT = 0, ItemId = 0; decimal Qty = 0;
                    DataSet Ds = new DataSet();
                    try
                    {
                        FindDataInGrid();
                        if (fillgrid == true)
                        {
                            CheckDuplicate();
                            // CheckOrderAmount_New();
                            if (valueofitem == 0)
                            {
                                if (clear == true)
                                {
                                    CheckTextBox();
                                    if (flag1 == false)
                                    {
                                    Entity_RequisitionCafeteria.RequisitionNo = newrevisednumber;
                                        string Reqno = newrevisednumber;

                                        Ds = Obj_RequisitionCafeteria.CkeckDuplicateSaveTime(Reqno, out StrError);
                                        if (Ds.Tables[0].Rows.Count > 0)
                                        {
                                            obj_Comman.ShowPopUpMsg("Duplicate Indent !", this.Page);
                                            MakeEmptyForm();
                                            return;
                                        }
                                        Entity_RequisitionCafeteria.RequisitionDate = !string.IsNullOrEmpty(txtReqDate.Text) ? Convert.ToDateTime(txtReqDate.Text.Trim()) : Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                        Entity_RequisitionCafeteria.CafeteriaId = Convert.ToInt32(Session["TransactionSiteID"].ToString());//CafeteriaNo
                                        Entity_RequisitionCafeteria.UserId = Convert.ToInt32(Session["UserId"]);
                                        Entity_RequisitionCafeteria.IsCostCentre = Convert.ToInt32(ddlCostCentre.SelectedValue);
                                        Entity_RequisitionCafeteria.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                        Entity_RequisitionCafeteria.Remark = TXTREMARK.Text;
                                        Entity_RequisitionCafeteria.RemarkIND = txtindremark.Text;
                                        insertrow = Obj_RequisitionCafeteria.InsertRequisitionDetails(ref Entity_RequisitionCafeteria, out StrError);
                                    db.insert("UPDATE RC      SET status = 0   from RequisitionCafeteria RC  INNER JOIN RequisitionCafeDtls RCD    ON RC.RequisitionCafeId = RCD.RequisitionCafeId  where  RC.IsCostCentre='" + ddlCostCentre.SelectedValue + "' and  RCD.TemplateID!='" + "0" + "'");
                                    db.insert("update RequisitionCafeteria set  revised='" + rv + "' ,status='"+"1"+"' where  RequisitionNo='" + newrevisednumber + "'");

                                        int MaxID = insertrow;
                                        int insertdtls = 0;
                                        if (insertrow != 0)
                                        {




                                            for (int i = 0; i < this.gvCustomers.Rows.Count; i++)
                                            {

                                                string temaplatename = gvCustomers.Rows[i].Cells[1].Text;

                                                string qty = gvCustomers.Rows[i].Cells[2].Text;
                                                GridView inner = this.gvCustomers.Rows[i].FindControl("gvOrders") as GridView;






                                                foreach (GridViewRow gvrow in inner.Rows)
                                                {


                                                    CheckBox chkRow = (gvrow.FindControl("chkbox") as CheckBox);
                                                    if (chkRow.Checked)
                                                    {
                                                        Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                                        Label TXTITEMNAME = gvrow.FindControl("TXTITEMNAME") as Label;



                                                        string GetString = (gvrow.Cells[8].Text.ToString());
                                                        if (GetString == "&nbsp;")
                                                        {
                                                            Entity_RequisitionCafeteria.AvlQty = Convert.ToDecimal(1);
                                                        }
                                                        else
                                                        {

                                                            if (GetString[0].Equals('-'))
                                                            {
                                                                GetString = GetString.Remove(0, 1);
                                                            }


                                                            string[] qw1 = GetString.Split('-');
                                                            Entity_RequisitionCafeteria.AvlQty = Convert.ToDecimal(qw1[0].ToString());

                                                        }
                                                        Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)gvrow.FindControl("txtorderqty")).Text);
                                                        Entity_RequisitionCafeteria.VendorId = Convert.ToInt32(((Label)gvrow.FindControl("lblVendorId")).Text);
                                                        Entity_RequisitionCafeteria.Rate = Convert.ToDecimal(((Label)gvrow.FindControl("lblrate")).Text);
                                                        Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(gvrow.Cells[9].Text);
                                                        Entity_RequisitionCafeteria.MinStockLevel = gvrow.Cells[10].Text;
                                                        Entity_RequisitionCafeteria.MaxStockLevel = gvrow.Cells[11].Text;
                                                        Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                                        Entity_RequisitionCafeteria.TemplateID = Convert.ToInt32(((Label)gvrow.FindControl("LblEntryId")).Text);
                                                        Entity_RequisitionCafeteria.ItemId = Convert.ToInt32(((Label)gvrow.FindControl("lblitem")).Text);

                                                        Entity_RequisitionCafeteria.ExpdDate = !string.IsNullOrEmpty(txtTempDate.Text) ? Convert.ToDateTime(txtTempDate.Text) : Convert.ToDateTime(DateTime.Now.AddDays(3));
                                                        Entity_RequisitionCafeteria.IsCancel = false;
                                                        Entity_RequisitionCafeteria.ItemDetailsId = !String.IsNullOrEmpty(((Label)gvrow.FindControl("lblItemDetailsId")).Text) ? Convert.ToInt32((((Label)gvrow.FindControl("lblItemDetailsId")).Text)) : 0;
                                                        Entity_RequisitionCafeteria.UnitConvDtlsId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)gvrow.FindControl("ddlUnitConvertor")).SelectedValue);
                                                        Entity_RequisitionCafeteria.RemarkForPO = (((TextBox)gvrow.FindControl("GrdRemark")).Text);
                                                        #region[Convert Quantity accordng to UnitFactor]
                                                        //---Coversionfactor---
                                                        UnitConvDtlsIdT = Convert.ToInt32(((AjaxControlToolkit.ComboBox)gvrow.FindControl("ddlUnitConvertor")).SelectedValue);
                                                        ItemId = Convert.ToInt32(((Label)gvrow.FindControl("lblitem")).Text);//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                                        string unitconvrt = Convert.ToInt32(((AjaxControlToolkit.ComboBox)gvrow.FindControl("ddlUnitConvertor")).SelectedValue).ToString();
                                                        DataSet DsTemp = new DataSet();
                                                        DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                                        if (DsTemp.Tables.Count > 0)
                                                        {
                                                            for (int j = 0; j < DsTemp.Tables[0].Rows.Count; j++)
                                                            {
                                                                if (unitconvrt.Equals(DsTemp.Tables[0].Rows[j]["Unit"].ToString()))
                                                                {
                                                                    Qty = (Convert.ToDecimal(((TextBox)gvrow.FindControl("txtorderqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[j]["Factor"].ToString());
                                                                }
                                                                else
                                                                {
                                                                    Qty = (Convert.ToDecimal(((TextBox)gvrow.FindControl("txtorderqty")).Text));
                                                                }
                                                            }
                                                        }

                                                        Entity_RequisitionCafeteria.Qty = Qty;
                                                        #endregion
                                                        insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                                    }
                                                }
                                            }









                                        SqlCommand cmd = new SqlCommand("   select  TemplateName , qty, TemplateID  from  addrowindent     where estmateno='" + lblReqNo.Text + "'  ");
                                        cmd.Connection = cn;

                                        cn.Open();
                                        SqlDataReader rdr = cmd.ExecuteReader();


                                        while (rdr.Read())
                                        {
                                            string TemplateName = (rdr["TemplateName"]).ToString();
                                            string TemplateID = (rdr["TemplateID"]).ToString();
                                            int qty = Convert.ToInt32(rdr["qty"]);
                                            Session["qty"] = qty.ToString();

                                           
                                            

                                            db.insert("insert into addrowindent values('" + newrevisednumber + "' ,'" + TemplateName + "' ,'" + qty + "' ,'" + TemplateID + "')");


                                        }






                                    }
                                        if (insertdtls != 0)
                                        {
                                            obj_Comman.ShowPopUpMsg("Record Saved Successfully!", this.Page);
                                            //SendMail(insertrow);
                                            MakeEmptyForm();
                                            GrdRequisition.Visible = true;
                                        }
                                        else
                                        {
                                            obj_Comman.ShowPopUpMsg(StrError, this.Page);
                                        }
                                    }
                                    else
                                    {
                                        obj_Comman.ShowPopUpMsg("Please Enter The Order Quantity", this.Page);
                                    }
                                }
                                else
                                {
                                    obj_Comman.ShowPopUpMsg("Name Already Present..!", this.Page);
                                    MakeEmptyForm();
                                }
                            }
                            else
                            {
                                obj_Comman.ShowPopUpMsg("Please Enter Order Quantity", this.Page);
                            }
                        }
                        else
                        {
                            obj_Comman.ShowPopUpMsg("Please Selet Atleast 1 Item", this.Page);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                








            }














            else
            {


                int UnitConvDtlsIdT = 0, ItemId = 0; decimal Qty = 0;
                try
                {
                    if (ViewState["EditID"] != null)
                    {
                        Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(ViewState["EditID"]);
                    }
                    Entity_RequisitionCafeteria.RequisitionDate = !string.IsNullOrEmpty(txtReqDate.Text) ? Convert.ToDateTime(txtReqDate.Text.Trim()) : Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    Entity_RequisitionCafeteria.UserId = Convert.ToInt32(Session["UserId"]);
                    Entity_RequisitionCafeteria.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    Entity_RequisitionCafeteria.IsCostCentre = Convert.ToInt32(ddlCostCentre.SelectedValue);
                    Entity_RequisitionCafeteria.Remark = TXTREMARK.Text;
                    Entity_RequisitionCafeteria.RemarkIND = txtindremark.Text;

                    insertrow = Obj_RequisitionCafeteria.UpdateRequisitionDetails(ref Entity_RequisitionCafeteria, out StrError);
                    int UpdateRow = 0;
                    if (insertrow != 0)
                    {
                        for (int i = 0; i < this.gvCustomers.Rows.Count; i++)
                        {

                            string temaplatename = gvCustomers.Rows[i].Cells[1].Text;

                            string qty = gvCustomers.Rows[i].Cells[2].Text;
                            GridView inner = this.gvCustomers.Rows[i].FindControl("gvOrders") as GridView;






                            foreach (GridViewRow gvrow in inner.Rows)
                            {

                                CheckBox chkRow = (gvrow.FindControl("chkbox") as CheckBox);
                                if (chkRow.Checked)
                                {
                                    if (((TextBox)gvrow.FindControl("txtorderqty")).Text == "")
                                    {
                                        ((TextBox)gvrow.FindControl("txtorderqty")).Text = "0";
                                    }
                                    if (Convert.ToDecimal(((TextBox)gvrow.FindControl("txtorderqty")).Text) > 0)
                                    {
                                        Entity_RequisitionCafeteria.TemplateID = Convert.ToInt32(((Label)gvrow.FindControl("LblEntryId")).Text);
                                        // string[] qw1 = (GrdRequisition.Rows[g].Cells[8].Text.ToString()).Split('-');
                                        string GetString = (gvrow.Cells[8].Text.ToString());
                                        if (GetString[0].Equals('-'))
                                        {
                                            GetString = GetString.Remove(0, 1);
                                        }
                                        string[] qw1 = GetString.Split('-');
                                        Entity_RequisitionCafeteria.AvlQty = Convert.ToDecimal(qw1[0].ToString());
                                        Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)gvrow.FindControl("txtorderqty")).Text);
                                        Entity_RequisitionCafeteria.VendorId = Convert.ToInt32(((Label)gvrow.FindControl("lblVendorId")).Text);
                                        Entity_RequisitionCafeteria.Rate = Convert.ToDecimal(((Label)gvrow.FindControl("lblrate")).Text);
                                        Entity_RequisitionCafeteria.ItemId = Convert.ToInt32(((Label)gvrow.FindControl("lblitem")).Text);//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);

                                        Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(gvrow.Cells[9].Text);
                                        Entity_RequisitionCafeteria.MinStockLevel = gvrow.Cells[10].Text;
                                        Entity_RequisitionCafeteria.MaxStockLevel = gvrow.Cells[11].Text;



                                        Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;


                                        //Entity_RequisitionCafeteria.RequiredDate = Convert.ToDateTime(((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);

                                        Entity_RequisitionCafeteria.ExpdDate = !string.IsNullOrEmpty(txtTempDate.Text) ? Convert.ToDateTime(txtTempDate.Text) : Convert.ToDateTime(DateTime.Now.AddDays(3));
                                        Entity_RequisitionCafeteria.IsCancel = false;


                                        Entity_RequisitionCafeteria.PriorityID = 0;


                                        //--Newly Added Field--
                                        Entity_RequisitionCafeteria.ItemDetailsId = !String.IsNullOrEmpty(((Label)gvrow.FindControl("lblItemDetailsId")).Text) ? Convert.ToInt32((((Label)gvrow.FindControl("lblItemDetailsId")).Text)) : 0;
                                        Entity_RequisitionCafeteria.UnitConvDtlsId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)gvrow.FindControl("ddlUnitConvertor")).SelectedValue);
                                        Entity_RequisitionCafeteria.RemarkForPO = (((TextBox)gvrow.FindControl("GrdRemark")).Text);

                                        #region[Convert Quantity accordng to UnitFactor]
                                        //---Coversionfactor---
                                        UnitConvDtlsIdT = Convert.ToInt32(((AjaxControlToolkit.ComboBox)gvrow.FindControl("ddlUnitConvertor")).SelectedValue);
                                        ItemId = Convert.ToInt32(((Label)gvrow.FindControl("lblitem")).Text); //Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                        string unitconvrt = Convert.ToInt32(((AjaxControlToolkit.ComboBox)gvrow.FindControl("ddlUnitConvertor")).SelectedValue).ToString();
                                        DataSet DsTemp = new DataSet();
                                        DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                        if (DsTemp.Tables.Count > 0)
                                        {
                                            for (int j = 0; j < DsTemp.Tables[0].Rows.Count; j++)
                                            {
                                                if (unitconvrt.Equals(DsTemp.Tables[0].Rows[j]["Unit"].ToString()))
                                                {
                                                    Qty = (Convert.ToDecimal(((TextBox)gvrow.FindControl("txtorderqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[j]["Factor"].ToString());
                                                }
                                                else
                                                {
                                                    Qty = (Convert.ToDecimal(((TextBox)gvrow.FindControl("txtorderqty")).Text));
                                                }
                                            }
                                        }

                                        Entity_RequisitionCafeteria.Qty = Qty;
                                        #endregion

                                        UpdateRow = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                    }
                                }
                            }
























                        }
                        if (UpdateRow != 0)
                        {
                            obj_Comman.ShowPopUpMsg("Record Updated Successfully!", this.Page);
                            MakeEmptyForm();
                        }
                        else
                        {
                            obj_Comman.ShowPopUpMsg(StrError, this.Page);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
    
    public static string[] GetCompletionListAll(string prefixText, int count, string contextKey)
    {
        database db = new database();


        List<string> SearchList = new List<string>();
        List<string> result = new List<string>();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("select  Email from SuplierMaster where Email  LIKE '%'+@SearchText+'%'", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchList.Add(dr["Email"].ToString());
                }
                return SearchList.ToArray();
            }
        }


    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {
        try
        {
            Ds = new DataSet();

            Session["categoryid"] = "";
            Session["templateid"] = ddlTemplate.SelectedValue.ToString();

            Ds = Obj_RequisitionCafeteria.GetTemplateData(Convert.ToInt32(ddlTemplate.SelectedValue), string.IsNullOrEmpty(TXTTEMPLATEMULTIPLY.Text) ? 1 : Convert.ToDecimal(TXTTEMPLATEMULTIPLY.Text), out StrError);

            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                BindItemToGridAll(Ds);

                // GrdSelectAll_CheckedChanged(sender, e);
                lblTotalAmt.Text = "0.00";
                ((TextBox)GrdRequisition.Rows[0].FindControl("txtOrdQty")).Focus();
            }
            else
            {
                SetInitialRow_GrdRequisition();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Convert.ToBoolean(ViewState["flag"]) == false)
            {
                int rowIndex = 0;
                if (ViewState["Requisition"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["Requisition"];
                    DataRow drCurrentRow = null;

                    DataTable uniqueCols = dtCurrentTable.DefaultView.ToTable(true, "ItemID");
                    while (dtCurrentTable.Rows.Count > 0)
                    {
                        int count = 0;
                        for (int q = 0; q < dtCurrentTable.Rows.Count; q++)
                        {
                            if ((dtCurrentTable.Rows[q]["#"].ToString()) == "")
                            {
                                count += 1;
                                dtCurrentTable.Rows.RemoveAt(q);
                                goto l1;
                            }
                        }
                        l1: if (count == 0)
                        {
                            goto l2;
                        }
                        else
                        {
                            continue;
                        }
                        l2: break;
                    }

                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        int rcount = GrdRequisition.Rows.Count;
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            if ((dtCurrentTable.Rows[rowIndex]["#"].ToString()) == "" && (dtCurrentTable.Rows[rowIndex]["#"].ToString()) == "0")
                            {
                                dtCurrentTable.Rows.RemoveAt(rowIndex);
                                //DupFlag = false;
                            }
                            else
                            {
                                Label li = (Label)GrdRequisition.Rows[rowIndex].Cells[18].FindControl("lblVendorId");
                                string lo = li.Text;
                                dtCurrentTable.Rows[rowIndex]["#"] = ((Label)GrdRequisition.Rows[rowIndex].Cells[0].FindControl("LblEntryId")).Text;
                                dtCurrentTable.Rows[rowIndex]["ItemCode"] = GrdRequisition.Rows[rowIndex].Cells[2].Text;

                                if ((GrdRequisition.Rows[rowIndex].Cells[7].Text).Equals("&nbsp;"))
                                {
                                    dtCurrentTable.Rows[rowIndex]["Location"] = "";
                                }
                                else
                                {
                                    dtCurrentTable.Rows[rowIndex]["Location"] = GrdRequisition.Rows[rowIndex].Cells[7].Text;
                                }

                                //dtCurrentTable.Rows[rowIndex]["Location"] = !string.IsNullOrEmpty(GrdRequisition.Rows[rowIndex].Cells[7].Text) ? GrdRequisition.Rows[rowIndex].Cells[7].Text : "";
                                string str = GrdRequisition.Rows[rowIndex].Cells[8].Text;
                                string[] AvlQty = str.Split(' ');
                                dtCurrentTable.Rows[rowIndex]["AvlQty"] = Convert.ToDecimal(AvlQty[0]);
                                dtCurrentTable.Rows[rowIndex]["TransitQty"] = Convert.ToDecimal(GrdRequisition.Rows[rowIndex].Cells[10].Text);
                                dtCurrentTable.Rows[rowIndex]["MinStockLevel"] = Convert.ToDecimal(GrdRequisition.Rows[rowIndex].Cells[11].Text);
                                dtCurrentTable.Rows[rowIndex]["MaxStockLevel"] = Convert.ToDecimal(GrdRequisition.Rows[rowIndex].Cells[12].Text);
                                dtCurrentTable.Rows[rowIndex]["AvgRate"] = Convert.ToDecimal(GrdRequisition.Rows[rowIndex].Cells[13].Text);
                                dtCurrentTable.Rows[rowIndex]["AvgRateDate"] = GrdRequisition.Rows[rowIndex].Cells[14].Text;
                                dtCurrentTable.Rows[rowIndex]["Vendor"] = GrdRequisition.Rows[rowIndex].Cells[15].Text.Replace("&amp;", "&");
                                dtCurrentTable.Rows[rowIndex]["Rate"] = GrdRequisition.Rows[rowIndex].Cells[16].Text;
                                dtCurrentTable.Rows[rowIndex]["VendorId"] = Convert.ToInt32(((Label)GrdRequisition.Rows[rowIndex].Cells[18].FindControl("lblVendorId")).Text);//ToInt32
                                //dtCurrentTable.Rows[rowIndex]["ItemID"] = Convert.ToInt32(((DropDownList)GrdRequisition.Rows[rowIndex].Cells[2].FindControl("ddlItem")).SelectedValue);
                                dtCurrentTable.Rows[rowIndex]["ItemID"] = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItem")).SelectedValue);
                                dtCurrentTable.Rows[rowIndex]["txtOrdQty"] = !string.IsNullOrEmpty(((TextBox)GrdRequisition.Rows[rowIndex].FindControl("txtOrdQty")).Text) ? ((TextBox)GrdRequisition.Rows[rowIndex].FindControl("txtOrdQty")).Text : "0";
                                dtCurrentTable.Rows[rowIndex]["IsCancel"] = GrdRequisition.Rows[rowIndex].Cells[23].Text;//Change 17 to 19
                                dtCurrentTable.Rows[rowIndex]["Priority"] = ((Label)GrdRequisition.Rows[rowIndex].FindControl("Priority")).Text;//GrdRequisition.Rows[rowIndex].Cells[17].Text;//adding Field here 5/1/13 for Priority
                                //dtCurrentTable.Rows[rowIndex]["PriorityID"] = Convert.ToInt32(((Label)GrdRequisition.Rows[rowIndex].FindControl("PriorityID")).Text);
                                dtCurrentTable.Rows[rowIndex]["PriorityID"] = !string.IsNullOrEmpty(((Label)GrdRequisition.Rows[rowIndex].FindControl("PriorityID")).Text) ? Convert.ToInt32(((Label)GrdRequisition.Rows[rowIndex].FindControl("PriorityID")).Text) : 0;
                                dtCurrentTable.Rows[rowIndex]["ItemDetailsId"] = !string.IsNullOrEmpty(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItemDescription")).Text) ? Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItemDescription")).SelectedValue) : 0;
                                dtCurrentTable.Rows[rowIndex]["UnitConvDtlsId"] = !string.IsNullOrEmpty(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlUnitConvertor")).SelectedValue) ? Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlUnitConvertor")).SelectedValue) : 0;
                                dtCurrentTable.Rows[rowIndex]["CategoryId"] = !string.IsNullOrEmpty(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlCategory")).SelectedValue) ? Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlCategory")).SelectedValue) : 0;
                                dtCurrentTable.Rows[rowIndex]["SubcategoryId"] = !string.IsNullOrEmpty(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlSubCategory")).SelectedValue) ? Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlSubCategory")).SelectedValue) : 0;
                                dtCurrentTable.Rows[rowIndex]["Remark"] = ((TextBox)GrdRequisition.Rows[rowIndex].FindControl("GrdRemark")).Text;
                                dtCurrentTable.Rows[rowIndex]["ItemName"] = Convert.ToString(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItem")).SelectedItem);
                                dtCurrentTable.Rows[rowIndex]["ItemToolTip"] = Convert.ToString(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItem")).SelectedValue);
                                //TxtItemName.Text = Convert.ToString(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItem")).SelectedItem);
                                //TxtItemName.ToolTip = Convert.ToString(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItem")).SelectedValue);

                                rowIndex++;
                            }
                        }
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["#"] = 0;
                        drCurrentRow["ItemCode"] = string.Empty;
                        drCurrentRow["Location"] = string.Empty;
                        drCurrentRow["AvlQty"] = 0.00;
                        drCurrentRow["TransitQty"] = 0;
                        drCurrentRow["MinStockLevel"] = 0.00;
                        drCurrentRow["MaxStockLevel"] = 0.00;
                        drCurrentRow["AvgRate"] = 0.00;
                        drCurrentRow["AvgRateDate"] = string.Empty;
                        drCurrentRow["Vendor"] = string.Empty;
                        drCurrentRow["Rate"] = 0.00;
                        drCurrentRow["VendorId"] = 0.00;
                        drCurrentRow["ItemID"] = 0;
                        drCurrentRow["txtOrdQty"] = 0.00;
                        drCurrentRow["txtAvlQty"] = 0.00;
                        drCurrentRow["IsCancel"] = "0";
                        drCurrentRow["Priority"] = "";//adding Field here 5/1/13 for Priority
                        drCurrentRow["PriorityID"] = 0;
                        drCurrentRow["ItemDetailsId"] = 0;
                        drCurrentRow["UnitConvDtlsId"] = 0;
                        drCurrentRow["CategoryId"] = 0;
                        drCurrentRow["SubcategoryId"] = 0;
                        drCurrentRow["ItemName"] = string.Empty;
                        drCurrentRow["ItemToolTip"] = 0;
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["Requisition"] = dtCurrentTable;
                        GrdRequisition.DataSource = dtCurrentTable;
                        GrdRequisition.DataBind();
                        GetItemAndCheckBox();
                        SetDataInTextBox();
                    }
                    else
                    {
                        Response.Write("ViewState is null");
                    }
                }
            }
            else
            {

            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            lblTotalAmt.Text = "0.00";
            for (int i = 0; i < GrdRequisition.Rows.Count; i++)
            {
                decimal ordqty = !string.IsNullOrEmpty(((TextBox)GrdRequisition.Rows[i].FindControl("txtOrdQty")).Text) ? Convert.ToDecimal(((TextBox)GrdRequisition.Rows[i].FindControl("txtOrdQty")).Text) : 0;
                decimal Rate = Convert.ToDecimal(GrdRequisition.Rows[i].Cells[13].Text);
                if (Rate > 0)
                {
                    lblTotalAmt.Text = Convert.ToString((Convert.ToDecimal(lblTotalAmt.Text) + Convert.ToDecimal((ordqty) * (Rate))).ToString("0.00"));
                }
                else
                {
                    lblTotalAmt.Text = lblTotalAmt.Text;
                }
                BtnSave.Focus();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
        String[] SearchList = Obj_RequisitionCafeteria.GetSuggestedRecord(prefixText, Lbllocids);
        return SearchList;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionItemNameList(string prefixText, int count, string contextKey)
    {
        DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
        String[] SearchList = Obj_RequisitionCafeteria.GetSuggestedRecordItems(prefixText, "");
        return SearchList;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListForTo(string prefixText, int count, string contextKey)
    {
        var symbols = from c in prefixText
                      where !char.IsLetterOrDigit(c)
                      group c by c into k
                      select new { Char = k.Key, Count = k.Count() };
        string[] MAILIDS = prefixText.Split(',');
        TOSTRING = "";
        for (int rin = 0; rin < MAILIDS.Length - 1; rin++)
        {
            TOSTRING = TOSTRING + MAILIDS[rin].ToString() + ",";
        }
        DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
        String[] SearchList = Obj_RequisitionCafeteria.GetSuggestedRecordForTo(MAILIDS[MAILIDS.Length - 1].ToString(), "");
        return SearchList;
    }

    public void GetItemForEdit()
    {
        for (int i = 0; i < GrdRequisition.Rows.Count; i++)
        {
            if (Convert.ToInt32(((Label)GrdRequisition.Rows[i].Cells[0].FindControl("LblEntryId")).Text) != 0 || Convert.ToInt32(((Label)GrdRequisition.Rows[i].Cells[18].FindControl("lblVendorId")).Text) != 0)
            {
                DataTable dttable2 = new DataTable();
                dttable2 = (DataTable)ViewState["Requisition"];
                if (ViewState["ItemsList"] != null)
                {
                    DataTable dttable = new DataTable();
                    dttable = (DataTable)ViewState["ItemsList"];

                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).DataSource = dttable;
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).DataTextField = "Item";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).DataBind();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).SelectedValue = dttable2.Rows[i]["ItemID"].ToString();
                    ((CheckBox)GrdRequisition.Rows[i].Cells[0].FindControl("GrdSelectAll")).Checked = true;
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlItem")).Enabled = false;
                    ((TextBox)GrdRequisition.Rows[i].Cells[0].FindControl("txtOrdQty")).Text = dttable2.Rows[i]["Qty"].ToString();
                }


                //----For Category----
                if (ViewState["CategoryList"] != null)
                {
                    DataTable dttable = new DataTable();
                    dttable = (DataTable)ViewState["CategoryList"];

                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataSource = dttable;
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataTextField = "Category";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataBind();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).SelectedValue = (Convert.ToInt32(dttable2.Rows[i]["CategoryId"])).ToString();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).Enabled = false;
                }
                //----For SubCategory----
                if (ViewState["SubCategoryList"] != null)
                {
                    DataTable dttable = new DataTable();
                    dttable = (DataTable)ViewState["SubCategoryList"];

                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataSource = dttable;
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataTextField = "SubCategory";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataBind();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).SelectedValue = (Convert.ToInt32(dttable2.Rows[i]["SubcategoryId"])).ToString();
                    ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).Enabled = false;
                }

            }
            else
            {
                DataTable dttable = new DataTable();
                dttable = (DataTable)ViewState["ItemsList"];
                DataTable dttable1 = new DataTable();
                dttable1 = (DataTable)ViewState["CategoryList"];
                DataTable dttable2 = new DataTable();
                dttable2 = (DataTable)ViewState["SubCategoryList"];
                //((DropDownList)GrdRequisition.Rows[i].Cells[2].FindControl("ddlItem")).DataSource = dttable;
                //((DropDownList)GrdRequisition.Rows[i].Cells[2].FindControl("ddlItem")).DataTextField = "Item";
                //((DropDownList)GrdRequisition.Rows[i].Cells[2].FindControl("ddlItem")).DataValueField = "#";
                //((DropDownList)GrdRequisition.Rows[i].Cells[2].FindControl("ddlItem")).DataBind();
                //((CheckBox)GrdRequisition.Rows[i].Cells[0].FindControl("GrdSelectAll")).Checked = false;
                //((DropDownList)GrdRequisition.Rows[i].Cells[2].FindControl("ddlItem")).Enabled = true;

                #region[Item]
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].Cells[2].FindControl("ddlItem")).DataSource = dttable;
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].Cells[2].FindControl("ddlItem")).DataTextField = "Item";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].Cells[2].FindControl("ddlItem")).DataValueField = "#";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].Cells[2].FindControl("ddlItem")).DataBind();
                ((CheckBox)GrdRequisition.Rows[i].Cells[0].FindControl("GrdSelectAll")).Checked = false;
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].Cells[2].FindControl("ddlItem")).Enabled = true;
                #endregion



                #region[Category]
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataSource = dttable1;
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataTextField = "Category";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataValueField = "#";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).DataBind();
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlCategory")).Enabled = true;
                #endregion
                #region[SubCategory]
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataSource = dttable2;
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataTextField = "SubCategory";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataValueField = "#";
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).DataBind();
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[i].FindControl("ddlSubCategory")).Enabled = true;
                #endregion
            }


        }
    }

    protected void GrdRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Delete"):
                    {
                        ViewState["DeleteID"] = Convert.ToInt32(e.CommandArgument);
                        DataTable dtdelete = (DataTable)ViewState["Requisition"];
                        if (GrdRequisition.Rows.Count > 1)
                        {
                            for (int currindex = 0; currindex < dtdelete.Rows.Count; currindex++)
                            {
                                dtdelete.Rows.RemoveAt(Convert.ToInt32(e.CommandArgument));
                                for (int icurrindex = 0; icurrindex < dtdelete.Rows.Count; icurrindex++)
                                {
                                    if ((dtdelete.Rows[icurrindex][0].ToString()) == "")
                                    {
                                        dtdelete.Rows.RemoveAt(icurrindex);
                                    }
                                }
                                break;
                            }
                            GrdRequisition.DataSource = dtdelete;
                            GrdRequisition.DataBind();
                            GetItemAndCheckBox();
                            SetDataInTextBox();
                        }

                    }
                    break;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void GrdRequisition_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GrdRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // GetAvaliableQuantityForItem();
            int ind = e.Row.RowIndex;
            DataSet dss = new DataSet();
            DataTable dttable1 = new DataTable();
            dttable1 = (DataTable)ViewState["Requisition"];
            if (!string.IsNullOrEmpty(dttable1.Rows[ind]["ItemID"].ToString()))
            {
                int ITEMID = Convert.ToInt32(dttable1.Rows[ind]["ItemID"].ToString());
                dss = Obj_RequisitionCafeteria.BindAvaliableQty(ITEMID, Convert.ToInt32(Session["TransactionSiteID"].ToString()), out StrError);

                //if (dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
                //{
                //    e.Row.Cells[8].Text = dss.Tables[0].Rows[0]["Closing"].ToString() + " - " + dss.Tables[1].Rows[0]["Unit"].ToString();
                //    //e.Row.Cells[4].Text = Convert.ToString(Session["CafeteriaNo"]);
                //}
                //else
                //{
                //    if (dss.Tables[1].Rows.Count > 0)
                //    {
                //        e.Row.Cells[8].Text = "0" + " - " + dss.Tables[1].Rows[0]["Unit"].ToString();
                //    }
                //    else
                //    {
                //        e.Row.Cells[8].Text = "0";
                //    }
                //    //e.Row.Cells[4].Text = Convert.ToString(Session["CafeteriaNo"]);
                //}

                //-----Bind UnitFactor TO Combobox------
                if (dss.Tables[2].Rows.Count > 0)
                {

                    ViewState["UnitConversnList"] = dss.Tables[2];
                    ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlUnitConvertor")).DataSource = dss.Tables[2];
                    ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlUnitConvertor")).DataTextField = "UnitFactor";
                    ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlUnitConvertor")).DataValueField = "#";
                    ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlUnitConvertor")).DataBind();
                    ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlUnitConvertor")).SelectedValue = (Convert.ToInt32(dttable1.Rows[e.Row.RowIndex]["UnitConvDtlsId"])).ToString();
                }
                //----Bind ITemDescription----
                if (dss.Tables[3].Rows.Count > 0)
                {
                    //((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlItemDescription")).SelectedValue = (Convert.ToInt32(dss.Tables[3].Rows[e.Row.RowIndex]["ItemDetailsId"])).ToString();

                    ViewState["ItemDesCriptionList"] = dss.Tables[3];
                    ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlItemDescription")).DataSource = dss.Tables[3];
                    ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlItemDescription")).DataTextField = "ItemDesc";
                    ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlItemDescription")).DataValueField = "ItemDetailsId";
                    ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlItemDescription")).DataBind();
                    ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlItemDescription")).SelectedValue = (Convert.ToInt32(dttable1.Rows[e.Row.RowIndex]["ItemDetailsId"])).ToString();
                    ((AjaxControlToolkit.ComboBox)e.Row.FindControl("ddlItemDescription")).Enabled = true;

                }

                if (ViewState["PriorityList"] != null)
                {
                    DataTable DtPriority = new DataTable();
                    DtPriority = (DataTable)ViewState["PriorityList"];
                    GridTemplatePriority.DataSource = DtPriority;
                    GridTemplatePriority.DataBind();
                }


                if (dss.Tables[4].Rows.Count > 0)
                {
                    e.Row.Cells[10].Text = dss.Tables[4].Rows[0]["Qty"].ToString();
                }
                else
                {
                    e.Row.Cells[10].Text = "0";
                }
            }

        }
    }

    protected void ReportGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                        Ds = Obj_RequisitionCafeteria.GetRequisitionDetailsForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                        if (Ds.Tables.Count > 0)
                        {
                            if (Ds.Tables[0].Rows.Count > 0)
                            {
                                txtReqNo.Text = Ds.Tables[0].Rows[0]["RequisitionNo"].ToString();
                                lblReqNo.Text = Ds.Tables[0].Rows[0]["RequisitionNo"].ToString();
                                txtReqDate.Text = Ds.Tables[0].Rows[0]["RequisitionDate"].ToString();
                                lblCafe.Text = Ds.Tables[0].Rows[0]["Cafeteria"].ToString();
                                txtTempDate.Text = Ds.Tables[0].Rows[0]["ExpdDate"].ToString();
                                ddlCostCentre.SelectedValue = Ds.Tables[0].Rows[0]["IsCostCentre"].ToString();
                                TXTREMARK.Text = Ds.Tables[0].Rows[0]["Remark"].ToString();
                                txtindremark.Text = Ds.Tables[0].Rows[0]["RemarkIND"].ToString();
                            }
                            if (Ds.Tables[1].Rows.Count > 0)
                            {
                                ViewState["Template"] = Ds.Tables[1];
                                ViewState["Requisition"] = Ds.Tables[1];
                                ViewState["TemplateDetails"] = Ds.Tables[1];





                                string abc = ddlCostCentre.SelectedItem.ToString();

                                if (abc == "-- Select Project--")
                                {
                                    GrdRequisition.Visible = true;
                                    gvCustomers.Visible = false;
                                    GrdRequisition.DataSource = Ds.Tables[1];
                                    GrdRequisition.DataBind();

                                }


                                else
                                {

                                    gvCustomers.Visible = true;
                                    GrdRequisition.Visible = false;
                                    gvCustomers.DataSource = db.Displaygrid("select *  from  addrowindent  where  estmateno='" + txtReqNo.Text + "' ");
                                    gvCustomers.DataBind();



                                
                                    int tmpid = Convert.ToInt32(db.getDb_Value("select  tempateid from  templateindent  where indentno='" + txtReqNo.Text + "'"));

                                    Ds = Obj_RequisitionCafeteria.GetTemplateData(Convert.ToInt32(tmpid), string.IsNullOrEmpty("1") ? 1 : Convert.ToDecimal("1"), out StrError);
                                    if (Ds.Tables.Count > 0)
                                    {
                                        if (Ds.Tables[0].Rows.Count > 0)
                                        {
                                            //txtTemplateFor.Text = Ds.Tables[0].Rows[0]["TemplateName"].ToString();
                                            // txtTemplateDate.Text = Ds.Tables[0].Rows[0]["TemplateDate"].ToString();
                                        }
                                        if (Ds.Tables[0].Rows.Count > 0)
                                        {
                                           
                                        

                                            //  ddlItem.ClearSelection();
                                            //   ddlCategory.ClearSelection();
                                        }
                                        else
                                        {
                                            //SetInitialRowGrdTemplate();
                                        }
                                    }







                                }










                                GetItemForEdit();
                                CalculateToTotal();
                                for (int t = 0; t < GrdRequisition.Rows.Count; t++)
                                {
                                    ((CheckBox)GrdRequisition.Rows[t].Cells[0].FindControl("GrdSelectAll")).Checked = true;

                                }
                                EditTemp = 0;
                                BtnUpdate.Visible = true;
                                BtnSave.Visible = false;
                            }
                            else
                            {
                                SetInitialRow_GrdRequisition();
                            }
                        }
                    }
                    break;
                case ("Delete"):
                    {
                        ViewState["DeleteID"] = Convert.ToInt32(e.CommandArgument);
                        Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(e.CommandArgument);
                        Entity_RequisitionCafeteria.UserId = Convert.ToInt32(Session["UserId"]);
                        Entity_RequisitionCafeteria.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        int DeletedRow = Obj_RequisitionCafeteria.DeleteRequisition(ref Entity_RequisitionCafeteria, out StrError);
                        if (DeletedRow != 0)
                        {
                            obj_Comman.ShowPopUpMsg("Record Deleted Successfully!", this.Page);
                            MakeEmptyForm();
                        }
                    }
                    break;
                case ("DeleteMR"):
                    {
                        ViewState["DeleteID"] = Convert.ToInt32(e.CommandArgument);
                        Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(e.CommandArgument);
                        Entity_RequisitionCafeteria.UserId = Convert.ToInt32(Session["UserId"]);
                        Entity_RequisitionCafeteria.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        int DeletedRow = Obj_RequisitionCafeteria.DeleteRequisition(ref Entity_RequisitionCafeteria, out StrError);
                        if (DeletedRow != 0)
                        {
                            obj_Comman.ShowPopUpMsg("Record Deleted Successfully!", this.Page);
                            MakeEmptyForm();
                        }
                    }
                    break;
                case ("MailIndent"):
                    {
                        TRLOADING.Visible = false;
                        ViewState["MailID"] = Convert.ToInt32(e.CommandArgument);
                        GETDATAFORMAIL(1, 1);
                        MDPopUpYesNoMail.Show();
                        BtnPopMail.Focus();
                    }
                    break;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ReportGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void ReportGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.ReportGrid.PageIndex = e.NewPageIndex;
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.FillCombo(Convert.ToInt32(Session["TransactionSiteID"].ToString()), out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[2].Rows.Count > 0)
                {
                    ReportGrid.DataSource = Ds.Tables[2];
                    this.ReportGrid.DataBind();
                    //-----For UserRights------
                    if (FlagDel && FlagEdit)
                    {
                        foreach (GridViewRow GRow in ReportGrid.Rows)
                        {
                            GRow.FindControl("ImgBtnDelete").Visible = false;
                            GRow.FindControl("ImageGridEdit").Visible = false;
                        }
                    }
                    else if (FlagDel)
                    {
                        foreach (GridViewRow GRow in ReportGrid.Rows)
                        {
                            GRow.FindControl("ImgBtnDelete").Visible = false;
                        }
                    }
                    else if (FlagEdit)
                    {
                        foreach (GridViewRow GRow in ReportGrid.Rows)
                        {
                            GRow.FindControl("ImageGridEdit").Visible = false;
                        }
                    }
                }
                Ds = null;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void ReportGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label GRDPOSTATUS = (Label)e.Row.FindControl("GRDPOSTATUS");

            if (GRDPOSTATUS.Text == "PO GENERATED")
            {
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[2].BackColor = System.Drawing.Color.SkyBlue;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[3].BackColor = System.Drawing.Color.SkyBlue;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[4].BackColor = System.Drawing.Color.SkyBlue;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[5].BackColor = System.Drawing.Color.SkyBlue;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[6].BackColor = System.Drawing.Color.SkyBlue;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[7].BackColor = System.Drawing.Color.SkyBlue;
                e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[8].BackColor = System.Drawing.Color.SkyBlue;              //LightSteelBlue;
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[9].BackColor = System.Drawing.Color.SkyBlue;
            }
            else
            {
                if ((e.Row.Cells[7].Text) == "Generated")
                {
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;
                }
                if ((e.Row.Cells[7].Text) == "Approved")
                {
                   
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Yellow;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Yellow;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Yellow;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Yellow;
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[9].BackColor = System.Drawing.Color.Yellow;
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
                }
                if ((e.Row.Cells[7].Text) == "Authorised")
                {
                  
                    e.Row.Cells[2].BackColor = System.Drawing.Color.MediumSeaGreen;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].BackColor = System.Drawing.Color.MediumSeaGreen;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[4].BackColor = System.Drawing.Color.MediumSeaGreen;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[5].BackColor = System.Drawing.Color.MediumSeaGreen;
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[6].BackColor = System.Drawing.Color.MediumSeaGreen;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[7].BackColor = System.Drawing.Color.MediumSeaGreen;
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[8].BackColor = System.Drawing.Color.MediumSeaGreen;
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[9].BackColor = System.Drawing.Color.MediumSeaGreen;
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
                }
            }

            if ((e.Row.Cells[9].Text) == "Email Sent")
            {
                e.Row.Cells[9].BackColor = System.Drawing.Color.IndianRed;
                //e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
            }

            // GridViewRow gvr=(GridViewRow)e.Row.NamingContainer;

            if ((GRDPOSTATUS.Text == "PO GENERATED"))
            {
                if (decimal.Parse(ReportGrid.DataKeys[e.Row.RowIndex].Values["POTotQty"].ToString()) < decimal.Parse(ReportGrid.DataKeys[e.Row.RowIndex].Values["IndentTotQty"].ToString()))
                {
                    e.Row.BackColor = System.Drawing.Color.Pink;
                }
            }
        }
    }

    protected void ReportGrid_DataBound(object sender, EventArgs e)
    {
        try
        {
            for (int rowIndex = ReportGrid.Rows.Count - 1; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = ReportGrid.Rows[rowIndex];
                ImageButton ImageAccepted = (ImageButton)ReportGrid.Rows[rowIndex].Cells[1].FindControl("ImageAccepted");
                ImageButton ImageGridEdit = (ImageButton)ReportGrid.Rows[rowIndex].Cells[1].FindControl("ImageGridEdit");
                bool OrderStatus = Convert.ToBoolean(ReportGrid.Rows[rowIndex].Cells[6].Text);

                ImageButton ImageApprove = (ImageButton)ReportGrid.Rows[rowIndex].Cells[1].FindControl("ImageApprove");
                ImageButton IMGDELETEMR = (ImageButton)ReportGrid.Rows[rowIndex].Cells[1].FindControl("IMGDELETEMR");
                ImageButton ImgBtnDelete = (ImageButton)ReportGrid.Rows[rowIndex].Cells[1].FindControl("ImgBtnDelete");
                Image ImagePrint = (Image)ReportGrid.Rows[rowIndex].Cells[1].FindControl("ImgBtnPrint");
                ImageButton ImageGridEditBlocked = (ImageButton)ReportGrid.Rows[rowIndex].Cells[1].FindControl("ImageGridEditBlocked");
                Label GRDPOSTATUS = (Label)ReportGrid.Rows[rowIndex].Cells[1].FindControl("GRDPOSTATUS");

                if (GRDPOSTATUS.Text == "NO PO GENERATED")
                {
                    IMGDELETEMR.Visible = true;
                }
                else
                {
                    IMGDELETEMR.Visible = false;
                }

                if (OrderStatus != false)
                {
                    ImageGridEditBlocked.Visible = true;
                }
                else
                {
                    ImageGridEditBlocked.Visible = false;
                }

                string ReqStatus = Convert.ToString(ReportGrid.Rows[rowIndex].Cells[7].Text);
                if (ReqStatus != "Generated")
                {
                    ImageAccepted.Visible = true;
                    ImageApprove.Visible = true;
                    ImageGridEdit.Visible = false;
                    ImagePrint.Visible = true;
                    ImgBtnDelete.Visible = false;
                    //ImgEmail.Visible = true;
                }
                else
                {
                    ImageAccepted.Visible = false;
                    ImageApprove.Visible = false;
                    ImageGridEdit.Visible = true;
                    ImagePrint.Visible = false;
                    ImgBtnDelete.Visible = true;
                    //ImgEmail.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void GrdRequisition_DataBound(object sender, EventArgs e)
    {
        try
        {
            for (int rowIndex = GrdRequisition.Rows.Count - 1; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = GrdRequisition.Rows[rowIndex];
                ImageButton ImageAccepted = (ImageButton)GrdRequisition.Rows[rowIndex].Cells[0].FindControl("ImgBtnBlocked");
                ImageButton ImageGridEdit = (ImageButton)GrdRequisition.Rows[rowIndex].Cells[0].FindControl("ImgBtnDelete");
                //ImageButton ImgNMail = (ImageButton)GrdRequisition.Rows[rowIndex].Cells[0].FindControl("ImgNMail");
                TextBox TXTGridEdit = (TextBox)GrdRequisition.Rows[rowIndex].Cells[0].FindControl("txtOrdQty");
                string OrderStatus = Convert.ToString(GrdRequisition.Rows[rowIndex].Cells[23].Text);//here 17 replace by 19 on 5/1/13 (IsCancel Flag)
                if (OrderStatus != "0")
                {
                    ImageAccepted.Visible = true;
                    // ImgNMail.Visible = true;
                    ImageGridEdit.Visible = false;
                    TXTGridEdit.ReadOnly = true;
                }
                else
                {
                    ImageAccepted.Visible = false;
                    //ImgNMail.Visible = false;
                    ImageGridEdit.Visible = true;
                    TXTGridEdit.ReadOnly = false;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void ChkShowProcess_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkShowProcess1 = (CheckBox)sender;
        GridViewRow row = (GridViewRow)ChkShowProcess1.NamingContainer;
        GridView GridTemplateVendor = (GridView)row.FindControl("GridTemplatePriority");
        int CurrRow = row.RowIndex;
        int Pos = 0;
        if (ChkShowProcess1.Checked == true)
        {
            if (GridTemplateVendor.Rows.Count > 0)
            {
                for (int j = 0; j < GridTemplateVendor.Rows.Count; j++)
                {
                    RadioButton RBVendor = (RadioButton)GridTemplateVendor.Rows[j].Cells[2].FindControl("RBPriority");
                    if (RBVendor.Checked == true)
                    {
                        Pos = j;
                        string VendorName = GridTemplateVendor.Rows[j].Cells[3].Text;
                        //((Label)GrdRequisition.Rows[CurrRow].Cells[10].FindControl("lblVendor")).Text = VendorName;
                        ((Label)GrdRequisition.Rows[CurrRow].Cells[17].FindControl("Priority")).Text = VendorName;
                        ((Label)GrdRequisition.Rows[CurrRow].Cells[20].FindControl("PriorityID")).Text = GridTemplateVendor.Rows[j].Cells[1].Text;//0 or 1
                    }
                }
                ((RadioButton)GridTemplateVendor.Rows[Pos].Cells[2].FindControl("RBPriority")).Checked = false;
                ChkShowProcess1.Checked = false;
            }
            else
            {
                obj_Comman.ShowPopUpMsg("There is no Vendor", this.Page);
            }
        }
        Panel PnlGrid = (Panel)row.FindControl("PnlGrid");
        PopupControlExtender popup = AjaxControlToolkit.PopupControlExtender.GetProxyForCurrentPopup(Page);
        popup.Commit("Popup");
    }

    protected void TxtItemName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = sender as TextBox;
            GridViewRow row = txt.NamingContainer as GridViewRow;
            int rowIndex = row.RowIndex;
            StrCondition = string.Empty;
            StrCondition = TxtItemName.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                TxtItemName.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                TxtItemName.ToolTip = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItem")).SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();

                ddlItem_SelectedIndexChanged((((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[rowIndex].FindControl("ddlItem"))) as AjaxControlToolkit.ComboBox, EventArgs.Empty);
                Ds = null;
            }
            else
            {
                TxtItemName.Text = "";
                TxtItemName.ToolTip = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void PopUpYesNoMail_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "yes")
        {
            SendMail(Convert.ToInt32(LBLID.Text));

        }
        else
        {

        }

    }

    protected void DDLKCMPY_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(DDLKCMPY.SelectedValue.ToString()) > 0)
            {
                //GETCLIENTPDF(Convert.ToInt32(LBLID.Text.ToString()), Convert.ToInt32(DDLKCMPY.SelectedValue.ToString()));
                MDPopUpYesNoMail.Show();
                BtnPopMail.Focus();
            }
        }
        catch (Exception ex) { }
        finally { }
    }

    protected void TXTKTO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TXTKTO.Text = TOSTRING + TXTKTO.Text.ToString();
            TXTKTO.Text = TXTKTO.Text.ToString().Replace(",,", ",");
            MDPopUpYesNoMail.Show();
            BtnPopMail.Focus();
        }
        catch (Exception ex)
        {
        }
    }
    
    protected void GrdSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox GrdSelectAllHeader1 = (CheckBox)sender;
        GridViewRow row = (GridViewRow)GrdSelectAllHeader1.NamingContainer;
        int CurrRow = row.RowIndex;
        if (GrdSelectAllHeader1.Checked == true)
        {
            for (int j = 0; j < GrdRequisition.Rows.Count; j++)
            {
                ComboBox GrdSelectAll = ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[j].FindControl("ddlItemDescription"));
                Int32 ItemDtlsId = Convert.ToInt32(GrdSelectAll.SelectedValue);
                int ItemId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[j].FindControl("ddlItem")).SelectedValue.ToString());


                if (Convert.ToInt32(GrdSelectAll.SelectedValue) > 0)
                {
                    DataSet DSUNIT = new DataSet();
                    DSUNIT = Obj_RequisitionCafeteria.GetAvlQtyOnItemDtlsId(ItemId, ItemDtlsId, out StrError);

                    for (int i = 0; i < DSUNIT.Tables[0].Rows.Count; i++)
                    {
                        GrdRequisition.Rows[j].Cells[8].Text = DSUNIT.Tables[0].Rows[0]["OpeningStock"].ToString();
                    }

                }
                if (Convert.ToInt32(GrdSelectAll.SelectedValue) == 0)
                {
                    DataSet DSUNIT = new DataSet();
                    DSUNIT = Obj_RequisitionCafeteria.GetAvlQtyOnItemDtlsId(ItemId, ItemDtlsId, out StrError);

                    for (int i = 0; i < DSUNIT.Tables[0].Rows.Count; i++)
                    {


                        GrdRequisition.Rows[j].Cells[8].Text = DSUNIT.Tables[0].Rows[0]["OpeningStock"].ToString();
                    }

                }

            }
        }
        else
        {
            //for (int j = 0; j < GrdReqPO.Rows.Count; j++)
            //{
            //    CheckBox GrdSelectAll = (CheckBox)GrdReqPO.Rows[j].Cells[1].FindControl("GrdSelectAll");
            //    GrdSelectAll.Checked = false;
            //}
        }

    }

    protected void BtnShowAvQty_Click(object sender, EventArgs e)
    {
        try
        {

            Button GrdSelectAllHeader1 = (Button)sender;
            GridViewRow row = (GridViewRow)GrdSelectAllHeader1.NamingContainer;
            int CurrRow = row.RowIndex;

            //for (int j = 0; j < GrdRequisition.Rows.Count; j++)
            //{
            ComboBox GrdSelectAll = ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[CurrRow].FindControl("ddlItemDescription"));
            Int32 ItemDtlsId = Convert.ToInt32(GrdSelectAll.SelectedValue);
            int ItemId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[CurrRow].FindControl("ddlItem")).SelectedValue.ToString());


            if (Convert.ToInt32(GrdSelectAll.SelectedValue) > 0)
            {
                DataSet DSUNIT = new DataSet();
                DSUNIT = Obj_RequisitionCafeteria.GetAvlQtyOnItemDtlsId(ItemId, ItemDtlsId, out StrError);

                for (int i = 0; i < DSUNIT.Tables[0].Rows.Count; i++)
                {
                    GrdRequisition.Rows[CurrRow].Cells[8].Text = DSUNIT.Tables[0].Rows[0]["OpeningStock"].ToString();
                }

            }
            if (Convert.ToInt32(GrdSelectAll.SelectedValue) == 0)
            {
                DataSet DSUNIT = new DataSet();
                DSUNIT = Obj_RequisitionCafeteria.GetAvlQtyOnItemDtlsId(ItemId, ItemDtlsId, out StrError);

                for (int i = 0; i < DSUNIT.Tables[0].Rows.Count; i++)
                {


                    GrdRequisition.Rows[CurrRow].Cells[8].Text = DSUNIT.Tables[0].Rows[0]["OpeningStock"].ToString();
                }

            }

            //  }

        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnShowAllAvQty_Click(object sender, EventArgs e)
    {
        try
        {

            Button GrdSelectAllHeader1 = (Button)sender;
            GridViewRow row = (GridViewRow)GrdSelectAllHeader1.NamingContainer;
            int CurrRow = row.RowIndex;

            for (int j = 0; j < GrdRequisition.Rows.Count; j++)
            {
                ComboBox GrdSelectAll = ((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[j].FindControl("ddlItemDescription"));
                Int32 ItemDtlsId = Convert.ToInt32(GrdSelectAll.SelectedValue);
                int ItemId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[j].FindControl("ddlItem")).SelectedValue.ToString());


                if (Convert.ToInt32(GrdSelectAll.SelectedValue) > 0)
                {
                    DataSet DSUNIT = new DataSet();
                    DSUNIT = Obj_RequisitionCafeteria.GetAvlQtyOnItemDtlsId(ItemId, ItemDtlsId, out StrError);

                    for (int i = 0; i < DSUNIT.Tables[0].Rows.Count; i++)
                    {
                        GrdRequisition.Rows[j].Cells[8].Text = DSUNIT.Tables[0].Rows[0]["OpeningStock"].ToString();
                    }

                }
                if (Convert.ToInt32(GrdSelectAll.SelectedValue) == 0)
                {
                    DataSet DSUNIT = new DataSet();
                    DSUNIT = Obj_RequisitionCafeteria.GetAvlQtyOnItemDtlsId(ItemId, ItemDtlsId, out StrError);

                    for (int i = 0; i < DSUNIT.Tables[0].Rows.Count; i++)
                    {


                        GrdRequisition.Rows[j].Cells[8].Text = DSUNIT.Tables[0].Rows[0]["OpeningStock"].ToString();
                    }

                }

            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnSave_Click1(object sender, EventArgs e)
    {
        string abc = ddlCostCentre.SelectedItem.ToString();

        if (abc == "-- Select Project--")
        {
            int UnitConvDtlsIdT = 0, ItemId = 0; decimal Qty = 0;
            DataSet Ds = new DataSet();
            try
            {
                FindDataInGrid();
                if (fillgrid == true)
                {
                    CheckDuplicate();
                    CheckOrderAmount_New();
                    if (valueofitem == 2)
                    {
                        if (clear == true)
                        {
                            CheckTextBox();
                            if (flag1 == true)
                            {
                                Entity_RequisitionCafeteria.RequisitionNo = "" + txtReqNo.Text;
                                string Reqno = "" + txtReqNo.Text;

                                Ds = Obj_RequisitionCafeteria.CkeckDuplicateSaveTime(Reqno, out StrError);
                                if (Ds.Tables[0].Rows.Count > 0)
                                {
                                    obj_Comman.ShowPopUpMsg("Duplicate Indent !", this.Page);
                                    MakeEmptyForm();
                                    return;
                                }
                                Entity_RequisitionCafeteria.RequisitionDate = !string.IsNullOrEmpty(txtReqDate.Text) ? Convert.ToDateTime(txtReqDate.Text.Trim()) : Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                Entity_RequisitionCafeteria.CafeteriaId = Convert.ToInt32(Session["TransactionSiteID"].ToString());//CafeteriaNo
                                Entity_RequisitionCafeteria.UserId = Convert.ToInt32(Session["UserId"]);
                                Entity_RequisitionCafeteria.IsCostCentre = Convert.ToInt32(ddlCostCentre.SelectedValue);
                                Entity_RequisitionCafeteria.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                Entity_RequisitionCafeteria.Remark = TXTREMARK.Text;
                                Entity_RequisitionCafeteria.RemarkIND = txtindremark.Text;
                                insertrow = Obj_RequisitionCafeteria.InsertRequisitionDetails(ref Entity_RequisitionCafeteria, out StrError);



                             


                                int MaxID = insertrow;
                                int insertdtls = 0;
                                if (insertrow != 0)
                                {
                                    for (int g = 0; g < GrdRequisition.Rows.Count; g++)
                                    {
                                        if (Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text) > 0)
                                        {
                                            Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                            string GetString = (GrdRequisition.Rows[g].Cells[8].Text.ToString());
                                            if (GetString[0].Equals('-'))
                                            {
                                                GetString = GetString.Remove(0, 1);
                                            }
                                            string[] qw1 = GetString.Split('-');
                                            Entity_RequisitionCafeteria.AvlQty = Convert.ToDecimal(qw1[0].ToString());
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text);
                                            Entity_RequisitionCafeteria.VendorId = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("lblVendorId")).Text);
                                            Entity_RequisitionCafeteria.Rate = Convert.ToDecimal(GrdRequisition.Rows[g].Cells[16].Text);
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(GrdRequisition.Rows[g].Cells[10].Text);
                                            Entity_RequisitionCafeteria.MinStockLevel = GrdRequisition.Rows[g].Cells[11].Text;
                                            Entity_RequisitionCafeteria.MaxStockLevel = GrdRequisition.Rows[g].Cells[12].Text;


                                            TextBox txtToDate = (TextBox)GrdRequisition.Rows[g].Cells[29].FindControl("txtRequiredDate");

                                            if (string.IsNullOrEmpty(txtToDate.Text))
                                            {
                                                Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                            }
                                            else
                                            {
                                                Entity_RequisitionCafeteria.RequiredDate = Convert.ToDateTime(txtToDate.Text);
                                            }


                                            //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                            Entity_RequisitionCafeteria.TemplateID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("LblEntryId")).Text);
                                            Entity_RequisitionCafeteria.ItemId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlItem")).SelectedValue);
                                            Entity_RequisitionCafeteria.ExpdDate = !string.IsNullOrEmpty(txtTempDate.Text) ? Convert.ToDateTime(txtTempDate.Text) : Convert.ToDateTime(DateTime.Now.AddDays(3));
                                            Entity_RequisitionCafeteria.IsCancel = Convert.ToBoolean(Convert.ToInt32(GrdRequisition.Rows[g].Cells[23].Text));// here 17 replace with 19
                                                                                                                                                             // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                                                                                                                                             //--Newly Added Field-- 
                                            Entity_RequisitionCafeteria.ItemDetailsId = !String.IsNullOrEmpty(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlItemDescription")).SelectedValue) ? Convert.ToInt32((((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlItemDescription")).SelectedValue)) : 0;
                                            Entity_RequisitionCafeteria.UnitConvDtlsId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlUnitConvertor")).SelectedValue);
                                            Entity_RequisitionCafeteria.RemarkForPO = (((TextBox)GrdRequisition.Rows[g].FindControl("GrdRemark")).Text);

                                            #region[Convert Quantity accordng to UnitFactor]
                                            //---Coversionfactor---
                                            UnitConvDtlsIdT = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlUnitConvertor")).SelectedValue);
                                            ItemId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlItem")).SelectedValue);//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                            string unitconvrt = (((AjaxControlToolkit.ComboBox)GrdRequisition.Rows[g].FindControl("ddlUnitConvertor")).SelectedItem).ToString();
                                            DataSet DsTemp = new DataSet();
                                            DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                            if (DsTemp.Tables.Count > 0)
                                            {
                                                for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                {
                                                    if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                    }
                                                    else
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)GrdRequisition.Rows[g].FindControl("txtOrdQty")).Text));
                                                    }
                                                }
                                            }

                                            Entity_RequisitionCafeteria.Qty = Qty;
                                            #endregion
                                            insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                        }
                                    }







                                }
                                if (insertdtls != 0)
                                {
                                    obj_Comman.ShowPopUpMsg("Record Saved Successfully!", this.Page);
                                    //SendMail(insertrow);
                                    MakeEmptyForm();
                                    GrdRequisition.Visible = true;
                                }
                                else
                                {
                                    obj_Comman.ShowPopUpMsg(StrError, this.Page);
                                }
                            }
                            else
                            {
                                obj_Comman.ShowPopUpMsg("Please Enter The Order Quantity", this.Page);
                            }
                        }
                        else
                        {
                            obj_Comman.ShowPopUpMsg("Name Already Present..!", this.Page);
                            MakeEmptyForm();
                        }
                    }
                    else
                    {
                        obj_Comman.ShowPopUpMsg("Please Enter Order Quantity", this.Page);
                    }
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("Please Selet Atleast 1 Item", this.Page);
                }
            }
            catch (Exception ex)
            {

            }

        }

        else
        {


            string ps = "IND" + "-"+ddlCostCentre.SelectedValue;
            db.insert("insert into  poprint values('" + ps + "' ,'" + ddlCostCentre.SelectedValue + "')  ");















            int UnitConvDtlsIdT = 0, ItemId = 0; decimal Qty = 0;
            DataSet Ds = new DataSet();
            try
            {
                FindDataInGrid();
                if (fillgrid == true)
                {
                    CheckDuplicate();
                    // CheckOrderAmount_New();
                    if (valueofitem == 0)
                    {
                        if (clear == true)
                        {
                            CheckTextBox();
                            if (flag1 == false)
                            {
                                Entity_RequisitionCafeteria.RequisitionNo = "" + txtReqNo.Text;
                                string Reqno = "" + txtReqNo.Text;

                                Ds = Obj_RequisitionCafeteria.CkeckDuplicateSaveTime(Reqno, out StrError);
                                if (Ds.Tables[0].Rows.Count > 0)
                                {
                                    obj_Comman.ShowPopUpMsg("Duplicate Indent !", this.Page);
                                    MakeEmptyForm();
                                    return;
                                }
                                Entity_RequisitionCafeteria.RequisitionDate = !string.IsNullOrEmpty(txtReqDate.Text) ? Convert.ToDateTime(txtReqDate.Text.Trim()) : Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                Entity_RequisitionCafeteria.CafeteriaId = Convert.ToInt32(Session["TransactionSiteID"].ToString());//CafeteriaNo
                                Entity_RequisitionCafeteria.UserId = Convert.ToInt32(Session["UserId"]);
                                Entity_RequisitionCafeteria.IsCostCentre = Convert.ToInt32(ddlCostCentre.SelectedValue);
                                Entity_RequisitionCafeteria.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                Entity_RequisitionCafeteria.Remark = TXTREMARK.Text;
                                Entity_RequisitionCafeteria.RemarkIND = txtindremark.Text;
                                insertrow = Obj_RequisitionCafeteria.InsertRequisitionDetails(ref Entity_RequisitionCafeteria, out StrError);
                                db.insert("update RequisitionCafeteria set  revised='" + "0"+ "' ,  status='"+"1"+"' where  RequisitionNo='" + txtReqNo.Text + "'");
                                int MaxID = insertrow;
                                int insertdtls = 0;
                                if (insertrow != 0)
                                {




                                    for (int i = 0; i < this.gvCustomers.Rows.Count; i++)
                                    {

                                        string temaplatename = gvCustomers.Rows[i].Cells[1].Text;

                                        string qty = gvCustomers.Rows[i].Cells[2].Text;
                                        GridView inner = this.gvCustomers.Rows[i].FindControl("gvOrders") as GridView;






                                        foreach (GridViewRow gvrow in inner.Rows)
                                        {


                                            CheckBox chkRow = (gvrow.FindControl("chkbox") as CheckBox);
                                            if (chkRow.Checked)
                                            {
                                                Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                                Label TXTITEMNAME = gvrow.FindControl("TXTITEMNAME") as Label;



                                                string GetString = (gvrow.Cells[8].Text.ToString());
                                                if (GetString == "&nbsp;")
                                                {
                                                    Entity_RequisitionCafeteria.AvlQty = Convert.ToDecimal(1);
                                                }
                                                else
                                                {

                                                    if (GetString[0].Equals('-'))
                                                    {
                                                        GetString = GetString.Remove(0, 1);
                                                    }


                                                    string[] qw1 = GetString.Split('-');
                                                    Entity_RequisitionCafeteria.AvlQty = Convert.ToDecimal(qw1[0].ToString());
                                                 
                                                }
                                                Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)gvrow.FindControl("txtorderqty")).Text);
                                                Entity_RequisitionCafeteria.VendorId = Convert.ToInt32(((Label)gvrow.FindControl("lblVendorId")).Text);
                                                Entity_RequisitionCafeteria.Rate = Convert.ToDecimal(((Label)gvrow.FindControl("lblrate")).Text);
                                                Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(gvrow.Cells[9].Text);
                                                Entity_RequisitionCafeteria.MinStockLevel = gvrow.Cells[10].Text;
                                                Entity_RequisitionCafeteria.MaxStockLevel = gvrow.Cells[11].Text;
                                                Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                                Entity_RequisitionCafeteria.TemplateID = Convert.ToInt32(((Label)gvrow.FindControl("LblEntryId")).Text);
                                                Entity_RequisitionCafeteria.ItemId = Convert.ToInt32(((Label)gvrow.FindControl("lblitem")).Text);

                                                Entity_RequisitionCafeteria.ExpdDate = !string.IsNullOrEmpty(txtTempDate.Text) ? Convert.ToDateTime(txtTempDate.Text) : Convert.ToDateTime(DateTime.Now.AddDays(3));
                                                Entity_RequisitionCafeteria.IsCancel = false;
                                                Entity_RequisitionCafeteria.ItemDetailsId = !String.IsNullOrEmpty(((Label)gvrow.FindControl("lblItemDetailsId")).Text) ? Convert.ToInt32((((Label)gvrow.FindControl("lblItemDetailsId")).Text)) : 0;
                                                Entity_RequisitionCafeteria.UnitConvDtlsId = Convert.ToInt32(((AjaxControlToolkit.ComboBox)gvrow.FindControl("ddlUnitConvertor")).SelectedValue);
                                                Entity_RequisitionCafeteria.RemarkForPO = (((TextBox)gvrow.FindControl("GrdRemark")).Text);
                                                #region[Convert Quantity accordng to UnitFactor]
                                                //---Coversionfactor---
                                                UnitConvDtlsIdT = Convert.ToInt32(((AjaxControlToolkit.ComboBox)gvrow.FindControl("ddlUnitConvertor")).SelectedValue);
                                                ItemId = Convert.ToInt32(((Label)gvrow.FindControl("lblitem")).Text);//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                                string unitconvrt = Convert.ToInt32(((AjaxControlToolkit.ComboBox)gvrow.FindControl("ddlUnitConvertor")).SelectedValue).ToString();
                                                DataSet DsTemp = new DataSet();
                                                DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                                if (DsTemp.Tables.Count > 0)
                                                {
                                                    for (int j = 0; j < DsTemp.Tables[0].Rows.Count; j++)
                                                    {
                                                        if (unitconvrt.Equals(DsTemp.Tables[0].Rows[j]["Unit"].ToString()))
                                                        {
                                                            Qty = (Convert.ToDecimal(((TextBox)gvrow.FindControl("txtorderqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[j]["Factor"].ToString());
                                                        }
                                                        else
                                                        {
                                                            Qty = (Convert.ToDecimal(((TextBox)gvrow.FindControl("txtorderqty")).Text));
                                                        }
                                                    }
                                                }

                                                Entity_RequisitionCafeteria.Qty = Qty;
                                                #endregion
                                                insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                            }
                                        }
                                    }










                           



                                }
                                if (insertdtls != 0)
                                {
                                    obj_Comman.ShowPopUpMsg("Record Saved Successfully!", this.Page);
                                    //SendMail(insertrow);
                                    MakeEmptyForm();
                                    GrdRequisition.Visible = true;
                                }
                                else
                                {
                                    obj_Comman.ShowPopUpMsg(StrError, this.Page);
                                }
                            }
                            else
                            {
                                obj_Comman.ShowPopUpMsg("Please Enter The Order Quantity", this.Page);
                            }
                        }
                        else
                        {
                            obj_Comman.ShowPopUpMsg("Name Already Present..!", this.Page);
                            MakeEmptyForm();
                        }
                    }
                    else
                    {
                        obj_Comman.ShowPopUpMsg("Please Enter Order Quantity", this.Page);
                    }
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("Please Selet Atleast 1 Item", this.Page);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void ddlCostCentre_SelectedIndexChanged(object sender, EventArgs e)
    {

       

        string abc = ddlCostCentre.SelectedItem.ToString();

        if (abc == "-- Select Project--")
        {
            SetInitialRow_GrdRequisition();
            GrdRequisition.Visible = true;

        }


        else
        {
            GrdRequisition.Visible = false;
        }

     //   int projectnoid = db.getDbstatus_Value("select projectno from Project_master where id='" + ddlCostCentre.SelectedValue + "' ");
        string projectno = db.getDbstatus_Value("select projectno from Project_master where id='" + ddlCostCentre.SelectedValue + "' ");
        string ps = "IND";
        string year = "19-20";
        int unicno = Convert.ToInt32(db.getDb_Value("select count(RequisitionCafeId) from RequisitionCafeteria where IsCostCentre= '" + ddlCostCentre.SelectedValue + "' and  IsDeleted='" + "False" + "'"));
        if (unicno == 0)
        {
            unicno = 1;
        }
        else
        {
            unicno++;
        }
        string finalprojectno = ps + "- " + " " + projectno + "/" + unicno;
        lblReqNo.Text = finalprojectno;
        txtReqNo.Text = finalprojectno;
        ViewState["finalprojectno"] = finalprojectno;



        //string project = db.getDbstatus_Value("select pno from Projectscope where project='" + ddlCostCentre.SelectedValue + "'");

        //drpvalve.DataSource = db.Displaygrid("select    (  CAST(ROW_NUMBER() OVER (ORDER BY id) AS VARCHAR)  +'-'+ valvetype +  '-'   + valvesize  +  '-' +  valveclass +'-'+ interlock) as valve , valvetype from    projectscopedetails where  pno='" + project + "' ");
        //drpvalve.DataValueField = "valve";
        //drpvalve.DataTextField = "valve";
        //drpvalve.DataBind();
        //drpvalve.Items.Insert(0, "Select valve");


        db.insert("delete   addrowindent where  estmateno='" + txtReqNo.Text + "' ");


        string project = db.getDbstatus_Value("select pno from Projectscope where project='" + ddlCostCentre.SelectedValue + "' order by id desc");

        SqlCommand cmd = new SqlCommand("   select  projectscopedetails.interlock ,  sum( projectscopedetails.qty) as qty from  projectscopedetails     where pno='" + project + "'  group by interlock ,qty ");
        cmd.Connection = cn;

        cn.Open();
        SqlDataReader rdr = cmd.ExecuteReader();


        while (rdr.Read())
        {
            string templateid = (rdr["interlock"]).ToString();
            int qty = Convert.ToInt32(rdr["qty"]);
            Session["qty"] = qty.ToString();

            SqlCommand cmd1 = new SqlCommand("  Select TemplateName, TemplateID   from TemplateMaster Where IsDeleted=0  and TemplateName='" + templateid + "'", cn);
            SqlDataAdapter adp1 = new SqlDataAdapter();
            DataSet ds1 = new DataSet();
            adp1.SelectCommand = cmd1;
            adp1.Fill(ds1);




            string a = ViewState["finalprojectno"].ToString();

            db.insert("insert into addrowindent values('" + a + "' ,'" + ds1.Tables[0].Rows[0]["TemplateName"].ToString() + "' ,'" + qty + "' ,'" + ds1.Tables[0].Rows[0]["TemplateID"].ToString() + "')");


        }



        //   int templateid = Convert.ToInt32(db.getDb_Value("select interlock from  projectscopedetails inner join  Projectscope on Projectscope.pno=projectscopedetails.pno  where Projectscope.project='"+ddlCostCentre.SelectedValue+"'"));
        //  int qty = Convert.ToInt32(db.getDb_Value("select qty from  projectscopedetails inner join  Projectscope on Projectscope.pno=projectscopedetails.pno  where Projectscope.project='"+ddlCostCentre.SelectedValue+"'"));

        //   


        gvCustomers.DataSource = db.Displaygrid("select *  from  addrowindent  where  estmateno='" + ViewState["finalprojectno"].ToString() + "' ");
        gvCustomers.DataBind();


























    }
    
    protected void Show_Hide_OrdersGrid(object sender, EventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlOrders").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            string customerId = gvCustomers.DataKeys[row.RowIndex].Value.ToString();
            GridView gvOrders = row.FindControl("gvOrders") as GridView;







            BindOrders(gvOrders);
            // bindtotak();
        }
        else
        {
            row.FindControl("pnlOrders").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";
        }
    }
    private void BindOrders(GridView gvOrders)
    {


    }
    protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }


    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string customerId = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString();
            string qty = gvCustomers.DataKeys[e.Row.RowIndex]["qty"].ToString();

            GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;


            Ds = new DataSet();
            TextBox tempqty = new TextBox();


            if (qty!=null || qty!=null)
            {
                tempqty.Text = qty;
            }
            else
            {
                tempqty.Text = "1";
            }
         
            Session["customerId"] = customerId.ToString();
            Ds = Obj_RequisitionCafeteria.GetTemplateData(Convert.ToInt32(customerId), string.IsNullOrEmpty(tempqty.Text) ? 1 : Convert.ToDecimal(tempqty.Text), out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                gvOrders.DataSource = Ds;
                gvOrders.DataBind();


            }


            //gvOrders.DataSource = GetData(string.Format("select distinct TD.ItemID as [#],IM.ItemName,ISNULL(ID.ItemDesc,'') as ItemDesc  ,ID.OpeningStock as AvlQty ,TD.Qty ,PurchaseOrdDtls.Rate from TemplateMaster TM INNER JOIN TemplateDetails TD ON TM.TemplateID=TD.TemplateID LEFT JOIN ItemMaster IM ON TD.ItemID=IM.ItemId LEFT JOIN ItemDetails ID ON TD.ItemID=ID.ItemId AND TD.ItemDtlsID=ID.ItemDetailsId left join  PurchaseOrdDtls on TD.ItemID=PurchaseOrdDtls.ItemId    where TM.IsDeleted=0 and TM.TemplateID='" + customerId + "' Order By IM.ItemName "));
            //gvOrders.DataBind();





        }
    }


    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {


        for (int i = 0; i < this.gvCustomers.Rows.Count; i++)
        {



            string qty = gvCustomers.Rows[i].Cells[2].Text;
            GridView inner = this.gvCustomers.Rows[i].FindControl("gvOrders") as GridView;
            CheckBox chkcheck1 = inner.HeaderRow.Cells[0].FindControl("chkboxSelectAll") as CheckBox;
            foreach (GridViewRow row in inner.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("chkbox");


                if (chkcheck1.Checked == true)
                {
                    chkcheck.Checked = true;
                }
                else
                {
                    chkcheck.Checked = false;
                }
            }
        }
        //  bindtotak();

    }

    protected void chkbox_CheckedChanged(object sender, EventArgs e)
    {
        //  bindtotak();
    }

    private static DataTable GetData(string query)
    {
        string constr = ConfigurationManager.ConnectionStrings["MayurInventory"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    protected void drpcatrgory_SelectedIndexChanged(object sender, EventArgs e)
    {



        string catid = drpcatrgory.SelectedValue;
        string prefix = db.getDbstatus_Value("select Prefix from ItemCategory where CategoryId='" + drpcatrgory.SelectedValue + "' ");
        string ps = "IND";
        string year = "19-20";
        int unicno = Convert.ToInt32(db.getDb_Value("select max(RequisitionCafeId) from RequisitionCafeteria "));
        if (unicno == 0)
        {
            unicno = 1;
        }
        else
        {
            unicno++;
        }
        string finalprojectno = ps + "- " + catid + "/" + prefix + "/" + unicno;
        lblReqNo.Text = finalprojectno;
        txtReqNo.Text = finalprojectno;




        GrdRequisition.Visible = true;
        gvCustomers.Visible = false;
        Session["categoryid"] = drpcatrgory.SelectedValue.ToString();

        Session["templateid"] = "";
        Ds = db.dgv_display("select ItemCategory.CategoryId as [#] , '0' as ItemToolTip, ItemMaster.ItemId ,ItemMaster.SubcategoryId  ,ItemMaster.CategoryId, ItemCategory.CategoryName ,SubCategory.SubCategory , ItemMaster.ItemCode     ,ItemName + ' => ' + isnull(ItemMaster.ItemRemark,' ') + ' => ' + isnull(SubCategory.Remark,' ') as ItemName, '' as Location,ItemDetails.OpeningStock as AvlQty,  ItemDetails.OpeningStock as txtAvlQty ,  0 as TransitQty, ItemMaster.MinStockLevel,ItemMaster.MaxStockLevel, ItemMaster.DeliveryPeriod,ISNULL(ItemDetails.ItemDetailsId,0)as ItemDetailsId,0 as UnitConvDtlsId,  ItemDetails.PurchaseRate   as AvgRate ,ItemMaster.AsOn as AvgRateDate , 'Demo' as Vendor, '1' as VendorId, ItemDetails.PurchaseRate as Rate ,'abc' as Priority,'0' as IsCancel, '1' as PriorityID , ItemDetails.ToQty as txtOrdQty  , '0'  as RequiredDate ,'0' as Remark , ItemDetails.DrawingPath from ItemMaster inner join  ItemCategory on ItemCategory.CategoryId=ItemMaster.CategoryId   inner join SubCategory on SubCategory.SubCategoryId=ItemMaster.SubcategoryId inner join ItemDetails on ItemDetails.ItemId=ItemMaster.ItemId     where ItemCategory.CategoryId='" + drpcatrgory.SelectedValue + "' ");
        // GrdRequisition.DataSource = db.Displaygrid("select ItemCategory.CategoryId as [#] , '0' as ItemToolTip, ItemMaster.ItemId ,ItemMaster.SubcategoryId  ,ItemMaster.CategoryId, ItemCategory.CategoryName ,SubCategory.SubCategory , ItemMaster.ItemCode     ,ItemName + ' => ' + isnull(ItemMaster.ItemRemark,' ') + ' => ' + isnull(SubCategory.Remark,' ') as ItemName, '' as Location,ItemDetails.OpeningStock as AvlQty,  ItemDetails.OpeningStock as txtAvlQty ,  0 as TransitQty, ItemMaster.MinStockLevel,ItemMaster.MaxStockLevel, ItemMaster.DeliveryPeriod,ISNULL(ItemDetails.ItemDetailsId,0)as ItemDetailsId,0 as UnitConvDtlsId,  ItemDetails.PurchaseRate   as AvgRate ,ItemMaster.AsOn as AvgRateDate , 'Demo' as Vendor, '1' as VendorId, ItemDetails.PurchaseRate as Rate ,'abc' as Priority,'0' as IsCancel, '1' as PriorityID , ItemDetails.ToQty as txtOrdQty  , '0'  as RequiredDate ,'0' as Remark from ItemMaster inner join  ItemCategory on ItemCategory.CategoryId=ItemMaster.CategoryId   inner join SubCategory on SubCategory.SubCategoryId=ItemMaster.SubcategoryId inner join ItemDetails on ItemDetails.ItemId=ItemMaster.ItemId     where ItemCategory.CategoryId='" + drpcatrgory.SelectedValue + "' ");
        // GrdRequisition.DataBind();

        if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
        {
            BindItemToGridAll(Ds);

            // GrdSelectAll_CheckedChanged(sender, e);
            lblTotalAmt.Text = "0.00";
            ((TextBox)GrdRequisition.Rows[0].FindControl("txtOrdQty")).Focus();
        }
        else
        {
            SetInitialRow_GrdRequisition();
        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = (CheckBox)e.Row.FindControl("chkbox");
            Label lblItemDetailsId=(Label)e.Row.FindControl("lblItemDetailsId");
            int poid = Convert.ToInt32(db.getDb_Value("select  RequisitionCafeId from  RequisitionCafeteria where  RequisitionNo='" + txtReqNo.Text + "'"));
            int count = Convert.ToInt32(db.getDb_Value("select  count(ItemDetailsId) from  RequisitionCafeDtls where  RequisitionCafeId='" + poid + "' and ItemDetailsId='"+ lblItemDetailsId.Text + "'"));
            if (count >= 1 )
            {
                chk.Checked = true;
            }
        }

    }

    protected void btniupload_Click(object sender, EventArgs e)
    {

    }


    protected void drpvalve_SelectedIndexChanged(object sender, EventArgs e)
    {



        string project = db.getDbstatus_Value("select pno from Projectscope where project='" + ddlCostCentre.SelectedValue + "'");
        string drp = drpvalve.SelectedItem.ToString();
        // string[] ssize = drp.Split(new char[0]);

        string[] tokens = drp.Split('-');
        SqlCommand cmd = new SqlCommand("   select  top 1 projectscopedetails.interlock ,  projectscopedetails.qty from  projectscopedetails     where valvetype='" + tokens[1].ToString() + "' and valvesize='"+tokens[2]+ "' and  valveclass='"+tokens[3].ToString()+ "' and interlock='"+ tokens[4] + "' and pno='"+project+"' ");
        cmd.Connection = cn;

        cn.Open();
        SqlDataReader rdr = cmd.ExecuteReader();

       
        while (rdr.Read())
        {
            string templateid = (rdr["interlock"]).ToString();
            int qty = Convert.ToInt32(rdr["qty"]);
            Session["qty"] = qty.ToString();

            SqlCommand cmd1 = new SqlCommand("  Select TemplateName, TemplateID   from TemplateMaster Where IsDeleted=0  and TemplateName='" + templateid + "'", cn);
            SqlDataAdapter adp1 = new SqlDataAdapter();
            DataSet ds1 = new DataSet();
            adp1.SelectCommand = cmd1;
            adp1.Fill(ds1);




            string a = ViewState["finalprojectno"].ToString();

            db.insert("insert into addrowindent values('" +a + "' ,'" + ds1.Tables[0].Rows[0]["TemplateName"].ToString() + "' ,'" + qty + "' ,'" + ds1.Tables[0].Rows[0]["TemplateID"].ToString() + "')");


        }



        //   int templateid = Convert.ToInt32(db.getDb_Value("select interlock from  projectscopedetails inner join  Projectscope on Projectscope.pno=projectscopedetails.pno  where Projectscope.project='"+ddlCostCentre.SelectedValue+"'"));
        //  int qty = Convert.ToInt32(db.getDb_Value("select qty from  projectscopedetails inner join  Projectscope on Projectscope.pno=projectscopedetails.pno  where Projectscope.project='"+ddlCostCentre.SelectedValue+"'"));

        //   


        gvCustomers.DataSource = db.Displaygrid("select *  from  addrowindent  where  estmateno='" + ViewState["finalprojectno"].ToString() + "' ");
        gvCustomers.DataBind();
    }

    protected void gvOrders_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void imgOrdersShow_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlOrders").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            string customerId = ReportGrid.DataKeys[row.RowIndex].Value.ToString();
            GridView gvOrders = row.FindControl("gvOrders") as GridView;


            string pno = db.getDbstatus_Value("select IsCostCentre from RequisitionCafeteria where RequisitionCafeId='" + customerId + "'");




            BindOrders(gvOrders, pno);

        }
        else
        {
            row.FindControl("pnlOrders").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";
        }
    }


    private void BindOrders(GridView gvOrders, string pno)
    {
        gvOrders.DataSource = db.Displaygrid("select distinct RC.RequisitionCafeId ,RC.RequisitionNo  , RC.ReqStatus                   from RequisitionCafeteria RC  inner join RequisitionCafeDtls RCD on RC.RequisitionCafeId=RCD.RequisitionCafeId              where RC.IsCostCentre='" + pno + "'  and   RC.IsDeleted=0  and RCD.TemplateID!=0   ");
        gvOrders.DataBind();

    }
}


