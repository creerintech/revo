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


public partial class Transactions_nonstdindentaspx : System.Web.UI.Page
{
    #region [private variables]
    bool flaggrditam = false;


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
        // TXTTEMPLATEMULTIPLY.Text = "1";
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
            Ds = Obj_RequisitionCafeteria.FillCombo1(Convert.ToInt32(Session["TransactionSiteID"].ToString()), out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    //ddlTemplate.DataSource = Ds.Tables[0];
                    //ddlTemplate.DataTextField = "Title";
                    //ddlTemplate.DataValueField = "#";
                    //ddlTemplate.DataBind();

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
            for (int i = 0; i < grditem.Rows.Count; i++)
            {
                DataTable dtq = (DataTable)ViewState["Requisition"];
                TextBox tr = (TextBox)grditem.Rows[i].FindControl("txtbqty");
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
                        valueofitem = 2;
                    }
                }
            }

            valueofitem = 2;
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
        for (int f = 0; f < grditem.Rows.Count; f++)
        {

            if (((TextBox)grditem.Rows[f].FindControl("txtbqty")).Text == "")
            {
                flag1 = false;
            }
            else
            {
                flag1 = true;
            }

        }
        flag1 = true;
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
        if (!IsPostBack)
        {
            // CheckUserRight();
            MakeEmptyForm();

            TXTKTO.DataSource = db.Displaygrid("select Email from SuplierMaster ");
            TXTKTO.DataTextField = "Email";
            TXTKTO.DataValueField = "Email";
            TXTKTO.DataBind();
            TXTKTO.Items.Insert(0, "Select Supplier Email");
            SetInitialRow_Grditem();
        }
        PaneName.Value = Request.Form[PaneName.UniqueID];
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

    //  [WebMethod]

    //public static List<string> GetAutoCompleteData(string username)
    //{
    //    List<string> result = new List<string>();


    //         SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MayurInventory"].ConnectionString);


    //        using (SqlCommand cmd = new SqlCommand("select Email from SuplierMaster where Email LIKE '%'+@SearchText+'%'", cn))
    //        {
    //        cn.Open();
    //            cmd.Parameters.AddWithValue("@SearchText", username);
    //            SqlDataReader dr = cmd.ExecuteReader();
    //            while (dr.Read())
    //            {
    //                result.Add(dr["Email"].ToString());
    //            }
    //            return result;
    //        }

    //}

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
        string abc4 = ddlCostCentre.SelectedItem.ToString();

        if (abc4 != "-- Select Project--")
        {

            if (chkrevised.Checked == true)
            {

                string newrevisednumber = "";

                string str = lblReqNo.Text;


                int rv = Convert.ToInt32(db.getDb_Value("select  top 1  rc.revised  from RequisitionCafeteria RC  INNER JOIN RequisitionCafeDtls RCD    ON RC.RequisitionCafeId = RCD.RequisitionCafeId   where  RC.IsCostCentre='" + ddlCostCentre.SelectedValue + "'  and  RCD.TemplateID='" + "0" + "'  order by rc.RequisitionCafeId desc "));
                rv++;


                string[] split = str.Split('/');

                string str0 = split[0];
                string str1 = split[1];
                string str2 = split[2];
                string str3 = split[3];
                string str4 = split[4];

                newrevisednumber = str0 + " /" + str1 + "/" + str2 + "/" + str3 + "/" + str4 + "/" + "R" + "0" + rv.ToString();

                db.insert("UPDATE RC      SET RC.status='" + "0" + "'  from RequisitionCafeteria RC  INNER JOIN RequisitionCafeDtls RCD    ON RC.RequisitionCafeId = RCD.RequisitionCafeId  where  RC.IsCostCentre='" + ddlCostCentre.SelectedValue + "' and  RCD.TemplateID='"+ "0" + "' ");

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
                        CheckOrderAmount_New();
                        if (valueofitem == 2)
                        {
                            if (clear == true)
                            {
                                CheckTextBox();
                                if (flag1 == true)
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


                                    db.insert("UPDATE RC      SET status = 0   from RequisitionCafeteria RC  INNER JOIN RequisitionCafeDtls RCD    ON RC.RequisitionCafeId = RCD.RequisitionCafeId  where  RC.IsCostCentre='" + ddlCostCentre.SelectedValue + "' and  RCD.TemplateID='" + "0" + "'");
                                    db.insert("update RequisitionCafeteria set  revised='" + rv + "' ,status='" + "1" + "' where  RequisitionNo='" + newrevisednumber + "'");
                                    int MaxID = insertrow;
                                    int insertdtls = 0;
                                    if (insertrow != 0)
                                    {
                                        for (int g = 0; g < grditem.Rows.Count; g++)
                                        {

                                            CheckBox chkRow = (grditem.Rows[g].FindControl("chkbox") as CheckBox);
                                            if (chkRow.Checked)
                                            {
                                                if (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text) > 0)
                                                {
                                                    Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.VendorId = 1;
                                                    Entity_RequisitionCafeteria.Rate = 1;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                                    string itemname = (((TextBox)grditem.Rows[g].FindControl("TxtItemName1")).Text);
                                                    string pipelenght = (((TextBox)grditem.Rows[g].FindControl("txtpopelenghtunitqty")).Text);
                                                    string avlstock = (((Label)grditem.Rows[g].FindControl("txtavlqty")).Text);
                                                    string balstock = (((Label)grditem.Rows[g].FindControl("txtbalqty")).Text);
                                                    string type = (((RadioButtonList)grditem.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                                    string abc = itemname;
                                                    string[] tokens = abc.Split('=');
                                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                                    Entity_RequisitionCafeteria.IsCancel = false;
                                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                                    //--Newly Added Field-- 
                                                    string valve = db.getDbstatus_Value("select valve from  tempnonstdpipebracketindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + MaxID + "'");
                                                    if (valve == null)
                                                    {
                                                        string message = "";
                                                        int count = 0;
                                                        foreach (ListItem item in lstvalve.Items)
                                                        {
                                                            count++;
                                                            if (item.Selected)
                                                            {
                                                                message += item.Text;
                                                            }
                                                        }
                                                        valve = message.ToString();
                                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                                    }
                                                    else
                                                    {
                                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                                    }
                                                    DropDownList ddlItemDescription = (((DropDownList)grditem.Rows[g].FindControl("ddlItemDescription")));
                                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                                    Entity_RequisitionCafeteria.RemarkForPO = type.ToString();
                                                    #region[Convert Quantity accordng to UnitFactor]
                                                    //---Coversionfactor---
                                                    UnitConvDtlsIdT = 1;
                                                    ItemId = Convert.ToInt32(itemid);//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                                    string unitconvrt = "Nos";
                                                    DataSet DsTemp = new DataSet();
                                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                                    if (DsTemp.Tables.Count > 0)
                                                    {
                                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                        {
                                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                            }
                                                            else
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text));
                                                            }
                                                        }
                                                    }

                                                    Qty = (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text));
                                                    Entity_RequisitionCafeteria.Qty = Qty;
                                                    Entity_RequisitionCafeteria.ItemId = ItemId;

                                                    #endregion
                                                    decimal a = Qty;
                                                    decimal b = Convert.ToDecimal(pipelenght);
                                                    decimal c = a * b;
                                                    Entity_RequisitionCafeteria.balstock = balstock + " " + "Mtr";
                                                    Entity_RequisitionCafeteria.avlstock = avlstock;
                                                    Entity_RequisitionCafeteria.unit = c + " " + "Mtr";
                                                    insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                                    //  db.insert("insert into nonstdpipebracketindent values('" + MaxID + "','"+ itemname + "' ,'"+ "0" + "','"+"0"+"','"+ Qty + "')");
                                                    db.insert("insert into nonstdpipebracketindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "','" + pipelenght + "' ,'" + avlstock + "' ,'" + balstock + "','" + type + "')");

                                                }
                                            }
                                        }

                                        for (int g = 0; g < grdplate.Rows.Count; g++)
                                        {

                                            CheckBox chkRow = (grdplate.Rows[g].FindControl("chkbox") as CheckBox);
                                            if (chkRow.Checked)
                                            {
                                                if (Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text) > 0)
                                                {
                                                    Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.VendorId = 1;
                                                    Entity_RequisitionCafeteria.Rate = 1;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                                    string itemname = (((TextBox)grdplate.Rows[g].FindControl("TxtItemNameplate")).Text);
                                                    string areaofunitqty = (((TextBox)grdplate.Rows[g].FindControl("txtareaforunitqty")).Text);
                                                    string totalarea = (((TextBox)grdplate.Rows[g].FindControl("lbltotalareaplate")).Text);
                                                    string avlstock = (((Label)grdplate.Rows[g].FindControl("lblavlqty")).Text);
                                                    string balstock = (((Label)grdplate.Rows[g].FindControl("txtbalqty")).Text);
                                                    string type = (((RadioButtonList)grdplate.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                                    string abc = itemname;
                                                    string[] tokens = abc.Split('=');
                                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));

                                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                                    Entity_RequisitionCafeteria.IsCancel = false;
                                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                                    //--Newly Added Field-- 
                                                    string valve = db.getDbstatus_Value("select valve from  tempnonstdplateindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + MaxID + "'");
                                                    if (valve == null)
                                                    {
                                                        string message = "";
                                                        int count = 0;
                                                        foreach (ListItem item in lstvalve.Items)
                                                        {
                                                            count++;
                                                            if (item.Selected)
                                                            {
                                                                message += item.Text;
                                                            }
                                                        }
                                                        valve = message.ToString();
                                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                                    }
                                                    else
                                                    {
                                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                                    }
                                                    DropDownList ddlItemDescription = (((DropDownList)grdplate.Rows[g].FindControl("ddlItemDescriptionplate")));
                                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                                    Entity_RequisitionCafeteria.RemarkForPO = type.ToString();
                                                    #region[Convert Quantity accordng to UnitFactor]
                                                    //---Coversionfactor---
                                                    UnitConvDtlsIdT = 1;
                                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                                    string unitconvrt = "Nos";
                                                    DataSet DsTemp = new DataSet();
                                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                                    if (DsTemp.Tables.Count > 0)
                                                    {
                                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                        {
                                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                            }
                                                            else
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text));
                                                            }
                                                        }
                                                    }

                                                    Entity_RequisitionCafeteria.Qty = Qty;
                                                    #endregion
                                                    Entity_RequisitionCafeteria.balstock = balstock + " " + "Sq.Mtr";
                                                    Entity_RequisitionCafeteria.avlstock = avlstock;
                                                    Entity_RequisitionCafeteria.unit = totalarea + " " + "Sq.Mtr";
                                                    insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                                    db.insert("insert into nonstdplateindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + areaofunitqty + "','" + totalarea + "','" + avlstock + "','" + balstock + "','" + type + "')");
                                                }
                                            }
                                        }
                                        for (int g = 0; g < grdadapter.Rows.Count; g++)
                                        {
                                            CheckBox chkRow = (grdadapter.Rows[g].FindControl("chkbox") as CheckBox);
                                            if (chkRow.Checked)
                                            {
                                                if (Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text) > 0)
                                                {
                                                    Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.VendorId = 1;
                                                    Entity_RequisitionCafeteria.Rate = 1;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                                    string itemname = (((TextBox)grdadapter.Rows[g].FindControl("TxtItemNameadapter")).Text);
                                                    string lblavlqty = (((Label)grdadapter.Rows[g].FindControl("lblavlqty")).Text);
                                                    string txtbalqty = (((Label)grdadapter.Rows[g].FindControl("txtbalqty")).Text);
                                                    string rbitemtype = (((RadioButtonList)grdadapter.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                                    string abc = itemname;
                                                    string[] tokens = abc.Split('=');
                                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                                    Entity_RequisitionCafeteria.IsCancel = false;
                                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                                    //--Newly Added Field-- 
                                                    string valve = db.getDbstatus_Value("select valve from  tempnonstdadapterindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + MaxID + "'");
                                                    if (valve == null)
                                                    {
                                                        string message = "";
                                                        int count = 0;
                                                        foreach (ListItem item in lstvalve.Items)
                                                        {
                                                            count++;
                                                            if (item.Selected)
                                                            {
                                                                message += item.Text;
                                                            }
                                                        }
                                                        valve = message.ToString();
                                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                                    }
                                                    else
                                                    {
                                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                                    }
                                                    DropDownList ddlItemDescription = (((DropDownList)grdadapter.Rows[g].FindControl("ddlItemDescriptionadapter")));
                                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                                    Entity_RequisitionCafeteria.RemarkForPO = rbitemtype.ToString();
                                                    #region[Convert Quantity accordng to UnitFactor]
                                                    //---Coversionfactor---
                                                    UnitConvDtlsIdT = 1;
                                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                                    string unitconvrt = "Nos";
                                                    DataSet DsTemp = new DataSet();
                                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                                    if (DsTemp.Tables.Count > 0)
                                                    {
                                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                        {
                                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                            }
                                                            else
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text));
                                                            }
                                                        }
                                                    }

                                                    Entity_RequisitionCafeteria.Qty = Qty;
                                                    #endregion
                                                    Entity_RequisitionCafeteria.balstock = txtbalqty;
                                                    Entity_RequisitionCafeteria.avlstock = lblavlqty;
                                                    Entity_RequisitionCafeteria.unit = "";
                                                    insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                                    db.insert("insert into nonstdadapterindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "' ,'" + rbitemtype + "')");
                                                }
                                            }
                                        }

                                        for (int g = 0; g < grdhandwheel.Rows.Count; g++)
                                        {
                                            CheckBox chkRow = (grdhandwheel.Rows[g].FindControl("chkbox") as CheckBox);
                                            if (chkRow.Checked)
                                            {
                                                if (Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text) > 0)
                                                {
                                                    Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.VendorId = 1;
                                                    Entity_RequisitionCafeteria.Rate = 1;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                                    string itemname = (((TextBox)grdhandwheel.Rows[g].FindControl("TxtItemNamehandwheel")).Text);
                                                    string lblavlqty = (((Label)grdhandwheel.Rows[g].FindControl("lblavlqty")).Text);
                                                    string txtbalqty = (((Label)grdhandwheel.Rows[g].FindControl("txtbalqty")).Text);
                                                    string rbitemtype = (((RadioButtonList)grdhandwheel.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                                    string abc = itemname;
                                                    string[] tokens = abc.Split('=');
                                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                                    Entity_RequisitionCafeteria.IsCancel = false;
                                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                                    //--Newly Added Field-- 
                                                    string valve = db.getDbstatus_Value("select valve from  tempnonstdhandwheelindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + MaxID + "'");
                                                    if (valve == null)
                                                    {
                                                        string message = "";
                                                        int count = 0;
                                                        foreach (ListItem item in lstvalve.Items)
                                                        {
                                                            count++;
                                                            if (item.Selected)
                                                            {
                                                                message += count + ")" + item.Text;
                                                            }
                                                        }
                                                        valve = message.ToString();
                                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                                    }
                                                    else
                                                    {
                                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                                    }
                                                    DropDownList ddlItemDescription = (((DropDownList)grdhandwheel.Rows[g].FindControl("ddlItemDescriptionhandwheel")));
                                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                                    Entity_RequisitionCafeteria.RemarkForPO = rbitemtype.ToString();
                                                    #region[Convert Quantity accordng to UnitFactor]
                                                    //---Coversionfactor---
                                                    UnitConvDtlsIdT = 1;
                                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                                    string unitconvrt = "Nos";
                                                    DataSet DsTemp = new DataSet();
                                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                                    if (DsTemp.Tables.Count > 0)
                                                    {
                                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                        {
                                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                            }
                                                            else
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text));
                                                            }
                                                        }
                                                    }

                                                    Entity_RequisitionCafeteria.Qty = Qty;
                                                    #endregion
                                                    Entity_RequisitionCafeteria.balstock = txtbalqty;
                                                    Entity_RequisitionCafeteria.avlstock = lblavlqty;
                                                    Entity_RequisitionCafeteria.unit = "";
                                                    insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                                    db.insert("insert into nonstdhandwheelindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "','" + rbitemtype + "')");
                                                }
                                            }
                                        }
                                        for (int g = 0; g < grdlever.Rows.Count; g++)
                                        {
                                            CheckBox chkRow = (grdlever.Rows[g].FindControl("chkbox") as CheckBox);
                                            if (chkRow.Checked)
                                            {
                                                if (Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text) > 0)
                                                {
                                                    Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.VendorId = 1;
                                                    Entity_RequisitionCafeteria.Rate = 1;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                                    string itemname = (((TextBox)grdlever.Rows[g].FindControl("TxtItemNamelever")).Text);
                                                    string txtlverunitqty = (((TextBox)grdlever.Rows[g].FindControl("txtlverunitqty")).Text);
                                                    string lbltotalleverlength = (((Label)grdlever.Rows[g].FindControl("lbltotalleverlength")).Text);
                                                    string lblavlqty = (((Label)grdlever.Rows[g].FindControl("lblavlqty")).Text);
                                                    string txtbalqty = (((Label)grdlever.Rows[g].FindControl("txtbalqty")).Text);
                                                    string rbitemtype = (((RadioButtonList)grdlever.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                                    string abc = itemname;
                                                    string[] tokens = abc.Split('=');
                                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                                    Entity_RequisitionCafeteria.IsCancel = false;
                                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                                    //--Newly Added Field-- 
                                                    string valve = db.getDbstatus_Value("select valve from  tempnonstdleverindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + MaxID + "'");
                                                    if (valve == null)
                                                    {
                                                        string message = "";
                                                        int count = 0;
                                                        foreach (ListItem item in lstvalve.Items)
                                                        {
                                                            count++;
                                                            if (item.Selected)
                                                            {
                                                                message += item.Text;
                                                            }
                                                        }
                                                        valve = message.ToString();
                                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                                    }
                                                    else
                                                    {
                                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                                    }
                                                    DropDownList ddlItemDescription = (((DropDownList)grdlever.Rows[g].FindControl("ddlItemDescriptionlever")));
                                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                                    Entity_RequisitionCafeteria.RemarkForPO = rbitemtype.ToString();
                                                    #region[Convert Quantity accordng to UnitFactor]
                                                    //---Coversionfactor---
                                                    UnitConvDtlsIdT = 1;
                                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                                    string unitconvrt = "Nos";
                                                    DataSet DsTemp = new DataSet();
                                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                                    if (DsTemp.Tables.Count > 0)
                                                    {
                                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                        {
                                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                            }
                                                            else
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text));
                                                            }
                                                        }
                                                    }

                                                    Entity_RequisitionCafeteria.Qty = Qty;
                                                    #endregion
                                                    Entity_RequisitionCafeteria.balstock = txtbalqty + " " + "Mtr";
                                                    Entity_RequisitionCafeteria.avlstock = lblavlqty;
                                                    Entity_RequisitionCafeteria.unit = lbltotalleverlength + " " + "Mtr";
                                                    insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                                    db.insert("insert into nonstdleverindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + txtlverunitqty + "' ,'" + Qty + "' ,'" + lbltotalleverlength + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "' ,'" + rbitemtype + "')");
                                                }
                                            }
                                        }


                                        for (int g = 0; g < grdsschain.Rows.Count; g++)
                                        {

                                            CheckBox chkRow = (grdsschain.Rows[g].FindControl("chkbox") as CheckBox);
                                            if (chkRow.Checked)
                                            {
                                                if (Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text) > 0)
                                                {
                                                    Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.VendorId = 1;
                                                    Entity_RequisitionCafeteria.Rate = 1;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                                    string itemname = (((TextBox)grdsschain.Rows[g].FindControl("TxtItemNamesschain")).Text);
                                                    string lblavlqty = (((Label)grdsschain.Rows[g].FindControl("lblavlqty")).Text);
                                                    string txtbalqty = (((Label)grdsschain.Rows[g].FindControl("lblbalqty")).Text);
                                                    string ddlItemDescriptionsschain = (((DropDownList)grdsschain.Rows[g].FindControl("ddlItemDescriptionsschain")).SelectedValue);
                                                    string drpsschain = (((DropDownList)grdsschain.Rows[g].FindControl("drpsschain")).SelectedValue);
                                                    string abc = itemname;
                                                    string[] tokens = abc.Split('=');
                                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));

                                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                                    Entity_RequisitionCafeteria.IsCancel = false;
                                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                                    //--Newly Added Field-- 

                                                    string valve = db.getDbstatus_Value("select valve from  tempnonstsschainindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + MaxID + "'");
                                                    if (valve == null)
                                                    {
                                                        string message = "";
                                                        int count = 0;
                                                        foreach (ListItem item in lstvalve.Items)
                                                        {
                                                            count++;
                                                            if (item.Selected)
                                                            {
                                                                message += item.Text;
                                                            }
                                                        }
                                                        valve = message.ToString();
                                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                                    }
                                                    else
                                                    {
                                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                                    }

                                                    DropDownList ddlItemDescription = (((DropDownList)grdsschain.Rows[g].FindControl("ddlItemDescriptionsschain")));
                                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;

                                                    if (drpsschain == "Mtr")
                                                    {
                                                        Entity_RequisitionCafeteria.UnitConvDtlsId = 5;
                                                    }
                                                    else
                                                    {
                                                        Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                                    }
                                                    Entity_RequisitionCafeteria.RemarkForPO = "";
                                                    #region[Convert Quantity accordng to UnitFactor]
                                                    //---Coversionfactor---
                                                    UnitConvDtlsIdT = 1;
                                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                                    string unitconvrt = "Nos";
                                                    DataSet DsTemp = new DataSet();
                                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                                    if (DsTemp.Tables.Count > 0)
                                                    {
                                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                        {
                                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                            }
                                                            else
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text));
                                                            }
                                                        }
                                                    }

                                                    Entity_RequisitionCafeteria.Qty = Qty;
                                                    #endregion
                                                    Entity_RequisitionCafeteria.balstock = txtbalqty;
                                                    Entity_RequisitionCafeteria.avlstock = lblavlqty;
                                                    Entity_RequisitionCafeteria.unit = "";
                                                    insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                                    db.insert("insert into nonstsschainindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "' ,'" + ddlItemDescriptionsschain + "')");
                                                }
                                            }
                                        }

                                        for (int g = 0; g < grdwoodenbox.Rows.Count; g++)
                                        {
                                            CheckBox chkRow = (grdwoodenbox.Rows[g].FindControl("chkbox") as CheckBox);
                                            if (chkRow.Checked)
                                            {
                                                if (Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text) > 0)
                                                {
                                                    Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.VendorId = 1;
                                                    Entity_RequisitionCafeteria.Rate = 1;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                                    Entity_RequisitionCafeteria.MaxStockLevel = 0.ToString();
                                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                                    string itemname = (((TextBox)grdwoodenbox.Rows[g].FindControl("TxtItemNamewooden")).Text);
                                                    string WoodenBoxSize = (((TextBox)grdwoodenbox.Rows[g].FindControl("txtwood")).Text);
                                                    string abc = itemname;
                                                    string[] tokens = abc.Split('=');
                                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                                    Entity_RequisitionCafeteria.IsCancel = false;
                                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                                    //--Newly Added Field-- 
                                                    DropDownList ddlItemDescription = (((DropDownList)grdwoodenbox.Rows[g].FindControl("ddlItemDescriptionwood")));
                                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                                    Entity_RequisitionCafeteria.RemarkForPO = "";
                                                    #region[Convert Quantity accordng to UnitFactor]
                                                    //---Coversionfactor---
                                                    UnitConvDtlsIdT = 1;
                                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                                    string unitconvrt = "Nos";
                                                    DataSet DsTemp = new DataSet();
                                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                                    if (DsTemp.Tables.Count > 0)
                                                    {
                                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                        {
                                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                            }
                                                            else
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text));
                                                            }
                                                        }
                                                    }

                                                    Entity_RequisitionCafeteria.Qty = Qty;
                                                    #endregion
                                                    Entity_RequisitionCafeteria.balstock = "";
                                                    Entity_RequisitionCafeteria.avlstock = "";
                                                    Entity_RequisitionCafeteria.unit = "";
                                                    insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                                    db.insert("insert into nonstdwoodindent values('" + MaxID + "','" + itemname + "' ,'" + WoodenBoxSize + "','" + Qty + "' ,'" + itemdetailsid + "')");
                                                }
                                            }
                                        }

                                        for (int g = 0; g < grdtagplate.Rows.Count; g++)
                                        {
                                            CheckBox chkRow = (grdtagplate.Rows[g].FindControl("chkbox") as CheckBox);
                                            if (chkRow.Checked)
                                            {
                                                if (Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text) > 0)
                                                {
                                                    Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.VendorId = 1;
                                                    Entity_RequisitionCafeteria.Rate = 1;
                                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text);
                                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                                    Entity_RequisitionCafeteria.MaxStockLevel = 0.ToString();
                                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                                    string itemname = (((TextBox)grdtagplate.Rows[g].FindControl("TxtItemNametagplate")).Text);
                                                    string WoodenBoxSize = (((TextBox)grdtagplate.Rows[g].FindControl("txtwood")).Text);
                                                    string abc = itemname;
                                                    string[] tokens = abc.Split('=');
                                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                                    Entity_RequisitionCafeteria.IsCancel = false;
                                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                                    //--Newly Added Field-- 
                                                    DropDownList ddlItemDescription = (((DropDownList)grdtagplate.Rows[g].FindControl("ddlItemDescriptiontagplate")));
                                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                                    Entity_RequisitionCafeteria.RemarkForPO = "";
                                                    #region[Convert Quantity accordng to UnitFactor]
                                                    //---Coversionfactor---
                                                    UnitConvDtlsIdT = 1;
                                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                                    string unitconvrt = "Nos";
                                                    DataSet DsTemp = new DataSet();
                                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                                    if (DsTemp.Tables.Count > 0)
                                                    {
                                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                        {
                                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                            }
                                                            else
                                                            {
                                                                Qty = (Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text));
                                                            }
                                                        }
                                                    }

                                                    Entity_RequisitionCafeteria.Qty = Qty;
                                                    #endregion
                                                    Entity_RequisitionCafeteria.balstock = "";
                                                    Entity_RequisitionCafeteria.avlstock = "";
                                                    Entity_RequisitionCafeteria.unit = "";
                                                    insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                                    db.insert("insert into nonstdtagplateindent values('" + MaxID + "','" + itemname + "' ,'" + WoodenBoxSize + "','" + Qty + "' ,'" + itemdetailsid + "')");
                                                }
                                            }
                                        }
                                    }
                                    if (insertdtls != 0)
                                    {
                                        obj_Comman.ShowPopUpMsg("Record Saved Successfully!", this.Page);
                                        //SendMail(insertrow);
                                        MakeEmptyForm();
                                        GrdRequisition.Visible = false;
                                        grditem.Visible = false;
                                        grdplate.Visible = false;
                                        grdadapter.Visible = false;
                                        grdhandwheel.Visible = false;
                                        grdlever.Visible = false;

                                        grdwoodenbox.Visible = false;
                                        grdtagplate.Visible = false;
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
                db.insert("truncate table tempnonstdpipebracketindent");
                db.insert("truncate table tempnonstdadapterindent");
                db.insert("truncate table tempnonstdhandwheelindent");
                db.insert("truncate table tempnonstdkeyindent");
                db.insert("truncate table tempnonstdleverindent");
                db.insert("truncate table tempnonstdplateindent");
                db.insert("truncate table tempnonstdwoodindent");
                db.insert("truncate table tempnonstsschainindent");



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
                        for (int g = 0; g < grditem.Rows.Count; g++)
                        {
                            CheckBox chkRow = (grditem.Rows[g].FindControl("chkbox") as CheckBox);
                            if (chkRow.Checked)
                            {
                                if (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text) > 0)
                                {
                                    Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(ViewState["EditID"]);
                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.VendorId = 1;
                                    Entity_RequisitionCafeteria.Rate = 1;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                    string itemname = (((TextBox)grditem.Rows[g].FindControl("TxtItemName1")).Text);
                                    string pipelenght = (((TextBox)grditem.Rows[g].FindControl("txtpopelenghtunitqty")).Text);
                                    string avlstock = (((Label)grditem.Rows[g].FindControl("txtavlqty")).Text);
                                    string balstock = (((Label)grditem.Rows[g].FindControl("txtbalqty")).Text);
                                    string type = (((RadioButtonList)grditem.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                    string abc1 = itemname;
                                    string[] tokens = abc1.Split('=');
                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                    Entity_RequisitionCafeteria.IsCancel = false;
                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                    //--Newly Added Field-- 
                                    string valve = db.getDbstatus_Value("select valve from  tempnonstdpipebracketindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    if (valve == null)
                                    {
                                        string message = "";
                                        int count = 0;
                                        foreach (ListItem item in lstvalve.Items)
                                        {
                                            count++;
                                            if (item.Selected)
                                            {
                                                message += item.Text;
                                            }
                                        }
                                        valve = message.ToString();
                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                    }
                                    else
                                    {
                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                    }
                                    DropDownList ddlItemDescription = (((DropDownList)grditem.Rows[g].FindControl("ddlItemDescription")));
                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                    Entity_RequisitionCafeteria.RemarkForPO = type.ToString();
                                    #region[Convert Quantity accordng to UnitFactor]
                                    //---Coversionfactor---
                                    UnitConvDtlsIdT = 1;
                                    ItemId = Convert.ToInt32(itemid);//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                    string unitconvrt = "Nos";
                                    DataSet DsTemp = new DataSet();
                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                    if (DsTemp.Tables.Count > 0)
                                    {
                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                        {
                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                            }
                                            else
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text));
                                            }
                                        }
                                    }

                                    Qty = (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text));
                                    Entity_RequisitionCafeteria.Qty = Qty;
                                    Entity_RequisitionCafeteria.ItemId = ItemId;

                                    #endregion
                                    decimal a = Qty;
                                    decimal b = Convert.ToDecimal(pipelenght);
                                    decimal c = a * b;
                                    Entity_RequisitionCafeteria.balstock = balstock + " " + "Mtr";
                                    Entity_RequisitionCafeteria.avlstock = avlstock;
                                    Entity_RequisitionCafeteria.unit = c + " " + "Mtr";
                                    db.insert("delete  RequisitionCafeDtls where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    UpdateRow = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                    //  db.insert("insert into nonstdpipebracketindent values('" + MaxID + "','"+ itemname + "' ,'"+ "0" + "','"+"0"+"','"+ Qty + "')");
                                    db.insert("delete  nonstdpipebracketindent where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    db.insert("insert into nonstdpipebracketindent values('" + Convert.ToInt32(ViewState["EditID"]) + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "','" + pipelenght + "' ,'" + avlstock + "' ,'" + balstock + "','" + type + "')");

                                }
                            }
                        }
                        for (int g = 0; g < grdplate.Rows.Count; g++)
                        {

                            CheckBox chkRow = (grdplate.Rows[g].FindControl("chkbox") as CheckBox);
                            if (chkRow.Checked)
                            {
                                if (Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text) > 0)
                                {
                                    Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(ViewState["EditID"]);
                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.VendorId = 1;
                                    Entity_RequisitionCafeteria.Rate = 1;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                    string itemname = (((TextBox)grdplate.Rows[g].FindControl("TxtItemNameplate")).Text);
                                    string areaofunitqty = (((TextBox)grdplate.Rows[g].FindControl("txtareaforunitqty")).Text);
                                    string totalarea = (((TextBox)grdplate.Rows[g].FindControl("lbltotalareaplate")).Text);
                                    string avlstock = (((Label)grdplate.Rows[g].FindControl("lblavlqty")).Text);
                                    string balstock = (((Label)grdplate.Rows[g].FindControl("txtbalqty")).Text);
                                    string type = (((RadioButtonList)grdplate.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                    string abc1 = itemname;
                                    string[] tokens = abc1.Split('=');
                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                    Entity_RequisitionCafeteria.IsCancel = false;
                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                    //--Newly Added Field-- 
                                    string valve = db.getDbstatus_Value("select valve from  tempnonstdplateindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    if (valve == null)
                                    {
                                        string message = "";
                                        int count = 0;
                                        foreach (ListItem item in lstvalve.Items)
                                        {
                                            count++;
                                            if (item.Selected)
                                            {
                                                message += item.Text;
                                            }
                                        }
                                        valve = message.ToString();
                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                    }
                                    else
                                    {
                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                    }
                                    DropDownList ddlItemDescription = (((DropDownList)grdplate.Rows[g].FindControl("ddlItemDescriptionplate")));
                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                    Entity_RequisitionCafeteria.RemarkForPO = type.ToString();
                                    #region[Convert Quantity accordng to UnitFactor]
                                    //---Coversionfactor---
                                    UnitConvDtlsIdT = 1;
                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                    string unitconvrt = "Nos";
                                    DataSet DsTemp = new DataSet();
                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                    if (DsTemp.Tables.Count > 0)
                                    {
                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                        {
                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                            }
                                            else
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text));
                                            }
                                        }
                                    }

                                    Entity_RequisitionCafeteria.Qty = Qty;
                                    #endregion
                                    Entity_RequisitionCafeteria.balstock = balstock + " " + "Sq.Mtr";
                                    Entity_RequisitionCafeteria.avlstock = avlstock + " " + "Sq.Mtr";
                                    Entity_RequisitionCafeteria.unit = totalarea + " " + "Sq.Mtr";
                                    db.insert("delete  RequisitionCafeDtls where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    UpdateRow = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                    db.insert("delete  nonstdplateindent where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    db.insert("insert into nonstdplateindent values('" + Convert.ToInt32(ViewState["EditID"]) + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + areaofunitqty + "','" + totalarea + "','" + avlstock + "','" + balstock + "','" + type + "')");

                                }
                            }
                        }

                        for (int g = 0; g < grdadapter.Rows.Count; g++)
                        {

                            CheckBox chkRow = (grdadapter.Rows[g].FindControl("chkbox") as CheckBox);
                            if (chkRow.Checked)
                            {
                                if (Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text) > 0)
                                {
                                    Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(ViewState["EditID"]);
                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.VendorId = 1;
                                    Entity_RequisitionCafeteria.Rate = 1;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                    string itemname = (((TextBox)grdadapter.Rows[g].FindControl("TxtItemNameadapter")).Text);
                                    string lblavlqty = (((Label)grdadapter.Rows[g].FindControl("lblavlqty")).Text);
                                    string txtbalqty = (((Label)grdadapter.Rows[g].FindControl("txtbalqty")).Text);
                                    string rbitemtype = (((RadioButtonList)grdadapter.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                    string abc1 = itemname;
                                    string[] tokens = abc1.Split('=');
                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                    Entity_RequisitionCafeteria.IsCancel = false;
                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                    //--Newly Added Field-- 
                                    string valve = db.getDbstatus_Value("select valve from  tempnonstdadapterindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    if (valve == null)
                                    {
                                        string message = "";
                                        int count = 0;
                                        foreach (ListItem item in lstvalve.Items)
                                        {
                                            count++;
                                            if (item.Selected)
                                            {
                                                message += item.Text;
                                            }
                                        }
                                        valve = message.ToString();
                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                    }
                                    else
                                    {
                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                    }
                                    DropDownList ddlItemDescription = (((DropDownList)grdadapter.Rows[g].FindControl("ddlItemDescriptionadapter")));
                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                    Entity_RequisitionCafeteria.RemarkForPO = rbitemtype.ToString();
                                    #region[Convert Quantity accordng to UnitFactor]
                                    //---Coversionfactor---
                                    UnitConvDtlsIdT = 1;
                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                    string unitconvrt = "Nos";
                                    DataSet DsTemp = new DataSet();
                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                    if (DsTemp.Tables.Count > 0)
                                    {
                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                        {
                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                            }
                                            else
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text));
                                            }
                                        }
                                    }

                                    Entity_RequisitionCafeteria.Qty = Qty;
                                    #endregion
                                    Entity_RequisitionCafeteria.balstock = txtbalqty;
                                    Entity_RequisitionCafeteria.avlstock = lblavlqty;
                                    Entity_RequisitionCafeteria.unit = "";
                                    db.insert("delete  RequisitionCafeDtls where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    UpdateRow = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                    db.insert("delete  nonstdadapterindent  where RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    db.insert("insert into nonstdadapterindent values('" + Convert.ToInt32(ViewState["EditID"]) + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "' ,'" + rbitemtype + "')");
                                }
                            }
                        }

                        for (int g = 0; g < grdhandwheel.Rows.Count; g++)
                        {

                            CheckBox chkRow = (grdhandwheel.Rows[g].FindControl("chkbox") as CheckBox);
                            if (chkRow.Checked)
                            {
                                if (Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text) > 0)
                                {
                                    Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(ViewState["EditID"]);
                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.VendorId = 1;
                                    Entity_RequisitionCafeteria.Rate = 1;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                    string itemname = (((TextBox)grdhandwheel.Rows[g].FindControl("TxtItemNamehandwheel")).Text);
                                    string lblavlqty = (((Label)grdhandwheel.Rows[g].FindControl("lblavlqty")).Text);
                                    string txtbalqty = (((Label)grdhandwheel.Rows[g].FindControl("txtbalqty")).Text);
                                    string rbitemtype = (((RadioButtonList)grdhandwheel.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                    string abc1 = itemname;
                                    string[] tokens = abc1.Split('=');
                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));

                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                    Entity_RequisitionCafeteria.IsCancel = false;
                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                    //--Newly Added Field-- 
                                    string valve = db.getDbstatus_Value("select valve from  tempnonstdhandwheelindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    if (valve == null)
                                    {
                                        string message = "";
                                        int count = 0;
                                        foreach (ListItem item in lstvalve.Items)
                                        {
                                            count++;
                                            if (item.Selected)
                                            {
                                                message += item.Text;
                                            }
                                        }
                                        valve = message.ToString();
                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                    }
                                    else
                                    {
                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                    }
                                    DropDownList ddlItemDescription = (((DropDownList)grdhandwheel.Rows[g].FindControl("ddlItemDescriptionhandwheel")));
                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                    Entity_RequisitionCafeteria.RemarkForPO = rbitemtype.ToString();
                                    #region[Convert Quantity accordng to UnitFactor]
                                    //---Coversionfactor---
                                    UnitConvDtlsIdT = 1;
                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                    string unitconvrt = "Nos";
                                    DataSet DsTemp = new DataSet();
                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                    if (DsTemp.Tables.Count > 0)
                                    {
                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                        {
                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                            }
                                            else
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text));
                                            }
                                        }
                                    }

                                    Entity_RequisitionCafeteria.Qty = Qty;
                                    #endregion
                                    Entity_RequisitionCafeteria.balstock = txtbalqty;
                                    Entity_RequisitionCafeteria.avlstock = lblavlqty;
                                    Entity_RequisitionCafeteria.unit = "";
                                    db.insert("delete  RequisitionCafeDtls where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    UpdateRow = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                    db.insert("delete  nonstdhandwheelindent where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    db.insert("insert into nonstdhandwheelindent values('" + Convert.ToInt32(ViewState["EditID"]) + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "','" + rbitemtype + "')");
                                }
                            }
                        }
                        for (int g = 0; g < grdlever.Rows.Count; g++)
                        {
                            CheckBox chkRow = (grdlever.Rows[g].FindControl("chkbox") as CheckBox);
                            if (chkRow.Checked)
                            {
                                if (Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text) > 0)
                                {
                                    Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(ViewState["EditID"]);
                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.VendorId = 1;
                                    Entity_RequisitionCafeteria.Rate = 1;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                    string itemname = (((TextBox)grdlever.Rows[g].FindControl("TxtItemNamelever")).Text);
                                    string txtlverunitqty = (((TextBox)grdlever.Rows[g].FindControl("txtlverunitqty")).Text);
                                    string lbltotalleverlength = (((Label)grdlever.Rows[g].FindControl("lbltotalleverlength")).Text);
                                    string lblavlqty = (((Label)grdlever.Rows[g].FindControl("lblavlqty")).Text);
                                    string txtbalqty = (((Label)grdlever.Rows[g].FindControl("txtbalqty")).Text);
                                    string rbitemtype = (((RadioButtonList)grdlever.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                    string abc1 = itemname;
                                    string[] tokens = abc1.Split('=');
                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                    Entity_RequisitionCafeteria.IsCancel = false;
                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                    //--Newly Added Field-- 
                                    string valve = db.getDbstatus_Value("select valve from  tempnonstdleverindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    if (valve == null)
                                    {
                                        string message = "";
                                        int count = 0;
                                        foreach (ListItem item in lstvalve.Items)
                                        {
                                            count++;
                                            if (item.Selected)
                                            {
                                                message += item.Text;
                                            }
                                        }
                                        valve = message.ToString();
                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                    }
                                    else
                                    {
                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                    }
                                    DropDownList ddlItemDescription = (((DropDownList)grdlever.Rows[g].FindControl("ddlItemDescriptionlever")));
                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                    Entity_RequisitionCafeteria.RemarkForPO = rbitemtype.ToString();
                                    #region[Convert Quantity accordng to UnitFactor]
                                    //---Coversionfactor---
                                    UnitConvDtlsIdT = 1;
                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                    string unitconvrt = "Nos";
                                    DataSet DsTemp = new DataSet();
                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                    if (DsTemp.Tables.Count > 0)
                                    {
                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                        {
                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                            }
                                            else
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text));
                                            }
                                        }
                                    }

                                    Entity_RequisitionCafeteria.Qty = Qty;
                                    #endregion
                                    Entity_RequisitionCafeteria.balstock = txtbalqty + " " + "Mtr";
                                    Entity_RequisitionCafeteria.avlstock = lblavlqty + " " + "Mtr";
                                    Entity_RequisitionCafeteria.unit = lbltotalleverlength + " " + "Mtr";
                                    db.insert("delete  RequisitionCafeDtls where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    UpdateRow = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                    db.insert("delete  nonstdleverindent  where RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    db.insert("insert into nonstdleverindent values('" + Convert.ToInt32(ViewState["EditID"]) + "','" + itemname + "' ,'" + itemdetailsid + "','" + txtlverunitqty + "' ,'" + Qty + "' ,'" + lbltotalleverlength + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "' ,'" + rbitemtype + "')");
                                }
                            }
                        }

                        for (int g = 0; g < grdsschain.Rows.Count; g++)
                        {

                            CheckBox chkRow = (grdsschain.Rows[g].FindControl("chkbox") as CheckBox);
                            if (chkRow.Checked)
                            {
                                if (Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text) > 0)
                                {
                                    Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(ViewState["EditID"]);
                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.VendorId = 1;
                                    Entity_RequisitionCafeteria.Rate = 1;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                    string itemname = (((TextBox)grdsschain.Rows[g].FindControl("TxtItemNamesschain")).Text);
                                    string lblavlqty = (((Label)grdsschain.Rows[g].FindControl("lblavlqty")).Text);
                                    string txtbalqty = (((Label)grdsschain.Rows[g].FindControl("lblbalqty")).Text);
                                    string ddlItemDescriptionsschain = (((DropDownList)grdsschain.Rows[g].FindControl("ddlItemDescriptionsschain")).SelectedValue);
                                    string drpsschain = (((DropDownList)grdsschain.Rows[g].FindControl("drpsschain")).SelectedValue);
                                    string abc1 = itemname;
                                    string[] tokens = abc1.Split('=');
                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                    Entity_RequisitionCafeteria.IsCancel = false;
                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                    //--Newly Added Field-- 
                                    string valve = db.getDbstatus_Value("select valve from  tempnonstsschainindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    if (valve == null)
                                    {
                                        string message = "";
                                        int count = 0;
                                        foreach (ListItem item in lstvalve.Items)
                                        {
                                            count++;
                                            if (item.Selected)
                                            {
                                                message += item.Text;
                                            }
                                        }
                                        valve = message.ToString();
                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                    }
                                    else
                                    {
                                        Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                    }
                                    DropDownList ddlItemDescription = (((DropDownList)grdsschain.Rows[g].FindControl("ddlItemDescriptionsschain")));
                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                    if (drpsschain == "Mtr")
                                    {
                                        Entity_RequisitionCafeteria.UnitConvDtlsId = 5;
                                    }
                                    else
                                    {
                                        Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                    }
                                    Entity_RequisitionCafeteria.RemarkForPO = "";
                                    #region[Convert Quantity accordng to UnitFactor]
                                    //---Coversionfactor---
                                    UnitConvDtlsIdT = 1;
                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                    string unitconvrt = "Nos";
                                    DataSet DsTemp = new DataSet();
                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                    if (DsTemp.Tables.Count > 0)
                                    {
                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                        {
                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                            }
                                            else
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text));
                                            }
                                        }
                                    }

                                    Entity_RequisitionCafeteria.Qty = Qty;
                                    #endregion
                                    Entity_RequisitionCafeteria.balstock = txtbalqty;
                                    Entity_RequisitionCafeteria.avlstock = lblavlqty;
                                    Entity_RequisitionCafeteria.unit = "";
                                    db.insert("delete  RequisitionCafeDtls where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    UpdateRow = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                    db.insert("delete  nonstsschainindent  where RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    db.insert("insert into nonstsschainindent values('" + Convert.ToInt32(ViewState["EditID"]) + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "' ,'" + ddlItemDescriptionsschain + "')");
                                }
                            }
                        }

                        for (int g = 0; g < grdwoodenbox.Rows.Count; g++)
                        {

                            CheckBox chkRow = (grdwoodenbox.Rows[g].FindControl("chkbox") as CheckBox);
                            if (chkRow.Checked)
                            {
                                if (Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text) > 0)
                                {
                                    Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(ViewState["EditID"]);
                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.VendorId = 1;
                                    Entity_RequisitionCafeteria.Rate = 1;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                    Entity_RequisitionCafeteria.MaxStockLevel = 0.ToString();
                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                    string itemname = (((TextBox)grdwoodenbox.Rows[g].FindControl("TxtItemNamewooden")).Text);
                                    string WoodenBoxSize = (((TextBox)grdwoodenbox.Rows[g].FindControl("txtwood")).Text);
                                    string abc1 = itemname;
                                    string[] tokens = abc1.Split('=');
                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                    Entity_RequisitionCafeteria.IsCancel = false;
                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                    //--Newly Added Field-- 
                                    DropDownList ddlItemDescription = (((DropDownList)grdwoodenbox.Rows[g].FindControl("ddlItemDescriptionwood")));
                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                    Entity_RequisitionCafeteria.RemarkForPO = "";
                                    #region[Convert Quantity accordng to UnitFactor]
                                    //---Coversionfactor---
                                    UnitConvDtlsIdT = 1;
                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                    string unitconvrt = "Nos";
                                    DataSet DsTemp = new DataSet();
                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                    if (DsTemp.Tables.Count > 0)
                                    {
                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                        {
                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                            }
                                            else
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text));
                                            }
                                        }
                                    }

                                    Entity_RequisitionCafeteria.Qty = Qty;
                                    #endregion
                                    Entity_RequisitionCafeteria.balstock = "";
                                    Entity_RequisitionCafeteria.avlstock = "";
                                    Entity_RequisitionCafeteria.unit = "";
                                    db.insert("delete  RequisitionCafeDtls where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    UpdateRow = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                    db.insert("delete  nonstdwoodindent where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    db.insert("insert into nonstdwoodindent values('" + Convert.ToInt32(ViewState["EditID"]) + "','" + itemname + "' ,'" + WoodenBoxSize + "','" + Qty + "' ,'" + itemdetailsid + "')");
                                }
                            }
                        }
                        for (int g = 0; g < grdtagplate.Rows.Count; g++)
                        {
                            CheckBox chkRow = (grdtagplate.Rows[g].FindControl("chkbox") as CheckBox);
                            if (chkRow.Checked)
                            {
                                if (Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text) > 0)
                                {
                                    Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(ViewState["EditID"]);
                                    Entity_RequisitionCafeteria.AvlQty = 0;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.VendorId = 1;
                                    Entity_RequisitionCafeteria.Rate = 1;
                                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text);
                                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                    Entity_RequisitionCafeteria.MaxStockLevel = 0.ToString();
                                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                    Entity_RequisitionCafeteria.TemplateID = 0;
                                    string itemname = (((TextBox)grdtagplate.Rows[g].FindControl("TxtItemNametagplate")).Text);
                                    string WoodenBoxSize = (((TextBox)grdtagplate.Rows[g].FindControl("txtwood")).Text);
                                    string abc1 = itemname;
                                    string[] tokens = abc1.Split('=');
                                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                    Entity_RequisitionCafeteria.ItemId = itemid;
                                    Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                    Entity_RequisitionCafeteria.IsCancel = false;
                                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                    //--Newly Added Field-- 
                                    DropDownList ddlItemDescription = (((DropDownList)grdtagplate.Rows[g].FindControl("ddlItemDescriptiontagplate")));
                                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                    Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                    Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                    Entity_RequisitionCafeteria.RemarkForPO = "";
                                    #region[Convert Quantity accordng to UnitFactor]
                                    //---Coversionfactor---
                                    UnitConvDtlsIdT = 1;
                                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                    string unitconvrt = "Nos";
                                    DataSet DsTemp = new DataSet();
                                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                    if (DsTemp.Tables.Count > 0)
                                    {
                                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                        {
                                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                            }
                                            else
                                            {
                                                Qty = (Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text));
                                            }
                                        }
                                    }

                                    Entity_RequisitionCafeteria.Qty = Qty;
                                    #endregion
                                    Entity_RequisitionCafeteria.balstock = "";
                                    Entity_RequisitionCafeteria.avlstock = "";
                                    Entity_RequisitionCafeteria.unit = "";
                                    db.insert("delete  RequisitionCafeDtls where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    UpdateRow = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                    db.insert("delete  nonstdtagplateindent where  RequisitionCafeId='" + Convert.ToInt32(ViewState["EditID"]) + "'");
                                    db.insert("insert into nonstdtagplateindent values('" + Convert.ToInt32(ViewState["EditID"]) + "','" + itemname + "' ,'" + WoodenBoxSize + "','" + Qty + "' ,'" + itemdetailsid + "')");
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
                catch (Exception ex)
                {

                }
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
                        Btnshow.Visible = false;
                        ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                        Ds = Obj_RequisitionCafeteria.GetRequisitionDetailsForEdititem(Convert.ToInt32(e.CommandArgument), out StrError);
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
                                    GrdRequisition.Visible = false;
                                    gvCustomers.Visible = false;
                                    GrdRequisition.DataSource = Ds.Tables[1];
                                    GrdRequisition.DataBind();
                                }
                                else
                                {
                                    grditem.Visible = true;
                                    grdplate.Visible = true;
                                    grdadapter.Visible = true;
                                    grdhandwheel.Visible = true;
                                    grdlever.Visible = true;
                                    grdtagplate.Visible = true;
                                    grdwoodenbox.Visible = true;
                                    grdsschain.Visible = true;
                                    grditem.DataSource = db.Displaygrid("select *  from nonstdpipebracketindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    grditem.DataBind();
                                    DataSet ds = new DataSet();
                                    ds = db.dgv_display("select *  from nonstdpipebracketindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    for (int g = 0; g < ds.Tables[0].Rows.Count; g++)
                                    {
                                        CheckBox chk = (grditem.Rows[g].FindControl("chkbox") as CheckBox);
                                        DropDownList ddlItemDescription = grditem.Rows[g].FindControl("ddlItemDescription") as DropDownList;
                                        TextBox txtpopelenghtunitqty = grditem.Rows[g].FindControl("txtpopelenghtunitqty") as TextBox;
                                        Label txtavlqty = grditem.Rows[g].FindControl("txtavlqty") as Label;
                                        Label txtbalqty = grditem.Rows[g].FindControl("txtbalqty") as Label;
                                        RadioButtonList rbitemtype = grditem.Rows[g].FindControl("rbitemtype") as RadioButtonList;
                                        chk.Checked = true;
                                        txtpopelenghtunitqty.Text = ds.Tables[0].Rows[g]["pipelenght"].ToString();
                                        txtavlqty.Text = ds.Tables[0].Rows[g]["avlstock"].ToString();
                                        txtbalqty.Text = ds.Tables[0].Rows[g]["balstock"].ToString();
                                        rbitemtype.SelectedValue = ds.Tables[0].Rows[g]["type"].ToString();
                                        ddlItemDescription.SelectedValue = ds.Tables[0].Rows[g]["intemdetailsid"].ToString();
                                    }
                                    grdplate.DataSource = db.Displaygrid("select *  from nonstdplateindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    grdplate.DataBind();
                                    DataSet dsplate = new DataSet();
                                    dsplate = db.dgv_display("select *  from nonstdplateindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");

                                    for (int g = 0; g < dsplate.Tables[0].Rows.Count; g++)
                                    {
                                        CheckBox chk = (grdplate.Rows[g].FindControl("chkbox") as CheckBox);
                                        DropDownList ddlItemDescription = grdplate.Rows[g].FindControl("ddlItemDescriptionplate") as DropDownList;
                                        TextBox areaofunitqty = (((TextBox)grdplate.Rows[g].FindControl("txtareaforunitqty")));
                                        TextBox totalarea = (((TextBox)grdplate.Rows[g].FindControl("lbltotalareaplate")));
                                        Label avlstock = (((Label)grdplate.Rows[g].FindControl("lblavlqty")));
                                        Label balstock = (((Label)grdplate.Rows[g].FindControl("txtbalqty")));
                                        RadioButtonList type = (((RadioButtonList)grdplate.Rows[g].FindControl("rbitemtype")));
                                        chk.Checked = true;
                                        areaofunitqty.Text = dsplate.Tables[0].Rows[g]["areaofunitqty"].ToString();
                                        totalarea.Text = dsplate.Tables[0].Rows[g]["totalarea"].ToString();
                                        avlstock.Text = dsplate.Tables[0].Rows[g]["avlstock"].ToString();
                                        balstock.Text = dsplate.Tables[0].Rows[g]["balstock"].ToString();
                                        type.SelectedValue = dsplate.Tables[0].Rows[g]["type"].ToString();
                                        ddlItemDescription.SelectedValue = dsplate.Tables[0].Rows[g]["itemdetailsid"].ToString();
                                    }
                                    
                                    grdadapter.DataSource = db.Displaygrid("select *  from nonstdadapterindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    grdadapter.DataBind();
                                    DataSet dsadapter = new DataSet();
                                    dsadapter = db.dgv_display("select *  from nonstdadapterindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    for (int g = 0; g < dsadapter.Tables[0].Rows.Count; g++)
                                    {
                                        CheckBox chk = (grdadapter.Rows[g].FindControl("chkbox") as CheckBox);
                                        DropDownList ddlItemDescription = grdadapter.Rows[g].FindControl("ddlItemDescriptionadapter") as DropDownList;
                                        TextBox txtbqty = (((TextBox)grdadapter.Rows[g].FindControl("txtbqty")));
                                        Label avlstock = (((Label)grdadapter.Rows[g].FindControl("lblavlqty")));
                                        Label balstock = (((Label)grdadapter.Rows[g].FindControl("txtbalqty")));
                                        RadioButtonList type = (((RadioButtonList)grdadapter.Rows[g].FindControl("rbitemtype")));
                                        chk.Checked = true;
                                        avlstock.Text = dsadapter.Tables[0].Rows[g]["avlstock"].ToString();
                                        balstock.Text = dsadapter.Tables[0].Rows[g]["balstock"].ToString();
                                        type.SelectedValue = dsadapter.Tables[0].Rows[g]["type"].ToString();
                                        ddlItemDescription.SelectedValue = dsadapter.Tables[0].Rows[g]["itemdetailsid"].ToString();
                                    }
                                    int count = Convert.ToInt32(db.getDb_Value("select count(*) from nonstsschainindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'"));
                                    if (count >= 1)
                                    {
                                        grdsschain.DataSource = db.Displaygrid("select *  from nonstsschainindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                        grdsschain.DataBind();
                                        DataSet dschain = new DataSet();
                                        dschain = db.dgv_display("select *  from nonstsschainindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                        for (int g = 0; g < dschain.Tables[0].Rows.Count; g++)
                                        {
                                            CheckBox chk = (grdsschain.Rows[g].FindControl("chkbox") as CheckBox);
                                            DropDownList ddlItemDescription = grdsschain.Rows[g].FindControl("ddlItemDescriptionsschain") as DropDownList;
                                            Label avlstock = (((Label)grdsschain.Rows[g].FindControl("lblavlqty")));
                                            Label balstock = (((Label)grdsschain.Rows[g].FindControl("lblbalqty")));
                                            DropDownList drpsschain = (((DropDownList)grdsschain.Rows[g].FindControl("drpsschain")));
                                            chk.Checked = true;
                                            avlstock.Text = dschain.Tables[0].Rows[g]["avlstock"].ToString();
                                            balstock.Text = dschain.Tables[0].Rows[g]["balstock"].ToString();
                                            drpsschain.SelectedValue = dschain.Tables[0].Rows[g]["unit"].ToString();
                                            ddlItemDescription.SelectedValue = dschain.Tables[0].Rows[g]["itemdetailsid"].ToString();
                                        }
                                        lblgrditem.Text = "Arm assembly Pipe/Rod";
                                        lblgrdplate.Text = "Arm assembly Plate & Support Plate";
                                        dvadpater.Visible = false;
                                        dvhook.Visible = true;
                                        dvhandwheel.Visible = false;
                                        dvlever.Visible = false;
                                    }
                                    else
                                    {
                                        lblgrdplate.Text = "Plate, U-shape, L-shape  Bracket";
                                        lblgrditem.Text = "Pipe Bracket";
                                        dvadpater.Visible = true;
                                        dvhook.Visible = false;
                                        dvhandwheel.Visible = true;
                                        dvlever.Visible = true;
                                    }
                                    grdhandwheel.DataSource = db.Displaygrid("select *  from nonstdhandwheelindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    grdhandwheel.DataBind();
                                    DataSet dshandwhhel = new DataSet();
                                    dshandwhhel = db.dgv_display("select *  from nonstdhandwheelindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    for (int g = 0; g < dshandwhhel.Tables[0].Rows.Count; g++)
                                    {
                                        CheckBox chk = (grdhandwheel.Rows[g].FindControl("chkbox") as CheckBox);
                                        DropDownList ddlItemDescription = grdhandwheel.Rows[g].FindControl("ddlItemDescriptionhandwheel") as DropDownList;
                                        TextBox txtbqty = (((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")));
                                        Label avlstock = (((Label)grdhandwheel.Rows[g].FindControl("lblavlqty")));
                                        Label balstock = (((Label)grdhandwheel.Rows[g].FindControl("txtbalqty")));
                                        RadioButtonList type = (((RadioButtonList)grdhandwheel.Rows[g].FindControl("rbitemtype")));
                                        chk.Checked = true;
                                        avlstock.Text = dshandwhhel.Tables[0].Rows[g]["avlstock"].ToString();
                                        balstock.Text = dshandwhhel.Tables[0].Rows[g]["balstock"].ToString();
                                        type.SelectedValue = dshandwhhel.Tables[0].Rows[g]["type"].ToString();
                                        ddlItemDescription.SelectedValue = dshandwhhel.Tables[0].Rows[g]["itemdetailsid"].ToString();
                                    }
                                    grdlever.DataSource = db.Displaygrid("select *  from nonstdleverindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    grdlever.DataBind();
                                    DataSet dsleverl = new DataSet();
                                    dsleverl = db.dgv_display("select *  from nonstdleverindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    for (int g = 0; g < dsleverl.Tables[0].Rows.Count; g++)
                                    {
                                        CheckBox chk = (grdlever.Rows[g].FindControl("chkbox") as CheckBox);
                                        DropDownList ddlItemDescription = grdlever.Rows[g].FindControl("ddlItemDescriptionlever") as DropDownList;
                                        TextBox txtlverunitqty = (((TextBox)grdlever.Rows[g].FindControl("txtlverunitqty")));
                                        Label lbltotalleverlength = (((Label)grdlever.Rows[g].FindControl("lbltotalleverlength")));
                                        Label avlstock = (((Label)grdlever.Rows[g].FindControl("lblavlqty")));
                                        Label balstock = (((Label)grdlever.Rows[g].FindControl("txtbalqty")));
                                        RadioButtonList type = (((RadioButtonList)grdlever.Rows[g].FindControl("rbitemtype")));
                                        chk.Checked = true;
                                        avlstock.Text = dsleverl.Tables[0].Rows[g]["avlstock"].ToString();
                                        balstock.Text = dsleverl.Tables[0].Rows[g]["balstock"].ToString();
                                        type.SelectedValue = dsleverl.Tables[0].Rows[g]["type"].ToString();
                                        ddlItemDescription.SelectedValue = dsleverl.Tables[0].Rows[g]["itemdetailsid"].ToString();
                                        lbltotalleverlength.Text = dsleverl.Tables[0].Rows[g]["TotalLeverLength"].ToString();
                                        txtlverunitqty.Text = dsleverl.Tables[0].Rows[g]["LeverlengtforunitQty"].ToString();
                                    }
                                    grdwoodenbox.DataSource = db.Displaygrid("select *  from nonstdwoodindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    grdwoodenbox.DataBind();
                                    DataSet dswood = new DataSet();
                                    dswood = db.dgv_display("select *  from nonstdwoodindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    for (int g = 0; g < dswood.Tables[0].Rows.Count; g++)
                                    {
                                        CheckBox chk = (grdwoodenbox.Rows[g].FindControl("chkbox") as CheckBox);
                                        DropDownList ddlItemDescription = grdwoodenbox.Rows[g].FindControl("ddlItemDescriptionwood") as DropDownList;
                                        chk.Checked = true;
                                        ddlItemDescription.SelectedValue = dswood.Tables[0].Rows[g]["itemdetailsid"].ToString();
                                    }
                                    grdtagplate.DataSource = db.Displaygrid("select *  from nonstdtagplateindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    grdtagplate.DataBind();
                                    DataSet dstagplate = new DataSet();
                                    dstagplate = db.dgv_display("select *  from nonstdtagplateindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                                    for (int g = 0; g < dstagplate.Tables[0].Rows.Count; g++)
                                    {
                                        CheckBox chk = (grdtagplate.Rows[g].FindControl("chkbox") as CheckBox);
                                        DropDownList ddlItemDescription = grdtagplate.Rows[g].FindControl("ddlItemDescriptiontagplate") as DropDownList;
                                        chk.Checked = true;
                                        ddlItemDescription.SelectedValue = dstagplate.Tables[0].Rows[g]["itemdetailsid"].ToString();
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
                        db.insert("delete nonstdpipebracketindent  where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                        db.insert("delete  nonstdplateindent  where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                        db.insert("delete  nonstdadapterindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                        db.insert("delete  nonstdhandwheelindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                        db.insert("delete  nonstdkeyindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                        db.insert("delete  nonstdleverindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");
                        db.insert("delete  nonstdwoodindent where RequisitionCafeId='" + Convert.ToInt32(e.CommandArgument) + "'");

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
                e.Row.Cells[7].BackColor = System.Drawing.Color.SkyBlue;              //LightSteelBlue;
                e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[8].BackColor = System.Drawing.Color.SkyBlue;
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
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
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

                            db.insert("update RequisitionCafeteria set  revised='" + "0" + "' ,  status='" + "1" + "' where  RequisitionNo='" + txtReqNo.Text + "'");
                            int MaxID = insertrow;
                            int insertdtls = 0;
                            if (insertrow != 0)
                            {
                                for (int g = 0; g < grditem.Rows.Count; g++)
                                {

                                    CheckBox chkRow = (grditem.Rows[g].FindControl("chkbox") as CheckBox);
                                    if (chkRow.Checked)
                                    {
                                        if (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text) > 0)
                                        {
                                            Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                            Entity_RequisitionCafeteria.AvlQty = 0;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.VendorId = 1;
                                            Entity_RequisitionCafeteria.Rate = 1;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                            Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                            //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                            Entity_RequisitionCafeteria.TemplateID = 0;
                                            string itemname = (((TextBox)grditem.Rows[g].FindControl("TxtItemName1")).Text);
                                            string pipelenght = (((TextBox)grditem.Rows[g].FindControl("txtpopelenghtunitqty")).Text);
                                            string avlstock = (((Label)grditem.Rows[g].FindControl("txtavlqty")).Text);
                                            string balstock = (((Label)grditem.Rows[g].FindControl("txtbalqty")).Text);
                                            string type = (((RadioButtonList)grditem.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                            string abc = itemname;
                                            string[] tokens = abc.Split('=');
                                            int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                            Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                            Entity_RequisitionCafeteria.IsCancel = false;
                                            // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                            //--Newly Added Field-- 
                                            string valve = db.getDbstatus_Value("select valve from  tempnonstdpipebracketindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + MaxID + "'");
                                            if (valve == null)
                                            {
                                                string message = "";
                                                int count = 0;
                                                foreach (ListItem item in lstvalve.Items)
                                                {
                                                    count++;
                                                    if (item.Selected)
                                                    {
                                                        message += item.Text;
                                                    }
                                                }
                                                valve = message.ToString();
                                                Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                            }
                                            else
                                            {
                                                Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                            }
                                            DropDownList ddlItemDescription = (((DropDownList)grditem.Rows[g].FindControl("ddlItemDescription")));
                                            int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                            Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                            Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                            Entity_RequisitionCafeteria.RemarkForPO = type.ToString();
                                            #region[Convert Quantity accordng to UnitFactor]
                                            //---Coversionfactor---
                                            UnitConvDtlsIdT = 1;
                                            ItemId = Convert.ToInt32(itemid);//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                            string unitconvrt = "Nos";
                                            DataSet DsTemp = new DataSet();
                                            DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                            if (DsTemp.Tables.Count > 0)
                                            {
                                                for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                {
                                                    if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                    }
                                                    else
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text));
                                                    }
                                                }
                                            }

                                            Qty = (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text));
                                            Entity_RequisitionCafeteria.Qty = Qty;
                                            Entity_RequisitionCafeteria.ItemId = ItemId;

                                            #endregion
                                            decimal a = Qty;
                                            decimal b = Convert.ToDecimal(pipelenght);
                                            decimal c = a * b;
                                            Entity_RequisitionCafeteria.balstock = balstock + " " + "Mtr";
                                            Entity_RequisitionCafeteria.avlstock = avlstock;
                                            Entity_RequisitionCafeteria.unit = c + " " + "Mtr";
                                            insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                            //  db.insert("insert into nonstdpipebracketindent values('" + MaxID + "','"+ itemname + "' ,'"+ "0" + "','"+"0"+"','"+ Qty + "')");
                                            db.insert("insert into nonstdpipebracketindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "','" + pipelenght + "' ,'" + avlstock + "' ,'" + balstock + "','" + type + "')");

                                        }
                                    }
                                }
                                
                                for (int g = 0; g < grdplate.Rows.Count; g++)
                                {

                                    CheckBox chkRow = (grdplate.Rows[g].FindControl("chkbox") as CheckBox);
                                    if (chkRow.Checked)
                                    {
                                        if (Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text) > 0)
                                        {
                                            Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                            Entity_RequisitionCafeteria.AvlQty = 0;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.VendorId = 1;
                                            Entity_RequisitionCafeteria.Rate = 1;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                            Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                            //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                            Entity_RequisitionCafeteria.TemplateID = 0;
                                            string itemname = (((TextBox)grdplate.Rows[g].FindControl("TxtItemNameplate")).Text);
                                            string areaofunitqty = (((TextBox)grdplate.Rows[g].FindControl("txtareaforunitqty")).Text);
                                            string totalarea = (((TextBox)grdplate.Rows[g].FindControl("lbltotalareaplate")).Text);
                                            string avlstock = (((Label)grdplate.Rows[g].FindControl("lblavlqty")).Text);
                                            string balstock = (((Label)grdplate.Rows[g].FindControl("txtbalqty")).Text);
                                            string type = (((RadioButtonList)grdplate.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                            string abc = itemname;
                                            string[] tokens = abc.Split('=');
                                            int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));

                                            Entity_RequisitionCafeteria.ItemId = itemid;
                                            Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                            Entity_RequisitionCafeteria.IsCancel = false;
                                            // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                            //--Newly Added Field-- 
                                            string valve = db.getDbstatus_Value("select valve from  tempnonstdplateindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + MaxID + "'");
                                            if (valve == null)
                                            {
                                                string message = "";
                                                int count = 0;
                                                foreach (ListItem item in lstvalve.Items)
                                                {
                                                    count++;
                                                    if (item.Selected)
                                                    {
                                                        message += item.Text;
                                                    }
                                                }
                                                valve = message.ToString();
                                                Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                            }
                                            else
                                            {
                                                Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                            }
                                            DropDownList ddlItemDescription = (((DropDownList)grdplate.Rows[g].FindControl("ddlItemDescriptionplate")));
                                            int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                            Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                            Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                            Entity_RequisitionCafeteria.RemarkForPO = type.ToString();
                                            #region[Convert Quantity accordng to UnitFactor]
                                            //---Coversionfactor---
                                            UnitConvDtlsIdT = 1;
                                            ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                            string unitconvrt = "Nos";
                                            DataSet DsTemp = new DataSet();
                                            DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                            if (DsTemp.Tables.Count > 0)
                                            {
                                                for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                {
                                                    if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                    }
                                                    else
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text));
                                                    }
                                                }
                                            }

                                            Entity_RequisitionCafeteria.Qty = Qty;
                                            #endregion
                                            Entity_RequisitionCafeteria.balstock = balstock + " " + "Sq.Mtr";
                                            Entity_RequisitionCafeteria.avlstock = avlstock;
                                            Entity_RequisitionCafeteria.unit = totalarea + " " + "Sq.Mtr";
                                            insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                            db.insert("insert into nonstdplateindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + areaofunitqty + "','" + totalarea + "','" + avlstock + "','" + balstock + "','" + type + "')");
                                        }
                                    }
                                }
                                for (int g = 0; g < grdadapter.Rows.Count; g++)
                                {
                                    CheckBox chkRow = (grdadapter.Rows[g].FindControl("chkbox") as CheckBox);
                                    if (chkRow.Checked)
                                    {
                                        if (Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text) > 0)
                                        {
                                            Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                            Entity_RequisitionCafeteria.AvlQty = 0;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.VendorId = 1;
                                            Entity_RequisitionCafeteria.Rate = 1;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                            Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                            //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                            Entity_RequisitionCafeteria.TemplateID = 0;
                                            string itemname = (((TextBox)grdadapter.Rows[g].FindControl("TxtItemNameadapter")).Text);
                                            string lblavlqty = (((Label)grdadapter.Rows[g].FindControl("lblavlqty")).Text);
                                            string txtbalqty = (((Label)grdadapter.Rows[g].FindControl("txtbalqty")).Text);
                                            string rbitemtype = (((RadioButtonList)grdadapter.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                            string abc = itemname;
                                            string[] tokens = abc.Split('=');
                                            int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                            Entity_RequisitionCafeteria.ItemId = itemid;
                                            Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                            Entity_RequisitionCafeteria.IsCancel = false;
                                            // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                            //--Newly Added Field-- 
                                            string valve = db.getDbstatus_Value("select valve from  tempnonstdadapterindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + MaxID + "'");
                                            if (valve == null)
                                            {
                                                string message = "";
                                                int count = 0;
                                                foreach (ListItem item in lstvalve.Items)
                                                {
                                                    count++;
                                                    if (item.Selected)
                                                    {
                                                        message += item.Text;
                                                    }
                                                }
                                                valve = message.ToString();
                                                Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                            }
                                            else
                                            {
                                                Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                            }
                                            DropDownList ddlItemDescription = (((DropDownList)grdadapter.Rows[g].FindControl("ddlItemDescriptionadapter")));
                                            int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                            Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                            Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                            Entity_RequisitionCafeteria.RemarkForPO = rbitemtype.ToString();
                                            #region[Convert Quantity accordng to UnitFactor]
                                            //---Coversionfactor---
                                            UnitConvDtlsIdT = 1;
                                            ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                            string unitconvrt = "Nos";
                                            DataSet DsTemp = new DataSet();
                                            DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                            if (DsTemp.Tables.Count > 0)
                                            {
                                                for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                {
                                                    if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                    }
                                                    else
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text));
                                                    }
                                                }
                                            }

                                            Entity_RequisitionCafeteria.Qty = Qty;
                                            #endregion
                                            Entity_RequisitionCafeteria.balstock = txtbalqty;
                                            Entity_RequisitionCafeteria.avlstock = lblavlqty;
                                            Entity_RequisitionCafeteria.unit = "";
                                            insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                            db.insert("insert into nonstdadapterindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "' ,'" + rbitemtype + "')");
                                        }
                                    }
                                }
                                
                                for (int g = 0; g < grdhandwheel.Rows.Count; g++)
                                {
                                    CheckBox chkRow = (grdhandwheel.Rows[g].FindControl("chkbox") as CheckBox);
                                    if (chkRow.Checked)
                                    {
                                        if (Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text) > 0)
                                        {
                                            Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                            Entity_RequisitionCafeteria.AvlQty = 0;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.VendorId = 1;
                                            Entity_RequisitionCafeteria.Rate = 1;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                            Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                            //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                            Entity_RequisitionCafeteria.TemplateID = 0;
                                            string itemname = (((TextBox)grdhandwheel.Rows[g].FindControl("TxtItemNamehandwheel")).Text);
                                            string lblavlqty = (((Label)grdhandwheel.Rows[g].FindControl("lblavlqty")).Text);
                                            string txtbalqty = (((Label)grdhandwheel.Rows[g].FindControl("txtbalqty")).Text);
                                            string rbitemtype = (((RadioButtonList)grdhandwheel.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                            string abc = itemname;
                                            string[] tokens = abc.Split('=');
                                            int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                            Entity_RequisitionCafeteria.ItemId = itemid;
                                            Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                            Entity_RequisitionCafeteria.IsCancel = false;
                                            // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                            //--Newly Added Field-- 
                                            string valve = db.getDbstatus_Value("select valve from  tempnonstdhandwheelindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + MaxID + "'");
                                            if (valve == null)
                                            {
                                                string message = "";
                                                int count = 0;
                                                foreach (ListItem item in lstvalve.Items)
                                                {
                                                    count++;
                                                    if (item.Selected)
                                                    {
                                                        message += count + ")" + item.Text;
                                                    }
                                                }
                                                valve = message.ToString();
                                                Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                            }
                                            else
                                            {
                                                Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                            }
                                            DropDownList ddlItemDescription = (((DropDownList)grdhandwheel.Rows[g].FindControl("ddlItemDescriptionhandwheel")));
                                            int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                            Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                            Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                            Entity_RequisitionCafeteria.RemarkForPO = rbitemtype.ToString();
                                            #region[Convert Quantity accordng to UnitFactor]
                                            //---Coversionfactor---
                                            UnitConvDtlsIdT = 1;
                                            ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                            string unitconvrt = "Nos";
                                            DataSet DsTemp = new DataSet();
                                            DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                            if (DsTemp.Tables.Count > 0)
                                            {
                                                for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                {
                                                    if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                    }
                                                    else
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text));
                                                    }
                                                }
                                            }

                                            Entity_RequisitionCafeteria.Qty = Qty;
                                            #endregion
                                            Entity_RequisitionCafeteria.balstock = txtbalqty;
                                            Entity_RequisitionCafeteria.avlstock = lblavlqty;
                                            Entity_RequisitionCafeteria.unit = "";
                                            insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                            db.insert("insert into nonstdhandwheelindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "','" + rbitemtype + "')");
                                        }
                                    }
                                }
                                for (int g = 0; g < grdlever.Rows.Count; g++)
                                {
                                    CheckBox chkRow = (grdlever.Rows[g].FindControl("chkbox") as CheckBox);
                                    if (chkRow.Checked)
                                    {
                                        if (Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text) > 0)
                                        {
                                            Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                            Entity_RequisitionCafeteria.AvlQty = 0;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.VendorId = 1;
                                            Entity_RequisitionCafeteria.Rate = 1;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                            Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                            //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                            Entity_RequisitionCafeteria.TemplateID = 0;
                                            string itemname = (((TextBox)grdlever.Rows[g].FindControl("TxtItemNamelever")).Text);
                                            string txtlverunitqty = (((TextBox)grdlever.Rows[g].FindControl("txtlverunitqty")).Text);
                                            string lbltotalleverlength = (((Label)grdlever.Rows[g].FindControl("lbltotalleverlength")).Text);
                                            string lblavlqty = (((Label)grdlever.Rows[g].FindControl("lblavlqty")).Text);
                                            string txtbalqty = (((Label)grdlever.Rows[g].FindControl("txtbalqty")).Text);
                                            string rbitemtype = (((RadioButtonList)grdlever.Rows[g].FindControl("rbitemtype")).SelectedValue);
                                            string abc = itemname;
                                            string[] tokens = abc.Split('=');
                                            int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                            Entity_RequisitionCafeteria.ItemId = itemid;
                                            Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                            Entity_RequisitionCafeteria.IsCancel = false;
                                            // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                            //--Newly Added Field-- 
                                            string valve = db.getDbstatus_Value("select valve from  tempnonstdleverindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + MaxID + "'");
                                            if (valve == null)
                                            {
                                                string message = "";
                                                int count = 0;
                                                foreach (ListItem item in lstvalve.Items)
                                                {
                                                    count++;
                                                    if (item.Selected)
                                                    {
                                                        message += item.Text;
                                                    }
                                                }
                                                valve = message.ToString();
                                                Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                            }
                                            else
                                            {
                                                Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                            }
                                            DropDownList ddlItemDescription = (((DropDownList)grdlever.Rows[g].FindControl("ddlItemDescriptionlever")));
                                            int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                            Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                            Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                            Entity_RequisitionCafeteria.RemarkForPO = rbitemtype.ToString();
                                            #region[Convert Quantity accordng to UnitFactor]
                                            //---Coversionfactor---
                                            UnitConvDtlsIdT = 1;
                                            ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                            string unitconvrt = "Nos";
                                            DataSet DsTemp = new DataSet();
                                            DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                            if (DsTemp.Tables.Count > 0)
                                            {
                                                for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                {
                                                    if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                    }
                                                    else
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text));
                                                    }
                                                }
                                            }

                                            Entity_RequisitionCafeteria.Qty = Qty;
                                            #endregion
                                            Entity_RequisitionCafeteria.balstock = txtbalqty + " " + "Mtr";
                                            Entity_RequisitionCafeteria.avlstock = lblavlqty;
                                            Entity_RequisitionCafeteria.unit = lbltotalleverlength + " " + "Mtr";
                                            insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                            db.insert("insert into nonstdleverindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + txtlverunitqty + "' ,'" + Qty + "' ,'" + lbltotalleverlength + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "' ,'" + rbitemtype + "')");
                                        }
                                    }
                                }


                                for (int g = 0; g < grdsschain.Rows.Count; g++)
                                {

                                    CheckBox chkRow = (grdsschain.Rows[g].FindControl("chkbox") as CheckBox);
                                    if (chkRow.Checked)
                                    {
                                        if (Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text) > 0)
                                        {
                                            Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                            Entity_RequisitionCafeteria.AvlQty = 0;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.VendorId = 1;
                                            Entity_RequisitionCafeteria.Rate = 1;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                            Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                            //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                            Entity_RequisitionCafeteria.TemplateID = 0;
                                            string itemname = (((TextBox)grdsschain.Rows[g].FindControl("TxtItemNamesschain")).Text);
                                            string lblavlqty = (((Label)grdsschain.Rows[g].FindControl("lblavlqty")).Text);
                                            string txtbalqty = (((Label)grdsschain.Rows[g].FindControl("lblbalqty")).Text);
                                            string ddlItemDescriptionsschain = (((DropDownList)grdsschain.Rows[g].FindControl("ddlItemDescriptionsschain")).SelectedValue);
                                            string drpsschain = (((DropDownList)grdsschain.Rows[g].FindControl("drpsschain")).SelectedValue);
                                            string abc = itemname;
                                            string[] tokens = abc.Split('=');
                                            int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));

                                            Entity_RequisitionCafeteria.ItemId = itemid;
                                            Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                            Entity_RequisitionCafeteria.IsCancel = false;
                                            // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                            //--Newly Added Field-- 

                                            string valve = db.getDbstatus_Value("select valve from  tempnonstsschainindent where itemname='" + itemname + "'  and  RequisitionCafeId='" + MaxID + "'");
                                            if (valve == null)
                                            {
                                                string message = "";
                                                int count = 0;
                                                foreach (ListItem item in lstvalve.Items)
                                                {
                                                    count++;
                                                    if (item.Selected)
                                                    {
                                                        message += item.Text;
                                                    }
                                                }
                                                valve = message.ToString();
                                                Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                            }
                                            else
                                            {
                                                Entity_RequisitionCafeteria.MaxStockLevel = valve.ToString();
                                            }

                                            DropDownList ddlItemDescription = (((DropDownList)grdsschain.Rows[g].FindControl("ddlItemDescriptionsschain")));
                                            int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                            Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;

                                            if (drpsschain == "Mtr")
                                            {
                                                Entity_RequisitionCafeteria.UnitConvDtlsId = 5;
                                            }
                                            else
                                            {
                                                Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                            }
                                            Entity_RequisitionCafeteria.RemarkForPO = "";
                                            #region[Convert Quantity accordng to UnitFactor]
                                            //---Coversionfactor---
                                            UnitConvDtlsIdT = 1;
                                            ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                            string unitconvrt = "Nos";
                                            DataSet DsTemp = new DataSet();
                                            DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                            if (DsTemp.Tables.Count > 0)
                                            {
                                                for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                {
                                                    if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                    }
                                                    else
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text));
                                                    }
                                                }
                                            }

                                            Entity_RequisitionCafeteria.Qty = Qty;
                                            #endregion
                                            Entity_RequisitionCafeteria.balstock = txtbalqty;
                                            Entity_RequisitionCafeteria.avlstock = lblavlqty;
                                            Entity_RequisitionCafeteria.unit = "";
                                            insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                            db.insert("insert into nonstsschainindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "' ,'" + ddlItemDescriptionsschain + "')");
                                        }
                                    }
                                }

                                for (int g = 0; g < grdwoodenbox.Rows.Count; g++)
                                {
                                    CheckBox chkRow = (grdwoodenbox.Rows[g].FindControl("chkbox") as CheckBox);
                                    if (chkRow.Checked)
                                    {
                                        if (Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text) > 0)
                                        {
                                            Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                            Entity_RequisitionCafeteria.AvlQty = 0;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.VendorId = 1;
                                            Entity_RequisitionCafeteria.Rate = 1;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                            Entity_RequisitionCafeteria.MaxStockLevel = 0.ToString();
                                            Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                            //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                            Entity_RequisitionCafeteria.TemplateID = 0;
                                            string itemname = (((TextBox)grdwoodenbox.Rows[g].FindControl("TxtItemNamewooden")).Text);
                                            string WoodenBoxSize = (((TextBox)grdwoodenbox.Rows[g].FindControl("txtwood")).Text);
                                            string abc = itemname;
                                            string[] tokens = abc.Split('=');
                                            int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                            Entity_RequisitionCafeteria.ItemId = itemid;
                                            Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                            Entity_RequisitionCafeteria.IsCancel = false;
                                            // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                            //--Newly Added Field-- 
                                            DropDownList ddlItemDescription = (((DropDownList)grdwoodenbox.Rows[g].FindControl("ddlItemDescriptionwood")));
                                            int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                            Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                            Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                            Entity_RequisitionCafeteria.RemarkForPO = "";
                                            #region[Convert Quantity accordng to UnitFactor]
                                            //---Coversionfactor---
                                            UnitConvDtlsIdT = 1;
                                            ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                            string unitconvrt = "Nos";
                                            DataSet DsTemp = new DataSet();
                                            DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                            if (DsTemp.Tables.Count > 0)
                                            {
                                                for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                {
                                                    if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                    }
                                                    else
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text));
                                                    }
                                                }
                                            }

                                            Entity_RequisitionCafeteria.Qty = Qty;
                                            #endregion
                                            Entity_RequisitionCafeteria.balstock = "";
                                            Entity_RequisitionCafeteria.avlstock = "";
                                            Entity_RequisitionCafeteria.unit = "";
                                            insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                            db.insert("insert into nonstdwoodindent values('" + MaxID + "','" + itemname + "' ,'" + WoodenBoxSize + "','" + Qty + "' ,'" + itemdetailsid + "')");
                                        }
                                    }
                                }

                                for (int g = 0; g < grdtagplate.Rows.Count; g++)
                                {
                                    CheckBox chkRow = (grdtagplate.Rows[g].FindControl("chkbox") as CheckBox);
                                    if (chkRow.Checked)
                                    {
                                        if (Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text) > 0)
                                        {
                                            Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                                            Entity_RequisitionCafeteria.AvlQty = 0;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.VendorId = 1;
                                            Entity_RequisitionCafeteria.Rate = 1;
                                            Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text);
                                            Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                                            Entity_RequisitionCafeteria.MaxStockLevel = 0.ToString();
                                            Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                                            //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                                            Entity_RequisitionCafeteria.TemplateID = 0;
                                            string itemname = (((TextBox)grdtagplate.Rows[g].FindControl("TxtItemNametagplate")).Text);
                                            string WoodenBoxSize = (((TextBox)grdtagplate.Rows[g].FindControl("txtwood")).Text);
                                            string abc = itemname;
                                            string[] tokens = abc.Split('=');
                                            int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                                            Entity_RequisitionCafeteria.ItemId = itemid;
                                            Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                                            Entity_RequisitionCafeteria.IsCancel = false;
                                            // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                                            //--Newly Added Field-- 
                                            DropDownList ddlItemDescription = (((DropDownList)grdtagplate.Rows[g].FindControl("ddlItemDescriptiontagplate")));
                                            int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                                            Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                                            Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                                            Entity_RequisitionCafeteria.RemarkForPO = "";
                                            #region[Convert Quantity accordng to UnitFactor]
                                            //---Coversionfactor---
                                            UnitConvDtlsIdT = 1;
                                            ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                                            string unitconvrt = "Nos";
                                            DataSet DsTemp = new DataSet();
                                            DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                                            if (DsTemp.Tables.Count > 0)
                                            {
                                                for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                                                {
                                                    if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                                                    }
                                                    else
                                                    {
                                                        Qty = (Convert.ToDecimal(((TextBox)grdtagplate.Rows[g].FindControl("txtbqty")).Text));
                                                    }
                                                }
                                            }

                                            Entity_RequisitionCafeteria.Qty = Qty;
                                            #endregion
                                            Entity_RequisitionCafeteria.balstock = "";
                                            Entity_RequisitionCafeteria.avlstock = "";
                                            Entity_RequisitionCafeteria.unit = "";
                                            insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                                            db.insert("insert into nonstdtagplateindent values('" + MaxID + "','" + itemname + "' ,'" + WoodenBoxSize + "','" + Qty + "' ,'" + itemdetailsid + "')");
                                        }
                                    }
                                }
                            }
                            if (insertdtls != 0)
                            {
                                obj_Comman.ShowPopUpMsg("Record Saved Successfully!", this.Page);
                                //SendMail(insertrow);
                                MakeEmptyForm();
                                GrdRequisition.Visible = false;
                                grditem.Visible = false;
                                grdplate.Visible = false;
                                grdadapter.Visible = false;
                                grdhandwheel.Visible = false;
                                grdlever.Visible = false;

                                grdwoodenbox.Visible = false;
                                grdtagplate.Visible = false;
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
        db.insert("truncate table tempnonstdpipebracketindent");
        db.insert("truncate table tempnonstdadapterindent");
        db.insert("truncate table tempnonstdhandwheelindent");
        db.insert("truncate table tempnonstdkeyindent");
        db.insert("truncate table tempnonstdleverindent");
        db.insert("truncate table tempnonstdplateindent");
        db.insert("truncate table tempnonstdwoodindent");
        db.insert("truncate table tempnonstsschainindent");
    }
    protected void ddlCostCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        db.insert("truncate table nonstdpipebracketdummy");
        db.insert("truncate table nonstdplatedummy");
        db.insert("truncate table nonstdadapterdummy");
        db.insert("truncate table nonstdhandwheeldummy");
        db.insert("truncate table nonstdleverdummy");
        db.insert("truncate table nonstdkeydummy");
        db.insert("truncate table nonstdwooddummy");
        db.insert("truncate table nonstdtagplatedummy");
        db.insert("truncate table nonstdtagnonstdhardwaredummy");
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
        bindvalve();
        SqlCommand cmdwood = new SqlCommand("    select nonstwood.itemname ,nonstwood.WoodenBoxSize  from nonstwood ");
        cmdwood.Connection = cn;
        cn.Open();
        SqlDataReader rdrwood = cmdwood.ExecuteReader();
        while (rdrwood.Read())
        {
            string itemname = rdrwood["itemname"].ToString();
            string WoodenBoxSize = rdrwood["WoodenBoxSize"].ToString();
            db.insert("insert into nonstdwooddummy values('" + itemname + "' ,'" + WoodenBoxSize + "' )");
        }
        cn.Close();
        grdwoodenbox.DataSource = db.Displaygrid("select *  , '0' as qty  from nonstdwooddummy");
        grdwoodenbox.DataBind();
        //SqlCommand cmdnonstdhardware = new SqlCommand("    select itemname ,WoodenBoxSize  from nonstnonstdhardware ");
        //cmdnonstdhardware.Connection = cn;
        //cn.Open();
        //SqlDataReader rdrnonstdhardware = cmdnonstdhardware.ExecuteReader();
        //while (rdrnonstdhardware.Read())
        //{
        //    string itemname = rdrnonstdhardware["itemname"].ToString();
        //    string WoodenBoxSize = rdrnonstdhardware["WoodenBoxSize"].ToString();
        //    db.insert("insert into nonstdtagnonstdhardwaredummy values('" + itemname + "' ,'" + WoodenBoxSize + "' )");
        //}
        //cn.Close();
        //grdnonstdhardware.DataSource = db.Displaygrid("select *  , '0' as qty  from nonstdtagnonstdhardwaredummy");
        //grdnonstdhardware.DataBind();
        SetInitialRow_grdnonstdhardware();
        try
        {
            SqlCommand cmdtagplate = new SqlCommand("    select nonsttagplate.itemname ,nonsttagplate.WoodenBoxSize  from nonsttagplate ");
            cmdtagplate.Connection = cn;
            cn.Open();
            SqlDataReader rdrtagplate = cmdtagplate.ExecuteReader();
            while (rdrtagplate.Read())
            {
                string itemname = rdrtagplate["itemname"].ToString();
                string WoodenBoxSize = rdrtagplate["WoodenBoxSize"].ToString();
                db.insert("insert into nonstdtagplatedummy values('" + itemname + "' ,'" + WoodenBoxSize + "' )");
            }
            cn.Close();

            grdtagplate.DataSource = db.Displaygrid("select *  , '0' as qty  from nonstdtagplatedummy");
            grdtagplate.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    public void bindvalve()
    {
        string project = db.getDbstatus_Value("select pno from Projectscope where project='" + ddlCostCentre.SelectedValue + "' order by id desc");
        drpvalve.DataSource = db.Displaygrid("select   (  CAST(ROW_NUMBER() OVER (ORDER BY id) AS VARCHAR)  +'-'+  valvetype +  '-'   + valvesize  +  '-' +  valveclass +'-'+ interlock) as valve , interlock from    projectscopedetails  where  pno='" + project + "' ");
        drpvalve.DataValueField = "valve";
        drpvalve.DataTextField = "valve";
        drpvalve.DataBind();
        drpvalve.Items.Insert(0, "Select Valve");
        lstvalve.DataSource = db.Displaygrid("select   (  CAST(ROW_NUMBER() OVER (ORDER BY id) AS VARCHAR)  +'-'+  valvetype +  '-'   + valvesize  +  '-' +  valveclass +'-'+ interlock) as valve , interlock from    projectscopedetails  where  pno='" + project + "' ");
        lstvalve.DataValueField = "valve";
        lstvalve.DataTextField = "valve";
        lstvalve.DataBind();
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
    protected void drpvalve_SelectedIndexChanged(object sender, EventArgs e)
    {
        string drp = drpvalve.SelectedItem.ToString();
        // string[] ssize = drp.Split(new char[0]);
        string[] tokens = drp.Split('-');
        int a = Convert.ToInt32(db.getDb_Value("select TemplateID   from TemplateMaster  where TemplateName='" + tokens[4] + "'"));
        SqlCommand cmd = new SqlCommand("     select nonstdpipebracket.itemname ,nonstdpipebracket.shecdule  , nonstdpipebracket.size from nonstdpipebracket inner join  Nonstandaredmaster on nonstdpipebracket.iterlockid=Nonstandaredmaster.interlock where nonstdpipebracket.iterlockid='" + a + "'");
        cmd.Connection = cn;
        cn.Open();
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            string itemname = rdr["itemname"].ToString();
            string shecdule = rdr["shecdule"].ToString();
            string size = rdr["size"].ToString();
            db.insert("insert into nonstdpipebracketdummy values('" + itemname + "' ,'" + size + "' ,'" + shecdule + "' )");
        }
        rdr.Close();
        cn.Close();
        grditem.DataSource = db.Displaygrid("select *   , '0' as qty from nonstdpipebracketdummy");
        grditem.DataBind();
        SqlCommand cmdplate = new SqlCommand("   select nonstdplate.itemname ,nonstdplate.Thickness  from nonstdplate inner join  Nonstandaredmaster on nonstdplate.iterlockid=Nonstandaredmaster.interlock  where nonstdplate.iterlockid='" + a + "'");
        cmdplate.Connection = cn;
        cn.Open();
        SqlDataReader rdrplate = cmdplate.ExecuteReader();
        while (rdrplate.Read())
        {
            string itemname = rdrplate["itemname"].ToString();
            string Thickness = rdrplate["Thickness"].ToString();
            db.insert("insert into nonstdplatedummy values('" + itemname + "' ,'" + Thickness + "' )");
        }
        cn.Close();
        grdplate.DataSource = db.Displaygrid("select *  , '0' as qty  from nonstdplatedummy");
        grdplate.DataBind();
        SqlCommand cmdadapter = new SqlCommand("   select nonstdadpter.itemname ,nonstdadpter.AdaptorSizes  from nonstdadpter inner join  Nonstandaredmaster on nonstdadpter.iterlockid=Nonstandaredmaster.interlock  where nonstdadpter.iterlockid='" + a + "'");
        cmdadapter.Connection = cn;
        cn.Open();
        SqlDataReader rdradapter = cmdadapter.ExecuteReader();
        while (rdradapter.Read())
        {
            string itemname = rdradapter["itemname"].ToString();
            string AdaptorSizes1 = rdradapter["AdaptorSizes"].ToString();
            db.insert("insert into nonstdadapterdummy values('" + itemname + "' ,'" + AdaptorSizes1 + "' )");
        }
        cn.Close();
        grdadapter.DataSource = db.Displaygrid("select *  , '0' as qty  from nonstdadapterdummy");
        grdadapter.DataBind();
        SqlCommand cmdhandwheel = new SqlCommand("   select nonstdhandwheel.itemname ,nonstdhandwheel.HandwheelSizes  from nonstdhandwheel inner join  Nonstandaredmaster on nonstdhandwheel.iterlockid=Nonstandaredmaster.interlock  where nonstdhandwheel.iterlockid='" + a + "'");
        cmdhandwheel.Connection = cn;
        cn.Open();
        SqlDataReader rdrhandwheel = cmdhandwheel.ExecuteReader();
        while (rdrhandwheel.Read())
        {
            string itemname = rdrhandwheel["itemname"].ToString();
            string HandwheelSizes = rdrhandwheel["HandwheelSizes"].ToString();
            db.insert("insert into nonstdhandwheeldummy values('" + itemname + "' ,'" + HandwheelSizes + "' )");
        }
        cn.Close();
        grdhandwheel.DataSource = db.Displaygrid("select *  , '0' as qty  from nonstdhandwheeldummy");
        grdhandwheel.DataBind();
        SqlCommand cmdlever = new SqlCommand("   select nonstdlever.itemname ,nonstdlever.LeverSizes  from nonstdlever inner join  Nonstandaredmaster on nonstdlever.iterlockid=Nonstandaredmaster.interlock  where nonstdlever.iterlockid='" + a + "'");
        cmdlever.Connection = cn;
        cn.Open();
        SqlDataReader rdrlever = cmdlever.ExecuteReader();
        while (rdrlever.Read())
        {
            string itemname = rdrlever["itemname"].ToString();
            string LeverSizes = rdrlever["LeverSizes"].ToString();
            db.insert("insert into nonstdleverdummy values('" + itemname + "' ,'" + LeverSizes + "' )");
        }
        cn.Close();
        grdlever.DataSource = db.Displaygrid("select *   , '0' as qty from nonstdleverdummy");
        grdlever.DataBind();
    }

    protected void TxtItemName_TextChanged1(object sender, EventArgs e)
    {
    }
    protected void ddlItem_SelectedIndexChanged1(object sender, EventArgs e)
    {
    }

    protected void TxtItemName1_TextChanged(object sender, EventArgs e)
    {

    }
    public void SetInitialRow_Grditem()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add("itemname", typeof(string));
            dt.Columns.Add(new DataColumn("size", typeof(string)));
            dt.Columns.Add("shecdule", typeof(string));
            dt.Columns.Add("length", typeof(string));
            dt.Columns.Add("uom", typeof(string));
            dt.Columns.Add("qty", typeof(string));
            dt.Columns.Add("avlqty", typeof(string));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["itemname"] = "";
            dr["size"] = "";
            dr["shecdule"] = "";
            dr["length"] = "";
            dr["uom"] = "";
            dr["qty"] = "";
            dr["avlqty"] = "";
            dt.Rows.Add(dr);
            ViewState["CurrentTable1"] = dt;
            grditem.DataSource = dt;
            grditem.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void grditem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Ds = new DataSet();
            TextBox TxtItemName = e.Row.FindControl("TxtItemName1") as TextBox;
            if (TxtItemName.Text != null)
            {
                string abc = TxtItemName.Text;
                string[] tokens = abc.Split('=');
                int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(itemid), out StrError);
                DropDownList ddlItemDescription = e.Row.FindControl("ddlItemDescription") as DropDownList;
                ddlItemDescription.DataSource = Ds.Tables[2];
                ddlItemDescription.DataValueField = "#";
                ddlItemDescription.DataTextField = "ItemDesc";
                ddlItemDescription.DataBind();
            }
        }
    }

    protected void grditem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grditem.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {
            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                string fileName;
                fileName = FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                string abc = "~/ScannedDrawings/" + fileName;
                // ItemDetailsId
                // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                DropDownList ddlItemDescription = row.FindControl("ddlItemDescription") as DropDownList;
                int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                Session["templateid1"] = itemdetailsid.ToString();
            }
        }

        if (e.CommandName == "Selectmulti")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grditem.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {
            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                foreach (GridViewRow rows in grditem.Rows)
                {
                    if (((CheckBox)row.FindControl("chkbox")).Checked)
                    {
                        string fileName;
                        fileName = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                        string abc = "~/ScannedDrawings/" + fileName;
                        // ItemDetailsId
                        // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                        DropDownList ddlItemDescription = rows.FindControl("ddlItemDescription") as DropDownList;
                        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                        db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                        Session["templateid1"] = itemdetailsid.ToString();
                    }
                }
            }
        }
    }

    protected void grditem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = Convert.ToInt32(grditem.DataKeys[e.RowIndex].ToString());
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int rowindex = gvr.RowIndex;
        GridViewRow row = grditem.Rows[rowindex];
        DropDownList ddlItemDescription = row.FindControl("ddlItemDescription") as DropDownList;
        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
        Session["templateid1"] = itemdetailsid.ToString();
        string a = "MaterialRequisitionTemplate12";
        string url = "Drawing.aspx";
        Response.Redirect("../Reports/Drawing.aspx?RowIndex=" + rowindex + "&FormName=" + a);
    }

    protected void lnkView_Click1(object sender, EventArgs e)
    {
    }

    protected void grdplate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Ds = new DataSet();
            TextBox TxtItemName = e.Row.FindControl("TxtItemNameplate") as TextBox;
            if (TxtItemName.Text != null)
            {
                string abc = TxtItemName.Text;
                string[] tokens = abc.Split('=');
                int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(itemid), out StrError);
                DropDownList ddlItemDescription = e.Row.FindControl("ddlItemDescriptionplate") as DropDownList;
                ddlItemDescription.DataSource = Ds.Tables[2];
                ddlItemDescription.DataValueField = "#";
                ddlItemDescription.DataTextField = "ItemDesc";
                ddlItemDescription.DataBind();
            }
        }
    }

    protected void grdadapter_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Ds = new DataSet();
            TextBox TxtItemName = e.Row.FindControl("TxtItemNameadapter") as TextBox;
            if (TxtItemName.Text != null)
            {
                string abc = TxtItemName.Text;
                string[] tokens = abc.Split('=');
                int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(itemid), out StrError);
                DropDownList ddlItemDescription = e.Row.FindControl("ddlItemDescriptionadapter") as DropDownList;
                ddlItemDescription.DataSource = Ds.Tables[2];
                ddlItemDescription.DataValueField = "#";
                ddlItemDescription.DataTextField = "ItemDesc";
                ddlItemDescription.DataBind();
            }
        }
    }

    protected void grdhandwheel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Ds = new DataSet();
            TextBox TxtItemName = e.Row.FindControl("TxtItemNamehandwheel") as TextBox;
            if (TxtItemName.Text != null)
            {
                string abc = TxtItemName.Text;
                string[] tokens = abc.Split('=');
                int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(itemid), out StrError);
                DropDownList ddlItemDescription = e.Row.FindControl("ddlItemDescriptionhandwheel") as DropDownList;
                ddlItemDescription.DataSource = Ds.Tables[2];
                ddlItemDescription.DataValueField = "#";
                ddlItemDescription.DataTextField = "ItemDesc";
                ddlItemDescription.DataBind();
            }
        }
    }

    protected void grdlever_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Ds = new DataSet();
            TextBox TxtItemName = e.Row.FindControl("TxtItemNamelever") as TextBox;
            if (TxtItemName.Text != null)
            {
                string abc = TxtItemName.Text;
                string[] tokens = abc.Split('=');
                int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(itemid), out StrError);
                DropDownList ddlItemDescription = e.Row.FindControl("ddlItemDescriptionlever") as DropDownList;
                ddlItemDescription.DataSource = Ds.Tables[2];
                ddlItemDescription.DataValueField = "#";
                ddlItemDescription.DataTextField = "ItemDesc";
                ddlItemDescription.DataBind();
            }
        }

    }

    protected void grdkey_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Ds = new DataSet();
            TextBox TxtItemName = e.Row.FindControl("TxtItemNamekey") as TextBox;
            if (TxtItemName.Text != null)
            {
                string abc = TxtItemName.Text;
                string[] tokens = abc.Split('=');
                int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(itemid), out StrError);
                DropDownList ddlItemDescription = e.Row.FindControl("ddlItemDescriptionkey") as DropDownList;
                ddlItemDescription.DataSource = Ds.Tables[2];
                ddlItemDescription.DataValueField = "#";
                ddlItemDescription.DataTextField = "ItemDesc";
                ddlItemDescription.DataBind();
            }
        }
    }


    protected void grdlever_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = Convert.ToInt32(grdlever.DataKeys[e.RowIndex].ToString());
    }

    protected void grdhandwheel_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = Convert.ToInt32(grdhandwheel.DataKeys[e.RowIndex].ToString());
    }

    protected void grdadapter_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = Convert.ToInt32(grdadapter.DataKeys[e.RowIndex].ToString());
    }

    protected void grdplate_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = Convert.ToInt32(grdplate.DataKeys[e.RowIndex].ToString());
    }

    protected void grdplate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdplate.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {
            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                string fileName;
                fileName = FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                string abc = "~/ScannedDrawings/" + fileName;
                // ItemDetailsId
                // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptionplate") as DropDownList;
                int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                Session["templateid1"] = itemdetailsid.ToString();
            }
        }

        if (e.CommandName == "Selectmulti")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdplate.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {
            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                foreach (GridViewRow rows in grdplate.Rows)
                {
                    if (((CheckBox)row.FindControl("chkbox")).Checked)
                    {
                        string fileName;
                        fileName = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                        string abc = "~/ScannedDrawings/" + fileName;
                        // ItemDetailsId
                        // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                        DropDownList ddlItemDescription = rows.FindControl("ddlItemDescriptionplate") as DropDownList;
                        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                        db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                        Session["templateid1"] = itemdetailsid.ToString();
                    }
                }
            }
        }
    }

    protected void grdadapter_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdadapter.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {
            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                string fileName;
                fileName = FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                string abc = "~/ScannedDrawings/" + fileName;
                // ItemDetailsId
                // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptionadapter") as DropDownList;
                int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                Session["templateid1"] = itemdetailsid.ToString();
            }
        }

        if (e.CommandName == "Selectmulti")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdadapter.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {
            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                foreach (GridViewRow rows in grdadapter.Rows)
                {
                    if (((CheckBox)row.FindControl("chkbox")).Checked)
                    {
                        string fileName;
                        fileName = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                        string abc = "~/ScannedDrawings/" + fileName;
                        // ItemDetailsId
                        // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                        DropDownList ddlItemDescription = rows.FindControl("ddlItemDescriptionadapter") as DropDownList;
                        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                        db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                        Session["templateid1"] = itemdetailsid.ToString();
                    }
                }
            }
        }
    }

    protected void grdhandwheel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdhandwheel.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {
            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                string fileName;
                fileName = FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                string abc = "~/ScannedDrawings/" + fileName;
                // ItemDetailsId
                // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptionhandwheel") as DropDownList;
                int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                Session["templateid1"] = itemdetailsid.ToString();
            }
        }

        if (e.CommandName == "Selectmulti")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdhandwheel.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {
            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                foreach (GridViewRow rows in grdhandwheel.Rows)
                {
                    if (((CheckBox)row.FindControl("chkbox")).Checked)
                    {
                        string fileName;
                        fileName = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                        string abc = "~/ScannedDrawings/" + fileName;
                        // ItemDetailsId
                        // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                        DropDownList ddlItemDescription = rows.FindControl("ddlItemDescriptionhandwheel") as DropDownList;
                        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                        db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                        Session["templateid1"] = itemdetailsid.ToString();
                    }
                }
            }
        }
    }

    protected void grdlever_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdlever.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {
            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                string fileName;
                fileName = FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                string abc = "~/ScannedDrawings/" + fileName;
                // ItemDetailsId
                // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptionlever") as DropDownList;
                int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                Session["templateid1"] = itemdetailsid.ToString();
            }
        }

        if (e.CommandName == "Selectmulti")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdlever.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {
            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                foreach (GridViewRow rows in grdlever.Rows)
                {
                    if (((CheckBox)row.FindControl("chkbox")).Checked)
                    {
                        string fileName;
                        fileName = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                        string abc = "~/ScannedDrawings/" + fileName;
                        // ItemDetailsId
                        // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                        DropDownList ddlItemDescription = rows.FindControl("ddlItemDescriptionlever") as DropDownList;
                        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                        db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                        Session["templateid1"] = itemdetailsid.ToString();
                    }
                }
            }
        }
    }
    protected void btnviewplate_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int rowindex = gvr.RowIndex;
        GridViewRow row = grdplate.Rows[rowindex];
        DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptionplate") as DropDownList;
        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
        Session["templateid1"] = itemdetailsid.ToString();
        string a = "MaterialRequisitionTemplate12";
        string url = "Drawing.aspx";
        Response.Redirect("../Reports/Drawing.aspx?RowIndex=" + rowindex + "&FormName=" + a);
    }

    protected void btnviewadpater_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int rowindex = gvr.RowIndex;
        GridViewRow row = grdadapter.Rows[rowindex];
        DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptionadapter") as DropDownList;
        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
        Session["templateid1"] = itemdetailsid.ToString();
        string a = "MaterialRequisitionTemplate12";
        string url = "Drawing.aspx";
        Response.Redirect("../Reports/Drawing.aspx?RowIndex=" + rowindex + "&FormName=" + a);
    }

    protected void btnviewhandwheel_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int rowindex = gvr.RowIndex;
        GridViewRow row = grdhandwheel.Rows[rowindex];
        DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptionhandwheel") as DropDownList;
        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
        Session["templateid1"] = itemdetailsid.ToString();
        string a = "MaterialRequisitionTemplate12";
        string url = "Drawing.aspx";
        Response.Redirect("../Reports/Drawing.aspx?RowIndex=" + rowindex + "&FormName=" + a);
    }

    protected void btnviewlever_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int rowindex = gvr.RowIndex;
        GridViewRow row = grdlever.Rows[rowindex];
        DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptionlever") as DropDownList;
        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
        Session["templateid1"] = itemdetailsid.ToString();
        string a = "MaterialRequisitionTemplate12";
        string url = "Drawing.aspx";
        Response.Redirect("../Reports/Drawing.aspx?RowIndex=" + rowindex + "&FormName=" + a);
    }
    protected void grdwoodenbox_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = Convert.ToInt32(grdwoodenbox.DataKeys[e.RowIndex].ToString());
    }

    protected void grdwoodenbox_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Ds = new DataSet();
            TextBox TxtItemName = e.Row.FindControl("TxtItemNamewooden") as TextBox;
            if (TxtItemName.Text != null)
            {
                string abc = TxtItemName.Text;
                string[] tokens = abc.Split('=');
                int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(itemid), out StrError);
                DropDownList ddlItemDescription = e.Row.FindControl("ddlItemDescriptionwood") as DropDownList;
                ddlItemDescription.DataSource = Ds.Tables[2];
                ddlItemDescription.DataValueField = "#";
                ddlItemDescription.DataTextField = "ItemDesc";
                ddlItemDescription.DataBind();
            }
        }
    }

    protected void grdwoodenbox_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdwoodenbox.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {
            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                string fileName;
                fileName = FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                string abc = "~/ScannedDrawings/" + fileName;
                // ItemDetailsId
                // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptionwood") as DropDownList;
                int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                Session["templateid1"] = itemdetailsid.ToString();
            }
        }

        if (e.CommandName == "Selectmulti")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdwoodenbox.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {
            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                foreach (GridViewRow rows in grdwoodenbox.Rows)
                {
                    if (((CheckBox)row.FindControl("chkbox")).Checked)
                    {
                        string fileName;
                        fileName = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                        string abc = "~/ScannedDrawings/" + fileName;
                        // ItemDetailsId
                        // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                        DropDownList ddlItemDescription = rows.FindControl("ddlItemDescriptionwood") as DropDownList;
                        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                        db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                        Session["templateid1"] = itemdetailsid.ToString();
                    }
                }
            }
        }
    }

    protected void btnviewwood_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int rowindex = gvr.RowIndex;
        GridViewRow row = grdwoodenbox.Rows[rowindex];
        DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptionwood") as DropDownList;
        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
        Session["templateid1"] = itemdetailsid.ToString();
        string a = "MaterialRequisitionTemplate12";
        string url = "Drawing.aspx";
        Response.Redirect("../Reports/Drawing.aspx?RowIndex=" + rowindex + "&FormName=" + a);
    }

    protected void drp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string drp = drpvalve.SelectedItem.ToString();
        // string[] ssize = drp.Split(new char[0]);
        string[] tokens = drp.Split('-');
        int a = Convert.ToInt32(db.getDb_Value("select TemplateID   from TemplateMaster  where TemplateName='" + tokens[4] + "'"));
        SqlCommand cmd = new SqlCommand("     select nonstdpipebracket.itemname ,nonstdpipebracket.shecdule  , nonstdpipebracket.size from nonstdpipebracket inner join  Nonstandaredmaster on nonstdpipebracket.iterlockid=Nonstandaredmaster.interlock where nonstdpipebracket.iterlockid='" + a + "'");
        cmd.Connection = cn;
        cn.Open();
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            string itemname = rdr["itemname"].ToString();
            string shecdule = rdr["shecdule"].ToString();
            string size = rdr["size"].ToString();
            db.insert("insert into nonstdpipebracketdummy values('" + itemname + "' ,'" + size + "' ,'" + shecdule + "' )");
        }
        rdr.Close();
        cn.Close();
        grditem.DataSource = db.Displaygrid("select *  from nonstdpipebracketdummy");
        grditem.DataBind();
        SqlCommand cmdplate = new SqlCommand("   select nonstdplate.itemname ,nonstdplate.Thickness  from nonstdplate inner join  Nonstandaredmaster on nonstdplate.iterlockid=Nonstandaredmaster.interlock  where nonstdplate.iterlockid='" + a + "'");
        cmdplate.Connection = cn;
        cn.Open();
        SqlDataReader rdrplate = cmdplate.ExecuteReader();
        while (rdrplate.Read())
        {
            string itemname = rdrplate["itemname"].ToString();
            string Thickness = rdrplate["Thickness"].ToString();
            db.insert("insert into nonstdplatedummy values('" + itemname + "' ,'" + Thickness + "' )");
        }
        cn.Close();
        grdplate.DataSource = db.Displaygrid("select *  from nonstdplatedummy");
        grdplate.DataBind();
        SqlCommand cmdadapter = new SqlCommand("   select nonstdadpter.itemname ,nonstdadpter.AdaptorSizes  from nonstdadpter inner join  Nonstandaredmaster on nonstdadpter.iterlockid=Nonstandaredmaster.interlock  where nonstdadpter.iterlockid='" + a + "'");
        cmdadapter.Connection = cn;
        cn.Open();
        SqlDataReader rdradapter = cmdadapter.ExecuteReader();
        while (rdradapter.Read())
        {
            string itemname = rdradapter["itemname"].ToString();
            string AdaptorSizes1 = rdradapter["AdaptorSizes"].ToString();
            db.insert("insert into nonstdadapterdummy values('" + itemname + "' ,'" + AdaptorSizes1 + "' )");
        }
        cn.Close();
        grdadapter.DataSource = db.Displaygrid("select *  from nonstdadapterdummy");
        grdadapter.DataBind();
        SqlCommand cmdhandwheel = new SqlCommand("   select nonstdhandwheel.itemname ,nonstdhandwheel.HandwheelSizes  from nonstdhandwheel inner join  Nonstandaredmaster on nonstdhandwheel.iterlockid=Nonstandaredmaster.interlock  where nonstdhandwheel.iterlockid='" + a + "'");
        cmdhandwheel.Connection = cn;
        cn.Open();
        SqlDataReader rdrhandwheel = cmdhandwheel.ExecuteReader();
        while (rdrhandwheel.Read())
        {
            string itemname = rdrhandwheel["itemname"].ToString();
            string HandwheelSizes = rdrhandwheel["HandwheelSizes"].ToString();
            db.insert("insert into nonstdhandwheeldummy values('" + itemname + "' ,'" + HandwheelSizes + "' )");
        }
        cn.Close();
        grdhandwheel.DataSource = db.Displaygrid("select *  from nonstdhandwheeldummy");
        grdhandwheel.DataBind();
        SqlCommand cmdlever = new SqlCommand("   select nonstdlever.itemname ,nonstdlever.LeverSizes  from nonstdlever inner join  Nonstandaredmaster on nonstdlever.iterlockid=Nonstandaredmaster.interlock  where nonstdlever.iterlockid='" + a + "'");
        cmdlever.Connection = cn;
        cn.Open();
        SqlDataReader rdrlever = cmdlever.ExecuteReader();
        while (rdrlever.Read())
        {
            string itemname = rdrlever["itemname"].ToString();
            string LeverSizes = rdrlever["LeverSizes"].ToString();
            db.insert("insert into nonstdleverdummy values('" + itemname + "' ,'" + LeverSizes + "' )");
        }
        cn.Close();
        grdlever.DataSource = db.Displaygrid("select *  from nonstdleverdummy");
        grdlever.DataBind();
    }

    protected void btntempsave_Click(object sender, EventArgs e)
    {
        int UnitConvDtlsIdT = 0, ItemId = 0; decimal Qty = 0;
        int MaxID = Convert.ToInt32(db.getDb_Value("select max(RequisitionCafeId) from  RequisitionCafeteria"));
        MaxID++;
        ViewState["maxid"] = MaxID;
        for (int g = 0; g < grditem.Rows.Count; g++)
        {
            CheckBox chkRow = (grditem.Rows[g].FindControl("chkbox") as CheckBox);
            if (chkRow.Checked)
            {
                if (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text) > 0)
                {
                    Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                    Entity_RequisitionCafeteria.AvlQty = 0;
                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text);
                    Entity_RequisitionCafeteria.VendorId = 1;
                    Entity_RequisitionCafeteria.Rate = 1;
                    Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text);
                    Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                    Entity_RequisitionCafeteria.MaxStockLevel = 0.ToString();
                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                    Entity_RequisitionCafeteria.TemplateID = 1;
                    string itemname = (((TextBox)grditem.Rows[g].FindControl("TxtItemName1")).Text);
                    string pipelenght = (((TextBox)grditem.Rows[g].FindControl("txtpopelenghtunitqty")).Text);
                    string avlstock = (((Label)grditem.Rows[g].FindControl("txtavlqty")).Text);
                    string balstock = (((Label)grditem.Rows[g].FindControl("txtbalqty")).Text);
                    string type = (((RadioButtonList)grditem.Rows[g].FindControl("rbitemtype")).SelectedValue);
                    string abc = itemname;
                    string[] tokens = abc.Split('=');
                    string itemid = (((DropDownList)grditem.Rows[g].FindControl("ddlItemDescription")).SelectedValue);
                    //Entity_RequisitionCafeteria.ItemId = itemid;
                    //Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                    //Entity_RequisitionCafeteria.IsCancel = false;
                    //// Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                    ////--Newly Added Field-- 
                    //DropDownList ddlItemDescription = (((DropDownList)grditem.Rows[g].FindControl("ddlItemDescription")));
                    //int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                    //Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                    //Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                    //Entity_RequisitionCafeteria.RemarkForPO = "";
                    string message = "";
                    int count = 0;
                    foreach (ListItem item in lstvalve.Items)
                    {
                        count++;
                        if (item.Selected)
                        {
                            message += count + ")" + item.Text;
                        }
                    }
                    #region[Convert Quantity accordng to UnitFactor]
                    //---Coversionfactor---
                    UnitConvDtlsIdT = 1;
                    ItemId = Convert.ToInt32(itemid);//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                    string unitconvrt = "Nos";
                    DataSet DsTemp = new DataSet();
                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                    if (DsTemp.Tables.Count > 0)
                    {
                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                        {
                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                            }
                            else
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text));
                            }
                        }
                    }

                    Qty = (Convert.ToDecimal(((TextBox)grditem.Rows[g].FindControl("txtbqty")).Text));
                    Entity_RequisitionCafeteria.Qty = Qty;
                    #endregion
                    // insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                    //  db.insert("insert into nonstdpipebracketindent values('" + MaxID + "','" + itemname + "' ,'" + size + "','" + schedule + "','" + Qty + "')");
                    db.insert("insert into tempnonstdpipebracketindent values('" + MaxID + "','" + itemname + "' ,'" + itemid + "','" + Qty + "','" + pipelenght + "' ,'" + avlstock + "' ,'" + balstock + "','" + type + "' ,'" + message.ToString() + "')");
                    
                }
            }
        }
        for (int g = 0; g < grdplate.Rows.Count; g++)
        {
            CheckBox chkRow = (grdplate.Rows[g].FindControl("chkbox") as CheckBox);
            if (chkRow.Checked)
            {
                if (Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text) > 0)
                {
                    //Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                    //Entity_RequisitionCafeteria.AvlQty = 0;
                    //Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text);
                    //Entity_RequisitionCafeteria.VendorId = 1;
                    //Entity_RequisitionCafeteria.Rate = 1;
                    //Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text);
                    //Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                    //Entity_RequisitionCafeteria.MaxStockLevel = 0.ToString();
                    Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                    //Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                    Entity_RequisitionCafeteria.TemplateID = 1;
                    string itemname = (((TextBox)grdplate.Rows[g].FindControl("TxtItemNameplate")).Text);
                    string areaofunitqty = (((TextBox)grdplate.Rows[g].FindControl("txtareaforunitqty")).Text);
                    string totalarea = (((TextBox)grdplate.Rows[g].FindControl("lbltotalareaplate")).Text);
                    string avlstock = (((Label)grdplate.Rows[g].FindControl("lblavlqty")).Text);
                    string balstock = (((Label)grdplate.Rows[g].FindControl("txtbalqty")).Text);
                    string type = (((RadioButtonList)grdplate.Rows[g].FindControl("rbitemtype")).SelectedValue);
                    string abc = itemname;
                    string[] tokens = abc.Split('=');
                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                    //Entity_RequisitionCafeteria.ItemId = itemid;
                    //Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                    //Entity_RequisitionCafeteria.IsCancel = false;
                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                    //--Newly Added Field-- 
                    DropDownList ddlItemDescription = (((DropDownList)grdplate.Rows[g].FindControl("ddlItemDescriptionplate")));
                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                    //Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                    //Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                    //Entity_RequisitionCafeteria.RemarkForPO = "";
                    #region[Convert Quantity accordng to UnitFactor]
                    //---Coversionfactor---
                    UnitConvDtlsIdT = 1;
                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                    string unitconvrt = "Nos";
                    DataSet DsTemp = new DataSet();
                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                    if (DsTemp.Tables.Count > 0)
                    {
                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                        {
                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                            }
                            else
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grdplate.Rows[g].FindControl("txtbqty")).Text));
                            }
                        }
                    }

                    Entity_RequisitionCafeteria.Qty = Qty;
                    #endregion
                    string message = "";
                    int count = 0;
                    foreach (ListItem item in lstvalve.Items)
                    {
                        count++;
                        if (item.Selected)
                        {
                            message += count + ")" + item.Text;
                        }
                    }
                    // insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                    // db.insert("insert into nonstdplateindent values('" + MaxID + "','" + itemname + "' ,'" + txtThickness + "','" + Qty + "')");
                    db.insert("insert into tempnonstdplateindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + areaofunitqty + "','" + totalarea + "','" + avlstock + "','" + balstock + "','" + type + "','" + message.ToString() + "')");
                    
                }
            }
        }
        for (int g = 0; g < grdadapter.Rows.Count; g++)
        {
            CheckBox chkRow = (grdadapter.Rows[g].FindControl("chkbox") as CheckBox);
            if (chkRow.Checked)
            {
                if (Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text) > 0)
                {
                    string itemname = (((TextBox)grdadapter.Rows[g].FindControl("TxtItemNameadapter")).Text);
                    string avlstock = (((Label)grdadapter.Rows[g].FindControl("lblavlqty")).Text);
                    string balstock = (((Label)grdadapter.Rows[g].FindControl("txtbalqty")).Text);
                    string type = (((RadioButtonList)grdadapter.Rows[g].FindControl("rbitemtype")).SelectedValue);
                    string abc = itemname;
                    string[] tokens = abc.Split('=');
                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                    //Entity_RequisitionCafeteria.ItemId = itemid;
                    //Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                    //Entity_RequisitionCafeteria.IsCancel = false;
                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                    //--Newly Added Field-- 
                    DropDownList ddlItemDescription = (((DropDownList)grdadapter.Rows[g].FindControl("ddlItemDescriptionadapter")));
                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                    //Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                    //Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                    //Entity_RequisitionCafeteria.RemarkForPO = "";
                    #region[Convert Quantity accordng to UnitFactor]
                    //---Coversionfactor---
                    UnitConvDtlsIdT = 1;
                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                    string unitconvrt = "Nos";
                    DataSet DsTemp = new DataSet();
                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                    if (DsTemp.Tables.Count > 0)
                    {
                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                        {
                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                            }
                            else
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grdadapter.Rows[g].FindControl("txtbqty")).Text));
                            }
                        }
                    }

                    // Entity_RequisitionCafeteria.Qty = Qty;
                    #endregion
                    string message = "";
                    int count = 0;
                    foreach (ListItem item in lstvalve.Items)
                    {
                        count++;
                        if (item.Selected)
                        {
                            message += count + ")" + item.Text;
                        }
                    }
                    //  insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                    // db.insert("insert into nonstdadapterindent values('" + MaxID + "','" + itemname + "' ,'" + txtadaptersize + "','" + Qty + "')");
                    db.insert("insert into tempnonstdadapterindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "','" + avlstock + "' ,'" + balstock + "' ,'" + type + "','" + message.ToString() + "')");
                }
            }
        }
        
        for (int g = 0; g < grdhandwheel.Rows.Count; g++)
        {
            CheckBox chkRow = (grdhandwheel.Rows[g].FindControl("chkbox") as CheckBox);
            if (chkRow.Checked)
            {
                if (Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text) > 0)
                {
                    string itemname = (((TextBox)grdhandwheel.Rows[g].FindControl("TxtItemNamehandwheel")).Text);
                    string lblavlqty = (((Label)grdhandwheel.Rows[g].FindControl("lblavlqty")).Text);
                    string txtbalqty = (((Label)grdhandwheel.Rows[g].FindControl("txtbalqty")).Text);
                    string rbitemtype = (((RadioButtonList)grdhandwheel.Rows[g].FindControl("rbitemtype")).Text);
                    string abc = itemname;
                    string[] tokens = abc.Split('=');
                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                    //Entity_RequisitionCafeteria.ItemId = itemid;
                    //Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                    //Entity_RequisitionCafeteria.IsCancel = false;
                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                    //--Newly Added Field-- 
                    DropDownList ddlItemDescription = (((DropDownList)grdhandwheel.Rows[g].FindControl("ddlItemDescriptionhandwheel")));
                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                    // Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                    //  Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                    //  Entity_RequisitionCafeteria.RemarkForPO = "";

                    #region[Convert Quantity accordng to UnitFactor]
                    //---Coversionfactor---
                    UnitConvDtlsIdT = 1;
                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                    string unitconvrt = "Nos";
                    DataSet DsTemp = new DataSet();
                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                    if (DsTemp.Tables.Count > 0)
                    {
                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                        {
                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                            }
                            else
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")).Text));
                            }
                        }
                    }

                    //Entity_RequisitionCafeteria.Qty = Qty;
                    #endregion
                    string message = "";
                    int count = 0;
                    foreach (ListItem item in lstvalve.Items)
                    {
                        count++;
                        if (item.Selected)
                        {
                            message += count + ")" + item.Text;
                        }
                    }
                    //  insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                    //   db.insert("insert into nonstdhandwheelindent values('" + MaxID + "','" + itemname + "' ,'" + txthandwheel + "','" + Qty + "')");
                    db.insert("insert into tempnonstdhandwheelindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "' ,'" + rbitemtype + "','" + message.ToString() + "')");
                }
            }
        }
        for (int g = 0; g < grdlever.Rows.Count; g++)
        {
            CheckBox chkRow = (grdlever.Rows[g].FindControl("chkbox") as CheckBox);
            if (chkRow.Checked)
            {
                if (Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text) > 0)
                {
                    string itemname = (((TextBox)grdlever.Rows[g].FindControl("TxtItemNamelever")).Text);
                    string txtlverunitqty = (((TextBox)grdlever.Rows[g].FindControl("txtlverunitqty")).Text);
                    string lbltotalleverlength = (((Label)grdlever.Rows[g].FindControl("lbltotalleverlength")).Text);
                    string lblavlqty = (((Label)grdlever.Rows[g].FindControl("lblavlqty")).Text);
                    string txtbalqty = (((Label)grdlever.Rows[g].FindControl("txtbalqty")).Text);
                    string rbitemtype = (((RadioButtonList)grdlever.Rows[g].FindControl("rbitemtype")).SelectedValue);
                    string abc = itemname;
                    string[] tokens = abc.Split('=');
                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                    DropDownList ddlItemDescription = (((DropDownList)grdlever.Rows[g].FindControl("ddlItemDescriptionlever")));
                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                    #region[Convert Quantity accordng to UnitFactor]
                    //---Coversionfactor---
                    UnitConvDtlsIdT = 1;
                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                    string unitconvrt = "Nos";
                    DataSet DsTemp = new DataSet();
                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                    if (DsTemp.Tables.Count > 0)
                    {
                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                        {
                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                            }
                            else
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grdlever.Rows[g].FindControl("txtbqty")).Text));
                            }
                        }
                    }

                    //  Entity_RequisitionCafeteria.Qty = Qty;
                    #endregion
                    string message = "";
                    int count = 0;
                    foreach (ListItem item in lstvalve.Items)
                    {
                        count++;
                        if (item.Selected)
                        {
                            message += count + ")" + item.Text;
                        }
                    }
                    db.insert("insert into tempnonstdleverindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + txtlverunitqty + "' ,'" + Qty + "' ,'" + lbltotalleverlength + "' ,'" + lblavlqty + "' ,'" + txtbalqty + "' ,'" + rbitemtype + "','" + message.ToString() + "')");
                }
            }
        }
        for (int g = 0; g < grdsschain.Rows.Count; g++)
        {
            CheckBox chkRow = (grdsschain.Rows[g].FindControl("chkbox") as CheckBox);
            if (chkRow.Checked)
            {
                if (Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text) > 0)
                {
                    string itemname = (((TextBox)grdsschain.Rows[g].FindControl("TxtItemNamesschain")).Text);
                    string avlstock = (((Label)grdsschain.Rows[g].FindControl("lblavlqty")).Text);
                    string balstock = (((Label)grdsschain.Rows[g].FindControl("lblbalqty")).Text);
                    string drpsschain = (((DropDownList)grdsschain.Rows[g].FindControl("drpsschain")).SelectedValue);
                    string abc = itemname;
                    string[] tokens = abc.Split('=');
                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                    //Entity_RequisitionCafeteria.ItemId = itemid;
                    //Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                    //Entity_RequisitionCafeteria.IsCancel = false;
                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                    //--Newly Added Field-- 
                    DropDownList ddlItemDescription = (((DropDownList)grdsschain.Rows[g].FindControl("ddlItemDescriptionsschain")));
                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                    //Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                    //Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                    //Entity_RequisitionCafeteria.RemarkForPO = "";

                    #region[Convert Quantity accordng to UnitFactor]
                    //---Coversionfactor---
                    UnitConvDtlsIdT = 1;
                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                    string unitconvrt = "Nos";
                    DataSet DsTemp = new DataSet();
                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                    if (DsTemp.Tables.Count > 0)
                    {
                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                        {
                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                            }
                            else
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grdsschain.Rows[g].FindControl("txtbqty")).Text));
                            }
                        }
                    }

                    // Entity_RequisitionCafeteria.Qty = Qty;
                    #endregion
                    //  insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                    // db.insert("insert into nonstdadapterindent values('" + MaxID + "','" + itemname + "' ,'" + txtadaptersize + "','" + Qty + "')");
                    string message = "";
                    int count = 0;
                    foreach (ListItem item in lstvalve.Items)
                    {
                        count++;
                        if (item.Selected)
                        {
                            message += count + ")" + item.Text;
                        }
                    }
                    db.insert("insert into tempnonstsschainindent values('" + MaxID + "','" + itemname + "' ,'" + itemdetailsid + "','" + Qty + "','" + avlstock + "' ,'" + balstock + "' ,'" + drpsschain + "','" + message.ToString() + "')");
                }
            }
        }
        for (int g = 0; g < grdwoodenbox.Rows.Count; g++)
        {
            CheckBox chkRow = (grdwoodenbox.Rows[g].FindControl("chkbox") as CheckBox);
            if (chkRow.Checked)
            {
                if (Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text) > 0)
                {
                    //Entity_RequisitionCafeteria.RequisitionCafeId = MaxID;
                    //Entity_RequisitionCafeteria.AvlQty = 0;
                    //Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text);
                    //Entity_RequisitionCafeteria.VendorId = 1;
                    //Entity_RequisitionCafeteria.Rate = 1;
                    //Entity_RequisitionCafeteria.TransitQty = Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text);
                    //Entity_RequisitionCafeteria.MinStockLevel = 0.ToString();
                    //Entity_RequisitionCafeteria.MaxStockLevel = 0.ToString();
                    //Entity_RequisitionCafeteria.RequiredDate = DateTime.Now;
                    ////Entity_RequisitionCafeteria.RequiredDate = (((TextBox)GrdRequisition.Rows[g].FindControl("txtRequiredDate")).Text);
                    //Entity_RequisitionCafeteria.TemplateID = 1;
                    string itemname = (((TextBox)grdwoodenbox.Rows[g].FindControl("TxtItemNamewooden")).Text);
                    string WoodenBoxSize = (((TextBox)grdwoodenbox.Rows[g].FindControl("txtwood")).Text);
                    string abc = itemname;
                    string[] tokens = abc.Split('=');
                    int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                    //Entity_RequisitionCafeteria.ItemId = itemid;
                    //Entity_RequisitionCafeteria.ExpdDate = Convert.ToDateTime(DateTime.Now.AddDays(3));
                    //Entity_RequisitionCafeteria.IsCancel = false;
                    // Entity_RequisitionCafeteria.PriorityID = Convert.ToInt32(((Label)GrdRequisition.Rows[g].FindControl("PriorityID")).Text);//Add Code on 5/1/13 for Priority
                    //--Newly Added Field-- 
                    DropDownList ddlItemDescription = (((DropDownList)grdwoodenbox.Rows[g].FindControl("ddlItemDescriptionwood")));
                    int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                    //Entity_RequisitionCafeteria.ItemDetailsId = itemdetailsid;
                    //Entity_RequisitionCafeteria.UnitConvDtlsId = 1;
                    //Entity_RequisitionCafeteria.RemarkForPO = "";
                    #region[Convert Quantity accordng to UnitFactor]
                    //---Coversionfactor---
                    UnitConvDtlsIdT = 1;
                    ItemId = itemid;//Convert.ToInt32(GrdRequisition.Rows[g].Cells[13].Text);
                    string unitconvrt = "Nos";
                    DataSet DsTemp = new DataSet();
                    DsTemp = Obj_RequisitionCafeteria.GetFactor(UnitConvDtlsIdT, ItemId, out StrError);
                    if (DsTemp.Tables.Count > 0)
                    {
                        for (int i = 0; i < DsTemp.Tables[0].Rows.Count; i++)
                        {
                            if (unitconvrt.Equals(DsTemp.Tables[0].Rows[i]["Unit"].ToString()))
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text)) / Convert.ToDecimal(DsTemp.Tables[0].Rows[i]["Factor"].ToString());
                            }
                            else
                            {
                                Qty = (Convert.ToDecimal(((TextBox)grdwoodenbox.Rows[g].FindControl("txtbqty")).Text));
                            }
                        }
                    }

                    //  Entity_RequisitionCafeteria.Qty = Qty;
                    #endregion
                    //insertdtls = Obj_RequisitionCafeteria.InsertRequisitionCafeDetails(ref Entity_RequisitionCafeteria, out StrError);
                    //db.insert("insert into nonstdwoodindent values('" + MaxID + "','" + itemname + "' ,'" + WoodenBoxSize + "','" + Qty + "')");
                    db.insert("insert into tempnonstdwoodindent values('" + MaxID + "','" + itemname + "' ,'" + WoodenBoxSize + "','" + Qty + "')");
                }
            }
        }
        Btnshow.Visible = true;
        GrdRequisition.Visible = false;
        grditem.DataSource = "";
        grditem.DataBind();
        grdplate.DataSource = "";
        grdplate.DataBind();
        grdadapter.DataSource = "";
        grdadapter.DataBind();
        grdhandwheel.DataSource = "";
        grdhandwheel.DataBind();
        grdlever.DataSource = "";
        grdlever.DataBind();
        grdsschain.DataSource = "";
        grdsschain.DataBind();
        db.insert("truncate table nonstdpipebracketdummy");
        db.insert("truncate table nonstdplatedummy");
        db.insert("truncate table nonstdadapterdummy");
        db.insert("truncate table nonstdhandwheeldummy");
        db.insert("truncate table nonstdleverdummy");
        db.insert("truncate table nonstdkeydummy");
        db.insert("truncate table nonstdwooddummy");
        flaggrditam = true;
    }

    protected void Btnshow_Click(object sender, EventArgs e)
    {
        grditem.Visible = true;
        grdplate.Visible = true;
        grdadapter.Visible = true;
        grdhandwheel.Visible = true;
        grdlever.Visible = true;
        grdwoodenbox.Visible = true;
        grditem.DataSource = db.Displaygrid("select *  from tempnonstdpipebracketindent where RequisitionCafeId='" + ViewState["maxid"].ToString() + "'");
        grditem.DataBind();
        DataSet ds = new DataSet();
        ds = db.dgv_display("select *  from tempnonstdpipebracketindent where RequisitionCafeId='" + ViewState["maxid"].ToString() + "'");
        for (int g = 0; g < ds.Tables[0].Rows.Count; g++)
        {
            CheckBox chk = (grditem.Rows[g].FindControl("chkbox") as CheckBox);
            DropDownList ddlItemDescription = grditem.Rows[g].FindControl("ddlItemDescription") as DropDownList;
            TextBox txtpopelenghtunitqty = grditem.Rows[g].FindControl("txtpopelenghtunitqty") as TextBox;
            Label txtavlqty = grditem.Rows[g].FindControl("txtavlqty") as Label;
            Label txtbalqty = grditem.Rows[g].FindControl("txtbalqty") as Label;
            RadioButtonList rbitemtype = grditem.Rows[g].FindControl("rbitemtype") as RadioButtonList;
            chk.Checked = true;
            txtpopelenghtunitqty.Text = ds.Tables[0].Rows[g]["pipelenght"].ToString();
            txtavlqty.Text = ds.Tables[0].Rows[g]["avlstock"].ToString();
            txtbalqty.Text = ds.Tables[0].Rows[g]["balstock"].ToString();
            rbitemtype.SelectedValue = ds.Tables[0].Rows[g]["type"].ToString();
            ddlItemDescription.SelectedValue = ds.Tables[0].Rows[g]["intemdetailsid"].ToString();
        }
        grdplate.DataSource = db.Displaygrid("select *  from tempnonstdplateindent where RequisitionCafeId='" + ViewState["maxid"].ToString() + "'");
        grdplate.DataBind();
        DataSet dsplate = new DataSet();
        dsplate = db.dgv_display("select *  from tempnonstdplateindent where RequisitionCafeId='" + ViewState["maxid"].ToString() + "'");
        for (int g = 0; g < dsplate.Tables[0].Rows.Count; g++)
        {
            CheckBox chk = (grdplate.Rows[g].FindControl("chkbox") as CheckBox);
            DropDownList ddlItemDescription = grdplate.Rows[g].FindControl("ddlItemDescriptionplate") as DropDownList;
            TextBox areaofunitqty = (((TextBox)grdplate.Rows[g].FindControl("txtareaforunitqty")));
            TextBox totalarea = (((TextBox)grdplate.Rows[g].FindControl("lbltotalareaplate")));
            Label avlstock = (((Label)grdplate.Rows[g].FindControl("lblavlqty")));
            Label balstock = (((Label)grdplate.Rows[g].FindControl("txtbalqty")));
            RadioButtonList type = (((RadioButtonList)grdplate.Rows[g].FindControl("rbitemtype")));
            chk.Checked = true;
            areaofunitqty.Text = dsplate.Tables[0].Rows[g]["areaofunitqty"].ToString();
            totalarea.Text = dsplate.Tables[0].Rows[g]["totalarea"].ToString();
            avlstock.Text = dsplate.Tables[0].Rows[g]["avlstock"].ToString();
            balstock.Text = dsplate.Tables[0].Rows[g]["balstock"].ToString();
            type.SelectedValue = dsplate.Tables[0].Rows[g]["type"].ToString();
            ddlItemDescription.SelectedValue = dsplate.Tables[0].Rows[g]["itemdetailsid"].ToString();
        }
        
        grdadapter.DataSource = db.Displaygrid("select *  from tempnonstdadapterindent where RequisitionCafeId='" + ViewState["maxid"].ToString() + "'");
        grdadapter.DataBind();
        DataSet dsadapter = new DataSet();
        dsadapter = db.dgv_display("select *  from tempnonstdadapterindent where RequisitionCafeId='" + ViewState["maxid"].ToString() + "'");
        for (int g = 0; g < dsadapter.Tables[0].Rows.Count; g++)
        {
            CheckBox chk = (grdadapter.Rows[g].FindControl("chkbox") as CheckBox);
            DropDownList ddlItemDescription = grdadapter.Rows[g].FindControl("ddlItemDescriptionadapter") as DropDownList;
            TextBox txtbqty = (((TextBox)grdadapter.Rows[g].FindControl("txtbqty")));
            Label avlstock = (((Label)grdadapter.Rows[g].FindControl("lblavlqty")));
            Label balstock = (((Label)grdadapter.Rows[g].FindControl("txtbalqty")));
            RadioButtonList type = (((RadioButtonList)grdadapter.Rows[g].FindControl("rbitemtype")));
            chk.Checked = true;
            avlstock.Text = dsadapter.Tables[0].Rows[g]["avlstock"].ToString();
            balstock.Text = dsadapter.Tables[0].Rows[g]["balstock"].ToString();
            type.SelectedValue = dsadapter.Tables[0].Rows[g]["type"].ToString();
            ddlItemDescription.SelectedValue = dsadapter.Tables[0].Rows[g]["itemdetailsid"].ToString();
        }
        grdhandwheel.DataSource = db.Displaygrid("select *  from tempnonstdhandwheelindent where RequisitionCafeId='" + ViewState["maxid"].ToString() + "'");
        grdhandwheel.DataBind();
        DataSet dshandwhhel = new DataSet();
        dshandwhhel = db.dgv_display("select *  from tempnonstdhandwheelindent where RequisitionCafeId='" + ViewState["maxid"].ToString() + "'");
        for (int g = 0; g < dshandwhhel.Tables[0].Rows.Count; g++)
        {
            CheckBox chk = (grdhandwheel.Rows[g].FindControl("chkbox") as CheckBox);
            DropDownList ddlItemDescription = grdhandwheel.Rows[g].FindControl("ddlItemDescriptionhandwheel") as DropDownList;
            TextBox txtbqty = (((TextBox)grdhandwheel.Rows[g].FindControl("txtbqty")));
            Label avlstock = (((Label)grdhandwheel.Rows[g].FindControl("lblavlqty")));
            Label balstock = (((Label)grdhandwheel.Rows[g].FindControl("txtbalqty")));
            RadioButtonList type = (((RadioButtonList)grdhandwheel.Rows[g].FindControl("rbitemtype")));
            chk.Checked = true;
            avlstock.Text = dshandwhhel.Tables[0].Rows[g]["avlstock"].ToString();
            balstock.Text = dshandwhhel.Tables[0].Rows[g]["balstock"].ToString();
            type.SelectedValue = dshandwhhel.Tables[0].Rows[g]["type"].ToString();
            ddlItemDescription.SelectedValue = dshandwhhel.Tables[0].Rows[g]["itemdetailsid"].ToString();
        }
        grdlever.DataSource = db.Displaygrid("select *  from tempnonstdleverindent where RequisitionCafeId='" + ViewState["maxid"].ToString() + "'");
        grdlever.DataBind();
        DataSet dsleverl = new DataSet();
        dsleverl = db.dgv_display("select *  from tempnonstdleverindent where RequisitionCafeId='" + ViewState["maxid"].ToString() + "'");
        for (int g = 0; g < dsleverl.Tables[0].Rows.Count; g++)
        {
            CheckBox chk = (grdlever.Rows[g].FindControl("chkbox") as CheckBox);
            DropDownList ddlItemDescription = grdlever.Rows[g].FindControl("ddlItemDescriptionlever") as DropDownList;
            TextBox txtlverunitqty = (((TextBox)grdlever.Rows[g].FindControl("txtlverunitqty")));
            Label lbltotalleverlength = (((Label)grdlever.Rows[g].FindControl("lbltotalleverlength")));
            Label avlstock = (((Label)grdlever.Rows[g].FindControl("lblavlqty")));
            Label balstock = (((Label)grdlever.Rows[g].FindControl("txtbalqty")));
            RadioButtonList type = (((RadioButtonList)grdlever.Rows[g].FindControl("rbitemtype")));
            chk.Checked = true;
            avlstock.Text = dsleverl.Tables[0].Rows[g]["avlstock"].ToString();
            balstock.Text = dsleverl.Tables[0].Rows[g]["balstock"].ToString();
            type.SelectedValue = dsleverl.Tables[0].Rows[g]["type"].ToString();
            ddlItemDescription.SelectedValue = dsleverl.Tables[0].Rows[g]["itemdetailsid"].ToString();
            lbltotalleverlength.Text = dsleverl.Tables[0].Rows[g]["TotalLeverLength"].ToString();
            txtlverunitqty.Text = dsleverl.Tables[0].Rows[g]["LeverlengtforunitQty"].ToString();
        }
        grdsschain.DataSource = db.Displaygrid("select *  from tempnonstsschainindent where RequisitionCafeId='" + ViewState["maxid"].ToString() + "'");
        grdsschain.DataBind();
        DataSet dsSSCHAIN = new DataSet();
        dsSSCHAIN = db.dgv_display("select *  from tempnonstsschainindent where RequisitionCafeId='" + ViewState["maxid"].ToString() + "'");
        for (int g = 0; g < dsSSCHAIN.Tables[0].Rows.Count; g++)
        {
            CheckBox chk = (grdsschain.Rows[g].FindControl("chkbox") as CheckBox);
            DropDownList ddlItemDescription = grdsschain.Rows[g].FindControl("ddlItemDescriptionsschain") as DropDownList;
            DropDownList drpsschain = grdsschain.Rows[g].FindControl("drpsschain") as DropDownList;
            Label avlstock = (((Label)grdsschain.Rows[g].FindControl("lblavlqty")));
            Label balstock = (((Label)grdsschain.Rows[g].FindControl("lblbalqty")));
            chk.Checked = true;
            avlstock.Text = dsSSCHAIN.Tables[0].Rows[g]["avlstock"].ToString();
            balstock.Text = dsSSCHAIN.Tables[0].Rows[g]["balstock"].ToString();
            drpsschain.SelectedValue = dsSSCHAIN.Tables[0].Rows[g]["unit"].ToString();
            ddlItemDescription.SelectedValue = dsSSCHAIN.Tables[0].Rows[g]["itemdetailsid"].ToString();
        }
        
    }

    protected void ddlItemDescription_SelectedIndexChanged1(object sender, EventArgs e)
    {
        for (int g = 0; g < grditem.Rows.Count; g++)
        {
            DropDownList ddlItemDescription = (grditem.Rows[g].FindControl("ddlItemDescription") as DropDownList);
            float avlqty = db.getDb_Value("select OpeningStock  as avlqty from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescription.SelectedValue + "' ");
            float conversion = db.getDb_Value("select toqty  as avlqty from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescription.SelectedValue + "' ");
            Label txtavlqty = (grditem.Rows[g].FindControl("txtavlqty") as Label);
            float c = avlqty * conversion;
            txtavlqty.Text = c.ToString();
        }
    }

    protected void ddlItemDescriptionplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < grdplate.Rows.Count; g++)
        {
        }
    }

    protected void ddlItemDescriptionadapter_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < grdadapter.Rows.Count; g++)
        {
            DropDownList ddlItemDescriptionadapter = (grdadapter.Rows[g].FindControl("ddlItemDescriptionadapter") as DropDownList);
            float avlqty = db.getDb_Value("select OpeningStock  as avlqty from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptionadapter.SelectedValue + "' ");
            Label lblavlqty = (grdadapter.Rows[g].FindControl("lblavlqty") as Label);
            lblavlqty.Text = avlqty.ToString();
        }
    }
    protected void btnaddtogrid_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (ListItem item in lstvalve.Items)
        {
            if (item.Selected)
            {
                count++;
            }
        }
        string xyz = lstvalve.SelectedItem.ToString();
        // string[] ssize = drp.Split(new char[0]);
        string[] tokens1 = xyz.Split('-');
        string interlock = tokens1[4].ToString();
        if (interlock == "DCM" || interlock == "DCS")
        {
            lblgrditem.Text = "Arm assembly Pipe/Rod";
            lblgrdplate.Text = "Arm assembly Plate & Support Plate";
            dvadpater.Visible = false;
            dvhook.Visible = true;
            dvhandwheel.Visible = false;
            dvlever.Visible = false;
            string drp = lstvalve.SelectedItem.ToString();
            // string[] ssize = drp.Split(new char[0]);
            string[] tokens = drp.Split('-');
            int a = Convert.ToInt32(db.getDb_Value("select TemplateID   from TemplateMaster  where TemplateName='" + tokens[4] + "'"));
            SqlCommand cmd = new SqlCommand("     select nonstdpipebracket.itemname ,nonstdpipebracket.shecdule  , nonstdpipebracket.size from nonstdpipebracket inner join  Nonstandaredmaster on nonstdpipebracket.iterlockid=Nonstandaredmaster.interlock where nonstdpipebracket.iterlockid='" + a + "'");
            cmd.Connection = cn;
            cn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string itemname = rdr["itemname"].ToString();
                string shecdule = rdr["shecdule"].ToString();
                string size = rdr["size"].ToString();
                db.insert("insert into nonstdpipebracketdummy values('" + itemname + "' ,'" + size + "' ,'" + shecdule + "' )");
            }
            rdr.Close();
            cn.Close();
            grditem.DataSource = db.Displaygrid("select *   , 0 as qty from nonstdpipebracketdummy");
            grditem.DataBind();
            SqlCommand cmdplate = new SqlCommand("   select nonstdplate.itemname ,nonstdplate.Thickness  from nonstdplate inner join  Nonstandaredmaster on nonstdplate.iterlockid=Nonstandaredmaster.interlock  where nonstdplate.iterlockid='" + a + "'");
            cmdplate.Connection = cn;
            cn.Open();
            SqlDataReader rdrplate = cmdplate.ExecuteReader();
            while (rdrplate.Read())
            {
                string itemname = rdrplate["itemname"].ToString();
                string Thickness = rdrplate["Thickness"].ToString();
                db.insert("insert into nonstdplatedummy values('" + itemname + "' ,'" + Thickness + "' )");
            }
            cn.Close();
            grdplate.DataSource = db.Displaygrid("select *  , 0 as qty  from nonstdplatedummy");
            grdplate.DataBind();
            SetInitialRow_grdsschain();
        }

        else
        {
            lblgrdplate.Text = "Plate, U-shape, L-shape  Bracket";
            lblgrditem.Text = "Pipe Bracket";
            dvadpater.Visible = true;
            dvhook.Visible = false;
            dvhandwheel.Visible = true;
            dvlever.Visible = true;
            string drp = lstvalve.SelectedItem.ToString();
            // string[] ssize = drp.Split(new char[0]);
            string[] tokens = drp.Split('-');
            int a = Convert.ToInt32(db.getDb_Value("select TemplateID   from TemplateMaster  where TemplateName='" + tokens[4] + "'"));
            SqlCommand cmd = new SqlCommand("     select nonstdpipebracket.itemname ,nonstdpipebracket.shecdule  , nonstdpipebracket.size from nonstdpipebracket inner join  Nonstandaredmaster on nonstdpipebracket.iterlockid=Nonstandaredmaster.interlock where nonstdpipebracket.iterlockid='" + a + "'");
            cmd.Connection = cn;
            cn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string itemname = rdr["itemname"].ToString();
                string shecdule = rdr["shecdule"].ToString();
                string size = rdr["size"].ToString();
                db.insert("insert into nonstdpipebracketdummy values('" + itemname + "' ,'" + size + "' ,'" + shecdule + "' )");
            }
            rdr.Close();
            cn.Close();
            grditem.DataSource = db.Displaygrid("select *   , 0 as qty from nonstdpipebracketdummy");
            grditem.DataBind();
            SqlCommand cmdplate = new SqlCommand("   select nonstdplate.itemname ,nonstdplate.Thickness  from nonstdplate inner join  Nonstandaredmaster on nonstdplate.iterlockid=Nonstandaredmaster.interlock  where nonstdplate.iterlockid='" + a + "'");
            cmdplate.Connection = cn;
            cn.Open();
            SqlDataReader rdrplate = cmdplate.ExecuteReader();
            while (rdrplate.Read())
            {
                string itemname = rdrplate["itemname"].ToString();
                string Thickness = rdrplate["Thickness"].ToString();
                db.insert("insert into nonstdplatedummy values('" + itemname + "' ,'" + Thickness + "' )");
            }
            cn.Close();
            grdplate.DataSource = db.Displaygrid("select *  , 0 as qty  from nonstdplatedummy");
            grdplate.DataBind();
            SqlCommand cmdadapter = new SqlCommand("   select nonstdadpter.itemname ,nonstdadpter.AdaptorSizes  from nonstdadpter inner join  Nonstandaredmaster on nonstdadpter.iterlockid=Nonstandaredmaster.interlock  where nonstdadpter.iterlockid='" + a + "'");
            cmdadapter.Connection = cn;
            cn.Open();
            SqlDataReader rdradapter = cmdadapter.ExecuteReader();
            while (rdradapter.Read())
            {
                string itemname = rdradapter["itemname"].ToString();
                string AdaptorSizes1 = rdradapter["AdaptorSizes"].ToString();
                db.insert("insert into nonstdadapterdummy values('" + itemname + "' ,'" + AdaptorSizes1 + "' )");
            }
            cn.Close();
            grdadapter.DataSource = db.Displaygrid("select *  , 0  as qty  from nonstdadapterdummy");
            grdadapter.DataBind();
            SqlCommand cmdhandwheel = new SqlCommand("   select nonstdhandwheel.itemname ,nonstdhandwheel.HandwheelSizes  from nonstdhandwheel inner join  Nonstandaredmaster on nonstdhandwheel.iterlockid=Nonstandaredmaster.interlock  where nonstdhandwheel.iterlockid='" + a + "'");
            cmdhandwheel.Connection = cn;
            cn.Open();
            SqlDataReader rdrhandwheel = cmdhandwheel.ExecuteReader();
            while (rdrhandwheel.Read())
            {
                string itemname = rdrhandwheel["itemname"].ToString();
                string HandwheelSizes = rdrhandwheel["HandwheelSizes"].ToString();
                db.insert("insert into nonstdhandwheeldummy values('" + itemname + "' ,'" + HandwheelSizes + "' )");
            }
            cn.Close();
            grdhandwheel.DataSource = db.Displaygrid("select *  , 0  as qty  from nonstdhandwheeldummy");
            grdhandwheel.DataBind();
            SqlCommand cmdlever = new SqlCommand("   select nonstdlever.itemname ,nonstdlever.LeverSizes  from nonstdlever inner join  Nonstandaredmaster on nonstdlever.iterlockid=Nonstandaredmaster.interlock  where nonstdlever.iterlockid='" + a + "'");
            cmdlever.Connection = cn;
            cn.Open();
            SqlDataReader rdrlever = cmdlever.ExecuteReader();
            while (rdrlever.Read())
            {

                string itemname = rdrlever["itemname"].ToString();
                string LeverSizes = rdrlever["LeverSizes"].ToString();
                db.insert("insert into nonstdleverdummy values('" + itemname + "' ,'" + LeverSizes + "' )");                
            }
            cn.Close();
            grdlever.DataSource = db.Displaygrid("select *   , 0  as qty from nonstdleverdummy");
            grdlever.DataBind();
        }

    }

    protected void ddlItemDescriptionhandwheel_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < grdhandwheel.Rows.Count; g++)
        {
            DropDownList ddlItemDescriptionhandwheel = (grdhandwheel.Rows[g].FindControl("ddlItemDescriptionhandwheel") as DropDownList);
            float avlqty = db.getDb_Value("select OpeningStock  as avlqty from ItemDetails  where ItemDetailsId='" + ddlItemDescriptionhandwheel.SelectedValue + "' ");
            Label lblavlqty = (grdhandwheel.Rows[g].FindControl("lblavlqty") as Label);
            lblavlqty.Text = avlqty.ToString();
        }
    }

    protected void ddlItemDescriptionlever_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < grdlever.Rows.Count; g++)
        {
            DropDownList ddlItemDescriptionlever = (grdlever.Rows[g].FindControl("ddlItemDescriptionlever") as DropDownList);
            float avlqty = db.getDb_Value("select OpeningStock  as avlqty from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptionlever.SelectedValue + "' ");
            float con = db.getDb_Value("select ToQty   from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptionlever.SelectedValue + "' ");
            float c = avlqty * con;
            Label lblavlqty = (grdlever.Rows[g].FindControl("lblavlqty") as Label);
            lblavlqty.Text = c.ToString();
        }
    }

    protected void txtlverunitqty_TextChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < grdlever.Rows.Count; g++)
        {
            CheckBox chk = (grdlever.Rows[g].FindControl("chkbox") as CheckBox);
            if (chk.Checked == true)
            {
                DropDownList ddlItemDescriptionlever = (grdlever.Rows[g].FindControl("ddlItemDescriptionlever") as DropDownList);
                float avlqty = db.getDb_Value("select OpeningStock  as avlqty from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptionlever.SelectedValue + "' ");
                float con = db.getDb_Value("select ToQty   from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptionlever.SelectedValue + "' ");
                float m = avlqty * con;
                Label lblavlqty = (grdlever.Rows[g].FindControl("lblavlqty") as Label);
                lblavlqty.Text = m.ToString();
                TextBox txtlverunitqty = (grdlever.Rows[g].FindControl("txtlverunitqty") as TextBox);
                TextBox txtbqty = (grdlever.Rows[g].FindControl("txtbqty") as TextBox);
                Label lbltotalleverlength = (grdlever.Rows[g].FindControl("lbltotalleverlength") as Label);
                Label txtbalqty = (grdlever.Rows[g].FindControl("txtbalqty") as Label);
                float a = float.Parse(txtlverunitqty.Text);
                float b = float.Parse(txtbqty.Text);
                float c = 0;
                c = (a * b);
                float d = c / 1000;
                lbltotalleverlength.Text = d.ToString();
                float x = float.Parse(lblavlqty.Text);
                float z = 0;
                z = x - d;
                txtbalqty.Text = z.ToString();
                db.insert("update ItemDetailsindent  set  OpeningStock='" + txtbalqty.Text + "' where  ItemDetailsId='" + ddlItemDescriptionlever.SelectedValue + "'");
            }
        }
    }

    protected void grdtagplate_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = Convert.ToInt32(grdplate.DataKeys[e.RowIndex].ToString());
    }

    protected void grdtagplate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Ds = new DataSet();
            TextBox TxtItemName = e.Row.FindControl("TxtItemNametagplate") as TextBox;
            if (TxtItemName.Text != null)
            {
                string abc = TxtItemName.Text;
                string[] tokens = abc.Split('=');
                int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(itemid), out StrError);
                DropDownList ddlItemDescription = e.Row.FindControl("ddlItemDescriptiontagplate") as DropDownList;
                ddlItemDescription.DataSource = Ds.Tables[2];
                ddlItemDescription.DataValueField = "#";
                ddlItemDescription.DataTextField = "ItemDesc";
                ddlItemDescription.DataBind();
            }
        }
    }

    protected void grdtagplate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdtagplate.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {

            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                string fileName;
                fileName = FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                string abc = "~/ScannedDrawings/" + fileName;
                // ItemDetailsId
                // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptiontagplate") as DropDownList;
                int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                Session["templateid1"] = itemdetailsid.ToString();
            }
        }

        if (e.CommandName == "Selectmulti")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdtagplate.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {

            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                foreach (GridViewRow rows in grdtagplate.Rows)
                {
                    if (((CheckBox)row.FindControl("chkbox")).Checked)
                    {
                        string fileName;
                        fileName = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                        string abc = "~/ScannedDrawings/" + fileName;
                        // ItemDetailsId
                        // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                        DropDownList ddlItemDescription = rows.FindControl("ddlItemDescriptiontagplate") as DropDownList;
                        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                        db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                        Session["templateid1"] = itemdetailsid.ToString();
                    }
                }               
            }
        }
    }

    protected void btnviewtagplate_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int rowindex = gvr.RowIndex;
        GridViewRow row = grdtagplate.Rows[rowindex];
        DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptiontagplate") as DropDownList;
        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
        Session["templateid1"] = itemdetailsid.ToString();
        string a = "MaterialRequisitionTemplate12";
        string url = "Drawing.aspx";
        Response.Redirect("../Reports/Drawing.aspx?RowIndex=" + rowindex + "&FormName=" + a);
    }

    protected void btnviewtagnonstdhardware_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int rowindex = gvr.RowIndex;
        GridViewRow row = grdnonstdhardware.Rows[rowindex];
        DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptiontagnonstdhardware") as DropDownList;
        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
        Session["templateid1"] = itemdetailsid.ToString();
        string a = "MaterialRequisitionTemplate12";
        string url = "Drawing.aspx";
        Response.Redirect("../Reports/Drawing.aspx?RowIndex=" + rowindex + "&FormName=" + a);
    }

    protected void grdnonstdhardware_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = Convert.ToInt32(grdnonstdhardware.DataKeys[e.RowIndex].ToString());
    }

    protected void grdnonstdhardware_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Ds = new DataSet();
            TextBox TxtItemName = e.Row.FindControl("TxtItemNamenonstdhardware") as TextBox;
            if (TxtItemName.Text != null)
            {
                string abc = TxtItemName.Text;
                string[] tokens = abc.Split('=');
                int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(itemid), out StrError);
                DropDownList ddlItemDescription = e.Row.FindControl("ddlItemDescriptiontagnonstdhardware") as DropDownList;
                ddlItemDescription.DataSource = Ds.Tables[2];
                ddlItemDescription.DataValueField = "#";
                ddlItemDescription.DataTextField = "ItemDesc";
                ddlItemDescription.DataBind();
            }
        }
    }

    protected void grdnonstdhardware_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdnonstdhardware.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {

            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                string fileName;
                fileName = FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                string abc = "~/ScannedDrawings/" + fileName;
                // ItemDetailsId
                // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                DropDownList ddlItemDescription = row.FindControl("ddlItemDescriptiontagnonstdhardware") as DropDownList;
                int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                Session["templateid1"] = itemdetailsid.ToString();
            }
        }

        if (e.CommandName == "Selectmulti")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdnonstdhardware.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {

            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                foreach (GridViewRow rows in grdnonstdhardware.Rows)
                {
                    if (((CheckBox)row.FindControl("chkbox")).Checked)
                    {
                        string fileName;
                        fileName = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));
                        string abc = "~/ScannedDrawings/" + fileName;
                        // ItemDetailsId
                        // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();
                        DropDownList ddlItemDescription = rows.FindControl("ddlItemDescriptiontagnonstdhardware") as DropDownList;
                        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                        db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");
                        Session["templateid1"] = itemdetailsid.ToString();
                    }
                }
            }
        }
    }

    protected void ddlItemDescriptiontagnonstdhardware_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < grdnonstdhardware.Rows.Count; g++)
        {
            DropDownList ddlItemDescriptiontagnonstdhardware = (grdnonstdhardware.Rows[g].FindControl("ddlItemDescriptiontagnonstdhardware") as DropDownList);
            float avlqty = db.getDb_Value("select OpeningStock  as avlqty from ItemDetails  where ItemDetailsId='" + ddlItemDescriptiontagnonstdhardware.SelectedValue + "' ");
            Label lblavlqty = (grdnonstdhardware.Rows[g].FindControl("lblavlqty") as Label);
            lblavlqty.Text = avlqty.ToString();
        }
    }
    protected void txtpopelenghtunitqty_TextChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < grditem.Rows.Count; g++)
        {
            CheckBox chk = (grditem.Rows[g].FindControl("chkbox") as CheckBox);
            if (chk.Checked == true)
            {
                DropDownList itemdetailsid = (grditem.Rows[g].FindControl("ddlItemDescription") as DropDownList);
                TextBox txtpopelenghtunitqty = (grditem.Rows[g].FindControl("txtpopelenghtunitqty") as TextBox);
                TextBox txtbqty = (grditem.Rows[g].FindControl("txtbqty") as TextBox);
                Label txtavlqty = (grditem.Rows[g].FindControl("txtavlqty") as Label);
                Label txtbalqty = (grditem.Rows[g].FindControl("txtbalqty") as Label);
                float a = float.Parse(txtpopelenghtunitqty.Text);
                float b = float.Parse(txtbqty.Text);
                float c = 0;
                c = (a * b);
                float d = float.Parse(txtavlqty.Text);
                float f = d - c;
                txtbalqty.Text = f.ToString();
                db.insert("update ItemDetailsindent set  OpeningStock='" + txtbalqty.Text + "' where  ItemDetailsId='" + itemdetailsid.SelectedValue + "'");
            }
        }
    }

    protected void ButtonAdd123_Click(object sender, EventArgs e)
    {
        AddNewRowToGriditem();
    }
    private void AddNewRowToGriditem()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTablenonstdhardware"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    // AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");
                    TextBox txtqty = (TextBox)grdnonstdhardware.Rows[rowIndex].Cells[1].FindControl("txtbqty");
                    TextBox TxtItemName = (TextBox)grdnonstdhardware.Rows[rowIndex].Cells[0].FindControl("TxtItemNamenonstdhardware");
                    drCurrentRow = dtCurrentTable.NewRow();
                    dtCurrentTable.Rows[i - 1]["ItemName"] = TxtItemName.Text;
                    dtCurrentTable.Rows[i - 1]["qty"] = txtqty.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTablenonstdhardware"] = dtCurrentTable;
                grdnonstdhardware.DataSource = dtCurrentTable;
                grdnonstdhardware.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks
        SetPreviousDataitem();
    }
    private void AddNewRowToGridsschain()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTablesschain"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    // AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");
                    TextBox txtqty = (TextBox)grdsschain.Rows[rowIndex].Cells[1].FindControl("txtbqty");
                    TextBox TxtItemName = (TextBox)grdsschain.Rows[rowIndex].Cells[0].FindControl("TxtItemNamesschain");
                    DropDownList ddlItemDescriptionsschain = (DropDownList)grdsschain.Rows[rowIndex].Cells[0].FindControl("ddlItemDescriptionsschain");
                    Label lblavlqty = (Label)grdsschain.Rows[rowIndex].Cells[0].FindControl("lblavlqty");
                    Label lblbalqty = (Label)grdsschain.Rows[rowIndex].Cells[0].FindControl("lblbalqty");
                    DropDownList drpsschain = (DropDownList)grdsschain.Rows[rowIndex].Cells[0].FindControl("drpsschain");
                    drCurrentRow = dtCurrentTable.NewRow();
                    dtCurrentTable.Rows[i - 1]["ItemName"] = TxtItemName.Text;
                    dtCurrentTable.Rows[i - 1]["qty"] = txtqty.Text;
                    dtCurrentTable.Rows[i - 1]["itemid"] = ddlItemDescriptionsschain.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["avlqty"] = lblavlqty.Text;
                    dtCurrentTable.Rows[i - 1]["balqty"] = lblbalqty.Text;
                    dtCurrentTable.Rows[i - 1]["unit"] = drpsschain.SelectedValue;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTablesschain"] = dtCurrentTable;
                grdsschain.DataSource = dtCurrentTable;
                grdsschain.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks
        SetPreviousDatasschain();
    }
    private void SetPreviousDatasschain()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTablesschain"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //   AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");
                    TextBox txtqty = (TextBox)grdsschain.Rows[rowIndex].Cells[1].FindControl("txtbqty");
                    TextBox TxtItemNamenonstdhardware = (TextBox)grdsschain.Rows[rowIndex].Cells[3].FindControl("TxtItemNamesschain");
                    DropDownList ddlItemDescriptionsschain = (DropDownList)grdsschain.Rows[rowIndex].Cells[0].FindControl("ddlItemDescriptionsschain");
                    Label lblavlqty = (Label)grdsschain.Rows[rowIndex].Cells[0].FindControl("lblavlqty");
                    Label lblbalqty = (Label)grdsschain.Rows[rowIndex].Cells[0].FindControl("lblbalqty");
                    DropDownList drpsschain = (DropDownList)grdsschain.Rows[rowIndex].Cells[0].FindControl("drpsschain");
                    TxtItemNamenonstdhardware.Text = dt.Rows[i]["ItemName"].ToString();
                    txtqty.Text = dt.Rows[i]["qty"].ToString();
                    ddlItemDescriptionsschain.SelectedValue = dt.Rows[i]["itemid"].ToString();
                    lblavlqty.Text = dt.Rows[i]["avlqty"].ToString();
                    lblbalqty.Text = dt.Rows[i]["balqty"].ToString();
                    drpsschain.SelectedValue = dt.Rows[i]["unit"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    private void SetPreviousDataitem()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTablenonstdhardware"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //   AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");
                    TextBox txtqty = (TextBox)grdnonstdhardware.Rows[rowIndex].Cells[1].FindControl("txtbqty");
                    TextBox TxtItemNamenonstdhardware = (TextBox)grdnonstdhardware.Rows[rowIndex].Cells[3].FindControl("TxtItemNamenonstdhardware");
                    TxtItemNamenonstdhardware.Text = dt.Rows[i]["ItemName"].ToString();
                    txtqty.Text = dt.Rows[i]["qty"].ToString();
                    rowIndex++;

                }
            }
        }
    }
    
    public void SetInitialRow_grdnonstdhardware()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("qty", typeof(string));
            dt.Columns.Add("unitprice", typeof(string));
            dt.Columns.Add("total", typeof(string));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["ItemName"] = "";
            dr["qty"] = "";
            dt.Rows.Add(dr);
            ViewState["CurrentTablenonstdhardware"] = dt;
            grdnonstdhardware.DataSource = dt;
            grdnonstdhardware.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void SetInitialRow_grdsschain()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("qty", typeof(string));
            dt.Columns.Add("itemid", typeof(string));
            dt.Columns.Add("avlqty", typeof(string));
            dt.Columns.Add("balqty", typeof(string));
            dt.Columns.Add("unit", typeof(string));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;

            dr["ItemName"] = "";
            dr["qty"] = "";
            dr["itemid"] = "";
            dr["avlqty"] = "";
            dr["balqty"] = "";
            dr["unit"] = "";

            dt.Rows.Add(dr);
            ViewState["CurrentTablesschain"] = dt;
            grdsschain.DataSource = dt;
            grdsschain.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    
    protected void ddlItemnonstdhardware_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void TxtItemNamenonstdhardware_TextChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < grdnonstdhardware.Rows.Count; g++)
        {
            Ds = new DataSet();

            TextBox TxtItemName = grdnonstdhardware.Rows[g].FindControl("TxtItemNamenonstdhardware") as TextBox;

            if (TxtItemName.Text != null)
            {
                string abc = TxtItemName.Text;
                string[] tokens = abc.Split('=');
                int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(itemid), out StrError);
                DropDownList ddlItemDescription = grdnonstdhardware.Rows[g].FindControl("ddlItemDescriptiontagnonstdhardware") as DropDownList;
                ddlItemDescription.DataSource = Ds.Tables[2];
                ddlItemDescription.DataValueField = "#";
                ddlItemDescription.DataTextField = "ItemDesc";
                ddlItemDescription.DataBind();
            }
        }
    }

    protected void txtareaforunitqty_TextChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < grdplate.Rows.Count; g++)
        {

            CheckBox chk = (grdplate.Rows[g].FindControl("chkbox") as CheckBox);
            if (chk.Checked == true)
            {
                DropDownList ddlItemDescriptionplate = (grdplate.Rows[g].FindControl("ddlItemDescriptionplate") as DropDownList);
                float avlqty = db.getDb_Value("select OpeningStock  as avlqty from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptionplate.SelectedValue + "' ");
                float conversion = db.getDb_Value("select toqty  as avlqty from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptionplate.SelectedValue + "' ");
                Label txtavlqty = (grdplate.Rows[g].FindControl("lblavlqty") as Label);
                float m = avlqty * conversion;
                txtavlqty.Text = m.ToString();


                TextBox txtareaforunitqty = (grdplate.Rows[g].FindControl("txtareaforunitqty") as TextBox);
                TextBox txtbqty = (grdplate.Rows[g].FindControl("txtbqty") as TextBox);
                Label lblavlqty = (grdplate.Rows[g].FindControl("lblavlqty") as Label);
                Label txtbalqty = (grdplate.Rows[g].FindControl("txtbalqty") as Label);
                TextBox lbltotalareaplate = (grdplate.Rows[g].FindControl("lbltotalareaplate") as TextBox);



                float a = float.Parse(txtareaforunitqty.Text);
                float b = float.Parse(txtbqty.Text);
                float c = 0;


                c = (a * b);
                lbltotalareaplate.Text = c.ToString();
                float d = float.Parse(lblavlqty.Text);
                float f = d - c;
                txtbalqty.Text = f.ToString();


                db.insert("update ItemDetailsindent set  OpeningStock ='" + txtbalqty.Text + "' where     ItemDetailsId='" + ddlItemDescriptionplate.SelectedValue + "'");

            }
        }
    }

    protected void txtbqty_TextChanged(object sender, EventArgs e)
    {


        for (int g = 0; g < grdadapter.Rows.Count; g++)
        {

            CheckBox chk = (grdadapter.Rows[g].FindControl("chkbox") as CheckBox);
            if (chk.Checked == true)
            {



                DropDownList ddlItemDescriptionadapter = (grdadapter.Rows[g].FindControl("ddlItemDescriptionadapter") as DropDownList);


                float avlqty = db.getDb_Value("select OpeningStock  as avlqty from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptionadapter.SelectedValue + "' ");
                Label lblavlqty = (grdadapter.Rows[g].FindControl("lblavlqty") as Label);
                lblavlqty.Text = avlqty.ToString();
                TextBox txtbqty = (grdadapter.Rows[g].FindControl("txtbqty") as TextBox);

                Label txtbalqty = (grdadapter.Rows[g].FindControl("txtbalqty") as Label);




                float b = float.Parse(txtbqty.Text);
                float a = float.Parse(lblavlqty.Text);
                float c = 0;

                c = a - b;

                txtbalqty.Text = c.ToString();

                db.insert("update  ItemDetailsindent set OpeningStock='" + txtbalqty.Text + "' where ItemDetailsId='" + ddlItemDescriptionadapter.SelectedValue + "'");

            }
        }
    }

    protected void txtbqty_TextChanged1(object sender, EventArgs e)
    {
        for (int g = 0; g < grdhandwheel.Rows.Count; g++)
        {

            CheckBox chk = (grdhandwheel.Rows[g].FindControl("chkbox") as CheckBox);
            if (chk.Checked == true)
            {


                DropDownList ddlItemDescriptionhandwheel = (grdhandwheel.Rows[g].FindControl("ddlItemDescriptionhandwheel") as DropDownList);



                float avlqty = db.getDb_Value("select OpeningStock  as avlqty from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptionhandwheel.SelectedValue + "' ");
                Label lblavlqty = (grdhandwheel.Rows[g].FindControl("lblavlqty") as Label);
                lblavlqty.Text = avlqty.ToString();

                TextBox txtbqty = (grdhandwheel.Rows[g].FindControl("txtbqty") as TextBox);

                Label txtbalqty = (grdhandwheel.Rows[g].FindControl("txtbalqty") as Label);




                float b = float.Parse(txtbqty.Text);
                float a = float.Parse(lblavlqty.Text);
                float c = 0;

                c = a - b;

                txtbalqty.Text = c.ToString();

                db.insert("update ItemDetailsindent set  OpeningStock='" + txtbalqty.Text + "' where ItemDetailsId ='" + ddlItemDescriptionhandwheel.SelectedValue + "'");
            }
        }
    }

    protected void ddlItemDescriptionsschain_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < grdsschain.Rows.Count; g++)
        {

            DropDownList ddlItemDescriptiontagnonstdhardware = (grdsschain.Rows[g].FindControl("ddlItemDescriptionsschain") as DropDownList);
            CheckBox chkbox = (grdsschain.Rows[g].FindControl("chkbox") as CheckBox);

            if (chkbox.Checked == true)


            {


                float avlqty = db.getDb_Value("select OpeningStock  as avlqty from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptiontagnonstdhardware.SelectedValue + "' ");
                Label lblavlqty = (grdsschain.Rows[g].FindControl("lblavlqty") as Label);
                lblavlqty.Text = avlqty.ToString();

            }
        }
    }

    protected void TxtItemNamesschain_TextChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < grdsschain.Rows.Count; g++)
        {
            Ds = new DataSet();

            TextBox TxtItemName = grdsschain.Rows[g].FindControl("TxtItemNamesschain") as TextBox;

            if (TxtItemName.Text != null)
            {
                string abc = TxtItemName.Text;
                string[] tokens = abc.Split('=');
                int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(itemid), out StrError);


                DropDownList ddlItemDescription = grdsschain.Rows[g].FindControl("ddlItemDescriptionsschain") as DropDownList;


                ddlItemDescription.DataSource = Ds.Tables[2];

                ddlItemDescription.DataValueField = "#";
                ddlItemDescription.DataTextField = "ItemDesc";
                ddlItemDescription.DataBind();
            }
        }
    }

    protected void ddlItemsschain_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grdsschain_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = Convert.ToInt32(grdsschain.DataKeys[e.RowIndex].ToString());
    }

    protected void grdsschain_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Ds = new DataSet();

            TextBox TxtItemName = e.Row.FindControl("TxtItemNamesschain") as TextBox;

            if (TxtItemName.Text != null && TxtItemName.Text != "")
            {
                string abc = TxtItemName.Text;
                string[] tokens = abc.Split('=');
                int itemid = Convert.ToInt32(db.getDb_Value("select  ItemId from  ItemMaster  where ItemName='" + tokens[0] + "'"));
                Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(itemid), out StrError);


                DropDownList ddlItemDescription = e.Row.FindControl("ddlItemDescriptionsschain") as DropDownList;


                ddlItemDescription.DataSource = Ds.Tables[2];

                ddlItemDescription.DataValueField = "#";
                ddlItemDescription.DataTextField = "ItemDesc";
                ddlItemDescription.DataBind();
            }
        }
    }

    protected void grdsschain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdsschain.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {

            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {
                string fileName;
                fileName = FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));


                string abc = "~/ScannedDrawings/" + fileName;
                // ItemDetailsId
                // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();

                DropDownList ddlItemDescription = row.FindControl("ddlItemsschain") as DropDownList;
                int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");

                Session["templateid1"] = itemdetailsid.ToString();



            }
        }

        if (e.CommandName == "Selectmulti")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdnonstdhardware.Rows[rowIndex];
            // FileUpload FileUpload1 = (row.FindControl("fl1") as FileUpload);
            FileUpload FileUpload1 = ((FileUpload)(row.FindControl("fl1")));
            if (FileUpload1 != null)
            {

            }
            //  System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
            if (FileUpload1.HasFile)
            {


                foreach (GridViewRow rows in grdsschain.Rows)
                {
                    if (((CheckBox)row.FindControl("chkbox")).Checked)
                    {
                        string fileName;
                        fileName = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/ScannedDrawings/" + fileName));


                        string abc = "~/ScannedDrawings/" + fileName;
                        // ItemDetailsId
                        // Label    lblitemdetailsid = ((Label)(row.FindControl("lblItemDetailsId"))).Text.ToString();

                        DropDownList ddlItemDescription = rows.FindControl("ddlItemsschain") as DropDownList;
                        int itemdetailsid = Convert.ToInt32(ddlItemDescription.SelectedValue);
                        db.insert("update ItemDetails set  DrawingPath='" + abc + "' where ItemDetailsId='" + itemdetailsid + "' ");

                        Session["templateid1"] = itemdetailsid.ToString();
                    }
                }


            }
        }
    }

    protected void btnaddfooter_Click(object sender, EventArgs e)
    {
        AddNewRowToGridsschain();
    }

    protected void drpsschain_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < grdsschain.Rows.Count; g++)
        {
            DropDownList ddlItemDescriptiontagnonstdhardware = (grdsschain.Rows[g].FindControl("ddlItemDescriptionsschain") as DropDownList);
            DropDownList drpsschain = (grdsschain.Rows[g].FindControl("drpsschain") as DropDownList);

            CheckBox chkbox = (grdsschain.Rows[g].FindControl("chkbox") as CheckBox);

            if (drpsschain.SelectedValue == "Mtr")
            {
                if (chkbox.Checked == true)
                {

                    float avlqty = db.getDb_Value("select OpeningStock  as avlqty from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptiontagnonstdhardware.SelectedValue + "' ");
                    float con = db.getDb_Value("select ToQty   from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptiontagnonstdhardware.SelectedValue + "' ");
                    Label lblavlqty = (grdsschain.Rows[g].FindControl("lblavlqty") as Label);
                    float c = avlqty * con;
                    lblavlqty.Text = c.ToString();


                    Label lblbalqty = (grdsschain.Rows[g].FindControl("lblbalqty") as Label);
                    TextBox txtbqty = (grdsschain.Rows[g].FindControl("txtbqty") as TextBox);

                    float d = float.Parse(txtbqty.Text);
                    float f = c - d;
                    lblbalqty.Text = f.ToString();
                    db.insert("update ItemDetailsindent set OpeningStock='" + lblbalqty.Text + "' where ItemDetailsId='" + ddlItemDescriptiontagnonstdhardware + "'");
                }
            }
            else
            {


                if (chkbox.Checked == true)
                {

                    float avlqty = db.getDb_Value("select OpeningStock  as avlqty from ItemDetailsindent  where ItemDetailsId='" + ddlItemDescriptiontagnonstdhardware.SelectedValue + "' ");
                    Label lblavlqty = (grdsschain.Rows[g].FindControl("lblavlqty") as Label);
                    lblavlqty.Text = avlqty.ToString();

                    Label lblbalqty = (grdsschain.Rows[g].FindControl("lblbalqty") as Label);
                    TextBox txtbqty = (grdsschain.Rows[g].FindControl("txtbqty") as TextBox);

                    float d = float.Parse(txtbqty.Text);
                    float c = float.Parse(lblavlqty.Text);

                    float f = c - d;
                    lblbalqty.Text = f.ToString();
                }
            }

        }
    }

    protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
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
        gvOrders.DataSource = db.Displaygrid("select distinct RC.RequisitionCafeId ,RC.RequisitionNo  , RC.ReqStatus                   from RequisitionCafeteria RC  inner join RequisitionCafeDtls RCD on RC.RequisitionCafeId=RCD.RequisitionCafeId              where RC.IsCostCentre='" + pno + "'  and   RC.IsDeleted=0  and RCD.TemplateID=0   ");
        gvOrders.DataBind();

    }
}