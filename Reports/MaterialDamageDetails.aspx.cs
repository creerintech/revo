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

using MayurInventory.DALSQLHelper;
using MayurInventory.DataModel;
using MayurInventory.DB;
using MayurInventory.EntityClass;
using MayurInventory.Utility;
public partial class Reports_MaterialDamageDetails : System.Web.UI.Page
{
    #region[Private Variables]
        DMDamage Obj_Damage = new DMDamage();
        Damage Entity_Damage = new Damage();
        CommanFunction Obj_Comm = new CommanFunction();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        DataSet DS = new DataSet();
        private static bool FlagPrint = false;
        decimal SubTotal = 0, Rate=0;
    #endregion

    #region[User Defined Function]
    private void SetInitialRow()
    {
        DataTable Dt = new DataTable();
        DataRow dr;

        Dt.Columns.Add("DamageNo", typeof(string));
        Dt.Columns.Add("Date", typeof(string));
        Dt.Columns.Add("Employee", typeof(string));
        Dt.Columns.Add("InwardNo", typeof(string));

        Dt.Columns.Add("Unit", typeof(string));
        Dt.Columns.Add("type", typeof(string));

        Dt.Columns.Add("InwardDate", typeof(string));
        Dt.Columns.Add("PONO", typeof(string));
        Dt.Columns.Add("Suplier", typeof(string));
        Dt.Columns.Add("Category", typeof(string));
        Dt.Columns.Add("ItemName", typeof(string));
        Dt.Columns.Add("ItemDesc", typeof(string));
        Dt.Columns.Add("DamageQty", typeof(string));
        Dt.Columns.Add("Reason", typeof(string));
        Dt.Columns.Add("Rate", typeof(decimal));
        Dt.Columns.Add("Amount", typeof(decimal));
        dr = Dt.NewRow();
        dr["DamageNo"] = "";
        dr["Date"] = "";
        dr["Employee"] = "";

        dr["Unit"] = "";
        dr["type"] = "";

        dr["InwardNo"] = "";
        dr["InwardDate"] = "";
        dr["PONO"] = "";
        dr["Suplier"] = "";
        dr["Category"] = "";
        dr["ItemName"] = "";
        dr["ItemDesc"] = "";
        dr["DamageQty"] = "";
        dr["Reason"] = "";
        dr["Rate"] = 0.00;
        dr["Amount"] = 0.00;
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
        lblNetAmount.Text = "0";
        lblRate.Text = "0";
        ChkFromDate.Checked = true;
        ddlsubcategory.SelectedIndex= ddlDamageNo.SelectedIndex = ddlLocation.SelectedIndex = ddlEmployee.SelectedIndex = ddlInward.SelectedIndex = ddlCategory.SelectedIndex = ddlItems.SelectedIndex = 0;
        SetInitialRow();
        // ReportGrid();
        ddlInward.SelectedValue = "0";

    }
    public void ReportGrid()
    {
        StrCondition = string.Empty;
        try
        {
            if (ChkFromDate.Checked)
            {
                StrCondition += " and DamageMaster.DamageDate between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and DamageMaster.DamageDate between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlDamageNo.SelectedValue) > 0)
            {
                StrCondition += " and DamageMaster.DamageId = " + Convert.ToInt32(ddlDamageNo.SelectedValue);
            }
            #region[EMPLOYEE]
            if (ddlEmployee.Items.Count == 0)
            {
                StrCondition = StrCondition + " and DamageMaster.PreparedBy =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and DamageMaster.PreparedBy =" + Convert.ToInt32(ddlEmployee.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlEmployee.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " and (DamageMaster.PreparedBy = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " or DamageMaster.PreparedBy = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
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

                if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and StockLocation.StockLocationID=" + Convert.ToInt32(ddlLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (StockLocation.StockLocationID = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or  StockLocation.StockLocationID = " + Convert.ToInt32(ddlLocation.Items[i].Value);
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
            if (Convert.ToInt32(ddlInward.SelectedValue) > 0)
            {
                StrCondition += " and DamageMaster.InwardId= " + Convert.ToInt32(ddlInward.SelectedValue);
            }
           
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and ItemMaster.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlsubcategory.SelectedValue) > 0)
            {
                StrCondition += " and ItemMaster.SubCategoryId = " + Convert.ToInt32(ddlsubcategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
            {
                StrCondition += " and DamageDetail.ItemId = " + Convert.ToInt32(ddlItems.SelectedValue);
            }
        
            DS = Obj_Damage.GetDamageForReport(StrCondition, out StrError, 1);

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
            string COND = string.Empty;
            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
            //    COND = COND + " AND P.LOC=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}
            DS = Obj_Damage.FillDamageComboForReport(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
            {
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        ddlDamageNo.DataSource = DS.Tables[0];
                        ddlDamageNo.DataTextField = "DamageNo";
                        ddlDamageNo.DataValueField = "DamageId";
                        ddlDamageNo.DataBind();
                    }
                    if (DS.Tables[3].Rows.Count > 0)
                    {
                        ddlLocation.DataSource = DS.Tables[3];
                        ddlLocation.DataTextField = "Location";
                        ddlLocation.DataValueField = "StockLocationID";
                        ddlLocation.DataBind();
                    }
                    if (DS.Tables[2].Rows.Count > 0)
                    {
                        ddlEmployee.DataSource = DS.Tables[2];
                        ddlEmployee.DataTextField = "UserName";
                        ddlEmployee.DataValueField = "UserId";
                        ddlEmployee.DataBind();
                    }
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        ddlInward.DataSource = DS.Tables[1];
                        ddlInward.DataTextField = "InwardNo";
                        ddlInward.DataValueField = "InwardId";
                        ddlInward.DataBind();
                    }
                    if (DS.Tables[4].Rows.Count > 0)
                    {
                        ddlCategory.DataSource = DS.Tables[4];
                        ddlCategory.DataTextField = "CategoryName";
                        ddlCategory.DataValueField = "CategoryId";
                        ddlCategory.DataBind();
                    }
                    if (DS.Tables[5].Rows.Count > 0)
                    {
                        ddlItems.DataSource = DS.Tables[5];
                        ddlItems.DataTextField = "ItemName";
                        ddlItems.DataValueField = "ItemId";
                        ddlItems.DataBind();
                    }
                    if (DS.Tables[6].Rows.Count > 0)
                    {
                        ddlsubcategory.DataSource = DS.Tables[6];
                        ddlsubcategory.DataTextField = "SubCategory";
                        ddlsubcategory.DataValueField = "SubCategoryId";
                        ddlsubcategory.DataBind();
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
               // if (!Session["UserRole"].Equals("Administrator"))
               // {
                    //Checking Right of users=======

                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Damage Report'");
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
              //  }
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
            lblRate.Text = "0";
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
                StrCondition += " and DamageMaster.DamageDate between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and DamageMaster.DamageDate between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlDamageNo.SelectedValue) > 0)
            {
                StrCondition += " and DamageMaster.DamageId = " + Convert.ToInt32(ddlDamageNo.SelectedValue);
            }
            #region[EMPLOYEE]
            if (ddlEmployee.Items.Count == 0)
            {
                StrCondition = StrCondition + " and DamageMaster.PreparedBy =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and DamageMaster.PreparedBy =" + Convert.ToInt32(ddlEmployee.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlEmployee.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " and (DamageMaster.PreparedBy = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " or DamageMaster.PreparedBy = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
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

           
                if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and StockLocation.StockLocationID=" + Convert.ToInt32(ddlLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (StockLocation.StockLocationID = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or  StockLocation.StockLocationID = " + Convert.ToInt32(ddlLocation.Items[i].Value);
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
            if (Convert.ToInt32(ddlInward.SelectedValue) > 0)
            {
                StrCondition += " and DamageMaster.InwardId = " + Convert.ToInt32(ddlInward.SelectedValue);
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and ItemMaster.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
            {
                StrCondition += " and DamageDetail.ItemId = " + Convert.ToInt32(ddlItems.SelectedValue);
            }
            if (Convert.ToInt32(ddlsubcategory.SelectedValue) > 0)
            {
                StrCondition += " and ItemMaster.SubCategoryId = " + Convert.ToInt32(ddlsubcategory.SelectedValue);
            }
           
            DS = Obj_Damage.GetDamageForReport(StrCondition, out StrError, 2);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GridView GridExp = new GridView();
                GridExp.DataSource = DS.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("Material Damage Details.xls", GridExp);
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

    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.DataRow && !string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InwardNo"))))
            //{
            //    if ((Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DamageQty"))) > 0 )
            //    {
            //        //e.Row.ForeColor = Color.Red;
            //        for (int y = 0; y < e.Row.Cells.Count; y++)
            //        {
            //            e.Row.Cells[y].ForeColor = System.Drawing.Color.FromName("Red");
            //        }
            //    }
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lblNetAmount.Text = (Convert.ToDecimal(lblNetAmount.Text) + Convert.ToDecimal(e.Row.Cells[16].Text)).ToString();
                lblRate.Text = (Convert.ToDecimal(lblRate.Text) + Convert.ToDecimal(e.Row.Cells[15].Text)).ToString();
                SubTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
                Rate += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Rate"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[14].Text = "Total :  ";
               // e.Row.Cells[14].Text = Rate.ToString();
                e.Row.Cells[16].Text = SubTotal.ToString();
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

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
                StrCondition += " and DamageMaster.DamageDate between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and DamageMaster.DamageDate between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlDamageNo.SelectedValue) > 0)
            {
                StrCondition += " and DamageMaster.DamageId = " + Convert.ToInt32(ddlDamageNo.SelectedValue);
            }
            #region[EMPLOYEE]
            if (ddlEmployee.Items.Count == 0)
            {
                StrCondition = StrCondition + " and DamageMaster.PreparedBy =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and DamageMaster.PreparedBy =" + Convert.ToInt32(ddlEmployee.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlEmployee.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " and (DamageMaster.PreparedBy = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " or DamageMaster.PreparedBy = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
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

          
                if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and StockLocation.StockLocationID=" + Convert.ToInt32(ddlLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (StockLocation.StockLocationID = " + Convert.ToInt32(ddlLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or  StockLocation.StockLocationID = " + Convert.ToInt32(ddlLocation.Items[i].Value);
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
            if (Convert.ToInt32(ddlInward.SelectedValue) > 0)
            {
                StrCondition += " and DamageMaster.InwardId= " + Convert.ToInt32(ddlInward.SelectedValue);
            }
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                StrCondition += " and ItemMaster.CategoryId = " + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlsubcategory.SelectedValue) > 0)
            {
                StrCondition += " and ItemMaster.SubCategoryId = " + Convert.ToInt32(ddlsubcategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlItems.SelectedValue) > 0)
            {
                StrCondition += " and DamageDetail.ItemId = " + Convert.ToInt32(ddlItems.SelectedValue);
            }
            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
            //    StrCondition = StrCondition + " AND DamageMaster.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}
            DS = Obj_Damage.GetDamageForReport(StrCondition, out StrError, 1);
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
                        ddlsubcategory.DataSource = DS.Tables[1];
                        ddlsubcategory.DataTextField = "Name";
                        ddlsubcategory.DataValueField = "Id";
                        ddlsubcategory.DataBind();
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
    protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string COND = string.Empty;
            DMFILTER DMFILTER = new DMFILTER();
            if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
            {
                COND = COND + " AND CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (Convert.ToInt32(ddlsubcategory.SelectedValue) > 0)
            {
                COND = COND + " AND SubcategoryId=" + Convert.ToInt32(ddlsubcategory.SelectedValue);
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