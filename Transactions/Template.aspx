<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="Template.aspx.cs" Inherits="Transactions_Template" Title="Master list of drawings"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
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
                            Height="20px" Width="120px" meta:resourcekey="Image3Resource1" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            Search for Templates :
            <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
                Width="292px" AutoPostBack="True" OnTextChanged="TxtSearch_TextChanged" meta:resourcekey="TxtSearchResource1"></asp:TextBox>
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
  Master list of drawings
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <fieldset id="F1" runat="server" class="FieldSet">
                            <table width="100%" cellspacing="5">
                                <tr>
                                    <td colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        Title :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTemplateFor" runat="server" CssClass="TextBox" Width="430px"
                                            meta:resourcekey="txtTemplateForResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="Req_Name" runat="server" ControlToValidate="txtTemplateFor"
                                            Display="None" ErrorMessage="Template Name is  Required" SetFocusOnError="True"
                                            ValidationGroup="Add" meta:resourcekey="Req_NameResource1"></asp:RequiredFieldValidator>
                                        <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" TargetControlID="Req_Name"
                                            WarningIconImageUrl="~/Images/Icon/Warning.png">
                                        </ajax:ValidatorCalloutExtender>
                                    </td>
                                    <td class="Label">
                                        Date :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTemplateDate" runat="server" CssClass="TextBox" Width="100px"
                                            meta:resourcekey="txtTemplateDateResource1"></asp:TextBox>
                                        <asp:ImageButton ID="ImageFromDate" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                            ImageUrl="~/Images/Icon/DateSelector.png" meta:resourcekey="ImageFromDateResource1" />
                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            PopupButtonID="ImageFromDate" TargetControlID="txtTemplateDate" Enabled="True">
                                        </ajax:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <fieldset id="F2" runat="server" class="FieldSet">
                            <asp:UpdatePanel ID="UpdateDisp" runat="server">
                                <ContentTemplate>
                                    <table width="100%" cellspacing="5">
                                        <tr>
                                            <td class="Label">
                                                Category :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCategory" runat="server" Width="200px" CssClass="ComboBox"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                                    meta:resourcekey="ddlCategoryResource1">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnAddAll" CssClass="button" runat="server" Text="Add" ToolTip="Add All Item In Category"
                                                    OnClick="BtnAddAll_Click" meta:resourcekey="BtnAddAllResource1" />
                                            </td>
                                            <td class="Label">
                                                Sub-Category :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlsubcategory" runat="server" Width="200px" CssClass="ComboBox"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnAddSubCategory" CssClass="button" runat="server" Text="Add" ToolTip="Add All Item In Sub-Category"
                                                    OnClick="BtnAddSubCategory_Click" />
                                            </td>
                                            <td class="Label">
                                                Items :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlItem" runat="server" Width="200px" CssClass="ComboBox" meta:resourcekey="ddlItemResource1">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnAdd" CssClass="button" runat="server" Text="Add" ToolTip="Add Item"
                                                    OnClick="BtnAdd_Click" meta:resourcekey="BtnAddResource1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="9" align="right" style="color: Red;">
                                                ** All The Entered Quantity Are Respect To Original Unit Which Is In Item Master
                                                Entry.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="9" align="center">
                                                <div id="DivDisp" runat="server" class="ScrollableDiv_FixHeightWidth4">
                                                    <asp:GridView ID="GrdTemplate" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                        DataKeyNames="#" OnRowDataBound="GrdTemplate_RowDataBound" meta:resourcekey="GrdTemplateResource1">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#" meta:resourcekey="TemplateFieldResource1">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px" meta:resourcekey="LblEntryIdResource1"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="Display_None" />
                                                                <ItemStyle CssClass="Display_None" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="All" meta:resourcekey="TemplateFieldResource2">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="GrdSelectAllHeader" runat="server" OnCheckedChanged="GrdSelectAll_CheckedChanged"
                                                                        AutoPostBack="True" meta:resourcekey="GrdSelectAllHeaderResource1" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="GrdSelectAll" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ItemCode" HeaderText="Code">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ItemName" HeaderText="Particulars">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ItemDetailsId" HeaderText="ItemDetailsId">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DrawingNo" HeaderText="DrawingNo">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Location" HeaderText="Site">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" CssClass="Display_None" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AvlQty" HeaderText="Avl Qty">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="MinStockLevel" HeaderText="Min Stock Level">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" CssClass="Display_None" />
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DeliveryPeriod" HeaderText="Delivery Period">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" CssClass="Display_None" />
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AvgRate" HeaderText="Avg Rate">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" CssClass="Display_None" />
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AvgRateDate" HeaderText="Date">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Vendor">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVendor" runat="server" Text='<%# Eval("Vendor") %>' meta:resourcekey="lblVendorResource1"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="Display_None" />
                                                                <ItemStyle CssClass="Display_None" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" Text='<%# Eval("Rate") %>' runat="server" Width="30px" meta:resourcekey="lblRateResource1"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="Display_None" />
                                                                <ItemStyle CssClass="Display_None" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add Vendor" meta:resourcekey="TemplateFieldResource7">
                                                                <ItemTemplate>
                                                                    <div style="">
                                                                        <asp:Image ID="ImgBtnEdit" runat="server" CssClass="Imagebutton" ImageUrl="~/Images/Icon/Gridadd.png"
                                                                            TabIndex="31" />
                                                                        <ajax:PopupControlExtender ID="popup" runat="server" PopupControlID="PnlGrid" Position="Left"
                                                                            CommitProperty="Value" TargetControlID="ImgBtnEdit" DynamicServicePath="" Enabled="True"
                                                                            ExtenderControlID="">
                                                                        </ajax:PopupControlExtender>
                                                                        <asp:Panel ID="PnlGrid" runat="server" Style="display: none;" s>
                                                                            <div class="PopupPanel">
                                                                                <asp:UpdatePanel ID="ProcessEntry" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <div id="DivTabControl" runat="server" class="scrollableDiv">
                                                                                                        <table style="width: 100%">
                                                                                                            <tr>
                                                                                                                <td align="left">
                                                                                                                    <div id="Div1" runat="server" class="ScrollableDiv">
                                                                                                                        <fieldset id="FS_TemplateVendorDetails" class="FieldSet" runat="server" style="width: 100%">
                                                                                                                            <legend id="Lgd_TemplateVendorDetails" class="legend" runat="server">Select Vendor</legend>
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:GridView ID="GridTemplateVendor" runat="server" AutoGenerateColumns="False"
                                                                                                                                        CssClass="mGrid" meta:resourcekey="GridTemplateVendorResource1">
                                                                                                                                        <Columns>
                                                                                                                                            <asp:TemplateField HeaderText="#" Visible="False" meta:resourcekey="TemplateFieldResource5">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px" meta:resourcekey="LblEntryIdResource2"></asp:Label>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:TemplateField>
                                                                                                                                            <asp:BoundField DataField="VendorId" HeaderText="VendorId" meta:resourcekey="BoundFieldResource9">
                                                                                                                                                <HeaderStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                                                                                                                    Wrap="False" />
                                                                                                                                                <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                                                                                                                    Wrap="False" />
                                                                                                                                            </asp:BoundField>
                                                                                                                                            <asp:TemplateField HeaderText="Select" meta:resourcekey="TemplateFieldResource6">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <asp:RadioButton runat="server" ID="RBVendor" meta:resourcekey="RBVendorResource1" />
                                                                                                                                                </ItemTemplate>
                                                                                                                                                <HeaderStyle Width="10px" Wrap="False" />
                                                                                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                                                            </asp:TemplateField>
                                                                                                                                            <asp:BoundField DataField="Vendor" HeaderText="Name" meta:resourcekey="BoundFieldResource10">
                                                                                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                            </asp:BoundField>
                                                                                                                                            <asp:BoundField DataField="PurRate" HeaderText="Rate" meta:resourcekey="BoundFieldResource11">
                                                                                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                            </asp:BoundField>
                                                                                                                                        </Columns>
                                                                                                                                    </asp:GridView>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </fieldset>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="center">
                                                                                                    <asp:CheckBox ID="ChkShowProcess" runat="server" Text="Add Rate" CssClass="CheckBox"
                                                                                                        AutoPostBack="True" OnCheckedChanged="ChkShowProcess_CheckedChanged" meta:resourcekey="ChkShowProcessResource1" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="Display_None" />
                                                                <ItemStyle CssClass="Display_None" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="VendorId" meta:resourcekey="TemplateFieldResource8">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVendorId" runat="server" Text='<%# Eval("VendorId") %>' Width="30px"
                                                                        meta:resourcekey="lblVendorIdResource1"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="Display_None" />
                                                                <ItemStyle CssClass="Display_None" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="UOM" HeaderText="UOM">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="GRDQTY" CssClass="TextBoxNumeric" Width="90px" Text='<%# Bind("QTY") %>'>
                                                                    </asp:TextBox>
                                                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderrate" runat="server" FilterType="Numbers, Custom"
                                                                        ValidChars="." TargetControlID="GRDQTY" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="GRDQTY"
                                                                        Display="None" Enabled="true" ErrorMessage="Please Enter Qunatity" SetFocusOnError="true"
                                                                        ValidationGroup="AddItem">
                                                                    </asp:RequiredFieldValidator>
                                                                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="true"
                                                                        TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                                    </ajax:ValidatorCalloutExtender>
                                                                </ItemTemplate>
                                                                <HeaderStyle />
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remark">
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" ID="GRDREMARK" CssClass="TextBox" Width="250px" Text='<%# Bind("REMARK") %>'
                                                                        TextMode="MultiLine" Height="25px">   
                                                                    </asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle />
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                   <table style="display:none">
                                                       <asp:DropDownList ID="drptemplate" runat="server" AutoPostBack="true" CssClass="ComboBox" OnSelectedIndexChanged="drptemplate_SelectedIndexChanged" Visible="false">
                                                       </asp:DropDownList>
                                                   </table>




                                                    
                                                  

                                                </div>
                                                  






                                             

                                               


                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset id="F3" runat="server" class="FieldSet">
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <table align="center" width="25%">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnUpdate" CssClass="button" runat="server" Text="Update" ValidationGroup="Add"
                                                        OnClick="BtnUpdate_Click" meta:resourcekey="BtnUpdateResource1" />
                                                    <ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
                                                        TargetControlID="BtnUpdate" Enabled="True">
                                                    </ajax:ConfirmButtonExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" ValidationGroup="Add"
                                                        OnClick="BtnSave_Click" meta:resourcekey="BtnSaveResource1" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnCancel" CssClass="button" runat="server" Text="Cancel" CausesValidation="False"
                                                        OnClick="BtnCancel_Click" meta:resourcekey="BtnCancelResource1" />
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
                    <td align="center" colspan="2">
                        <div id="Div5" runat="server" class="scrollableDiv">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="ReportGrid" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        DataKeyNames="#" AllowPaging="True" OnRowCommand="ReportGrid_RowCommand" OnRowDeleting="ReportGrid_RowDeleting"
                                        OnPageIndexChanging="ReportGrid_PageIndexChanging" meta:resourcekey="ReportGridResource1">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" Visible="False" meta:resourcekey="TemplateFieldResource9">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblEstimateId" runat="server" Text='<%# Eval("#") %>' Width="15px"
                                                        meta:resourcekey="LblEstimateIdResource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource10">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageGridEdit" runat="server" CommandArgument='<%# Eval("#") %>'
                                                        CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" meta:resourcekey="ImageGridEditResource1" />
                                                    <asp:ImageButton ID="ImgBtnDelete" runat="server" CommandArgument='<%# Eval("#") %>'
                                                        CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete"
                                                        meta:resourcekey="ImgBtnDeleteResource1" />
                                                    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                        TargetControlID="ImgBtnDelete" Enabled="True">
                                                    </ajax:ConfirmButtonExtender>
                                                    <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#") %>&amp;PDFFlag=PDF&Flag=<%="TM"%>'
                                                        target="_blank">
                                                        <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                                                            ToolTip="Print Template" TabIndex="29" meta:resourcekey="ImgBtnPrintResource1" />
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TemplateName" HeaderText="Title" meta:resourcekey="BoundFieldResource12">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TemplateDate" HeaderText="Date" meta:resourcekey="BoundFieldResource13">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
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
