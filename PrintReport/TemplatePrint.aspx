<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="TemplatePrint.aspx.cs" Inherits="Reports_TemplatePrint" Title="Template Print" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<div>
<fieldset id="fieldset1" class="FieldSet">
<table width="100%">
<tr>
<td colspan="5" align="right" style="height: 36px">
<asp:ImageButton ID="ImgPrint" runat="server" 
ImageUrl="~/Images/Icon/Print-Button.png" CssClass="Imagebutton"
OnClientClick="javascript:CallPrint('divPrint')"
ToolTip="PDF" Height="35px" Width="35px"/>
<asp:ImageButton ID="ImgPdf" runat="server" ImageUrl="~/Images/Icon/PDF.jpg" CssClass="Imagebutton"
ToolTip="PDF" Height="35px" Width="35px" onclick="ImgPdf_Click" />
</td>
</tr>
<tr>
<td colspan="5">
<div id="divPrint"> 
<table width="100%" align="center">
<tr>
<td colspan="5">
<asp:Image ID="imgAntTime" runat="server" CssClass="image" Width="150px" Height="50px"/>
</td>
</tr>
<tr>
<td class="Label_Dynamic" colspan="4">
    <asp:Label ID="lblCompanyName" runat="server" Text="CompanyName" Font-Bold="True"></asp:Label><br />
    <asp:Label ID="lblCompanyAddress" runat="server" Text="CompanyAddresss" Font-Bold="True"></asp:Label><br />
    <b>PhoneNo:</b><asp:Label ID="lblPhnNo" runat="server" Text="phoneNo" Font-Bold="True"></asp:Label><br />
    <b>FaxNo:</b><asp:Label ID="lblFaxNo" runat="server" Text="FaxNo" Font-Bold="True"></asp:Label><br />
</td>
</tr>
<tr>
<td colspan="5"  align="center" class="SubTitle" >
<h2>Template Details</h2></td>
</tr>
<tr>
<td colspan="5"  align="center" class="SubTitle">
    &nbsp;</td>
</tr>
<tr><%-- class="BoldText">--%>
<td  class="Label_Dynamic" align="right" ><%-- class="BoldText">--%>Title :&nbsp;
</td>
<td style="width: 298px">
    <asp:Label ID="lblCafeteria" runat="server" CssClass="BoldText" 
        Text="Cafeteria"></asp:Label>
</td>
<td style="width: 2px">
    &nbsp;</td>
<td align="right" class="Label_Dynamic">Template Date :</td>
<td>
<asp:Label ID="lblIssueDate" runat="server" CssClass="BoldText" Text="IssueDate"></asp:Label></td>
</tr>
<tr>
<td  class="Label_Dynamic" align="right" >&nbsp;</td>
<td style="width: 298px">
    &nbsp;&nbsp;<br />
</td>
<td style="width: 2px">
    &nbsp;</td>
<td></td>
<td></td>
</tr>
<tr>
<td  colspan="5">
    <hr style="width:100%"/>
    </td>
</tr>
<tr>
<td colspan="5">
<table width="100%">
<tr>
<td>
<div id="divProDetails">
<asp:GridView ID="IssueRegGrid" runat="server" 
CssClass="mGrid" AutoGenerateColumns="False" >
<Columns>
<asp:TemplateField HeaderText="Sr. No." >                        
<ItemTemplate>
<asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="6%" Wrap="False"/>
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" 
Width="6%" />
</asp:TemplateField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="Location" HeaderText="Location" Visible="false">
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="AvlQty" HeaderText="Avl Qty" Visible="false">
<HeaderStyle Wrap="False" HorizontalAlign="Left" CssClass="Display_None"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="Display_None" />
</asp:BoundField>
<asp:BoundField DataField="DeliveryPeriod" HeaderText="Delivery Period" Visible="false">
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="AvgRate" HeaderText="Avg Rate" Visible="false" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="Vendor" HeaderText="Vendor" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Vendor Rate" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
</Columns>
</asp:GridView>
</div>
</td>
</tr>
</table>
</td>        
</tr>  

<tr>
<td class="Label1">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label1">&nbsp;</td>
<td >&nbsp;</td>
</tr>
<tr>
<td class="Label1" width="15%">Prepared By :</td>
<td></td>
<td class="Label1" width="30%">Authorised By :</td>
<td ></td>
</tr> 
<tr>
<td class="Label1">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label1">&nbsp;</td>
<td >&nbsp;</td>
</tr> 
<tr>
<td class="Label1">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label1">&nbsp;</td>
<td >&nbsp;</td>
</tr> 
<tr>
<td class="Label2">Name & Designation</td>
<td></td>
<td class="Label2">Name & Designation</td>
<td ></td>
</tr>
<tr>
<td class="Label1">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label1">&nbsp;</td>
<td >&nbsp;</td>
</tr>
</table>
</div>
</td>
</tr>
</table>
</fieldset></div>
</asp:Content>
