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
/// Summary description for GeneratePO
/// </summary>
public class GeneratePO
{
    #region Constants
        
        public static string _CentralPOId = "@CentralPOId";
	    public static string _CentralPONo="@CentralPONo";
	    public static string _CentralPODate="@CentralPODate";
	    public static string _DepartmentId="@DepartmentId";
	    public static string _ItemId="@ItemId";
	    public static string _ReqQty="@ReqQty";
	    public static string _QtyInHand="@QtyInHand";
	    public static string _MinStockLevel="@MinStockLevel";
	    public static string _LocationId="@LocationId";
	    public static string _TransitQty="@TransitQty";
	    public static string _Vendor="@Vendor";
	    public static string _QtyToOrder="@QtyToOrder";
	    public static string _OrderQty="@OrderQty";
	    public static string _TotalAmount="@TotalAmount";
	    public static string _UserId="@UserId";
	    public static string _LoginDate="@LoginDate";
	    public static string _IsDeleted="@IsDeleted";
        public static string _strCond = "@strCond";
    	
	    public static string _VendorDistId="@VendorDistId";
	    public static string _VendorId="@VendorId";
        public static string _OrderQtyDist = "@OrderQtyDist";
	    public static string _ExpectedAmt="@ExpectedAmt";
        public static string _PurchaseRate= "@PurchaseRate";

    #endregion

    #region Definition
        private Int32 m_CentralPOId;
        public Int32 CentralPOId
        {
            get { return m_CentralPOId; }
            set { m_CentralPOId = value; }
        }

        private string m_CentralPONo;
        public string CentralPONo
        {
            get { return m_CentralPONo; }
            set { m_CentralPONo = value; }
        }

        private DateTime m_CentralPODate;
        public DateTime CentralPODate
        {
            get { return m_CentralPODate; }
            set { m_CentralPODate = value; }
        }

        private Int32 m_DepartmentId;
        public Int32 DepartmentId
        {
            get { return m_DepartmentId; }
            set { m_DepartmentId = value; }
        }

        private Int32 m_ItemId;
        public Int32 ItemId
        {
            get { return m_ItemId; }
            set { m_ItemId = value; }
        }

        private decimal m_ReqQty;
        public decimal ReqQty
        {
            get { return m_ReqQty; }
            set { m_ReqQty = value; }
        }

        private decimal m_QtyInHand;
        public decimal QtyInHand
        {
            get { return m_QtyInHand; }
            set { m_QtyInHand = value; }
        }

        private decimal m_MinStockLevel;
        public decimal MinStockLevel
        {
            get { return m_MinStockLevel; }
            set { m_MinStockLevel = value; }
        }

        private Int32 m_LocationId;
        public Int32 LocationId
        {
            get { return m_LocationId; }
            set { m_LocationId = value; }
        }

        private decimal m_TransitQty;
        public decimal TransitQty
        {
            get { return m_TransitQty; }
            set { m_TransitQty = value; }
        }

        private string m_Vendor;
        public string Vendor
        {
            get { return m_Vendor; }
            set { m_Vendor = value; }
        }

        private decimal m_QtyToOrder;
        public decimal QtyToOrder
        {
            get { return m_QtyToOrder; }
            set { m_QtyToOrder = value; }
        }

        private decimal m_OrderQty;
        public decimal OrderQty
        {
            get { return m_OrderQty; }
            set { m_OrderQty = value; }
        }

        private decimal m_TotalAmount;
        public decimal TotalAmount
        {
            get { return m_TotalAmount; }
            set { m_TotalAmount = value; }
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


        private Int32 m_VendorDistId;
        public Int32 VendorDistId
        {
            get { return m_VendorDistId; }
            set { m_VendorDistId = value; }
        }

        private Int32 m_VendorId;
        public Int32 VendorId
        {
            get { return m_VendorId; }
            set { m_VendorId = value; }
        }

        private decimal m_OrderQtyDist;
        public decimal OrderQtyDist
        {
            get { return m_OrderQtyDist; }
            set { m_OrderQtyDist = value; }
        }

        private decimal m_ExpectedAmt;
        public decimal ExpectedAmt
        {
            get { return m_ExpectedAmt; }
            set { m_ExpectedAmt = value; }
        }

        private decimal m_PurchaseRate;

        public decimal PurchaseRate
        {
            get { return m_PurchaseRate; }
            set { m_PurchaseRate = value; }
        }

    #endregion

    #region Procedure
        public static string SP_GeneratePO = "SP_GeneratePO";
    #endregion

    public GeneratePO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
