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
/// Summary description for PriorityMaster
/// </summary>
public class PriorityMaster
{
    #region Constants
    public static string _Action = "@Action";
    public static string _PriorityID = "@PriorityID";
    public static string _Priority = "@Priority";
    public static string _LoginId = "@LoginId";
    public static string _LoginDate = "@LoginDate";
    public static string _StrCondition = "@strCond";
    #endregion

    #region Definition
    private Int32 m_Action;

    public Int32 Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    private Int32 m_PriorityID;

    public Int32 PriorityID
    {
        get { return m_PriorityID; }
        set { m_PriorityID = value; }
    }

    private string m_Priority;

    public string Priority
    {
        get { return m_Priority; }
        set { m_Priority = value; }
    }

    private Int32 m_LoginId;

    public Int32 LoginId
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

    private string m_StrCondition;
    public string StrCondition
    {
        get { return m_StrCondition; }
        set { m_StrCondition = value; }
    }


    #endregion

    #region Procedure
    public static string SP_Priority = "SP_PriorityMaster";
    #endregion

	public PriorityMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
