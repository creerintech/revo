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
/// Summary description for IndentEmail
/// </summary>
public class IndentEmail
{
     #region[Constants]
    //----****Material Requisition Master****----
    public static string _Action = "@Action";
    public static string _RequisitionCafeId = "@RequisitionCafeId";
    public static string _RequisitionNo = "RequisitionNo";
    public static string _RequisitionDate = "RequisitionDate";
    public static string _CafeteriaId = "CafeteriaId";
    public static string _UserId = "UserId";
    public static string _LoginDate = "LoginDate";
    public static string _IsDeleted = "IsDeleted";
    public static string _StrCondition = "StrCondition";
    public static string _VendorId = "@VendorId";
    public static string _Rate = "@Rate";
    public static string _IsCancel = "@IsCancel";	
    //----****Material Requisition Details****----
   
    public static string _RequisitionCafeDtlsId = "@RequisitionCafeDtlsId";
    public static string _ItemId = "@ItemId";
    public static string _Qty = "@Qty";
    public static string _QtyByCafe = "QtyByCafe";
    public static string _UnitId = "UnitId";
    public static string _ExpdDate = "@ExpdDate";
    public static string _PriorityID = "@PriorityID";
    public static string _Instruction = "@Instruction";
    public static string _TemplateID = "@TemplateID";
    public static string _AvlQty = "@AvlQty";
    public static string _TransitQty = "@TransitQty";
    public static string _IsCostCentre = "@IsCostCentre";
    public static string _Remark = "@Remark";

    //--Newly Added Field--
    public static string _ItemDetailsId = "@ItemDetailsId";
    public static string _UnitConvDtlsId = "@UnitConvDtlsId";
    public static string _RemarkForPO = "@RemarkForPO";
    public static string _RemarkIND = "@RemarkIND";

    #endregion

    #region[Defination]

    private String m_RemarkIND;

    public String RemarkIND
    {
        get { return m_RemarkIND; }
        set { m_RemarkIND = value; }
    }


    private String m_RemarkForPO;

    public String RemarkForPO
    {
        get { return m_RemarkForPO; }
        set { m_RemarkForPO = value; }
    }


    private Int32 m_ItemDetailsId;

    public Int32 ItemDetailsId
    {
        get { return m_ItemDetailsId; }
        set { m_ItemDetailsId = value; }
    }
    private Int32 m_UnitConvDtlsId;

    public Int32 UnitConvDtlsId
    {
        get { return m_UnitConvDtlsId; }
        set { m_UnitConvDtlsId = value; }
    }

    private string m_Remark;
    public string Remark
    {
        get { return m_Remark; }
        set { m_Remark = value; }
    }

    private bool m_IsCancel;
    public bool IsCancel
    {
        get { return m_IsCancel; }
        set { m_IsCancel = value; }
    }

    private Decimal m_AvlQty;
    public Decimal AvlQty
    {
        get { return m_AvlQty; }
        set { m_AvlQty = value; }
    }
    private Decimal m_TransitQty;
    public Decimal TransitQty
    {
        get { return m_TransitQty; }
        set { m_TransitQty = value; }
    }
    private Decimal m_Rate;
    public Decimal Rate
    {
        get { return m_Rate; }
        set { m_Rate = value; }
    }
    private Int32 m_VendorId;
    public Int32 VendorId
    {
        get { return m_VendorId; }
        set { m_VendorId = value; }
    }
    private Int32 m_IsCostCentre;
    public Int32 IsCostCentre
    {
        get { return m_IsCostCentre; }
        set { m_IsCostCentre = value; }
    }
    private Int32 m_TemplateID;
    public Int32 TemplateID
    {
        get { return m_TemplateID; }
        set { m_TemplateID = value; }
    }
    //---**Material Requisition Master**----

        //mAction
        private string m_Action;
        public string Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        //RequisitionId
        private Int32 m_RequisitionCafeId;
        public Int32 RequisitionCafeId
        {
            get{return m_RequisitionCafeId;}
            set{m_RequisitionCafeId=value;}
        }

        //RequisitionNo
        private string m_RequisitionNo;
        public string RequisitionNo
        {
            get{return m_RequisitionNo;}
            set{m_RequisitionNo=value;}
        }

        //RequisitionDate
        private DateTime  m_RequisitionDate;
        public DateTime RequisitionDate
        {
            get{return m_RequisitionDate;}
            set{m_RequisitionDate=value;}
        }

        //CafeteriaId
        private Int32 m_CafeteriaId;
        public Int32  CafeteriaId
        {
            get{return m_CafeteriaId;}
            set{m_CafeteriaId=value;}
        }
        
        //UserId
        private Int32 m_UserId;
        public Int32 UserId
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }

        //LoginDate
        private DateTime m_LoginDate;
        public DateTime LoginDate
        {
            get{return m_LoginDate;}
            set{m_LoginDate=value;}
        }

        //IsDeleted
        private bool m_IsDeleted;
        public bool IsDeleted
        {
            get{return m_IsDeleted;}
            set{m_IsDeleted=value;}
        }

        //StrCondition
        private string m_StrCondition;
        public string StrCondition
        {
            get{return StrCondition;}
            set{m_StrCondition=value;}
        }


    //---** Material Requisition Details**---

        //RequisitionCafeDtlsId
        private string m_RequisitionCafeDtlsId;
        public string RequisitionCafeDtlsId
        {
            get { return m_RequisitionCafeDtlsId; }
            set { m_RequisitionCafeDtlsId = value; }
        }

        //ItemId
        private Int32 m_ItemId;
        public Int32 ItemId
        {
            get { return m_ItemId; }
            set { m_ItemId = value; }
        }

        //Qty
        private decimal m_Qty;
        public decimal Qty
        {
            get { return m_Qty; }
            set { m_Qty = value; }
        }
        
        //QtyByCafe
        private decimal m_QtyByCafe;
        public decimal QtyByCafe
        {
            get { return m_QtyByCafe; }
            set { m_QtyByCafe = value; }
        }

        //UnitId
        private Int32 m_UnitId;
        public Int32 UnitId
        {
            get { return m_UnitId; }
            set { m_UnitId = value; }
        }

        //ExpdDate
        private DateTime m_ExpdDate;
        public DateTime ExpdDate
        {
            get { return m_ExpdDate; }
            set { m_ExpdDate = value; }
        }

        //Priority
        private Int32 m_PriorityID;
        public Int32 PriorityID
        {
            get { return m_PriorityID; }
            set { m_PriorityID = value; }
        }

        //Instruction
        private string m_Instruction;
        public string Instruction
        {
            get { return m_Instruction; }
            set { m_Instruction = value; }
        }

    #endregion

    #region[procedure]
        public static string SP_EmailIndent = "SP_EmailIndent";
    #endregion


	public IndentEmail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
