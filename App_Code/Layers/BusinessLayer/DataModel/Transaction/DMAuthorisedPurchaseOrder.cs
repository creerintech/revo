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
    public class DMAuthorisedPurchaseOrder : Utility.Setting
    {
        #region[Business Logic]

        public DataSet GetItems(string RepCondition, String LOCID, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@Temp", SqlDbType.NVarChar);
                SqlParameter pLOCID = new SqlParameter("@Temp1", SqlDbType.NVarChar);

                pAction.Value = 9;
                pRepCondition.Value = RepCondition;
                pLOCID.Value = LOCID;

                SqlParameter[] param = new SqlParameter[] { pAction, pRepCondition, pLOCID };
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, "SP_EDITAUTHPURCHASEORDER", param);

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


        public string[] GetSuggestedRecordItems(string prefixText, string LOC)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@Temp", SqlDbType.NVarChar);
                SqlParameter pRemark = new SqlParameter("@Temp1", SqlDbType.NVarChar);
                pAction.Value = 8;
                pRepCondition.Value = prefixText;
                pRemark.Value = LOC;

                SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition, pRemark };

                Open(CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, "SP_EDITAUTHPURCHASEORDER", oparamcol);

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


        public int Insert_PurchaseOrderTandC(ref PurchaseOrder Entity_Call,String POPAYMENTTERMS, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(PurchaseOrder._POId, SqlDbType.BigInt);
                SqlParameter pTermsId = new SqlParameter(PurchaseOrder._TermsID, SqlDbType.BigInt);
                SqlParameter pTitle = new SqlParameter(PurchaseOrder._Title, SqlDbType.NVarChar);
                SqlParameter pTermsCondition = new SqlParameter(PurchaseOrder._TermsCondition, SqlDbType.NVarChar);
                SqlParameter pPaymentDesc = new SqlParameter("@PaymentDesc", SqlDbType.NVarChar);
                pAction.Value = 7;
                pPOId.Value = Entity_Call.POId;
                pTermsId.Value = Entity_Call.TermsID;
                pTitle.Value = Entity_Call.Title;
                pTermsCondition.Value = Entity_Call.TermsCondition;
                pPaymentDesc.Value = POPAYMENTTERMS;
                SqlParameter[] Param = new SqlParameter[] { pAction, pPOId, pTermsId, pTitle, pTermsCondition,pPaymentDesc };
                Open(CONNECTION_STRING);
                BeginTransaction();
                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_II, Param);
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

        public int Update_PurchaseOrderTandC(ref PurchaseOrder Entity_Call,string paymentterms, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(PurchaseOrder._POId, SqlDbType.BigInt);
                SqlParameter pTermsId = new SqlParameter(PurchaseOrder._TermsID, SqlDbType.BigInt);
                SqlParameter pTitle = new SqlParameter(PurchaseOrder._Title, SqlDbType.NVarChar);
                SqlParameter pTermsCondition = new SqlParameter(PurchaseOrder._TermsCondition, SqlDbType.NVarChar);
                SqlParameter pPaymentDesc = new SqlParameter("@PaymentDesc", SqlDbType.NVarChar);
                pAction.Value = 6;
                pPOId.Value = Entity_Call.POId;
                pTermsId.Value = Entity_Call.TermsID;
                pTitle.Value = Entity_Call.Title;
                pTermsCondition.Value = Entity_Call.TermsCondition;
                pPaymentDesc.Value = paymentterms;
                SqlParameter[] Param = new SqlParameter[] { pAction, pPOId, pTermsId, pTitle, pTermsCondition,pPaymentDesc };
                Open(CONNECTION_STRING);
                BeginTransaction();
                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, "SP_EDITAUTHPURCHASEORDER", Param);
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
        public DataSet GetTermsAndCondition(out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                pAction.Value = 8;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_I, pAction);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet BindRATES(int ItemID,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(PurchaseOrder._ItemId, SqlDbType.BigInt);
                pAction.Value = 9;
                pItemId.Value = ItemID;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_I, pAction,pItemId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public int InsertRecord(ref PurchaseOrder Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPONo = new SqlParameter(PurchaseOrder._PONo, SqlDbType.NVarChar);
                SqlParameter pPODate = new SqlParameter(PurchaseOrder._PODate, SqlDbType.DateTime);
                SqlParameter pSuplierId = new SqlParameter(PurchaseOrder._SuplierId, SqlDbType.BigInt);
                SqlParameter pBillingAddress = new SqlParameter(PurchaseOrder._BillingAddress, SqlDbType.NVarChar);
                SqlParameter pShippingAddress = new SqlParameter(PurchaseOrder._ShippingAddress, SqlDbType.NVarChar);
                SqlParameter pSubTotal = new SqlParameter(PurchaseOrder._SubTotal, SqlDbType.Decimal);
                SqlParameter pDiscount = new SqlParameter(PurchaseOrder._Discount, SqlDbType.Decimal);
                SqlParameter pVat = new SqlParameter(PurchaseOrder._Vat, SqlDbType.Decimal);
                SqlParameter pGrandTotal = new SqlParameter(PurchaseOrder._GrandTotal, SqlDbType.Decimal);
                SqlParameter pInstruction = new SqlParameter(PurchaseOrder._Instruction, SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(PurchaseOrder._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(PurchaseOrder._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(PurchaseOrder._IsDeleted, SqlDbType.Bit);
               
                pAction.Value = 1;
                pPONo.Value = Entity_Call.PONo;
                pPODate.Value = Entity_Call.PODate;
                pSuplierId.Value = Entity_Call.SuplierId;
                pBillingAddress.Value = Entity_Call.BillingAddress;
                pShippingAddress.Value = Entity_Call.ShippingAddress;
                pSubTotal.Value = Entity_Call.SubTotal;
                pDiscount.Value = Entity_Call.Discount;
                pVat.Value = Entity_Call.Vat;
                pGrandTotal.Value = Entity_Call.GrandTotal;
                pInstruction.Value = Entity_Call.Instruction;
               
                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;
                pIsDeleted.Value = Entity_Call.IsDeleted;

                SqlParameter[] Param = new SqlParameter[] { pAction, pPONo, pPODate, pSuplierId, pBillingAddress, pShippingAddress,
                    pSubTotal,pDiscount,pVat,pGrandTotal,pInstruction,pCreatedBy, pCreatedDate, pIsDeleted };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure,   "SP_EDITAUTHPURCHASEORDER", Param);

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

        public int InsertDetailsRecord(ref PurchaseOrder Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(PurchaseOrder._POId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(PurchaseOrder._ItemId, SqlDbType.BigInt);
                SqlParameter pQty = new SqlParameter(PurchaseOrder._Qty, SqlDbType.Decimal);
                SqlParameter pRate = new SqlParameter(PurchaseOrder._Rate, SqlDbType.Decimal);
                SqlParameter pAmount = new SqlParameter(PurchaseOrder._Amount, SqlDbType.Decimal);
                SqlParameter pTaxPer = new SqlParameter(PurchaseOrder._TaxPer, SqlDbType.Decimal);
                SqlParameter pTaxAmount = new SqlParameter(PurchaseOrder._TaxAmount, SqlDbType.Decimal);
                SqlParameter pNetAmount = new SqlParameter(PurchaseOrder._NetAmount, SqlDbType.Decimal);
                SqlParameter pExptdDate = new SqlParameter(PurchaseOrder._ExptdDate, SqlDbType.DateTime);
                SqlParameter pPurchaseRate = new SqlParameter(PurchaseOrder._PurchaseRate, SqlDbType.Decimal);
                SqlParameter pDeliveryPeriod = new SqlParameter(PurchaseOrder._DeliveryPeriod, SqlDbType.BigInt);
                SqlParameter pQtyInHand = new SqlParameter(PurchaseOrder._QtyInHand, SqlDbType.Decimal);
                SqlParameter pQtyInTransite = new SqlParameter(PurchaseOrder._QtyInTransite, SqlDbType.Decimal);

                pAction.Value = 8;
                pPOId.Value = Entity_Call.POId;
                pItemId.Value = Entity_Call.ItemId;
                pQty.Value = Entity_Call.Qty;
                pRate.Value = Entity_Call.Rate;
                pAmount.Value = Entity_Call.Amount;
                pTaxPer.Value = Entity_Call.TaxPer;
                pTaxAmount.Value = Entity_Call.TaxAmount;
                pNetAmount.Value = Entity_Call.NetAmount;
                pExptdDate.Value = Entity_Call.ExptdDate;
                pPurchaseRate.Value = Entity_Call.PurchaseRate;
                pDeliveryPeriod.Value = Entity_Call.DeliveryPeriod;
                pQtyInHand.Value = Entity_Call.QtyInHand;
                pQtyInTransite.Value = Entity_Call.QtyInTransite;
               
                SqlParameter[] Param = new SqlParameter[] { pAction,pPOId,pItemId,pQty,pRate,pAmount,pTaxPer,pTaxAmount,pNetAmount,
                    pExptdDate,pPurchaseRate,pDeliveryPeriod,pQtyInHand,pQtyInTransite };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure,   "SP_EDITAUTHPURCHASEORDER", Param);

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
        public int DeleteDtlsForUpdate(int PODTLSID, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(PurchaseOrder._PODtlsId, SqlDbType.BigInt);
                pAction.Value = 10;
                pPOId.Value = PODTLSID;
                SqlParameter[] Param = new SqlParameter[] { pAction,pPOId};
                Open(CONNECTION_STRING);
                BeginTransaction();
                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_II, Param);

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
        public int UpdateRecord(ref PurchaseOrder Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(PurchaseOrder._POId, SqlDbType.BigInt);
                SqlParameter pPODate = new SqlParameter(PurchaseOrder._PODate, SqlDbType.DateTime);
                SqlParameter pSubTotal = new SqlParameter(PurchaseOrder._SubTotal, SqlDbType.Decimal);
                SqlParameter pDiscount = new SqlParameter(PurchaseOrder._Discount, SqlDbType.Decimal);
                SqlParameter pVat = new SqlParameter(PurchaseOrder._Vat, SqlDbType.Decimal);
                SqlParameter pGrandTotal = new SqlParameter(PurchaseOrder._GrandTotal, SqlDbType.Decimal);
                SqlParameter pHamaliAmt = new SqlParameter(PurchaseOrder._HamaliAmt, SqlDbType.Decimal);
                SqlParameter pFreightAmt = new SqlParameter(PurchaseOrder._FreightAmt, SqlDbType.Decimal);
                SqlParameter pPostageAmt = new SqlParameter(PurchaseOrder._PostageAmt, SqlDbType.Decimal);
                SqlParameter pOtherCharegs = new SqlParameter(PurchaseOrder._OtherCharges, SqlDbType.Decimal);
                SqlParameter pCompanyID = new SqlParameter(PurchaseOrder._CompanyID, SqlDbType.BigInt);
                SqlParameter pPaymentTerms = new SqlParameter(PurchaseOrder._PaymentTerms, SqlDbType.BigInt);
                SqlParameter pInstruction = new SqlParameter(PurchaseOrder._Instruction, SqlDbType.NVarChar);
                SqlParameter pServiceTaxPer = new SqlParameter(PurchaseOrder._ServiceTaxPer, SqlDbType.Decimal);
                SqlParameter pServiceTaxAmt = new SqlParameter(PurchaseOrder._ServiceTaxAmt, SqlDbType.Decimal);
                SqlParameter pPOQTDATE = new SqlParameter(PurchaseOrder._POQTDATE, SqlDbType.NVarChar);

                SqlParameter pHamaliActual = new SqlParameter(PurchaseOrder._HamaliActual, SqlDbType.BigInt);
                SqlParameter pFreightActual = new SqlParameter(PurchaseOrder._FreightActual, SqlDbType.BigInt);
                SqlParameter pOtherChargeActual = new SqlParameter(PurchaseOrder._OtherChargeActual, SqlDbType.BigInt);
                SqlParameter pLoadingActual = new SqlParameter(PurchaseOrder._LoadingActual, SqlDbType.BigInt);

                SqlParameter pExcisePer = new SqlParameter(PurchaseOrder._ExcisePer, SqlDbType.Decimal);
                SqlParameter pExciseAmount = new SqlParameter(PurchaseOrder._ExciseAmount, SqlDbType.Decimal);

                SqlParameter pUpdatedBy = new SqlParameter(PurchaseOrder._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(PurchaseOrder._LoginDate, SqlDbType.DateTime);
              
                pAction.Value = 4;
                pPOId.Value = Entity_Call.POId;
                pPODate.Value = Entity_Call.PODate;
                pSubTotal.Value = Entity_Call.SubTotal;
                pDiscount.Value = Entity_Call.Discount;
                pVat.Value = Entity_Call.Vat;
                pGrandTotal.Value = Entity_Call.GrandTotal;
                pHamaliAmt.Value = Entity_Call.HamaliAmt;
                pFreightAmt.Value = Entity_Call.FreightAmt;
                pPostageAmt.Value = Entity_Call.PostageAmt;
                pOtherCharegs.Value = Entity_Call.OtherCharges;
                pCompanyID.Value = Entity_Call.CompanyID;
                pPaymentTerms.Value = Entity_Call.PaymentTerms;
                pInstruction.Value = Entity_Call.Instruction;
                pServiceTaxPer.Value = Entity_Call.ServiceTaxPer;
                pServiceTaxAmt.Value = Entity_Call.ServiceTaxAmt;
                pPOQTDATE.Value = Entity_Call.POQTDATE;

                pHamaliActual.Value = Entity_Call.HamaliActual;
                pFreightActual.Value = Entity_Call.FreightActual;
                pOtherChargeActual.Value = Entity_Call.OtherChargeActual;
                pLoadingActual.Value = Entity_Call.LoadingActual;

                pExcisePer.Value = Entity_Call.ExcisePer;
                pExciseAmount.Value = Entity_Call.ExciseAmount;

                pUpdatedBy.Value = Entity_Call.UserId;
                pUpdatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction,pPOId, pPODate, pPOQTDATE,pSubTotal,pDiscount,pVat,pGrandTotal,pInstruction,pUpdatedBy, pUpdatedDate,pServiceTaxPer,pServiceTaxAmt,
                  pHamaliAmt,pFreightAmt,pPostageAmt,pOtherCharegs,pCompanyID,pPaymentTerms
                ,pHamaliActual,pFreightActual,pOtherChargeActual,pLoadingActual,pExcisePer,pExciseAmount};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, "SP_EDITAUTHPURCHASEORDER", Param);

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

        public int DeleteRecord(ref PurchaseOrder Entity_Call, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(PurchaseOrder._POId, SqlDbType.BigInt);
                SqlParameter pDeletedBy = new SqlParameter(PurchaseOrder._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(PurchaseOrder._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(PurchaseOrder._IsDeleted, SqlDbType.Bit);

                pAction.Value = 3;
                pPOId.Value = Entity_Call.POId;
                pIsDeleted.Value = Entity_Call.IsDeleted;

                pDeletedBy.Value = Entity_Call.UserId;
                pDeletedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pPOId, pIsDeleted, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure,   "SP_EDITAUTHPURCHASEORDER", Param);

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

        public DataSet GetPONo(out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                pAction.Value = 4;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure,   "SP_EDITAUTHPURCHASEORDER", pAction);
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

        public DataSet GetPOForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(PurchaseOrder._POId, SqlDbType.BigInt);

                pAction.Value = 7;
                pPOId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_I, pAction, pPOId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetPurchaseOrd(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(PurchaseOrder._strCond, SqlDbType.NVarChar);

                pAction.Value = 6;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure,   "SP_EDITAUTHPURCHASEORDER", pAction, pRepCondition);

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
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);

                pAction.Value = 7;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure,   "SP_EDITAUTHPURCHASEORDER", pAction);
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
                SqlParameter MAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(PurchaseOrder._strCond, SqlDbType.NVarChar);

                MAction.Value = 6;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure,   "SP_EDITAUTHPURCHASEORDER", oParmCol);

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

        public DataSet GetPurchaseRate(int ItemId,int SuppId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(PurchaseOrder._ItemId, SqlDbType.BigInt);
                SqlParameter pSuplierId = new SqlParameter(PurchaseOrder._SuplierId, SqlDbType.BigInt);

                pAction.Value = 9;
                pItemId.Value = ItemId;
                pSuplierId.Value = SuppId;

                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId, pSuplierId };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure,   "SP_EDITAUTHPURCHASEORDER", Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet BindComboBox(out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                pAction.Value = 1;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure,  "SP_EDITAUTHPURCHASEORDER", pAction);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        public DataSet BindVendor(out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);

                pAction.Value = 3;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_I, pAction);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetOrder(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@RptCond", SqlDbType.NVarChar);

                pAction.Value = 2;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_EDITAUTHPURCHASEORDER", pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        public DataSet GetDataForMail(Int32 POID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOID = new SqlParameter(PurchaseOrder._POId, SqlDbType.BigInt);

                pAction.Value = 5;
                pPOID.Value = POID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Report, pAction, pPOID);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetRate_SupplierWise(Int32 ItemId, Int32 SuppId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(PurchaseOrder._ItemId, SqlDbType.BigInt);
                SqlParameter pSuplierId = new SqlParameter(PurchaseOrder._SuplierId, SqlDbType.BigInt);

                pAction.Value = 4;
                pItemId.Value = ItemId;
                pSuplierId.Value = SuppId;

                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId, pSuplierId };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_I, Param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public int UpdateInsert_PurchseOrder(int PONO, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pPONo = new SqlParameter("@POId", SqlDbType.BigInt);
                pAction.Value = 11;
                pPONo.Value = PONO;
                SqlParameter[] Param = new SqlParameter[] { pAction, pPONo};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_II, Param);

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

        public int UpdateInsert_PurchseOrderMAIL(int PONO, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pPONo = new SqlParameter("@POId", SqlDbType.BigInt);
                pAction.Value = 12;
                pPONo.Value = PONO;
                SqlParameter[] Param = new SqlParameter[] { pAction, pPONo };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_II, Param);

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

        public int Insert_PurchseOrder(ref PurchaseOrder Entity_Call,String NO, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPONo = new SqlParameter(PurchaseOrder._PONo, SqlDbType.NVarChar);
                SqlParameter pPODate = new SqlParameter(PurchaseOrder._PODate, SqlDbType.DateTime);
                SqlParameter pSuplierId = new SqlParameter(PurchaseOrder._SuplierId, SqlDbType.BigInt);
                SqlParameter pBillingAddress = new SqlParameter(PurchaseOrder._BillingAddress, SqlDbType.NVarChar);
                SqlParameter pShippingAddress = new SqlParameter(PurchaseOrder._ShippingAddress, SqlDbType.NVarChar);
                SqlParameter pSubTotal = new SqlParameter(PurchaseOrder._SubTotal, SqlDbType.Decimal);
                SqlParameter pDiscount = new SqlParameter(PurchaseOrder._Discount, SqlDbType.Decimal);
                SqlParameter pVat = new SqlParameter(PurchaseOrder._Vat, SqlDbType.Decimal);
                SqlParameter pGrandTotal = new SqlParameter(PurchaseOrder._GrandTotal, SqlDbType.Decimal);
                SqlParameter pPOQTNO = new SqlParameter("@POQTNO", SqlDbType.NVarChar);
                SqlParameter pPOQTDATE = new SqlParameter(PurchaseOrder._POQTDATE, SqlDbType.NVarChar);
                SqlParameter pDekhrakhAmt = new SqlParameter(PurchaseOrder._DekhrekhAmt, SqlDbType.Decimal);
                SqlParameter pHamaliAmt = new SqlParameter(PurchaseOrder._HamaliAmt, SqlDbType.Decimal);
                SqlParameter pCessAmt = new SqlParameter(PurchaseOrder._CESSAmt, SqlDbType.Decimal);
                SqlParameter pFreightAmt = new SqlParameter(PurchaseOrder._FreightAmt, SqlDbType.Decimal);
                SqlParameter pPackingAmt = new SqlParameter(PurchaseOrder._PackingAmt, SqlDbType.Decimal);
                SqlParameter pPostageAmt = new SqlParameter(PurchaseOrder._PostageAmt, SqlDbType.Decimal);
                SqlParameter pOtherCharegs = new SqlParameter(PurchaseOrder._OtherCharges, SqlDbType.Decimal);
                SqlParameter pCompanyID = new SqlParameter(PurchaseOrder._CompanyID, SqlDbType.BigInt);
                SqlParameter pPaymentTerms = new SqlParameter(PurchaseOrder._PaymentTerms, SqlDbType.BigInt);
                SqlParameter pServiceTaxPer = new SqlParameter(PurchaseOrder._ServiceTaxPer, SqlDbType.Decimal);
                SqlParameter pServiceTaxAmt = new SqlParameter(PurchaseOrder._ServiceTaxAmt, SqlDbType.Decimal);

                SqlParameter pHamaliActual = new SqlParameter(PurchaseOrder._HamaliActual, SqlDbType.BigInt);
                SqlParameter pFreightActual = new SqlParameter(PurchaseOrder._FreightActual, SqlDbType.BigInt);
                SqlParameter pOtherChargeActual = new SqlParameter(PurchaseOrder._OtherChargeActual, SqlDbType.BigInt);
                SqlParameter pLoadingActual = new SqlParameter(PurchaseOrder._LoadingActual, SqlDbType.BigInt);

                SqlParameter pExcisePer = new SqlParameter(PurchaseOrder._ExcisePer, SqlDbType.Decimal);
                SqlParameter pExciseAmount = new SqlParameter(PurchaseOrder._ExciseAmount, SqlDbType.Decimal);

                SqlParameter pInstruction = new SqlParameter(PurchaseOrder._Instruction, SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(PurchaseOrder._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(PurchaseOrder._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(PurchaseOrder._IsDeleted, SqlDbType.Bit);

                pAction.Value = 1;
                pPONo.Value = Entity_Call.PONo;
                pPODate.Value = Entity_Call.PODate;
                pSuplierId.Value = Entity_Call.SuplierId;                
                pBillingAddress.Value = Entity_Call.BillingAddress;
                pShippingAddress.Value = Entity_Call.ShippingAddress;
                pSubTotal.Value = Entity_Call.SubTotal;
                pDiscount.Value = Entity_Call.Discount;
                pVat.Value = Entity_Call.Vat;
                pGrandTotal.Value = Entity_Call.GrandTotal;
                pPOQTNO.Value = NO;
                pPOQTDATE.Value = Entity_Call.POQTDATE;
                pDekhrakhAmt.Value = Entity_Call.DekhrekhAmt;
                pHamaliAmt.Value = Entity_Call.HamaliAmt;
                pCessAmt.Value = Entity_Call.CESSAmt;
                pFreightAmt.Value = Entity_Call.FreightAmt;
                pPackingAmt.Value = Entity_Call.PackingAmt;
                pPostageAmt.Value = Entity_Call.PostageAmt;
                pOtherCharegs.Value = Entity_Call.OtherCharges;
                pCompanyID.Value = Entity_Call.CompanyID;
                pPaymentTerms.Value = Entity_Call.PaymentTerms;
                pServiceTaxPer.Value = Entity_Call.ServiceTaxPer;
                pServiceTaxAmt.Value = Entity_Call.ServiceTaxAmt;

                pInstruction.Value = Entity_Call.Instruction;
                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;
                pIsDeleted.Value = Entity_Call.IsDeleted;

                pHamaliActual.Value = Entity_Call.HamaliActual;
                pFreightActual.Value = Entity_Call.FreightActual;
                pOtherChargeActual.Value = Entity_Call.OtherChargeActual;
                pLoadingActual.Value = Entity_Call.LoadingActual;
                pExcisePer.Value = Entity_Call.ExcisePer;
                pExciseAmount.Value = Entity_Call.ExciseAmount;

                SqlParameter[] Param = new SqlParameter[] { pAction, pPONo, pPODate,pSuplierId, pBillingAddress, pShippingAddress,
                pPOQTDATE, pSubTotal,pDiscount,pVat,pGrandTotal,pInstruction,pCreatedBy, pCreatedDate, pIsDeleted,pPOQTNO,pServiceTaxPer,pServiceTaxAmt,
                pDekhrakhAmt,pHamaliAmt,pCessAmt,pFreightAmt,pPackingAmt,pPostageAmt,pOtherCharegs,pCompanyID,pPaymentTerms
                ,pHamaliActual,pFreightActual,pOtherChargeActual,pLoadingActual,pExcisePer,pExciseAmount};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_II, Param);

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

        public int Insert_PurchaseOrderDtls(ref PurchaseOrder Entity_Call,int ID, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(PurchaseOrder._POId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(PurchaseOrder._ItemId, SqlDbType.BigInt);                
                SqlParameter pQty = new SqlParameter(PurchaseOrder._Qty, SqlDbType.Decimal);
                SqlParameter pRate = new SqlParameter(PurchaseOrder._Rate, SqlDbType.Decimal);
                SqlParameter pAmount = new SqlParameter(PurchaseOrder._Amount, SqlDbType.Decimal);
                SqlParameter pTaxPer = new SqlParameter(PurchaseOrder._TaxPer, SqlDbType.Decimal);
                SqlParameter pTaxAmount = new SqlParameter(PurchaseOrder._TaxAmount, SqlDbType.Decimal);
                SqlParameter pNetAmount = new SqlParameter(PurchaseOrder._NetAmount, SqlDbType.Decimal);

                SqlParameter pDISCPer = new SqlParameter(PurchaseOrder._DiscPer, SqlDbType.Decimal);
                SqlParameter pDISCAmount = new SqlParameter(PurchaseOrder._DiscAmt, SqlDbType.Decimal);

                SqlParameter pRequisitionCafeId = new SqlParameter(PurchaseOrder._RequisitionCafeId, SqlDbType.BigInt);
                SqlParameter pFK_ItemDtlsId = new SqlParameter("@FK_ItemDtlsId", SqlDbType.BigInt);
                SqlParameter pFK_UnitConvDtlsId = new SqlParameter(PurchaseOrder._FK_UnitConvDtlsId, SqlDbType.BigInt);
                SqlParameter pMainQty = new SqlParameter(PurchaseOrder._MainQty, SqlDbType.Decimal);
                SqlParameter pRemarkForPO = new SqlParameter(PurchaseOrder._RemarkForPO, SqlDbType.NVarChar);
                SqlParameter pCHKFLAG = new SqlParameter(PurchaseOrder._CHKFLAG, SqlDbType.BigInt);

                pAction.Value = 5;
                pPOId.Value = Entity_Call.POId;
                pItemId.Value = Entity_Call.ItemId;                
                pQty.Value = Entity_Call.Qty;
                pRate.Value = Entity_Call.Rate;
                pAmount.Value = Entity_Call.Amount;
                pTaxPer.Value = Entity_Call.TaxPer;
                pTaxAmount.Value = Entity_Call.TaxAmount;

                pDISCPer.Value = Entity_Call.DiscPer;
                pDISCAmount.Value = Entity_Call.DiscAmt;

                pNetAmount.Value = Entity_Call.NetAmount;
                pRequisitionCafeId.Value = Entity_Call.RequisitionCafeId;
                pFK_ItemDtlsId.Value = ID;
                pFK_UnitConvDtlsId.Value = Entity_Call.FK_UnitConvDtlsId;
                pMainQty.Value = Entity_Call.MainQty;
                pRemarkForPO.Value = Entity_Call.RemarkForPO;
                pCHKFLAG.Value = Entity_Call.CHKFLAG;
                SqlParameter[] Param = new SqlParameter[] { pAction,pPOId,pItemId,pQty,pRate,pAmount,pDISCPer,pDISCAmount,
                                                            pTaxPer,pTaxAmount,pNetAmount,pRequisitionCafeId,pFK_ItemDtlsId
                ,pFK_UnitConvDtlsId,pMainQty,pRemarkForPO,pCHKFLAG};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, "SP_EDITAUTHPURCHASEORDER", Param);

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

        public int Update_PurchaseOrderDtls(ref PurchaseOrder Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(PurchaseOrder._PODtlsId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(PurchaseOrder._ItemId, SqlDbType.BigInt);
                SqlParameter pQty = new SqlParameter(PurchaseOrder._Qty, SqlDbType.Decimal);
                SqlParameter pRate = new SqlParameter(PurchaseOrder._Rate, SqlDbType.Decimal);
                SqlParameter pAmount = new SqlParameter(PurchaseOrder._Amount, SqlDbType.Decimal);
                SqlParameter pTaxPer = new SqlParameter(PurchaseOrder._TaxPer, SqlDbType.Decimal);
                SqlParameter pTaxAmount = new SqlParameter(PurchaseOrder._TaxAmount, SqlDbType.Decimal);

                SqlParameter pDISCPer = new SqlParameter(PurchaseOrder._DiscPer, SqlDbType.Decimal);
                SqlParameter pDISCAmount = new SqlParameter(PurchaseOrder._DiscAmt, SqlDbType.Decimal);

                SqlParameter pNetAmount = new SqlParameter(PurchaseOrder._NetAmount, SqlDbType.Decimal);

                pAction.Value = 6;
                pPOId.Value = Entity_Call.PODtlsId;
                pItemId.Value = Entity_Call.ItemId;
                pQty.Value = Entity_Call.Qty;
                pRate.Value = Entity_Call.Rate;
                pAmount.Value = Entity_Call.Amount;
                pTaxPer.Value = Entity_Call.TaxPer;
                pTaxAmount.Value = Entity_Call.TaxAmount;

                pDISCPer.Value = Entity_Call.DiscPer;
                pDISCAmount.Value = Entity_Call.DiscAmt;

                pNetAmount.Value = Entity_Call.NetAmount;

                SqlParameter[] Param = new SqlParameter[] { pAction,pPOId,pItemId,pQty,pRate,pAmount,pDISCPer,pDISCAmount,
                                                            pTaxPer,pTaxAmount,pNetAmount};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_II, Param);

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

        public int Delete_PurchaseOrder(ref PurchaseOrder Entity_Call, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(PurchaseOrder._POId, SqlDbType.BigInt);
                SqlParameter pDeletedBy = new SqlParameter(PurchaseOrder._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(PurchaseOrder._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(PurchaseOrder._IsDeleted, SqlDbType.Bit);

                pAction.Value = 3;
                pPOId.Value = Entity_Call.POId;
                pIsDeleted.Value = Entity_Call.IsDeleted;

                pDeletedBy.Value = Entity_Call.UserId;
                pDeletedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pPOId, pIsDeleted, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_II, Param);

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

        public DataSet GetPurchase_Order(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@RptCond", SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_II, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetCancelPurchase_Order(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@RptCond", SqlDbType.NVarChar);

                pAction.Value = 15;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_II, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetRequisitionDtls_ItemWise(Int32 ItemId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(PurchaseOrder._ItemId, SqlDbType.BigInt);                

                pAction.Value = 5;
                pItemId.Value = ItemId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_I, pAction, pItemId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet PrintPOReport_SupplierWise(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pPOId = new SqlParameter(PurchaseOrder._POId, SqlDbType.BigInt);
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);

                pAction.Value = 6;
                pPOId.Value = ID;

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_I, pAction, pPOId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        public DataSet GetPOList(string RepCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter PstrCond = new SqlParameter(PurchaseOrder._strCond, SqlDbType.NVarChar);
                pAction.Value = 6;
                PstrCond.Value = RepCondition;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure,   "SP_EDITAUTHPURCHASEORDER", pAction, PstrCond);
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



        public int Cancel_AuthPurchseOrder(int PONO,int UserID,String Remark, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pPONo = new SqlParameter("@POId", SqlDbType.BigInt);
                SqlParameter pUserId = new SqlParameter("@UserId", SqlDbType.BigInt);
                SqlParameter pRemark = new SqlParameter("@Remark", SqlDbType.NVarChar);
                pAction.Value = 11;
                pPONo.Value = PONO;
                pUserId.Value = UserID;
                pRemark.Value = Remark;
                SqlParameter[] Param = new SqlParameter[] { pAction, pPONo,pUserId,pRemark };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure,   "SP_EDITAUTHPURCHASEORDER", Param);

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

        #region [Report]

        public DataSet GetPOForReport(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@StrCond", SqlDbType.NVarChar);

                pAction.Value = 1;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Report, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetPOForDetailReport(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@StrCond", SqlDbType.NVarChar);

                pAction.Value = 3;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Report, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet FillComboReport(int userId,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pUserId = new SqlParameter("@UserId", SqlDbType.BigInt);

                pAction.Value = 2;
                pUserId.Value = userId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Report, pAction,pUserId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetPOForPrint(Int32 POId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(PurchaseOrder._POId, SqlDbType.BigInt);

                pAction.Value = 4;
                pPOId.Value = POId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Report, pAction, pPOId);

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
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Report, pAction, pCategoryId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet Fill_ItemsFromSUBCAT(int CategoryId, int SubCategoryId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pCategoryId = new SqlParameter("@CategoryId", SqlDbType.BigInt);
                SqlParameter pSubCategoryId = new SqlParameter("@SubCategoryId", SqlDbType.BigInt);

                pAction.Value = 10;
                pCategoryId.Value = CategoryId;
                pSubCategoryId.Value = SubCategoryId;
                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] {pAction,pCategoryId,pSubCategoryId };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Report, param);

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
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Report, pAction);

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

                pAction.Value = 8;
                pItemId.Value = ItemId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Report, pAction, pItemId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }


        public DataSet GetPoDtls(int POID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter("@POId", SqlDbType.BigInt);

                pAction.Value = 10;
                pPOId.Value = POID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure,   "SP_EDITAUTHPURCHASEORDER", pAction, pPOId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetSupplierTerms(int POID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter("@POId", SqlDbType.BigInt);

                pAction.Value = 12;
                pPOId.Value = POID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure,   "SP_EDITAUTHPURCHASEORDER", pAction, pPOId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GETACCESS(int UserID,string Password, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pUserId = new SqlParameter("@UserId", SqlDbType.BigInt);
                SqlParameter pPassword = new SqlParameter("@Password", SqlDbType.NVarChar);
                pAction.Value = 7;
                pUserId.Value = UserID;
                pPassword.Value = Password;
                SqlParameter[] param = new SqlParameter[] { pAction,pUserId,pPassword};
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure,"SP_EDITAUTHPURCHASEORDER", param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GETUNITCONVERT(int UnitID, int ItemID, decimal Qty,int ItemDtlsId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pPOID = new SqlParameter("@POId", SqlDbType.BigInt);
                SqlParameter pItemID = new SqlParameter("@ItemId", SqlDbType.BigInt);
                SqlParameter pQty = new SqlParameter("@Qty", SqlDbType.Decimal);
                SqlParameter pItemDtlsId = new SqlParameter("@FK_ItemDtlsId", SqlDbType.BigInt);
                pAction.Value = 3;
                pItemID.Value = ItemID;
                pPOID.Value = UnitID;
                pQty.Value = Qty;
                pItemDtlsId.Value = ItemDtlsId;
                SqlParameter[] param = new SqlParameter[] { pAction, pItemID, pPOID, pQty,pItemDtlsId };
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, "SP_EDITAUTHPURCHASEORDER", param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        } 

        public DataSet Get_ItemWiseOrd(int ItemId, int SuplierId, decimal ItemRate, decimal OrdQty, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);                
                SqlParameter pSuplierId = new SqlParameter("@SuplierId", SqlDbType.BigInt);
                SqlParameter pItemRate = new SqlParameter("@ItemRate", SqlDbType.Decimal);
                SqlParameter pOrdQty = new SqlParameter("@OrdQty", SqlDbType.Decimal);

                pAction.Value = 9;
                pItemId.Value = ItemId;
                pSuplierId.Value = SuplierId;
                pItemRate.Value = ItemRate;
                pOrdQty.Value = OrdQty;

                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId, pSuplierId, pItemRate, pOrdQty};

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Report, Param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetPOForStatusReport(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@StrCond", SqlDbType.NVarChar);

                pAction.Value = 11;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Report, pAction, pRepCondition);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        #endregion
        public DMAuthorisedPurchaseOrder()
        {
        }
    }
}
