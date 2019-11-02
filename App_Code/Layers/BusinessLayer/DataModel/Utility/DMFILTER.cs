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
using MayurInventory.DataModel;
using MayurInventory.EntityClass;
using MayurInventory.DALSQLHelper;
using MayurInventory.DB;


namespace MayurInventory.DataModel
{
    public class DMFILTER : Utility.Setting
    {
        public DMFILTER()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet FillReportComboCategoryWise(int Action,int LOCID,String CAT, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCOND = new SqlParameter("@LOCID", SqlDbType.BigInt);
                SqlParameter pCAT = new SqlParameter("@CAT", SqlDbType.NVarChar);

                pAction.Value = Action;
                pCOND.Value = LOCID;
                pCAT.Value = CAT;
                SqlParameter[] param = new SqlParameter[] {pAction,pCOND,pCAT};
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, "SP_FILTERDATA", param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet FillReportComboCategorySubCategoryWise(int Action, int LOCID, String CAT,String SUBCAT, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCOND = new SqlParameter("@LOCID", SqlDbType.BigInt);
                SqlParameter pCAT = new SqlParameter("@CAT", SqlDbType.NVarChar);
                SqlParameter pSUBCAT = new SqlParameter("@SUBCAT", SqlDbType.NVarChar);

                pAction.Value = Action;
                pCOND.Value = LOCID;
                pCAT.Value = CAT;
                pSUBCAT.Value = SUBCAT;
                SqlParameter[] param = new SqlParameter[] { pAction, pCOND, pCAT ,pSUBCAT};
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, "SP_FILTERDATA", param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
    }
}