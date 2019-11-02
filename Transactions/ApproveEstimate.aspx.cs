using CrystalDecisions.CrystalReports.Engine;
using MayurInventory.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transactions_ApproveEstimate : System.Web.UI.Page
{
    CommanFunction obj_Comman = new CommanFunction();
    ReportDocument CRpt = new ReportDocument();
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    DataSet Ds = new DataSet();
    private static bool FlagAdd, FlagDel, FlagEdit, FlagPrint = false;
    database db = new database();

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

                DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Authorized Requisition'");
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
                // //Checking View Right ========                    
                //if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                //{
                //    ReportGrid.Visible = false;
                //}
                //Checking Add Right ========                    
                if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                {
                    BtnSave.Visible = false;
                    FlagAdd = true;

                    //}
                    ////Edit /Delete Column Visible ========
                    //if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false &&
                    //    Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    //{
                    //    foreach (GridViewRow GRow in ReportGrid.Rows)
                    //    {
                    //        GRow.FindControl("ImgBtnDelete").Visible = false;
                    //        GRow.FindControl("ImageGridEdit").Visible = false;
                    //        GRow.FindControl("ImgBtnPrint").Visible = false;
                    //    }
                    //    //BtnUpdate.Visible = false;
                    //    FlagDel = true;
                    //    FlagEdit = true;
                    //    FlagPrint = true;
                    //}
                    //else
                    //{
                    //    //Checking Delete Right ========
                    //    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                    //    {
                    //        foreach (GridViewRow GRow in ReportGrid.Rows)
                    //        {
                    //            GRow.FindControl("ImgBtnDelete").Visible = false;
                    //            FlagDel = true;
                    //        }
                    //    }

                    //    //Checking Edit Right ========
                    //    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    //    {
                    //        foreach (GridViewRow GRow in ReportGrid.Rows)
                    //        {
                    //            GRow.FindControl("ImageGridEdit").Visible = false;
                    //            FlagEdit = true;
                    //        }
                    //    }

                    //    //Checking Print Right ========
                    //    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    //    {
                    //        foreach (GridViewRow GRow in ReportGrid.Rows)
                    //        {
                    //            GRow.FindControl("ImgBtnPrint").Visible = false;
                    //            FlagPrint = true;
                    //        }
                    //    }
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
    private void MakeEmptyForm()
    {
        BindReportGrid(StrCondition);
        GETHIGHLITE();
        SetInitialRow_GRDPOPUPFOREDIT();
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        //show1.Visible = false;
        if (!FlagAdd)
            BtnSave.Visible = true;
        BtnAuthorized.Visible = false;
        TR_PODtls.Visible = false;
        TxtSearch.Text = string.Empty;
    }
    public void GETHIGHLITE()
    {
        for (int i = 0; i < GrdReq.Rows.Count; i++)
        {
            if ((GrdReq.Rows[i].Cells[6].Text) == "Generated")
            {
                GrdReq.Rows[i].BackColor = System.Drawing.Color.White;
            }
            if ((GrdReq.Rows[i].Cells[6].Text) == "Approved")
            {
                GrdReq.Rows[i].BackColor = System.Drawing.Color.Yellow;
            }
            if ((GrdReq.Rows[i].Cells[6].Text) == "Authorised")
            {
                GrdReq.Rows[i].BackColor = System.Drawing.Color.Green;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
    }
    private void SetInitialRow_GRDPOPUPFOREDIT()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("RequestId", typeof(int));
            dt.Columns.Add("RequestNo", typeof(string));
            dt.Columns.Add("RequestDate", typeof(string));
            dt.Columns.Add("ItemId", typeof(int));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("Qty", typeof(Decimal));
            dt.Columns.Add("Rate", typeof(Decimal));
            dt.Columns.Add("Emp", typeof(string));
            dt.Columns.Add("Priority", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("RemarkFull", typeof(string));
            dt.Columns.Add("RemarkAuth", typeof(string));
            dr = dt.NewRow();

            dr["RequestId"] = 0;
            dr["RequestNo"] = "";
            dr["RequestDate"] = "";
            dr["ItemId"] = 0;
            dr["ItemCode"] = "";
            dr["ItemName"] = "";
            dr["Qty"] = 0;
            dr["Rate"] = 0;
            dr["Emp"] = "";
            dr["Priority"] = "";
            dr["Remark"] = "";
            dr["RemarkFull"] = "";
            dr["Location"] = "";
            dr["RemarkAuth"] = "";
            dt.Rows.Add(dr);

            ViewState["CurrentTablePO"] = dt;
            GRDPOPUPFOREDIT.DataSource = dt;
            GRDPOPUPFOREDIT.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public void BindReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            string RepCondition1 = string.Empty;
            //if (!Convert.ToBoolean(Session["IsCentral"].ToString()))
            //{
            //    RepCondition1 = RepCondition1 + " AND D.CafeteriaId=" + Convert.ToInt32(Session["CafeteriaId"].ToString());
            //}
            DataTable dt = new DataTable();
            dt= db.Displaygrid("select   id as #, indentno ,party  ,status  as status  from estimatemaster where status <>  '"+"Authorised"+"' ");
            DsReport.Tables.Add(dt);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                GrdReq.DataSource = DsReport.Tables[0];
                GrdReq.DataBind();
            }
            else
            {
                GrdReq.DataSource = null;
                GrdReq.DataBind();
               // SetInitialRow_ReqDetails();
            }


            //if (DsReport.Tables.Count > 0 && DsReport.Tables[1].Rows.Count > 0)
            //{
            //    LblGen.Text = DsReport.Tables[1].Rows[0][0].ToString();
            //    LblApprov.Text = DsReport.Tables[1].Rows[0][1].ToString();
            //    LblAutho.Text = DsReport.Tables[1].Rows[0][2].ToString();
            //}
            //else
            //{
            //    LblAutho.Text = LblApprov.Text = LblGen.Text = "0";
            //}
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckUserRight();
            MakeEmptyForm();
        }
    }
    protected void ImgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
    }


    protected void GrdReq_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int j = 0; j < GrdReq.Rows.Count; j++)
        {
            if ((GrdReq.Rows[j].Cells[6].Text) == "Generated")
            {
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageApproved")).Visible = true;
            }
            if ((GrdReq.Rows[j].Cells[6].Text) == "Approved")
            {
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageApproved")).Visible = false;
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageAuthorised")).Visible = true;
            }
            if ((GrdReq.Rows[j].Cells[6].Text) == "Authorised")
            {
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageAuthorised")).Visible = false;
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageApproved")).Visible = false;
                ((ImageButton)GrdReq.Rows[j].FindControl("ImageSuccess")).Visible = true;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }



    protected void GrdReq_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        int CurrRow = Convert.ToInt32(e.CommandArgument);
                        int RequisitnId = Convert.ToInt32(Convert.ToInt32(((Label)GrdReq.Rows[CurrRow].FindControl("LblEntryId")).Text));
                        BindPurchaseOrderDetails(RequisitnId);
                    }
                    break;

                case ("Delete"):
                    {
                    }
                    break;
                case ("SHOWPOPUP"):
                    {
                        int CurrRow = Convert.ToInt32(e.CommandArgument);
                        int RequisitnId = Convert.ToInt32(Convert.ToInt32(((Label)GrdReq.Rows[CurrRow].FindControl("LblEntryId")).Text));
                        //BindPurchaseOrderDetailsFORPOPUP(RequisitnId);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:ShowPOP();", true);


                    }
                    break;

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public void BindPurchaseOrderDetails(Int32 RequisitionCafeId)
    {
        //try
        //{
        //    Ds = Obj_ApproveRequisition.GetRequisitionDtls(RequisitionCafeId, out StrError);
        //    if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
        //    {
        //        GrdReqDtls.DataSource = Ds.Tables[0];
        //        GrdReqDtls.DataBind();

        //        TR_PODtls.Visible = true;
        //    }
        //    else
        //    {
        //        GrdReqDtls.DataSource = null;
        //        GrdReqDtls.DataBind();

        //        TR_PODtls.Visible = false;
        //    }
        //}
        //catch (Exception ex) { throw new Exception(ex.Message); }
    }



    protected void ImageApproved_Click(object sender, EventArgs e)
    {
        //------Approved Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        GrdReq.Rows[currrow].Cells[6].Text = "Approved";
        ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageCancel")).Visible = true;
        ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageApproved")).Visible = false;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }
    protected void ImageAuthorised_Click(object sender, EventArgs e)
    {
        //------Authorised Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        GrdReq.Rows[currrow].Cells[6].Text = "Authorised";
        ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageCancel")).Visible = true;
        ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageAuthorised")).Visible = false;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
    
        for (int j = 0; j < GrdReq.Rows.Count; j++)
        {
            if ((GrdReq.Rows[j].Cells[6].Text) == "Approved")
            {
                string STR = ((Label)GrdReq.Rows[j].FindControl("LblEntryId")).Text;
                db.insert("update estimatemaster set  status='"+ "Approved" + "'  where id='"+STR+"' ");
            }
            if ((GrdReq.Rows[j].Cells[6].Text) == "Authorised")
            {
                string STR = ((Label)GrdReq.Rows[j].FindControl("LblEntryId")).Text;
                db.insert("update estimatemaster set  status='" + "Authorised" + "'  where id='" + STR + "' ");
            }
        }
        BindReportGrid("");
        GETHIGHLITE();
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
    }

    protected void ImageCancel_Click(object sender, EventArgs e)
    {
        //------Cancel Image------
        ImageButton Imagebtn = (ImageButton)sender;
        GridViewRow grd = (GridViewRow)Imagebtn.Parent.Parent;
        int currrow = grd.RowIndex;
        if (GrdReq.Rows[currrow].Cells[6].Text == "Approved")
        {
            GrdReq.Rows[currrow].Cells[6].Text = "Generated";
            ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageApproved")).Visible = true;
            ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageCancel")).Visible = false;
        }
        if (GrdReq.Rows[currrow].Cells[6].Text == "Authorised")
        {
            GrdReq.Rows[currrow].Cells[6].Text = "Approved";
            ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageAuthorised")).Visible = true;
            ((ImageButton)GrdReq.Rows[currrow].FindControl("ImageCancel")).Visible = false;
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);


    }
}