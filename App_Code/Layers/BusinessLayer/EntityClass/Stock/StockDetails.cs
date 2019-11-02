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
/// Summary description for StockDetails
/// </summary>
public class StockDetails
{
    #region Constants
    public static string _Action = "@Action";
    public static string _StockID = "@StockID";
    public static string _StockTypeID = "@StockTypeID";
    public static string _StockDate = "@StockDate";
    public static string _ProductID = "@ProductID";
    public static string _StockLocationID = "@StockLocationID";
    public static string _StockQty = "@StockQty";
    public static string _ProductMRP = "@ProductMRP";
    public static string _StockUnitID = "@StockUnitID";
    public static string _TransID = "@TransID";

    public static string _UserId = "@UserId";
    public static string _LoginDate = "@LoginDate";
    public static string _IsDeleted = "@IsDeleted";
    public static string _strCond = "@strCond";
    #endregion

    #region Definition

    //Action
    private Int32 m_Action;
    public Int32 Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    //StockID
    private Int32 m_StockID;
    public Int32 StockID
    {
        get { return m_StockID; }
        set { m_StockID = value; }
    }
    //StockTypeID
    private Int32 m_StockTypeID;
    public Int32 StockTypeID
    {
        get { return m_StockID; }
        set { m_StockID = value; }
    }
    //StockDate
    private DateTime m_StockDate;
    public DateTime StockDate
    {
        get { return m_StockDate; }
        set { m_StockDate = value; }
    }

    //ProductID
    private Int32 m_ProductID;
    public Int32 ProductID
    {
        get { return m_ProductID; }
        set { m_ProductID = value; }
    }

    //StockLocationID
    private Int32 m_StockLocationID;
    public Int32 StockLocationID
    {
        get { return m_StockLocationID; }
        set { m_StockLocationID = value; }
    }
    
    //StockQty
    private decimal m_StockQty;
    public decimal StockQty
    {
        get { return m_StockQty; }
        set { m_StockQty = value; }
    }

    //ProductMRP
    private decimal m_ProductMRP;
    public decimal ProductMRP
    {
        get { return m_ProductMRP; }
        set { m_ProductMRP = value; }
    }

    //StockUnitID
    private Int32 m_StockUnitID;
    public Int32 StockUnitID
    {
        get { return m_StockUnitID; }
        set { m_StockUnitID = value; }
    }

    //TransID
    private Int32 m_TransID;
    public Int32 TransID
    {
        get { return m_TransID; }
        set { m_TransID = value; }
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
    #endregion

    #region Procedure
    public static string SP_Stock_Details = "SP_Stock_Details";
    #endregion


	public StockDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
