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

namespace MayurInventory.DataModel
{
    public class DMUnit:Utility.Setting
    {
        public int InsertRecord(ref Unit Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(Unit._Action, SqlDbType.BigInt);
                SqlParameter pUnit = new SqlParameter(Unit._Unit, SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(Unit._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(Unit._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(Unit._IsDeleted, SqlDbType.Bit);

                pAction.Value = 1;
                pUnit.Value = Entity_Call.UnitName;
                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;
                pIsDeleted.Value = Entity_Call.IsDeleted;

                SqlParameter[] param = new SqlParameter[] { pAction, pUnit, pCreatedBy, pCreatedDate, pIsDeleted };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, Unit.SP_UnitMaster, param);

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
                throw new Exception(ex.Message);
            }

            finally
            {
                Close(); 
            }
            return iInsert;
        }

        public int UpdateRecord(ref Unit Entity_Call, out String StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(Unit._Action, SqlDbType.BigInt);
                SqlParameter pUnitId = new SqlParameter(Unit._UnitId, SqlDbType.BigInt);
                SqlParameter pUnit = new SqlParameter(Unit._Unit, SqlDbType.NVarChar);
                SqlParameter pUpdatedBy = new SqlParameter(Unit._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(Unit._LoginDate, SqlDbType.DateTime);

                pAction.Value = 2;
                pUnitId.Value = Entity_Call.UnitId;
                pUnit.Value = Entity_Call.UnitName;
                pUpdatedBy.Value = Entity_Call.UserId;
                pUpdatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] param = new SqlParameter[] { pAction, pUnitId, pUnit, pUpdatedBy, pUpdatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, Unit.SP_UnitMaster, param);

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
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
            return iInsert;
        
        }

        public int DeleteRecord(ref Unit Entity_Call, out string StrError)
        {
            int iDelete = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pUnitId = new SqlParameter(Unit._UnitId, SqlDbType.BigInt);

                SqlParameter pIsDeleted = new SqlParameter(Unit._IsDeleted,SqlDbType.Bit);
                SqlParameter pDeletedBy = new SqlParameter(Unit._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(Unit._LoginDate, SqlDbType.DateTime);

                pAction.Value = 3;
                pUnitId.Value = Entity_Call.UnitId;

                pIsDeleted.Value = Entity_Call.IsDeleted;
                pDeletedBy.Value = Entity_Call.UserId;
                pDeletedDate.Value = Entity_Call.LoginDate;
                SqlParameter[] param = new SqlParameter[] { pAction, pUnitId,pIsDeleted, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,Unit.SP_UnitMaster,param);
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
                throw new Exception(ex.Message);
            }
            finally 
            {
                Close();
            }
            return iDelete;
        }

        public DataSet GetUnitForEdit(int ID, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(Unit._Action, SqlDbType.BigInt);
                SqlParameter pUnitID = new SqlParameter(Unit._UnitId, SqlDbType.BigInt);

                pAction.Value = 4;
                pUnitID.Value = ID;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Unit.SP_UnitMaster, pAction, pUnitID);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
            return DS;
        }

        public DataSet GetUnit(string RepCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(Unit._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(Unit._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Unit.SP_UnitMaster, pAction, pRepCondition);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                SqlParameter pAction = new SqlParameter(Unit._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(Unit._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 6;
                pRepCondition.Value = Name;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Unit.SP_UnitMaster, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
            return DS;
        }

        public string[] GetSuggestRecord(string prefixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem= string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(Unit._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(Unit._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = prefixText;

                SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition };

                Open(CONNECTION_STRING);
                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, Unit.SP_UnitMaster, oparamcol);

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
        public DMUnit()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}