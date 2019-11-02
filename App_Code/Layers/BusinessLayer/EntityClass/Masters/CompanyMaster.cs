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
/// Summary description for CompanyMaster
/// </summary>
public class CompanyMaster
{
    #region[Constant]
     //--**CompanyMaster**--
     
    public static string _Action="@Action";
    public static string _CompanyId="@CompanyId";
    public static string _CompanyName="@CompanyName";
    public static string _CAddress = "@CAddress";
    public static string _CLogo="@CLogo";
    public static string _PhoneNo="@PhoneNo";
    public static string _EmailId="@EmailId";
    public static string _Website="@Website";
    public static string _FaxNo="@FaxNo";
    public static string _TinNo="@TinNo";
    public static string _VatNo="@VatNo";
    public static string _ServiceTaxNo="@ServiceTaxNo";
    public static string _DigitalSignature="@DigitalSignature";
    public static string _DigitalSignature1 = "@DigitalSignature1";
    public static string _DigitalSignature2 = "@DigitalSignature2";
    public static string _Note="@Note";
    public static string _BankId="@BankId";
    public static string _strCond="@strCond";
    public static string _UserId="@UserId";
    public static string _LoginDate="@LoginDate";
   

    //--**BankDetails**--
    public static string _BankName="@BankName";
    public static string _AccountNo="@AccountNo";
    public static string _NoteB = "@NoteB";
    public static string _abbreviation = "@abbreviation";
    #endregion

    #region[Defination]
    private string m_abbreviation;
    public string abbreviation
    {
        get { return m_abbreviation; }
        set { m_abbreviation = value; }
    }

    private string m_NoteB;
    public string NoteB
    {
        get { return m_NoteB; }
        set { m_NoteB = value; }
    }
      
        private Int32 m_Action;
        public Int32 Action
        {
         get { return m_Action; }
         set { m_Action = value; }
        }           
        private Int32 m_CompanyId;
        public Int32 CompanyId
        {
         get { return m_CompanyId; }
         set { m_CompanyId = value; }
        }          
        private string m_CompanyName;
        public string CompanyName
        {
         get { return m_CompanyName; }
         set { m_CompanyName = value; }
        }  
        private string m_CAddress;
        public string CAddress
        {
         get { return m_CAddress; }
         set { m_CAddress = value; }
        }
        private string m_CLogo;
        public string CLogo
        {
         get { return m_CLogo; }
         set { m_CLogo = value; }
        }
        private string m_PhoneNo;
        public string PhoneNo
        {
         get { return m_PhoneNo; }
         set { m_PhoneNo = value; }
        }
        private string m_EmailId;
        public string EmailId
        {
          get { return m_EmailId; }
          set { m_EmailId = value; }
        }
        private string m_Website;
        public string Website
        {
          get { return m_Website; }
          set { m_Website = value; }
        }
        private string m_FaxNo;
        public string FaxNo
        {
          get { return m_FaxNo; }
          set { m_FaxNo = value; }
        }
        private string m_TinNo;
        public string TinNo
        {
          get { return m_TinNo; }
          set { m_TinNo = value; }
        }
        private string m_VatNo;
        public string VatNo
        {
          get { return m_VatNo; }
          set { m_VatNo = value; }
        }
        private string m_ServiceTaxNo;
        public string ServiceTaxNo
        {
          get { return m_ServiceTaxNo; }
          set { m_ServiceTaxNo = value; }
        }
        private string  m_DigitalSignature;
        public string DigitalSignature
        {
          get { return m_DigitalSignature; }
          set { m_DigitalSignature = value; }
        }

        private string m_DigitalSignature1;
        public string DigitalSignature1
        {
            get { return m_DigitalSignature1; }
            set { m_DigitalSignature1 = value; }
        }

        private string m_DigitalSignature2;
        public string DigitalSignature2
        {
            get { return m_DigitalSignature2; }
            set { m_DigitalSignature2 = value; }
        }

        private string m_Note;
        public string Note
        {
          get { return m_Note; }
          set { m_Note = value; }
        }
        private Int32 m_BankId;
        public Int32 BankId
        {
          get { return m_BankId; }
          set { m_BankId = value; }
        }
        private string m_strCond;
        public string StrCond
        {
          get { return m_strCond; }
          set { m_strCond = value; }
        }
        private Int32   m_UserId;
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
        private DateTime m_DeletedDate;
        public DateTime DeletedDate
        {
          get { return m_DeletedDate; }
          set { m_DeletedDate = value; }
        }
        private string  m_BankName;
        public string BankName
        {
          get { return m_BankName; }
          set { m_BankName = value; }
        }
        private string m_AccountNo;
        public string AccountNo
        {
          get { return m_AccountNo; }
          set { m_AccountNo = value; }
        }

    #endregion

    #region[StoreProcedure]
     public static string SP_CompanyMaster="SP_CompanyMaster";
    #endregion

    public CompanyMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
