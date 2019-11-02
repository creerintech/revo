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


using System.Collections.Generic;
using System.Data.SqlClient;
using MayurInventory.Utility;
using MayurInventory.EntityClass;
using MayurInventory.DB;
using MayurInventory.DataModel;
using MayurInventory.DALSQLHelper;
public partial class ChequePrint_ChequePrint : System.Web.UI.Page
{


   # region variable
    public  string AcPay = string.Empty;
    public  string name = string.Empty;
    public  string date = string.Empty;
    public  string Amount = string.Empty;
    public  string firmName = string.Empty;
    public  string Authority = string.Empty;
    public string R_Symbol = string.Empty;

    public static string Bank = string.Empty;

   #endregion
    //public void loaddata()
    //{
    //    txtTODate.Text = DateTime.Now.Date.ToShortDateString().ToString();
    //    radionAcpay.SelectedIndex = 0;        
    //    radionBank.SelectedIndex = 2;
    //    txtpay.Text = "Vasant Thombre";
    //    txtAmount.Text = "20000";
    //    //rdo_RupeeSymbol.SelectedIndex = 0;
    //    radionFirm.SelectedIndex = 0;
    //    ddlFirm.SelectedIndex = 1;
    //    ddlAuto.SelectedIndex = 2;
    //    txtTODate.Text = DateTime.Now.Date.ToShortDateString().ToString();

    //}
    protected void Page_Load(object sender, EventArgs e)
    {

        txtTODate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
        //makeEmptyform();


     //   //foreach (ListItem li in radionBank.Items)
     //   ////{
     //   radionBank.Items[0].Selected = true;
     //   ////}
     // // foreach (ListItem li in radionFirm.Items)
     ////  {
     //       radionFirm.Items[0].Selected = true;
     // // }

     ////  foreach (ListItem li in radionAcpay.Items)
     ////  {
     //       radionAcpay.Items[0].Selected = true;
     // //  }
        //loaddata();// Default Data will Load at the Time of Form load.......
    }
    public void makeEmptyform()
    {
        txtAmount.Text = "";
        txtpay.Text = "";
        lblAmt.Text = "";
        ddlAuto.SelectedValue = "0";
        ddlFirm.SelectedValue = "0";
        
        radionAcpay.SelectedValue = "0";
        radionBank.SelectedValue = "0";
        radionFirm.SelectedValue = "0";
       
    }
    public void getdata()
    {
        try
        {
            date = Convert.ToDateTime(txtTODate.Text).ToString("dd/MMM/yyyy");
            for (int i = 0; i < radionAcpay.Items.Count; i++)
            {
                if (radionAcpay.Items[i].Selected == true)
                {
                    AcPay += radionAcpay.Items[i].Value;
                    if (AcPay == "Yes")
                    {
                        AcPay = "A/c Payee";
                    }
                    else
                    {
                        AcPay = "";
                    }
                }
            }
            for (int i = 0; i < radionBank.Items.Count; i++)
            {
                if (radionBank.Items[i].Selected == true)
                {
                    Bank  += radionBank.Items[i].Value;

                }
            }
            name = txtpay.Text.Trim();
            Amount =Convert.ToString(txtAmount.Text.Trim());
            for (int i = 0; i < radionFirm.Items.Count; i++)
            {
                if (radionFirm.Items[i].Selected == true)
                {
                    firmName += radionFirm.Items[i].Value;
                    if (firmName == "Yes")
                    {
                        firmName = Convert.ToString(ddlFirm.SelectedValue);
                        Authority = Convert.ToString(ddlAuto.SelectedValue);
                    }
                    else
                    {
                        firmName = "";
                        Authority = "";
                    }
                }
            }
            //---------------------- For Rupee Symbol ----------------
            //for (int i = 0; i < rdo_RupeeSymbol.Items.Count; i++)
            //{
            //    if (rdo_RupeeSymbol.Items[i].Selected == true)
            //    {
            //        R_Symbol += rdo_RupeeSymbol.Items[i].Value;
            //        if (R_Symbol == "Yes")
            //        {
            //            R_Symbol = "`";
            //        }
            //        else
            //        {
            //            R_Symbol = "";                        
            //        }
            //    }
            //}
            //--------------------------- END --------------------------
            ArrayList arr = new ArrayList();
            //if (name != "")
            //{
                  arr.Add(name);
            //}            
            //if (date != "")
            //{
                  arr.Add(date);
            //}
            //if (AcPay != "")
            //{
                  arr.Add(AcPay);
            //}
            //if (Amount != "")
            //{
                  arr.Add(Amount);                
            //}
            //if (firmName != "")
            //{
                  arr.Add(firmName);
            //}
            //if (Authority != "")
            //{
                  arr.Add(Authority);
            //}
            //if (Bank != "")
            //{
                arr.Add(Bank);
            //}
            //if (R_Symbol != "")
            //{
             // arr.Add(R_Symbol);
            //}

            AcPay = string.Empty;
            name = string.Empty;
            date = string.Empty;
            Amount = string.Empty;
            firmName = string.Empty;
            Authority = string.Empty;
            Bank = string.Empty;
            R_Symbol = string.Empty;

            //Cache["test"] = arr;            
            string arry = String.Join(",", ((string[])arr.ToArray(typeof(String))));
            arr = null;
            Response.Redirect("ChequeReportView.aspx?Id=" + arry);
         makeEmptyform();
        }
        catch
        {
          
        }    
    }  
    protected void btnprint_Click(object sender, EventArgs e)
    {
       
    }
    protected void ImgPrint_Click(object sender, EventArgs e)
    {
            getdata();
    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        decimal  amt = Convert.ToDecimal(txtAmount.Text.Trim());
        string w_amt = WordAmount.convertcurrency(amt);
       w_amt= w_amt.Replace("Rupees", "");
        lblAmt.Text = w_amt.ToString();
        

    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        makeEmptyform();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        DMPaymentVoucher Obj_CN = new DMPaymentVoucher();
        String[] SearchList = Obj_CN.GetSuggestedRecordForChequePrinting(prefixText);
        return SearchList;
    }
}
