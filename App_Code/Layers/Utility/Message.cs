using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Message
/// </summary>
/// 
namespace MayurInventory.Utility
{
    public class Message
    {
        public Message()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static Exception LastException
        {
            get
            {
                return _LastException;
            }
            set
            {
                if (value != _LastException)
                {
                    _LastException = value;
                }
            }
        }

        private static Exception _LastException;

    }
}