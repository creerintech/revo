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
/// Summary description for DMRecipeTemplate
/// </summary>
namespace MayurInventory.DataModel 
{
    public class DMRecipeTemplate:Utility.Setting
    {
        #region[Business Logic]

        public int InsertRecord(ref RecipeTemplate Entity_Recipe, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction= new SqlParameter(RecipeTemplate._Action, SqlDbType.BigInt);
                SqlParameter pMenuItem= new SqlParameter(RecipeTemplate._MenuItem, SqlDbType.NVarChar);
                SqlParameter pAmtPerPlate= new SqlParameter(RecipeTemplate._AmtPerPlate, SqlDbType.Decimal);
               
                SqlParameter pCreatedBy = new SqlParameter(RecipeTemplate._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(RecipeTemplate._LoginDate, SqlDbType.DateTime);

                pAction.Value = 1;
                pMenuItem.Value = Entity_Recipe.MenuItem;
                pAmtPerPlate.Value = Entity_Recipe.AmtPerPlate;

                pCreatedBy.Value = Entity_Recipe.UserId;
                pCreatedDate.Value = Entity_Recipe.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pMenuItem, pAmtPerPlate, pCreatedBy, pCreatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, RecipeTemplate.SP_RecipeTemplate, Param);

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

        public int UpdateRecord(ref RecipeTemplate Entity_Recipe, out string strError)
        {
            int iUpdate= 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(RecipeTemplate._Action, SqlDbType.BigInt);
                SqlParameter pRecipeId = new SqlParameter(RecipeTemplate._RecipeId, SqlDbType.BigInt);
                SqlParameter pMenuItem = new SqlParameter(RecipeTemplate._MenuItem, SqlDbType.NVarChar);
                SqlParameter pAmtPerPlate = new SqlParameter(RecipeTemplate._AmtPerPlate, SqlDbType.Decimal);

                SqlParameter pCreatedBy = new SqlParameter(RecipeTemplate._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(RecipeTemplate._LoginDate, SqlDbType.DateTime);

                pAction.Value = 2;
                pRecipeId.Value = Entity_Recipe.RecipeId;
                pMenuItem.Value = Entity_Recipe.MenuItem;
                pAmtPerPlate.Value = Entity_Recipe.AmtPerPlate;

                pCreatedBy.Value = Entity_Recipe.UserId;
                pCreatedDate.Value = Entity_Recipe.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pRecipeId, pMenuItem, pAmtPerPlate, pCreatedBy, pCreatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iUpdate = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RecipeTemplate.SP_RecipeTemplate, Param);

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
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return iUpdate;
        }

        public int DeleteRecord(ref RecipeTemplate Entity_Recipe, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(RecipeTemplate._Action, SqlDbType.BigInt);
                SqlParameter pRecipeId = new SqlParameter(RecipeTemplate._RecipeId, SqlDbType.BigInt);

                SqlParameter pDeletedBy = new SqlParameter(RecipeTemplate._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(RecipeTemplate._LoginDate, SqlDbType.DateTime);

                pAction.Value = 3;
                pRecipeId.Value = Entity_Recipe.RecipeId;

                pDeletedBy.Value = Entity_Recipe.UserId;
                pDeletedDate.Value = Entity_Recipe.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pRecipeId, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RecipeTemplate.SP_RecipeTemplate, Param);

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

        public int InsertDetailsRecord(ref RecipeTemplate Entity_Recipe, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(RecipeTemplate._Action, SqlDbType.BigInt);
                SqlParameter pRecipeId = new SqlParameter(RecipeTemplate._RecipeId, SqlDbType.NVarChar);
                SqlParameter pItemId = new SqlParameter(RecipeTemplate._ItemId, SqlDbType.Decimal);
                SqlParameter pQty = new SqlParameter(RecipeTemplate._Qty, SqlDbType.Decimal);
                SqlParameter pSubUnitId = new SqlParameter(RecipeTemplate._SubUnitId, SqlDbType.BigInt);
                SqlParameter pIngredAmt = new SqlParameter(RecipeTemplate._IngredAmt, SqlDbType.Decimal);
                SqlParameter pActualRate = new SqlParameter(RecipeTemplate._ActualRate, SqlDbType.Decimal);
                SqlParameter pQtyPerUnit = new SqlParameter(RecipeTemplate._QtyPerUnit, SqlDbType.Decimal);

                pAction.Value = 6;
                pRecipeId.Value = Entity_Recipe.RecipeId;
                pItemId.Value = Entity_Recipe.ItemId;
                pQty.Value = Entity_Recipe.Qty;
                pSubUnitId.Value = Entity_Recipe.SubUnitId;
                pIngredAmt.Value = Entity_Recipe.IngredAmt;
                pActualRate.Value = Entity_Recipe.ActualRate;
                pQtyPerUnit.Value = Entity_Recipe.QtyPerUnit;


                SqlParameter[] Param = new SqlParameter[] { pAction, pRecipeId, pItemId, pQty, pSubUnitId, pIngredAmt ,pActualRate,pQtyPerUnit};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RecipeTemplate.SP_RecipeTemplate, Param);

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

        public DataSet GetAvgPurchaseRate(Int32 ItemId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(RecipeTemplate._Action, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(RecipeTemplate._ItemId, SqlDbType.BigInt);

                pAction.Value = 5;
                pItemId.Value = ItemId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RecipeTemplate.SP_RecipeTemplate, pAction, pItemId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet FillCombo(out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(RecipeTemplate._Action, SqlDbType.BigInt);
                pAction.Value = 4;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, RecipeTemplate.SP_RecipeTemplate, pAction);
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

        public DataSet ChkDuplicate(string Name, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(RecipeTemplate._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(RecipeTemplate._strCond, SqlDbType.NVarChar);

                pAction.Value = 7;
                pRepCondition.Value = Name;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RecipeTemplate.SP_RecipeTemplate, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetConversnFactr(Int32 SubUnitId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(RecipeTemplate._Action, SqlDbType.BigInt);
                SqlParameter pSubUnitId = new SqlParameter(RecipeTemplate._SubUnitId, SqlDbType.BigInt);

                pAction.Value = 8;
                pSubUnitId.Value = SubUnitId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RecipeTemplate.SP_RecipeTemplate, pAction, pSubUnitId);

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
                SqlParameter pCondition = new SqlParameter(RecipeTemplate._strCond, SqlDbType.NVarChar);
                pAction.Value = 9;
                pCondition.Value = RepCondition;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RecipeTemplate.SP_RecipeTemplate, pAction, pCondition);
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
                SqlParameter pAction = new SqlParameter(RecipeTemplate._Action, SqlDbType.BigInt);
                SqlParameter pRecipeId = new SqlParameter(RecipeTemplate._RecipeId, SqlDbType.BigInt);

                pAction.Value = 10;
                pRecipeId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RecipeTemplate.SP_RecipeTemplate, pAction, pRecipeId);

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
                SqlParameter MAction = new SqlParameter(RecipeTemplate._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(RecipeTemplate._strCond, SqlDbType.NVarChar);

                MAction.Value = 9;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, RecipeTemplate.SP_RecipeTemplate, oParmCol);

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

        public DMRecipeTemplate()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
