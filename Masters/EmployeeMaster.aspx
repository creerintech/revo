<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true"
    CodeFile="EmployeeMaster.aspx.cs" Inherits="Masters_EmployeeMaster" Title="Employee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
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
            Search for Employee :
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
    Employee Master
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <fieldset id="fieldset1" width="100%" runat="server" class="FieldSet">
                            <table width="100%" cellspacing="6">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        Name :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtEmpName" runat="server" CssClass="TextBox" MaxLength="50" Width="368px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="Req_Name" runat="server" ControlToValidate="TxtEmpName"
                                            Display="None" ErrorMessage="Employee Name is Required" SetFocusOnError="True"
                                            ValidationGroup="Add">
                                        </asp:RequiredFieldValidator>
                                        <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" TargetControlID="Req_Name"
                                            WarningIconImageUrl="~/Images/Icon/Warning.png">
                                        </ajax:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LabelMultiLine">
                                        Address :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtEmpAddress" runat="server" CssClass="TextBox" MaxLength="50"
                                            Width="368px" TextMode="MultiLine" Height="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        Tel No :
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TxtTelNo" runat="server" CssClass="TextBox" MaxLength="13" Width="150px"></asp:TextBox>
                                                    <ajax:FilteredTextBoxExtender ID="FTE_Tel" runat="server" TargetControlID="TxtTelNo"
                                                        FilterType="Custom,Numbers" ValidChars="-">
                                                    </ajax:FilteredTextBoxExtender>
                                                </td>
                                                <td class="Label">
                                                    Mobile No :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtMobileNo" runat="server" CssClass="TextBox" MaxLength="13" Width="150px"></asp:TextBox>
                                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TxtMobileNo"
                                                        FilterType="Custom,Numbers" ValidChars="+">
                                                    </ajax:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label" style="height: 24px">
                                        Email :
                                    </td>
                                    <td style="height: 24px">
                                        <asp:TextBox ID="TxtEmail" runat="server" CssClass="TextBox" MaxLength="50" Width="368px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="REV2" runat="server" Display="None" ErrorMessage="Please Enter Valid Email ID..!"
                                            ControlToValidate="TxtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="Add">
                                        </asp:RegularExpressionValidator>
                                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                            TargetControlID="REV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                        </ajax:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Display_None">
                                        Designation :
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="Display_None"  Width="150px"></asp:TextBox>                                                    
                                                </td>
                                                <td class="Display_None">
                                                    Department :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDepartment" runat="server" CssClass="Display_None"  Width="150px"></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="LabelMultiLine">
                                        Notes :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtNotes" runat="server" CssClass="TextBox" MaxLength="50" Width="368px"
                                            TextMode="MultiLine" Height="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
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
                        <fieldset id="fieldset2" width="100%" runat="server" class="FieldSet">
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <table align="center" width="25%">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnUpdate" runat="server" CssClass="button" Text="Update" ValidationGroup="Add"
                                                        OnClick="BtnUpdate_Click" />
                                                    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
                                                        TargetControlID="BtnUpdate">
                                                    </ajax:ConfirmButtonExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="Add"
                                                        OnClick="BtnSave_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnDelete" runat="server" CssClass="button" Text="Delete" ValidationGroup="Add"
                                                        OnClick="BtnDelete_Click" />
                                                    <ajax:ConfirmButtonExtender ID="ConfirmBuettonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record ?"
                                                        TargetControlID="BtnDelete">
                                                    </ajax:ConfirmButtonExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnCancel" runat="server" CausesValidation="false" CssClass="button"
                                                        Text="Cancel" OnClick="BtnCancel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ReportTitle" runat="Server">
    Employee List
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Report" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="ScrollableDiv_FixHeightWidthForRepeater">
                <ul id="subnav">
                    <%--Ul Li Problem Solved repeater--%>
                    <asp:Repeater ID="GrdReport" runat="server" OnItemCommand="GrdReport_ItemCommand">
                        <ItemTemplate>
                            <li id="Menuitem" runat="server">
                                <asp:LinkButton ID="lbtn_List" CssClass="linkButton" CausesValidation="false" CommandName="Select"
                                    CommandArgument='<%# Eval("#") %>' runat="server" Text='<%# Eval("Name") %>'>
                                </asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
