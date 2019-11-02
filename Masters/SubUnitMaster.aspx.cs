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

public partial class Masters_SubUnitMaster : System.Web.UI.Page
{
    #region[Private Variables]
    DMSubUnit Obj_SU = new DMSubUnit();
    SubUnit Entity_SU = new SubUnit();
    CommanFunction Obj_comm = new CommanFunction();
    DataSet DS = new DataSet();
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    private static bool FlagAdd, FlagDel, FlagEdit = false;
    #endregion

    #region[User Function]

    private void MakeEmptyForm()
    {
        TxtSubUnit.Text = string.Empty;
        ddlUnit.SelectedValue = "0";
        ddlUnit.Enabled = true;
        TxtConversnFactor.Text = string.Empty;
        if (!FlagAdd)
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnDelete.Visible = false;
        TxtSubUnit.Text = string.Empty;
        TxtSearch.Text = string.Empty;
        ReportGrid(StrCondition);
    }

    private void FillCombo()
    {
        try
        {
            DS = Obj_SU.FillCombo(out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlUnit.DataSource = DS.Tables[0];
                    ddlUnit.DataTextField = "Unit";
                    ddlUnit.DataValueField = "UnitId";
                    ddlUnit.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void ReportGrid(string RepCondition)
    {
        try
        {
            DS = Obj_SU.GetSubUnit(RepCondition, out StrError);
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
            Obj_SU = null;
            DS = null;
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
                //if (!Session["UserRole"].Equals("Administrator"))
                //{
                    //Checking Right of users=======

                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Sub Site Master'");
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
            CheckUserRight();
            FillCombo();
            MakeEmptyForm();
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0;
        try
        {
            DS = Obj_SU.ChkDuplicate(TxtSubUnit.Text.Trim(), out StrError);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Obj_comm.ShowPopUpMsg("This Sub Unit Is Already Exist....", this.Page);
                TxtSubUnit.Focus();
            }
            else
            {
                Entity_SU.SubUnit1 = TxtSubUnit.Text.Trim();
                Entity_SU.UnitId=Convert.ToInt32(ddlUnit.SelectedValue.ToString());
                Entity_SU.ConversionFactor = Convert.ToDecimal(TxtConversnFactor.Text.Trim());
                Entity_SU.UserId = Convert.ToInt32(Session["userId"]);
                Entity_SU.LoginDate = DateTime.Now;

                InsertRow = Obj_SU.InsertRecord(ref Entity_SU, out StrError);

                if (InsertRow != 0)
                {
                    Obj_comm.ShowPopUpMsg("Record Saved successfully", this.Page);
                    MakeEmptyForm();
                    Entity_SU = null;
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
            if (ViewState["EditID"] != null)
            {
                Entity_SU.SubUnitId = Convert.ToInt32(ViewState["EditID"]);
            }
            Entity_SU.SubUnit1 = TxtSubUnit.Text.Trim();
            Entity_SU.ConversionFactor = Convert.ToDecimal(TxtConversnFactor.Text.Trim());

            Entity_SU.UserId = Convert.ToInt32(Session["userId"]);
            Entity_SU.LoginDate = DateTime.Now;

            UpdateRow = Obj_SU.UpdateRecord(ref Entity_SU, out StrError);

            if (UpdateRow != 0)
            {
                Obj_comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                MakeEmptyForm();
                Entity_SU = null;
                Obj_comm = null;
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
                Entity_SU.SubUnitId = DeleteId;
                Entity_SU.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_SU.LoginDate = DateTime.Now;
                int iDelete = Obj_SU.DeleteRecord(ref Entity_SU, out StrError);

                if (iDelete != 0)
                {
                    Obj_comm.ShowPopUpMsg("Record Deleted Successfully !!", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_SU = null;
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
                            DS = Obj_SU.GetUnitForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                            {
                                TxtSubUnit.Text = DS.Tables[0].Rows[0]["SubUnit"].ToString();
                                ddlUnit.SelectedValue = DS.Tables[0].Rows[0]["UnitId"].ToString();
                                ddlUnit.Enabled = false;
                                TxtConversnFactor.Text = DS.Tables[0].Rows[0]["ConversionFactor"].ToString();
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            DS = null;
                            Obj_SU = null;
                            if (!FlagEdit)
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            if (!FlagDel)
                            BtnDelete.Visible = true;
                            TxtSubUnit.Focus();
                        }
                        break;
                    }
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
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        ReportGrid(StrCondition);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMSubUnit Obj_SU = new DMSubUnit();
        string[] SearchList = Obj_SU.GetSuggestRecord(prefixText);
        return SearchList;
    }

   
}