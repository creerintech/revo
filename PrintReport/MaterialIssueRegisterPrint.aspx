<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialIssueRegisterPrint.aspx.cs" Inherits="Reports_MaterialIssueRegisterPrint" Title="Material Issue Register Report" %>
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
<table width="100%" align="center" cellspacing="5">
<tr>
<td colspan="5" >
<asp:Image ID="imgAntTime" runat="server" CssClass="image" Width="150px" Height="50px" /></td>
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
<td colspan="5"  align="center" class="SubTitle">
<h2>Material Issue Register Details</h2></td>
</tr>
<tr>
<td colspan="5"  align="center" class="SubTitle">
    &nbsp;</td>
</tr>
<tr><%-- class="BoldText">--%>
<td  class="Label_Dynamic" align="right" ><%-- class="BoldText">--%> Issue No :
</td>
<td style="width: 298px">
<asp:Label ID="lblIssueNo" runat="server" CssClass="BoldText" Text="IssueNo"></asp:Label>
</td>
<td style="width: 2px">
    &nbsp;</td>
<td align="right" class="Label_Dynamic">For Requisition :</td>
<td>
<asp:Label ID="lblForRequisition" runat="server" CssClass="BoldText" Text="ForRequisition"></asp:Label>
    </td>
</tr>
<tr>
<td  class="Label_Dynamic" align="right" >Issue Date :</td>
<td style="width: 298px">
    <asp:Label ID="lblIssueDate" runat="server" CssClass="BoldText" Text="IssueDate"></asp:Label>
    <br />
</td>
<td style="width: 2px">
    &nbsp;</td>
<td class="Label_Dynamic" align="right" style="height: 18px">Requisition Date :</td>
<td>
    <asp:Label ID="ReqDate" runat="server" CssClass="BoldText" Text="ReqDate"></asp:Label></td>
</tr>
<tr>
<td  class="Label_Dynamic" align="right" >Issue From :</td>
<td style="width: 298px">
<asp:Label ID="Label1" runat="server" CssClass="BoldText" Text="Central Location"></asp:Label>
</td>
<td style="width: 2px">
    &nbsp;</td>
<td class="Label_Dynamic" align="right" style="height: 18px">Issue To :</td>
<td >
    <asp:Label ID="lblSuplier" runat="server" CssClass="BoldText" Text="Cafeteria"></asp:Label>
    </td>
</tr>

<tr>
<td class="Label_Dynamic" align="right" style="height: 18px">
    &nbsp;</td>
<td >
    &nbsp;</td>
<td style="width: 2px; height: 18px;">
    </td>
<td class="Label_Dynamic" align="right" style="width: 133px; height: 18px;">
    </td>
<td style="height: 18px">
    &nbsp;&nbsp;</td>
</tr>
<tr>
<td class="Label_Dynamic" align="right">
    &nbsp;</td>
<td colspan="4">
    &nbsp;</td>
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
CssClass="mGrid" AutoGenerateColumns="false" >
<Columns>
<asp:TemplateField HeaderText="Sr. No." >                        
<ItemTemplate>
<asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%"/>
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
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
<asp:BoundField DataField="ReqQty" HeaderText="Requisition Qty" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="IssueQty" HeaderText="Item Issused" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="PendingQty" HeaderText="Item Pending" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="Status" HeaderText="Status" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
</Columns>
</asp:GridView>
</div>
</td>
</tr>
<tr><td></td></tr>
<tr><td></td></tr>
<tr><td></td></tr>
<tr><td></td></tr>
<tr>
<td colspan="5" class="Label_Dynamic" align="right">
Issused By : 
<asp:Label runat="server" Text="" CssClass="BoldText" ID="lblIssBy"></asp:Label>
</td>
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

