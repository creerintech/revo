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
using MayurInventory.EntityClass;
using MayurInventory.DALSQLHelper;
using MayurInventory.DB;
using MayurInventory.Utility;
using System.Collections.Generic;

/// <summary>
/// Summary description for DMBrandMaster
/// </summary>
/// 

    public class DMSundaryReport : Setting
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

        public DataSet Getdetails(string LedgerId, string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(SundaryReport._Action, SqlDbType.BigInt);
                SqlParameter MRepCondition = new SqlParameter(SundaryReport._RepCondition, SqlDbType.NVarChar);
                SqlParameter MLedgerId = new SqlParameter(SundaryReport._LedgerID, SqlDbType.NVarChar);

                MAction.Value = 1;
                MRepCondition.Value = RepCondition;
                MLedgerId.Value = LedgerId;

                SqlParameter[] Param = new SqlParameter[] { MAction, MRepCondition, MLedgerId };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundaryReport.PRO_Sundary_Report, Param);
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
                SqlParameter MAction = new SqlParameter(SundaryReport._Action, SqlDbType.BigInt);
                SqlParameter MToDate = new SqlParameter(SundaryReport._ToDate, SqlDbType.NVarChar);
                SqlParameter MFromDate = new SqlParameter(SundaryReport._FromDate, SqlDbType.NVarChar);


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

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundaryReport.PRO_Sundary_Report, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet LedgerEntry(string LedgerId, int Cond, int Cond1, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(SundaryReport._Action, SqlDbType.BigInt);
                SqlParameter MLedgerId = new SqlParameter(SundaryReport._LedgerID, SqlDbType.NVarChar);
                SqlParameter MRepCondition = new SqlParameter(SundaryReport._RepCondition, SqlDbType.NVarChar);
                SqlParameter MVoucherId = new SqlParameter("@VoucherId", SqlDbType.BigInt);
                SqlParameter MVoucherId1 = new SqlParameter("@VoucherId1", SqlDbType.BigInt);

                MAction.Value = 4;
                MLedgerId.Value = LedgerId;
                MRepCondition.Value = Cond;
                MVoucherId.Value = Cond;
                MVoucherId1.Value = Cond1;

                SqlParameter[] Param = new SqlParameter[] { MAction, MLedgerId, MRepCondition, MVoucherId, MVoucherId1 };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundaryReport.PRO_Sundary_Report, Param);
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
                SqlParameter MAction = new SqlParameter(SundaryReport._Action, SqlDbType.BigInt);
                SqlParameter MRepCond = new SqlParameter(SundaryReport._LedgerID, SqlDbType.NVarChar);
                SqlParameter MRepCondition = new SqlParameter(SundaryReport._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 2;
                MRepCond.Value = RepCond;
                MRepCondition.Value = StrCondition;

                SqlParameter[] Param = new SqlParameter[] { MAction, MRepCond, MRepCondition };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundaryReport.PRO_Sundary_Report, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet DisplayMonthDetails(string LedgerId, string StrCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(SundaryReport._Action, SqlDbType.BigInt);
                SqlParameter MLedgerId = new SqlParameter(SundaryReport._LedgerID, SqlDbType.NVarChar);
                SqlParameter MRepCondition = new SqlParameter(SundaryReport._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 3;
                MLedgerId.Value = LedgerId;
                MRepCondition.Value = StrCondition;

                SqlParameter[] Param = new SqlParameter[] { MAction, MLedgerId, MRepCondition };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundaryReport.PRO_Sundary_Report, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet FindOpeningBalDebtor(string RepCond, string StrCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(SundaryReport._Action, SqlDbType.BigInt);
                SqlParameter MRepCond = new SqlParameter(SundaryReport._LedgerID, SqlDbType.NVarChar);
                SqlParameter MRepCondition = new SqlParameter(SundaryReport._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 5;
                MRepCond.Value = RepCond;
                MRepCondition.Value = StrCondition;

                SqlParameter[] Param = new SqlParameter[] { MAction, MRepCond, MRepCondition };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundaryReport.PRO_Sundary_Report, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet DisplayMonthDetailsDebitor(string LedgerId, string StrCondition, string StrCondition1, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(SundaryReport._Action, SqlDbType.BigInt);
                SqlParameter MLedgerId = new SqlParameter(SundaryReport._LedgerID, SqlDbType.NVarChar);
                SqlParameter MRepCondition = new SqlParameter(SundaryReport._RepCondition, SqlDbType.NVarChar);
                SqlParameter MTempLedgerID = new SqlParameter("@TempLedgerID", SqlDbType.NVarChar);

                MAction.Value = 6;
                MLedgerId.Value = LedgerId;
                MRepCondition.Value = StrCondition;
                MTempLedgerID.Value = StrCondition1;

                SqlParameter[] Param = new SqlParameter[] { MAction, MLedgerId, MRepCondition, MTempLedgerID };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundaryReport.PRO_Sundary_Report, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet LedgerEntryDebitor(string LedgerId, int Cond, int Cond1, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(SundaryReport._Action, SqlDbType.BigInt);
                SqlParameter MLedgerId = new SqlParameter(SundaryReport._LedgerID, SqlDbType.NVarChar);
                SqlParameter MRepCondition = new SqlParameter(SundaryReport._RepCondition, SqlDbType.NVarChar);
                SqlParameter MVoucherId = new SqlParameter("@VoucherId", SqlDbType.BigInt);
                SqlParameter MVoucherId1 = new SqlParameter("@VoucherId1", SqlDbType.BigInt);

                MAction.Value = 7;
                MLedgerId.Value = LedgerId;
                MRepCondition.Value = Cond;
                MVoucherId.Value = Cond;
                MVoucherId1.Value = Cond1;

                SqlParameter[] Param = new SqlParameter[] { MAction, MLedgerId, MRepCondition, MVoucherId, MVoucherId1 };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundaryReport.PRO_Sundary_Report, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        public DataSet GetdetailsD(string LedgerId, string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(SundaryReport._Action, SqlDbType.BigInt);
                SqlParameter MRepCondition = new SqlParameter(SundaryReport._RepCondition, SqlDbType.NVarChar);
                SqlParameter MLedgerId = new SqlParameter(SundaryReport._LedgerID, SqlDbType.NVarChar);

                MAction.Value = 8;
                MRepCondition.Value = RepCondition;
                MLedgerId.Value = LedgerId;

                SqlParameter[] Param = new SqlParameter[] { MAction, MRepCondition, MLedgerId };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundaryReport.PRO_Sundary_Report, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        #endregion

        public DMSundaryReport()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

