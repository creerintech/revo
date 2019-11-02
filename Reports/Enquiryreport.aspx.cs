using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Enquiryreport : System.Web.UI.Page
{

    database db = new database();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

            GrdReport.DataSource = db.Displaygrid("select   distinct(Enquirydetails.supplier) , (enquirymaster.id) as #,    Enquirydetails.reamrk , enquirymaster.date , enquirymaster.enno from Enquirydetails  inner join enquirymaster   on enquirymaster.enno=Enquirydetails.enno  order by enquirymaster.enno  desc");
            GrdReport.DataBind();
        }

    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        //GrdReport.DataSource = db.Displaygrid("select id as # , enno as Enquiry_No , companyid as Company_Name , date As Date  from enquirymaster  where date BETWEEN '" + txtFromDate.Text+"' AND '"+txtToDate.Text+"' ");
        //GrdReport.DataBind();

    }

    protected void GrdReport_SelectedIndexChanged(object sender, EventArgs e)
    {

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


                case ("Print"):
                    {
                        DataTable dt = new DataTable();
                        ViewState["printid"] = Convert.ToInt32(e.CommandArgument);






                        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                        int rowIndex = gvr.RowIndex;






                        // int rowIndex = Convert.ToInt32(e.CommandArgument);


                        GridViewRow row = GrdReport.Rows[rowIndex];



                        //Fetch value of Country
                        string supplier = GrdReport.Rows[rowIndex].Cells[1].Text;














                        string enq = db.getDbstatus_Value("select enno from enquirymaster where id='" + ViewState["printid"] + "' ");
                        //string supplier = db.getDbstatus_Value("select  supplier from  Enquirydetails where id='" + ViewState["printid"] + "'");
                        grdprint.DataSource = db.Displaygrid("select  itemname as Itemname , particulars  as Particular , qty as Qty, reamrk as Remark from  Enquirydetails   where   enno='" + enq + "' and  supplier='" + supplier + "' ");
                        grdprint.DataBind();
                        lblenno.Text = enq.ToString();
                        lblsuppadd.Text = db.getDbstatus_Value("select Address from SuplierMaster where SuplierName='" + supplier + "' ");
                        lblsuppcontact.Text = db.getDbstatus_Value("select MobileNo from SuplierMaster where SuplierName='" + supplier + "' ");
                        lblSuppname.Text = supplier.ToString();
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
}