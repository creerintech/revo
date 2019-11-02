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
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using MayurInventory.DALSQLHelper;

namespace MayurInventory.DataModel
{
public class DMCostCentre:Utility.Setting
{
    public int InsertRecord(ref CostCentre Entity_call, out string strError)
    {
        int iInsert = 0;
        strError = string.Empty;
        try 
        {
            SqlParameter pAction = new SqlParameter(CostCentre._Action, SqlDbType.BigInt);
            SqlParameter pLocation = new SqlParameter(CostCentre._Location,SqlDbType.NVarChar);
            SqlParameter pSiteId = new SqlParameter(CostCentre._SiteId, SqlDbType.BigInt);
            SqlParameter pTowerId = new SqlParameter(CostCentre._TowerId, SqlDbType.BigInt);

            SqlParameter pCreatedBy = new SqlParameter(CostCentre._UserId,SqlDbType.BigInt);
            SqlParameter PCreatedDate = new SqlParameter(CostCentre._LoginDate,SqlDbType.DateTime);
            SqlParameter pIsDeleted = new SqlParameter(CostCentre ._IsDeleted,SqlDbType.Bit);

            pAction.Value = 1;
            pLocation.Value = Entity_call.Location;
            pSiteId.Value = Entity_call.SiteId;
            pTowerId.Value = Entity_call.TowerId;

            pCreatedBy.Value = Entity_call.UserId;
            PCreatedDate.Value = Entity_call.LoginDate;
            pIsDeleted.Value = Entity_call.IsDeleted;
            
            SqlParameter[] param = new SqlParameter[] {pAction, pLocation,pSiteId,pTowerId, pCreatedBy, PCreatedDate,pIsDeleted};

            Open(CONNECTION_STRING);
            BeginTransaction();

            iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure,CostCentre.SP_CostCentre, param);

            if (iInsert > 0)
            {
                CommitTransaction();
            }
            else
            {
                RollBackTransaction();
            }
        }
 
        catch(Exception ex)
        {
            RollBackTransaction();
            strError = ex.Message;
        }

        finally
            {
                Close();
            }
        return iInsert;
    }

    public int UpdateRecord(ref CostCentre Entity_Call, out string StrError)
    {
        int iInsert = 0;
        StrError = string.Empty;
        try
        {
            SqlParameter pAction = new SqlParameter(CostCentre._Action,SqlDbType.BigInt);
            SqlParameter pStockLocationId = new SqlParameter(CostCentre._StockLocationID,SqlDbType.BigInt);
            SqlParameter pLocation = new SqlParameter(CostCentre._Location,SqlDbType.NVarChar);
            SqlParameter pSiteId = new SqlParameter(CostCentre._SiteId, SqlDbType.BigInt);
            SqlParameter pTowerId = new SqlParameter(CostCentre._TowerId, SqlDbType.BigInt);

            SqlParameter pCreatedBy = new SqlParameter(CostCentre._UserId,SqlDbType.BigInt);
            SqlParameter pCreatedDate = new SqlParameter(CostCentre._LoginDate,SqlDbType.DateTime);
            pAction.Value = 2;
            pStockLocationId.Value = Entity_Call.StockLocationID;
            pLocation.Value = Entity_Call.Location;
            pSiteId.Value = Entity_Call.SiteId;
            pTowerId.Value = Entity_Call.TowerId;

            pCreatedBy.Value = Entity_Call.UserId;
            pCreatedDate.Value = Entity_Call.LoginDate;

            SqlParameter [] param=new SqlParameter []{ pAction,pStockLocationId,pLocation,pSiteId,pTowerId,pCreatedBy,pCreatedDate};

            Open(CONNECTION_STRING);
            BeginTransaction();

            iInsert=SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,CostCentre.SP_CostCentre,param);
            if(iInsert > 0)
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
            StrError=ex.Message;
        }
        finally
        {
           Close();
        }
        return iInsert;
    }

    public int DeleteRecord(ref CostCentre EntityCall, out string StrError)
    {
        int iDelete = 0;
        StrError = string.Empty;
        try
        {
            SqlParameter pAction = new SqlParameter(CostCentre._Action, SqlDbType.BigInt);
            SqlParameter pStockLocationId = new SqlParameter(CostCentre._StockLocationID,SqlDbType.BigInt);
            SqlParameter pDeletedBy = new SqlParameter(CostCentre._UserId,SqlDbType.BigInt);
            SqlParameter pDeletedDate = new SqlParameter(CostCentre._LoginDate,SqlDbType.DateTime);
            SqlParameter pIsDeleted = new SqlParameter(CostCentre._IsDeleted,SqlDbType.Bit);

            pAction.Value = 3;
            pStockLocationId.Value = EntityCall.StockLocationID;
            pDeletedBy.Value = EntityCall.UserId;
            pDeletedDate.Value = EntityCall.LoginDate;
            pIsDeleted.Value = EntityCall.IsDeleted;

            SqlParameter[] param = new SqlParameter[] { pAction, pStockLocationId, pDeletedBy, pDeletedDate,pIsDeleted };

            Open(CONNECTION_STRING);
            BeginTransaction();

            iDelete = SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,CostCentre.SP_CostCentre,param);

            if (iDelete > 0)
            {
                CommitTransaction();
            }
            else
            {
                RollBackTransaction();
               
            }
           
        }
        catch(Exception ex)
        {
            RollBackTransaction();
            StrError = ex.Message;
        }
        finally
        {
            Close();
        }
        return iDelete;
    }

    public DataSet GetStockLOcationForEdit(int ID, out string strError)
    {
        strError = string.Empty;
        DataSet DS = new DataSet();

        try
        {
            SqlParameter pAction = new SqlParameter(CostCentre._Action,SqlDbType.BigInt);
            SqlParameter pStockLocationId = new SqlParameter(CostCentre._StockLocationID,SqlDbType.BigInt);

            pAction.Value = 4;
            pStockLocationId.Value = ID;

            Open(CONNECTION_STRING);
            DS = SQLHelper.GetDataSetDoubleParm(_Connection,_Transaction,CommandType.StoredProcedure,CostCentre.SP_CostCentre,pAction,pStockLocationId);

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

    public DataSet GetStockLocation(string RepCondition, out string StrError)
    {
        StrError = string.Empty;

        DataSet DS = new DataSet();
        try
        {
            SqlParameter pAction= new SqlParameter(CostCentre._Action, SqlDbType.BigInt);
            SqlParameter pRepCondition = new SqlParameter(CostCentre._StrCondition,SqlDbType.NVarChar);

            pAction.Value = 5;
            pRepCondition.Value = RepCondition;

            Open(CONNECTION_STRING);
            DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, CostCentre.SP_CostCentre, pAction, pRepCondition);
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }
        finally 
        {
            Close();
        }
        return DS;
    }

    public DataSet ChkDuplicate(string Name, out string StrError)
    {
        StrError = string.Empty;
        DataSet DS = new DataSet();
        try 
        {
            SqlParameter pAction = new SqlParameter(CostCentre._Action,SqlDbType.BigInt);

            SqlParameter PRepCondition = new SqlParameter(CostCentre._StrCondition,SqlDbType.NVarChar);

            pAction.Value = 6;
            PRepCondition.Value = Name;
            Open(CONNECTION_STRING);
            DS = SQLHelper.GetDataSetDoubleParm(_Connection,_Transaction,CommandType.StoredProcedure,CostCentre.SP_CostCentre ,
                pAction,PRepCondition);
           
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
            {

            }
        return DS;
    }

    public string[] GetSuggestRecord(string prefixText)
    {
        List<string> SearchList = new List<string>();
        string ListItem = string.Empty;
        try
        {
            //---For Checking Of Execution Of Procedure
            SqlParameter pAction = new SqlParameter(CostCentre._Action, SqlDbType.BigInt);
            SqlParameter pRepCondition = new SqlParameter(CostCentre._StrCondition, SqlDbType.NVarChar);

            pAction.Value = 5;
            pRepCondition.Value = prefixText;

            SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition };

            Open(CONNECTION_STRING);
            SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, CostCentre.SP_CostCentre, oparamcol);
            if (dr != null && dr.HasRows == true)
            {
                while (dr.Read())
                {
                    ListItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString());
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

    public DataSet BindCombo(out string StrError)
    {
        StrError = string.Empty;
        DataSet DS = new DataSet();
        try
        {
            SqlParameter pAction = new SqlParameter(CostCentre._Action, SqlDbType.BigInt);

            pAction.Value = 7;
            Open(CONNECTION_STRING);

            DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, CostCentre.SP_CostCentre, pAction);
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }
        finally
        {
            Close();
        }
        return DS;
    }

    public DMCostCentre()
    {

    }
}
}