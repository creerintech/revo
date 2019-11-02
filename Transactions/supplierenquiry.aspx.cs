using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transactions_supplierenquiry : System.Web.UI.Page
{
    int estmateid;
    Label estid = new Label();
    database db = new database();
    string cs = ConfigurationManager.ConnectionStrings["MayurInventory"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            if (Session["UserName"].ToString() != "" && Session["UserName"].ToString() != null)
            {
                txtdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                bindsupplier();
                lbldate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                estmateid = Convert.ToInt32(db.getDb_Value("select max (id) from  supplierenquirydetails "));
                estmateid++;

                est.Text = estmateid.ToString();
                ReportGrid.DataSource = db.Displaygrid(" select enqid as ENQ_No ,  id as #, supplier  as Supplier from supplierenquirydetails order by id desc");
                ReportGrid.DataBind();
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }








    }
    void bindsupplier()
    {
        drpsupplier.DataSource = db.Displaygrid("select distinct(supplier)  from  Enquirydetails  where status='" + "open" + "' order by supplier  ");

        drpsupplier.DataTextField = "supplier";
        drpsupplier.DataValueField = "supplier";
        drpsupplier.DataBind();
        drpsupplier.Items.Insert(0, "-Select Supplier -");
    }
    void bindenquiry()
    {
        drpenquiry.DataSource = db.Displaygrid("select distinct(enno)  from Enquirydetails where  supplier='" + drpsupplier.SelectedValue + "' and status='" + "open" + "'  ");
        drpenquiry.DataTextField = "enno";
        drpenquiry.DataValueField = "enno";
        drpenquiry.DataBind();
        drpenquiry.Items.Insert(0, "-Select Enquiry -");
    }

    protected void GrdRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtparti1 = (e.Row.FindControl("txtparti1") as TextBox);

           // string a = db.getDb_Value("select  TaxDtlsId from ItemMaster where ItemName='" + txtparti1.Text + "'").ToString();


            string a = db.getDb_Value("select TaxDetails.gst from TaxDetails inner join ItemMaster on ItemMaster.TaxDtlsId = TaxDetails.TaxDtlsId where ItemMaster.ItemName= '" + txtparti1.Text+"'").ToString();

            TextBox GrdtxtPerVAT= (e.Row.FindControl("GrdtxtPerVAT") as TextBox);
            TextBox igst = (e.Row.FindControl("GrdtxtIGSTPer") as TextBox);

            string supplierstate = db.getDb_Value("select  StateID from  SuplierMaster where  SuplierName ='" + drpsupplier.SelectedValue + "' ").ToString();
            if (supplierstate.ToString() == "13")
            {
                GrdtxtPerVAT.Text = a.ToString();
            }
            else
            {
                igst.Text = a.ToString();


            }
            
        }
    }

    protected void drpsupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindenquiry();
    }



    protected void drpenquiry_SelectedIndexChanged(object sender, EventArgs e)
    {
        GrdRequisition.DataSource = db.Displaygrid("      select Enquirydetails.*    from  Enquirydetails  where  Enquirydetails.enno  ='" + drpenquiry.SelectedValue + "' and Enquirydetails.supplier='" + drpsupplier.SelectedValue + "'");
        GrdRequisition.DataBind();

    }

    protected void txtrate_TextChanged(object sender, EventArgs e)
    {
        //TextBox txtQuantity = (sender as TextBox);
        //GridViewRow row = (txtQuantity.NamingContainer as GridViewRow);
      
        //TextBox txtgst = (row.FindControl("GrdtxtPerVAT") as TextBox);

        //TextBox txtcsgt = (row.FindControl("GrdtxtCGSTPer") as TextBox);
        //TextBox txtsgst = (row.FindControl("GrdtxtSGSTPer") as TextBox);

        //if(txtgst.Text=="18.00")
        //{
        //    //txtcsgt.Text = "9";
        //    //txtsgst.Text = "9";
        //    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "CalculateGrid(GrdRequisition)", true);
        //}
     

       

    }






    protected void BtnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in GrdRequisition.Rows)
        {
            TextBox txtparticular = (TextBox)gvrow.FindControl("TxtItemName");
            TextBox txtrate = (TextBox)gvrow.FindControl("txtrate");
            TextBox txtqty = (TextBox)gvrow.FindControl("txtqty");
            TextBox txtamount = (TextBox)gvrow.FindControl("txtamount");
            TextBox itemname = (TextBox)gvrow.FindControl("txtparti1");









            TextBox cgstper = (TextBox)gvrow.FindControl("GrdtxtCGSTPer");
            TextBox cgstamt = (TextBox)gvrow.FindControl("GrdtxtCGSTAmt");
            TextBox sgstper = (TextBox)gvrow.FindControl("GrdtxtSGSTPer");
            TextBox sgstamt = (TextBox)gvrow.FindControl("GrdtxtSGSTAmt");
            TextBox igstper = (TextBox)gvrow.FindControl("GrdtxtIGSTPer");

            TextBox igstamt = (TextBox)gvrow.FindControl("GrdtxtIGSTAmt");

            TextBox txtgst = (TextBox)gvrow.FindControl("GrdtxtPerVAT");






            // string particular = txtparticular.Text;

            double a = double.Parse(txtamount.Text);
            db.insert("insert into Supplierenquiryitem values( '" + est.Text + "' ,'" + drpenquiry.SelectedValue + "' , '" + itemname.Text + "','" + txtparticular.Text + " ' , '" + txtqty.Text + " ' ,'" + txtrate.Text + "' ,'" + Math.Round(a) + "'  ,'" + drpsupplier.SelectedValue + "' ,'"+cgstper.Text+"' ,'"+cgstamt.Text+"' ,'"+sgstper.Text+"' ,'"+sgstamt.Text+"' ,'"+igstper.Text+"' ,'"+igstamt.Text+"' ,'"+txtgst.Text+"')");
        }
        db.insert("insert into supplierenquirydetails values('" + est.Text + "','" + drpenquiry.SelectedValue + "' ,'" + txtdate.Text + "' ,'" + drpsupplier.SelectedValue + "' ,'" + txtSubTotal.Text + "' ,'" + 0 + "' ,'" + 0 + "' ,'" + 0 + "' ,'" + 0 + "' ,'" + txtvatper.Text + "' ,'" + txtSGSTPer.Text + "' ,'" + txtIGSTPer.Text + "' ,'" + txtvatamt.Text + "' ,'" + txtSGSTAmt.Text + "' ,'" + txtIGSTAmt.Text + "' ,'" + txtNetTotal.Text + "' ,'" + Session["UserName"].ToString() + "' ,'" + Session["mob"].ToString() + "' ,'"+txtnarration.Text+"')");

        db.insert("update Enquirydetails set status='" + "close" + "' where  supplier='" + drpsupplier.SelectedValue + "'");


        lblSuppname.Text = drpsupplier.SelectedItem.ToString();
        lblenno.Text = drpenquiry.SelectedItem.ToString();
       // lblnarration.Text = txtnarration.Text;
        lblsuppadd.Text = db.getDbstatus_Value("select Address from SuplierMaster where SuplierName='" + drpsupplier.SelectedValue + "' ");
        lblsuppcontact.Text = db.getDbstatus_Value("select MobileNo from SuplierMaster where SuplierName='" + drpsupplier.SelectedValue + "' ");
        clear();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintAftersave", "PrintAftersave();", true);
        GrdRequisition.DataSource = "";
        GrdRequisition.DataBind();
        ReportGrid.DataSource = db.Displaygrid(" select enqid as ENQ_No ,  id as #, supplier  as Supplier from supplierenquirydetails order by id desc");
        ReportGrid.DataBind();
        bindsupplier();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Save Succesfully ');", true);
        //  Response.Write(" ");


    }

    void clear()
    {
        txtdate.Text = "";
        //drpsupplier.SelectedValue = " 0";
        txtSubTotal.Text = "";

        //txtDisCountSub.Text = "";
        //txtFreight.Text = "";
        //txtLadUnload.Text = "";
        //txtAmount.Text = "";
        txtvatper.Text = "";
        txtSGSTPer.Text = "";
        txtIGSTPer.Text = "";
        txtIGSTPer.Text = "";
        txtvatamt.Text = "";
        txtSGSTAmt.Text = "";
        txtIGSTAmt.Text = "";
        txtNetTotal.Text = "";
        txtToalWithGST.Text = "";
        txtnarration.Text = "";
        

     //   lblTotalAmt.Text = "";
        //txtSpeDisPer.Text = "";
        //txtSpeDisAmt.Text = "";
        //txtWithSpecialAmt.Text = "";
        txtNetTotal.Text = "";
    }



    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        grdprint.DataSource = db.Displaygrid("select  Particulars , Qty , rate  as Rate, amount as Amount from Supplierenquiryitem where  enqid='" + drpenquiry.SelectedValue + "' ");
        grdprint.DataBind();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPanel", "PrintPanel();", true);
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


                        db.insert(" delete  supplierenquirydetails where id='" + ViewState["DeleteID"] + "' ");
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted Succesfully ');", true);

                    }
                    break;


                case ("Print"):
                    {
                        DataTable dt = new DataTable();
                        ViewState["printid"] = Convert.ToInt32(e.CommandArgument);

                        string enq = db.getDbstatus_Value("select enqid from supplierenquirydetails where id='" + ViewState["printid"] + "' ");
                        string supplier = db.getDbstatus_Value("select  supplier from  supplierenquirydetails where id='" + ViewState["printid"] + "'");
                        grdprint.DataSource = db.Displaygrid("select   Supplierenquiryitem.itemname as Itemname ,  Supplierenquiryitem.Particulars ,Supplierenquiryitem.Qty ,Supplierenquiryitem.rate as Rate  from Supplierenquiryitem   where   Supplierenquiryitem.enqid='" + enq + "' and  Supplierenquiryitem.supplier='" + supplier + "' ");
                        grdprint.DataBind();

                        lblenno.Text = enq.ToString();
                        lblsuppadd.Text = db.getDbstatus_Value("select Address from SuplierMaster where SuplierName='" + supplier + "' ");
                        lblsuppcontact.Text = db.getDbstatus_Value("select MobileNo from SuplierMaster where SuplierName='" + supplier + "' ");
                        lblSuppname.Text = supplier.ToString();
                        image.ImageUrl = db.getDbstatus_Value("select  CLogo from  CompanyMaster where  CompanyId='" + "1" + "'");

                        lblcontactperson.Text = db.getDbstatus_Value("select  contactname from supplierenquirydetails where  id='" + ViewState["printid"] + "' ");
                        lblcontactno.Text = db.getDbstatus_Value("select  contctmob from supplierenquirydetails where  id='" + ViewState["printid"] + "' ");
                        float a = db.getDb_Value("SELECT cgstamount FROM supplierenquirydetails WHERE id='" + ViewState["printid"] + "'  ");
                        float b = db.getDb_Value("SELECT sgstamount FROM supplierenquirydetails WHERE id='" + ViewState["printid"] + "'  ");
                        float c = db.getDb_Value("SELECT igstamount FROM supplierenquirydetails WHERE id='" + ViewState["printid"] + "'  ");
                        float d = a + b + c;
                        lblcgstammount.Text = a.ToString();
                        lblsgstammount.Text = b.ToString();
                        lbligstammount.Text = c.ToString();


                        float k = db.getDb_Value("SELECT cgst FROM supplierenquirydetails WHERE id='" + ViewState["printid"] + "'  ");
                        float f = db.getDb_Value("SELECT sgst FROM supplierenquirydetails WHERE id='" + ViewState["printid"] + "'  ");
                        float g = db.getDb_Value("SELECT igst FROM supplierenquirydetails WHERE id='" + ViewState["printid"] + "'  ");

                        lblcgestper.Text = k.ToString();
                        lblsgstper.Text = f.ToString();
                        lbligstper.Text = g.ToString();


                        lblgst.Text = d.ToString();
                        lbldiscount.Text = db.getDb_Value("SELECT discount FROM supplierenquirydetails WHERE id='" + ViewState["printid"] + "'  ").ToString();
                        lblsubtotal.Text = db.getDb_Value("SELECT subtotal FROM supplierenquirydetails WHERE id='" + ViewState["printid"] + "'  ").ToString();
                        lblgrandTotal.Text = db.getDb_Value("SELECT nettotal FROM supplierenquirydetails WHERE id='" + ViewState["printid"] + "'  ").ToString();
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

    protected void GrdtxtPerVAT_TextChanged(object sender, EventArgs e)
    {

    }





    protected void BtnRefresh_Click1(object sender, EventArgs e)
    {
        double GTotal = 0;
        for (int i = 0; i < GrdRequisition.Rows.Count; i++)
        {
           string total = (GrdRequisition.Rows[i].FindControl("txtqty") as TextBox).Text;
            string total1= (GrdRequisition.Rows[i].FindControl("txtrate") as TextBox).Text;

            float ab = float.Parse(total);
            float bc= float.Parse(total1);
            float cd = ab * bc;
            string grandtgotal = cd.ToString();
            if (grandtgotal != "")
            {
                GTotal += Convert.ToDouble(grandtgotal);
            }
        }






        double cgst = 0;
        for (int i = 0; i < GrdRequisition.Rows.Count; i++)
        {
            String total = (GrdRequisition.Rows[i].FindControl("GrdtxtCGSTAmt") as TextBox).Text;

            if (total != "")
            {
                cgst += Convert.ToDouble(total);
            }
        }



        double sGST = 0;
        for (int i = 0; i < GrdRequisition.Rows.Count; i++)
        {
            String total = (GrdRequisition.Rows[i].FindControl("GrdtxtSGSTAmt") as TextBox).Text;

            if (total != "")
            {
                sGST += Convert.ToDouble(total);
            }
        }






        double IGST = 0;

        string sgstper = "0";
        string igstper = "0";
        string cgstper = "0";
        for (int i = 0; i < GrdRequisition.Rows.Count; i++)
        {
            String total = (GrdRequisition.Rows[i].FindControl("GrdtxtIGSTAmt") as TextBox).Text;
            cgstper = (GrdRequisition.Rows[i].FindControl("GrdtxtCGSTPer") as TextBox).Text;
            sgstper = (GrdRequisition.Rows[i].FindControl("GrdtxtSGSTPer") as TextBox).Text;
            igstper = (GrdRequisition.Rows[i].FindControl("GrdtxtIGSTPer") as TextBox).Text;
            if (total != "")
            {
                IGST += Convert.ToDouble(total);
            }
        }


        txtvatper.Text = cgstper.ToString();
        txtSGSTPer.Text = sgstper.ToString();
        txtIGSTPer.Text = igstper.ToString();
        txtSGSTAmt.Text = sGST.ToString();
        txtIGSTAmt.Text = IGST.ToString();
        txtvatamt.Text = cgst.ToString();
      txtSubTotal.Text = GTotal.ToString();
        //  txtDisCountSub.Text = "0";
       // lblTotalAmt.Text = GTotal.ToString();
        //txtDisCountSub.Text = "0";
        //  ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Calculate_NetTotal()", true);

        float a = float.Parse(txtSGSTAmt.Text);
        float b = float.Parse(txtIGSTAmt.Text);
        float c = float.Parse(txtvatamt.Text);
        float d = a + b + c;
        // txtToalWithGST.Text = double.Parse(txtvatper.Text) + double.Parse(txtSGSTPer.Text) + double.Parse(txtIGSTPer.Text).ToString();
        txtToalWithGST.Text = d.ToString();
        float n = float.Parse(txtSubTotal.Text);
        float f = n + d;
        txtNetTotal.Text = f.ToString();


    }

    protected void GrdRequisition_DataBound(object sender, EventArgs e)
    {
        
    }
}