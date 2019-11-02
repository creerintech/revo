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
/// Summary description for DMIndentEmail
/// </summary>
/// 
namespace MayurInventory.DataModel
{
public class DMIndentEmail : Utility.Setting
{

    public DataSet GetAllRequisition(string RepCondition, string RepCondition1, out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter pAction = new SqlParameter(IndentEmail._Action, SqlDbType.BigInt);
            //SqlParameter pRepCond = new SqlParameter(ApproveRequisition._RptCond, SqlDbType.NVarChar);
            //SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);
            pAction.Value = 1;
            //pRepCond.Value = RepCondition;
            //pCOND.Value = RepCondition1;
            Open(CONNECTION_STRING);
            SqlParameter[] param = new SqlParameter[] { pAction };
            Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure,IndentEmail.SP_EmailIndent , param);
        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;
    }

    public DataSet GetRequisition(string RepCondition, String LOCID, out string StrError ,int LId  )
    {
        StrError = string.Empty;
        DataSet DS = new DataSet();
        try
        {
            SqlParameter pAction = new SqlParameter(IndentEmail._Action, SqlDbType.BigInt);
            SqlParameter pRepCondition = new SqlParameter(IndentEmail._StrCondition, SqlDbType.NVarChar);
            SqlParameter pLOCID = new SqlParameter("@Remark", SqlDbType.NVarChar);

            pAction.Value = 2;
            pRepCondition.Value = RepCondition;
            pLOCID.Value = LId;

            SqlParameter[] param = new SqlParameter[] { pAction, pRepCondition, pLOCID };
            Open(CONNECTION_STRING);
            DS = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, IndentEmail.SP_EmailIndent, param);

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

    public DataSet FillCombo(int LOCID, out string StrError)
    {
        StrError = string.Empty;
        DataSet DS = new DataSet();
        try
        {
            SqlParameter pAction = new SqlParameter(IndentEmail._Action, SqlDbType.BigInt);
            SqlParameter pLOCID = new SqlParameter("@LOCID", SqlDbType.BigInt);
            pAction.Value = 1;
            pLOCID.Value = LOCID;
            Open(CONNECTION_STRING);
            DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, IndentEmail.SP_EmailIndent, pAction, pLOCID);
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

    public DataSet BindComboBox(string Cond, out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter pAction = new SqlParameter(PurchaseOrder._Action, SqlDbType.BigInt);
            SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);

            pAction.Value = 1;
            pCOND.Value = Cond;

            Open(CONNECTION_STRING);
            Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, PurchaseOrder.SP_PurchaseOrder_Part_I, pAction, pCOND);
        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;
    }

    public DataSet GetRateCompare( Decimal NewRate, int REQID, out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter pAction = new SqlParameter(IndentEmail._Action, SqlDbType.BigInt);
            SqlParameter pREQID = new SqlParameter(IndentEmail._RequisitionCafeId, SqlDbType.BigInt);
            SqlParameter pRate = new SqlParameter(IndentEmail._Rate, SqlDbType.Decimal);

            pAction.Value = 3;
            pREQID.Value = REQID;
            pRate.Value = NewRate;

            SqlParameter[] param = new SqlParameter[] { pAction, pRate, pREQID };
            Open(CONNECTION_STRING);
            Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, IndentEmail.SP_EmailIndent, param);

        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;

    }


	public DMIndentEmail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
}
