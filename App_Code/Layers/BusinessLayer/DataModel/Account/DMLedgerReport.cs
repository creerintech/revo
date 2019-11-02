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

using System.Collections.Generic;
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using MayurInventory.DALSQLHelper;

namespace MayurInventory.DataModel
{
    public class DMLedgerReport : Setting
    {
        #region [Business Logic]

        public DataSet BindCombo(out string StrError)
        {
            DataSet ds = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);

                pAction.Value = 1;

                Open(CONNECTION_STRING);

                ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, "PRO_MISAccountLedgerReport", pAction);
            }
            catch (Exception ex)
            {
                StrError = ex.Message;
            }
            finally
            {
                Close();
            }
            return ds;
        }

        public DataSet GetdetailsLedgerIdOther1n2(int LedgerId, string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(LedgerMaster._Action, SqlDbType.BigInt);
                SqlParameter MRepCondition = new SqlParameter(LedgerMaster._RepCondition, SqlDbType.NVarChar);
                SqlParameter MLedgerId = new SqlParameter("@TempLedgerID", SqlDbType.NVarChar);

                MAction.Value = 5;
                MRepCondition.Value = RepCondition;
                MLedgerId.Value = LedgerId;

                SqlParameter[] Param = new SqlParameter[] { MAction, MRepCondition, MLedgerId };

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, LedgerMaster.PRO_MISAccountLedgerReport, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetdetailsLedgerIdIS1or2(int LedgerId, string ToDate, string FromDate, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(LedgerMaster._Action, SqlDbType.BigInt);
                SqlParameter MToDate = new SqlParameter(LedgerMaster._ToDate, SqlDbType.NVarChar);
                SqlParameter MFromDate = new SqlParameter(LedgerMaster._FromDate, SqlDbType.NVarChar);


                if (LedgerId == 1)
                {
                    MAction.Value = 3;
                }
                else
                {
                    MAction.Value = 4;
                }
                MToDate.Value = ToDate;
                MFromDate.Value = FromDate;

                SqlParameter[] Param = new SqlParameter[] { MAction, MToDate, MFromDate };

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, LedgerMaster.PRO_MISAccountLedgerReport, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet FindLedgerGroupId(int LedgerId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(LedgerMaster._Action, SqlDbType.BigInt);
                SqlParameter MLedgerId = new SqlParameter(LedgerMaster._LedgerID, SqlDbType.BigInt);

                MAction.Value = 7;
                MLedgerId.Value = LedgerId;

                SqlParameter[] Param = new SqlParameter[] { MAction, MLedgerId };

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, LedgerMaster.PRO_MISAccountLedgerReport, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet FindOpeningBal(string RepCond, string StrCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(LedgerMaster._Action, SqlDbType.BigInt);
                SqlParameter MRepCond = new SqlParameter("@RepCond", SqlDbType.NVarChar);
                SqlParameter MRepCondition = new SqlParameter(LedgerMaster._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 6;
                MRepCond.Value = RepCond;
                MRepCondition.Value = StrCondition;

                SqlParameter[] Param = new SqlParameter[] { MAction, MRepCond, MRepCondition };

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, LedgerMaster.PRO_MISAccountLedgerReport, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet DisplayMonthDetails(int LedgerId, string StrCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(LedgerMaster._Action, SqlDbType.BigInt);
                SqlParameter MLedgerId = new SqlParameter("@TempLedgerID", SqlDbType.BigInt);
                SqlParameter MRepCondition = new SqlParameter(LedgerMaster._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 8;
                MLedgerId.Value = LedgerId;
                MRepCondition.Value = StrCondition;

                SqlParameter[] Param = new SqlParameter[] { MAction, MLedgerId, MRepCondition };

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, LedgerMaster.PRO_MISAccountLedgerReport, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetGroupId(int LedgerId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(LedgerMaster._Action, SqlDbType.BigInt);
                SqlParameter MLedgerId = new SqlParameter(LedgerMaster._LedgerID, SqlDbType.BigInt);

                MAction.Value = 9;
                MLedgerId.Value = LedgerId;

                SqlParameter[] Param = new SqlParameter[] { MAction, MLedgerId };

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, LedgerMaster.PRO_MISAccountLedgerReport, MAction, MLedgerId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetLedgerForParticularMonth(string StrCondLedger, string StrCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(LedgerMaster._Action, SqlDbType.BigInt);
                SqlParameter MStrCondledger = new SqlParameter("@RepCond", SqlDbType.NVarChar);
                SqlParameter MSrCondition = new SqlParameter(LedgerMaster._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 10;
                MStrCondledger.Value = StrCondLedger;
                MSrCondition.Value = StrCondition;

                SqlParameter[] Param = new SqlParameter[] { MAction, MStrCondledger, MSrCondition };

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, LedgerMaster.PRO_MISAccountLedgerReport_I, Param);
            }
            catch (Exception ex)
            {
                StrError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetLedgerVoucherForParticularMonth1n2(string StrCondLedger, string StrCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(LedgerMaster._Action, SqlDbType.BigInt);
                SqlParameter MStrCondLedger = new SqlParameter("@RepCond", SqlDbType.NVarChar);
                SqlParameter MStrCondition = new SqlParameter(LedgerMaster._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 11;
                MStrCondLedger.Value = StrCondLedger;
                MStrCondition.Value = StrCondition;

                SqlParameter[] Param = new SqlParameter[] { MAction, MStrCondLedger, MStrCondition };

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, LedgerMaster.PRO_MISAccountLedgerReport_I, Param);
            }
            catch (Exception ex)
            {
                StrError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        #endregion

        public DMLedgerReport()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}