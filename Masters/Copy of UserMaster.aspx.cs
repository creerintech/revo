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

using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using MayurInventory.DALSQLHelper;

public partial class Masters_UserMaster : System.Web.UI.Page
{
    #region [Private Varialbles]
    DMUserMaster Obj_UserMaster = new DMUserMaster();
    UserMaster Entity_UserMaster = new UserMaster();
    CommanFunction Obj_Comm = new CommanFunction();
    DataSet DS = new DataSet();
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    private int flag = 0;
    private static bool FlagAdd = false, FlagDel = false, FlagEdit = false;
    #endregion

    #region[UserDefine Function]

    private bool CHECKVALIDATION()
    {
        bool status = false;
        if (!String.IsNullOrEmpty(TxtUserName.Text) && !String.IsNullOrEmpty(TxtUserId.Text) && !String.IsNullOrEmpty(TxtPasswrod.Text))
        {
            for (int k = 0; k < ChkSite.Items.Count; k++)
            {
                if (ChkSite.Items[k].Selected == true)
                {
                    status = true;
                }
            }
        }
        else
        {
            status = false;
        }
        return status;
    }

    private void MakeEmptyForm()
    {
        TxtUserName.Focus();
        if(!FlagAdd)
         BtnSave.Visible = true;
        BtnDelete.Visible = false;
        BtnUpdate.Visible = false;
        TxtUserName.Text = string.Empty;
        TxtUserId.Text = string.Empty;
        TxtSearch.Text = string.Empty;
        //DDLCafe.SelectedIndex = 0;
        TxtPasswrod.Attributes["value"] = "";
        TxtConfrmPasswrd.Attributes["value"] = "";
        TxtPasswrod.Text = string.Empty;
        TxtMailId.Text = string.Empty;
        TxtConfrmPasswrd.Text = string.Empty;
        CHKYESEDITPO.Checked = false;
        CHKYESUNITCONVERSION.Checked = false;
        TXTPASSEDITPO.Text = "";

        FillGrid();
        ReportGrid(StrCondition);
        for (int i = 0; i < ChkSite.Items.Count; i++)
        {
            ChkSite.Items[i].Selected = false;
            
        }
    }
    public void FillGrid()
    {
        try
        {
            DS = Obj_UserMaster.GetUserRightTable(out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GridUserRight.DataSource = DS.Tables[0];
                GridUserRight.DataBind();

                GridUser.DataSource = DS.Tables[1];
                GridUser.DataBind();
            }
            else
            {
                GridUserRight.DataSource = null;
                GridUserRight.DataBind();

                GridUser.DataSource = null;
                GridUser.DataBind();
            }
            //Obj_User = null;
           // DS = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public void ReportGrid(string RepCondition)
    {
        try
        {
            DS = Obj_UserMaster.GetUserDetails(RepCondition, out StrError);
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
            Obj_UserMaster = null;
            DS = null;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void FillCombo()
    {
        try
        {
            DS = Obj_UserMaster.GetCafe(out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                //DDLCafe.DataSource = DS.Tables[0];
                //DDLCafe.DataTextField = "Cafeteria";
                //DDLCafe.DataValueField = "CafeteriaId";                
                //DDLCafe.DataBind();
                //DDLCafe.SelectedIndex = 0;
                ChkSite.DataSource = DS.Tables[1];
                ChkSite.DataValueField = "CafeteriaId";
                ChkSite.DataTextField = "Cafeteria";
                ChkSite.DataBind();
                ViewState["IsCentral"] = DS.Tables[1];
            }
            else
            {                  
            }
           // Obj_UserMaster = null;
          //DS = null;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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
                if (!Session["UserRole"].Equals("Administrator"))
                {
                    //Checking Right of users=======

                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='User Master'");
                    if (dtRow.Length > 0)
                    {
                        DataTable dt = dtRow.CopyToDataTable();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        Response.Redirect("~/Masters/NotAuthUser.aspx");
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
                    //Checking Print Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {

                    }
                    //Edit /Delete Column Visible ========
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        BtnDelete.Visible = false;
                        BtnUpdate.Visible = false;
                        FlagDel = true;
                        FlagEdit = true;
                    }
                    else
                    {
                        //Checking Delete Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                        {
                            BtnDelete.Visible = false;
                            FlagDel = true;
                        }

                        //Checking Edit Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                        {
                            BtnUpdate.Visible = false;
                            FlagEdit = true;
                        }
                    }
                    dsChkUserRight.Dispose();
                }
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
       // LocationMatch = 0;
        TXTPASSEDITPO.Enabled = false;
        TxtPasswrod.Attributes["value"] = TxtPasswrod.Text;
        TxtConfrmPasswrd.Attributes["value"] = TxtConfrmPasswrd.Text;
        AccordionPane2.HeaderContainer.Width = AccordionPane1.HeaderContainer.Width = 680;
        
        if (!Page.IsPostBack)
        {
            FillCombo();
            CheckUserRight();
            MakeEmptyForm();
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0,InsertDetail=0,InsertChkDetail=0;
        try
        {
            if (CHECKVALIDATION() == true)
            {
                DS = Obj_UserMaster.ChkDuplicate(TxtUserName.Text.Trim(), out StrError);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Obj_Comm.ShowPopUpMsg("Please Enter Another Name !!!", this.Page);
                    TxtUserName.Focus();
                }
                else
                {
                    Entity_UserMaster.UserName = TxtUserName.Text;
                    Entity_UserMaster.EmailId = TxtMailId.Text;
                    Entity_UserMaster.LoginName = TxtUserId.Text;
                    // Entity_UserMaster.CafeteriaId=Convert.ToInt32(DDLCafe.SelectedValue);
                    if (RadioIsAdmin.Text.Equals("T"))
                    {
                        Entity_UserMaster.IsAdmin = true;
                        Entity_UserMaster.UserType = "Admin";
                    }
                    if (RadioIsAdmin.Text.Equals("F"))
                    {
                        Entity_UserMaster.IsAdmin = false;
                        Entity_UserMaster.UserType = "User";
                    }

                    Entity_UserMaster.LUserID = Convert.ToInt32(Session["UserID"]);
                    Entity_UserMaster.LoginDate = DateTime.Now;
                    Entity_UserMaster.IsDeleted = false;

                    Entity_UserMaster.Password = TxtPasswrod.Text;
                    InsertRow = Obj_UserMaster.InsertUserDetails(ref Entity_UserMaster, out StrError);
                    if (InsertRow != 0)
                    {
                        int ID = InsertRow;
                        for (int i = 0; i < GridUserRight.Rows.Count; i++)
                        {
                            Label LblFormName = (Label)GridUserRight.Rows[i].Cells[3].FindControl("LblFormName");
                            CheckBox GrdAddRight = (CheckBox)GridUserRight.Rows[i].FindControl("GrdAddRight");
                            CheckBox GrdViewRight = (CheckBox)GridUserRight.Rows[i].FindControl("GrdViewRight");
                            CheckBox GrdEditRight = (CheckBox)GridUserRight.Rows[i].FindControl("GrdEditRight");
                            CheckBox GrdDelRight = (CheckBox)GridUserRight.Rows[i].FindControl("GrdDelRight");
                            CheckBox GrdPrintRight = (CheckBox)GridUserRight.Rows[i].FindControl("GrdPrintRight");

                            Entity_UserMaster.FkUserId = ID;
                            Entity_UserMaster.FormCaption = LblFormName.Text.Trim();
                            Entity_UserMaster.ViewAuth = GrdViewRight.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);
                            Entity_UserMaster.AddAuth = GrdAddRight.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);
                            Entity_UserMaster.DelAuth = GrdDelRight.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);
                            Entity_UserMaster.EditAuth = GrdEditRight.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);
                            Entity_UserMaster.PrintAuth = GrdPrintRight.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);

                            InsertRow = Obj_UserMaster.InsertUserAuthDetails(ref Entity_UserMaster, out StrError);
                        }
                        for (int i = 0; i < GridUser.Rows.Count; i++)
                        {
                            Label LblFormName = (Label)GridUser.Rows[i].Cells[3].FindControl("LblFormName");
                            CheckBox GrdUserAllHeaderAll = (CheckBox)GridUser.Rows[i].Cells[1].FindControl("GrdUserAllHeaderAll");

                            Entity_UserMaster.FkUserId = ID;
                            Entity_UserMaster.FormCaption = LblFormName.Text.Trim();
                            Entity_UserMaster.Email = GrdUserAllHeaderAll.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);

                            InsertDetail = Obj_UserMaster.InsertUserEmailDetail(ref Entity_UserMaster, out StrError);

                        }

                        for (int i = 0; i < ChkSite.Items.Count; i++)
                        {
                            Entity_UserMaster.FkUserId = ID;
                            Entity_UserMaster.CafeteriaId = Convert.ToInt32(ChkSite.Items[i].Value);
                            if (ChkSite.Items[i].Selected)
                            {

                                Entity_UserMaster.Cheked = true;
                            }
                            else
                            {
                                Entity_UserMaster.Cheked = false;
                            }
                            InsertChkDetail = Obj_UserMaster.InsertUserSiteDetail(ref Entity_UserMaster, out StrError);

                        }

                        #region [FOR SETTING ]---------------
                        if (CHKYESEDITPO.Checked == true)
                        {
                            InsertChkDetail = Obj_UserMaster.InsertUserPermissionForEditPO(ID, "Edit Authorised Purchase Order",1,TXTPASSEDITPO.Text.Trim(), out StrError);
                        }
                        if (CHKYESUNITCONVERSION.Checked == true)
                        {
                            InsertChkDetail = Obj_UserMaster.InsertUserPermissionForEditPO(ID, "Unit Conversion In Item Master", 1, TXTPASSEDITPO.Text.Trim(), out StrError);
                        }
                        if (CHKESCESSREPORT.Checked == true)
                        {
                            InsertChkDetail = Obj_UserMaster.InsertUserPermissionForEditPO(Convert.ToInt32(ViewState["EditID"]), "Purchase Order Shortage/Excess Report", 1, TXTEXCESSREPORT.Text.Trim(), out StrError);
                        }
                        #endregion---------------------------
                        if (InsertRow != 0)
                        {
                            Obj_Comm.ShowPopUpMsg("Record Saved Successfully!!", this.Page);
                            MakeEmptyForm();
                            Entity_UserMaster = null;
                            Obj_Comm = null;
                        }
                    }

                }
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("Please Fill Data For Save Record....Site Must Be Select..", this.Page);
            }
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }

    }
    
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, InsertDetail = 0, InsertChkDetail=0;

        try
        {
            if (ViewState["EditID"] != null)
            {
                Entity_UserMaster.UserID = Convert.ToInt32(ViewState["EditID"]);
            }
            Entity_UserMaster.UserName = TxtUserName.Text.Trim();
            Entity_UserMaster.LoginName = TxtUserId.Text.Trim();
            Entity_UserMaster.EmailId = TxtMailId.Text.Trim();
            Entity_UserMaster.Password = TxtPasswrod.Text.Trim();
          //  Entity_UserMaster.CafeteriaId = Convert.ToInt32(DDLCafe.SelectedValue);
            UpdateRow = Obj_UserMaster.UpdateUserDetails(ref Entity_UserMaster, out StrError);

            if (RadioIsAdmin.Text.Equals("T"))
            {
                Entity_UserMaster.IsAdmin = true;
                Entity_UserMaster.UserType = "Admin";
            }
            if (RadioIsAdmin.Text.Equals("F"))
            {
                Entity_UserMaster.IsAdmin = false;
                Entity_UserMaster.UserType = "User";
            }
            Entity_UserMaster.LUserID = Convert.ToInt32(Session["UserId"]);
            Entity_UserMaster.LoginDate = DateTime.Now;

            UpdateRow = Obj_UserMaster.UpdateUserDetails(ref Entity_UserMaster, out StrError);
            if (UpdateRow != 0)
            {
                for (int i = 0; i < GridUserRight.Rows.Count; i++)
                {
                    Label LblFormName = (Label)GridUserRight.Rows[i].FindControl("LblFormName");
                    CheckBox GrdAddRight = (CheckBox)GridUserRight.Rows[i].FindControl("GrdAddRight");
                    CheckBox GrdViewRight = (CheckBox)GridUserRight.Rows[i].FindControl("GrdViewRight");
                    CheckBox GrdEditRight = (CheckBox)GridUserRight.Rows[i].FindControl("GrdEditRight");
                    CheckBox GrdDelRight = (CheckBox)GridUserRight.Rows[i].FindControl("GrdDelRight");
                    CheckBox GrdPrintRight = (CheckBox)GridUserRight.Rows[i].FindControl("GrdPrintRight");

                    Entity_UserMaster.FkUserId = Convert.ToInt32(ViewState["EditID"]);
                    Entity_UserMaster.FormCaption = LblFormName.Text.Trim();
                    Entity_UserMaster.ViewAuth = GrdViewRight.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);
                    Entity_UserMaster.AddAuth = GrdAddRight.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);
                    Entity_UserMaster.DelAuth = GrdDelRight.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);
                    Entity_UserMaster.EditAuth = GrdEditRight.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);
                    Entity_UserMaster.PrintAuth = GrdPrintRight.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);

                    UpdateRow = Obj_UserMaster.InsertUserAuthDetails(ref Entity_UserMaster, out StrError);
                }

                for (int i = 0; i < GridUser.Rows.Count; i++)
                {
                    Label LblFormName = (Label)GridUser.Rows[i].Cells[3].FindControl("LblFormName");
                    CheckBox GrdUserAllHeaderAll = (CheckBox)GridUser.Rows[i].Cells[1].FindControl("GrdUserAllHeaderAll");

                    Entity_UserMaster.FkUserId = Convert.ToInt32(ViewState["EditID"]);
                    Entity_UserMaster.FormCaption = LblFormName.Text.Trim();
                    Entity_UserMaster.Email = GrdUserAllHeaderAll.Checked == true ? Convert.ToBoolean(1) : Convert.ToBoolean(0);

                    InsertDetail = Obj_UserMaster.InsertUserEmailDetail(ref Entity_UserMaster, out StrError);

                }
                for (int i = 0; i < ChkSite.Items.Count; i++)
                {
                    Entity_UserMaster.FkUserId = Convert.ToInt32(ViewState["EditID"]);
                    Entity_UserMaster.CafeteriaId = Convert.ToInt32(ChkSite.Items[i].Value);
                    if (ChkSite.Items[i].Selected)
                    {

                        Entity_UserMaster.Cheked = true;
                    }
                    else
                    {
                        Entity_UserMaster.Cheked = false;
                    }
                    InsertChkDetail = Obj_UserMaster.InsertUserSiteDetail(ref Entity_UserMaster, out StrError);

                }
                #region [FOR SETTING ]---------------
                if (CHKYESEDITPO.Checked == true)
                {
                    InsertChkDetail = Obj_UserMaster.InsertUserPermissionForEditPO(Convert.ToInt32(ViewState["EditID"]), "Edit Authorised Purchase Order", 1, TXTPASSEDITPO.Text.Trim(), out StrError);
                }
                if (CHKYESUNITCONVERSION.Checked == true)
                {
                    InsertChkDetail = Obj_UserMaster.InsertUserPermissionForEditPO(Convert.ToInt32(ViewState["EditID"]), "Unit Conversion In Item Master", 1, TXTPASSEDITPO.Text.Trim(), out StrError);
                }
                if (CHKESCESSREPORT.Checked == true)
                {
                    InsertChkDetail = Obj_UserMaster.InsertUserPermissionForEditPO(Convert.ToInt32(ViewState["EditID"]), "Purchase Order Shortage/Excess Report", 1, TXTEXCESSREPORT.Text.Trim(), out StrError);
                }
                #endregion---------------------------
                if (UpdateRow != 0)
                {
                    Obj_Comm.ShowPopUpMsg("Record Updated Successfully!!", this.Page);
                    MakeEmptyForm();
                    Entity_UserMaster = null;
                    Obj_Comm = null;
                }
            }

        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }
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
                Entity_UserMaster.UserID = DeleteId;
                Entity_UserMaster.LoginDate = DateTime.Now;
                Entity_UserMaster.IsDeleted = true;

                int iDelete = Obj_UserMaster.DeleteUserDetails(ref Entity_UserMaster, out StrError);

                if (iDelete != 0)
                {
                    Obj_Comm.ShowPopUpMsg("Record Deleted SuccessFully...!", this.Page);
                    MakeEmptyForm();
                }


                Entity_UserMaster = null;
                Obj_Comm = null;
            }
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMUserMaster Obj_UserMaster = new DMUserMaster();
        string[] SearchList = Obj_UserMaster.GetSuggestedRecord(prefixText);
        return SearchList;
    }
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        ReportGrid(StrCondition);
    }
    protected void GrdReport_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        flag = 1;
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        if (Convert.ToInt32(e.CommandArgument) != 0)
                        {
                            ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                            DS = Obj_UserMaster.GetUserDetailsForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            DataTable dt = new DataTable();
                          //  dt = (DataTable)ViewState["IsCentral"];
                            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                            {
                                TxtUserId.Text = DS.Tables[0].Rows[0]["LoginName"].ToString();
                                TxtMailId.Text = DS.Tables[0].Rows[0]["EmailId"].ToString();
                                TxtUserName.Text = DS.Tables[0].Rows[0]["UserName"].ToString();
                                TxtPasswrod.Attributes["value"] = DS.Tables[0].Rows[0]["Password"].ToString();
                                TxtConfrmPasswrd.Attributes["value"] = DS.Tables[0].Rows[0]["Password"].ToString();
                           
                                string str;
                                str = DS.Tables[0].Rows[0]["IsAdmin"].ToString();
                                if (str.Equals("True"))
                                {
                                    RadioIsAdmin.Text = "T";
                                }
                                else
                                {
                                    RadioIsAdmin.Text = "F";
                                }
                            }
                            if (DS.Tables[1].Rows.Count > 0)
                            {
                                GridUserRight.DataSource = DS.Tables[1];
                                GridUserRight.DataBind();
                            }
                            if (DS.Tables[2].Rows.Count > 0)
                            {
                                GridUser.DataSource = DS.Tables[2];
                                GridUser.DataBind();
                            }
                            if (DS.Tables[3].Rows.Count > 0)
                            {
                                ChkSite.DataSource = DS.Tables[3];
                                ChkSite.DataValueField = "CafeteriaId";
                                ChkSite.DataTextField = "Cafeteria";
                                ChkSite.DataBind();
                                ViewState["IsCentral"] = DS.Tables[3];

                                for (int i = 0; i < ChkSite.Items.Count; i++)
                                {
                                    if (Convert.ToBoolean(DS.Tables[3].Rows[i]["Cheked"])==false)
                                    {
                                        ChkSite.Items[i].Selected = false;
                                    }
                                    else
                                    {
                                        ChkSite.Items[i].Selected = true;
                                    }
                                }
                            }
                            else
                            {
                                FillGrid();
                                //MakeEmptyForm();
                            }
                            if (DS.Tables[4].Rows.Count > 0)
                            {
                                for (int k = 0; k < DS.Tables[4].Rows.Count; k++)
                                {
                                    if (DS.Tables[4].Rows[k]["Form"].ToString() == "Edit Authorised Purchase Order")
                                    {
                                        CHKYESEDITPO.Checked = true;
                                        TXTPASSEDITPO.Text = DS.Tables[4].Rows[k]["Password"].ToString();
                                    }
                                    if (DS.Tables[4].Rows[k]["Form"].ToString() == "Unit Conversion In Item Master")
                                    {
                                        CHKYESUNITCONVERSION.Checked = true;
                                    }
                                    if (DS.Tables[4].Rows[k]["Form"].ToString() == "Purchase Order Shortage/Excess Report")
                                    {
                                        CHKESCESSREPORT.Checked = true;
                                        TXTEXCESSREPORT.Text = DS.Tables[4].Rows[k]["Password"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                CHKESCESSREPORT.Checked = CHKYESUNITCONVERSION.Checked = CHKYESEDITPO.Checked = false;
                                TXTEXCESSREPORT.Text = TXTPASSEDITPO.Text = "";

                            }
                            DS = null;
                            Obj_UserMaster = null;
                            BtnSave.Visible = false;
                            if (!FlagEdit)
                              BtnUpdate.Visible = true;
                            if (!FlagDel)
                              BtnDelete.Visible = true;
                            TxtUserName.Focus();
                        }
                    }
                    break;
            }
        }

        catch (Exception ex)
        {
            StrError = ex.Message;
        }
    }

    
   
    protected void GrdViewRight_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkViewRight = (CheckBox)sender;
        GridViewRow dr = (GridViewRow)chkViewRight.Parent.Parent;
        if (chkViewRight.Checked == false)
        {
            ((CheckBox)dr.FindControl("GrdDelRight")).Checked = false;
            ((CheckBox)dr.FindControl("GrdEditRight")).Checked = false;

        }
    }
  
    
    protected void RadioIsAdmin_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckBox GrdSelectAllHeader = ((CheckBox)GridUserRight.HeaderRow.FindControl("GrdSelectAllHeader"));
        try
        {
            if (RadioIsAdmin.Items[0].Selected)
            {

                GrdSelectAllHeader.Checked = true;
                for (int i = 0; i < GridUserRight.Rows.Count; i++)
                {
                    ((CheckBox)GridUserRight.Rows[i].Cells[0].FindControl("GrdSelectAll")).Checked = true;
                    ((CheckBox)GridUserRight.Rows[i].Cells[4].FindControl("GrdAddRight")).Checked = true;
                    ((CheckBox)GridUserRight.Rows[i].Cells[5].FindControl("GrdViewRight")).Checked = true;
                    ((CheckBox)GridUserRight.Rows[i].Cells[6].FindControl("GrdEditRight")).Checked = true;
                    ((CheckBox)GridUserRight.Rows[i].Cells[7].FindControl("GrdDelRight")).Checked = true;
                    ((CheckBox)GridUserRight.Rows[i].Cells[8].FindControl("GrdPrintRight")).Checked = true;
                    GridUserRight.Rows[i].BackColor = System.Drawing.Color.LightGray;
                    GridUserRight.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#F2FFF8");

                }
                PnlUserRight.Enabled = false;
                RadioIsAdmin.Enabled = true;
                RadioIsAdmin.Text = "T";

            }
            else
            {
                GrdSelectAllHeader.Checked = false;
                for (int i = 0; i < GridUserRight.Rows.Count; i++)
                {
                    ((CheckBox)GridUserRight.Rows[i].Cells[0].FindControl("GrdSelectAll")).Checked = false;
                    ((CheckBox)GridUserRight.Rows[i].Cells[4].FindControl("GrdAddRight")).Checked = false;
                    ((CheckBox)GridUserRight.Rows[i].Cells[5].FindControl("GrdViewRight")).Checked = false;
                    ((CheckBox)GridUserRight.Rows[i].Cells[6].FindControl("GrdEditRight")).Checked = false;
                    ((CheckBox)GridUserRight.Rows[i].Cells[7].FindControl("GrdDelRight")).Checked = false;
                    ((CheckBox)GridUserRight.Rows[i].Cells[8].FindControl("GrdPrintRight")).Checked = false;
                    GridUserRight.Rows[i].BackColor = System.Drawing.Color.White;
                }
                PnlUserRight.Enabled = true;
                RadioIsAdmin.Text = "F";
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

   protected void GridUserRight_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //=========For Merge the Row In to Single Section=================
        for (int rowIndex = GridUserRight.Rows.Count - 2;
                                    rowIndex >= 0; rowIndex--)
        {
            GridViewRow gvRow = GridUserRight.Rows[rowIndex];
            GridViewRow gvPreviousRow = GridUserRight.Rows[rowIndex + 1];
            // for (int cellCount = 0; cellCount < gvRow.Cells.Count;cellCount++)
            //{
            if (gvRow.Cells[3].Text ==
                                   gvPreviousRow.Cells[3].Text)
            {
                if (gvPreviousRow.Cells[3].RowSpan < 2)
                {
                    gvRow.Cells[3].RowSpan = 2;
                }
                else
                {
                    gvRow.Cells[3].RowSpan =
                        gvPreviousRow.Cells[3].RowSpan + 1;
                }
                gvPreviousRow.Cells[3].Visible = false;
            }
            //}
        }
        //=========For Merge the Row In to Single Section=================
    }

    protected void GridUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //=========For Merge the Row In to Single Section=================
        for (int rowIndex = GridUser.Rows.Count - 2;
                                    rowIndex >= 0; rowIndex--)
        {
            GridViewRow gvRow = GridUser.Rows[rowIndex];
            GridViewRow gvPreviousRow = GridUser.Rows[rowIndex + 1];
            // for (int cellCount = 0; cellCount < gvRow.Cells.Count;cellCount++)
            //{
            if (gvRow.Cells[3].Text ==
                                   gvPreviousRow.Cells[3].Text)
            {
                if (gvPreviousRow.Cells[3].RowSpan < 2)
                {
                    gvRow.Cells[3].RowSpan = 2;
                }
                else
                {
                    gvRow.Cells[3].RowSpan =
                        gvPreviousRow.Cells[3].RowSpan + 1;
                }
                gvPreviousRow.Cells[3].Visible = false;
            }
            //}
        }
        //=========For Merge the Row In to Single Section=================
    }

    protected void GrdSelectAllHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox GrdSelectAllHeader = ((CheckBox)GridUserRight.HeaderRow.FindControl("GrdSelectAllHeader"));
        if (GrdSelectAllHeader.Checked == true)
        {
            for (int i = 0; i < GridUserRight.Rows.Count; i++)
            {
                ((CheckBox)GridUserRight.Rows[i].Cells[0].FindControl("GrdSelectAll")).Checked = true;
                ((CheckBox)GridUserRight.Rows[i].Cells[4].FindControl("GrdAddRight")).Checked = true;
                ((CheckBox)GridUserRight.Rows[i].Cells[5].FindControl("GrdViewRight")).Checked = true;
                ((CheckBox)GridUserRight.Rows[i].Cells[6].FindControl("GrdEditRight")).Checked = true;
                ((CheckBox)GridUserRight.Rows[i].Cells[7].FindControl("GrdDelRight")).Checked = true;
                ((CheckBox)GridUserRight.Rows[i].Cells[8].FindControl("GrdPrintRight")).Checked = true;
                // GridUserRight.Rows[i].BackColor = System.Drawing.Color.LightGray;
                GridUserRight.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#F2FFF8");
            }
        }
        else
        {
            for (int i = 0; i < GridUserRight.Rows.Count; i++)
            {
                ((CheckBox)GridUserRight.Rows[i].Cells[0].FindControl("GrdSelectAll")).Checked = false;
                ((CheckBox)GridUserRight.Rows[i].Cells[4].FindControl("GrdAddRight")).Checked = false;
                ((CheckBox)GridUserRight.Rows[i].Cells[5].FindControl("GrdViewRight")).Checked = false;
                ((CheckBox)GridUserRight.Rows[i].Cells[6].FindControl("GrdEditRight")).Checked = false;
                ((CheckBox)GridUserRight.Rows[i].Cells[7].FindControl("GrdDelRight")).Checked = false;
                ((CheckBox)GridUserRight.Rows[i].Cells[8].FindControl("GrdPrintRight")).Checked = false;
                GridUserRight.Rows[i].BackColor = System.Drawing.Color.White;
            }
        }
    }

    protected void GrdUserAllHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox GrdUserAllHeader = ((CheckBox)GridUser.HeaderRow.FindControl("GrdUserAllHeader"));
        if (GrdUserAllHeader.Checked == true)
        {
            for (int i = 0; i < GridUser.Rows.Count; i++)
            {
                ((CheckBox)GridUser.Rows[i].Cells[0].FindControl("GrdUserAllHeaderAll")).Checked = true;
               
                // GridUserRight.Rows[i].BackColor = System.Drawing.Color.LightGray;
                GridUser.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#F2FFF8");
            }
        }
        else
        {
            for (int i = 0; i < GridUser.Rows.Count; i++)
            {
                ((CheckBox)GridUser.Rows[i].Cells[0].FindControl("GrdUserAllHeaderAll")).Checked = false;

                GridUser.Rows[i].BackColor = System.Drawing.Color.White;
            }
        }
    }

    protected void GrdSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelectAll = (CheckBox)sender;
        GridViewRow dr = (GridViewRow)chkSelectAll.Parent.Parent;
        if (chkSelectAll.Checked == true)
        {
            ((CheckBox)dr.FindControl("GrdViewRight")).Checked = true;
            ((CheckBox)dr.FindControl("GrdAddRight")).Checked = true;
            ((CheckBox)dr.FindControl("GrdDelRight")).Checked = true;
            ((CheckBox)dr.FindControl("GrdEditRight")).Checked = true;
            ((CheckBox)dr.FindControl("GrdPrintRight")).Checked = true;
            //dr.BackColor = System.Drawing.Color.LightGray;
            dr.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2FFF8");
        }
        else
        {
            ((CheckBox)dr.FindControl("GrdViewRight")).Checked = false;
            ((CheckBox)dr.FindControl("GrdAddRight")).Checked = false;
            ((CheckBox)dr.FindControl("GrdDelRight")).Checked = false;
            ((CheckBox)dr.FindControl("GrdEditRight")).Checked = false;
            ((CheckBox)dr.FindControl("GrdPrintRight")).Checked = false;
            dr.BackColor = System.Drawing.Color.White;
        }
        GridUserRight.Rows[dr.RowIndex].FindControl("GrdSelectAll").Focus();
    }

   


}
