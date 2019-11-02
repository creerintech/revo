<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="MaterialInwardRegister.aspx.cs" Inherits="Reports_MaterialInwardRegister"
    Title="  Material Inward Summary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="Server">
    Material Inward Summary     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UPEntry" runat="server">
        <ContentTemplate>
            <fieldset id="F1" runat="server" class="FieldSet">
                <table width="100%" cellspacing="8">
                <tr><td colspan="6"></td></tr>
                    <tr>
                        <td class="Label">
                            <asp:CheckBox ID="ChkFrmDate" runat="server" AutoPostBack="True" CssClass="CheckBox"
                                Text=" From Date :" OnCheckedChanged="ChkFrmDate_CheckedChanged" />&nbsp;
                        </td>
                        <td align="left" class="Label1" colspan="3">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Width="90px" 
                               ></asp:TextBox>
                            <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd-MMM-yyyy"
                                PopupButtonID="ImageButton212" TargetControlID="txtFromDate" />
                            <asp:ImageButton ID="ImageButton212" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="1" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To :
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" Width="90px" 
                               ></asp:TextBox>
                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd-MMM-yyyy"
                                PopupButtonID="ImageButton1" TargetControlID="txtToDate" />
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="1" />
                        </td>
                          <td class="Label">
                              Inward No :
                        </td>
                        <td>
                           
                              <ajax:ComboBox ID="ddlInwardNo" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="160px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
                        </td>
                        <td class="Label">
                            Supplier Name :
                        </td>
                        <td>
                      
                               <ajax:ComboBox ID="ddlSuppName" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="160px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
                        </td>
                    </tr>
                  
                   
                    <tr>
                    <td class="Label">Inward Through :</td>
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
                    <td colspan="5" ></td>
                        <td  align="center" >
                            <asp:Button ID="BtnShow" runat="server" CssClass="button" 
                                OnClick="BtnShow_Click" TabIndex="18" Text="Show" ToolTip="Show Details" 
                                ValidationGroup="Add" />
                            <asp:Button ID="BtnCancel" runat="server" CssClass="button" TabIndex="18" Text="Cancel"
                                oolTip="Clear The Details" OnClick="BtnCancel_Click" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td align="center" colspan="6">
                            <asp:Label ID="lblCount" runat="server" CssClass="SubTitle" ></asp:Label>
                        </td>
                        <td colspan="7" align="right">
                            <asp:ImageButton ID="ImgBtnPrint" runat="server" OnClientClick="javascript:CallPrint('divPrint')"
                                ImageUrl="~/Images/Icon/Print-Icon.png" ToolTip="Print Report" />
                            <asp:ImageButton ID="ImgBtnExport" runat="server" ImageUrl="~/Images/Icon/excel-icon.png"
                                ToolTip="Export To Excel" onclick="ImgBtnExport_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <div id="divPrint" class="ScrollableDiv_FixHeightWidth4" ><asp:GridView ID="GridInword" runat="server" AllowPaging="false" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="mGrid" 
                            ForeColor="Black" onpageindexchanging="GridInword_PageIndexChanging" 
                            onrowcommand="GridInword_RowCommand" onrowdatabound="GridInword_RowDataBound" 
                            PageSize="100" ShowFooter="True" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSrNo" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" 
                                        Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageGridEdit" runat="server" 
                                            CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Select" 
                                            ImageUrl="~/Images/New Icon/ViewRecord.png" ToolTip="Click Inward Details" />
                                    </ItemTemplate>
                                    <FooterStyle Wrap="True" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" 
                                        Wrap="False" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="InwardNo" HeaderText="Inward No">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="InwardDate" HeaderText="InwardDate">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SuplierName" HeaderText="Suplier">
                                    <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PONO" HeaderText="PO Number">
                                    <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="type" HeaderText="Cash Mode">
                                    <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SubTotal" HeaderText="Sub Total">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <FooterStyle Font-Bold="true" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Discount" HeaderText="Discount">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <FooterStyle Font-Bold="true" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Vat" HeaderText="VAT">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" />
                                    <FooterStyle Font-Bold="true" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DekhrekhAmt" HeaderText="Dekhrekh">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" />
                                    <FooterStyle Font-Bold="true" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HamaliAmt" HeaderText="Hamali">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" />
                                    <FooterStyle Font-Bold="true" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CESSAmt" HeaderText="CESS">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" />
                                    <FooterStyle Font-Bold="true" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FreightAmt" HeaderText="Freight">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" />
                                    <FooterStyle Font-Bold="true" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PackingAmt" HeaderText="Packing">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" />
                                    <FooterStyle Font-Bold="true" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PostageAmt" HeaderText="Postage">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" />
                                    <FooterStyle Font-Bold="true" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OtherCharges" HeaderText="OtherCharges">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="True" />
                                    <FooterStyle Font-Bold="true" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GrandTotal" HeaderText="Grand Total">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    <FooterStyle Font-Bold="true" ForeColor="White" HorizontalAlign="Right" 
                                        VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                            <FooterStyle CssClass="ftr" />
                        </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    
                    <tr>
                    <td colspan="8" runat="server" id="GRIDDETAILS" >
                     <asp:LinkButton ID="hyl_Hide" runat="server" CssClass="linkButton" 
                            onclick="hyl_Hide_Click" ToolTip="Hide Inward Details" >Hide</asp:LinkButton>      
                    <div id="div1" class="ScrollableDiv_FixHeightWidth4" >
                        
                    <asp:GridView ID="GridInwordDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="mGrid" BackColor="White" BorderColor="#CCCCCC" 
                                    BorderStyle="None" BorderWidth="1px" AllowPaging="false"
                                    CellPadding="4" ForeColor="Black" PageSize="100" 
                                    ShowFooter="True"  >
                                    <Columns>
                                       
                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Wrap="True" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="6%" />
                                        </asp:TemplateField>
                                        
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
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" />
                                        </asp:BoundField>
                                           <asp:BoundField DataField="PORate" HeaderText="PORate">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                                ForeColor="White" />
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
                </table>
            </fieldset>
        </ContentTemplate>
        <Triggers>
    <asp:PostBackTrigger ControlID="ImgBtnExport" />
</Triggers>
    </asp:UpdatePanel>
</asp:Content>
