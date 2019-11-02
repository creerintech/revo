<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="RequsitionCancelPrint.aspx.cs" Inherits="Reports_PurchaseOrderPrint" Title="Material Requisition Cancellation Print" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<div>
<fieldset id="fieldset1" class="FieldSet">
<table width="100%">
        <tr>
        <td colspan="5" align="right" style="height: 36px">
        <asp:ImageButton ID="ImgPrint" runat="server" 
        ImageUrl="~/Images/Icon/Print-Button.png" CssClass="Imagebutton"
        OnClientClick="javascript:CallPrint('divPrint')"
        ToolTip="PDF" Height="35px" Width="35px"/>
        <asp:ImageButton ID="ImgPdf" runat="server" ImageUrl="~/Images/Icon/PDF.jpg" CssClass="Imagebutton"
        ToolTip="Print" Height="35px" Width="35px" onclick="ImgPdf_Click"/>
        </td>
        </tr>
        <tr><td colspan="5">
<table id="divPrint" cellspacing="5" width="100%">
<tr>
<td colspan="5">
<div > 
<table width="100%" align="center" cellspacing="4">
            
           
            <tr>
               <td colspan="4" >
               <asp:Image ID="imgAntTime" runat="server" CssClass="image" width="150px" Height="50px" /></td>
            </tr>
            <tr>
            <td class="Label_Dynamic" colspan="4">
            <asp:Label ID="lblCompanyName" runat="server" Text="CompanyName" Font-Bold="True"></asp:Label><br />
            <asp:Label ID="lblCompanyAddress" runat="server" Text="CompanyAddresss" Font-Bold="True"></asp:Label><br />
            <b>PhoneNo:</b><asp:Label ID="lblPhnNo" runat="server" Text="phoneNo" Font-Bold="True"></asp:Label><br />
            <b>FaxNo:</b><asp:Label ID="lblFaxNo" runat="server" Text="FaxNo" Font-Bold="True"></asp:Label><br />
            </td>
            </tr>
            <tr>
                <td colspan="4"  align="center" class="SubTitle">
                    <h2>Requisition Cancellation</h2></td>
            </tr>
        
            <tr>
                <td colspan="4"  align="center" class="SubTitle">
                    &nbsp;</td>
            </tr>
        
            <tr>
                <td class="Label_Dynamic" align="right">
                    CancelledBy :</td>
                <td >
            <asp:Label ID="lblCancelBy" runat="server" Font-Bold="True" 
                Text="lblCancelBy"></asp:Label>
                                                     </td>
                
                <td class="Label_Dynamic" align="right" >
                    Cancellation Date :</td>
                <td  >
            <asp:Label ID="lblCancelDate" runat="server" Font-Bold="True" 
                Text="lblCancelDate"></asp:Label>
                                                     </td>
               
            </tr>
                
            <tr>
               
                 <td  class="Label_Dynamic" align="right" >
                     Requisition CancelNo:</td>
                <td colspan="3" style="height: 20px" >
            <asp:Label ID="lblReqCancelNo" runat="server" Font-Bold="True" 
                Text="ReqCancelNo"></asp:Label>
                                                     </td>
               
            </tr>
            
                        
            <tr>
            <td colspan="4">
            <table width="100%">
            <tr>
            <td>
            <div id="divProDetails">
            <asp:GridView ID="RequsitionGrid" runat="server" 
            CssClass="mGrid"   width="100%">
            </asp:GridView>
            </div>
            </td>
            </tr>
            <tr>
            <td class="Label_Dynamic" align="left">
            <h3>&nbsp;</h3>
            </td>

            </tr>
            <tr>
            <td>
            <div id="divHWDetails">
            </div>
            </td>
            </tr>
            </table>
            </td>        
            </tr>   
        </table>
</div>
  </td>
  </tr>


<tr>
<td colspan="5">
<table width="100%">
<tr>
<td class="Label1" width="30%">Prepared By :</td>
<td ></td>
<td class="Label1" width="30%">Authorised By :</td>
<td></td>
</tr> 
<tr>
<td class="Label1" width="30%"></td>
<td></td>
<td class="Label1" width="30%"></td>
<td ></td>
</tr> 
<tr>
<td class="Label1" ></td>
<td></td>
<td class="Label1" width="30%">&nbsp;</td>
<td></td>
</tr> 
<tr>
<td class="Label2" width="30%">Name & Designation</td>
<td></td>
<td class="Label2" width="30%">Name & Designation</td>
<td></td>
</tr>
<tr>
<td class="Label1" width="30%"></td>
<td></td>
<td class="Label1" width="30%"></td>
<td></td>
</tr>
</table>
</td>
</tr>


</table></td></tr>
</table>
</fieldset>
</div>
</asp:Content>

