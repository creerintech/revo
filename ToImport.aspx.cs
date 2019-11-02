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
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using MayurInventory.DataModel;

public partial class ToImport : System.Web.UI.Page
{
    DMEmployee Obj_EM = new DMEmployee();
    EmployeeMaster Entity_EM = new EmployeeMaster();
    string Strerror = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    if (ExcelUpload.HasFile)
    //    {
    //        try
    //        {
    //            string path = string.Concat(Server.MapPath("~/UploadedFolder/" + ExcelUpload.FileName));
    //            ExcelUpload.SaveAs(path);

    //            // Connection String to Excel Workbook
    //            string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);
    //            OleDbConnection connection = new OleDbConnection();
    //            connection.ConnectionString = excelConnectionString;
    //            OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);
    //            connection.Open();
    //            // Create DbDataReader to Data Worksheet
    //            DbDataReader dr = command.ExecuteReader();

    //            // SQL Server Connection String
    //            string sqlConnectionString = @"Data Source=COMP01-PC;Initial Catalog=Petro_MMS_13th_Feb_2017;User ID=sa;Password=Admin@123";

    //            // Bulk Copy to SQL Server 
    //            SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);
    //            bulkInsert.DestinationTableName = "EmployeeMaster";
    //            bulkInsert.WriteToServer(dr);
    //            //Label1.Text = "Ho Gaya";
    //        }
    //        catch (Exception ex)
    //        {
    //            //Label1.Text = ex.Message;
    //        }
    //    }
    //}


    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    if (FileUpload1.HasFile)
    //    {
    //        string sPath = Server.MapPath("~/BulkFolder/" + FileUpload1.FileName);
    //        FileUpload1.SaveAs(sPath);

    //        ImporttoSQL(sPath);
    //    }
    //}

    //private void ImporttoSQL(string sPath)
    //{
    //    // Connect to Excel 2007 earlier version
    //    //string sSourceConstr = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\AgentList.xls; Extended Properties=""Excel 8.0;HDR=YES;""";
    //    // Connect to Excel 2007 (and later) files with the Xlsx file extension 
    //    string sSourceConstr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", sPath);

    //    string sDestConstr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    //    OleDbConnection sSourceConnection = new OleDbConnection(sSourceConstr);
    //    using (sSourceConnection)
    //    {
    //        string sql = string.Format("Select [EmployeeId],[EmpName],[Address],[TelNo],[MobileNo],[Email],[Designation],[Department],[Note],[CreatedBy],[CreatedDate],[IsDeleted], FROM [{0}]", "Sheet1$");
    //        OleDbCommand command = new OleDbCommand(sql, sSourceConnection);
    //        sSourceConnection.Open();
    //        using (OleDbDataReader dr = command.ExecuteReader())
    //        {
    //            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sDestConstr))
    //            {
    //                bulkCopy.DestinationTableName = "Employee";
    //                //You can mannualy set the column mapping by the following way.
    //                //bulkCopy.ColumnMappings.Add("Employee ID", "Employee Code");
    //                bulkCopy.WriteToServer(dr);
    //            }
    //        }
    //    }

    //}

    public DataTable SaveToDatabase(DataTable DD)
    {
        DataSet DSA = new DataSet();
        DSA.Tables.Add(DD);
        string XMLstr = DSA.GetXml();
        DataSet DDData = Obj_EM.InsertData(XMLstr, out Strerror);
        return DDData.Tables[0];
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {          
            if (ExcelUpload.HasFile)
            {                               
                string FileName = Path.GetFileName(ExcelUpload.PostedFile.FileName);
                string Extension = Path.GetExtension(ExcelUpload.PostedFile.FileName);
                if (Extension == ".xls" || Extension == ".xlsx")
                {
                    //string FolderPath = ConfigurationManager.AppSettings["EmpData"];
                    string FilePath = Server.MapPath("~/BulkFolder/" + FileName);
                    string File = FileName.Substring(0, FileName.IndexOf("."));
                    //ExcelUpload.SaveAs(FilePath);   //-------- SAVE FILE PATH
                    string conStr = string.Empty;
                    if (Extension == ".xls")
                    {
                        conStr = String.Format(ConfigurationManager.AppSettings["ExcelConn03"], FilePath, true);
                    }
                    else if (Extension == ".xlsx")
                    {
                        conStr = String.Format(ConfigurationManager.AppSettings["ExcelConn07"], FilePath, true);
                    }
                    OleDbConnection connExcel = new OleDbConnection(conStr);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable dt = new DataTable();
                    cmdExcel.Connection = connExcel;
                    connExcel.Open();
                    DataTable dtExcelSchema;
                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    connExcel.Close();

                    cmdExcel.CommandText = "Select [EmpName],[Address],[TelNo],[MobileNo],[Email],[Designation],[Department],[Note],[CreatedBy],[CreatedDate],[IsDeleted] From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    connExcel.Open();
                    long totalRecInExcelFile = 0;
                    oda.Fill(dt);
                    
                        totalRecInExcelFile = dt.Rows.Count;
                       
                        DataTable dtFinal = SaveToDatabase(dt);
                       
                        DataRow[] dr = dtFinal.Select("STATUS='Rejected'");
                        int noofrows = 0;
                        noofrows = dr.Length;                                            
                }
                else
                {
                    //obj_Comman.ShowPopUpMsg("Please Upload Excel 97-2003/2007 File Format Only", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
