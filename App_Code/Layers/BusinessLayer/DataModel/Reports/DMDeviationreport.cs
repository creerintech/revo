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
using System.Collections.Generic;

/// <summary>
/// Summary description for DMPendingRequisition
/// </summary>
namespace MayurInventory.DataModel
{
    public class DMDeviationreport : Utility.Setting
    {
        public DataSet FillReportCombo(int COND,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.BigInt);

                pAction.Value = 1;
                pCOND.Value = COND;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_DeviationReport", pAction,pCOND);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetList(int StrCond, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pstrCond = new SqlParameter("@DevId", SqlDbType.BigInt);
              

                pAction.Value = 2;
                pstrCond.Value = StrCond;
               

                SqlParameter[] Param = new SqlParameter[] { pAction, pstrCond};

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, "SP_DeviationReport", Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DMDeviationreport()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
