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
/// Summary description for SubUnit
/// </summary>
public class SubUnit
{
    #region[Constants]
        public static string _Action = "@Action";
        public static string _SubUnitId = "@SubUnitId";
        public static string _UnitId = "@UnitId";
        public static string _SubUnit1 = "@SubUnit";
        public static string _ConversionFactor = "@ConversionFactor";
        public static string _UserId = "@UserId";
        public static string _LoginDate = "@LoginDate";
        public static string _IsDeleted = "@IsDeleted";
        public static string _StrCondition = "@strCond";
    #endregion

    #region[Defination]
        private Int32 m_Action;
        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        private Int32 m_SubUnitId;
        public Int32 SubUnitId
        {
            get { return m_SubUnitId; }
            set { m_SubUnitId = value; }
        }

        private Int32 m_UnitId;
        public Int32 UnitId
        {
            get { return m_UnitId; }
            set { m_UnitId = value; }
        }

        private string m_SubUnit1;
        public string SubUnit1
        {
            get { return m_SubUnit1; }
            set { m_SubUnit1 = value; }
        }

        private decimal m_ConversionFactor;
        public decimal ConversionFactor
        {
            get { return m_ConversionFactor; }
            set { m_ConversionFactor = value; }
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
        
        private Int32 m_IsDeleted;
        public Int32 IsDeleted
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

    #region[Store Procedure]
     public static string SP_SubUnitMaster= "SP_SubUnitMaster";
    #endregion

    public SubUnit()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
