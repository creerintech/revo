using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transactions_Estimate : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["MayurInventory"].ConnectionString;
    database db = new database();
    // SqlConnection cn=new SqlConnection(Configuration.)
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MayurInventory"].ConnectionString);
    int id = 0;
    int estmateid;
    Label estid = new Label();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //gvCustomers.DataSource = GetData("select top 10 * from ItemDetails");
            //gvCustomers.DataBind();
            bindparty();
            bindsupplier();

            estmateid = Convert.ToInt32(db.getDb_Value("select max (estmateno) from  addrowestimate "));
            estmateid++;

            est.Text = estmateid.ToString();

            ReportGrid.DataSource = db.Displaygrid("select indentno as EST_No, indentno as #, party as Project from  estimatemaster ");
            ReportGrid.DataBind();

        }

    }

    void bindparty()
    {
        drptitle.DataSource = db.Displaygrid("Select TemplateName, TemplateID   from TemplateMaster Where IsDeleted=0  ");

        drptitle.DataTextField = "TemplateName";
        drptitle.DataValueField = "TemplateID";
        drptitle.DataBind();
        drptitle.Items.Insert(0, "Select Title");
    }

    void bindsupplier()
    {
        using (SqlConnection con = new SqlConnection(cs))
        {

            using (SqlCommand cmd = new SqlCommand("SELECT ProjectName ,id  FROM Project_master  "))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();

                drpparty.DataSource = cmd.ExecuteReader();
                drpparty.DataTextField = "ProjectName";
                drpparty.DataValueField = "id";
                drpparty.DataBind();
                con.Close();
                drpparty.Items.Insert(0, "Select Project");

                //ddlsubbrand1.Text = ddlsubbrand.SelectedValue;
            }



        }
    }

    protected void drptitle_SelectedIndexChanged(object sender, EventArgs e)
    {

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
            string customerId = gvCustomers.DataKeys[row.RowIndex].Value.ToString();
            GridView gvOrders = row.FindControl("gvOrders") as GridView;







            BindOrders(gvOrders);
            bindtotak();
        }
        else
        {
            row.FindControl("pnlOrders").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";
        }
    }
    private void BindOrders(GridView gvOrders)
    {


    }
    private static DataTable GetData(string query)
    {
        string constr = ConfigurationManager.ConnectionStrings["MayurInventory"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    protected void Show_Hide_ProductsGrid(object sender, EventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlProducts").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            //     int orderId = Convert.ToInt32((row.NamingContainer as GridView).DataKeys[row.RowIndex].Value);
            GridView gvProducts = row.FindControl("gvProducts") as GridView;
            // BindProducts(orderId, gvProducts);
        }
        else
        {
            row.FindControl("pnlProducts").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";
        }
    }

    protected void OnProductsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView gvProducts = (sender as GridView);
        //gvProducts.PageIndex = e.NewPageIndex;
        //BindProducts(int.Parse(gvProducts.ToolTip), gvProducts);
    }
    protected void OnOrdersGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gvOrders = (sender as GridView);
        gvOrders.PageIndex = e.NewPageIndex;
        BindOrders(gvOrders);
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        gvCustomers.DataSource = db.Displaygrid("Select TemplateName, TemplateID   from TemplateMaster Where IsDeleted=0  and TemplateID='" + drptitle.SelectedValue + "'   ");
        gvCustomers.DataBind();
    }

    protected void btnadd_Click1(object sender, EventArgs e)
    {
        DataTable Ds = db.Displaygrid("Select TemplateName, TemplateID   from TemplateMaster Where IsDeleted=0  and TemplateID='" + drptitle.SelectedValue + "'   ");
        // gvCustomers.DataBind();

        //ViewState["ItemsList"] = db.Displaygrid("Select TemplateName, TemplateID   from TemplateMaster Where IsDeleted=0  and TemplateID='" + drptitle.SelectedValue + "'  ");
        //DataTable dttable = new DataTable();
        //dttable = (DataTable)ViewState["ItemsList"];
        //gvCustomers.DataSource = dttable;
        //gvCustomers.DataBind();


        SqlCommand cmd1 = new SqlCommand("  Select TemplateName, TemplateID   from TemplateMaster Where IsDeleted=0  and TemplateID='" + drptitle.SelectedValue + "'", cn);
        SqlDataAdapter adp1 = new SqlDataAdapter();
        DataSet ds1 = new DataSet();
        adp1.SelectCommand = cmd1;
        adp1.Fill(ds1);

        //  DataTable dt = new DataTable();
        //  dt.Columns.Add("TemplateName");
        ////  dt.Columns.Add("name");
        //  foreach (GridViewRow gvRow in gvCustomers.Rows)
        //  {
        //      DataRow dr = dt.NewRow();
        //      dr["TemplateName"] = ((Label)gvRow.FindControl("lblSno")).Text;
        //      //dr["name"] = ((Label)gvRow.FindControl("txtName")).Text;
        //      dt.Rows.Add(dr);
        //  }

        //  DataRow dr1 = dt.NewRow();
        //  dr1["TemplateName"] = "";
        //  dr1["name"] = "";
        //  dt.Rows.Add(dr1);

        //  gvCustomers.DataSource = dt;
        //  gvCustomers.DataBind();

        db.insert("insert into addrowestimate values('" + est.Text + "' ,'" + ds1.Tables[0].Rows[0]["TemplateName"].ToString() + "' ,'" + txtitemqty.Text + "' ,'" + ds1.Tables[0].Rows[0]["TemplateID"].ToString() + "')");
        gvCustomers.DataSource = db.Displaygrid("select *  from  addrowestimate  where  estmateno='" + est.Text + "' ");
        gvCustomers.DataBind();


    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string customerId = gvCustomers.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;






            gvOrders.DataSource = GetData(string.Format("select distinct TD.ItemID as [#],IM.ItemName,ISNULL(ID.ItemDesc,'') as ItemDesc  ,ID.OpeningStock as AvlQty ,TD.Qty ,PurchaseOrdDtls.Rate from TemplateMaster TM INNER JOIN TemplateDetails TD ON TM.TemplateID=TD.TemplateID LEFT JOIN ItemMaster IM ON TD.ItemID=IM.ItemId LEFT JOIN ItemDetails ID ON TD.ItemID=ID.ItemId AND TD.ItemDtlsID=ID.ItemDetailsId left join  PurchaseOrdDtls on TD.ItemID=PurchaseOrdDtls.ItemId    where TM.IsDeleted=0 and TM.TemplateID='" + customerId + "' Order By IM.ItemName "));
            gvOrders.DataBind();



            CheckBox chkcheck = gvOrders.HeaderRow.Cells[0].FindControl("chkboxSelectAll") as CheckBox;


            foreach (GridViewRow gvrow in gvOrders.Rows)
            {




               // CheckBox chkcheck = (CheckBox)gvrow.FindControl("chkboxSelectAll");



                if (gvrow.RowType == DataControlRowType.DataRow)
                {


                    CheckBox chk = (CheckBox)gvrow.FindControl("chkbox");
                    {

                        if (chkcheck.Checked == true)
                        {
                            chk.Checked = true;
                        }
                        else
                        {
                            chk.Checked = false;
                        }
                        if (chk != null && chk.Checked)
                        {

                            TextBox txtreqqty = (TextBox)gvrow.FindControl("txtreqqty");
                            TextBox txtavlqty = (TextBox)gvrow.FindControl("txtavlqty");
                            TextBox ttxprocuredqty = (TextBox)gvrow.FindControl("ttxprocuredqty");
                            TextBox txtamount = (gvrow.FindControl("txtamount") as TextBox);
                            TextBox txttodaysvalue = (TextBox)gvrow.FindControl("txttodaysvalue");
                            TextBox txtllp = (TextBox)gvrow.FindControl("txtllp");
                            TextBox txtamount1 = (TextBox)gvrow.FindControl("txtamount1");

                            double todaysvalue = 0;
                            if (txttodaysvalue.Text != "")
                            {
                                todaysvalue = Convert.ToDouble(txttodaysvalue.Text);
                            }
                            else
                            {
                                todaysvalue = 0;
                            }
                            double req = Convert.ToDouble(txtreqqty.Text);
                            double avl = 0;
                            if (txtavlqty.Text != "")
                            {
                                avl = Convert.ToDouble(txtavlqty.Text);
                            }
                            else
                                avl = 0;

                            if (req < avl)
                            {
                                ttxprocuredqty.Text = "0";
                                txtamount.Text = "0";
                            }

                            else
                            {

                                if (avl == 0)
                                {

                                    ttxprocuredqty.Text = req.ToString();
                                    int a = Convert.ToInt32(ttxprocuredqty.Text);
                                    Double c = a * todaysvalue;

                                    txtamount.Text = c.ToString();
                                }
                                else
                                {
                                    ttxprocuredqty.Text = (avl - req).ToString();
                                    int a = Convert.ToInt32(ttxprocuredqty.Text);
                                    txtamount.Text = (a * todaysvalue).ToString();
                                }
                            }

                            txtamount1.Text = (req * todaysvalue).ToString();

                        }
                    }
                }














            }
        }
    }

    protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void ttxprocuredqty_TextChanged(object sender, EventArgs e)
    {
        TextBox ttxprocuredqty = (sender as TextBox);
        GridViewRow row = (ttxprocuredqty.NamingContainer as GridViewRow);

        GridView gvorder = (ttxprocuredqty.NamingContainer as GridView);
        TextBox txtamount = (row.FindControl("txtamount") as TextBox);
        TextBox txtamount1 = (row.FindControl("txtamount1") as TextBox);
        TextBox txttodaysvalue = (row.FindControl("txttodaysvalue") as TextBox);
        TextBox txtreqqty = (row.FindControl("txtreqqty") as TextBox);
        txtamount.Text = (int.Parse(ttxprocuredqty.Text) * int.Parse(txttodaysvalue.Text)).ToString();
        txtamount1.Text = (int.Parse(txtreqqty.Text) * int.Parse(txttodaysvalue.Text)).ToString();



        bindtotak();










    }

    void bindtotak()
    {
        int num = 0;
        int num1 = 0;
        for (int i = 0; i < this.gvCustomers.Rows.Count; i++)
        {
            GridView inner = this.gvCustomers.Rows[i].FindControl("gvOrders") as GridView;



            foreach (GridViewRow gvrow in inner.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkbox");
                {

                    if (chk != null && chk.Checked)
                    {

                        TextBox txtamount = gvrow.FindControl("txtamount") as TextBox;
                        if (txtamount.Text != "")
                        {
                            num += Convert.ToInt32(txtamount.Text);
                        }
                    }
                }
            }
        }


        TextBox1.Text = num.ToString();



        for (int i = 0; i < this.gvCustomers.Rows.Count; i++)
        {
            GridView inner = this.gvCustomers.Rows[i].FindControl("gvOrders") as GridView;
            foreach (GridViewRow gvrow in inner.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkbox");
                {

                    if (chk != null && chk.Checked)
                    {
                        TextBox txtamount = gvrow.FindControl("txtamount1") as TextBox;
                        if (txtamount.Text != "")
                        {
                            num1 += Convert.ToInt32(txtamount.Text);
                        }
                    }

                }
            }
        }
        txtSubTotal.Text = num1.ToString();


        txtDisCountSub.Text = (num1 - num).ToString();


    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < this.gvCustomers.Rows.Count; i++)
        {

            string temaplatename = gvCustomers.Rows[i].Cells[1].Text;

            string qty = gvCustomers.Rows[i].Cells[2].Text;
            GridView inner = this.gvCustomers.Rows[i].FindControl("gvOrders") as GridView;






            foreach (GridViewRow gvrow in inner.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkbox");
                {

                    if (chk != null && chk.Checked)
                    {
                        Label TXTITEMNAME = gvrow.FindControl("TXTITEMNAME") as Label;
                        TextBox txtreqqty = gvrow.FindControl("txtreqqty") as TextBox;
                        TextBox txtllp = gvrow.FindControl("txtllp") as TextBox;

                        TextBox txttodaysvalue = gvrow.FindControl("txttodaysvalue") as TextBox;
                        TextBox txtavlqty = gvrow.FindControl("txtavlqty") as TextBox;
                        TextBox ttxprocuredqty = gvrow.FindControl("ttxprocuredqty") as TextBox;
                        TextBox txtamount = gvrow.FindControl("txtamount") as TextBox;



                        db.insert("insert into Estimatedetails values('" + est.Text + "' ,'" + temaplatename.ToString() + "' ,'" + qty.ToString() + "' , '" + TXTITEMNAME.Text + "' ,'" + txtreqqty.Text + "' ,'" + txtllp.Text + "' ,'" + txttodaysvalue.Text + "' ,'" + txtavlqty.Text + "' ,'" + ttxprocuredqty.Text + "' ,'" + txtamount.Text + "')");


                    }
                }

            }
        }
        db.insert("insert into estimatemaster values('" + est.Text + "' ,'" + drpparty.SelectedItem.ToString() + "' ,'" + txtSubTotal.Text + "' ,'" + txtDisCountSub.Text + "' ,'" + TextBox1.Text + "' ,'" + TextBox2.Text + "' ,'" + "Generated" + "')");


        // System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Record Save Sucessfully ');", true);
        gvCustomers.DataSource = "";
        gvCustomers.DataBind();

        //    db.insert("truncate table addrowestimate");

        ReportGrid.DataSource = db.Displaygrid("select indentno as EST_No, indentno as #, party as Project from  estimatemaster ");
        ReportGrid.DataBind();

        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Save Succesfully ');", true);
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        lblparty.Text = drpparty.SelectedItem.ToString();
        lbldate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        lblest.Text = "EST" + est.Text.ToString();
        lblemail.Text = "";
        lbltotalestmate.Text = txtSubTotal.Text;
        lbltobeprocured.Text = TextBox1.Text;
        lblavlqty.Text = txtDisCountSub.Text;
        lblorderbookedat.Text = TextBox2.Text;
        grdprint.DataSource = db.Displaygrid("select  [particular] as Particular, [qty] as Qty,[item] as Item,[reqqty] as REQ_QTY, [llp] as    LLP ,[todaysvalue] as TodaysValue, [avlqry] as Avl_Qty,[tobeprocured] as ToBe_Procured,[amount]  as Amount  from Estimatedetails   where indentno='" + est.Text + "' ");
        grdprint.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPanel", "PrintPanel();", true);
        // Page.ClientScript.RegisterStartupScript(this.GetType(), "PrintPanel", "PrintPanel()", true);


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

                        db.insert(" delete  estimatemaster WHERE  indentno ='" + ViewState["DeleteID"] + "'");
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted Succesfully ');", true);
                    }
                    break;


                case ("Print"):
                    {
                        DataTable dt = new DataTable();
                        ViewState["printid"] = Convert.ToInt32(e.CommandArgument);


                        image.ImageUrl = db.getDbstatus_Value("select  CLogo from  CompanyMaster where  CompanyId='" + "1" + "'");


                        lblproject.Text = db.getDbstatus_Value("SELECT   party FROM estimatemaster WHERE  indentno ='" + ViewState["printid"] + "' ");
                        Label4.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        lblest.Text = "EST" + ViewState["printid"].ToString();
                        lblemail.Text = "";
                        lbltotalestimate.Text = db.getDb_Value("SELECT   amount FROM estimatemaster WHERE  indentno ='" + ViewState["printid"] + "'").ToString();
                        lbltobeprocured1.Text = db.getDb_Value("SELECT   TObeProcured FROM estimatemaster WHERE  indentno ='" + ViewState["printid"] + "'").ToString();
                        lblavlqty1.Text = db.getDb_Value("SELECT   AvlQty FROM estimatemaster WHERE  indentno ='" + ViewState["printid"] + "'").ToString();
                        lblorderbookedat1.Text = db.getDb_Value("SELECT   OrderBookedAt FROM estimatemaster WHERE  indentno ='" + ViewState["printid"] + "'").ToString();


                        GridView1.DataSource = db.Displaygrid("select  [item] as Item,[reqqty] as REQ_QTY, [llp] as    LLP ,[todaysvalue] as TodaysValue, [avlqry] as Avl_Qty,[tobeprocured] as ToBe_Procured,[amount]  as Amount  from Estimatedetails   where indentno='" + ViewState["printid"] + "' ");
                        GridView1.DataBind();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPanel", "PrintPanel();", true);
                    }
                    break;

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ReportGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ReportGrid.PageIndex = e.NewPageIndex;
    }

    protected void btngetamount_Click(object sender, EventArgs e)
    {
        //  bindtotak();
    }

    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {


        for (int i = 0; i < this.gvCustomers.Rows.Count; i++)
        {



            string qty = gvCustomers.Rows[i].Cells[2].Text;
            GridView inner = this.gvCustomers.Rows[i].FindControl("gvOrders") as GridView;
            CheckBox chkcheck1 = inner.HeaderRow.Cells[0].FindControl("chkboxSelectAll") as CheckBox;
            foreach (GridViewRow row in inner.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("chkbox");


                if (chkcheck1.Checked == true)
                {
                    chkcheck.Checked = true;
                }
                else
                {
                    chkcheck.Checked = false;
                }
            }
        }
        bindtotak();

    }

    protected void chkbox_CheckedChanged(object sender, EventArgs e)
    {
        bindtotak();
    }
}