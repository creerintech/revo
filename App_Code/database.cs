using System;
using System.Collections.Generic;
//using System.Data.Linq;
using System.Web;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.IO;
using System.Data.OleDb;
using System.Data.Common;


/// <summary>
/// Summary description for databaseCustomerId
/// </summary>
public class database
{
    //static string connStr = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
   // static string connStr = ConfigurationManager.ConnectionStrings["RestrosoftWebConnectionString"].ConnectionString;
    public SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MayurInventory"].ConnectionString);
    //public SqlConnection cn = new SqlConnection(connectionString);
    DataTable dt;
    SqlDataAdapter da;
    SqlCommand cmd = new SqlCommand();
    string str;




    public void cnopen()
    {
        if (cn.State == ConnectionState.Open)
        {
            cn.Close();

        }
        
        cn.Open();
    }

    public void cnclose()
    {
        if (cn.State != ConnectionState.Closed)
        {
            cn.Close();
        }
    }

    public DataTable DbCon(string pStr)
    {
        try
        {
            dt = new DataTable();
            da = new SqlDataAdapter(pStr, cn);
            da.Fill(dt);
        }
        catch (Exception pEx)
        {
            WriteErrorLog(pEx);
        }
        return dt;
    }


    public void drp_bind(DropDownList cmb, string strSQL, string dColumn, string vcolumn)
    {
        cnopen();
        cmd = new SqlCommand(strSQL, cn); // table name 
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);  // fill dataset

        cmb.DataTextField = ds.Tables[0].Columns[dColumn].ToString(); // text field name of table dispalyed in dropdown
        cmb.DataValueField = ds.Tables[0].Columns[vcolumn].ToString();             // to retrive specific  textfield name 
        cmb.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
        cmb.DataBind();  //binding dropdownlist
        cnclose();
    }

    public void FillDropDownList(DropDownList cmb, string strSQL, string dColumn)
    {
        cnopen();
        cmd = new SqlCommand(strSQL, cn);
        SqlDataReader rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            cmb.Items.Add(rd[dColumn].ToString());
        }
        rd.Close();
        cnclose();
    }


    public bool ChkDb_Value(String query)//for chk database value  by shubhangi
    {
        cnopen();
        SqlCommand cmd = new SqlCommand(query, cn);
        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.Read() == true)
            return true;
        else
            return false;
    }
    public void ClearAllControlsRecursive(Control container)
    {
        foreach (var control in container.Controls)
        {
            if (control is TextBox)
            {
                ((TextBox)control).Text = String.Empty;
            }


        }
    }


    public void insert(string query) //for insert  by shubhangi
    {
        cnopen();
        SqlCommand cmd = new SqlCommand(query, cn);
        cmd.ExecuteNonQuery();

        cnclose();

    }

    float a;

    public float getDb_Value(String query) //for getvalue from database  by shubhangi
    {

     
            cnopen();
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    a = 0;
                else
                    a = float.Parse(cmd.ExecuteScalar().ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                cnclose();
            a = 0;
            }
            return a;
            cnclose();
        
    }
    public string getDbstatus_Value(String query) //for get string value from database  by shubhangi
    {
        cnopen();
        SqlCommand cmd = new SqlCommand(query, cn);
        try
        {
            if (cmd.ExecuteScalar().ToString() == "")
                str = "0";
            else
                str = cmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {// MessageBox.Show(ex.Message);
        }
        return str;
        cnclose();
    }


    public DataTable DisplaygridView(string query) //for bind datagridview   by shubhangi
    {
        DataTable dt = new DataTable();
        cnopen();
        try
        {
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }
        return dt;
    }





    public DataTable DbSPCon(string pStr, ArrayList pArrName, ArrayList pArrValue)
    {
        try
        {
            dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = pStr;
            cmd.Connection = cn;
            cmd.Parameters.Clear();
            for (int index = 0; index <= pArrName.Count - 1; index++)
            {
                cmd.Parameters.AddWithValue("@" + pArrName[index], pArrValue[index]);
            }
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception pEx)
        {
            WriteErrorLog(pEx);
        }

        return dt;

    }
    public DataTable DbSPCon(string pStr)
    {
        try
        {
            dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = pStr;
            cmd.Connection = cn;
            cmd.Parameters.Clear();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception pEx)
        {
            WriteErrorLog(pEx);
        }

        return dt;
    }

    public void WriteErrorLog(Exception pEx)
    {
        try
        {
            //DbCon("INSERT INTO SaveErrorLog (ErrorMessage,Trace,Source,ErrorDate)VALUES ('" + pEx.Message.Replace("'", "") + "','" + pEx.StackTrace.Replace("'", "") + "','" + pEx.Source.Replace("'", "") + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')");
        }
        catch
        {
            SetPageMessage("Error Occured!!!!", true);
        }
    }




    public void RecordDelete(object pBtnDelete, string pTableName, string _pkName = "ID")
    {
        try
        {
            LinkButton _sender = (LinkButton)pBtnDelete;
            RepeaterItem _item = (RepeaterItem)_sender.Parent;
            HiddenField _hdnID = (HiddenField)_item.FindControl("hdnID");
            if (_hdnID != null)
            {
                Delete(pTableName, Convert.ToInt32(_hdnID.Value), _pkName);
            }
        }
        catch (Exception pEx)
        {
            WriteErrorLog(pEx);
        }

    }
    public void SetPageMessage(string pMessage, bool IsError)
    {
        try
        {
            Page page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                Panel _msgPanel = (Panel)page.Master.FindControl("genMsg");
                if (_msgPanel != null)
                {
                    IsError = true;
                    _msgPanel.CssClass = IsError ? "alert alert-error" : "alert alert-info";
                    LiteralControl _literal = (LiteralControl)_msgPanel.Controls[0];
                    _literal.Text = pMessage;
                    _msgPanel.Visible = true;
                }
            }
        }
        catch (Exception pEx)
        {

        }

    }


    public void Delete(string pTableName, int pTableID, string _pkName = "ID")
    {
        try
        {
            dt = new DataTable();
            da = new SqlDataAdapter("Delete From " + pTableName + " where " + _pkName + "=" + pTableID, cn);
            da.Fill(dt);
            SetPageMessage("Record Deleted Successfuly", false);
        }
        catch
        {
            SetPageMessage("Error In Record Deletion", true);
        }
    }
    public DataTable SelectTable(string pSpName, string pkname = "ID")//Method for fill value to contol selected from selectd row
    {
        try
        {
            dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = pSpName;
            cmd.Connection = cn;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@" + pkname, 0);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception pEx)
        {
            WriteErrorLog(pEx);
        }

        return dt;
    }
    //public void RecordEdit(object pBtnEdit, string pSpName, ControlCollection pCntlCollection)
    //{
    //    HiddenField _hdnID = null;
    //    try
    //    {

    //        LinkButton _sender = (LinkButton)pBtnEdit;
    //        RepeaterItem _item = (RepeaterItem)_sender.Parent;
    //        _hdnID = (HiddenField)_item.FindControl("hdnID");

    //        if (_hdnID != null)
    //        {
    //            dt = new DataTable();
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = pSpName;
    //            cmd.Connection = cn;
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@ID", _hdnID.Value);
    //            da = new SqlDataAdapter(cmd);
    //            da.Fill(dt);
    //            foreach (TextBox txt in pCntlCollection.OfType<TextBox>())
    //            {
    //                try
    //                {
    //                    txt.Text = dt.Rows[0][(txt.ID.Substring(3))].ToString();
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }
    //            foreach (Label lbl in pCntlCollection.OfType<Label>())
    //            {
    //                try
    //                {
    //                    if (lbl.ID.Contains("lebl"))
    //                        lbl.Text = dt.Rows[0][(lbl.ID.Substring(4))].ToString();
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }
    //            foreach (Image img in pCntlCollection.OfType<Image>())
    //            {
    //                try
    //                {
    //                    //MemoryStream stream = new MemoryStream((byte[])dt.Rows[0][(img.ID.Substring(3))]);
    //                    //System.Drawing.Image imge = System.Drawing.Image.FromStream(stream);
    //                    //string strPath = HttpContext.Current.Server.MapPath("~/Photo/" + img.ID + ".jpg");
    //                    //imge.Save(strPath);
    //                    //img.ImageUrl = strPath;
    //                    img.ImageUrl = dt.Rows[0][(img.ID.Substring(3))].ToString();
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }

    //            foreach (DropDownList cmb in pCntlCollection.OfType<DropDownList>())
    //            {
    //                try
    //                {
    //                    cmb.ClearSelection();
    //                    cmb.Items.FindByValue(dt.Rows[0][(cmb.ID)].ToString()).Selected = true;
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }

    //            foreach (CheckBoxList chk in pCntlCollection.OfType<CheckBoxList>())
    //            {
    //                try
    //                {
    //                    string[] checkedValues = (dt.Rows[0][(chk.ID)]).ToString().Split(',');
    //                    foreach (string val in checkedValues)
    //                    {
    //                        chk.Items.FindByValue(val).Selected = true;
    //                    }
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }
    //            foreach (HiddenField hf in pCntlCollection.OfType<HiddenField>())
    //            {
    //                try
    //                {
    //                    hf.Value = dt.Rows[0][(hf.ID)].ToString();
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }
    //            foreach (RadioButtonList lsb in pCntlCollection.OfType<RadioButtonList>())
    //            {
    //                try
    //                {
    //                    lsb.Items.FindByValue(dt.Rows[0][(lsb.ID)].ToString()).Selected = true;
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }

    //        }
    //    }
    //    catch (Exception pEx)
    //    {
    //        WriteErrorLog(pEx);
    //    }


    //}
    //public void RecordEdit(object pBtnEdit, string pSpName, ControlCollection pCntlCollection, string _pkName = "ID")
    //{
    //    HiddenField _hdnID = null;
    //    try
    //    {

    //        LinkButton _sender = (LinkButton)pBtnEdit;
    //        RepeaterItem _item = (RepeaterItem)_sender.Parent;
    //        _hdnID = (HiddenField)_item.FindControl("hdnID");

    //        if (_hdnID != null)
    //        {
    //            dt = new DataTable();
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = pSpName;
    //            cmd.Connection = cn;
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@" + _pkName, _hdnID.Value);
    //            da = new SqlDataAdapter(cmd);
    //            da.Fill(dt);
    //            foreach (TextBox txt in pCntlCollection.OfType<TextBox>())
    //            {
    //                try
    //                {
    //                    txt.Text = dt.Rows[0][(txt.ID.Substring(3))].ToString();
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }
    //            foreach (Label lbl in pCntlCollection.OfType<Label>())
    //            {
    //                try
    //                {
    //                    if (lbl.ID.Contains("lebl"))
    //                        lbl.Text = dt.Rows[0][(lbl.ID.Substring(4))].ToString();
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }
    //            foreach (Image img in pCntlCollection.OfType<Image>())
    //            {
    //                try
    //                {
    //                    //MemoryStream stream = new MemoryStream((byte[])dt.Rows[0][(img.ID.Substring(3))]);
    //                    //System.Drawing.Image imge = System.Drawing.Image.FromStream(stream);
    //                    //string strPath = HttpContext.Current.Server.MapPath("~/Photo/" + img.ID + ".jpg");
    //                    //imge.Save(strPath);
    //                    //img.ImageUrl = strPath;
    //                    img.ImageUrl = dt.Rows[0][(img.ID.Substring(3))].ToString();
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }

    //            foreach (DropDownList cmb in pCntlCollection.OfType<DropDownList>())
    //            {
    //                try
    //                {
    //                    cmb.ClearSelection();
    //                    cmb.Items.FindByValue(dt.Rows[0][cmb.ID.Substring(3)].ToString()).Selected = true;
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }

    //            foreach (CheckBoxList chk in pCntlCollection.OfType<CheckBoxList>())
    //            {
    //                try
    //                {
    //                    string[] checkedValues = (dt.Rows[0][(chk.ID.Substring(3))]).ToString().Split(',');
    //                    foreach (string val in checkedValues)
    //                    {
    //                        chk.Items.FindByValue(val).Selected = true;
    //                    }
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }
    //            foreach (HiddenField hf in pCntlCollection.OfType<HiddenField>())
    //            {
    //                try
    //                {
    //                    hf.Value = dt.Rows[0][(hf.ID)].ToString();
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }
    //            foreach (RadioButtonList lsb in pCntlCollection.OfType<RadioButtonList>())
    //            {
    //                try
    //                {
    //                    string lsbVAl = string.Empty;
    //                    if (dt.Rows[0][(lsb.ID.Substring(3))].ToString() == "True")
    //                    {
    //                        lsbVAl = "1";
    //                        lsb.SelectedIndex = 1;
    //                    }
    //                    else if (dt.Rows[0][(lsb.ID.Substring(3))].ToString() == "False")
    //                    {
    //                        lsbVAl = "0";
    //                        lsb.SelectedIndex = 0;
    //                    }
    //                    else
    //                    {
    //                        lsb.Items.FindByValue(dt.Rows[0][(lsb.ID.Substring(3))].ToString()).Selected = true;

    //                    }
    //                }
    //                catch (Exception pEx)
    //                {
    //                    WriteErrorLog(pEx);
    //                }
    //            }

    //        }
    //    }
    //    catch (Exception pEx)
    //    {
    //        WriteErrorLog(pEx);
    //    }

    //}
    //public void RecordSave(ControlCollection pCntlCollection, string pSpName, string talName = "StudentMaster")
    //{
    //    try
    //    {
    //        dt = new DataTable();
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.CommandText = pSpName;
    //        cmd.Connection = cn;
    //        cmd.Parameters.Clear();
    //        string _message = string.Empty;

    //        foreach (TextBox txt in pCntlCollection.OfType<TextBox>())
    //        {
    //            try
    //            {

    //                if (txt.Text.Trim() == "")
    //                    cmd.Parameters.AddWithValue("@" + txt.ID.Substring(3), DBNull.Value);
    //                else
    //                    cmd.Parameters.AddWithValue("@" + txt.ID.Substring(3), txt.Text);

    //            }
    //            catch (Exception pEx)
    //            {
    //                WriteErrorLog(pEx);
    //            }
    //        }

    //        foreach (DropDownList cmb in pCntlCollection.OfType<DropDownList>())
    //        {
    //            try
    //            {
    //                cmd.Parameters.AddWithValue("@" + cmb.ID.Substring(3), cmb.SelectedValue);
    //            }
    //            catch (Exception pEx)
    //            {
    //                WriteErrorLog(pEx);
    //            }
    //        }
    //        foreach (CheckBox chkb in pCntlCollection.OfType<CheckBox>())
    //        {
    //            try
    //            {
    //                cmd.Parameters.AddWithValue("@" + chkb.ID.Substring(3), chkb.Checked ? 1 : 0);

    //            }
    //            catch (Exception pEx)
    //            {
    //                WriteErrorLog(pEx);
    //            }
    //        }
    //        foreach (CheckBoxList chk in pCntlCollection.OfType<CheckBoxList>())
    //        {
    //            try
    //            {
    //                string checkedValues = string.Empty;
    //                foreach (ListItem cm in chk.Items)
    //                {
    //                    if (cm.Selected)
    //                        checkedValues += cm.Value + ",";
    //                }
    //                cmd.Parameters.AddWithValue("@" + chk.ID.Substring(3), checkedValues);

    //            }
    //            catch (Exception pEx)
    //            {
    //                WriteErrorLog(pEx);
    //            }
    //        }

    //        foreach (HiddenField hf in pCntlCollection.OfType<HiddenField>())
    //        {
    //            try
    //            {
    //                if ((hf.ID).ToLower() == "id")
    //                    if (hf.Value == "0")
    //                        _message = "Record Saved Successfully";
    //                    else
    //                        _message = "Record Updated Successfully";
    //                cmd.Parameters.AddWithValue("@" + hf.ID, hf.Value);
    //            }
    //            catch (Exception pEx)
    //            {

    //            }
    //        }

    //        foreach (FileUpload flu in pCntlCollection.OfType<FileUpload>())
    //        {

    //            try
    //            {
    //                //IDataParameter par = cmd.CreateParameter();
    //                //par.ParameterName = flu.ID.Substring(3);
    //                //par.DbType = DbType.Binary;
    //                //par.Value = flu.FileBytes;
    //                //cmd.Parameters.Add(par);
    //                if ((flu.FileContent.Length > 5119 && flu.FileContent.Length < 512001) || flu.FileContent.Length == 0)
    //                {
    //                    string ID = "0";
    //                    foreach (HiddenField hf in pCntlCollection.OfType<HiddenField>())
    //                    {
    //                        if ((hf.ID).ToLower() == "id")
    //                        {
    //                            ID = hf.Value;
    //                        }
    //                    }
    //                    if (ID == "0")
    //                    {
    //                        dt = this.DbCon("select IDENT_CURRENT('" + talName + "')");
    //                        int maxid = Convert.ToInt32(dt.Rows[0][0]);
    //                        string strPath = HttpContext.Current.Server.MapPath("~/Photo/" + maxid.ToString() + ".jpg");
    //                        flu.PostedFile.SaveAs(strPath);
    //                        cmd.Parameters.AddWithValue("@" + flu.ID.Substring(3), "../Photo/" + maxid.ToString() + ".jpg");
    //                    }
    //                    else
    //                    {
    //                        string strPath = HttpContext.Current.Server.MapPath("~/Photo/" + ID + ".jpg");
    //                        FileInfo fl = new FileInfo(strPath);
    //                        fl.Delete();
    //                        flu.PostedFile.SaveAs(strPath);
    //                    }
    //                }
    //                else
    //                {
    //                    SetPageMessage("Photo Size is between 5KB to 500 KB !", true);
    //                    break;
    //                    // goto endabc;
    //                }
    //            }
    //            catch (Exception pEx)
    //            {
    //                WriteErrorLog(pEx);
    //            }
    //        }
    //        foreach (RadioButtonList lsb in pCntlCollection.OfType<RadioButtonList>())
    //        {
    //            try
    //            {
    //                cmd.Parameters.AddWithValue("@" + lsb.ID.Substring(3), lsb.SelectedValue);
    //            }
    //            catch (Exception pEx)
    //            {
    //                WriteErrorLog(pEx);
    //            }
    //        }
    //        try
    //        {
    //            //cmd.Parameters.AddWithValue("IsActive", true);
    //            //cmd.Parameters.AddWithValue("IsArchived", false);
    //            //cmd.Parameters.AddWithValue("CreatedBy", 1);
    //            //cmd.Parameters.AddWithValue("CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    //            //cmd.Parameters.AddWithValue("ModifiedBy", 0);
    //            //cmd.Parameters.AddWithValue("ModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    //            da = new SqlDataAdapter(cmd);
    //            da.Fill(dt);
    //            SetPageMessage(_message, false);
    //        }
    //        catch
    //        {
    //            SetPageMessage("Error in record saving!", true);
    //        }
    //    }
    //    catch (Exception pEx)
    //    {
    //        WriteErrorLog(pEx);
    //    }
    //endabc:
    //    ;


    //}
    public void BindDDLFromLookup(DropDownList pddl, string pLookupCode)
    {
        try
        {
            ArrayList arName = new ArrayList();
            ArrayList arValue = new ArrayList();
            arName.Add("CategoryCode");
            arValue.Add(pLookupCode);
            pddl.DataSource = this.DbSPCon("LookupList", arName, arValue);
            pddl.DataValueField = "ID";
            pddl.DataTextField = "LookupName";
            pddl.DataBind();
            ListItem li = new ListItem("Select", "0");
            pddl.Items.Insert(0, li);
        }
        catch (Exception pEx)
        {
            WriteErrorLog(pEx);
        }
    }
    public void BindDDLFromTable(DropDownList pddl, string pSpName, string pDisplayMember)//Method for fill value to contol selected from selectd row
    {
        try
        {
            ArrayList arName = new ArrayList();
            ArrayList arValue = new ArrayList();
            arName.Add("ID");
            arValue.Add("0");
            pddl.DataSource = this.DbSPCon(pSpName, arName, arValue);
            pddl.DataValueField = "ID";
            pddl.DataTextField = pDisplayMember;
            pddl.DataBind();
            pddl.Items.Insert(0, "Select");
        }
        catch (Exception pEx)
        {
            WriteErrorLog(pEx);
        }

    }
    public void BindDDLFromTable(DropDownList pddl, string pSpName, string pDisplayMember, string pValueMember)//Method for fill value to contol selected from selectd row
    {
        try
        {
            ArrayList arName = new ArrayList();
            ArrayList arValue = new ArrayList();
            arName.Add("ID");
            arValue.Add("0");
            pddl.DataSource = this.DbSPCon(pSpName, arName, arValue);
            pddl.DataValueField = "ID";
            pddl.DataTextField = pDisplayMember;
            pddl.DataBind();
            pddl.Items.Insert(0, "Select");
        }
        catch (Exception pEx)
        {
            WriteErrorLog(pEx);
        }

    }


    public void DeleteData(string query, string tblName)
    {
        try
        {
            cnopen();
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            // MessageBox.Show(ex.Message);
           // erLog.WriteErrorLog(ex.ToString());
        }
        finally
        {
            cnclose();
        }
    }
    public DataTable Displaygrid(string query)
    {
        DataTable dt = new DataTable();
        try
        {
            
            cnopen();
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception ex)
        {
        }
        return dt;
        cnclose();
    }





    public DataSet dgv_display(string query)
    {

        //cnopen();
        SqlDataAdapter da = new SqlDataAdapter(query, cn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        return ds;

    }
    public DataSet dgvaddsub(string query)
    {
        cnopen();
        SqlDataAdapter da = new SqlDataAdapter(query, cn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        return ds;
    }
    public void updateDtls(string pSpName, string val, string parameterName)
    {
        dt = new DataTable();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = pSpName;
        cmd.Connection = cn;
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue(parameterName, val);
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
    }
    public void importExcel(FileUpload f, string tbl) //parameters FileUpload tag name,table 
    {

        string ExcelContentType = "application/vnd.ms-excel";
        string Excel2010ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        if (f.HasFile)
        {
            if (f.PostedFile.ContentType == ExcelContentType || f.PostedFile.ContentType == Excel2010ContentType)
            {
                try
                {

                    string path = string.Concat(HttpContext.Current.Server.MapPath("~/TempFiles/"), f.FileName);

                    string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);
                    using (OleDbConnection connection =
                                 new OleDbConnection(excelConnectionString))
                    {
                        OleDbCommand command = new OleDbCommand
                                ("Select * FROM [Sheet1$]", connection);
                        connection.Open();
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            cnopen();
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cn))
                            {
                                bulkCopy.DestinationTableName = tbl;
                                bulkCopy.WriteToServer(dr);
                            }
                            cnclose();
                        }
                    }
                }

                catch (Exception ex)
                {

                }
            }
        }
    }

    public int getUniqueId(string query)
    {
        int a = 0;
        cnopen();

        SqlCommand cmd = new SqlCommand(query, cn);

        try
        {
            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
                a = int.Parse(rd[0].ToString());


        }
        catch (Exception ex)
        { Console.WriteLine(ex); }

        return a + 1;

    }
}

    

