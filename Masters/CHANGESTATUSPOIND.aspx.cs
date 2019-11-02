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

public partial class Masters_CHANGESTATUSPOIND : System.Web.UI.Page
{
    #region[Private variables]
    DMCHStatus Obj_Unit = new DMCHStatus();
    CHStatus Entity_Unit = new CHStatus();
    CommanFunction obj_Comm = new CommanFunction();
    DataSet DS = new DataSet();
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    private static bool FlagAdd, FlagDel, FlagEdit = false;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CheckUserRight();
            //MakeEmptyForm();
            BtnUpdate.Visible = false;
            string radio_btn_value = RadioButtonList1.SelectedValue;
            if (radio_btn_value == "1")
            {
                Lblststus.Text = "Po NO";
            }
            else if (radio_btn_value == "2")
            {
                Lblststus.Text = "Indent NO";
            }
        }
    }

    public void CheckUserRight()
    {
        try
        {
            #region [USER RIGHT]
            //Checking Session Varialbels========
            if (Session["UserName"] != null && Session["UserRole"] != null)
            {
                //Checking User Role========
                if (!Session["UserRole"].Equals("Administrator"))
                {
                    //Checking Right of users=======

                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    dsChkUserRight1 = (DataSet)Session["DataSet"];

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Status Update'");
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
                    //Checking View Right ========                    
                    //if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                    //{
                    //    GrdReport.Visible = false;
                    //}
                    ////Checking Add Right ========                    
                    //if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                    //{
                    //    BtnSave.Visible = false;
                    //    FlagAdd = true;
                    //}
                    //Edit /Delete Column Visible ========
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        BtnDelete.Visible = false;
                        BtnUpdate.Visible = false;
                        FlagDel = true;
                        FlagEdit = true;
                    }
                    else
                    {
                        //Checking Delete Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                        {
                            BtnDelete.Visible = false;
                            FlagDel = true;
                        }

                        //Checking Edit Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                        {
                            BtnUpdate.Visible = false;
                            FlagEdit = true;
                        }
                    }
                    dsChkUserRight.Dispose();
                }
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

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0;
        try
        {
            string radio_btn_value = RadioButtonList1.SelectedValue;
            if (radio_btn_value =="1")
            {

                Entity_Unit.Action = 1;
                Entity_Unit.PONO = txtPono.Text.Trim();

                Entity_Unit.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_Unit.LoginDate = DateTime.Now;

                UpdateRow = Obj_Unit.UpdateRecord(ref Entity_Unit, out StrError);

                if (UpdateRow != 0)
                {
                    obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                    // MakeEmptyForm();
                    txtPono.Text = string.Empty;
                    Entity_Unit = null;
                    obj_Comm = null;
                }
            }
            else if (radio_btn_value == "2")
            {
                Entity_Unit.Action = 2;
                Entity_Unit.PONO = txtPono.Text.Trim();

                Entity_Unit.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_Unit.LoginDate = DateTime.Now;

                UpdateRow = Obj_Unit.UpdateRecord(ref Entity_Unit, out StrError);

                if (UpdateRow != 0)
                {
                    obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
                    // MakeEmptyForm();
                    txtPono.Text = string.Empty;
                    Entity_Unit = null;
                    obj_Comm = null;
                }
            }
        }
        catch (Exception)
        {       
            throw;
        }
    }
    protected void BtnCheck_Click(object sender, EventArgs e)
    {
        DataSet DSC = new DataSet();
        DataSet DSC1 = new DataSet();
        string radio_btn_value = RadioButtonList1.SelectedValue;
        if (radio_btn_value == "1")
        {

            DSC1 = Obj_Unit.GetCheck(6, txtPono.Text.Trim(), out StrError);
            if (DSC1.Tables[0].Rows.Count > 0)
            {
                obj_Comm.ShowPopUpMsg("Inward Is Generated Against This PO. ", this.Page);
                BtnUpdate.Visible = false;
            }
            else
            {
                DSC = Obj_Unit.GetCheck(3, txtPono.Text.Trim(), out StrError);

                if (DSC.Tables[0].Rows.Count > 0)
                {
                    BtnUpdate.Visible = true;
                }
                else
                {
                    obj_Comm.ShowPopUpMsg("Already In Generated Mode", this.Page);
                    BtnUpdate.Visible = false;
                }
            }
        }
        else if (radio_btn_value == "2")
        {
            
             DSC1 = Obj_Unit.GetCheck(8, txtPono.Text.Trim(), out StrError);
             if (DSC1.Tables[0].Rows.Count > 0)
             {
                 obj_Comm.ShowPopUpMsg("PO Is Generated Against This Indent. ", this.Page);
                 BtnUpdate.Visible = false;
             }
             else
             {
                 DSC = Obj_Unit.GetCheck(4, txtPono.Text.Trim(), out StrError);
                 if (DSC.Tables[0].Rows.Count > 0)
                 {
                     BtnUpdate.Visible = true;
                 }
                 else
                 {
                     obj_Comm.ShowPopUpMsg("Already In Generated Mode", this.Page);
                     BtnUpdate.Visible = false;
                 }
             }
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        txtPono.Text = string.Empty;
        BtnUpdate.Visible = false;
        RadioButtonList1.SelectedIndex = 0;
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string radio_btn_value = RadioButtonList1.SelectedValue;
        if (radio_btn_value == "1")
        {
            Lblststus.Text = "Po NO";
        }
        else if (radio_btn_value == "2")
        {
            Lblststus.Text = "Indent NO";
        }
    }
}
