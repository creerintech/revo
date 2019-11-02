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
/// Summary description for StockLocation
/// </summary>
public class CostCentre
{
    #region Constants
        public static string _Action="@Action";
	    public static string _StockLocationID="@StockLocationID";
	    public static string _Location="@Location";
        public static string _SiteId = "@SiteId";
        public static string _TowerId = "@TowerId";
        public static string _CompanyId = "@CompanyId";

	    public static string _UserId="@UserId";
	    public static string _LoginDate="@LoginDate";
	    public static string _IsDeleted="@IsDeleted";
        public static string _StrCondition="@strCond";
        public static string _IsCost = "@IsCost";
    #endregion

    #region Definition
        private Int32 m_Action;
        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        private Int32 m_StockLocationID;
        public Int32 StockLocationID
        {
            get { return m_StockLocationID; }
            set { m_StockLocationID = value; }
        }

        private string m_Location;
        public string Location
        {
            get { return m_Location; }
            set { m_Location = value; }
        }

        private Int32 m_SiteId;
        public Int32 SiteId
        {
            get { return m_SiteId; }
            set { m_SiteId = value; }
        }

        private Int32 m_TowerId;
        public Int32 TowerId
        {
            get { return m_TowerId; }
            set { m_TowerId = value; }
        }

        private Int32 m_CompanyId;
        public Int32 CompanyId
        {
            get { return m_CompanyId; }
            set { m_CompanyId = value; }
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
       
        private bool m_IsCental;
        public bool IsCental
        {
            get { return m_IsCental; }
            set { m_IsCental = value; }
        }

        private string m_StrCondition;
        public string StrCondition
        {
            get { return m_StrCondition; }
            set { m_StrCondition = value; }
        }
   

    #endregion

    #region Procedure
        public static string SP_CostCentre = "SP_CostCentre";
    #endregion

        public CostCentre()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
