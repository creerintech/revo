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
/// Summary description for GroupMaster
/// </summary>
/// 
  public class PaymentVoucher
    {
        #region[Constants]
        public const string _Action = "@Action";
        public const string _VoucherId = "@VoucherId";
        public const string _LocationId = "@LocationId";
        public const string _VoucherTypeID = "@VoucherTypeID";
        public const string _VoucherType = "@VoucherType";
        public const string _VoucherDate = "@VoucherDate";
        public const string _VoucherDebit = "@VoucherDebit";
        public const string _VoucherCredit = "@VoucherCredit";
        public const string _VoucherAmount = "@VoucherAmt";
        public const string _VoucherNarration = "@VoucherNarration";
        public const string _InvoiceCode = "@InvoiceCode";
        public const string _InvoiceType = "@InvoiceType";
        public const string _Crnote = "@Crnote";
        public const string _ReconDate = "@ReconDate";
        public const string _LoginID = "@LoginID";
        public const string _LoginDate = "@LoginDate";
        public const string _RepCondition = "@RepCondition";
        public const string _OutStanding = "@OutStanding";
        public const string _ReceivedAmt = "@ReceivedAmt";
        public const string _IsReference = "@IsReference";
       
        #endregion

        #region[Definations]

        private Int32 m_LocationId;
        public Int32 LocationId
        {
            get { return m_LocationId; }
            set { m_LocationId = value; }
        }
        private Int32 m_Action;
        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }
        private Int32 m_VoucherId;
        public Int32 VoucherId
        {
            get { return m_VoucherId; }
            set { m_VoucherId = value; }
        }
        private Char m_VoucherType;
        public Char VoucherType
        {
            get { return m_VoucherType; }
            set { m_VoucherType = value; }
        }
        private Int32 m_VoucherTypeId;
        public Int32 VoucherTypeId
        {
            get { return m_VoucherTypeId; }
            set { m_VoucherTypeId = value; }
        }
        private DateTime m_VoucherDate;
        public DateTime VoucherDate
        {
            get { return m_VoucherDate; }
            set { m_VoucherDate = value; }
        }
        private Int32 m_VoucherDebit;
        public Int32 VoucherDebit
        {
            get { return m_VoucherDebit; }
            set { m_VoucherDebit = value; }
        }
        private Int32 m_VoucherCredit;
        public Int32 VoucherCredit
        {
            get { return m_VoucherCredit; }
            set { m_VoucherCredit = value; }
        }
        private decimal m_VoucherAmount;
        public decimal VoucherAmount
        {
            get { return m_VoucherAmount; }
            set { m_VoucherAmount = value; }
        }
        private string m_VoucherNarration;
        public string VoucherNarration
        {
            get { return m_VoucherNarration; }
            set { m_VoucherNarration = value; }
        }
        private Int32 m_InvoiceCode;
        public Int32 InvoiceCode
        {
            get { return m_InvoiceCode; }
            set { m_InvoiceCode = value; }
        }
        private string m_InvoiceType;
        public string InvoiceType
        {
            get { return m_InvoiceType; }
            set { m_InvoiceType = value; }
        }
        private Int32 m_Crnote;
        public Int32 Crnote
        {
            get { return m_Crnote; }
            set { m_Crnote = value; }
        }

        private decimal m_ReceivedAmt;

        public decimal ReceivedAmt
        {
            get { return m_ReceivedAmt; }
            set { m_ReceivedAmt = value; }
        }
        private decimal m_OutStanding;

        public decimal OutStanding
        {
            get { return m_OutStanding; }
            set { m_OutStanding = value; }
        }

        private bool m_IsReference;

        public bool IsReference
        {
            get { return m_IsReference; }
            set { m_IsReference = value; }
        }

        private Int32 m_LoginID;
        public Int32 LoginID
        {
            get { return m_LoginID; }
            set { m_LoginID = value; }
        }
        private DateTime m_LoginDate;
        public DateTime LoginDate
        {
            get { return m_LoginDate; }
            set { m_LoginDate = value; }
        }
        private string m_RepCondition;
        public string RepCondition
        {
            get { return m_RepCondition; }
            set { m_RepCondition = value; }
        }

        #endregion

        #region[Store Procedures]
        public const string PRO_PaymentVoucher = "PRO_PaymentVoucher";
        #endregion

        public PaymentVoucher()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
