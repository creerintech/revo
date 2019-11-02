<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="SendeMail.aspx.cs" Inherits="Utility_SendeMail" Title="E-Mail" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"/>
<asp:UpdatePanel ID="UpdatePanel5" runat="server">
<ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" >
<ProgressTemplate>            
<div id="progressBackgroundFilter"></div>
<div id="processMessage">   
<center><span class="SubTitle">Loading....!!! </span></center>
<asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" 
Height="20px" Width="120px" />                                
</div>
</ProgressTemplate>
</asp:UpdateProgress>
 </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Email-Utility 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<table width="100%" cellspacing="6" align="center">
<tr align="center">
<td colspan="2" align="left">
<fieldset id="F1" runat="server" class="FieldSet">
<table width="100%" cellspacing="8">
<tr>
<td class="Label">To :</td>
<td colspan="3">
<asp:TextBox runat="server" ID="TxtTo" CssClass="TextBox" Width="450px"></asp:TextBox>
<ajax:RoundedCornersExtender ID="RoundedCornersExtender4" runat="server" TargetControlID="TxtTo" Corners="All" Radius="6" BorderColor="Gray"></ajax:RoundedCornersExtender>          

<asp:RequiredFieldValidator ID="Rq_V2" runat="server" 
ControlToValidate="TxtTo" CssClass="Error" Display="None" 
ErrorMessage="Please Enter E-MailID" ValidationGroup="Add"></asp:RequiredFieldValidator>
<ajax:ValidatorCalloutExtender ID="Rq_V2_ValidatorCalloutExtender" 
runat="server" TargetControlID="Rq_V2" 
WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>
<asp:RegularExpressionValidator ID="REV2" runat="server" Display="None" 
ErrorMessage="Please Enter Valid Email ID..!" ControlToValidate="TxtTo" 
ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Add">
</asp:RegularExpressionValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" 
Enabled="True" TargetControlID="REV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>  
</td>
</tr>
<tr>
<td class="Label"></td>
<td style="color:Red; font-size:smaller">
* For Multiple Mail Address Please Seperate by Semo-Colon ( ; )
</td>
</tr>
<tr>
<td class="Label">CC :</td>
<td colspan="3">
<asp:TextBox runat="server" ID="TxtCC" CssClass="TextBox" Width="450px"></asp:TextBox>
<ajax:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="TxtCC" Corners="All" Radius="6" BorderColor="Gray"></ajax:RoundedCornersExtender>          

<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None" 
ErrorMessage="Please Enter Valid Email ID..!" ControlToValidate="TxtCC" 
ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Add">
</asp:RegularExpressionValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
Enabled="True" TargetControlID="RegularExpressionValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>
</td>
</tr>
<tr>
<td class="Label">BCC :</td>
<td colspan="3">
<asp:TextBox runat="server" ID="TxtBCC" CssClass="TextBox" Width="450px"></asp:TextBox>
<ajax:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="TxtBCC" Corners="All" Radius="6" BorderColor="Gray"></ajax:RoundedCornersExtender>          

<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None" 
ErrorMessage="Please Enter Valid Email ID..!" ControlToValidate="TxtBCC" 
ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Add">
</asp:RegularExpressionValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
Enabled="True" TargetControlID="RegularExpressionValidator2" WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>
</td>
</tr>
<tr>
<td class="Label">Subject :</td>
<td colspan="3">
<asp:TextBox runat="server" ID="TxtSubject" CssClass="TextBox" Width="450px" 
        MaxLength="450"></asp:TextBox>
<ajax:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server" TargetControlID="TxtSubject" Corners="All" Radius="6" BorderColor="Gray"></ajax:RoundedCornersExtender>          
        
</td>
</tr>
<tr>
<td class="Label"></td>
<td>
<asp:UpdatePanel ID="UpdatePanel4" runat="server"> 
<Triggers ><asp:PostBackTrigger ControlID ="lnkAttachedFile" /></Triggers>
<ContentTemplate>
<asp:CheckBox ID="ChkAttach1" runat="server" AutoPostBack="True" 
        CssClass="CheckBox" oncheckedchanged="ChkAttach1_CheckedChanged"  />
<asp:FileUpload ID="FileUploader1" runat="server" size="50" CssClass="TextBox" 
        BorderStyle="None" Font-Names="Candara" />
        
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
<tr>
<td class="Label"></td>
<td>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
<Triggers ><asp:PostBackTrigger ControlID ="lnkAttachedFile" /></Triggers>
<ContentTemplate>
<asp:CheckBox ID="ChkAttach2" runat="server" AutoPostBack="True" 
        CssClass="CheckBox" oncheckedchanged="ChkAttach2_CheckedChanged"  />
<asp:FileUpload ID="FileUpload2" runat="server" size="50" CssClass="TextBox" 
        BorderStyle="None" Font-Names="Candara" />
        
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
<tr>
<td class="Label"></td>
<td>
<asp:UpdatePanel ID="UpdatePanel3" runat="server"> 
<Triggers ><asp:PostBackTrigger ControlID ="lnkAttachedFile" /></Triggers>
<ContentTemplate>
<asp:CheckBox ID="ChkAttach3" runat="server" AutoPostBack="True" 
        CssClass="CheckBox" oncheckedchanged="ChkAttach3_CheckedChanged"  />
<asp:FileUpload ID="FileUpload3" runat="server" size="50" CssClass="TextBox" 
        BorderStyle="None" Font-Names="Candara" />
        
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
<tr>
<td class="Label"></td>
<td>
<asp:LinkButton ID="lnkAttachedFile" runat="server" CssClass="linkButton" 
        onclick="lnkAttachedFile_Click">Attach</asp:LinkButton>
</td>
</tr>
<tr>
<td class="Label" width="30%">Body :</td>
<td colspan="3">
</td>
</tr>
<tr>
<td class="Label"></td>
<td colspan="3">
<asp:TextBox runat="server" ID="TxtBody" CssClass="TextBox" Width="450px" Height="150px" TextMode="MultiLine"></asp:TextBox>
<ajax:RoundedCornersExtender ID="RCB" runat="server" TargetControlID="TxtBody" Corners="All" Radius="6" BorderColor="Gray"></ajax:RoundedCornersExtender>
<ajax:HtmlEditorExtender runat="server" ID="bhtml" TargetControlID="TxtBody">
<Toolbar>
<ajax:Undo />
<ajax:Redo />
<ajax:Bold />
<ajax:Italic />
<ajax:Underline />
<ajax:StrikeThrough />
<ajax:Subscript />
<ajax:Superscript />
<ajax:JustifyLeft />
<ajax:JustifyCenter />
<ajax:JustifyRight />
<ajax:JustifyFull />
<ajax:InsertOrderedList />
<ajax:InsertUnorderedList />
<ajax:CreateLink />
<ajax:UnLink />
<ajax:RemoveFormat />
<ajax:Cut />
<ajax:Copy />
<ajax:Paste />
<ajax:BackgroundColorSelector />
<ajax:ForeColorSelector />
<ajax:InsertHorizontalRule />
<ajax:FontNameSelector />
<ajax:FontSizeSelector />
</Toolbar>
</ajax:HtmlEditorExtender>
</td>
</tr>
<tr>

<td colspan="4" align="center">

</td>
</tr>
<tr>

<td colspan="4" align="center">

</td>
</tr>
<tr>

<td colspan="4" align="center">

</td>
</tr>
<tr>

<td colspan="4" align="center">
<asp:Button ID="BtnSend" CssClass="button" runat="server" Text="Send" 
ValidationGroup="Add" onclick="BtnSend_Click"  />

<asp:Button ID="BtnCancel" CssClass="button" runat="server" Text="Cancel" 
        CausesValidation="False" onclick="BtnCancel_Click" />
<ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Would You Like To Cancel ?"
TargetControlID="BtnCancel"></ajax:ConfirmButtonExtender>
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

