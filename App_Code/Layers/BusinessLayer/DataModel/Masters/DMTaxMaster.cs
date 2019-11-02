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
    public class DMTaxMaster : Utility.Setting
    {
        public int InsertRecord(ref TaxMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TaxMaster._Action, SqlDbType.BigInt);
                SqlParameter pTaxName = new SqlParameter(TaxMaster._TaxName, SqlDbType.NVarChar);
                //SqlParameter pTaxPer = new SqlParameter(TaxMaster._TaxPer, SqlDbType.Decimal);
                //SqlParameter pEffectiveFrom = new SqlParameter(TaxMaster._EffectiveFrom, SqlDbType.DateTime);
                //SqlParameter pTaxTypeID = new SqlParameter(TaxMaster._TaxTypeID, SqlDbType.BigInt);
                SqlParameter pCreatedBy = new SqlParameter(TaxMaster._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(TaxMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 1;
                pTaxName.Value = Entity_Call.TaxName;
                //pTaxPer.Value = Entity_Call.TaxPer;
                //pEffectiveFrom.Value = Entity_Call.EffectiveFrom;
                //pTaxTypeID.Value = Entity_Call.TaxTypeID;
                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pTaxName, pCreatedBy, pCreatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, TaxMaster.SP_ItemCategory, Param);

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

        public int UpdateRecord(ref TaxMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TaxMaster._Action, SqlDbType.BigInt);
                SqlParameter pTaxId = new SqlParameter(TaxMaster._TaxId, SqlDbType.BigInt);
                SqlParameter pTaxName = new SqlParameter(TaxMaster._TaxName, SqlDbType.NVarChar);
                //SqlParameter pTaxPer = new SqlParameter(TaxMaster._TaxPer, SqlDbType.Decimal);
                //SqlParameter pEffectiveFrom = new SqlParameter(TaxMaster._EffectiveFrom, SqlDbType.DateTime);
                //SqlParameter pTaxTypeID = new SqlParameter(TaxMaster._TaxTypeID, SqlDbType.BigInt);
                SqlParameter pUpdatedBy = new SqlParameter(TaxMaster._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(TaxMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 2;
                pTaxId.Value = Entity_Call.TaxId;
                pTaxName.Value = Entity_Call.TaxName;
                //pTaxPer.Value = Entity_Call.TaxPer;
                //pEffectiveFrom.Value = Entity_Call.EffectiveFrom;
                //pTaxTypeID.Value = Entity_Call.TaxTypeID;
                pUpdatedBy.Value = Entity_Call.UserId;
                pUpdatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pTaxId, pTaxName, pUpdatedBy, pUpdatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TaxMaster.SP_ItemCategory, Param);

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

        public int DeleteRecord(ref TaxMaster Entity_Call, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TaxMaster._Action, SqlDbType.BigInt);
                SqlParameter pCategoryId = new SqlParameter(TaxMaster._TaxId, SqlDbType.BigInt);
                SqlParameter pDeletedBy = new SqlParameter(TaxMaster._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(TaxMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 3;
                pCategoryId.Value = Entity_Call.TaxId;

                pDeletedBy.Value = Entity_Call.UserId;
                pDeletedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pCategoryId, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TaxMaster.SP_ItemCategory, Param);

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

        public DataSet GetTaxMasterForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TaxMaster._Action, SqlDbType.BigInt);
                SqlParameter pTaxId = new SqlParameter(TaxMaster._TaxId, SqlDbType.BigInt);

                pAction.Value = 4;
                pTaxId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TaxMaster.SP_ItemCategory, pAction, pTaxId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetTaxMaster(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TaxMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(TaxMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TaxMaster.SP_ItemCategory, pAction, pRepCondition);

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
                SqlParameter pAction = new SqlParameter(TaxMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(TaxMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { pAction, pRepCondition };

                Open(CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, TaxMaster.SP_ItemCategory, oParmCol);

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

        public DataSet ChkDuplicate(string Name, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TaxMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(TaxMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 6;
                pRepCondition.Value = Name;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TaxMaster.SP_ItemCategory, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DMTaxMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int InsertTaxtls(ref TaxMaster Entity_TaxMaster, out string StrError)
        {
            int InsertRow = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter PAction = new SqlParameter(TaxMaster._Action, SqlDbType.BigInt);
                SqlParameter PTaxId = new SqlParameter(TaxMaster._TaxId, SqlDbType.BigInt);
                SqlParameter PGST = new SqlParameter(TaxMaster._GST, SqlDbType.Decimal);
                SqlParameter PApplicableDate = new SqlParameter(TaxMaster._ApplicableDate, SqlDbType.DateTime);


                PAction.Value = 8;
                PTaxId.Value = Entity_TaxMaster.TaxId;
                PGST.Value = Entity_TaxMaster.GST;
                PApplicableDate.Value = Entity_TaxMaster.ApplicableDate;

                SqlParameter[] ParamArray = new SqlParameter[] { PAction, PTaxId, PGST, PApplicableDate };

                Open(Setting.CONNECTION_STRING);
                BeginTransaction();

                InsertRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, "SP_TaxMaster", ParamArray);

                if (InsertRow != 0)
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
                StrError = ex.Message;
            }
            finally
            {
                Close();
            }
            return InsertRow;

        }

        public int UpdateTaskDetails(ref TaxMaster Entity_TaxMaster, out string StrError)
        {
            throw new NotImplementedException();
        }
    }
}
