using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MayurInventory.DataModel;
using MayurInventory.Utility;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transactions_projectscope1 : System.Web.UI.Page
{
    DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
    RequisitionCafeteria Entity_RequisitionCafeteria = new RequisitionCafeteria();
    // RequisitionCafeteria Entity_RequisitionCafeteria = new RequisitionCafeteria();
    private string StrCondition = string.Empty;
    CommanFunction obj_Comman = new CommanFunction();
    DataSet Ds = new DataSet();

    int grandTotal = 0;
    private string StrError = string.Empty;
    database db = new database();
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        //  dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("systemname", typeof(string)));
        dt.Columns.Add(new DataColumn("systemno", typeof(string)));
        dt.Columns.Add(new DataColumn("valvetype", typeof(string)));
        dt.Columns.Add(new DataColumn("valvesize", typeof(string)));
        dt.Columns.Add(new DataColumn("valveclass", typeof(string)));
        dt.Columns.Add(new DataColumn("valveoperator", typeof(string)));
        dt.Columns.Add(new DataColumn("valvetagno", typeof(string)));
        dt.Columns.Add(new DataColumn("interlock", typeof(string)));
        dt.Columns.Add(new DataColumn("handwheel", typeof(string)));
        dt.Columns.Add(new DataColumn("lever", typeof(string)));
        dt.Columns.Add(new DataColumn("qty", typeof(string)));
        dt.Columns.Add(new DataColumn("unitprice", typeof(string)));
        dt.Columns.Add(new DataColumn("totalprice", typeof(string)));
        dt.Columns.Add(new DataColumn("Keys", typeof(string)));
        dt.Columns.Add(new DataColumn("Currency", typeof(string)));

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["systemname"] = string.Empty;
        dr["systemno"] = string.Empty;
        dr["valvetype"] = string.Empty;
        dr["valvesize"] = string.Empty;
        dr["valveclass"] = string.Empty;
        dr["valveoperator"] = string.Empty;
        dr["valvetagno"] = string.Empty;
        dr["interlock"] = string.Empty;
        dr["handwheel"] = string.Empty;
        dr["lever"] = string.Empty;
        dr["qty"] = string.Empty;
        dr["unitprice"] = 1;
        dr["totalprice"] = 1;
        dr["Keys"] = string.Empty;
        dr["Currency"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        Gridview1.DataSource = dt;
        Gridview1.DataBind();
    }


    public void SetInitialRow_Grditem()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("qty", typeof(string));
            dt.Columns.Add("unitprice", typeof(string));
            dt.Columns.Add("total", typeof(string));
            dt.Columns.Add("Currency", typeof(string));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;

            dr["ItemName"] = "";
            dr["qty"] = "";
            dr["unitprice"] = "";
            dr["total"] = "";
            dr["Currency"] = "";
            dt.Rows.Add(dr);
            ViewState["CurrentTable1"] = dt;
            grditem.DataSource = dt;
            grditem.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }




    public void SetInitialRow_Grdpd()
    {
        try
        {


            DataTable dt = new DataTable();
            DataRow dr = null;

            //  dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("pd", typeof(string)));
            dt.Columns.Add(new DataColumn("uprice", typeof(string)));
            dt.Columns.Add(new DataColumn("Details", typeof(string)));
            dt.Columns.Add(new DataColumn("Currency", typeof(string)));



            dr = dt.NewRow();

            dr["pd"] = "";
            dr["uprice"] = "";
            dr["Details"] = "";
            dr["Currency"] = "";


            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["CurrentTablepd"] = dt;

            grdpd.DataSource = dt;
            grdpd.DataBind();


        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }



    public void SetInitialRow_Grrdsummery()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            //  dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("valvetype", typeof(string)));
            dt.Columns.Add(new DataColumn("valvesize", typeof(string)));

            dt.Columns.Add(new DataColumn("qty", typeof(string)));

            dr = dt.NewRow();

            dr["valvetype"] = "";
            dr["valvesize"] = "";

            dr["qty"] = string.Empty;

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["CurrentTablesum"] = dt;

            Grisummery.DataSource = dt;
            Grisummery.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }



    private void AddNewRowToGriditem()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values

                    // AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");

                    TextBox txtqty = (TextBox)grditem.Rows[rowIndex].Cells[1].FindControl("txtqty");
                    DropDownList TxtItemName = (DropDownList)grditem.Rows[rowIndex].Cells[0].FindControl("ddlitem");
                    TextBox txtunitprice = (TextBox)grditem.Rows[rowIndex].Cells[2].FindControl("txtunitprice");
                    TextBox txttotalprice = (TextBox)grditem.Rows[rowIndex].Cells[3].FindControl("txttotalpriceitem1");
                    DropDownList drpCurrency = (DropDownList)grditem.Rows[rowIndex].Cells[3].FindControl("drpCurrency");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    dtCurrentTable.Rows[i - 1]["ItemName"] = TxtItemName.Text;
                    dtCurrentTable.Rows[i - 1]["qty"] = txtqty.Text;
                    dtCurrentTable.Rows[i - 1]["unitprice"] = txtunitprice.Text;
                    dtCurrentTable.Rows[i - 1]["total"] = txttotalprice.Text;
                    dtCurrentTable.Rows[i - 1]["Currency"] = drpCurrency.Text;


                    rowIndex++;
                }










                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable1"] = dtCurrentTable;

                grditem.DataSource = dtCurrentTable;
                grditem.DataBind();

                if (hdn1.Value.ToString() != "")
                {
                    ((Label)Gridview1.FooterRow.FindControl("lblGrandTotal1")).Text = hdn1.Value;
                }
                if (hdn2.Value.ToString() != "")
                {
                    ((Label)grditem.FooterRow.FindControl("lblGrandTotalitem1")).Text = hdn2.Value;
                }
                if (hdn3.Value.ToString() != "")
                {
                    ((Label)grdpd.FooterRow.FindControl("lblpdprice")).Text = hdn3.Value;
                }




            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataitem();
    }





    private void AddNewRowToGridpd()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTablepd"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTablepd"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values

                    // AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    DropDownList TxtItemName = (DropDownList)grdpd.Rows[rowIndex].Cells[0].FindControl("drppd");
                    TextBox txtunitprice = (TextBox)grdpd.Rows[rowIndex].Cells[1].FindControl("txtpdprice");
                    TextBox txtpdDetails = (TextBox)grdpd.Rows[rowIndex].Cells[1].FindControl("txtpdDetails");
                    DropDownList drppdCurrency = (DropDownList)grdpd.Rows[rowIndex].Cells[1].FindControl("drppdCurrency");


                    drCurrentRow = dtCurrentTable.NewRow();



                    dtCurrentTable.Rows[i - 1]["pd"] = TxtItemName.Text;
                    dtCurrentTable.Rows[i - 1]["uprice"] = txtunitprice.Text;
                    dtCurrentTable.Rows[i - 1]["Details"] = txtpdDetails.Text;
                    dtCurrentTable.Rows[i - 1]["Currency"] = drppdCurrency.Text;



                    rowIndex++;
                }










                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTablepd"] = dtCurrentTable;

                grdpd.DataSource = dtCurrentTable;
                grdpd.DataBind();

                if (hdn1.Value.ToString() != "")
                {
                    ((Label)Gridview1.FooterRow.FindControl("lblGrandTotal1")).Text = hdn1.Value;
                }
                if (hdn2.Value.ToString() != "")
                {
                    ((Label)grditem.FooterRow.FindControl("lblGrandTotalitem1")).Text = hdn2.Value;
                }
                if (hdn3.Value.ToString() != "")
                {
                    ((Label)grdpd.FooterRow.FindControl("lblpdprice")).Text = hdn3.Value;
                }




            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDatapd();
    }



    private void AddNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values

                    DropDownList drpsystemname = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("drpsystemname");
                    TextBox txtsystem = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtsystem");
                    DropDownList drpvalvetype = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("drpvalvetype");
                    DropDownList drpvalvesize = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalvesize");
                    DropDownList drpvalveclass = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalveclass");
                    DropDownList drpvalveoperator = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalveoperator");
                    TextBox txtvalvetagno = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtvalvetagno");
                    DropDownList drpiterlock = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpiterlock");
                    DropDownList drphandwheelsize = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drphandwheelsize");
                    DropDownList drplever = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drplever");
                    TextBox txtqty = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtqty");
                    TextBox txtunitprice = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtunitprice");
                    TextBox txttotalprice = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txttotalprice1");
                    TextBox txtKeys = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtKeys");
                    DropDownList drpCurrency = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpCurrency");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    dtCurrentTable.Rows[i - 1]["systemname"] = drpsystemname.Text;
                    dtCurrentTable.Rows[i - 1]["systemno"] = txtsystem.Text;
                    dtCurrentTable.Rows[i - 1]["valvetype"] = drpvalvetype.Text;
                    dtCurrentTable.Rows[i - 1]["valvesize"] = drpvalvesize.Text;
                    dtCurrentTable.Rows[i - 1]["valveclass"] = drpvalveclass.Text;
                    dtCurrentTable.Rows[i - 1]["valveoperator"] = drpvalveoperator.Text;
                    dtCurrentTable.Rows[i - 1]["valvetagno"] = txtvalvetagno.Text;
                    dtCurrentTable.Rows[i - 1]["interlock"] = drpiterlock.Text;
                    dtCurrentTable.Rows[i - 1]["handwheel"] = drphandwheelsize.Text;
                    dtCurrentTable.Rows[i - 1]["lever"] = drplever.Text;
                    dtCurrentTable.Rows[i - 1]["qty"] = txtqty.Text;
                    dtCurrentTable.Rows[i - 1]["unitprice"] = txtunitprice.Text;
                    dtCurrentTable.Rows[i - 1]["totalprice"] = txttotalprice.Text;
                    dtCurrentTable.Rows[i - 1]["Keys"] = txtKeys.Text;
                    dtCurrentTable.Rows[i - 1]["Currency"] = drpCurrency.Text;

                    rowIndex++;
                }










                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();

                if (hdn1.Value.ToString() != "")
                {
                    ((Label)Gridview1.FooterRow.FindControl("lblGrandTotal1")).Text = hdn1.Value;
                }
                if (hdn2.Value.ToString() != "")
                {
                    ((Label)grditem.FooterRow.FindControl("lblGrandTotalitem1")).Text = hdn2.Value;
                }
                if (hdn3.Value.ToString() != "")
                {
                    ((Label)grdpd.FooterRow.FindControl("lblpdprice")).Text = hdn3.Value;
                }

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }



    private void AddNewRowToGridsummery()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable1.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable1.Rows.Count; i++)
                {
                    //extract the TextBox values

                    DropDownList drpvalvetype = (DropDownList)Grisummery.Rows[rowIndex].Cells[0].FindControl("drpvalvetype");
                    DropDownList drpvalvesize = (DropDownList)Grisummery.Rows[rowIndex].Cells[1].FindControl("drpvalvesize");

                    TextBox txtqty = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtqty");


                    drCurrentRow = dtCurrentTable1.NewRow();


                    dtCurrentTable1.Rows[i - 1]["valvetype"] = drpvalvetype.Text;
                    dtCurrentTable1.Rows[i - 1]["valvesize"] = drpvalvesize.Text;

                    dtCurrentTable1.Rows[i - 1]["qty"] = txtqty.Text;


                    rowIndex++;
                }










                dtCurrentTable1.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable1;

                Grisummery.DataSource = dtCurrentTable1;
                Grisummery.DataBind();






            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDatasummery();
    }




    private void AddNewRowToGridsameabove()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values

                    DropDownList drpsystemname = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("drpsystemname");
                    TextBox txtsystem = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtsystem");
                    DropDownList drpvalvetype = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("drpvalvetype");
                    DropDownList drpvalvesize = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalvesize");
                    DropDownList drpvalveclass = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalveclass");
                    DropDownList drpvalveoperator = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalveoperator");
                    TextBox txtvalvetagno = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtvalvetagno");
                    DropDownList drpiterlock = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpiterlock");
                    DropDownList drphandwheelsize = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drphandwheelsize");
                    DropDownList drplever = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drplever");
                    TextBox txtqty = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtqty");
                    TextBox txtunitprice = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtunitprice");
                    TextBox txttotalprice = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txttotalprice1");
                    TextBox txtKeys = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtKeys");
                    DropDownList drpCurrency = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpCurrency");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    dtCurrentTable.Rows[i - 1]["systemname"] = drpsystemname.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["systemno"] = txtsystem.Text;
                    dtCurrentTable.Rows[i - 1]["valvetype"] = drpvalvetype.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["valvesize"] = drpvalvesize.Text;
                    dtCurrentTable.Rows[i - 1]["valveclass"] = drpvalveclass.Text;
                    dtCurrentTable.Rows[i - 1]["valveoperator"] = drpvalveoperator.Text;
                    dtCurrentTable.Rows[i - 1]["valvetagno"] = txtvalvetagno.Text;
                    dtCurrentTable.Rows[i - 1]["interlock"] = drpiterlock.Text;
                    dtCurrentTable.Rows[i - 1]["handwheel"] = drphandwheelsize.Text;
                    dtCurrentTable.Rows[i - 1]["lever"] = drplever.Text;
                    dtCurrentTable.Rows[i - 1]["qty"] = 1;
                    dtCurrentTable.Rows[i - 1]["unitprice"] = txtunitprice.Text;
                    dtCurrentTable.Rows[i - 1]["totalprice"] = txttotalprice.Text;
                    dtCurrentTable.Rows[i - 1]["Keys"] = txtKeys.Text;
                    dtCurrentTable.Rows[i - 1]["Currency"] = drpCurrency.Text;

                    rowIndex++;



                }










                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();


                if (hdn1.Value.ToString() != "")
                {
                    ((Label)Gridview1.FooterRow.FindControl("lblGrandTotal1")).Text = hdn1.Value;
                }

                if (hdn2.Value.ToString() != "")
                {
                    ((Label)grditem.FooterRow.FindControl("lblGrandTotalitem1")).Text = hdn2.Value;
                }
                if (hdn3.Value.ToString() != "")
                {
                    ((Label)grdpd.FooterRow.FindControl("lblpdprice")).Text = hdn3.Value;
                }



            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData1();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DropDownList drpsystemname = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("drpsystemname");
                    TextBox txtsystem = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtsystem");
                    DropDownList drpvalvetype = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("drpvalvetype");
                    DropDownList drpvalvesize = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalvesize");
                    DropDownList drpvalveclass = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalveclass");
                    DropDownList drpvalveoperator = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalveoperator");
                    TextBox txtvalvetagno = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtvalvetagno");
                    DropDownList drpiterlock = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpiterlock");
                    DropDownList drphandwheelsize = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drphandwheelsize");
                    DropDownList drplever = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drplever");
                    TextBox txtqty = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtqty");
                    TextBox txtunitprice = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtunitprice");
                    TextBox txttotalprice = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txttotalprice1");
                    TextBox txtKeys = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtKeys");
                    DropDownList drpCurrency = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpCurrency");


                    drpsystemname.Text = dt.Rows[i]["systemname"].ToString();
                    txtsystem.Text = dt.Rows[i]["systemno"].ToString();
                    drpvalvetype.Text = dt.Rows[i]["valvetype"].ToString();
                    drpvalvesize.Text = dt.Rows[i]["valvesize"].ToString();
                    drpvalveclass.Text = dt.Rows[i]["valveclass"].ToString();
                    drpvalveoperator.Text = dt.Rows[i]["valveoperator"].ToString();
                    txtvalvetagno.Text = dt.Rows[i]["valvetagno"].ToString();
                    drpiterlock.Text = dt.Rows[i]["interlock"].ToString();
                    drphandwheelsize.Text = dt.Rows[i]["handwheel"].ToString();
                    drplever.Text = dt.Rows[i]["lever"].ToString();
                    txtqty.Text = dt.Rows[i]["qty"].ToString();
                    txtunitprice.Text = dt.Rows[i]["unitprice"].ToString();
                    txttotalprice.Text = dt.Rows[i]["totalprice"].ToString();
                    txtKeys.Text = dt.Rows[i]["Keys"].ToString();
                    drpCurrency.Text = dt.Rows[i]["Currency"].ToString();

                    rowIndex++;

                }
            }
        }
    }



    private void SetPreviousDatasummery()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DropDownList drpvalvetype = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("drpvalvetype");
                    DropDownList drpvalvesize = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalvesize");


                    TextBox txtqty = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtqty");

                    string a = dt.Rows[i]["valvetype"].ToString();

                    drpvalvetype.Text = dt.Rows[i]["valvetype"].ToString();
                    drpvalvesize.Text = dt.Rows[i]["valvesize"].ToString();

                    txtqty.Text = dt.Rows[i]["qty"].ToString();

                    rowIndex++;

                }
            }
        }
    }



    private void SetPreviousData1()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                int j = dt.Rows.Count - 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    DropDownList drpsystemname = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("drpsystemname");
                    TextBox txtsystem = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtsystem");
                    DropDownList drpvalvetype = (DropDownList)Gridview1.Rows[rowIndex].Cells[2].FindControl("drpvalvetype");
                    DropDownList drpvalvesize = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalvesize");
                    DropDownList drpvalveclass = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalveclass");
                    DropDownList drpvalveoperator = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpvalveoperator");
                    TextBox txtvalvetagno = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtvalvetagno");
                    DropDownList drpiterlock = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpiterlock");
                    DropDownList drphandwheelsize = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drphandwheelsize");
                    DropDownList drplever = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drplever");
                    TextBox txtqty = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtqty");
                    TextBox txtunitprice = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtunitprice");
                    TextBox txttotalprice = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txttotalprice1");
                    TextBox txtKeys = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtKeys");
                    DropDownList drpCurrency = (DropDownList)Gridview1.Rows[rowIndex].Cells[3].FindControl("drpCurrency");

                    if (i != j)
                    {
                        drpsystemname.Text = dt.Rows[i]["systemname"].ToString();
                        txtsystem.Text = dt.Rows[i]["systemno"].ToString();
                        drpvalvetype.Text = dt.Rows[i]["valvetype"].ToString();
                        drpvalvesize.Text = dt.Rows[i]["valvesize"].ToString();
                        drpvalveclass.Text = dt.Rows[i]["valveclass"].ToString();
                        drpvalveoperator.Text = dt.Rows[i]["valveoperator"].ToString();
                        txtvalvetagno.Text = dt.Rows[i]["valvetagno"].ToString();
                        drpiterlock.Text = dt.Rows[i]["interlock"].ToString();
                        drphandwheelsize.Text = dt.Rows[i]["handwheel"].ToString();
                        drplever.Text = dt.Rows[i]["lever"].ToString();
                        txtqty.Text = dt.Rows[i]["qty"].ToString();
                        txtunitprice.Text = dt.Rows[i]["unitprice"].ToString();
                        txttotalprice.Text = dt.Rows[i]["totalprice"].ToString();
                        txtKeys.Text = dt.Rows[i]["Keys"].ToString();
                        drpCurrency.Text = dt.Rows[i]["Currency"].ToString();
                    }
                    else
                    {
                        drpsystemname.Text = dt.Rows[j - 1]["systemname"].ToString();
                        txtsystem.Text = dt.Rows[j - 1]["systemno"].ToString();
                        drpvalvetype.Text = dt.Rows[j - 1]["valvetype"].ToString();
                        drpvalvesize.Text = dt.Rows[j - 1]["valvesize"].ToString();
                        drpvalveclass.Text = dt.Rows[j - 1]["valveclass"].ToString();
                        drpvalveoperator.Text = dt.Rows[j - 1]["valveoperator"].ToString();
                        txtvalvetagno.Text = "";
                        drpiterlock.Text = dt.Rows[j - 1]["interlock"].ToString();
                        drphandwheelsize.Text = dt.Rows[j - 1]["handwheel"].ToString();
                        drplever.Text = dt.Rows[j - 1]["lever"].ToString();
                        txtqty.Text = dt.Rows[j - 1]["qty"].ToString();
                        txtunitprice.Text = dt.Rows[j - 1]["unitprice"].ToString();
                        txttotalprice.Text = dt.Rows[j - 1]["totalprice"].ToString();
                        txtKeys.Text = dt.Rows[j - 1]["Keys"].ToString();
                        drpCurrency.Text = dt.Rows[j - 1]["Currency"].ToString();
                    }
                    rowIndex++;

                }
            }
        }
    }


    private void SetPreviousDataitem()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //   AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");

                    TextBox txtqty = (TextBox)grditem.Rows[rowIndex].Cells[1].FindControl("txtqty");
                    DropDownList TxtItemName = (DropDownList)grditem.Rows[rowIndex].Cells[0].FindControl("ddlitem");
                    TextBox txtunitprice = (TextBox)grditem.Rows[rowIndex].Cells[2].FindControl("txtunitprice");
                    TextBox txttotalprice = (TextBox)grditem.Rows[rowIndex].Cells[3].FindControl("txttotalpriceitem1");
                    DropDownList drpCurrency = (DropDownList)grditem.Rows[rowIndex].Cells[3].FindControl("drpCurrency");


                    TxtItemName.Text = dt.Rows[i]["ItemName"].ToString();
                    txtqty.Text = dt.Rows[i]["qty"].ToString();
                    txtunitprice.Text = dt.Rows[i]["unitprice"].ToString();
                    txttotalprice.Text = dt.Rows[i]["total"].ToString();
                    drpCurrency.Text = dt.Rows[i]["Currency"].ToString();


                    rowIndex++;

                }
            }
        }
    }





    private void SetPreviousDatapd()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTablepd"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTablepd"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //   AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    DropDownList TxtItemName = (DropDownList)grdpd.Rows[rowIndex].Cells[0].FindControl("drppd");
                    TextBox txtunitprice = (TextBox)grdpd.Rows[rowIndex].Cells[1].FindControl("txtpdprice");
                    TextBox txtpdDetails = (TextBox)grdpd.Rows[rowIndex].Cells[1].FindControl("txtpdDetails");
                    DropDownList drppdCurrency = (DropDownList)grdpd.Rows[rowIndex].Cells[1].FindControl("drppdCurrency");



                    TxtItemName.Text = dt.Rows[i]["pd"].ToString();

                    txtunitprice.Text = dt.Rows[i]["uprice"].ToString();
                    txtpdDetails.Text = dt.Rows[i]["Details"].ToString();
                    drppdCurrency.Text = dt.Rows[i]["Currency"].ToString();

                    rowIndex++;

                }
            }
        }
    }





    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {


            SetInitialRow();
            SetInitialRow_Grditem();
            bindreportgrid();
            SetInitialRow_Grrdsummery();

            BtnUpdate.Visible = false;

            ddlCostCentre.DataSource = db.Displaygrid("select id  , ProjectName  from  Project_master");
            ddlCostCentre.DataValueField = "id";
            ddlCostCentre.DataTextField = "ProjectName";
            ddlCostCentre.DataBind();
            ddlCostCentre.Items.Insert(0, "Select project");
            txtReqDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            SetInitialRow_Grdpd();

        }
        else
        {

        }

    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);
    }

    protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList drpvalvetype = e.Row.FindControl("drpvalvetype") as DropDownList;


            drpvalvetype.DataSource = db.Displaygrid("Select * from valvetype");

            drpvalvetype.DataValueField = "valve";
            drpvalvetype.DataTextField = "valve";
            drpvalvetype.DataBind();
            drpvalvetype.Items.Insert(0, "Select Valve type");

            DropDownList drpsystemname = e.Row.FindControl("drpsystemname") as DropDownList;


            drpsystemname.DataSource = db.Displaygrid("Select * from SystemName");

            drpsystemname.DataValueField = "SystemName";
            drpsystemname.DataTextField = "SystemName";
            drpsystemname.DataBind();
            drpsystemname.Items.Insert(0, "Select SystemName");



            DropDownList drpvalvesize = e.Row.FindControl("drpvalvesize") as DropDownList;


            drpvalvesize.DataSource = db.Displaygrid("Select * from valvesize");

            drpvalvesize.DataValueField = "valvesize";
            drpvalvesize.DataTextField = "valvesize";
            drpvalvesize.DataBind();
            drpvalvesize.Items.Insert(0, "Select Valve Size");



            DropDownList drpvalveclass = e.Row.FindControl("drpvalveclass") as DropDownList;


            drpvalveclass.DataSource = db.Displaygrid("Select * from valveclass");

            drpvalveclass.DataValueField = "valveclass";
            drpvalveclass.DataTextField = "valveclass";
            drpvalveclass.DataBind();
            drpvalveclass.Items.Insert(0, "Select Valve Class");








            DropDownList drpvalveoperator = e.Row.FindControl("drpvalveoperator") as DropDownList;


            drpvalveoperator.DataSource = db.Displaygrid("Select * from valveoperator");

            drpvalveoperator.DataValueField = "valveoperator";
            drpvalveoperator.DataTextField = "valveoperator";
            drpvalveoperator.DataBind();
            drpvalveoperator.Items.Insert(0, "Select Valve operator");

            DropDownList drpiterlock = e.Row.FindControl("drpiterlock") as DropDownList;


            drpiterlock.DataSource = db.Displaygrid("Select TemplateName, TemplateID   from TemplateMaster Where IsDeleted=0");

            drpiterlock.DataValueField = "TemplateName";
            drpiterlock.DataTextField = "TemplateName";
            drpiterlock.DataBind();
            drpiterlock.Items.Insert(0, "Select  Interlock");

            DropDownList drphandwheelsize = e.Row.FindControl("drphandwheelsize") as DropDownList;





            drphandwheelsize.DataSource = db.Displaygrid("Select * from handwheel");

            drphandwheelsize.DataValueField = "handwheel";
            drphandwheelsize.DataTextField = "handwheel";
            drphandwheelsize.DataBind();
            drphandwheelsize.Items.Insert(0, "Select handwheel");

            DropDownList drplever = e.Row.FindControl("drplever") as DropDownList;


            drplever.DataSource = db.Displaygrid("Select * from lever");

            drplever.DataValueField = "lever";
            drplever.DataTextField = "lever";
            drplever.DataBind();
            drplever.Items.Insert(0, "Select Lever");




            DropDownList drpCurrency = e.Row.FindControl("drpCurrency") as DropDownList;


            drpCurrency.DataSource = db.Displaygrid("Select * from CurrencyMaster");

            drpCurrency.DataValueField = "Currency";
            drpCurrency.DataTextField = "Currency";
            drpCurrency.DataBind();
            drpCurrency.Items.Insert(0, "Select Currency");

            if (drpiterlock.SelectedItem.ToString() == "QTLS")
            {

                drphandwheelsize.Visible = false;
                drplever.Visible = true;
                drphandwheelsize.SelectedItem.Value = "1";

            }
            if (drpiterlock.SelectedItem.ToString() == "QTLL")
            {
                drplever.Visible = true;
                drphandwheelsize.Visible = false;
                drphandwheelsize.SelectedItem.Value = "1";
            }
            if (drpiterlock.SelectedItem.ToString() == "MTLL")
            {
                drphandwheelsize.Visible = true;
                drplever.Visible = false;
                drplever.SelectedItem.Value = "1";
            }
            if (drpiterlock.SelectedItem.ToString() == "MTLS")
            {
                drphandwheelsize.Visible = true;
                drplever.Visible = false;
                drplever.SelectedItem.Value = "1";
            }

            if (drpiterlock.SelectedItem.ToString() == "DCM")
            {
                drphandwheelsize.Visible = false;
                drplever.Visible = false;
                drplever.SelectedItem.Value = "1";
                drphandwheelsize.SelectedItem.Value = "1";
            }

            if (drpiterlock.SelectedItem.ToString() == "DCS")
            {
                drphandwheelsize.Visible = false;
                drplever.Visible = false;
                drplever.SelectedItem.Value = "1";
                drphandwheelsize.SelectedItem.Value = "1";
            }
        }

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {

        int z = 0;

        if (hdn4.Value.ToString() != "")
        {
            z = Convert.ToInt32(hdn4.Value);

        }
        else
        {
            z = 0;
        }

        if (ddlCostCentre.SelectedValue != null && ddlCostCentre.SelectedValue != "Select project")
        {
            db.insert("Insert into  Projectscope values('" + lblReqNo.Text + "' ,'" + txtReqDate.Text + "' ,'" + ddlCostCentre.SelectedValue + "' ,'" + "" + "' ,'" + TXTREMARK.Text + "' ,'" + "false" + "' ,'" + Session["UserName"].ToString() + "' ,'" + System.DateTime.Now.ToString("dd/MM/yyyy") + "' ,'" + z + "' ,'" + "1" + "' ,'" + "0" + "' ,'" + txtpurchaseordewrdate.Text + "')");

            string drphandwheelsize;
            string drplever;
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                string drpsystemname = (((DropDownList)Gridview1.Rows[i].FindControl("drpsystemname")).SelectedValue);
                string txtsystem = (((TextBox)Gridview1.Rows[i].FindControl("txtsystem")).Text);
                string drpvalvetype = (((DropDownList)Gridview1.Rows[i].FindControl("drpvalvetype")).SelectedValue);
                string drpvalvesize = (((DropDownList)Gridview1.Rows[i].FindControl("drpvalvesize")).SelectedValue);
                string drpvalveclass = (((DropDownList)Gridview1.Rows[i].FindControl("drpvalveclass")).SelectedValue);
                string drpvalveoperator = (((DropDownList)Gridview1.Rows[i].FindControl("drpvalveoperator")).SelectedValue);
                string txtvalvetagno = (((TextBox)Gridview1.Rows[i].FindControl("txtvalvetagno")).Text);
                string drpiterlock = (((DropDownList)Gridview1.Rows[i].FindControl("drpiterlock")).SelectedValue);
                DropDownList drp = ((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize"));
                if (drp.SelectedValue != null)
                {
                    drphandwheelsize = (((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).SelectedValue);
                }
                else
                {
                    drphandwheelsize = "";
                }

                DropDownList drp1 = ((DropDownList)Gridview1.Rows[i].FindControl("drplever"));
                if (drp1.SelectedValue != null)
                {
                    drplever = (((DropDownList)Gridview1.Rows[i].FindControl("drplever")).SelectedValue);
                }
                else
                {
                    drplever = "";
                }
                int txtqty = Convert.ToInt32(((TextBox)Gridview1.Rows[i].FindControl("txtqty")).Text);
                int txtunitprice = Convert.ToInt32(((TextBox)Gridview1.Rows[i].FindControl("txtunitprice")).Text);
                int txttotalprice = Convert.ToInt32(((TextBox)Gridview1.Rows[i].FindControl("txttotalprice1")).Text);

                //  Label lb1= Gridview1.FooterRow.FindControl("lblGrandTotal") as Label;

                // Label lblgtotal = (Label)Gridview1.FooterRow.FindControl("lblGrandTotal");
                //  hdn1.va
                int y = 0;

                if (hdn1.Value.ToString() != "")
                {
                    y = Convert.ToInt32(hdn1.Value);

                }
                else
                {
                    y = 0;
                }




                string txtKeys = (((TextBox)Gridview1.Rows[i].FindControl("txtKeys")).Text);
                string drpCurrency = (((DropDownList)Gridview1.Rows[i].FindControl("drpCurrency")).SelectedValue);
                db.insert("insert into projectscopedetails values('" + lblReqNo.Text + "' ,      '" + drpsystemname + "',  '" + txtsystem + "','" + drpvalvetype + "'    ,'" + drpvalvesize + "' ,'" + drpvalveclass + "','" + drpvalveoperator + "','" + txtvalvetagno + "','" + drpiterlock + "','" + drphandwheelsize + "','" + drplever + "','" + txtqty + "' ,'" + txtunitprice + "' ,'" + txttotalprice + "' ,'" + y + "' ,'" + txtKeys + "' ,'" + drpCurrency + "')");

            }

            for (int i = 0; i < grditem.Rows.Count; i++)
            {
                string itemname = (((DropDownList)grditem.Rows[i].FindControl("ddlitem")).SelectedValue);
                string txtqty = (((TextBox)grditem.Rows[i].FindControl("txtqty")).Text);
                string txtunitprice = (((TextBox)grditem.Rows[i].FindControl("txtunitprice")).Text);
                string txttotalprice = (((TextBox)grditem.Rows[i].FindControl("txttotalpriceitem1")).Text);
                string drpCurrency = (((DropDownList)grditem.Rows[i].FindControl("drpCurrency")).Text);
                int gtotal = 0;



                if (hdn2.Value.ToString() != "")
                {
                    gtotal = Convert.ToInt32(hdn2.Value);
                }
                else
                {
                    gtotal = 0;

                }
                if (itemname.ToString() != "")
                {

                    db.insert("Insert Into projectscopeitem values('" + lblReqNo.Text + "' ,'" + itemname + "' ,'" + txtqty + "' ,'" + txtunitprice + "' ,'" + txttotalprice + "' ,'" + gtotal + "','" + drpCurrency + "')");
                }
            }


            for (int i = 0; i < grdpd.Rows.Count; i++)
            {
                string pd = (((DropDownList)grdpd.Rows[i].FindControl("drppd")).SelectedValue);
                string pdunitp = (((TextBox)grdpd.Rows[i].FindControl("txtpdprice")).Text);
                string txtpdDetails = (((TextBox)grdpd.Rows[i].FindControl("txtpdDetails")).Text);
                string drppdCurrency = (((DropDownList)grdpd.Rows[i].FindControl("drppdCurrency")).Text);
                int gtotal = 0;
                if (hdn3.Value.ToString() != "")
                {
                    gtotal = Convert.ToInt32(hdn3.Value);
                }
                else
                {
                    gtotal = 0;

                }
                db.insert("insert into projectscope_pd  values ('" + lblReqNo.Text + "' ,'" + pd + "' ,'" + pdunitp + "' ,'" + gtotal + "' ,'" + txtpdDetails + "' ,'" + drppdCurrency + "')");

            }

            obj_Comman.ShowPopUpMsg("Record Save Successfully!", this.Page);
            SetInitialRow();
            SetInitialRow_Grditem();
            SetInitialRow_Grdpd();
            lblReqNo.Text = "";
            txtReqDate.Text = "";
            chkrevised.Checked = false;
            bindreportgrid();
        }
        else
        {
            obj_Comman.ShowPopUpMsg("Please Select project !", this.Page);
        }
    }
    public void bindreportgrid()
    {
        ReportGrid.DataSource = db.Displaygrid("Select distinct Projectscope.pno as # ,  Project_master.ProjectName as  project , Projectscope.pdate  from  Projectscope inner join Project_master on Project_master.id=Projectscope.project where  Projectscope.staus='1'");
        ReportGrid.DataBind();
    }


    protected void ReportGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        ViewState["EditID"] = (e.CommandArgument);

                        int PROJECT = Convert.ToInt32(db.getDb_Value("select project from  Projectscope where  pno='" + ViewState["EditID"] + "'   "));

                        int count = Convert.ToInt32(db.getDb_Value("select count( IsCostCentre) from  RequisitionCafeteria where IsCostCentre='" + PROJECT + "'  and  IsDeleted='" + "False" + "'"));
                        if (count == 0)
                        {


                            BtnSave.Visible = false;
                            BtnUpdate.Visible = true;


                            lblReqNo.Text = ViewState["EditID"].ToString();
                            ddlCostCentre.SelectedValue = db.getDb_Value("select  project from Projectscope   where pno='" + ViewState["EditID"] + "' ").ToString();
                            txtpurchaseordewrdate.Text = db.getDbstatus_Value("select  purchasedate from Projectscope   where pno='" + ViewState["EditID"] + "' ");
                            txtReqDate.Text = db.getDbstatus_Value("select  pdate from Projectscope   where pno='" + ViewState["EditID"] + "' ");
                            lblupdatedby.Text = db.getDbstatus_Value("select  updatedby  from Projectscope   where pno='" + ViewState["EditID"] + "' ");
                            grditem.DataSource = db.Displaygrid("select  Row_Number() Over ( Order By id )   as  RowNumber , item as  ItemName  ,qty , unitprice , total  , Currency from  projectscopeitem  where  pno='" + ViewState["EditID"] + "' ");
                            grditem.DataBind();


                            ViewState["CurrentTable1"] = db.Displaygrid("select  Row_Number() Over ( Order By id )   as  RowNumber , item as  ItemName  ,qty , unitprice , total ,Currency from  projectscopeitem  where  pno='" + ViewState["EditID"] + "' ");
                            DataTable dt1 = new DataTable();
                            Lblgggtotal.Text = Convert.ToInt32(db.getDb_Value("select total from   Projectscope where pno='" + ViewState["EditID"] + "'")).ToString();
                            dt1 = db.Displaygrid("select  item as  ItemName  ,qty , unitprice , total ,gtotal ,Currency from  projectscopeitem  where  pno='" + ViewState["EditID"] + "'");

                            for (int i = 0; i < grditem.Rows.Count; i++)
                            {
                                ((DropDownList)grditem.Rows[i].FindControl("ddlitem")).SelectedValue = dt1.Rows[i]["ItemName"].ToString();
                                ((DropDownList)grditem.Rows[i].FindControl("drpCurrency")).SelectedValue = dt1.Rows[i]["ItemName"].ToString();
                                ((Label)grditem.FooterRow.FindControl("lblGrandTotalitem1")).Text = dt1.Rows[i]["gtotal"].ToString();
                                hdn2.Value = dt1.Rows[i]["gtotal"].ToString();

                            }

                            Gridview1.DataSource = db.Displaygrid(" select  Row_Number() Over ( Order By id )   as  RowNumber , systemname, systemno, valvetype, valvesize, valveclass, valveoperator ,  valvetagno , interlock ,  handwheel, lever ,  qty,  unitprice, totalprice,Currency,Keys from projectscopedetails where  pno='" + ViewState["EditID"] + "'");

                            Gridview1.DataBind();

                            DataTable dt2 = new DataTable();
                            dt2 = db.Displaygrid("select    pd   ,uprice ,  gtotal  , Currency , Details from  projectscope_pd  where  pno='" + ViewState["EditID"] + "'");

                            grdpd.DataSource = db.Displaygrid("select *  from  projectscope_pd where pno='" + ViewState["EditID"] + "' ");
                            grdpd.DataBind();

                            ViewState["CurrentTablepd"] = db.Displaygrid("select *  from  projectscope_pd where pno='" + ViewState["EditID"] + "' ");

                            for (int i = 0; i < grdpd.Rows.Count; i++)
                            {
                                ((DropDownList)grdpd.Rows[i].FindControl("drppd")).SelectedValue = dt2.Rows[i]["pd"].ToString();
                                ((DropDownList)grdpd.Rows[i].FindControl("drppdCurrency")).SelectedValue = dt2.Rows[i]["Currency"].ToString();
                                ((Label)grdpd.FooterRow.FindControl("lblpdprice")).Text = dt2.Rows[i]["gtotal"].ToString();
                                hdn3.Value = dt2.Rows[i]["gtotal"].ToString();
                            }






                            ((Label)grdpd.FooterRow.FindControl("lblpdprice")).Text = db.getDb_Value("select  gtotal from  projectscope_pd where pno='" + ViewState["EditID"] + "' ").ToString();

                            ViewState["CurrentTable"] = db.Displaygrid(" select  Row_Number() Over ( Order By id )   as  RowNumber , systemname, systemno, valvetype, valvesize, valveclass, valveoperator ,  valvetagno , interlock ,  handwheel, lever ,  qty,  unitprice, totalprice ,Currency, Keys  from projectscopedetails where  pno='" + ViewState["EditID"] + "'");
                            DataTable dt = new DataTable();
                            dt = db.Displaygrid("select id as  RowNumber , systemname, systemno, valvetype, valvesize, valveclass, valveoperator ,  valvetagno , interlock ,  handwheel, lever ,  qty,  unitprice, totalprice ,total, Currency,Keys from projectscopedetails where  pno='" + ViewState["EditID"] + "'");

                            for (int i = 0; i < Gridview1.Rows.Count; i++)
                            {
                                ((DropDownList)Gridview1.Rows[i].FindControl("drpsystemname")).SelectedValue = dt.Rows[i]["systemname"].ToString();
                                ((DropDownList)Gridview1.Rows[i].FindControl("drpvalvetype")).SelectedValue = dt.Rows[i]["valvetype"].ToString();
                                ((DropDownList)Gridview1.Rows[i].FindControl("drpvalvesize")).SelectedValue = dt.Rows[i]["valvesize"].ToString();
                                ((DropDownList)Gridview1.Rows[i].FindControl("drpvalveclass")).SelectedValue = dt.Rows[i]["valveclass"].ToString();
                                ((DropDownList)Gridview1.Rows[i].FindControl("drpvalveoperator")).SelectedValue = dt.Rows[i]["valveoperator"].ToString();
                                ((DropDownList)Gridview1.Rows[i].FindControl("drpiterlock")).SelectedValue = dt.Rows[i]["interlock"].ToString();
                                ((DropDownList)Gridview1.Rows[i].FindControl("drpCurrency")).SelectedValue = dt.Rows[i]["Currency"].ToString();
                                ((Label)Gridview1.FooterRow.FindControl("lblGrandTotal1")).Text = dt.Rows[i]["total"].ToString();

                                //  hdn1.Value= dt.Rows[i]["total"].ToString();
                                if (dt.Rows[i]["interlock"].ToString() == "QTLS")
                                {
                                    ((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).Enabled = false;
                                    ((DropDownList)Gridview1.Rows[i].FindControl("drplever")).Enabled = true;

                                }
                                if (dt.Rows[i]["interlock"].ToString() == "QTLL")
                                {
                                    ((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).Enabled = false;
                                    ((DropDownList)Gridview1.Rows[i].FindControl("drplever")).Enabled = true;

                                }
                                if (dt.Rows[i]["interlock"].ToString() == "MTLL")
                                {
                                    ((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).Enabled = true;
                                    ((DropDownList)Gridview1.Rows[i].FindControl("drplever")).Enabled = false;

                                }
                                if (dt.Rows[i]["interlock"].ToString() == "MTLS")
                                {

                                    ((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).Enabled = true;
                                    ((DropDownList)Gridview1.Rows[i].FindControl("drplever")).Enabled = false;

                                }

                                if (dt.Rows[i]["interlock"].ToString() == "DCS")
                                {
                                    ((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).Enabled = false;
                                    ((DropDownList)Gridview1.Rows[i].FindControl("drplever")).Enabled = false;
                                }

                                if (dt.Rows[i]["interlock"].ToString() == "DCM")
                                {
                                    ((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).Enabled = false;
                                    ((DropDownList)Gridview1.Rows[i].FindControl("drplever")).Enabled = false;
                                }




                                    ((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).SelectedValue = dt.Rows[i]["handwheel"].ToString();
                                ((DropDownList)Gridview1.Rows[i].FindControl("drplever")).SelectedValue = dt.Rows[i]["lever"].ToString();
                            }

                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);
                        }
                        else
                        {
                            obj_Comman.ShowPopUpMsg("Please Delete Indent First!", this.Page);
                        }
                    }
                    break;
                case ("Delete"):
                    {


                        ViewState["EditID"] = (e.CommandArgument);


                        db.insert("delete projectscopedetails where  pno='" + ViewState["EditID"] + "' ");
                        db.insert("delete projectscopeitem where  pno='" + ViewState["EditID"] + "' ");
                        db.insert("delete Projectscope where  pno='" + ViewState["EditID"] + "' ");
                        bindreportgrid();
                        obj_Comman.ShowPopUpMsg("Record Deleted Successfully!", this.Page);

                    }
                    break;










                case ("RS"):
                    {


                        ViewState["EditID"] = (e.CommandArgument);

                        GridView2.DataSource = db.Displaygrid("select  Row_Number() Over ( Order By id )   as  SrNo , systemname+' '+systemno as 'Sysytem Name', valvetype as 'Valve Type', valvesize as 'Valve Size',  valveoperator  as 'Operator',  valvetagno  as 'Tag No', interlock  as Interlock,    qty as QTY,  unitprice as 'Price', totalprice as 'Total Price' from projectscopedetails where  pno='" + ViewState["EditID"] + "'");
                        GridView2.DataBind();
                        // GridView3.DataSource = db.Displaygrid("select  item as  ItemName  ,qty as QTY , unitprice  as Price , total  as Total from  projectscopeitem  where  pno='" + ViewState["EditID"] + "' ");
                        // GridView3.DataBind();
                        image1.ImageUrl = db.getDbstatus_Value("select  CLogo from  CompanyMaster where  CompanyId='" + "1" + "'");
                        lblSuppname.Text = ViewState["EditID"].ToString();
                        lblsuppadd.Text = db.getDbstatus_Value("select   purchasedate from   Projectscope where  pno='" + ViewState["EditID"] + "'");
                        lblsuppcontact.Text = db.getDbstatus_Value("select   updatedby from   Projectscope where  pno='" + ViewState["EditID"] + "'");
                        Label6.Text = db.getDbstatus_Value("select   updateddate from   Projectscope where  pno='" + ViewState["EditID"] + "'");
                        Label9.Text = db.getDbstatus_Value("select  CAddress from  CompanyMaster");
                        Label5.Text = db.getDbstatus_Value("select  CAddress from  CompanyMaster");
                        lbladdress.Text = db.getDbstatus_Value("select  CAddress from  CompanyMaster");
                        lbltel1.Text = db.getDbstatus_Value("select  PhoneNo from  CompanyMaster");
                        Label21.Text = db.getDbstatus_Value("select  PhoneNo from  CompanyMaster");
                        Label20.Text = db.getDbstatus_Value("select  PhoneNo from  CompanyMaster");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPanel1", "PrintPanel1();", true);

                    }
                    break;


                case ("Oldprint"):
                    {


                        ViewState["EditID"] = (e.CommandArgument);
                        int projectnumber = Convert.ToInt32(db.getDb_Value("select  project from  Projectscope where  pno='" + ViewState["EditID"] + "' "));


                        string lastpn = db.getDbstatus_Value("SELECT TOP 1 pno From (select Top 2 * from Projectscope where  project='" + projectnumber + "'  ORDER BY id DESC) x                      ORDER BY id");
                        GridView4.DataSource = db.Displaygrid("select  Row_Number() Over ( Order By id )   as  SrNo , systemname+' '+systemno as 'Sysytem Name', valvetype as 'Valve Type', valvesize as 'Valve Size',  valveoperator  as 'Operator',  valvetagno  as 'Tag No', interlock  as Interlock,    qty as QTY,  unitprice as 'Price', totalprice as 'Total Price' from projectscopedetails where  pno='" + lastpn + "'");
                        GridView4.DataBind();
                        //GridView5.DataSource = db.Displaygrid("select  item as  ItemName  ,qty as QTY , unitprice  as Price , total  as Total from  projectscopeitem  where  pno='" + lastpn + "' ");
                        //GridView5.DataBind();
                        image2.ImageUrl = db.getDbstatus_Value("select  CLogo from  CompanyMaster where  CompanyId='" + "1" + "'");
                        Label11.Text = lastpn.ToString();
                        Label12.Text = db.getDbstatus_Value("select   purchasedate from   Projectscope where  pno='" + lastpn + "'");
                        Label13.Text = db.getDbstatus_Value("select   updatedby from   Projectscope where  pno='" + lastpn + "'");
                        Label14.Text = db.getDbstatus_Value("select   pdate from   Projectscope where  pno='" + lastpn + "'");
                        Label9.Text = db.getDbstatus_Value("select  CAddress from  CompanyMaster");
                        Label5.Text = db.getDbstatus_Value("select  CAddress from  CompanyMaster");
                        lbladdress.Text = db.getDbstatus_Value("select  CAddress from  CompanyMaster");
                        lbltel1.Text = db.getDbstatus_Value("select  PhoneNo from  CompanyMaster");
                        Label21.Text = db.getDbstatus_Value("select  PhoneNo from  CompanyMaster");
                        Label20.Text = db.getDbstatus_Value("select  PhoneNo from  CompanyMaster");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPanel2", "PrintPanel2();", true);

                    }
                    break;


                case ("Print"):
                    {
                        ViewState["EditID"] = (e.CommandArgument);
                        gv1.DataSource = db.Displaygrid(" select  Row_Number() Over ( Order By id )   as  SrNo , systemname+' '+systemno as 'Sysytem Name', valvetype as 'Valve Type', valvesize as 'Valve Size',  valveoperator  as 'Operator',  valvetagno  as 'Tag No', interlock  as Interlock,    qty as QTY,  unitprice as 'Price', totalprice as 'Total Price' from projectscopedetails where  pno='" + ViewState["EditID"] + "'");

                        gv1.DataBind();
                        image.ImageUrl = db.getDbstatus_Value("select  CLogo from  CompanyMaster where  CompanyId='" + "1" + "'");

                        lbltotalgridivew1.Text = db.getDb_Value("select total from  projectscopedetails where  pno='" + ViewState["EditID"] + "'").ToString();
                        lbltotalgvoitem1.Text = db.getDb_Value("select gtotal from  projectscopeitem where  pno='" + ViewState["EditID"] + "'").ToString();
                        lblpdtotal.Text = db.getDb_Value("select gtotal from  projectscope_pd where  pno='" + ViewState["EditID"] + "'").ToString();


                        gv2.DataSource = db.Displaygrid("select  item as  ItemName  ,qty as QTY , unitprice  as Price , total  as Total from  projectscopeitem  where  pno='" + ViewState["EditID"] + "' ");
                        gv2.DataBind();

                        Label17.Text = Convert.ToInt32(db.getDb_Value("select total from   Projectscope where  pno='" + ViewState["EditID"] + "' ")).ToString();

                        lblenno.Text = ViewState["EditID"].ToString();
                        lbldate.Text = db.getDbstatus_Value("select   purchasedate from   Projectscope where  pno='" + ViewState["EditID"] + "'");
                        Label15.Text = db.getDbstatus_Value("select   updatedby from   Projectscope where  pno='" + ViewState["EditID"] + "'");
                        Label16.Text = db.getDbstatus_Value("select   updateddate from   Projectscope where  pno='" + ViewState["EditID"] + "'");
                        Label9.Text = db.getDbstatus_Value("select  CAddress from  CompanyMaster");
                        Label5.Text = db.getDbstatus_Value("select  CAddress from  CompanyMaster");
                        lbladdress.Text = db.getDbstatus_Value("select  CAddress from  CompanyMaster");
                        lbltel1.Text = db.getDbstatus_Value("select  PhoneNo from  CompanyMaster");
                        Label21.Text = db.getDbstatus_Value("select  PhoneNo from  CompanyMaster");
                        Label20.Text = db.getDbstatus_Value("select  PhoneNo from  CompanyMaster");

                        gv3.DataSource = db.Displaygrid(" select pd  as Charges ,uprice as Price from  projectscope_pd where pno='" + ViewState["EditID"] + "' ");
                        gv3.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPanel", "PrintPanel();", true);
                    }
                    break;
                case ("Printoffer"):
                    {

                        ViewState["EditID"] = (e.CommandArgument);
                        DataTable dt = db.Displaygrid("select interlock as Type , keys as Keys, valvetype as 'Valve Type' , valvesize as 'Valve Size', valveclass as 'Valve Class',   valveoperator as 'Valve Operation',         CASE      WHEN handwheel = 1 THEN '-'          ELSE handwheel END AS 'Handwheel Dia',    CASE      WHEN lever = 1 THEN '-'         ELSE lever END AS 'Lever Lenght',          valvetagno as 'Tag No',qty as 'Qty.', unitprice as 'Unit Price',totalprice as 'Total Price'  from  projectscopedetails where  pno='" + ViewState["EditID"] + "'   ");

                        grdoffer.DataSource = dt;
                        grdoffer.DataBind();

                        grdpddetailsoffer.DataSource = db.Displaygrid("select  pd as 'Charges',uprice as 'Price'   from  projectscope_pd where  pno='" + ViewState["EditID"] + "'  ");
                        grdpddetailsoffer.DataBind();


                        girdofferextra.DataSource = db.Displaygrid("select item as Particular , qty as Qty,unitprice as Price , Total  from projectscopeitem  where  pno='" + ViewState["EditID"] + "' ");
                        girdofferextra.DataBind();
                        lbloffertotalab.Text = db.getDbstatus_Value("select   total  from Projectscope where pno='" + ViewState["EditID"] + "'");
                        lblofeeroptionaltotal.Text = db.getDbstatus_Value("select   gtotal  from projectscope_pd where pno='" + ViewState["EditID"] + "'");
                        lbloffergridview1total.Text = db.getDbstatus_Value("select total  from projectscopedetails where pno='" + ViewState["EditID"] + "'");
                        lblofferitemtotal.Text = db.getDbstatus_Value("select total  from projectscopeitem where pno='" + ViewState["EditID"] + "'");
                        image3.ImageUrl = db.getDbstatus_Value("select  CLogo from  CompanyMaster where  CompanyId='" + "1" + "'");

                        lblofferprojectno.Text = ViewState["EditID"].ToString();
                        lblofferpurchasedate.Text = db.getDbstatus_Value("select   purchasedate from   Projectscope where  pno='" + ViewState["EditID"] + "'");
                        lbloffercreateddaye.Text = db.getDbstatus_Value("select   updatedby from   Projectscope where  pno='" + ViewState["EditID"] + "'");
                        lbloffercreateddate.Text = db.getDbstatus_Value("select   updateddate from   Projectscope where  pno='" + ViewState["EditID"] + "'");
                        lbloffferrtelno.Text = db.getDbstatus_Value("select  PhoneNo from  CompanyMaster");
                        lblofferemail.Text = db.getDbstatus_Value("select  EmailId from  CompanyMaster");
                        lbloferrwebsite.Text = db.getDbstatus_Value("select  Website from  CompanyMaster");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPaneloffer", "PrintPaneloffer();", true);

                    }

                    break;
            }
        }

        catch (Exception ex)
        {

        }
    }

   

    private void BindGrid()
    {

        grdofferitem.DataSource = db.Displaygrid("select interlock as Type , keys as Keys, valvetype as 'Valve Type' , valvesize as 'Valve Size', valveclass as 'Valve Class',   valveoperator as 'Valve Operation',         CASE      WHEN handwheel = 1 THEN '-'          ELSE handwheel END AS 'Handwheel Dia',    CASE      WHEN lever = 1 THEN '-'         ELSE lever END AS 'Lever Lenght',          valvetagno as 'Tag No',qty as 'Qty.', unitprice as 'Unit Price',totalprice as 'Total Price'  from  projectscopedetails    ");
        grdofferitem.DataBind();

    }

    protected void ddlCostCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        string projectno = db.getDbstatus_Value("select projectno from Project_master where id='" + ddlCostCentre.SelectedValue + "' ");
        string ps = "PS";
        string year = "19-20";
        int unicno = Convert.ToInt32(db.getDb_Value("select max(id) from Projectscope where project= '" + ddlCostCentre.SelectedValue + "'"));
        if (unicno == 0)
        {
            unicno = 1;
        }
        else
        {
            unicno++;
        }
        string finalprojectno = ps + "- " + projectno + "/" + unicno;
        lblReqNo.Text = finalprojectno;


    }

    protected void ButtonAdd_Click1(object sender, EventArgs e)
    {
        AddNewRowToGriditem();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            //   db.insert("Insert into  Projectscope values('" + lblReqNo.Text + "' ,'" + txtReqDate.Text + "' ,'" + ddlCostCentre.SelectedValue + "' ,'" + txtsystemname.Text + "' ,'" + TXTREMARK.Text + "')");

            if (chkrevised.Checked == true)
            {
                string newrevisednumber = "";
                string str = lblReqNo.Text;



                int rv = Convert.ToInt32(db.getDb_Value("select  top 1  revised from Projectscope  where  project='" + ddlCostCentre.SelectedValue + "' order by id desc "));
                rv++;


                string[] split = str.Split('/');

                string str0 = split[0];
                string str1 = split[1];
                string str2 = split[2];
                string str3 = split[3];
                string str4 = split[4];

                newrevisednumber = str0 + " /" + str1 + "/" + str2 + "/" + str3 + "/" + str4 + "/" + "R" + "0" + rv.ToString();


                db.insert("update  Projectscope set staus='" + "0" + "' where  project ='" + ddlCostCentre.SelectedValue + "'");


                int z = 0;

                if (hdn4.Value.ToString() != "")
                {
                    z = Convert.ToInt32(hdn4.Value);

                }
                else
                {
                    z = 0;
                }
                db.insert("Insert into  Projectscope values('" + newrevisednumber + "' ,'" + txtReqDate.Text + "' ,'" + ddlCostCentre.SelectedValue + "' ,'" + "" + "' ,'" + TXTREMARK.Text + "' ,'" + "True" + "' ,'" + Session["UserName"].ToString() + "' ,'" + System.DateTime.Now.ToString("dd/MM/yyyy") + "' ,'" + z + "' ,'" + "1" + "' ,'" + rv + "' ,'" + txtpurchaseordewrdate.Text + "')");


                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    int gt = 0;

                    string drpsystemname = ((DropDownList)Gridview1.Rows[i].FindControl("drpsystemname")).SelectedValue;
                    string txtsystem = ((TextBox)Gridview1.Rows[i].FindControl("txtsystem")).Text;
                    string drpvalvetype = ((DropDownList)Gridview1.Rows[i].FindControl("drpvalvetype")).SelectedValue;
                    string drpvalvesize = ((DropDownList)Gridview1.Rows[i].FindControl("drpvalvesize")).SelectedValue;
                    string drpvalveclass = (((DropDownList)Gridview1.Rows[i].FindControl("drpvalveclass")).SelectedValue);
                    string drpvalveoperator = (((DropDownList)Gridview1.Rows[i].FindControl("drpvalveoperator")).SelectedValue);
                    string txtvalvetagno = (((TextBox)Gridview1.Rows[i].FindControl("txtvalvetagno")).Text);
                    string drpiterlock = (((DropDownList)Gridview1.Rows[i].FindControl("drpiterlock")).SelectedValue);
                    string drphandwheelsize = (((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).SelectedValue);
                    string drplever = (((DropDownList)Gridview1.Rows[i].FindControl("drplever")).SelectedValue);
                    string txtqty = (((TextBox)Gridview1.Rows[i].FindControl("txtqty")).Text);
                    string txtunitprice = (((TextBox)Gridview1.Rows[i].FindControl("txtunitprice")).Text);
                    string txttotalprice = (((TextBox)Gridview1.Rows[i].FindControl("txttotalprice1")).Text);
                    string txtKeys = (((TextBox)Gridview1.Rows[i].FindControl("txtKeys")).Text);
                    string drpCurrency = (((DropDownList)Gridview1.Rows[i].FindControl("drpCurrency")).Text);
                    if (hdn1.Value.ToString() != "")
                    {
                        gt = Convert.ToInt32(hdn1.Value);

                    }
                    else
                    {
                        gt = 0;
                    }

                    DropDownList drp = ((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize"));
                    if (drp.SelectedValue != null && drp.SelectedValue != "Select handwheel")
                    {
                        drphandwheelsize = (((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).SelectedValue);
                    }
                    else
                    {
                        drphandwheelsize = "";
                    }

                    DropDownList drp1 = ((DropDownList)Gridview1.Rows[i].FindControl("drplever"));
                    if (drp1.SelectedValue != null && drp1.SelectedValue != "Select Lever")
                    {
                        drplever = (((DropDownList)Gridview1.Rows[i].FindControl("drplever")).SelectedValue);
                    }
                    else
                    {
                        drplever = "";
                    }



                    db.insert("insert into projectscopedetails values('" + newrevisednumber + "' , '" + drpsystemname + "' ,    '" + txtsystem + "' ,'" + drpvalvetype + "'    ,'" + drpvalvesize + "' ,'" + drpvalveclass + "','" + drpvalveoperator + "','" + txtvalvetagno + "','" + drpiterlock + "','" + drphandwheelsize + "','" + drplever + "','" + txtqty + "' ,'" + txtunitprice + "' ,'" + txttotalprice + "' ,'" + gt + "' ,'" + txtKeys + "','" + drpCurrency + "')");

                }

                for (int i = 0; i < grditem.Rows.Count; i++)
                {
                    string itemname = (((DropDownList)grditem.Rows[i].FindControl("ddlitem")).Text);
                    string txtqty = (((TextBox)grditem.Rows[i].FindControl("txtqty")).Text);
                    string txtunitprice = (((TextBox)grditem.Rows[i].FindControl("txtunitprice")).Text);
                    string txttotalprice = (((TextBox)grditem.Rows[i].FindControl("txttotalpriceitem1")).Text);
                    string drpCurrency = (((DropDownList)grditem.Rows[i].FindControl("drpCurrency")).Text);
                    int gtotal = 0;
                    if (hdn2.Value.ToString() != "")
                    {
                        gtotal = Convert.ToInt32(hdn2.Value);
                    }
                    else
                    {
                        gtotal = 0;

                    }


                    if (itemname.ToString() != "")
                    {
                        db.insert("Insert Into projectscopeitem values('" + newrevisednumber + "' ,'" + itemname + "' ,'" + txtqty + "' ,'" + txtunitprice + "' ,'" + txttotalprice + "' ,'" + gtotal + "' ,'" + drpCurrency + "')");
                    }
                }

                for (int i = 0; i < grdpd.Rows.Count; i++)
                {
                    string pd = (((DropDownList)grdpd.Rows[i].FindControl("drppd")).SelectedValue);
                    string pdunitp = (((TextBox)grdpd.Rows[i].FindControl("txtpdprice")).Text);
                    string txtpdDetails = (((TextBox)grdpd.Rows[i].FindControl("txtpdDetails")).Text);
                    string drppdCurrency = (((DropDownList)grdpd.Rows[i].FindControl("drppdCurrency")).Text);

                    int gtotal = 0;
                    if (hdn3.Value.ToString() != "")
                    {
                        gtotal = Convert.ToInt32(hdn3.Value);
                    }
                    else
                    {
                        gtotal = 0;

                    }

                    db.insert("insert into projectscope_pd  values ('" + newrevisednumber + "' ,'" + pd + "' ,'" + pdunitp + "' ,'" + gtotal + "' ,'" + txtpdDetails + "' ,'" + drppdCurrency + "')");

                }
            }

            else
            {


                int z = 0;

                if (hdn4.Value.ToString() != "")
                {
                    z = Convert.ToInt32(hdn4.Value);

                }
                else
                {
                    z = 0;
                }


                db.insert("update   Projectscope set  pdate='" + txtReqDate.Text + "' ,  project= '" + ddlCostCentre.SelectedValue + "' , systemname='" + "" + "' , remark='" + TXTREMARK.Text + "'      ,isupdated='" + "True" + "' , updatedby='" + Session["UserName"].ToString() + "',  updateddate='" + System.DateTime.Now.ToString("dd/MM/yyyy") + "'    ,total='" + z + "' ,staus='" + "1" + "' , purchasedate='" + txtpurchaseordewrdate.Text + "'  where pno='" + lblReqNo.Text + "' ");

                db.insert("delete projectscopedetails where pno='" + lblReqNo.Text + "' ");
                db.insert("delete projectscopeitem where pno='" + lblReqNo.Text + "' ");
                db.insert("delete projectscope_pd where pno='" + lblReqNo.Text + "' ");



                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    int gt = 0;

                    string drpsystemname = ((DropDownList)Gridview1.Rows[i].FindControl("drpsystemname")).SelectedValue;
                    string txtsystem = ((TextBox)Gridview1.Rows[i].FindControl("txtsystem")).Text;
                    string drpvalvetype = ((DropDownList)Gridview1.Rows[i].FindControl("drpvalvetype")).SelectedValue;
                    string drpvalvesize = ((DropDownList)Gridview1.Rows[i].FindControl("drpvalvesize")).SelectedValue;
                    string drpvalveclass = (((DropDownList)Gridview1.Rows[i].FindControl("drpvalveclass")).SelectedValue);
                    string drpvalveoperator = (((DropDownList)Gridview1.Rows[i].FindControl("drpvalveoperator")).SelectedValue);
                    string txtvalvetagno = (((TextBox)Gridview1.Rows[i].FindControl("txtvalvetagno")).Text);
                    string drpiterlock = (((DropDownList)Gridview1.Rows[i].FindControl("drpiterlock")).SelectedValue);
                    string drphandwheelsize = (((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).SelectedValue);
                    string drplever = (((DropDownList)Gridview1.Rows[i].FindControl("drplever")).SelectedValue);
                    string txtqty = (((TextBox)Gridview1.Rows[i].FindControl("txtqty")).Text);
                    string txtunitprice = (((TextBox)Gridview1.Rows[i].FindControl("txtunitprice")).Text);
                    string txttotalprice = (((TextBox)Gridview1.Rows[i].FindControl("txttotalprice1")).Text);
                    string txtKeys = (((TextBox)Gridview1.Rows[i].FindControl("txtKeys")).Text);
                    string drpCurrency = (((DropDownList)Gridview1.Rows[i].FindControl("drpCurrency")).Text);
                    if (hdn1.Value.ToString() != "")
                    {
                        gt = Convert.ToInt32(hdn1.Value);

                    }
                    else
                    {
                        gt = 0;
                    }


                    DropDownList drp = ((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize"));
                    if (drp.SelectedValue != null && drp.SelectedValue!= "Select handwheel")
                    {
                        drphandwheelsize = (((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).SelectedValue);
                    }
                    else
                    {
                        drphandwheelsize = "";
                    }

                    DropDownList drp1 = ((DropDownList)Gridview1.Rows[i].FindControl("drplever"));
                    if (drp1.SelectedValue != null && drp1.SelectedValue != "Select Lever")
                    {
                        drplever = (((DropDownList)Gridview1.Rows[i].FindControl("drplever")).SelectedValue);
                    }
                    else
                    {
                        drplever = "";
                    }


                    db.insert("insert into projectscopedetails values('" + lblReqNo.Text + "' , '" + drpsystemname + "' ,    '" + txtsystem + "' ,'" + drpvalvetype + "'    ,'" + drpvalvesize + "' ,'" + drpvalveclass + "','" + drpvalveoperator + "','" + txtvalvetagno + "','" + drpiterlock + "','" + drphandwheelsize + "','" + drplever + "','" + txtqty + "' ,'" + txtunitprice + "' ,'" + txttotalprice + "' ,'" + gt + "','" + txtKeys + "','" + drpCurrency + "')");

                }

                for (int i = 0; i < grditem.Rows.Count; i++)
                {
                    string itemname = (((DropDownList)grditem.Rows[i].FindControl("ddlitem")).Text);
                    string txtqty = (((TextBox)grditem.Rows[i].FindControl("txtqty")).Text);
                    string txtunitprice = (((TextBox)grditem.Rows[i].FindControl("txtunitprice")).Text);
                    string txttotalprice = (((TextBox)grditem.Rows[i].FindControl("txttotalpriceitem1")).Text);
                    string drpCurrency = (((DropDownList)grditem.Rows[i].FindControl("drpCurrency")).Text);
                    int gtotal = 0;
                    if (hdn2.Value.ToString() != "")
                    {
                        gtotal = Convert.ToInt32(hdn2.Value);
                    }
                    else
                    {
                        gtotal = 0;

                    }


                    if (itemname.ToString() != "")
                    {
                        db.insert("Insert Into projectscopeitem values('" + lblReqNo.Text + "' ,'" + itemname + "' ,'" + txtqty + "' ,'" + txtunitprice + "' ,'" + txttotalprice + "' ,'" + gtotal + "' ,'" + drpCurrency + "')");
                    }
                }

                for (int i = 0; i < grdpd.Rows.Count; i++)
                {
                    string pd = (((DropDownList)grdpd.Rows[i].FindControl("drppd")).SelectedValue);
                    string pdunitp = (((TextBox)grdpd.Rows[i].FindControl("txtpdprice")).Text);
                    string txtpdDetails = (((TextBox)grdpd.Rows[i].FindControl("txtpdDetails")).Text);
                    string drppdCurrency = (((DropDownList)grdpd.Rows[i].FindControl("drppdCurrency")).Text);
                    int gtotal = 0;
                    if (hdn3.Value.ToString() != "")
                    {
                        gtotal = Convert.ToInt32(hdn3.Value);
                    }
                    else
                    {
                        gtotal = 0;

                    }

                    db.insert("insert into projectscope_pd  values ('" + lblReqNo.Text + "' ,'" + pd + "' ,'" + pdunitp + "' ,'" + gtotal + "' ,'" + txtpdDetails + "' ,'" + drppdCurrency + "')");

                }

            }

            obj_Comman.ShowPopUpMsg("Record Updated Successfully!", this.Page);
            //Gridview1.DataSource = null;
            //Gridview1.DataBind();
            //grditem.DataSource = null;
            //grditem.DataBind();
            SetInitialRow();
            SetInitialRow_Grdpd();
            SetInitialRow_Grditem();
            lblReqNo.Text = "";
            txtReqDate.Text = "";

            BtnSave.Visible = true;
            BtnUpdate.Visible = false;
            bindreportgrid();
        }
        catch (Exception ex)
        { }
        chkrevised.Checked = false;
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        SetInitialRow();
        SetInitialRow_Grditem();
        lblReqNo.Text = "";
        txtReqDate.Text = "";


        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
    }

    protected void drpiterlock_SelectedIndexChanged(object sender, EventArgs e)
    {


        for (int g = 0; g < Gridview1.Rows.Count; g++)
        {

            DropDownList drpiterlock = new DropDownList();
            DropDownList drplever = new DropDownList();

            DropDownList drphandwheelsize = new DropDownList();
            drpiterlock = (DropDownList)Gridview1.Rows[g].FindControl("drpiterlock");

            drplever = (DropDownList)Gridview1.Rows[g].FindControl("drplever");
            drphandwheelsize = (DropDownList)Gridview1.Rows[g].FindControl("drphandwheelsize");

            if (drpiterlock.SelectedItem.ToString() == "QTLS")
            {

                drphandwheelsize.Visible = false;
                drplever.Visible = true;
                drphandwheelsize.SelectedItem.Value = "1";

            }
            if (drpiterlock.SelectedItem.ToString() == "QTLL")
            {
                drplever.Visible = true;
                drphandwheelsize.Visible = false;
                drphandwheelsize.SelectedItem.Value = "1";
            }
            if (drpiterlock.SelectedItem.ToString() == "MTLL")
            {
                drphandwheelsize.Visible = true;
                drplever.Visible = false;
                drplever.SelectedItem.Value = "1";
            }
            if (drpiterlock.SelectedItem.ToString() == "MTLS")
            {
                drphandwheelsize.Visible = true;
                drplever.Visible = false;
                drplever.SelectedItem.Value = "1";
            }

            if (drpiterlock.SelectedItem.ToString() == "DCM")
            {
                drphandwheelsize.Visible = false;
                drplever.Visible = false;
                drplever.SelectedItem.Value = "1";
                drphandwheelsize.SelectedItem.Value = "1";
            }

            if (drpiterlock.SelectedItem.ToString() == "DCS")
            {
                drphandwheelsize.Visible = false;
                drplever.Visible = false;
                drplever.SelectedItem.Value = "1";
                drphandwheelsize.SelectedItem.Value = "1";
            }

        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        AddNewRowToGridsameabove();


        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);

        foreach (GridViewRow row in Gridview1.Rows)
        {



            DropDownList drpiterlock = row.FindControl("drpiterlock") as DropDownList;
            DropDownList drphandwheelsize = row.FindControl("drphandwheelsize") as DropDownList;
            DropDownList drplever = row.FindControl("drplever") as DropDownList;
            if (drpiterlock.SelectedItem.ToString() == "QTLS")
            {

                drphandwheelsize.Visible = false;
                drplever.Visible = true;
                drphandwheelsize.SelectedItem.Value = "1";

            }
            if (drpiterlock.SelectedItem.ToString() == "QTLL")
            {
                drplever.Visible = true;
                drphandwheelsize.Visible = false;
                drphandwheelsize.SelectedItem.Value = "1";
            }
            if (drpiterlock.SelectedItem.ToString() == "MTLL")
            {
                drphandwheelsize.Visible = true;
                drplever.Visible = false;
                drplever.SelectedItem.Value = "1";
            }
            if (drpiterlock.SelectedItem.ToString() == "MTLS")
            {
                drphandwheelsize.Visible = true;
                drplever.Visible = false;
                drplever.SelectedItem.Value = "1";
            }

            if (drpiterlock.SelectedItem.ToString() == "DCM")
            {
                drphandwheelsize.Visible = false;
                drplever.Visible = false;
                drplever.SelectedItem.Value = "1";
                drphandwheelsize.SelectedItem.Value = "1";
            }

            if (drpiterlock.SelectedItem.ToString() == "DCS")
            {
                drphandwheelsize.Visible = false;
                drplever.Visible = false;
                drplever.SelectedItem.Value = "1";
                drphandwheelsize.SelectedItem.Value = "1";
            }
        }
    }

    protected void Grisummery_DataBound(object sender, EventArgs e)
    {

    }

    protected void Grisummery_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnsummery_Click(object sender, EventArgs e)
    {
        db.insert("truncate table projectscopedetailsdummy ");
        string drphandwheelsize;
        string drplever;
        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
            string drpvalvetype = (((DropDownList)Gridview1.Rows[i].FindControl("drpvalvetype")).SelectedValue);
            string drpvalvesize = (((DropDownList)Gridview1.Rows[i].FindControl("drpvalvesize")).SelectedValue);
            string drpvalveclass = (((DropDownList)Gridview1.Rows[i].FindControl("drpvalveclass")).SelectedValue);
            string drpvalveoperator = (((DropDownList)Gridview1.Rows[i].FindControl("drpvalveoperator")).SelectedValue);
            string txtvalvetagno = (((TextBox)Gridview1.Rows[i].FindControl("txtvalvetagno")).Text);
            string drpiterlock = (((DropDownList)Gridview1.Rows[i].FindControl("drpiterlock")).SelectedValue);
            DropDownList drp = ((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize"));
            if (drp.SelectedValue != null)
            {
                drphandwheelsize = (((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).SelectedValue);
            }
            else
            {
                drphandwheelsize = "";
            }

            DropDownList drp1 = ((DropDownList)Gridview1.Rows[i].FindControl("drplever"));
            if (drp1.SelectedValue != null)
            {
                drplever = (((DropDownList)Gridview1.Rows[i].FindControl("drplever")).SelectedValue);
            }
            else
            {
                drplever = "";
            }
            int txtqty = Convert.ToInt32(((TextBox)Gridview1.Rows[i].FindControl("txtqty")).Text);
            int txtunitprice = Convert.ToInt32(((TextBox)Gridview1.Rows[i].FindControl("txtunitprice")).Text);
            int txttotalprice = Convert.ToInt32(((TextBox)Gridview1.Rows[i].FindControl("txttotalprice1")).Text);
            db.insert("insert into projectscopedetailsdummy values('" + lblReqNo.Text + "' ,'" + drpvalvetype + "'    ,'" + drpvalvesize + "' ,'" + drpvalveclass + "','" + drpvalveoperator + "','" + txtvalvetagno + "','" + drpiterlock + "','" + drphandwheelsize + "','" + drplever + "','" + txtqty + "' ,'" + txtunitprice + "' ,'" + txttotalprice + "')");


        }
        Grisummery.DataSource = db.Displaygrid("select valvetype as 'Valve type' ,valvesize as  'Valve size'  ,valveclass as 'Valve class', interlock  as Interlock, sum(qty) as Qty from  projectscopedetailsdummy group  by valvetype ,valvesize,qty,valveclass,interlock");
        Grisummery.DataBind();


        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);
    }

    protected void Gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Delete"):
                    {

                        ViewState["EditID"] = (e.CommandArgument);
                        GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                        int RowIndex = gvr.RowIndex;
                        DataTable dt = (DataTable)ViewState["CurrentTable"];
                        dt.Rows.RemoveAt(RowIndex);
                        Gridview1.DataSource = dt;
                        Gridview1.DataBind();

                        for (int i = 0; i < Gridview1.Rows.Count; i++)
                        {
                            ((DropDownList)Gridview1.Rows[i].FindControl("drpsystemname")).SelectedValue = dt.Rows[i]["systemname"].ToString();
                            ((DropDownList)Gridview1.Rows[i].FindControl("drpvalvetype")).SelectedValue = dt.Rows[i]["valvetype"].ToString();
                            ((DropDownList)Gridview1.Rows[i].FindControl("drpvalvesize")).SelectedValue = dt.Rows[i]["valvesize"].ToString();
                            ((DropDownList)Gridview1.Rows[i].FindControl("drpvalveclass")).SelectedValue = dt.Rows[i]["valveclass"].ToString();
                            ((DropDownList)Gridview1.Rows[i].FindControl("drpvalveoperator")).SelectedValue = dt.Rows[i]["valveoperator"].ToString();
                            ((DropDownList)Gridview1.Rows[i].FindControl("drpiterlock")).SelectedValue = dt.Rows[i]["interlock"].ToString();

                            ((DropDownList)Gridview1.Rows[i].FindControl("drphandwheelsize")).SelectedValue = dt.Rows[i]["handwheel"].ToString();
                            ((DropDownList)Gridview1.Rows[i].FindControl("drplever")).SelectedValue = dt.Rows[i]["lever"].ToString();
                        }
                             ((Label)Gridview1.FooterRow.FindControl("lblGrandTotal1")).Text = hdn1.Value;


                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);

                    }
                    break;
                case ("Delete12"):
                    {
                        ViewState["EditID"] = (e.CommandArgument);
                        db.insert("delete projectscopedetails where  pno='" + ViewState["EditID"] + "' ");
                        db.insert("delete projectscopeitem where  pno='" + ViewState["EditID"] + "' ");
                        db.insert("delete Projectscope where  pno='" + ViewState["EditID"] + "' ");
                        obj_Comman.ShowPopUpMsg("Record Deleted Successfully!", this.Page);
                        bindreportgrid();
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

    protected void grditem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList ddlitem = e.Row.FindControl("ddlitem") as DropDownList;
            ddlitem.DataSource = db.Displaygrid("select ItemName from ItemMaster  where IsDeleted=0");
            ddlitem.DataValueField = "ItemName";
            ddlitem.DataTextField = "ItemName";
            ddlitem.DataBind();
            ddlitem.Items.Insert(0, "Select ItemName");

            DropDownList drpCurrency = e.Row.FindControl("drpCurrency") as DropDownList;
            drpCurrency.DataSource = db.Displaygrid("select Currency from CurrencyMaster");
            drpCurrency.DataValueField = "Currency";
            drpCurrency.DataTextField = "Currency";
            drpCurrency.DataBind();
            drpCurrency.Items.Insert(0, "Select Currency");
        }
    }

    protected void grdpd_DataBinding(object sender, EventArgs e)
    {

    }

    protected void Gridview1_DataBound(object sender, EventArgs e)
    {

    }

    protected void grdpd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList drppd = e.Row.FindControl("drppd") as DropDownList;
            drppd.DataSource = db.Displaygrid("select pd from pdetails");
            drppd.DataValueField = "pd";
            drppd.DataTextField = "pd";
            drppd.DataBind();
            drppd.Items.Insert(0, "Select Charges");
            DropDownList drppdCurrency = e.Row.FindControl("drppdCurrency") as DropDownList;
            drppdCurrency.DataSource = db.Displaygrid("select Currency from CurrencyMaster");
            drppdCurrency.DataValueField = "Currency";
            drppdCurrency.DataTextField = "Currency";
            drppdCurrency.DataBind();
            drppdCurrency.Items.Insert(0, "Select Currency");
        }
    }

    protected void btnpdadd_Click(object sender, EventArgs e)
    {
        AddNewRowToGridpd();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);
    }

    protected void grditem_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            switch (e.CommandName)
            {
                case ("Delete"):
                    {

                        ViewState["EditID"] = (e.CommandArgument);





                        GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                        int RowIndex = gvr.RowIndex;
                        DataTable dt = (DataTable)ViewState["CurrentTable1"];
                        dt.Rows.RemoveAt(RowIndex);
                        grditem.DataSource = dt;
                        grditem.DataBind();
                        int a = 0;
                        for (int i = 0; i < grditem.Rows.Count; i++)
                        {
                            ((DropDownList)grditem.Rows[i].FindControl("ddlitem")).SelectedValue = dt.Rows[i]["ItemName"].ToString();
                            a += Convert.ToInt32(dt.Rows[i]["unitprice"].ToString());

                        }
                        ((Label)grditem.FooterRow.FindControl("lblGrandTotalitem1")).Text = a.ToString();
                        hdn2.Value = a.ToString();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);



                    }
                    break;
                case ("Delete12"):
                    {
                        ViewState["EditID"] = (e.CommandArgument);
                        db.insert("delete projectscopedetails where  pno='" + ViewState["EditID"] + "' ");
                        db.insert("delete projectscopeitem where  pno='" + ViewState["EditID"] + "' ");
                        db.insert("delete Projectscope where  pno='" + ViewState["EditID"] + "' ");
                        obj_Comman.ShowPopUpMsg("Record Deleted Successfully!", this.Page);
                        bindreportgrid();
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

    protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdpd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdpd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Delete"):
                    {

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);

                        ViewState["EditID"] = (e.CommandArgument);

                        GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                        int RowIndex = gvr.RowIndex;
                        DataTable dt = (DataTable)ViewState["CurrentTablepd"];
                        dt.Rows.RemoveAt(RowIndex);
                        grdpd.DataSource = dt;
                        grdpd.DataBind();

                        int a = 0;
                        for (int i = 0; i < grdpd.Rows.Count; i++)
                        {
                            ((DropDownList)grdpd.Rows[i].FindControl("drppd")).SelectedValue = dt.Rows[i]["pd"].ToString();

                            a += Convert.ToInt32(dt.Rows[i]["uprice"].ToString());
                        }
                          ((Label)grdpd.FooterRow.FindControl("lblpdprice")).Text = a.ToString();
                        hdn3.Value = a.ToString();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);


                    }
                    break;
                case ("Delete12"):
                    {
                        ViewState["EditID"] = (e.CommandArgument);
                        db.insert("delete projectscopedetails where  pno='" + ViewState["EditID"] + "' ");
                        db.insert("delete projectscopeitem where  pno='" + ViewState["EditID"] + "' ");
                        db.insert("delete Projectscope where  pno='" + ViewState["EditID"] + "' ");
                        obj_Comman.ShowPopUpMsg("Record Deleted Successfully!", this.Page);
                        bindreportgrid();
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


    protected void btndemo_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = db.Displaygrid("select interlock , keys , valvetype , valvesize, valveclass, valveoperator ,valvetagno,qty, unitprice ,totalprice  from  projectscopedetails where pno='PS- 3/S. K./102/19-20/1'");
        StringBuilder sb = new StringBuilder();
        //Table start.
        sb.Append("<table  style='class:mgrid'>");

        //Adding HeaderRow.
        sb.Append("<tr>");
        foreach (DataColumn column in dt.Columns)
        {
            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.ColumnName + "</th>");
        }
        sb.Append("</tr>");



        foreach (DataRow row in dt.Rows)
        {

            foreach (DataColumn column in dt.Columns)
            {
                sb.Append("<tr>");
                string bc = "valvetagno";
                string ac = column.ColumnName.ToString();

                if (ac == "qty")
                {
                    sb.Append("<td rowspan='6' style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
                }


                else if (ac == "valveoperator")
                {
                    sb.Append("     <td style='width:100px;border: 1px solid #ccc'>" + column.ColumnName.ToString() + "=" + "</td>   <td rowspan='6' style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
                }
                else if (ac == "unitprice")
                {
                    sb.Append("<td rowspan='6' style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
                }
                else if (ac == "totalprice")
                {
                    sb.Append("<tr>");
                    sb.Append("<td rowspan='6' style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
                    sb.Append("</tr>");
                }
                else if (ac == bc)
                {

                    sb.Append("    <td rowspan='6' style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");

                }

                else
                {
                    sb.Append("<tr>");
                    sb.Append("      <td style='width:100px;border: 1px solid #ccc'>" + column.ColumnName.ToString() + "=" + "</td>                       <td style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tr>");
            }


        }
        sb.Append("</table>");
        // ltTable.Text = sb.ToString();


    }


    //Table end.




    protected void txtunitprice_TextChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < Gridview1.Rows.Count; g++)
        {
            TextBox txtqty = (Gridview1.Rows[g].FindControl("txtqty") as TextBox);
            TextBox txtunitprice = (Gridview1.Rows[g].FindControl("txtunitprice") as TextBox);
            TextBox txttotalprice1 = (Gridview1.Rows[g].FindControl("txttotalprice1") as TextBox);

            float a = float.Parse(txtqty.Text);
            float b = float.Parse(txtunitprice.Text);
            float c = 0;

            c = a * b;
            txttotalprice1.Text = c.ToString();
            hdn4.Value = c.ToString();
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);

    }



    protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


    private void BindOrders(GridView gvOrders ,string pno)
    {
         gvOrders.DataSource = db.Displaygrid("select pno as 'Project No' ,purchasedate as 'Purachse Date'  from Projectscope where project='" + pno + "'");
          gvOrders.DataBind();

    }

    protected void Show_Hide_OrdersGrid(object sender, EventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlOrders").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            string customerId = ReportGrid.DataKeys[row.RowIndex].Value.ToString();
            GridView gvOrders = row.FindControl("gvOrders") as GridView;


            string pno = db.getDbstatus_Value("select project from Projectscope where pno='"+ customerId + "'");




            BindOrders(gvOrders, pno);

        }
        else
        {
            row.FindControl("pnlOrders").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";
        }
    }
    protected void drppd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int total = 0;
        for (int g = 0; g < Gridview1.Rows.Count; g++)
        {
            TextBox txttotalprice1 = (Gridview1.Rows[g].FindControl("txttotalprice1") as TextBox);

            total += Convert.ToInt32(txttotalprice1.Text);

        }
        ViewState["total"] = total.ToString();

        for (int g = 0; g < grdpd.Rows.Count; g++)
        {

            DropDownList drppd = (grdpd.Rows[g].FindControl("drppd") as DropDownList);
            TextBox txtpdprice = (grdpd.Rows[g].FindControl("txtpdprice") as TextBox);
            if (drppd.SelectedValue == "Documentation & Packing   " && txtpdprice.Text == "")
            {


                int d = Convert.ToInt32(total / 100 * 5 );
                txtpdprice.Text = d.ToString();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);
            }

            else if (drppd.SelectedValue == "Waranty Extension Charges" && txtpdprice.Text=="")
            {

                int d = Convert.ToInt32(total / 100 * 10);
                txtpdprice.Text = d.ToString();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);
            }
            else
            {

            }

        }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalprice_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpriceitem_function();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "txttotalpricepd_function();", true);
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string customerId = ReportGrid.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;






            gvOrders.DataSource = db.Displaygrid("select pno as 'Project No' ,purchasedate as 'Purachse Date'  from Projectscope");
            gvOrders.DataBind();
        }
    }

    protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            switch (e.CommandName)
            {
                case ("Printoffer"):
                    {

                        ViewState["EditID"] = (e.CommandArgument);
                        DataTable dt = db.Displaygrid("select interlock as Type , keys as Keys, valvetype as 'Valve Type' , valvesize as 'Valve Size', valveclass as 'Valve Class',   valveoperator as 'Valve Operation',         CASE      WHEN handwheel = 1 THEN '-'          ELSE handwheel END AS 'Handwheel Dia',    CASE      WHEN lever = 1 THEN '-'         ELSE lever END AS 'Lever Lenght',          valvetagno as 'Tag No',qty as 'Qty.', unitprice as 'Unit Price',totalprice as 'Total Price'  from  projectscopedetails where  pno='" + ViewState["EditID"] + "'   ");

                        grdoffer.DataSource = dt;
                        grdoffer.DataBind();

                        grdpddetailsoffer.DataSource = db.Displaygrid("select  pd as 'Charges',uprice as 'Price'   from  projectscope_pd where  pno='" + ViewState["EditID"] + "'  ");
                        grdpddetailsoffer.DataBind();


                        girdofferextra.DataSource = db.Displaygrid("select item as Particular , qty as Qty,unitprice as Price , Total  from projectscopeitem  where  pno='" + ViewState["EditID"] + "' ");
                        girdofferextra.DataBind();
                        lbloffertotalab.Text = db.getDbstatus_Value("select   total  from Projectscope where pno='" + ViewState["EditID"] + "'");
                        lblofeeroptionaltotal.Text = db.getDbstatus_Value("select   gtotal  from projectscope_pd where pno='" + ViewState["EditID"] + "'");
                        lbloffergridview1total.Text = db.getDbstatus_Value("select total  from projectscopedetails where pno='" + ViewState["EditID"] + "'");
                        lblofferitemtotal.Text = db.getDbstatus_Value("select total  from projectscopeitem where pno='" + ViewState["EditID"] + "'");
                        image3.ImageUrl = db.getDbstatus_Value("select  CLogo from  CompanyMaster where  CompanyId='" + "1" + "'");

                        lblofferprojectno.Text = ViewState["EditID"].ToString();
                        lblofferpurchasedate.Text = db.getDbstatus_Value("select   purchasedate from   Projectscope where  pno='" + ViewState["EditID"] + "'");
                        lbloffercreateddaye.Text = db.getDbstatus_Value("select   updatedby from   Projectscope where  pno='" + ViewState["EditID"] + "'");
                        lbloffercreateddate.Text = db.getDbstatus_Value("select   updateddate from   Projectscope where  pno='" + ViewState["EditID"] + "'");
                        lbloffferrtelno.Text = db.getDbstatus_Value("select  PhoneNo from  CompanyMaster");
                        lblofferemail.Text = db.getDbstatus_Value("select  EmailId from  CompanyMaster");
                        lbloferrwebsite.Text = db.getDbstatus_Value("select  Website from  CompanyMaster");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPaneloffer", "PrintPaneloffer();", true);

                    }

                    break;



            }
        }

        catch (Exception ex)
        {

        }
    }
}