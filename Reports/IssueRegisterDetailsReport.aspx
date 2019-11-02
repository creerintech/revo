<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="IssueRegisterDetailsReport.aspx.cs" Inherits="Reports_IssueRegisterDetailsReport" Title="IssueRegisterDetailsReport" %>
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
    Issue Register Details Report  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<fieldset id="F1" runat="server" class="FieldSet">
<table style="width: 100%" cellspacing="7">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td class="Label">
            <asp:CheckBox ID="ChkFromDate" runat="server" AutoPostBack="true" CssClass="CheckBox"
            Text="From :" oncheckedchanged="ChkFromDate_CheckedChanged" />
                </td>
            <td>
               <asp:TextBox ID="TxtFromDate" runat="server" CssClass="TextBox"></asp:TextBox>
               <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
               PopupButtonID="ImageButton1" TargetControlID="TxtFromDate"></ajax:CalendarExtender>
               
               
               <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CssClass="Imagebutton"
               ImageUrl="~/Images/Icon/DateSelector.png" />
               </td>
            <td class="Label">
                To: </td>
            <td>
               <asp:TextBox ID="TxtToDate" runat="server" CssClass="TextBox"></asp:TextBox>
               <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" Format="dd-MMM-yyyy"
               PopupButtonID="ImageButton2" TargetControlID="TxtToDate"></ajax:CalendarExtender>
               <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CssClass="Imagebutton"
               ImageUrl="~/Images/Icon/DateSelector.png" />
                             </td>
            <td class="Label">
                M.Issued No: </td>
            <td>
                <asp:DropDownList ID="ddlIssueNo" runat="server" Width="152px">
                </asp:DropDownList>
                             </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>        
        <tr>
         <td class="Label">
               Item :
            <td>
               <asp:DropDownList ID="ddlItem" runat="server" Width="152px"></asp:DropDownList> </td>
             <td class="Label">
                 Employee :</td>
            <td>
               <asp:DropDownList ID="ddlEmp" runat="server" Width="152px"></asp:DropDownList></td>
           
           
            <td class="Label">
                Location :</td>
            <td>
               <asp:DropDownList ID="ddlLocation" runat="server" Width="152px"></asp:DropDownList></td>
                <td>
                    &nbsp;</td>
             <td align="left">
                 &nbsp;<asp:Button ID="BtnShow" runat="server" CssClass="button" 
                    TabIndex="4" Text="Show" ValidationGroup="Add" 
                    ToolTip="Show Details" onclick="BtnShow_Click"  />   
               <asp:Button ID="BtnCancel" runat="server" CssClass="button" 
                    TabIndex="5" Text="Cancel"  
                    ToolTip="Clear The Details" onclick="BtnCancel_Click" />
            </td>
        </tr>
         <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
           
        </tr>       
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblCount" CssClass="SubTitle"  runat="server"></asp:Label></td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td align="right">
                <asp:ImageButton ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/Print-Icon.png" OnClientClick="javascript:CallPrint('divPrint')" />
                <asp:ImageButton ID="ImgBtnExcel" runat="server" 
                    ImageUrl="~/Images/Icon/excel-icon.png" onclick="ImgBtnExcel_Click" />
                </td>
        </tr>
        <tr>
            <td colspan="8">
              <div id="divPrint" class="ScrollableDiv_FixHeightWidth4">
                <asp:GridView ID="GridDetails" runat="server" AllowPaging="false"
                 AutoGenerateColumns="false" BackColor="White" BorderColor="#CCCCC"
                 BorderStyle="None" BorderWidth="1px" ForeColor="Black" GridLines="Horizontal"
                  CaptionAlign="Top" CssClass="mGrid" Width="100%" 
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
                 
                 <asp:BoundField DataField="IssueNo" HeaderText="Issue No">
                 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 </asp:BoundField>
                  <asp:BoundField DataField="IssueDate" HeaderText="Issue Date">
                 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 </asp:BoundField>
                  <asp:BoundField DataField ="Cafeteria" HeaderText="Location">
                 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 </asp:BoundField>
                  <asp:BoundField DataField ="EmpName" HeaderText="EmpName">
                 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 </asp:BoundField>
                 <asp:BoundField DataField="ItemName" HeaderText="Item">
                 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 </asp:BoundField>
                 <asp:BoundField DataField ="Qty" HeaderText="Req.Qty">
                 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 </asp:BoundField>
                 <asp:BoundField DataField ="IssueQty" HeaderText="Issue Qty">
                 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 </asp:BoundField>
                 <asp:BoundField DataField="PendingQty" HeaderText="Pending Qty">
                 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                 <ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
                 </asp:BoundField>
                
                 <asp:BoundField DataField="Notes" HeaderText="Notes">
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
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
 </fieldset>
    
</asp:Content>

