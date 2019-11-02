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
/// Summary description for SubCategory
/// </summary>
public class SubCategory
{
    #region Constants
        public static string _Action="@Action";
	    public static string _SubCategoryId="@SubCategoryId";
	    public static string _SubCategory="@SubCategory";
        public static string _Category = "@Category";
        public static string _CategoryId = "@CategoryId";
	    public static string _UserId="@UserId";
	    public static string _LoginDate="@LoginDate";
	    public static string _IsDeleted="@IsDeleted";
        public static string _StrCondition = "@strCond";
        public static string _Remark = "@Remark";
    #endregion

    #region Definition
        public string Remark { get; set; }

        private Int32 m_Action;

        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        private Int32 m_SubCategoryId;

        public Int32 SubCategoryId
        {
            get { return m_SubCategoryId; }
            set { m_SubCategoryId = value; }
        }
        private Int32 m_CategoryId;

        public Int32 CategoryId
        {
            get { return m_CategoryId; }
            set { m_CategoryId = value; }
        }

        private string m_SubCategory;

        public string SubCategoryName
        {
            get { return m_SubCategory; }
            set { m_SubCategory = value; }
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

        private string m_Category;

        public string Category
        {
            get { return m_Category; }
            set { m_Category = value; }
        }

        private string m_StrCondition;

        public string StrCondition
        {
            get { return m_StrCondition; }
            set { m_StrCondition = value; }
        }
    #endregion

    
      #region Procedure
        public static string SP_SubCategory = "SP_SubCategory";
    #endregion


    public SubCategory()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
