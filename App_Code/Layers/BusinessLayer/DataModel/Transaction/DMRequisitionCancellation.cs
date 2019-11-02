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
/// Summary description for DMRequisitionCancellation
/// </summary>

namespace MayurInventory.DataModel
{
    public class DMRequisitionCancellation:Utility.Setting
    {
        #region[old methods]
        
        public DataSet GetRequisitionCancelationList(string RepCondition, out string StrError)
        {
            StrError = string.Empty;

            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(RequisitionCancellation._Action, SqlDbType.BigInt);
                SqlParameter PRepCondition = new SqlParameter(RequisitionCancellation._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 7;
                PRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);

                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation, pAction, PRepCondition);


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
        public DataSet GetDuplicate(string ID, out string StrError)
        {
            StrError = string.Empty;
            DataSet dss = new DataSet();
        //    try
        //    {
        //        SqlParameter pAction = new SqlParameter(RequisitionCancellation._Action, SqlDbType.BigInt);
        //        SqlParameter pReqNo = new SqlParameter(RequisitionCancellation._RequisitionNo, SqlDbType.NVarChar);

        //        pAction.Value = 10;
        //        pReqNo.Value = ID;

        //        Open(CONNECTION_STRING);

        //        dss = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation, pAction, pReqNo);

        //    }
        //    catch (Exception ex)
        //    {
        //        StrError = ex.Message;
        //    }
        //    finally
        //    {
        //        Close();
        //    }
          return dss;
        }

        #endregion

        #region[New Methods]

        public int InsertRecord(ref RequisitionCancellation Entity_Call, out string StrError)
        {
              int iInsert = 0;
              StrError = string.Empty;
               try
               {
                   SqlParameter pAction = new SqlParameter(RequisitionCancellation._Action, SqlDbType.BigInt);
                  // SqlParameter pRequisitionCancelNo = new SqlParameter(RequisitionCancellation._RequisitionCancelNo, SqlDbType.NVarChar);
                   SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCancellation._RequisitionCafeId, SqlDbType.BigInt);
                   SqlParameter pCancelledBy = new SqlParameter(RequisitionCancellation._CancelledBy, SqlDbType.NVarChar);
                   SqlParameter pCancelledDate = new SqlParameter(RequisitionCancellation._CancelledDate, SqlDbType.DateTime);
                   SqlParameter pReason = new SqlParameter(RequisitionCancellation._Reason, SqlDbType.NVarChar);
                   SqlParameter pCancelType = new SqlParameter(RequisitionCancellation._CancelType, SqlDbType.Bit);


                   SqlParameter pCreatedBy = new SqlParameter(RequisitionCancellation._UserId, SqlDbType.BigInt);
                   SqlParameter pCreatedDate = new SqlParameter(RequisitionCancellation._LoginDate, SqlDbType.DateTime);

                   pAction.Value = 1;
                  // pRequisitionCancelNo.Value = Entity_Call.RequisitionCancelNo;
                   pRequisitionCafeId.Value = Entity_Call.RequisitionCafeId;
                   pCancelledBy.Value = Entity_Call.CancelledBy;
                   pCancelledDate.Value = Entity_Call.CancelledDate;
                   pReason.Value = Entity_Call.Reason;
                   pCancelType.Value = Entity_Call.CancelType;

                   pCreatedBy.Value = Entity_Call.UserId;
                   pCreatedDate.Value = Entity_Call.LoginDate;


                   SqlParameter[] param = new SqlParameter[] {pAction,pRequisitionCafeId,pCancelledBy,
                        pCancelledDate,pReason,pCancelType,pCreatedBy,pCreatedDate};

                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation, param);

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

        public int InsertRecordForItem(ref RequisitionCancellation Entity_Call, out string StrError)
        {
              int iInsert = 0;
              StrError = string.Empty;
               try
               {
                   SqlParameter pAction = new SqlParameter(RequisitionCancellation._Action, SqlDbType.BigInt);
                   SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCancellation._RequisitionCafeId, SqlDbType.BigInt);
                   SqlParameter pCancelledBy = new SqlParameter(RequisitionCancellation._CancelledBy, SqlDbType.NVarChar);
                   SqlParameter pCancelledDate = new SqlParameter(RequisitionCancellation._CancelledDate, SqlDbType.DateTime);
                   SqlParameter pReason = new SqlParameter(RequisitionCancellation._Reason, SqlDbType.NVarChar);
                   SqlParameter pCancelType = new SqlParameter(RequisitionCancellation._CancelType, SqlDbType.Bit);

                   SqlParameter pCreatedBy = new SqlParameter(RequisitionCancellation._UserId, SqlDbType.BigInt);
                   SqlParameter pCreatedDate = new SqlParameter(RequisitionCancellation._LoginDate, SqlDbType.DateTime);

                   pAction.Value = 1;
                   pRequisitionCafeId.Value = Entity_Call.RequisitionCafeId;
                   pCancelledBy.Value = Entity_Call.CancelledBy;
                   pCancelledDate.Value = Entity_Call.CancelledDate;
                   pReason.Value = Entity_Call.Reason;
                   pCancelType.Value = Entity_Call.CancelType;

                   pCreatedBy.Value = Entity_Call.UserId;
                   pCreatedDate.Value = Entity_Call.LoginDate;


                   SqlParameter[] param = new SqlParameter[] {pAction,pRequisitionCafeId,pCancelledBy,
                        pCancelledDate,pReason,pCancelType,pCreatedBy,pCreatedDate};

                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation2, param);

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
        
        public DataSet FillCombo(string Condition,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);
                pAction.Value = 5;
                pCOND.Value = Condition;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation, pAction,pCOND);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetRecordForRequisition(string Condition,string StrCondition, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pStrcond = new SqlParameter("@StrCondition", SqlDbType.NVarChar);
                SqlParameter pGrdDateCondition = new SqlParameter("@GrdDateCondition", SqlDbType.NVarChar);

                pAction.Value = 4;
                pStrcond.Value=Condition;
                pGrdDateCondition.Value = StrCondition;
                
                SqlParameter[] param = new SqlParameter[] { pAction, pStrcond, pGrdDateCondition };

                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation,param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return ds;
        }

        public DataSet GetRecordForItem(string Condition, int ReqId, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pStrcond = new SqlParameter("@StrCondition", SqlDbType.NVarChar);
                SqlParameter pReqId = new SqlParameter("@RequisitionCafeId", SqlDbType.BigInt);

                pAction.Value = 4;
                pStrcond.Value = Condition;
                pReqId.Value = ReqId;

                SqlParameter[] param = new SqlParameter[] { pAction, pStrcond, pReqId };

                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation, param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return ds;
        }

        public int UpdateIsCancelFlag(int RequisitionId, out string StrError)
        {
            int iUpdate = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId= new SqlParameter("@RequisitionCafeId", SqlDbType.BigInt);

                pAction.Value = 6;
                pRequisitionCafeId.Value = RequisitionId;

                SqlParameter[] opara = new SqlParameter[] { pAction, pRequisitionCafeId };
                Open(CONNECTION_STRING);
                BeginTransaction();
                iUpdate = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure,"SP_RequisitionCancellation", opara);

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

        public int UpdateIsCancelInReqCafeDtls(int ItemId,int RequisitionId, out string StrError)
        {
            int iUpdate = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter("@ItemId ", SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter("@RequisitionCafeId", SqlDbType.BigInt);

                pAction.Value = 9;
                pItemId.Value = ItemId;
                pRequisitionCafeId.Value = RequisitionId;

                SqlParameter[] opara = new SqlParameter[] { pAction, pItemId, pRequisitionCafeId };
                Open(CONNECTION_STRING);
                BeginTransaction();
                iUpdate = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, "SP_RequisitionCancellation", opara);

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

        public DataSet GetReqCafeteria(int ReqId, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pReqCafeId= new SqlParameter(RequisitionCancellation._RequisitionCafeId, SqlDbType.NVarChar);

                pAction.Value = 8;
                pReqCafeId.Value = ReqId;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation, pAction, pReqCafeId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return ds;
        }

        public int InsertReqCancelDtls(ref RequisitionCancellation Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(RequisitionCancellation._Action, SqlDbType.BigInt);
                SqlParameter pRequisitionCancelId = new SqlParameter(RequisitionCancellation._RequisitionCancelId, SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCancellation._RequisitionCafeId, SqlDbType.BigInt);
                SqlParameter pItemsId = new SqlParameter(RequisitionCancellation._ItemId, SqlDbType.BigInt);
                SqlParameter pQty = new SqlParameter(RequisitionCancellation._OrdQty, SqlDbType.Decimal);
               
                pAction.Value = 10;
                pRequisitionCancelId.Value = Entity_Call.RequisitionCancelId;
                pRequisitionCafeId.Value = Entity_Call.RequisitionCafeId;
                pItemsId.Value = Entity_Call.ItemId;
                pQty.Value = Entity_Call.OrdQty;

                SqlParameter[] param = new SqlParameter[] { pAction, pRequisitionCancelId, pRequisitionCafeId, pItemsId, pQty };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation, param);

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

        public DataSet GetRecordForEdit(int ReqId, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pReqCafeId = new SqlParameter(RequisitionCancellation._RequisitionCafeId, SqlDbType.NVarChar);

                pAction.Value = 2;
                pReqCafeId.Value = ReqId;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation, pAction, pReqCafeId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return ds;
        }

        public DataSet GetRecordForEdit_ItemWise(int ReqCancelId,int ReqId, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pRequisitionCancelId = new SqlParameter(RequisitionCancellation._RequisitionCancelId, SqlDbType.NVarChar);
                SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCancellation._RequisitionCafeId, SqlDbType.NVarChar);

                pAction.Value = 3;
                pRequisitionCancelId.Value = ReqCancelId;
                pRequisitionCafeId.Value = ReqId;

                SqlParameter[] param = new SqlParameter[] { pAction, pRequisitionCancelId, pRequisitionCafeId};
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation2, param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return ds;
        }

        public DataSet GetRecordForEdit_RequisitionWise(int ReqCancelId, int ReqId, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pRequisitionCancelId = new SqlParameter(RequisitionCancellation._RequisitionCancelId, SqlDbType.NVarChar);
                SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCancellation._RequisitionCafeId, SqlDbType.NVarChar);

                pAction.Value = 4;
                pRequisitionCancelId.Value = ReqCancelId;
                pRequisitionCafeId.Value = ReqId;

                SqlParameter[] param = new SqlParameter[] { pAction, pRequisitionCancelId, pRequisitionCafeId };
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation2, param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return ds;
        }

        public int UpdateRequisitionWise(ref RequisitionCancellation Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(RequisitionCancellation._Action, SqlDbType.BigInt);
                SqlParameter pRequisitionCancelId = new SqlParameter(RequisitionCancellation._RequisitionCancelId, SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCancellation._RequisitionCafeId, SqlDbType.BigInt);
                SqlParameter pCancelledBy = new SqlParameter(RequisitionCancellation._CancelledBy, SqlDbType.NVarChar);
                SqlParameter pCancelledDate = new SqlParameter(RequisitionCancellation._CancelledDate, SqlDbType.DateTime);
                SqlParameter pReason = new SqlParameter(RequisitionCancellation._Reason, SqlDbType.NVarChar);
                SqlParameter pCancelType = new SqlParameter(RequisitionCancellation._CancelType, SqlDbType.Bit);
                SqlParameter pIsCancel = new SqlParameter(RequisitionCancellation._IsCancel, SqlDbType.BigInt);


                SqlParameter pCreatedBy = new SqlParameter(RequisitionCancellation._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(RequisitionCancellation._LoginDate, SqlDbType.DateTime);

                pAction.Value = 5;
                pRequisitionCancelId.Value = Entity_Call.RequisitionCancelId;
                pRequisitionCafeId.Value = Entity_Call.RequisitionCafeId;
                pCancelledBy.Value = Entity_Call.CancelledBy;
                pCancelledDate.Value = Entity_Call.CancelledDate;
                pReason.Value = Entity_Call.Reason;
                pCancelType.Value = Entity_Call.CancelType;
                pIsCancel.Value = Entity_Call.IsCancel;

                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;


                SqlParameter[] param = new SqlParameter[] {pAction,pRequisitionCancelId,pRequisitionCafeId,pCancelledBy,pIsCancel,
                        pCancelledDate,pReason,pCancelType,pCreatedBy,pCreatedDate};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation2, param);

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

        public int UpdateRequisitionItemWise(ref RequisitionCancellation Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(RequisitionCancellation._Action, SqlDbType.BigInt);
                SqlParameter pRequisitionCancelId = new SqlParameter(RequisitionCancellation._RequisitionCancelId, SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCancellation._RequisitionCafeId, SqlDbType.BigInt);
                SqlParameter pCancelledBy = new SqlParameter(RequisitionCancellation._CancelledBy, SqlDbType.NVarChar);
                SqlParameter pCancelledDate = new SqlParameter(RequisitionCancellation._CancelledDate, SqlDbType.DateTime);
                SqlParameter pReason = new SqlParameter(RequisitionCancellation._Reason, SqlDbType.NVarChar);
                SqlParameter pCancelType = new SqlParameter(RequisitionCancellation._CancelType, SqlDbType.Bit);

                SqlParameter pCreatedBy = new SqlParameter(RequisitionCancellation._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(RequisitionCancellation._LoginDate, SqlDbType.DateTime);

                pAction.Value = 6;
                pRequisitionCancelId.Value = Entity_Call.RequisitionCancelId;
                pRequisitionCafeId.Value = Entity_Call.RequisitionCafeId;
                pCancelledBy.Value = Entity_Call.CancelledBy;
                pCancelledDate.Value = Entity_Call.CancelledDate;
                pReason.Value = Entity_Call.Reason;
                pCancelType.Value = Entity_Call.CancelType;

                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;


                SqlParameter[] param = new SqlParameter[] {pAction,pRequisitionCancelId,pRequisitionCafeId,pCancelledBy,
                        pCancelledDate,pReason,pCancelType,pCreatedBy,pCreatedDate};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation2, param);

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

        public int DeleteRequisitionCancel(string DeleteCancelType, int ReqCancelId, int ReqId, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(RequisitionCancellation._Action, SqlDbType.BigInt);
                SqlParameter pRequisitionCancelId = new SqlParameter(RequisitionCancellation._RequisitionCancelId, SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCancellation._RequisitionCafeId, SqlDbType.BigInt);
                SqlParameter pDeleteCancelType = new SqlParameter("@DeleteCancelType", SqlDbType.NVarChar);
                

                pAction.Value = 7;
                pRequisitionCancelId.Value = ReqCancelId;
                pRequisitionCafeId.Value = ReqId;
                pDeleteCancelType.Value = DeleteCancelType;



                SqlParameter[] param = new SqlParameter[] { pAction, pRequisitionCancelId, pRequisitionCafeId, pDeleteCancelType };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCancellation.SP_RequisitionCancellation2, param);

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

        public string[] GetSuggestedRecord(string prefixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 7;
                pRepCondition.Value = prefixText;

                SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition };

                Open(CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, oparamcol);

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
                throw ex;
            }
            finally
            {
                Close();
            }

            return SearchList.ToArray();
        }

        #endregion


        #region[Report]

        public DataSet GetReqCancelPrint(Int32 RequisitionCancelId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(RequisitionCancellation._Action, SqlDbType.BigInt);
                SqlParameter pRequisitionCancelId = new SqlParameter(RequisitionCancellation._RequisitionCancelId, SqlDbType.BigInt);

                pAction.Value = 5;
                pRequisitionCancelId.Value = RequisitionCancelId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_RequisitionCafeteriaReport", pAction, pRequisitionCancelId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        #endregion
        public DMRequisitionCancellation()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
