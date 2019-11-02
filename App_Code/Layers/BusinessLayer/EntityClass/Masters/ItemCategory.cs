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
/// Summary description for ItemCategory
/// </summary>
public class ItemCategory
{
    #region Constants
        public static string _Action = "@Action";
        public static string _CategoryId="@CategoryId";
	    public static string _CategoryName="@CategoryName";
	    public static string _UserId="@UserId";
	    public static string _Prefix = "@Prefix";
	    public static string _LoginDate="@LoginDate";
	    public static string _IsDeleted="@IsDeleted";
        public static string _strCond = "@strCond";
    #endregion

    #region Definition
        private Int32 m_Action;

        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        private Int32 m_CategoryId;

        public Int32 CategoryId
        {
            get { return m_CategoryId; }
            set { m_CategoryId = value; }
        }

        private string m_Prefix;

        public string Prefix
    {
            get { return m_Prefix; }
            set { m_Prefix = value; }
        }


    private string m_CategoryName;

    public string CategoryName
    {
        get { return m_CategoryName; }
        set { m_CategoryName = value; }
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
        public static string SP_ItemCategory = "SP_ItemCategory";
    #endregion

    public ItemCategory()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
