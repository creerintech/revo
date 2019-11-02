<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="ChequePrint.aspx.cs" Inherits="ChequePrint_ChequePrint" Title="Cheque Print" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
 <input type="hidden" id="TXTCRAMTHIDE" runat="server" value="0" />
 <script type="text/javascript" language="javascript">

  //Number to word

	function NumbertoWord()
	 {
    var junkVal="";
    
   junkVal = document.getElementById('<%=TXTCRAMTHIDE.ClientID%>').value;   
  
    val=junkVal;
   junkVal=Math.floor(junkVal);

    var obStr=new String(junkVal);

    numReversed=obStr.split("");

    actnumber=numReversed.reverse();

    if(Number(junkVal) >=0)
    {
        //do nothing

    }
    else{

        alert('wrong Number cannot be converted');
        return false;
    }
    if(Number(junkVal)==0){

        document.getElementById('container').innerHTML=obStr+''+'Rupees Zero Only';
        return false;
    }
    if(actnumber.length>9){
        alert('Oops!!!! the Number is too big to covertes');

        return false;
    }

 

    var iWords=["Zero", " One", " Two", " Three", " Four", " Five", " Six", " Seven", " Eight", " Nine"];

    var ePlace=['Ten', ' Eleven', ' Twelve', ' Thirteen', ' Fourteen', ' Fifteen', ' Sixteen', ' Seventeen', ' Eighteen', ' Nineteen'];

    var tensPlace=['dummy', ' Ten', ' Twenty', ' Thirty', ' Forty', ' Fifty', ' Sixty', ' Seventy', ' Eighty', ' Ninety' ];

 

    var iWordsLength=numReversed.length;

    var totalWords="";

    var inWords=new Array();

    var finalWord="";

    j=0;

    for(i=0; i<iWordsLength; i++){

        switch(i)

        {

        case 0:

            if(actnumber[i]==0 || actnumber[i+1]==1 ) {

                inWords[j]='';

            }

            else {

                inWords[j]=iWords[actnumber[i]];

            }

            inWords[j]=inWords[j]+'';

            break;

        case 1:

            tens_complication();

            break;

        case 2:

            if(actnumber[i]==0) {

                inWords[j]='';

            }

            else if(actnumber[i-1]!=0 && actnumber[i-2]!=0) {

                inWords[j]=iWords[actnumber[i]]+' Hundred and';

            }

            else {

                inWords[j]=iWords[actnumber[i]]+' Hundred';

            }

            break;

        case 3:

            if(actnumber[i]==0 || actnumber[i+1]==1) {

                inWords[j]='';

            }

            else {

                inWords[j]=iWords[actnumber[i]];

            }

            if(actnumber[i+1] != 0 || actnumber[i] > 0){

                inWords[j]=inWords[j]+" Thousand";

            }

            break;

        case 4:

            tens_complication();

            break;

        case 5:

            if(actnumber[i]==0 || actnumber[i+1]==1) {

                inWords[j]='';

            }

            else {

                inWords[j]=iWords[actnumber[i]];

            }

            if(actnumber[i+1] != 0 || actnumber[i] > 0){

                inWords[j]=inWords[j]+" Lakh";

            }

            break;

        case 6:

            tens_complication();

            break;

        case 7:

            if(actnumber[i]==0 || actnumber[i+1]==1 ){

                inWords[j]='';

            }

            else {

                inWords[j]=iWords[actnumber[i]];

            }

            inWords[j]=inWords[j]+" Crore";

            break;

        case 8:

            tens_complication();

            break;

        default:

            break;

        }

        j++;

    }
 

    function tens_complication() {

        if(actnumber[i]==0) {

            inWords[j]='';

        }

        else if(actnumber[i]==1) {

            inWords[j]=ePlace[actnumber[i-1]];
        }

        else {

            inWords[j]=tensPlace[actnumber[i]];

        }
    }

    inWords.reverse();

    for(i=0; i<inWords.length; i++) {
        finalWord+=inWords[i];

    }
return finalWord;
}
    </script>

   
<script type="text/javascript" language="javascript">
function test_skill() {
   var junkVal = document.getElementById('<% =txtAmount.ClientID %>').value;
    junkVal=Math.floor(junkVal);
    var obStr=new String(junkVal);
    numReversed=obStr.split("");
    actnumber=numReversed.reverse();

    if(Number(junkVal) >=0){
        //do nothing
    }
    else{
        alert('wrong Number cannot be converted');
        return false;
    }
    if(Number(junkVal)==0){
        document.getElementById('<%=lblAmt.ClientID %>').value = obStr + '' + 'Rupees Zero Only';
        return false;
    }
    if(actnumber.length>9){
        alert('Oops!!!! the Number is too big to covertes');
        return false;
    }

    var iWords=["Zero", " One", " Two", " Three", " Four", " Five", " Six", " Seven", " Eight", " Nine"];
    var ePlace=['Ten', ' Eleven', ' Twelve', ' Thirteen', ' Fourteen', ' Fifteen', ' Sixteen', ' Seventeen', ' Eighteen', ' Nineteen'];
    var tensPlace=['dummy', ' Ten', ' Twenty', ' Thirty', ' Forty', ' Fifty', ' Sixty', ' Seventy', ' Eighty', ' Ninety' ];

    var iWordsLength=numReversed.length;
    var totalWords="";
    var inWords=new Array();
    var finalWord="";
    j=0;
    for(i=0; i<iWordsLength; i++){
        switch(i)
        {
        case 0:
            if(actnumber[i]==0 || actnumber[i+1]==1 ) {
                inWords[j]='';
            }
            else {
                inWords[j]=iWords[actnumber[i]];
            }
            inWords[j]=inWords[j];
            break;
        case 1:
            tens_complication();
            break;
        case 2:
            if(actnumber[i]==0) {
                inWords[j]='';
            }
            else if(actnumber[i-1]!=0 && actnumber[i-2]!=0) {
                inWords[j]=iWords[actnumber[i]]+' Hundred and';
            }
            else {
                inWords[j]=iWords[actnumber[i]]+' Hundred';
            }
            break;
        case 3:
            if(actnumber[i]==0 || actnumber[i+1]==1) {
                inWords[j]='';
            }
            else {
                inWords[j]=iWords[actnumber[i]];
            }
            if(actnumber[i+1] != 0 || actnumber[i] > 0){
                inWords[j]=inWords[j]+" Thousand";
            }
            break;
        case 4:
            tens_complication();
            break;
        case 5:
            if(actnumber[i]==0 || actnumber[i+1]==1 ) {
                inWords[j]='';
            }
            else {
                inWords[j]=iWords[actnumber[i]];
            }
            inWords[j]=inWords[j]+" Lakh";
            break;
        case 6:
            tens_complication();
            break;
        case 7:
            if(actnumber[i]==0 || actnumber[i+1]==1 ){
                inWords[j]='';
            }
            else {
                inWords[j]=iWords[actnumber[i]];
            }
            inWords[j]=inWords[j]+" Crore";
            break;
        case 8:
            tens_complication();
            break;
        default:
            break;
        }
        j++;
    }

    function tens_complication() {
        if(actnumber[i]==0) {
            inWords[j]='';
        }
        else if(actnumber[i]==1) {
            inWords[j]=ePlace[actnumber[i-1]];
        }
        else {
            inWords[j]=tensPlace[actnumber[i]];
        }
    }
    inWords.reverse();
    for(i=0; i<inWords.length; i++) {
        finalWord+=inWords[i];
    }
        return finalWord;
  
       
}

function paisa_conver()
{
 var val = document.getElementById("<%= txtAmount.ClientID %>").value;
 if(isNaN(val)|| val=="" || parseInt(val)==0)
 {
    document.getElementById('<%=lblAmt.ClientID %>').value="0";
 }
 else
 {
        var finalWord1 = test_skill();
            var finalWord2;
           
        if(val.indexOf('.')!=-1)
    {
        val = val.substring(val.indexOf('.') + 1, val.length);
     
        document.getElementById("<%= TXTCRAMTHIDE.ClientID %>").value = val.substring(0, 2);

        if (val.length == 0) {

            finalWord2 = " paise zero only";
          }
          else {
             // alert("ELSE");
             

              finalWord2 = "paise "+ NumbertoWord() + " only";
              }
    }
        else{
            finalWord2 = "paise zero only";
      }
     
    document.getElementById('<%=lblAmt.ClientID %>').value = finalWord1 + " and " + finalWord2;
   }
   
}
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
Cheque Print
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="UpEntry" runat="server">
        <ContentTemplate>
        <table width="100%"><tr><td align="center">
           <table width="75%" ><tr><td align="center">
            <fieldset id="f1" class="FieldSet" runat="server">
                <table cellspacing="6px" style="background-color: #FFFAF0" width="100%">
                    <tr>
                        <td align="left" class="Label" width="20%">
                            A/C Payee</td>
                        <td align="left" colspan="7">
                            <asp:RadioButtonList ID="radionAcpay" runat="server" CssClass="RadioButton" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                                           
                        </td>
                        <td align="left" class="Label">
                            Date</td>
                        <td align="left">
                             <asp:TextBox ID="txtTODate" runat="server" CssClass="TextBox"></asp:TextBox>
                           
                                            <ajax:CalendarExtender ID="txtTODate_CalendarExtender"  Format="dd-MMM-yyyy"
                                runat="server" Enabled="True" TargetControlID="txtTODate" PopupButtonID="ImageButton1">
                            </ajax:CalendarExtender>
                           
                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/Images/Icon/DateSelector.png" CausesValidation="False" 
                    CssClass="Imagebutton" TabIndex="10" />
                                           
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="Label">
                            &nbsp;Pay</td>
                        <td align="left" colspan="9">
                            <asp:TextBox ID="txtpay" runat="server" CssClass="TextBox" 
                                Width="550px"></asp:TextBox>
                                           
                                        <asp:RequiredFieldValidator ID="Rq_V1" runat="server" ControlToValidate="txtpay"
                                            Display="None" ErrorMessage="Name is Required" SetFocusOnError="True"
                                            ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <ajax:ValidatorCalloutExtender ID="Rq_V1_ValidatorCalloutExtender" runat="server"
                                            TargetControlID="Rq_V1" 
                                            WarningIconImageUrl="~/Images/Icon/Warning.png">
                                        </ajax:ValidatorCalloutExtender>
                                        
                                        <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
      TargetControlID="txtpay" CompletionInterval="100"                             
      UseContextKey="True" FirstRowSelected ="true" ShowOnlyCurrentWordInCompletionListItem="true"
       ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender"
      CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
    </ajax:AutoCompleteExtender>
                                           
                        </td>
                    </tr>
                    <tr>
                       
                        <td align="left" class="Label">
                            Amount</td>
                        <td align="left">
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBox" 
                                EnableViewState="False"  onkeyup="paisa_conver();"
                                ontextchanged="txtAmount_TextChanged" Width="300px"></asp:TextBox>
                                           
                                        <asp:RequiredFieldValidator ID="Rq_V2" runat="server" ControlToValidate="txtAmount"
                                            Display="None" ErrorMessage="Enter Amount" SetFocusOnError="True"
                                            ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <ajax:ValidatorCalloutExtender ID="Rq_V2_ValidatorCalloutExtender" runat="server"
                                            TargetControlID="Rq_V2" 
                                            WarningIconImageUrl="~/Images/Icon/Warning.png">
                                        </ajax:ValidatorCalloutExtender>
                                           
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="Label">
                            Rupees</td>
                        <td align="left" colspan="9">
                        <asp:TextBox ID="lblAmt" runat="server" CssClass="TextBox" 
                                width="550px"
                                ></asp:TextBox>
                           
                    </tr>
                    <tr>
                        <td class="Label" >
                            &nbsp;Select Bank&nbsp;
                            </td>
                            <div>
                        <td align="left" colspan="8">
                            <asp:RadioButtonList ID="radionBank" runat="server" 
                                CssClass="RadioButton" RepeatDirection="Horizontal" ValidationGroup="chk">
                                <asp:ListItem Selected="True">Bank Of Maharashtra</asp:ListItem>
                                <asp:ListItem>Hdfc Bank</asp:ListItem>
                                <asp:ListItem>Saraswat Bank</asp:ListItem>
                                 <asp:ListItem>Bank of India</asp:ListItem>
                                
                            </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="Label" >
                            Firm Name</td>
                        <td align="left" colspan="3">
                            <asp:RadioButtonList ID="radionFirm" runat="server" 
                                CssClass="RadioButton" RepeatDirection="Horizontal" ValidationGroup="chk">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="left" colspan="3">
                            <asp:DropDownList ID="ddlFirm" runat="server" CssClass="ComboBox" Width="300px">
                             <asp:ListItem Text="--Select Firm-- " Value="0"></asp:ListItem>
                                <asp:ListItem >For KARIA DEVELOPERS </asp:ListItem>
                                <%--<asp:ListItem>For Tripple A Enterprises</asp:ListItem>
                                <asp:ListItem>For MNM ENTERPRISES</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                       <td></td>
                        <td align="left" colspan="3">
                            <asp:DropDownList ID="ddlAuto" runat="server" CssClass="ComboBox" Width="300px">
                                <asp:ListItem Text="--Select Authority--" Value="0"></asp:ListItem>
                                <asp:ListItem>POA</asp:ListItem>
                                <asp:ListItem>PROPRIETOR</asp:ListItem>
                                <asp:ListItem>AUTHORISED SIGNATORY</asp:ListItem>
                                <asp:ListItem>DIRECTOR</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                    
                        <td align="center" align="center" colspan="10">
                        <table width="35%" align="center">
                        <tr><td>
                        
                            <asp:Button ID="ImgPrint" runat="server" CssClass="button" Text="Print"
                                ImageUrl="~/Images/Icon/Print-Button.png" onclick="ImgPrint_Click" 
                                ToolTip="Print" ValidationGroup="Add" OnClientClick="aspnetForm.target ='_blank';" />
                        <%--</td>
                        <td>--%>
                        <asp:Button ID="BtnCancel" runat="server" CssClass="button" Text="Cancel"
                                onclick="BtnCancel_Click" ToolTip="Cancel"/>
                        </td>
                        </tr>
                        </table>
                        </td>
                    </tr>
                    
                      </table>
            </fieldset>
            </td></tr></table>
            </td>
            </tr>
            </table>
                </ContentTemplate>
                <Triggers >
                <asp:PostBackTrigger ControlID="ImgPrint" />
                </Triggers>
                </asp:UpdatePanel>
</asp:Content>

