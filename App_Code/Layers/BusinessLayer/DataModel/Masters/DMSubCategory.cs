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

   public class DMSubCategory:Utility.Setting
   {
       public int InsertRecord(ref SubCategory Entity_Call, out string StrError)
       {
           int iInsert = 0;
           StrError = string.Empty;

           try
           {
               SqlParameter pAction = new SqlParameter(SubCategory._Action,SqlDbType.NVarChar);
               SqlParameter pSubCategory = new SqlParameter(SubCategory._SubCategory,SqlDbType.NVarChar);
               SqlParameter pCategoryId = new SqlParameter(SubCategory._CategoryId, SqlDbType.BigInt);
               SqlParameter pCreateBy = new SqlParameter(SubCategory._UserId,SqlDbType.BigInt);
               SqlParameter PCreatedDate = new SqlParameter(SubCategory._LoginDate,SqlDbType.DateTime);
               SqlParameter PIsDeleted = new SqlParameter(SubCategory._IsDeleted,SqlDbType.Bit);
               SqlParameter pRemark = new SqlParameter(SubCategory._Remark, SqlDbType.NVarChar);

               pAction.Value = 1;
               pSubCategory.Value = Entity_Call.SubCategoryName;
               pCategoryId.Value = Entity_Call.CategoryId;
               pCreateBy.Value = Entity_Call.UserId;
               PCreatedDate.Value = Entity_Call.LoginDate;
               PIsDeleted.Value = Entity_Call.IsDeleted;
               pRemark.Value = Entity_Call.Remark;

               SqlParameter[] param = new SqlParameter[] { pAction, pSubCategory, pCreateBy, PCreatedDate, PIsDeleted, pCategoryId ,pRemark};

               Open(CONNECTION_STRING);
               BeginTransaction();

               iInsert = SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,SubCategory.SP_SubCategory,param);

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

           finally
           {
               Close();
           }
           return iInsert;
       }

       public int UpdateRecord(ref SubCategory Entity_Call, out string StrError)
       {
           int iInsert = 0;
           StrError = string.Empty;
           try
           {
               SqlParameter pAction = new SqlParameter(SubCategory._Action,SqlDbType.BigInt);
               SqlParameter pSubCategoryId = new SqlParameter(SubCategory._SubCategoryId,SqlDbType.BigInt);
               SqlParameter pCategoryId = new SqlParameter(SubCategory._CategoryId, SqlDbType.BigInt);
               SqlParameter pSubCategory = new SqlParameter(SubCategory._SubCategory,SqlDbType.NVarChar);
               SqlParameter pUpdatedBy = new SqlParameter(SubCategory._UserId,SqlDbType.BigInt);
               SqlParameter pUpdatedDate = new SqlParameter(SubCategory._LoginDate,SqlDbType.DateTime);
               SqlParameter pRemark = new SqlParameter(SubCategory._Remark, SqlDbType.NVarChar);
               pAction.Value = 2;
               pSubCategoryId.Value = Entity_Call.SubCategoryId;
               pCategoryId.Value = Entity_Call.CategoryId;
               pSubCategory.Value = Entity_Call.SubCategoryName;
               pUpdatedBy.Value = Entity_Call.UserId;
               pUpdatedDate.Value = Entity_Call.LoginDate;
               pRemark.Value = Entity_Call.Remark;
    

               SqlParameter[] param = new SqlParameter[] {pAction,pSubCategoryId,pSubCategory,pUpdatedBy,pUpdatedDate,pCategoryId,pRemark };

               Open(CONNECTION_STRING);
               BeginTransaction();

               iInsert = SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,SubCategory.SP_SubCategory,param);
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

       public int DeleteRecord(ref SubCategory Entity_Call, out string StrError)
       {
           int iDelete = 0;
           StrError = string.Empty;
           try
           {
               SqlParameter pAction = new SqlParameter(SubCategory._Action,SqlDbType.BigInt);
               SqlParameter pSubCategoryId = new SqlParameter(SubCategory._SubCategoryId,SqlDbType.BigInt);
               SqlParameter pDeletedBy = new SqlParameter(SubCategory._UserId,SqlDbType.BigInt);
               SqlParameter pDeletedDate = new SqlParameter(SubCategory._LoginDate,SqlDbType.DateTime);
               SqlParameter pIsDeleted = new SqlParameter(SubCategory._IsDeleted,SqlDbType.Bit);


               @pAction.Value = 3;
               @pSubCategoryId.Value = Entity_Call.SubCategoryId;
               @pDeletedBy.Value = Entity_Call.UserId;
               @pDeletedDate.Value = Entity_Call.LoginDate;
               @pIsDeleted.Value = Entity_Call.IsDeleted;

               SqlParameter[] param = new SqlParameter[]{@pAction,@pSubCategoryId,@pDeletedBy,@pDeletedDate,@pIsDeleted};

               Open(CONNECTION_STRING);
               BeginTransaction();

               iDelete = SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,SubCategory.SP_SubCategory,param);

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

       public DataSet GetSubCategoryForEdit(int ID, out string StrError)
       {
           StrError = string.Empty;
           DataSet DS = new DataSet();
           try
           {
               SqlParameter PAction = new SqlParameter(SubCategory._Action, SqlDbType.BigInt);
               SqlParameter PSubCategoryId = new SqlParameter(SubCategory._SubCategoryId, SqlDbType.BigInt);

               PAction.Value = 4;
               PSubCategoryId.Value = ID;

               Open(CONNECTION_STRING);
               DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, SubCategory.SP_SubCategory, PAction, PSubCategoryId);

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

       public DataSet GetSubCategory(string RepCondition,out string StrError)
       {
           StrError = string.Empty;
           DataSet DS = new DataSet();
           try
           {
               SqlParameter pAction = new SqlParameter(SubCategory._Action, SqlDbType.BigInt);
               SqlParameter PRepCondition = new SqlParameter(SubCategory._StrCondition, SqlDbType.NVarChar);

               pAction.Value = 5;
               PRepCondition.Value = RepCondition;

               Open(CONNECTION_STRING);
               DS    = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, SubCategory.SP_SubCategory, pAction, PRepCondition);

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

       public DataSet ChkDuplicate(string Name, out string strError)
       {
           strError = string.Empty;
           DataSet DS = new DataSet();
           try
           {
               SqlParameter pAction = new SqlParameter(SubCategory._Action, SqlDbType.BigInt);

               SqlParameter PRepCondition = new SqlParameter(SubCategory._StrCondition, SqlDbType.NVarChar);

               pAction.Value = 6;
               PRepCondition.Value = Name;
               Open(CONNECTION_STRING);
               DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, SubCategory.SP_SubCategory, pAction, PRepCondition);

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

       public string[] GetSuggestRecord(string prefixText)
       {
           List<string> SearchList = new List<string>();
           string ListItem = string.Empty;
           try
           {
               SqlParameter pAction = new SqlParameter(SubCategory._Action, SqlDbType.BigInt);
               SqlParameter pRepCondition = new SqlParameter(SubCategory._StrCondition, SqlDbType.NVarChar);

               pAction.Value = 5;
               pRepCondition.Value = prefixText;

               SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition };

               Open(CONNECTION_STRING);

               SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, SubCategory.SP_SubCategory, oparamcol);

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



       public DataSet GetCategoryDetail(int id, out string StrError)
       {
           StrError = string.Empty;
           DataSet DS = new DataSet();
           try
           {
               SqlParameter pAction = new SqlParameter(SubCategory._Action, SqlDbType.BigInt);
               SqlParameter PCategoryId = new SqlParameter(SubCategory._CategoryId, SqlDbType.NVarChar);

               pAction.Value = 7;
               PCategoryId.Value = id;

               Open(CONNECTION_STRING);
               DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, SubCategory.SP_SubCategory, pAction, PCategoryId);

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


}
}