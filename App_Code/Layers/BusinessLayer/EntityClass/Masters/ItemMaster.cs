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
/// Summary description for ItemMaster
/// </summary>
public class ItemMaster
{
    #region Constants
    //------------ItemMaster
    public static string _Action = "@Action";
    public static string _ItemCode = "@ItemCode";
    public static string _Barcode = "@Barcode";
    public static string _ItemId = "@ItemId";
    public static string _ItemName = "@ItemName";
    public static string _CategoryId = "@CategoryId";
    public static string _SubcategoryId = "@SubcategoryId";
    public static string _ItemRemark = "@Remark";
    public static string _TaxPer = "@TaxPer";
    public static string _MinStockLevel = "@MinStockLevel";
    public static string _ReorderLevel = "@ReorderLevel";
    public static string _MaxStockLevel = "@MaxStockLevel";
    public static string _OpeningStock = "@OpeningStock";
     public static string _MinStockSupp= "@MinStockSupp";
	 public static string _MaxStockSupp = "@MaxStockSupp";
     public static string _ReorderSupp = "@ReorderSupp";
    public static string _DeliveryPeriod = "@DeliveryPeriod";
    public static string _AsOn = "@AsOn";
    public static string _StockLocationID = "@StockLocationID";
    public static string _UnitId = "@UnitId";
    public static string _IsClub = "@IsClub";
    public static string _UserId = "@UserId";
    public static string _LoginDate = "@LoginDate";
    public static string _IsDeleted = "@IsDeleted";
    public static string _strCond = "@strCond";
    public static string _HSNCode = "@HSNCode";
    //---------ItemDetails
    public static string _ItemDetailsId = "@ItemDetailsId";
    public static string _PurchaseRate = "@PurchaseRate";
    public static string _SupplierId = "@SupplierId";
    public static string _IsKitchenAssign = "@IsKitchenAssign";

    public static string _OpeningStockDetails = "@OpeningStockDetails";
    public static string _LocationIDDetails = "@LocationIDDetails";
    public static string _ItemDesc = "@ItemDesc";

    //----------ItemDtlsUnitConversion
    public static string _ItemDtlsUnitId = "@ItemDtlsUnitId";
    public static string _UnitConvId = "@UnitConvId";
    public static string _UnitConvDtlsId = "@UnitConvDtlsId";

    //----------Item Unit Entry New----------------------
    public static string _ItemUnitMasterID = "@ItemUnitMasterID";
    public static string _ItemID = "@ItemID";
    public static string _FromQty = "@FromQty";
    public static string _FromUnitID = "@FromUnitID";
    public static string _ToQty = "@ToQty";
    public static string _ToUnitID = "@ToUnitID";

    public static string _ItemUnitDetailsID = "@ItemUnitDetailsID";
    public static string _ItemMasterID = "@ItemMasterID";
    //----------END HERE --------------------------------

    //----------ItemDtlsUnitCalculation
    public static string _From_Factor = "@From_Factor";
    public static string _From_UnitID = "@From_UnitID";
    public static string _To_Factor = "@To_Factor";
    public static string _To_UnitID = "@To_UnitID";
    public static string _Factor_Desc = "@Factor_Desc";
    public static string _DrawingNo = "@DrawingNo";
    public static string _DrawingPath = "@DrawingPath";
    public static string _TaxDtlsId = "@TaxDtlsId";
    public static string _TaxTemplateID = "@TaxTemplateID";

    #endregion

    #region Definition
    //*****ItemMaster
    public string Remark { get; set; }
    public string HSNCode { get; set; }

    public Int32 TaxTemplateID { get; set; }
    private decimal m_MinStockSupp;
    public decimal MinStockSupp
    {
        get { return m_MinStockSupp; }
        set { m_MinStockSupp = value; }
    }

    private decimal m_MaxStockSupp;
    public decimal MaxStockSupp
    {
        get { return m_MaxStockSupp; }
        set { m_MaxStockSupp = value; }
    }

    private decimal m_ReorderSupp;
    public decimal ReorderSupp
    {
        get { return m_ReorderSupp; }
        set { m_ReorderSupp = value; }
    }

    private string m_DrawingPath;
    public string DrawingPath
    {
        get { return m_DrawingPath; }
        set { m_DrawingPath = value; }
    }

    private Int32 m_IsClub;
    public Int32 IsClub
    {
        get { return m_IsClub; }
        set { m_IsClub = value; }
    }

    private Decimal m_OpeningStockDetails;
    public Decimal OpeningStockDetails
    {
        get { return m_OpeningStockDetails; }
        set { m_OpeningStockDetails = value; }
    }

    private Int32 m_LocationIDDetails;
    public Int32 LocationIDDetails
    {
        get { return m_LocationIDDetails; }
        set { m_LocationIDDetails = value; }
    }


    private Int32 m_IsKitchenAssign;
    public Int32 IsKitchenAssign
    {
        get { return m_IsKitchenAssign; }
        set { m_IsKitchenAssign = value; }
    }

    private Int32 m_Action;
    public Int32 Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    private Int32 m_ItemId;
    public Int32 ItemId
    {
        get { return m_ItemId; }
        set { m_ItemId = value; }
    }

    private string m_ItemCode;
    public string ItemCode
    {
        get { return m_ItemCode; }
        set { m_ItemCode = value; }
    }

    private string m_Barcode;
    public string Barcode
    {
        get { return m_Barcode; }
        set { m_Barcode = value; }
    }

    private string m_ItemName;
    public string ItemName
    {
        get { return m_ItemName; }
        set { m_ItemName = value; }
    }

    private Int32 m_CategoryId;
    public Int32 CategoryId
    {
        get { return m_CategoryId; }
        set { m_CategoryId = value; }
    }

    private Int32 m_SubcategoryId;
    public Int32 SubcategoryId
    {
        get { return m_SubcategoryId; }
        set { m_SubcategoryId = value; }
    }

    private decimal m_TaxPer;
    public decimal TaxPer
    {
        get { return m_TaxPer; }
        set { m_TaxPer = value; }
    }

    private string m_MinStockLevel;
    public string MinStockLevel
    {
        get { return m_MinStockLevel; }
        set { m_MinStockLevel = value; }
    }

    private string m_ReorderLevel;
    public string ReorderLevel
    {
        get { return m_ReorderLevel; }
        set { m_ReorderLevel = value; }
    }

    private string m_MaxStockLevel;
    public string MaxStockLevel
    {
        get { return m_MaxStockLevel; }
        set { m_MaxStockLevel = value; }
    }

    private decimal m_OpeningStock;
    public decimal OpeningStock
    {
        get { return m_OpeningStock; }
        set { m_OpeningStock = value; }
    }

    private Int32 m_DeliveryPeriod;
    public Int32 DeliveryPeriod
    {
        get { return m_DeliveryPeriod; }
        set { m_DeliveryPeriod = value; }
    }

    private DateTime m_AsOn;
    public DateTime AsOn
    {
        get { return m_AsOn; }
        set { m_AsOn = value; }
    }

    private Int32 m_StockLocationID;
    public Int32 StockLocationID
    {
        get { return m_StockLocationID; }
        set { m_StockLocationID = value; }
    }
    private int m_UnitId;
    public Int32 UnitId
    {
        get { return m_UnitId; }
        set { m_UnitId = value; }
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

    //****ItemDetails
    private Int32 m_ItemDetailsId;
    public Int32 ItemDetailsId
    {
        get { return m_ItemDetailsId; }
        set { m_ItemDetailsId = value; }
    }

    private Int32 m_SupplierId;
    public Int32 SupplierId
    {
        get { return m_SupplierId; }
        set { m_SupplierId = value; }
    }

    private decimal m_PurchaseRate;
    public decimal PurchaseRate
    {
        get { return m_PurchaseRate; }
        set { m_PurchaseRate = value; }
    }

    private string m_ItemDesc;
    public string ItemDesc
    {
        get { return m_ItemDesc; }
        set { m_ItemDesc = value; }
    }

    //****ItemDtlsUnitConversions
    private Int32 m_ItemDtlsUnitId;
    public Int32 ItemDtlsUnitId
    {
        get { return m_ItemDtlsUnitId; }
        set { m_ItemDtlsUnitId = value; }
    }
    private Int32 m_UnitConvId;
    public Int32 UnitConvId
    {
        get { return m_UnitConvId; }
        set { m_UnitConvId = value; }
    }
    private Int32 m_UnitConvDtlsId;
    public Int32 UnitConvDtlsId
    {
        get { return m_UnitConvDtlsId; }
        set { m_UnitConvDtlsId = value; }
    }


    //----------Item New Unit Conversion
    private Int32 m_ItemUnitMasterID;
    public Int32 ItemUnitMasterID
    {
        get { return m_ItemUnitMasterID; }
        set { m_ItemUnitMasterID = value; }
    }

    private Int32 m_ItemID;
    public Int32 ItemID
    {
        get { return m_ItemID; }
        set { m_ItemID = value; }
    }

    private Decimal m_FromQty;
    public Decimal FromQty
    {
        get { return m_FromQty; }
        set { m_FromQty = value; }
    }

    private Int32 m_FromUnitID;
    public Int32 FromUnitID
    {
        get { return m_FromUnitID; }
        set { m_FromUnitID = value; }
    }

    private Decimal m_ToQty;
    public Decimal ToQty
    {
        get { return m_ToQty; }
        set { m_ToQty = value; }
    }

    private Int32 m_ToUnitID;
    public Int32 ToUnitID
    {
        get { return m_ToUnitID; }
        set { m_ToUnitID = value; }
    }

    private Int32 m_ItemUnitDetailsID;
    public Int32 ItemUnitDetailsID
    {
        get { return m_ItemUnitDetailsID; }
        set { m_ItemUnitDetailsID = value; }
    }

    private Int32 m_ItemMasterID;
    public Int32 ItemMasterID
    {
        get { return m_ItemMasterID; }
        set { m_ItemMasterID = value; }
    }
    //----------END HERE

    //---- Add Field By Anand on Dated 24 Mar 2014(For Unit Calculation)

    //From_Factor
    private Decimal m_From_Factor;
    public Decimal From_Factor
    {
        get { return m_From_Factor; }
        set { m_From_Factor = value; }
    }
    //From_UnitID
    private Int32 m_From_UnitID;
    public Int32 From_UnitID
    {
        get { return m_From_UnitID; }
        set { m_From_UnitID = value; }
    }
    //To_Factor
    private Decimal m_To_Factor;
    public Decimal To_Factor
    {
        get { return m_To_Factor; }
        set { m_To_Factor = value; }
    }
    //To_UnitID
    private Int32 m_To_UnitID;
    public Int32 To_UnitID
    {
        get { return m_To_UnitID; }
        set { m_To_UnitID = value; }
    }
    //Factor_Desc
    private String m_Factor_Desc;
    public String Factor_Desc
    {
        get { return m_Factor_Desc; }
        set { m_Factor_Desc = value; }
    }
    private String m_DrawingNo;
    public String DrawingNo
    {
        get { return m_DrawingNo; }
        set { m_DrawingNo = value; }
    }
    public Int32 TaxDtlsId
    {
        get;
        set;
    }

    #endregion

    #region Procedure
    public static string SP_ItemMaster = "SP_ItemMaster";
    #endregion

    public ItemMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
