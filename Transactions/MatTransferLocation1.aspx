<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="MatTransferLocation1.aspx.cs" Inherits="TempFiles_MatTransferLocation1"
    Title="Transfer Location" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
    <input type="hidden" id="hiddenbox" runat="server" value="" />

    <script type="text/javascript" language="javascript">

        function DeleteEquipFunction() {
            var gridViewCtlId = '<%=GridDetails.ClientID%>';
            var ctlGridViewItems = null;
            var ItemID;
            ctlGridViewItems = document.getElementById(gridViewCtlId);
            ItemID = ctlGridViewItems.rows[1].cells[3].innerText;
            if (ItemID == 0) {
                return confirm("There is no record to delete");
            }
            else {
                if (confirm("Would You Want To Delete This  Record ?") == true) {
                    document.getElementById('<%= hiddenbox.ClientID%>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%= hiddenbox.ClientID%>').value = "0";
                    return false;
                }
            }
        }
    </script>

    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
            Search For Transfer No:
            <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
                TabIndex="10" Width="292px" AutoPostBack="True" OnTextChanged="TxtSearch_TextChanged"
                meta:resourcekey="TxtSearchResource1"></asp:TextBox>
            <div id="divwidth">
            </div>
            <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtSearch"
                CompletionInterval="100" UseContextKey="True" FirstRowSelected="True" ShowOnlyCurrentWordInCompletionListItem="True"
                ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" DelimiterCharacters=""
                Enabled="True" ServicePath="">
            </ajax:AutoCompleteExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="Server">
    Material Transfer Location
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <fieldset id="fieldset1" width="100%" runat="server" class="FieldSet">
                            <legend class="legend" align="left">Transfer Details</legend>
                            <table width="100%" cellspacing="6">
                                <tr visible="False" id="TRNO" runat="server">
                                    <td class="Label" runat="server">
                                        Transfer No :
                                    </td>
                                    <td runat="server">
                                        <asp:TextBox runat="server" Width="200px" ID="TxtTranNo" CssClass="TextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        Transfer By :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" Width="200px" CssClass="ComboBox" ID="ddlBy" meta:resourcekey="ddlByResource1"
                                            Visible="False">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="Req1" runat="server" ControlToValidate="ddlBy" Display="None"
                                            InitialValue="0" ErrorMessage="Employee Is Required" SetFocusOnError="True" ValidationGroup="Add"
                                            meta:resourcekey="Req1Resource1"></asp:RequiredFieldValidator>
                                        <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" TargetControlID="Req1"
                                            WarningIconImageUrl="~/Images/Icon/Warning.png">
                                        </ajax:ValidatorCalloutExtender>
                                        <asp:Label ID="LblEmployee" runat="server" CssClass="Label" meta:resourcekey="LblEmployeeResource1"
                                            Text="LblEmployee"></asp:Label>
                                    </td>
                                    <td class="Label">
                                        Transfer Date :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" Width="80px" CssClass="TextBox" ID="TxtDate" meta:resourcekey="TxtDateResource1"></asp:TextBox>
                                        <asp:ImageButton ID="ImageToDate" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                            ImageUrl="~/Images/Icon/DateSelector.png" meta:resourcekey="ImageToDateResource1" />
                                        <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                            PopupButtonID="ImageToDate" TargetControlID="TxtDate" Enabled="True">
                                        </ajax:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        Item Category :
                                    </td>
                                    <td>
                                        <ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                            AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                            Width="200px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                        </ajax:ComboBox>
                                    </td>
                                    <td class="Label">
                                        Item Sub-Category :
                                    </td>
                                    <td>
                                        <ajax:ComboBox ID="ddlSubCategory" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                            AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                            Width="200px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                        </ajax:ComboBox>
                                    </td>
                    </td>
                </tr>
                <tr>
                    <td class="Label">
                        Item :
                    </td>
                    <td>
                        <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                            AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
                            ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                        </ajax:ComboBox>
                        <asp:RequiredFieldValidator ID="Req3" runat="server" ControlToValidate="ddlItem"
                            Display="None" InitialValue="0" ErrorMessage="Item Is Required" SetFocusOnError="True"
                            ValidationGroup="AddGrid" meta:resourcekey="Req3Resource1"></asp:RequiredFieldValidator>
                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                            TargetControlID="Req3" WarningIconImageUrl="~/Images/Icon/Warning.png">
                        </ajax:ValidatorCalloutExtender>
                        <%--THIS START HERE--%>
                       <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="TxtItemName" runat="server" CssClass="search_List" Width="292px"
                                    AutoPostBack="True" OnTextChanged="TxtItemName_TextChanged"></asp:TextBox>
                                <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemName" runat="server" TargetControlID="TxtItemName"
                                    CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                    ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                    CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                </ajax:AutoCompleteExtender>
                                <ajax:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="TxtItemName"
                                    WatermarkText="Type Item Name" WatermarkCssClass="water" />
                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <%--THIS END HERE--%>
                    </td>
                    <td class="Label">
                        Description :
                    </td>
                    <td>
                        <ajax:ComboBox ID="ddldescription" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                            CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append" Width="200px"
                            CssClass="CustomComboBoxStyle" AutoPostBack="True" OnSelectedIndexChanged="ddldescription_SelectedIndexChanged">
                        </ajax:ComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddldescription"
                            Display="None" InitialValue=" --Select ItemDescription--" ErrorMessage="Description Is Required"
                            SetFocusOnError="True" ValidationGroup="Add" meta:resourcekey="Req1Resource1"></asp:RequiredFieldValidator>
                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                            TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                        </ajax:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td class="Label">
                        Transfer From :
                    </td>
                    <td>
                        <asp:Label ID="lblLocation" runat="server" Font-Bold="True" Text="lblEmployeeName"
                            Width="200px" meta:resourcekey="lblLocationResource1"></asp:Label>
                        <asp:DropDownList ID="ddlEmployee" runat="server" Width="120px" Visible="False" CssClass="ComboBox"
                            TabIndex="32" meta:resourcekey="ddlEmployeeResource1">
                        </asp:DropDownList>
                    </td>
                    <td class="Label">
                        Quantity At Source :
                    </td>
                    <td>
                        <asp:Label ID="lblQuantAtSource" runat="server" Font-Bold="True"></asp:Label>
                        -
                        <asp:Label runat="server" ID="LBLUNIT" CssClass="Label" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Label">
                        Transfer To :
                    </td>
                    <td>
                        <ajax:ComboBox ID="ddlTansTo" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                            AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                            Width="200px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlTansTo_SelectedIndexChanged">
                        </ajax:ComboBox>
                        <asp:RequiredFieldValidator ID="Req4" runat="server" ControlToValidate="ddlTansTo"
                            Display="None" InitialValue="0" ErrorMessage="Location Is Required" SetFocusOnError="True"
                            ValidationGroup="AddGrid" meta:resourcekey="Req4Resource1"></asp:RequiredFieldValidator>
                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                            TargetControlID="Req4" WarningIconImageUrl="~/Images/Icon/Warning.png">
                        </ajax:ValidatorCalloutExtender>
                    </td>
                    <td class="Label">
                        Quantity At Destination :
                    </td>
                    <td>
                        <asp:Label ID="txtQuantAtDest" runat="server" Font-Bold="True" Width="200px" meta:resourcekey="txtQuantAtDestResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="Label">
                        Transfer Quantity :
                    </td>
                    <td>
                        <asp:TextBox ID="txtTansQty" runat="server" CssClass="TextBox" Width="200px" meta:resourcekey="txtTansQtyResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="Req5" runat="server" ControlToValidate="txtTansQty"
                            Display="None" ErrorMessage="Transfer Quantity Is Required" SetFocusOnError="True"
                            ValidationGroup="AddGrid" meta:resourcekey="Req5Resource1"></asp:RequiredFieldValidator>
                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                            TargetControlID="Req5" WarningIconImageUrl="~/Images/Icon/Warning.png">
                        </ajax:ValidatorCalloutExtender>
                        <ajax:FilteredTextBoxExtender ID="FTP_TransQty" runat="server" TargetControlID="txtTansQty"
                            FilterType="Custom, Numbers" ValidChars="." Enabled="True">
                        </ajax:FilteredTextBoxExtender>
                        <asp:DropDownList ID="ddlUnitConversion" runat="server" CssClass="ComboBox" TabIndex="32"
                            Width="80px">
                        </asp:DropDownList>
                    </td>
                    <td class="Label">
                        Rate :
                    </td>
                    <td>
                        <asp:Label runat="server" Width="200px" CssClass="Label1" ID="txtRate" MaxLength="10"></asp:Label>
                        <asp:ImageButton ID="ImgAddGrid" runat="server" CssClass="Imagebutton" Height="16px"
                            ImageUrl="~/Images/Icon/Gridadd.png" OnClick="ImgAddGrid_Click" ToolTip="Add Grid"
                            ValidationGroup="AddGrid" Width="16px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div class="ScrollableDiv_FixHeightWidth4">
                                                <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    BorderStyle="None" BorderWidth="1px" CssClass="mGrid" Font-Bold="False" ForeColor="Black"
                                                    GridLines="Horizontal" OnRowCommand="GridDetails_RowCommand" OnRowDeleting="GridDetails_RowDeleting"
                                                    meta:resourcekey="GridDetailsResource1">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#" Visible="False" meta:resourcekey="TemplateFieldResource1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' meta:resourcekey="LblEntryIdResource1" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageGridEdit" runat="server" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                                    CommandName="SelectGrid" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit"
                                                                    CssClass="Display_None" />
                                                                <asp:ImageButton ID="ImageBtnDelete" runat="server" CommandArgument='<%# Eval("#") %>'
                                                                    CommandName="Delete" OnClientClick="DeleteEquipFunction();" ImageUrl="~/Images/Icon/GridDelete.png"
                                                                    ToolTip="Delete" meta:resourcekey="ImageBtnDeleteResource1" />
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" meta:resourcekey="BoundFieldResource1">
                                                            <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle Wrap="False" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CategoryName" HeaderText="Category" meta:resourcekey="BoundFieldResource2">
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ItemId" HeaderText="ItemID" meta:resourcekey="BoundFieldResource3">
                                                            <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle Wrap="False" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Item" HeaderText="Item" meta:resourcekey="BoundFieldResource4">
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ItemDescID" HeaderText="ItemDescID" meta:resourcekey="BoundFieldResource4">
                                                            <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle Wrap="False" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Unit" HeaderText="Unit">
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TransFrom" HeaderText="Transfer From" meta:resourcekey="BoundFieldResource5">
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="AvlQtySou" HeaderText="Qty At Source" meta:resourcekey="BoundFieldResource6">
                                                            <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle Wrap="False" HorizontalAlign="Right" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="StockLocationID" HeaderText="TransToId" meta:resourcekey="BoundFieldResource7">
                                                            <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle Wrap="False" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TransTo" HeaderText="Transfer To" meta:resourcekey="BoundFieldResource8">
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="AvlQty" HeaderText="Qty At Destination" meta:resourcekey="BoundFieldResource9">
                                                            <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle Wrap="False" HorizontalAlign="Right" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Qty" HeaderText="Transfer Qty" meta:resourcekey="BoundFieldResource10">
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle Wrap="False" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="rate" HeaderText="Rate" SortExpression="rate">
                                                            <HeaderStyle Wrap="False" />
                                                            <ItemStyle Wrap="False" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OriQtyatSource" HeaderText="OriQtyatSource" meta:resourcekey="BoundFieldResource7">
                                                            <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle Wrap="False" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OriQtyatDest" HeaderText="OriQtyatDest" meta:resourcekey="BoundFieldResource7">
                                                            <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle Wrap="False" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OriTrnasfer" HeaderText="OriTrnasfer" meta:resourcekey="BoundFieldResource7">
                                                            <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle Wrap="False" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UnitID" HeaderText="UnitID">
                                                            <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle Wrap="False" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td class="Label">
                        Notes :
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Height="15px" Width="800px"
                            CssClass="TextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
            </table>
            </fieldset> </td> </tr>
            <tr>
                <td>
                    <fieldset id="fieldset2" runat="server" class="FieldSet" width="100%">
                        <table cellspacing="6" width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="BtnUpdate" runat="server" CssClass="button" OnClick="BtnUpdate_Click"
                                        TabIndex="6" Text="Update" ValidationGroup="Add" meta:resourcekey="BtnUpdateResource1" />
                                    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Would You Like To Update the Record ..! "
                                        TargetControlID="BtnUpdate" Enabled="True">
                                    </ajax:ConfirmButtonExtender>
                                    <asp:Button ID="BtnSave" runat="server" CssClass="button" OnClick="BtnSave_Click"
                                        TabIndex="6" Text="Save" ValidationGroup="Add" meta:resourcekey="BtnSaveResource1" />
                                    <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" CssClass="button"
                                        OnClick="BtnCancel_Click" TabIndex="7" Text="Cancel" meta:resourcekey="BtnCancelResource1" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel_GrdReport" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GrdReport" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CssClass="mGrid" GridLines="None" Width="100%" OnRowCommand="GrdReport_RowCommand"
                                    OnRowDeleting="GrdReport_RowDeleting" meta:resourcekey="GrdReportResource1">
                                    <Columns>
                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgBtnEdit" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                                    CommandArgument='<%# Eval("#") %>' CommandName="SelectGrid" ImageUrl="~/Images/Icon/GridEdit.png"
                                                    TabIndex="62" ToolTip="Edit Record" meta:resourcekey="ImgBtnEditResource1" />
                                                <asp:ImageButton ID="ImgBtnDelete" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                                    CommandArgument='<%# Eval("#") %>' CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png"
                                                    TabIndex="63" ToolTip="Delete Record" meta:resourcekey="ImgBtnDeleteResource1" />
                                                <a href='../PrintReport/MaterialTransfrLocPrint.aspx?ID=<%# Eval("#") %>' target="_blank">
                                                    <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                                                        ToolTip="Print Material Transfer Location" />
                                                </a>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sr. No." meta:resourcekey="TemplateFieldResource4">
                                            <ItemTemplate>
                                                <asp:Label ID="TransId" runat="server" Text='<%# Container.DataItemIndex+1 %>' meta:resourcekey="TransIdResource1"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="7%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TransId" HeaderText="TransId" meta:resourcekey="BoundFieldResource11">
                                            <HeaderStyle CssClass="Display_None" />
                                            <ItemStyle CssClass="Display_None" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Transfer No" DataField="TransNo" meta:resourcekey="BoundFieldResource12">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Transfer Date" DataField="Date" meta:resourcekey="BoundFieldResource13">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Transfer By" DataField="TransBy" meta:resourcekey="BoundFieldResource14">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
