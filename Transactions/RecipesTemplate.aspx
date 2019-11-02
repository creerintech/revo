 <%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="RecipesTemplate.aspx.cs" Inherits="Transactions_RecipesTemplate" Title="Recipe Template" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">

 <input type="hidden" id="hiddenbox" runat="server" value=""/>
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
</script>

    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
    CombineScripts="True" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
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
    <div id="divwidth"></div>
    <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtSearch"  CompletionInterval="100"                               
    UseContextKey="True" FirstRowSelected ="True" ShowOnlyCurrentWordInCompletionListItem="True"  ServiceMethod="GetCompletionList"
    CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
    DelimiterCharacters="" Enabled="True" ServicePath=""></ajax:AutoCompleteExtender> 
    </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
        Recipe Template          
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
<ContentTemplate>
 <table width="100%">
   <tr>
   <td>
   <fieldset id="F1" runat="server" class="FieldSet">
   <table width="100%" cellspacing="3">
   <tr>
   <td class="Label" width="100px">Menu Item :</td>
   <td>
    <asp:TextBox ID="txtMenuName" runat="server" CssClass="TextBox" Width="300px" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="Req_Name" runat="server" 
    ControlToValidate="txtMenuName" Display="None" 
    ErrorMessage="Recipe Name is Required" 
    SetFocusOnError="True" ValidationGroup="Add"> 
    </asp:RequiredFieldValidator>
    <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" 
    TargetControlID="Req_Name" WarningIconImageUrl="~/Images/Icon/Warning.png">
    </ajax:ValidatorCalloutExtender>
   </td>
   </tr>
   <tr>
   <td class="Label">Amount Per Plate:</td>
   <td> 
    <asp:TextBox ID="txtAmtPerPlate" runat="server" CssClass="TextBox" Width="150px" >
    </asp:TextBox>
    </td>
   </tr>
   <tr>
   <td colspan="">
   </td>
   </tr>
   </table>
   </fieldset>
   </td>
   </tr>
   <tr>
   <td>
   <fieldset id="Fieldset1" runat="server" class="FieldSet">
   <legend id="Legend3" class="legend" runat="server">Ingredients</legend>
   <table width="100%" cellspacing="3">
   <tr>
   <td class="Label" width="100px">ItemName :</td>
   <td>
    <asp:DropDownList ID="ddlItemName" runat="server" Width="200px" 
    CssClass="ComboBox" AutoPostBack="True" onselectedindexchanged="ddlItemName_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="Req_ItemName" runat="server" ControlToValidate="ddlItemName" Display="None" 
    ErrorMessage="ItemName Required" SetFocusOnError="True" ValidationGroup="Add"> 
    </asp:RequiredFieldValidator>
    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="Req_ItemName" 
    WarningIconImageUrl="~/Images/Icon/Warning.png">
    </ajax:ValidatorCalloutExtender>
   </td>
   <td class="Label">Quantity :</td>
   <td>
    <asp:TextBox ID="TxtQuantity" runat="server" CssClass="TextBox" Width="120px" >
    </asp:TextBox>
     <asp:DropDownList ID="ddlUnit" runat="server" Width="90px" 
    CssClass="ComboBox" AutoPostBack="True">
    </asp:DropDownList>
    <%--<asp:Label ID="LblUnit" runat="server" CssClass="Label_Dynamic" Width="60px" Text="Unit"></asp:Label>--%>
   </td>
   <td class="Label">Avg Purchase Rate :</td>
   <td>
    <asp:Label ID="LblAmount" runat="server" CssClass="Label_Dynamic"  Width="60px" Text="Amount" Font-Bold="True"></asp:Label>
    <%--<asp:DropDownList ID="ddlAmount" runat="server" Width="120px" CssClass="ComboBox" AutoPostBack="True">
    </asp:DropDownList>--%>
    <asp:ImageButton ID="ImgAddGrid" runat="server" CssClass="Imagebutton" 
    Height="16px" ImageUrl="~/Images/Icon/Gridadd.png" ToolTip="Add Grid" 
           ValidationGroup="AddGrid" Width="16px" onclick="ImgAddGrid_Click" />
   </td>
   </tr>
   <tr>
   <td colspan="6">
    <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" 
          BackColor="White" BorderStyle="None" BorderWidth="1px" CssClass="mGrid" 
          Font-Bold="False" ForeColor="Black" GridLines="Horizontal" 
           onrowdatabound="GridDetails_RowDataBound" 
           onrowdeleting="GridDetails_RowDeleting" 
           onrowcommand="GridDetails_RowCommand">
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
            ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
            </ItemTemplate>
            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle Wrap="False" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="ItemName" DataField="ItemName">
            <HeaderStyle Wrap="false" />
            <ItemStyle Wrap="false" />
            </asp:BoundField>
            <asp:BoundField HeaderText="ItemId" DataField="ItemId" >
            <HeaderStyle CssClass="Display_None" />
            <ItemStyle CssClass="Display_None" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Qty-Unit" DataField="Quantity">
            <HeaderStyle Wrap="false" />
            <ItemStyle Wrap="false" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Amount" DataField="IngredAmt">
            <HeaderStyle Wrap="false" />
            <ItemStyle Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="SubUnitId" HeaderText="SubUnitId">
            <%--<HeaderStyle CssClass="Display_None" Wrap="False" />--%>
            <HeaderStyle CssClass="Display_None" Wrap="False" />
            <ItemStyle CssClass="Display_None" Wrap="False" />
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
   </td>
   </tr>
   <tr>
    <td class="Label" colspan="6"  align="right">Total Amount :
    <asp:TextBox ID="TxtTotalAmt" runat="server" CssClass="textboxnumericreadonly" 
     Enabled="false"></asp:TextBox>
    </td>
   </tr>
   <tr>
   <td colspan="6"></td>
   </tr>
<tr>
<td colspan="6" align="center">
<fieldset id="F3" runat="server" width="100%">
<table width="100%">
<tr>
<td align="center">
<table width="25%">
<tr>
<td >
<asp:Button ID="BtnUpdate" runat="server" CausesValidation="False" 
CssClass="button" Text="Update" 
ValidationGroup="Add" onclick="BtnUpdate_Click" />
<ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" 
ConfirmText="Would You Like To Update The Record ?" TargetControlID="BtnUpdate">
</ajax:ConfirmButtonExtender>
</td>
<td>
<asp:Button ID="BtnSave" runat="server" CausesValidation="False" 
CssClass="button" Text="Save" ValidationGroup="Add" onclick="BtnSave_Click" />
</td>
<td>
<asp:Button ID="BtnCancel" runat="server" CausesValidation="False" 
CssClass="button" Text="Cancel" onclick="BtnCancel_Click" />
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
<td colspan="6" align="center">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
<ContentTemplate>
<asp:GridView ID="ReportGrid" runat="server" AllowPaging="True" 
AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="#" 
        onrowcommand="ReportGrid_RowCommand" onrowdeleting="ReportGrid_RowDeleting">
<Columns>
<asp:TemplateField HeaderText="#" Visible="False">
<ItemTemplate>
<asp:Label ID="LblRecipeId" runat="server" Text='<%# Eval("#") %>' 
Width="15px">
</asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField>
<ItemTemplate>
<asp:ImageButton ID="ImageGridEdit" runat="server" CommandArgument='<%# Eval("#") %>' CommandName="Select" 
ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
<asp:ImageButton ID="ImgBtnDelete" runat="server" CommandArgument='<%# Eval("#") %>' CommandName="Delete" 
ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
<ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record..!" 
TargetControlID="ImgBtnDelete"></ajax:ConfirmButtonExtender>
<a href='../PrintReport/MaterialReqTemplatePrint.aspx?ID=<%# Eval("#")%>&amp;Flag=<%="PS"%>' 
target="_blank">
<%--<asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png" TabIndex="29" 
ToolTip="Print Issue Register" />--%>
</a>
</ItemTemplate>
<FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<HeaderStyle Width="20px" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" 
Wrap="false" />
</asp:TemplateField>
<asp:BoundField DataField="MenuItem" HeaderText="Menu Item">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
<asp:BoundField DataField="AmtPerPlate" HeaderText="Amount Per Plate">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
</Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
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

