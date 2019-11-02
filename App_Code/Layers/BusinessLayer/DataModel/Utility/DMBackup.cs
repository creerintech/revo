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
/// Summary description for DMBackup
/// </summary>
namespace MayurInventory.DataModel
{
    public class DMBackup : Utility.Setting
    {
        public DataSet CreateBU(string destdir, string DateCondition, out string strError)
        {
            DataSet DS = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(Backup._Action, SqlDbType.BigInt);
                SqlParameter pDateCondition = new SqlParameter(Backup._DateCondition, SqlDbType.NVarChar);
                SqlParameter pDestDir = new SqlParameter(Backup._DestDir, SqlDbType.NVarChar);

                pAction.Value = 1;
                pDateCondition.Value = DateCondition;
                pDestDir.Value=destdir;

                SqlParameter[] param = new SqlParameter[] { pAction, pDateCondition, pDestDir };

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, Backup.SP_Backup, param);
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

        public DMBackup()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
