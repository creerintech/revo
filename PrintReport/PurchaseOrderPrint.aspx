<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderPrint.aspx.cs" Inherits="Reports_PurchaseOrderPrint" Title="Purchase Order Report" %>
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
    ToolTip="Print" Height="35px" Width="35px"/>

    <asp:ImageButton ID="ImgPdf" runat="server" ImageUrl="~/Images/Icon/PDF.jpg" CssClass="Imagebutton"
    ToolTip="PDF" Height="35px" Width="35px" onclick="ImgPdf_Click"/>

    <asp:ImageButton ID="ImgBtnExport" runat="server" 
    ImageUrl="~/Images/Icon/excel.png" TabIndex="7"
    ToolTip="Export To Excel"  CssClass="Imagebutton" onclick="ImgBtnExport_Click"/>
    </td>
    </tr>
 <tr>
<td colspan="5">
 <div id="divPrint"> 
  <table width="100%" align="center">
            <tr>
             <td colspan="5" >
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
                <td colspan="5"  align="center" class="SubTitle">
                    <h2>Purchase Details</h2></td>
            </tr>
        
            <tr>
                <td colspan="5"  align="center" class="SubTitle">
                    &nbsp;</td>
            </tr>
        
            <tr>
                <td class="Label_Dynamic" align="right">
                    To,</td>
                <td width="100" >
                     &nbsp;</td>
                <td width="500" >
                     &nbsp;</td>
                    <td class="Label_Dynamic" align="right">PO No :</td>
                    <td>
                     <asp:Label ID="lblPono" runat="server" CssClass="BoldText" Text="PONo"></asp:Label></td>
               
            </tr>
                
            <tr><%-- class="BoldText">--%>
                <td  class="Label_Dynamic" align="right" >Vendor Name:<%-- class="BoldText">--%>
                </td>
                <td colspan="2">
                    <asp:Label ID="lblSuplier" runat="server" CssClass="BoldText" Font-Size="Small"  Text="Suplier"></asp:Label>
                    </td>
                <td class="Label_Dynamic" align="right">
                    PO Date :</td>
                <td width="100">
                <asp:Label ID="lblPODate" runat="server" CssClass="BoldText" Text="PODate"></asp:Label></td>
               
            </tr>
            
         <tr>
         <td class="Label_Dynamic" align="right">
             Address :</td>
         <td colspan="2">
                    <asp:Label ID="lblSupAddress" runat="server" CssClass="BoldText" 
                 Font-Size="Small"  Text="SuplierAddress"></asp:Label>
                                                     </td>
                    <td class="Label_Dynamic" align="right">Site Name :</td>
                    <td>
                <asp:Label ID="lblSiteName" runat="server" CssClass="BoldText" Text="EON KHARADI"></asp:Label></td>
                
         </tr>
         
            <tr>
         <td class="Label_Dynamic" align="left" colspan="5">
           <h4> Purchase Order Details :</h4> </td>
                
         </tr>
            <tr>
                   <td colspan="5">
                  
                       <table width="100%">
                           <tr>
                               <td>
                             <div id="divProDetails">
                         <asp:GridView ID="PurOrderGrid" runat="server" 
                             CssClass="mGrid"   >
                         </asp:GridView>
                             </div>
                            </td>
                           </tr>
                           <tr>
                           <td class="Label_Dynamic" align="left">
                           <%--<h3>Terms and Conditions :</h3>--%>
                           </td>
                           
                           </tr>
                           <tr>
                           <td class="Label_Dynamic" align="left">
                               <h4>Terms And Conditions:</h4></td>
                           
                           </tr>
                           <tr>
                           <td>
                           <div id="divHWDetails">
                         <asp:GridView ID="TermsGrid" runat="server" 
                             CssClass="mGrid"  >
                             <FooterStyle Wrap="False" />
                             <SelectedRowStyle Wrap="False" />
                             <HeaderStyle Wrap="False" />
                         </asp:GridView>
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
 <tr class="Display_None">
 <td>
 <b>TINNo :</b> <asp:Label ID="LblTinNo" runat="server" Text="TinNo" Font-Bold="True" CssClass="Label_Dynamic"></asp:Label><br />
 </td>
 </tr>
 <tr class="Display_None">
 <td>
  <b>VATNo : </b><asp:Label ID="lblVatNo" runat="server" Text="VatNo" Font-Bold="True"  CssClass="Label_Dynamic"></asp:Label><br />
 </td>
 </tr>
 <tr class="Display_None">
 <td>
  <b>ServiceTaxNo : </b><asp:Label ID="lblServiceTaxNo" runat="server" Text="ServiceTaxNo" Font-Bold="True"  CssClass="Label_Dynamic"></asp:Label><br />
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
 
 </table>
 </fieldset></div>
</asp:Content>

