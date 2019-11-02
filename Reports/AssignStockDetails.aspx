<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="AssignStockDetails.aspx.cs" Inherits="Reports_MaterialReqSummary" Title="Issuse Stock Details" %>
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
    Issuse Stock Details     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div width="99%">
<fieldset id="F1" runat="server" class="FieldSet">
<table width="100%" cellspacing="7">
<tr><td colspan="7"></td></tr>
<tr>
<td class="Label"> <asp:CheckBox ID="ChkFromDate" runat="server" 
        AutoPostBack="true" CssClass="CheckBox" Text="  From :" 
        oncheckedchanged="ChkFromDate_CheckedChanged" /></td>
<td>
<asp:TextBox ID="TxtFromDate" runat="server" CssClass="TextBox" Width="90px" 
      ></asp:TextBox>
<asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CssClass="Imagebutton"
ImageUrl="~/Images/Icon/DateSelector.png" />
<ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
PopupButtonID="ImageButton1" TargetControlID="TxtFromDate"></ajax:CalendarExtender>
</td>
<td class="Label">To:
</td>
<td>
<asp:TextBox ID="TxtToDate" runat="server" CssClass="TextBox" Width="90px" 
      ></asp:TextBox>
<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CssClass="Imagebutton"
ImageUrl="~/Images/Icon/DateSelector.png" />
<ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" Format="dd-MMM-yyyy"
PopupButtonID="ImageButton2" TargetControlID="TxtToDate"></ajax:CalendarExtender>
</td>
<td class="Label">
    Issuse No:</td>
<td>
<asp:DropDownList ID="ddlStockNo" runat="server" Width="182px" ></asp:DropDownList>
</td>
<td>
 <asp:RadioButtonList ID="RdoType" runat="server" 
            CellPadding="25" RepeatDirection="Horizontal" >
            <asp:ListItem Selected="True" Text="&nbsp;All&nbsp;&nbsp;&nbsp;&nbsp;" 
            Value="All"></asp:ListItem>
            <asp:ListItem  Text="&nbsp;Inward&nbsp;&nbsp;&nbsp;&nbsp;" 
            Value="IW"></asp:ListItem>
            <asp:ListItem Text="&nbsp;Item&nbsp;&nbsp;&nbsp;"
            Value="I"></asp:ListItem>
            <asp:ListItem Text="&nbsp;Requisition" Value="RQ"></asp:ListItem>
            </asp:RadioButtonList>
</td></tr>

<tr>
<td class="Label"> Category :</td>
<td>
<asp:DropDownList ID="ddlCategory" runat="server" Width="182px" AutoPostBack="True" 
        onselectedindexchanged="ddlCategory_SelectedIndexChanged" ></asp:DropDownList>
</td>
<td class="Label">Sub Category :
</td>
<td>
<asp:DropDownList ID="ddlSubCategory" runat="server" Width="182px" AutoPostBack="True" 
        onselectedindexchanged="ddlSubCategory_SelectedIndexChanged"></asp:DropDownList>
</td>

<td class="Label">Items :
</td>
<td>
<asp:DropDownList ID="ddlItems" runat="server" Width="182px" ></asp:DropDownList>
</td>


</tr>

<tr>
<td class="Label">
    Location :</td>
<td>
<asp:DropDownList ID="ddlLocation" runat="server" Width="182px" ></asp:DropDownList>
</td>
<td align="right" colspan="5">
    &nbsp;<asp:Button ID="BtnShow" runat="server" CssClass="button" 
TabIndex="4" Text="Show" ValidationGroup="Add" 
ToolTip="Show Details" onclick="BtnShow_Click"   />   
<asp:Button ID="BtnCancel" runat="server" CssClass="button" 
TabIndex="5" Text="Cancel"  
ToolTip="Clear The Details" onclick="BtnCancel_Click"  />
</td>
</tr>


<tr>
<td colspan="6" align="center"> <asp:Label ID="lblCount" CssClass="SubTitle"  runat="server"></asp:Label></td></td>
<td align="right">
<asp:ImageButton ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/Print-Icon.png" OnClientClick="javascript:CallPrint('DivGridDetails')" />
<asp:ImageButton ID="ImgBtnExcel" runat="server" 
        ImageUrl="~/Images/Icon/excel-icon.png" onclick="ImgBtnExcel_Click"  />
</td>
</tr>
<tr>
<td colspan="7">
<div id="DivGridDetails" class="ScrollableDiv_FixHeightWidth4">
<asp:GridView ID="GridDetails" runat="server"
AutoGenerateColumns="False" BackColor="White" BorderColor="#0CCCCC"
BorderStyle="None" BorderWidth="1px" ForeColor="Black" GridLines="Horizontal"
CaptionAlign="Top" CssClass="mGrid" Width="100%" 
onrowdatabound="GridDetails_RowDataBound" ShowFooter=True
onpageindexchanging="GridDetails_PageIndexChanging">
<Columns>
<asp:TemplateField HeaderText="Sr.No">
<ItemTemplate>
<asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'>
</asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" Width="6%" />
</asp:TemplateField>
<asp:BoundField DataField="Type" HeaderText="Type">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="50px" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="50px" />
</asp:BoundField>
<asp:BoundField DataField="StockNo" HeaderText="Issuse No">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="50px" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="50px" />
</asp:BoundField>
<asp:BoundField DataField="Date" HeaderText="Date">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField ="Location" HeaderText="Issuse Location">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField ="Employee" HeaderText="Issuse By">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField ="InwardNo" HeaderText="Inward / Requisition No">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField ="INDATE" HeaderText="Inward / Requisition Date">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField="Category" HeaderText="Category">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField="Items" HeaderText="Particular">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField="ItemDesc" HeaderText="Description">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField ="Unit" HeaderText="Unit">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField ="InwardQty" HeaderText="Inward / Requisition Qty">
<HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField ="OutwardQty" HeaderText="Issuse Qty">
<HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField ="Pending_Qty" HeaderText="Pending Qty">
<HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>


<asp:BoundField DataField ="OutwardRate" HeaderText="Rate">
    <FooterStyle Font-Bold="True" ForeColor="White" Wrap="False" />
<HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField ="Amount" HeaderText="Amount">
    <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" 
        Wrap="False" />
<HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>


<asp:BoundField DataField ="Status" HeaderText="Status">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
</Columns>
<PagerStyle CssClass="pgr" />
<AlternatingRowStyle CssClass="alt" />
<FooterStyle CssClass="ftr" />
</asp:GridView>
</div>
</td>           
 </tr>
  <tr><td colspan="7"></td></tr>
</table>
</fieldset>
</div>
</ContentTemplate>
  <Triggers>
             <asp:PostBackTrigger ControlID ="ImgBtnExcel" />
             </Triggers>
</asp:UpdatePanel>
</asp:Content>

