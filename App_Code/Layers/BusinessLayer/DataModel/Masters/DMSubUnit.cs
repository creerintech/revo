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
    public class DMSubUnit : Utility.Setting
    {
        #region[BusinessLogic]
        public int InsertRecord(ref SubUnit Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(SubUnit._Action, SqlDbType.BigInt);
                SqlParameter pSubUnit = new SqlParameter(SubUnit._SubUnit1, SqlDbType.NVarChar);
                SqlParameter pUnitId = new SqlParameter(SubUnit._UnitId, SqlDbType.BigInt);
                SqlParameter pConversionFactor = new SqlParameter(SubUnit._ConversionFactor, SqlDbType.BigInt);

                SqlParameter pCreatedBy = new SqlParameter(SubUnit._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(SubUnit._LoginDate, SqlDbType.DateTime);

                pAction.Value = 1;
                pSubUnit.Value = Entity_Call.SubUnit1;
                pUnitId.Value = Entity_Call.UnitId;
                pConversionFactor.Value = Entity_Call.ConversionFactor;
                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] param = new SqlParameter[] { pAction, pSubUnit, pUnitId , pConversionFactor, pCreatedBy, pCreatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, SubUnit.SP_SubUnitMaster, param);

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

        public int UpdateRecord(ref SubUnit Entity_Call, out String StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(SubUnit._Action, SqlDbType.BigInt);
                SqlParameter pSubUnitId = new SqlParameter(SubUnit._SubUnitId, SqlDbType.BigInt);
                SqlParameter pSubUnit = new SqlParameter(SubUnit._SubUnit1, SqlDbType.NVarChar);
                SqlParameter pConversionFactor = new SqlParameter(SubUnit._ConversionFactor, SqlDbType.NVarChar);
                //SqlParameter pUnitId = new SqlParameter(SubUnit._UnitId, SqlDbType.BigInt);

                SqlParameter pUpdatedBy = new SqlParameter(SubUnit._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(SubUnit._LoginDate, SqlDbType.DateTime);

                pAction.Value = 2;
                pSubUnitId.Value = Entity_Call.SubUnitId;
                pSubUnit.Value = Entity_Call.SubUnit1;
                pConversionFactor.Value = Entity_Call.ConversionFactor;
                pUpdatedBy.Value = Entity_Call.UserId;
                pUpdatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] param = new SqlParameter[] { pAction, pSubUnit,pSubUnitId,pConversionFactor,pUpdatedBy,pUpdatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, SubUnit.SP_SubUnitMaster, param);

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

        public int DeleteRecord(ref SubUnit Entity_Call, out string StrError)
        {
            int iDelete = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pSubUnitId = new SqlParameter(SubUnit._SubUnitId, SqlDbType.BigInt);
                SqlParameter pDeletedBy = new SqlParameter(Unit._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(Unit._LoginDate, SqlDbType.DateTime);

                pAction.Value = 3;
                pSubUnitId.Value = Entity_Call.SubUnitId;
                pDeletedBy.Value = Entity_Call.UserId;
                pDeletedDate.Value = Entity_Call.LoginDate;
                SqlParameter[] param = new SqlParameter[] { pAction, pSubUnitId, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, SubUnit.SP_SubUnitMaster, param);
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
                SqlParameter pAction = new SqlParameter(SubUnit._Action, SqlDbType.BigInt);
                SqlParameter pSubUnitID = new SqlParameter(SubUnit._SubUnitId, SqlDbType.BigInt);

                pAction.Value = 4;
                pSubUnitID.Value = ID;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, SubUnit.SP_SubUnitMaster, pAction, pSubUnitID);
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

        public DataSet GetSubUnit(string RepCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(SubUnit._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(SubUnit._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, SubUnit.SP_SubUnitMaster, pAction, pRepCondition);
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
                SqlParameter pAction = new SqlParameter(SubUnit._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(SubUnit._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 6;
                pRepCondition.Value = Name;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, SubUnit.SP_SubUnitMaster, pAction, pRepCondition);

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
            string ListItem = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(SubUnit._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(SubUnit._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = prefixText;

                SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition };

                Open(CONNECTION_STRING);
                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, SubUnit.SP_SubUnitMaster, oparamcol);

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

        public DataSet FillCombo(out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(SubUnit._Action, SqlDbType.BigInt);

                pAction.Value = 7;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, SubUnit.SP_SubUnitMaster, pAction);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        #endregion

        public DMSubUnit()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
