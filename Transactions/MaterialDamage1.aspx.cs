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

public partial class Transactions_MaterialDamage1 : System.Web.UI.Page
{
    #region[Variables]
        DMDamage Obj_damage = new DMDamage();
        Damage Entity_damage = new Damage();
        CommanFunction obj_Comm = new CommanFunction();
        DMPurchaseOrder Obj_PurchaseOrder = new DMPurchaseOrder();
        DataSet DS = new DataSet();
        DataSet DSS = new DataSet();
        DataSet DSNEW = new DataSet();
        private static DataTable DSItem = new DataTable();
        private static DataTable DSLocation = new DataTable();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        string DamageNum = string.Empty;
        public static int ItemId = 0, LocationId=0,DamageThrough=0,DamageId=0;
        public static bool chkflag = false, chkflag1 = false;
        private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
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

                        DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Material Damage Register'");
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
                            ReportGridDtls.Visible = false;
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
                            foreach (GridViewRow GRow in ReportGridDtls.Rows)
                            {
                                GRow.FindControl("ImgBtnDelete").Visible = false;
                                GRow.FindControl("ImageGridEdit").Visible = false;
                                GRow.FindControl("ImgBtnPrint").Visible = false;
                            }
                            BtnUpdate.Visible = false;
                            FlagDel = true;
                            FlagEdit = true;
                            FlagPrint = true;
                        }
                        else
                        {
                            //Checking Delete Right ========
                            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                            {
                                foreach (GridViewRow GRow in ReportGridDtls.Rows)
                                {
                                    GRow.FindControl("ImgBtnDelete").Visible = false;
                                    FlagDel = true;
                                }
                            }

                            //Checking Edit Right ========
                            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                            {
                                foreach (GridViewRow GRow in ReportGridDtls.Rows)
                                {
                                    GRow.FindControl("ImageGridEdit").Visible = false;
                                    FlagEdit = true;
                                }
                            }

                            //Checking Print Right ========
                            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                            {
                                foreach (GridViewRow GRow in ReportGridDtls.Rows)
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

        private void MakeEmptyForm()
        {
            if(!FlagAdd)
             BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            BtnDelete.Visible = false;
            t1.Visible = true;
            Fillcombo();
            TxtDamageDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            ddlInwardNo.SelectedValue = "0";
            lblInwardNo.Text = "";
            lblInwardDate.Text = "";
            lblPONO.Text = "";
            lblSuppName.Text = "";
            TxtSearch.Text = "";
            lstSupplierRate.Items.Clear();
            
            rdoDamagedThrough.SelectedValue = "0";

            FS_InwardDtls.Visible = true;
            FS_ItemDtls.Visible = false;
            FieldsetInward.Visible = true;
            FieldsetItem.Visible = false;

            ViewState["EditId"] = null;
            ViewState["DamagedDtls"] = null;
            SetInitialRowInwardWise();
            //SetInitialRow();
            BindReportGrid(StrCondition);

            #region[UserRights]
            if (FlagEdit)
            {
                foreach (GridViewRow GRow in ReportGridDtls.Rows)
                {
                    GRow.FindControl("ImageGridEdit").Visible = false;
                }
            }
            if (FlagDel)
            {
                foreach (GridViewRow GRow in ReportGridDtls.Rows)
                {
                    GRow.FindControl("ImgBtnDelete").Visible = false;
                }
            }
            if (FlagPrint)
            {
                foreach (GridViewRow GRow in ReportGridDtls.Rows)
                {
                    GRow.FindControl("ImgBtnPrint").Visible = false;
                }
            }
            #endregion
        }

        private void MakeControlEmpty()
        {
            ViewState["GridIndex"] = null;
            ViewState["GrdInward"] = null;
            ViewState["DamagedDtls"] = null;
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("ItemId", typeof(Int32));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("ItemDesc", typeof(string));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("UnitId", typeof(Int32));
            dt.Columns.Add("StockInHand", typeof(string));
            dt.Columns.Add("DamageQty", typeof(decimal));
            dt.Columns.Add("InwardRate", typeof(decimal));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("Reason", typeof(string));
            //dt.Columns.Add("OrderQty", typeof(decimal));
            //dt.Columns.Add("InwardQty", typeof(decimal));
           //dt.Columns.Add("RemainInwardQty", typeof(decimal));

            dr = dt.NewRow();

            dr["ItemId"] = 0;
            dr["ItemCode"]="";
            dr["ItemName"] = "";
            dr["ItemDesc"] = "";
            dr["Unit"] = "";
            dr["UnitId"] = 0;
            dr["StockInHand"] = 0.0;
            dr["DamageQty"] = 0.0;
            dr["InwardRate"] = 0.0;
            dr["Amount"] = 0.0;
            dr["Reason"] = "";

            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
            ViewState["DamagedDtls"] = dt;

            GRDItemWise.DataSource = dt;
            GRDItemWise.DataBind();
        }

        private void SetInitialRowInwardWise()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("InwardNo", typeof(string));
            dt.Columns.Add("ItemId", typeof(Int32));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("ItemDesc", typeof(string));
            dt.Columns.Add("InwardQty", typeof(decimal));
            dt.Columns.Add("UnitId", typeof(Int32));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("ReturnQty", typeof(decimal));
            dt.Columns.Add("PreviousDamageQty", typeof(decimal));
            dt.Columns.Add("DamageQty", typeof(decimal));
            dt.Columns.Add("RemainInwardQty", typeof(decimal));
            dt.Columns.Add("InwardRate", typeof(decimal));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("Reason", typeof(string));

            dr = dt.NewRow();

            dr["InwardNo"] = 0;
            dr["ItemId"] = 0;
            dr["ItemCode"] = "";
            dr["ItemName"] = "";
            dr["ItemDesc"] = "";
            dr["InwardQty"] = 0.0;
            dr["UnitId"] = 0;
            dr["Unit"] = "";
            dr["ReturnQty"] = 0.0;
            dr["PreviousDamageQty"] = 0.0;
            dr["DamageQty"] = 0.0;
            dr["RemainInwardQty"] = 0.0;
            dr["InwardRate"] = 0.0;
            dr["Amount"] = 0.0;
            dr["Reason"] = "";

            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;
            ViewState["InwardDetails"] = dt;

            GrdInwardWiseDetails.DataSource = dt;
            GrdInwardWiseDetails.DataBind();
        }

        private void Fillcombo()
        {
            try
            {
                string COND = string.Empty;
                if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
                {
                    COND = COND + " AND InwardRegisterDtls.LocID=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
                }
                DS = Obj_damage.FillCombo(COND ,out StrError);
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        ddlInwardNo.DataSource = DS.Tables[0];
                        ddlInwardNo.DataTextField = "InwardNo";
                        ddlInwardNo.DataValueField = "InwardId";
                        ddlInwardNo.DataBind();
                    }
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        ddlCategory.DataSource = DS.Tables[1];
                        ddlCategory.DataTextField = "CategoryName";
                        ddlCategory.DataValueField = "CategoryId";
                        ddlCategory.DataBind();
                    }
                    if (DS.Tables[2].Rows.Count > 0)
                    {
                        ddlItem.DataSource = DS.Tables[2];
                        ddlItem.DataTextField = "ItemName";
                        ddlItem.DataValueField = "ItemId";
                        ddlItem.DataBind();
                    }
                    if (DS.Tables[3].Rows.Count > 0)
                    {
                        ddlsubcategory.DataSource = DS.Tables[3];
                        ddlsubcategory.DataTextField = "SubCategory";
                        ddlsubcategory.DataValueField = "SubCategoryId";
                        ddlsubcategory.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void CheckEmployeeName(Label LableEmpName)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                LableEmpName.Text = Session["UserName"].ToString();
            }
            else
            {
                Response.Redirect("~/Masters/Default.aspx");
            }
        }

        private void BindReportGrid(string StrCondition)
        {
            try
            {
                DataSet DsGrd = new DataSet();
                DataTable dt = new DataTable();
                string COND = string.Empty;
                if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
                {
                    COND = COND + " AND DM.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
                }
                DsGrd = Obj_damage.GetDamageDetails(StrCondition,COND, out StrError);
                if (DsGrd.Tables.Count > 0 && DsGrd.Tables[0].Rows.Count > 0)
                {
                    ReportGridDtls.DataSource = DsGrd;
                    ReportGridDtls.DataBind();
                }
                else
                {
                    ReportGridDtls.DataSource = null;
                    ReportGridDtls.DataBind();
                }
                DsGrd = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void CheckQty()
        {
            decimal DamageQty = 0, InwardQty=0;
            for (int i = 0; i < GrdInwardWiseDetails.Rows.Count; i++)
            {
                if (((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text == " " || string.IsNullOrEmpty(((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text))
                {
                    ((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text = "0";
                    DamageQty = Convert.ToDecimal(((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text);
                }

                InwardQty = Convert.ToDecimal(GrdInwardWiseDetails.Rows[i].Cells[5].Text.ToString());
                if (DamageQty > InwardQty)
                {
                    chkflag = true;
                    break;
                }
                else
                {
                    chkflag = false;
                }
            }
        }

        private void CheckQtyItemWise()
        {
            decimal DamageQty = 0, StockInHand = 0;
            for (int i = 0; i < GRDItemWise.Rows.Count; i++)
            {
                if (((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text == " " || string.IsNullOrEmpty(((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text))
                {
                    ((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text = "0";
                    DamageQty = Convert.ToDecimal(((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text);
                }

                StockInHand = Convert.ToDecimal(GRDItemWise.Rows[i].Cells[6].Text.ToString());
                if (DamageQty > StockInHand)
                {
                    chkflag1 = true;
                    break;
                }
                else
                {
                    chkflag1 = false;
                }
            }
        }

        private void colorchange()
        {
            if (rdoDamagedThrough.SelectedValue == "0")
            {
                for (int i = 0; i < GrdInwardWiseDetails.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text) > 0)
                    {
                        for (int y = 0; y < GrdInwardWiseDetails.Rows[i].Cells.Count; y++)
                        {
                            GrdInwardWiseDetails.Rows[i].Cells[y].ForeColor = System.Drawing.Color.Gray;
                            ((TextBox)GrdInwardWiseDetails.Rows[i].Cells[9].FindControl("GrdtxtDamageQty")).ForeColor = System.Drawing.Color.Gray;
                            ((TextBox)GrdInwardWiseDetails.Rows[i].Cells[10].FindControl("TxtReason")).ForeColor = System.Drawing.Color.Gray;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < GRDItemWise.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text) > 0)
                    {
                        for (int y = 0; y < GRDItemWise.Rows[i].Cells.Count; y++)
                        {
                            GRDItemWise.Rows[i].Cells[y].ForeColor = System.Drawing.Color.Gray;
                            ((TextBox)GRDItemWise.Rows[i].Cells[7].FindControl("GrdtxtDamageQty")).ForeColor = System.Drawing.Color.Gray;
                            ((TextBox)GRDItemWise.Rows[i].Cells[10].FindControl("TxtReason")).ForeColor = System.Drawing.Color.Gray;
                        }
                    }
                }
            }
        }

        private void BlockTextBox()
        {
            if (rdoDamagedThrough.SelectedValue == "0")
            {
                for (int i = 0; i < GrdInwardWiseDetails.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(GrdInwardWiseDetails.Rows[i].Cells[5].Text) <= 0)
                    {
                        ((TextBox)GrdInwardWiseDetails.Rows[i].Cells[9].FindControl("GrdtxtDamageQty")).ReadOnly = true;
                        ((TextBox)GrdInwardWiseDetails.Rows[i].Cells[13].FindControl("TxtReason")).ReadOnly = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < GRDItemWise.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(GRDItemWise.Rows[i].Cells[6].Text) <= 0)
                    {
                        ((TextBox)GRDItemWise.Rows[i].Cells[7].FindControl("GrdtxtDamageQty")).ReadOnly = true;
                        ((TextBox)GRDItemWise.Rows[i].Cells[10].FindControl("TxtReason")).ReadOnly = true;
                    }
                }
            }
        }

        private bool checkInwrd()//---Inward > Returnqty+RemainInwardQty+DamageQty----
        {
           bool flagGreaterInward = false;
            decimal sum=0;
            if (rdoDamagedThrough.SelectedValue == "0")
            {
                for (int i = 0; i < GrdInwardWiseDetails.Rows.Count; i++)
                {
                    sum=Convert.ToDecimal(GrdInwardWiseDetails.Rows[i].Cells[8].Text)+
                        Convert.ToDecimal(GrdInwardWiseDetails.Rows[i].Cells[9].Text) +
                        Convert.ToDecimal(((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text);
                    if (Convert.ToDecimal(GrdInwardWiseDetails.Rows[i].Cells[5].Text) >= sum)
                    {
                        flagGreaterInward = true;
                    }
                }
            }
            return flagGreaterInward;
        }

        private bool CheckInwardForUpdate()//---Inward > Returnqty+RemainInwardQty+DamageQty----
        {
            bool flagGreaterInward = false;
            decimal sum = 0;
            if (rdoDamagedThrough.SelectedValue == "0")
            {
                for (int i = 0; i < GrdInwardWiseDetails.Rows.Count; i++)
                {
                    sum = Convert.ToDecimal(GrdInwardWiseDetails.Rows[i].Cells[8].Text) +
                         Convert.ToDecimal(((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text);
                    if (Convert.ToDecimal(GrdInwardWiseDetails.Rows[i].Cells[5].Text) >= sum)
                    {
                        flagGreaterInward = true;
                    }
                }
            }
            return flagGreaterInward;
        }

        public void BindItemToGrid(DataSet ds, string Rate, int UnitId, string Unit, string ItemDesc,decimal Qty)  //For Binding To Grid One By One
        {           
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                int dsItemID = Convert.ToInt32(ds.Tables[0].Rows[j]["ItemId"]);
                decimal dsRate = Convert.ToDecimal(ds.Tables[0].Rows[j]["InwardRate"]);
               
                if (ViewState["DamagedDtls"] != null)
                {
                    bool DupFlag = false;
                    int k = 0;
                    DataTable DTTABLE = (DataTable)ViewState["DamagedDtls"];
                    DataRow DTROW = null;
                    if (DTTABLE.Rows.Count > 0)
                    {
                        if (DTTABLE.Rows.Count == 1 && (DTTABLE.Rows[0]["ItemId"].ToString()) == "0")
                        {
                            DTTABLE.Rows.RemoveAt(0);
                            //DupFlag = false;
                        }
                        if (ViewState["GridIndex"] != null)
                        {
                            for (int i = 0; i < DTTABLE.Rows.Count; i++)
                            {
                                int dtItemID = Convert.ToInt32(DTTABLE.Rows[i]["ItemId"]);
                                decimal dtRate = Convert.ToDecimal(DTTABLE.Rows[i]["InwardRate"]);

                                if (Convert.ToInt32(dsItemID) == Convert.ToInt32(dtItemID))
                                {
                                    if (Convert.ToDecimal(dsRate) == Convert.ToDecimal(dtRate))
                                    {
                                        DupFlag = true;
                                        k = i;
                                    }
                                }
                            }
                            if (DupFlag == true)
                            {
                                // DTROW = DTTABLE.NewRow();
                                DTTABLE.Rows[k]["ItemId"] = Convert.ToInt32(ds.Tables[0].Rows[j]["ItemId"]);
                                DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemCode"]);
                                DTTABLE.Rows[k]["ItemName"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemName"]);
                                DTTABLE.Rows[k]["ItemDesc"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemDesc"]);
                                DTTABLE.Rows[k]["Unit"] = Convert.ToString(ds.Tables[0].Rows[j]["Unit"]);
                                DTTABLE.Rows[k]["UnitId"] = Convert.ToInt32(ds.Tables[0].Rows[j]["UnitId"]);
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    DTTABLE.Rows[k]["StockInHand"] = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["Closing"].ToString())?0: Convert.ToDecimal(ds.Tables[1].Rows[0]["Closing"]);
                                }
                                else
                                {
                                    DTTABLE.Rows[k]["StockInHand"]=0;
                                }
                                ////DTTABLE.Rows[k]["StockInHand"] = string.IsNullOrEmpty(ds.Tables[1].Rows[j]["Closing"].ToString()) ? 0 : Convert.ToDecimal(ds.Tables[1].Rows[j]["Closing"]);
                                DTTABLE.Rows[k]["DamageQty"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["DamageQty"]);
                                DTTABLE.Rows[k]["InwardRate"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["InwardRate"]);
                                DTTABLE.Rows[k]["Amount"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["Amount"]);
                                DTTABLE.Rows[k]["Reason"] = Convert.ToString(ds.Tables[0].Rows[j]["Reason"]);
                               
                                //DTTABLE.Rows.Add(DTROW);
                                ViewState["DamagedDtls"] = DTTABLE;
                                GRDItemWise.DataSource = DTTABLE;
                                GRDItemWise.DataBind();
                            }
                            else
                            {
                                DTROW = DTTABLE.NewRow();

                                DTROW["ItemId"] = Convert.ToInt32(ds.Tables[0].Rows[j]["ItemId"]);
                                DTROW["ItemCode"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemCode"]);
                                DTROW["ItemName"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemName"]);
                                DTROW["ItemDesc"] = Convert.ToString(ItemDesc);
                                DTROW["Unit"] = Convert.ToString(Unit);
                                DTROW["DamageQty"] = Convert.ToDecimal(Qty);
                                DTROW["UnitId"] = Convert.ToInt32(UnitId);
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    DTROW["StockInHand"] = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["Closing"].ToString()) ? 0 : Convert.ToDecimal(ds.Tables[1].Rows[0]["Closing"]);
                                }
                                else
                                {
                                    DTROW["StockInHand"] = 0;
                                }
                                
                                ////DTROW["StockInHand"] = string.IsNullOrEmpty(ds.Tables[1].Rows[j]["Closing"].ToString()) ? 0 : Convert.ToDecimal(ds.Tables[1].Rows[j]["Closing"]); ;
                                DTROW["DamageQty"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["DamageQty"]);
                                //DTROW["InwardRate"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["InwardRate"]);
                                DTROW["InwardRate"] = Convert.ToDecimal(Rate);
                                DTROW["Amount"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["Amount"]);
                                DTROW["Reason"] = Convert.ToString(ds.Tables[0].Rows[j]["Reason"]);

                                DTTABLE.Rows.Add(DTROW);
                                ViewState["DamagedDtls"] = DTTABLE;
                                GRDItemWise.DataSource = DTTABLE;
                                GRDItemWise.DataBind();
                                ViewState["GridIndex"] = k++;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < DTTABLE.Rows.Count; i++)
                            {
                                int dtItemID = Convert.ToInt32(DTTABLE.Rows[i]["ItemId"]);
                                decimal dtRate = Convert.ToDecimal(DTTABLE.Rows[i]["InwardRate"]);

                                if (Convert.ToInt32(dsItemID) == Convert.ToInt32(dtItemID))
                                {
                                    if (Convert.ToDecimal(dsRate) == Convert.ToDecimal(dtRate))
                                    {
                                        DupFlag = true;
                                        k = i;
                                    }
                                }
                            }
                            if (DupFlag == true)
                            {
                                // DTROW = DTTABLE.NewRow();
                                int rowindex = Convert.ToInt32(ViewState["GridIndex"]);
                                DTTABLE.Rows[k]["ItemId"] = Convert.ToInt32(ds.Tables[0].Rows[j]["ItemId"]);
                                DTTABLE.Rows[k]["ItemCode"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemCode"]);
                                DTTABLE.Rows[k]["ItemName"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemName"]);
                                DTTABLE.Rows[k]["ItemDesc"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemDesc"]);
                                DTTABLE.Rows[k]["Unit"] = Convert.ToString(ds.Tables[0].Rows[j]["Unit"]);
                                DTTABLE.Rows[k]["UnitId"] = Convert.ToInt32(ds.Tables[0].Rows[j]["UnitId"]);
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    DTTABLE.Rows[k]["StockInHand"] = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["Closing"].ToString()) ? 0 : Convert.ToDecimal(ds.Tables[1].Rows[0]["Closing"]);
                                }
                                else
                                {
                                    DTTABLE.Rows[k]["StockInHand"] = 0;
                                }
                                
                                ////DTTABLE.Rows[k]["StockInHand"] = string.IsNullOrEmpty(ds.Tables[1].Rows[j]["Closing"].ToString()) ? 0 : Convert.ToDecimal(ds.Tables[1].Rows[j]["Closing"]); ;
                                DTTABLE.Rows[k]["DamageQty"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["DamageQty"]);
                                DTTABLE.Rows[k]["InwardRate"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["InwardRate"]);
                                DTTABLE.Rows[k]["Amount"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["Amount"]);
                                DTTABLE.Rows[k]["Reason"] = Convert.ToString(ds.Tables[0].Rows[j]["Reason"]);
                                //DTTABLE.Rows.Add(DTROW);
                                ViewState["DamagedDtls"] = DTTABLE;
                                GRDItemWise.DataSource = DTTABLE;
                                GRDItemWise.DataBind();
                            }
                            else
                            {
                                DTROW = DTTABLE.NewRow();
                                DTROW["ItemId"] = Convert.ToInt32(ds.Tables[0].Rows[j]["ItemId"]);
                                DTROW["ItemCode"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemCode"]);
                                DTROW["ItemName"] = Convert.ToString(ds.Tables[0].Rows[j]["ItemName"]);
                                DTROW["ItemDesc"] = Convert.ToString(ItemDesc);
                                DTROW["DamageQty"] = Convert.ToDecimal(Qty);
                                DTROW["Unit"] = Convert.ToString(Unit);
                                DTROW["UnitId"] = Convert.ToDecimal(UnitId);
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    DTROW["StockInHand"] = string.IsNullOrEmpty(ds.Tables[1].Rows[0]["Closing"].ToString()) ? 0 : Convert.ToDecimal(ds.Tables[1].Rows[0]["Closing"]);
                                }
                                else
                                {
                                    DTROW["StockInHand"] = 0;
                                }
                                
                                ////DTROW["StockInHand"] = string.IsNullOrEmpty(ds.Tables[1].Rows[j]["Closing"].ToString()) ? 0 : Convert.ToDecimal(ds.Tables[1].Rows[j]["Closing"]); ;
                               // DTROW["DamageQty"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["DamageQty"]);
                                //DTROW["InwardRate"] = Convert.ToInt32(ds.Tables[0].Rows[j]["InwardRate"]);
                                DTROW["InwardRate"] = Convert.ToDecimal(Rate);
                                DTROW["Amount"] = Convert.ToDecimal(ds.Tables[0].Rows[j]["Amount"]);
                                DTROW["Reason"] = Convert.ToString(ds.Tables[0].Rows[j]["Reason"]);
                                DTTABLE.Rows.Add(DTROW);
                                ViewState["DamagedDtls"] = DTTABLE;
                                GRDItemWise.DataSource = DTTABLE;
                                GRDItemWise.DataBind();
                                ViewState["GridIndex"] = k++;
                            }
                        }
                    }
                }
            }
        }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Fillcombo();
            MakeEmptyForm();
            // Check Session (if Session is null then go to Login Page)
            CheckEmployeeName(lblPreparedBy);
            CheckUserRight();
        }
        else
        {
           
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeControlEmpty();
        MakeEmptyForm();
    }

    protected void ddlInwardNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {             
               int InwardId = Convert.ToInt32(ddlInwardNo.SelectedValue);
               string COND = string.Empty;
               if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
               {
                   COND = COND + " AND RCF.CafeteriaId=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
               }
                DS = Obj_damage.GetInwardDtls(InwardId,COND, out StrError);
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        lblInwardNo.Text = DS.Tables[0].Rows[0]["InwardNo"].ToString();
                        lblInwardDate.Text = DS.Tables[0].Rows[0]["InwardDate"].ToString();
                        lblPONO.Text = DS.Tables[0].Rows[0]["PONo"].ToString();
                        lblSuppName.Text = DS.Tables[0].Rows[0]["SuplierName"].ToString();
                    }
                    else
                    {
                        lblInwardNo.Text = "";
                        lblInwardDate.Text = "";
                        lblPONO.Text = "";
                        lblSuppName.Text = "";
                    }
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        GrdInwardWiseDetails.DataSource = DS.Tables[1];
                        GrdInwardWiseDetails.DataBind();
                        BlockTextBox();
                    }
                    else
                    {
                        GrdInwardWiseDetails.DataSource = null;
                    }
                    FieldsetInward.Visible = true;
                    FieldsetItem.Visible = false;
                }
          

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InsertRowDtls = 0;
        decimal DamageQty = 0;
        try
        {
            if (rdoDamagedThrough.SelectedValue == "0")
            {
                if (checkInwrd())
                {
                    #region[InwardWise]
                    CheckQty();
                    if (chkflag == false)
                    {
                        //Entity_damage.DamageNo = TxtDamageNo.Text;
                        Entity_damage.DamageDate = Convert.ToDateTime(TxtDamageDate.Text);
                        Entity_damage.PreparedBy = Convert.ToInt32(Session["UserID"]);

                        Entity_damage.InwardId = Convert.ToInt32(ddlInwardNo.SelectedValue);
                        Entity_damage.DamagedThrough = 0;

                        if (rdoDebitNote.SelectedValue == "1")
                        {
                            Entity_damage.DebitNote = 1;
                        }
                        else
                        {
                            Entity_damage.DebitNote = 0;
                        }

                        Entity_damage.UserID = Convert.ToInt32(Session["UserId"]);
                        Entity_damage.LoginDate = DateTime.Now;

                        InsertRow = Obj_damage.InsertRecord(ref Entity_damage,Convert.ToInt32(Session["CafeteriaId"].ToString()), out StrError);
                        if (InsertRow > 0)
                        {
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtInsert = new DataTable();
                                for (int i = 0; i < GrdInwardWiseDetails.Rows.Count; i++)
                                {
                                    if (((((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text).Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text)) > 0)
                                    {
                                        Entity_damage.DamageId = InsertRow;
                                        DamageId = Entity_damage.DamageId;
                                        Entity_damage.ItemId = Convert.ToInt32(GrdInwardWiseDetails.Rows[i].Cells[1].Text.ToString());
                                        Entity_damage.InwardQty = Convert.ToDecimal(GrdInwardWiseDetails.Rows[i].Cells[5].Text.ToString());
                                        Entity_damage.InwardRate = Convert.ToDecimal(GrdInwardWiseDetails.Rows[i].Cells[12].Text.ToString());
                                        if (((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text == " " || string.IsNullOrEmpty(((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text))
                                        {
                                            Entity_damage.DamageQty = 0;
                                        }
                                        else
                                        {
                                            Entity_damage.DamageQty = Convert.ToDecimal(((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text);
                                        }
                                        Entity_damage.Reason = (((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("TxtReason")).Text);
                                        Entity_damage.ConversionUnitId = Convert.ToInt32(GrdInwardWiseDetails.Rows[i].Cells[6].Text.ToString());
                                        Entity_damage.ItemDesc = GrdInwardWiseDetails.Rows[i].Cells[4].Text.ToString();
                                        //StockDetails
                                        Entity_damage.StockUnitId = Convert.ToInt32(GrdInwardWiseDetails.Rows[i].Cells[6].Text.ToString());
                                        Entity_damage.StockDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                        Entity_damage.StockLocationID = Convert.ToInt32(Session["CafeteriaId"]);

                                        InsertRowDtls = Obj_damage.InsertDetailsRecord(ref Entity_damage, out StrError);
                                    }
                                }
                            }
                            if (InsertRow > 0)
                            {
                                obj_Comm.ShowPopUpMsg("Record Saved Successfully", this.Page);
                                #region[DebitNote Print]
                                //-----DebitNote print-------
                                if (rdoDebitNote.SelectedValue == "1")
                                {
                                    //string s = "~/PrintReport/DebitNote.aspx?ID=" + DamageId;
                                    ////Response.Write("<script> window.open( ‘" + s + "’,'_blank' ); </script>");
                                    //Response.Write("<script>");
                                    //Response.Write("window.open('"+s+"','_blank')");
                                    //Response.Write("</script>");
                                    ////Response.Redirect("~/PrintReport/DebitNote.aspx?ID=" + DamageId);
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DebitNote.aspx?ID="+DamageId+"');", true);
                                }
                                #endregion


                                MakeControlEmpty();
                                MakeEmptyForm();
                                Entity_damage = null;
                                Obj_damage = null;
                            }

                        }
                    }
                    else
                    {
                        obj_Comm.ShowPopUpMsg("Damage Quantity Should be less than Inward Qty", this.Page);
                    }
                    #endregion
                }
                else
                {
                    obj_Comm.ShowPopUpMsg("InwardQty must be > ReturnQty+DamageQty+PreviousDamageQty", this.Page);
                }
            }
            else
            {
                #region[ItemWise]
                CheckQtyItemWise();
                if (chkflag1 == false)
                {
                    Entity_damage.DamageDate = Convert.ToDateTime(TxtDamageDate.Text);
                    Entity_damage.PreparedBy = Convert.ToInt32(Session["UserID"]);

                    Entity_damage.InwardId = 0;
                    Entity_damage.DamagedThrough = 1;

                    Entity_damage.UserID = Convert.ToInt32(Session["UserId"]);
                    Entity_damage.LoginDate = DateTime.Now;

                    InsertRow = Obj_damage.InsertRecord(ref Entity_damage, Convert.ToInt32(Session["CafeteriaId"].ToString()), out StrError);
                    if (InsertRow > 0)
                    {
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtInsert = new DataTable();
                            for (int i = 0; i < GRDItemWise.Rows.Count; i++)
                            {
                                if (((((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text).Equals("") ? 0 : Convert.ToDecimal(((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text)) > 0)
                                {
                                    Entity_damage.DamageId = InsertRow;
                                    Entity_damage.ItemId = Convert.ToInt32(GRDItemWise.Rows[i].Cells[0].Text.ToString());
                                    Entity_damage.InwardQty = Convert.ToDecimal(GRDItemWise.Rows[i].Cells[6].Text.ToString());
                                    Entity_damage.InwardRate = Convert.ToDecimal(GRDItemWise.Rows[i].Cells[8].Text.ToString());
                                    if (((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text == " " || string.IsNullOrEmpty(((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text))
                                    {
                                        Entity_damage.DamageQty = 0;
                                    }
                                    else
                                    {
                                        Entity_damage.DamageQty = Convert.ToDecimal(((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text);
                                    }
                                    Entity_damage.Reason = (((TextBox)GRDItemWise.Rows[i].FindControl("TxtReason")).Text);
                                    Entity_damage.ConversionUnitId = Convert.ToInt32(GRDItemWise.Rows[i].Cells[4].Text.ToString());
                                    Entity_damage.ItemDesc = GRDItemWise.Rows[i].Cells[3].Text.ToString();
                                    //StockDetails
                                    Entity_damage.StockUnitId = Convert.ToInt32(GRDItemWise.Rows[i].Cells[4].Text.ToString());
                                    Entity_damage.StockDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                    Entity_damage.StockLocationID = Convert.ToInt32(Session["CafeteriaId"]);

                                    InsertRowDtls = Obj_damage.InsertDetailsRecord(ref Entity_damage, out StrError);
                                }
                            }
                        }
                        if (InsertRow > 0)
                        {
                            obj_Comm.ShowPopUpMsg("Record Saved Successfully", this.Page);
                            MakeControlEmpty();
                            MakeEmptyForm();
                            Entity_damage = null;
                            Obj_damage = null;
                        }
                    }
                }
                else
                {
                    obj_Comm.ShowPopUpMsg("Damage Quantity Should be less than Inward Qty", this.Page);
                }
                #endregion
            }
        }
        catch (ThreadAbortException ex1) { }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, UpdateRowDtls = 0;
        try
        {
            if (rdoDamagedThrough.SelectedValue == "0")
            {
                if (CheckInwardForUpdate())
                {
                    #region[InwardWise]
                    CheckQty();
                    if (chkflag == false)
                    {
                        if (ViewState["EditId"] != null)
                        {
                            Entity_damage.DamageId = Convert.ToInt32(ViewState["EditId"]);
                            DamageId = Entity_damage.DamageId;
                        }
                        Entity_damage.DamageDate = Convert.ToDateTime(TxtDamageDate.Text);
                        Entity_damage.PreparedBy = Convert.ToInt32(Session["UserID"]);

                        if (rdoDamagedThrough.SelectedValue == "0")
                        {
                            //Entity_damage.InwardId = Convert.ToInt32(ddlInwardNo.SelectedValue);
                            Entity_damage.DamagedThrough = 0;
                        }
                        else
                        {
                            // Entity_damage.InwardId = 0;
                            Entity_damage.DamagedThrough = 1;
                        }

                        if (rdoDebitNote.SelectedValue == "1")
                        {
                            Entity_damage.DebitNote = 1;
                        }
                        else
                        {
                            Entity_damage.DebitNote = 0;
                        }

                        Entity_damage.UserID = Convert.ToInt32(Session["UserId"]);
                        Entity_damage.LoginDate = DateTime.Now;

                        UpdateRow = Obj_damage.UpdateRecord(ref Entity_damage, out StrError);
                        if (UpdateRow > 0)
                        {
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtInsert = new DataTable();
                                dtInsert = (DataTable)ViewState["CurrentTable"];
                                for (int i = 0; i < GrdInwardWiseDetails.Rows.Count; i++)
                                {
                                    if (((((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text).Equals("") ? 0 : Convert.ToDecimal(((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text)) > 0)
                                    {
                                        Entity_damage.ItemId = Convert.ToInt32(GrdInwardWiseDetails.Rows[i].Cells[1].Text.ToString());
                                        Entity_damage.InwardQty = Convert.ToDecimal(GrdInwardWiseDetails.Rows[i].Cells[5].Text.ToString());
                                        Entity_damage.InwardRate = Convert.ToDecimal(GrdInwardWiseDetails.Rows[i].Cells[12].Text.ToString());
                                        if (((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text == " " || string.IsNullOrEmpty(((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text))
                                        {
                                            Entity_damage.DamageQty = 0;
                                        }
                                        else
                                        {
                                            Entity_damage.DamageQty = Convert.ToDecimal(((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("GrdtxtDamageQty")).Text);
                                        }
                                        Entity_damage.Reason = (((TextBox)GrdInwardWiseDetails.Rows[i].FindControl("TxtReason")).Text);
                                        Entity_damage.ConversionUnitId = Convert.ToInt32(GrdInwardWiseDetails.Rows[i].Cells[6].Text.ToString());

                                        //StockDetails
                                        Entity_damage.StockUnitId = Convert.ToInt32(GrdInwardWiseDetails.Rows[i].Cells[6].Text.ToString());
                                        Entity_damage.StockDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                        Entity_damage.StockLocationID = Convert.ToInt32(Session["CafeteriaId"]);

                                        UpdateRowDtls = Obj_damage.InsertDetailsRecord(ref Entity_damage, out StrError);
                                    }
                                }
                            }
                            if (UpdateRow > 0)
                            {
                                obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                                #region[DebitNote Print]
                                //-----DebitNote print-------
                                if (rdoDebitNote.SelectedValue == "1")
                                {
                                    //string s = "~/PrintReport/DebitNote.aspx?ID=" + DamageId;
                                    ////Response.Write("<script> window.open( ‘" + s + "’,'_blank' ); </script>");
                                    //Response.Write("<script>");
                                    //Response.Write("window.open('" + s + "','_blank')");
                                    //Response.Write("</script>");
                                    ////Response.Redirect("~/PrintReport/DebitNote.aspx?ID=" + DamageId);
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('DebitNote.aspx?ID=" + DamageId + "');", true);
                                }
                                #endregion
                                MakeControlEmpty();
                                MakeEmptyForm();
                                Entity_damage = null;
                                Obj_damage = null;
                            }
                        }
                    }
                    else
                    {
                        obj_Comm.ShowPopUpMsg("Damage Quantity Should be less than Inward Qty", this.Page);
                    }
                    #endregion
                }
                else
                {
                    obj_Comm.ShowPopUpMsg("InwardQty must be > ReturnQty+DamageQty+PreviousDamageQty", this.Page);
                }
            }
            else
            {
                #region[ItemWise]
                CheckQtyItemWise();
                if (chkflag1 == false)
                {
                    if (ViewState["EditId"] != null)
                    {
                        Entity_damage.DamageId = Convert.ToInt32(ViewState["EditId"]);
                    }
                    Entity_damage.DamageDate = Convert.ToDateTime(TxtDamageDate.Text);
                    Entity_damage.PreparedBy = Convert.ToInt32(Session["UserID"]);

                    if (rdoDamagedThrough.SelectedValue == "0")
                    {
                        //Entity_damage.InwardId = Convert.ToInt32(ddlInwardNo.SelectedValue);
                        Entity_damage.DamagedThrough = 0;
                    }
                    else
                    {
                        // Entity_damage.InwardId = 0;
                        Entity_damage.DamagedThrough = 1;
                    }

                    Entity_damage.UserID = Convert.ToInt32(Session["UserId"]);
                    Entity_damage.LoginDate = DateTime.Now;

                    UpdateRow = Obj_damage.UpdateRecord(ref Entity_damage, out StrError);
                    if (UpdateRow > 0)
                    {
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtInsert = new DataTable();
                            dtInsert = (DataTable)ViewState["CurrentTable"];
                            for (int i = 0; i < GRDItemWise.Rows.Count; i++)
                            {
                                if (((((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text).Equals("") ? 0 : Convert.ToDecimal(((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text)) > 0)
                                {
                                    Entity_damage.ItemId = Convert.ToInt32(GRDItemWise.Rows[i].Cells[0].Text.ToString());
                                    Entity_damage.InwardQty = Convert.ToDecimal(GRDItemWise.Rows[i].Cells[6].Text.ToString());
                                    Entity_damage.InwardRate = Convert.ToDecimal(GRDItemWise.Rows[i].Cells[8].Text.ToString());
                                    if (((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text == " " || string.IsNullOrEmpty(((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text))
                                    {
                                        Entity_damage.DamageQty = 0;
                                    }
                                    else
                                    {
                                        Entity_damage.DamageQty = Convert.ToDecimal(((TextBox)GRDItemWise.Rows[i].FindControl("GrdtxtDamageQty")).Text);
                                    }
                                    Entity_damage.Reason = (((TextBox)GRDItemWise.Rows[i].FindControl("TxtReason")).Text);
                                    Entity_damage.ConversionUnitId = Convert.ToInt32(GRDItemWise.Rows[i].Cells[4].Text.ToString());

                                    //StockDetails
                                    Entity_damage.StockUnitId = Convert.ToInt32(GRDItemWise.Rows[i].Cells[4].Text.ToString());
                                    Entity_damage.StockDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                    Entity_damage.StockLocationID = Convert.ToInt32(Session["CafeteriaId"]);

                                    UpdateRowDtls = Obj_damage.InsertDetailsRecord(ref Entity_damage, out StrError);
                                }
                            }
                        }
                        if (UpdateRow > 0)
                        {
                            obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                            MakeControlEmpty();
                            MakeEmptyForm();
                            Entity_damage = null;
                            Obj_damage = null;
                        }
                    }

                }
                else
                {
                    obj_Comm.ShowPopUpMsg("Damage Quantity Should be less than Inward Qty", this.Page);
                }
                #endregion
            }
            
        }
        catch (ThreadAbortException ex1) { }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMDamage obj_Damage = new DMDamage();
        String[] SearchList = obj_Damage.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        BindReportGrid(StrCondition);
    }

    protected void ReportGridDtls_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        if (Convert.ToInt32(e.CommandArgument) != 0)
                        {
                            ViewState["EditId"] = Convert.ToInt32(e.CommandArgument);

                            DSS = Obj_damage.GetRecordForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (DSS.Tables.Count > 0 && DSS.Tables[0].Rows.Count > 0)
                            {
                                TxtDamageNo.Text = DSS.Tables[0].Rows[0]["DamageNo"].ToString();
                                TxtDamageDate.Text = DSS.Tables[0].Rows[0]["DamageDate"].ToString();
                                lblPreparedBy.Text = Session["UserName"].ToString();

                                rdoDamagedThrough.SelectedValue = DSS.Tables[0].Rows[0]["DamagedThrough"].ToString();
                                rdoDebitNote.SelectedValue = DSS.Tables[0].Rows[0]["DebitNote"].ToString();
                                
                                if (rdoDamagedThrough.SelectedValue == "0")
                                {
                                    FS_InwardDtls.Visible = true;
                                    FS_ItemDtls.Visible = false;
                                    FieldsetInward.Visible = true;
                                    FieldsetItem.Visible = false;

                                    t1.Visible = false;
                                    lblInwardNo.Text = DSS.Tables[0].Rows[0]["InwardNo"].ToString();
                                    lblInwardDate.Text = DSS.Tables[0].Rows[0]["InwardDate"].ToString();
                                    lblPONO.Text = DSS.Tables[0].Rows[0]["PONo"].ToString();
                                    lblSuppName.Text = DSS.Tables[0].Rows[0]["SuplierName"].ToString();
                                    //ddlInwardNo.SelectedValue = DSS.Tables[0].Rows[0]["InwardId"].ToString();
                                }
                                else
                                {
                                    FS_InwardDtls.Visible = false;
                                    FS_ItemDtls.Visible = true;
                                    FieldsetInward.Visible = false;
                                    FieldsetItem.Visible = true;
                                }
                            }
                            else
                            {
                                MakeEmptyForm();
                            }

                            if (rdoDamagedThrough.SelectedValue == "0")
                            {
                                if (DSS.Tables[1].Rows.Count > 0)
                                {
                                    GrdInwardWiseDetails.DataSource = DSS.Tables[1];
                                    GrdInwardWiseDetails.DataBind();
                                    ViewState["CurrentTable"] = DSS.Tables[1];
                                    ViewState["DamagedDtls"] = DSS.Tables[1];
                                    colorchange();
                                    BlockTextBox();
                                }
                            }
                            else
                            {
                                GRDItemWise.DataSource = DSS.Tables[1];
                                GRDItemWise.DataBind();
                                ViewState["CurrentTable"] = DSS.Tables[1];
                                ViewState["DamagedDtls"] = DSS.Tables[1];
                                colorchange();
                                BlockTextBox();
                            }
                            DSS = null;
                            Obj_damage = null;
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                        }
                        break;
                    }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void ReportGridDtls_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int DeleteId = 0;
        try
        {
            DeleteId = Convert.ToInt32(((ImageButton)ReportGridDtls.Rows[e.RowIndex].Cells[0].FindControl("ImgBtnDelete")).CommandArgument.ToString());
            if (DeleteId != 0)
            {
                Entity_damage.DamageId = DeleteId;
                Entity_damage.UserID = Convert.ToInt32(Session["UserID"]);
                Entity_damage.LoginDate = DateTime.Now;

                int iDelete = Obj_damage.DeleteRecord(ref Entity_damage, out StrError);
                if (iDelete != 0)
                {
                    obj_Comm.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_damage = null;
            Obj_damage = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    protected void rdoInwardThrough_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoDamagedThrough.SelectedValue == "0")
        {
            FS_InwardDtls.Visible = true;
            FS_ItemDtls.Visible = false;
            FieldsetInward.Visible = true;
            FieldsetItem.Visible = false;
            SetInitialRowInwardWise();
        }
        else
        {
            FS_InwardDtls.Visible = false;
            FS_ItemDtls.Visible = true;
            FieldsetInward.Visible = false;
            FieldsetItem.Visible = true; 
            SetInitialRow();
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataSet dsItem_Cat = new DataSet();
        //DataSet dsItem_All = new DataSet();
        //int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);
        //dsItem_Cat = Obj_damage.Fill_Items(categoryId, out StrError);
        //dsItem_All = Obj_damage.Fill_AllItem(out StrError);

        //if (dsItem_Cat.Tables[0].Rows.Count > 1)
        //{
        //    ddlItem.DataSource = dsItem_Cat.Tables[0];
        //    ddlItem.DataTextField = "Item";
        //    ddlItem.DataValueField = "ItemId";
        //    ddlItem.DataBind();
        //}
        //else
        //{
        //    ddlItem.DataSource = dsItem_All.Tables[0];
        //    ddlItem.DataTextField = "Item";
        //    ddlItem.DataValueField = "ItemId";
        //    ddlItem.DataBind();
        //}

        ////string COND = string.Empty;
        ////DMFILTER DMFILTER = new DMFILTER();
        ////if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
        ////{
        ////    COND = COND + " AND CategoryId=" + Convert.ToInt32(ddlCategory.SelectedValue);
        ////}
        ////DS = DMFILTER.FillReportComboCategoryWise(1, 0, COND, out StrError);
        ////{
        ////    if (DS.Tables.Count > 0)
        ////    {
        ////        if (DS.Tables[0].Rows.Count > 0)
        ////        {
        ////            ddlItem.DataSource = DS.Tables[0];
        ////            ddlItem.DataTextField = "ItemName";
        ////            ddlItem.DataValueField = "ItemId";
        ////            ddlItem.DataBind();
        ////        }
        ////        if (DS.Tables[1].Rows.Count > 0)
        ////        {
        ////            ddlsubcategory.DataSource = DS.Tables[1];
        ////            ddlsubcategory.DataTextField = "Name";
        ////            ddlsubcategory.DataValueField = "Id";
        ////            ddlsubcategory.DataBind();
        ////        }
        ////    }
        ////    else
        ////    {
        ////        DS = null;
        ////    }
        ////}
        DataSet dsItem_Cat = new DataSet();
        DataSet dsItem_All = new DataSet();
        int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);
        dsItem_Cat = Obj_PurchaseOrder.Fill_Items(categoryId, out StrError);
        dsItem_All = Obj_PurchaseOrder.Fill_AllItem(out StrError);

        if (dsItem_Cat.Tables[0].Rows.Count > 1)
        {
            ddlItem.DataSource = dsItem_Cat.Tables[0];
            ddlItem.DataTextField = "ItemName";
            ddlItem.DataValueField = "ItemId";
            ddlItem.DataBind();

            ddlsubcategory.DataSource = dsItem_Cat.Tables[2];
            ddlsubcategory.DataTextField = "SubCategory";
            ddlsubcategory.DataValueField = "SubCategoryId";
            ddlsubcategory.DataBind();
        }
        else
        {
            ddlItem.DataSource = dsItem_All.Tables[0];
            ddlItem.DataTextField = "ItemName";
            ddlItem.DataValueField = "ItemId";
            ddlItem.DataBind();

            ddlsubcategory.DataSource = dsItem_All.Tables[1];
            ddlsubcategory.DataTextField = "SubCategory";
            ddlsubcategory.DataValueField = "SubCategoryId";
            ddlsubcategory.DataBind();
        }
        TR_RateList.Visible = true;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);


    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        DataSet DsItem = new DataSet();
        if (!string.IsNullOrEmpty(lstSupplierRate.Text.Trim()))
        {
            try
            {
                if (ddlItem.SelectedIndex > 0)
                {
                    string MainString = lstSupplierRate.Text;
                    string Rate = string.Empty;
                    string ItemDesc = string.Empty;
                    string SupId = string.Empty;
                    int UnitId = 0;
                    decimal DamagQty = 0;
                    string Unit = string.Empty;
                    int IDLenght = MainString.IndexOf(')');
                    SupId = MainString.Substring(0, IDLenght);
                    int RateStart = MainString.IndexOf('@');
                    int RateEnd = MainString.IndexOf("Rs/-");
                    Rate = MainString.Substring(RateStart + 2, ((RateEnd) - (RateStart + 2)));

                    UnitId = Convert.ToInt32(ddlUnitConversn.SelectedValue);
                    Unit = ddlUnitConversn.SelectedItem.Text.ToString();
                    ItemDesc = ddldesc.SelectedItem.Text.ToString();
                    DamagQty = Convert.ToDecimal(txtItemOrdQty.Text);
                    //string [] UnitConversion = Unit.Split('-');
                    //Unitpara = UnitConversion[0].ToString();
                    int ItemDescId = Convert.ToInt32(ddldesc.SelectedValue.ToString());
                    LocationId = Convert.ToInt32(Session["CafeteriaId"]);
                    DsItem = Obj_damage.Fill_Items_Details(Convert.ToInt32(ddlItem.SelectedValue), LocationId, ItemDescId, out StrError);
                    if (DsItem.Tables.Count > 0 && DsItem.Tables[0].Rows.Count > 0)
                    {
                        BindItemToGrid(DsItem, Rate, UnitId, Unit, ItemDesc, DamagQty);
                    }
                    else
                    {
                        obj_Comm.ShowPopUpMsg("Record Not Found", this.Page);
                    }
                    DsItem = null;
                }
                else
                {
                    obj_Comm.ShowPopUpMsg("Please Select Item...", this.Page);
                }
                BlockTextBox();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
    }

    protected void GrdtxtDamageQty_TextChanged(object sender, EventArgs e)
    {
       #region[Calculate RemainInwardQuantity]
        TextBox txt=(TextBox)sender;
        Decimal Damage = !string.IsNullOrEmpty(txt.Text) ? Convert.ToDecimal(txt.Text) : 0;
        GridViewRow grd = (GridViewRow)txt.Parent.Parent;
        if ((Convert.ToDecimal(grd.Cells[5].Text) >= Damage))
        {
            grd.Cells[11].Text = (Convert.ToDecimal(grd.Cells[5].Text) - Damage).ToString("#0.00");
            grd.Cells[13].Text = (Convert.ToDecimal(grd.Cells[12].Text)*Damage).ToString("#0.00");
            ((TextBox)grd.FindControl("TxtReason")).Focus();
        }
        else
        {
            obj_Comm.ShowPopUpMsg("You have only  " + (Convert.ToDecimal(grd.Cells[5].Text)) + " Items", this.Page);
            txt.Focus();
        }
       #endregion
    }

    protected void GrdtxtDamageQty_TextChanged1(object sender, EventArgs e)
    {
        #region[Calculate RemainInwardQuantity]
        TextBox txt = (TextBox)sender;
        Decimal Damage = !string.IsNullOrEmpty(txt.Text) ? Convert.ToDecimal(txt.Text) : 0;
        GridViewRow grd = (GridViewRow)txt.Parent.Parent;
        if ((Convert.ToDecimal(grd.Cells[6].Text) >= Damage))
        {
             grd.Cells[9].Text = (Convert.ToDecimal(grd.Cells[8].Text) * Damage).ToString("#0.00");
            ((TextBox)grd.FindControl("TxtReason")).Focus();
        }
        else
        {
            obj_Comm.ShowPopUpMsg("You have only  " + (Convert.ToDecimal(grd.Cells[5].Text)) + " Items", this.Page);
            txt.Focus();
        }
        #endregion
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlItem.SelectedValue != "0")
            {
                DataSet dsItem_All = new DataSet();
                int ItemId = Convert.ToInt32(ddlItem.SelectedValue);
                dsItem_All = Obj_PurchaseOrder.Fill_SupplierRate(ItemId, out StrError);

                if (dsItem_All.Tables[0].Rows.Count > 0)
                {
                    ddldesc.DataSource = dsItem_All.Tables[2];
                    ddldesc.DataTextField = "ItemDesc";
                    ddldesc.DataValueField = "ItemDetailsId";
                    ddldesc.DataBind();

                    ddlUnitConversn.DataSource = dsItem_All.Tables[3];
                    ddlUnitConversn.DataTextField = "Unit";
                    ddlUnitConversn.DataValueField = "UnitId";
                    ddlUnitConversn.DataBind();


                    for (int i = 0; i < ddlUnitConversn.Items.Count; i++)
                    {
                        ddlUnitConversn.Items[i].Attributes.Add("Title", dsItem_All.Tables[3].Rows[i]["Factor"].ToString());
                        // UNITFACTOR.Add(dsItem_All.Tables[3].Rows[i]["Factor"].ToString());
                    }
                    lstSupplierRate.DataSource = dsItem_All.Tables[0];
                    lstSupplierRate.DataTextField = "Rate";
                    lstSupplierRate.DataValueField = "Rate";
                    lstSupplierRate.DataBind();
                    lstSupplierRate.SelectedIndex = 0;
                }
                TR_RateList.Visible = true;

            }
            else
            {
                TR_RateList.Visible = false;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
   
    protected void ReportGridDtls_DataBound(object sender, EventArgs e)
    {
        
       
    }

    protected void ReportGridDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

    protected void ReportGridDtls_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.ReportGridDtls.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            string COND = string.Empty;
            if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            {
                COND = COND + " AND DM.Location=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            }
            DS = Obj_damage.GetDamageDetails(StrCondition,COND, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                ReportGridDtls.DataSource = DS.Tables[0];
                this.ReportGridDtls.DataBind();
                //-----For UserRights-------
                if (FlagDel && FlagEdit)
                {
                    foreach (GridViewRow GRow in ReportGridDtls.Rows)
                    {
                        GRow.FindControl("ImgBtnDelete").Visible = false;
                        GRow.FindControl("ImageGridEdit").Visible = false;
                    }
                }
                else if (FlagDel)
                {
                    foreach (GridViewRow GRow in ReportGridDtls.Rows)
                    {
                        GRow.FindControl("ImgBtnDelete").Visible = false;
                    }
                }
                else
                {
                    foreach (GridViewRow GRow in ReportGridDtls.Rows)
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

    DataTable GetDataTable(GridView dtg)
    {
        try
        {
            int k = 0;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DamagedDtls"];
            while (dt.Rows.Count > 0)
            {
                dt.Rows.RemoveAt(0);
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                if (Convert.ToInt32(GRDItemWise.Rows[k].Cells[0].Text) > 0)
                {
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        //dr["#"] = (((Label)GRDItemWise.Rows[k].FindControl("LblEntryId")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((Label)GRDItemWise.Rows[k].FindControl("LblEntryId")).Text);
                        dr["ItemId"] = Convert.ToInt32(GRDItemWise.Rows[k].Cells[0].Text);
                        dr["ItemCode"] =Convert.ToString(GRDItemWise.Rows[k].Cells[1].Text);
                        dr["ItemName"] = Convert.ToString(GRDItemWise.Rows[k].Cells[2].Text);
                        dr["UnitId"] = Convert.ToInt32(GRDItemWise.Rows[k].Cells[3].Text);
                        dr["Unit"] = Convert.ToString(GRDItemWise.Rows[k].Cells[4].Text);
                        dr["StockInHand"] = Convert.ToDecimal(GRDItemWise.Rows[k].Cells[5].Text);
                        dr["DamageQty"] = (((TextBox)GRDItemWise.Rows[k].FindControl("GrdtxtDamageQty")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GRDItemWise.Rows[k].FindControl("GrdtxtDamageQty")).Text);
                        dr["InwardRate"] = Convert.ToDecimal(GRDItemWise.Rows[k].Cells[7].Text);
                        dr["Amount"] = Convert.ToDecimal(GRDItemWise.Rows[k].Cells[8].Text);
                        dr["Reason"] = (((TextBox)GRDItemWise.Rows[k].FindControl("TxtReason")).Text).Trim().Equals("") ? " " : Convert.ToString(((TextBox)GRDItemWise.Rows[k].FindControl("TxtReason")).Text);

                        dt.Rows.Add(dr);
                        k++;
                    }
                    else if ((GRDItemWise.Rows[k].Cells[2].Text.Equals("&nbsp;")))
                    {

                    }
                    else
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                       // dr["#"] = (((Label)GRDItemWise.Rows[k].FindControl("LblEntryId")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((Label)GRDItemWise.Rows[k].FindControl("LblEntryId")).Text);
                        dr["ItemId"] = Convert.ToInt32(GRDItemWise.Rows[k].Cells[0].Text);
                        dr["ItemCode"] = Convert.ToString(GRDItemWise.Rows[k].Cells[1].Text);
                        dr["ItemName"] = Convert.ToString(GRDItemWise.Rows[k].Cells[2].Text);
                        dr["UnitId"] = Convert.ToInt32(GRDItemWise.Rows[k].Cells[3].Text);
                        dr["Unit"] = Convert.ToString(GRDItemWise.Rows[k].Cells[4].Text);
                        dr["StockInHand"] = Convert.ToDecimal(GRDItemWise.Rows[k].Cells[5].Text);
                        dr["DamageQty"] = (((TextBox)GRDItemWise.Rows[k].FindControl("GrdtxtDamageQty")).Text).Trim().Equals("") ? 0 : Convert.ToDecimal(((TextBox)GRDItemWise.Rows[k].FindControl("GrdtxtDamageQty")).Text);
                        dr["InwardRate"] = Convert.ToDecimal(GRDItemWise.Rows[k].Cells[7].Text);
                        dr["Amount"] = Convert.ToDecimal(GRDItemWise.Rows[k].Cells[8].Text);
                        dr["Reason"] = (((TextBox)GRDItemWise.Rows[k].FindControl("TxtReason")).Text).Trim().Equals("") ? " " : Convert.ToString(((TextBox)GRDItemWise.Rows[k].FindControl("TxtReason")).Text);
                        dt.Rows.Add(dr);
                        k++;
                    }
                }
                else
                {
                    DataRow dr;
                    dr = dt.NewRow();
                   // dr["#"] = 0;
                   
                    dr["ItemId"] = 0;
                    dr["ItemCode"] = "";
                    dr["ItemName"] = "";
                    dr["Unit"] = "";
                    dr["UnitId"] = 0;
                    dr["StockInHand"] = 0.0;
                    dr["DamageQty"] = 0.0;
                    dr["InwardRate"] = 0.0;
                    dr["Amount"] = 0.0;
                    dr["Reason"] = "";

                    dt.Rows.Add(dr);
                }
            }
            ViewState["DamagedDtls"] = dt;
            return dt;
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
                        ddlItem.DataSource = DS.Tables[0];
                        ddlItem.DataTextField = "ItemName";
                        ddlItem.DataValueField = "ItemId";
                        ddlItem.DataBind();
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

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionItemNameList(string prefixText, int count, string contextKey)
    {
        DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
        String[] SearchList = Obj_RequisitionCafeteria.GetSuggestedRecordItems(prefixText, "");
        return SearchList;
    }
 
    protected void hdnValue_ValueChanged(object sender, EventArgs e)
    {
        try
        {
            DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
            if (Convert.ToInt32(((HiddenField)sender).Value) != 0)
            {
                ViewState["EditID"] = Convert.ToInt32(((HiddenField)sender).Value);
                //DSNEW = Obj_RequisitionCafeteria.GetRate(Convert.ToInt32(ViewState["EditID"]), out StrError);
                //lstSupplierRate.DataSource = DSNEW.Tables[0];
                //lstSupplierRate.DataTextField = "Rate";
                //lstSupplierRate.DataValueField = "Rate";
                //lstSupplierRate.DataBind();
            }
        }
        catch (Exception ex)
        {
            obj_Comm.ShowPopUpMsg(ex.Message, this.Page);
        }


        ///populate the form based on retrieved data
    }

    protected void TxtItemName_TextChanged(object sender, EventArgs e)
    {
        try
        {

            //StrCondition = string.Empty;
            //StrCondition = TxtItemName.Text.Trim();           
            //DataSet Ds = new DataSet();
            //DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
            ////Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);

            ////ViewState["EditID"]

            //Ds = Obj_RequisitionCafeteria.GetItemsDetails(Convert.ToInt32(ddlCategory.SelectedValue), StrCondition, "", out StrError);
           
            //if (Ds.Tables.Count > 0 && Ds.Tables[1].Rows.Count > 0)
            //{
            //    TxtItemName.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
            //    //TxtItemName.ToolTip = Ds.Tables[1].Rows[0]["ItemId"].ToString();
            //    ddlItem.SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();
            //    lstSupplierRate.Text = Ds.Tables[1].Rows[0]["PurchaseRate"].ToString();

            //    if (Ds.Tables[2].Rows.Count > 0)
            //    {
            //        ddldesc.DataSource = Ds.Tables[2];
            //        ddldesc.DataValueField = "ItemDetailsId";
            //        ddldesc.DataTextField = "ItemDesc";
            //        ddldesc.DataBind();
            //    }

            //    if (Ds.Tables[3].Rows.Count > 0)
            //    {
            //        ddlUnitConversn.DataSource = Ds.Tables[3];
            //        ddlUnitConversn.DataValueField = "FromUnitID";
            //        ddlUnitConversn.DataTextField = "Unit";
            //        ddlUnitConversn.DataBind();
            //    }
              
            //    Ds = null;
            //}
            //else
            //{
            //    TxtItemName.Text = "";
            //    TxtItemName.ToolTip = "0";
            //}
          
            

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
