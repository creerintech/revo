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

/// <summary>
/// Summary description for DataAccess
/// </summary>
/// 
namespace MayurInventory.DALSQLHelper
{
    public class SQLHelper

    {
      public static SqlDataReader ExecuteReaderSingleParm(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter singleParm)
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Parameters.Add(singleParm);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleResult);
            cmd.Parameters.Clear();
            return rdr;
        }

      public static SqlDataReader ExecuteReaderDoubleParm(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter Parm1, SqlParameter Parm2)
      {
          SqlCommand cmd = new SqlCommand();
          if (conn.State != ConnectionState.Open)
          {
              conn.Open();
          }
          cmd.Connection = conn;
          if (trans != null)
          {
              cmd.Transaction = trans;
          }
          cmd.CommandText = cmdText;
          cmd.CommandType = cmdType;
          cmd.Parameters.Add(Parm1);
          cmd.Parameters.Add(Parm2);
          SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleResult);
          cmd.Parameters.Clear();
          return rdr;
      }      
        public static SqlDataReader ExecuteReaderSingleRowSingleParm(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter singleParm)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Parameters.Add(singleParm);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);
            return rdr;
        }        
        public static SqlDataReader ExecuteReaderSingleRow(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            PrepareCommand(cmd, cmdParms);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);
            cmd.Parameters.Clear();
            return rdr;
        }        
        public static SqlDataReader ExecuteReaderNoParm(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleResult);
            return rdr;
        }        
        public static SqlDataReader ExecuteReader(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            PrepareCommand(cmd, cmdParms);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleResult);
            return rdr;
        }        
        public static int ExecuteScalar(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            PrepareCommand(cmd, cmdParms);
            //int val = Convert.ToInt32(cmd.ExecuteScalar());
            int val =(int)Convert.ToInt32(cmd.ExecuteScalar());
            return val;
        }
        public static int ExecuteScalarSingleParm(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter singleParm)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.Parameters.Add(singleParm);
            int val = (int)Convert.ToInt32(cmd.ExecuteScalar());
            return val;
        }
        public static object ExecuteScalarNoParm(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            object val = (int)Convert.ToInt32(cmd.ExecuteScalar());
            return val;
        }    
        public static int ExecuteNonQuery(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.StoredProcedure; ;
            PrepareCommand(cmd, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }   
        public static int ExecuteNonQuerySingleParm(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter singleParam)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Parameters.Add(singleParam);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        public static int ExecuteNonQueryDoubleParm(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter Param1, SqlParameter Param2)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Parameters.Add(Param1);
            cmd.Parameters.Add(Param2);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }    
        public static int ExecuteNonQueryNoParm(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            int val = cmd.ExecuteNonQuery();
            return val;
        }    
        public static DataSet GetDataSetNoParm(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);


            return ds;
        }       
        public static DataSet GetDataSetSingleParm(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter singleParam)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 150;
            cmd.CommandType = cmdType;
            cmd.Parameters.Add(singleParam);

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public static DataSet GetDataSetDoubleParm(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter Param1, SqlParameter Param2)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
          
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Parameters.Add(Param1);
            cmd.Parameters.Add(Param2);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public static DataSet GetDataSet(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                if (trans != null)
                {
                    cmd.Transaction = trans;
                }
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;
                PrepareCommand(cmd, cmdParms);

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();

                return ds;
            
        }             

        private static void PrepareCommand(SqlCommand cmd, SqlParameter[] cmdParms)
        {
            try
            {
                    if (cmdParms != null)
                {
                    for (int i = 0; i <= cmdParms.Length - 1; i++)
                    {
                        SqlParameter parm = (SqlParameter)cmdParms[i];
                        cmd.Parameters.Add(parm);                                                
                    }
                }
                   // cmdParms = null;===========Done By me
            }
            catch { }
        }
      

        public SQLHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}