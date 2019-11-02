﻿<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="ConsumptionReport.aspx.cs" Inherits="Reports_ConsumptionReport" Title="Consumption Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
 <script src="../Jscript/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Jscript/ScrollableGrid.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
        
        }
)

function FreGrid()
{
       $('#<%=GrdReport.ClientID %>').Scrollable();
}

function FreGrid1()
{
       $('#<%=GrdReport.ClientID %>').Scrollable({ScrollHeight: 100});
}

</script>
 <script type="text/javascript" language="javascript"> 
        function RateOption()
        {   
            var RDO_FromTo=document.getElementById('<%= RDO_FromTo.ClientID %>');            
            var RDO_GreaterThenEqualTo=document.getElementById('<%= RDO_GreaterThenEqualTo.ClientID %>');            
            var RDO_LessThenEqualTo=document.getElementById('<%= RDO_LessThenEqualTo.ClientID %>');            
            var RDO_EqualTo=document.getElementById('<%= RDO_EqualTo.ClientID %>');     
            
            var _txtFromRate = document.getElementById('<%= txtFromRate.ClientID %>');  
            
            var t1=0;
            if(_txtFromRate.value != "") t1=_txtFromRate.value;             
            
            if(RDO_FromTo.checked)
            {     
                document.getElementById('<%= txtFromRate.ClientID %>').removeAttribute('readOnly'); //remove ReadOnly Property                
                document.getElementById('<%= txtToRate.ClientID %>').removeAttribute('readOnly');  //remove ReadOnly Property 
                document.getElementById('<%= txtGreaterRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtLessRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtEqualRate.ClientID %>').setAttribute('readonly',true); 
                
                document.getElementById('<%= txtFromRate.ClientID %>').value='0.00';
                document.getElementById('<%= txtToRate.ClientID %>').value='0.00';
                document.getElementById('<%= txtGreaterRate.ClientID %>').value='';
                document.getElementById('<%= txtLessRate.ClientID %>').value='';
                document.getElementById('<%= txtEqualRate.ClientID %>').value='';
            }  
            else if(RDO_GreaterThenEqualTo.checked)
            {            
                document.getElementById('<%= txtFromRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtToRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtGreaterRate.ClientID %>').removeAttribute('readOnly'); //remove ReadOnly Property
                document.getElementById('<%= txtLessRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtEqualRate.ClientID %>').setAttribute('readonly',true); 
                
                document.getElementById('<%= txtFromRate.ClientID %>').value='';
                document.getElementById('<%= txtToRate.ClientID %>').value='';
                document.getElementById('<%= txtGreaterRate.ClientID %>').value='0.00';
                document.getElementById('<%= txtLessRate.ClientID %>').value='';
                document.getElementById('<%= txtEqualRate.ClientID %>').value='';
            }
            else if(RDO_LessThenEqualTo.checked)
            {            
                document.getElementById('<%= txtFromRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtToRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtGreaterRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtLessRate.ClientID %>').removeAttribute('readOnly'); //remove ReadOnly Property
                document.getElementById('<%= txtEqualRate.ClientID %>').setAttribute('readonly',true);
                
                document.getElementById('<%= txtFromRate.ClientID %>').value='';
                document.getElementById('<%= txtToRate.ClientID %>').value='';
                document.getElementById('<%= txtGreaterRate.ClientID %>').value='';
                document.getElementById('<%= txtLessRate.ClientID %>').value='0.00';
                document.getElementById('<%= txtEqualRate.ClientID %>').value=''; 
            }
            else if(RDO_EqualTo.checked)
            {            
                document.getElementById('<%= txtFromRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtToRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtGreaterRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtLessRate.ClientID %>').setAttribute('readonly',true); 
                document.getElementById('<%= txtEqualRate.ClientID %>').removeAttribute('readOnly'); //remove ReadOnly Property
                
                document.getElementById('<%= txtFromRate.ClientID %>').value='';
                document.getElementById('<%= txtToRate.ClientID %>').value='';
                document.getElementById('<%= txtGreaterRate.ClientID %>').value='';
                document.getElementById('<%= txtLessRate.ClientID %>').value='';
                document.getElementById('<%= txtEqualRate.ClientID %>').value='0.00';
            }          
        }
        </script>
  <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</ajax:ToolkitScriptManager>
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
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
Consumption Report
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UPEntry" runat="server">
<ContentTemplate>
<fieldset id="F1" runat="server" class="FieldSet" width="98%">
<div width="100%">
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
                <asp:RadioButton ID="RDO_FromTo" runat="server" GroupName="Rate" 
                    TextAlign="Left" OnClick="javascript:RateOption();" />
                If Rate :</td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtFromRate" runat="server" CssClass="TextBox" Width="50px"></asp:TextBox>
                
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; To&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtToRate" runat="server" CssClass="TextBox" Width="50px"></asp:TextBox>
                
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
                <asp:RadioButton ID="RDO_GreaterThenEqualTo" runat="server" GroupName="Rate" 
                    TextAlign="Left" OnClick="javascript:RateOption();"/>
                If Rate :</td>
            <td>
                &gt;=
                <asp:TextBox ID="txtGreaterRate" runat="server" CssClass="TextBox" Width="50px"></asp:TextBox>
                
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
                <asp:RadioButton ID="RDO_LessThenEqualTo" runat="server" GroupName="Rate" 
                    TextAlign="Left" OnClick="javascript:RateOption();"/>
                If Rate :</td>
            <td>
                &lt;=
                <asp:TextBox ID="txtLessRate" runat="server" CssClass="TextBox" Width="50px"></asp:TextBox>
                &nbsp;
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
                <asp:RadioButton ID="RDO_EqualTo" runat="server" GroupName="Rate" 
                    TextAlign="Left" OnClick="javascript:RateOption();"/>
                If Rate :</td>
            <td>
                &nbsp; =&nbsp;<asp:TextBox ID="txtEqualRate" runat="server" CssClass="TextBox" Width="50px"></asp:TextBox>
                &nbsp;
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
                    ToolTip="Clear The Details" onclick="BtnCancel_Click"   />
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
                    ToolTip="Export To Excel" onclick="ImgBtnExport_Click"   />
            </td>
            </tr> 
            <tr>
          <%--   ShowFooter="True" --%>
                <td colspan="2" align="center">
                <div id="divPrint" class="ScrollableDiv_FixHeightWidth4"  style="width:98%">
                    <asp:GridView ID="GrdReport" runat="server" AllowPaging="false" ShowFooter="true"
                        AutoGenerateColumns="False" CaptionAlign="Top" CssClass="mGrid"                        
                        Width="98%"  PageSize="30" 
                        onpageindexchanging="GrdReport_PageIndexChanging" 
                        onrowdatabound="GrdReport_RowDataBound"  >
                        <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">                        
                        <ItemTemplate>
                        <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                         Width="5px" />
                         </asp:TemplateField>                   
                            <asp:BoundField DataField="Category" HeaderText="Category">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProductCode" HeaderText="Code" >
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>                            
                            <asp:BoundField DataField="Product" HeaderText="Product">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProductUnit" HeaderText="Unit">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                        
                              <asp:BoundField DataField="ProductMRP" HeaderText="MRP">
                                <FooterStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Left"  VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                                <asp:BoundField DataField="StockLocation" HeaderText="Location">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Right" 
                                    VerticalAlign="Middle" />
                            </asp:BoundField>
                           <%--  <asp:BoundField DataField="Opening" HeaderText="Opening">                               
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Purchase" HeaderText="Purchase">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Sales" HeaderText="Sales">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SalesReturn" HeaderText="SalesReturn">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Inward" HeaderText="Inward">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Outward" HeaderText="Outward">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>
                            
                          <%--  <asp:BoundField DataField="TransferIN" HeaderText="Transfer IN">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TransferOUT" HeaderText="Transfer OUT">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>--%>
                            
                            <asp:BoundField DataField="Damage" HeaderText="Damage">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>
                      <%--      <asp:BoundField DataField="ReturnToSupplier" HeaderText="PurchaseReturn">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Closing" HeaderText="Closing">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle ForeColor="White" HorizontalAlign="Right" VerticalAlign="Middle" 
                                    Font-Bold="True" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Amount" HeaderText="Amount">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle" Wrap="False" />
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
   </div>
</fieldset>
</ContentTemplate>
            <Triggers>
             <asp:PostBackTrigger ControlID ="ImgBtnExport" />
             </Triggers>
</asp:UpdatePanel>
</asp:Content>

