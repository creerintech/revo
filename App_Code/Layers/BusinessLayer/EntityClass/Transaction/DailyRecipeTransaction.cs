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
/// Summary description for DailyRecipeTransaction
/// </summary>
namespace MayurInventory.EntityClass
{
    public class DailyRecipeTransaction
    {

        #region[Constants]
        //-----**DailyRecipeTransaction**----
        public static string _Action="@Action";
        public static string _OrderId = "@OrderId";
        public static string _OrderNo = "@OrderNo";
        public static string _OrderDate = "@OrderDate";
        public static string _RecipeId = "@RecipeId";
        public static string _Qty= "@Qty";
        public static string _TotalAmt = "@TotalAmt";
        public static string _TotalOrderCost = "@TotalOrderCost";
        public static string _strCond = "@strCond";
        public static string _UserId = "@UserId";
        public static string _LoginDate = "@LoginDate";
        public static string _DeletedDate = "@DeletedDate";
        public static string _SubUnitId = "@SubUnitId";

        //-----**StockDetails**-------
        public static string _ItemId = "@ItemId";
        public static string _UnitId = "@UnitId";
        public static string _StockDate = "@StockDate";
        public static string _StockLocationID = "@StockLocationID";
        public static string _StockId = "@StockId";
        public static string _QtyPerUnit = "@QtyPerUnit";
        public static string _ActualRate = "@ActualRate"; 


        #endregion
       
        #region[Definations]
        //-----**DailyRecipeTransaction**----
            private Int32 m_Action;
            public Int32 Action
            {
                get { return m_Action; }
                set { m_Action = value; }
            }
            private Int32 m_OrderId;
            public Int32 OrderId
            {
                get { return m_OrderId; }
                set { m_OrderId = value; }
            }
            private string m_OrderNo;
            public string OrderNo
            {
                get { return m_OrderNo; }
                set { m_OrderNo = value; }
            }
            private DateTime m_OrderDate;
            public DateTime OrderDate
            {
                get { return m_OrderDate; }
                set { m_OrderDate = value; }
            }
            private Int32 m_RecipeId;
            public Int32 RecipeId
            {
                get { return m_RecipeId; }
                set { m_RecipeId = value; }
            }
            private decimal m_Qty;
            public decimal Qty
            {
                get { return m_Qty; }
                set { m_Qty = value; }
            }
            private decimal m_TotalAmt;
            public decimal TotalAmt
            {
                get { return m_TotalAmt; }
                set { m_TotalAmt = value; }
            }
            private decimal m_TotalOrderCost;
            public decimal TotalOrderCost
            {
                get { return m_TotalOrderCost; }
                set { m_TotalOrderCost = value; }
            }
            private string m_strCond;
            public string StrCond
            {
                get { return m_strCond; }
                set { m_strCond = value; }
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
            private Int32 m_SubUnitId;
            public Int32 SubUnitId
            {
                get { return m_SubUnitId; }
                set { m_SubUnitId = value; }
            }

            //-----**StockDetails**-------
            private Int32 m_ItemId;
            public Int32 ItemId
            {
                get { return m_ItemId; }
                set { m_ItemId = value; }
            }
            private Int32 m_UnitId;
            public Int32 UnitId
            {
                get { return m_UnitId; }
                set { m_UnitId = value; }
            }
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
            private Int32 m_StockId;
            public Int32 StockId
            {
                get { return m_StockId; }
                set { m_StockId = value; }
            }
            private decimal m_QtyPerUnit;
            public decimal QtyPerUnit
            {
                get { return m_QtyPerUnit; }
                set { m_QtyPerUnit = value; }
            }
            private decimal m_ActualRate;
            public decimal ActualRate
            {
                get { return m_ActualRate; }
                set { m_ActualRate = value; }
            }



        #endregion

        #region[storeProcedure]
          public const string SP_DailyRecipeTransaction="SP_DailyRecipeTransaction";
        #endregion

        public DailyRecipeTransaction()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
