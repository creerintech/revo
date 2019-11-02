<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="BackupData.aspx.cs" Async="true" Inherits="Utility_BackupData" Title="Database Backup" %>
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
    Back-Up DataBase 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<table width="100%" cellspacing="10" align="center">
<tr align="center">
<td class="Label">Date :</td>
<td align="left">
<asp:Label runat="server" Font-Bold="true" ID="lblDate"></asp:Label>
</td>
<td align="left">
<asp:Button ID="BtnBackup" CssClass="buttonLOGIN" runat="server" Text="BACKUP" 
onclick="BtnBackup_Click" />
</td>
<td align="left">
<asp:Button ID="BTNDELETEFILE" CssClass="buttonLOGIN" runat="server" Text="DELETE" 
onclick="BTNDELETEFILE_Click" />
</td>
</tr>
<tr><td colspan="3">
</td></tr>
<tr><td colspan="3"></td></tr>
<tr><td colspan="3"></td></tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

