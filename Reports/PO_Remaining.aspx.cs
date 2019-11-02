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
using MayurInventory.DALSQLHelper;
using MayurInventory.DataModel;
using MayurInventory.DB;
using MayurInventory.EntityClass;
using MayurInventory.Utility;
using System.IO;

public partial class Reports_PO_Remaining : System.Web.UI.Page
{

    #region[Private variables]
    DMPurchaseOrder Obj_PO = new DMPurchaseOrder();
    PurchaseOrder Entity_PO = new PurchaseOrder();
    CommanFunction Obj_Comm = new CommanFunction();
    public static DataSet DsGrd = new DataSet();
    DataSet DS = new DataSet();
    decimal TotalQty, TotalAmt, TotalNetAmt, TotalTax = 0;
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    private static bool FlagPrint = false;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MakeEmptyForm();

        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt;
            if (DateTime.TryParse(txtFromDate.Text.ToString(), out dt) == false)
            {
                Obj_Comm.ShowPopUpMsg("Please enter valid as on date..", this.Page);
                txtFromDate.Focus();
                return;
            }

            if (DateTime.Parse(txtFromDate.Text.ToString()) > DateTime.Now.Date)
            {
                Obj_Comm.ShowPopUpMsg("As on date should be less than or equal to todays date.", this.Page);
                txtFromDate.Focus();
                return;
            }

            DataSet ds = Obj_PO.Get_PONotComplete(txtFromDate.Text.ToString(), out StrError);
            if (ds.Tables.Count > 0)
            {
                GrdReport.DataSource = ds.Tables[0];
                GrdReport.DataBind();
            }
        }
        catch (Exception exp)
        {   
            throw new Exception(exp.Message);
        }
        


    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
        GrdReport.DataSource = null;
        GrdReport.DataBind();
    }
    protected void ImgBtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition",
            "attachment;filename=RemainingPOReport_" + DateTime.Now.ToString("dd MMM yyyy") + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView grd = new GridView();
        grd = GrdReport;
        grd.Columns[0].ItemStyle.Width = 0;
        grd.Columns[0].HeaderStyle.Width = 0;
        grd.Columns[0].Visible = false;

        grd.Columns[1].ItemStyle.Width = 0;
        grd.Columns[1].HeaderStyle.Width = 0;
        grd.Columns[1].Visible = false;

        grd.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Output.Write("");
        Response.Flush();
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    private void MakeEmptyForm()
    {
        if (!FlagPrint)
            //ImgBtnPrint.Visible = true;
        if (!FlagPrint)
            ImgBtnExport.Visible = true;
        txtFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        lblCount.Text = "";
        

    }
}
