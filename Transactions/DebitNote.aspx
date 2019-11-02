<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="DebitNote.aspx.cs" Inherits="Reports_DebitNote" Title="Debit Note" %>
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
            <td colspan="4" >
            <asp:Image ID="imgAntTime" runat="server" CssClass="image" width="150px" Height="50px" /></td>
            </tr>
            <tr>
            <td class="Label_Dynamic" colspan="2">
            <asp:Label ID="lblCompanyName" runat="server" Text="CompanyName" Font-Bold="True"></asp:Label><br />
            <asp:Label ID="lblCompanyAddress" runat="server" Text="CompanyAddresss" Font-Bold="True"></asp:Label><br />
            <b>PhoneNo:</b><asp:Label ID="lblPhnNo" runat="server" Text="phoneNo" Font-Bold="True"></asp:Label><br />
            <b>FaxNo:</b><asp:Label ID="lblFaxNo" runat="server" Text="FaxNo" Font-Bold="True"></asp:Label><br />
            </td>
            <td class="Label">
                Debit Note No :
              <br />
                Date :
              <br />
                Site Name :
            </td>
            <td>
                <asp:Label ID="lblDebitNoteNo" runat="server" CssClass="BoldText" 
                    Text="DebitNoteNo"></asp:Label>
                <br />
                <asp:Label ID="lblDate" runat="server" CssClass="BoldText" Text="Date"></asp:Label>
                <br />
                <asp:Label ID="lblPODate0" runat="server" CssClass="BoldText" Text="EON KHARADI"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4"  align="center" class="SubTitle">
                    <h2>Debit Note</h2></td>
            </tr>
           
        
          
            <tr>
                <td  class="Label_Dynamic" align="right" >To,</td>
                <td>
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
               
            </tr>
           
        
          
            <tr><%-- class="BoldText">--%>
                <td  class="Label_Dynamic" align="right" >Vendor Name:<%-- class="BoldText">--%>
                </td>
                <td>
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
         <td>
                    <asp:Label ID="lblSupAddress" runat="server" CssClass="BoldText" 
                 Font-Size="Small"  Text=""></asp:Label>
                                                     </td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
         </tr>
            <tr>
         <td class="Label_Dynamic" align="right">
             &nbsp;</td>
         <td>
                    &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
         </tr>
            <tr>
         <td class="Label_Dynamic" align="left" colspan="4">
             Reason For Debit Note</td>
         </tr>
            <tr>
         <td class="Label_Dynamic" align="right" colspan="4">
             &nbsp;</td>
         </tr>
            <tr>
         <td class="Label_Dynamic" align="right">
             Date :</td>
        <td>
            <asp:Label ID="lblDebitNoteDate" runat="server" CssClass="Label_Dynamic" 
                Text="lblDebitNote"></asp:Label>
        </td>
        <td class="Label_Dynamic" align="right">Time :</td>
        <td>
            <asp:Label ID="lblDebitTime" runat="server" CssClass="Label_Dynamic" 
                Text="lblDebitTime"></asp:Label>
                                                             </td>
         </tr>
            <tr>
         <td class="Label_Dynamic" align="right">
             &nbsp;</td>
         <td>
                    &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
         </tr>
            <tr>
                   <td colspan="4">
                       <table width="100%">
                           <tr>
                             <td>
                             <div id="divProDetails">
                         <asp:GridView ID="DebitNote" runat="server" 
                             CssClass="mGrid" Width="100%"   >
                         </asp:GridView>
                             </div>
                            </td>
                           </tr>
                          
                           <tr>
                           <td class="Label_Dynamic" align="left">
                               &nbsp;</td>
                           </tr>
                           <tr>
                           <td class="Label1">
                               Note For Any Discrepency in Material :</td>
                           </tr>
                           <tr>
                           <td class="Label1">
                               &nbsp;</td>
                           </tr>
                           <tr class="Label1">
                           <td>
                               &nbsp;</td>
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

