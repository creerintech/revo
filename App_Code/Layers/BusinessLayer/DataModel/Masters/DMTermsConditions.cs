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
using System.Data.SqlClient;
using System.Collections.Generic;

using MayurInventory.DALSQLHelper;
using MayurInventory.DataModel;
using MayurInventory.DB;
using MayurInventory.EntityClass;
using MayurInventory.Utility;

/// <summary>
/// Summary description for DMTermsConditions
/// </summary>
namespace MayurInventory.DataModel
{
    public class DMTermsConditions:Utility.Setting
    {
        public int InsertRecord(ref TermsCondition Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(TermsCondition._Action, SqlDbType.BigInt);
                SqlParameter pTitle = new SqlParameter(TermsCondition._Title, SqlDbType.NVarChar);
                SqlParameter PTermsConditions = new SqlParameter(TermsCondition._TermsConditions, SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(TermsCondition._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(TermsCondition._LoginDate, SqlDbType.DateTime);


                pAction.Value = 1;
                pTitle.Value = Entity_Call.Title;
                PTermsConditions.Value = Entity_Call.TermsConditions;
                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;


                SqlParameter[] param = new SqlParameter[] { pAction, pTitle,PTermsConditions, pCreatedBy, pCreatedDate};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TermsCondition.SP_TermCondition, param);

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
                throw new Exception(ex.Message);
            }

            finally
            {
                Close();
            }
            return iInsert;
        }

        public int UpdateRecord(ref TermsCondition Entity_Call, out String StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(TermsCondition._Action, SqlDbType.BigInt);
                SqlParameter PTermsID = new SqlParameter(TermsCondition._TermsID, SqlDbType.BigInt);
                SqlParameter pTitle = new SqlParameter(TermsCondition._Title, SqlDbType.NVarChar);
                SqlParameter PTermsConditions = new SqlParameter(TermsCondition._TermsConditions, SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(TermsCondition._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(TermsCondition._LoginDate, SqlDbType.DateTime);


                pAction.Value = 2;
                PTermsID.Value = Entity_Call.TermsID;
                pTitle.Value = Entity_Call.Title;
                PTermsConditions.Value = Entity_Call.TermsConditions;
                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;


                SqlParameter[] param = new SqlParameter[] { pAction,PTermsID, pTitle, PTermsConditions, pCreatedBy, pCreatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TermsCondition.SP_TermCondition, param);

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
                throw new Exception(ex.Message);
            }

            finally
            {
                Close();
            }
            return iInsert;

        }

        public int DeleteRecord(ref TermsCondition Entity_Call, out string StrError)
        {
            int iDelete = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter PTermsID = new SqlParameter(TermsCondition._TermsID, SqlDbType.BigInt);

                SqlParameter pDeletedBy = new SqlParameter(TermsCondition._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(TermsCondition._LoginDate, SqlDbType.DateTime);

                pAction.Value = 3;
                PTermsID.Value = Entity_Call.TermsID;

                pDeletedBy.Value = Entity_Call.UserId;
                pDeletedDate.Value = Entity_Call.LoginDate;
                SqlParameter[] param = new SqlParameter[] { pAction, PTermsID, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, TermsCondition.SP_TermCondition, param);
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
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
            return iDelete;
        }

        public DataSet GetTermsForEdit(int ID, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(TermsCondition._Action, SqlDbType.BigInt);
                SqlParameter PTermsID = new SqlParameter(TermsCondition._TermsID, SqlDbType.BigInt);

                pAction.Value = 4;
                PTermsID.Value = ID;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TermsCondition.SP_TermCondition, pAction, PTermsID);

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

        public DataSet GetTerms(string RepCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TermsCondition._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(TermsCondition._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TermsCondition.SP_TermCondition, pAction, pRepCondition);
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

        public DataSet ChkDuplicate(string Name, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(TermsCondition._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(TermsCondition._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 6;
                pRepCondition.Value = Name;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, TermsCondition.SP_TermCondition, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public string[] GetSuggestRecord(string prefixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(TermsCondition._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(TermsCondition._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 5;
                pRepCondition.Value = prefixText;

                SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition };

                Open(CONNECTION_STRING);
                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, TermsCondition.SP_TermCondition, oparamcol);

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
        public DMTermsConditions()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}