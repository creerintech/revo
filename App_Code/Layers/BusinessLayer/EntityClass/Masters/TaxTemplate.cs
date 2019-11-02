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
using MayurInventory.EntityClass;
using MayurInventory.DALSQLHelper;
using MayurInventory.DB;
using MayurInventory.Utility;


/// <summary>
/// Summary description for TaxTemplate
/// </summary>
namespace MayurInventory.EntityClass
{
    public class TaxTemplate
    {
        # region Column Constant
        public const string _RepCondition = "@RepCondition";
        public const string _Action = "@Action";
        public const string _TaxID = "@TaxID";
        public const string _TaxName = "@TaxName";
        public const string _TaxPer = "@TaxPer";

        public const string _TaxAmt = "@TaxAmt";
        public const string _EffectiveDate = "@EffectiveDate";
        public const string _CreatedBy = "@LoginID";
        public const string _CreatedDate = "@LoginDate";
        public const string _UpdatedBy = "@LoginID";
        public const string _UpdatedDate = "@LoginDate";
        public const string _DeletedBy = "@LoginID";
        public const string _DeletedDate = "@LoginDate";
        public const string _IsDeleted = "@IsDeleted";

        # endregion
        #region Store Procedure

        public const string PRO_TAXTemplateMASTER = "PRO_TAXTemplateMASTER";

        #endregion
        #region Property Defination

        public Int32 TaxId { get; set; }
        public string TaxName { get; set; }
        public string TaxPer { get; set; }
        public string TaxAmt { get; set; }
        public DateTime EffectiveDate { get; set; }

        #region Authorization Details
        // Created By
        private Int32 m_CreatedByID;
        public Int32 CreatedByID
        {
            get { return m_CreatedByID; }
            set { m_CreatedByID = value; }

        }
        // Created Date
        private DateTime m_CreatedByDate;
        public DateTime CreatedDate
        {
            get { return m_CreatedByDate; }
            set { m_CreatedByDate = value; }

        }
        //Updated By
        private Int32 m_UpdatedByID;
        public Int32 UpdatedByID
        {
            get { return m_UpdatedByID; }
            set { m_UpdatedByID = value; }

        }
        // Updated Date
        private DateTime m_UpdatedByDate;
        public DateTime UpdatedDate
        {
            get { return m_UpdatedByDate; }
            set { m_UpdatedByDate = value; }

        }

        //Deleted By
        private Int32 m_DeletedByID;
        public Int32 DeletedByID
        {
            get
            {
                return
                    m_DeletedByID;
            }
            set { m_DeletedByID = value; }

        }
        // Deleted Date
        private DateTime m_DeletedDate;
        public DateTime DeletedDate
        {
            get { return m_DeletedDate; }
            set { m_DeletedDate = value; }

        }
        //IsDeleted
        private bool m_IsDeleted;

        public bool IsDeleted
        {
            get { return m_IsDeleted; }
            set { m_IsDeleted = value; }
        }

        #endregion



        #endregion
        public TaxTemplate()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}