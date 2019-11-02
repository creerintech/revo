<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="PendingRequisitionReport.aspx.cs" Inherits="Reports_PendingRequisitionReport"
    Title="Indent Pending Quantity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div id="progressBackgroundFilter">
                    </div>
                    <div id="processMessage">
                        <center>
                            <span class="SubTitle">Loading....!!! </span>
                        </center>
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif"
                            Height="20px" Width="120px" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="Server">
    Indent Status Details (Item Wise)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset id="F1" runat="server" class="FieldSet">
                <table width="80%" cellspacing="7">
                    <tr>
                        <td colspan="8">
                        </td>
                    </tr>
                    <tr>
                        <td class="Label">
                            <asp:CheckBox ID="ChkFromDate" runat="server" AutoPostBack="true" CssClass="CheckBox"
                                Text="  From :" OnCheckedChanged="ChkFromDate_CheckedChanged1" />
                        </td>
                        <td>
                            <asp:TextBox ID="TxtFromDate" runat="server" CssClass="TextBox" Width="90px"> 
                            </asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CssClass="Imagebutton"
                                ImageUrl="~/Images/Icon/DateSelector.png" />
                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                PopupButtonID="ImageButton1" TargetControlID="TxtFromDate">
                            </ajax:CalendarExtender>
                        </td>
                        <td class="Label">
                            To:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtToDate" runat="server" CssClass="TextBox" Width="90px"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CssClass="Imagebutton"
                                ImageUrl="~/Images/Icon/DateSelector.png" />
                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" Format="dd-MMM-yyyy"
                                PopupButtonID="ImageButton2" TargetControlID="TxtToDate">
                            </ajax:CalendarExtender>
                        </td>
                        <td class="Label">
                            Indent No:
                        </td>
                        <td>                          
                            <ajax:ComboBox ID="ddlRequisitionNo" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append" Width="150px"
                                CssClass="CustomComboBoxStyle">
                            </ajax:ComboBox>
                        </td>
                        <td class="Label" nowrap="nowrap">
                            Req.By :
                        </td>
                        <td>                          
                            <ajax:ComboBox ID="ddlRequisitnBy" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append" Width="150px"
                                CssClass="CustomComboBoxStyle">
                            </ajax:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Label" nowrap="nowrap">
                            Location :
                        </td>
                        <td>                         
                            <ajax:ComboBox ID="ddlLocation" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append" Width="150px"
                                CssClass="CustomComboBoxStyle">
                            </ajax:ComboBox>
                        </td>
                        <td class="Label" nowrap="nowrap">
                            Issue No :
                        </td>
                        <td>                            
                            <ajax:ComboBox ID="ddlIssueNo" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append" Width="150px"
                                CssClass="CustomComboBoxStyle">
                            </ajax:ComboBox>
                        </td>
                        <td class="Label">
                            Issue By :
                        </td>
                        <td>                           
                            <ajax:ComboBox ID="ddlIssueBy" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append" Width="150px"
                                CssClass="CustomComboBoxStyle">
                            </ajax:ComboBox>
                        </td>
                        <td class="Label" nowrap="nowrap">
                            Category :
                        </td>
                        <td>                           
                            <ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append" Width="150px"
                                CssClass="CustomComboBoxStyle" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                            </ajax:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Label">
                            Item :
                        </td>
                        <td>                           
                            <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append" Width="150px"
                                CssClass="CustomComboBoxStyle">
                            </ajax:ComboBox>
                        </td>
                        <td class="Label">
                            Unit :
                        </td>
                        <td>                           
                            <ajax:ComboBox ID="ddlUnit" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append" Width="150px"
                                CssClass="CustomComboBoxStyle">
                            </ajax:ComboBox>
                        </td>
                        <td align="right" colspan="4">
                            <asp:Button ID="BtnShow" runat="server" CssClass="button" OnClick="BtnShow_Click"
                                TabIndex="4" Text="Show" ToolTip="Show Details" ValidationGroup="Add" />
                            <asp:Button ID="BtnCancel" runat="server" CssClass="button" OnClick="BtnCancel_Click"
                                TabIndex="5" Text="Cancel" ToolTip="Clear The Details" />
                        </td>                      
                    </tr>
                    <tr>
                        <td colspan="7">
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/Print-Icon.png"
                                OnClientClick="javascript:CallPrint('DivGridDetails')" />
                            <asp:ImageButton ID="ImgBtnExcel" runat="server" ImageUrl="~/Images/Icon/excel-icon.png"
                                OnClick="ImgBtnExcel_Click1" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" align="center">
                            <asp:Label ID="lblCount" runat="server" CssClass="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <div id="DivGridDetails" class="ScrollableDiv_FixHeightWidth4">
                                <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#0CCCCC" BorderStyle="None" BorderWidth="1px" ForeColor="Black"
                                    GridLines="Horizontal" CaptionAlign="Top" CssClass="mGrid" Width="100%" ShowFooter="true"
                                    OnRowDataBound="GridDetails_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RequisitionNo" HeaderText="Indent No">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" Width="50px" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ReqDate" HeaderText="Indent Date">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ReqLocation" HeaderText="Location">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IssuseNo" HeaderText="Issuse No">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IssuseDate" HeaderText="Issuse Date">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IssuseLocation" HeaderText="Location">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ReqBy" HeaderText="IND. By">
                                            <FooterStyle Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IssuBy" HeaderText="Issuse By">
                                            <FooterStyle Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CategoryName" HeaderText="Category">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ItemName" HeaderText="Item">
                                            <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                            <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RemarkForPO" HeaderText="Remark">
                                            <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Unit" HeaderText="Unit">
                                            <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" Wrap="false" />
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Qty" HeaderText="Indent Qty">
                                            <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" Wrap="false" />
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OutwardQty" HeaderText="Issuse Qty">
                                            <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PendingQty" HeaderText="Pending">
                                            <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Rate" HeaderText="Issuse Rate">
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount">
                                            <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false" Width="30px" />
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
            <asp:PostBackTrigger ControlID="ImgBtnExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
