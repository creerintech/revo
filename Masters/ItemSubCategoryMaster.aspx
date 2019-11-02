<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="ItemSubCategoryMaster.aspx.cs" Inherits="Masters_ItemSubCategoryMaster" Title="ItemSubCategoryMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
  
   <%-- <asp:UpdateProgress ID="UpdateProgress2" runat="server" >
    <ProgressTemplate>            
    <div id="progressBackgroundFilter"></div>
    <div id="processMessage">   
    <center><span class="SubTitle">Loading....!!! </span></center>
    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
    </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>

      Search for Sub Category : 
      <asp:TextBox ID="TxtSearch" runat="server" 
       CssClass="search" ToolTip="Enter The Text" Width="292px" AutoPostBack="True" 
          ontextchanged="TxtSearch_TextChanged">
      </asp:TextBox>
      <div id="divwidth"></div>
      <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
          TargetControlID="TxtSearch" CompletionInterval="100"                          
          UseContextKey="True" FirstRowSelected ="true" ShowOnlyCurrentWordInCompletionListItem="true"  
          ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender"
          CompletionListItemCssClass="AutoExtenderList"
          CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                  
     </ajax:AutoCompleteExtender> 
    <%--<input type="submit" value="Search" class="submit"/>--%>
 </ContentTemplate>                             
 </asp:UpdatePanel>                         
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Item Sub Category Master    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
   <table width="100%">
   <tr>
     <td align="center" >
        <asp:UpdatePanel ID="AjaxPanelUpdateProgress" runat="server">
           <ContentTemplate >
              <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
                <ProgressTemplate>            
          <div id="progressBackgroundFilter"></div>
                                <div id="processMessage">   
                                <center><span class="SubTitle">Loading....!!! </span></center>
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" />                                
                                </div> 
                 </ProgressTemplate>
           </asp:UpdateProgress> 
        </ContentTemplate> 
     </asp:UpdatePanel> 
    </td>
 </tr>
 </table>
 
  
   <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
     <ContentTemplate >
     
      <table width="100%">
      
       <tr>
  <td>
      <fieldset id="fieldset1" runat="server" class="FieldSet">
        <table width="100%" cellspacing="6">
        <tr>
            <td class="Label">
                </td>
               <td align="left" >
               </td>
            <td>
            </td>
        </tr>
         
           <tr>
            <td class="Label">
                Category :</td>
            <td align="left">
                <ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown"
    AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="331px"  TabIndex="1"  CssClass="CustomComboBoxStyle" 
                    AutoPostBack="True" onselectedindexchanged="ddlCategory_SelectedIndexChanged" >
    </ajax:ComboBox>      
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="TxtItemSubCat" Display="None" 
                    ErrorMessage="Item SubCategory Required" SetFocusOnError="True" 
                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                    Enabled="True" TargetControlID="Req1" 
                    WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>
            </td>
        </tr>  
        
        <tr>
            <td class="Label">
                Item Sub Category :</td>
            <td align="left">
                <asp:TextBox ID="TxtItemSubCat" runat="server" CssClass="TextBox" 
                 MaxLength="90" Width="350px" TabIndex="1"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="Req1" runat="server" 
                    ControlToValidate="TxtItemSubCat" Display="None" 
                    ErrorMessage="Item SubCategory Required" SetFocusOnError="True" 
                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
                    Enabled="True" TargetControlID="Req1" 
                    WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>
            </td>
        </tr>  
        <tr>
            <td class="Label">
                Remark :</td>
            <td align="left">
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" Rows="3" runat="server" CssClass="TextBox" 
                 MaxLength="500" Width="350px"  TabIndex="1" ></asp:TextBox>
            </td>
            <td>
                
            </td>
        </tr>  
        <tr>
            <td class="Label">
                </td>
               <td align="left" >
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
   <fieldset id="fieldset2" runat="server" class="FieldSet">
   
     <table width="100%">
                 <tr>
                 <td align="center">
                 <table align="center" width="25%">
                 <tr>
         <td>
                <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="button" TabIndex="1" 
                   ValidationGroup="Add" onclick="BtnUpdate_Click"/>
                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                   ConfirmText="Would You Like To Update the Record ..! "
                   TargetControlID="BtnUpdate" >
                </ajax:ConfirmButtonExtender>
         </td>
         <td>
                <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" TabIndex="1" 
                   ValidationGroup="Add" onclick="BtnSave_Click"/>
               
         </td>
         <td>
                <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="button" 
                    TabIndex="1" CausesValidation="False" ValidationGroup="Add" 
                    onclick="BtnDelete_Click"  />
                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" 
                   ConfirmText="Would You Like To Delete the Record ..! "
                   TargetControlID="BtnDelete" >
                </ajax:ConfirmButtonExtender>
          </td>
          <td>
                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="button" 
                   TabIndex="1" CausesValidation="False" ValidationGroup="Add" 
                    onclick="BtnCancel_Click"/>
                  
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
<div ID="Div5" runat="server" class="scrollableDiv">
<asp:UpdatePanel ID="UpdatePanel6" runat="server">
<ContentTemplate>
<asp:GridView ID="CategoryGrid" runat="server" AutoGenerateColumns="False" 
CssClass="mGrid" DataKeyNames="#" AllowPaging="false" 
        meta:resourcekey="ReportGridResource1" 
        onrowdatabound="CategoryGrid_RowDataBound"  >
<Columns>
<asp:BoundField DataField="CategoryName" HeaderText="Category" 
        meta:resourcekey="BoundFieldResource12">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
<asp:BoundField DataField="SubCategory" HeaderText="SubCategory" 
        meta:resourcekey="BoundFieldResource13">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
</asp:BoundField>
<asp:BoundField DataField="Remark" HeaderText="Remark" 
        meta:resourcekey="BoundFieldResource13">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" />
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
<asp:Content ID="Content4" ContentPlaceHolderID="ReportTitle" Runat="Server">
    Item Sub Category List    
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Report" Runat="Server">                 
 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate >
    <div class="ScrollableDiv_FixHeightWidthForRepeater">
    <ul id="subnav">
            <%--Ul Li Problem Solved repeater--%>
            <asp:Repeater ID="GrdReport" runat="server" 
                onitemcommand="GrdReport_ItemCommand">    
            <ItemTemplate>
            <li id="Menuitem" runat="server" >                              
              <asp:LinkButton ID="lbtn_List" CssClass="linkButton"
                CausesValidation="false" TabIndex="2"
                CommandName="Select"
                CommandArgument='<%# Eval("#") %>'
                runat="server"
                 Text='<%# Eval("Name") %>'
                ></asp:LinkButton>
                                 
            </li>
            </ItemTemplate>    
            </asp:Repeater>
            </ul>
         </div>
              </ContentTemplate>
       </asp:UpdatePanel>                  
</asp:Content>

