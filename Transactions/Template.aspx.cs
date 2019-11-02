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

using AjaxControlToolkit;
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;

public partial class Transactions_Template : System.Web.UI.Page
{
    #region [private variables]
        DMTemplate Obj_Template = new DMTemplate();
        Template Entity_Template = new Template();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        private static DataTable DSVendor = new DataTable();
        DataSet DsEdit = new DataSet();
        DataTable DtEditPO;
        public static int EditTemp = 1;
        int CategoryID, insertrow;
        bool flag = false; bool clear = false; bool flag1 = false;
        bool CheckFlag = false;
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint= false;
    database db = new database();
    #endregion

    #region[UserDefineFunction]
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

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Create/Update Template'");
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
                            ReportGrid.Visible = false;
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
                            foreach (GridViewRow GRow in ReportGrid.Rows)
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
                                foreach (GridViewRow GRow in ReportGrid.Rows)
                                {
                                    GRow.FindControl("ImgBtnDelete").Visible = false;
                                    FlagDel = true;
                                }
                            }

                            //Checking Edit Right ========
                            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                            {
                                foreach (GridViewRow GRow in ReportGrid.Rows)
                                {
                                    GRow.FindControl("ImageGridEdit").Visible = false;
                                    FlagEdit = true;
                                }
                            }

                            //Checking Print Right ========
                            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                            {
                                foreach (GridViewRow GRow in ReportGrid.Rows)
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
            catch(ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //User Right Function===========

        public void MakeEmptyForm()
        {
            txtTemplateFor.Text = string.Empty;
            txtTemplateDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            flag = clear = false;
            CategoryID = insertrow = 0;
            ViewState["Template"] = 0;
            SetInitialRowGrdTemplate();
            SetInitialRow_GridTemplateVendor();
       
        SetInitialRow_ReportGrid();
            if (!FlagAdd)
            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            TxtSearch.Text = string.Empty;
            BindCombo();
            ViewState["GridIndex"] = null;

            BindReportGrid();

            #region[UserRights]
            if (FlagEdit)
            {
                foreach (GridViewRow GRow in ReportGrid.Rows)
                {
                   GRow.FindControl("ImageGridEdit").Visible = false;
                }
            }
            if (FlagDel)
            {
                foreach (GridViewRow GRow in ReportGrid.Rows)
                {
                    GRow.FindControl("ImgBtnDelete").Visible = false;
                }
            }
            if (FlagPrint)
            {
                foreach (GridViewRow GRow in ReportGrid.Rows)
                {
                    GRow.FindControl("ImgBtnPrint").Visible = false;
                }
            }
            #endregion
            txtTemplateFor.Focus();
        }

        private void SetInitialRowGrdTemplate()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add("#", typeof(int));
                dt.Columns.Add("ItemCode", typeof(string));
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("ItemDesc", typeof(string));
                dt.Columns.Add("DrawingNo", typeof(string));
                
                dt.Columns.Add("ItemDetailsId", typeof(int));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("AvlQty", typeof(decimal));
                dt.Columns.Add("AvgRate", typeof(decimal));
                dt.Columns.Add("MinStockLevel", typeof(decimal));
                dt.Columns.Add("DeliveryPeriod", typeof(decimal));
                dt.Columns.Add("AvgRateDate", typeof(string));
                dt.Columns.Add("Vendor", typeof(string));
                dt.Columns.Add("Rate", typeof(string));
                dt.Columns.Add("VendorId", typeof(int));
                dt.Columns.Add("QTY", typeof(string));
                dt.Columns.Add("REMARK", typeof(string));
                dt.Columns.Add("UOM", typeof(string)); 
                dr = dt.NewRow();

                dr["#"] = 0;
                dr["ItemCode"] = "";
                dr["ItemName"] = "";
                dr["ItemDesc"] = "";
                dr["DrawingNo"] = ""; 
                dr["ItemDetailsId"] = 0;
                dr["Location"] = "";
                dr["AvgRateDate"] = "";
                dr["Vendor"] = "";
                dr["AvlQty"] = 0;
                dr["AvgRate"] = 0;
                dr["MinStockLevel"] = 0;
                dr["DeliveryPeriod"] = 0;
                dr["Rate"] = "";
                dr["VendorId"] = 0;
                dr["QTY"] = "0";
                dr["REMARK"] = "";
                dr["UOM"] = "-";
                dt.Rows.Add(dr);
                ViewState["Template"] = dt;
                GrdTemplate.DataSource = dt;
                GrdTemplate.DataBind();
            }

            catch (Exception ex) { throw new Exception(ex.Message); }
        }

  
    private void SetInitialRow_GridTemplateVendor()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("#", typeof(int)));
                dt.Columns.Add(new DataColumn("VendorId", typeof(int)));
                dt.Columns.Add(new DataColumn("Vendor", typeof(string)));
                dt.Columns.Add(new DataColumn("PurRate", typeof(decimal)));
                dr = dt.NewRow();
                dr["#"] = string.Empty;
                dr["VendorId"] = string.Empty;
                dr["Vendor"] = string.Empty;
                dr["PurRate"] = 0;
                dt.Rows.Add(dr);
                GridTemplateVendor.DataSource = dt;
                GridTemplateVendor.DataBind();
            }
            catch (Exception ex)
            {
            }
        }




    private void SetInitialRow_ReportGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("#", typeof(int)));
                dt.Columns.Add(new DataColumn("TemplateId", typeof(string)));
                dt.Columns.Add(new DataColumn("TemplateDate", typeof(string)));
                dt.Columns.Add(new DataColumn("TemplateBy", typeof(string)));
                dr = dt.NewRow();
                dr["#"] = 0;
                dr["TemplateId"] = string.Empty;
                dr["TemplateDate"] = string.Empty;
                dr["TemplateBy"] = string.Empty;
                dt.Rows.Add(dr);
                ReportGrid.DataSource = dt;
                ReportGrid.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        public void BindReportGrid()
        {
            try
            {
                DataSet DS = new DataSet();
                Ds = Obj_Template.GetGridTempalate(StrCondition, out StrError);
                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    ReportGrid.DataSource = Ds.Tables[0];
                    ReportGrid.DataBind();
                }
                else
                {
                    SetInitialRow_ReportGrid();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void BindCombo()
        {
            try
            {
                Ds = new DataSet();
                Ds = Obj_Template.GetCategoryName(out StrError);
                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    ddlCategory.DataSource = Ds.Tables[0];
                    ddlCategory.DataTextField = "Name";
                    ddlCategory.DataValueField = "#";
                    ddlCategory.DataBind();
                   
                }
                if (Ds.Tables.Count > 0 && Ds.Tables[2].Rows.Count > 0)
                {
                    ddlItem.DataSource = Ds.Tables[2];
                    ddlItem.DataTextField = "ItemName";
                    ddlItem.DataValueField = "#";
                    ddlItem.DataBind();
                   
                }
                Ds = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CheckDuplicate(int ItemID)
        {
            if (ViewState["Template"] != null)
            {
                DataTable dtt = new DataTable();
                dtt = (DataTable)ViewState["Template"];
                for (int u = 0; u < dtt.Rows.Count; u++)
                {
                    if (Convert.ToInt32(dtt.Rows[u]["#"]) == ItemID)
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
        }

        public void CheckDuplicateAddAll(int ItemID, DataSet ds)
        {
            int q = 0;
            if (ViewState["Template"] != null)
            {
                DataTable dtt = new DataTable();
                dtt = (DataTable)ViewState["Template"];
                for (int u = 0; u < dtt.Rows.Count; u++)
                {
                    for (int e = 0; e < ds.Tables[0].Rows.Count; e++)
                    {
                        if ((Convert.ToInt32(dtt.Rows[u]["#"]) == Convert.ToInt32(ds.Tables[0].Rows[e]["#"])) && Convert.ToInt32(dtt.Rows[u]["ItemDetailsId"]) == Convert.ToInt32(ds.Tables[0].Rows[e]["ItemDetailsId"]))
                        {
                            flag1 = true;
                            // break;
                            q = q + 1;
                        }
                        else
                        {
                            // flag1 = false;
                        }
                    }
                }
                if ((dtt.Rows.Count) == q)
                {
                    flag1 = false;
                }
            }
        }

        public void BindItemToGrid(DataSet ds)  //For Binding To Grid One By One
        {
            if (ViewState["Template"] != null)
            {
                bool DupFlag = false;
                int k = 0;
                DataTable DTTABLE = (DataTable)ViewState["Template"];
                DataRow DTROW = null;
                if (DTTABLE.Rows.Count > 0)
                {
                    if (DTTABLE.Rows.Count > 0)
                    {

                        if (ViewState["GridIndex"] == null)
                        {

                            for (int i = 0; i < DTTABLE.Rows.Count; i++)
                            {
                                int ItemID = Convert.ToInt32(DTTABLE.Rows[i]["#"]);
                                int ItemDetailsId = Convert.ToInt32(DTTABLE.Rows[i]["ItemDetailsId"]);
                                if (DTTABLE.Rows.Count == 1 && (DTTABLE.Rows[0]["#"].ToString()) == "0")
                                {
                                    DTTABLE.Rows.RemoveAt(0);
                                }
                                if ((Convert.ToInt32(ds.Tables[0].Rows[i]["#"]) == Convert.ToInt32(ItemID)) && (Convert.ToInt32(ds.Tables[0].Rows[i]["ItemDetailsId"]) == Convert.ToInt32(ItemDetailsId)))
                                {
                                    DupFlag = true;
                                    k = i;
                                }
                            }
                            if (DupFlag == true)
                            {
                                DTTABLE.Rows[k]["#"] = Convert.ToInt32(DTTABLE.Rows[k]["#"]);
                                DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(DTTABLE.Rows[k]["ItemCode"]);
                                DTTABLE.Rows[k]["ItemName"] = Convert.ToString(DTTABLE.Rows[k]["ItemName"]);
                                DTTABLE.Rows[k]["ItemDetailsId"] = Convert.ToString(DTTABLE.Rows[k]["ItemDetailsId"]);
                                DTTABLE.Rows[k]["ItemDesc"] = Convert.ToString(DTTABLE.Rows[k]["ItemDesc"]);
                                DTTABLE.Rows[k]["Location"] = Convert.ToString(DTTABLE.Rows[k]["Location"]);
                                DTTABLE.Rows[k]["AvlQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvlQty"]);
                                DTTABLE.Rows[k]["MinStockLevel"] = Convert.ToDecimal(DTTABLE.Rows[k]["MinStockLevel"]);
                                DTTABLE.Rows[k]["DeliveryPeriod"] = Convert.ToDecimal(DTTABLE.Rows[k]["DeliveryPeriod"]);
                                DTTABLE.Rows[k]["AvgRate"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvgRate"]);
                                DTTABLE.Rows[k]["AvgRateDate"] = Convert.ToString(DTTABLE.Rows[k]["AvgRateDate"]);
                                DTTABLE.Rows[k]["Vendor"] = Convert.ToString(DTTABLE.Rows[k]["Vendor"]);
                                DTTABLE.Rows[k]["VendorId"] = Convert.ToString(DTTABLE.Rows[k]["VendorId"]);
                                DTTABLE.Rows[k]["Rate"] = Convert.ToString(DTTABLE.Rows[k]["Rate"]);
                                ViewState["Template"] = DTTABLE;
                                GrdTemplate.DataSource = DTTABLE;
                                GrdTemplate.DataBind();
                                DtEditPO = (DataTable)ViewState["Template"];
                            }
                            else
                            {
                                DTROW = DTTABLE.NewRow();

                                DTROW["#"] = Convert.ToInt32(ds.Tables[0].Rows[k]["#"]);
                                DTROW["ItemCode"] = Convert.ToString(ds.Tables[0].Rows[0]["ItemCode"]);
                                DTROW["ItemName"] = Convert.ToString(ds.Tables[0].Rows[0]["ItemName"]);
                                DTROW["ItemDetailsId"] = Convert.ToString(ds.Tables[0].Rows[0]["ItemDetailsId"]);
                                DTROW["ItemDesc"] = Convert.ToString(ds.Tables[0].Rows[0]["ItemDesc"]);
                                DTROW["Location"] = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
                                DTROW["AvlQty"] = Convert.ToDecimal(ds.Tables[0].Rows[0]["AvlQty"]);
                                DTROW["MinStockLevel"] = Convert.ToDecimal(ds.Tables[0].Rows[0]["MinStockLevel"]);
                                DTROW["DeliveryPeriod"] = Convert.ToDecimal(ds.Tables[0].Rows[0]["DeliveryPeriod"]);
                                DTROW["AvgRate"] = Convert.ToDecimal(ds.Tables[0].Rows[0]["AvgRate"]);
                                DTROW["AvgRateDate"] = Convert.ToString(ds.Tables[0].Rows[0]["AvgRateDate"]);
                                DTROW["Vendor"] = Convert.ToString(ds.Tables[0].Rows[0]["Vendor"]);
                                DTROW["VendorId"] = Convert.ToString(ds.Tables[0].Rows[0]["VendorId"]);
                                DTROW["Rate"] = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);
                                DTTABLE.Rows.Add(DTROW);
                                ViewState["Template"] = DTTABLE;
                                GrdTemplate.DataSource = DTTABLE;
                                GrdTemplate.DataBind();
                                DtEditPO = (DataTable)ViewState["Template"];
                                ViewState["GridIndex"] = k++;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                int ItemID = Convert.ToInt32(DTTABLE.Rows[i]["#"]);
                                int ItemDetailsId = Convert.ToInt32(DTTABLE.Rows[i]["ItemDetailsId"]);
                                if ((Convert.ToInt32(ds.Tables[0].Rows[0]["#"]) == Convert.ToInt32(ItemID)) && (Convert.ToInt32(ds.Tables[0].Rows[i]["ItemDetailsId"]) == Convert.ToInt32(ItemDetailsId)))
                                {
                                    DupFlag = false;
                                    k = i;
                                }
                            }
                            if (DupFlag == true)
                            {
                                int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                                DTTABLE.Rows[k]["#"] = Convert.ToInt32(DTTABLE.Rows[k]["#"]);
                                DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(DTTABLE.Rows[k]["ItemCode"]);
                                DTTABLE.Rows[k]["ItemName"] = Convert.ToString(DTTABLE.Rows[k]["ItemName"]);
                                DTTABLE.Rows[k]["ItemDetailsId"] = Convert.ToString(DTTABLE.Rows[k]["ItemDetailsId"]);
                                DTTABLE.Rows[k]["ItemDesc"] = Convert.ToString(DTTABLE.Rows[k]["ItemDesc"]);
                                DTTABLE.Rows[k]["Location"] = Convert.ToString(DTTABLE.Rows[k]["Location"]);
                                DTTABLE.Rows[k]["AvlQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvlQty"]);
                                DTTABLE.Rows[k]["MinStockLevel"] = Convert.ToDecimal(DTTABLE.Rows[k]["MinStockLevel"]);
                                DTTABLE.Rows[k]["DeliveryPeriod"] = Convert.ToDecimal(DTTABLE.Rows[k]["DeliveryPeriod"]);
                                DTTABLE.Rows[k]["AvgRate"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvgRate"]);
                                DTTABLE.Rows[k]["AvgRateDate"] = Convert.ToString(DTTABLE.Rows[k]["AvgRateDate"]);
                                DTTABLE.Rows[k]["Vendor"] = Convert.ToString(DTTABLE.Rows[k]["Vendor"]);
                                DTTABLE.Rows[k]["VendorId"] = Convert.ToString(DTTABLE.Rows[k]["VendorId"]);
                                DTTABLE.Rows[k]["Rate"] = Convert.ToString(DTTABLE.Rows[k]["Rate"]);
                                ViewState["Template"] = DTTABLE;
                                GrdTemplate.DataSource = DTTABLE;
                                GrdTemplate.DataBind();
                                DtEditPO = (DataTable)ViewState["Template"];
                            }
                            else
                            {
                                DTROW = DTTABLE.NewRow();
                                DTROW["#"] = Convert.ToInt32(ds.Tables[0].Rows[0]["#"]);
                                DTROW["ItemCode"] = Convert.ToString(ds.Tables[0].Rows[0]["ItemCode"]);
                                DTROW["ItemName"] = Convert.ToString(ds.Tables[0].Rows[0]["ItemName"]);
                                DTROW["ItemDetailsId"] = Convert.ToString(ds.Tables[0].Rows[0]["ItemDetailsId"]);
                                DTROW["ItemDesc"] = Convert.ToString(ds.Tables[0].Rows[0]["ItemDesc"]);
                                DTROW["Location"] = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
                                DTROW["AvlQty"] = Convert.ToDecimal(ds.Tables[0].Rows[0]["AvlQty"]);
                                DTROW["MinStockLevel"] = Convert.ToDecimal(ds.Tables[0].Rows[0]["MinStockLevel"]);
                                DTROW["DeliveryPeriod"] = Convert.ToDecimal(ds.Tables[0].Rows[0]["DeliveryPeriod"]);
                                DTROW["AvgRate"] = Convert.ToDecimal(ds.Tables[0].Rows[0]["AvgRate"]);
                                DTROW["AvgRateDate"] = Convert.ToString(ds.Tables[0].Rows[0]["AvgRateDate"]);
                                DTROW["Vendor"] = Convert.ToString(ds.Tables[0].Rows[0]["Vendor"]);
                                DTROW["VendorId"] = Convert.ToString(ds.Tables[0].Rows[0]["VendorId"]);
                                DTROW["Rate"] = Convert.ToString(ds.Tables[0].Rows[0]["Rate"]);
                                DTTABLE.Rows.Add(DTROW);
                                ViewState["Template"] = DTTABLE;
                                GrdTemplate.DataSource = DTTABLE;
                                GrdTemplate.DataBind();
                                DtEditPO = (DataTable)ViewState["Template"];
                            }
                        }
                    }
                }
            }
        }

        public void CheckBoxCheck()
        {
            Ds = new DataSet();
            Ds = Obj_Template.GetDuplicateName(txtTemplateFor.Text, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                clear = false;
            }
            else
            {
                clear = true;
            }
        }

        public void BindItemToGridAll(DataSet ds)  //For Binding To Grid All
        {
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                int ItemID_All = Convert.ToInt32(ds.Tables[0].Rows[j]["#"]);
                int ItemDetailsId = Convert.ToInt32(ds.Tables[0].Rows[j]["ItemDetailsId"]);
                if (ViewState["Template"] != null)
                {
                    bool DupFlag = false;
                    int k = 0;
                    DataTable DTTABLE = (DataTable)ViewState["Template"];
                    DataRow DTROW = null;
                    // int ItemID = Convert.ToInt32(DTTABLE.Rows[j]["#"]);
                    if (DTTABLE.Rows.Count > 0)
                    {
                        //if (DTTABLE.Rows.Count == 1 && (DTTABLE.Rows[0]["#"].ToString()) == "0")
                        //{
                        //    DTTABLE.Rows.RemoveAt(0);
                        //    //DupFlag = false;
                        //}
                        if (ViewState["GridIndex"] == null)
                        {
                            for (int i = 0; i < DTTABLE.Rows.Count; i++)
                            {
                                int ItemID_Single = Convert.ToInt32(DTTABLE.Rows[i]["#"]);
                                int ItemDetailsId_Single = Convert.ToInt32(DTTABLE.Rows[i]["ItemDetailsId"]);
                                if (DTTABLE.Rows.Count == 1 && (DTTABLE.Rows[0]["#"].ToString()) == "0")
                                {
                                    DTTABLE.Rows.RemoveAt(0);
                                    //DupFlag = false;
                                }
                                if ((Convert.ToInt32(ItemID_Single) == Convert.ToInt32(ItemID_All)) && Convert.ToInt32(ItemDetailsId_Single) == Convert.ToInt32(ItemDetailsId))
                                {
                                    DupFlag = true;
                                    k = i;
                                }
                            }
                            if (DupFlag == true)
                            {
                                DTTABLE.Rows[k]["#"] = Convert.ToString(DTTABLE.Rows[k]["#"]);
                                DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(DTTABLE.Rows[k]["ItemCode"]);
                                DTTABLE.Rows[k]["ItemName"] = Convert.ToString(DTTABLE.Rows[k]["ItemName"]);
                                DTTABLE.Rows[k]["ItemDetailsId"] = Convert.ToString(DTTABLE.Rows[k]["ItemDetailsId"]);
                                DTTABLE.Rows[k]["ItemDesc"] = Convert.ToString(DTTABLE.Rows[k]["ItemDesc"]);
                                DTTABLE.Rows[k]["DrawingNo"] = Convert.ToString(DTTABLE.Rows[k]["DrawingNo"]);                          
                                DTTABLE.Rows[k]["Location"] = Convert.ToString(DTTABLE.Rows[k]["Location"]);
                                DTTABLE.Rows[k]["AvlQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvlQty"]);
                                DTTABLE.Rows[k]["MinStockLevel"] = Convert.ToDecimal(DTTABLE.Rows[k]["MinStockLevel"]);
                                DTTABLE.Rows[k]["DeliveryPeriod"] = Convert.ToDecimal(DTTABLE.Rows[k]["DeliveryPeriod"]);
                                DTTABLE.Rows[k]["AvgRate"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvgRate"]);
                                DTTABLE.Rows[k]["AvgRateDate"] = Convert.ToString(DTTABLE.Rows[k]["AvgRateDate"]);
                                DTTABLE.Rows[k]["Vendor"] = Convert.ToString(DTTABLE.Rows[k]["Vendor"]);
                                DTTABLE.Rows[k]["VendorId"] = Convert.ToString(DTTABLE.Rows[k]["VendorId"]);
                                DTTABLE.Rows[k]["Rate"] = Convert.ToString(DTTABLE.Rows[k]["Rate"]);
                                DTTABLE.Rows[k]["UOM"] = Convert.ToString(DTTABLE.Rows[k]["UOM"]);
                                ViewState["Template"] = DTTABLE;
                                GrdTemplate.DataSource = DTTABLE;
                                GrdTemplate.DataBind();
                                DtEditPO = (DataTable)ViewState["Template"];
                            }
                            else
                            {
                                DTROW = DTTABLE.NewRow();

                                DTROW["#"] = Convert.ToString(ds.Tables[0].Rows[j]["#"]);
                                DTROW["ItemCode"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemCode"]);
                                DTROW["ItemName"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemName"]);
                                DTROW["ItemDetailsId"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemDetailsId"]);
                                DTROW["ItemDesc"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemDesc"]);
                                DTROW["DrawingNo"] = Convert.ToString(ds.Tables[0].Rows[j]["DrawingNo"]);
                                DTROW["Location"] = Convert.ToString(ds.Tables[0].Rows[j]["Location"]);
                                DTROW["AvlQty"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["AvlQty"]);
                                DTROW["MinStockLevel"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["MinStockLevel"]);
                                DTROW["DeliveryPeriod"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["DeliveryPeriod"]);
                                DTROW["AvgRate"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["AvgRate"]);
                                DTROW["AvgRateDate"] = Convert.ToString(ds.Tables[0].Rows[j]["AvgRateDate"]);
                                DTROW["Vendor"] = Convert.ToString(ds.Tables[0].Rows[j]["Vendor"]);
                                DTROW["VendorId"] = Convert.ToString(ds.Tables[0].Rows[j]["VendorId"]);
                                DTROW["Rate"] = Convert.ToString(ds.Tables[0].Rows[j]["Rate"]);
                                DTROW["UOM"] = Convert.ToString(ds.Tables[0].Rows[j]["UOM"]);
                                DTTABLE.Rows.Add(DTROW);
                                ViewState["Template"] = DTTABLE;
                                GrdTemplate.DataSource = DTTABLE;
                                GrdTemplate.DataBind();
                                DtEditPO = (DataTable)ViewState["Template"];
                                int g = k;
                                ViewState["GridIndex"] = g++;

                            }
                        }
                        else
                        {
                            for (int i = 0; i < DTTABLE.Rows.Count; i++)
                            {
                                int ItemID_Single = Convert.ToInt32(DTTABLE.Rows[i]["#"]);
                                int ItemDetailsId_Single = Convert.ToInt32(DTTABLE.Rows[i]["ItemDetailsId"]);
                                if ((Convert.ToInt32(ItemID_Single) == Convert.ToInt32(ItemID_All)) && Convert.ToInt32(ItemDetailsId_Single) == Convert.ToInt32(ItemDetailsId))
                                {
                                    DupFlag = true;
                                    k = i;
                                }
                            }
                            if (DupFlag == true)
                            {
                                // DTROW = DTTABLE.NewRow();
                                int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                                DTTABLE.Rows[k]["#"] = Convert.ToString(DTTABLE.Rows[k]["#"]);
                                DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(DTTABLE.Rows[k]["ItemCode"]);
                                DTTABLE.Rows[k]["ItemName"] = Convert.ToString(DTTABLE.Rows[k]["ItemName"]);
                                DTTABLE.Rows[k]["ItemDetailsId"] = Convert.ToString(DTTABLE.Rows[k]["ItemDetailsId"]);
                                DTTABLE.Rows[k]["ItemDesc"] = Convert.ToString(DTTABLE.Rows[k]["ItemDesc"]);
                                DTTABLE.Rows[k]["DrawingNo"] = Convert.ToString(DTTABLE.Rows[k]["DrawingNo"]);                          
                                DTTABLE.Rows[k]["Location"] = Convert.ToString(DTTABLE.Rows[k]["Location"]);
                                DTTABLE.Rows[k]["AvlQty"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvlQty"]);
                                DTTABLE.Rows[k]["MinStockLevel"] = Convert.ToDecimal(DTTABLE.Rows[k]["MinStockLevel"]);
                                DTTABLE.Rows[k]["DeliveryPeriod"] = Convert.ToDecimal(DTTABLE.Rows[k]["DeliveryPeriod"]);
                                DTTABLE.Rows[k]["AvgRate"] = Convert.ToDecimal(DTTABLE.Rows[k]["AvgRate"]);
                                DTTABLE.Rows[k]["AvgRateDate"] = Convert.ToString(DTTABLE.Rows[k]["AvgRateDate"]);
                                DTTABLE.Rows[k]["Vendor"] = Convert.ToString(DTTABLE.Rows[k]["Vendor"]);
                                DTTABLE.Rows[k]["VendorId"] = Convert.ToString(DTTABLE.Rows[k]["VendorId"]);
                                DTTABLE.Rows[k]["Rate"] = Convert.ToString(DTTABLE.Rows[k]["Rate"]);
                                DTTABLE.Rows[k]["UOM"] = Convert.ToString(DTTABLE.Rows[k]["UOM"]);
                                //DTTABLE.Rows.Add(DTROW);
                                ViewState["Template"] = DTTABLE;
                                GrdTemplate.DataSource = DTTABLE;
                                GrdTemplate.DataBind();
                                DtEditPO = (DataTable)ViewState["Template"];
                            }
                            else
                            {
                                DTROW = DTTABLE.NewRow();
                                DTROW["#"] = Convert.ToString(ds.Tables[0].Rows[j]["#"]);
                                DTROW["ItemCode"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemCode"]);
                                DTROW["ItemName"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemName"]);
                                DTROW["ItemDetailsId"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemDetailsId"]);
                                DTROW["ItemDesc"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemDesc"]);
                                DTROW["DrawingNo"] = Convert.ToString(ds.Tables[0].Rows[j]["DrawingNo"]);
                                DTROW["Location"] = Convert.ToString(ds.Tables[0].Rows[j]["Location"]);
                                DTROW["AvlQty"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["AvlQty"]);
                                DTROW["MinStockLevel"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["MinStockLevel"]);
                                DTROW["DeliveryPeriod"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["DeliveryPeriod"]);
                                DTROW["AvgRate"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["AvgRate"]);
                                DTROW["AvgRateDate"] = Convert.ToString(ds.Tables[0].Rows[j]["AvgRateDate"]);
                                DTROW["Vendor"] = Convert.ToString(ds.Tables[0].Rows[j]["Vendor"]);
                                DTROW["VendorId"] = Convert.ToString(ds.Tables[0].Rows[j]["VendorId"]);
                                DTROW["Rate"] = Convert.ToString(ds.Tables[0].Rows[j]["Rate"]);
                                DTROW["UOM"] = Convert.ToString(ds.Tables[0].Rows[j]["UOM"]);
                                DTTABLE.Rows.Add(DTROW);
                                ViewState["Template"] = DTTABLE;
                                GrdTemplate.DataSource = DTTABLE;
                                GrdTemplate.DataBind();
                                DtEditPO = (DataTable)ViewState["Template"];
                            }
                        }
                    }
                }
            }
        }

        public void IsCheckBoxChecked()
        {
            for (int i = 0; i < GrdTemplate.Rows.Count; i++)
            {
                CheckBox GrdSelectAll = (CheckBox)GrdTemplate.Rows[i].Cells[1].FindControl("GrdSelectAll");
                if (GrdSelectAll.Checked == true)
                {
                    CheckFlag = true;
                    break;
                }
                else
                {
                    CheckFlag = false;
                }
            }

        }        
    #endregion    

    protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MakeEmptyForm();
            // CheckUserRight();

            database db = new database();
            drptemplate.DataSource = db.Displaygrid("select TemplateID, TemplateName  from TemplateMaster");
            drptemplate.DataValueField = "TemplateID";
            drptemplate.DataTextField = "TemplateName";
            drptemplate.DataBind();
            drptemplate.Items.Insert(0, "Select Template");
            }
        }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Ds = new DataSet();
            CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            Ds = Obj_Template.GetItemName(CategoryID,out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ddlItem.DataSource = Ds.Tables[0];
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "#";
                ddlItem.DataBind();
            }
            if (Ds.Tables.Count > 0 && Ds.Tables[1].Rows.Count > 0)
            {
                ddlsubcategory.DataSource = Ds.Tables[1];
                ddlsubcategory.DataTextField = "Name";
                ddlsubcategory.DataValueField = "#";
                ddlsubcategory.DataBind();
                ddlCategory.Focus();
            }
            else
            {
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
            Ds = new DataSet();
            CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            int SubCategoryID = Convert.ToInt32(ddlsubcategory.SelectedValue);
            Ds = Obj_Template.GetItemNameonSubCategory(CategoryID,SubCategoryID , out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ddlItem.DataSource = Ds.Tables[0];
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "#";
                ddlItem.DataBind();
                ddlsubcategory.Focus();
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void BtnAddAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedIndex > 0)
            {
                Ds = new DataSet();
                CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                Ds = Obj_Template.BindItemsToGrid(Convert.ToInt32(ddlCategory.SelectedValue), "ALL", out StrError);
                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    CheckDuplicateAddAll(CategoryID, Ds);
                    if (flag1 == false)
                    {
                        BindItemToGridAll(Ds);
                    }
                    else
                    {
                        obj_Comman.ShowPopUpMsg("Items Already Present...", this.Page);
                    }
                    for (int t = 0; t < GrdTemplate.Rows.Count; t++)
                    {
                        ((CheckBox)GrdTemplate.Rows[t].Cells[1].FindControl("GrdSelectAll")).Checked = true;

                    }
                }
                else
                {
                    SetInitialRowGrdTemplate();
                }
                Ds = null;
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Please Select Category...", this.Page);

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void BtnAddSubCategory_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlsubcategory.SelectedIndex > 0)
            {
                Ds = new DataSet();
                CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                Ds = Obj_Template.BindSUBItemsToGrid(Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlsubcategory.SelectedValue), "ADDSUB", out StrError);
                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    CheckDuplicateAddAll(CategoryID, Ds);
                    if (flag1 == false)
                    {
                        BindItemToGridAll(Ds);
                    }
                    else
                    {
                        obj_Comman.ShowPopUpMsg("Items Already Present...", this.Page);
                    }
                    for (int t = 0; t < GrdTemplate.Rows.Count; t++)
                    {
                        ((CheckBox)GrdTemplate.Rows[t].Cells[1].FindControl("GrdSelectAll")).Checked = true;
                    }
                }
                else
                {
                    SetInitialRowGrdTemplate();
                }
                Ds = null;
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Please Select Category...", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlItem.SelectedIndex > 0)
            {
                Ds = new DataSet();
                CategoryID = Convert.ToInt32(ddlItem.SelectedValue);
                CheckDuplicate(Convert.ToInt32(ddlItem.SelectedValue));
                if (flag == false)
                {
                    Ds = Obj_Template.BindItemsToGrid(Convert.ToInt32(ddlItem.SelectedValue), "ADD", out StrError);
                    if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                    {
                        //BindItemToGrid(Ds);
                        BindItemToGridAll(Ds);
                       
                        for (int t = 0; t < GrdTemplate.Rows.Count; t++)
                        {
                            ((CheckBox)GrdTemplate.Rows[t].Cells[1].FindControl("GrdSelectAll")).Checked = true;

                        }
                    }
                    else
                    {
                        obj_Comman.ShowPopUpMsg("Record Not Found", this.Page);
                    }
                    Ds = null;
                }
                else
                {
                    obj_Comman.ShowPopUpMsg("Item Already Present...", this.Page);
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Please Select Item...", this.Page);
            }
        }
      
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
        //ddlItem.DataSource = null;
        //ddlItem.DataBind();
        ddlItem.ClearSelection();        
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        IsCheckBoxChecked();
        if (CheckFlag == true)
        {
            CheckBoxCheck();
            if (clear == true)
            {
                Entity_Template.TemplateName = txtTemplateFor.Text;
                Entity_Template.TemplateDate = txtTemplateDate.Text;
                Entity_Template.LoginID = Convert.ToInt32(Session["UserId"]);
                Entity_Template.LoginDate = DateTime.Now.ToShortDateString();
                insertrow = Obj_Template.Insert_Template(ref Entity_Template, out StrError);
                int MaxID = insertrow;
                int insertdtls = 0;
                if (insertrow != 0)
                {
                    for (int g = 0; g < GrdTemplate.Rows.Count; g++)
                    {
                        if (((CheckBox)GrdTemplate.Rows[g].Cells[1].FindControl("GrdSelectAll")).Checked == true)
                        {
                            Label LblEntryId = (Label)GrdTemplate.Rows[g].FindControl("LblEntryId");
                            Label lblVendorId = (Label)GrdTemplate.Rows[g].FindControl("lblVendorId");
                            Label lblRate = (Label)GrdTemplate.Rows[g].FindControl("lblRate");
                            Entity_Template.ItemDtlsID = Convert.ToInt32(GrdTemplate.Rows[g].Cells[4].Text);
                            Entity_Template.TemplateID = MaxID;
                            Entity_Template.ItemID = Convert.ToInt32(LblEntryId.Text);
                            Entity_Template.VendorID = Convert.ToInt32(lblVendorId.Text);
                            Entity_Template.Rate = Convert.ToDecimal(lblRate.Text);
                            Entity_Template.QTY = ((TextBox)GrdTemplate.Rows[g].FindControl("GRDQTY")).Text == "" ? Convert.ToDecimal("0") : Convert.ToDecimal(((TextBox)GrdTemplate.Rows[g].FindControl("GRDQTY")).Text);
                            Entity_Template.StrCondition = Convert.ToString(((TextBox)GrdTemplate.Rows[g].FindControl("GRDREMARK")).Text);
                            insertdtls = Obj_Template.Insert_TemplateDetails(ref Entity_Template,out StrError);
                        }
                    }


           


                }
                if (insertdtls != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Saved Successfully!", this.Page);
                    MakeEmptyForm();
                }
                else
                {
                    obj_Comman.ShowPopUpMsg(StrError, this.Page);
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Tempalate Name Already Present..!", this.Page);
                MakeEmptyForm();
            }
        }
        else
        {
            obj_Comman.ShowPopUpMsg("Please Select Atleast One Item..!", this.Page);
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            IsCheckBoxChecked();
            if (CheckFlag == true)
            {
                if (ViewState["EditID"] != null)
                {
                    Entity_Template.TemplateID = Convert.ToInt32(ViewState["EditID"]);
                }
                Entity_Template.TemplateName = txtTemplateFor.Text;
                Entity_Template.TemplateDate = txtTemplateDate.Text;
                Entity_Template.LoginID = Convert.ToInt32(Session["UserId"]);
                Entity_Template.LoginDate = DateTime.Now.ToShortDateString();
                insertrow = Obj_Template.Update_Template(ref Entity_Template, out StrError);
                //int MaxID = insertrow;
                int UpdateRow = 0;
                if (insertrow != 0)
                {
                    for (int g = 0; g < GrdTemplate.Rows.Count; g++)
                    {
                        if (((CheckBox)GrdTemplate.Rows[g].Cells[1].FindControl("GrdSelectAll")).Checked == true)
                        {
                            Label LblEntryId = (Label)GrdTemplate.Rows[g].Cells[0].FindControl("LblEntryId");
                            Label lblVendorId = (Label)GrdTemplate.Rows[g].Cells[13].FindControl("lblVendorId");
                            Label lblRate = (Label)GrdTemplate.Rows[g].Cells[11].FindControl("lblRate");
                            //Entity_Template.TemplateID = Convert.ToInt32(ViewState["EditID"]);
                            Entity_Template.ItemDtlsID = Convert.ToInt32(GrdTemplate.Rows[g].Cells[4].Text);
                            Entity_Template.ItemID = Convert.ToInt32(LblEntryId.Text);
                            Entity_Template.VendorID = Convert.ToInt32(lblVendorId.Text);//value not get
                            Entity_Template.Rate = Convert.ToDecimal(lblRate.Text);
                            Entity_Template.QTY = Convert.ToDecimal(((TextBox)GrdTemplate.Rows[g].FindControl("GRDQTY")).Text);
                            Entity_Template.StrCondition = Convert.ToString(((TextBox)GrdTemplate.Rows[g].FindControl("GRDREMARK")).Text);
                            UpdateRow = Obj_Template.Insert_TemplateDetails(ref Entity_Template, out StrError);
                        }
                    }



             

                }
                if (UpdateRow != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Updated Successfully!", this.Page);
                    MakeEmptyForm();
                }
                else
                {
                    obj_Comman.ShowPopUpMsg(StrError, this.Page);
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Please Select Atleast One Item..!", this.Page);
            }
        }
        catch (Exception ex)
        {            
        }
    }

    protected void GrdTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int itemid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "#"));
                int ItemDtlsId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ItemDetailsId"));
                Ds = new DataSet();
                Ds = Obj_Template.BindToVendorGrid(itemid,ItemDtlsId,out StrError);
                if (Ds.Tables.Count > 0)
                {
                    if(Ds.Tables[0].Rows.Count > 0)
                    {
                      GridTemplateVendor.DataSource = Ds.Tables[0];
                      GridTemplateVendor.DataBind();
                    }
                    if (Ds.Tables[1].Rows.Count > 0)
                    {
                        e.Row.Cells[7].Text=Ds.Tables[1].Rows[0][0].ToString();
                    }
                }
                else
                {
                    SetInitialRow_GridTemplateVendor();
                }
               
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }       
    }

    protected void ReportGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case ("Select"):
                {
                    ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                    Ds = Obj_Template.BindForEditGridTemplate(Convert.ToInt32(e.CommandArgument), out StrError);



                    if (Ds.Tables.Count > 0)
                    {
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            txtTemplateFor.Text = Ds.Tables[0].Rows[0]["TemplateName"].ToString();
                            txtTemplateDate.Text = Ds.Tables[0].Rows[0]["TemplateDate"].ToString();
                        }
                        if (Ds.Tables[1].Rows.Count > 0)
                        {
                            ViewState["GRDVID"] = Ds.Tables[1].Rows[0]["VendorID"];//seamless education academy 
                            ViewState["Template"] = Ds.Tables[1];
                            GrdTemplate.DataSource = Ds.Tables[1];
                            GrdTemplate.DataBind();


                            for (int t = 0; t < GrdTemplate.Rows.Count; t++)
                            {
                                ((CheckBox)GrdTemplate.Rows[t].Cells[1].FindControl("GrdSelectAll")).Checked = true;

                            }
                            EditTemp = 0;
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            ddlItem.ClearSelection();
                            ddlCategory.ClearSelection();
                        }
                        else
                        {
                            SetInitialRowGrdTemplate();
                        }
                    }
                }
                break;
            case ("Delete"):
                {
                    ViewState["DeleteID"] = Convert.ToInt32(e.CommandArgument);
                    Entity_Template.TemplateID = Convert.ToInt32(e.CommandArgument);
                    Entity_Template.LoginID = Convert.ToInt32(Session["UserId"]);
                    Entity_Template.LoginDate = DateTime.Now.ToShortDateString();
                    int DeletedRow = Obj_Template.Delete_Template(ref Entity_Template, out StrError);
                    if (DeletedRow!=0)
                    {
                        obj_Comman.ShowPopUpMsg("Record Deleted Successfully!", this.Page);
                        MakeEmptyForm();
                    }
                }
                break;
        }
    }

    protected void ReportGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMTemplate Obj_Template = new DMTemplate();
        String[] SearchList = Obj_Template.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            StrCondition = string.Empty;
            StrCondition = TxtSearch.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_Template.GetTemplateList(StrCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                //ViewState["CurrentTable"] = Ds.Tables[1];
                ReportGrid.DataSource = Ds.Tables[0];
                ReportGrid.DataBind();
                Ds = null;
            }
            else
            {
                ReportGrid.DataSource = null;
                ReportGrid.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void GrdSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox GrdSelectAllHeader1 = (CheckBox)sender;
        GridViewRow row = (GridViewRow)GrdSelectAllHeader1.NamingContainer;
        int CurrRow = row.RowIndex;
        if (GrdSelectAllHeader1.Checked == true)
        {
            for (int j = 0; j < GrdTemplate.Rows.Count; j++)
            {
                CheckBox GrdSelectAll = (CheckBox)GrdTemplate.Rows[j].Cells[1].FindControl("GrdSelectAll");
                GrdSelectAll.Checked = true;
            }
        }
        else
        {
            for (int j = 0; j < GrdTemplate.Rows.Count; j++)
            {
                CheckBox GrdSelectAll = (CheckBox)GrdTemplate.Rows[j].Cells[1].FindControl("GrdSelectAll");
                GrdSelectAll.Checked = false;
            }
        }
    }

    protected void ChkShowProcess_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkShowProcess1 = (CheckBox)sender;
        GridViewRow row = (GridViewRow)ChkShowProcess1.NamingContainer;
        GridView GridTemplateVendor = (GridView)row.FindControl("GridTemplateVendor");  
        int CurrRow=row.RowIndex;
        int Pos = 0;
        if (ChkShowProcess1.Checked == true)
        {
            if (GridTemplateVendor.Rows.Count > 0)
            {
                for (int j = 0; j < GridTemplateVendor.Rows.Count; j++)
                {
                    RadioButton RBVendor = (RadioButton)GridTemplateVendor.Rows[j].Cells[2].FindControl("RBVendor");
                    if (RBVendor.Checked == true)
                    {
                        Pos = j;
                        string VendorName = GridTemplateVendor.Rows[j].Cells[3].Text;
                        ((Label)GrdTemplate.Rows[CurrRow].FindControl("lblVendor")).Text = VendorName;
                        decimal Rate = string.IsNullOrEmpty(GridTemplateVendor.Rows[j].Cells[4].Text) ? 0 : Convert.ToDecimal(GridTemplateVendor.Rows[j].Cells[4].Text);
                        ((Label)GrdTemplate.Rows[CurrRow].FindControl("lblRate")).Text = Rate.ToString();
                        ((Label)GrdTemplate.Rows[CurrRow].FindControl("lblVendorId")).Text = GridTemplateVendor.Rows[j].Cells[1].Text;
                    }
                }
                ((RadioButton)GridTemplateVendor.Rows[Pos].Cells[2].FindControl("RBVendor")).Checked = false;
                ChkShowProcess1.Checked = false;
            }
            else
            {
                obj_Comman.ShowPopUpMsg("There is no Vendor",this.Page);
            }
        }
        Panel PnlGrid = (Panel)row.FindControl("PnlGrid");
        PopupControlExtender popup = AjaxControlToolkit.PopupControlExtender.GetProxyForCurrentPopup(Page);
        popup.Commit("Popup");          
    }

    protected void ReportGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.ReportGrid.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            Ds = Obj_Template.GetGridTempalate(StrCondition, out StrError);

            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ReportGrid.DataSource = Ds.Tables[0];
                this.ReportGrid.DataBind();
                if (FlagDel && FlagEdit)
                {
                    foreach (GridViewRow GRow in ReportGrid.Rows)
                    {
                        GRow.FindControl("ImgBtnDelete").Visible = false;
                        GRow.FindControl("ImageGridEdit").Visible = false;
                    }
                }
                else if (FlagDel)
                {
                    foreach (GridViewRow GRow in ReportGrid.Rows)
                    {
                        GRow.FindControl("ImgBtnDelete").Visible = false;
                    }
                }
                else if (FlagEdit)
                {

                    foreach (GridViewRow GRow in ReportGrid.Rows)
                    {
                        GRow.FindControl("ImageGridEdit").Visible = false;
                    }
                }
            }
            else
            {
                //SetInitialRow_ReportGrid();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int itemid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "#"));
                int ItemDtlsId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ItemDetailsId"));
                Ds = new DataSet();
                Ds = Obj_Template.BindToVendorGrid(itemid, ItemDtlsId, out StrError);
                if (Ds.Tables.Count > 0)
                {
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        GridTemplateVendor.DataSource = Ds.Tables[0];
                        GridTemplateVendor.DataBind();
                    }
                    if (Ds.Tables[1].Rows.Count > 0)
                    {
                        e.Row.Cells[7].Text = Ds.Tables[1].Rows[0][0].ToString();
                    }
                }
                else
                {
                    SetInitialRow_GridTemplateVendor();
                }

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void drptemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
        int id = Int32.Parse(drptemplate.SelectedValue);
        Ds = Obj_Template.BindForEditGridTemplate(Convert.ToInt32(id), out StrError);
        if (Ds.Tables.Count > 0)
        {
            if (Ds.Tables[0].Rows.Count > 0)
            {
                //txtTemplateFor.Text = Ds.Tables[0].Rows[0]["TemplateName"].ToString();
               // txtTemplateDate.Text = Ds.Tables[0].Rows[0]["TemplateDate"].ToString();
            }
            if (Ds.Tables[1].Rows.Count > 0)
            {
                ViewState["GRDVID"] = Ds.Tables[1].Rows[0]["VendorID"];//seamless education academy 
                ViewState["Template"] = Ds.Tables[1];
               
                for (int t = 0; t < GrdTemplate.Rows.Count; t++)
                {
                  //  ((CheckBox)GridView1.Rows[t].Cells[1].FindControl("GrdSelectAll")).Checked = true;

                }
                EditTemp = 0;
           
                ddlItem.ClearSelection();
                ddlCategory.ClearSelection();
            }
            else
            {
                SetInitialRowGrdTemplate();
            }
        }
    }

    protected void GrdSelectAllHeader_CheckedChanged(object sender, EventArgs e)
    {
       
    }
}
