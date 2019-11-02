<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="Customermaster.aspx.cs" Inherits="Masters_Customermaster" %>


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
    
        Search for Customer :
     <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
     Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged">
     </asp:TextBox>
        <br />



            <asp:Label ID="lblcounttext" Text="Total No. of Customer:- " runat="server"></asp:Label>
                                   <asp:Label ID="lblcount" Text="0" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
     <div id="divwidth"></div>
     <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
      TargetControlID="TxtSearch" CompletionInterval="100"                             
      UseContextKey="True" FirstRowSelected ="true" ShowOnlyCurrentWordInCompletionListItem="true"
       ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender"
      CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
    </ajax:AutoCompleteExtender> 
    </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Customer Master    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
  <ContentTemplate >
   <table width="100%">
  <tr>
  <td>
  
   <fieldset id="fieldset1" width="100%" runat="server" class="FieldSet">
   
    <table width="100%" cellspacing="6">
       
        <tr>
            <td class="Label">
                Code:</td>
            <td>
            
                 <asp:TextBox ID="TxtSuppCode" runat="server" CssClass="TextBox" 
                  MaxLength="50" Width="150px" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Label">
                Name :</td>
            <td>
                <asp:TextBox ID="TxtSuppName" runat="server" CssClass="TextBox" 
                 Width="368px" Height="17px"></asp:TextBox>&nbsp;<asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                <asp:RequiredFieldValidator ID="Req_Name" runat="server"
                     ControlToValidate="TxtSuppName" Display="None" 
                    ErrorMessage="Supplier Name is Required" SetFocusOnError="True" 
                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" 
                    Enabled="True" TargetControlID="Req_Name" 
                    WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>
                      
            </td>
           <%-- <td>
                
           </td>--%>
            
        </tr>
        <tr>
            <td class="LabelMultiLine">
                Address :</td>
            <td>
               <asp:TextBox ID="TxtSuppAddress" runat="server" CssClass="TextBox" 
                 MaxLength="50" Width="368px" TextMode="MultiLine" Height="60px"></asp:TextBox></td>
        </tr>
        
        <tr>
            <td class="Label" colspan="1">
                     Tel No :
                 </td>  
                 <td>
                     <table>
                         <tr>
                             <td>
                                 <asp:TextBox ID="TxtTelNo" runat="server" CssClass="TextBox" MaxLength="13" 
                                     Width="150px">
                                 </asp:TextBox>
                                 <ajax:FilteredTextBoxExtender ID="FTE_Tel" runat="server" TargetControlID="TxtTelNo"
                                    FilterType="Custom,Numbers" ValidChars="-"></ajax:FilteredTextBoxExtender>
                             </td>
                             <td class="Label">
                                 Mobile No :
                             </td>
                             <td>
                                 <asp:TextBox ID="TxtMobile" runat="server" CssClass="TextBox" MaxLength="13" 
                                     Width="150px">
                                 </asp:TextBox>
                              
                                 <ajax:FilteredTextBoxExtender ID="FTE_Mobile" runat="server" TargetControlID="TxtMobile"
                                    FilterType="Custom,Numbers" ValidChars="+"></ajax:FilteredTextBoxExtender>
                             </td>
                         </tr>
                     </table>                            
                 </td>
           
        </tr>
        
        <tr>
            <td class="Label" colspan="1">
                     Fax No :
                 </td>  
                 <td>
                     <table>
                         <tr>
                             <td>
                                 <asp:TextBox ID="TxtFaxNo" runat="server" CssClass="TextBox" MaxLength="13" 
                                     Width="150px">
                                 </asp:TextBox>
                                 <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TxtFaxNo"
                                    FilterType="Custom,Numbers" ValidChars="-"></ajax:FilteredTextBoxExtender>
                             </td>
                             <td class="Label">
                                  State :
                             </td>
                             <td>
                                 <asp:DropDownList ID="ddlState" runat="server" CssClass="ComboBox"  TabIndex="4" Width="160px"
                                 AutoPostBack="true">
                            </asp:DropDownList>
                             </td>
                         </tr>
                     </table>                            
                 </td>
                 
                 
           
        </tr>
        
        <tr>
            <td class="Label">
                Email ID :</td>
            <td colspan="1">
                <asp:TextBox ID="TxtEmail" runat="server" CssClass="TextBox" 
                    MaxLength="50" Width="368px"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="REV2" runat="server" Display="None" 
                        ErrorMessage="Please Enter Valid Email ID..!" ControlToValidate="TxtEmail" 
                       ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Add">
                  </asp:RegularExpressionValidator>
               <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" 
                    Enabled="True" TargetControlID="REV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>     
            </td>
        </tr>
        
         <tr>
            <td class="Label">
                Website :</td>
            <td colspan="1">
                <asp:TextBox ID="TxtWebsite" runat="server" CssClass="TextBox" 
                    MaxLength="50" Width="368px"></asp:TextBox>
                <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None" 
                        ErrorMessage="Please Enter Valid Website..!" ControlToValidate="TxtWebsite" 
                       ValidationExpression="([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" ValidationGroup="Add">
                  </asp:RegularExpressionValidator>
               <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                    Enabled="True" TargetControlID="RegularExpressionValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>     --%>
            </td>
        </tr>
        
        
        <tr>
            <td class="LabelMultiLine">
                Notes :</td>
            <td colspan="1">
               <asp:TextBox ID="TxtNotes" runat="server" CssClass="TextBox" 
                    MaxLength="50" Width="368px" TextMode="MultiLine" Height="60px"></asp:TextBox>
            </td>
        </tr>
        
<tr>
<td colspan="2"><hr />
</td>

</tr>
    
<tr>
<td class="Label">Person Name :
</td>

<td >
<asp:TextBox ID="TxtPerName" runat="server" CssClass="TextBox" 
Width="368px" Height="17px"></asp:TextBox>

</td>

</tr>

<tr>
<td class="Label">Designation :
</td>

<td >
<asp:TextBox ID="TxtDesignation" runat="server" CssClass="TextBox" 
Width="368px" Height="17px"></asp:TextBox>

</td>

</tr>

<tr>
<td class="Label">Mobile No :
</td>

<td >
<asp:TextBox ID="TxtContPerMobileNo" runat="server" CssClass="TextBox" MaxLength="13" Width="150px"></asp:TextBox>

<ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TxtContPerMobileNo"
FilterType="Custom,Numbers" ValidChars="+"></ajax:FilteredTextBoxExtender>
</td>

</tr>

<tr>
<td class="Label">
    Email ID :</td>
<td colspan="1">
<asp:TextBox ID="TxtContPerEmail" runat="server" CssClass="TextBox" 
MaxLength="50" Width="368px"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None" 
ErrorMessage="Please Enter Valid Email ID..!" ControlToValidate="TxtContPerEmail" 
ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Add">
</asp:RegularExpressionValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" 
Enabled="True" TargetControlID="RegularExpressionValidator2" WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>     
</td>
</tr>

<tr>
<td class="Label">
    Website :</td>
<td colspan="1">
<asp:TextBox ID="TxtContPerWebsite" runat="server" CssClass="TextBox" 
MaxLength="50" Width="368px"></asp:TextBox>
<%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="None" 
ErrorMessage="Please Enter Valid Website..!" ControlToValidate="TxtContPerWebsite" 
ValidationExpression="/^([a-z0-9_-]+\.)*[a-z0-9_-]+(\.[a-z]{2,6}){1,2}$/" ValidationGroup="Add">
</asp:RegularExpressionValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" 
Enabled="True" TargetControlID="RegularExpressionValidator3" WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>  --%>   
</td>
</tr>
<tr>
<td colspan="2"><hr />
</td>
</tr>
<tr>
<td class="Label">GST Registration No. :
</td>
<td >
<table><tr><td>
<asp:TextBox ID="TxtSTRN" runat="server" CssClass="TextBox" Width="140px" 
        MaxLength="15"></asp:TextBox></td>
<td>
<asp:UpdatePanel ID ="updatePanel3" runat ="server" >
<Triggers ><asp:PostBackTrigger ControlID="lnkSerTax" /></Triggers>
<ContentTemplate >
<asp:FileUpload ID="SerTaxRegNo" CssClass ="TextBox" runat="server"  Width="170px" 
        BorderStyle="None" ToolTip="Upload Only Image File"/>
<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="SerTaxRegNo"
Display="None" ErrorMessage="Upload Image File only" SetFocusOnError="True" ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G)|(b|B)(m|M)(p|P))$"	
ValidationGroup="Add"></asp:RegularExpressionValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RegularExpressionValidator4"
WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender> 
<asp:LinkButton ID="lnkSerTax" runat="server" ValidationGroup="AddGridComp" 
 onclick="lnkSerTax_Click">Upload</asp:LinkButton> 
</ContentTemplate> 
</asp:UpdatePanel> 
</td>
<td>
<asp:Label ID="lblSerTaxNopath" runat="server" Text="Label" Visible="false"></asp:Label>
<asp:Image ID="ImgTaxRegNoPath" runat="server" Height="30px" Width="40px" />
</td>
</tr></table>
</td>

</tr>
<%--<tr>
<td class="Label">Service Tax Jurisdiction :
</td>
<td >
<asp:TextBox ID="TxtSTJ" runat="server" CssClass="TextBox" Width="220px"></asp:TextBox>
</td>
</tr>

<tr>
<td class="Label">VAT No (if applicable) :
</td>
<td >
<asp:TextBox ID="TxtVatNo" runat="server" CssClass="TextBox" Width="220px"></asp:TextBox>
</td>
</tr>

<tr>
<td class="Label">Local Sales Tax No./ TIN NO :
</td>
<td >
<asp:TextBox ID="TxtTINNO" runat="server" CssClass="TextBox" Width="220px"></asp:TextBox>
</td>
</tr>


<tr>
<td class="Label">Central Sales Tax Registration No. :
</td>
<td >
<asp:TextBox ID="TxtCSTRN" runat="server" CssClass="TextBox" Width="220px"></asp:TextBox>
</td>
</tr>

<tr>
<td class="Label">Excise Range :
</td>
<td >
<asp:TextBox ID="TxtExciseRange" runat="server" CssClass="TextBox" Width="220px"></asp:TextBox>
</td>
</tr>

     

<tr>
<td class="Label">Excise Division :
</td>
<td >
<asp:TextBox ID="TxtExciseDivision" runat="server" CssClass="TextBox" Width="220px"></asp:TextBox>
</td>

</tr>
<tr>
<td class="Label">Excise Circle :
</td>
<td >
<asp:TextBox ID="TxtExciseCircle" runat="server" CssClass="TextBox" Width="220px"></asp:TextBox>
</td>
</tr>

<tr>
<td class="Label">Excise Zone :
</td>
<td >
<asp:TextBox ID="TxtExciseZone" runat="server" CssClass="TextBox" Width="220px"></asp:TextBox>
</td>
</tr>

<tr>
<td class="Label">Excise Collectorate :
</td>
<td >
<asp:TextBox ID="TxtExciseCollectorate" runat="server" CssClass="TextBox" Width="220px"></asp:TextBox>
</td>
</tr>


<tr>
<td class="Label">Excise ECC NO :
</td>
<td >
<asp:TextBox ID="TxtExciseECCNO" runat="server" CssClass="TextBox" Width="220px"></asp:TextBox>
</td>
</tr>

<tr>
<td class="Label">TIN / BIN No. ( If applicable) :
</td>
<td >
<asp:TextBox ID="TxtTINBINNO" runat="server" CssClass="TextBox" Width="220px"></asp:TextBox>
</td>
</tr>--%>


<tr>
<td class="Label">PAN No :
</td>
<td>
<table>
<tr>
<td>
<asp:TextBox ID="TxtPanNo" runat="server" CssClass="TextBox" Width="140px" 
        MaxLength="15"></asp:TextBox></td>
<td>
<asp:UpdatePanel ID ="updatePanel7" runat ="server" >
<Triggers ><asp:PostBackTrigger ControlID="lnkPanNo" /></Triggers>
<ContentTemplate>
<asp:FileUpload ID="PANCARDUpload" CssClass ="TextBox" runat="server"  Width="170px" BorderStyle="None" ToolTip="Upload Only Image File"/>
<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="PANCARDUpload"
Display="None" ErrorMessage="Upload Image File only" SetFocusOnError="True" ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G)|(b|B)(m|M)(p|P))$"	
ValidationGroup="Add"></asp:RegularExpressionValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RegularExpressionValidator6"
WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender> 
<asp:LinkButton ID="lnkPanNo" runat="server" ValidationGroup="AddGridComp" 
onclick="lnkPanNo_Click">Upload</asp:LinkButton> 
</ContentTemplate> 
</asp:UpdatePanel> 
</td>
<td>
<asp:Label ID="lblPanNo" runat="server" Text="Label" Visible="false"></asp:Label>
<asp:Image ID="ImgPanNoPath" runat="server" Height="30px" Width="40px" />
</td>
</tr></table>
</td>
</tr>

<tr>
<td class="Label">TDS certificate :
</td>
<td >
<asp:TextBox ID="TxtTDSCertificate" runat="server" CssClass="TextBox" Width="220px"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="2"><hr />
</td>
</tr>
<tr>
<td colspan="2">
<ajax:Accordion ID="Accordion1" runat="server" ContentCssClass="accordionContent1"
HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" FadeTransitions="true"
TransitionDuration="260" FramesPerSecond="20" AutoSize="None"
RequireOpenedPane="false"
SuppressHeaderPostbacks="true"
SelectedIndex="1">
<Panes>
<ajax:AccordionPane ID="AccordionPane1" runat="server" Width="100%">
<Header>
<a class="href" href="#">Terms and Conditions For Supplier</a></Header>
<Content>
<div style="width:100%">
<asp:GridView ID="GridTermCond" runat="server" AutoGenerateColumns="False" 
DataKeyNames="#" Width="100%"  CssClass="mGrid"
TabIndex="4" AllowSorting="True" 
ShowFooter="True">
<Columns>
<asp:TemplateField HeaderText="#" Visible="False">
<EditItemTemplate>
<asp:Label ID="LblEntryId0" runat="server" Width="1px"></asp:Label>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="1px"></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="All">
<HeaderTemplate>
<asp:CheckBox ID="GrdSelectAllHeader" runat="server" AutoPostBack="true" OnCheckedChanged="GrdSelectAllHeader1_CheckedChanged"/>
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="GrdSelectAll" runat="server" AutoPostBack="false" 
Checked='<%# Convert.ToBoolean(Eval("select").ToString()) %>' />
</ItemTemplate>
<FooterTemplate>
<asp:ImageButton ID="img_btn_Add" runat="server" 
ImageUrl="~/Images/Icon/Gridadd.png" TabIndex="8" OnClick="img_btn_Add_Click"/>
</FooterTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Sr.No.">                        
<ItemTemplate>
<asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
Width="6%" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Title" >
<ItemTemplate>
<asp:TextBox ID="GrtxtTermCondition_Head" runat="server"  MaxLength="400" TextMode="MultiLine" Width="150px"
CssClass="TextBox" Text='<%# Bind("Title") %>' TabIndex="6" ></asp:TextBox>
</ItemTemplate>
<ControlStyle  />
<HeaderStyle HorizontalAlign="Left" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Description"  >
<ItemTemplate>
<asp:TextBox ID="GrtxtDesc" runat="server" TextMode="MultiLine"  Width="300px"
CssClass="TextBox" Text='<%# Bind("TDescptn") %>' TabIndex="6" ></asp:TextBox>
</ItemTemplate>
<ControlStyle />
<HeaderStyle HorizontalAlign="Left" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
</asp:TemplateField>
</Columns>
<PagerStyle CssClass="pgr" />
</asp:GridView> 
</div>
</Content>
</ajax:AccordionPane>
</Panes>
</ajax:Accordion>
</td>
</tr>             
</table>
</fieldset>
 </td>
 </tr>
 <tr>
  <td>
   <fieldset id="fieldset2" width="100%" runat="server" class="FieldSet">
    <table width="100%" align="center">
 <tr>
 <td align="center">
  <table width="25%">
  
     <tr>
    <td>
   <asp:Button ID="BtnUpdate" runat="server" CssClass="button" 
     TabIndex="17" Text="Update" ValidationGroup="Add" 
     onclick="BtnUpdate_Click" />
    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
      ConfirmText="Would You Like To Update the Record ..! " 
      TargetControlID="BtnUpdate">
    </ajax:ConfirmButtonExtender>
    </td>
    <td>
        <asp:Button ID="BtnSave" runat="server" CssClass="button" 
          TabIndex="18" Text="Save" ValidationGroup="Add" onclick="BtnSave_Click" />
    </td>
    <td>
        <asp:Button ID="BtnDelete" runat="server" CssClass="button" 
          Text="Delete" onclick="BtnDelete_Click"  />
        <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
          ConfirmText="Would You Like To Delete the Record ..! " 
          TargetControlID="BtnDelete">
        </ajax:ConfirmButtonExtender>
    </td>
    <td>
        <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" 
         CssClass="button" TabIndex="19" Text="Cancel" onclick="BtnCancel_Click" />


 </td>
</tr>
</table>
 </td>
 </tr>
</table>
     </fieldset>

    </td>
      </tr>
      </table>
     
  </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ReportTitle" Runat="Server">
    Supplier List    
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Report" Runat="Server">                 
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate >
    <div class="ScrollableDiv_FixHeightWidthForRepeater">
    <ul id="subnav">
            <%--Ul Li Problem Solved repeater--%>
            <asp:Repeater ID="GrdReport" runat="server" 
                onitemcommand="GrdReport_ItemCommand">    
            <ItemTemplate>
            <li id="Menuitem" runat="server" >                              
            <asp:LinkButton ID="lbtn_List" CssClass="linkButton" CausesValidation="false"
                CommandName="Select" CommandArgument='<%# Eval("#") %>' runat="server"
                Text='<%# Eval("Name") %>'>
            </asp:LinkButton>
                                 
            </li>
            </ItemTemplate>    
            </asp:Repeater>
            </ul>
         </div>  
    </ContentTemplate>
  </asp:UpdatePanel>                  
</asp:Content>
