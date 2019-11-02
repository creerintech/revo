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

public partial class Masters_ItemSubCategoryMaster : System.Web.UI.Page
{
    #region[Private Variables]
        DMSubCategory Obj_IS=new DMSubCategory();
        SubCategory Entity_IS=new SubCategory();
        CommanFunction Obj_comm=new CommanFunction();
        DataSet DS=new DataSet();
        private string StrCondition=string.Empty;
        private string StrError=string.Empty;
        private static bool FlagAdd, FlagDel, FlagEdit = false;
    #endregion

    #region[User Function]
    private void MakeEmptyForm()
    {

        if (!FlagAdd)
        BtnSave.Visible=true;
        BtnUpdate.Visible=false;
        BtnDelete.Visible=false;
        TxtItemSubCat.Text=string.Empty;
        TxtSearch.Text=string.Empty;
        ReportGrid(StrCondition);
        ddlCategory.Focus();
        txtRemark.Text = string.Empty;
    }
    public void ReportGrid(string RepCondition)
    {
        try
        {
            DS=Obj_IS.GetSubCategory(RepCondition,out StrError);
            if(DS.Tables.Count>0&&DS.Tables[0].Rows.Count>0)
            {
                GrdReport.DataSource=DS.Tables[0];
                GrdReport.DataBind();
            }
            else
            {
                GrdReport.DataSource=null;
                GrdReport.DataBind();
            }
            if (DS.Tables.Count > 0 && DS.Tables[1].Rows.Count > 0)
            {
                ddlCategory.DataSource = DS.Tables[1];
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataBind();
            }
            else
            {
                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
            }
            ddlCategory.SelectedIndex = 0;
            Obj_IS=null;
            DS=null;

        }
        catch(Exception ex)
        {
            StrError=ex.Message;
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Item Sub Categoty Master'");
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
            DS = Obj_IS.ChkDuplicate(TxtItemSubCat.Text.Trim(), out StrError);

            if (DS.Tables[0].Rows.Count > 0)
            {
                Obj_comm.ShowPopUpMsg("This Sub Category Is Already Exist....", this.Page);
                TxtItemSubCat.Focus();
            }
            else
            {
                Entity_IS.SubCategoryName = TxtItemSubCat.Text.Trim();
                Entity_IS.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue.ToString());
                Entity_IS.UserId = Convert.ToInt32(Session["userId"]);
                Entity_IS.LoginDate = DateTime.Now;
                Entity_IS.IsDeleted = false;
                Entity_IS.Remark = txtRemark.Text.Trim();
                InsertRow = Obj_IS.InsertRecord(ref Entity_IS,out StrError);

                if (InsertRow != 0)
                {
                    Obj_comm.ShowPopUpMsg("Record Saved successfully",this.Page);
                    MakeEmptyForm();
                    Entity_IS = null;
                    Obj_comm = null;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0;
        try
        {
            //Check Duplicate
            //DS = Obj_IS.ChkDuplicate(TxtItemSubCat.Text.Trim(), out StrError);

            //if (DS.Tables[0].Rows.Count > 0)
            //{
            //    Obj_comm.ShowPopUpMsg("This Sub Category Is Already Exist....", this.Page);
            //    TxtItemSubCat.Focus();
            //}
            //else
            //{
                if (ViewState["EditID"] != null)
                {
                    Entity_IS.SubCategoryId = Convert.ToInt32(ViewState["EditID"]);
                }

                Entity_IS.SubCategoryName = TxtItemSubCat.Text.Trim();
                Entity_IS.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue.ToString());
                Entity_IS.UserId = Convert.ToInt32(Session["userId"]);
                Entity_IS.LoginDate = DateTime.Now;
                Entity_IS.Remark = txtRemark.Text.Trim();
                UpdateRow = Obj_IS.UpdateRecord(ref Entity_IS, out StrError);

                if (UpdateRow != 0)
                {
                    Obj_comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                    MakeEmptyForm();
                    Entity_IS = null;
                    Obj_comm = null;
                }
           // }
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
                Entity_IS.SubCategoryId = DeleteId;
                Entity_IS.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_IS.LoginDate = DateTime.Now;
                Entity_IS.IsDeleted = true;
                int iDelete = Obj_IS.DeleteRecord(ref Entity_IS, out StrError);

                if (iDelete != 0)
                {
                    Obj_comm.ShowPopUpMsg("Record Deleted Successfully !!", this.Page);
                    MakeEmptyForm();
                }


            }
            Entity_IS = null;
            Obj_comm = null;
        }
        catch (Exception ex)
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
                            ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                            DS = Obj_IS.GetSubCategoryForEdit(Convert.ToInt32(e.CommandArgument),out StrError);
                            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                            {
                                TxtItemSubCat.Text = DS.Tables[0].Rows[0]["SubCategory"].ToString();
                                ddlCategory.SelectedValue = DS.Tables[0].Rows[0]["CategoryID"].ToString();
                                txtRemark.Text = DS.Tables[0].Rows[0]["Remark"].ToString();
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            DS = null;
                            Obj_IS = null;
                            if (!FlagEdit)
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            if (!FlagDel)
                            BtnDelete.Visible = true;
                            TxtItemSubCat.Focus();
                        }
                        break;

                    }
            }
 
        }
        catch(Exception ex)
        {
            StrError = ex.Message;
        }
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        ReportGrid(StrCondition);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMSubCategory Obj_IS = new DMSubCategory();
        string[] SearchList = Obj_IS.GetSuggestRecord(prefixText);
        return SearchList;
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet DS = new DataSet();

        DS = Obj_IS.GetCategoryDetail(Convert.ToInt32(ddlCategory.SelectedValue),out StrError);

        if (DS.Tables[0].Rows.Count > 0)
        {
            CategoryGrid.DataSource = DS.Tables[0];
            CategoryGrid.DataBind();
        }
        else
        {
            CategoryGrid.DataSource =null;
            CategoryGrid.DataBind();
            Obj_comm.ShowPopUpMsg("No Subcategory Present For "+ddlCategory.SelectedItem.ToString() +" Category!!", this.Page);
        }

    }
    protected void CategoryGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           
            for (int rowIndex = CategoryGrid.Rows.Count - 2;
                                  rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = CategoryGrid.Rows[rowIndex];
                GridViewRow gvPreviousRow = CategoryGrid.Rows[rowIndex + 1];
                // for (int cellCount = 0; cellCount < gvRow.Cells.Count;cellCount++)
                //{
                if (gvRow.Cells[0].Text ==
                                       gvPreviousRow.Cells[0].Text)
                {
                    if (gvPreviousRow.Cells[0].RowSpan < 2)
                    {
                        gvRow.Cells[0].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[0].RowSpan =
                            gvPreviousRow.Cells[0].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[0].Visible = false;
                }
                //}
            }

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
}
