<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="pdetails.aspx.cs" Inherits="Masters_pdetails" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
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
  
      Search For Valve :
    <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" 
          ToolTip="Enter The Text" Width="292px" AutoPostBack="True" 
          ontextchanged="TxtSearch_TextChanged">
     </asp:TextBox>

    <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
     TargetControlID="TxtSearch" CompletionInterval="100"                             
     UseContextKey="True" FirstRowSelected ="true" ShowOnlyCurrentWordInCompletionListItem="true"
    ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender"
    CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
    </ajax:AutoCompleteExtender> 
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
      Valve Class     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
<ContentTemplate>
    <table style="width: 100%">
        <tr>
            
                 <td align="center" >
          <asp:UpdateProgress ID="UpdateProgress2" runat="server" >
            <ProgressTemplate>            
           <%-- <div id="progressBackgroundFilter"></div>
               <div id="processMessage">   
               <center><span class="SubTitle">Loading....!!! </span></center>
               <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" />                                
             </div>--%> 
            </ProgressTemplate>
 </asp:UpdateProgress>
 </td>
 </tr>
 </table>


   <table width="100%">
  <tr>
  <td>
 <fieldset id="fieldset" runat="server" class="FieldSet">
 <table width="100%" cellspacing="6">
        <tr>
            <td class="Label">
            </td>
            <td align="left">
            </td>
                <td>
                </td>
        </tr>
        
        <tr>
            <td class="Label">
                Valve Class :
            </td>
            
            <td align="left">
            <asp:TextBox ID="txtvalve" runat="server" CssClass="TextBox"
            MaxLength="50" Width="350px">
            </asp:TextBox>
            </td>
            
            <td>
            <asp:RequiredFieldValidator ID="Req1" runat="server"
            ControlToValidate="txtvalve" Display="None"
            ErrorMessage="valve Type  Required" SetFocusOnError="True"
            ValidationGroup="Add">
            </asp:RequiredFieldValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
            Enabled="True" TargetControlID="Req1"
            WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        
     
        
        </table>
               </fieldset>
    </td>
     </tr>
     
          <tr>
           <td>  
           <fieldset id="fieldset1" runat="server" class="FieldSet">
            <table width="100%">
                 <tr>
                 <td align="center" colspan="3">
                 <table align="center" width="25%">
              
                 <tr>
            <td>
            <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="button"
            ValidationGroup="Add" onclick="BtnUpdate_Click" />
            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server"
            ConfirmText="Would You Want To Upadte The Record ?" 
            TargetControlID="BtnUpdate">
            </ajax:ConfirmButtonExtender> 
            </td>
            
            <td>
            <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button"
            ValidationGroup="Add" onclick="BtnSave_Click" />
            </td>
            
            <td>
            <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="button"
            ValidationGroup="Add" onclick="BtnDelete_Click" />
            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server"
            ConfirmText="Would You Want To Delete This Record ?"
            TargetControlID="BtnDelete">
            </ajax:ConfirmButtonExtender>
            </td> 
            
            <td>
            <asp:Button ID="BtnCancel" runat="server" Text="Cancel"
            CssClass="button" CausesValidation="False" onclick="BtnCancel_Click"/>
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
<asp:Content ID="Content4" ContentPlaceHolderID="ReportTitle" Runat="Server">
        Valve List    
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
              <asp:LinkButton ID="lbtn_List" CssClass="linkButton" CausesValidation="false"
                CommandName="Select" CommandArgument='<%# Eval("#") %>' runat="server"
                 Text='<%# Eval("pd") %>'>
              </asp:LinkButton>
                                 
            </li>
            </ItemTemplate>    
            </asp:Repeater>
    </ul>
         </div>
       </ContentTemplate>
 </asp:UpdatePanel>       
</asp:Content>
