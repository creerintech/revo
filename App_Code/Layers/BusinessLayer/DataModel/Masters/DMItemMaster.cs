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
    public class DMItemMaster : Utility.Setting
    {
        #region[Business Region]

        public int InsertRecord(ref ItemMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemCode = new SqlParameter(ItemMaster._ItemCode, SqlDbType.NVarChar);
                SqlParameter pBarcode = new SqlParameter(ItemMaster._Barcode, SqlDbType.NVarChar);
                SqlParameter pItemName = new SqlParameter(ItemMaster._ItemName, SqlDbType.NVarChar);
                SqlParameter pCategoryId = new SqlParameter(ItemMaster._CategoryId, SqlDbType.BigInt);
                SqlParameter pSubcategoryId = new SqlParameter(ItemMaster._SubcategoryId, SqlDbType.BigInt);
                SqlParameter pTaxDtlsId = new SqlParameter(ItemMaster._TaxDtlsId, SqlDbType.BigInt);
                SqlParameter pTaxTemplateID = new SqlParameter(ItemMaster._TaxTemplateID, SqlDbType.BigInt);  
                SqlParameter pPurchaseRate = new SqlParameter(ItemMaster._PurchaseRate, SqlDbType.Decimal);
                SqlParameter pTaxPer = new SqlParameter(ItemMaster._TaxPer, SqlDbType.Decimal);
                SqlParameter pDeliveryPeriod = new SqlParameter(ItemMaster._DeliveryPeriod, SqlDbType.BigInt);
                SqlParameter pMinStockLevel = new SqlParameter(ItemMaster._MinStockLevel, SqlDbType.NVarChar);
                SqlParameter pReorderLevel = new SqlParameter(ItemMaster._ReorderLevel, SqlDbType.NVarChar);              
                SqlParameter pMaxStockLevel = new SqlParameter(ItemMaster._MaxStockLevel, SqlDbType.NVarChar);
                SqlParameter pOpeningStock = new SqlParameter(ItemMaster._OpeningStock, SqlDbType.Decimal);
                SqlParameter pAsOn = new SqlParameter(ItemMaster._AsOn, SqlDbType.DateTime);
                SqlParameter pStockLocationID = new SqlParameter(ItemMaster._StockLocationID, SqlDbType.BigInt);
                SqlParameter pUnitId = new SqlParameter(ItemMaster._UnitId, SqlDbType.BigInt);
                SqlParameter pCreatedBy = new SqlParameter(ItemMaster._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(ItemMaster._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(ItemMaster._IsDeleted, SqlDbType.Bit);
                SqlParameter pIsKitchenAssign = new SqlParameter(ItemMaster._IsKitchenAssign, SqlDbType.Bit);
                SqlParameter pIsClub = new SqlParameter(ItemMaster._IsClub, SqlDbType.BigInt);
                SqlParameter pItemRemark = new SqlParameter(ItemMaster._ItemRemark, SqlDbType.NVarChar);
                 SqlParameter pHSNCode = new SqlParameter(ItemMaster._HSNCode, SqlDbType.NVarChar);

                pIsKitchenAssign.Value = Entity_Call.IsKitchenAssign;
                pAction.Value = 1;
                pItemCode.Value = Entity_Call.ItemCode;
                pBarcode.Value = Entity_Call.Barcode;
                pItemName.Value = Entity_Call.ItemName;
                pCategoryId.Value = Entity_Call.CategoryId;
                pSubcategoryId.Value = Entity_Call.SubcategoryId;
                pTaxDtlsId.Value = Entity_Call.TaxDtlsId;
                pTaxTemplateID.Value = Entity_Call.TaxTemplateID;
                pPurchaseRate.Value = Entity_Call.PurchaseRate;
                pTaxPer.Value = Entity_Call.TaxPer;
                pDeliveryPeriod.Value = Entity_Call.DeliveryPeriod;
                pMinStockLevel.Value = Entity_Call.MinStockLevel;
                pReorderLevel.Value = Entity_Call.ReorderLevel;
                pMaxStockLevel.Value = Entity_Call.MaxStockLevel;
                pOpeningStock.Value = Entity_Call.OpeningStock;
                pAsOn.Value = Entity_Call.AsOn;
                pStockLocationID.Value = Entity_Call.StockLocationID;
                pUnitId.Value = Entity_Call.UnitId;
                pIsClub.Value = Entity_Call.IsClub;
                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;
                pIsDeleted.Value = Entity_Call.IsDeleted;
                pItemRemark.Value = Entity_Call.Remark;
                pHSNCode.Value = Entity_Call.HSNCode;

                SqlParameter[] Param = new SqlParameter[] { pAction, pItemCode, pBarcode, pItemName, pCategoryId, pSubcategoryId,pTaxDtlsId,pTaxTemplateID, pPurchaseRate,
                    pTaxPer,pDeliveryPeriod,pMinStockLevel,pReorderLevel,pMaxStockLevel,pOpeningStock,pAsOn,pStockLocationID,pUnitId,
                    pCreatedBy, pCreatedDate, pIsDeleted,pIsKitchenAssign ,pIsClub,pItemRemark,pHSNCode};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, Param);

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

        public int InsertUnitConvrsnDtls(ref ItemMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pUnitConvId = new SqlParameter(ItemMaster._UnitConvId, SqlDbType.BigInt);
                SqlParameter pUnitConvDtlsId = new SqlParameter(ItemMaster._UnitConvDtlsId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(ItemMaster._ItemId, SqlDbType.BigInt);

                pAction.Value = 16;
                pUnitConvDtlsId.Value = Entity_Call.UnitConvDtlsId;
                pUnitConvId.Value = Entity_Call.UnitConvId;
                pItemId.Value = Entity_Call.ItemId;

                SqlParameter[] Param = new SqlParameter[] { pAction, pUnitConvDtlsId, pUnitConvId, pItemId };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, Param);

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

        public int InsertDetailsRecord(ref ItemMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(ItemMaster._ItemId, SqlDbType.BigInt);
                SqlParameter pItemDetailsId = new SqlParameter(ItemMaster._ItemDetailsId, SqlDbType.BigInt);
                SqlParameter pSupplierId = new SqlParameter(ItemMaster._SupplierId, SqlDbType.BigInt);
                SqlParameter pPurchaseRate = new SqlParameter(ItemMaster._PurchaseRate, SqlDbType.Decimal);

                SqlParameter pOpeningStock = new SqlParameter(ItemMaster._OpeningStock, SqlDbType.Decimal);
                SqlParameter pMinStockSupp = new SqlParameter(ItemMaster._MinStockSupp, SqlDbType.Decimal);
                SqlParameter pMaxStockSupp = new SqlParameter(ItemMaster._MaxStockSupp, SqlDbType.Decimal);
                SqlParameter pReorderSupp = new SqlParameter(ItemMaster._ReorderSupp, SqlDbType.Decimal);
                SqlParameter pStockLocationID = new SqlParameter(ItemMaster._StockLocationID, SqlDbType.BigInt);
                SqlParameter pUnitId = new SqlParameter(ItemMaster._UnitId, SqlDbType.BigInt);

                SqlParameter pItemDesc = new SqlParameter(ItemMaster._ItemDesc, SqlDbType.NVarChar);
                SqlParameter pFromQty = new SqlParameter(ItemMaster._FromQty, SqlDbType.Decimal);
                SqlParameter pFromUnitID = new SqlParameter(ItemMaster._FromUnitID, SqlDbType.BigInt);
                SqlParameter pToQty = new SqlParameter(ItemMaster._ToQty, SqlDbType.Decimal);
                SqlParameter pToUnitID = new SqlParameter(ItemMaster._ToUnitID, SqlDbType.BigInt);
                SqlParameter pDrawingNo = new SqlParameter(ItemMaster._DrawingNo, SqlDbType.NVarChar);
                SqlParameter pCreatedDate = new SqlParameter(ItemMaster._LoginDate, SqlDbType.DateTime);
                SqlParameter pDrawingPath = new SqlParameter(ItemMaster._DrawingPath, SqlDbType.NVarChar);

                pAction.Value = 9;
                pItemId.Value = Entity_Call.ItemId;
                pSupplierId.Value = Entity_Call.SupplierId;
                pPurchaseRate.Value = Entity_Call.PurchaseRate;
                pItemDetailsId.Value = Entity_Call.ItemDetailsId;
                pOpeningStock.Value = Entity_Call.OpeningStock;
                pStockLocationID.Value = Entity_Call.StockLocationID;
                pUnitId.Value = Entity_Call.UnitId;
                pItemDesc.Value = Entity_Call.ItemDesc;

                pMinStockSupp.Value = Entity_Call.MinStockSupp;
                pMaxStockSupp.Value = Entity_Call.MaxStockSupp;
                pReorderSupp.Value = Entity_Call.ReorderSupp;
                pFromQty.Value = Entity_Call.FromQty;
                pFromUnitID.Value = Entity_Call.FromUnitID;
                pToQty.Value = Entity_Call.ToQty;
                pToUnitID.Value = Entity_Call.ToUnitID;
                pDrawingNo.Value = Entity_Call.DrawingNo;
                pDrawingPath.Value = Entity_Call.DrawingPath;
                pCreatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId, pPurchaseRate, pSupplierId, pOpeningStock,pStockLocationID,
                                                            pUnitId,pItemDesc,pMinStockSupp,pMaxStockSupp,pReorderSupp,pCreatedDate,pItemDetailsId,pDrawingNo,pDrawingPath
                , pFromQty, pFromUnitID, pToQty, pToUnitID };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, Param);

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

        public int InsertDetailsSizeRecord(ref ItemMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(ItemMaster._ItemId, SqlDbType.BigInt);
                SqlParameter pSupplierId = new SqlParameter("@SizeId", SqlDbType.BigInt);


                pAction.Value = 10;
                pItemId.Value = Entity_Call.ItemId;
                pSupplierId.Value = Entity_Call.SupplierId;

                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId, pSupplierId };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, Param);

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

        public int UpdateRecord(ref ItemMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(ItemMaster._ItemId, SqlDbType.NVarChar);
                SqlParameter pItemCode = new SqlParameter(ItemMaster._ItemCode, SqlDbType.NVarChar);
                SqlParameter pBarcode = new SqlParameter(ItemMaster._Barcode, SqlDbType.NVarChar);
                SqlParameter pItemName = new SqlParameter(ItemMaster._ItemName, SqlDbType.NVarChar);
                SqlParameter pCategoryId = new SqlParameter(ItemMaster._CategoryId, SqlDbType.BigInt);
                SqlParameter pSubcategoryId = new SqlParameter(ItemMaster._SubcategoryId, SqlDbType.BigInt);
                SqlParameter pTaxTemplateID = new SqlParameter(ItemMaster._TaxTemplateID, SqlDbType.BigInt);  
                SqlParameter pTaxDtlsId = new SqlParameter(ItemMaster._TaxDtlsId, SqlDbType.BigInt);
                SqlParameter pPurchaseRate = new SqlParameter(ItemMaster._PurchaseRate, SqlDbType.Decimal);
                SqlParameter pTaxPer = new SqlParameter(ItemMaster._TaxPer, SqlDbType.Decimal);
                SqlParameter pDeliveryPeriod = new SqlParameter(ItemMaster._DeliveryPeriod, SqlDbType.BigInt);
                SqlParameter pMinStockLevel = new SqlParameter(ItemMaster._MinStockLevel, SqlDbType.NVarChar);
                SqlParameter pReorderLevel = new SqlParameter(ItemMaster._ReorderLevel, SqlDbType.NVarChar);
                SqlParameter pMaxStockLevel = new SqlParameter(ItemMaster._MaxStockLevel, SqlDbType.NVarChar);
                SqlParameter pOpeningStock = new SqlParameter(ItemMaster._OpeningStock, SqlDbType.Decimal);
                SqlParameter pAsOn = new SqlParameter(ItemMaster._AsOn, SqlDbType.DateTime);
                SqlParameter pStockLocationID = new SqlParameter(ItemMaster._StockLocationID, SqlDbType.BigInt);
                SqlParameter pUnitId = new SqlParameter(ItemMaster._UnitId, SqlDbType.BigInt);
                SqlParameter pCreatedBy = new SqlParameter(ItemMaster._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(ItemMaster._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(ItemMaster._IsDeleted, SqlDbType.Bit);
                SqlParameter pIsKitchenAssign = new SqlParameter(ItemMaster._IsKitchenAssign, SqlDbType.Bit);
                SqlParameter pIsClub = new SqlParameter(ItemMaster._IsClub, SqlDbType.BigInt);
                SqlParameter pItemRemark = new SqlParameter(ItemMaster._ItemRemark, SqlDbType.NVarChar);
                SqlParameter pHSNCode = new SqlParameter(ItemMaster._HSNCode, SqlDbType.NVarChar);

                pIsKitchenAssign.Value = Entity_Call.IsKitchenAssign;
                pIsClub.Value = Entity_Call.IsClub;
                pAction.Value = 2;
                pItemId.Value = Entity_Call.ItemId;
                pItemCode.Value = Entity_Call.ItemCode;
                pBarcode.Value = Entity_Call.Barcode;
                pItemName.Value = Entity_Call.ItemName;
                pCategoryId.Value = Entity_Call.CategoryId;
                pSubcategoryId.Value = Entity_Call.SubcategoryId;
                pTaxDtlsId.Value = Entity_Call.TaxDtlsId;
                pTaxTemplateID.Value = Entity_Call.TaxTemplateID;
                pPurchaseRate.Value = Entity_Call.PurchaseRate;
                pTaxPer.Value = Entity_Call.TaxPer;
                pDeliveryPeriod.Value = Entity_Call.DeliveryPeriod;
                pMinStockLevel.Value = Entity_Call.MinStockLevel;
                pReorderLevel.Value = Entity_Call.ReorderLevel;
                pMaxStockLevel.Value = Entity_Call.MaxStockLevel;
                pOpeningStock.Value = Entity_Call.OpeningStock;
                pAsOn.Value = Entity_Call.AsOn;
                pStockLocationID.Value = Entity_Call.StockLocationID;
                pUnitId.Value = Entity_Call.UnitId;

                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;
                pIsDeleted.Value = Entity_Call.IsDeleted;
                pItemRemark.Value = Entity_Call.Remark;
                pHSNCode.Value = Entity_Call.HSNCode;

                SqlParameter[] Param = new SqlParameter[] { pAction,pItemId, pItemCode, pBarcode, pItemName, pCategoryId, pSubcategoryId,pTaxDtlsId,pTaxTemplateID, pPurchaseRate,
                    pTaxPer,pDeliveryPeriod,pMinStockLevel,pReorderLevel,pMaxStockLevel,pOpeningStock,pAsOn,pStockLocationID,pUnitId,
                    pCreatedBy, pCreatedDate, pIsDeleted ,pIsKitchenAssign,pIsClub,pItemRemark,pHSNCode};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, Param);

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

        public int DeleteRecord(ref ItemMaster Entity_Call, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(ItemMaster._ItemId, SqlDbType.BigInt);
                SqlParameter pDeletedBy = new SqlParameter(ItemMaster._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(ItemMaster._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(ItemMaster._IsDeleted, SqlDbType.Bit);

                pAction.Value = 3;
                pItemId.Value = Entity_Call.ItemId;

                pDeletedBy.Value = Entity_Call.UserId;
                pDeletedDate.Value = Entity_Call.LoginDate;
                pIsDeleted.Value = Entity_Call.IsDeleted;

                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId, pDeletedBy, pDeletedDate, pIsDeleted };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, Param);

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

        public DataSet GetItemForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(ItemMaster._ItemId, SqlDbType.BigInt);

                pAction.Value = 4;
                pItemId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, pAction, pItemId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetItem(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ItemMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetItemDetailsForCalculateFactor(int id, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ItemMaster._ItemId, SqlDbType.BigInt);

                pAction.Value = 19;
                pRepCondition.Value = id;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetItemByCategory(string RepCondition, int i, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ItemMaster._strCond, SqlDbType.NVarChar);
                SqlParameter pAnotherCondition = new SqlParameter("@RepCondition", SqlDbType.NVarChar);

                pAction.Value = i;
                pRepCondition.Value = RepCondition;

                SqlParameter[] param = new SqlParameter[] { pAction, pRepCondition, pAnotherCondition };
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_ItemMasterForSearch", pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetItemBySubCategory(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ItemMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 14;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetItemBySubCategoryWithCategory(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ItemMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 13;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetItemcode(out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                pAction.Value = 6;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, pAction);
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

        public DataSet ChkDuplicate(string Name, Int32 catid, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ItemMaster._strCond, SqlDbType.NVarChar);
                SqlParameter pcategoty = new SqlParameter(ItemMaster._CategoryId, SqlDbType.BigInt);

                pAction.Value = 7;
                pRepCondition.Value = Name;
                pcategoty.Value = catid;
                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] { pAction, pRepCondition, pcategoty };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet FillCombo(out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);

                pAction.Value = 8;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, pAction);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet FillComboForTaxTemplate(int TaxTemplateID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pCategoryId = new SqlParameter(ItemMaster._CategoryId, SqlDbType.BigInt);
                //@SizeId
                pAction.Value = 20;
                pCategoryId.Value = TaxTemplateID;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, pAction, pCategoryId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetSpecialPermission(int UserID, string FormName, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pUserId = new SqlParameter(ItemMaster._UserId, SqlDbType.BigInt);
                SqlParameter pFormName = new SqlParameter(ItemMaster._ItemName, SqlDbType.NVarChar);
                pAction.Value = 17;
                pUserId.Value = UserID;
                pFormName.Value = FormName;
                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] { pAction, pUserId, pFormName };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public string[] GetSuggestedRecord(string prefixText, string Category, string SubCategory)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;
            try
            {

                // -- For Checking OF Execution of Procedure=========
                SqlParameter MAction = new SqlParameter(ItemMaster._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(ItemMaster._strCond, SqlDbType.NVarChar);
                SqlParameter MCATTEMP = new SqlParameter("@CATTEMP", SqlDbType.NVarChar);
                SqlParameter MSUBCATTEMP = new SqlParameter("@SUBCATTEMP", SqlDbType.NVarChar);

                MAction.Value = 5;
                MRepCondition.Value = prefixText;
                MCATTEMP.Value = Category;
                MSUBCATTEMP.Value = SubCategory;
                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition, MCATTEMP, MSUBCATTEMP };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, oParmCol);

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

        public string[] GetSuggestedRecordCategory(string prefixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;
            try
            {

                // -- For Checking OF Execution of Procedure=========
                SqlParameter MAction = new SqlParameter(ItemMaster._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(ItemMaster._strCond, SqlDbType.NVarChar);

                MAction.Value = 11;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, oParmCol);

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

        public string[] GetSuggestedRecordSubCategory(string prefixText, string Category)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;
            try
            {

                // -- For Checking OF Execution of Procedure=========
                SqlParameter MAction = new SqlParameter(ItemMaster._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(ItemMaster._strCond, SqlDbType.NVarChar);
                SqlParameter MTEMP = new SqlParameter("@CATTEMP", SqlDbType.NVarChar);
                MAction.Value = 12;
                MRepCondition.Value = prefixText;
                MTEMP.Value = Category;
                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition, MTEMP };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, oParmCol);

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

        public DataSet GetUnitConversionDtls(int UnitID, int ItemId, int temp, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pUnitId = new SqlParameter(ItemMaster._UnitId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(ItemMaster._ItemId, SqlDbType.BigInt);
                SqlParameter pStrCond = new SqlParameter("@Temp", SqlDbType.BigInt);

                pAction.Value = 15;
                pUnitId.Value = UnitID;
                pItemId.Value = ItemId;
                pStrCond.Value = temp;

                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] { pAction, pUnitId, pItemId, pStrCond };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetItemUnitDtls(int UnitID, int ItemId, int temp, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pUnitId = new SqlParameter(ItemMaster._FromUnitID, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(ItemMaster._ItemID, SqlDbType.BigInt);

                pAction.Value = 4;
                pUnitId.Value = UnitID;
                pItemId.Value = ItemId;

                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] { pAction, pUnitId, pItemId };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, "SP_ItemUnitConversion", param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public int InsertItemUnitMaster(ref ItemMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemID = new SqlParameter(ItemMaster._ItemID, SqlDbType.BigInt);
                SqlParameter pFromQty = new SqlParameter(ItemMaster._FromQty, SqlDbType.Decimal);
                SqlParameter pFromUnitID = new SqlParameter(ItemMaster._FromUnitID, SqlDbType.BigInt);
                SqlParameter pToQty = new SqlParameter(ItemMaster._ToQty, SqlDbType.Decimal);
                SqlParameter pToUnitID = new SqlParameter(ItemMaster._ToUnitID, SqlDbType.BigInt);

                pAction.Value = 1;
                pItemID.Value = Entity_Call.ItemID;
                pFromQty.Value = Entity_Call.FromQty;
                pFromUnitID.Value = Entity_Call.FromUnitID;
                pToQty.Value = Entity_Call.ToQty;
                pToUnitID.Value = Entity_Call.ToUnitID;

                SqlParameter[] Param = new SqlParameter[] { pAction, pItemID, pFromQty, pFromUnitID, pToQty, pToUnitID };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, "SP_ItemUnitConversion", Param);

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

        public int InsertItemUnitDetails(ref ItemMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemUnitMasterID = new SqlParameter(ItemMaster._ItemMasterID, SqlDbType.BigInt);
                SqlParameter pItemID = new SqlParameter(ItemMaster._ItemID, SqlDbType.BigInt);
                SqlParameter pFromQty = new SqlParameter(ItemMaster._FromQty, SqlDbType.Decimal);
                SqlParameter pFromUnitID = new SqlParameter(ItemMaster._FromUnitID, SqlDbType.BigInt);
                SqlParameter pToQty = new SqlParameter(ItemMaster._ToQty, SqlDbType.Decimal);
                SqlParameter pToUnitID = new SqlParameter(ItemMaster._ToUnitID, SqlDbType.BigInt);

                pAction.Value = 2;
                pItemUnitMasterID.Value = Entity_Call.ItemMasterID;
                pItemID.Value = Entity_Call.ItemID;
                pFromQty.Value = Entity_Call.FromQty;
                pFromUnitID.Value = Entity_Call.FromUnitID;
                pToQty.Value = Entity_Call.ToQty;
                pToUnitID.Value = Entity_Call.ToUnitID;

                SqlParameter[] Param = new SqlParameter[] { pAction, pItemUnitMasterID, pItemID, pFromQty, pFromUnitID, pToQty, pToUnitID };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, "SP_ItemUnitConversion", Param);

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

        public int Insert_UnitCalculation(ref ItemMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(ItemMaster._ItemId, SqlDbType.BigInt);
                SqlParameter pFrom_Factor = new SqlParameter(ItemMaster._From_Factor, SqlDbType.Decimal);
                SqlParameter pFrom_UnitID = new SqlParameter(ItemMaster._From_UnitID, SqlDbType.BigInt);
                SqlParameter pTo_Factor = new SqlParameter(ItemMaster._To_Factor, SqlDbType.Decimal);
                SqlParameter pTo_UnitID = new SqlParameter(ItemMaster._To_UnitID, SqlDbType.BigInt);
                SqlParameter pFactor_Desc = new SqlParameter(ItemMaster._Factor_Desc, SqlDbType.NVarChar);
                //SqlParameter pDrawingNo = new SqlParameter(ItemMaster._DrawingNo, SqlDbType.NVarChar);

                pAction.Value = 18;
                pItemId.Value = Entity_Call.ItemId;
                pFrom_Factor.Value = Entity_Call.From_Factor;
                pFrom_UnitID.Value = Entity_Call.From_UnitID;
                pTo_Factor.Value = Entity_Call.To_Factor;
                pTo_UnitID.Value = Entity_Call.To_UnitID;
                pFactor_Desc.Value = Entity_Call.Factor_Desc;
                // pDrawingNo.Value = Entity_Call.DrawingNo;
                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId, pFrom_Factor, pFrom_UnitID ,
                                                            pTo_Factor,pTo_UnitID,pFactor_Desc };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, Param);

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

        #endregion

        #region [FOR PEINTING ITEM MASTER]---
        public DataSet GetItemForPrint(String Cond, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(ItemMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 19;
                pItemId.Value = Cond;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemMaster.SP_ItemMaster, pAction, pItemId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        #endregion

        public DMItemMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int DeleteItemDetails(ref ItemMaster Entity_ItemMaster, out string StrError)
        {
            int iDelete = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(ItemMaster._Action, SqlDbType.BigInt);
                SqlParameter pItemDetailsId = new SqlParameter(ItemMaster._ItemDetailsId, SqlDbType.BigInt);
           

                pAction.Value = 23;
                pItemDetailsId.Value = Entity_ItemMaster.ItemDetailsId;

                SqlParameter[] param = new SqlParameter[] { pAction, pItemDetailsId };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, "SP_ItemMaster", param);

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
    }
}