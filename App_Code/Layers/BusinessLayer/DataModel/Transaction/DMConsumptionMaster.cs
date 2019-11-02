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

 
    public class DMConsumptionMaster:Utility.Setting
    {
        public DMConsumptionMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet ChkIssueExit(int IssueID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(ConsumptionMaster._IssueId, SqlDbType.BigInt);

                pAction.Value = 11;
                pInwardId.Value = IssueID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, pAction, pInwardId);
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
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ConsumptionMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 9;
           
                pRepCondition.Value = RepCondition;

                 SqlParameter[] Param = new SqlParameter[] { pAction, pRepCondition};
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster,Param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetDetailOnCondForIssue(int RepCondition,int LocationId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter("@IssueId", SqlDbType.BigInt);
                SqlParameter pLocationId = new SqlParameter("@LocationId", SqlDbType.BigInt);

                pAction.Value = 4;
                pRepCondition.Value = RepCondition;
                pLocationId.Value = LocationId;

                SqlParameter[] Param = new SqlParameter[] { pAction, pRepCondition,pLocationId };
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetDetailOnCondForIssuePresent(string RepCondition, string LocationId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ConsumptionMaster._strCond, SqlDbType.NVarChar);
                SqlParameter pLocationId = new SqlParameter("@Status", SqlDbType.NVarChar);

                pAction.Value = 20;
                pRepCondition.Value = RepCondition;
                pLocationId.Value = LocationId;

                SqlParameter[] Param = new SqlParameter[] { pAction, pRepCondition, pLocationId };
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, Param);
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
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ConsumptionMaster._strCond, SqlDbType.NVarChar);
                pAction.Value = 10;
                pRepCondition.Value = inwardid;
                SqlParameter[] Param = new SqlParameter[] { pAction, pRepCondition };
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, Param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        public DataSet GetConsumptionNo(out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                pAction.Value = 2;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, pAction);
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

        public DataSet GetConsumption(string RepCondition,String LocID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ConsumptionMaster._strCond, SqlDbType.NVarChar);
                SqlParameter pLocationId = new SqlParameter("@COND", SqlDbType.NVarChar);

                pAction.Value = 3;
                pRepCondition.Value = RepCondition;
                pLocationId.Value = LocID;
                SqlParameter[] Param = new SqlParameter[] { pAction, pRepCondition,pLocationId };                
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, Param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }


        public DataSet FillComboIssueCond(string FromDate,string Outdate,string COND,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pFromDate = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                SqlParameter pToDate = new SqlParameter("@ToDate", SqlDbType.NVarChar);
                SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);
                pAction.Value = 11;
                pFromDate.Value = FromDate;
                pToDate.Value = Outdate;
                pCOND.Value = COND;
                SqlParameter[] Param = new SqlParameter[] { pAction, pFromDate, pToDate,pCOND};     
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
         
            return Ds;
        }


        public DataSet FillCombo(String Location,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pLocationId = new SqlParameter("@COND", SqlDbType.NVarChar);
                pAction.Value = 1;
                pLocationId.Value = Location;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, pAction,pLocationId);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }


        public int InsertRecord(ref ConsumptionMaster Entity_StockMaster,int LOCID, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pStockNo = new SqlParameter(ConsumptionMaster._ConsumptionNo, SqlDbType.NVarChar);
                SqlParameter pStockDate = new SqlParameter(ConsumptionMaster._ConsumptionAsOn, SqlDbType.DateTime);
                SqlParameter pConsumptionDate = new SqlParameter(ConsumptionMaster._ConsumptionDate, SqlDbType.DateTime);
                SqlParameter pIssueId = new SqlParameter("@IssueId", SqlDbType.BigInt);
                SqlParameter pCreatedBy = new SqlParameter(ConsumptionMaster._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(ConsumptionMaster._LoginDate, SqlDbType.DateTime);
                SqlParameter pLOCID = new SqlParameter("@LOCID", SqlDbType.BigInt);

                pAction.Value = 5;
                pStockNo.Value = Entity_StockMaster.ConsumptionNo;
                pStockDate.Value = Entity_StockMaster.ConsumptionAsOn;
                pConsumptionDate.Value = Entity_StockMaster.ConsumptionAsOn;
                pIssueId.Value = Entity_StockMaster.IssueId;
                pCreatedBy.Value = Entity_StockMaster.UserId;
                pCreatedDate.Value = Entity_StockMaster.LoginDate;
                pLOCID.Value = LOCID;

                SqlParameter[] Param = new SqlParameter[] { pAction, pStockNo,pIssueId ,pConsumptionDate,pStockDate,pCreatedBy, pCreatedDate,pLOCID};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, Param);

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

        public int InsertDetailsRecord(ref ConsumptionMaster Entity_StockMaster,int UNITID, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
             
                SqlParameter pOutwardId = new SqlParameter(ConsumptionMaster._ConsumptionId, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(ConsumptionMaster._IssueId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(ConsumptionMaster._ItemId, SqlDbType.BigInt);

                SqlParameter pItemDetailsId = new SqlParameter(ConsumptionMaster._ItemDetailsId, SqlDbType.BigInt);
                SqlParameter pItemDesc = new SqlParameter(ConsumptionMaster._ItemDesc, SqlDbType.NVarChar);

                SqlParameter pInwardQty = new SqlParameter(ConsumptionMaster._IssueQty, SqlDbType.Decimal);
                SqlParameter pOutwardQty = new SqlParameter(ConsumptionMaster._ConsumeQty, SqlDbType.Decimal);
                SqlParameter pPendingQty = new SqlParameter(ConsumptionMaster._PendingQty, SqlDbType.Decimal);
                SqlParameter pLocationId= new SqlParameter(ConsumptionMaster._LocationId, SqlDbType.BigInt);
                SqlParameter pStockLocationId = new SqlParameter(ConsumptionMaster._StockLocationId, SqlDbType.BigInt);
                SqlParameter pRate = new SqlParameter("@Rate1", SqlDbType.Decimal);
                SqlParameter pAmount = new SqlParameter(ConsumptionMaster._Amount, SqlDbType.Decimal);
                SqlParameter pCreatedDate = new SqlParameter(PurchaseOrder._LoginDate, SqlDbType.DateTime);
                
                SqlParameter pUNITID = new SqlParameter("@UnitConversionId", SqlDbType.BigInt);

                pAction.Value = 6;
                pOutwardId.Value = Entity_StockMaster.ConsumptionId;
                pInwardId.Value = Entity_StockMaster.IssueId;
                pItemId.Value = Entity_StockMaster.ItemId;

                pItemDetailsId.Value = Entity_StockMaster.ItemDetailsId;
                pItemDesc.Value = Entity_StockMaster.ItemDesc;


                pInwardQty.Value = Entity_StockMaster.IssueQty;
                pOutwardQty.Value = Entity_StockMaster.ConsumeQty;
                pPendingQty.Value = Entity_StockMaster.PendingQty;
                pLocationId.Value = Entity_StockMaster.LocationId;
                pStockLocationId.Value = Entity_StockMaster.StockLocationID;
                pRate.Value = Entity_StockMaster.Rate;
                pAmount.Value = Entity_StockMaster.Amount;
                pCreatedDate.Value = Entity_StockMaster.LoginDate;
                pUNITID.Value = 0;
                //pUNITID.Value = UNITID;
                //pUNITID
                SqlParameter[] Param = new SqlParameter[] { pAction, pOutwardId, pItemId, pInwardId, pInwardQty, pOutwardQty, pPendingQty, pLocationId, pStockLocationId, pRate, pAmount, pCreatedDate,pUNITID,  pItemDetailsId, pItemDesc };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, Param);

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

        public int UpdateRecord(ref ConsumptionMaster Entity_StockMaster, out string strError)
        {

            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pStockNo = new SqlParameter(ConsumptionMaster._ConsumptionNo, SqlDbType.NVarChar);
                SqlParameter pStockDate = new SqlParameter(ConsumptionMaster._ConsumptionAsOn, SqlDbType.DateTime);
                SqlParameter pConsumptionDate = new SqlParameter(ConsumptionMaster._ConsumptionDate, SqlDbType.DateTime);
                SqlParameter pOutwardId = new SqlParameter(ConsumptionMaster._ConsumptionId, SqlDbType.BigInt);
                SqlParameter pUpdatedBy = new SqlParameter(ConsumptionMaster._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(ConsumptionMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 7;
                pStockNo.Value = Entity_StockMaster.ConsumptionNo;
                pStockDate.Value = Entity_StockMaster.ConsumptionAsOn;
                pConsumptionDate.Value = Entity_StockMaster.ConsumptionAsOn;
                pOutwardId.Value = Entity_StockMaster.ConsumptionId;
                pUpdatedBy.Value = Entity_StockMaster.UserId;
                pUpdatedDate.Value = Entity_StockMaster.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction,pStockNo ,pStockDate,pConsumptionDate,pOutwardId,pUpdatedBy, pUpdatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, Param);

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

        public int DeleteRecord(ref ConsumptionMaster Entity_StockMaster, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pOutwardId = new SqlParameter(ConsumptionMaster._ConsumptionId, SqlDbType.BigInt);

                SqlParameter pDeletedBy = new SqlParameter(ConsumptionMaster._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(ConsumptionMaster._LoginDate, SqlDbType.DateTime);
              
                pAction.Value = 9;
                pOutwardId.Value = Entity_StockMaster.ConsumptionId;

                pDeletedBy.Value = Entity_StockMaster.UserId;
                pDeletedDate.Value = Entity_StockMaster.LoginDate;


                SqlParameter[] Param = new SqlParameter[] { pAction, pOutwardId, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, Param);

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
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pOutwardId = new SqlParameter(ConsumptionMaster._ConsumptionId, SqlDbType.BigInt);

                pAction.Value = 8;
                pOutwardId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, pAction, pOutwardId);
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
                SqlParameter MAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(ConsumptionMaster._strCond, SqlDbType.NVarChar);

                MAction.Value = 3;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, oParmCol);

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
        public DataSet GetConsumeStockForPrint(int ID, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter(ConsumptionMaster._ConsumptionId, SqlDbType.BigInt);
                pAction.Value = 10;
                pRequisitionCafeId.Value = ID;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, pAction, pRequisitionCafeId);
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
        public int UpdateAssignFlag(ref ConsumptionMaster Entity_StockMaster, out string strError)
        {

            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pInwardId = new SqlParameter(ConsumptionMaster._IssueId, SqlDbType.BigInt);
                

                pAction.Value = 16;
                pInwardId.Value = Entity_StockMaster.IssueId;
             
             
                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQueryDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_CosumptionMaster, pAction,pInwardId);

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
        public DataSet GetConsumeStockForSummary(string RepCondition, out string strError,int actionno)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ConsumptionMaster._strCond, SqlDbType.NVarChar);
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
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_ConsumptionMasterReport, pAction, pRepCondition);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        public DataSet GetConsumeStockForReport(string RepCondition, out string strError, int actionno)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ConsumptionMaster._strCond, SqlDbType.NVarChar);
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
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_ConsumptionMasterReport, pAction, pRepCondition);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
        public DataSet FillConsumeNoComboForReport(int COND,out string StrError)
        {
            StrError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pLOCID = new SqlParameter("@UserID", SqlDbType.BigInt);
                pAction.Value = 1;
                pLOCID.Value = COND;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_ConsumptionMasterReport, pAction,pLOCID);
            }
            catch (Exception ex)
            {
                StrError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }


        public DataSet GetStockOfConsumption(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ConsumptionMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ConsumptionMaster._strCond, SqlDbType.NVarChar);
               
                pAction.Value = 6;
                pRepCondition.Value = RepCondition;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ConsumptionMaster.SP_ConsumptionMasterReport, pAction, pRepCondition);
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
