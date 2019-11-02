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
/// <summary>
/// Summary description for DMGroupMaster
/// </summary>
/// 
namespace MayurInventory.DataModel
{
    public class DMPaymentVoucher : Utility.Setting
    {
        #region[Business Logic]

        public int InsertPaymentVoucher(ref PaymentVoucher obj_PayVoc, out string strError)
        {
            int InsertRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter PAction = new SqlParameter(PaymentVoucher._Action, SqlDbType.BigInt);
                SqlParameter PVoucherTypeId = new SqlParameter(PaymentVoucher._VoucherTypeID, SqlDbType.BigInt);
                SqlParameter PVoucherType = new SqlParameter(PaymentVoucher._VoucherType, SqlDbType.Char);
                SqlParameter PVoucherDate = new SqlParameter(PaymentVoucher._VoucherDate, SqlDbType.DateTime);
                SqlParameter PVoucherDebit = new SqlParameter(PaymentVoucher._VoucherDebit, SqlDbType.BigInt);
                SqlParameter PVoucherCredit = new SqlParameter(PaymentVoucher._VoucherCredit, SqlDbType.BigInt);
                SqlParameter PLocationID = new SqlParameter(PaymentVoucher._LocationId, SqlDbType.BigInt);
                SqlParameter PVoucherAmount = new SqlParameter(PaymentVoucher._VoucherAmount, SqlDbType.Decimal);
                SqlParameter PVoucherNarration = new SqlParameter(PaymentVoucher._VoucherNarration, SqlDbType.NVarChar);
                SqlParameter PCrnote = new SqlParameter(PaymentVoucher._Crnote, SqlDbType.BigInt);
                SqlParameter PInvoiceCode = new SqlParameter(PaymentVoucher._InvoiceCode, SqlDbType.BigInt);
                SqlParameter PIsRefrence = new SqlParameter(PaymentVoucher._IsReference, SqlDbType.BigInt);

                SqlParameter PCreatedBy = new SqlParameter(PaymentVoucher._LoginID, SqlDbType.BigInt);
                SqlParameter PCreatedDate = new SqlParameter(PaymentVoucher._LoginDate, SqlDbType.DateTime);

                PAction.Value = 2;
                PVoucherType.Value = obj_PayVoc.VoucherType;
                PVoucherTypeId.Value = obj_PayVoc.VoucherTypeId;
                PVoucherDate.Value = obj_PayVoc.VoucherDate;
                PVoucherDebit.Value = obj_PayVoc.VoucherDebit;
                PVoucherCredit.Value = obj_PayVoc.VoucherCredit;
                PLocationID.Value = obj_PayVoc.LocationId;
                PVoucherAmount.Value = obj_PayVoc.VoucherAmount;
                PVoucherNarration.Value = obj_PayVoc.VoucherNarration;
                PIsRefrence.Value = obj_PayVoc.IsReference;
                PCrnote.Value = obj_PayVoc.Crnote;
                PInvoiceCode.Value = obj_PayVoc.InvoiceCode;

                PCreatedBy.Value = obj_PayVoc.LoginID.Equals("") ? 0 : obj_PayVoc.LoginID;
                PCreatedDate.Value = obj_PayVoc.LoginDate;

                SqlParameter[] ParamArray = new SqlParameter[] { PAction, PVoucherType,PVoucherTypeId, PVoucherDate,PVoucherDebit,PVoucherCredit,
                PLocationID,PVoucherAmount,PVoucherNarration,PCrnote,PInvoiceCode,PIsRefrence,PCreatedBy, PCreatedDate };
                Open(CONNECTION_STRING);
                BeginTransaction();
                InsertRow = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, PaymentVoucher.PRO_PaymentVoucher, ParamArray);

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

        public int UpdatePaymentVoucher(ref PaymentVoucher obj_obj_PayVoc, out string strError)
        {
            int InsertRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter PAction = new SqlParameter(PaymentVoucher._Action, SqlDbType.BigInt);
                SqlParameter PVoucherId = new SqlParameter(PaymentVoucher._VoucherId, SqlDbType.BigInt);
                SqlParameter PVoucherTypeId = new SqlParameter(PaymentVoucher._VoucherTypeID, SqlDbType.BigInt);
                SqlParameter PVoucherType = new SqlParameter(PaymentVoucher._VoucherType, SqlDbType.Char);
                SqlParameter PVoucherDate = new SqlParameter(PaymentVoucher._VoucherDate, SqlDbType.DateTime);
                SqlParameter PVoucherDebit = new SqlParameter(PaymentVoucher._VoucherDebit, SqlDbType.BigInt);
                SqlParameter PVoucherCredit = new SqlParameter(PaymentVoucher._VoucherCredit, SqlDbType.BigInt);
                SqlParameter PVoucherAmount = new SqlParameter(PaymentVoucher._VoucherAmount, SqlDbType.Decimal);
                SqlParameter PVoucherNarration = new SqlParameter(PaymentVoucher._VoucherNarration, SqlDbType.NVarChar);
                SqlParameter PCrnote = new SqlParameter(PaymentVoucher._Crnote, SqlDbType.BigInt);
                SqlParameter PInvoiceCode = new SqlParameter(PaymentVoucher._InvoiceCode, SqlDbType.BigInt);
                SqlParameter PIsRefrence = new SqlParameter(PaymentVoucher._IsReference, SqlDbType.BigInt);


                SqlParameter PCreatedBy = new SqlParameter(PaymentVoucher._LoginID, SqlDbType.BigInt);
                SqlParameter PCreatedDate = new SqlParameter(PaymentVoucher._LoginDate, SqlDbType.DateTime);

                PAction.Value = 3;
                PVoucherId.Value = obj_obj_PayVoc.VoucherId;
                PVoucherType.Value = obj_obj_PayVoc.VoucherType;
                PVoucherTypeId.Value = obj_obj_PayVoc.VoucherTypeId;
                PVoucherDate.Value = obj_obj_PayVoc.VoucherDate;
                PVoucherDebit.Value = obj_obj_PayVoc.VoucherDebit;
                PVoucherCredit.Value = obj_obj_PayVoc.VoucherCredit;
                PVoucherAmount.Value = obj_obj_PayVoc.VoucherAmount;
                PVoucherNarration.Value = obj_obj_PayVoc.VoucherNarration;
                PIsRefrence.Value = obj_obj_PayVoc.IsReference;
                PCrnote.Value = obj_obj_PayVoc.Crnote;
                PInvoiceCode.Value = obj_obj_PayVoc.InvoiceCode;


                PCreatedBy.Value = obj_obj_PayVoc.LoginID.Equals("") ? 0 : obj_obj_PayVoc.LoginID;
                PCreatedDate.Value = obj_obj_PayVoc.LoginDate;

                SqlParameter[] ParamArray = new SqlParameter[] { PAction,PVoucherId, PVoucherType,PVoucherTypeId, PVoucherDate,PVoucherDebit,PVoucherCredit,
                    PVoucherAmount,PVoucherNarration,PCrnote,PInvoiceCode,PIsRefrence,PCreatedBy, PCreatedDate };
                Open(CONNECTION_STRING);
                BeginTransaction();
                InsertRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, PaymentVoucher.PRO_PaymentVoucher, ParamArray);

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

        public int DeletePaymentVoucher(ref  PaymentVoucher obj_PayVoc, out string strError)
        {
            int DeleteRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter PAction = new SqlParameter(PaymentVoucher._Action, SqlDbType.BigInt);
                SqlParameter PVoucherID = new SqlParameter(PaymentVoucher._VoucherId, SqlDbType.BigInt);
                SqlParameter PDeletedBy = new SqlParameter(PaymentVoucher._LoginID, SqlDbType.BigInt);
                SqlParameter PDeletedDate = new SqlParameter(PaymentVoucher._LoginDate, SqlDbType.DateTime);

                PAction.Value = 4;
                PVoucherID.Value = obj_PayVoc.VoucherId;
                PDeletedBy.Value = obj_PayVoc.LoginID.Equals("") ? 0 : obj_PayVoc.LoginID;
                PDeletedDate.Value = obj_PayVoc.LoginDate;

                SqlParameter[] ParamArray = new SqlParameter[] { PAction, PVoucherID, PDeletedBy, PDeletedDate };
                Open(CONNECTION_STRING);
                BeginTransaction();
                DeleteRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, PaymentVoucher.PRO_PaymentVoucher, ParamArray);

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

        public DataSet FillComboForGroup()
        {
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PaymentVoucher._Action, SqlDbType.BigInt);
                pAction.Value = 1;

                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, PaymentVoucher.PRO_PaymentVoucher, pAction);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return DS;
        }

        public DataSet GetVoucherNo(out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                pAction.Value = 6;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, PaymentVoucher.PRO_PaymentVoucher, pAction);
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
        public DataSet GetoutStanding(int SuppId,out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter PSuppId = new SqlParameter("@LedgerID", SqlDbType.BigInt);
                pAction.Value = 8;
                PSuppId.Value = SuppId;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PaymentVoucher.PRO_PaymentVoucher, pAction,PSuppId);
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

        public DataSet GetCreditForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter PAction = new SqlParameter(PaymentVoucher._Action, SqlDbType.BigInt);
                SqlParameter PVoucherId = new SqlParameter(PaymentVoucher._VoucherId, SqlDbType.BigInt);

                PAction.Value = 5;
                PVoucherId.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PaymentVoucher.PRO_PaymentVoucher, PAction, PVoucherId);
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
                SqlParameter MAction = new SqlParameter(PaymentVoucher._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(PaymentVoucher._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 7;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, PaymentVoucher.PRO_PaymentVoucher, oParmCol);

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

        public string[] GetSuggestedRecordForChequePrinting(string prefixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;
            try
            {

                // -- For Checking OF Execution of Procedure=========
                SqlParameter MAction = new SqlParameter(PaymentVoucher._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(PaymentVoucher._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 11;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, PaymentVoucher.PRO_PaymentVoucher, oParmCol);

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

        public DataSet GetCreditNote(string RepCondition, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(PaymentVoucher._Action, SqlDbType.BigInt);
                SqlParameter MRepCondition = new SqlParameter(PaymentVoucher._RepCondition, SqlDbType.NVarChar);

                MAction.Value = 7;
                MRepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PaymentVoucher.PRO_PaymentVoucher, MAction, MRepCondition);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet GetPaymentVoucher(int VoucherId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter(PaymentVoucher._Action, SqlDbType.BigInt);
                SqlParameter MRepCondition = new SqlParameter(PaymentVoucher._VoucherId, SqlDbType.NVarChar);

                MAction.Value = 1;
                MRepCondition.Value = VoucherId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, "PRO_PRINT_PaymentVoucher", MAction, MRepCondition);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public int InsertVoucherDetail(ref PaymentVoucher obj_PayVoc, out string strError)
        {
            int InsertRow = 0;
            strError = string.Empty;
            try
            {
                SqlParameter PAction = new SqlParameter(PaymentVoucher._Action, SqlDbType.BigInt);
                SqlParameter PVoucherId = new SqlParameter(PaymentVoucher._VoucherId, SqlDbType.BigInt);
                SqlParameter PVoucherAmount = new SqlParameter(PaymentVoucher._VoucherAmount, SqlDbType.Decimal);
                SqlParameter PInvoiceCode = new SqlParameter(PaymentVoucher._InvoiceCode, SqlDbType.BigInt);
                SqlParameter pOutstanding = new SqlParameter(PaymentVoucher._OutStanding, SqlDbType.Decimal);
                SqlParameter pReceivedAmt = new SqlParameter(PaymentVoucher._ReceivedAmt, SqlDbType.Decimal);

                PAction.Value = 9;
                PVoucherId.Value = obj_PayVoc.VoucherId;
                PVoucherAmount.Value = obj_PayVoc.VoucherAmount;
                PInvoiceCode.Value = obj_PayVoc.InvoiceCode;
                pOutstanding.Value = obj_PayVoc.OutStanding;
                pReceivedAmt.Value = obj_PayVoc.ReceivedAmt;

                SqlParameter[] ParamArray = new SqlParameter[] { PAction,PVoucherId, 
                    PVoucherAmount,PInvoiceCode,pOutstanding,pReceivedAmt};
                Open(CONNECTION_STRING);
                BeginTransaction();
                InsertRow = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, PaymentVoucher.PRO_PaymentVoucher, ParamArray);

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
        #endregion

        public DMPaymentVoucher()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}