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
/// Summary description for EmailPO
/// </summary>
public class EmailPO
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
    public static string _UserId = "@UserId";
    public static string _LoginDate = "@LoginDate";
    public static string _IsDeleted = "@IsDeleted";
    public static string _strCond = "@strCond";//@RequisitionCafeId
    public static string _RequisitionCafeId = "@RequisitionCafeId";
    public static string _ServiceTaxPer = "@ServiceTaxPer";
    public static string _ServiceTaxAmt = "@ServiceTaxAmt";
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
    public static string _TermsID = "@TermsID";
    public static string _Title = "@Title";
    public static string _TermsCondition = "@TermsCondition";


    public static string _DekhrekhAmt = "@DekhrekhAmt";
    public static string _HamaliAmt = "@HamaliAmt";
    public static string _CESSAmt = "@CESSAmt";
    public static string _FreightAmt = "@FreightAmt";
    public static string _PackingAmt = "@PackingAmt";
    public static string _PostageAmt = "@PostageAmt";
    public static string _OtherCharges = "@OtherCharges";
    public static string _FK_UnitConvDtlsId = "@FK_UnitConvDtlsId";
    public static string _MainQty = "@MainQty";
    public static string _FK_ItemDtlsId = "@FK_ItemDtlsId";
    public static string _DiscPer = "@DiscPer";
    public static string _DiscAmt = "@DiscAmt";

    public static string _PaymentTerms = "@PaymentTerms";
    public static string _RemarkForPO = "@RemarkForPO";
    public static string _CompanyID = "@CompanyID";

    public static string _POQTDATE = "@POQTDATE";

    public static string _HamaliActual = "@HamaliActual";
    public static string _FreightActual = "@FreightActual";
    public static string _OtherChargeActual = "@OtherChargeActual";
    public static string _LoadingActual = "@LoadingActual";
    public static string _ExcisePer = "@ExcisePer";
    public static string _ExciseAmount = "@ExciseAmount";
    public static string _CHKFLAG = "@CHKFLAG";
    public static string _InstallationRemark = "@InstallationRemark";
    public static string _InstallationCharge = "@InstallationCharge";
    public static string _InstallationSerTaxPer = "@InstallationSerTaxPer";
    public static string _InstallationSerTaxAmt = "@InstallationSerTaxAmt";
    #endregion

    #region[Definations]
    //--**PurchseOrder**--
    private Decimal m_InstallationSerTaxAmt;
    public Decimal InstallationSerTaxAmt
    {
        get { return m_InstallationSerTaxAmt; }
        set { m_InstallationSerTaxAmt = value; }
    }

    private Decimal m_InstallationSerTaxPer;
    public Decimal InstallationSerTaxPer
    {
        get { return m_InstallationSerTaxPer; }
        set { m_InstallationSerTaxPer = value; }
    }

    private Decimal m_InstallationCharge;
    public Decimal InstallationCharge
    {
        get { return m_InstallationCharge; }
        set { m_InstallationCharge = value; }
    }

    private String m_InstallationRemark;
    public String InstallationRemark
    {
        get { return m_InstallationRemark; }
        set { m_InstallationRemark = value; }
    }

    private Int32 m_CHKFLAG;
    public Int32 CHKFLAG
    {
        get { return m_CHKFLAG; }
        set { m_CHKFLAG = value; }
    }


    private decimal m_ExcisePer;
    public decimal ExcisePer
    {
        get { return m_ExcisePer; }
        set { m_ExcisePer = value; }
    }

    private decimal m_ExciseAmount;
    public decimal ExciseAmount
    {
        get { return m_ExciseAmount; }
        set { m_ExciseAmount = value; }
    }
    private Int32 m_HamaliActual;
    public Int32 HamaliActual
    {
        get { return m_HamaliActual; }
        set { m_HamaliActual = value; }
    }

    private Int32 m_FreightActual;
    public Int32 FreightActual
    {
        get { return m_FreightActual; }
        set { m_FreightActual = value; }
    }

    private Int32 m_OtherChargeActual;
    public Int32 OtherChargeActual
    {
        get { return m_OtherChargeActual; }
        set { m_OtherChargeActual = value; }
    }

    private Int32 m_LoadingActual;
    public Int32 LoadingActual
    {
        get { return m_LoadingActual; }
        set { m_LoadingActual = value; }
    }


    private String m_POQTDATE;
    public String POQTDATE
    {
        get { return m_POQTDATE; }
        set { m_POQTDATE = value; }
    }

    private decimal m_ServiceTaxPer;
    public decimal ServiceTaxPer
    {
        get { return m_ServiceTaxPer; }
        set { m_ServiceTaxPer = value; }
    }

    private decimal m_ServiceTaxAmt;
    public decimal ServiceTaxAmt
    {
        get { return m_ServiceTaxAmt; }
        set { m_ServiceTaxAmt = value; }
    }

    private String m_RemarkForPO;
    public String RemarkForPO
    {
        get { return m_RemarkForPO; }
        set { m_RemarkForPO = value; }
    }

    private Int32 m_FK_UnitConvDtlsId;
    public Int32 FK_UnitConvDtlsId
    {
        get { return m_FK_UnitConvDtlsId; }
        set { m_FK_UnitConvDtlsId = value; }
    }

    private Decimal m_MainQty;
    public Decimal MainQty
    {
        get { return m_MainQty; }
        set { m_MainQty = value; }
    }

    private Int32 m_FK_ItemDtlsId;
    public Int32 FK_ItemDtlsId
    {
        get { return m_FK_ItemDtlsId; }
        set { m_FK_ItemDtlsId = value; }
    }

    private Int32 m_CompanyID;
    public Int32 CompanyID
    {
        get { return m_CompanyID; }
        set { m_CompanyID = value; }
    }

    private Int32 m_PaymentTerms;
    public Int32 PaymentTerms
    {
        get { return m_PaymentTerms; }
        set { m_PaymentTerms = value; }
    }


    private decimal m_DekhrekhAmt;
    public decimal DekhrekhAmt
    {
        get { return m_DekhrekhAmt; }
        set { m_DekhrekhAmt = value; }
    }


    private decimal m_HamaliAmt;
    public decimal HamaliAmt
    {
        get { return m_HamaliAmt; }
        set { m_HamaliAmt = value; }
    }


    private decimal m_CESSAmt;
    public decimal CESSAmt
    {
        get { return m_CESSAmt; }
        set { m_CESSAmt = value; }
    }


    private decimal m_FreightAmt;
    public decimal FreightAmt
    {
        get { return m_FreightAmt; }
        set { m_FreightAmt = value; }
    }


    private decimal m_PackingAmt;
    public decimal PackingAmt
    {
        get { return m_PackingAmt; }
        set { m_PackingAmt = value; }
    }


    private decimal m_DiscPer;
    public decimal DiscPer
    {
        get { return m_DiscPer; }
        set { m_DiscPer = value; }
    }

    private decimal m_DiscAmt;
    public decimal DiscAmt
    {
        get { return m_DiscAmt; }
        set { m_DiscAmt = value; }
    }



    private decimal m_PostageAmt;
    public decimal PostageAmt
    {
        get { return m_PostageAmt; }
        set { m_PostageAmt = value; }
    }

    private decimal m_OtherCharges;
    public decimal OtherCharges
    {
        get { return m_OtherCharges; }
        set { m_OtherCharges = value; }
    }




    private String m_TermsCondition;

    public String TermsCondition
    {
        get { return m_TermsCondition; }
        set { m_TermsCondition = value; }
    }
    private String m_Title;

    public String Title
    {
        get { return m_Title; }
        set { m_Title = value; }
    }
    private Int32 m_RequisitionCafeId;

    public Int32 RequisitionCafeId
    {
        get { return m_RequisitionCafeId; }
        set { m_RequisitionCafeId = value; }
    }
    private Int32 m_TermsID;

    public Int32 TermsID
    {
        get { return m_TermsID; }
        set { m_TermsID = value; }
    }//RequisitionCafeId
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

    public static string SP_EmailPurchaseOrder = "SP_EmailPurchaseOrder";
    public static string SP_PurchaseOrder_Part_II = "SP_PurchaseOrder_Part_II";

    #endregion

	public EmailPO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
