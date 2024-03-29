﻿<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialDamageDetails.aspx.cs" Inherits="Reports_MaterialDamageDetails" Title="Material Damage Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</ajax:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Material Damage Details   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<fieldset id="F1" runat="server" class="FieldSet">
<table width="100%" cellspacing="7">
<tr><td colspan="8"></td></tr>
<tr>
<td class="Label"> 
    <asp:CheckBox ID="ChkFromDate" runat="server" 
        AutoPostBack="true" CssClass="CheckBox" Text="  From :" 
        oncheckedchanged="ChkFromDate_CheckedChanged" /></td>
<td >
<asp:TextBox ID="TxtFromDate" runat="server" CssClass="TextBox" Width="90px" 
        ></asp:TextBox>
<asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CssClass="Imagebutton"
ImageUrl="~/Images/Icon/DateSelector.png" />
<ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
PopupButtonID="ImageButton1" TargetControlID="TxtFromDate"></ajax:CalendarExtender>&nbsp;<ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" Format="dd-MMM-yyyy"
PopupButtonID="ImageButton2" TargetControlID="TxtToDate"></ajax:CalendarExtender>
</td>
<td class="Label"> To:</td>
<td>
    <asp:TextBox ID="TxtToDate" runat="server" CssClass="TextBox" Width="90px" 
       ></asp:TextBox>
    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" 
        CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
</td>
<td class="Label">&nbsp;Damage No:</td>
<td>
    <asp:DropDownList ID="ddlDamageNo" runat="server" Width="162px">
    </asp:DropDownList>
</td>
<td class="Label">Inward No :</td>
<td >
    <asp:DropDownList ID="ddlInward" runat="server" Width="162px">
    </asp:DropDownList>
    </td>
</tr>
<tr>
<td class="Label"> Category : </td>
<td>
<asp:DropDownList ID="ddlCategory" runat="server" Width="162px" AutoPostBack="true"
        onselectedindexchanged="ddlCategory_SelectedIndexChanged" ></asp:DropDownList>
</td>
<td class="Label">Sub Category :
</td>
<td><asp:DropDownList ID="ddlsubcategory" runat="server" Width="162px" AutoPostBack="true"
        onselectedindexchanged="ddlsubcategory_SelectedIndexChanged"></asp:DropDownList></td>

<td class="Label">Items :
</td>
<td><asp:DropDownList ID="ddlItems" runat="server" Width="162px" ></asp:DropDownList></td>

<td class="Label">Attended By :</td>
<td><asp:DropDownList ID="ddlEmployee" runat="server" Width="162px" ></asp:DropDownList></td>

</tr>

<tr>
<td class="Label">
    Location :</td>
<td>
<asp:DropDownList ID="ddlLocation" runat="server" Width="162px" ></asp:DropDownList>
</td>
</tr>


<tr>
<td colspan="7"></td>
<td align="center">
    &nbsp;<asp:Button ID="BtnShow" runat="server" CssClass="button" 
TabIndex="4" Text="Show" ValidationGroup="Add" 
ToolTip="Show Details" onclick="BtnShow_Click"  />   
<asp:Button ID="BtnCancel" runat="server" CssClass="button" 
TabIndex="5" Text="Cancel"  
ToolTip="Clear The Details" onclick="BtnCancel_Click" />
</td>
</tr>
<tr>
<td colspan="7" align="center"> <asp:Label ID="lblCount" CssClass="SubTitle"  runat="server"></asp:Label></td></td>
<td align="right">
<asp:ImageButton ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/Print-Icon.png" OnClientClick="javascript:CallPrint('divPrint')" />
<asp:ImageButton ID="ImgBtnExcel" runat="server" 
        ImageUrl="~/Images/Icon/excel-icon.png" onclick="ImgBtnExcel_Click"  />
</td>
</tr>
<tr>
<td colspan="8">
<div id="divPrint" class="ScrollableDiv_FixHeightWidth4">
<asp:GridView ID="GridDetails" runat="server"
AutoGenerateColumns="False" BackColor="White" BorderColor="#0CCCCC"
BorderStyle="None" BorderWidth="1px" ForeColor="Black" GridLines="Horizontal"
CaptionAlign="Top" CssClass="mGrid" Width="100%" 
onrowdatabound="GridDetails_RowDataBound" ShowFooter="True"
onpageindexchanging="GridDetails_PageIndexChanging" >
<Columns>
<asp:TemplateField HeaderText="Sr.No">
<ItemTemplate>
<asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'>
</asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" Width="6%" />
</asp:TemplateField>
<asp:BoundField DataField="DamageNo" HeaderText="Damage No">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="50px" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="50px" />
</asp:BoundField>
<asp:BoundField DataField="Date" HeaderText="Date">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField="type" HeaderText="Damage Type">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField ="Employee" HeaderText="Employee">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField ="InwardNo" HeaderText="Inward No">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField="InwardDate" HeaderText="Date">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField="PONO" HeaderText="Purchase Order">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField="Suplier" HeaderText="Vendor">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField="Category" HeaderText="Category">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Particular">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField="ItemDesc" HeaderText="Description">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField="Reason" HeaderText="Remark">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
<FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" 
VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
<asp:BoundField DataField="Unit" HeaderText="Unit">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField="DamageQty" HeaderText="Quantity">
<HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Right" VerticalAlign="Middle" Wrap="false" />
<FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" 
VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>

<asp:BoundField DataField="Rate" HeaderText="Rate">
<FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" 
VerticalAlign="Middle" Wrap="False" />
<HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" 
VerticalAlign="Middle" Wrap="False" />
<ItemStyle Font-Bold="False" HorizontalAlign="Right" VerticalAlign="Middle" 
Wrap="False" />
</asp:BoundField>
<asp:BoundField DataField="Amount" HeaderText="Amount">
<FooterStyle ForeColor="White" Font-Bold="True" HorizontalAlign="Right" VerticalAlign="Middle" 
Wrap="False" />
<HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" 
VerticalAlign="Middle" Wrap="False" />
<ItemStyle Font-Bold="False" HorizontalAlign="Right" VerticalAlign="Middle" 
Wrap="False" />
</asp:BoundField>
</Columns>
<PagerStyle CssClass="pgr" />
<AlternatingRowStyle CssClass="alt" />
<FooterStyle CssClass="ftr" />
</asp:GridView>
</div>
</td>           
 </tr>
  <%--<tr><td colspan="8"></td></tr>--%>
  <tr><td colspan="3" align="left"></td><td colspan="3"></td></tr>
  <tr><td colspan="7" align="right" class="Display_None" >Net Total :
  <asp:Label ID="lblNetAmount" runat="server" Text="0.00" Font-Bold="true"></asp:Label></td></tr>
  <tr><td colspan="7" align="right" class="Display_None" >Rate :
  <asp:Label ID="lblRate" runat="server" Text="0.00" Font-Bold="true"></asp:Label></td></tr>
</table>
</fieldset>
</ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID ="ImgBtnExcel" />
</Triggers>
</asp:UpdatePanel>
</asp:Content>

