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
using System.Threading;
using System.IO;
using System.Management;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

public partial class Login : System.Web.UI.Page
{
    #region Private Variable

    DataSet dsLogin = new DataSet();
    CommanFunction obj_Msg = new CommanFunction();
    DMUserLogin obj_Login = new DMUserLogin();
    UserLogin Entity_Login = new UserLogin();
    private string StrError = string.Empty;
    Int64 HDDSrNo;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DateTime.Compare(DateTime.Now, Convert.ToDateTime("01-01-2020")) >= 0)//MM-dd-yyyy
        {
            Session["ERROR"] = "The Version of Software is expired, Please contact your service provider..";
            Response.Redirect("~/ErrorPages/UserAccessDenied.aspx");
        }
        if (!Page.IsPostBack)
        {
            Page.Header.DataBind();
            MakeEmptyForm();
            //LBLSERIALNO.Text = CheckSerial().Replace("-", "");
        }
    }
   
    public void MakeEmptyForm()
    {
        Session.Clear();
        DDLLoc.SelectedIndex = 0;
        TxtUserName.Text = TxtPass.Text = string.Empty;
        BindCafe();
        TxtUserName.Focus();
    }
   
    public void BindCafe()
    {
        dsLogin = new DataSet();
        dsLogin = obj_Login.GetCafe(out StrError);
        if (dsLogin.Tables.Count > 0 && dsLogin.Tables[0].Rows.Count > 0)
        {
            DDLLoc.DataSource = dsLogin.Tables[0];
            DDLLoc.DataTextField = "Cafeteria";
            DDLLoc.DataValueField = "CafeteriaId";
            DDLLoc.DataBind();
            DDLLoc.SelectedIndex = 0;
        }
        else
        {
          
        }
    }
   
    protected void Timer1_Tick(object sender, EventArgs e)
    {
    }
   
    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsCHNO = obj_Login.GetCHNO(LBLSERIALNO.Text.Trim(), out StrError);
            Entity_Login.UserName = TxtUserName.Text.Trim();
            Entity_Login.Password = TxtPass.Text.Trim();
           // Entity_Login.CafeID = Convert.ToInt32(DDLLoc.SelectedValue);
            dsLogin = obj_Login.GetLoginInfo(ref Entity_Login, out StrError);
            if (dsLogin.Tables.Count > 0 && dsLogin.Tables[0].Rows.Count > 0)
            {
                Session.Add("UserName", dsLogin.Tables[0].Rows[0]["UserName"].ToString());
                Session.Add("UserID", dsLogin.Tables[0].Rows[0]["UserID"].ToString());
                Session.Add("Password", dsLogin.Tables[0].Rows[0]["Password"].ToString());
                Session.Add("CafeteriaId", dsLogin.Tables[2].Rows[0]["SiteID"].ToString());
                Session.Add("CafeteriaNo", dsLogin.Tables[2].Rows[0]["Site"].ToString());
                Session.Add("Location", DDLLoc.SelectedItem.ToString());
                Session.Add("SiteID", dsLogin.Tables[2].Rows[0]["SiteID"].ToString());
                Session.Add("SiteName", dsLogin.Tables[2].Rows[0]["SiteID"].ToString());
                Session.Add("IsCentral", dsLogin.Tables[0].Rows[0]["IsCentral"].ToString());



                Session["mob"] = dsLogin.Tables[0].Rows[0]["mob"].ToString();
                //Session.Add("FinStartDate", dsLogin.Tables[3].Rows[0]["StartDate"].ToString());
                //Session.Add("FinEndDate", dsLogin.Tables[3].Rows[0]["StartDate"].ToString());
                if (Convert.ToBoolean(dsLogin.Tables[0].Rows[0]["IsAdmin"].ToString()) == true)
                {
                    Session.Add("UserRole", "Administrator");
                }
                else
                {
                    Session.Add("UserRole", "User");
                }
                if (dsLogin.Tables[1].Rows.Count > 0)
                {
                    Session.Add("DataSet", dsLogin);
                }
                if (dsLogin.Tables[4].Rows.Count > 0)
                {
                    Session.Add("DataSetSpecialPermission", dsLogin);
                }


      
               
                //Session.Add("FinStartDate1", Convert.ToDateTime(dsLogin.Tables[3].Rows[0]["StartDate"].ToString()).ToString("dd-MM-yyyy"));
                //Session.Add("FinEndDate1", Convert.ToDateTime(dsLogin.Tables[3].Rows[0]["EndDate"].ToString()).ToString("dd-MM-yyyy"));
                Response.Redirect("~/Masters/Default.aspx");
            }
            else
            {
                obj_Msg.ShowPopUpMsg("Invalid Login....!!", this.Page);
                MakeEmptyForm();
            }

        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
   
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
    
    //private string identifier(string wmiClass, string wmiProperty)
    //{
    //    string result = "";
    //    System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
    //    System.Management.ManagementObjectCollection moc = mc.GetInstances();
    //    foreach (System.Management.ManagementObject mo in moc)
    //    {
    //        if (result == "")
    //        {
    //            try
    //            {
    //                result = mo[wmiProperty].ToString();
    //                break;
    //            }
    //            catch
    //            {
    //            }
    //        }

    //    }
    //    return result;
    //}

   

        
    //public static string  CheckSerial()
    //{
    //    string res = ExecuteCommandSync("vol");
    //    const string search = "Number is";
    //    int startI = res.IndexOf(search, StringComparison.InvariantCultureIgnoreCase);
    //    string currentDiskID = "";
    //    if (startI > 0)
    //    {
    //        currentDiskID = res.Substring(startI + search.Length).Trim();

    //    }
    //    return currentDiskID;
    //}

    public static string ExecuteCommandSync(object command)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo procStartInfo =
                new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                string result = proc.StandardOutput.ReadToEnd();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DMUserLogin Obj_CN = new DMUserLogin();
            String[] SearchList = Obj_CN.GetSuggestedRecord(prefixText);
            return SearchList;
        }
       
}
