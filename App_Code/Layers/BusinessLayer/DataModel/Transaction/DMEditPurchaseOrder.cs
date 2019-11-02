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
    public class DMEditPurchaseOrder : Utility.Setting
    {
        public DMEditPurchaseOrder()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet GetApprovedPurchase_Order(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EditPurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@RptCond", SqlDbType.NVarChar);

                pAction.Value = 1;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EditPurchaseOrder.SP_EditPurchaseOrder, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetAuthorizedPurchase_Order(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EditPurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@RptCond", SqlDbType.NVarChar);

                pAction.Value = 2;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EditPurchaseOrder.SP_EditPurchaseOrder, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public int UpdatePurchase_OrderStatus_Approved(ref EditPurchaseOrder Entity_PurchaseOrder, out string strError)
        {
             int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(EditPurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(EditPurchaseOrder._POId, SqlDbType.BigInt);
                SqlParameter pPOStatus = new SqlParameter(EditPurchaseOrder._POStatus, SqlDbType.NVarChar);
                SqlParameter pApprovedTime = new SqlParameter(EditPurchaseOrder._ApprovedTime, SqlDbType.DateTime);                
                SqlParameter pUpdatedBy = new SqlParameter(EditPurchaseOrder._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(EditPurchaseOrder._LoginDate, SqlDbType.DateTime);

                pAction.Value=3;
                pPOId.Value=Entity_PurchaseOrder.POId;
                pPOStatus.Value = Entity_PurchaseOrder.POStatus;
                pApprovedTime.Value = Entity_PurchaseOrder.ApprovedTime;                
                pUpdatedBy.Value = Entity_PurchaseOrder.UserId;
                pUpdatedDate.Value = Entity_PurchaseOrder.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction,pPOStatus,pApprovedTime,pPOId, pUpdatedBy, pUpdatedDate };


                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, EditPurchaseOrder.SP_EditPurchaseOrder, Param);

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

        public int UpdatePurchase_OrderStatus_Authroized(ref EditPurchaseOrder Entity_PurchaseOrder, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(EditPurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter(EditPurchaseOrder._POId, SqlDbType.BigInt);
                SqlParameter pPOStatus = new SqlParameter(EditPurchaseOrder._POStatus, SqlDbType.NVarChar);                
                SqlParameter pAuthorizedTime = new SqlParameter(EditPurchaseOrder._AuthorizedTime, SqlDbType.DateTime);
                SqlParameter pUpdatedBy = new SqlParameter(EditPurchaseOrder._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(EditPurchaseOrder._LoginDate, SqlDbType.DateTime);

                pAction.Value = 6;
                pPOId.Value = Entity_PurchaseOrder.POId;
                pPOStatus.Value = Entity_PurchaseOrder.POStatus;                
                pAuthorizedTime.Value = Entity_PurchaseOrder.AuthorizedTime;
                pUpdatedBy.Value = Entity_PurchaseOrder.UserId;
                pUpdatedDate.Value = Entity_PurchaseOrder.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pPOStatus, pAuthorizedTime, pPOId, pUpdatedBy, pUpdatedDate };


                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, EditPurchaseOrder.SP_EditPurchaseOrder, Param);

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

        public DataSet GetPurchase_Order(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EditPurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@strCond", SqlDbType.NVarChar);

                pAction.Value = 4;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EditPurchaseOrder.SP_EditPurchaseOrder, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetPurchase_OrderMISC(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EditPurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@strCond", SqlDbType.NVarChar);

                pAction.Value = 8;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EditPurchaseOrder.SP_EditPurchaseOrder, pAction, pRepCondition);

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
                SqlParameter MAction = new SqlParameter(EditPurchaseOrder._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(EditPurchaseOrder._strCond, SqlDbType.NVarChar);

                MAction.Value = 2;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, EditPurchaseOrder.SP_EditPurchaseOrder, oParmCol);

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

        public string[] GetSuggestedRecordItemWise(string prefixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;
            try
            {

                // -- For Checking OF Execution of Procedure=========
                SqlParameter MAction = new SqlParameter(EditPurchaseOrder._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(EditPurchaseOrder._strCond, SqlDbType.NVarChar);

                MAction.Value = 8;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, EditPurchaseOrder.SP_EditPurchaseOrder, oParmCol);

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

        public DataSet GetRequisitionDtls_SupplierWise(Int32 ItemId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EditPurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pSupplierID = new SqlParameter(EditPurchaseOrder._POId, SqlDbType.BigInt);

                pAction.Value = 5;
                pSupplierID.Value = ItemId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EditPurchaseOrder.SP_EditPurchaseOrder, pAction, pSupplierID);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetRateCompare(string Cond,Decimal NewRate,int POID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EditPurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOID = new SqlParameter(EditPurchaseOrder._POId, SqlDbType.BigInt);
                SqlParameter pRate = new SqlParameter(EditPurchaseOrder._Rate, SqlDbType.Decimal);
                SqlParameter pstrCond = new SqlParameter(EditPurchaseOrder._strCond, SqlDbType.NVarChar);

                pAction.Value = 9;
                pPOID.Value = POID;
               pRate.Value = NewRate;

               // Orignal  pRate.Value = 0;
                pstrCond.Value = Cond;
                SqlParameter[] param = new SqlParameter[] {pAction,pRate,pstrCond,pPOID };
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, EditPurchaseOrder.SP_EditPurchaseOrder, param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetRateCompareVertically(string Cond, Decimal NewRate, int POID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(EditPurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter pPOID = new SqlParameter(EditPurchaseOrder._POId, SqlDbType.BigInt);
                SqlParameter pRate = new SqlParameter(EditPurchaseOrder._Rate, SqlDbType.Decimal);
                SqlParameter pstrCond = new SqlParameter(EditPurchaseOrder._strCond, SqlDbType.NVarChar);

                pAction.Value = 10;
                pPOID.Value = POID;
                pRate.Value = NewRate;
                pstrCond.Value = Cond;
                SqlParameter[] param = new SqlParameter[] { pAction, pRate, pstrCond, pPOID };
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, EditPurchaseOrder.SP_EditPurchaseOrder, param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close();}
            return Ds;

        }


        public int UpdateRecord(int POID, decimal QTY, int USERID, DateTime LoginDate, decimal Amount,int ItemId,string Remark,int PODtlsID, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pPOId = new SqlParameter("@POId", SqlDbType.BigInt);
                SqlParameter pPOQTY = new SqlParameter("@POQTY", SqlDbType.Decimal);
                SqlParameter pUSERID = new SqlParameter("@UserId", SqlDbType.BigInt);
                SqlParameter pDATE = new SqlParameter("@LoginDate", SqlDbType.DateTime);
                SqlParameter pAmount = new SqlParameter("@Amount", SqlDbType.Decimal);
                SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);
                SqlParameter pRemark = new SqlParameter("@Remark", SqlDbType.NVarChar);
                SqlParameter pPODtlsId = new SqlParameter("@SuplierId", SqlDbType.BigInt);
                pAction.Value = 7;
                pPOId.Value = POID ;
                pPOQTY.Value = QTY;
                pUSERID.Value = USERID;
                pDATE.Value = LoginDate;
                pAmount.Value = Amount;
                pItemId.Value = ItemId;
                pRemark.Value = Remark;
                pPODtlsId.Value = PODtlsID;
                SqlParameter[] Param = new SqlParameter[] { pAction, pPOId, pPOQTY, pUSERID, pDATE, pAmount, pItemId, pRemark ,pPODtlsId};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, EditPurchaseOrder.SP_EditPurchaseOrder, Param);

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


    }
}