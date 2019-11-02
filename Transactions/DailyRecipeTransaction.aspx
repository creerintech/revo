<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="DailyRecipeTransaction.aspx.cs" Inherits="Transactions_DailyRecipeTransaction"
    Title="Daily Recipe Transaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
    <input type="hidden" id="hiddenbox" runat="server" value="" />

    <script type="text/javascript" language="javascript">

function DeleteEquipFunction()
 { 
        var gridViewCtlId = '<%=GridDetails.ClientID%>';
        var ctlGridViewItems = null;
        var ItemID;
        ctlGridViewItems = document.getElementById(gridViewCtlId);
        ItemID =  ctlGridViewItems.rows[1].cells[2].innerText;        
        if(ItemID==0)
        {        
        if(confirm("There is no record to delete")==true)
        {
        document.getElementById('<%= hiddenbox.ClientID%>').value="0"; 
        }
        else
        {
         document.getElementById('<%= hiddenbox.ClientID%>').value="0"; 
        }
        }
        else
        {
        if(confirm("Are you sure you want to delete?")==true)
        {
        document.getElementById('<%= hiddenbox.ClientID%>').value="1";
        return true;
        }
        else
        {
         document.getElementById('<%= hiddenbox.ClientID%>').value="0";
         return false;
         }
        }
}

 function CalTotalAmt()
 {
    var _txtTotalQty = document.getElementById('<%= txtTotalQty.ClientID %>');   
    var _txtAmtPerPlate = document.getElementById('<%= txtAmtPerPlate.ClientID %>');   
    var _txtTotAmt = document.getElementById('<%= txtTotAmt.ClientID %>');   
    var _TotAmt=0;
    _TotAmt=parseFloat(_txtTotalQty.value)*parseFloat(_txtAmtPerPlate.value);
    _txtTotAmt.value=parseFloat(_TotAmt).toFixed(2);
 }

    </script>

    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>            
    <div id="progressBackgroundFilter"></div>
    <div id="processMessage">   
    <center><span class="SubTitle">Loading....!!! </span></center>
    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" 
    Height="20px" Width="120px" meta:resourcekey="Image3Resource1" />                                
    </div>
    </ProgressTemplate>
            </asp:UpdateProgress>
            Search for Templates :
            <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
                Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged"></asp:TextBox>
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
    Daily Recipe Transaction  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="4">
                        <fieldset id="F1" runat="server" class="FieldSet">
                            <table width="100%" cellspacing="3">
                                <tr>
                                    <td class="Label">
                                        Date :
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="90px" ReadOnly="True"></asp:TextBox>
                                        <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="false" Format="dd-MMM-yyyy"
                                            PopupButtonID="ImageButton212" TargetControlID="txtDate" />
                                        <asp:ImageButton ID="ImageButton212" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                            ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label" width="100px">
                                        Recipe Name :
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ddlRecipeName" runat="server" Width="200px" CssClass="ComboBox"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlRecipeName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="Req_Name" runat="server" ControlToValidate="ddlRecipeName"
                                            Display="None" ErrorMessage="Recipe Name is Required" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Add"> 
                                        </asp:RequiredFieldValidator>
                                        <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" TargetControlID="Req_Name"
                                            WarningIconImageUrl="~/Images/Icon/Warning.png">
                                        </ajax:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        Quantity :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalQty" runat="server" CssClass="TextBox" Width="100px" onkeyup="CalTotalAmt();"></asp:TextBox>
                                    </td>
                                    <td class="Label">
                                        Amount Per Plate:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmtPerPlate" runat="server" CssClass="TextBox" Width="100px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Label">
                                        Total Amount :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotAmt" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:Button ID="BtnAdd" CssClass="button" runat="server" Text="Add" OnClick="BtnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <fieldset id="Fieldset1" runat="server" class="FieldSet">
                            <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderStyle="None" BorderWidth="1px" CssClass="mGrid" Font-Bold="False" ForeColor="Black"
                                GridLines="Horizontal" OnRowCommand="GridDetails_RowCommand" OnRowDeleting="GridDetails_RowDeleting"
                                OnRowDataBound="GridDetails_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="#" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageBtnDelete" runat="server" CommandArgument='<%#Eval("#") %>'
                                                CommandName="Delete" OnClientClick="DeleteEquipFunction();" ImageUrl="~/Images/Icon/GridDelete.png"
                                                ToolTip="Delete" />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="RecipeId" DataField="RecipeId">
                                        <HeaderStyle CssClass="Display_None" />
                                        <ItemStyle CssClass="Display_None" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="RecipeName" DataField="RecipeName">
                                        <HeaderStyle Wrap="false" />
                                        <ItemStyle Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Total Quantity" DataField="Quantity">
                                        <HeaderStyle Wrap="false" />
                                        <ItemStyle Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="AmountPerPlate" DataField="AmtPerPlate">
                                        <HeaderStyle Wrap="false" />
                                        <ItemStyle Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Total Amount" DataField="TotalAmt">
                                        <HeaderStyle Wrap="false" />
                                        <ItemStyle Wrap="false" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="SubUnitId" HeaderText="SubUnitId">
            <HeaderStyle CssClass="Display_None" Wrap="False" />
            <ItemStyle CssClass="Display_None" Wrap="False" />
            </asp:BoundField>--%>
                                </Columns>
                                <FooterStyle CssClass="ftr" />
                                <PagerStyle CssClass="pgr" />
                            </asp:GridView>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="Label" align="right">
                        Total Order Cost :
                        <asp:TextBox ID="txtTotalOrderCost" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <fieldset id="Fieldset2" runat="server" class="FieldSet">
                            <asp:GridView ID="GridItemDtls" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderStyle="None" BorderWidth="1px" CssClass="mGrid" Font-Bold="False" ForeColor="Black"
                                GridLines="Horizontal">
                                <Columns>
                                    <asp:TemplateField HeaderText="#" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="ItemId" DataField="ItemId">
                                        <HeaderStyle CssClass="Display_None" />
                                        <ItemStyle CssClass="Display_None" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="ItemName" DataField="ItemName">
                                        <HeaderStyle Wrap="false" />
                                        <ItemStyle Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Quantity" DataField="Quantity">
                                        <HeaderStyle Wrap="false" />
                                        <ItemStyle Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="IngredAmount" DataField="IngredAmt">
                                        <HeaderStyle Wrap="false" />
                                        <ItemStyle Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubUnitId" HeaderText="SubUnitId">
                                        <%--<HeaderStyle CssClass="Display_None" Wrap="False" />--%>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RecipeId" HeaderText="RecipeId">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ActualRate" HeaderText="ActualRate">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QtyPerUnit" HeaderText="QtyPerUnit">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle CssClass="ftr" />
                                <PagerStyle CssClass="pgr" />
                            </asp:GridView>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <fieldset id="Fieldset3" runat="server" class="FieldSet">
                            <table width="25%">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnUpdate" runat="server" CausesValidation="False" CssClass="button"
                                            Text="Update" ValidationGroup="Add" OnClick="BtnUpdate_Click" />
                                        <ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
                                            TargetControlID="BtnUpdate">
                                        </ajax:ConfirmButtonExtender>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnSave" runat="server" CausesValidation="False" CssClass="button"
                                            Text="Save" ValidationGroup="Add" OnClick="BtnSave_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" CssClass="button"
                                            Text="Cancel" OnClick="BtnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <fieldset id="Fieldset4" runat="server" class="FieldSet">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="6" align="center">
                                                <asp:GridView ID="ReportGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                    CssClass="mGrid" DataKeyNames="#" onrowcommand="ReportGrid_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblRecipeId" runat="server" Text='<%# Eval("#") %>' Width="15px">
                                                                </asp:Label>
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
                                                                <a href='../PrintReport/MaterialReqTemplatePrint.aspx?ID=<%# Eval("#")%>&amp;Flag=<%="PS"%>'
                                                                    target="_blank">
                                                                    <%--<asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png" TabIndex="29" 
    ToolTip="Print Issue Register" />--%>
                                                                </a>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Wrap="false" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="OrderDate" HeaderText="Date">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OrderNo" HeaderText="OrderNo">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TotalOrderCost" HeaderText="TotalCost">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
