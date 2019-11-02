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
/// Summary description for DMIssueRegister
/// </summary>
/// 
namespace MayurInventory.DataModel
{
    public class DMIssueRegister:Utility.Setting
    {
        public int InsertIssueRegister(ref IssueRegister Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action,SqlDbType.BigInt);
                SqlParameter pIssueNo = new SqlParameter(IssueRegister._IssueNo,SqlDbType.NVarChar);
                SqlParameter pIssueDate = new SqlParameter(IssueRegister._IssueDate,SqlDbType.DateTime);
                SqlParameter pEmployeeId = new SqlParameter(IssueRegister._EmployeeId, SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter(IssueRegister._RequisitionCafeId,SqlDbType.BigInt);
                SqlParameter pLoginDate = new SqlParameter(IssueRegister._LoginDate,SqlDbType.DateTime);
                SqlParameter pUserId = new SqlParameter(IssueRegister._UserId,SqlDbType.BigInt);
                SqlParameter pIsDeleted = new SqlParameter(IssueRegister._IsDeleted,SqlDbType.Bit);

                pAction.Value = 1;
                pIssueNo.Value = Entity_Call.IssueNo;
                pIssueDate.Value = Entity_Call.IssueDate;
                pEmployeeId.Value = Entity_Call.EmployeeId;
                pRequisitionCafeId.Value = Entity_Call.RequisitionCafeId;

                pUserId.Value = Entity_Call.UserId;
                pLoginDate.Value = Entity_Call.LoginDate;
                pIsDeleted.Value = Entity_Call.IsDeleted;

                SqlParameter[] param = new SqlParameter[] { pAction, pIssueNo, pIssueDate, pEmployeeId, pRequisitionCafeId, pLoginDate, pUserId, pIsDeleted };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert=SQLHelper.ExecuteScalar(_Connection,_Transaction,CommandType.StoredProcedure,IssueRegister.SP_IssueRegister,param);

                if(iInsert>0)
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
                RollBackTransaction();
            }
            finally
            {
                Close();
            }
            return iInsert;
        }

        public int InsertIssueRegisterDetails(ref IssueRegister Entity_Call, out string StrError)
        {
            StrError = string.Empty;
            int iInsert = 0;
            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action,SqlDbType.BigInt);
                SqlParameter pIssueRegisterId = new SqlParameter(IssueRegister._IssueRegisterId,SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(IssueRegister._ItemId,SqlDbType.BigInt);
                SqlParameter pIssueQty = new SqlParameter(IssueRegister._IssueQty,SqlDbType.Decimal);
                SqlParameter pPendingQty = new SqlParameter(IssueRegister._PendingQty,SqlDbType.Decimal);
                SqlParameter pQty = new SqlParameter(IssueRegister._Qty,SqlDbType.Decimal);
                SqlParameter pNotes = new SqlParameter(IssueRegister._Notes,SqlDbType.NVarChar);

                pAction.Value = 8;
                pIssueRegisterId.Value = Entity_Call.IssueRegisterId;
                pItemId.Value = Entity_Call.ItemId;
                pIssueQty.Value = Entity_Call.IssueQty;
                pPendingQty.Value = Entity_Call.PendingQty;
                pQty.Value = Entity_Call.Qty;
                pNotes.Value = Entity_Call.Notes;

                SqlParameter[] param = new SqlParameter[] { pAction, pIssueRegisterId, pItemId, pPendingQty,pIssueQty,pQty,pNotes};
                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,IssueRegister.SP_IssueRegister,param);

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
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
            return iInsert;
        }

        public int UpdateIssueRegister(ref IssueRegister Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action, SqlDbType.BigInt);
                SqlParameter pIssueRegisterId = new SqlParameter(IssueRegister._IssueRegisterId, SqlDbType.BigInt);
                SqlParameter pIssueNo = new SqlParameter(IssueRegister._IssueNo, SqlDbType.NVarChar);
                SqlParameter pIssueDate = new SqlParameter(IssueRegister._IssueDate, SqlDbType.DateTime);
                SqlParameter pEmployeeId = new SqlParameter(IssueRegister._EmployeeId, SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter(IssueRegister._RequisitionCafeId, SqlDbType.BigInt);

                SqlParameter pUpdatedBy = new SqlParameter(IssueRegister._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(IssueRegister._LoginDate, SqlDbType.DateTime);

                pAction.Value = 2;
                pIssueRegisterId.Value = Entity_Call.IssueRegisterId;
                pIssueNo.Value = Entity_Call.IssueNo;
                pIssueDate.Value = Entity_Call.IssueDate;
                pEmployeeId.Value = Entity_Call.EmployeeId;
                pRequisitionCafeId.Value = Entity_Call.RequisitionCafeId;

                pUpdatedBy.Value = Entity_Call.UserId;
                pUpdatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] param = new SqlParameter[] { pAction, pIssueRegisterId, pIssueNo, pIssueDate, pEmployeeId, pRequisitionCafeId, pUpdatedBy, pUpdatedDate };
                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,IssueRegister.SP_IssueRegister,param);

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

        public int DeleteIssueRegister(ref IssueRegister Entity_Call, out string StrError)
        {
            StrError = string.Empty;

            int iDelete = 0;
            try
            {

                SqlParameter pAction = new SqlParameter(IssueRegister._Action,SqlDbType.BigInt);
                SqlParameter pIssueRegisterId = new SqlParameter(IssueRegister._IssueRegisterId,SqlDbType.BigInt);
                SqlParameter pIsDeleted = new SqlParameter(IssueRegister._IsDeleted,SqlDbType.Bit);

                SqlParameter pDeletedBy = new SqlParameter(IssueRegister._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(IssueRegister._LoginDate,SqlDbType.DateTime);

                pAction.Value = 3;
                pIssueRegisterId.Value = Entity_Call.IssueRegisterId;
                pIsDeleted.Value = Entity_Call.IsDeleted;
                pDeletedBy.Value = Entity_Call.UserId;
                pDeletedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] param = new SqlParameter[] {pAction,pIssueRegisterId,pIsDeleted,pDeletedBy,pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,IssueRegister.SP_IssueRegister,param);

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

        public DataSet GetIssueRegisterForEdit(int ID, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action, SqlDbType.BigInt);
                SqlParameter pIssueRegisterId = new SqlParameter(IssueRegister._IssueRegisterId, SqlDbType.BigInt);

                pAction.Value = 4;
                pIssueRegisterId.Value = ID;

                Open(CONNECTION_STRING);

                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, IssueRegister.SP_IssueRegister, pAction, pIssueRegisterId);
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

        public DataSet GetIssueRegister(string RepCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS=new DataSet();
            try
            {

                SqlParameter pAction = new SqlParameter(IssueRegister._Action,SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(IssueRegister._StrCondition,SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = RepCondition;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection,_Transaction,CommandType.StoredProcedure,IssueRegister.SP_IssueRegister,pAction,pRepCondition);
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
                SqlParameter pAction = new SqlParameter(IssueRegister._Action,SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(IssueRegister._StrCondition,SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = prefixText;

                SqlParameter[] oparamcol = new SqlParameter[] {pAction,pRepCondition };
                Open(CONNECTION_STRING);
                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection,_Transaction,CommandType.StoredProcedure,IssueRegister.SP_IssueRegister,oparamcol);

                if (dr != null && dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        ListItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString(),dr[0].ToString());
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

        public DataSet GetIssueRegisterNo(out string StrError)
        {
            DataSet DS = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action,SqlDbType.BigInt);

                pAction.Value = 6;
                Open(CONNECTION_STRING);
                DS=SQLHelper.GetDataSetSingleParm(_Connection,_Transaction,CommandType.StoredProcedure,IssueRegister.SP_IssueRegister,pAction);

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

        public DataSet FillCombo(out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action,SqlDbType.BigInt);

                pAction.Value = 7;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetSingleParm(_Connection,_Transaction,CommandType.StoredProcedure,IssueRegister.SP_IssueRegister,pAction);

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

        public DataSet FillItemGrid(int ID, out string StrError, bool DupFlag)
        {
            DataSet DS = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action,SqlDbType.BigInt);
                SqlParameter pReqCafeteriaId = new SqlParameter(IssueRegister._RequisitionCafeId,SqlDbType.BigInt);
                SqlParameter pIssueRegisterId = new SqlParameter(IssueRegister._IssueRegisterId, SqlDbType.BigInt);
                pAction.Value = 9;
                if (DupFlag == true)
                {
                    pIssueRegisterId.Value = ID;
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, IssueRegister.SP_IssueRegister, pAction, pIssueRegisterId);
                }
                if (DupFlag == false)
                {
                    pReqCafeteriaId.Value = ID;
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, IssueRegister.SP_IssueRegister, pAction, pReqCafeteriaId);
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
            return DS;
        }

        public DataSet GetItemDetails( string RepCondition,out string StrError)
        {
            DataSet DS = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action,SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(IssueRegister._StrCondition,SqlDbType.NVarChar);
                pAction.Value = 10;
                pRepCondition.Value = RepCondition;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection,_Transaction,CommandType.StoredProcedure,IssueRegister.SP_IssueRegister,pAction,pRepCondition);
                return DS;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetItemForAdd(int ItemId, out string StrError)
        {
            DataSet DS = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(IssueRegister._ItemId, SqlDbType.BigInt);
                pAction.Value = 12;
                pItemId.Value = ItemId;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, IssueRegister.SP_IssueRegister, pAction, pItemId);
                return DS;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

         public DataSet GetItemForList(int CategoryId, out string StrError)
        {
            DataSet DS = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(IssueRegister._ItemId, SqlDbType.BigInt);
                pAction.Value = 11;
                pItemId.Value = CategoryId;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, IssueRegister.SP_IssueRegister, pAction, pItemId);
                return DS;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        

        public DataSet FillComboForReport(int UserID,out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action, SqlDbType.BigInt);
                SqlParameter pUserId = new SqlParameter("@UserId",SqlDbType.BigInt);

                pAction.Value = 1;
                pUserId.Value = UserID;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, IssueRegister.SP_IssueRegisterReport, pAction,pAction);
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

        #region[This Region For Material Issue Report]------------------------------------------
        public DataSet ShowIssueRegisterReport(string RepCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {

                SqlParameter pAction = new SqlParameter(IssueRegister._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(IssueRegister._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 2;
                pRepCondition.Value = RepCondition;
                Open(Setting.CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, IssueRegister.SP_IssueRegisterReport, pAction, pRepCondition);
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

        public DataSet ShowIssueRegisterDetailsReport(string RepCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(IssueRegister._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 3;
                pRepCondition.Value = RepCondition;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, IssueRegister.SP_IssueRegisterReport, pAction, pRepCondition);
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

        public DataSet GetIRForPrint(Int32 IssueRegisterId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(IssueRegister._Action, SqlDbType.BigInt);
                SqlParameter pIssueRegisterId = new SqlParameter(IssueRegister._IssueRegisterId, SqlDbType.BigInt);
                pAction.Value = 5;
                pIssueRegisterId.Value = IssueRegisterId;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, IssueRegister.SP_IssueRegisterReport, pAction, pIssueRegisterId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        #endregion[End]------------------------------------------------------------------------------
        public DMIssueRegister()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}