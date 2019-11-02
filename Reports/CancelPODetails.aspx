<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="CancelPODetails.aspx.cs" Inherits="Reports_CancelPODetails" Title="Cancelled Purchase Order Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</ajax:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
  </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Cancelled Purchase Order Details  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="UPEntry" runat="server">
<ContentTemplate>
<fieldset id="F1" runat="server" class="FieldSet">
 <table width="100%" cellspacing="5">
       
        <tr>
            <td class="Label">
                <asp:CheckBox ID="ChkFrmDate" runat="server" AutoPostBack="true"
                    CssClass="CheckBox"  Text=" From :" TabIndex="1"
                    oncheckedchanged="ChkFrmDate_CheckedChanged" />
                </td>
            <td  colspan="4">
                 <table>
                 <tr>
                 <td>
                 <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Width="90px"> 
                        </asp:TextBox>
                  <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" 
                    Format="dd-MMM-yyyy" PopupButtonID="ImageButton212" TargetControlID="txtFromDate" />
                  <asp:ImageButton ID="ImageButton212" runat="server" CausesValidation="False" 
                    CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="1" />      
                    </td>
                    <td class="Label">
                        &nbsp;&nbsp;&nbsp;To :</td>                
                     <td>
                     <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" 
                     Width="90px" ></asp:TextBox>
                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                    Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" TargetControlID="txtToDate" />
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                    CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" 
                    TabIndex="2"  /> 
                    </td> 
                    </tr> 
                    </table>                    
            </td>
            <td class="Label" >
                Purchase Order:</td>
            <td colspan="2" >
 
                 <ajax:ComboBox ID="ddlNo" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
            </td>
         <td align="right">
               </td>    
        </tr>
        <tr>
            <td class="Label">
                 Supplier :</td>
             <td colspan="4">
           
                <ajax:ComboBox ID="ddlSupplier" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
             </td>
         
                 <td class="Label">
                 Category :</td>
             <td colspan="4">
           
              <ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" AutoPostBack="true"
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle" onselectedindexchanged="ddlCategory_SelectedIndexChanged"></ajax:ComboBox>
             </td>
           
        </tr>
        <tr>
          <td class="Label">
                SubCategory :</td>
            <td colspan="4">
            
                   <ajax:ComboBox ID="ddlsubcategory" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle"
AutoPostBack="true" onselectedindexchanged="ddlsubcategory_SelectedIndexChanged"></ajax:ComboBox>
            </td>
           <td class="Label">
                Item :</td>
            <td colspan="4">
            
                   <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
            </td>
          
          
            </tr>
            <tr>
         
                 <td class="Label">
                Unit :</td>
            <td colspan="2">
       
                 <ajax:ComboBox ID="ddlUnit" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
            </td>
           
                <td colspan="2">
                    &nbsp;</td>
                <td class="Label">
                    Site</td>
                <td>
            
                      <ajax:ComboBox ID="ddlSite" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
                </td>
                <td colspan="2">
                    &nbsp;</td>
               
           
        </tr>
        <tr>           
            <td align="left" colspan="5">
            <asp:RadioButtonList ID="RdoType" runat="server"
            CellPadding="25"  RepeatDirection="Horizontal" CssClass="Display_None">
            <asp:ListItem Selected="True" Text="All&nbsp;&nbsp;" 
            Value="All"></asp:ListItem>
            <asp:ListItem Text="Generated&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" 
                    Value="Generated"></asp:ListItem>
            <asp:ListItem Text="Approved&nbsp;&nbsp;&nbsp;" Value="Approved"></asp:ListItem>
            <asp:ListItem Text="Authorised" Value="Authorised"></asp:ListItem>
            </asp:RadioButtonList>
            </td>
            
               <td class="Label">
                Size :</td>
            <td colspan="2">
              
                 <ajax:ComboBox ID="ddlSize" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
            </td>
        </tr>
        <tr>
        <td align="right" colspan="9">
                   &nbsp;<asp:Button ID="BtnShow" runat="server" CssClass="button" 
                    TabIndex="4" Text="Show" ValidationGroup="Add" 
                    ToolTip="Show Details" onclick="BtnShow_Click" />   
               <asp:Button ID="BtnCancel" runat="server" CssClass="button" 
                    TabIndex="5" Text="Cancel"  
                    ToolTip="Clear The Details" onclick="BtnCancel_Click"  />
            </td> </td>
        </tr>
     
</table>
  <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblCount" runat="server" CssClass="SubTitle"></asp:Label>
            </td>
            <td align="right" valign="middle" >
               <asp:ImageButton ID="ImgBtnPrint" runat="server" TabIndex="6"
                  OnClientClick="javascript:CallPrint('divPrint')"
                    ImageUrl="~/Images/Icon/Print-Icon.png" 
                    ToolTip="Print Report"  />
                <asp:ImageButton ID="ImgBtnExport" runat="server" 
                    ImageUrl="~/Images/Icon/excel-icon.png" TabIndex="7"
                    ToolTip="Export To Excel" onclick="ImgBtnExport_Click"  />
            </td>
            </tr> 
            <tr>
       
                <td colspan="2" align="center">
                <div id="divPrint" class="ScrollableDiv_FixHeightWidth4" >
                    <asp:GridView ID="GrdReport" runat="server" 
                        AutoGenerateColumns="False" CaptionAlign="Top" CssClass="mGrid"                        
                        Width="100%" ShowFooter="True" AllowPaging="false" onrowdatabound="GrdReport_RowDataBound" 
                        onpageindexchanging="GrdReport_PageIndexChanging" PageSize="30" >
                        <Columns>
                        <asp:TemplateField HeaderText="Sr. No.">                        
                        <ItemTemplate>
                        <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                         Width="6%" />
                         </asp:TemplateField>
                         
                             <asp:TemplateField HeaderText="">                        
                        <ItemTemplate>
                          <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="PS"%>&SFlag=<%="Generated"%>&PDFFlag=<%="PDF"%>' target="_blank">
                                 <asp:Image ID="IMGCALLPDF" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png" ToolTip="PDF Purchase Order" TabIndex="29" />
                                </a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                         Width="6%" />
                         </asp:TemplateField>
                          <asp:BoundField DataField="PONO" HeaderText="PO.NO.">
                               <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField> 
                         
                      <asp:BoundField DataField="PODate" HeaderText="Date">
                               <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField> 
                            
                             <asp:BoundField DataField="Location" HeaderText="Location">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                                    VerticalAlign="Middle" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="Supplier" HeaderText="Supplier">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                                    VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ItemName" HeaderText="Item" >
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                                 <asp:BoundField DataField="ItemDesc" HeaderText="Description" >
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" />
                            </asp:BoundField>
                                 <asp:BoundField DataField="RemarkForPO" HeaderText="Remark" >
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" />
                            </asp:BoundField>
                                <asp:BoundField DataField="Unit" HeaderText="Unit" >
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>
                          
                          
                            <asp:BoundField DataField="Qty" HeaderText="Qty">
                               <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Rate" HeaderText="Rate">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                        
                              <asp:BoundField DataField="Amount" HeaderText="Amount">
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                                <asp:BoundField DataField="TaxPer" HeaderText="Tax(%)">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                                    VerticalAlign="Middle" />
                                </asp:BoundField>
                             <asp:BoundField DataField="TaxAmount" HeaderText="TaxAmount">
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                            
                              <asp:BoundField DataField="DiscPer" HeaderText="Disc(%)">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                                    VerticalAlign="Middle" />
                                </asp:BoundField>
                             <asp:BoundField DataField="DiscAmount" HeaderText="Disc Amount">
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="NetAmount" HeaderText="NetAmount">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>
                          
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <AlternatingRowStyle CssClass="alt" />
                        <FooterStyle CssClass="ftr" />
                    </asp:GridView>
                    </div>
                </td>
        </tr>
            </table>
</fieldset>
</ContentTemplate>
            <Triggers>
             <asp:PostBackTrigger ControlID ="ImgBtnExport" />
             </Triggers>
</asp:UpdatePanel>
</asp:Content>

