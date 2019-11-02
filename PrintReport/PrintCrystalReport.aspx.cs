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

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;

using System.IO;
public partial class PrintReport_PrintCrystalReport : System.Web.UI.Page
{
    #region [Private Variables]

    string strError = string.Empty;
    private static int EditModeId = 0;
    private static string OpenForm = "";

    CommanFunction obj_Comman = new CommanFunction();

    System.Data.DataSet dsLogin = new System.Data.DataSet();
    ReportDocument CRpt = new ReportDocument();
    decimal pric = (decimal)0.00;
    string TotAmt;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            EditModeId = Convert.ToInt32(Request.QueryString["ID"]);
            OpenForm = Convert.ToString(Request.QueryString["Flag"]);

            switch (OpenForm)
            {
                    //Purchase Order details
                case "PS":
                    {
                        DMPurchaseOrder oDMPurchaseOrder = new DMPurchaseOrder();
                        dsLogin = oDMPurchaseOrder.PrintPOReport_SupplierWise(EditModeId, out strError);
                        dsLogin.Tables[0].TableName = "PurchaseOrder_Master";
                        //dsLogin.Tables[1].TableName = "PurchaseOrder_Details";                        
                        //-------------------------------------------
                        if (dsLogin.Tables.Count > 0)
                        {
                            for (int i = 0; i < dsLogin.Tables[0].Rows.Count; i++)
                            {
                                pric += Convert.ToDecimal(dsLogin.Tables[0].Rows[i]["NetAmount"].ToString());
                            }
                        }
                        TotAmt = WordAmount.convertcurrency(pric);
                        DataColumn column = new DataColumn("Number2Word");
                        column.DataType = typeof(string);
                        dsLogin.Tables[0].Columns.Add("Number2Word");

                        dsLogin.Tables[0].Rows[dsLogin.Tables[0].Rows.Count - 1]["Number2Word"] = TotAmt.ToString();
                        //-------------------------------------------
                        CRpt.Load(Server.MapPath("~/PrintReport/PurchaseOrder/PurchaseOrder_SupplierWise.rpt"));
                        CRpt.SetDataSource(dsLogin);
                        break;
                    }
                //Authorised Purchase Order
                case "PO":
                    {
                        DMPurchaseOrder oDMPurchaseOrder = new DMPurchaseOrder();
                        dsLogin = oDMPurchaseOrder.PrintPOReport_SupplierWise(EditModeId, out strError);
                        dsLogin.Tables[0].TableName = "PurchaseOrder_Master";
                        //dsLogin.Tables[1].TableName = "PurchaseOrder_Details";                        
                        //-------------------------------------------
                        if (dsLogin.Tables.Count > 0)
                        {
                            for (int i = 0; i < dsLogin.Tables[0].Rows.Count; i++)
                            {
                                pric += Convert.ToDecimal(dsLogin.Tables[0].Rows[i]["NetAmount"].ToString());
                            }
                        }
                        TotAmt = WordAmount.convertcurrency(pric);
                        DataColumn column = new DataColumn("Number2Word");
                        column.DataType = typeof(string);
                        dsLogin.Tables[0].Columns.Add("Number2Word");

                        dsLogin.Tables[0].Rows[dsLogin.Tables[0].Rows.Count - 1]["Number2Word"] = TotAmt.ToString();
                        //-------------------------------------------
                        CRpt.Load(Server.MapPath("~/PrintReport/PurchaseOrder/Authorized PurchaseOrder.rpt"));
                       CRpt.SetDataSource(dsLogin);
                        break;
                    }

            }

            CrystalReportViewer1.ReportSource = CRpt;
            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.DisplayToolbar = true;
        }
        catch (Exception ex)
        {
        }
    }
}
