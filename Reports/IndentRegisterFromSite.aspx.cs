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
using MayurInventory.DALSQLHelper;
using MayurInventory.DataModel;
using MayurInventory.DB;
using MayurInventory.EntityClass;
using MayurInventory.Utility;
using System.Threading;
public partial class Reports_IndentRegisterFromSite : System.Web.UI.Page
{
    #region[Private Variables]
    DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
    RequisitionCafeteria Entity_RequisitionCafeteria = new RequisitionCafeteria();
    CommanFunction Obj_Comm = new CommanFunction();
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    DataSet DS = new DataSet();
    private static bool FlagPrint = false;
    decimal SubTotal = 0;

    #endregion

    #region[User Defined Function]
    private void SetInitialRow()
    {
        DataTable Dt = new DataTable();
        DataRow dr;
        Dt.Columns.Add("#", typeof(int));
        Dt.Columns.Add("RequisitionNo", typeof(string));
        Dt.Columns.Add("Date", typeof(string));
        Dt.Columns.Add("Cafeteria", typeof(string));
        Dt.Columns.Add("Employee", typeof(string));
        Dt.Columns.Add("Amount", typeof(string));
        Dt.Columns.Add("Category", typeof(string));
        Dt.Columns.Add("SubCategory", typeof(string));
        Dt.Columns.Add("Items", typeof(string));
        Dt.Columns.Add("ItemDesc", typeof(string));
        Dt.Columns.Add("RemarkForPO", typeof(string));
        Dt.Columns.Add("MinStockLevel", typeof(string));
        Dt.Columns.Add("Qty", typeof(string));
        Dt.Columns.Add("Suplier", typeof(string));
        Dt.Columns.Add("Rate", typeof(string));
        Dt.Columns.Add("Unit", typeof(string));
        Dt.Columns.Add("PONo", typeof(string));
        Dt.Columns.Add("POQty", typeof(string));
        Dt.Columns.Add("SuplierName", typeof(string));
        Dt.Columns.Add("SuplierInfo", typeof(string));

        dr = Dt.NewRow();
        dr["#"] = 0;
        dr["RequisitionNo"] = "";
        dr["Date"] = "";
        dr["Cafeteria"] = "";
        dr["Employee"] = "";
        dr["Amount"] = "0.00";
        dr["Items"] = "";
        dr["ItemDesc"] = "";
        dr["RemarkForPO"] = "";
        dr["MinStockLevel"] = "";
        dr["Qty"] = "";
        dr["Suplier"] = "";
        dr["Rate"] = "";
        dr["Category"] = "";
        dr["SubCategory"] = "";
        dr["Unit"] = "";

        Dt.Rows.Add(dr);

        ViewState["CurrentTable"] = Dt;
        GridDetails.DataSource = Dt;
        GridDetails.DataBind();
    }
    public void MakeEmptyForm()
    {
        if (!FlagPrint)
            ImgBtnPrint.Visible = true;
        if (!FlagPrint)
            ImgBtnExcel.Visible = true;
        TxtFromDate.Enabled = TxtToDate.Enabled = true;
        TxtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
        TxtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        lblCount.Text = "";
        lblNetAmount.Text = "0";
        ChkFromDate.Checked = true;
        ddlTemplateNo.SelectedIndex = ddlCategory.SelectedIndex = ddlItems.SelectedIndex = 0;
        ddlEmployee.SelectedIndex = ddlUnit.SelectedIndex =  ddlSubCategory.SelectedIndex = 0;
        SetInitialRow();
        RdoType.SelectedValue = "All";
        // ReportGrid();
        ddlCategory.Focus();
    }
    public void ReportGrid()
    {
        StrCondition = string.Empty;
        try
        {
            if (ChkFromDate.Checked)//and RC.RequisitionCafeId=22 and RC.CafeteriaId=4 and RC.RequisitionDate
            {
                StrCondition += " and F.RequisitionDate Between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and F.RequisitionDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
           
            #region[EMPLOYEE]
            if (ddlEmployee.Items.Count == 0)
            {
                StrCondition = StrCondition + " and F.UserId =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and F.UserId =" + Convert.ToInt32(ddlEmployee.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlEmployee.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " and (F.UserId = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " or F.UserId = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
                            }
                        }
                        if (i == ddlEmployee.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
            }
            #endregion
            #region[LOCATION]


            if (Convert.ToInt32(ddlTemplateNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and F.CafeteriaId=" + Convert.ToInt32(ddlTemplateNo.SelectedValue);
            }
            else
            {
                for (int i = 1; i < ddlTemplateNo.Items.Count; i++)
                {
                    if (i == 1)
                    {
                        if (Convert.ToInt32(ddlTemplateNo.Items[i].Value) != 0)
                        {
                            StrCondition += " and (F.CafeteriaId = " + Convert.ToInt32(ddlTemplateNo.Items[i].Value);
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(ddlTemplateNo.Items[i].Value) != 0)
                        {
                            StrCondition += " or F.CafeteriaId = " + Convert.ToInt32(ddlTemplateNo.Items[i].Value);
                        }
                    }
                    if (i == ddlTemplateNo.Items.Count - 1)
                    {
                        if (Convert.ToInt32(ddlTemplateNo.Items[i].Value) != 0)
                        {
                            StrCondition += " )";
                        }
                    }

                }
            }

            #endregion
            if (Convert.ToInt32(ddlUnit.SelectedValue) > 0)
            {
                StrCondition += " and F.UnitId = " + Convert.ToInt32(ddlUnit.SelectedValue);
            }

            if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
            {
                StrCondition += " and F.ItemId= " + Convert.ToInt32(ddlItems.SelectedValue);
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and F.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                StrCondition += " and F.SubCategoryId = " + Convert.ToInt32(ddlSubCategory.SelectedValue);
            }
            if (RdoType.SelectedValue == "All")
            {
                StrCondition += "And F.ReqStatus in ('Generated','Approved','Authorised')";
            }
            if (RdoType.SelectedValue == "Generated")
            {
                StrCondition += " And F.ReqStatus = " + "'" + RdoType.SelectedValue + "'";
            }
            if (RdoType.SelectedValue == "Approved")
            {
                StrCondition += " And F.ReqStatus = " + "'" + RdoType.SelectedValue + "'";
            }
            if (RdoType.SelectedValue == "Authorised")
            {
                StrCondition += " And F.ReqStatus = " + "'" + (RdoType.SelectedValue) + "'";
            }

            DS = Obj_RequisitionCafeteria.GetRequisitionDetailsITEMPENDINGForReport(StrCondition, out StrError, 1);
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
                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + " Record Found";
                DS = null;
            }
            else
            {
                lblCount.Text = "0 Record Found";
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
            
            DS = Obj_RequisitionCafeteria.FillRequisitionNoComboForReport(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
            {
                if (DS.Tables.Count > 0)
                {
                    
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        ddlTemplateNo.DataSource = DS.Tables[1];
                        ddlTemplateNo.DataTextField = "Location";
                        ddlTemplateNo.DataValueField = "StockLocationID";
                        ddlTemplateNo.DataBind();
                    }
                    if (DS.Tables[2].Rows.Count > 0)
                    {
                        ddlItems.DataSource = DS.Tables[2];
                        ddlItems.DataTextField = "ItemName";
                        ddlItems.DataValueField = "ItemId";
                        ddlItems.DataBind();
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
                        ddlEmployee.DataSource = DS.Tables[4];
                        ddlEmployee.DataTextField = "UserName";
                        ddlEmployee.DataValueField = "UserId";
                        ddlEmployee.DataBind();
                    }
                    if (DS.Tables[5].Rows.Count > 0)
                    {
                        ddlUnit.DataSource = DS.Tables[5];
                        ddlUnit.DataTextField = "Unit";
                        ddlUnit.DataValueField = "UnitId";
                        ddlUnit.DataBind();
                    }
                   
                    if (DS.Tables[7].Rows.Count > 0)
                    {
                        ddlSubCategory.DataSource = DS.Tables[7];
                        ddlSubCategory.DataTextField = "SubCategory";
                        ddlSubCategory.DataValueField = "SubCategoryId";
                        ddlSubCategory.DataBind();
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
    public void MakeRed()
    {
        if (ViewState["IScancel"] != null)
        {
            DataTable dttable = new DataTable();
            dttable = (DataTable)ViewState["IScancel"];
            int temp = GridDetails.Rows.Count;
            for (int i = 0; i < temp; i++)
            {
                for (int j = 0; j < GridDetails.Rows.Count; j++)
                {
                    if (((dttable.Rows[i][0].ToString()) == (GridDetails.Rows[j].Cells[1].Text)) && (Convert.ToBoolean(dttable.Rows[i][11].ToString()) == true))
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
            lblNetAmount.Text = "0";
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
            if (ChkFromDate.Checked)//and RC.RequisitionCafeId=22 and RC.CafeteriaId=4 and RC.RequisitionDate
            {
                StrCondition += " and F.RequisitionDate Between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and F.RequisitionDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }

            #region[EMPLOYEE]
            if (ddlEmployee.Items.Count == 0)
            {
                StrCondition = StrCondition + " and F.UserId =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and F.UserId =" + Convert.ToInt32(ddlEmployee.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlEmployee.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " and (F.UserId = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " or F.UserId = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
                            }
                        }
                        if (i == ddlEmployee.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
            }
            #endregion
            #region[LOCATION]


            if (Convert.ToInt32(ddlTemplateNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and F.CafeteriaId=" + Convert.ToInt32(ddlTemplateNo.SelectedValue);
            }
            else
            {
                for (int i = 1; i < ddlTemplateNo.Items.Count; i++)
                {
                    if (i == 1)
                    {
                        if (Convert.ToInt32(ddlTemplateNo.Items[i].Value) != 0)
                        {
                            StrCondition += " and (F.CafeteriaId = " + Convert.ToInt32(ddlTemplateNo.Items[i].Value);
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(ddlTemplateNo.Items[i].Value) != 0)
                        {
                            StrCondition += " or F.CafeteriaId = " + Convert.ToInt32(ddlTemplateNo.Items[i].Value);
                        }
                    }
                    if (i == ddlTemplateNo.Items.Count - 1)
                    {
                        if (Convert.ToInt32(ddlTemplateNo.Items[i].Value) != 0)
                        {
                            StrCondition += " )";
                        }
                    }

                }
            }

            #endregion
            if (Convert.ToInt32(ddlUnit.SelectedValue) > 0)
            {
                StrCondition += " and F.UnitId = " + Convert.ToInt32(ddlUnit.SelectedValue);
            }

            if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
            {
                StrCondition += " and F.ItemId= " + Convert.ToInt32(ddlItems.SelectedValue);
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and F.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                StrCondition += " and F.SubCategoryId = " + Convert.ToInt32(ddlSubCategory.SelectedValue);
            }
            if (RdoType.SelectedValue == "All")
            {
                StrCondition += "And F.ReqStatus in ('Generated','Approved','Authorised')";
            }
            if (RdoType.SelectedValue == "Generated")
            {
                StrCondition += " And F.ReqStatus = " + "'" + RdoType.SelectedValue + "'";
            }
            if (RdoType.SelectedValue == "Approved")
            {
                StrCondition += " And F.ReqStatus = " + "'" + RdoType.SelectedValue + "'";
            }
            if (RdoType.SelectedValue == "Authorised")
            {
                StrCondition += " And F.ReqStatus = " + "'" + (RdoType.SelectedValue) + "'";
            }

            DS = Obj_RequisitionCafeteria.GetRequisitionDetailsITEMPENDINGForReport(StrCondition, out StrError, 2);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GridView GridExp = new GridView();
                GridExp.DataSource = DS.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("Material Requisition.xls", GridExp);
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
    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }
    protected void GridDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.GridDetails.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
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
           
            #region[EMPLOYEE]
            if (ddlEmployee.Items.Count == 0)
            {
                StrCondition = StrCondition + " and UM.UserId =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and UM.UserId =" + Convert.ToInt32(ddlEmployee.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlEmployee.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " and (UM.UserId = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " or UM.UserId = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
                            }
                        }
                        if (i == ddlEmployee.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
            }
            #endregion
            #region[LOCATION]


            if (Convert.ToInt32(ddlTemplateNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and RC.CafeteriaId=" + Convert.ToInt32(ddlTemplateNo.SelectedValue);
            }
            else
            {
                for (int i = 1; i < ddlTemplateNo.Items.Count; i++)
                {
                    if (i == 1)
                    {
                        if (Convert.ToInt32(ddlTemplateNo.Items[i].Value) != 0)
                        {
                            StrCondition += " and (RC.CafeteriaId = " + Convert.ToInt32(ddlTemplateNo.Items[i].Value);
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(ddlTemplateNo.Items[i].Value) != 0)
                        {
                            StrCondition += " or RC.CafeteriaId = " + Convert.ToInt32(ddlTemplateNo.Items[i].Value);
                        }
                    }
                    if (i == ddlTemplateNo.Items.Count - 1)
                    {
                        if (Convert.ToInt32(ddlTemplateNo.Items[i].Value) != 0)
                        {
                            StrCondition += " )";
                        }
                    }

                }
            }

            #endregion
            if (Convert.ToInt32(ddlUnit.SelectedValue) > 0)
            {
                StrCondition += " and UnM.UnitId = " + Convert.ToInt32(ddlUnit.SelectedValue);
            }

            if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
            {
                StrCondition += " and IM.ItemId= " + Convert.ToInt32(ddlItems.SelectedValue);
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and IC.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                StrCondition += " and SC.SubCategoryId = " + Convert.ToInt32(ddlSubCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                StrCondition += " and((dbo.ConvertUnit_Calculation(RCD.ItemId,RCD.Qty,RCD.UnitConvDtlsId,IM.UnitId,RCD.ItemDetailsId))-(dbo.ConvertUnit_Calculation(RCD.ItemId,ISNULL(POD.Qty,0),ISNULL(POD.FK_UnitConvDtlsId,IM.UnitId),IM.UnitId,POD.FK_ItemDtlsId)))>0";
            }

            DS = Obj_RequisitionCafeteria.GetRequisitionDetailsForReport(StrCondition, out StrError, 1);

            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                ImgBtnPrint.Visible = true;
                ImgBtnExcel.Visible = true;
                ViewState["IScancel"] = DS.Tables[0];
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
                MakeRed();
                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + " Record Found";
                DS = null;
                //ScriptManager.RegisterStartupScript(this,this.GetType(),"starScript","")
            }
            else
            {
                lblCount.Text = "0 Record Found";
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
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
            {
                DS = Obj_RequisitionCafeteria.FillRequisitionNoComboForReport(Convert.ToInt32(ddlEmployee.SelectedValue), out StrError);
                {
                    if (DS.Tables.Count > 0)
                    {
                       
                        if (DS.Tables[1].Rows.Count > 0)
                        {
                            ddlTemplateNo.DataSource = DS.Tables[1];
                            ddlTemplateNo.DataTextField = "Location";
                            ddlTemplateNo.DataValueField = "StockLocationID";
                            ddlTemplateNo.DataBind();
                        }

                        else
                        {
                            DS = null;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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
                        ddlItems.DataSource = DS.Tables[0];
                        ddlItems.DataTextField = "ItemName";
                        ddlItems.DataValueField = "ItemId";
                        ddlItems.DataBind();
                    }
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        ddlSubCategory.DataSource = DS.Tables[1];
                        ddlSubCategory.DataTextField = "Name";
                        ddlSubCategory.DataValueField = "Id";
                        ddlSubCategory.DataBind();
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

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string COND = string.Empty;
            DMFILTER DMFILTER = new DMFILTER();
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                COND = COND + " AND CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                COND = COND + " AND SubcategoryId=" + Convert.ToInt32(ddlSubCategory.SelectedValue);
            }
            DS = DMFILTER.FillReportComboCategoryWise(2, 0, COND, out StrError);
            {
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        ddlItems.DataSource = DS.Tables[0];
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
    
}
