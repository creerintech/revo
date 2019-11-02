using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Threading;
public partial class Controls_LogOutControl : System.Web.UI.UserControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoServerCaching();
        HttpContext.Current.Response.Cache.SetNoStore();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {

        }

    }
    protected void Label1_Init(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            Label1.Text = " " + Session["UserName"].ToString();
        }
        else
        {
            Label1.Text = " ";
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["UserName"] = null;
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
        Response.Redirect("~/Default.aspx");

    }
}
