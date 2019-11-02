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
public partial class Reports_MaterialTransferSummary : System.Web.UI.Page
{
    #region[Private Variables]
        DMTransferLocation Obj_TransferLocation = new DMTransferLocation();
        TransferLocation Entity_TransferLocation = new TransferLocation();
        CommanFunction Obj_Comm = new CommanFunction();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        DataSet DS = new DataSet();
        private static bool FlagPrint = false;
    #endregion
    #region[User Defined Function]
    private void SetInitialRow()
    {
        DataTable Dt = new DataTable();
        DataRow dr;

        Dt.Columns.Add("TransferNo", typeof(string));
        Dt.Columns.Add("Date", typeof(string));
        Dt.Columns.Add("Employee", typeof(string));
        Dt.Columns.Add("FromLocation", typeof(string));
        Dt.Columns.Add("ToLocation", typeof(string));
        Dt.Columns.Add("Notes", typeof(string));
        Dt.Columns.Add("Amount", typeof(string)); 
        dr = Dt.NewRow();
        dr["TransferNo"] = "";
        dr["Date"] = "";
        dr["Employee"] = "";
        dr["FromLocation"] = "";
        dr["ToLocation"] = "";
        dr["Notes"] = "";
        dr["Amount"] = "";
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
        ddlTransferNo.SelectedIndex = ddlFromLocation.SelectedIndex = ddlEmployee.SelectedIndex = ddlToLocation.SelectedIndex = 0;
        SetInitialRow();
        // ReportGrid();

    }

    public void ReportGrid()
    {
        StrCondition = string.Empty;
        try
        {
            if (ChkFromDate.Checked)
            {
                StrCondition += " and TransferLocationMaster.Date between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and TransferLocationMaster.Date between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlTransferNo.SelectedValue) > 0)
            {
                StrCondition += " and TransferLocationMaster.TransId= " + Convert.ToInt32(ddlTransferNo.SelectedValue);
            }
            #region[EMPLOYEE]
            if (ddlEmployee.Items.Count == 0)
            {
                StrCondition = StrCondition + " and TransferLocationMaster.TransBy =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and TransferLocationMaster.TransBy =" + Convert.ToInt32(ddlEmployee.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlEmployee.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " and (TransferLocationMaster.TransBy = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " or TransferLocationMaster.TransBy = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
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
            #region[FROM LOCATION]

                if (Convert.ToInt32(ddlFromLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " AND TransferLocationDetail.TransFrom=" + Convert.ToInt32(ddlFromLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlFromLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlFromLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (TransferLocationDetail.TransFrom = " + Convert.ToInt32(ddlFromLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlFromLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or TransferLocationDetail.TransFrom = " + Convert.ToInt32(ddlFromLocation.Items[i].Value);
                            }
                        }
                        if (i == ddlFromLocation.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlFromLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
           
            #endregion
            #region[TO LOCATION]

                if (Convert.ToInt32(ddlToLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and TransferLocationDetail.TransTo =" + Convert.ToInt32(ddlFromLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlToLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlToLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (TransferLocationDetail.TransTo = " + Convert.ToInt32(ddlFromLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlToLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or TransferLocationDetail.TransTo = " + Convert.ToInt32(ddlFromLocation.Items[i].Value);
                            }
                        }
                        if (i == ddlToLocation.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlToLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
          
            #endregion

            DS = Obj_TransferLocation.GetTransferForSummary(StrCondition, out StrError, 1);

            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                if (!FlagPrint)
                  ImgBtnPrint.Visible = true;
                if (!FlagPrint)
                  ImgBtnExcel.Visible = true;
                // ViewState["IScancel"] = DS.Tables[0];
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
                GridDetails.FooterRow.Cells[4].Text = "Total :";
                for (int q = 0; q < GridDetails.Rows.Count; q++)
                {
                    GridDetails.FooterRow.Cells[6].Text = ((GridDetails.FooterRow.Cells[6].Text.Equals("&nbsp;") ? 0 : Convert.ToDecimal(GridDetails.FooterRow.Cells[6].Text)) + Convert.ToDecimal(GridDetails.Rows[q].Cells[6].Text)).ToString("#0.00");
                }
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
            DS = Obj_TransferLocation.FillDamageComboForReport(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Transfer Report'");
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
            if (ChkFromDate.Checked)//and RC.RequisitionCafeId=22 and RC.CafeteriaId=4 and RC.RequisitionDate
            {
                StrCondition += " and TransferLocationMaster.Date between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and TransferLocationMaster.Date between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlTransferNo.SelectedValue) > 0)
            {
                StrCondition += " and TransferLocationMaster.TransId= " + Convert.ToInt32(ddlTransferNo.SelectedValue);
            }
          
            #region[EMPLOYEE]
            if (ddlEmployee.Items.Count == 0)
            {
                StrCondition = StrCondition + " and TransferLocationMaster.TransBy =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and TransferLocationMaster.TransBy =" + Convert.ToInt32(ddlEmployee.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlEmployee.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " and (TransferLocationMaster.TransBy = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " or TransferLocationMaster.TransBy = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
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
            #region[FROM LOCATION]

                if (Convert.ToInt32(ddlFromLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " AND TransferLocationDetail.TransFrom=" + Convert.ToInt32(ddlFromLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlFromLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlFromLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (TransferLocationDetail.TransFrom = " + Convert.ToInt32(ddlFromLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlFromLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or TransferLocationDetail.TransFrom = " + Convert.ToInt32(ddlFromLocation.Items[i].Value);
                            }
                        }
                        if (i == ddlFromLocation.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlFromLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
           
            #endregion
            #region[TO LOCATION]

                if (Convert.ToInt32(ddlToLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and TransferLocationDetail.TransTo =" + Convert.ToInt32(ddlFromLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlToLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlToLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (TransferLocationDetail.TransTo = " + Convert.ToInt32(ddlFromLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlToLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or TransferLocationDetail.TransTo = " + Convert.ToInt32(ddlFromLocation.Items[i].Value);
                            }
                        }
                        if (i == ddlToLocation.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlToLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
           
            #endregion
         

            DS = Obj_TransferLocation.GetTransferForSummary(StrCondition, out StrError, 2);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GridView GridExp = new GridView();
                GridExp.DataSource = DS.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("Material Transfer Summary.xls", GridExp);
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

    protected void GridDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.GridDetails.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            if (ChkFromDate.Checked)
            {
                StrCondition += " and TransferLocationMaster.Date between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and TransferLocationMaster.Date between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlTransferNo.SelectedValue) > 0)
            {
                StrCondition += " and TransferLocationMaster.TransId= " + Convert.ToInt32(ddlTransferNo.SelectedValue);
            }
            #region[EMPLOYEE]
            if (ddlEmployee.Items.Count == 0)
            {
                StrCondition = StrCondition + " and TransferLocationMaster.TransBy =" + Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (Convert.ToInt32(ddlEmployee.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and TransferLocationMaster.TransBy =" + Convert.ToInt32(ddlEmployee.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlEmployee.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " and (TransferLocationMaster.TransBy = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlEmployee.Items[i].Value) != 0)
                            {
                                StrCondition += " or TransferLocationMaster.TransBy = " + Convert.ToInt32(ddlEmployee.Items[i].Value);
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
            #region[FROM LOCATION]

           
                if (Convert.ToInt32(ddlFromLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " AND TransferLocationDetail.TransFrom=" + Convert.ToInt32(ddlFromLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlFromLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlFromLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (TransferLocationDetail.TransFrom = " + Convert.ToInt32(ddlFromLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlFromLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or TransferLocationDetail.TransFrom = " + Convert.ToInt32(ddlFromLocation.Items[i].Value);
                            }
                        }
                        if (i == ddlFromLocation.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlFromLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
          
            #endregion
            #region[TO LOCATION]

                if (Convert.ToInt32(ddlToLocation.SelectedValue) > 0)
                {
                    StrCondition = StrCondition + " and TransferLocationDetail.TransTo =" + Convert.ToInt32(ddlFromLocation.SelectedValue);
                }
                else
                {
                    for (int i = 1; i < ddlToLocation.Items.Count; i++)
                    {
                        if (i == 1)
                        {
                            if (Convert.ToInt32(ddlToLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " and (TransferLocationDetail.TransTo = " + Convert.ToInt32(ddlFromLocation.Items[i].Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlToLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " or TransferLocationDetail.TransTo = " + Convert.ToInt32(ddlFromLocation.Items[i].Value);
                            }
                        }
                        if (i == ddlToLocation.Items.Count - 1)
                        {
                            if (Convert.ToInt32(ddlToLocation.Items[i].Value) != 0)
                            {
                                StrCondition += " )";
                            }
                        }

                    }
                }
          
            #endregion
            DS = Obj_TransferLocation.GetTransferForSummary(StrCondition, out StrError, 1);
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

    //========Important Required for Excel Export Must=============
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    //========Important Required for Excel Export Must=============
   
}
