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
using MayurInventory.Utility;
using MayurInventory.DataModel;
using MayurInventory.EntityClass;
public partial class Masters_Default : System.Web.UI.Page
{
    #region [Declare Variables]------------
    DataSet dsLogin = new DataSet();
    CommanFunction obj_Msg = new CommanFunction();
    DMUserLogin obj_Login = new DMUserLogin();
    UserLogin Entity_Login = new UserLogin();
    private string StrError = string.Empty;
    #endregion ----------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetLocation();
            Session["TransactionSiteID"] = RdoSite.SelectedValue;
            Session["TransactionSiteName"] = RdoSite.SelectedItem;
            Session["TransactionSite"] = RdoSite.SelectedItem;
            Session.Add("CafeteriaId", RdoSite.SelectedValue);
            Session.Add("CafeteriaNo", RdoSite.SelectedItem);
            Session.Add("Location",RdoSite.SelectedItem);
        }
    }

    #region [For User Define Function]-----
    private void GetLocation()
    {
        if (!string.IsNullOrEmpty(Session["SiteID"].ToString()))
        {
            String LocationIDs = " AND StockLocationID IN ( " + Session["SiteID"].ToString() + " )";
            dsLogin = new DataSet();
            dsLogin = obj_Login.GetLocationAccordingToUser(LocationIDs, out StrError);
            if (dsLogin.Tables.Count > 0 && dsLogin.Tables[0].Rows.Count > 0)
            {
                RdoSite.DataSource = dsLogin.Tables[0];
                RdoSite.DataTextField = "Location";
                RdoSite.DataValueField = "StockLocationID";
                RdoSite.DataBind();
                RdoSite.SelectedIndex = 0;
            }
            else
            {

            }
        }
        else
        {
            obj_Msg.ShowPopUpMsg("No Location Details Found For This User, You Can't Make Transaction, Please Update User Master..", this.Page);
        }
    }
    #endregion-----------------------------
    protected void RdoSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["TransactionSiteID"] = RdoSite.SelectedValue;
        Session["TransactionSiteName"] = RdoSite.SelectedItem;
        Session["TransactionSite"] = RdoSite.SelectedItem;
        Session.Add("CafeteriaId", RdoSite.SelectedValue);
        Session.Add("CafeteriaNo", RdoSite.SelectedItem);
        Session.Add("Location", RdoSite.SelectedItem);
    }
}
