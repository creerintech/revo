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

 
    public class DMStockMaster:Utility.Setting
    {
        public DMStockMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet ChkInwardExit(int InwardID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(StockMaster._InwardId, SqlDbType.BigInt);

                pAction.Value = 11;
                pInwardId.Value = InwardID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, pAction, pInwardId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet getAvlQty(int ID,int cafeId,int UnitID,decimal Qty,string DESC, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemID = new SqlParameter(StockMaster._ItemId, SqlDbType.BigInt);
                SqlParameter pStockLocationId = new SqlParameter(StockMaster._StockLocationId, SqlDbType.BigInt);
                SqlParameter pUnitConvDtlsId = new SqlParameter(StockMaster._UnitConvDtlsId, SqlDbType.BigInt);
                SqlParameter pInwardQty = new SqlParameter(StockMaster._InwardQty, SqlDbType.Decimal);
                SqlParameter pItemDesc = new SqlParameter(StockMaster._ItemDesc, SqlDbType.NVarChar);
                pAction.Value = 14;
                pItemID.Value=ID;
                pStockLocationId.Value = cafeId;
                pUnitConvDtlsId.Value = UnitID;
                pInwardQty.Value = Qty;
                pItemDesc.Value = DESC;
                SqlParameter[] Param = new SqlParameter[] { pAction, pItemID, pStockLocationId, pUnitConvDtlsId,pInwardQty ,pItemDesc};
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, Param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet getAvlQtyItemWise(int ID, int cafeId, int UnitID, decimal Qty, string DESC, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemID = new SqlParameter(StockMaster._ItemId, SqlDbType.BigInt);
                SqlParameter pStockLocationId = new SqlParameter(StockMaster._StockLocationId, SqlDbType.BigInt);
                SqlParameter pUnitConvDtlsId = new SqlParameter(StockMaster._UnitConvDtlsId, SqlDbType.BigInt);
                SqlParameter pInwardQty = new SqlParameter(StockMaster._InwardQty, SqlDbType.Decimal);
                SqlParameter pItemDesc = new SqlParameter(StockMaster._ItemDesc, SqlDbType.NVarChar);
                pAction.Value = 20;
                pItemID.Value = ID;
                pStockLocationId.Value = cafeId;
                pUnitConvDtlsId.Value = UnitID;
                pInwardQty.Value = Qty;
                pItemDesc.Value = DESC;
                SqlParameter[] Param = new SqlParameter[] { pAction, pItemID, pStockLocationId, pUnitConvDtlsId, pInwardQty, pItemDesc };
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, Param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }


        public DataSet GetDetailOnCond(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(StockMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 9;
           
                pRepCondition.Value = RepCondition;

                 SqlParameter[] Param = new SqlParameter[] { pAction, pRepCondition};
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster,Param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetDetailOnCondForRequisitn(string RepCondition,int LocationId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(StockMaster._strCond, SqlDbType.NVarChar);
                SqlParameter pLocationId = new SqlParameter("@LocationId", SqlDbType.NVarChar);

                pAction.Value = 17;
                pRepCondition.Value = RepCondition;
                pLocationId.Value = LocationId;

                SqlParameter[] Param = new SqlParameter[] { pAction, pRepCondition,pLocationId };
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster1, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetDetailOnCondForRequisitnPresent(string RepCondition, string LocationId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(StockMaster._strCond, SqlDbType.NVarChar);
                SqlParameter pLocationId = new SqlParameter("@Status", SqlDbType.NVarChar);

                pAction.Value = 20;
                pRepCondition.Value = RepCondition;
                pLocationId.Value = LocationId;

                SqlParameter[] Param = new SqlParameter[] { pAction, pRepCondition, pLocationId };
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster1, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetDetailOnCondForItem(int CategoryId,String Desc, out string strError, string ac)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                if (ac == "I")
                {
                    SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                    pAction.Value = 12;
                    SqlParameter pRepCondition = new SqlParameter(StockMaster._ItemId, SqlDbType.NVarChar);
                    SqlParameter pDesc = new SqlParameter("@ItemDesc", SqlDbType.NVarChar);
                    pRepCondition.Value = CategoryId;
                    pDesc.Value = Desc;
                    SqlParameter[] Param = new SqlParameter[] { pAction, pRepCondition, pDesc };
                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, Param);
                }
                if (ac == "C")
                {
                    SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                    pAction.Value = 13;
                    SqlParameter pRepCondition = new SqlParameter(StockMaster._CategoryId, SqlDbType.NVarChar);                    
                    pRepCondition.Value = CategoryId;
                    SqlParameter[] Param = new SqlParameter[] { pAction, pRepCondition };
                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, Param);                    
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        public DataSet GetDetailForExitId(int inwardid, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(StockMaster._strCond, SqlDbType.NVarChar);
                pAction.Value = 10;
                pRepCondition.Value = inwardid;
                SqlParameter[] Param = new SqlParameter[] { pAction, pRepCondition };
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, Param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        public DataSet GetStockNo(out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                pAction.Value = 6;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, pAction);
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

        public DataSet GetStock(string RepCondition,string COND, out string strError)
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
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet FillCombo(string COND,int UserId,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);
                SqlParameter pUserId= new SqlParameter(StockMaster._UserId, SqlDbType.NVarChar);
                pAction.Value = 7;
                pCOND.Value = COND;
                pUserId.Value = UserId;

                SqlParameter[] param = new SqlParameter[] {pAction,pCOND,pUserId }; 
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }


        public DataSet FillComboRequisition(string FromCond, string ToCond, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pFromCond = new SqlParameter("@FromCond", SqlDbType.NVarChar);
                SqlParameter pToCond = new SqlParameter("@ToCond", SqlDbType.NVarChar);

                pAction.Value = 16;
                pFromCond.Value = FromCond;
                pToCond.Value = ToCond;

                Open(CONNECTION_STRING);
                SqlParameter[] Param = new SqlParameter[] { pAction, pFromCond,pToCond };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }


        public DataSet FillComboItemCategory(string STRCOND, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pFromCond = new SqlParameter("@FromCond", SqlDbType.NVarChar);
                pAction.Value = 17;
                pFromCond.Value = STRCOND;
                Open(CONNECTION_STRING);
                SqlParameter[] Param = new SqlParameter[] { pAction, pFromCond};
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        public DataSet FillComboItemSUBCategory(string STRCOND, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pFromCond = new SqlParameter("@FromCond", SqlDbType.NVarChar);
                pAction.Value = 18;
                pFromCond.Value = STRCOND;
                Open(CONNECTION_STRING);
                SqlParameter[] Param = new SqlParameter[] { pAction, pFromCond };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet Fill_Discription(int ItemId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);

                pAction.Value = 19;
                pItemId.Value = ItemId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, pAction, pItemId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public int InsertRecord(ref StockMaster Entity_StockMaster,Int32 LOCID, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pStockNo = new SqlParameter(StockMaster._StockNo, SqlDbType.NVarChar);
                SqlParameter pStockDate = new SqlParameter(StockMaster._StockAsOn, SqlDbType.DateTime);
                SqlParameter pConsumptionDate = new SqlParameter(StockMaster._ConsumptionDate, SqlDbType.DateTime);
                SqlParameter pType = new SqlParameter(StockMaster._Type, SqlDbType.Char);
             
                SqlParameter pCreatedBy = new SqlParameter(PurchaseOrder._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(PurchaseOrder._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(PurchaseOrder._IsDeleted, SqlDbType.Bit);
                SqlParameter pLOCID = new SqlParameter("@LOCID", SqlDbType.BigInt);
                SqlParameter pRemark = new SqlParameter("@Remark", SqlDbType.NVarChar);

                pAction.Value = 1;
                pStockNo.Value = Entity_StockMaster.StockNo;
                pStockDate.Value = Entity_StockMaster.StockAsOn;
                pConsumptionDate.Value = Entity_StockMaster.ConsumptionDate;
                pType.Value = Entity_StockMaster.Type;
                pCreatedBy.Value = Entity_StockMaster.UserId;
                pCreatedDate.Value = Entity_StockMaster.LoginDate;
                pIsDeleted.Value = Entity_StockMaster.IsDeleted;
                pLOCID.Value = LOCID;
                pRemark.Value = Entity_StockMaster.Remark;
                SqlParameter[] Param = new SqlParameter[] { pAction, pStockNo, pConsumptionDate, pStockDate, pType, pCreatedBy, pCreatedDate, pIsDeleted, pLOCID, pRemark };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, Param);

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

        public int InsertDetailsRecord(ref StockMaster Entity_StockMaster, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
             
                SqlParameter pOutwardId = new SqlParameter(StockMaster._OutwardId, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(StockMaster._InwardId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(StockMaster._ItemId, SqlDbType.BigInt);
                SqlParameter pInwardQty = new SqlParameter(StockMaster._InwardQty, SqlDbType.Decimal);
                SqlParameter pOutwardQty = new SqlParameter(StockMaster._OutwardQty, SqlDbType.Decimal);
                SqlParameter pPendingQty = new SqlParameter(StockMaster._PendingQty, SqlDbType.Decimal);
                SqlParameter pLocationId= new SqlParameter(StockMaster._LocationId, SqlDbType.BigInt);
                SqlParameter pStockLocationId = new SqlParameter(StockMaster._StockLocationId, SqlDbType.BigInt);
                SqlParameter pRate = new SqlParameter(StockMaster._Rate, SqlDbType.Decimal);
                SqlParameter pAmount = new SqlParameter(StockMaster._Amount, SqlDbType.Decimal);
                SqlParameter pUnitConversion = new SqlParameter(StockMaster._UnitConvDtlsId,SqlDbType.BigInt);
                SqlParameter pItemDetailsId = new SqlParameter(StockMaster._ItemDetailsId, SqlDbType.BigInt);
                SqlParameter pItemDesc = new SqlParameter(StockMaster._ItemDesc, SqlDbType.NVarChar);

                pAction.Value = 8;
                pOutwardId.Value = Entity_StockMaster.OutwardId;
                pInwardId.Value = Entity_StockMaster.InwardId;
                pItemId.Value = Entity_StockMaster.ItemId;
                pInwardQty.Value = Entity_StockMaster.InwardQty;
                pOutwardQty.Value = Entity_StockMaster.OutwardQty;
                pPendingQty.Value = Entity_StockMaster.PendingQty;
                pLocationId.Value = Entity_StockMaster.LocationId;
                pStockLocationId.Value = Entity_StockMaster.StockLocationID;
                pRate.Value = Entity_StockMaster.Rate;
                pAmount.Value = Entity_StockMaster.Amount;
                pUnitConversion.Value = Entity_StockMaster.UnitConvDtlsId;
                pItemDetailsId.Value = Entity_StockMaster.ItemDetailsId;
                pItemDesc.Value = Entity_StockMaster.ItemDesc;

                SqlParameter[] Param = new SqlParameter[] { pAction, pOutwardId, pItemId, pInwardId, pInwardQty, pOutwardQty, pPendingQty, pLocationId, pStockLocationId, pRate, pAmount, pUnitConversion, pItemDetailsId, pItemDesc };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, Param);

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

        public int UpdateRecord(ref StockMaster Entity_StockMaster, out string strError)
        {

            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pStockNo = new SqlParameter(StockMaster._StockNo, SqlDbType.NVarChar);
                SqlParameter pStockDate = new SqlParameter(StockMaster._StockAsOn, SqlDbType.DateTime);
                SqlParameter pConsumptionDate = new SqlParameter(StockMaster._ConsumptionDate, SqlDbType.DateTime);
                SqlParameter pOutwardId = new SqlParameter(StockMaster._OutwardId, SqlDbType.BigInt);
                SqlParameter pUpdatedBy = new SqlParameter(StockMaster._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(StockMaster._LoginDate, SqlDbType.DateTime);
                SqlParameter pRemark = new SqlParameter("@Remark", SqlDbType.NVarChar);


                pAction.Value = 2;
                pStockNo.Value = Entity_StockMaster.StockNo;
                pStockDate.Value = Entity_StockMaster.StockAsOn;
                pConsumptionDate.Value = Entity_StockMaster.ConsumptionDate;
                pOutwardId.Value = Entity_StockMaster.OutwardId;
                pUpdatedBy.Value = Entity_StockMaster.UserId;
                pUpdatedDate.Value = Entity_StockMaster.LoginDate;
                pRemark.Value = Entity_StockMaster.Remark;

                SqlParameter[] Param = new SqlParameter[] { pAction,pStockNo ,pStockDate,pConsumptionDate,pOutwardId,pUpdatedBy, pUpdatedDate,pRemark };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, Param);

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

        public int DeleteRecord(ref StockMaster Entity_StockMaster, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pOutwardId = new SqlParameter(StockMaster._OutwardId, SqlDbType.BigInt);

                SqlParameter pDeletedBy = new SqlParameter(StockMaster._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(StockMaster._LoginDate, SqlDbType.DateTime);
              
                pAction.Value = 3;
                pOutwardId.Value = Entity_StockMaster.OutwardId;

                pDeletedBy.Value = Entity_StockMaster.UserId;
                pDeletedDate.Value = Entity_StockMaster.LoginDate;


                SqlParameter[] Param = new SqlParameter[] { pAction, pOutwardId, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, Param);

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

        public DataSet GetRecordForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pOutwardId = new SqlParameter(StockMaster._OutwardId, SqlDbType.BigInt);

                pAction.Value = 4;
                pOutwardId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, pAction, pOutwardId);
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
                SqlParameter MAction = new SqlParameter(StockMaster._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(StockMaster._strCond, SqlDbType.NVarChar);

                MAction.Value = 5;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, oParmCol);

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
        public DataSet GetStockForPrint(int ID, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter(StockMaster._OutwardId, SqlDbType.BigInt);
                pAction.Value = 15;
                pRequisitionCafeId.Value = ID;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster, pAction, pRequisitionCafeId);
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
        public int UpdateAssignFlag(ref StockMaster Entity_StockMaster, out string strError)
        {

            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(StockMaster._InwardId, SqlDbType.BigInt);
                

                pAction.Value = 16;
                pInwardId.Value = Entity_StockMaster.InwardId;
             
             
                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQueryDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster1, pAction,pInwardId);

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

        public int UpdateAssignFlagInward(ref StockMaster Entity_StockMaster, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(StockMaster._InwardId, SqlDbType.BigInt);

                pAction.Value = 18;
                pInwardId.Value = Entity_StockMaster.InwardId;

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQueryDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster1, pAction, pInwardId);

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

        public int UpdateStatusFlag(string Status ,ref StockMaster Entity_StockMaster, out string strError)
        {

            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pOutwardId = new SqlParameter(StockMaster._OutwardId, SqlDbType.BigInt);
                SqlParameter pStatus = new SqlParameter(StockMaster._Status, SqlDbType.NVarChar);
                SqlParameter pInwardId = new SqlParameter(StockMaster._InwardId, SqlDbType.BigInt);
                pAction.Value = 19;
                pOutwardId.Value = Entity_StockMaster.OutwardId;
                pStatus.Value = Status;
                pInwardId.Value = Entity_StockMaster.InwardId;
                SqlParameter[] Param = new SqlParameter[] { pAction, pOutwardId, pStatus, pInwardId }; 

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMaster1, Param);

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
        
        #region [Report section For Summary and details]-------------------------
        public DataSet GetAssignStockForSummary(string RepCondition, out string strError,int actionno)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(StockMaster._strCond, SqlDbType.NVarChar);
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
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMasterReport, pAction, pRepCondition);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        public DataSet GetAssignStockForReport(string RepCondition, out string strError, int actionno)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(StockMaster._strCond, SqlDbType.NVarChar);
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
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMasterReport, pAction, pRepCondition);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        public DataSet FillStockNoComboForReport(int COND,out string StrError)
        {
            StrError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pCOND = new SqlParameter("@UserID", SqlDbType.BigInt);
                pAction.Value = 1;
                pCOND.Value = COND;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMasterReport, pAction,pCOND);
            }
            catch (Exception ex)
            {
                StrError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }


        public DataSet GetAssignStockForConsumption(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(StockMaster._strCond, SqlDbType.NVarChar);
               
                pAction.Value = 6;
                pRepCondition.Value = RepCondition;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockMaster.SP_StockMasterReport, pAction, pRepCondition);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        #endregion [End Report Section]------------------------------------------

    }
}
