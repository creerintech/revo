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

public partial class Reports_MonthEndSummary : System.Web.UI.Page
{
    #region[Private Variables]
        DMMayurInventory_Reports Obj_Reports = new DMMayurInventory_Reports();
        MayurInventory_Reports Entity_Reports = new MayurInventory_Reports();
        CommanFunction Obj_Comm = new CommanFunction();
        public static DataSet DsGrd = new DataSet();
        DataSet DS = new DataSet();
        private string StrCondition = string.Empty;
        private string StrRateCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagPrint = false;
    #endregion
    #region[User Defined Function]
        private void SetInitialRow()
        {
            DataTable Dt = new DataTable();
            DataRow dr;

            Dt.Columns.Add("RequisitionNo", typeof(string));
            Dt.Columns.Add("RequisitionDate", typeof(string));
            Dt.Columns.Add("Cafeteria", typeof(string));
            Dt.Columns.Add("AssignNo", typeof(string));
            Dt.Columns.Add("AssignDate", typeof(decimal));
            Dt.Columns.Add("ReqQty", typeof(string));
            Dt.Columns.Add("AssignQty", typeof(string));
            Dt.Columns.Add("PendingQty", typeof(string));
            Dt.Columns.Add("Status", typeof(string));
            Dt.Columns.Add("ItemName", typeof(string));
            Dt.Columns.Add("Description", typeof(string));
            Dt.Columns.Add("Remark", typeof(string));
            Dt.Columns.Add("CategoryName", typeof(string));
            Dt.Columns.Add("Unit", typeof(string));

            dr = Dt.NewRow();

            dr["RequisitionNo"] = "";
            dr["RequisitionDate"] = "";
            dr["Cafeteria"] = "";
            dr["AssignNo"] = "";
            dr["AssignDate"] = 0;
            dr["ReqQty"] = "";
            dr["AssignQty"] = "";
            dr["PendingQty"] = "";
            dr["Status"] = "";
            dr["ItemName"] = "";
            dr["Description"] = "";
            dr["Remark"] = "";
            dr["CategoryName"] = "";
            dr["Unit"] = "";
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
            TxtForMonth.Enabled = true;
            TxtForMonth.Text = DateTime.Now.ToString("MMM-yyyy");
            ddlRequisitionNo.SelectedIndex = ddlInwardNo.SelectedIndex = 0;
            SetInitialRow();
        }

        public void ReportGrid()
        {
            StrCondition = string.Empty;
            
            try
            {
                if (!TxtForMonth.Text.Equals(""))
                {
                    StrCondition += " and RC.RequisitionDate Between '" + Convert.ToDateTime("01-"+TxtForMonth.Text).ToString("MM-dd-yyyy") + "' and '" +
                        (Convert.ToDateTime(TxtForMonth.Text).AddMonths(1).AddDays(-1)).ToString("MM-dd-yyyy") + "'";
                }
                if (TxtForMonth.Text.Equals(""))
                {
                    StrCondition += " and RC.RequisitionDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                        Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
                }
                if (Convert.ToInt32(ddlRequisitionNo.SelectedValue) > 0)
                {
                    StrCondition += " and RC.RequisitionCafeId = " + Convert.ToInt32(ddlRequisitionNo.SelectedValue);
                }
                if (Convert.ToInt32(ddlInwardNo.SelectedValue) > 0)
                {
                    StrCondition += " and OUR.OutwardId = " + Convert.ToInt32(ddlInwardNo.SelectedValue);
                }
                if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
                {
                    StrCondition = StrCondition + " AND RC.CafeteriaId=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
                }
                DS = Obj_Reports.MonthReportReport(StrCondition, out StrError);

                if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    if (!FlagPrint)
                      ImgBtnPrint.Visible = true;
                    if (!FlagPrint)
                      ImgBtnExcel.Visible = true;
                    GridDetails.DataSource = DS.Tables[0];
                    GridDetails.DataBind();
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
                //    COND = COND + " AND P.LOC=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
                //}
                DS = Obj_Reports.FillReportCombo(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
                {
                    if (DS.Tables.Count > 0)
                    {
                        if (DS.Tables[6].Rows.Count > 0)
                        {
                            ddlRequisitionNo.DataSource = DS.Tables[6];
                            ddlRequisitionNo.DataTextField = "RequisitionNo";
                            ddlRequisitionNo.DataValueField = "RequisitionCafeId";
                            ddlRequisitionNo.DataBind();
                        }
                        if (DS.Tables[7].Rows.Count > 0)
                        {
                            ddlInwardNo.DataSource = DS.Tables[7];
                            ddlInwardNo.DataTextField = "StockNo";
                            ddlInwardNo.DataValueField = "OutwardId";
                            ddlInwardNo.DataBind();
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
            bool flag = true;
            try
            {
                //if (ChkFromDate.Checked == true)
                //{
                //    if (!string.IsNullOrEmpty(TxtFromDate.Text) && !string.IsNullOrEmpty(TxtToDate.Text))
                //    {
                //        if ((Convert.ToDateTime(TxtFromDate.Text) < DateTime.Now) && (Convert.ToDateTime(TxtToDate.Text) < DateTime.Now))
                //        {
                //            if (Convert.ToDateTime(TxtFromDate.Text) < Convert.ToDateTime(TxtToDate.Text))
                //            {
                //                flag = true;
                //            }
                //            else
                //            {
                //                Obj_Comm.ShowPopUpMsg("From Date Must Be Less Than To Date...", this.Page);
                //            }
                //        }
                //        else
                //        {
                //            Obj_Comm.ShowPopUpMsg("Please Check Date..", this.Page);
                //        }
                //    }
                //    else
                //    {
                //        Obj_Comm.ShowPopUpMsg("Please Select Date..", this.Page);
                //    }
                //}
                //else
                //{
                //    flag = true;
                //}
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
                    //if (!Session["UserRole"].Equals("Administrator"))
                    //{
                        //Checking Right of users=======

                        System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                        System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                        dsChkUserRight1 = (DataSet)Session["DataSet"];

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Inventory Report'");
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
    protected void ImgBtnExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (!TxtForMonth.Text.Equals(""))
            {
                StrCondition += " and RC.RequisitionDate Between '" + Convert.ToDateTime(TxtForMonth.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtForMonth.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (TxtForMonth.Text.Equals(""))
            {
                StrCondition += " and RC.RequisitionDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlRequisitionNo.SelectedValue) > 0)
            {
                StrCondition += " and RC.RequisitionCafeId = " + Convert.ToInt32(ddlRequisitionNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlInwardNo.SelectedValue) > 0)
            {
                StrCondition += " and OUR.OutwardId = " + Convert.ToInt32(ddlInwardNo.SelectedValue);
            }
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                StrCondition = StrCondition + " AND RC.CafeteriaId=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            DS = Obj_Reports.MonthReportReport(StrCondition, out StrError);
            DS.Tables[0].Columns[1].ColumnName = "Requisition Date";
            DS.Tables[0].Columns[4].ColumnName = "Assign Date";
            DS.Tables[0].Columns[5].ColumnName = "Particular";
            DS.Tables[0].Columns[6].ColumnName = "Category";
            DS.Tables[0].Columns[7].ColumnName = "Requisition_Qty";
            DS.Tables[0].Columns[8].ColumnName = "Assign_Qty";
            DS.Tables[0].Columns[9].ColumnName = "Pending_Qty";
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GridView GridExp = new GridView();
                GridExp.DataSource = DS.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("MonthlyReport"+"_"+DateTime.Now.ToString("dd-MMM-yyyy")+".xls", GridExp);
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

    //========Important Required for Excel Export Must=============
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    //========Important Required for Excel Export Must=============

    protected void GridDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.GridDetails.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;
            if (!TxtForMonth.Text.Equals(""))
            {
                StrCondition += " and RC.RequisitionDate Between '" + Convert.ToDateTime(TxtForMonth.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtForMonth.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (TxtForMonth.Text.Equals(""))
            {
                StrCondition += " and RC.RequisitionDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlRequisitionNo.SelectedValue) > 0)
            {
                StrCondition += " and RC.RequisitionCafeId = " + Convert.ToInt32(ddlRequisitionNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlInwardNo.SelectedValue) > 0)
            {
                StrCondition += " and OUR.OutwardId = " + Convert.ToInt32(ddlInwardNo.SelectedValue);
            }
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                StrCondition = StrCondition + " AND RC.CafeteriaId=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            DS = Obj_Reports.MonthReportReport(StrCondition, out StrError);

            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                ImgBtnPrint.Visible = true;
                ImgBtnExcel.Visible = true;
                ViewState["IScancel"] = DS.Tables[0];
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
                MakeRed();
                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + "Record Found";
                DS = null;

                //ScriptManager.RegisterStartupScript(this,this.GetType(),"starScript","")
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
}
