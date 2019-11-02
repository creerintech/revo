<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="NotAuthUser.aspx.cs" Inherits="ErrorPages_NotAuthUser" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
Sorry U Have No Authority!!!
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<table id="Table1" runat="server" width="100%">
<tr>
<td align="center">
</td>
</tr>

<tr>
<td align="center">
<asp:Image ID="Image3" runat="server" ImageUrl="~/Images/New Icon/access_denied1.jpg" Height="400px" Width="600px" /> 
</td>
</tr>
</table>
</asp:Content>

