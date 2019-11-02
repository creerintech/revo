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

namespace MayurInventory.EntityClass
{
    public class UserMaster
    {
        #region Column Constant

        //User Master Info==============
        public const string _Action = "@Action";
        public const string _UserId = "@UserId";
        public const string _UserName = "@UserName";
        public const string _EmailId = "@EmailId";
        public const string _LoginName = "@LoginName";
        public const string _mob = "@mob";
        public const string _Password = "@Password";
        public const string _ConfirmPassword = "@ConfirmPassword";
        public const string _UserType = "@UserType";
        public const string _IsAdmin = "@IsAdmin";
        public const string _Email = "@Email";
        public const string _Cheked = "@Checked";

        //------- Who Fields ------
        public const string _LUserID = "@LUserID";
        public const string _LoginDate = "@LoginDate";
        public const string _IsDeleted = "@IsDeleted";


        //User Details Info==============                
        public const string _FkUserId = "@Fk_UserId";
        public const string _FormCaption = "@FormCaption";
        public const string _ViewAuth = "@ViewAuth";
        public const string _AddAuth = "@AddAuth";
        public const string _DelAuth = "@DelAuth";
        public const string _EditAuth = "@EditAuth";
        public const string _PrintAuth = "@PrintAuth";

        //------- Conditional Field --------
        public const string _RepCondition = "@strCond";
        public const string _CafeteriaId = "@CafeteriaId";
        #endregion

        #region Definitions

        //User Master Info==============
        private Int32 m_CafeteriaId;

        public Int32 CafeteriaId
        {
            get { return m_CafeteriaId; }
            set { m_CafeteriaId = value; }
        }
        //UserID
        private Int32 m_UserID;
        public Int32 UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        //UserName
        private string m_UserName;
        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }



        private string m_mob;
        public string mob
        {
            get { return m_mob; }
            set { m_mob = value; }
        }  
        //EmailId
        private string m_EmailId;
        public string EmailId
        {
            get { return m_EmailId; }
            set { m_EmailId = value; }
        }
        //LoginName
        private string m_LoginName;

        public string LoginName
        {
            get { return m_LoginName; }
            set { m_LoginName = value; }
        }
       
        //UserType
        private string m_UserType;
        public string UserType
        {
            get { return m_UserType; }
            set { m_UserType = value; }
        }
        //Password
        private string m_Password;
        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }
        //IsAdmin
        private bool m_IsAdmin;
        public bool IsAdmin
        {
            get { return m_IsAdmin; }
            set { m_IsAdmin = value; }
        }
        //IsDeleted
        private bool m_IsDeleted;
        public bool IsDeleted
        {
            get { return m_IsDeleted; }
            set { m_IsDeleted = value; }
        }
        #region Who Column Details

        //LUserID
        private Int32 m_LUserID;
        public Int32 LUserID
        {
            get { return m_LUserID; }
            set { m_LUserID = value; }

        }
        // LoginDate
        private DateTime m_LoginDate;
        public DateTime LoginDate
        {
            get { return m_LoginDate; }
            set { m_LoginDate = value; }
        }
        #endregion

        //User Master Info==============


        //User Details Info==============                      

        private Int32 m_FkUserId;

        public Int32 FkUserId
        {
            get { return m_FkUserId; }
            set { m_FkUserId = value; }
        }

        //FormCaption
        private string m_FormCaption;
        public string FormCaption
        {
            get { return m_FormCaption; }
            set { m_FormCaption = value; }
        }

        //ViewAuth
        private bool m_ViewAuth;
        public bool ViewAuth
        {
            get { return m_ViewAuth; }
            set { m_ViewAuth = value; }
        }

        //AddAuth
        private bool m_AddAuth;
        public bool AddAuth
        {
            get { return m_AddAuth; }
            set { m_AddAuth = value; }
        }

        //DelAuth
        private bool m_DelAuth;
        public bool DelAuth
        {
            get { return m_DelAuth; }
            set { m_DelAuth = value; }
        }

        //EditAuth
        private bool m_EditAuth;
        public bool EditAuth
        {
            get { return m_EditAuth; }
            set { m_EditAuth = value; }
        }

        //PrintAuth
        private bool m_PrintAuth;
        public bool PrintAuth
        {
            get { return m_PrintAuth; }
            set { m_PrintAuth = value; }
        }
        //User Details Info==============

        //Email Check

        private bool m_Email;

        public bool Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }
        private bool m_Cheked;

        public bool Cheked
        {
            get { return m_Cheked; }
            set { m_Cheked = value; }
        }
        
        #endregion

        #region Stored Procedures

        public const string SP_UserMaster = "SP_USERMASTER";

        #endregion

    }
}
