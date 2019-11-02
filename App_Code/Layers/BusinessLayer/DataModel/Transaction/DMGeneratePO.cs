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
/// Summary description for DMGeneratePO
/// </summary>
/// 
namespace MayurInventory.DataModel
{
    public class DMGeneratePO : Utility.Setting
    {
        #region Business Logic
        public int InsertRecord(ref GeneratePO Obj_Entity, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pPONo = new SqlParameter(GeneratePO._CentralPONo, SqlDbType.NVarChar);
                SqlParameter pPODate = new SqlParameter(GeneratePO._CentralPODate, SqlDbType.DateTime);
                SqlParameter pDeptId = new SqlParameter(GeneratePO._DepartmentId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(GeneratePO._ItemId, SqlDbType.BigInt);
                SqlParameter pReqQty = new SqlParameter(GeneratePO._ReqQty, SqlDbType.Decimal);
                SqlParameter pQtyInHand = new SqlParameter(GeneratePO._QtyInHand, SqlDbType.Decimal);
                SqlParameter pMinStkLvl = new SqlParameter(GeneratePO._MinStockLevel, SqlDbType.Decimal);
                SqlParameter pLocationId = new SqlParameter(GeneratePO._LocationId, SqlDbType.Decimal);
                SqlParameter pTransitQty = new SqlParameter(GeneratePO._TransitQty, SqlDbType.Decimal);
                SqlParameter pVendor = new SqlParameter(GeneratePO._Vendor, SqlDbType.NVarChar);
                SqlParameter pQtyToOrder = new SqlParameter(GeneratePO._QtyToOrder, SqlDbType.Decimal);
                SqlParameter pQtyOrder = new SqlParameter(GeneratePO._OrderQty, SqlDbType.Decimal);
                SqlParameter pTotalAmount = new SqlParameter(GeneratePO._TotalAmount, SqlDbType.Decimal);

                SqlParameter pUserId = new SqlParameter(GeneratePO._UserId, SqlDbType.BigInt);
                SqlParameter pLoginDate = new SqlParameter(GeneratePO._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(GeneratePO._IsDeleted, SqlDbType.Bit);

                pAction.Value = 1;
                pPONo.Value = Obj_Entity.CentralPONo;
                pPODate.Value = Obj_Entity.CentralPODate;
                pDeptId.Value = Obj_Entity.DepartmentId;
                pItemId.Value = Obj_Entity.ItemId;
                pReqQty.Value = Obj_Entity.ReqQty;
                pQtyInHand.Value = Obj_Entity.QtyInHand;
                pMinStkLvl.Value = Obj_Entity.MinStockLevel;
                pLocationId.Value = Obj_Entity.LocationId;
                pTransitQty.Value = Obj_Entity.TransitQty;
                pVendor.Value = Obj_Entity.Vendor;
                pQtyToOrder.Value = Obj_Entity.QtyToOrder;
                pQtyOrder.Value = Obj_Entity.OrderQty;
                pTotalAmount.Value = Obj_Entity.TotalAmount;

                pUserId.Value = Obj_Entity.UserId;
                pLoginDate.Value = Obj_Entity.LoginDate;
                pIsDeleted.Value = Obj_Entity.IsDeleted;

                SqlParameter[] Param = new SqlParameter[]{pAction,pPONo,pPODate,pDeptId,pItemId,pReqQty,pQtyInHand,pMinStkLvl,
                pLocationId,pTransitQty,pVendor,pQtyToOrder,pQtyOrder,pTotalAmount,pUserId,pLoginDate,pIsDeleted};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, GeneratePO.SP_GeneratePO, Param);

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

        public int UpdateRecord(ref GeneratePO Obj_Entity, out string StrError)
        {
            int iUpdate = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCentralPOId= new SqlParameter(GeneratePO._CentralPOId, SqlDbType.BigInt);
                SqlParameter pPONo = new SqlParameter(GeneratePO._CentralPONo, SqlDbType.NVarChar);
                SqlParameter pPODate = new SqlParameter(GeneratePO._CentralPODate, SqlDbType.DateTime);
                SqlParameter pDeptId = new SqlParameter(GeneratePO._DepartmentId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(GeneratePO._ItemId, SqlDbType.BigInt);
                SqlParameter pReqQty = new SqlParameter(GeneratePO._ReqQty, SqlDbType.Decimal);
                SqlParameter pQtyInHand = new SqlParameter(GeneratePO._QtyInHand, SqlDbType.Decimal);
                SqlParameter pMinStkLvl = new SqlParameter(GeneratePO._MinStockLevel, SqlDbType.Decimal);
                SqlParameter pLocationId = new SqlParameter(GeneratePO._LocationId, SqlDbType.Decimal);
                SqlParameter pTransitQty = new SqlParameter(GeneratePO._TransitQty, SqlDbType.Decimal);
                SqlParameter pVendor = new SqlParameter(GeneratePO._Vendor, SqlDbType.NVarChar);
                SqlParameter pQtyToOrder = new SqlParameter(GeneratePO._QtyToOrder, SqlDbType.Decimal);
                SqlParameter pQtyOrder = new SqlParameter(GeneratePO._OrderQty, SqlDbType.Decimal);
                SqlParameter pTotalAmount = new SqlParameter(GeneratePO._TotalAmount, SqlDbType.Decimal);

                SqlParameter pUserId = new SqlParameter(GeneratePO._UserId, SqlDbType.BigInt);
                SqlParameter pLoginDate = new SqlParameter(GeneratePO._LoginDate, SqlDbType.DateTime);
                
                pAction.Value = 2;
                pCentralPOId.Value = Obj_Entity.CentralPOId;
                pPONo.Value = Obj_Entity.CentralPONo;
                pPODate.Value = Obj_Entity.CentralPODate;
                pDeptId.Value = Obj_Entity.DepartmentId;
                pItemId.Value = Obj_Entity.ItemId;
                pReqQty.Value = Obj_Entity.ReqQty;
                pQtyInHand.Value = Obj_Entity.QtyInHand;
                pMinStkLvl.Value = Obj_Entity.MinStockLevel;
                pLocationId.Value = Obj_Entity.LocationId;
                pTransitQty.Value = Obj_Entity.TransitQty;
                pVendor.Value = Obj_Entity.Vendor;
                pQtyToOrder.Value = Obj_Entity.QtyToOrder;
                pQtyOrder.Value = Obj_Entity.OrderQty;
                pTotalAmount.Value = Obj_Entity.TotalAmount;

                pUserId.Value = Obj_Entity.UserId;
                pLoginDate.Value = Obj_Entity.LoginDate;
               

                SqlParameter[] Param = new SqlParameter[]{pAction,pCentralPOId,pPONo,pPODate,pDeptId,pItemId,pReqQty,pQtyInHand,pMinStkLvl,
                pLocationId,pTransitQty,pVendor,pQtyToOrder,pQtyOrder,pTotalAmount,pUserId,pLoginDate};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iUpdate = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, GeneratePO.SP_GeneratePO, Param);

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

        public int DeleteRecord(ref GeneratePO Obj_Entity, out string StrError)
        {
            int iDelete = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCentralPOId = new SqlParameter(GeneratePO._CentralPOId, SqlDbType.BigInt);

                SqlParameter pIsDeleted = new SqlParameter(GeneratePO._IsDeleted, SqlDbType.Bit);
                SqlParameter pUserId = new SqlParameter(GeneratePO._UserId, SqlDbType.BigInt);
                SqlParameter pLoginDate = new SqlParameter(GeneratePO._LoginDate, SqlDbType.DateTime);

                pAction.Value = 3;
                pCentralPOId.Value = Obj_Entity.CentralPOId;
                pIsDeleted.Value = Obj_Entity.IsDeleted;
                pUserId.Value = Obj_Entity.UserId;
                pLoginDate.Value = Obj_Entity.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pCentralPOId, pIsDeleted, pUserId, pLoginDate };

                Open(CONNECTION_STRING);
                BeginTransaction();
                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, GeneratePO.SP_GeneratePO, Param);

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

        public int InsertVendorDtls(ref GeneratePO Obj_Entity, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action",SqlDbType.BigInt);
                SqlParameter pCentralPOId = new SqlParameter(GeneratePO._CentralPOId,SqlDbType.BigInt);
                SqlParameter pVendorId = new SqlParameter(GeneratePO._VendorId,SqlDbType.BigInt);
                SqlParameter pOrderQtyDist = new SqlParameter(GeneratePO._OrderQtyDist,SqlDbType.Decimal);
                SqlParameter pExpectedAmt = new SqlParameter(GeneratePO._ExpectedAmt,SqlDbType.Decimal);
                SqlParameter pPurchaseRate = new SqlParameter(GeneratePO._PurchaseRate, SqlDbType.Decimal);

                pAction.Value = 8;
                pVendorId.Value = Obj_Entity.VendorId;
                pCentralPOId.Value = Obj_Entity.CentralPOId;
                pOrderQtyDist.Value = Obj_Entity.OrderQtyDist;
                pExpectedAmt.Value = Obj_Entity.ExpectedAmt;
                pPurchaseRate.Value = Obj_Entity.PurchaseRate;

                SqlParameter[] param = new SqlParameter[] { pAction, pVendorId, pCentralPOId, pOrderQtyDist, pExpectedAmt, pPurchaseRate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,GeneratePO.SP_GeneratePO,param);

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

        public DataSet GetPODetailsForEdit(int CentralPOId , out string StrError)
        {
            DataSet Ds = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCentralPOId = new SqlParameter(GeneratePO._CentralPOId, SqlDbType.BigInt);

                pAction.Value = 7;
                pCentralPOId.Value = CentralPOId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, GeneratePO.SP_GeneratePO, pAction, pCentralPOId);
            }
            catch (Exception ex)
            {
                StrError = ex.Message;
            }
            finally
            {
                Close();
            }

            return Ds;
        }
              
        public DataSet BindCombo(out string StrError)
        {
            DataSet ds = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action",SqlDbType .BigInt);

                pAction.Value = 5;

                Open(CONNECTION_STRING);

                ds = SQLHelper.GetDataSetSingleParm(_Connection ,_Transaction,CommandType.StoredProcedure,GeneratePO.SP_GeneratePO,pAction);
            }
            catch (Exception ex)
            {
                StrError = ex.Message;
            }
            finally
            {
                Close();
            }
            return ds;
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
                ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, GeneratePO.SP_GeneratePO, pAction);
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

        public DataSet GetPO(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(GeneratePO._strCond, SqlDbType.NVarChar);

                pAction.Value = 6;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, GeneratePO.SP_GeneratePO, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetItemCode(int ItemId,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(PurchaseOrder._ItemId, SqlDbType.BigInt);
               
                pAction.Value = 9;
                pItemId.Value = ItemId;
               
                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId};

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, GeneratePO.SP_GeneratePO, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetPOrate(int ItemId, int SuppId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(GeneratePO._ItemId, SqlDbType.BigInt);
                SqlParameter pVendorId = new SqlParameter(GeneratePO._VendorId, SqlDbType.BigInt);

                pAction.Value = 10;
                pItemId.Value = ItemId;
                pVendorId.Value = SuppId;

                SqlParameter[] Param = new SqlParameter[] { pAction, pItemId, pVendorId };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, GeneratePO.SP_GeneratePO, Param);
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

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, GeneratePO.SP_GeneratePO, oParmCol);

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

        #endregion

        public DMGeneratePO()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
