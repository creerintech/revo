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
using System.Data.SqlClient;
using System.Collections.Generic;

using MayurInventory.DALSQLHelper;
using MayurInventory.DataModel;
using MayurInventory.DB;
using MayurInventory.EntityClass;
using MayurInventory.Utility;

/// <summary>
/// Summary description for DMCHStatus
/// </summary>
/// 
namespace MayurInventory.DataModel
{
    public class DMCHStatus : Utility.Setting
    {


        public int UpdateRecord(ref CHStatus Entity_Call, out String StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(CHStatus._Action, SqlDbType.BigInt);
                SqlParameter pUnit = new SqlParameter(CHStatus._PONO, SqlDbType.NVarChar);
                SqlParameter pUpdatedBy = new SqlParameter(CHStatus._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(CHStatus._LoginDate, SqlDbType.DateTime);

                pAction.Value = Entity_Call.Action;

                pUnit.Value = Entity_Call.PONO;
                pUpdatedBy.Value = Entity_Call.UserId;
                pUpdatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] param = new SqlParameter[] { pAction, pUnit, pUpdatedBy, pUpdatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, CHStatus.SP_ChangePoIndStatus, param);

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
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
            return iInsert;

        }

        public DataSet GetCheck(Int32 Actio, string IPNO, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pActionid = new SqlParameter(CHStatus._Action, SqlDbType.BigInt);
                SqlParameter pPOINDNO = new SqlParameter(CHStatus._PONO, SqlDbType.NVarChar);
                pActionid.Value = Actio;
                pPOINDNO.Value = IPNO;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, CHStatus.SP_ChangePoIndStatus, pActionid, pPOINDNO);

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
            return DS;


        }

        public DataSet GetCheckPOINWORD(Int32 Actio, string IPNO, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pActionid = new SqlParameter(CHStatus._Action, SqlDbType.BigInt);
                SqlParameter pPOINDNO = new SqlParameter(CHStatus._PONO, SqlDbType.NVarChar);
                pActionid.Value = Actio;
                pPOINDNO.Value = IPNO;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, CHStatus.SP_ChangePoIndStatus, pActionid, pPOINDNO);

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
            return DS;


        }




        public DMCHStatus()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
