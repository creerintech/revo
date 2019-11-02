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
/// Summary description for RecipeTemplate
/// </summary>
/// 
namespace MayurInventory.EntityClass
{
    public class RecipeTemplate
    {
        #region[Constant]
        //--**RecipeMaster**--
        public const string _Action = "@Action";
	    public const string _RecipeId="@RecipeId"; 
	    public const string _MenuItem="@MenuItem"; 
        public const string _AmtPerPlate="@AmtPerPlate"; 

        //--**RecipeDetails **--
        public const string _RecipeDetailsId="@RecipeDetailsId";
        public const string _ItemId="@ItemId";
        public const string _Qty="@Qty";
        public const string _SubUnitId = "@SubUnitId";
        public const string _IngredAmt="@IngredAmt";
        public const string _strCond = "@strCond"; 
        public const string _UserId = "@UserId";
        public const string _LoginDate = "@LoginDate";

        public const string _ActualRate = "@ActualRate";
        public const string _QtyPerUnit = "@QtyPerUnit";

        //StockDetails
        public const string _StockDate = "@StockDate";
        public const string _StockLocationID = "@StockLocationID";
        public const string _StockUnitId = "@StockUnitId";
        #endregion

        #region[Defination]
        //Recipemaster
        private Int32 m_Action;
        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }
        private Int32 m_RecipeId;
        public Int32 RecipeId
        {
            get { return m_RecipeId; }
            set { m_RecipeId = value; }
        }
        private string m_MenuItem;
        public string MenuItem
        {
            get { return m_MenuItem; }
            set { m_MenuItem = value; }
        }
        private decimal m_AmtPerPlate;
        public decimal AmtPerPlate
        {
            get { return m_AmtPerPlate; }
            set { m_AmtPerPlate = value; }
        }
        //RecipeDetails
        private Int32 m_RecipeDetailsId;
        public Int32 RecipeDetailsId
        {
            get { return m_RecipeDetailsId; }
            set { m_RecipeDetailsId = value; }
        }
        private Int32 m_ItemId;
        public Int32 ItemId
        {
            get { return m_ItemId; }
            set { m_ItemId = value; }
        }
        private decimal m_Qty;
        public decimal Qty
        {
            get { return m_Qty; }
            set { m_Qty = value; }
        }
        private decimal m_IngredAmt;
        public decimal IngredAmt
        {
            get { return m_IngredAmt; }
            set { m_IngredAmt = value; }
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
        private decimal m_ActualRate;
        public decimal ActualRate
        {
            get { return m_ActualRate; }
            set { m_ActualRate = value; }
        }
        private decimal m_QtyPerUnit;
        public decimal QtyPerUnit
        {
            get { return m_QtyPerUnit; }
            set { m_QtyPerUnit = value; }
        }

        #endregion

        #region[StoreProcedure]
        public const string SP_RecipeTemplate= "SP_RecipeTemplate";
        #endregion

        public RecipeTemplate()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
