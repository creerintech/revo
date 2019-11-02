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
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;

public partial class Reports_MaterialInwardRegister : System.Web.UI.Page
{
    #region[Variables]
        DMMaterialInwardReg Obj_MaterialInwardReg = new DMMaterialInwardReg();
        MaterialInwardReg Entity_MaterialInwardReg = new MaterialInwardReg();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        //decimal Amount = 0;
        decimal NetAmt = 0;
        string InwardNo = string.Empty;
        decimal SubTotal = 0, GrandTotal=0;
        decimal Discount = 0, Vat = 0, DekhrekhAmt = 0, HamaliAmt = 0, CESSAmt = 0, FreightAmt = 0, PackingAmt = 0, PostageAmt = 0, OtherCharges = 0;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagPrint = false;
    #endregion

    #region[User Define Function]
   
    private void FillCombo()
    {
        DataSet dsCombo = new DataSet();
        string COND = string.Empty;
        //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
        //{
           // COND = COND + " AND Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
        //}
            dsCombo = Obj_MaterialInwardReg.FillReportCombo(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
        if (dsCombo.Tables.Count > 0)
        {
            if (dsCombo.Tables[0].Rows.Count > 0)
            {
                ddlSuppName.DataSource = dsCombo.Tables[0];
                ddlSuppName.DataTextField = "SuplierName";
                ddlSuppName.DataValueField = "SuplierId";
                ddlSuppName.DataBind();
            }
            if (dsCombo.Tables[1].Rows.Count > 0)
            {
                ddlInwardNo.DataSource = dsCombo.Tables[1];
                ddlInwardNo.DataTextField = "InwardNo";
                ddlInwardNo.DataValueField = "InwardId";
                ddlInwardNo.DataBind();
            }
        }
        else
        {
            dsCombo = null;
        }
    }

    private void MakeEmptyForm()
    {
        if (!FlagPrint)
            ImgBtnPrint.Visible = true;
        if (!FlagPrint)
            ImgBtnExport.Visible = true;
        ChkFrmDate.Checked = true;
        txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
        txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        ddlSuppName.SelectedIndex = 0;
        ddlInwardNo.SelectedIndex = 0;
        RdoType.SelectedIndex = 0;
        lblCount.Visible = false;
        SetInitialRow();
        GRIDDETAILS.Visible = false;
        ddlInwardNo.SelectedValue = "0";

    }

    public void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("#", typeof(int)));
            dt.Columns.Add(new DataColumn("InwardNo", typeof(string)));
            dt.Columns.Add(new DataColumn("InwardDate", typeof(string)));
            dt.Columns.Add(new DataColumn("PONO", typeof(string)));
            dt.Columns.Add(new DataColumn("type", typeof(string)));
            dt.Columns.Add(new DataColumn("SuplierName", typeof(string)));
            dt.Columns.Add(new DataColumn("SubTotal", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Discount", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Vat", typeof(decimal)));
            dt.Columns.Add(new DataColumn("DekhrekhAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("HamaliAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CESSAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("FreightAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PackingAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("PostageAmt", typeof(decimal)));
            dt.Columns.Add(new DataColumn("OtherCharges", typeof(decimal)));

            dt.Columns.Add(new DataColumn("GrandTotal", typeof(decimal)));

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["InwardNo"] = "";
            dr["InwardDate"] = "";
            dr["SuplierName"] = "";
            dr["PONO"] = "";
            dr["type"] = "";
            dr["SubTotal"] = 0;
            dr["Discount"] = 0;
            dr["Vat"] = 0;
            dr["DekhrekhAmt"] = 0;
            dr["HamaliAmt"] = 0;
            dr["CESSAmt"] = 0;
            dr["FreightAmt"] = 0;
            dr["PackingAmt"] = 0;
            dr["PostageAmt"] = 0;
            dr["OtherCharges"] = 0;
            dr["GrandTotal"] = 0;
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GridInword.DataSource = dt;
            GridInword.DataBind();
            dt = null;
            dr = null;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void BindReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            DsReport = Obj_MaterialInwardReg.GetInwordReport(RepCondition, out StrError);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                if (!FlagPrint)
                    ImgBtnPrint.Visible = true;
                if (!FlagPrint)
                    ImgBtnExport.Visible = true;
                GridInword.DataSource = DsReport.Tables[0];
                GridInword.DataBind();
                lblCount.Text = DsReport.Tables[0].Rows.Count + " Records Found";
                lblCount.Visible = true;
            }
            else
            {
                GridInword.DataSource = null;
                GridInword.DataBind();
                lblCount.Text = DsReport.Tables[0].Rows.Count + " Records Found";
                lblCount.Visible = true;
                SetInitialRow();
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Inward Report'");
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
                        ImgBtnExport.Visible = false;
                        FlagPrint = true;
                    }

                    dsChkUserRight.Dispose();
               // }
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
        if (!Page.IsPostBack)
        {
            FillCombo();
            CheckUserRight();
            MakeEmptyForm();
            SetInitialRow();
        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        DataSet DsGrd = new DataSet();
        StrCondition = string.Empty;

        if (Convert.ToInt32(RdoType.SelectedValue) != 2)
        {
            StrCondition = StrCondition + " and QM.InwardThrough=" + Convert.ToInt32(RdoType.SelectedValue);
        }

        if (Convert.ToInt32(ddlInwardNo.SelectedValue) > 0)
        {
            StrCondition = StrCondition + " and QM.InwardId=" + Convert.ToInt32(ddlInwardNo.SelectedValue);
        }
       
        if (!string.IsNullOrEmpty(ddlSuppName.Text) && Convert.ToInt32(ddlSuppName.SelectedValue) > 0)
        {
            StrCondition = StrCondition + " and QM.SuplierId=" + Convert.ToInt32(ddlSuppName.SelectedValue);
        }
        if (ChkFrmDate.Checked == true)
        {
            StrCondition = StrCondition + " and QM.InwardDate between '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "' ";
        }
        else
        {
            StrCondition = StrCondition + " AND QM.InwardDate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "' ";
        }
      //  //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
      //  //{
      //      StrCondition = StrCondition + " AND QM.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
      ////  }
        BindReportGrid(StrCondition);
    }
    protected void ChkFrmDate_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkFrmDate.Checked == true)
        {
            txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
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
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
    protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        DataSet DsGrd = new DataSet();
        StrCondition = string.Empty;
        try
        {
            if (Convert.ToInt32(RdoType.SelectedValue) != 2)
            {
                StrCondition = StrCondition + " and QM.InwardThrough=" + Convert.ToInt32(RdoType.SelectedValue);
            }
            if (Convert.ToInt32(ddlInwardNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and QM.InwardId=" + Convert.ToInt32(ddlInwardNo.SelectedValue);
            }

            if (!string.IsNullOrEmpty(ddlSuppName.Text) && Convert.ToInt32(ddlSuppName.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and QM.SuplierId=" + Convert.ToInt32(ddlSuppName.SelectedValue);
            }
            if (ChkFrmDate.Checked == true)
            {
                StrCondition = StrCondition + " and QM.InwardDate between '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "' ";
            }
            else
            {
                StrCondition = StrCondition + " AND QM.InwardDate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "' ";
            }
           // //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
           // //{
           //     StrCondition = StrCondition + " AND QM.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
           //// }
            DsGrd = Obj_MaterialInwardReg.GetInwordReport(StrCondition, out StrError);

            if (DsGrd.Tables[0].Rows.Count > 0)
            {
                //========Call Register
                GridView GridExp = new GridView();
                GridExp.DataSource = DsGrd.Tables[0];
                GridExp.DataBind();
                obj_Comman.Export("InwardRegisterList.xls", GridExp);
            }
            else
            {
                obj_Comman.ShowPopUpMsg("No Data Found To Export..!", this.Page);
                DsGrd.Dispose();
                GridInword.DataSource = null;
                GridInword.DataBind();
            }
        }
        catch (ThreadAbortException tex)
        {

        }
        catch (Exception ex)
        {
           
        }
    }
    protected void GridInword_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow && !string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "InwardNo"))))
            {
                //Taxamt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DiscAmnt"));
                SubTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SubTotal"));
                Discount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Discount"));
                Vat += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Vat"));
                DekhrekhAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DekhrekhAmt"));
                HamaliAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "HamaliAmt"));
                CESSAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CESSAmt"));
                FreightAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FreightAmt"));
                PackingAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PackingAmt"));
                PostageAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PostageAmt"));
                OtherCharges += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OtherCharges"));
                GrandTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "GrandTotal"));                
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {                
                e.Row.Cells[6].Text = "Total";
                e.Row.Cells[7].Text = SubTotal.ToString("0.00");
                e.Row.Cells[8].Text = Discount.ToString("0.00");
                e.Row.Cells[9].Text = Vat.ToString("0.00");
                e.Row.Cells[10].Text = DekhrekhAmt.ToString("0.00");
                e.Row.Cells[11].Text = HamaliAmt.ToString("0.00");
                e.Row.Cells[12].Text = CESSAmt.ToString("0.00");
                e.Row.Cells[13].Text = FreightAmt.ToString("0.00");
                e.Row.Cells[14].Text = PackingAmt.ToString("0.00");
                e.Row.Cells[15].Text = PostageAmt.ToString("0.00");
                e.Row.Cells[16].Text = OtherCharges.ToString("0.00");
                e.Row.Cells[17].Text = GrandTotal.ToString("0.00");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void GridInword_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.GridInword.PageIndex = e.NewPageIndex;
            DataSet DsReport = new DataSet();
            StrCondition = string.Empty;
            if (Convert.ToInt32(ddlInwardNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and QM.InwardId=" + Convert.ToInt32(ddlInwardNo.SelectedValue);
            }

            if (!string.IsNullOrEmpty(ddlSuppName.Text) && Convert.ToInt32(ddlSuppName.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " and QM.SuplierId=" + Convert.ToInt32(ddlSuppName.SelectedValue);
            }
            if (ChkFrmDate.Checked == true)
            {
                StrCondition = StrCondition + " and QM.InwardDate between '" + Convert.ToDateTime(txtFromDate.Text).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(txtToDate.Text).ToString("MM-dd-yyyy") + "' ";
            }
            else
            {
                StrCondition = StrCondition + " AND QM.InwardDate between '01-01-1975' AND '" + DateTime.Now.ToString("MM-dd-yyyy") + "' ";
            }
            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
            //    StrCondition = StrCondition + " AND QM.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}
            DsReport = Obj_MaterialInwardReg.GetInwordReport(StrCondition, out StrError);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                GridInword.DataSource = DsReport.Tables[0];
                GridInword.DataBind();
                lblCount.Text = DsReport.Tables[0].Rows.Count + " Records Found";
                lblCount.Visible = true;
            }
            else
            {
                GridInword.DataSource = null;
                GridInword.DataBind();
                lblCount.Text = DsReport.Tables[0].Rows.Count + " Records Found";
                lblCount.Visible = true;
                SetInitialRow();
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    protected void GridInword_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        DataSet DSInwardDetails = new DataSet();
                        DSInwardDetails = Obj_MaterialInwardReg.GetInwordDetailsReport(Convert.ToString(GridInword.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text.Trim()), out StrError);
                        if (DSInwardDetails.Tables.Count > 0 && DSInwardDetails.Tables[0].Rows.Count > 0)
                        {
                            GridInwordDetails.Visible = true;
                            GRIDDETAILS.Visible = true;
                            GridInwordDetails.DataSource = DSInwardDetails.Tables[0];
                            GridInwordDetails.DataBind();
                        
                        }
                        else
                        {
                            GridInwordDetails.Visible = false;
                            GRIDDETAILS.Visible = false;
                            GridInwordDetails.DataSource = null;
                            GridInwordDetails.DataBind();
                        }
                    }
                    break;

            }
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void hyl_Hide_Click(object sender, EventArgs e)
    {
        GridInwordDetails.Visible = false;
        GRIDDETAILS.Visible = false;
    }
}
