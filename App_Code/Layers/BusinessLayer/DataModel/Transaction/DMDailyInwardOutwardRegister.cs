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
using MayurInventory.DALSQLHelper;
using MayurInventory.DataModel;
using MayurInventory.DB;
using MayurInventory.EntityClass;
using MayurInventory.Utility;
using System.Data.SqlClient;
using System.Collections.Generic;
/// <summary>
/// Summary description for DMDailyInwardOutwardRegister
/// </summary>
namespace MayurInventory.DataModel
{
    public class DMDailyInwardOutwardRegister:Utility.Setting
    {
        public DataSet FillCombo(out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(DailyInwardOutwardRegister._Action, SqlDbType.BigInt);
                pAction.Value = 1;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, DailyInwardOutwardRegister.SP_DailyInwardOutwardRegister, pAction);
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
        public DataSet GetItemForList(int CategoryId, out string StrError)
        {
            DataSet DS = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(DailyInwardOutwardRegister._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(DailyInwardOutwardRegister._ItemId, SqlDbType.BigInt);
                pAction.Value = 11;
                pItemId.Value = CategoryId;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, DailyInwardOutwardRegister.SP_DailyInwardOutwardRegister, pAction, pItemId);
                return DS;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertDailyInwardOutwardRegister(ref DailyInwardOutwardRegister Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(DailyInwardOutwardRegister._Action, SqlDbType.BigInt);
                SqlParameter pInwardNo = new SqlParameter(DailyInwardOutwardRegister._InwardNo, SqlDbType.NVarChar);
                SqlParameter pInwardDate = new SqlParameter(DailyInwardOutwardRegister._InwardDate, SqlDbType.DateTime);
                SqlParameter pEmployeeId = new SqlParameter(DailyInwardOutwardRegister._EmployeeId, SqlDbType.BigInt);
                SqlParameter pLoginDate = new SqlParameter(DailyInwardOutwardRegister._LoginDate, SqlDbType.DateTime);
                SqlParameter pUserId = new SqlParameter(DailyInwardOutwardRegister._UserId, SqlDbType.BigInt);
                pAction.Value = 1;
                pInwardNo.Value = Entity_Call.InwardNo;
                pInwardDate.Value = Entity_Call.InwardDate;
                pEmployeeId.Value = Entity_Call.EmployeeId;
                pUserId.Value = Entity_Call.UserId;
                pLoginDate.Value = Entity_Call.LoginDate;
                SqlParameter[] param = new SqlParameter[] { pAction, pInwardNo, pInwardDate, pEmployeeId,  pLoginDate, pUserId };
                Open(CONNECTION_STRING);
                BeginTransaction();
                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, DailyInwardOutwardRegister.SP_DailyInwardOutwardRegister, param);
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

        public int InsertDailyInwardOutwardRegisterDetails(ref DailyInwardOutwardRegister Entity_Call, out string StrError)
        {
            StrError = string.Empty;
            int iInsert = 0;
            try
            {
                SqlParameter pAction = new SqlParameter(DailyInwardOutwardRegister._Action, SqlDbType.BigInt);
                SqlParameter pDailyOutRegisterId = new SqlParameter(DailyInwardOutwardRegister._DailyOutRegisterId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(DailyInwardOutwardRegister._ItemId, SqlDbType.BigInt);
                SqlParameter pQuantity = new SqlParameter(DailyInwardOutwardRegister._Quantity, SqlDbType.Decimal);
                pAction.Value = 8;
                pDailyOutRegisterId.Value = Entity_Call.DailyOutRegisterId;
                pItemId.Value = Entity_Call.ItemId;
                pQuantity.Value = Entity_Call.Quantity;
                SqlParameter[] param = new SqlParameter[] { pAction, pDailyOutRegisterId, pItemId, pQuantity};
                Open(CONNECTION_STRING);
                BeginTransaction();
                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, DailyInwardOutwardRegister.SP_DailyInwardOutwardRegister, param);
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
        public int UpdateDailyInwardOutwardRegister(ref DailyInwardOutwardRegister Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(DailyInwardOutwardRegister._Action, SqlDbType.BigInt);
                SqlParameter pDailyOutRegisterId = new SqlParameter(DailyInwardOutwardRegister._DailyOutRegisterId, SqlDbType.BigInt);
                SqlParameter pInwardNo = new SqlParameter(DailyInwardOutwardRegister._InwardNo, SqlDbType.NVarChar);
                SqlParameter pInwardDate = new SqlParameter(DailyInwardOutwardRegister._InwardDate, SqlDbType.DateTime);
                SqlParameter pEmployeeId = new SqlParameter(DailyInwardOutwardRegister._EmployeeId, SqlDbType.BigInt);
                SqlParameter pUpdatedBy = new SqlParameter(DailyInwardOutwardRegister._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(DailyInwardOutwardRegister._LoginDate, SqlDbType.DateTime);
                pAction.Value = 2;
                pDailyOutRegisterId.Value = Entity_Call.DailyOutRegisterId;
                pInwardNo.Value = Entity_Call.InwardNo;
                pInwardDate.Value = Entity_Call.InwardDate;
                pEmployeeId.Value = Entity_Call.EmployeeId;
                pUpdatedBy.Value = Entity_Call.UserId;
                pUpdatedDate.Value = Entity_Call.LoginDate;
                SqlParameter[] param = new SqlParameter[] { pAction, pDailyOutRegisterId, pInwardNo, pInwardDate, pEmployeeId, pUpdatedBy, pUpdatedDate };
                Open(CONNECTION_STRING);
                BeginTransaction();
                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, DailyInwardOutwardRegister.SP_DailyInwardOutwardRegister, param);
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

        public int DeleteDailyInwardOutwardRegister(ref DailyInwardOutwardRegister Entity_Call, out string StrError)
        {
            StrError = string.Empty;

            int iDelete = 0;
            try
            {

                SqlParameter pAction = new SqlParameter(DailyInwardOutwardRegister._Action, SqlDbType.BigInt);
                SqlParameter pDailyOutRegisterId = new SqlParameter(DailyInwardOutwardRegister._DailyOutRegisterId, SqlDbType.BigInt);
                SqlParameter pDeletedBy = new SqlParameter(DailyInwardOutwardRegister._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(DailyInwardOutwardRegister._LoginDate, SqlDbType.DateTime);
                pAction.Value = 3;
                pDailyOutRegisterId.Value = Entity_Call.DailyOutRegisterId;
                pDeletedBy.Value = Entity_Call.UserId;
                pDeletedDate.Value = Entity_Call.LoginDate;
                SqlParameter[] param = new SqlParameter[] { pAction, pDailyOutRegisterId, pDeletedBy, pDeletedDate };
                Open(CONNECTION_STRING);
                BeginTransaction();
                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, DailyInwardOutwardRegister.SP_DailyInwardOutwardRegister, param);
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
        public DataSet GetDailyInwardOutwardRegisterForEdit(int ID, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(DailyInwardOutwardRegister._Action, SqlDbType.BigInt);
                SqlParameter pDailyOutRegisterId = new SqlParameter(DailyInwardOutwardRegister._DailyOutRegisterId, SqlDbType.BigInt);
                pAction.Value = 4;
                pDailyOutRegisterId.Value = ID;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, DailyInwardOutwardRegister.SP_DailyInwardOutwardRegister, pAction, pDailyOutRegisterId);
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
        public string[] GetSuggestedRecord(string prefixText)
        {
            List<string> SearchList = new List<string>();

            string ListItem = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(DailyInwardOutwardRegister._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(DailyInwardOutwardRegister._StrCondition, SqlDbType.NVarChar);
                pAction.Value = 5;
                pRepCondition.Value = prefixText;
                SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition };
                Open(CONNECTION_STRING);
                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, DailyInwardOutwardRegister.SP_DailyInwardOutwardRegister, oparamcol);
                if (dr != null && dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        ListItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString(), dr[0].ToString());
                        SearchList.Add(ListItem);
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
            return SearchList.ToArray();
        }
        public DataSet GetItemForAdd(int ItemId, out string StrError)
        {
            DataSet DS = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(DailyInwardOutwardRegister._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(DailyInwardOutwardRegister._ItemId, SqlDbType.BigInt);
                pAction.Value = 12;
                pItemId.Value = ItemId;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, DailyInwardOutwardRegister.SP_DailyInwardOutwardRegister, pAction, pItemId);
                return DS;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataSet FillComboForReport(out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(DailyInwardOutwardRegister._Action, SqlDbType.BigInt);
                pAction.Value = 1;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, DailyInwardOutwardRegister.SP_DailyInwardOutwardRegister, pAction);
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
        public DataSet GetDailyInwardOutward(string RepCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(DailyInwardOutwardRegister._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(DailyInwardOutwardRegister._StrCondition, SqlDbType.NVarChar);
                pAction.Value = 5;
                pRepCondition.Value = RepCondition;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, DailyInwardOutwardRegister.SP_DailyInwardOutwardRegister, pAction, pRepCondition);
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
        public DMDailyInwardOutwardRegister()
        {		//
            // TODO: Add constructor logic here
            //
        }
    }
}