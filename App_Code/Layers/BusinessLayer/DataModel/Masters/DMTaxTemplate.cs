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
using MayurInventory.DALSQLHelper;
using MayurInventory.DB;
using MayurInventory.Utility;
using System.Data.SqlClient;
using System.Collections.Generic;
using MayurInventory.EntityClass;

/// <summary>
/// Summary description for DMTaxTemplate
/// </summary>
namespace MayurInventory.BussinessLayer
{
    public class DMTaxTemplate : Utility.Setting
    {
        #region Business Methods

        public int InsertTax(ref TaxTemplate obj_Tax, out string strError)
        {
            int InsertRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter PAction = new SqlParameter(TaxTemplate._Action, SqlDbType.BigInt);
                SqlParameter PTaxName = new SqlParameter(TaxTemplate._TaxName, SqlDbType.NVarChar);
                SqlParameter PTaxPer = new SqlParameter(TaxTemplate._TaxPer, SqlDbType.NVarChar);
                SqlParameter PTaxAmt = new SqlParameter(TaxTemplate._TaxAmt, SqlDbType.NVarChar);
                SqlParameter PEffectiveDate= new SqlParameter(TaxTemplate._EffectiveDate, SqlDbType.DateTime);
                SqlParameter PCreatedBy = new SqlParameter(TaxTemplate._CreatedBy, SqlDbType.BigInt);
                SqlParameter PCreatedDate = new SqlParameter(TaxTemplate._CreatedDate, SqlDbType.DateTime);

                PAction.Value = 1;
                PTaxName.Value = obj_Tax.TaxName;
                PTaxPer.Value = obj_Tax.TaxPer;
                PTaxAmt.Value = obj_Tax.TaxAmt;
                PEffectiveDate.Value = obj_Tax.EffectiveDate;

                PCreatedBy.Value = obj_Tax.CreatedByID.Equals("") ? 0 : obj_Tax.CreatedByID;
                PCreatedDate.Value = obj_Tax.CreatedDate;

                SqlParameter[] ParamArray = new SqlParameter[] { PAction, PTaxName, PTaxPer, PTaxAmt, PEffectiveDate,PCreatedBy, PCreatedDate };
                Open(Setting.CONNECTION_STRING);
                BeginTransaction();
                InsertRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TaxTemplate.PRO_TAXTemplateMASTER, ParamArray);

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
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return InsertRow;

        }
        public int UpdateTax(ref TaxTemplate obj_Tax, out string strError)
        {
            int UpdateRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter PAction = new SqlParameter(TaxTemplate._Action, SqlDbType.BigInt);
                SqlParameter PTaxID = new SqlParameter(TaxTemplate._TaxID, SqlDbType.BigInt);
                SqlParameter PTaxName = new SqlParameter(TaxTemplate._TaxName, SqlDbType.NVarChar);
                SqlParameter PTaxPer = new SqlParameter(TaxTemplate._TaxPer, SqlDbType.NVarChar);
                SqlParameter PTaxAmt = new SqlParameter(TaxTemplate._TaxAmt, SqlDbType.NVarChar);
                SqlParameter PEffectiveDate = new SqlParameter(TaxTemplate._EffectiveDate, SqlDbType.DateTime);
                SqlParameter PUpdatedBy = new SqlParameter(TaxTemplate._UpdatedBy, SqlDbType.BigInt);
                SqlParameter PUpdatedDate = new SqlParameter(TaxTemplate._UpdatedDate, SqlDbType.DateTime);

                PAction.Value = 2;
                PTaxID.Value = obj_Tax.TaxId;
                PTaxName.Value = obj_Tax.TaxName;
                PTaxPer.Value = obj_Tax.TaxPer;
                PTaxAmt.Value = obj_Tax.TaxAmt;
                PEffectiveDate.Value = obj_Tax.EffectiveDate;


                PUpdatedBy.Value = obj_Tax.UpdatedByID.Equals("") ? 0 : obj_Tax.UpdatedByID;
                PUpdatedDate.Value = obj_Tax.UpdatedDate;

                SqlParameter[] ParamArray = new SqlParameter[] { PAction, PTaxID, PTaxName, PTaxPer, PTaxAmt,PEffectiveDate, PUpdatedBy, PUpdatedDate };
                Open(Setting.CONNECTION_STRING);
                BeginTransaction();
                UpdateRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TaxTemplate.PRO_TAXTemplateMASTER, ParamArray);

                if (UpdateRow != 0)
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
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return UpdateRow;

        }
        public int DeleteTax(ref  TaxTemplate obj_Tax, out string strError)
        {
            int DeleteRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter PAction = new SqlParameter(TaxTemplate._Action, SqlDbType.BigInt);
                SqlParameter PTaxID = new SqlParameter(TaxTemplate._TaxID, SqlDbType.BigInt);
                SqlParameter PDeletedBy = new SqlParameter(TaxTemplate._DeletedBy, SqlDbType.BigInt);
                SqlParameter PDeletedDate = new SqlParameter(TaxTemplate._DeletedDate, SqlDbType.DateTime);

                PAction.Value = 3;
                PTaxID.Value = obj_Tax.TaxId;
                PDeletedBy.Value = obj_Tax.DeletedByID.Equals("") ? 0 : obj_Tax.DeletedByID;
                PDeletedDate.Value = obj_Tax.DeletedDate;

                SqlParameter[] ParamArray = new SqlParameter[] { PAction, PTaxID, PDeletedBy, PDeletedDate };
                Open(Setting.CONNECTION_STRING);
                BeginTransaction();
                DeleteRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TaxTemplate.PRO_TAXTemplateMASTER, ParamArray);

                if (DeleteRow != 0)
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
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return DeleteRow;

        }
        public DataSet GetTax(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(TaxTemplate._Action, SqlDbType.BigInt);
                SqlParameter MRepCondition = new SqlParameter(TaxTemplate._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 4;
                MRepCondition.Value = RepCondition;

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TaxTemplate.PRO_TAXTemplateMASTER, MAction, MRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        public DataSet GetTaxForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter PAction = new SqlParameter(TaxTemplate._Action, SqlDbType.BigInt);
                SqlParameter PTaxID = new SqlParameter(TaxTemplate._TaxID, SqlDbType.BigInt);
                PAction.Value = 5;
                PTaxID.Value = ID;

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TaxTemplate.PRO_TAXTemplateMASTER, PAction, PTaxID);

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
                SqlParameter MAction = new SqlParameter(TaxTemplate._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(TaxTemplate._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 4;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, TaxTemplate.PRO_TAXTemplateMASTER, oParmCol);

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

        public DataSet ChkDuplicate(string Condition, string Brands, out string StrError)
        {
            DataSet DS = new DataSet();
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(TaxTemplate._Action, SqlDbType.BigInt);
                SqlParameter pCondition = new SqlParameter(TaxTemplate._RepCondition, SqlDbType.NVarChar);
                SqlParameter pTaxName = new SqlParameter(TaxTemplate._TaxName, SqlDbType.NVarChar);

                pAction.Value = 6;
                pCondition.Value = Condition;
                pTaxName.Value = Brands;

                SqlParameter[] param = new SqlParameter[] { pAction, pCondition, pTaxName };
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, TaxTemplate.PRO_TAXTemplateMASTER, param);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return DS;
        }
     

       
        #endregion
        public DMTaxTemplate()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}