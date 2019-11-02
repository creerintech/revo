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
using System.IO;
using System.Web.Mail;
public partial class ConfirmPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetConfirm();
        }
    }

    public void SendMailREQ(int reqid)
    {
        try
        {
            DataSet dslogin = new DataSet();
            DataSet Ds = new DataSet();
            string StrError = string.Empty;
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
                msg.Subject = "Material Request From " + Ds.Tables[0].Rows[0]["Employee"].ToString().ToUpper() + " (" + Ds.Tables[0].Rows[0]["Site"].ToString() + " )-( REQ. NO. : " + Ds.Tables[0].Rows[0]["RequisitionNo"].ToString() + " ) Has Been Approved";
                string mystring = "Hello Sir,<br/> Request No. : " + Ds.Tables[0].Rows[0]["RequisitionNo"].ToString() + "<br/>" + "Request Date :" + Ds.Tables[0].Rows[0]["ReqDate"].ToString() + "<br/> Please Check This Request For PO";
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

    public void SendMail(int reqid)
    {
        try
        {
            DataSet dslogin = new DataSet();
            DataSet Ds = new DataSet();
            string StrError = string.Empty;
            DMPurchaseOrder obj_PO = new DMPurchaseOrder();
            Ds = obj_PO.GetPOForPrint(reqid, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                //-------------------------------------------------[Mail Code]-------------------------------------------------
                string smtpServer = "smtp.gmail.com";//localhost
                string userName = "laxman.washivale@sapragroup.com";//sapragrouppune@gmail.com
                string password = "shravani";//saprapune@12345
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
                //if (txtCC.Text != "")
                //{
                //    msg.Cc = txtCC.Text;
                //}
                //if (txtBcc.Text != "")
                //{
                //    msg.Bcc = txtBcc.Text;
                //}

                msg.From = "laxman.washivale@sapragroup.com";
                msg.Subject = "PO " + Ds.Tables[0].Rows[0]["PONo"].ToString().ToUpper() + " Has Been Approved";
                string mystring = "Hello Sir,<br/> PO No. : " + Ds.Tables[0].Rows[0]["PONo"].ToString() + "<br/>" + "PO Date :" + Ds.Tables[0].Rows[0]["PODate"].ToString() + "<br/> Please Check This PO";
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

    private void GetConfirm()
    {
        string RequestFor = string.Empty;
        string RequestBY = string.Empty;
        string StrError = string.Empty;
        int RequestID = 0;
        DateTime RequestDate = DateTime.Now;
        int LoginID = 0;
        DateTime LoginDate =DateTime.Now;
        RequestFor=Convert.ToString(Request.QueryString["for"].ToString());
        RequestID = Convert.ToInt32(Request.QueryString["request"].ToString());
        RequestBY = Convert.ToString(Request.QueryString["by"].ToString());

        DMUserLogin obj_Login = new DMUserLogin();
        DataSet dsLogin = obj_Login.GetLoginIDFORMAILREQUEST(RequestBY, out StrError);
        if (dsLogin.Tables.Count > 0 && dsLogin.Tables[0].Rows.Count > 0)
        {
            LoginID = Convert.ToInt32(dsLogin.Tables[0].Rows[0]["UserId"].ToString());
            if (RequestFor == "Req")
            {
                DMApproveRequisition Obj_ApproveRequisition = new DMApproveRequisition();
                ApproveRequisition Entity_ApproveRequisition = new ApproveRequisition();
                Entity_ApproveRequisition.RequisitionCafeId = Convert.ToInt32(RequestID);
                Entity_ApproveRequisition.ReqStatus = "Authorised";
                Entity_ApproveRequisition.AuthorizedTime = DateTime.Now;
                Entity_ApproveRequisition.UserId = LoginID;
                Entity_ApproveRequisition.LoginDate = DateTime.Now;
                int UpdateRow = Obj_ApproveRequisition.UpdateReq_Status_Authorozed(ref Entity_ApproveRequisition, out StrError);
                if (UpdateRow > 0)
                {
                    SendMailREQ(RequestID);
                    LblPass.Text = "Requisition Has Been Successfully Authorized..";
                }
                else
                {
                    LblPass.Text = "Requisition Not Authorize Please Try After Some Time..";
                }
            }
            else
            {
                DMEditPurchaseOrder Obj_EditPO = new DMEditPurchaseOrder();
                EditPurchaseOrder Entity_PurchaseOrder = new EditPurchaseOrder();
                Entity_PurchaseOrder.POId = Convert.ToInt32(RequestID);
                Entity_PurchaseOrder.POStatus = "Authorised";
                Entity_PurchaseOrder.AuthorizedTime = DateTime.Now;
                Entity_PurchaseOrder.UserId = LoginID;
                Entity_PurchaseOrder.LoginDate = DateTime.Now;
                int UpdateRow = Obj_EditPO.UpdatePurchase_OrderStatus_Authroized(ref Entity_PurchaseOrder, out StrError);
                if (UpdateRow > 0)
                {
                    SendMail(RequestID);
                    LblPass.Text = "Purchase Order Has Been Successfully Authorized..";
                }
                else
                {
                    LblPass.Text = "Purchase Order Not Authorize Please Try After Some Time..";
                }
                
            }
        }
    }
}
