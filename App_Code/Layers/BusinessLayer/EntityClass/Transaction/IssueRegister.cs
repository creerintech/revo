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
/// Summary description for IssueRegister
/// </summary>
public class IssueRegister
{
    #region[Constants]
    //Constant Of Issue Register
    public static string _Action ="@Action";
    public static string _IssueRegisterId="@IssueRegisterId"; 
    public static string _IssueNo="@IssueNo"; 
    public static string _IssueDate="@IssueDate";
    public static string _EmployeeId = "@EmployeeId";
    public static string _RequisitionCafeId="@RequisitionCafeId";
    public static string _UserId="@UserId";
    public static string _LoginDate="@LoginDate";
    public static string _IsDeleted="@IsDeleted";
    public static string _StrCondition="@StrCondition";

   //--* Constants of Issue Register Details *--  
    public static string _IssueRegisterDetailsId="@IssueRegisterdetailsId";
    public static string _ItemId="@ItemId";
    public static string _IssueQty="@IssueQty";
    public static string _PendingQty = "@PendingQty";
    public static string _Qty = "@Qty";
    public static string _StockLocationID = "@StockLocationID"; 
    public static string _Notes="@Notes";    
    #endregion

    #region[Defination]
    // Issue Register
     //mAction
    private string m_Action;
    public string Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    //IssueRegisterId
    private Int32 m_IssueRegisterId;
    public Int32 IssueRegisterId
    {
        get { return m_IssueRegisterId; }
        set { m_IssueRegisterId = value; }
    }

    //IssueNo
    private string m_IssueNo;
    public string IssueNo
    {
        get { return m_IssueNo; }
        set { m_IssueNo = value; }
    }

    //IssueDate
    private DateTime m_IssueDate;
    public DateTime IssueDate
    {
        get { return m_IssueDate; }
        set { m_IssueDate = value; }
    }

    //IssueTo
    private string m_EmployeeId;
    public string EmployeeId
    {
        get { return m_EmployeeId; }
        set { m_EmployeeId = value; }
    }

    //RequisitionCafeId
    private Int32 m_RequisitionCafeId;
    public Int32 RequisitionCafeId
    {
        get { return m_RequisitionCafeId; }
        set { m_RequisitionCafeId = value; }
    }
       
    //UserID
    private Int32 m_UserId;
    public Int32 UserId
    {
        get { return m_UserId; }
        set { m_UserId = value; }
    }

    //Logindate
    private DateTime m_Logindate;
    public DateTime LoginDate
    {
        get { return m_Logindate; }
        set { m_Logindate = value; }
    }

    //IsDeleted
    private bool m_IsDeleted;
    public bool IsDeleted
    {
        get { return m_IsDeleted; }
        set { m_IsDeleted = value; }
    }

    //StrCondition
    private string m_StrCondition;
    public string StrCondtion
    {
        get { return m_StrCondition; }
        set { m_StrCondition = value; }
    }

    // Defination Of Issue Register Details

    //IssueRegisterDetailsId
    private Int32 m_IssueRegisterDetailsId;
    public Int32 IssueRegisterDetailsId
    {

        get { return m_IssueRegisterDetailsId; }
        set { m_IssueRegisterDetailsId = value; }
    }
    //ItemId
    private Int32 m_ItemId;
    public Int32 ItemId
    {
        get { return m_ItemId; }
        set { m_ItemId=value;}
    }

    //PendingQty
    private decimal m_PendingQty;
    public decimal PendingQty
    {
        get { return m_PendingQty; }
        set { m_PendingQty = value; }
    }

    //IssueQty
    private decimal m_IssueQty;
    public decimal IssueQty
    {
        get { return m_IssueQty; }
        set { m_IssueQty = value; }
    }

    //Qty
    private decimal m_Qty;
    public decimal Qty
    {
        get { return m_Qty; }
        set { m_Qty = value; }
    }
    //StockLocationID
    private Int32 m_StockLocationID;
    public Int32 StockLocationID
    {
        get { return m_StockLocationID; }
        set { m_StockLocationID = value; }
    }
    //Notes
    private string m_Notes;
    public string Notes
    {
        get { return m_Notes; }
        set { m_Notes = value; }
    }
    /// <summary>
  
    /// </summary>
    #endregion

    #region[Procedure]
    public static string SP_IssueRegister = "SP_IssueRegisterNew";
    public static string SP_IssueRegisterReport = "SP_IssueRegisterReport";
    #endregion
    public IssueRegister()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
