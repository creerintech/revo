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
using MayurInventory.DALSQLHelper;
using MayurInventory.DataModel;
using MayurInventory.DB;
using MayurInventory.EntityClass;
using MayurInventory.Utility;

public partial class Reports_IssueRegisterDetailsReport : System.Web.UI.Page
{
   #region[Private Variables]
        DMIssueRegister Obj_IssueRegister = new DMIssueRegister();
        IssueRegister Entity_IssueRegister = new IssueRegister();
        CommanFunction Obj_Comm = new CommanFunction();
        private string StrCondition=string.Empty;
        private string StrError = string.Empty;
        private static bool FlagPrint = false;
        DataSet DS = new DataSet();
    #endregion

   #region[User Defined Function]
   private void SetInitialRow()
    {
        DataTable Dt=new DataTable();
        DataRow dr;

        Dt.Columns.Add("IssueNo",typeof(string));
        Dt.Columns.Add("IssueDate",typeof(string));
        Dt.Columns.Add("ItemName", typeof(string));
        Dt.Columns.Add("Qty",typeof(decimal));
        Dt.Columns.Add("IssueQty",typeof(decimal));
        Dt.Columns.Add("PendingQty",typeof(decimal));
        Dt.Columns.Add("Cafeteria", typeof(string));
        Dt.Columns.Add("EmpName",typeof(string));
        Dt.Columns.Add("Notes",typeof(string));

        dr=Dt.NewRow();

        dr["IssueNo"] = "";
        dr["IssueDate"] = "";
        dr["ItemName"] = "";
        dr["Qty"] = 0;
        dr["IssueQty"] = 0;
        dr["PendingQty"] = 0;
        dr["Cafeteria"] = "";
        dr["EmpName"] = "";
        dr["Notes"] = "";
        
       Dt.Rows.Add(dr);

       ViewState["CurrentTable"] = Dt;
       GridDetails.DataSource = Dt;
       GridDetails.DataBind();
    }

   public void MakeEmptyForm()
   {
       TxtFromDate.Enabled = TxtToDate.Enabled = true;
       TxtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
       TxtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
       ChkFromDate.Checked = true;
       ddlIssueNo.SelectedIndex = ddlItem.SelectedIndex = ddlLocation.SelectedIndex = 0;
       SetInitialRow();
       ReportGrid();
   }

   public void FillCombo()
   {
       try
       {
           DS = Obj_IssueRegister.FillComboForReport(Convert.ToInt32(Session["UserID"]),out StrError);
           {
                if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlIssueNo.DataSource = DS.Tables[0];
                    ddlIssueNo.DataTextField = "IssueNo";
                    ddlIssueNo.DataValueField = "#";
                    ddlIssueNo.DataBind();
                }
                if (DS.Tables[1].Rows.Count > 0)
                {
                    ddlEmp.DataSource = DS.Tables[1];
                    ddlEmp.DataTextField = "Name";
                    ddlEmp.DataValueField = "#";
                    ddlEmp.DataBind();
                }
                    if(DS.Tables[4].Rows.Count>0)
                    {
                        ddlItem.DataSource=DS.Tables[4];
                        ddlItem.DataTextField="Item";
                        ddlItem.DataValueField="#";
                        ddlItem.DataBind();
                    }
                    if(DS.Tables[5].Rows.Count>0)
                    {
                        ddlLocation.DataSource=DS.Tables[5];
                        ddlLocation.DataTextField="Cafeteria";
                        ddlLocation.DataValueField="#";
                        ddlLocation.DataBind();
                    }
            }
            else
            {
                DS = null;
            }
           }
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
 
   }

   public bool ChkDateFormat()
   {
       bool flag = false;
       try
       {
           if (ChkFromDate.Checked == true)
           {
               if (!string.IsNullOrEmpty(TxtFromDate.Text) && !string.IsNullOrEmpty(TxtToDate.Text))
               {
                   if ((Convert.ToDateTime(TxtFromDate.Text) < DateTime.Now) && (Convert.ToDateTime(TxtToDate.Text) < DateTime.Now))
                   {
                       if (Convert.ToDateTime(TxtFromDate.Text) < Convert.ToDateTime(TxtToDate.Text))
                       {
                           flag = true;
                       }
                       else
                       {
                           Obj_Comm.ShowPopUpMsg("From Date Must Be Than To Date...", this.Page);
                       }
                   }
                   else
                   {
                       Obj_Comm.ShowPopUpMsg("Please Check Date..", this.Page);
                   }
               }
               else
               {
                   Obj_Comm.ShowPopUpMsg("Please Select Date..", this.Page);
               }
           }
           else
           {
               flag = true;
           }
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
       return flag;
   }

   public void ReportGrid()
   {
       StrCondition = string.Empty;
      
       try
       {
           if (ChkFromDate.Checked)
           {
               StrCondition += " and IR.IssueDate Between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "'and'" +
                   Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
           }
           if (!ChkFromDate.Checked)
           {
               StrCondition += " and IR.IssueDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" + 
                   Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
           }
           if (Convert.ToInt32(ddlIssueNo.SelectedValue) > 0)
           {
               StrCondition += " and IR.IssueRegisterId=" + Convert.ToInt32(ddlIssueNo.SelectedValue);
           }
           if (Convert.ToInt32(ddlItem.SelectedValue) > 0)
           {
               StrCondition += " and IM.ItemId=" + Convert.ToInt32(ddlItem.SelectedValue);
           }
           if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
           {
               StrCondition += " and SL.StockLocationID=" + Convert.ToInt32(ddlLocation.SelectedValue);
           }
           DS = Obj_IssueRegister.ShowIssueRegisterDetailsReport(StrCondition, out StrError);

               if(DS.Tables.Count>0&&DS.Tables[0].Rows.Count>0)
               {
                   if(!FlagPrint)
                   ImgBtnPrint.Visible=true;
                   GridDetails.DataSource=DS.Tables[0];
                   GridDetails.DataBind();
                   lblCount.Text=DS.Tables[0].Rows.Count.ToString()+"Record Found";
                   DS=null;
                   //ScriptManager.RegisterStartupScript(this,this.GetType(),"starScript","")
               }
           else
               {
                   GridDetails.DataSource=DS.Tables[0];
                   GridDetails.DataBind();
                   lblCount.Text="";
                   DS=null;
                   SetInitialRow();
                   ImgBtnPrint.Visible=false;
               }
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
               if (!Session["UserRole"].Equals("Administrator"))
               {
                   //Checking Right of users=======

                   System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                   System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                   dsChkUserRight1 = (DataSet)Session["DataSet"];

                   DataRow[] dtRow = dsChkUserRight1.Tables[1].Select("FormName ='IssueRegisterDetailsReport'");
                   if (dtRow.Length > 0)
                   {
                       DataTable dt = dtRow.CopyToDataTable();
                       dsChkUserRight.Tables.Add(dt);
                   }
                   if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                       && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                   {
                       Response.Redirect("~/Masters/NotAuthUser.aspx");
                   }
                   //Checking View Right ========                    
                   if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false)
                   {
                       BtnShow.Visible = false;
                   }
                   //Checking Print Right ========                    
                   if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                   {
                       ImgBtnPrint.Visible = false;
                       ImgBtnExcel.Visible = false;
                       FlagPrint = true;
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
   //User Right Function==========  

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       if(!IsPostBack)
        {
            FillCombo();
            CheckUserRight();
            MakeEmptyForm();           
        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        if (ChkDateFormat() == true)
        {
            ReportGrid();
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MakeEmptyForm();
    }
    protected void ChkFromDate_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkFromDate.Checked == true)
        {
            TxtFromDate.Enabled = TxtToDate.Enabled = true;
        }
        else
        {
            TxtFromDate.Enabled = TxtToDate.Enabled = false;
        }
    }
    protected void ImgBtnExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ChkFromDate.Checked == true)
            {
                StrCondition += " and IR.IssueDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy")
                    + " 'and'" + Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlIssueNo.SelectedValue) > 0)
            {
                StrCondition += " and IR.IssueRegisterId=" + Convert.ToInt32(ddlIssueNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlItem.SelectedValue) > 0)
            {
                StrCondition += " and IM.ItemId=" + Convert.ToInt32(ddlItem.SelectedValue);
            }
            if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
            {
                StrCondition += " and IRD.StockLocationID=" + Convert.ToInt32(ddlLocation.SelectedValue);
            }
            if (Convert.ToInt32(ddlEmp.SelectedValue) > 0)
            {
                StrCondition += " and EM.EmployeeId=" + Convert.ToInt32(ddlEmp.SelectedValue);
            }
            DS = Obj_IssueRegister.ShowIssueRegisterDetailsReport(StrCondition, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                GridView GridExp = new GridView();
                GridExp.DataSource = DS.Tables[0];
                GridExp.DataBind();
                Obj_Comm.Export("Material Issued Report.xls", GridExp);
            }
            else
            {
                Obj_Comm.ShowPopUpMsg("No Data Found To Export...!", this.Page);
                DS.Dispose();
                GridDetails.DataSource = null;
                GridDetails.DataBind();
            }
            DS = null;
        }
        catch (ThreadAbortException tex)
        {

        }
        catch (Exception ex)
        {
            Obj_Comm.ShowPopUpMsg(ex.Message, this.Page);
        }
    }

    //========Important Required for Excel Export Must=============
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    //========Important Required for Excel Export Must=============

    protected void GridDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.GridDetails.PageIndex = e.NewPageIndex;
            DataSet DS = new DataSet();
            StrCondition = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;
            if (ChkFromDate.Checked)
            {
                StrCondition += " and IR.IssueDate Between '" + Convert.ToDateTime(TxtFromDate.Text).ToString("MM-dd-yyyy") + "'and'" +
                    Convert.ToDateTime(TxtToDate.Text).ToString("MM-dd-yyyy") + "'";
            }
            if (!ChkFromDate.Checked)
            {
                StrCondition += " and IR.IssueDate Between '" + Convert.ToDateTime("01-Jan-1990").ToString("MM-dd-yyyy") + "' and '" +
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM-dd-yyyy") + "'";
            }
            if (Convert.ToInt32(ddlIssueNo.SelectedValue) > 0)
            {
                StrCondition += " and IR.IssueRegisterId=" + Convert.ToInt32(ddlIssueNo.SelectedValue);
            }
            if (Convert.ToInt32(ddlItem.SelectedValue) > 0)
            {
                StrCondition += " and IM.ItemId=" + Convert.ToInt32(ddlItem.SelectedValue);
            }
            if (Convert.ToInt32(ddlLocation.SelectedValue) > 0)
            {
                StrCondition += " and SL.StockLocationID=" + Convert.ToInt32(ddlLocation.SelectedValue);
            }
            DS = Obj_IssueRegister.ShowIssueRegisterDetailsReport(StrCondition, out StrError);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                ImgBtnPrint.Visible = true;
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
                lblCount.Text = DS.Tables[0].Rows.Count.ToString() + "Record Found";
                DS = null;
                //ScriptManager.RegisterStartupScript(this,this.GetType(),"starScript","")
            }
            else
            {
                GridDetails.DataSource = DS.Tables[0];
                GridDetails.DataBind();
                lblCount.Text = "";
                DS = null;
                SetInitialRow();
                ImgBtnPrint.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
