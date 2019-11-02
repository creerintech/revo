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
/// Summary description for Setting
/// </summary>
namespace MayurInventory.Utility
{
    public class Setting
    {
        //Sqlconnection Tasks                                                
        public static readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["MayurInventory"].ConnectionString;

        public string CONNECTION_STRING_FUN()
        {
            string Connection = string.Empty;
            try
            {
                if (HttpContext.Current.Session["Location"] != null)
                {
                    Connection = ConfigurationManager.ConnectionStrings[Convert.ToString(HttpContext.Current.Session["Location"].ToString())].ConnectionString;
                }
                else
                {
                    Connection = ConfigurationManager.ConnectionStrings["Mumbai"].ConnectionString;
                }
            }
            catch (Exception ex)
            {
                Connection = ConfigurationManager.ConnectionStrings["Mumbai"].ConnectionString;
                return Connection;

            }
            return Connection;
        }

        // -- Define Private Connection and Transaction object for 
        public SqlConnection _Connection;
        public SqlTransaction _Transaction;
        // -- Impliments the Common Function

        public void BeginTransaction()
        {
            if (_Connection.State != ConnectionState.Open)
            {
                _Connection.Open();
            }
            _Transaction = _Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void RollBackTransaction()
        {
            if (_Transaction != null)
            {
                _Transaction.Rollback();
                _Transaction = null;
            }
        }

        public void CommitTransaction()
        {
            if (_Transaction != null)
            {
                _Transaction.Commit();
                _Transaction = null;
            }
        }

        public void Open(string connString)
        {
            if (_Connection == null && string.IsNullOrEmpty(connString) == false)
            {
                _Connection = new SqlConnection(connString);
            }
            if (_Connection.State != ConnectionState.Open)
            {
                _Connection.Open();
            }
        }
        
        public void Close()
        {
            if (_Connection != null && _Connection.State != ConnectionState.Closed)
            {
                _Connection.Close();
            }
        }

        public Setting()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}