<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="StockReportNoGridView.aspx.cs" Inherits="Reports_StockReportNoGridView" Title="Stock Report" %>
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
        <ProgressTemplate>            
        <div id="progressBackgroundFilter"></div>
        <div id="processMessage">   
        <center><span class="SubTitle">Loading....!!! </span></center>
        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
        </div>
        </ProgressTemplate>
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
            <td class="Label" >
                Category :</td>
            <td >
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="ComboBox" TabIndex="2"
                    Width="152px">
                </asp:DropDownList>
            </td>
            <td class="Label">
                 Location :</td>
                 <td>
                   <asp:DropDownList ID="ddlLocation" runat="server" CssClass="ComboBox" TabIndex="3"
                    Width="152px">
                </asp:DropDownList>
                 </td>
        </tr>
        <tr>
            <td class="Label">
                Item :</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlItemName" runat="server" CssClass="ComboBox" 
                    TabIndex="2" Width="250px">
                </asp:DropDownList>
            </td>
            <td class="Label">
              </td>
            <td>
                               
            </td>
            <td class="Label">
                Unit :</td>
            <td>
                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="ComboBox" TabIndex="3" 
                    Width="152px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="Label">
                &nbsp;</td>
            <td colspan="3">
                &nbsp;</td>
            <td class="Label">
               </td>
            <td>
               
                
                &nbsp;</td>
            <td class="Label">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Label">
                &nbsp;</td>
            <td colspan="3">
                &nbsp;</td>
            <td class="Label">
              </td>
            <td>
               
            </td>
            <td class="Label">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Label">
                &nbsp;</td>
            <td colspan="3">
                &nbsp;</td>
            <td class="Label">
               </td>
            <td>
               
            </td>
            <td class="Label">
                &nbsp;</td>
            <td>
                &nbsp;</td>
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
                        AutoGenerateColumns="true" CaptionAlign="Top" CssClass="mGrid"                        
                        Width="98%" onrowdatabound="GrdReport_RowDataBound" RowStyle-Wrap="false"
                        HeaderStyle-Wrap="false" FooterStyle-Font-Bold="true" FooterStyle-ForeColor="White"
                        onpageindexchanging="GrdReport_PageIndexChanging" PageSize="30000">
                        <AlternatingRowStyle CssClass="alt" />
                          <FooterStyle />
                           <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
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

