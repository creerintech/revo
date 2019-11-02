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
using MayurInventory.DataModel;
using MayurInventory.Utility;

public partial class Masters_Drawing : System.Web.UI.Page
{
    DMRequisitionCafeteria Obj_RequisitionCafeteria = new DMRequisitionCafeteria();
    RequisitionCafeteria Entity_RequisitionCafeteria = new RequisitionCafeteria();
    string StrError;
    CommanFunction Obj_Comm = new CommanFunction();
    database db = new database();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataTable dt;
            database db = new database();
            if (Request.QueryString["url"] != null)
            {
                Response.Redirect(Request.QueryString["url"]);
            }


            if (Request.QueryString["RowIndex"] != null)
            {
                int rowIndex = int.Parse(Request.QueryString["RowIndex"]);

                if (Request.QueryString["FormName"] == "ItemMaster")
                {
                    dt = (DataTable)Session["ItemData"];
                    //string product = dt.Rows[rowIndex]["Product"].ToString();  

                    string path = dt.Rows[rowIndex]["DrawingPath"].ToString();

                    Response.Redirect("Drawing.aspx?url=" + dt.Rows[rowIndex]["DrawingPath"].ToString());

                }


                if (Request.QueryString["FormName"] == "MaterialRequisitionTemplate1")
                {
                    if (!string.IsNullOrEmpty(Session["customerId"] as string))
                    {
                        DataSet Ds = Obj_RequisitionCafeteria.GetTemplateData(Convert.ToInt32(Session["customerId"]), string.IsNullOrEmpty("1") ? 1 : Convert.ToDecimal("1"), out StrError);
                        DataTable dm = new DataTable();
                        dm = Ds.Tables[0];
                        string path = dm.Rows[rowIndex]["DrawingPath"].ToString();
                        Response.Redirect("Drawing.aspx?url=" + dm.Rows[rowIndex]["DrawingPath"].ToString());
                    }
                }


                if (Request.QueryString["FormName"] == "MaterialRequisitionTemplate12")
                {
                    if (!string.IsNullOrEmpty(Session["templateid1"] as string))
                    {
                        DataSet Ds = db.dgv_display("select DrawingPath  from ItemDetails  where ItemDetailsId='"+ Session["templateid1"] + "'");
                        DataTable dm = new DataTable();
                        dm = Ds.Tables[0];
                        string path = dm.Rows[0]["DrawingPath"].ToString();
                        Response.Redirect("Drawing.aspx?url=" + dm.Rows[0]["DrawingPath"].ToString());
                    }
                }

                if (Request.QueryString["FormName"] == "MaterialRequisitionTemplate")
                {

                    if (!string.IsNullOrEmpty(Session["categoryid"] as string))
                    {
                        dt = db.Displaygrid("select ItemCategory.CategoryId as [#] , '0' as ItemToolTip, ItemMaster.ItemId ,ItemMaster.SubcategoryId  ,ItemMaster.CategoryId, ItemCategory.CategoryName ,SubCategory.SubCategory , ItemMaster.ItemCode     ,ItemName + ' => ' + isnull(ItemMaster.ItemRemark,' ') + ' => ' + isnull(SubCategory.Remark,' ') as ItemName, '' as Location,ItemDetails.OpeningStock as AvlQty,  ItemDetails.OpeningStock as txtAvlQty ,  0 as TransitQty, ItemMaster.MinStockLevel,ItemMaster.MaxStockLevel, ItemMaster.DeliveryPeriod,ISNULL(ItemDetails.ItemDetailsId,0)as ItemDetailsId,0 as UnitConvDtlsId,  ItemDetails.PurchaseRate   as AvgRate ,ItemMaster.AsOn as AvgRateDate , 'Demo' as Vendor, '1' as VendorId, ItemDetails.PurchaseRate as Rate ,'abc' as Priority,'0' as IsCancel, '1' as PriorityID , ItemDetails.ToQty as txtOrdQty  , '0'  as RequiredDate ,'0' as Remark , ItemDetails.DrawingPath from ItemMaster inner join  ItemCategory on ItemCategory.CategoryId=ItemMaster.CategoryId   inner join SubCategory on SubCategory.SubCategoryId=ItemMaster.SubcategoryId inner join ItemDetails on ItemDetails.ItemId=ItemMaster.ItemId     where ItemCategory.CategoryId='" + Session["categoryid"] + "' ");
                        //string product = dt.Rows[rowIndex]["Product"].ToString();  

                        string path = dt.Rows[rowIndex]["DrawingPath"].ToString();

                        Response.Redirect("Drawing.aspx?url=" + dt.Rows[rowIndex]["DrawingPath"].ToString());
                    }
                    else if (!string.IsNullOrEmpty(Session["templateid"] as string))
                    {

                      DataSet  Ds = Obj_RequisitionCafeteria.GetTemplateData(Convert.ToInt32(Session["templateid"]), string.IsNullOrEmpty("1") ? 1 : Convert.ToDecimal("1"), out StrError);
                        DataTable dm = new DataTable();
                        dm = Ds.Tables[0];
                        string path = dm.Rows[rowIndex]["DrawingPath"].ToString();
                        Response.Redirect("Drawing.aspx?url=" + dm.Rows[rowIndex]["DrawingPath"].ToString());

                    }
                }
         

                else
                {

                    dt = (DataTable)Session["StockData"];
                    string product = dt.Rows[rowIndex]["Product"].ToString();
                    //GrdReport.DataKeys[gvr.RowIndex]["Product"].ToString();

                    var query = from a in dt.AsEnumerable()
                                where a.Field<string>("Product") == product
                                select a;

                    // Display records from the query
                    foreach (var EmpID in query)
                    {

                        //Response.Write("<script type='text/javascript'>");
                        //Response.Write("window.open('Drawing.aspx?url=~/ScannedDrawings/Kunal Passports.pdf','_blank');");
                        //Response.Write("</script>");
                        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('~/Reports/Drawing.aspx?url=~/ScannedDrawings/Kunal Passports.pdf','_blank')", true);

                        string path = dt.Rows[rowIndex]["DrawingPath"].ToString();
                        //Response.Redirect(path);                
                        Response.Redirect("Drawing.aspx?url=" + dt.Rows[rowIndex]["DrawingPath"].ToString());
                        //Process.Start(@"~/ScannedDrawings/Kunal Passports.pdf");
                    }

                }
            }
        }
        //Since you use Bound Fields, use row.Cells[] to read values



        catch (Exception ex)
        {
            StrError = ex.Message;
            if (StrError == "Thread was being aborted.")
            {
                Obj_Comm.ShowPopUpMsg("Please Check drawing path..!!", this.Page);

            }
            else
            {
                Obj_Comm.ShowPopUpMsg(StrError, this.Page);
            }
        }

    }
}
