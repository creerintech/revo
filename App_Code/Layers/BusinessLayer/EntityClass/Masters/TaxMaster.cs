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
public class TaxMaster
{
    #region Constants
        public static string _Action = "@Action";
        public static string _TaxId="@TaxId";
	    public static string _TaxName="@TaxName";
        public static string _TaxPer="@TaxPer";
        public static string _TaxTypeID = "@TaxTypeID";
        public static string _EffectiveFrom = "@EffectiveFrom";
	    public static string _UserId="@UserId";
	    public static string _LoginDate="@LoginDate";
	    public static string _IsDeleted="@IsDeleted";
        public static string _strCond = "@strCond";
        public static string _TaxDtlsId = "@TaxDtlsId";
        public const string _ApplicableDate = "@ApplicableDate";
        public const string _GST = "@GST";
        
    #endregion

    #region Definition
        private DateTime m_EffectiveFrom;

        public DateTime EffectiveFrom
        {
            get { return m_EffectiveFrom; }
            set { m_EffectiveFrom = value; }
        }

        private Int32 m_TaxTypeID;

        public Int32 TaxTypeID
        {
            get { return m_TaxTypeID; }
            set { m_TaxTypeID = value; }
        }

        private Int32 m_Action;

        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        private Int32 m_TaxId;

        public Int32 TaxId
        {
            get { return m_TaxId; }
            set { m_TaxId = value; }
        }

        private string m_TaxName;

        public string TaxName
        {
            get { return m_TaxName; }
            set { m_TaxName = value; }
        }

        private Decimal m_TaxPer;

        public Decimal TaxPer
        {
            get { return m_TaxPer; }
            set { m_TaxPer = value; }
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

        private decimal m_GST;

        public decimal GST
        {
            get { return m_GST; }
            set { m_GST = value; }
        }

        public Int32 TaxDtlsId
        {
            get;
            set;
        }
    
        public DateTime ApplicableDate { get; set; }
    #endregion

    #region Procedure
        public static string SP_ItemCategory = "SP_TaxMaster";
    #endregion

    public TaxMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
