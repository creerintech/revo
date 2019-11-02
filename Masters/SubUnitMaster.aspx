<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="SubUnitMaster.aspx.cs" Inherits="Masters_SubUnitMaster" Title="SubUnitMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" >
       <ProgressTemplate>            
    <div id="progressBackgroundFilter"></div>
    <div id="processMessage">   
    <center><span class="SubTitle">Loading....!!! </span></center>
    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
    </div>
    </ProgressTemplate>
    </asp:UpdateProgress>

        Search for Employee :
        <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
        Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged"></asp:TextBox>
        <div id="div1"></div>
        <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
          TargetControlID="TxtSearch"  CompletionInterval="100"                               
        UseContextKey="True" FirstRowSelected ="True" 
          ShowOnlyCurrentWordInCompletionListItem="True"  ServiceMethod="GetCompletionList"
        CompletionListCssClass="AutoExtender" 
          CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
        DelimiterCharacters="" Enabled="True" ServicePath="" MinimumPrefixLength="2"></ajax:AutoCompleteExtender> 

    <%--<input type="submit" value="Search" class="submit"/>--%>
 </ContentTemplate>                             
 </asp:UpdatePanel>                         
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    SubUnit Master       
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
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
            <td class="Label">Unit :</td>
            <td>
            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="ComboBox" 
            Width="142px" ValidationGroup="Add">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RFV2" runat="server" 
            ControlToValidate="ddlUnit" Display="None" 
            ErrorMessage="Unit is Required" SetFocusOnError="True" 
            ValidationGroup="AddGrid" InitialValue="0"></asp:RequiredFieldValidator>
            <ajax:ValidatorCalloutExtender ID="VCE1" runat="server" Enabled="True" 
            TargetControlID="RFV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender>
            </td>
            </tr>
         
            <tr>
            <td class="Label">
                SubUnit :</td>
            <td align="left">
            <asp:TextBox ID="TxtSubUnit" runat="server" CssClass="TextBox" 
            MaxLength="90" Width="142px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="Req1" runat="server" 
            ControlToValidate="TxtSubUnit" Display="None" 
            ErrorMessage="SubUnit Required" SetFocusOnError="True" 
            ValidationGroup="Add"></asp:RequiredFieldValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
            Enabled="True" TargetControlID="Req1" 
            WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender>
            </td>
            </tr>  
           

        <tr>
            <td class="Label">
                Conversion Factor :</td>
               <td align="left" >
                   <asp:TextBox ID="TxtConversnFactor" runat="server" CssClass="TextBox" 
                       MaxLength="90" Width="142px"></asp:TextBox>
                   (e.g 1Kg=1000gm)</td>
            <td>
            </td>
        </tr>
            <tr>
                <td class="Label">
                    &nbsp;</td>
                <td align="left">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
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
        <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="button" TabIndex="17" 
        ValidationGroup="Add" onclick="BtnUpdate_Click"/>
        <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
        ConfirmText="Would You Like To Update the Record ..! "
        TargetControlID="BtnUpdate" >
        </ajax:ConfirmButtonExtender>
        </td>
        <td>
        <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" TabIndex="18" 
        ValidationGroup="Add" onclick="BtnSave_Click"/>

        </td>
        <td>
        <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="button" 
        TabIndex="19" CausesValidation="False" ValidationGroup="Add" 
                onclick="BtnDelete_Click"  />
        <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" 
        ConfirmText="Would You Like To Delete the Record ..! "
        TargetControlID="BtnDelete" >
        </ajax:ConfirmButtonExtender>
        </td>
        <td>
        <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="button" 
        TabIndex="19" CausesValidation="False" ValidationGroup="Add" 
                onclick="BtnCancel_Click" />

      

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
    SubUnits List      
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Report" Runat="Server">                 
 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate >
    <ul id="subnav">
            <%--Ul Li Problem Solved repeater--%>
            <asp:Repeater ID="GrdReport" runat="server" 
                onitemcommand="GrdReport_ItemCommand">
            <ItemTemplate>
            <li id="Menuitem" runat="server" >                              
              <asp:LinkButton ID="lbtn_List" CssClass="linkButton"
                CausesValidation="false"
                CommandName="Select"
                CommandArgument='<%# Eval("#") %>'
                runat="server"
                 Text='<%# Eval("Name") %>'
                ></asp:LinkButton>
                                 
            </li>
            </ItemTemplate>    
            </asp:Repeater>
            </ul>
            <%--Ul Li Problem Solved repeater--%>
            <%--<table width="100%">
            <asp:Repeater ID="GrdReport" runat="server" onitemcommand="GrdReport_ItemCommand">    
           <ItemTemplate>
           <tr>
        <td>
                <asp:LinkButton ID="lbtn_List" CssClass="linkButton"
                                CausesValidation="false"
                                CommandName="Select"
                                CommandArgument='<%# Eval("#") %>'
                                runat="server"
                                style="padding-left:7px; padding-top: 10px;" Text='<%# Eval("Name") %>'
                                ></asp:LinkButton>
                                </td>
     </tr>
            </ItemTemplate>   
            </asp:Repeater>   
            </table>     --%>
              </ContentTemplate>
       </asp:UpdatePanel>                  
</asp:Content>

