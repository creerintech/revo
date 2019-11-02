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
    public class DMSupplierMaster : Utility.Setting
    {
        #region[Business Logic]

        public int InsertRecord(ref SuplierMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(SuplierMaster._Action, SqlDbType.BigInt);
                SqlParameter pSuplierCode = new SqlParameter(SuplierMaster._SuplierCode, SqlDbType.NVarChar);
                SqlParameter pSuplierName = new SqlParameter(SuplierMaster._SuplierName, SqlDbType.NVarChar);
                SqlParameter pAddress = new SqlParameter(SuplierMaster._Address, SqlDbType.NVarChar);
                SqlParameter pTelNo = new SqlParameter(SuplierMaster._TelNo, SqlDbType.NVarChar);
                SqlParameter pMobileNo = new SqlParameter(SuplierMaster._MobileNo, SqlDbType.NVarChar);
                SqlParameter pEmail = new SqlParameter(SuplierMaster._Email, SqlDbType.NVarChar);
                SqlParameter pNote = new SqlParameter(SuplierMaster._Note, SqlDbType.NVarChar);
                //------Newly Added Fields--------
                SqlParameter pWebSite = new SqlParameter(SuplierMaster._WebSite, SqlDbType.NVarChar);
                SqlParameter pPersonName = new SqlParameter(SuplierMaster._PersonName, SqlDbType.NVarChar);
                SqlParameter pDesignation = new SqlParameter(SuplierMaster._PDesignation, SqlDbType.NVarChar);
                SqlParameter pPMobileNo = new SqlParameter(SuplierMaster._PMobileNo, SqlDbType.NVarChar);
                SqlParameter pEmailId = new SqlParameter(SuplierMaster._PEmailId, SqlDbType.NVarChar);
                SqlParameter ppWebsite = new SqlParameter(SuplierMaster._PWebsite, SqlDbType.NVarChar);
                SqlParameter pTaxRegNo = new SqlParameter(SuplierMaster._STaxRegNo, SqlDbType.NVarChar);
                SqlParameter pTaxJurisdiction = new SqlParameter(SuplierMaster._STaxJurisdiction, SqlDbType.NVarChar);
                SqlParameter pVATNo = new SqlParameter(SuplierMaster._VATNo, SqlDbType.NVarChar);
                SqlParameter pTINNo = new SqlParameter(SuplierMaster._TINNo, SqlDbType.NVarChar);
                SqlParameter pCentralSaleTaxRagNo = new SqlParameter(SuplierMaster._CentralSaleTaxRagNo, SqlDbType.NVarChar);
                SqlParameter pExciseRange = new SqlParameter(SuplierMaster._ExciseRange, SqlDbType.NVarChar);
                SqlParameter pExciseDivision = new SqlParameter(SuplierMaster._ExciseDivision, SqlDbType.NVarChar);
                SqlParameter pExciseCircle = new SqlParameter(SuplierMaster._ExciseCircle, SqlDbType.NVarChar);
                SqlParameter pExciseZone = new SqlParameter(SuplierMaster._ExciseZone, SqlDbType.NVarChar);
                SqlParameter pExciseCollectorate = new SqlParameter(SuplierMaster._ExciseCollectorate, SqlDbType.NVarChar);
                SqlParameter pExciseECCNO = new SqlParameter(SuplierMaster._ExciseECCNO, SqlDbType.NVarChar);
                SqlParameter pTIN_BINNo = new SqlParameter(SuplierMaster._TIN_BINNo, SqlDbType.NVarChar);
                SqlParameter pPANNo = new SqlParameter(SuplierMaster._PANNo, SqlDbType.NVarChar);
                SqlParameter pTDSCertificate = new SqlParameter(SuplierMaster._TDSCertificate, SqlDbType.NVarChar);
                SqlParameter pImgTaxRegNoPath= new SqlParameter(SuplierMaster._ImgTaxRegNoPath, SqlDbType.NVarChar);
                SqlParameter pImgPanNoPath = new SqlParameter(SuplierMaster._ImgPanNoPath, SqlDbType.NVarChar);
                SqlParameter pStateID = new SqlParameter(SuplierMaster._StateID, SqlDbType.BigInt);
                SqlParameter pCreatedBy = new SqlParameter(SuplierMaster._UserId, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(SuplierMaster._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(SuplierMaster._IsDeleted, SqlDbType.Bit);

                pAction.Value = 1;
                pSuplierCode.Value = Entity_Call.SuplierCode;
                pSuplierName.Value = Entity_Call.SuplierName;
                pAddress.Value = Entity_Call.Address;
                pTelNo.Value = Entity_Call.TelNo;
                pMobileNo.Value = Entity_Call.MobileNo;
                pEmail.Value = Entity_Call.Email;
                pNote.Value = Entity_Call.Note;
            
                //-----Newly Added Fields------
                pWebSite.Value = Entity_Call.WebSite;
                pPersonName.Value = Entity_Call.PersonName;
                pDesignation.Value = Entity_Call.PDesignation;
                pPMobileNo.Value = Entity_Call.PMobileNo;
                pEmailId.Value = Entity_Call.PEmailId;
                ppWebsite.Value = Entity_Call.PWebsite;
                pTaxRegNo.Value = Entity_Call.STaxRegNo;
                pTaxJurisdiction.Value = Entity_Call.STaxJurisdiction;
                pVATNo.Value = Entity_Call.VATNo;
                pTINNo.Value = Entity_Call.TINNo;
                pCentralSaleTaxRagNo.Value = Entity_Call.CentralSaleTaxRagNo;
                pExciseRange.Value = Entity_Call.ExciseRange;
                pExciseDivision.Value = Entity_Call.ExciseDivision;
                pExciseCircle.Value = Entity_Call.ExciseCircle;
                pExciseZone.Value = Entity_Call.ExciseZone;
                pExciseCollectorate.Value = Entity_Call.ExciseCollectorate;
                pExciseECCNO.Value = Entity_Call.ExciseECCNO;
                pTIN_BINNo.Value = Entity_Call.TIN_BINNo;
                pPANNo.Value = Entity_Call.PANNo;
                pTDSCertificate.Value = Entity_Call.TDSCertificate;
                pImgTaxRegNoPath.Value = Entity_Call.ImgTaxRegNoPath;
                pImgPanNoPath.Value=Entity_Call.ImgPanNoPath;
                pStateID.Value = Entity_Call.StateID;
                pCreatedBy.Value = Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;
                pIsDeleted.Value = Entity_Call.IsDeleted;

                SqlParameter[] Param = new SqlParameter[] { pAction, pSuplierCode, pSuplierName, pAddress, pTelNo, pMobileNo, pEmail, pNote,
                    pWebSite,pPersonName,pDesignation,pPMobileNo,pEmailId,ppWebsite,pTaxRegNo,pTaxJurisdiction,pVATNo,pTINNo,pCentralSaleTaxRagNo,
                    pExciseRange,pExciseDivision,pExciseCircle,pExciseZone,pExciseCollectorate,pExciseECCNO,pTIN_BINNo,pPANNo,pTDSCertificate,
                    pImgTaxRegNoPath,pImgPanNoPath,pStateID,
                    pCreatedBy, pCreatedDate, pIsDeleted };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, SuplierMaster.SP_SuplierMaster, Param);

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

        public int Insert_SupplierTandC(ref SuplierMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(SuplierMaster._Action, SqlDbType.BigInt);
                SqlParameter pTermsId = new SqlParameter(SuplierMaster._TermsID, SqlDbType.BigInt);
                SqlParameter pSuplierId = new SqlParameter(SuplierMaster._SuplierId, SqlDbType.BigInt);
                SqlParameter pTitle = new SqlParameter(SuplierMaster._TermsTitle, SqlDbType.NVarChar);
                SqlParameter pTermsCondition = new SqlParameter(SuplierMaster._TermsDetls, SqlDbType.NVarChar);
                pAction.Value = 9;
                pSuplierId.Value = Entity_Call.SuplierId;
                pTermsId.Value = Entity_Call.TermsID;
                pTitle.Value = Entity_Call.TermsTitle;
                pTermsCondition.Value = Entity_Call.TermsDetls;
                SqlParameter[] Param = new SqlParameter[] { pAction,pSuplierId, pTermsId, pTitle, pTermsCondition };
                Open(CONNECTION_STRING);
                BeginTransaction();
                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, SuplierMaster.SP_SuplierMaster, Param);
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

        public int UpdateRecord(ref SuplierMaster Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(SuplierMaster._Action, SqlDbType.BigInt);
                SqlParameter pSuplierId = new SqlParameter(SuplierMaster._SuplierId, SqlDbType.BigInt);
                SqlParameter pSuplierCode = new SqlParameter(SuplierMaster._SuplierCode, SqlDbType.NVarChar);
                SqlParameter pSuplierName = new SqlParameter(SuplierMaster._SuplierName, SqlDbType.NVarChar);
                SqlParameter pAddress = new SqlParameter(SuplierMaster._Address, SqlDbType.NVarChar);
                SqlParameter pTelNo = new SqlParameter(SuplierMaster._TelNo, SqlDbType.NVarChar);
                SqlParameter pMobileNo = new SqlParameter(SuplierMaster._MobileNo, SqlDbType.NVarChar);
                SqlParameter pEmail = new SqlParameter(SuplierMaster._Email, SqlDbType.NVarChar);
                SqlParameter pNote = new SqlParameter(SuplierMaster._Note, SqlDbType.NVarChar);

                //------Newly Added Fields--------
                SqlParameter pWebSite = new SqlParameter(SuplierMaster._WebSite, SqlDbType.NVarChar);
                SqlParameter pPersonName = new SqlParameter(SuplierMaster._PersonName, SqlDbType.NVarChar);
                SqlParameter pDesignation = new SqlParameter(SuplierMaster._PDesignation, SqlDbType.NVarChar);
                SqlParameter pPMobileNo = new SqlParameter(SuplierMaster._PMobileNo, SqlDbType.NVarChar);
                SqlParameter pEmailId = new SqlParameter(SuplierMaster._PEmailId, SqlDbType.NVarChar);
                SqlParameter ppWebsite = new SqlParameter(SuplierMaster._PWebsite, SqlDbType.NVarChar);
                SqlParameter pTaxRegNo = new SqlParameter(SuplierMaster._STaxRegNo, SqlDbType.NVarChar);
                SqlParameter pTaxJurisdiction = new SqlParameter(SuplierMaster._STaxJurisdiction, SqlDbType.NVarChar);
                SqlParameter pVATNo = new SqlParameter(SuplierMaster._VATNo, SqlDbType.NVarChar);
                SqlParameter pTINNo = new SqlParameter(SuplierMaster._TINNo, SqlDbType.NVarChar);
                SqlParameter pCentralSaleTaxRagNo = new SqlParameter(SuplierMaster._CentralSaleTaxRagNo, SqlDbType.NVarChar);
                SqlParameter pExciseRange = new SqlParameter(SuplierMaster._ExciseRange, SqlDbType.NVarChar);
                SqlParameter pExciseDivision = new SqlParameter(SuplierMaster._ExciseDivision, SqlDbType.NVarChar);
                SqlParameter pExciseCircle = new SqlParameter(SuplierMaster._ExciseCircle, SqlDbType.NVarChar);
                SqlParameter pExciseZone = new SqlParameter(SuplierMaster._ExciseZone, SqlDbType.NVarChar);
                SqlParameter pExciseCollectorate = new SqlParameter(SuplierMaster._ExciseCollectorate, SqlDbType.NVarChar);
                SqlParameter pExciseECCNO = new SqlParameter(SuplierMaster._ExciseECCNO, SqlDbType.NVarChar);
                SqlParameter pTIN_BINNo = new SqlParameter(SuplierMaster._TIN_BINNo, SqlDbType.NVarChar);
                SqlParameter pPANNo = new SqlParameter(SuplierMaster._PANNo, SqlDbType.NVarChar);
                SqlParameter pTDSCertificate = new SqlParameter(SuplierMaster._TDSCertificate, SqlDbType.NVarChar);
                SqlParameter pImgTaxRegNoPath = new SqlParameter(SuplierMaster._ImgTaxRegNoPath, SqlDbType.NVarChar);
                SqlParameter pImgPanNoPath = new SqlParameter(SuplierMaster._ImgPanNoPath, SqlDbType.NVarChar);
                SqlParameter pStateID = new SqlParameter(SuplierMaster._StateID, SqlDbType.BigInt);
                SqlParameter pUpdatedBy = new SqlParameter(SuplierMaster._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(SuplierMaster._LoginDate, SqlDbType.DateTime);

                pAction.Value = 2;
                pSuplierId.Value = Entity_Call.SuplierId;
                pSuplierCode.Value = Entity_Call.SuplierCode;
                pSuplierName.Value = Entity_Call.SuplierName;
                pAddress.Value = Entity_Call.Address;
                pTelNo.Value = Entity_Call.TelNo;
                pMobileNo.Value = Entity_Call.MobileNo;
                pEmail.Value = Entity_Call.Email;
                pNote.Value = Entity_Call.Note;

                //-----Newly Added Fields------
                pWebSite.Value = Entity_Call.WebSite;
                pPersonName.Value = Entity_Call.PersonName;
                pDesignation.Value = Entity_Call.PDesignation;
                pPMobileNo.Value = Entity_Call.PMobileNo;
                pEmailId.Value = Entity_Call.PEmailId;
                ppWebsite.Value = Entity_Call.PWebsite;
                pTaxRegNo.Value = Entity_Call.STaxRegNo;
                pTaxJurisdiction.Value = Entity_Call.STaxJurisdiction;
                pVATNo.Value = Entity_Call.VATNo;
                pTINNo.Value = Entity_Call.TINNo;
                pCentralSaleTaxRagNo.Value = Entity_Call.CentralSaleTaxRagNo;
                pExciseRange.Value = Entity_Call.ExciseRange;
                pExciseDivision.Value = Entity_Call.ExciseDivision;
                pExciseCircle.Value = Entity_Call.ExciseCircle;
                pExciseZone.Value = Entity_Call.ExciseZone;
                pExciseCollectorate.Value = Entity_Call.ExciseCollectorate;
                pExciseECCNO.Value = Entity_Call.ExciseECCNO;
                pTIN_BINNo.Value = Entity_Call.TIN_BINNo;
                pPANNo.Value = Entity_Call.PANNo;
                pTDSCertificate.Value = Entity_Call.TDSCertificate;
                pImgTaxRegNoPath.Value = Entity_Call.ImgTaxRegNoPath;
                pImgPanNoPath.Value = Entity_Call.ImgPanNoPath;
                pStateID.Value = Entity_Call.StateID;
                pUpdatedBy.Value = Entity_Call.UserId;
                pUpdatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pSuplierId, pSuplierCode, pSuplierName, pAddress, pTelNo, pMobileNo, pEmail, pNote,
                     pWebSite,pPersonName,pDesignation,pPMobileNo,pEmailId,ppWebsite,pTaxRegNo,pTaxJurisdiction,pVATNo,pTINNo,pCentralSaleTaxRagNo,
                     pExciseRange,pExciseDivision,pExciseCircle,pExciseZone,pExciseCollectorate,pExciseECCNO,pTIN_BINNo,pPANNo,pTDSCertificate,
                     pImgTaxRegNoPath,pImgPanNoPath,pStateID,
                     pUpdatedBy, pUpdatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, SuplierMaster.SP_SuplierMaster, Param);

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

        public int DeleteRecord(ref SuplierMaster Entity_Call, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(SuplierMaster._Action, SqlDbType.BigInt);
                SqlParameter pSuplierId = new SqlParameter(SuplierMaster._SuplierId, SqlDbType.BigInt);
                SqlParameter pDeletedBy = new SqlParameter(SuplierMaster._UserId, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(SuplierMaster._LoginDate, SqlDbType.DateTime);
                SqlParameter pIsDeleted = new SqlParameter(SuplierMaster._IsDeleted, SqlDbType.Bit);

                pAction.Value = 3;
                pSuplierId.Value = Entity_Call.SuplierId;

                pDeletedBy.Value = Entity_Call.UserId;
                pDeletedDate.Value = Entity_Call.LoginDate;
                pIsDeleted.Value = Entity_Call.IsDeleted;

                SqlParameter[] Param = new SqlParameter[] { pAction, pSuplierId, pDeletedBy, pDeletedDate, pIsDeleted };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, SuplierMaster.SP_SuplierMaster, Param);

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

        public DataSet GetSupplierForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(SuplierMaster._Action, SqlDbType.BigInt);
                SqlParameter pSuplierId = new SqlParameter(SuplierMaster._SuplierId, SqlDbType.BigInt);

                pAction.Value = 5;
                pSuplierId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, SuplierMaster.SP_SuplierMaster, pAction, pSuplierId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetSupplier(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(SuplierMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(SuplierMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 6;
                pRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, SuplierMaster.SP_SuplierMaster, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetTerms(out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(SuplierMaster._Action, SqlDbType.BigInt);

                pAction.Value = 8;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, SuplierMaster.SP_SuplierMaster, pAction);

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
                SqlParameter pAction = new SqlParameter(SuplierMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(SuplierMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 6;
                pRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { pAction, pRepCondition };

                Open(CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, SuplierMaster.SP_SuplierMaster, oParmCol);

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
                SqlParameter pAction = new SqlParameter(SuplierMaster._Action, SqlDbType.BigInt);
                SqlParameter pRepCondition = new SqlParameter(SuplierMaster._strCond, SqlDbType.NVarChar);

                pAction.Value = 7;
                pRepCondition.Value = Name;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, SuplierMaster.SP_SuplierMaster, pAction, pRepCondition);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetSuppcode(out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                pAction.Value = 4;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, SuplierMaster.SP_SuplierMaster, pAction);
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


        #endregion

        public DMSupplierMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet FillCombo(out string StrError)
        {
            StrError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(SuplierMaster._Action, SqlDbType.BigInt);
                MAction.Value = 10;

                Open(Setting.CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, SuplierMaster.SP_SuplierMaster, MAction);

            }
            catch (Exception ex)
            {
                StrError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
    }
}
