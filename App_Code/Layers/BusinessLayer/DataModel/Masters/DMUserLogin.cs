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
using MayurInventory.Utility;
using MayurInventory.DataModel;
using MayurInventory.EntityClass;
using MayurInventory.DALSQLHelper;
using MayurInventory.DB;
using System.Collections.Generic;


 namespace MayurInventory.DataModel
{
    public class DMUserLogin :Utility.Setting
    {
        public DataSet GetLoginInfo(ref UserLogin Entity_login, out string strError)
        {           
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(UserLogin._Action, SqlDbType.BigInt);
                SqlParameter pUsername = new SqlParameter(UserLogin._userName, SqlDbType.NVarChar);
                SqlParameter pPassword = new SqlParameter(UserLogin._Password, SqlDbType.NVarChar);
                SqlParameter pCafeID = new SqlParameter(UserLogin._CafeID, SqlDbType.BigInt);
                pAction.Value = 2;
                pUsername.Value = Entity_login.UserName;
                pPassword.Value = Entity_login.Password;
                pCafeID.Value = Entity_login.CafeID;
                SqlParameter[] oParmCol = new SqlParameter[] { pAction, pUsername, pPassword,pCafeID };
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, UserLogin.SP_UserLogin, oParmCol);
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

        public DataSet GetLoginInfoWrites(ref UserLogin Entity_login, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(UserLogin._Action, SqlDbType.BigInt);
                SqlParameter pUsername = new SqlParameter(UserLogin._userName, SqlDbType.NVarChar);
                pAction.Value = 8;
                pUsername.Value = Entity_login.UserName;
                SqlParameter[] oParmCol = new SqlParameter[] { pAction, pUsername};
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, UserLogin.SP_UserLogin, oParmCol);
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

        public string[] GetSuggestedRecord(string prefixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;
            try
            {

                // -- For Checking OF Execution of Procedure=========
                SqlParameter MAction = new SqlParameter(UserLogin._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(UserLogin._userName, SqlDbType.NVarChar);

                MAction.Value = 7;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, UserLogin.SP_UserLogin, oParmCol);

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

        public DataSet GetLoginIDFORMAILREQUEST(string mailid, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(UserLogin._Action, SqlDbType.BigInt);
                SqlParameter pUsername = new SqlParameter(UserLogin._userName, SqlDbType.NVarChar);
                pAction.Value = 5;
                pUsername.Value = mailid;
                SqlParameter[] oParmCol = new SqlParameter[] { pAction, pUsername };
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, UserLogin.SP_UserLogin, oParmCol);
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

        public DataSet GetCHNO(string CHNO, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(UserLogin._Action, SqlDbType.BigInt);
                SqlParameter pUsername = new SqlParameter("@CHNO", SqlDbType.NVarChar);
                pAction.Value = 4;
                pUsername.Value = CHNO;
                SqlParameter[] oParmCol = new SqlParameter[] { pAction, pUsername };
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, UserLogin.SP_UserLogin, oParmCol);
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

        public DataSet GETDATAFORPOPUP(out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                pAction.Value = 1;
                SqlParameter[] oParmCol = new SqlParameter[] { pAction};
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, "SP_GETDATAFORPOPUP", oParmCol);
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


        public DataSet GetCafe(out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(UserLogin._Action, SqlDbType.BigInt);
                pAction.Value = 3;
                SqlParameter[] oParmCol = new SqlParameter[] { pAction };
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, UserLogin.SP_UserLogin,pAction);
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

        public DataSet GetLocationAccordingToUser(String Condition,out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(UserLogin._Action, SqlDbType.BigInt);
                SqlParameter pStrCondition = new SqlParameter("@StrCondition", SqlDbType.NVarChar);
                pAction.Value = 6;
                pStrCondition.Value = Condition;
                SqlParameter[] oParmCol = new SqlParameter[] { pAction,pStrCondition };
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, UserLogin.SP_UserLogin, pAction,pStrCondition);
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

	    public DMUserLogin()
	    {
    		
	    }
    }

}