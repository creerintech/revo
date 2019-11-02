<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialTransferOutSideDetails.aspx.cs" Inherits="Reports_MaterialTransferOutSideDetails" Title="Material Transfer OutSide Details" %>
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
    Material Transfer OutSide Details   
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
<asp:TextBox ID="TxtFromDate" runat="server" CssClass="TextBox" Width="90px" >
</asp:TextBox>
<asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CssClass="Imagebutton"
ImageUrl="~/Images/Icon/DateSelector.png" />
<ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
PopupButtonID="ImageButton1" TargetControlID="TxtFromDate"></ajax:CalendarExtender>&nbsp;<ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" Format="dd-MMM-yyyy"
PopupButtonID="ImageButton2" TargetControlID="TxtToDate"></ajax:CalendarExtender>
</td>
<td class="Label"> To:</td>
<td>
<asp:TextBox ID="TxtToDate" runat="server" CssClass="TextBox" Width="90px" ></asp:TextBox>
<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" 
CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
</td>
<td class="Label">&nbsp;Transfer No:</td>
<td>
<asp:DropDownList ID="ddlTransferNo" runat="server" Width="162px">
    </asp:DropDownList>
</td>
<td class="Label">Employee :</td>
<td >
    <asp:DropDownList ID="ddlEmployee" runat="server" Width="162px">
    </asp:DropDownList>
    </td>
</tr>
<tr>
<td class="Label"> Category : </td>
<td>
<asp:DropDownList ID="ddlCategory" runat="server" Width="162px" ></asp:DropDownList>
</td>
<td class="Label">Items :
</td>
<td><asp:DropDownList ID="ddlItems" runat="server" Width="162px" ></asp:DropDownList></td>

<td class="Label">From Location :</td>
<td><asp:DropDownList ID="ddlFromLocation" runat="server" Width="162px" ></asp:DropDownList></td>
<td class="Label">
    To Location :</td>
<td>
<asp:DropDownList ID="ddlToLocation" runat="server" Width="162px" ></asp:DropDownList>
</td>
</tr>
<tr>
<td class="Label"></td>
<td colspan="6" align="left">
<asp:RadioButtonList ID="RdoType" runat="server" CssClass="RadioButton"
CellPadding="25" RepeatDirection="Horizontal" >
<asp:ListItem Selected="True" Text="&nbsp;All&nbsp;&nbsp;" 
Value="A"></asp:ListItem>
<asp:ListItem  Text="&nbsp;Transfer IN&nbsp;&nbsp;" 
Value="I"></asp:ListItem>
<asp:ListItem Text="&nbsp;Transfer OUT"
Value="O"></asp:ListItem>
</asp:RadioButtonList>
</td>
<td align="center">
    &nbsp;<asp:Button ID="BtnShow" runat="server" CssClass="button" 
TabIndex="4" Text="Show" ValidationGroup="Add" 
ToolTip="Show Details" onclick="BtnShow_Click"  />   
<asp:Button ID="BtnCancel" runat="server" CssClass="button" 
TabIndex="5" Text="Cancel"  
ToolTip="Clear The Details" onclick="BtnCancel_Click"  />
</td>
</tr>
<tr>
<td colspan="7" align="center"> <asp:Label ID="lblCount" CssClass="SubTitle"  runat="server"></asp:Label></td>

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
CaptionAlign="Top" CssClass="mGrid" Width="100%" ShowFooter="True"
        onpageindexchanging="GridDetails_PageIndexChanging" PageSize="30" 
        onrowdatabound="GridDetails_RowDataBound" >
<Columns>
<asp:TemplateField HeaderText="Sr.No">
<ItemTemplate>
<asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'>
</asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" Width="6%" />
</asp:TemplateField>
<asp:BoundField DataField="TransferNo" HeaderText="Transfer No">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="50px" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="50px" />
</asp:BoundField>
<asp:BoundField DataField="Date" HeaderText="Date">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField ="type" HeaderText="Transfer IN / OUT">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>


<asp:BoundField DataField ="Employee" HeaderText="Employee">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField ="Category" HeaderText="Category">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Particular">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField="Unit" HeaderText="Unit">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField="FromLocation" HeaderText="From Location">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField="ToLocation" HeaderText="To Location">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField="Qty" HeaderText="Quantity">
    <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField="rate1" HeaderText="Rate">
    <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" Wrap="false" />
<HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Right" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>

<asp:BoundField DataField="rate" HeaderText="Amount">
    <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
<HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Right" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField="Notes" HeaderText="Remark">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
</Columns>
<PagerStyle CssClass="pgr" />
<AlternatingRowStyle CssClass="alt" />
<FooterStyle CssClass="ftr" />
</asp:GridView>
</div>
</td>           
 </tr>
  <tr><td colspan="8"></td></tr>
</table>
</fieldset>
</ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID ="ImgBtnExcel" />
</Triggers>
</asp:UpdatePanel>
</asp:Content>

