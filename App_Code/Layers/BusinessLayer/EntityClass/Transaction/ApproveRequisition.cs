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
/// Summary description for ApproveRequisition
/// </summary>
public class ApproveRequisition
{
    #region[Constants]

    //--**ApproveRequisition**--
    public static string _Action = "@Action";
    public static string _RequisitionCafeId = "@RequisitionCafeId";
    public static string _ReqStatus= "@ReqStatus";
    public static string _GeneratedTime = "@GeneratedTime";
    public static string _ApprovedTime = "@ApprovedTime";
    public static string _AuthorizedTime = "@AuthorizedTime";
    public static string _UserId = "@UserId";
    public static string _LoginDate = "@LoginDate";
    public static string _IsDeleted = "@IsDeleted";
    public static string _RptCond= "@RptCond";

    #endregion

    #region[Definations]
    //--**PurchseOrder**--
    private Int32 m_Action;
    public Int32 Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    private Int32 m_RequisitionCafeId;
    public Int32 RequisitionCafeId
    {
        get { return m_RequisitionCafeId; }
        set { m_RequisitionCafeId = value; }
    }
   
    private string m_ReqStatus;
    public string ReqStatus
    {
        get { return m_ReqStatus; }
        set { m_ReqStatus = value; }
    }

    private DateTime m_GeneratedTime;
    public DateTime GeneratedTime
    {
        get { return m_GeneratedTime; }
        set { m_GeneratedTime = value; }
    }
    private DateTime m_ApprovedTime;
    public DateTime ApprovedTime
    {
        get { return m_ApprovedTime; }
        set { m_ApprovedTime = value; }
    }
    private DateTime m_AuthorizedTime;
    public DateTime AuthorizedTime
    {
        get { return m_AuthorizedTime; }
        set { m_AuthorizedTime = value; }
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

    private string m_RptCond;
    public string RptCond
    {
        get { return m_RptCond; }
        set { m_RptCond = value; }
    }

    #endregion

    #region[Store Procedures]
    public static string SP_ApproveRequisition= "SP_ApproveRequisition";
    #endregion

    public ApproveRequisition()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
