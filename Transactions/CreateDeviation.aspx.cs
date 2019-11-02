using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;

public partial class Transactions_CreateDeviation : System.Web.UI.Page
{
    #region [Variable]-------------------------------------------------------------------
        DMDeviation Obj_Deviation = new DMDeviation();
        Deviation Entity_Deviation = new Deviation();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        DataSet DsDeviationGrd = new DataSet();
        DataTable DSTABLE = new DataTable();
        DataTable DtEditPO = new DataTable();
        decimal NetAmt = 0;
        private string StrCondition = string.Empty;
        private string StrRateCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
    #endregion---------------------------------------------------------------------------

    #region [user Define Function]-------------------------------------------------------------------
    private void GetAmount()
    {
        try
        {
            decimal r1 = 0, r2 = 0;
            for (int q = 0; q < DeviationGrid.Rows.Count; q++)
            {
                r1 += Convert.ToDecimal(DeviationGrid.Rows[q].Cells[11].Text);
                //r2 += Convert.ToDecimal(((TextBox)DeviationGrid.Rows[q].FindControl("GrtxtPhyAmount")).Text);
                r2 += Math.Abs(Convert.ToDecimal(((Label)DeviationGrid.Rows[q].FindControl("GrdDeviationAmount")).Text));
            }
            TxtSysamount.Text = r1.ToString("#0.00");
            TxtPhyamount.Text = Math.Abs(r2).ToString("#0.00");
            TxtTotDeviationAmount.Text = (r1 - Math.Abs(r2)).ToString("0.00");
        }
        catch (Exception)
        {
            throw;
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
                ////Checking Right of users=======
                //{
                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Deviation Register'");
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
                        ReportGridDeviation.Visible = false;
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
                        foreach (GridViewRow GRow in ReportGridDeviation.Rows)
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
                            foreach (GridViewRow GRow in ReportGridDeviation.Rows)
                            {
                                GRow.FindControl("ImgBtnDelete").Visible = false;
                                FlagDel = true;
                            }
                        }

                        //Checking Edit Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                        {
                            foreach (GridViewRow GRow in ReportGridDeviation.Rows)
                            {
                                GRow.FindControl("ImageGridEdit").Visible = false;
                                FlagEdit = true;
                            }
                        }

                        //Checking Print Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                        {
                            foreach (GridViewRow GRow in ReportGridDeviation.Rows)
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

    private void MakeControlDisable(int a)
    {
        if (a == 0)
        {
            txtFromDate.Enabled = txtToDate.Enabled = txtEqualRate.Enabled = txtFromRate.Enabled = txtGreaterRate.Enabled = txtLessRate.Enabled = txtToRate.Enabled = true;
            ddlCategory.Enabled = ddlItemName.Enabled = ddlLocation.Enabled = ddlUnit.Enabled = true;
            lblDevationPeriod.Visible = false;
        }
        else
        {
            txtFromDate.Enabled = txtToDate.Enabled = txtEqualRate.Enabled = txtFromRate.Enabled = txtGreaterRate.Enabled = txtLessRate.Enabled = txtToRate.Enabled = false;
            ddlCategory.Enabled = ddlItemName.Enabled = ddlLocation.Enabled = ddlUnit.Enabled = false;
            lblDevationPeriod.Visible = true;
        }
    }

    private void Fillchkdate()
    {
        if (ChkFrmDate.Checked == true)
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;


        }
        else
        {
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;

        }
    }
    private void MakeEmptyForm()
    {
        ChkFrmDate.Checked = true;
        Fillchkdate();
        txtFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        SetInitialRow();
        StrCondition = string.Empty;
        BindReportGrid(StrCondition);
        ddlLocation.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        ddlItemName.SelectedIndex = 0;
        ddlUnit.SelectedIndex = 0;

        RDO_FromTo.Checked = false;
        RDO_GreaterThenEqualTo.Checked = false;
        RDO_LessThenEqualTo.Checked = false;
        RDO_EqualTo.Checked = false;

        TxtSysamount.Text=TxtPhyamount.Text=TxtTotDeviationAmount.Text=txtFromRate.Text = "";
        txtToRate.Text = "";
        txtGreaterRate.Text = "";
        txtLessRate.Text = "";
        txtEqualRate.Text = "";
        TxtSearch.Text = string.Empty;
        if (!FlagAdd)
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnShow.Visible = true;
        MakeControlDisable(0);
        #region[UserRights]
        if (FlagEdit)
        {
            foreach (GridViewRow GRow in ReportGridDeviation.Rows)
            {
                GRow.FindControl("ImageGridEdit").Visible = false;
            }
        }
        if (FlagDel)
        {
            foreach (GridViewRow GRow in ReportGridDeviation.Rows)
            {
                GRow.FindControl("ImgBtnDelete").Visible = false;
            }
        }
        if (FlagPrint)
        {
            foreach (GridViewRow GRow in ReportGridDeviation.Rows)
            {
                GRow.FindControl("ImgBtnPrint").Visible = false;
            }
        }
        #endregion
    }
    private void FillCombo()
    {
        try
        {
            string COND = string.Empty;
            if ((Session["IsCentral"].ToString())!="True")
            {
                COND = COND + " AND StockLocationID=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            Ds = Obj_Deviation.FillCombo(COND,out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlCategory.DataSource = Ds.Tables[0];
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();
                }
                if (Ds.Tables[2].Rows.Count > 0)
                {
                    ddlLocation.DataSource = Ds.Tables[2];
                    ddlLocation.DataTextField = "Location";
                    ddlLocation.DataValueField = "StockLocationID";
                    ddlLocation.DataBind();
                }
                if (Ds.Tables[3].Rows.Count > 0)
                {
                    ddlUnit.DataSource = Ds.Tables[3];
                    ddlUnit.DataTextField = "Unit";
                    ddlUnit.DataValueField = "UnitId";
                    ddlUnit.DataBind();
                }
                if (Ds.Tables[4].Rows.Count > 0)
                {
                    ddlItemName.DataSource = Ds.Tables[4];
                    ddlItemName.DataTextField = "ItemName";
                    ddlItemName.DataValueField = "ItemId";
                    ddlItemName.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    private bool CheckValidation()
    {
        bool Chackflag = false;
        StrRateCondition = String.Empty;

        if (RDO_FromTo.Checked == true)
        {
            if (string.IsNullOrEmpty(txtFromRate.Text))
            {
                txtFromRate.Text = "0";
            }
            if (string.IsNullOrEmpty(txtToRate.Text))
            {
                txtToRate.Text = "0";
            }
            if (Convert.ToDecimal(txtFromRate.Text) >= 0 && Convert.ToDecimal(txtToRate.Text) >= 0 && Convert.ToDecimal(txtFromRate.Text) <= Convert.ToDecimal(txtToRate.Text))
            {
                StrRateCondition = StrRateCondition + " and (SD.ProductMRP >=" + Convert.ToDecimal(txtFromRate.Text);
                StrRateCondition = StrRateCondition + " and SD.ProductMRP <=" + Convert.ToDecimal(txtToRate.Text) + ") ";

                Chackflag = true;
            }
        }
        else if (RDO_GreaterThenEqualTo.Checked == true)
        {
            if (string.IsNullOrEmpty(txtGreaterRate.Text))
            {
                txtGreaterRate.Text = "0";
            }
            if (Convert.ToDecimal(txtGreaterRate.Text) >= 0)
            {
                StrRateCondition = StrRateCondition + " and SD.ProductMRP >=" + Convert.ToDecimal(txtGreaterRate.Text);

                Chackflag = true;
            }
        }
        else if (RDO_LessThenEqualTo.Checked == true)
        {
            if (string.IsNullOrEmpty(txtLessRate.Text))
            {
                txtLessRate.Text = "0";
            }
            if (Convert.ToDecimal(txtLessRate.Text) >= 0)
            {
                StrRateCondition = StrRateCondition + " and SD.ProductMRP <=" + Convert.ToDecimal(txtLessRate.Text);

                Chackflag = true;
            }
        }
        else if (RDO_EqualTo.Checked == true)
        {
            if (string.IsNullOrEmpty(txtEqualRate.Text))
            {
                txtEqualRate.Text = "0";
            }
            if (Convert.ToDecimal(txtEqualRate.Text) >= 0)
            {
                StrRateCondition = StrRateCondition + " and SD.ProductMRP =" + Convert.ToDecimal(txtEqualRate.Text);

                Chackflag = true;
            }
        }
        else
        {
            Chackflag = true;//If no option is selected...
        }

        return Chackflag;
    }

    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("#", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Category", typeof(string)));
            dt.Columns.Add(new DataColumn("Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Product", typeof(string)));
            dt.Columns.Add(new DataColumn("UnitID", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("MRP", typeof(decimal)));
            dt.Columns.Add(new DataColumn("LocationID", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Location", typeof(string)));
            dt.Columns.Add(new DataColumn("Closing", typeof(decimal)));
            dt.Columns.Add(new DataColumn("SystemAmount", typeof(decimal)));            
            dt.Columns.Add(new DataColumn("PhyClosing", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Deviation", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DeviationAmount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PhyAmount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PhyDeviationQty", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PhyDeviationAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PhyDeviationAmount", typeof(decimal))); 

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["Category"] = "";
            dr["Code"] = "";
            dr["Product"] = "";
            dr["UnitID"] = 0;
            dr["Unit"] = "";
            dr["MRP"] = 0;
            dr["LocationID"] = 0;
            dr["Location"] = "";
            dr["Closing"] = 0;
            dr["SystemAmount"] = 0;
            dr["PhyClosing"] = 0;
            dr["Deviation"] = 0;
            dr["DeviationAmount"] = 0;
            dr["PhyAmount"] = 0;
            dr["PhyDeviationQty"] = 0;
            dr["PhyDeviationAmt"] = 0;
            dr["PhyDeviationAmount"] = 0;
            
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            DeviationGrid.DataSource = dt;
            DeviationGrid.DataBind();
            dt = null;
            dr = null;
            DeviationGrid.Columns[13].Visible=false;
            DeviationGrid.Columns[14].Visible=false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    private void ReportGrid(string FromDate, string ToDate, string StrCondition)
    {
        try
        {
            DsDeviationGrd = Obj_Deviation.ShowStockInHand(FromDate, ToDate, StrCondition, out StrError);

            if (DsDeviationGrd.Tables.Count > 0 && DsDeviationGrd.Tables[0].Rows.Count > 0)
            {
                DeviationGrid.DataSource = DsDeviationGrd.Tables[0];
                DeviationGrid.DataBind();
                DeviationGrid.Columns[13].Visible = false;
                DeviationGrid.Columns[14].Visible = false;
                //GetAmount();
                ((TextBox)DeviationGrid.Rows[0].FindControl("GrtxtPhyClosing")).Focus();
            }

            else
            {
                DeviationGrid.DataSource = null;
                DeviationGrid.DataBind();
                SetInitialRow();
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void BindReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            string COND = string.Empty;
            if ((Session["IsCentral"].ToString())!="True")
            {
                COND = COND + " AND Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            DsReport = Obj_Deviation.GetReport(RepCondition,COND, out StrError);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                ReportGridDeviation.DataSource = DsReport.Tables[0];
                ReportGridDeviation.DataBind();
            }
            else
            {
                ReportGridDeviation.DataSource = null;
                ReportGridDeviation.DataBind();
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    

    #endregion---------------------------------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillCombo();
            MakeEmptyForm();
            CheckUserRight();
        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet DsGrid = new DataSet();
            StrCondition = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;
            string FromDateCheck = string.Empty;
            string ToDateCheck = string.Empty;
            if (CheckValidation() == true)
            {
                if (ChkFrmDate.Checked == true)
                {
                    FromDate = Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy");
                    ToDate = Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy");
                    FromDateCheck = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MM-yyyy");
                    ToDateCheck = Convert.ToDateTime(txtToDate.Text).ToString("dd-MM-yyyy");
                }
                else
                {
                    FromDate = Convert.ToDateTime("01-01-1975").ToString("MM-dd-yyyy");
                    ToDate = DateTime.Now.ToString("MM-dd-yyyy");
                    FromDateCheck = Convert.ToDateTime("01-01-1975").ToString("dd-MM-yyyy");
                    ToDateCheck = DateTime.Now.ToString("dd-MM-yyyy");

                }
             //  if (DateTime.Compare(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate)) <= 0)
                 if (DateTime.Compare(Convert.ToDateTime(FromDateCheck), Convert.ToDateTime(ToDateCheck)) <= 0)
                {
                    if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
                    {
                        StrCondition = StrCondition + " and IC.CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
                    }
                    if (Convert.ToInt32(ddlItemName.SelectedValue) > 0)
                    {
                        StrCondition = StrCondition + " and IM.ItemId=" + Convert.ToInt32(ddlItemName.SelectedValue);
                    }
                    if ((Session["IsCentral"].ToString())!="True")
                    {
                            StrCondition = StrCondition + " AND SD.StockLocationID=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
                    }
                    if ((Session["IsCentral"].ToString())=="True")
                    {
                        if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
                        {
                            StrCondition = StrCondition + " and SD.StockLocationID=" + Convert.ToInt32(ddlLocation.SelectedValue);
                        }
                    }
                    if (Convert.ToInt32(ddlUnit.SelectedValue) > 0)
                    {
                        StrCondition = StrCondition + " and UM.UnitId=" + Convert.ToInt32(ddlUnit.SelectedValue);
                    }

                    if (RDO_FromTo.Checked == true)
                    {
                        StrCondition = StrCondition + StrRateCondition;
                    }
                    if (RDO_GreaterThenEqualTo.Checked == true)
                    {
                        StrCondition = StrCondition + StrRateCondition;
                    }
                    if (RDO_LessThenEqualTo.Checked == true)
                    {
                        StrCondition = StrCondition + StrRateCondition;
                    }
                    if (RDO_EqualTo.Checked == true)
                    {
                        StrCondition = StrCondition + StrRateCondition;
                    }

                    ReportGrid(FromDate, ToDate, StrCondition);

                }
                else
                {
                    obj_Comman.ShowPopUpMsg("From Date should be less or equal to ToDate..!", this.Page);
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Please Check Date..!", this.Page);
            }
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDtls = 0;
        try
        {           
                    Entity_Deviation.DeviationNo = "";                    
                    Entity_Deviation.LoginID = Convert.ToInt32(Session["UserId"]);
                    Entity_Deviation.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    Entity_Deviation.DeviationFrom = Convert.ToDateTime(txtFromDate.Text);
                    Entity_Deviation.DeviationTo = Convert.ToDateTime(txtToDate.Text);

                    Entity_Deviation.SysAmt = Convert.ToDecimal(TxtSysamount.Text);
                    Entity_Deviation.PhyAmt = Convert.ToDecimal(TxtPhyamount.Text);

                    InsertRow = Obj_Deviation.Insert_DeviationMaster(ref Entity_Deviation,Convert.ToInt32(Session["CafeteriaId"].ToString()), out StrError);
                    if (InsertRow > 0)
                    {
                        if (DeviationGrid.Rows.Count>0)
                        {
                            DataTable dtInsert = new DataTable();
                            for (int i = 0; i < DeviationGrid.Rows.Count; i++)
                            {
                                    Entity_Deviation.DeviationID= InsertRow;
                                    Entity_Deviation.ItemID = Convert.ToInt32(((Label)DeviationGrid.Rows[i].FindControl("LblProcessId")).Text);
                                    Entity_Deviation.UnitID = Convert.ToInt32(DeviationGrid.Rows[i].Cells[5].Text);
                                    Entity_Deviation.Rate = Convert.ToDecimal(DeviationGrid.Rows[i].Cells[7].Text);
                                    Entity_Deviation.LocationID = Convert.ToInt32(DeviationGrid.Rows[i].Cells[8].Text);
                                    Entity_Deviation.SysClosing = Convert.ToDecimal(((TextBox)DeviationGrid.Rows[i].FindControl("GrtxtSysClosing")).Text);
                                    Entity_Deviation.PhyClosing = Convert.ToDecimal(((TextBox)DeviationGrid.Rows[i].FindControl("GrtxtPhyClosing")).Text);
                                    Entity_Deviation.Deviation1 = (Convert.ToDecimal(((TextBox)DeviationGrid.Rows[i].FindControl("GrtxtSysClosing")).Text) - Convert.ToDecimal(((TextBox)DeviationGrid.Rows[i].FindControl("GrtxtPhyClosing")).Text));//Convert.ToDecimal(((Label)DeviationGrid.Rows[i].FindControl("Deviation")).Text);
                                    Entity_Deviation.PhyRate = Convert.ToDecimal(((TextBox)DeviationGrid.Rows[i].FindControl("GrtxtPhyRate")).Text);
                                    InsertRowDtls = Obj_Deviation.Insert_DeviationDetails(ref Entity_Deviation, out StrError);
                                }
                        }

                    }

            if (InsertRow > 0)
            {
                obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                MakeEmptyForm();
                Entity_Deviation = null;
                Obj_Deviation = null;
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, InsertRowDtls = 0;
        try
        {
            if (ViewState["ID"] != null)
            {
                Entity_Deviation.DeviationID= Convert.ToInt32(ViewState["ID"]);
            }
            Entity_Deviation.DeviationNo = "";
            Entity_Deviation.LoginID = Convert.ToInt32(Session["UserId"]);
            Entity_Deviation.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            Entity_Deviation.SysAmt = Convert.ToDecimal(TxtSysamount.Text);
            Entity_Deviation.PhyAmt = Convert.ToDecimal(TxtPhyamount.Text);
            UpdateRow = Obj_Deviation.Update_DeviationMaster(ref Entity_Deviation, out StrError);
            if (UpdateRow > 0)
            {
                if (DeviationGrid.Rows.Count > 0)
                {
                    DataTable dtInsert = new DataTable();
                    for (int i = 0; i < DeviationGrid.Rows.Count; i++)
                    {
                        Entity_Deviation.ItemID = Convert.ToInt32(((Label)DeviationGrid.Rows[i].FindControl("LblProcessId")).Text);
                        Entity_Deviation.UnitID = Convert.ToInt32(DeviationGrid.Rows[i].Cells[5].Text);
                        Entity_Deviation.Rate = Convert.ToDecimal(DeviationGrid.Rows[i].Cells[7].Text);
                        Entity_Deviation.LocationID = Convert.ToInt32(DeviationGrid.Rows[i].Cells[8].Text);
                        Entity_Deviation.SysClosing = Convert.ToDecimal(((TextBox)DeviationGrid.Rows[i].FindControl("GrtxtSysClosing")).Text);
                        Entity_Deviation.PhyClosing = Convert.ToDecimal(((TextBox)DeviationGrid.Rows[i].FindControl("GrtxtPhyClosing")).Text);
                        Entity_Deviation.Deviation1 = (Convert.ToDecimal(((TextBox)DeviationGrid.Rows[i].FindControl("GrtxtSysClosing")).Text) - Convert.ToDecimal(((TextBox)DeviationGrid.Rows[i].FindControl("GrtxtPhyClosing")).Text));//Convert.ToDecimal(((Label)DeviationGrid.Rows[i].FindControl("Deviation")).Text);
                        Entity_Deviation.PhyRate = Convert.ToDecimal(((TextBox)DeviationGrid.Rows[i].FindControl("GrtxtPhyRate")).Text);
                        InsertRowDtls = Obj_Deviation.Insert_DeviationDetails(ref Entity_Deviation, out StrError);
                    }
                }

            }
            if (UpdateRow > 0)
            {
                obj_Comman.ShowPopUpMsg("Record Updated Successfully", this.Page);
                MakeEmptyForm();
                Entity_Deviation = null;
                Obj_Deviation = null;
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        BindReportGrid(StrCondition);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMDeviation Obj_DMDeviation = new DMDeviation();
        String[] SearchList = Obj_DMDeviation.GetSuggestedRecord(prefixText);
        return SearchList;
    }
    protected void ReportGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {

                        if (Convert.ToInt32(e.CommandArgument) != 0)
                        {
                            ViewState["ID"] = Convert.ToInt32(e.CommandArgument);
                            Ds = Obj_Deviation.GetRecordForEdit(Convert.ToInt32(e.CommandArgument), out StrError);

                            if (Ds.Tables[0].Rows.Count > 0)
                            {
                                DeviationGrid.DataSource = Ds.Tables[0];
                                DeviationGrid.DataBind();
                                ViewState["CurrentTable"] = Ds.Tables[0];
                                BtnUpdate.Visible = true;
                                BtnSave.Visible = false;
                                BtnShow.Visible = false;
                                lblDevationPeriod.Text = "Deviation Period - "+Ds.Tables[0].Rows[0]["DevationPeriod"].ToString();
                                MakeControlDisable(1);
                                DeviationGrid.Columns[13].Visible = false ;
                                DeviationGrid.Columns[14].Visible = false;
                                (TxtSysamount.Text) = Ds.Tables[0].Rows[0]["SysAmt"].ToString();
                                (TxtPhyamount.Text) = Ds.Tables[0].Rows[0]["PhyAmt"].ToString();
                                (TxtTotDeviationAmount.Text) = (Convert.ToDecimal((TxtSysamount.Text)) - Convert.ToDecimal((TxtPhyamount.Text))).ToString("#0.00");
                               // GetAmount();
                                ScriptManager.RegisterStartupScript(this, GetType(), "TotalForUpdates", "javascript:TotalForUpdates();", true);
                            }
                            else
                            {
                                MakeEmptyForm();
                            }

                            Ds = null;
                            Obj_Deviation = null;
                        }
                        break;
                    }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    protected void ReportGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int DeleteId = 0;
        try
        {
            DeleteId = Convert.ToInt32(((ImageButton)ReportGridDeviation.Rows[e.RowIndex].Cells[0].FindControl("ImgBtnDelete")).CommandArgument.ToString());
            if (DeleteId != 0)
            {
                Entity_Deviation.DeviationID= DeleteId;
                Entity_Deviation.LoginID= Convert.ToInt32(Session["UserID"]);
                Entity_Deviation.LoginDate = DateTime.Now;
                int iDelete = Obj_Deviation.Delete_DeviationMaster(ref Entity_Deviation, out StrError);
                if (iDelete != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_Deviation = null;
            Obj_Deviation = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void GrtxtPhyClosing_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Decimal Quantity = !string.IsNullOrEmpty(txt.Text) ? Convert.ToDecimal(txt.Text) : 0;
        GridViewRow grd = (GridViewRow)txt.Parent.Parent;
        ((Label)grd.FindControl("GrdDeviation")).Text=(Convert.ToDecimal(grd.Cells[10].Text) - Convert.ToDecimal(((TextBox)grd.FindControl("GrtxtPhyClosing")).Text)).ToString("#0.00");
        ((Label)grd.FindControl("GrdDeviationAmount")).Text = Math.Abs(Convert.ToDecimal((Convert.ToDecimal(((TextBox)grd.FindControl("GrtxtPhyClosing")).Text) - (Convert.ToDecimal(grd.Cells[10].Text)))) * Convert.ToDecimal(((TextBox)grd.FindControl("GrtxtPhyAmount")).Text)).ToString("#0.00");
        ((Label)grd.FindControl("GrdDeviationAmt")).Text = Math.Abs(Convert.ToDecimal((Convert.ToDecimal(((TextBox)grd.FindControl("GrtxtPhyClosing")).Text) )) * Convert.ToDecimal(((TextBox)grd.FindControl("GrtxtPhyAmount")).Text)).ToString("#0.00");
        //decimal Rate = Convert.ToDecimal(grd.Cells[7].Text);
        //((TextBox)grd.FindControl("GrtxtPhyAmount")).Text = (Quantity * Rate).ToString("#0.00");
        GetAmount();
        ((TextBox)grd.FindControl("GrtxtPhyAmount")).Focus();
    }
    protected void DeviationGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              //  ((Label)e.Row.FindControl("GrdDeviation")).Text = (Convert.ToDecimal(((TextBox)e.Row.FindControl("GrtxtPhyClosing")).Text) - (Convert.ToDecimal(e.Row.Cells[10].Text))).ToString("#0.00");
              //  ((Label)e.Row.FindControl("GrdDeviationAmount")).Text = Math.Abs((Convert.ToDecimal(((TextBox)e.Row.FindControl("GrtxtPhyClosing")).Text) - (Convert.ToDecimal(e.Row.Cells[10].Text))) * Convert.ToDecimal(((TextBox)e.Row.FindControl("GrtxtPhyAmount")).Text)).ToString("#0.00");
              //  ((Label)e.Row.FindControl("GrdDeviationAmt")).Text = Math.Abs((Convert.ToDecimal(((TextBox)e.Row.FindControl("GrtxtPhyClosing")).Text) - (Convert.ToDecimal(e.Row.Cells[10].Text))) * Convert.ToDecimal(((TextBox)e.Row.FindControl("GrtxtPhyAmount")).Text)).ToString("#0.00");
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void GrtxtPhyAmount_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        GridViewRow grd = (GridViewRow)txt.Parent.Parent;
        ((Label)grd.FindControl("GrdDeviation")).Text = (Convert.ToDecimal(grd.Cells[10].Text) - Convert.ToDecimal(((TextBox)grd.FindControl("GrtxtPhyClosing")).Text)).ToString("#0.00");
        ((Label)grd.FindControl("GrdDeviationAmount")).Text = Math.Abs(Convert.ToDecimal((Convert.ToDecimal(((TextBox)grd.FindControl("GrtxtPhyClosing")).Text) - (Convert.ToDecimal(grd.Cells[10].Text)))) * Convert.ToDecimal(((TextBox)grd.FindControl("GrtxtPhyAmount")).Text)).ToString("#0.00");
        ((Label)grd.FindControl("GrdDeviationAmt")).Text = Math.Abs(Convert.ToDecimal((Convert.ToDecimal(((TextBox)grd.FindControl("GrtxtPhyClosing")).Text) )) * Convert.ToDecimal(((TextBox)grd.FindControl("GrtxtPhyAmount")).Text)).ToString("#0.00");
        GetAmount();
        if (DeviationGrid.Rows.Count <= grd.RowIndex + 1)
            ((TextBox)DeviationGrid.Rows[grd.RowIndex].FindControl("GrtxtPhyClosing")).Focus();
        else
            ((TextBox)DeviationGrid.Rows[grd.RowIndex + 1].FindControl("GrtxtPhyClosing")).Focus();
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionItemNameList(string prefixText, int count, string contextKey)
    {
        DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
        String[] SearchList = Obj_RequisitionCafeteria.GetSuggestedRecordItems(prefixText, "");
        return SearchList;
    }
    protected void TxtItemName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
            StrCondition = string.Empty;
            StrCondition = TxtItemName.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                TxtItemName.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                TxtItemName.ToolTip = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                ddlItemName.SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                Ds = null;
            }
            else
            {
                TxtItemName.Text = "";
                TxtItemName.ToolTip = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
