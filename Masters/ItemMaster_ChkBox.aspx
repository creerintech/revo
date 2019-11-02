<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="ItemMaster_ChkBox.aspx.cs" Inherits="Masters_ItemMaster" Title="Item Master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
  <input type="hidden" id="hiddenbox" runat="server" value=""/>
  <input type="hidden" id="hiddenbox1" runat="server" value=""/>
  
    <style type="text/css"> 
    .water 
    {
    	color:Gray;
    }
 </style>
 
<script type="text/javascript" language="javascript">

function DeleteEquipFunction()
 { 
        var gridViewCtlId = '<%=GridDetails.ClientID%>';
        var ctlGridViewItems = null;
        var ItemID;
        ctlGridViewItems = document.getElementById(gridViewCtlId);
        ItemID =  ctlGridViewItems.rows[1].cells[2].innerText;        
        alert(ItemID);
        if(ItemID==0)
        {        
        if(confirm("There is no record to delete")==true)
        {
        document.getElementById('<%= hiddenbox.ClientID%>').value="0"; 
        }
        else
        {
         document.getElementById('<%= hiddenbox.ClientID%>').value="0"; 
        }
        }
        else
        {
        if(confirm("Are you sure you want to delete?")==true)
        {
        document.getElementById('<%= hiddenbox.ClientID%>').value="1";
        return true;
        }
        else
        {
         document.getElementById('<%= hiddenbox.ClientID%>').value="0";
         return false;
         }
        }
}


function CalculateNetAmtForGrid()
{

var _GridDetails = document.getElementById('<%= GridDetails.ClientID %>');  

var total=0;
var _TxtENTERQTY = document.getElementById('<%= TxtOpeningStock.ClientID %>');   
var _TxtNETQTY = document.getElementById('<%= TXTNetOpeningStock.ClientID %>');   
var _TxtFlag = document.getElementById('<%= TXTUPDATEVALUE.ClientID %>');   



if (_TxtENTERQTY.value=="" || isNaN(_TxtENTERQTY.value))
{
_TxtENTERQTY.value=0;           
}

if (_TxtNETQTY.value=="" || isNaN(_TxtNETQTY.value))
{
_TxtNETQTY.value=0;           
}

var FSUBTOTAL=0;
var FAMOUNT;
if(parseFloat(_TxtFlag.value)==0)
{
for (var i=1;i<_GridDetails.rows.length-1;i++)
{ 
FAMOUNT=(_GridDetails.rows[i].cells[6].children[0]);
if (FAMOUNT.value=="" || isNaN(FAMOUNT.value))
{
FAMOUNT.value=0;           
}
FSUBTOTAL=parseFloat(FAMOUNT.value)+parseFloat(FSUBTOTAL);
}
}
else
{
for (var i=1;i<_GridDetails.rows.length;i++)
{ 
FAMOUNT=(_GridDetails.rows[i].cells[6].children[0]);
if (FAMOUNT.value=="" || isNaN(FAMOUNT.value))
{
FAMOUNT.value=0;           
}
FSUBTOTAL=parseFloat(FAMOUNT.value)+parseFloat(FSUBTOTAL);
}
}
FSUBTOTAL=parseFloat(_TxtENTERQTY.value)+parseFloat(FSUBTOTAL);
if(parseFloat(FSUBTOTAL) > parseFloat(_TxtNETQTY.value))
{
var MSGX="You Entered Total Opening Stock is "+parseFloat(_TxtNETQTY.value)+ '\n' +"And In Fragmented its Exceed By "+(parseFloat(FSUBTOTAL)-parseFloat(_TxtNETQTY.value));
alert(MSGX);
_TxtENTERQTY.focus();
}

}

function CheckUnitDuplicate() {

    var D1 = document.getElementById('<%= DDLMAINUNIT.ClientID  %>');
    var D1Value = D1.options[D1.selectedIndex].value;
    var D2 = document.getElementById('<%= DDLSUBUNIT.ClientID  %>');
    var D2Value = D2.options[D2.selectedIndex].value;
    var T1 = document.getElementById('<%= TXTSUBUNITQTY.ClientID  %>');
    if (D1Value == D2Value) {
        if (T1.value > 1) {
            T1.value = "0.00";
        }
    }
}

function CheckAllValidations() {
    var D1 = document.getElementById('<%= DDLMAINUNIT.ClientID  %>');
    var D1Value = D1.options[D1.selectedIndex].value;
    var D2 = document.getElementById('<%= DDLSUBUNIT.ClientID  %>');
    var D2Value = D2.options[D2.selectedIndex].value;
    var T1 = document.getElementById('<%= TXTUNITQTY.ClientID  %>');
    var T2 = document.getElementById('<%= TXTSUBUNITQTY.ClientID  %>');
    if (T1.value == "" || isNaN(T1.value)) {
        T1.value = 1;
        document.getElementById('<%= hiddenbox1.ClientID%>').value = "0";
        alert("T1 Please Enter Quantity..!");
        T1.focus();
        return false;
    }
    if (T2.value == "" || isNaN(T2.value)) {
        T2.value = 1;
        document.getElementById('<%= hiddenbox1.ClientID%>').value = "0";
        alert(" T2 Please Enter Quantity..!");
        T2.focus();
        return false;
    }
    if (D1Value == 0) {
        document.getElementById('<%= hiddenbox1.ClientID%>').value = "0";
        alert("Please Select Unit..!");
        D1.focus();
        return false;
    }
    if (D2Value == 0) {
        document.getElementById('<%= hiddenbox1.ClientID%>').value = "0";
        alert("Please Select Unit..!");
        D2.focus();
        return false;
    }
    else {
        document.getElementById('<%= hiddenbox1.ClientID%>').value = "1";
        return true;
    }
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
    <table>
   
    <tr>
    <td>
    Search for Category :
    </td><td>
      <asp:TextBox ID="TxtSearchCategory" runat="server" CssClass="search" ToolTip="Enter The Category"
      Width="292px" AutoPostBack="True" ontextchanged="TxtSearchCategory_TextChanged">
      </asp:TextBox>
       
      <ajax:AutoCompleteExtender ID="AutoCompleteExtender2"   runat="server" 
      TargetControlID="TxtSearchCategory" CompletionInterval="100"                             
      UseContextKey="True" FirstRowSelected ="true" ShowOnlyCurrentWordInCompletionListItem="true"
       ServiceMethod="GetCompletionListCategory" CompletionListCssClass="AutoExtender"
      CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
    </ajax:AutoCompleteExtender>
    </td>
    </tr>
   <tr>
    <td>
    Search for SubCategory :
    </td><td>
      <asp:TextBox ID="TxtSearchSubCategory" runat="server" CssClass="search" ToolTip="Enter The SubCategory"
      Width="292px" AutoPostBack="True" ontextchanged="TxtSearchSubCategory_TextChanged">
      </asp:TextBox></td><td>
      <ajax:AutoCompleteExtender ID="AutoCompleteExtender3"   runat="server" 
      TargetControlID="TxtSearchSubCategory" CompletionInterval="100"                             
      UseContextKey="True" FirstRowSelected ="true" ShowOnlyCurrentWordInCompletionListItem="true"
       ServiceMethod="GetCompletionListSubCategory" CompletionListCssClass="AutoExtender"
      CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
    </ajax:AutoCompleteExtender>
    </td>
    </tr>
    <tr>
    <td>
      Search for Item :
       </td><td>
      <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Item"
      Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged">
      </asp:TextBox>
      <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
      TargetControlID="TxtSearch" CompletionInterval="100"                             
      UseContextKey="True" FirstRowSelected ="true" ShowOnlyCurrentWordInCompletionListItem="true"
       ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender"
      CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
    </ajax:AutoCompleteExtender>
    </td>
    </tr>
    </table>
      
    
    
    
    
    </ContentTemplate>
    </asp:UpdatePanel>
      </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Item Master           
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
  <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
  <ContentTemplate>
   <table width="100%">
      <tr>
      <td>
       <fieldset id="fieldset1" width="100%" runat="server" class="FieldSet">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
        <table width="100%" cellspacing="6">
           <tr>
       <td></td>
        <td></td>
         <td></td>
          <td></td>
       
       </tr>
       
           <tr>
               <td class="Label">
                   Code :</td>
               <td>
                   <asp:TextBox ID="TxtItemCode" runat="server" CssClass="TextBox" MaxLength="50" 
                    Width="140px" ReadOnly="True"></asp:TextBox>
               </td>
               <td class="Label">
                   Barcode :</td>
               <td>
                   <asp:TextBox ID="TxtMfgBarcode" runat="server" CssClass="TextBox" MaxLength="50" 
                    Width="140px"></asp:TextBox>
               </td>
              
           </tr>
           
           <tr>
               <td class="Label">
                   Name :</td>
               <td colspan="3">
                   <asp:TextBox ID="TxtItemName" runat="server" CssClass="TextBox" MaxLength="50" 
                       Width="482px"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="Req1" runat="server" 
                       ControlToValidate="TxtItemName" Display="None" 
                       ErrorMessage="Item Name is Required" SetFocusOnError="True" 
                       ValidationGroup="Add"></asp:RequiredFieldValidator>
                   <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" 
                       TargetControlID="Req1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                   </ajax:ValidatorCalloutExtender>
               </td>
              
           </tr>
           
           <tr>
               <td class="Label">
                   Category :</td>
               <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="ComboBox" Width="142px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="Req_Name" runat="server" 
                    ControlToValidate="ddlCategory" Display="None" 
                    ErrorMessage="Please Select Category" InitialValue="0" SetFocusOnError="True" 
                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" 
                    TargetControlID="Req_Name" WarningIconImageUrl="~/Images/Icon/Warning.png">
                    </ajax:ValidatorCalloutExtender>
               </td>
               <td class="Label">
                   Sub Category :</td>
               <td>
                   <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="ComboBox" 
                       Width="142px">
                   </asp:DropDownList>
               </td>
               
           </tr>
           
           <tr>
               <td class="Label">
                   Delivery Period :</td>
               <td>
                    <asp:TextBox ID="TxtDelivryPeriod" runat="server" CssClass="TextBox" 
                    MaxLength="50" Width="140px"></asp:TextBox>
                    (In Days)
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TxtDelivryPeriod"
                    FilterType="Custom,Numbers"></ajax:FilteredTextBoxExtender>
                   </td>
               <td class="Label">
                   Tax(%) :</td>
               <td>
                    <asp:TextBox ID="TxtTaxPer" runat="server" CssClass="TextBox" MaxLength="50" 
                    Width="140px"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TxtTaxPer"
                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>
               </td>
              
           </tr>
           
           <tr>
               <td class="Label">
                   Min Stock Level :</td>
               <td>
                    <asp:TextBox ID="TxtMinStockLevel" runat="server" CssClass="TextBox" 
                    MaxLength="50" Width="140px"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TxtMinStockLevel"
                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>
               </td>
               <td class="Label">
                   Re Order Level :</td>
               <td>
                    <asp:TextBox ID="TxtReOrdLevel" runat="server" CssClass="TextBox" 
                    MaxLength="50" Width="140px"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="TxtReOrdLevel"
                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>
               </td>
              
           </tr>
           
           <tr>
               <td class="Label">
                   Max Stock Level :</td>
               <td>
                    <asp:TextBox ID="TxtMaxStockLevel" runat="server" CssClass="TextBox" 
                    MaxLength="50" Width="140px"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TxtMaxStockLevel"
                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>
               </td>
               <td class="Label">
                   As On Date :</td>
               <td>
                    <asp:TextBox ID="TxtAsOnDate" runat="server" CssClass="TextBox" MaxLength="50" 
        Width="140px"></asp:TextBox>
       <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" 
       PopupButtonID="Img_AsOnDate" TargetControlID="TxtAsOnDate" />
       <asp:ImageButton ID="Img_AsOnDate" runat="server" CausesValidation="False" 
       CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
               </td>
              
           </tr>
           
           
           <tr>
   <td class="Label">
       Unit :
   </td>
   <td>
   <asp:DropDownList ID="ddlUnit" runat="server" CssClass="ComboBox" Width="142px" 
           AutoPostBack="True" onselectedindexchanged="ddlUnit_SelectedIndexChanged">
   </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RFV5" runat="server" 
        ControlToValidate="ddlUnit" Display="None" 
        ErrorMessage="Unit is Required" SetFocusOnError="True" 
        ValidationGroup="Add" InitialValue="0"></asp:RequiredFieldValidator>
        <ajax:ValidatorCalloutExtender ID="VCE6" runat="server" Enabled="True" 
        TargetControlID="RFV5" WarningIconImageUrl="~/Images/Icon/Warning.png">
        </ajax:ValidatorCalloutExtender>
       <td class="Label">Total Opening Stock :
       <%--<asp:Label id="show" runat="server">
       </asp:Label>--%>
       </td>
       <td>
       <asp:TextBox ID="TXTNetOpeningStock" runat="server" CssClass="TextBox" MaxLength="10" Width="140px"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="TXTNetOpeningStock"
                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender> 
                    
       <asp:CheckBox ID="ChkKitchenAssign" runat="server" CssClass="Display_None"/>
       </td>
   </td>

</tr>
<tr class="Display_None">
<td colspan="4">
<table width="100%">
   <tr>
       <td>
           <fieldset ID="fieldset4" runat="server" class="FieldSet" width="100%">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
               <table width="100%" cellspacing="6">
                  <tr>
                       <td class="Label">
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                           <td class="Label">
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
                       <td class="Label">
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                   <td class="Label">
                       Size :</td>
                   <td>
                         <asp:DropDownList ID="ddlsize" runat="server" CssClass="ComboBox" ValidationGroup="AddGrid1"
           Width="142px">
       </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="ddlsize" Display="None" 
        ErrorMessage="Size Is Required" SetFocusOnError="True" 
        ValidationGroup="AddGrid1" InitialValue="0"></asp:RequiredFieldValidator>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" 
        TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
        </ajax:ValidatorCalloutExtender>
        </td>
                   

            <td colspan="3" align="left">
            <asp:ImageButton ID="ImgAddGridSize" runat="server" CssClass="Imagebutton" 
            Height="16px" ImageUrl="~/Images/Icon/Gridadd.png" onclick="ImgAddGridSize_Click" 
            ToolTip="Add Grid" ValidationGroup="AddGrid1" Width="16px" />
            </td>

            </tr>
                   <tr>
                       <td colspan="5">
                                <div class="scrollableDiv">
                                <asp:GridView ID="Grd_Size" runat="server" AutoGenerateColumns="False" 
                                BackColor="White" BorderStyle="None" BorderWidth="1px" CssClass="mGrid" 
                                Font-Bold="False" ForeColor="Black" GridLines="Horizontal" onrowdeleting="Grd_Size_RowDeleting" >
                                <Columns>
                                <asp:TemplateField HeaderText="#" Visible="False">
                                <ItemTemplate>
                                <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                <ItemTemplate>
                                <asp:ImageButton ID="ImageBtnDelete" runat="server" 
                                CommandArgument='<%#Eval("#") %>' CommandName="Delete" 
                                ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                <HeaderStyle Wrap="False" Width="20px"/>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="20px" />
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="SizeId" DataField="SizeId" >
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Size" DataField="SizeName" >
                                </asp:BoundField>
                                
                                </Columns>
                                </asp:GridView>
                                </div>
                        </td>
                      
                   </tr>
              
               </table>
                </ContentTemplate>
                </asp:UpdatePanel>
           </fieldset>
       </td>
   </tr>
</table>
</td>
</tr>

<tr ID="TR_UnitConversion" runat="server">
<td colspan="4">
<table width="100%">
  <tr>
       <td>
         <div class="ScrollableDiv_FixHeightWidth_N">
          <asp:GridView ID="GrdUnitConversion" runat="server" AutoGenerateColumns="False" 
          BackColor="White" BorderStyle="None" BorderWidth="1px" CssClass="mGrid" 
          Font-Bold="False" ForeColor="Black" GridLines="Horizontal">
          <Columns>
                <asp:TemplateField HeaderText="#" Visible="False">
                  <ItemTemplate>
                      <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="All">
                <HeaderTemplate>
                <asp:CheckBox ID="GrdSelectAllHeader" runat="server"                                                  
                AutoPostBack="true" OnCheckedChanged="GrdSelectAllHeader_CheckedChanged"/>
                </HeaderTemplate>
                <ItemTemplate>
                <asp:CheckBox ID="ChkSelect" runat="server" CssClass="CheckBox"/>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="UnitFactor" DataField="UnitFactor" >
                  <HeaderStyle Wrap="false" />
                  <ItemStyle Wrap="false" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Qty" DataField="Qty">
                <HeaderStyle Wrap="false" />
                <ItemStyle Wrap="false" />
                </asp:BoundField>
                <asp:BoundField HeaderText="UnitConvDtlsId" DataField="UnitConvDtlsId">
                <HeaderStyle Wrap="false" CssClass="Display_None"/>
                <ItemStyle Wrap="false" CssClass="Display_None"/>
                </asp:BoundField>
          </Columns>
      </asp:GridView>
  </div>
  </td>
  </tr>
</table>
</td>
</tr>
<tr ID="Tr_hyl_Hide" runat="server">
<td align="left" colspan="4">
<asp:LinkButton  ID="hyl_Hide" runat="server" CssClass="linkButton" 
        onclick="hyl_Hide_Click">Hide</asp:LinkButton>
</td>
</tr>
   
<tr ID="Tr1" runat="server">
<td align="left" colspan="4">   
<ajax:Accordion ID="Accordion1" runat="server" ContentCssClass="accordionContent1"
HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" FadeTransitions="true"
TransitionDuration="260" FramesPerSecond="20" AutoSize="None"
RequireOpenedPane="false"
SuppressHeaderPostbacks="true"
SelectedIndex="1">
<Panes>
<ajax:AccordionPane ID="AccordionPane1" runat="server">
<Header>
<a class="href" href="#">+ Unit Conversion</a></Header>
<Content>
<div runat="server" class="Display_None" >
<table>
<tr>
<td class="Label"><asp:TextBox runat="server" ID="TXTUNITQTY" Width="80px" CssClass="TextBoxNumeric"></asp:TextBox></td>
<ajax:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="TXTUNITQTY" WatermarkText="Qty" WatermarkCssClass="water" />
<ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="TXTUNITQTY" FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>
<td class="Label"><asp:DropDownList runat="server" ID="DDLMAINUNIT" Width="180px" CssClass="ComboBox" onchange="Javascript:CheckUnitDuplicate();"></asp:DropDownList></td>
<td class="Label"><asp:Label runat="server" ID="LABELEQUAL" Text="&nbsp;&nbsp;=&nbsp;&nbsp;" CssClass="Label_Dynamic"></asp:Label></td>
<td class="Label"><asp:TextBox runat="server" ID="TXTSUBUNITQTY" Width="80px" CssClass="TextBoxNumeric"></asp:TextBox></td>
<ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TXTSUBUNITQTY" WatermarkText="Qty" WatermarkCssClass="water" />
<ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="TXTSUBUNITQTY" FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>
<td class="Label"><asp:DropDownList runat="server" ID="DDLSUBUNIT" Width="180px" CssClass="ComboBox" onchange="Javascript:CheckUnitDuplicate();"></asp:DropDownList></td>
<td class="Label"><asp:TextBox runat="server" ID="TextBox1" Width="100px" CssClass="Display_None"></asp:TextBox></td>
<td><asp:ImageButton ID="IMGBTNADDUNIT" runat="server" CssClass="Imagebutton" Height="19px" ImageUrl="~/Images/Icon/Gridadd.png" ToolTip="Add To Unit Conversion Grid" ValidationGroup="AddUnitGrid" Width="16px" OnClick="IMGBTNADDUNIT_Click"  OnClientClick="javascript:CheckAllValidations();" /></td>
</tr>
</table> 
</div>
<div runat="server" class="Display_None">              
    <asp:GridView ID="GridUnitConversion" runat="server" AutoGenerateColumns="False" 
          BackColor="White" BorderStyle="None" BorderWidth="1px" CssClass="mGrid" 
          Font-Bold="False" ForeColor="Black" GridLines="Horizontal" onrowdeleting="GridUnitConversion_RowDeleting" >                                
    <Columns>
    <asp:TemplateField HeaderText="#" Visible="false">                                        
    <ItemTemplate>
    <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="" >                                        
    <ItemTemplate>
    <asp:ImageButton ID="ImageBtnUnitDelete" runat="server" CommandArgument='<%#Container.DataItemIndex+1 %>' CommandName="Delete" 
         ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete Row" />
         <ajax:ConfirmButtonExtender ID="ConfirmButton" runat="server" ConfirmText="Would You Like To Delete The Unit Conversion Record..!"
         TargetControlID="ImageBtnUnitDelete"></ajax:ConfirmButtonExtender>
    </ItemTemplate>
     <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
     <HeaderStyle Wrap="False" />
     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="5%"/>
    </asp:TemplateField>
      
                                    
    <asp:TemplateField HeaderText="Sr. No.">                        
    <ItemTemplate>
    <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
    Width="7%" />
    </asp:TemplateField>
    <asp:BoundField DataField="MainUnitFactor" HeaderText="Qty" >
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    </asp:BoundField>

    <asp:BoundField DataField="UnitID" HeaderText="UnitID">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"/>
    </asp:BoundField>

    <asp:BoundField DataField="MainUnit" HeaderText="Unit">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    </asp:BoundField>

    <asp:BoundField DataField="SubUnitFactor" HeaderText="Qty">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    </asp:BoundField>

    <asp:BoundField DataField="SubUnitID" HeaderText="SubUnitID">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"/>
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"/>
    </asp:BoundField>

    <asp:BoundField DataField="SubUnit" HeaderText="Unit">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    </asp:BoundField>
    </Columns> 
    <PagerStyle CssClass="pgr" />
    <AlternatingRowStyle CssClass="alt" />                               
    </asp:GridView>
</div>

<div >
<table>
<tr>
<td>
<asp:CheckBoxList runat="server" ID="CHKUNITCONVERSION"  CellSpacing="11" ToolTip="You Can Check Multiple CheckBox For Multiple Unit Conversion.." CssClass="CheckBox" RepeatDirection="Horizontal" RepeatColumns="10" RepeatLayout="Table"></asp:CheckBoxList>
</td>
</tr>
<tr>
<td>
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="UnitGridCalculate" Text="SET FACTOR" CssClass="buttonpayment" OnClick="UnitGridCalculate_Click" ToolTip="Get Grid For Setting Factor Qunatity For Seleted Unit(s)" />
</td>
</tr>
</table> 
</div>

<div >              
<asp:GridView ID="GridUC" runat="server" AutoGenerateColumns="False" 
 BackColor="White" BorderStyle="None" BorderWidth="1px" CssClass="mGrid" 
Font-Bold="False" ForeColor="Black" GridLines="Horizontal"  >                                
<Columns>
<asp:TemplateField HeaderText="#" Visible="false">                                        
<ItemTemplate>
<asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
</ItemTemplate>
</asp:TemplateField>

    
       <asp:TemplateField HeaderText="Sr. No.">                        
    <ItemTemplate>
    <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
    Width="7%" />
    </asp:TemplateField>
                                    
   
    <asp:BoundField DataField="MainUnitQty" HeaderText="Qty" >
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    </asp:BoundField>

    <asp:BoundField DataField="MainUnitID" HeaderText="UnitID">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"/>
    </asp:BoundField>

    <asp:BoundField DataField="MainUnit" HeaderText="Unit">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    </asp:BoundField>

    <asp:TemplateField HeaderText="Qty">
    <ItemTemplate>
    <asp:TextBox ID="SubUnitQty" runat="server"  CssClass="TextBoxNumeric" MaxLength="10" 
    Text='<%# Bind("SubUnitQty") %>' TextMode="SingleLine" Width="100px"></asp:TextBox>
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
    </asp:TemplateField>
                

    <asp:BoundField DataField="SubUnitID" HeaderText="SubUnitID">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"/>
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"/>
    </asp:BoundField>

    <asp:BoundField DataField="SubUnit" HeaderText="Unit">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    </asp:BoundField>
    </Columns> 
    <PagerStyle CssClass="pgr" />
    <AlternatingRowStyle CssClass="alt" />                               
    </asp:GridView>
</div>

</Content>
</ajax:AccordionPane>
</Panes>
</ajax:Accordion>
</td>
</tr>


 <tr>
<td colspan="4">
<table width="100%">
   <tr>
       <td>
           <fieldset ID="fieldset3" runat="server" class="FieldSet" width="100%">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
               <table width="100%" cellspacing="6">
                  <tr>
                       <td class="Label">
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                           <td class="Label">
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
                       <td class="Label">
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                   <td class="Label2">
                       Stock Location :</td>
                   <td>
                         <asp:DropDownList ID="ddlStockLocation" runat="server" CssClass="ComboBox" 
           Width="142px">
       </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RFV3" runat="server" 
        ControlToValidate="ddlStockLocation" Display="None" 
        ErrorMessage="Stock Location Required" SetFocusOnError="True" 
        ValidationGroup="AddGrid" InitialValue="0"></asp:RequiredFieldValidator>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" 
        TargetControlID="RFV3" WarningIconImageUrl="~/Images/Icon/Warning.png">
        </ajax:ValidatorCalloutExtender>
        </td>
                   <td class="Label">
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
                       <td class="Label2">
                        Supplier :</td>
                   <td>
                       <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="ComboBox" 
                               Width="142px" ValidationGroup="AddGrid">
                           </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="RFV2" runat="server" 
                               ControlToValidate="ddlSupplier" Display="None" 
                               ErrorMessage="Supplier Name is Required" SetFocusOnError="True" 
                               ValidationGroup="AddGrid" InitialValue="0"></asp:RequiredFieldValidator>
                           <ajax:ValidatorCalloutExtender ID="VCE1" runat="server" Enabled="True" 
                               TargetControlID="RFV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                           </ajax:ValidatorCalloutExtender>
                           </td>
                       
               </tr>
              
                   <tr>
                       <td class="Label2">
                            Opening Stock :</td>
                       <td>
                            <asp:TextBox ID="TxtOpeningStock" runat="server" CssClass="TextBox"  onblur="CalculateNetAmtForGrid()"
                    MaxLength="10" Width="140px"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="TxtOpeningStock"
                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender> 
                       </td>
                       <td class="Label">
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
                       <td class="Label2" align="right">
                       Purchase Rate :</td>
                       <td>
                           <asp:TextBox ID="TxtPurchaseRate" runat="server" CssClass="TextBox" 
                               MaxLength="50" Width="80px" ></asp:TextBox>
                               &nbsp;&nbsp;&nbsp;
                         
                               <asp:TextBox ID="TXTUPDATEVALUE" runat="server" CssClass="Display_None" 
                               MaxLength="50" Width="10px" ></asp:TextBox>
                       </td>
                   </tr>
                   
                   <tr>
                       <td class="Label2">
                             Description :</td>
                       <td colspan="5">
                          <asp:TextBox ID="txtDescription" runat="server" CssClass="TextBox"  onblur="CalculateNetAmtForGrid()"
                    MaxLength="500" Width="500px"></asp:TextBox>
                      <asp:ImageButton ID="ImgAddGrid" runat="server" CssClass="Imagebutton" 
                               Height="16px" ImageUrl="~/Images/Icon/Gridadd.png" onclick="ImgAddGrid_Click" 
                               ToolTip="Add Grid" ValidationGroup="AddGrid" Width="16px" />
                    </td>
                           <td class="Label">
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
                       <td class="Label">
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                   </tr>
              
               </table>
                </ContentTemplate>
                </asp:UpdatePanel>
           </fieldset>
       </td>
   </tr>
</table>
</td>
</tr>

 <tr>
<td colspan="4">
<asp:UpdatePanel ID="UpdatePanel6" runat="server">
<ContentTemplate>
   <table width="100%">
     <tr>
       <td>
         <div class="ScrollableDiv_FixHeightWidth_N">
          <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" 
          BackColor="White" BorderStyle="None" BorderWidth="1px" CssClass="mGrid" 
          Font-Bold="False" ForeColor="Black" GridLines="Horizontal" 
          onrowcommand="GridDetails_RowCommand" onrowdeleting="GridDetails_RowDeleting">
          <Columns>
              <asp:TemplateField HeaderText="#" Visible="False">
                  <ItemTemplate>
                      <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
                  <ItemTemplate>
                      <asp:ImageButton ID="ImageGridEdit" runat="server" 
                          CommandArgument="<%#((GridViewRow)Container).RowIndex %>" 
                          CommandName="SelectGrid" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                          
                      
                        <asp:ImageButton ID="ImageBtnDelete" runat="server" 
                          CommandArgument='<%#Eval("#") %>' CommandName="Delete" 
                          ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                          <ajax:ConfirmButtonExtender ID="ConfirmButton" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                        TargetControlID="ImageBtnDelete">
                                    </ajax:ConfirmButtonExtender>
                  </ItemTemplate>
                  <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  <HeaderStyle Wrap="False" />
                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
              </asp:TemplateField>
              
                <asp:BoundField HeaderText="LocationId" DataField="LocationId" >
                  <HeaderStyle CssClass="Display_None" />
                  <ItemStyle CssClass="Display_None" />
              </asp:BoundField>
              
               <asp:TemplateField HeaderText="Site">
                <ItemTemplate>
                <asp:TextBox ID="GrdtxtLocation" runat="server"  CssClass="TextBoxGrid" MaxLength="10" 
                Text='<%# Bind("Location") %>' TextMode="SingleLine" Width="120px" Enabled="false" ></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
                Wrap="False" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
                Wrap="False" />
                </asp:TemplateField>
                
                
              <asp:BoundField HeaderText="SuplierName" DataField="SuplierName">
                  <HeaderStyle Wrap="false" />
                  <ItemStyle Wrap="false" />
              </asp:BoundField>
              <asp:BoundField HeaderText="SupplierId" DataField="SupplierId" >
                  <HeaderStyle CssClass="Display_None" />
                  <ItemStyle CssClass="Display_None" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Last Purchase Rate" DataField="PurchaseRate">
                  <HeaderStyle Wrap="false" CssClass="Display_None" />
                  <ItemStyle Wrap="false" CssClass="Display_None" />
              </asp:BoundField>

                <asp:TemplateField HeaderText="Opening Stock">
                <ItemTemplate>
                <asp:TextBox ID="GrdtxtOpeningStock" runat="server"  CssClass="TextBoxGrid" MaxLength="10" 
                Text='<%# Bind("OpeningStock") %>' TextMode="SingleLine" Width="50px" Enabled="false"></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
                Wrap="False" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
                Wrap="False" />
                </asp:TemplateField>
                
                   <asp:TemplateField HeaderText="Purchase Rate">
                <ItemTemplate>
                <asp:TextBox ID="GrdtxtPurchaseRate" runat="server"  CssClass="TextBoxGrid" MaxLength="10" 
                Text='<%# Bind("PurchaseRate") %>' TextMode="SingleLine" Width="50px" Enabled="false"></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
                Wrap="False" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
                Wrap="False" />
                </asp:TemplateField>
                
                 <asp:BoundField HeaderText="Item Description" DataField="ItemDesc">
                  <HeaderStyle Wrap="false" />
                  <ItemStyle Wrap="false" />
              </asp:BoundField>


          </Columns>
      </asp:GridView>
  </div>
  </td>
  </tr>
  </table>
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
      
 <tr>
        <td></td>
        <td></td>
        <td></td>
        <td></td>

        </tr>
       </table>
       </ContentTemplate>
       </asp:UpdatePanel>
       </fieldset>
        </td>
        </tr>
  
     
      <tr>
          <td>
              <fieldset ID="fieldset2" runat="server" class="FieldSet" width="100%">
               <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                 <ContentTemplate>
                  <table width="100%">
                      <tr>
                          <td align="center">
                              <table align="center" width="25%">
                                  <tr>
                                      <td>
                                          <asp:Button ID="BtnUpdate" runat="server" CssClass="button" Text="Update" 
                                              ValidationGroup="Add" onclick="BtnUpdate_Click" />
                                          <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                                              ConfirmText="Would You Like To Update The Record ?" TargetControlID="BtnUpdate">
                                          </ajax:ConfirmButtonExtender>
                                      </td>
                                      <td>
                                          <asp:Button ID="BtnSave" runat="server" CssClass="button" Text="Save" 
                                              ValidationGroup="Add" onclick="BtnSave_Click" />
                                      </td>
                                      <td>
                                          <asp:Button ID="BtnDelete" runat="server" CssClass="button" Text="Delete" 
                                              ValidationGroup="Add" onclick="BtnDelete_Click" />
                                          <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" 
                                          ConfirmText="Would You Like To Delete The Record ?" TargetControlID="BtnDelete">
                                          </ajax:ConfirmButtonExtender>
                                      </td>
                                      <td>
                                          <asp:Button ID="BtnCancel" runat="server" CausesValidation="false" 
                                              CssClass="button" onclick="BtnCancel_Click" Text="Cancel" />
                                      </td>
                                  </tr>
                              </table>
                          </td>
                      </tr>
                  </table>
                  </ContentTemplate>
                  </asp:UpdatePanel>
              </fieldset>
          </td>
      </tr>
        </table>
  </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ReportTitle" Runat="Server">
    Item List           
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Report" Runat="Server">                 
 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate >
    <div class="ScrollableDiv_FixHeightWidthForRepeater">
    <ul id="subnav">
            <%--Ul Li Problem Solved repeater--%>
            <asp:Repeater ID="GrdReport" runat="server" 
                onitemcommand="GrdReport_ItemCommand">
                   
            <ItemTemplate>
            <li id="Menuitem" runat="server" >                              
              <asp:LinkButton ID="lbtn_List" CssClass="linkButton" CausesValidation="false"
                CommandName="Select" CommandArgument='<%# Eval("#") %>' runat="server"
                 Text='<%# Eval("Name") %>'>
              </asp:LinkButton>
            </li>
            </ItemTemplate>    
           

            </asp:Repeater>
             <asp:Repeater ID="rptPages" runat="server" 
                onitemcommand="rptPages_ItemCommand" >
                <HeaderTemplate>
                <asp:LinkButton ID="btnPrev" runat="server" Text="Prev" CommandName="Prev"></asp:LinkButton>
                </HeaderTemplate>
               <ItemTemplate>
                <asp:LinkButton ID="btnPage" ForeColor="Red"
                CssClass="RepeaterPagging"
                 CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                 runat="server"><%# Container.DataItem %>
                </asp:LinkButton>
               
            </ItemTemplate>
            <FooterTemplate>
             <asp:LinkButton ID="btnNext" runat="server" Text="Next" CommandName="Next"></asp:LinkButton>
             </FooterTemplate>
                </asp:Repeater>
    </ul>
       </div>    
       </ContentTemplate>
 </asp:UpdatePanel>                  
</asp:Content>

