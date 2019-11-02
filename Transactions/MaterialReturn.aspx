<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="MaterialReturn.aspx.cs" Inherits="Transactions_MaterialReturn" Title="Material Return Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
    <ajax:ToolkitScriptManager ID="ToolScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <%--<ProgressTemplate>            
<div id="progressBackgroundFilter"></div>
<div id="processMessage">   
<center><span class="SubTitle">Loading....!!! </span></center>
<asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
</div>
</ProgressTemplate>--%>
            </asp:UpdateProgress>
            Search for Material Return No :
            <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
                Width="292px" AutoPostBack="True" OnTextChanged="TxtSearch_TextChanged">
            </asp:TextBox>
            <div id="divwidth">
            </div>
            <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtSearch"
                CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" ShowOnlyCurrentWordInCompletionListItem="true"
                ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
            </ajax:AutoCompleteExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="Server">
    Material Return
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
        <ContentTemplate>
            <table width="95%">
                <tr>
                    <td>
                        <fieldset class="FieldSet" id="Fieldset8" runat="server">
                            <legend class="legend">Return Details </legend>
                            <table width="100%" cellspacing="2">
                                <tr class="Display_None">
                                    <td class="Label">
                                        Return No :
                                    </td>
                                    <td colspan="">
                                        <asp:TextBox ID="TxtReturnNo" CssClass="TextBox" runat="server" Width="160px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        Prepared By :
                                        <td>
                                            <asp:Label ID="lblPreparedBy" runat="server" CssClass="Label_Dynamic" Text=""></asp:Label>
                                        </td>
                                        <td class="Label">
                                            Return Date :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblReturndate" runat="server" CssClass="Label_Dynamic" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkReturnDebitNote" runat="server" Text="&nbsp;&nbsp;DEBIT TO SUPPLIER"
                                                CssClass="CheckBox" ForeColor="Black" Font-Bold="true" ToolTip="CHECK IF THE RETURN MATERIAL ARE NOT INWARD, THEY PERMANENTLY RETURN TO SUPPLIER." />
                                            <asp:Label runat="server" ID="IDIMP" Text="&nbsp;&nbsp;&nbsp;**" ForeColor="Red"
                                                ToolTip="PLEASE CHECK CAREFULLY..."></asp:Label>
                                        </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset class="FieldSet" id="FS_InwardDtls" runat="server">
                            <legend class="legend">Inward Details </legend>
                            <table width="100%" cellspacing="2">
                                <tr id="Tr1" runat="server">
                                    <td width="13%" class="Label">
                                        Site :
                                    </td>
                                    <td>
                                        <ajax:ComboBox ID="ddlSite" runat="server" DropDownStyle="DropDown" Enabled="false"
                                            AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                            Width="250px" CssClass="CustomComboBoxStyle">
                                        </ajax:ComboBox>
                                    </td>
                                    <td>
                                        Date From :
                                        <asp:TextBox ID="TxtFromDate" runat="server" CssClass="TextBox" Width="90px" TabIndex="3"></asp:TextBox>
                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            PopupButtonID="TxtFromDate" TargetControlID="TxtFromDate">
                                        </ajax:CalendarExtender>
                                        To :
                                        <asp:TextBox ID="TxtToDate" runat="server" CssClass="TextBox" Width="90px" TabIndex="3"></asp:TextBox>
                                        <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                            PopupButtonID="TxtToDate" TargetControlID="TxtToDate">
                                        </ajax:CalendarExtender>
                                        <asp:CompareValidator ID="CMPCALENDER" runat="server" ControlToValidate="TxtFromDate"
                                            ControlToCompare="TxtToDate" Operator="LessThanEqual" Display="Dynamic" Text="CHECK TO DATE."
                                            ForeColor="Red" BackColor="Yellow" ValidationGroup="GETREFRESH"></asp:CompareValidator>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnShowFilter" runat="server" CssClass="button" Text="REFRESH" ValidationGroup="GETREFRESH"
                                            OnClick="BtnShowFilter_Click" />
                                    </td>
                                </tr>
                                <tr id="t1" runat="server">
                                    <td width="13%" class="Label">
                                        Inward No :
                                    </td>
                                    <td>
                                        <ajax:ComboBox ID="ddlInwardNo" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                            AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                            Width="250px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlInwardNo_SelectedIndexChanged">
                                        </ajax:ComboBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        InwardNo :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInwardNo" runat="server" Text="lblInwardNo" CssClass="Label_Dynamic"></asp:Label>
                                    </td>
                                    <td class="Label">
                                        Inward Date :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInwardDate" runat="server" Text="lblInwardDate" CssClass="Label_Dynamic"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        SupplierName :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSuppName" runat="server" Text="lblSuppName" CssClass="Label_Dynamic"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        Purchase No :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblpono" runat="server" CssClass="Label_Dynamic" Text="PONO"></asp:Label>
                                    </td>
                                    <td>
                                        Quotation No.&nbsp;&nbsp; :&nbsp;&nbsp;
                                        <asp:Label ID="lblQuotationNo" runat="server" CssClass="Label_Dynamic" Text="Quotation No"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        Purchase Date :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPODATE" runat="server" CssClass="Label_Dynamic" Text="PODate"></asp:Label>
                                    </td>
                                    <td>
                                        Quotation Date :&nbsp;<asp:Label ID="lblQuotationDate" runat="server" CssClass="Label_Dynamic"
                                            Text="Quotation Date"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <%--
<tr>
<td class="Label">
    &nbsp;</td>
<td>
    &nbsp;</td>
    <td>
        &nbsp;
    </td>
    <td>
        &nbsp;</td>
    <td>
    </td>
</tr>--%>
                <%--
<tr>
<td class="Label">
&nbsp;</td>
    <td>
        &nbsp;</td>
    <td>
    </td>
    <td>
    </td>
</tr>
--%>
            </table>
            <tr>
                <td>
                    <fieldset id="Fieldset2" runat="server" class="FieldSet">
                        <legend class="legend">Material Details</legend>
                        <div class="ScrollableDiv_FixHeightWidth4">
                            <asp:GridView ID="GrdInward" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#0CCCCC" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" CssClass="mGrid" ForeColor="Black" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="#" HeaderText="#">
                                        <HeaderStyle CssClass="Display_None" Wrap="False" />
                                        <ItemStyle CssClass="Display_None" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InwardNo" HeaderText="Inward No">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemId" HeaderText="ItemId">
                                        <HeaderStyle CssClass="Display_None" Wrap="False" />
                                        <ItemStyle CssClass="Display_None" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Code">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Particular">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UnitId" HeaderText="UnitID">
                                        <HeaderStyle CssClass="Display_None" Wrap="False" />
                                        <ItemStyle CssClass="Display_None" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Unit" HeaderText="Unit">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InwardQty" HeaderText="Inward Qty">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Damageqty" HeaderText="Damage Qty">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PrevReturnQty" HeaderText="Previous Return Qty">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Return Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="GrdtxtReturnQty" runat="server" AutoPostBack="true" CssClass="TextBoxNumeric"
                                                MaxLength="10" OnTextChanged="GrdtxtReturnQty_TextChanged" Text='<%# Bind("GrdtxtReturnQty") %>'
                                                TextMode="SingleLine" Width="60px"></asp:TextBox>
                                            <ajax:FilteredTextBoxExtender ID="FTE_ReturnQty" runat="server" FilterType="Custom,Numbers"
                                                TargetControlID="GrdtxtReturnQty" ValidChars=".">
                                            </ajax:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="rate" HeaderText="Rate">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Amount" HeaderText="Amount">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Reason">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtReason" runat="server" CssClass="TextBox" Height="20px" Text='<%# Bind("Reason") %>'
                                                TextMode="MultiLine" Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="250px" Wrap="False" />
                                        <ItemStyle Width="20px" Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset id="Fieldset3" runat="server" class="FieldSet">
                        <table width="100%">
                            <tr>
                                <td align="center" colspan="3">
                                    <table align="center" width="25%">
                                        <tr>
                                            <td>
                                                <asp:Button ID="BtnUpdate" runat="server" CssClass="button" OnClick="BtnUpdate_Click"
                                                    Text="Update" ValidationGroup="Add" />
                                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Would You Want To Update The Record ?"
                                                    TargetControlID="BtnUpdate">
                                                </ajax:ConfirmButtonExtender>
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnSave" runat="server" CssClass="button" OnClick="BtnSave_Click"
                                                    Text="Save" ValidationGroup="Add" />
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnCancel" runat="server" CssClass="button" OnClick="BtnCancel_Click"
                                                    Text="Cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="Div5" runat="server" class="scrollableDiv">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:GridView ID="ReportGridDtls" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CssClass="mGrid" DataKeyNames="#" OnPageIndexChanging="ReportGridDtls_PageIndexChanging"
                                    OnRowCommand="ReportGridDtls_RowCommand" OnRowDeleting="ReportGridDtls_RowDeleting"
                                    PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LblEstimateId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageGridEdit" runat="server" CommandArgument='<%# Eval("#") %>'
                                                    CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                                <asp:ImageButton ID="ImgBtnDelete" runat="server" CommandArgument='<%# Eval("#") %>'
                                                    CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                    TargetControlID="ImgBtnDelete">
                                                </ajax:ConfirmButtonExtender>
                                                <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&amp;Flag=<%="RETURN"%>&amp;PDFFlag=<%="NOPDF"%>'
                                                    target="_blank">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Icon/GridPrint.png" TabIndex="29"
                                                        ToolTip="Print Damage" />
                                                </a><a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&amp;Flag=<%="RETURN"%>&amp;PDFFlag=<%="PDF"%>'
                                                    target="_blank">
                                                    <asp:Image ID="Image1" runat="server" Height="20px" ImageUrl="~/Images/Icon/PDF.jpg"
                                                        TabIndex="29" ToolTip="Print Damage" />
                                                </a>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle Width="20px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ReturnNo" HeaderText="Return No">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ReturnDate" HeaderText="Return Date">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ReturnAmount" HeaderText="Return Amount">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
