<%@ Page Title="Unit Conversion Master" Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="UnitConversionMaster.aspx.cs" Inherits="Masters_UnitConversionMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<input type="hidden" id="hiddenbox" runat="server" value=""/>
<script type="text/javascript" language="javascript">

function DeleteEquipFunction()
 { 
        var gridViewCtlId = '<%=GridDetails.ClientID%>';
        var ctlGridViewItems = null;
        var ID;
        ctlGridView = document.getElementById(gridViewCtlId);
        ID = ctlGridView.rows[1].cells[1].innerText;
        if (ID == 0)
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
</script>
    <ajax:ToolkitScriptManager ID="ToolScriptManager" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>  
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
                <ProgressTemplate>            
                    <div id="progressBackgroundFilter"></div>
                    <div id="processMessage">   
                        <center><span class="SubTitle">Loading....!!! </span></center>
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>  
            Search For Unit :
            <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" 
                ToolTip="Enter The Text" Width="292px" AutoPostBack="True" 
                ontextchanged="TxtSearch_TextChanged">
             </asp:TextBox>
             <div id="divwidth"></div>
            <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                TargetControlID="TxtSearch" CompletionInterval="100"                             
                UseContextKey="True" FirstRowSelected ="true" ShowOnlyCurrentWordInCompletionListItem="true"
                ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender"
                CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
            </ajax:AutoCompleteExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Unit Conversion Master 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
    <ContentTemplate>
    <fieldset id="fieldset" runat="server" class="FieldSet">
        <div class="clear"></div>
        <table width="100%">           
            <tr>
                <td class="LabelRight">
                    Unit :
                </td>
                <td>
                    <ajax:ComboBox ID="ddlUnit" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                        AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
                        ItemInsertLocation="Append" Width="183px" CssClass="CustomComboBoxStyle" 
                        OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                    </ajax:ComboBox>
                </td>
            </tr>            
        </table> 
        <div class="clear"></div>       
    </fieldset>
    <div class="clear"></div>
    <fieldset id="fieldset1" runat="server" class="FieldSet">
        <div class="clear"></div>
        <table width="100%">            
            <tr>
                <td class="LabelRight">
                    Unit Factor :
                </td>
                <td>
                    <ajax:ComboBox ID="ddlUnitConversion" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                        AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
                        ItemInsertLocation="Append" Width="183px" CssClass="CustomComboBoxStyle" >
                    </ajax:ComboBox>
                   <%-- <asp:TextBox ID="txtUnitFactor" runat="server" CssClass="TextBox" 
                        MaxLength="50" Width="200px" CausesValidation="True">
                    </asp:TextBox>--%>
                        
                    <asp:RequiredFieldValidator ID="RFV2" runat="server" ControlToValidate="ddlUnitConversion" Display="None" 
                    ErrorMessage="Unit Factor is Required" SetFocusOnError="True" ValidationGroup="AddGrid" InitialValue="0">
                    </asp:RequiredFieldValidator>
                    <ajax:ValidatorCalloutExtender ID="VCE1" runat="server" Enabled="True" 
                        TargetControlID="RFV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                    </ajax:ValidatorCalloutExtender>
                    
                   <%-- <asp:CompareValidator ID="CFV" runat="server" ValidationGroup="AddGrid"
                     ControlToValidate="ddlUnitConversion" ControlToCompare="ddlUnit" ErrorMessage="Select Another Unit..!" Operator="NotEqual">
                     </asp:CompareValidator>
                     <ajax:ValidatorCalloutExtender ID="VCE2" runat="server" Enabled="True" 
                        TargetControlID="CFV" WarningIconImageUrl="~/Images/Icon/Warning.png">
                    </ajax:ValidatorCalloutExtender>--%>
                </td>
                <td class="LabelRight">
                    Qty :
                </td>
                <td>
                    <asp:TextBox ID="txtQty" runat="server" CssClass="TextBoxNumeric" MaxLength="50" Width="200px" CausesValidation="True">
                    </asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FTE" Enabled="true" FilterType="Custom,Numbers" ValidChars="." runat="server" TargetControlID="txtQty"></ajax:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="RF_V3" runat="server" ControlToValidate="txtQty" Display="None" 
                    ErrorMessage="Unit Qty is Required" SetFocusOnError="True" ValidationGroup="AddGrid">
                    </asp:RequiredFieldValidator>
                    
                    <ajax:ValidatorCalloutExtender ID="VC_E1" runat="server" Enabled="True" 
                        TargetControlID="RF_V3" WarningIconImageUrl="~/Images/Icon/Warning.png">
                    </ajax:ValidatorCalloutExtender>
                    &nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ImgAddGrid" runat="server" CssClass="Imagebutton" 
                    Height="16px" ImageUrl="~/Images/Icon/Gridadd.png" ToolTip="Add Grid" 
                        ValidationGroup="AddGrid" Width="16px" onclick="ImgAddGrid_Click" />                                        
                    <asp:TextBox ID="TXTUPDATEVALUE" runat="server" CssClass="Display_None" MaxLength="50" Width="10px" >
                    </asp:TextBox>
                </td>
            </tr>  
            <div class="clear"></div>
            <tr>
                <td colspan="4">
                    <div class="scrollableDiv">
                        <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderStyle="None" 
                        BorderWidth="1px" CssClass="mGrid" OnRowCommand="GridDetails_RowCommand" OnRowDeleting="GridDetails_RowDeleting"
                        Font-Bold="False" ForeColor="Black" GridLines="Horizontal" >
                            <Columns>
                                <asp:TemplateField HeaderText="#" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageGridEdit" runat="server" 
                                        CommandArgument="<%#((GridViewRow)Container).RowIndex %>" 
                                        CommandName="SelectGrid" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />

                                        <asp:ImageButton ID="ImageBtnDelete" runat="server" 
                                        CommandArgument='<%#Eval("#") %>' CommandName="Delete" OnClientClick="DeleteEquipFunction();" 
                                        ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" CssClass="Display_None" />
                                    </ItemTemplate>                                    
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle" Wrap="false"/>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Middle" Wrap="false"/>
                                </asp:TemplateField>
                  
                                <asp:BoundField HeaderText="UnitId" DataField="UnitId" >
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" CssClass="Display_None"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" CssClass="Display_None"/>
                                </asp:BoundField> 
                                
                                <asp:BoundField HeaderText="UnitFactor" DataField="UnitFactor" >
                                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false"/>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false"/>
                                </asp:BoundField>                             
                                    
                                <asp:BoundField HeaderText="Qty" DataField="Qty">
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false"/>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="false"/>
                                </asp:BoundField>              
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>          
        </table> 
        <div class="clear"></div>
    </fieldset>    
    <div class="clear"></div>
    <fieldset ID="fieldset2" runat="server" class="FieldSet" width="100%">
        <table width="100%"><tr><td align="center">
                    <table>
                        <tr>                       
                            <td>
                                <asp:Button ID="BtnUpdate" runat="server" CssClass="button" Text="Update" OnClick="BtnUpdate_Click" ValidationGroup="Add" />
                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server"
                                ConfirmText="Would You Like To Update The Record ?" TargetControlID="BtnUpdate">
                                </ajax:ConfirmButtonExtender>
                            </td>
                            <td>
                                <asp:Button ID="BtnSave" runat="server" CssClass="button" Text="Save" OnClick="BtnSave_Click" ValidationGroup="Add" />
                            </td>
                            <td>
                                <asp:Button ID="BtnDelete" runat="server" CssClass="Display_None" Text="Delete" 
                                    ValidationGroup="Add" OnClick="BtnDelete_Click" />
                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" 
                                ConfirmText="Would You Like To Delete The Record ?" TargetControlID="BtnDelete">
                                </ajax:ConfirmButtonExtender>
                            </td>
                            <td>
                                <asp:Button ID="BtnCancel" runat="server" CausesValidation="false" 
                                    CssClass="button" Text="Cancel" OnClick="BtnCancel_Click" />
                            </td>
                        </tr>
                    </table>
        </td></tr></table>
    </fieldset>
       </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ReportTitle" Runat="Server">
    Unit Conversion List 
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Report" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate >
            <div class="ScrollableDiv_FixHeightWidthForRepeater">
                <ul id="subnav">
                    <%--Ul Li Problem Solved repeater--%>
                    <asp:Repeater ID="GrdReport" runat="server" OnItemCommand="GrdReport_ItemCommand">    
                        <ItemTemplate>
                            <li id="Menuitem" runat="server" >                              
                                <asp:LinkButton ID="lbtn_List" CssClass="linkButton" CausesValidation="false"
                                CommandName="Select" CommandArgument='<%# Eval("#") %>' runat="server" Text='<%# Eval("Name") %>'>
                                </asp:LinkButton>                                         
                            </li>
                        </ItemTemplate>    
                    </asp:Repeater>
                </ul>
            </div> 
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

