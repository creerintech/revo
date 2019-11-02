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
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using System.Text;
using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using AjaxControlToolkit;
using System.Threading;

public partial class Transactions_EditAuthPurchaseOrder : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoServerCaching();
        HttpContext.Current.Response.Cache.SetNoStore();
    }


    #region [private variables]
    DMAuthorisedPurchaseOrder Obj_PurchaseOrder = new DMAuthorisedPurchaseOrder();
    PurchaseOrder Entity_PurchaseOrder = new PurchaseOrder();
    CommanFunction obj_Comman = new CommanFunction();
    DataSet Ds = new DataSet();
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    #endregion
    #region [For User Defined Function]--------------
    private void MakeEmptyForm()
    {
       Setinitialrow();
        FillCombo();
        TxtPONO.Text = string.Empty;
        lblindentno.Text = LBLPOTHROUGH.Text = "-";
        txtpodate.Text = txtquotdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtSubTotal.Text = "0.00";
        txtDiscount.Text = "0.00";
        txtVATAmount.Text = "0.00";
        txtexciseduty.Text = txtGrandTotal.Text = "0.00";
        txtexcisedutyper.Text = "0";
        CHKHAMALI.Checked = CHKFreightAmt.Checked = CHKOtherCharges.Checked = CHKLoading.Checked = false;
        txtSerTax.Text = txtHamaliAmt.Text = txtFreightAmt.Text = txtPostageAmt.Text = txtOtherCharges.Text = txtGrandTotal.Text = "0.00";
        TXTTERMSCONDITION.Text = TXTPaymentTerms.Text = txtNarration.Text=txtpoqtno.Text = "";
        BindReportGrid("");
        txtpodate.Focus();
    }

    public void BindReportGrid(string RepCondition)
    {
        try
        {
            DataSet DsReport = new DataSet();
            DsReport = Obj_PurchaseOrder.GetCancelPurchase_Order(RepCondition, out StrError);
            if (DsReport.Tables.Count > 0 && DsReport.Tables[0].Rows.Count > 0)
            {
                ReportGrid.DataSource = DsReport.Tables[0];
                ReportGrid.DataBind();
            }
            else
            {
                ReportGrid.DataSource = null;
                ReportGrid.DataBind();
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void FillCombo()
    {
        try
        {
            string Cond = string.Empty;
            Ds = Obj_PurchaseOrder.BindComboBox(out StrError);
            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    ddlpono.DataSource = Ds.Tables[0];
                    ddlpono.DataTextField = "Name";
                    ddlpono.DataValueField = "ID";
                    ddlpono.DataBind();
                }
                if (Ds.Tables[1].Rows.Count > 0)
                {
                    ddlCompany.DataSource = Ds.Tables[1];
                    ddlCompany.DataTextField = "Name";
                    ddlCompany.DataValueField = "ID";
                    ddlCompany.DataBind();
                }
                if (Ds.Tables[2].Rows.Count > 0)
                {
                    DDLSERVICETAX.DataSource = Ds.Tables[2];
                    DDLSERVICETAX.DataTextField = "Name";
                    DDLSERVICETAX.DataValueField = "ID";
                    DDLSERVICETAX.DataBind();
                }
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    private void Setinitialrow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("ItemId", typeof(int));
            dt.Columns.Add("ItemDetailsId", typeof(int));
            dt.Columns.Add("UnitId", typeof(int));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("ItemDesc", typeof(string));
            dt.Columns.Add("ReqQty", typeof(decimal));
            dt.Columns.Add("SuplierID", typeof(int));
            dt.Columns.Add("Unit", typeof(int));
            dt.Columns.Add("ReqByCafeteria", typeof(string));
            dt.Columns.Add("VUOM", typeof(string));
            dt.Columns.Add("AvgPurRate", typeof(decimal));
            dt.Columns.Add("OrdQty", typeof(decimal));
            dt.Columns.Add("PerVat", typeof(decimal));
            dt.Columns.Add("VAT", typeof(decimal));
            dt.Columns.Add("PerDisc", typeof(decimal));
            dt.Columns.Add("Disc", typeof(decimal));
            dt.Columns.Add("RemarkForPO", typeof(string));
            dr = dt.NewRow();
            dr["#"] = 0;
            dr["ItemId"] = 0;
            dr["ItemDetailsId"] = 0;
            dr["UnitId"] = 0;
            dr["ItemCode"] = "";
            dr["ItemName"] = "";
            dr["ItemDesc"] = "";
            dr["ReqQty"] = 0;
            dr["Unit"] = 0;
            dr["SuplierID"] = 0;
            dr["ReqByCafeteria"] = "";
            dr["VUOM"] = "";
            dr["AvgPurRate"] = 0;
            dr["OrdQty"] = 0;
            dr["PerVat"] = 0;
            dr["VAT"] = 0;
            dr["PerDisc"] = 0;
            dr["Disc"] = 0;
            dr["RemarkForPO"] = "";
            dt.Rows.Add(dr);
            ViewState["CurrentTablePO"] = dt;
            GrdReqPO.DataSource = dt;
            GrdReqPO.DataBind();
        }

        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    public void BindRequisitionGrid(string RepCondition)
    {
        try
        {
            Ds = Obj_PurchaseOrder.GetOrder(RepCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                GrdReqPO.DataSource = Ds.Tables[0];
                GrdReqPO.DataBind();

                for (int v = 0; v < GrdReqPO.Rows.Count; v++)
                {
                    DropDownList ddlu = (DropDownList)GrdReqPO.Rows[v].FindControl("GrdddlUOM");
                    ddlu.Enabled = false;

                    DropDownList ddlVendor = (DropDownList)GrdReqPO.Rows[v].FindControl("GrdddlVendor");
                    ddlVendor.Enabled = false;
                    TextBox GrdtxtRate = (TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtRate");
                    GrdtxtRate.Enabled = false;

                    TextBox GrdtxtOrdQty = (TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtOrdQty");
                    GrdtxtOrdQty.Enabled = false;


                    TextBox GrdtxtPerVAT = (TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtPerVAT");
                    GrdtxtPerVAT.Enabled = false;

                    TextBox GrdtxtVAT = (TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtVAT");
                    GrdtxtVAT.Enabled = false;

                    TextBox GrdtxtPerDISC = (TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtPerDISC");
                    GrdtxtPerDISC.Enabled = false;

                    TextBox GrdtxtDISC = (TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtDISC");
                    GrdtxtDISC.Enabled = false;


                    TextBox GrdtxtRemarkForPO = (TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtRemarkForPO");
                    GrdtxtRemarkForPO.Enabled = false;
                    
                }


                ViewState["POCurrentTable"] = Ds.Tables["Table"];
                if (Ds.Tables[1].Rows.Count > 0)
                {
                    ddlCompany.SelectedValue = Ds.Tables[1].Rows[0]["CompanyID"].ToString();
                    lblindentno.Text = Ds.Tables[2].Rows[0]["REQNO"].ToString();
                    LBLPOTHROUGH.Text = Ds.Tables[1].Rows[0]["UserName"].ToString();
                    txtSubTotal.Text= Ds.Tables[1].Rows[0]["SubTotal"].ToString();
                    txtGrandTotal.Text = Ds.Tables[1].Rows[0]["GrandTotal"].ToString();
                    txtVATAmount.Text = Ds.Tables[1].Rows[0]["Discount"].ToString();
                    txtDiscount.Text = Ds.Tables[1].Rows[0]["Vat"].ToString();
                    txtpodate.Text = Convert.ToDateTime(Ds.Tables[1].Rows[0]["PODate"].ToString()).ToString("dd-MMM-yyyy");
                    txtHamaliAmt.Text = Ds.Tables[1].Rows[0]["HamaliAmt"].ToString();
                    txtFreightAmt.Text = Ds.Tables[1].Rows[0]["FreightAmt"].ToString();
                    txtPostageAmt.Text = Ds.Tables[1].Rows[0]["PostageAmt"].ToString();
                    txtOtherCharges.Text = Ds.Tables[1].Rows[0]["OtherCharges"].ToString();
                    txtpoqtno.Text = Ds.Tables[1].Rows[0]["POQTNO"].ToString();
                   // txtquotdate.Text =Convert.ToDateTime(Ds.Tables[1].Rows[0]["POQTDATE"].ToString()).ToString("dd-MMM-yyyy");
                    txtSerTax.Text = Ds.Tables[1].Rows[0]["ServiceTaxAmt"].ToString();
                    DDLSERVICETAX.Items.FindByText(Ds.Tables[1].Rows[0]["ServiceTaxPer"].ToString()).Selected = true;
                    txtNarration.Text = Ds.Tables[1].Rows[0]["Instruction"].ToString();
                    txtexcisedutyper.Text = Ds.Tables[1].Rows[0]["ExcisePer"].ToString();
                    txtexciseduty.Text = Ds.Tables[1].Rows[0]["ExciseAmount"].ToString();

                    txtInstallationRemark.Text = Ds.Tables[1].Rows[0]["InstallationRemark"].ToString();
                    txtInstallationCharge.Text = Ds.Tables[1].Rows[0]["InstallationCharge"].ToString();
                    txtInstallationServicetax.Text = Ds.Tables[1].Rows[0]["InstallationSerTaxPer"].ToString();
                    txtInstallationServiceAmount.Text = Ds.Tables[1].Rows[0]["InstallationSerTaxAmt"].ToString();

                    if (Convert.ToInt32(Ds.Tables[1].Rows[0]["HamaliActual"].ToString()) == 1)
                    {
                        CHKHAMALI.Checked = true;
                    }
                    if (Convert.ToInt32(Ds.Tables[1].Rows[0]["HamaliActual"].ToString()) == 0)
                    {
                        CHKHAMALI.Checked = false;
                    }

                    if (Convert.ToInt32(Ds.Tables[1].Rows[0]["FreightActual"].ToString()) == 1)
                    {
                        CHKFreightAmt.Checked = true;
                    }
                    if (Convert.ToInt32(Ds.Tables[1].Rows[0]["FreightActual"].ToString()) == 0)
                    {
                        CHKFreightAmt.Checked = false;
                    }

                    if (Convert.ToInt32(Ds.Tables[1].Rows[0]["OtherChargeActual"].ToString()) == 1)
                    {
                        CHKOtherCharges.Checked = true;
                    }
                    if (Convert.ToInt32(Ds.Tables[1].Rows[0]["OtherChargeActual"].ToString()) == 0)
                    {
                        CHKOtherCharges.Checked = false;
                    }

                    if (Convert.ToInt32(Ds.Tables[1].Rows[0]["LoadingActual"].ToString()) == 1)
                    {
                        CHKLoading.Checked = true;
                    }
                    if (Convert.ToInt32(Ds.Tables[1].Rows[0]["LoadingActual"].ToString()) == 0)
                    {
                        CHKLoading.Checked = false;
                    }
                    TXTTERMSCONDITION.Text = Ds.Tables[3].Rows[0]["TermsCondition"].ToString();
                    TXTPaymentTerms.Text = Ds.Tables[3].Rows[0]["PaymentTerms"].ToString();
                }
                ((DropDownList)GrdReqPO.Rows[0].FindControl("GrdddlVendor")).Focus();
            }
            else
            {
                GrdReqPO.DataSource = null;
                GrdReqPO.DataBind();
                Setinitialrow();
                obj_Comman.ShowPopUpMsg("OOPS!.. Some Error Occured While Loading Data, Please Try Again Later..", this.Page);
            }
        }
        catch (Exception ex) { obj_Comman.ShowPopUpMsg("OOPS!.. Some Error Occured While Loading Data, Please Try Again Later..", this.Page); }
    }
    #endregion---------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Session["UserName"].ToString()))
            {
                Username.Text = Session["UserName"].ToString();
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
           TXTPASSWORDFORM.Focus();
            MakeEmptyForm();
        }
    }
    protected void ddlpono_SelectedIndexChanged(object sender, EventArgs e)
    {
        StrCondition = string.Empty;
        if (ddlpono.SelectedValue != "0")
        {
            StrCondition = StrCondition + " AND PO.POID=" + Convert.ToInt32(ddlpono.SelectedValue);
        }
        BindRequisitionGrid(StrCondition);
    }
    protected void GrdReqPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region[Add PURCHASE ORDER UNIT AND SUPPLIER]------------
             
                if (Convert.ToInt32(((Label)e.Row.FindControl("LblEntryId")).Text) > 0)
                {
                    DataSet dsUnit = new DataSet();
                    DMAuthorisedPurchaseOrder PO = new DMAuthorisedPurchaseOrder();
                    dsUnit = PO.GETUNITCONVERT(Convert.ToInt32(e.Row.Cells[4].Text.ToString()), Convert.ToInt32(e.Row.Cells[2].Text.ToString()), Convert.ToDecimal(((TextBox)e.Row.FindControl("GrdtxtOrdQty")).Text),Convert.ToInt32(e.Row.Cells[3].Text.ToString()), out StrError);
                    if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
                    {
                        ((DropDownList)e.Row.FindControl("GrdddlUOM")).DataSource = dsUnit.Tables[0];
                        ((DropDownList)e.Row.FindControl("GrdddlUOM")).DataTextField = "UnitFactor";
                        ((DropDownList)e.Row.FindControl("GrdddlUOM")).DataValueField = "UnitConvDtlsId";
                        ((DropDownList)e.Row.FindControl("GrdddlUOM")).DataBind();
                    }
                    else
                    {
                        ((DropDownList)e.Row.FindControl("GrdddlUOM")).DataSource = null;
                        ((DropDownList)e.Row.FindControl("GrdddlUOM")).DataBind();
                    }
                    if (dsUnit.Tables.Count > 0 && dsUnit.Tables[1].Rows.Count > 0)
                    {
                        ((DropDownList)e.Row.FindControl("GrdddlVendor")).DataSource = dsUnit.Tables[1];
                        ((DropDownList)e.Row.FindControl("GrdddlVendor")).DataTextField = "SuplierName";
                        ((DropDownList)e.Row.FindControl("GrdddlVendor")).DataValueField = "SuplierId";
                        ((DropDownList)e.Row.FindControl("GrdddlVendor")).DataBind();
                        ((DropDownList)e.Row.FindControl("GrdddlVendor")).SelectedValue = String.IsNullOrEmpty(((TextBox)e.Row.FindControl("GRDTXTSUPLID")).Text) ? "0" : ((TextBox)e.Row.FindControl("GRDTXTSUPLID")).Text;
                    }
                    else
                    {
                        ((DropDownList)e.Row.FindControl("GrdddlVendor")).DataSource = null;
                        ((DropDownList)e.Row.FindControl("GrdddlVendor")).DataBind();
                    }
                }
                #endregion-----------
            }
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg(ex.Message+" ..THIS ERROR OCCURED WHILE LOADING DATA.",this.Page);
        }
    }
    protected void GrdReqPO_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable DtEditPO = new DataTable();
            DtEditPO = (DataTable)ViewState["POCurrentTable"];
            if (DtEditPO.Rows.Count == 0)
            {
                obj_Comman.ShowPopUpMsg("No ROW Find For Perform Delete Opeation", this.Page);
                return;
            }
            else
            {
                int CurrRow = Convert.ToInt32(e.RowIndex);

                if (DtEditPO.Rows.Count > 0)
                {
                    DtEditPO.Rows[CurrRow].Delete();
                    DtEditPO.AcceptChanges();
                    if (DtEditPO.Rows.Count == 0)
                    {
                       
                        Setinitialrow();
                        return;
                    }
                    GrdReqPO.DataSource = null;
                    GrdReqPO.DataSource = DtEditPO;
                    GrdReqPO.DataBind();
                    ViewState["POCurrentTable"] = DtEditPO;

                }
            }
        }
        catch (Exception ex)
        {
            obj_Comman.ShowPopUpMsg(ex.Message+" ..THIS ERROR OCCURED WHILE LOADING DATA.",this.Page);
        }
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int InsertRowDtls = 0, iUpdate = 0;
            if (ddlCompany.SelectedValue != "0")
            {
                    Entity_PurchaseOrder.POId = Convert.ToInt32(ddlpono.SelectedValue.ToString());
                    Entity_PurchaseOrder.PODate = Convert.ToDateTime(txtpodate.Text.ToString());
                    Entity_PurchaseOrder.SubTotal = txtSubTotal.Text.Equals("") ? 0 : Convert.ToDecimal(txtSubTotal.Text);
                    Entity_PurchaseOrder.Discount = txtDiscount.Text.Equals("") ? 0 : Convert.ToDecimal(txtDiscount.Text);
                    Entity_PurchaseOrder.Vat = txtVATAmount.Text.Equals("") ? 0 : Convert.ToDecimal(txtVATAmount.Text);
                    Entity_PurchaseOrder.GrandTotal = txtGrandTotal.Text.Equals("") ? 0 : Convert.ToDecimal(txtGrandTotal.Text);
                    Entity_PurchaseOrder.HamaliAmt = txtHamaliAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtHamaliAmt.Text);
                    Entity_PurchaseOrder.FreightAmt = txtFreightAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtFreightAmt.Text);
                    Entity_PurchaseOrder.PostageAmt = txtPostageAmt.Text.Equals("") ? 0 : Convert.ToDecimal(txtPostageAmt.Text);
                    Entity_PurchaseOrder.OtherCharges = txtOtherCharges.Text.Equals("") ? 0 : Convert.ToDecimal(txtOtherCharges.Text);
                    Entity_PurchaseOrder.ServiceTaxPer = Convert.ToDecimal(DDLSERVICETAX.SelectedItem.ToString());
                    Entity_PurchaseOrder.ServiceTaxAmt = Convert.ToDecimal(txtSerTax.Text.ToString());
                    Entity_PurchaseOrder.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue.ToString());
                    Entity_PurchaseOrder.Instruction = txtNarration.Text.Trim();
                    Entity_PurchaseOrder.UserId = Convert.ToInt32(Session["UserId"]);
                    Entity_PurchaseOrder.LoginDate = DateTime.Now;
                    Entity_PurchaseOrder.POQTDATE = Convert.ToDateTime(txtquotdate.Text.ToString()).ToString();
                    Entity_PurchaseOrder.ExcisePer = Convert.ToDecimal(txtexcisedutyper.Text.ToString());
                    Entity_PurchaseOrder.ExciseAmount = Convert.ToDecimal(txtexciseduty.Text.ToString());
                    if (CHKHAMALI.Checked == true)
                    {
                        Entity_PurchaseOrder.HamaliActual = 1;
                    }
                    if (CHKHAMALI.Checked == false)
                    {
                        Entity_PurchaseOrder.HamaliActual = 0;
                    }
                    if (CHKFreightAmt.Checked == true)
                    {
                        Entity_PurchaseOrder.FreightActual = 1;
                    }
                    if (CHKFreightAmt.Checked == false)
                    {
                        Entity_PurchaseOrder.FreightActual = 0;
                    }
                    if (CHKOtherCharges.Checked == true)
                    {
                        Entity_PurchaseOrder.OtherChargeActual = 1;
                    }
                    if (CHKOtherCharges.Checked == false)
                    {
                        Entity_PurchaseOrder.OtherChargeActual = 0;
                    }
                    if (CHKLoading.Checked == true)
                    {
                        Entity_PurchaseOrder.LoadingActual = 1;
                    }
                    if (CHKLoading.Checked == false)
                    {
                        Entity_PurchaseOrder.LoadingActual = 0;
                    }
                    iUpdate = Obj_PurchaseOrder.UpdateRecord(ref Entity_PurchaseOrder, out StrError);
               
                if (iUpdate > 0)
                {
                    if (ViewState["POCurrentTable"] != null)
                    {
                        DataTable dttable = new DataTable();
                        dttable = (DataTable)ViewState["POCurrentTable"];
                        for (int v = 0; v < GrdReqPO.Rows.Count; v++)
                        {
                            DMPurchaseOrder oDMPO = new DMPurchaseOrder();
                            PurchaseOrder pod = new PurchaseOrder();
                            pod.POId = Convert.ToInt32(ddlpono.SelectedValue.ToString());
                            pod.ItemId = Convert.ToInt32(GrdReqPO.Rows[v].Cells[2].Text);
                            pod.Qty = Convert.ToDecimal(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtOrdQty")).Text);
                            pod.Rate = Convert.ToDecimal(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtRate")).Text);
                            pod.Amount = Convert.ToDecimal(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtOrdQty")).Text) * Convert.ToDecimal(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtRate")).Text) ;
                            pod.TaxPer = Convert.ToDecimal(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtPerVAT")).Text);
                            pod.TaxAmount = Convert.ToDecimal(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtVAT")).Text);
                            pod.DiscPer = Convert.ToDecimal(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtPerDISC")).Text);
                            pod.DiscAmt = Convert.ToDecimal(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtDISC")).Text);
                            pod.NetAmount = ((Convert.ToDecimal(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtOrdQty")).Text) * Convert.ToDecimal(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtRate")).Text)) - Convert.ToDecimal(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtDISC")).Text)) + Convert.ToDecimal(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtVAT")).Text);
                            pod.RequisitionCafeId = Convert.ToInt32(GrdReqPO.Rows[v].Cells[3].Text);
                            pod.FK_UnitConvDtlsId = Convert.ToInt32(((DropDownList)GrdReqPO.Rows[v].FindControl("GrdddlUOM")).SelectedValue);
                            pod.MainQty = 0;
                            pod.RemarkForPO = Convert.ToString(((TextBox)GrdReqPO.Rows[v].FindControl("GrdtxtRemarkForPO")).Text);
                            pod.CHKFLAG = (((CheckBox)GrdReqPO.Rows[v].FindControl("GrdReqPO_CHK")).Checked)?1:0;
                            InsertRowDtls = Obj_PurchaseOrder.Insert_PurchaseOrderDtls(ref pod, Convert.ToInt32(GrdReqPO.Rows[v].Cells[4].Text), out StrError);
                            oDMPO = null;
                            pod = null;
                        }
                    }
                    if (InsertRowDtls > 0)
                    {

                        DMAuthorisedPurchaseOrder ODMPUR = new DMAuthorisedPurchaseOrder();
                                PurchaseOrder ENPUR = new PurchaseOrder();
                                ENPUR.POId = Convert.ToInt32(ddlpono.SelectedValue.ToString());
                                ENPUR.Title = "";
                                ENPUR.TermsCondition = Convert.ToString(TXTTERMSCONDITION.Text);
                                InsertRowDtls = ODMPUR.Update_PurchaseOrderTandC(ref ENPUR, Convert.ToString(TXTPaymentTerms.Text), out StrError);
                                ODMPUR = null;
                                ENPUR = null;
                     
                    }
                    if (InsertRowDtls != 0)
                    {
                        obj_Comman.ShowPopUpMsg("Record Updated Successfully!", this.Page);
                        int GETUPDATES = Obj_PurchaseOrder.UpdateInsert_PurchseOrder( Convert.ToInt32(ddlpono.SelectedValue.ToString()), out StrError);
                        //Response.Redirect("~/CrystalPrint/PrintCryRpt.aspx?ID=" + Convert.ToInt32(ddlpono.SelectedValue.ToString()) + "&Flag=CPS&SFlag=Authorised&PDFFlag=PDF&PrintFlag=NO");
                        MakeEmptyForm();
                        
                    }
                    else
                    {
                        obj_Comman.ShowPopUpMsg(StrError, this.Page);
                    }
                }
            }
            else
            {
                obj_Comman.ShowPopUpMsg("Please select company!", this.Page);
            }
        }
        catch (Exception ex) { obj_Comman.ShowPopUpMsg(ex.Message + " --> THIS ERROR OCCURED WHILE UPDATING DATA.", this.Page); }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
    protected void BTNLOGINFORM_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet DS = new DataSet();
            DMAuthorisedPurchaseOrder DMPO = new DMAuthorisedPurchaseOrder();
            DS = DMPO.GETACCESS(Convert.ToInt32(Session["UserId"]), TXTPASSWORDFORM.Text.Trim(), out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {

                Panel1.Visible = false;

            }
            else
            {
                obj_Comman.ShowPopUpMsg("Password Not Match......Please Try Again!", this.Page);
            }
        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception ex) { obj_Comman.ShowPopUpMsg(ex.Message + " --> THIS ERROR OCCURED WHILE UPDATING DATA.", this.Page); }
    }

    protected void GrdReqPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.ReportGrid.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            Ds = Obj_PurchaseOrder.GetCancelPurchase_Order(StrCondition, out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                ReportGrid.DataSource = Ds.Tables[0];
                this.ReportGrid.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionItemNameList(string prefixText, int count, string contextKey)
    {
        DMAuthorisedPurchaseOrder DMPO = new DMAuthorisedPurchaseOrder();
        String[] SearchList = DMPO.GetSuggestedRecordItems(prefixText, "");
        return SearchList;
    }
    protected void TxtPONO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            StrCondition = TxtPONO.Text.Trim();
            Ds = new DataSet();
            Ds = Obj_PurchaseOrder.GetItems(StrCondition, "", out StrError);
            if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                TxtPONO.Text = Ds.Tables[0].Rows[0]["Name"].ToString();
                TxtPONO.ToolTip = Ds.Tables[0].Rows[0]["ID"].ToString();
                ddlpono.SelectedValue = Ds.Tables[0].Rows[0]["ID"].ToString();
                ddlpono_SelectedIndexChanged(ddlpono as AjaxControlToolkit.ComboBox, EventArgs.Empty);
                Ds = null;
            }
            else
            {
                TxtPONO.Text = "";
                TxtPONO.ToolTip = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void GrdReqPO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        int CurrRow = Convert.ToInt32(e.CommandArgument);

                        CheckBox SELCHK = (CheckBox)GrdReqPO.Rows[CurrRow].FindControl("GrdReqPO_CHK");
                        //SELCHK.Checked = true;
                        //if (SELCHK.Checked == true)
                        //{

                            //for (int v = 0; v < GrdReqPO.Rows.Count; v++)
                            //{
                                DropDownList ddlu = (DropDownList)GrdReqPO.Rows[CurrRow].FindControl("GrdddlUOM");
                                ddlu.Enabled = true;

                                DropDownList ddlVendor = (DropDownList)GrdReqPO.Rows[CurrRow].FindControl("GrdddlVendor");
                                ddlVendor.Enabled = true;

                                TextBox GrdtxtRate = (TextBox)GrdReqPO.Rows[CurrRow].FindControl("GrdtxtRate");
                                GrdtxtRate.Enabled = true;

                                TextBox GrdtxtOrdQty = (TextBox)GrdReqPO.Rows[CurrRow].FindControl("GrdtxtOrdQty");
                                GrdtxtOrdQty.Enabled = true;


                                TextBox GrdtxtPerVAT = (TextBox)GrdReqPO.Rows[CurrRow].FindControl("GrdtxtPerVAT");
                                GrdtxtPerVAT.Enabled = true;

                                TextBox GrdtxtVAT = (TextBox)GrdReqPO.Rows[CurrRow].FindControl("GrdtxtVAT");
                                GrdtxtVAT.Enabled = true;

                                TextBox GrdtxtPerDISC = (TextBox)GrdReqPO.Rows[CurrRow].FindControl("GrdtxtPerDISC");
                                GrdtxtPerDISC.Enabled = true;

                                TextBox GrdtxtDISC = (TextBox)GrdReqPO.Rows[CurrRow].FindControl("GrdtxtDISC");
                                GrdtxtDISC.Enabled = true;


                                TextBox GrdtxtRemarkForPO = (TextBox)GrdReqPO.Rows[CurrRow].FindControl("GrdtxtRemarkForPO");
                                GrdtxtRemarkForPO.Enabled = true;
                            //}
                        //}
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
        }

    }

   
}
