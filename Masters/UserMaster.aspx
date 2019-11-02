<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="UserMaster.aspx.cs" Inherits="Masters_UserMaster" Title="User Master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<script type="text/javascript" language="javascript">
function getvalue()
{
   var rb1=document.getElementById("<%=RadioIsAdmin.ClientID %>");
     
   var rb2=rb1.getElementsByTagName("input");
   rb2[0].disabled =true;
    rb2[1].checked =true;
    rb2[1].disabled=true;
 var ddlReport=0;//
var Text = ddlReport.options[ddlReport.selectedIndex].text;
var Value = ddlReport.options[ddlReport.selectedIndex].value;
if(Value==4)
{
    rb2[0].disabled =false;
    rb2[0].checked =true;
    document.getElementById("<%=RadioIsAdmin.ClientID %>").onchange();
    rb2[1].disabled=false;
}

}

function ValidateModuleList(source, args) {
    var chkListModules = document.getElementById('<%= ChkSite.ClientID %>');
    var chkListinputs = chkListModules.getElementsByTagName("input");
    for (var i = 0; i < chkListinputs.length; i++) {
        if (chkListinputs[i].checked) {
            args.IsValid = true;
            return;
        }
    }
    args.IsValid = false;
}

function EnableTextBox() {
    var TextBoxEditPO = document.getElementById('<%= TXTPASSEDITPO.ClientID %>');
    var chkEditPO = document.getElementById('<%= CHKYESEDITPO.ClientID %>');
    if (chkEditPO.checked == false) {
        TextBoxEditPO.value = "";
            TextBoxEditPO.disabled = true;
 }
        if (chkEditPO.checked == true) {
            TextBoxEditPO.disabled = false;
 }

}

function EXCESSEnableTextBox() {
    var TextBoxEditPO = document.getElementById('<%= TXTEXCESSREPORT.ClientID %>');
    var chkEditPO = document.getElementById('<%= CHKESCESSREPORT.ClientID %>');
    if (chkEditPO.checked == false) {
        TextBoxEditPO.value = "";
        TextBoxEditPO.disabled = true;
    }
    if (chkEditPO.checked == true) {
        TextBoxEditPO.disabled = false;
    }

}

</script>
<style>
 
    .mycheckbox input[type="checkbox"] 
{ 
    margin-right: 5px; 
}
</style>

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
 
    Search for User : 
      <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" 
        ToolTip="Enter The Text" Width="292px" AutoPostBack="True" 
        ontextchanged="TxtSearch_TextChanged">
      </asp:TextBox>
     <div id="divwidth"></div>
      <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"  
         runat="server" TargetControlID="TxtSearch" 
         CompletionInterval="100"                             
         UseContextKey="True" FirstRowSelected ="true" 
         ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender"
         CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
         </ajax:AutoCompleteExtender> 
                              
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    User Master      
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
   <ContentTemplate>
   <table width="100%" >
   <tr>
   <td style="width: 628px">
    <fieldset id="fieldset1"  width="100%" runat="server" class="FieldSet">
     
        <table width="100%" cellspacing="5">
            <tr>
                <td class="Label">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td style="width: 4px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="Label">
                    User Name :</td>
                <td>
                    <asp:TextBox ID="TxtUserName" runat="server" CssClass="TextBox" MaxLength="50" 
                        Width="250px"></asp:TextBox>
                </td>
                <td style="width: 4px">
                    <asp:RequiredFieldValidator ID="Req1" runat="server" 
                        ControlToValidate="TxtUserName" Display="None" 
                        ErrorMessage="User Name Required" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
                        Enabled="True" TargetControlID="Req1" 
                        WarningIconImageUrl="~/Images/Icon/Warning.png">
                    </ajax:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td class="Label">
                    Mail Id :</td>
                <td>
                    <asp:TextBox ID="TxtMailId" runat="server" CssClass="TextBox" MaxLength="50" 
                        Width="250px"></asp:TextBox>
                    <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" 
                        TargetControlID="TxtMailId" WatermarkText="abc@gmail.com">
                    </ajax:TextBoxWatermarkExtender>
                </td>
                <td style="width: 4px">
                    <asp:RegularExpressionValidator ID="REVmail" runat="server" 
                        ControlToValidate="TxtMailId" Display="None" 
                        ErrorMessage="Enter Valid Email ID..!!" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ValidationGroup="Add"></asp:RegularExpressionValidator>
                  <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" 
                    Enabled="True" TargetControlID="REVmail" 
                    WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td class="Label">
                    User Name For Login :</td>
                <td>
                    <asp:TextBox ID="TxtUserId" runat="server" CssClass="TextBox" MaxLength="50" 
                        Width="250px"></asp:TextBox>
                </td>
                <td style="width: 4px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="Label">
                    Password :</td>
                <td>
                    <asp:TextBox ID="TxtPasswrod" runat="server" CssClass="TextBox" MaxLength="50" 
                        Width="250px" ValidationGroup="Add" TextMode="Password"></asp:TextBox>
                        
                    <ajax:PasswordStrength ID="PasswordStrength1" runat="server" 
                        PreferredPasswordLength="6" TargetControlID="TxtPasswrod">
                    </ajax:PasswordStrength>
                </td>
                <td style="width: 4px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="Label">
                    Confirm Password :</td>
                <td>
                    <asp:TextBox ID="TxtConfrmPasswrd" runat="server" CssClass="TextBox" 
                        MaxLength="50" Width="250px" ValidationGroup="Add" TextMode="Password"></asp:TextBox>
                </td>
                <td style="width: 4px">
                    <asp:CompareValidator ID="CPV1" runat="server" 
                        ErrorMessage="Password Should Match..!" 
                        ControlToCompare="TxtPasswrod" ControlToValidate="TxtConfrmPasswrd" 
                        Display="None" ValidationGroup="Add"></asp:CompareValidator>
                    <ajax:ValidatorCalloutExtender ID="Ajax_RC_Validator1" runat ="server"
                        TargetControlID ="CPV1"  WarningIconImageUrl="~/Images/Icon/Warning.png" >
                    </ajax:ValidatorCalloutExtender></td>
                </td>
            </tr>




       <tr>
                <td class="Label">
                    Mobile No  :</td>
                <td>
                    <asp:TextBox ID="txtmob" runat="server" CssClass="TextBox"  
                        MaxLength="10" Width="250px" ValidationGroup="Add"  ></asp:TextBox>
                     <ajax:FilteredTextBoxExtender ID="FTE_Mobile" runat="server" TargetControlID="txtmob"
                                    FilterType="Custom,Numbers" ValidChars="+"></ajax:FilteredTextBoxExtender>
                </td>
               
            </tr>






            <tr>
                <td class="Label">
                    Is admin :</td>
           
               <td align="left" colspan="2">
               <asp:RadioButtonList ID="RadioIsAdmin" runat="server" CssClass="RadioButton" 
                    RepeatDirection="Horizontal"  AutoPostBack="True" 
                       onselectedindexchanged="RadioIsAdmin_SelectedIndexChanged">
                    <asp:ListItem  Value="T">True</asp:ListItem>
                    <asp:ListItem  Selected="True" Value="F">False</asp:ListItem>
                </asp:RadioButtonList>
            </td>
                <td>
                    </td>
            </tr>
               <tr>
                <td class="Label" style="height: 23px">
                    For Site :</td>
                <td style="height: 23px">
                    <asp:CheckBoxList ID="ChkSite" runat="server" RepeatDirection="Vertical" Font-Bold="true" 
                    CssClass="mycheckbox"></asp:CheckBoxList>
                </td>
                <td style="width: 4px; height: 23px;">
                <asp:CustomValidator runat="server" ID="cvmodulelist" ValidationGroup="Add"
                ClientValidationFunction="ValidateModuleList"
                ErrorMessage="Please Select Atleast one Module" >
                </asp:CustomValidator>
                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                    Enabled="True" TargetControlID="cvmodulelist" 
                    WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>
                </td>
            </tr>
            
      
        </table>
     
    </fieldset>
       
   </td>
   </tr>
   <tr>
   <td  >
<ajax:Accordion ID="Accordion1" runat="server" ContentCssClass="accordionContent1"
HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" FadeTransitions="true"
TransitionDuration="260" FramesPerSecond="20" AutoSize="None"
RequireOpenedPane="false"
SuppressHeaderPostbacks="true"
SelectedIndex="1">
<Panes>
<ajax:AccordionPane ID="AccordionPane1" runat="server">
<Header>
<a class="href" href="#">Set Email</a></Header>
<Content>
<div >              
                                <asp:GridView ID="GridUser" runat="server" AutoGenerateColumns="False" Width="648px" 
                                TabIndex="6"  
                                CssClass="mGrid" GridLines="None" onrowdatabound="GridUser_RowDataBound">                                
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" Visible="false">                                        
                                            <ItemTemplate>
                                                <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="All">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="GrdUserAllHeader" runat="server"                                                  
                                                AutoPostBack="true" 
                                                oncheckedchanged="GrdUserAllHeader_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            
                                            <asp:CheckBox ID="GrdUserAllHeaderAll" runat="server" Checked='<%# Convert.ToBoolean(Eval("Email").ToString()) %>'  />
                                          
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sr. No.">                        
                                            <ItemTemplate>
                                            <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                                            Width="7%" />
                                        </asp:TemplateField>
                                            <asp:BoundField DataField="ModuleId" HeaderText="Module">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" 
                                            Font-Size="Small"  />
                                    </asp:BoundField>
                                        <asp:TemplateField HeaderText="Form" >
                                            <ItemTemplate>
                                                <asp:Label ID="LblFormName" runat="server" Text='<%# Bind("FormName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                       
                                    </Columns> 
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />                               
                                </asp:GridView>
</div>
</Content>
</ajax:AccordionPane>
</Panes>
</ajax:Accordion>
   </td>
   </tr>
   <tr>
   <td>
   <fieldset id="F1" runat="server" class="FieldSet">
                <legend id="Legend1" class="legend" runat="server">&nbsp;&nbsp;Set Permissions&nbsp;&nbsp;</legend>
                
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
<ContentTemplate>
                <table width="100%">    
                    <tr>
                        <td>
                            <asp:Panel ID="PnlUserRight" runat="server"  ScrollBars="Horizontal" Height="300px" 
                            Width="620px">                    
                                <asp:GridView ID="GridUserRight" runat="server" AutoGenerateColumns="False" Width="100%" 
                                TabIndex="6"  
                                CssClass="mGrid" GridLines="None" onrowdatabound="GridUserRight_RowDataBound" >                                
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" Visible="false">                                        
                                            <ItemTemplate>
                                                <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="All">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="GrdSelectAllHeader" runat="server"                                                  
                                                AutoPostBack="true" 
                                                oncheckedchanged="GrdSelectAllHeader_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:CheckBox ID="GrdSelectAll" runat="server" AutoPostBack="true" 
                                            oncheckedchanged="GrdSelectAll_CheckedChanged"/>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sr. No.">                        
                                            <ItemTemplate>
                                            <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                                            Width="7%" />
                                        </asp:TemplateField>
                                            <asp:BoundField DataField="ModuleId" HeaderText="Module">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" 
                                            Font-Size="Small"  />
                                    </asp:BoundField>
                                        <asp:TemplateField HeaderText="Form">
                                            <ItemTemplate>
                                                <asp:Label ID="LblFormName" runat="server" Text='<%# Bind("FormName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Add" >
                                       <HeaderTemplate>
                                                <asp:CheckBox ID="GrdAllAddRight" runat="server" Text="&nbsp;Add"  AutoPostBack="true" 
                                                oncheckedchanged="GrdAllAddRight_CheckedChanged" />
                                       </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="GrdAddRight" runat="server" 
                                                Checked='<%# Convert.ToBoolean(Eval("AddAuth").ToString()) %>' 
                                                   />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                          <HeaderTemplate>
                                                <asp:CheckBox ID="GrdAllViewRight" runat="server" Text="&nbsp;View"  AutoPostBack="true" 
                                                oncheckedchanged="GrdAllViewRight_CheckedChanged" />
                                       </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="GrdViewRight" runat="server" AutoPostBack="True" 
                                                Checked='<%# Convert.ToBoolean(Eval("ViewAuth").ToString()) %>' 
                                                oncheckedchanged="GrdViewRight_CheckedChanged" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" >
                                        <HeaderTemplate>
                                                <asp:CheckBox ID="GrdAllEditRight" runat="server" Text="&nbsp;Edit"  AutoPostBack="true" 
                                                oncheckedchanged="GrdAllEditRight_CheckedChanged" />
                                       </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="GrdEditRight" runat="server" 
                                                Checked='<%# Convert.ToBoolean(Eval("EditAuth").ToString()) %>' 
                                                    />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" >
                                             <HeaderTemplate>
                                                <asp:CheckBox ID="GrdAllDelRight" runat="server" Text="&nbsp;Delete"  AutoPostBack="true" 
                                                oncheckedchanged="GrdAllDelRight_CheckedChanged" />
                                       </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="GrdDelRight" runat="server" 
                                                Checked='<%# Convert.ToBoolean(Eval("DelAuth").ToString()) %>' 
                                                    />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Print" >
                                         <HeaderTemplate>
                                                <asp:CheckBox ID="GrdAllPrintRight" runat="server" Text="&nbsp;Print"  AutoPostBack="true" 
                                                oncheckedchanged="GrdAllPrintRight_CheckedChanged" />
                                       </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="GrdPrintRight" runat="server" 
                                                Checked='<%# Convert.ToBoolean(Eval("PrintAuth").ToString()) %>' 
                                                    />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns> 
                                <PagerStyle CssClass="pgr" />
                                <AlternatingRowStyle CssClass="alt" />                               
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
   </td>
   </tr>
   
   
      <tr>
   <td  >
<ajax:Accordion ID="Accordion2" runat="server" ContentCssClass="accordionContent1"
HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" FadeTransitions="true"
TransitionDuration="260" FramesPerSecond="20" AutoSize="None"
RequireOpenedPane="false"
SuppressHeaderPostbacks="true"
SelectedIndex="1">
<Panes>
<ajax:AccordionPane ID="AccordionPane2" runat="server">
<Header>
<a class="href" href="#">Set Special Permissions</a></Header>
<Content>
<div >              
<table width="100%" >
<tr>
<td align="center">Transcation </td>
<td class="Label">Change Status :</td>
<td class="Label">&nbsp; </td>
<td align="left">
<asp:CheckBox runat="server" ID="CHKYESEDITPO" Text="&nbsp;&nbsp;View/Edit"  />
</td>
<td>
<asp:Label runat="server" ID="LBLPASSWORDHERE" Text="Password Here :" ForeColor="Red"></asp:Label> 
<asp:TextBox runat="server" ID="TXTPASSEDITPO" Width="150px" CssClass="TextBox_SearchWaterMark" TextMode="Password"></asp:TextBox>
  
</td>
</tr>
<tr>
<td align="center">Master </td>
<td class="Label">Unit Conversion In Item Master :</td>
<td class="Label">&nbsp; </td>
<td align="left">
<asp:CheckBox runat="server" ID="CHKYESUNITCONVERSION" Text="&nbsp;&nbsp;Save/Edit"/>
</td>
<td class="Label">
</td>
</tr>

<tr>
<td align="center">Report </td>
<td class="Label">Purchase Order Shortage/Excess Report :</td>
<td class="Label">&nbsp; </td>
<td align="left">
<asp:CheckBox runat="server" ID="CHKESCESSREPORT" Text="&nbsp;&nbsp;View/Edit"  />
</td>
<td>
<asp:Label runat="server" ID="LBLPasshere" Text="Password Here :" ForeColor="Red"></asp:Label> 
<asp:TextBox runat="server" ID="TXTEXCESSREPORT" Width="150px" CssClass="TextBox_SearchWaterMark" TextMode="Password"></asp:TextBox>

</td>
</tr>

</table>
</div>
</Content>
</ajax:AccordionPane>
</Panes>
</ajax:Accordion>
   </td>
   </tr>
  <tr>
  <td style="width: 628px">
  <fieldset id="fieldset2" runat="server" class="FieldSet">
  <table width="100%">
    <tr>
         <td align="center" >
                <table  align="center" width="25%" >
                    <tr>
                        <td>
                  <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="button" TabIndex="17" 
                     ValidationGroup="Add" onclick="BtnUpdate_Click" />
                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                    ConfirmText="Would You Like To Update the Record ..! "
                    TargetControlID="BtnUpdate" >
                  </ajax:ConfirmButtonExtender>
                  
                        </td>
                        
                        <td>
                           <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" TabIndex="18" 
                            ValidationGroup="Add" onclick="BtnSave_Click" />
                        </td>
                        
                        <td>
                          <asp:Button ID="BtnDelete" runat="server" CssClass="button" Text="Delete" 
                         ValidationGroup="Add" onclick="BtnDelete_Click" />
                          <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                    ConfirmText="Would You Like To Delete the Record ..! "
                    TargetControlID="BtnDelete" >
                  </ajax:ConfirmButtonExtender>
                        </td>
                        <td>
                           <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="button" 
                            TabIndex="19" CausesValidation="False" onclick="BtnCancel_Click" />
                        </td>
                        
                        
                    </tr>
                </table>
         </td>
        </tr>
 </table></fieldset>
  </td>
  </tr>
   
   </table>
   
   </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ReportTitle" Runat="Server">
    User List      
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Report" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate >
       <div class="ScrollableDiv_FixHeightWidthForRepeater">
    <ul id="subnav">
            <%--Ul Li Problem Solved repeater--%>
            <asp:Repeater ID="GrdReport" runat="server" onitemcommand="GrdReport_ItemCommand" 
                >    
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

