using MayurInventory.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transactions_Generateenquiry : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["MayurInventory"].ConnectionString;
    database db = new database();
    CommanFunction obj_Comman = new CommanFunction();
    float ENNO = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"].ToString() != "" && Session["UserName"].ToString() != null)
            {
                binddrpdrpindentno();
                bindcompany();
                //  grdiparticular.DataBind();
                ENNO = db.getDb_Value("select max(id) from enquirymaster ");
                ENNO++;
                lblenquiryno.Text = "ENQ" + ENNO.ToString();
                txtdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ReportGrid.DataSource = db.Displaygrid("select   distinct(Enquirydetails.supplier) , (enquirymaster.id) as #,    Enquirydetails.reamrk , enquirymaster.date , enquirymaster.enno from Enquirydetails  inner join enquirymaster   on enquirymaster.enno=Enquirydetails.enno  order by enquirymaster.enno  desc ");
                ReportGrid.DataBind();
                txtdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        ENNO = db.getDb_Value("select max(id) from enquirymaster ");
        ENNO++;

    }



    public void SetInitialRow_ReportGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("#", typeof(int)));
            dt.Columns.Add(new DataColumn("ENQNO", typeof(string)));
            dt.Columns.Add(new DataColumn("SupplierName", typeof(string)));
            dt.Columns.Add(new DataColumn("Remark", typeof(string)));

            // dt.Columns.Add(new DataColumn("EmailStatus"))

            dr = dt.NewRow();
            dr["#"] = 0;
            dr["ENQNO"] = string.Empty;
            dr["TemplateTitle"] = string.Empty;
            dr["Remark"] = string.Empty;

            dt.Rows.Add(dr);
            ReportGrid.DataSource = dt;
            ReportGrid.DataBind();
        }
        catch (Exception ex)
        {
        }
    }


    void binddrpdrpindentno()
    {
        drpindentno.DataSource = db.Displaygrid("select distinct(RequisitionNo) ,RequisitionCafeId from  RequisitionCafeteria WHERE  RequisitionNo NOT IN (SELECT indentno FROM enquirymaster) and  ReqStatus='" + "Authorised" + "' ");

        drpindentno.DataTextField = "RequisitionNo";
        drpindentno.DataValueField = "RequisitionCafeId";
        drpindentno.DataBind();
        drpindentno.Items.Insert(0, "Select Indent No.");
    }

    void bindcompany()
    {
        drpcompany.DataSource = db.Displaygrid("select CompanyName ,CompanyId from  CompanyMaster ");

        drpcompany.DataTextField = "CompanyName";
        drpcompany.DataValueField = "CompanyName";
        drpcompany.DataBind();
    }


    protected void grdiparticular_RowDataBound(object sender, GridViewRowEventArgs e)
    {








    }

    protected void GrdRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            string name = (e.Row.FindControl("txtParticulars") as TextBox).Text;

            float itemid = db.getDb_Value("select   ItemId  from  ItemMaster where  ItemName='" + name + "'");

            int supplierid = Convert.ToInt32(db.getDb_Value("select top 1 SuplierMaster.SuplierId from  PurchaseOrder inner join PurchaseOrdDtls  on  PurchaseOrder.POId=PurchaseOrdDtls.POId   inner join SuplierMaster on  SuplierMaster.SuplierId=PurchaseOrder.SuplierId where  PurchaseOrdDtls.ItemId='" + itemid + "'"));








            DropDownList ddlsupplier = (e.Row.FindControl("ddlsupplier") as DropDownList);
            DropDownList ddlsupplier2 = (e.Row.FindControl("ddlsupplier2") as DropDownList);
            DropDownList ddlsupplier3 = (e.Row.FindControl("ddlsupplier3") as DropDownList);

            DropDownList ddlsupplier4 = (e.Row.FindControl("ddlsupplier4") as DropDownList);
            DropDownList ddlsupplier5 = (e.Row.FindControl("ddlsupplier5") as DropDownList);
            DropDownList ddlsupplier6 = (e.Row.FindControl("ddlsupplier6") as DropDownList);
            using (SqlConnection con = new SqlConnection(cs))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT SuplierName ,SuplierId  FROM SuplierMaster  order by  SuplierName  "))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    ddlsupplier.DataSource = cmd.ExecuteReader();
                    ddlsupplier.DataTextField = "SuplierName";
                    ddlsupplier.DataValueField = "SuplierName";
                    ddlsupplier.DataBind();
                    con.Close();

                    ddlsupplier.Items.Insert(0, "Select Supplier");

                    if (supplierid != 0)
                    {
                        ddlsupplier.SelectedValue = supplierid.ToString();
                    }
                }


            }






            using (SqlConnection con = new SqlConnection(cs))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT SuplierName ,SuplierId  FROM SuplierMaster  order by  SuplierName  "))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    ddlsupplier2.DataSource = cmd.ExecuteReader();
                    ddlsupplier2.DataTextField = "SuplierName";
                    ddlsupplier2.DataValueField = "SuplierName";
                    ddlsupplier2.DataBind();
                    con.Close();
                    ddlsupplier2.Items.Insert(0, "Select Supplier");
                    //ddlsubbrand1.Text = ddlsubbrand.SelectedValue;
                }


            }





            using (SqlConnection con = new SqlConnection(cs))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT SuplierName ,SuplierId  FROM SuplierMaster  order by  SuplierName  "))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    ddlsupplier3.DataSource = cmd.ExecuteReader();
                    ddlsupplier3.DataTextField = "SuplierName";
                    ddlsupplier3.DataValueField = "SuplierName";
                    ddlsupplier3.DataBind();
                    con.Close();
                    ddlsupplier3.Items.Insert(0, "Select Supplier");

                    //ddlsubbrand1.Text = ddlsubbrand.SelectedValue;
                }


            }



            using (SqlConnection con = new SqlConnection(cs))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT SuplierName ,SuplierId  FROM SuplierMaster  order by  SuplierName  "))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    ddlsupplier4.DataSource = cmd.ExecuteReader();
                    ddlsupplier4.DataTextField = "SuplierName";
                    ddlsupplier4.DataValueField = "SuplierName";
                    ddlsupplier4.DataBind();
                    con.Close();
                    ddlsupplier4.Items.Insert(0, "Select Supplier");

                    //ddlsubbrand1.Text = ddlsubbrand.SelectedValue;
                }


            }





            using (SqlConnection con = new SqlConnection(cs))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT SuplierName ,SuplierId  FROM SuplierMaster  order by  SuplierName  "))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    ddlsupplier5.DataSource = cmd.ExecuteReader();
                    ddlsupplier5.DataTextField = "SuplierName";
                    ddlsupplier5.DataValueField = "SuplierName";
                    ddlsupplier5.DataBind();
                    con.Close();
                    ddlsupplier5.Items.Insert(0, "Select Supplier");

                    //ddlsubbrand1.Text = ddlsubbrand.SelectedValue;
                }


            }


            using (SqlConnection con = new SqlConnection(cs))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT SuplierName ,SuplierId  FROM SuplierMaster  order by  SuplierName  "))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    ddlsupplier6.DataSource = cmd.ExecuteReader();
                    ddlsupplier6.DataTextField = "SuplierName";
                    ddlsupplier6.DataValueField = "SuplierName";
                    ddlsupplier6.DataBind();
                    con.Close();
                    ddlsupplier6.Items.Insert(0, "Select Supplier");

                    //ddlsubbrand1.Text = ddlsubbrand.SelectedValue;
                }


            }






        }
    }

    protected void drpindentno_SelectedIndexChanged(object sender, EventArgs e)
    {

        string abc = drpindentno.SelectedValue;
        string[] tokens = abc.Split('=');

        int projectid = Convert.ToInt32(db.getDb_Value("select  IsCostCentre  from RequisitionCafeteria  where RequisitionCafeId = '" + drpindentno.SelectedValue + "'"));

        string projectno = db.getDbstatus_Value("select ProjectName from Project_master where id='" + projectid + "' ");
        lblenquiryno.Text = "ENQ" + "/" + projectno + "/" + "00" + ENNO.ToString();
        GrdRequisition.DataSource = db.Displaygrid(" select IM.ItemName,     ID.ItemDesc,   RCD.Qty as Qty from RequisitionCafeDtls RCD  inner join ItemMaster IM on RCD.ItemId=IM.ItemId left join ItemCategory IC on IC.CategoryId=IM.CategoryId left join SubCategory SC on SC.SubCategoryId=IM.SubcategoryId left join ItemDetails ID on ID.ItemDetailsId=RCD.ItemDetailsId left join UnitConversionDtls UCD on UCD.UnitConvDtlsId=RCD.UnitConvDtlsId Left join StockLocation SL on IM.StockLocationID=SL.StockLocationID left join TemplateDetails TM on RCD.TemplateID=TM.TemplateDtlsID left join SuplierMaster SM on RCD.VendorId=SM.SuplierId left Join PriorityMaster PM on RCD.PriorityID=PM.PriorityID left Join UnitMaster UM on RCD.UnitConvDtlsId=UM.UnitId  where   RCD.RequisitionCafeId='" + drpindentno.SelectedValue + "'");
        GrdRequisition.DataBind();


    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {

        db.insert("insert into enquirymaster values('" + lblenquiryno.Text + "' ,'" + drpindentno.SelectedItem.ToString() + "' ,'" + drpcompany.SelectedValue + "' ,'" + txtdate.Text + "' ,'" + txtnarration.Text + "' ,'" + lblTotalAmt.Text + "' ,'" + Session["UserName"].ToString() + "' ,'" + Session["mob"].ToString() + "')");


        foreach (GridViewRow gvrow in GrdRequisition.Rows)
        {
            TextBox txtqty = gvrow.FindControl("txtqty") as TextBox;
            DropDownList ddlsupplier = gvrow.FindControl("ddlsupplier") as DropDownList;
            DropDownList ddlsupplier2 = gvrow.FindControl("ddlsupplier2") as DropDownList;
            DropDownList ddlsupplier3 = gvrow.FindControl("ddlsupplier3") as DropDownList;
            DropDownList ddlsupplier4 = gvrow.FindControl("ddlsupplier4") as DropDownList;

            DropDownList ddlsupplier5 = gvrow.FindControl("ddlsupplier5") as DropDownList;
            DropDownList ddlsupplier6 = gvrow.FindControl("ddlsupplier6") as DropDownList;
            TextBox txtremark = gvrow.FindControl("txtremark") as TextBox;
            TextBox txtitem = gvrow.FindControl("TxtItemName") as TextBox;
            TextBox txtitemname = gvrow.FindControl("txtParticulars") as TextBox;
            if (ddlsupplier.SelectedItem.Text != "Select Supplier")
            {
                db.insert("insert into  Enquirydetails values( '" + txtitemname.Text + "','" + txtitem.Text + "' , '" + txtqty.Text + "' ,'" + ddlsupplier.SelectedItem.Text + "'       ,   '" + txtremark.Text + "' ,'" + "" + "' , '" + lblenquiryno.Text + "','" + drpindentno.SelectedItem.ToString() + "' ,'" + "open" + "')");
            }
            if (ddlsupplier2.SelectedItem.Text != "Select Supplier")
            {
                db.insert("insert into  Enquirydetails values( '" + txtitemname.Text + "','" + txtitem.Text + "' , '" + txtqty.Text + "' ,'" + ddlsupplier2.SelectedItem.Text + "','" + txtremark.Text + "' ,'" + "" + "' , '" + lblenquiryno.Text + "','" + drpindentno.SelectedItem.ToString() + "' ,'" + "open" + "')");
            }

            if (ddlsupplier3.SelectedItem.Text != "Select Supplier")
            {
                db.insert("insert into  Enquirydetails values( '" + txtitemname.Text + "','" + txtitem.Text + "' , '" + txtqty.Text + "' ,'" + ddlsupplier3.SelectedItem.Text + "','" + txtremark.Text + "' ,'" + "" + "' , '" + lblenquiryno.Text + "','" + drpindentno.SelectedItem.ToString() + "' ,'" + "open" + "')");
            }
            if (ddlsupplier4.SelectedItem.Text != "Select Supplier")
            {

                db.insert("insert into  Enquirydetails values( '" + txtitemname.Text + "','" + txtitem.Text + "' , '" + txtqty.Text + "' ,'" + ddlsupplier4.SelectedItem.Text + "','" + txtremark.Text + "' ,'" + "" + "' , '" + lblenquiryno.Text + "','" + drpindentno.SelectedItem.ToString() + "' ,'" + "open" + "')");
            }

            if (ddlsupplier5.SelectedItem.Text != "Select Supplier")
            {
                db.insert("insert into  Enquirydetails values( '" + txtitemname.Text + "','" + txtitem.Text + "' , '" + txtqty.Text + "' ,'" + ddlsupplier5.SelectedItem.Text + "','" + txtremark.Text + "' ,'" + "" + "' , '" + lblenquiryno.Text + "','" + drpindentno.SelectedItem.ToString() + "' ,'" + "open" + "')");
            }
            if (ddlsupplier6.SelectedItem.Text != "Select Supplier")
            {
                db.insert("insert into  Enquirydetails values( '" + txtitemname.Text + "','" + txtitem.Text + "' , '" + txtqty.Text + "' ,'" + ddlsupplier6.SelectedItem.Text + "','" + txtremark.Text + "' ,'" + "" + "' , '" + lblenquiryno.Text + "','" + drpindentno.SelectedItem.ToString() + "' ,'" + "open" + "')");
            }
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Enquiry Genrated Successfully ');", true);

            GrdRequisition.DataSource = "";
            GrdRequisition.DataBind();

            ReportGrid.DataSource = db.Displaygrid("select   distinct(Enquirydetails.supplier) , (enquirymaster.id) as #,  Enquirydetails.reamrk , enquirymaster.date , enquirymaster.enno from Enquirydetails  inner join enquirymaster   on enquirymaster.enno=Enquirydetails.enno  order by enquirymaster.enno  desc ");
            ReportGrid.DataBind();

        }

    }

    protected void ReportGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ReportGrid.PageIndex = e.NewPageIndex;
    }




    protected void ReportGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {

                case ("Delete"):
                    {

                        ViewState["DeleteID"] = Convert.ToInt32(e.CommandArgument);

                        string enq = db.getDbstatus_Value("select enno from enquirymaster where id='" + ViewState["DeleteID"] + "' ");
                        string supplier = db.getDbstatus_Value("select  supplier from  Enquirydetails where enno='" + enq + "'");
                        db.insert(" delete  Enquirydetails where supplier='" + supplier + "' and  enno='" + enq + "'");
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Record Deleted Successfully ');", true);
                    }
                    break;






                case ("Select"):
                    {

                        string enq = db.getDbstatus_Value("select enno from enquirymaster where id='" + Convert.ToInt32(e.CommandArgument) + "' ");
                        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                        int rowIndex = gvr.RowIndex;



                        lblenquiryno.Text = enq.ToString();


                        // int rowIndex = Convert.ToInt32(e.CommandArgument);


                        GridViewRow row = ReportGrid.Rows[rowIndex];



                        //Fetch value of Country
                        string supplier = ReportGrid.Rows[rowIndex].Cells[4].Text;


                        GrdRequisition.DataSource = db.Displaygrid("select  itemname as Itemname , particulars  as ItemDesc , qty as Qty, reamrk as Remark  from  Enquirydetails   where   enno='" + enq + "' and  supplier='" + supplier + "' ");
                        GrdRequisition.DataBind();
                        DataSet ds = new DataSet();
                        ds = db.dgv_display("select  itemname as Itemname , particulars  as ItemDesc , qty as Qty, reamrk as Remark  ,supplier from  Enquirydetails   where   enno='" + enq + "' and  supplier='" + supplier + "' ");
                        for (int g = 0; g < ds.Tables[0].Rows.Count; g++)
                        {

                            DropDownList ddlItemDescription = GrdRequisition.Rows[g].FindControl("ddlsupplier") as DropDownList;

                            ddlItemDescription.SelectedValue = ds.Tables[0].Rows[g]["supplier"].ToString();


                        }
                    }
                    break;



                case ("Print"):
                    {
                        DataTable dt = new DataTable();
                        ViewState["printid"] = Convert.ToInt32(e.CommandArgument);






                        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                        int rowIndex = gvr.RowIndex;






                        // int rowIndex = Convert.ToInt32(e.CommandArgument);


                        GridViewRow row = ReportGrid.Rows[rowIndex];



                        //Fetch value of Country
                        string supplier = ReportGrid.Rows[rowIndex].Cells[4].Text;














                        string enq = db.getDbstatus_Value("select enno from enquirymaster where id='" + ViewState["printid"] + "' ");

                        grdprint.DataSource = db.Displaygrid("select  itemname as Itemname , particulars  as Particular , qty as Qty, reamrk as Remark from  Enquirydetails   where   enno='" + enq + "' and  supplier='" + supplier + "' ");
                        grdprint.DataBind();
                        lblenno.Text = enq.ToString();
                        lblsuppadd.Text = db.getDbstatus_Value("select Address from SuplierMaster where SuplierName='" + supplier + "' ");
                        lblsuppcontact.Text = db.getDbstatus_Value("select MobileNo from SuplierMaster where SuplierName='" + supplier + "' ");
                        lblSuppname.Text = supplier.ToString();
                        image.ImageUrl = db.getDbstatus_Value("select  CLogo from  CompanyMaster where  CompanyId='" + "1" + "'");
                        lblcontactperson.Text = db.getDbstatus_Value("select  contactname from enquirymaster where  id='" + ViewState["printid"] + "' ");
                        lblcontactno.Text = db.getDbstatus_Value("select  contctmob from enquirymaster where  id='" + ViewState["printid"] + "' ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPanel", "PrintPanel();", true);
                    }
                    break;

            }
        }
        catch (Exception ex)
        {

        }
    }





    protected void grdprint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[3].Attributes["width"] = "70px";
        e.Row.Cells[2].Attributes["width"] = "70px";
        e.Row.Cells[1].Attributes["width"] = "70px";
        e.Row.Cells[0].Attributes["width"] = "70px";
    }

    protected void btnpopup_Click(object sender, EventArgs e)
    {

        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;


        TextBox abc = gvr.FindControl("txtParticulars") as TextBox;


        string xyx = abc.ToString();




    }

    protected void GrdRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdRequisition.Rows[rowIndex];

            //Fetch value of Name.
            string name = (row.FindControl("txtParticulars") as TextBox).Text;

            float itemid = db.getDb_Value("select   ItemId  from  ItemMaster where  ItemName='" + name + "'");
            DataTable dt = new DataTable();
            dt = db.Displaygrid("select  top 3 SuplierMaster.SuplierName from  PurchaseOrder inner join PurchaseOrdDtls  on  PurchaseOrder.POId=PurchaseOrdDtls.POId   inner join SuplierMaster on  SuplierMaster.SuplierId=PurchaseOrder.SuplierId where  PurchaseOrdDtls.ItemId='" + itemid + "'");
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            string lastsupplier = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lastsupplier = lastsupplier + "\n " + ds.Tables[0].Rows[i]["SuplierName"].ToString();
            }

            obj_Comman.ShowPopUpMsg("Last 3 Supplier For Item -\n" + lastsupplier, this.Page);

            // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Last 3 Supplier : " + lastsupplier + "\\nCountry: " + "a" + "');", true);
        }


    }


    public void ShowPopUpMsg(string msg, Page oPage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(oPage, this.GetType(), "showalert", sb.ToString(), true);
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {






       //foreach (GridViewRow gvrow in GrdRequisition.Rows)
       // {
            
       //     DropDownList ddlsupplier = gvrow.FindControl("ddlsupplier") as DropDownList;
            
       //     if (ddlsupplier.SelectedItem.Text != "Select Supplier")
       //     {
       //         db.insert("update   Enquirydetails set   supplier='"+ ddlsupplier.SelectedItem.Text + "' where  enno='"+ lblenquiryno.Text + "'");
       //     }
          
       //     System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Enquiry Genrated Successfully ');", true);

       //     GrdRequisition.DataSource = "";
       //     GrdRequisition.DataBind();

       //     ReportGrid.DataSource = db.Displaygrid("select   distinct(Enquirydetails.supplier) , (enquirymaster.id) as #,  Enquirydetails.reamrk , enquirymaster.date , enquirymaster.enno from Enquirydetails  inner join enquirymaster   on enquirymaster.enno=Enquirydetails.enno  order by enquirymaster.enno  desc ");
       //     ReportGrid.DataBind();


       // }
       // db.insert("update  enquirymaster set  indentno='" + drpindentno.SelectedItem.ToString() + "' ,    companyid='" + drpcompany.SelectedValue + "' , date='" + txtdate.Text + "' , naration='" + txtnarration.Text + "' ,totalamount='" + lblTotalAmt.Text + "'  where enno='" + lblenquiryno.Text + "'");
    }
}