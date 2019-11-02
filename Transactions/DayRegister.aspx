<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="DayRegister.aspx.cs" Inherits="Transactions_DayRegister" Title="Daily Inward/Outward Register" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
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
    Search For Inward:
<asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged">
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
    Daily Inward/Outward Register 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UP1" runat="server">
<ContentTemplate>
<div>
<table width="100%" cellspacing="8">
<tr width="100%"><td >
<fieldset id="fieldset1" width="100%" runat="server" class="FieldSet">
<table width="100%" cellspacing="8">
<tr>
<td class="Label">
<asp:Label ID="lblDInwardNo" runat="server" Text="Inward No :"></asp:Label>
</td>
<td ><asp:Label ID="lblDInwardNoShow" runat="server" Text="MAT/INW/DAI-0001" Font-Bold="true"></asp:Label></td>
<td class="Label">Inward By :</td>
<td><asp:Label ID="lblEmployee" runat="server" Font-Bold="true" Text="Employee"></asp:Label></td>
<td class="Label">Date :</td>
<td>
<asp:TextBox ID="TxtDate" CssClass="TextBox" runat="server" Width="100px"></asp:TextBox>
<ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MMM/yyyy" 
PopupButtonID="Img_IssueDate" TargetControlID="TxtDate" />
<asp:ImageButton ID="Img_IssueDate" runat="server" CausesValidation="False" 
CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
</td>
</tr>
<tr><td colspan="6"><hr  width="100%"/></td></tr>
<tr>
<td class="Label">
    Category :
</td>
<td>
<asp:DropDownList ID="ddlCategory" runat="server" Width="162px" CssClass="ComboBox" 
        onselectedindexchanged="ddlCategory_SelectedIndexChanged" > </asp:DropDownList>
</td>
<td class="Label">Item :
</td>
<td>
<asp:DropDownList ID="ddlItem" runat="server" Width="162px" CssClass="ComboBox" > </asp:DropDownList>
<asp:RequiredFieldValidator ID="RFV1" runat="server" ControlToValidate="ddlItem" Display="None"
TargetControlId="ddlItem" Enabled="true" ErrorMessage="Please Select Item To Add"
SetFocusOnError="true" ValidationGroup="AddItem" InitialValue="0">
</asp:RequiredFieldValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="true" 
TargetControlID="RFV1" WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>   
</td>
<td class="Label">Quantity :
</td>
<td>
<asp:TextBox runat="server" ID="TxtQty" CssClass="TextBox" Width="80px"></asp:TextBox>
<ajax:FilteredTextBoxExtender ID="FTE_Qty" runat="server" TargetControlID="TxtQty"
FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>
    &nbsp;&nbsp;&nbsp;&nbsp;
<asp:ImageButton ID="BtnAdd" runat="server" ToolTip="Add Item To List" 
        ValidationGroup="AddItem" ImageUrl="~/Images/Icon/Gridadd.png" TabIndex="9" 
        onclick="BtnAdd_Click"/>
</td>
</tr>
<tr><td colspan="6"><hr  width="100%"/></td></tr>
<tr>
<td colspan="6">
<div class="ScrollableDiv_FixHeightWidth4">
<asp:GridView ID="GridDetails" CssClass="mGrid" runat="server" AutoGenerateColumns="False"
BackColor="White" BorderColor="#0CCCCC" BorderStyle="None" BorderWidth="1px"
ForeColor="Black" GridLines="Horizontal" Font-Bold="False" 
        onrowcommand="GridDetails_RowCommand">
<Columns>
<asp:TemplateField HeaderText="">
<ItemTemplate>
<asp:ImageButton ID="ImgBtnDelete" runat="server" 
CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="Delete" 
ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete Item From List" />
<ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
ConfirmText="Would You Like To Delete The Record..!" 
TargetControlID="ImgBtnDelete">
</ajax:ConfirmButtonExtender>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
</asp:TemplateField>
<asp:BoundField HeaderText="ItemId" DataField="ItemId">
<HeaderStyle Wrap="false" CssClass="Display_None" />
<ItemStyle Wrap="false" CssClass="Display_None" />
</asp:BoundField>
<asp:BoundField HeaderText="Category" DataField="Category">
<HeaderStyle Wrap="false" HorizontalAlign="Left" />
<ItemStyle Wrap="false" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField HeaderText="Item Name" DataField="ItemName">
<HeaderStyle Wrap="false" HorizontalAlign="Left" />
<ItemStyle Wrap="false" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField HeaderText="Quantity" DataField="Quantity">
<HeaderStyle Wrap="false" HorizontalAlign="Left" />
<ItemStyle Wrap="false" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField HeaderText="DetailsId" DataField="DetailsId">
<HeaderStyle Wrap="false" CssClass="Display_None" />
<ItemStyle Wrap="false" CssClass="Display_None" />
</asp:BoundField>
</Columns>
</asp:GridView>
</div>
</td>
</tr>
</table>
</fieldset>
<tr>
<td colspan="6">
<asp:UpdatePanel ID="UpdatePanel9" runat="server">
<ContentTemplate>
<table width="100%">
<tr>
<td>
<fieldset id="fieldset4" width="100%" runat="server" class="FieldSet">
<asp:UpdatePanel ID="UpdatePanel4" runat="server">
<ContentTemplate>
<table width="100%">
<tr>
<td align="center">
<table align="center" width="25%">
<tr>
<td>
<asp:Button ID="BtnUpdate" runat="server" CssClass="button" 
TabIndex="17" Text="Update" onclick="BtnUpdate_Click"  />
<ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
ConfirmText="Would You Like To Update the Record ..! " 
TargetControlID="BtnUpdate">
</ajax:ConfirmButtonExtender>
</td>
<td>
<asp:Button ID="BtnSave" runat="server" CssClass="button" 
TabIndex="18" Text="Save" onclick="BtnSave_Click"  />
</td>             
<td>
<asp:Button ID="BtnCancel" runat="server" CausesValidation="False" 
CssClass="button" TabIndex="19" Text="Cancel" onclick="BtnCancel_Click" />
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
</td>
</tr>
<tr>
<td colspan="6">
<div class="scrollableDiv_FixHeightWidth">
<asp:GridView ID="GridReport" CssClass="mGrid" runat="server" AutoGenerateColumns="False"
BackColor="White" BorderColor="#0CCCCC" BorderStyle="None" BorderWidth="1px"
ForeColor="Black" GridLines="Horizontal" Font-Bold="False" 
        onrowcommand="GridReport_RowCommand" onrowdeleting="GridReport_RowDeleting">
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:ImageButton ID="ImgGridEdit" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
CommandName="SelectGrid" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit Record" />
<%-- <asp:ImageButton ID="ImagePrint" runat="server" CommandArgument='<%#Eval("#") %>' CommandName="Print"
ImageUrl="~/Images/Icon/GridPrint.png" ToolTip="Print" />
<ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Want To Delete This Record ?"
TargetControlID="ImagePrint">
</ajax:ConfirmButtonExtender>--%>
<asp:ImageButton ID="ImageBtnDelete" runat="server" 
CommandArgument='<% #Eval("#") %>' CommandName="Delete" 
ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete Record" 
/>
<ajax:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" 
ConfirmText="Would You want To Delete This Record ?" 
TargetControlID="ImageBtnDelete">
</ajax:ConfirmButtonExtender>

<a href='../Reports/MaterialIssueRegisterPrint.aspx?ID=<%# Eval("#")%>&Flag=<%="PS"%>' target="_blank">
<asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
ToolTip="Print Record" TabIndex="29" />
</ItemTemplate>
<FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<HeaderStyle Wrap="false" Width="6px"/>
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
</asp:TemplateField>
<asp:BoundField HeaderText="Inward No" DataField="InwardNo">
<HeaderStyle Wrap="false" HorizontalAlign="Left" />
<ItemStyle Wrap="false" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField HeaderText="Date" DataField="Date">
<HeaderStyle Wrap="false" HorizontalAlign="Left" />
<ItemStyle Wrap="false" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField HeaderText="By" DataField="EmpName">
<HeaderStyle Wrap="false" HorizontalAlign="Left" />
<ItemStyle Wrap="false" HorizontalAlign="Left" />
</asp:BoundField>
</Columns>
</asp:GridView>
</div>
</td>
</tr>
</td></tr>
</table>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

