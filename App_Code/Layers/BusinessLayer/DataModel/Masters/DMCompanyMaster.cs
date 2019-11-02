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
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using MayurInventory.DALSQLHelper;
namespace MayurInventory.DataModel
{

    /// <summary>
    /// Summary description for DMCompanyMaster
    /// </summary>
    public class DMCompanyMaster : Utility.Setting
    {
        public int InsertRecord(ref CompanyMaster Entity_Call, out string StrError)
        {
            int iInsert = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(CompanyMaster._Action, SqlDbType.NVarChar);
                SqlParameter pCompanyName = new SqlParameter(CompanyMaster._CompanyName, SqlDbType.NVarChar);
                SqlParameter pCAddress = new SqlParameter(CompanyMaster._CAddress, SqlDbType.NVarChar);
                SqlParameter pCLogo = new SqlParameter(CompanyMaster._CLogo, SqlDbType.NVarChar);
                SqlParameter PphoneNo = new SqlParameter(CompanyMaster._PhoneNo, SqlDbType.NVarChar);
                SqlParameter pEmailId = new SqlParameter(CompanyMaster._EmailId, SqlDbType.NVarChar);
                SqlParameter pWebsite = new SqlParameter(CompanyMaster._Website, SqlDbType.NVarChar);
                SqlParameter pFaxNo = new SqlParameter(CompanyMaster._FaxNo, SqlDbType.NVarChar);
                SqlParameter pTinNo = new SqlParameter(CompanyMaster._TinNo, SqlDbType.NVarChar);
                SqlParameter pVatNo = new SqlParameter(CompanyMaster._VatNo, SqlDbType.NVarChar);
                SqlParameter pServiceTaxNo = new SqlParameter(CompanyMaster._ServiceTaxNo, SqlDbType.NVarChar);
                SqlParameter pDigitalSignature = new SqlParameter(CompanyMaster._DigitalSignature, SqlDbType.NVarChar);
                SqlParameter pDigitalSignature1 = new SqlParameter(CompanyMaster._DigitalSignature1, SqlDbType.NVarChar);
                SqlParameter pDigitalSignature2 = new SqlParameter(CompanyMaster._DigitalSignature2, SqlDbType.NVarChar);
                SqlParameter pNote = new SqlParameter(CompanyMaster._Note, SqlDbType.NVarChar);
                SqlParameter pBankId = new SqlParameter(CompanyMaster._BankId, SqlDbType.BigInt);
                SqlParameter pabbreviation = new SqlParameter(CompanyMaster._abbreviation, SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(CompanyMaster._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(CompanyMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 1;
                pCompanyName.Value = Entity_Call.CompanyName;
                pCAddress.Value = Entity_Call.CAddress;
                pCLogo.Value = Entity_Call.CLogo;
                PphoneNo.Value = Entity_Call.PhoneNo;
                pEmailId.Value = Entity_Call.EmailId;
                pWebsite.Value = Entity_Call.Website;
                pFaxNo.Value = Entity_Call.FaxNo;
                pTinNo.Value = Entity_Call.TinNo;
                pVatNo.Value = Entity_Call.VatNo;
                pServiceTaxNo.Value = Entity_Call.ServiceTaxNo;
                pDigitalSignature.Value = Entity_Call.DigitalSignature;
                pDigitalSignature1.Value = Entity_Call.DigitalSignature1;
                pDigitalSignature2.Value = Entity_Call.DigitalSignature2;
                pNote.Value = Entity_Call.Note;
                pBankId.Value = Entity_Call.BankId;
                pabbreviation.Value = Entity_Call.abbreviation;
                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] param = new SqlParameter[]{pAction,pCompanyName,pCAddress,pCLogo,PphoneNo,pEmailId,
                    pWebsite,pFaxNo,pTinNo,pVatNo,pServiceTaxNo,pDigitalSignature,pDigitalSignature1,pDigitalSignature2,pNote,pBankId,
                    pCreatedBy,pCreatedDate,pabbreviation};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure,
                    CompanyMaster.SP_CompanyMaster, param);

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

            finally
            {
                Close();
            }
            return iInsert;
        }

        public int UpdateRecord(ref CompanyMaster Entity_Call, out string StrError)
        {
            int iUpdate = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(CompanyMaster._Action, SqlDbType.NVarChar);
                SqlParameter pCompanyId = new SqlParameter(CompanyMaster._CompanyId, SqlDbType.NVarChar);
                SqlParameter pCompanyName = new SqlParameter(CompanyMaster._CompanyName, SqlDbType.NVarChar);
                SqlParameter pCAddress = new SqlParameter(CompanyMaster._CAddress, SqlDbType.NVarChar);
                SqlParameter pCLogo = new SqlParameter(CompanyMaster._CLogo, SqlDbType.NVarChar);
                SqlParameter PphoneNo = new SqlParameter(CompanyMaster._PhoneNo, SqlDbType.NVarChar);
                SqlParameter pEmailId = new SqlParameter(CompanyMaster._EmailId, SqlDbType.NVarChar);
                SqlParameter pWebsite = new SqlParameter(CompanyMaster._Website, SqlDbType.NVarChar);
                SqlParameter pFaxNo = new SqlParameter(CompanyMaster._FaxNo, SqlDbType.NVarChar);
                SqlParameter pTinNo = new SqlParameter(CompanyMaster._TinNo, SqlDbType.NVarChar);
                SqlParameter pVatNo = new SqlParameter(CompanyMaster._VatNo, SqlDbType.NVarChar);
                SqlParameter pServiceTaxNo = new SqlParameter(CompanyMaster._ServiceTaxNo, SqlDbType.NVarChar);
                SqlParameter pDigitalSignature = new SqlParameter(CompanyMaster._DigitalSignature, SqlDbType.NVarChar);
                SqlParameter pDigitalSignature1 = new SqlParameter(CompanyMaster._DigitalSignature1, SqlDbType.NVarChar);
                SqlParameter pDigitalSignature2 = new SqlParameter(CompanyMaster._DigitalSignature2, SqlDbType.NVarChar);
                SqlParameter pNote = new SqlParameter(CompanyMaster._Note, SqlDbType.NVarChar);
                SqlParameter pBankId = new SqlParameter(CompanyMaster._BankId, SqlDbType.BigInt);
                SqlParameter pabbreviation = new SqlParameter(CompanyMaster._abbreviation, SqlDbType.NVarChar);
                SqlParameter pUpdatedBy = new SqlParameter(CompanyMaster._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(CompanyMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 2;
                pCompanyId.Value = Entity_Call.CompanyId;
                pCompanyName.Value = Entity_Call.CompanyName;
                pCAddress.Value = Entity_Call.CAddress;
                pCLogo.Value = Entity_Call.CLogo;
                PphoneNo.Value = Entity_Call.PhoneNo;
                pEmailId.Value = Entity_Call.EmailId;
                pWebsite.Value = Entity_Call.Website;
                pFaxNo.Value = Entity_Call.FaxNo;
                pTinNo.Value = Entity_Call.TinNo;
                pVatNo.Value = Entity_Call.VatNo;
                pServiceTaxNo.Value = Entity_Call.ServiceTaxNo;
                pDigitalSignature.Value = Entity_Call.DigitalSignature;
                pDigitalSignature1.Value = Entity_Call.DigitalSignature1;
                pDigitalSignature2.Value = Entity_Call.DigitalSignature2;
                pNote.Value = Entity_Call.Note;
                pBankId.Value = Entity_Call.BankId;
                pabbreviation.Value = Entity_Call.abbreviation;
                pUpdatedBy.Value = Entity_Call.UserId;
                pUpdatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] param = new SqlParameter[]{pAction,pCompanyId,pCompanyName,pCAddress,pCLogo,PphoneNo,pEmailId,
                    pWebsite,pFaxNo,pTinNo,pVatNo,pServiceTaxNo,pDigitalSignature,pDigitalSignature1,pDigitalSignature2,pNote,pBankId,
                    pUpdatedBy,pUpdatedDate,pabbreviation};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iUpdate = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, CompanyMaster.SP_CompanyMaster, param);

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
                StrError = ex.Message;
            }
            finally
            {
                Close();
            }
            return iUpdate;
        }

        public int DeleteRecord(ref CompanyMaster EntityCall, out string StrError)
        {
            int iDelete = 0;
            StrError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(CompanyMaster._Action, SqlDbType.BigInt);
                SqlParameter pComapnyId = new SqlParameter(CompanyMaster._CompanyId, SqlDbType.BigInt);
                SqlParameter pDeletedBy = new SqlParameter(CompanyMaster._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(CompanyMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 3;
                pComapnyId.Value = EntityCall.CompanyId;
                pDeletedBy.Value = EntityCall.UserId;
                pDeletedDate.Value = EntityCall.LoginDate;

                SqlParameter[] param = new SqlParameter[] { pAction, pComapnyId, pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, CompanyMaster.SP_CompanyMaster, param);

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
                StrError = ex.Message;
            }
            finally
            {
                Close();
            }
            return iDelete;
        }

        public int InsertDetailsRecord(ref CompanyMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(CompanyMaster._Action, SqlDbType.BigInt);
                SqlParameter pBankId = new SqlParameter(CompanyMaster._BankId, SqlDbType.BigInt);
                SqlParameter pCompanyId = new SqlParameter(CompanyMaster._CompanyId, SqlDbType.BigInt);
                SqlParameter pAccountNo = new SqlParameter(CompanyMaster._AccountNo, SqlDbType.NVarChar);
                SqlParameter pNoteB = new SqlParameter(CompanyMaster._NoteB, SqlDbType.NVarChar);

                pAction.Value = 5;
                pBankId.Value = Entity_Call.BankId;
                pCompanyId.Value = Entity_Call.CompanyId;
                pAccountNo.Value = Entity_Call.AccountNo;
                pNoteB.Value=Entity_Call.NoteB;

                SqlParameter[] Param = new SqlParameter[] { pAction, pBankId, pCompanyId, pAccountNo, pNoteB };                                                        

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, CompanyMaster.SP_CompanyMaster, Param);

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

        public DataSet BindCombo(out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(CompanyMaster._Action, SqlDbType.BigInt);
               
                pAction.Value = 6;
                Open(CONNECTION_STRING);

                DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, CompanyMaster.SP_CompanyMaster, pAction);
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

        public DataSet GetCompanyForEdit(int ID, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(CompanyMaster._Action, SqlDbType.BigInt);
                SqlParameter pCompanyId = new SqlParameter(CompanyMaster._CompanyId, SqlDbType.BigInt);

                pAction.Value = 4;
                pCompanyId.Value = ID;

                Open(CONNECTION_STRING);

                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, CompanyMaster.SP_CompanyMaster, pAction, pCompanyId);
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

        public DataSet ChkDuplicate(string Name, out string StrError)
        {
            StrError = string.Empty;

            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(CompanyMaster._Action, SqlDbType.BigInt);
                SqlParameter pstrCond = new SqlParameter(CompanyMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 7;
                pstrCond.Value = Name;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, CompanyMaster.SP_CompanyMaster, pAction, pstrCond);
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

        public string[] GetSuggestRecord(string preFixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(CompanyMaster._Action, SqlDbType.BigInt);
                SqlParameter PrepCondition = new SqlParameter(CompanyMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 8;
                PrepCondition.Value = preFixText;

                SqlParameter[] oparamcol = new SqlParameter[] { pAction, PrepCondition };

                Open(CONNECTION_STRING);
                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, CompanyMaster.SP_CompanyMaster, oparamcol);

                if (dr != null && dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        ListItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(),
                            dr[1].ToString());

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

        public DataSet GetCompanyDtls(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(CompanyMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(CompanyMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 8;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, CompanyMaster.SP_CompanyMaster, pAction, pRepCondition);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet CompanyDtlsOnPrint(out string strError)
        {
            strError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(CompanyMaster._Action, SqlDbType.BigInt);

                pAction.Value = 9;
                Open(CONNECTION_STRING);

                DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, CompanyMaster.SP_CompanyMaster, pAction);
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

        public DMCompanyMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
