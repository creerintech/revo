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
/// Summary description for SundaryReport
/// </summary>
/// 

    public class SundaryReport
    {
        # region Column Constant

        public const string _Action = "@Action";
        public const string _LedgerID = "@LedgerID";
        public const string _LedgerName = "@LedgerName";
        public const string _LedgerType = "@LedgerType";
        public const string _LedgerNotes = "@LedgerNotes";
        public const string _LedgerGroupID = "@LedgerGroupID";
        public const string _LedgerSubGroupID = "@LedgerSubGroupID";
        public const string _LedgerOB = "@LedgerOB";
        public const string _LedgerDRCR = "@LedgerDRCR";

        public const string _CreatedBy = "@LoginID";
        public const string _CreatedDate = "@LoginDate";
        public const string _UpdatedBy = "@LoginID";
        public const string _UpdatedDate = "@LoginDate";
        public const string _DeletedBy = "@LoginID";
        public const string _DeletedDate = "@LoginDate";
        public const string _IsDeleted = "@IsDeleted";

        public const string _RepCondition = "@RepCond";
        public const string _ToDate = "@ToDate";
        public const string _FromDate = "@FromDate";


        # endregion
        #region Store Procedure

        public const string PRO_Sundary_Report = "PRO_SUNDARY_REPORT";

        #endregion
        #region Property Defination

        // ToDate
        private string m_ToDate;
        public string ToDate
        {
            get { return m_ToDate; }
            set { m_ToDate = value; }
        }
        // FromDate
        private string m_FromDate;
        public string FromDate
        {
            get { return m_FromDate; }
            set { m_FromDate = value; }
        }

        // Action 
        private Int32 m_Action;
        public Int32 Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        // LedgerID
        private Int32 m_LedgerID;
        public Int32 LedgerID
        {
            get { return m_LedgerID; }
            set { m_LedgerID = value; }
        }

        // LedgerName
        private String m_LedgerName;
        public String LedgerName
        {
            get { return m_LedgerName; }
            set { m_LedgerName = value; }
        }

        // LedgerType
        private String m_LedgerType;
        public String LedgerType
        {
            get { return m_LedgerType; }
            set { m_LedgerType = value; }
        }

        // LedgerNotes
        private String m_LedgerNotes;
        public String LedgerNotes
        {
            get { return m_LedgerNotes; }
            set { m_LedgerNotes = value; }
        }

        // LedgerGroupID
        private Int32 m_LedgerGroupID;
        public Int32 LedgerGroupID
        {
            get { return m_LedgerGroupID; }
            set { m_LedgerGroupID = value; }
        }

        // LedgerSubGroupID
        private Int32 m_LedgerSubGroupID;
        public Int32 LedgerSubGroupID
        {
            get { return m_LedgerSubGroupID; }
            set { m_LedgerSubGroupID = value; }
        }

        // LedgerOB
        private Decimal m_LedgerOB;
        public Decimal LedgerOB
        {
            get { return m_LedgerOB; }
            set { m_LedgerOB = value; }
        }

        // LedgerDRCR
        private String m_LedgerDRCR;
        public String LedgerDRCR
        {
            get { return m_LedgerDRCR; }
            set { m_LedgerDRCR = value; }
        }

        //RepCondition
        private String m_RepCondition;
        public String RepCondition
        {
            get { return m_RepCondition; }
            set { m_RepCondition = value; }
        }

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

        public SundaryReport()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
