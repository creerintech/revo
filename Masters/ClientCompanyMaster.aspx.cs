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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using CrystalDecisions.Shared;
using System.Web.Mail;
public partial class Masters_ClientCompanyMaster : System.Web.UI.Page
{
    #region UserDefined
    private string StrError = string.Empty;
    private string StrCondition = string.Empty;
    DataSet Ds = new DataSet();
    DMClienCompanyMaster Obj_CCMaster = new DMClienCompanyMaster();
    ClientCompMaster Entity_CCMaster = new ClientCompMaster();
    CommanFunction obj_Comman = new CommanFunction();
    DataSet DS = new DataSet();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BtnUpdate.Visible = false;
            BtnDelete.Visible = false;
            setInitialRow();
            ReportGrid(StrCondition);
            ViewState["GridIndex"] = null;
            EmptyFormFields();
            EmptyMasterForm();
        }
    }

    public int SendMail(string MailAttachPath)
    {
        int Status = 0;
        try
        {

            string smtpServer = "smtp.gmail.com";
            string userName = "revomms@gmail.com";
            string password = "revo@123";

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
            string mystring = string.Empty;
            msg.To = TXTKTO.Text;
            msg.Cc = TXTKCC.Text;
            msg.Subject = TXTKSUBJECT.Text;
            msg.Body = TxtBody.Text;
            msg.From = "revomms@gmail.com";
            String sFile = MailAttachPath;
            if (CHKATTACHBROUCHER.Checked == true)
            {
                msg.Attachments.Add(new System.Web.Mail.MailAttachment(sFile));
            }
            SmtpMail.SmtpServer = smtpServer;
            SmtpMail.Send(msg);
            obj_Comman.ShowPopUpMsg("Mail Send..", this.Page);
            //File.Delete(MailAttachPath);
            //  -------------------------------------------------[End Mail Code]-------------------------------------------------

        }
        catch (Exception)
        {
            obj_Comman.ShowPopUpMsg("Network Error Occured While Sending Mail.", this.Page);
        }
        
            return Status;
       
    }

    private void GETCLIENTPDF(int ClientCompantID, int CompanyID, int WITHREMARK)
    {
        try
        {
            DMClienCompanyMaster obj_Template = new DMClienCompanyMaster();
            ReportDocument CRpt = new ReportDocument();
            DataSet dslogin = new DataSet();
            string PDFMaster = string.Empty;
                string StrError = string.Empty;
                dslogin = obj_Template.GetCCMFORPRINT(ClientCompantID, CompanyID, out StrError);
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
                if (WITHREMARK == 0)
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ClientCompanyPrint.rpt"));
                    CRpt.SetDataSource(dslogin);
                    string DATE = DateTime.Now.ToString("dd-MMM-yyyy ss");
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "ClientCompay - " + (DATE) + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    CHKATTACHBROUCHER.Checked = true;
                    CHKATTACHBROUCHER.Text = "Contact Details";
                    CHKATTACHBROUCHER.ToolTip = PDFMaster;

                    iframepdf.Attributes.Add("src", "../TempFiles/" + "ClientCompay - " + (DATE) + ".pdf");
                }
                else
                {
                    CRpt.Load(Server.MapPath("~/CrystalPrint/ClientCompanyPrintWithRemark.rpt"));
                    CRpt.SetDataSource(dslogin);
                    string DATE = DateTime.Now.ToString("dd-MMM-yyyy ss");
                    PDFMaster = Server.MapPath(@"~/TempFiles/" + "ClientCompayDetails - " + (DATE) + ".pdf");
                    CRpt.ExportToDisk(ExportFormatType.PortableDocFormat, PDFMaster);
                    CHKATTACHBROUCHER.Checked = true;
                    CHKATTACHBROUCHER.Text = "Contact Details";
                    CHKATTACHBROUCHER.ToolTip = PDFMaster;

                    iframepdf.Attributes.Add("src", "../TempFiles/" + "ClientCompayDetails - " + (DATE) + ".pdf");
                }
        }
        catch(Exception ex)
        {
        }
    }

    private void GETDATAFORMAIL(int From,int ClientCompanyID,int WithDetails)
    {
        try
        {
            DMStockLocation Obj_SL = new DMStockLocation();
            DataSet DS = new DataSet();
            string To = string.Empty;
            string CC = string.Empty;
            string Body = string.Empty;
            DS = Obj_SL.GetStockLocation("", out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[1].Rows.Count > 0)
            {
                DDLKCMPY.DataSource = DS.Tables[1];
                DDLKCMPY.DataValueField = "CompanyId";
                DDLKCMPY.DataTextField = "CompanyName";
                DDLKCMPY.DataBind();
                DDLKCMPY.SelectedValue = "1";
            }
            Obj_SL = null;
            DS = null;
            TXTKTO.Text = "";
            TXTKCC.Text = "";
            string description = string.Empty;
            if (WithDetails == 0)
            {
                description = Server.HtmlDecode("HELLO,<br /><br />AS REQUESTED, PLEASE FIND THE ATTACHED THE CONTACT DETAILS.<br /><br />REGARDS,<br />KARIA DEVELOPERS.").Replace("<br />", Environment.NewLine);
            }
            if(WithDetails==1)
            {
                description = Server.HtmlDecode("HELLO,<br /><br />AS REQUESTED, PLEASE FIND THE ATTACHED THE CONTACT DETAILS & THE SUPPLY DETAILS. THIS INFORMATION IS CONFIDENTIAL & WE REQUEST YOU NOT TO SHARE IT.<br /><br />REGARDS,<br />KARIA DEVELOPERS.").Replace("<br />", Environment.NewLine);
            }
            TxtBody.Text = description;
            //TxtBody.Text = "HELLO,<br />PLEASE FIND ATTACHED THE CONTACT CARD AS REQUESTED BY YOU.<br />REGARDS,<br />KARIA DEVELOPERS,<br />PUNE.";
            //TxtBody.Text.Replace(Environment.NewLine, "<br />");
            if (From == 1)
            {
                if (WithDetails == 0)
                {
                    GETCLIENTPDF(Convert.ToInt32(ViewState["EditID"]), 1, 0);
                    LBLDESCWITH.Text = "0";
                    LBLID.Text = Convert.ToString(ViewState["EditID"]);
                }
                else
                {
                    GETCLIENTPDF(Convert.ToInt32(ViewState["EditID"]), 1, 1);
                    LBLDESCWITH.Text = "1";
                    LBLID.Text = Convert.ToString(ViewState["EditID"]);
                }
            }
            else if(From==2)
            {
                if (WithDetails == 0)
                {
                    GETCLIENTPDF(ClientCompanyID, 1,0);
                    LBLDESCWITH.Text = "0";
                    LBLID.Text = Convert.ToString(ClientCompanyID);
                }
                else
                {
                    GETCLIENTPDF(ClientCompanyID, 1,1);
                    LBLDESCWITH.Text = "1";
                    LBLID.Text = Convert.ToString(ClientCompanyID);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void EmptyFormFields()
    {
        TxtPersonName.Text = string.Empty;
        TxtDesignation.Text = string.Empty;
        TxtContactNo1.Text = string.Empty;
        TxtContactNo2.Text = string.Empty;
        TxtEMailId1.Text = string.Empty;
        TxtEmailId2.Text = string.Empty;
        TxtNote.Text = string.Empty;
        TxtPersonName.Focus();
        ImgAddGrid.ImageUrl = "~/Images/Icon/Gridadd.png";
        ViewState["GridIndex"] = null;
    }
    public void EmptyMasterForm()
    {
        TxtCompanyName.Text = string.Empty;
        TxtSupplierFor.Text=string.Empty;
        TxtAddress.Text=string.Empty;
        TxtWebsite.Text=string.Empty;
        TxtRemark.Text = string.Empty;
        TxtCompanyName.Focus();
        BtnSave.Visible = true;
        BtnMail.Visible = BtnUpdate.Visible = false;
        BtnDelete.Visible = false;
    }
    public void setInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("#", typeof(Int32));
        dt.Columns.Add("PersonName", typeof(string));
        dt.Columns.Add("Designation", typeof(string));
        dt.Columns.Add("ContactNo1", typeof(string));
        dt.Columns.Add("ContactNo2", typeof(string));
        dt.Columns.Add("EmailId1", typeof(string));
        dt.Columns.Add("EmailId2", typeof(string));
        dt.Columns.Add("Note", typeof(string));
        dr = dt.NewRow();

        dr["#"] = 0;
        dr["PersonName"] = "";
        dr["Designation"] = "";
        dr["ContactNo1"] = "";
        dr["ContactNo2"] = "";
        dr["EmailId1"] = "";
        dr["EmailId2"] = "";
        dr["Note"] = "";

        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        GridDetails.DataSource = dt;
        GridDetails.DataBind();
    }
    protected void ImgAddGrid_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow dtTableRow = null;
                bool DupFlag = false;
                int k = 0;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    if (dtCurrentTable.Rows.Count == 1 && string.IsNullOrEmpty(dtCurrentTable.Rows[0]["PersonName"].ToString()))
                    {
                        dtCurrentTable.Rows.RemoveAt(0);
                    }
                    if (ViewState["GridIndex"] != null)
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            if ((dtCurrentTable.Rows[i]["PersonName"].ToString()) == TxtPersonName.Text)
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            PopUpYesNo.Show();
                            btnPopUpYes.Focus();

                        }
                        else
                        {
                            dtTableRow = dtCurrentTable.NewRow();
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                            dtTableRow["#"] = 0;
                            dtTableRow["PersonName"] = TxtPersonName.Text;
                            dtTableRow["Designation"] = TxtDesignation.Text;
                            dtTableRow["ContactNo1"] = TxtContactNo1.Text;
                            dtTableRow["ContactNo2"] = TxtContactNo2.Text;
                            dtTableRow["EmailId1"] = TxtEMailId1.Text;
                            dtTableRow["EmailId2"] = TxtEmailId2.Text;
                            dtTableRow["Note"] = TxtNote.Text;
                            dtCurrentTable.Rows.Add(dtTableRow);
                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            EmptyFormFields();
                        }

                    }
                    else
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            if (dtCurrentTable.Rows[i]["PersonName"].ToString() == TxtPersonName.Text)
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            PopUpYesNo.Show();
                            btnPopUpYes.Focus();

                        }
                        else
                        {
                            dtTableRow = dtCurrentTable.NewRow();
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                            dtTableRow["#"] = 0;
                            dtTableRow["PersonName"] = TxtPersonName.Text;
                            dtTableRow["Designation"] = TxtDesignation.Text;
                            dtTableRow["ContactNo1"] = TxtContactNo1.Text;
                            dtTableRow["ContactNo2"] = TxtContactNo2.Text;
                            dtTableRow["EmailId1"] = TxtEMailId1.Text;
                            dtTableRow["EmailId2"] = TxtEmailId2.Text;
                            dtTableRow["Note"] = TxtNote.Text;
                            dtCurrentTable.Rows.Add(dtTableRow);
                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            EmptyFormFields();

                        }

                    }

                }
                else
                {


                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void PopUpYesNo_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "yes")
        {
            GetDataInGrid();
        }
        else
        {

        }

    }

    protected void GetDataInGrid()
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //DataRow dtTableRow = null;
            bool DupFlag = false;
            int k = 0;

            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    if ((dtCurrentTable.Rows[i]["PersonName"].ToString()) == TxtPersonName.Text)
                    {
                        DupFlag = true;
                        k = i;
                    }
                }
            }
            if (DupFlag == true)
            {
                dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(dtCurrentTable.Rows[k]["#"].ToString());
                dtCurrentTable.Rows[k]["PersonName"] =  TxtPersonName.Text;
                dtCurrentTable.Rows[k]["Designation"] = (!string.IsNullOrEmpty(TxtDesignation.Text)) ? TxtDesignation.Text : "";
                dtCurrentTable.Rows[k]["ContactNo1"] =  TxtContactNo1.Text;
                dtCurrentTable.Rows[k]["ContactNo2"] =  (!string.IsNullOrEmpty(TxtContactNo1.Text)) ? TxtContactNo1.Text : ""; ;
                dtCurrentTable.Rows[k]["EmailId1"] = TxtEMailId1.Text;
                dtCurrentTable.Rows[k]["EmailId2"] = (!string.IsNullOrEmpty(TxtEmailId2.Text))?TxtEmailId2.Text:"";
                dtCurrentTable.Rows[k]["Note"] = (!string.IsNullOrEmpty(TxtNote.Text))?TxtNote.Text:"";

                ViewState["CurrentTable"] = dtCurrentTable;
                GridDetails.DataSource = dtCurrentTable;
                GridDetails.DataBind();
                EmptyFormFields();
            }
        }

    }

    protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Index;
            if (e.CommandName == "SelectGrid")
            {
                if ((!(string.IsNullOrEmpty(GridDetails.Rows[0].Cells[2].Text))) && GridDetails.Rows[0].Cells[2].Text.Equals("&nbsp"))
                {
                    obj_Comman.ShowPopUpMsg("There Is No Record To Edit", this.Page);
                    TxtPersonName.Focus();
                }
                else
                {
                    ImgAddGrid.ImageUrl = "~/Images/Icon/GridUpdate.png";
                    ImgAddGrid.ToolTip = "Update";
                    //TXTUPDATEVALUE.Text = "0";
                    Index = Convert.ToInt32(e.CommandArgument);

                    ViewState["GridIndex"] = Index;

                    TxtPersonName.Text = GridDetails.Rows[Index].Cells[2].Text;
                    TxtContactNo1.Text = GridDetails.Rows[Index].Cells[4].Text;
                    TxtEMailId1.Text = GridDetails.Rows[Index].Cells[6].Text;
                    if (!GridDetails.Rows[Index].Cells[3].Text.Equals("&nbsp;"))
                    {
                        TxtDesignation.Text = GridDetails.Rows[Index].Cells[3].Text;
                    }
                    else
                    {
                        TxtDesignation.Text = "";
                    }

                    if (!GridDetails.Rows[Index].Cells[5].Text.Equals("&nbsp;"))
                    {
                        TxtContactNo2.Text = GridDetails.Rows[Index].Cells[5].Text;
                    }
                    else
                    {
                        TxtContactNo2.Text = "";
                    }

                    if (!GridDetails.Rows[Index].Cells[7].Text.Equals("&nbsp;"))
                    {
                        TxtEmailId2.Text = GridDetails.Rows[Index].Cells[7].Text;
                    }
                    else
                    {
                        TxtEmailId2.Text = "";
                    }
                    if (!GridDetails.Rows[Index].Cells[8].Text.Equals("&nbsp;"))
                    {
                        TxtNote.Text = GridDetails.Rows[Index].Cells[8].Text;
                    }
                    else
                    {
                        TxtNote.Text = "";
                    }
                  
                }
                TxtPersonName.Focus();
            }
            //ImgAddGrid.ImageUrl = "~/Images/Icon/Gridadd.png";
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void GridDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            if (ViewState["CurrentTable"] != null)
            {
                int id = e.RowIndex;
                DataTable dt = (DataTable)ViewState["CurrentTable"];

                dt.Rows.RemoveAt(id);
                if (dt.Rows.Count > 0)
                {
                    GridDetails.DataSource = dt;
                    ViewState["CurrentTable"] = dt;
                    GridDetails.DataBind();
                }
                else
                {
                    setInitialRow();
                }
                EmptyFormFields();
            }
        }
        catch
        {
        
        }
    }
    public void ReportGrid(string RepCondition)
    {
        try
        {
           
            Ds = Obj_CCMaster.GetItem(RepCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ViewState["CurrentTable1"] = Ds.Tables[0];
                if (Ds.Tables[0].Rows.Count > 16)
                {
                    rptPages.Visible = true;
                    //CallPagging();
                }
                else
                {
                    GrdReport.DataSource = Ds.Tables[0];
                    GrdReport.DataBind();
                    rptPages.Visible = false;
                }
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }

            //TxtSearchCategory_TextChanged(TxtSearchSubCategory, System.EventArgs.Empty);

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    //public void CallPagging()
    //{
    //    #region [Code For Pagging in Repeater]--------------------------------
    //    PageNumber = Convert.ToInt32(1);// Add For Pdgging by Piyush
    //    PagedDataSource pgitems = new PagedDataSource();
    //    DataTable dt = new DataTable();
    //    dt = (DataTable)ViewState["CurrentTable1"];
    //    System.Data.DataView dv = new System.Data.DataView(dt);
    //    pgitems.DataSource = dv;
    //    pgitems.AllowPaging = true;
    //    pgitems.PageSize = 16;
    //    pgitems.CurrentPageIndex = PageNumber;
    //    if (pgitems.PageCount > 1)
    //    {
    //        rptPages.Visible = true;
    //        System.Collections.ArrayList pages = new System.Collections.ArrayList();
    //        for (int i = 0; i < pgitems.PageCount; i++)
    //            pages.Add((i + 1).ToString());
    //        //Extra Code Here
    //        countPage = pgitems.PageCount;
    //        System.Collections.ArrayList pages1 = new System.Collections.ArrayList();
    //        for (int i = 0; i < 5; i++)
    //            pages1.Add((i + 1).ToString());
    //        //End Here
    //        rptPages.DataSource = pages1;
    //        rptPages.DataBind();
    //    }
    //    else
    //    {
    //        //GridDetails.Visible = false;
    //    }
    //    GrdReport.DataSource = pgitems;
    //    GrdReport.DataBind();

    //    ((LinkButton)rptPages.Controls[0].FindControl("btnPrev")).Visible = false;
    //    if ((pgitems.PageCount) <= 5)
    //    {
    //        ((LinkButton)rptPages.Controls[rptPages.Controls.Count - 1].FindControl("btnNext")).Visible = false;
    //    }
    //    #endregion [Code For Pagging in Repeater]--------------------------------
    //}
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string StrError = "";
        int InsertRow = 0, InsertRowDtls = 0;
        DS = Obj_CCMaster.ChkDuplicate(TxtCompanyName.Text.Trim(), out  StrError);
        if (DS.Tables[0].Rows.Count > 0)
        {
            obj_Comman.ShowPopUpMsg("Record is Already Present..", this.Page);
            TxtCompanyName.Focus();
        }
        else
        {
            Entity_CCMaster.Action = 1;
            Entity_CCMaster.CompanyName = TxtCompanyName.Text;
            Entity_CCMaster.SupplierFor = TxtSupplierFor.Text;
            Entity_CCMaster.Address = TxtAddress.Text;
            Entity_CCMaster.WebUrl = TxtWebsite.Text;
            Entity_CCMaster.Remark = TxtRemark.Text;
            Entity_CCMaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
            Entity_CCMaster.CreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            Entity_CCMaster.IsDelete = false;
            InsertRow = Obj_CCMaster.InsertRecord(ref Entity_CCMaster, StrError);
            if (InsertRow > 0)
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtInsert = new DataTable();
                    dtInsert = (DataTable)ViewState["CurrentTable"];
                    for (int i = 0; i < dtInsert.Rows.Count; i++)
                    {
                        Entity_CCMaster.ClientCompanyId = InsertRow;
                        Entity_CCMaster.PersonName = dtInsert.Rows[i]["PersonName"].ToString();
                        //Entity_CCMaster.Address = dtInsert.Rows[i]["Address"].ToString();
                        Entity_CCMaster.Designation = dtInsert.Rows[i]["Designation"].ToString();
                        Entity_CCMaster.ContactNo1 = dtInsert.Rows[i]["ContactNo1"].ToString();
                        Entity_CCMaster.ContactNo2 = dtInsert.Rows[i]["ContactNo2"].ToString();
                        Entity_CCMaster.EmailId1 = dtInsert.Rows[i]["EmailId1"].ToString();
                        Entity_CCMaster.EmailId2 = dtInsert.Rows[i]["EmailId2"].ToString();
                        Entity_CCMaster.Note = dtInsert.Rows[i]["Note"].ToString();
                        InsertRowDtls = Obj_CCMaster.InsertDertailsRecord(ref Entity_CCMaster, StrError);
                        
                    }
                    if(InsertRowDtls>0)
                    {
                        obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                        EmptyFormFields();
                        EmptyMasterForm();
                        setInitialRow();
                        ReportGrid(StrCondition);
                    }
                }
            }
        }

    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, UpdateRowDtls = 0, UpdateRow1 = 0, UpdateUnitCal = 0;
         string StrError = "";
        try
        {



            if (ViewState["EditID"] != null)
            {
                Entity_CCMaster.ClientCompanyId = Convert.ToInt32(ViewState["EditID"]);
            }
            Entity_CCMaster.CompanyName = TxtCompanyName.Text;
            Entity_CCMaster.SupplierFor = TxtSupplierFor.Text;
            Entity_CCMaster.Address = TxtAddress.Text;
            Entity_CCMaster.WebUrl = TxtWebsite.Text;
            Entity_CCMaster.Remark = TxtRemark.Text;


            Entity_CCMaster.UpdatedBy = Convert.ToInt32(Session["UserId"]);
            Entity_CCMaster.UpdatedDate = DateTime.Now;

            UpdateRow = Obj_CCMaster.UpdateRecord(ref Entity_CCMaster, out StrError);

            if (UpdateRow > 0)
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtInsert = new DataTable();
                    dtInsert = (DataTable)ViewState["CurrentTable"];
                    for (int i = 0; i < dtInsert.Rows.Count; i++)
                    {
                        //Entity_CCMaster.PersonId = Convert.ToInt32(dtInsert.Rows[i]["#"].ToString());
                        Entity_CCMaster.ClientCompanyId = Convert.ToInt32(ViewState["EditID"]);
                        Entity_CCMaster.PersonName = dtInsert.Rows[i]["PersonName"].ToString();
                        Entity_CCMaster.Designation = dtInsert.Rows[i]["Designation"].ToString();
                        Entity_CCMaster.ContactNo1 = dtInsert.Rows[i]["ContactNo1"].ToString();
                        Entity_CCMaster.ContactNo2 = dtInsert.Rows[i]["ContactNo2"].ToString();
                        Entity_CCMaster.EmailId1 = dtInsert.Rows[i]["EmailId1"].ToString();
                        Entity_CCMaster.EmailId2 = dtInsert.Rows[i]["EmailId2"].ToString();
                        Entity_CCMaster.Note = dtInsert.Rows[i]["Note"].ToString();
                        UpdateRowDtls=Obj_CCMaster.InsertDertailsRecord(ref Entity_CCMaster,StrError);
                    }
                }

                //if (ViewState["CurrentTableSize"] != null)
                //{
                //    DataTable dtInsert = new DataTable();
                //    dtInsert = (DataTable)ViewState["CurrentTableSize"];
                //    for (int i = 0; i < dtInsert.Rows.Count; i++)
                //    {
                //        //Entity_ItemMaster.ItemId = Convert.ToInt32(ViewState["EditID"]);
                //        //Entity_ItemMaster.SupplierId = Convert.ToInt32(dtInsert.Rows[i]["SizeId"].ToString());
                //        //UpdateRowDtls = Obj_ItemMaster.InsertDetailsSizeRecord(ref Entity_ItemMaster, out StrError);
                //    }
                //}
            }
            if (UpdateRow > 0)
            {
                obj_Comman.ShowPopUpMsg("Record Updated Successfully", this.Page);
                EmptyFormFields();               
                EmptyMasterForm();
                setInitialRow();
                ReportGrid(StrCondition);
                Entity_CCMaster = null;
                Obj_CCMaster = null;
            }
        }
        catch(Exception ex) 
        {
            throw new Exception(ex.Message);
        }
    }
    protected void GrdReport_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {

                        if (Convert.ToInt32(e.CommandArgument) != 0)
                        {
                            BtnSave.Visible = false;
                            BtnDelete.Visible = true;
                            BtnMail.Visible = BtnUpdate.Visible = true;
                            ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                            Ds = Obj_CCMaster.GetItemForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                            {
                                TxtCompanyName.Text = Ds.Tables[0].Rows[0]["Name"].ToString();
                                TxtSupplierFor.Text = Ds.Tables[0].Rows[0]["SupplierFor"].ToString();
                                TxtAddress.Text = Ds.Tables[0].Rows[0]["Address"].ToString();
                                TxtWebsite.Text = Ds.Tables[0].Rows[0]["WebSite"].ToString();
                                TxtRemark.Text = Ds.Tables[0].Rows[0]["Remark"].ToString();
                                TxtCompanyName.Focus();
                            }
                            else
                            {
                                setInitialRow();
                                EmptyFormFields();
                            }
                            if (Ds.Tables[1].Rows.Count > 0)
                            {
                                ViewState["CurrentTable"] = Ds.Tables[1];
                                GridDetails.DataSource = Ds.Tables[1];
                                GridDetails.DataBind();
                                //TxtPersonName.Focus();

                            }
                            else
                            {
                                setInitialRow();
                                //EmptyFormFields();
                            }

                        }
                        break;
                    }
                case ("MAIL"):
                    {
                        GETDATAFORMAIL(2, Convert.ToInt32(e.CommandArgument),0);
                        MDPopUpYesNoMail.Show();
                        BtnPopMail.Focus();
                        break;
                    }
                case ("MAILDetails"):
                    {
                        GETDATAFORMAIL(2, Convert.ToInt32(e.CommandArgument),1);
                        MDPopUpYesNoMail.Show();
                        BtnPopMail.Focus();
                        break;
                    }
            }

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        EmptyFormFields();
        EmptyMasterForm();
        setInitialRow();
        ReportGrid(StrCondition);
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int DeleteId = 0;

            if (ViewState["ITEMINUSEORNOT"] == null)
            {
                if (ViewState["EditID"] != null)
                {
                    DeleteId = Convert.ToInt32(ViewState["EditID"]);
                }
                if (DeleteId != 0)
                {
                    Entity_CCMaster.ClientCompanyId = DeleteId;
                    Entity_CCMaster.DeletedBy = Convert.ToInt32(Session["UserID"]);
                    Entity_CCMaster.DeletedDate = DateTime.Now;
                    Entity_CCMaster.IsDelete = true;

                    int iDelete = Obj_CCMaster.DeleteRecord(ref Entity_CCMaster, out StrError);
                    if (iDelete > 0)
                    {
                        obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                        EmptyFormFields();
                        EmptyMasterForm();
                        setInitialRow();
                        ReportGrid(StrCondition);
                    }
                    else
                    {
                        obj_Comman.ShowPopUpMsg("Item is used in Transaction, Please delete references from Transaction to perform delete operation...!", this.Page);
                    }
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Cant Delete Record.. \nUsed In Further Process..!", this.Page);
            }
            Entity_CCMaster = null;
            Obj_CCMaster = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text;
        try
        {
            Ds = Obj_CCMaster.GetSearchItem(StrCondition);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ViewState["CurrentTable1"] = Ds.Tables[0];
                    GrdReport.DataSource = Ds.Tables[0];
                    GrdReport.DataBind();
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }
            GrdReport.Focus();
            Ds = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMClienCompanyMaster Obj_CCMaster = new DMClienCompanyMaster();
        String[] SearchList = Obj_CCMaster.GetSuggestedRecord(prefixText);
        return SearchList;
    }

   
    protected void BtnMail_Click(object sender, EventArgs e)
    {
        GETDATAFORMAIL(1,1,0);
        MDPopUpYesNoMail.Show();
        BtnPopMail.Focus();
    }
    protected void PopUpYesNoMail_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "yes")
        {
            int ststus=SendMail(CHKATTACHBROUCHER.ToolTip.ToString());
        }
        else
        {

        }

    }

    protected void DDLKCMPY_SelectedIndexChanged(object sender, EventArgs e)
    {
            try
            {
                if(Convert.ToInt32(DDLKCMPY.SelectedValue.ToString())>0)
                {
                    GETCLIENTPDF(Convert.ToInt32(LBLID.Text.ToString()), Convert.ToInt32(DDLKCMPY.SelectedValue.ToString()), Convert.ToInt32(LBLDESCWITH.Text.ToString()));
                    MDPopUpYesNoMail.Show();
                    BtnPopMail.Focus();
                }
            }
            catch (Exception ex) { }
            finally            {}
    }
    
}