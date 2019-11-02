<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="SUNDRYCREDITOR.aspx.cs" Inherits="MISACCOUNT_SUNDRYCREDITOR" Title="Outstanding Report" %>
<%@ Register Assembly="AjaxControlToolKit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
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
    <div class="PageTitle">
        OUTSTANDING 
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
 <fieldset id="F1" class="FieldSet" runat="server">
        <asp:UpdatePanel ID="UpPnlBody" runat="server">
            <ContentTemplate>
                <table width="100%">
                   <%-- <tr>
                        <td colspan="5">
                        </td>
                    </tr>--%>
                    <tr>
                       <td align="right">
                            <asp:CheckBox ID="ChkDate" runat="server" CssClass="CheckBox" Text=" " 
                                AutoPostBack="True" TabIndex="1" 
                                oncheckedchanged="ChkDate_CheckedChanged"/>
                            From Date :
                        </td>
                        <td width="40%">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Width="80px" TabIndex="2"></asp:TextBox>
                            <asp:ImageButton ID="ImageFromDate" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="3" />
                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                PopupButtonID="ImageFromDate" TargetControlID="txtFromDate" />
                            &nbsp;&nbsp;&nbsp;To Date :
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" Width="80px" TabIndex="4"></asp:TextBox>
                            <asp:ImageButton ID="ImageToDate" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="5" />
                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                PopupButtonID="ImageToDate" TargetControlID="txtToDate" />
                              
                        </td>
                     
                        <td width="40%">

                        <asp:Button ID="BtnShow" runat="server" CssClass="button" TabIndex="6" Text="Show"
                            ValidationGroup="Add" onclick="BtnShow_Click"  />
                    <%--</td>
                    <td>--%>
                        <asp:Button ID="BtnPrint" runat="server" CssClass="Display_None" TabIndex="7" Text="Print"
                            ValidationGroup="Add" OnClientClick="aspnetForm.target ='_blank';"  />
                   <%-- </td>
                    <td>--%>
                       
                   <%-- </td>
                                                
                        <td>--%>
                       
                             <asp:Button ID="BtnCancel" runat="server" CssClass="button" TabIndex="8" Text="Cancel"
                            ValidationGroup="Add" onclick="BtnCancel_Click"  />
                             </td>
                       </tr>
                    
                </table>
            </ContentTemplate>
              <Triggers>
    <asp:PostBackTrigger ControlID="BtnExport" />
</Triggers>
        </asp:UpdatePanel>
    </fieldset>
    
    <asp:UpdatePanel ID="UpPnlGrid" runat="server">
        <ContentTemplate>
     <%--<div class="MISscrollableDiv3">--%>
                 <table width="100%">
                <tr>
                    
                                    <td colspan="2" align="center">
                                        <asp:Label ID="LblRecordCount" runat="server" CssClass="Label3" Font-Bold="True" 
                                            Font-Size="Small" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                
                                <td colspan="2" align="right">
                                    <asp:Label ID="LblFrmDate" runat="server" CssClass="Label3" ></asp:Label>
                                    <asp:Label ID="TO" runat="server" Text="TO" CssClass="Label3" ></asp:Label>
                                    <asp:Label ID="LblToDate" runat="server" CssClass="Label3" ></asp:Label>
                                </td>
                                </tr>
                                <tr>
                                <td colspan="2" align="right">
                                 <asp:Label ID="Label1" runat="server" CssClass="Label3" Text=""></asp:Label>
                                </td>
                                </tr>
                                <tr>
                                <td>
                                 <asp:Label ID="Label2" runat="server" CssClass="Label3" Text=""></asp:Label>
                                </td>
                                <td >
                                <asp:Label ID="Label3" runat="server" CssClass="Label3" Text=""></asp:Label>
                                </td>
                                </tr>
                           
                
                <tr runat="server" id="TR0">
                <td  colspan="2">
                
                <fieldset ID="Fieldset1" runat="server" class="FieldSet">
                <legend ID="Legend1" runat="server" class="legend">Group Summary</legend>
                     <table width="100%">
                <tr>
                <td align="right">
                <asp:Button ID="BtnPrintGroupSummary" runat="server" CssClass="button" 
                        TabIndex="9" Text="Print"
                ValidationGroup="Add" OnClientClick="aspnetForm.target ='_blank';" 
                        onclick="BtnPrintGroupSummary_Click"  />
                      <asp:Button ID="BtnExport" runat="server" CssClass="button" TabIndex="10" Text="Export"
                            onclick="BtnExport_Click"   ToolTip="Export GroupSummary" />
                </td>
                </tr>
                <tr>
                <td>
               <div class="MISscrollableDiv2">
                <asp:GridView ID="GrdReport" runat="server" AllowPaging="false" PageSize="30" AutoGenerateColumns="False"
                                CaptionAlign="Top" CssClass="mGrid1" PagerStyle-CssClass="pgr" 
                        Width="100%" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" ForeColor="Black"
                                GridLines="Horizontal"  ShowFooter="True" 
                        onrowdatabound="GrdReport_RowDataBound" 
                        onrowcommand="GrdReport_RowCommand"  >
                                
                                <Columns>
                                    <asp:TemplateField>
                                    <ItemTemplate>  
                                    <asp:ImageButton ID="ImageGridEdit" runat="server" 
                                    CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                    CommandName="Select" ImageUrl="~/Images/New Icon/ViewRecord.png" 
                                    ToolTip="Show Ledger Monthly Summary" TabIndex="10"/>
                                <%--    <a href='' target="_blank">
                                    <asp:Image ID="ImgBtnPrint" runat="server" 
                                    ImageUrl="~/Images/Icon/GridPrint.png" TabIndex="29" 
                                    ToolTip="Print Issue Register" />
                                    </a>--%>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"
                                    Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="5px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="5px" />
                                    </asp:TemplateField>
                                   
                                    <asp:BoundField DataField="Particulars" HeaderText="PARTICULARS">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                            ForeColor="White" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Debit" HeaderText="DEBIT">
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                            ForeColor="White" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Credit" HeaderText="CREDIT">
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" Font-Bold="true"
                                            ForeColor="White" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LedgerID" HeaderText="LedgerID">
                                        <HeaderStyle Wrap="False" CssClass="Display_None" />
                                        <ItemStyle Wrap="False" CssClass="Display_None"/>
                                        <FooterStyle Wrap="False" CssClass="Display_None"/>
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <FooterStyle CssClass="ftr" />
                            </asp:GridView>
                 </div>
                  </td>
                </tr>
                   </table>
                 </fieldset>
               <%--  </div>--%>
            
                </td>
                </tr>
                
                 <tr id="TR1" runat="server">
              <td align="left" colspan="2">
              <div class="MISscrollableDiv2">
                  <fieldset ID="Fieldset2" runat="server" class="FieldSet">
                    <legend ID="Legend2" runat="server" class="legend" >Ledger Monthly Summary</legend>
                    <table width="100%">
                      <tr>
                    <td align="right">
                    <asp:Button ID="BtnPrintLedgerMonthSummary" runat="server" CssClass="button" 
                            TabIndex="11" Text="Print"
                    ValidationGroup="Add" OnClientClick="aspnetForm.target ='_blank';" 
                            onclick="BtnPrintLedgerMonthSummary_Click"  />
                               <asp:Button ID="BtnExportLedgerMonthSummary" runat="server" CssClass="button" TabIndex="12" Text="Export"
                            ValidationGroup="Add" onclick="BtnExportLedgerMonthSummary_Click"   ToolTip="Export LedgerMonthSummary" />
                    </td>
                    </tr>
                    <tr>
                    <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                    <ContentTemplate>
                    <asp:GridView ID="GrdLedgersummary" runat="server" AllowPaging="false" PageSize="30" AutoGenerateColumns="False"
                    CaptionAlign="Top" CssClass="mGrid1" PagerStyle-CssClass="pgr" Width="100%" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                    GridLines="Horizontal"  ShowFooter="True" 
                            onrowdatabound="GrdLedgersummary_RowDataBound" 
                            onrowcommand="GrdLedgersummary_RowCommand" >
                    <Columns>
                    <asp:TemplateField>
                    <ItemTemplate>  
                    <asp:ImageButton ID="ImageGridEdit" runat="server" 
                    CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                    CommandName="Select" ImageUrl="~/Images/New Icon/ViewRecord.png"
                     ToolTip="Show Ledger Requisition" TabIndex="12"/>
                    
                   <%-- <a href='../PrintReport/MaterialReqTemplatePrint.aspx?ID=<%# Eval("#")%>&amp;Flag=<%="PS"%>' 
                    target="_blank">
                    <asp:Image ID="ImgBtnPrint" runat="server" 
                    ImageUrl="~/Images/Icon/GridPrint.png" TabIndex="29" 
                    ToolTip="Print Issue Register" />
                    </a>--%>
                    
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"
                    Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sr.No.">
                    <ItemTemplate>
                    <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="5px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="5px" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Particulars" HeaderText="PARTICULARS">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" 
                    ForeColor="White" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Debit" HeaderText="DEBIT">
                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" 
                    ForeColor="White" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Credit" HeaderText="CREDIT">
                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" 
                    ForeColor="White" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Closing" HeaderText="Closing">
                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" 
                    ForeColor="White" />
                    </asp:BoundField>
                      <asp:BoundField DataField="Closing1" HeaderText="Closing1">
                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" 
                    ForeColor="White" />
                    </asp:BoundField>
                     <asp:BoundField DataField="LedgerID" HeaderText="LedgerID" >
                     <HeaderStyle Wrap="False" CssClass="Display_None" />
                     <ItemStyle Wrap="False" CssClass="Display_None"/>
                     <FooterStyle Wrap="False" CssClass="Display_None"/>
                     </asp:BoundField>
                       <asp:BoundField DataField="ForMonth" HeaderText="ForMonth" >
                     <HeaderStyle Wrap="False" CssClass="Display_None" />
                     <ItemStyle Wrap="False" CssClass="Display_None"/>
                     <FooterStyle Wrap="False" CssClass="Display_None"/>
                     </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <FooterStyle CssClass="ftr" />
                    </asp:GridView>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    </tr>

                    <tr>
                    <td align="left">
                    <asp:LinkButton ID="hyl_Hide" runat="server" CssClass="linkButton" 
                            onclick="hyl_Hide_Click" >
                        Back</asp:LinkButton>
                    </td>
                    </tr>
                    </table>
                    </fieldset>
              </div>
              </td>
              </tr>
              
              <tr id="TR2" runat="server">
              <td colspan="2">
               <div class="MISscrollableDiv2">
               <fieldset ID="Fieldset3" runat="server" class="FieldSet">
                    <legend ID="Legend3" runat="server" class="legend" >Ledger Voucher</legend>
                    <table width="100%">
                      <tr>
                    <td align="right">
                    <asp:Button ID="BtnLedgerVoucher" runat="server" CssClass="button" 
                            TabIndex="12" Text="Print"
                    ValidationGroup="Add" OnClientClick="aspnetForm.target ='_blank';" 
                            onclick="BtnLedgerVoucher_Click"  />
                       <asp:Button ID="BtnExportLedgerVoucher" runat="server" CssClass="button" TabIndex="13" Text="Export"
                           onclick="BtnExportLedgerVoucher_Click"   ToolTip="Export LedgerVoucher" />
                    </td>
                    </tr>
                    <tr>
                    <td>
               <asp:GridView ID="GrdLedgerVoucher" runat="server" AllowPaging="false" PageSize="30" AutoGenerateColumns="False"
                                CaptionAlign="Top" CssClass="mGrid1" PagerStyle-CssClass="pgr" Width="100%" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None"  BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                GridLines="Horizontal"  ShowFooter="True"   onrowdatabound="GrdLedgerVoucher_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                    <ItemTemplate>  
                                  <%--  <asp:ImageButton ID="ImageGridEdit" runat="server" 
                                    CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                    CommandName="Select" ImageUrl="~/Images/New Icon/ViewRecord.png" ToolTip="Show Items Of Requisition" />
                                    --%>
                                 <%--   <a href='../PrintReport/MaterialReqTemplatePrint.aspx?ID=<%# Eval("#")%>&amp;Flag=<%="PS"%>' 
                                    target="_blank">
                                    <asp:Image ID="ImgBtnPrint" runat="server" 
                                    ImageUrl="~/Images/Icon/GridPrint.png" TabIndex="29" 
                                    ToolTip="Print Issue Register" />
                                    </a>--%>
                                    
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"
                                    Wrap="False" />
                                    
                                   
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="5px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="5px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VchDate" HeaderText="DATE">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Particulars" HeaderText="PARTICULARS">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" 
                                            ForeColor="White" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VoucherType" HeaderText="VCH TYPE">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" 
                                            ForeColor="White" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VoucherID" HeaderText="VCH NO">
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" 
                                            ForeColor="White" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Debit" HeaderText="DEBIT">
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" 
                                            ForeColor="White" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Credit" HeaderText="CREDIT">
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" 
                                        ForeColor="White" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <FooterStyle CssClass="ftr" />
                            </asp:GridView>
                </td>
                </tr>
                </table>
                            
                             <asp:LinkButton ID="hy2_Hide" runat="server" CssClass="linkButton" 
                        onclick="hy2_Hide_Click" >
                    Back</asp:LinkButton>
             </fieldset>
               </div>
              </td>
              </tr>
            
            </table>
         <%-- </div>--%>


        </ContentTemplate>
          <Triggers>
        <asp:PostBackTrigger ControlID="BtnPrintGroupSummary" />
         <asp:PostBackTrigger ControlID="BtnPrintGroupSummary" />
        <asp:PostBackTrigger ControlID="BtnPrintLedgerMonthSummary" />
          <asp:PostBackTrigger ControlID="BtnLedgerVoucher" />
          <asp:PostBackTrigger ControlID="BtnExportLedgerMonthSummary" />
          <asp:PostBackTrigger ControlID="BtnExportLedgerVoucher" />
          
          
</Triggers>
    </asp:UpdatePanel>
</asp:Content>

