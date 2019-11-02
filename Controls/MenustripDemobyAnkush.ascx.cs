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

public partial class Controls_MenustripDemobyAnkush : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] p = (HttpContext.Current.Request.Url.AbsolutePath).Split('/');
        string path1 =p[(p.Length)-1];

        #region[NewUserRights]
            string str = Session["UserRole"].ToString();
            if (Session["UserName"] != null && Session["UserRole"] != null)
            {
                    //Checking User Role========
                    //if (!Session["UserRole"].Equals("User"))
                    //{
                    //Checking Right of users=======
                    System.Data.DataSet dsChkUserRight = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRight1 = new System.Data.DataSet();
                    System.Data.DataSet dsChkUserRightSP = new System.Data.DataSet(); 
                    dsChkUserRight1 = (DataSet)Session["DataSet"];
                    dsChkUserRightSP = (DataSet)Session["DataSetSpecialPermission"];
                    //-------Masters-----
                    #region[For User Master]
                    DataRow[] dtRow1 = dsChkUserRight1.Tables[1].Select("FormName ='User Master'");
                    if (dtRow1.Length > 0)
                    {
                        DataTable dt = dtRow1.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A1.Visible = false;
                    }
                    #endregion
                    #region[For Company Master]
                    DataRow[] dtRow58 = dsChkUserRight1.Tables[1].Select("FormName ='Company Master'");
                    if (dtRow58.Length > 0)
                    {
                        DataTable dt = dtRow58.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A38.Visible = false;
                    }
                    #endregion
                    #region[For Supplier Master]
                    DataRow[] dtRow2 = dsChkUserRight1.Tables[1].Select("FormName ='Supplier Master'");
                    if (dtRow2.Length > 0)
                    {
                        DataTable dt = dtRow2.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A2.Visible = false;
                    }
                    #endregion
                    #region[For Employee Master]
                    DataRow[] dtRow3 = dsChkUserRight1.Tables[1].Select("FormName ='Employee Master'");
                    if (dtRow3.Length > 0)
                    {
                        DataTable dt = dtRow3.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                       // Response.Redirect("~/NotAuthUser.aspx");
                        A3.Visible = false;
                    }
                    #endregion
                    #region[For Site Master]
                    DataRow[] dtRow4 = dsChkUserRight1.Tables[1].Select("FormName ='Site Master'");
                    if (dtRow4.Length > 0)
                    {
                        DataTable dt = dtRow4.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A4.Visible = true;
                    }
                    #endregion
                    #region[For Sub Site Master]
                    DataRow[] dtRow5 = dsChkUserRight1.Tables[1].Select("FormName ='Sub Site Master'");
                    if (dtRow5.Length > 0)
                    {
                        DataTable dt = dtRow5.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A45.Visible = false;
                    }
                    #endregion
                    #region[For Item Category Master]
                    DataRow[] dtRow6 = dsChkUserRight1.Tables[1].Select("FormName ='Item Category Master'");
                    if (dtRow6.Length > 0)
                    {
                        DataTable dt = dtRow6.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A5.Visible = false;
                    }
                    #endregion
                    #region[For Item Sub Categoty Master]
                    DataRow[] dtRow8 = dsChkUserRight1.Tables[1].Select("FormName ='Item Sub Categoty Master'");
                    if (dtRow8.Length > 0)
                    {
                        DataTable dt = dtRow8.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A7.Visible = false;
                    }
                    #endregion
                    #region[For Unit Master]
                    DataRow[] dtRow7 = dsChkUserRight1.Tables[1].Select("FormName ='Unit Master'");
                    if (dtRow7.Length > 0)
                    {
                        DataTable dt = dtRow7.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A6.Visible = false;
                    }
                    #endregion
                    #region[For Priority Master]
                    DataRow[] dtRow9 = dsChkUserRight1.Tables[1].Select("FormName ='Priority Master'");
                    if (dtRow9.Length > 0)
                    {
                        DataTable dt = dtRow9.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A25.Visible = false;
                    }
                    #endregion
                    #region[For Item Master]
                    DataRow[] dtRow10 = dsChkUserRight1.Tables[1].Select("FormName ='Item Master'");
                    if (dtRow10.Length > 0)
                    {
                        DataTable dt = dtRow10.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A8.Visible = false;
                    }
                    #endregion
                    #region[For Terms & Condition Master]
                    DataRow[] dtRow11 = dsChkUserRight1.Tables[1].Select("FormName ='Terms & Condition Master'");
                    if (dtRow11.Length > 0)
                    {
                        DataTable dt = dtRow11.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A24.Visible = false;
                    }
            #endregion

            //-----Transaction------
            #region[Generate Enquiry]
            DataRow[] dtRow200 = dsChkUserRight1.Tables[1].Select("FormName ='Generate Enquiry'");
                    if (dtRow200.Length > 0)
                    {
                        DataTable dt = dtRow200.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                // Response.Redirect("~/NotAuthUser.aspx");
                A67.Visible = false;
                    }
            #endregion


            #region[Supplier quotation]
            DataRow[] dtRow201 = dsChkUserRight1.Tables[1].Select("FormName ='Supplier quotation'");
            if (dtRow201.Length > 0)
            {
                DataTable dt = dtRow201.CopyToDataTable();
                dsChkUserRight = new DataSet();
                dsChkUserRight.Tables.Add(dt);// = dt.Copy();
            }
            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
            {
                // Response.Redirect("~/NotAuthUser.aspx");
                A68.Visible = false;
            }
            #endregion






            #region[Supplier Comparsion Sheet]
            DataRow[] dtRow202 = dsChkUserRight1.Tables[1].Select("FormName ='Supplier Comparsion Sheet'");
            if (dtRow202.Length > 0)
            {
                DataTable dt = dtRow202.CopyToDataTable();
                dsChkUserRight = new DataSet();
                dsChkUserRight.Tables.Add(dt);// = dt.Copy();
            }
            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
            {
                // Response.Redirect("~/NotAuthUser.aspx");
                A70.Visible = false;
            }
            #endregion






            #region[Approve Comparison  Sheet]
            DataRow[] dtRow203 = dsChkUserRight1.Tables[1].Select("FormName ='Approve Comparison  Sheet'");
            if (dtRow203.Length > 0)
            {
                DataTable dt = dtRow203.CopyToDataTable();
                dsChkUserRight = new DataSet();
                dsChkUserRight.Tables.Add(dt);// = dt.Copy();
            }
            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
            {
                // Response.Redirect("~/NotAuthUser.aspx");
                A74.Visible = false;
            }
            #endregion


            #region[Estimate]
            DataRow[] dtRow204 = dsChkUserRight1.Tables[1].Select("FormName ='Estimate'");
            if (dtRow204.Length > 0)
            {
                DataTable dt = dtRow204.CopyToDataTable();
                dsChkUserRight = new DataSet();
                dsChkUserRight.Tables.Add(dt);// = dt.Copy();
            }
            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
            {
                // Response.Redirect("~/NotAuthUser.aspx");
                A69.Visible = false;
            }
            #endregion







            #region[Approve Estimate]
            DataRow[] dtRow205 = dsChkUserRight1.Tables[1].Select("FormName ='Approve Estimate'");
            if (dtRow205.Length > 0)
            {
                DataTable dt = dtRow205.CopyToDataTable();
                dsChkUserRight = new DataSet();
                dsChkUserRight.Tables.Add(dt);// = dt.Copy();
            }
            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
            {
                // Response.Redirect("~/NotAuthUser.aspx");
                A72.Visible = false;
            }
            #endregion




            #region[Approve Estimate]
            DataRow[] dtRow206 = dsChkUserRight1.Tables[1].Select("FormName ='Job Work Order'");
            if (dtRow206.Length > 0)
            {
                DataTable dt = dtRow206.CopyToDataTable();
                dsChkUserRight = new DataSet();
                dsChkUserRight.Tables.Add(dt);// = dt.Copy();
            }
            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
            {
                // Response.Redirect("~/NotAuthUser.aspx");
                A75.Visible = false;
            }
            #endregion



            #region[Approve Estimate]
            DataRow[] dtRow207 = dsChkUserRight1.Tables[1].Select("FormName ='Service Po'");
            if (dtRow207.Length > 0)
            {
                DataTable dt = dtRow207.CopyToDataTable();
                dsChkUserRight = new DataSet();
                dsChkUserRight.Tables.Add(dt);// = dt.Copy();
            }
            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
            {
                // Response.Redirect("~/NotAuthUser.aspx");
                A76.Visible = false;
            }
            #endregion










            #region[For Create/Update Template]
            DataRow[] dtRow12 = dsChkUserRight1.Tables[1].Select("FormName ='Create/Update Template'");
            if (dtRow12.Length > 0)
            {
                DataTable dt = dtRow12.CopyToDataTable();
                dsChkUserRight = new DataSet();
                dsChkUserRight.Tables.Add(dt);// = dt.Copy();
            }
            if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
            {
                // Response.Redirect("~/NotAuthUser.aspx");
                A29.Visible = false;
            }
            #endregion




            #region[For Material Requisition]
            DataRow[] dtRow13 = dsChkUserRight1.Tables[1].Select("FormName ='Material Requisition'");
                    if (dtRow13.Length > 0)
                    {
                        DataTable dt = dtRow13.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A9.Visible = false;
                    }
                    #endregion
                    #region[For Material Requisition Cancellation]
                    DataRow[] dtRow14 = dsChkUserRight1.Tables[1].Select("FormName ='Material Requisition Cancellation'");
                    if (dtRow14.Length > 0)
                    {
                        DataTable dt = dtRow14.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        //A10.Visible = false;
                    }
                    #endregion
                    #region[For Authorized Requisition]
                    DataRow[] dtRow15 = dsChkUserRight1.Tables[1].Select("FormName ='Authorized Requisition'");
                    if (dtRow15.Length > 0)
                    {
                        DataTable dt = dtRow15.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A31.Visible = false;
                    }
                    #endregion
                    #region[For Purchase Order To Supplier]
                    DataRow[] dtRow16 = dsChkUserRight1.Tables[1].Select("FormName ='Purchase Order To Supplier'");
                    if (dtRow16.Length > 0)
                    {
                        DataTable dt = dtRow16.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A11.Visible = false;
                    }
                    #endregion
                    #region[For Authorized Purchase Order]
                    DataRow[] dtRow17 = dsChkUserRight1.Tables[1].Select("FormName ='Authorized Purchase Order'");
                    if (dtRow17.Length > 0)
                    {
                        DataTable dt = dtRow17.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A12.Visible = false;
                    }
                    #endregion
                    #region[For Material Inward Register]
                    DataRow[] dtRow18 = dsChkUserRight1.Tables[1].Select("FormName ='Material Inward Register'");
                    if (dtRow18.Length > 0)
                    {
                        DataTable dt = dtRow18.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A13.Visible = false;
                    }
                    #endregion
                    #region[For Material Damage Register]
                    DataRow[] dtRow19 = dsChkUserRight1.Tables[1].Select("FormName ='Material Damage Register'");
                    if (dtRow19.Length > 0)
                    {
                        DataTable dt = dtRow19.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A16.Visible = false;
                    }
                    #endregion
                    #region[For Material Return Register]
                    DataRow[] dtRow20 = dsChkUserRight1.Tables[1].Select("FormName ='Material Return Register'");
                    if (dtRow20.Length > 0)
                    {
                        DataTable dt = dtRow20.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A52.Visible = false;
                    }
                    #endregion
                    #region[For Material Issue Register]
                    DataRow[] dtRow21 = dsChkUserRight1.Tables[1].Select("FormName ='Material Issue Register'");
                    if (dtRow21.Length > 0)
                    {
                        DataTable dt = dtRow21.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A14.Visible = false;
                    }
                    #endregion
                    #region[For Material Consumption Register]
                    DataRow[] dtRow22 = dsChkUserRight1.Tables[1].Select("FormName ='Material Consumption Register'");
                    if (dtRow22.Length > 0)
                    {
                        DataTable dt = dtRow22.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A53.Visible = false;
                    }
                    #endregion
                    #region[For Material Transfer Register]
                    DataRow[] dtRow44 = dsChkUserRight1.Tables[1].Select("FormName ='Material Transfer Register'");
                    if (dtRow44.Length > 0)
                    {
                        DataTable dt = dtRow44.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A17.Visible = false;
                    }
                    #endregion
                    #region[For Material Deviation Register]
                    DataRow[] dtRow24 = dsChkUserRight1.Tables[1].Select("FormName ='Material Deviation Register'");
                    if (dtRow24.Length > 0)
                    {
                        DataTable dt = dtRow24.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A40.Visible = false;
                    }
                    #endregion

                    //------Reports-------
                    #region[For Material Requisition Report]
                    DataRow[] dtRow25 = dsChkUserRight1.Tables[1].Select("FormName ='Material Requisition Report'");
                    if (dtRow25.Length > 0)
                    {
                        DataTable dt = dtRow25.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                            && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                       b1.Visible = false;
                    }
                    #endregion
                    #region[For Material Purchase Report]
                    DataRow[] dtRow27 = dsChkUserRight1.Tables[1].Select("FormName ='Material Purchase Report'");
                    if (dtRow27.Length > 0)
                    {
                        DataTable dt = dtRow27.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                            && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        b2.Visible = false;
                    }
                    #endregion
                    #region[For Material Inward Report]
                    DataRow[] dtRow28 = dsChkUserRight1.Tables[1].Select("FormName ='Material Inward Report'");
                    if (dtRow28.Length > 0)
                    {
                        DataTable dt = dtRow28.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                            && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        b3.Visible = false;
                    }
                    #endregion
                    #region[For Material Damage Report]
                    DataRow[] dtRow31 = dsChkUserRight1.Tables[1].Select("FormName ='Material Damage Report'");
                    if (dtRow31.Length > 0)
                    {
                        DataTable dt = dtRow31.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                            && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        b4.Visible = false;
                    }
                    #endregion
                    #region[For Material Issue Report]
                    DataRow[] dtRow32 = dsChkUserRight1.Tables[1].Select("FormName ='Material Issue Report'");
                    if (dtRow32.Length > 0)
                    {
                        DataTable dt = dtRow32.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                             && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        b5.Visible = false;
                    }
                    #endregion
                    #region[For Material Consumption Report]
                    DataRow[] dtRow33 = dsChkUserRight1.Tables[1].Select("FormName ='Material Consumption Report'");
                    if (dtRow33.Length > 0)
                    {
                        DataTable dt = dtRow33.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                            && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        b6.Visible = false;
                    }
                    #endregion
                    #region[For Material Transfer Report]
                    DataRow[] dtRow36 = dsChkUserRight1.Tables[1].Select("FormName ='Material Transfer Report'");
                    if (dtRow36.Length > 0)
                    {
                        DataTable dt = dtRow36.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                             && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                      b7.Visible = false;
                    }
                    #endregion
                    #region[For Material Deviation Report]
                    DataRow[] dtRow38 = dsChkUserRight1.Tables[1].Select("FormName ='Material Deviation Report'");
                    if (dtRow38.Length > 0)
                    {
                        DataTable dt = dtRow38.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                            && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                       // Response.Redirect("~/NotAuthUser.aspx");
                       b8.Visible = false;
                    }
                    #endregion
                    #region[For Material Inventory Report]
                    DataRow[] dtRow39 = dsChkUserRight1.Tables[1].Select("FormName ='Material Inventory Report'");
                    if (dtRow39.Length > 0)
                    {
                        DataTable dt = dtRow39.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                           && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        b9.Visible = false;
                    }
                    #endregion
                    #region[For Artical History Report]
                    DataRow[] dtRow49 = dsChkUserRight1.Tables[1].Select("FormName ='Artical History Report'");
                    if (dtRow49.Length > 0)
                    {
                        DataTable dt = dtRow49.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false
                           && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["PrintAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        b10.Visible = false;
                    }
                    #endregion
                    #region[For Send E-mail]
                    DataRow[] dtRow42 = dsChkUserRight1.Tables[1].Select("FormName ='Send E-mail'");
                    if (dtRow42.Length > 0)
                    {
                        DataTable dt = dtRow42.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A37.Visible = false;
                    }
                    #endregion
                    #region[For Database Backup]
                    DataRow[] dtRow43 = dsChkUserRight1.Tables[1].Select("FormName ='Database Backup'");
                    if (dtRow43.Length > 0)
                    {
                        DataTable dt = dtRow43.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        dsChkUserRight.Tables.Add(dt);// = dt.Copy();
                    }
                    if (Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["ViewAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["AddAuth"].ToString()) == false &&
                        Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["DelAuth"].ToString()) == false && Convert.ToBoolean(dsChkUserRight.Tables[0].Rows[0]["EditAuth"].ToString()) == false)
                    {
                        // Response.Redirect("~/NotAuthUser.aspx");
                        A39.Visible = false;
                    }
                    #endregion

                    #region[Hide ManuStipName]
                    //if (!A26.Visible && !A27.Visible)
                    //{
                    //    b1.Visible = false;
                    //}
                    //if (!A19.Visible && !A18.Visible)
                    //{
                    //    b2.Visible = false;
                    //}
                    //if (!A20.Visible && !A21.Visible)
                    //{
                    //    b3.Visible = false;
                    //}
                    //if (!A32.Visible && !A34.Visible)
                    //{
                    //    b4.Visible = false;
                    //}
                    //if (!A28.Visible && !A30.Visible && !A43.Visible && !A46.Visible)
                    //{
                    //    b5.Visible = false;
                    //}
                    //if (!A35.Visible && !A36.Visible)
                    //{
                    //    b6.Visible = false;
                    //}
                    //if (!A33.Visible)//&& !A42.Visible
                    //{
                    //    A15.Visible = false;
                    //}
                    //if (!A15.Visible && !A41.Visible)//&& !A38.Visible
                    //{
                    //    b7.Visible = false;
                    //}
                    //if (!A47.Visible && !A48.Visible)
                    //{
                    //    //b8.Visible = false;
                    //}
                    //if (!A49.Visible && !A50.Visible)
                    //{
                    //    b9.Visible = false;
                    //}
                    //if (!A55.Visible && !A56.Visible)
                    //{
                    //    A54.Visible = false;
                    //}
                    #endregion

                    #region[For Edit Authorised Purchase Order]
                    DataRow[] dtRow100 = dsChkUserRightSP.Tables[4].Select("Form ='Edit Authorised Purchase Order'");
                    if (dtRow100.Length > 0)
                    {
                        DataTable dt = dtRow100.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        if (dt.Rows[0]["Permission"].ToString() == "0")
                        {
                            A61.Visible = false;
                        }
                    }
                    //DataRow[] dtRow101 = dsChkUserRightSP.Tables[4].Select("Form ='Unit Conversion In Item Master'");
                    //if (dtRow101.Length > 0)
                    //{
                    //    DataTable dt = dtRow101.CopyToDataTable();
                    //    dsChkUserRight = new DataSet();
                    //    if (dt.Rows[0]["Permission"].ToString() == "0")
                    //    {
                    //        A61.Visible = false;
                    //    }
                    //}
                    DataRow[] dtRow102 = dsChkUserRightSP.Tables[4].Select("Form ='Purchase Order Shortage/Excess Report'");
                    if (dtRow102.Length > 0)
                    {
                        DataTable dt = dtRow102.CopyToDataTable();
                        dsChkUserRight = new DataSet();
                        if (dt.Rows[0]["Permission"].ToString() == "0")
                        {
                            A62.Visible = false;
                        }
                    }
                    #endregion


                    #region[For ChkStatus]

                    if (Convert.ToInt32(Session["UserId"]) == 2)
                    {
                        A66.Visible = true;
                    }
                    else
                    {
                        A66.Visible = false;
                    }
                   
                    #endregion
            }
        
        #endregion

    }
}
