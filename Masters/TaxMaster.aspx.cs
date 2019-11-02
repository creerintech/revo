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

public partial class Masters_TaxMaster : System.Web.UI.Page
{
    #region[Private Variables]
    DMTaxMaster Obj_TaxMaster = new DMTaxMaster();
        TaxMaster Entity_TaxMaster = new TaxMaster();
        CommanFunction obj_Comman = new CommanFunction();
        DataSet Ds = new DataSet();
        DataTable dtTable = new DataTable();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
        private static bool FlagAdd,FlagDel,FlagEdit=false;
    #endregion

    #region[UserDefineFunctions]

        //private void SetInitialRow()
        //{
        //    DataTable dt = new DataTable();
        //    DataRow dr;

        //    dt.Columns.Add("#", typeof(Int32));
        //    dt.Columns.Add("From", typeof(string));
        //    dt.Columns.Add("To", typeof(string));
        //    dt.Columns.Add("Taxper", typeof(string));
           
        //    dr = dt.NewRow();

        //    dr["#"] = 0;
        //    dr["From"] = "";
        //    dr["To"] = "";
        //    dr["Taxper"] = "";

        //    dt.Rows.Add(dr);

        //    ViewState["CurrentTable"] = dt;
        //    Grd_Tax.DataSource = dt;
        //    Grd_Tax.DataBind();
        //}

    private void MakeEmptyForm()
    {
        ViewState["EditID"] = null;
        TxtTaxName.Focus();
        if(!FlagAdd)
        BtnSave.Visible = true;
        BtnDelete.Visible = false;
        BtnUpdate.Visible = false;
        RdoTaxType.SelectedValue = "1";
        TxtTaxPer.Text = TxtTaxName.Text = TXTEFFECTIVEDATE.Text = string.Empty;
        TxtSearch.Text = string.Empty;
        TxtTaxName.Enabled = true;
        SetInitialRow();
        ReportGrid(StrCondition);
    }



    public void SetInitialRow()
    {

        try
        {
            DataRow dr;
            dtTable.Columns.Add("#", typeof(int));
            dtTable.Columns.Add("GSTPer", typeof(decimal));
            dtTable.Columns.Add("NewGSTApplicableDate", typeof(string));

            dr = dtTable.NewRow();
            dr["#"] = 0;
            dr["GSTPer"] = 0;
            dr["NewGSTApplicableDate"] = "";

            dtTable.Rows.Add(dr);
            ViewState["CurrentTable"] = dtTable;
            GridDetails.DataSource = dtTable;
            GridDetails.DataBind();

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void ReportGrid(string RepCondition)
    {
        try
        {
            Ds = Obj_TaxMaster.GetTaxMaster(RepCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                GrdReport.DataSource = Ds.Tables[0];
                GrdReport.DataBind();
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }
            Obj_TaxMaster = null;
            Ds = null;
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
                //if (!Session["UserRole"].Equals("Administrator"))
                //{
                    //Checking Right of users=======

                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Item Category Master'");
                    if (dtRow.Length > 0)
                    {
                        DataTable dt = dtRow.CopyToDataTable();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        Response.Redirect("~/NotAuthUser.aspx");
                    }
                    //Checking View Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                    {
                        GrdReport.Visible = false;
                    }
                    //Checking Add Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                    {
                        BtnSave.Visible = false;
                        BtnCancel.Visible = false;
                        FlagAdd = true;

                    }
                    //Edit /Delete Column Visible ========
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        BtnDelete.Visible = false;
                        BtnUpdate.Visible = false;
                        FlagDel = true;
                        FlagEdit = true;
                    }
                    else
                    {
                        //Checking Delete Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                        {
                            BtnDelete.Visible = false;
                            FlagDel = true;
                        }

                        //Checking Edit Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                        {
                            BtnUpdate.Visible = false;
                            FlagEdit = true;
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
        catch (ThreadAbortException)
        {
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //User Right Function===========
    private void MakeControlEmpty()
    {
        TxtTaxPer.Text = string.Empty;
        TXTEFFECTIVEDATE.Text = string.Empty;
        ViewState["GridIndex"] = null;
    }

    #endregion

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMTaxMaster Obj_TaxMaster = new DMTaxMaster();
        String[] SearchList = Obj_TaxMaster.GetSuggestedRecord(prefixText);
        return SearchList;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckUserRight();
            MakeEmptyForm();
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0, InserRowDtls = 0;
        try
        {
            Ds = Obj_TaxMaster.ChkDuplicate(TxtTaxName.Text.Trim(), out StrError);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                obj_Comman.ShowPopUpMsg("Please Enter Another Tax Name..", this.Page);
                TxtTaxName.Focus();
            }
            else
            {
                Entity_TaxMaster.TaxName = TxtTaxName.Text.Trim();
                //Entity_TaxMaster.TaxPer = Convert.ToDecimal(TxtTaxPer.Text.Trim());
                //Entity_TaxMaster.EffectiveFrom = Convert.ToDateTime(TXTEFFECTIVEDATE.Text.Trim());
                Entity_TaxMaster.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_TaxMaster.LoginDate = DateTime.Now;
                ////Entity_TaxMaster.TaxTypeID = Convert.ToInt32(RdoTaxType.SelectedValue.ToString());
                InsertRow = Obj_TaxMaster.InsertRecord(ref Entity_TaxMaster, out StrError);

                for (int i = 0; i < GridDetails.Rows.Count; i++)
                {
                    Entity_TaxMaster.TaxId = InsertRow;
                    if ((GridDetails.Rows[i].Cells[2]).Text != "&nbsp;")
                    {
                        Entity_TaxMaster.ApplicableDate = DateTime.Parse((GridDetails.Rows[i].Cells[2]).Text);
                    }
                    else
                    {
                        Entity_TaxMaster.ApplicableDate = DateTime.Parse(DateTime.Now.ToString("dd/MMM/yyyy"));
                    }
                    Entity_TaxMaster.GST = Convert.ToDecimal(GridDetails.Rows[i].Cells[1].Text.ToString());

                    InserRowDtls = Obj_TaxMaster.InsertTaxtls(ref Entity_TaxMaster, out StrError);
                }
                if (InsertRow != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Saved Successfully", this.Page);
                    MakeEmptyForm();
                    Entity_TaxMaster = null;
                    Obj_TaxMaster = null;
                }
            }
        }
        catch(Exception ex) { throw new Exception(ex.Message); }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0, InserRow=0;
        try
        {
            if (ViewState["EditID"] != null)
            {
                Entity_TaxMaster.TaxId = Convert.ToInt32(ViewState["EditID"]);
            }
            Entity_TaxMaster.TaxName = TxtTaxName.Text.Trim();
            //Entity_TaxMaster.TaxPer = Convert.ToDecimal(TxtTaxPer.Text.Trim());
            //Entity_TaxMaster.EffectiveFrom = Convert.ToDateTime(TXTEFFECTIVEDATE.Text.Trim());
            Entity_TaxMaster.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_TaxMaster.LoginDate = DateTime.Now;
            //Entity_TaxMaster.TaxTypeID = Convert.ToInt32(RdoTaxType.SelectedValue.ToString());
            UpdateRow = Obj_TaxMaster.UpdateRecord(ref Entity_TaxMaster, out StrError);
            for (int i = 0; i < GridDetails.Rows.Count; i++)
            {
                Entity_TaxMaster.TaxId = Convert.ToInt32(ViewState["EditID"]);

                if ((GridDetails.Rows[i].Cells[2]).Text != "&nbsp;")
                {
                    Entity_TaxMaster.ApplicableDate = DateTime.Parse((GridDetails.Rows[i].Cells[2]).Text);
                }
                else
                {
                    Entity_TaxMaster.ApplicableDate = DateTime.Parse(DateTime.Now.ToString("dd/MMM/yyyy"));
                }

                Entity_TaxMaster.GST = Convert.ToDecimal(GridDetails.Rows[i].Cells[1].Text.ToString());


                if (Convert.ToInt32(GridDetails.Rows[i].Cells[19].Text) == 0)
                {
                    InserRow = Obj_TaxMaster.InsertTaxtls(ref Entity_TaxMaster, out StrError);
                }
                else
                {
                    Entity_TaxMaster.TaxDtlsId = Convert.ToInt32(GridDetails.Rows[i].Cells[19].Text);

                    InserRow = Obj_TaxMaster.UpdateTaskDetails(ref Entity_TaxMaster, out StrError);

                
                }

               

            }
            if (UpdateRow != 0)
            {
                obj_Comman.ShowPopUpMsg("Record Updated Successfully", this.Page);
                MakeEmptyForm();
                Entity_TaxMaster = null;
                Obj_TaxMaster = null;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int DeleteId = 0;
            if (ViewState["EditID"] != null)
            {
                DeleteId = Convert.ToInt32(ViewState["EditID"]);
            }
            if (DeleteId != 0)
            {
                Entity_TaxMaster.TaxId = DeleteId;
                Entity_TaxMaster.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_TaxMaster.LoginDate = DateTime.Now;

                int iDelete = Obj_TaxMaster.DeleteRecord(ref Entity_TaxMaster, out StrError);
                if (iDelete != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_TaxMaster = null;
            Obj_TaxMaster = null;
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void GrdReport_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        if (Convert.ToInt32(e.CommandArgument) != 0)
                        {
                            ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                            Ds = Obj_TaxMaster.GetTaxMasterForEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                            {
                                TxtTaxName.Text = Ds.Tables[0].Rows[0]["TaxName"].ToString();
                                //TxtTaxPer.Text = Ds.Tables[0].Rows[0]["TaxPer"].ToString();
                                //TXTEFFECTIVEDATE.Text =!string.IsNullOrEmpty(Ds.Tables[0].Rows[0]["EffectiveFrom"].ToString())?Ds.Tables[0].Rows[0]["EffectiveFrom"].ToString():DateTime.Now.ToString("dd-MMM-yyyy");
                                ////TxtTaxName.Enabled = false;
                                //RdoTaxType.SelectedValue = !string.IsNullOrEmpty(Ds.Tables[0].Rows[0]["TaxTypeID"].ToString()) ? Convert.ToString((Ds.Tables[0].Rows[0]["TaxTypeID"].ToString())) : "1";
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            if (Ds.Tables.Count > 0 && Ds.Tables[1].Rows.Count > 0)
                            {
                                GridDetails.DataSource = Ds.Tables[1];
                                GridDetails.DataBind();
                                ViewState["CurrentTable"] = Ds.Tables[1];
                            }
                            else
                            {
                                GridDetails.DataSource = null;
                                GridDetails.DataBind();
                                ViewState["CurrentTable"] = null;
                            }
                            Ds = null;
                            Obj_TaxMaster = null;
                            if(!FlagEdit)
                            BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            if (!FlagDel )
                            BtnDelete.Visible = true;
                            TxtTaxName.Focus();
                        }
                        break;
                    }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        ReportGrid(StrCondition);
    }

    protected void ImgBtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow dtTableRow = null;
                bool DupFlag = false;
                int k = 0;

                if (dtCurrentTable.Rows.Count == 1 && string.IsNullOrEmpty(dtCurrentTable.Rows[0]["NewGSTApplicableDate"].ToString()))
                {
                    dtCurrentTable.Rows.RemoveAt(0);
                }
                if (ViewState["GridIndex"] != null)
                {
                    k = Convert.ToInt32(ViewState["GridIndex"]);
                    dtCurrentTable.Rows[k]["GSTPer"] = TxtTaxPer.Text;
                    dtCurrentTable.Rows[k]["NewGSTApplicableDate"] = TXTEFFECTIVEDATE.Text;

                    ViewState["CurrentTable"] = dtCurrentTable;
                    GridDetails.DataSource = dtCurrentTable;
                    GridDetails.DataBind();
                    MakeControlEmpty();
                }
                else
                {
                    dtTableRow = dtCurrentTable.NewRow();
                    int rowindex = Convert.ToInt32(ViewState["GridIndex"]);

                    dtTableRow["GSTPer"] = TxtTaxPer.Text;
                    dtTableRow["NewGSTApplicableDate"] = TXTEFFECTIVEDATE.Text;
                    dtCurrentTable.Rows.Add(dtTableRow);

                    ViewState["CurrentTable"] = dtCurrentTable;
                    GridDetails.DataSource = dtCurrentTable;
                    GridDetails.DataBind();
                    MakeControlEmpty();
                }

            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Index;
            if (e.CommandName == "Select")
            {
                ImgBtnAdd.ToolTip = "Update";

                Index = Convert.ToInt32(e.CommandArgument);

                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        ViewState["GridIndex"] = Index;
                        TxtTaxPer.Text = dtCurrentTable.Rows[Index]["GSTPer"].ToString();
                        TXTEFFECTIVEDATE.Text = dtCurrentTable.Rows[Index]["NewGSTApplicableDate"].ToString();
                    }
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void GridDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            if (ViewState["CurrentTable"] != null)
            {
                int id = e.RowIndex;
                DataTable dt = (DataTable)ViewState["CurrentTable"];

                dt.Rows.RemoveAt(id);

                if (dt.Rows.Count > 0)
                {
                    GridDetails.DataSource = dt;
                    GridDetails.DataBind();
                    ViewState["CurrentTable"] = dt;
                }
                else
                {
                    SetInitialRow();
                }
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
