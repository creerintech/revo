<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="WorkOutJobOrder.aspx.cs" Inherits="Transactions_WorkOutJobOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
    <asp:HiddenField ID="SetFlagHidden" runat="server" Value="0" />

    <script language="javascript" type="text/javascript">

      


       
       


        
        
        function CalPercentage_Amount(TxtBoxId) {

            var _TxtNETTotal = document.getElementById('<%= txtNetTotal.ClientID %>');
            var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');
            var _TxtDiscount = document.getElementById('<%= txtDiscount.ClientID %>');
            var _TxtVat = document.getElementById('<%= txtVATAmount.ClientID %>');
            
            var _txtDekhrekhAmt = document.getElementById('<%= txtDekhrekhAmt.ClientID %>');
            
            var _txtHamaliAmt = document.getElementById('<%= txtHamaliAmt.ClientID %>');
            
            var _txtCESSAmt = document.getElementById('<%= txtCESSAmt.ClientID %>');
            var _txtFreightAmt = document.getElementById('<%= txtFreightAmt.ClientID %>');
            var _txtPackingAmt = document.getElementById('<%= txtPackingAmt.ClientID %>');
            var _txtPostageAmt = document.getElementById('<%= txtPostageAmt.ClientID %>');
            var _txtOtherCharges = document.getElementById('<%= txtOtherCharges.ClientID %>');
            var _txtexciseduty = document.getElementById('<%= txtexciseduty.ClientID %>');
            var _txtInstallationCharge = document.getElementById('<%= txtInstallationCharge.ClientID %>');
            var _txtInstallationServiceAmount = document.getElementById('<%= txtInstallationServiceAmount.ClientID %>');
            
             var _txtCGSTamt = document.getElementById('<%= txtCGSTamt.ClientID %>');
            var _txtSGSTAmt = document.getElementById('<%= txtSGSTAmt.ClientID %>');
            var _txtIGSTAmt = document.getElementById('<%= txtIGSTAmt.ClientID %>');
            
            
            var _TxtGrandTotal = document.getElementById('<%= txtGrandTotal.ClientID %>');
            var t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0;


                if (_txtCGSTamt.value == "" || isNaN(_txtCGSTamt.value)) {
                _txtCGSTamt.value = 0;
                }


                if (_txtSGSTAmt.value == "" || isNaN(_txtSGSTAmt.value)) {
                _txtSGSTAmt.value = 0;
                }

                if (_txtIGSTAmt.value == "" || isNaN(_txtIGSTAmt.value)) {
                _txtIGSTAmt.value = 0;
                }


            if (_txtInstallationServiceAmount.value == "" || isNaN(_txtInstallationServiceAmount.value)) {
                _txtInstallationServiceAmount.value = 0;
            }

            if (_txtInstallationCharge.value == "" || isNaN(_txtInstallationCharge.value)) {
                _txtInstallationCharge.value = 0;
            }

            if (_txtexciseduty.value == "" || isNaN(_txtexciseduty.value)) {
                _txtexciseduty.value = 0;
            }
            if (_TxtNETTotal.value == "" || isNaN(_TxtNETTotal.value)) {
                _TxtNETTotal.value = 0;
            }
            if (_TxtSubTotal.value == "" || isNaN(_TxtSubTotal.value)) {
                _TxtSubTotal.value = 0;
            }
            if (_TxtDiscount.value == "" || isNaN(_TxtDiscount.value)) {
                _TxtDiscount.value = 0;
            }
            if (_TxtVat.value == "" || isNaN(_TxtVat.value)) {
                _TxtVat.value = 0;
            }
            if (_txtDekhrekhAmt.value == "" || isNaN(_txtDekhrekhAmt.value)) {
                _txtDekhrekhAmt.value = 0;
            }
            if (_txtHamaliAmt.value == "" || isNaN(_txtHamaliAmt.value)) {
                _txtHamaliAmt.value = 0;
            }
            if (_txtCESSAmt.value == "" || isNaN(_txtCESSAmt.value)) {
                _txtCESSAmt.value = 0;
            }
            if (_txtFreightAmt.value == "" || isNaN(_txtFreightAmt.value)) {
                _txtFreightAmt.value = 0;
            }
            if (_txtPackingAmt.value == "" || isNaN(_txtPackingAmt.value)) {
                _txtPackingAmt.value = 0;
            }
            if (_txtPostageAmt.value == "" || isNaN(_txtPostageAmt.value)) {
                _txtPostageAmt.value = 0;
            }
            if (_txtOtherCharges.value == "" || isNaN(_txtOtherCharges.value)) {
                _txtOtherCharges.value = 0;
            }
            if (_TxtGrandTotal.value == "" || isNaN(_TxtGrandTotal.value)) {
                _TxtGrandTotal.value = 0;
            }

            _TxtGrandTotal.value = parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_txtInstallationServiceAmount.value) + parseFloat(_TxtVat.value) + parseFloat(_txtDekhrekhAmt.value) + parseFloat(_txtHamaliAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtCESSAmt.value) + parseFloat(_txtFreightAmt.value) + parseFloat(_txtPackingAmt.value) + parseFloat(_txtPostageAmt.value) + parseFloat(_txtInstallationCharge.value)+ parseFloat(_txtCGSTamt.value)+ parseFloat(_txtSGSTAmt.value)+ parseFloat(_txtIGSTAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2);
            _TxtNETTotal.value = parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_txtInstallationServiceAmount.value) + parseFloat(_TxtVat.value) + parseFloat(_txtDekhrekhAmt.value) + parseFloat(_txtHamaliAmt.value) + parseFloat(_txtCESSAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtFreightAmt.value) + parseFloat(_txtPackingAmt.value) + parseFloat(_txtPostageAmt.value) + parseFloat(_txtInstallationCharge.value)+ parseFloat(_txtCGSTamt.value)+ parseFloat(_txtSGSTAmt.value)+ parseFloat(_txtIGSTAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2);
        }



        function CalAsPerDDl() {
            var _TxtNETTotal = document.getElementById('<%= txtNetTotal.ClientID %>');
            var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');
            var _TxtDiscount = document.getElementById('<%= txtDiscount.ClientID %>');
            var _TxtVat = document.getElementById('<%= txtVATAmount.ClientID %>');
            var _txtDekhrekhAmt = document.getElementById('<%= txtDekhrekhAmt.ClientID %>');
            var _txtHamaliAmt = document.getElementById('<%= txtHamaliAmt.ClientID %>');
            var _txtCESSAmt = document.getElementById('<%= txtCESSAmt.ClientID %>');
            var _txtFreightAmt = document.getElementById('<%= txtFreightAmt.ClientID %>');
            var _txtPackingAmt = document.getElementById('<%= txtPackingAmt.ClientID %>');
            var _txtPostageAmt = document.getElementById('<%= txtPostageAmt.ClientID %>');
            var _txtOtherCharges = document.getElementById('<%= txtOtherCharges.ClientID %>');
            var _txtInstallationCharge = document.getElementById('<%= txtInstallationCharge.ClientID %>');
            var _txtInstallationServiceAmount = document.getElementById('<%= txtInstallationServiceAmount.ClientID %>');
            var _TxtGrandTotal = document.getElementById('<%= txtGrandTotal.ClientID %>');
            var _TxtSerTax = document.getElementById('<%= txtSerTax.ClientID %>');
            var _txtexciseduty = document.getElementById('<%= txtexciseduty.ClientID %>');
            var DDL_SERVICETAX = document.getElementById("<%=DDLSERVICETAX.ClientID %>");
            var ddlValue = DDL_SERVICETAX.options[DDL_SERVICETAX.selectedIndex].text;


            var t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0;

            if (_txtInstallationServiceAmount.value == "" || isNaN(_txtInstallationServiceAmount.value)) {
                _txtInstallationServiceAmount.value = 0;
            }

            if (_txtInstallationCharge.value == "" || isNaN(_txtInstallationCharge.value)) {
                _txtInstallationCharge.value = 0;
            }
            if (_txtexciseduty.value == "" || isNaN(_txtexciseduty.value)) {
                _txtexciseduty.value = 0;
            }
            if (_TxtNETTotal.value == "" || isNaN(_TxtNETTotal.value)) {
                _TxtNETTotal.value = 0;
            }
            if (_TxtSubTotal.value == "" || isNaN(_TxtSubTotal.value)) {
                _TxtSubTotal.value = 0;
            }
            if (_TxtDiscount.value == "" || isNaN(_TxtDiscount.value)) {
                _TxtDiscount.value = 0;
            }
            if (_TxtVat.value == "" || isNaN(_TxtVat.value)) {
                _TxtVat.value = 0;
            }
            if (_txtDekhrekhAmt.value == "" || isNaN(_txtDekhrekhAmt.value)) {
                _txtDekhrekhAmt.value = 0;
            }
            if (_txtHamaliAmt.value == "" || isNaN(_txtHamaliAmt.value)) {
                _txtHamaliAmt.value = 0;
            }
            if (_txtCESSAmt.value == "" || isNaN(_txtCESSAmt.value)) {
                _txtCESSAmt.value = 0;
            }
            if (_txtFreightAmt.value == "" || isNaN(_txtFreightAmt.value)) {
                _txtFreightAmt.value = 0;
            }
            if (_txtPackingAmt.value == "" || isNaN(_txtPackingAmt.value)) {
                _txtPackingAmt.value = 0;
            }
            if (_txtPostageAmt.value == "" || isNaN(_txtPostageAmt.value)) {
                _txtPostageAmt.value = 0;
            }
            if (_txtOtherCharges.value == "" || isNaN(_txtOtherCharges.value)) {
                _txtOtherCharges.value = 0;
            }
            if (_TxtGrandTotal.value == "" || isNaN(_TxtGrandTotal.value)) {
                _TxtGrandTotal.value = 0;
            }

            _TxtSerTax.value = parseFloat((ddlValue / 100) * (parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) + parseFloat(_txtDekhrekhAmt.value) + parseFloat(_txtHamaliAmt.value) + parseFloat(_txtCESSAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtFreightAmt.value) + parseFloat(_txtPackingAmt.value) + parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2))).toFixed(2);

            _TxtGrandTotal.value = parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) + parseFloat(_TxtSerTax.value) + parseFloat(_txtDekhrekhAmt.value) + parseFloat(_txtInstallationServiceAmount.value) + parseFloat(_txtHamaliAmt.value) + parseFloat(_txtCESSAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtInstallationCharge.value) + parseFloat(_txtFreightAmt.value) + parseFloat(_txtPackingAmt.value) + parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2);
            _TxtNETTotal.value = parseFloat(parseFloat(_TxtSubTotal.value) - parseFloat(_TxtDiscount.value) + parseFloat(_TxtVat.value) + parseFloat(_TxtSerTax.value) + parseFloat(_txtDekhrekhAmt.value) + parseFloat(_txtInstallationServiceAmount.value) + parseFloat(_txtHamaliAmt.value) + parseFloat(_txtCESSAmt.value) + parseFloat(_txtexciseduty.value) + parseFloat(_txtInstallationCharge.value) + parseFloat(_txtFreightAmt.value) + parseFloat(_txtPackingAmt.value) + parseFloat(_txtPostageAmt.value) + parseFloat(_txtOtherCharges.value)).toFixed(2);

        }

        function GetAmountOfExciseDuty() {
            var _txtexcisedutyper = document.getElementById('<%= txtexcisedutyper.ClientID %>');
            var _txtexciseduty = document.getElementById('<%= txtexciseduty.ClientID %>');
            var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');
            if (_txtexcisedutyper.value == "" || isNaN(_txtexcisedutyper.value)) {
                _txtexcisedutyper.value = 0;
            }
            if (_txtexciseduty.value == "" || isNaN(_txtexciseduty.value)) {
                _txtexciseduty.value = 0;
            }
            if (_TxtSubTotal.value == "" || isNaN(_TxtSubTotal.value)) {
                _TxtSubTotal.value = 0;
            }
            _txtexciseduty.value = parseFloat((parseFloat(_txtexcisedutyper.value) * 0.01) * (parseFloat(_TxtSubTotal.value))).toFixed(2);
            CalAsPerDDl();
        }

        function GetAmountOfINSTALLSERVICECHARGE() {

            var _txtInstallationServicetax = document.getElementById('<%= txtInstallationServicetax.ClientID %>');
            var _txtInstallationCharge = document.getElementById('<%= txtInstallationCharge.ClientID %>');
            var _txtInstallationServiceAmount = document.getElementById('<%= txtInstallationServiceAmount.ClientID %>');

            if (_txtInstallationServiceAmount.value == "" || isNaN(_txtInstallationServiceAmount.value)) {
                _txtInstallationServiceAmount.value = 0;
            }
            if (_txtInstallationServicetax.value == "" || isNaN(_txtInstallationServicetax.value)) {
                _txtInstallationServicetax.value = 0;
            }

            if (_txtInstallationCharge.value == "" || isNaN(_txtInstallationCharge.value)) {
                _txtInstallationCharge.value = 0;
            }

            _txtInstallationServiceAmount.value = parseFloat((parseFloat(_txtInstallationServicetax.value) * 0.01) * (parseFloat(_txtInstallationCharge.value))).toFixed(2);
            CalAsPerDDl();
        }

        function EnableTextBox() {
            var txtHamaliAmt = document.getElementById('<%= txtHamaliAmt.ClientID %>');
            var CHKHAMALI = document.getElementById('<%= CHKHAMALI.ClientID %>');

            var txtFreightAmt = document.getElementById('<%= txtFreightAmt.ClientID %>');
            var CHKFreightAmt = document.getElementById('<%= CHKFreightAmt.ClientID %>');

            var txtOtherCharges = document.getElementById('<%= txtOtherCharges.ClientID %>');
            var CHKOtherCharges = document.getElementById('<%= CHKOtherCharges.ClientID %>');

            var txtPostageAmt = document.getElementById('<%= txtPostageAmt.ClientID %>');
            var CHKLoading = document.getElementById('<%= CHKLoading.ClientID %>');

            if (CHKHAMALI.checked == false) {
                txtHamaliAmt.disabled = false;
            }
            if (CHKHAMALI.checked == true) {
                txtHamaliAmt.value = "0.00";
                txtHamaliAmt.disabled = true;
            }

            if (CHKFreightAmt.checked == false) {
                txtFreightAmt.disabled = false;
            }
            if (CHKFreightAmt.checked == true) {
                txtFreightAmt.value = "0.00";
                txtFreightAmt.disabled = true;
            }

            if (CHKOtherCharges.checked == false) {
                txtOtherCharges.disabled = false;
            }
            if (CHKOtherCharges.checked == true) {
                txtOtherCharges.value = "0.00";
                txtOtherCharges.disabled = true;
            }

            if (CHKLoading.checked == false) {
                txtPostageAmt.disabled = false;
            }
            if (CHKLoading.checked == true) {
                txtPostageAmt.value = "0.00";
                txtPostageAmt.disabled = true;
            }
            CalAsPerDDl();
        }
    </script>

    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
            <%-------------------------------------------------------------------------------------------------------------- --%>
            <asp:Button ID="BtnPopMail" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlInfoMail" runat="server" CssClass="ModelPopUpPanelBackGroundMail"
                Style="display: none;">
                <table width="100%" class="PopUpHeader">
                    <tr style="background-color: Navy; text-align: center">
                        <td>
                            &nbsp;<asp:Label ID="Label5" Text="Revo MMS - Mail" runat="server" ForeColor="white"
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
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
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
                                        <asp:TextBox runat="server" ID="TXTSRNDMAIL" CssClass="TextBox" Width="550px"></asp:TextBox>
                                        <ajax:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server" TargetControlID="TXTSRNDMAIL"
                                            Corners="All" Radius="6" BorderColor="Gray">
                                        </ajax:RoundedCornersExtender>
                                        <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="TXTSRNDMAIL"
                                            WatermarkText="SENDMAIL" WatermarkCssClass="water" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="TXTKSUBJECT" CssClass="TextBox" Width="550px"></asp:TextBox>
                                        <ajax:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="TXTKSUBJECT"
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
                                        <iframe runat="server" id="iframepdf" height="260px" width="800px"></iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajax:ModalPopupExtender ID="MDPopUpYesNoMail" BackgroundCssClass="modalBackground"
                runat="server" TargetControlID="BtnPopMail" PopupControlID="pnlInfoMail" DropShadow="true">
            </ajax:ModalPopupExtender>
            <%---------------------------------------------------------------------------------------------------------------------%>
            
            <div id="divwidth">
            </div>
          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="Server">
   Job Work Out Order  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <%--pop up for Indent button--%>
            <asp:Button ID="btnPopHide" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlInfo" runat="server" CssClass="ModelPopUpPanelBackGroundSmall"
                Style="display: none;">
                <table width="100%" class="PopUpHeader">
                    <tr style="background-color: Navy; text-align: center">
                        <td>
                            &nbsp;<asp:Label ID="popUpTitle" Text="Revo MMS - Purchase Order" runat="server"
                                ForeColor="white" Font-Bold="true" Font-Size="12px"></asp:Label>
                        </td>
                    </tr>
                    <%--   <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblPopUpYesNoMessage" runat="server" Font-Size="12px" 
                            Text="Are You Sure To Delete All Un-Check List Of Particulars From Indent ? ">
                            </asp:Label>
                        </td>
                    </tr>--%>
                </table>
                <table width="100%" style="margin: 5px 0 5px 0;">
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblPopUpYesNoMessage" runat="server" Font-Size="12px" Text="Are You Sure To Delete All Un-Check List Of Particulars From Indent ? ">
                            </asp:Label>
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
            <%--pop up for Indent button--%>
            <asp:Button ID="btnPopHodePOR" runat="server" Style="display: none;" />
            <asp:Panel ID="PnlGETPOR" runat="server" CssClass="ModelPopUpPanelBackGroundLarge"
                Style="display: none;">
                <div style="width: 900px; height: 500px; overflow: scroll;">
                    <table width="100%" class="PopUpHeader">
                        <tr style="background-color: Navy; text-align: center">
                            <td>
                                &nbsp;<asp:Label ID="Label3" Text="Revo MMS - Purchase Order" runat="server" ForeColor="white"
                                    Font-Bold="true" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="margin: 5px 0 5px 0;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="Label4" runat="server" Font-Size="12px" Text="Last Purchase Order Record"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewLPR" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                    DataKeyNames="#">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="LblProcessId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="All">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="GridViewLPRSelect" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Purchased" HeaderText="Purchased">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PONo" HeaderText="PO No.">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PODate" HeaderText="Date">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ItemName" HeaderText="Particulars">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SuplierName" HeaderText="Vendor">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Qty" HeaderText="Qty.">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Rate" HeaderText="Rate">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DiscPer" HeaderText="Discount">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TaxPer" HeaderText="VAt">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NETRATE" HeaderText="Net Amount">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Project" HeaderText="Project">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div style="float: right; margin-bottom: 8px;">
                        <asp:Button ID="PopUpBtnAdd" Text="Yes" runat="server" CssClass="button" CommandName="yes"
                            OnCommand="PopUpYesNoPOR_Command" />
                        &nbsp; &nbsp;<asp:Button ID="PopUpBtnCANCEL" Text="No" runat="server" CssClass="button"
                            CommandName="no" OnCommand="PopUpYesNoPOR_Command" />
                        &nbsp; &nbsp;</div>
                </div>
            </asp:Panel>
            <ajax:ModalPopupExtender ID="PopUpYesNoPOR" BackgroundCssClass="modalBackground"
                runat="server" TargetControlID="btnPopHodePOR" PopupControlID="PnlGETPOR">
            </ajax:ModalPopupExtender>
            <%--Start Pop Up For Indent Item Rate History--%>
            <%--pop up for Indent button--%>
            <asp:Button ID="btnPopIndItmRtHist" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlIndentItmRateHistory" runat="server" Style="position: fixed; top: 4%;
                left: 10px; min-width: 800px; height: 550px; background-color: White; border: solid 3px black;
                display: none;">
                <table width="100%" class="PopUpHeader">
                    <tr style="background-color: Navy; height: 25px; text-align: center; vertical-align: middle;">
                        <td>
                            <div style="float: left;">
                                <asp:Label ID="Label6" Text="Revo MMS - Purchase Order" runat="server" ForeColor="white"
                                    Font-Bold="true" Font-Size="12px"></asp:Label></div>
                            <div style="float: right;">
                                <asp:Button ID="btnPopIndentItmRateHistClose" Text="Close" runat="server" CssClass="button"
                                    OnClick="btnPopIndentItmRateHistClose_Click" /></div>
                        </td>
                    </tr>
                </table>
                <div style="margin: 0px 10px 10px 10px;">
                    <table width="100%" style="margin: 5px 0 5px 0;">
                        <tr>
                            <td>
                                <asp:Label ID="PopLblIndentNo" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="overflow: scroll; width: 770px; height: 450px;">
                                    <asp:GridView ID="grdPopIndentItmRates" runat="server" CssClass="mGrid" GridLines="Both"
                                        AllowPaging="false" AutoGenerateColumns="true" EmptyDataText="Sorry...No Records Found...!!!">
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div style="float: right; margin-bottom: 8px;">
                    </div>
                </div>
            </asp:Panel>
            <ajax:ModalPopupExtender ID="PopUpIndentItemRateHistory" BackgroundCssClass="modalBackground"
                runat="server" TargetControlID="btnPopIndItmRtHist" PopupControlID="pnlIndentItmRateHistory">
            </ajax:ModalPopupExtender>
            <%--pop up for Indent button--%>
            <%--End Pop Up For Indent Item Rate History--%>
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%" cellspacing="6">
                            <tr>
                                <td colspan="2">
                                    <fieldset id="F1" runat="server" class="FieldSet">
                                        <table width="100%">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <tr>
                                                       <%-- <td class="Label">
                                                            PO Through :
                                                        </td>--%>
                                                        <%--<td>
                                                            <asp:RadioButtonList ID="rdoPOThrough" runat="server" RepeatDirection="Horizontal"
                                                                CssClass="RadioButton" OnSelectedIndexChanged="rdoPOThrough_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                                
                                                                <asp:ListItem  Value="1">&#160;Item Wise</asp:ListItem>

                                                                <asp:ListItem  Selected="True" Value="2">&#160;Supplier Comparsion</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>--%>
                                                        <td align="right" class="Label_Dynamic">
                                                            <asp:Button ID="BtnShow" runat="server" CssClass="Display_None" OnClick="BtnShow_Click"
                                                                Text="Show" ToolTip="Show Details" ValidationGroup="Add" />
                                                            Date :
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="LblDate" runat="server" CssClass="Display_None" Font-Bold="true"></asp:Label>
                                                            <asp:TextBox runat="server" ID="TxtDate" CssClass="TextBox" Width="80px"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageFromDate" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                                                ImageUrl="~/Images/Icon/DateSelector.png" meta:resourcekey="ImageFromDateResource1" />
                                                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                                PopupButtonID="ImageFromDate" TargetControlID="TxtDate" Enabled="True">
                                                            </ajax:CalendarExtender>
                                                        </td>
                                                        <td class="Label">
                                                            Company :
                                                        </td>
                                                        <td align="left">
                                                            <ajax:ComboBox ID="ddlCompany" runat="server" DropDownStyle="DropDown" AutoPostBack="false"
                                                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                                                Width="230px" CssClass="CustomComboBoxStyle">
                                                            </ajax:ComboBox>
                                                        </td>





                                                         <td class="Label">
                                                            Nature Of Proccessing :
                                                        </td>
                                                        <td align="left">
                                                         <asp:TextBox ID="txtnatureofproccessing" runat="server" TextMode="MultiLine" ></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                     
                                                    <tr id="trsupcon" runat="server">
                                                         <td class="Label">
                                                          Enquiry No:
                                                        </td>
                                                        <td colspan="1">
                                                            <asp:DropDownList ID="drpenquiry" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpenquiry_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>

                                                          

                                                        
                                                   
                                                    </tr>


                                                   




                                                    <tr>



                                                        



                                                       
                                                        <td colspan="1">
                                                            <asp:TextBox ID="TXTPOQTNO" runat="server" CssClass="TextBox" Width="200px" Visible="false"></asp:TextBox>
                                                        </td>
                                                        
                                                     <td colspan="3">
                                                            <asp:TextBox ID="TXTPOQTDATE" runat="server" CssClass="TextBox" Width="80px" Visible="false"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton4" runat="server" Visible="false" CausesValidation="False" CssClass="Imagebutton"
                                                                ImageUrl="~/Images/Icon/DateSelector.png" meta:resourcekey="ImageFromDateResource1" />
                                                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd-MMM-yyyy HH':'mm':'ss"
                                                                Animated="true" PopupButtonID="ImageButton4" TargetControlID="TXTPOQTDATE" Enabled="True">
                                                            </ajax:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="TR_Requision" runat="server">
                                                        <td class="Label">
                                                            Indent No.:
                                                        </td>
                                                        <td>
                                                            <ajax:ComboBox ID="ddlDepartment" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                                                Width="200px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                            </ajax:ComboBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Button ID="btnShowIndentItmRateHist" runat="server" Text="Show Indent Item Rates"
                                                                Width="160px" OnClick="btnShowIndentItmRateHist_Click" />
                                                        </td>
                                                        <td align="left">
                                                        </td>
                                                        <td class="Label">
                                                        </td>
                                                        <td align="left">
                                                            <asp:CheckBox runat="server" ID="RejectIndentItem" Text="&nbsp;Complete Indent" Font-Bold="true"
                                                                Font-Size="Medium" ForeColor="DarkCyan" ToolTip="THE UNCHECK CHECK BOX ARE CONSIDER FOR DELETING THAT PARTICULARES FROM INDENT & ARE NOT CONSIDERD FOR FURTHER PURCHASE ORDER"
                                                                AutoPostBack="true" OnCheckedChanged="RejectIndentItem_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                    <tr id="TR_Item" runat="server">
                                                        <td class="Label">
                                                            Category :
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCategory" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                                                Width="270px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="Label_Dynamic" align="right" rowspan="5" valign="middle">
                                                            Rate :
                                                        </td>
                                                        <td align="left" rowspan="5">


                                                            <asp:TextBox ID="lstSupplierRate" runat="server"></asp:TextBox>
                                                           <%-- <asp:ListBox ID="lstSupplierRate" runat="server" Width="300px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="lstSupplierRate_SelectedIndexChanged"></asp:ListBox>--%>
                                                        </td>

                                                       
                                                        <td align="right" class="Label_Dynamic" valign="top">
                                                            Qty :
                                                        </td>
                                                        <td align="left" class="Label_Dynamic" valign="top">
                                                            <asp:TextBox ID="txtItemOrdQty" runat="server" CssClass="TextBox" Width="60px" AutoPostBack="True"
                                                                OnTextChanged="txtItemOrdQty_TextChanged"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlUNIT" runat="server" AutoPostBack="true" CssClass="ComboBox"
                                                                Width="90px" OnSelectedIndexChanged="ddlUNIT_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="TR_RateList" runat="server">
                                                        <td class="Label">
                                                            Sub Category :
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlsubcategory" runat="server" DropDownStyle="DropDown" AutoPostBack="true" 
                                                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                                                Width="270px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            GST(%) :
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtvatper" runat="server" CssClass="TextBox" Width="60px" AutoPostBack="True"
                                                                OnTextChanged="txtvatper_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:TextBox ID="txtvatamt" runat="server" CssClass="Display_None" Width="60px" Enabled="false"></asp:TextBox>
                                                            <asp:CheckBox ID="ChkCGST" Text="CGST" runat="server" OnCheckedChanged="ChkCGST_CheckedChanged"
                                                                AutoPostBack="true" onkeyup="Calculate_NetTotal();" />
                                                            <asp:CheckBox ID="ChkSGST" Text="SGST" runat="server" OnCheckedChanged="ChkSGST_CheckedChanged"
                                                                AutoPostBack="true" onkeyup="Calculate_NetTotal();" />
                                                            <asp:CheckBox ID="ChkIGST" Text="IGST" runat="server" OnCheckedChanged="ChkIGST_CheckedChanged"
                                                                AutoPostBack="true" onkeyup="Calculate_NetTotal();" />
                                                            <%--&nbsp;Rs/---%>
                                                        </td>
                                                        <td align="left" valign="middle">
                                                        </td>
                                                        <td align="left" valign="middle">
                                                        </td>
                                                    </tr>
                                                    <tr id="TR2" runat="server">
                                                        <td class="Label">
                                                            Item :
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                                                Width="270px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right" class="Label" valign="top">
                                                            CGST(%) :
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtCGSTItemPer" runat="server" CssClass="TextBox" Width="60px" AutoPostBack="True"
                                                                OnTextChanged="txtCGSTItemPer_TextChanged"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:TextBox ID="txtCGSTItemAmt" runat="server" CssClass="TextBox" Width="60px" Enabled="false"></asp:TextBox>
                                                            &nbsp;Rs/-
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td class="Label">
                                                        </td>
                                                    </tr>
                                                    <tr id="TR9" runat="server">
                                                        <td class="Label">
                                                        </td>
                                                        <td valign="top">
                                                        </td>
                                                        <td align="right" class="Label" valign="top">
                                                            SGST(%) :
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtSGSTItemPer" runat="server" CssClass="TextBox" Width="60px" AutoPostBack="True"
                                                                OnTextChanged="txtSGSTItemPer_TextChanged"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:TextBox ID="txtSGSTItemAmt" runat="server" CssClass="TextBox" Width="60px" Enabled="false"></asp:TextBox>
                                                            &nbsp;Rs/-
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td class="Label">
                                                        </td>
                                                    </tr>
                                                    <tr id="TR10" runat="server">
                                                        <td class="Label">
                                                        </td>
                                                        <td valign="top">
                                                        </td>
                                                        <td align="right" class="Label" valign="top">
                                                            IGST(%) :
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtIGSTItemPer" runat="server" CssClass="TextBox" Width="60px" AutoPostBack="True"
                                                                OnTextChanged="txtIGSTItemPer_TextChanged"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:TextBox ID="txtIGSTItemAmt" runat="server" CssClass="TextBox" Width="60px" Enabled="false"></asp:TextBox>
                                                            &nbsp;Rs/-
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td class="Label">
                                                        </td>
                                                    </tr>
                                                    <tr id="TR3" runat="server">
                                                        <td class="Label">
                                                            Item Desc :
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlItemDesc" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                                                Width="270px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right" class="Label" valign="top">
                                                            DISC(%) :
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtdiscper" runat="server" CssClass="TextBox" Width="60px" AutoPostBack="True"
                                                                OnTextChanged="txtdiscper_TextChanged"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:TextBox ID="txtdiscamt" runat="server" CssClass="TextBox" Width="60px" Enabled="false"></asp:TextBox>
                                                            &nbsp;Rs/-
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td class="Label">
                                                        </td>
                                                    </tr>
                                                    <tr id="TR5" runat="server">

                                                         <td class="Label">
                                                        Select Supplier:
                                                        </td>
                                                        <td colspan="1">
                                                            <asp:DropDownList ID="drpsupplier"    runat="server" Width="270px" AutoPostBack="true"  OnSelectedIndexChanged="drpsupplier_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>

                                                        <td>
                                                        </td>
                                                        <td valign="top">
                                                        </td>
                                                       
                                                        <td class="Label_Dynamic" align="right" rowspan="5" valign="middle">
                                                            <asp:Button ID="BtnAdd" runat="server" CssClass="button" OnClick="BtnAdd_Click" Text="Add"
                                                                ToolTip="Add To Purchase Order Grid" />
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td class="Label">
                                                        </td>
                                                    </tr>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                          
                            <tr id="TR_PODtls" runat="server">
                                <td colspan="2">
                                    <fieldset id="FS_Requisition" class="FieldSet" runat="server" style="width: 100%">
                                        <legend id="Legend2" class="legend" runat="server">Indent Details</legend>
                                        <div id="Div4" runat="server" class="scrollableDiv">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="true">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="GrdPODtls" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                                    DataKeyNames="#">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LblProcessId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageGridEdit" runat="server" Visible="false" CommandArgument='<%# Eval("#") %>'
                                                                                    CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                                                                <asp:ImageButton ID="ImgBtnDelete" runat="server" Visible="false" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                                                    CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                                                    TargetControlID="ImgBtnDelete">
                                                                                </ajax:ConfirmButtonExtender>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                            <HeaderStyle Width="20px" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" CssClass="Display_None"
                                                                                Wrap="false" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="Cafeteria" HeaderText="Cafeteria">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ItemCode" HeaderText="Code">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ItemName" HeaderText="Item">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="OrdQty" HeaderText="Ord Qty">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="StoreLocation" HeaderText="Store Location">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:LinkButton ID="hyl_Hide" runat="server" CssClass="linkButton" OnClick="hyl_Hide_Click">Hide</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <fieldset id="Fieldset1" class="FieldSet" runat="server" style="width: 100%">
                                        <legend id="Legend1" class="legend" runat="server">Purchase Order Details</legend>
                                        <div id="Div1" runat="server" class="ScrollableDiv_FixHeightWidth4">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="PurOrderGrid" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                                    DataKeyNames="#" OnRowDataBound="PurOrderGrid_RowDataBound" OnRowCommand="PurOrderGrid_RowCommand"
                                                                    OnRowDeleting="PurOrderGrid_RowDeleting">
                                                                    <Columns>
                                                                        <%-- 0--%>
                                                                        <asp:TemplateField HeaderText="#" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LblProcessId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%-- 1--%>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageGridEdit" runat="server" Visible="false" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                                                    CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                                                                <asp:ImageButton ID="ImgBtnDelete" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                                                    CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                                                    TargetControlID="ImgBtnDelete">
                                                                                </ajax:ConfirmButtonExtender>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <HeaderStyle Width="20px" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Wrap="false" />
                                                                        </asp:TemplateField>
                                                                        <%-- 2--%>
                                                                        <asp:BoundField DataField="Code" HeaderText="Code">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                        </asp:BoundField>
                                                                        <%-- 3--%>
                                                                        <asp:BoundField DataField="Item" HeaderText="Item">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 4--%>
                                                                        <asp:BoundField DataField="ItemDescID" HeaderText="DescriptionID">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                        </asp:BoundField>
                                                                        <%-- 5--%>
                                                                        <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 6--%>
                                                                        <asp:BoundField DataField="ReqQty" HeaderText="Avl. Qty">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 7--%>
                                                                        <asp:BoundField DataField="OrdQty" HeaderText="Ord. Qty">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 8--%>
                                                                        <asp:BoundField DataField="Vendor" HeaderText="Vendor">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 9--%>
                                                                        <asp:BoundField DataField="PurchaseRate" HeaderText="Rate">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 10--%>
                                                                        <asp:BoundField DataField="pervat" HeaderText="VAT(%)">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                        </asp:BoundField>
                                                                        <%-- 11--%>
                                                                        <asp:BoundField DataField="vat" HeaderText="VAT">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                        </asp:BoundField>
                                                                        <%-- 12--%>
                                                                        <asp:BoundField DataField="perGST" HeaderText="GST(%)">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 13--%>
                                                                        <asp:BoundField DataField="CGSTPer" HeaderText="CGST(%)">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 14--%>
                                                                        <asp:BoundField DataField="CGSTAmt" HeaderText="CGST">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 15--%>
                                                                        <asp:BoundField DataField="SGSTPer" HeaderText="SGST(%)">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 16--%>
                                                                        <asp:BoundField DataField="SGSTAmt" HeaderText="SGST">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 17--%>
                                                                        <asp:BoundField DataField="IGSTPer" HeaderText="IGST(%)">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 18--%>
                                                                        <asp:BoundField DataField="IGSTAmt" HeaderText="IGST">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 19--%>
                                                                        <asp:BoundField DataField="perdisc" HeaderText="DISC(%)">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 20--%>
                                                                        <asp:BoundField DataField="disc" HeaderText="DISC">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 21--%>
                                                                        <asp:BoundField DataField="PurchaseAmount" HeaderText="Amount">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:BoundField>
                                                                        <%-- 22--%>
                                                                        <asp:BoundField DataField="VendorId" HeaderText="VendorId">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                        </asp:BoundField>
                                                                        <%-- 23--%>
                                                                        <asp:BoundField DataField="ItemId" HeaderText="ItemId">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                        </asp:BoundField>
                                                                        <%-- 24--%>
                                                                        <asp:BoundField DataField="RequisitionCafeId" HeaderText="RequisitionCafeId">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                        </asp:BoundField>
                                                                        <%-- 25--%>
                                                                        <asp:BoundField DataField="UnitConvDtlsId" HeaderText="UnitConvDtlsId">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                        </asp:BoundField>
                                                                        <%-- 26--%>
                                                                        <asp:BoundField DataField="MainUnitQty" HeaderText="MainUnitQty">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                        </asp:BoundField>
                                                                        <%-- 27--%>
                                                                        <asp:TemplateField HeaderText="TERMS CONDITION">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImgBtnAddTermsSuplier" runat="server" Visible="true" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                                                    CommandName="TERMS" ImageUrl="~/Images/Icon/Gridadd.png" ToolTip="Add Term & Condition" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="6%" />
                                                                        </asp:TemplateField>
                                                                        <%--28--%>
                                                                        <asp:TemplateField HeaderText="TERMS CONDITION">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="TXTTERMSCONDITIONPOGRID" Text='<%# Bind("TXTTERMSCONDITIONPOGRID") %>'
                                                                                    runat="server" CssClass="Display_None"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"
                                                                                Width="6%" />
                                                                        </asp:TemplateField>
                                                                        <%-- 29--%>
                                                                        <asp:TemplateField HeaderText="TERMS CONDITION">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="TXTTERMSCONDITIONPOGRIDPAYMNET" Text='<%# Bind("TXTTERMSCONDITIONPOGRIDPAYMNET") %>'
                                                                                    runat="server" CssClass="Display_None"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"
                                                                                Width="6%" />
                                                                        </asp:TemplateField>
                                                                        <%-- 30--%>
                                                                        <asp:TemplateField HeaderText="Remark">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="TXTREMARK" Text='<%# Bind("RemarkForPO") %>' runat="server" CssClass="TextBox"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td class="Label" >
                                    Net Total :
                                </td>
                                <td width="150px" align="right">
                                    Rs-
                                    <asp:TextBox ID="txtNetTotal" runat="server" CssClass="TextBoxReadOnly" Width="128px"></asp:TextBox>
                                </td>
                            </tr>
                            <%--Here New design for adding extra things--%>
                            <tr>
                                <td colspan="2">
                                    <table width="100%" cellspacing="8">
                                        <tr id="Tr1" runat="server">
                                            <td class="LabelextraDuty" align="right">
                                                Sub Total :
                                            </td>
                                            <td class="Label" align="right">
                                                Rs-
                                                <asp:TextBox ID="txtSubTotal" runat="server" CssClass="TextBoxReadOnly" Enabled="false"
                                                    onkeyup="CalPercentage_Amount(this);" Width="128px" Style="text-align: right"
                                                    TabIndex="19"></asp:TextBox>
                                            </td>
                                            <td class="LabelextraDuty" align="right">
                                                Discount :
                                            </td>
                                            <td class="Label" align="right">
                                                Rs-
                                                <asp:TextBox ID="txtDiscount" onkeyup="CalPercentage_Amount(this);" OnChange="CalPercentage_Amount(this);"
                                                    runat="server" TabIndex="21" CssClass="TextBoxReadOnly" Width="128px" Style="text-align: right"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Tr8" runat="server">
                                            <td class="Display_None" align="right">
                                                VAT :
                                            </td>
                                            <td class="Display_None" align="right">
                                                Rs-
                                                <asp:TextBox ID="txtVATAmount" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    OnChange="CalPercentage_Amount(this);" TabIndex="23" CssClass="TextBoxReadOnly"
                                                    Width="128px" Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td class="LabelextraDuty" align="right">
                                                Hamali :
                                            </td>
                                            <td class="Label" align="right">
                                                <asp:CheckBox runat="server" ID="CHKHAMALI" Text="AT ACTUAL" onclick="javascript:EnableTextBox();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                Rs-
                                                <asp:TextBox ID="txtHamaliAmt" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="25" OnChange="CalPercentage_Amount(this);" CssClass="TextBox" Width="128px"
                                                    Style="text-align: right"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Tr4" runat="server">
                                            <td class="LabelextraDuty" align="right">
                                                Transport / Freight :
                                            </td>
                                            <td class="Label" align="right">
                                                <asp:CheckBox runat="server" ID="CHKFreightAmt" Text="AT ACTUAL" onclick="javascript:EnableTextBox();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                Rs-
                                                <asp:TextBox ID="txtFreightAmt" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="28" OnChange="CalPercentage_Amount(this);" CssClass="TextBox" Width="128px"
                                                    Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td class="LabelextraDuty" align="right">
                                                Other Charges :
                                            </td>
                                            <td class="Label" align="right">
                                                <asp:CheckBox runat="server" ID="CHKOtherCharges" Text="AT ACTUAL" onclick="javascript:EnableTextBox();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                Rs-
                                                <asp:TextBox ID="txtOtherCharges" runat="server" CssClass="TextBox" Width="128px"
                                                    Style="text-align: right" TabIndex="31" OnChange="CalPercentage_Amount(this);"
                                                    onkeyup="CalPercentage_Amount(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Tr7" runat="server" class="Display_None">
                                            <td class="LabelextraDuty" align="right">
                                                Dekhrekh :
                                            </td>
                                            <td class="Label" align="right">
                                                Rs-
                                                <asp:TextBox ID="txtDekhrekhAmt" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="24" OnChange="CalPercentage_Amount(this);" CssClass="TextBox" Width="128px"
                                                    Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td class="LabelextraDuty" align="right">
                                                Packing :
                                            </td>
                                            <td class="Label" align="right">
                                                Rs-
                                                <asp:TextBox ID="txtPackingAmt" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="29" OnChange="CalPercentage_Amount(this);" CssClass="TextBox" Width="128px"
                                                    Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td class="LabelextraDuty" align="right">
                                                Cess :
                                            </td>
                                            <td class="Label" align="right">
                                                Rs-
                                                <asp:TextBox ID="txtCESSAmt" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="27" OnChange="CalPercentage_Amount(this);" CssClass="TextBox" Width="128px"
                                                    Style="text-align: right"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LabelextraDuty" align="right">
                                                Loading / Unloading :
                                            </td>
                                            <td class="Label" align="right">
                                                <asp:CheckBox runat="server" ID="CHKLoading" Text="AT ACTUAL" onclick="javascript:EnableTextBox();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                Rs-
                                                <asp:TextBox ID="txtPostageAmt" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="30" OnChange="CalPercentage_Amount(this);" CssClass="TextBox" Width="128px"
                                                    Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td class="LabelextraDuty" align="right">
                                                Ser. Tax :
                                            </td>
                                            <td class="Label" align="right">
                                                <asp:DropDownList ID="DDLSERVICETAX" runat="server" Width="80px" CssClass="Display_None"
                                                    onchange="CalAsPerDDl();">
                                                </asp:DropDownList>
                                             <%--   &nbsp;&nbsp;&nbsp;&nbsp; Rs---%>
                                                <asp:TextBox ID="txtSerTax" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="27" OnChange="CalPercentage_Amount(this);" CssClass="Display_None" Width="128px"
                                                    Style="text-align: right"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Tr6" runat="server">
                                            <td class="LabelextraDuty" align="right">
                                                Installation Charges :
                                            </td>
                                            <td class="Label" align="right">
                                                <asp:TextBox runat="server" ID="txtInstallationRemark" Width="200px" CssClass="TextBox"
                                                    Height="20px" TextMode="MultiLine"></asp:TextBox>
                                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtInstallationRemark"
                                                    WatermarkText="REMARK FOR INSTALLATION CHARGES" WatermarkCssClass="water" />
                                                &nbsp;&nbsp;&nbsp; Rs-
                                                <asp:TextBox ID="txtInstallationCharge" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="25" OnChange="CalPercentage_Amount(this);" CssClass="TextBox" Width="128px"
                                                    Style="text-align: right" ToolTip="Installation Charges"></asp:TextBox>
                                            </td>
                                            <td class="LabelextraDuty" align="right">
                                                Service Tax On Installation Charges :
                                            </td>
                                            <td class="Label" align="right">
                                                <asp:TextBox runat="server" ID="txtInstallationServicetax" Width="80px" CssClass="TextBox"
                                                    onkeyup="GetAmountOfINSTALLSERVICECHARGE();" Style="text-align: right"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp; Rs-
                                                <asp:TextBox ID="txtInstallationServiceAmount" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="25" OnChange="CalPercentage_Amount(this);" CssClass="TextBoxReadOnly"
                                                    Width="128px" Style="text-align: right" Enabled="false" ToolTip="Installation Charges Service Tax"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LabelextraDuty" align="right">
                                                Excise Duty :
                                            </td>
                                            <td class="Label" align="right">
                                                <asp:TextBox runat="server" ID="txtexcisedutyper" ToolTip="Enter Percentage For Here"
                                                    onkeyup="GetAmountOfExciseDuty();" TabIndex="25" OnChange="GetAmountOfExciseDuty();"
                                                    CssClass="TextBox" Width="30px" Style="text-align: right"></asp:TextBox>&nbsp;&nbsp;%=&nbsp;&nbsp;
                                                <asp:TextBox ID="txtexciseduty" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="25" OnChange="CalPercentage_Amount(this);" CssClass="TextBoxReadOnly"
                                                    Width="128px" Style="text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td align="right" class="LabelextraDuty">
                                            </td>
                                            <td class="Label" align="right">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LabelextraDuty" align="right">
                                            </td>
                                            <td class="Label" align="right">
                                                <%--<asp:CheckBox ID="ChkCGST" Text="CGST" runat="server" AutoPostBack="true" />
                                                <asp:CheckBox ID="ChkSGST" Text="SGST" runat="server" AutoPostBack="true" />
                                                <asp:CheckBox ID="ChkIGST" Text="IGST" runat="server" AutoPostBack="true" />--%>
                                            </td>
                                            <td align="right" class="LabelextraDuty">
                                            </td>
                                            <td class="Label" align="right">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LabelextraDuty" align="right">
                                                CGST  :
                                            </td>
                                            <td class="Label" align="right">
                                                <asp:TextBox runat="server" ID="txtCGSTper" onkeyup="Calculate_NetTotal();" TabIndex="25"
                                                    CssClass="Display_None" Width="30px" Style="text-align: right"></asp:TextBox>
                                                <asp:TextBox ID="txtCGSTamt" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="25" OnChange="CalPercentage_Amount(this);" CssClass="TextBoxReadOnly"
                                                    Width="128px" Style="text-align: right" Enabled="false" ></asp:TextBox>
                                            </td>
                                            <td align="right" class="LabelextraDuty">
                                                SGST  :
                                            </td>
                                            <td class="Label" align="right">
                                                <asp:TextBox runat="server" ID="txtSGSTPer" onkeyup="Calculate_NetTotal();" TabIndex="25"
                                                    CssClass="Display_None" Width="30px" Style="text-align: right"></asp:TextBox>
                                                <asp:TextBox ID="txtSGSTAmt" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="25" OnChange="CalPercentage_Amount(this);" CssClass="TextBoxReadOnly"
                                                    Width="128px" Style="text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LabelextraDuty" align="right">
                                                IGST  :
                                            </td>
                                            <td class="Label" align="right">
                                                <asp:TextBox runat="server" ID="txtIGSTPer" onkeyup="Calculate_NetTotal();" TabIndex="25"
                                                    CssClass="Display_None" Width="30px" Style="text-align: right"></asp:TextBox>
                                                <asp:TextBox ID="txtIGSTAmt" runat="server" onkeyup="CalPercentage_Amount(this);"
                                                    TabIndex="25" OnChange="CalPercentage_Amount(this);" CssClass="TextBoxReadOnly"
                                                    Width="128px" Style="text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td align="right" class="LabelextraDuty">
                                                Grand Total :
                                            </td>
                                            <td class="Label" align="right">
                                                Rs-
                                                <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="TextBoxReadOnly" Width="128px"
                                                    Style="text-align: right" TabIndex="32" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LabelextraDuty" align="right">
                                                Narration :
                                            </td>
                                            <td colspan="5" align="right">
                                                <asp:TextBox runat="server" ID="TXTNARRATION" Width="900px" CssClass="TextBox" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <%--end of new design--%>
                            <%--Here Design of terms and Condition Start--%>
                            <tr runat="server" class="Display_None">
                                <td colspan="6" width="100%" align="left">
                                    <%--colspan="6"--%>
                                    <ajax:Accordion ID="Accordion1" runat="server" ContentCssClass="accordionContent1"
                                        HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                        FadeTransitions="true" TransitionDuration="260" FramesPerSecond="20" AutoSize="None"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" SelectedIndex="1">
                                        <Panes>
                                            <ajax:AccordionPane ID="AccordionPane1" runat="server" Width="100%">
                                                <Header>
                                                    <a class="href" href="#">Terms and Conditions</a></Header>
                                                <Content>
                                                    <div style="width: 100%">
                                                        <asp:GridView ID="GridTermCond" runat="server" AutoGenerateColumns="False" DataKeyNames="#"
                                                            Width="100%" CssClass="mGrid" TabIndex="4" AllowSorting="True" ShowFooter="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#" Visible="False">
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="LblEntryId0" runat="server" Width="30px"></asp:Label>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="All">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="GrdSelectAllHeader" runat="server" AutoPostBack="true" OnCheckedChanged="GrdSelectAllHeader1_CheckedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="GrdSelectAll" runat="server" AutoPostBack="false" Checked='<%# Convert.ToBoolean(Eval("select").ToString()) %>' />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="img_btn_Add" runat="server" ImageUrl="~/Images/Icon/Gridadd.png"
                                                                            TabIndex="8" OnClick="img_btn_Add_Click" />
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr.No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="6%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Title" ControlStyle-Width="200px" ControlStyle-Height="20px"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrtxtTermCondition_Head" runat="server" MaxLength="400" TextMode="MultiLine"
                                                                            CssClass="TextBox" Text='<%# Bind("Title") %>' TabIndex="6"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ControlStyle Height="30px" Width="200px" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description" ControlStyle-Width="200px" ControlStyle-Height="20px"
                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrtxtDesc" runat="server" MaxLength="400" TextMode="MultiLine" CssClass="TextBox"
                                                                            Text='<%# Bind("TDescptn") %>' TabIndex="6"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ControlStyle Height="30px" Width="480px" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
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
                            <%--Here Design of terms and Condition End--%>
                            <%--Here Design of PAYMENT TERMS Start--%>
                            <tr>
                                <td colspan="6" width="100%" align="left" visible="false" runat="server" id="TRID">
                                    <%--colspan="6"--%>
                                    <ajax:Accordion ID="Accordion2" runat="server" ContentCssClass="accordionContent1"
                                        HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                        FadeTransitions="true" TransitionDuration="260" FramesPerSecond="20" AutoSize="None"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" SelectedIndex="1">
                                        <Panes>
                                            <ajax:AccordionPane ID="AccordionPane2" runat="server" Width="100%">
                                                <Header>
                                                    <a class="href" href="#">Payment Terms</a></Header>
                                                <Content>
                                                    <div style="width: 100%">
                                                        <asp:RadioButtonList ID="RdoPaymentDays" runat="server" CellPadding="25" RepeatDirection="Horizontal"
                                                            CssClass="RadioButton">
                                                            <asp:ListItem Selected="True" Text="Immediate&nbsp;&nbsp;" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="15 Days&nbsp;&nbsp;&nbsp;" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="21 Days&nbsp;&nbsp;&nbsp;" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="30 Days&nbsp;&nbsp;&nbsp;" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="45 Days&nbsp;&nbsp;&nbsp;" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="60 Days" Value="6"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </Content>
                                            </ajax:AccordionPane>
                                        </Panes>
                                    </ajax:Accordion>
                                </td>
                            </tr>
                            <%--Here Design of terms and Condition End--%>
                            <tr>
                                <td align="center" colspan="2">
                                    <table align="center" width="25%">
                                        <tr>
                                            <td>
                                                <asp:Button ID="BtnUpdate" CssClass="button" runat="server" Text="Update" ValidationGroup="Add"
                                                    OnClick="BtnUpdate_Click" />
                                                <ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
                                                    TargetControlID="BtnUpdate">
                                                </ajax:ConfirmButtonExtender>
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" ValidationGroup="Add"
                                                    OnClick="BtnSave_Click" />
                                                <asp:TextBox ID="TXTJAVASCRIPTFLAG" runat="server" MaxLength="10" TextMode="SingleLine"
                                                    CssClass="Display_None"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnDelete" CssClass="button" runat="server" Text="Delete" ValidationGroup="Add"
                                                    OnClick="BtnDelete_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnCancel" CssClass="button" runat="server" Text="Cancel" OnClick="BtnCancel_Click"
                                                    CausesValidation="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <asp:Button ID="Btn1" runat="server" BackColor="White" Enabled="false" Width="5%" />
                                        <asp:Label runat="server" ID="lblBG" Text="- Generated" CssClass="Label4"></asp:Label>
                                        <asp:Button ID="Button1" runat="server" BackColor="Yellow" Enabled="false" Width="5%" />
                                        <asp:Label runat="server" ID="Label7" Text="- Approved" CssClass="Label4"></asp:Label>
                                        <asp:Button ID="Button2" runat="server" BackColor="Green" Enabled="false" Width="5%" />
                                        <asp:Label runat="server" ID="Label8" Text="- Authorised" CssClass="Label4"></asp:Label>
                                        <asp:Button ID="Button3" runat="server" BackColor="Brown" Enabled="false" Width="5%" />
                                        <asp:Label runat="server" ID="Label9" Text="- Mail Send" CssClass="Label4"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <div id="Div5" runat="server" class="ScrollableDiv">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <asp:GridView ID="ReportGrid" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    DataKeyNames="#,MailSend" AllowPaging="True" OnRowCommand="ReportGrid_RowCommand"
                                                    OnRowDeleting="ReportGrid_RowDeleting" OnDataBound="ReportGrid_DataBound" OnPageIndexChanging="GrdReqPO_PageIndexChanging"
                                                    OnRowDataBound="ReportGrid_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblEstimateId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageAccepted" runat="server" Visible="false" CommandArgument='<%# Eval("#") %>'
                                                                    CommandName="Acctepted" ImageUrl="~/Images/New Icon/DoneChanges.png" ToolTip="Order Accepted Can't Edit" />
                                                                <asp:ImageButton ID="ImageGridEdit" runat="server" Visible="true" CommandArgument='<%# Eval("#") %>'
                                                                    CommandName="Select" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                                                <asp:ImageButton ID="ImageApprove" runat="server" Visible="false" CommandArgument='<%# Eval("#") %>'
                                                                    CommandName="Approved" ImageUrl="~/Images/New Icon/LockReport.png" ToolTip="Order Approved Can't Delete" />
                                                                <asp:ImageButton ID="ImgBtnDelete" runat="server" CommandArgument='<%# Eval("#") %>'
                                                                    CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                                    TargetControlID="ImgBtnDelete">
                                                                </ajax:ConfirmButtonExtender>
                                                                <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="PS"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="NOPDF"%>&PrintFlag=<%="NO" %>'
                                                                    target="_blank">
                                                                    <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                                                                        ToolTip="Print Purchase Order" TabIndex="29" />
                                                                </a><a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="PS1"%>&SFlag=<%# Eval("POStatus")%>&PDFFlag=<%="PDF"%>&PrintFlag=<%="NO" %>'
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
                                                        <asp:BoundField DataField="PONo" HeaderText="PO No">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PODate" HeaderText="PO Date">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Amount" HeaderText="Amount">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="POStatus" HeaderText="Status">
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
                                                        <asp:BoundField DataField="MailSend" HeaderText="Email Status"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="HIGHLIGHT ROWS SHOWS MAIL SEND TO THE SUPPLIER.."
                                        ID="LBLREDMARK" Font-Bold="true" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div runat="server" id="Panel1" class="Display_None">
                                    <fieldset id="Fieldset3" runat="server" class="FieldSet" style="background-color: Silver">
                                        <table width="100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblCount" runat="server" CssClass="SubTitle" Text="Please Reorder Following Items"></asp:Label>
                                                </td>
                                                <td align="right" valign="top">
                                                    <a href='../Transactions/PurchaseOrderDtls.aspx' target="_blank" class="Info">Place
                                                        Order</a>
                                                    <asp:ImageButton ID="ImgBtnClose" runat="server" ImageUrl="~/Images/Icon/CloseButton.png"
                                                        ToolTip="Close" OnClick="ImgBtnClose_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <div id="divPrint" class="scrollableDiv" style="width: 98%">
                                                        <asp:GridView ID="GrdReorder" runat="server" ShowFooter="true" AutoGenerateColumns="False"
                                                            CaptionAlign="Top" AllowPaging="true" CssClass="mGrid" Width="100%" PageSize="5"
                                                            OnPageIndexChanging="GrdReorder_PageIndexChanging">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr.No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="6%" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ItemCode" HeaderText="Code">
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ItemName" HeaderText="Item">
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ReorderLavel" HeaderText="Reorder Level">
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" Width="50px" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" Width="50px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="AvilableQty" HeaderText="Reached Level">
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" Width="50px" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" Width="50px" />
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
                                </div>
                                <ajax:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" TargetControlID="Panel1"
                                    VerticalSide="Bottom" VerticalOffset="10" HorizontalSide="Right" HorizontalOffset="10"
                                    ScrollEffectDuration=".1" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
         
       
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

