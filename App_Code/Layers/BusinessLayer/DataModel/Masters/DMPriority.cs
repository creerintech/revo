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
/// <summary>
/// Summary description for DMPriority
/// </summary>
namespace MayurInventory.DataModel
{
    public class DMPriority : Utility.Setting
    {
        public int InsertRecord(ref PriorityMaster Entity_call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PriorityMaster._Action, SqlDbType.BigInt);
                SqlParameter pPriority = new SqlParameter(PriorityMaster._Priority, SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(PriorityMaster._LoginId, SqlDbType.BigInt);
                SqlParameter PCreatedDate = new SqlParameter(PriorityMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 1;
                pPriority.Value = Entity_call.Priority;
                pCreatedBy.Value = Entity_call.LoginId;
                PCreatedDate.Value = Entity_call.LoginDate;

                SqlParameter[] param = new SqlParameter[] { pAction, pPriority, pCreatedBy, PCreatedDate};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, PriorityMaster.SP_Priority, param);

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

        public int UpdateRecord(ref PriorityMaster Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PriorityMaster._Action, SqlDbType.BigInt);
                SqlParameter pPriorityId = new SqlParameter(PriorityMaster._PriorityID, SqlDbType.BigInt);
                SqlParameter pPriority = new SqlParameter(PriorityMaster._Priority, SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(PriorityMaster._LoginId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(PriorityMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 2;
                pPriorityId.Value = Entity_Call.PriorityID;
                pPriority.Value = Entity_Call.Priority;
                pCreatedBy.Value = Entity_Call.LoginId;
                pCreatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] param = new SqlParameter[] { pAction, pPriorityId, pPriority, pCreatedBy, pCreatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, PriorityMaster.SP_Priority, param);
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

        public int DeleteRecord(ref PriorityMaster EntityCall, out string StrError)
        {
            int iDelete = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PriorityMaster._Action, SqlDbType.BigInt);
                SqlParameter pPriorityId = new SqlParameter(PriorityMaster._PriorityID, SqlDbType.BigInt);
                SqlParameter pDeletedBy = new SqlParameter(PriorityMaster._LoginId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(PriorityMaster._LoginDate, SqlDbType.DateTime);
                pAction.Value = 3;
                pPriorityId.Value = EntityCall.PriorityID;
                pDeletedBy.Value = EntityCall.LoginId;
                pDeletedDate.Value = EntityCall.LoginDate;

                SqlParameter[] param = new SqlParameter[] { pAction, pPriorityId, pDeletedBy, pDeletedDate};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, PriorityMaster.SP_Priority, param);

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

        public DataSet GetPriorityForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(PriorityMaster._Action, SqlDbType.BigInt);
                SqlParameter pPriorityId = new SqlParameter(PriorityMaster._PriorityID, SqlDbType.BigInt);

                pAction.Value = 4;
                pPriorityId.Value = ID;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PriorityMaster.SP_Priority, pAction, pPriorityId);

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

        public DataSet GetPriority(string RepCondition, out string StrError)
        {
            StrError = string.Empty;

            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PriorityMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(PriorityMaster._StrCondition, SqlDbType.NVarChar);


                pAction.Value = 5;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PriorityMaster.SP_Priority, pAction, pRepCondition);


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
                SqlParameter pAction = new SqlParameter(PriorityMaster._Action, SqlDbType.BigInt);
                SqlParameter PRepCondition = new SqlParameter(PriorityMaster._StrCondition, SqlDbType.NVarChar);
                pAction.Value = 6;
                PRepCondition.Value = Name;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PriorityMaster.SP_Priority,
                    pAction, PRepCondition);
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
                SqlParameter pAction = new SqlParameter(PriorityMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(PriorityMaster._StrCondition, SqlDbType.NVarChar);
                pAction.Value = 5;
                pRepCondition.Value = prefixText;
                SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition };
                Open(CONNECTION_STRING);
                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, PriorityMaster.SP_Priority, oparamcol);
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

        public DMPriority()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
