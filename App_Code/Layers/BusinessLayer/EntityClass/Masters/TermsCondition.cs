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
/// Summary description for TermsCondition
/// </summary>
public class TermsCondition
{
    #region[Variables]
    public static string _Action = "@Action";
    public static string _TermsID = "@TermsID";
    public static string _TermsConditions = "@TermsCondition";
    public static string _Title = "@Title";
    public static string _UserId = "@UserId";
    public static string _LoginDate = "@LoginDate";
    public static string _IsDeleted = "@IsDeleted";
    public static string _StrCondition = "@StrCondition";
    #endregion
    
    #region Defination
    private Int32 m_Action;

    public Int32 Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }
    private Int32 m_TermsID;

    public Int32 TermsID
    {
        get { return m_TermsID; }
        set { m_TermsID = value; }
    }
    private String m_TermsConditions;

    public String TermsConditions
    {
        get { return m_TermsConditions; }
        set { m_TermsConditions = value; }
    }
    private String m_Title;

    public String Title
    {
        get { return m_Title; }
        set { m_Title = value; }
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
    private String m_StrCondition;

    public String StrCondition
    {
        get { return m_StrCondition; }
        set { m_StrCondition = value; }
    }

    #endregion

    #region Stored Proc
    public static string SP_TermCondition = "SP_TermCondition";
    #endregion
    public TermsCondition()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
}
