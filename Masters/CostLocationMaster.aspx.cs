﻿using System;
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
using MayurInventory.DALSQLHelper;

public partial class Masters_StockLocationMaster : System.Web.UI.Page
{
    #region[Private variables]
        DMCostCentre Obj_SL = new DMCostCentre();
        CostCentre Entity_SL = new CostCentre();
        CommanFunction obj_Comm = new CommanFunction();
        DataSet DS = new DataSet();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagAdd, FladDel, FlagEdit = false;
    #endregion

    #region[User Function]
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='CostCenter'");
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
                        FladDel = true;
                        FlagEdit = true;
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

    private void MakeEmptyForm()
    {
        if(!FlagAdd)
          BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnDelete.Visible = false;
        TxtCostCentre.Text = string.Empty;
        ddlSite.SelectedValue = "0";
        ddlTower.SelectedValue = "0";
        TxtSearch.Text = string.Empty;
        ReportGrid(StrCondition);
        TxtCostCentre.Focus();
    }

    private void FillCombo()
    {
        try
        {
            DS = Obj_SL.BindCombo(out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlSite.DataSource = DS.Tables[0];
                    ddlSite.DataTextField = "Location";
                    ddlSite.DataValueField = "StockLocationID";
                    ddlSite.DataBind();
                }
                if (DS.Tables[1].Rows.Count > 0)
                {
                    ddlTower.DataSource = DS.Tables[1];
                    ddlTower.DataTextField = "TowerName";
                    ddlTower.DataValueField = "TowerID";
                    ddlTower.DataBind();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }

    public void ReportGrid(string RepCondition)
    {
        try
        {
            DS = Obj_SL.GetStockLocation(RepCondition, out StrError);
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
            Obj_SL = null;
            DS = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CheckUserRight();
            FillCombo();
            MakeEmptyForm();
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0;
        try
        {
            DS = Obj_SL.ChkDuplicate(TxtCostCentre.Text.Trim(), out StrError);

            if (DS.Tables[0].Rows.Count > 0)
            {
                obj_Comm.ShowPopUpMsg("Cost Location Name Already Exist..!", this.Page);
                TxtCostCentre.Focus();
            }
            else
            {
                Entity_SL.Location = TxtCostCentre.Text.Trim();
                Entity_SL.SiteId = Convert.ToInt32(ddlSite.SelectedValue);
                Entity_SL.TowerId = Convert.ToInt32(ddlTower.SelectedValue);
                Entity_SL.CompanyId = 0;
                Entity_SL.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_SL.LoginDate = DateTime.Now;
                Entity_SL.IsDeleted = false;
                InsertRow = Obj_SL.InsertRecord(ref Entity_SL,out StrError);

                if (InsertRow != 0)
                {
                    obj_Comm.ShowPopUpMsg("Record Saved Successfully",this.Page);
                    MakeEmptyForm();
                    Entity_SL = null;
                    obj_Comm = null;
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
                Entity_SL.StockLocationID = Convert.ToInt32(ViewState["EditID"]);
            }
            Entity_SL.Location = TxtCostCentre.Text.Trim();
            Entity_SL.SiteId = Convert.ToInt32(ddlSite.SelectedValue);
            Entity_SL.TowerId = Convert.ToInt32(ddlTower.SelectedValue);
            Entity_SL.CompanyId = 0;

            Entity_SL.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_SL.LoginDate = DateTime.Now;
            UpdateRow = Obj_SL.UpdateRecord(ref Entity_SL, out StrError);

            if (UpdateRow != 0)
            {
                obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                MakeEmptyForm();
                Entity_SL = null;
                obj_Comm = null;
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
                Entity_SL.StockLocationID = DeleteId;
                Entity_SL.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_SL.LoginDate = DateTime.Now;
                Entity_SL.IsDeleted = true;
                int iDelete = Obj_SL.DeleteRecord(ref Entity_SL,out StrError);
                if (iDelete != 0)
                {
                    obj_Comm.ShowPopUpMsg("Record Deleted Successfully..!",this.Page);
                    MakeEmptyForm();
                }

            }
            Entity_SL = null;
            obj_Comm = null;
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
                            DS = Obj_SL.GetStockLOcationForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                            {
                                TxtCostCentre.Text = DS.Tables[0].Rows[0]["Location"].ToString();
                                ddlSite.SelectedValue = DS.Tables[0].Rows[0]["SiteId"].ToString();
                                ddlTower.SelectedValue = DS.Tables[0].Rows[0]["TowerId"].ToString();
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            DS = null;
                            Obj_SL = null;
                            if(!FlagEdit)
                             BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            if (!FladDel)
                              BtnDelete.Visible = true;
                            TxtCostCentre.Focus();
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
  
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        ReportGrid(StrCondition);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMCostCentre Obj_SL = new DMCostCentre();
        string[] SearchList = Obj_SL.GetSuggestRecord(prefixText);
        return SearchList;
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
}
