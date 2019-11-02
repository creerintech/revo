<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="EditAuthPurchaseOrder.aspx.cs" Inherits="Transactions_EditAuthPurchaseOrder" Title="Edit Authorised Purchase Order" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<script language="javascript" type="text/javascript">
    function EnterEvent(e) {
        if (e.keyCode == 13) {
            var _GridDetails = document.getElementById('<%= BTNLOGINFORM.ClientID %>');
            _GridDetails.click();
        }
    }
    
    function CalculateGrid(objGrid) {

var _GridDetails = document.getElementById('<%= GrdReqPO.ClientID %>');
var rowIndex = objGrid.offsetParent.parentNode.rowIndex;

var Rate=(_GridDetails.rows[rowIndex].cells[12].children[0]);
var OrdQty=(_GridDetails.rows[rowIndex].cells[13].children[0]);
var pervat=(_GridDetails.rows[rowIndex].cells[15].children[0]);
var vat=(_GridDetails.rows[rowIndex].cells[16].children[0]);
var perdisc=(_GridDetails.rows[rowIndex].cells[17].children[0]);
var disc=(_GridDetails.rows[rowIndex].cells[18].children[0]);
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
vat.value = parseFloat((pervat.value / 100) * ((Rate.value * OrdQty.value) - disc.value)).toFixed(2);

var FSUBTOTAL = 0;
var FNETTOTAL = 0;
var PerTax = 0;
var AmtTax = 0;
var PerDisc = 0;
var AmtDisc = 0;

var FAMOUNT;
var FNETAMT;
var FPERTAX;
var FTAX;
var FPERDISC;
var FDISC;
for (var i = 1; i < _GridDetails.rows.length; i++) {
    FAMOUNT = (_GridDetails.rows[i].cells[12].children[0]);
    FNETAMT = (_GridDetails.rows[i].cells[13].children[0]);
    FPERTAX = (_GridDetails.rows[i].cells[15].children[0]);
    FTAX = (_GridDetails.rows[i].cells[16].children[0]);
    FPERDISC = (_GridDetails.rows[i].cells[17].children[0]);
    FDISC = (_GridDetails.rows[i].cells[18].children[0]);

    FSUBTOTAL = parseFloat(parseFloat(FAMOUNT.value) * parseFloat(FNETAMT.value)) + parseFloat(FSUBTOTAL);
    AmtTax = parseFloat(FTAX.value) + parseFloat(AmtTax);
    AmtDisc = parseFloat(FDISC.value) + parseFloat(AmtDisc);

}
var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');
var _TxtDiscount = document.getElementById('<%= txtDiscount.ClientID %>');
var _TxtVat = document.getElementById('<%= txtVATAmount.ClientID %>');
_TxtSubTotal.value = parseFloat(FSUBTOTAL).toFixed(2);
_TxtVat.value = parseFloat(AmtTax).toFixed(2);
_TxtDiscount.value = parseFloat(AmtDisc).toFixed(2);
CalAsPerDDl();
}



function CalPercentage_Amount(TxtBoxId)
{

var _TxtNETTotal = document.getElementById('<%= txtGrandTotal.ClientID %>');   
var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');   
var _TxtDiscount = document.getElementById('<%= txtDiscount.ClientID %>');   
var _TxtVat = document.getElementById('<%= txtVATAmount.ClientID %>');

var _txtHamaliAmt = document.getElementById('<%= txtHamaliAmt.ClientID %>');

var _txtFreightAmt = document.getElementById('<%= txtFreightAmt.ClientID %>');

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

if (_txtHamaliAmt.value=="" || isNaN(_txtHamaliAmt.value))
{
_txtHamaliAmt.value=0;           
}

if (_txtFreightAmt.value=="" || isNaN(_txtFreightAmt.value))
{
_txtFreightAmt.value=0;           
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

_TxtGrandTotal.value = parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) +parseFloat(_txtHamaliAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtFreightAmt.value) +  parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2);
_TxtNETTotal.value = parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) +  parseFloat(_txtHamaliAmt.value) +  parseFloat(_txtexciseduty.value) + parseFloat(_txtFreightAmt.value) +  parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2);
}



function CalAsPerDDl() {
    var _TxtNETTotal = document.getElementById('<%= txtGrandTotal.ClientID %>');
    var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');
    var _TxtDiscount = document.getElementById('<%= txtDiscount.ClientID %>');
    var _TxtVat = document.getElementById('<%= txtVATAmount.ClientID %>');
    
    var _txtHamaliAmt = document.getElementById('<%= txtHamaliAmt.ClientID %>');
    var _txtFreightAmt = document.getElementById('<%= txtFreightAmt.ClientID %>');
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
   
    if (_txtHamaliAmt.value == "" || isNaN(_txtHamaliAmt.value)) {
        _txtHamaliAmt.value = 0;
    }
  
    if (_txtFreightAmt.value == "" || isNaN(_txtFreightAmt.value)) {
        _txtFreightAmt.value = 0;
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

    _TxtSerTax.value = parseFloat((ddlValue / 100) * (parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) + parseFloat(_txtHamaliAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtFreightAmt.value) + parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2))).toFixed(2);

    _TxtGrandTotal.value = parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) + parseFloat(_TxtSerTax.value) +  parseFloat(_txtHamaliAmt.value) +  parseFloat(_txtexciseduty.value) + parseFloat(_txtFreightAmt.value) +parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2);
    _TxtNETTotal.value = parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) + parseFloat(_TxtSerTax.value) +  parseFloat(_txtHamaliAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtFreightAmt.value) +  parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2);

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
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
    <div id="divwidth"></div>
 </ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Authorised Purchase Order  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<table width="100%" cellspacing="8">
<tr>
<td class="Label">
    Purchase Order No :
</td>
<td colspan="5">
<ajax:ComboBox ID="ddlpono" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="230px" CssClass="CustomComboBoxStyle" Visible="false"
onselectedindexchanged="ddlpono_SelectedIndexChanged" ></ajax:ComboBox>

<%--THIS START HERE--%>
<%--Text='<%# Eval("ItemName") %>' ToolTip='<%# Eval("ItemToolTip") %> '--%>

<asp:UpdatePanel ID="UpdatePanel4" runat="server">
<ContentTemplate>
<asp:TextBox ID="TxtPONO" runat="server" 
CssClass="search_List" Width="692px" AutoPostBack="True"  ontextchanged="TxtPONO_TextChanged"></asp:TextBox>

<ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemName"   runat="server" 
TargetControlID="TxtPONO"    CompletionInterval="100"                               
UseContextKey="True" FirstRowSelected ="true" 
CompletionSetCount="20" 
ShowOnlyCurrentWordInCompletionListItem="true"  
ServiceMethod="GetCompletionItemNameList"
CompletionListCssClass="AutoExtender"
CompletionListItemCssClass="AutoExtenderList"
CompletionListHighlightedItemCssClass="AutoExtenderHighlight"                         
></ajax:AutoCompleteExtender> 

<ajax:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="TxtPONO" 
WatermarkText="Search P.O." WatermarkCssClass="water" />
</ContentTemplate>
</asp:UpdatePanel >
<%--THIS END HERE--%>

</td>
</tr>

<tr>
<td class="Label">
    PO By :
</td>
<td>
<asp:Label runat="server" ID="LBLPOTHROUGH"></asp:Label>
</td>
<td class="Label">
    PO Date :
</td>
<td>
<asp:TextBox runat="server" ID="txtpodate" CssClass="TextBox" Width="80px"></asp:TextBox>
<ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" 
PopupButtonID="txtpodate" TargetControlID="txtpodate" Enabled="True">
</ajax:CalendarExtender>
</td>
<td class="Label">
    For Company :
</td>
<td>
<ajax:ComboBox ID="ddlCompany" runat="server" DropDownStyle="DropDown" AutoPostBack="false"
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="230px" CssClass="CustomComboBoxStyle" ></ajax:ComboBox>
</td>
</tr>

<tr>
<td class="Label">
    PO Quot. No. :
</td>
<td>
<asp:TextBox ID="txtpoqtno" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
</td>
<td class="Label">
    PO Quot. Date :
</td>
<td>
<asp:TextBox runat="server" ID="txtquotdate" CssClass="TextBox" Width="80px"></asp:TextBox>
<ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
PopupButtonID="txtquotdate" TargetControlID="txtquotdate" Enabled="True">
</ajax:CalendarExtender>
</td>
<td class="Label">
    Indent No :
</td>
<td>
<asp:Label runat="server" ID="lblindentno" CssClass="Label"></asp:Label>
</td>
</tr>

<tr>
<td colspan="6">
<div runat="server" class="ScrollableDiv_FixHeightWidth4" id="DIVPOGRID">
<asp:GridView ID="GrdReqPO" runat="server" AutoGenerateColumns="False" Width="100%"
CssClass="mGrid" BackColor="White" BorderColor="#0CCCCC"
BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
AllowPaging="false" DataKeyNames="#" onrowdatabound="GrdReqPO_RowDataBound" 
        onrowcommand="GrdReqPO_RowCommand" onrowdeleting="GrdReqPO_RowDeleting" 
        >
<columns>
            <asp:TemplateField HeaderText="#" Visible="False">
                 <ItemTemplate>
                     <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
            <asp:TemplateField>
                 <ItemTemplate>
                   <asp:ImageButton ID="ImageGridEdit" runat="server" Visible="true"
                                             CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                             CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                     <asp:ImageButton ID="ImgBtnDelete" runat="server" 
                         CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete"/>
                     <ajax:ConfirmButtonExtender ID="KKKK" runat="server" 
                         ConfirmText="Would You Like To Delete The Record From Purchase Order..!" 
                         TargetControlID="ImgBtnDelete" >
                     </ajax:ConfirmButtonExtender>
                       <asp:CheckBox ID="GrdReqPO_CHK" runat="server" Height="20px" Width="20px"  CssClass="Display_None" />
                 </ItemTemplate>
                 <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                 <HeaderStyle Width="10px" />
                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" 
                     Wrap="false" />
             </asp:TemplateField>
            <asp:BoundField DataField="ItemId" HeaderText="ItemId">
                <HeaderStyle Wrap="False" CssClass="Display_None" />
                <ItemStyle Wrap="False" CssClass="Display_None"/>
            </asp:BoundField> 
              <asp:BoundField DataField="ItemDetailsId" HeaderText="ItemDetailsId">
                <HeaderStyle Wrap="False" CssClass="Display_None" />
                <ItemStyle Wrap="False" CssClass="Display_None"/>
           </asp:BoundField>  
              <asp:BoundField DataField="UnitID" HeaderText="UnitID">
                <HeaderStyle Wrap="False" CssClass="Display_None" />
                <ItemStyle Wrap="False" CssClass="Display_None"/>
           </asp:BoundField>  
            <asp:BoundField DataField="ItemCode" HeaderText="Code">
                <HeaderStyle Wrap="False" />
                <ItemStyle Wrap="False" />
            </asp:BoundField>    
            <asp:BoundField  HeaderText="Particular" DataField="ItemName">
           <HeaderStyle Wrap="false" Width="150px"  />
           <ItemStyle Wrap="false" Width="150px" />
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
           <asp:BoundField HeaderText="ReqByCafeteria" DataField="ReqByCafeteria">
           <HeaderStyle Wrap="false" CssClass="Display_None"/>
           <ItemStyle Wrap="false" CssClass="Display_None"/>
           </asp:BoundField>  
           <asp:TemplateField HeaderText="Vendor">
           <ItemTemplate>
           <asp:DropDownList ID="GrdddlVendor" runat="server" Width="150px" onchange="javascript:SETVALUE(this);" >                        
           </asp:DropDownList>
             <asp:TextBox runat="server" ID="GRDTXTSUPLID" Text='<%# Bind("SuplierId") %>' CssClass="Display_None"></asp:TextBox>
           </ItemTemplate>
           <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
           <ItemStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
           </asp:TemplateField>
           
           <asp:BoundField HeaderText="Vendor UOM" DataField="VUOM">
           <HeaderStyle Wrap="false" CssClass="Display_None"/>
           <ItemStyle Wrap="false" CssClass="Display_None"/>
           </asp:BoundField>  
           
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
          </ItemTemplate>
          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
          </asp:TemplateField>      
          
          <asp:TemplateField HeaderText="UOM">
          <ItemTemplate>
          <asp:DropDownList ID="GrdddlUOM" runat="server" Width="90px" >
           </asp:DropDownList>
          </ItemTemplate>
          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px"/>
          </asp:TemplateField>      
           
          <asp:TemplateField HeaderText="VAT (%)">
          <ItemTemplate>
          <asp:TextBox ID="GrdtxtPerVAT" runat="server" CssClass="TextBoxNumeric" onkeydown="javascript:CalculateGrid(this);" OnChange="javascript:CalculateGrid(this);"
          MaxLength="10" Text='<%# Bind("pervat") %>'  Width="40px"></asp:TextBox>
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
           <asp:TemplateField HeaderText="Remark">
           <ItemTemplate>
           <asp:TextBox ID="GrdtxtRemarkForPO" runat="server" CssClass="TextBox" TextMode="MultiLine" 
           Text='<%# Bind("RemarkForPO") %>'  Width="190px">
           </asp:TextBox>
           </ItemTemplate>
           </asp:TemplateField>
        </columns>
     </asp:GridView>
</div>
</td>
</tr>

<tr>
<td class="LabelextraDuty">
    Terms &amp; Condition :
</td>
<td align="left" colspan="5">
<asp:TextBox ID="TXTTERMSCONDITION" runat="server" CssClass="TextBox" Width="850px" TextMode="MultiLine" TabIndex="32"></asp:TextBox>
</td>
</tr>

<tr>
<td class="LabelextraDuty">
    Payment Terms :
</td>
<td align="left" colspan="5">
<asp:TextBox ID="TXTPaymentTerms" runat="server" CssClass="TextBox" Width="850px" TextMode="MultiLine" TabIndex="32"></asp:TextBox>
</td>
</tr>

<tr>
<td class="LabelextraDuty">
    Subtotal :
</td>
<td align="right">
<asp:TextBox ID="txtSubTotal" runat="server" CssClass="TextBoxReadOnly" Enabled="false" onkeyup="CalPercentage_Amount(this);"  Width="100px" style="text-align:right" TabIndex="19"></asp:TextBox>
    Rs/-</td>
<td class="LabelextraDuty">
    Total Discount :
</td>
<td align="right">
<asp:TextBox ID="txtDiscount"  onkeyup="CalPercentage_Amount(this);"  OnChange="CalPercentage_Amount(this);"  runat="server" TabIndex="21"
CssClass="TextBoxReadOnly" Width="100px" style="text-align:right" Enabled="false" ></asp:TextBox>
    Rs/-</td>
<td class="LabelextraDuty">
    Total VAT :
</td>
<td align="LabelextraDuty">
<asp:TextBox ID="txtVATAmount" runat="server"  onkeyup="CalPercentage_Amount(this);" OnChange="CalPercentage_Amount(this);"  TabIndex="23"
CssClass="TextBoxReadOnly" Width="100px" style="text-align:right" Enabled="false" ></asp:TextBox>
    Rs/-</td>
</tr>


<tr>
<td class="LabelextraDuty">
    Excise Duty :
</td>
<td align="right">
<asp:TextBox runat="server" ID="txtexcisedutyper" ToolTip="Enter Percentage For Here" onkeyup="GetAmountOfExciseDuty();" TabIndex="25" OnChange="GetAmountOfExciseDuty();"
CssClass="TextBox" Width="30px" style="text-align:right"></asp:TextBox>%=
<asp:TextBox ID="txtexciseduty" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="25" OnChange="CalPercentage_Amount(this);"
CssClass="TextBoxReadOnly" Width="100px" style="text-align:right" Enabled="false"></asp:TextBox>
    Rs/-</td>
<td class="LabelextraDuty">
    Hamali :
</td>
<td align="right">
<asp:CheckBox runat="server" ID="CHKHAMALI" Text="AT ACTUAL" onclick="javascript:EnableTextBox();"/>
<asp:TextBox ID="txtHamaliAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="25" OnChange="CalPercentage_Amount(this);"
CssClass="TextBox" Width="100px" style="text-align:right"></asp:TextBox>
  <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtsdendervat" runat="server" FilterType="Numbers, Custom"
           ValidChars="." TargetControlID="txtHamaliAmt" />
    Rs/-</td>
<td class="LabelextraDuty">
    Transport/Freight:
</td>
<td align="right">
<asp:CheckBox runat="server" ID="CHKFreightAmt" Text="AT ACTUAL" onclick="javascript:EnableTextBox();"/>
<asp:TextBox ID="txtFreightAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="28" OnChange="CalPercentage_Amount(this);"
CssClass="TextBox" Width="100px" style="text-align:right"></asp:TextBox>
  <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtesdnder1" runat="server" FilterType="Numbers, Custom"
           ValidChars="." TargetControlID="txtFreightAmt" />
    Rs/-</td>
</tr>

<tr>
<td class="LabelextraDuty">
    Other Charges :
</td>
<td align="right">
<asp:CheckBox runat="server" ID="CHKOtherCharges" Text="AT ACTUAL" onclick="javascript:EnableTextBox();"/>
<asp:TextBox ID="txtOtherCharges" runat="server" CssClass="TextBox" Width="100px" style="text-align:right" TabIndex="31" OnChange="CalPercentage_Amount(this);"
onkeyup="CalPercentage_Amount(this);"></asp:TextBox>
 <ajax:FilteredTextBoxExtender ID="FilteredTextBoxsdExtender1" runat="server" FilterType="Numbers, Custom"
           ValidChars="." TargetControlID="txtOtherCharges" />
    Rs/-</td>
<td class="LabelextraDuty">
    Loading/Unloading:
</td>
<td align="right">
  <asp:CheckBox runat="server" ID="CHKLoading" Text="AT ACTUAL" onclick="javascript:EnableTextBox();"/>
<asp:TextBox ID="txtPostageAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="30" OnChange="CalPercentage_Amount(this);"
CssClass="TextBox" Width="100px" style="text-align:right" ></asp:TextBox>
 <ajax:FilteredTextBoxExtender ID="FilteredTezdsxtBoxExtender1" runat="server" FilterType="Numbers, Custom"
           ValidChars="." TargetControlID="txtPostageAmt" />
    Rs/-</td>
<td class="LabelextraDuty">
    Ser. Tax :
</td>
<td align="right">
<asp:DropDownList ID="DDLSERVICETAX" runat="server" Width="80px" CssClass="ComboBox" onchange="CalAsPerDDl();">
</asp:DropDownList>
<asp:TextBox ID="txtSerTax" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="27" OnChange="CalPercentage_Amount(this);"
CssClass="TextBox" Width="100px" style="text-align:right"></asp:TextBox>
    Rs/-</td>
</tr>
<tr id="Tr6" runat="server">

<td class="LabelextraDuty" >Inst. Charges :</td>
<td  colspan="2">
<asp:TextBox runat="server" ID="txtInstallationRemark" Width="200px" CssClass="TextBox" Height="20px" TextMode="MultiLine"></asp:TextBox>
  <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtInstallationRemark" WatermarkText="REMARK FOR INSTALLATION CHARGES"
                             WatermarkCssClass="water" />
&nbsp;
 <asp:TextBox ID="txtInstallationCharge" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="25" OnChange="CalPercentage_Amount(this);"
CssClass="TextBox" Width="100px" style="text-align:right" ToolTip="Installation Charges"></asp:TextBox>
Rs-</td>

<td class="LabelextraDuty" >Ser. Tax On Inst. Charges :</td>
<td class="Label" align="right" colspan="2">
<asp:TextBox runat="server" ID="txtInstallationServicetax" Width="80px" CssClass="TextBox" onkeyup="GetAmountOfINSTALLSERVICECHARGE();" style="text-align:right" ></asp:TextBox>
&nbsp;=
 <asp:TextBox ID="txtInstallationServiceAmount" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="25" OnChange="CalPercentage_Amount(this);"
CssClass="TextBoxReadOnly" Width="100px" style="text-align:right" Enabled="false" ToolTip="Installation Charges Service Tax"></asp:TextBox>
Rs/-</td>

</tr>

<tr>
<td class="LabelextraDuty">
    Net Total :
</td>
<td align="right">
<asp:TextBox ID="txtGrandTotal" runat="server" CssClass="TextBoxReadOnly" Width="100px" style="text-align:right" TabIndex="32" Enabled="false"></asp:TextBox>
    Rs/-</td>
</tr>

<tr>
<td class="LabelextraDuty">
    Narration :
</td>
<td align="left" colspan="5">
<asp:TextBox ID="txtNarration" runat="server" CssClass="TextBox" Width="850px" TextMode="MultiLine" TabIndex="32"></asp:TextBox>
</td>
</tr>


<tr>
<td align="center" colspan="6" >
    <table align="center" width="25%">
<tr>
<td>
<asp:Button ID="BtnUpdate" CssClass="buttonLOGIN" runat="server" Text="Update" ValidationGroup="Add" onclick="BtnUpdate_Click" />
<ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
TargetControlID="BtnUpdate">
</ajax:ConfirmButtonExtender>
</td>
<td>
<asp:Button ID="BtnCancel" CssClass="buttonLOGIN" runat="server" Text="Cancel" 
        onclick="BtnCancel_Click" CausesValidation="False" />
</td>
</tr>

</table>
</td>
</tr>

<tr>
<td  align="center" colspan="6">
<div ID="Div5" runat="server" class="ScrollableDiv">
                 <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                     <ContentTemplate>
                        <asp:GridView ID="ReportGrid" runat="server" AutoGenerateColumns="False" 
                             CssClass="mGrid" DataKeyNames="#" AllowPaging="True" PageSize="25"
                           onpageindexchanging="GrdReqPO_PageIndexChanging">
                             <Columns>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                           <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="CPS"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="NOPDF"%>&PrintFlag=<%="NO" %>' target="_blank">
                                         <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                                                ToolTip="Print Purchase Order" TabIndex="29" />
                                         </a>
                                            <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="CPS"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="PDF"%>&PrintFlag=<%="NO" %>' target="_blank">
                                         <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png"
                                                ToolTip="PDF Purchase Order" TabIndex="29" />
                                         </a>
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
                                                                
                                 <asp:BoundField DataField="POStatus" HeaderText="Status">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>                                 
                                 <asp:BoundField DataField="POId" HeaderText="POId">
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

</table>

<table width="100%">
<tr><td>
    <asp:Panel ID="Panel2" runat="server">
   
<asp:UpdatePanel ID="UpdatePanel3" runat="server" >
<ContentTemplate>
<asp:Panel Id="UPPANEL" runat="server" DefaultButton="BTNLOGINFORM">
<div runat="server" id="Panel1" >
<div id="progressBackgroundFilter"></div>
<div runat="server" id="Div1" class="PopUpSample">
<fieldset id="Fieldset3" runat="server" class="FieldSet" style="background-color:ThreeDLighShadow">
<table width="100%" cellspacing="8">
         <tr>
            <td align="center">
                <asp:Label ID="Label1" runat="server" Text="USER NAME" CssClass="Label"></asp:Label>
            </td>
            <td align="right" valign="top"> 
               <asp:TextBox runat="server" ID="Username" CssClass="TextBoxLOGIN" Enabled="false"></asp:TextBox>
            </td>
       </tr> 
       
        <tr>
            <td align="center">
                <asp:Label ID="LBLPASSWORD" runat="server" Text="PASSWORD" CssClass="Label"></asp:Label>
            </td>
            <td align="right" valign="top"> 
               <asp:TextBox runat="server" ID="TXTPASSWORDFORM" CssClass="TextBoxLOGIN" TextMode="Password" onkeypress="return EnterEvent(event)"></asp:TextBox>
            </td>
       </tr>
      <tr>
            <td align="center" colspan="2">
               <asp:Button runat="server" ID="BTNLOGINFORM" Text="Login" CssClass="buttonLOGIN" BorderColor="WhiteSmoke"
                    onclick="BTNLOGINFORM_Click" />
                    <ajax:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server" TargetControlID="BTNLOGINFORM" Corners="All" Radius="8" BorderColor="Gray"></ajax:RoundedCornersExtender>          
            </td>
       </tr> 
       
            </table> 
</fieldset></div></div>
</asp:Panel>
<ajax:AlwaysVisibleControlExtender   
ID="AlwaysVisibleControlExtender1" 
TargetControlID="Panel1"  
VerticalSide="Middle"  
VerticalOffset="5"  
HorizontalSide="Center"  
HorizontalOffset="5"  
ScrollEffectDuration=".1"
runat="server" /></ContentTemplate> </asp:UpdatePanel></asp:Panel>
</td></tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

