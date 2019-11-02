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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;

public partial class ChequePrint_ChequeReportView : System.Web.UI.Page
{

    #region variable

    public string[] arr;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
   
        arr = Request.QueryString["Id"].ToString().Split(',');

        try
        {

            if (arr[6].ToString() == "Bank Of Maharashtra")
            {
                ReportDocument cryRpt = new ReportDocument();
                cryRpt.Load(Server.MapPath("MaharashtraBank.rpt"));


                // For Pay
                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                crParameterDiscreteValue.Value = arr[0];
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["Pay"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                CRPt.ReportSource = cryRpt;

                // For date 



                ParameterFieldDefinitions crParameterFieldDefinitions1;
                ParameterFieldDefinition crParameterFieldDefinition1;
                ParameterValues crParameterValues1 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue1 = new ParameterDiscreteValue();

                crParameterDiscreteValue1.Value = arr[1];
                crParameterFieldDefinitions1 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition1 = crParameterFieldDefinitions["Date"];
                crParameterValues1 = crParameterFieldDefinition.CurrentValues;

                crParameterValues1.Clear();
                crParameterValues1.Add(crParameterDiscreteValue1);
                crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1);

                CRPt.ReportSource = cryRpt;


                // For A/c Pay 



                ParameterFieldDefinitions crParameterFieldDefinitions2;
                ParameterFieldDefinition crParameterFieldDefinition2;
                ParameterValues crParameterValues2 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue2 = new ParameterDiscreteValue();

                crParameterDiscreteValue2.Value = arr[2];
                crParameterFieldDefinitions2 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition2 = crParameterFieldDefinitions["A/C Payee"];
                crParameterValues2 = crParameterFieldDefinition.CurrentValues;

                crParameterValues2.Clear();
                crParameterValues2.Add(crParameterDiscreteValue2);
                crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2);

                CRPt.ReportSource = cryRpt;

                // Amount 
                ParameterFieldDefinitions crParameterFieldDefinitions3;
                ParameterFieldDefinition crParameterFieldDefinition3;
                ParameterValues crParameterValues3 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue3 = new ParameterDiscreteValue();


                crParameterDiscreteValue3.Value = "** "+arr[3]+ "/-" ;
                decimal amt = (decimal)0.00;
                amt = Convert.ToDecimal(arr[3]);
                string w_amt = WordAmount.convertcurrency(amt);
                w_amt = w_amt.Replace("Rupees", "");
                crParameterFieldDefinitions3 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition3 = crParameterFieldDefinitions["Amount"];
                crParameterValues3 = crParameterFieldDefinition.CurrentValues;

                crParameterValues3.Clear();
                crParameterValues3.Add(crParameterDiscreteValue3);
                crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3);

                CRPt.ReportSource = cryRpt;

                // ----- Rupee Symbol ------ 
                //ParameterFieldDefinitions crParameterFieldDefinitions_Symbol;
                //ParameterFieldDefinition crParameterFieldDefinition_Symbol;
                //ParameterValues crParameterValues_Symbol = new ParameterValues();
                //ParameterDiscreteValue crParameterDiscreteValue_Symbol = new ParameterDiscreteValue();


                //crParameterDiscreteValue3.Value =arr[7];
                //crParameterFieldDefinitions_Symbol = cryRpt.DataDefinition.ParameterFields;
                //crParameterFieldDefinition_Symbol = crParameterFieldDefinitions["R_Symbol"];
                //crParameterValues_Symbol = crParameterFieldDefinition.CurrentValues;

                //crParameterValues_Symbol.Clear();
                //crParameterValues_Symbol.Add(crParameterDiscreteValue_Symbol);
                //crParameterFieldDefinition_Symbol.ApplyCurrentValues(crParameterValues_Symbol);

                //CRPt.ReportSource = cryRpt;

                // End 


                ParameterFieldDefinitions crParameterFieldDefinitionsamt;
                ParameterFieldDefinition crParameterFieldDefinitionamt;
                ParameterValues crParameterValuesamt = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValueamt = new ParameterDiscreteValue();

                crParameterDiscreteValueamt.Value = w_amt;

                crParameterFieldDefinitionsamt = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinitionamt = crParameterFieldDefinitions["WordAmt"];
                crParameterValuesamt = crParameterFieldDefinition.CurrentValues;

                crParameterValuesamt.Clear();
                crParameterValuesamt.Add(crParameterDiscreteValueamt);
                crParameterFieldDefinitionamt.ApplyCurrentValues(crParameterValuesamt);

                CRPt.ReportSource = cryRpt;



                //  Firm 

                ParameterFieldDefinitions crParameterFieldDefinitions4;
                ParameterFieldDefinition crParameterFieldDefinition4;
                ParameterValues crParameterValues4 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue4 = new ParameterDiscreteValue();

                crParameterDiscreteValue4.Value = arr[4];
                crParameterFieldDefinitions4 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition4 = crParameterFieldDefinitions["Firm"];
                crParameterValues4 = crParameterFieldDefinition.CurrentValues;

                crParameterValues4.Clear();
                crParameterValues4.Add(crParameterDiscreteValue4);
                crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4);

                CRPt.ReportSource = cryRpt;


                // Authotiry 


                ParameterFieldDefinitions crParameterFieldDefinitions5;
                ParameterFieldDefinition crParameterFieldDefinition5;
                ParameterValues crParameterValues5 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue5 = new ParameterDiscreteValue();

                crParameterDiscreteValue5.Value = arr[5];
                crParameterFieldDefinitions5 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition5 = crParameterFieldDefinitions["Authority"];
                crParameterValues5 = crParameterFieldDefinition.CurrentValues;

                crParameterValues5.Clear();
                crParameterValues5.Add(crParameterDiscreteValue5);
                crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5);

                CRPt.ReportSource = cryRpt;
            }

            if (arr[6].ToString() == "Hdfc Bank")
            {
                ReportDocument cryRpt = new ReportDocument();
                cryRpt.Load(Server.MapPath("HDFCBank.rpt"));


                // For Pay
                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                crParameterDiscreteValue.Value = arr[0];
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["Pay"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                CRPt.ReportSource = cryRpt;

                // For date 



                ParameterFieldDefinitions crParameterFieldDefinitions1;
                ParameterFieldDefinition crParameterFieldDefinition1;
                ParameterValues crParameterValues1 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue1 = new ParameterDiscreteValue();

                crParameterDiscreteValue1.Value = arr[1];
                crParameterFieldDefinitions1 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition1 = crParameterFieldDefinitions["Date"];
                crParameterValues1 = crParameterFieldDefinition.CurrentValues;

                crParameterValues1.Clear();
                crParameterValues1.Add(crParameterDiscreteValue1);
                crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1);

                CRPt.ReportSource = cryRpt;


                // For A/c Pay 



                ParameterFieldDefinitions crParameterFieldDefinitions2;
                ParameterFieldDefinition crParameterFieldDefinition2;
                ParameterValues crParameterValues2 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue2 = new ParameterDiscreteValue();

                crParameterDiscreteValue2.Value = arr[2];
                crParameterFieldDefinitions2 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition2 = crParameterFieldDefinitions["A/C Payee"];
                crParameterValues2 = crParameterFieldDefinition.CurrentValues;

                crParameterValues2.Clear();
                crParameterValues2.Add(crParameterDiscreteValue2);
                crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2);

                CRPt.ReportSource = cryRpt;

                // Amount 



                ParameterFieldDefinitions crParameterFieldDefinitions3;
                ParameterFieldDefinition crParameterFieldDefinition3;
                ParameterValues crParameterValues3 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue3 = new ParameterDiscreteValue();


                crParameterDiscreteValue3.Value = "** "+arr[3] + "/-";
                decimal amt = (decimal)0.00;
                amt = Convert.ToDecimal(arr[3]);
                string w_amt = WordAmount.convertcurrency(amt);
                w_amt = w_amt.Replace("Rupees", "");
                crParameterFieldDefinitions3 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition3 = crParameterFieldDefinitions["Amount"];
                crParameterValues3 = crParameterFieldDefinition.CurrentValues;

                crParameterValues3.Clear();
                crParameterValues3.Add(crParameterDiscreteValue3);
                crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3);

                CRPt.ReportSource = cryRpt;
                
                // ------- Rupee Symbol ---------- 
                //ParameterFieldDefinitions crParameterFieldDefinitions_Symbol;
                //ParameterFieldDefinition crParameterFieldDefinition_Symbol;
                //ParameterValues crParameterValues_Symbol = new ParameterValues();
                //ParameterDiscreteValue crParameterDiscreteValue_Symbol = new ParameterDiscreteValue();


                //crParameterDiscreteValue_Symbol.Value = arr[7];
                
                //crParameterFieldDefinitions_Symbol = cryRpt.DataDefinition.ParameterFields;
                //crParameterFieldDefinition_Symbol = crParameterFieldDefinitions["R_Symbol"];
                //crParameterValues_Symbol = crParameterFieldDefinition.CurrentValues;

                //crParameterValues_Symbol.Clear();
                //crParameterValues_Symbol.Add(crParameterDiscreteValue3);
                //crParameterFieldDefinition_Symbol.ApplyCurrentValues(crParameterValues3);

                //CRPt.ReportSource = cryRpt;

                // Number To word 


                ParameterFieldDefinitions crParameterFieldDefinitionsamt;
                ParameterFieldDefinition crParameterFieldDefinitionamt;
                ParameterValues crParameterValuesamt = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValueamt = new ParameterDiscreteValue();

                crParameterDiscreteValueamt.Value = w_amt;

                crParameterFieldDefinitionsamt = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinitionamt = crParameterFieldDefinitions["WordAmt"];
                crParameterValuesamt = crParameterFieldDefinition.CurrentValues;

                crParameterValuesamt.Clear();
                crParameterValuesamt.Add(crParameterDiscreteValueamt);
                crParameterFieldDefinitionamt.ApplyCurrentValues(crParameterValuesamt);

                CRPt.ReportSource = cryRpt;



                //  Firm 

                ParameterFieldDefinitions crParameterFieldDefinitions4;
                ParameterFieldDefinition crParameterFieldDefinition4;
                ParameterValues crParameterValues4 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue4 = new ParameterDiscreteValue();

                crParameterDiscreteValue4.Value = arr[4];
                crParameterFieldDefinitions4 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition4 = crParameterFieldDefinitions["Firm"];
                crParameterValues4 = crParameterFieldDefinition.CurrentValues;

                crParameterValues4.Clear();
                crParameterValues4.Add(crParameterDiscreteValue4);
                crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4);

                CRPt.ReportSource = cryRpt;


                // Authotiry 


                ParameterFieldDefinitions crParameterFieldDefinitions5;
                ParameterFieldDefinition crParameterFieldDefinition5;
                ParameterValues crParameterValues5 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue5 = new ParameterDiscreteValue();

                crParameterDiscreteValue5.Value = arr[5];
                crParameterFieldDefinitions5 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition5 = crParameterFieldDefinitions["Authority"];
                crParameterValues5 = crParameterFieldDefinition.CurrentValues;

                crParameterValues5.Clear();
                crParameterValues5.Add(crParameterDiscreteValue5);
                crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5);

                CRPt.ReportSource = cryRpt;
            }

            if (arr[6].ToString() == "Saraswat Bank")
            {
                ReportDocument cryRpt = new ReportDocument();
                cryRpt.Load(Server.MapPath("SaraswatBank.rpt"));


                // For Pay
                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                crParameterDiscreteValue.Value = arr[0];
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["Pay"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                CRPt.ReportSource = cryRpt;

                // For date 



                ParameterFieldDefinitions crParameterFieldDefinitions1;
                ParameterFieldDefinition crParameterFieldDefinition1;
                ParameterValues crParameterValues1 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue1 = new ParameterDiscreteValue();

                crParameterDiscreteValue1.Value = arr[1];
                crParameterFieldDefinitions1 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition1 = crParameterFieldDefinitions["Date"];
                crParameterValues1 = crParameterFieldDefinition.CurrentValues;

                crParameterValues1.Clear();
                crParameterValues1.Add(crParameterDiscreteValue1);
                crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1);

                CRPt.ReportSource = cryRpt;


                // For A/c Pay 



                ParameterFieldDefinitions crParameterFieldDefinitions2;
                ParameterFieldDefinition crParameterFieldDefinition2;
                ParameterValues crParameterValues2 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue2 = new ParameterDiscreteValue();

                crParameterDiscreteValue2.Value = arr[2];
                crParameterFieldDefinitions2 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition2 = crParameterFieldDefinitions["A/C Payee"];
                crParameterValues2 = crParameterFieldDefinition.CurrentValues;

                crParameterValues2.Clear();
                crParameterValues2.Add(crParameterDiscreteValue2);
                crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2);

                CRPt.ReportSource = cryRpt;

                // Amount 



                ParameterFieldDefinitions crParameterFieldDefinitions3;
                ParameterFieldDefinition crParameterFieldDefinition3;
                ParameterValues crParameterValues3 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue3 = new ParameterDiscreteValue();


                crParameterDiscreteValue3.Value = "** "+arr[3] + "/-";
                decimal amt = (decimal)0.00;
                amt = Convert.ToDecimal(arr[3]);
                string w_amt = WordAmount.convertcurrency(amt);
                w_amt = w_amt.Replace("Rupees", "");
                crParameterFieldDefinitions3 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition3 = crParameterFieldDefinitions["Amount"];
                crParameterValues3 = crParameterFieldDefinition.CurrentValues;

                crParameterValues3.Clear();
                crParameterValues3.Add(crParameterDiscreteValue3);
                crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3);

                CRPt.ReportSource = cryRpt;

                ////// ------- Rupee Symbol ---------- 



                //ParameterFieldDefinitions crParameterFieldDefinitions_Symbol;
                //ParameterFieldDefinition crParameterFieldDefinition_Symbol;
                //ParameterValues crParameterValues_Symbol = new ParameterValues();
                //ParameterDiscreteValue crParameterDiscreteValue_Symbol = new ParameterDiscreteValue();


                //crParameterDiscreteValue_Symbol.Value = arr[7];
                //crParameterFieldDefinitions_Symbol = cryRpt.DataDefinition.ParameterFields;
                //crParameterFieldDefinition_Symbol = crParameterFieldDefinitions["R_Symbol"];
                //crParameterValues_Symbol = crParameterFieldDefinition.CurrentValues;

                //crParameterValues_Symbol.Clear();
                //crParameterValues_Symbol.Add(crParameterDiscreteValue_Symbol);
                //crParameterFieldDefinition_Symbol.ApplyCurrentValues(crParameterValues_Symbol);

                //CRPt.ReportSource = cryRpt;

                // Number To word 


                ParameterFieldDefinitions crParameterFieldDefinitionsamt;
                ParameterFieldDefinition crParameterFieldDefinitionamt;
                ParameterValues crParameterValuesamt = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValueamt = new ParameterDiscreteValue();

                crParameterDiscreteValueamt.Value = w_amt;

                crParameterFieldDefinitionsamt = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinitionamt = crParameterFieldDefinitions["WordAmt"];
                crParameterValuesamt = crParameterFieldDefinition.CurrentValues;

                crParameterValuesamt.Clear();
                crParameterValuesamt.Add(crParameterDiscreteValueamt);
                crParameterFieldDefinitionamt.ApplyCurrentValues(crParameterValuesamt);

                CRPt.ReportSource = cryRpt;



                //  Firm 

                ParameterFieldDefinitions crParameterFieldDefinitions4;
                ParameterFieldDefinition crParameterFieldDefinition4;
                ParameterValues crParameterValues4 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue4 = new ParameterDiscreteValue();

                crParameterDiscreteValue4.Value = arr[4];
                crParameterFieldDefinitions4 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition4 = crParameterFieldDefinitions["Firm"];
                crParameterValues4 = crParameterFieldDefinition.CurrentValues;

                crParameterValues4.Clear();
                crParameterValues4.Add(crParameterDiscreteValue4);
                crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4);

                CRPt.ReportSource = cryRpt;


                // Authotiry 


                ParameterFieldDefinitions crParameterFieldDefinitions5;
                ParameterFieldDefinition crParameterFieldDefinition5;
                ParameterValues crParameterValues5 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue5 = new ParameterDiscreteValue();

                crParameterDiscreteValue5.Value = arr[5];
                crParameterFieldDefinitions5 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition5 = crParameterFieldDefinitions["Authority"];
                crParameterValues5 = crParameterFieldDefinition.CurrentValues;

                crParameterValues5.Clear();
                crParameterValues5.Add(crParameterDiscreteValue5);
                crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5);

                CRPt.ReportSource = cryRpt;
            }





            if (arr[6].ToString() == "Bank of India")
            {
                ReportDocument cryRpt = new ReportDocument();
                cryRpt.Load(Server.MapPath("BankofIndia.rpt"));


                // For Pay
                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                crParameterDiscreteValue.Value = arr[0];
                crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["Pay"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                CRPt.ReportSource = cryRpt;

                // For date 



                ParameterFieldDefinitions crParameterFieldDefinitions1;
                ParameterFieldDefinition crParameterFieldDefinition1;
                ParameterValues crParameterValues1 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue1 = new ParameterDiscreteValue();

                crParameterDiscreteValue1.Value = arr[1];
                crParameterFieldDefinitions1 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition1 = crParameterFieldDefinitions["Date"];
                crParameterValues1 = crParameterFieldDefinition.CurrentValues;

                crParameterValues1.Clear();
                crParameterValues1.Add(crParameterDiscreteValue1);
                crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1);

                CRPt.ReportSource = cryRpt;


                // For A/c Pay 



                ParameterFieldDefinitions crParameterFieldDefinitions2;
                ParameterFieldDefinition crParameterFieldDefinition2;
                ParameterValues crParameterValues2 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue2 = new ParameterDiscreteValue();

                crParameterDiscreteValue2.Value = arr[2];
                crParameterFieldDefinitions2 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition2 = crParameterFieldDefinitions["A/C Payee"];
                crParameterValues2 = crParameterFieldDefinition.CurrentValues;

                crParameterValues2.Clear();
                crParameterValues2.Add(crParameterDiscreteValue2);
                crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2);

                CRPt.ReportSource = cryRpt;

                // Amount 



                ParameterFieldDefinitions crParameterFieldDefinitions3;
                ParameterFieldDefinition crParameterFieldDefinition3;
                ParameterValues crParameterValues3 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue3 = new ParameterDiscreteValue();


                crParameterDiscreteValue3.Value = "** "+ arr[3] + "/-";
                decimal amt = (decimal)0.00;
                amt = Convert.ToDecimal(arr[3]);
                string w_amt = WordAmount.convertcurrency(amt);
                w_amt = w_amt.Replace("Rupees", "");
                crParameterFieldDefinitions3 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition3 = crParameterFieldDefinitions["Amount"];
                crParameterValues3 = crParameterFieldDefinition.CurrentValues;

                crParameterValues3.Clear();
                crParameterValues3.Add(crParameterDiscreteValue3);
                crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3);

                CRPt.ReportSource = cryRpt;

                // Number To word 


                ParameterFieldDefinitions crParameterFieldDefinitionsamt;
                ParameterFieldDefinition crParameterFieldDefinitionamt;
                ParameterValues crParameterValuesamt = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValueamt = new ParameterDiscreteValue();

                crParameterDiscreteValueamt.Value = w_amt;

                crParameterFieldDefinitionsamt = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinitionamt = crParameterFieldDefinitions["WordAmt"];
                crParameterValuesamt = crParameterFieldDefinition.CurrentValues;

                crParameterValuesamt.Clear();
                crParameterValuesamt.Add(crParameterDiscreteValueamt);
                crParameterFieldDefinitionamt.ApplyCurrentValues(crParameterValuesamt);

                CRPt.ReportSource = cryRpt;



                //  Firm 

                ParameterFieldDefinitions crParameterFieldDefinitions4;
                ParameterFieldDefinition crParameterFieldDefinition4;
                ParameterValues crParameterValues4 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue4 = new ParameterDiscreteValue();

                crParameterDiscreteValue4.Value = arr[4];
                crParameterFieldDefinitions4 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition4 = crParameterFieldDefinitions["Firm"];
                crParameterValues4 = crParameterFieldDefinition.CurrentValues;

                crParameterValues4.Clear();
                crParameterValues4.Add(crParameterDiscreteValue4);
                crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4);

                CRPt.ReportSource = cryRpt;


                // Authotiry 


                ParameterFieldDefinitions crParameterFieldDefinitions5;
                ParameterFieldDefinition crParameterFieldDefinition5;
                ParameterValues crParameterValues5 = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue5 = new ParameterDiscreteValue();

                crParameterDiscreteValue5.Value = arr[5];
                crParameterFieldDefinitions5 = cryRpt.DataDefinition.ParameterFields;
                crParameterFieldDefinition5 = crParameterFieldDefinitions["Authority"];
                crParameterValues5 = crParameterFieldDefinition.CurrentValues;

                crParameterValues5.Clear();
                crParameterValues5.Add(crParameterDiscreteValue5);
                crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5);

                CRPt.ReportSource = cryRpt;
            }


        }

        catch { }
      



       
    }
    protected void CRPt_Init(object sender, EventArgs e)
    {

    }
}
