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
using MayurInventory.DataModel;
using MayurInventory.EntityClass;
using MayurInventory.DALSQLHelper;
using MayurInventory.DB;


namespace MayurInventory.DataModel
{
    public class DMItemSizeMaster : Utility.Setting
    {
        public int InsertRecord(ref ItemSizeMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ItemSizeMaster._Action, SqlDbType.BigInt);
                SqlParameter pCategoryName = new SqlParameter(ItemSizeMaster._SizeName, SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(ItemSizeMaster._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(ItemSizeMaster._LoginDate, SqlDbType.DateTime);
                
                pAction.Value = 1;
                pCategoryName.Value = Entity_Call.SizeName;
                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;
               
                SqlParameter[] Param = new SqlParameter[] {pAction, pCategoryName, pCreatedBy, pCreatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ItemSizeMaster.SP_ItemSizeMaster, Param);

                if (iInsert > 0)
                {
                    CommitTransaction();
                }
                else
                {
                    RollBackTransaction();
                }
            }catch (Exception ex)
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

        public int UpdateRecord(ref ItemSizeMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ItemSizeMaster._Action, SqlDbType.BigInt);
                SqlParameter pCategoryId = new SqlParameter(ItemSizeMaster._SizeId, SqlDbType.BigInt);
                SqlParameter pCategoryName = new SqlParameter(ItemSizeMaster._SizeName, SqlDbType.NVarChar);
                SqlParameter pUpdatedBy = new SqlParameter(ItemSizeMaster._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(ItemSizeMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 2;
                pCategoryId.Value = Entity_Call.SizeId;
                pCategoryName.Value = Entity_Call.SizeName;

                pUpdatedBy.Value = Entity_Call.UserId;
                pUpdatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pCategoryId, pCategoryName, pUpdatedBy, pUpdatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ItemSizeMaster.SP_ItemSizeMaster, Param);

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

        public int DeleteRecord(ref ItemSizeMaster Entity_Call, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ItemSizeMaster._Action, SqlDbType.BigInt);
                SqlParameter pCategoryId = new SqlParameter(ItemSizeMaster._SizeId, SqlDbType.BigInt);
                SqlParameter pDeletedBy = new SqlParameter(ItemSizeMaster._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(ItemSizeMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 3;
                pCategoryId.Value = Entity_Call.SizeId;
                pDeletedBy.Value = Entity_Call.UserId;
                pDeletedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] {pAction, pCategoryId, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ItemSizeMaster.SP_ItemSizeMaster, Param);

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
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return iDelete;
        }

        public DataSet GetItemSizeForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemSizeMaster._Action, SqlDbType.BigInt);
                SqlParameter pCategoryId = new SqlParameter(ItemSizeMaster._SizeId, SqlDbType.BigInt);

                pAction.Value = 4;
                pCategoryId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemSizeMaster.SP_ItemSizeMaster, pAction, pCategoryId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetItemSize(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemSizeMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ItemSizeMaster._strCond, SqlDbType.NVarChar);
                
                pAction.Value = 5;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemSizeMaster.SP_ItemSizeMaster ,pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public string[] GetSuggestedRecord(string prefixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;
            try
            {

                // -- For Checking OF Execution of Procedure=========   
                SqlParameter pAction = new SqlParameter(ItemSizeMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ItemSizeMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { pAction, pRepCondition };

                Open(CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, ItemSizeMaster.SP_ItemSizeMaster, oParmCol);

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

        public DataSet ChkDuplicate(string Name, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemSizeMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ItemSizeMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 6;
                pRepCondition.Value = Name;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemSizeMaster.SP_ItemSizeMaster, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DMItemSizeMaster()
        {
            
        }
    }
}
