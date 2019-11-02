<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPageError.master" AutoEventWireup="true" CodeFile="UserAccessDenied.aspx.cs" Inherits="ErrorPages_UserAccessDenied" Title="Access Denied" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<table id="Table1" runat="server" width="100%">
<tr>
<td align="center">
<asp:Label runat="server" ID="LBLERROR" CssClass="Label_DynamicAccess"></asp:Label>
</td>
</tr>

<tr>
<td align="center">
<asp:Image ID="Image3" runat="server" ImageUrl="~/Images/New Icon/access_denied1.jpg" Height="400px" Width="600px" /> 
</td>
</tr>
</table>
</asp:Content>


