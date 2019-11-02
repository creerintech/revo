using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using MayurInventory.Utility;
using MayurInventory.DataModel;
using MayurInventory.EntityClass;
using System.Threading;
using System.IO;
using System.Management;
using System.Text;
using System.Runtime.InteropServices;
public partial class MasterPages_MasterPage : System.Web.UI.MasterPage
{
    //#region Private Variable
    //DataSet dsLogin = new DataSet();
    //CommanFunction obj_Msg = new CommanFunction();
    //DMUserLogin obj_Login = new DMUserLogin();
    //UserLogin Entity_Login = new UserLogin();
    //private string StrError = string.Empty;
    //#endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    
    //protected void UpdateTimer_Tick(object sender, EventArgs e)
    //{
        
    //    try
    //    {
    //        dsLogin = obj_Login.GETDATAFORPOPUP(out StrError);
    //        int total = 0;
    //        if (dsLogin.Tables.Count > 0 && dsLogin.Tables[0].Rows.Count > 0)
    //        {
    //            for (int x = 0; x < GRDSHOWDATA.Rows.Count; x++)
    //            {
    //                total+= Convert.ToInt32(GRDSHOWDATA.Rows[x].Cells[1].Text.ToString());
    //            }
    //            if (total != Convert.ToInt32(dsLogin.Tables[0].Rows[0]["Total"].ToString()))
    //            {
    //                GRDSHOWDATA.DataSource = dsLogin.Tables[0];
    //                GRDSHOWDATA.DataBind();
                    
    //            }
    //            else
    //            {
    //                GRDSHOWDATA.DataSource = dsLogin.Tables[0];
    //                GRDSHOWDATA.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            obj_Msg.ShowPopUpMsg("Data Not Found....!!", this.Page);
    //        }
    //    }
    //    catch (ThreadAbortException)
    //    {
    //    }
    //    catch (Exception ex) { throw new Exception(ex.Message); }
    //}
}
