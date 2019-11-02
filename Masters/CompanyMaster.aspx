<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="CompanyMaster.aspx.cs" Inherits="Masters_CompanyMaster" Title="Company Master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<input type="hidden" id="hiddenbox" runat="server" value=""/>
<script type="text/javascript" language="javascript">

function DeleteEquipFunction()
 { 
        var gridViewCtlId = '<%=GridDetails.ClientID%>';
        var ctlGridViewItems = null;
        var ItemID;
        ctlGridViewItems = document.getElementById(gridViewCtlId);
        ItemID =  ctlGridViewItems.rows[1].cells[2].innerText;        
        if(ItemID==0)
        {        
        if(confirm("There is no record to delete")==true)
        {
        document.getElementById('<%= hiddenbox.ClientID%>').value="0"; 
        }
        else
        {
         document.getElementById('<%= hiddenbox.ClientID%>').value="0"; 
        }
        }
        else
        {
        if(confirm("Are you sure you want to delete?")==true)
        {
        document.getElementById('<%= hiddenbox.ClientID%>').value="1";
        return true;
        }
        else
        {
         document.getElementById('<%= hiddenbox.ClientID%>').value="0";
         return false;
         }
        }
}
</script>

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

        Search for Company :
        <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
        Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged">
        </asp:TextBox>
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
    Company Master     
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
    <ContentTemplate>
    <table width="100%">
    <tr>
    <td>
    <fieldset id="fieldset1" width="100%" runat="server" class="FieldSet">
    <table width="100%" cellspacing="6">
        <tr>
            <td class="Label">
                &nbsp;</td>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Label">
                Company Name :
            </td>
            <td colspan="3">
                <asp:TextBox ID="TxtCompanyName" runat="server" CssClass="TextBox" 
                    Width="442px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="Req_Name" runat="server" 
                    ControlToValidate="TxtCompanyName" Display="None" 
                    ErrorMessage="Company Name is Required" SetFocusOnError="True" 
                    ValidationGroup="Add">
                </asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" 
                    TargetControlID="Req_Name" WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        
         <tr>
            <td class="Label">
                Abbreviation :
            </td>
            <td colspan="3">
                <asp:TextBox ID="Txtabbreviations" runat="server" CssClass="TextBox" ToolTip="Short Name For Company Which Bind With Transaction Number"
                    MaxLength="50" Width="442px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="Txtabbreviations" Display="None" 
                    ErrorMessage="Abbreviations is Required For Generating Code!" SetFocusOnError="True" 
                    ValidationGroup="Add">
                </asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" 
                    TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        
        
     <tr>
     <td class="Label">
         Address :
     </td>
     <td colspan="3">
      <asp:TextBox ID="TxtAddress" runat="server" CssClass="TextBox" 
       TextMode="MultiLine" Width="442px" Height="20px"></asp:TextBox>
     </td>
     </tr>
     
     <tr>
     <td class="Label">
         Phone No:     
     </td>
     <td colspan="3">
     <asp:TextBox ID="TxtPhoneNo" runat="server" CssClass="TextBox" 
       Width="125px" MaxLength="15"></asp:TextBox>
     <ajax:FilteredTextBoxExtender ID="FTE_Mobile" runat="server" TargetControlID="TxtPhoneNo"
     FilterType="Custom,Numbers" ValidChars="+"></ajax:FilteredTextBoxExtender>
     </td>
     </tr>
     <tr>
     <td class="Label">
         Email ID :
     </td>
     <td colspan="3">
        <asp:TextBox ID="TxtEmail" runat="server" CssClass="TextBox" 
        MaxLength="50" Width="442px"></asp:TextBox>
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
         Website :
     </td>
     <td colspan="3">
      <asp:TextBox ID="TxtWebsite" runat="server" CssClass="TextBox" 
        MaxLength="50" Width="442px"></asp:TextBox>
     </td>
     </tr>
     <tr>
     <td class="Label">
         PAN No :
     </td>
     <td>
     <asp:TextBox ID="TxtFaxNo" runat="server" CssClass="TextBox" 
       Width="125px" MaxLength="15"></asp:TextBox>
      
     </td>
     <td class="Label">
         Tin No :
     </td>
     <td>
     <asp:TextBox ID="TxtTinNo" runat="server" CssClass="TextBox" 
       Width="125px" MaxLength="15"></asp:TextBox>
     </td>
     </tr>
     <tr>
     <td class="Label">
        GSTN No :
     </td>
     <td>
     <asp:TextBox ID="TxtVatNo" runat="server" CssClass="TextBox" 
       Width="125px" MaxLength="15"></asp:TextBox>
     </td>
     <td class="Label">
         Service Tax No :
     </td>
     <td>
     <asp:TextBox ID="TxtServiceTaxNo" runat="server" CssClass="TextBox" 
       Width="125px" MaxLength="15"></asp:TextBox>
     </td>
     </tr>
     <tr>
     <td class="Label">
         Company Logo :
     </td>
      <td colspan="3">
            <table width="100%">
            <tr>
            <td align="left" width="30%">
            <asp:UpdatePanel ID ="updatePanel7" runat ="server" >
            <Triggers ><asp:PostBackTrigger ControlID="lnkCompany" /></Triggers>
            <ContentTemplate >
            <asp:FileUpload ID="CompanyLogoUpload" CssClass ="TextBox" runat="server" BorderStyle="None" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="CompanyLogoUpload"
            Display="None" ErrorMessage="Upload Image File only" SetFocusOnError="True" ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G)|(b|B)(m|M)(p|P))$"	
            ValidationGroup="Add"></asp:RegularExpressionValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RegularExpressionValidator6"
            WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender> 
          
            </ContentTemplate> 
            </asp:UpdatePanel> 
            </td>
            <td align="left">
            <asp:LinkButton ID="lnkCompany" runat="server" ValidationGroup="AddGridComp" onclick="lnkCompany_Click" >Upload</asp:LinkButton> 
            <asp:LinkButton ID="lnkCompanyCancel" runat="server" onclick="lnkCompanyCancel_Click">Cancel</asp:LinkButton>                                                                  
            </td>
            <td>
                <asp:Label ID="lblLogopath" runat="server" Text="Label" Visible="false"></asp:Label>
                <asp:Image ID="ImgCompanyLogo" runat="server" Height="30px" Width="40px" />* For Show In Prints
            </td>
            </tr>
            </table>
     </td>
     </tr>
      
        <tr>
            <td class="Label">
                Digital Signature1 :
            </td>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td align="left" width="30%">
                            <asp:UpdatePanel ID="updatePanel3" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkSign" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:FileUpload ID="DigitalSignUpload" runat="server" CssClass="TextBox"  BorderStyle="None"/>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                        ControlToValidate="DigitalSignUpload" Display="None" 
                                        ErrorMessage="Upload Image File only" SetFocusOnError="True" 
                                        ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G)|(b|B)(m|M)(p|P))$" 
                                        ValidationGroup="Add"></asp:RegularExpressionValidator>
                                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                                        TargetControlID="RegularExpressionValidator1" 
                                        WarningIconImageUrl="~/Images/Icon/Warning.png">
                                    </ajax:ValidatorCalloutExtender>
                               
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left">
                        <asp:LinkButton ID="lnkSign" runat="server" onclick="lnkSign_Click">Upload</asp:LinkButton>
                        <asp:LinkButton ID="lnkCancle1" runat="server" onclick="lnkCancle1_Click">Cancel</asp:LinkButton>
                        </td>
                        <td>
                        <asp:Label ID="LblSignPath" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Image ID="ImgSign" runat="server" Height="30px" Width="40px" />&nbsp;* For Print In P.O.
                        </td>
                    </tr>
                </table>
            </td>
            
        </tr>
        <tr>
            <td class="Label">
                Digital Signature2 :
            </td>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td align="left" width="30%">
                            <asp:UpdatePanel ID="updatePanel4" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkSign1" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:FileUpload ID="DigitalSignUpload2" runat="server" CssClass="TextBox"  BorderStyle="None"/>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                        ControlToValidate="DigitalSignUpload2" Display="None" 
                                        ErrorMessage="Upload Image File only" SetFocusOnError="True" 
                                        ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G)|(b|B)(m|M)(p|P))$" 
                                        ValidationGroup="Add"></asp:RegularExpressionValidator>
                                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
                                        TargetControlID="RegularExpressionValidator2" 
                                        WarningIconImageUrl="~/Images/Icon/Warning.png">
                                    </ajax:ValidatorCalloutExtender>
                                  
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left">
                        <asp:LinkButton ID="lnkSign1" runat="server" onclick="lnkSign1_Click">Upload</asp:LinkButton>
                        <asp:LinkButton ID="lnkCancle2" runat="server" onclick="lnkCancle2_Click">Cancel</asp:LinkButton>
                        </td>
                        <td>&nbsp;
                        
                        <asp:Label ID="LblSignPath1" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Image ID="ImgSign1" runat="server" Height="30px" Width="40px" />&nbsp;* For Pur.  Manager
                        </td>
                    </tr>
                </table>
            </td>
           
        </tr>
        <tr>
            <td class="Label">
                Digital Signature3 :
            </td>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td align="left" width="30%">
                            <asp:UpdatePanel ID="updatePanel5" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkSign2" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:FileUpload ID="DigitalSignUpload3" runat="server" CssClass="TextBox"  BorderStyle="None"/>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                        ControlToValidate="DigitalSignUpload3" Display="None" 
                                        ErrorMessage="Upload Image File only" SetFocusOnError="True" 
                                        ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G)|(b|B)(m|M)(p|P))$" 
                                        ValidationGroup="Add"></asp:RegularExpressionValidator>
                                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" 
                                        TargetControlID="RegularExpressionValidator3" 
                                        WarningIconImageUrl="~/Images/Icon/Warning.png">
                                    </ajax:ValidatorCalloutExtender>
                                   
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left">
                        <asp:LinkButton ID="lnkSign2" runat="server" onclick="lnkSign2_Click">Upload</asp:LinkButton>
                        <asp:LinkButton ID="lnkCancle3" runat="server" onclick="lnkCancle3_Click">Cancel</asp:LinkButton>
                        </td>
                        <td>
                        <asp:Label ID="LblSignPath2" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Image ID="ImgSign2" runat="server" Height="30px" Width="40px" />&nbsp;* For Site Manager
                        </td>
                    </tr>
                </table>
            </td>
           
        </tr>
        <tr>
        <td class="Label">
            Notes:
        </td>
        <td colspan="3">
          <asp:TextBox ID="TxtNoteC" runat="server" CssClass="TextBox" 
          Width="442px" Height="20px" TextMode="MultiLine"></asp:TextBox>
        </td>
        </tr>
         <tr>
         <td colspan="4"></td>
         </tr>
        <tr>
            <td colspan="4">
              <fieldset id="fieldset3"  width="100%" runat="server" class="FieldSet">
               <legend><b>Bank Details</b></legend>
                <table width="100%">
                 <tr>
                 <td class="Label">
                     Bank Name :
                 </td>
                 <td>
                <asp:DropDownList ID="ddlBankName" runat="server" Width="300px" 
                CssClass="ComboBox">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RFV2" runat="server" 
                ControlToValidate="ddlBankName" Display="None" 
                ErrorMessage="Bank Name is Required" SetFocusOnError="True" 
                ValidationGroup="AddGrid" InitialValue="0"></asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="VCE1" runat="server" Enabled="True" 
                TargetControlID="RFV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>
                 </td>
                 </tr>
                 <tr>
                 <td class="Label">
                     Account No : 
                 </td>
                 <td>
                    <asp:TextBox ID="TxtAccntNo" runat="server" CssClass="TextBox"  MaxLength="15"
                    Width="135px"></asp:TextBox>
                      <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TxtAccntNo"
     FilterType="Custom,Numbers" ></ajax:FilteredTextBoxExtender>
                 </td>
                 </tr>
                    <tr>
                        <td class="Label">
                            Note :</td>
                        <td>
                            <asp:TextBox ID="TxtNoteB" runat="server" CssClass="TextBox" Width="300px"></asp:TextBox>
                            <asp:ImageButton ID="ImgAddGrid" runat="server" 
                                ImageUrl="~/Images/Icon/Gridadd.png" onclick="ImgAddGrid_Click" 
                                ToolTip="Add Grid" ValidationGroup="AddGrid" />
                        </td>
                    </tr>
                 
                 <tr>
<td colspan="2">
<asp:UpdatePanel ID="UpdatePanel6" runat="server">
<ContentTemplate>
   <table width="100%" cellspacing="10">
     <tr>
       <td>
         <div class="scrollableDiv">
          <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" 
          BackColor="White" BorderStyle="None" BorderWidth="1px" CssClass="mGrid" 
          Font-Bold="False" ForeColor="Black" GridLines="Horizontal" 
                 onrowcommand="GridDetails_RowCommand" onrowdeleting="GridDetails_RowDeleting">
          <Columns>
              <asp:TemplateField HeaderText="#" Visible="False">
                  <ItemTemplate>
                      <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
                  <ItemTemplate>
                      <asp:ImageButton ID="ImageGridEdit" runat="server" 
                          CommandArgument="<%#((GridViewRow)Container).RowIndex %>" 
                          CommandName="SelectGrid" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                      <asp:ImageButton ID="ImageBtnDelete" runat="server" 
                          CommandArgument='<%#Eval("#") %>' CommandName="Delete" OnClientClick="DeleteEquipFunction();" 
                          ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                  
                  </ItemTemplate>
                  <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  <HeaderStyle Wrap="False" />
                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
              </asp:TemplateField>
              <asp:BoundField DataField="BankId" HeaderText="BankId">
                  <HeaderStyle Wrap="False" CssClass="Display_None"  />
                  <ItemStyle Wrap="False" CssClass="Display_None" />
              </asp:BoundField>
              <asp:BoundField HeaderText="BankName" DataField="BankName">
                  <HeaderStyle Wrap="false" />
                  <ItemStyle Wrap="false" />
              </asp:BoundField>
              <asp:BoundField HeaderText="AccountNo" DataField="AccountNo" >
              </asp:BoundField>
              <asp:BoundField HeaderText="Note" DataField="NoteB">
                  <HeaderStyle Wrap="false" />
                  <ItemStyle Wrap="false" />
              </asp:BoundField>
          </Columns>
      </asp:GridView>
  </div>
  </td>
  </tr>
  </table>
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
    </table>
    </fieldset>
    </td>
    </tr>
    </table>
    </fieldset>
    </td>
    </tr>
    <tr>
    <td>
    <fieldset id="fieldset2" width="100%" runat="server" class="FieldSet">
    
                 <table width="100%">
                 <tr>
                 <td align="center">
                 <table align="center" width="25%">
                 <tr>
                 <td>
                 <asp:Button ID="BtnUpdate" runat="server" CssClass="button"
                 Text="Update" ValidationGroup="Add" onclick="BtnUpdate_Click"/>
                 <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server"
                 ConfirmText="Would You Like To Update The Record ?" TargetControlID="BtnUpdate">
                 </ajax:ConfirmButtonExtender>
                 </td>
                 
                 <td>
                 <asp:Button ID="BtnSave" runat="server" CssClass="button"
                 Text="Save" ValidationGroup="Add" onclick="BtnSave_Click"/>
                 </td>
                
                 <td>
                 <asp:Button ID="BtnDelete" runat="server" CssClass="button"
                 Text="Delete" ValidationGroup="Add" onclick="BtnDelete_Click"/>
                     <ajax:ConfirmButtonExtender ID="ConfirmButtonExteuynder2" runat="server"
                 ConfirmText="Would You Like To Delete The Record ?" TargetControlID="BtnDelete">
                 </ajax:ConfirmButtonExtender>
                 </td>
                 
                 <td>
                 <asp:Button ID="BtnCancel" runat="server" CausesValidation="false"
                 CssClass="button" Text="Cancel" onclick="BtnCancel_Click"/>
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
    Company List  
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

