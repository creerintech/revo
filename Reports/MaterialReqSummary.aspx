<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialReqSummary.aspx.cs" Inherits="Reports_MaterialReqSummary" Title="Material Indent Summary" %>
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
    Material Indent Summary       
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
  <fieldset id="F1" runat="server" class="FieldSet">
   <table width="100%" cellspacing="7">
    <tr><td colspan="8"></td></tr>
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
        Indent No:</td>
    <td>
    <%--    <asp:DropDownList ID="ddlRequisitionNo" runat="server" Width="152px" ></asp:DropDownList>--%>
    
    <ajax:ComboBox ID="ddlRequisitionNo" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="172px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
    </td>
    <td class="Label">
        Location :</td>
    <td>
    <%--   <asp:DropDownList ID="ddlTemplateNo" runat="server" Width="152px" ></asp:DropDownList>--%>
     <ajax:ComboBox ID="ddlTemplateNo" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="172px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
    </td>
    </tr>
    <tr>
    <td class="Label">
        Employee :</td>
    <td>
    <%--    <asp:DropDownList ID="ddlEmployee" runat="server" Width="152px" ></asp:DropDownList>--%>
         <ajax:ComboBox ID="ddlEmployee" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="152px" CssClass="CustomComboBoxStyle" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"></ajax:ComboBox>
    </td>
    
    <td colspan="5">
      <asp:RadioButtonList ID="RdoType" runat="server"
            CellPadding="25"  RepeatDirection="Horizontal" CssClass="RadioButton">
            <asp:ListItem Selected="True" Text="All&nbsp;&nbsp;" 
            Value="All"></asp:ListItem>
            <asp:ListItem Text="Generated&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" 
                    Value="Generated"></asp:ListItem>
            <asp:ListItem Text="Approved&nbsp;&nbsp;&nbsp;" Value="Approved"></asp:ListItem>
            <asp:ListItem Text="Authorised" Value="Authorised"></asp:ListItem>
            </asp:RadioButtonList>
    </td>
    </tr>
        
    <tr>
    <td colspan="7"></td>
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
    <asp:GridView ID="GridDetails" runat="server"
    AutoGenerateColumns="False" BackColor="White" BorderColor="#0CCCCC"
    BorderStyle="None" BorderWidth="1px" ForeColor="Black" GridLines="Horizontal"
    CaptionAlign="Top" CssClass="mGrid" Width="100%" PageSize="50" 
    onpageindexchanging="GridDetails_PageIndexChanging" ShowFooter="false" 
    onrowdatabound="GridDetails_RowDataBound">
    <Columns>
    <asp:TemplateField HeaderText="Sr.No">
    <ItemTemplate>
    <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'>
    </asp:Label>
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" Width="6%" />
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="">                        
    <ItemTemplate>
    <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RS"%>&PDFFlag=<%="PDF"%>' target="_blank">
    <asp:Image ID="Image1" runat="server" 
    ImageUrl="~/Images/New Icon/pdfImg.png" TabIndex="29" 
    ToolTip="PDF of Request Register" />
    </a>
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
    Width="6%" />
    </asp:TemplateField>
                         
                         
    <asp:BoundField DataField="RequisitionNo" HeaderText="Indent No">
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
    <asp:BoundField DataField ="EmpName" HeaderText="Employee Name">
        <FooterStyle Font-Bold="True" ForeColor="White" />
    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
    </asp:BoundField>
    <asp:BoundField DataField="Amount" HeaderText="Net Total" Visible="false">
    <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
    <ItemStyle HorizontalAlign ="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
    </asp:BoundField>
    <asp:BoundField DataField="ReqStatus" HeaderText="Status">
        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
    <ItemStyle HorizontalAlign ="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
    </asp:BoundField>
    <asp:BoundField DataField="GeneratedTime" HeaderText="GeneratedTime">
    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
    <ItemStyle HorizontalAlign ="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
    </asp:BoundField>
    <asp:BoundField DataField="ApprovedTime" HeaderText="ApprovedTime">
    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
    <ItemStyle HorizontalAlign ="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
    </asp:BoundField>
    <asp:BoundField DataField="AuthorizedTime" HeaderText="AuthorizedTime">
    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
    <ItemStyle HorizontalAlign ="Right" VerticalAlign="Middle" Wrap="false" width="30px"/>
    </asp:BoundField>
    
       <asp:BoundField DataField="PONo" HeaderText="Purchase Order No.">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
    <ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false"/>
    </asp:BoundField>
    
       <asp:BoundField DataField="SuplierName" HeaderText="Supplier">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
    <ItemStyle HorizontalAlign ="Left" VerticalAlign="Middle" Wrap="false" />
    </asp:BoundField>
    
       <asp:BoundField DataField="SuplierInfo" HeaderText="Supplier Information">
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
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

 <tr><td colspan="8">* Note: Red Line Shows Item Cancelled In Indent</td></tr>
  <tr><td colspan="8"></td></tr>
</table>
</fieldset>
</ContentTemplate>
  <Triggers>
             <asp:PostBackTrigger ControlID ="ImgBtnExcel" />
             </Triggers>
</asp:UpdatePanel>
</asp:Content>

