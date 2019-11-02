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
/// Summary description for DMUnitConversion
/// </summary>
public class DMUnitConversion : MayurInventory.Utility.Setting
{
    #region [Business Logic]

    public int InsertRecord(ref UnitConversion Entity_Call, out string StrError)
    {
        int iInsert = 0;
        StrError = string.Empty;

        try
        {
            SqlParameter pAction = new SqlParameter(UnitConversion._Action, SqlDbType.BigInt);
            SqlParameter pUnitId = new SqlParameter(UnitConversion._UnitId, SqlDbType.BigInt);
            SqlParameter pCreatedBy = new SqlParameter(UnitConversion._LoginId, SqlDbType.BigInt);
            SqlParameter pCreatedDate = new SqlParameter(UnitConversion._LoginDate, SqlDbType.DateTime);

            pAction.Value = 1;
            pUnitId.Value = Entity_Call.UnitId;
            pCreatedBy.Value = Entity_Call.LoginId;
            pCreatedDate.Value = Entity_Call.LoginDate;

            SqlParameter[] param = new SqlParameter[] { pAction, pUnitId, pCreatedBy, pCreatedDate };

            Open(CONNECTION_STRING);
            BeginTransaction();

            iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, UnitConversion.SP_UnitConversion, param);

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
            StrError = ex.Message;
        }

        finally
        {
            Close();
        }
        return iInsert;
    }

    public int UpdateRecord(ref UnitConversion Entity_Call, out string StrError)
    {
        int iUpdate = 0;
        StrError = string.Empty;

        try
        {
            SqlParameter pAction = new SqlParameter(UnitConversion._Action, SqlDbType.BigInt);
            SqlParameter pUnitConvId = new SqlParameter(UnitConversion._UnitConvId, SqlDbType.BigInt);
            SqlParameter pUnitId = new SqlParameter(UnitConversion._UnitId, SqlDbType.BigInt);
            SqlParameter pUpdatedBy = new SqlParameter(UnitConversion._LoginId, SqlDbType.BigInt);
            SqlParameter pUpdatedDate = new SqlParameter(UnitConversion._LoginDate, SqlDbType.DateTime);

            pAction.Value = 2;
            pUnitConvId.Value = Entity_Call.UnitConvId;
            pUnitId.Value = Entity_Call.UnitId;
            pUpdatedBy.Value = Entity_Call.LoginId;
            pUpdatedDate.Value = Entity_Call.LoginDate;

            SqlParameter[] param = new SqlParameter[] { pAction, pUnitConvId, pUnitId, pUpdatedBy, pUpdatedDate };

            Open(CONNECTION_STRING);
            BeginTransaction();

            iUpdate = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, UnitConversion.SP_UnitConversion, param);

            if (iUpdate > 0)
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
            StrError = ex.Message;
        }

        finally
        {
            Close();
        }
        return iUpdate;
    }

    public int DeleteRecord(ref UnitConversion Entity_Call, out string StrError)
    {
        int iUpdate = 0;
        StrError = string.Empty;

        try
        {
            SqlParameter pAction = new SqlParameter(UnitConversion._Action, SqlDbType.BigInt);
            SqlParameter pUnitConvId = new SqlParameter(UnitConversion._UnitConvId, SqlDbType.BigInt);
            SqlParameter pDeletedBy = new SqlParameter(UnitConversion._LoginId, SqlDbType.BigInt);
            SqlParameter pDeletedDate = new SqlParameter(UnitConversion._LoginDate, SqlDbType.DateTime);

            pAction.Value = 3;
            pUnitConvId.Value = Entity_Call.UnitConvId;
            pDeletedBy.Value = Entity_Call.LoginId;
            pDeletedDate.Value = Entity_Call.LoginDate;

            SqlParameter[] param = new SqlParameter[] { pAction, pUnitConvId, pDeletedBy, pDeletedDate };

            Open(CONNECTION_STRING);
            BeginTransaction();

            iUpdate = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, UnitConversion.SP_UnitConversion, param);

            if (iUpdate > 0)
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
            StrError = ex.Message;
        }

        finally
        {
            Close();
        }
        return iUpdate;
    }

    public int InsertRecordDtls(ref UnitConversion Entity_Call, out string StrError)
    {
        int iInsert = 0;
        StrError = string.Empty;

        try
        {
            SqlParameter pAction = new SqlParameter(UnitConversion._Action, SqlDbType.BigInt);
            SqlParameter pUnitConvId = new SqlParameter(UnitConversion._UnitConvId, SqlDbType.BigInt);
            SqlParameter pUnitId = new SqlParameter(UnitConversion._UnitId, SqlDbType.BigInt);
            SqlParameter pUnitFactor = new SqlParameter(UnitConversion._UnitFactor, SqlDbType.NVarChar);
            SqlParameter pQty = new SqlParameter(UnitConversion._Qty, SqlDbType.Decimal);

            pAction.Value = 4;
            pUnitConvId.Value = Entity_Call.UnitConvId;
            pUnitId.Value = Entity_Call.UnitId;
            pUnitFactor.Value = Entity_Call.UnitFactor;
            pQty.Value = Entity_Call.Qty;

            SqlParameter[] param = new SqlParameter[] { pAction, pUnitConvId,pUnitId, pUnitFactor, pQty};

            Open(CONNECTION_STRING);
            BeginTransaction();

            iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, UnitConversion.SP_UnitConversion, param);

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
            StrError = ex.Message;
        }

        finally
        {
            Close();
        }
        return iInsert;
    }

    public int UpdateRecordDtls(int Id, decimal qty,out string StrError)
    {
        int iInsert = 0;
        StrError = string.Empty;
        try
        {
            SqlParameter pAction = new SqlParameter(UnitConversion._Action, SqlDbType.BigInt);
            SqlParameter pUnitConvDtlsId = new SqlParameter(UnitConversion._UnitConvDtlsId, SqlDbType.BigInt);
            SqlParameter pQty = new SqlParameter(UnitConversion._Qty, SqlDbType.Decimal);
           
            pAction.Value = 9;
            pUnitConvDtlsId.Value = Id;
            pQty.Value = qty;

            SqlParameter[] param = new SqlParameter[] { pAction, pUnitConvDtlsId,pQty };

            Open(CONNECTION_STRING);
            BeginTransaction();

            iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, UnitConversion.SP_UnitConversion, param);

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
            StrError = ex.Message;
        }

        finally
        {
            Close();
        }
        return iInsert;
    }

    public DataSet GetRecordToEdit(int ID, out string StrError)
    {
        DataSet ds = new DataSet();
        StrError = string.Empty;

        try
        {
            SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
            SqlParameter pId = new SqlParameter(UnitConversion._UnitConvId, SqlDbType.BigInt);

            pAction.Value = 5;
            pId.Value = ID;

            Open(CONNECTION_STRING);

            ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, UnitConversion.SP_UnitConversion, pAction, pId);
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }
        return ds;
    }

    public DataSet BindCombo(out string StrError)
    {
        DataSet ds = new DataSet();
        StrError = string.Empty;

        try
        {
            SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);

            pAction.Value = 6;

            Open(CONNECTION_STRING);

            ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, UnitConversion.SP_UnitConversion, pAction);
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }
        return ds;
    }

    public DataSet GetUnit(string RepCondition, out string StrError)
    {
        DataSet ds = new DataSet();
        StrError = string.Empty;

        try
        {
            SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
            SqlParameter pCondition = new SqlParameter("@Condition", SqlDbType.NVarChar);

            pAction.Value = 7;
            pCondition.Value = RepCondition;

            Open(CONNECTION_STRING);

            ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, UnitConversion.SP_UnitConversion, pAction, pCondition);
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }

        return ds;
    }

    public DataSet GetUnitForDuplicate(int ID, out string StrError)
    {
        DataSet ds = new DataSet();
        StrError = string.Empty;

        try
        {
            SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
            SqlParameter pID = new SqlParameter(UnitConversion._UnitId, SqlDbType.BigInt);

            pAction.Value = 8;
            pID.Value = ID;

            Open(CONNECTION_STRING);

            ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, UnitConversion.SP_UnitConversion, pAction, pID);
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }
        return ds;
    }

    public string[] GetSuggestRecord(string preFixText)
    {
        List<string> SearchList = new List<string>();
        string ListItem = string.Empty;

        try
        {
            SqlParameter pAction = new SqlParameter(UnitConversion._Action, SqlDbType.BigInt);
            SqlParameter PrepCondition = new SqlParameter("@Condition", SqlDbType.NVarChar);

            pAction.Value = 7;
            PrepCondition.Value = preFixText;

            SqlParameter[] oparamcol = new SqlParameter[] { pAction, PrepCondition };

            Open(CONNECTION_STRING);
            SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, UnitConversion.SP_UnitConversion, oparamcol);

            if (dr != null && dr.HasRows == true)
            {
                while (dr.Read())
                {
                    ListItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString(),
                        dr[0].ToString());

                    SearchList.Add(ListItem);
                }

            }
            dr.Close();
        }

        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            Close();
        }

        return SearchList.ToArray();
    }

    public DataSet GetUnitConvrsndtlsId(ref UnitConversion Entity_Call, out string StrError)
    {
        DataSet ds = new DataSet();
        StrError = string.Empty;

        try
        {
            SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
            SqlParameter pUnitFactor = new SqlParameter(UnitConversion._UnitFactor, SqlDbType.NVarChar);
            SqlParameter pUnitId = new SqlParameter(UnitConversion._UnitId, SqlDbType.BigInt);
            SqlParameter pUnitConvId = new SqlParameter(UnitConversion._UnitConvId, SqlDbType.BigInt);
            SqlParameter pQty = new SqlParameter(UnitConversion._Qty, SqlDbType.Decimal);

            pAction.Value = 10;
            pUnitFactor.Value = Entity_Call.UnitFactor;
            pUnitId.Value = Entity_Call.UnitId;
            pUnitConvId.Value = Entity_Call.UnitConvId;
            pQty.Value = Entity_Call.Qty;
            SqlParameter[] param = new SqlParameter[] { pAction, pUnitFactor, pUnitId, pUnitConvId,pQty };

            Open(CONNECTION_STRING);

            ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, UnitConversion.SP_UnitConversion,param);
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }
        return ds;
    }

    #endregion
}
