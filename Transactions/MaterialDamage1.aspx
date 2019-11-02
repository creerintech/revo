<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="MaterialDamage1.aspx.cs" Inherits="Transactions_MaterialDamage1" Title="Material Damage Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
    <ajax:ToolkitScriptManager ID="ToolScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>

    <script type="text/javascript" language="javascript">
        function OnContactSelected(source, eventArgs) {

            var hdnValueID = "<%= hdnValue.ClientID %>";

            document.getElementById(hdnValueID).value = eventArgs.get_value();
            __doPostBack(hdnValueID, "");
        } 
    </script>

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
            Search for Damage Item :
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
    Material Damage register
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
        <ContentTemplate>
            <table width="75%">
                <tr>
                    <td>
                        <fieldset class="FieldSet" id="Fieldset8" runat="server">
                            <legend class="legend">Damage Details </legend>
                            <table width="100%" cellspacing="2">
                                <tr class="Display_None">
                                    <td class="Label">
                                        DamageNo :
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="TxtDamageNo" CssClass="TextBox" runat="server" Width="160px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="13%" class="Label">
                                        Prepared By :
                                        <td>
                                            <asp:Label ID="lblPreparedBy" runat="server" CssClass="Label_Dynamic" Text="lblPreparedBy"></asp:Label>
                                        </td>
                                        <td class="Label">
                                            Damage Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtDamageDate" CssClass="TextBox" runat="server" Width="80px">
                                            </asp:TextBox>
                                            <asp:ImageButton ID="ImageDate" CssClass="Imagebutton" runat="server" ImageUrl="~/Images/Icon/DateSelector.png" />
                                            <ajax:CalendarExtender ID="CalDate" runat="server" Format="dd-MMM-yyyy" TargetControlID="TxtDamageDate"
                                                PopupButtonID="ImageDate">
                                            </ajax:CalendarExtender>
                                        </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        Damaged Through :
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoDamagedThrough" runat="server" RepeatDirection="Horizontal"
                                            CssClass="RadioButton" AutoPostBack="true" OnSelectedIndexChanged="rdoInwardThrough_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0">&nbsp;Inward Wise&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1">&nbsp;Item Wise</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="Label">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
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
                                <tr id="t1" runat="server">
                                    <td width="13%" class="Label">
                                        InwardNo :
                                    </td>
                                    <td>
                                        <%--   <asp:DropDownList ID="ddlInwardNo" runat="server" CssClass="ComboBox" 
                  Width="250px" AutoPostBack="True" 
                  onselectedindexchanged="ddlInwardNo_SelectedIndexChanged">
              </asp:DropDownList>--%>
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
                                        <asp:Label ID="lblInwardNo" runat="server" Text="lblInwardNo" Width="162px" CssClass="Label_Dynamic"></asp:Label>
                                    </td>
                                    <td class="Label">
                                        InwardDate :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInwardDate" runat="server" Text="lblInwardDate" Width="162px" CssClass="Label_Dynamic"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        PONo :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPONO" runat="server" Text="lblPONO" CssClass="Label_Dynamic"></asp:Label>
                                    </td>
                                    <td class="Label">
                                        SupplierName :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSuppName" runat="server" Text="lblSuppName" CssClass="Label_Dynamic"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        Debit Note :
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoDebitNote" runat="server" AutoPostBack="true" CssClass="RadioButton"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">&nbsp;Yes&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="0">&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="Label">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset class="FieldSet" id="FS_ItemDtls" runat="server">
                            <legend class="legend">Item Details </legend>
                            <table width="100%" cellspacing="2">
                                <tr>
                                    <td class="Label" width="13%">
                                        Category :
                                    </td>
                                    <td>
                                        <%-- <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" 
                 CssClass="ComboBox" Width="250px" 
                 onselectedindexchanged="ddlCategory_SelectedIndexChanged">
             </asp:DropDownList>--%>
                                        <ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                            AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                            Width="250px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                        </ajax:ComboBox>
                                    </td>
                                    <td class="Label">
                                        Sub Category
                                    </td>
                                    <td>
                                        <ajax:ComboBox ID="ddlsubcategory" runat="server" AutoCompleteMode="SuggestAppend"
                                            AutoPostBack="True" CaseSensitive="false" CssClass="CustomComboBoxStyle" DropDownStyle="DropDown"
                                            ItemInsertLocation="Append" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged"
                                            RenderMode="Inline" Width="250px">
                                        </ajax:ComboBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnAdd" runat="server" CssClass="button" ValidationGroup="AddItem"
                                            meta:resourcekey="BtnAddResource1" OnClick="BtnAdd_Click" Text="Add" />
                                    </td>
                                </tr>
                                <tr id="TR_RateList" runat="server">
                                    <td class="Label" align="right" rowspan="3" valign="top">
                                        Rate :
                                    </td>
                                    <td align="left" rowspan="3">
                                        <asp:ListBox ID="lstSupplierRate" runat="server" Width="300px" AutoPostBack="True">
                                        </asp:ListBox>
                                    </td>
                                    <td class="Label" align="right" valign="top">
                                        Item :
                                    </td>
                                    <td align="left">
                                        <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                            AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                            Width="250px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                        </ajax:ComboBox>
                                         <asp:HiddenField ID="hdnValue" OnValueChanged="hdnValue_ValueChanged" runat="server" />
                                        <%--<asp:DropDownList ID="ddlUnitConversn" runat="server" width="250px" CssClass="ComboBox">
                        </asp:DropDownList>--%>
                                        <%-- <ajax:ComboBox ID="ddlItem" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="true"
                                            CaseSensitive="false" CssClass="CustomComboBoxStyle" DropDownStyle="DropDown"
                                            ItemInsertLocation="Append" Visible="false" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged"
                                            RenderMode="Inline" Width="250px">
                                        </ajax:ComboBox>--%>
                                        <%--THIS START HERE--%>
                                        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="TxtItemName" runat="server" CssClass="search_List" Width="272px"
                                                    AutoPostBack="True" OnTextChanged="TxtItemName_TextChanged"></asp:TextBox>
                                               
                                                <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemName" runat="server" TargetControlID="TxtItemName"
                                                    CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                    ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                    CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" OnClientItemSelected="OnContactSelected">
                                                </ajax:AutoCompleteExtender>
                                                <ajax:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="TxtItemName"
                                                    WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                        <%--THIS END HERE--%>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label" align="right">
                                        Desc :
                                    </td>
                                    <td>
                                        <ajax:ComboBox ID="ddldesc" runat="server" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                            CssClass="CustomComboBoxStyle" DropDownStyle="DropDown" ItemInsertLocation="Append"
                                            RenderMode="Inline" Width="250px">
                                        </ajax:ComboBox>
                                        <asp:RequiredFieldValidator ID="RFVDESC" runat="server" ControlToValidate="ddldesc"
                                            Display="None" Enabled="true" ErrorMessage="Description Is Required" InitialValue="--Select Description--"
                                            SetFocusOnError="true" ValidationGroup="AddItem">
                                        </asp:RequiredFieldValidator>
                                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutDESCExtender1" runat="server"
                                            Enabled="true" TargetControlID="RFVDESC" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                        </ajax:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Label_Dynamic" valign="top">
                                        Qty :
                                    </td>
                                    <td align="left" class="Label_Dynamic" valign="top">
                                        <asp:TextBox ID="txtItemOrdQty" runat="server" CssClass="TextBox" Width="60px" AutoPostBack="True"></asp:TextBox>
                                        <%-- OnTextChanged="txtItemOrdQty_TextChanged"--%>
                                        <asp:DropDownList ID="ddlUnitConversn" runat="server" AutoPostBack="true" CssClass="ComboBox"
                                            Width="90px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset class="FieldSet" id="FieldsetItem" runat="server">
                            <legend class="legend">ItemWise Details</legend>
                            <div class="ScrollableDiv_FixHeightWidth4">
                                <asp:GridView ID="GRDItemWise" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#0CCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="4" CssClass="mGrid" ForeColor="Black" Width="100%">
                                    <Columns>
                                        <%-- <asp:TemplateField HeaderText="#" Visible="False"></asp:TemplateField>--%>
                                        <asp:BoundField DataField="ItemId" HeaderText="ItemId">
                                            <HeaderStyle CssClass="Display_None" Wrap="False" />
                                            <ItemStyle CssClass="Display_None" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ItemName" HeaderText="Particules">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UnitId" HeaderText="UnitId">
                                            <HeaderStyle Wrap="True" CssClass="Display_None" />
                                            <ItemStyle CssClass="Display_None" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                        <asp:BoundField DataField="StockinHand" HeaderText="StockinHand"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Damage Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="GrdtxtDamageQty" runat="server" CssClass="TextBox" MaxLength="10"
                                                    Text='<%# Bind("DamageQty") %>' TextMode="SingleLine" Width="80px" AutoPostBack="True"
                                                    OnTextChanged="GrdtxtDamageQty_TextChanged1"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="InwardRate" HeaderText="Inward Rate">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TxtReason" runat="server" TextMode="MultiLine" Width="250px" Height="30px"
                                                    Text='<%# Bind("Reason") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="False" Width="250px" />
                                            <ItemStyle Wrap="False" Width="250px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset class="FieldSet" id="FieldsetInward" runat="server">
                            <legend class="legend">InwardWise Details</legend>
                            <div class="ScrollableDiv_FixHeightWidth4">
                                <asp:GridView ID="GrdInwardWiseDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#0CCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="4" CssClass="mGrid" ForeColor="Black" Width="100%">
                                    <Columns>
                                        <%-- <asp:TemplateField HeaderText="#" Visible="False"></asp:TemplateField>--%>
                                        <asp:BoundField DataField="InwardNo" HeaderText="Inward No" />
                                        <asp:BoundField DataField="ItemId" HeaderText="ItemId">
                                            <HeaderStyle CssClass="Display_None" Wrap="False" />
                                            <ItemStyle CssClass="Display_None" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ItemName" HeaderText="Item">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InwardQty" HeaderText="Inward Qty">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UnitId" HeaderText="UnitId">
                                            <HeaderStyle CssClass="Display_None" Wrap="True" />
                                            <ItemStyle CssClass="Display_None" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Unit" HeaderText="Unit"></asp:BoundField>
                                        <asp:BoundField DataField="ReturnQty" HeaderText="Return Qty" />
                                        <asp:BoundField DataField="PreviousDamageQty" HeaderText="Previous Damage Qty" />
                                        <asp:TemplateField HeaderText="DamageQty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="GrdtxtDamageQty" runat="server" CssClass="TextBox" MaxLength="10"
                                                    Text='<%# Bind("DamageQty") %>' TextMode="SingleLine" Width="80px" OnTextChanged="GrdtxtDamageQty_TextChanged"
                                                    AutoPostBack="True"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RemainInwardQty" HeaderText="Remain Inward Qty">
                                            <HeaderStyle Wrap="False" CssClass="Display_None" />
                                            <ItemStyle Wrap="False" CssClass="Display_None" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InwardRate" HeaderText="Inward Rate">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TxtReason" runat="server" TextMode="MultiLine" Width="250px" Height="30px"
                                                    Text='<%# Bind("Reason") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="False" Width="250px" />
                                            <ItemStyle Wrap="False" Width="250px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset class="FieldSet" id="Fieldset3" runat="server">
                            <table width="100%">
                                <tr>
                                    <td colspan="3" align="center">
                                        <table align="center" width="25%">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnUpdate" CssClass="button" runat="server" Text="Update" ValidationGroup="Add"
                                                        OnClick="BtnUpdate_Click" />
                                                    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Would You Want To Update The Record ?"
                                                        TargetControlID="BtnUpdate">
                                                    </ajax:ConfirmButtonExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" ValidationGroup="Add"
                                                        OnClick="BtnSave_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnDelete" CssClass="button" runat="server" Text="Delete" ValidationGroup="Add" />
                                                    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Would You Want To Delete The Record ?"
                                                        TargetControlID="BtnDelete">
                                                    </ajax:ConfirmButtonExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnCancel" CssClass="button" runat="server" Text="Cancel" OnClick="BtnCancel_Click" />
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
                        <div id="Div5" runat="server" class="ScrollableDiv">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <asp:GridView ID="ReportGridDtls" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        DataKeyNames="#" AllowPaging="True" OnRowCommand="ReportGridDtls_RowCommand"
                                        OnRowDeleting="ReportGridDtls_RowDeleting" OnPageIndexChanging="ReportGridDtls_PageIndexChanging">
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
                                                    <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="DAMAGE"%>&PDFFlag=<%="NOPDF"%>'
                                                        target="_blank">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Icon/GridPrint.png" ToolTip="Print Damage"
                                                            TabIndex="29" />
                                                    </a>
                                                    <%--<a href="../Transactions/DebitNote.aspx?ID=<%# Eval("#")%>" target="_blank">
                                         <asp:Image ID="ImgDebitPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                                                ToolTip="Print Debit Note" TabIndex="29" Visible='<%# Convert.ToBoolean(Eval("DebitNote").ToString()) %>' />
                                         </a>--%>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DamageNo" HeaderText="Damage No"></asp:BoundField>
                                            <asp:BoundField DataField="DamageDate" HeaderText="Damage Date" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                            <asp:BoundField DataField="DebitNote" HeaderText="DebitNote">
                                                <HeaderStyle CssClass="Display_None" />
                                                <ItemStyle CssClass="Display_None" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
