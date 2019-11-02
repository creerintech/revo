<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="POExcessQty.aspx.cs" Inherits="Reports_POExcessQty" Title="Excess Purchase Order Report" %>

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
    Purchase Order Excess/Shortage Quantity Report
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="UPEntry" runat="server">
        <ContentTemplate>
            <fieldset id="F1" runat="server" class="FieldSet">
                <table width="100%" cellspacing="8">
                    <tr>
                        <td class="Label">
                            Site :
                        </td>
                        <td>
                            <ajax:ComboBox ID="ddlSite" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append" Width="240px"
                                CssClass="CustomComboBoxStyle">
                            </ajax:ComboBox>
                        </td>
                        <td class="Label">
                            Supplier:
                        </td>
                        <td>
                            <ajax:ComboBox ID="ddlSupplier" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append" Width="240px"
                                CssClass="CustomComboBoxStyle">
                            </ajax:ComboBox>
                        </td>
                        <td class="Label">
                            PO No.:
                        </td>
                        <td>
                            <ajax:ComboBox ID="ddlNo" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append" Width="240px"
                                CssClass="CustomComboBoxStyle">
                            </ajax:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Label" colspan="5">
                        </td>
                        <td align="left" colspan="1">
                            &nbsp;<asp:Button ID="BtnShow" runat="server" CssClass="button" TabIndex="4" Text="Show"
                                ValidationGroup="Add" ToolTip="Show Details" OnClick="BtnShow_Click" />
                            <asp:Button ID="BtnCancel" runat="server" CssClass="button" TabIndex="5" Text="Cancel"
                                ToolTip="Clear The Details" OnClick="BtnCancel_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblCount" runat="server" CssClass="SubTitle"></asp:Label>
                        </td>
                        <td align="right" valign="middle">
                            <asp:ImageButton ID="ImgBtnPrint" runat="server" TabIndex="6" OnClientClick="javascript:CallPrint('divPrint')"
                                ImageUrl="~/Images/Icon/Print-Icon.png" ToolTip="Print Report" />
                            <asp:ImageButton ID="ImgBtnExport" runat="server" ImageUrl="~/Images/Icon/excel-icon.png"
                                TabIndex="7" ToolTip="Export To Excel" OnClick="ImgBtnExport_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <div id="divPrint" class="ScrollableDiv_FlexiableHeight">
                                <asp:GridView ID="GrdReport" runat="server" ShowFooter="true" AutoGenerateColumns="False"
                                    CaptionAlign="Top" AllowPaging="false" CssClass="mGrid" Width="100%" OnRowDataBound="GrdReport_RowDataBound"
                                    OnPageIndexChanging="GrdReport_PageIndexChanging" PageSize="300">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="All">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="GrdSelectAllHeader" runat="server" AutoPostBack="true" OnCheckedChanged="GrdSelectAllHeader_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="GrdSelectAll" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <a id="ANCPATH" href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="PS"%>&SFlag=<%="Authorised"%>&PDFFlag=<%="PDF"%>&SID=<%# Eval("SuplierId")%>&IID=<%# Eval("ItemId")%>'
                                                    target="_blank">
                                                    <asp:Image ID="IMGCALLPDF" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png"
                                                        ToolTip="PDF Purchase Order" TabIndex="29" />
                                                </a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Supplier" HeaderText="Supplier">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="True"
                                                Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PONo" HeaderText="PO No.">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PODate" HeaderText="PO Date">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Itemname" HeaderText="Item Name">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="POQTY" HeaderText="Qty">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="INQTY" HeaderText="Inw.Qty">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BALQTY" HeaderText="Shortage/Excess Qty">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="#" HeaderText="#">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SuplierID" HeaderText="SID">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ItemId" HeaderText="IID">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                            <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
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
                        <td align="center" colspan="2">
                            <table align="center" width="25%">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Approve" ValidationGroup="Add"
                                            OnClick="BtnSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div runat="server" id="Panel1">
                                        <div id="progressBackgroundFilter">
                                        </div>
                                        <div runat="server" id="Div1" class="PopUpSample">
                                            <fieldset id="Fieldset3" runat="server" class="FieldSet" style="background-color: ThreeDLighShadow">
                                                <table width="100%" cellspacing="8">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="Label1" runat="server" Text="USER NAME" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td align="right" valign="top">
                                                            <asp:TextBox runat="server" ID="Username" CssClass="TextBoxLOGIN" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="LBLPASSWORD" runat="server" Text="PASSWORD" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td align="right" valign="top">
                                                            <asp:TextBox runat="server" ID="TXTPASSWORDFORM" CssClass="TextBoxLOGIN" TextMode="Password"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <asp:Button runat="server" ID="BTNLOGINFORM" Text="Login" CssClass="buttonLOGIN"
                                                                BorderColor="WhiteSmoke" OnClick="BTNLOGINFORM_Click" />
                                                            <ajax:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server" TargetControlID="BTNLOGINFORM"
                                                                Corners="All" Radius="8" BorderColor="Gray">
                                                            </ajax:RoundedCornersExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <ajax:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" TargetControlID="Panel1"
                                        VerticalSide="Middle" VerticalOffset="5" HorizontalSide="Center" HorizontalOffset="5"
                                        ScrollEffectDuration=".1" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
