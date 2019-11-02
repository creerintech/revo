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
    public class DMDamage:Utility.Setting
    {
        public DMDamage()
        {
            //
            // TODO: Add constructor logic here
            //
        }
 
        #region[Business Logic]

            public int InsertRecord(ref Damage Entity_damage,int LOCID, out string strError)
            {
                int iInsert = 0;
                strError = string.Empty;
                try
                {
                    SqlParameter pAction = new SqlParameter(Damage._Action, SqlDbType.BigInt);
                    SqlParameter pDamageNo = new SqlParameter(Damage._DamageNo, SqlDbType.NVarChar);
                    SqlParameter pDamageDate = new SqlParameter(Damage._DamageDate, SqlDbType.DateTime);
                    SqlParameter pPreparedBy = new SqlParameter(Damage._PreparedBy, SqlDbType.BigInt);
                    SqlParameter pInwardId = new SqlParameter(Damage._InwardId, SqlDbType.BigInt);
                    SqlParameter pDamagedThrough = new SqlParameter(Damage._DamagedThrough, SqlDbType.BigInt);
                    SqlParameter pDebitNote = new SqlParameter(Damage._DebitNote, SqlDbType.BigInt);
                    SqlParameter pLOCID = new SqlParameter("LOCID", SqlDbType.BigInt);
                    SqlParameter pCreatedBy = new SqlParameter(Damage._UserId, SqlDbType.BigInt);
                    SqlParameter pCreatedDate = new SqlParameter(Damage._LoginDate, SqlDbType.DateTime);

                    pAction.Value = 1;
                    pDamageNo.Value = Entity_damage.DamageNo;
                    pDamageDate.Value = Entity_damage.DamageDate;
                    pPreparedBy.Value = Entity_damage.PreparedBy;
                    pInwardId.Value = Entity_damage.InwardId;
                    pDamagedThrough.Value = Entity_damage.DamagedThrough;
                    pDebitNote.Value = Entity_damage.DebitNote;
                    pLOCID.Value = LOCID;
                    pCreatedBy.Value = Entity_damage.UserID;
                    pCreatedDate.Value = Entity_damage.LoginDate;

                    SqlParameter[] Param = new SqlParameter[] { pAction, pDamageNo, pDamageDate, pPreparedBy, pInwardId, pDamagedThrough, pDebitNote,pCreatedBy, pCreatedDate,pLOCID };

                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMaster1, Param);

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

            public int UpdateRecord(ref Damage Entity_damage, out string strError)
            {
                int iInsert = 0;
                strError = string.Empty;
                try
                {
                    SqlParameter pAction = new SqlParameter(Damage._Action, SqlDbType.BigInt);
                    SqlParameter pDamageNo = new SqlParameter(Damage._DamageNo, SqlDbType.NVarChar);
                    SqlParameter pDamageDate = new SqlParameter(Damage._DamageDate, SqlDbType.DateTime);
                    SqlParameter pPreparedBy = new SqlParameter(Damage._PreparedBy, SqlDbType.BigInt);
                    //SqlParameter pInwardId = new SqlParameter(Damage._InwardId, SqlDbType.BigInt);
                    SqlParameter pDamagedThrough = new SqlParameter(Damage._DamagedThrough, SqlDbType.BigInt);
                    SqlParameter pDamageId = new SqlParameter(Damage._DamageId, SqlDbType.BigInt);
                    SqlParameter pDebitNote = new SqlParameter(Damage._DebitNote, SqlDbType.BigInt);

                    SqlParameter pUpdatedBy = new SqlParameter(Damage._UserId, SqlDbType.BigInt);
                    SqlParameter pUpdatedDate = new SqlParameter(Damage._LoginDate, SqlDbType.DateTime);

                    pAction.Value = 2;
                    pDamageNo.Value = Entity_damage.DamageNo;
                    pDamageDate.Value = Entity_damage.DamageDate;
                    pPreparedBy.Value = Entity_damage.PreparedBy;
                    //pInwardId.Value = Entity_damage.InwardId;
                    pDamagedThrough.Value = Entity_damage.DamagedThrough;
                    pDamageId.Value = Entity_damage.DamageId;
                    pDebitNote.Value = Entity_damage.DebitNote;

                    pUpdatedBy.Value = Entity_damage.UserID;
                    pUpdatedDate.Value = Entity_damage.LoginDate;

                    SqlParameter[] Param = new SqlParameter[] { pAction, pDamageNo, pDamageDate, pPreparedBy, pDamageId, pDamagedThrough, pDebitNote, pUpdatedBy, pUpdatedDate };

                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMaster1, Param);

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

            public int DeleteRecord(ref Damage Entity_damage, out string strError)
            {
                int iDelete = 0;
                strError = string.Empty;
                try
                {
                    SqlParameter pAction = new SqlParameter(Damage._Action, SqlDbType.BigInt);
                    SqlParameter pDamageId = new SqlParameter(Damage._DamageId, SqlDbType.BigInt);

                    SqlParameter pDeletedBy = new SqlParameter(Damage._UserId, SqlDbType.BigInt);
                    SqlParameter pDeletedDate = new SqlParameter(Damage._LoginDate, SqlDbType.DateTime);


                    pAction.Value = 3;
                    pDamageId.Value = Entity_damage.DamageId;

                    pDeletedBy.Value = Entity_damage.UserID;
                    pDeletedDate.Value = Entity_damage.LoginDate;
                    //    pIsDeleted.Value = Entity_damage.IsDeleted;

                    SqlParameter[] Param = new SqlParameter[] { pAction, pDamageId, pDeletedBy, pDeletedDate };

                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMaster1, Param);

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

            public int InsertDetailsRecord(ref Damage Entity_damage, out string strError)
            {
                int iInsert = 0;
                strError = string.Empty;
                try
                {
                    SqlParameter pAction = new SqlParameter(Damage._Action, SqlDbType.BigInt);

                    SqlParameter pDamageId = new SqlParameter(Damage._DamageId, SqlDbType.BigInt);
                    SqlParameter pItemId = new SqlParameter(Damage._ItemId, SqlDbType.BigInt);
                    SqlParameter pInwardQty = new SqlParameter(Damage._InwardQty, SqlDbType.Decimal);
                    SqlParameter pInwardRate = new SqlParameter(Damage._InwardRate, SqlDbType.Decimal);
                    SqlParameter pDamageQty = new SqlParameter(Damage._DamageQty, SqlDbType.Decimal);
                    SqlParameter pReason = new SqlParameter(Damage._Reason, SqlDbType.NVarChar);
                    SqlParameter pConversionUnitId = new SqlParameter(Damage._ConversionUnitId, SqlDbType.BigInt);
                    //ItemDesc
                    SqlParameter pItemDesc = new SqlParameter(Damage._ItemDesc, SqlDbType.NVarChar);
                    //StockDetails
                    SqlParameter pStockUnitId = new SqlParameter(Damage._StockUnitId, SqlDbType.BigInt);
                    SqlParameter pStockDate = new SqlParameter(Damage._StockDate, SqlDbType.DateTime);
                    SqlParameter pStockLocationID = new SqlParameter(Damage._StockLocationID, SqlDbType.BigInt);

                    pAction.Value = 7;
                    pDamageId.Value = Entity_damage.DamageId;
                    pItemId.Value = Entity_damage.ItemId;
                    pInwardQty.Value = Entity_damage.InwardQty;
                    pInwardRate.Value = Entity_damage.InwardRate;
                    pDamageQty.Value = Entity_damage.DamageQty;
                    pReason.Value = Entity_damage.Reason;
                    pConversionUnitId.Value = Entity_damage.ConversionUnitId;
                    pItemDesc.Value = Entity_damage.ItemDesc;
                    //StockDetails
                    pStockUnitId.Value = Entity_damage.StockUnitId;
                    pStockDate.Value = Entity_damage.StockDate;
                    pStockLocationID.Value = Entity_damage.StockLocationID;

                    SqlParameter[] Param = new SqlParameter[] { pAction, pDamageId, pItemId, pInwardQty, pInwardRate, pDamageQty, pReason,
                        pConversionUnitId,pItemDesc,pStockUnitId,pStockDate,pStockLocationID };

                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMaster1, Param);

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
                    SqlParameter pCOND = new SqlParameter("COND", SqlDbType.NVarChar);
                    pAction.Value = 6;
                    pCOND.Value = COND;
                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMaster1, pAction,pCOND);
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                }
                finally { Close(); }
                return Ds;
            }

            public DataSet GetInwardDtls(int InwardId,string COND, out string strError)
            {
                strError = string.Empty;
                DataSet Ds = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(Damage._Action, SqlDbType.BigInt);
                    SqlParameter pInwardId = new SqlParameter(Damage._InwardId, SqlDbType.BigInt);
                    SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);
                    pAction.Value = 8;
                    pInwardId.Value = InwardId;
                    pCOND.Value = COND;
                    Open(CONNECTION_STRING);
                    SqlParameter[] param = new SqlParameter[] {pAction,pInwardId,pCOND };
                    Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMaster1, param);
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                }
                finally { Close(); }
                return Ds;

            }

            public DataSet GetDamageDetails(string RepCondition,string COND, out string strError)
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
                    Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMaster1, param);

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
                    SqlParameter pAction = new SqlParameter(Damage._Action, SqlDbType.BigInt);
                    SqlParameter pDamageId = new SqlParameter(Damage._DamageId, SqlDbType.BigInt);

                    pAction.Value = 4;
                    pDamageId.Value = ID;

                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMaster1, pAction, pDamageId);
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
                    SqlParameter MAction = new SqlParameter(Damage._Action, SqlDbType.VarChar);
                    SqlParameter MRepCondition = new SqlParameter(Damage._strCond, SqlDbType.NVarChar);

                    MAction.Value = 5;
                    MRepCondition.Value = prefixText;

                    SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                    Open(Setting.CONNECTION_STRING);

                    SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMaster1, oParmCol);

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
                    SqlParameter pAction = new SqlParameter(Damage._Action, SqlDbType.BigInt);
                    SqlParameter pInwardId = new SqlParameter(Damage._InwardId, SqlDbType.BigInt);

                    pAction.Value = 11;
                    pInwardId.Value = InwardId;

                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMaster1, pAction, pInwardId);

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
                    Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMasterReport, pAction, pCategoryId);

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
                    Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMasterReport, pAction);

                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                }
                finally { Close(); }
                return Ds;
            }

            public DataSet Fill_Items_Details(int ItemId, int LocationId,int DtlsId, out string strError)
            {
                strError = string.Empty;
                DataSet Ds = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                    SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);
                    SqlParameter pLocationId = new SqlParameter("@LocationId", SqlDbType.BigInt);
                    SqlParameter pItemDtlsId = new SqlParameter("@ItemDtlsId", SqlDbType.BigInt);

                    pAction.Value = 8;
                    pItemId.Value = ItemId;
                    pLocationId.Value = LocationId;
                    pItemDtlsId.Value = DtlsId;
                    SqlParameter[] Param = new SqlParameter[] { pAction, pItemId, pLocationId,pItemDtlsId };

                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMasterReport,Param);
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                }
                finally { Close(); }
                return Ds;
            }

            public DataSet Fill_SupplierRate(int ItemId, out string strError)
            {
                strError = string.Empty;
                DataSet Ds = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                    SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);

                    pAction.Value = 9;
                    pItemId.Value = ItemId;

                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMasterReport, pAction, pItemId);

                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                }
                finally { Close(); }
                return Ds;
            }

            #region [Report section For Summary and details]-------------------------
            public DataSet GetDamageForSummary(string RepCondition, out string strError, int actionno)
            {
                strError = string.Empty;
                DataSet Ds = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(Damage._Action, SqlDbType.BigInt);
                    SqlParameter pRepCondition = new SqlParameter(Damage._strCond, SqlDbType.NVarChar);
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
                    Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMasterReport, pAction, pRepCondition);
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                }
                finally { Close(); }
                return Ds;
            }
            public DataSet GetDamageForReport(string RepCondition, out string strError, int actionno)
            {
                strError = string.Empty;
                DataSet Ds = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(Damage._Action, SqlDbType.BigInt);
                    SqlParameter pRepCondition = new SqlParameter(Damage._strCond, SqlDbType.NVarChar);
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
                    Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMasterReport, pAction, pRepCondition);
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                }
                finally { Close(); }
                return Ds;
            }
            public DataSet FillDamageComboForReport(int UserId, out string StrError)
            {
                StrError = string.Empty;
                DataSet Ds = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(Damage._Action, SqlDbType.BigInt);
                    SqlParameter pUserId = new SqlParameter("@userId", SqlDbType.BigInt);
                    pAction.Value = 1;
                    pUserId.Value = UserId;
                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMasterReport, pAction,pUserId);
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
            public DataSet BindForPrint(int DamageId, out string strError)
            {
                DataSet ds = new DataSet();
                strError = string.Empty;
                try
                {
                    SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                    SqlParameter pDamageId = new SqlParameter(Damage._DamageId, SqlDbType.BigInt);

                    pAction.Value = 9;
                    pDamageId.Value = DamageId;

                    Open(CONNECTION_STRING);
                    ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMaster1, pAction, pDamageId);
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
            public DataSet BindForPrintDebitNote(int DamageId, out string strError)
            {
                DataSet ds = new DataSet();
                strError = string.Empty;
                try
                {
                    SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                    SqlParameter pDamageId = new SqlParameter(Damage._DamageId, SqlDbType.BigInt);

                    pAction.Value = 10;
                    pDamageId.Value = DamageId;

                    Open(CONNECTION_STRING);
                    ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Damage.SP_DamageMaster1, pAction, pDamageId);
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
