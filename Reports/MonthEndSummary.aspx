<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="MonthEndSummary.aspx.cs" Inherits="Reports_MonthEndSummary" Title="Month End Summary" %>
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
   Month End Summary   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<fieldset id="F1" runat="server" class="FieldSet">
<table width="100%" cellspacing="7">
<tr><td colspan="8"></td></tr>
<tr>
<td class="Label">For Month : </td>
<td>
<asp:TextBox ID="TxtForMonth" runat="server" CssClass="TextBox" Width="90px" 
      ></asp:TextBox>
<asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CssClass="Imagebutton"
ImageUrl="~/Images/Icon/DateSelector.png" />
<ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="MMM-yyyy"
PopupButtonID="ImageButton1" TargetControlID="TxtForMonth"></ajax:CalendarExtender>
</td>
<td class="Label">
    Requisition No:</td>
<td>
<asp:DropDownList ID="ddlRequisitionNo" runat="server" Width="152px" ></asp:DropDownList>
</td>
<td class="Label">
    Inward No :</td>
<td>
<asp:DropDownList ID="ddlInwardNo" runat="server" Width="152px" ></asp:DropDownList>
</td>

<td class="Label">
</td>
<td align="left">
    &nbsp;<asp:Button ID="BtnShow" runat="server" CssClass="button" 
TabIndex="4" Text="Show" ValidationGroup="Add" 
ToolTip="Show Details" onclick="BtnShow_Click"   />   
<asp:Button ID="BtnCancel" runat="server" CssClass="button" 
TabIndex="5" Text="Cancel"  
ToolTip="Clear The Details" onclick="BtnCancel_Click"  />
</td>



</tr>

<tr>
<td colspan="7" align="center"> <asp:Label ID="lblCount" CssClass="SubTitle" runat="server"></asp:Label></td></td>
<td align="right">
<asp:ImageButton ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/Print-Icon.png" OnClientClick="javascript:CallPrint('DivGridDetails')" />
<asp:ImageButton ID="ImgBtnExcel" runat="server" 
        ImageUrl="~/Images/Icon/excel-icon.png" onclick="ImgBtnExcel_Click"  />
</td>
</tr>
<tr>
<td colspan="8">
<div id="DivGridDetails" class="ScrollableDiv_FixHeightWidth4">
<asp:GridView ID="GridDetails" runat="server" AllowPaging="false"
AutoGenerateColumns="false" BackColor="White" BorderColor="#CCCCC"
BorderStyle="None" BorderWidth="1px" ForeColor="Black" GridLines="Horizontal"
CaptionAlign="Top" CssClass="mGrid" Width="100%" PageSize="30" 
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
<asp:BoundField DataField="RequisitionNo" HeaderText="Requisition No">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="50px" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="50px" />
</asp:BoundField>
<asp:BoundField DataField="RequisitionDate" HeaderText="Date">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField ="Cafeteria" HeaderText="Location">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField ="AssignNo" HeaderText="Assign No">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
</asp:BoundField>
<asp:BoundField DataField="AssignDate" HeaderText="Date">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="30px"/>
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" width="30px"/>
</asp:BoundField>
<asp:BoundField DataField="CategoryName" HeaderText="Category">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="30px"/>
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" width="30px"/>
</asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Particular">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="30px"/>
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" width="30px"/>
</asp:BoundField>

<asp:BoundField DataField="Description" HeaderText="Description">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="30px"/>
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" width="30px"/>
</asp:BoundField>

<asp:BoundField DataField="Remark" HeaderText="Remark">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="30px"/>
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" width="30px"/>
</asp:BoundField>

<asp:BoundField DataField="Unit" HeaderText="Unit">
<HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
<ItemStyle HorizontalAlign ="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
</asp:BoundField>

<asp:BoundField DataField="ReqQty" HeaderText="Requisition Qty">
<HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
<ItemStyle HorizontalAlign ="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
</asp:BoundField>
<asp:BoundField DataField="AssignQty" HeaderText="Assign Qty">
<HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
<ItemStyle HorizontalAlign ="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
</asp:BoundField>
<asp:BoundField DataField="PendingQty" HeaderText="Pending Qty">
<HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
<ItemStyle HorizontalAlign ="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
</asp:BoundField>
<asp:BoundField DataField="Status" HeaderText="Status">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" width="30px"/>
<ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" width="30px"/>
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

