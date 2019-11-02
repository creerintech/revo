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
/// <summary>
/// Summary description for DMReturn
/// </summary>
namespace MayurInventory.DataModel
{
    public class DMReturn : Utility.Setting
    {
        public DMReturn()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region[Business Logic]

        public int InsertRecord(ref EReturn Entity_damage,int LOCID, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(EReturn._Action, SqlDbType.BigInt);
                SqlParameter pReturnDate = new SqlParameter(EReturn._ReturnDate, SqlDbType.DateTime);
                SqlParameter pPreparedBy = new SqlParameter(EReturn._PreparedBy, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(EReturn._InwardId, SqlDbType.BigInt);
                SqlParameter pCreatedBy = new SqlParameter(EReturn._UserId, SqlDbType.BigInt);
                SqlParameter pIsDebitNote = new SqlParameter(EReturn._IsDebitNote, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(EReturn._LoginDate, SqlDbType.DateTime);
                SqlParameter pLOCID = new SqlParameter("@LOCID", SqlDbType.BigInt);

                pAction.Value = 1;
                pReturnDate.Value = Entity_damage.ReturnDate;
                pPreparedBy.Value = Entity_damage.PreparedBy;
                pInwardId.Value = Entity_damage.InwardId;
                pCreatedBy.Value = Entity_damage.UserID;
                pCreatedDate.Value = Entity_damage.LoginDate;
                pLOCID.Value = LOCID;
                pIsDebitNote.Value = Entity_damage.IsDebitNote;
                SqlParameter[] Param = new SqlParameter[] { pAction, pReturnDate, pPreparedBy, pInwardId, pCreatedBy, pCreatedDate, pLOCID, pIsDebitNote };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMaster, Param);

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

        public int UpdateRecord(ref EReturn Entity_damage, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(EReturn._Action, SqlDbType.BigInt);
                SqlParameter pPreparedBy = new SqlParameter(EReturn._PreparedBy, SqlDbType.BigInt);
                SqlParameter pReturnId = new SqlParameter(EReturn._ReturnId, SqlDbType.BigInt);
                SqlParameter pUpdatedBy = new SqlParameter(EReturn._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(EReturn._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDebitNote = new SqlParameter(EReturn._IsDebitNote, SqlDbType.BigInt);
                pAction.Value = 2;
                pPreparedBy.Value = Entity_damage.PreparedBy;
                pReturnId.Value = Entity_damage.ReturnId;
                pUpdatedBy.Value = Entity_damage.UserID;
                pUpdatedDate.Value = Entity_damage.LoginDate;
                pIsDebitNote.Value = Entity_damage.IsDebitNote;
                SqlParameter[] Param = new SqlParameter[] { pAction,  pPreparedBy, pReturnId,  pUpdatedBy, pUpdatedDate,pIsDebitNote };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMaster, Param);

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

        public int DeleteRecord(ref EReturn Entity_damage, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(EReturn._Action, SqlDbType.BigInt);
                SqlParameter pReturnId = new SqlParameter(EReturn._ReturnId, SqlDbType.BigInt);

                SqlParameter pDeletedBy = new SqlParameter(EReturn._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(EReturn._LoginDate, SqlDbType.DateTime);


                pAction.Value = 3;
                pReturnId.Value = Entity_damage.ReturnId;

                pDeletedBy.Value = Entity_damage.UserID;
                pDeletedDate.Value = Entity_damage.LoginDate;
                //    pIsDeleted.Value = Entity_damage.IsDeleted;

                SqlParameter[] Param = new SqlParameter[] { pAction, pReturnId, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMaster, Param);

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

        public int InsertDetailsRecord(ref EReturn Entity_damage, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(EReturn._Action, SqlDbType.BigInt);
                SqlParameter pReturnId = new SqlParameter(EReturn._ReturnId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(EReturn._ItemId, SqlDbType.BigInt);
                SqlParameter pInwardQty = new SqlParameter(EReturn._InwardQty, SqlDbType.Decimal);
                SqlParameter pInwardRate = new SqlParameter(EReturn._InwardRate, SqlDbType.Decimal);
                SqlParameter pDamageQty = new SqlParameter(EReturn._DamageQty, SqlDbType.Decimal);
                SqlParameter pPrevReturnQty = new SqlParameter(EReturn._PrevReturnQty, SqlDbType.Decimal);
                SqlParameter pReturnQty = new SqlParameter(EReturn._ReturnQty, SqlDbType.Decimal);
                SqlParameter pReason = new SqlParameter(EReturn._Reason, SqlDbType.NVarChar);
                SqlParameter pItemDesc = new SqlParameter(EReturn._ItemDesc, SqlDbType.NVarChar);
                SqlParameter pIsDebitNote = new SqlParameter(EReturn._IsDebitNote, SqlDbType.BigInt);
                //StockDetails
                SqlParameter pStockUnitId = new SqlParameter(EReturn._StockUnitId, SqlDbType.BigInt);
                SqlParameter pStockDate = new SqlParameter(EReturn._StockDate, SqlDbType.DateTime);
                SqlParameter pStockLocationID = new SqlParameter(EReturn._StockLocationID, SqlDbType.BigInt);

                pAction.Value = 7;
                pReturnId.Value = Entity_damage.ReturnId;
                pItemId.Value = Entity_damage.ItemId;
                pIsDebitNote.Value = Entity_damage.IsDebitNote;
                pInwardQty.Value = Entity_damage.InwardQty;
                pInwardRate.Value = Entity_damage.InwardRate;
                pDamageQty.Value = Entity_damage.DamageQty;
                pPrevReturnQty.Value = Entity_damage.PrevReturnQty;
                pReturnQty.Value = Entity_damage.ReturnQty;
                pReason.Value = Entity_damage.Reason;
                pItemDesc.Value = Entity_damage.ItemDesc;
                //StockDetails
                pStockUnitId.Value = Entity_damage.StockUnitId;
                pStockDate.Value = Entity_damage.StockDate;
                pStockLocationID.Value = Entity_damage.StockLocationID;

                SqlParameter[] Param = new SqlParameter[] { pAction, pReturnId, pItemId, pInwardQty, pInwardRate, pDamageQty,pPrevReturnQty,pReturnQty, pReason,
                        pStockUnitId,pStockDate,pStockLocationID ,pItemDesc,pIsDebitNote};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMaster, Param);

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

        public DataSet FillCombo(string COND,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);
               // SqlParameter pCOND = new SqlParameter("@JID", SqlDbType.BigInt);

                pAction.Value = 6;
                pCOND.Value = COND;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMaster, pAction,pCOND);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet FillComboONCOND(string COND, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);

                pAction.Value = 10;
                pCOND.Value = COND;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMaster, pAction, pCOND);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetInwardDtls(int InwardId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EReturn._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(EReturn._InwardId, SqlDbType.BigInt);

                pAction.Value = 8;
                pInwardId.Value = InwardId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMaster, pAction, pInwardId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetReturnDetails(string RepCondition,string COND, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(StockMaster._strCond, SqlDbType.NVarChar);
                SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);
                pAction.Value = 5;
                pRepCondition.Value = RepCondition;
                pCOND.Value = COND;
                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] {pAction,pRepCondition,pCOND };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMaster, param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetRecordForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EReturn._Action, SqlDbType.BigInt);
                SqlParameter pReturnId = new SqlParameter(EReturn._ReturnId, SqlDbType.BigInt);

                pAction.Value = 4;
                pReturnId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMaster, pAction, pReturnId);
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
                SqlParameter MAction = new SqlParameter(EReturn._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(EReturn._strCond, SqlDbType.NVarChar);

                MAction.Value = 5;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMaster, oParmCol);

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

        public DataSet ChkInwardIdExit(Int32 InwardId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EReturn._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(EReturn._InwardId, SqlDbType.BigInt);

                pAction.Value = 11;
                pInwardId.Value = InwardId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMaster, pAction, pInwardId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet Fill_Items(int CategoryId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pCategoryId = new SqlParameter("@CategoryId", SqlDbType.BigInt);

                pAction.Value = 6;
                pCategoryId.Value = CategoryId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMasterReport, pAction, pCategoryId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet Fill_AllItem(out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);

                pAction.Value = 7;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMasterReport, pAction);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet Fill_Items_Details(int ItemId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);

                pAction.Value = 8;
                pItemId.Value = ItemId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMasterReport, pAction, pItemId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        #region [Report section For Summary and details]-------------------------
        public DataSet GetReturnForSummary(string RepCondition, out string strError, int actionno)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EReturn._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(EReturn._strCond, SqlDbType.NVarChar);
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
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMasterReport, pAction, pRepCondition);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        public DataSet GetReturnForReport(string RepCondition, out string strError, int actionno)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EReturn._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(EReturn._strCond, SqlDbType.NVarChar);
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
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMasterReport, pAction, pRepCondition);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        public DataSet FillReturnComboForReport(out string StrError)
        {
            StrError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EReturn._Action, SqlDbType.BigInt);
                pAction.Value = 1;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMasterReport, pAction);
            }
            catch (Exception ex)
            {
                StrError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        #endregion [End Report Section]------------------------------------------

        #region[Report]
        public DataSet BindForPrint(int ReturnId, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pReturnId = new SqlParameter(EReturn._ReturnId, SqlDbType.BigInt);

                pAction.Value = 9;
                pReturnId.Value = ReturnId;

                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EReturn.SP_ReturnMaster, pAction, pReturnId);
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

        #endregion
    }
}
