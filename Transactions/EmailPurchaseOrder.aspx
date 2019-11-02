<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="EmailPurchaseOrder.aspx.cs" Inherits="Transactions_EmailPurchaseOrder" Title="Email Purchase Order" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
    <style type="text/css">
        .water
        {
            width: 310px;
            border: 0;
            background: #FFF url(../Images/MasterPages/input.gif) no-repeat;
            padding: 4px;
            font-weight: bold;
            margin: 0 0 0 3px;
            color: Gray;
        }
    </style>

    
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server">
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
            </asp:UpdateProgress>--%>
            <%-------------------------------------------------------------------------------------------------------------- --%>
            <asp:Button ID="BtnPopMail2" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlInfoMail2" runat="server" CssClass="ModelPopUpPanelBackGroundMail"
                Style="display: none;">
                <table width="100%" class="PopUpHeader">
                    <tr style="background-color: Navy; text-align: center">
                        <td>
                            &nbsp;<asp:Label ID="Label1" Text="Revo MMS - Mail" runat="server" ForeColor="white"
                                Font-Bold="true" Font-Size="12px"></asp:Label></td>
                    </tr>
                    <tr id="TRLOADING" runat="server">
                        <td>
                            <asp:Image Width="100%" Height="15px" runat="server" ID="IMGPROGRESS" ImageUrl="~/Images/New Icon/progressBar.gif" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <table width="50%" style="margin: 5px 0 5px 0;" cellspacing="8">
                                <tr>
                                    <td>
                                        <asp:DropDownList runat="server" ID="DDLKCMPY" CssClass="ComboBox" Width="550px"
                                            OnSelectedIndexChanged="DDLKCMPY_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <ajax:RoundedCornersExtender ID="RCCDDLKCMPY" runat="server" TargetControlID="DDLKCMPY"
                                            Corners="All" Radius="6" BorderColor="Gray">
                                        </ajax:RoundedCornersExtender>
                                        <asp:Label ID="LBLID" runat="server" CssClass="Display_None"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="TXTKTO" CssClass="TextBox" Width="550px" AutoPostBack="true"
                                                    OnTextChanged="TXTKTO_TextChanged"></asp:TextBox>
                                                <ajax:RoundedCornersExtender ID="RCCTXTKTO" runat="server" TargetControlID="TXTKTO"
                                                    Corners="All" Radius="6" BorderColor="Gray">
                                                </ajax:RoundedCornersExtender>
                                                <ajax:TextBoxWatermarkExtender ID="WMTXTKTO" runat="server" TargetControlID="TXTKTO"
                                                    WatermarkText="To" WatermarkCssClass="water" />
                                                <asp:RegularExpressionValidator ID="REV2" runat="server" Display="None" ErrorMessage="Please Enter Valid Email ID..!"
                                                    ControlToValidate="TXTKTO" ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                                                    ValidationGroup="Add">
                                                </asp:RegularExpressionValidator>
                                                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True"
                                                    TargetControlID="REV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                </ajax:ValidatorCalloutExtender>
                                                <asp:RequiredFieldValidator ID="Rq_V2" runat="server" ControlToValidate="TXTKTO"
                                                    CssClass="Error" Display="None" ErrorMessage="Please Enter MailID" ValidationGroup="AddMail"></asp:RequiredFieldValidator>
                                                <ajax:ValidatorCalloutExtender ID="Rq_V2_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="Rq_V2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                </ajax:ValidatorCalloutExtender>
                                                <ajax:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="TXTKTO"
                                                    CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" ShowOnlyCurrentWordInCompletionListItem="true"
                                                    ServiceMethod="GetCompletionListForTo" CompletionListCssClass="AutoExtender"
                                                    CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                </ajax:AutoCompleteExtender>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="TXTKCC" CssClass="TextBox" Width="550px"></asp:TextBox>
                                        <ajax:RoundedCornersExtender ID="RCCTXTKCC" runat="server" TargetControlID="TXTKCC"
                                            Corners="All" Radius="6" BorderColor="Gray">
                                        </ajax:RoundedCornersExtender>
                                        <ajax:TextBoxWatermarkExtender ID="WMTXTKCC" runat="server" TargetControlID="TXTKCC"
                                            WatermarkText="CC" WatermarkCssClass="water" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"
                                            ErrorMessage="Please Enter Valid Email ID For CC" ControlToValidate="TXTKCC"
                                            ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                                            ValidationGroup="Add">
                                        </asp:RegularExpressionValidator>
                                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True"
                                            TargetControlID="RegularExpressionValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                        </ajax:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="TXTKSUBJECT" CssClass="TextBox" Width="550px"></asp:TextBox>
                                        <ajax:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="TXTKSUBJECT"
                                            Corners="All" Radius="6" BorderColor="Gray">
                                        </ajax:RoundedCornersExtender>
                                        <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TXTKSUBJECT"
                                            WatermarkText="SUBJECT" WatermarkCssClass="water" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="TxtBody" CssClass="TextBox" Width="550px" Height="200px"
                                            TextMode="MultiLine"></asp:TextBox>
                                        <ajax:TextBoxWatermarkExtender ID="TXBTxtBody" runat="server" TargetControlID="TxtBody"
                                            WatermarkText="MAIL BODY" WatermarkCssClass="water" />
                                        <ajax:RoundedCornersExtender ID="RCB" runat="server" TargetControlID="TxtBody" Corners="All"
                                            Radius="6" BorderColor="Gray">
                                        </ajax:RoundedCornersExtender>
                                        <%--<ajax:HtmlEditorExtender runat="server" ID="bhtml" TargetControlID="TxtBody"></ajax:HtmlEditorExtender>--%>
                                    </td>
                                </tr>
                                <tr class="Display_None">
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkAttachedFile" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:FileUpload ID="FileUpload2" runat="server" size="50" CssClass="TextBox" BorderStyle="None"
                                                    Font-Names="Candara" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="lnkAttachedFile" runat="server" CssClass="linkButton">Attach</asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox runat="server" ID="CHKATTACHBROUCHER" CssClass="CheckBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox runat="server" ID="CHKATTACHMANUAL" CssClass="Display_None" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="PopUpYesMail" Text="SEND" runat="server" CssClass="button" CommandName="yes"
                                            OnCommand="PopUpYesNoMail_Command" ValidationGroup="AddMail" CausesValidation="true" />
                                        &nbsp; &nbsp;<asp:Button ID="PopUpNoMail" Text="CANCEL" runat="server" CssClass="button"
                                            CommandName="no" OnCommand="PopUpYesNoMail_Command" />
                                    </td>
                                </tr>
                            </table>
                            <table width="80%">
                                <tr>
                                    <td>
                                        <iframe runat="server" id="iframepdf" height="240px" width="800px"></iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajax:ModalPopupExtender ID="MDPopUpYesNoMail2" BackgroundCssClass="modalBackground"
                runat="server" TargetControlID="BtnPopMail2" PopupControlID="pnlInfoMail2" DropShadow="true">
            </ajax:ModalPopupExtender>
            <%---------------------------------------------------------------------------------------------------------------------%>
            Search for Purchase Order :
            <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
                Width="292px" AutoPostBack="True" OnTextChanged="TxtSearch_TextChanged"></asp:TextBox>
            <div id="divwidth">
            </div>
            <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtSearch"
                CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" ShowOnlyCurrentWordInCompletionListItem="true"
                ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
            </ajax:AutoCompleteExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Purchase Order Follow Up
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
      <ContentTemplate>
      <table width="100%" cellpadding="8">
     
         <tr >
           <td align="center" >
         
              <fieldset id="Fieldset2"  class="FieldSet" runat="server">
              <legend id="Legend3" class="legend" runat="server">Material PO</legend>
                     <div id="div1" class="ScrollableDiv_FixHeightWidthAPP">
                     <table width="100%">
                     <tr><td>
                     <table width="100%">
                     <tr>
                     <td class="BoldText">Sort By :
                            <asp:RadioButton ID="rdbSupplier" runat="server" Text=" Supplier" 
                             GroupName="SortGroup" ValidationGroup="SortBtn" />&nbsp;&nbsp;
                         <asp:RadioButton ID="rdbSite" runat="server" Text=" Site" 
                             GroupName="SortGroup" ValidationGroup="SortBtn" />&nbsp;&nbsp;
                         <asp:RadioButton ID="rdbDate" runat="server" Text=" Date" 
                             GroupName="SortGroup" ValidationGroup="SortBtn" /> </td>
                 
                     <%--<td class="BoldText">Today's Generated Indent :&nbsp;&nbsp;&nbsp;<asp:Label ID="LblGen" CssClass="BoldText" runat="server" ></asp:Label></td>
                     
                     <td class="BoldText">Today's Approved Indent :&nbsp;&nbsp;&nbsp;<asp:Label ID="LblApprov" CssClass="BoldText" runat="server"></asp:Label></td>
                     
                     <td class="BoldText">Today's Authorised Indent :&nbsp;&nbsp;&nbsp;<asp:Label ID="LblAutho" CssClass="BoldText" runat="server"></asp:Label></td>--%>
                     
                     </tr>
                     
                     <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnShow" runat="server" Text="Show" onclick="BtnShow_Click" 
                                ValidationGroup="SortBtn" /></td>
                     
                     </tr>
                     
                     
                     
                     <tr><td colspan="3"> <hr /></td></tr>
                     </table>
                     </td></tr>
                         <tr>
                            <td align="center">        
                            <asp:GridView ID="ReportGrid" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    DataKeyNames="#,MailSend,PODate" AllowPaging="True" OnRowCommand="ReportGrid_RowCommand"
                                                    OnPageIndexChanging="GrdReqPO_PageIndexChanging"
                                                    OnRowDataBound="ReportGrid_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#,PODateDirect" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblEstimateId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <%--<asp:ImageButton ID="ImageAccepted" runat="server" Visible="false" CommandArgument='<%# Eval("#") %>'
                                                                    CommandName="Acctepted" ImageUrl="~/Images/New Icon/DoneChanges.png" ToolTip="Order Accepted Can't Edit" />
                                                                <asp:ImageButton ID="ImageGridEdit" runat="server" Visible="true" CommandArgument='<%# Eval("#") %>'
                                                                    CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                                                <asp:ImageButton ID="ImageApprove" runat="server" Visible="false" CommandArgument='<%# Eval("#") %>'
                                                                    CommandName="Approved" ImageUrl="~/Images/New Icon/LockReport.png" ToolTip="Order Approved Can't Delete" />
                                                                <asp:ImageButton ID="ImgBtnDelete" runat="server" CommandArgument='<%# Eval("#") %>'
                                                                    CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                                    TargetControlID="ImgBtnDelete">
                                                                </ajax:ConfirmButtonExtender>--%>
                                                                <asp:ImageButton ID="ImageApproved" runat="server" 
                                                                    ImageUrl="~/Images/New Icon/ImgApprove.jpg" ToolTip="Send Email" OnClick="ImageApproved_Click" />
                                                                    
                                                                    <asp:ImageButton ID="ImageAuthorised" runat="server" Visible="false"
                                                                    ImageUrl="~/Images/New Icon/ImgAuthorised.jpg" ToolTip="Send Email" OnClick="ImageAuthorised_Click" />
                                                                    
                                                                    <asp:ImageButton ID="ImageCancel" runat="server" Visible="false"
                                                                    ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Cancel" OnClick="ImageCancel_Click" />
                                                                    
                                                                <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="PS"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="NOPDF"%>&PrintFlag=<%="NO" %>'
                                                                    target="_blank">
                                                                    <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                                                                        ToolTip="Print Purchase Order" TabIndex="29" />
                                                                </a><a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="PS"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="PDF"%>&PrintFlag=<%="NO" %>'
                                                                    target="_blank">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png" ToolTip="PDF Purchase Order"
                                                                        TabIndex="29" />
                                                                </a>
                                                                <asp:ImageButton ID="ImgEmail" runat="server" CausesValidation="False" CommandArgument='<%# Eval("#") %>'
                                                                    CommandName="Email" Height="16px" ImageUrl="~/Images/Icon/e-mail.png" TabIndex="9"
                                                                    ToolTip="MARK AS PRINT AND MAIL TO SUPPLIER MANUALLY." Visible="false" />
                                                                <asp:ImageButton ID="ImgNMail" runat="server" CommandArgument='<%# Eval("#") %>'
                                                                    CommandName="MailPO" ImageUrl="~/Images/Icon/Email-Blue.jpg" ToolTip="Mail Purchase Order" />
                                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandArgument='<%# Eval("#") %>'
                                                                    CommandName="CANCELAUTHPO" Height="16px" ImageUrl="~/Images/New Icon/Cancel__Black.png"
                                                                    TabIndex="9" ToolTip="Cancel Purchase Order" Visible="false" />
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderStyle Width="20px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Wrap="false" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="SuplierName" HeaderText="Supplier">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Site" HeaderText="Site">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PONo" HeaderText="PO No">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="POAuthorisedTime" HeaderText="PO Authorise Date">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Amount" HeaderText="Amount">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                                        </asp:BoundField>
                                                        
                                                        <asp:BoundField DataField="EmailApprovedTime" HeaderText="PO Sent Date">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        
                                                        <asp:BoundField DataField="POStatus" HeaderText="Status" Visible="false">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="POId" HeaderText="POId">
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SuplierId" HeaderText="SuplierId">
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MailSend" HeaderText="Email Status">
                                                           <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="ApproveEmail" HeaderText="ApproveEmail">
                                                               <HeaderStyle Wrap="False"  CssClass ="Display_None"/>
                                                               <ItemStyle Wrap="False" CssClass ="Display_None" />
                                                            </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                            </td>
                         </tr>  
                         <tr>
            <td align="center" colspan="2" >
                <table align="center" width="25%">
                    <tr>
                        <td>
                        <asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" 
                                ValidationGroup="Add" onclick="BtnSave_Click" />
                        </td>
                        <td>
                        <asp:Button ID="BtnAuthorized" CssClass="button" runat="server" Text="Authorized" 
                            ValidationGroup="Add" Visible="False"  />
                        </td>
                        <td>
                          <asp:Button ID="BtnCancel" CssClass="button" runat="server" Text="Cancel" 
                               ValidationGroup="Add"  CausesValidation="False" onclick="BtnCancel_Click" />
                        </td>
                    </tr>

                </table>
            </td>
         </tr>
                     </table>
                     </div>
              </fieldset>
           
            </td>
         </tr>
         
         
           
         
       </table>
       
      </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

