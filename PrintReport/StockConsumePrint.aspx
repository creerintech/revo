<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="StockConsumePrint.aspx.cs" Inherits="Reports_StockConsumePrint" Title="Stock Consumption Print" %>
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
ToolTip="Print" Height="35px" Width="35px"/>
<asp:ImageButton ID="ImgPdf" runat="server" ImageUrl="~/Images/Icon/PDF.jpg" CssClass="Imagebutton"
ToolTip="PDF" Height="35px" Width="35px" Visible="true" onclick="ImgPdf_Click"/>
</td>
</tr>
<tr>
<td colspan="5">
<div id="divPrint"> 
<table width="100%" align="center">
<tr>
<td colspan="5" >
<asp:Image ID="imgAntTime" runat="server" CssClass="image"  Width="150px" Height="50px"/></td>
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
<h2>Consumption Details</h2></td>
</tr>

<tr>
<td class="Label_Dynamic" align="left" colspan="2" >Consumption No :&nbsp;

<asp:Label ID="lblStockNo" runat="server"  Text="" ></asp:Label>
</td>
<td class="Label_Dynamic" align="right" colspan="2">Date :&nbsp;
<asp:Label ID="lblStockDate" runat="server" Text=""></asp:Label></td>
</tr>


<tr>
<td class="Label_Dynamic" align="left" colspan="2" >For Issue :&nbsp;

<asp:Label ID="lblRefNo" runat="server"  Text="" ></asp:Label>
</td>
<td class="Label_Dynamic" align="right" colspan="2"></td>
</tr>

<tr>
<td  colspan="5">
    <hr />
    </td>
</tr>
<tr>
<td colspan="5">
<table width="100%">
<tr>
<td>
<div id="divProDetails">
<asp:GridView ID="ReqGrid" runat="server" 
CssClass="mGrid" AutoGenerateColumns="false" width="100%">
<Columns>
<asp:TemplateField HeaderText="Sr. No." >                        
<ItemTemplate>
<asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%"/>
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
Width="6%" />
</asp:TemplateField>
<asp:BoundField DataField="Item" HeaderText="Item Name" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="Unit" HeaderText="Unit" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="Issue" HeaderText="Issue Qty" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="rate" HeaderText="Rate" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="Amount" HeaderText="Amount" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="Consumption" HeaderText="Consumed Qty" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="ConsumptionAmt" HeaderText="Amount" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
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
<td colspan="5">
<table width="100%">
<tr>
<td class="Label1" width="35%">Prepared By</td>
<td></td>
<td class="Label1" width="30%">Authorised By</td>
<td></td>
<td class="Label1" width="30%">Consumption By</td>
<td></td>
<td class="Label1" width="30%">Received By</td>
</tr>
<tr>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
</tr>
<tr>
<td class="Label2"><asp:Label ID="LblPrepareBy" runat="server"  ></asp:Label></td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label2"><asp:Label ID="LblIssusedBy" runat="server" ></asp:Label></td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
</tr>
<tr>
<td class="Label2" width="30%">Name & Designation</td>
<td></td>
<td class="Label2" width="30%">Name & Designation</td>
<td></td>
<td class="Label2" width="30%">Name & Designation</td>
<td></td>
<td class="Label2" width="30%">Name & Designation</td>
</tr>
<tr>
<td class="Label2">Store Incharge</td>
<td>&nbsp;</td>
<td class="Label2">Unit Head / Operation Manager </td>
<td>&nbsp;</td>
<td class="Label2">Store Incharge</td>
<td>&nbsp;</td>
<td class="Label2">&nbsp;</td>
</tr>
</table>
</td>
</tr>

</table>
</div>
</td>
</tr>
</table>
</fieldset></div>
</asp:Content>

