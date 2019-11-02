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
/// Summary description for EmployeeMaster
/// </summary>
public class EmployeeMaster
{
    #region Constants
    public static string _Action = "@Action";
        public static string _EmployeeId="@EmployeeId";
	    public static string _EmpName="@EmpName";
	    public static string _Address="@Address";
	    public static string _TelNo="@TelNo";
	    public static string _MobileNo="@MobileNo";
	    public static string _Email="@Email";
	    public static string _Note="@Note";
	    public static string _UserId="@UserId";
	    public static string _LoginDate="@LoginDate";
	    public static string _IsDeleted="@IsDeleted";
        public static string _Department = "@Department";
    public static string _Designation = "@Designation";
    public static string  _StrCondition="@StrCond";

    #endregion

    #region Defination
        private string m_Action;
        public string Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        public string Department { get; set; }
        public string Designation { get; set; }
       
        private Int32 m_EmployeeId;

        public Int32 EmployeeId
        {
            get { return m_EmployeeId; }
            set { m_EmployeeId = value; }
        }

        private string m_EmpName;

        public string EmpName
        {
            get { return m_EmpName; }
            set { m_EmpName = value; }
        }

        private string m_Address;

        public string Address
        {
            get { return m_Address; }
            set { m_Address = value; }
        }

        private string m_TelNo;

        public string TelNo
        {
            get { return m_TelNo; }
            set { m_TelNo = value; }
        }

        private string m_MobileNo;

        public string MobileNo
        {
            get { return m_MobileNo; }
            set { m_MobileNo = value; }
        }

        private string m_Email;

        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }

        private string m_Note;

        public string Note
        {
            get { return m_Note; }
            set { m_Note = value; }
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

        private string m_StrCondition;

        public string StrCondition
        {
            get { return m_StrCondition; }
            set { m_StrCondition = value; }
        }

    #endregion

    #region Procedure
        public static string SP_EmployeeMaster = "SP_EmployeeMaster";
    #endregion

    public EmployeeMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
