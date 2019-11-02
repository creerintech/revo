<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="PO_Remaining.aspx.cs" Inherits="Reports_PO_Remaining" Title="Remaining Purchase Order Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div id="progressBackgroundFilter">
                    </div>
                    <div id="processMessage">
                        <center>
                            <span class="SubTitle">Loading....!!! </span>
                        </center>
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif"
                            Height="20px" Width="120px" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="Server">
    Remaining Purchase Order Report
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="UPEntry" runat="server">
        <ContentTemplate>
            <fieldset id="F1" runat="server" class="FieldSet">
            <center>
                <table width="50%"  align="center">
                <tr><td></td></tr>
                    <tr align="center">
            <td>
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Width="90px"> </asp:TextBox>
                  <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" 
                    Format="dd-MMM-yyyy" PopupButtonID="ImageButton212" TargetControlID="txtFromDate" />
                  <asp:ImageButton ID="ImageButton212" runat="server" CausesValidation="False" 
                    CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="1" />      
                    </td>
            <td>
                   &nbsp;<asp:Button ID="BtnShow" runat="server" CssClass="button" 
                    TabIndex="4" Text="Show" ValidationGroup="Add" 
                    ToolTip="Show Details" onclick="BtnShow_Click" />   
               <asp:Button ID="BtnCancel" runat="server" CssClass="button" 
                    TabIndex="5" Text="Cancel"  
                    ToolTip="Clear The Details" onclick="BtnCancel_Click"  />
            </td> 
        </tr>
                </table>
            </center>
  <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblCount" runat="server" CssClass="SubTitle"></asp:Label>
            </td>
            <td align="right" valign="middle">
               <%--<asp:ImageButton ID="ImgBtnPrint" runat="server" TabIndex="6"
                  OnClientClick="javascript:CallPrint('divPrint')"
                    ImageUrl="~/Images/Icon/Print-Icon.png" 
                    ToolTip="Print Report" onclick="ImgBtnPrint_Click"  />--%>
                <asp:ImageButton ID="ImgBtnExport" runat="server" 
                    ImageUrl="~/Images/Icon/excel-icon.png" TabIndex="7"
                    ToolTip="Export To Excel" onclick="ImgBtnExport_Click"  />
            </td>
            </tr> 
            <tr>
                <td colspan="2" align="center">
                <div style="width:1000px; overflow:scroll; height:400px;" >
                    <asp:GridView ID="GrdReport" runat="server" 
                        AutoGenerateColumns="False" CaptionAlign="Top" CssClass="mGrid"                        
                        Width="100%" ShowFooter="True" AllowPaging="false" 
                          EmptyDataText="Sorry....No Records Found...!!!" >
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
                         <asp:BoundField DataField="PONo" HeaderText="PO NO">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                         <asp:BoundField DataField="PODate" HeaderText="PO Date">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SuplierName" HeaderText="Supplier Name">
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
                         <asp:BoundField DataField="Qty" HeaderText="Qty">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Rate" HeaderText="Rate">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NetAmount" HeaderText="Net Amount">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Wrap="False" />
                        <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                        VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name">
                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" Wrap="False" />
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
            
        
            </table>
</fieldset>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ImgBtnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
