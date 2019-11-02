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
using MayurInventory.DALSQLHelper;

public partial class Masters_StockLocationMaster : System.Web.UI.Page
{
    #region[Private variables]
        DMStockLocation Obj_SL = new DMStockLocation();
        StockLocation Entity_SL = new StockLocation();
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
                //if (!Session["UserRole"].Equals("Administrator"))
                //{
                    //Checking Right of users=======

                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Site Master'");
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
        TxtStockLocation.Focus();
        if(!FlagAdd)
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnDelete.Visible = false;
        TxtStockLocation.Text = string.Empty;
        ddlPersonName.SelectedValue = "0";
        TxtSiteAddr.Text = string.Empty;
        Txtabbreviations.Text = string.Empty;
        TxtSearch.Text = string.Empty;
        chkCenLoc.Checked = false;
        SetInitialRow();
        ReportGrid(StrCondition);
    }

    private void MakeControlEmpty()
    {
        ddlPersonName.SelectedValue = "0";
        TxtContactNo.Text = string.Empty;
        TxtPEmail.Text = string.Empty;
        TxtAddress.Text = string.Empty;

        ViewState["GridIndex"] = null;
        ViewState["GridDetails"] = null;
        ImgAddGrid.ImageUrl = "~/Images/Icon/Gridadd.png";
        ImgAddGrid.ToolTip = "Add Grid";

        ddlPersonName.Focus();
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
            if (DS.Tables.Count > 0 && DS.Tables[1].Rows.Count > 0)
            {
                ddlcompany.DataSource = DS.Tables[1];
                ddlcompany.DataValueField = "CompanyId";
                ddlcompany.DataTextField = "CompanyName";
                ddlcompany.DataBind();
            }
            Obj_SL = null;
            DS = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("#", typeof(Int32));
        dt.Columns.Add("EmployeeId", typeof(Int32));
        dt.Columns.Add("PersonName", typeof(string));
        dt.Columns.Add("ContactNo", typeof(string));
        dt.Columns.Add("EmailId", typeof(string));
        dt.Columns.Add("Address", typeof(string));

        dr = dt.NewRow();

        dr["#"] = 0;
        dr["EmployeeId"] = 0;
        dr["PersonName"] = "";
        dr["ContactNo"] = "";
        dr["EmailId"] = "";
        dr["Address"] = "";

        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        GridDetails.DataSource = dt;
        GridDetails.DataBind();
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
                    ddlPersonName.DataSource = DS.Tables[0];
                    ddlPersonName.DataTextField = "EmpName";
                    ddlPersonName.DataValueField = "EmployeeId";
                    ddlPersonName.DataBind();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckUserRight();
            FillCombo();
            MakeControlEmpty();
            MakeEmptyForm();
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDtls = 0;
        try
        {
            DS = Obj_SL.ChkDuplicate(TxtStockLocation.Text.Trim(), out StrError);
            if (DS.Tables[0].Rows.Count > 0)
            {
                obj_Comm.ShowPopUpMsg("Location Name Already Exist..!", this.Page);
                TxtStockLocation.Focus();
            }
            else
            {
                Entity_SL.Location = TxtStockLocation.Text.Trim();
                Entity_SL.abbreviation = Txtabbreviations.Text.Trim();
                Entity_SL.SiteId = 0;
                Entity_SL.TowerId = 0;
                Entity_SL.CompanyId = Convert.ToInt32(ddlcompany.SelectedValue);
                Entity_SL.SiteAddr = TxtSiteAddr.Text;
                Entity_SL.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_SL.LoginDate = DateTime.Now;
                Entity_SL.IsDeleted = false;
                Entity_SL.IsCental = chkCenLoc.Checked ? true : false;
                InsertRow = Obj_SL.InsertRecord(ref Entity_SL, out StrError);

                if (InsertRow > 0)
                {
                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dtInsert = new DataTable();
                        dtInsert = (DataTable)ViewState["CurrentTable"];
                        for (int i = 0; i < dtInsert.Rows.Count; i++)
                        {
                            Entity_SL.StockLocationID = InsertRow;
                            Entity_SL.EmployeeId = Convert.ToInt32(dtInsert.Rows[i]["EmployeeId"].ToString());
                            Entity_SL.CpersonName = dtInsert.Rows[i]["PersonName"].ToString();
                            Entity_SL.ContactNo = dtInsert.Rows[i]["ContactNo"].ToString();
                            Entity_SL.MailId = dtInsert.Rows[i]["EmailId"].ToString();
                            Entity_SL.PersonAddress = dtInsert.Rows[i]["Address"].ToString();
                            InsertRowDtls = Obj_SL.InsertDetailsRecord(ref Entity_SL, out StrError);
                        }
                    }
                    if (InsertRow != 0)
                    {
                        obj_Comm.ShowPopUpMsg("Record Saved Successfully", this.Page);
                        MakeControlEmpty();
                        MakeEmptyForm();
                        Entity_SL = null;
                        obj_Comm = null;
                    }
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
        int UpdateRow = 0, InsertRowDtls=0;
        try
        {
            if (ViewState["EditID"] != null)
            {
                Entity_SL.StockLocationID = Convert.ToInt32(ViewState["EditID"]);
            }
            Entity_SL.Location = TxtStockLocation.Text.Trim();
            Entity_SL.abbreviation = Txtabbreviations.Text.Trim();
            Entity_SL.SiteId = 0;
            Entity_SL.TowerId = 0;
            Entity_SL.SiteAddr = TxtSiteAddr.Text;
            Entity_SL.CompanyId = Convert.ToInt32(ddlcompany.SelectedValue);

            Entity_SL.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_SL.LoginDate = DateTime.Now;
            Entity_SL.IsCental = chkCenLoc.Checked ? true : false;
            UpdateRow = Obj_SL.UpdateRecord(ref Entity_SL, out StrError);

            if (UpdateRow > 0)
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtInsert = new DataTable();
                    dtInsert = (DataTable)ViewState["CurrentTable"];
                    for (int i = 0; i < dtInsert.Rows.Count; i++)
                    {
                        Entity_SL.EmployeeId = Convert.ToInt32(dtInsert.Rows[i]["EmployeeId"].ToString());
                        Entity_SL.CpersonName = dtInsert.Rows[i]["PersonName"].ToString();
                        Entity_SL.ContactNo = dtInsert.Rows[i]["ContactNo"].ToString();
                        Entity_SL.MailId = dtInsert.Rows[i]["EmailId"].ToString();
                        Entity_SL.PersonAddress = dtInsert.Rows[i]["Address"].ToString();
                        InsertRowDtls = Obj_SL.InsertDetailsRecord(ref Entity_SL, out StrError);
                    }
                }
                if (UpdateRow > 0)
                {
                    obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                    MakeControlEmpty();
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
                            if (Convert.ToInt32(dtCurrentTable.Rows[i]["EmployeeId"]) == Convert.ToInt32(ddlPersonName.SelectedValue))
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            dtCurrentTable.Rows[k]["EmployeeId"] = Convert.ToInt32(ddlPersonName.SelectedValue);
                            dtCurrentTable.Rows[k]["PersonName"] = ddlPersonName.SelectedItem.Text;
                            dtCurrentTable.Rows[k]["ContactNo"] = TxtContactNo.Text.Trim();
                            dtCurrentTable.Rows[k]["EmailId"] = TxtPEmail.Text.Trim();
                            dtCurrentTable.Rows[k]["Address"] = TxtAddress.Text.Trim(); 

                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            MakeControlEmpty();

                        }

                        else
                        {
                            dtTableRow = dtCurrentTable.NewRow();
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                            dtTableRow["EmployeeId"] = Convert.ToInt32(ddlPersonName.SelectedValue);
                            dtTableRow["PersonName"] = ddlPersonName.SelectedItem.Text;
                            dtTableRow["ContactNo"] = TxtContactNo.Text.Trim();
                            dtTableRow["EmailId"] = TxtPEmail.Text.Trim();
                            dtTableRow["Address"] = TxtAddress.Text.Trim();
                            dtCurrentTable.Rows.Add(dtTableRow);
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
                            if (Convert.ToInt32(dtCurrentTable.Rows[i]["EmployeeId"]) == Convert.ToInt32(ddlPersonName.SelectedValue))
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            dtCurrentTable.Rows[k]["EmployeeId"] = Convert.ToInt32(ddlPersonName.SelectedValue);
                            dtCurrentTable.Rows[k]["PersonName"] = ddlPersonName.SelectedItem.Text;
                            dtCurrentTable.Rows[k]["ContactNo"] = TxtContactNo.Text.Trim();
                            dtCurrentTable.Rows[k]["EmailId"] = TxtPEmail.Text.Trim();
                            dtCurrentTable.Rows[k]["Address"] = TxtAddress.Text.Trim();

                            ViewState["CurrentTable"] = dtCurrentTable;
                            GridDetails.DataSource = dtCurrentTable;
                            GridDetails.DataBind();
                            MakeControlEmpty();

                        }

                        else
                        {
                            dtTableRow = dtCurrentTable.NewRow();
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                            dtTableRow["EmployeeId"] = Convert.ToInt32(ddlPersonName.SelectedValue);
                            dtTableRow["PersonName"] = ddlPersonName.SelectedItem.Text;
                            dtTableRow["ContactNo"] = TxtContactNo.Text.Trim();
                            dtTableRow["EmailId"] = TxtPEmail.Text.Trim();
                            dtTableRow["Address"] = TxtAddress.Text.Trim();

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

    protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Index;
            if (e.CommandName == "SelectGrid")
            {
                if ((!(string.IsNullOrEmpty(GridDetails.Rows[0].Cells[3].Text))) && (GridDetails.Rows[0].Cells[3].Text.Equals("&nbsp;")))
                {
                    obj_Comm.ShowPopUpMsg("There Is No Record To Edit", this.Page);
                }
                else
                {
                    ImgAddGrid.ImageUrl = "~/Images/Icon/GridUpdate.png";
                    ImgAddGrid.ToolTip = "Update";

                    Index = Convert.ToInt32(e.CommandArgument);
                    ViewState["GridIndex"] = Index;

                    ddlPersonName.SelectedValue = GridDetails.Rows[Index].Cells[2].Text;
                    if (GridDetails.Rows[Index].Cells[4].Text.Equals("&nbsp;"))
                    { 
                        TxtContactNo.Text=string.Empty;
                    }
                    else
                    {
                        TxtContactNo.Text = (GridDetails.Rows[Index].Cells[4].Text);
                    }
                    if (GridDetails.Rows[Index].Cells[5].Text.Equals("&nbsp;"))
                    {
                        TxtPEmail.Text = string.Empty;
                    }
                    else
                    {
                        TxtPEmail.Text = (GridDetails.Rows[Index].Cells[5].Text);
                    }
                    if (GridDetails.Rows[Index].Cells[6].Text.Equals("&nbsp;"))
                    {
                        TxtAddress.Text = string.Empty;
                    }
                    else
                    {
                        TxtAddress.Text = (GridDetails.Rows[Index].Cells[6].Text);
                    }
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
                                ddlcompany.SelectedValue = DS.Tables[0].Rows[0]["CompanyID"].ToString();
                                TxtStockLocation.Text = DS.Tables[0].Rows[0]["Location"].ToString();
                                Txtabbreviations.Text = DS.Tables[0].Rows[0]["abbreviation"].ToString();
                                TxtSiteAddr.Text = DS.Tables[0].Rows[0]["SiteAddr"].ToString();
                                if (Convert.ToBoolean(DS.Tables[0].Rows[0]["IsCentral"].ToString()) == true)
                                {
                                    chkCenLoc.Checked = true;
                                }
                                else
                                {
                                    chkCenLoc.Checked = false;
                                }
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
                            Obj_SL = null;
                            if(!FlagEdit)
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            if (!FladDel)
                            BtnDelete.Visible = true;
                            TxtStockLocation.Focus();
                            MakeControlEmpty();
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
        DMStockLocation Obj_SL = new DMStockLocation();
        string[] SearchList = Obj_SL.GetSuggestRecord(prefixText);
        return SearchList;
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeControlEmpty();
        MakeEmptyForm();
    }
}
