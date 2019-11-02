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
using System.Net;

using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using System.IO;
using System.Threading;

#region [DataBase Backup]
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;
using Microsoft.SqlServer.Management.Smo.Broker;
using Microsoft.SqlServer.Management.Smo.Mail;
using Microsoft.SqlServer.Management.Smo.RegisteredServers;
using System.Net;
using System.ComponentModel;
#endregion

public partial class Utility_BackupData : System.Web.UI.Page
{
    #region [Variable]-----------------
    CommanFunction obj_Comman = new CommanFunction();
    string StrCondition = string.Empty;
    DMBackup Obj_Backup = new DMBackup();
    Backup Entity_Backup = new Backup();
    private string StrError = string.Empty;

     //Metioned here your database name
    string dbname = "MayurInventory";
    SqlConnection sqlcon=new SqlConnection();
    SqlCommand sqlcmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataTable dt = new DataTable();
    public static bool FlagAdd = false;

    #endregion

    #region[UserDefineFunction]
    public void MakeFormEmpty()
    {
        lblDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
    }
    //User Right Function===========
    public void CheckUserRight()
    {
        try
        {
            #region [USER RIGHT]
            //Checking Session Varialbels========
            if (Session["UserName"] != null && Session["UserRole"] != null)
            {
                //Checking User Role========
                //if (!Session["UserRole"].Equals("Administrator"))
                //{
                //Checking Right of users=======

                System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                dsChkUserRight1 = (DataSet)Session["DataSet"];

                DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Database Backup'");
                if (dtRow.Length > 0)
                {
                    DataTable dt = dtRow.CopyToDataTable();
                    dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                }
                if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                    Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                {
                    Response.Redirect("~/NotAuthUser.aspx");
                }

                //Checking Add Right ========                    
                if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                {
                    BtnBackup.Visible = false;
                    FlagAdd = true;
                }
                dsChkUserRight.Dispose();
                //}
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
            #endregion
        }
        catch (ThreadAbortException)
        {
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //User Right Function===========
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MakeFormEmpty();
            CheckUserRight();
        }
    }
    protected void BtnBackup_Click(object sender, EventArgs e)
    {
        DataSet DS = new DataSet();
        string Date = DateTime.Now.ToString("dd-MMM-yyyy :HHmmss");
         //Enter destination directory where backup file stored
        string destdir = "~\\TempFiles\\SWDATA"+Date;
        Date=destdir;
        if (!System.IO.Directory.Exists(destdir))
        {
            System.IO.Directory.CreateDirectory("E:\\backupdb");
        }
        try
        {
            DS = Obj_Backup.CreateBU(destdir,Date, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                 string str=DS.Tables[0].Rows[0]["Path"].ToString();
                 WebClient webClient = new WebClient();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void Completed(object sender, AsyncCompletedEventArgs e)
    {
        obj_Comman.ShowPopUpMsg("Download completed!", this.Page);
    }
    protected void BTNDELETEFILE_Click(object sender, EventArgs e)
    {
        try
        {
        DataSet DS = new DataSet();
        string destdir = @"C:\Windows\Microsoft.NET\Framework\v2.0.50727\Temporary ASP.NET Files";
        string Deldestdir = @"C:\Windows\Microsoft.NET\Framework\v2.0.50727\Temporary ASP.NET Files";
        if (System.IO.Directory.Exists(destdir))
        {
            System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(destdir);
            CleanDirectory(downloadedMessageInfo);
            //downloadedMessageInfo.Delete(true);
            //File.GetAccessControl(destdir);
            //File.Delete(destdir);
            //string[] fileEntries = Directory.GetFiles(destdir);
            //foreach (string fileName in fileEntries)
            //{
            //    Deldestdir=destdir+@"\"+fileName;
            //    File.Delete(Deldestdir);
            //}
           
        }
        
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static void CleanDirectory(DirectoryInfo di)
    {
        if (di == null)
            return;

        foreach (FileSystemInfo fsEntry in di.GetFileSystemInfos())
        {
            CleanDirectory(fsEntry as DirectoryInfo);
            fsEntry.Delete();
        }
        WaitForDirectoryToBecomeEmpty(di);
    }

    private static void WaitForDirectoryToBecomeEmpty(DirectoryInfo di)
    {
        for (int i = 0; i < 5; i++)
        {
            if (di.GetFileSystemInfos().Length == 0)
                return;
            Thread.Sleep(50 * i);
        }
    }
    
}
