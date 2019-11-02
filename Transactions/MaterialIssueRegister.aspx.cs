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

public partial class Transactions_MaterialIssueRegister1 : System.Web.UI.Page
{
    #region [Private Variables]
        DMIssueRegister Obj_IssueRegister = new DMIssueRegister();
        IssueRegister Entity_IssueRegister = new IssueRegister();
        CommanFunction Obj_Comm = new CommanFunction();
        DataSet DS = new DataSet();
        private static DataTable DSLocation = new DataTable();
        DataTable DtEditIssue, DtEditPO;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
    #endregion
  
    #region [User Defined Methods ]

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
                    ////Checking Right of users=======
                    //{
                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Issue Register'");
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
                        GrdReport.Visible = false;
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
                        foreach (GridViewRow GRow in GrdReport.Rows)
                        {
                            GRow.FindControl("ImageBtnDelete").Visible = false;
                            GRow.FindControl("ImgGridEdit").Visible = false;
                            GRow.FindControl("ImgBtnPrint").Visible = false;
                        }
                        BtnUpdate.Visible = false;
                        FlagDel = true;
                        FlagEdit = true;
                        FlagPrint = true;
                    }
                    else
                    {
                        //Checking Delete Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                        {
                            foreach (GridViewRow GRow in GrdReport.Rows)
                            {
                                GRow.FindControl("ImageBtnDelete").Visible = false;
                                FlagDel = true;
                            }
                        }

                        //Checking Edit Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                        {
                            foreach (GridViewRow GRow in GrdReport.Rows)
                            {
                                GRow.FindControl("ImgGridEdit").Visible = false;
                                FlagEdit = true;
                            }
                        }

                        //Checking Print Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                        {
                            foreach (GridViewRow GRow in GrdReport.Rows)
                            {
                                GRow.FindControl("ImgBtnPrint").Visible = false;
                                FlagPrint = true;
                            }
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
        ViewState["EditID"] = null;
        if(!FlagAdd)
         BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        TxtMIssueNo.Focus();
        TxtReqBy.Text=TxtReqstnDate.Text= TxtSearch.Text = string.Empty;
        ddlIssueTo.Text = Session["UserName"].ToString().ToUpper();
        TxtIssueDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
     // TxtReqstnDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
      //  ddlIssueTo.SelectedValue = "0";
        ddlRequisitionNo.SelectedValue = "0";
        ddlCategory.SelectedIndex = ddlItems.SelectedIndex = 0;
        GetIssueRegisterNO();
        SetInitialRow();
        SetInitialRow_IssueDetails();
        ddlRequisitionNo.Enabled = true;
        ReportGrid(StrCondition);
        #region[UserRights]
        if (FlagEdit)
        {
            foreach (GridViewRow GRow in GrdReport.Rows)
            {
                GRow.FindControl("ImgGridEdit").Visible = false;
            }
        }
        if (FlagDel)
        {
            foreach (GridViewRow GRow in GrdReport.Rows)
            {
                GRow.FindControl("ImageBtnDelete").Visible = false;
            }
        }
        if (FlagPrint)
        {
            foreach (GridViewRow GRow in GrdReport.Rows)
            {
                GRow.FindControl("ImgBtnPrint").Visible = false;
            }
        }
        #endregion
    }

    public void MakeControlEmpty()
    {
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        TxtMIssueNo.Focus();
        TxtSearch.Text = string.Empty;
        ViewState["DupFlag1"] = false;
        ViewState["GridIndex"] = null;
        ViewState["GridDetails"] = null;
        ddlRequisitionNo.Focus();

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
                            if (DTTABLE.Rows.Count == 1 && (DTTABLE.Rows[0]["#"].ToString()) == "0")
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
                            DTTABLE.Rows[k]["#"] = Convert.ToString(DTTABLE.Rows[k]["#"]);
                            DTTABLE.Rows[k]["ItemId"] = Convert.ToString(DTTABLE.Rows[k]["ItemId"]);
                            DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(DTTABLE.Rows[k]["ItemCode"]);
                            DTTABLE.Rows[k]["ItemName"] = Convert.ToString(DTTABLE.Rows[k]["ItemName"]);
                            DTTABLE.Rows[k]["AvailableQty"] = Convert.ToString(DTTABLE.Rows[k]["AvailableQty"]);
                            DTTABLE.Rows[k]["Qty"] = Convert.ToDecimal(DTTABLE.Rows[k]["Qty"]);
                            DTTABLE.Rows[k]["IssueQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["IssueQty"]);
                            DTTABLE.Rows[k]["PendingQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["PendingQty"]);
                            DTTABLE.Rows[k]["Notes"] = Convert.ToDecimal(DTTABLE.Rows[k]["Notes"]);
                            DTTABLE.Rows[k]["Status"] = Convert.ToString(DTTABLE.Rows[k]["Status"]);
                            ViewState["CurrentTable"] = DTTABLE;
                            GridDetails.DataSource = DTTABLE;
                            GridDetails.DataBind();
                            DtEditPO = (DataTable)ViewState["CurrentTable"];
                        }
                        else
                        {
                            drCurrentRow = DTTABLE.NewRow();
                            drCurrentRow["#"] = Ds.Tables[0].Rows[j]["#"].ToString();
                            drCurrentRow["ItemId"] = Ds.Tables[0].Rows[j]["ItemId"].ToString();
                            drCurrentRow["ItemCode"] = Ds.Tables[0].Rows[j]["ItemCode"].ToString();
                            drCurrentRow["ItemName"] = Ds.Tables[0].Rows[j]["ItemName"].ToString();
                            drCurrentRow["AvailableQty"] = Ds.Tables[0].Rows[j]["AvailableQty"].ToString();
                            drCurrentRow["Qty"] = Ds.Tables[0].Rows[j]["Qty"].ToString();
                            drCurrentRow["IssueQty"] =!string.IsNullOrEmpty(TxtIssueQty.Text)?TxtIssueQty.Text:"0";//Ds.Tables[0].Rows[j]["IssueQty"].ToString();
                            drCurrentRow["PendingQty"] = Ds.Tables[0].Rows[j]["PendingQty"].ToString();
                            drCurrentRow["Notes"] =!string.IsNullOrEmpty(TxtRemark.Text)?TxtRemark.Text:"";// Ds.Tables[0].Rows[j]["Notes"].ToString();
                            drCurrentRow["Status"] = Ds.Tables[0].Rows[j]["Status"].ToString();
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
                            DTTABLE.Rows[k]["#"] = Convert.ToString(DTTABLE.Rows[k]["#"]);
                            DTTABLE.Rows[k]["ItemId"] = Convert.ToString(DTTABLE.Rows[k]["ItemId"]);
                            DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(DTTABLE.Rows[k]["ItemCode"]);
                            DTTABLE.Rows[k]["ItemName"] = Convert.ToString(DTTABLE.Rows[k]["ItemName"]);
                            DTTABLE.Rows[k]["AvailableQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvailableQty"]);
                            DTTABLE.Rows[k]["Qty"] = Convert.ToDecimal(DTTABLE.Rows[k]["Qty"]);
                            DTTABLE.Rows[k]["IssueQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["IssueQty"]);
                            DTTABLE.Rows[k]["PendingQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["PendingQty"]);
                            DTTABLE.Rows[k]["Notes"] = Convert.ToDecimal(DTTABLE.Rows[k]["Notes"]);
                            DTTABLE.Rows[k]["Status"] = Convert.ToString(DTTABLE.Rows[k]["Status"]);
                            ViewState["CurrentTable"] = DTTABLE;
                            GridDetails.DataSource = DTTABLE;
                            GridDetails.DataBind();
                            DtEditPO = (DataTable)ViewState["CurrentTable"];
                        }
                        else
                        {
                            drCurrentRow = DTTABLE.NewRow();
                            drCurrentRow["#"] = Ds.Tables[0].Rows[j]["#"].ToString();
                            drCurrentRow["ItemId"] = Ds.Tables[0].Rows[j]["ItemId"].ToString();
                            drCurrentRow["ItemCode"] = Ds.Tables[0].Rows[j]["ItemCode"].ToString();
                            drCurrentRow["ItemName"] = Ds.Tables[0].Rows[j]["ItemName"].ToString();
                            drCurrentRow["AvailableQty"] = Ds.Tables[0].Rows[j]["AvailableQty"].ToString();
                            drCurrentRow["Qty"] = Ds.Tables[0].Rows[j]["Qty"].ToString();
                            drCurrentRow["IssueQty"] = !string.IsNullOrEmpty(TxtIssueQty.Text) ? TxtIssueQty.Text : "0";//Ds.Tables[0].Rows[j]["IssueQty"].ToString();
                            drCurrentRow["PendingQty"] = Ds.Tables[0].Rows[j]["PendingQty"].ToString();
                            drCurrentRow["Notes"] = !string.IsNullOrEmpty(TxtRemark.Text) ? TxtRemark.Text : "";// Ds.Tables[0].Rows[j]["Notes"].ToString();
                            drCurrentRow["Status"] = Ds.Tables[0].Rows[j]["Status"].ToString();
                            DTTABLE.Rows.Add(drCurrentRow);
                            //newrowindex++;
                            ViewState["CurrentTable"] = DTTABLE;
                            ViewState["GridDetails1"] = DTTABLE;
                            GridDetails.DataSource = DTTABLE;
                            GridDetails.DataBind();
                            DtEditPO = (DataTable)ViewState["GridDetails1"];
                        }
                    }
                }
            }
        }
    }

    private bool ChkDetails()
    {
        bool flag = false;
        try
        {
            for (int h = 0; h < GridDetails.Rows.Count; h++)
            {
                string str1=(((TextBox)GridDetails.Rows[h].FindControl("TxtIssueQty")).Text);
                if (string.IsNullOrEmpty((((TextBox)GridDetails.Rows[h].FindControl("TxtIssueQty")).Text)) || (((TextBox)GridDetails.Rows[h].FindControl("TxtIssueQty")).Text).Equals("&nbsp;"))
                {
                    (((TextBox)GridDetails.Rows[h].FindControl("TxtIssueQty")).Text) = "0";
                }
            }
            if (GridDetails.Rows.Count > 0 && !string.IsNullOrEmpty(GridDetails.Rows[0].Cells[2].Text) && !GridDetails.Rows[0].Cells[2].Text.Equals("&nbsp;") && GridDetails.Rows[0].Cells[2].Text != "0"&&(Convert.ToDecimal((TextBox)GridDetails.Rows[0].FindControl("IssueQty"))==0))
            {
                flag = true;
            }
            if (flag == true)
            {
                for (int i = 0; i < GridDetails.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(GridDetails.Rows[i].Cells[3].Text) > 0)
                    {
                        if (Convert.ToDecimal(((TextBox)GridDetails.Rows[i].FindControl("TxtIssueQty")).Text) > Convert.ToDecimal(GridDetails.Rows[i].Cells[3].Text))
                        {
                            Obj_Comm.ShowPopUpMsg("Issue Quantity Must Less than And Equal to Requisition Quantity", this.Page);
                            flag = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return flag;
    }

    private void FillCombo()
    {
        try
        {
            DS = Obj_IssueRegister.FillCombo(out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlRequisitionNo.DataSource = DS.Tables[0];
                    ddlRequisitionNo.DataTextField = "RequisitionNo";
                    ddlRequisitionNo.DataValueField = "RequisitionCafeId";
                    ddlRequisitionNo.DataBind();
                }

                if (DS.Tables[1].Rows.Count > 0)
                {
                    DSLocation = DS.Tables[1].Copy();
                }

                if (DS.Tables[2].Rows.Count > 0)
                {
                    //ddlIssueTo.DataSource = DS.Tables[2];
                    //ddlIssueTo.DataTextField = "EmpName";
                    //ddlIssueTo.DataValueField = "EmployeeId";
                    //ddlIssueTo.DataBind();
                }
                if (DS.Tables[3].Rows.Count > 0)
                {
                    ddlCategory.DataSource = DS.Tables[3];
                    ddlCategory.DataTextField = "Category";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();
                }
                if (DS.Tables[4].Rows.Count > 0)
                {
                    ddlItems.DataSource = DS.Tables[4];
                    ddlItems.DataTextField = "ItemName";
                    ddlItems.DataValueField = "ItemId";
                    ddlItems.DataBind();
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
            DS = Obj_IssueRegister.GetIssueRegister(StrCondition,out StrError);
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
            Obj_IssueRegister = null;
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
        dt.Columns.Add("ItemId", typeof(Int32));
        dt.Columns.Add("ItemCode", typeof(string));
        dt.Columns.Add("ItemName", typeof(string));
        dt.Columns.Add("AvailableQty",typeof(decimal));
        dt.Columns.Add("Qty", typeof(decimal));
        dt.Columns.Add("IssueQty", typeof(decimal));
        dt.Columns.Add("PendingQty",typeof(decimal));
        //dt.Columns.Add("StockLocationID", typeof(Int32));
        //dt.Columns.Add("Location", typeof(string));
       dt.Columns.Add("Notes", typeof(string));
       dt.Columns.Add("Status",typeof(string));

        dr = dt.NewRow();

        dr["#"] = 0;
        dr["ItemId"] = 0;
        dr["ItemCode"] = "";
        dr["ItemName"] = "";
        dr["IssueQty"] = 0.00;
        //dr["StockLocationID"] = 0;
        //dr["Location"] = "";
        dr["Qty"] = 0;
        dr["Notes"] = "";
        dr["Status"] = "";
        dr["PendingQty"] = 0;

        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        GridDetails.DataSource = dt;
        GridDetails.DataBind();
    }

    private void SetInitialRow_IssueDetails()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("#",typeof(Int32));
        dt.Columns.Add("IssueNo", typeof(string));
        dt.Columns.Add("IssueDate", typeof(DateTime));
        dt.Columns.Add("EmpName", typeof(string));
        dt.Columns.Add("RequisitionNo", typeof(string));
        dt.Columns.Add("RequisitionDate", typeof(DateTime));
        dt.Columns.Add("RequisitionCafeId",typeof(Int32));
        dt.Columns.Add("IssueRegisterId",typeof(Int32));

        dr = dt.NewRow();
        dr["#"] = 0;
        dr["IssueNo"] = "";
        dr["IssueDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
        dr["EmpName"] = "";
        dr["RequisitionNo"] = "";
        dr["RequisitionDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
        dr["RequisitionCafeId"] = 0;
        dr["IssueRegisterId"] = 0;
        
        dt.Rows.Add(dr);
        ViewState["ItemDetails"] = dt;
        GrdReport.DataSource = dt;
        GrdReport.DataBind();
    }

    private void BindLocation(DropDownList ddlIssueLocation)
    {
        try
        {
            if (DS.Tables[1].Rows.Count > 0)
            {
                GridDetails.DataSource = DS.Tables[1];

                ddlIssueLocation.DataSource = DS.Tables[1];
                ddlIssueLocation.DataTextField = "Location";
                ddlIssueLocation.DataValueField = "StockLocationID";
                ddlIssueLocation.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void GetIssueRegisterNO()
    {
        try
        {
            DS = Obj_IssueRegister.GetIssueRegisterNo(out StrError);
            if (DS.Tables[0].Rows.Count > 0)
            {
                TxtMIssueNo.Text = DS.Tables[0].Rows[0]["IssueNo"].ToString();
            }
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
            FillCombo();
            MakeControlEmpty();
            MakeEmptyForm();
            CheckUserRight();
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, UpdateRowDtls = 0;
       try
        {
            if (ViewState["EditID"] != null)
            {
                Entity_IssueRegister.IssueRegisterId = Convert.ToInt32(ViewState["EditID"]);

            }
            Entity_IssueRegister.IssueNo = TxtMIssueNo.Text;
            Entity_IssueRegister.IssueDate = !string.IsNullOrEmpty(TxtIssueDate.Text)? Convert.ToDateTime(TxtIssueDate.Text.Trim()):DateTime.Now;
            Entity_IssueRegister.EmployeeId = Convert.ToString(Session["UserId"]); //Convert.ToString(ddlIssueTo.SelectedValue);
            Entity_IssueRegister.RequisitionCafeId = Convert.ToInt32(ddlRequisitionNo.SelectedValue);

            Entity_IssueRegister.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_IssueRegister.LoginDate = DateTime.Now;

            UpdateRow = Obj_IssueRegister.UpdateIssueRegister(ref Entity_IssueRegister, out StrError);
            if (UpdateRow > 0)
            {
                if (ChkDetails() == true)
                {
                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dtInsert = new DataTable();
                        dtInsert = (DataTable)ViewState["CurrentTable"];

                        for (int i = 0; i < GridDetails.Rows.Count; i++)
                        {
                            Entity_IssueRegister.IssueRegisterId = Convert.ToInt32(ViewState["EditID"]);
                            Entity_IssueRegister.ItemId = Convert.ToInt32(GridDetails.Rows[i].Cells[0].Text);
                           // Entity_IssueRegister.StockLocationID = Convert.ToInt32(((DropDownList)GridDetails.Rows[i].FindControl("ddlIssueLocation")).SelectedValue);
                            Entity_IssueRegister.IssueQty = Convert.ToDecimal(((TextBox)GridDetails.Rows[i].FindControl("TxtIssueQty")).Text);
                            string[] OrderQty1 = (GridDetails.Rows[i].Cells[6].Text).Split(' ');
                            decimal OrderQty = Convert.ToDecimal(OrderQty1[0]);
                            Entity_IssueRegister.PendingQty = (Convert.ToDecimal(((TextBox)GridDetails.Rows[i].FindControl("TxtIssueQty")).Text) - Convert.ToDecimal(OrderQty));//Convert.ToDecimal(OrderQty);
                            string[] OrderQty2 = (GridDetails.Rows[i].Cells[4].Text).Split(' ');
                            decimal OrderQty3 = Convert.ToDecimal(OrderQty2[0]);
                            Entity_IssueRegister.Qty = Convert.ToDecimal(OrderQty3);
                            Entity_IssueRegister.Notes = Convert.ToString(((TextBox)GridDetails.Rows[i].FindControl("TxtNotes")).Text);

                            UpdateRowDtls = Obj_IssueRegister.InsertIssueRegisterDetails(ref Entity_IssueRegister, out StrError);

                        }
                    }
                    if (UpdateRow > 0)
                    {
                        Obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                        MakeControlEmpty();
                        MakeEmptyForm();
                        Entity_IssueRegister = null;
                        Obj_IssueRegister = null;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDetails = 0;
        try
        {
            if (ChkDetails() == true)
            {
                Entity_IssueRegister.IssueNo = TxtMIssueNo.Text;
                Entity_IssueRegister.IssueDate =!string.IsNullOrEmpty(TxtReqstnDate.Text)?Convert.ToDateTime(TxtReqstnDate.Text.Trim()):DateTime.Now;
                Entity_IssueRegister.EmployeeId = Convert.ToString(Session["UserId"]);//Convert.ToString(ddlIssueTo.SelectedValue);
                Entity_IssueRegister.RequisitionCafeId = Convert.ToInt32(ddlRequisitionNo.SelectedValue);

                Entity_IssueRegister.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_IssueRegister.LoginDate = DateTime.Now;
                Entity_IssueRegister.IsDeleted = false;
                InsertRow = Obj_IssueRegister.InsertIssueRegister(ref Entity_IssueRegister, out StrError);

                if (InsertRow > 0)
                {
                    if (ViewState["CurrentTable"] != null)
                    {
                        for (int i = 0; i < GridDetails.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(((TextBox)GridDetails.Rows[i].FindControl("TxtIssueQty")).Text) > 0)
                            {
                                Entity_IssueRegister.IssueRegisterId = InsertRow;
                                Entity_IssueRegister.ItemId = Convert.ToInt32(GridDetails.Rows[i].Cells[0].Text);
                                Entity_IssueRegister.IssueQty = Convert.ToDecimal(((TextBox)GridDetails.Rows[i].FindControl("TxtIssueQty")).Text);
                                // Entity_IssueRegister.StockLocationID = Convert.ToInt32(((DropDownList)GridDetails.Rows[i].FindControl("ddlIssueLocation")).SelectedValue);
                                Entity_IssueRegister.Notes = Convert.ToString(((TextBox)GridDetails.Rows[i].FindControl("TxtNotes")).Text);
                                string[] OrderQty1 = (GridDetails.Rows[i].Cells[6].Text).Split(' ');
                                decimal OrderQty = Convert.ToDecimal(OrderQty1[0]);
                                Entity_IssueRegister.PendingQty = ( Convert.ToDecimal(OrderQty)-Convert.ToDecimal(((TextBox)GridDetails.Rows[i].FindControl("TxtIssueQty")).Text) );//Convert.ToDecimal(OrderQty);
                                string[] OrderQty2 = (GridDetails.Rows[i].Cells[4].Text).Split(' ');
                                decimal OrderQty3 = Convert.ToDecimal(OrderQty2[0]);
                                Entity_IssueRegister.Qty = Convert.ToDecimal(OrderQty3);
                                InsertRowDetails = Obj_IssueRegister.InsertIssueRegisterDetails(ref Entity_IssueRegister, out StrError);
                            }
                        }
                    }

                    if (InsertRow > 0)
                    {
                        Obj_Comm.ShowPopUpMsg("Record Saved Successfully...!", this.Page);
                        MakeEmptyForm();
                        ddlRequisitionNo.SelectedValue = "0";
                        Entity_IssueRegister = null;
                        Obj_IssueRegister = null;

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

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
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
        StrCondition = TxtSearch.Text.Trim();
        ReportGrid(StrCondition);
    }

    protected void ddlRequisitionNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool DupFlag = false;
        int ID=0;
        try
        {
            if (GrdReport.Rows.Count != null)
            {
            for (int i = 0; i < GrdReport.Rows.Count; i++)
            {
                if (Convert.ToInt32(GrdReport.Rows[i].Cells[6].Text) == Convert.ToInt32(ddlRequisitionNo.SelectedValue))
                {
                    DupFlag = true;
                    ViewState["DupFlag1"] = true;
                    ID = Convert.ToInt32(GrdReport.Rows[i].Cells[7].Text);
                }
            }
            }
           if (DupFlag == false)
           {
               ID = Convert.ToInt32(ddlRequisitionNo.SelectedValue);
           }
               DS = Obj_IssueRegister.FillItemGrid( ID, out StrError,DupFlag);
               if (DS.Tables.Count > 0)
               {
                   if (DupFlag == false)
                   {
                       TxtReqstnDate.Text = Convert.ToDateTime(DS.Tables[0].Rows[0]["RequisitionDate"]).ToString("dd-MMM-yyyy");
                       TxtReqBy.Text =Session["UserName"].ToString(); //DS.Tables[0].Rows[0]["CreatedBy"].ToString();
                      // lblCafeteria.Text = DS.Tables[0].Rows[0]["Cafeteria"].ToString();
                   }
                   else
                   {
                       TxtReqstnDate.Text = Convert.ToDateTime(DS.Tables[1].Rows[0]["RequisitionDate"]).ToString("dd-MMM-yyyy");
                       TxtReqBy.Text = DS.Tables[1].Rows[0]["CreatedBy"].ToString();
                   //    lblCafeteria.Text = DS.Tables[1].Rows[0]["Cafeteria"].ToString();

                   }
                   GridDetails.DataSource = null;
                   GridDetails.DataBind();
                   GridDetails.DataSource = DS.Tables[0];
                   GridDetails.DataBind();
                   ViewState["CurrentTable"] = DS.Tables[0];

                   //for (int i = 0; i < GridDetails.Rows.Count; i++)
                   //{
                   //    if (Convert.ToDecimal(GridDetails.Rows[i].Cells[5].Text) == 0)
                   //    {
                   //        GridDetails.Rows[i].Enabled = false;
                   //    }
                   //}
               }
        
        }
        catch (Exception ex)
        {
            Obj_Comm.ShowPopUpMsg(ex.Message,this.Page);
        }
    }

    protected void ddlIssueTo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string[] OrderQty1 = (e.Row.Cells[6].Text).Split(' ');
                decimal OrderQty = Convert.ToDecimal(OrderQty1[0]);
                string[] OrderQty2 = (e.Row.Cells[4].Text).Split(' ');
                decimal OrderQty3 = Convert.ToDecimal(OrderQty2[0]);
                if (Convert.ToDecimal(OrderQty) <= 0 && Convert.ToDecimal(OrderQty3) > 0)
                {
                    ((TextBox)e.Row.FindControl("TxtIssueQty")).ReadOnly = true;
                    ((TextBox)e.Row.FindControl("TxtNotes")).ReadOnly = true;
                }
                else
                {
                    ((TextBox)e.Row.FindControl("TxtIssueQty")).ReadOnly = false;
                    ((TextBox)e.Row.FindControl("TxtNotes")).ReadOnly = false;
                }
            }
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    ((DropDownList)e.Row.FindControl("ddlIssueLocation")).DataSource = DSLocation;
            //    ((DropDownList)e.Row.FindControl("ddlIssueLocation")).DataTextField = "Location";
            //    ((DropDownList)e.Row.FindControl("ddlIssueLocation")).DataValueField = "StockLocationID";
            //    ((DropDownList)e.Row.FindControl("ddlIssueLocation")).DataBind();
            //    ((DropDownList)e.Row.FindControl("ddlIssueLocation")).SelectedValue = ((Label)e.Row.FindControl("GrdlblStockLocationId")).Text;

            //}
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void GrdReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int DeleteId = 0;
            int CurRow = Convert.ToInt32(e.RowIndex);
            DeleteId = Convert.ToInt32(GrdReport.Rows[CurRow].Cells[7].Text);
            //ViewState["EditID"] = IssueRegisterID;
            if (DeleteId != 0)
            {
                Entity_IssueRegister.IssueRegisterId = DeleteId;
                Entity_IssueRegister.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_IssueRegister.LoginDate = DateTime.Now;
                Entity_IssueRegister.IsDeleted = true;

                int iDelete = Obj_IssueRegister.DeleteIssueRegister(ref Entity_IssueRegister, out StrError);
                if (iDelete != 0)
                {
                    Obj_Comm.ShowPopUpMsg("Record Succesfully Deleted",this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_IssueRegister = null;
            Obj_IssueRegister = null;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void GrdReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("SelectGrid"):
                    {
                       
                       int CurRow=Convert.ToInt32(e.CommandArgument);
                       int IssueRegisterID = Convert.ToInt32(GrdReport.Rows[CurRow].Cells[7].Text);
                       ViewState["EditID"] = IssueRegisterID;
                       DS = Obj_IssueRegister.GetIssueRegisterForEdit(IssueRegisterID, out StrError);
                       if (DS.Tables.Count>0&&DS.Tables[0].Rows.Count > 0)
                       {
                           TxtMIssueNo.Text = DS.Tables[0].Rows[0]["IssueNo"].ToString();
                           TxtIssueDate.Text = Convert.ToDateTime(DS.Tables[0].Rows[0]["IssueDate"]).ToString("dd-MMM-yyyy");
                           ddlIssueTo.Text = Convert.ToString(DS.Tables[0].Rows[0]["UserName"]).ToString();
                           ddlRequisitionNo.SelectedValue =Convert.ToInt32(DS.Tables[0].Rows[0]["RequisitionCafeId"]).ToString();
                           ddlRequisitionNo.Enabled = false;
                           TxtReqstnDate.Text = Convert.ToDateTime(DS.Tables[0].Rows[0]["RequisitionDate"]).ToString("dd-MMM-yyyy");
                           TxtReqBy.Text = Convert.ToString(DS.Tables[0].Rows[0]["UserNameRequisition"]);//UserName UserNameRequisition
                           //lblCafeteria.Text = DS.Tables[0].Rows[0]["Cafeteria"].ToString();

                           GridDetails.DataSource = DS.Tables[1];
                           GridDetails.DataBind();
                           ViewState["CurrentTable"] = DS.Tables[1];
                           ViewState["DupFlag"] = true;
                          // MakeControlEmpty();
                           BtnUpdate.Visible = true;
                           BtnSave.Visible = false;
 
                       }
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
 
        }
    }

    protected void TxtIssueQty_TextChanged(object sender, EventArgs e)
  {
        try
        {
            //decimal ReqQty, pending, IssueQty, oldIssueQty, newPendingQty,ReqQuantity;
            //TextBox txt = (TextBox)sender;
            //GridViewRow grd = (GridViewRow)txt.Parent.Parent;
            //DataTable dt = (DataTable)ViewState["CurrentTable"];
            //int currentrow = grd.RowIndex;
            //pending = Convert.ToDecimal(GridDetails.Rows[currentrow].Cells[5].Text);
            //IssueQty = Convert.ToDecimal(((TextBox)grd.FindControl("TxtIssueQty")).Text);
            //ReqQuantity = Convert.ToDecimal(GridDetails.Rows[currentrow].Cells[3].Text);
            //if (Convert.ToBoolean(ViewState["DupFlag1"]) == true)
            // {
            //     if (IssueQty <= ReqQuantity)
            //     {
            //         pending = ReqQuantity - IssueQty;
            //         GridDetails.Rows[currentrow].Cells[5].Text = Convert.ToDecimal(pending).ToString();
            //     }
            //     else
            //     {
            //         ShowQtyMsg();
            //     }
            // }
            //else
            //{
            //    oldIssueQty = Convert.ToDecimal(dt.Rows[currentrow]["IssueQty"].ToString());
            //    if (Convert.ToBoolean(ViewState["DupFlag"]) == true)
            //    {
            //        if (IssueQty <= (pending + oldIssueQty))
            //        {
            //            if (IssueQty < oldIssueQty)
            //            {
            //                pending = oldIssueQty - IssueQty;
            //            }
            //            if ((pending + oldIssueQty) <= IssueQty)
            //            {
            //                //if ((IssueQty+pending) > pending)
            //                //{
            //                //    pending = ((ReqQuantity - pending) - IssueQty);
            //                //}
            //                //else
            //                //{
            //                    newPendingQty = pending + oldIssueQty;
            //                    pending = newPendingQty - IssueQty; ;
            //                //}
            //            }
                      
            //        }
            //        else if (IssueQty > pending)
            //        {
            //            pending = (ReqQuantity - (pending + IssueQty));
            //        }
            //        else
            //        {
            //            ShowQtyMsg();
            //        }
            //        if (IssueQty <= pending)
            //        {
            //            pending = (pending - IssueQty) + oldIssueQty;
            //        }
            //      //  ViewState["DupFlag"] = false;
            //    }
            //    else
            //    {
            //        ReqQty = Convert.ToDecimal(dt.Rows[currentrow]["Qty"]);
            //        pending = ReqQty - IssueQty;
                    
            //    }
            //    GridDetails.Rows[currentrow].Cells[5].Text = Convert.ToDecimal(pending).ToString();
            //}
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    
    }

    private void ShowQtyMsg()
    {
        Obj_Comm.ShowPopUpMsg("Issue Qty Must Be less Than Requisition Qty", this.Page);
        GridDetails.DataSource = ViewState["CurrentTable"];
        GridDetails.DataBind();
    }

    protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowindex = Convert.ToInt32(e.CommandArgument);
        ViewState["CurrentRow"] = rowindex;
    }
    protected void BtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DS = null;
            DS = Obj_IssueRegister.GetItemForAdd(Convert.ToInt32(ddlItems.SelectedValue), out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                BindToGrid(DS);
            }
            else
            {
            }
        }
        catch(Exception ex)
        {
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DS = null;
            DS=Obj_IssueRegister.GetItemForList(Convert.ToInt32(ddlCategory.SelectedValue),out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                ddlItems.DataSource = DS.Tables[0];
                ddlItems.DataTextField = "ItemName";
                ddlItems.DataValueField = "ItemId";
                ddlItems.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void GrdReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.GrdReport.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            DS = Obj_IssueRegister.GetIssueRegister(StrCondition, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GrdReport.DataSource = DS.Tables[0];
                this.GrdReport.DataBind();
                //-----For UserRights-------
                if (FlagDel && FlagEdit)
                {
                    foreach (GridViewRow GRow in GrdReport.Rows)
                    {
                        GRow.FindControl("ImageBtnDelete").Visible = false;
                        GRow.FindControl("ImgGridEdit").Visible = false;
                    }
                }
                else if (FlagDel)
                {
                    foreach (GridViewRow GRow in GrdReport.Rows)
                    {
                        GRow.FindControl("ImageBtnDelete").Visible = false;
                    }
                }
                else
                {
                    foreach (GridViewRow GRow in GrdReport.Rows)
                    {
                        GRow.FindControl("ImgGridEdit").Visible = false;
                    }
                }
            }
            else
            {
                //SetInitialRow_ReportGrid();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
