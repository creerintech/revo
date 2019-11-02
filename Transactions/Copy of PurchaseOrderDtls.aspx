<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Copy of PurchaseOrderDtls.aspx.cs" Inherits="Transactions_PurchaseOrderDtls" Title="Purchase Order" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<asp:HiddenField ID="SetFlagHidden" runat="server" value="0"/>
<script language="javascript" type="text/javascript">


     function RadioCheck(rb) {
         var gv = document.getElementById("<%=GridLINKSUPPLIERRATE.ClientID%>");
        var rbs = gv.getElementsByTagName("input");

        var row = rb.parentNode.parentNode;
        var getrow=0;
        for (var i = 0; i < rbs.length; i++) {
            if (rbs[i].type == "radio") {
                if (rbs[i].checked == true) {
                    getrow = i;
                }
            }
        }
       
        for (var i = 0; i < rbs.length; i++) {
            if (rbs[i].type == "radio") {
              
                rbs[i].checked = false;
                
            }
        }
            rbs[getrow].checked = true;

        }
       
        
        
  



    function ShowPOP() 
    {
        document.getElementById('<%= dialog.ClientID %>').style.display = "block";
    }
    function SetFlag() {
        document.getElementById('<%= SetFlagHidden.ClientID %>').value = 1;
        
    }

    function HidePOP() 
    {
        document.getElementById('<%= dialog.ClientID %>').style.display = "none";
    }


    function ShowPOPTermsPOSupplier() {

        document.getElementById('<%= DIVTERMSSUPPLIER.ClientID %>').style.display = "block";
      
    }

    function HidePOPTermsPOSupplier() {

        document.getElementById('<%= DIVTERMSSUPPLIER.ClientID %>').style.display = "none";
     
    }
    

    function HidePOPForceClose(s)
        {

           document.getElementById('<%= dialog.ClientID %>').style.display = "none";
       }


       function HideALLPOPForceClose(s) {

           document.getElementById('<%= dialog.ClientID %>').style.display = "none";
           document.getElementById('<%= DIVTERMSSUPPLIER.ClientID %>').style.display = "none";
           
       }
 
function SETVALUE(objGrid)
{
var _GridDetails = document.getElementById('<%= GrdReqPO.ClientID %>');  
var rowIndex=objGrid.offsetParent.parentNode.rowIndex;
var ddlsupp=(_GridDetails.rows[rowIndex].cells[18].children[0]);
var suppId=(_GridDetails.rows[rowIndex].cells[25].children[0]);
if (ddlsupp.value==0 )
{
suppId.value=0; 
alert("Please select Supplier");          
}
if (ddlsupp.value>0 )
{
suppId.value=ddlsupp.value;           
}
}

function CalculateGrid(objGrid)
{
var _GridDetails = document.getElementById('<%= GrdReqPO.ClientID %>');  
var rowIndex=objGrid.offsetParent.parentNode.rowIndex;
var Rate=(_GridDetails.rows[rowIndex].cells[19].children[0]);
var OrdQty=(_GridDetails.rows[rowIndex].cells[20].children[0]);
var pervat=(_GridDetails.rows[rowIndex].cells[21].children[0]);
var vat=(_GridDetails.rows[rowIndex].cells[22].children[0]);
var perdisc=(_GridDetails.rows[rowIndex].cells[23].children[0]);
var disc=(_GridDetails.rows[rowIndex].cells[24].children[0]);
if (Rate.value=="" || isNaN(Rate.value))
{
Rate.value=0;           
}

if (OrdQty.value=="" || isNaN(OrdQty.value))
{
OrdQty.value=0;           
}

if (pervat.value=="" || isNaN(pervat.value))
{
pervat.value=0;           
}

if (vat.value=="" || isNaN(vat.value))
{
vat.value=0;           
}

if (perdisc.value=="" || isNaN(perdisc.value))
{
perdisc.value=0;           
}

if (disc.value=="" || isNaN(disc.value))
{
disc.value=0;           
}
disc.value=parseFloat((perdisc.value/100)*((Rate.value*OrdQty.value))).toFixed(2);
vat.value=parseFloat((pervat.value/100)*((Rate.value*OrdQty.value)-disc.value)).toFixed(2);
}


function CalPercentage_Amount(TxtBoxId)
{

var _TxtNETTotal = document.getElementById('<%= txtNetTotal.ClientID %>');   
var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');   
var _TxtDiscount = document.getElementById('<%= txtDiscount.ClientID %>');   
var _TxtVat = document.getElementById('<%= txtVATAmount.ClientID %>');
var _txtDekhrekhAmt = document.getElementById('<%= txtDekhrekhAmt.ClientID %>'); 
var _txtHamaliAmt = document.getElementById('<%= txtHamaliAmt.ClientID %>');
var _txtCESSAmt = document.getElementById('<%= txtCESSAmt.ClientID %>');
var _txtFreightAmt = document.getElementById('<%= txtFreightAmt.ClientID %>');
var _txtPackingAmt = document.getElementById('<%= txtPackingAmt.ClientID %>');
var _txtPostageAmt = document.getElementById('<%= txtPostageAmt.ClientID %>');
var _txtOtherCharges = document.getElementById('<%= txtOtherCharges.ClientID %>');
var _txtexciseduty = document.getElementById('<%= txtexciseduty.ClientID %>'); 
var _TxtGrandTotal = document.getElementById('<%= txtGrandTotal.ClientID %>');   
var t1=0,t2=0,t3=0,t4=0,t5=0,t6=0,t7=0,t8=0,t9=0;
if (_txtexciseduty.value == "" || isNaN(_txtexciseduty.value)) {
    _txtexciseduty.value = 0;
}
if (_TxtNETTotal.value=="" || isNaN(_TxtNETTotal.value))
{
_TxtNETTotal.value=0;           
}
if (_TxtSubTotal.value=="" || isNaN(_TxtSubTotal.value))
{
_TxtSubTotal.value=0;           
}
if (_TxtDiscount.value=="" || isNaN(_TxtDiscount.value))
{
_TxtDiscount.value=0;           
}
if (_TxtVat.value=="" || isNaN(_TxtVat.value))
{
_TxtVat.value=0;           
}
if (_txtDekhrekhAmt.value=="" || isNaN(_txtDekhrekhAmt.value))
{
_txtDekhrekhAmt.value=0;           
}
if (_txtHamaliAmt.value=="" || isNaN(_txtHamaliAmt.value))
{
_txtHamaliAmt.value=0;           
}
if (_txtCESSAmt.value=="" || isNaN(_txtCESSAmt.value))
{
_txtCESSAmt.value=0;           
}
if (_txtFreightAmt.value=="" || isNaN(_txtFreightAmt.value))
{
_txtFreightAmt.value=0;           
}
if (_txtPackingAmt.value=="" || isNaN(_txtPackingAmt.value))
{
_txtPackingAmt.value=0;           
}
if (_txtPostageAmt.value=="" || isNaN(_txtPostageAmt.value))
{
_txtPostageAmt.value=0;           
}
if (_txtOtherCharges.value=="" || isNaN(_txtOtherCharges.value))
{
_txtOtherCharges.value=0;           
}
if (_TxtGrandTotal.value=="" || isNaN(_TxtGrandTotal.value))
{
_TxtGrandTotal.value=0;           
}

_TxtGrandTotal.value = parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) + parseFloat(_txtDekhrekhAmt.value) + parseFloat(_txtHamaliAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtCESSAmt.value) + parseFloat(_txtFreightAmt.value) + parseFloat(_txtPackingAmt.value) + parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2);
_TxtNETTotal.value = parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) + parseFloat(_txtDekhrekhAmt.value) + parseFloat(_txtHamaliAmt.value) + parseFloat(_txtCESSAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtFreightAmt.value) + parseFloat(_txtPackingAmt.value) + parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2);
}



function CalAsPerDDl() {
    var _TxtNETTotal = document.getElementById('<%= txtNetTotal.ClientID %>');
    var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');
    var _TxtDiscount = document.getElementById('<%= txtDiscount.ClientID %>');
    var _TxtVat = document.getElementById('<%= txtVATAmount.ClientID %>');
    var _txtDekhrekhAmt = document.getElementById('<%= txtDekhrekhAmt.ClientID %>');
    var _txtHamaliAmt = document.getElementById('<%= txtHamaliAmt.ClientID %>');
    var _txtCESSAmt = document.getElementById('<%= txtCESSAmt.ClientID %>');
    var _txtFreightAmt = document.getElementById('<%= txtFreightAmt.ClientID %>');
    var _txtPackingAmt = document.getElementById('<%= txtPackingAmt.ClientID %>');
    var _txtPostageAmt = document.getElementById('<%= txtPostageAmt.ClientID %>');
    var _txtOtherCharges = document.getElementById('<%= txtOtherCharges.ClientID %>');
    var _TxtGrandTotal = document.getElementById('<%= txtGrandTotal.ClientID %>');
    var _TxtSerTax = document.getElementById('<%= txtSerTax.ClientID %>');
    var _txtexciseduty = document.getElementById('<%= txtexciseduty.ClientID %>'); 
    var DDL_SERVICETAX = document.getElementById("<%=DDLSERVICETAX.ClientID %>");
    var ddlValue = DDL_SERVICETAX.options[DDL_SERVICETAX.selectedIndex].text;


    var t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0;
    if (_txtexciseduty.value == "" || isNaN(_txtexciseduty.value)) {
        _txtexciseduty.value = 0;
    }
    if (_TxtNETTotal.value == "" || isNaN(_TxtNETTotal.value)) {
        _TxtNETTotal.value = 0;
    }
    if (_TxtSubTotal.value == "" || isNaN(_TxtSubTotal.value)) {
        _TxtSubTotal.value = 0;
    }
    if (_TxtDiscount.value == "" || isNaN(_TxtDiscount.value)) {
        _TxtDiscount.value = 0;
    }
    if (_TxtVat.value == "" || isNaN(_TxtVat.value)) {
        _TxtVat.value = 0;
    }
    if (_txtDekhrekhAmt.value == "" || isNaN(_txtDekhrekhAmt.value)) {
        _txtDekhrekhAmt.value = 0;
    }
    if (_txtHamaliAmt.value == "" || isNaN(_txtHamaliAmt.value)) {
        _txtHamaliAmt.value = 0;
    }
    if (_txtCESSAmt.value == "" || isNaN(_txtCESSAmt.value)) {
        _txtCESSAmt.value = 0;
    }
    if (_txtFreightAmt.value == "" || isNaN(_txtFreightAmt.value)) {
        _txtFreightAmt.value = 0;
    }
    if (_txtPackingAmt.value == "" || isNaN(_txtPackingAmt.value)) {
        _txtPackingAmt.value = 0;
    }
    if (_txtPostageAmt.value == "" || isNaN(_txtPostageAmt.value)) {
        _txtPostageAmt.value = 0;
    }
    if (_txtOtherCharges.value == "" || isNaN(_txtOtherCharges.value)) {
        _txtOtherCharges.value = 0;
    }
    if (_TxtGrandTotal.value == "" || isNaN(_TxtGrandTotal.value)) {
        _TxtGrandTotal.value = 0;
    }

    _TxtSerTax.value = parseFloat((ddlValue / 100) * (parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) + parseFloat(_txtDekhrekhAmt.value) + parseFloat(_txtHamaliAmt.value) + parseFloat(_txtCESSAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtFreightAmt.value) + parseFloat(_txtPackingAmt.value) + parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2))).toFixed(2);

    _TxtGrandTotal.value = parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) + parseFloat(_TxtSerTax.value) + parseFloat(_txtDekhrekhAmt.value) + parseFloat(_txtHamaliAmt.value) + parseFloat(_txtCESSAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtFreightAmt.value) + parseFloat(_txtPackingAmt.value) + parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2);
    _TxtNETTotal.value = parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) + parseFloat(_TxtSerTax.value) + parseFloat(_txtDekhrekhAmt.value) + parseFloat(_txtHamaliAmt.value) + parseFloat(_txtCESSAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtFreightAmt.value) + parseFloat(_txtPackingAmt.value) + parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2);

}

function GetAmountOfExciseDuty() {
    var _txtexcisedutyper = document.getElementById('<%= txtexcisedutyper.ClientID %>');
    var _txtexciseduty = document.getElementById('<%= txtexciseduty.ClientID %>');
    var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');
    if (_txtexcisedutyper.value == "" || isNaN(_txtexcisedutyper.value)) {
        _txtexcisedutyper.value = 0;
    }
    if (_txtexciseduty.value == "" || isNaN(_txtexciseduty.value)) {
        _txtexciseduty.value = 0;
    }
    if (_TxtSubTotal.value == "" || isNaN(_TxtSubTotal.value)) {
        _TxtSubTotal.value = 0;
    }
    _txtexciseduty.value = parseFloat((parseFloat(_txtexcisedutyper.value) * 0.01) * (parseFloat(_TxtSubTotal.value))).toFixed(2);
    CalAsPerDDl();
}

function EnableTextBox() {
    var txtHamaliAmt = document.getElementById('<%= txtHamaliAmt.ClientID %>');
    var CHKHAMALI = document.getElementById('<%= CHKHAMALI.ClientID %>');

    var txtFreightAmt = document.getElementById('<%= txtFreightAmt.ClientID %>');
    var CHKFreightAmt = document.getElementById('<%= CHKFreightAmt.ClientID %>');

    var txtOtherCharges = document.getElementById('<%= txtOtherCharges.ClientID %>');
    var CHKOtherCharges = document.getElementById('<%= CHKOtherCharges.ClientID %>');

    var txtPostageAmt = document.getElementById('<%= txtPostageAmt.ClientID %>');
    var CHKLoading = document.getElementById('<%= CHKLoading.ClientID %>');
    
    if (CHKHAMALI.checked == false) {
        txtHamaliAmt.disabled = false;
    }
    if (CHKHAMALI.checked == true) {
     txtHamaliAmt.value = "0.00";
        txtHamaliAmt.disabled = true;
    }

    if (CHKFreightAmt.checked == false) {
        txtFreightAmt.disabled = false;
    }
    if (CHKFreightAmt.checked == true) {
        txtFreightAmt.value = "0.00";
        txtFreightAmt.disabled = true;
    }

    if (CHKOtherCharges.checked == false) {
        txtOtherCharges.disabled = false;
    }
    if (CHKOtherCharges.checked == true) {
        txtOtherCharges.value = "0.00";
        txtOtherCharges.disabled = true;
    }

    if (CHKLoading.checked == false) {
        txtPostageAmt.disabled = false;
    }
    if (CHKLoading.checked == true) {
        txtPostageAmt.value = "0.00";
        txtPostageAmt.disabled = true;
    }
    CalAsPerDDl();
}
</script>
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
    <ProgressTemplate>            
    <div id="progressBackgroundFilter"></div>
    <div id="processMessage">   
    <center><span class="SubTitle">Loading....!!! </span></center>
    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
    </div>
    </ProgressTemplate>
    </asp:UpdateProgress>

        Search for Purchase Order : <asp:TextBox ID="TxtSearch" runat="server" 
    CssClass="search" ToolTip="Enter The Text"
    Width="292px" AutoPostBack="True" 
    ontextchanged="TxtSearch_TextChanged"></asp:TextBox>
    <div id="divwidth"></div>
    <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
    TargetControlID="TxtSearch"    CompletionInterval="100"                               
    UseContextKey="True" FirstRowSelected ="true" 
    ShowOnlyCurrentWordInCompletionListItem="true"  
    ServiceMethod="GetCompletionList"
    CompletionListCssClass="AutoExtender"
    CompletionListItemCssClass="AutoExtenderList"
    CompletionListHighlightedItemCssClass="AutoExtenderHighlight"                       
    ></ajax:AutoCompleteExtender> 
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Purchase Order              
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<table width="100%"><tr><td>
<table width="100%" cellspacing="6">
   <tr>
   <td colspan="2">
   <fieldset id="F1" runat="server" class="FieldSet">
     <table width="100%">
        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
<ContentTemplate>
  <tr>

  <td class="Label">
      PO Through :</td>
   <td>
       <asp:RadioButtonList ID="rdoPOThrough" runat="server" 
                    RepeatDirection="Horizontal" CssClass="RadioButton" 
           onselectedindexchanged="rdoPOThrough_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Selected="True" Value="0">&#160;Indent Wise&#160;&#160;&#160;&#160;</asp:ListItem>
                    <asp:ListItem Value="1">&#160;Item Wise</asp:ListItem>
                </asp:RadioButtonList>
   </td>
 
   <td align="right" class="Label_Dynamic">
       <asp:Button ID="BtnShow" runat="server" CssClass="Display_None" 
           onclick="BtnShow_Click" Text="Show" ToolTip="Show Details" 
           ValidationGroup="Add" />
       Date :</td>    
      <td align="left">
          <asp:Label ID="LblDate" runat="server" CssClass="Display_None" Font-Bold="true"></asp:Label>
          <asp:TextBox runat="server" ID="TxtDate" CssClass="TextBox" Width="80px"></asp:TextBox>
          <asp:ImageButton ID="ImageFromDate" runat="server" CausesValidation="False"
CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" 
        meta:resourcekey="ImageFromDateResource1" />
<ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
PopupButtonID="ImageFromDate" TargetControlID="TxtDate" Enabled="True">
</ajax:CalendarExtender>
          
      </td>
      
       <td class="Label">
                 Company :</td>
             <td align="left">
           
                    <ajax:ComboBox ID="ddlCompany" runat="server" DropDownStyle="DropDown" AutoPostBack="false"
        AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="230px" CssClass="CustomComboBoxStyle" ></ajax:ComboBox>
                 </td>    
                 
        
   </tr>
      
      <tr>

  <td class="Label">
      PO Quot. No :</td>
   <td colspan="1">
          <asp:TextBox ID="TXTPOQTNO" runat="server" CssClass="TextBox" Width="200px"  ></asp:TextBox>
   </td>
  <td class="Label">
      Quot. Dt :</td>
   <td colspan="3">
          <asp:TextBox ID="TXTPOQTDATE" runat="server" CssClass="TextBox" Width="150px"  ></asp:TextBox>
<asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False"
CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" 
meta:resourcekey="ImageFromDateResource1" />
<ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy HH':'mm':'ss" Animated="true"
PopupButtonID="ImageButton4" TargetControlID="TXTPOQTDATE" Enabled="True">
</ajax:CalendarExtender>
   </td>
    
                 
        
   </tr>
   
         <tr id="TR_Requision" runat="server">
             <td class="Label">
                 Indent No.:</td>
             <td>
                 <ajax:ComboBox ID="ddlDepartment" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
        AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="350px" CssClass="CustomComboBoxStyle" onselectedindexchanged="ddlDepartment_SelectedIndexChanged"
        ></ajax:ComboBox>
        
             </td>
               <td align="left">
                   &nbsp;</td>    
                 
             <td align="left">
                 &nbsp;</td>
            <td class="Label">              
              
   </td>
   <td align="left">
           
   </td>
           
                 
         </tr>
         <tr id="TR_Item" runat="server">
             <td class="Label">
                 Category :</td>
             <td>
           
                <ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
        AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="270px" CssClass="CustomComboBoxStyle" 
    onselectedindexchanged="ddlCategory_SelectedIndexChanged" ></ajax:ComboBox>
             </td>
             <td class="Label_Dynamic" align="right" rowspan="5" valign="middle">
                 Rate :</td>
             <td align="left" rowspan="5">
                 <asp:ListBox ID="lstSupplierRate" runat="server" Width="300px" 
                     AutoPostBack="True" 
                     onselectedindexchanged="lstSupplierRate_SelectedIndexChanged"></asp:ListBox>
             </td>
             <td align="right" class="Label_Dynamic" valign="top">
                 Qty :</td>
             <td align="left" class="Label_Dynamic" valign="top">
                 <asp:TextBox ID="txtItemOrdQty" runat="server" CssClass="TextBox" Width="60px" 
                     AutoPostBack="True" ontextchanged="txtItemOrdQty_TextChanged"></asp:TextBox>
                     
                          <asp:DropDownList ID="ddlUNIT" runat="server" AutoPostBack="true" 
                     CssClass="ComboBox" Width="90px" 
                     onselectedindexchanged="ddlUNIT_SelectedIndexChanged" >
             </asp:DropDownList>
             </td>
         </tr>
         <tr id="TR_RateList" runat="server">
             <td class="Label">Sub Category :
                </td>
             <td valign="top">
                 
                
                <ajax:ComboBox ID="ddlsubcategory" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
        AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="270px" CssClass="CustomComboBoxStyle" 
    onselectedindexchanged="ddlsubcategory_SelectedIndexChanged" ></ajax:ComboBox>
                 
             </td>
             <td align="left" valign="middle">
                 
                 VAT(%) :</td>
             <td align="left" valign="middle">
                  <asp:TextBox ID="txtvatper" runat="server" CssClass="TextBox" Width="60px" 
                     AutoPostBack="True" ontextchanged="txtvatper_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:TextBox ID="txtvatamt" runat="server" CssClass="TextBox" Width="60px" Enabled="false"></asp:TextBox>
                  &nbsp;Rs/-
             </td>
              <td align="left" valign="middle">
                
             </td>
              <td align="left" valign="middle">
                
             </td>
             
         </tr>
         
         <tr id="TR2" runat="server">
             <td class="Label" > Item :</td>
             <td valign="top">
               
                  <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
        AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="270px" CssClass="CustomComboBoxStyle" 
    onselectedindexchanged="ddlItem_SelectedIndexChanged" ></ajax:ComboBox>
             </td>
             <td align="right" class="Label" valign="top">
                  DISC(%) :
                </td>
             <td align="left" valign="middle">
                <asp:TextBox ID="txtdiscper" runat="server" CssClass="TextBox" Width="60px" 
                     AutoPostBack="True" ontextchanged="txtdiscper_TextChanged"></asp:TextBox>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:TextBox ID="txtdiscamt" runat="server" CssClass="TextBox" Width="60px" Enabled="false"></asp:TextBox>
                 &nbsp;Rs/-
             </td>
               <td align="left">
                   &nbsp;</td>
            <td class="Label">              
              
   </td>
         </tr>
         <tr id="TR3" runat="server">
             <td  class="Label">Item Desc :</td>
             <td valign="top">
                  
                  <ajax:ComboBox ID="ddlItemDesc" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
        AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="270px" CssClass="CustomComboBoxStyle" 
    onselectedindexchanged="ddlItem_SelectedIndexChanged" ></ajax:ComboBox>
    
             </td>
             <td align="right" class="Label" valign="top">
                 
               </td>
             <td align="left" valign="middle">
              
             </td>
               <td align="left">
                   &nbsp;</td>
            <td class="Label">              
              
   </td>
         </tr>
         
           <tr id="TR5" runat="server">
             <td ></td>
             <td valign="top">
                 
             </td>
             <td align="right" class="Label" valign="top">
                 
                </td>
             <td align="left" valign="middle">
                <asp:Button ID="BtnAdd" runat="server" CssClass="button" onclick="BtnAdd_Click" 
                     Text="Add" ToolTip="Add To Purchase Order Grid"/>
             </td>
               <td align="left">
                   &nbsp;</td>
            <td class="Label">              
              
   </td>
         </tr>
         </ContentTemplate>
         </asp:UpdatePanel>
      
</table>

</fieldset>
</td>
</tr>

<tr>
<td colspan="2" >
    <fieldset id="Fieldset2"  class="FieldSet" runat="server" style="width: 100%">
         <legend id="Legend3" class="legend" runat="server">  Material Indent</legend>
    <div id="divGridDetails" class="ScrollableDiv_FixHeightWidth3">
    <table width="100%">
    <tr>
        <td >        
     <asp:GridView ID="GrdReqPO" runat="server" AutoGenerateColumns="False" Width="100%"
      CssClass="mGrid" BackColor="White" BorderColor="#0CCCCC"
       BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
            AllowPaging="false" onrowdatabound="GrdReqPO_RowDataBound" 
            onrowcommand="GrdReqPO_RowCommand" DataKeyNames="#" >
        <columns>
            <asp:TemplateField HeaderText="#" Visible="False">
                 <ItemTemplate>
                     <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
            <asp:TemplateField HeaderText="All" >
             <HeaderTemplate>
                 <asp:CheckBox ID="GrdSelectAllHeader" runat="server"                                                  
                          AutoPostBack="true" OnCheckedChanged="GrdSelectAllHeader_CheckedChanged"/>
             </HeaderTemplate>
             <ItemTemplate>
                     <asp:CheckBox ID="GrdSelectAll" runat="server"/>
             </ItemTemplate>
             <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
             </asp:TemplateField>
            <asp:TemplateField>
                 <ItemTemplate>
                     <asp:ImageButton ID="ImageGridEdit" runat="server" 
                         CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                         CommandName="Select" ImageUrl="~/Images/New Icon/ViewRecord.png" ToolTip="Item Details" />
                     
                     <asp:ImageButton ID="ImgBtnDelete" runat="server" Visible="false"
                         CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                         CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete"/>
                     <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                         ConfirmText="Would You Like To Delete The Record..!" 
                         TargetControlID="ImgBtnDelete">
                     </ajax:ConfirmButtonExtender>
                 </ItemTemplate>
                 <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                 <HeaderStyle Width="10px" />
                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" 
                     Wrap="false" />
             </asp:TemplateField>
            <asp:BoundField DataField="ItemCode" HeaderText="Code">
                <HeaderStyle Wrap="False"  CssClass="Display_None"/>
                <ItemStyle Wrap="False" CssClass="Display_None"/>
            </asp:BoundField>      
            
           
                                 
            <asp:BoundField  HeaderText="Particular" DataField="ItemName">
           <HeaderStyle Wrap="false" Width="150px"  />
           <ItemStyle Wrap="false" Width="150px" />
           </asp:BoundField> 
           
            <asp:BoundField DataField="ItemDetailsId" HeaderText="ItemDetailsId">
                <HeaderStyle Wrap="False" CssClass="Display_None" />
                <ItemStyle Wrap="False" CssClass="Display_None"/>
            </asp:BoundField>      
            
         
              <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                <HeaderStyle Wrap="False" />
                <ItemStyle Wrap="False" />
            </asp:BoundField>                
            <asp:BoundField HeaderText="Req.Qty" DataField="ReqQty" >
           <HeaderStyle Wrap="false" />
           <ItemStyle Wrap="false" />
           </asp:BoundField> 
              <asp:BoundField HeaderText="Unit" DataField="Unit" >
           <HeaderStyle Wrap="false" CssClass="Display_None" />
           <ItemStyle Wrap="false" CssClass="Display_None"/>
           </asp:BoundField>    
            <asp:BoundField HeaderText="Tot.Ord" DataField="TotOrdQty" >
            <HeaderStyle Wrap="false" CssClass="Display_None" />
            <ItemStyle Wrap="false" CssClass="Display_None"/>
            </asp:BoundField>            
            <asp:BoundField HeaderText="ReqByCafeteria" DataField="ReqByCafeteria">
           <HeaderStyle Wrap="false" CssClass="Display_None"/>
           <ItemStyle Wrap="false" CssClass="Display_None"/>
           </asp:BoundField>            
            <asp:BoundField HeaderText="Transit Qty" DataField="TransitQty" >
           <HeaderStyle Wrap="false" CssClass="Display_None"/>
           <ItemStyle Wrap ="false" CssClass="Display_None" />
           </asp:BoundField>     
            <asp:BoundField HeaderText="Avl.Stock" DataField="AvailableStock" >
           <HeaderStyle Wrap="false" CssClass="Display_None" />
           <ItemStyle Wrap ="false" CssClass="Display_None"/>
           </asp:BoundField> 
           <asp:BoundField HeaderText="Bal.Qty" DataField="RemQty" >
            <HeaderStyle Wrap="false" CssClass="Display_None"/>
            <ItemStyle Wrap="false" CssClass="Display_None" />
            </asp:BoundField>                
            <asp:BoundField HeaderText="MinStockLevel" DataField="MinStockLevel" >
           <HeaderStyle Wrap="false" CssClass="Display_None"/>
           <ItemStyle Wrap="false" CssClass="Display_None"/>
           </asp:BoundField >  
             <asp:BoundField HeaderText="Unit" DataField="Unit" >
           <HeaderStyle Wrap="false" CssClass="Display_None"/>
           <ItemStyle Wrap="false" CssClass="Display_None"/>
           </asp:BoundField>          
            <asp:BoundField HeaderText="Del.Period" DataField="DeliveryPeriod" >
           <HeaderStyle Wrap="false" CssClass="Display_None"/>
           <ItemStyle Wrap="false" CssClass="Display_None"/>
           </asp:BoundField>           
            <asp:BoundField HeaderText="Site" DataField="StoreLocation">
           <HeaderStyle Wrap="false" CssClass="Display_None"/>
           <ItemStyle Wrap="false" CssClass="Display_None"/>
           </asp:BoundField>                       
            
       
             
           <asp:TemplateField HeaderText="Last Purchase Record">
           <ItemTemplate>

<div style="">
<div style="">
<asp:LinkButton runat="server" ID="LINKLASTPURCHASE" Text="LAST 3 PURCHASE RATE&nbsp;&nbsp;" OnClientClick="javascript:HideALLPOPForceClose();"></asp:LinkButton>
<ajax:PopupControlExtender ID="popupLINKLASTPURCHASE" runat="server" OnPreRender="ONLOADALLRECORD_OnLoad"
PopupControlID="PnlGrid" Position="Left" CommitProperty="Value"
TargetControlID="LINKLASTPURCHASE" DynamicServicePath="" Enabled="True" 
ExtenderControlID="">
</ajax:PopupControlExtender>
<asp:Panel ID="PnlGrid" runat="server" >
<div class="PopupPanel">
<asp:UpdatePanel ID="ProcessEntry" runat="server">
<ContentTemplate>
<table>
<tr>                       
<td > 
<div ID="DivTabControl" runat="server" class="scrollableDiv"> 
<table style="width: 100%">
<tr>
<td align="left">
<asp:UpdatePanel ID="UpdatePanelLINKLASTPURCHASE" runat="server">
<ContentTemplate>

<asp:RadioButtonList ID="RdoLASTPURCHASE" runat="server" CellPadding="25"  RepeatDirection="Vertical" CssClass="RadioButton">
</asp:RadioButtonList>

</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
</table> 
</div>
</td>
</tr>
<tr>
<td align="center">
<asp:CheckBox ID="CHKLINKLASTPURCHASE" runat ="server" Text="Confirm"  OnClientClick="javascript:HideALLPOPForceClose();"
CssClass="CheckBox" AutoPostBack="True" OnCheckedChanged="CHKLINKLASTPURCHASE_CheckedChanged"   />                                                                
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Panel>
</div>

<div style="">
<asp:LinkButton runat="server" ID="LINKSUPPLIERRATE" Text="SUPPLIER WISE RATE&nbsp;&nbsp;"  OnClientClick="javascript:HideALLPOPForceClose();"></asp:LinkButton>
<ajax:PopupControlExtender ID="PopupLINKSUPPLIERRATE" runat="server" OnPreRender="ONLOADALLRECORD_OnLoad"
PopupControlID="PanelLINKSUPPLIERRATE" Position="Left" CommitProperty="Value"
TargetControlID="LINKSUPPLIERRATE" DynamicServicePath="" Enabled="True" 
ExtenderControlID="" >
</ajax:PopupControlExtender>
<asp:Panel ID="PanelLINKSUPPLIERRATE" runat="server" >
<div class="PopupPanel">
<asp:UpdatePanel ID="UpdatePanel4" runat="server">
<ContentTemplate>
<table>
<tr>                       
<td > 
<div ID="DivLINKSUPPLIERRATE" runat="server" class="scrollableDiv"> 
<table style="width: 100%">
<tr>
<td align="left">
<asp:UpdatePanel ID="UpdatePanel7" runat="server">
<ContentTemplate>

<asp:GridView ID="GridLINKSUPPLIERRATE" runat="server" AutoGenerateColumns="False" DataKeyNames="#" CssClass="mGrid" >
<Columns>
<asp:TemplateField HeaderText="#" Visible="False" >
<ItemTemplate>
<asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px" ></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="SuplierID" HeaderText="SupplierID">
<HeaderStyle CssClass="Display_None"  HorizontalAlign="Left" 
VerticalAlign="Middle" Wrap="False" />
<ItemStyle  CssClass="Display_None" HorizontalAlign="Left" 
VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
<asp:TemplateField HeaderText="Select" >
<ItemTemplate>
<asp:RadioButton runat="server" ID="RBSupplierList" GroupName="SuplierListLINKSUPPLIERRATE" onchange="javascript:RadioCheck(this);"  />
</ItemTemplate>
<HeaderStyle Width="10px" Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
</asp:TemplateField>
<asp:BoundField DataField="SuplierName" HeaderText="Suplier"  >
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
<asp:TemplateField HeaderText="Rate">
<ItemTemplate>
<asp:TextBox ID="GridLINKSUPPLIERRATERATE" runat="server" CssClass="TextBoxNumeric"
MaxLength="10" Text='<%# Bind("Rate") %>' TextMode="SingleLine" Width="60px"></asp:TextBox>
<ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderrate" runat="server" FilterType="Numbers, Custom"
ValidChars="." TargetControlID="GridLINKSUPPLIERRATERATE" />
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="60px"/>
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="60px"/>
</asp:TemplateField>  
</Columns>
</asp:GridView>

</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
</table> 
</div>
</td>
</tr>
<tr>
<td align="center">
<asp:CheckBox ID="ChkLINKSUPPLIERRATE" runat ="server" Text="Confirm"  OnClientClick="javascript:HideALLPOPForceClose();"
CssClass="CheckBox" AutoPostBack="True"   OnCheckedChanged="CHKLINKSUPPLIERRATE_CheckedChanged"   />                                                                
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Panel>
</div>

<div style="">
<asp:LinkButton runat="server" ID="LINKALLRECORD" Text="VIEW ALL"  OnClientClick="javascript:HideALLPOPForceClose();"></asp:LinkButton>
<ajax:PopupControlExtender ID="PopupLINKALLRECORD" runat="server" OnPreRender="ONLOADALLRECORD_OnLoad"
PopupControlID="PanelLINKALLRECORD" Position="Left" CommitProperty="Value"
TargetControlID="LINKALLRECORD" DynamicServicePath="" Enabled="True" 
ExtenderControlID="">
</ajax:PopupControlExtender>
<asp:Panel ID="PanelLINKALLRECORD" runat="server" >
<div class="PopupPanel">
<asp:UpdatePanel ID="UpdatePanel8" runat="server">
<ContentTemplate>
<table>
<tr>                       
<td > 
<div ID="Div2" runat="server" class="scrollableDiv"> 
<table style="width: 100%">
<tr>
<td align="left">
<asp:UpdatePanel ID="UpdatePanel9" runat="server">
<ContentTemplate>

<asp:GridView ID="GridLINKALLRECORD" runat="server" AutoGenerateColumns="False" DataKeyNames="#" CssClass="mGrid" >
<Columns>
<asp:TemplateField HeaderText="#" Visible="False" >
<ItemTemplate>
<asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px" ></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="SuplierID" HeaderText="SupplierID">
<HeaderStyle CssClass="Display_None"  HorizontalAlign="Left" 
VerticalAlign="Middle" Wrap="False" />
<ItemStyle  CssClass="Display_None" HorizontalAlign="Left" 
VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
<asp:TemplateField HeaderText="Select" >
<ItemTemplate>
<asp:RadioButton runat="server" ID="RBLINKALLRECORD"  GroupName="SuplierListLINKALLRECORD" />
</ItemTemplate>
<HeaderStyle Width="10px" Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
</asp:TemplateField>
<asp:BoundField DataField="PODate" HeaderText="Date">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
 
 <asp:BoundField DataField="ItemName" HeaderText="Item"  >
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>

<asp:BoundField DataField="ItemDesc" HeaderText="ItemDesc"  >
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>

<asp:BoundField DataField="Supplierrate" HeaderText="Supplier With Rate">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
</asp:BoundField> 
</Columns>
</asp:GridView>

</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
</table> 
</div>
</td>
</tr>
<tr>
<td align="center">
<asp:CheckBox ID="ChkLINKALLRECORD" runat ="server" Text="Confirm"  OnClientClick="javascript:HideALLPOPForceClose();"
CssClass="CheckBox" AutoPostBack="True"    OnCheckedChanged="ChkLINKALLRECORD_CheckedChanged"   />                                                                
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Panel>
</div>
</div>

               </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
           </asp:TemplateField>  
           
                <asp:TemplateField HeaderText="Vendor">
                 <ItemTemplate>
                     <asp:DropDownList ID="GrdddlVendor" runat="server" Width="150px" onchange="javascript:SETVALUE(this);" >                        
                    </asp:DropDownList>
                    <asp:Label ID="GrdlblVendorName" runat="server" Text='<%# Eval("VendorName") %>' CssClass="Display_None"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                 <ItemStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
             </asp:TemplateField>
             
            <asp:TemplateField HeaderText="Rate">
           <ItemTemplate>
               <asp:TextBox ID="GrdtxtRate" runat="server" CssClass="TextBoxNumeric" onKeydown="javascript:CalculateGrid(this);" OnChange="javascript:CalculateGrid(this);"
               MaxLength="10" Text='<%# Bind("AvgPurRate") %>' TextMode="SingleLine" Width="60px"></asp:TextBox>
               
               <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderrate" runat="server" FilterType="Numbers, Custom"
                ValidChars="." TargetControlID="GrdtxtRate" />
    
    
               </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="60px"/>
               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="60px"/>
           </asp:TemplateField>  
           
           
                 <asp:TemplateField HeaderText="OrdQty">
           <ItemTemplate>
               <asp:TextBox ID="GrdtxtOrdQty" runat="server" CssClass="TextBoxNumeric" onKeydown="javascript:CalculateGrid(this);" OnChange="javascript:CalculateGrid(this);"
               MaxLength="10" Text='<%# Bind("OrdQty") %>'  Width="40px"></asp:TextBox>
               
                 <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderordqty" runat="server" FilterType="Numbers, Custom"
                ValidChars="." TargetControlID="GrdtxtOrdQty" />
                
                   <asp:DropDownList ID="GrdddlUNITCONVERT" runat="server" Width="80px" AutoPostBack="true"
                   OnSelectedIndexChanged="GrdddlUNITCONVERT_SelectedIndexChanged">
                   </asp:DropDownList>
                
               </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
           </asp:TemplateField>      
           
           <asp:TemplateField HeaderText="VAT (%)">
           <ItemTemplate>
               <asp:TextBox ID="GrdtxtPerVAT" runat="server" CssClass="TextBoxNumeric" onkeydown="javascript:CalculateGrid(this);" OnChange="javascript:CalculateGrid(this);"
               MaxLength="10" Text='<%# Bind("pervat") %>'  Width="40px"></asp:TextBox>
               <asp:RangeValidator runat="server" id="RGrdtxtPerVAT" controltovalidate="GrdtxtPerVAT" type="Double" 
               minimumvalue="0.00" maximumvalue="12.50" 
               errormessage="***" />
                 <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderpervat" runat="server" FilterType="Numbers, Custom"
                ValidChars="." TargetControlID="GrdtxtPerVAT" />
               </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
               
           </asp:TemplateField>      
           
           <asp:TemplateField HeaderText="VAT">
           <ItemTemplate>
               <asp:TextBox ID="GrdtxtVAT" runat="server" CssClass="TextBoxNumericReadOnly" Enabled="false"
               MaxLength="10" Text='<%# Bind("vat") %>'  Width="40px"></asp:TextBox>
               
                 <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtendervat" runat="server" FilterType="Numbers, Custom"
                ValidChars="." TargetControlID="GrdtxtVAT" />
                
                
               </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
           </asp:TemplateField>      
           
           <asp:TemplateField HeaderText="DISC (%)">
           <ItemTemplate>
               <asp:TextBox ID="GrdtxtPerDISC" runat="server" CssClass="TextBoxNumeric"  onKeydown="javascript:CalculateGrid(this);" OnChange="javascript:CalculateGrid(this);"
               MaxLength="10" Text='<%# Bind("perdisc") %>' Width="40px"></asp:TextBox>
               
                 <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderperdisc" runat="server" FilterType="Numbers, Custom"
                ValidChars="." TargetControlID="GrdtxtPerDISC" />
                
                
               </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
           </asp:TemplateField>      
           
           <asp:TemplateField HeaderText="DISC">
           <ItemTemplate>
               <asp:TextBox ID="GrdtxtDISC" runat="server" CssClass="TextBoxNumericReadOnly" Enabled="false"
               MaxLength="10" Text='<%# Bind("disc") %>'  Width="40px"></asp:TextBox>
               
                 <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderdisc" runat="server" FilterType="Numbers, Custom"
                ValidChars="." TargetControlID="GrdtxtDISC" />
                
                
               </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
           </asp:TemplateField>      
           
            <asp:TemplateField HeaderText="Vendor1">
                 <ItemTemplate>
                   <%--  <asp:Label ID="GrdlblVendorID" runat="server" Text='<%# Eval("VendorID") %>' Width="30px"></asp:Label>--%>
                      <asp:TextBox ID="GrdtxtVendorID" runat="server" CssClass="TextBoxNumeric" 
               MaxLength="10" Text='<%# Bind("VendorID") %>'  Width="20px"></asp:TextBox>
                 </ItemTemplate>
                 <HeaderStyle Width="120px" CssClass="Display_None"/>
                 <ItemStyle Width="120px" CssClass="Display_None"/>
             </asp:TemplateField>
            <asp:BoundField HeaderText="ItemId" DataField="ItemId">
             <HeaderStyle Wrap="false"  CssClass="Display_None"/>
             <ItemStyle Wrap="false" CssClass="Display_None"/>
             </asp:BoundField> 
             
               <asp:TemplateField HeaderText="Remark">
                 <ItemTemplate>
                      <asp:TextBox ID="GrdtxtRemarkForPO" runat="server" CssClass="TextBox" TextMode="MultiLine" 
                       Text='<%# Bind("RemarkForPO") %>'  Width="190px">
                       </asp:TextBox>
                 </ItemTemplate>
             </asp:TemplateField>
             
                                   
            <asp:TemplateField HeaderText="">
                 <ItemTemplate>
                     <asp:ImageButton ID="ImgAddSupplier" runat="server" ValidationGroup="AddSupplier"
                    ImageUrl="~/Images/Icon/Gridadd.png" onclick="ImgAddSupplier_Click"  ToolTip="Add To Purchase Order"/>
                 </ItemTemplate>
                 <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Middle"/>
                 <ItemStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Middle"/>
             </asp:TemplateField>
             <asp:BoundField HeaderText="RequisitionCafeId" DataField="RequisitionCafeId">
             <HeaderStyle Wrap="false"  CssClass="Display_None"/>
             <ItemStyle Wrap="false" CssClass="Display_None"/>
             </asp:BoundField>
        </columns>
     </asp:GridView>    
        </td>
     </tr>
     <tr>
     <td align="left">
         <asp:LinkButton ID="hyl_AddAll" runat="server" CssClass="linkButton" ToolTip="Add All Item To Purchase Order"
             onclick="hyl_AddAll_Click">Add All</asp:LinkButton>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:LinkButton ID="hylAddCancel" runat="server" CssClass="linkButton" 
             onclick="hylAddCancel_Click">Cancel</asp:LinkButton>
     </td>
     </tr>
     </table> 
    </div>
    </fieldset>
</td>
</tr>
<tr id="TR_PODtls" runat="server">
    <td colspan="2">
        <fieldset id="FS_Requisition"  class="FieldSet" runat="server" style="width: 100%">
            <legend id="Legend2" class="legend" runat="server">  Indent Details</legend>
            <div ID="Div4" runat="server" class="scrollableDiv">
            <table width="100%">
                <tr>
                <td> 
                 <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="true">
                     <ContentTemplate>
                        <asp:GridView ID="GrdPODtls" runat="server" AutoGenerateColumns="False" 
                             CssClass="mGrid" DataKeyNames="#" >
                             <Columns>
                                 <asp:TemplateField HeaderText="#" Visible="False">
                                     <ItemTemplate>
                                         <asp:Label ID="LblProcessId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                         <asp:ImageButton ID="ImageGridEdit" runat="server" Visible="false"
                                            CommandArgument='<%# Eval("#") %>' 
                                             CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                         <asp:ImageButton ID="ImgBtnDelete" runat="server" Visible="false"
                                             CommandArgument="<%#((GridViewRow)Container).RowIndex %>" 
                                             CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                         <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                             ConfirmText="Would You Like To Delete The Record..!" 
                                             TargetControlID="ImgBtnDelete">
                                         </ajax:ConfirmButtonExtender>
                                     </ItemTemplate>
                                     <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"/>
                                     <HeaderStyle Width="20px" CssClass="Display_None"/>
                                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" CssClass="Display_None" 
                                         Wrap="false" />
                                 </asp:TemplateField>
                                 <asp:BoundField DataField="Cafeteria" HeaderText="Cafeteria">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="ItemCode" HeaderText="Code">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                 </asp:BoundField>
                                 <asp:BoundField DataField="ItemName" HeaderText="Item">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>                                 
                                 <asp:BoundField DataField="OrdQty" HeaderText="Ord Qty">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="StoreLocation" HeaderText="Store Location">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>                                 
                             </Columns>
                         </asp:GridView>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                 </td>
                 </tr>
                 <tr>
                 <td align="left">
                     <asp:LinkButton ID="hyl_Hide" runat="server" CssClass="linkButton" 
                         onclick="hyl_Hide_Click">Hide</asp:LinkButton>                     
                 </td>
                 </tr>
                 </table>
             </div>
         </fieldset>
    </td>
</tr>

 <tr>
        <td align="left" colspan="2">
        
        <fieldset id="Fieldset1"  class="FieldSet" runat="server" style="width: 100%">
            <legend id="Legend1" class="legend" runat="server">  Purchase Order Details</legend>
            <div ID="Div1" runat="server" class="ScrollableDiv_FixHeightWidth4">
               <table width="100%">
                <tr>
                <td> 
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true">
                     <ContentTemplate>
                        <asp:GridView ID="PurOrderGrid" runat="server" AutoGenerateColumns="False" 
                             CssClass="mGrid" DataKeyNames="#" 
                             onrowdatabound="PurOrderGrid_RowDataBound" 
                             onrowcommand="PurOrderGrid_RowCommand" 
                             onrowdeleting="PurOrderGrid_RowDeleting" >
                             <Columns>
                                 <asp:TemplateField HeaderText="#" Visible="False">
                                     <ItemTemplate>
                                         <asp:Label ID="LblProcessId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                         <asp:ImageButton ID="ImageGridEdit" runat="server" Visible="false"
                                             CommandArgument="<%#((GridViewRow)Container).RowIndex %>" 
                                             CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                         <asp:ImageButton ID="ImgBtnDelete" runat="server" 
                                             CommandArgument="<%#((GridViewRow)Container).RowIndex %>" 
                                             CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                         <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                             ConfirmText="Would You Like To Delete The Record..!" 
                                             TargetControlID="ImgBtnDelete">
                                         </ajax:ConfirmButtonExtender>
                                     </ItemTemplate>
                                     <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     <HeaderStyle Width="20px" />
                                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" 
                                         Wrap="false" />
                                 </asp:TemplateField>
                                 <asp:BoundField DataField="Code" HeaderText="Code">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Item" HeaderText="Item">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="ItemDescID" HeaderText="DescriptionID">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                 </asp:BoundField>
                                 <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="ReqQty" HeaderText="Avl. Qty">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>                                 
                                 <asp:BoundField DataField="OrdQty" HeaderText="Ord. Qty">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Vendor" HeaderText="Vendor">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="PurchaseRate" HeaderText="Rate">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 
                                   <asp:BoundField DataField="pervat" HeaderText="VAT(%)">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField> 
                                 
                                   <asp:BoundField DataField="vat" HeaderText="VAT">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField> 
                                 
                                   <asp:BoundField DataField="perdisc" HeaderText="DISC(%)">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False"/>
                                 </asp:BoundField> 
                                 
                                   <asp:BoundField DataField="disc" HeaderText="DISC">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 
                                 <asp:BoundField DataField="PurchaseAmount" HeaderText="Amount">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>                
                                 <asp:BoundField DataField="VendorId" HeaderText="VendorId">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                 </asp:BoundField>                
                                 <asp:BoundField DataField="ItemId" HeaderText="ItemId">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                 </asp:BoundField>    
                                   <asp:BoundField DataField="RequisitionCafeId" HeaderText="RequisitionCafeId">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                 </asp:BoundField> 
                                 
                                     <asp:BoundField DataField="UnitConvDtlsId" HeaderText="UnitConvDtlsId">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                 </asp:BoundField> 
                                 
                                     <asp:BoundField DataField="MainUnitQty" HeaderText="MainUnitQty">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                 </asp:BoundField> 
                                 
                                    <asp:TemplateField HeaderText="TERMS CONDITION">                        
                                    <ItemTemplate>
                                         <asp:ImageButton ID="ImgBtnAddTermsSuplier" runat="server" Visible="true"
                                             CommandArgument="<%#((GridViewRow)Container).RowIndex %>" 
                                             CommandName="TERMS" ImageUrl="~/Images/Icon/Gridadd.png" ToolTip="Add Term & Condition" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                                    Width="6%" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="TERMS CONDITION">                        
                                    <ItemTemplate>
                                      <asp:TextBox ID="TXTTERMSCONDITIONPOGRID" Text='<%# Bind("TXTTERMSCONDITIONPOGRID") %>' runat="server" CssClass="Display_None"></asp:TextBox>
                                      
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"
                                    Width="6%" />
                                    </asp:TemplateField>
                                              
                                    <asp:TemplateField HeaderText="TERMS CONDITION">                        
                                    <ItemTemplate>
                                      <asp:TextBox ID="TXTTERMSCONDITIONPOGRIDPAYMNET" Text='<%# Bind("TXTTERMSCONDITIONPOGRIDPAYMNET") %>' runat="server" CssClass="Display_None"></asp:TextBox>
                                      
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"
                                    Width="6%" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Remark">                        
                                    <ItemTemplate>
                                      <asp:TextBox ID="TXTREMARK" Text='<%# Bind("RemarkForPO") %>' 
                                        runat="server" CssClass="TextBox"></asp:TextBox>
                                      
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False"  />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                    </asp:TemplateField>
                                    
                             </Columns>
                         </asp:GridView>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                   </td>
                 </tr>
                 </table>
             </div>
         </fieldset>
        </td>
     </tr>
     

<tr >
<td class="Label">
    Net Total :
</td>
<td width="100px" align="left">
    <asp:TextBox ID="txtNetTotal" runat="server" CssClass="TextBoxReadOnly"></asp:TextBox>
</td>
</tr>

<%--Here New design for adding extra things--%>
<tr >
<td colspan="2">
<table width="100%">


<tr id="Tr1" runat="server">

<td class="Label"   align="right">Sub Total :</td><td class="Label" align="right">
<asp:TextBox ID="txtSubTotal" runat="server" CssClass="TextBoxReadOnly" Enabled="false" onkeyup="CalPercentage_Amount(this);"  Width="128px" style="text-align:right" TabIndex="19"></asp:TextBox>
    Rs/-</td>

<td class="Label"  align="right">Discount :</td><td class="Label" align="right">
<asp:TextBox ID="txtDiscount"  onkeyup="CalPercentage_Amount(this);"  OnChange="CalPercentage_Amount(this);"  runat="server" TabIndex="21"
CssClass="TextBoxReadOnly" Width="128px" style="text-align:right"  ></asp:TextBox>
    Rs/-</td>

<td class="Label"  align="right">VAT :</td><td class="Label" align="right">
<asp:TextBox ID="txtVATAmount" runat="server"  onkeyup="CalPercentage_Amount(this);" OnChange="CalPercentage_Amount(this);"  TabIndex="23"
CssClass="TextBoxReadOnly" Width="128px" style="text-align:right" ></asp:TextBox>
    Rs/-</td>

</tr>


<tr id="Tr6" runat="server">
<td class="Label" align="right">Excise Duty :</td><td class="Label" align="right">
<asp:TextBox runat="server" ID="txtexcisedutyper" ToolTip="Enter Percentage For Here" onkeyup="GetAmountOfExciseDuty();" TabIndex="25" OnChange="GetAmountOfExciseDuty();"
CssClass="TextBox" Width="30px" style="text-align:right"></asp:TextBox>%=
<asp:TextBox ID="txtexciseduty" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="25" OnChange="CalPercentage_Amount(this);"
CssClass="TextBoxReadOnly" Width="128px" style="text-align:right" Enabled="false"></asp:TextBox>
    Rs/-</td>

</tr>

<tr id="Tr4" runat="server">
<td class="Label" align="right">Hamali :</td><td class="Label" align="right">
<asp:CheckBox runat="server" ID="CHKHAMALI" Text="AT ACTUAL" onclick="javascript:EnableTextBox();"/>
<asp:TextBox ID="txtHamaliAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="25" OnChange="CalPercentage_Amount(this);"
CssClass="TextBox" Width="128px" style="text-align:right"></asp:TextBox>
    Rs/-</td>


<td class="Label"  align="right">Transport / Freight :</td><td class="Label" align="right">
<asp:CheckBox runat="server" ID="CHKFreightAmt" Text="AT ACTUAL" onclick="javascript:EnableTextBox();"/>
<asp:TextBox ID="txtFreightAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="28" OnChange="CalPercentage_Amount(this);"
CssClass="TextBox" Width="128px" style="text-align:right"></asp:TextBox>
    Rs/-</td>
    
<td class="Label"  align="right">Other Charges :</td><td class="Label" align="right">
<asp:CheckBox runat="server" ID="CHKOtherCharges" Text="AT ACTUAL" onclick="javascript:EnableTextBox();"/>
<asp:TextBox ID="txtOtherCharges" runat="server" CssClass="TextBox" Width="128px" style="text-align:right" TabIndex="31" OnChange="CalPercentage_Amount(this);"
onkeyup="CalPercentage_Amount(this);"></asp:TextBox>
    Rs/-</td>
</tr>

<tr id="Tr7" runat="server" class="Display_None">

    <td class="Label" align="right">Dekhrekh :</td><td class="Label" align="right">

<asp:TextBox ID="txtDekhrekhAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="24" OnChange="CalPercentage_Amount(this);"
CssClass="TextBox" Width="128px" style="text-align:right"></asp:TextBox>
    Rs/-</td>

<td class="Label"  align="right">Packing :</td><td class="Label" align="right">

<asp:TextBox ID="txtPackingAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="29" OnChange="CalPercentage_Amount(this);"
CssClass="TextBox" Width="128px" style="text-align:right"></asp:TextBox>
    Rs/-</td>

<td class="Label"  align="right">Cess :</td><td class="Label" align="right">
<asp:TextBox ID="txtCESSAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="27" OnChange="CalPercentage_Amount(this);"
CssClass="TextBox" Width="128px" style="text-align:right"></asp:TextBox>
    Rs/-</td>

</tr>

<tr >


    <td class="Label"  align="right">Loading / Unloading :</td><td class="Label" align="right">
    <asp:CheckBox runat="server" ID="CHKLoading" Text="AT ACTUAL" onclick="javascript:EnableTextBox();"/>
<asp:TextBox ID="txtPostageAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="30" OnChange="CalPercentage_Amount(this);"
CssClass="TextBox" Width="128px" style="text-align:right" ></asp:TextBox>
    Rs/-</td>
    
<td class="Label"  align="right">Ser. Tax :</td><td class="Label" align="right">
<asp:DropDownList ID="DDLSERVICETAX" runat="server" Width="80px" CssClass="ComboBox" onchange="CalAsPerDDl();">
</asp:DropDownList>

<asp:TextBox ID="txtSerTax" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="27" OnChange="CalPercentage_Amount(this);"
CssClass="TextBox" Width="128px" style="text-align:right"></asp:TextBox>
    Rs/-</td>
    
<td align="left" class="Label">
    Grand Total :</td><td class="Label" align="right">
<asp:TextBox ID="txtGrandTotal" runat="server" CssClass="TextBoxReadOnly" Width="128px" style="text-align:right" TabIndex="32" Enabled="false"></asp:TextBox>
        Rs/-</td>

</tr>

<tr >
<td class="Label"  align="right">Narration :</td>
<td colspan="5" align="right">
<asp:TextBox runat="server" ID="TXTNARRATION" Width="900px" CssClass="TextBox" TextMode="MultiLine"></asp:TextBox>
</td>
    
</tr>

</table>
</td>
</tr>
<%--end of new design--%>
  
<%--Here Design of terms and Condition Start--%>
<tr runat="server" class="Display_None">
<td colspan="6"  width="100%" align="left"><%--colspan="6"--%>
<ajax:Accordion ID="Accordion1" runat="server" ContentCssClass="accordionContent1"
HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" FadeTransitions="true"
TransitionDuration="260" FramesPerSecond="20" AutoSize="None"
RequireOpenedPane="false"
SuppressHeaderPostbacks="true"
SelectedIndex="1">
<Panes>
<ajax:AccordionPane ID="AccordionPane1" runat="server" Width="100%">
<Header>
<a class="href" href="#">Terms and Conditions</a></Header>
<Content>
<div style="width:100%">
<asp:GridView ID="GridTermCond" runat="server" AutoGenerateColumns="False" 
DataKeyNames="#" Width="100%"  CssClass="mGrid"
TabIndex="4" AllowSorting="True" 
ShowFooter="True">
<Columns>
<asp:TemplateField HeaderText="#" Visible="False">
<EditItemTemplate>
<asp:Label ID="LblEntryId0" runat="server" Width="30px"></asp:Label>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="All">
<HeaderTemplate>
<asp:CheckBox ID="GrdSelectAllHeader" runat="server" AutoPostBack="true" OnCheckedChanged="GrdSelectAllHeader1_CheckedChanged"/>
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="GrdSelectAll" runat="server" AutoPostBack="false" 
Checked='<%# Convert.ToBoolean(Eval("select").ToString()) %>' />
</ItemTemplate>
<FooterTemplate>
<asp:ImageButton ID="img_btn_Add" runat="server" 
ImageUrl="~/Images/Icon/Gridadd.png" TabIndex="8" OnClick="img_btn_Add_Click"/>
</FooterTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Sr.No.">                        
<ItemTemplate>
<asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
Width="6%" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Title" ControlStyle-Width="200px" ControlStyle-Height="20px" HeaderStyle-HorizontalAlign="Left" >
<ItemTemplate>
<asp:TextBox ID="GrtxtTermCondition_Head" runat="server"  MaxLength="400" TextMode="MultiLine" 
CssClass="TextBox" Text='<%# Bind("Title") %>' TabIndex="6" ></asp:TextBox>
</ItemTemplate>
<ControlStyle Height="30px" Width="200px" />
<HeaderStyle HorizontalAlign="Left" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Description" ControlStyle-Width="200px" ControlStyle-Height="20px" HeaderStyle-HorizontalAlign="Left" >
<ItemTemplate>
<asp:TextBox ID="GrtxtDesc" runat="server"  MaxLength="400" TextMode="MultiLine" 
CssClass="TextBox" Text='<%# Bind("TDescptn") %>' TabIndex="6" ></asp:TextBox>
</ItemTemplate>
<ControlStyle Height="30px" Width="480px" />
<HeaderStyle HorizontalAlign="Left" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
</asp:TemplateField>
</Columns>
<PagerStyle CssClass="pgr" />
</asp:GridView> 
</div>
</Content>
</ajax:AccordionPane>
</Panes>
</ajax:Accordion>
</td>
</tr>
<%--Here Design of terms and Condition End--%>

<%--Here Design of PAYMENT TERMS Start--%>
<tr>
<td colspan="6"  width="100%" align="left" visible="false" runat="server" id="TRID"><%--colspan="6"--%>
<ajax:Accordion ID="Accordion2" runat="server" ContentCssClass="accordionContent1"
HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" FadeTransitions="true"
TransitionDuration="260" FramesPerSecond="20" AutoSize="None"
RequireOpenedPane="false"
SuppressHeaderPostbacks="true"
SelectedIndex="1">
<Panes>
<ajax:AccordionPane ID="AccordionPane2" runat="server" Width="100%">
<Header>
<a class="href" href="#">Payment Terms</a></Header>
<Content>
<div style="width:100%">

<asp:RadioButtonList ID="RdoPaymentDays" runat="server" CellPadding="25"  RepeatDirection="Horizontal" CssClass="RadioButton">
<asp:ListItem Selected="True" Text="Immediate&nbsp;&nbsp;" Value="1"></asp:ListItem>
<asp:ListItem Text="15 Days&nbsp;&nbsp;&nbsp;" Value="2"></asp:ListItem>
<asp:ListItem Text="21 Days&nbsp;&nbsp;&nbsp;" Value="3"></asp:ListItem>
<asp:ListItem Text="30 Days&nbsp;&nbsp;&nbsp;" Value="4"></asp:ListItem>
<asp:ListItem Text="45 Days&nbsp;&nbsp;&nbsp;" Value="5"></asp:ListItem>
<asp:ListItem Text="60 Days" Value="6"></asp:ListItem>
</asp:RadioButtonList>
 
</div>
</Content>
</ajax:AccordionPane>
</Panes>
</ajax:Accordion>
</td>
</tr>
<%--Here Design of terms and Condition End--%>
    
<tr>
<td align="center" colspan="2" >
    <table align="center" width="25%">
<tr>
<td>
<asp:Button ID="BtnUpdate" CssClass="button" runat="server" Text="Update" ValidationGroup="Add" onclick="BtnUpdate_Click" />
<ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
TargetControlID="BtnUpdate">
</ajax:ConfirmButtonExtender>
</td>
<td>
<asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" 
ValidationGroup="Add" onclick="BtnSave_Click" />
<asp:TextBox ID="TXTJAVASCRIPTFLAG" runat="server"  MaxLength="10" TextMode="SingleLine" 
CssClass="Display_None"  ></asp:TextBox>
</td>
<td>
<asp:Button ID="BtnDelete" CssClass="button" runat="server" Text="Delete" 
        ValidationGroup="Add" onclick="BtnDelete_Click" />
</td>

<td>
<asp:Button ID="BtnCancel" CssClass="button" runat="server" Text="Cancel" 
        onclick="BtnCancel_Click" CausesValidation="False" />
</td>
</tr>

</table>
</td>
</tr>
      <tr>
          <td align="center" colspan="2">
              <div ID="Div5" runat="server" class="ScrollableDiv">
                 <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                     <ContentTemplate>
                        <asp:GridView ID="ReportGrid" runat="server" AutoGenerateColumns="False" 
                             CssClass="mGrid" DataKeyNames="#" AllowPaging="True" 
                             onrowcommand="ReportGrid_RowCommand" 
                             onrowdeleting="ReportGrid_RowDeleting" ondatabound="ReportGrid_DataBound" onpageindexchanging="GrdReqPO_PageIndexChanging">
                             <Columns>
                                 <asp:TemplateField HeaderText="#" Visible="False">
                                     <ItemTemplate>
                                         <asp:Label ID="LblEstimateId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                         <asp:ImageButton ID="ImageAccepted" runat="server" Visible="false"
                                             CommandArgument='<%# Eval("#") %>' 
                                             CommandName="Acctepted" ImageUrl="~/Images/New Icon/DoneChanges.png" ToolTip="Order Accepted Can't Edit" />                                         
                                         <asp:ImageButton ID="ImageGridEdit" runat="server" Visible="true"
                                             CommandArgument='<%# Eval("#") %>' 
                                             CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                         <asp:ImageButton ID="ImageApprove" runat="server" Visible="false"
                                             CommandArgument='<%# Eval("#") %>' 
                                             CommandName="Approved" ImageUrl="~/Images/New Icon/LockReport.png" ToolTip="Order Approved Can't Delete" />
                                         <asp:ImageButton ID="ImgBtnDelete" runat="server" 
                                             CommandArgument='<%# Eval("#") %>' 
                                             CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />                                             
                                         <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                             ConfirmText="Would You Like To Delete The Record..!" 
                                             TargetControlID="ImgBtnDelete">
                                         </ajax:ConfirmButtonExtender>                                         
                                     
                                           <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="PS"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="NOPDF"%>&PrintFlag=<%="NO" %>' target="_blank">
                                         <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                                                ToolTip="Print Purchase Order" TabIndex="29" />
                                         </a>
                                            <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="PS"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="PDF"%>&PrintFlag=<%="NO" %>' target="_blank">
                                         <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png"
                                                ToolTip="PDF Purchase Order" TabIndex="29" />
                                         </a>
                                          <asp:ImageButton ID="ImgEmail" runat="server" CausesValidation="False" 
                                                    CommandArgument='<%# Eval("#") %>' CommandName="Email" Height="16px" 
                                                    ImageUrl="~/Images/Icon/e-mail.png" TabIndex="9" ToolTip="MARK AS PRINT AND MAIL TO SUPPLIER MANUALLY."  Visible="false"/>
                                                
                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                CommandArgument='<%# Eval("#") %>' CommandName="CANCELAUTHPO" Height="16px" 
                                                ImageUrl="~/Images/New Icon/Cancel__Black.png" TabIndex="9" ToolTip="Cancel Purchase Order"  Visible="true"/>  
                                                        
                                     </ItemTemplate>
                                     <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     <HeaderStyle Width="20px" />
                                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" 
                                         Wrap="false" />
                                 </asp:TemplateField>                               
                                 <asp:BoundField DataField="SuplierName" HeaderText="Supplier">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>                                 
                                 <asp:BoundField DataField="PONo" HeaderText="PO No">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="PODate" HeaderText="PO Date">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Amount" HeaderText="Amount">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>                                 
                                 <asp:BoundField DataField="POStatus" HeaderText="Status">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>                                 
                                 <asp:BoundField DataField="POId" HeaderText="POId">
                                     <HeaderStyle CssClass="Display_None" />
                                     <ItemStyle CssClass="Display_None"/>
                                 </asp:BoundField>
                                 <asp:BoundField DataField="SuplierId" HeaderText="SuplierId">
                                     <HeaderStyle CssClass="Display_None" />
                                     <ItemStyle CssClass="Display_None"/>
                                 </asp:BoundField>
                                  <asp:BoundField DataField="MailSend" HeaderText="MailSend">
                                     <HeaderStyle CssClass="Display_None" />
                                     <ItemStyle CssClass="Display_None"/>
                                 </asp:BoundField>
                             </Columns>
                         </asp:GridView>
                     </ContentTemplate>
                 </asp:UpdatePanel>
             </div>
          </td>
      </tr>
<tr>
<td> <asp:Label runat="server" Text="HIGHLIGHT ROWS SHOWS MAIL SEND TO THE SUPPLIER.." ID="LBLREDMARK" Font-Bold="true" Font-Size="Smaller" ForeColor="Red"></asp:Label> </td>
</tr>
</table>

<asp:UpdatePanel runat="server" >
<ContentTemplate>
<div runat="server" id="Panel1" class="Display_None">
<fieldset id="Fieldset3" runat="server" class="FieldSet" style="background-color:Silver">
<table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblCount" runat="server" CssClass="SubTitle" Text="Please Reorder Following Items"></asp:Label>
            </td>
            <td align="right" valign="top"> 
            <a href='../Transactions/PurchaseOrderDtls.aspx' target="_blank" class="Info">Place Order</a>
            <asp:ImageButton ID="ImgBtnClose" runat="server" 
                 ImageUrl="~/Images/Icon/CloseButton.png" ToolTip="Close" 
                    onclick="ImgBtnClose_Click" />              
            </td>
       </tr> 
       <tr>
            <td colspan="2" align="center">
            <div id="divPrint" class="scrollableDiv" style="width:98%">
                <asp:GridView ID="GrdReorder" runat="server" ShowFooter="true"
                    AutoGenerateColumns="False" CaptionAlign="Top" AllowPaging="true" CssClass="mGrid"                        
                    Width="100%" PageSize="5" 
                    onpageindexchanging="GrdReorder_PageIndexChanging" >
                    <Columns>
                    <asp:TemplateField HeaderText="Sr.No.">                        
                    <ItemTemplate>
                    <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                     Width="6%" />
                     </asp:TemplateField>
                        <asp:BoundField DataField="ItemCode" HeaderText="Code" >
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False"/>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemName" HeaderText="Item">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ReorderLavel" HeaderText="Reorder Level">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" Width="50px"/>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" Width="50px"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="AvilableQty" HeaderText="Reached Level">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" Width="50px"/>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" Width="50px"/>
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
                    <FooterStyle CssClass="ftr" />
                </asp:GridView>
                </div>
            </td>
        </tr>
            </table> 
</fieldset></div>

<ajax:AlwaysVisibleControlExtender   
ID="AlwaysVisibleControlExtender1" 
TargetControlID="Panel1"  
VerticalSide="Bottom"  
VerticalOffset="10"  
HorizontalSide="Right"  
HorizontalOffset="10"  
ScrollEffectDuration=".1"
runat="server" /></ContentTemplate> </asp:UpdatePanel>
</td></tr></table>



<div id="dialog" class="PopUpSample" runat="server">
<div id="progressBackgroundFilter1"></div>
<div id="DivSHOWPOPUP" class="PopUpSample" runat="server">
<table width="95%" cellspacing="16px">
<tr>
<td colspan="2"><asp:Label runat="server" ID="LBLTEXT" Text="ARE YOU SURE TO DELETE PURCHASE ORDER" CssClass="LabelPOP"></asp:Label></td>
<td id="Td1" runat="server" align="right">
<asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="javascript:HidePOPForceClose(2);"            
ImageUrl="~/Images/New Icon/close-button1.png" ToolTip="Close" 
onclick="ImgBtnClose_Click" />    
</td>
</tr>
<tr>
<td colspan="2">
<div id="divPrintsd" class="ScrollableDiv_FixHeightWidthPOP">
<asp:GridView ID="GRDPOPUPFOREDIT" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
<Columns>
<asp:BoundField DataField="POID" HeaderText="POID">
<HeaderStyle CssClass="Display_None" HorizontalAlign="Center" 
VerticalAlign="Middle" Wrap="False" />
<ItemStyle CssClass="Display_None" HorizontalAlign="Left" 
VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
<asp:BoundField DataField="PONO" HeaderText="Purchase Order No">
<HeaderStyle  HorizontalAlign="Center" 
VerticalAlign="Middle" Wrap="False" />
<ItemStyle  HorizontalAlign="Left" 
VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
<asp:BoundField DataField="PODate" HeaderText="Purchase Order Date">
<HeaderStyle  HorizontalAlign="Center" 
VerticalAlign="Middle" Wrap="False" />
<ItemStyle  HorizontalAlign="Left" 
VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
<asp:BoundField DataField="SuplierName" HeaderText="Supplier Name">
<HeaderStyle  HorizontalAlign="Center" 
VerticalAlign="Middle" Wrap="False" />
<ItemStyle  HorizontalAlign="Left" 
VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>

<asp:BoundField DataField="POAmount" HeaderText="Amount">
<HeaderStyle  HorizontalAlign="Center" 
VerticalAlign="Middle" Wrap="False" />
<ItemStyle  HorizontalAlign="Left" 
VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
<asp:BoundField DataField="Status" HeaderText="Status">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
</Columns>
</asp:GridView>
</div>
</td>
</tr>
<tr>
<td class="Label">
Remark:
</td>
<td>
<asp:TextBox ID="TxtRemarkAlL" width="760px" TextMode="MultiLine" runat="server" CssClass="TextBox"></asp:TextBox>
</td>
</tr>
<tr>
<td align="center" colspan="2">
<asp:Button ID="BTNCANCELPO" CssClass="buttonpayment" runat="server" Text="CANCEL PURCHASE ORDER" 
CausesValidation="false" onclick="BTNCANCELPO_Click"/>
<ajax:RoundedCornersExtender ID="RoundedCornersExtender5" runat="server" TargetControlID="BTNCANCELPO" Corners="All" Radius="8" BorderColor="Gray"></ajax:RoundedCornersExtender>          

</td>
</tr>
</table>
</div>
</div>


<div id="DIVTERMSSUPPLIER" class="PopUpSample" runat="server">
<div id="progressBackgroundFilter"></div>
<div id="Div7" class="PopUpSample" runat="server">
<table width="98%" cellspacing="16px">
<tr>
<td colspan="2"><asp:Label runat="server" ID="Label1" Text="Terms & Conditions :" CssClass="LabelPOP"></asp:Label>
<asp:Label runat="server" ID="LBLSUPPLERID" CssClass="Display_None"></asp:Label></td>
<td id="Td2" runat="server" align="right">
<asp:ImageButton ID="ImageButton3" runat="server" OnClientClick="javascript:HidePOPTermsPOSupplier();"            
ImageUrl="~/Images/New Icon/close-button1.png" ToolTip="Close Box" 
onclick="ImgBtnClose_Click" />    
</td>
</tr>
<tr>

<td class="Label" colspan="3">
<asp:TextBox runat="server" ID="POPUPTXTTERMSSUPPLIER" CssClass="TextBox" Width="850px" Height="100px" TextMode="MultiLine"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="3" align="left">
<asp:Label runat="server" ID="Label2" Text="Payment Conditions :" CssClass="LabelPOP"></asp:Label>
</td>
</tr>
<tr>
<td class="Label" colspan="3">
<asp:TextBox runat="server" ID="POPUPTXTTERMSSUPPLIERPayment" CssClass="TextBox" Width="850px" Height="100px" TextMode="MultiLine"></asp:TextBox>
</td>
</tr>
<tr>
<td align="center" colspan="3">
<asp:Button ID="BTNPOPUPTERMS" CssClass="button" runat="server" Text="SET" 
CausesValidation="false" onclick="BTNPOPUPTERMS_Click"/>
<ajax:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="BTNPOPUPTERMS" Corners="All" Radius="8" BorderColor="Gray"></ajax:RoundedCornersExtender>          

</td>
</tr>
</table>
</div>
</div>
 
 
 


 
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

