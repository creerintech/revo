<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="InwardPrint.aspx.cs" Inherits="Reports_PurchaseOrderPrint" Title="Inward Register Report" %>
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
    <td colspan="5" align="right" height="36px">
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
        <asp:Image ID="imgAntTime" runat="server" CssClass="image" width="150px" Height="50px"/></td>
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
                    <h2>Inward Register</h2></td>
            </tr>
                    <tr>
                <td class="Label_Dynamic" align="right" width="100px">
                    Inward No :</td>
                <td colspan="2">
                     <asp:Label ID="lbInwardNo" runat="server" CssClass="BoldText" 
                         Text="InwardNo"></asp:Label></td>
              
                <td class="Label_Dynamic" align="right" width="133px">
                    Inward Date :</td>
                <td  >
                <asp:Label ID="lblInwardDate" runat="server" CssClass="BoldText" 
                        Text="InwardDate"></asp:Label></td>
               
            </tr>
                
            <tr>
                <td  class="Label_Dynamic" align="right" width="100px" >Bill No :</td>
                <td colspan="2">
                     <asp:Label ID="lbBillNo" runat="server" CssClass="BoldText" 
                         Text="BillNo"></asp:Label></td>
              
                 <td align="right" class="Label_Dynamic">Inward Through :</td>
                    <td>
                     <asp:Label ID="lbInwardThrough" runat="server" CssClass="BoldText" 
                         Text="InwardThrough"></asp:Label></td>
            </tr>
            
            <tr>
                <td  class="Label_Dynamic" align="right" width="100px" >To,</td>
                <td colspan="2">
                    &nbsp;</td>
                     <td class="Label_Dynamic" align="right">Bill Date :</td>
                    <td>
                    <asp:Label ID="lblbilldate" runat="server" CssClass="BoldText" 
                         Text="-"></asp:Label>
                    </td>
                
            </tr>
            
            <tr>
            <td  class="Label_Dynamic" align="right" height="20px" width="221px">
                &nbsp;</td>
            <td  colspan="2">
            <asp:Label ID="lblSupplier" runat="server" CssClass="BoldText" 
            Text="Supplier"></asp:Label>
            </td>
                    <td class="Label_Dynamic" align="right">Purchase Order No :</td>
                    <td>
                    <asp:Label ID="lblPONO" runat="server" CssClass="BoldText" 
                         Text="PUrchase No"></asp:Label>
                    </td>
            </tr>

            <tr>
            <td  class="Label_Dynamic" align="right" style="height: 20px; width: 100px">
                Billing Address :</td>
            <td colspan="4" style="height: 20px" >
            <asp:Label ID="lblBillingAddress" runat="server" CssClass="BoldText" 
            Text="BillingAddress"></asp:Label>
            </td>

            </tr>
            <tr>
            <td  class="Label_Dynamic" align="right" style="height: 20px; width: 100px">
                Shipping Address :</td>
            <td colspan="4" style="height: 20px" >
            <asp:Label ID="lblShippingAddress" runat="server" CssClass="BoldText" Text="ShippingAddress"></asp:Label>
            </td>

            </tr>
             <tr>
                   <td colspan="10">
                       <table width="100%">
                           <tr>
                            <td colspan="6" align="center">
                            <div id="divProDetails" class="mgrid">
                            <asp:GridView ID="InwardGrid" runat="server" 
                              CssClass="mGrid">
                            </asp:GridView>
                            </div>
                            </td>
                           </tr>
                           <tr><td colspan="6"></td></tr>
                           <tr><td colspan="6"></td></tr>
                           <tr><td colspan="6"></td></tr>
                           <tr>
                           <td colspan="6" align="left" class="Label_Dynamic">
                               Instruction:
                           <asp:Label ID="lblInstruction" runat="server" CssClass="Label" Text="-"></asp:Label>
                           </td>
                           </tr>
                           <tr>
                         
                            <td class="Label_Dynamic" align="right" width="16.66%" >
                                Subtotal:</td>
                            <td align="left" width="16.66%">
                            <asp:Label ID="lblSubTotal" runat="server" CssClass="BoldText" Text="SubTotal"></asp:Label>
                            </td>
                              <td class="Label_Dynamic" align="right" width="16.66%" >
                                Discount:</td>
                            <td align="left" width="16.66%">
                            <asp:Label ID="lblDiscount" runat="server" CssClass="BoldText" Text="lblDiscount"></asp:Label>
                            </td>
                              <td class="Label_Dynamic" align="right" width="16.66%" >
                                VAT:</td>
                            <td height="20px"  colspan="1" align="left" width="16.66%">
                            <asp:Label ID="lblVat" runat="server" CssClass="BoldText" Text="lblVAT"></asp:Label>
                            </td>
                           </tr>
                          
                           <tr>
                           
                            <td class="Label_Dynamic" align="right" >
                                Dekhrekh :</td>
                            <td   align="left">
                            <asp:Label ID="lblDekhrekh" runat="server" CssClass="BoldText" Text="Dekhrekh"></asp:Label>
                            </td>
                            <td class="Label_Dynamic" align="right" >
                                Hamali :</td>
                            <td align="left">
                            <asp:Label ID="lblHamali" runat="server" CssClass="BoldText" Text="Hamali"></asp:Label>
                            </td>
                             <td class="Label_Dynamic" align="right" >
                                Cess :</td>
                            <td align="left">
                            <asp:Label ID="lblCESS" runat="server" CssClass="BoldText" Text="Cess"></asp:Label>
                            </td>
                           </tr>
                         
                           <tr>
                          
                            <td class="Label_Dynamic" align="right" >
                                Freight :</td>
                            <td align="left">
                            <asp:Label ID="lblFreight" runat="server" CssClass="BoldText" Text="Freight"></asp:Label>
                            </td>
                                <td class="Label_Dynamic" align="right" >
                                Packing :</td>
                            <td align="left">
                            <asp:Label ID="lblPacking" runat="server" CssClass="BoldText" Text="Packing"></asp:Label>
                            </td>
                               <td class="Label_Dynamic" align="right" >
                                Postage :</td>
                            <td align="left">
                            <asp:Label ID="lblPostage" runat="server" CssClass="BoldText" Text="Postage"></asp:Label>
                            </td>
                           </tr>
                          
                           <tr>
                           
                         
                                 <td class="Label_Dynamic" align="right" colspan="1" style="width: 553px">
                                OtherCharges :</td>
                            <td  align="left">
                            <asp:Label ID="lblOtherCharges" runat="server" CssClass="BoldText" 
                              Text="OtherCharges"></asp:Label>
                            </td>
                             <td class="Label_Dynamic" align="right" >
                                GrandTotal:</td>
                            <td  align="left">
                            <asp:Label ID="lblGrandTotal" runat="server" CssClass="BoldText" 
                                    Text="lblGrandTotal"></asp:Label>
                            </td>
                           </tr>
                           
                          <tr>
                            <td class="Label_Dynamic">
                             </td>
                       
                           </tr>
                             <tr>
                            <td class="Label_Dynamic">
                             </td>
                       
                           </tr>
                           
                             <tr>
                            <td class="Label_Dynamic">
                             <b>TinNo:</b><asp:Label ID="LblTinNo" runat="server" Text="TinNo" Font-Bold="True"></asp:Label></td>
                       
                           </tr>
                           
                           <tr>
                            <td class="Label_Dynamic">
                             <b>VatNo:</b><asp:Label ID="lblVatNo" runat="server" Text="VatNo" Font-Bold="True"></asp:Label></td>
                       
                           </tr>
                           <tr>
                            <td class="Label_Dynamic">
                             <b>ServiceTaxNo:</b><asp:Label ID="lblServiceTaxNo" runat="server" Text="ServiceTaxNo" Font-Bold="True"></asp:Label></td>
                           
                           </tr>
                           <tr>
                            <td class="Label_Dynamic">
                                &nbsp;</td>
                            <td class="Label_Dynamic" align="right" >
                                &nbsp;</td>
                            <td height="10px" align="right">
                                &nbsp;</td>
                           </tr>
                          
                       </table>
                   
                   </td>        
               </tr>   
               
            <tr>
            <td colspan="7">
            <table width="100%">
            <tr>
<td class="Label1" width="15%">Prepared By</td>
<td></td>
<td class="Display_None" width="15%"></td>
<td></td>
<td class="Display_None" width="15%"></td>
</tr>
            <tr>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
</tr>
            <tr>
<td class="Label2"><asp:Label ID="LblPrepareBy" runat="server" Text="" Font-Bold="True"></asp:Label></td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
<td>&nbsp;</td>
<td class="Label">&nbsp;</td>
</tr> 
<tr>
<td class="Label2" width="30%">Name &amp; Designation</td>
<td></td>
<td class="Display_None" width="30%"></td>
<td></td>
<td class="Display_None" width="30%"></td>
</tr>
<tr>
<td class="Label2">Store Incharge</td>
<td></td>
<td class="Display_None"></td>
<td></td>
<td class="Display_None"></td>
</tr>
 </table>
</td>
</tr>
</table>
</div>
  </td>
  </tr>
      </table>
 </fieldset></div>
</asp:Content>

