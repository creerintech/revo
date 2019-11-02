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
/// Summary description for Deviation
/// </summary>
public class Deviation
{
    #region [Constant]---------------------------------------
    public static string _Action = "@Action";
    public static string _DeviationID = "@DeviationID";
    public static string _DeviationNo = "@DeviationNo";
    public static string _LoginID = "@LoginID";
    public static string _LoginDate = "@LoginDate";
    public static string _DeviationDtlsID = "@DeviationDtlsID";
    public static string _ItemID = "@ItemID";
    public static string _UnitID = "@UnitID";
    public static string _Rate = "@Rate";
    public static string _LocationID = "@LocationID";
    public static string _SysClosing = "@SysClosing";
    public static string _PhyClosing = "@PhyClosing";
    public static string _Deviation1 = "@Deviation1";
    public static string _StrCondition = "@StrCondition";
    public static string _DeviationFrom = "@DeviationFrom";
    public static string _DeviationTo = "@DeviationTo";
    public static string _PhyRate = "@PhyRate";
    public static string _SysAmt = "@SysAmt";
    public static string _PhyAmt = "@PhyAmt";
    #endregion------------------------------------------------

    #region [Defination]--------------------------------------
    private DateTime m_DeviationFrom;

    public DateTime DeviationFrom
    {
        get { return m_DeviationFrom; }
        set { m_DeviationFrom = value; }
    }

    private DateTime m_DeviationTo;

    public DateTime DeviationTo
    {
        get { return m_DeviationTo; }
        set { m_DeviationTo = value; }
    }

    private String m_StrCondition;

    public String StrCondition
    {
        get { return m_StrCondition; }
        set { m_StrCondition = value; }
    }
    
    private Decimal m_Deviation1;

    public Decimal Deviation1
    {
        get { return m_Deviation1; }
        set { m_Deviation1 = value; }
    }
    
    private Decimal m_PhyClosing;

    public Decimal PhyClosing
    {
        get { return m_PhyClosing; }
        set { m_PhyClosing = value; }
    }
    
    private Decimal m_SysClosing;

    public Decimal SysClosing
    {
        get { return m_SysClosing; }
        set { m_SysClosing = value; }
    }
    
    private Int32 m_LocationID;

    public Int32 LocationID
    {
        get { return m_LocationID; }
        set { m_LocationID = value; }
    }
    
    private Decimal m_Rate;

    public Decimal Rate
    {
        get { return m_Rate; }
        set { m_Rate = value; }
    }

    private Decimal m_PhyRate;

    public Decimal PhyRate
    {
        get { return m_PhyRate; }
        set { m_PhyRate = value; }
    }
    
    private Int32 m_UnitID;

    public Int32 UnitID
    {
        get { return m_UnitID; }
        set { m_UnitID = value; }
    }
    
    private Int32 m_ItemID;

    public Int32 ItemID
    {
        get { return m_ItemID; }
        set { m_ItemID = value; }
    }
    
    private Int32 m_DeviationDtlsID;

    public Int32 DeviationDtlsID
    {
        get { return m_DeviationDtlsID; }
        set { m_DeviationDtlsID = value; }
    }
    
    private DateTime m_LoginDate;

    public DateTime LoginDate
    {
        get { return m_LoginDate; }
        set { m_LoginDate = value; }
    }
    
    private Int32 m_LoginID;

    public Int32 LoginID
    {
        get { return m_LoginID; }
        set { m_LoginID = value; }
    }
    
    private String m_DeviationNo;

    public String DeviationNo
    {
        get { return m_DeviationNo; }
        set { m_DeviationNo = value; }
    }
    private Int32 m_DeviationID;

    public Int32 DeviationID
    {
        get { return m_DeviationID; }
        set { m_DeviationID = value; }
    }
    private Int32 m_Action;

    public Int32 Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    private Decimal m_PhyAmt;

    public Decimal PhyAmt
    {
        get { return m_PhyAmt; }
        set { m_PhyAmt = value; }
    }

    private Decimal m_SysAmt;

    public Decimal SysAmt
    {
        get { return m_SysAmt; }
        set { m_SysAmt = value; }
    }
    #endregion------------------------------------------------

    #region [Stored Procedure]--------------------------------
    public static string SP_Deviation = "SP_Deviation";
    #endregion------------------------------------------------

    public Deviation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
