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
using System.Web.Mail;
using System.IO;

public partial class Transactions_EditPurchaseOrder : System.Web.UI.Page
{
    #region [private variables]
        DMEditPurchaseOrder Obj_EditPO = new DMEditPurchaseOrder();
        EditPurchaseOrder Entity_PurchaseOrder = new EditPurchaseOrder();
        CommanFunction obj_Comman = new CommanFunction();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        DataSet Ds = new DataSet();
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
    #endregion

    #region[UserDefine Function]

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

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Authorized Purchase Order'");
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

    private void MakeEmptyForm()
    {
        BindReportGrid(StrCondition);
        GETHIGHLITE();
        if(!FlagAdd)
         BtnSave.Visible = true;
        BtnAuthorized.Visible = false;
        TR_PODtls.Visible = false;
        TxtSearch.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
    }

    private void SetInitialRow_ItemRateDtls()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("PONo", typeof(string));
            dt.Columns.Add("PODate", typeof(string));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("OrderQuantity", typeof(string));
            dt.Columns.Add("Rate", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("PORate", typeof(string));
            dt.Columns.Add("RateDiff", typeof(string));
            

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["PONo"] = "";
            dr["PODate"] = "";
            dr["ItemCode"] = "";
            dr["ItemName"] = "";
            dr["OrderQuantity"] = "";
            dr["Rate"] = "";
            dr["Amount"] = "";
            dr["Location"] = "";
            dr["PORate"] = "";
            dr["RateDiff"] = "";

            dt.Rows.Add(dr);

            ViewState["CurrentTableItemRateDtls"] = dt;
            GrdItemWiseRate.DataSource = dt;
            GrdItemWiseRate.DataBind();
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
            dt.Columns.Add("SuplierName", typeof(string));
            dt.Columns.Add("PONo", typeof(int));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("PODate", typeof(string));
            dt.Columns.Add("Amount", typeof(Decimal));
            dt.Columns.Add("POStatus", typeof(string));
            dt.Columns.Add("POId", typeof(Int32));
            dt.Columns.Add("SuplierId", typeof(Int32));
            dt.Columns.Add("Remark", typeof(string));
            dr = dt.NewRow();

            dr["#"] = 0;
            dr["SuplierName"] = string.Empty;
            dr["PONo"] = 0;
            dr["Location"] = "";
            dr["PODate"] = DateTime.Now.ToString("dd-MMM-yyyy");
            dr["Amount"] = 0;
            dr["POStatus"] = "";
            dr["POId"] = 0;
            dr["SuplierId"] = 0;
            dr["Remark"] = "";


            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
            GrdReqPO.DataSource = dt;
            GrdReqPO.DataBind();
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
            dt.Columns.Add("SuplierName", typeof(string));
            dt.Columns.Add("PONo", typeof(int));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("PODate", typeof(string));
            dt.Columns.Add("Amount", typeof(Decimal));
            dt.Columns.Add("POStatus", typeof(string));
            dt.Columns.Add("POId", typeof(Int32));
            dt.Columns.Add("SuplierId", typeof(Int32));
            dt.Columns.Add("Remark", typeof(Int32));

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["SuplierName"] = string.Empty;
            dr["PONo"] = 0;
            dr["Location"] = "";
            dr["PODate"] = DateTime.Now.ToString("dd-MMM-yyyy");
            dr["Amount"] = 0;
            dr["POStatus"] = "";
            dr["POId"] = 0;
            dr["SuplierId"] = 0;
            dr["Remark"] = "";

            dt.Rows.Add(dr);

            ViewState["CurrentTablePO"] = dt;
            GrdReqPO.DataSource = dt;
            GrdReqPO.DataBind();
        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void BindReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            DsReport = Obj_EditPO.GetPurchase_Order(RepCondition, out StrError);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                GrdReqPO.DataSource = DsReport.Tables[0];
                GrdReqPO.DataBind();

            }
            else
            {
                GrdReqPO.DataSource = null;
                GrdReqPO.DataBind();
                SetInitialRow_ReqDetails();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public void BindReportGridMISC(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            DsReport = Obj_EditPO.GetPurchase_OrderMISC(RepCondition, out StrError);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                GrdReqPO.DataSource = DsReport.Tables[0];
                GrdReqPO.DataBind();

            }
            else
            {
                GrdReqPO.DataSource = null;
                GrdReqPO.DataBind();
                SetInitialRow_ReqDetails();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void MakeEnableControl(bool flag)
    {
        for (int p = 0; GrdPODtls.Rows.Count > p; p++)
        {
            ((TextBox)GrdPODtls.Rows[p].FindControl("GrdtxtOrderQty")).Enabled = flag;
        }
    }

    public void BindPurchaseOrderDetails(Int32 SuplierId, string status)
    {
        try
        {
            Ds = Obj_EditPO.GetRequisitionDtls_SupplierWise(SuplierId, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                GrdPODtls.DataSource = Ds.Tables[0];
                GrdPODtls.DataBind();
                if (status == "Authorised")
                {
                    MakeEnableControl(false);
                }
                else
                {
                    MakeEnableControl(true);
                }
                //TR_PODtls.Visible = true;

                //if (Ds.Tables[1].Rows.Count > 0)
                //{
                //    PLBLPONO.Text = Ds.Tables[1].Rows[0]["PONo"].ToString();
                //    PLBLPODATE.Text = Ds.Tables[1].Rows[0]["PODate"].ToString();
                //    PLBLSUPNAME.Text = Ds.Tables[1].Rows[0]["SuplierName"].ToString();

                //    PLBLDEKHREKH.Text = Ds.Tables[1].Rows[0]["DekhrekhAmt"].ToString();
                //    PLBLHAMALI.Text = Ds.Tables[1].Rows[0]["HamaliAmt"].ToString();
                //    PLBLCESS.Text = Ds.Tables[1].Rows[0]["CESSAmt"].ToString();
                //    PLBLFRIEGHT.Text = Ds.Tables[1].Rows[0]["FreightAmt"].ToString();
                //    PLBLPACKING.Text = Ds.Tables[1].Rows[0]["PackingAmt"].ToString();
                //    PLBLPOSTAGE.Text = Ds.Tables[1].Rows[0]["PostageAmt"].ToString();
                //    PLBLOTHERCHARGES.Text = Ds.Tables[1].Rows[0]["OtherCharges"].ToString();
                //}

                //if (Ds.Tables[2].Rows.Count > 0)
                //{
                //    GRDPOPUPFOREDIT.DataSource = Ds.Tables[2];
                //    GRDPOPUPFOREDIT.DataBind();
                //}

                //if (Ds.Tables[3].Rows.Count > 0)
                //{
                //    PLBLNETPAIDAMOUNT.Text = Ds.Tables[3].Rows[0]["Amount1"].ToString();
                //}

                //if (Ds.Tables[4].Rows.Count > 0)
                //{
                //    PLBLTERMS.Text = Ds.Tables[4].Rows[0]["TermsCondition"].ToString();
                //}

                //if (Ds.Tables[5].Rows.Count > 0)
                //{
                //    PLBLCMPYNAME.Text = Ds.Tables[5].Rows[0]["CompanyName"].ToString();
                //}
            }
            else
            {
                GrdPODtls.DataSource = null;
                GrdPODtls.DataBind();

                TR_PODtls.Visible = false;
            }

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void BindPurchaseOrderLastRatesDetails(string Cond,Decimal NewRate)
    {
        try
        {
            Ds = Obj_EditPO.GetRateCompare(Cond, NewRate, 0, out StrError);
            //Ds = Obj_EditPO.GetRateCompare(Cond, 0,0, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                GrdItemWiseRate.DataSource = Ds.Tables[0];
                GrdItemWiseRate.DataBind();
            }
            else
            {
                GrdItemWiseRate.DataSource = null;
                GrdItemWiseRate.DataBind();
                SetInitialRow_ItemRateDtls();
             
            }

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    #endregion

    public void SendMail(int reqid)
    {
        try
        {
            DataSet dslogin = new DataSet();
            Ds = new DataSet();
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

    public void GETHIGHLITE()
    {
        for (int i = 0; i < GrdReqPO.Rows.Count; i++)
        {
            if ((GrdReqPO.Rows[i].Cells[8].Text) == "Generated")
            {
                GrdReqPO.Rows[i].BackColor = System.Drawing.Color.White;
                GrdReqPO.Rows[i].ForeColor = System.Drawing.Color.Black;
            }
            if ((GrdReqPO.Rows[i].Cells[8].Text) == "Approved")
            {
                GrdReqPO.Rows[i].BackColor = System.Drawing.Color.Yellow;
                GrdReqPO.Rows[i].ForeColor = System.Drawing.Color.Black;
            }
            if ((GrdReqPO.Rows[i].Cells[8].Text) == "Authorised")
            {
                GrdReqPO.Rows[i].BackColor = System.Drawing.Color.YellowGreen;
                GrdReqPO.Rows[i].ForeColor = System.Drawing.Color.Black;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           MakeEmptyForm();
           CheckUserRight();
        }
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

    protected void ImageApproved_Click(object sender, EventArgs e)
    {
        //------Approved Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        GrdReqPO.Rows[currrow].Cells[8].Text = "Approved";
        ((ImageButton)GrdReqPO.Rows[currrow].FindControl("ImageCancel")).Visible = true;
        ((ImageButton)GrdReqPO.Rows[currrow].FindControl("ImageApproved")).Visible = false;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

    }

    protected void ImageCancel_Click(object sender, EventArgs e)
    {
        //------Cancel Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        if (GrdReqPO.Rows[currrow].Cells[8].Text == "Approved")
        {
            GrdReqPO.Rows[currrow].Cells[8].Text = "Generated";
            ((ImageButton)GrdReqPO.Rows[currrow].FindControl("ImageApproved")).Visible = true;
            ((ImageButton)GrdReqPO.Rows[currrow].FindControl("ImageCancel")).Visible = false;
        }
        if (GrdReqPO.Rows[currrow].Cells[8].Text == "Authorised")
        {
            GrdReqPO.Rows[currrow].Cells[8].Text = "Approved";
            ((ImageButton)GrdReqPO.Rows[currrow].FindControl("ImageAuthorised")).Visible = true;
            ((ImageButton)GrdReqPO.Rows[currrow].FindControl("ImageCancel")).Visible = false;
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

    }

    protected void ImageAuthorised_Click(object sender, EventArgs e)
    {
        //------Authorised Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        GrdReqPO.Rows[currrow].Cells[8].Text = "Authorised";
        ((ImageButton)GrdReqPO.Rows[currrow].FindControl("ImageCancel")).Visible = true;
        ((ImageButton)GrdReqPO.Rows[currrow].FindControl("ImageAuthorised")).Visible = false;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0;
        for (int j = 0; j < GrdReqPO.Rows.Count; j++)
        {
            if ((GrdReqPO.Rows[j].Cells[8].Text) == "Approved")
            {
                Entity_PurchaseOrder.POId = Convert.ToInt32(GrdReqPO.Rows[j].Cells[9].Text.ToString());
                Entity_PurchaseOrder.POStatus = "Approved";
                Entity_PurchaseOrder.ApprovedTime = DateTime.Now;
                Entity_PurchaseOrder.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_PurchaseOrder.LoginDate = DateTime.Now;
                UpdateRow = Obj_EditPO.UpdatePurchase_OrderStatus_Approved(ref Entity_PurchaseOrder, out StrError);
                ((ImageButton)GrdReqPO.Rows[j].FindControl("ImageCancel")).Visible = false;
                ((ImageButton)GrdReqPO.Rows[j].FindControl("ImageAuthorised")).Visible = true;
            }
            if ((GrdReqPO.Rows[j].Cells[8].Text) == "Authorised")
            {
                Entity_PurchaseOrder.POId = Convert.ToInt32(GrdReqPO.Rows[j].Cells[9].Text.ToString());
                Entity_PurchaseOrder.POStatus = "Authorised";
                Entity_PurchaseOrder.AuthorizedTime = DateTime.Now;
                Entity_PurchaseOrder.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_PurchaseOrder.LoginDate = DateTime.Now;
                UpdateRow = Obj_EditPO.UpdatePurchase_OrderStatus_Authroized(ref Entity_PurchaseOrder, out StrError);
                ((ImageButton)GrdReqPO.Rows[j].FindControl("ImageSuccess")).Visible = true;
                ((ImageButton)GrdReqPO.Rows[j].FindControl("ImageAuthorised")).Visible = false;
                ((ImageButton)GrdReqPO.Rows[j].FindControl("ImageCancel")).Visible = false;
                
                //SendMail(Convert.ToInt32(GrdReqPO.Rows[j].Cells[9].Text.ToString()));
            }
        }
        MakeEmptyForm();
        GETHIGHLITE();
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }

    protected void GrdReqPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int j = 0; j < GrdReqPO.Rows.Count; j++)
        {
            if ((GrdReqPO.Rows[j].Cells[8].Text) == "Generated")
            {
                ((ImageButton)GrdReqPO.Rows[j].FindControl("ImageApproved")).Visible = true;
            }
            if ((GrdReqPO.Rows[j].Cells[8].Text) == "Approved")
            {
                ((ImageButton)GrdReqPO.Rows[j].FindControl("ImageApproved")).Visible = false;
                ((ImageButton)GrdReqPO.Rows[j].FindControl("ImageAuthorised")).Visible = true;
            }
            if ((GrdReqPO.Rows[j].Cells[8].Text) == "Authorised")
            {
                ((ImageButton)GrdReqPO.Rows[j].FindControl("ImageAuthorised")).Visible = false;
                ((ImageButton)GrdReqPO.Rows[j].FindControl("ImageApproved")).Visible = false;
                ((ImageButton)GrdReqPO.Rows[j].FindControl("ImageSuccess")).Visible = true;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP1", "javascript:HidePOP();", true);


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
                        int SupplierID = Convert.ToInt32(GrdReqPO.Rows[CurrRow].Cells[9].Text);
                        BindPurchaseOrderDetails(SupplierID, GrdReqPO.Rows[CurrRow].Cells[8].Text);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:ShowPOP();", true);
                    }
                    break;

                case ("Delete"):
                    {
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
            this.GrdReqPO.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            Ds = Obj_EditPO.GetPurchase_Order(StrCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                GrdReqPO.DataSource = Ds.Tables[0];
                this.GrdReqPO.DataBind();
                GETHIGHLITE();
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

            }
            else
            {
                //SetInitialRow_ReportGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {

        StrCondition = TxtSearch.Text.Trim();
        BindReportGrid(StrCondition);
        GETHIGHLITE();
        for (int j = 0; j < GrdReqPO.Rows.Count; j++)
        {
            if (GrdReqPO.Rows[j].Cells[8].Text == "Generated")
            {

                BtnSave.Visible = true;
                BtnAuthorized.Visible = false;
                BtnCancel.Visible = true;
                TR_PODtls.Visible = false;
            }
            if (GrdReqPO.Rows[j].Cells[8].Text == "Authorized")
            {

                BtnSave.Visible = false;
                BtnAuthorized.Visible = false;
                BtnCancel.Visible = true;
                TR_PODtls.Visible = false;
            }
            if (GrdReqPO.Rows[j].Cells[8].Text == "Approved")
            {

                BtnSave.Visible = false;
                BtnAuthorized.Visible = true;
                BtnCancel.Visible = true;
                TR_PODtls.Visible = false;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMEditPurchaseOrder Obj_EditPO = new DMEditPurchaseOrder();
        String[] SearchList = Obj_EditPO.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, iUpdate = 0;
        try
        {
            if (GrdPODtls.Rows.Count > 0)
            {
                if (Convert.ToInt32(GrdPODtls.Rows[0].Cells[2].Text) > 0)
                {
                    for (int row = 0; GrdPODtls.Rows.Count > row; row++)
                    {
                        iUpdate = Obj_EditPO.UpdateRecord(Convert.ToInt32(GrdPODtls.Rows[row].Cells[2].Text), Convert.ToDecimal(((TextBox)GrdPODtls.Rows[row].FindControl("GrdtxtOrderQty")).Text), Convert.ToInt32(Session["UserId"]), Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDecimal(((TextBox)GrdPODtls.Rows[row].FindControl("GrdtxtAmount")).Text), Convert.ToInt32(GrdPODtls.Rows[row].Cells[5].Text), Convert.ToString(((TextBox)GrdPODtls.Rows[row].FindControl("GrdtxtRemark")).Text), Convert.ToInt32(((Label)GrdPODtls.Rows[row].FindControl("LblProcessId")).Text), out StrError);
                    }
                    if (iUpdate > 0)
                    {
                        obj_Comman.ShowPopUpMsg("Record Updated Successfully!", this.Page);
                        MakeEmptyForm();
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

 
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void GrdtxtOrderQty_TextChanged(object sender, EventArgs e)
    {
        #region[Calculate]
        TextBox txt = (TextBox)sender;
        Decimal NEWPO = !string.IsNullOrEmpty(txt.Text) ? Convert.ToDecimal(txt.Text) : 0;
        GridViewRow grd = (GridViewRow)txt.Parent.Parent;
        grd.Cells[11].Text = (Convert.ToDecimal(grd.Cells[10].Text) * NEWPO).ToString("#0.00");
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

        #endregion
    }

    protected void ImgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        DMEditPurchaseOrder Obj_EditPO = new DMEditPurchaseOrder();
        String[] SearchList = Obj_EditPO.GetSuggestedRecordItemWise(prefixText);
        return SearchList;

    }

    protected void TxtSearchMisc_TextChanged(object sender, EventArgs e)
    {

        StrCondition = txtMisc.Text.Trim();
        if (string.IsNullOrEmpty(StrCondition))
        {
            BindReportGrid(StrCondition);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

        }
        else
        {
            BindReportGridMISC(StrCondition);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

        }
        GETHIGHLITE();
        for (int j = 0; j < GrdReqPO.Rows.Count; j++)
        {
            if (GrdReqPO.Rows[j].Cells[9].Text == "Generated")
            {

                BtnSave.Visible = true;
                BtnAuthorized.Visible = false;
                BtnCancel.Visible = true;
                TR_PODtls.Visible = false;
            }
            if (GrdReqPO.Rows[j].Cells[9].Text == "Authorized")
            {

                BtnSave.Visible = false;
                BtnAuthorized.Visible = false;
                BtnCancel.Visible = true;
                TR_PODtls.Visible = false;
            }
            if (GrdReqPO.Rows[j].Cells[9].Text == "Approved")
            {

                BtnSave.Visible = false;
                BtnAuthorized.Visible = true;
                BtnCancel.Visible = true;
                TR_PODtls.Visible = false;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);

    }

    protected void GrdPODtls_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        string strcond = string.Empty;
                        int CurrRow = Convert.ToInt32(e.CommandArgument);
                        int ItemID = !string.IsNullOrEmpty(GrdPODtls.Rows[CurrRow].Cells[5].Text) ? Convert.ToInt32(GrdPODtls.Rows[CurrRow].Cells[5].Text) : 0; ;
                        int ItemDescID =!string.IsNullOrEmpty(GrdPODtls.Rows[CurrRow].Cells[17].Text)? Convert.ToInt32(GrdPODtls.Rows[CurrRow].Cells[17].Text):0;
                        if (ItemID > 0)
                        {
                            strcond += " AND POD.ItemId= " + ItemID;
                        }
                        if (ItemDescID > 0)
                        {
                            strcond += " AND POD.FK_ItemDtlsId= " + ItemDescID;
                        }
                        BindPurchaseOrderLastRatesDetails(strcond, Convert.ToDecimal(((TextBox)GrdPODtls.Rows[CurrRow].FindControl("GrdtxtRate")).Text));
                        //ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:ShowPOP();", true);
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
        }

    }
   
}