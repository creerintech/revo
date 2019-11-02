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

public partial class Reports_POExcessQty : System.Web.UI.Page
{
    #region[Private variables]
        DMPurchaseOrder Obj_PO = new DMPurchaseOrder();
        PurchaseOrder Entity_PO = new PurchaseOrder();
        CommanFunction Obj_Comm = new CommanFunction();
        public static DataSet DsGrd = new DataSet();
        DataSet DS = new DataSet();
        decimal SubTotal, GrandTotal,Discount,Vat = 0;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagPrint = false;
    #endregion

    #region[User Defined Function]

    private void MakeEmptyForm()
    {
        if (!FlagPrint)
            ImgBtnPrint.Visible = true;
        if (!FlagPrint)
            ImgBtnExport.Visible = true;
        SetInitialRow();
        ddlSupplier.SelectedIndex = 0;
        ddlNo.SelectedIndex = 0;
    }

    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
          
            dt.Columns.Add(new DataColumn("#", typeof(Int32)));
            dt.Columns.Add(new DataColumn("SuplierId", typeof(Int32)));
            dt.Columns.Add(new DataColumn("ItemID", typeof(Int32)));
            dt.Columns.Add(new DataColumn("PONo", typeof(string)));
            dt.Columns.Add(new DataColumn("PODate", typeof(string)));
            dt.Columns.Add(new DataColumn("Supplier", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
            dt.Columns.Add(new DataColumn("POQTY", typeof(string)));
            dt.Columns.Add(new DataColumn("INQTY", typeof(string)));
            dt.Columns.Add(new DataColumn("BALQTY", typeof(string)));
            dt.Columns.Add(new DataColumn("InwardNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Inwarddate", typeof(string)));

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["SuplierId"] = 0;
            dr["ItemID"] = 0;
            dr["PONo"] = "";
            dr["PODate"] = "";
            dr["Supplier"] = "";

            dr["ItemName"] = "";
            dr["ItemDesc"] = "";
            dr["POQTY"] = "";
            dr["INQTY"] = "";
            dr["BALQTY"] = "";
            dr["InwardNo"] = "";
            dr["Inwarddate"] = "";

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            GrdReport.DataSource = dt;
            GrdReport.DataBind();
            dt = null;
            dr = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    private void FillCombo()
    {
        try
        {

            DS = Obj_PO.FillComboReport(Convert.ToInt32(Session["UserID"]),out StrError);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlNo.DataSource = DS.Tables[0];
                    ddlNo.DataTextField = "PONo";
                    ddlNo.DataValueField = "POId";
                    ddlNo.DataBind();
                }
                if (DS.Tables[1].Rows.Count > 0)
                {
                    ddlSupplier.DataSource = DS.Tables[1];
                    ddlSupplier.DataTextField = "SuplierName";
                    ddlSupplier.DataValueField = "SuplierId";
                    ddlSupplier.DataBind();
                }
                if (DS.Tables[6].Rows.Count > 0)
                {
                    ddlSite.DataSource = DS.Tables[6];
                    ddlSite.DataTextField = "Location";
                    ddlSite.DataValueField = "StockLocationID";
                    ddlSite.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void ReportGrid(string StrCondition)
    {
        try
        {
            DsGrd = Obj_PO.GetPOForExcessReport(StrCondition, out StrError);
            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                if (!FlagPrint)
                    ImgBtnPrint.Visible = true;
                if (!FlagPrint)
                    ImgBtnExport.Visible = true;
                GrdReport.DataSource = DsGrd.Tables[0];
                GrdReport.DataBind();
                lblCount.Text = DsGrd.Tables[0].Rows.Count + " Records Found";
                lblCount.Visible = true;
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
                lblCount.Text = "No Records Found";
                lblCount.Visible = true;
                SetInitialRow();
            }
            MergeRows(GrdReport);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Purchase Report'");
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
        if (!Page.IsPostBack)
        {
            FillCombo();
            CheckUserRight();
            SetInitialRow();
            MakeEmptyForm();
            Username.Text = Session["UserName"].ToString();
        }
    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {
         try
        {
            DataSet DsGrid = new DataSet();
            StrCondition = string.Empty;
            
            if (Convert.ToInt32(ddlNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " AND C.POId=" + Convert.ToInt32(ddlNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlSupplier.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " AND C.SuplierId=" + Convert.ToInt32(ddlSupplier.SelectedValue);
            }
            
              if (Convert.ToInt32(ddlSite.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " AND C.IsCostCentre = " + Convert.ToInt32(ddlSite.SelectedValue);
            }
             
            ReportGrid(StrCondition);
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }

    protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataSet DsGrid = new DataSet();
            StrCondition = string.Empty;

            if (Convert.ToInt32(ddlNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " AND C.POId=" + Convert.ToInt32(ddlNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlSupplier.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " AND C.SuplierId=" + Convert.ToInt32(ddlSupplier.SelectedValue);
            }

            if (Convert.ToInt32(ddlSite.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " AND C.IsCostCentre = " + Convert.ToInt32(ddlSite.SelectedValue);
            }
            DsGrd = Obj_PO.GetPOForExcessReport(StrCondition, out StrError);
            DsGrd.Tables[0].Columns.Remove("SuplierId");
            DsGrd.Tables[0].Columns.Remove("POId");
            DsGrd.Tables[0].Columns.Remove("IsCostCentre");
            DsGrd.Tables[0].Columns.Remove("#");
            DsGrd.Tables[0].Columns.Remove("ExcessApproved");
            DsGrd.Tables[0].Columns.Remove("ItemId");

            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                //========Call Register
                GridView GridExp = new GridView();
                GridExp.DataSource = DsGrd.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("Purchase Order Excess Report.xls", GridExp);
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("No Data Found To Export..!", this.Page);
                DsGrd.Dispose();
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }
            DsGrd = null;
        }
        catch (ThreadAbortException tex)
        {

        }
        catch(Exception ex)
        {
            
        }
    }
   
    protected void GrdReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           
        }
        catch(Exception ex)
        {
           
        }
    }

    protected void GrdReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.GrdReport.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;

            if (Convert.ToInt32(ddlNo.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " AND C.POId=" + Convert.ToInt32(ddlNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlSupplier.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " AND C.SuplierId=" + Convert.ToInt32(ddlSupplier.SelectedValue);
            }

            if (Convert.ToInt32(ddlSite.SelectedValue) > 0)
            {
                StrCondition = StrCondition + " AND C.IsCostCentre = " + Convert.ToInt32(ddlSite.SelectedValue);
            }
            DsGrd = Obj_PO.GetPOForExcessReport(StrCondition, out StrError);

            if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
            {
                GrdReport.DataSource = DsGrd.Tables[0];
                GrdReport.DataBind();
                lblCount.Text = DsGrd.Tables[0].Rows.Count + " Records Found";
                lblCount.Visible = true;
            }

            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
                lblCount.Text = DsGrd.Tables[0].Rows.Count + " Records Found";
                lblCount.Visible = true;
                SetInitialRow();
            }
            MergeRows(GrdReport);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void GrdSelectAllHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox GrdSelectAllHeader1 = (CheckBox)sender;
        GridViewRow row = (GridViewRow)GrdSelectAllHeader1.NamingContainer;
        int CurrRow = row.RowIndex;
        if (GrdSelectAllHeader1.Checked == true)
        {
            for (int j = 0; j < GrdReport.Rows.Count; j++)
            {
                CheckBox GrdSelectAll = (CheckBox)GrdReport.Rows[j].Cells[1].FindControl("GrdSelectAll");
                GrdSelectAll.Checked = true;
            }
        }
        else
        {
            for (int j = 0; j < GrdReport.Rows.Count; j++)
            {
                CheckBox GrdSelectAll = (CheckBox)GrdReport.Rows[j].Cells[1].FindControl("GrdSelectAll");
                GrdSelectAll.Checked = false;
            }
        }
    }

    public void MergeRows(GridView gridView)
    {

        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            if (row.Cells[3].Text == previousRow.Cells[3].Text)
            {
                row.Cells[3].RowSpan = previousRow.Cells[3].RowSpan < 2 ? 2 :
                                       previousRow.Cells[3].RowSpan + 1;
                previousRow.Cells[3].Visible = false;

            }
            if (row.Cells[4].Text == previousRow.Cells[4].Text)
            {
                row.Cells[4].RowSpan = previousRow.Cells[4].RowSpan < 2 ? 2 :
                                       previousRow.Cells[4].RowSpan + 1;
                previousRow.Cells[4].Visible = false;

            }
            if (row.Cells[5].Text == previousRow.Cells[5].Text)
            {
                row.Cells[5].RowSpan = previousRow.Cells[5].RowSpan < 2 ? 2 :
                                       previousRow.Cells[5].RowSpan + 1;
                previousRow.Cells[5].Visible = false;

            }
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int UpdateRow = 0;
            for (int j = 0; j < GrdReport.Rows.Count; j++)
            {
                if (((CheckBox)GrdReport.Rows[j].FindControl("GrdSelectAll")).Checked == true)
                {
                    Entity_PO.POId=Convert.ToInt32(GrdReport.Rows[j].Cells[11].Text.ToString());
                    Entity_PO.SuplierId = Convert.ToInt32(GrdReport.Rows[j].Cells[12].Text.ToString());
                    Entity_PO.ItemId = Convert.ToInt32(GrdReport.Rows[j].Cells[13].Text.ToString());
                    Entity_PO.UserId = Convert.ToInt32(Session["UserId"]);
                    UpdateRow = Obj_PO.UpdateReqExcessStatus(ref Entity_PO, out StrError);
                }
            }
            if (UpdateRow > 0)
            {
                Obj_Comm.ShowPopUpMsg("Excess Approve Successfully..", this.Page);
                MakeEmptyForm();
                BtnShow_Click(BtnShow, EventArgs.Empty);
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BTNLOGINFORM_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet DS = new DataSet();
            DMPurchaseOrder DMPO = new DMPurchaseOrder();
            DS = DMPO.GETACCESSForReport(Convert.ToInt32(Session["UserId"]), TXTPASSWORDFORM.Text.Trim(), out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                Panel1.Visible = false;
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("Password Not Match......Please Try Again!", this.Page);
            }
        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception ex) { Obj_Comm.ShowPopUpMsg(ex.Message + " --> THIS ERROR OCCURED WHILE UPDATING DATA.", this.Page); }
    }
}
