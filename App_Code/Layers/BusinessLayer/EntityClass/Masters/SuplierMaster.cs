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
/// Summary description for SuplierMaster
/// </summary>
public class SuplierMaster
{
    #region Variables
        public static string _Action="@Action";
	    public static string _SuplierId="@SuplierId";
	    public static string _SuplierCode="@SuplierCode";
	    public static string _SuplierName="@SuplierName";
	    public static string _Address="@Address";
	    public static string _TelNo="@TelNo";
	    public static string _MobileNo="@MobileNo";
	    public static string _Email="@Email";
	    public static string _Note="@Note";
	    public static string _UserId="@UserId";
	    public static string _LoginDate="@LoginDate";
	    public static string _IsDeleted="@IsDeleted";
        public static string _strCond = "@strCond";
        public static string _StateID = "@StateID";
        //------Newly Added Field------
         public static string _WebSite="@WebSite";
	     public static string _PersonName="@PersonName";
	     public static string _PDesignation="@PDesignation";
	     public static string _PMobileNo="@PMobileNo";
	     public static string _PEmailId="@PEmailId";
	     public static string _PWebsite="@PWebsite";
	     public static string _STaxRegNo="@STaxRegNo";
	     public static string _STaxJurisdiction="@STaxJurisdiction";
	     public static string _VATNo="@VATNo";
	     public static string _TINNo="@TINNo";
	     public static string _CentralSaleTaxRagNo="@CentralSaleTaxRagNo";
	     public static string _ExciseRange="@ExciseRange";
	     public static string _ExciseDivision="@ExciseDivision";
	     public static string _ExciseCircle="@ExciseCircle";
	     public static string _ExciseZone="@ExciseZone";
	     public static string _ExciseCollectorate="@ExciseCollectorate";
	     public static string _ExciseECCNO="@ExciseECCNO";
	     public static string _TIN_BINNo="@TIN_BINNo";
	     public static string _PANNo="@PANNo";
         public static string _TDSCertificate ="@TDSCertificate";
         public static string _ImgTaxRegNoPath="@ImgTaxRegNoPath";
	     public static string _ImgPanNoPath ="@ImgPanNoPath";

         public static string _TermsID = "@TermsID";
         public static string _TermsTitle = "@STitle";
         public static string _TermsDetls = "@SDescription";
    
    #endregion

    #region Definition

         private String m_TermsTitle;
         public String TermsTitle
         {
             get { return m_TermsTitle; }
             set { m_TermsTitle = value; }
         }

         private String m_TermsDetls;
         public String TermsDetls
         {
             get { return m_TermsDetls; }
             set { m_TermsDetls = value; }
         }
         
    private Int32 m_TermsID;
         public Int32 TermsID
         {
             get { return m_TermsID; }
             set { m_TermsID = value; }
         }

        private Int32 m_Action;
        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }



        private Int32 m_StateID;
        public Int32 StateID
        {
            get { return m_StateID; }
            set { m_StateID = value; }
        }

        private Int32 m_SuplierId;
        public Int32 SuplierId
        {
            get { return m_SuplierId; }
            set { m_SuplierId = value; }
        }
        private string m_SuplierCode;
        public string SuplierCode
        {
            get { return m_SuplierCode; }
            set { m_SuplierCode = value; }
        }
        private string m_SuplierName;
        public string SuplierName
        {
            get { return m_SuplierName; }
            set { m_SuplierName = value; }
        }
        private string m_Address;
        public string Address
        {
            get { return m_Address; }
            set { m_Address = value; }
        }
        private string m_TelNo;
        public string TelNo
        {
            get { return m_TelNo; }
            set { m_TelNo = value; }
        }
        private string m_MobileNo;
        public string MobileNo
        {
            get { return m_MobileNo; }
            set { m_MobileNo = value; }
        }
        private string m_Email;
        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }
        private string m_Note;
        public string Note
        {
            get { return m_Note; }
            set { m_Note = value; }
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
        public string strCond
        {
            get { return m_strCond; }
            set { m_strCond = value; }
        }

        //-------Newly Added Fields--------
        private string m_WebSite;
        public string WebSite
        {
            get { return m_WebSite; }
            set { m_WebSite = value; }
        }
        private string m_PersonName;
        public string PersonName
        {
            get { return m_PersonName; }
            set { m_PersonName = value; }
        }
        private string m_PDesignation;
        public string PDesignation
        {
            get { return m_PDesignation; }
            set { m_PDesignation = value; }
        }
        private string m_PMobileNo;
        public string PMobileNo
        {
            get { return m_PMobileNo; }
            set { m_PMobileNo = value; }
        }
        private string m_PEmailId;
        public string PEmailId
        {
            get { return m_PEmailId; }
            set { m_PEmailId = value; }
        }
        private string m_PWebsite;
        public string PWebsite
        {
            get { return m_PWebsite; }
            set { m_PWebsite = value; }
        }
        private string m_STaxRegNo;
        public string STaxRegNo
        {
            get { return m_STaxRegNo; }
            set { m_STaxRegNo = value; }
        }
        private string m_STaxJurisdiction;
        public string STaxJurisdiction
        {
            get { return m_STaxJurisdiction; }
            set { m_STaxJurisdiction = value; }
        }
        private string m_VATNo;
        public string VATNo
        {
            get { return m_VATNo; }
            set { m_VATNo = value; }
        }
        private string m_TINNo;
        public string TINNo
        {
            get { return m_TINNo; }
            set { m_TINNo = value; }
        }
        private string m_CentralSaleTaxRagNo;
        public string CentralSaleTaxRagNo
        {
            get { return m_CentralSaleTaxRagNo; }
            set { m_CentralSaleTaxRagNo = value; }
        }
        private string m_ExciseRange;
        public string ExciseRange
        {
            get { return m_ExciseRange; }
            set { m_ExciseRange = value; }
        }
        private string m_ExciseDivision;
        public string ExciseDivision
        {
            get { return m_ExciseDivision; }
            set { m_ExciseDivision = value; }
        }
        private string m_ExciseCircle;
        public string ExciseCircle
        {
            get { return m_ExciseCircle; }
            set { m_ExciseCircle = value; }
        }
        private string m_ExciseZone;
        public string ExciseZone
        {
            get { return m_ExciseZone; }
            set { m_ExciseZone = value; }
        }
        private string m_ExciseCollectorate;
        public string ExciseCollectorate
        {
            get { return m_ExciseCollectorate; }
            set { m_ExciseCollectorate = value; }
        }
        private string m_ExciseECCNO;
        public string ExciseECCNO
        {
            get { return m_ExciseECCNO; }
            set { m_ExciseECCNO = value; }
        }
        private string m_TIN_BINNo;
        public string TIN_BINNo
        {
            get { return m_TIN_BINNo; }
            set { m_TIN_BINNo = value; }
        }
        private string m_PANNo;
        public string PANNo
        {
            get { return m_PANNo; }
            set { m_PANNo = value; }
        }
        private string m_TDSCertificate;
        public string TDSCertificate
        {
            get { return m_TDSCertificate; }
            set { m_TDSCertificate = value; }
        }
        private string m_ImgTaxRegNoPath;
        public string ImgTaxRegNoPath
        {
            get { return m_ImgTaxRegNoPath; }
            set { m_ImgTaxRegNoPath = value; }
        }
        private string m_ImgPanNoPath;
        public string ImgPanNoPath
        {
            get { return m_ImgPanNoPath; }
            set { m_ImgPanNoPath = value; }
        }

        
    #endregion

    #region Procedure
        public static string SP_SuplierMaster = "SP_SuplierMaster";
    #endregion

    public SuplierMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
