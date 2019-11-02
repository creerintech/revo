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
using System.Collections;

/// <summary>
/// Summary description for WordAmount
/// </summary>
public class WordAmount
{
     
    public static string convertcurrency(decimal amount) 
    {
        string[] txtarray=new string[]{"","","Hundred","Thousand","Thousand","Lac","Lac","Crore","Crore"};
        decimal IntVal;     
        decimal DecVal;
        string RetVal="";
        
        IntVal=Convert.ToDecimal(Convert.ToInt32(amount));
        DecVal=Convert.ToDecimal(Math.Round(amount-Convert.ToInt32(amount),2))*100;
        if (IntVal.ToString().Length > 9)
        {
            //break;
        }
        switch (IntVal.ToString().Length)
        {
            case 1:
                RetVal=TwoDigit(IntVal);
                break;
            case 2:
                RetVal = TwoDigit(IntVal);
                break;
            case 3:
                RetVal = TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(0, 1))) + " Hundred and " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(1, 2)));
                break;
            case 4:
                RetVal = TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(0, 1))) + " Thousand " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(1, 1))) + " Hundred " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(2, 2)));
                break;
            case 5:
                RetVal = TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(0, 2))) + " Thousand " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(2, 1))) + " Hundred " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(3, 2)));
                break;
            case 6:
                RetVal = TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(0, 1))) + " Lac " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(1, 2))) + " Thousand " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(3, 1))) + " Hundred " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(4, 2)));
                break;
            case 7:
                RetVal = TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(0, 2))) + " Lac " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(2, 2))) + " Thousand " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(4, 1))) + " Hundred " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(5, 2)));
                break;
            case 8:
                RetVal = TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(0, 1))) + " Crore " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(1, 2))) + " Lac " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(3, 2))) + " Thousand " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(5, 1))) + " Hundred " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(6, 2)));
                break;
            case 9:
                RetVal = TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(0, 2))) + " Crore " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(2, 2))) + " Lac " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(4, 2))) + " Thousand " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(6, 1))) + " Hundred " + TwoDigit(Convert.ToDecimal(Convert.ToString(IntVal).Substring(7, 2)));
            break;
        }
        RetVal=RetVal.ToString().Trim();
        RetVal = RetVal.Replace("Hundred", "Hundred");
        RetVal = RetVal.Replace("Thousand  Hundred", "Thousand");
        RetVal = RetVal.Replace("Lac  Thousand", "Lac");
        RetVal = RetVal.Replace("Crore  Lac", "Crore");
        RetVal = RetVal.Replace(" and ", " ");
        RetVal = RetVal.Replace(" ", " ");
        if(DecVal > 0)
        {
            RetVal = "Rupees " + RetVal + " and Paise " + TwoDigit(DecVal) + " Only";
        }
        else
        {
            RetVal = "Rupees " + RetVal + " Only";
        }
        RetVal = RetVal.Replace(" Rupees ", " Rupees");
        RetVal = RetVal.Replace(" Only", " Only");
        RetVal = RetVal.Replace(" and Paise Only", " Only");
        RetVal = RetVal.Replace("Rupees Only", "Nil").Trim();
        RetVal = RetVal.Replace("Hundred Only", "Hundred Only");
        RetVal = RetVal.Replace("Thousand Only", "Thousand Only");
        RetVal = RetVal.Replace("Lac Only", "Lac Only");
        RetVal = RetVal.Replace("Crore Only", "Crore Only");
        RetVal = RetVal.Replace("Only", "Only");
        RetVal = RetVal.Replace("  ", " ");

        return RetVal;

    }
    public static string TwoDigit(decimal Number)
    {
        string PreFix="";
        string PostFix="";
        string result = "";
        if(Number<20)
        {
            //TwoDigit = OneDigit(Convert.ToInt32(Number));
            result= OneDigit(Convert.ToInt32(Number));
        }
        else
        {
            //string PreFix;
            //string PostFix;            
            string[] TensArray=new string[]{"","","Twenty","Thirty","Forty","Fifty","Sixty","Seventy","Eighty","Ninety"};
            PreFix=TensArray[Convert.ToInt32(Convert.ToString(Number).Substring(0, 1))];
            PostFix = OneDigit(Convert.ToInt32(Convert.ToString(Number).Substring(1, 1)));
            //TwoDigit = (PreFix + " " + PostFix).Trim();
            result=(PreFix + " " + PostFix).Trim();
        }
        return result;
    }
    public static string OneDigit(int Number)
    {
        string strret=""; 
        switch(Number)
        {
            case 0: strret = "";
                break;
            case 1: strret = "One";
                break;
            case 2: strret = "Two";
                break;
            case 3: strret = "Three";
                break;
            case 4: strret = "Four";
                break;
            case 5: strret = "Five";
                break;
            case 6: strret = "Six";
                break;
            case 7: strret = "Seven";
                break;
            case 8: strret = "Eight";
                break;
            case 9: strret = "Nine";
                break;
            case 10: strret = "Ten";
                break;
            case 11: strret = "Eleven";
                break;
            case 12: strret = "Twelve";
                break;
            case 13: strret = "Thirteen";
                break;
            case 14: strret = "Fourteen";
                break;
            case 15: strret = "Fifteen";
                break;
            case 16: strret = "Sixteen";
                break;
            case 17: strret = "Seventeen";
                break;
            case 18: strret = "Eighteen";
                break;
            case 19: strret = "Ninteen";
                break;                
        }
        return strret;
    } 
    //public static string SpellNumber(string MyNumber)
    //{
                //int Dollars;
                //int Cents;
                //int temp;
                //MyNumber = Convert.ToString(MyNumber.Trim());
                //DecimalPlace = Convert.ToString(MyNumber, ".");

                //        'Main Function
                //Function SpellNumber(ByVal MyNumber) As String
                //    Dim Dollars, Cents, temp
                //    Dim DecimalPlace, count
                //    ReDim Place(9) As String
                //    Place(2) = " Thousand "
                //    Place(3) = " Million "
                //    Place(4) = " Billion "
                //    Place(5) = " Trillion "
                //    ' String representation of amount.
                //    MyNumber = Trim(Str(MyNumber))
                //    ' Position of decimal place 0 if none.
                //    DecimalPlace = InStr(MyNumber, ".")
                //    ' Convert cents and set MyNumber to dollar amount.
                //    If DecimalPlace > 0 Then
                //        Cents = GetTens(Left(Mid(MyNumber, DecimalPlace + 1) + _
                //                  "00", 2))
                //        MyNumber = Trim(Left(MyNumber, DecimalPlace - 1))
                //    End If
                //    count = 1
                //    Do While CStr(MyNumber) <> ""
                //        temp = GetHundreds(Right(MyNumber, 3))
                //        If temp <> "" Then Dollars = temp + Place(count) + Dollars
                //        If Len(MyNumber) > 3 Then
                //            MyNumber = Left(MyNumber, Len(MyNumber) - 3)
                //        Else
                //            MyNumber = ""
                //        End If
                //        count = count + 1
                //    Loop
                //    Select Case Dollars
                //        Case ""
                //            Dollars = ""
                //        Case "One"
                //            Dollars = "One Only"
                //         Case Else
                //            Dollars = Dollars + " Only"
                //    End Select
                //    Select Case Cents
                //        Case ""
                //            Cents = ""
                //        Case "One"
                //            Cents = " and One Paise"
                //              Case Else
                //            Cents = " and " + Cents + " Paise"
                //    End Select
                //    SpellNumber = "Rupees " + Dollars + Cents
                //End Function
    //}

    //public void GetHundreds(int MyNumber) 
    //{
    //    string Result;
    //    if(MyNumber==0)
    //    {
            
    //    }
    //    MyNumber=Right("000" + MyNumber, 3);
    //    if (Mid(MyNumber, 1, 1) != "0")
    //    {
    //        Result = GetDigit(Mid(MyNumber, 1, 1)) + " Hundred ";
    //    }
    //    if (Mid(MyNumber, 2, 1) != "0")
    //    {
    //        Result = Result + GetTens(Mid(MyNumber, 2));
    //    }
    //    else
    //    {  
    //        Result = Result + GetDigit(Mid(MyNumber, 3));
    //    }    
    //    GetHundreds = Result;
    //}
    //public void GetTens(string TensText)
    //{
    //    string Result="";
    //    if(Left(TensText, 1) == 1)
    //    {
    //        switch(TensText)
    //        {
    //            case 10: Result = "Ten";
    //            case 11: Result = "Eleven";
    //            case 12: Result = "Twelve";
    //            case 13: Result = "Thirteen";
    //            case 14: Result = "Fourteen";
    //            case 15: Result = "Fifteen";
    //            case 16: Result = "Sixteen";
    //            case 17: Result = "Seventeen";
    //            case 18: Result = "Eighteen";
    //            case 19: Result = "Nineteen";                
    //        }            
    //    }
    //    else
    //    {
    //        switch(Left(TensText, 1))
    //        {
    //            case 2: Result = "Twenty ";
    //            case 3: Result = "Thirty ";
    //            case 4: Result = "Forty ";
    //            case 5: Result = "Fifty ";
    //            case 6: Result = "Sixty ";
    //            case 7: Result = "Seventy ";
    //            case 8: Result = "Eighty ";
    //            case 9: Result = "Ninety ";
    //        }
    //         Result = Result + GetDigit(Right(TensText, 1));
    //    }
    //    GetTens = Result;
    //}
    //public void GetDigit(string Digit)
    //{
    //    switch(Digit)
    //    {
    //        case 1: GetDigit = "One";
    //        case 2: GetDigit = "Two";
    //        case 3: GetDigit = "Three";
    //        case 4: GetDigit = "Four";
    //        case 5: GetDigit = "Five";
    //        case 6: GetDigit = "Six";
    //        case 7: GetDigit = "Seven";
    //        case 8: GetDigit = "Eight";
    //        case 9: GetDigit = "Nine";
    //        default: GetDigit = "";
    //    }
    //}
	public WordAmount()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
