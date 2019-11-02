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

using MayurInventory.Utility;
using MayurInventory.DataModel;
using MayurInventory.EntityClass;
using MayurInventory.DALSQLHelper;
using MayurInventory.DB;


/// <summary>
/// Summary description for ErrorLogger
/// </summary>
/// 
namespace MayurInventory.DataModel
{
    public class ErrorLogger : Utility.Setting
    {
        ErrorLog EntError = new ErrorLog();

        public ErrorLogger()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void ErrorInfo(Exception ex)
        {
            try
            {
                EntError.FunctionName = ex.TargetSite.ToString();
                EntError.Messege = ex.Message;
                EntError.Source = ex.Source;
                EntError.StackTrace = ex.StackTrace;
                EntError.GeneratedDate = DateTime.Now;

                SqlParameter pAction = new SqlParameter(ErrorLog._Action, SqlDbType.BigInt);
                SqlParameter pFunctionName = new SqlParameter(ErrorLog._FunctionName, SqlDbType.NVarChar);
                SqlParameter pMessege = new SqlParameter(ErrorLog._Message, SqlDbType.NVarChar);
                SqlParameter pStackTrace = new SqlParameter(ErrorLog._StackTrace, SqlDbType.NVarChar);
                SqlParameter pSource = new SqlParameter(ErrorLog._Source, SqlDbType.NVarChar);
                SqlParameter pGeneratedDate = new SqlParameter(ErrorLog._GeneratedDate, SqlDbType.DateTime);

                pAction.Value = 1;
                pFunctionName.Value = ex.TargetSite.ToString();
                pMessege.Value = ex.Message;
                pSource.Value = ex.Source;
                pStackTrace.Value = ex.StackTrace;
                pGeneratedDate.Value = DateTime.Now;

                SqlParameter[] Param = new SqlParameter[] { pAction, pFunctionName, pMessege, pSource, pStackTrace, pGeneratedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                int iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ErrorLog.SP_ErrorLogger, Param);

                if (iInsert > 0)
                {
                    CommitTransaction();
                }
                else
                {
                    RollBackTransaction();
                }
            }
            catch (Exception exe)
            {
                RollBackTransaction();
                //ErrorLogger Erlog = new ErrorLogger();
                //Erlog.ErrorInfo(exe);
                //strError = ex.Message;
            }
            finally
            {
                Close();
            }

        }
    }
}