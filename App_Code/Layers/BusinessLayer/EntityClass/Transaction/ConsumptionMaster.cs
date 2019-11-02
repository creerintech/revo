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
/// Summary description for ConsumptionMaster
/// </summary>
public class ConsumptionMaster
{
    #region[Constants]
    
    public static string _Action = "@Action";
    public const string _ConsumptionId = "@ConsumptionId";
    public const string _ConsumptionNo = "@ConsumptionNo";
    public static string _ConsumptionAsOn = "@ConsumptionAsOn";
    public static string _ConsumptionDate= "@ConsumptionDate";
    public static string _UserId = "@UserId";
    public static string _LoginDate = "@LoginDate";
    public static string _IsDeleted = "@IsDeleted";
    public static string _strCond = "@strCond";

    public static string _ConsumptionDtlsId = "@ConsumptionDtlsId";
    public static string _ItemId = "@ItemId";
    public static string _IssueId = "@IssueId";
    public static string _IssueQty = "@IssueQty";
    public static string _ConsumeQty = "@ConsumeQty";
    public static string _LocationId = "@LocationId";
    public static string _PendingQty = "@PendingQty";
    public static string _CategoryId = "@CategoryId";
    public static string _Rate = "@IRate";
    public static string _Amount= "@Amount";
    public static string _StockLocationId = "@StockLocationID";

    public static string _ItemDetailsId = "@ItemDetailsId";
    public static string _ItemDesc = "@ItemDesc";

    #endregion

    #region[Definations]

    private Int32 m_IssueId;
    public Int32 IssueId
    {
        get { return m_IssueId; }
        set { m_IssueId = value; }
    }

    private DateTime m_ConsumptionDate;
    public DateTime ConsumptionDate
    {
        get { return m_ConsumptionDate; }
        set { m_ConsumptionDate = value; }
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

    private Int32 m_ConsumptionId;
    public Int32 ConsumptionId
    {
        get { return m_ConsumptionId; }
        set { m_ConsumptionId = value; }
    }

    private string m_ConsumptionNo;
    public string ConsumptionNo
    {
        get { return m_ConsumptionNo; }
        set { m_ConsumptionNo = value; }
    }

    private DateTime m_ConsumptionAsOn;
    public DateTime ConsumptionAsOn
    {
        get { return m_ConsumptionAsOn; }
        set { m_ConsumptionAsOn = value; }
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

    private Int32 m_ConsumptionDtlsId;
    public Int32 ConsumptionDtlsId
    {
        get { return m_ConsumptionDtlsId; }
        set { m_ConsumptionDtlsId = value; }
    }

    private Int32 m_ItemId;
    public Int32 ItemId
    {
        get { return m_ItemId; }
        set { m_ItemId = value; }
    }

    private Decimal m_IssueQty;
    public Decimal IssueQty
    {
        get { return m_IssueQty; }
        set { m_IssueQty = value; }
    }

    private Decimal m_ConsumeQty;
    public Decimal ConsumeQty
    {
        get { return m_ConsumeQty; }
        set { m_ConsumeQty = value; }
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



    private Int32 m_ItemDetailsId;
    public Int32 ItemDetailsId
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
    public static string SP_CosumptionMaster = "SP_Consumption";
    public static string SP_ConsumptionMasterReport = "SP_ConsumptionReport";
    #endregion

    public ConsumptionMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
