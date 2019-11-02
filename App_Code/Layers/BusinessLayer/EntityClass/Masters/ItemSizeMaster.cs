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
public class ItemSizeMaster
{
    #region Constants
        public static string _Action = "@Action";
        public static string _SizeId="@SizeId";
	    public static string _SizeName="@SizeName";
	    public static string _UserId="@UserId";
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

        private Int32 m_SizeId;

        public Int32 SizeId
        {
            get { return m_SizeId; }
            set { m_SizeId = value; }
        }

        private string m_SizeName;

        public string SizeName
        {
            get { return m_SizeName; }
            set { m_SizeName = value; }
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
        public static string SP_ItemSizeMaster = "SP_ItemSizeMaster";
    #endregion

        public ItemSizeMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
