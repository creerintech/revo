<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Login"  Title="Welcome To Revo Dynamics"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TECH BUILD LOGIN</title>
     <link rel="stylesheet" type="text/css" href="StyleSheet/StyleSheet.css"/>
     <link rel="stylesheet" type="text/css" href="StyleSheet/style.css"/>
     <link rel="stylesheet" type="text/css" href="StyleSheet/MenuStyle.css"/>    
    <style type="text/css"> 
     #mydiv {
    position:absolute;
    top: 45%;
    left: 50%;
    width:42.0em;
    height:19.6em;
    margin-top: -9em; /*set to a negative number 1/2 of your height*/
    margin-left: -15em; /*set to a negative number 1/2 of your width*/
    border: 0px solid #FFFFFF;
    background-color: #FFFFFF;
    }
    .water 
    {
    	color:Gray;
    }
 </style>
 
</head>
<body>
<form id="form1" runat="server" >
<script language="javascript" type="text/javascript">
function ClickDoneBtn40()
{
var key = window.event.keyCode;
if (key == 145)
{
var _TxtValue = document.getElementById('<%= LBLSERIALNO.ClientID %>');  
alert(_TxtValue.value);
}
else
{

}
}

document.send.inputText.onkeypress = function(event){
var key = window.event.keyCode;
if (key == 145)
{
var _TxtValue = document.getElementById('<%= LBLSERIALNO.ClientID %>');  
alert(_TxtValue.value);
}
else
{

}
    };
</script>
<div id="mydiv" >
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
<asp:UpdateProgress ID="UpdateProgress1" runat="server" >
<ProgressTemplate>            
<div id="progressBackgroundFilter"></div>
<div id="processMessage">   
<center><span class="SubTitle">Loading....!!! </span></center>
<asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
</div>
</ProgressTemplate>
</asp:UpdateProgress>
<fieldset style="width: 80%; background-image: url('Images/Login/old-interior1.jpg'); height:110%; background-repeat:inherit" id="FS" runat="server" >

<div>
<table width="100%"><tr><td align="center">
<table cellspacing="8" >

<tr><td colspan="2" align="center">
<img src="Images/MasterPages/Revodynamic.png"  width="200px" height="70px"></img>
    </td></tr>
    
    
<tr><td colspan="2" align="center">
<asp:Label runat="server" style="color:#0B0B61; font-weight:bold;font-size:24px;font-family:Gaps;" Text="" ></asp:Label>
    </td></tr>
    
    <tr><td colspan="2" align="center">
    </td></tr>
    
<tr>
<td class="LabeLog"> User Name :</td>
<td>
<asp:TextBox ID="TxtUserName" runat="server" Width="200px" CssClass="TextBoxLOGIN" Height="20px" onkeyup="ClickDoneBtn40();"></asp:TextBox>
<ajax:RoundedCornersExtender ID="RoundedCornersExtender4" runat="server" TargetControlID="TxtUserName" Corners="All" Radius="8" BorderColor="Gray"></ajax:RoundedCornersExtender>          
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
ControlToValidate="TxtUserName" Display="None" 
ErrorMessage="User Name Required" SetFocusOnError="True" 
ValidationGroup="Add"></asp:RequiredFieldValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
Enabled="True" TargetControlID="RequiredFieldValidator1" 
WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>

<ajax:TextBoxWatermarkExtender ID="TBWE2" runat="server"
    TargetControlID="TxtUserName"
    WatermarkText="User Name"
    WatermarkCssClass="water" />
  
    <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
      TargetControlID="TxtUserName" CompletionInterval="100"                             
      UseContextKey="True" FirstRowSelected ="true" ShowOnlyCurrentWordInCompletionListItem="true"
       ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender"
      CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
    </ajax:AutoCompleteExtender>  
    
</td>
</tr>
<tr>
<td class="LabeLog"> Password :</td>
<td>
<asp:TextBox ID="TxtPass" runat="server" Width="200px" TextMode="Password" Height="20px" CssClass="TextBoxLOGIN" ></asp:TextBox>
<ajax:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="TxtPass" Corners="All" Radius="8" BorderColor="Gray"></ajax:RoundedCornersExtender>          
<asp:RequiredFieldValidator ID="Req1" runat="server" 
ControlToValidate="TxtPass" Display="None" 
ErrorMessage="Password Required" SetFocusOnError="True" 
ValidationGroup="Add"></asp:RequiredFieldValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
Enabled="True" TargetControlID="Req1" 
WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>
<ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
    TargetControlID="TxtPass"
    WatermarkText="Password"
    WatermarkCssClass="water" />    
</td>
</tr>
<tr visible="false">
<td class="Display_None"> Site :</td>
<td>
<asp:DropDownList ID="DDLLoc" runat="server" Width="204px" CssClass="Display_None" Height="20px"  AutoPostBack="false"></asp:DropDownList>
<%--<ajax:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="DDLLoc" Corners="All" Radius="8" BorderColor="Gray"></ajax:RoundedCornersExtender>          
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
ControlToValidate="DDLLoc" Display="None" 
ErrorMessage="Please Select Location" SetFocusOnError="True" InitialValue="0"
ValidationGroup="Add"></asp:RequiredFieldValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" 
Enabled="True" TargetControlID="RequiredFieldValidator2" 
WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>--%>
</td>
</tr>
<tr>
<td class="LabeLog"> </td>
<td align="right">
<asp:Button ID="BtnLogin" CssClass="buttonLOGIN" runat="server" Text="Sign In" ValidationGroup="Add"
BorderColor="WhiteSmoke" Font-Bold="true" onclick="BtnLogin_Click"/>
<ajax:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server" TargetControlID="BtnLogin" Corners="All" Radius="8" BorderColor="Gray"></ajax:RoundedCornersExtender>          

<asp:Button ID="BtnCancel" CssClass="buttonLOGIN" runat="server" Text="Reset" 
BorderColor="WhiteSmoke" Font-Bold="true" onclick="BtnCancel_Click"/>
<ajax:RoundedCornersExtender ID="RoundedCornersExtender5" runat="server" TargetControlID="BtnCancel" Corners="All" Radius="8" BorderColor="Gray"></ajax:RoundedCornersExtender>          
</td>
</tr>
<tr>
<td class="LabeLog"> </td>
<td>
<asp:TextBox ID="LBLSERIALNO" runat="server" Enabled="false" CssClass="Display_None"></asp:TextBox>
</td>
</tr>
</table>
</td></tr></table>
</div>
</fieldset>
<ajax:DropShadowExtender ID="dse" runat="server" TargetControlID="FS" Opacity=".6" Rounded="true" TrackPosition="true"  />
<ajax:RoundedCornersExtender ID="RoundedCornersExtender6" runat="server" TargetControlID="FS" Corners="All" Radius="19" BorderColor="Gray" ></ajax:RoundedCornersExtender>          
</div>
</form>
</body>
</html>
