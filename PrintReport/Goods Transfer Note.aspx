<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Goods Transfer Note.aspx.cs" Inherits="Reports_GoodTransferNote" Title="Good Transfer Note Print" %>

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
<h2>Good Transfer Note</h2></td>
</tr>
<tr>
<td colspan="5"  align="center" class="SubTitle">
    &nbsp;</td>
</tr>
<tr><%-- class="BoldText">--%>
<td  class="Label_Dynamic" align="right" ><%-- class="BoldText">--%>Transfer No :
</td>
<td style="width: 298px">
    <asp:Label ID="lblTransferNo" runat="server" CssClass="BoldText" 
        Text="TransferNo"></asp:Label>
</td>
<td style="width: 2px">
    &nbsp;</td>
<td align="right" class="Label_Dynamic">Transfer Date :</td>
<td align="left">
<asp:Label ID="lblTransferDate" runat="server" CssClass="BoldText" 
        Text="TransferDate"></asp:Label>
                                                     </td>
</tr>
<tr>
<td  class="Label_Dynamic" align="right" >Transfer By :</td>
<td style="width: 298px">
    &nbsp;<asp:Label ID="lblTransferBy" runat="server" CssClass="BoldText" 
        Text="TransferBy"></asp:Label>
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
<td align="right" class="Label_Dynamic">&nbsp;</td>
<td>
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
<div class="scrollableDiv">
          <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" 
          BackColor="White" BorderStyle="None" BorderWidth="1px" CssClass="mGrid" 
          Font-Bold="False" ForeColor="Black" GridLines="Horizontal">
          <Columns>
           <asp:TemplateField HeaderText="#" Visible="False">
            <ItemTemplate>
            <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
            </ItemTemplate>
           </asp:TemplateField>
           <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" >
            <HeaderStyle Wrap="False" CssClass="Display_None"/>
            <ItemStyle Wrap="False" CssClass="Display_None"/>
           </asp:BoundField> 
           <asp:BoundField DataField="CategoryName" HeaderText="Category">
                <HeaderStyle Wrap="false" />
                <ItemStyle Wrap="false" />
           </asp:BoundField>
           <asp:BoundField DataField="ItemId" HeaderText="ItemID" >
                <HeaderStyle Wrap="False" CssClass="Display_None"/>
                <ItemStyle Wrap="False" CssClass="Display_None"/>
           </asp:BoundField> 
           <asp:BoundField DataField="Item" HeaderText="Item">
                <HeaderStyle Wrap="false" />
                <ItemStyle Wrap="false" />
           </asp:BoundField>
           <asp:BoundField DataField="TransFrom" HeaderText="Transfer From">
                <HeaderStyle Wrap="false" />
                <ItemStyle Wrap="false" />
                </asp:BoundField>
                <asp:BoundField DataField="AvlQtySou" HeaderText="Qty At Source">
                <HeaderStyle Wrap="false" />
                <ItemStyle Wrap="false" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="StockLocationID" HeaderText="TransToId" >
                <HeaderStyle Wrap="False" CssClass="Display_None"/>
                <ItemStyle Wrap="False" CssClass="Display_None"/>
                </asp:BoundField> 
                <asp:BoundField DataField="TransTo" HeaderText="Transfer To">
                <HeaderStyle Wrap="false" />
                <ItemStyle Wrap="false" />
                </asp:BoundField>
                <asp:BoundField DataField="AvlQty" HeaderText="Qty At Destination">
                <HeaderStyle Wrap="false" />
                <ItemStyle Wrap="false" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Qty" HeaderText="Transfer Qty">
                <HeaderStyle Wrap="false" />
                <ItemStyle Wrap="false" HorizontalAlign="Right"/>
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
<table Width="100%">
<tr>
<td class="Label1" width="15%">Prepared By</td>
<td></td>
<td class="Label1" width="15%">Received By</td>
<td></td>
<td class="Label1" width="15%">Authorised By</td>
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
<td class="Label2">Unit Manager / Store Incharge</td>
<td>&nbsp;</td>
<td class="Label2">Unit Head / Operation Manager </td>
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
