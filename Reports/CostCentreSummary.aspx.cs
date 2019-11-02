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
        DMMayurInventory_CostCentre Obj_Reports = new DMMayurInventory_CostCentre();
        MayurInventory_CostCentre Entity_Reports = new MayurInventory_CostCentre();
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

            Dt.Columns.Add("Center", typeof(string));
            Dt.Columns.Add("Location", typeof(string));
            Dt.Columns.Add("Date", typeof(string));
            Dt.Columns.Add("AssignNo", typeof(string));
            Dt.Columns.Add("Item", typeof(decimal));
            Dt.Columns.Add("ItemDesc", typeof(string));
            Dt.Columns.Add("Qty", typeof(string));
            Dt.Columns.Add("Rate", typeof(string));
            Dt.Columns.Add("Amount", typeof(string));
            Dt.Columns.Add("Unit", typeof(string));

            dr = Dt.NewRow();

            dr["Center"] = "";
            dr["Location"] = "";
            dr["Date"] = "";
            dr["AssignNo"] = "";
            dr["Item"] = 0;
            dr["ItemDesc"] = "";
            dr["Qty"] = "";
            dr["Rate"] = "";
            dr["Amount"] = "";
            dr["Unit"] = "";

            Dt.Rows.Add(dr);

            ViewState["CurrentTable"] = Dt;
            GridDetails.DataSource = Dt;
            GridDetails.DataBind();
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
            ddlStockNo.SelectedIndex = 0;
            SetInitialRow();
        }

        public void ReportGrid()
        {
            StrCondition = string.Empty;

            try
            {
                if (ChkFromDate.Checked)
                {
                    StrCondition += " and OWR.StockAsOn>= '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and OWR.StockAsOn<='" +
                        Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
                }
                if (!ChkFromDate.Checked)
                {
                    StrCondition += " and OWR.StockAsOn>= '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and OWR.StockAsOn<='" +
                        Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
                }
                //if (Convert.ToInt32(ddlStockNo.SelectedValue) > 0)
                //{
                //    StrCondition += " and OWRD.LocationId = " + Convert.ToInt32(ddlStockNo.SelectedValue);
                //}
                #region[LOCATION]

                if (Convert.ToInt32(ddlStockNo.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " AND OWRD.LocationId=" + Convert.ToInt32(ddlStockNo.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlStockNo.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlStockNo.Items[i].Value) != 0)
                            {
                                StrCondition += " and ( OWRD.LocationId = " + Convert.ToInt32(ddlStockNo.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlStockNo.Items[i].Value) != 0)
                            {
                                StrCondition += " or OWRD.LocationId = " + Convert.ToInt32(ddlStockNo.Items[i].Value);
                            }
                        }
                        if (i == ddlStockNo.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlStockNo.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }

                #endregion

                DS = Obj_Reports.MonthReportReport(StrCondition, out StrError);

                if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    if (!FlagPrint)
                      ImgBtnPrint.Visible = true;
                    if (!FlagPrint)
                      ImgBtnExcel.Visible = true;
                    GridDetails.DataSource = DS.Tables[0];
                    GridDetails.DataBind();
                    GridDetails.FooterRow.Cells[8].Text = "Total :";
                    for (int q = 0; q < GridDetails.Rows.Count; q++)
                    {
                        GridDetails.FooterRow.Cells[9].Text = ((GridDetails.FooterRow.Cells[9].Text.Equals("&nbsp;") ? 0 : Convert.ToDecimal(GridDetails.FooterRow.Cells[9].Text)) + Convert.ToDecimal(GridDetails.Rows[q].Cells[9].Text)).ToString("#0.00");
                    }
                    lblCount.Text = DS.Tables[0].Rows.Count.ToString() + "Record Found";
                    DS = null;
                }
                else
                {
                    GridDetails.DataSource = null;
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

                DS = Obj_Reports.FillReportCombo(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
                {
                    if (DS.Tables.Count > 0)
                    {
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            ddlStockNo.DataSource = DS.Tables[0];
                            ddlStockNo.DataTextField = "Location";
                            ddlStockNo.DataValueField = "StockLocationID";
                            ddlStockNo.DataBind();
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
                            Obj_Comm.ShowPopUpMsg("Please Check Date..", this.Page);
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
                    //if (!Session["UserRole"].Equals("Administrator"))
                    //{
                        //Checking Right of users=======

                        System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                        System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                        dsChkUserRight1 = (DataSet)Session["DataSet"];

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Issue Report'");
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
    protected void ChkFromDate_CheckedChanged(object sender, EventArgs e)
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
            if (ChkFromDate.Checked)
            {
                StrCondition += " and OWR.StockAsOn>= '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and OWR.StockAsOn<='" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and OWR.StockAsOn>= '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and OWR.StockAsOn<='" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            //if (Convert.ToInt32(ddlStockNo.SelectedValue) > 0)
            //{
            //    StrCondition += " and OWRD.LocationId = " + Convert.ToInt32(ddlStockNo.SelectedValue);
            //}
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                StrCondition = StrCondition + " AND OWR.Location =" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            if (Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                if (Convert.ToInt32(ddlStockNo.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and OWR.Location =" + Convert.ToInt32(ddlStockNo.SelectedValue);
                }
            }
            DS = Obj_Reports.MonthReportReport(StrCondition, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GridView GridExp = new GridView();
                GridExp.DataSource = DS.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("Cost_Centre_Wise_Report"+"_"+DateTime.Now.ToString("dd-MMM-yyyy")+".xls", GridExp);
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
            if (ChkFromDate.Checked)
            {
                StrCondition += " and OWR.StockAsOn>= '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and OWR.StockAsOn<='" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and OWR.StockAsOn>= '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and OWR.StockAsOn<='" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            //if (Convert.ToInt32(ddlStockNo.SelectedValue) > 0)
            //{
            //    StrCondition += " and OWRD.LocationId = " + Convert.ToInt32(ddlStockNo.SelectedValue);
            //}
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                StrCondition = StrCondition + " AND OWR.Location =" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            if (Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                if (Convert.ToInt32(ddlStockNo.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and OWR.Location =" + Convert.ToInt32(ddlStockNo.SelectedValue);
                }
            }
            DS = Obj_Reports.MonthReportReport(StrCondition, out StrError);

            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                ImgBtnPrint.Visible = true;
                ImgBtnExcel.Visible = true;
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
           
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
