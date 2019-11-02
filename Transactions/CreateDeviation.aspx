<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="CreateDeviation.aspx.cs" Inherits="Transactions_CreateDeviation" Title="Deviation Management" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
<script type="text/javascript" language="javascript"> 
        function RateOption()
        {   
            var RDO_FromTo=document.getElementById('<%= RDO_FromTo.ClientID %>');            
            var RDO_GreaterThenEqualTo=document.getElementById('<%= RDO_GreaterThenEqualTo.ClientID %>');            
            var RDO_LessThenEqualTo=document.getElementById('<%= RDO_LessThenEqualTo.ClientID %>');            
            var RDO_EqualTo=document.getElementById('<%= RDO_EqualTo.ClientID %>');     
            
            var _txtFromRate = document.getElementById('<%= txtFromRate.ClientID %>');  
            
            var t1=0;
            if(_txtFromRate.value != "") t1=_txtFromRate.value;             
            
            if(RDO_FromTo.checked)
            {     
                document.getElementById('<%= txtFromRate.ClientID %>').removeAttribute('readOnly'); //remove ReadOnly Property                
                document.getElementById('<%= txtToRate.ClientID %>').removeAttribute('readOnly');  //remove ReadOnly Property 
                document.getElementById('<%= txtGreaterRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtLessRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtEqualRate.ClientID %>').setAttribute('readonly',true); 
                
                document.getElementById('<%= txtFromRate.ClientID %>').value='0.00';
                document.getElementById('<%= txtToRate.ClientID %>').value='0.00';
                document.getElementById('<%= txtGreaterRate.ClientID %>').value='';
                document.getElementById('<%= txtLessRate.ClientID %>').value='';
                document.getElementById('<%= txtEqualRate.ClientID %>').value='';
            }  
            else if(RDO_GreaterThenEqualTo.checked)
            {            
                document.getElementById('<%= txtFromRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtToRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtGreaterRate.ClientID %>').removeAttribute('readOnly'); //remove ReadOnly Property
                document.getElementById('<%= txtLessRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtEqualRate.ClientID %>').setAttribute('readonly',true); 
                
                document.getElementById('<%= txtFromRate.ClientID %>').value='';
                document.getElementById('<%= txtToRate.ClientID %>').value='';
                document.getElementById('<%= txtGreaterRate.ClientID %>').value='0.00';
                document.getElementById('<%= txtLessRate.ClientID %>').value='';
                document.getElementById('<%= txtEqualRate.ClientID %>').value='';
            }
            else if(RDO_LessThenEqualTo.checked)
            {            
                document.getElementById('<%= txtFromRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtToRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtGreaterRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtLessRate.ClientID %>').removeAttribute('readOnly'); //remove ReadOnly Property
                document.getElementById('<%= txtEqualRate.ClientID %>').setAttribute('readonly',true);
                
                document.getElementById('<%= txtFromRate.ClientID %>').value='';
                document.getElementById('<%= txtToRate.ClientID %>').value='';
                document.getElementById('<%= txtGreaterRate.ClientID %>').value='';
                document.getElementById('<%= txtLessRate.ClientID %>').value='0.00';
                document.getElementById('<%= txtEqualRate.ClientID %>').value=''; 
            }
            else if(RDO_EqualTo.checked)
            {            
                document.getElementById('<%= txtFromRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtToRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtGreaterRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtLessRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtEqualRate.ClientID %>').removeAttribute('readOnly'); //remove ReadOnly Property
                
                document.getElementById('<%= txtFromRate.ClientID %>').value='';
                document.getElementById('<%= txtToRate.ClientID %>').value='';
                document.getElementById('<%= txtGreaterRate.ClientID %>').value='';
                document.getElementById('<%= txtLessRate.ClientID %>').value='';
                document.getElementById('<%= txtEqualRate.ClientID %>').value='0.00';
            }          
        }
        
        
        
function CalculateGrid(objGrid)
{

var _GridDetails = document.getElementById('<%= DeviationGrid.ClientID %>');  

var rowIndex=objGrid.offsetParent.parentNode.rowIndex;

var SYSQTY=(_GridDetails.rows[rowIndex].cells[9].children[0]);
var SYSAMT=(_GridDetails.rows[rowIndex].cells[10].children[0]);
var PHYQTY=(_GridDetails.rows[rowIndex].cells[11].children[0]);
var PHYRATE=(_GridDetails.rows[rowIndex].cells[12].children[0]);
var PHYAMT=(_GridDetails.rows[rowIndex].cells[13].children[0]);
var DEVQTY=(_GridDetails.rows[rowIndex].cells[14].children[0]);
var DEVAMT=(_GridDetails.rows[rowIndex].cells[15].children[0]);
var total=0;

var _TxtSYSAMT = document.getElementById('<%= TxtSysamount.ClientID %>');   
var _TxtDEVAMT = document.getElementById('<%= TxtTotDeviationAmount.ClientID %>');   
var _TxtPHYAMT = document.getElementById('<%= TxtPhyamount.ClientID %>');   
var _TxtFINALAMT = document.getElementById('<%= TxtTotDeviationAmount.ClientID %>');  


if (SYSQTY.value=="" || isNaN(SYSQTY.value))
{
SYSQTY.value=0;           
}

if (SYSAMT.value=="" || isNaN(SYSAMT.value))
{
SYSAMT.value=0;           
}

if (PHYQTY.value=="" || isNaN(PHYQTY.value))
{
PHYQTY.value=0;           
}

if (PHYRATE.value=="" || isNaN(PHYRATE.value))
{
PHYRATE.value=0;           
}

if (PHYAMT.value=="" || isNaN(PHYAMT.value))
{
PHYAMT.value=0;           
}

if (DEVQTY.value=="" || isNaN(DEVQTY.value))
{
DEVQTY.value=0;           
}

if (DEVAMT.value=="" || isNaN(DEVAMT.value))
{
DEVAMT.value=0;           
}

PHYAMT.value=parseFloat(parseFloat(PHYQTY.value)*parseFloat(PHYRATE.value)).toFixed(2);
DEVQTY.value=parseFloat(parseFloat(SYSQTY.value)-parseFloat(PHYQTY.value)).toFixed(2);
DEVAMT.value=parseFloat((parseFloat(SYSQTY.value)-parseFloat(PHYQTY.value))*parseFloat(PHYRATE.value)).toFixed(2);

var FSYSTOTAL=0;
var FPHYTOTAL=0;
var FDEVTOTAL=0;

var SYSTOTAL;
var PHYTOTAL;
var DEVTOTAL;
for (var i=1;i<_GridDetails.rows.length;i++)
{ 
SYSTOTAL=(_GridDetails.rows[i].cells[10].children[0]);
PHYTOTAL=(_GridDetails.rows[i].cells[13].children[0]);
DEVTOTAL=(_GridDetails.rows[i].cells[15].children[0]);

FSYSTOTAL=(parseFloat(FSYSTOTAL)+parseFloat(SYSTOTAL.value));
FPHYTOTAL=(parseFloat(FPHYTOTAL)+parseFloat(PHYTOTAL.value));
FDEVTOTAL=(parseFloat(FDEVTOTAL)+parseFloat(DEVTOTAL.value));

}
_TxtSYSAMT.value=parseFloat(FSYSTOTAL);
_TxtDEVAMT.value=parseFloat(FDEVTOTAL);
_TxtPHYAMT.value=parseFloat(FPHYTOTAL);
_TxtFINALAMT.value=parseFloat(FDEVTOTAL);

}

function TotalForUpdates() {
    var _GridDetails = document.getElementById('<%= DeviationGrid.ClientID %>');  
    var _TxtSYSAMT = document.getElementById('<%= TxtSysamount.ClientID %>');
    var _TxtDEVAMT = document.getElementById('<%= TxtTotDeviationAmount.ClientID %>');
    var _TxtPHYAMT = document.getElementById('<%= TxtPhyamount.ClientID %>');
    var _TxtFINALAMT = document.getElementById('<%= TxtTotDeviationAmount.ClientID %>');  

    var FSYSTOTAL = 0;
    var FPHYTOTAL = 0;
    var FDEVTOTAL = 0;

    var SYSTOTAL;
    var PHYTOTAL;
    var DEVTOTAL;
    for (var i = 1; i < _GridDetails.rows.length; i++) {
        SYSTOTAL = (_GridDetails.rows[i].cells[10].children[0]);
        PHYTOTAL = (_GridDetails.rows[i].cells[13].children[0]);
        DEVTOTAL = (_GridDetails.rows[i].cells[15].children[0]);

        FSYSTOTAL = (parseFloat(FSYSTOTAL) + parseFloat(SYSTOTAL.value));
        FPHYTOTAL = (parseFloat(FPHYTOTAL) + parseFloat(PHYTOTAL.value));
        FDEVTOTAL = (parseFloat(FDEVTOTAL) + parseFloat(DEVTOTAL.value));

    }
    _TxtSYSAMT.value = parseFloat(FSYSTOTAL);
    _TxtDEVAMT.value = parseFloat(FDEVTOTAL);
    _TxtPHYAMT.value = parseFloat(FPHYTOTAL);
    _TxtFINALAMT.value = parseFloat(FDEVTOTAL);
}

</script>
        
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
    Search for Deviation File : 
    <asp:TextBox ID="TxtSearch" runat="server" 
CssClass="search" ToolTip="Enter The Text"
Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged"></asp:TextBox>
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
    Create Deviation 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<table width="100%" cellspacing="5">
<tr>
<td>
</td>
<td align="left" >
</td>
<td class="Label" >
</td>
<td align="left">
</td>            
<td align="right">
</td>            
<td align="right">
</td>            
<td align="right">
    &nbsp;</td>
<td align="right">
    &nbsp;</td>
</tr>
<tr>
<td class="Label">
<asp:CheckBox ID="ChkFrmDate" runat="server" AutoPostBack="true"
CssClass="CheckBox"  Text=" From :" TabIndex="1" />
</td>
<td  colspan="3">
<table>
<tr>
<td>
<asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Width="90px" 
ReadOnly="false"></asp:TextBox>
<asp:ImageButton ID="ImageButton212" runat="server" CausesValidation="False" 
CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="1" />

<ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" 
Format="dd-MMM-yyyy" PopupButtonID="ImageButton212" TargetControlID="txtFromDate" />

</td>
<td class="Label">
    &nbsp;&nbsp;&nbsp;To :</td>                
<td>
<asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" 
Width="90px" ReadOnly="false" ></asp:TextBox>
<ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" TargetControlID="txtToDate" />
<asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" 
TabIndex="2"  /> 
</td> 
</tr> 
</table>                    
</td>
<td class="Label" >
    Category :</td>
<td >

<%--<asp:DropDownList ID="ddlCategory" runat="server" CssClass="ComboBox" TabIndex="2"
Width="152px">
</asp:DropDownList>--%>
<ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown" 
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="160px" CssClass="CustomComboBoxStyle" >
</ajax:ComboBox>
</td>
<td class="Label">
    Location :</td>
<td>
<%--<asp:DropDownList ID="ddlLocation" runat="server" CssClass="ComboBox" TabIndex="3"
Width="152px">
</asp:DropDownList>--%>
<ajax:ComboBox ID="ddlLocation" runat="server" DropDownStyle="DropDown" 
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="160px" CssClass="CustomComboBoxStyle" >
</ajax:ComboBox>
</td>
</tr>
<tr>
<td class="Label">
    Item :</td>
<td colspan="3">
<ajax:ComboBox ID="ddlItemName" runat="server" DropDownStyle="DropDown" Visible="false"
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="250px" CssClass="CustomComboBoxStyle" >
</ajax:ComboBox>


<%--THIS START HERE--%>
<asp:UpdatePanel ID="UpdatePanel4" runat="server">
<ContentTemplate>
<asp:TextBox ID="TxtItemName" runat="server" 
CssClass="search_List" Width="292px" AutoPostBack="True" ontextchanged="TxtItemName_TextChanged" ></asp:TextBox>

<ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemName"   runat="server" 
TargetControlID="TxtItemName"    CompletionInterval="100"                               
UseContextKey="True" FirstRowSelected ="true" 
CompletionSetCount="20" 
ShowOnlyCurrentWordInCompletionListItem="true"  
ServiceMethod="GetCompletionItemNameList"
CompletionListCssClass="AutoExtender"
CompletionListItemCssClass="AutoExtenderList"
CompletionListHighlightedItemCssClass="AutoExtenderHighlight"                         
></ajax:AutoCompleteExtender> 

<ajax:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="TxtItemName" 
WatermarkText="Type Item Name" WatermarkCssClass="water" />
</ContentTemplate>
</asp:UpdatePanel >
<%--THIS END HERE--%>

</td>
<td class="Label">
<asp:RadioButton ID="RDO_FromTo" runat="server" GroupName="Rate" 
TextAlign="Left" OnClick="javascript:RateOption();" />
    If Rate :</td>
<td>
    &nbsp;&nbsp;&nbsp;&nbsp;
<asp:TextBox ID="txtFromRate" runat="server" CssClass="TextBox" Width="50px"></asp:TextBox>

    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; To&nbsp;&nbsp;&nbsp;&nbsp;
<asp:TextBox ID="txtToRate" runat="server" CssClass="TextBox" Width="50px"></asp:TextBox>

</td>
<td class="Label">
    Unit :</td>
<td>
<%--<asp:DropDownList ID="ddlUnit" runat="server" CssClass="ComboBox" TabIndex="3" 
Width="152px">
</asp:DropDownList>--%>
<ajax:ComboBox ID="ddlUnit" runat="server" DropDownStyle="DropDown" 
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="160px" CssClass="CustomComboBoxStyle" >
</ajax:ComboBox>
</td>
</tr>
<tr>
<td class="Label">
    &nbsp;</td>
<td colspan="3">
    &nbsp;</td>
<td class="Label">
<asp:RadioButton ID="RDO_GreaterThenEqualTo" runat="server" GroupName="Rate" 
TextAlign="Left" OnClick="javascript:RateOption();"/>
    If Rate :</td>
<td>
    &gt;=
<asp:TextBox ID="txtGreaterRate" runat="server" CssClass="TextBox" Width="50px"></asp:TextBox>

    &nbsp;</td>
<td class="Label">
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td class="Label">
    &nbsp;</td>
<td colspan="3">
    &nbsp;</td>
<td class="Label">
<asp:RadioButton ID="RDO_LessThenEqualTo" runat="server" GroupName="Rate" 
TextAlign="Left" OnClick="javascript:RateOption();"/>
    If Rate :</td>
<td>
    &lt;=
<asp:TextBox ID="txtLessRate" runat="server" CssClass="TextBox" Width="50px"></asp:TextBox>
    &nbsp;
</td>
<td class="Label">
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td class="Label">
    &nbsp;</td>
<td colspan="3">
    &nbsp;</td>
<td class="Label">
<asp:RadioButton ID="RDO_EqualTo" runat="server" GroupName="Rate" 
TextAlign="Left" OnClick="javascript:RateOption();"/>
    If Rate :</td>
<td>
    &nbsp; =&nbsp;<asp:TextBox ID="txtEqualRate" runat="server" CssClass="TextBox" Width="50px"></asp:TextBox>
    &nbsp;
</td>
<td class="Label">
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>           
<td colspan="7" align="left">
<asp:Label ID="lblDevationPeriod" runat="server" Text="Deviation From : " CssClass="Label" Font-Bold="true"></asp:Label>
</td>
<td align="left" colspan="1">
    &nbsp;<asp:Button ID="BtnShow" runat="server" CssClass="button" 
TabIndex="4" Text="Show" ValidationGroup="Add" 
ToolTip="Pull Record" onclick="BtnShow_Click" />   
</td>
</tr>
</table>
<table width="100%" cellspacing="5">
<tr>
        <td align="center">
        <fieldset id="Fieldset1"  class="FieldSet" runat="server" style="width: 100%">
            <legend id="Legend1" class="legend" runat="server">  Stock Details</legend>
            <%--<table width="100%" class="headextragrid">
                     <tr>
                     <td class="Label4"> 
                     Sr.No.
                     </td>
                     <td class="Label4"> 
                     Category
                     </td>
                     <td class="Label4"> 
                     Code
                     </td>
                     <td class="Label4"> 
                     Product
                     </td>
                     <td class="Label4"> 
                     Unit
                     </td>
                     <td class="Label4"> 
                     Rate
                     </td>
                     <td class="Label4"> 
                     Location
                     </td>
                     <td class="Label4"> 
                     System Closing
                     </td>
                     <td class="Label4"> 
                     System Amount
                     </td>
                     <td class="Label4"> 
                     Physical Closing
                     </td>
                     <td class="Label4"> 
                     Physical Rate
                     </td>
                     <td class="Label4"> 
                     Physical Amount
                     </td>
                     <td class="Label4"> 
                     Deviation
                     </td>
                     <td class="Label4"> 
                     Deviation Amount
                     </td>
                     </tr>
                     </table>--%>
            <div ID="Div1" runat="server" class="ScrollableDiv_FixHeightWidth5">
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true">
                     <ContentTemplate>
                     
                        <asp:GridView ID="DeviationGrid" runat="server" AutoGenerateColumns="False" 
                             CssClass="mGrid" DataKeyNames="#" RowStyle-VerticalAlign="Bottom"
                             onrowdatabound="DeviationGrid_RowDataBound" >
                            <Columns>
                            <asp:TemplateField HeaderText="Sr.No.">                        
                            <ItemTemplate>
                            <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                            Width="5px" />
                            </asp:TemplateField>          
                                 <asp:TemplateField HeaderText="#" Visible="False">
                                     <ItemTemplate>
                                         <asp:Label ID="LblProcessId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 
                                 <asp:BoundField DataField="Category" HeaderText="Category">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Code" HeaderText="Code">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Product" HeaderText="Product">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>  
                                 <asp:BoundField DataField="UnitID" HeaderText="Unit">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                 </asp:BoundField>                               
                                 <asp:BoundField DataField="Unit" HeaderText="Unit">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="MRP" HeaderText="Rate">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                      <asp:BoundField DataField="LocationID" HeaderText="Unit" >
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                 </asp:BoundField> 
                                 <asp:BoundField DataField="Location" HeaderText="Location">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 
                                    <asp:TemplateField HeaderText="System Closing"  >
                                <ItemTemplate>
                                <asp:TextBox ID="GrtxtSysClosing" runat="server" CssClass="TextBoxGrid" Text='<%# Bind("Closing") %>' 
                                TabIndex="6" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle Width="90px" />
                                <HeaderStyle HorizontalAlign="Center" Width="110px" Wrap="false"/>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" Wrap="false"/>
                                </asp:TemplateField>     
                                
                                
                               <%--  <asp:BoundField DataField="Closing" HeaderText="System Closing">
                                     <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>  --%>
                                 
                                 <asp:TemplateField HeaderText="System Amount"  >
                                <ItemTemplate>
                                <asp:TextBox ID="GrtxtSysAmount" runat="server" CssClass="TextBoxGrid" Text='<%# Bind("SystemAmount") %>' 
                                TabIndex="6"   Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle Width="90px" />
                                <HeaderStyle HorizontalAlign="Center" Width="110px" Wrap="false"/>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" Wrap="false"/>
                                </asp:TemplateField>     
                                                                
                          <%--       <asp:BoundField DataField="SystemAmount" HeaderText="System Amount">
                                     <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>  --%>
                                <asp:TemplateField HeaderText="Physical Closing"  >
                                <ItemTemplate>
                                <asp:TextBox ID="GrtxtPhyClosing" runat="server" CssClass="TextBoxNumeric" Text='<%# Bind("PhyClosing") %>' 
                                TabIndex="6" onkeyup="CalculateGrid(this);"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle Width="90px" />
                                <HeaderStyle HorizontalAlign="Center" Width="110px" Wrap="false"/>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" Wrap="false"/>
                                </asp:TemplateField>  
                                   
                                <asp:TemplateField HeaderText="Deviation" >
                                <ItemTemplate>
                                 <asp:TextBox ID="GrtxtDeviation" runat="server" CssClass="TextBoxGrid" Text='<%# Bind("Deviation") %>' 
                                TabIndex="6"  Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" CssClass="Display_None"/>
                                <HeaderStyle HorizontalAlign="Right" CssClass="Display_None"/>
                                </asp:TemplateField>                                                          
                                <asp:TemplateField HeaderText="Deviation Amount" >
                                <ItemTemplate>
                                   <asp:TextBox ID="GrtxtDeviatoinAmount" runat="server" CssClass="TextBoxGrid" Text='<%# Bind("DeviationAmount") %>' 
                                TabIndex="6"   Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" CssClass="Display_None"/>
                                <HeaderStyle HorizontalAlign="Right" CssClass="Display_None"/>
                                </asp:TemplateField>  
                                
                                   <asp:TemplateField HeaderText="Physical Rate"  >
                                <ItemTemplate>
                                <asp:TextBox ID="GrtxtPhyRate" runat="server" CssClass="TextBoxNumeric" Text='<%# Bind("PhyAmount") %>' 
                                TabIndex="6" onkeyup="CalculateGrid(this);"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle Width="90px" />
                                <HeaderStyle HorizontalAlign="Center" Width="110px" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" Width="90px"/>
                                </asp:TemplateField>     
                                         
                                 <asp:TemplateField HeaderText="Physical Amount" >
                                <ItemTemplate>
                                 <asp:TextBox ID="GrtxtPhyAmount" runat="server" CssClass="TextBoxGrid" Text='<%# Bind("PhyDeviationAmt") %>' 
                                TabIndex="6"  Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Wrap="false"/>
                                <HeaderStyle HorizontalAlign="Right" Wrap="false"/>
                                </asp:TemplateField> 
                                        
                                         
                                <asp:TemplateField HeaderText="Deviation" >
                                <ItemTemplate>
                                  <asp:TextBox ID="GrtxtDeviationQty" runat="server" CssClass="TextBoxGrid" Text='<%# Bind("PhyDeviationQty") %>' 
                                TabIndex="6"  Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"/>
                                <HeaderStyle HorizontalAlign="Right"/>
                                </asp:TemplateField> 
                                 
                                <asp:TemplateField HeaderText="Deviation Amount" >
                                <ItemTemplate>
                                  <asp:TextBox ID="GrtxtDeviationAmount" runat="server" CssClass="TextBoxGrid" Text='<%# Bind("PhyDeviationAmount") %>' 
                                TabIndex="6"  Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"/>
                                <HeaderStyle HorizontalAlign="Right" Wrap="false"/>
                                </asp:TemplateField>  
                                         
                                         
                                         
                             </Columns>
                             <%--<HeaderStyle CssClass="header"Height="20px" />--%>
                         </asp:GridView>
                     </ContentTemplate>
                 </asp:UpdatePanel>
             </div>
         </fieldset>
        </td>
     </tr>
     <tr>
     <td align="right">
     <table width="80%">
     <tr>
     <td class="Label_Dynamic">
         System Amount :
     </td>
     <td align="left">
     <asp:TextBox ID="TxtSysamount" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
     </td>   <td class="Label_Dynamic">
          Physical Amount :
     </td>
     <td align="left">
     <asp:TextBox ID="TxtPhyamount" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
     </td>
      <td class="Label_Dynamic">
         Deviation Amount:
     </td>
     <td align="left">
     <asp:TextBox ID="TxtTotDeviationAmount" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
     </td>
   
   
     </tr>
     </table>
     </td>
     </tr>
     <tr>
     <td></td>
     </tr>
     <tr>
     <td align="center">
     <table align="center" width="25%">
<tr>
<td>
<asp:Button ID="BtnUpdate" CssClass="button" runat="server" Text="Update" 
CausesValidation="false" onclick="BtnUpdate_Click" TabIndex="7" />
<ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
TargetControlID="BtnUpdate">
</ajax:ConfirmButtonExtender>
</td>
<td>
<asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" 
ValidationGroup="Add" onclick="BtnSave_Click" TabIndex="7" />
</td>
<td>
<asp:Button ID="Button1" CssClass="button" runat="server" Text="Cancel" 
CausesValidation="False" onclick="Button1_Click" TabIndex="8"  /></td>


</tr>
</table>
     </td>
     </tr>
<tr>
<td align="center" >
<div ID="Div5" runat="server" class="ScrollableDiv" width="100%">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
 <ContentTemplate>
    
    <asp:GridView ID="ReportGridDeviation" runat="server" AutoGenerateColumns="False" 
         CssClass="mGrid" DataKeyNames="#" AllowPaging="True" PageSize="30"
         onrowcommand="ReportGrid_RowCommand" onrowdeleting="ReportGrid_RowDeleting" >
         <Columns>
             <asp:TemplateField HeaderText="#" Visible="False">
                 <ItemTemplate>
                  <asp:Label ID="lblDeviationID" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField>
                 <ItemTemplate>                                                             
                     <asp:ImageButton ID="ImageGridEdit" runat="server" 
                         CommandArgument='<%# Eval("#") %>' 
                         CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                     <asp:ImageButton ID="ImgBtnDelete" runat="server" 
                         CommandArgument='<%# Eval("#") %>' 
                         CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />                                             
                     <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                         ConfirmText="Would You Like To Delete The Record..!" 
                         TargetControlID="ImgBtnDelete">
                     </ajax:ConfirmButtonExtender>
                     
                     <a href="../PrintReport/DeviationPrint.aspx?ID=<%# Eval("#")%>" target="_blank">
                     <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                            ToolTip="Print Deviation Details" TabIndex="29" />
                     </a>
                 </ItemTemplate>
                 <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                 <HeaderStyle Width="20px" />
                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" 
                     Wrap="false" />
             </asp:TemplateField>
             <asp:BoundField DataField="DeviationNo" HeaderText="Deviation No">
                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
             </asp:BoundField>                                 
             <asp:BoundField DataField="DeviationDate" HeaderText="Date">
                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
             </asp:BoundField>
         </Columns>
     </asp:GridView>
 </ContentTemplate>
</asp:UpdatePanel>
</div>
</td>
</tr>

</table>
</ContentTemplate>
   <Triggers>
             <asp:PostBackTrigger ControlID ="BtnSave" />
             </Triggers>
</asp:UpdatePanel>
</asp:Content>

