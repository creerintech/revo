<%@ Page Title="Change Status" Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="CHANGESTATUSPOIND.aspx.cs" Inherits="Masters_CHANGESTATUSPOIND" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolScriptManager" runat="server" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
  
   <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
    <ProgressTemplate>            
    <div id="progressBackgroundFilter"></div>
    <div id="processMessage">   
    <center><span class="SubTitle">Loading....!!! </span></center>
    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
    </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>
  
     <%-- Search For Unit :
    <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" 
          ToolTip="Enter The Text" Width="292px" AutoPostBack="True" 
          ontextchanged="TxtSearch_TextChanged">
     </asp:TextBox>

    <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
     TargetControlID="TxtSearch" CompletionInterval="100"                             
     UseContextKey="True" FirstRowSelected ="true" ShowOnlyCurrentWordInCompletionListItem="true"
    ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender"
    CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
    </ajax:AutoCompleteExtender> --%>
 </ContentTemplate>
 </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
Change Status
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
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    RepeatDirection="Horizontal" AutoPostBack="True" 
                    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Value="1" Selected="True">PO</asp:ListItem>
                    <asp:ListItem Value="2">IND</asp:ListItem>
                </asp:RadioButtonList>
            </td>
                <td>
                </td>
        </tr>
        <tr>
            <td class="Label2">
                <asp:Label ID="Lblststus" runat="server" CssClass="Label" Width="100px"></asp:Label>
            </td>
            
            <td align="left">
            <asp:TextBox ID="txtPono" runat="server" CssClass="TextBox"
            MaxLength="50" Width="350px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="Req_Name" runat="server" 
                    ControlToValidate="txtPono" Display="None" 
                    ErrorMessage="No is Required" SetFocusOnError="True" 
                    ValidationGroup="Add">
                </asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" 
                    TargetControlID="Req_Name" WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>
                <asp:Button ID="BtnCheck" runat="server" CssClass="button" 
                    onclick="BtnCheck_Click" Text="Check" ValidationGroup="Add" />
            </td>
            
            <td>
          <%--  <asp:RequiredFieldValidator ID="Req1" runat="server"
            ControlToValidate="TxtUnit" Display="None"
            ErrorMessage="Unit Is Required" SetFocusOnError="True"
            ValidationGroup="Add">
            </asp:RequiredFieldValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
            Enabled="True" TargetControlID="Req1"
            WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender>--%>
            </td>
        </tr>
        
        <tr>
            <td class="Label">
            </td>
            <td align="left">
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
           <fieldset id="fieldset1" runat="server" class="FieldSet">
            <table width="100%">
                 <tr>
                 <td align="center">
                 <table align="center" width="25%">
              
                 <tr>
            <td>
            <asp:Button ID="BtnUpdate" runat="server" Text="Change Status" CssClass="button"
            ValidationGroup="Add" onclick="BtnUpdate_Click" Width="90px" />
            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server"
            ConfirmText="Would You Want To Change Status The Record ?" 
            TargetControlID="BtnUpdate">
            </ajax:ConfirmButtonExtender> 
            </td>
            
            <td>
                &nbsp;</td>
            
            <td>
            <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="button"
            ValidationGroup="Add" Visible="False" />
            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server"
            ConfirmText="Would You Want To Delete This Record ?"
            TargetControlID="BtnDelete">
            </ajax:ConfirmButtonExtender>
            </td> 
            
            <td>
            <asp:Button ID="BtnCancel" runat="server" Text="Cancel"
            CssClass="button" CausesValidation="False"
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
    </table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ReportTitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Report" Runat="Server">
</asp:Content>

