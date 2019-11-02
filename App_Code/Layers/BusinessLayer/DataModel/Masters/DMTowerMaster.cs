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
    public class DMTowerMaster : Utility.Setting
    {
        public int InsertRecord(ref TowerMaster Entity_call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TowerMaster._Action, SqlDbType.BigInt);
                SqlParameter pTowerName = new SqlParameter(TowerMaster._TowerName, SqlDbType.NVarChar);

                SqlParameter pCreatedBy = new SqlParameter(TowerMaster._UserId, SqlDbType.BigInt);
                SqlParameter PCreatedDate = new SqlParameter(TowerMaster._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(TowerMaster._IsDeleted, SqlDbType.Bit);

                pAction.Value = 1;
                pTowerName.Value = Entity_call.TowerName;

                pCreatedBy.Value = Entity_call.UserId;
                PCreatedDate.Value = Entity_call.LoginDate;
                pIsDeleted.Value = Entity_call.IsDeleted;

                SqlParameter[] param = new SqlParameter[] { pAction, pTowerName, pCreatedBy, PCreatedDate, pIsDeleted };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TowerMaster.SP_TowerMaster, param);

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

        public int UpdateRecord(ref TowerMaster Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TowerMaster._Action, SqlDbType.BigInt);
                SqlParameter pTowerID = new SqlParameter(TowerMaster._TowerID, SqlDbType.BigInt);
                SqlParameter pTowerName = new SqlParameter(TowerMaster._TowerName, SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(TowerMaster._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(TowerMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 2;
                pTowerID.Value = Entity_Call.TowerID;
                pTowerName.Value = Entity_Call.TowerName;
                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] param = new SqlParameter[] { pAction, pTowerID, pTowerName, pCreatedBy, pCreatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TowerMaster.SP_TowerMaster, param);
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

        public int DeleteRecord(ref TowerMaster EntityCall, out string StrError)
        {
            int iDelete = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TowerMaster._Action, SqlDbType.BigInt);
                SqlParameter pTowerID = new SqlParameter(TowerMaster._TowerID, SqlDbType.BigInt);
                SqlParameter pDeletedBy = new SqlParameter(TowerMaster._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(TowerMaster._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(TowerMaster._IsDeleted, SqlDbType.Bit);

                pAction.Value = 3;
                pTowerID.Value = EntityCall.TowerID;
                pDeletedBy.Value = EntityCall.UserId;
                pDeletedDate.Value = EntityCall.LoginDate;
                pIsDeleted.Value = EntityCall.IsDeleted;

                SqlParameter[] param = new SqlParameter[] { pAction, pTowerID, pDeletedBy, pDeletedDate, pIsDeleted };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TowerMaster.SP_TowerMaster, param);

                if (iDelete > 0)
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
            return iDelete;
        }

        public DataSet GetTowerForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(TowerMaster._Action, SqlDbType.BigInt);
                SqlParameter pTowerID = new SqlParameter(TowerMaster._TowerID, SqlDbType.BigInt);

                pAction.Value = 4;
                pTowerID.Value = ID;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TowerMaster.SP_TowerMaster, pAction, pTowerID);

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
                SqlParameter pAction = new SqlParameter(TowerMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(TowerMaster._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TowerMaster.SP_TowerMaster, pAction, pRepCondition);
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
                SqlParameter pAction = new SqlParameter(TowerMaster._Action, SqlDbType.BigInt);

                SqlParameter PRepCondition = new SqlParameter(TowerMaster._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 6;
                PRepCondition.Value = Name;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TowerMaster.SP_TowerMaster,pAction, PRepCondition);
            }
            catch (Exception ex)
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
                SqlParameter pAction = new SqlParameter(TowerMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(TowerMaster._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = prefixText;

                SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition };

                Open(CONNECTION_STRING);
                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, TowerMaster.SP_TowerMaster, oparamcol);
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

        public DMTowerMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
