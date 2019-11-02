using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

using MayurInventory.Utility;
using System.Text;

public partial class Transactions_conversion : System.Web.UI.Page
{
    CommanFunction obj_Comman = new CommanFunction();
    database db = new database();
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MayurInventory"].ConnectionString);
    string strheadertext2 = "";
    string strheadertext1 = "";
    string strheadertext3 = "";
    string strheadertext4 = "";
    string strheadertext5 = "";
    string strheadertext6 = "";
    int chk = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            chk = 0;
            drpenquiry.DataSource = db.Displaygrid("select distinct(enqid)  from Supplierenquiryitem  where enqid NOT IN ( Select enquiry from Conversionpo)   ");
            drpenquiry.DataTextField = "enqid";
            drpenquiry.DataValueField = "enqid";
            drpenquiry.DataBind();
            drpenquiry.Items.Insert(0, "-Select Enquiry -");

            drpproject.DataSource = db.Displaygrid("SELECT ProjectName ,id  FROM Project_master     ");
            drpproject.DataTextField = "ProjectName";
            drpproject.DataValueField = "ProjectName";
            drpproject.DataBind();
            drpproject.Items.Insert(0, "-Select Project -");
            ReportGrid.DataSource = db.Displaygrid("select distinct project ,enquiry  , enquiry as # ,status  as Status from   Conversionpo ");
            ReportGrid.DataBind();

            GETHIGHLITE();
        }
        else
        {
            chk = 0;
            grdreport.DataSource = db.Displaygrid("DECLARE @cols NVARCHAR(MAX)   SELECT     @cols = COALESCE(@cols + ',[' + (supplier  ) 				+ ']','[' + (supplier) + ']'  )  FROM    supplierenquirydetails	 	 where enqid='" + drpenquiry.SelectedValue + "'														    DECLARE @qry NVARCHAR(4000) SET       @qry = 'SELECT * FROM (select itemname as Itemname, particular as Particular, qty as Qty ,rate  ,supplier as Supplier  from  demosupplierenquiry     )emp    PIVOT (MAX(rate) FOR supplier IN (' + @cols + ')) AS stat' 		  exec(@qry)");
            grdreport.DataBind();

        }

    }
    public void GETHIGHLITE()
    {
        for (int i = 0; i < ReportGrid.Rows.Count; i++)
        {
            if ((ReportGrid.Rows[i].Cells[4].Text) == "Generated")
            {
                ReportGrid.Rows[i].BackColor = System.Drawing.Color.White;
                ReportGrid.Rows[i].ForeColor = System.Drawing.Color.Black;
            }
            if ((ReportGrid.Rows[i].Cells[4].Text) == "Approved")
            {
                ReportGrid.Rows[i].BackColor = System.Drawing.Color.Yellow;
                //ReportGrid.Rows[i].ForeColor = System.Drawing.Color.Black;
            }

            if ((ReportGrid.Rows[i].Cells[4].Text) == "Authorised")
            {
                ReportGrid.Rows[i].BackColor = System.Drawing.Color.Green;
               //ReportGrid.Rows[i].ForeColor = System.Drawing.Color.White;
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOP", "javascript:HidePOP();", true);
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPOPTERMS", "javascript:HidePOPTermsPOSupplier();", true);
    }
    protected void drpenquiry_SelectedIndexChanged(object sender, EventArgs e)
    {
        db.insert("truncate table demosupplierenquiry");
        SqlCommand cmd = new SqlCommand("  select Particulars, itemname, Qty ,rate,amount ,supplier ,enqid  from  Supplierenquiryitem  where enqid='" + drpenquiry.SelectedValue + "' ");
        cmd.Connection = con;

        con.Open();
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            SqlCommand com1 = new SqlCommand("insert into demosupplierenquiry  values(     '" + rdr["itemname"].ToString() + "'  ,'" + rdr["Particulars"].ToString() + "' ,'" + rdr["Qty"].ToString() + "' ,'" + rdr["amount"].ToString() + "' ,'" + rdr["enqid"].ToString() + "' ,'" + rdr["rate"].ToString() + "' ,'" + rdr["supplier"].ToString() + "')", con);

            com1.ExecuteNonQuery();



        }
        grdreport.DataSource = db.Displaygrid("DECLARE @cols NVARCHAR(MAX)   SELECT     @cols = COALESCE(@cols + ',[' + (supplier  ) 				+ ']','[' + (supplier) + ']'  )  FROM    supplierenquirydetails	 	 where enqid='" + drpenquiry.SelectedValue + "'														    DECLARE @qry NVARCHAR(4000) SET       @qry = 'SELECT * FROM (select itemname as Itemname, particular as Particular, qty as Qty ,rate  ,supplier as Supplier  from  demosupplierenquiry     )emp    PIVOT (MAX(rate) FOR supplier IN (' + @cols + ')) AS stat' 		  exec(@qry)");
        grdreport.DataBind();

        grdprint.DataSource = db.Displaygrid("DECLARE @cols NVARCHAR(MAX)   SELECT     @cols = COALESCE(@cols + ',[' + (supplier  ) 				+ ']','[' + (supplier) + ']'  )  FROM    supplierenquirydetails	 	 where enqid='" + drpenquiry.SelectedValue + "'														    DECLARE @qry NVARCHAR(4000) SET       @qry = 'SELECT * FROM (select itemname as Itemname, particular as Particular, qty as Qty ,rate  ,supplier  as Supplier  from  demosupplierenquiry     )emp    PIVOT (MAX(rate) FOR supplier IN (' + @cols + ')) AS stat' 		  exec(@qry)");
        grdprint.DataBind();
      //  lblenqno.Text = drpenquiry.SelectedItem.ToString();
          }
    void button1_Click(object sender, EventArgs e )
    {
        Button mb = (sender as Button);
        string suupname = mb.ID;


      string terms=  db.getDbstatus_Value("select  naration from  supplierenquirydetails where  enqid='" + drpenquiry.SelectedValue + "'  and  supplier='" + suupname + "'");

        obj_Comman.ShowPopUpMsg(" Terms and Conditions -\n " + terms, this.Page);
    }
    void button2_Click(object sender, EventArgs e)
    {
        Button mb = (sender as Button);
        string suupname = mb.ID;


        string terms = db.getDbstatus_Value("select  naration from  supplierenquirydetails where  enqid='" + drpenquiry.SelectedValue + "'  and  supplier='" + suupname + "'");

        obj_Comman.ShowPopUpMsg(" Terms and Conditions -\n " + terms, this.Page);
    }
    void button3_Click(object sender, EventArgs e)
    {
        Button mb = (sender as Button);
        string suupname = mb.ID;


        string terms = db.getDbstatus_Value("select  naration from  supplierenquirydetails where  enqid='" + drpenquiry.SelectedValue + "'  and  supplier='" + suupname + "'");

        obj_Comman.ShowPopUpMsg(" Terms and Conditions -\n " + terms, this.Page);
    }
    void button4_Click(object sender, EventArgs e)
    {
        Button mb = (sender as Button);
        string suupname = mb.ID;


        string terms = db.getDbstatus_Value("select  naration from  supplierenquirydetails where  enqid='" + drpenquiry.SelectedValue + "'  and  supplier='" + suupname + "'");

        obj_Comman.ShowPopUpMsg(" Terms and Conditions -\n " + terms, this.Page);
    }
    void button5_Click(object sender, EventArgs e)
    {
        Button mb = (sender as Button);
        string suupname = mb.ID;


        string terms = db.getDbstatus_Value("select  naration from  supplierenquirydetails where  enqid='" + drpenquiry.SelectedValue + "'  and  supplier='" + suupname + "'");

        obj_Comman.ShowPopUpMsg(" Terms and Conditions -\n " + terms, this.Page);
    }
    void button6_Click(object sender, EventArgs e)
    {
        Button mb = (sender as Button);
        string suupname = mb.ID;


        string terms = db.getDbstatus_Value("select  naration from  supplierenquirydetails where  enqid='" + drpenquiry.SelectedValue + "'  and  supplier='" + suupname + "'");

        obj_Comman.ShowPopUpMsg(" Terms and Conditions -\n " + terms, this.Page);
    }
    public void ShowPopUpMsg(string msg, Page oPage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(oPage, this.GetType(), "showalert", sb.ToString(), true);
    }
    protected void grdreport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    
        int temp = e.Row.Cells.Count;

        temp--;

      
        if(e.Row.RowType==DataControlRowType.Header)
        {
            if (temp >= 3)
            {
                Button btn1 = new Button();
                btn1.Text = e.Row.Cells[3].Text  +"         " + "View Terms ";
                btn1.ID = e.Row.Cells[3].Text;
                string id = e.Row.Cells[3].Text;
                btn1.Click += new EventHandler(button1_Click  );
                e.Row.Cells[3].Controls.Add(btn1);

            }
            if (temp >= 4)
            {

                Button btn2 = new Button();
                btn2.Text = e.Row.Cells[4].Text+ "         "+ "View Terms ";
                btn2.ID = e.Row.Cells[4].Text;
                 btn2.Click += new EventHandler(button2_Click);
                e.Row.Cells[4].Controls.Add(btn2);

            }

            if (temp >= 5)
            {
                Button btn3 = new Button();
                btn3.Text = e.Row.Cells[5].Text + "         " + "View Terms ";
                btn3.ID = e.Row.Cells[5].Text;
                  btn3.Click += new EventHandler(button3_Click);
                e.Row.Cells[5].Controls.Add(btn3);

            }
            if (temp >= 6)
            {

                Button btn4 = new Button();
                btn4.Text = e.Row.Cells[6].Text + "         " + "View Terms ";
                btn4.ID = e.Row.Cells[6].Text;
                 btn4.Click += new EventHandler(button4_Click);
                e.Row.Cells[6].Controls.Add(btn4);
            }

            if (temp >= 7)
            {
                Button btn5 = new Button();
                btn5.Text = e.Row.Cells[7].Text + "         " + "View Terms ";
                btn5.ID = e.Row.Cells[7].Text;
                 btn5.Click += new EventHandler(button5_Click);
                e.Row.Cells[7].Controls.Add(btn5);

            }
            if (temp >= 8)
            {


                Button btn6 = new Button();
                btn6.Text = e.Row.Cells[8].Text + "         " + "View Terms ";
                btn6.ID = e.Row.Cells[8].Text;
                btn6.Click += new EventHandler(button6_Click);

                e.Row.Cells[8].Controls.Add(btn6);
            }
        }





        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (temp >= 3)
            {



                strheadertext1 = grdreport.HeaderRow.Cells[3].Text;

                CheckBox cb1 = new CheckBox();
                // cb1.ID = chk.ToString();
                cb1.ID =  "cb1"+e.Row.Cells[1].Text; 
                cb1.Text = e.Row.Cells[3].Text;

                if (cb1.Text != "&nbsp;")
                {

                    e.Row.Cells[3].Controls.Add(cb1);
                }
                


            }





            if (temp >= 4)
            {
                strheadertext2 = grdreport.HeaderRow.Cells[4].Text;
                CheckBox cb2 = new CheckBox();
                cb2.ID = "cb2" + e.Row.Cells[1].Text;
                cb2.Text = e.Row.Cells[4].Text;
                if (cb2.Text != "&nbsp;")
                {
                    e.Row.Cells[4].Controls.Add(cb2);
                }
            }


            if (temp >= 5)
            {
                strheadertext3 = grdreport.HeaderRow.Cells[5].Text;
                CheckBox cb3 = new CheckBox();
                cb3.ID = "cb3" + e.Row.Cells[1].Text;
                cb3.Text = e.Row.Cells[5].Text;
                if (cb3.Text != "&nbsp;")
                {
                    e.Row.Cells[5].Controls.Add(cb3);
                }
            }

            if (temp >= 6)
            {
                strheadertext4 = grdreport.HeaderRow.Cells[6].Text;
                CheckBox cb4 = new CheckBox();
                cb4.ID = "cb4" + e.Row.Cells[1].Text;
                cb4.Text = e.Row.Cells[6].Text;
                if (cb4.Text != "&nbsp;")
                {
                    e.Row.Cells[6].Controls.Add(cb4);
                }
            }


            if (temp >= 7)
            {
                strheadertext5 = grdreport.HeaderRow.Cells[7].Text;
                CheckBox cb5 = new CheckBox();
                cb5.ID = "cb5" + e.Row.Cells[1].Text;
                cb5.Text = e.Row.Cells[7].Text;

                if (cb5.Text != "&nbsp;")
                {
                    e.Row.Cells[7].Controls.Add(cb5);
                }
            }

            if (temp >= 8)
            {
                strheadertext6 = grdreport.HeaderRow.Cells[8].Text;
                CheckBox cb6 = new CheckBox();
                cb6.ID = "cb6" + e.Row.Cells[1].Text;
                cb6.Text = e.Row.Cells[8].Text;
                if (cb6.Text != "&nbsp;")
                {
                    e.Row.Cells[8].Controls.Add(cb6);
                }
            }
            chk++;
        }

        

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {






        foreach (GridViewRow row in grdreport.Rows)
        {


            CheckBox checkbox1 = (CheckBox)row.FindControl("cb1"+row.Cells[1].Text);

            if (checkbox1 == null)
            {

            }
            else
            {
                if (checkbox1.Checked == true)
                {
                    string itemname = row.Cells[0].Text;
                    string particular = row.Cells[1].Text;
                    string qty = row.Cells[2].Text;
                    db.insert("insert into Conversionpo values('" + drpproject.SelectedValue + "' ,'" + drpenquiry.SelectedValue + "' ,'" + itemname + "' ,'" + particular + "' ,'" + qty + "' ,'" + checkbox1.Text + "' ,'" + strheadertext1 + "' ,'" + "Generated" + "' ,'"+"Pending"+"')");
                }
            }
            CheckBox checkbox2 = (CheckBox)row.FindControl("cb2" + row.Cells[1].Text);
            if (checkbox2 == null)
            {

            }
            else
            {
                if (checkbox2.Checked == true)
                {
                    string itemname = row.Cells[0].Text;
                    string particular = row.Cells[1].Text;
                    string qty = row.Cells[2].Text;
                    db.insert("insert into Conversionpo values('" + drpproject.SelectedValue + "' ,'" + drpenquiry.SelectedValue + "' ,'" + itemname + "' ,'" + particular + "' ,'" + qty + "' ,'" + checkbox2.Text + "' ,'" + strheadertext2 + "' ,'" + "Generated" + "' , '" + "Pending" + "')");
                }
            }
            CheckBox checkbox3 = (CheckBox)row.FindControl("cb3" + row.Cells[1].Text);



            if (checkbox3 == null)
            {

            }
            else
            {
                if (checkbox3.Checked == true)
                {
                    string itemname = row.Cells[0].Text;
                    string particular = row.Cells[1].Text;
                    string qty = row.Cells[2].Text;
                    db.insert("insert into Conversionpo values('" + drpproject.SelectedValue + "' ,'" + drpenquiry.SelectedValue + "' ,'" + itemname + "' ,'" + particular + "' ,'" + qty + "' ,'" + checkbox3.Text + "' ,'" + strheadertext3 + "' ,'" + "Generated" + "'  , '" + "Pending" + "')");
                }
            }
            CheckBox checkbox4 = (CheckBox)row.FindControl("cb4" + row.Cells[1].Text);
            if (checkbox4 == null)
            {

            }
            else
            {
                if (checkbox4.Checked == true)
                {
                    string itemname = row.Cells[0].Text;
                    string particular = row.Cells[1].Text;
                    string qty = row.Cells[2].Text;
                    db.insert("insert into Conversionpo values('" + drpproject.SelectedValue + "' ,'" + drpenquiry.SelectedValue + "' ,'" + itemname + "' ,'" + particular + "' ,'" + qty + "' ,'" + checkbox4.Text + "' ,'" + strheadertext4 + "' ,'" + "Generated" + "'  , '" + "Pending" + "')");
                }
            }
            CheckBox checkbox5 = (CheckBox)row.FindControl("cb5" + row.Cells[1].Text);

            if (checkbox5 == null)
            {

            }
            else
            {
                if (checkbox5.Checked == true)
                {
                    string itemname = row.Cells[0].Text;
                    string particular = row.Cells[1].Text;
                    string qty = row.Cells[2].Text;
                    db.insert("insert into Conversionpo values('" + drpproject.SelectedValue + "' ,'" + drpenquiry.SelectedValue + "' ,'" + itemname + "' ,'" + particular + "' ,'" + qty + "' ,'" + checkbox5.Text + "' ,'" + strheadertext5 + "' ,'" + "Generated" + "'  , '" + "Pending" + "')");
                }
            }

            CheckBox checkbox6 = (CheckBox)row.FindControl("cb6" + row.Cells[1].Text);
            if (checkbox6 == null)
            {

            }
            else
            {
                if (checkbox6.Checked == true)
                {
                    string itemname = row.Cells[0].Text;
                    string particular = row.Cells[1].Text;
                    string qty = row.Cells[2].Text;
                    db.insert("insert into Conversionpo values('" + drpproject.SelectedValue + "' ,'" + drpenquiry.SelectedValue + "' ,'" + itemname + "' ,'" + particular + "' ,'" + qty + "' ,'" + checkbox6.Text + "' ,'" + strheadertext6 + "' ,'"+ "Generated" + " '  , '" + "Pending" + "') ");
                }

            }
            



        }
        ReportGrid.DataSource = db.Displaygrid("select distinct project ,enquiry  , enquiry as #  , status as Status  from   Conversionpo ");
        ReportGrid.DataBind();

        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase Order   Succesfully ');", true);




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
                        ViewState["printid"] = Convert.ToString(e.CommandArgument);




                        db.insert("truncate table demosupplierenquiry");
                        SqlCommand cmd = new SqlCommand("  select Supplierenquiryitem.Particulars, Supplierenquiryitem.itemname, Supplierenquiryitem.Qty ,Supplierenquiryitem.rate, Supplierenquiryitem.amount ,Supplierenquiryitem.supplier ,Supplierenquiryitem.enqid  from  Supplierenquiryitem  where enqid='" + ViewState["printid"] + "' ");
                        cmd.Connection = con;

                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            SqlCommand com1 = new SqlCommand("insert into demosupplierenquiry  values(     '" + rdr["itemname"].ToString() + "'  ,'" + rdr["Particulars"].ToString() + "' ,'" + rdr["Qty"].ToString() + "' ,'" + rdr["amount"].ToString() + "' ,'" + rdr["enqid"].ToString() + "' ,'" + rdr["rate"].ToString() + "' ,'" + rdr["supplier"].ToString() + "')", con);

                            com1.ExecuteNonQuery();



                        }
                      

                        grdprint.DataSource = db.Displaygrid("DECLARE @cols NVARCHAR(MAX)   SELECT     @cols = COALESCE(@cols + ',[' + (supplier  ) 				+ ']','[' + (supplier) + ']'  )  FROM    supplierenquirydetails	 	 where enqid='" + ViewState["printid"] + "'														    DECLARE @qry NVARCHAR(4000) SET       @qry = 'SELECT * FROM (select itemname as Itemname, particular as Particular, qty as Qty ,rate  ,supplier  as Supplier  from  demosupplierenquiry     )emp    PIVOT (MAX(rate) FOR supplier IN (' + @cols + ')) AS stat' 		  exec(@qry)");
                        grdprint.DataBind();
                        lbdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        image.ImageUrl = db.getDbstatus_Value("select  CLogo from  CompanyMaster where  CompanyId='" + "1" + "'");
                     //   grdprint.DataSource = db.Displaygrid(" select  id as Id , project as Project ,enquiry Enquiry ,itemname as Itemname , particular as Particular ,qty as Qty ,rate as Rate ,supplier as Supplier from Conversionpo where enquiry='" + ViewState["printid"] + "'  ");
                      //  grdprint.DataBind();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPanel", "PrintPanel();", true);
                    }
                    break;

            }
        }
        catch (Exception ex)
        {

        }
    }


}















































