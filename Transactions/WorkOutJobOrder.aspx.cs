using AjaxControlToolkit;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MayurInventory.DataModel;
using MayurInventory.EntityClass;
using MayurInventory.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transactions_WorkOutJobOrder : System.Web.UI.Page
{
    database db = new database();
    #region [private variables]

    Class1 Obj_PurchaseOrder = new Class1();

    PurchaseOrder Entity_PurchaseOrder = new PurchaseOrder();
    CommanFunction obj_Comman = new CommanFunction();
    DataSet Ds = new DataSet();
    private static ArrayList UNITFACTOR = new ArrayList();
    private static DataTable DSVendor = new DataTable();
    DataSet DsEdit = new DataSet();
    DataTable DtEditPO;
    private static string TOSTRING = string.Empty;
    decimal Amount = 0;
    public static int ItemId = 0, SupplierId = 0, updatePOD = 0;
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    private int SampleFlag = 0;
    decimal TotalQty = 0;
    decimal TotalRate = 0;
    decimal TotalAmt = 0;
    decimal TotalVat = 0;
    decimal TotalCGST = 0;
    decimal TotalSGST = 0;
    decimal TotalIGST = 0;
    decimal TotalDISC = 0;
    decimal TotalNETAmt = 0;
    public decimal CGST = 0;
    public decimal SGST = 0;
    public decimal IGST = 0;

    private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
    #endregion

    #region [user defined Function]

    private void GETSENTMAIL()
    {
        //var client = new System.Web.Mail.Email.Imap.ImapClient("imap.gmail.com", "username@gmail.com", "password"); 
        //client.Connect(true); 
        //client.SelectFolder("[Gmail]/Sent Mail"); 
        //var messageInfoCol = client.ListMessages(); 
        //foreach (var messageInfo in messageInfoCol) 
        //{ 
        //    var SequenceNumber = messageInfo.SequenceNumber; 
        //    var message = client.FetchMessage(SequenceNumber, true); 
        //    var subject = message.Subject; Console.WriteLine(subject);
        //}
    }

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
            TXTKTO.Text = "";
            TXTKCC.Text = "";
            string description = Server.HtmlDecode("HELLO,<br /><br />PLEASE FIND ATTACHED A PURCHASE ORDER OF THE MATERIAL REQUIRED BY US. PLEASE PROVIDE MATERIAL AS MENTION IN PO.<br /><br />IF THE MATERIAL REQUIRED IS NOT AVAILABLE, THEN PLEASE SUGGEST ALTERNATIVE MATERIAL.<br /><br />REGARDS,<br />" + DDLKCMPY.SelectedItem.ToString()).Replace("<br />", Environment.NewLine);
            TxtBody.Text = description;
            LBLID.Text = Convert.ToString(ViewState["MailID"]);
            GETPDF(Convert.ToInt32(ViewState["MailID"]));

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void SendMail(int reqid)
    {
        try
        {
            MailMessage Mail = new MailMessage();
            DataSet dslogin = new DataSet();
            TRLOADING.Visible = true;
            string path = GETPDF(reqid);


            //-------------------------------------------------[Mail Code]-------------------------------------------------
            //string smtpServer = "smtp.gmail.com";
            //string userName = "revomms@gmail.com";
            //string password = "revo@123";

            //int cdoBasic = 1;
            //int cdoSendUsingPort = 2;
            //MailMessage msg = new MailMessage();
            //if (userName.Length > 0)
            //{
            //    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
            //    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 465);
            //    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
            //    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
            //    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", userName);
            //    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);
            //    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", true);
            //}
            //string mystring = string.Empty;
            //msg.To = TXTKTO.Text;
            //msg.Cc = TXTKCC.Text;
            //msg.Subject = TXTKSUBJECT.Text;
            //msg.Body = TxtBody.Text;
            //msg.From = "revomms@gmail.com:465";
            //String sFile = path;
            //if (CHKATTACHBROUCHER.Checked == true)
            //{
            //    msg.Attachments.Add(new System.Web.Mail.MailAttachment(sFile));
            //}

            Mail.To.Add("" + TXTKTO.Text + "");
            if (!string.IsNullOrEmpty(TXTKCC.Text))
            {
                Mail.CC.Add("" + TXTKCC.Text + ""); //= TXTKCC.Text;
            }
            Mail.Subject = TXTKSUBJECT.Text;
            Mail.Body = TxtBody.Text;
            Mail.From = new MailAddress("revosolutionpune@yahoo.com"); //"purchase@kariadevelopers.com";
            if (CHKATTACHBROUCHER.Checked == true)
            {
                //Mail.Attachments.Add(new System.Web.Mail.MailAttachment(sFile));
                //Mail.Attachments.Add(Mail.Attachments());
                Mail.Attachments.Add(new Attachment(path));

            }
            Mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 465);
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;//true
            smtp.Credentials = new System.Net.NetworkCredential("revosolutionpune@yahoo.com", "revosacred123");



            smtp.EnableSsl = true;//false
            smtp.Send(Mail);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            obj_Comman.ShowPopUpMsg("Mail Send..", this.Page);
            //Update Mail Send flag.
            int cnt = Obj_PurchaseOrder.Update_POEmailSentStatus(reqid, out StrError);
            TRLOADING.Visible = false;

            //SmtpMail.SmtpServer = smtpServer;
            //SmtpMail.Send(msg);
            //obj_Comman.ShowPopUpMsg("Mail Send..", this.Page);
            //TRLOADING.Visible = false;

            //  -------------------------------------------------[End Mail Code]-------------------------------------------------
        }
        catch (Exception)
        {
            throw;
        }

    }

    public string GETPDF(int reqid)
    {
        DMPurchaseOrder obj_PO = new DMPurchaseOrder();
        string StrError = string.Empty;
        string PDFMaster = string.Empty;
        ReportDocument CRpt = new ReportDocument();
        DataSet dslogin = new DataSet();
        dslogin = obj_PO.GetPOForPrint(reqid, out StrError);

        if (dslogin.Tables.Count <= 0)
        {
            return string.Empty;
        }
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
        BinaryReader brs1;//
        if ((dslogin.Tables[0].Rows[0]["Status"].ToString()) == "Authorised")
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


        //-------------------------------------------------------------------------------------------
        CRpt.Load(Server.MapPath("~/CrystalPrint/PurchaseOrder_OLD.rpt"));
        CRpt.SetDataSource(dslogin);
        string DATE = DateTime.Now.ToString("dd-MMM-yyyy ss");
        PDFMaster = Server.MapPath(@"~/TempFiles/" + "PO - " + DATE + ".pdf");
        CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
        CHKATTACHBROUCHER.Checked = true;
        CHKATTACHBROUCHER.Text = "PO Details";
        CHKATTACHBROUCHER.ToolTip = PDFMaster;
        iframepdf.Attributes.Add("src", "../TempFiles/" + "PO - " + (DATE) + ".pdf");
        return PDFMaster;
    }

    private void GetLocation()
    {
        DataSet dsLogin = new DataSet();
        DMUserLogin obj_Login = new DMUserLogin();
        UserLogin Entity_Login = new UserLogin();
        if (!string.IsNullOrEmpty(Session["SiteID"].ToString()))
        {
            String LocationIDs = " AND StockLocationID = " + Session["TransactionSiteID"].ToString();
            dsLogin = new DataSet();
            dsLogin = obj_Login.GetLocationAccordingToUser(LocationIDs, out StrError);
            if (dsLogin.Tables.Count > 0 && dsLogin.Tables[1].Rows.Count > 0)
            {
                ddlCompany.SelectedValue = dsLogin.Tables[1].Rows[0]["CompanyID"].ToString();
            }
            else
            {
                ddlCompany.SelectedValue = "0";
            }
        }
    }

    public void BindPurchaseOrderFORCANCELPOPUP(Int32 POID)
    {
        try
        {
           
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private decimal GETORDQTY()
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
            return Convert.ToDecimal((Convert.ToDecimal(txtItemOrdQty.Text)) / Convert.ToDecimal(UNITFACTOR[ddlUNIT.SelectedIndex].ToString()));

        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    //User Right Function===========
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

                DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Purchase Order To Supplier'");
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
        catch (ThreadAbortException exa)
        {
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //User Right Function===========

    public void GETHIGHLITE()
    {
        for (int i = 0; i < ReportGrid.Rows.Count; i++)
        {
            if ((ReportGrid.Rows[i].Cells[9].Text) == "1")
            {
                ReportGrid.Rows[i].BackColor = System.Drawing.Color.White;
                ReportGrid.Rows[i].ForeColor = System.Drawing.Color.Black;
            }
            if ((ReportGrid.Rows[i].Cells[9].Text) == "1")
            {
                ReportGrid.Rows[i].BackColor = System.Drawing.Color.Yellow;
                ReportGrid.Rows[i].ForeColor = System.Drawing.Color.Black;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    private void MakeEmptyForm()
    {
        SampleFlag = 0;
        ViewState["CheckExistingItem"] = null;
        ViewState["EditId"] = null;

        //txtFromDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
        TXTPOQTDATE.Text = TxtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        ddlDepartment.SelectedValue = "0";
      //  txtInstallationRemark.Text = TXTNARRATION.Text = TxtSearch.Text = "";
        txtSubTotal.Text = "0.00";
        txtDiscount.Text = "0.00";
        txtVATAmount.Text = "0.00";
        txtexciseduty.Text = txtNetTotal.Text = "0.00";
        txtexcisedutyper.Text = "0";
        txtCGSTper.Text = "0.00";


        txtCGSTamt.Text = "0.00";
        txtSGSTPer.Text = "0.00";
        txtSGSTAmt.Text = "0.00";
        txtIGSTPer.Text = "0.00";
        txtIGSTAmt.Text = "0.00";


        CHKHAMALI.Checked = CHKFreightAmt.Checked = CHKOtherCharges.Checked = CHKLoading.Checked = false;
        txtInstallationServiceAmount.Text = txtInstallationServicetax.Text = txtInstallationCharge.Text = txtSerTax.Text = txtDekhrekhAmt.Text = txtHamaliAmt.Text = txtCESSAmt.Text = txtFreightAmt.Text = txtPackingAmt.Text = txtPostageAmt.Text = txtOtherCharges.Text = txtGrandTotal.Text = "0.00";


        trsupcon.Visible = false;
        TR_Requision.Visible = false;
        TR_Item.Visible = true;
        TR_RateList.Visible = TR2.Visible = TR3.Visible = TR5.Visible = TR9.Visible = TR10.Visible = true;
        TR_RateList.Visible = false;
        FS_Requisition.Visible = false;
        DDLSERVICETAX.SelectedValue = "0";
        if (!FlagAdd)
            BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnDelete.Visible = false;
     

        SetInitialRow_ReqDetails();
        SetInitialRow_PODetails();
        SetInitialRow_PurOrderGrd();
        SetInitialRow_TermsCondition();
        SetInitialRowFORPOCANCEL();
        //BindRequisitionGrid(StrCondition = string.Empty);
        BindReportGrid(StrCondition);
        GETHIGHLITE();
        BindTermsAndCond(StrCondition);
        Accordion1.SelectedIndex = 1;
        Accordion2.SelectedIndex = 1;

        SetInitialRowReorderGrid();//Reorder Items Details
        SetInitialRow_LastPurchaseOrderGrid();
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
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    public void FillGST()
    {

        string MainString1 = lstSupplierRate.Text;
        string Rate1 = string.Empty;
        //int IDLenght1 = MainString1.IndexOf(')');
        //int RateStart1 = MainString1.IndexOf('@');
        //int RateEnd1 = MainString1.IndexOf("Rs/-");
        Rate1 = lstSupplierRate.Text;


        if (ChkCGST.Checked == true && ChkSGST.Checked == true)
        {
            CGST = Convert.ToDecimal(txtvatper.Text) / 2;
            txtCGSTItemPer.Text = CGST.ToString();

            //txtvatper.Text = string.IsNullOrEmpty(txtvatper.Text) ? "0" : txtvatper.Text;

            txtCGSTItemAmt.Text = ((Convert.ToDecimal(txtItemOrdQty.Text) * (Convert.ToDecimal(Rate1)) * CGST) / 100).ToString("#0.00");

            SGST = Convert.ToDecimal(txtvatper.Text) / 2;
            txtSGSTItemPer.Text = SGST.ToString();

            txtSGSTItemAmt.Text = ((Convert.ToDecimal(txtItemOrdQty.Text) * (Convert.ToDecimal(Rate1)) * SGST) / 100).ToString("#0.00");

            txtIGSTItemPer.Text = "0";
            txtIGSTItemAmt.Text = "0";
        }
        if (ChkIGST.Checked == true)
        {
            IGST = Convert.ToDecimal(txtvatper.Text);
            txtIGSTItemPer.Text = IGST.ToString();
            txtIGSTItemAmt.Text = ((Convert.ToDecimal(txtItemOrdQty.Text) * (Convert.ToDecimal(Rate1)) * IGST) / 100).ToString("#0.00");

            txtSGSTItemPer.Text = "0";
            txtSGSTItemAmt.Text = "0";
            txtCGSTItemPer.Text = "0";
            txtCGSTItemAmt.Text = "0";
        }

    }

    private void MakeControlEmpty()
    {
        // txtFromDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
        // txtToDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
        //ddlCompany.SelectedValue=ddlDepartment.SelectedValue = "0";
        //ddlCategory.SelectedValue = "0";
        //ddlItem.SelectedValue = "0";
        ViewState["PurchaseOrder1"] = null;
        ViewState["GridIndex"] = null;
        ViewState["GridDetails"] = null;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("RequisitionNo", typeof(Int32));
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("Quantity", typeof(decimal));
            dt.Columns.Add("Rate", typeof(decimal));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("TaxPer", typeof(decimal));
            dt.Columns.Add("TaxAmount", typeof(decimal));
            dt.Columns.Add("NetAmount", typeof(decimal));
            dt.Columns.Add("ExpDeliveryDate", typeof(string));
            dt.Columns.Add("DeliveryDays", typeof(string));
            dt.Columns.Add("PurchaseRate", typeof(Int32));
            dt.Columns.Add("QtyInHand", typeof(decimal));
            dt.Columns.Add("QtyInTransit", typeof(decimal));

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["RequisitionNo"] = 0;
            dr["Item"] = "";
            dr["Quantity"] = 0;
            dr["Rate"] = 0.00;
            dr["Amount"] = 0.00;
            dr["TaxPer"] = 0.00;
            dr["TaxAmount"] = 0.00;
            dr["NetAmount"] = 0.00;
            dr["ExpDeliveryDate"] = "";
            dr["DeliveryDays"] = 0;
            dr["PurchaseRate"] = 0.00;
            dr["QtyInHand"] = 0.00;
            dr["QtyInTransit"] = 0.00;

            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
            //GrdReqPO.DataSource = dt;
            //GrdReqPO.DataBind();
        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void SetInitialRow_LastPurchaseOrderGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("Purchased", typeof(string));
            dt.Columns.Add("PONo", typeof(string));
            dt.Columns.Add("PODate", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("SuplierName", typeof(string));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("UOM", typeof(string));
            dt.Columns.Add("Rate", typeof(string));
            dt.Columns.Add("DiscPer", typeof(string));
            dt.Columns.Add("TaxPer", typeof(string));
            dt.Columns.Add("NETRATE", typeof(string));
            dt.Columns.Add("Project", typeof(string));

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["Purchased"] = "";
            dr["PONo"] = "";
            dr["PODate"] = "";
            dr["ItemName"] = "";
            dr["SuplierName"] = "";
            dr["Qty"] = "";
            dr["UOM"] = "";
            dr["Rate"] = "";
            dr["DiscPer"] = "";
            dr["TaxPer"] = "";
            dr["NETRATE"] = "";
            dr["Project"] = "";

            dt.Rows.Add(dr);

            ViewState["CurrentTable_LastPurchaseOrderGrid"] = dt;
            GridViewLPR.DataSource = dt;
            GridViewLPR.DataBind();
        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void SetInitialRowFORPOCANCEL()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("POID", typeof(int));
            dt.Columns.Add("PONO", typeof(string));
            dt.Columns.Add("PODate", typeof(string));
            dt.Columns.Add("POAmount", typeof(decimal));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("SuplierName", typeof(string));
            dr = dt.NewRow();
            dr["POID"] = 0;
            dr["PONO"] = 0;
            dr["PODate"] = "";
            dr["POAmount"] = 0;
            dr["Status"] = "";
            dr["SuplierName"] = "";

            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
           
        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void SetInitialRow_PODetails()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("Cafeteria", typeof(string));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("OrdQty", typeof(decimal));
            dt.Columns.Add("StoreLocation", typeof(string));

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["Cafeteria"] = string.Empty;
            dr["ItemCode"] = string.Empty;
            dr["ItemName"] = string.Empty;
            dr["OrdQty"] = 0.00;
            dr["StoreLocation"] = string.Empty;

            dt.Rows.Add(dr);

            ViewState["PODetails"] = dt;
            GrdPODtls.DataSource = dt;
            GrdPODtls.DataBind();
        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void SetInitialRow_ReqDetails()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("ItemDetailsId", typeof(int));
            dt.Columns.Add("ItemDesc", typeof(string));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("ReqQty", typeof(decimal));
            dt.Columns.Add("TotOrdQty", typeof(decimal));
            dt.Columns.Add("ReqByCafeteria", typeof(decimal));
            dt.Columns.Add("AvgPurRate", typeof(decimal));
            dt.Columns.Add("TransitQty", typeof(decimal));
            dt.Columns.Add("AvailableStock", typeof(decimal));
            dt.Columns.Add("RemQty", typeof(decimal));
            // dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("MinStockLevel", typeof(decimal));
            dt.Columns.Add("DeliveryPeriod", typeof(decimal));
            dt.Columns.Add("StoreLocation", typeof(string));
            dt.Columns.Add("OrdQty", typeof(decimal));
            dt.Columns.Add("VendorID", typeof(Int32));
            dt.Columns.Add("VendorName", typeof(string));
            dt.Columns.Add("ItemId", typeof(Int32));
            dt.Columns.Add("RequisitionCafeId", typeof(Int32));//RequisitionCafeId
            dt.Columns.Add("perGST", typeof(string));

            dt.Columns.Add("CGSTPer", typeof(string));
            dt.Columns.Add("CGSTAmt", typeof(string));
            dt.Columns.Add("SGSTPer", typeof(string));
            dt.Columns.Add("SGSTAmt", typeof(string));

            dt.Columns.Add("IGSTPer", typeof(string));
            dt.Columns.Add("IGSTAmt", typeof(string));

            dt.Columns.Add("vat", typeof(string));
            dt.Columns.Add("perdisc", typeof(string));
            dt.Columns.Add("disc", typeof(string));
            dt.Columns.Add("RemarkForPO", typeof(string));


            dr = dt.NewRow();

            dr["#"] = 0;
            dr["ItemDetailsId"] = 0;
            dr["ItemCode"] = string.Empty;
            dr["ItemName"] = string.Empty;
            dr["ItemDesc"] = string.Empty;
            dr["ReqQty"] = 0;
            dr["Unit"] = string.Empty;
            dr["TotOrdQty"] = 0;
            dr["ReqByCafeteria"] = 0;
            dr["AvgPurRate"] = 0;
            dr["TransitQty"] = 0;
            dr["AvailableStock"] = 0;
            dr["RemQty"] = 0;
            dr["MinStockLevel"] = 0;
            //dr["Unit"] = string.Empty;
            dr["DeliveryPeriod"] = 0;
            dr["StoreLocation"] = string.Empty;
            dr["OrdQty"] = 0;
            dr["VendorID"] = 0;
            dr["VendorName"] = "";
            dr["ItemId"] = 0;
            dr["RequisitionCafeId"] = 0;
            dr["perGST"] = "";



            dr["CGSTPer"] = "";
            dr["CGSTAmt"] = "";
            dr["SGSTPer"] = "";
            dr["SGSTAmt"] = "";
            dr["IGSTPer"] = "";
            dr["IGSTAmt"] = "";



            dr["perdisc"] = "";
            dr["disc"] = "";
            dr["RemarkForPO"] = "";
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
            //GrdReqPO.DataSource = dt;
            //GrdReqPO.DataBind();
        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void SetInitialRow_PurOrderGrd()
    {
        try
        {
            DataTable dtTable = new DataTable();
            DataRow dr = null;
            dtTable.Columns.Add(new DataColumn("#", typeof(Int32)));
            dtTable.Columns.Add(new DataColumn("Code", typeof(string)));
            dtTable.Columns.Add(new DataColumn("Item", typeof(string)));
            dtTable.Columns.Add(new DataColumn("ItemDescID", typeof(Int32)));
            dtTable.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
            dtTable.Columns.Add(new DataColumn("ReqQty", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("OrdQty", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("Vendor", typeof(string)));
            dtTable.Columns.Add(new DataColumn("PurchaseRate", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("PurchaseAmount", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("pervat", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("vat", typeof(decimal)));

            dtTable.Columns.Add(new DataColumn("perGST", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("CGSTPer", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("CGSTAmt", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("SGSTPer", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("SGSTAmt", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("IGSTPer", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("IGSTAmt", typeof(decimal)));




            dtTable.Columns.Add(new DataColumn("perdisc", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("disc", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("VendorId", typeof(Int32)));
            dtTable.Columns.Add(new DataColumn("ItemId", typeof(Int32)));
            dtTable.Columns.Add(new DataColumn("RequisitionCafeId", typeof(Int32)));//RequisitionCafeId
            dtTable.Columns.Add(new DataColumn("UnitConvDtlsId", typeof(Int32)));
            dtTable.Columns.Add(new DataColumn("MainUnitQty", typeof(decimal)));
            dtTable.Columns.Add(new DataColumn("TXTTERMSCONDITIONPOGRID", typeof(string)));
            dtTable.Columns.Add(new DataColumn("TXTTERMSCONDITIONPOGRIDPAYMNET", typeof(string)));
            dtTable.Columns.Add(new DataColumn("RemarkForPO", typeof(string)));
            dr = dtTable.NewRow();

            dr["#"] = 0;
            dr["Code"] = string.Empty;
            dr["Item"] = string.Empty;
            dr["ItemDescID"] = 0;
            dr["ItemDesc"] = string.Empty;
            dr["ReqQty"] = 0;
            dr["OrdQty"] = 0;
            dr["Vendor"] = string.Empty;
            dr["PurchaseRate"] = 0;
            dr["PurchaseAmount"] = 0;
            dr["pervat"] = 0;
            dr["vat"] = 0;

            dr["perGST"] = 0;
            dr["CGSTPer"] = 0;
            dr["CGSTAmt"] = 0;
            dr["SGSTPer"] = 0;
            dr["SGSTAmt"] = 0;
            dr["IGSTPer"] = 0;
            dr["IGSTAmt"] = 0;


            dr["perdisc"] = 0;
            dr["disc"] = 0;
            dr["VendorId"] = 0;
            dr["ItemId"] = 0;
            dr["RequisitionCafeId"] = 0;
            dr["UnitConvDtlsId"] = 0;
            dr["MainUnitQty"] = 0;
            dr["TXTTERMSCONDITIONPOGRID"] = string.Empty;
            dr["TXTTERMSCONDITIONPOGRIDPAYMNET"] = string.Empty;
            dr["RemarkForPO"] = "";
            dtTable.Rows.Add(dr);
            ViewState["PurchaseOrder"] = dtTable;
            PurOrderGrid.DataSource = dtTable;
            PurOrderGrid.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    private void SetInitialRow_TermsCondition()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("TDescptn", typeof(string));
            dt.Columns.Add("select", typeof(bool));
            dr = dt.NewRow();
            dr["#"] = 0;
            dr["Title"] = string.Empty;
            dr["TDescptn"] = string.Empty;
            dr["select"] = 0;
            dt.Rows.Add(dr);
            ViewState["TermsTable"] = dt;
            GridTermCond.DataSource = dt;
            GridTermCond.DataBind();
        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void BindReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            DsReport = Obj_PurchaseOrder.GetPurchase_Order(RepCondition, out StrError);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                ReportGrid.DataSource = DsReport.Tables[0];
                ReportGrid.DataBind();
            }
            else
            {
                ReportGrid.DataSource = null;
                ReportGrid.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void BindTermsAndCond(string RepCondition)
    {
        DataSet DSTAC = new DataSet();
        DSTAC = Obj_PurchaseOrder.GetTermsAndCondition(out RepCondition);
        if (DSTAC.Tables.Count > 0 && DSTAC.Tables[0].Rows.Count > 0)
        {
            ViewState["TermsTable"] = DSTAC.Tables[0];
            GridTermCond.DataSource = DSTAC.Tables[0];
            GridTermCond.DataBind();
        }
        else
        {
            GridTermCond.DataSource = null;
            GridTermCond.DataBind();
            SetInitialRow_TermsCondition();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }
    public void BindRequisitionGrid(string RepCondition)
    {
        
    }

    public void BindRequisitionDetails(Int32 ItemID)
    {
        try
        {
            Ds = Obj_PurchaseOrder.GetRequisitionDtls_ItemWise(ItemID, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                GrdPODtls.DataSource = Ds.Tables[0];
                GrdPODtls.DataBind();

                TR_PODtls.Visible = true;
            }
            else
            {
                GrdPODtls.DataSource = null;
                GrdPODtls.DataBind();

                TR_PODtls.Visible = false;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void FillCombo()
    {
        try
        {
            string Cond = string.Empty;
            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
            //    Cond = Cond + " AND P.CafeteriaId=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}
            Ds = Obj_PurchaseOrder.BindComboBox(Cond, out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepartment.DataSource = Ds.Tables[0];
                    ddlDepartment.DataTextField = "RequisitionNo";
                    ddlDepartment.DataValueField = "RequisitionCafeId";
                    ddlDepartment.DataBind();
                }
                DSVendor = Ds.Tables[1];
                if (Ds.Tables[2].Rows.Count > 0)
                {
                    ddlCategory.DataSource = Ds.Tables[2];
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();
                }
                if (Ds.Tables[3].Rows.Count > 0)
                {
                    ddlItem.DataSource = Ds.Tables[3];
                    ddlItem.DataTextField = "ItemName";
                    ddlItem.DataValueField = "ItemId";
                    ddlItem.DataBind();
                }
                if (Ds.Tables[4].Rows.Count > 0)
                {
                    ddlCompany.DataSource = Ds.Tables[4];
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataValueField = "CompanyId";
                    ddlCompany.DataBind();
                }
                if (Ds.Tables[5].Rows.Count > 0)
                {
                    DDLSERVICETAX.DataSource = Ds.Tables[5];
                    DDLSERVICETAX.DataTextField = "TaxPer";
                    DDLSERVICETAX.DataValueField = "TaxTemplateID";
                    DDLSERVICETAX.DataBind();
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void BindVendor(DropDownList ddlVendor)
    {
        try
        {
            Ds = Obj_PurchaseOrder.BindVendor(out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlVendor.DataSource = Ds.Tables[0];
                    ddlVendor.DataTextField = "SuplierName";
                    ddlVendor.DataValueField = "SuplierId";
                    ddlVendor.DataBind();
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void CalculateRemQty()
    {
        
    }

    private bool Grid_ReqFeild()
    {
        bool flag = false;
        try
        {

            if ((PurOrderGrid.Rows[0].Cells[2].Text.Trim()).ToString() != "")
            {
                if ((PurOrderGrid.Rows[0].Cells[2].Text.Trim()).ToString() != "&nbsp;")
                {
                    flag = true;
                }
            }
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg(ex.Message, this.Page);
        }
        return flag;
    }

    private void CalculateFooterTotal()
    {
        try
        {
            decimal SubTot = string.IsNullOrEmpty(txtSubTotal.Text) ? 0 : Convert.ToDecimal(txtSubTotal.Text);
            decimal Disc = string.IsNullOrEmpty(txtDiscount.Text) ? 0 : Convert.ToDecimal(txtDiscount.Text);
            //decimal VatAmt = string.IsNullOrEmpty(txtVATAmount.Text) ? 0 : Convert.ToDecimal(txtVATAmount.Text);

            decimal CGSTAmt = string.IsNullOrEmpty(txtCGSTamt.Text) ? 0 : Convert.ToDecimal(txtCGSTamt.Text);
            decimal SGSTAmt = string.IsNullOrEmpty(txtSGSTAmt.Text) ? 0 : Convert.ToDecimal(txtSGSTAmt.Text);
            decimal IGSTAmt = string.IsNullOrEmpty(txtIGSTAmt.Text) ? 0 : Convert.ToDecimal(txtIGSTAmt.Text);
            decimal NetTot = string.IsNullOrEmpty(txtNetTotal.Text) ? 0 : Convert.ToDecimal(txtNetTotal.Text);


            txtNetTotal.Text = ((SubTot + CGSTAmt + SGSTAmt + IGSTAmt) - Disc).ToString("0.00");
            txtGrandTotal.Text = ((SubTot + CGSTAmt + SGSTAmt + IGSTAmt) - Disc).ToString("0.00");
            txtNetTotal.ReadOnly = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex)
        {

        }
    }

    private void AddItemIn_POGrid(DataSet DSItemWise)
    {
        try
        {
            int CurrRow = 0;

            Int32 ItemID = Convert.ToInt32(DSItemWise.Tables[0].Rows[CurrRow]["ItemId"].ToString());
            string ItemCode = Convert.ToString(DSItemWise.Tables[0].Rows[CurrRow]["ItemCode"].ToString());
            string ItemName = Convert.ToString(DSItemWise.Tables[0].Rows[CurrRow]["ItemName"].ToString());
            int VendorId = Convert.ToInt32(DSItemWise.Tables[0].Rows[CurrRow]["VendorID"].ToString());
            string VendorName = Convert.ToString(DSItemWise.Tables[0].Rows[CurrRow]["VendorName"].ToString());
            int ItemDescId = 0;
            string ItemDesc = "";
            if (Convert.ToInt32(ddlItemDesc.SelectedValue) > 0)
            {
                ItemDescId = Convert.ToInt32(ddlItemDesc.SelectedValue.ToString());
                ItemDesc = Convert.ToString(ddlItemDesc.SelectedItem.ToString());
            }


            decimal OrdQty = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["OrdQty"].ToString());
            decimal BalQty = 0;
            decimal PurRate = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["AvgPurRate"].ToString());
            decimal PurAmount = 0;

            if (!string.IsNullOrEmpty(OrdQty.ToString()) && Convert.ToDecimal(OrdQty) > 0)
            {
                if (!string.IsNullOrEmpty(OrdQty.ToString()))
                {
                    PurAmount = Convert.ToDecimal((PurRate * (Convert.ToDecimal(GETORDQTY()))).ToString("0.00"));
                }
                else
                {
                    PurAmount = Convert.ToDecimal((PurRate * Convert.ToDecimal("0")).ToString("0.00"));
                }

                if (ViewState["PurchaseOrder"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["PurchaseOrder"];
                    // DSItemWise = (DataSet)dtCurrentTable;
                    DataRow dtTableRow = null;
                    //updatePOD = Convert.ToInt32(dtCurrentTable.Rows[0]["#"].ToString());

                    bool DupFlag = false;
                    int k = 0;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        if (dtCurrentTable.Rows.Count == 1 && (dtCurrentTable.Rows[0]["ItemId"].ToString()) == "0")
                        {
                            dtCurrentTable.Rows.RemoveAt(0);
                        }
                        if (ViewState["GridIndex"] != null)
                        {
                            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dtCurrentTable.Rows[i]["ItemId"]) == Convert.ToInt32(ItemID))
                                {
                                    if (Convert.ToInt32(dtCurrentTable.Rows[i]["VendorId"]) == Convert.ToInt32(VendorId))//check here
                                    {
                                        DupFlag = true;
                                        k = i;
                                    }
                                }
                            }
                            if (DupFlag == true)
                            {
                                dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(dtCurrentTable.Rows[k]["#"].ToString());
                                dtCurrentTable.Rows[k]["Code"] = Convert.ToString(ItemCode);
                                dtCurrentTable.Rows[k]["Item"] = Convert.ToString(ItemName);
                                dtCurrentTable.Rows[k]["ItemDesc"] = Convert.ToString(ItemDesc);
                                dtCurrentTable.Rows[k]["ItemDescID"] = Convert.ToString(ItemDescId);
                                dtCurrentTable.Rows[k]["ReqQty"] = Convert.ToDecimal(BalQty + Convert.ToDecimal(OrdQty));
                                dtCurrentTable.Rows[k]["OrdQty"] = Convert.ToDecimal(OrdQty);
                                dtCurrentTable.Rows[k]["Vendor"] = Convert.ToString(VendorName);
                                dtCurrentTable.Rows[k]["PurchaseRate"] = Convert.ToDecimal(PurRate);
                                dtCurrentTable.Rows[k]["PurchaseAmount"] = Convert.ToDecimal(PurAmount);
                                dtCurrentTable.Rows[k]["VendorId"] = Convert.ToInt32(VendorId);
                                dtCurrentTable.Rows[k]["ItemId"] = Convert.ToInt32(ItemID);
                                dtCurrentTable.Rows[k]["RequisitionCafeId"] = Convert.ToInt32(dtCurrentTable.Rows[k]["RequisitionCafeId"].ToString());
                                dtCurrentTable.Rows[k]["pervat"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["pervat"].ToString());

                                dtCurrentTable.Rows[k]["perGST"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["perGST"].ToString());
                                dtCurrentTable.Rows[k]["CGSTPer"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["CGSTPer"].ToString());
                                dtCurrentTable.Rows[k]["CGSTAmt"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["CGSTAmt"].ToString());
                                dtCurrentTable.Rows[k]["SGSTPer"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["SGSTPer"].ToString());
                                dtCurrentTable.Rows[k]["SGSTAmt"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["SGSTAmt"].ToString());
                                dtCurrentTable.Rows[k]["IGSTPer"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["IGSTPer"].ToString());
                                dtCurrentTable.Rows[k]["IGSTAmt"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["IGSTAmt"].ToString());


                                dtCurrentTable.Rows[k]["vat"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["vat"].ToString());
                                dtCurrentTable.Rows[k]["perdisc"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["perdisc"].ToString());
                                dtCurrentTable.Rows[k]["disc"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["disc"].ToString());
                                dtCurrentTable.Rows[k]["UnitConvDtlsId"] = Convert.ToDecimal(ddlUNIT.SelectedValue.ToString());
                                dtCurrentTable.Rows[k]["MainUnitQty"] = Convert.ToDecimal(Convert.ToDecimal(OrdQty) / GETORDQTY());
                                dtCurrentTable.Rows[k]["TXTTERMSCONDITIONPOGRID"] = Convert.ToString(dtCurrentTable.Rows[k]["TXTTERMSCONDITIONPOGRID"].ToString());
                                dtCurrentTable.Rows[k]["TXTTERMSCONDITIONPOGRIDPAYMNET"] = Convert.ToString(dtCurrentTable.Rows[k]["TXTTERMSCONDITIONPOGRIDPAYMNET"].ToString());
                                dtCurrentTable.Rows[k]["RemarkForPO"] = Convert.ToString(dtCurrentTable.Rows[k]["RemarkForPO"].ToString());
                                ViewState["PurchaseOrder"] = dtCurrentTable;
                                PurOrderGrid.DataSource = dtCurrentTable;
                                PurOrderGrid.DataBind();
                                DtEditPO = (DataTable)ViewState["PurchaseOrder"];
                                // MakeControlEmpty();
                            }
                            else
                            {
                                int rowindex = Convert.ToInt32(ViewState["GridIndex"]);

                                dtCurrentTable.Rows[rowindex]["#"] = 0; //updatePOD;// 0;
                                dtCurrentTable.Rows[rowindex]["Code"] = Convert.ToString(ItemCode);
                                dtCurrentTable.Rows[rowindex]["Item"] = Convert.ToString(ItemName);
                                dtCurrentTable.Rows[rowindex]["ItemDesc"] = Convert.ToString(ItemDesc);
                                dtCurrentTable.Rows[rowindex]["ItemDescID"] = Convert.ToString(ItemDescId);
                                dtCurrentTable.Rows[rowindex]["ReqQty"] = Convert.ToDecimal(BalQty + Convert.ToDecimal(OrdQty));
                                dtCurrentTable.Rows[rowindex]["OrdQty"] = Convert.ToDecimal(OrdQty);
                                dtCurrentTable.Rows[rowindex]["Vendor"] = Convert.ToString(VendorName);
                                dtCurrentTable.Rows[rowindex]["PurchaseRate"] = Convert.ToDecimal(PurRate);
                                dtCurrentTable.Rows[rowindex]["PurchaseAmount"] = Convert.ToDecimal(PurAmount);
                                dtCurrentTable.Rows[rowindex]["VendorId"] = Convert.ToInt32(VendorId);
                                dtCurrentTable.Rows[rowindex]["ItemId"] = Convert.ToInt32(ItemID);
                                dtCurrentTable.Rows[rowindex]["RequisitionCafeId"] = 0;

                                dtCurrentTable.Rows[rowindex]["perGST"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["perGST"].ToString());
                                dtCurrentTable.Rows[rowindex]["CGSTPer"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["CGSTPer"].ToString());
                                dtCurrentTable.Rows[rowindex]["CGSTAmt"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["CGSTAmt"].ToString());
                                dtCurrentTable.Rows[rowindex]["SGSTPer"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["SGSTPer"].ToString());
                                dtCurrentTable.Rows[rowindex]["SGSTAmt"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["SGSTAmt"].ToString());
                                dtCurrentTable.Rows[rowindex]["IGSTPer"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["IGSTPer"].ToString());
                                dtCurrentTable.Rows[rowindex]["IGSTAmt"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["IGSTAmt"].ToString());

                                dtCurrentTable.Rows[rowindex]["pervat"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["pervat"].ToString());
                                dtCurrentTable.Rows[rowindex]["vat"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["vat"].ToString());
                                dtCurrentTable.Rows[rowindex]["perdisc"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["perdisc"].ToString());
                                dtCurrentTable.Rows[rowindex]["disc"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["disc"].ToString());
                                dtCurrentTable.Rows[rowindex]["UnitConvDtlsId"] = Convert.ToDecimal(ddlUNIT.SelectedValue.ToString());
                                dtCurrentTable.Rows[rowindex]["MainUnitQty"] = Convert.ToDecimal(Convert.ToDecimal(OrdQty) / GETORDQTY());
                                dtCurrentTable.Rows[rowindex]["TXTTERMSCONDITIONPOGRID"] = Convert.ToString(dtCurrentTable.Rows[0]["TXTTERMSCONDITIONPOGRID"].ToString());
                                dtCurrentTable.Rows[rowindex]["TXTTERMSCONDITIONPOGRIDPAYMNET"] = Convert.ToString(dtCurrentTable.Rows[0]["TXTTERMSCONDITIONPOGRIDPAYMNET"].ToString());
                                dtCurrentTable.Rows[rowindex]["RemarkForPO"] = Convert.ToString(dtCurrentTable.Rows[k]["RemarkForPO"].ToString());
                                ViewState["PurchaseOrder"] = dtCurrentTable;
                                PurOrderGrid.DataSource = dtCurrentTable;
                                PurOrderGrid.DataBind();
                                DtEditPO = (DataTable)ViewState["PurchaseOrder"];
                                //  MakeControlEmpty();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dtCurrentTable.Rows[i]["ItemID"]) == Convert.ToInt32(ItemID))
                                {
                                    if (Convert.ToInt32(dtCurrentTable.Rows[i]["VendorId"]) == Convert.ToInt32(VendorId))
                                    {
                                        DupFlag = true;
                                        k = i;
                                    }
                                }
                            }
                            if (DupFlag == true)
                            {
                                dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(dtCurrentTable.Rows[k]["#"].ToString());
                                dtCurrentTable.Rows[k]["Code"] = Convert.ToString(ItemCode);
                                dtCurrentTable.Rows[k]["Item"] = Convert.ToString(ItemName);
                                dtCurrentTable.Rows[k]["ItemDesc"] = Convert.ToString(ItemDesc);
                                dtCurrentTable.Rows[k]["ItemDescID"] = Convert.ToString(ItemDescId);
                                dtCurrentTable.Rows[k]["ReqQty"] = Convert.ToDecimal(BalQty + Convert.ToDecimal(OrdQty));
                                dtCurrentTable.Rows[k]["OrdQty"] = Convert.ToDecimal(OrdQty);
                                dtCurrentTable.Rows[k]["Vendor"] = Convert.ToString(VendorName);
                                dtCurrentTable.Rows[k]["PurchaseRate"] = Convert.ToDecimal(PurRate);
                                dtCurrentTable.Rows[k]["PurchaseAmount"] = Convert.ToDecimal(PurAmount);
                                dtCurrentTable.Rows[k]["VendorId"] = Convert.ToInt32(VendorId);
                                dtCurrentTable.Rows[k]["ItemId"] = Convert.ToInt32(ItemID);
                                dtCurrentTable.Rows[k]["RequisitionCafeId"] = Convert.ToInt32(dtCurrentTable.Rows[k]["RequisitionCafeId"].ToString());

                                dtCurrentTable.Rows[k]["perGST"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["perGST"].ToString());
                                dtCurrentTable.Rows[k]["CGSTPer"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["CGSTPer"].ToString());
                                dtCurrentTable.Rows[k]["CGSTAmt"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["CGSTAmt"].ToString());
                                dtCurrentTable.Rows[k]["SGSTPer"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["SGSTPer"].ToString());
                                dtCurrentTable.Rows[k]["SGSTAmt"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["SGSTAmt"].ToString());
                                dtCurrentTable.Rows[k]["IGSTPer"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["IGSTPer"].ToString());
                                dtCurrentTable.Rows[k]["IGSTAmt"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["IGSTAmt"].ToString());

                                dtCurrentTable.Rows[k]["pervat"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["pervat"].ToString());
                                dtCurrentTable.Rows[k]["vat"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["vat"].ToString());
                                dtCurrentTable.Rows[k]["perdisc"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["perdisc"].ToString());
                                dtCurrentTable.Rows[k]["disc"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["disc"].ToString());
                                dtCurrentTable.Rows[k]["UnitConvDtlsId"] = Convert.ToDecimal(ddlUNIT.SelectedValue.ToString());
                                dtCurrentTable.Rows[k]["MainUnitQty"] = Convert.ToDecimal(Convert.ToDecimal(OrdQty) / GETORDQTY());
                                dtCurrentTable.Rows[k]["TXTTERMSCONDITIONPOGRID"] = Convert.ToString(dtCurrentTable.Rows[k]["TXTTERMSCONDITIONPOGRID"].ToString());
                                dtCurrentTable.Rows[k]["TXTTERMSCONDITIONPOGRIDPAYMNET"] = Convert.ToString(dtCurrentTable.Rows[k]["TXTTERMSCONDITIONPOGRIDPAYMNET"].ToString());
                                dtCurrentTable.Rows[k]["RemarkForPO"] = Convert.ToString(dtCurrentTable.Rows[k]["RemarkForPO"].ToString());
                                ViewState["PurchaseOrder"] = dtCurrentTable;
                                PurOrderGrid.DataSource = dtCurrentTable;
                                PurOrderGrid.DataBind();
                                DtEditPO = (DataTable)ViewState["PurchaseOrder"];
                                //  MakeControlEmpty();
                            }
                            else
                            {
                                dtTableRow = dtCurrentTable.NewRow();

                                dtTableRow["#"] = 0;
                                dtTableRow["Code"] = Convert.ToString(ItemCode);
                                dtTableRow["Item"] = Convert.ToString(ItemName);
                                dtTableRow["ItemDesc"] = Convert.ToString(ItemDesc);
                                dtTableRow["ItemDescID"] = Convert.ToString(ItemDescId);
                                dtTableRow["ReqQty"] = Convert.ToDecimal(BalQty + Convert.ToDecimal(OrdQty));
                                dtTableRow["OrdQty"] = Convert.ToDecimal(OrdQty);
                                dtTableRow["Vendor"] = Convert.ToString(VendorName);
                                dtTableRow["PurchaseRate"] = Convert.ToDecimal(PurRate);
                                dtTableRow["PurchaseAmount"] = Convert.ToDecimal(PurAmount);
                                dtTableRow["VendorId"] = Convert.ToInt32(VendorId);
                                dtTableRow["ItemId"] = Convert.ToInt32(ItemID);
                                dtTableRow["RequisitionCafeId"] = 0;
                                dtTableRow["pervat"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["pervat"].ToString());
                                dtTableRow["perGST"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["perGST"].ToString());
                                dtTableRow["CGSTPer"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["CGSTPer"].ToString());
                                dtTableRow["CGSTAmt"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["CGSTAmt"].ToString());
                                dtTableRow["SGSTPer"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["SGSTPer"].ToString());
                                dtTableRow["SGSTAmt"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["SGSTAmt"].ToString());
                                dtTableRow["IGSTPer"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["IGSTPer"].ToString());
                                dtTableRow["IGSTAmt"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["IGSTAmt"].ToString());

                                dtTableRow["vat"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["vat"].ToString());
                                dtTableRow["perdisc"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["perdisc"].ToString());
                                dtTableRow["disc"] = Convert.ToDecimal(DSItemWise.Tables[0].Rows[CurrRow]["disc"].ToString());
                                dtTableRow["UnitConvDtlsId"] = Convert.ToDecimal(ddlUNIT.SelectedValue.ToString());
                                dtTableRow["MainUnitQty"] = Convert.ToDecimal(Convert.ToDecimal(OrdQty) / GETORDQTY());
                                dtTableRow["TXTTERMSCONDITIONPOGRID"] = "";
                                dtTableRow["TXTTERMSCONDITIONPOGRIDPAYMNET"] = "";
                                dtTableRow["RemarkForPO"] = "";
                                dtCurrentTable.Rows.Add(dtTableRow);

                                ViewState["PurchaseOrder"] = dtCurrentTable;
                                PurOrderGrid.DataSource = dtCurrentTable;
                                PurOrderGrid.DataBind();
                                DtEditPO = (DataTable)ViewState["PurchaseOrder"];
                                //  MakeControlEmpty();
                            }
                        }
                    }
                }
                ViewState["PurchaseOrder1"] = DtEditPO;
            }
            //******* Calculate Total Order Qty. ********
            CalculateRemQty();
            //ViewState["PurchaseOrder"] = PurOrderGrid;
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex)
        {
        }

    }

    #endregion

    #region For ReOrder Item Code(Copy and Paste in All Forms)

    private void SetInitialRowReorderGrid()
    {
        try
        {
            StockDetails Entity_StockDetails = new StockDetails();
            DMStockDetails Obj_StockDetails = new DMStockDetails();
            DataSet DS_Stock = new DataSet();
            //int locationID = Convert.ToInt32(Session["CafeteriaId"]);
            int locationID = 1;

            DS_Stock = Obj_StockDetails.Get_Stock_For_Reorder_LocationWise(locationID, out StrError);
            if (DS_Stock.Tables.Count > 0 && DS_Stock.Tables[0].Rows.Count > 0)
            {
                ViewState["ReorderItemDtls"] = DS_Stock.Tables[0];
                GrdReorder.DataSource = DS_Stock.Tables[0];
                GrdReorder.DataBind();

            }
            else
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add("#", typeof(int));
                dt.Columns.Add("ItemCode", typeof(string));
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("ReorderLavel", typeof(decimal));
                dt.Columns.Add("AvilableQty", typeof(decimal));

                dr = dt.NewRow();
                dr["#"] = 0;
                dr["ItemCode"] = "";
                dr["ItemName"] = "";
                dr["ReorderLavel"] = 0;
                dr["AvilableQty"] = 0;

                dt.Rows.Add(dr);
                ViewState["ReorderItemDtls"] = dt;
                GrdReorder.DataSource = dt;
                GrdReorder.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void GrdReorder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.GrdReorder.PageIndex = e.NewPageIndex;
            SetInitialRowReorderGrid();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void ImgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        Panel1.Visible = false;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            AccordionPane1.HeaderContainer.Width = 1030;
            AccordionPane2.HeaderContainer.Width = 1030;

            MakeControlEmpty();
            MakeEmptyForm();
            //  CheckUserRight();

            FillCombo();




            trsupcon.Visible = false;
            TR_Requision.Visible = false;
            TR_Item.Visible = true;
            TR_RateList.Visible = TR2.Visible = TR3.Visible = TR5.Visible = TR9.Visible = TR10.Visible = true;
            TR_RateList.Visible = false;
            SetInitialRow_ReqDetails();

            drpsupplier.DataSource = db.Displaygrid("select SuplierId,SuplierName from SuplierMaster where IsDeleted=0  ");
            drpsupplier.DataTextField = "SuplierName";
            drpsupplier.DataValueField = "SuplierId";
            drpsupplier.DataBind();
            drpsupplier.Items.Insert(0, "-Select Supplier  -");


        }
    }
    

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = string.Empty;
      //  StrCondition = TxtSearch.Text.Trim();
        Ds = new DataSet();
        Ds = Obj_PurchaseOrder.GetPOList(StrCondition, out StrError);
        if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
        {
            //ViewState["CurrentTable"] = Ds.Tables[1];
            ReportGrid.DataSource = Ds.Tables[0];
            ReportGrid.DataBind();
            Ds = null;
        }
        else
        {
            ReportGrid.DataSource = null;
            ReportGrid.DataBind();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMPurchaseOrder Obj_PurchaseOrder = new DMPurchaseOrder();
        String[] SearchList = Obj_PurchaseOrder.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void GrdSelectAllHeader_CheckedChanged(object sender, EventArgs e)
    {
       
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCompany.SelectedValue != "0")
            {
                int InsertRowDtls = 0, iInsert = 0, MaxId = 0;
                ArrayList Suplierlist = new ArrayList();
                ArrayList POlist = new ArrayList();
                int PO_SupId;

                bool Match_Flag = false;
                if (Grid_ReqFeild() == true)
                {
                    for (int MaxGrdRow = 0; MaxGrdRow < PurOrderGrid.Rows.Count; MaxGrdRow++)
                    {
                        PO_SupId = Convert.ToInt32(PurOrderGrid.Rows[MaxGrdRow].Cells[22].Text);
                        if (Suplierlist.Count == 0)
                        {
                            Match_Flag = true;
                        }
                        else
                        {
                            for (int m = 0; m < Suplierlist.Count; m++)
                            {
                                if (Suplierlist[m].ToString() == PO_SupId.ToString())
                                {
                                    goto CheckNextSupplier;
                                }
                                else
                                {
                                    Match_Flag = true;
                                }
                            }
                        }
                        if (Match_Flag == true)
                        {
                            Suplierlist.Add(PO_SupId);


                            Entity_PurchaseOrder.PONo = "";
                            Entity_PurchaseOrder.PODate = Convert.ToDateTime(TxtDate.Text.ToString());
                            Entity_PurchaseOrder.SuplierId = Convert.ToInt32(PO_SupId);
                            Entity_PurchaseOrder.BillingAddress = "";
                            Entity_PurchaseOrder.ShippingAddress = "";
                            Entity_PurchaseOrder.SubTotal = txtSubTotal.Text.Equals("") ? 0 : Convert.ToDecimal(txtSubTotal.Text);
                            Entity_PurchaseOrder.Discount = txtDiscount.Text.Equals("") ? 0 : Convert.ToDecimal(txtDiscount.Text);
                            Entity_PurchaseOrder.Vat = txtVATAmount.Text.Equals("") ? 0 : Convert.ToDecimal(txtVATAmount.Text);
                            Entity_PurchaseOrder.GrandTotal = txtGrandTotal.Text.Equals("") ? 0 : Convert.ToDecimal(txtGrandTotal.Text);

                            Entity_PurchaseOrder.DekhrekhAmt = txtDekhrekhAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtDekhrekhAmt.Text);
                            Entity_PurchaseOrder.HamaliAmt = txtHamaliAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtHamaliAmt.Text);
                            Entity_PurchaseOrder.CESSAmt = txtCESSAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtCESSAmt.Text);
                            Entity_PurchaseOrder.FreightAmt = txtFreightAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtFreightAmt.Text);
                            Entity_PurchaseOrder.PackingAmt = txtPackingAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtPackingAmt.Text);
                            Entity_PurchaseOrder.PostageAmt = txtPostageAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtPostageAmt.Text);
                            Entity_PurchaseOrder.OtherCharges = txtOtherCharges.Text.Equals("") ? 0 : Convert.ToDecimal(txtOtherCharges.Text);
                            Entity_PurchaseOrder.ServiceTaxPer = Convert.ToDecimal(DDLSERVICETAX.SelectedItem.ToString());
                            Entity_PurchaseOrder.ServiceTaxAmt = Convert.ToDecimal(txtSerTax.Text.ToString());
                            Entity_PurchaseOrder.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue.ToString());

                            Entity_PurchaseOrder.Instruction = TXTNARRATION.Text.Trim();

                            Entity_PurchaseOrder.UserId = Convert.ToInt32(Session["UserId"]);
                            Entity_PurchaseOrder.LoginDate = DateTime.Now;
                            Entity_PurchaseOrder.PaymentTerms = Convert.ToInt32(RdoPaymentDays.SelectedValue.ToString());
                            Entity_PurchaseOrder.POQTDATE = Convert.ToDateTime(TXTPOQTDATE.Text.ToString()).ToString();

                            Entity_PurchaseOrder.ExcisePer = Convert.ToDecimal(txtexcisedutyper.Text.ToString());
                            Entity_PurchaseOrder.ExciseAmount = Convert.ToDecimal(txtexciseduty.Text.ToString());


                            //Entity_PurchaseOrder.GSTPer = (!string.IsNullOrEmpty(txtGSTPer.Text)) ? Convert.ToDecimal(txtGSTPer.Text.Trim()) : 0;
                            //Entity_PurchaseOrder.GSTAmt = (!string.IsNullOrEmpty(txtGSTAmt.Text)) ? Convert.ToDecimal(txtGSTAmt.Text.Trim()) : 0;
                            Entity_PurchaseOrder.CGSTTotAmt = (!string.IsNullOrEmpty(txtCGSTamt.Text)) ? Convert.ToDecimal(txtCGSTamt.Text.Trim()) : 0;
                            Entity_PurchaseOrder.SGSTTotalAmt = (!string.IsNullOrEmpty(txtSGSTAmt.Text)) ? Convert.ToDecimal(txtSGSTAmt.Text.Trim()) : 0;
                            Entity_PurchaseOrder.IGSTTotalAmt = (!string.IsNullOrEmpty(txtIGSTAmt.Text)) ? Convert.ToDecimal(txtIGSTAmt.Text.Trim()) : 0;


                            Entity_PurchaseOrder.InstallationRemark = Convert.ToString(txtInstallationRemark.Text.ToString());
                            Entity_PurchaseOrder.InstallationCharge = Convert.ToDecimal(txtInstallationCharge.Text.ToString());
                            Entity_PurchaseOrder.InstallationSerTaxPer = Convert.ToDecimal(txtInstallationServicetax.Text.ToString());
                            Entity_PurchaseOrder.InstallationSerTaxAmt = Convert.ToDecimal(txtInstallationServiceAmount.Text.ToString());
                            Entity_PurchaseOrder.natureofproccesing = txtnatureofproccessing.Text;
                            if (CHKHAMALI.Checked == true)
                            {
                                Entity_PurchaseOrder.HamaliActual = 1;
                            }
                            if (CHKHAMALI.Checked == false)
                            {
                                Entity_PurchaseOrder.HamaliActual = 0;
                            }

                            if (CHKFreightAmt.Checked == true)
                            {
                                Entity_PurchaseOrder.FreightActual = 1;
                            }
                            if (CHKFreightAmt.Checked == false)
                            {
                                Entity_PurchaseOrder.FreightActual = 0;
                            }

                            if (CHKOtherCharges.Checked == true)
                            {
                                Entity_PurchaseOrder.OtherChargeActual = 1;
                            }
                            if (CHKOtherCharges.Checked == false)
                            {
                                Entity_PurchaseOrder.OtherChargeActual = 0;
                            }

                            if (CHKLoading.Checked == true)
                            {
                                Entity_PurchaseOrder.LoadingActual = 1;
                            }
                            if (CHKLoading.Checked == false)
                            {
                                Entity_PurchaseOrder.LoadingActual = 0;
                            }

                            iInsert = Obj_PurchaseOrder.Insert_PurchseOrder(ref Entity_PurchaseOrder, TXTPOQTNO.Text.Trim(), out StrError);
                            MaxId = iInsert;
                            POlist.Add(iInsert);
                            #region [Insert PO Details Grid]
                            if (MaxId > 0)
                            {
                                if (ViewState["CurrentTable"] != null)
                                {
                                    for (int k = 0; k < PurOrderGrid.Rows.Count; k++)
                                    {
                                        int PODtls_SupId = Convert.ToInt32(PurOrderGrid.Rows[k].Cells[22].Text);

                                        if (PO_SupId == PODtls_SupId)
                                        {
                                            Class1 oDMPurchase_Order = new Class1();
                                            PurchaseOrder oPurchase_Order = new PurchaseOrder();

                                            oPurchase_Order.POId = MaxId;
                                            oPurchase_Order.ItemId = Convert.ToInt32(PurOrderGrid.Rows[k].Cells[23].Text);
                                            oPurchase_Order.Qty = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[7].Text);
                                            oPurchase_Order.Rate = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[9].Text);
                                            oPurchase_Order.Amount = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[21].Text);
                                            //oPurchase_Order.TaxPer = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[10].Text);
                                            //oPurchase_Order.TaxAmount = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[11].Text);
                                            oPurchase_Order.DiscPer = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[19].Text);
                                            oPurchase_Order.DiscAmt = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[20].Text);
                                            oPurchase_Order.NetAmount = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[21].Text);
                                            oPurchase_Order.RequisitionCafeId = Convert.ToInt32(PurOrderGrid.Rows[k].Cells[24].Text);
                                            oPurchase_Order.FK_UnitConvDtlsId = Convert.ToInt32(PurOrderGrid.Rows[k].Cells[25].Text);
                                            oPurchase_Order.MainQty = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[26].Text);
                                            oPurchase_Order.RemarkForPO = Convert.ToString(((TextBox)PurOrderGrid.Rows[k].FindControl("TXTREMARK")).Text);
                                            oPurchase_Order.GSTPerDetails = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[12].Text);
                                            oPurchase_Order.CGSTPer = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[13].Text);
                                            oPurchase_Order.SGSTPer = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[15].Text);
                                            oPurchase_Order.IGSTPer = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[17].Text);
                                            oPurchase_Order.CGSTAmt = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[14].Text);
                                            oPurchase_Order.SGSTAmt = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[16].Text);
                                            oPurchase_Order.IGSTAmt = Convert.ToDecimal(PurOrderGrid.Rows[k].Cells[18].Text);

                                            InsertRowDtls = oDMPurchase_Order.Insert_PurchaseOrderDtls(ref oPurchase_Order, Convert.ToInt32(PurOrderGrid.Rows[k].Cells[4].Text), out StrError);

                                            oDMPurchase_Order = null;
                                            oPurchase_Order = null;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region [Terms and Condition]------

                            if (MaxId > 0)
                            {
                                if (ViewState["CurrentTable"] != null)
                                {
                                    for (int k = 0; k < PurOrderGrid.Rows.Count; k++)
                                    {
                                        int PODtls_SupId = Convert.ToInt32(PurOrderGrid.Rows[k].Cells[22].Text);

                                        if (PO_SupId == PODtls_SupId)
                                        {
                                            DMPurchaseOrder ODMPUR = new DMPurchaseOrder();
                                            PurchaseOrder ENPUR = new PurchaseOrder();

                                            ENPUR.POId = MaxId;
                                            ENPUR.Title = "";
                                            ENPUR.TermsCondition = Convert.ToString(((TextBox)PurOrderGrid.Rows[k].FindControl("TXTTERMSCONDITIONPOGRID")).Text);

                                            InsertRowDtls = ODMPUR.Insert_PurchaseOrderTandC(ref ENPUR, Convert.ToString(((TextBox)PurOrderGrid.Rows[k].FindControl("TXTTERMSCONDITIONPOGRIDPAYMNET")).Text), out StrError);

                                            ODMPUR = null;
                                            ENPUR = null;
                                        }
                                    }
                                }
                            }
                            #endregion

                        }
                    CheckNextSupplier: Match_Flag = false;
                    }//End For Loop
                    if (iInsert != 0)
                    {
                        for (int m = 0; m < POlist.Count; m++)
                        {
                            int GETUPDATES = Obj_PurchaseOrder.UpdateInsert_PurchseOrder(Convert.ToInt32(POlist[m].ToString()), out StrError);
                        }

                        obj_Comman.ShowPopUpMsg("Record Saved Successfully!", this.Page);
                        //for (int n = 0; n < POlist.Count; n++)
                        //{
                        //    int status = SendMail(Convert.ToInt32(POlist[n].ToString()), 2);
                        //    if (status == 1)
                        //    {
                        //        int GETUPDATES1 = Obj_PurchaseOrder.UpdateInsert_PurchseOrderMAIL(Convert.ToInt32(POlist[n].ToString()), out StrError);
                        //    }

                        //}

                        MakeControlEmpty();
                        MakeEmptyForm();
                    }
                    else
                    {
                        obj_Comman.ShowPopUpMsg(StrError, this.Page);
                    }
                    Entity_PurchaseOrder = null;
                    Obj_PurchaseOrder = null;
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("No Purchase Order Details are Found ..!", this.Page);
                }
                StrCondition = string.Empty;
            }
            else
            {
                obj_Comman.ShowPopUpMsg("please select company!", this.Page);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int InsertRowDtls = 0, iUpdate = 0;
            if (ddlCompany.SelectedValue != "0")
            {
                if (ViewState["EditID"] != null)
                {
                    Entity_PurchaseOrder.POId = Convert.ToInt32(ViewState["EditID"]);
                    Entity_PurchaseOrder.PODate = Convert.ToDateTime(TxtDate.Text.ToString());
                    Entity_PurchaseOrder.BillingAddress = "";
                    Entity_PurchaseOrder.ShippingAddress = "";
                    Entity_PurchaseOrder.SubTotal = txtSubTotal.Text.Equals("") ? 0 : Convert.ToDecimal(txtSubTotal.Text);
                    Entity_PurchaseOrder.Discount = txtDiscount.Text.Equals("") ? 0 : Convert.ToDecimal(txtDiscount.Text);
                    Entity_PurchaseOrder.Vat = txtVATAmount.Text.Equals("") ? 0 : Convert.ToDecimal(txtVATAmount.Text);
                    Entity_PurchaseOrder.GrandTotal = txtGrandTotal.Text.Equals("") ? 0 : Convert.ToDecimal(txtGrandTotal.Text);

                    Entity_PurchaseOrder.DekhrekhAmt = txtDekhrekhAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtDekhrekhAmt.Text);
                    Entity_PurchaseOrder.HamaliAmt = txtHamaliAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtHamaliAmt.Text);
                    Entity_PurchaseOrder.CESSAmt = txtCESSAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtCESSAmt.Text);
                    Entity_PurchaseOrder.FreightAmt = txtFreightAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtFreightAmt.Text);
                    Entity_PurchaseOrder.PackingAmt = txtPackingAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtPackingAmt.Text);
                    Entity_PurchaseOrder.PostageAmt = txtPostageAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtPostageAmt.Text);
                    Entity_PurchaseOrder.OtherCharges = txtOtherCharges.Text.Equals("") ? 0 : Convert.ToDecimal(txtOtherCharges.Text);
                    Entity_PurchaseOrder.ServiceTaxPer = Convert.ToDecimal(DDLSERVICETAX.SelectedItem.ToString());
                    Entity_PurchaseOrder.ServiceTaxAmt = Convert.ToDecimal(txtSerTax.Text.ToString());
                    Entity_PurchaseOrder.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue.ToString());

                    Entity_PurchaseOrder.Instruction = TXTNARRATION.Text.Trim();

                    Entity_PurchaseOrder.UserId = Convert.ToInt32(Session["UserId"]);
                    Entity_PurchaseOrder.LoginDate = DateTime.Now;
                    Entity_PurchaseOrder.PaymentTerms = Convert.ToInt32(RdoPaymentDays.SelectedValue.ToString());
                    Entity_PurchaseOrder.POQTDATE = Convert.ToDateTime(TXTPOQTDATE.Text.ToString()).ToString();

                    Entity_PurchaseOrder.ExcisePer = Convert.ToDecimal(txtexcisedutyper.Text.ToString());
                    Entity_PurchaseOrder.ExciseAmount = Convert.ToDecimal(txtexciseduty.Text.ToString());

                    Entity_PurchaseOrder.CGSTTotAmt = (!string.IsNullOrEmpty(txtCGSTamt.Text)) ? Convert.ToDecimal(txtCGSTamt.Text.Trim()) : 0;
                    Entity_PurchaseOrder.SGSTTotalAmt = (!string.IsNullOrEmpty(txtSGSTAmt.Text)) ? Convert.ToDecimal(txtSGSTAmt.Text.Trim()) : 0;
                    Entity_PurchaseOrder.IGSTTotalAmt = (!string.IsNullOrEmpty(txtIGSTAmt.Text)) ? Convert.ToDecimal(txtIGSTAmt.Text.Trim()) : 0;


                    Entity_PurchaseOrder.InstallationRemark = Convert.ToString(txtInstallationRemark.Text.ToString());
                    Entity_PurchaseOrder.InstallationCharge = Convert.ToDecimal(txtInstallationCharge.Text.ToString());
                    Entity_PurchaseOrder.InstallationSerTaxPer = Convert.ToDecimal(txtInstallationServicetax.Text.ToString());
                    Entity_PurchaseOrder.InstallationSerTaxAmt = Convert.ToDecimal(txtInstallationServiceAmount.Text.ToString());
                    Entity_PurchaseOrder.natureofproccesing = txtnatureofproccessing.Text;

                    if (CHKHAMALI.Checked == true)
                    {
                        Entity_PurchaseOrder.HamaliActual = 1;
                    }
                    if (CHKHAMALI.Checked == false)
                    {
                        Entity_PurchaseOrder.HamaliActual = 0;
                    }

                    if (CHKFreightAmt.Checked == true)
                    {
                        Entity_PurchaseOrder.FreightActual = 1;
                    }
                    if (CHKFreightAmt.Checked == false)
                    {
                        Entity_PurchaseOrder.FreightActual = 0;
                    }

                    if (CHKOtherCharges.Checked == true)
                    {
                        Entity_PurchaseOrder.OtherChargeActual = 1;
                    }
                    if (CHKOtherCharges.Checked == false)
                    {
                        Entity_PurchaseOrder.OtherChargeActual = 0;
                    }

                    if (CHKLoading.Checked == true)
                    {
                        Entity_PurchaseOrder.LoadingActual = 1;
                    }
                    if (CHKLoading.Checked == false)
                    {
                        Entity_PurchaseOrder.LoadingActual = 0;
                    }

                    iUpdate = Obj_PurchaseOrder.UpdateRecord(ref Entity_PurchaseOrder, out StrError);
                }
                if (iUpdate > 0)
                {
                    if (ViewState["PurchaseOrder1"] != null)
                    {
                        DataTable dttable = new DataTable();
                        dttable = (DataTable)ViewState["PurchaseOrder1"];
                        for (int v = 0; v < PurOrderGrid.Rows.Count; v++)//for purchase Order grid
                        {
                            Class1 oDMPO = new Class1();
                            PurchaseOrder pod = new PurchaseOrder();
                            pod.POId = Convert.ToInt32(ViewState["EditID"]);
                            pod.PODtlsId = Convert.ToInt32(dttable.Rows[v][0].ToString());
                            pod.ItemId = Convert.ToInt32(PurOrderGrid.Rows[v].Cells[23].Text);
                            pod.Qty = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[7].Text);
                            pod.Rate = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[9].Text);
                            pod.Amount = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[21].Text);
                            //pod.TaxPer = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[10].Text);
                            //pod.TaxAmount = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[11].Text);
                            pod.DiscPer = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[19].Text);
                            pod.DiscAmt = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[20].Text);
                            pod.NetAmount = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[21].Text);
                            pod.RequisitionCafeId = Convert.ToInt32(PurOrderGrid.Rows[v].Cells[24].Text);
                            pod.FK_UnitConvDtlsId = Convert.ToInt32(PurOrderGrid.Rows[v].Cells[25].Text);
                            pod.MainQty = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[26].Text);

                            pod.GSTPerDetails = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[12].Text);
                            pod.CGSTPer = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[13].Text);
                            pod.SGSTPer = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[15].Text);
                            pod.IGSTPer = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[17].Text);
                            pod.CGSTAmt = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[14].Text);
                            pod.SGSTAmt = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[16].Text);
                            pod.IGSTAmt = Convert.ToDecimal(PurOrderGrid.Rows[v].Cells[18].Text);
                            pod.RemarkForPO = Convert.ToString(((TextBox)PurOrderGrid.Rows[v].FindControl("TXTREMARK")).Text);

                            InsertRowDtls = oDMPO.Insert_PurchaseOrderDtls(ref pod, Convert.ToInt32(PurOrderGrid.Rows[v].Cells[4].Text), out StrError);


                            oDMPO = null;
                            pod = null;
                        }
                    }
                    if (InsertRowDtls > 0)
                    {
                        if (ViewState["TermsTable"] != null)
                        {
                            for (int J = 0; J < PurOrderGrid.Rows.Count; J++)
                            {
                                DMPurchaseOrder ODMPUR = new DMPurchaseOrder();
                                PurchaseOrder ENPUR = new PurchaseOrder();

                                ENPUR.POId = Convert.ToInt32(ViewState["EditID"]);
                                ENPUR.Title = "";
                                ENPUR.TermsCondition = Convert.ToString(((TextBox)PurOrderGrid.Rows[J].FindControl("TXTTERMSCONDITIONPOGRID")).Text);

                                InsertRowDtls = ODMPUR.Update_PurchaseOrderTandC(ref ENPUR, Convert.ToString(((TextBox)PurOrderGrid.Rows[J].FindControl("TXTTERMSCONDITIONPOGRIDPAYMNET")).Text), out StrError);

                                ODMPUR = null;
                                ENPUR = null;
                            }
                        }
                    }
                    if (InsertRowDtls != 0)
                    {
                        obj_Comman.ShowPopUpMsg("Record Updated Successfully!", this.Page);
                        MakeControlEmpty();
                        MakeEmptyForm();
                    }
                    else
                    {
                        obj_Comman.ShowPopUpMsg(StrError, this.Page);
                    }
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("please select company!", this.Page);
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int DeleteId = 0;
            if (ViewState["EditID"] != null)
            {
                DeleteId = Convert.ToInt32(ViewState["EditID"]);
            }
            if (DeleteId != 0)
            {
                Entity_PurchaseOrder.POId = DeleteId;
                Entity_PurchaseOrder.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_PurchaseOrder.LoginDate = DateTime.Now;
                Entity_PurchaseOrder.IsDeleted = true;

                int iDelete = Obj_PurchaseOrder.DeleteRecord(ref Entity_PurchaseOrder, out StrError);
                if (iDelete != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_PurchaseOrder = null;
            Obj_PurchaseOrder = null;
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeControlEmpty();
        MakeEmptyForm();
    }

    protected void GrdReqPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {





               

















                #region[Add PURCHASE ORDER RATE ACCORDING TO ITEMS]------------
                if (Convert.ToInt32(((Label)e.Row.FindControl("LblEntryId")).Text) > 0)
                {
                    DataSet DSGRID = new DataSet();
                    DMPurchaseOrder Obj_PO = new DMPurchaseOrder();
                    DSGRID = Obj_PO.BindRATES(Convert.ToInt32(((Label)e.Row.FindControl("LblEntryId")).Text), out StrError);
                    if (DSGRID.Tables.Count > 0)
                    {
                       
                    }
                }
                #endregion-----------

                ((DropDownList)e.Row.FindControl("GrdddlVendor")).DataSource = DSVendor;
                ((DropDownList)e.Row.FindControl("GrdddlVendor")).DataTextField = "SuplierName";
                ((DropDownList)e.Row.FindControl("GrdddlVendor")).DataValueField = "SuplierId";
                ((DropDownList)e.Row.FindControl("GrdddlVendor")).DataBind();
                ((DropDownList)e.Row.FindControl("GrdddlVendor")).SelectedValue = String.IsNullOrEmpty(((TextBox)e.Row.FindControl("GrdtxtVendorID")).Text) ? "0" : ((TextBox)e.Row.FindControl("GrdtxtVendorID")).Text;
                if (Convert.ToInt32(((Label)e.Row.FindControl("LblEntryId")).Text) > 0)
                {
                    DataSet dss = new DataSet();
                    DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
                    dss = Obj_RequisitionCafeteria.BindAvaliableQty(Convert.ToInt32(((Label)e.Row.FindControl("LblEntryId")).Text), Convert.ToInt32(Session["CafeteriaId"].ToString()), out StrError);

                    if (dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
                    {
                        e.Row.Cells[10].Text = dss.Tables[0].Rows[0]["Closing"].ToString() + " - " + dss.Tables[1].Rows[0]["Unit"].ToString();
                        string[] s = (e.Row.Cells[10].Text).Split(' ');
                        string[] s1 = (e.Row.Cells[7].Text).Split(' ');
                        e.Row.Cells[11].Text = (Convert.ToDecimal(s[0]) - Convert.ToDecimal(s1[0])).ToString("#0.00");
                    }
                    else
                    {
                        e.Row.Cells[10].Text = "0--";
                    }
                    Obj_RequisitionCafeteria = null;
                    int cou = e.Row.Cells.Count;
                    DataSet dsUnit = new DataSet();
                    DMPurchaseOrder PO = new DMPurchaseOrder();
                    dsUnit = PO.GETUNITCONVERT(Convert.ToInt32(((Label)e.Row.FindControl("LblEntryId")).Text), Convert.ToInt32(e.Row.Cells[35].Text), e.Row.Cells[5].Text.Equals("&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[5].Text), Convert.ToDecimal(((TextBox)e.Row.FindControl("GrdtxtOrdQty")).Text), out StrError);
                    if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
                    {
                        ((DropDownList)e.Row.FindControl("GrdddlUNITCONVERT")).DataSource = dsUnit;
                        ((DropDownList)e.Row.FindControl("GrdddlUNITCONVERT")).DataTextField = "UnitFactor";
                        ((DropDownList)e.Row.FindControl("GrdddlUNITCONVERT")).DataValueField = "UnitConvDtlsId";
                        ((DropDownList)e.Row.FindControl("GrdddlUNITCONVERT")).DataBind();
                        ((DropDownList)e.Row.FindControl("GrdddlUNITCONVERT")).SelectedValue = dsUnit.Tables[1].Rows[0]["UnitConvDtlsId"].ToString();
                    }
                    else
                    {
                        ((DropDownList)e.Row.FindControl("GrdddlUNITCONVERT")).DataSource = null;
                        ((DropDownList)e.Row.FindControl("GrdddlUNITCONVERT")).DataBind();
                    }

                }
                // TextBox txt=(TextBox)e.Row.FindControl("GrdtxtRate");


            }










            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);

        }
        catch (Exception ex)
        {
        }

    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {
        StrCondition = string.Empty;
        if (ddlDepartment.SelectedValue != "0")
        {
            StrCondition = StrCondition + " AND RC.RequisitionCafeId=" + Convert.ToInt32(ddlDepartment.SelectedValue);
        }
        BindRequisitionGrid(StrCondition);
        GetLocation();
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    protected void GrdReqPO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        int CurrRow = Convert.ToInt32(e.CommandArgument);
                        //int ItemID = Convert.ToInt32(GrdReqPO.Rows[CurrRow].Cells[20].Text);
                        //BindRequisitionDetails(ItemID);
                        string sendstritemid = string.Empty;
                        
                        string SendItemID = sendstritemid.TrimStart(',');
                        DataSet DsReport = new DataSet();
                        DsReport = Obj_PurchaseOrder.GetPurchase_Order_LPR(SendItemID, out StrError);
                        if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
                        {
                            GridViewLPR.DataSource = DsReport.Tables[0];
                            GridViewLPR.DataBind();
                            PopUpYesNoPOR.Show();
                            btnPopHodePOR.Focus();
                        }
                        else
                        {
                            GridViewLPR.DataSource = null;
                            GridViewLPR.DataBind();
                        }

                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                    }
                    break;

                case ("Delete"):
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void hyl_AddAll_Click(object sender, EventArgs e)
    {
        try
        {
            
            //******* Calculate Total Order Qty. ********
            CalculateRemQty();
            MergeRows(PurOrderGrid);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex)
        {
        }
    }

    protected void ImgAddSupplier_Click(object sender, ImageClickEventArgs e)
    {
     
    }

    protected void hylAddCancel_Click(object sender, EventArgs e)
    {
        
    }

    protected void hyl_Hide_Click(object sender, EventArgs e)
    {
        try
        {
            TR_PODtls.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex)
        {
        }
    }

    protected void PurOrderGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToInt32(e.Row.Cells[24].Text) > 0)
                {
                    TotalAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PurchaseRate")) * Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OrdQty"));
                }
                else
                {
                    TotalAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PurchaseRate")) * (GETORDQTY());
                }
                TotalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OrdQty"));
                TotalRate += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PurchaseRate"));
                TotalCGST += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CGSTAmt"));
                TotalSGST += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SGSTAmt"));
                TotalIGST += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "IGSTAmt"));
                // TotalVat += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CGSTAmt"));

                TotalDISC += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "disc"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                txtSubTotal.Text = TotalAmt.ToString("0.00");
                // txtVATAmount.Text = TotalVat.ToString("0.00");
                txtCGSTamt.Text = TotalCGST.ToString("0.00");
                txtSGSTAmt.Text = TotalSGST.ToString("0.00");
                txtIGSTAmt.Text = TotalIGST.ToString("0.00");
                txtDiscount.Text = TotalDISC.ToString("0.00");
                CalculateFooterTotal();
                TotalQty = 0;
                TotalRate = 0;
                TotalAmt = 0;
                TotalVat = TotalDISC = 0;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex)
        {
        }
    }

    protected void PurOrderGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case ("Select"):
                {
                    obj_Comman.ShowPopUpMsg("Edit Mode Temporary Not Available.!", this.Page);
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                    break;
                }
            case ("Delete"):
                {
                    DtEditPO = (DataTable)ViewState["PurchaseOrder"];
                    if (DtEditPO.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                        return;
                    }
                    else
                    {
                        int CurrRow = Convert.ToInt32(e.CommandArgument);

                        if (DtEditPO.Rows.Count > 0)
                        {
                            DtEditPO.Rows[CurrRow].Delete();
                            DtEditPO.AcceptChanges();
                            if (DtEditPO.Rows.Count == 0)
                            {
                                SetInitialRow_PurOrderGrd();
                                return;
                            }
                            PurOrderGrid.DataSource = null;
                            PurOrderGrid.DataSource = DtEditPO;
                            PurOrderGrid.DataBind();
                            ViewState["PurchaseOrder1"] = DtEditPO;

                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                    }
                    break;
                }
            
        }
    }

    protected void PurOrderGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
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
                        DsEdit = Obj_PurchaseOrder.GetPOForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                        if (DsEdit.Tables.Count > 0)
                        {
                            if (DsEdit.Tables[0].Rows.Count > 0)
                            {
                                //DtEditPO = DsEdit.Tables["Table"];
                                //GrdReqPO.DataSource = DsEdit.Tables[0];
                                //GrdReqPO.DataBind();//GrdReqPO
                                //for (int r = 0; r < GrdReqPO.Rows.Count; r++)
                                //{
                                //   // ((DropDownList)GrdReqPO.Rows[r].FindControl("GrdddlVendor")).Enabled = false;
                                //}
                            }
                            if (DsEdit.Tables[1].Rows.Count > 0)
                            {
                                DtEditPO = DsEdit.Tables["Table1"];
                                ViewState["PurchaseOrder"] = DtEditPO;//PurchaseOrderDtls old viewstate
                                ViewState["PurchaseOrder1"] = DtEditPO;//PurchaseOrderDtls old viewstate
                                                                       // ViewState["GridIndex"] = DtEditPO;
                                PurOrderGrid.DataSource = DsEdit.Tables[1];
                                PurOrderGrid.DataBind();

                            }

                            if (DsEdit.Tables[2].Rows.Count > 0)
                            {
                                DtEditPO = DsEdit.Tables["Table2"];
                                ViewState["TermsTable"] = DtEditPO;//PurchaseOrderDtls old viewstate
                                GridTermCond.DataSource = DsEdit.Tables[2];
                                GridTermCond.DataBind();
                                for (int i = 0; i < GridTermCond.Rows.Count; i++)
                                {
                                    ((CheckBox)GridTermCond.Rows[i].Cells[1].FindControl("GrdSelectAll")).Checked = true;
                                    GridTermCond.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#F2FFF8");
                                }
                            }

                            if (DsEdit.Tables[3].Rows.Count > 0)
                            {
                                ddlCompany.SelectedValue = DsEdit.Tables[3].Rows[0]["CompanyID"].ToString();
                                txtDekhrekhAmt.Text = DsEdit.Tables[3].Rows[0]["DekhrekhAmt"].ToString();
                                txtHamaliAmt.Text = DsEdit.Tables[3].Rows[0]["HamaliAmt"].ToString();
                                txtCESSAmt.Text = DsEdit.Tables[3].Rows[0]["CESSAmt"].ToString();
                                txtFreightAmt.Text = DsEdit.Tables[3].Rows[0]["FreightAmt"].ToString();
                                txtPackingAmt.Text = DsEdit.Tables[3].Rows[0]["PackingAmt"].ToString();
                                txtPostageAmt.Text = DsEdit.Tables[3].Rows[0]["PostageAmt"].ToString();
                                txtOtherCharges.Text = DsEdit.Tables[3].Rows[0]["OtherCharges"].ToString();
                                RdoPaymentDays.SelectedValue = DsEdit.Tables[3].Rows[0]["PaymentTerms"].ToString();
                                TXTPOQTNO.Text = DsEdit.Tables[3].Rows[0]["POQTNO"].ToString();
                                TXTPOQTDATE.Text = DsEdit.Tables[3].Rows[0]["POQTDATE"].ToString();
                                txtSerTax.Text = DsEdit.Tables[3].Rows[0]["ServiceTaxAmt"].ToString();
                                DDLSERVICETAX.Items.FindByText(DsEdit.Tables[3].Rows[0]["ServiceTaxPer"].ToString()).Selected = true;
                                TXTNARRATION.Text = DsEdit.Tables[3].Rows[0]["Instruction"].ToString();
                                txtGrandTotal.Text = DsEdit.Tables[3].Rows[0]["GrandTotal"].ToString();

                                txtInstallationRemark.Text = DsEdit.Tables[3].Rows[0]["InstallationRemark"].ToString();
                                txtInstallationCharge.Text = DsEdit.Tables[3].Rows[0]["InstallationCharge"].ToString();
                                txtInstallationServicetax.Text = DsEdit.Tables[3].Rows[0]["InstallationSerTaxPer"].ToString();
                                txtInstallationServiceAmount.Text = DsEdit.Tables[3].Rows[0]["InstallationSerTaxAmt"].ToString();

                                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["HamaliActual"].ToString()) == 1)
                                {
                                    CHKHAMALI.Checked = true;
                                }
                                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["HamaliActual"].ToString()) == 0)
                                {
                                    CHKHAMALI.Checked = false;
                                }

                                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["FreightActual"].ToString()) == 1)
                                {
                                    CHKFreightAmt.Checked = true;
                                }
                                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["FreightActual"].ToString()) == 0)
                                {
                                    CHKFreightAmt.Checked = false;
                                }

                                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["OtherChargeActual"].ToString()) == 1)
                                {
                                    CHKOtherCharges.Checked = true;
                                }
                                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["OtherChargeActual"].ToString()) == 0)
                                {
                                    CHKOtherCharges.Checked = false;
                                }

                                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["LoadingActual"].ToString()) == 1)
                                {
                                    CHKLoading.Checked = true;
                                }
                                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["LoadingActual"].ToString()) == 0)
                                {
                                    CHKLoading.Checked = false;
                                }


                                txtexcisedutyper.Text = DsEdit.Tables[3].Rows[0]["ExcisePer"].ToString();
                                txtexciseduty.Text = DsEdit.Tables[3].Rows[0]["ExciseAmount"].ToString();
                            }
                            BtnSave.Visible = false;
                            BtnUpdate.Visible = true;
                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                    }
                    break;

                case ("Delete"):
                    {
                        int Deleteid = Convert.ToInt32(e.CommandArgument);
                        Entity_PurchaseOrder.POId = Deleteid;
                        Entity_PurchaseOrder.UserId = Convert.ToInt32(Session["UserId"]);
                        Entity_PurchaseOrder.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                        int DeleteDetails = Obj_PurchaseOrder.Delete_PurchaseOrder(ref Entity_PurchaseOrder, out StrError);

                        if (DeleteDetails > 0)
                        {
                            obj_Comman.ShowPopUpMsg("Record Deleted Successfully", this.Page);
                            MakeEmptyForm();
                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                    }
                    break;
                case ("Email"):
                    {
                        int Mailid = Convert.ToInt32(e.CommandArgument);
                        int status = 1;// SendMail(Mailid, 1);
                        if (status == 1)
                        {
                            int GETUPDATES1 = Obj_PurchaseOrder.UpdateInsert_PurchseOrderMAIL(Convert.ToInt32(Mailid), out StrError);

                        }
                        MakeEmptyForm();



                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                    }
                    break;
                case ("CANCELAUTHPO"):
                    {
                        int CurrRow = Convert.ToInt32(e.CommandArgument);
                        //int RequisitnId = Convert.ToInt32(Convert.ToInt32(((Label)ReportGrid.Rows[CurrRow].FindControl("LblEntryId")).Text));
                        BindPurchaseOrderFORCANCELPOPUP(CurrRow);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:ShowPOP();", true);

                    }
                    break;
                case ("MailPO"):
                    {
                        TRLOADING.Visible = false;
                        ViewState["MailID"] = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        string a = gvRow.Cells[8].Text;
                        string mail = db.getDbstatus_Value(" select Email from SuplierMaster where  SuplierId ='" + a + "'");
                        GETDATAFORMAIL(1, 1);
                        TXTKTO.Text = mail.ToString();
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
        try
        {
        }
        catch (Exception ex)
        {
        }
    }

    protected void ReportGrid_DataBound(object sender, EventArgs e)
    {
        try
        {
            for (int rowIndex = ReportGrid.Rows.Count - 1; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = ReportGrid.Rows[rowIndex];
                ImageButton ImageAccepted = (ImageButton)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImageAccepted");
                ImageButton ImageGridEdit = (ImageButton)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImageGridEdit");
                // Image ImagePrint = (Image)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImgBtnPrint");
                ImageButton ImageApprove = (ImageButton)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImageApprove");
                ImageButton ImgBtnDelete = (ImageButton)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImgBtnDelete");
                ImageButton ImgEmail = (ImageButton)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImgEmail");
                ImageButton ImgCANCEL = (ImageButton)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImageButton2");

                string OrderStatus = Convert.ToString(ReportGrid.Rows[rowIndex].Cells[6].Text);
                if (OrderStatus != "Generated")
                {
                    ImageAccepted.Visible = true;
                    ImageGridEdit.Visible = false;
                    // ImagePrint.Visible = true;
                    ImageApprove.Visible = true;
                    ImgBtnDelete.Visible = false;
                    ImgEmail.Visible = true;
                    //ImgCANCEL.Visible = true;
                }
                else
                {
                    ImageAccepted.Visible = false;
                    ImageGridEdit.Visible = true;
                    //   ImagePrint.Visible = false;
                    ImageApprove.Visible = false;
                    ImgBtnDelete.Visible = true;
                    ImgEmail.Visible = false;
                    //ImgCANCEL.Visible = false;
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {



        BtnShow_Click(sender, e);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    protected void GrdReqPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.ReportGrid.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            Ds = Obj_PurchaseOrder.GetPurchase_Order(StrCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ReportGrid.DataSource = Ds.Tables[0];
                this.ReportGrid.DataBind();
                GETHIGHLITE();
            }
            else
            {
                //SetInitialRow_ReportGrid();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void GrdSelectAllHeader1_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox GrdSelectAllHeader = ((CheckBox)GridTermCond.HeaderRow.FindControl("GrdSelectAllHeader"));
        if (GrdSelectAllHeader.Checked == true)
        {
            for (int i = 0; i < GridTermCond.Rows.Count; i++)
            {
                ((CheckBox)GridTermCond.Rows[i].Cells[1].FindControl("GrdSelectAll")).Checked = true;

                // GridUserRight.Rows[i].BackColor = System.Drawing.Color.LightGray;
                GridTermCond.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#F2FFF8");
            }
        }
        else
        {
            for (int i = 0; i < GridTermCond.Rows.Count; i++)
            {
                ((CheckBox)GridTermCond.Rows[i].Cells[0].FindControl("GrdSelectAll")).Checked = false;

                GridTermCond.Rows[i].BackColor = System.Drawing.Color.White;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    protected void img_btn_Add_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["TermsTable"] != null)
            {
                DataTable dtCurrentTableTACForVIEW = (DataTable)ViewState["TermsTable"];
                DataRow drCurrentRow = null;
                //DataRow drCurrentRow; 
                if (dtCurrentTableTACForVIEW.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtCurrentTableTACForVIEW.Rows.Count - 1; i++)
                    {
                        //extract the values
                        dtCurrentTableTACForVIEW.Rows[rowIndex]["#"] = ((Label)GridTermCond.Rows[rowIndex].Cells[0].FindControl("LblEntryId")).Text; //Convert.ToInt32(GridLookUp.Rows[rowIndex].Cells[0].Text); 
                        dtCurrentTableTACForVIEW.Rows[rowIndex]["select"] = ((CheckBox)GridTermCond.Rows[rowIndex].Cells[1].FindControl("GrdSelectAll")).Checked;
                        dtCurrentTableTACForVIEW.Rows[rowIndex]["Title"] = ((TextBox)GridTermCond.Rows[rowIndex].Cells[3].FindControl("GrtxtTermCondition_Head")).Text;
                        dtCurrentTableTACForVIEW.Rows[rowIndex]["TDescptn"] = ((TextBox)GridTermCond.Rows[rowIndex].Cells[4].FindControl("GrtxtDesc")).Text;
                        rowIndex++;
                    }
                    drCurrentRow = dtCurrentTableTACForVIEW.NewRow();

                    drCurrentRow["#"] = 0;
                    drCurrentRow["select"] = false;
                    drCurrentRow["Title"] = "";
                    drCurrentRow["TDescptn"] = "";

                    //add new row to DataTable
                    dtCurrentTableTACForVIEW.Rows.Add(drCurrentRow);

                    //Store the current data to ViewState
                    ViewState["TermsTable"] = dtCurrentTableTACForVIEW;

                    //Rebind the Grid with the current data
                    GridTermCond.DataSource = dtCurrentTableTACForVIEW;

                    GridTermCond.DataBind();
                }
            }

            else
            {
                Response.Write("No Data Present..");

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void rdoPOThrough_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rdoPOThrough.SelectedValue == "0")
        //{
        //    TR_Requision.Visible = true;
        //    TR_Item.Visible = false;
        //    TR_RateList.Visible = TR2.Visible = TR3.Visible = TR5.Visible = TR9.Visible = TR10.Visible = false;
        //    TR_RateList.Visible = false;
        //    trsupcon.Visible = false;
        //    SetInitialRow_ReqDetails();
        //}


        //if (rdoPOThrough.SelectedValue == "2")
        //{
        //    TR_Requision.Visible = false;
        //    TR_Item.Visible = false;
        //    TR_RateList.Visible = TR2.Visible = TR3.Visible = TR5.Visible = TR9.Visible = TR10.Visible = false;
        //    TR_RateList.Visible = false;
        //    trsupcon.Visible = true;
        //    SetInitialRow_ReqDetails();
        //}


        //else
        //{
        //    trsupcon.Visible = false;
        //    TR_Requision.Visible = false;
        //    TR_Item.Visible = true;
        //    TR_RateList.Visible = TR2.Visible = TR3.Visible = TR5.Visible = TR9.Visible = TR10.Visible = true;
        //    TR_RateList.Visible = false;
        //    SetInitialRow_ReqDetails();
        //}
        //ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        //ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsItem_Cat = new DataSet();
        DataSet dsItem_All = new DataSet();
        int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);
        dsItem_Cat = Obj_PurchaseOrder.Fill_Items(categoryId, out StrError);
        dsItem_All = Obj_PurchaseOrder.Fill_AllItem(out StrError);

        if (dsItem_Cat.Tables[0].Rows.Count > 1)
        {
            ddlItem.DataSource = dsItem_Cat.Tables[0];
            ddlItem.DataTextField = "ItemName";
            ddlItem.DataValueField = "ItemId";
            ddlItem.DataBind();

            ddlsubcategory.DataSource = dsItem_Cat.Tables[2];
            ddlsubcategory.DataTextField = "SubCategory";
            ddlsubcategory.DataValueField = "SubCategoryId";
            ddlsubcategory.DataBind();
        }
        else
        {
            ddlItem.DataSource = dsItem_All.Tables[0];
            ddlItem.DataTextField = "ItemName";
            ddlItem.DataValueField = "ItemId";
            ddlItem.DataBind();

            ddlsubcategory.DataSource = dsItem_All.Tables[1];
            ddlsubcategory.DataTextField = "SubCategory";
            ddlsubcategory.DataValueField = "SubCategoryId";
            ddlsubcategory.DataBind();
        }
        TR_RateList.Visible = true;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsItem_Cat = new DataSet();
        int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);
        int subcategory = Convert.ToInt32(ddlsubcategory.SelectedValue);
        dsItem_Cat = Obj_PurchaseOrder.Fill_ItemsFromSUBCAT(categoryId, subcategory, out StrError);
        if (dsItem_Cat.Tables[0].Rows.Count > 1)
        {
            ddlItem.DataSource = dsItem_Cat.Tables[0];
            ddlItem.DataTextField = "ItemName";
            ddlItem.DataValueField = "ItemId";
            ddlItem.DataBind();
        }
        else
        {
            ddlItem.DataSource = null;
            ddlItem.DataBind();
        }
        TR_RateList.Visible = true;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItem.SelectedValue != "0")
        {
            DataSet dsItem_All = new DataSet();
            int ItemId = Convert.ToInt32(ddlItem.SelectedValue);
            dsItem_All = Obj_PurchaseOrder.Fill_SupplierRate(ItemId, out StrError);


            float a = db.getDb_Value("select  PurchaseRate from ItemDetails where  ItemId='" + ItemId + "'");

            lstSupplierRate.Text = a.ToString();

            if (dsItem_All.Tables[0].Rows.Count > 0)
            {
                ddlItemDesc.DataSource = dsItem_All.Tables[2];
                ddlItemDesc.DataTextField = "ItemDesc";
                ddlItemDesc.DataValueField = "ItemDetailsId";
                ddlItemDesc.DataBind();

                ddlUNIT.DataSource = dsItem_All.Tables[3];
                ddlUNIT.DataTextField = "Unit";
                ddlUNIT.DataValueField = "UnitId";
                ddlUNIT.DataBind();


                for (int i = 0; i < ddlUNIT.Items.Count; i++)
                {
                    ddlUNIT.Items[i].Attributes.Add("Title", dsItem_All.Tables[3].Rows[i]["Factor"].ToString());
                    UNITFACTOR.Add(dsItem_All.Tables[3].Rows[i]["Factor"].ToString());
                }
               
                txtvatper.Text = dsItem_All.Tables[1].Rows[0]["TaxPer"].ToString();
                txtdiscper.Text = txtdiscamt.Text = "0.00";
                txtvatper_TextChanged(sender, e);
            }
            TR_RateList.Visible = true;

        }
        else
        {
            TR_RateList.Visible = false;
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lstSupplierRate.Text.Trim()))
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemOrdQty.Text) && Convert.ToDecimal(txtItemOrdQty.Text) > 0)
                {
                    string MainString = lstSupplierRate.Text;
                    string SupId = string.Empty;
                    string Rate = string.Empty;
                    int IDLenght = MainString.IndexOf(')');
                    //int RateStart = MainString.IndexOf('@');
                    //  int RateEnd = MainString.IndexOf("Rs/-");
                    SupId = drpsupplier.SelectedValue;
                   // Rate = MainString.Substring(RateStart + 2, ((RateEnd) - (RateStart + 2)));
                    Rate = lstSupplierRate.Text;

                    Ds = Obj_PurchaseOrder.Get_ItemWiseOrd(Convert.ToInt32(ddlItem.SelectedValue), Convert.ToInt32(SupId), Convert.ToDecimal(Rate.Trim()), Convert.ToDecimal(txtItemOrdQty.Text), out StrError);

                    if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                    {
                        Ds.Tables[0].Rows[0]["pervat"] = txtvatper.Text;
                        Ds.Tables[0].Rows[0]["vat"] = txtvatamt.Text;


                        Ds.Tables[0].Rows[0]["perGST"] = txtvatamt.Text;
                        Ds.Tables[0].Rows[0]["CGSTPer"] = txtCGSTItemPer.Text;
                        Ds.Tables[0].Rows[0]["CGSTAmt"] = txtCGSTItemAmt.Text;

                        Ds.Tables[0].Rows[0]["SGSTPer"] = txtSGSTItemPer.Text;
                        Ds.Tables[0].Rows[0]["SGSTAmt"] = txtSGSTItemAmt.Text;
                        Ds.Tables[0].Rows[0]["IGSTPer"] = txtIGSTItemPer.Text;
                        Ds.Tables[0].Rows[0]["IGSTAmt"] = txtIGSTItemAmt.Text;

                        Ds.Tables[0].Rows[0]["perdisc"] = txtdiscper.Text;
                        Ds.Tables[0].Rows[0]["disc"] = txtdiscamt.Text;
                        AddItemIn_POGrid(Ds);

                        UNITFACTOR.RemoveRange(0, UNITFACTOR.Count);
                        MergeRows(PurOrderGrid);
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }

    protected void txtvatper_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lstSupplierRate.Text.Trim()))
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemOrdQty.Text) && Convert.ToDecimal(txtItemOrdQty.Text) > 0)
                {

                    string MainString = lstSupplierRate.Text;
                    string Rate = string.Empty;
                    //int IDLenght = MainString.IndexOf(')');
                    //int RateStart = MainString.IndexOf('@');
                    //int RateEnd = MainString.IndexOf("Rs/-");
                    Rate = lstSupplierRate.Text;
                    txtdiscper.Text = string.IsNullOrEmpty(txtdiscper.Text) ? "0" : txtdiscper.Text;
                    txtvatper.Text = string.IsNullOrEmpty(txtvatper.Text) ? "0" : txtvatper.Text;
                    txtdiscamt.Text = ((Convert.ToDecimal(txtdiscper.Text) / 100) * (Convert.ToDecimal(txtItemOrdQty.Text) * Convert.ToDecimal(Rate))).ToString("#0.00");
                    txtvatamt.Text = ((Convert.ToDecimal(txtvatper.Text) / 100) * ((Convert.ToDecimal(txtItemOrdQty.Text) * Convert.ToDecimal(Rate)) - Convert.ToDecimal(txtdiscamt.Text))).ToString("#0.00");
                    ddlUNIT_SelectedIndexChanged(sender, e);
                    txtdiscper.Focus();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }

    protected void txtdiscper_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lstSupplierRate.Text.Trim()))
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemOrdQty.Text) && Convert.ToDecimal(txtItemOrdQty.Text) > 0)
                {
                    string MainString = lstSupplierRate.Text;
                    string Rate = string.Empty;
                    //int IDLenght = MainString.IndexOf(')');
                    //int RateStart = MainString.IndexOf('@');
                    //int RateEnd = MainString.IndexOf("Rs/-");
                   // Rate = MainString.Substring(RateStart + 2, ((RateEnd) - (RateStart + 2)));
                    Rate = lstSupplierRate.Text;
                    txtdiscper.Text = string.IsNullOrEmpty(txtdiscper.Text) ? "0" : txtdiscper.Text;
                    txtvatper.Text = string.IsNullOrEmpty(txtvatper.Text) ? "0" : txtvatper.Text;
                    txtdiscamt.Text = ((Convert.ToDecimal(txtdiscper.Text) / 100) * (Convert.ToDecimal(txtItemOrdQty.Text) * Convert.ToDecimal(Rate))).ToString("#0.00");
                    txtvatamt.Text = ((Convert.ToDecimal(txtvatper.Text) / 100) * ((Convert.ToDecimal(txtItemOrdQty.Text) * Convert.ToDecimal(Rate)) - Convert.ToDecimal(txtdiscamt.Text))).ToString("#0.00");
                    ddlUNIT_SelectedIndexChanged(sender, e);
                    BtnAdd.Focus();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }

    protected void txtItemOrdQty_TextChanged(object sender, EventArgs e)
    {
        txtvatper_TextChanged(sender, e);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }

    protected void lstSupplierRate_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtvatper_TextChanged(sender, e);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
    }

    protected void ddlUNIT_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(lstSupplierRate.Text.Trim()))
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemOrdQty.Text) && Convert.ToDecimal(txtItemOrdQty.Text) > 0)
                {
                    decimal Factor = Convert.ToDecimal(UNITFACTOR[ddlUNIT.SelectedIndex].ToString());
                    string MainString = lstSupplierRate.Text;
                    string Rate = string.Empty;
                    int IDLenght = MainString.IndexOf(')');
                    int RateStart = MainString.IndexOf('@');
                    int RateEnd = MainString.IndexOf("Rs/-");
                    Rate = MainString.Substring(RateStart + 2, ((RateEnd) - (RateStart + 2)));
                    txtdiscper.Text = string.IsNullOrEmpty(txtdiscper.Text) ? "0" : txtdiscper.Text;
                    txtvatper.Text = string.IsNullOrEmpty(txtvatper.Text) ? "0" : txtvatper.Text;
                    txtdiscamt.Text = ((Convert.ToDecimal(txtdiscper.Text) / 100) * (((Convert.ToDecimal(txtItemOrdQty.Text)) / Factor) * Convert.ToDecimal(Rate))).ToString("#0.00");
                    txtvatamt.Text = ((Convert.ToDecimal(txtvatper.Text) / 100) * ((((Convert.ToDecimal(txtItemOrdQty.Text)) / Factor) * Convert.ToDecimal(Rate)) - Convert.ToDecimal(txtdiscamt.Text))).ToString("#0.00");
                    txtdiscper.Focus();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
            }
            catch (Exception ex) { }
            finally
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
            }
        }

    }

    protected void CHKLINKLASTPURCHASE_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkShowProcess1 = (CheckBox)sender;
        GridViewRow row = (GridViewRow)ChkShowProcess1.NamingContainer;
        RadioButtonList GridTemplateVendor = (RadioButtonList)row.FindControl("RdoLASTPURCHASE");
        int CurrRow = row.RowIndex;
       
        ChkShowProcess1.Checked = false;
        Panel PnlGrid = (Panel)row.FindControl("PnlGrid");
        PopupControlExtender popup = AjaxControlToolkit.PopupControlExtender.GetProxyForCurrentPopup(Page);
        popup.Commit("Popup");
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        
    }

    protected void CHKLINKSUPPLIERRATE_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkShowProcess1 = (CheckBox)sender;
        GridViewRow row = (GridViewRow)ChkShowProcess1.NamingContainer;
        GridView GridTemplateVendor = (GridView)row.FindControl("GridLINKSUPPLIERRATE");
        int CurrRow = row.RowIndex;
        int Pos = 0;
        if (ChkShowProcess1.Checked == true)
        {
           

        }

        ChkShowProcess1.Checked = false;
        Panel PnlGrid = (Panel)row.FindControl("PanelLINKSUPPLIERRATE");
        PopupControlExtender popup = AjaxControlToolkit.PopupControlExtender.GetProxyForCurrentPopup(Page);
        popup.Commit("PopupLINKSUPPLIERRATE");
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
      
    }

    protected void ChkLINKALLRECORD_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkShowProcess1 = (CheckBox)sender;
        GridViewRow row = (GridViewRow)ChkShowProcess1.NamingContainer;
        GridView GridTemplateVendor = (GridView)row.FindControl("GridLINKALLRECORD");
        int CurrRow = row.RowIndex;
        int Pos = 0;
        if (ChkShowProcess1.Checked == true)
        {
          
        }

        ChkShowProcess1.Checked = false;
        Panel PnlGrid = (Panel)row.FindControl("PanelLINKALLRECORD");
        PopupControlExtender popup = AjaxControlToolkit.PopupControlExtender.GetProxyForCurrentPopup(Page);
        popup.Commit("PanelLINKALLRECORD");
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
       
    }

    protected void BTNCANCELPO_Click(object sender, EventArgs e)
    {
        
       

    }

    protected void BTNPOPUPTERMS_Click(object sender, EventArgs e)
    {
       

    }

    public void MergeRows(GridView gridView)
    {

        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            if (row.Cells[8].Text == previousRow.Cells[8].Text)
            {
                row.Cells[8].RowSpan = previousRow.Cells[8].RowSpan < 2 ? 2 :
                                       previousRow.Cells[8].RowSpan + 1;
                previousRow.Cells[8].Visible = false;

                row.Cells[20].RowSpan = previousRow.Cells[20].RowSpan < 2 ? 2 :
                                      previousRow.Cells[20].RowSpan + 1;
                previousRow.Cells[20].Visible = false;
            }


        }
    }

    protected void ONLOADALLRECORD_OnLoad(object sender, EventArgs e)
    {
        try
        {
            string name = Page.Form.Target.ToString();

            if (SampleFlag == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
            }

        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }

    protected void GrdddlUNITCONVERT_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region[Convert Quantity accordng to UnitFactor]
      
        #endregion
    }

    protected void RejectIndentItem_CheckedChanged(object sender, EventArgs e)
    {
        #region[For rejecting items from Indent]
        if (RejectIndentItem.Checked == true)
        {
            PopUpYesNo.Show();
            btnPopUpYes.Focus();
        }

        #endregion
    }

    protected void PopUpYesNo_Command(object sender, CommandEventArgs e)
    {
       
    }

    protected void PopUpYesNoPOR_Command(object sender, CommandEventArgs e)
    {

      
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
                string description = Server.HtmlDecode("HELLO,<br /><br />PLEASE FIND ATTACHED A PURCHASE ORDER OF THE MATERIAL REQUIRED BY US. PLEASE PROVIDE MATERIAL AS MENTION IN PO.<br /><br />IF THE MATERIAL REQUIRED IS NOT AVAILABLE, THEN PLEASE SUGGEST ALTERNATIVE MATERIAL.<br /><br />REGARDS,<br />" + DDLKCMPY.SelectedItem.ToString()).Replace("<br />", Environment.NewLine);
                TxtBody.Text = description;
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

    #region IndentItemRateHistory
    protected void btnShowIndentItmRateHist_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedIndex > 0)
            {
                //show pop up of Indent Items rate history
                PopLblIndentNo.Text = "Indent No : - " + ddlDepartment.SelectedItem.Text.ToString();
                PopUpIndentItemRateHistory.Show();
                //StrCondition = string.Empty;
                //StrCondition = StrCondition + " AND RC.RequisitionCafeId=" + Convert.ToInt32(ddlDepartment.SelectedValue);
                Ds = Obj_PurchaseOrder.GetOrderRequsition_ItemRateHistory(long.Parse(ddlDepartment.SelectedValue.ToString()), out StrError);
                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    grdPopIndentItmRates.DataSource = Ds.Tables[0];
                    grdPopIndentItmRates.DataBind();

                }
                else
                {
                    grdPopIndentItmRates.DataSource = null;
                    grdPopIndentItmRates.DataBind();
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Please select Indent form list...!", this.Page);
                ddlDepartment.Focus();
            }
        }
        catch (Exception exp)
        {
            throw;
        }
    }
    protected void btnPopIndentItmRateHistClose_Click(object sender, EventArgs e)
    {
        try
        {
            PopUpIndentItemRateHistory.Hide();
        }
        catch (Exception exp)
        {
            throw;
        }
    }
    #endregion

    protected void ReportGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label GRDPOSTATUS = (Label)e.Row.FindControl("GRDPOSTATUS");

            if ((e.Row.Cells[6].Text) == "Generated")
            {
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
            }
            if ((e.Row.Cells[6].Text) == "Approved")
            {
                e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
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
                e.Row.Cells[9].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;


            }
            if ((e.Row.Cells[6].Text) == "Authorised")
            {
                e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[2].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[3].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[4].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[5].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[6].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[7].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[9].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[9].ForeColor = System.Drawing.Color.White;
            }

            if (e.Row.Cells[9].Text.ToString() == "Mail Send")
            {
                e.Row.Cells[9].BackColor = System.Drawing.Color.Brown;
                e.Row.Cells[9].ForeColor = System.Drawing.Color.White;
            }

        }
    }

    #region CheckBoxEvent
    protected void ChkCGST_CheckedChanged(Object sender, EventArgs e)
    {
        ChkIGST.Checked = false;
        ChkSGST.Checked = true;

        FillGST();

    }
    protected void ChkSGST_CheckedChanged(Object sender, EventArgs e)
    {

        ChkIGST.Checked = false;
        ChkCGST.Checked = true;
        FillGST();

    }
    protected void ChkIGST_CheckedChanged(Object sender, EventArgs e)
    {
        ChkSGST.Checked = false;
        ChkCGST.Checked = false;
        FillGST();

    }
    #endregion


    protected void GrdddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        // DataSet DSA = new DataSet();

      



        decimal CGSTPer, SGSTPer, IGSTAmt, CGSTAmt, SGSTAmt;
        //DropDownList txt = (DropDownList)sender;
        //GridViewRow grd = (GridViewRow)txt.Parent.Parent;

        DropDownList lb = (DropDownList)sender;
        GridViewRow grd = (GridViewRow)lb.NamingContainer;
        DataSet DSJ = new DataSet();
        try
        {

            DropDownList GrdddlVendor = ((DropDownList)grd.FindControl("GrdddlVendor"));
            Entity_PurchaseOrder.SuplierId = Convert.ToInt32(GrdddlVendor.SelectedValue);

            DSJ = Obj_PurchaseOrder.BindSuppDtls(ref Entity_PurchaseOrder, out StrError);

            if (DSJ.Tables.Count > 0)
            {
                if (DSJ.Tables[0].Rows.Count > 0)
                {

                    if ((Convert.ToInt32(DSJ.Tables[0].Rows[0]["StateId"]) == 13))
                    {
                        TextBox TxtGSTPerDetails = ((TextBox)grd.FindControl("GrdtxtPerVAT"));
                        TextBox TxtCGSTPer = ((TextBox)grd.FindControl("GrdtxtCGSTPer"));
                        TextBox TxtSGSTPer = ((TextBox)grd.FindControl("GrdtxtSGSTPer"));
                        TextBox txtCGSTTotalAmt = ((TextBox)grd.FindControl("GrdtxtCGSTAmt"));
                        TextBox txtSGSTTotalAmt = ((TextBox)grd.FindControl("GrdtxtSGSTAmt"));
                        TextBox GrdtxtOrdQty = ((TextBox)grd.FindControl("GrdtxtOrdQty"));
                        TextBox GrdtxtRate = ((TextBox)grd.FindControl("GrdtxtRate"));


                        CGSTPer = Convert.ToDecimal(TxtGSTPerDetails.Text) / 2;
                        SGSTPer = Convert.ToDecimal(TxtGSTPerDetails.Text) / 2;

                        TxtCGSTPer.Text = CGSTPer.ToString();
                        TxtSGSTPer.Text = SGSTPer.ToString();

                        CGSTAmt = (Convert.ToDecimal(GrdtxtOrdQty.Text) * Convert.ToDecimal(GrdtxtRate.Text)) * Convert.ToDecimal(TxtCGSTPer.Text) / 100;
                        SGSTAmt = (Convert.ToDecimal(GrdtxtOrdQty.Text) * Convert.ToDecimal(GrdtxtRate.Text)) * Convert.ToDecimal(TxtSGSTPer.Text) / 100;

                        txtCGSTTotalAmt.Text = CGSTAmt.ToString("0.00");
                        txtSGSTTotalAmt.Text = SGSTAmt.ToString("0.00");




                    }
                    else
                    {
                        TextBox TxtGSTPerDetails = ((TextBox)grd.FindControl("GrdtxtPerVAT"));
                        TextBox TxtIGST = ((TextBox)grd.FindControl("GrdtxtIGSTPer"));
                        TextBox TxtCGSTPer = ((TextBox)grd.FindControl("GrdtxtCGSTPer"));
                        TextBox TxtSGSTPer = ((TextBox)grd.FindControl("GrdtxtSGSTPer"));
                        TextBox GrdtxtOrdQty = ((TextBox)grd.FindControl("GrdtxtOrdQty"));
                        TextBox GrdtxtRate = ((TextBox)grd.FindControl("GrdtxtRate"));
                        TextBox txtIGSTTotalAmt = ((TextBox)grd.FindControl("GrdtxtIGSTAmt"));
                        TextBox txtCGSTTotalAmt = ((TextBox)grd.FindControl("GrdtxtCGSTAmt"));
                        TextBox txtSGSTTotalAmt = ((TextBox)grd.FindControl("GrdtxtSGSTAmt"));

                        TxtIGST.Text = TxtGSTPerDetails.Text;

                        IGSTAmt = (Convert.ToDecimal(GrdtxtOrdQty.Text) * Convert.ToDecimal(GrdtxtRate.Text)) * Convert.ToDecimal(TxtIGST.Text) / 100;
                        txtIGSTTotalAmt.Text = IGSTAmt.ToString("0.00");

                        TxtCGSTPer.Text = "0";
                        TxtSGSTPer.Text = "0";
                        txtCGSTTotalAmt.Text = "0";
                        txtSGSTTotalAmt.Text = "0";
                    }





                }

            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void drpenquiry_SelectedIndexChanged(object sender, EventArgs e)
    {
        //drpsupplier.DataSource = db.Displaygrid("select distinct(supplier)  from Conversionpo  where enquiry ='" + drpenquiry.SelectedValue + "'  ");
        //drpsupplier.DataTextField = "supplier";
        //drpsupplier.DataValueField = "supplier";
        //drpsupplier.DataBind();
        //drpsupplier.Items.Insert(0, "-Select Supplier  -");
    }

    protected void drpsupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
       

      

    }

    protected void txtSGSTItemPer_TextChanged(object sender, EventArgs e)
    {


        if (!string.IsNullOrEmpty(lstSupplierRate.Text.Trim()))
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemOrdQty.Text) && Convert.ToDecimal(txtItemOrdQty.Text) > 0)
                {
                    string MainString = lstSupplierRate.Text;
                    string Rate = string.Empty;
                    //int IDLenght = MainString.IndexOf(')');
                    //int RateStart = MainString.IndexOf('@');
                    //int RateEnd = MainString.IndexOf("Rs/-");
                    // Rate = MainString.Substring(RateStart + 2, ((RateEnd) - (RateStart + 2)));
                    Rate = lstSupplierRate.Text;
                    string abc = txtSGSTItemPer.Text;
                    txtSGSTItemAmt.Text = ((Convert.ToDecimal(txtSGSTItemPer.Text) / 100) * (Convert.ToDecimal(txtItemOrdQty.Text) * Convert.ToDecimal(Rate))).ToString("#0.00");
                  //  txtvatamt.Text = ((Convert.ToDecimal(txtvatper.Text) / 100) * ((Convert.ToDecimal(txtItemOrdQty.Text) * Convert.ToDecimal(Rate)) - Convert.ToDecimal(txtdiscamt.Text))).ToString("#0.00");
                    ddlUNIT_SelectedIndexChanged(sender, e);
                    BtnAdd.Focus();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }

    protected void txtCGSTItemPer_TextChanged(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(lstSupplierRate.Text.Trim()))
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemOrdQty.Text) && Convert.ToDecimal(txtItemOrdQty.Text) > 0)
                {
                    string MainString = lstSupplierRate.Text;
                    string Rate = string.Empty;
                    //int IDLenght = MainString.IndexOf(')');
                    //int RateStart = MainString.IndexOf('@');
                    //int RateEnd = MainString.IndexOf("Rs/-");
                    // Rate = MainString.Substring(RateStart + 2, ((RateEnd) - (RateStart + 2)));
                    Rate = lstSupplierRate.Text;
                    string abc = txtSGSTItemPer.Text;
                    txtCGSTItemAmt.Text = ((Convert.ToDecimal(txtCGSTItemPer.Text) / 100) * (Convert.ToDecimal(txtItemOrdQty.Text) * Convert.ToDecimal(Rate))).ToString("#0.00");
                  //  txtvatamt.Text = ((Convert.ToDecimal(txtvatper.Text) / 100) * ((Convert.ToDecimal(txtItemOrdQty.Text) * Convert.ToDecimal(Rate)) - Convert.ToDecimal(txtdiscamt.Text))).ToString("#0.00");
                    ddlUNIT_SelectedIndexChanged(sender, e);
                    BtnAdd.Focus();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }

    protected void txtIGSTItemPer_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lstSupplierRate.Text.Trim()))
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemOrdQty.Text) && Convert.ToDecimal(txtItemOrdQty.Text) > 0)
                {
                    string MainString = lstSupplierRate.Text;
                    string Rate = string.Empty;
                    //int IDLenght = MainString.IndexOf(')');
                    //int RateStart = MainString.IndexOf('@');
                    //int RateEnd = MainString.IndexOf("Rs/-");
                    // Rate = MainString.Substring(RateStart + 2, ((RateEnd) - (RateStart + 2)));
                    Rate = lstSupplierRate.Text;
                    string abc = txtSGSTItemPer.Text;
                    txtIGSTItemAmt.Text = ((Convert.ToDecimal(txtIGSTItemPer.Text) / 100) * (Convert.ToDecimal(txtItemOrdQty.Text) * Convert.ToDecimal(Rate))).ToString("#0.00");
                  //  txtvatamt.Text = ((Convert.ToDecimal(txtvatper.Text) / 100) * ((Convert.ToDecimal(txtItemOrdQty.Text) * Convert.ToDecimal(Rate)) - Convert.ToDecimal(txtdiscamt.Text))).ToString("#0.00");
                    ddlUNIT_SelectedIndexChanged(sender, e);
                    BtnAdd.Focus();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}