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
    public class DMTransferLocationOutside:Utility.Setting
    {
        #region[BusinessLogic]
          public DataSet FillCombo(out string StrError)
        {
            DataSet DS = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);

                pAction.Value = 6;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, pAction);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return DS;
        }

          public DataSet GetTransNo(out string StrError)
        {
            StrError = string.Empty;

            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                pAction.Value = 7;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, pAction);
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

          public int InsertRecord(ref TransferLocationOutside Entity_Trans, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                SqlParameter pTransferNo = new SqlParameter(TransferLocationOutside._TransNo, SqlDbType.NVarChar);
                SqlParameter pTransferDate = new SqlParameter(TransferLocationOutside._Date, SqlDbType.DateTime);
                SqlParameter pTransferBy = new SqlParameter(TransferLocationOutside._TransBy, SqlDbType.BigInt);
                SqlParameter pNotes = new SqlParameter(TransferLocationOutside._Notes, SqlDbType.NVarChar);
                SqlParameter pType = new SqlParameter(TransferLocationOutside._Type, SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(TransferLocationOutside._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(TransferLocationOutside._LoginDate, SqlDbType.DateTime);

                pAction.Value = 1;
                pTransferNo.Value = Entity_Trans.TransNo;
                pTransferDate.Value = Entity_Trans.Date;
                pTransferBy.Value = Entity_Trans.TransBy;
                pNotes.Value = Entity_Trans.Notes;
                pType.Value = Entity_Trans.Type;
                pCreatedBy.Value = Entity_Trans.UserID;
                pCreatedDate.Value = Entity_Trans.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pTransferNo, pTransferDate, pTransferBy, pNotes, pCreatedBy, pCreatedDate,pType };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, Param);

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

          public int InsertDetailsRecord(ref TransferLocationOutside Entity_Trans, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pTransferId = new SqlParameter(TransferLocationOutside._TransId, SqlDbType.BigInt);
                SqlParameter pCategoryId = new SqlParameter(TransferLocationOutside._CategoryId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(TransferLocationOutside._ItemId, SqlDbType.BigInt);
                SqlParameter pTransFrom = new SqlParameter(TransferLocationOutside._TransFrom, SqlDbType.BigInt);
                SqlParameter pQtyAtSource = new SqlParameter(TransferLocationOutside._QtyAtSource, SqlDbType.Decimal);
                SqlParameter pTransTo = new SqlParameter(TransferLocationOutside._TransTo, SqlDbType.BigInt);
                SqlParameter pQtyAtDest = new SqlParameter(TransferLocationOutside._QtyAtDest, SqlDbType.Decimal);
                SqlParameter pTransferqTY = new SqlParameter(TransferLocationOutside._TransQty, SqlDbType.Decimal);
                SqlParameter prate= new SqlParameter(TransferLocationOutside._rate, SqlDbType.Decimal);

                pAction.Value = 8;
                pTransferId.Value = Entity_Trans.TransId;
                pCategoryId.Value = Entity_Trans.CategoryId;
                pTransFrom.Value = Entity_Trans.TransFrom;
                pQtyAtSource.Value = Entity_Trans.QtyAtSource;
                pQtyAtDest.Value = Entity_Trans.QtyAtDest;
                pTransTo.Value = Entity_Trans.TransTo;
                pTransferqTY.Value = Entity_Trans.TransQty;
                pItemId.Value = Entity_Trans.ItemId;
                prate.Value = Entity_Trans.rate;
                SqlParameter[] Param = new SqlParameter[] { pAction, pTransferId, pCategoryId, pTransFrom, pQtyAtSource, pQtyAtDest, pTransTo, pTransferqTY, pItemId,prate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, Param);

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

          public int UpdateRecord(ref TransferLocationOutside Entity_Trans, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                SqlParameter pTransNo = new SqlParameter(TransferLocationOutside._TransNo, SqlDbType.NVarChar);
                SqlParameter pDate = new SqlParameter(TransferLocationOutside._Date, SqlDbType.DateTime);
                SqlParameter pTransBy = new SqlParameter(TransferLocationOutside._TransBy, SqlDbType.BigInt);
                SqlParameter pNotes = new SqlParameter(TransferLocationOutside._Notes, SqlDbType.NVarChar);
                SqlParameter pTransId = new SqlParameter(TransferLocationOutside._TransId, SqlDbType.BigInt);
                SqlParameter pUpdatedBy = new SqlParameter(TransferLocationOutside._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(TransferLocationOutside._LoginDate, SqlDbType.DateTime);

                pAction.Value = 2;
                pTransNo.Value = Entity_Trans.TransNo;
                pDate.Value = Entity_Trans.Date;
                pTransBy.Value = Entity_Trans.TransBy;
                pNotes.Value = Entity_Trans.Notes;
                pTransId.Value = Entity_Trans.TransId;
                pUpdatedBy.Value = Entity_Trans.UserID;
                pUpdatedDate.Value = Entity_Trans.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pTransNo, pDate, pTransBy, pNotes, pTransId, pUpdatedBy, pUpdatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, Param);

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

          public int DeleteRecord(ref TransferLocationOutside Entity_Trans, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                SqlParameter pTransId = new SqlParameter(TransferLocationOutside._TransId, SqlDbType.BigInt);

                SqlParameter pDeletedBy = new SqlParameter(TransferLocationOutside._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(TransferLocationOutside._LoginDate, SqlDbType.DateTime);

                pAction.Value = 3;
                pTransId.Value = Entity_Trans.TransId;

                pDeletedBy.Value = Entity_Trans.UserID;
                pDeletedDate.Value = Entity_Trans.LoginDate;
                //    pIsDeleted.Value = Entity_damage.IsDeleted;

                SqlParameter[] Param = new SqlParameter[] { pAction, pTransId, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, Param);

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

          public DataSet GetTransferList(string RepCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                SqlParameter PrepCondition = new SqlParameter(TransferLocationOutside._strCond, SqlDbType.NVarChar);

                pAction.Value = 5;
                PrepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);

                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, pAction, PrepCondition);
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

          public DataSet GetRecordForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                SqlParameter pTransId = new SqlParameter(TransferLocationOutside._TransId, SqlDbType.BigInt);

                pAction.Value = 4;
                pTransId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, pAction, pTransId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

          public string[] GetSuggestRecord(string prefixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;
            try
            {
                //---For Checking Of Execution Of Procedure
                SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(TransferLocationOutside._strCond, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = prefixText;

                SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition };

                Open(CONNECTION_STRING);
                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, oparamcol);
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

          public DataSet GetItems(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                SqlParameter pId = new SqlParameter(TransferLocationOutside._CategoryId, SqlDbType.BigInt);

                pAction.Value = 9;
                pId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, pAction, pId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

          public DataSet GetCategory(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                SqlParameter pId = new SqlParameter(TransferLocationOutside._ItemId, SqlDbType.BigInt);

                pAction.Value = 11;
                pId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, pAction, pId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

          public DataSet GetQuantity(int ItemId, int LocationId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);
                SqlParameter pStockLocationID = new SqlParameter("@TransTo", SqlDbType.BigInt);

                pAction.Value = 10;
                pItemId.Value = ItemId;
                pStockLocationID.Value = LocationId;

                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId, pStockLocationID };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

          public DataSet GetRate(int ItemId, out string strError)
          {
              strError = string.Empty;
              DataSet Ds = new DataSet();
              try
              {
                  SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                  SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);
                  
                  pAction.Value = 13;
                  pItemId.Value = ItemId;
     
                  SqlParameter[] Param = new SqlParameter[] { pAction, pItemId};

                  Open(CONNECTION_STRING);
                  Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, Param);
              }
              catch (Exception ex)
              {
                  strError = ex.Message;
              }
              finally { Close(); }
              return Ds;

          }
          #region[Print Report]
            public DataSet BindForPrint(int TransId, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pTransId = new SqlParameter(TransferLocationOutside._TransId, SqlDbType.BigInt);

                pAction.Value = 12;
                pTransId.Value = TransId;

                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, pAction, pTransId);
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
        #endregion

          #region [For Report Summary and Details]----------------------------------------------------------
            public DataSet GetTransferForSummary(string RepCondition, out string strError, int actionno)
            {
                strError = string.Empty;
                DataSet Ds = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                    SqlParameter pRepCondition = new SqlParameter(TransferLocationOutside._strCond, SqlDbType.NVarChar);
                    if (actionno == 1)
                    {
                        pAction.Value = 2;
                    }
                    else
                    {
                        pAction.Value = 3;
                    }
                    pRepCondition.Value = RepCondition;
                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutsideReport, pAction, pRepCondition);
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                }
                finally { Close(); }
                return Ds;
            }
            public DataSet GetTransferForDetails(string RepCondition, out string strError, int actionno)
            {
                strError = string.Empty;
                DataSet Ds = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                    SqlParameter pRepCondition = new SqlParameter(TransferLocationOutside._strCond, SqlDbType.NVarChar);
                    if (actionno == 1)
                    {
                        pAction.Value = 4;
                    }
                    else
                    {
                        pAction.Value = 5;
                    }
                    pRepCondition.Value = RepCondition;
                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutsideReport, pAction, pRepCondition);
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                }
                finally { Close(); }
                return Ds;
            }
            public DataSet FillDamageComboForReport(out string StrError)
        {
            StrError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                pAction.Value = 1;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutsideReport, pAction);
            }
            catch (Exception ex)
            {
                StrError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        #endregion  [End Report Region Here]--------------------------------------------------------------


            public DataSet GetUnit(int ItemId,out string strError)
            {
                strError = string.Empty;
                DataSet Ds = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(TransferLocationOutside._Action, SqlDbType.BigInt);
                    SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);

                    pAction.Value = 14;
                    pItemId.Value = ItemId;

                    SqlParameter[] Param = new SqlParameter[] { pAction, pItemId };

                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocationOutside.SP_LocationTransferOutside, Param);
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                }
                finally { Close(); }
                return Ds;

            }

        #endregion

        public DMTransferLocationOutside()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
