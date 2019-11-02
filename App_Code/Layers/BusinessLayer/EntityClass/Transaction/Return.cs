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
/// Summary description for EReturn
/// </summary>
namespace MayurInventory.EntityClass
{
    public class EReturn
    {
        #region Column Constant

        //Master
        public const string _Action = "@Action";
        public const string _ReturnId = "@ReturnId";
        public const string _ReturnNo = "@ReturnNo";
        public const string _ReturnDate = "@ReturnDate";
        public const string _PreparedBy = "@PreparedBy";
        public const string _InwardId = "@InwardId";
        public const string _UserId = "@UserId";
        public const string _strCond = "@strCond";
        public const string _LoginDate = "@LoginDate";
        public const string _IsDebitNote = "@IsDebitNote";
        //Details
        public const string _ReturnDtlId = "@ReturnDtlId";
        public const string _ItemId = "@ItemId";
        public const string _InwardQty = "@InwardQty";
        public const string _InwardRate = "@InwardRate";
        public const string _ReturnQty = "@ReturnQty";
        public const string _DamageQty = "@DamageQty";
        public const string _PrevReturnQty = "@PrevReturnQty";
        public const string _Reason = "@Reason";
        public const string _ItemDesc = "@ItemDesc";

        //StockDetails
        public const string _StockDate = "@StockDate";
        public const string _StockLocationID = "@StockLocationID";
        public const string _StockUnitId = "@StockUnitId";

        #endregion

        #region Defination
        private Int32 m_IsDebitNote;
        public Int32 IsDebitNote
        {
            get { return m_IsDebitNote; }
            set { m_IsDebitNote = value; }
        }

        private string m_ItemDesc;
        public string ItemDesc
        {
            get { return m_ItemDesc; }
            set { m_ItemDesc = value; }
        }


        private Decimal m_DamageQty;
        public Decimal DamageQty
        {
            get { return m_DamageQty; }
            set { m_DamageQty = value; }
        }

        private Decimal m_PrevReturnQty;
        public Decimal PrevReturnQty
        {
            get { return m_PrevReturnQty; }
            set { m_PrevReturnQty = value; }
        }


        //Master
        private Int32 m_Action;
        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        private Int32 m_ReturnId;
        public Int32 ReturnId
        {
            get { return m_ReturnId; }
            set { m_ReturnId = value; }
        }

        private String m_ReturnNo;
        public String ReturnNo
        {
            get { return m_ReturnNo; }
            set { m_ReturnNo = value; }
        }

        private DateTime m_ReturnDate;
        public DateTime ReturnDate
        {
            get { return m_ReturnDate; }
            set { m_ReturnDate= value; }
        }

        private Int32 m_PreparedBy;
        public Int32 PreparedBy
        {
            get { return m_PreparedBy; }
            set { m_PreparedBy = value; }
        }

        private Int32 m_InwardId;
        public Int32 InwardId
        {
            get { return m_InwardId; }
            set { m_InwardId = value; }
        }

        private Int32 m_ReturndThrough;
        public Int32 ReturndThrough
        {
            get { return m_ReturndThrough; }
            set { m_ReturndThrough = value; }
        }

        private Int32 m_UserID;
        public Int32 UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        private String m_strCond;
        public String StrCond
        {
            get { return m_strCond; }
            set { m_strCond = value; }
        }

        private DateTime m_LoginDate;
        public DateTime LoginDate
        {
            get { return m_LoginDate; }
            set { m_LoginDate = value; }
        }

        //Details
        private Int32 m_ReturnDtlId;
        public Int32 ReturnDtlId
        {
            get { return m_ReturnDtlId; }
            set { m_ReturnDtlId = value; }
        }

        private Int32 m_ItemId;
        public Int32 ItemId
        {
            get { return m_ItemId; }
            set { m_ItemId = value; }
        }

        private Decimal m_InwardQty;
        public Decimal InwardQty
        {
            get { return m_InwardQty; }
            set { m_InwardQty = value; }
        }


        private Decimal m_InwardRate;
        public Decimal InwardRate
        {
            get { return m_InwardRate; }
            set { m_InwardRate = value; }
        }

        private Decimal m_ReturnQty;
        public Decimal ReturnQty
        {
            get { return m_ReturnQty; }
            set { m_ReturnQty = value; }
        }

        private String m_Reason;
        public String Reason
        {
            get { return m_Reason; }
            set { m_Reason = value; }
        }

        //private bool m_IsDeleted;

        //public bool IsDeleted
        //{
        //    get { return m_IsDeleted; }
        //    set { m_IsDeleted = value; }
        //}

        private DateTime m_StockDate;
        public DateTime StockDate
        {
            get { return m_StockDate; }
            set { m_StockDate = value; }
        }

        private Int32 m_StockLocationID;
        public Int32 StockLocationID
        {
            get { return m_StockLocationID; }
            set { m_StockLocationID = value; }
        }

        private Int32 m_StockUnitId;
        public Int32 StockUnitId
        {
            get { return m_StockUnitId; }
            set { m_StockUnitId = value; }
        }

        #endregion

        #region Strored Proc
        public const string SP_ReturnMaster = "SP_ReturnMaster";
        public const string SP_ReturnMaster1 = "SP_ReturnMaster1";
        public const string SP_ReturnMasterReport = "SP_ReturnMasterReport";

        #endregion


        public EReturn()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
