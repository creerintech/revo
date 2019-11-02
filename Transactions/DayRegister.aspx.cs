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

using MayurInventory.DALSQLHelper;
using MayurInventory.DataModel;
using MayurInventory.DB;
using MayurInventory.EntityClass;
using MayurInventory.Utility;

public partial class Transactions_DayRegister : System.Web.UI.Page
{
    #region [Private Variables]
        DMDailyInwardOutwardRegister Obj_DailyInwardOutwardRegister = new DMDailyInwardOutwardRegister();
        DailyInwardOutwardRegister Entity_DailyInwardOutwardRegister = new DailyInwardOutwardRegister();
        CommanFunction Obj_Comm = new CommanFunction();
        DataSet DS = new DataSet();
        private static DataTable DSLocation = new DataTable();
        DataTable DtInward, DtEditInward;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
    #endregion

    #region [User Defined Methods]

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
                    //Checking Right of users=======
                    {
                        System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                        System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                        dsChkUserRight1 = (DataSet)Session["DataSet"];

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='DayRegister'");
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
                        //Checking View Right ========                    
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                        {
                            GridReport.Visible = false;
                        }
                        //Checking Add Right ========                    
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                        {
                            BtnSave.Visible = false;
                            FlagAdd = true;

                        }
                        //Edit /Delete Column Visible ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false &&
                            Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                        {
                            foreach (GridViewRow GRow in GridReport.Rows)
                            {
                                GRow.FindControl("ImgBtnDelete").Visible = false;
                                GRow.FindControl("ImageGridEdit").Visible = false;
                                GRow.FindControl("ImgBtnPrint").Visible = false;
                            }
                            //BtnUpdate.Visible = false;
                            FlagDel = true;
                            FlagEdit = true;
                            FlagPrint = true;
                        }
                        else
                        {
                            //Checking Delete Right ========
                            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                            {
                                foreach (GridViewRow GRow in GridReport.Rows)
                                {
                                    GRow.FindControl("ImgBtnDelete").Visible = false;
                                    FlagDel = true;
                                }
                            }

                            //Checking Edit Right ========
                            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                            {
                                foreach (GridViewRow GRow in GridReport.Rows)
                                {
                                    GRow.FindControl("ImageGridEdit").Visible = false;
                                    FlagEdit = true;
                                }
                            }

                            //Checking Print Right ========
                            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                            {
                                foreach (GridViewRow GRow in GridReport.Rows)
                                {
                                    GRow.FindControl("ImgBtnPrint").Visible = false;
                                    FlagPrint = true;
                                }
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
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //User Right Function===========

    public void MakeEmptyForm()
    {
        TxtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        lblEmployee.Text = Session["UserName"].ToString().ToUpper();
        if(!FlagAdd)
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        SetInitialRow();
        SetInitialRowReportGrid();
        FillCombo();
        ReportGrid(StrCondition);

        #region[UserRights]
        if (FlagEdit)
        {
            foreach (GridViewRow GRow in GridReport.Rows)
            {
                GRow.FindControl("ImageGridEdit").Visible = false;
            }
        }
        if (FlagDel)
        {
            foreach (GridViewRow GRow in GridReport.Rows)
            {
                GRow.FindControl("ImgBtnDelete").Visible = false;
            }
        }
        if (FlagPrint)
        {
            foreach (GridViewRow GRow in GridReport.Rows)
            {
                GRow.FindControl("ImgBtnPrint").Visible = false;
            }
        }
        #endregion
    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("ItemId", typeof(Int32));
        dt.Columns.Add("ItemName", typeof(string));
        dt.Columns.Add("Category", typeof(string));
        dt.Columns.Add("Quantity", typeof(decimal));
        dt.Columns.Add("DetailsId", typeof(Int32));
        dr = dt.NewRow();
        dr["ItemId"] = 0;
        dr["Category"] = "";
        dr["ItemName"] = "";
        dr["Quantity"] = 0.00;
        dr["DetailsId"] = 0;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        GridDetails.DataSource = dt;
        GridDetails.DataBind();
    }
    private void SetInitialRowReportGrid()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("#", typeof(Int32));
        dt.Columns.Add("InwardNo", typeof(string));
        dt.Columns.Add("Date", typeof(string));
        dt.Columns.Add("EmpName", typeof(string));
        dr = dt.NewRow();
        dr["#"] = 0;
        dr["InwardNo"] = "";
        dr["Date"] = "";
        dr["EmpName"] = "";
        dt.Rows.Add(dr);
        ViewState["CurrentReportTable"] = dt;
        GridReport.DataSource = dt;
        GridReport.DataBind();
    }
    private void FillCombo()
    {
        try
        {
            DS = Obj_DailyInwardOutwardRegister.FillCombo(out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlCategory.DataSource = DS.Tables[0];
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();
                }
                if (DS.Tables[1].Rows.Count > 0)
                {
                    ///ItemId,,,,ItemName
                    ddlItem.DataSource = DS.Tables[1];
                    ddlItem.DataTextField = "ItemName";
                    ddlItem.DataValueField = "ItemId";
                    ddlItem.DataBind();
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
            DS = Obj_DailyInwardOutwardRegister.GetDailyInwardOutward(StrCondition, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                //GridReport.DataSource = DS.Tables[0];
                //GridReport.DataBind();
            }
            else
            {
                GridReport.DataSource = null;
                GridReport.DataBind();
            }
            Obj_DailyInwardOutwardRegister = null;
            DS = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    private void BindToGrid(DataSet Ds)
    {
        for (int j = 0; j < Ds.Tables[0].Rows.Count; j++)
        {
            int ItemID_All = Convert.ToInt32(Ds.Tables[0].Rows[j]["ItemID"]);
            if (ViewState["CurrentTable"] != null)
            {
                bool DupFlag = false;
                int k = 0;
                DataTable DTTABLE = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (DTTABLE.Rows.Count > 0)
                {
                    if (ViewState["GridIndex"] == null)
                    {
                        for (int i = 0; i < DTTABLE.Rows.Count; i++)
                        {
                            int ItemID_Single = Convert.ToInt32(DTTABLE.Rows[i]["ItemID"]);
                            if (DTTABLE.Rows.Count == 1 && (DTTABLE.Rows[0]["ItemID"].ToString()) == "0")
                            {
                                DTTABLE.Rows.RemoveAt(0);
                            }
                            if (Convert.ToInt32(ItemID_Single) == Convert.ToInt32(ItemID_All))
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            DTTABLE.Rows[k]["ItemId"] = Convert.ToString(DTTABLE.Rows[k]["ItemId"]);
                            DTTABLE.Rows[k]["ItemName"] = Convert.ToString(DTTABLE.Rows[k]["ItemName"]);
                            DTTABLE.Rows[k]["Category"] = Convert.ToString(DTTABLE.Rows[k]["Category"]);
                            DTTABLE.Rows[k]["Quantity"] = Convert.ToDecimal(DTTABLE.Rows[k]["Quantity"]);
                            DTTABLE.Rows[k]["DetailsId"] = Convert.ToDecimal(DTTABLE.Rows[k]["DetailsId"]);
                            ViewState["CurrentTable"] = DTTABLE;
                            GridDetails.DataSource = DTTABLE;
                            GridDetails.DataBind();
                            DtEditInward = (DataTable)ViewState["CurrentTable"];
                        }
                        else
                        {
                            drCurrentRow = DTTABLE.NewRow();
                            drCurrentRow["ItemId"] = Ds.Tables[0].Rows[j]["ItemId"].ToString();
                            drCurrentRow["ItemName"] = Ds.Tables[0].Rows[j]["ItemName"].ToString();
                            drCurrentRow["Category"] = Ds.Tables[0].Rows[j]["Category"].ToString();
                            drCurrentRow["Quantity"] = Ds.Tables[0].Rows[j]["Quantity"].ToString();
                            drCurrentRow["DetailsId"] = Ds.Tables[0].Rows[j]["DetailsId"].ToString();
                            DTTABLE.Rows.Add(drCurrentRow);
                            ViewState["CurrentTable"] = DTTABLE;
                            ViewState["GridDetails1"] = DTTABLE;
                            GridDetails.DataSource = DTTABLE;
                            GridDetails.DataBind();
                            int g = k;
                            ViewState["GridIndex"] = g++;

                        }
                    }
                    else
                    {
                        for (int i = 0; i < DTTABLE.Rows.Count; i++)
                        {
                            int ItemID_Single = Convert.ToInt32(DTTABLE.Rows[i]["ItemID"]);
                            if (Convert.ToInt32(ItemID_Single) == Convert.ToInt32(ItemID_All))
                            {
                                DupFlag = true;
                                k = i;
                            }
                        }
                        if (DupFlag == true)
                        {
                            // DTROW = DTTABLE.NewRow();
                            int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                            DTTABLE.Rows[k]["ItemId"] = Convert.ToString(DTTABLE.Rows[k]["ItemId"]);
                            DTTABLE.Rows[k]["ItemName"] = Convert.ToString(DTTABLE.Rows[k]["ItemName"]);
                            DTTABLE.Rows[k]["Category"] = Convert.ToDecimal(DTTABLE.Rows[k]["Category"]);
                            DTTABLE.Rows[k]["Quantity"] = Convert.ToDecimal(DTTABLE.Rows[k]["Quantity"]);
                            DTTABLE.Rows[k]["DetailsId"] = Convert.ToDecimal(DTTABLE.Rows[k]["DetailsId"]);
                            ViewState["CurrentTable"] = DTTABLE;
                            GridDetails.DataSource = DTTABLE;
                            GridDetails.DataBind();
                            DtEditInward = (DataTable)ViewState["CurrentTable"];
                        }
                        else
                        {
                            drCurrentRow = DTTABLE.NewRow();
                            drCurrentRow["ItemId"] = Ds.Tables[0].Rows[j]["ItemId"].ToString();
                            drCurrentRow["ItemName"] = Ds.Tables[0].Rows[j]["ItemName"].ToString();
                            drCurrentRow["Category"] = Ds.Tables[0].Rows[j]["Category"].ToString();
                            drCurrentRow["Quantity"] = Ds.Tables[0].Rows[j]["Quantity"].ToString();
                            drCurrentRow["DetailsId"] = Ds.Tables[0].Rows[j]["DetailsId"].ToString();
                            DTTABLE.Rows.Add(drCurrentRow);
                            //newrowindex++;
                            ViewState["CurrentTable"] = DTTABLE;
                            ViewState["GridDetails1"] = DTTABLE;
                            GridDetails.DataSource = DTTABLE;
                            GridDetails.DataBind();
                            DtEditInward = (DataTable)ViewState["GridDetails1"];
                        }
                    }
                }
            }
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {   
            MakeEmptyForm();
            CheckUserRight();
         }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMIssueRegister Obj_IR = new DMIssueRegister();
        string[] SearchList = Obj_IR.GetSuggestedRecord(prefixText);
        return SearchList;
    }
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            StrCondition = TxtSearch.Text.Trim();
            ReportGrid(StrCondition);
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DS = null;
            DS = Obj_DailyInwardOutwardRegister.GetItemForList(Convert.ToInt32(ddlCategory.SelectedValue), out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                ddlItem.DataSource = DS.Tables[0];
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "ItemId";
                ddlItem.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void BtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DS = null;
            DS = Obj_DailyInwardOutwardRegister.GetItemForAdd(Convert.ToInt32(ddlItem.SelectedValue), out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                BindToGrid(DS);
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDetails = 0;
        try
        {
            if (true == true)
            {
                Entity_DailyInwardOutwardRegister.InwardNo = lblDInwardNo.Text;
                Entity_DailyInwardOutwardRegister.InwardDate = !string.IsNullOrEmpty(TxtDate.Text) ? Convert.ToDateTime(TxtDate.Text.Trim()) : DateTime.Now;
                Entity_DailyInwardOutwardRegister.EmployeeId = Convert.ToInt32(Session["UserId"]);//Convert.ToString(ddlIssueTo.SelectedValue);
                Entity_DailyInwardOutwardRegister.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_DailyInwardOutwardRegister.LoginDate = DateTime.Now;
                InsertRow = Obj_DailyInwardOutwardRegister.InsertDailyInwardOutwardRegister(ref Entity_DailyInwardOutwardRegister, out StrError);

                if (InsertRow > 0)
                {
                    if (ViewState["CurrentTable"] != null)
                    {
                        for (int i = 0; i < GridDetails.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(((TextBox)GridDetails.Rows[i].FindControl("TxtIssueQty")).Text) > 0)
                            {
                                Entity_DailyInwardOutwardRegister.DailyOutRegisterId = InsertRow;//nazeria  1200000
                                Entity_DailyInwardOutwardRegister.ItemId = Convert.ToInt32(GridDetails.Rows[i].Cells[0].Text);
                                Entity_DailyInwardOutwardRegister.Quantity = Convert.ToDecimal(((TextBox)GridDetails.Rows[i].FindControl("TxtIssueQty")).Text);
                                InsertRowDetails = Obj_DailyInwardOutwardRegister.InsertDailyInwardOutwardRegisterDetails(ref Entity_DailyInwardOutwardRegister, out StrError);
                            }
                        }
                    }

                    if (InsertRow > 0)
                    {
                        Obj_Comm.ShowPopUpMsg("Record Saved Successfully...!", this.Page);
                        MakeEmptyForm();
                        ddlCategory.SelectedValue =ddlItem.SelectedValue= "0";
                        Entity_DailyInwardOutwardRegister = null;
                        Obj_DailyInwardOutwardRegister = null;

                    }
                }
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("Please Enter Details...!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, UpdateRowDtls = 0;
        try
        {
            if (ViewState["EditID"] != null)
            {
                Entity_DailyInwardOutwardRegister.DailyOutRegisterId = Convert.ToInt32(ViewState["EditID"]);

            }
            Entity_DailyInwardOutwardRegister.InwardNo = lblDInwardNo.Text;
            Entity_DailyInwardOutwardRegister.InwardDate = !string.IsNullOrEmpty(TxtDate.Text) ? Convert.ToDateTime(TxtDate.Text.Trim()) : DateTime.Now;
            Entity_DailyInwardOutwardRegister.EmployeeId = Convert.ToInt32(Session["UserId"]);//Convert.ToString(ddlIssueTo.SelectedValue);
            Entity_DailyInwardOutwardRegister.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_DailyInwardOutwardRegister.LoginDate = DateTime.Now;
            UpdateRow = Obj_DailyInwardOutwardRegister.UpdateDailyInwardOutwardRegister(ref Entity_DailyInwardOutwardRegister, out StrError);
            if (UpdateRow > 0)
            {
                if (true== true)
                {
                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dtInsert = new DataTable();
                        dtInsert = (DataTable)ViewState["CurrentTable"];

                        for (int i = 0; i < GridDetails.Rows.Count; i++)
                        {
                            Entity_DailyInwardOutwardRegister.DailyOutRegisterId = Convert.ToInt32(ViewState["EditID"]);
                            Entity_DailyInwardOutwardRegister.ItemId = Convert.ToInt32(GridDetails.Rows[i].Cells[0].Text);
                            Entity_DailyInwardOutwardRegister.Quantity = Convert.ToDecimal(((TextBox)GridDetails.Rows[i].FindControl("TxtIssueQty")).Text);
                            UpdateRowDtls = Obj_DailyInwardOutwardRegister.InsertDailyInwardOutwardRegisterDetails(ref Entity_DailyInwardOutwardRegister, out StrError);
                        }
                    }
                    if (UpdateRow > 0)
                    {
                        Obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                        //MakeControlEmpty();
                        MakeEmptyForm();
                        Entity_DailyInwardOutwardRegister = null;
                        Obj_DailyInwardOutwardRegister = null;
                    }
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
    protected void GridReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("SelectGrid"):
                    {
                        int CurRow = Convert.ToInt32(e.CommandArgument);
                        int InwardOutwardID = Convert.ToInt32(GridReport.Rows[CurRow].Cells[0].Text);
                        ViewState["EditID"] = InwardOutwardID;
                        DS = Obj_DailyInwardOutwardRegister.GetDailyInwardOutwardRegisterForEdit(InwardOutwardID, out StrError);
                        if (DS.Tables.Count > 0 )
                        {
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                lblDInwardNo.Text = DS.Tables[0].Rows[0]["InwardNo"].ToString();
                                TxtDate.Text = DS.Tables[0].Rows[0]["TxtDate"].ToString();
                                lblEmployee.Text = DS.Tables[0].Rows[0]["EmpName"].ToString();
                            }
                            if (DS.Tables[1].Rows.Count > 0)
                            {
                                GridDetails.DataSource = DS.Tables[1];
                                GridDetails.DataBind();
                            }
                        }
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void GridReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int DeleteId = 0;
            int CurRow = Convert.ToInt32(e.RowIndex);
            DeleteId = Convert.ToInt32(GridReport.Rows[CurRow].Cells[7].Text);
            ViewState["EditID"] = DeleteId;
            if (DeleteId != 0)
            {
                Entity_DailyInwardOutwardRegister.DailyOutRegisterId= DeleteId;
                Entity_DailyInwardOutwardRegister.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_DailyInwardOutwardRegister.LoginDate = DateTime.Now;
                int iDelete = Obj_DailyInwardOutwardRegister.DeleteDailyInwardOutwardRegister(ref Entity_DailyInwardOutwardRegister, out StrError);
                if (iDelete != 0)
                {
                    Obj_Comm.ShowPopUpMsg("Record Succesfully Deleted", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_DailyInwardOutwardRegister = null;
            Obj_DailyInwardOutwardRegister = null;

        }
        catch (Exception ex)
        {   
        }
    }
    protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Delete"):
                    {
                        ViewState["DeleteID"] = Convert.ToInt32(e.CommandArgument);
                        DataTable dtdelete = (DataTable)ViewState["CurrentTable"];
                        for (int currindex = 0; currindex < dtdelete.Rows.Count; currindex++)
                        {  
                            dtdelete.Rows.RemoveAt(Convert.ToInt32(e.CommandArgument));
                            break;                         
                        }
                        GridDetails.DataSource = dtdelete;
                        GridDetails.DataBind();
                    }
                    break;
            }
        }
        catch (Exception ex)
        {

        }
        
    }
}
