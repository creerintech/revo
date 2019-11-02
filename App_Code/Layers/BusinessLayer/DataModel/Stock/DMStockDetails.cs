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
/// Summary description for DMStockDetails
/// </summary>
/// 
namespace MayurInventory.DataModel
{
    public class DMStockDetails : Utility.Setting
    {

        public int InsertStockDetails(ref StockDetails Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(StockDetails._Action, SqlDbType.BigInt);
                SqlParameter pStockID = new SqlParameter(StockDetails._StockID, SqlDbType.BigInt);
                SqlParameter pStockTypeID = new SqlParameter(StockDetails._StockTypeID, SqlDbType.BigInt);
                SqlParameter pStockDate = new SqlParameter(StockDetails._StockDate, SqlDbType.DateTime);
                SqlParameter pProductID = new SqlParameter(StockDetails._ProductID, SqlDbType.BigInt);
                SqlParameter pStockLocationID = new SqlParameter(StockDetails._StockLocationID, SqlDbType.BigInt);
                SqlParameter pStockQty = new SqlParameter(StockDetails._StockQty, SqlDbType.Decimal);
                SqlParameter pProductMRP = new SqlParameter(StockDetails._ProductMRP, SqlDbType.Decimal);
                SqlParameter pStockUnitID = new SqlParameter(StockDetails._StockUnitID, SqlDbType.BigInt);
                SqlParameter pTransID = new SqlParameter(StockDetails._TransID, SqlDbType.BigInt);

                

                pAction.Value = 1;
                pStockID.Value = Entity_Call.StockID;
                pStockTypeID.Value = Entity_Call.StockTypeID;
                pStockDate.Value = Entity_Call.StockDate;
                pProductID.Value = Entity_Call.ProductID;
                pStockLocationID.Value = Entity_Call.StockLocationID;
                pStockQty.Value = Entity_Call.StockQty;
                pProductMRP.Value = Entity_Call.ProductMRP;
                pStockUnitID.Value = Entity_Call.StockUnitID;
                pTransID.Value = Entity_Call.TransID;

                SqlParameter[] Param = new SqlParameter[] { pAction, pStockID, pStockTypeID, pStockDate, pProductID,
                                                            pStockLocationID,pStockQty,pProductMRP,pStockUnitID,pTransID};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, StockDetails.SP_Stock_Details, Param);

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

        public int UpdateStockDetails(ref StockDetails Entity_Call, out string strError)
        {
            int iInsert = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(StockDetails._Action, SqlDbType.BigInt);
                SqlParameter pStockID = new SqlParameter(StockDetails._StockID, SqlDbType.BigInt);
                SqlParameter pStockTypeID = new SqlParameter(StockDetails._StockTypeID, SqlDbType.BigInt);
                SqlParameter pStockDate = new SqlParameter(StockDetails._StockDate, SqlDbType.DateTime);
                SqlParameter pProductID = new SqlParameter(StockDetails._ProductID, SqlDbType.BigInt);
                SqlParameter pStockLocationID = new SqlParameter(StockDetails._StockLocationID, SqlDbType.BigInt);
                SqlParameter pStockQty = new SqlParameter(StockDetails._StockQty, SqlDbType.Decimal);
                SqlParameter pProductMRP = new SqlParameter(StockDetails._ProductMRP, SqlDbType.Decimal);
                SqlParameter pStockUnitID = new SqlParameter(StockDetails._StockUnitID, SqlDbType.BigInt);
                SqlParameter pTransID = new SqlParameter(StockDetails._TransID, SqlDbType.BigInt);



                pAction.Value = 2;
                pStockID.Value = Entity_Call.StockID;
                pStockTypeID.Value = Entity_Call.StockTypeID;
                pStockDate.Value = Entity_Call.StockDate;
                pProductID.Value = Entity_Call.ProductID;
                pStockLocationID.Value = Entity_Call.StockLocationID;
                pStockQty.Value = Entity_Call.StockQty;
                pProductMRP.Value = Entity_Call.ProductMRP;
                pStockUnitID.Value = Entity_Call.StockUnitID;
                pTransID.Value = Entity_Call.TransID;

                SqlParameter[] Param = new SqlParameter[] { pAction, pStockID, pStockTypeID, pStockDate, pProductID,
                                                            pStockLocationID,pStockQty,pProductMRP,pStockUnitID,pTransID};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, StockDetails.SP_Stock_Details, Param);

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

        public int DeleteStockDetails(ref StockDetails Entity_Call, out string strError)
        {
            int iDelete = 0;
            strError = string.Empty;
            try
            {
                SqlParameter pAction = new SqlParameter(StockDetails._Action, SqlDbType.BigInt);
                SqlParameter pStockID = new SqlParameter(StockDetails._StockID, SqlDbType.BigInt);
                SqlParameter pStockTypeID = new SqlParameter(StockDetails._StockTypeID, SqlDbType.BigInt);
                SqlParameter pStockDate = new SqlParameter(StockDetails._StockDate, SqlDbType.DateTime);
                SqlParameter pProductID = new SqlParameter(StockDetails._ProductID, SqlDbType.BigInt);
                SqlParameter pStockLocationID = new SqlParameter(StockDetails._StockLocationID, SqlDbType.BigInt);
                SqlParameter pStockQty = new SqlParameter(StockDetails._StockQty, SqlDbType.Decimal);
                SqlParameter pProductMRP = new SqlParameter(StockDetails._ProductMRP, SqlDbType.Decimal);
                SqlParameter pStockUnitID = new SqlParameter(StockDetails._StockUnitID, SqlDbType.BigInt);
                SqlParameter pTransID = new SqlParameter(StockDetails._TransID, SqlDbType.BigInt);



                pAction.Value = 3;
                pStockID.Value = Entity_Call.StockID;
                pStockTypeID.Value = Entity_Call.StockTypeID;
                pStockDate.Value = Entity_Call.StockDate;
                pProductID.Value = Entity_Call.ProductID;
                pStockLocationID.Value = Entity_Call.StockLocationID;
                pStockQty.Value = Entity_Call.StockQty;
                pProductMRP.Value = Entity_Call.ProductMRP;
                pStockUnitID.Value = Entity_Call.StockUnitID;
                pTransID.Value = Entity_Call.TransID;

                SqlParameter[] Param = new SqlParameter[] { pAction, pStockID, pStockTypeID, pStockDate, pProductID,
                                                            pStockLocationID,pStockQty,pProductMRP,pStockUnitID,pTransID};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete = SQLHelper.ExecuteNonQuery(_Connection, _Transaction, CommandType.StoredProcedure, StockDetails.SP_Stock_Details, Param);

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

        public DataSet GetStockDetails(int ID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockDetails._Action, SqlDbType.BigInt);
                SqlParameter pStockID = new SqlParameter(StockDetails._StockID, SqlDbType.BigInt);
                SqlParameter pStockTypeID = new SqlParameter(StockDetails._StockTypeID, SqlDbType.BigInt);
                SqlParameter pStockDate = new SqlParameter(StockDetails._StockDate, SqlDbType.DateTime);
                SqlParameter pProductID = new SqlParameter(StockDetails._ProductID, SqlDbType.BigInt);
                SqlParameter pStockLocationID = new SqlParameter(StockDetails._StockLocationID, SqlDbType.BigInt);
                SqlParameter pStockQty = new SqlParameter(StockDetails._StockQty, SqlDbType.Decimal);
                SqlParameter pProductMRP = new SqlParameter(StockDetails._ProductMRP, SqlDbType.Decimal);
                SqlParameter pStockUnitID = new SqlParameter(StockDetails._StockUnitID, SqlDbType.BigInt);
                SqlParameter pTransID = new SqlParameter(StockDetails._TransID, SqlDbType.BigInt);



                pAction.Value = 4;
                pProductID.Value = ID;
                

                SqlParameter[] Param = new SqlParameter[] { pAction, pProductID};

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, ItemCategory.SP_ItemCategory, pAction, pProductID);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }

        public DataSet Get_Stock_For_Reorder_LocationWise(int LocationID, out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter pAction = new SqlParameter(StockDetails._Action, SqlDbType.BigInt);                                
                SqlParameter pStockLocationID = new SqlParameter(StockDetails._StockLocationID, SqlDbType.BigInt);

                pAction.Value = 1;
                pStockLocationID.Value = LocationID;


                SqlParameter[] Param = new SqlParameter[] { pAction, pStockLocationID };

                Open(CONNECTION_STRING);
                Ds = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, StockDetails.SP_Stock_Details, pAction,pStockLocationID);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;

        }
        public DMStockDetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}