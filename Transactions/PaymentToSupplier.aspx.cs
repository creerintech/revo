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
using System.Web.Mail;
using System.IO;

public partial class Transactions_EditPurchaseOrder : System.Web.UI.Page
{
    #region [private variables]
        DMPaymentToSupplier Obj_EditPO = new DMPaymentToSupplier();
        CommanFunction obj_Comman = new CommanFunction();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        DataSet Ds = new DataSet();
    #endregion

    #region[UserDefine Function]

        //User Right Function===========
        public void CheckUserRight()
        {
            try
            {
                #region [USER RIGHT]
                //Checking Session Varialbels========
                if (Session["UserName"] != null && Session["UserRole"] != null)
                {
                   
                        System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                        System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                        dsChkUserRight1 = (DataSet)Session["DataSet"];

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Authorized Purchase Order'");
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
                                    
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                        {
                            BtnSave.Visible = false;
                           

                       
                        }
                        dsChkUserRight.Dispose();
                    
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

    private void MakeEmptyForm()
    {
        StrCondition = string.Empty;
        BindReportGrid(StrCondition);
        BtnSave.Visible = true;
        TxtSearch.Text = string.Empty;
    }

    
    private void SetInitialRow_ReqDetails()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("POId", typeof(int));
            dt.Columns.Add("SuplierName", typeof(string));
            dt.Columns.Add("PONo", typeof(string));
            dt.Columns.Add("PODate", typeof(string));
            dt.Columns.Add("POAmount", typeof(string));
            dt.Columns.Add("PaymentAmount", typeof(string));
            dt.Columns.Add("RemAmount", typeof(string));
            dt.Columns.Add("ChequeNo", typeof(string));
            dt.Columns.Add("PersonName", typeof(string));
            dt.Columns.Add("Remark", typeof(string));

            dr = dt.NewRow();

            dr["#"] = 0;
            dr["POId"]=0;
            dr["SuplierName"]="";
            dr["PONo"]="";
            dr["PODate"]="";
            dr["POAmount"]="";
            dr["PaymentAmount"]="";
            dr["RemAmount"]="";
            dr["ChequeNo"]="";
            dr["PersonName"]="";
            dr["Remark"]="";

            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
            GrdReqPO.DataSource = dt;
            GrdReqPO.DataBind();
        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }

 

    public void BindReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            DsReport = Obj_EditPO.FillGrid(RepCondition,out StrError);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                GrdReqPO.DataSource = DsReport.Tables[0];
                GrdReqPO.DataBind();

            }
            else
            {
                GrdReqPO.DataSource = null;
                GrdReqPO.DataBind();
                SetInitialRow_ReqDetails();
            }

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void BindReportGridTextSearch(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            DsReport = Obj_EditPO.FillGridFromSearch(RepCondition, out StrError);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                GrdReqPO.DataSource = DsReport.Tables[0];
                GrdReqPO.DataBind();

            }
            else
            {
                GrdReqPO.DataSource = null;
                GrdReqPO.DataBind();
                SetInitialRow_ReqDetails();
            }

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
  

    
    #endregion

    protected void GrdSelectAllHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox GrdSelectAllHeader1 = (CheckBox)sender;
        GridViewRow row = (GridViewRow)GrdSelectAllHeader1.NamingContainer;
        int CurrRow = row.RowIndex;
        if (GrdSelectAllHeader1.Checked == true)
        {
            for (int j = 0; j < GrdReqPO.Rows.Count; j++)
            {
                CheckBox GrdSelectAll = (CheckBox)GrdReqPO.Rows[j].Cells[1].FindControl("GrdSelectAll");
                GrdSelectAll.Checked = true;
            }
        }
        else
        {
            for (int j = 0; j < GrdReqPO.Rows.Count; j++)
            {
                CheckBox GrdSelectAll = (CheckBox)GrdReqPO.Rows[j].Cells[1].FindControl("GrdSelectAll");
                GrdSelectAll.Checked = false;
            }
        }
    }

   

   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           MakeEmptyForm();
           CheckUserRight();
        }
    }

    

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0;
        for (int j = 0; j < GrdReqPO.Rows.Count; j++)
        {
            
        }

    }

    protected void BtnReset_Click(object sender, EventArgs e)
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
        DMPaymentToSupplier Obj_EditPO = new DMPaymentToSupplier();
        String[] SearchList = Obj_EditPO.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    

}