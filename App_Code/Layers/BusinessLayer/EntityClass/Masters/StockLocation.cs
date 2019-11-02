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
public class StockLocation
{
    #region Constants
        public static string _Action="@Action";
	    public static string _StockLocationID="@StockLocationID";
	    public static string _Location="@Location";
        public static string _SiteId="@SiteId";
        public static string _TowerId="@TowerId";
        public static string _CompanyId="@CompanyId";
        public static string _SiteAddr = "@SiteAddr";

	    public static string _UserId="@UserId";
	    public static string _LoginDate="@LoginDate";
	    public static string _IsDeleted="@IsDeleted";
        public static string _StrCondition="@strCond";
        public static string _IsCental = "@IsCentral";

        //----ContactPerson_details----
        public static string _CPersonId="@CPersonId";
        public static string _EmployeeId = "@EmployeeId";
        public static string _CpersonName="@CpersonName";
        public static string _MailId="@MailId";
        public static string _PersonAddress="@PersonAddress";
        public static string _ContactNo ="@ContactNo";
        public static string _abbreviation = "@abbreviation";
    #endregion

    #region Definition

        private string m_abbreviation;
        public string abbreviation
        {
            get { return m_abbreviation; }
            set { m_abbreviation = value; }
        }

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

        private string m_SiteAddr;
        public string SiteAddr
        {
            get { return m_SiteAddr; }
            set { m_SiteAddr = value; }
        }

        //----ContactPerson_details----
        private Int32 m_CPersonId;
        public Int32 CPersonId
        {
            get { return m_CPersonId; }
            set { m_CPersonId = value; }
        }

        private Int32 m_EmployeeId;
        public Int32 EmployeeId
        {
            get { return m_EmployeeId; }
            set { m_EmployeeId = value; }
        }

        private string m_CpersonName;
        public string CpersonName
        {
            get { return m_CpersonName; }
            set { m_CpersonName = value; }
        }

        private string m_MailId;
        public string MailId
        {
            get { return m_MailId; }
            set { m_MailId = value; }
        }

        private string m_PersonAddress;
        public string PersonAddress
        {
            get { return m_PersonAddress; }
            set { m_PersonAddress = value; }
        }

        private string m_ContactNo;
        public string ContactNo
        {
            get { return m_ContactNo; }
            set { m_ContactNo = value; }
        }

    #endregion

    #region Procedure
        public static string SP_StockLocation = "SP_StockLocation";
    #endregion

    public StockLocation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
