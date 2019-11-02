<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="DeviationPrint.aspx.cs" Inherits="PrintReport_DeviationPrint" Title="Deviation Details Print" %>
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
  <asp:ImageButton ID="ImgBtnExport" runat="server" 
        ImageUrl="~/Images/Icon/excel.png" TabIndex="7"
ToolTip="Export To Excel"  CssClass="Imagebutton" onclick="ImgBtnExport_Click"/>
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
<h2>Deviation Details</h2></td>
</tr>
<tr>
<td colspan="5"  align="center" class="SubTitle">
    &nbsp;</td>
</tr>
<tr><%-- class="BoldText">--%>
<td  class="Label_Dynamic" align="right" ><%-- class="BoldText">--%>Deviation No :
</td>
<td style="width: 298px">
<asp:Label ID="lblDeviationNo" runat="server" CssClass="BoldText" Text="DamageNo"></asp:Label>
</td>
<td style="width: 2px">
    &nbsp;</td>
<td align="right" class="Label_Dynamic">Date :</td>
<td align="left">
<asp:Label ID="lblDeviationDate" runat="server" CssClass="BoldText" Text="DamageDate"></asp:Label>
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
<td  class="Label_Dynamic" align="right" >Daviation From :</td>
<td style="width: 398px">
<asp:Label ID="lblDaviationFrom" runat="server" CssClass="BoldText" Text="InwardNo"></asp:Label>
</td>
<td style="width: 2px">
    &nbsp;</td>
<td align="right" class="Label_Dynamic"></td>
<td>

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
<asp:GridView ID="GrdDeviation" runat="server" 
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
<asp:BoundField DataField="Category" HeaderText="Category" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="Code" HeaderText="Code" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="Product" HeaderText="Product" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="Closing" HeaderText="System Closing " >
<HeaderStyle Wrap="False" HorizontalAlign="Right"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="PhyClosing" HeaderText="Physical Closing" >
<HeaderStyle Wrap="False" HorizontalAlign="Right"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="Deviation" HeaderText="Deviation" >
<HeaderStyle Wrap="False" HorizontalAlign="Right"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="DMRP" HeaderText="Deviation MRP" >
<HeaderStyle Wrap="False" HorizontalAlign="Right"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="DeviationRate" HeaderText="Deviation Amount" >
<HeaderStyle Wrap="False" HorizontalAlign="Right"/>
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
</table>
</div>
</td>
</tr>
</table>
</fieldset></div>
</asp:Content>

