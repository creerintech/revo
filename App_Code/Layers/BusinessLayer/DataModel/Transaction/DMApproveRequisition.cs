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
    public class DMApproveRequisition : Utility.Setting
    {
        public DMApproveRequisition()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int UpdateReq_Status_Approve(ref ApproveRequisition Entity_ApproveRequisition, out string strError)
        {
             int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ApproveRequisition._Action, SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter(ApproveRequisition._RequisitionCafeId, SqlDbType.BigInt);
                SqlParameter pReqStatus = new SqlParameter(ApproveRequisition._ReqStatus, SqlDbType.NVarChar);
                SqlParameter pApprovedTime = new SqlParameter(ApproveRequisition._ApprovedTime, SqlDbType.DateTime);
                SqlParameter pUpdatedBy = new SqlParameter(ApproveRequisition._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(ApproveRequisition._LoginDate, SqlDbType.DateTime);

                pAction.Value=3;
                pRequisitionCafeId.Value = Entity_ApproveRequisition.RequisitionCafeId;
                pReqStatus.Value = Entity_ApproveRequisition.ReqStatus;
                pApprovedTime.Value = Entity_ApproveRequisition.ApprovedTime;
                pUpdatedBy.Value = Entity_ApproveRequisition.UserId;
                pUpdatedDate.Value = Entity_ApproveRequisition.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pReqStatus, pApprovedTime, pRequisitionCafeId, pUpdatedBy, pUpdatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ApproveRequisition.SP_ApproveRequisition, Param);

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

        public int UpdateReq_Status_Authorozed(ref ApproveRequisition Entity_ApproveRequisition, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(ApproveRequisition._Action, SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter(ApproveRequisition._RequisitionCafeId, SqlDbType.BigInt);
                SqlParameter pReqStatus = new SqlParameter(ApproveRequisition._ReqStatus, SqlDbType.NVarChar);
                SqlParameter pAuthorizedTime = new SqlParameter(ApproveRequisition._AuthorizedTime, SqlDbType.DateTime);
                SqlParameter pUpdatedBy = new SqlParameter(ApproveRequisition._UserId, SqlDbType.BigInt);
                SqlParameter pUpdatedDate = new SqlParameter(ApproveRequisition._LoginDate, SqlDbType.DateTime);

                pAction.Value = 4;
                pRequisitionCafeId.Value = Entity_ApproveRequisition.RequisitionCafeId;
                pReqStatus.Value = Entity_ApproveRequisition.ReqStatus;
                pAuthorizedTime.Value = Entity_ApproveRequisition.AuthorizedTime;
                pUpdatedBy.Value = Entity_ApproveRequisition.UserId;
                pUpdatedDate.Value = Entity_ApproveRequisition.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pReqStatus, pAuthorizedTime, pRequisitionCafeId, pUpdatedBy, pUpdatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ApproveRequisition.SP_ApproveRequisition, Param);

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

        public DataSet GetAllRequisition(string RepCondition, string RepCondition1, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ApproveRequisition._Action, SqlDbType.BigInt);
                SqlParameter pRepCond = new SqlParameter(ApproveRequisition._RptCond,SqlDbType.NVarChar);
                SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);
                pAction.Value = 1;
                pRepCond.Value = RepCondition;
                pCOND.Value = RepCondition1;
                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] { pAction, pRepCond, pCOND };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, ApproveRequisition.SP_ApproveRequisition, param);
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
                SqlParameter MAction = new SqlParameter(ApproveRequisition._Action, SqlDbType.BigInt);
                SqlParameter MRepCondition = new SqlParameter(ApproveRequisition._RptCond, SqlDbType.NVarChar);

                MAction.Value = 1;
                MRepCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, ApproveRequisition.SP_ApproveRequisition, oParmCol);

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

        public int UpdateRecord(int REQID, int ITEMID, int USERID, string REMARK,string RemarkAuthorise, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pREQID = new SqlParameter("@RequisitionCafeId", SqlDbType.BigInt);
                SqlParameter pITEMID = new SqlParameter("@ItemID", SqlDbType.BigInt);
                SqlParameter pUSERID = new SqlParameter("@UserId", SqlDbType.BigInt);
                SqlParameter pREMARK = new SqlParameter("@TEMP", SqlDbType.NVarChar);
                SqlParameter pRemarkFromAuthorise= new SqlParameter("@RemarkFromAuthorise", SqlDbType.NVarChar);

                pAction.Value = 6;
                pREQID.Value = REQID;
                pITEMID.Value = ITEMID;
                pUSERID.Value = USERID;
                pREMARK.Value = REMARK;
                pRemarkFromAuthorise.Value = RemarkAuthorise;

                SqlParameter[] Param = new SqlParameter[] { pAction, pREQID,pITEMID, pUSERID,pREMARK,pRemarkFromAuthorise };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, ApproveRequisition.SP_ApproveRequisition, Param);

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


        public DataSet GetRequisitionDtls(Int32 RequisitionCafeId, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(ApproveRequisition._Action, SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter(ApproveRequisition._RequisitionCafeId, SqlDbType.BigInt);

                pAction.Value = 2;
                pRequisitionCafeId.Value = RequisitionCafeId;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ApproveRequisition.SP_ApproveRequisition, pAction, pRequisitionCafeId);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }


    }
}