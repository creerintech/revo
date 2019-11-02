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

/// <summary>
/// Summary description for DMMayurInventory_Reports
/// </summary>
/// 
namespace MayurInventory.DataModel
{
    public class DMMayurInventory_Reports : Utility.Setting
    {

        public DataSet FillReportCombo(int COND,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MayurInventory_Reports._Action, SqlDbType.BigInt);
                SqlParameter pCOND = new SqlParameter("@UserId", SqlDbType.BigInt);

                pAction.Value = 1;
                pCOND.Value = COND;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MayurInventory_Reports.SP_MI_REPORTS_PART_I, pAction,pCOND);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet StockInHand(string FromDate, string ToDate, string StrCond,int pos, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MayurInventory_Reports._Action, SqlDbType.BigInt);
                SqlParameter pstrCond = new SqlParameter(MayurInventory_Reports._strCond, SqlDbType.NVarChar);
                SqlParameter pFromDate = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                SqlParameter pToDate = new SqlParameter("@ToDate", SqlDbType.NVarChar);

                if (pos == 0)
                {
                    pAction.Value = 2;
                }
                else
                {
                    pAction.Value = 7;
                }
                pstrCond.Value = StrCond;
                pFromDate.Value = FromDate;
                pToDate.Value = ToDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pstrCond, pFromDate,pToDate };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, MayurInventory_Reports.SP_MI_REPORTS_PART_I, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet StockInHand1(string FromDate, string ToDate, string StrCond, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MayurInventory_Reports._Action, SqlDbType.BigInt);
                SqlParameter pstrCond = new SqlParameter(MayurInventory_Reports._strCond, SqlDbType.NVarChar);
                SqlParameter pFromDate = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                SqlParameter pToDate = new SqlParameter("@ToDate", SqlDbType.NVarChar);

                pAction.Value = 6;
                pstrCond.Value = StrCond;
                pFromDate.Value = FromDate;
                pToDate.Value = ToDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pstrCond, pFromDate, pToDate };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, MayurInventory_Reports.SP_MI_REPORTS_PART_I, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }


        public DataSet StockInHand_WithDeviation(string FromDate, string ToDate, string StrCond, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MayurInventory_Reports._Action, SqlDbType.BigInt);
                SqlParameter pstrCond = new SqlParameter(MayurInventory_Reports._strCond, SqlDbType.NVarChar);
                SqlParameter pFromDate = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                SqlParameter pToDate = new SqlParameter("@ToDate", SqlDbType.NVarChar);

                pAction.Value = 3;
                pstrCond.Value = StrCond;
                pFromDate.Value = FromDate;
                pToDate.Value = ToDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pstrCond, pFromDate, pToDate };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, MayurInventory_Reports.SP_MI_REPORTS_PART_I, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet ConsumptionReport(string FromDate, string ToDate, string StrCond, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MayurInventory_Reports._Action, SqlDbType.BigInt);
                SqlParameter pstrCond = new SqlParameter(MayurInventory_Reports._strCond, SqlDbType.NVarChar);
                SqlParameter pFromDate = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                SqlParameter pToDate = new SqlParameter("@ToDate", SqlDbType.NVarChar);

                pAction.Value = 4;
                pstrCond.Value = StrCond;
                pFromDate.Value = FromDate;
                pToDate.Value = ToDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pstrCond, pFromDate, pToDate };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, MayurInventory_Reports.SP_MI_REPORTS_PART_I, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet MonthReportReport(string StrCond, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MayurInventory_Reports._Action, SqlDbType.BigInt);
                SqlParameter pstrCond = new SqlParameter(MayurInventory_Reports._strCond, SqlDbType.NVarChar);

                pAction.Value = 5;
                pstrCond.Value = StrCond;

                SqlParameter[] Param = new SqlParameter[] { pAction, pstrCond };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, MayurInventory_Reports.SP_MI_REPORTS_PART_I, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }  



        public DMMayurInventory_Reports()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}