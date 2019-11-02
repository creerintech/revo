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
using System.Collections.Generic;
 

    public class SundryCreditorReport
    {
        # region [Column Constant]

        public const string _Action = "@Action";
        public const string _LedgerID = "@LedgerID";
        public const string _RepCondition = "@RepCond";        
        
        # endregion

        #region [Property Defination]

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

        #region [Stored Procedure]

        public const string PRO_SUNDRY_CREDITOR_REPORT = "PRO_SUNDARY_REPORT";

        #endregion        
    }
