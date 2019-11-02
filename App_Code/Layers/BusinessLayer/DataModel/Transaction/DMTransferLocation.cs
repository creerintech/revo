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
/// Summary description for DMTransferLocation
/// </summary>


namespace MayurInventory.DataModel 
{
    public class DMTransferLocation:Utility.Setting
    {
      
        public DataSet FillCombo(out string StrError)
        {
            DataSet DS = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);

                pAction.Value = 6;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, pAction);

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
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                pAction.Value = 7;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, pAction);


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

        public int InsertRecord(ref TransferLocation Entity_Trans,int LOCID, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pTransferNo= new SqlParameter(TransferLocation._TransNo, SqlDbType.NVarChar);
                SqlParameter pTransferDate = new SqlParameter(TransferLocation._Date, SqlDbType.DateTime);
                SqlParameter pTransferBy = new SqlParameter(TransferLocation._TransBy, SqlDbType.BigInt);
                SqlParameter pNotes= new SqlParameter(TransferLocation._Notes, SqlDbType.NVarChar);

                SqlParameter pCreatedBy = new SqlParameter(TransferLocation._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(TransferLocation._LoginDate, SqlDbType.DateTime);
                SqlParameter pLOCID = new SqlParameter("@LOCID", SqlDbType.BigInt);

                pAction.Value = 1;
                pTransferNo.Value = Entity_Trans.TransNo;
                pTransferDate.Value = Entity_Trans.Date;
                pTransferBy.Value = Entity_Trans.TransBy;
                pNotes.Value = Entity_Trans.Notes;
                pCreatedBy.Value = Entity_Trans.UserID;
                pCreatedDate.Value = Entity_Trans.LoginDate;
                pLOCID.Value = LOCID;

                SqlParameter[] Param = new SqlParameter[] { pAction, pTransferNo, pTransferDate, pTransferBy, pNotes, pCreatedBy, pCreatedDate,pLOCID };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, Param);

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

        public int InsertDetailsRecord(ref TransferLocation Entity_Trans, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pTransferId = new SqlParameter(TransferLocation._TransId, SqlDbType.BigInt);
                SqlParameter pCategoryId = new SqlParameter(TransferLocation._CategoryId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(TransferLocation._ItemId, SqlDbType.BigInt);
                SqlParameter pTransFrom = new SqlParameter(TransferLocation._TransFrom, SqlDbType.BigInt);
                SqlParameter pQtyAtSource = new SqlParameter(TransferLocation._QtyAtSource, SqlDbType.Decimal);
                SqlParameter pTransTo = new SqlParameter(TransferLocation._TransTo, SqlDbType.BigInt);
                SqlParameter pQtyAtDest = new SqlParameter(TransferLocation._QtyAtDest, SqlDbType.Decimal);
                SqlParameter pTransferqTY = new SqlParameter(TransferLocation._TransQty, SqlDbType.Decimal);
                SqlParameter prate = new SqlParameter(TransferLocation._rate, SqlDbType.Decimal);
                SqlParameter pUnitConversionId = new SqlParameter(TransferLocation._UnitConversionId, SqlDbType.BigInt);
                SqlParameter pItemDescID = new SqlParameter(TransferLocation._ItemDescID, SqlDbType.BigInt);
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
                pUnitConversionId.Value = Entity_Trans.UnitConversionId;
                pItemDescID.Value = Entity_Trans.ItemDescID;
                SqlParameter[] Param = new SqlParameter[] { pAction, pTransferId, pCategoryId, pTransFrom, pQtyAtSource, pQtyAtDest, pTransTo, pTransferqTY, pItemId, prate, pUnitConversionId,pItemDescID };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, Param);

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

        public int UpdateRecord(ref TransferLocation Entity_Trans, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pTransNo = new SqlParameter(TransferLocation._TransNo, SqlDbType.NVarChar);
                SqlParameter pDate = new SqlParameter(TransferLocation._Date, SqlDbType.DateTime);
                SqlParameter pTransBy = new SqlParameter(TransferLocation._TransBy, SqlDbType.BigInt);
                SqlParameter pNotes = new SqlParameter(TransferLocation._Notes, SqlDbType.NVarChar);
                SqlParameter pTransId = new SqlParameter(TransferLocation._TransId, SqlDbType.BigInt);
                SqlParameter pUpdatedBy = new SqlParameter(TransferLocation._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(TransferLocation._LoginDate, SqlDbType.DateTime);

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

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, Param);

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

        public int DeleteRecord(ref TransferLocation Entity_Trans, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pTransId = new SqlParameter(TransferLocation._TransId, SqlDbType.BigInt);

                SqlParameter pDeletedBy = new SqlParameter(TransferLocation._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(TransferLocation._LoginDate, SqlDbType.DateTime);


                pAction.Value = 3;
                pTransId.Value = Entity_Trans.TransId;

                pDeletedBy.Value = Entity_Trans.UserID;
                pDeletedDate.Value = Entity_Trans.LoginDate;
                //    pIsDeleted.Value = Entity_damage.IsDeleted;

                SqlParameter[] Param = new SqlParameter[] { pAction, pTransId, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, Param);

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

        public DataSet GetTransferList(string RepCondition,string COND, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter PrepCondition = new SqlParameter(TransferLocation._strCond, SqlDbType.NVarChar);
                SqlParameter PCOND = new SqlParameter("@COND", SqlDbType.NVarChar);
                pAction.Value = 5;
                PrepCondition.Value = RepCondition;
                PCOND.Value = COND;
                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] {pAction,PrepCondition,PCOND };
                DS = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, param);
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
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pTransId = new SqlParameter(TransferLocation._TransId, SqlDbType.BigInt);

                pAction.Value = 4;
                pTransId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, pAction, pTransId);
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
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(TransferLocation._strCond, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = prefixText;

                SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition };

                Open(CONNECTION_STRING);
                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, oparamcol);
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
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pId = new SqlParameter(TransferLocation._CategoryId, SqlDbType.BigInt);

                pAction.Value = 9;
                pId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, pAction, pId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetSubItems(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pId = new SqlParameter(TransferLocation._CategoryId, SqlDbType.BigInt);

                pAction.Value = 15;
                pId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, pAction, pId);

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
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pId = new SqlParameter(TransferLocation._ItemId, SqlDbType.BigInt);

                pAction.Value = 11;
                pId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, pAction, pId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetQuantity(int ItemId,int LocationId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);
                SqlParameter pStockLocationID = new SqlParameter("@TransTo", SqlDbType.BigInt);

                pAction.Value = 10;
                pItemId.Value = ItemId;
                pStockLocationID.Value = LocationId;

                SqlParameter[] Param = new SqlParameter[] { pAction,pItemId,pStockLocationID };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, Param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        #region Done By Sushma
        public DataSet GetRate(int ItemId, out string strError)
          {
              strError = string.Empty;
              DataSet Ds = new DataSet();
              try
              {
                  SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                  SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);
                  
                  pAction.Value = 13;
                  pItemId.Value = ItemId;
     
                  SqlParameter[] Param = new SqlParameter[] { pAction, pItemId};

                  Open(CONNECTION_STRING);
                  Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, Param);
              }
              catch (Exception ex)
              {
                  strError = ex.Message;
              }
              finally { Close(); }
              return Ds;

          }

        public DataSet GetUnit(int ItemId,decimal Qty, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);
                SqlParameter pQtyAtSource = new SqlParameter(TransferLocation._QtyAtSource, SqlDbType.Decimal);

                pAction.Value = 14;
                pItemId.Value = ItemId;
                pQtyAtSource.Value = Qty;
                
                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId,pQtyAtSource };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetItemDesc(int ItemId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);

                pAction.Value = 16;
                pItemId.Value = ItemId;

                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }


        public DataSet GetDescUnit(int IID,int ItemId,decimal QTY,int LOCID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter("@ItemDetailsId", SqlDbType.BigInt);
                SqlParameter pIId = new SqlParameter(TransferLocation._ItemId, SqlDbType.BigInt);
                SqlParameter pQtyAtSource = new SqlParameter(TransferLocation._QtyAtSource, SqlDbType.Decimal);
                SqlParameter pLOCID = new SqlParameter("@LOCID", SqlDbType.BigInt);

                pAction.Value = 17;
                pItemId.Value = ItemId;
                pIId.Value = IID;
                pQtyAtSource.Value = QTY;
                pLOCID.Value = LOCID;
                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId ,pIId,pQtyAtSource,pLOCID};

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
    
        #endregion
           #region[Print Report]
           public DataSet BindForPrint(int TransId, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pTransId = new SqlParameter(TransferLocation._TransId, SqlDbType.BigInt);

                pAction.Value = 12;
                pTransId.Value = TransId;

                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransfer, pAction, pTransId);
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
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(TransferLocation._strCond, SqlDbType.NVarChar);
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
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransferReport, pAction, pRepCondition);
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
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(TransferLocation._strCond, SqlDbType.NVarChar);
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
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransferReport, pAction, pRepCondition);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        public DataSet FillDamageComboForReport(int  COND,out string StrError)
        {
            StrError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TransferLocation._Action, SqlDbType.BigInt);
                SqlParameter pCOND = new SqlParameter("@UserID", SqlDbType.BigInt);
                pAction.Value = 1;
                pCOND.Value = COND;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TransferLocation.SP_LocationTransferReport, pAction,pCOND);
            }
            catch (Exception ex)
            {
                StrError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        #endregion  [End Report Region Here]--------------------------------------------------------------

        public DMTransferLocation()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
