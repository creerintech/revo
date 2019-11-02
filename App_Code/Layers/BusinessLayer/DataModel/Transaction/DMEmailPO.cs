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

/// <summary>
/// Summary description for DMEmailPO
/// </summary>
/// 

namespace MayurInventory.DataModel
{
    public class DMEmailPO : Utility.Setting
    {
        public DataSet GetPurchase_Order(string RepCondition, out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter pAction = new SqlParameter(EmailPO._Action, SqlDbType.BigInt);
            SqlParameter pRepCondition = new SqlParameter("@RptCond", SqlDbType.NVarChar);

            pAction.Value = 1;
            pRepCondition.Value = RepCondition;

            Open(CONNECTION_STRING);
            Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EmailPO.SP_EmailPurchaseOrder, pAction, pRepCondition);

        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;

    }

        public DataSet GetPOList(string RepCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
                SqlParameter PstrCond = new SqlParameter(PurchaseOrder._strCond, SqlDbType.NVarChar);
                pAction.Value = 2;
                PstrCond.Value = RepCondition;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure,EmailPO.SP_EmailPurchaseOrder , pAction, PstrCond);
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


        public int UpdateApproveEmailStatusNew(int poID, long UserId, out string StrError)
        {
            int UpdateEStatus = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pREQID = new SqlParameter("@POId", SqlDbType.BigInt);
                SqlParameter pUserID = new SqlParameter("@UserId", SqlDbType.BigInt);

                pAction.Value = 4;
                pREQID.Value = poID;
                pUserID.Value = UserId;

                SqlParameter[] param = new SqlParameter[] { pAction, pREQID, pUserID };

                Open(CONNECTION_STRING);
                BeginTransaction();

                UpdateEStatus = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, "SP_EmailPurchaseOrder", param);

                if (UpdateEStatus > 0)
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
            return UpdateEStatus;
        }
        public int UpdateEmailStatusNew(int poid, long userID, out string StrError)
        {
            int UpdateEStatus = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pREQID = new SqlParameter("@POId", SqlDbType.BigInt);
                SqlParameter pUserID = new SqlParameter("@UserId", SqlDbType.BigInt);
               
                pAction.Value = 3;
                pREQID.Value = poid;
                pUserID.Value = userID;
                SqlParameter[] param = new SqlParameter[] { pAction, pREQID, pUserID };

                Open(CONNECTION_STRING);
                BeginTransaction();

                UpdateEStatus = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, "SP_EmailPurchaseOrder", param);

                if (UpdateEStatus > 0)
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
            return UpdateEStatus;
        }
	    public DMEmailPO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    }
}