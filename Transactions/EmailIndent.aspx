<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="EmailIndent.aspx.cs" Inherits="Transactions_EmailIndent" Title="Email Indent" %>

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
            <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" >
        <ProgressTemplate>            
        <div id="progressBackgroundFilter"></div>
        <div id="processMessage">   
        <center><span class="SubTitle">Loading....!!! </span></center>
        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
        </div>
        </ProgressTemplate>
        </asp:UpdateProgress>--%>
            <%--ontextchanged="TxtSearch_TextChanged"--%>
            
              <%-------------------------------------------------------------------------------------------------------------- --%>
            <asp:Button ID="BtnPopMail1" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlInfoMail1" runat="server" CssClass="ModelPopUpPanelBackGroundMail"
                Style="display: none;">
                <table width="100%" class="PopUpHeader">
                    <tr style="background-color: Navy; text-align: center">
                        <td>
                            &nbsp;<asp:Label ID="Label1" Text="Revo MMS - Mail" runat="server" ForeColor="white"
                                Font-Bold="true" Font-Size="12px"></asp:Label>
                        </td>
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
            <ajax:ModalPopupExtender ID="MDPopUpYesNoMail" BackgroundCssClass="modalBackground"
                runat="server" TargetControlID="BtnPopMail1" PopupControlID="pnlInfoMail1" DropShadow="true">
            </ajax:ModalPopupExtender>
            <%---------------------------------------------------------------------------------------------------------------------%>
            Search for Material Indent :
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
Indent Follow Up
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="8">
                <tr>
                    <td align="center">
                        <fieldset id="Fieldset2" class="FieldSet" runat="server">
                            <legend id="Legend3" class="legend" runat="server">Material Indent</legend>
                            <div id="div1" class="ScrollableDiv_FixHeightWidthAPP">
                                <table width="100%">
                                    <tr>
                                        <%--<td>
                                            <table width="100%">
                                                <tr>
                                                 <td>
                 <div>
                                        <asp:Button ID="Btn1" runat="server" BackColor="White" Enabled="false" Width="5%" />
                                        <asp:Label runat="server" ID="lblBG" Text="- Generated" CssClass="Label4"></asp:Label>
                                        <asp:Button ID="Button1" runat="server" BackColor="Yellow" Enabled="false" Width="5%" />
                                        <asp:Label runat="server" ID="Label7" Text="- Approved" CssClass="Label4"></asp:Label>
                                        <asp:Button ID="Button2" runat="server" BackColor="MediumSeaGreen" Enabled="false" Width="5%" />
                                        <asp:Label runat="server" ID="Label8" Text="- Authorised" CssClass="Label4"></asp:Label>
                                        <asp:Button ID="Button3" runat="server" BackColor="PowderBlue" Enabled="false" Width="5%" />
                                        <asp:Label runat="server" ID="Label2" Text="-PO GENERATED" CssClass="Label4"></asp:Label>
                                        <asp:Button ID="Button4" runat="server" BackColor="IndianRed" Enabled="false" Width="5%" />
                                        <asp:Label runat="server" ID="Label3" Text="-Email Sent" CssClass="Label4"></asp:Label>
                                    </div>
                </td>
                                                    <%--<td class="BoldText">Today's Generated Indent :&nbsp;&nbsp;&nbsp;<asp:Label ID="LblGen" CssClass="BoldText" runat="server" ></asp:Label></td>
                     
                     <td class="BoldText">Today's Approved Indent :&nbsp;&nbsp;&nbsp;<asp:Label ID="LblApprov" CssClass="BoldText" runat="server"></asp:Label></td>
                     
                     <td class="BoldText">Today's Authorised Indent :&nbsp;&nbsp;&nbsp;<asp:Label ID="LblAutho" CssClass="BoldText" runat="server"></asp:Label></td>
                     --%>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <hr />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                         </tr> 
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                    <asp:GridView ID="ReportGrid" runat="server" ForeColor="Black" AllowPaging="True" AutoGenerateColumns="False"
                                                        CssClass="mGrid" DataKeyNames="#,POTotQty,IndentTotQty,IsCancel,ReqStatus,POSTATUS,RequisitionDate" OnRowCommand="ReportGrid_RowCommand"
                                                        OnRowDeleting="ReportGrid_RowDeleting" OnPageIndexChanging="ReportGrid_PageIndexChanging"
                                                        OnRowDataBound="ReportGrid_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblEstimateId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                <asp:ImageButton ID="ImageApproved" runat="server" 
                                                                    ImageUrl="~/Images/New Icon/ImgApprove.jpg" ToolTip="Send Email" OnClick="ImageApproved_Click" />
                                                                    
                                                                    <asp:ImageButton ID="ImageAuthorised" runat="server" Visible="false"
                                                                    ImageUrl="~/Images/New Icon/ImgAuthorised.jpg" ToolTip="Send Email" OnClick="ImageAuthorised_Click" />
                                                                    
                                                                    <asp:ImageButton ID="ImageCancel" runat="server" Visible="false"
                                                                    ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Cancel" OnClick="ImageCancel_Click" />
                                                                     
                                                                    
                                                                    
                                                                   <%-- <asp:ImageButton ID="ImageAccepted" runat="server" TabIndex="29" CommandArgument='<%# Eval("#") %>'
                                                                        CommandName="Acctepted" ImageUrl="~/Images/New Icon/DoneChanges.png" ToolTip="Indent Accepted Can't Edit" />
                                                                    <asp:ImageButton ID="ImageApprove" runat="server" TabIndex="29" CommandArgument='<%# Eval("#") %>'
                                                                        CommandName="Approved" ImageUrl="~/Images/New Icon/LockReport.png" ToolTip="Indent Approved Can't Delete" />
                                                                    <asp:ImageButton ID="ImageGridEditBlocked" runat="server" TabIndex="29" CommandArgument='<%# Eval("#") %>'
                                                                        ImageUrl="~/Images/Icon/Restrictl.png" ToolTip="Indent Cancelled" />
                                                                    <asp:ImageButton ID="ImageGridEdit" runat="server" TabIndex="29" CommandArgument='<%# Eval("#") %>'
                                                                        CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                                                    <asp:ImageButton ID="ImgBtnDelete" runat="server" TabIndex="29" CommandArgument='<%# Eval("#") %>'
                                                                        CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                                        TargetControlID="ImgBtnDelete">
                                                                    </ajax:ConfirmButtonExtender>--%>
                                                                    
                                                                    <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RATECOMPIND"%>&SFlag=<%# Eval("ReqStatus")%>&PDFFlag=<%="PDF"%>&Rate=<%="0"%>&REQID=<%# Eval("#")%>&ItemID=<%="0"%>'
                                                                target="_blank">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/New Icon/favicon_C.ico"
                                                                    ToolTip="PDF For Rate Comparison" TabIndex="29"/>
                                                                    </a>
                                                                    
                                                                    <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RS"%>&PDFFlag=<%="NOPDF"%>'
                                                                        target="_blank">
                                                                        <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                                                                            TabIndex="29" ToolTip="Print Indent Register" />
                                                                            
                                                                    </a><a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RS"%>&PDFFlag=<%="PDF"%>'
                                                                        target="_blank">
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png" TabIndex="29"
                                                                            ToolTip="PDF of Indent Register" />
                                                                    </a>
                                                                    <asp:ImageButton ID="ImgNMail" runat="server" CommandArgument='<%# Eval("#") %>'
                                                                        CommandName="MailIndent" ImageUrl="~/Images/Icon/Email-Blue.jpg" ToolTip="Mail Indent" />
                                                                    <%--<asp:ImageButton ID="IMGDELETEMR" runat="server" CommandArgument='<%# Eval("#") %>'
                                                                        CommandName="DeleteMR" ImageUrl="~/Images/New Icon/Cancel__Black.png" ToolTip="Delete" />
                                                                    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                                        TargetControlID="IMGDELETEMR">
                                                                    </ajax:ConfirmButtonExtender>--%>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <HeaderStyle Width="20px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Wrap="false" />
                                                            </asp:TemplateField>
                                                            
                                                            <%--<asp:BoundField DataField="ReqId" HeaderText="ReqId">
                                                                 <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                 Wrap="False" />
                                                                <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                                 Wrap="False" />
                                                             </asp:BoundField>--%>
                                            
                                                            <asp:BoundField DataField="RequisitionNo" HeaderText="Indent No.">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TemplateTitle" HeaderText="Site">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IndentAuthTime" HeaderText="Indent Authrize Date">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="EmailApproveTime" HeaderText="Indent Sent Date">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="EmailAuthorisedTime" HeaderText="Quote Recd Date">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                            </asp:BoundField>
                                                            <%--<asp:BoundField DataField="IsCancel" HeaderText="Cancel">
                                                                <HeaderStyle />
                                                                <ItemStyle />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ReqStatus" HeaderText="Indent Status">
                                                                <HeaderStyle Wrap="False" />
                                                                <ItemStyle Wrap="False" />
                                                                <FooterStyle Wrap="False" CssClass="Display_None" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="P.O. Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="GRDPOSTATUS" runat="server" Text='<%# Eval("POSTATUS") %>' ToolTip='<%# Eval("POINFO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>--%>
                                                            <asp:BoundField DataField="EmailStatus" HeaderText="Email Status">
                                                                <HeaderStyle CssClass ="Display_None"/>
                                                                <ItemStyle CssClass ="Display_None"/>
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="sel" HeaderText="Sel">
                                                               <HeaderStyle Wrap="False"  CssClass ="Display_None"/>
                                                               <ItemStyle Wrap="False" CssClass ="Display_None" />
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="ApproveEmail" HeaderText="ApproveEmail">
                                                               <HeaderStyle Wrap="False"  CssClass ="Display_None"/>
                                                               <ItemStyle Wrap="False" CssClass ="Display_None" />
                                                            </asp:BoundField>
                                                            
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <table align="center" width="25%">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" ValidationGroup="Add"
                                                            OnClick="BtnSave_Click" />
                                                    </td>
                                                    <%--<td>
                                                        <asp:Button ID="BtnAuthorized" CssClass="button" runat="server" Text="Authorized"
                                                            ValidationGroup="Add" Visible="False" />
                                                    </td>--%>
                                                    <td>
                                                        <asp:Button ID="BtnCancel" CssClass="button" runat="server" Text="Cancel" ValidationGroup="Add"
                                                            CausesValidation="False" OnClick="BtnCancel_Click" />
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

