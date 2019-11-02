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
/// Summary description for ErrorLog
/// </summary>
public class ErrorLog
{
    # region [Constants]

    public static string _Action = "@Action"; 
    public static string _FunctionName = "@FunctionName";
    public static string _Message = "@Message";
    public static string _Source = "@Source";
    public static string _StackTrace = "@StackTrace";
    public static string _GeneratedDate = "@GenerataedDate";

    #endregion

    # region [Definations]

    private string m_FunctionName;

    public string FunctionName
    {
        get { return m_FunctionName; }
        set { m_FunctionName = value; }
    }

    private string m_Messege;
    public string Messege
    {
        get { return m_Messege; }
        set { m_Messege = value; }
    }

    private string m_Source;

    public string Source
    {
        get { return m_Source; }
        set { m_Source = value; }
    }

    private string m_StackTrace;

    public string StackTrace
    {
        get { return m_StackTrace; }
        set { m_StackTrace = value; }
    }

    private DateTime m_GeneratedDate;

    public DateTime GeneratedDate
    {
        get { return m_GeneratedDate; }
        set { m_GeneratedDate = value; }

    }

    #endregion

    # region [Stored Procedure]

    public static string SP_ErrorLogger = "SP_ErrorLogger";

    #endregion

	public ErrorLog()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
