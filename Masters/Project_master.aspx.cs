using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MayurInventory.Utility;

public partial class Masters_Project_master : System.Web.UI.Page
{
    database db = new database();
    DataSet DS = new DataSet();
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    private static bool FlagAdd, FladDel, FlagEdit = false;
    CommanFunction obj_Comm = new CommanFunction();
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

                DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='Site Master'");
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


    private void MakeEmptyForm()
    {
        txtproject.Focus();
        if (!FlagAdd)
            BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnDelete.Visible = false;
        TxtContactNo.Text = string.Empty;
     
        txtcontactpersonname.Text = string.Empty;
        txtprojectenddate.Text = string.Empty;
        txtprojectstartdate.Text = string.Empty;
      
       
        ReportGrid(StrCondition);
    }



    public void ReportGrid(string RepCondition)
    {
        try
        {
          DataTable dt  = db.Displaygrid("select   id as #   , ProjectName, Comapanyid , Customer , ProjectstartDate , ProjectEndDate  from  Project_master");
            DS.Tables.Add(dt);
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
           
                ddlcompany.DataSource = db.Displaygrid("select 0 as CompanyId,' --Select Company--' as CompanyName union       select CompanyId, CompanyName from CompanyMaster where IsDeleted = 0");
                ddlcompany.DataValueField = "CompanyId";
                ddlcompany.DataTextField = "CompanyName";
                ddlcompany.DataBind();
          
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
        
    {
        if (!IsPostBack)
        {
           // CheckUserRight();
            MakeEmptyForm();

            ddlcustomer.DataSource = db.Displaygrid("select CustomerId, CustomerName from  CustomerMaster where  IsDeleted='"+"0"+"'");
            ddlcustomer.DataValueField = "CustomerName";
            ddlcustomer.DataTextField = "CustomerName";
            ddlcustomer.DataBind();
            ddlcustomer.Items.Insert(0, "Select Customer");



            int id = Convert.ToInt32(db.getDb_Value("select max (id) from Project_master "));
            int internalno = Convert.ToInt32(db.getDb_Value("select max (internalno) from Project_master "));
            if (internalno == 0)
            {
                internalno = 100;
            }
            else
            {
                internalno++;
            }
            id++;
            string year = "19-20";
            // String projectno = id + "/" + '"+ txtproject.Text+"' + '"+id+"';
            string projectno = id + "/ " + " " + txtproject.Text + "/" + year + "/" + internalno;

            lblunique.Text = internalno.ToString();
            lblprojectno.Text = projectno.ToString();

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
                            DataTable dt = db.Displaygrid("select *  from  Project_master where id='" + ViewState["EditID"] + "'");
                            DataSet ds1 = new DataSet();

                            ds1.Tables.Add(dt);
                            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                ddlcompany.SelectedValue = ds1.Tables[0].Rows[0]["Comapanyid"].ToString();
                                ddlcustomer.SelectedValue = ds1.Tables[0].Rows[0]["Customer"].ToString();
                                txtprojectstartdate.Text = ds1.Tables[0].Rows[0]["ProjectstartDate"].ToString();
                                txtprojectenddate.Text = ds1.Tables[0].Rows[0]["ProjectEndDate"].ToString();
                                txtcontactpersonname.Text = ds1.Tables[0].Rows[0]["ContactPerson"].ToString();
                                TxtContactNo.Text= ds1.Tables[0].Rows[0]["ContactNo"].ToString();
                                TxtPEmail.Text = ds1.Tables[0].Rows[0]["MailId"].ToString();
                                txtremark.Text = ds1.Tables[0].Rows[0]["Remark"].ToString();
                                txtproject.Text = ds1.Tables[0].Rows[0]["ProjectName"].ToString();
                                lblunique.Text = ds1.Tables[0].Rows[0]["internalno"].ToString();
                                lblprojectno.Text = ds1.Tables[0].Rows[0]["projectno"].ToString();
                            }
                            else
                            {
                                MakeEmptyForm();
                            }
                            
                           
                            DS = null;
                           
                            if (!FlagEdit)
                                BtnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            if (!FladDel)
                                BtnDelete.Visible = true;
                          
                            
                        }

                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {


        int id = Convert.ToInt32(db.getDb_Value("select max (id) from Project_master "));
        int internalno = Convert.ToInt32(db.getDb_Value("select max (internalno) from Project_master "));
        if (internalno == 0)
        {
            internalno = 100;
        }
        else
        {
            internalno++;
        }
        id++;
        string year = "19-20";
        // String projectno = id + "/" + '"+ txtproject.Text+"' + '"+id+"';
        string projectno = id + "/"+txtproject.Text + "/" + internalno + "/" + year;

        db.insert("insert into Project_master values('" + txtproject.Text + "' ,'" + ddlcompany.SelectedValue + "' ,'" + ddlcustomer.SelectedValue + "' ,'" + txtprojectstartdate.Text + "' ,'" + txtprojectenddate.Text + "' ,'" + txtcontactpersonname.Text + "' ,'" + TxtContactNo.Text + "' ,'" + TxtPEmail.Text + "' ,'" + txtremark.Text + "' ,'"+ projectno + "' ,'"+ lblunique.Text + "')");
        obj_Comm.ShowPopUpMsg("Record Saved Successfully", this.Page);
       
        MakeEmptyForm();

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

                db.insert("delete Project_master where id='" + DeleteId + "'  ");
                    obj_Comm.ShowPopUpMsg("Record Deleted Successfully..!", this.Page);
                    MakeEmptyForm();
                

            }
           
            obj_Comm = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int updateid = 0;
        if (ViewState["EditID"] != null)
        {
            updateid = Convert.ToInt32(ViewState["EditID"]);
        }

        db.insert("update  Project_master set  ProjectName='" + txtproject.Text + "' , Comapanyid='" + ddlcompany.SelectedValue + "' ,Customer='" + ddlcustomer.SelectedValue + "' ,ProjectstartDate='" + txtprojectstartdate.Text + "' , ProjectEndDate='" + txtprojectenddate.Text + "' , ContactPerson='" + txtcontactpersonname.Text + "' , ContactNo='" + TxtContactNo.Text + "' ,MailId='" + TxtPEmail.Text + "' , Remark='" + txtremark.Text + "'  where id='" + updateid + "'");
        obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
      
        MakeEmptyForm();



    }
}