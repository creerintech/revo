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
using System.IO;
using System.Threading;

using System.Web.Mail;
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;

public partial class Utility_SendeMail : System.Web.UI.Page
{
    #region[Variables]
        public static string[] attach;
        CommanFunction obj_Comman = new CommanFunction();
        public static string str1 = "";
        public static string str2 = "";
        public static string str3 = "";
        public static string path1 = "";
        public static string path2 = "";
        public static string path3 = "";
        public static bool FlagAdd = false;
    #endregion

    #region[UserDefinefunction]
    public void MakeFormEmpty()
    {
        TxtTo.Text = string.Empty;
        TxtCC.Text = string.Empty;
        TxtBCC.Text = string.Empty;
        TxtSubject.Text = string.Empty;
        TxtBody.Text = string.Empty;
        ChkAttach1.Visible = false;
        ChkAttach2.Visible = false;
        ChkAttach3.Visible = false;
        FileUploader1.Visible = true;
        FileUpload2.Visible = true;
        FileUpload3.Visible = true;
        lnkAttachedFile.Visible = true;
        str1 = str2 = str3 = "";
        path1 = path2 = path3 = "";
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
                //{
                    //Checking Right of users=======

                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Send E-mail'");
                    if (dtRow.Length > 0)
                    {
                        DataTable dt = dtRow.CopyToDataTable();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        Response.Redirect("~/NotAuthUser.aspx");
                    }
                  
                    //Checking Add Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                    {
                        BtnSend.Visible = false;
                        FlagAdd = true;
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
        catch (ThreadAbortException)
        {
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //User Right Function===========
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {  
            MakeFormEmpty();
            CheckUserRight();
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeFormEmpty();
    }
    protected void BtnSend_Click(object sender, EventArgs e)
    {
        try
        {
            string smtpServer = "smtp.gmail.com";
            string userName = "laxman.washivale@sapragroup.com";
            string password = "shravani";

            int cdoBasic = 1;
            int cdoSendUsingPort = 2;
            MailMessage msg = new MailMessage();
            if (userName.Length > 0)
            {
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", userName);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);
            }

            msg.To = TxtTo.Text;


            if (TxtCC.Text != "")
            {
                msg.Cc = TxtCC.Text;
            }
            if (TxtBCC.Text != "")
            {
                msg.Bcc = TxtBCC.Text;
            }

            msg.From = "laxman.washivale@sapragroup.com";
            msg.Subject = Convert.ToString(TxtSubject.Text);
            string mystring = TxtBody.Text;
            mystring = mystring.Replace("<br/>", System.Environment.NewLine);
            msg.Body = mystring.ToString();

            if (str1 != "")
            {
                msg.Attachments.Add(new System.Web.Mail.MailAttachment(path1));

            }

            if (str2 != "")
            {

                msg.Attachments.Add(new System.Web.Mail.MailAttachment(path2));

            }

            if (str3 != "")
            {
                msg.Attachments.Add(new System.Web.Mail.MailAttachment(path3));
            }

            //for (int i = 0; i < chkAttachList.Items.Count; i++)
            //{
            //    if (chkAttachList.Items[i].Selected == true)
            //    {
            //        if (attach[i] != "")
            //        {
            //            string strmail = attach[i];
            //            msg.Attachments.Add(new System.Web.Mail.MailAttachment(attach[i]));
            //        }
            //    }
            //}

            SmtpMail.SmtpServer = smtpServer;
            SmtpMail.Send(msg);

            obj_Comman.ShowPopUpMsg("Your Message has been Send Sussesfully!", this.Page);

            #region [Code For Deleting File from Temp Attch After deleting]----------------------------------------------

            string[] filePaths = System.IO.Directory.GetFiles(Server.MapPath("~/TempAttachment/"));
            foreach (string filePath in filePaths)
            {
                string delName = filePath.ToString();
                int Last = delName.Length;
                int First = delName.Length - 9;
                delName = delName.Substring(First);
                if (delName == "Thumbs.db")
                {

                }
                else
                {
                    System.IO.File.Delete(filePath);
                }
            }

            #endregion [end code here for deleting file]-----------------------------------------------------------------

            MakeFormEmpty();
            attach = null;

         
            
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void lnkAttachedFile_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUploader1.HasFile)
            {
                str1 = System.IO.Path.GetFileName(FileUploader1.PostedFile.FileName);
                FileUploader1.SaveAs(Server.MapPath("~/TempAttachment/") + str1);

                path1 = Server.MapPath("~/TempAttachment/") + str1;
                ChkAttach1.Visible = true;
                ChkAttach1.Checked = true;
                ChkAttach1.Text = str1;
                FileUploader1.Visible = false;
            }
            if (FileUpload2.HasFile)
            {
                str2 = System.IO.Path.GetFileName(FileUpload2.PostedFile.FileName);
                FileUpload2.SaveAs(Server.MapPath("~/TempAttachment/") + str2);

                path2 = Server.MapPath("~/TempAttachment/") + str2;
                ChkAttach2.Visible = true;
                ChkAttach2.Checked = true;
                ChkAttach2.Text = str2;
                FileUpload2.Visible = false;
            }
            if (FileUpload3.HasFile)
            {
                str3 = System.IO.Path.GetFileName(FileUpload3.PostedFile.FileName);
                FileUpload3.SaveAs(Server.MapPath("~/TempAttachment/") + str3);

                path3 = Server.MapPath("~/TempAttachment/") + str3;
                ChkAttach3.Visible = true;
                ChkAttach3.Checked = true;
                ChkAttach3.Text = str3;
                FileUpload3.Visible = false;

            }
            if (path1 != "" && path2 != "" && path3 != "")
            {
                lnkAttachedFile.Visible = false;
            }
        }

        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg("Files Not Attched, Please try Again..", this.Page);

        }
    }
    protected void ChkAttach1_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkAttach1.Checked == false)
        {

            str1 = "";
        }
    }
    protected void ChkAttach2_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkAttach2.Checked == false)
        {

            str2 = "";
        }
    }
    protected void ChkAttach3_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkAttach3.Checked == false)
        {

            str3 = "";
        }
    }
}
