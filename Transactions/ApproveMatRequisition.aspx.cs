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
using System.IO;
using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net;

public partial class Transactions_ApproveMatRequisition : System.Web.UI.Page
{
    #region [private variables]
        DMApproveRequisition Obj_ApproveRequisition = new DMApproveRequisition();
        ApproveRequisition Entity_ApproveRequisition = new ApproveRequisition();
        CommanFunction obj_Comman = new CommanFunction();
        ReportDocument CRpt = new ReportDocument();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        DataSet Ds = new DataSet();
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
    #endregion

    #region[UserDefine Function]
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

            DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Authorized Requisition'");
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
                BtnSave.Visible = false;
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

    public void SendMail(int reqid)
    {
        try
        {
            DataSet dslogin = new DataSet();
            Ds = new DataSet();
            DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
            Ds = Obj_RequisitionCafeteria.GETDATAFORMAIL(reqid, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                //-------------------------------------------------[Mail Code]-------------------------------------------------
                string smtpServer = "smtp.gmail.com";
                string userName = "laxman.washivale@sapragroup.com";
                string password = "shravani";


                //string smtpServer = "smtp.mail.yahoo.com";//localhost
                //string userName = "revosolutionpune@yahoo.com";//sapragrouppune@gmail.com
                //string password = "revosacred123";//saprapune@12345

                int cdoBasic = 1;
                int cdoSendUsingPort = 2;
                MailMessage msg = new MailMessage();
                if (userName.Length > 0)
                {
                    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
                    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 465);
                    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
                    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
                    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", userName);
                    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);
                    msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", true);
                }

                msg.To = "laxman.washivale@sapragroup.com";
              //  msg.To="revosolutionpune@yahoo.com";
                //if (txtCC.Text != "")
                //{
                //    msg.Cc = txtCC.Text;
                //}
                //if (txtBcc.Text != "")
                //{
                //    msg.Bcc = txtBcc.Text;
                //}

                msg.From = "laxman.washivale@sapragroup.com";
               // msg.From = "revosolutionpune@yahoo.com";
                msg.Subject = "Material Request From " + Ds.Tables[0].Rows[0]["Employee"].ToString().ToUpper() + " (" + Ds.Tables[0].Rows[0]["Site"].ToString() + " )-( REQ. NO. : " + Ds.Tables[0].Rows[0]["RequisitionNo"].ToString()+" ) Has Been Approved";
                string mystring = "Hello Sir,<br/> Request No. : " + Ds.Tables[0].Rows[0]["RequisitionNo"].ToString() + "<br/>"+"Request Date :" + Ds.Tables[0].Rows[0]["ReqDate"].ToString() + "<br/> Please Check This Request For PO";
                mystring = mystring.Replace("<br/>", System.Environment.NewLine);
                msg.Body = mystring.ToString();

                SmtpMail.SmtpServer = smtpServer;
                SmtpMail.Send(msg);
            }
            //  -------------------------------------------------[End Mail Code]-------------------------------------------------
        }
        catch (Exception)
        {
            
        }
    }

    public void CALLPDF()
    {
        DMRequisitionCafeteria obj_RequisitionCafeteria = new DMRequisitionCafeteria();
        DataSet dslogin = new DataSet();
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
            CRpt.Load(Server.MapPath("~/CrystalPrint/REQUEST.rpt"));
            CRpt.SetDataSource(dslogin);
            string PDFMaster = Server.MapPath(@"~/TempFiles/" + "PREREQUISITION - " + (DateTime.Now).ToString("dd-MMM-yyyy") + ".pdf");
            CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(PDFMaster);
            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }
    }

    private void MakeEmptyForm()
    {
        BindReportGrid(StrCondition);
        GETHIGHLITE();
        SetInitialRow_GRDPOPUPFOREDIT();
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        //show1.Visible = false;
        if(!FlagAdd)
        BtnSave.Visible = true;
        BtnAuthorized.Visible = false;
        TR_PODtls.Visible = false;
        TxtSearch.Text = string.Empty;
    }

    private void SetInitialRow_ReqDetails()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("RequisitionNo", typeof(string));
            dt.Columns.Add("RemarkIND", typeof(string));
            dt.Columns.Add("RequisitionDate", typeof(string));
            dt.Columns.Add("ReqStatus", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dr = dt.NewRow();

            dr["#"] = 0;
            dr["RequisitionNo"] = "";
            dr["RemarkIND"] = "";
            dr["RequisitionDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
            dr["ReqStatus"] = "";
            dr["Location"] = "";
          
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
            GrdReq.DataSource = dt;
            GrdReq.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void SetInitialRow_GrdReqDtls()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("ItemId", typeof(int));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("Qty", typeof(Decimal));
            dt.Columns.Add("Rate", typeof(Decimal));
            dt.Columns.Add("SuplierName", typeof(string));
            dt.Columns.Add("Priority", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("RemarkFull", typeof(string)); 
            dr = dt.NewRow();

            dr["ItemId"] = 0;
            dr["ItemCode"] = "";
            dr["ItemName"] = "";
            dr["Qty"] = 0;
            dr["Rate"] = 0;
            dr["SuplierName"] = "";
            dr["Priority"] = "";
            dr["Remark"] = "";
            dr["RemarkFull"] = "";
            dt.Rows.Add(dr);

            ViewState["CurrentTablePO"] = dt;
            GrdReqDtls.DataSource = dt;
            GrdReqDtls.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void SetInitialRow_GRDPOPUPFOREDIT()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("RequestId", typeof(int));
            dt.Columns.Add("RequestNo", typeof(string));
            dt.Columns.Add("RequestDate", typeof(string));
            dt.Columns.Add("ItemId", typeof(int));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("Qty", typeof(Decimal));
            dt.Columns.Add("Rate", typeof(Decimal));
            dt.Columns.Add("Emp", typeof(string));
            dt.Columns.Add("Priority", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("RemarkFull", typeof(string));
            dt.Columns.Add("RemarkAuth", typeof(string));
            dr = dt.NewRow();

            dr["RequestId"]=0;
            dr["RequestNo"]="";
            dr["RequestDate"] = "";
            dr["ItemId"] = 0;
            dr["ItemCode"] = "";
            dr["ItemName"] = "";
            dr["Qty"] = 0;
            dr["Rate"] = 0;
            dr["Emp"] = "";
            dr["Priority"] = "";
            dr["Remark"] = "";
            dr["RemarkFull"] = "";
            dr["Location"] = "";
            dr["RemarkAuth"] = "";
            dt.Rows.Add(dr);

            ViewState["CurrentTablePO"] = dt;
            GRDPOPUPFOREDIT.DataSource = dt;
            GRDPOPUPFOREDIT.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
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

            DsReport = Obj_ApproveRequisition.GetAllRequisition(RepCondition,RepCondition1, out StrError);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                GrdReq.DataSource = DsReport.Tables[0];
                GrdReq.DataBind();
            }
            else
            {
                GrdReq.DataSource = null;
                GrdReq.DataBind();
                SetInitialRow_ReqDetails();
            }


            if (DsReport.Tables.Count > 0 && DsReport.Tables[1].Rows.Count > 0)
            {
                LblGen.Text = DsReport.Tables[1].Rows[0][0].ToString();
                LblApprov.Text = DsReport.Tables[1].Rows[0][1].ToString();
                LblAutho.Text = DsReport.Tables[1].Rows[0][2].ToString();
            }
            else
            {
                LblAutho.Text = LblApprov.Text = LblGen.Text = "0";
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void GETHIGHLITE()
    {
        for (int i = 0; i < GrdReq.Rows.Count; i++)
        {
            if ((GrdReq.Rows[i].Cells[8].Text) == "Generated")
            {
               GrdReq.Rows[i].BackColor = System.Drawing.Color.White;
            }
            if ((GrdReq.Rows[i].Cells[8].Text) == "Approved")
            {
               GrdReq.Rows[i].BackColor = System.Drawing.Color.Yellow;
            }
            if ((GrdReq.Rows[i].Cells[8].Text) == "Authorised")
            {
              GrdReq.Rows[i].BackColor = System.Drawing.Color.Green;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
    }

    public void BindPurchaseOrderDetails(Int32 RequisitionCafeId)
    {
        try
        {
            Ds = Obj_ApproveRequisition.GetRequisitionDtls(RequisitionCafeId, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                GrdReqDtls.DataSource = Ds.Tables[0];
                GrdReqDtls.DataBind();

                TR_PODtls.Visible = true;
            }
            else
            {
                GrdReqDtls.DataSource = null;
                GrdReqDtls.DataBind();

                TR_PODtls.Visible = false;
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void BindPurchaseOrderDetailsFORPOPUP(Int32 RequisitionCafeId)
    {
        try
        {
            Ds = Obj_ApproveRequisition.GetRequisitionDtls(RequisitionCafeId, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                GRDPOPUPFOREDIT.DataSource = Ds.Tables[0];
                GRDPOPUPFOREDIT.DataBind();
                TxtRemarkAl.Text=Ds.Tables[0].Rows[0]["RemarkFromAuthorise"].ToString();
  
            }
            else
            {
                GRDPOPUPFOREDIT.DataSource = null;
                GRDPOPUPFOREDIT.DataBind();
                TxtRemarkAl.Text = string.Empty;
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  CheckUserRight();
            MakeEmptyForm();
        }
        
    }
   
    protected void ImageApproved_Click(object sender, EventArgs e)
    {
        //------Approved Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        GrdReq.Rows[currrow].Cells[8].Text = "Approved";
        ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageCancel")).Visible = true;
        ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageApproved")).Visible = false;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }

    protected void ImageCancel_Click(object sender, EventArgs e)
    {
        //------Cancel Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        if (GrdReq.Rows[currrow].Cells[8].Text == "Approved")
        {
            GrdReq.Rows[currrow].Cells[8].Text = "Generated";
            ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageApproved")).Visible = true;
            ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageCancel")).Visible = false;
        }
        if (GrdReq.Rows[currrow].Cells[8].Text == "Authorised")
        {
            GrdReq.Rows[currrow].Cells[8].Text = "Approved";
            ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageAuthorised")).Visible = true;
            ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageCancel")).Visible = false;
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }

    protected void ImageAuthorised_Click(object sender, EventArgs e)
    {
        //------Authorised Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        GrdReq.Rows[currrow].Cells[8].Text = "Authorised";
        ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageCancel")).Visible = true;
        ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageAuthorised")).Visible = false;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }

    protected void GrdReq_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int j = 0; j < GrdReq.Rows.Count; j++)
        {
            if ((GrdReq.Rows[j].Cells[8].Text) == "Generated")
            {
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageApproved")).Visible = true;
            }
            if ((GrdReq.Rows[j].Cells[8].Text) == "Approved")
            {
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageApproved")).Visible = false;
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageAuthorised")).Visible = true;
            }
            if ((GrdReq.Rows[j].Cells[8].Text) == "Authorised")
            {
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageAuthorised")).Visible = false;
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageApproved")).Visible = false;
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageSuccess")).Visible = true;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }

    protected void hyl_Hide_Click(object sender, EventArgs e)
    {
        try
        {
            TR_PODtls.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


        }
        catch (Exception ex)
        {
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {

        int UpdateRow = 0;
        for (int j = 0; j < GrdReq.Rows.Count; j++)
        {
            if ((GrdReq.Rows[j].Cells[8].Text) == "Approved")
            {
                string STR = ((Label)GrdReq.Rows[j].FindControl("LblEntryId")).Text;
                Entity_ApproveRequisition.RequisitionCafeId = Convert.ToInt32(((Label)GrdReq.Rows[j].FindControl("LblEntryId")).Text);
                Entity_ApproveRequisition.ReqStatus = GrdReq.Rows[j].Cells[8].Text.ToString();
                Entity_ApproveRequisition.ApprovedTime = DateTime.Now;
                Entity_ApproveRequisition.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_ApproveRequisition.LoginDate = DateTime.Now;
                UpdateRow = Obj_ApproveRequisition.UpdateReq_Status_Approve(ref Entity_ApproveRequisition, out StrError);
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageCancel")).Visible = false;
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageAuthorised")).Visible = true;
            }
            if ((GrdReq.Rows[j].Cells[8].Text) == "Authorised")
            {
                Entity_ApproveRequisition.RequisitionCafeId = Convert.ToInt32(((Label)GrdReq.Rows[j].FindControl("LblEntryId")).Text);
                Entity_ApproveRequisition.ReqStatus = GrdReq.Rows[j].Cells[8].Text.ToString();
                Entity_ApproveRequisition.AuthorizedTime = DateTime.Now;
                Entity_ApproveRequisition.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_ApproveRequisition.LoginDate = DateTime.Now;
                UpdateRow = Obj_ApproveRequisition.UpdateReq_Status_Authorozed(ref Entity_ApproveRequisition, out StrError);
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageSuccess")).Visible = true;
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageAuthorised")).Visible = false;
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageCancel")).Visible = false;
                //SendMail(Convert.ToInt32(((Label)GrdReq.Rows[j].FindControl("LblEntryId")).Text));
            }
        }
        BindReportGrid("");
        GETHIGHLITE();
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }

    protected void GrdReq_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        int CurrRow = Convert.ToInt32(e.CommandArgument);
                        int RequisitnId = Convert.ToInt32(Convert.ToInt32(((Label)GrdReq.Rows[CurrRow].FindControl("LblEntryId")).Text));
                        BindPurchaseOrderDetails(RequisitnId);
                    }
                    break;

                case ("Delete"):
                    {
                    }
                    break;
                case ("SHOWPOPUP"):
                    {
                        int CurrRow = Convert.ToInt32(e.CommandArgument);
                        int RequisitnId = Convert.ToInt32(Convert.ToInt32(((Label)GrdReq.Rows[CurrRow].FindControl("LblEntryId")).Text));
                        BindPurchaseOrderDetailsFORPOPUP(RequisitnId);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:ShowPOP();", true);

                        
                    }
                    break;
                
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        BindReportGrid(StrCondition);
        GETHIGHLITE();
        TR_PODtls.Visible = false;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMApproveRequisition Obj_ApprvReq= new DMApproveRequisition();
        String[] SearchList = Obj_ApprvReq.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, iUpdate = 0;
        try
        {
            if (GRDPOPUPFOREDIT.Rows.Count > 0)
            {
                if (Convert.ToInt32(GRDPOPUPFOREDIT.Rows[0].Cells[0].Text) > 0)
                {
                    for (int row = 0; GRDPOPUPFOREDIT.Rows.Count > row; row++)
                    {
                        iUpdate = Obj_ApproveRequisition.UpdateRecord(Convert.ToInt32(GRDPOPUPFOREDIT.Rows[row].Cells[0].Text), 
                            Convert.ToInt32(GRDPOPUPFOREDIT.Rows[row].Cells[3].Text), Convert.ToInt32(Session["UserId"]), 
                            Convert.ToString(((TextBox)(GRDPOPUPFOREDIT.Rows[row].FindControl("TXTXREMARKFORAUTH"))).Text),
                            TxtRemarkAl.Text,out StrError);
                    }
                    if (iUpdate > 0)
                    {
                        obj_Comman.ShowPopUpMsg("Record Updated Successfully!", this.Page);
                        MakeEmptyForm();
                    }
                }
            }

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
        
    }

    protected void ImgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
    }
    
}