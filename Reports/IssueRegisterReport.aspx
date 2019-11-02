<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="IssueRegisterReport.aspx.cs" Inherits="Reports_IssueRegisterReport" Title="Material Issue Register Summary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<script type="text/javascript" language="javascript">
function chkCommissionDate()
{
if(document.getElementById("<%= ChkFrmDate.ClientID%>").checked == true)
{
document.getElementById("<%= txtToDate.ClientID%>").disabled = false;
document.getElementById("<%= txtFromDate.ClientID%>").disabled = false;
}
else
{
document.getElementById("<%= txtToDate.ClientID%>").disabled = true;
document.getElementById("<%= txtFromDate.ClientID%>").disabled = true;
}
}
</script>
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
    Material Issue Report    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UPEntry" runat="server">
<ContentTemplate>
<fieldset id="F1" runat="server" class="FieldSet">
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
        <%--OnChange="javascript:chkCommissionDate();"--%>
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
                       ></asp:TextBox>
                  <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" 
                    Format="dd-MMM-yyyy" PopupButtonID="ImageButton212" TargetControlID="txtFromDate" />
                  <asp:ImageButton ID="ImageButton212" runat="server" CausesValidation="False" 
                    CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="1" />      
                    </td>
                    <td class="Label">
                        &nbsp;&nbsp;&nbsp;To :</td>                
                     <td>
                     <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" 
                     Width="90px"></asp:TextBox>
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
                M.Issued No :</td>
            <td >
                <asp:DropDownList ID="ddlNo" runat="server" CssClass="ComboBox" TabIndex="2"
                    Width="152px">
                </asp:DropDownList>
            </td>
            <td class="Label">
                 M.Issued To :</td>
                 <td>
                   <asp:DropDownList ID="ddlTo" runat="server" CssClass="ComboBox" TabIndex="3"
                    Width="152px">
                </asp:DropDownList>
                 </td>
        </tr>
        <tr>           
            <td class="Label" colspan="7">
                <br />
            </td>
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
                <div id="divPrint" class="ScrollableDiv_FixHeightWidth4" style="width:98%">
                    <asp:GridView ID="GrdReport" runat="server" AllowPaging="false" 
                        AutoGenerateColumns="False" CaptionAlign="Top" CssClass="mGrid"                        
                        Width="100%" onpageindexchanging="GrdReport_PageIndexChanging" >
                        <Columns>
                        <asp:TemplateField HeaderText="Sr. No.">                        
                        <ItemTemplate>
                        <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                         Width="6%" />
                         </asp:TemplateField>
                            <asp:BoundField DataField="IssueNo" HeaderText="Issue No">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IssueDate" HeaderText="Issue Date">
                              <%--  <FooterStyle Font-Bold="True" HorizontalAlign="Right" VerticalAlign="Middle"  ForeColor="White"/>--%>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RequisitionNo" HeaderText="Req.No">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RequisitionDate" HeaderText="Req. Date">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                        
                              <asp:BoundField DataField="EmpName" HeaderText="Employee">
                               <%-- <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />--%>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left"  VerticalAlign="Middle" Wrap="False" />
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

