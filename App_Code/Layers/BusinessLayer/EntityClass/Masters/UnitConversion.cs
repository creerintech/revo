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
/// Summary description for UnitConversion
/// </summary>
public class UnitConversion
{
    #region [Constants]

    public static string _Action = "@Action";
    public static string _UnitConvId="@UnitConvId";
	public static string _UnitId="@UnitId";
	public static string _LoginId="@LoginId";
	public static string _LoginDate="@LoginDate";
	public static string _IsDeleted="@IsDeleted";
	public static string _UnitConvDtlsId="@UnitConvDtlsId";
	public static string _UnitFactor="@UnitFactor";
	public static string _Qty="@Qty";

    #endregion

    #region [Defination]

    private int m_Action;

    public int Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    private int m_UnitConvId;

    public int UnitConvId
    {
        get { return m_UnitConvId; }
        set { m_UnitConvId = value; }
    }

    private int m_UnitId;

    public int UnitId
    {
        get { return m_UnitId; }
        set { m_UnitId = value; }
    }

    private int m_LoginId;

    public int LoginId
    {
        get { return m_LoginId; }
        set { m_LoginId = value; }
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

    private int m_UnitConvDtlsId;

    public int UnitConvDtlsId
    {
        get { return m_UnitConvDtlsId; }
        set { m_UnitConvDtlsId = value; }
    }

    private string m_UnitFactor;

    public string UnitFactor
    {
        get { return m_UnitFactor; }
        set { m_UnitFactor = value; }
    }

    private decimal m_Qty;

    public decimal Qty
    {
        get { return m_Qty; }
        set { m_Qty = value; }
    }

    #endregion

    #region [Procedure]
        public static string SP_UnitConversion = "SP_UnitConversion";
    #endregion

    public UnitConversion()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
