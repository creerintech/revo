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
/// Summary description for IssueRegister
/// </summary>
public class DailyInwardOutwardRegister
{
    #region[Constants]
    //Constant Of Issue Register
    public static string _Action ="@Action";
    public static string _DailyOutRegisterId = "@DailyOutRegisterId";
    public static string _InwardNo = "@InwardNo";
    public static string _InwardDate = "@InwardDate";
    public static string _EmployeeId = "@EmployeeId";
    public static string _UserId="@UserId";
    public static string _LoginDate="@LoginDate";
    public static string _IsDeleted="@IsDeleted";
    public static string _StrCondition="@StrCondition";

   //--* Constants of Issue Register Details *--  
    public static string _DailyOutRegisterdetailsId = "@DailyOutRegisterdetailsId";
    public static string _ItemId="@ItemId";
    public static string _Quantity = "@Quantity";
    public static string _DetailsId = "@DetailsId";
    #endregion

    #region[Defination]
    // Issue Register
     //mAction
    private Int32 m_DetailsId;

    public Int32 DetailsId
    {
        get { return m_DetailsId; }
        set { m_DetailsId = value; }
    }
    private Decimal m_Quantity;

    public Decimal Quantity
    {
        get { return m_Quantity; }
        set { m_Quantity = value; }
    }
    private Int32 m_ItemId;

    public Int32 ItemId
    {
        get { return m_ItemId; }
        set { m_ItemId = value; }
    }
    private Int32 m_DailyOutRegisterdetailsId;

    public Int32 DailyOutRegisterdetailsId
    {
        get { return m_DailyOutRegisterdetailsId; }
        set { m_DailyOutRegisterdetailsId = value; }
    }
    private String m_StrCondition;

    public String StrCondition
    {
        get { return m_StrCondition; }
        set { m_StrCondition = value; }
    }
    private Int32 m__IsDeleted;

    public Int32 _IsDeleted1
    {
        get { return m__IsDeleted; }
        set { m__IsDeleted = value; }
    }
    private DateTime m_LoginDate;

    public DateTime LoginDate
    {
        get { return m_LoginDate; }
        set { m_LoginDate = value; }
    }
    private Int32 m_UserId;

    public Int32 UserId
    {
        get { return m_UserId; }
        set { m_UserId = value; }
    }
    
    private Int32 m_EmployeeId;

    public Int32 EmployeeId
    {
        get { return m_EmployeeId; }
        set { m_EmployeeId = value; }
    }

    private DateTime m_InwardDate;

    public DateTime InwardDate
    {
        get { return m_InwardDate; }
        set { m_InwardDate = value; }
    }

    private string m_Action;
    public string Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    private Int32 m_DailyOutRegisterId;

    public Int32 DailyOutRegisterId
    {
        get { return m_DailyOutRegisterId; }
        set { m_DailyOutRegisterId = value; }
    }
    private String m_InwardNo;

    public String InwardNo
    {
        get { return m_InwardNo; }
        set { m_InwardNo = value; }
    }
    /// <summary>
  
    /// </summary>
    #endregion

    #region[Procedure]
    public static string SP_DailyInwardOutwardRegister = "SP_DailyInwardOutwardRegister";
    #endregion
    public DailyInwardOutwardRegister()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
