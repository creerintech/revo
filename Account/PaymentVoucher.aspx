<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="PaymentVoucher.aspx.cs" Inherits="Account_PaymentVoucher" Title="Payment Voucher" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <input type="hidden" id="hiddenbox" runat="server" value="" />
    <input type="hidden" id="TXTCRAMTHIDE" runat="server" value="0" />
    <script type="text/javascript" language="javascript">

        function AdjustAmount()
        {
        var _txtDrAmt = document.getElementById('<%= txtDrAmt.ClientID %>');   
        var _txtCrAmt = document.getElementById('<%= txtCrAmt.ClientID %>');   
        var _TxtTotalDebit = document.getElementById('<%= TxtTotalDebit.ClientID %>');   
        var _TxtTotalCredit = document.getElementById('<%= TxtTotalCredit.ClientID %>');       

        var DebitAmt=0,CreditAmt=0,TotalDebit=0,TotalCredit=0;    

        if(_txtCrAmt.value != "") CreditAmt=_txtCrAmt.value; 

        _txtDrAmt.value=parseFloat(CreditAmt).toFixed(2);
        _TxtTotalDebit.value=parseFloat(CreditAmt).toFixed(2);
        _TxtTotalCredit.value=parseFloat(CreditAmt).toFixed(2);
        paisa_conver();
        }

        function UpdateEquipFunction() {

        var value = document.getElementById('<%=txtVoucherNo.ClientID%>').value;

        if (value == "") {
            document.getElementById('<%= hiddenbox.ClientID%>').value = "1";
        }
        else {
            if (confirm("Would You Want To Upadte The Record ?") == true) {
                document.getElementById('<%= hiddenbox.ClientID%>').value = "0";
                return false;
            }
            else {
                document.getElementById('<%= hiddenbox.ClientID%>').value = "100";

            }
        }
    }

    function DeleteEquipFunction() {

        var value = document.getElementById('<%=txtVoucherNo.ClientID%>').value;

        if (value == "") {
            document.getElementById('<%= hiddenbox.ClientID%>').value = "1";
        }
        else {
            if (confirm("Would You like To Delete The Record ?") == true) {
                document.getElementById('<%= hiddenbox.ClientID%>').value = "0";
                return false;
            }
            else {
                document.getElementById('<%= hiddenbox.ClientID%>').value = "100";

            }
        }
    }
    </script>

    <script type="text/javascript" language="javascript">

  //Number to word

	function NumbertoWord()
	 {
    var junkVal="";
    // document.getElementById('<%=txtAmtInWrds.ClientID%>').value="";
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
// document.getElementById('<%=txtAmtInWrds.ClientID%>').value=finalWord;
return finalWord;
}
    </script>

   
<script type="text/javascript" language="javascript">
function test_skill() {
   var junkVal = document.getElementById('<% =txtCrAmt.ClientID %>').value;
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
        document.getElementById('<%=txtAmtInWrds.ClientID %>').value = obStr + '' + 'Rupees Zero Only';
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
 var val = document.getElementById("<%= txtCrAmt.ClientID %>").value;
 if(isNaN(val)|| val=="" || parseInt(val)==0)
 {
    document.getElementById('<%=txtAmtInWrds.ClientID %>').value="0";
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
             // document.getElementById('<%=txtAmtInWrds.ClientID %>').value = val;

              finalWord2 = "paise "+ NumbertoWord() + " only";
              }
    }
        else{
            finalWord2 = "paise zero only";
      }
     
    document.getElementById('<%=txtAmtInWrds.ClientID %>').value = finalWord1 + " and " + finalWord2;
   }
   
}
    </script>
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" >
    <ProgressTemplate>            
    <div id="progressBackgroundFilter"></div>
    <div id="processMessage">   
    <center><span class="SubTitle">Loading....!!! </span></center>
    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
    </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>
      

   
<script type="text/javascript" language="javascript">
function CalculateAmtAsPerGrid(objGrid)
{
var Txt_Cramt=document.getElementById("<%=txtCrAmt.ClientID %>");
var _GridDetails = document.getElementById('<%= GridInvoice.ClientID %>');  
var rowIndex=objGrid.offsetParent.parentNode.rowIndex;
var RecivedAmt=(_GridDetails.rows[rowIndex].cells[4].children[0]);
var OutstandingAmt=(_GridDetails.rows[rowIndex].cells[5].children[0]);
var RecivedAmtSample=(_GridDetails.rows[rowIndex].cells[6].children[0]);
 var total = 0;

if (Txt_Cramt.value=="" || isNaN(Txt_Cramt.value))
{
Txt_Cramt.value=0;           
}
if (RecivedAmt.value=="" || isNaN(RecivedAmt.value))
{
RecivedAmt.value=0;           
}
if (OutstandingAmt.value=="" || isNaN(OutstandingAmt.value))
{
OutstandingAmt.value=0;           
}
if (RecivedAmtSample.value=="" || isNaN(RecivedAmtSample.value))
{
RecivedAmtSample.value=0;           
}
//alert(Txt_Cramt.value);
//alert(RecivedAmt.value);
//alert(OutstandingAmt.value);
if(parseFloat(Txt_Cramt.value)==0)
{
if(parseFloat(RecivedAmt.value)>parseFloat(OutstandingAmt.value))
{
alert("Received Amount should be Less than or equal to Outstanding Amount");
RecivedAmt.value=RecivedAmtSample.value;
total=0;
}
}
else
{
if(parseFloat(RecivedAmt.value)>parseFloat(OutstandingAmt.value))
{
alert("Received Amount should be Less than or equal to Outstanding Amount");
RecivedAmt.value=RecivedAmtSample.value;
total=0;
}
else
{
total=0;
for(var i = 1;i < _GridDetails.rows.length;i++)
   { 
  total=(parseFloat(total)+ parseFloat(_GridDetails.rows[i].cells[4].children[0].value));
   }
//   alert(total);
//   alert(Txt_Cramt.value);
if(parseFloat(total)>parseFloat(Txt_Cramt.value))
{
total=0;
alert("Total Received Amount Should be less than or Equal to Credit Amount");
RecivedAmt.value=RecivedAmtSample.value;
}
}
}
}

</script>
        Search for Supplier :
      <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
      Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged" TabIndex="19">
      </asp:TextBox>
      <div id="divwidth"></div>
      <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
      TargetControlID="TxtSearch" CompletionInterval="100"                             
      UseContextKey="True" FirstRowSelected ="true" ShowOnlyCurrentWordInCompletionListItem="true"
       ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender"
      CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
    </ajax:AutoCompleteExtender>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
Payment Voucher
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UpEntry" runat="server">
        <ContentTemplate>
        <table width="100%"><tr><td align="center">
           <table width="75%" ><tr><td align="center">
            <fieldset id="f1" class="FieldSet" runat="server">
                <table cellspacing="6px" style="background-color: #FFFAF0" width="100%">
                    <tr>
                        <td class="Label">
                            Voucher No :
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="TextBox" Width="250px"></asp:TextBox>
                        </td>
                        <td class="Label">
                            Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="250px" TabIndex="1"></asp:TextBox>
                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                TargetControlID="txtDate" PopupButtonID="ImageValid" />
                            <asp:ImageButton ID="ImageValid" runat="server" ImageUrl="~/Images/Icon/DateSelector.png"
                                CausesValidation="False" CssClass="Imagebutton" TabIndex="2"/>
                        </td>
                    </tr>
                      <tr>
                       
                        <td colspan="4" align="left">
                            <hr style="border-width: thin; border-style: solid none none none" width="850px" />
                        </td>
                    </tr>
                    <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                        <td align="right">                                                                          
                             <asp:Label ID="lblCredit" runat="server" Text="Debit" CssClass="Label2"></asp:Label>
                                        
                                      </td>
                                        <td align="right">
                                         <asp:Label ID="lblDebit" runat="server" Text="Credit" CssClass="Label2"></asp:Label>        
                                        </td>
                                    </tr>
                    <tr>
                       
                        <td colspan="4" >
                            <hr style="border-width: thin; border-style: solid none none none" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Label" nowrap="nowrap">
                            (Payment To)Debit :
                        </td>
                        <td style="height: 24px" >
                            <asp:DropDownList ID="ddlLedgerNameTO" runat="server" CssClass="ComboBox" Width="250px"
                                 OnSelectedIndexChanged="ddlLedgerNameTO_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="Req1" runat="server" ControlToValidate="ddlLedgerNameTO"
                                Display="None" ErrorMessage="Payment To Is Required" SetFocusOnError="True" ValidationGroup="Add"
                                InitialValue="0">
                            </asp:RequiredFieldValidator>
                            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                TargetControlID="Req1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                            </ajax:ValidatorCalloutExtender>
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtCrAmt" runat="server" CssClass="TextBoxNumeric" Width="150px"
                                onkeyup="AdjustAmount();" TabIndex="4"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers"
                                TargetControlID="txtCrAmt" ValidChars=".">
                            </ajax:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="Req3" runat="server" ControlToValidate="txtCrAmt"
                                Display="None" ErrorMessage=" Amount is  Required" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                            <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" TargetControlID="Req3"
                                WarningIconImageUrl="~/Images/Icon/Warning.png">
                            </ajax:ValidatorCalloutExtender>
                        </td>
                        <td>
                       
                       
                        </td>
                    </tr>
                    <tr>
                        <td class="Label">
                            (From)Credit :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLedgerNameFrom" runat="server" CssClass="ComboBox" Width="250px"
                                TabIndex="5">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="Req2" runat="server" ControlToValidate="ddlLedgerNameFrom"
                                Display="None" ErrorMessage="Payment From Is Required" SetFocusOnError="True"
                                ValidationGroup="Add" InitialValue="0">
                            </asp:RequiredFieldValidator>
                            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                TargetControlID="Req2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                            </ajax:ValidatorCalloutExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                       <td align="right">
                            <asp:TextBox ID="txtDrAmt" runat="server" CssClass="TextBoxNumeric" Width="150px"
                                Enabled="False" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    <td></td>
                    <td colspan="3">
                     <asp:Label ID="lblOutstandingName" runat="server"  CssClass="Label2"></asp:Label>
                    </td>
                    
                    </tr>
                      <tr>
                        <td class="Label">
                            For Site :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlsite" runat="server" CssClass="ComboBox" Width="250px"
                                TabIndex="5">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                       <td align="right">
                           
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="Label">
                            Amount(In Words) :
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtAmtInWrds" runat="server" CssClass="TextBox" Width="560px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rdAmtType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                OnSelectedIndexChanged="rdAmtType_SelectedIndexChanged" TabIndex="6">
                                <asp:ListItem Value="0"> Against Reference </asp:ListItem>
                                <asp:ListItem Value="1"> On Account </asp:ListItem>
                                <asp:ListItem Value="2"> Advance </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridInvoice" runat="server" AutoGenerateColumns="False" 
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                CellPadding="4" CssClass="mGrid" ForeColor="Black" GridLines="Horizontal" 
                                OnRowCommand="GridInvoice_RowCommand" onrowdatabound="GridInvoice_RowDataBound" 
                                OnRowDeleting="GridInvoice_RowDeleting" TabIndex="7">
                                <Columns>
                                    
                                    <asp:BoundField DataField="InvoiceId" HeaderText="InvoiceId">
                                       <%-- <HeaderStyle CssClass="Display_None" />
                                        <ItemStyle CssClass="Display_None" />--%>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" />
                                    <asp:BoundField DataField="InvoiceAmt" HeaderText="InvoiceAmt">
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Outstanding">                        
                                    <ItemTemplate>
                                    <asp:TextBox ID="Outstanding" runat="server" Text='<%# Eval("Outstanding") %>'
                                    Width="150px"  CssClass="TextBoxNumeric" onkeyup="CalculateAmtAsPerGrid(this);" Enabled="false">
                                    </asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" 
                                   width="150px"
                                    />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="ReceivedAmt">                        
                                    <ItemTemplate>
                                    <asp:TextBox ID="ReceivedAmt" runat="server" Text='<%# Eval("ReceivedAmt") %>'
                                    Width="150px"  CssClass="TextBoxNumeric" onkeyup="CalculateAmtAsPerGrid(this);">
                                    </asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" 
                                   width="150px"
                                    />
                                    </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Outstanding1">                        
                                    <ItemTemplate>
                                    <asp:TextBox ID="Outstanding1" runat="server" Text='<%# Eval("OutstandingSample") %>'
                                    Width="200px"  CssClass="TextBoxNumeric" onkeyup="CalculateAmtAsPerGrid(this);" Enabled="false">
                                    </asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False"  CssClass="Display_None"
                                   width="200px" 
                                    />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ReceivedAmt1">                        
                                    <ItemTemplate>
                                    <asp:TextBox ID="ReceivedAmt1" runat="server" Text='<%# Eval("ReceivedAmtSample") %>'
                                    Width="200px"  CssClass="TextBoxNumeric" onkeyup="CalculateAmtAsPerGrid(this);" Enabled="false" >
                                    </asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="Display_None"/>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"
                                   width="200px" 
                                    />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="right" colspan="3">
                                <hr style="border-width: thin; border-style: solid none none none" 
                                    width="500px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Label">
                                Narration :
                            </td>
                            <td>
                                <asp:TextBox ID="txtNarration" runat="server" CssClass="TextBox" TabIndex="11" 
                                    TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
                            <td>
                                Total :Dr.
                                <asp:TextBox ID="TxtTotalCredit" runat="server" CssClass="TextBoxNumeric" 
                                    Enabled="False" Width="150px"></asp:TextBox>
                            </td>
                            <td align="right">
                                Cr. :
                                <asp:TextBox ID="TxtTotalDebit" runat="server" CssClass="TextBoxNumeric" 
                                    Enabled="False" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        
                        
                    </tr>
                    
                </table>
            </fieldset>
            </td></tr></table>
             <table width="83%" ><tr><td align="center">
             <fieldset id="Fieldset1" class="FieldSet" runat="server">
             <table cellspacing="6px" style="background-color: #FFFAF0" width="100%">
            <tr>
                            <td align="center" colspan="4">
                                <table align="center" width="20%">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnUpdate" runat="server" CssClass="button" 
                                                onclick="BtnUpdate_Click" TabIndex="12" Text="Update" ValidationGroup="Add" />
                                            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                                                ConfirmText="Would You Like To Update the Record ..! " 
                                                TargetControlID="BtnUpdate">
                                            </ajax:ConfirmButtonExtender>
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnSave" runat="server" CssClass="button" 
                                                onclick="BtnSave_Click" TabIndex="12" Text="Save" ValidationGroup="Add" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" 
                                                CssClass="button" onclick="BtnCancel_Click" TabIndex="13" Text="Cancel" 
                                                ValidationGroup="Add" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <%--<td></td>--%>
                        </tr>
             </table>
                         </fieldset>
            </td></tr></table>
             <table width="83%" ><tr><td align="center">
             <%--<fieldset id="Fieldset2" class="FieldSet" runat="server">--%>
             <table cellspacing="6px" style="background-color: #FFFAF0" width="100%">
             <tr>
                    <td colspan="4">
                    <asp:GridView ID="GrdReport" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt"
                    AutoGenerateColumns="False" CssClass="mGrid" GridLines="None" PagerStyle-CssClass="pgr"
                    Width="100%" onrowcommand="GrdReport_RowCommand" 
                    onpageindexchanging="GrdReport_PageIndexChanging" 
                    onrowdeleting="GrdReport_RowDeleting">
                    <Columns>
                        <asp:TemplateField >
                            <ItemTemplate >
                                <asp:ImageButton ID="ImgBtnEdit" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                    CommandArgument='<%# Eval("#") %>' CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png"
                                    TabIndex="15" ToolTip="Edit Record" />
                                <asp:ImageButton ID="ImgBtnDelete" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                    CommandArgument='<%# Eval("#") %>' CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png"
                                    TabIndex="16" ToolTip="Delete Record" />
                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                    TargetControlID="ImgBtnDelete">
                                </ajax:ConfirmButtonExtender>
                                 <a href='../PrintAccReport/PrintReport.aspx?ID=<%# Eval("#")%>&Flag=<%="PaymentVoucher"%>&PDFFlag=<%="NOPDF"%>' target="_blank">
                                         <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                                                ToolTip="Print Payment Voucher" TabIndex="17" />
                                         </a>
                                          <a href='../PrintAccReport/PrintReport.aspx?ID=<%# Eval("#")%>&Flag=<%="PaymentVoucher"%>&PDFFlag=<%="PDF"%>' target="_blank">
                                         <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png"
                                                ToolTip="PDF Purchase Order" TabIndex="18" />
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sr. No.">
                            <ItemTemplate>
                                <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="7%" />
                        </asp:TemplateField>
                         <asp:BoundField DataField="VoucherDate" HeaderText="Voucher Date">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText=" Name">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="VoucherAmount" HeaderText="Voucher Amount">
                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
                    </td>
                    </tr>
              </table>
                        <%-- </fieldset>
              --%></td></tr></table>
             
                       
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

