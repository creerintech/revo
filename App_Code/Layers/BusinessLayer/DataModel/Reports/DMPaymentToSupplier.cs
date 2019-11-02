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
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using MayurInventory.DALSQLHelper;
using System.Collections.Generic;

/// <summary>
/// Summary description for DMPaymentToSupplier
/// </summary>
namespace MayurInventory.DataModel
{
    public class DMPaymentToSupplier : Utility.Setting
{
 
    #region[BusinessLogic]
    public DataSet FillGrid(string Cond,out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
            SqlParameter MRepCondition = new SqlParameter("@Temp", SqlDbType.NVarChar);
            pAction.Value = 1;
            MRepCondition.Value = Cond;

            Open(CONNECTION_STRING);
            Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_PaymentToSupplier", pAction,MRepCondition);
        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;
    }

    public DataSet FillGridFromSearch(string Cond, out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
            SqlParameter MRepCondition = new SqlParameter("@Temp", SqlDbType.NVarChar);
            pAction.Value = 3;
            MRepCondition.Value = Cond;

            Open(CONNECTION_STRING);
            Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_PaymentToSupplier", pAction,MRepCondition);
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
            SqlParameter MAction = new SqlParameter("@Action", SqlDbType.BigInt);
            SqlParameter MRepCondition = new SqlParameter("@Temp", SqlDbType.NVarChar);

            MAction.Value = 2;
            MRepCondition.Value = prefixText;

            SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
            Open(Setting.CONNECTION_STRING);

            SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, "SP_PaymentToSupplier", oParmCol);

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


    public int Insert_Template(int POID, int SupplierID, int PaymentTermId, string ChequeNo, string PersonName, string Remark, decimal Chequeamount,decimal PaymentAmount,decimal PendingAmount, out string strError)
    {
        int iInsert = 0;
        strError = string.Empty;
        try
        {
            SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
            SqlParameter pPOID = new SqlParameter("@POID", SqlDbType.BigInt);
            SqlParameter pSupplierID = new SqlParameter("@SupplierID", SqlDbType.BigInt);
            SqlParameter pPaymentTermId = new SqlParameter("@PaymentTermsID", SqlDbType.BigInt);
            SqlParameter pChequeNo = new SqlParameter("@ChequeNo", SqlDbType.NVarChar);
            SqlParameter pPersonName = new SqlParameter("@PersonName", SqlDbType.NVarChar);
            SqlParameter pRemark = new SqlParameter("@Remark", SqlDbType.NVarChar);
            SqlParameter pChequeamount = new SqlParameter("@Chequeamount", SqlDbType.Decimal);
            SqlParameter pPaymentAmount = new SqlParameter("@PaymentAmount", SqlDbType.Decimal);
            SqlParameter pPendingAmount = new SqlParameter("@PendingAmount", SqlDbType.Decimal);


            pAction.Value = 2;
            pPOID.Value = POID;
            pSupplierID.Value = SupplierID;
            pPaymentTermId.Value = PaymentTermId;
            pChequeNo.Value = ChequeNo;
            pPersonName.Value = PersonName;
            pRemark.Value = Remark;
            pChequeamount.Value = Chequeamount;
            pPaymentAmount.Value = PaymentAmount;
            pPendingAmount.Value = PendingAmount;

            SqlParameter[] Param = new SqlParameter[] { pAction, pPOID, pSupplierID, pPaymentTermId, 
                pChequeNo,pPersonName,pRemark,pChequeamount,pPaymentAmount,pPendingAmount};

            Open(CONNECTION_STRING);
            BeginTransaction();

            iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, "SP_PaymentToSupplier", Param);

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
   
    #endregion
    public DMPaymentToSupplier()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
}
