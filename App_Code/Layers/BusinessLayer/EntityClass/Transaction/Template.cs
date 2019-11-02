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
/// Summary description for Template
/// </summary>
public class Template
{
    #region [Constant]
    public static string _Action = "@Action";
    public static string _TemplateID = "@TemplateID";
    public static string _TemplateName = "@TemplateName";
    public static string _TemplateDate = "@TemplateDate";
    public static string _LoginID = "@LoginID";
    public static string _LoginDate = "@LoginDate";
    public static string _TemplateDtlsID = "@TemplateDtlsID";
    public static string _ItemDtlsID = "@ItemDtlsID";
    public static string _ItemID = "@ItemID";
    public static string _QTY = "@QTY";
    public static string _VendorID = "@VendorID";
    public static string _Rate = "@Rate";
    public static string _StrCondition = "@StrCondition";

    public static string _CategoryID = "@CategoryID";
    #endregion

    #region [Defination]
    private Decimal m_QTY;

    public Decimal QTY
    {
        get { return m_QTY; }
        set { m_QTY = value; }
    }
    private String m_CategoryID;

    public String CategoryID
    {
        get { return m_CategoryID; }
        set { m_CategoryID = value; }
    }
    private String m_StrCondition;

    public String StrCondition
    {
        get { return m_StrCondition; }
        set { m_StrCondition = value; }
    }

    private Decimal m_Rate;

    public Decimal Rate
    {
        get { return m_Rate; }
        set { m_Rate = value; }
    }
    private Int32 m_VendorID;

    public Int32 VendorID
    {
        get { return m_VendorID; }
        set { m_VendorID = value; }
    }

    private Int32 m_ItemDtlsID;

    public Int32 ItemDtlsID
    {
        get { return m_ItemDtlsID; }
        set { m_ItemDtlsID = value; }
    }   
    private Int32 m_ItemID;

    public Int32 ItemID
    {
        get { return m_ItemID; }
        set { m_ItemID = value; }
    }
    private Int32 m_TemplateDtlsID;

    public Int32 TemplateDtlsID
    {
        get { return m_TemplateDtlsID; }
        set { m_TemplateDtlsID = value; }
    }
    private String m_LoginDate;

    public String LoginDate
    {
        get { return m_LoginDate; }
        set { m_LoginDate = value; }
    }
    private Int32 m_LoginID;

    public Int32 LoginID
    {
        get { return m_LoginID; }
        set { m_LoginID = value; }
    }
    private String m_TemplateDate;

    public String TemplateDate
    {
        get { return m_TemplateDate; }
        set { m_TemplateDate = value; }
    }
    private String m_TemplateName;

    public String TemplateName
    {
        get { return m_TemplateName; }
        set { m_TemplateName = value; }
    }
    private Int32 m_Action;

    public Int32 Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    private Int32 m_TemplateID;

    public Int32 TemplateID
    {
        get { return m_TemplateID; }
        set { m_TemplateID = value; }
    }
    #endregion

    #region [Stored Procedure]
    public static string SP_Template = "Template";
    #endregion
    public Template()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
