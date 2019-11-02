<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialReqDetails.aspx.cs" Inherits="Reports_MaterialReqSummary" Title="Material Indent Details" %>
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
    Material Indent Details      
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<fieldset id="F1" runat="server" class="FieldSet">
<table width="80%" cellspacing="6">

    <tr>
    <td class="Label"> <asp:CheckBox ID="ChkFromDate" runat="server" 
            AutoPostBack="true" CssClass="CheckBox" Text="  From :" 
            oncheckedchanged="ChkFromDate_CheckedChanged" /></td>
    <td>
    <asp:TextBox ID="TxtFromDate" runat="server" CssClass="TextBox" Width="90px"></asp:TextBox>
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
     <%--<asp:DropDownList ID="ddlRequisitionNo" runat="server" Width="190px" ></asp:DropDownList>--%>
        <ajax:ComboBox ID="ddlRequisitionNo" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="190px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
    
    </td>
</tr>
    <tr>
    <td class="Label"> Category :</td>
    <td>
     <%--<asp:DropDownList ID="ddlCategory" runat="server" Width="190px" ></asp:DropDownList>--%>
    <ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown" 
    AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" AutoPostBack="true"
    ItemInsertLocation="Append" Width="190px" CssClass="CustomComboBoxStyle"  onselectedindexchanged="ddlCategory_SelectedIndexChanged" ></ajax:ComboBox>
    </td>
    <td class="Label"> SubCategory :</td>
    <td>
    <%--<asp:DropDownList ID="ddlSubCategory" runat="server" Width="190px" ></asp:DropDownList>--%>
    <ajax:ComboBox ID="ddlSubCategory" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="190px" CssClass="CustomComboBoxStyle" AutoPostBack="true" onselectedindexchanged="ddlSubCategory_SelectedIndexChanged"></ajax:ComboBox>
    </td>
    <td class="Label">
    Size :</td>
    <td>
    <%--<asp:DropDownList ID="ddlSize" runat="server" Width="190px"></asp:DropDownList>--%>
    <ajax:ComboBox ID="ddlSize" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="190px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
    </td>
</tr>
    <tr>
    <td class="Label">Items :
    </td>
    <td>
    <%--<asp:DropDownList ID="ddlItems" runat="server" Width="190px" ></asp:DropDownList>--%>
     <ajax:ComboBox ID="ddlItems" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="190px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
    </td>
    <td class="Label">
    Unit :</td>
    <td>
<%--    <asp:DropDownList ID="ddlUnit" runat="server" Width="190px">
    </asp:DropDownList>--%>
    
     <ajax:ComboBox ID="ddlUnit" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="190px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
    
    </td>
    <td class="Label">
    Employee :</td>
    <td>
<%--    <asp:DropDownList ID="ddlEmployee" runat="server" Width="190px">
    </asp:DropDownList>--%>
    
      <ajax:ComboBox ID="ddlEmployee" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="190px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
    </td>
    </tr>
    <tr>
    <td class="Label">
    Location :</td>
    <td >
     <%--<asp:DropDownList ID="ddlTemplateNo" runat="server" Width="190px" ></asp:DropDownList>--%>
    <ajax:ComboBox ID="ddlTemplateNo" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="190px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
    </td>
    <td>&nbsp;</td>
      <td colspan="3" align="left">
        <asp:RadioButtonList ID="RdoType" runat="server"
        CellPadding="25"  RepeatDirection="Horizontal" CssClass="RadioButton">
        <asp:ListItem Selected="True" Text="All&nbsp;&nbsp;" 
        Value="All"></asp:ListItem>
        <asp:ListItem Text="Generated&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" 
                Value="Generated"></asp:ListItem>
        <asp:ListItem Text="Approved&nbsp;&nbsp;&nbsp;" Value="Approved"></asp:ListItem>
        <asp:ListItem Text="Authorised&nbsp;&nbsp;&nbsp;" Value="Authorised"></asp:ListItem>
        <asp:ListItem Text="Balance" Value="Balance"></asp:ListItem>
        </asp:RadioButtonList>
        </td>
    </tr>
   
    <tr>
        <td align="right" colspan="6">
            &nbsp;<asp:Button ID="BtnShow" runat="server" CssClass="button" 
                onclick="BtnShow_Click" TabIndex="4" Text="Show" ToolTip="Show Details" 
                ValidationGroup="Add" />
            <asp:Button ID="BtnCancel" runat="server" CssClass="button" 
                onclick="BtnCancel_Click" TabIndex="5" Text="Cancel" 
                ToolTip="Clear The Details" />
        </td>
        
    </tr>
    <tr>
        <td align="center" colspan="5">
            <asp:Label ID="lblCount" runat="server" CssClass="SubTitle"></asp:Label>
        </td>
        <td align="right">
            <asp:ImageButton ID="ImgBtnPrint" runat="server" 
                ImageUrl="~/Images/Icon/Print-Icon.png" 
                OnClientClick="javascript:CallPrint('DivGridDetails')" TabIndex="6" 
                ToolTip="Print Report" />
            <asp:ImageButton ID="ImgBtnExcel" runat="server" 
                ImageUrl="~/Images/Icon/excel-icon.png" onclick="ImgBtnExcel_Click" />
        </td>
    <tr>
        <td colspan="6">
            <div ID="DivGridDetails" class="ScrollableDiv_FixHeightWidth4">
                <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#0CCCCC" BorderStyle="None" BorderWidth="1px" 
                    CaptionAlign="Top" CssClass="mGrid" ForeColor="Black" GridLines="Horizontal" 
                    onpageindexchanging="GridDetails_PageIndexChanging" 
                    onrowdatabound="GridDetails_RowDataBound" PageSize="100" ShowFooter="false" 
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No">
                            <ItemTemplate>
                                <asp:Label ID="lblSrNo" runat="server" Text="<%#Container.DataItemIndex+1 %>">
                                </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" 
                                Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&amp;Flag=<%="RS"%>&amp;PDFFlag=<%="PDF"%>' 
                                    target="_blank">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png" 
                                    TabIndex="29" ToolTip="PDF of Request Register" />
                                </a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" 
                                Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="RequisitionNo" HeaderText="Indent No">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" width="50px" 
                                Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" width="50px" 
                                Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Date" HeaderText="Date">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Employee" HeaderText="Employee">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Suplier" HeaderText="Suplier" Visible="false">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Cafeteria" HeaderText="Location">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Category" HeaderText="Category">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" width="30px" 
                                Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" width="30px" 
                                Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SubCategory" HeaderText="SubCategory">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" width="30px" 
                                Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" width="30px" 
                                Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Items" HeaderText="Item">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" width="30px" 
                                Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" width="30px" 
                                Wrap="false" />
                        </asp:BoundField>
                        
                          <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" width="30px" 
                                Wrap="true" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" width="30px" 
                                Wrap="true" />
                        </asp:BoundField>
                        
                          <asp:BoundField DataField="RemarkForPO" HeaderText="Remark">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" width="30px" 
                                Wrap="true" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" width="30px" 
                                Wrap="true" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="Unit" HeaderText="Unit">
                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MinStockLevel" HeaderText="Min Stocl Level">
                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Qty" HeaderText="Req. Qty">
                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Rate" HeaderText="Rate" Visible="false">
                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" width="30px" 
                                Wrap="false" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" width="30px" 
                                Wrap="false" />
                            <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                                Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Amount" HeaderText="Total Amount" Visible="false">
                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" width="30px" 
                                Wrap="false" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" width="30px" 
                                Wrap="false" />
                            <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PONo" HeaderText="Purchase Order No.">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="POQty" HeaderText="P.O. Qty.">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SuplierName" HeaderText="Supplier Name">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SuplierInfo" HeaderText="Supplier Details">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
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
    <tr>
        <td colspan="6">
            * Note: Red Line Shows Item Cancelled In Indent</td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td align="right" class="Label" colspan="6">
            Net Total :
            <asp:Label ID="lblNetAmount" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
        </td>
    </tr>
 
</table>
</fieldset>
</ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID ="ImgBtnExcel" />
    </Triggers>
</asp:UpdatePanel>
</asp:Content>

