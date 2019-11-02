using MayurInventory.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_pipelenght : System.Web.UI.Page
{
    database db = new database();
    private string StrCondition = string.Empty;
    private string StrError = string.Empty;
    private static bool FlagAdd, FladDel, FlagEdit = false;
    CommanFunction obj_Comm = new CommanFunction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CheckUserRight();
            MakeEmptyForm();
        }
    }
    private void MakeEmptyForm()
    {
        if (!FlagAdd)
            BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        BtnDelete.Visible = false;
        txtlenth.Text = string.Empty;
        TxtSearch.Text = string.Empty;
        ReportGrid(StrCondition);
        txtlenth.Focus();
    }
    public void ReportGrid(string RepCondition)
    {
        try
        {

            GrdReport.DataSource = db.Displaygrid("select id as # , length  from length");
            GrdReport.DataBind();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {

        try
        {
            db.insert("insert into length values('" + txtlenth.Text + "')");
            MakeEmptyForm();

            obj_Comm.ShowPopUpMsg("Record Saved Successfully", this.Page);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        int id = 0;
        try
        {
            if (ViewState["EditID"] != null)
            {
                id = Convert.ToInt32(ViewState["EditID"]);
            }

            db.insert("update length set length='" + txtlenth.Text + " ' where id='" + id + "' ");

            obj_Comm.ShowPopUpMsg("Record Updated Successfully", this.Page);
            MakeEmptyForm();

            obj_Comm = null;


        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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

                db.insert("delete  length where id='" + DeleteId + "'");

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
                            DataTable dt = db.Displaygrid("select * from length where id='" + ViewState["EditID"] + "' ");
                            DataSet DS = new DataSet();
                            DS.Tables.Add(dt);




                            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                            {
                                txtlenth.Text = DS.Tables[0].Rows[0]["length"].ToString();
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
                            txtlenth.Focus();
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
    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        StrCondition = TxtSearch.Text.Trim();
        ReportGrid(StrCondition);
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
}