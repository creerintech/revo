<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Masters_ProrityMaster" Title="Change Password" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
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
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Change Password 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
   
   <table width="100%">
  <tr>
  <td>
 <fieldset id="fieldset" runat="server" class="FieldSet">
 <table width="100%" cellspacing="6">
       <tr>
            <td class="Label">
            User Name :
            </td>
            <td align="left">
            <asp:Label ID="LblUserName" runat="server" CssClass="Label" Text=""></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        
         <tr>
            <td class="Label">
            For Site :
            </td>
            <td align="left">
            <asp:Label ID="LblLocation" runat="server" CssClass="Label" Text=""></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        
         <tr>
            <td class="Label">
                Old Password :
            </td>
            
            <td align="left">
            <asp:TextBox ID="OPassword" runat="server" CssClass="TextBox"
            MaxLength="50" Width="350px" TextMode="Password"></asp:TextBox>
            <asp:TextBox ID="CmpOPassword" runat="server" CssClass="Display_None"
            MaxLength="50" Width="20px" Enabled="false"></asp:TextBox>
            </td>
            
            <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
            ControlToValidate="OPassword" Display="None"
            ErrorMessage="Old Password Is Required" SetFocusOnError="True"
            ValidationGroup="Add"></asp:RequiredFieldValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
            Enabled="True" TargetControlID="RequiredFieldValidator2"
            WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender>
            
            
              <asp:CompareValidator ID="CompareValidator1" runat="server" SetFocusOnError="true"
                        ErrorMessage="Enter Old Password Is Incorrect" 
                        ControlToCompare="CmpOPassword" ControlToValidate="OPassword" 
                        Display="None" ValidationGroup="Add"></asp:CompareValidator>
                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat ="server"
                        TargetControlID ="CompareValidator1"  WarningIconImageUrl="~/Images/Icon/Warning.png" >
                    </ajax:ValidatorCalloutExtender></td>
                    
            </td>
        </tr>
        
        
        <tr>
            <td class="Label">
                New Password :
            </td>
            
            <td align="left">
            <asp:TextBox ID="Password" runat="server" CssClass="TextBox"
            MaxLength="50" Width="350px" TextMode="Password"></asp:TextBox>
            </td>
            
            <td>
            <asp:RequiredFieldValidator ID="Req1" runat="server"
            ControlToValidate="Password" Display="None"
            ErrorMessage="Password Is Required" SetFocusOnError="True"
            ValidationGroup="Add"></asp:RequiredFieldValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
            Enabled="True" TargetControlID="Req1"
            WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender>
            <ajax:PasswordStrength ID="PasswordStrength1" runat="server" 
            PreferredPasswordLength="6" TargetControlID="Password">
            </ajax:PasswordStrength>
            </td>
        </tr>
        
          <tr>
            <td class="Label">
                Confirm Password :
            </td>
            
            <td align="left">
            <asp:TextBox ID="CPassword" runat="server" CssClass="TextBox"
            MaxLength="50" Width="350px" TextMode="Password"></asp:TextBox>
            </td>
            
            <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ControlToValidate="CPassword" Display="None"
            ErrorMessage="Confirm Password Is Required" SetFocusOnError="True"
            ValidationGroup="Add"></asp:RequiredFieldValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
            Enabled="True" TargetControlID="RequiredFieldValidator1"
            WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender>
            
               <asp:CompareValidator ID="CPV1" runat="server" 
                        ErrorMessage="Password Should Match..!" 
                        ControlToCompare="Password" ControlToValidate="CPassword" 
                        Display="None" ValidationGroup="Add"></asp:CompareValidator>
                    <ajax:ValidatorCalloutExtender ID="Ajax_RC_Validator1" runat ="server"
                        TargetControlID ="CPV1"  WarningIconImageUrl="~/Images/Icon/Warning.png" >
                    </ajax:ValidatorCalloutExtender></td>
            
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
            ValidationGroup="Add" onclick="BtnUpdate_Click"  />
            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server"
            ConfirmText="Would You Want To Upadte The Record ?" 
            TargetControlID="BtnUpdate">
            </ajax:ConfirmButtonExtender> 
            </td>
            <td>
            <asp:Button ID="BtnCancel" runat="server" Text="Cancel"
            CssClass="button" CausesValidation="False" onclick="BtnCancel_Click" />
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

