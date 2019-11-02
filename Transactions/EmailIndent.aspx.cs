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

public partial class Transactions_EmailIndent : System.Web.UI.Page
{

    #region [private variables]
    DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
    RequisitionCafeteria Entity_RequisitionCafeteria = new RequisitionCafeteria();

    DMIndentEmail OBJ_IndentEmail = new DMIndentEmail();
    IndentEmail Entity_IndentEmail = new IndentEmail();
    
    CommanFunction obj_Comman = new CommanFunction();
    DataSet Ds = new DataSet();
    public static int rowindexes = 0;
    int valueofitem = 0;
    private static DataTable DSVendor = new DataTable();
    DataSet DsEdit = new DataSet();
    DataTable DtEditPO;
    public static int EditTemp = 1;
    int CategoryID, insertrow; bool fillgrid = false;
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    private static string Lbllocids;
    private static string TOSTRING = string.Empty;
    private static bool FlagDel,FlagAdd ,FlagEdit, FlagPrint = false;
    #endregion

    #region[user right fun]
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

                DataTable dt9 = new DataTable();
                dt9 = dsChkUserRight1.Tables[1];

                DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Email Indent'");
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
                // //Checking View Right ========                    
                //if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                //{
                //    ReportGrid.Visible = false;
                //}
                //Checking Add Right ========                    
                if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                {
                    //BtnSave.Visible = false;
                    FlagAdd = true;

                    //}
                    ////Edit /Delete Column Visible ========
                    //if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false &&
                    //    Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    //{
                    //    foreach (GridViewRow GRow in ReportGrid.Rows)
                    //    {
                    //        GRow.FindControl("ImgBtnDelete").Visible = false;
                    //        GRow.FindControl("ImageGridEdit").Visible = false;
                    //        GRow.FindControl("ImgBtnPrint").Visible = false;
                    //    }
                    //    //BtnUpdate.Visible = false;
                    //    FlagDel = true;
                    //    FlagEdit = true;
                    //    FlagPrint = true;
                    //}
                    //else
                    //{
                    //    //Checking Delete Right ========
                    //    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                    //    {
                    //        foreach (GridViewRow GRow in ReportGrid.Rows)
                    //        {
                    //            GRow.FindControl("ImgBtnDelete").Visible = false;
                    //            FlagDel = true;
                    //        }
                    //    }

                    //    //Checking Edit Right ========
                    //    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    //    {
                    //        foreach (GridViewRow GRow in ReportGrid.Rows)
                    //        {
                    //            GRow.FindControl("ImageGridEdit").Visible = false;
                    //            FlagEdit = true;
                    //        }
                    //    }

                    //    //Checking Print Right ========
                    //    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    //    {
                    //        foreach (GridViewRow GRow in ReportGrid.Rows)
                    //        {
                    //            GRow.FindControl("ImgBtnPrint").Visible = false;
                    //            FlagPrint = true;
                    //        }
                    //    }
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
    #endregion

    protected void ImageApproved_Click(object sender, EventArgs e)
    {
        

        //------Approved Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        ReportGrid.Rows[currrow].Cells[8].Text = "1";
        ReportGrid.Rows[currrow].Cells[9].Text = "1";
        
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
        ReportGrid.Rows[currrow].Cells[8].Text = "1";
        ReportGrid.Rows[currrow].Cells[9].Text = "2";
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
        ReportGrid.Rows[currrow].Cells[8].Text = "0";

        if (ReportGrid.Rows[currrow].Cells[9].Text == "1")
        {
            ReportGrid.Rows[currrow].Cells[9].Text = "0";
            ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageApproved")).Visible = true;
            ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageCancel")).Visible = false;
        }
        if (ReportGrid.Rows[currrow].Cells[9].Text == "2")
        {
            ReportGrid.Rows[currrow].Cells[9].Text = "1";
            ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageAuthorised")).Visible = true;
            ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageCancel")).Visible = false;
        }

        ((ImageButton)ReportGrid.Rows[currrow].FindControl("ImageCancel")).Visible = false;

        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }

    private void MakeEmptyForm()
    {
        //BindReportGrid(StrCondition);
        BindCombo();
        GETHIGHLITE();
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        //show1.Visible = false;
        
    }

    public void BindCombo()
    {
        try
        {
            Ds = new DataSet();
            Ds = OBJ_IndentEmail.FillCombo(Convert.ToInt32(Session["TransactionSiteID"].ToString()), out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ReportGrid.DataSource = Ds.Tables[0];
                    ReportGrid.DataBind();

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
                Flag = true;
                //int EmailStatus = Obj_RequisitionCafeteria.UpdateEmailStatus(reqid, out StrError);
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

    public void BindReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            string RepCondition1 = string.Empty;
            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
            //    RepCondition1 = RepCondition1 + " AND D.CafeteriaId=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}

            DsReport = OBJ_IndentEmail.GetAllRequisition(RepCondition, RepCondition1, out StrError);
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

            //if (DsReport.Tables.Count > 0 && DsReport.Tables[1].Rows.Count > 0)
            //{
            //    LblGen.Text = DsReport.Tables[1].Rows[0][0].ToString();
            //    LblApprov.Text = DsReport.Tables[1].Rows[0][1].ToString();
            //    LblAutho.Text = DsReport.Tables[1].Rows[0][2].ToString();
            //}
            //else
            //{
            //    LblAutho.Text = LblApprov.Text = LblGen.Text = "0";
            //}
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void GETHIGHLITE()
    {
        for (int i = 0; i < ReportGrid.Rows.Count; i++)
        {
            if ((ReportGrid.Rows[i].Cells[5].Text) == "Email Not Approved")
            {
                ReportGrid.Rows[i].BackColor = System.Drawing.Color.White;
            }
            else
            {
                
                ReportGrid.Rows[i].BackColor = System.Drawing.Color.Yellow;
                if (ReportGrid.Rows[i].Cells[7].Text == "Email Sent")
                {
                    ReportGrid.Rows[i].BackColor = System.Drawing.Color.Green;
                }
            }
            //if ((ReportGrid.Rows[i].Cells[7].Text) == "Authorised")
            //{
            //    ReportGrid.Rows[i].BackColor = System.Drawing.Color.Green;
            //}
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckUserRight();
            MakeEmptyForm();
        }
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            StrCondition = string.Empty;
            StrCondition = TxtSearch.Text.Trim();
            Ds = new DataSet();
            Ds = OBJ_IndentEmail.GetRequisition(StrCondition, " and P.CafeteriaId=" + Convert.ToInt32(Session["CafeteriaId"].ToString()), out StrError,Convert.ToInt32(Session["CafeteriaId"].ToString()));
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

    protected void ReportGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                //case ("Select"):
                //    {
                //        ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                //        Ds = Obj_RequisitionCafeteria.GetRequisitionDetailsForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                //        if (Ds.Tables.Count > 0)
                //        {
                //            if (Ds.Tables[0].Rows.Count > 0)
                //            {
                //                txtReqNo.Text = Ds.Tables[0].Rows[0]["RequisitionNo"].ToString();
                //                lblReqNo.Text = Ds.Tables[0].Rows[0]["RequisitionNo"].ToString();
                //                txtReqDate.Text = Ds.Tables[0].Rows[0]["RequisitionDate"].ToString();
                //                lblCafe.Text = Ds.Tables[0].Rows[0]["Cafeteria"].ToString();
                //                txtTempDate.Text = Ds.Tables[0].Rows[0]["ExpdDate"].ToString();
                //                ddlCostCentre.SelectedValue = Ds.Tables[0].Rows[0]["IsCostCentre"].ToString();
                //                TXTREMARK.Text = Ds.Tables[0].Rows[0]["Remark"].ToString();
                //                txtindremark.Text = Ds.Tables[0].Rows[0]["RemarkIND"].ToString();
                //            }
                //            if (Ds.Tables[1].Rows.Count > 0)
                //            {
                //                ViewState["Template"] = Ds.Tables[1];
                //                ViewState["Requisition"] = Ds.Tables[1];
                //                ViewState["TemplateDetails"] = Ds.Tables[1];
                //                GrdRequisition.DataSource = Ds.Tables[1];
                //                GrdRequisition.DataBind();
                //                GetItemForEdit();
                //                CalculateToTotal();
                //                for (int t = 0; t < GrdRequisition.Rows.Count; t++)
                //                {
                //                    ((CheckBox)GrdRequisition.Rows[t].Cells[0].FindControl("GrdSelectAll")).Checked = true;

                //                }
                //                EditTemp = 0;
                //                BtnUpdate.Visible = true;
                //                BtnSave.Visible = false;
                //            }
                //            else
                //            {
                //                SetInitialRow_GrdRequisition();
                //            }
                //        }
                //    }
                //    break;
                //case ("Delete"):
                //    {
                //        ViewState["DeleteID"] = Convert.ToInt32(e.CommandArgument);
                //        Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(e.CommandArgument);
                //        Entity_RequisitionCafeteria.UserId = Convert.ToInt32(Session["UserId"]);
                //        Entity_RequisitionCafeteria.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                //        int DeletedRow = Obj_RequisitionCafeteria.DeleteRequisition(ref Entity_RequisitionCafeteria, out StrError);
                //        if (DeletedRow != 0)
                //        {
                //            obj_Comman.ShowPopUpMsg("Record Deleted Successfully!", this.Page);
                //            MakeEmptyForm();
                //        }
                //    }
                //    break;
                //case ("DeleteMR"):
                //    {
                //        ViewState["DeleteID"] = Convert.ToInt32(e.CommandArgument);
                //        Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(e.CommandArgument);
                //        Entity_RequisitionCafeteria.UserId = Convert.ToInt32(Session["UserId"]);
                //        Entity_RequisitionCafeteria.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                //        int DeletedRow = Obj_RequisitionCafeteria.DeleteRequisition(ref Entity_RequisitionCafeteria, out StrError);
                //        if (DeletedRow != 0)
                //        {
                //            obj_Comman.ShowPopUpMsg("Record Deleted Successfully!", this.Page);
                //            MakeEmptyForm();
                //        }
                //    }
                //    break;
                case ("MailIndent"):
                    {
                        TRLOADING.Visible = false;
                        ViewState["MailID"] = Convert.ToInt32(e.CommandArgument);
                        GETDATAFORMAIL(1, 1);
                        MDPopUpYesNoMail.Show();
                        BtnPopMail1.Focus();
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
            Ds = OBJ_IndentEmail.FillCombo(Convert.ToInt32(Session["TransactionSiteID"].ToString()), out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ReportGrid.DataSource = Ds.Tables[0];
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
        // 0 bydefault
        // 1 for approve
        // 2 for authorise

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            if ((e.Row.Cells[9].Text) == "0")
            {
                ((ImageButton)e.Row.FindControl("ImageApproved")).Visible = true;
                ((ImageButton)e.Row.FindControl("ImageAuthorised")).Visible = false;
                
            }
            if ((e.Row.Cells[9].Text) == "1")
            {
                ((ImageButton)e.Row.FindControl("ImageApproved")).Visible = false;
                ((ImageButton)e.Row.FindControl("ImageAuthorised")).Visible = true;
            }

            if ((e.Row.Cells[5].Text) != "Email Not Approved")
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
            }

            if ((e.Row.Cells[7].Text) == "Email Sent")
            {
                e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[2].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[3].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[4].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[5].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[6].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[7].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;

                ((ImageButton)e.Row.FindControl("ImageAuthorised")).Visible = false;
                ((ImageButton)e.Row.FindControl("ImageApproved")).Visible = false;
            }
            /////////////////Start Akshay//////////////            

        }


        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);




        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label GRDPOSTATUS = (Label)e.Row.FindControl("GRDPOSTATUS");

        //    if (GRDPOSTATUS.Text == "PO GENERATED")
        //    {
        //        e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[1].BackColor = System.Drawing.Color.SkyBlue;
        //        e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[2].BackColor = System.Drawing.Color.SkyBlue;
        //        e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[3].BackColor = System.Drawing.Color.SkyBlue;
        //        e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[4].BackColor = System.Drawing.Color.SkyBlue;
        //        e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[5].BackColor = System.Drawing.Color.SkyBlue;
        //        e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[6].BackColor = System.Drawing.Color.SkyBlue;
        //        e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[7].BackColor = System.Drawing.Color.SkyBlue;//LightSteelBlue;
        //        e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;
        //        e.Row.Cells[8].BackColor = System.Drawing.Color.SkyBlue;
        //    }
        //    else
        //    {
        //        if ((e.Row.Cells[6].Text) == "Generated")
        //        {
        //            e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
        //        }
        //        if ((e.Row.Cells[6].Text) == "Approved")
        //        {
        //            e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
        //            e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[2].BackColor = System.Drawing.Color.Yellow;
        //            e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
        //            e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[4].BackColor = System.Drawing.Color.Yellow;
        //            e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
        //            e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[6].BackColor = System.Drawing.Color.Yellow;
        //            e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[7].BackColor = System.Drawing.Color.Yellow;
        //            e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
        //            e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;
        //        }
        //        if ((e.Row.Cells[6].Text) == "Authorised")
        //        {
        //            e.Row.Cells[1].BackColor = System.Drawing.Color.MediumSeaGreen;
        //            e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[2].BackColor = System.Drawing.Color.MediumSeaGreen;
        //            e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[3].BackColor = System.Drawing.Color.MediumSeaGreen;
        //            e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[4].BackColor = System.Drawing.Color.MediumSeaGreen;
        //            e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[5].BackColor = System.Drawing.Color.MediumSeaGreen;
        //            e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[6].BackColor = System.Drawing.Color.MediumSeaGreen;
        //            e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[7].BackColor = System.Drawing.Color.MediumSeaGreen;
        //            e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
        //            e.Row.Cells[8].BackColor = System.Drawing.Color.MediumSeaGreen;
        //            e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;
        //        }
        //    }

        //    if ((e.Row.Cells[8].Text) == "Email Sent")
        //    {
        //        e.Row.Cells[8].BackColor = System.Drawing.Color.IndianRed;
        //        //e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
        //    }

        //    // GridViewRow gvr=(GridViewRow)e.Row.NamingContainer;

        //    if ((GRDPOSTATUS.Text == "PO GENERATED"))
        //    {
        //        if (decimal.Parse(ReportGrid.DataKeys[e.Row.RowIndex].Values["POTotQty"].ToString()) < decimal.Parse(ReportGrid.DataKeys[e.Row.RowIndex].Values["IndentTotQty"].ToString()))
        //        {
        //            e.Row.BackColor = System.Drawing.Color.Pink;
        //        }
        //    }
        //}
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
                BtnPopMail1.Focus();
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
            BtnPopMail1.Focus();
        }
        catch (Exception ex)
        {
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0;
        for (int j = 0; j < ReportGrid.Rows.Count; j++)
        {

            if (ReportGrid.Rows[j].Cells[8].Text.ToString() == "1")
            {
                if (ReportGrid.Rows[j].Cells[9].Text.ToString() == "1")
                {
                    int reqid = Convert.ToInt32(((Label)ReportGrid.Rows[j].FindControl("LblEstimateId")).Text);
                    
                  
                    
                    //int EmailStatus = Obj_RequisitionCafeteria.UpdateApproveEmailStatus(reqid, out StrError);
                    int EmailStatus = Obj_RequisitionCafeteria.UpdateApproveEmailStatusNew(reqid, long.Parse(Session["UserID"].ToString()), out StrError);
                }
                if (ReportGrid.Rows[j].Cells[9].Text.ToString() == "2")
                {
                    int reqid = Convert.ToInt32(((Label)ReportGrid.Rows[j].FindControl("LblEstimateId")).Text);
                    //int EmailStatus = Obj_RequisitionCafeteria.UpdateEmailStatus(reqid, out StrError);
                    int EmailStatus = Obj_RequisitionCafeteria.UpdateEmailStatusNew(reqid, long.Parse(Session["UserID"].ToString()), out StrError);
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

    //protected void ImageAuthorised_Click(object sender, EventArgs e)
    //{
    //    //------Authorised Image------
    //    ImageButton Imagebtn = (ImageButton)sender;
    //    GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
    //    int currrow = grd.RowIndex;
    //    //GrdReq.Rows[currrow].Cells[7].Text = "Authorised";
    //    //GrdReq.Rows[currrow].Cells[8].Text = "1";
    //    //((ImageButton)GrdReq.Rows[currrow].FindControl("ImageCancel")).Visible = true;
    //    //((ImageButton)GrdReq.Rows[currrow].FindControl("ImageAuthorised")).Visible = false;
    //    ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    //}


    /*
    protected void ReportGrid_DataBound(object sender, EventArgs e)
    {
        try
        {
            for (int rowIndex = ReportGrid.Rows.Count - 1; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = ReportGrid.Rows[rowIndex];
                ImageButton ImageAccepted = (ImageButton)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImageAccepted");
                ImageButton ImageGridEdit = (ImageButton)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImageGridEdit");
                bool OrderStatus = Convert.ToBoolean(ReportGrid.Rows[rowIndex].Cells[5].Text);

                ImageButton ImageApprove = (ImageButton)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImageApprove");
                ImageButton IMGDELETEMR = (ImageButton)ReportGrid.Rows[rowIndex].Cells[0].FindControl("IMGDELETEMR");
                ImageButton ImgBtnDelete = (ImageButton)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImgBtnDelete");
                Image ImagePrint = (Image)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImgBtnPrint");
                ImageButton ImageGridEditBlocked = (ImageButton)ReportGrid.Rows[rowIndex].Cells[0].FindControl("ImageGridEditBlocked");
                Label GRDPOSTATUS = (Label)ReportGrid.Rows[rowIndex].Cells[0].FindControl("GRDPOSTATUS");

                //if (GRDPOSTATUS.Text == "NO PO GENERATED")
                //{
                //    IMGDELETEMR.Visible = true;
                //}
                //else
                //{
                //    IMGDELETEMR.Visible = false;
                //}

                //if (OrderStatus != false)
                //{
                //    ImageGridEditBlocked.Visible = true;
                //}
                //else
                //{
                //    ImageGridEditBlocked.Visible = false;
                //}

                string ReqStatus = Convert.ToString(ReportGrid.Rows[rowIndex].Cells[6].Text);
                if (ReqStatus != "Generated")
                {
                    //ImageAccepted.Visible = true;
                    //ImageApprove.Visible = true;
                    //ImageGridEdit.Visible = false;
                    ImagePrint.Visible = true;
                    //ImgBtnDelete.Visible = false;
                    //ImgEmail.Visible = true;
                }
                else
                {
                    //ImageAccepted.Visible = false;
                    //ImageApprove.Visible = false;
                    //ImageGridEdit.Visible = true;
                    ImagePrint.Visible = false;
                    //ImgBtnDelete.Visible = true;
                    //ImgEmail.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
      */
}
