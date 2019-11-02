<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="POStatus.aspx.cs" Inherits="Reports_POStatus" Title="Purchase Order Status" %>
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
    Purchase Order Status
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UPEntry" runat="server">
<ContentTemplate>
<fieldset id="F1" runat="server" class="FieldSet">
 <table width="100%" cellspacing="5">
        <tr>
                     
            <td align="center" colspan="6">
            
<asp:RadioButtonList ID="rdoPOSTATUSWISE" runat="server" AutoPostBack="true"
RepeatDirection="Horizontal" CssClass="RadioButton" TabIndex="1"
onselectedindexchanged="rdoPOSTATUSWISE_SelectedIndexChanged">
<asp:ListItem Selected="True" Value="0" >&nbsp;All Purchase Order&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
<asp:ListItem Value="1" >&nbsp;Outstanding Purchase Order</asp:ListItem>
</asp:RadioButtonList>

            </td> 
        </tr>
        <tr>
            <td class="Label">
                <asp:CheckBox ID="ChkFrmDate" runat="server" AutoPostBack="true"
                    CssClass="CheckBox"  Text=" From :" TabIndex="1"
                    oncheckedchanged="ChkFrmDate_CheckedChanged" />
                </td>
            <td  colspan="3">
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
                        <td class="Label">
               Location :</td>
            <td>
                                
                    <ajax:ComboBox ID="ddlSite" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
            </td>
        </tr>
        <tr>
           

            <td class="Label" >
                Purchase Order NO:</td>
            <td colspan="3" >
              
                  <ajax:ComboBox ID="ddlPurchaseNo" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
            </td>
         <td class="Label">
                 Inward No :</td>
             <td >
              
                    <ajax:ComboBox ID="ddlInwardNo" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
             </td>
            </tr>
        <tr>
        <td align="right" colspan="6">
                   &nbsp;<asp:Button ID="BtnShow" runat="server" CssClass="button" 
                    TabIndex="4" Text="Show" ValidationGroup="Add" 
                    ToolTip="Show Details" onclick="BtnShow_Click" />   
               <asp:Button ID="BtnCancel" runat="server" CssClass="button" 
                    TabIndex="5" Text="Cancel"  
                    ToolTip="Clear The Details" onclick="BtnCancel_Click"  />
            </td> 
        </tr>
     
</table>
  <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblCount" runat="server" CssClass="SubTitle"></asp:Label>
            </td>
            <td align="right" valign="middle">
               <asp:ImageButton ID="ImgBtnPrint" runat="server" TabIndex="6"
                  OnClientClick="javascript:CallPrint('divPrint')"
                    ImageUrl="~/Images/Icon/Print-Icon.png" 
                    ToolTip="Print Report"  />
                <asp:ImageButton ID="ImgBtnExport" runat="server" 
                    ImageUrl="~/Images/Icon/excel-icon.png" TabIndex="7"
                    ToolTip="Export To Excel" onclick="ImgBtnExport_Click"  />
            </td>
            </tr> 
            <tr runat="server" id="TRGRDALLPO">
         
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
                          <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="PS"%>&SFlag=<%="Authorised"%>&PDFFlag=<%="PDF"%>' target="_blank">
                                 <asp:Image ID="IMGCALLPDF" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png" ToolTip="PDF Purchase Order" TabIndex="29" />
                                </a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                         Width="6%" />
                         </asp:TemplateField>
                           <asp:BoundField DataField="SuplierName" HeaderText="Supplier Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PODate" HeaderText="PODate">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PONo" HeaderText="PONO">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="ItemName" HeaderText="ItemName" >
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="ItemDesc" HeaderText="ItemDesc" >
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        
                           <asp:BoundField DataField="RemarkForPo" HeaderText="Remark" >
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Qty" HeaderText="Qty">
                        <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                        Font-Bold="True" Wrap="True" />
                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="">                        
                        <ItemTemplate>
                            <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("InwardId")%>&Flag=<%="IN"%>&PDFFlag=<%="PDF"%>' target="_blank">
                            <asp:Image ID="IMGCALLPDF" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png"
                            ToolTip="PDF Inward Details" TabIndex="29" />
                            </a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                         Width="6%" />
                         </asp:TemplateField>
                         
                         
  
                        <asp:BoundField DataField="InwardNo" HeaderText="Inward No">
                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>

                        <asp:BoundField DataField="InwardQty" HeaderText="Inward Qty">
                        <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                        Font-Bold="True" Wrap="True" />
                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="PendingQty" HeaderText="Pending Qty">
                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Location" HeaderText="Location">
                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>

                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <AlternatingRowStyle CssClass="alt" />
                        <FooterStyle CssClass="ftr" />
                    </asp:GridView>
                    </div>
                </td>
                
        </tr>
            <tr runat="server" id="TRGRDOUTSTANDING">
   
                <td colspan="2" align="center">
                <div id="div1" runat="server" class="ScrollableDiv_FlexiableHeight" >
                    <asp:GridView ID="GRDOUTSTANDINGPO" runat="server" 
                        AutoGenerateColumns="False" CaptionAlign="Top" CssClass="mGrid"                        
                        Width="100%" ShowFooter="True" AllowPaging="false"
                        onrowcommand="GRDOUTSTANDINGPO_RowCommand">
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
                         
                            <asp:ImageButton ID="ImageGridEdit" runat="server" 
                            CommandArgument='<%#Eval("#") %>' 
                            CommandName="Select" ImageUrl="~/Images/New Icon/ViewRecord.png" ToolTip="Show Outstanding Items"/>
                                
         
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                         Width="6%" />
                         </asp:TemplateField>
                         <asp:BoundField DataField="SuplierName" HeaderText="Supplier Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>

                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <AlternatingRowStyle CssClass="alt" />
                        <FooterStyle CssClass="ftr" />
                    </asp:GridView>
                    </div>
                 <hr color="red" />
                  <fieldset class="FieldSet" id="FGRDSUPLIEROSREPORT">
                 <legend id="Legend3" class="legend" runat="server">Outstanding Quantity</legend>
                 <div id="div2" runat="server" class="ScrollableDiv_FlexiableHeight" >
                    <asp:GridView ID="GRDSUPLIEROSREPORT" runat="server" 
                        AutoGenerateColumns="False" CaptionAlign="Top" CssClass="mGrid"                        
                        Width="100%" ShowFooter="True" AllowPaging="false">
                        <Columns>
                        <asp:TemplateField HeaderText="Sr. No.">                        
                        <ItemTemplate>
                        <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                         Width="6%" />
                         </asp:TemplateField>
                         <asp:BoundField DataField="SuplierName" HeaderText="Supplier Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                             <asp:TemplateField HeaderText="">                        
                        <ItemTemplate>
                          <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("POId")%>&Flag=<%="PS"%>&SFlag=<%="Authorised"%>&PDFFlag=<%="PDF"%>' target="_blank">
                          <asp:Image ID="IMGCALLPDF" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png" ToolTip="PDF Purchase Order" TabIndex="29" />
                          </a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                         Width="6%" />
                         </asp:TemplateField>
                        <asp:BoundField DataField="pono" HeaderText="Purcahse Order No.">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                          <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                          <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                               <asp:BoundField DataField="RemarkForPo" HeaderText="Remark">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                          <asp:BoundField DataField="Unit" HeaderText="Unit">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                        
                         <asp:BoundField DataField="sumpoqty" HeaderText="PO Qty">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                        
                           <asp:BoundField DataField="suminqty" HeaderText="Inward Qty">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                        
                          <asp:BoundField DataField="BalanceQty" HeaderText="Balance Qty">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                        
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <AlternatingRowStyle CssClass="alt" />
                        <FooterStyle CssClass="ftr" />
                    </asp:GridView>
                    </div>
                        </fieldset> 
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

