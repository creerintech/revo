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

using MayurInventory.Utility;
using MayurInventory.DataModel;
using MayurInventory.EntityClass;
using MayurInventory.DALSQLHelper;
using MayurInventory.DB;


/// <summary>
/// Summary description for DMUserMaster
/// </summary>
/// 

namespace MayurInventory.DataModel
{
    public class DMUserMaster : Utility.Setting
    {
        #region Business Methods

       public int InsertUserDetails(ref  UserMaster obj_User, out string strError)
        {
            int InsertRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);
                SqlParameter pUserName = new SqlParameter(UserMaster._UserName, SqlDbType.NVarChar);
                SqlParameter pEmailId = new SqlParameter(UserMaster._EmailId, SqlDbType.NVarChar);
                SqlParameter PLoginName = new SqlParameter(UserMaster._LoginName,SqlDbType.NVarChar);
                SqlParameter pmob = new SqlParameter(UserMaster._mob, SqlDbType.NVarChar);
                SqlParameter pPassword = new SqlParameter(UserMaster._Password, SqlDbType.NVarChar);
                SqlParameter pUserType = new SqlParameter(UserMaster._UserType,SqlDbType.NVarChar);
                SqlParameter pIsAdmin = new SqlParameter(UserMaster._IsAdmin, SqlDbType.Bit);
                SqlParameter pCreatedBy = new SqlParameter(UserMaster._LUserID, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(UserMaster._LoginDate, SqlDbType.DateTime);
                SqlParameter pCafeteriaId = new SqlParameter(UserMaster._CafeteriaId, SqlDbType.BigInt);
                pAction.Value = 1;
                pUserName.Value = obj_User.UserName;
                pEmailId.Value = obj_User.EmailId;
                PLoginName.Value = obj_User.LoginName;
                pPassword.Value = obj_User.Password;
                pUserType.Value = obj_User.UserType;
                pIsAdmin.Value = obj_User.IsAdmin;
                pCreatedBy.Value = obj_User.LUserID.Equals("") ? 0 : obj_User.LUserID;
                pCreatedDate.Value = obj_User.LoginDate;
                pCafeteriaId.Value = obj_User.CafeteriaId;
                pmob.Value = obj_User.mob;
                SqlParameter[] ParamArray = new SqlParameter[] { pAction, pUserName,pEmailId,PLoginName, pPassword,pUserType, pIsAdmin, pCreatedBy, pCreatedDate,pCafeteriaId ,pmob };
                Open(CONNECTION_STRING);
                BeginTransaction();
                InsertRow = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster, ParamArray);

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
                RollBackTransaction();
            }
            finally
            {
                Close();
            }
            return InsertRow;

        }

       public int UpdateUserDetails(ref  UserMaster obj_User, out string strError)
        {
            int UpdateRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);
                SqlParameter pUserID = new SqlParameter(UserMaster._UserId, SqlDbType.BigInt);
                SqlParameter pUserName = new SqlParameter(UserMaster._UserName, SqlDbType.NVarChar);
                SqlParameter pLoginName =new SqlParameter(UserMaster._LoginName,SqlDbType.NVarChar);

                SqlParameter pEmailId = new SqlParameter(UserMaster._EmailId, SqlDbType.NVarChar);
                SqlParameter pPassword = new SqlParameter(UserMaster._Password, SqlDbType.NVarChar);
                SqlParameter pUserType=new SqlParameter(UserMaster._UserType,SqlDbType.NVarChar);
                SqlParameter pIsAdmin = new SqlParameter(UserMaster._IsAdmin, SqlDbType.Bit);
                SqlParameter pUpdatedBy = new SqlParameter(UserMaster._LUserID, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(UserMaster._LoginDate, SqlDbType.DateTime);
                SqlParameter pCafeteriaId = new SqlParameter(UserMaster._CafeteriaId, SqlDbType.BigInt);
                SqlParameter pmob = new SqlParameter(UserMaster._mob, SqlDbType.BigInt);

                pAction.Value = 2;
                pUserID.Value = obj_User.UserID;
                pUserName.Value = obj_User.UserName;
                pLoginName.Value=obj_User.LoginName;
                pEmailId.Value = obj_User.EmailId;
                pPassword.Value = obj_User.Password;
                pUserType.Value=obj_User.UserType;
                pIsAdmin.Value = obj_User.IsAdmin;
                pUpdatedBy.Value = obj_User.LUserID.Equals("") ? 0 : obj_User.LUserID;
                pUpdatedDate.Value = obj_User.LoginDate;
                pCafeteriaId.Value = obj_User.CafeteriaId;
                pmob.Value = obj_User.mob;
                SqlParameter[] ParamArray = new SqlParameter[] { pAction, pUserID, pUserName,pLoginName, pEmailId, pPassword,pUserType, pIsAdmin, pUpdatedBy, pUpdatedDate,pCafeteriaId ,pmob };
                Open(CONNECTION_STRING);
                BeginTransaction();
                UpdateRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster, ParamArray);

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
                RollBackTransaction();
            }
            finally
            {
                Close();
            }
            return UpdateRow;

        }
        
       public int DeleteUserDetails(ref  UserMaster obj_User_Master, out string strError)
       {
           int DeleteRow = 0;
           strError = string.Empty;
           try
           {
               SqlParameter MAction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);
               SqlParameter MUserID = new SqlParameter(UserMaster._UserId, SqlDbType.BigInt);
               SqlParameter MDeletedBy = new SqlParameter(UserMaster._LUserID, SqlDbType.BigInt);
               SqlParameter MDeletedDate = new SqlParameter(UserMaster._LoginDate, SqlDbType.DateTime);

               MAction.Value = 3;
               MUserID.Value = obj_User_Master.UserID;
               MDeletedBy.Value = obj_User_Master.LUserID.Equals("") ? 0 : obj_User_Master.LUserID;
               MDeletedDate.Value = obj_User_Master.LoginDate;

               SqlParameter[] ParamArray = new SqlParameter[] { MAction, MUserID, MDeletedBy, MDeletedDate };
               Open(CONNECTION_STRING);
               BeginTransaction();
               DeleteRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster, ParamArray);

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
               RollBackTransaction();
           }
           finally
           {
               Close();
           }
           return DeleteRow;

       }
       public DataSet GetUserDetailsForEdit(int ID, out string strError)
       {
           strError = string.Empty;
           DataSet Ds = new DataSet();
           try
           {
               SqlParameter MAction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);
               SqlParameter MUserID = new SqlParameter(UserMaster._UserId, SqlDbType.BigInt);
               MAction.Value = 4;
               MUserID.Value = ID;

               Open(CONNECTION_STRING);
               Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster, MAction, MUserID);

           }
           catch (Exception ex)
           {
               strError = ex.Message;
           }
           finally { Close(); }
           return Ds;

       }
        public DataSet GetUserDetails(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);
                SqlParameter MRepCondition = new SqlParameter(UserMaster._RepCondition, SqlDbType.NVarChar);
                MAction.Value = 5;
                MRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster, MAction, MRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        public DataSet GetCafe(out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);
                MAction.Value = 7;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster, MAction);

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
                SqlParameter MAction = new SqlParameter(UserMaster._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(UserMaster._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 5;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster, oParmCol);

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

            DataSet DS = new DataSet();

            try
            {
                SqlParameter Paction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(UserMaster._RepCondition, SqlDbType.NVarChar);

                Paction.Value = 6;
                pRepCondition.Value = Name;

                Open(CONNECTION_STRING);

                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure,
                    UserMaster.SP_UserMaster, Paction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return DS;
        }

        public DataSet GetUserRightTable(out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter Paction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);

                Paction.Value = 8;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster,Paction);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public int InsertUserAuthDetails(ref  UserMaster obj_User, out string strError)
        {
            int InsertRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter MAction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);
                SqlParameter MUserID = new SqlParameter(UserMaster._FkUserId, SqlDbType.BigInt);
                SqlParameter MFormCaption = new SqlParameter(UserMaster._FormCaption, SqlDbType.NVarChar);
                SqlParameter MViewAuth = new SqlParameter(UserMaster._ViewAuth, SqlDbType.Bit);
                SqlParameter MAddAuth = new SqlParameter(UserMaster._AddAuth, SqlDbType.Bit);
                SqlParameter MDelAuth = new SqlParameter(UserMaster._DelAuth, SqlDbType.Bit);
                SqlParameter MEditAuth = new SqlParameter(UserMaster._EditAuth, SqlDbType.Bit);
                SqlParameter MPrintAuth = new SqlParameter(UserMaster._PrintAuth, SqlDbType.Bit);

                MAction.Value = 9;
                MUserID.Value = obj_User.FkUserId;
                MFormCaption.Value = obj_User.FormCaption;
                MViewAuth.Value = obj_User.ViewAuth;
                MAddAuth.Value = obj_User.AddAuth;
                MDelAuth.Value = obj_User.DelAuth;
                MEditAuth.Value = obj_User.EditAuth;
                MPrintAuth.Value = obj_User.PrintAuth;

                SqlParameter[] ParamArray = new SqlParameter[] {MAction ,MUserID, MFormCaption, MViewAuth, MAddAuth,
                    MDelAuth, MEditAuth,MPrintAuth };
                Open(CONNECTION_STRING);
                BeginTransaction();
                InsertRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster, ParamArray);

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
                RollBackTransaction();
            }
            finally
            {
                Close();
            }
            return InsertRow;

        }


        public int InsertUserEmailDetail(ref  UserMaster obj_User, out string strError)
        {
            int InsertRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter MAction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);
                SqlParameter MUserID = new SqlParameter(UserMaster._FkUserId, SqlDbType.BigInt);
                SqlParameter MFormCaption = new SqlParameter(UserMaster._FormCaption, SqlDbType.NVarChar);
                SqlParameter MEmail = new SqlParameter(UserMaster._Email,SqlDbType.Bit);

                MAction.Value = 10;
                MUserID.Value = obj_User.FkUserId;
                MFormCaption.Value = obj_User.FormCaption;
                MEmail.Value = obj_User.Email;



                SqlParameter[] ParamArray = new SqlParameter[] { MAction, MUserID, MFormCaption, MEmail };
                Open(CONNECTION_STRING);
                BeginTransaction();
                InsertRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster, ParamArray);

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
                RollBackTransaction();
            }
            finally
            {
                Close();
            }
            return InsertRow;



        }

        public int InsertUserSiteDetail(ref  UserMaster obj_User, out string strError)
        {
            int InsertRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter MAction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);
                SqlParameter MUserID = new SqlParameter(UserMaster._FkUserId, SqlDbType.BigInt);
                SqlParameter MCafeId = new SqlParameter(UserMaster._CafeteriaId, SqlDbType.BigInt);
                SqlParameter MChecked = new SqlParameter(UserMaster._Cheked, SqlDbType.Bit);
               
                MAction.Value = 11;
                MUserID.Value = obj_User.FkUserId;
                MCafeId.Value = obj_User.CafeteriaId;
                MChecked.Value = obj_User.Cheked;


                SqlParameter[] ParamArray = new SqlParameter[] { MAction, MUserID, MCafeId, MChecked };
                Open(CONNECTION_STRING);
                BeginTransaction();
                InsertRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster, ParamArray);

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
                RollBackTransaction();
            }
            finally
            {
                Close();
            }
            return InsertRow;

        }
        public int InsertUserPermissionForEditPO(int UserId,String FormName,int Permission,String Password, out string strError)
        {
            int InsertRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter MAction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);
                SqlParameter MUserID = new SqlParameter(UserMaster._FkUserId, SqlDbType.BigInt);
                SqlParameter MCafeId = new SqlParameter(UserMaster._FormCaption, SqlDbType.NVarChar);
                SqlParameter MChecked = new SqlParameter(UserMaster._Cheked, SqlDbType.Bit);
                SqlParameter MPassword = new SqlParameter(UserMaster._Password, SqlDbType.NVarChar);

                MAction.Value = 12;
                MUserID.Value = UserId;
                MCafeId.Value = FormName;
                MChecked.Value = Permission;
                MPassword.Value = Password;

                SqlParameter[] ParamArray = new SqlParameter[] { MAction, MUserID, MCafeId, MChecked,MPassword };
                Open(CONNECTION_STRING);
                BeginTransaction();
                InsertRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster, ParamArray);

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
                RollBackTransaction();
            }
            finally
            {
                Close();
            }
            return InsertRow;

        }
        public int InsertUserPermissionForEditUnit(ref  UserMaster obj_User, out string strError)
        {
            int InsertRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter MAction = new SqlParameter(UserMaster._Action, SqlDbType.BigInt);
                SqlParameter MUserID = new SqlParameter(UserMaster._FkUserId, SqlDbType.BigInt);
                SqlParameter MCafeId = new SqlParameter(UserMaster._CafeteriaId, SqlDbType.BigInt);
                SqlParameter MChecked = new SqlParameter(UserMaster._Cheked, SqlDbType.Bit);

                MAction.Value = 11;
                MUserID.Value = obj_User.FkUserId;
                MCafeId.Value = obj_User.CafeteriaId;
                MChecked.Value = obj_User.Cheked;


                SqlParameter[] ParamArray = new SqlParameter[] { MAction, MUserID, MCafeId, MChecked };
                Open(CONNECTION_STRING);
                BeginTransaction();
                InsertRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, UserMaster.SP_UserMaster, ParamArray);

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
                RollBackTransaction();
            }
            finally
            {
                Close();
            }
            return InsertRow;

        }
        #endregion

    }
}