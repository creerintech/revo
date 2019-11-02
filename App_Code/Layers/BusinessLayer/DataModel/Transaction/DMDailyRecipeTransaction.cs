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
    public class DMDailyRecipeTransaction : Utility.Setting
    {
        #region[BusinessLogic]

        public int InsertRecord(ref DailyRecipeTransaction  Entity_Recipe, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(DailyRecipeTransaction._Action, SqlDbType.BigInt);
                SqlParameter pOrderDate = new SqlParameter(DailyRecipeTransaction._OrderDate, SqlDbType.DateTime);
                SqlParameter pTotalOrderCost = new SqlParameter(DailyRecipeTransaction._TotalOrderCost, SqlDbType.Decimal);
               
                SqlParameter pCreatedBy = new SqlParameter(DailyRecipeTransaction._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(DailyRecipeTransaction._LoginDate, SqlDbType.DateTime);

                pAction.Value = 1;
                pOrderDate.Value = Entity_Recipe.OrderDate;
                pTotalOrderCost.Value = Entity_Recipe.TotalOrderCost;

                pCreatedBy.Value = Entity_Recipe.UserId;
                pCreatedDate.Value = Entity_Recipe.LoginDate;
               
                SqlParameter[] Param = new SqlParameter[] { pAction, pOrderDate, pTotalOrderCost,pCreatedBy, pCreatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, DailyRecipeTransaction.SP_DailyRecipeTransaction, Param);

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

        public int InsertDetailsRecord(ref DailyRecipeTransaction Entity_Recipe, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(DailyRecipeTransaction._Action, SqlDbType.BigInt);
                SqlParameter pOrderId = new SqlParameter(DailyRecipeTransaction._OrderId, SqlDbType.BigInt);
                SqlParameter pRecipeId = new SqlParameter(DailyRecipeTransaction._RecipeId, SqlDbType.BigInt);
                SqlParameter pQty = new SqlParameter(DailyRecipeTransaction._Qty, SqlDbType.Decimal);
                SqlParameter pTotalAmt = new SqlParameter(DailyRecipeTransaction._TotalAmt, SqlDbType.Decimal);

                //---**StockDetails**---
                //SqlParameter pItemId = new SqlParameter(DailyRecipeTransaction._ItemId, SqlDbType.BigInt);
                ////SqlParameter pUnitId = new SqlParameter(DailyRecipeTransaction._UnitId, SqlDbType.BigInt);
                //SqlParameter pStockDate = new SqlParameter(DailyRecipeTransaction._StockDate, SqlDbType.DateTime);
                //SqlParameter pStockLocationID = new SqlParameter(DailyRecipeTransaction._StockLocationID, SqlDbType.BigInt);
                //SqlParameter pQtyPerUnit = new SqlParameter(DailyRecipeTransaction._QtyPerUnit, SqlDbType.Decimal);
                //SqlParameter pActualRate = new SqlParameter(DailyRecipeTransaction._ActualRate, SqlDbType.Decimal);

                pAction.Value = 6;
                pOrderId.Value = Entity_Recipe.OrderId;
                pRecipeId.Value = Entity_Recipe.RecipeId;
                pQty.Value = Entity_Recipe.Qty;
                pTotalAmt.Value = Entity_Recipe.TotalAmt;

                //---**StockDetails**---
                //pItemId.Value = Entity_Recipe.ItemId;
                ////pUnitId.Value = Entity_Recipe.UnitId;
                //pStockDate.Value = Entity_Recipe.StockDate;
                //pStockLocationID.Value = Entity_Recipe.StockLocationID;
                //pQtyPerUnit.Value = Entity_Recipe.QtyPerUnit;
                //pActualRate.Value = Entity_Recipe.ActualRate;

                SqlParameter[] Param = new SqlParameter[] {pAction,pOrderId,pRecipeId,pQty,pTotalAmt};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, DailyRecipeTransaction.SP_DailyRecipeTransaction, Param);

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

        public int InsertStockDetails(ref DailyRecipeTransaction Entity_Recipe, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                //---**StockDetails**---
                SqlParameter pAction = new SqlParameter(DailyRecipeTransaction._Action, SqlDbType.BigInt);
                SqlParameter pOrderId = new SqlParameter(DailyRecipeTransaction._OrderId, SqlDbType.BigInt);
                SqlParameter pRecipeId = new SqlParameter(DailyRecipeTransaction._RecipeId, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(DailyRecipeTransaction._ItemId, SqlDbType.BigInt);
                SqlParameter pStockDate = new SqlParameter(DailyRecipeTransaction._StockDate, SqlDbType.DateTime);
                SqlParameter pStockLocationID = new SqlParameter(DailyRecipeTransaction._StockLocationID, SqlDbType.BigInt);
                SqlParameter pQtyPerUnit = new SqlParameter(DailyRecipeTransaction._QtyPerUnit, SqlDbType.Decimal);
                SqlParameter pActualRate = new SqlParameter(DailyRecipeTransaction._ActualRate, SqlDbType.Decimal);

                //---**StockDetails**---
                pAction.Value = 11;
                pOrderId.Value = Entity_Recipe.OrderId;
                pRecipeId.Value = Entity_Recipe.RecipeId;
                pItemId.Value = Entity_Recipe.ItemId;
                pStockDate.Value = Entity_Recipe.StockDate;
                pStockLocationID.Value = Entity_Recipe.StockLocationID;
                pQtyPerUnit.Value = Entity_Recipe.QtyPerUnit;
                pActualRate.Value = Entity_Recipe.ActualRate;

                SqlParameter[] Param = new SqlParameter[] {pAction,pOrderId,pRecipeId,pItemId,pStockDate,pStockLocationID,pQtyPerUnit,pActualRate};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, DailyRecipeTransaction.SP_DailyRecipeTransaction, Param);

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

        public int UpdateRecord(ref DailyRecipeTransaction Entity_Recipe, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(DailyRecipeTransaction._Action, SqlDbType.BigInt);
                SqlParameter pOrderId = new SqlParameter(DailyRecipeTransaction._OrderId, SqlDbType.BigInt);
                SqlParameter pOrderNo = new SqlParameter(DailyRecipeTransaction._OrderNo, SqlDbType.NVarChar);
                SqlParameter pOrderDate = new SqlParameter(DailyRecipeTransaction._OrderDate, SqlDbType.DateTime);
                SqlParameter pTotalOrderCost = new SqlParameter(DailyRecipeTransaction._TotalOrderCost, SqlDbType.Decimal);
               
                SqlParameter pUpdatedBy = new SqlParameter(DailyRecipeTransaction._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(DailyRecipeTransaction._LoginDate, SqlDbType.DateTime);

                pAction.Value = 2;
                pOrderId.Value = Entity_Recipe.OrderId;
                pOrderNo.Value = Entity_Recipe.OrderNo;
                pOrderDate.Value = Entity_Recipe.OrderDate;
                pTotalOrderCost.Value = Entity_Recipe.TotalOrderCost;

                pUpdatedBy.Value = Entity_Recipe.UserId;
                pUpdatedDate.Value = Entity_Recipe.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pOrderId,pOrderNo, pOrderDate, pTotalOrderCost,pUpdatedBy, pUpdatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, DailyRecipeTransaction.SP_DailyRecipeTransaction, Param);

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

        public int DeleteRecord(ref DailyRecipeTransaction Entity_Recipe, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(DailyRecipeTransaction._Action, SqlDbType.BigInt);
                SqlParameter pOrderId = new SqlParameter(DailyRecipeTransaction._OrderId, SqlDbType.BigInt);

                SqlParameter pDeletedBy = new SqlParameter(DailyRecipeTransaction._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(DailyRecipeTransaction._LoginDate, SqlDbType.DateTime);

                pAction.Value = 3;
                pOrderId.Value = Entity_Recipe.OrderId;

                pDeletedBy.Value = Entity_Recipe.UserId;
                pDeletedDate.Value = Entity_Recipe.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pOrderId, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, DailyRecipeTransaction.SP_DailyRecipeTransaction, Param);

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

        public DataSet FillCombo(int RecipeId,out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(DailyRecipeTransaction._Action, SqlDbType.BigInt);
                SqlParameter pRecipeId = new SqlParameter(DailyRecipeTransaction._RecipeId, SqlDbType.BigInt);
               
                pAction.Value = 4;
                pRecipeId.Value = RecipeId;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, DailyRecipeTransaction.SP_DailyRecipeTransaction, pAction,pRecipeId);
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

        public DataSet GetRecipeDetails(Int32 RecipeId, out string strError, Int32 Qty)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(DailyRecipeTransaction._Action, SqlDbType.BigInt);
                SqlParameter pRecipeId = new SqlParameter(DailyRecipeTransaction._RecipeId, SqlDbType.BigInt);
                SqlParameter pQty = new SqlParameter(DailyRecipeTransaction._Qty, SqlDbType.BigInt);

                pAction.Value = 5;
                pRecipeId.Value = RecipeId;
                pQty.Value = Qty;

                SqlParameter[] Param = new SqlParameter[] { pAction, pRecipeId,pQty};

                Open(CONNECTION_STRING);
                BeginTransaction();

                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, DailyRecipeTransaction.SP_DailyRecipeTransaction, Param);

                //Open(CONNECTION_STRING);
                //Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, DailyRecipeTransaction.SP_DailyRecipeTransaction, pAction, pRecipeId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet ChkDuplicate(string Name, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(DailyRecipeTransaction._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(DailyRecipeTransaction._strCond, SqlDbType.NVarChar);

                pAction.Value = 7;
                pRepCondition.Value = Name;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, DailyRecipeTransaction.SP_DailyRecipeTransaction, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetReportGrid(string RepCondition, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCondition = new SqlParameter(DailyRecipeTransaction._strCond, SqlDbType.NVarChar);
                pAction.Value = 9;
                pCondition.Value = RepCondition;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, DailyRecipeTransaction.SP_DailyRecipeTransaction, pAction, pCondition);
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

        public DataSet GetRecordForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(DailyRecipeTransaction._Action, SqlDbType.BigInt);
                SqlParameter pOrderId = new SqlParameter(DailyRecipeTransaction._OrderId, SqlDbType.BigInt);

                pAction.Value = 10;
                pOrderId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, DailyRecipeTransaction.SP_DailyRecipeTransaction, pAction, pOrderId);

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
                SqlParameter MAction = new SqlParameter(DailyRecipeTransaction._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(DailyRecipeTransaction._strCond, SqlDbType.NVarChar);

                MAction.Value = 9;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, DailyRecipeTransaction.SP_DailyRecipeTransaction, oParmCol);

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

        public DMDailyRecipeTransaction()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}