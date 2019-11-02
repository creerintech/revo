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

public partial class Reports_PendingRequisitionReport : System.Web.UI.Page
{
    #region[Private Variables]
        DMPendingRequisition Obj_RequisitionCafeteria = new DMPendingRequisition();
        PendingRequisition Entity_RequisitionCafeteria = new PendingRequisition();
        CommanFunction Obj_Comm = new CommanFunction();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        DataSet DS = new DataSet();
        private static bool FlagPrint = false;
        decimal SubTotal = 0;
        decimal Req = 0;
        decimal Iss= 0;
        decimal Pen= 0;
    #endregion

    #region[User Defined Function]
    private void SetInitialRow()
    {
        DataTable Dt = new DataTable();
        DataRow dr;

        Dt.Columns.Add("RequisitionNo", typeof(string));
        Dt.Columns.Add("ReqDate", typeof(string));
        Dt.Columns.Add("IssuseNo", typeof(string));
        Dt.Columns.Add("IssuseDate", typeof(string));
        Dt.Columns.Add("ReqLocation", typeof(string));
        Dt.Columns.Add("IssuseLocation", typeof(string));
        Dt.Columns.Add("ReqBy", typeof(string));
        Dt.Columns.Add("IssuBy", typeof(string));
        Dt.Columns.Add("CategoryName", typeof(string));
        Dt.Columns.Add("ItemName", typeof(string));
        Dt.Columns.Add("ItemDesc", typeof(string));
        Dt.Columns.Add("RemarkForPO", typeof(string));
        Dt.Columns.Add("Unit", typeof(string));
        Dt.Columns.Add("Qty", typeof(decimal));
        Dt.Columns.Add("OutwardQty", typeof(decimal));
        Dt.Columns.Add("PendingQty", typeof(decimal));
        Dt.Columns.Add("Rate", typeof(decimal));
        Dt.Columns.Add("Amount", typeof(decimal));

        dr = Dt.NewRow();

        dr["RequisitionNo"] = "";
        dr["ReqDate"] = "";
        dr["IssuseNo"] = "";
        dr["IssuseDate"] = "";
        dr["ReqLocation"] = "";
        dr["IssuseLocation"] = "";
        dr["ReqBy"] = "";
        dr["IssuBy"] = "";
        dr["CategoryName"] = "";
        dr["ItemName"] = "";
        dr["ItemDesc"] = "";
        dr["RemarkForPO"] = "";
        dr["Unit"] = "";
        dr["Qty"] = 0;
        dr["OutwardQty"] = 0;
        dr["PendingQty"] = 0;
        dr["Rate"] = 0;
        dr["Amount"] = 0;

        Dt.Rows.Add(dr);

        ViewState["CurrentTable"] = Dt;
        GridDetails.DataSource = Dt;
        GridDetails.DataBind();
    }

    public void MakeRed()
    {
        if (ViewState["IScancel"] != null)
        {
            DataTable dttable = new DataTable();
            dttable = (DataTable)ViewState["IScancel"];
            for (int i = 0; i < dttable.Rows.Count; i++)
            {
                for (int j = 0; j < GridDetails.Rows.Count; j++)
                {
                    if (((dttable.Rows[i][0].ToString()) == (GridDetails.Rows[j].Cells[1].Text)) && (Convert.ToBoolean(dttable.Rows[i][5].ToString()) == true))
                    {
                        for (int k = 0; k < GridDetails.Rows[j].Cells.Count; k++)
                        {
                            GridDetails.Rows[i].Cells[k].ForeColor = System.Drawing.Color.FromName("Red");
                        }
                    }
                }
            }
        }
    }

    public void MakeEmptyForm()
    {
        if(!FlagPrint)
          ImgBtnPrint.Visible = true;
        if (!FlagPrint)
          ImgBtnExcel.Visible = true;
        TxtFromDate.Enabled = TxtToDate.Enabled = true;
        TxtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
        TxtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        ChkFromDate.Checked = true;
        ddlRequisitionNo.SelectedIndex =ddlRequisitnBy.SelectedIndex=ddlLocation.SelectedIndex=ddlIssueNo.SelectedIndex= 0;
        ddlIssueBy.SelectedIndex =ddlItem.SelectedIndex=ddlCategory.SelectedIndex=ddlUnit.SelectedIndex= 0;
        SetInitialRow();
        SubTotal = Req = Iss = Pen = 0;
       // ReportGrid();

    }

    public void ReportGrid()
    {
        StrCondition = string.Empty;
        try
        {
            if (ChkFromDate.Checked)
            {
                StrCondition += " and RC.RequisitionDate Between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and RC.RequisitionDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlRequisitionNo.SelectedValue) > 0)
            {
                StrCondition += " and RC.RequisitionCafeId =" + Convert.ToInt32(ddlRequisitionNo.SelectedValue);
            }
           
            //----For IssueNo-----
            if (Convert.ToInt32(ddlIssueNo.SelectedValue) > 0)
            {
                StrCondition += " and O.OutwardId = " + Convert.ToInt32(ddlIssueNo.SelectedValue);
            }
            //----For RequisitnBy-----
            #region[EMPLOYEE REQ BY]
            if (ddlRequisitnBy.Items.Count == 0)
            {
                StrCondition = StrCondition + " and UM.UserId =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlRequisitnBy.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and UM.UserId =" + Convert.ToInt32(ddlRequisitnBy.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlRequisitnBy.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlRequisitnBy.Items[i].Value) != 0)
                            {
                                StrCondition += " and (UM.UserId = " + Convert.ToInt32(ddlRequisitnBy.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlRequisitnBy.Items[i].Value) != 0)
                            {
                                StrCondition += " or UM.UserId = " + Convert.ToInt32(ddlRequisitnBy.Items[i].Value);
                            }
                        }
                        if (i == ddlRequisitnBy.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlRequisitnBy.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
            }
            #endregion

            #region[EMPLOYEE ISSUE BY]
            if (ddlIssueBy.Items.Count == 0)
            {
                StrCondition = StrCondition + " and UM.UserId =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlIssueBy.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and UM.UserId =" + Convert.ToInt32(ddlIssueBy.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlIssueBy.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlIssueBy.Items[i].Value) != 0)
                            {
                                StrCondition += " and (UM.UserId = " + Convert.ToInt32(ddlIssueBy.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlIssueBy.Items[i].Value) != 0)
                            {
                                StrCondition += " or UM.UserId = " + Convert.ToInt32(ddlIssueBy.Items[i].Value);
                            }
                        }
                        if (i == ddlIssueBy.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlIssueBy.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
            }
            #endregion

            #region[LOCATION]

                if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and RC.CafeteriaId=" + Convert.ToInt32(ddlLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (RC.CafeteriaId = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or RC.CafeteriaId = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        if (i == ddlLocation.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
            
            #endregion

            //----For UnitId-----
            if (Convert.ToInt32(ddlUnit.SelectedValue) > 0)
            {
                StrCondition += " and U.UnitId = " + Convert.ToInt32(ddlUnit.SelectedValue);
            }
            //----For CategoryWise-----
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and IC.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            //----For ItemWIse-----
            if (Convert.ToInt32(ddlItem.SelectedValue) > 0)
            {
                StrCondition += " and IM.ItemId = " + Convert.ToInt32(ddlItem.SelectedValue);
            }
           
           
            DS = Obj_RequisitionCafeteria.GetList(StrCondition, out StrError);

            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                if (!FlagPrint)
                  ImgBtnPrint.Visible = true;
                if (!FlagPrint)
                  ImgBtnExcel.Visible = true;
                ViewState["IScancel"] = DS.Tables[0];
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
                //MakeRed();
                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + "Record Found";
                DS = null;
            }
            else
            {
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
                lblCount.Text = "";
                DS = null;
                SetInitialRow();
                ImgBtnPrint.Visible = false;
            }

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
            string COND = string.Empty;
            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
            COND = COND + " AND R.CafeteriaId= "+ Convert.ToInt32(Session["CafeteriaId"].ToString());
           // }
            DS = Obj_RequisitionCafeteria.FillReportCombo(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
            {
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        ddlRequisitionNo.DataSource = DS.Tables[0];
                        ddlRequisitionNo.DataTextField = "RequisitionNo";
                        ddlRequisitionNo.DataValueField = "RequisitionCafeId";
                        ddlRequisitionNo.DataBind();
                    }
                    //------For Location-------
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        ddlLocation.DataSource = DS.Tables[1];
                        ddlLocation.DataTextField = "Location";
                        ddlLocation.DataValueField = "StockLocationID";
                        ddlLocation.DataBind();
                    }
                    //------For RequisitionBy-------
                    if (DS.Tables[4].Rows.Count > 0)
                    {
                        //----Requisition By----
                        ddlRequisitnBy.DataSource = DS.Tables[4];
                        ddlRequisitnBy.DataTextField = "UserName";
                        ddlRequisitnBy.DataValueField = "UserId";
                        ddlRequisitnBy.DataBind();
                        //----Issue By----
                        ddlIssueBy.DataSource = DS.Tables[4];
                        ddlIssueBy.DataTextField = "UserName";
                        ddlIssueBy.DataValueField = "UserId";
                        ddlIssueBy.DataBind();
                    }
                    //------For Category-------
                    if (DS.Tables[3].Rows.Count > 0)
                    {
                        ddlCategory.DataSource = DS.Tables[3];
                        ddlCategory.DataTextField = "CategoryName";
                        ddlCategory.DataValueField = "CategoryId";
                        ddlCategory.DataBind();
                    }
                    //------For Item-------
                    if (DS.Tables[2].Rows.Count > 0)
                    {
                        ddlItem.DataSource = DS.Tables[2];
                        ddlItem.DataTextField = "ItemName";
                        ddlItem.DataValueField = "ItemId";
                        ddlItem.DataBind();
                    }
                    //------For Unit-------
                    if (DS.Tables[5].Rows.Count > 0)
                    {
                        ddlUnit.DataSource = DS.Tables[5];
                        ddlUnit.DataTextField = "Unit";
                        ddlUnit.DataValueField = "UnitId";
                        ddlUnit.DataBind();
                    }
                    //------For IssueNo-------
                    if (DS.Tables[6].Rows.Count > 0)
                    {
                        ddlIssueNo.DataSource = DS.Tables[6];
                        ddlIssueNo.DataTextField = "IssuseNo";
                        ddlIssueNo.DataValueField = "OutwardId";
                        ddlIssueNo.DataBind();
                    }
                }
                else
                {
                    DS = null;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public bool ChkDateFormat()
    {
        bool flag = false;
        try
        {
            if (ChkFromDate.Checked == true)
            {
                if (!string.IsNullOrEmpty(TxtFromDate.Text) && !string.IsNullOrEmpty(TxtToDate.Text))
                {
                    if ((Convert.ToDateTime(TxtFromDate.Text) < DateTime.Now) && (Convert.ToDateTime(TxtToDate.Text) < DateTime.Now))
                    {
                        if (Convert.ToDateTime(TxtFromDate.Text) < Convert.ToDateTime(TxtToDate.Text))
                        {
                            flag = true;
                        }
                        else
                        {
                            Obj_Comm.ShowPopUpMsg("From Date Must Be Less Than To Date...", this.Page);
                        }
                    }
                    else
                    {
                        Obj_Comm.ShowPopUpMsg("Please Select Date Less Than or Equal To Today Date..", this.Page);
                    }
                }
                else
                {
                    Obj_Comm.ShowPopUpMsg("Please Select Date..", this.Page);
                }
            }
            else
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return flag;
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Requisition Report'");
                    if (dtRow.Length > 0)
                    {
                        DataTable dt = dtRow.CopyToDataTable();
                        dsChkUserRight.Tables.Add(dt);
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                        && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        Response.Redirect("~/Masters/NotAuthUser.aspx");
                    }
                    //Checking View Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                    {
                        BtnShow.Visible = false;
                    }
                    //Checking Print Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        ImgBtnPrint.Visible = false;
                        ImgBtnExcel.Visible = false;
                        FlagPrint = true;
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
    //User Right Function==========  
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCombo();
            CheckUserRight();
            MakeEmptyForm();
        }
    }
   
    //========Important Required for Excel Export Must=============
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    //========Important Required for Excel Export Must=============

    protected void ChkFromDate_CheckedChanged1(object sender, EventArgs e)
    {
        if (ChkFromDate.Checked == true)
        {
            TxtFromDate.Enabled = TxtToDate.Enabled = true;
        }
        else
        {
            TxtFromDate.Enabled = TxtToDate.Enabled = false;
        }
    }

    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SubTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
                Req += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Qty"));
                Iss += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OutwardQty"));
                Pen += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingQty"));
                if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingQty")) > 0)
                {
                    e.Row.Cells[14].ForeColor = System.Drawing.Color.Red;
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[13].Text = "Total :  ";
                e.Row.Cells[14].Text = Req.ToString();
                e.Row.Cells[15].Text = Iss.ToString();
                e.Row.Cells[16].Text = Pen.ToString();
                e.Row.Cells[18].Text = SubTotal.ToString();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {
        if (ChkDateFormat() == true)
        {
            ReportGrid();
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }

    protected void ImgBtnExcel_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ChkFromDate.Checked)//and RC.RequisitionCafeId=22 and RC.CafeteriaId=4 and RC.RequisitionDate
            {
                StrCondition += " and RC.RequisitionDate Between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and RC.RequisitionDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlRequisitionNo.SelectedValue) > 0)
            {
                StrCondition += " and RC.RequisitionCafeId =" + Convert.ToInt32(ddlRequisitionNo.SelectedValue);
            }

            //----For IssueNo-----
            if (Convert.ToInt32(ddlIssueNo.SelectedValue) > 0)
            {
                StrCondition += " and O.OutwardId = " + Convert.ToInt32(ddlIssueNo.SelectedValue);
            }
            //----For RequisitnBy-----
            #region[EMPLOYEE REQ BY]
            if (ddlRequisitnBy.Items.Count == 0)
            {
                StrCondition = StrCondition + " and UM.UserId =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlRequisitnBy.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and UM.UserId =" + Convert.ToInt32(ddlRequisitnBy.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlRequisitnBy.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlRequisitnBy.Items[i].Value) != 0)
                            {
                                StrCondition += " and (UM.UserId = " + Convert.ToInt32(ddlRequisitnBy.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlRequisitnBy.Items[i].Value) != 0)
                            {
                                StrCondition += " or UM.UserId = " + Convert.ToInt32(ddlRequisitnBy.Items[i].Value);
                            }
                        }
                        if (i == ddlRequisitnBy.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlRequisitnBy.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
            }
            #endregion
            #region[EMPLOYEE ISSUE BY]
            if (ddlIssueBy.Items.Count == 0)
            {
                StrCondition = StrCondition + " and UM.UserId =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlIssueBy.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and UM.UserId =" + Convert.ToInt32(ddlIssueBy.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlIssueBy.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlIssueBy.Items[i].Value) != 0)
                            {
                                StrCondition += " and (UM.UserId = " + Convert.ToInt32(ddlIssueBy.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlIssueBy.Items[i].Value) != 0)
                            {
                                StrCondition += " or UM.UserId = " + Convert.ToInt32(ddlIssueBy.Items[i].Value);
                            }
                        }
                        if (i == ddlIssueBy.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlIssueBy.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
            }
            #endregion
            #region[LOCATION]

                if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and RC.CafeteriaId=" + Convert.ToInt32(ddlLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (RC.CafeteriaId = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or RC.CafeteriaId = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        if (i == ddlLocation.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
            
            #endregion

            //----For UnitId-----
            if (Convert.ToInt32(ddlUnit.SelectedValue) > 0)
            {
                StrCondition += " and U.UnitId = " + Convert.ToInt32(ddlUnit.SelectedValue);
            }
            //----For CategoryWise-----
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and IC.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            //----For ItemWIse-----
            if (Convert.ToInt32(ddlItem.SelectedValue) > 0)
            {
                StrCondition += " and IM.ItemId = " + Convert.ToInt32(ddlItem.SelectedValue);
            }
            //----For Location----
           

            DS = Obj_RequisitionCafeteria.GetList(StrCondition, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GridView GridExp = new GridView();
                GridExp.DataSource = DS.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("Requisition_Status_Itemwise_Report.xls", GridExp);
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("No Data Found To Export...!", this.Page);
                DS.Dispose();
                GridDetails.DataSource = null;
                GridDetails.DataBind();
            }
            DS = null;
        }
        catch (ThreadAbortException tex)
        {

        }
        catch (Exception ex)
        {
            Obj_Comm.ShowPopUpMsg(ex.Message, this.Page);
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string COND = string.Empty;
            DMFILTER DMFILTER = new DMFILTER();
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                COND = COND + " AND CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            DS = DMFILTER.FillReportComboCategoryWise(1, 0, COND, out StrError);
            {
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        ddlItem.DataSource = DS.Tables[0];
                        ddlItem.DataTextField = "ItemName";
                        ddlItem.DataValueField = "ItemId";
                        ddlItem.DataBind();
                    }
                }
                else
                {
                    DS = null;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


}
