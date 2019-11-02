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
/// Summary description for DMTemplate
/// </summary>
namespace MayurInventory.DataModel
{
    public class DMTemplate:Utility.Setting
    {
        #region [Bussiness Logic]
        public DataSet GetCategoryName(out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                pAction.Value = 1;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, pAction);
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

        public DataSet GetDuplicateName(string Name,out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pName = new SqlParameter("@TemplateName", SqlDbType.NVarChar);
                pAction.Value = 10;
                pName.Value = Name;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, pAction,pName);
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

        public DataSet GetItemName(int catID,out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCategoryId = new SqlParameter(Template._CategoryID,SqlDbType.BigInt);
                pAction.Value = 2;
                pCategoryId.Value = catID;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, pAction,pCategoryId);
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
        public DataSet GetItemNameonSubCategory(int catID,int SCatID, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCategoryId = new SqlParameter(Template._CategoryID, SqlDbType.BigInt);
                SqlParameter pSubCategoryID = new SqlParameter("@SubCategoryID", SqlDbType.BigInt);
                pAction.Value = 14;
                pCategoryId.Value = catID;
                pSubCategoryID.Value = SCatID;
                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] { pAction, pCategoryId, pSubCategoryID };
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, param);
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
        public DataSet GetGridTempalate(string RepCondition,out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCondition = new SqlParameter(Template._StrCondition,SqlDbType.NVarChar);
                pAction.Value = 4;
                pCondition.Value = RepCondition;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, pAction,pCondition);
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

        public DataSet BindItemsToGrid(int CateID,string All, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCategoryID = new SqlParameter(Template._CategoryID, SqlDbType.BigInt);
                SqlParameter pStrCondition = new SqlParameter(Template._StrCondition, SqlDbType.NVarChar);
                pAction.Value = 5;
                pCategoryID.Value = CateID;
                pStrCondition.Value = All;
                SqlParameter[] param = new SqlParameter[] { pAction, pCategoryID, pStrCondition};
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template,param);
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
        public DataSet BindSUBItemsToGrid(int CateID, int SubCateID, string All, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pCategoryID = new SqlParameter(Template._CategoryID, SqlDbType.BigInt);
                SqlParameter pSubCategoryID = new SqlParameter("@SubCategoryID", SqlDbType.BigInt);
                SqlParameter pStrCondition = new SqlParameter(Template._StrCondition, SqlDbType.NVarChar);
                pAction.Value = 5;
                pCategoryID.Value = CateID;
                pSubCategoryID.Value = SubCateID;
                pStrCondition.Value = All;
                SqlParameter[] param = new SqlParameter[] { pAction, pCategoryID, pStrCondition,pSubCategoryID };
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, param);
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

        public DataSet BindToVendorGrid(int ItemID,int ItemDtlsID, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pItemID = new SqlParameter(Template._ItemID, SqlDbType.BigInt);
                SqlParameter pItemDtlsID = new SqlParameter(Template._CategoryID, SqlDbType.BigInt);
                pAction.Value = 3;
                pItemID.Value = ItemID;
                pItemDtlsID.Value = ItemDtlsID;
                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] { pAction,pItemID,pItemDtlsID};
                ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template,param);
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

        public DataSet BindForEditGridTemplate(int TemplateID, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pTemplateID = new SqlParameter(Template._TemplateID, SqlDbType.BigInt);
                pAction.Value = 8;
                pTemplateID.Value = TemplateID;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, pAction, pTemplateID);
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

        public DataSet BindForReport(int TemplateID, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pTemplateID = new SqlParameter(Template._TemplateID, SqlDbType.BigInt);
                pAction.Value = 12;
                pTemplateID.Value = TemplateID;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, pAction, pTemplateID);
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

        public int Insert_Template(ref Template Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(Template._Action, SqlDbType.BigInt);
                SqlParameter pTemplateName = new SqlParameter(Template._TemplateName, SqlDbType.NVarChar);
                SqlParameter pTemplateDate = new SqlParameter(Template._TemplateDate, SqlDbType.DateTime);
                SqlParameter pCreatedBy = new SqlParameter(Template._LoginID, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(Template._LoginDate, SqlDbType.DateTime);

                pAction.Value = 6;
                pTemplateName.Value = Entity_Call.TemplateName;
                pTemplateDate.Value = Entity_Call.TemplateDate;
                pCreatedBy.Value = Entity_Call.LoginID;
                pCreatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pTemplateName, pTemplateDate, pCreatedBy, pCreatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, Param);

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

        public int Update_Template(ref Template Entity_Call, out string strError)
        {
            int iUpdate = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(Template._Action, SqlDbType.BigInt);
                SqlParameter pTemplateID = new SqlParameter(Template._TemplateID, SqlDbType.BigInt);
                SqlParameter pTemplateName = new SqlParameter(Template._TemplateName, SqlDbType.NVarChar);
                SqlParameter pTemplateDate = new SqlParameter(Template._TemplateDate, SqlDbType.DateTime);
                SqlParameter pCreatedBy = new SqlParameter(Template._LoginID, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(Template._LoginDate, SqlDbType.DateTime);

                pAction.Value = 9;
                pTemplateID.Value = Entity_Call.TemplateID;
                pTemplateName.Value = Entity_Call.TemplateName;
                pTemplateDate.Value = Entity_Call.TemplateDate;
                pCreatedBy.Value = Entity_Call.LoginID;
                pCreatedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction,pTemplateID,pTemplateName,pTemplateDate, pCreatedBy, pCreatedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iUpdate = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, Param);

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
                strError = ex.Message;
            }
            finally
            {
                Close();
            }
            return iUpdate;
        }

        public int Insert_TemplateDetails(ref Template Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(Template._Action, SqlDbType.BigInt);
                SqlParameter pTemplateID = new SqlParameter(Template._TemplateID, SqlDbType.BigInt);
                SqlParameter pItemID = new SqlParameter(Template._ItemID, SqlDbType.BigInt);
                SqlParameter pItemDtlsID = new SqlParameter(Template._ItemDtlsID, SqlDbType.BigInt);
                SqlParameter pVendorID = new SqlParameter(Template._VendorID, SqlDbType.BigInt);
                SqlParameter pRate = new SqlParameter(Template._Rate, SqlDbType.Decimal);
                SqlParameter pQTY = new SqlParameter(Template._QTY, SqlDbType.Decimal);
                SqlParameter pStrCondition = new SqlParameter(Template._StrCondition, SqlDbType.NVarChar);
                pAction.Value = 7;
                pTemplateID.Value = Entity_Call.TemplateID;
                pItemID.Value = Entity_Call.ItemID;
                pItemDtlsID.Value = Entity_Call.ItemDtlsID;
                pVendorID.Value = Entity_Call.VendorID;
                pRate.Value = Entity_Call.Rate;
                pQTY.Value = Entity_Call.QTY;
                pStrCondition.Value = Entity_Call.StrCondition;
                SqlParameter[] Param = new SqlParameter[] { pAction, pTemplateID, pItemID, pVendorID, pRate, pItemDtlsID, pQTY, pStrCondition };
                Open(CONNECTION_STRING);
                BeginTransaction();
                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, Param);
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


        public int Update_TemplateDetails(ref Template Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(Template._Action, SqlDbType.BigInt);
                SqlParameter pTemplateDtlsID = new SqlParameter(Template._TemplateDtlsID, SqlDbType.BigInt);
                SqlParameter pTemplateID = new SqlParameter(Template._TemplateID, SqlDbType.BigInt);
                SqlParameter pItemID = new SqlParameter(Template._ItemID, SqlDbType.BigInt);
                SqlParameter pVendorID = new SqlParameter(Template._VendorID, SqlDbType.BigInt);
                SqlParameter pRate = new SqlParameter(Template._Rate, SqlDbType.BigInt);
                pAction.Value = 1;
                pTemplateDtlsID.Value = Entity_Call.TemplateDtlsID;
                pTemplateID.Value = Entity_Call.TemplateID;
                pItemID.Value = Entity_Call.ItemID;
                pVendorID.Value = Entity_Call.VendorID;
                pRate.Value = Entity_Call.Rate;
                SqlParameter[] Param = new SqlParameter[] { pAction, pTemplateDtlsID, pTemplateID, pItemID, pVendorID, pRate };
                Open(CONNECTION_STRING);
                BeginTransaction();
                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, Param);
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

        public int Delete_Template(ref Template Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(Template._Action, SqlDbType.BigInt);
                SqlParameter pTemplateID = new SqlParameter(Template._TemplateID, SqlDbType.BigInt);
                SqlParameter pLoginID = new SqlParameter(Template._LoginID, SqlDbType.BigInt);
                SqlParameter pLoginDate = new SqlParameter(Template._LoginDate, SqlDbType.DateTime);

                pAction.Value = 13;
                pTemplateID.Value = Entity_Call.TemplateID;
                pLoginID.Value = Entity_Call.LoginID;
                pLoginDate.Value = Entity_Call.LoginDate;
                SqlParameter[] Param = new SqlParameter[] { pAction, pTemplateID, pLoginID, pLoginDate };
                Open(CONNECTION_STRING);
                BeginTransaction();
                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, Param);
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

        public string[] GetSuggestedRecord(string prefixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;
            try
            {
                // -- For Checking OF Execution of Procedure=========
                SqlParameter MAction = new SqlParameter(Template._Action, SqlDbType.VarChar);
                SqlParameter MRepCondition = new SqlParameter(Template._StrCondition, SqlDbType.NVarChar);
                MAction.Value = 11;
                MRepCondition.Value = prefixText;
                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MRepCondition };
                Open(Setting.CONNECTION_STRING);
                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, oParmCol);
                if (dr != null && dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        ListItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString(), dr[0].ToString());
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

        public DataSet GetTemplateList(string RepCondition, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(Template._Action, SqlDbType.BigInt);
                SqlParameter PstrCond = new SqlParameter(Template._StrCondition, SqlDbType.NVarChar);
                pAction.Value = 4;
                PstrCond.Value = RepCondition;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Template.SP_Template, pAction, PstrCond);
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
        #endregion
        public DMTemplate()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}