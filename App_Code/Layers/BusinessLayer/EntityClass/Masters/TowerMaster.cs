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
/// Summary description for TowerMaster
/// </summary>
public class TowerMaster
{
    #region Constants
        public static string _Action = "@Action";
        public static string _TowerID = "@TowerID";
        public static string _TowerName = "@TowerName";
        public static string _UserId = "@UserId";
        public static string _LoginDate = "@LoginDate";
        public static string _IsDeleted = "@IsDeleted";
        public static string _StrCondition = "@strCond";
       
    #endregion

    #region Definition
        private Int32 m_Action;
        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        private Int32 m_TowerID;
        public Int32 TowerID
        {
            get { return m_TowerID; }
            set { m_TowerID = value; }
        }

        private string m_TowerName;
        public string TowerName
        {
            get { return m_TowerName; }
            set { m_TowerName = value; }
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

        private string m_StrCondition;
        public string StrCondition
        {
            get { return m_StrCondition; }
            set { m_StrCondition = value; }
        }

    #endregion

    #region Procedure
     public static string SP_TowerMaster = "SP_TowerMaster";
    #endregion

	public TowerMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
