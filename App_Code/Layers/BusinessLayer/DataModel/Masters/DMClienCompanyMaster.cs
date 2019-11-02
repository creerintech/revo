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
    public class DMClienCompanyMaster:Utility.Setting
    {
        public int InsertRecord(ref ClientCompMaster Entity_Call,string StrError)
        {
            int iInsert = 0;
            StrError =string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ClientCompMaster._Action,SqlDbType.BigInt);
                SqlParameter pCompanyName = new SqlParameter(ClientCompMaster._CompanyName,SqlDbType.NVarChar);
                SqlParameter pSupplierFor = new SqlParameter(ClientCompMaster._SuplierFor,SqlDbType.NVarChar);
                SqlParameter pAddress = new SqlParameter(ClientCompMaster._Address,SqlDbType.NVarChar);
                SqlParameter pWebUrl = new SqlParameter(ClientCompMaster._WebUrl, SqlDbType.NVarChar);
                SqlParameter pRemark = new SqlParameter(ClientCompMaster._Remark,SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(ClientCompMaster._CreatedBy, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(ClientCompMaster._CreatedDate,SqlDbType.DateTime);
                SqlParameter pIsDelete = new SqlParameter(ClientCompMaster._IsDelete,SqlDbType.Bit);
                

                pAction.Value = 1;
                pCompanyName.Value = Entity_Call.CompanyName;
                pSupplierFor.Value = Entity_Call.SupplierFor;
                pAddress.Value = Entity_Call.Address;
                pWebUrl.Value = Entity_Call.WebUrl;
                pRemark.Value = Entity_Call.Remark;
                pCreatedBy.Value = Entity_Call.CreatedBy;
                pCreatedDate.Value =Entity_Call.CreatedDate;
                pIsDelete.Value = Entity_Call.IsDelete;

                


                SqlParameter[] param = new SqlParameter[] { pAction, pCompanyName, pSupplierFor,pAddress,pWebUrl,
                                                            pRemark,pIsDelete,pCreatedBy,pCreatedDate};
                //pPersonName,pDesignation,pContactNo1,pContactNo2,
                //                                            pEmailId1,pEmailId2,pNote

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection,_Transaction,CommandType.StoredProcedure,ClientCompMaster.SP_ClientCompanyMaster,param);
                if (iInsert > 0)
                {
                    CommitTransaction();
                    
                }
                else
                {
                    RollBackTransaction();
                }
            }
            catch(Exception ex)
            {
                RollBackTransaction();
                StrError = ex.Message;
            }
            return iInsert;
        }

        public int InsertDertailsRecord(ref ClientCompMaster Entity_Call, string StrError)
        { 
            int iInsert = 0;
            StrError =string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ClientCompMaster._Action,SqlDbType.BigInt);
                SqlParameter pClientCompnayId = new SqlParameter(ClientCompMaster._ClienCompanyId,SqlDbType.BigInt);
                SqlParameter pPersonName = new SqlParameter(ClientCompMaster._PersonName,SqlDbType.NVarChar);
                SqlParameter pDesignation = new SqlParameter(ClientCompMaster._Designation,SqlDbType.NVarChar);
                SqlParameter pContactNo1 = new SqlParameter(ClientCompMaster._ContactNo1,SqlDbType.NVarChar);
                SqlParameter pContactNo2 = new SqlParameter(ClientCompMaster._ContactNo2,SqlDbType.NVarChar);
                SqlParameter pEmailId1 = new SqlParameter(ClientCompMaster._EmailId1,SqlDbType.NVarChar);
                SqlParameter pEmailId2 = new SqlParameter(ClientCompMaster._EmailId2,SqlDbType.NVarChar);
                SqlParameter pNote = new SqlParameter(ClientCompMaster._Note,SqlDbType.NVarChar);

                pAction.Value = 2;
                pClientCompnayId.Value = Entity_Call.ClientCompanyId;
                pPersonName.Value = Entity_Call.PersonName;
                pDesignation.Value = Entity_Call.Designation;
                pContactNo1.Value = Entity_Call.ContactNo1;
                pContactNo2.Value = Entity_Call.ContactNo2;
                pEmailId1.Value = Entity_Call.EmailId1;
                pEmailId2.Value = Entity_Call.EmailId2;
                pNote.Value = Entity_Call.Note;


                SqlParameter[] param = new SqlParameter[] {pAction,pClientCompnayId,pPersonName,pDesignation,pContactNo1,
                                                            pContactNo2,pEmailId1,pEmailId2,pNote};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ClientCompMaster.SP_ClientCompanyMaster, param);
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
            return iInsert;
        }
        public DataSet ChkDuplicate(string Name,out string StrError)
        {
            StrError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ClientCompMaster._Action, SqlDbType.VarChar);
                SqlParameter pStrCond = new SqlParameter(ClientCompMaster._CompanyName, SqlDbType.NVarChar);
               
                pAction.Value = 7;
                pStrCond.Value = Name;
               

                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] { pAction, pStrCond };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, ClientCompMaster.SP_ClientCompanyMaster, param);
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

        public DataSet GetItem(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ClientCompMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(ClientCompMaster._StrCond, SqlDbType.NVarChar);

                pAction.Value = 3;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ClientCompMaster.SP_ClientCompanyMaster, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetCCMFORPRINT(int ClientCompanyID, int CompanyID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ClientCompMaster._Action, SqlDbType.BigInt);
                SqlParameter pClientCompanyID = new SqlParameter(ClientCompMaster._ClienCompanyId, SqlDbType.NVarChar);
                SqlParameter pCompanyID = new SqlParameter(ClientCompMaster._CreatedBy, SqlDbType.NVarChar);

                pAction.Value = 10;
                pClientCompanyID.Value = ClientCompanyID;
                pCompanyID.Value = CompanyID;
                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] { pClientCompanyID, pCompanyID, pAction };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, ClientCompMaster.SP_ClientCompanyMaster, param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetItemForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ClientCompMaster._Action, SqlDbType.BigInt);
                SqlParameter pClientCompnyId = new SqlParameter(ClientCompMaster._ClienCompanyId, SqlDbType.BigInt);

                pAction.Value = 4;
                pClientCompnyId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ClientCompMaster.SP_ClientCompanyMaster, pAction, pClientCompnyId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        public int DeleteRecord(ref ClientCompMaster Entity_Call, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ClientCompMaster._Action, SqlDbType.BigInt);
                SqlParameter pClienCompanyId = new SqlParameter(ClientCompMaster._ClienCompanyId, SqlDbType.BigInt);

                SqlParameter pDeletedBy = new SqlParameter(ClientCompMaster._DeletedBy, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(ClientCompMaster._DeletedDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(ClientCompMaster._IsDelete, SqlDbType.Bit);

                pAction.Value = 5;
                pClienCompanyId.Value = Entity_Call.ClientCompanyId;

                pDeletedBy.Value = Entity_Call.DeletedBy;
                pDeletedDate.Value = Entity_Call.DeletedDate;
                pIsDeleted.Value = Entity_Call.IsDelete;

                SqlParameter[] Param = new SqlParameter[] { pAction, pClienCompanyId, pDeletedBy, pDeletedDate, pIsDeleted };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ClientCompMaster.SP_ClientCompanyMaster, Param);

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
        public int UpdateRecord(ref ClientCompMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ClientCompMaster._Action, SqlDbType.BigInt);
                SqlParameter pClientCompanyId = new SqlParameter(ClientCompMaster._ClienCompanyId,SqlDbType.BigInt);
                SqlParameter pCompanyName = new SqlParameter(ClientCompMaster._CompanyName, SqlDbType.NVarChar);
                SqlParameter pSupplierFor = new SqlParameter(ClientCompMaster._SuplierFor, SqlDbType.NVarChar);
                SqlParameter pAddress = new SqlParameter(ClientCompMaster._Address, SqlDbType.NVarChar);
                SqlParameter pWebUrl = new SqlParameter(ClientCompMaster._WebUrl, SqlDbType.NVarChar);
                SqlParameter pRemark = new SqlParameter(ClientCompMaster._Remark, SqlDbType.NVarChar);
                SqlParameter pUpdatedBy = new SqlParameter(ClientCompMaster._UpdatedBy, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(ClientCompMaster._UpdatedDate, SqlDbType.DateTime);
                


                pAction.Value = 6;
                pClientCompanyId.Value = Entity_Call.ClientCompanyId;
                pCompanyName.Value = Entity_Call.CompanyName;
                pSupplierFor.Value = Entity_Call.SupplierFor;
                pAddress.Value = Entity_Call.Address;
                pWebUrl.Value = Entity_Call.WebUrl;
                pRemark.Value = Entity_Call.Remark;
                pUpdatedBy.Value = Entity_Call.UpdatedBy;
                pUpdatedDate.Value = Entity_Call.UpdatedDate;
                




                SqlParameter[] param = new SqlParameter[] { pAction,pClientCompanyId,pCompanyName, pSupplierFor,pAddress,pWebUrl,
                                                            pRemark,pUpdatedBy,pUpdatedDate};
       

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ClientCompMaster.SP_ClientCompanyMaster, param);
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
            return iInsert;
        }
        public int InsertDetailsRecord(ref ClientCompMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ClientCompMaster._Action,SqlDbType.BigInt);
                SqlParameter pPersonId = new SqlParameter(ClientCompMaster._PersonId,SqlDbType.BigInt);
                SqlParameter pClientCompanyId = new SqlParameter(ClientCompMaster._ClienCompanyId, SqlDbType.BigInt);
                SqlParameter pPersonName = new SqlParameter(ClientCompMaster._PersonName, SqlDbType.NVarChar);
                SqlParameter pDesignation = new SqlParameter(ClientCompMaster._Designation, SqlDbType.NVarChar);
                SqlParameter pContactNo1 = new SqlParameter(ClientCompMaster._ContactNo1, SqlDbType.NVarChar);
                SqlParameter pContactNo2 = new SqlParameter(ClientCompMaster._ContactNo2, SqlDbType.NVarChar);
                SqlParameter pEmailId1 = new SqlParameter(ClientCompMaster._EmailId1, SqlDbType.NVarChar);
                SqlParameter pEmailId2 = new SqlParameter(ClientCompMaster._EmailId2, SqlDbType.NVarChar);
                SqlParameter pNote = new SqlParameter(ClientCompMaster._Note, SqlDbType.NVarChar);

                pAction.Value = 8;
                pPersonId.Value = Entity_Call.PersonId;
                pClientCompanyId.Value = Entity_Call.ClientCompanyId;
                pPersonName.Value = Entity_Call.PersonName;
                pDesignation.Value = Entity_Call.Designation;
                pContactNo1.Value = Entity_Call.ContactNo1;
                pContactNo2.Value = Entity_Call.ContactNo2;
                pEmailId1.Value = Entity_Call.EmailId1;
                pEmailId2.Value = Entity_Call.EmailId2;
                pNote.Value = Entity_Call.Note;

                SqlParameter[] param = new SqlParameter[] { pAction,pPersonId,pClientCompanyId,pPersonName,
                                                            pDesignation,pContactNo1,pContactNo2,
                                                            pEmailId1,pEmailId2,pNote};
                Open(CONNECTION_STRING);
                BeginTransaction();
                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ClientCompMaster.SP_ClientCompanyMaster, param);

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
        public DataSet GetSearchItem(string SearchCond)
        {
            DataSet Ds =new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ClientCompMaster._Action,SqlDbType.BigInt);
                SqlParameter pSearchCondition = new SqlParameter(ClientCompMaster._StrCond,SqlDbType.NVarChar);

                pAction.Value = 9;
                pSearchCondition.Value = SearchCond;

                SqlParameter[] param = new SqlParameter[] { pAction,pSearchCondition};
                
                Open(CONNECTION_STRING);
                BeginTransaction();


                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, "SP_ClientCompanyMaster",param);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return Ds;
        }

        public string[] GetSuggestedRecord(string prefixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;
            try
            {               
                SqlParameter pAction = new SqlParameter(ClientCompMaster._Action, SqlDbType.VarChar);
                SqlParameter pRepCondition = new SqlParameter(ClientCompMaster._StrCond, SqlDbType.NVarChar);
                pAction.Value = 9;
                pRepCondition.Value = prefixText;
                
                SqlParameter[] param = new SqlParameter[] { pAction, pRepCondition};
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, ClientCompMaster.SP_ClientCompanyMaster, param);

                if (dr != null && dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        ListItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Name"].ToString(),"");
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
        public DMClienCompanyMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
