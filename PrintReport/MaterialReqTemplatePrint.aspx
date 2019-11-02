<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialReqTemplatePrint.aspx.cs" Inherits="Reports_MaterialReqTemplatePrint" Title="Requisition Print" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<div>
<fieldset id="fieldset1" class="FieldSet">
<table width="100%" cellspacing="8">
<tr>
<td colspan="6" align="right" style="height: 36px">
<asp:ImageButton ID="ImgPrint" runat="server" 
ImageUrl="~/Images/Icon/Print-Button.png" CssClass="Imagebutton"
OnClientClick="javascript:CallPrint('divPrint')"
ToolTip="PDF" Height="35px" Width="35px"/>
<asp:ImageButton ID="ImgPdf" runat="server" ImageUrl="~/Images/Icon/PDF.jpg" CssClass="Imagebutton"
ToolTip="PDF" Height="35px" Width="35px" onclick="ImgPdf_Click" />
<asp:ImageButton ID="ImgBtnExport" runat="server" 
    ImageUrl="~/Images/Icon/excel.png" TabIndex="7"
    ToolTip="Export To Excel"  CssClass="Imagebutton" 
        onclick="ImgBtnExport_Click1" />
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
<h2>Material Requisition Details</h2></td>
</tr>
<tr>
<td colspan="5"  align="center" class="SubTitle">
    &nbsp;</td>
</tr>
<tr>
<td  class="Label_Dynamic" align="right" >Requisition No :
</td>
<td >
    <asp:Label ID="lblReqNo" runat="server" CssClass="BoldText" 
        ></asp:Label>
</td>
<td ></td>
<td align="right" class="Label_Dynamic">Requisition Date :</td>
<td>
<asp:Label ID="lblReqDate" runat="server" CssClass="BoldText" Width="100px"></asp:Label></td>
</tr>
<tr>
<td  class="Label_Dynamic" align="right" >Requisition From :</td>
<td >
    <asp:Label ID="lblAuthBy" runat="server" CssClass="BoldText" ></asp:Label>
</td>
<td >
    &nbsp;</td>
<td align="right" class="Label_Dynamic">Requisition By :</td>
<td><asp:Label ID="lblEmp" runat="server" CssClass="BoldText" ></asp:Label></td></td>
</tr>
<tr>
<td  colspan="5">
<hr style="width:100%"/>
</td>
</tr>
<tr>
<td colspan="10">
<table width="100%">
<tr>
<td>
<div id="divProDetails">
<asp:GridView ID="ReqGrid" runat="server" 
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
<asp:BoundField DataField="ItemCode" HeaderText="Code" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>

<asp:BoundField DataField="DeliveryPeriod" HeaderText="Del. Days" Visible="false" >
<HeaderStyle Wrap="False" HorizontalAlign="Center" />
<ItemStyle Wrap="False" HorizontalAlign="Right" CssClass="Display_None"/>
</asp:BoundField>
<asp:BoundField DataField="AvgRate" HeaderText="Avg. Rate"  Visible="false">
<HeaderStyle Wrap="False" HorizontalAlign="Center" CssClass="Display_None"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" CssClass="Display_None"/>
</asp:BoundField>
<asp:BoundField DataField="Vendor" HeaderText="Vendor" Visible="false">
<HeaderStyle Wrap="False" HorizontalAlign="Center" CssClass="Display_None"/>
<ItemStyle Wrap="False" HorizontalAlign="Left"  CssClass="Display_None"/>
</asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="Qty" HeaderText="Ord. Qty" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>

<asp:BoundField DataField="Qty" HeaderText="Issue Qty" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>

<asp:BoundField DataField="Remark" HeaderText="Phy.Issus Qty" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Center" />
</asp:BoundField>

<asp:BoundField DataField="Amount" HeaderText="Amount" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Right" />
</asp:BoundField>


<asp:BoundField DataField="Location" HeaderText="Location" Visible="false" >
<HeaderStyle Wrap="False" HorizontalAlign="Left"/>
<ItemStyle Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>

<asp:BoundField DataField="ReqStatus" HeaderText="Status" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Center" />
</asp:BoundField>

<asp:BoundField DataField="Remark" HeaderText="Remark" >
<HeaderStyle Wrap="False" HorizontalAlign="Center"/>
<ItemStyle Wrap="False" HorizontalAlign="Center" />
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
<td class="Label1" width="15%" align="center">Prepared By</td>
<td></td>
<td class="Label1" width="15%" align="center">Authorised By</td>
<td></td>
<td class="Label1" width="15%" align="center">Issued By</td>
<td></td>
<td class="Label1" width="15%" align="center">Received By</td>
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
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
</tr>
<tr>
<td class="Label2"> <asp:Label ID="LblPreareBy" runat="server" CssClass="BoldText" ></asp:Label></td>
<td>&nbsp;</td>
<td class="Label2"> <asp:Label ID="LblAuthorisedBy" runat="server" CssClass="BoldText" ></asp:Label></td>
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
<td></td>
<td class="Label2">Name &amp; Designation</td>
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
</table>
</fieldset>
</div>

   

</asp:Content>

