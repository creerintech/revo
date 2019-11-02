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
/// Summary description for StockMaster
/// </summary>
public class StockMaster
{
    #region[Constants]
    //OutwardRegister
    public static string _Action = "@Action";
    public const string _OutwardId = "@OutwardId";
    public const string _StockNo = "@StockNo";
    public static string _StockAsOn = "@StockAsOn";
    public static string _ConsumptionDate= "@ConsumptionDate";
    public static string _Type = "@Type";
    public static string _Status = "@Status";
    public static string _UserId = "@UserId";
    public static string _LoginDate = "@LoginDate";
    public static string _IsDeleted = "@IsDeleted";
    public static string _strCond = "@strCond";
    //OutwardRegisterDtls
    public static string _OutwardDtlsId = "@OutwardDtlsId";
    public static string _InwardId = "@InwardId";
    public static string _ItemId = "@ItemId";
    public static string _InwardQty = "@InwardQty";
    public static string _OutwardQty = "@OutwardQty";
    public static string _LocationId = "@LocationId";
    public static string _PendingQty = "@PendingQty";
    public static string _CategoryId = "@CategoryId";
    public static string _Rate = "@IRate";
    public static string _Amount= "@Amount";
    public static string _StockLocationId = "@StockLocationID";
    public static string _Remark = "@Remark";
    public static string _UnitConvDtlsId = "@UnitConvDtlsId";
    public static string _ItemDetailsId = "@ItemDetailsId";
    public static string _ItemDesc = "@ItemDesc";

    #endregion

    #region[Definations]
    private string m_Remark;
    public string Remark
    {
        get { return m_Remark; }
        set { m_Remark = value; }
    }

    private DateTime m_ConsumptionDate;
    public DateTime ConsumptionDate
    {
        get { return m_ConsumptionDate; }
        set { m_ConsumptionDate = value; }
    }

    private string m_Status;
    public string Status
    {
        get { return m_Status; }
        set { m_Status = value; }
    }

    private Decimal m_Rate;
    public Decimal Rate
    {
        get { return m_Rate; }
        set { m_Rate = value; }
    }

    private Decimal m_Amount;
    public Decimal Amount
    {
        get { return m_Amount; }
        set { m_Amount= value; }
    }


    private Int32 m_StockLocationID;
    public Int32 StockLocationID
    {
        get { return m_StockLocationID; }
        set { m_StockLocationID = value; }
    }
    
    private string m_Type;
    public string Type
    {
        get { return m_Type; }
        set { m_Type = value; }
    }

    private Int32 m_CategoryId;
    public Int32 CategoryId
    {
        get { return m_CategoryId; }
        set { m_CategoryId = value; }
    }

    private Int32 m_Action;
    public Int32 Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    private Int32 m_OutwardId;
    public Int32 OutwardId
    {
        get { return m_OutwardId; }
        set { m_OutwardId = value; }
    }
    
    private string m_StockNo;
    public string StockNo
    {
        get { return m_StockNo; }
        set { m_StockNo = value; }
    }

    private DateTime m_StockAsOn;
    public DateTime StockAsOn
    {
        get { return m_StockAsOn; }
        set { m_StockAsOn = value; }
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

    private Int32 m_OutwardDtlsId;
    public Int32 OutwardDtlsId
    {
        get { return m_OutwardDtlsId; }
        set { m_OutwardDtlsId = value; }
    }

    private Int32 m_InwardId;

    public Int32 InwardId
    {
        get { return m_InwardId; }
        set { m_InwardId = value; }
    }

    private Int32 m_ItemId;
    public Int32 ItemId
    {
        get { return m_ItemId; }
        set { m_ItemId = value; }
    }

    private Decimal m_InwardQty;
    public Decimal InwardQty
    {
        get { return m_InwardQty; }
        set { m_InwardQty = value; }
    }

    private Decimal m_OutwardQty;
    public Decimal OutwardQty
    {
        get { return m_OutwardQty; }
        set { m_OutwardQty = value; }
    }

    private Decimal m_PendingQty;
    public Decimal PendingQty
    {
        get { return m_PendingQty; }
        set { m_PendingQty = value; }
    }

    private Int32 m_LocationId;
    public Int32 LocationId
    {
        get { return m_LocationId; }
        set { m_LocationId = value; }
    }

    private int m_UnitConvDtlsId;

    public int UnitConvDtlsId
    {
        get { return m_UnitConvDtlsId; }
        set { m_UnitConvDtlsId = value; }
    }


    private int m_ItemDetailsId;

    public int ItemDetailsId
    {
        get { return m_ItemDetailsId; }
        set { m_ItemDetailsId = value; }
    }

    private string m_ItemDesc;

    public string ItemDesc
    {
        get { return m_ItemDesc; }
        set { m_ItemDesc = value; }
    }








    #endregion

    #region[Store Procedures]
    public static string SP_StockMaster = "SP_StockMasterNew";
    public static string SP_StockMasterReport = "SP_StockMasterNewReport";
    public static string SP_StockMaster1 = "SP_StockMasterNew1";
    #endregion

    public StockMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
