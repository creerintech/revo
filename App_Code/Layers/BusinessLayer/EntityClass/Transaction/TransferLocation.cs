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
/// Summary description for TransferLocation
/// </summary>
namespace MayurInventory.EntityClass
{
    public class TransferLocation
    {
        #region Column Constant

        //Master
        public const string _Action = "@Action";
        public const string _TransId = "@TransId";
        public const string _TransNo = "@TransNo";
        public const string _Date = "@Date";
        public const string _TransBy = "@TransBy";
        public const string _Notes = "@Notes";
        public const string _UserId = "@UserId";
        public const string _strCond = "@strCond";
        public const string _LoginDate = "@LoginDate";

        //Details
        public const string _TransDtlId = "@TransDtlId";
        public const string _CategoryId = "@CategoryId";
        public const string _ItemId = "@ItemId";
        
        public const string _TransFrom = "@TransFrom";
        public const string _QtyAtSource = "@QtyAtSource";
        public const string _QtyAtDest = "@QtyAtDest";
        public const string _TransTo = "@TransTo";
        public const string _TransQty = "@TransQty";
        public const string _rate = "@Rate";
        public const string _UnitConversionId = "@UnitConversionId";
        public const string _ItemDescID = "@ItemDescID";
        #endregion

        #region Defination

        //Master
        private Int32 m_UnitConversionId;

        public Int32 UnitConversionId
        {
            get { return m_UnitConversionId; }
            set { m_UnitConversionId = value; }
        }

        private Int32 m_Action;

        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }
        private Int32 m_TransId;

        public Int32 TransId
        {
            get { return m_TransId; }
            set { m_TransId = value; }
        }
        private String m_TransNo;

        public String TransNo
        {
            get { return m_TransNo; }
            set { m_TransNo = value; }
        }
        private DateTime m_Date;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }
        private Int32 m_TransBy;

        public Int32 TransBy
        {
            get { return m_TransBy; }
            set { m_TransBy = value; }
        }
        private String m_Notes;

        public String Notes
        {
            get { return m_Notes; }
            set { m_Notes = value; }
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
        private Int32 m_TransDtlId;

        public Int32 TransDtlId
        {
            get { return m_TransDtlId; }
            set { m_TransDtlId = value; }
        }
        private Int32 m_CategoryId;

        public Int32 CategoryId
        {
            get { return m_CategoryId; }
            set { m_CategoryId = value; }
        }
        private Int32 m_ItemId;

        public Int32 ItemId
        {
            get { return m_ItemId; }
            set { m_ItemId = value; }
        }
        private Decimal m_TransQty;

        public Decimal TransQty
        {
            get { return m_TransQty; }
            set { m_TransQty = value; }
        }

       
        private Int32 m_TransFrom;

        public Int32 TransFrom
        {
            get { return m_TransFrom; }
            set { m_TransFrom = value; }
        }

        private Decimal m_QtyAtSource;

        public Decimal QtyAtSource
        {
            get { return m_QtyAtSource; }
            set { m_QtyAtSource = value; }
        }
        private Decimal m_QtyAtDest;

        public Decimal QtyAtDest
        {
            get { return m_QtyAtDest; }
            set { m_QtyAtDest = value; }
        }

        private Int32 m_TransTo;

        public Int32 TransTo
        {
            get { return m_TransTo; }
            set { m_TransTo = value; }
        }


        private Int32 m_ItemDescID;

        public Int32 ItemDescID
        {
            get { return m_ItemDescID; }
            set { m_ItemDescID = value; }
        }
        private decimal m_rate;
        public decimal rate
        {
            get { return m_rate; }
            set { m_rate = value; }
        }

        #endregion

        #region Strored Proc
        //public const string SP_TransferLocation = "SP_TransferLocation";
        public const string SP_LocationTransfer = "SP_LocationTransfer";
        public const string SP_LocationTransferReport = "SP_LocationTransferReport";
        #endregion

        public TransferLocation()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
