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
public class DMStockLocation:Utility.Setting
{
    public int InsertRecord(ref StockLocation Entity_call, out string strError)
    {
        int iInsert = 0;
        strError = string.Empty;
        try 
        {
            SqlParameter pAction = new SqlParameter(StockLocation._Action, SqlDbType.BigInt);
            SqlParameter pLocation = new SqlParameter(StockLocation._Location,SqlDbType.NVarChar);
            SqlParameter pSiteId = new SqlParameter(StockLocation._SiteId, SqlDbType.BigInt);
            SqlParameter pTowerId = new SqlParameter(StockLocation._TowerId, SqlDbType.BigInt);
            SqlParameter pCompanyId = new SqlParameter(StockLocation._CompanyId, SqlDbType.BigInt);
            SqlParameter pSiteAddr = new SqlParameter(StockLocation._SiteAddr, SqlDbType.NVarChar);
            SqlParameter pabbreviation = new SqlParameter(StockLocation._abbreviation, SqlDbType.NVarChar);
            SqlParameter pCreatedBy = new SqlParameter(StockLocation._UserId,SqlDbType.BigInt);
            SqlParameter PCreatedDate = new SqlParameter(StockLocation._LoginDate,SqlDbType.DateTime);
            SqlParameter pIsDeleted = new SqlParameter(StockLocation ._IsDeleted,SqlDbType.Bit);
            SqlParameter pIsCental = new SqlParameter(StockLocation._IsCental, SqlDbType.Bit);

            pAction.Value = 1;
            pLocation.Value = Entity_call.Location;
            pSiteId.Value = Entity_call.SiteId;
            pTowerId.Value = Entity_call.TowerId;
            pCompanyId.Value = Entity_call.CompanyId;
            pSiteAddr.Value = Entity_call.SiteAddr;
            pabbreviation.Value = Entity_call.abbreviation;
            pCreatedBy.Value = Entity_call.UserId;
            PCreatedDate.Value = Entity_call.LoginDate;
            pIsDeleted.Value = Entity_call.IsDeleted;
            pIsCental.Value = Entity_call.IsCental;

            SqlParameter[] param = new SqlParameter[] { pAction, pLocation, pSiteId, pTowerId, pCompanyId, pSiteAddr, pCreatedBy, PCreatedDate, pIsDeleted, pIsCental, pabbreviation };

            Open(CONNECTION_STRING);
            BeginTransaction();

            iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure,StockLocation.SP_StockLocation, param);

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

    public int UpdateRecord(ref StockLocation Entity_Call, out string StrError)
    {
        int iInsert = 0;
        StrError = string.Empty;
        try
        {
            SqlParameter pAction = new SqlParameter(StockLocation._Action,SqlDbType.BigInt);
            SqlParameter pStockLocationId = new SqlParameter(StockLocation._StockLocationID,SqlDbType.BigInt);
            SqlParameter pLocation = new SqlParameter(StockLocation._Location,SqlDbType.NVarChar);
            SqlParameter pSiteId = new SqlParameter(StockLocation._SiteId, SqlDbType.BigInt);
            SqlParameter pTowerId = new SqlParameter(StockLocation._TowerId, SqlDbType.BigInt);
            SqlParameter pCompanyId = new SqlParameter(StockLocation._CompanyId, SqlDbType.BigInt);
            SqlParameter pSiteAddr = new SqlParameter(StockLocation._SiteAddr, SqlDbType.NVarChar);
            SqlParameter pabbreviation = new SqlParameter(StockLocation._abbreviation, SqlDbType.NVarChar);
            SqlParameter pCreatedBy = new SqlParameter(StockLocation._UserId,SqlDbType.BigInt);
            SqlParameter pCreatedDate = new SqlParameter(StockLocation._LoginDate,SqlDbType.DateTime);
            SqlParameter pIsCental = new SqlParameter(StockLocation._IsCental, SqlDbType.Bit);
           
            pAction.Value = 2;
            pStockLocationId.Value = Entity_Call.StockLocationID;
            pLocation.Value = Entity_Call.Location;
            pSiteId.Value = Entity_Call.SiteId;
            pTowerId.Value = Entity_Call.TowerId;
            pCompanyId.Value = Entity_Call.CompanyId;
            pSiteAddr.Value = Entity_Call.SiteAddr;
            pabbreviation.Value = Entity_Call.abbreviation;
            pCreatedBy.Value = Entity_Call.UserId;
            pCreatedDate.Value = Entity_Call.LoginDate;
            pIsCental.Value = Entity_Call.IsCental;

            SqlParameter[] param = new SqlParameter[] { pAction, pStockLocationId, pLocation, pCompanyId, pSiteAddr, pSiteId, pTowerId, pCreatedBy, pCreatedDate, pIsCental, pabbreviation };

            Open(CONNECTION_STRING);
            BeginTransaction();

            iInsert=SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,StockLocation.SP_StockLocation,param);
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

    public int DeleteRecord(ref StockLocation EntityCall, out string StrError)
    {
        int iDelete = 0;
        StrError = string.Empty;
        try
        {
            SqlParameter pAction = new SqlParameter(StockLocation._Action, SqlDbType.BigInt);
            SqlParameter pStockLocationId = new SqlParameter(StockLocation._StockLocationID,SqlDbType.BigInt);
            SqlParameter pDeletedBy = new SqlParameter(StockLocation._UserId,SqlDbType.BigInt);
            SqlParameter pDeletedDate = new SqlParameter(StockLocation._LoginDate,SqlDbType.DateTime);
            SqlParameter pIsDeleted = new SqlParameter(StockLocation._IsDeleted,SqlDbType.Bit);

            pAction.Value = 3;
            pStockLocationId.Value = EntityCall.StockLocationID;
            pDeletedBy.Value = EntityCall.UserId;
            pDeletedDate.Value = EntityCall.LoginDate;
            pIsDeleted.Value = EntityCall.IsDeleted;

            SqlParameter[] param = new SqlParameter[] { pAction, pStockLocationId, pDeletedBy, pDeletedDate,pIsDeleted };

            Open(CONNECTION_STRING);
            BeginTransaction();

            iDelete = SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,StockLocation.SP_StockLocation,param);

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

    public int InsertDetailsRecord(ref StockLocation Entity_Call, out string strError)
    {
        int iInsert = 0;
        strError = string.Empty;
        try
        {
            SqlParameter pAction = new SqlParameter(StockLocation._Action, SqlDbType.BigInt);
            SqlParameter pStockLocationID = new SqlParameter(StockLocation._StockLocationID, SqlDbType.BigInt);
            SqlParameter pEmployeeId = new SqlParameter(StockLocation._EmployeeId, SqlDbType.BigInt);
            SqlParameter pCpersonName = new SqlParameter(StockLocation._CpersonName, SqlDbType.NVarChar);
            SqlParameter pContactNo = new SqlParameter(StockLocation._ContactNo, SqlDbType.NVarChar);
            SqlParameter pMailId = new SqlParameter(StockLocation._MailId, SqlDbType.NVarChar);
            SqlParameter pPersonAddress = new SqlParameter(StockLocation._PersonAddress, SqlDbType.NVarChar);

            pAction.Value = 7;
            pStockLocationID.Value = Entity_Call.StockLocationID;
            pEmployeeId.Value = Entity_Call.EmployeeId;
            pCpersonName.Value = Entity_Call.CpersonName;
            pContactNo.Value = Entity_Call.ContactNo;
            pMailId.Value = Entity_Call.MailId;
            pPersonAddress.Value = Entity_Call.PersonAddress;

            SqlParameter[] Param = new SqlParameter[] { pAction, pStockLocationID, pEmployeeId, pCpersonName, pContactNo, pMailId, pPersonAddress };

            Open(CONNECTION_STRING);
            BeginTransaction();

            iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, StockLocation.SP_StockLocation, Param);

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
            strError = ex.Message;
        }
        finally
        {
            Close();
        }
        return iInsert;
    }

    public DataSet GetStockLOcationForEdit(int ID, out string strError)
    {
        strError = string.Empty;
        DataSet DS = new DataSet();

        try
        {
            SqlParameter pAction = new SqlParameter(StockLocation._Action,SqlDbType.BigInt);
            SqlParameter pStockLocationId = new SqlParameter(StockLocation._StockLocationID,SqlDbType.BigInt);

            pAction.Value = 4;
            pStockLocationId.Value = ID;

            Open(CONNECTION_STRING);
            DS = SQLHelper.GetDataSetDoubleParm(_Connection,_Transaction,CommandType.StoredProcedure,StockLocation.SP_StockLocation,pAction,pStockLocationId);

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
            SqlParameter pAction = new SqlParameter(StockLocation._Action, SqlDbType.BigInt);
            SqlParameter pRepCondition = new SqlParameter(StockLocation._StrCondition, SqlDbType.NVarChar);


            pAction.Value = 5;
            pRepCondition.Value = RepCondition;

            Open(CONNECTION_STRING);
            DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockLocation.SP_StockLocation, pAction, pRepCondition);


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
            SqlParameter pAction = new SqlParameter(StockLocation._Action,SqlDbType.BigInt);

            SqlParameter PRepCondition = new SqlParameter(StockLocation._StrCondition,SqlDbType.NVarChar);

            pAction.Value = 6;
            PRepCondition.Value = Name;
            Open(CONNECTION_STRING);
            DS = SQLHelper.GetDataSetDoubleParm(_Connection,_Transaction,CommandType.StoredProcedure,StockLocation.SP_StockLocation ,
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
            SqlParameter pAction = new SqlParameter(StockLocation._Action, SqlDbType.BigInt);
            SqlParameter pRepCondition = new SqlParameter(StockLocation._StrCondition, SqlDbType.NVarChar);

            pAction.Value = 5;
            pRepCondition.Value = prefixText;

            SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition };

            Open(CONNECTION_STRING);
            SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, StockLocation.SP_StockLocation, oparamcol);
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
            SqlParameter pAction = new SqlParameter(StockLocation._Action, SqlDbType.BigInt);

            pAction.Value = 8;
            Open(CONNECTION_STRING);

            DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockLocation.SP_StockLocation, pAction);
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

    public DMStockLocation()
    {

    }
}
}