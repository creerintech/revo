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
public partial class Reports_MaterialTransferOutSideDetails : System.Web.UI.Page
{
    #region[Private Variables]
        DMTransferLocationOutside Obj_TransferLocation = new DMTransferLocationOutside();
        TransferLocationOutside Entity_TransferLocation = new TransferLocationOutside();
        CommanFunction Obj_Comm = new CommanFunction();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        DataSet DS = new DataSet();
        private static bool FlagPrint = false;
        public static decimal SubTotal = 0, Rate = 0;
    #endregion

    #region[User Defined Function]
    private void SetInitialRow()
    {
        DataTable Dt = new DataTable();
        DataRow dr;

        Dt.Columns.Add("TransferNo", typeof(string));
        Dt.Columns.Add("Date", typeof(string));
        Dt.Columns.Add("Employee", typeof(string));
        Dt.Columns.Add("Category", typeof(string));
        Dt.Columns.Add("ItemName", typeof(string));
        Dt.Columns.Add("FromLocation", typeof(string));
        Dt.Columns.Add("ToLocation", typeof(string));
        Dt.Columns.Add("Qty", typeof(string));
        Dt.Columns.Add("rate", typeof(decimal));
        Dt.Columns.Add("Notes", typeof(string));
        Dt.Columns.Add("rate1", typeof(decimal));
        Dt.Columns.Add("type", typeof(string));
        Dt.Columns.Add("Unit", typeof(string));

        dr = Dt.NewRow();
        dr["TransferNo"] = "";
        dr["Date"] = "";
        dr["Employee"] = "";
        dr["Category"] = "";
        dr["ItemName"] = "";
        dr["FromLocation"] = "";
        dr["ToLocation"] = "";
        dr["Qty"] = "";
        dr["rate"] = 0.00;
        dr["rate1"] = 0.00;
        dr["Notes"] = "";

        dr["type"] = "";
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
        lblCount.Text = "";
        ChkFromDate.Checked = true;
        ddlTransferNo.SelectedIndex = ddlFromLocation.SelectedIndex = ddlEmployee.SelectedIndex = ddlToLocation.SelectedIndex = ddlCategory.SelectedIndex = ddlItems.SelectedIndex = 0;
        SetInitialRow();
        SubTotal = 0;
        // ReportGrid();

    }
    public void ReportGrid()
    {
        StrCondition = string.Empty;

        try
        {
            if (ChkFromDate.Checked)
            {
                StrCondition += " and TransLocOutsideMaster.Date between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and'" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and TransLocOutsideMaster.Date between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and'" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlTransferNo.SelectedValue) > 0)
            {
                StrCondition += " and TransLocOutsideMaster.TransId = " + Convert.ToInt32(ddlTransferNo.SelectedValue);
            }
            if ((Convert.ToInt32(ddlFromLocation.SelectedValue) > 0) && (Convert.ToInt32(ddlToLocation.SelectedValue) <= 0))
            {
                StrCondition += " and TransLocOutsideDetails.TransFrom = " + Convert.ToInt32(ddlFromLocation.SelectedItem);//instead of value
            }
            if ((Convert.ToInt32(ddlToLocation.SelectedValue) > 0)&& (Convert.ToInt32(ddlFromLocation.SelectedValue) <= 0))
            {
                StrCondition += " and TransLocOutsideDetails.TransTo= " + Convert.ToInt32(ddlToLocation.SelectedValue);
            }
            if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
            {
                StrCondition += " and TransLocOutsideMaster.TransBy= " + Convert.ToInt32(ddlEmployee.SelectedValue);
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and TransLocOutsideDetails.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
            {
                StrCondition += " and TransLocOutsideDetails.ItemId = " + Convert.ToInt32(ddlItems.SelectedValue);
            }
            if ((Convert.ToInt32(ddlToLocation.SelectedValue) > 0) && (Convert.ToInt32(ddlFromLocation.SelectedValue) > 0))
            {
                StrCondition += " and TransLocOutsideDetails.TransFrom='" + Convert.ToInt32(ddlFromLocation.SelectedItem) + "'and TransferLocationDetail.TransTo=" + Convert.ToInt32(ddlToLocation.SelectedValue);
            }
            if ((RdoType.SelectedValue) != "A")
            {
                StrCondition += " and TransLocOutsideMaster.Type = '" + (RdoType.SelectedValue) + "'";
            }
            SubTotal = 0;
            DS = Obj_TransferLocation.GetTransferForDetails(StrCondition, out StrError, 1);

            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                if (!FlagPrint)
                  ImgBtnPrint.Visible = true;
                if (!FlagPrint)
                  ImgBtnExcel.Visible = true;
                // ViewState["IScancel"] = DS.Tables[0];
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();

                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + " Record Found";
                DS = null;

                //ScriptManager.RegisterStartupScript(this,this.GetType(),"starScript","")
            }
            else
            {
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + " Record Found";
                //lblCount.Text = "";
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
            DS = Obj_TransferLocation.FillDamageComboForReport(out StrError);
            {
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        ddlTransferNo.DataSource = DS.Tables[0];
                        ddlTransferNo.DataTextField = "TransNo";
                        ddlTransferNo.DataValueField = "TransId";
                        ddlTransferNo.DataBind();
                    }
                    if (DS.Tables[2].Rows.Count > 0)
                    {
                        ddlFromLocation.DataSource = DS.Tables[2];
                        ddlFromLocation.DataTextField = "Location";
                        ddlFromLocation.DataValueField = "StockLocationID";
                        ddlFromLocation.DataBind();
                    }
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        ddlEmployee.DataSource = DS.Tables[1];
                        ddlEmployee.DataTextField = "UserName";
                        ddlEmployee.DataValueField = "UserId";
                        ddlEmployee.DataBind();
                    }
                    if (DS.Tables[2].Rows.Count > 0)
                    {
                        ddlToLocation.DataSource = DS.Tables[2];
                        ddlToLocation.DataTextField = "Location";
                        ddlToLocation.DataValueField = "StockLocationID";
                        ddlToLocation.DataBind();
                    }
                    if (DS.Tables[3].Rows.Count > 0)
                    {
                        ddlCategory.DataSource = DS.Tables[3];
                        ddlCategory.DataTextField = "CategoryName";
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
                if (!Session["UserRole"].Equals("Administrator"))
                {
                    //Checking Right of users=======

                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='TransLocOutsideDetails'");
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
    protected void ChkFromDate_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkFromDate.Checked == true)
        {
            TxtFromDate.Enabled = TxtToDate.Enabled = true;
            TxtFromDate.Text = TxtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
        else
        {
            TxtFromDate.Enabled = TxtToDate.Enabled = false;
            TxtFromDate.Text= TxtToDate.Text= "";
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
                StrCondition += " and TransLocOutsideMaster.Date between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and TransLocOutsideMaster.Date between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlTransferNo.SelectedValue) > 0)
            {
                StrCondition += " and TransLocOutsideMaster.TransId = " + Convert.ToInt32(ddlTransferNo.SelectedValue);
            }
            if ((Convert.ToInt32(ddlFromLocation.SelectedValue) > 0) && (Convert.ToInt32(ddlToLocation.SelectedValue) <= 0))
            {
                StrCondition += " and TransLocOutsideDetails.TransFrom = " + Convert.ToInt32(ddlFromLocation.SelectedItem);//instead of value
            }
            if ((Convert.ToInt32(ddlToLocation.SelectedValue) > 0) && (Convert.ToInt32(ddlFromLocation.SelectedValue) <= 0))
            {
                StrCondition += " and TransLocOutsideDetails.TransTo= " + Convert.ToInt32(ddlToLocation.SelectedValue);
            }
            if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
            {
                StrCondition += " and TransLocOutsideMaster.TransBy= " + Convert.ToInt32(ddlEmployee.SelectedValue);
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and TransLocOutsideDetails.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
            {
                StrCondition += " and TransLocOutsideDetails.ItemId = " + Convert.ToInt32(ddlItems.SelectedValue);
            }
            if ((Convert.ToInt32(ddlToLocation.SelectedValue) > 0) && (Convert.ToInt32(ddlFromLocation.SelectedValue) > 0))
            {
                StrCondition += " and TransLocOutsideDetails.TransFrom='" + Convert.ToInt32(ddlFromLocation.SelectedItem) + "' and TransLocOutsideDetails.TransTo=" + Convert.ToInt32(ddlToLocation.SelectedValue);
            }
            if ((RdoType.SelectedValue) != "A")
            {
                StrCondition += " and TransLocOutsideMaster.Type = '" + (RdoType.SelectedValue) + "'";
            }
            SubTotal = 0;
            DS = Obj_TransferLocation.GetTransferForDetails(StrCondition, out StrError, 2);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GridView GridExp = new GridView();
                GridExp.DataSource = DS.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("Material Transfer Outside Details.xls", GridExp);
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("No Data Found To Export...!", this.Page);
                DS.Dispose();
                GridDetails.DataSource = null;
                GridDetails.DataBind();
                SetInitialRow();
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
            if (ChkFromDate.Checked)
            {
                StrCondition += " and TransLocOutsideMaster.Date between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and TransLocOutsideMaster.Date between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlTransferNo.SelectedValue) > 0)
            {
                StrCondition += " and TransLocOutsideMaster.TransId = " + Convert.ToInt32(ddlTransferNo.SelectedValue);
            }
            if ((Convert.ToInt32(ddlFromLocation.SelectedValue) > 0) && (Convert.ToInt32(ddlToLocation.SelectedValue) <= 0))
            {
                StrCondition += " and TransLocOutsideDetails.TransFrom = " + Convert.ToInt32(ddlFromLocation.SelectedItem);//instead of value
            }
            if ((Convert.ToInt32(ddlToLocation.SelectedValue) > 0) && (Convert.ToInt32(ddlFromLocation.SelectedValue) <= 0))
            {
                StrCondition += " and TransLocOutsideDetails.TransTo= " + Convert.ToInt32(ddlToLocation.SelectedValue);
            }
            if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
            {
                StrCondition += " and TransLocOutsideMaster.TransBy= " + Convert.ToInt32(ddlEmployee.SelectedValue);
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and TransLocOutsideDetails.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
            {
                StrCondition += " and TransLocOutsideDetails.ItemId = " + Convert.ToInt32(ddlItems.SelectedValue);
            }
            if ((Convert.ToInt32(ddlToLocation.SelectedValue) > 0) && (Convert.ToInt32(ddlFromLocation.SelectedValue) > 0))
            {
                StrCondition += " and TransLocOutsideDetails.TransFrom='" + Convert.ToInt32(ddlFromLocation.SelectedItem) + "' and TransLocOutsideDetails.TransTo=" + Convert.ToInt32(ddlToLocation.SelectedValue);
            }
            if ((RdoType.SelectedValue) != "A")
            {
                StrCondition += " and TransLocOutsideMaster.Type = '" + (RdoType.SelectedValue) + "'";
            }
            SubTotal = 0;
            DS = Obj_TransferLocation.GetTransferForDetails(StrCondition, out StrError, 1);

            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                ImgBtnPrint.Visible = true;
                ImgBtnExcel.Visible = true;
                // ViewState["IScancel"] = DS.Tables[0];
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();

                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + " Record Found";
                DS = null;

                //ScriptManager.RegisterStartupScript(this,this.GetType(),"starScript","")
            }
            else
            {
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + " Record Found";
                //lblCount.Text = "";
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
    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //lblNetAmount.Text = (Convert.ToDecimal(lblNetAmount.Text) + Convert.ToDecimal(e.Row.Cells[11].Text)).ToString();
                SubTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "rate"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[11].Text = "Total :  ";
                e.Row.Cells[12].Text = SubTotal.ToString("#0.00");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
