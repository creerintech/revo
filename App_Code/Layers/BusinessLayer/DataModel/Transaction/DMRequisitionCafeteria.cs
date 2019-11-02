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

namespace MayurInventory.DataModel
{
        public class DMRequisitionCafeteria : Utility.Setting
        {
            public DataSet BindAvaliableQty(int ItemID, int LOCID, out string strError)
            {
                DataSet ds = new DataSet();
                strError = string.Empty;
                try
                {
                    SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                    SqlParameter pItemID = new SqlParameter(RequisitionCafeteria._ItemId, SqlDbType.BigInt);
                    SqlParameter pLOCID = new SqlParameter("LOCID", SqlDbType.BigInt);
                    pAction.Value = 12;
                    pItemID.Value = ItemID;
                    pLOCID.Value = LOCID;
                    SqlParameter[] param = new SqlParameter[]{pAction, pItemID, pLOCID};
                    Open(CONNECTION_STRING);
                    ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);
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
                    SqlParameter pReqID = new SqlParameter(RequisitionCafeteria._RequisitionCafeId, SqlDbType.BigInt);
                    pAction.Value = 11;
                    pReqID.Value = TemplateID;
                    Open(CONNECTION_STRING);
                    ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pReqID);
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


        public DataSet BindForReportoffer(string TemplateID, out string strError)
        {
            DataSet ds = new DataSet();
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter pReqID = new SqlParameter(RequisitionCafeteria._pn, SqlDbType.NVarChar);
                pAction.Value = 111;
                pReqID.Value = TemplateID;
                Open(CONNECTION_STRING);
                ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pReqID);
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




        public int InsertRequisitionDetails(ref RequisitionCafeteria Entity_Call, out string StrError)
            {
                int iInsert = 0;
                StrError = string.Empty;

                try 
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action,SqlDbType.BigInt);
                    SqlParameter pRequisitionNo = new SqlParameter(RequisitionCafeteria._RequisitionNo,SqlDbType.NVarChar);
                    SqlParameter pRequisitionDate = new SqlParameter(RequisitionCafeteria._RequisitionDate,SqlDbType.DateTime);
                    SqlParameter pCefeteriaId = new SqlParameter(RequisitionCafeteria._CafeteriaId,SqlDbType.BigInt );
                    SqlParameter pUserId = new SqlParameter(RequisitionCafeteria._UserId,SqlDbType.BigInt);
                    SqlParameter pLoginDate = new SqlParameter(RequisitionCafeteria._LoginDate,SqlDbType.DateTime);
                    SqlParameter pIsCostCentre = new SqlParameter(RequisitionCafeteria._IsCostCentre, SqlDbType.BigInt);
                    SqlParameter pRemark = new SqlParameter(RequisitionCafeteria._Remark, SqlDbType.NVarChar);
                    SqlParameter pRemarkIND = new SqlParameter(RequisitionCafeteria._RemarkIND, SqlDbType.NVarChar);
                    //RemarkIND
                    pAction.Value = 4;
                    pRequisitionNo.Value = Entity_Call.RequisitionNo;
                    pRequisitionDate.Value = Entity_Call.RequisitionDate;
                    pCefeteriaId.Value = Entity_Call.CafeteriaId;
                    pUserId.Value = Entity_Call.UserId;
                    pLoginDate.Value = Entity_Call.LoginDate;
                    pIsCostCentre.Value = Entity_Call.IsCostCentre;
                    pRemark.Value = Entity_Call.Remark;
                    pRemarkIND.Value = Entity_Call.RemarkIND;
                    SqlParameter[] param = new SqlParameter[] {pAction,pRequisitionNo,pRequisitionDate,pCefeteriaId,
                    pUserId, pLoginDate,pIsCostCentre,pRemark,pRemarkIND};
                    Open(CONNECTION_STRING);
                    BeginTransaction();
                    iInsert = SQLHelper.ExecuteScalar(_Connection,_Transaction,CommandType.StoredProcedure,RequisitionCafeteria.SP_RequisitionCafeteriaTemplate,param);
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

            public int InsertRequisitionCafeDetails(ref RequisitionCafeteria Entity_Call, out string StrError)
            {
                int iInsert = 0;
                StrError = string.Empty;
                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action,SqlDbType.BigInt);
                    SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCafeteria._RequisitionCafeId,SqlDbType.BigInt);
                    SqlParameter pItemId = new SqlParameter(RequisitionCafeteria._ItemId,SqlDbType.BigInt);
                    SqlParameter pQty = new SqlParameter(RequisitionCafeteria._Qty,SqlDbType.Decimal);
                    SqlParameter pTemplateID = new SqlParameter(RequisitionCafeteria._TemplateID,SqlDbType.BigInt);
                    SqlParameter pExpdDate = new SqlParameter(RequisitionCafeteria._ExpdDate,SqlDbType.DateTime);
                    SqlParameter pVendorId = new SqlParameter(RequisitionCafeteria._VendorId, SqlDbType.BigInt);
                    SqlParameter pRate = new SqlParameter(RequisitionCafeteria._Rate, SqlDbType.Decimal);
                    SqlParameter pAvlQty = new SqlParameter(RequisitionCafeteria._AvlQty, SqlDbType.Decimal);
                    SqlParameter pIsCancel = new SqlParameter(RequisitionCafeteria._IsCancel, SqlDbType.Bit);
                    SqlParameter pPriorityID = new SqlParameter(RequisitionCafeteria._PriorityID, SqlDbType.BigInt);
                    SqlParameter pTransitQty = new SqlParameter(RequisitionCafeteria._TransitQty, SqlDbType.Decimal);
                    //---Newly Added Fields---
                    SqlParameter pItemDetailsId = new SqlParameter(RequisitionCafeteria._ItemDetailsId, SqlDbType.BigInt);
                    SqlParameter pUnitConvDtlsId = new SqlParameter(RequisitionCafeteria._UnitConvDtlsId, SqlDbType.BigInt);
                    SqlParameter pRemarkForPO = new SqlParameter(RequisitionCafeteria._RemarkForPO, SqlDbType.NVarChar);
                    SqlParameter pMinStockLevel = new SqlParameter(RequisitionCafeteria._MinStockLevel, SqlDbType.NVarChar);
                    SqlParameter pMaxStockLevel = new SqlParameter(RequisitionCafeteria._MaxStockLevel, SqlDbType.NVarChar);
                    SqlParameter pRequiredDate = new SqlParameter(RequisitionCafeteria._RequiredDate, SqlDbType.DateTime);
                SqlParameter pbalstock = new SqlParameter(RequisitionCafeteria._balstock, SqlDbType.NVarChar);
                SqlParameter pavlstock = new SqlParameter(RequisitionCafeteria._avlstock, SqlDbType.NVarChar);
                SqlParameter punit = new SqlParameter(RequisitionCafeteria._unit, SqlDbType.NVarChar);

                pAction.Value = 5;


                punit.Value = Entity_Call.unit;
                pavlstock.Value = Entity_Call.avlstock ;
                pbalstock.Value = Entity_Call.balstock;
                    pRequisitionCafeId.Value = Entity_Call.RequisitionCafeId;
                    pItemId.Value = Entity_Call.ItemId;
                    pQty.Value = Entity_Call.Qty;
                    pTemplateID.Value = Entity_Call.TemplateID;
                    pExpdDate.Value = Entity_Call.ExpdDate;
                    pVendorId.Value = Entity_Call.VendorId;
                    pAvlQty.Value = Entity_Call.AvlQty;
                    pTransitQty.Value = Entity_Call.TransitQty;
                    pRate.Value=Entity_Call.Rate;
                    pIsCancel.Value = Entity_Call.IsCancel;
                    pPriorityID.Value = Entity_Call.PriorityID;
                    pItemDetailsId.Value = Entity_Call.ItemDetailsId;
                    pUnitConvDtlsId.Value = Entity_Call.UnitConvDtlsId;
                    pRemarkForPO.Value = Entity_Call.RemarkForPO;
                    pMinStockLevel.Value = Entity_Call.MinStockLevel;
                    pMaxStockLevel.Value = Entity_Call.MaxStockLevel;
                    pRequiredDate.Value = Entity_Call.RequiredDate;

                    SqlParameter[] param = new SqlParameter[] { pAction, pRequisitionCafeId, pItemId, pQty, pExpdDate, pTemplateID,pAvlQty,pTransitQty,
                            pVendorId, pRate,pIsCancel,pPriorityID,pItemDetailsId,pUnitConvDtlsId,pRemarkForPO,pMinStockLevel,pMaxStockLevel,pRequiredDate ,pavlstock,pbalstock ,punit};
                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    iInsert = SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,RequisitionCafeteria.SP_RequisitionCafeteriaTemplate,param);

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
                    StrError=string.Empty;
                    RollBackTransaction();
                }
                finally
                {
                    Close();
                }

                return iInsert;
            }

            public int UpdateRequisitionDetails(ref RequisitionCafeteria Entity_Call, out string StrError)
            {
                int iInsert = 0;
                StrError = string.Empty;

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCafeteria._RequisitionCafeId, SqlDbType.BigInt);
                    SqlParameter PRequisitionDate = new SqlParameter(RequisitionCafeteria._RequisitionDate, SqlDbType.DateTime);
                    SqlParameter pUpdatedBy = new SqlParameter(RequisitionCafeteria._UserId, SqlDbType.BigInt);
                    SqlParameter pUpdatedDate = new SqlParameter(RequisitionCafeteria._LoginDate, SqlDbType.DateTime);
                    SqlParameter pIsCostCentre = new SqlParameter(RequisitionCafeteria._IsCostCentre, SqlDbType.BigInt);
                    SqlParameter pRemark = new SqlParameter(RequisitionCafeteria._Remark, SqlDbType.NVarChar);
                    SqlParameter pRemarkIND = new SqlParameter(RequisitionCafeteria._RemarkIND, SqlDbType.NVarChar);
                    pAction.Value = 6;
                    pRequisitionCafeId.Value = Entity_Call.RequisitionCafeId;
                    PRequisitionDate.Value = Entity_Call.RequisitionDate;
                    pUpdatedBy.Value = Entity_Call.UserId;
                    pUpdatedDate.Value = Entity_Call.LoginDate;
                    pIsCostCentre.Value = Entity_Call.IsCostCentre;
                    pRemark.Value = Entity_Call.Remark;
                    pRemarkIND.Value = Entity_Call.RemarkIND;
                    SqlParameter[] param = new SqlParameter[] { pAction, pRequisitionCafeId, PRequisitionDate, pUpdatedBy, pUpdatedDate, pIsCostCentre, pRemark, pRemarkIND };

                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);

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

            public int DeleteRequisition(ref RequisitionCafeteria Entity_Call,out string StrError)
            {
                int iDelete=0;
                StrError = string.Empty;

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCafeteria._RequisitionCafeId, SqlDbType.BigInt);

                    SqlParameter pDeletedBy = new SqlParameter(RequisitionCafeteria._UserId, SqlDbType.BigInt);
                    SqlParameter pDeletedDate = new SqlParameter(RequisitionCafeteria._LoginDate, SqlDbType.DateTime);

                    pAction.Value = 9;
                    pRequisitionCafeId.Value = Entity_Call.RequisitionCafeId;
                    pDeletedBy.Value = Entity_Call.UserId;
                    pDeletedDate.Value = Entity_Call.LoginDate;

                    SqlParameter[] param = new SqlParameter[] { pAction, pRequisitionCafeId, pDeletedBy, pDeletedDate };

                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);

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

            public DataSet GetRequisitionDetailsForEdit(int ID, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCafeteria._RequisitionCafeId, SqlDbType.BigInt);

                    pAction.Value = 10;
                    pRequisitionCafeId.Value = ID;

                    Open(CONNECTION_STRING);

                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pRequisitionCafeId);

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



        public DataSet GetRequisitionDetailsForEdititem(int ID, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                SqlParameter pRequisitionCafeId = new SqlParameter(RequisitionCafeteria._RequisitionCafeId, SqlDbType.BigInt);

                pAction.Value = 101;
                pRequisitionCafeId.Value = ID;

                Open(CONNECTION_STRING);

                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pRequisitionCafeId);

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

        public DataSet GetRequisition(string RepCondition,String LOCID, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pRepCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);
                    SqlParameter pLOCID = new SqlParameter("@Remark", SqlDbType.NVarChar);

                    pAction.Value = 7;
                    pRepCondition.Value = RepCondition;
                    pLOCID.Value = LOCID;

                    SqlParameter[] param = new SqlParameter[] { pAction, pRepCondition, pLOCID };
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure,RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);

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

            public DataSet GetItems(string RepCondition, String LOCID, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pRepCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);
                    SqlParameter pLOCID = new SqlParameter("@Remark", SqlDbType.NVarChar);

                    pAction.Value = 19;
                    pRepCondition.Value = RepCondition;
                    pLOCID.Value = LOCID;

                    SqlParameter[] param = new SqlParameter[] { pAction, pRepCondition, pLOCID };
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);

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


            public DataSet GetItemsDetails(int Catid,string RepCondition, String LOCID, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pRepCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);
                    SqlParameter pLOCID = new SqlParameter("@Remark", SqlDbType.NVarChar);
                    SqlParameter pCatid = new SqlParameter("@CategoryId",SqlDbType.BigInt);
                   
                    pAction.Value = 19;
                    pRepCondition.Value = RepCondition;
                    pLOCID.Value = LOCID;
                    pCatid.Value = Catid;

                    SqlParameter[] param = new SqlParameter[] { pAction, pRepCondition, pLOCID, pCatid };
                    Open(CONNECTION_STRING);

                    DS = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);
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

            public DataSet GetRequisitionDetails(string RepCondition, out string StrError)
            {
                StrError = string.Empty;

                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter PRepCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);

                    pAction.Value = 5;
                    PRepCondition.Value = RepCondition;

                    Open(CONNECTION_STRING);

                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure,RequisitionCafeteria.SP_RequisitionCafeteria, pAction, PRepCondition);


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

            public DataSet ChkDuplicate(string RepCondition, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter PRepCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);

                    pAction.Value = 6;
                    PRepCondition.Value = RepCondition;
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure,RequisitionCafeteria.SP_RequisitionCafeteria, pAction, PRepCondition);

                }

                catch (Exception ex)
                {
                    StrError = ex.Message;
                }
                return DS;
            }

            public DataSet FillCombo(int LOCID,out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pLOCID = new SqlParameter("@LOCID", SqlDbType.BigInt);
                    pAction.Value = 1;
                    pLOCID.Value = LOCID;
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction,pLOCID);
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






        public DataSet FillCombo1(int LOCID, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                SqlParameter pLOCID = new SqlParameter("@LOCID", SqlDbType.BigInt);
                pAction.Value = 277;
                pLOCID.Value = LOCID;
                Open(CONNECTION_STRING);
                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pLOCID);
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

        public DataSet GETDATAFORMAIL(int ReqID, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pREQID = new SqlParameter(RequisitionCafeteria._RequisitionCafeId, SqlDbType.BigInt);
                    pAction.Value = 13;
                    pREQID.Value = ReqID;
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pREQID);
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

            public DataSet GetUnit(int ID,out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pItemId = new SqlParameter(RequisitionCafeteria._ItemId, SqlDbType.BigInt);

                    pAction.Value = 10;
                    pItemId.Value = ID;

                    Open(CONNECTION_STRING);

                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteria, pAction, pItemId);

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
            public DataSet GetTemplateData(int ID,decimal Qty, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pTemplateID = new SqlParameter(RequisitionCafeteria._TemplateID, SqlDbType.BigInt);
                    SqlParameter pQty = new SqlParameter(RequisitionCafeteria._Qty, SqlDbType.Decimal);
                    pAction.Value = 2;
                    pTemplateID.Value = ID;
                    pQty.Value = Qty;
                    Open(CONNECTION_STRING);
                    SqlParameter[] param = new SqlParameter[] { pAction, pTemplateID, pQty };
                    DS = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);
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
            public DataSet GetItemDataAccordingToID(int ID, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pItemId = new SqlParameter(RequisitionCafeteria._ItemId, SqlDbType.BigInt);
                    pAction.Value = 3;
                    pItemId.Value = ID;
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pItemId);
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
            public DataSet GetDuplicateName(string Name, out string strError)
            {
                DataSet ds = new DataSet();
                strError = string.Empty;
                try
                {
                    SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                    SqlParameter pRequisitionNo = new SqlParameter("@RequisitionNo", SqlDbType.NVarChar);
                    pAction.Value = 8;
                    pRequisitionNo.Value = Name;
                    Open(CONNECTION_STRING);
                    ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pRequisitionNo);
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

            public DataSet GetRequisitionNo(out string StrError)
            {
                DataSet DS = new DataSet();
                StrError = string.Empty;
                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    pAction.Value = 9;
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetSingleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteria, pAction);

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

            public string[] GetSuggestedRecord(string prefixText, string LOC)
            {
                List<string> SearchList = new List<string>();
                string ListItem = string.Empty;

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pRepCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);
                    SqlParameter pRemark = new SqlParameter("@Remark", SqlDbType.NVarChar);
                    pAction.Value = 7;
                    pRepCondition.Value = prefixText;
                    pRemark.Value = LOC;

                    SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition,pRemark};

                    Open(CONNECTION_STRING);

                    SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, oparamcol);

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

            public string[] GetSuggestedRecordForTo(string prefixText, string LOC)
            {
                List<string> SearchList = new List<string>();
                string ListItem = string.Empty;

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pRepCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);
                    SqlParameter pRemark = new SqlParameter("@Remark", SqlDbType.NVarChar);
                    pAction.Value = 20;
                    pRepCondition.Value = prefixText;
                    pRemark.Value = LOC;

                    SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition, pRemark };

                    Open(CONNECTION_STRING);

                    SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, oparamcol);

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

            public string[] GetSuggestedRecordItems(string prefixText, string LOC)
            {
                List<string> SearchList = new List<string>();
                string ListItem = string.Empty;

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pRepCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);
                    SqlParameter pRemark = new SqlParameter("@Remark", SqlDbType.NVarChar);                 
                    pAction.Value = 18;
                    pRepCondition.Value = prefixText;
                    pRemark.Value = LOC;                    
                    SqlParameter[] oparamcol = new SqlParameter[] { pAction, pRepCondition, pRemark };

                    Open(CONNECTION_STRING);

                    SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, oparamcol);

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

            public DataSet GetFactor(int UnitConversionId,int ItemId, out string strError)
            {
                DataSet ds = new DataSet();
                strError = string.Empty;
                try
                {
                    SqlParameter pAction = new SqlParameter("@Action", SqlDbType.BigInt);
                    SqlParameter pUnitConvDtlsId = new SqlParameter(RequisitionCafeteria._UnitConvDtlsId, SqlDbType.BigInt);
                    SqlParameter pItemId = new SqlParameter(RequisitionCafeteria._ItemId, SqlDbType.BigInt);
                  
                    pAction.Value = 14;
                    pUnitConvDtlsId.Value = UnitConversionId;
                    pItemId.Value = ItemId;
                   
                    SqlParameter[] param = new SqlParameter[] { pAction, pUnitConvDtlsId,pItemId };
                    Open(CONNECTION_STRING);
                    ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);
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
            public DataSet GetSubCategory(int ID, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pCategoryId = new SqlParameter("@CategoryId", SqlDbType.BigInt);
                    pAction.Value = 15;
                    pCategoryId.Value = ID;
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pCategoryId);
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
            public DataSet GetItemsONSubCategory(int ID, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pSubCategoryId = new SqlParameter("@SubCategoryId", SqlDbType.BigInt);
                    pAction.Value = 16;
                    pSubCategoryId.Value = ID;
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pSubCategoryId);
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

            public DataSet GetUnitOnItemDesc(int ITEMID, int ITEMDESCID, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);
                    SqlParameter pItemDetailsId = new SqlParameter("@ItemDetailsId", SqlDbType.BigInt);
                    pAction.Value = 17;
                    pItemId.Value = ITEMID;
                    pItemDetailsId.Value = ITEMDESCID;
                    SqlParameter[] param = new SqlParameter[] {pAction,pItemId,pItemDetailsId };
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);
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
            public DataSet FillGETINDENTNO(int LOCID, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pLOCID = new SqlParameter("@LOCID", SqlDbType.BigInt);
                    pAction.Value = 1;
                    pLOCID.Value = LOCID;
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pLOCID);
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

            public DataSet CkeckDuplicateSaveTime(string Rno, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pRequisitionNo = new SqlParameter(RequisitionCafeteria._RequisitionNo, SqlDbType.NVarChar);

                    pAction.Value = 21;
                    pRequisitionNo.Value = Rno;

                    Open(CONNECTION_STRING);

                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pRequisitionNo);

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


            public DataSet SetEmailStatus(int ReqID, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pREQID = new SqlParameter(RequisitionCafeteria._RequisitionCafeId, SqlDbType.BigInt);

                    pAction.Value = 22;
                    pREQID.Value = ReqID;

                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, pAction, pREQID);
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


            public int UpdateEmailStatus(int ReqID, out string StrError)
            {
                int UpdateEStatus = 0;
                StrError = string.Empty;

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pREQID = new SqlParameter(RequisitionCafeteria._RequisitionCafeId, SqlDbType.BigInt);

                    pAction.Value = 22;
                    pREQID.Value = ReqID;

                    SqlParameter[] param = new SqlParameter[] { pAction, pREQID };

                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    UpdateEStatus = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);

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

            public int UpdateApproveEmailStatus(int ReqID, out string StrError)
            {
                int UpdateEStatus = 0;
                StrError = string.Empty;

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pREQID = new SqlParameter(RequisitionCafeteria._RequisitionCafeId, SqlDbType.BigInt);


                    pAction.Value = 23;
                    pREQID.Value = ReqID;

                    SqlParameter[] param = new SqlParameter[] { pAction, pREQID };

                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    UpdateEStatus = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);

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


            public int UpdateApproveEmailStatusNew(int ReqID,long UserId, out string StrError)
            {
                int UpdateEStatus = 0;
                StrError = string.Empty;

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pREQID = new SqlParameter(RequisitionCafeteria._RequisitionCafeId, SqlDbType.BigInt);
                    SqlParameter pUserID = new SqlParameter("@UserId", SqlDbType.BigInt);

                    pAction.Value = 23;
                    pREQID.Value = ReqID;
                    pUserID.Value = UserId;
                   

                    SqlParameter[] param = new SqlParameter[] { pAction, pREQID, pUserID};

                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    UpdateEStatus = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);

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
            public int UpdateEmailStatusNew(int ReqID, long userID,out string StrError)
            {
                int UpdateEStatus = 0;
                StrError = string.Empty;

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pREQID = new SqlParameter(RequisitionCafeteria._RequisitionCafeId, SqlDbType.BigInt);
                    SqlParameter pUserID = new SqlParameter("@UserId", SqlDbType.BigInt);
                    pAction.Value = 22;
                    pREQID.Value = ReqID;
                    pUserID.Value = userID;
                    SqlParameter[] param = new SqlParameter[] { pAction, pREQID,pUserID };

                    Open(CONNECTION_STRING);
                    BeginTransaction();

                    UpdateEStatus = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);

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
            ///////FOR REQUISITION CAFETERIA REPORT////////////////////////////////////////////////
            #region [Report Section]-----------------------------------------------------------------------------
            public DataSet GetRequisationForPrintSummary(string StrCondition, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pStrCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);

                    pAction.Value = 2;
                    pStrCondition.Value = StrCondition;

                    Open(CONNECTION_STRING);

                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaReport1, pAction, pStrCondition);

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
            public DataSet FillRequisitionNoComboForReport(int userId,out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pCOND = new SqlParameter("@COND", SqlDbType.NVarChar);
                    SqlParameter pUserId = new SqlParameter("@UserID", SqlDbType.NVarChar);

                    pAction.Value = 1;
                  //  pCOND.Value = COND;
                    pUserId.Value = userId;
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaReport1, pAction, pUserId);
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

            public DataSet GetDetailAgainstEmp(int userId, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pUserId = new SqlParameter("@UserID", SqlDbType.NVarChar);

                    pAction.Value = 7;
                    pUserId.Value = userId;
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaReport1, pAction, pUserId);
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
            public DataSet GetRequisitionDetailsForReport(string StrCondition, out string StrError,int action)
            {

                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pStrCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);
                    if (action == 1)
                    {
                        pAction.Value = 6;
                    }
                    else
                    {
                        pAction.Value = 7;
                    }
                    pStrCondition.Value = StrCondition;

                    Open(CONNECTION_STRING);

                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaReport, pAction, pStrCondition);

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

            public DataSet GetRequisitionDetailsForReportPM(string StrCondition, out string StrError, int action)
            {

                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pStrCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);
                    if (action == 1)
                    {
                        pAction.Value = 9;
                    }
                    else
                    {
                        pAction.Value = 10;
                    }
                    pStrCondition.Value = StrCondition;

                    Open(CONNECTION_STRING);

                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaReport, pAction, pStrCondition);

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

            public DataSet GetRequisitionDetailsITEMPENDINGForReport(string StrCondition, out string StrError, int action)
            {

                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pStrCondition = new SqlParameter(RequisitionCafeteria._StrCondition, SqlDbType.NVarChar);
                    if (action == 1)
                    {
                        pAction.Value = 8;
                    }
                    else
                    {
                        pAction.Value = 8;
                    }
                    pStrCondition.Value = StrCondition;

                    Open(CONNECTION_STRING);

                    DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaReport, pAction, pStrCondition);

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
            #endregion [End Report Region]-----------------------------------

            public DataSet GetRate(int ItemId, out string strError)
            {
                strError = string.Empty;
                DataSet Ds = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                    SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);

                    pAction.Value = 24;
                    pItemId.Value = ItemId;

                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_RequisitionCafeteriaTemplate", pAction, pItemId);

                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                }
                finally { Close(); }
                return Ds;
            }

            public DataSet FillComboProject(int Uid, out string StrError)
            {
                StrError = string.Empty;
                DataSet Ds = new DataSet();
                try
                {
                    SqlParameter pAction = new SqlParameter(MaterialInwardReg._Action, SqlDbType.BigInt);
                    SqlParameter pItemId = new SqlParameter("@UserId", SqlDbType.BigInt);

                    pAction.Value = 11;
                    pItemId.Value = Uid;

                    Open(CONNECTION_STRING);
                    Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, "SP_RequisitionCafeteria", pAction, pItemId);

                }
                catch (Exception ex)
                {
                    StrError = ex.Message;
                }
                finally { Close(); }
                return Ds;
            }



            public DataSet GetTaxAmt(DateTime dateTime, out string StrError)
            {
                throw new NotImplementedException();
            }

            public DataSet GetAvlQtyOnItemDtlsId(int ItemId, int ItemDtlsId, out string StrError)
            {
                StrError = string.Empty;
                DataSet DS = new DataSet();

                try
                {
                    SqlParameter pAction = new SqlParameter(RequisitionCafeteria._Action, SqlDbType.BigInt);
                    SqlParameter pItemId = new SqlParameter("@ItemId", SqlDbType.BigInt);
                    SqlParameter pItemDetailsId = new SqlParameter("@ItemDetailsId", SqlDbType.BigInt);
                    pAction.Value = 24;
                    pItemId.Value = ItemId;
                    pItemDetailsId.Value = ItemDtlsId;
                    SqlParameter[] param = new SqlParameter[] { pAction, pItemId, pItemDetailsId };
                    Open(CONNECTION_STRING);
                    DS = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, RequisitionCafeteria.SP_RequisitionCafeteriaTemplate, param);
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

