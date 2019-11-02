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

using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using MayurInventory.DALSQLHelper;

public partial class Masters_ProrityMaster : System.Web.UI.Page
{
    #region[Private variables]
        DMChangePassword Obj_PR = new DMChangePassword();
        CommanFunction obj_Comm = new CommanFunction();
        DataSet DS = new DataSet();
        private string StrCondition = string.Empty;
        private string StrError = string.Empty;
    #endregion

    #region[UserDefine Function]
    private void MakeEmptyForm()
    {
        OPassword.Focus();
        CmpOPassword.Text=CPassword.Text = OPassword.Text = Password.Text = string.Empty;
        ReportGrid(StrCondition);
    }
    public void ReportGrid(string RepCondition)
    {
        try
        {
            DS = Obj_PR.GetUserForEdit(Convert.ToInt32(Session["UserID"].ToString()), out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                LblUserName.Text = DS.Tables[0].Rows[0]["Name"].ToString();
                LblLocation.Text = DS.Tables[0].Rows[0]["Location"].ToString();
                CmpOPassword.Text = DS.Tables[0].Rows[0]["Pass"].ToString();
            }
            else
            {
               
            }
            Obj_PR = null;
            DS = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MakeEmptyForm();
        }
    }
 
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        int UpdateRow = 0;
        try
        {
            UpdateRow = Obj_PR.UpdatePassword(Convert.ToInt32(Session["UserID"].ToString()), OPassword.Text, Password.Text, Convert.ToInt32(Session["UserID"].ToString()), out StrError);
            if (UpdateRow != 0)
            {
                obj_Comm.ShowPopUpMsg("Password Change Successfully", this.Page);
                MakeEmptyForm();
                obj_Comm = null;
            }
            }
 
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
 
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
   
    

   
}
