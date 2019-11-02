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

using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DALSQLHelper;
using MayurInventory.DataModel;

public partial class Masters_EmployeeMaster : System.Web.UI.Page
{
    #region[Private Variables]
    DMEmployee Obj_EM = new DMEmployee();
    EmployeeMaster Entity_EM = new EmployeeMaster();
    CommanFunction obj_Comm = new CommanFunction();
    DataSet DS = new DataSet();
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    private static bool FlagAdd, FladDel, FlagEdit = false;
    #endregion

    #region[userDefine Function]

    private void MakeEmptyForm()
    {
        TxtEmpName.Focus();
        if(!FlagAdd)
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnDelete.Visible = false;
        TxtEmpName.Text = string.Empty;
        TxtEmpAddress.Text = string.Empty;
        TxtTelNo.Text = string.Empty;
        TxtMobileNo.Text = string.Empty;
        TxtEmail.Text = string.Empty;
        TxtNotes.Text = string.Empty;
        TxtSearch.Text = string.Empty;
        txtDesignation.Text = string.Empty;
        txtDepartment.Text = string.Empty;
        ReportGrid(StrCondition);
    }

    public void ReportGrid(string RepCondition)
    {
        try
        {
            DS = Obj_EM.GeteEmployee(RepCondition, out StrError);

            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GrdReport.DataSource = DS.Tables[0];
                GrdReport.DataBind();
            }
            else
            {
                GrdReport.DataSource = null;
                GrdReport.DataBind();
            }
            Obj_EM = null;
            DS = null;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

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

                    DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Employee Master'");
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
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                    {
                        GrdReport.Visible = false;
                    }
                    //Checking Add Right ========                    
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false)
                    {
                        BtnSave.Visible = false;
                        FlagAdd = true;
                    }
                    //Edit /Delete Column Visible ========
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        BtnDelete.Visible = false;
                        BtnUpdate.Visible = false;
                        FladDel = true;
                        FlagEdit = true;
                    }
                    else
                    {
                        //Checking Delete Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false)
                        {
                            BtnDelete.Visible = false;
                            FladDel = true;
                        }

                        //Checking Edit Right ========
                        if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                        {
                            BtnUpdate.Visible = false;
                            FlagEdit = true;
                        }
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
            CheckUserRight();
            MakeEmptyForm();
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int InsertRow = 0;
        try
        {
            DS = Obj_EM.ChkDuplicate(TxtEmpName.Text.Trim(), out StrError);

            if (DS.Tables[0].Rows.Count > 0)
            {
                obj_Comm.ShowPopUpMsg("Employee Name Already Exist !!!", this.Page);
                TxtEmpName.Focus();
            }
            else
            {
                Entity_EM.EmpName = TxtEmpName.Text.Trim();
                Entity_EM.Address = TxtEmpAddress.Text.Trim();
                Entity_EM.TelNo = TxtTelNo.Text.Trim();
                Entity_EM.Email = TxtEmail.Text.Trim();
                Entity_EM.Note = TxtNotes.Text.Trim();
                Entity_EM.MobileNo = TxtMobileNo.Text.Trim();
                Entity_EM.Department = txtDepartment.Text.Trim();
                Entity_EM.Designation = txtDesignation.Text.Trim();
                Entity_EM.UserId = Convert.ToInt32(Session["UserId"]);
                Entity_EM.LoginDate = DateTime.Now;
                Entity_EM.IsDeleted = false;
                InsertRow = Obj_EM.InsertRecord(ref Entity_EM, out StrError);

                if (InsertRow != 0)
                {
                    obj_Comm.ShowPopUpMsg("Record Saved Successfully!!", this.Page);
                    MakeEmptyForm();
                    Entity_EM = null;
                    obj_Comm = null;
                }
            }
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int UpdateRow = 0;

        try
        {
            if (ViewState["EditID"] != null)
            {
                Entity_EM.EmployeeId = Convert.ToInt32(ViewState["EditID"]);

            }
            Entity_EM.EmpName = TxtEmpName.Text.Trim();
            Entity_EM.Address = TxtEmpAddress.Text.Trim();
            Entity_EM.TelNo = TxtTelNo.Text.Trim();
            Entity_EM.MobileNo = TxtMobileNo.Text.Trim();
            Entity_EM.Email = TxtEmail.Text.Trim();
            //Entity_EM.Department = txtDepartment.Text.Trim();
            //Entity_EM.Designation = txtDesignation.Text.Trim();
            Entity_EM.Note = TxtNotes.Text.Trim();
            Entity_EM.UserId = Convert.ToInt32(Session["UserId"]);
            Entity_EM.LoginDate = DateTime.Now;

            UpdateRow = Obj_EM.UpdateRecord(ref Entity_EM, out StrError);

            if (UpdateRow != 0)
            {
                obj_Comm.ShowPopUpMsg("Record Updated Successfully!!", this.Page);
                MakeEmptyForm();
                Entity_EM = null;
                obj_Comm = null;
            }
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int DeleteId = 0;
            if (ViewState["EditID"] != null)
            {
                DeleteId = Convert.ToInt32(ViewState["EditID"]);
            }
            if (DeleteId != 0)
            {
                Entity_EM.EmployeeId = DeleteId;
                Entity_EM.UserId = Convert.ToInt32(Session["UserID"]);
                Entity_EM.LoginDate = DateTime.Now;
                Entity_EM.IsDeleted = true;

                int iDelete = Obj_EM.DeleteRecord(ref Entity_EM, out StrError);

                if (iDelete != 0)
                {
                    obj_Comm.ShowPopUpMsg("Record Deleted SuccessFully..!", this.Page);
                    MakeEmptyForm();

                }
                Entity_EM = null;
                obj_Comm = null;
            }
        }
        catch (Exception ex)
        {
            StrError = ex.Message;
        }


    }

    protected void GrdReport_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try 
        {
            switch (e.CommandName)
            {
                case ("Select"):
                    {
                        if (Convert.ToInt32(e.CommandArgument) != 0)
                        {
                            ViewState["EditID"] = Convert.ToInt32(e.CommandArgument);
                            DS = Obj_EM.GetEmployeeForEdit(Convert.ToInt32(e.CommandArgument), out StrError);

                            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                            {
                                TxtEmpName.Text = DS.Tables[0].Rows[0]["EmpName"].ToString();
                                TxtEmpAddress.Text = DS.Tables[0].Rows[0]["Address"].ToString();
                                TxtTelNo.Text = DS.Tables[0].Rows[0]["TelNo"].ToString();
                                TxtMobileNo.Text = DS.Tables[0].Rows[0]["MobileNo"].ToString();
                                TxtEmail.Text = DS.Tables[0].Rows[0]["Email"].ToString();
                                TxtNotes.Text = DS.Tables[0].Rows[0]["Note"].ToString();
                                //txtDepartment.Text = DS.Tables[0].Rows[0]["Department"].ToString();
                                //txtDesignation.Text = DS.Tables[0].Rows[0]["Designation"].ToString();
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            DS=null;
                            Obj_EM=null;
                            if(!FlagEdit)
                            BtnUpdate.Visible=true;
                            BtnSave.Visible=false;
                            if (!FladDel)
                            BtnDelete.Visible=true;
                            TxtEmpName.Focus();

                        }
                        break;
                    }
            }
        }
        catch(Exception ex)
        {
            StrError=ex.Message;

        }
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        ReportGrid(StrCondition);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMEmployee Obj_EM = new DMEmployee();
        string[] SearchList = Obj_EM.GetSuggestRecord(prefixText);
        return SearchList;
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
}
