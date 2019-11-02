using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net.Mail;

/// <summary>
/// Summary description for CommanFunction
/// </summary>
namespace MayurInventory.Utility
{

 #region [Comman Function]
    public class CommanFunction : MayurInventory.Utility.Setting
    {

        public void FreezeGridviewHeader(GridView _gv1, Table _tb1, Panel _pc1, Page _Page1, int pnlWidth)
        {
            try
            {
                // ******** Commented By Anand In dated 06-11-2012 *************
                ////////////_Page1.EnableViewState = false;

                //////////////[Español]Copiando las propiedades del renglon de encabezado
                //////////////[English]Copying a header row data and properties    
                ////////////_tb1.Rows.Add(_gv1.HeaderRow);
                ////////////_tb1.Rows[0].ControlStyle.CopyFrom(_gv1.HeaderStyle);
                ////////////_tb1.CellPadding = _gv1.CellPadding;
                ////////////_tb1.CellSpacing = _gv1.CellSpacing;
                ////////////_tb1.BorderWidth = _gv1.BorderWidth;


                //////////////if (!_gv1.Width.IsEmpty)
                //////////////_gv1.Width = Unit.Pixel(Convert.ToInt32(_gv1.Width.Value) + Convert.ToInt32(
                //////////////    _tb1.Width.Value) + 13);

                //////////////[Español]Copiando las propiedades de cada celda del nuevo encabezado.
                //////////////[English]Copying  each cells properties to the new header cells properties
                ////////////int Count = 0;
                ////////////_pc1.Width = Unit.Pixel(pnlWidth);

                ////////////for (Count = 0; Count < _gv1.HeaderRow.Cells.Count - 1; Count++)
                ////////////{
                ////////////    _tb1.Rows[0].Cells[Count].Width = _gv1.Columns[Count].ItemStyle.Width;
                ////////////    _tb1.Rows[0].Cells[Count].BorderWidth = _gv1.Columns[Count].HeaderStyle.BorderWidth;
                ////////////    _tb1.Rows[0].Cells[Count].BorderStyle = _gv1.Columns[Count].HeaderStyle.BorderStyle;
                ////////////    _pc1.Width = Unit.Pixel(Convert.ToInt32(_tb1.Rows[0].Cells[Count].Width.Value) + Convert.ToInt32(_pc1.Width.Value) + 14);
                ////////////}
                //////////////Panel1.Width = Unit.Pixel(Convert.ToInt32(
                //////////////    _tb1.Rows[0].Cells[Count-1].Width.Value) + 12);

                ////////////_Page1.EnableViewState = true;
                // ******** END *************
            }
            catch { }
        }
    
        //===========To Show Popup================
        public void ShowPopUpMsg(string msg, Page oPage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(oPage, this.GetType(), "showalert", sb.ToString(), true);
        }
        //===========To Show Popup================


        //===========To Resize Uploaded Image================
        public System.Drawing.Image ResizeImage(System.Drawing.Image sourceImage, int width, int height)
        {
            System.Drawing.Image oThumbNail = new Bitmap(sourceImage, width, height);
            Graphics oGraphic = Graphics.FromImage(oThumbNail);
            oGraphic.CompositingQuality = CompositingQuality.HighQuality;
            oGraphic.SmoothingMode = SmoothingMode.HighQuality;
            oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle oRectangle = new Rectangle(0, 0, width, height);
            oGraphic.DrawImage(sourceImage, oRectangle);
            return oThumbNail;
        }
        //===========To Resize Uploaded Image================

        //===========To Export Grid================
        public void Export(string fileName, GridView gv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid
                    Table table = new Table();

                    //  add the header row to the table
                    if (gv.HeaderRow != null)
                    {
                        CommanFunction.PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        CommanFunction.PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        CommanFunction.PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// Replace any of the contained controls with literals
        /// </summary>
        /// <param name="control"></param>
        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    CommanFunction.PrepareControlForExport(current);
                }
            }
        }
        //===========To Export Grid================


       
        public void SendMail(string EmailID, string Subject, string Body, string Attachment)
        {
            string strError = string.Empty;
            try
            {                
                MailMessage mail = new MailMessage();
                mail.To.Add(EmailID);
                mail.From = new MailAddress("orchidtechnologiespune@Gmail.com");
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                //Attach file using FileUpload Control and put the file in memory stream
                if (!string.IsNullOrEmpty(Attachment))
                {
                    mail.Attachments.Add(new Attachment(Attachment));
                }
                SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.live.com"
                //Or Your SMTP Server Address for Hotmail
                smtp.Host = "smtp.gmail.com";
                //Or Your SMTP Server Address for Gmail
                //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential("orchidtechnologiespune@Gmail.com", "Software");
                //smtp.Credentials = new System.Net.NetworkCredential("vasant.thombre8@gmail.com", "revosolution");
                //Or your Smtp Email ID and Password
                smtp.EnableSsl = true;

                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
        }
        public string ToTitleCase(string str)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }   
        //============== Bind ComboBox=========//
        public CommanFunction()
        {
            //
            // TODO: Add constructor logic here
            //
        }

    }
#endregion
}