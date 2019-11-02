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
    public class DMMaterialInwardReg : Utility.Setting
    {
        #region[BusinessLogic]

        public int InsertRecord(ref MaterialInwardReg Entity_MaterialInwardReg, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pInwardNo = new SqlParameter(MaterialInwardReg._InwardNo, SqlDbType.NVarChar);
                SqlParameter pInwardDate = new SqlParameter(MaterialInwardReg._InwardDate, SqlDbType.DateTime);
                SqlParameter pPOId = new SqlParameter(MaterialInwardReg._POId, SqlDbType.BigInt);
                SqlParameter pType = new SqlParameter(MaterialInwardReg._Type, SqlDbType.NVarChar);

                SqlParameter pBillNo = new SqlParameter(MaterialInwardReg._BillNo, SqlDbType.NVarChar);
                SqlParameter pInwardThrough = new SqlParameter(MaterialInwardReg._InwardThrough, SqlDbType.NVarChar);
                //SqlParameter pSuplierId = new SqlParameter(MaterialInwardReg._SuplierId, SqlDbType.BigInt);
                SqlParameter pBillingAddress = new SqlParameter(MaterialInwardReg._BillingAddress, SqlDbType.NVarChar);
                SqlParameter pShippingAddress = new SqlParameter(MaterialInwardReg._ShippingAddress, SqlDbType.NVarChar);
                SqlParameter pSuplierId = new SqlParameter(MaterialInwardReg._SuplierId, SqlDbType.BigInt);

                SqlParameter pSubTotal = new SqlParameter(MaterialInwardReg._SubTotal, SqlDbType.Decimal);
                SqlParameter pDiscountPer = new SqlParameter(MaterialInwardReg._DiscountPer, SqlDbType.Decimal);
                SqlParameter pDiscountAmt = new SqlParameter(MaterialInwardReg._DiscountAmt, SqlDbType.Decimal);
                SqlParameter pVatPer = new SqlParameter(MaterialInwardReg._VatPer, SqlDbType.Decimal);
                SqlParameter pVatAmt = new SqlParameter(MaterialInwardReg._VatAmt, SqlDbType.Decimal);
                SqlParameter pDekhrekhPer = new SqlParameter(MaterialInwardReg._DekhrekhPer, SqlDbType.Decimal);
                SqlParameter pDekhrekhAmt = new SqlParameter(MaterialInwardReg._DekhrekhAmt, SqlDbType.Decimal);
                SqlParameter pHamaliPer = new SqlParameter(MaterialInwardReg._HamaliPer, SqlDbType.Decimal);
                SqlParameter pHamaliAmt = new SqlParameter(MaterialInwardReg._HamaliAmt, SqlDbType.Decimal);
                SqlParameter pCESSPer = new SqlParameter(MaterialInwardReg._CESSPer, SqlDbType.Decimal);
                SqlParameter pCESSAmt = new SqlParameter(MaterialInwardReg._CESSAmt, SqlDbType.Decimal);
                SqlParameter pFreightPer = new SqlParameter(MaterialInwardReg._FreightPer, SqlDbType.Decimal);
                SqlParameter pFreightAmt = new SqlParameter(MaterialInwardReg._FreightAmt, SqlDbType.Decimal);
                SqlParameter pPackingPer = new SqlParameter(MaterialInwardReg._PackingPer, SqlDbType.Decimal);
                SqlParameter pPackingAmt = new SqlParameter(MaterialInwardReg._PackingAmt, SqlDbType.Decimal);
                SqlParameter pPostagePer = new SqlParameter(MaterialInwardReg._PostagePer, SqlDbType.Decimal);
                SqlParameter pPostageAmt = new SqlParameter(MaterialInwardReg._PostageAmt, SqlDbType.Decimal);
                SqlParameter pOtherCharges = new SqlParameter(MaterialInwardReg._OtherCharges, SqlDbType.Decimal);
                SqlParameter pGrandTotal = new SqlParameter(MaterialInwardReg._GrandTotal, SqlDbType.Decimal);

                SqlParameter pBillDate = new SqlParameter(MaterialInwardReg._BillDate, SqlDbType.DateTime);
                SqlParameter pInstruction = new SqlParameter(MaterialInwardReg._Instruction, SqlDbType.Text);

                SqlParameter pVehicalNo = new SqlParameter(MaterialInwardReg._VehicalNo, SqlDbType.Text);
                SqlParameter pTimeIn = new SqlParameter(MaterialInwardReg._TimeIn, SqlDbType.DateTime);
                SqlParameter pTimeOut = new SqlParameter(MaterialInwardReg._TimeOut, SqlDbType.DateTime);    

                SqlParameter pCreatedBy = new SqlParameter(MaterialInwardReg._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(MaterialInwardReg._LoginDate, SqlDbType.DateTime);
                SqlParameter pUserInwardNo = new SqlParameter(MaterialInwardReg._UserInwardNo, SqlDbType.NVarChar);
                // SqlParameter pIsDeleted = new SqlParameter(MaterialInwardReg._IsDeleted, SqlDbType.Bit);

                pAction.Value = 1;
                pInwardNo.Value = Entity_MaterialInwardReg.InwardNo;
                pInwardDate.Value = Entity_MaterialInwardReg.InwardDate;
                pPOId.Value = Entity_MaterialInwardReg.POId;
                pType.Value = Entity_MaterialInwardReg.Type;
                pBillNo.Value = Entity_MaterialInwardReg.BillNo;
                pInwardThrough.Value = Entity_MaterialInwardReg.InwardThrough;
                //pSuplierId.Value = Entity_Call.SuplierId;
                pBillingAddress.Value = Entity_MaterialInwardReg.BillingAddress;
                pShippingAddress.Value = Entity_MaterialInwardReg.ShippingAddress;
                pSuplierId.Value = Entity_MaterialInwardReg.SuplierId;
                pSubTotal.Value = Entity_MaterialInwardReg.SubTotal;
                pDiscountPer.Value = Entity_MaterialInwardReg.DiscountPer;
                pDiscountAmt.Value = Entity_MaterialInwardReg.DiscountAmt;
                pVatPer.Value = Entity_MaterialInwardReg.VatPer;
                pVatAmt.Value = Entity_MaterialInwardReg.VatAmt;
                pDekhrekhPer.Value = Entity_MaterialInwardReg.DekhrekhPer;
                pDekhrekhAmt.Value = Entity_MaterialInwardReg.DekhrekhAmt;
                pHamaliPer.Value = Entity_MaterialInwardReg.HamaliPer;
                pHamaliAmt.Value = Entity_MaterialInwardReg.HamaliAmt;
                pCESSPer.Value = Entity_MaterialInwardReg.CESSPer;
                pCESSAmt.Value = Entity_MaterialInwardReg.CESSAmt;
                pFreightPer.Value = Entity_MaterialInwardReg.FreightPer;
                pFreightAmt.Value = Entity_MaterialInwardReg.FreightAmt;
                pPackingPer.Value = Entity_MaterialInwardReg.PackingPer;
                pPackingAmt.Value = Entity_MaterialInwardReg.PackingAmt;
                pPostagePer.Value = Entity_MaterialInwardReg.PostagePer;
                pPostageAmt.Value = Entity_MaterialInwardReg.PostageAmt;
                pOtherCharges.Value = Entity_MaterialInwardReg.OtherCharges;
                pGrandTotal.Value = Entity_MaterialInwardReg.GrandTotal;

                pBillDate.Value = Entity_MaterialInwardReg.BillDate;

                pInstruction.Value = Entity_MaterialInwardReg.Instruction;

                pVehicalNo.Value = Entity_MaterialInwardReg.Vehical;
                pTimeIn.Value = Entity_MaterialInwardReg.TimeIn;
                pTimeOut.Value = Entity_MaterialInwardReg.TimeOut;

                pCreatedBy.Value = Entity_MaterialInwardReg.UserId;
                pCreatedDate.Value = Entity_MaterialInwardReg.LoginDate;
                pUserInwardNo.Value = Entity_MaterialInwardReg.UserInwardNo;
                //pIsDeleted.Value = Entity_Call.IsDeleted;

                SqlParameter[] Param = new SqlParameter[] { pAction, pInwardNo, pInwardDate, pPOId,pType,pBillNo,pInwardThrough, pBillingAddress, pShippingAddress,
                    pSubTotal,pDiscountPer,pDiscountAmt,pVatPer,pVatAmt,pDekhrekhPer,pDekhrekhAmt,pHamaliPer,pHamaliAmt,
                    pCESSPer,pCESSAmt,pFreightPer,pFreightAmt,pPackingPer,pPackingAmt,pPostagePer,pPostageAmt,pOtherCharges,pGrandTotal,
                    pInstruction,pVehicalNo,pTimeIn,pTimeOut,pCreatedBy, pCreatedDate, pSuplierId,pBillDate,pUserInwardNo};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, Param);

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

        public int InsertDetailsRecord(ref MaterialInwardReg Entity_MaterialInwardReg, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(MaterialInwardReg._InwardId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(MaterialInwardReg._ItemId, SqlDbType.BigInt);
                SqlParameter pInwardQty = new SqlParameter(MaterialInwardReg._InwardQty, SqlDbType.Decimal);
                SqlParameter pOrderQty = new SqlParameter(MaterialInwardReg._OrderQty, SqlDbType.Decimal);
                SqlParameter pPendingQty = new SqlParameter(MaterialInwardReg._PendingQty, SqlDbType.Decimal);
                SqlParameter pInwardRate = new SqlParameter(MaterialInwardReg._InwardRate, SqlDbType.Decimal);
                SqlParameter pPORate = new SqlParameter(MaterialInwardReg._PORate, SqlDbType.Decimal);
                SqlParameter pDiffrence = new SqlParameter(MaterialInwardReg._Diffrence, SqlDbType.Decimal);
                SqlParameter pAmount = new SqlParameter(MaterialInwardReg._Amount, SqlDbType.Decimal);
                SqlParameter pTaxPer = new SqlParameter(MaterialInwardReg._TaxPer, SqlDbType.Decimal);
                SqlParameter pTaxAmount = new SqlParameter(MaterialInwardReg._TaxAmount, SqlDbType.Decimal);
                SqlParameter pDiscPer = new SqlParameter(MaterialInwardReg._DiscPer, SqlDbType.Decimal);
                SqlParameter pDiscAmt = new SqlParameter(MaterialInwardReg._DiscAmt, SqlDbType.Decimal);
                SqlParameter pNetAmount = new SqlParameter(MaterialInwardReg._NetAmount, SqlDbType.Decimal);
                SqlParameter pExpectedDate = new SqlParameter(MaterialInwardReg._ExpectedDate, SqlDbType.DateTime);
                SqlParameter pDeliveryDate = new SqlParameter(MaterialInwardReg._DeliveryDate, SqlDbType.DateTime);
                SqlParameter pUnitId = new SqlParameter(MaterialInwardReg._UnitId, SqlDbType.BigInt);
                SqlParameter pStockDate = new SqlParameter(MaterialInwardReg._StockDate, SqlDbType.DateTime);
                SqlParameter pStockLocationID = new SqlParameter(MaterialInwardReg._StockLocationID, SqlDbType.BigInt);
                SqlParameter pLocID = new SqlParameter(MaterialInwardReg._LocID, SqlDbType.BigInt);
                SqlParameter pConversionUnitId = new SqlParameter(MaterialInwardReg._ConversionUnitId, SqlDbType.BigInt);
                SqlParameter pActualQty = new SqlParameter(MaterialInwardReg._ActualQty, SqlDbType.Decimal);
                SqlParameter pItemDesc = new SqlParameter(MaterialInwardReg._ItemDesc, SqlDbType.NVarChar);

                pAction.Value = 8;
                pInwardId.Value = Entity_MaterialInwardReg.InwardId;
                pItemId.Value = Entity_MaterialInwardReg.ItemId;
                pInwardQty.Value = Entity_MaterialInwardReg.InwardQty;
                pOrderQty.Value = Entity_MaterialInwardReg.OrderQty;
                pPendingQty.Value = Entity_MaterialInwardReg.PendingQty;
                pInwardRate.Value = Entity_MaterialInwardReg.InwardRate;
                pPORate.Value = Entity_MaterialInwardReg.PORate;
                pDiffrence.Value = Entity_MaterialInwardReg.Diffrence;
                pAmount.Value = Entity_MaterialInwardReg.Amount;
                pTaxPer.Value = Entity_MaterialInwardReg.TaxPer;
                pTaxAmount.Value = Entity_MaterialInwardReg.TaxAmount;
                pDiscPer.Value = Entity_MaterialInwardReg.DiscPer;
                pDiscAmt.Value = Entity_MaterialInwardReg.DiscAmt;
                pNetAmount.Value = Entity_MaterialInwardReg.NetAmount;
                pExpectedDate.Value = Entity_MaterialInwardReg.ExpectedDate;
                pDeliveryDate.Value = Entity_MaterialInwardReg.DeliveryDate;
                pUnitId.Value = Entity_MaterialInwardReg.UnitId;
                pStockDate.Value = Entity_MaterialInwardReg.StockDate;
                pStockLocationID.Value = Entity_MaterialInwardReg.StockLocationID;
                pLocID.Value = Entity_MaterialInwardReg.LocID;
                pConversionUnitId.Value = Entity_MaterialInwardReg.ConversionUnitId;
                pActualQty.Value = Entity_MaterialInwardReg.ActualQty;
                pItemDesc.Value = Entity_MaterialInwardReg.ItemDesc;

                SqlParameter[] Param = new SqlParameter[] { pAction,pInwardId, pItemId, pInwardQty, pOrderQty,pPendingQty,pInwardRate,pPORate,pDiffrence,pAmount,
                    pTaxPer,pTaxAmount,pDiscPer,pDiscAmt,pNetAmount,pExpectedDate,pDeliveryDate,pUnitId,pStockDate,pStockLocationID,pLocID,pConversionUnitId,pActualQty
                ,pItemDesc};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, Param);

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

        public int DeleteFromOutwardRecord(ref MaterialInwardReg Entity_MaterialInwardReg, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(MaterialInwardReg._InwardId, SqlDbType.BigInt);

                pAction.Value = 15;
                pInwardId.Value = Entity_MaterialInwardReg.InwardId;

                SqlParameter[] Param = new SqlParameter[] { pAction,pInwardId};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, Param);

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

        public int UpdateForAssignStatus(ref MaterialInwardReg Entity_MaterialInwardReg, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(MaterialInwardReg._InwardId, SqlDbType.BigInt);

                pAction.Value = 16;
                pInwardId.Value = Entity_MaterialInwardReg.InwardId;

                SqlParameter[] Param = new SqlParameter[] { pAction, pInwardId };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, Param);

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


        public int UpdateRecord(ref MaterialInwardReg Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(MaterialInwardReg._InwardId, SqlDbType.BigInt);
                SqlParameter pInwardNo = new SqlParameter(MaterialInwardReg._InwardNo, SqlDbType.NVarChar);
                SqlParameter pInwardDate = new SqlParameter(MaterialInwardReg._InwardDate, SqlDbType.DateTime);
                SqlParameter pPOId = new SqlParameter(MaterialInwardReg._POId, SqlDbType.NVarChar);
                SqlParameter pType = new SqlParameter(MaterialInwardReg._Type, SqlDbType.NVarChar);

                SqlParameter pBillNo = new SqlParameter(MaterialInwardReg._BillNo, SqlDbType.NVarChar);
                SqlParameter pInwardThrough = new SqlParameter(MaterialInwardReg._InwardThrough, SqlDbType.NVarChar);
                SqlParameter pSuplierId = new SqlParameter(MaterialInwardReg._SuplierId, SqlDbType.BigInt);
                SqlParameter pBillingAddress = new SqlParameter(MaterialInwardReg._BillingAddress, SqlDbType.NVarChar);
                SqlParameter pShippingAddress = new SqlParameter(MaterialInwardReg._ShippingAddress, SqlDbType.NVarChar);

                SqlParameter pSubTotal = new SqlParameter(MaterialInwardReg._SubTotal, SqlDbType.Decimal);
                SqlParameter pDiscountPer = new SqlParameter(MaterialInwardReg._DiscountPer, SqlDbType.Decimal);
                SqlParameter pDiscountAmt = new SqlParameter(MaterialInwardReg._DiscountAmt, SqlDbType.Decimal);
                SqlParameter pVatPer = new SqlParameter(MaterialInwardReg._VatPer, SqlDbType.Decimal);
                SqlParameter pVatAmt = new SqlParameter(MaterialInwardReg._VatAmt, SqlDbType.Decimal);
                SqlParameter pDekhrekhPer = new SqlParameter(MaterialInwardReg._DekhrekhPer, SqlDbType.Decimal);
                SqlParameter pDekhrekhAmt = new SqlParameter(MaterialInwardReg._DekhrekhAmt, SqlDbType.Decimal);
                SqlParameter pHamaliPer = new SqlParameter(MaterialInwardReg._HamaliPer, SqlDbType.Decimal);
                SqlParameter pHamaliAmt = new SqlParameter(MaterialInwardReg._HamaliAmt, SqlDbType.Decimal);
                SqlParameter pCESSPer = new SqlParameter(MaterialInwardReg._CESSPer, SqlDbType.Decimal);
                SqlParameter pCESSAmt = new SqlParameter(MaterialInwardReg._CESSAmt, SqlDbType.Decimal);
                SqlParameter pFreightPer = new SqlParameter(MaterialInwardReg._FreightPer, SqlDbType.Decimal);
                SqlParameter pFreightAmt = new SqlParameter(MaterialInwardReg._FreightAmt, SqlDbType.Decimal);
                SqlParameter pPackingPer = new SqlParameter(MaterialInwardReg._PackingPer, SqlDbType.Decimal);
                SqlParameter pPackingAmt = new SqlParameter(MaterialInwardReg._PackingAmt, SqlDbType.Decimal);
                SqlParameter pPostagePer = new SqlParameter(MaterialInwardReg._PostagePer, SqlDbType.Decimal);
                SqlParameter pPostageAmt = new SqlParameter(MaterialInwardReg._PostageAmt, SqlDbType.Decimal);
                SqlParameter pOtherCharges = new SqlParameter(MaterialInwardReg._OtherCharges, SqlDbType.Decimal);
                SqlParameter pGrandTotal = new SqlParameter(MaterialInwardReg._GrandTotal, SqlDbType.Decimal);
                
                SqlParameter pInstruction = new SqlParameter(MaterialInwardReg._Instruction, SqlDbType.Text);

                SqlParameter pUpdatedBy = new SqlParameter(MaterialInwardReg._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(MaterialInwardReg._LoginDate, SqlDbType.DateTime);

                SqlParameter pBillDate = new SqlParameter(MaterialInwardReg._BillDate, SqlDbType.DateTime);
                SqlParameter pUserInwardNo = new SqlParameter(MaterialInwardReg._UserInwardNo, SqlDbType.NVarChar);

                pAction.Value = 2;
                pInwardId.Value = Entity_Call.InwardId;
                pInwardNo.Value = Entity_Call.InwardNo;
                pInwardDate.Value = Entity_Call.InwardDate;
                pPOId.Value = Entity_Call.POId;
                pType.Value = Entity_Call.Type;

                pBillNo.Value = Entity_Call.BillNo;
                pInwardThrough.Value = Entity_Call.InwardThrough;
                pSuplierId.Value = Entity_Call.SuplierId;
                pBillingAddress.Value = Entity_Call.BillingAddress;
                pShippingAddress.Value = Entity_Call.ShippingAddress;

                pSubTotal.Value = Entity_Call.SubTotal;
                pDiscountPer.Value = Entity_Call.DiscountPer;
                pDiscountAmt.Value = Entity_Call.DiscountAmt;
                pVatPer.Value = Entity_Call.VatPer;
                pVatAmt.Value = Entity_Call.VatAmt;
                pDekhrekhPer.Value = Entity_Call.DekhrekhPer;
                pDekhrekhAmt.Value = Entity_Call.DekhrekhAmt;
                pHamaliPer.Value = Entity_Call.HamaliPer;
                pHamaliAmt.Value = Entity_Call.HamaliAmt;
                pCESSPer.Value = Entity_Call.CESSPer;
                pCESSAmt.Value = Entity_Call.CESSAmt;
                pFreightPer.Value = Entity_Call.FreightPer;
                pFreightAmt.Value = Entity_Call.FreightAmt;
                pPackingPer.Value = Entity_Call.PackingPer;
                pPackingAmt.Value = Entity_Call.PackingAmt;
                pPostagePer.Value = Entity_Call.PostagePer;
                pPostageAmt.Value = Entity_Call.PostageAmt;
                pOtherCharges.Value = Entity_Call.OtherCharges;
                pGrandTotal.Value = Entity_Call.GrandTotal;
                
                pInstruction.Value = Entity_Call.Instruction;

                pUpdatedBy.Value = Entity_Call.UserId;
                pUpdatedDate.Value = Entity_Call.LoginDate;

                pBillDate.Value = Entity_Call.BillDate;
                pUserInwardNo.Value = Entity_Call.UserInwardNo;

                SqlParameter[] Param = new SqlParameter[] { pAction,pInwardId,pInwardNo, pInwardDate, pPOId,pType,pBillNo,pInwardThrough, pBillingAddress, pShippingAddress,
                    pSubTotal,pDiscountPer,pDiscountAmt,pVatPer,pVatAmt,pDekhrekhPer,pDekhrekhAmt,pHamaliPer,pHamaliAmt,
                    pCESSPer,pCESSAmt,pFreightPer,pFreightAmt,pPackingPer,pPackingAmt,pPostagePer,pPostageAmt,pOtherCharges,pGrandTotal,
                    pInstruction,pUpdatedBy, pUpdatedDate,pSuplierId,pBillDate,pUserInwardNo };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, Param);

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

        public int DeleteRecord(ref MaterialInwardReg Entity_MaterialInwardReg, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(MaterialInwardReg._InwardId, SqlDbType.BigInt);
                //SqlParameter pPOId = new SqlParameter(MaterialInwardReg._POId,SqlDbType.BigInt);

                SqlParameter pDeletedBy = new SqlParameter(MaterialInwardReg._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(MaterialInwardReg._LoginDate, SqlDbType.DateTime);
              

                pAction.Value = 3;
                pInwardId.Value = Entity_MaterialInwardReg.InwardId;
                //pPOId.Value = Entity_MaterialInwardReg.POId;
                pDeletedBy.Value = Entity_MaterialInwardReg.UserId;
                pDeletedDate.Value = Entity_MaterialInwardReg.LoginDate;
              

                SqlParameter[] Param = new SqlParameter[] { pAction, pInwardId, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, Param);

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

        public DataSet GetInwardNo(out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                pAction.Value = 4;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, pAction);
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


        public DataSet GetSaveInwardNo(int billno, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pBill = new SqlParameter("@InwardId", SqlDbType.BigInt);
                pAction.Value = 13;
                pBill.Value = billno;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, pAction,pBill);
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


        public DataSet GetInwardReg(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(MaterialInwardReg._strCond, SqlDbType.NVarChar);

                pAction.Value = 6;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, pAction, pRepCondition);

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
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(MaterialInwardReg._InwardId, SqlDbType.BigInt);

                pAction.Value = 5;
                pInwardId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, pAction, pInwardId);
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
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);

                pAction.Value = 7;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, pAction);

            }
            catch (Exception ex)
            {
                ErrorLogger errLog = new ErrorLogger();
                errLog.ErrorInfo(ex);
                strError = ex.Message;

            }
            finally { Close(); }
            return Ds;
        }
        public DataSet FillReportCombo(int UserId,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pUserId = new SqlParameter("@userId", SqlDbType.BigInt);
                pAction.Value = 1;
                pUserId.Value = UserId;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReport, pAction, pUserId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetInwordReport(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter MRepCondition = new SqlParameter(MaterialInwardReg._strCond, SqlDbType.NVarChar);
                MAction.Value = 2;
                MRepCondition.Value = RepCondition;

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReport, MAction, MRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetInwordDetailsReport(string InwardNo, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter MRepCondition = new SqlParameter(MaterialInwardReg._strCond, SqlDbType.NVarChar);
                MAction.Value = 7;
                MRepCondition.Value = InwardNo;

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReport, MAction, MRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetInwardDetailsReport(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter MRepCondition = new SqlParameter(MaterialInwardReg._strCond, SqlDbType.NVarChar);
                MAction.Value = 3;
                MRepCondition.Value = RepCondition;

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReport, MAction, MRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }


        public DataSet GetReport(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(MaterialInwardReg._strCond, SqlDbType.NVarChar);

                pAction.Value = 6;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, pAction, pRepCondition);

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
                SqlParameter MAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(MaterialInwardReg._strCond, SqlDbType.NVarChar);

                MAction.Value = 6;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, oParmCol);

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

        public DataSet GetSupplierDtls_PONowise(Int32 POId, out string strError,string Type)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(MaterialInwardReg._POId, SqlDbType.BigInt);
                SqlParameter pType = new SqlParameter(MaterialInwardReg._Type, SqlDbType.NVarChar);
                pAction.Value = 9;
                pPOId.Value = POId;
                pType.Value = Type;
                Open(CONNECTION_STRING);
                 SqlParameter[] Param = new SqlParameter[] { pAction, pPOId, pType};
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg,Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetDtls_PONowise(Int32 POId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(MaterialInwardReg._POId, SqlDbType.BigInt);

                pAction.Value = 10;
                pPOId.Value = POId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, pAction, pPOId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetSubUnit(Int32 ItemId,Int32 UnitID,Decimal Qty,String Desc, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(MaterialInwardReg._ItemId, SqlDbType.BigInt);
                SqlParameter pUnitId = new SqlParameter(MaterialInwardReg._UnitId, SqlDbType.BigInt);
                SqlParameter pQty = new SqlParameter(MaterialInwardReg._InwardQty, SqlDbType.Decimal);
                SqlParameter pItemDesc = new SqlParameter(MaterialInwardReg._ItemDesc, SqlDbType.NVarChar);
                pAction.Value = 17;
                pItemId.Value = ItemId;
                pUnitId.Value = UnitID;
                pQty.Value = Qty;
                pItemDesc.Value = Desc;
                SqlParameter[] param = new SqlParameter[] {pAction,pItemId,pUnitId,pQty,pItemDesc };
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetFactor(Int32 CUnitId, Int32 ItemId,Decimal Qty,String Desc,decimal rate,int POId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(MaterialInwardReg._ItemId, SqlDbType.BigInt);
                SqlParameter pCUnitId = new SqlParameter("@ConversionUnitId", SqlDbType.BigInt);
                SqlParameter pQTY = new SqlParameter(MaterialInwardReg._InwardQty, SqlDbType.Decimal);
                SqlParameter pItemDesc = new SqlParameter(MaterialInwardReg._ItemDesc, SqlDbType.NVarChar);
                SqlParameter pInwardRate = new SqlParameter(MaterialInwardReg._InwardRate, SqlDbType.Decimal);
                SqlParameter pPOId = new SqlParameter(MaterialInwardReg._POId, SqlDbType.BigInt);
                pAction.Value = 18;
                pItemId.Value = ItemId;
                pCUnitId.Value = CUnitId;
                pQTY.Value = Qty;
                pItemDesc.Value = Desc;
                pInwardRate.Value = rate;
                pPOId.Value = POId;
                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId, pCUnitId,pQTY,pItemDesc,pInwardRate ,pPOId};

                Open(CONNECTION_STRING);

                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }


        public DataSet ChkPoIdExit(Int32 POId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(MaterialInwardReg._POId, SqlDbType.BigInt);

                pAction.Value = 11;
                pPOId.Value = POId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, pAction, pPOId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet Fill_Items(int CategoryId,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pCategoryId = new SqlParameter("@CategoryId", SqlDbType.BigInt);

                pAction.Value = 5;
                pCategoryId.Value = CategoryId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReport, pAction, pCategoryId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }


        public DataSet Get_BillNo(string BillNo,int SupplierID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pBillNo = new SqlParameter("@BillNo", SqlDbType.BigInt);
                SqlParameter pSuplierId = new SqlParameter("@SuplierId", SqlDbType.BigInt);
                pAction.Value = 14;
                pBillNo.Value = BillNo;
                pSuplierId.Value = SupplierID;

                SqlParameter[] Param = new SqlParameter[] { pAction,pBillNo,pSuplierId};

                Open(CONNECTION_STRING);

                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, Param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetItemName(int catID, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                //SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                ////SqlParameter pCategoryId = new SqlParameter(MaterialInwardReg._CategoryID, SqlDbType.BigInt);

                //pAction.Value = 2;
                //pCategoryId.Value = 1;//catID;
                //Open(CONNECTION_STRING);
                //ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, pAction, pCategoryId);
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

        public DataSet GetReportGridInward(string RepCondition, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCondition = new SqlParameter(MaterialInwardReg._strCond, SqlDbType.NVarChar);
                pAction.Value = 6;
                pCondition.Value = RepCondition;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, pAction, pCondition);
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

        public DataSet Get_Ten_PurchaseRate_Item(int ItemID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(MaterialInwardReg._ItemId, SqlDbType.BigInt);

                pAction.Value = 6;
                pItemId.Value = ItemID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReport, pAction, pItemId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        #endregion

        #region Report
        public DataSet GetInwardForPrint(Int32 InwardId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(MaterialInwardReg._InwardId, SqlDbType.BigInt);

                pAction.Value = 4;
                pInwardId.Value = InwardId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReport, pAction, pInwardId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        #endregion
        public DMMaterialInwardReg()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet FillComboPONO(out string StrError)
        {
            StrError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);

                pAction.Value = 7;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_MaterialInwardReg_ByRohini", pAction);

            }
            catch (Exception ex)
            {
                ErrorLogger errLog = new ErrorLogger();
                errLog.ErrorInfo(ex);
                StrError = ex.Message;

            }
            finally { Close(); }
            return Ds;
        }
    }
}
