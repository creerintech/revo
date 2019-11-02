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

public partial class Masters_TermsCondition : System.Web.UI.Page
{
    #region[Private Variables]
        DMTermsConditions Obj_TermsConditionMaster = new DMTermsConditions();
        TermsCondition Entity_TermsConditionMaster = new TermsCondition();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagAdd, FlagDel, FlagEdit = false;
    #endregion

    #region UserFunctions
    private void MakeEmptyForm()
    {
        ViewState["EditID"] = null;
        if (!FlagAdd)
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnDelete.Visible = false;
        TxtTitle.Text = string.Empty;
        TxtDescription.Text = string.Empty;

      //  GetSupplierCode();
        ReportGrid(StrCondition);
        TxtTitle.Focus();
    }
    private void ReportGrid(string StrCondition)
    {
        try
        {
            Ds = Obj_TermsConditionMaster.GetTerms(StrCondition, out StrError);
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
            Obj_TermsConditionMaster = null;
            Ds = null;
        }
        catch(Exception ex)
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Terms & Condition Master'");
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
        if(!Page.IsPostBack)
        {
            CheckUserRight();
            MakeEmptyForm();
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0;
        try
        {
            Ds = Obj_TermsConditionMaster.ChkDuplicate(TxtTitle.Text.Trim(), out StrError);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                obj_Comman.ShowPopUpMsg("Title Name Already Exist..!", this.Page);
                TxtTitle.Focus();
            }
            else
            {
                Entity_TermsConditionMaster.Title = TxtTitle.Text;
                Entity_TermsConditionMaster.TermsConditions = TxtDescription.Text;
                Entity_TermsConditionMaster.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_TermsConditionMaster.LoginDate = DateTime.Now;
                InsertRow = Obj_TermsConditionMaster.InsertRecord(ref Entity_TermsConditionMaster, out StrError);
                if (InsertRow > 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                    MakeEmptyForm();
                }
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0;
        try
        {
            if (ViewState["EditID"] != null)
            {
                Entity_TermsConditionMaster.TermsID = Convert.ToInt32(ViewState["EditID"]);
            }

            Entity_TermsConditionMaster.Title = TxtTitle.Text;
            Entity_TermsConditionMaster.TermsConditions = TxtDescription.Text;
            Entity_TermsConditionMaster.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_TermsConditionMaster.LoginDate = DateTime.Now;
            UpdateRow = Obj_TermsConditionMaster.UpdateRecord(ref Entity_TermsConditionMaster, out StrError);
            if (UpdateRow > 0)
            {
                obj_Comman.ShowPopUpMsg("Record Updated Successfully", this.Page);
                MakeEmptyForm();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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
                Entity_TermsConditionMaster.TermsID = DeleteId;
                Entity_TermsConditionMaster.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_TermsConditionMaster.LoginDate = DateTime.Now;

                int iDelete = Obj_TermsConditionMaster.DeleteRecord(ref Entity_TermsConditionMaster, out StrError);
                if (iDelete != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_TermsConditionMaster = null;
            Obj_TermsConditionMaster = null;
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
                            Ds = Obj_TermsConditionMaster.GetTermsForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                            {
                                TxtTitle.Text = Ds.Tables[0].Rows[0]["Title"].ToString();
                                TxtDescription.Text = Ds.Tables[0].Rows[0]["TermsCondition"].ToString();
                             
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            Ds = null;
                            Obj_TermsConditionMaster = null;
                            if (!FlagEdit)
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            if (!FlagDel)
                            BtnDelete.Visible = true;
                       
                        }
                        break;
                    }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMTermsConditions Obj_SupplierMaster = new DMTermsConditions();
        String[] SearchList = Obj_SupplierMaster.GetSuggestRecord(prefixText);
        return SearchList;
    }
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        ReportGrid(StrCondition);
    }
}
