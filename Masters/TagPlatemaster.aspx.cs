using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MayurInventory.Utility;
using MayurInventory.DataModel;


public partial class Masters_TagPlatemaster : System.Web.UI.Page
{
    DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
    RequisitionCafeteria Entity_RequisitionCafeteria = new RequisitionCafeteria();
    private string StrCondition = string.Empty;
    CommanFunction obj_Comman = new CommanFunction();
    database db = new database();
    DataSet Ds = new DataSet();
    private string StrError = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            SetInitialRow_Grdwood();

            SetInitialRow_ReportGrid();
            bindreport();


        }

    }



    public void SetInitialRow_Grdwood()
    {
        try
        {
            DataTable dtplate = new DataTable();
            DataRow dr;
            dtplate.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dtplate.Columns.Add("ItemName", typeof(string));

            dtplate.Columns.Add(new DataColumn("WoodenBoxSize", typeof(string)));


            dr = dtplate.NewRow();
            dr["RowNumber"] = 1;

            dr["ItemName"] = "";

            dr["WoodenBoxSize"] = "";




            dtplate.Rows.Add(dr);
            ViewState["CurrentTablewood"] = dtplate;
            grdwoodenbox.DataSource = dtplate;
            grdwoodenbox.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public void SetInitialRow_ReportGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("#", typeof(int)));
            dt.Columns.Add(new DataColumn("TemplateName", typeof(string)));
            dt.Columns.Add(new DataColumn("title", typeof(string)));

            // dt.Columns.Add(new DataColumn("EmailStatus"))

            dr = dt.NewRow();
            dr["#"] = 0;
            dr["TemplateName"] = string.Empty;
            dr["title"] = string.Empty;

            dt.Rows.Add(dr);
            ReportGrid.DataSource = dt;
            ReportGrid.DataBind();
        }
        catch (Exception ex)
        {
        }
    }
    public void bindreport()
    {
        ReportGrid.DataSource = db.Displaygrid("select distinct 0 as # , itemname as TemplateName ,0 as title from nonsttagplate");
        ReportGrid.DataBind();
    }


    private void AddNewRowToGridwood()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTablewood"] != null)
        {
            DataTable CurrentTablewood = (DataTable)ViewState["CurrentTablewood"];
            DataRow drCurrentRow = null;
            if (CurrentTablewood.Rows.Count > 0)
            {
                for (int i = 1; i <= CurrentTablewood.Rows.Count; i++)
                {
                    //extract the TextBox values

                    // AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemNamewooden = (TextBox)grdwoodenbox.Rows[rowIndex].Cells[0].FindControl("TxtItemNamewooden");


                    TextBox txtwoodenbox = (TextBox)grdwoodenbox.Rows[rowIndex].Cells[1].FindControl("txtwoodenbox");








                    drCurrentRow = CurrentTablewood.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    CurrentTablewood.Rows[i - 1]["ItemName"] = TxtItemNamewooden.Text;

                    CurrentTablewood.Rows[i - 1]["WoodenBoxSize"] = txtwoodenbox.Text;





                    rowIndex++;
                }

                CurrentTablewood.Rows.Add(drCurrentRow);
                ViewState["CurrentTablekey"] = CurrentTablewood;

                grdwoodenbox.DataSource = CurrentTablewood;
                grdwoodenbox.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDatawood();
    }





    private void SetPreviousDatawood()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTablewood"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTablewood"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //   AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdwoodenbox.Rows[rowIndex].Cells[0].FindControl("TxtItemNamewooden");

                    TextBox txtwoodenbox = (TextBox)grdwoodenbox.Rows[rowIndex].Cells[1].FindControl("txtwoodenbox");









                    TxtItemName.Text = dt.Rows[i]["ItemName"].ToString();

                    txtwoodenbox.Text = dt.Rows[i]["WoodenBoxSize"].ToString();





                    rowIndex++;

                }
            }
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionItemNameList(string prefixText, int count, string contextKey)
    {
        DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
        String[] SearchList = Obj_RequisitionCafeteria.GetSuggestedRecordItems(prefixText, "");
        return SearchList;
    }





    protected void BtnSave_Click(object sender, EventArgs e)
    {














        string txtwoood = "0";
        for (int i = 0; i < grdwoodenbox.Rows.Count; i++)
        {
            try
            {
                string itemname = (((TextBox)grdwoodenbox.Rows[i].FindControl("TxtItemNamewooden")).Text);

                TextBox txt = grdwoodenbox.Rows[i].FindControl("txtwoodenbox") as TextBox;
                if (string.IsNullOrEmpty(txt.Text))
                {
                    txtwoood = 0.ToString();
                }
                else
                {
                    txtwoood = (((TextBox)grdwoodenbox.Rows[i].FindControl("txtwoodenbox")).Text);

                }



                db.insert("Insert Into nonsttagplate values('" + "0" + "' ,'" + itemname + "' ,'" + txtwoood + "' )");
                bindreport();
            }
            catch (Exception ex)
            {
                obj_Comman.ShowPopUpMsg("Error!", this.Page);
            }
        }




        obj_Comman.ShowPopUpMsg("Record Save Successfully!", this.Page);


    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {


        db.insert("delete nonsttagplate  ");





        string txtwoood = "0";
        for (int i = 0; i < grdwoodenbox.Rows.Count; i++)
        {
            try
            {
                string itemname = (((TextBox)grdwoodenbox.Rows[i].FindControl("TxtItemNamewooden")).Text);

                TextBox txt = grdwoodenbox.Rows[i].FindControl("txtwoodenbox") as TextBox;
                if (string.IsNullOrEmpty(txt.Text))
                {
                    txtwoood = 0.ToString();
                }
                else
                {
                    txtwoood = (((TextBox)grdwoodenbox.Rows[i].FindControl("txtwoodenbox")).Text);

                }



                db.insert("Insert Into nonsttagplate values('" + "0" + "' ,'" + itemname + "' ,'" + txtwoood + "' )");
            }
            catch (Exception ex)
            {
                obj_Comman.ShowPopUpMsg("Error!", this.Page);
            }
        }

        bindreport();
        obj_Comman.ShowPopUpMsg("Record Updated Successfully!", this.Page);



    }



    protected void ReportGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        BtnSave.Visible = false;
                        BtnUpdate.Visible = true;
                        ViewState["EditID"] = (e.CommandArgument);




                        grdwoodenbox.DataSource = db.Displaygrid("select   itemname  , WoodenBoxSize  from nonsttagplate where iterlockid='" + 0 + "'");

                        grdwoodenbox.DataBind();

                        ViewState["CurrentTablewood"] = db.Displaygrid("select id as RowNumber,  itemname  , WoodenBoxSize from nonsttagplate where iterlockid='" + 0 + "'");
                        DataTable dtwood = new DataTable();
                        dtwood = db.Displaygrid("select *  from nonsttagplate where iterlockid='" + ViewState["EditID"] + "'");

                        for (int i = 0; i < grdwoodenbox.Rows.Count; i++)
                        {
                            ((TextBox)grdwoodenbox.Rows[i].FindControl("TxtItemNamewooden")).Text = dtwood.Rows[i]["itemname"].ToString();
                            ((TextBox)grdwoodenbox.Rows[i].FindControl("txtwoodenbox")).Text = dtwood.Rows[i]["WoodenBoxSize"].ToString();
                        }








                    }
                    break;
                case ("Delete"):
                    {
                        ViewState["EditID"] = (e.CommandArgument);

                        db.insert("delete nonsttagplate where  iterlockid='" + ViewState["EditID"] + "' ");


                        obj_Comman.ShowPopUpMsg("Record Deleted Successfully!", this.Page);




                    }
                    break;
                case ("DeleteMR"):
                    {
                        //ViewState["DeleteID"] = Convert.ToInt32(e.CommandArgument);
                        //Entity_RequisitionCafeteria.RequisitionCafeId = Convert.ToInt32(e.CommandArgument);
                        //Entity_RequisitionCafeteria.UserId = Convert.ToInt32(Session["UserId"]);
                        //Entity_RequisitionCafeteria.LoginDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        //int DeletedRow = Obj_RequisitionCafeteria.DeleteRequisition(ref Entity_RequisitionCafeteria, out StrError);
                        //if (DeletedRow != 0)
                        //{
                        //    obj_Comman.ShowPopUpMsg("Record Deleted Successfully!", this.Page);
                        //    MakeEmptyForm();
                        //}
                    }
                    break;
                case ("MailIndent"):
                    {
                        //TRLOADING.Visible = false;
                        //ViewState["MailID"] = Convert.ToInt32(e.CommandArgument);
                        //GETDATAFORMAIL(1, 1);
                        //MDPopUpYesNoMail.Show();
                        //BtnPopMail.Focus();
                    }
                    break;
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void ReportGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


    protected void btnwoodenbox_OnClick(object sender, EventArgs e)
    {
        AddNewRowToGridwood();
    }




    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AjaxControlToolkit.ComboBox ddlItems = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd = (GridViewRow)ddlItems.Parent.Parent;
            int currrow = grd.RowIndex;
            string a = ViewState["ddlItem"].ToString();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(a), out StrError);

            DataTable dt = new DataTable();
            dt = Ds.Tables[0];
            DataTable dt1 = new DataTable();
            dt1 = Ds.Tables[1];
            DataTable dt2 = new DataTable();
            dt2 = Ds.Tables[2];
            DataTable dt3 = new DataTable();
            dt3 = Ds.Tables[3];

            ViewState["ParticularItem"] = Ds.Tables[0];
            DataTable dtt = new DataTable();
            dtt = (DataTable)ViewState["ParticularItem"];
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {

            }
            else
            {

            }
            //  GetAvaliableQuantityForItem(currrow);


            #region[Bind ItemDescription and UnitConverSion]
            if (Ds.Tables[2].Rows.Count > 0)
            {
                //ViewState["ItemDesCriptionList"] = Ds.Tables[2];
                //((AjaxControlToolkit.ComboBox)grditem.Rows[currrow].FindControl("ddlItemDescription")).DataSource = Ds.Tables[2];
                //((AjaxControlToolkit.ComboBox)grditem.Rows[currrow].FindControl("ddlItemDescription")).DataTextField = "ItemDesc";
                //((AjaxControlToolkit.ComboBox)grditem.Rows[currrow].FindControl("ddlItemDescription")).DataValueField = "#";
                //((AjaxControlToolkit.ComboBox)grditem.Rows[currrow].FindControl("ddlItemDescription")).DataBind();
                //string UNIT = string.Empty;
                //for (int i = 1; i < Ds.Tables[2].Rows.Count; i++)
                //{
                //    UNIT = UNIT + "\n, " + Ds.Tables[2].Rows[i]["ItemDesc"].ToString();
                //}
                //if (Ds.Tables[2].Rows.Count > 1)
                //{
                //    obj_Comman.ShowPopUpMsg("For This Particular,\nIndent Can Be Made Using Following Description -\n" + UNIT, this.Page);
                //}
            }
            if (Ds.Tables[3].Rows.Count > 0)
            {
                //ViewState["UnitConversnList"] = Ds.Tables[3];
                //((AjaxControlToolkit.ComboBox)grditem.Rows[currrow].FindControl("ddlUnitConvertor")).DataSource = Ds.Tables[3];
                //((AjaxControlToolkit.ComboBox)grditem.Rows[currrow].FindControl("ddlUnitConvertor")).DataTextField = "UnitFactor";
                //((AjaxControlToolkit.ComboBox)grditem.Rows[currrow].FindControl("ddlUnitConvertor")).DataValueField = "#";
                //((AjaxControlToolkit.ComboBox)grditem.Rows[currrow].FindControl("ddlUnitConvertor")).DataBind();
                //if (Ds.Tables[2].Rows.Count <= 1)
                //{
                //    if (Ds.Tables[3].Rows.Count > 1)
                //    {
                //        string UNIT1 = string.Empty;
                //        for (int i = 0; i < Ds.Tables[3].Rows.Count; i++)
                //        {
                //            UNIT1 = UNIT1 + "\n, " + Ds.Tables[3].Rows[i]["UnitFactor"].ToString();
                //        }
                //        obj_Comman.ShowPopUpMsg("For This Particular,\nIndent Can Be Made Using Following UOM -\n" + UNIT1, this.Page);
                //    }
                //}


            }
            #endregion

            //}
            //else
            //{
            //    // SetInitialRow_GrdRequisition();
            //    obj_Comman.ShowPopUpMsg("Item Already Present!", this.Page);
            //}      
            if (Ds.Tables[2].Rows.Count > 1)
            {
                ((AjaxControlToolkit.ComboBox)grdwoodenbox.Rows[currrow].FindControl("ddlItemDescription")).Focus();
            }
            else
            {
                //  ((AjaxControlToolkit.ComboBox)grditem.Rows[currrow].FindControl("ddlUnitConvertor")).Focus();
            }

        }
        catch (Exception ex)
        {

        }
    }


    protected void TxtItemNamewooden_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = sender as TextBox;
            GridViewRow row = txt.NamingContainer as GridViewRow;
            int rowIndex = row.RowIndex;
            StrCondition = string.Empty;
            StrCondition = TxtItemNamewooden.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                TxtItemNamewooden.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                TxtItemNamewooden.ToolTip = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                ViewState["ddlItem"] = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                ((AjaxControlToolkit.ComboBox)grdwoodenbox.Rows[rowIndex].FindControl("ddlItemwoden")).SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();

                ddlItem_SelectedIndexChanged((((AjaxControlToolkit.ComboBox)grdwoodenbox.Rows[rowIndex].FindControl("ddlItemwoden"))) as AjaxControlToolkit.ComboBox, EventArgs.Empty);
                Ds = null;
            }
            else
            {
                TxtItemNamewooden.Text = "";
                TxtItemNamewooden.ToolTip = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void ddlItemwoden_OnSelectedIndexChanged_(object sender, EventArgs e)
    {
        try
        {
            AjaxControlToolkit.ComboBox ddlItemwoden = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd = (GridViewRow)ddlItemwoden.Parent.Parent;
            int currrow = grd.RowIndex;
            string a = ViewState["ddlItem"].ToString();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItemDataAccordingToID(Convert.ToInt32(a), out StrError);

            DataTable dt = new DataTable();
            dt = Ds.Tables[0];
            DataTable dt1 = new DataTable();
            dt1 = Ds.Tables[1];
            DataTable dt2 = new DataTable();
            dt2 = Ds.Tables[2];
            DataTable dt3 = new DataTable();
            dt3 = Ds.Tables[3];

            ViewState["ParticularItem"] = Ds.Tables[0];
            DataTable dtt = new DataTable();
            dtt = (DataTable)ViewState["ParticularItem"];
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {

            }
            else
            {

            }
            //  GetAvaliableQuantityForItem(currrow);


            #region[Bind ItemDescription and UnitConverSion]
            if (Ds.Tables[2].Rows.Count > 0)
            {

            }
            if (Ds.Tables[3].Rows.Count > 0)
            {



            }

            if (Ds.Tables[2].Rows.Count > 1)
            {
                //((AjaxControlToolkit.ComboBox)grdwoodenbox.Rows[currrow].FindControl("ddlItemwoden")).Focus();
            }
            else
            {

            }
            #endregion
        }
        catch (Exception ex)
        {

        }
    }
}