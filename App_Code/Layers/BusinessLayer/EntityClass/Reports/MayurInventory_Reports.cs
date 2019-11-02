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
/// Summary description for MayurInventory_Reports
/// </summary>
public class MayurInventory_Reports
{
    #region Constants
    public static string _Action = "@Action";    

    public static string _UserId = "@UserId";
    public static string _LoginDate = "@LoginDate";    
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
    
    private string m_strCond;
    public string StrCond
    {
        get { return m_strCond; }
        set { m_strCond = value; }
    }
    #endregion

    #region Procedure
    public static string SP_MI_REPORTS_PART_I = "SP_MI_REPORTS_PART_I";    
    #endregion

	public MayurInventory_Reports()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
