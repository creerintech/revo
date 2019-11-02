<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Masters_Default" Title="Home" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
<script language="javascript"  type="text/javascript">
//preload images
var image1=new Image();
image1.src="../Images/EONImages/eon-it-park-kharadi-pune-ireo-panchshil.png"
var image2=new Image();
image2.src="../Images/EONImages/e-on_coffee_sign.png"
var image3=new Image();
image3.src="../Images/EONImages/images (2).png"


</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">


<script type="text/javascript">
<!--
var step=1
var whichimage=1
function slideit(){
if (!document.images)
return
document.images.slide.src=eval("image"+step+".src")
whichimage=step
if (step<3)
step++
else
step=1
setTimeout("slideit()",5000)
}
slideit()
function slidelink(){
if (whichimage==1)
window.location="link1.htm"
else if (whichimage==2)
window.location="link2.htm"
else if (whichimage==3)
window.location="link3.htm"
}
//-->
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ReportTitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Report" Runat="Server">
<table cellspacing="20">

<tr><td>
<asp:Label runat="server" ID="LBLSITE" CssClass="LabelPOP" Text="Please Select Site"></asp:Label>
</td></tr>

<tr><td>
<asp:RadioButtonList ID="RdoSite" runat="server" CellPadding="25"  AutoPostBack="true"
RepeatDirection="Vertical" CssClass="LabelRDO" 
 onselectedindexchanged="RdoSite_SelectedIndexChanged"></asp:RadioButtonList>
</td></tr>
<tr><td>
<hr width="100%" />
</td></tr>

<tr><td>
        <ul>
          <li><img src="../Images/New Icon/icon1.jpeg" height="16px" width="16px" /><a href="../Transactions/MaterialRequisitionTemplate.aspx"  class="linkButtonMenu">Material 
              Requisition</a></li>
          <li><img src="../Images/New Icon/icon1.jpeg" height="16px" width="16px" /><a href="../Transactions/PurchaseOrderDtls.aspx" class="linkButtonMenu">Purchase 
              Order To Supplier</a></li>
          <li><img src="../Images/New Icon/icon1.jpeg" height="16px" width="16px" /><a href="../Transactions/EditPurchaseOrder.aspx"  class="linkButtonMenu">Approve 
              &amp; Authorise PO</a></li>
          <li><img src="../Images/New Icon/icon1.jpeg" height="16px" width="16px" /><a href="../Transactions/MaterialInwardRegister1.aspx"  class="linkButtonMenu">Inward 
              Register</a></li>
        </ul>
</td></tr></table>
</asp:Content>

