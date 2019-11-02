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
using System.Collections.Generic;
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using MayurInventory.DALSQLHelper;
/// <summary>
/// Summary description for DMPriority
/// </summary>
namespace MayurInventory.DataModel
{
    public class DMChangePassword : Utility.Setting
    {
        public int UpdatePassword(int ID,string OldPass, string NewPass,int LogID, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("Action", SqlDbType.BigInt);
                SqlParameter pUserID = new SqlParameter("UserID", SqlDbType.BigInt);
                SqlParameter pPassword = new SqlParameter("Password", SqlDbType.NVarChar);
                SqlParameter POPassword = new SqlParameter("OPassword", SqlDbType.NVarChar);
                SqlParameter PLoginID = new SqlParameter("LoginID", SqlDbType.BigInt);
                SqlParameter PLoginDate = new SqlParameter("LoginDate", SqlDbType.DateTime);

                pAction.Value = 2;
                pUserID.Value =ID;
                pPassword.Value = NewPass;
                POPassword.Value = OldPass;
                PLoginID.Value = LogID;
                PLoginDate.Value = DateTime.Now;

                SqlParameter[] param = new SqlParameter[] { pAction,pUserID,POPassword,pPassword,PLoginID,PLoginDate};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, "SP_ChangePassword", param);

                if (iInsert > 0)
                {
                    CommitTransaction();
                }
                else
                {
                    RollBackTransaction();
                }
            }

            catch (Exception ex)
            {
                RollBackTransaction();
                strError = ex.Message;
            }

            finally
            {
                Close();
            }
            return iInsert;
        }

        public DataSet GetUserForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter("Action", SqlDbType.BigInt);
                SqlParameter pPriorityId = new SqlParameter("UserID", SqlDbType.BigInt);

                pAction.Value = 1;
                pPriorityId.Value = ID;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_ChangePassword", pAction, pPriorityId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return DS;
        }

        public DMChangePassword()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
