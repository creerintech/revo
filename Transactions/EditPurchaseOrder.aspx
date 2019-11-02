<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditPurchaseOrder.aspx.cs" Inherits="Transactions_EditPurchaseOrder"
    Title="Purchase Order Regeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />

    <script language="javascript" type="text/javascript">

        function ShowPOP() {
            document.getElementById('<%= dialog.ClientID %>').style.display = "block";
        }
        function HidePOP() {
            document.getElementById('<%= dialog.ClientID %>').style.display = "none";
        }


        function CalculateNetAmtForGrid(objGrid) {
            var _GridDetails = document.getElementById('<%= GrdPODtls.ClientID %>');
            var rowIndex = objGrid.offsetParent.parentNode.rowIndex;
            var ORDQTY = (_GridDetails.rows[rowIndex].cells[9].children[0]);
            var INWQTY = (_GridDetails.rows[rowIndex].cells[10].children[0]);
            var PENQTY = (_GridDetails.rows[rowIndex].cells[11].children[0]);
            var total = 0;

            if (ORDQTY.value == "" || isNaN(ORDQTY.value)) {
                ORDQTY.value = 0;
            }

            if (INWQTY.value == "" || isNaN(INWQTY.value)) {
                INWQTY.value = 0;
            }

            if (PENQTY.value == "" || isNaN(PENQTY.value)) {
                PENQTY.value = 0;
            }

            PENQTY.value = parseFloat((parseFloat(ORDQTY.value) * parseFloat(INWQTY.value))).toFixed(2);

        }
    </script>

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
            Search for Purchase Order :
            <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
                Width="292px" AutoPostBack="True" OnTextChanged="TxtSearch_TextChanged"></asp:TextBox>
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
    Approve &amp; Authorise Purchase Order
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="10">
                <tr>
                    <td align="right">
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label runat="server" ID="LBLSEARCH" Text="Custom Search :" ToolTip="Search By Company Name, Site Name, Item Name, Category, Supplier Name, PO No, PO Date, Status"
                                        CssClass="Label_Dynamic"></asp:Label>
                                    <asp:TextBox ID="txtMisc" runat="server" OnTextChanged="TxtSearchMisc_TextChanged"
                                        CssClass="TextBox_Search" ToolTip="Search By Company Name, Site Name, Item Name, Category, Supplier Name, PO No, PO Date, Status"
                                        Width="292px" AutoPostBack="True"></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtMisc"
                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" ShowOnlyCurrentWordInCompletionListItem="true"
                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ServiceMethod="GetCompletionList2">
                                    </ajax:AutoCompleteExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <fieldset id="Fieldset2" class="FieldSet" runat="server">
                            <legend id="Legend3" class="legend" runat="server">Purchase Order </legend>
                            <div id="div1" class="ScrollableDiv_FixHeightWidthAPP">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GrdReqPO" runat="server" AutoGenerateColumns="False" Width="100%"
                                                CssClass="mGrid" BackColor="White" BorderColor="#0CCCCC" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="4" ForeColor="Black" AllowPaging="True" DataKeyNames="#" OnRowCommand="GrdReqPO_RowCommand"
                                                OnRowDataBound="GrdReqPO_RowDataBound" OnPageIndexChanging="GrdReqPO_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageApproved" runat="server" ImageUrl="~/Images/New Icon/ImgApprove.jpg"
                                                                ToolTip="Would you like to Approve this Purchase order?" OnClick="ImageApproved_Click" />
                                                            <asp:ImageButton ID="ImageAuthorised" runat="server" Visible="false" ImageUrl="~/Images/New Icon/ImgAuthorised.jpg"
                                                                ToolTip="Would you like to Authorise this Purchase order?" OnClick="ImageAuthorised_Click" />
                                                            <asp:ImageButton ID="ImageSuccess" runat="server" Visible="false" ImageUrl="~/Images/New Icon/Success1.jpg"
                                                                ToolTip="This Purchase order is Authorised, Can't Change its status" />
                                                            <asp:ImageButton ID="ImageCancel" runat="server" Visible="false" ImageUrl="~/Images/Icon/GridDelete.png"
                                                                ToolTip="Cancel" OnClick="ImageCancel_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%--                                                                
<a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RATECOMP"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="PDF"%>&Rate=<%="0"%>&POID=<%# Eval("POId")%>&ItemID=<%="0"%>' target="_blank">
<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png" ToolTip="PDF For Rate Comparison" TabIndex="29"  />
</a>
      --%>
                                                            <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RATECOMPV"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="PDF"%>&Rate=<%="0"%>&POID=<%# Eval("POId")%>&ItemID=<%="0"%>'
                                                                target="_blank">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/New Icon/favicon_C.ico"
                                                                    ToolTip="PDF For Rate Comparison" TabIndex="29" />
                                                            </a>
                                                            <asp:ImageButton ID="ImageGridEdit" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                                CommandName="Select" ImageUrl="~/Images/New Icon/ViewRecord.png" ToolTip="Show/Edit Items in Purchase Order" />
                                                            <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="PS"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="PDF"%>'
                                                                target="_blank">
                                                                <asp:Image ID="IMGCALLPDF" runat="server" ImageUrl="~/Images/New Icon/favicon.ico"
                                                                    ToolTip="PDF Purchase Order" TabIndex="29" />
                                                            </a>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="PS"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="PDF"%>'
                                                                target="_blank">
                                                                <asp:Image ID="IMGCALLPDF1" runat="server" ImageUrl="~/Images/New Icon/favicon_C.ico"
                                                                    ToolTip="PDF View Purchase Order" TabIndex="29" />
                                                            </a>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SuplierName" HeaderText="SuplierName">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PONo" HeaderText="PO No">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                    </asp:BoundField>
                                               <%--     <asp:BoundField DataField="Location" HeaderText="Site">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                    </asp:BoundField>--%>
                                                    <asp:BoundField DataField="PODate" HeaderText="PO Date">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="POStatus" HeaderText="Status">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"
                                                            Wrap="False" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Display_None"
                                                            Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="POId" HeaderText="POId">
                                                        <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                        <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SuplierId" HeaderText="SuplierId">
                                                        <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                        <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                            Wrap="False" />
                                                    </asp:BoundField>
                                                  <%--  <asp:BoundField DataField="Remark" HeaderText="Remark">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                                    </asp:BoundField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <table align="center" width="25%">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" ValidationGroup="Add"
                                                            OnClick="BtnSave_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="BtnAuthorized" CssClass="button" runat="server" Text="Authorized"
                                                            ValidationGroup="Add" Visible="False" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="BtnCancel" CssClass="button" runat="server" Text="Cancel" ValidationGroup="Add"
                                                            CausesValidation="False" OnClick="BtnCancel_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <tr id="TR_PODtls" runat="server">
                    <td align="left" colspan="2">
                        <fieldset id="Fieldset1" runat="server" class="FieldSet" style="width: 100%">
                            <legend id="Legend1" runat="server" class="legend" onclick="return Legend1_onclick()">
                                Purchase Order Details</legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <table align="center" width="25%">
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:LinkButton ID="hyl_Hide" runat="server" CssClass="linkButton" OnClick="hyl_Hide_Click">Hide</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <div id="dialog" class="PopUpSample" runat="server">
                <div id="progressBackgroundFilter1">
                </div>
                <div id="Div2" class="PopUpSample" runat="server">
                    <table width="90%" cellspacing="16px">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="LBLTEXT" Text="PURCHASE ORDER DETAILS.." CssClass="LabelPOP"></asp:Label>
                            </td>
                            <td id="Td1" runat="server" align="right">
                                <asp:ImageButton ID="ImgBtnClose" runat="server" OnClientClick="javascript:HidePOP();"
                                    ImageUrl="~/Images/New Icon/close-button1.png" ToolTip="Close" OnClick="ImgBtnClose_Click" />
                            </td>
                        </tr>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="divPrintsd" class="ScrollableDiv_FixHeightWidthPOP">
                                    <asp:GridView ID="GrdPODtls" runat="server" AutoGenerateColumns="False" Width="100%"
                                        CssClass="mGrid" DataKeyNames="#" OnRowCommand="GrdPODtls_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblProcessId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageBtnLastPO" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                        Height="20px" Width="20px" CommandName="Select" ImageUrl="~/Images/New Icon/view_file-icon.gif"
                                                        ToolTip="Show Last 10 Purchase Order"></asp:ImageButton>
                                                    <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("POId")%>&Flag=<%="PS"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="PDF"%>'
                                                        target="_blank">
                                                        <asp:Image ID="IMGCALLPDF" runat="server" ImageUrl="~/Images/New Icon/favicon.ico"
                                                            ToolTip="PDF Purchase Order" TabIndex="29" />
                                                    </a><a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RATECOMP"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="PDF"%>&Rate=<%# Eval("Rate")%>&POID=<%# Eval("POId")%>&ItemID=<%# Eval("ItemId")%>'
                                                        target="_blank">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/New Icon/favicon_C.ico"
                                                            ToolTip="PDF For Rate Comparison" TabIndex="29" CssClass="Display_None" />
                                                    </a><a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RATECOMPV"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="PDF"%>&Rate=<%# Eval("Rate")%>&POID=<%# Eval("POId")%>&ItemID=<%# Eval("ItemId")%>'
                                                        target="_blank">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/New Icon/favicon_C.ico"
                                                            ToolTip="PDF For Rate Comparison" TabIndex="29" />
                                                    </a>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="POId" HeaderText="POId">
                                                <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" VerticalAlign="Middle"
                                                    Wrap="False" />
                                                <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                    Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PONo" HeaderText="PONo">
                                                <HeaderStyle CssClass="Display_None" />
                                                <ItemStyle CssClass="Display_None" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PODate" HeaderText="PODate">
                                                <HeaderStyle CssClass="Display_None" />
                                                <ItemStyle CssClass="Display_None" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemId" HeaderText="ItemId">
                                                <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" VerticalAlign="Middle"
                                                    Wrap="False" />
                                                <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                    Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Code">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OrderQuantity" HeaderText="Ord. Qty">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Ord. Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GrdtxtOrderQty" runat="server" CssClass="TextBox" onkeyup="CalculateNetAmtForGrid(this);"
                                                        Text='<%# Eval("OrderQuantity") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GrdtxtRate" runat="server" CssClass="TextBoxGrid" Enabled="false"
                                                        Text='<%# Eval("Rate") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GrdtxtAmount" runat="server" CssClass="TextBoxGrid" Enabled="false"
                                                        Text='<%# Eval("Amount") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Location" HeaderText="Site">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="LBLREMARK" Text='<%# Eval("Remark") %>' ToolTip='<%# Eval("RemarkFull") %>'
                                                        CssClass="Label"> </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remark At Authorisation Time">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="LBLAuthREMARK" Text='<%# Eval("AuthRemark") %>' ToolTip='<%# Eval("AuthRemarkFull") %>'
                                                        CssClass="Label"> </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remark For Authorisation">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GrdtxtRemark" runat="server" CssClass="TextBox" Text='<%# Eval("AuthPORemark") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FK_ItemDtlsId" HeaderText="ItemDescID">
                                                <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" VerticalAlign="Middle"
                                                    Wrap="False" />
                                                <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                    Wrap="False" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="BtnUpdate" CssClass="button" runat="server" Text="Update" TabIndex="34"
                                    ToolTip="For Update The Quantity Of Purchase Order.." CausesValidation="false"
                                    OnClick="BtnUpdate_Click" AccessKey="u" />
                                <ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
                                    TargetControlID="BtnUpdate">
                                </ajax:ConfirmButtonExtender>
                                <ajax:RoundedCornersExtender ID="RoundedCornersExtender5" runat="server" TargetControlID="BtnUpdate"
                                    Corners="All" Radius="8" BorderColor="Gray">
                                </ajax:RoundedCornersExtender>
                                <asp:Button ID="Button2" CssClass="Display_None" runat="server" Text="Cancel" TabIndex="36"
                                    CausesValidation="False" OnClick="BtnCancel_Click" AccessKey="c" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <div id="div3" class="ScrollableDiv_FixHeightWidthPOP">
                                    <asp:GridView ID="GrdItemWiseRate" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        DataKeyNames="#">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblProcessId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PONo" HeaderText="PONo">
                                                <HeaderStyle CssClass="Display_None" />
                                                <ItemStyle CssClass="Display_None" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PODate" HeaderText="PODate">
                                                <HeaderStyle CssClass="Display_None" />
                                                <ItemStyle CssClass="Display_None" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Code">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Material">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OrderQuantity" HeaderText="Ord. Qty">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Old Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GrdtxtRate" runat="server" CssClass="TextBoxGrid" Enabled="false"
                                                        Width="100px" Text='<%# Eval("Rate") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="This PO Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GrdtxtPORate" runat="server" CssClass="TextBoxGrid" Enabled="false"
                                                        Width="100px" Text='<%# Eval("PORate") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Difference">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GrdtxtPORate" runat="server" CssClass="TextBoxGrid" Enabled="false"
                                                        Width="100px" Text='<%# Eval("RateDiff") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GrdtxtAmount" runat="server" CssClass="TextBoxGrid" Enabled="false"
                                                        Width="100px" Text='<%# Eval("Amount") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Location" HeaderText="Site">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
