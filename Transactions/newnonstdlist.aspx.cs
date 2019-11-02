using AjaxControlToolkit;
using MayurInventory.DataModel;
using MayurInventory.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transactions_newnonstdlist : System.Web.UI.Page
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
            SetInitialRow_Grditem();
            SetInitialRow_Grdpipebracket();
            SetInitialRow_Grdplate();
            SetInitialRow_Grdadapter();
            SetInitialRow_Grdadhandwheel();
            SetInitialRow_Grdadlever();
            SetInitialRow_Grdkey();
          

            SetInitialRow_ReportGrid();
            BindComboOfProject();
            bindcombo();
        }

    }

    private void bindcombo()
    {
        ReportGrid.DataSource = db.Displaygrid("select Nonstandaredmaster.interlock as #, TemplateMaster.TemplateName, Nonstandaredmaster.title from Nonstandaredmaster  inner join TemplateMaster on TemplateMaster.TemplateID=Nonstandaredmaster.interlock");
        ReportGrid.DataBind();
    }
    public void SetInitialRow_Grditem()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add("ItemName", typeof(string));

            dt.Columns.Add(new DataColumn("size", typeof(string)));
            dt.Columns.Add("shecdule", typeof(string));
            dt.Columns.Add("length", typeof(string));
            dt.Columns.Add("uom", typeof(string));
            dt.Columns.Add("qty", typeof(string));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;

            dr["ItemName"] = "";

            dr["size"] = "";
            dr["shecdule"] = "";
            dr["length"] = "";
            dr["uom"] = "";
            dr["qty"] = "";

            dt.Rows.Add(dr);
            ViewState["CurrentTable1"] = dt;
            grditem.DataSource = dt;
            grditem.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }    
    public void SetInitialRow_Grdpipebracket()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add("ItemName", typeof(string));

            dt.Columns.Add(new DataColumn("size", typeof(string)));
            dt.Columns.Add("shecdule", typeof(string));
           
         

            dr = dt.NewRow();
            dr["RowNumber"] = 1;

            dr["ItemName"] = "";

            dr["size"] = "";
            dr["shecdule"] = "";
          
          

            dt.Rows.Add(dr);
            ViewState["CurrentTablepipebracket"] = dt;
            grdpipebracket.DataSource = dt;
            grdpipebracket.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public void SetInitialRow_Grdplate()
    {
        try
        {
            DataTable dtplate = new DataTable();
            DataRow dr;
            dtplate.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dtplate.Columns.Add("ItemName", typeof(string));

            dtplate.Columns.Add(new DataColumn("Thickness", typeof(string)));


            dr = dtplate.NewRow();
            dr["RowNumber"] = 1;

            dr["ItemName"] = "";

            dr["Thickness"] = "";
           

           

            dtplate.Rows.Add(dr);
            ViewState["CurrentTableplate"] = dtplate;
            grdplate.DataSource = dtplate;
            grdplate.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public void SetInitialRow_Grdadapter()
    {
        try
        {
            DataTable dtplate = new DataTable();
            DataRow dr;
            dtplate.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dtplate.Columns.Add("ItemName", typeof(string));

            dtplate.Columns.Add(new DataColumn("AdaptorSizes", typeof(string)));

           

            dr = dtplate.NewRow();
            dr["RowNumber"] = 1;

            dr["ItemName"] = "";

            dr["AdaptorSizes"] = "";


         

            dtplate.Rows.Add(dr);
            ViewState["CurrentTableadapter"] = dtplate;
            grdadapter.DataSource = dtplate;
            grdadapter.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public void SetInitialRow_Grdadhandwheel()
    {
        try
        {
            DataTable dtplate = new DataTable();
            DataRow dr;
            dtplate.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dtplate.Columns.Add("ItemName", typeof(string));

            dtplate.Columns.Add(new DataColumn("HandwheelSizes", typeof(string)));

           

            dr = dtplate.NewRow();
            dr["RowNumber"] = 1;

            dr["ItemName"] = "";

            dr["HandwheelSizes"] = "";


          

            dtplate.Rows.Add(dr);
            ViewState["CurrentTablehandwheel"] = dtplate;
            grdhandwheel.DataSource = dtplate;
            grdhandwheel.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public void SetInitialRow_Grdadlever()
    {
        try
        {
            DataTable dtplate = new DataTable();
            DataRow dr;
            dtplate.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dtplate.Columns.Add("ItemName", typeof(string));

            dtplate.Columns.Add(new DataColumn("LeverSizes", typeof(string)));

           

            dr = dtplate.NewRow();
            dr["RowNumber"] = 1;

            dr["ItemName"] = "";

            dr["LeverSizes"] = "";


        

            dtplate.Rows.Add(dr);
            ViewState["CurrentTablelever"] = dtplate;
            grdlever.DataSource = dtplate;
            grdlever.DataBind();
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    public void SetInitialRow_Grdkey()
    {
        try
        {
            DataTable dtplate = new DataTable();
            DataRow dr;
            dtplate.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dtplate.Columns.Add("ItemName", typeof(string));

            dtplate.Columns.Add(new DataColumn("KeyCabinet", typeof(string)));

            dtplate.Columns.Add("qty", typeof(string));

            dr = dtplate.NewRow();
            dr["RowNumber"] = 1;

            dr["ItemName"] = "";

            dr["KeyCabinet"] = "";


            dr["qty"] = "";

            dtplate.Rows.Add(dr);
            ViewState["CurrentTablekey"] = dtplate;
            grdkey.DataSource = dtplate;
            grdkey.DataBind();
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

                    TextBox txtqty = (TextBox)grditem.Rows[rowIndex].Cells[3].FindControl("txtqty");
                    TextBox TxtItemName = (TextBox)grditem.Rows[rowIndex].Cells[0].FindControl("TxtItemName");


                    TextBox txtvalvesize = (TextBox)grditem.Rows[rowIndex].Cells[1].FindControl("txtvalvesize");



                    TextBox txtSchedule = (TextBox)grditem.Rows[rowIndex].Cells[2].FindControl("txtSchedule");

                    TextBox txtUOM = (TextBox)grditem.Rows[rowIndex].Cells[4].FindControl("txtUOM");
                    TextBox txtbqty = (TextBox)grditem.Rows[rowIndex].Cells[5].FindControl("txtbqty");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    dtCurrentTable.Rows[i - 1]["ItemName"] = TxtItemName.Text;

                    dtCurrentTable.Rows[i - 1]["size"] = txtvalvesize.Text;
                    dtCurrentTable.Rows[i - 1]["shecdule"] = txtSchedule.Text;
                    dtCurrentTable.Rows[i - 1]["length"] = txtqty.Text;

                    dtCurrentTable.Rows[i - 1]["uom"] = txtUOM.Text;
                    dtCurrentTable.Rows[i - 1]["qty"] = txtbqty.Text;


                    rowIndex++;
                }










                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable1"] = dtCurrentTable;

                grditem.DataSource = dtCurrentTable;
                grditem.DataBind();






            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataitem();
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

                    TextBox txtqty = (TextBox)grditem.Rows[rowIndex].Cells[3].FindControl("txtqty");
                    TextBox TxtItemName = (TextBox)grditem.Rows[rowIndex].Cells[0].FindControl("TxtItemName");

                    TextBox txtvalvesize = (TextBox)grditem.Rows[rowIndex].Cells[1].FindControl("txtvalvesize");



                    TextBox txtSchedule = (TextBox)grditem.Rows[rowIndex].Cells[2].FindControl("txtSchedule");

                    TextBox txtUOM = (TextBox)grditem.Rows[rowIndex].Cells[4].FindControl("txtUOM");
                    TextBox txtbqty = (TextBox)grditem.Rows[rowIndex].Cells[4].FindControl("txtbqty");


                    TxtItemName.Text = dt.Rows[i]["ItemName"].ToString();

                    txtvalvesize.Text = dt.Rows[i]["size"].ToString();

                    txtSchedule.Text = dt.Rows[i]["shecdule"].ToString();
                    txtqty.Text = dt.Rows[i]["length"].ToString();

                    txtUOM.Text = dt.Rows[i]["uom"].ToString();
                    txtbqty.Text = dt.Rows[i]["qty"].ToString();


                    rowIndex++;

                }
            }
        }
    }
    private void AddNewRowToGriplate()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTableplate"] != null)
        {
            DataTable CurrentTableplate = (DataTable)ViewState["CurrentTableplate"];
            DataRow drCurrentRow = null;
            if (CurrentTableplate.Rows.Count > 0)
            {
                for (int i = 1; i <= CurrentTableplate.Rows.Count; i++)
                {
                    //extract the TextBox values

                    // AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdplate.Rows[rowIndex].Cells[0].FindControl("TxtItemNameplate");


                    TextBox txtthickenss = (TextBox)grdplate.Rows[rowIndex].Cells[1].FindControl("txtthickenss");






                  
                    drCurrentRow = CurrentTableplate.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    CurrentTableplate.Rows[i - 1]["ItemName"] = TxtItemName.Text;

                    CurrentTableplate.Rows[i - 1]["Thickness"] = txtthickenss.Text;


              


                    rowIndex++;
                }

                CurrentTableplate.Rows.Add(drCurrentRow);
                ViewState["CurrentTableplate"] = CurrentTableplate;

                grdplate.DataSource = CurrentTableplate;
                grdplate.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataplate();
    }
    private void SetPreviousDataplate()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableplate"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableplate"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //   AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdplate.Rows[rowIndex].Cells[0].FindControl("TxtItemNameplate");

                    TextBox txtthickenss = (TextBox)grdplate.Rows[rowIndex].Cells[1].FindControl("txtthickenss");






                  


                    TxtItemName.Text = dt.Rows[i]["ItemName"].ToString();

                    txtthickenss.Text = dt.Rows[i]["Thickness"].ToString();





                  


                    rowIndex++;

                }
            }
        }
    }

    private void AddNewRowToGripipebracket()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTablepipebracket"] != null)
        {
            DataTable CurrentTablepipebracket = (DataTable)ViewState["CurrentTablepipebracket"];
            DataRow drCurrentRow = null;
            if (CurrentTablepipebracket.Rows.Count > 0)
            {
                for (int i = 1; i <= CurrentTablepipebracket.Rows.Count; i++)
                {
                    //extract the TextBox values

                    // AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdpipebracket.Rows[rowIndex].Cells[0].FindControl("TxtItemNamepipebracket");


                    TextBox txtvalvesize = (TextBox)grdpipebracket.Rows[rowIndex].Cells[1].FindControl("txtvalvesize");



                    TextBox txtSchedule = (TextBox)grdpipebracket.Rows[rowIndex].Cells[2].FindControl("txtSchedule");


      

                    drCurrentRow = CurrentTablepipebracket.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    CurrentTablepipebracket.Rows[i - 1]["ItemName"] = TxtItemName.Text;

                    CurrentTablepipebracket.Rows[i - 1]["size"] = txtvalvesize.Text;
                    CurrentTablepipebracket.Rows[i - 1]["shecdule"] = txtSchedule.Text;

                   


                    rowIndex++;
                }

                CurrentTablepipebracket.Rows.Add(drCurrentRow);
                ViewState["CurrentTablepipebracket"] = CurrentTablepipebracket;

                grdpipebracket.DataSource = CurrentTablepipebracket;
                grdpipebracket.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDatapipebracket();
    }
    private void SetPreviousDatapipebracket()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTablepipebracket"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTablepipebracket"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //   AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdpipebracket.Rows[rowIndex].Cells[0].FindControl("TxtItemNamepipebracket");

                    TextBox txtvalvesize = (TextBox)grdpipebracket.Rows[rowIndex].Cells[1].FindControl("txtvalvesize");



                    TextBox txtSchedule = (TextBox)grdpipebracket.Rows[rowIndex].Cells[2].FindControl("txtSchedule");


                  


                    TxtItemName.Text = dt.Rows[i]["ItemName"].ToString();

                    txtvalvesize.Text = dt.Rows[i]["size"].ToString();

                    txtSchedule.Text = dt.Rows[i]["shecdule"].ToString();



          


                    rowIndex++;

                }
            }
        }
    }

    private void AddNewRowToGriadapter()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTableadapter"] != null)
        {
            DataTable CurrentTableadapter = (DataTable)ViewState["CurrentTableadapter"];
            DataRow drCurrentRow = null;
            if (CurrentTableadapter.Rows.Count > 0)
            {
                for (int i = 1; i <= CurrentTableadapter.Rows.Count; i++)
                {
                    //extract the TextBox values

                    // AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdadapter.Rows[rowIndex].Cells[0].FindControl("TxtItemNameadapter");


                    TextBox txtadaptersize = (TextBox)grdadapter.Rows[rowIndex].Cells[1].FindControl("txtadaptersize");






 

                    drCurrentRow = CurrentTableadapter.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    CurrentTableadapter.Rows[i - 1]["ItemName"] = TxtItemName.Text;

                    CurrentTableadapter.Rows[i - 1]["AdaptorSizes"] = txtadaptersize.Text;


                  


                    rowIndex++;
                }

                CurrentTableadapter.Rows.Add(drCurrentRow);
                ViewState["CurrentTable1"] = CurrentTableadapter;

                grdadapter.DataSource = CurrentTableadapter;
                grdadapter.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataadapter();
    }
    private void SetPreviousDataadapter()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableadapter"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableadapter"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //   AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdadapter.Rows[rowIndex].Cells[0].FindControl("TxtItemNameadapter");

                    TextBox txtadaptersize = (TextBox)grdadapter.Rows[rowIndex].Cells[1].FindControl("txtadaptersize");






                  


                    TxtItemName.Text = dt.Rows[i]["ItemName"].ToString();

                    txtadaptersize.Text = dt.Rows[i]["AdaptorSizes"].ToString();


                    


                    rowIndex++;

                }
            }
        }
    }
    private void AddNewRowToGrihandwheel()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTablehandwheel"] != null)
        {
            DataTable CurrentTablehandwheel = (DataTable)ViewState["CurrentTablehandwheel"];
            DataRow drCurrentRow = null;
            if (CurrentTablehandwheel.Rows.Count > 0)
            {
                for (int i = 1; i <= CurrentTablehandwheel.Rows.Count; i++)
                {
                    //extract the TextBox values

                    // AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdhandwheel.Rows[rowIndex].Cells[0].FindControl("TxtItemNamehandwheel");


                    TextBox txthandwheel = (TextBox)grdhandwheel.Rows[rowIndex].Cells[1].FindControl("txthandwheel");






                    

                    drCurrentRow = CurrentTablehandwheel.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    CurrentTablehandwheel.Rows[i - 1]["ItemName"] = TxtItemName.Text;

                    CurrentTablehandwheel.Rows[i - 1]["HandwheelSizes"] = txthandwheel.Text;


                    


                    rowIndex++;
                }

                CurrentTablehandwheel.Rows.Add(drCurrentRow);
                ViewState["CurrentTablehandwheel"] = CurrentTablehandwheel;

                grdhandwheel.DataSource = CurrentTablehandwheel;
                grdhandwheel.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDatahandwheel();
    }
    private void SetPreviousDatahandwheel()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTablehandwheel"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTablehandwheel"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //   AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdhandwheel.Rows[rowIndex].Cells[0].FindControl("TxtItemNamehandwheel");

                    TextBox txthandwheel = (TextBox)grdhandwheel.Rows[rowIndex].Cells[1].FindControl("txthandwheel");






                  


                    TxtItemName.Text = dt.Rows[i]["ItemName"].ToString();

                    txthandwheel.Text = dt.Rows[i]["HandwheelSizes"].ToString();


                   


                    rowIndex++;

                }
            }
        }
    }
    private void AddNewRowToGrilever()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTablelever"] != null)
        {
            DataTable CurrentTablelever = (DataTable)ViewState["CurrentTablelever"];
            DataRow drCurrentRow = null;
            if (CurrentTablelever.Rows.Count > 0)
            {
                for (int i = 1; i <= CurrentTablelever.Rows.Count; i++)
                {
                    //extract the TextBox values

                    // AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdlever.Rows[rowIndex].Cells[0].FindControl("TxtItemNamelever");


                    TextBox txtlever = (TextBox)grdlever.Rows[rowIndex].Cells[1].FindControl("txtlever");






                    

                    drCurrentRow = CurrentTablelever.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    CurrentTablelever.Rows[i - 1]["ItemName"] = TxtItemName.Text;

                    CurrentTablelever.Rows[i - 1]["LeverSizes"] = txtlever.Text;




                    rowIndex++;
                }

                CurrentTablelever.Rows.Add(drCurrentRow);
                ViewState["CurrentTablelever"] = CurrentTablelever;

                grdlever.DataSource = CurrentTablelever;
                grdlever.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDatalever();
    }
    private void SetPreviousDatalever()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTablelever"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTablelever"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //   AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdlever.Rows[rowIndex].Cells[0].FindControl("TxtItemNamelever");

                    TextBox txtlever = (TextBox)grdlever.Rows[rowIndex].Cells[1].FindControl("txtlever");






                    TextBox txtbqty = (TextBox)grdlever.Rows[rowIndex].Cells[2].FindControl("txtbqty");


                    TxtItemName.Text = dt.Rows[i]["ItemName"].ToString();

                    txtlever.Text = dt.Rows[i]["LeverSizes"].ToString();


                   


                    rowIndex++;

                }
            }
        }
    }
    private void AddNewRowToGrikey()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTablekey"] != null)
        {
            DataTable CurrentTablekey = (DataTable)ViewState["CurrentTablekey"];
            DataRow drCurrentRow = null;
            if (CurrentTablekey.Rows.Count > 0)
            {
                for (int i = 1; i <= CurrentTablekey.Rows.Count; i++)
                {
                    //extract the TextBox values

                    // AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdkey.Rows[rowIndex].Cells[0].FindControl("TxtItemNamekey");


                    TextBox txtkey = (TextBox)grdkey.Rows[rowIndex].Cells[1].FindControl("txtkey");






                   

                    drCurrentRow = CurrentTablekey.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;


                    CurrentTablekey.Rows[i - 1]["ItemName"] = TxtItemName.Text;

                    CurrentTablekey.Rows[i - 1]["KeyCabinet"] = txtkey.Text;


                   


                    rowIndex++;
                }

                CurrentTablekey.Rows.Add(drCurrentRow);
                ViewState["CurrentTablekey"] = CurrentTablekey;

                grdkey.DataSource = CurrentTablekey;
                grdkey.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDatakey();
    }
    private void SetPreviousDatakey()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTablekey"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTablekey"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //   AjaxControlToolkit.ComboBox ddlItem = (AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].Cells[0].FindControl("ddlItem");


                    TextBox TxtItemName = (TextBox)grdkey.Rows[rowIndex].Cells[0].FindControl("TxtItemNamekey");

                    TextBox txtlever = (TextBox)grdkey.Rows[rowIndex].Cells[1].FindControl("txtkey");






           


                    TxtItemName.Text = dt.Rows[i]["ItemName"].ToString();

                    txtlever.Text = dt.Rows[i]["KeyCabinet"].ToString();


                 


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
    public void BindComboOfProject()
    {
        try
        {

            drpproject.DataSource = db.Displaygrid("select TemplateName, TemplateID  from TemplateMaster");
            drpproject.DataTextField = "TemplateName";
            drpproject.DataValueField = "TemplateID";
            drpproject.DataBind();
            drpproject.Items.Insert(0, "Select Interlock");




        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }




    }
    
    protected void ButtonAdd_Click1(object sender, EventArgs e)
    {
        AddNewRowToGriditem();
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
                ((AjaxControlToolkit.ComboBox)grditem.Rows[currrow].FindControl("ddlItemDescription")).Focus();
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
    protected void TxtItemName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = sender as TextBox;
            GridViewRow row = txt.NamingContainer as GridViewRow;
            int rowIndex = row.RowIndex;
            StrCondition = string.Empty;
            StrCondition = TxtItemName.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                TxtItemName.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                TxtItemName.ToolTip = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                ViewState["ddlItem"] = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                ((AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].FindControl("ddlItem")).SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();

                ddlItem_SelectedIndexChanged((((AjaxControlToolkit.ComboBox)grditem.Rows[rowIndex].FindControl("ddlItem"))) as AjaxControlToolkit.ComboBox, EventArgs.Empty);
                Ds = null;
            }
            else
            {
                TxtItemName.Text = "";
                TxtItemName.ToolTip = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void ddlItempipebracket_SelectedIndexChanged(object sender, EventArgs e)
    {





        try
        {
            AjaxControlToolkit.ComboBox ddlItempipebracket = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd = (GridViewRow)ddlItempipebracket.Parent.Parent;
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
                ((AjaxControlToolkit.ComboBox)grdpipebracket.Rows[currrow].FindControl("ddlItemDescription")).Focus();
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
    protected void TxtItemNamepipebracket_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = sender as TextBox;
            GridViewRow row = txt.NamingContainer as GridViewRow;
            int rowIndex = row.RowIndex;
            StrCondition = string.Empty;
            StrCondition = TxtItemNamepipebracket.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                if (StrCondition == "")
                {
                    TxtItemNamepipebracket.Text = "";
                    TxtItemNamepipebracket.ToolTip = "";
                }
                else
                {
                    TxtItemNamepipebracket.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                    TxtItemNamepipebracket.ToolTip = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                    ViewState["ddlItem"] = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                    ((AjaxControlToolkit.ComboBox)grdpipebracket.Rows[rowIndex].FindControl("ddlItempipebracket")).SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();

                    ddlItem_SelectedIndexChanged((((AjaxControlToolkit.ComboBox)grdpipebracket.Rows[rowIndex].FindControl("ddlItempipebracket"))) as AjaxControlToolkit.ComboBox, EventArgs.Empty);
                    Ds = null;
                }
            }
            else
            {
                TxtItemNamepipebracket.Text = "";
                TxtItemNamepipebracket.ToolTip = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void ddlItemplate_SelectedIndexChanged(object sender, EventArgs e)
    {



        try
        {
            AjaxControlToolkit.ComboBox ddlItemplate = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd = (GridViewRow)ddlItemplate.Parent.Parent;
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
                ((AjaxControlToolkit.ComboBox)grdplate.Rows[currrow].FindControl("ddlItemDescription")).Focus();
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
    protected void TxtItemNameplate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = sender as TextBox;
            GridViewRow row = txt.NamingContainer as GridViewRow;
            int rowIndex = row.RowIndex;
            StrCondition = string.Empty;
            StrCondition = TxtItemNameplate.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {


                if (StrCondition == "")
                {
                    TxtItemNameplate.Text = "";
                    TxtItemNameplate.ToolTip = "";
                }
                else
                {
                    TxtItemNameplate.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                    TxtItemNameplate.ToolTip = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                    ViewState["ddlItem"] = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                    ((AjaxControlToolkit.ComboBox)grdplate.Rows[rowIndex].FindControl("ddlItemplate")).SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();

                    ddlItem_SelectedIndexChanged((((AjaxControlToolkit.ComboBox)grdplate.Rows[rowIndex].FindControl("ddlItemplate"))) as AjaxControlToolkit.ComboBox, EventArgs.Empty);
                    Ds = null;
                }
            }
            else
            {
                TxtItemNamepipebracket.Text = "";
                TxtItemNamepipebracket.ToolTip = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void ddlItemadapter_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            AjaxControlToolkit.ComboBox ddlItemadapter = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd = (GridViewRow)ddlItemadapter.Parent.Parent;
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
                ((AjaxControlToolkit.ComboBox)grdadapter.Rows[currrow].FindControl("ddlItemDescription")).Focus();
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
    protected void TxtItemNameadapter_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = sender as TextBox;
            GridViewRow row = txt.NamingContainer as GridViewRow;
            int rowIndex = row.RowIndex;
            StrCondition = string.Empty;
            StrCondition = TxtItemNameadapter.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {

                if (StrCondition == "")
                {
                    TxtItemNameadapter.Text = "";
                    TxtItemNameadapter.ToolTip = "";
                }
                else
                {
                    TxtItemNameadapter.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                    TxtItemNameadapter.ToolTip = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                    ViewState["ddlItem"] = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                    ((AjaxControlToolkit.ComboBox)grdadapter.Rows[rowIndex].FindControl("ddlItemadapter")).SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();

                    ddlItem_SelectedIndexChanged((((AjaxControlToolkit.ComboBox)grdadapter.Rows[rowIndex].FindControl("ddlItemadapter"))) as AjaxControlToolkit.ComboBox, EventArgs.Empty);
                    Ds = null;
                }
            }
            else
            {
                TxtItemNamepipebracket.Text = "";
                TxtItemNamepipebracket.ToolTip = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void ddlItemhandwheel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AjaxControlToolkit.ComboBox ddlItemhandwheel = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd = (GridViewRow)ddlItemhandwheel.Parent.Parent;
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
                ((AjaxControlToolkit.ComboBox)grdhandwheel.Rows[currrow].FindControl("ddlItemDescription")).Focus();
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
    protected void TxtItemNamehandwheel_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = sender as TextBox;
            GridViewRow row = txt.NamingContainer as GridViewRow;
            int rowIndex = row.RowIndex;
            StrCondition = string.Empty;
            StrCondition = TxtItemNamehandwheel.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                if (StrCondition == "")
                {
                    TxtItemNamehandwheel.Text = "";
                    TxtItemNamehandwheel.ToolTip = "";
                }
                else
                {
                    TxtItemNamehandwheel.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                    TxtItemNamehandwheel.ToolTip = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                }
                ViewState["ddlItem"] = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                ((AjaxControlToolkit.ComboBox)grdhandwheel.Rows[rowIndex].FindControl("ddlItemhandwheel")).SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();

                ddlItem_SelectedIndexChanged((((AjaxControlToolkit.ComboBox)grdhandwheel.Rows[rowIndex].FindControl("ddlItemhandwheel"))) as AjaxControlToolkit.ComboBox, EventArgs.Empty);
                Ds = null;
            }
            else
            {
                TxtItemNamepipebracket.Text = "";
                TxtItemNamepipebracket.ToolTip = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    protected void ddlItemlever_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AjaxControlToolkit.ComboBox ddlItemlever = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd = (GridViewRow)ddlItemlever.Parent.Parent;
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
                ((AjaxControlToolkit.ComboBox)grdlever.Rows[currrow].FindControl("ddlItemDescription")).Focus();
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
    protected void TxtItemNamelever_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = sender as TextBox;
            GridViewRow row = txt.NamingContainer as GridViewRow;
            int rowIndex = row.RowIndex;
            StrCondition = string.Empty;
            StrCondition = TxtItemNamelever.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {

                if (StrCondition == "")
                {
                    TxtItemNamelever.Text = "";
                    TxtItemNamelever.ToolTip = "";
                }
                else
                {
                    TxtItemNamelever.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                    TxtItemNamelever.ToolTip = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                    ViewState["ddlItem"] = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                    ((AjaxControlToolkit.ComboBox)grdlever.Rows[rowIndex].FindControl("ddlItemlever")).SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();

                    ddlItem_SelectedIndexChanged((((AjaxControlToolkit.ComboBox)grdlever.Rows[rowIndex].FindControl("ddlItemlever"))) as AjaxControlToolkit.ComboBox, EventArgs.Empty);
                    Ds = null;
                }
            }
            else
            {
                TxtItemNamepipebracket.Text = "";
                TxtItemNamepipebracket.ToolTip = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    protected void ddlItemkey_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AjaxControlToolkit.ComboBox ddlItemkey = (AjaxControlToolkit.ComboBox)sender;
            GridViewRow grd = (GridViewRow)ddlItemkey.Parent.Parent;
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
                ((AjaxControlToolkit.ComboBox)grdkey.Rows[currrow].FindControl("ddlItemDescription")).Focus();
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
    protected void TxtItemNamekey_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = sender as TextBox;
            GridViewRow row = txt.NamingContainer as GridViewRow;
            int rowIndex = row.RowIndex;
            StrCondition = string.Empty;
            StrCondition = TxtItemNamekey.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_RequisitionCafeteria.GetItems(StrCondition, "", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {


                if (StrCondition == "")
                {
                    TxtItemNamekey.Text = "";
                    TxtItemNamekey.ToolTip = "";
                }
                else
                {
                    TxtItemNamekey.Text = Ds.Tables[0].Rows[0]["ItemName"].ToString();
                    TxtItemNamekey.ToolTip = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                    ViewState["ddlItem"] = Ds.Tables[0].Rows[0]["ItemId"].ToString();
                    ((AjaxControlToolkit.ComboBox)grdkey.Rows[rowIndex].FindControl("ddlItemkey")).SelectedValue = Ds.Tables[0].Rows[0]["ItemId"].ToString();

                    ddlItem_SelectedIndexChanged((((AjaxControlToolkit.ComboBox)grdkey.Rows[rowIndex].FindControl("ddlItemkey"))) as AjaxControlToolkit.ComboBox, EventArgs.Empty);
                    Ds = null;
                }
            }
            else
            {
                TxtItemNamepipebracket.Text = "";
                TxtItemNamepipebracket.ToolTip = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {

        if (drpproject.SelectedItem.ToString() != "Select Interlock")
        {

            int count = Convert.ToInt32(db.getDb_Value("select  count(interlock ) from Nonstandaredmaster where interlock='" + drpproject.SelectedValue + "'"));
            if (count == 0)
            {
                db.insert("insert into Nonstandaredmaster values('" + drpproject.SelectedValue + "' ,'" + txttitle.Text + "')");




                string txtvalvesize = "0";
                string txtSchedule = "0";

                for (int i = 0; i < grdpipebracket.Rows.Count; i++)
                {
                    try
                    {
                        string itemname = (((TextBox)grdpipebracket.Rows[i].FindControl("TxtItemNamepipebracket")).Text);

                        TextBox txt = grdpipebracket.Rows[i].FindControl("txtvalvesize") as TextBox;
                        if (string.IsNullOrEmpty(txt.Text))
                        {
                            txtvalvesize = 0.ToString();
                        }
                        else
                        {
                            txtvalvesize = (((TextBox)grdpipebracket.Rows[i].FindControl("txtvalvesize")).Text);

                        }


                        TextBox txt1 = grdpipebracket.Rows[i].FindControl("txtSchedule") as TextBox;
                        if (string.IsNullOrEmpty(txt1.Text))
                        {
                            txtSchedule = "0";
                        }
                        else
                        {
                            txtSchedule = (((TextBox)grdpipebracket.Rows[i].FindControl("txtSchedule")).Text);
                        }



                        db.insert("Insert Into nonstdpipebracket values('" + drpproject.SelectedValue + "' ,'" + itemname + "' ,'" + txtvalvesize + "' ,'" + txtSchedule + "')");
                    }
                    catch (Exception ex)
                    {
                        obj_Comman.ShowPopUpMsg("Error!", this.Page);
                    }
                }







                string txtthickenss = "0";
                for (int i = 0; i < grdplate.Rows.Count; i++)
                {
                    try
                    {
                        string itemname = (((TextBox)grdplate.Rows[i].FindControl("TxtItemNameplate")).Text);

                        TextBox txt = grdplate.Rows[i].FindControl("txtthickenss") as TextBox;
                        if (string.IsNullOrEmpty(txt.Text))
                        {
                            txtthickenss = 0.ToString();
                        }
                        else
                        {
                            txtthickenss = (((TextBox)grdplate.Rows[i].FindControl("txtthickenss")).Text);

                        }



                        db.insert("Insert Into nonstdplate values('" + drpproject.SelectedValue + "' ,'" + itemname + "' ,'" + txtthickenss + "' )");
                    }
                    catch (Exception ex)
                    {
                        obj_Comman.ShowPopUpMsg("Error!", this.Page);
                    }
                }





                string txtadaptersize = "0";
                for (int i = 0; i < grdadapter.Rows.Count; i++)
                {
                    try
                    {
                        string itemname = (((TextBox)grdadapter.Rows[i].FindControl("TxtItemNameadapter")).Text);

                        TextBox txt = grdadapter.Rows[i].FindControl("txtadaptersize") as TextBox;
                        if (string.IsNullOrEmpty(txt.Text))
                        {
                            txtadaptersize = 0.ToString();
                        }
                        else
                        {
                            txtadaptersize = (((TextBox)grdadapter.Rows[i].FindControl("txtadaptersize")).Text);

                        }



                        db.insert("Insert Into nonstdadpter values('" + drpproject.SelectedValue + "' ,'" + itemname + "' ,'" + txtadaptersize + "' )");
                    }
                    catch (Exception ex)
                    {
                        obj_Comman.ShowPopUpMsg("Error!", this.Page);
                    }
                }








                string txthandwheel = "0";
                for (int i = 0; i < grdhandwheel.Rows.Count; i++)
                {
                    try
                    {
                        string itemname = (((TextBox)grdhandwheel.Rows[i].FindControl("TxtItemNamehandwheel")).Text);

                        TextBox txt = grdhandwheel.Rows[i].FindControl("txthandwheel") as TextBox;
                        if (string.IsNullOrEmpty(txt.Text))
                        {
                            txthandwheel = 0.ToString();
                        }
                        else
                        {
                            txthandwheel = (((TextBox)grdhandwheel.Rows[i].FindControl("txthandwheel")).Text);

                        }



                        db.insert("Insert Into nonstdhandwheel values('" + drpproject.SelectedValue + "' ,'" + itemname + "' ,'" + txthandwheel + "' )");
                    }
                    catch (Exception ex)
                    {
                        obj_Comman.ShowPopUpMsg("Error!", this.Page);
                    }
                }



                string txtlever = "0";
                for (int i = 0; i < grdlever.Rows.Count; i++)
                {
                    try
                    {
                        string itemname = (((TextBox)grdlever.Rows[i].FindControl("TxtItemNamelever")).Text);

                        TextBox txt = grdlever.Rows[i].FindControl("txtlever") as TextBox;
                        if (string.IsNullOrEmpty(txt.Text))
                        {
                            txtlever = 0.ToString();
                        }
                        else
                        {
                            txtlever = (((TextBox)grdlever.Rows[i].FindControl("txtlever")).Text);

                        }



                        db.insert("Insert Into nonstdlever values('" + drpproject.SelectedValue + "' ,'" + itemname + "' ,'" + txtlever + "' )");
                    }
                    catch (Exception ex)
                    {
                        obj_Comman.ShowPopUpMsg("Error!", this.Page);
                    }
                }





                string txtkey = "0";
                for (int i = 0; i < grdkey.Rows.Count; i++)
                {
                    try
                    {
                        string itemname = (((TextBox)grdkey.Rows[i].FindControl("TxtItemNamekey")).Text);

                        TextBox txt = grdkey.Rows[i].FindControl("txtkey") as TextBox;
                        if (string.IsNullOrEmpty(txt.Text))
                        {
                            txtkey = 0.ToString();
                        }
                        else
                        {
                            txtkey = (((TextBox)grdkey.Rows[i].FindControl("txtkey")).Text);

                        }



                        db.insert("Insert Into nonstdkey values('" + drpproject.SelectedValue + "' ,'" + itemname + "' ,'" + txtkey + "' )");
                    }
                    catch (Exception ex)
                    {
                        obj_Comman.ShowPopUpMsg("Error!", this.Page);
                    }
                }



                















                obj_Comman.ShowPopUpMsg("Record Save Successfully!", this.Page);
                    SetInitialRow_Grditem();
                    txttitle.Text = "";
                    bindcombo();
                }
            else
            {
                    obj_Comman.ShowPopUpMsg("Interlock Already Added Please Update Interlock !", this.Page);
                }
            }

            else
            {
                obj_Comman.ShowPopUpMsg("Please Select Interlock !", this.Page);
            }
        } 
   
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        if (drpproject.SelectedItem.ToString() != "Select Interlock")
        {
            db.insert("update Nonstandaredmaster  set  title='" + txttitle.Text + "'");
            db.insert("delete Nonstandreddetails where  iterlockid='" + drpproject.SelectedValue + "' ");
            db.insert("delete nonstdadpter where  iterlockid='" + drpproject.SelectedValue + "' ");
            db.insert("delete nonstdhandwheel where  iterlockid='" + drpproject.SelectedValue + "' ");
            db.insert("delete nonstdkey where  iterlockid='" + drpproject.SelectedValue + "' ");
            db.insert("delete nonstdlever where  iterlockid='" + drpproject.SelectedValue + "' ");
            db.insert("delete nonstdpipebracket where  iterlockid='" + drpproject.SelectedValue + "' ");
            db.insert("delete nonstdplate where  iterlockid='" + drpproject.SelectedValue + "' ");
            db.insert("delete nonstwood where  iterlockid='" + drpproject.SelectedValue + "' ");

            string txtvalvesize = "0";
            string txtSchedule = "0";

            for (int i = 0; i < grdpipebracket.Rows.Count; i++)
            {
                try
                {
                    string itemname = (((TextBox)grdpipebracket.Rows[i].FindControl("TxtItemNamepipebracket")).Text);

                    TextBox txt = grdpipebracket.Rows[i].FindControl("txtvalvesize") as TextBox;
                    if (string.IsNullOrEmpty(txt.Text))
                    {
                        txtvalvesize = 0.ToString();
                    }
                    else
                    {
                        txtvalvesize = (((TextBox)grdpipebracket.Rows[i].FindControl("txtvalvesize")).Text);

                    }


                    TextBox txt1 = grdpipebracket.Rows[i].FindControl("txtSchedule") as TextBox;
                    if (string.IsNullOrEmpty(txt1.Text))
                    {
                        txtSchedule = "0";
                    }
                    else
                    {
                        txtSchedule = (((TextBox)grdpipebracket.Rows[i].FindControl("txtSchedule")).Text);
                    }



                    db.insert("Insert Into nonstdpipebracket values('" + drpproject.SelectedValue + "' ,'" + itemname + "' ,'" + txtvalvesize + "' ,'" + txtSchedule + "')");
                }
                catch (Exception ex)
                {
                    obj_Comman.ShowPopUpMsg("Error!", this.Page);
                }
            }







            string txtthickenss = "0";
            for (int i = 0; i < grdplate.Rows.Count; i++)
            {
                try
                {
                    string itemname = (((TextBox)grdplate.Rows[i].FindControl("TxtItemNameplate")).Text);

                    TextBox txt = grdplate.Rows[i].FindControl("txtthickenss") as TextBox;
                    if (string.IsNullOrEmpty(txt.Text))
                    {
                        txtthickenss = 0.ToString();
                    }
                    else
                    {
                        txtthickenss = (((TextBox)grdplate.Rows[i].FindControl("txtthickenss")).Text);

                    }



                    db.insert("Insert Into nonstdplate values('" + drpproject.SelectedValue + "' ,'" + itemname + "' ,'" + txtthickenss + "' )");
                }
                catch (Exception ex)
                {
                    obj_Comman.ShowPopUpMsg("Error!", this.Page);
                }
            }





            string txtadaptersize = "0";
            for (int i = 0; i < grdadapter.Rows.Count; i++)
            {
                try
                {
                    string itemname = (((TextBox)grdadapter.Rows[i].FindControl("TxtItemNameadapter")).Text);

                    TextBox txt = grdadapter.Rows[i].FindControl("txtadaptersize") as TextBox;
                    if (string.IsNullOrEmpty(txt.Text))
                    {
                        txtadaptersize = 0.ToString();
                    }
                    else
                    {
                        txtadaptersize = (((TextBox)grdadapter.Rows[i].FindControl("txtadaptersize")).Text);

                    }



                    db.insert("Insert Into nonstdadpter values('" + drpproject.SelectedValue + "' ,'" + itemname + "' ,'" + txtadaptersize + "' )");
                }
                catch (Exception ex)
                {
                    obj_Comman.ShowPopUpMsg("Error!", this.Page);
                }
            }








            string txthandwheel = "0";
            for (int i = 0; i < grdhandwheel.Rows.Count; i++)
            {
                try
                {
                    string itemname = (((TextBox)grdhandwheel.Rows[i].FindControl("TxtItemNamehandwheel")).Text);

                    TextBox txt = grdhandwheel.Rows[i].FindControl("txthandwheel") as TextBox;
                    if (string.IsNullOrEmpty(txt.Text))
                    {
                        txthandwheel = 0.ToString();
                    }
                    else
                    {
                        txthandwheel = (((TextBox)grdhandwheel.Rows[i].FindControl("txthandwheel")).Text);

                    }



                    db.insert("Insert Into nonstdhandwheel values('" + drpproject.SelectedValue + "' ,'" + itemname + "' ,'" + txthandwheel + "' )");
                }
                catch (Exception ex)
                {
                    obj_Comman.ShowPopUpMsg("Error!", this.Page);
                }
            }



            string txtlever = "0";
            for (int i = 0; i < grdlever.Rows.Count; i++)
            {
                try
                {
                    string itemname = (((TextBox)grdlever.Rows[i].FindControl("TxtItemNamelever")).Text);

                    TextBox txt = grdlever.Rows[i].FindControl("txtlever") as TextBox;
                    if (string.IsNullOrEmpty(txt.Text))
                    {
                        txtlever = 0.ToString();
                    }
                    else
                    {
                        txtlever = (((TextBox)grdlever.Rows[i].FindControl("txtlever")).Text);

                    }



                    db.insert("Insert Into nonstdlever values('" + drpproject.SelectedValue + "' ,'" + itemname + "' ,'" + txtlever + "' )");
                }
                catch (Exception ex)
                {
                    obj_Comman.ShowPopUpMsg("Error!", this.Page);
                }
            }





            string txtkey = "0";
            for (int i = 0; i < grdkey.Rows.Count; i++)
            {
                try
                {
                    string itemname = (((TextBox)grdkey.Rows[i].FindControl("TxtItemNamekey")).Text);

                    TextBox txt = grdkey.Rows[i].FindControl("txtkey") as TextBox;
                    if (string.IsNullOrEmpty(txt.Text))
                    {
                        txtkey = 0.ToString();
                    }
                    else
                    {
                        txtkey = (((TextBox)grdkey.Rows[i].FindControl("txtkey")).Text);

                    }



                    db.insert("Insert Into nonstdkey values('" + drpproject.SelectedValue + "' ,'" + itemname + "' ,'" + txtkey + "' )");
                }
                catch (Exception ex)
                {
                    obj_Comman.ShowPopUpMsg("Error!", this.Page);
                }
            }



           


            obj_Comman.ShowPopUpMsg("Record Updated Successfully!", this.Page);
            SetInitialRow_Grditem();
            txttitle.Text = "";
            bindcombo();

        }

        else
        {
            obj_Comman.ShowPopUpMsg("Please Select Interlock !", this.Page);
        }
    }

    protected void btnpipebracket_Click(object sender, EventArgs e)
    {
        AddNewRowToGripipebracket();
    }
    protected void btnadapter_Click(object sender, EventArgs e)
    {
        AddNewRowToGriadapter();
    }



    protected void btnhandwheel_Click(object sender, EventArgs e)
    {
        AddNewRowToGrihandwheel();
    }


    protected void btnlever_Click(object sender, EventArgs e)
    {
        AddNewRowToGrilever();
    }


    protected void btnkey_Click(object sender, EventArgs e)
    {
        AddNewRowToGrikey();
    }


    protected void btnplate_Click(object sender, EventArgs e)
    {
        AddNewRowToGriplate();
    }
    protected void grdpipebracket_DataBound(object sender, EventArgs e)
    {

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

                        drpproject.SelectedValue = e.CommandArgument.ToString();





                        grditem.DataSource = db.Displaygrid("select   itemname  , size, shecdule , length, uom ,  qty  from Nonstandreddetails where iterlockid='" + ViewState["EditID"] + "'");

                        grditem.DataBind();

                        ViewState["CurrentTable1"] = db.Displaygrid("select id as RowNumber,  itemname  , size, shecdule , length, uom ,  qty  from Nonstandreddetails where iterlockid='" + ViewState["EditID"] + "'");

                        DataTable dt = new DataTable();
                        dt = db.Displaygrid("select *  from Nonstandreddetails where iterlockid='" + ViewState["EditID"] + "'");

                        for (int i = 0; i < grditem.Rows.Count; i++)
                        {
                            ((TextBox)grditem.Rows[i].FindControl("TxtItemName")).Text = dt.Rows[i]["itemname"].ToString();
                            ((TextBox)grditem.Rows[i].FindControl("txtvalvesize")).Text = dt.Rows[i]["size"].ToString();
                            ((TextBox)grditem.Rows[i].FindControl("txtSchedule")).Text = dt.Rows[i]["shecdule"].ToString();
                            ((TextBox)grditem.Rows[i].FindControl("txtqty")).Text = dt.Rows[i]["length"].ToString();
                            ((TextBox)grditem.Rows[i].FindControl("txtUOM")).Text = dt.Rows[i]["uom"].ToString();
                            ((TextBox)grditem.Rows[i].FindControl("txtbqty")).Text = dt.Rows[i]["qty"].ToString();
                        }



                        grdpipebracket.DataSource = db.Displaygrid("select  ROW_NUMBER() OVER(ORDER BY id ASC) AS RowNumber,   itemname  , size, shecdule  from nonstdpipebracket where iterlockid='" + ViewState["EditID"] + "'");

                        grdpipebracket.DataBind();

                        ViewState["CurrentTablepipebracket"] = db.Displaygrid("select ROW_NUMBER() OVER(ORDER BY id ASC) AS RowNumber ,  itemname  , size, shecdule  from nonstdpipebracket where iterlockid='" + ViewState["EditID"] + "'");
                        DataTable dtpipebracket = new DataTable();
                        dtpipebracket = db.Displaygrid("select *  from nonstdpipebracket where iterlockid='" + ViewState["EditID"] + "'");

                        for (int i = 0; i < grdpipebracket.Rows.Count; i++)
                        {
                            ((TextBox)grdpipebracket.Rows[i].FindControl("TxtItemNamepipebracket")).Text = dtpipebracket.Rows[i]["itemname"].ToString();
                            ((TextBox)grdpipebracket.Rows[i].FindControl("txtvalvesize")).Text = dtpipebracket.Rows[i]["size"].ToString();
                            ((TextBox)grdpipebracket.Rows[i].FindControl("txtSchedule")).Text = dtpipebracket.Rows[i]["shecdule"].ToString();
                         
                        }



                        grdplate.DataSource = db.Displaygrid("select  ROW_NUMBER() OVER(ORDER BY id ASC) AS RowNumber,  itemname  , Thickness  from nonstdplate where iterlockid='" + ViewState["EditID"] + "'");

                        grdplate.DataBind();

                        ViewState["CurrentTableplate"] = db.Displaygrid("select ROW_NUMBER() OVER(ORDER BY id ASC) AS RowNumber,  itemname  , Thickness from nonstdplate where iterlockid='" + ViewState["EditID"] + "'");

                        DataTable dtplate = new DataTable();
                        dtplate = db.Displaygrid("select *  from nonstdplate where iterlockid='" + ViewState["EditID"] + "'");

                        for (int i = 0; i < grdplate.Rows.Count; i++)
                        {
                            ((TextBox)grdplate.Rows[i].FindControl("TxtItemNameplate")).Text = dtplate.Rows[i]["itemname"].ToString();
                            ((TextBox)grdplate.Rows[i].FindControl("txtthickenss")).Text = dtplate.Rows[i]["Thickness"].ToString();
                        }


                         
                        grdadapter.DataSource = db.Displaygrid("select  ROW_NUMBER() OVER(ORDER BY id ASC) AS RowNumber,  itemname  , AdaptorSizes  from nonstdadpter where iterlockid='" + ViewState["EditID"] + "'");

                        grdadapter.DataBind();

                        ViewState["CurrentTableadapter"] = db.Displaygrid("select ROW_NUMBER() OVER(ORDER BY id ASC) AS RowNumber,  itemname  , AdaptorSizes from nonstdadpter where iterlockid='" + ViewState["EditID"] + "'");

                        DataTable dtadapter = new DataTable();
                        dtadapter = db.Displaygrid("select *  from nonstdadpter where iterlockid='" + ViewState["EditID"] + "'");

                        for (int i = 0; i < grdadapter.Rows.Count; i++)
                        {
                            ((TextBox)grdadapter.Rows[i].FindControl("TxtItemNameadapter")).Text = dtadapter.Rows[i]["itemname"].ToString();
                            ((TextBox)grdadapter.Rows[i].FindControl("txtadaptersize")).Text = dtadapter.Rows[i]["AdaptorSizes"].ToString();
                        }



                        grdhandwheel.DataSource = db.Displaygrid("select  ROW_NUMBER() OVER(ORDER BY id ASC) AS RowNumber,  itemname  , HandwheelSizes  from nonstdhandwheel where iterlockid='" + ViewState["EditID"] + "'");

                        grdhandwheel.DataBind();

                        ViewState["CurrentTablehandwheel"] = db.Displaygrid("select ROW_NUMBER() OVER(ORDER BY id ASC) AS RowNumber,  itemname  , HandwheelSizes from nonstdhandwheel where iterlockid='" + ViewState["EditID"] + "'");


                        DataTable dthandwheel = new DataTable();
                        dthandwheel = db.Displaygrid("select *  from nonstdhandwheel where iterlockid='" + ViewState["EditID"] + "'");

                        for (int i = 0; i < grdhandwheel.Rows.Count; i++)
                        {
                            ((TextBox)grdhandwheel.Rows[i].FindControl("TxtItemNamehandwheel")).Text = dthandwheel.Rows[i]["itemname"].ToString();
                            ((TextBox)grdhandwheel.Rows[i].FindControl("txthandwheel")).Text = dthandwheel.Rows[i]["HandwheelSizes"].ToString();
                        }






                        grdlever.DataSource = db.Displaygrid("select  ROW_NUMBER() OVER(ORDER BY id ASC) AS RowNumber, itemname  , LeverSizes  from nonstdlever where iterlockid='" + ViewState["EditID"] + "'");

                        grdlever.DataBind();

                        ViewState["CurrentTablelever"] = db.Displaygrid("select ROW_NUMBER() OVER(ORDER BY id ASC) AS RowNumber,  itemname  , LeverSizes from nonstdlever where iterlockid='" + ViewState["EditID"] + "'");

                        DataTable dtlever = new DataTable();
                        dtlever = db.Displaygrid("select *  from nonstdlever where iterlockid='" + ViewState["EditID"] + "'");

                        for (int i = 0; i < grdlever.Rows.Count; i++)
                        {
                            ((TextBox)grdlever.Rows[i].FindControl("TxtItemNamelever")).Text = dtlever.Rows[i]["itemname"].ToString();
                            ((TextBox)grdlever.Rows[i].FindControl("txtlever")).Text = dtlever.Rows[i]["LeverSizes"].ToString();
                        }






                        grdkey.DataSource = db.Displaygrid("select   itemname  , KeyCabinet  from nonstdkey where iterlockid='" + ViewState["EditID"] + "'");

                        grdkey.DataBind();

                        ViewState["CurrentTablekey"] = db.Displaygrid("select id as RowNumber,  itemname  , KeyCabinet from nonstdkey where iterlockid='" + ViewState["EditID"] + "'");
                        DataTable dtkey = new DataTable();
                        dtkey = db.Displaygrid("select *  from nonstdkey where iterlockid='" + ViewState["EditID"] + "'");

                        for (int i = 0; i < grdkey.Rows.Count; i++)
                        {
                            ((TextBox)grdkey.Rows[i].FindControl("TxtItemNamekey")).Text = dtkey.Rows[i]["itemname"].ToString();
                            ((TextBox)grdkey.Rows[i].FindControl("txtkey")).Text = dtkey.Rows[i]["KeyCabinet"].ToString();
                        }











                    }
                    break;
                case ("Delete"):
                    {
                        ViewState["EditID"] = (e.CommandArgument);
                        db.insert("delete Nonstandreddetails where  iterlockid='" + ViewState["EditID"] + "' ");
                        db.insert("delete Nonstandaredmaster where  interlock='" + ViewState["EditID"] + "' ");
                        db.insert("delete nonstdadpter where  iterlockid='" + ViewState["EditID"] + "' ");
                        db.insert("delete nonstdhandwheel where  iterlockid='" + ViewState["EditID"] + "' ");
                        db.insert("delete nonstdkey where  iterlockid='" + ViewState["EditID"] + "' ");
                        db.insert("delete nonstdlever where  iterlockid='" + ViewState["EditID"] + "' ");
                        db.insert("delete nonstdplate where  iterlockid='" + ViewState["EditID"] + "' ");
                        db.insert("delete nonstwood where  iterlockid='" + ViewState["EditID"] + "' ");
                        db.insert("delete nonstdpipebracket where  iterlockid='" + ViewState["EditID"] + "' ");
                        bindcombo();
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
    protected void grdadapter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //DropDownList drpvalvesize = e.Row.FindControl("drpvalvesizead") as DropDownList;


            //drpvalvesize.DataSource = db.Displaygrid("Select * from valvesize");

            //drpvalvesize.DataValueField = "id";
            //drpvalvesize.DataTextField = "valvesize";
            //drpvalvesize.DataBind();
            //drpvalvesize.Items.Insert(0, "Select Valve Size");
        }
    }
    protected void grditem_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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
                        
                       



                    }
                    break;
              
                
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void btndeleteitem_Click(object sender, EventArgs e)
    {

    }

    protected void grdpipebracket_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        DataTable dt = (DataTable)ViewState["CurrentTablepipebracket"];
                        dt.Rows.RemoveAt(RowIndex);
                        grdpipebracket.DataSource = dt;
                        grdpipebracket.DataBind();


                        if(grdpipebracket.Rows.Count>0)
                        {
                            for (int i = 0; i < grdpipebracket.Rows.Count; i++)
                            {
                                ((DropDownList)grdpipebracket.Rows[i].FindControl("ddlItempipebracket")).SelectedValue = dt.Rows[i]["ItemName"].ToString();


                            }
                        }

                        else
                        {
                            SetInitialRow_Grdpipebracket();
                        }
                       





                    }
                    break;


            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void grdpipebracket_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdplate_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        DataTable dt = (DataTable)ViewState["CurrentTableplate"];
                        dt.Rows.RemoveAt(RowIndex);
                        grdplate.DataSource = dt;
                        grdplate.DataBind();


                        if (grdplate.Rows.Count > 0)
                        {
                            for (int i = 0; i < grdplate.Rows.Count; i++)
                            {
                                ((DropDownList)grdplate.Rows[i].FindControl("ddlItemplate")).SelectedValue = dt.Rows[i]["ItemName"].ToString();


                            }
                        }

                        else
                        {
                            SetInitialRow_Grdplate();
                        }






                    }
                    break;


            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void grdplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdadapter_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        DataTable dt = (DataTable)ViewState["CurrentTableadapter"];
                        dt.Rows.RemoveAt(RowIndex);
                        grdadapter.DataSource = dt;
                        grdadapter.DataBind();


                        if (grdadapter.Rows.Count > 0)
                        {
                            for (int i = 0; i < grdadapter.Rows.Count; i++)
                            {
                                ((DropDownList)grdadapter.Rows[i].FindControl("ddlItemadapter")).SelectedValue = dt.Rows[i]["ItemName"].ToString();


                            }
                        }

                        else
                        {
                            SetInitialRow_Grdadapter();
                        }






                    }
                    break;


            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void grdadapter_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {



    }

    protected void grdhandwheel_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdhandwheel_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        DataTable dt = (DataTable)ViewState["CurrentTablehandwheel"];
                        dt.Rows.RemoveAt(RowIndex);
                        grdhandwheel.DataSource = dt;
                        grdhandwheel.DataBind();


                        if (grdhandwheel.Rows.Count > 0)
                        {
                            for (int i = 0; i < grdhandwheel.Rows.Count; i++)
                            {
                                ((DropDownList)grdhandwheel.Rows[i].FindControl("ddlItemhandwheel")).SelectedValue = dt.Rows[i]["ItemName"].ToString();


                            }
                        }

                        else
                        {
                            SetInitialRow_Grdadhandwheel();
                        }






                    }
                    break;


            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void grdlever_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grdlever_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        DataTable dt = (DataTable)ViewState["CurrentTablelever"];
                        dt.Rows.RemoveAt(RowIndex);
                        grdlever.DataSource = dt;
                        grdlever.DataBind();


                        if (grdlever.Rows.Count > 0)
                        {
                            for (int i = 0; i < grdlever.Rows.Count; i++)
                            {
                                ((DropDownList)grdlever.Rows[i].FindControl("ddlItemlever")).SelectedValue = dt.Rows[i]["ItemName"].ToString();


                            }
                        }

                        else
                        {
                            SetInitialRow_Grdadlever();
                        }






                    }
                    break;


            }
        }
        catch (Exception ex)
        {

        }
    }
}