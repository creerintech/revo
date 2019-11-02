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

public partial class Masters_SupplierMaster : System.Web.UI.Page
{
    #region[Private Variables]
        DMSupplierMaster Obj_SupplierMaster = new DMSupplierMaster();
        SuplierMaster Entity_SupplierMaster = new SuplierMaster();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagAdd, FladDel, FlagEdit = false;
    #endregion

    #region[UserDefine Functions]
     
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("#", typeof(Int32));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("TDescptn", typeof(string));
            dt.Columns.Add("select", typeof(bool));
            dr = dt.NewRow();
            dr["#"] = 0;
            dr["Title"] = "";
            dr["select"] = false;
            dr["TDescptn"] = "";
            dt.Rows.Add(dr);
            ViewState["TermsTable"] = dt;
            GridTermCond.DataSource = dt;
            GridTermCond.DataBind();
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
                    ////Checking User Role========
                    //if (!Session["UserRole"].Equals("Administrator"))
                    //{
                        //Checking Right of users=======

                        System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                        System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                        dsChkUserRight1 = (DataSet)Session["DataSet"];

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Supplier Master'");
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
                        //Checking View Right ========                    
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                        {
                            GrdReport.Visible = false;
                        }
                        //Checking Add Right ========                    
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                        {
                            BtnSave.Visible = false;
                            FlagAdd = true;

                        }
                        //Edit /Delete Column Visible ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                        {
                            BtnDelete.Visible = false;
                            BtnUpdate.Visible = false;
                            FlagEdit = true;
                            FladDel = true;
                        }
                        else
                        {
                            //Checking Delete Right ========
                            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                            {
                                BtnDelete.Visible = false;
                                FladDel = true;
                            }

                            //Checking Edit Right ========
                            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                            {
                                BtnUpdate.Visible = false;
                                FlagEdit = true;
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
            catch (ThreadAbortException)
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
            ViewState["EditID"] = null;
            if(!FlagAdd)
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            BtnDelete.Visible = false;
            TxtSuppCode.Focus();
            TxtSuppCode.Text = string.Empty;
            TxtSuppName.Text = string.Empty;
            TxtSuppAddress.Text = string.Empty;
            TxtTelNo.Text = string.Empty;
            TxtMobile.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtNotes.Text = string.Empty;
            TxtSearch.Text = string.Empty;
            TxtFaxNo.Text = string.Empty;
            ddlState.SelectedValue = "0";
            //----Newly Added Fields----
            TxtWebsite.Text = string.Empty;
            TxtPerName.Text = string.Empty;
            TxtDesignation.Text = string.Empty;
            TxtContPerMobileNo.Text = string.Empty;
            TxtContPerEmail.Text = string.Empty;
            TxtContPerWebsite.Text = string.Empty;
            TxtSTRN.Text = string.Empty;
            lblSerTaxNopath.Text = string.Empty;
            //TxtSTJ.Text = string.Empty;
            //TxtVatNo.Text = string.Empty;
            //TxtTINNO.Text = string.Empty;
            //TxtCSTRN.Text = string.Empty;
            //TxtExciseRange.Text = string.Empty;
            //TxtExciseDivision.Text = string.Empty;
            //TxtExciseCircle.Text = string.Empty;
            //TxtExciseZone.Text = string.Empty;
            //TxtExciseCollectorate.Text = string.Empty;
            //TxtExciseECCNO.Text = string.Empty;
            //TxtTINBINNO.Text = string.Empty;
            TxtPanNo.Text = string.Empty;
            lblPanNo.Text = string.Empty;
            TxtTDSCertificate.Text = string.Empty;
            ImgTaxRegNoPath.ImageUrl = "";
            ImgPanNoPath.ImageUrl = "";
            AccordionPane1.HeaderContainer.Width = 660;
         
            GetSupplierCode();
            FillCombo();
            ReportGrid(StrCondition);
           
            BINDREPORTGRID();
        }

        public void BINDREPORTGRID()
        {
            try
            {
                DataSet Ds = new DataSet();
                Ds = Obj_SupplierMaster.GetTerms(out StrError);
                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    GridTermCond.DataSource = Ds.Tables[0];
                    GridTermCond.DataBind();
                    ViewState["TermsTable"] = Ds.Tables[1];
                }
                else
                {
                    GridTermCond.DataSource = null;
                    GridTermCond.DataBind();
                    SetInitialRow();
                }
                Ds = null;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
   
        public void ReportGrid(string RepCondition)
        {
            try
            {
                Ds = Obj_SupplierMaster.GetSupplier(RepCondition, out StrError);
                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    GrdReport.DataSource = Ds.Tables[0];
                    GrdReport.DataBind();
                }
                else
                {
                    GrdReport.DataSource = null;
                    GrdReport.DataBind();
                }
                Ds = null;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        private void GetSupplierCode()
        {
            try
            {
                Ds = Obj_SupplierMaster.GetSuppcode(out StrError);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    TxtSuppCode.Text = Ds.Tables[0].Rows[0]["SuplierCode"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void FillCombo()
        {
            Ds = Obj_SupplierMaster.FillCombo(out StrError);

            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlState.DataSource = Ds.Tables[0];
                    ddlState.DataTextField = "State";
                    ddlState.DataValueField = "StateID";
                    ddlState.DataBind();

                }
            }
        }

    #endregion

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMSupplierMaster Obj_SupplierMaster = new DMSupplierMaster();
        String[] SearchList = Obj_SupplierMaster.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          //  CheckUserRight();
            MakeEmptyForm();
            database db = new database();
            lblcount.Text = db.getDb_Value("select count(*) from SuplierMaster").ToString();
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0;
        int InsertRowDtls = 0;
        try
        {
            Ds = Obj_SupplierMaster.ChkDuplicate(TxtSuppName.Text.Trim(), out StrError);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                obj_Comman.ShowPopUpMsg("Supplier Name Already Exist..!", this.Page);
                TxtSuppName.Focus();
            }
            else
            {
                Entity_SupplierMaster.SuplierCode = TxtSuppCode.Text;
                Entity_SupplierMaster.SuplierName = TxtSuppName.Text.Trim();
                Entity_SupplierMaster.TelNo = TxtTelNo.Text;
                Entity_SupplierMaster.MobileNo = TxtMobile.Text;
                Entity_SupplierMaster.Address = TxtSuppAddress.Text;
                Entity_SupplierMaster.Email = TxtEmail.Text;
                Entity_SupplierMaster.Note = TxtNotes.Text;
                Entity_SupplierMaster.StateID = Convert.ToInt32(ddlState.SelectedValue);
                //--------Newly Added Fields---------
                Entity_SupplierMaster.WebSite = TxtWebsite.Text;
                Entity_SupplierMaster.PersonName = TxtPerName.Text;
                Entity_SupplierMaster.PDesignation = TxtDesignation.Text;
                Entity_SupplierMaster.PMobileNo = TxtContPerMobileNo.Text;
                Entity_SupplierMaster.PEmailId = TxtContPerEmail.Text;
                Entity_SupplierMaster.PWebsite = TxtContPerWebsite.Text;
                Entity_SupplierMaster.STaxRegNo = TxtSTRN.Text;
                Entity_SupplierMaster.ImgTaxRegNoPath = lblSerTaxNopath.Text;
                Entity_SupplierMaster.STaxJurisdiction = "0";
                Entity_SupplierMaster.VATNo = "0";
                Entity_SupplierMaster.TINNo = "0";
                Entity_SupplierMaster.CentralSaleTaxRagNo = "0";
                Entity_SupplierMaster.ExciseRange = "0";
                Entity_SupplierMaster.ExciseDivision = "0";
                Entity_SupplierMaster.ExciseCircle = "0";
                Entity_SupplierMaster.ExciseZone = "0";
                Entity_SupplierMaster.ExciseCollectorate = "0";
                Entity_SupplierMaster.ExciseECCNO = "0";
                Entity_SupplierMaster.TIN_BINNo = "0";
                Entity_SupplierMaster.PANNo = TxtPanNo.Text;
                Entity_SupplierMaster.ImgPanNoPath = lblPanNo.Text;
                Entity_SupplierMaster.TDSCertificate = TxtTDSCertificate.Text;

                Entity_SupplierMaster.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_SupplierMaster.LoginDate = DateTime.Now;
                Entity_SupplierMaster.IsDeleted = false;

                InsertRow = Obj_SupplierMaster.InsertRecord(ref Entity_SupplierMaster, out StrError);

                if (InsertRow > 0)
                {
                    if (ViewState["TermsTable"] != null)
                    {
                        for (int J = 0; J < GridTermCond.Rows.Count; J++)
                        {
                            DMSupplierMaster ODMSM = new DMSupplierMaster();
                            SuplierMaster ENSM = new SuplierMaster();
                            CheckBox GrdSelectAll = (CheckBox)GridTermCond.Rows[J].FindControl("GrdSelectAll");
                            TextBox txtTitle1 = (TextBox)GridTermCond.Rows[J].FindControl("GrtxtTermCondition_Head");
                            TextBox txtDesc = (TextBox)GridTermCond.Rows[J].FindControl("GrtxtDesc");
                            if (GrdSelectAll.Checked == true && txtTitle1.Text != "" && txtDesc.Text != "")
                            {
                                ENSM.SuplierId = InsertRow;
                                ENSM.TermsTitle = txtTitle1.Text;
                                ENSM.TermsDetls = txtDesc.Text;
                                InsertRowDtls = ODMSM.Insert_SupplierTandC(ref ENSM, out StrError);
                            }
                            ODMSM = null;
                            ENSM = null;
                        }
                    }
                    obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                    MakeEmptyForm();
                    Entity_SupplierMaster = null;
                    Obj_SupplierMaster = null;
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }


    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0; int UpdateRowDtls = 0;
        try
        {
            if (ViewState["EditID"] != null)
            {
                Entity_SupplierMaster.SuplierId = Convert.ToInt32(ViewState["EditID"]);
            }
            Entity_SupplierMaster.SuplierCode = TxtSuppCode.Text;
            Entity_SupplierMaster.SuplierName = TxtSuppName.Text.Trim();
            Entity_SupplierMaster.TelNo = TxtTelNo.Text;
            Entity_SupplierMaster.MobileNo = TxtMobile.Text;
            Entity_SupplierMaster.Address = TxtSuppAddress.Text;
            Entity_SupplierMaster.Email = TxtEmail.Text;
            Entity_SupplierMaster.Note = TxtNotes.Text;

            //--------Newly Added Fields---------
            Entity_SupplierMaster.WebSite = TxtWebsite.Text;
            Entity_SupplierMaster.PersonName = TxtPerName.Text;
            Entity_SupplierMaster.PDesignation = TxtDesignation.Text;
            Entity_SupplierMaster.PMobileNo = TxtContPerMobileNo.Text;
            Entity_SupplierMaster.PEmailId = TxtContPerEmail.Text;
            Entity_SupplierMaster.PWebsite = TxtContPerWebsite.Text;
            Entity_SupplierMaster.STaxRegNo = TxtSTRN.Text;
            Entity_SupplierMaster.ImgTaxRegNoPath = lblSerTaxNopath.Text;
            Entity_SupplierMaster.STaxJurisdiction = "0";
            Entity_SupplierMaster.VATNo = "0";
            Entity_SupplierMaster.TINNo = "0";
            Entity_SupplierMaster.StateID = Convert.ToInt32(ddlState.SelectedValue);
            Entity_SupplierMaster.CentralSaleTaxRagNo = "0";
            Entity_SupplierMaster.ExciseRange = "0";
            Entity_SupplierMaster.ExciseDivision = "0";
            Entity_SupplierMaster.ExciseCircle = "0";
            Entity_SupplierMaster.ExciseZone = "0";
            Entity_SupplierMaster.ExciseCollectorate = "0";
            Entity_SupplierMaster.ExciseECCNO = "0";
            Entity_SupplierMaster.TIN_BINNo = "0";
            Entity_SupplierMaster.PANNo = TxtPanNo.Text;
            Entity_SupplierMaster.ImgPanNoPath = lblPanNo.Text;
            Entity_SupplierMaster.TDSCertificate = TxtTDSCertificate.Text;
            Entity_SupplierMaster.StateID = Convert.ToInt32(ddlState.SelectedValue);
            Entity_SupplierMaster.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_SupplierMaster.LoginDate = DateTime.Now;

            UpdateRow = Obj_SupplierMaster.UpdateRecord(ref Entity_SupplierMaster, out StrError);

            if (UpdateRow > 0)
            {
                if (ViewState["TermsTable"] != null)
                {
                    for (int J = 0; J < GridTermCond.Rows.Count; J++)
                    {
                        DMSupplierMaster ODMSM = new DMSupplierMaster();
                        SuplierMaster ENSM = new SuplierMaster();
                        CheckBox GrdSelectAll = (CheckBox)GridTermCond.Rows[J].FindControl("GrdSelectAll");
                        TextBox txtTitle1 = (TextBox)GridTermCond.Rows[J].FindControl("GrtxtTermCondition_Head");
                        TextBox txtDesc = (TextBox)GridTermCond.Rows[J].FindControl("GrtxtDesc");
                        if (GrdSelectAll.Checked == true && txtTitle1.Text != "" && txtDesc.Text != "")
                        {
                            ENSM.SuplierId = Convert.ToInt32(ViewState["EditID"]);
                            ENSM.TermsTitle = txtTitle1.Text;
                            ENSM.TermsDetls = txtDesc.Text;
                            UpdateRowDtls = ODMSM.Insert_SupplierTandC(ref ENSM, out StrError);
                        }
                        ODMSM = null;
                        ENSM = null;
                    }
                }
                obj_Comman.ShowPopUpMsg("Record Updated Successfully", this.Page);
                MakeEmptyForm();
                Entity_SupplierMaster = null;
                Obj_SupplierMaster = null;
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
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
                            ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                            Ds = Obj_SupplierMaster.GetSupplierForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                            {
                                TxtSuppCode.Text = Ds.Tables[0].Rows[0]["SuplierCode"].ToString();
                                TxtSuppName.Text = Ds.Tables[0].Rows[0]["SuplierName"].ToString();
                                TxtSuppAddress.Text = Ds.Tables[0].Rows[0]["Address"].ToString();
                                TxtTelNo.Text = Ds.Tables[0].Rows[0]["TelNo"].ToString();
                                TxtMobile.Text = Ds.Tables[0].Rows[0]["MobileNo"].ToString();
                                TxtEmail.Text = Ds.Tables[0].Rows[0]["Email"].ToString();
                                TxtNotes.Text = Ds.Tables[0].Rows[0]["Note"].ToString();
                                //-------Newly Added Fields-------
                                TxtWebsite.Text = Ds.Tables[0].Rows[0]["WebSite"].ToString();
                                TxtPerName.Text = Ds.Tables[0].Rows[0]["PersonName"].ToString();
                                TxtDesignation.Text = Ds.Tables[0].Rows[0]["PDesignation"].ToString();
                                TxtContPerMobileNo.Text = Ds.Tables[0].Rows[0]["PMobileNo"].ToString();
                                TxtContPerEmail.Text = Ds.Tables[0].Rows[0]["PEmailId"].ToString();
                                TxtContPerWebsite.Text = Ds.Tables[0].Rows[0]["PWebsite"].ToString();
                                TxtSTRN.Text = Ds.Tables[0].Rows[0]["STaxRegNo"].ToString();
                                lblSerTaxNopath.Text = Ds.Tables[0].Rows[0]["ImgTaxRegNoPath"].ToString();
                                ImgTaxRegNoPath.ImageUrl = lblSerTaxNopath.Text;

                                if (!string.IsNullOrEmpty(Ds.Tables[0].Rows[0]["StateID"].ToString()))
                                {
                                    ddlState.SelectedValue = Ds.Tables[0].Rows[0]["StateID"].ToString();
                                }
                                else
                                {
                                    ddlState.SelectedValue = "0";
                                }

                                //TxtSTJ.Text = Ds.Tables[0].Rows[0]["STaxJurisdiction"].ToString();
                                //TxtVatNo.Text = Ds.Tables[0].Rows[0]["VATNo"].ToString();
                                //TxtTINNO.Text = Ds.Tables[0].Rows[0]["TINNo"].ToString();
                                //TxtCSTRN.Text = Ds.Tables[0].Rows[0]["CentralSaleTaxRagNo"].ToString();
                                //TxtExciseRange.Text = Ds.Tables[0].Rows[0]["ExciseRange"].ToString();
                                //TxtExciseDivision.Text = Ds.Tables[0].Rows[0]["ExciseDivision"].ToString();
                                //TxtExciseCircle.Text = Ds.Tables[0].Rows[0]["ExciseCircle"].ToString();
                                //TxtExciseZone.Text = Ds.Tables[0].Rows[0]["ExciseZone"].ToString();
                                //TxtExciseCollectorate.Text = Ds.Tables[0].Rows[0]["ExciseCollectorate"].ToString();
                                //TxtExciseECCNO.Text = Ds.Tables[0].Rows[0]["ExciseECCNO"].ToString();
                                //TxtTINBINNO.Text = Ds.Tables[0].Rows[0]["TIN_BINNo"].ToString();
                                TxtPanNo.Text = Ds.Tables[0].Rows[0]["PANNo"].ToString();
                                lblPanNo.Text = Ds.Tables[0].Rows[0]["ImgPanNoPath"].ToString();
                                ImgPanNoPath.ImageUrl = lblPanNo.Text;

                                TxtTDSCertificate.Text = Ds.Tables[0].Rows[0]["TDSCertificate"].ToString();
                                if (Ds.Tables[1].Rows.Count > 0)
                                {
                                    GridTermCond.DataSource = Ds.Tables[1];
                                    GridTermCond.DataBind();
                                    ViewState["TermsTable"] = Ds.Tables[1];
                                }
                                else
                                {
                                    GridTermCond.DataSource = null;
                                    GridTermCond.DataBind();
                                    SetInitialRow();
                                }
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            Ds = null;
                            Obj_SupplierMaster = null;
                            if(!FlagEdit)
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            if (!FladDel)
                            BtnDelete.Visible = true;
                            TxtSuppCode.Focus();
                        }
                        break;
                    }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        ReportGrid(StrCondition);
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
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
                Entity_SupplierMaster.SuplierId = DeleteId;
                Entity_SupplierMaster.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_SupplierMaster.LoginDate = DateTime.Now;
                Entity_SupplierMaster.IsDeleted = true;

                int iDelete = Obj_SupplierMaster.DeleteRecord(ref Entity_SupplierMaster, out StrError);
                if (iDelete != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_SupplierMaster = null;
            Obj_SupplierMaster = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void lnkSerTax_Click(object sender, EventArgs e)
    {
        try
        {
            //To Delete All Files In Direcotry===========
            string[] filePaths = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Temp/"));
            foreach (string filePath in filePaths)
                System.IO.File.Delete(filePath);
            //To Delete All Files In Direcotry===========
            Random random = new Random();

            if (SerTaxRegNo.HasFile)
            {
                //--Total No of Files--
                Int64 TotalFiles = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Imgupload")).Count();

                string filename = System.IO.Path.GetFileName(SerTaxRegNo.FileName);
                filename = TotalFiles + "-" + filename;
                SerTaxRegNo.SaveAs(Server.MapPath("~/Images/Temp/") + filename);

                //==========USed For Resize Image to Gal Size===================
                System.Drawing.Image GalImage = obj_Comman.ResizeImage(System.Drawing.Image.FromFile(Server.MapPath("~/Images/Temp/") + filename), 200, 200);
                GalImage.Save(Server.MapPath("~/Images/Imgupload/") + filename);
                GalImage = null;
                //==========USed For Resize Image to Gal Size===================   

                lblSerTaxNopath.Text = "~/Images/Imgupload/" + filename;
                //ImgDone.Visible = true;
                ImgTaxRegNoPath.ImageUrl = @"~/Images/Imgupload/" + filename;
                ImgTaxRegNoPath.DataBind();
             //   TxtSTJ.Focus();
            }
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this.Page);
        }
    }

    protected void lnkPanNo_Click(object sender, EventArgs e)
    {
        try
        {
            //To Delete All Files In Direcotry===========
            string[] filePaths = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Temp/"));
            foreach (string filePath in filePaths)
                System.IO.File.Delete(filePath);
            //To Delete All Files In Direcotry===========
            Random random = new Random();

            if (PANCARDUpload.HasFile)
            {
                //--Total No of Files--
                Int64 TotalFiles = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Imgupload")).Count();

                string filename = System.IO.Path.GetFileName(PANCARDUpload.FileName);
                filename = TotalFiles + "-" + filename;
                PANCARDUpload.SaveAs(Server.MapPath("~/Images/Temp/") + filename);

                //==========USed For Resize Image to Gal Size===================
                System.Drawing.Image GalImage = obj_Comman.ResizeImage(System.Drawing.Image.FromFile(Server.MapPath("~/Images/Temp/") + filename), 200, 200);
                GalImage.Save(Server.MapPath("~/Images/Imgupload/") + filename);
                GalImage = null;
                //==========USed For Resize Image to Gal Size===================   

                lblPanNo.Text = "~/Images/Imgupload/" + filename;
                //ImgDone.Visible = true;
                ImgPanNoPath.ImageUrl = @"~/Images/Imgupload/" + filename;
                ImgPanNoPath.DataBind();
                TxtTDSCertificate.Focus();
            }
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this.Page);
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
                    dtCurrentTableTACForVIEW.Rows.Add(drCurrentRow);
                    ViewState["TermsTable"] = dtCurrentTableTACForVIEW;
                    GridTermCond.DataSource = dtCurrentTableTACForVIEW;
                    GridTermCond.DataBind();
                }
            }

            else
            {
               obj_Comman.ShowPopUpMsg("Page Not Proper Load From Server...",this.Page);

            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
}
