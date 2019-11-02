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


public class MaterialInwardReg
{

    #region[Constants]
    //--**Material Inward Register Master**--
	 public static string _Action="@Action";
     public static string _InwardId="@InwardId";
	 public static string _InwardNo="@InwardNo"; 	
	 public static string _InwardDate="@InwardDate";
     public static string _POId = "@POId";	
	 public static string _PONO="@PONO";
     public static string _BillNo = "@BillNo";//Add By Anand in Dated 17-Jan-2013
     public static string _InwardThrough = "@InwardThrough";//Add By Anand in Dated 17-Jan-2013
	 public static string _SuplierId="@SuplierId"; 
	 public static string _BillingAddress="@BillingAddress";	
	 public static string _ShippingAddress="@ShippingAddress";
	 
     public static string _SubTotal="@SubTotal";
     public static string _DiscountPer = "@DiscountPer";//Add By Anand in Dated 17-Jan-2013
     public static string _DiscountAmt = "@DiscountAmt";//Add By Anand in Dated 17-Jan-2013
     public static string _VatPer = "@VatPer";//Add By Anand in Dated 17-Jan-2013
     public static string _VatAmt = "@VatAmt";//Add By Anand in Dated 17-Jan-2013
     public static string _DekhrekhPer = "@DekhrekhPer";//Add By Anand in Dated 17-Jan-2013
     public static string _DekhrekhAmt = "@DekhrekhAmt";//Add By Anand in Dated 17-Jan-2013
     public static string _HamaliPer = "@HamaliPer";//Add By Anand in Dated 17-Jan-2013
     public static string _HamaliAmt = "@HamaliAmt";//Add By Anand in Dated 17-Jan-2013
     public static string _CESSPer = "@CESSPer";//Add By Anand in Dated 17-Jan-2013
     public static string _CESSAmt = "@CESSAmt";//Add By Anand in Dated 17-Jan-2013
     public static string _FreightPer = "@FreightPer";//Add By Anand in Dated 17-Jan-2013
     public static string _FreightAmt = "@FreightAmt";//Add By Anand in Dated 17-Jan-2013
     public static string _PackingPer = "@PackingPer";//Add By Anand in Dated 17-Jan-2013
     public static string _PackingAmt = "@PackingAmt";//Add By Anand in Dated 17-Jan-2013
     public static string _PostagePer = "@PostagePer";//Add By Anand in Dated 17-Jan-2013
     public static string _PostageAmt = "@PostageAmt";//Add By Anand in Dated 17-Jan-2013
     public static string _OtherCharges = "@OtherCharges";//Add By Anand in Dated 17-Jan-2013

    

	 public static string _GrandTotal="@GrandTotal";		
	 public static string _Instruction="@Instruction";

     public static string _VehicalNo = "@VehicalNo";//Add By Shweta in Dated 28-Oct-2013
     public static string _TimeIn = "@TimeIn";//Add By Shweta in Dated 28-Oct-2013
     public static string _TimeOut = "@TimeOut";//Add By Shweta in Dated 28-Oct-2013
	
	 public static string _UserId="@UserId";
	 public static string _LoginDate="@LoginDate"; 
	 public static string _IsDeleted="@IsDeleted"; 
	 public static string _strCond="@strCond"; 
	
	//--**Material Inward Register Details**--
	 public static string _InwardDtlsId="@InwardDtlsId";	
	 public static string _ItemId="@ItemId";
     public static string _ItemDesc = "@ItemDesc";
	 public static string _InwardQty="@InwardQty"; 	
	 public static string _OrderQty="@OrderQty"; 
	 public static string _PendingQty="@PendingQty";	
	 public static string _InwardRate="@InwardRate";		
	 public static string _PORate="@PORate";		
	 public static string _Diffrence="@Diffrence"; 
	 public static string _Amount="@Amount"; 
	 public static string _TaxPer="@TaxPer";		
	 public static string _TaxAmount="@TaxAmount";
     public static string _DiscPer = "@DiscPer";
     public static string _DiscAmt = "@DiscAmt"; 	    
	 public static string _NetAmount="@NetAmount"; 
	 public static string _ExpectedDate="@ExpectedDate"; 	
	 public static string _DeliveryDate="@DeliveryDate";
     public static string _UnitId= "@UnitId";
     public static string _StockDate= "@StockDate"; 
     public static string _StockLocationID= "@StockLocationID";
     public static string _Type = "@Type";
     public static string _LocID = "@LocID";
     public static string _ConversionUnitId = "@ConversionUnitId";
     public static string _ActualQty = "@ActualQty";

     public static string _BillDate = "@BillDate";
     public static string _UserInwardNo = "@UserInwardNo";
    #endregion

    #region[Definations]

    	//--**Material Inward Register Master**--


     private string m_ItemDesc;
     public string ItemDesc
     {
         get { return m_ItemDesc; }
         set { m_ItemDesc = value; }
     }
     private Int32 m_LocID;
     public Int32 LocID
     {
         get { return m_LocID; }
         set { m_LocID = value; }
     }  

     private string m_UserInwardNo;
     public string UserInwardNo
     {
         get { return m_UserInwardNo; }
         set { m_UserInwardNo = value; }
     }    
    private Int32 m_Action;
        public Int32 Action
        {
          get { return m_Action; }
          set { m_Action = value; }
        }

        private Int32 m_InwardId;
        public Int32 InwardId
        {
          get { return m_InwardId; }
          set { m_InwardId = value; }
        }

        private string m_InwardNo;
        public string InwardNo
        {
            get { return m_InwardNo; }
            set { m_InwardNo = value; }
        }

        private DateTime m_InwardDate;
        public DateTime InwardDate
        {
            get { return m_InwardDate; }
            set { m_InwardDate = value; }
        }

        private DateTime m_BillDate;
        public DateTime BillDate
        {
            get { return m_BillDate; }
            set { m_BillDate = value; }
        }

        private Int32 m_POId;

        public Int32 POId
        {
            get { return m_POId; }
            set { m_POId = value; }
        }

        private string m_PONO;
        public string PONO
        {
            get { return m_PONO; }
            set { m_PONO = value; }
        }

        private string m_Type;
        public string Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }
        private string m_BillNo;
        public string BillNo
        {
            get { return m_BillNo; }
            set { m_BillNo = value; }
        }

        private Int32 m_InwardThrough;
        public Int32 InwardThrough
        {
            get { return m_InwardThrough; }
            set { m_InwardThrough = value; }
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

        private decimal m_DiscountPer;
        public decimal DiscountPer
        {
            get { return m_DiscountPer; }
            set { m_DiscountPer = value; }
        }
        private decimal m_DiscountAmt;
        public decimal DiscountAmt
        {
            get { return m_DiscountAmt; }
            set { m_DiscountAmt = value; }
        }

        private decimal m_VatPer;
        public decimal VatPer
        {
            get { return m_VatPer; }
            set { m_VatPer = value; }
        }
        private decimal m_VatAmt;
        public decimal VatAmt
        {
            get { return m_VatAmt; }
            set { m_VatAmt = value; }
        }

        private decimal m_DekhrekhPer;
        public decimal DekhrekhPer
        {
            get { return m_DekhrekhPer; }
            set { m_DekhrekhPer = value; }
        }
        private decimal m_DekhrekhAmt;
        public decimal DekhrekhAmt
        {
            get { return m_DekhrekhAmt; }
            set { m_DekhrekhAmt = value; }
        }

        private decimal m_HamaliPer;
        public decimal HamaliPer
        {
            get { return m_HamaliPer; }
            set { m_HamaliPer = value; }
        }
        private decimal m_HamaliAmt;
        public decimal HamaliAmt
        {
            get { return m_HamaliAmt; }
            set { m_HamaliAmt = value; }
        }

        private decimal m_CESSPer;
        public decimal CESSPer
        {
            get { return m_CESSPer; }
            set { m_CESSPer = value; }
        }
        private decimal m_CESSAmt;
        public decimal CESSAmt
        {
            get { return m_CESSAmt; }
            set { m_CESSAmt = value; }
        }

        private decimal m_FreightPer;
        public decimal FreightPer
        {
            get { return m_FreightPer; }
            set { m_FreightPer = value; }
        }
        private decimal m_FreightAmt;
        public decimal FreightAmt
        {
            get { return m_FreightAmt; }
            set { m_FreightAmt = value; }
        }

        private decimal m_PackingPer;
        public decimal PackingPer
        {
            get { return m_PackingPer; }
            set { m_PackingPer = value; }
        }
        private decimal m_PackingAmt;
        public decimal PackingAmt
        {
            get { return m_PackingAmt; }
            set { m_PackingAmt = value; }
        }

        private decimal m_PostagePer;
        public decimal PostagePer
        {
            get { return m_PostagePer; }
            set { m_PostagePer = value; }
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

        private string m_VehicalNo;
        public string Vehical
        {
            get { return m_VehicalNo; }
            set { m_VehicalNo = value; }
        }

        private DateTime m_TimeOut;
        public DateTime TimeOut
        {
            get { return m_TimeOut; }
            set { m_TimeOut = value; }
        }
        private DateTime m_TimeIn;
        public DateTime TimeIn
        {
            get { return m_TimeIn; }
            set { m_TimeIn = value; }
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
	
	//--**Material Inward Register Details**--
        private Int32 m_InwardDtlsId;
        public Int32 InwardDtlsId
        {
            get { return m_InwardDtlsId; }
            set { m_InwardDtlsId = value; }
        }

        private Int32 m_ItemId;
        public Int32 ItemId
        {
            get { return m_ItemId; }
            set { m_ItemId = value; }
        }

        private decimal m_InwardQty;
        public decimal InwardQty
        {
            get { return m_InwardQty; }
            set { m_InwardQty = value; }
        }

        private decimal m_OrderQty;
        public decimal OrderQty
        {
            get { return m_OrderQty; }
            set { m_OrderQty = value; }
        }

        private decimal m_PendingQty;
        public decimal PendingQty
        {
            get { return m_PendingQty; }
            set { m_PendingQty = value; }
        }

        private decimal m_InwardRate;
        public decimal InwardRate
        {
            get { return m_InwardRate; }
            set { m_InwardRate = value; }
        }

        private decimal m_PORate;
        public decimal PORate
        {
            get { return m_PORate; }
            set { m_PORate = value; }
        }

        private decimal m_Diffrence;
        public decimal Diffrence
        {
            get { return m_Diffrence; }
            set { m_Diffrence = value; }
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

        private decimal m_NetAmount;
        public decimal NetAmount
        {
            get { return m_NetAmount; }
            set { m_NetAmount = value; }
        }

        private DateTime m_ExpectedDate;
        public DateTime ExpectedDate
        {
            get { return m_ExpectedDate; }
            set { m_ExpectedDate = value; }
        }

        private DateTime m_DeliveryDate;
        public DateTime DeliveryDate
        {
            get { return m_DeliveryDate; }
            set { m_DeliveryDate = value; }
        }

        private Int32 m_UnitId;
        public Int32 UnitId
        {
            get { return m_UnitId; }
            set { m_UnitId = value; }
        }

        private DateTime m_StockDate;
        public DateTime StockDate
        {
            get { return m_StockDate; }
            set { m_StockDate = value; }
        }

        private Int32 m_StockLocationID;
        public Int32 StockLocationID
        {
            get { return m_StockLocationID; }
            set { m_StockLocationID = value; }
        }

        private Int32 m_ConversionUnitID;
        public Int32 ConversionUnitId
        {
            get { return m_ConversionUnitID; }
            set { m_ConversionUnitID = value; }
        }

        private Decimal m_ActualQty;
        public Decimal ActualQty
        {
            get { return m_ActualQty; }
            set { m_ActualQty = value; }
        }
        #endregion

    #region[Store Procedure]
           public static string SP_MaterialInwardReg = "SP_MaterialInwardReg";
           public static string SP_MaterialInwardReport = "SP_MaterialInwardReport";
    #endregion

	public MaterialInwardReg()
    {
        


        //
		// TODO: Add constructor logic here
		//
	}
}
