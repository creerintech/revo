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

/// <summary>
/// Summary description for ICommonDBFunction
/// </summary>
/// 
namespace MayurInventory.DB
{
    interface ICommonDBFunction
    {
        // -- Define Common function that must be included in all Bussiness class
        void BeginTransaction();
        void RollBackTransaction();
        void CommitTransaction();
        void Open(string connString);
        //void Open(string connString, bool bOpenConnection);
        void Close();
    }
}