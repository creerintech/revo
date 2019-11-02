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
/// Summary description for ClientCompMaster
/// </summary>
public class ClientCompMaster
{
    #region Constants
    public static string _Action = "@Action"; 
    public static string _CompanyName="@CompanyName";
    public static string _SuplierFor = "@SupplierFor";
    public static string _Address = "@Address";
    public static string _WebUrl = "@WebSite";
    public static string _Remark = "@Remark";
    public static string _ClienCompanyId = "@ClientCompanyId";
    public static string _CreatedBy = "@CreatedBy";
    public static string _CreatedDate = "@CreatedDate";
    public static string _UpdatedBy = "@UpdatedBy";
    public static string _UpdatedDate = "@UpdatedDate";
    public static string _IsDelete = "@IsDelete";
    public static string _DeletedBy = "@DeletedBy";
    public static string _DeletedDate = "@DeletedDate";

    public static string _PersonId = "@PersonId";
    public static string _PersonName="@PersonName";
    public static string _Designation="@Designation";
    public static string _ContactNo1="@ContactNo1";
    public static string _ContactNo2="@ContactNo2";
    public static string _EmailId1="@EmailId1";
    public static string _EmailId2="@EmailId2";
    public static string _Note = "@Note";
    public static string _StrCond = "@StrCond";
    #endregion

    #region
    private int m_Action;
    public int Action
    {
        get { return m_Action;}
        set { m_Action=value ; }
    }
    private int m_ClientCompanyId;
    public Int32 ClientCompanyId
    {
        get { return m_ClientCompanyId; }
        set { m_ClientCompanyId = value; }
    }
    private string m_StrCond;
    public string StrCond
    {
        get { return m_StrCond; }
        set { m_StrCond = value; }
    }
    private Int32 m_CompanyId;
    public Int32 CompanyId
    {
        get { return m_CompanyId; }
        set { m_CompanyId = value; }
    }
    private string m_CompanyName;
    public string CompanyName
    {
        get {return m_CompanyName;}
        set { m_CompanyName=value; }
    }

    private string m_SupplierFor;
    public string SupplierFor
    {
        get { return m_SupplierFor; }
        set { m_SupplierFor=value;}
    }
    private string m_Address;
    public string Address
    { 
        get { return m_Address; }
        set { m_Address=value;}
    }
    
    private string m_WebUrl;
    public string WebUrl
    {
        get { return m_WebUrl; }
        set { m_WebUrl=value ; }
    }
    private string m_Remark;
    public string Remark
    {
        get { return m_Remark; }
        set { m_Remark=value ; }
    }
    private bool m_IsDelete;
    public bool IsDelete
    {
        get { return m_IsDelete; }
        set { m_IsDelete = value; }
    }
    private Int32 m_CreatedBy;
    public Int32 CreatedBy
    {
        get { return m_CreatedBy; }
        set {  m_CreatedBy=value; }
    }
    private DateTime m_CreatedDate;
    public DateTime CreatedDate
    {
        get { return m_CreatedDate; }
        set { m_CreatedDate=value; }
    }
    private Int32 m_DeletedBy;
    public Int32 DeletedBy
    {
        get { return m_DeletedBy; }
        set { m_DeletedBy = value; }
    }
    private DateTime m_DeletedDate;
    public DateTime DeletedDate
    {
        get { return m_DeletedDate; }
        set { m_DeletedDate = value; }
    }
    private Int32 m_UpdatedBy;
    public Int32 UpdatedBy
    {
        get { return m_UpdatedBy; }
        set { m_UpdatedBy = value; }
    }
    private DateTime m_UpdatedDate;
    public DateTime UpdatedDate
    {
        get {return m_UpdatedDate; }
        set { m_UpdatedDate = value; }
    }
    private Int32 m_PersonId;
    public Int32 PersonId
    {
        get { return m_PersonId; }
        set { m_PersonId = value; }
    }
    private string m_PersonName;
    public string PersonName
    {
        get { return m_PersonName; }
        set { m_PersonName=value; }
    }

    private string m_Designation;
    public string Designation
    {
        get { return m_Designation; }
        set {m_Designation= value; }
    }
    private string m_ContactNo1;
    public string ContactNo1
    {
        get { return m_ContactNo1;}
        set {  m_ContactNo1=value; }
    }
    private string m_ContactNo2;
    public string ContactNo2
    {
        get { return m_ContactNo2; }
        set {m_ContactNo2=value; }
    }
    private string m_EmailId1;
    public string EmailId1
    {
        get { return m_EmailId1; }
        set {m_EmailId1= value; }
    }
    private string m_EmailId2;
    public string EmailId2
    {
        get { return m_EmailId2; }
        set {  m_EmailId2=value; }
    }

    private string m_Note;
    public string Note
    {
        get { return m_Note;}
        set {  m_Note=value;}
    }
    #endregion
    #region Procedure
    public static string SP_ClientCompanyMaster = "SP_ClientCompanyMaster";

    #endregion

    public ClientCompMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
