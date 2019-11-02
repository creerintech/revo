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

public class DMSundryCreditorReport : Setting
{
    #region [Business Logic]

    //In Use**************
    public DataSet Getdetails(string LedgerId, string RepCondition, out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter MAction = new SqlParameter(SundryCreditorReport._Action, SqlDbType.BigInt);
            SqlParameter MRepCondition = new SqlParameter(SundryCreditorReport._RepCondition, SqlDbType.NVarChar);
            SqlParameter MLedgerId = new SqlParameter(SundryCreditorReport._LedgerID, SqlDbType.NVarChar);

            MAction.Value = 1;
            MRepCondition.Value = RepCondition;
            MLedgerId.Value = LedgerId;

            SqlParameter[] Param = new SqlParameter[] { MAction, MRepCondition, MLedgerId };

            Open(Setting.CONNECTION_STRING);
            Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundryCreditorReport.PRO_SUNDRY_CREDITOR_REPORT, Param);
        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;
    }

    //In Use**************
    public DataSet LedgerEntry(string LedgerId, int Cond, int Cond1, out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter MAction = new SqlParameter(SundryCreditorReport._Action, SqlDbType.BigInt);
            SqlParameter MLedgerId = new SqlParameter(SundryCreditorReport._LedgerID, SqlDbType.NVarChar);
            SqlParameter MRepCondition = new SqlParameter(SundryCreditorReport._RepCondition, SqlDbType.NVarChar);
            SqlParameter MVoucherId = new SqlParameter("@VoucherId", SqlDbType.BigInt);
            SqlParameter MVoucherId1 = new SqlParameter("@VoucherId1", SqlDbType.BigInt);

            MAction.Value = 4;
            MLedgerId.Value = LedgerId;
            MRepCondition.Value = Cond;
            MVoucherId.Value = Cond;
            MVoucherId1.Value = Cond1;

            SqlParameter[] Param = new SqlParameter[] { MAction, MLedgerId, MRepCondition, MVoucherId, MVoucherId1 };

            Open(Setting.CONNECTION_STRING);
            Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundryCreditorReport.PRO_SUNDRY_CREDITOR_REPORT, Param);
        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;
    }

    //In Use**************
    public DataSet FindOpeningBal(string RepCond, string StrCondition, out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter MAction = new SqlParameter(SundryCreditorReport._Action, SqlDbType.BigInt);
            SqlParameter MRepCond = new SqlParameter(SundryCreditorReport._LedgerID, SqlDbType.NVarChar);
            SqlParameter MRepCondition = new SqlParameter(SundryCreditorReport._RepCondition, SqlDbType.NVarChar);

            MAction.Value = 2;
            MRepCond.Value = RepCond;
            MRepCondition.Value = StrCondition;

            SqlParameter[] Param = new SqlParameter[] { MAction, MRepCond, MRepCondition };

            Open(Setting.CONNECTION_STRING);
            Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundryCreditorReport.PRO_SUNDRY_CREDITOR_REPORT, Param);
        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;
    }

    //In Use**************
    public DataSet DisplayMonthDetails(string LedgerId, string StrCondition, out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter MAction = new SqlParameter(SundryCreditorReport._Action, SqlDbType.BigInt);
            SqlParameter MLedgerId = new SqlParameter(SundryCreditorReport._LedgerID, SqlDbType.NVarChar);
            SqlParameter MRepCondition = new SqlParameter(SundryCreditorReport._RepCondition, SqlDbType.NVarChar);

            MAction.Value = 3;
            MLedgerId.Value = LedgerId;
            MRepCondition.Value = StrCondition;

            SqlParameter[] Param = new SqlParameter[] { MAction, MLedgerId, MRepCondition };

            Open(Setting.CONNECTION_STRING);
            Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, SundryCreditorReport.PRO_SUNDRY_CREDITOR_REPORT, Param);
        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;
    }

    #endregion
}