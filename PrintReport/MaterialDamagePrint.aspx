<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialDamagePrint.aspx.cs" Inherits="Reports_DamagePrint" Title="Damage Details Print" %>

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
<td colspan="5" >
<asp:Image ID="imgAntTime" runat="server" CssClass="image" Width="150px" Height="50px"/></td>
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
<h2>Damage Details</h2></td>
</tr>
<tr>
<td colspan="5"  align="center" class="SubTitle">
    &nbsp;</td>
</tr>
<tr><%-- class="BoldText">--%>
<td  class="Label_Dynamic" align="right" ><%-- class="BoldText">--%>Damage No :
</td>
<td style="width: 298px">
<asp:Label ID="lblDamageNo" runat="server" CssClass="BoldText" Text="DamageNo"></asp:Label>
</td>
<td style="width: 2px">
    &nbsp;</td>
<td align="right" class="Label_Dynamic">Damage Date :</td>
<td align="left">
<asp:Label ID="lblDamageDate" runat="server" CssClass="BoldText" Text="DamageDate"></asp:Label>
                                                     </td>
</tr>
<tr>
<td  class="Label_Dynamic" align="right" >Prepared By :</td>
<td style="width: 298px">
    &nbsp;<asp:Label ID="lblPreparedBy" runat="server" CssClass="BoldText" 
        Text="PreparedBy"></asp:Label>
    <br />
</td>
<td style="width: 2px">
    &nbsp;</td>
<td></td>
<td></td>
</tr>
<tr>
<td  class="Label_Dynamic" align="right" >&nbsp;</td>
<td style="width: 298px">
    &nbsp;</td>
<td style="width: 2px">
    &nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
</tr>
<tr id="TR1" runat="server">
<td  class="Label_Dynamic" align="right" >Inward No :</td>
<td style="width: 298px">
<asp:Label ID="lblInwardNo" runat="server" CssClass="BoldText" Text="InwardNo"></asp:Label>
</td>
<td style="width: 2px">
    &nbsp;</td>
<td align="right" class="Label_Dynamic">Inward Date :</td>
<td>
<asp:Label ID="lblInwardDate" runat="server" CssClass="BoldText" Text="InwardDate"></asp:Label></td>
</tr>
<tr id="TR2" runat="server">
<td  class="Label_Dynamic" align="right" >PO No :</td>
<td style="width: 298px">
<asp:Label ID="lblPONo" runat="server" CssClass="BoldText" Text="PONo"></asp:Label>
</td>
<td style="width: 2px">
    &nbsp;</td>
<td align="right" class="Label_Dynamic">Supplier Name :</td>
<td>
<asp:Label ID="lblSuppName" runat="server" CssClass="BoldText" Text="SuppName"></asp:Label></td>
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
<asp:GridView ID="GrdDamage" runat="server" 
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
<asp:BoundField DataField="InwardQty" HeaderText="InwardQty" >
</asp:BoundField>
<asp:BoundField DataField="DamageQty" HeaderText="DamageQty" >
</asp:BoundField>
<asp:BoundField DataField="InwardRate" HeaderText="InwardRate" >
</asp:BoundField>
<asp:BoundField DataField="Reason" HeaderText="Reason" >
</asp:BoundField>
    <asp:BoundField DataField="Amount" HeaderText="Amount">
        <FooterStyle HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
    </asp:BoundField>
</Columns>
</asp:GridView>
</div>
</td>
</tr>
<tr>
<td colspan="5">
<table width="100%">
<tr>
<td class="Label1" width="30%">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label1" width="30%">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label1" width="30%">&nbsp;</td>
</tr>
<tr>
<td class="Label1" width="30%">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label1" width="30%">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label1" width="30%">&nbsp;</td>
</tr>
<tr>
<td class="Label1" width="30%">Prepared By</td>
<td></td>
<td class="Label1" width="30%">Accepted By</td>
<td></td>
<td class="Label1" width="30%">Authorised By</td>
</tr>
<tr>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
</tr>
<tr>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
</tr>
<tr>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
</tr>
<tr>
<td class="Label2">Name &amp; Designation</td>
<td></td>
<td class="Label2">Name &amp; Designation</td>
<td></td>
<td class="Label2">Name &amp; Designation</td>
</tr>
<tr>
<td class="Label2">Store Incharge</td>
<td>&nbsp;</td>
<td class="Label2">Vendor: Seal &amp; Signature</td>
<td>&nbsp;</td>
<td class="Label2">Unit Head/Operation Manager</td>
</tr>
</table>
 
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
