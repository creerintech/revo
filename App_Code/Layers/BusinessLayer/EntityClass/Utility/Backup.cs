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
/// Summary description for Backup
/// </summary>
public class Backup
{
    #region [Constant]
    public static string _Action = "@Action";
    public static string _DateCondition = "@DateCondition";
    public static string _DestDir = "@DestDir";
    
    #endregion

    #region [Defination]------------------------------
    private Int32 m_Action;
    public Int32 Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    private string m_DateCondition;
    public string DateCondition
    {
        get { return m_DateCondition; }
        set { m_DateCondition = value; }
    }

    private string m_DestDir;
    public string DestDir
    {
        get { return m_DestDir; }
        set { m_DestDir = value; }
    }
    #endregion

    #region [Stored-Procedure]-----------------------
    public static string SP_Backup = "SP_Backup";
    #endregion

    public Backup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
