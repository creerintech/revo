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
using MayurInventory.DALSQLHelper;
using MayurInventory.DB;
using MayurInventory.Utility;
/// <summary>
/// Summary description for RequisitionCancellation
/// </summary>
namespace MayurInventory.EntityClass
{
    public class RequisitionCancellation
    {
        #region[Constant]
        //RequisitionCancellation
        public static string _Action ="@Action";
        public static string _RequisitionCancelId="@RequisitionCancelId";
        public static string _RequisitionCancelNo="@RequisitionCancelNo ";
        public static string _RequisitionCafeId="@RequisitionCafeId";
        public static string _Site = "@Site";

        public static string _IsCancel = "@IsCancel";
        public static string _CancelledBy="@CancelledBy";
        public static string _CancelledDate="@CancelledDate ";
        public static string _Reason="@Reason";
        public static string _CancelType ="@CancelType"; 
        public static string _UserId="@UserId";
        public static string _strCond="@strCond";
        public static string _LoginDate="@LoginDate";
        public static string _Temp="@Temp";
        public static string _IsDeleted="@IsDeleted";
   
        //RequisitionCafeDtls
        public static string _RequisitionCancelDtlsId = "@RequisitionCancelDtlsId";
        public static string _ItemId="@ItemId";
        public static string _OrdQty ="@OrdQty";
        public static string _StrCondition="@StrCondition";
        public static string _GrdDateCondition="@GrdDateCondition";

        #endregion

        #region[Stored Procedure]
          public static string SP_RequisitionCancellation = "SP_RequisitionCancellation";
          public static string SP_RequisitionCancellation2 = "SP_RequisitionCancellation2";
        #endregion

        #region[Defination]
        private Int32 m_Action;
        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        private Int32 m_RequisitionCancelId;
        public Int32 RequisitionCancelId
        {
            get { return m_RequisitionCancelId; }
            set { m_RequisitionCancelId = value; }
        }

        private string m_RequisitionCancelNo;
        public string RequisitionCancelNo
        {
            get { return m_RequisitionCancelNo; }
            set { m_RequisitionCancelNo = value; }
        }

        private Int32 m_RequisitionCafeId;
        public Int32 RequisitionCafeId
        {
            get { return m_RequisitionCafeId; }
            set { m_RequisitionCafeId = value; }
        }

        private Int32 m_IsCancel;
        public Int32 IsCancel
        {
            get { return m_IsCancel; }
            set { m_IsCancel = value; }
        }

        private string m_Site;
        public string Site
        {
            get { return m_Site; }
            set { m_Site = value; }
        }

        private string m_CancelledBy;
        public string CancelledBy
        {
            get { return m_CancelledBy; }
            set { m_CancelledBy = value; }
        }

        private DateTime m_CancelledDate;
        public DateTime CancelledDate
        {
            get { return m_CancelledDate; }
            set { m_CancelledDate = value; }
        }

        private string m_Reason;
        public string Reason
        {
            get { return m_Reason; }
            set { m_Reason = value; }
        }

        private Int32 m_UserId;
        public Int32 UserId
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }

        private string m_strCond;
        public string StrCond
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

        private string m_Temp;
        public string Temp
        {
            get { return m_Temp; }
            set { m_Temp = value; }
        }

        private bool m_IsDeleted;
        public bool IsDeleted
        {
            get { return m_IsDeleted; }
            set { m_IsDeleted = value; }
        }

        private bool m_CancelType;
        public bool CancelType
        {
            get { return m_CancelType; }
            set { m_CancelType = value; }
        }
       

         //--*RequisitionCafeDtls*-- 

        private Int32 m_RequisitionCancelDtlsId;
        public Int32 RequisitionCancelDtlsId
        {
            get { return m_RequisitionCancelDtlsId; }
            set { m_RequisitionCancelDtlsId = value; }
        }

        private Int32 m_ItemId;
        public Int32 ItemId
        {
            get { return m_ItemId; }
            set { m_ItemId = value; }
        }

        private decimal m_OrdQty;
        public decimal OrdQty
        {
            get { return m_OrdQty; }
            set { m_OrdQty = value; }
        }

        private string m_StrCondition;
        public string StrCondition
        {
            get { return m_StrCondition; }
            set { m_StrCondition = value; }
        }

        private string m_GrdDateCondition;
        public string GrdDateCondition
        {
            get { return m_GrdDateCondition; }
            set { m_GrdDateCondition = value; }
        }




        #endregion

        public RequisitionCancellation()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
