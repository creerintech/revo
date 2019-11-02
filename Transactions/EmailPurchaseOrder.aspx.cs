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

public partial class Transactions_EmailPurchaseOrder : System.Web.UI.Page
{
    #region [private variables]

    DMPurchaseOrder Obj_PurchaseOrder = new DMPurchaseOrder();
    PurchaseOrder Entity_PurchaseOrder = new PurchaseOrder();

    DMEmailPO OBJ_EmailPO = new DMEmailPO();
    EmailPO Entity_EmailPO = new EmailPO();

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
    decimal TotalDISC = 0;
    decimal TotalNETAmt = 0;


    private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
    bool FlagSort = false;

    #endregion

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

                DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Email Purchase Order'");
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
                    //BtnSave.Visible = false;
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
            GETPDF2(); 

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
            Mail.From = new MailAddress("purchase@kariadevelopers.com"); //"purchase@kariadevelopers.com";
            if (CHKATTACHBROUCHER.Checked == true)
            {
                //Mail.Attachments.Add(new System.Web.Mail.MailAttachment(sFile));
                //Mail.Attachments.Add(Mail.Attachments());
                Mail.Attachments.Add(new Attachment(path));
                Mail.Attachments.Add(new Attachment("C:/Users/comp01/Downloads/Kunal Passports.pdf"));
            }
            Mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.rediffmailpro.com", 587);
            smtp.Host = "smtp.rediffmailpro.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;//true
            smtp.Credentials = new System.Net.NetworkCredential("purchase@kariadevelopers.com", "mehta123");



            smtp.EnableSsl = false;//false
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
    public string GETPDF2()
    {
        return "C:/Users/comp01/Downloads/Kunal Passports.pdf"; 
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
        //iframepdf.Attributes.Add("src","C:/Users/comp01/Downloads/Kunal Passports.pdf");
        
        return PDFMaster;
    }

    private void MakeEmptyForm()
    {
        SampleFlag = 0;
        ViewState["CheckExistingItem"] = null;
        ViewState["EditId"] = null;

        //txtFromDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
        //TXTPOQTDATE.Text = TxtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        //ddlDepartment.SelectedValue = "0";
        //txtInstallationRemark.Text = TXTNARRATION.Text = TxtSearch.Text = "";
        //txtSubTotal.Text = "0.00";
        //txtDiscount.Text = "0.00";
        //txtVATAmount.Text = "0.00";
        //txtexciseduty.Text = txtNetTotal.Text = "0.00";
        //txtexcisedutyper.Text = "0";
        //CHKHAMALI.Checked = CHKFreightAmt.Checked = CHKOtherCharges.Checked = CHKLoading.Checked = false;
        //txtInstallationServiceAmount.Text = txtInstallationServicetax.Text = txtInstallationCharge.Text = txtSerTax.Text = txtDekhrekhAmt.Text = txtHamaliAmt.Text = txtCESSAmt.Text = txtFreightAmt.Text = txtPackingAmt.Text = txtPostageAmt.Text = txtOtherCharges.Text = txtGrandTotal.Text = "0.00";
        //DDLSERVICETAX.SelectedValue = "0";
        //if (!FlagAdd)
        //    BtnSave.Visible = true;
        //BtnUpdate.Visible = false;
        //BtnDelete.Visible = false;

        //TR_PODtls.Visible = false;
        //rdoPOThrough.SelectedValue = "0";
        //TR_Requision.Visible = true;
        //TR_Item.Visible = false;
        //TR_RateList.Visible = false;
        //TR_RateList.Visible = TR2.Visible = TR3.Visible = TR5.Visible = false;

        //SetInitialRow_ReqDetails();
        //SetInitialRow_PODetails();
        //SetInitialRow_PurOrderGrd();
        //SetInitialRow_TermsCondition();
        //SetInitialRowFORPOCANCEL();
        //BindRequisitionGrid(StrCondition = string.Empty);
        BindReportGrid(StrCondition);
        //GETHIGHLITE();
        //BindTermsAndCond(StrCondition);
        //Accordion1.SelectedIndex = 1;
        //Accordion2.SelectedIndex = 1;

        //SetInitialRowReorderGrid();//Reorder Items Details
        //SetInitialRow_LastPurchaseOrderGrid();
        //#region[UserRights]
        //if (FlagEdit)
        //{
        //    foreach (GridViewRow GRow in ReportGrid.Rows)
        //    {
        //        GRow.FindControl("ImageGridEdit").Visible = false;
        //    }
        //}
        //if (FlagDel)
        //{
        //    foreach (GridViewRow GRow in ReportGrid.Rows)
        //    {
        //        GRow.FindControl("ImgBtnDelete").Visible = false;
        //    }
        //}
        //if (FlagPrint)
        //{
        //    foreach (GridViewRow GRow in ReportGrid.Rows)
        //    {
        //        GRow.FindControl("ImgBtnPrint").Visible = false;
        //    }
        //}
        //#endregion

        GETHIGHLITE();
        //ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        // ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //AccordionPane1.HeaderContainer.Width = 1030;
            //AccordionPane2.HeaderContainer.Width = 1030;
            //FillCombo();
            MakeControlEmpty();
            MakeEmptyForm();
            CheckUserRight();
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        //else
        //{   
        //    DtEditPO = (DataTable)ViewState["PurchaseOrder"];
        //    if (TXTJAVASCRIPTFLAG.Text.Equals("Open"))
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOPForceClose(3);", true);
        //        TXTJAVASCRIPTFLAG.Text = "";
        //    }
        //}
    }

    public void BindReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            DsReport = OBJ_EmailPO.GetPurchase_Order(RepCondition, out StrError);
            ViewState["GridDS"] = DsReport.Tables[0];
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

    protected void DDLKCMPY_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (Convert.ToInt32(DDLKCMPY.SelectedValue.ToString()) > 0)
        //    {
        //        //GETCLIENTPDF(Convert.ToInt32(LBLID.Text.ToString()), Convert.ToInt32(DDLKCMPY.SelectedValue.ToString()));
        //        string description = Server.HtmlDecode("HELLO,<br /><br />PLEASE FIND ATTACHED A PURCHASE ORDER OF THE MATERIAL REQUIRED BY US. PLEASE PROVIDE MATERIAL AS MENTION IN PO.<br /><br />IF THE MATERIAL REQUIRED IS NOT AVAILABLE, THEN PLEASE SUGGEST ALTERNATIVE MATERIAL.<br /><br />REGARDS,<br />" + DDLKCMPY.SelectedItem.ToString()).Replace("<br />", Environment.NewLine);
        //        TxtBody.Text = description;
        //        MDPopUpYesNoMail.Show();
        //        BtnPopMail2.Focus();
        //    }
        //}
        //catch (Exception ex) { }
        //finally { }
    }

    protected void TXTKTO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TXTKTO.Text = TOSTRING + TXTKTO.Text.ToString();
            TXTKTO.Text = TXTKTO.Text.ToString().Replace(",,", ",");

            MDPopUpYesNoMail2.Show();
            BtnPopMail2.Focus();
        }
        catch (Exception ex)
        {
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

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = string.Empty;
        StrCondition = TxtSearch.Text.Trim();
        Ds = new DataSet();
        Ds = OBJ_EmailPO.GetPOList(StrCondition, out StrError);
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

    protected void ReportGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                //case ("Select"):
                //    {
                //        ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                //        DsEdit = Obj_PurchaseOrder.GetPOForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                //        if (DsEdit.Tables.Count > 0)
                //        {
                //            if (DsEdit.Tables[0].Rows.Count > 0)
                //            {
                //                //DtEditPO = DsEdit.Tables["Table"];
                //                //GrdReqPO.DataSource = DsEdit.Tables[0];
                //                //GrdReqPO.DataBind();//GrdReqPO
                //                //for (int r = 0; r < GrdReqPO.Rows.Count; r++)
                //                //{
                //                //   // ((DropDownList)GrdReqPO.Rows[r].FindControl("GrdddlVendor")).Enabled = false;
                //                //}
                //            }
                //            if (DsEdit.Tables[1].Rows.Count > 0)
                //            {
                //                DtEditPO = DsEdit.Tables["Table1"];
                //                ViewState["PurchaseOrder"] = DtEditPO;//PurchaseOrderDtls old viewstate
                //                ViewState["PurchaseOrder1"] = DtEditPO;//PurchaseOrderDtls old viewstate
                //                // ViewState["GridIndex"] = DtEditPO;
                //                PurOrderGrid.DataSource = DsEdit.Tables[1];
                //                PurOrderGrid.DataBind();

                //            }

                //            if (DsEdit.Tables[2].Rows.Count > 0)
                //            {
                //                DtEditPO = DsEdit.Tables["Table2"];
                //                ViewState["TermsTable"] = DtEditPO;//PurchaseOrderDtls old viewstate
                //                GridTermCond.DataSource = DsEdit.Tables[2];
                //                GridTermCond.DataBind();
                //                for (int i = 0; i < GridTermCond.Rows.Count; i++)
                //                {
                //                    ((CheckBox)GridTermCond.Rows[i].Cells[1].FindControl("GrdSelectAll")).Checked = true;
                //                    GridTermCond.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#F2FFF8");
                //                }
                //            }

                //            if (DsEdit.Tables[3].Rows.Count > 0)
                //            {
                //                ddlCompany.SelectedValue = DsEdit.Tables[3].Rows[0]["CompanyID"].ToString();
                //                txtDekhrekhAmt.Text = DsEdit.Tables[3].Rows[0]["DekhrekhAmt"].ToString();
                //                txtHamaliAmt.Text = DsEdit.Tables[3].Rows[0]["HamaliAmt"].ToString();
                //                txtCESSAmt.Text = DsEdit.Tables[3].Rows[0]["CESSAmt"].ToString();
                //                txtFreightAmt.Text = DsEdit.Tables[3].Rows[0]["FreightAmt"].ToString();
                //                txtPackingAmt.Text = DsEdit.Tables[3].Rows[0]["PackingAmt"].ToString();
                //                txtPostageAmt.Text = DsEdit.Tables[3].Rows[0]["PostageAmt"].ToString();
                //                txtOtherCharges.Text = DsEdit.Tables[3].Rows[0]["OtherCharges"].ToString();
                //                RdoPaymentDays.SelectedValue = DsEdit.Tables[3].Rows[0]["PaymentTerms"].ToString();
                //                TXTPOQTNO.Text = DsEdit.Tables[3].Rows[0]["POQTNO"].ToString();
                //                TXTPOQTDATE.Text = DsEdit.Tables[3].Rows[0]["POQTDATE"].ToString();
                //                txtSerTax.Text = DsEdit.Tables[3].Rows[0]["ServiceTaxAmt"].ToString();
                //                DDLSERVICETAX.Items.FindByText(DsEdit.Tables[3].Rows[0]["ServiceTaxPer"].ToString()).Selected = true;
                //                TXTNARRATION.Text = DsEdit.Tables[3].Rows[0]["Instruction"].ToString();
                //                txtGrandTotal.Text = DsEdit.Tables[3].Rows[0]["GrandTotal"].ToString();

                //                txtInstallationRemark.Text = DsEdit.Tables[3].Rows[0]["InstallationRemark"].ToString();
                //                txtInstallationCharge.Text = DsEdit.Tables[3].Rows[0]["InstallationCharge"].ToString();
                //                txtInstallationServicetax.Text = DsEdit.Tables[3].Rows[0]["InstallationSerTaxPer"].ToString();
                //                txtInstallationServiceAmount.Text = DsEdit.Tables[3].Rows[0]["InstallationSerTaxAmt"].ToString();

                //                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["HamaliActual"].ToString()) == 1)
                //                {
                //                    CHKHAMALI.Checked = true;
                //                }
                //                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["HamaliActual"].ToString()) == 0)
                //                {
                //                    CHKHAMALI.Checked = false;
                //                }

                //                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["FreightActual"].ToString()) == 1)
                //                {
                //                    CHKFreightAmt.Checked = true;
                //                }
                //                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["FreightActual"].ToString()) == 0)
                //                {
                //                    CHKFreightAmt.Checked = false;
                //                }

                //                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["OtherChargeActual"].ToString()) == 1)
                //                {
                //                    CHKOtherCharges.Checked = true;
                //                }
                //                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["OtherChargeActual"].ToString()) == 0)
                //                {
                //                    CHKOtherCharges.Checked = false;
                //                }

                //                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["LoadingActual"].ToString()) == 1)
                //                {
                //                    CHKLoading.Checked = true;
                //                }
                //                if (Convert.ToInt32(DsEdit.Tables[3].Rows[0]["LoadingActual"].ToString()) == 0)
                //                {
                //                    CHKLoading.Checked = false;
                //                }


                //                txtexcisedutyper.Text = DsEdit.Tables[3].Rows[0]["ExcisePer"].ToString();
                //                txtexciseduty.Text = DsEdit.Tables[3].Rows[0]["ExciseAmount"].ToString();
                //            }
                //            BtnSave.Visible = false;
                //            BtnUpdate.Visible = true;
                //        }
                //        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                //        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                //    }
                //    break;

                //case ("Delete"):
                //    {
                //        int Deleteid = Convert.ToInt32(e.CommandArgument);
                //        Entity_PurchaseOrder.POId = Deleteid;
                //        Entity_PurchaseOrder.UserId = Convert.ToInt32(Session["UserId"]);
                //        Entity_PurchaseOrder.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                //        int DeleteDetails = Obj_PurchaseOrder.Delete_PurchaseOrder(ref Entity_PurchaseOrder, out StrError);

                //        if (DeleteDetails > 0)
                //        {
                //            obj_Comman.ShowPopUpMsg("Record Deleted Successfully", this.Page);
                //            MakeEmptyForm();
                //        }
                //        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                //        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                //    }
                //    break;
                //case ("Email"):
                //    {
                //        int Mailid = Convert.ToInt32(e.CommandArgument);
                //        int status = 1;// SendMail(Mailid, 1);
                //        if (status == 1)
                //        {
                //            int GETUPDATES1 = Obj_PurchaseOrder.UpdateInsert_PurchseOrderMAIL(Convert.ToInt32(Mailid), out StrError);

                //        }
                //        MakeEmptyForm();
                //        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
                //        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                //    }
                //    break;
                //case ("CANCELAUTHPO"):
                //    {
                //        int CurrRow = Convert.ToInt32(e.CommandArgument);
                //        //int RequisitnId = Convert.ToInt32(Convert.ToInt32(((Label)ReportGrid.Rows[CurrRow].FindControl("LblEntryId")).Text));
                //        BindPurchaseOrderFORCANCELPOPUP(CurrRow);
                //        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
                //        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:ShowPOP();", true);

                //    }
                //    break;
                case ("MailPO"):
                    {
                        TRLOADING.Visible = false;
                        ViewState["MailID"] = Convert.ToInt32(e.CommandArgument);
                        GETDATAFORMAIL(1, 1);
                        MDPopUpYesNoMail2.Show();
                        BtnPopMail2.Focus();
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void GrdReqPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (FlagAdd == false)
            {
                this.ReportGrid.PageIndex = e.NewPageIndex;
                DataSet DS = new DataSet();
                StrCondition = TxtSearch.Text; ;
                Ds = OBJ_EmailPO.GetPurchase_Order(StrCondition, out StrError);
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

            else
            {
                this.ReportGrid.PageIndex = e.NewPageIndex;
                ReportGrid.DataSource = ViewState["PageIndex"] as DataTable;
                this.ReportGrid.DataBind();
                GETHIGHLITE();
            }
            //this.ReportGrid.PageIndex = e.NewPageIndex;
            //DataSet DS = new DataSet();
            //StrCondition = TxtSearch.Text; ;
            //Ds = OBJ_EmailPO.GetPurchase_Order(StrCondition, out StrError);
            //if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            //{
            //    ReportGrid.DataSource = Ds.Tables[0];
            //    this.ReportGrid.DataBind();
            //    //GETHIGHLITE();
            //}
            //else
            //{
            //    //SetInitialRow_ReportGrid();
            //}
            //ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void ReportGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        // 0 bydefault
        // 1 for approve
        // 2 for authorise

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.Cells[12].Text) == "0")
            {
                ((ImageButton)e.Row.FindControl("ImageApproved")).Visible = true;
                ((ImageButton)e.Row.FindControl("ImageAuthorised")).Visible = false;

            }
            if ((e.Row.Cells[12].Text) == "1")
            {
                ((ImageButton)e.Row.FindControl("ImageApproved")).Visible = false;
                ((ImageButton)e.Row.FindControl("ImageAuthorised")).Visible = true;
            }

            /////////////////Start Akshay//////////////            

        }

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //Label GRDPOSTATUS = (Label)e.Row.FindControl("GRDPOSTATUS");

        //    if ((e.Row.Cells[6].Text) == "Generated")
        //    {
        //        e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;

        //    }
        //    if ((e.Row.Cells[6].Text) == "Approved")
        //    {
        //        e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
        //        e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[2].BackColor = System.Drawing.Color.Yellow;
        //        e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
        //        e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[4].BackColor = System.Drawing.Color.Yellow;
        //        e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
        //        e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[6].BackColor = System.Drawing.Color.Yellow;
        //        e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[7].BackColor = System.Drawing.Color.Yellow;
        //        e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[9].BackColor = System.Drawing.Color.Yellow;
        //        e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;


        //    }
        //    if ((e.Row.Cells[6].Text) == "Authorised")
        //    {
        //        e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
        //        e.Row.Cells[1].ForeColor = System.Drawing.Color.White;
        //        e.Row.Cells[2].BackColor = System.Drawing.Color.Green;
        //        e.Row.Cells[2].ForeColor = System.Drawing.Color.White;
        //        e.Row.Cells[3].BackColor = System.Drawing.Color.Green;
        //        e.Row.Cells[3].ForeColor = System.Drawing.Color.White;
        //        e.Row.Cells[4].BackColor = System.Drawing.Color.Green;
        //        e.Row.Cells[4].ForeColor = System.Drawing.Color.White;
        //        e.Row.Cells[5].BackColor = System.Drawing.Color.Green;
        //        e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
        //        e.Row.Cells[6].BackColor = System.Drawing.Color.Green;
        //        e.Row.Cells[6].ForeColor = System.Drawing.Color.White;
        //        e.Row.Cells[7].BackColor = System.Drawing.Color.Green;
        //        e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
        //        e.Row.Cells[9].BackColor = System.Drawing.Color.Green;
        //        e.Row.Cells[9].ForeColor = System.Drawing.Color.White;
        //    }

        //    if (e.Row.Cells[9].Text.ToString() == "Mail Send")
        //    {
        //        e.Row.Cells[9].BackColor = System.Drawing.Color.Brown;
        //        e.Row.Cells[9].ForeColor = System.Drawing.Color.White;
        //    }

        //}
    }

    protected void ImageApproved_Click(object sender, EventArgs e)
    {
        //------Approved Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        ReportGrid.Rows[currrow].Cells[10].Text = "1";
        ReportGrid.Rows[currrow].Cells[11].Text = "1";

        ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageCancel")).Visible = true;
        ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageApproved")).Visible = false;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }

    protected void ImageAuthorised_Click(object sender, EventArgs e)
    {
        //------Authorised Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        ReportGrid.Rows[currrow].Cells[10].Text = "1";
        ReportGrid.Rows[currrow].Cells[11].Text = "2";
        ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageCancel")).Visible = true;
        ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageAuthorised")).Visible = false;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }

    protected void ImageCancel_Click(object sender, EventArgs e)
    {
        //------Cancel Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        ReportGrid.Rows[currrow].Cells[9].Text = "0";

        if (ReportGrid.Rows[currrow].Cells[10].Text == "1")
        {
            ReportGrid.Rows[currrow].Cells[10].Text = "0";
            ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageApproved")).Visible = true;
            ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageCancel")).Visible = false;
        }
        if (ReportGrid.Rows[currrow].Cells[10].Text == "2")
        {
            ReportGrid.Rows[currrow].Cells[10].Text = "1";
            ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageAuthorised")).Visible = true;
            ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageCancel")).Visible = false;
        }

        ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageCancel")).Visible = false;

        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0;
        DMEmailPO Obj_PO = new DMEmailPO();

        for (int j = 0; j < ReportGrid.Rows.Count; j++)
        {

            if (ReportGrid.Rows[j].Cells[10].Text.ToString() == "1")
            {
                if (ReportGrid.Rows[j].Cells[11].Text.ToString() == "1")
                {
                    int reqid = Convert.ToInt32(((Label)ReportGrid.Rows[j].FindControl("LblEstimateId")).Text);
                    //int EmailStatus = Obj_RequisitionCafeteria.UpdateApproveEmailStatus(reqid, out StrError);
                    int EmailStatus = Obj_PO.UpdateApproveEmailStatusNew(reqid, long.Parse(Session["UserID"].ToString()), out StrError);
                }
                if (ReportGrid.Rows[j].Cells[11].Text.ToString() == "2")
                {
                    int reqid = Convert.ToInt32(((Label)ReportGrid.Rows[j].FindControl("LblEstimateId")).Text);
                    //int EmailStatus = Obj_RequisitionCafeteria.UpdateEmailStatus(reqid, out StrError);
                    int EmailStatus = Obj_PO.UpdateEmailStatusNew(reqid, long.Parse(Session["UserID"].ToString()), out StrError);
                }


                #region [CODE]
                //int reqid = Convert.ToInt32(((Label)ReportGrid.Rows[j].FindControl("LblEstimateId")).Text);
                //int EmailStatus = Obj_RequisitionCafeteria.UpdateEmailStatus(reqid, out StrError);


                //int EmailStatus = Obj_RequisitionCafeteria.UpdateEmailStatus(reqid, out StrError);
                #   endregion

            }
        }
        BindReportGrid("");
        GETHIGHLITE();
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

    }

    public void GETHIGHLITE()
    {
        for (int i = 0; i < ReportGrid.Rows.Count; i++)
        {
            if ((ReportGrid.Rows[i].Cells[7].Text) == "Email Not Approved")
            {
                ReportGrid.Rows[i].BackColor = System.Drawing.Color.White;
            }
            else
            {
                ReportGrid.Rows[i].BackColor = System.Drawing.Color.Yellow;
            }
            
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {

        if (rdbSupplier.Checked == false && rdbSite.Checked == false && rdbDate.Checked == false)
        {
            obj_Comman.ShowPopUpMsg("Please check the checkbox.!!", this.Page);
        }

        else
        {
            FlagAdd = true;
            DataTable dt = new DataTable();
            dt = ViewState["GridDS"] as DataTable;
            string colName = "";
            if (rdbSupplier.Checked == true)
            {
                colName = "SuplierName ," + "PODateDirect desc";
            }
            if (rdbSite.Checked == true)
            {
                colName = "Site ," + "PODateDirect desc";
            }
            if (rdbDate.Checked == true)
            {
                colName = "PODateDirect desc";
            }
            dt.DefaultView.Sort = colName;
            dt = dt.DefaultView.ToTable();

            ViewState["PageIndex"] = dt;

            ReportGrid.DataSource = dt;
            ReportGrid.DataBind();
            GETHIGHLITE();

        }
    }
}
