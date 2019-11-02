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

/// <summary>
/// Summary description for DMArticleHistoryReport
/// </summary>
namespace MayurInventory.DataModel
{
   public class DMArticleHistoryReport : Utility.Setting
{
 
    #region[BusinessLogic]
    public DataSet FillCombo(out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
            pAction.Value = 7;
            Open(CONNECTION_STRING);
            Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, pAction);
        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;
    }
    public DataSet FillReportCombo(out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);

            pAction.Value = 1;

            Open(CONNECTION_STRING);
            Ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_ArticleHistoryReport", pAction);

        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;
    }
    public DataSet GetInwardDetailsReport(string RepCondition, out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter MAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
            SqlParameter MRepCondition = new SqlParameter(MaterialInwardReg._strCond, SqlDbType.NVarChar);
            MAction.Value = 3;
            MRepCondition.Value = RepCondition;

            Open(Setting.CONNECTION_STRING);
            Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_ArticleHistoryReport", MAction, MRepCondition);

        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;

    }
    public DataSet GetOutwardDetailsReport(string RepCondition, out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter MAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
            SqlParameter MRepCondition = new SqlParameter(MaterialInwardReg._strCond, SqlDbType.NVarChar);
            MAction.Value = 6;
            MRepCondition.Value = RepCondition;

            Open(Setting.CONNECTION_STRING);
            Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_ArticleHistoryReport", MAction, MRepCondition);

        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;

    }
    public DataSet Fill_Items(int CategoryId, out string strError)
    {
        strError = string.Empty;
        DataSet Ds = new DataSet();
        try
        {
            SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
            SqlParameter pCategoryId = new SqlParameter("@CategoryId", SqlDbType.BigInt);

            pAction.Value = 5;
            pCategoryId.Value = CategoryId;

            Open(CONNECTION_STRING);
            Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure,"SP_ArticleHistoryReport", pAction, pCategoryId);

        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally { Close(); }
        return Ds;
    }
    public DataSet GetItemName(int catID, out string strError)
    {
        DataSet ds = new DataSet();
        strError = string.Empty;
        try
        {
            //SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
            ////SqlParameter pCategoryId = new SqlParameter(MaterialInwardReg._CategoryID, SqlDbType.BigInt);

            //pAction.Value = 2;
            //pCategoryId.Value = 1;//catID;
            //Open(CONNECTION_STRING);
            //ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, MaterialInwardReg.SP_MaterialInwardReg, pAction, pCategoryId);
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
    public DMArticleHistoryReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
}
