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
using MayurInventory.DataModel;
using MayurInventory.EntityClass;
using MayurInventory.DALSQLHelper;
using MayurInventory.DB;
using System.Collections.Generic;

/// <summary>
/// Summary description for DMDeviation
/// </summary>
namespace MayurInventory.DataModel
{
    public class DMDeviation:Utility.Setting
    {
        #region [Form]--------------------------------------
        public DataSet FillCombo(string COND,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(Deviation._Action, SqlDbType.BigInt);
                SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);
                pAction.Value = 1;
                pCOND.Value = COND;
                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Deviation.SP_Deviation, pAction,pCOND);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }

        public DataSet ShowStockInHand(string FromDate, string ToDate, string StrCond, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(Deviation._Action, SqlDbType.BigInt);
                SqlParameter pstrCond = new SqlParameter(Deviation._StrCondition, SqlDbType.NVarChar);
                SqlParameter pFromDate = new SqlParameter("@FromDate", SqlDbType.NVarChar);
                SqlParameter pToDate = new SqlParameter("@ToDate", SqlDbType.NVarChar);

                pAction.Value = 2;
                pstrCond.Value = StrCond;
                pFromDate.Value = FromDate;
                pToDate.Value = ToDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pstrCond, pFromDate, pToDate };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, Deviation.SP_Deviation, Param);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public int Insert_DeviationMaster(ref Deviation Entity_Call,int LOCID, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(Deviation._Action, SqlDbType.BigInt);
                SqlParameter pDeviationNo = new SqlParameter(Deviation._DeviationNo, SqlDbType.NVarChar);
                SqlParameter pCreatedBy = new SqlParameter(Deviation._LoginID, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(Deviation._LoginDate, SqlDbType.DateTime);
                SqlParameter pDeviationFrom = new SqlParameter(Deviation._DeviationFrom, SqlDbType.DateTime);
                SqlParameter pDeviationTo = new SqlParameter(Deviation._DeviationTo, SqlDbType.DateTime);
                SqlParameter pSysAmt = new SqlParameter(Deviation._SysAmt, SqlDbType.Decimal);
                SqlParameter pPhyAmt = new SqlParameter(Deviation._PhyAmt, SqlDbType.Decimal);
                SqlParameter pLOCID = new SqlParameter("@LOCID", SqlDbType.BigInt);
                pAction.Value = 3;
                pDeviationNo.Value = Entity_Call.DeviationNo;
                pCreatedBy.Value = Entity_Call.LoginID;
                pCreatedDate.Value = Entity_Call.LoginDate;
                pDeviationFrom.Value = Entity_Call.DeviationFrom;
                pDeviationTo.Value = Entity_Call.DeviationTo;
                pSysAmt.Value = Entity_Call.SysAmt;
                pPhyAmt.Value = Entity_Call.PhyAmt;
                pLOCID.Value = LOCID;
                SqlParameter[] Param = new SqlParameter[] { pAction,pDeviationNo ,pCreatedBy, pCreatedDate,pDeviationFrom,pDeviationTo,pSysAmt,pPhyAmt,pLOCID};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteScalar(_Connection, _Transaction, CommandType.StoredProcedure, Deviation.SP_Deviation, Param);

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

        public int Insert_DeviationDetails(ref Deviation Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(Deviation._Action, SqlDbType.BigInt);
                SqlParameter pDeviationID = new SqlParameter(Deviation._DeviationID, SqlDbType.BigInt);
                SqlParameter pItemId = new SqlParameter(Deviation._ItemID, SqlDbType.BigInt);
                SqlParameter pUnitID = new SqlParameter(Deviation._UnitID, SqlDbType.BigInt);
                SqlParameter pRate = new SqlParameter(Deviation._Rate, SqlDbType.Decimal);
                SqlParameter pLocationID = new SqlParameter(Deviation._LocationID, SqlDbType.BigInt);
                SqlParameter pSysClosing = new SqlParameter(Deviation._SysClosing, SqlDbType.Decimal);
                SqlParameter pPhyClosing = new SqlParameter(Deviation._PhyClosing, SqlDbType.Decimal);
                SqlParameter pDeviation = new SqlParameter(Deviation._Deviation1, SqlDbType.Decimal);
                SqlParameter pPhyRate = new SqlParameter(Deviation._PhyRate, SqlDbType.Decimal);
                pAction.Value = 4;
                pDeviationID.Value = Entity_Call.DeviationID;
                pItemId.Value = Entity_Call.ItemID;
                pUnitID.Value = Entity_Call.UnitID;
                pRate.Value = Entity_Call.Rate;
                pLocationID.Value = Entity_Call.LocationID;
                pSysClosing.Value = Entity_Call.SysClosing;
                pPhyClosing.Value = Entity_Call.PhyClosing;
                pDeviation.Value = Entity_Call.Deviation1;
                pPhyRate.Value = Entity_Call.PhyRate;

                SqlParameter[] Param = new SqlParameter[] { pAction,pDeviationID,pItemId,pUnitID,pRate,pLocationID,
                                                            pSysClosing,pPhyClosing,pDeviation,pPhyRate};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, Deviation.SP_Deviation, Param);

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

        public int Update_DeviationMaster(ref Deviation Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(Deviation._Action, SqlDbType.BigInt);
                SqlParameter pDeviationNo = new SqlParameter(Deviation._DeviationNo, SqlDbType.NVarChar);
                SqlParameter pDeviationID = new SqlParameter(Deviation._DeviationID, SqlDbType.BigInt);
                SqlParameter pCreatedBy = new SqlParameter(Deviation._LoginID, SqlDbType.BigInt);
                SqlParameter pCreatedDate = new SqlParameter(Deviation._LoginDate, SqlDbType.DateTime);
                SqlParameter pSysAmt = new SqlParameter(Deviation._SysAmt, SqlDbType.Decimal);
                SqlParameter pPhyAmt = new SqlParameter(Deviation._PhyAmt, SqlDbType.Decimal);

                pAction.Value = 5;
                pDeviationNo.Value = Entity_Call.DeviationNo;
                pDeviationID.Value = Entity_Call.DeviationID;
                pCreatedBy.Value = Entity_Call.LoginID;
                pCreatedDate.Value = Entity_Call.LoginDate;
                pSysAmt.Value = Entity_Call.SysAmt;
                pPhyAmt.Value = Entity_Call.PhyAmt;

                SqlParameter[] Param = new SqlParameter[] { pAction, pDeviationNo,pDeviationID, pCreatedBy, pCreatedDate,pSysAmt,pPhyAmt };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, Deviation.SP_Deviation, Param);

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

        public int Delete_DeviationMaster(ref Deviation Entity_Call, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(Deviation._Action, SqlDbType.BigInt);
                SqlParameter pDeviationID = new SqlParameter(Deviation._DeviationID, SqlDbType.BigInt);
                SqlParameter pDeletedBy = new SqlParameter(Deviation._LoginID, SqlDbType.BigInt);
                SqlParameter pDeletedDate = new SqlParameter(Deviation._LoginDate, SqlDbType.DateTime);

                pAction.Value = 6;
                pDeviationID.Value = Entity_Call.DeviationID;
                pDeletedBy.Value = Entity_Call.LoginID;
                pDeletedDate.Value = Entity_Call.LoginDate;

                SqlParameter[] Param = new SqlParameter[] { pAction, pDeviationID,  pDeletedBy, pDeletedDate };

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, Deviation.SP_Deviation, Param);

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

        public DataSet GetReport(string RepCondition,string COND, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(Deviation._Action, SqlDbType.BigInt);
                SqlParameter pStrCondition = new SqlParameter(Deviation._StrCondition, SqlDbType.NVarChar);
                SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);

                pAction.Value =7;
                pStrCondition.Value = RepCondition;
                pCOND.Value = COND;
                Open(CONNECTION_STRING);
                SqlParameter[] param = new SqlParameter[] {pAction,pStrCondition,pCOND };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, Deviation.SP_Deviation, param);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet GetRecordForEdit(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(Deviation._Action, SqlDbType.BigInt);
                SqlParameter pDeviationID = new SqlParameter(Deviation._DeviationID, SqlDbType.BigInt);

                pAction.Value = 8;
                pDeviationID.Value = ID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Deviation.SP_Deviation, pAction, pDeviationID);
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
                SqlParameter MAction = new SqlParameter(Deviation._Action, SqlDbType.VarChar);
                SqlParameter MStrCondition = new SqlParameter(Deviation._StrCondition, SqlDbType.NVarChar);

                MAction.Value =7;
                MStrCondition.Value = prefixText;

                SqlParameter[] oParmCol = new SqlParameter[] { MAction, MStrCondition };
                Open(Setting.CONNECTION_STRING);

                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, Deviation.SP_Deviation, oParmCol);

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
        #endregion------------------------------------------
        #region Report--------------------------------------
        public DataSet GetInwardForPrint(Int32 DeviationID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(Deviation._Action, SqlDbType.BigInt);
                SqlParameter pDeviationID = new SqlParameter(Deviation._DeviationID, SqlDbType.BigInt);

                pAction.Value = 9;
                pDeviationID.Value = DeviationID;

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, Deviation.SP_Deviation, pAction, pDeviationID);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        #endregion----------------------------------------------
        public DMDeviation()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
