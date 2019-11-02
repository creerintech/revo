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
    public class DMEmployee:Utility.Setting
    {
        public int InsertRecord(ref EmployeeMaster Entity_Call,out string StrError)
        {
            int iInsert=0;
            StrError=string.Empty;

            try
            {
               SqlParameter pAction=new SqlParameter(EmployeeMaster._Action,SqlDbType.NVarChar);
                SqlParameter pEmpName=new SqlParameter(EmployeeMaster._EmpName,SqlDbType.NVarChar);
                SqlParameter pAddress=new SqlParameter(EmployeeMaster._Address,SqlDbType.NVarChar);
                SqlParameter pTelNo = new SqlParameter(EmployeeMaster._TelNo, SqlDbType.NVarChar);
                SqlParameter PMobileNo = new SqlParameter(EmployeeMaster._MobileNo, SqlDbType.NVarChar);
                SqlParameter pEmail=new SqlParameter(EmployeeMaster._Email,SqlDbType.NVarChar);
                SqlParameter pNote=new SqlParameter(EmployeeMaster._Note,SqlDbType.NVarChar);
                SqlParameter pDepartment = new SqlParameter(EmployeeMaster._Department, SqlDbType.NVarChar);
                SqlParameter PDesignation = new SqlParameter(EmployeeMaster._Designation,SqlDbType.NVarChar);
                SqlParameter pCreatedBy=new SqlParameter(EmployeeMaster._UserId,SqlDbType.BigInt);
                SqlParameter pCreatedDate=new SqlParameter(EmployeeMaster._LoginDate,SqlDbType.DateTime);
                SqlParameter pIsDeleted=new SqlParameter(EmployeeMaster._IsDeleted,SqlDbType.Bit);

                pAction.Value=1;
                pEmpName.Value=Entity_Call.EmpName;
                pAddress.Value=Entity_Call.Address;
                pTelNo.Value=Entity_Call.TelNo;
                PMobileNo.Value = Entity_Call.MobileNo;
                pEmail.Value=Entity_Call.Email;
                pNote.Value=Entity_Call.Note;
                //pDepartment.Value = Entity_Call.Department;
                //PDesignation.Value = Entity_Call.Designation;
                pCreatedBy.Value=Entity_Call.UserId;
                pCreatedDate.Value = Entity_Call.LoginDate;
                pIsDeleted.Value=Entity_Call.IsDeleted;

                SqlParameter[]param =new SqlParameter[]{pAction,pEmpName,pAddress,
                pTelNo,PMobileNo,pEmail,pNote,pCreatedBy,pCreatedDate,pIsDeleted};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert=SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,
                    EmployeeMaster.SP_EmployeeMaster,param);

                if(iInsert>0)
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
                StrError=ex.Message;
            }

            finally
            {
                Close();
            }
            return iInsert;
        }

        public int UpdateRecord(ref EmployeeMaster Entity_Call,out string StrError)
        {
            int iInsert=0;
            StrError=string.Empty;
            
            try
            {
                SqlParameter pAction=new SqlParameter(EmployeeMaster._Action,SqlDbType.BigInt);
                SqlParameter pEmployeeId=new SqlParameter(EmployeeMaster._EmployeeId,SqlDbType.BigInt);
                SqlParameter pEmpName=new SqlParameter(EmployeeMaster._EmpName,SqlDbType.NVarChar);
                SqlParameter pAddress=new SqlParameter(EmployeeMaster._Address,SqlDbType.NVarChar);
                SqlParameter pTelNo = new SqlParameter(EmployeeMaster._TelNo, SqlDbType.NVarChar);
                SqlParameter PMobileNo = new SqlParameter(EmployeeMaster._MobileNo, SqlDbType.NVarChar);
                SqlParameter pEmail=new SqlParameter(EmployeeMaster._Email,SqlDbType.NVarChar);
                SqlParameter pNote=new SqlParameter(EmployeeMaster._Note,SqlDbType.NVarChar);
                //SqlParameter pDepartment = new SqlParameter(EmployeeMaster._Department, SqlDbType.NVarChar);
                //SqlParameter PDesignation = new SqlParameter(EmployeeMaster._Designation, SqlDbType.NVarChar);
                SqlParameter pUpdatedBy=new SqlParameter(EmployeeMaster._UserId,SqlDbType.BigInt);
                SqlParameter pUpdatedDate=new SqlParameter(EmployeeMaster._LoginDate,SqlDbType.DateTime);

                pAction.Value=2;
                pEmployeeId.Value=Entity_Call.EmployeeId;
                pEmpName.Value=Entity_Call.EmpName;
                pAddress.Value=Entity_Call.Address;
                pTelNo.Value=Entity_Call.TelNo;
                PMobileNo.Value = Entity_Call.MobileNo;
                pEmail.Value=Entity_Call.Email;

                pNote.Value=Entity_Call.Note;
                //pDepartment.Value = Entity_Call.Department;
                //PDesignation.Value = Entity_Call.Designation;
                pUpdatedBy.Value = Entity_Call.UserId;
                pUpdatedDate.Value = Entity_Call.LoginDate;
               
                SqlParameter []param =new SqlParameter[]{pAction,pEmployeeId,pEmpName,pAddress,pTelNo,PMobileNo,
                pEmail,pNote,pUpdatedBy,pUpdatedDate};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iInsert=SQLHelper.ExecuteNonQuery(_Connection ,_Transaction,CommandType.StoredProcedure,EmployeeMaster.SP_EmployeeMaster,param);

                if(iInsert>0)
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
                StrError=ex.Message;
            }
            finally
            {
                Close();
            }
            return iInsert;
        }

        public int DeleteRecord(ref EmployeeMaster EntityCall, out string StrError)
        {
            int iDelete = 0;
            StrError = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(EmployeeMaster._Action, SqlDbType.BigInt);
                SqlParameter pEmployeeId=new SqlParameter(EmployeeMaster._EmployeeId,SqlDbType.BigInt);
                SqlParameter pDeletedBy=new SqlParameter(EmployeeMaster._UserId,SqlDbType.BigInt);
                SqlParameter pDeletedDate=new SqlParameter(EmployeeMaster._LoginDate,SqlDbType.DateTime);
                SqlParameter pIsDeleted =new SqlParameter(EmployeeMaster._IsDeleted,SqlDbType.Bit);

                pAction.Value=3;
                pEmployeeId.Value=EntityCall.EmployeeId;
                pDeletedBy.Value=EntityCall.UserId;
                pDeletedDate.Value=EntityCall.LoginDate;
                pIsDeleted.Value=EntityCall.IsDeleted;

                SqlParameter[] param=new SqlParameter[]{pAction,pEmployeeId,pDeletedBy,pDeletedDate,pIsDeleted};

                Open(CONNECTION_STRING);
                BeginTransaction();

                iDelete=SQLHelper.ExecuteNonQuery(_Connection,_Transaction,CommandType.StoredProcedure,EmployeeMaster.SP_EmployeeMaster,param);

                if(iDelete>0)
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
                StrError=ex.Message;
            }
            finally
            {
                Close();
            }
            return iDelete;
        }

        public DataSet GetEmployeeForEdit(int ID, out string StrError)
        {
            StrError = string.Empty;
            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(EmployeeMaster._Action, SqlDbType.BigInt);
                SqlParameter pEmployeeId = new SqlParameter(EmployeeMaster._EmployeeId, SqlDbType.BigInt);

                pAction.Value = 4;
                pEmployeeId.Value = ID;

                Open(CONNECTION_STRING);

                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EmployeeMaster.SP_EmployeeMaster, pAction, pEmployeeId);

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

        public DataSet GeteEmployee(string RepCondition, out string StrError)
        {
            StrError = string.Empty;

            DataSet DS = new DataSet();

            try
            {
                SqlParameter pAction = new SqlParameter(EmployeeMaster._Action, SqlDbType.BigInt);
                SqlParameter PrepCondition = new SqlParameter(EmployeeMaster._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 5;
                PrepCondition.Value = RepCondition;

                Open(CONNECTION_STRING);

                DS = SQLHelper.GetDataSetDoubleParm(_Connection, _Transaction, CommandType.StoredProcedure, EmployeeMaster.SP_EmployeeMaster, pAction, PrepCondition);


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

        public DataSet ChkDuplicate(string Name, out string StrError)
        {
            StrError = string.Empty;

            DataSet DS = new DataSet();
            try
            {
                SqlParameter pAction=new SqlParameter(EmployeeMaster._Action,SqlDbType.BigInt);
                SqlParameter pRepCondition=new SqlParameter(EmployeeMaster._StrCondition,SqlDbType.NVarChar);

                pAction.Value=6;
                pRepCondition.Value=Name;

                Open(CONNECTION_STRING);
                DS=SQLHelper.GetDataSetDoubleParm(_Connection,_Transaction,CommandType.StoredProcedure,EmployeeMaster.SP_EmployeeMaster,pAction,pRepCondition);

            }
            catch (Exception ex)
            {
                StrError=ex.Message;
            }
            finally
            {
                Close();
            }
            return DS;
        }

        public string[] GetSuggestRecord(string preFixText)
        {
            List<string> SearchList = new List<string>();
            string ListItem = string.Empty;

            try
            {
                SqlParameter pAction = new SqlParameter(EmployeeMaster._Action, SqlDbType.BigInt);
                SqlParameter PrepCondition = new SqlParameter(EmployeeMaster._StrCondition, SqlDbType.NVarChar);

                pAction.Value = 5;
                PrepCondition.Value = preFixText;

                SqlParameter[] oparamcol = new SqlParameter[] { pAction, PrepCondition };

                Open(CONNECTION_STRING);
                SqlDataReader dr = SQLHelper.ExecuteReader(_Connection, _Transaction, CommandType.StoredProcedure, EmployeeMaster.SP_EmployeeMaster, oparamcol);

                if (dr != null && dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        ListItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(),
                            dr[1].ToString());

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

        public DataSet InsertData(string XMLstr,out string strError)
        {
            strError = string.Empty;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter MAction = new SqlParameter("@Action", SqlDbType.BigInt);
                SqlParameter MXMLstr = new SqlParameter("@XMLstr", SqlDbType.NVarChar);              

                MAction.Value = 3;
                MXMLstr.Value = XMLstr;

                Open(CONNECTION_STRING);
                SqlParameter[] ParamArray = new SqlParameter[] { MAction, MXMLstr };
                Ds = SQLHelper.GetDataSet(_Connection, _Transaction, CommandType.StoredProcedure, "SP_EmployeeMaster", ParamArray);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            finally { Close(); }
            return Ds;
        }
    }


    //public class DMEmployee
    //{
    //    public DMEmployee()
    //    {
    //        //
    //        // TODO: Add constructor logic here
    //        //
    //    }
    //}
}