using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for PurchaseOrder
/// </summary>
public class EditPurchaseOrder
{
    #region[Constants]

    //--**PurchseOrder**--
    public static string _Action = "@Action";
    public static string _POId = "@POId";
    public static string _PONo = "@PONo";
    public static string _PODate = "@PODate";
    public static string _SuplierId = "@SuplierId";
    public static string _BillingAddress = "@BillingAddress";
    public static string _ShippingAddress = "@ShippingAddress";
    public static string _SubTotal = "@SubTotal";
    public static string _Discount = "@Discount";
    public static string _Vat = "@Vat";
    public static string _GrandTotal = "@GrandTotal";
    public static string _Instruction = "@Instruction";
    public static string _POStatus = "@POStatus";
    public static string _GeneratedTime = "@GeneratedTime";
    public static string _ApprovedTime = "@ApprovedTime";
    public static string _AuthorizedTime = "@AuthorizedTime";
    public static string _UserId = "@UserId";
    public static string _LoginDate = "@LoginDate";
    public static string _IsDeleted = "@IsDeleted";
    public static string _strCond = "@strCond";

    //--**PurchaseOrdDtls**--
    public static string _PODtlsId = "@PODtlsId";
    public static string _ItemId = "@ItemId";
    public static string _Qty = "@Qty";
    public static string _Rate = "@Rate";
    public static string _Amount = "@Amount";
    public static string _TaxPer = "@TaxPer";
    public static string _TaxAmount = "@TaxAmount";
    public static string _NetAmount = "@NetAmount";
    public static string _ExptdDate = "@ExptdDate";
    public static string _PurchaseRate = "@PurchaseRate";
    public static string _DeliveryPeriod = "@DeliveryPeriod";
    public static string _QtyInHand = "@QtyInHand";
    public static string _QtyInTransite = "@QtyInTransite";


    #endregion

    #region[Definations]
    //--**PurchseOrder**--
    private Int32 m_Action;
    public Int32 Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    private Int32 m_POId;
    public Int32 POId
    {
        get { return m_POId; }
        set { m_POId = value; }
    }

    private string m_PONo;
    public string PONo
    {
        get { return m_PONo; }
        set { m_PONo = value; }
    }

    private DateTime m_PODate;
    public DateTime PODate
    {
        get { return m_PODate; }
        set { m_PODate = value; }
    }

    private Int32 m_SuplierId;
    public Int32 SuplierId
    {
        get { return m_SuplierId; }
        set { m_SuplierId = value; }
    }

    private string m_BillingAddress;
    public string BillingAddress
    {
        get { return m_BillingAddress; }
        set { m_BillingAddress = value; }
    }

    private string m_ShippingAddress;
    public string ShippingAddress
    {
        get { return m_ShippingAddress; }
        set { m_ShippingAddress = value; }
    }

    private decimal m_SubTotal;
    public decimal SubTotal
    {
        get { return m_SubTotal; }
        set { m_SubTotal = value; }
    }

    private decimal m_Discount;
    public decimal Discount
    {
        get { return m_Discount; }
        set { m_Discount = value; }
    }

    private decimal m_Vat;
    public decimal Vat
    {
        get { return m_Vat; }
        set { m_Vat = value; }
    }

    private decimal m_GrandTotal;
    public decimal GrandTotal
    {
        get { return m_GrandTotal; }
        set { m_GrandTotal = value; }
    }

    private string m_Instruction;
    public string Instruction
    {
        get { return m_Instruction; }
        set { m_Instruction = value; }
    }

    private string m_POStatus;
    public string POStatus
    {
        get { return m_POStatus; }
        set { m_POStatus = value; }
    }

    private DateTime m_GeneratedTime;
    public DateTime GeneratedTime
    {
        get { return m_GeneratedTime; }
        set { m_GeneratedTime = value; }
    }

    private DateTime m_ApprovedTime;
    public DateTime ApprovedTime
    {
        get { return m_ApprovedTime; }
        set { m_ApprovedTime = value; }
    }

    private DateTime m_AuthorizedTime;
    public DateTime AuthorizedTime
    {
        get { return m_AuthorizedTime; }
        set { m_AuthorizedTime = value; }
    }

    private Int32 m_UserId;
    public Int32 UserId
    {
        get { return m_UserId; }
        set { m_UserId = value; }
    }

    private DateTime m_LoginDate;
    public DateTime LoginDate
    {
        get { return m_LoginDate; }
        set { m_LoginDate = value; }
    }

    private bool m_IsDeleted;
    public bool IsDeleted
    {
        get { return m_IsDeleted; }
        set { m_IsDeleted = value; }
    }

    private string m_strCond;
    public string StrCond
    {
        get { return m_strCond; }
        set { m_strCond = value; }
    }


    //--**PurchaseOrdDtls**--
    private Int32 m_PODtlsId;
    public Int32 PODtlsId
    {
        get { return m_PODtlsId; }
        set { m_PODtlsId = value; }
    }

    private Int32 m_ItemId;
    public Int32 ItemId
    {
        get { return m_ItemId; }
        set { m_ItemId = value; }
    }

    private decimal m_Qty;
    public decimal Qty
    {
        get { return m_Qty; }
        set { m_Qty = value; }
    }

    private decimal m_Rate;
    public decimal Rate
    {
        get { return m_Rate; }
        set { m_Rate = value; }
    }

    private decimal m_Amount;
    public decimal Amount
    {
        get { return m_Amount; }
        set { m_Amount = value; }
    }

    private decimal m_TaxPer;
    public decimal TaxPer
    {
        get { return m_TaxPer; }
        set { m_TaxPer = value; }
    }

    private decimal m_TaxAmount;
    public decimal TaxAmount
    {
        get { return m_TaxAmount; }
        set { m_TaxAmount = value; }
    }

    private decimal m_NetAmount;
    public decimal NetAmount
    {
        get { return m_NetAmount; }
        set { m_NetAmount = value; }
    }

    private DateTime m_ExptdDate;
    public DateTime ExptdDate
    {
        get { return m_ExptdDate; }
        set { m_ExptdDate = value; }
    }

    private decimal m_PurchaseRate;
    public decimal PurchaseRate
    {
        get { return m_PurchaseRate; }
        set { m_PurchaseRate = value; }
    }

    private Int32 m_DeliveryPeriod;
    public Int32 DeliveryPeriod
    {
        get { return m_DeliveryPeriod; }
        set { m_DeliveryPeriod = value; }
    }

    private decimal m_QtyInHand;
    public decimal QtyInHand
    {
        get { return m_QtyInHand; }
        set { m_QtyInHand = value; }
    }

    private decimal m_QtyInTransite;
    public decimal QtyInTransite
    {
        get { return m_QtyInTransite; }
        set { m_QtyInTransite = value; }
    }

    #endregion

    #region[Store Procedures]
    public static string SP_PurchaseOrder = "SP_PurchaseOrder";
    public static string SP_PurchaseOrder_Part_I = "SP_PurchaseOrder_Part_I";
    public static string SP_PurchaseOrder_Part_II = "SP_PurchaseOrder_Part_II";

   
    public static string SP_EditPurchaseOrder = "SP_EditPurchaseOrder";
    #endregion

    public EditPurchaseOrder()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
