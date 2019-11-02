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

public partial class Masters_ItemCategory : System.Web.UI.Page
{
    #region[Private Variables]
        DMItemCategory Obj_ItemCategory = new DMItemCategory();
        ItemCategory Entity_ItemCategory = new ItemCategory();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagAdd,FlagDel,FlagEdit=false;
    #endregion

    #region[UserDefineFunctions]

    private void MakeEmptyForm()
    {
        ViewState["EditID"] = null;
        TxtItemCat.Focus();
        if(!FlagAdd)
        BtnSave.Visible = true;
        BtnDelete.Visible = false;
        BtnUpdate.Visible = false;
        TxtItemCat.Text = string.Empty;
        txtPrefix.Text = string.Empty;
        TxtSearch.Text = string.Empty;


        ReportGrid(StrCondition);
    }

    public void ReportGrid(string RepCondition)
    {
        try
        {
            Ds = Obj_ItemCategory.GetItemCategory(RepCondition, out StrError);
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
            Obj_ItemCategory = null;
            Ds = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Item Category Master'");
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
                        BtnCancel.Visible = false;
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

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMItemCategory Obj_ItemCategory = new DMItemCategory();
        String[] SearchList = Obj_ItemCategory.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          //  CheckUserRight();
            MakeEmptyForm();
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0;
        try
        {
            Ds = Obj_ItemCategory.ChkDuplicate(TxtItemCat.Text.Trim(), out StrError);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                obj_Comman.ShowPopUpMsg("Please Enter Another Item Category..", this.Page);
                TxtItemCat.Focus();
            }
            else
            {
                Entity_ItemCategory.CategoryName = TxtItemCat.Text.Trim();
                Entity_ItemCategory.Prefix = txtPrefix.Text;
                Entity_ItemCategory.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_ItemCategory.LoginDate = DateTime.Now;

                InsertRow = Obj_ItemCategory.InsertRecord(ref Entity_ItemCategory, out StrError);

                if (InsertRow != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                    MakeEmptyForm();
                    Entity_ItemCategory = null;
                    Obj_ItemCategory = null;
                }
            }
        }
        catch(Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0;
        try
        {
            if (ViewState["EditID"] != null)
            {
                Entity_ItemCategory.CategoryId = Convert.ToInt32(ViewState["EditID"]);
            }
            Entity_ItemCategory.CategoryName = TxtItemCat.Text.Trim();
            Entity_ItemCategory.Prefix = txtPrefix.Text.Trim();

            Entity_ItemCategory.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_ItemCategory.LoginDate = DateTime.Now;

            UpdateRow = Obj_ItemCategory.UpdateRecord(ref Entity_ItemCategory, out StrError);

            if (UpdateRow != 0)
            {
                obj_Comman.ShowPopUpMsg("Record Updated Successfully", this.Page);
                MakeEmptyForm();
                Entity_ItemCategory = null;
                Obj_ItemCategory = null;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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
                Entity_ItemCategory.CategoryId = DeleteId;
                Entity_ItemCategory.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_ItemCategory.LoginDate = DateTime.Now;

                int iDelete = Obj_ItemCategory.DeleteRecord(ref Entity_ItemCategory, out StrError);
                if (iDelete != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_ItemCategory = null;
            Obj_ItemCategory = null;
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
                            Ds = Obj_ItemCategory.GetItemCategoryForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                            {
                                TxtItemCat.Text = Ds.Tables[0].Rows[0]["CategoryName"].ToString();
                                txtPrefix.Text = Ds.Tables[0].Rows[0]["Prefix"].ToString();
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            Ds = null;
                            Obj_ItemCategory = null;
                            if(!FlagEdit)
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            if (!FlagDel )
                            BtnDelete.Visible = true;
                            TxtItemCat.Focus();
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
}
