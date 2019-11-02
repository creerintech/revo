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

public partial class Reports_MaterialReqSummary : System.Web.UI.Page
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
        private string ReqStatus = string.Empty;
    #endregion

    #region[User Defined Function]
    private void SetInitialRow()
    {
        DataTable Dt = new DataTable();
        DataRow dr;

        Dt.Columns.Add("#", typeof(int));
        Dt.Columns.Add("RequisitionNo", typeof(string));
        Dt.Columns.Add("RequisitionDate", typeof(string));
        Dt.Columns.Add("Cafeteria", typeof(string));
        Dt.Columns.Add("EmpName", typeof(string));
        Dt.Columns.Add("Amount", typeof(decimal));
        Dt.Columns.Add("ReqStatus", typeof(string));
        Dt.Columns.Add("GeneratedTime", typeof(string));
        Dt.Columns.Add("ApprovedTime", typeof(string));
        Dt.Columns.Add("AuthorizedTime", typeof(string));
        Dt.Columns.Add("PONo", typeof(string));
        Dt.Columns.Add("SuplierName", typeof(string));
        Dt.Columns.Add("SuplierInfo", typeof(string));

        dr = Dt.NewRow();
        dr["#"] = 0;
        dr["RequisitionNo"] = "";
        dr["RequisitionDate"] = "";
        dr["Cafeteria"] = "";
        dr["EmpName"] = "";
        dr["Amount"] = 0;
        dr["ReqStatus"] = "";
        dr["GeneratedTime"] = "";
        dr["ApprovedTime"] = "";
        dr["AuthorizedTime"] = "";
        dr["PONo"] = "";
        dr["SuplierName"] = "";
        dr["SuplierInfo"] = "";

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
                    if (((dttable.Rows[i][0].ToString()) == (GridDetails.Rows[j].Cells[2].Text)) && (Convert.ToBoolean(dttable.Rows[i][5].ToString()) == true))
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
        ddlRequisitionNo.SelectedIndex = ddlTemplateNo.SelectedIndex =ddlEmployee.SelectedIndex= 0;
        SetInitialRow();
        RdoType.SelectedValue = "All";
        ddlRequisitionNo.Focus();
        ddlRequisitionNo.SelectedValue = "0";
    }

    public void ReportGrid()
    {
        StrCondition = string.Empty;
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
                StrCondition += " and RC.RequisitionCafeId = " + Convert.ToInt32(ddlRequisitionNo.SelectedValue);
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
            #region
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
            //}
            if (RdoType.SelectedValue == "All")
            {
                StrCondition += "And RC.ReqStatus in ('Generated','Approved','Authorised')";
            }
            if (RdoType.SelectedValue == "Generated")
            {
                StrCondition += " And RC.ReqStatus = " + "'" + RdoType.SelectedValue + "'";
            }
            if (RdoType.SelectedValue == "Approved")
            {
                StrCondition += " And RC.ReqStatus = " + "'" + RdoType.SelectedValue + "'";
            }
            if (RdoType.SelectedValue == "Authorised")
            {
                StrCondition += " And RC.ReqStatus = " + "'" + (RdoType.SelectedValue)+ "'";
            }
            DS = Obj_RequisitionCafeteria.GetRequisationForPrintSummary(StrCondition, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                if (!FlagPrint)
                  ImgBtnPrint.Visible = true;
                if (!FlagPrint)
                  ImgBtnExcel.Visible = true;
                ViewState["IScancel"] = DS.Tables[0];
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
                MakeRed();
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
            //  //  COND = COND + " AND USD.FK_UserId=" + Convert.ToInt32(Session["UserID"].ToString());
            //}
            DS = Obj_RequisitionCafeteria.FillRequisitionNoComboForReport(Convert.ToInt32(Session["UserID"]),out StrError);
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
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        ddlTemplateNo.DataSource = DS.Tables[1];
                        ddlTemplateNo.DataTextField = "Location";
                        ddlTemplateNo.DataValueField = "StockLocationID";
                        ddlTemplateNo.DataBind();
                    }
                    if (DS.Tables[4].Rows.Count > 0)
                    {
                        ddlEmployee.DataSource = DS.Tables[4];
                        ddlEmployee.DataTextField = "UserName";
                        ddlEmployee.DataValueField = "UserId";
                        ddlEmployee.DataBind();
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
                StrCondition += " and RC.RequisitionCafeId = " + Convert.ToInt32(ddlRequisitionNo.SelectedValue);
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

            DS = Obj_RequisitionCafeteria.GetRequisationForPrintSummary(StrCondition, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GridView GridExp = new GridView();
                GridExp.DataSource = DS.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("Material Requisition Report.xls", GridExp);
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
                StrCondition += " and RC.RequisitionCafeId = " + Convert.ToInt32(ddlRequisitionNo.SelectedValue);
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
            DS = Obj_RequisitionCafeteria.GetRequisationForPrintSummary(StrCondition, out StrError);

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
    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                SubTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total :  ";
                e.Row.Cells[5].Text = SubTotal.ToString();
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
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            ddlRequisitionNo.DataSource = DS.Tables[0];
                            ddlRequisitionNo.DataTextField = "RequisitionNo";
                            ddlRequisitionNo.DataValueField = "RequisitionCafeId";
                            ddlRequisitionNo.DataBind();
                        }
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
}
