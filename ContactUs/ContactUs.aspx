<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs_ContactUs" Title="Contact Us" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolScriptManager" runat="server" />

<script type="text/javascript"
src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false">
</script>
<script type="text/javascript">
function initialize() {
    var myLatlng = new google.maps.LatLng(18.504338100000000000, 73.902913799999960000)
var mapOptions = {
center: myLatlng,
zoom: 8,
mapTypeId: google.maps.MapTypeId.ROADMAP,
marker: true
};
var map = new google.maps.Map(document.getElementById("map_canvas"),mapOptions);
}
</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
Contact Us..
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<table width="100%">
<tr><td >
<div id="map_canvas" style="width: 500px; height: 400px; z-index:auto"></div>
</td>
<td valign="top" align="center" style="width:70%">
<div id="Div1" >
<table width="100%" cellspacing="15">
<tr>
<td>
<asp:Image runat="server" ID="IMGCONTACTUS" ImageUrl="~/Images/EONImages/CONTACTUS.jpg" Width="230px" Height="80px" />
</td>
</tr>

<tr>
<td>
<asp:Label runat="server" ID="Label2" Text="Revo Dynamics" CssClass="LabelTimer"></asp:Label>
</td>
</tr>
<tr>
<td>
&nbsp;
</td>
</tr>

<tr>
<td>
<asp:Label runat="server" ID="Label3" Text="SB 504, Sacred World," CssClass="LabelTimerSUB"></asp:Label>
</td>
</tr>

<tr>
<td>
<asp:Label runat="server" ID="Label6" Text="Wanowrie," CssClass="LabelTimerSUB"></asp:Label>
</td>
</tr>

<tr>
<td>
<asp:Label runat="server" ID="Label1" Text="Pune 411040." CssClass="LabelTimerSUB"></asp:Label>
</td>
</tr>

<tr>
<td>
<asp:Label runat="server" ID="Label7" Text="Tel: +91 - 020 - 40055251" CssClass="LabelTimerSUB"></asp:Label>
</td>
</tr>

<tr>
<td>
<asp:Label runat="server" ID="Label5" Text="Email: revosolutionpune@yahoo.com" CssClass="LabelTimerSUB"></asp:Label>
</td>
</tr>
</table>

</div>




</td>
</tr>
</table>

</asp:Content>

