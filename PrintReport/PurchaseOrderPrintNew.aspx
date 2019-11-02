<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderPrintNew.aspx.cs" Inherits="Reports_PurchaseOrderPrintNew" Title="Purchase Order Print" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
  <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<div>
    
<fieldset id="fieldset1" class="FieldSet">
<table width="100%" >
<tr>
    <td colspan="5" align="right" >
    <asp:ImageButton ID="ImgPrint" runat="server" 
    ImageUrl="~/Images/Icon/Print-Button.png" CssClass="Imagebutton"
    OnClientClick="javascript:CallPrint('divPrint')"
    ToolTip="Print" Height="35px" Width="35px"/>

    <asp:ImageButton ID="ImgPdf" runat="server" ImageUrl="~/Images/Icon/PDF.jpg" CssClass="Imagebutton"
    ToolTip="PDF" Height="35px" Width="35px" onclick="ImgPdf_Click"/>

   
    </td>
</tr>
<tr><td colspan="5"><table id="divPrint" cellspacing="5" width="95%">
<tr>
<td colspan="5">

<table width="100%" align="center">
            <tr>
            <td colspan="5" >
            <asp:Image ID="imgAntTime" runat="server" CssClass="image" width="150px" Height="50px" /></td>
            </tr>
            <tr>
            <td class="Label_Dynamic" colspan="3">
            <asp:Label ID="lblCompanyName" runat="server" Text="CompanyName" Font-Bold="True"></asp:Label><br />
            <asp:Label ID="lblCompanyAddress" runat="server" Text="CompanyAddresss" Font-Bold="True"></asp:Label><br />
            <b>PhoneNo:</b><asp:Label ID="lblPhnNo" runat="server" Text="phoneNo" Font-Bold="True"></asp:Label><br />
            <b>FaxNo:</b><asp:Label ID="lblFaxNo" runat="server" Text="FaxNo" Font-Bold="True"></asp:Label><br />
            </td>
            <td class="Label">
              PO No :
              <br />
              PO Date :
              <br />
              Site Name :
            </td>
            <td>
                <asp:Label ID="lblPono" runat="server"  Text="PONo" Width="100%"></asp:Label>
                <br />
                <asp:Label ID="lblPODate" runat="server"  Text="PODate"></asp:Label>
                <br />
                <asp:Label ID="lblPODate0" runat="server" Text="Head-Office" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="5"  align="center" class="SubTitle">
                    <h2>Purchase Details</h2></td>
            </tr>
           
        
          
            <tr><%-- class="BoldText">--%>
                <td  class="Label_Dynamic" align="right" >Vendor Name:<%-- class="BoldText">--%>
                </td>
                <td colspan="2">
                    <asp:Label ID="lblSuplier" runat="server" CssClass="BoldText" Font-Size="Small"  Text="Suplier"></asp:Label>
                    </td>
                <td >
                    </td>
                <td >
                    &nbsp;</td>
               
            </tr>
            <tr>
         <td class="Label_Dynamic" align="right">
             Address :</td>
         <td colspan="2">
                    <asp:Label ID="lblSupAddress" runat="server" CssClass="BoldText" 
                 Font-Size="Small"  Text=""></asp:Label>
                                                     </td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
         </tr>
            <tr>
                   <td colspan="5">
                       <table width="100%">
                           <tr>
                             <td colspan="2">
                             <div id="divProDetails">
                         <asp:GridView ID="PurOrderGrid" runat="server" 
                             CssClass="mGrid" Width="100%"   >
                         </asp:GridView>
                             </div>
                            </td>
                           </tr>
                           <tr>
                           <td class="Label4" align="right" >
                             Total :
                           </td>
                           <td>
                               <asp:Label ID="lblTotal" runat="server" CssClass="Label2" Text="Label"></asp:Label>
                                                                 </td>
                           </tr>
                           <tr>
                           <td class="Label" align="right">
                           VAT :
                           </td>
                           <td>0.00</td>
                           </tr>
                           <tr>
                           <td class="Label" align="right">
                           Service Tax :
                           </td>
                           <td>0.00</td>

                           </tr>
                           <tr>
                           <td class="Label" align="right">
                               Freight &amp; Insurance:
                           </td>
                           <td>0.00          </tr>
                           <tr>
                           <td class="Label" align="right">
                           other Changes :</td>
                           <td>0.00</td>
                           </tr>
                           <tr>
                           <td class="Label4" align="right">
                           Gross Total :
                           </td>
                           <td>
                               <asp:Label ID="lblGrossTotal" runat="server" CssClass="Label2" Text="Label"></asp:Label>
                                                                 </td>
                           </tr>
                          
                           <tr>
                           <td class="Label_Dynamic" align="left">
                               <h4>Terms And Conditions</h4>
                           </td>
                           </tr>
                           <tr>
                           <td class="Label1">
                               1.All material to be deliver with valid PO no and as per the Standerd 
                               specification of the material
                           </td>
                           </tr>
                           <tr>
                           <td class="Label1">
                               2.All the material to be deliver on time if not purcahser have the right to cancelled the order 
                           </td>
                           </tr>
                           <tr class="Label1">
                           <td>
                           3.payment as per the contract 	
                           </td>
                           </tr>
                           <tr class="Label1">
                           <td>
                           4.If we received any damage material will be return back to supplier
                           </td>
                           </tr>
                           <tr class="Label1">
                           <td>
                           5.Subject to Pune Jurisdiction Only 
                           </td>
                           </tr>
                                                 </table>
                   </td>        
               </tr>
        </table>

</td>
</tr>
 
<tr>
<td colspan="5">
<table width="100%">
<tr>
<td class="Label1" width="30%">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label1" width="30%">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label1" width="30%">&nbsp;</td>
</tr>
<tr>
<td class="Label1" width="30%">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label1" width="30%">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label1" width="30%">&nbsp;</td>
</tr>
<tr>
<td class="Label1" width="30%">Prepared By</td>
<td></td>
<td class="Label1" width="30%">Accepted By</td>
<td></td>
<td class="Label1" width="30%">Authorised By</td>
</tr>
<tr>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
</tr>
<tr>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
</tr>
<tr>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
</tr>
<tr>
<td class="Label2">Name &amp; Designation</td>
<td></td>
<td class="Label2">Name &amp; Designation</td>
<td></td>
<td class="Label2">Name &amp; Designation</td>
</tr>
<tr>
<td class="Label2">Store Incharge</td>
<td>&nbsp;</td>
<td class="Label2">Vendor: Seal &amp; Signature</td>
<td>&nbsp;</td>
<td class="Label2">Unit Head/Operation Manager</td>
</tr>
</table>
 
</td>
</tr>
 </table></td></tr>
 </table>
 </fieldset></div>
</asp:Content>

