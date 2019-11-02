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

public partial class Masters_UnitConversionMaster : System.Web.UI.Page
{
    #region [Variables]
    DMUnitConversion Obj_UnitConversion = new DMUnitConversion();
    UnitConversion Entity_UnitConversion = new UnitConversion();
    CommanFunction obj_Comman = new CommanFunction();
    DataSet Ds = new DataSet();
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    private static bool FlagAdd, FlagDel, FlagEdit = false;
    int str = 0;
    public static int countPage = 0;
    #endregion

    #region [User Define Function]

    private void FillCombo()
    {
        try
        {
            Ds = Obj_UnitConversion.BindCombo(out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ddlUnit.DataSource = Ds.Tables[0];
                ddlUnit.DataTextField = "Unit";
                ddlUnit.DataValueField = "UnitId";
                ddlUnit.DataBind();

                ddlUnitConversion.DataSource = Ds.Tables[0];
                ddlUnitConversion.DataTextField = "Unit";
                ddlUnitConversion.DataValueField = "UnitId";
                ddlUnitConversion.DataBind();
            }
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg(ex.Message, this.Page);
            return;
        }
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("#", typeof(Int32));
        dt.Columns.Add("UnitId", typeof(Int32));
        dt.Columns.Add("UnitFactor", typeof(string));
        dt.Columns.Add("Qty", typeof(decimal));

        dr = dt.NewRow();

        dr["#"] = 0;
        dr["UnitId"] = 0;
        dr["UnitFactor"] = "";
        dr["Qty"] = 0;

        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        GridDetails.DataSource = dt;
        GridDetails.DataBind();
    }

    private void MakeEmptyForm()
    {
        ViewState["EditID"] = null;
        if (!FlagAdd)
            BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnDelete.Visible = false;
        ddlUnit.Focus();
        TxtSearch.Text = string.Empty;
        ddlUnit.SelectedValue = "0";
        ddlUnitConversion.SelectedValue = "0";
        //txtUnitFactor.Text = string.Empty;
        txtQty.Text = string.Empty;

        SetInitialRow();

        ReportGrid(StrCondition);

        TXTUPDATEVALUE.Text = "1";
    }

    private void MakeControlEmpty()
    {
        ddlUnitConversion.SelectedValue = "0";
        ddlUnitConversion.Enabled = true;
        //txtUnitFactor.Text = string.Empty;
        txtQty.Text = string.Empty;
        TXTUPDATEVALUE.Text = "1";
        ViewState["GridIndex"] = null;
        ImgAddGrid.ImageUrl = "~/Images/Icon/Gridadd.png";
        ImgAddGrid.ToolTip = "Add Grid";
        ddlUnitConversion.Focus();
    }

    public void ReportGrid(string RepCondition)
    {
        try
        {
            Ds = Obj_UnitConversion.GetUnit(RepCondition, out StrError);

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
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region [Web Service]

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMUnitConversion Obj_CM = new DMUnitConversion();
        String[] SearchList = Obj_CM.GetSuggestRecord(prefixText);
        return SearchList;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillCombo();
            MakeControlEmpty();
            MakeEmptyForm();
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0;
        int InsertRowDtls = 0;
        try
        {
            if (GridDetails.Rows[0].Cells[3].Text.Equals("&nbsp;"))
            {
                obj_Comman.ShowPopUpMsg("Please Enter Details ..!", this.Page);
                ddlUnit.Focus();
                return;
            }

            Entity_UnitConversion.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
            Entity_UnitConversion.LoginId = Convert.ToInt32(Session["UserID"]);
            Entity_UnitConversion.LoginDate = System.DateTime.Now;

            InsertRow = Obj_UnitConversion.InsertRecord(ref Entity_UnitConversion, out StrError);

            if (InsertRow > 0)
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtInsert = new DataTable();
                    dtInsert = (DataTable)ViewState["CurrentTable"];

                    if (dtInsert.Rows.Count > 0 && !string.IsNullOrEmpty(dtInsert.Rows[0]["UnitFactor"].ToString()))
                    {
                        for (int i = 0; i < dtInsert.Rows.Count; i++)
                        {
                            Entity_UnitConversion.UnitConvId = InsertRow;
                            Entity_UnitConversion.UnitId = Convert.ToInt32(dtInsert.Rows[i]["UnitId"].ToString());
                            Entity_UnitConversion.UnitFactor = dtInsert.Rows[i]["UnitFactor"].ToString();
                            Entity_UnitConversion.Qty = Convert.ToDecimal(dtInsert.Rows[i]["Qty"].ToString());

                            InsertRowDtls = Obj_UnitConversion.InsertRecordDtls(ref Entity_UnitConversion, out StrError);
                        }
                    }
                }
                if (InsertRow > 0 && InsertRowDtls > 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Saved Successfully..!", this.Page);
                    MakeControlEmpty();
                    MakeEmptyForm();
                    Obj_UnitConversion = null;
                    Entity_UnitConversion = null;
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Please Enter Details ..!", this.Page);
            }
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg(ex.Message, this.Page);
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        DataSet DsTemp=new DataSet();
        int UpdateRow = 0, UpdateRowDtls = 0;
        decimal qty=0;
        try
        {
            if (GridDetails.Rows[0].Cells[3].Text.Equals("&nbsp;"))
            {
                obj_Comman.ShowPopUpMsg("Please Enter Details ..!", this.Page);
                ddlUnit.Focus();
                return;
            }

            //If Record is not Updated in Details Grid(Update Image is Visible)
            if (ImgAddGrid.ImageUrl == "~/Images/Icon/GridUpdate.png")
            {
                obj_Comman.ShowPopUpMsg("Update The Current Record In Details Grid First!!", this.Page);
                return;
            }

            if (ViewState["EditID"] != null)
            {
                Entity_UnitConversion.UnitConvId = Convert.ToInt32(ViewState["EditID"]);
            }
            Entity_UnitConversion.UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
            Entity_UnitConversion.LoginId = Convert.ToInt32(Session["UserID"]);
            Entity_UnitConversion.LoginDate = System.DateTime.Now;

            UpdateRow = Obj_UnitConversion.UpdateRecord(ref Entity_UnitConversion, out StrError);

            if (UpdateRow > 0)
            {
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtInsert = new DataTable();
                    dtInsert = (DataTable)ViewState["CurrentTable"];

                    if (dtInsert.Rows.Count > 0 && !dtInsert.Rows[0]["UnitFactor"].ToString().Equals(""))
                    {
                        for (int i = 0; i < dtInsert.Rows.Count; i++)
                        {
                            Entity_UnitConversion.UnitConvId =  Convert.ToInt32(ViewState["EditID"]);
                            Entity_UnitConversion.UnitId = Convert.ToInt32(dtInsert.Rows[i]["UnitId"].ToString());
                            Entity_UnitConversion.UnitFactor = dtInsert.Rows[i]["UnitFactor"].ToString();
                            Entity_UnitConversion.Qty = Convert.ToDecimal(dtInsert.Rows[i]["Qty"].ToString());
                            qty=Convert.ToDecimal(dtInsert.Rows[i]["Qty"].ToString());
                            //----For Updating Record In UnitConversionDtlsId-----
                            if (Convert.ToInt32(dtInsert.Rows[i]["#"].ToString()) != 0)
                            {
                                UpdateRowDtls = Obj_UnitConversion.UpdateRecordDtls(Convert.ToInt32(dtInsert.Rows[i]["#"].ToString()), qty, out StrError);
                                //DsTemp = Obj_UnitConversion.GetUnitConvrsndtlsId(ref Entity_UnitConversion, out StrError);
                                //if (DsTemp.Tables.Count > 0)
                                //{
                                //    if (DsTemp.Tables[0].Rows.Count > 0)
                                //    {
                                //        UpdateRowDtls = Obj_UnitConversion.UpdateRecordDtls(Convert.ToInt32(DsTemp.Tables[0].Rows[0]["UnitConvDtlsId"]), qty, out StrError);
                                //    }
                                //    else
                                //    {
                                //        UpdateRowDtls = Obj_UnitConversion.InsertRecordDtls(ref Entity_UnitConversion, out StrError);
                                //    }
                                //}
                            }
                            else
                            {
                                UpdateRowDtls = Obj_UnitConversion.InsertRecordDtls(ref Entity_UnitConversion, out StrError);
                            }
                            
                        }

                    }
                }

                if (UpdateRow > 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Updated Successfully..!", this.Page);
                    MakeControlEmpty();
                    MakeEmptyForm();
                    Obj_UnitConversion = null;
                    Entity_UnitConversion = null;
                }
            }
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg(ex.Message, this.Page);
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
                Entity_UnitConversion.UnitConvId = DeleteId;
                Entity_UnitConversion.LoginId = Convert.ToInt32(Session["UserID"]);
                Entity_UnitConversion.LoginDate = DateTime.Now;

                int iDelete = Obj_UnitConversion.DeleteRecord(ref Entity_UnitConversion, out StrError);
                if (iDelete != 0)
                {
                    obj_Comman.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                }
            }
            Entity_UnitConversion = null;
            Obj_UnitConversion = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeControlEmpty();
        MakeEmptyForm();
    }

    protected void ImgAddGrid_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow dtTableRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dtCurrentTable.Rows[0]["UnitFactor"].ToString()))
                    {
                        dtCurrentTable.Rows.RemoveAt(0);
                    }
                }
                if (ViewState["GridIndex"] != null)
                {
                    int rowindex = Convert.ToInt32(ViewState["GridIndex"]);

                    dtCurrentTable.Rows[rowindex]["UnitId"] = Convert.ToInt32(ddlUnitConversion.SelectedValue);
                    dtCurrentTable.Rows[rowindex]["UnitFactor"] = ddlUnitConversion.SelectedItem.Text;
                    dtCurrentTable.Rows[rowindex]["Qty"] = Convert.ToDecimal(txtQty.Text);

                    ViewState["CurrentTable"] = dtCurrentTable;
                    GridDetails.DataSource = dtCurrentTable;
                    GridDetails.DataBind();
                    MakeControlEmpty();
                }
                else
                {
                    dtTableRow = dtCurrentTable.NewRow();

                    dtTableRow["#"] = 0;

                    dtTableRow["UnitId"] = Convert.ToInt32(ddlUnitConversion.SelectedValue);
                    dtTableRow["UnitFactor"] = ddlUnitConversion.SelectedItem.Text;
                    dtTableRow["Qty"] = Convert.ToDecimal(txtQty.Text);

                    dtCurrentTable.Rows.Add(dtTableRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    GridDetails.DataSource = dtCurrentTable;
                    GridDetails.DataBind();
                    MakeControlEmpty();
                }
            }
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg(ex.Message, this.Page);
        }
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        ReportGrid(StrCondition);
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
                            Ds = Obj_UnitConversion.GetRecordToEdit(Convert.ToInt32(e.CommandArgument), out StrError);
                            if (Ds.Tables.Count > 0)
                            {
                                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                                {
                                    ddlUnit.SelectedValue = Ds.Tables[0].Rows[0]["UnitId"].ToString();
                                }
                                if (Ds.Tables[1].Rows.Count > 0)
                                {
                                    GridDetails.DataSource = Ds.Tables[1];
                                    GridDetails.DataBind();
                                    ViewState["CurrentTable"] = Ds.Tables[1];
                                }
                                else
                                {
                                    MakeEmptyForm();
                                }
                            }
                            Ds = null;
                            Obj_UnitConversion = null;
                            if (!FlagEdit)
                                BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            if (!FlagDel)
                                BtnDelete.Visible = true;
                            ddlUnit.Focus();
                            MakeControlEmpty();
                        }
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg(ex.Message, this.Page);
            return;
        }
    }

    protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Index;
            if (e.CommandName == "SelectGrid")
            {
                if (string.IsNullOrEmpty(GridDetails.Rows[0].Cells[3].Text) || (GridDetails.Rows[0].Cells[3].Text).Equals("&nbsp;"))
                {
                    obj_Comman.ShowPopUpMsg("There is no record to edit", this.Page);
                }
                else
                {
                    ImgAddGrid.ImageUrl = "~/Images/Icon/GridUpdate.png";
                    ImgAddGrid.ToolTip = "Update";

                    Index = Convert.ToInt32(e.CommandArgument);

                    ViewState["GridIndex"] = Index;
                    ddlUnitConversion.SelectedValue = GridDetails.Rows[Index].Cells[2].Text.ToString();
                    ddlUnitConversion.Enabled = false;
                    //txtUnitFactor.Text = GridDetails.Rows[Index].Cells[2].Text;
                    txtQty.Text = GridDetails.Rows[Index].Cells[4].Text;
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void GridDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = Convert.ToInt32(hiddenbox.Value);
            if (i == 1)
            {
                if (string.IsNullOrEmpty(GridDetails.Rows[0].Cells[3].Text) || (GridDetails.Rows[0].Cells[3].Text).Equals("&nbsp;"))
                {
                    obj_Comman.ShowPopUpMsg("There Is No Record To Delete", this.Page);
                }
                else
                {
                    if (ViewState["CurrentTable"] != null)
                    {
                        int id = e.RowIndex;
                        DataTable dt = (DataTable)ViewState["CurrentTable"];

                        dt.Rows.RemoveAt(id);
                        if (dt.Rows.Count > 0)
                        {
                            GridDetails.DataSource = dt;
                            ViewState["CurrentTable"] = dt;
                            GridDetails.DataBind();
                        }
                        else
                        {
                            SetInitialRow();
                        }
                        MakeControlEmpty();
                    }
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlUnit.SelectedValue) > 0)
            {
                Ds = Obj_UnitConversion.GetUnitForDuplicate(Convert.ToInt32(ddlUnit.SelectedValue), out StrError);
                if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    obj_Comman.ShowPopUpMsg("This Unit Is Already Present. Please Select The Other Unit from List!", this.Page);
                    ddlUnit.Focus();
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg(ex.Message, this.Page);
            return;
        }
    }
}