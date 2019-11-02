<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="InwardRegisterDetails.aspx.cs" Inherits="Reports_InwardRegisterDetails" Title="Material Inward Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolScriptManager1" runat="server">
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
    Material Inward Details     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="UPEntry" runat="server">
        <ContentTemplate>
            <fieldset id="F1" runat="server" class="FieldSet">
                <table width="100%" cellspacing="6">
                    <tr>
                        <td class="Label">
                            <asp:CheckBox ID="ChkFrmDate" runat="server" AutoPostBack="True" CssClass="CheckBox"
                                Text=" From Date :" oncheckedchanged="ChkFrmDate_CheckedChanged"  />
                        </td>
                        <td align="left" class="Label1" >
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Width="80px" 
                               ></asp:TextBox>
                            <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd-MMM-yyyy"
                                PopupButtonID="ImageButton212" TargetControlID="txtFromDate" />
                            <asp:ImageButton ID="ImageButton212" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="1" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To:
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" Width="80px" 
                               ></asp:TextBox>
                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd-MMM-yyyy"
                                PopupButtonID="ImageButton1" TargetControlID="txtToDate" />
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="1" />
                        </td>
                         <td class="Label">
                             Supplier :
                        </td>
                        <td>
                                                 
                              <ajax:ComboBox ID="ddlSupp" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="180px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
    
                        </td>
                        <td class="Label">Inward No : </td>
                        <td >
                       
                          <ajax:ComboBox ID="ddlInwardNo" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="180px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Label">
                            Category :
                        </td>
                        <td>
                           
                            
                             <ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="180px" CssClass="CustomComboBoxStyle"
    AutoPostBack="true" onselectedindexchanged="ddlCategory_SelectedIndexChanged"></ajax:ComboBox>
                        </td>
                         <td class="Label">Sub Category :</td>
                      <td>
                         <ajax:ComboBox ID="ddlsubcategory" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="180px" CssClass="CustomComboBoxStyle"
    AutoPostBack="true" onselectedindexchanged="ddlsubcategory_SelectedIndexChanged"></ajax:ComboBox>
                      </td>
                        <td class="Label">
                            Items :
                        </td>
                        <td>
                        
                            
                              <ajax:ComboBox ID="ddlItemName" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="180px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
    
                        </td>
                     
                      
                    </tr>
                   
                  
                    <tr>
                       <td class="Label" >Inward Through :</td>
                        <td>
                        <asp:RadioButtonList ID="RdoType" runat="server" 
                        CellPadding="25" RepeatDirection="Horizontal" >
                        <asp:ListItem Selected="True" Text="&nbsp;All&nbsp;&nbsp;&nbsp;&nbsp;" 
                        Value="2"></asp:ListItem>
                        <asp:ListItem  Text="&nbsp;Cash&nbsp;&nbsp;&nbsp;&nbsp;" 
                        Value="0"></asp:ListItem>
                        <asp:ListItem Text="&nbsp;Credit&nbsp;&nbsp;&nbsp;"
                        Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                        
                        </td>
                        
                        <td>
                            &nbsp;</td>
                        <td align="right" colspan="4">
                             <asp:Button ID="BtnShow" runat="server" CssClass="button" 
                             TabIndex="18" Text="Show" ToolTip="Show Details" 
                                ValidationGroup="Add" onclick="BtnShow_Click" />
                            <asp:Button ID="BtnCancel" runat="server" CssClass="button" TabIndex="18" Text="Cancel"
                                ToolTip="Clear The Details" onclick="BtnCancel_Click" /></td>
                    </tr>
                   
                  
                    <tr>
                        <td colspan="5" align="center">
                            <asp:Label ID="lblCount" runat="server" CssClass="SubTitle"></asp:Label>
                        </td>
                        <td colspan="1" align="right">
                            <asp:ImageButton ID="ImgBtnPrint" runat="server" OnClientClick="javascript:CallPrint('divPrint')"
                                ImageUrl="~/Images/Icon/Print-Icon.png" ToolTip="Print Report" />
                            <asp:ImageButton ID="ImgBtnExport" runat="server" ImageUrl="~/Images/Icon/excel-icon.png"
                                ToolTip="Export To Excel" onclick="ImgBtnExport_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <div id="divPrint" class="ScrollableDiv_FixHeightWidth4">
                                <asp:GridView ID="GridInwordDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="mGrid" BackColor="White" BorderColor="#CCCCCC" 
                                    BorderStyle="None" BorderWidth="1px" AllowPaging="false"
                                    CellPadding="4" ForeColor="Black" PageSize="100" 
                                    ShowFooter="True" onrowdatabound="GridInwordDetails_RowDataBound" 
                                    onpageindexchanging="GridInwordDetails_PageIndexChanging">
                                    <Columns>
                                        <%-- <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgBtnDelete" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                CommandArgument='<%# Eval("#") %>' CommandName="Delete" 
                                ImageUrl="~/Images/Icon/GridDelete.png" TabIndex="19" ToolTip="Delete Record" />
                            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                ConfirmText="Would You Like To Delete The Record..!" 
                                TargetControlID="ImgBtnDelete">
                            </ajax:ConfirmButtonExtender>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                    </asp:TemplateField>--%>
                                       <%-- <asp:BoundField DataField="#" HeaderText="#" Visible="false" />--%>
                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Wrap="True" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="InwardNo" HeaderText="Inw.No.">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InwardDate" HeaderText="Inw.Date">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SuplierName" HeaderText="Supplier">
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" ForeColor="White" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        
                                          <asp:BoundField DataField="PONO" HeaderText="PO NO.">
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" ForeColor="White" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        
                                          <asp:BoundField DataField="type" HeaderText="Inward Type">
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" ForeColor="White" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        
                                        
                                        <asp:BoundField DataField="ItemName" HeaderText="Item">
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" ForeColor="White" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Unit" HeaderText="Unit">
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" ForeColor="White" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="OrderQty" HeaderText="Ord.Qty">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InwardQty" HeaderText="Inw.Qty">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" />
                                        </asp:BoundField>
                                        
                                          <asp:BoundField DataField="PendingQty" HeaderText="Pen.Qty">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="InwardRate" HeaderText="Inw.Rate">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" CssClass="Display_None"/>
                                        </asp:BoundField>
                                           <asp:BoundField DataField="PORate" HeaderText="PORate">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" CssClass="Display_None"/>
                                        </asp:BoundField>
                                           <asp:BoundField DataField="Diffrence" HeaderText="Diffrence">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Amount" HeaderText="Amount">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="TaxPer" HeaderText="Tax(%)">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="TaxAmount" HeaderText="Tax(Rs/-)">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DiscPer" HeaderText="Disc(%)">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="DiscAmt" HeaderText="Disc(Rs/-)">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="NetAmount" HeaderText="NetAmount">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" />
                                        </asp:BoundField>
                                      
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <AlternatingRowStyle CssClass="alt" />
                                    <FooterStyle CssClass="ftr" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr><td colspan="3" align="left">* Note: Red Line Shows Variance in Rate</td><td colspan="3"></td></tr>
                </table>
            </fieldset>
        </ContentTemplate>
        <Triggers>
    <asp:PostBackTrigger ControlID="ImgBtnExport" />
</Triggers>
    </asp:UpdatePanel>
</asp:Content>

