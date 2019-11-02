<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="ClientCompanyMaster.aspx.cs" Inherits="Masters_ClientCompanyMaster" Title="Company Representer (Supplier)" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<style type="text/css"> 
.water {
color:Gray;
}
</style>


    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>            
    <div id="progressBackgroundFilter"></div>
    <div id="processMessage">   
    <center><span class="SubTitle">Loading....!!! </span></center>
    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
    </div>
    </ProgressTemplate>
    </asp:UpdateProgress>

        Search :
        <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text" Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged">
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
    Company Representer (Supplier)
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
    <ContentTemplate>
    
      <asp:Button ID="btnPopHide" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlInfo" runat="server" CssClass="ModelPopUpPanelBackGroundSmall"
                Style="display: none;">
                <table width="100%" class="PopUpHeader">
                    <tr style="background-color: Navy; text-align: center">
                        <td>
                            &nbsp;<asp:Label ID="popUpTitle" Text="Revo MMS - Client Person Entry" runat="server"
                                ForeColor="white" Font-Bold="true" Font-Size="12px"></asp:Label></td>
                    </tr>
                </table>
                <table width="100%" style="margin: 5px 0 5px 0;">
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblPopUpYesNoMessage" runat="server" Font-Size="12px" Text="The Person Already Exists In Below List Do You Want To Replace IT ? "></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                <br />
                <div style="float: right; margin-bottom: 8px;">
                    <asp:Button ID="btnPopUpYes" Text="Yes" runat="server" CssClass="button" CommandName="yes"
                        OnCommand="PopUpYesNo_Command" />
                    &nbsp; &nbsp;<asp:Button ID="btnPopUpNo" Text="No" runat="server" CssClass="button"
                        CommandName="no" OnCommand="PopUpYesNo_Command" />
                    &nbsp; &nbsp;</div>
            </asp:Panel>
            <ajax:ModalPopupExtender ID="PopUpYesNo" BackgroundCssClass="modalBackground" runat="server"
                TargetControlID="btnPopHide" PopupControlID="pnlInfo">
            </ajax:ModalPopupExtender>
    
<%-------------------------------------------------------------------------------------------------------------- --%>
    
     <asp:Button ID="BtnPopMail" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlInfoMail" runat="server" CssClass="ModelPopUpPanelBackGroundMail"
                Style="display: none;">
                <table width="100%" class="PopUpHeader">
                    <tr style="background-color: Navy; text-align: center">
                        <td>
                            &nbsp;<asp:Label ID="Label1" Text="Revo MMS - Mail" runat="server"
                                ForeColor="white" Font-Bold="true" Font-Size="12px"></asp:Label></td>
                    </tr>
                </table>
                <table width="100%"><tr><td align="center">
                <table width="50%" style="margin: 5px 0 5px 0;" cellspacing="8" >
                    <tr>
                       <td >
                       <asp:DropDownList runat="server" ID="DDLKCMPY" CssClass="ComboBox" Width="550px"
                        onselectedindexchanged="DDLKCMPY_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                       <ajax:RoundedCornersExtender ID="RCCDDLKCMPY" runat="server" TargetControlID="DDLKCMPY" Corners="All" Radius="6" BorderColor="Gray"></ajax:RoundedCornersExtender>
                       <asp:Label ID="LBLID" runat="server" CssClass="Display_None"></asp:Label>
                       <asp:Label ID="LBLDESCWITH" runat="server" CssClass="Display_None"></asp:Label>
                       </td>
                    </tr>
                    <tr>
                       <td >
                       <asp:TextBox runat="server" ID="TXTKTO" CssClass="TextBox" Width="550px"></asp:TextBox>
                       <ajax:RoundedCornersExtender ID="RCCTXTKTO" runat="server" TargetControlID="TXTKTO" Corners="All" Radius="6" BorderColor="Gray"></ajax:RoundedCornersExtender>
                        <ajax:TextBoxWatermarkExtender ID="WMTXTKTO" runat="server" TargetControlID="TXTKTO" WatermarkText="To"
                             WatermarkCssClass="water" />
                             
                        <asp:RegularExpressionValidator ID="REV2" runat="server" Display="None" 
                        ErrorMessage="Please Enter Valid Email ID..!" ControlToValidate="TXTKTO" 
                        ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*" ValidationGroup="Add">
                        </asp:RegularExpressionValidator>
                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" 
                        Enabled="True" TargetControlID="REV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                        </ajax:ValidatorCalloutExtender>  
      
                        <asp:RequiredFieldValidator ID="Rq_V2" runat="server" 
                        ControlToValidate="TXTKTO" CssClass="Error" Display="None" 
                        ErrorMessage="Please Enter MailID" ValidationGroup="AddMail"></asp:RequiredFieldValidator>
                        <ajax:ValidatorCalloutExtender ID="Rq_V2_ValidatorCalloutExtender" 
                        runat="server" TargetControlID="Rq_V2" 
                        WarningIconImageUrl="~/Images/Icon/Warning.png">
                        </ajax:ValidatorCalloutExtender>
                       </td>
                    </tr>
                    <tr>
                       <td >
                       <asp:TextBox runat="server" ID="TXTKCC" CssClass="TextBox" Width="550px"></asp:TextBox>
                       <ajax:RoundedCornersExtender ID="RCCTXTKCC" runat="server" TargetControlID="TXTKCC" Corners="All" Radius="6" BorderColor="Gray"></ajax:RoundedCornersExtender>
                       <ajax:TextBoxWatermarkExtender ID="WMTXTKCC" runat="server" TargetControlID="TXTKCC" WatermarkText="CC"
                             WatermarkCssClass="water" />
                             
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None" 
                        ErrorMessage="Please Enter Valid Email ID For CC" ControlToValidate="TXTKCC" 
                        ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*" ValidationGroup="Add">
                        </asp:RegularExpressionValidator>
                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" 
                        Enabled="True" TargetControlID="RegularExpressionValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                        </ajax:ValidatorCalloutExtender>  
                        
                       </td>
                    </tr>
                         <tr>
                       <td >
                       <asp:TextBox runat="server" ID="TXTKSUBJECT" CssClass="TextBox" Width="550px"></asp:TextBox>
                       <ajax:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="TXTKSUBJECT" Corners="All" Radius="6" BorderColor="Gray"></ajax:RoundedCornersExtender>
                       <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TXTKSUBJECT" WatermarkText="SUBJECT"
                             WatermarkCssClass="water" />
                             
                        
                       </td>
                    </tr>
                    <tr >
                       <td>
                       <asp:TextBox runat="server" ID="TxtBody" CssClass="TextBox" Width="550px" Height="200px" TextMode="MultiLine"></asp:TextBox>
                         <ajax:TextBoxWatermarkExtender ID="TXBTxtBody" runat="server" TargetControlID="TxtBody" WatermarkText="MAIL BODY"
                             WatermarkCssClass="water" />
                            <ajax:RoundedCornersExtender ID="RCB" runat="server" TargetControlID="TxtBody" Corners="All" Radius="6" BorderColor="Gray"></ajax:RoundedCornersExtender>
                            <%--<ajax:HtmlEditorExtender runat="server" ID="bhtml" TargetControlID="TxtBody"></ajax:HtmlEditorExtender>--%>
                       </td>
                    </tr>
                     <tr class="Display_None">
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server"> 
                        <Triggers ><asp:PostBackTrigger ControlID ="lnkAttachedFile" /></Triggers>
                        <ContentTemplate>
                        <asp:FileUpload ID="FileUpload2" runat="server" size="50" CssClass="TextBox" 
                        BorderStyle="None" Font-Names="Candara" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkAttachedFile" runat="server" CssClass="linkButton" >Attach</asp:LinkButton>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    </tr>
                    <tr>
                    <td>
                    <asp:CheckBox  runat="server" ID="CHKATTACHBROUCHER" CssClass="CheckBox"/>
                    </td>
                    </tr>
                    <tr>
                    <td>
                    <asp:CheckBox  runat="server" ID="CHKATTACHMANUAL" CssClass="Display_None"/>
                    </td>
                    </tr>
                   <tr>
                   <td align="center">
                    <asp:Button ID="PopUpYesMail" Text="SEND" runat="server" CssClass="button" CommandName="yes"
                        OnCommand="PopUpYesNoMail_Command" ValidationGroup="AddMail" CausesValidation="true"/>
                    &nbsp; &nbsp;<asp:Button ID="PopUpNoMail" Text="CANCEL" runat="server" CssClass="button"
                        CommandName="no" OnCommand="PopUpYesNoMail_Command" />
                   </td>
                   </tr>
                
                </table>
                <table width="80%"><tr><td>
                  <IFrame runat="server" id="iframepdf" height="260px" width="800px" ></IFrame>
                </td></tr></table>
             </td></tr></table>
                   
                    
            </asp:Panel>
            <ajax:ModalPopupExtender ID="MDPopUpYesNoMail" BackgroundCssClass="modalBackground" runat="server"
                TargetControlID="BtnPopMail" PopupControlID="pnlInfoMail" DropShadow="true"  >
            </ajax:ModalPopupExtender>
            
<%---------------------------------------------------------------------------------------------------------------------%>
    
    <table width="100%">
    <tr>
    <td>
        <fieldset id="fieldset1" width="100%" runat="server" class="FieldSet">
        <table width="100%" cellspacing="6">
           
            <tr>
                <td class="Label">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Company Name :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TxtCompanyName" runat="server" CssClass="TextBox" 
                        Width="442px"></asp:TextBox><b class="req_star">*</b>
                    <asp:RequiredFieldValidator ID="Req_CompanyName" runat="server" 
                        ControlToValidate="TxtCompanyName" Display="None" 
                        ErrorMessage="Company Name is Required" SetFocusOnError="True" 
                        ValidationGroup="AddGrid">
                    </asp:RequiredFieldValidator>
                    <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" 
                        TargetControlID="Req_CompanyName" WarningIconImageUrl="~/Images/Icon/Warning.png">
                    </ajax:ValidatorCalloutExtender>
                </td>
            </tr>
            
             <tr>
                <td class="Label">
                    Supplier For :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TxtSupplierFor" runat="server" CssClass="TextBox" 
                        MaxLength="50" Width="442px"></asp:TextBox>
                </td>
            </tr>
            
            
         <tr>
         <td class="Label">
             Address :
         </td>
         <td colspan="3">
          <asp:TextBox ID="TxtAddress" runat="server" CssClass="TextBox" 
           TextMode="MultiLine" Width="442px" Height="30px" ></asp:TextBox>
           <asp:RegularExpressionValidator ID="REV_Address" runat="server" ErrorMessage="Maximum of 2000 characters allowed"
                ControlToValidate="TxtAddress" Display="Dynamic" ValidationExpression=".{0,2000}"
                ValidationGroup="Add" />
          <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" 
                TargetControlID="REV_Address" WarningIconImageUrl="~/Images/Icon/Warning.png">
          </ajax:ValidatorCalloutExtender> 
         </td>
         </tr>
         <tr>
          <td class="Label">
                 Website :
          </td>
          <td colspan="3">
           <asp:TextBox ID="TxtWebsite" runat="server" CssClass="TextBox" MaxLength="50" Width="442px">
           </asp:TextBox>           
              <asp:RegularExpressionValidator ID="REV1_Web" runat="server" ControlToValidate="TxtWebsite"
               SetFocusOnError="True" ValidationGroup="AddGrid" ErrorMessage="Invalid Web Url" 
               ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*@)?" >
                    </asp:RegularExpressionValidator>                      
              <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" 
                        TargetControlID="REV1_Web" WarningIconImageUrl="~/Images/Icon/Warning.png">
                    </ajax:ValidatorCalloutExtender>                         
           </td>
           </tr>  
           <tr>
         <td class="Label">
             Remark :
         </td>
         <td colspan="3">
          <asp:TextBox ID="TxtRemark" runat="server" CssClass="TextBox" 
           TextMode="MultiLine" Width="442px" Height="30px" MaxLength="2000"></asp:TextBox>
          <asp:RegularExpressionValidator ID="REV_Remark" runat="server" ErrorMessage="Maximum of 2000 characters allowed"
                ControlToValidate="TxtRemark" Display="Dynamic" ValidationExpression=".{0,2000}"
                ValidationGroup="Add" />
          <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" 
                TargetControlID="REV_Remark" WarningIconImageUrl="~/Images/Icon/Warning.png">
          </ajax:ValidatorCalloutExtender>
         </td>
         </tr>          
         </table>
        </fieldset>
        
          
              <fieldset id="fieldset2"  width="100%" runat="server" class="FieldSet">
              <legend><b>Person Details</b></legend>
               <table width="100%" cellspacing="6">
                <tr>
                <td class="Label">
                      Name :
                </td>
                
                 <td>
                     <asp:TextBox ID="TxtPersonName" runat="server" CssClass="TextBox" 
                        Width="442px"></asp:TextBox><b class="req_star">*</b>
                    <asp:RequiredFieldValidator ID="Req_PersonName" runat="server" 
                        ControlToValidate="TxtPersonName" Display="None" 
                        ErrorMessage="Person Name is Required" SetFocusOnError="True" 
                        ValidationGroup="Add">
                    </asp:RequiredFieldValidator>
                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" 
                        TargetControlID="Req_PersonName" WarningIconImageUrl="~/Images/Icon/Warning.png">
                    </ajax:ValidatorCalloutExtender>
                     
                 </td>
                </tr>
                <tr>
                  <td class="Label">
                    Designation :
                  </td>
                  <td>
                    <asp:TextBox ID="TxtDesignation" runat="server" CssClass="TextBox" 
                        Width="442px"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                   <td class="Label">
                    Contact No : 
                   </td>
                   <td>
                    <asp:TextBox ID="TxtContactNo1" runat="server" CssClass="TextBox" 
                        Width="442px" MaxLength="15"></asp:TextBox><b class="req_star">*</b>
                    <asp:RequiredFieldValidator ID="Req_ContactNo1" runat="server" 
                        ControlToValidate="TxtContactNo1" Display="None" 
                        ErrorMessage="Conatct No is Required" SetFocusOnError="True" 
                        ValidationGroup="Add">
                    </asp:RequiredFieldValidator>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TxtContactNo1"
                           FilterType="Custom,Numbers" ValidChars="+-">
                    </ajax:FilteredTextBoxExtender>
                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" 
                        TargetControlID="Req_ContactNo1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                    </ajax:ValidatorCalloutExtender>
                   </td>
                </tr>
                  <tr>
                   <td class="Label">
                   Alternate Contact No : 
                   </td>
                   <td>
                    <asp:TextBox ID="TxtContactNo2" runat="server" CssClass="TextBox" 
                        Width="442px" MaxLength="15"></asp:TextBox>                   
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TxtContactNo2"
                           FilterType="Custom,Numbers" ValidChars="+-">
                    </ajax:FilteredTextBoxExtender>
                   </td>
                </tr>
                <tr>
                 <td class="Label">
                    Mail ID :
                 </td>
                 <td>
                    <asp:TextBox ID="TxtEMailId1" runat="server" CssClass="TextBox" 
                        Width="442px"></asp:TextBox><b class="req_star">*</b>  
                    <asp:RequiredFieldValidator ID="Req_EmailId" runat="server" 
                        ControlToValidate="TxtEMailId1" Display="None" 
                        ErrorMessage="EmailId is Required" SetFocusOnError="True" 
                        ValidationGroup="Add">
                    </asp:RequiredFieldValidator>
                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" 
                        TargetControlID="Req_EmailId" WarningIconImageUrl="~/Images/Icon/Warning.png">
                    </ajax:ValidatorCalloutExtender>
                     <asp:RegularExpressionValidator ID="REV2_EmailId1" runat="server" 
                     ErrorMessage="Invalid EMail ID" SetFocusOnError="True" 
                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TxtEmailId1" ValidationGroup="Add">
                     </asp:RegularExpressionValidator>                     
                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" 
                        TargetControlID="REV2_EmailId1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                    </ajax:ValidatorCalloutExtender>
                 </td>
                </tr>
                <tr>
                 <td class="Label">
                   Alternate Mail ID :
                 </td>
                 <td>
                    <asp:TextBox ID="TxtEmailId2" runat="server" CssClass="TextBox" 
                        Width="442px"></asp:TextBox>  
                     <asp:RegularExpressionValidator ID="REV3_EmilId2" runat="server" 
                     ErrorMessage="Invalid EMail ID" SetFocusOnError="True" 
                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TxtEmailId2" ValidationGroup="Add">
                     </asp:RegularExpressionValidator>                     
                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" 
                        TargetControlID="REV3_EmilId2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                    </ajax:ValidatorCalloutExtender>
                    
                 </td>
                </tr>
                <tr>
                 <td class="Label">
                     Remark :
                 </td>
                 <td colspan="3">
                  <asp:TextBox ID="TxtNote" runat="server" CssClass="TextBox" 
                   TextMode="MultiLine" Width="442px" Height="30px" ></asp:TextBox>
                   <asp:RegularExpressionValidator ID="REV_Note" runat="server" ErrorMessage="Maximum of 500 characters allowed"
                      ControlToValidate="TxtNote" Display="Dynamic" ValidationExpression=".{0,2000}"
                      ValidationGroup="Add" />
                   <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" 
                          TargetControlID="REV_Note" WarningIconImageUrl="~/Images/Icon/Warning.png">
                   </ajax:ValidatorCalloutExtender>  
                   <asp:ImageButton ID="ImgAddGrid" runat="server" 
                                ImageUrl="~/Images/Icon/Gridadd.png" onclick="ImgAddGrid_Click" 
                                ToolTip="Add Grid" ValidationGroup="Add" />
                 </td>
                 </tr>
                 <tr>
                    <td colspan="2">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                          <ContentTemplate>
                                <table width="100%" cellspacing="10">
                                 <tr>
                                   <td>
                                    <div class="ScrollableDiv_FixHeightWidth1">
                                    <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" 
                                      BackColor="White" BorderStyle="None" BorderWidth="1px" CssClass="mGrid" 
                                      Font-Bold="False" ForeColor="Black" GridLines="Horizontal" 
                                      OnRowCommand="GridDetails_RowCommand" OnRowDeleting="GridDetails_RowDeleting"> 
                                            
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
                                          <asp:BoundField HeaderText="Name" DataField="PersonName">
                                            <ItemStyle Wrap="false"/>
                                            <HeaderStyle Wrap="false"/>
                                         </asp:BoundField>
                                            
                                          <asp:BoundField HeaderText="Designation" DataField="Designation">
                                          <ItemStyle Wrap="false"/>
                                            <HeaderStyle Wrap="false"/>
                                         </asp:BoundField>
                                          <asp:BoundField HeaderText="Contact No." DataField="ContactNo1">
                                          <ItemStyle Wrap="false"/>
                                            <HeaderStyle Wrap="false"/>
                                         </asp:BoundField>
                                          <asp:BoundField HeaderText="Alternate Contact No." DataField="ContactNo2">
                                          <ItemStyle Wrap="false"/>
                                            <HeaderStyle Wrap="false"/>
                                         </asp:BoundField>
                                          <asp:BoundField HeaderText="Mail Id" DataField="EmailId1">
                                          <ItemStyle Wrap="false"/>
                                            <HeaderStyle Wrap="false"/>
                                         </asp:BoundField>
                                          <asp:BoundField HeaderText="Alternate Mail Id" DataField="EmailId2">
                                           <ItemStyle Wrap="false"/>
                                            <HeaderStyle Wrap="false"/>
                                         </asp:BoundField>
                                          <asp:BoundField HeaderText="Remark" DataField="Note">
                                                <ItemStyle Wrap="false"/>
                                            <HeaderStyle Wrap="false"/>
                                         </asp:BoundField>
                                         <%--<asp:TemplateField HeaderText="Note">
                                           <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Note") %>' Width="100px"></asp:Label>
                                           </ItemTemplate>
                                         </asp:TemplateField>--%>
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
                
               <fieldset id="fieldset3" width="100%" runat="server" class="FieldSet">
                
                 <table width="100%">
                 <tr>
                 <td align="center">
                 <table align="center" width="25%">
                 <tr>
                 <td>
                 <asp:Button ID="BtnUpdate" runat="server" CssClass="button"
                 Text="Update" ValidationGroup="AddGrid" OnClick="BtnUpdate_Click"/>
                 <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server"
                 ConfirmText="Would You Like To Update The Record ?" TargetControlID="BtnUpdate">
                 </ajax:ConfirmButtonExtender>
                 </td>
                 
                 <td>        
                 <asp:Button ID="BtnSave" runat="server" CssClass="button"
                 Text="Save" ValidationGroup="AddGrid" OnClick="BtnSave_Click"/>
                 </td>
                
                 <td>
                 <asp:Button ID="BtnDelete" runat="server" CssClass="button"
                 Text="Delete" OnClick="BtnDelete_Click"/>
                     <ajax:ConfirmButtonExtender ID="ConfirmButtonExteuynder2" runat="server"
                 ConfirmText="Would You Like To Delete The Record ?" TargetControlID="BtnDelete">
                 </ajax:ConfirmButtonExtender>
                 </td>
                 
                 <td>
                 <asp:Button ID="BtnCancel" runat="server" CausesValidation="false"
                 CssClass="button" Text="Cancel" OnClick="BtnCancel_Click"/>
                 </td>
                  <td>
                 <asp:Button ID="BtnMail" runat="server" CausesValidation="false"
                 CssClass="button" Text="Mail" OnClick="BtnMail_Click"/>
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
    List
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Report" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="ScrollableDiv_FixHeightWidthForRepeater">
                <ul id="subnav">
                    <%--Ul Li Problem Solved repeater--%>
                    <asp:Repeater ID="GrdReport" runat="server" OnItemCommand="GrdReport_ItemCommand">
                        <ItemTemplate>
                            <li id="Menuitem" runat="server">
                            <table><tr><td>
                                <asp:LinkButton ID="lbtn_List" CssClass="linkButton" CausesValidation="false" CommandName="Select"
                                    CommandArgument='<%# Eval("#") %>' runat="server" Text='<%# Eval("Name") %>'>
                                </asp:LinkButton>
                                </td>
                                <td>  <asp:ImageButton ID="ImgMail" runat="server" ImageUrl="~/Images/Icon/e-mail.png" 
                                CommandArgument='<%# Eval("#") %>' CommandName="MAIL"
                               ToolTip="Mail Contact Details"/>
                               
                               <asp:ImageButton ID="ImgMailDtls" runat="server" ImageUrl="~/Images/Icon/e-mail.png" 
                                CommandArgument='<%# Eval("#") %>' CommandName="MAILDetails"
                               ToolTip="Mail Contact Details With Remark"/>
                               
                               </td>
                                </tr></table>
                                
                           </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="rptPages" runat="server" >
                        <HeaderTemplate>
                            <asp:LinkButton ID="btnPrev" runat="server" Text="Prev" CommandName="Prev"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnPage" ForeColor="Red" CssClass="RepeaterPagging" CommandName="Page"
                                CommandArgument="<%# Container.DataItem %>" runat="server"><%# Container.DataItem %>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="btnNext" runat="server" Text="Next" CommandName="Next"></asp:LinkButton>
                        </FooterTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

