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

using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DALSQLHelper;
using MayurInventory.DataModel;


public partial class Masters_CompanyMaster : System.Web.UI.Page
{
    #region[Variables]
    DMCompanyMaster Obj_CM = new DMCompanyMaster();
    CompanyMaster Entity_CM = new CompanyMaster();
    CommanFunction obj_Comm = new CommanFunction();
    DataSet DS = new DataSet();
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    private static bool FlagAdd, FlagDel, FlagEdit = false;
    #endregion

    #region[UserDefine Function]

    private void MakeEmptyForm()
    {
        TxtCompanyName.Focus();
        if (!FlagAdd)
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnDelete.Visible = false;
        TxtCompanyName.Text = string.Empty;
        Txtabbreviations.Text = string.Empty;
        TxtAddress.Text = string.Empty;
        TxtPhoneNo.Text = string.Empty;
        TxtEmail.Text = string.Empty;
        TxtWebsite.Text = string.Empty;
        TxtFaxNo.Text = string.Empty;
        TxtTinNo.Text = string.Empty;
        TxtVatNo.Text = string.Empty;
        TxtServiceTaxNo.Text = string.Empty;
        TxtNoteC.Text = string.Empty;
        lblLogopath.Text = "";
        LblSignPath.Text = "";
        LblSignPath1.Text = "";
        LblSignPath2.Text = "";
        TxtSearch.Text = string.Empty;
        ImgCompanyLogo.ImageUrl = "";
        ImgSign.ImageUrl = "";
        ImgSign1.ImageUrl = "";
        ImgSign2.ImageUrl = "";

        SetInitialRow();
        ReportGrid(StrCondition);
    }

    private void MakeControlEmpty()
    {
        ddlBankName.SelectedValue = "0";
        TxtAccntNo.Text = "";
        TxtNoteB.Text = "";

        ViewState["GridIndex"] = null;
        ViewState["GridDetails"] = null;
        ImgAddGrid.ImageUrl = "~/Images/Icon/Gridadd.png";
        ImgAddGrid.ToolTip = "Add Grid";
       
        ddlBankName.Focus();
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("#", typeof(Int32));
        dt.Columns.Add("BankId", typeof(Int32));
        dt.Columns.Add("BankName", typeof(string));
        dt.Columns.Add("AccountNo", typeof(string));
        dt.Columns.Add("NoteB", typeof(string));

        dr = dt.NewRow();

        dr["#"] = 0;
        dr["BankId"] = 0;
        dr["BankName"] = "";
        dr["AccountNo"] = "";
        dr["NoteB"] = "";

        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        GridDetails.DataSource = dt;
        GridDetails.DataBind();
    }

    private void FillCombo()
    {
        try
        {
            DS = Obj_CM.BindCombo(out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlBankName.DataSource = DS.Tables[0];
                    ddlBankName.DataTextField = "BankName";
                    ddlBankName.DataValueField = "BankId";
                    ddlBankName.DataBind();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }

    public void ReportGrid(string RepCondition)
    {
        try
        {
            DS = Obj_CM.GetCompanyDtls(RepCondition, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GrdReport.DataSource = DS.Tables[0];
                GrdReport.DataBind();
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }
            Obj_CM = null;
            DS = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private bool ChkDetails()
    {
        bool flag = false;
        try
        {
            if (GridDetails.Rows.Count > 0 && !String.IsNullOrEmpty(GridDetails.Rows[0].Cells[3].Text) && !GridDetails.Rows[0].Cells[3].Text.Equals("&nbsp;"))
            {
                flag = true;
            }
            flag = true;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
        return flag;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CheckUserRight();
            FillCombo();
            MakeControlEmpty();
            MakeEmptyForm();
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMCompanyMaster Obj_CM = new DMCompanyMaster();
        String[] SearchList = Obj_CM.GetSuggestRecord(prefixText);
        return SearchList;
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeControlEmpty();
        MakeEmptyForm();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDtls = 0;
        try
        {
            if (ChkDetails() == true)
            {
                DS = Obj_CM.ChkDuplicate(TxtCompanyName.Text.Trim(), out StrError);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    obj_Comm.ShowPopUpMsg("Record is Already Present..", this.Page);
                    TxtCompanyName.Focus();
                }
                else
                {
                    Entity_CM.CompanyName = TxtCompanyName.Text;
                    Entity_CM.abbreviation = Txtabbreviations.Text;
                    Entity_CM.CAddress = TxtAddress.Text;
                    if (!string.IsNullOrEmpty(lblLogopath.Text))
                    {
                        Entity_CM.CLogo = lblLogopath.Text;
                    }
                    else
                    {
                        Entity_CM.CLogo = "";                        
                    }
                    Entity_CM.PhoneNo = TxtPhoneNo.Text.Trim();
                    Entity_CM.EmailId = TxtEmail.Text.Trim();
                    Entity_CM.Website = TxtWebsite.Text.Trim();
                    Entity_CM.FaxNo = TxtFaxNo.Text.Trim();
                    Entity_CM.TinNo = TxtTinNo.Text.Trim();
                    Entity_CM.VatNo = TxtVatNo.Text.Trim();
                    Entity_CM.ServiceTaxNo = TxtServiceTaxNo.Text.Trim();
                    if (!string.IsNullOrEmpty(LblSignPath.Text))
                    {
                        Entity_CM.DigitalSignature = LblSignPath.Text;
                    }
                    else
                    {
                        Entity_CM.DigitalSignature ="";
                    }

                    if (!string.IsNullOrEmpty(LblSignPath1.Text))
                    {
                        Entity_CM.DigitalSignature1 = LblSignPath1.Text;
                    }
                    else
                    {
                        Entity_CM.DigitalSignature1 = "";
                    }

                    if (!string.IsNullOrEmpty(LblSignPath2.Text))
                    {
                        Entity_CM.DigitalSignature2 = LblSignPath2.Text;
                    }
                    else
                    {
                        Entity_CM.DigitalSignature2 = "";
                    }
                    Entity_CM.Note = TxtNoteC.Text.Trim();
                    Entity_CM.UserId = Convert.ToInt32(Session["UserId"]);
                    Entity_CM.LoginDate = DateTime.Now;

                    InsertRow = Obj_CM.InsertRecord(ref Entity_CM, out StrError);

                    if (InsertRow > 0)
                    {
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtInsert = new DataTable();
                            dtInsert = (DataTable)ViewState["CurrentTable"];
                            for (int i = 0; i < dtInsert.Rows.Count; i++)
                            {
                                Entity_CM.CompanyId = InsertRow;
                                Entity_CM.BankId = Convert.ToInt32(dtInsert.Rows[i]["BankId"].ToString());
                                Entity_CM.AccountNo = dtInsert.Rows[i]["AccountNo"].ToString();
                                Entity_CM.NoteB = dtInsert.Rows[i]["NoteB"].ToString();
                                InsertRowDtls = Obj_CM.InsertDetailsRecord(ref Entity_CM, out StrError);
                            }
                        }
                        if (InsertRow > 0)
                        {
                            obj_Comm.ShowPopUpMsg("Record Saved Successfully", this.Page);
                            MakeControlEmpty();
                            MakeEmptyForm();
                            Entity_CM = null;
                            Obj_CM = null;
                        }
                    }
                }
            }
            else
            {
                obj_Comm.ShowPopUpMsg("Please Enter Details ..!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        
        int UpdateRow = 0, InsertRowDtls = 0;
        try
        {
            if (ChkDetails() == true)
            {
                
                    if (ViewState["EditID"] != null)
                    {
                        Entity_CM.CompanyId = Convert.ToInt32(ViewState["EditID"]);
                    }
                    Entity_CM.CompanyName = TxtCompanyName.Text;
                    Entity_CM.abbreviation = Txtabbreviations.Text;
                    Entity_CM.CAddress = TxtAddress.Text;
                    if (!string.IsNullOrEmpty(lblLogopath.Text))
                    {
                        Entity_CM.CLogo = lblLogopath.Text;
                    }
                    else
                    {
                        Entity_CM.CLogo = "";
                    }
                    Entity_CM.PhoneNo = TxtPhoneNo.Text.Trim();
                    Entity_CM.EmailId = TxtEmail.Text.Trim();
                    Entity_CM.Website = TxtWebsite.Text.Trim();
                    Entity_CM.FaxNo = TxtFaxNo.Text.Trim();
                    Entity_CM.TinNo = TxtTinNo.Text.Trim();
                    Entity_CM.VatNo = TxtVatNo.Text.Trim();
                    Entity_CM.ServiceTaxNo = TxtServiceTaxNo.Text.Trim();
                    if (!string.IsNullOrEmpty(LblSignPath.Text))
                    {
                        Entity_CM.DigitalSignature = LblSignPath.Text;
                    }
                    else
                    {
                        Entity_CM.DigitalSignature = "";
                    }

                    if (!string.IsNullOrEmpty(LblSignPath1.Text))
                    {
                        Entity_CM.DigitalSignature1 = LblSignPath1.Text;
                    }
                    else
                    {
                        Entity_CM.DigitalSignature1 = "";
                    }

                    if (!string.IsNullOrEmpty(LblSignPath2.Text))
                    {
                        Entity_CM.DigitalSignature2 = LblSignPath2.Text;
                    }
                    else
                    {
                        Entity_CM.DigitalSignature2 = "";
                    }
                    Entity_CM.Note = TxtNoteC.Text.Trim();
                    Entity_CM.UserId = Convert.ToInt32(Session["UserId"]);
                    Entity_CM.LoginDate = DateTime.Now;

                    UpdateRow = Obj_CM.UpdateRecord(ref Entity_CM, out StrError);

                    if (UpdateRow > 0)
                    {
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtInsert = new DataTable();
                            dtInsert = (DataTable)ViewState["CurrentTable"];
                            for (int i = 0; i < dtInsert.Rows.Count; i++)
                            {
                                Entity_CM.BankId = Convert.ToInt32(dtInsert.Rows[i]["BankId"].ToString());
                                Entity_CM.AccountNo = dtInsert.Rows[i]["AccountNo"].ToString();
                                Entity_CM.NoteB = dtInsert.Rows[i]["NoteB"].ToString();
                                InsertRowDtls = Obj_CM.InsertDetailsRecord(ref Entity_CM, out StrError);
                            }
                        }
                        if (UpdateRow > 0)
                        {
                            obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                            MakeControlEmpty();
                            MakeEmptyForm();
                            Entity_CM = null;
                            Obj_CM = null;
                        }
                    }
                
            }
            else
            {
                obj_Comm.ShowPopUpMsg("Please Enter Details ..!", this.Page);
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
        ReportGrid(StrCondition);
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
                    if (dtCurrentTable.Rows.Count == 1 && string.IsNullOrEmpty(dtCurrentTable.Rows[0]["BankName"].ToString()))
                    {
                        dtCurrentTable.Rows.RemoveAt(0);
                    }
                    if (ViewState["GridIndex"] != null)
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dtCurrentTable.Rows[i]["BankId"]) == Convert.ToInt32(ddlBankName.SelectedValue))
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            dtCurrentTable.Rows[k]["BankName"] = ddlBankName.SelectedItem;
                            dtCurrentTable.Rows[k]["BankId"] = Convert.ToInt32(ddlBankName.SelectedValue);
                            dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(ddlBankName.SelectedValue);
                            dtCurrentTable.Rows[k]["AccountNo"] = TxtAccntNo.Text;
                            dtCurrentTable.Rows[k]["NoteB"] = TxtNoteB.Text;

                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            MakeControlEmpty();

                        }

                        else
                        {
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                            dtTableRow["BankName"] = ddlBankName.SelectedItem;
                            dtTableRow["BankId"] = Convert.ToInt32(ddlBankName.SelectedValue);
                            dtTableRow["#"] = Convert.ToInt32(ddlBankName.SelectedValue);
                            dtTableRow["AccountNo"] = TxtAccntNo.Text;
                            dtTableRow["NoteB"] = TxtNoteB.Text;

                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            MakeControlEmpty();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dtCurrentTable.Rows[i]["BankId"]) == Convert.ToInt32(ddlBankName.SelectedValue))
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            dtCurrentTable.Rows[k]["BankName"] = ddlBankName.SelectedItem;
                            dtCurrentTable.Rows[k]["BankId"] = Convert.ToInt32(ddlBankName.SelectedValue);
                            dtCurrentTable.Rows[k]["#"] = Convert.ToInt32(ddlBankName.SelectedValue);
                            dtCurrentTable.Rows[k]["AccountNo"] = TxtAccntNo.Text;
                            dtCurrentTable.Rows[k]["NoteB"] = TxtNoteB.Text;

                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            MakeControlEmpty();

                        }

                        else
                        {
                            dtTableRow = dtCurrentTable.NewRow();
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                            dtTableRow["BankName"] = ddlBankName.SelectedItem;
                            dtTableRow["BankId"] = Convert.ToInt32(ddlBankName.SelectedValue);
                            dtTableRow["#"] = Convert.ToInt32(ddlBankName.SelectedValue);
                            dtTableRow["AccountNo"] = TxtAccntNo.Text;
                            dtTableRow["NoteB"] = TxtNoteB.Text;

                            dtCurrentTable.Rows.Add(dtTableRow);

                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            MakeControlEmpty();
                        }
                    }
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        int DeleteId = 0;
        try
        {
            if (ViewState["EditID"] != null)
            {
                DeleteId = Convert.ToInt32(ViewState["EditID"]);
            }
            if (DeleteId != 0)
            {
                Entity_CM.CompanyId = DeleteId;
                Entity_CM.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_CM.LoginDate = DateTime.Now;

                int iDelete = Obj_CM.DeleteRecord(ref Entity_CM, out StrError);
                if (iDelete != 0)
                {
                    obj_Comm.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_CM = null;
            Obj_CM = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Index;
            if (e.CommandName == "SelectGrid")
            {
                if ((!(string.IsNullOrEmpty(GridDetails.Rows[0].Cells[2].Text))) && (GridDetails.Rows[0].Cells[2].Text.Equals("&nbsp;")))
                {
                    obj_Comm.ShowPopUpMsg("There Is No Record To Edit", this.Page);
                }
                else
                {
                    ImgAddGrid.ImageUrl = "~/Images/Icon/GridUpdate.png";
                    ImgAddGrid.ToolTip = "Update";

                    Index = Convert.ToInt32(e.CommandArgument);

                    ViewState["GridIndex"] = Index;
                    ddlBankName.SelectedValue = GridDetails.Rows[Index].Cells[2].Text;
                    TxtAccntNo.Text = GridDetails.Rows[Index].Cells[4].Text;
                    TxtNoteB.Text = GridDetails.Rows[Index].Cells[5].Text;
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void GridDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = Convert.ToInt32(hiddenbox.Value);
            if (i == 1)
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
                        SetInitialRow();
                    }
                    MakeControlEmpty();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void GrdReport_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
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
                                DS = Obj_CM.GetCompanyForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                                if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                                {
                                    TxtCompanyName.Text = DS.Tables[0].Rows[0]["CompanyName"].ToString();
                                    Txtabbreviations.Text = DS.Tables[0].Rows[0]["abbreviation"].ToString();
                                    TxtAddress.Text = DS.Tables[0].Rows[0]["CAddress"].ToString();
                                    lblLogopath.Text = DS.Tables[0].Rows[0]["CLogo"].ToString();
                                    ImgCompanyLogo.ImageUrl = lblLogopath.Text;
                                    TxtPhoneNo.Text = DS.Tables[0].Rows[0]["PhoneNo"].ToString();
                                    TxtEmail.Text = DS.Tables[0].Rows[0]["EmailId"].ToString();
                                    TxtWebsite.Text = DS.Tables[0].Rows[0]["Website"].ToString();
                                    TxtFaxNo.Text = DS.Tables[0].Rows[0]["FaxNo"].ToString();
                                    TxtTinNo.Text = DS.Tables[0].Rows[0]["TinNo"].ToString();
                                    TxtVatNo.Text = DS.Tables[0].Rows[0]["VatNo"].ToString();
                                    TxtServiceTaxNo.Text = DS.Tables[0].Rows[0]["ServiceTaxNo"].ToString();
                                    LblSignPath.Text = DS.Tables[0].Rows[0]["DigitalSignature"].ToString();
                                    LblSignPath1.Text = DS.Tables[0].Rows[0]["DigitalSignature1"].ToString();
                                    LblSignPath2.Text = DS.Tables[0].Rows[0]["DigitalSignature2"].ToString();
                                    ImgSign.ImageUrl = LblSignPath.Text;
                                    ImgSign1.ImageUrl = LblSignPath1.Text;
                                    ImgSign2.ImageUrl = LblSignPath2.Text;
                                    TxtNoteC.Text = DS.Tables[0].Rows[0]["Note"].ToString();
                                }
                                else
                                {
                                    MakeEmptyForm();
                                }
                                if (DS.Tables[1].Rows.Count > 0)
                                {
                                    GridDetails.DataSource = DS.Tables[1];
                                    GridDetails.DataBind();
                                    ViewState["CurrentTable"] = DS.Tables[1];
                                }
                                else
                                {
                                    SetInitialRow();
                                }
                                DS = null;
                                Obj_CM = null;
                                if (!FlagEdit)
                                BtnUpdate.Visible = true;
                                BtnSave.Visible = false;
                                if (!FlagDel)
                                BtnDelete.Visible = true;
                                TxtCompanyName.Focus();
                                MakeControlEmpty();
                            }
                            break;
                        }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void lnkCompany_Click(object sender, EventArgs e)
    {
        try
        {
            //To Delete All Files In Direcotry===========
            string[] filePaths = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Temp/"));
            foreach (string filePath in filePaths)
                System.IO.File.Delete(filePath);
            //To Delete All Files In Direcotry===========
            Random random = new Random();

            if (CompanyLogoUpload.HasFile)
            {
                //--Total No of Files--
                Int64 TotalFiles = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Imgupload")).Count();

                string filename = System.IO.Path.GetFileName(CompanyLogoUpload.FileName);
                filename = TotalFiles + "-" + filename;
                CompanyLogoUpload.SaveAs(Server.MapPath("~/Images/Temp/") + filename);

                //==========USed For Resize Image to Gal Size===================
                System.Drawing.Image GalImage = obj_Comm.ResizeImage(System.Drawing.Image.FromFile(Server.MapPath("~/Images/Temp/") + filename), 200, 200);
                GalImage.Save(Server.MapPath("~/Images/Imgupload/") + filename);
                GalImage = null;
                //==========USed For Resize Image to Gal Size===================   


                lblLogopath.Text = "~/Images/Imgupload/" + filename;
                //ImgDone.Visible = true;
                ImgCompanyLogo.ImageUrl = @"~/Images/Imgupload/" + filename;
                ImgCompanyLogo.DataBind();
             }
        }
        catch (Exception ex)
        {
            obj_Comm.ShowPopUpMsg("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this.Page);
        }
    }

    protected void lnkCompanyCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ImgCompanyLogo.ImageUrl = "";
            lblLogopath.Text = "";
           
        }
        catch (Exception ex)
        {
            obj_Comm.ShowPopUpMsg("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this.Page);
        }

    }

    protected void lnkSign_Click(object sender, EventArgs e)
    {
        try
        {
            //To Delete All Files In Direcotry===========
            string[] filePaths = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Temp/"));
            foreach (string filePath in filePaths)
                System.IO.File.Delete(filePath);
            //To Delete All Files In Direcotry===========
            Random random = new Random();

            if (DigitalSignUpload.HasFile)
            {
                //--Total No of Files--
                Int64 TotalFiles = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Imgupload")).Count();

                string filename = System.IO.Path.GetFileName(DigitalSignUpload.FileName);
                filename = TotalFiles + "-" + filename;
                DigitalSignUpload.SaveAs(Server.MapPath("~/Images/Temp/") + filename);

                //==========USed For Resize Image to Gal Size===================
                System.Drawing.Image GalImage = obj_Comm.ResizeImage(System.Drawing.Image.FromFile(Server.MapPath("~/Images/Temp/") + filename), 200, 200);
                GalImage.Save(Server.MapPath("~/Images/Imgupload/") + filename);
                GalImage = null;
                //==========USed For Resize Image to Gal Size===================   

              
                //==========USed For Resize Image to Thumb===================
                LblSignPath.Text = "~/Images/Imgupload/" + filename;
                //ImgDone.Visible = true;
                ImgSign.ImageUrl = @"~/Images/Imgupload/" + filename;
                ImgSign.DataBind();
            }
        }
        catch (Exception ex)
        {
            obj_Comm.ShowPopUpMsg("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this.Page);
        }
    }
    protected void lnkSign1_Click(object sender, EventArgs e)
    {
        try
        {
            //To Delete All Files In Direcotry===========
            string[] filePaths = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Temp/"));
            foreach (string filePath in filePaths)
                System.IO.File.Delete(filePath);
            //To Delete All Files In Direcotry===========
            Random random = new Random();

            if (DigitalSignUpload2.HasFile)
            {
                //--Total No of Files--
                Int64 TotalFiles = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Imgupload")).Count();

                string filename = System.IO.Path.GetFileName(DigitalSignUpload2.FileName);
                filename = TotalFiles + "-" + filename;
                DigitalSignUpload2.SaveAs(Server.MapPath("~/Images/Temp/") + filename);

                //==========USed For Resize Image to Gal Size===================
                System.Drawing.Image GalImage = obj_Comm.ResizeImage(System.Drawing.Image.FromFile(Server.MapPath("~/Images/Temp/") + filename), 200, 200);
                GalImage.Save(Server.MapPath("~/Images/Imgupload/") + filename);
                GalImage = null;
                //==========USed For Resize Image to Gal Size===================   


                //==========USed For Resize Image to Thumb===================
                LblSignPath1.Text = "~/Images/Imgupload/" + filename;
                //ImgDone.Visible = true;
                ImgSign1.ImageUrl = @"~/Images/Imgupload/" + filename;
                ImgSign1.DataBind();
            }
        }
        catch (Exception ex)
        {
            obj_Comm.ShowPopUpMsg("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this.Page);
        }
    }
    protected void lnkSign2_Click(object sender, EventArgs e)
    {
        try
        {
            //To Delete All Files In Direcotry===========
            string[] filePaths = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Temp/"));
            foreach (string filePath in filePaths)
                System.IO.File.Delete(filePath);
            //To Delete All Files In Direcotry===========
            Random random = new Random();

            if (DigitalSignUpload3.HasFile)
            {
                //--Total No of Files--
                Int64 TotalFiles = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Imgupload")).Count();

                string filename = System.IO.Path.GetFileName(DigitalSignUpload3.FileName);
                filename = TotalFiles + "-" + filename;
                DigitalSignUpload3.SaveAs(Server.MapPath("~/Images/Temp/") + filename);

                //==========USed For Resize Image to Gal Size===================
                System.Drawing.Image GalImage = obj_Comm.ResizeImage(System.Drawing.Image.FromFile(Server.MapPath("~/Images/Temp/") + filename), 200, 200);
                GalImage.Save(Server.MapPath("~/Images/Imgupload/") + filename);
                GalImage = null;
                //==========USed For Resize Image to Gal Size===================   


                //==========USed For Resize Image to Thumb===================
                LblSignPath2.Text = "~/Images/Imgupload/" + filename;
                //ImgDone.Visible = true;
                ImgSign2.ImageUrl = @"~/Images/Imgupload/" + filename;
                ImgSign2.DataBind();
            }
        }
        catch (Exception ex)
        {
            obj_Comm.ShowPopUpMsg("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this.Page);
        }
    }
    protected void lnkCancle1_Click(object sender, EventArgs e)
    {
        try
        {
            ImgSign.ImageUrl = "";
            LblSignPath.Text = "";
        }
        catch (Exception ex)
        {
            obj_Comm.ShowPopUpMsg("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this.Page);
        }
    }
    protected void lnkCancle2_Click(object sender, EventArgs e)
    {
        try
        {
            ImgSign1.ImageUrl = "";
            LblSignPath1.Text = "";
        }
        catch (Exception ex)
        {
            obj_Comm.ShowPopUpMsg("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this.Page);
        }

    }
    protected void lnkCancle3_Click(object sender, EventArgs e)
    {
        try
        {
            ImgSign2.ImageUrl = "";
            LblSignPath2.Text = "";
        }
        catch (Exception ex)
        {
            obj_Comm.ShowPopUpMsg("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this.Page);
        }

    }
   
}
