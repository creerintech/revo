<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="PaymentToSupplier.aspx.cs" Inherits="Transactions_EditPurchaseOrder" Title="Payment To Supplier Register" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
        <ProgressTemplate>            
        <div id="progressBackgroundFilter"></div>
        <div id="processMessage">   
        <center><span class="SubTitle">Loading....!!! </span></center>
        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
        </div>
        </ProgressTemplate>
        </asp:UpdateProgress>
        
            Search for Purchase Order / Supplier :
        <asp:TextBox ID="TxtSearch" runat="server" 
        CssClass="search" ToolTip="Enter The Text"    Width="292px" 
        AutoPostBack="True" ontextchanged="TxtSearch_TextChanged" ></asp:TextBox> 
        <div id="divwidth"></div>
        <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
        TargetControlID="TxtSearch"    CompletionInterval="100"  UseContextKey="True" FirstRowSelected ="true"                              
        ShowOnlyCurrentWordInCompletionListItem="true"  ServiceMethod="GetCompletionList"
        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" >                         
        </ajax:AutoCompleteExtender> 
        </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
  Payment To Supplier    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
      <ContentTemplate>
      <table width="100%">
      
         <tr >
           <td align="center">
         
              <fieldset id="Fieldset2"  class="FieldSet" runat="server">
              <legend id="Legend3" class="legend" runat="server">Payment Details </legend>
                 
                       <div id="div1" class="ScrollableDiv_FixHeightWidthAPP">
                     <table width="100%">
                         <tr>
                            <td>        
                            <asp:GridView ID="GrdReqPO" runat="server" AutoGenerateColumns="False" Width="100%"
                                 CssClass="mGrid" BackColor="White" BorderColor="#0CCCCC"
                                 BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                                 AllowPaging="false" DataKeyNames="#"  >
                            <columns>
                            <asp:TemplateField HeaderText="#" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="All" >
                            <HeaderTemplate>
                            <asp:CheckBox ID="GrdSelectAllHeader" runat="server"                                                  
                            AutoPostBack="true" OnCheckedChanged="GrdSelectAllHeader_CheckedChanged"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:CheckBox ID="GrdSelectAll" runat="server"/>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                           
                             <asp:BoundField DataField="POId" HeaderText="POId">
                                            <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" 
                                              VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle CssClass="Display_None" HorizontalAlign="Left" 
                                              VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                            <asp:BoundField DataField="SuplierName" HeaderText="SuplierName">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>                                 
                            <asp:BoundField DataField="PONo" HeaderText="PO No">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                            <asp:BoundField DataField="PODate" HeaderText="PO Date">
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                            <asp:TemplateField HeaderText="P.O. Amount">
                                <ItemTemplate>
                                <asp:TextBox ID="GrdtxtPOAmount" runat="server" CssClass="TextBoxNumeric" 
                                MaxLength="10" Text='<%# Bind("POAmount") %>' TextMode="SingleLine" Width="100px"></asp:TextBox>
                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPoamt" runat="server" FilterType="Numbers, Custom"
                                ValidChars="." TargetControlID="GrdtxtPOAmount" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="100px"/>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="100px"/>
                                </asp:TemplateField>  
                          
                                 
                                <asp:TemplateField HeaderText="Payment Amount">
                                <ItemTemplate>
                                <asp:TextBox ID="GrdtxtPaymentAmount" runat="server" CssClass="TextBoxNumeric" 
                                MaxLength="10" Text='<%# Bind("PaymentAmount") %>' TextMode="SingleLine" Width="100px"></asp:TextBox>
                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderpayamt" runat="server" FilterType="Numbers, Custom"
                                ValidChars="." TargetControlID="GrdtxtPaymentAmount" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="100px"/>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="100px"/>
                                </asp:TemplateField>  
                                
                                <asp:TemplateField HeaderText="Remaining Amount">
                                <ItemTemplate>
                                <asp:TextBox ID="GrdtxtRemaingAmount" runat="server" CssClass="TextBoxNumeric" 
                                MaxLength="10" Text='<%# Bind("RemAmount") %>' TextMode="SingleLine" Width="100px"></asp:TextBox>
                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderrepa" runat="server" FilterType="Numbers, Custom"
                                ValidChars="." TargetControlID="GrdtxtRemaingAmount" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="100px"/>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="100px"/>
                                </asp:TemplateField>  

                                <asp:TemplateField HeaderText="Cheque No.">
                                <ItemTemplate>
                                <asp:TextBox ID="GrdtxtChequeNo" runat="server" CssClass="TextBox"
                                MaxLength="10" Text='<%# Bind("ChequeNo") %>' TextMode="SingleLine" Width="130px"></asp:TextBox>
                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtendercheno" runat="server" FilterType="Numbers, Custom"
                                ValidChars="." TargetControlID="GrdtxtChequeNo" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="130px"/>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="130px"/>
                                </asp:TemplateField>  

                                <asp:TemplateField HeaderText="Hand Over Person Name">
                                <ItemTemplate>
                                <asp:TextBox ID="GrdtxtPersonName" runat="server" CssClass="TextBoxNumeric" 
                                MaxLength="10" Text='<%# Bind("PersonName") %>' TextMode="MultiLine" Width="130px"></asp:TextBox>
                               
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="130px"/>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="130px"/>
                                </asp:TemplateField>  
                                
                                <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                <asp:TextBox ID="GrdtxtRemark" runat="server" CssClass="TextBox"
                                 Text='<%# Bind("Remark") %>' TextMode="MultiLine" Width="120px"></asp:TextBox>
                               
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="60px"/>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="60px"/>
                                </asp:TemplateField>  

                                         
                           </columns>
                            </asp:GridView>
                            </td>
                         </tr>  
                         <tr>
            <td align="center" colspan="2" >
                <table align="center" width="25%">
                    <tr>
                        <td>
                        <asp:Button ID="BtnSave" CssClass="buttonpayment" runat="server" Text="Payment Done" ValidationGroup="Add" onclick="BtnSave_Click" />
                        </td>
                          <td>
                        <asp:Button ID="BtnReset" CssClass="buttonpayment" runat="server" Text="Reset" ValidationGroup="Add" onclick="BtnReset_Click" />
                        </td>
                    </tr>

                </table>
            </td>
         </tr>
         
                     </table>
                     </div>
              </fieldset>
           
            </td>
         </tr>
       </table>
      </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

