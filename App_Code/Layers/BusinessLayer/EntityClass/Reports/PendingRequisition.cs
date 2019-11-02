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
/// Summary description for PendingRequisition
/// </summary>
public class PendingRequisition
{
    #region Constants
    public static string _Action = "@Action";
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

   
    private string m_strCond;
    public string StrCond
    {
        get { return m_strCond; }
        set { m_strCond = value; }
    }
    #endregion

    #region Procedure
    public static string SP_PendingReport = "SP_PendingReport";
    #endregion
	public PendingRequisition()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
