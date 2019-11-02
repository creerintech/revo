<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="GroundStockReport.aspx.cs" Inherits="Reports_GroundStockReport" Title="Stock Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
    <script type="text/javascript" language="javascript"> 
        
        </script>
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</ajax:ToolkitScriptManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Stock Report    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UPEntry" runat="server">
<ContentTemplate>
  <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
        <%--<ProgressTemplate>            
        <div id="progressBackgroundFilter"></div>
        <div id="processMessage">   
        <center><span class="SubTitle">Loading....!!! </span></center>
        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
        </div>
        </ProgressTemplate>--%>
  </asp:UpdateProgress>
<fieldset id="F1" runat="server" class="FieldSet" width="98%">
 <table width="100%" cellspacing="5">
        <tr>
            <td>
                </td>
            <td align="left" >
                </td>
            <td class="Label" >
                </td>
            <td align="left">
               </td>            
            <td align="right">
                </td>            
            <td align="right">
               </td>            
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
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
                 <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Width="90px" 
                         ReadOnly="false"></asp:TextBox>
                  <asp:ImageButton ID="ImageButton212" runat="server" CausesValidation="False" 
                    CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="1" />
                    
                  <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" 
                    Format="dd-MMM-yyyy" PopupButtonID="ImageButton212" TargetControlID="txtFromDate" />
                        
                    </td>
                    <td class="Label">
                        &nbsp;&nbsp;&nbsp;To :</td>                
                     <td>
                     <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" 
                     Width="90px" ReadOnly="false" ></asp:TextBox>
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
                <%--   <asp:DropDownList ID="ddlLocation" runat="server" CssClass="ComboBox" TabIndex="3"
                    Width="152px">
                </asp:DropDownList>--%>
                   <ajax:ComboBox ID="ddlLocation" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
                 </td>
            <td class="Label" >
                Category :</td>
            <td >
                
           
                  <ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle" AutoPostBack="true" onselectedindexchanged="ddlCategory_SelectedIndexChanged"></ajax:ComboBox>
            </td>
           
        </tr>
        <tr>
          
          <td class="Label">
                 Sub Category :</td>
            <td colspan="3">
           
                 <ajax:ComboBox ID="ddlSubCategory" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle" AutoPostBack="true" onselectedindexchanged="ddlSubCategory_SelectedIndexChanged"></ajax:ComboBox>
                
            </td>
          
             <td class="Label">
                 Item :</td>
            <td >
             <%--   <asp:DropDownList ID="ddlItemName" runat="server" CssClass="ComboBox" 
                    TabIndex="2" Width="250px">
                </asp:DropDownList>--%>
                 <ajax:ComboBox ID="ddlItemName" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
                
            </td>
            
              <td class="Label">
                Unit :</td>
            <td>
                <%--<asp:DropDownList ID="ddlUnit" runat="server" CssClass="ComboBox" TabIndex="3" 
                    Width="152px">
                </asp:DropDownList>--%>
                <ajax:ComboBox ID="ddlUnit" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle"></ajax:ComboBox>
            </td>
            
            
        </tr>
        <tr>
            <td class="Label">
                <asp:CheckBox ID="CHKNORATE" runat="server" AutoPostBack="true"
                    CssClass="Display_None"  Text="" TabIndex="1"  /></td>
            <td colspan="3">
                <asp:Label Text="SHOW RECORD WITH OUT MRP" runat="server" ID="LBLNORATE" ForeColor="Black" Font-Bold="true" CssClass="Display_None"> </asp:Label></td>
            <td class="Label">
               </td>
            <td>
                &nbsp;</td>
           <td class="Label">
            </td>
            <td>
               
            </td> 
        </tr>
        <tr>           
            <td class="Label" colspan="7"></td>
            <td align="left" colspan="1">
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
          <%--   ShowFooter="True" --%>
                <td colspan="2" align="center">
                <div id="divPrint" class="ScrollableDiv_FixHeightWidth4">
                    <asp:GridView ID="GrdReport" runat="server" AllowPaging="false" ShowFooter="true"
                        AutoGenerateColumns="False" CaptionAlign="Top" CssClass="mGrid"                        
                        Width="98%" onrowdatabound="GrdReport_RowDataBound" OnRowCommand="GrdReport_RowCommand"
                        onpageindexchanging="GrdReport_PageIndexChanging" PageSize="30000" >
                        <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">                        
                        <ItemTemplate>
                        <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                         Width="5px" />
                         </asp:TemplateField>                   
                            <asp:BoundField DataField="Category" HeaderText="Category" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProductCode" HeaderText="Code" Visible="true">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                            </asp:BoundField>                            
                            <asp:BoundField DataField="Product" HeaderText="Product">
                              <%--  <FooterStyle Font-Bold="True" HorizontalAlign="Right" VerticalAlign="Middle"  ForeColor="White"/>--%>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="ProductUnit" HeaderText="Unit">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                        
                              <asp:BoundField DataField="ProductMRP" HeaderText="MRP">
                                <FooterStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Left"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                            </asp:BoundField>
                                <asp:BoundField DataField="StockLocation" HeaderText="Location">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" 
                                    VerticalAlign="Middle" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Opening" HeaderText="Opening">                               
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Purchase" HeaderText="Purchase">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                              <asp:BoundField DataField="Sales" HeaderText="Sales" Visible="false">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"  />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SalesReturn" HeaderText="SalesReturn" Visible="false">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False"  CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ReturnToSupplier" HeaderText="PurchaseReturn">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="Inward" HeaderText="Inward">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Outward" HeaderText="Issue">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                                     <asp:BoundField DataField="OutwardReturn" HeaderText="Issuse Return">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                              <asp:BoundField DataField="Damage" HeaderText="Damage">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="TransferIN" HeaderText="Transfer IN" >
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TransferOUT" HeaderText="Transfer OUT" >
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                            
                           <asp:BoundField DataField="Consumption" HeaderText="Consumption">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="Closing" HeaderText="Closing">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>
                            
                            
                            <asp:BoundField DataField="Amount" HeaderText="Amount">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" CssClass="Display_None" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="DrawingNo" HeaderText="DrawingNo">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False"  />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False"   />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True"   />
                            </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="View Drawing">
                        <ItemTemplate>
                           <asp:LinkButton runat="server" ID="lnkView"  
                             CommandName="VIEW" 
                             PostBackUrl='<%# "Drawing.aspx?RowIndex=" +
                            Container.DataItemIndex %>' target="_blank" CssClass="Display_None"
                             >View Deal</asp:LinkButton>
                             <a href='<%# "Drawing.aspx?RowIndex=" +
                            Container.DataItemIndex %>'  target="_blank">View Drawing PDF</a>
                             
                             </ItemTemplate>
                           </asp:TemplateField>
      
      
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

