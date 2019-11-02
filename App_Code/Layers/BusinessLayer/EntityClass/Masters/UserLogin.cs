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
/// Summary description for UserLogin
/// </summary>
/// 
namespace MayurInventory.EntityClass
{
    public class UserLogin
    {
        #region Constant
            public static string _Action = "@Action";
            public static string _userName = "@userName";
            public static string _Password = "@Password";
            public static string _CafeID = "@CafeID";
        #endregion

        #region Defination
            private Int32 m_CafeID;

            public Int32 CafeID
            {
                get { return m_CafeID; }
                set { m_CafeID = value; }
            }
        private string m_userName;

        public string UserName
        {
            get { return m_userName; }
            set { m_userName = value; }
        }
        private string m_Password;

        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }
        private string m_Action;

        #endregion

        #region Stored_Proc
            public static string SP_UserLogin = "SP_USERLOGIN";
        #endregion

    }
}
