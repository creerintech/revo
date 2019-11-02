<%@ Page Title="Project Scope" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Projectscope.aspx.cs" Inherits="Transactions_Projectscope" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<%@ Register Assembly="CrystalDecisions.ReportAppServer.ClientDoc, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
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

    <script language="javascript" type="text/javascript">
    function CheckOtherIsCheckedByGVID(rb) {
           var isChecked = rb.checked;
           var row = rb.parentNode.parentNode;

           var currentRdbID = rb.id;
           parent = document.getElementById("<%= GridTemplatePriority.ClientID %>");
           var items = parent.getElementsByTagName('input');

           for (i = 0; i < items.length; i++) {
           if (items[i].id != currentRdbID && items[i].type == "radio") {
           if (items[i].checked) {
                items[i].checked = false;                   
            }
        }
     }
   }
    </script>




    <link href="css/ui-lightness/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {
            $("#TXTKTO").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "MaterialRequisitionTemplate.aspx/GetAutoCompleteData",
                        data: "{'username':'" + extractLast(request.term) + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                focus: function () {
                    // prevent value inserted on focus
                    return false;
                },
                select: function (event, ui) {
                    var terms = split(this.value);
                    // remove the current input
                    terms.pop();
                    // add the selected item
                    terms.push(ui.item.value);
                    // add placeholder to get the comma-and-space at the end
                    terms.push("");
                    this.value = terms.join(", ");
                    return false;
                }
            });
            $("#TXTKTO").bind("keydown", function (event) {
                if (event.keyCode === $.ui.keyCode.TAB &&
                    $(this).data("autocomplete").menu.active) {
                    event.preventDefault();
                }
            })
            function split(val) {
                return val.split(/,\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }
        }
    </script>
    <script language="javascript" type="text/javascript">

        function SetZIndex(control, args) {

            // Set auto complete extender control's z-index to a high value

            // so it will appear on top of, not under, the ModalPopUp extended panel.

            control._completionListElement.style.zIndex = 99999999;

        }

    </script>
    <style>
        .AutoExtenderList
 {
   display:block;
   elevation:higher;
   position:relative;
   z-index:9999;
   border-bottom: dotted 1px #006699;
   cursor: pointer;
   color: Maroon;
 }
    </style>
    
    
    <%--<script language="javascript" type="text/javascript">
        //19 march for discount
        function Calculate_NetTotal() {
            var Vatamt = 0;
            var SGSTAmt = 0;
            var IGSTAmt = 0;
            var SubTotal = document.getElementById('<%= lblTotalAmt.ClientID%>');
//            var SpeDisPer = document.getElementById('<%= txtSpeDisPer.ClientID%>');
//            var SpeDisAmt = document.getElementById('<%= txtSpeDisAmt.ClientID%>');
//            var DisCountSub = document.getElementById('<%= txtDisCountSub.ClientID%>');
//            var Freight = document.getElementById('<%= txtFreight.ClientID%>');
//            var LadUnload = document.getElementById('<%= txtLadUnload.ClientID%>');
           // var Amount_BeforeVAT = document.getElementById('<%= txtAmount.ClientID%>');
            var vatper = document.getElementById('<%= txtvatper.ClientID%>');
            var vatamt = document.getElementById('<%= txtvatamt.ClientID%>');
            var SGSTPer = document.getElementById('<%= txtSGSTPer.ClientID%>');
            var SGSTAmt = document.getElementById('<%= txtSGSTAmt.ClientID%>');
            var IGSTPer = document.getElementById('<%= txtIGSTPer.ClientID%>');
            var IGSTAmt = document.getElementById('<%= txtIGSTAmt.ClientID%>');
            var TotalGST = document.getElementById('<%= txtTotalGST.ClientID%>');
            var TotalWithGSTAmt = document.getElementById('<%= txtToalWithGST.ClientID%>');
            var NetTotal = document.getElementById('<%= txtNetTotal.ClientID%>');
            var ChkCGST = document.getElementById('<%= ChkCGST.ClientID%>');
            var ChkSGST = document.getElementById('<%= ChkSGST.ClientID%>');
            var ChkIGST = document.getElementById('<%= ChkIGST.ClientID%>');
            var WithSpecialAmt = document.getElementById('<%= txtWithSpecialAmt.ClientID%>');
            
            if (SubTotal.value == "" || isNaN(SubTotal.value)) {
                SubTotal.value = 0;
            }
//            if (SpeDisPer.value == "" || isNaN(SpeDisPer.value)) {
//               SpeDisPer.value = 0;
//           }
//            if (SpeDisAmt.value == "" || isNaN(SpeDisAmt.value)) {
//                SpeDisAmt.value = 0;
//            }
//            if (DisCountSub.value == "" || isNaN(DisCountSub.value)) {
//                DisCountSub.value = 0;
//            }
//            if (Freight.value == "" || isNaN(Freight.value)) {
//                Freight.value = 0;
//            }
//            if (LadUnload.value == "" || isNaN(LadUnload.value)) {
//                LadUnload.value = 0;
//            }
//            if (Amount_BeforeVAT.value == "" || isNaN(Amount_BeforeVAT.value)) {
//                Amount_BeforeVAT.value = 0;
//            }
            if (WithSpecialAmt.value == "" || isNaN(WithSpecialAmt.value)) {
                WithSpecialAmt.value = 0;
            }
            if (vatper.value == "" || isNaN(vatper.value)) {
                vatper.value = 0;
            }
            if (vatamt.value == "" || isNaN(vatamt.value)) {
                vatamt.value = 0;
            }
            if (SGSTPer.value == "" || isNaN(SGSTPer.value)) {
                SGSTPer.value = 0;
            }
            if (SGSTAmt.value == "" || isNaN(SGSTAmt.value)) {
                SGSTAmt.value = 0;
            }
            if (IGSTPer.value == "" || isNaN(IGSTPer.value)) {
                IGSTPer.value = 0;
            }
            if (IGSTAmt.value == "" || isNaN(IGSTAmt.value)) {
                IGSTAmt.value = 0;
            }
            if (TotalGST.value == "" || isNaN(TotalGST.value)) {
                TotalGST.value = 0;
            }
            if (TotalWithGSTAmt.value == "" || isNaN(TotalWithGSTAmt.value)) {
                TotalWithGSTAmt.value = 0;
            }
            if (NetTotal.value == "" || isNaN(NetTotal.value)) {
                NetTotal.value = 0;
            }

            //Amount Before Discount Amount
           
            //Amount Before VAT Amount
          //  Amount_BeforeVAT.value = parseFloat(parseFloat(SubTotal.value)  - parseFloat(DisCountSub.value) + parseFloat(Freight.value) + parseFloat(LadUnload.value)).toFixed(2);
            //alert("Amount Before VAT:" +Amount_BeforeVAT.value);

//            if (SpeDisPer.value != "" && Amount_BeforeVAT.value != "") {
//                SpeDisAmt.value = parseFloat((Amount_BeforeVAT.value * SpeDisPer.value) / 100).toFixed(2);
//                //alert("spcl Disc :" +parseFloat(SpeDisAmt.value).toFixed(2));
//                document.getElementById('<%= txtWithSpecialAmt.ClientID%>').value = parseFloat(parseFloat(Amount_BeforeVAT.value) - parseFloat(SpeDisAmt.value)).toFixed(2); 
//            } 
            //Amount Before VAT Amount

            if (vatper.value != "" && !isNaN(SubTotal.value)) {
                vatamt.value = parseFloat((SubTotal.value * vatper.value) / 100).toFixed(2);
                    //alert("VAT Amount :" +vatamt.value);
                }


                if (SGSTPer.value != "" && !isNaN(SubTotal.value)) {
                    SGSTAmt.value = parseFloat((SubTotal.value * SGSTPer.value) / 100).toFixed(2);
                    //alert("VAT Amount :" +vatamt.value);
                }


                if (IGSTPer.value != "" && !isNaN(SubTotal.value)) {
                    IGSTAmt.value = parseFloat((SubTotal.value * IGSTPer.value) / 100).toFixed(2);
                    //alert("VAT Amount :" +vatamt.value);
                }


                //Amount Before Net Amount
                if (SGSTAmt.value != 0 && vatamt.value != 0) {
                    TotalGST.value = parseFloat(parseFloat(SGSTAmt.value) + parseFloat(vatamt.value));
                }
                if (IGSTAmt.value != 0)
            {
            TotalGST.value=parseFloat(parseFloat(IGSTAmt.value));
        }
        if (SGSTAmt.value != 0 && vatamt.value != 0) {
            TotalWithGSTAmt.value = parseFloat(parseFloat(SubTotal.value) - parseFloat(DisCountSub.value) + parseFloat(Freight.value) + parseFloat(LadUnload.value) + parseFloat(vatamt.value) + parseFloat(SGSTAmt.value) - parseFloat(SpeDisAmt.value)).toFixed(2);
        }
        if (IGSTAmt.value != 0) {
            TotalWithGSTAmt.value = parseFloat(parseFloat(SubTotal.value) - parseFloat(DisCountSub.value) + parseFloat(Freight.value) + parseFloat(LadUnload.value) + parseFloat(IGSTAmt.value) - parseFloat(SpeDisAmt.value)).toFixed(2);
         }
//         if (SpeDisAmt.value != "" && TotalWithGSTAmt.value != "") {
//             SpeDisAmt.value = parseFloat(TotalGST.value).toFixed(2);
//                //alert("spcl Disc :" +parseFloat(SpeDisAmt.value).toFixed(2));
//            }

            NetTotal.value = parseFloat( parseFloat(SubTotal.value)-parseFloat(SpeDisAmt.value) - parseFloat(DisCountSub.value) + parseFloat(Freight.value) + parseFloat(LadUnload.value) +parseFloat(SGSTAmt.value)+parseFloat(vatamt.value)+parseFloat(IGSTAmt.value)).toFixed(2);
            //alert("Net Amount :" +NetTotal.value);

            NetTotal.value = Math.round(NetTotal.value);
            //NumbertoWord();
            // CalculateNetTotal();
        }
    </script>--%>

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
            <asp:Button ID="BtnPopMail" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlInfoMail" runat="server" CssClass="ModelPopUpPanelBackGroundMail"
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

                                                <asp:DropDownList ID="TXTKTO" runat="server" CssClass="TextBox" Width="550px" AutoPostBack="false"  ></asp:DropDownList>

                                              <%--  <asp:TextBox runat="server" ID="TXTKTO" CssClass="TextBox" Width="550px" AutoPostBack="true" 
                                                    OnTextChanged="TXTKTO_TextChanged"></asp:TextBox>--%>
                                               <%-- <ajax:RoundedCornersExtender ID="RCCTXTKTO" runat="server" TargetControlID="TXTKTO"
                                                    Corners="All" Radius="6" BorderColor="Gray">
                                                </ajax:RoundedCornersExtender>
                                                <ajax:TextBoxWatermarkExtender ID="WMTXTKTO" runat="server" TargetControlID="TXTKTO"
                                                    WatermarkText="To" WatermarkCssClass="water" />--%>


                                              <%--  <ajax:AutoCompleteExtender ServiceMethod="SearchCustomers" CompletionListCssClass="AutoExtenderList"
    MinimumPrefixLength="2"
    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
    TargetControlID="TXTKTO"
    ID="AutoCompleteExtender3" runat="server" FirstRowSelected = "false">
</ajax:AutoCompleteExtender>--%>
                                             
                                     <%--   <ajax:TextBoxWatermarkExtender ID="txtWE1" runat="server" TargetControlID="TXTKTO"
                                            WatermarkText="Enter Customer Here" WatermarkCssClass="TextBox_SearchWaterMark">
                                        </ajax:TextBoxWatermarkExtender>--%>



                                                <asp:RegularExpressionValidator ID="REV2" runat="server" Display="None" ErrorMessage="Please Enter Valid Email ID..!"
                                                    ControlToValidate="TXTKTO" ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                                                    ValidationGroup="Add">
                                                </asp:RegularExpressionValidator>
                                                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True"
                                                    TargetControlID="REV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                </ajax:ValidatorCalloutExtender>
                                               
                                                
                                                
                                                
                                                
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
                runat="server" TargetControlID="BtnPopMail" PopupControlID="pnlInfoMail" DropShadow="true">
            </ajax:ModalPopupExtender>
            <%---------------------------------------------------------------------------------------------------------------------%>
            Search for Project Scope :
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
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="Server">
 Project Scope
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="8">
                <tr>
                    <td>
                        <fieldset id="F1" runat="server" width="100%">
                            <table width="100%" cellspacing="8">
                                <div>
                                    <tr>
                                        <td class="Label">
                                            Project Scope  No :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtReqNo" runat="server" CssClass="TextBox" Visible="false" Width="180px"></asp:TextBox>
                                            <asp:Label ID="lblReqNo" runat="server" CssClass="Label2" Text=""></asp:Label>
                                        </td>
                                        <td class="Label">
                                            Project Scope Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtReqDate" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CssClass="Imagebutton"
                                                ImageUrl="~/Images/Icon/DateSelector.png" />
                                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                PopupButtonID="ImageButton2" TargetControlID="txtReqDate">
                                            </ajax:CalendarExtender>
                                        </td>
                                       
                                        <td class="Label">
                                            From :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCafe" runat="server" CssClass="TextBox" Visible="false" Width="150px"></asp:TextBox>
                                            <asp:Label ID="lblCafe" runat="server" CssClass="Label2" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Label">
                                            Project :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCostCentre" runat="server" CssClass="ComboBox" Width="142px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCostCentre"
                                                Display="None" ErrorMessage="Please Select Project"  SetFocusOnError="True"
                                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                            </ajax:ValidatorCalloutExtender>
                                        </td>
                                        <td class="Label">
                                           System Name :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsystemname" runat="server" CssClass="TextBox" Width="182px"></asp:TextBox>
                                           
                                            <asp:RequiredFieldValidator ID="Req_Name" runat="server" ControlToValidate="txtsystemname"
                                                Display="None" ErrorMessage="Please Select Template" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="AddT"></asp:RequiredFieldValidator>
                                            <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" TargetControlID="Req_Name"
                                                WarningIconImageUrl="~/Images/Icon/Warning.png">
                                            </ajax:ValidatorCalloutExtender>
                                        </td>
                                     
                                      
                                    </tr>
                                    
                                </div>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset id="F2" runat="server" width="100%">
                            <table width="100%" cellspacing="8">
                                <tr>
                                    <td colspan="2">
                                        <div id="DivDisp" runat="server" class="ScrollableDiv_FixHeightWidth4" width="90%">




                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label16" Text="Valve Type:-" runat="server"></asp:Label>
                                                    </td>
                                                  
                                                    <td>
                                                        <asp:DropDownList ID="drpvalvetype" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                            Font-Size="Medium">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 30px;"></td>

                                                    <td>
                                                        <asp:Label ID="Label17" Text="Valve Type:-" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="drpvalvesize" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                            Font-Size="Medium">
                                                        </asp:DropDownList>

                                                    </td>
                                                    <td style="width: 30px;"></td>

                                                    <td>
                                                        <asp:Label ID="Label5" Text="Valve Type" runat="server"></asp:Label>

                                                    </td>

                                                    <td>
                                                        <asp:DropDownList ID="drpvalveclass" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                            Font-Size="Medium">
                                                        </asp:DropDownList>

                                                    </td>
                                                    <td style="width: 30px;"></td>
                                                    <td>
                                                        <asp:Label ID="Label6" Text="Valve Type" runat="server"></asp:Label>
                                                    </td>
                                                    <td>

                                                        <asp:DropDownList ID="drpvalveoperator" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                            Font-Size="Medium">
                                                        </asp:DropDownList>

                                                    </td>


                                                </tr>



                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" Text="Valve Type" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtvalvetagno" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    </td>


                                                    <td style="width: 30px;"></td>

                                                    <td>
                                                        <asp:Label ID="Label10" Text="Valve Type" runat="server"></asp:Label>

                                                    </td>

                                                    <td>
                                                        <asp:DropDownList ID="drpiterlock" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                            Font-Size="Medium">
                                                        </asp:DropDownList>
                                                    </td>


                                                    <td style="width: 30px;"></td>

                                                    <td>
                                                        <asp:Label ID="Label11" Text="Valve Type" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="drphandwheelsize" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                            Font-Size="Medium">
                                                        </asp:DropDownList>
                                                    </td>

                                                    <td style="width: 30px;"></td>

                                                    <td><asp:Label ID="Label9" Text="Valve Type" runat="server"></asp:Label></td>
                                                    <td> <asp:DropDownList ID="drplever" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                Font-Size="Medium">
                                            </asp:DropDownList></td>


                                                      <td style="width: 30px;"></td>
                                                    <td>
                                                           <asp:Label ID="Label12" Text="Valve Type" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtqty" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    </td>

                                                </tr>

                                            </table>

                                              


                                           
                                          
                                           

                                            
                                            

                                             <asp:Label ID="Label14" Text="Valve Type" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtunitprice" runat="server" CssClass="TextBox"></asp:TextBox>
                                             <asp:Label ID="Label15" Text="Valve Type" runat="server"></asp:Label>
                                            <asp:TextBox ID="txttotalprice" runat="server" CssClass="TextBox"></asp:TextBox>

                                              <asp:GridView ID="grditem" runat="server" CssClass="mGrid" AutoGenerateColumns="False" DataKeyNames="CustomerId" DataSourceID="SqlDataSource1" OnRowDataBound="grditem_RowDataBound">
                                                
                                                <Columns>

                                                    

                                                       


                                                      
                                                   


                                                    
                                                </Columns>
                                                
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Petro_2018ConnectionString %>" SelectCommand="SELECT  top 1 [CustomerId], [CustomerCode] FROM [CustomerMaster]"></asp:SqlDataSource>
                                            <asp:Button ID="addtogrid" runat="server" CssClass="button" OnClick="addtogrid_Click" />
                                            <caption>
                                                gdkjfghdfjhgdkjfhgd
                                                <hr />
                                                <asp:GridView ID="GrdRequisition" runat="server" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="#" OnDataBound="GrdRequisition_DataBound" OnRowCommand="GrdRequisition_RowCommand" OnRowDataBound="GrdRequisition_RowDataBound" OnRowDeleting="GrdRequisition_RowDeleting" ShowFooter="true">
                                                    <Columns>
                                                        <%-- 0--%>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="BtnAdd" runat="server" CommandName="InsertNew" ImageUrl="~/Images/Icon/Gridadd.png" OnClick="BtnAdd_Click" TabIndex="5" ValidationGroup="Add" />
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                            <FooterStyle CssClass="" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <%-- 1--%>
                                                        <asp:TemplateField HeaderText="All" Visible="false">
                                                            <HeaderTemplate>
                                                                <asp:Button ID="BtnShowAllAvQty" runat="server" CssClass="button" OnClick="BtnShowAllAvQty_Click" Text="GetAllAvQty" Visible="false" />
                                                                <asp:CheckBox ID="GrdSelectAllHeader" runat="server" AutoPostBack="true" OnCheckedChanged="GrdSelectAllHeader_CheckedChanged" Visible="false" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Button ID="BtnShowAvQty" runat="server" CssClass="button" OnClick="BtnShowAvQty_Click" Text="GetAvQty" />
                                                                <asp:CheckBox ID="GrdSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="GrdSelectAll_CheckedChanged" Visible="false" />
                                                                <asp:ImageButton ID="ImgBtnBlocked" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" ImageUrl="~/Images/Icon/bolcked.png" ToolTip="Item Cancelled" />
                                                                <asp:ImageButton ID="ImgBtnDelete" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record..!" TargetControlID="ImgBtnDelete">
                                                                </ajax:ConfirmButtonExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <%-- 2--%>
                                                        <asp:BoundField DataField="ItemCode" HeaderText="Code">
                                                        <HeaderStyle CssClass="Display_None" />
                                                        <ItemStyle CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <%-- 3--%>
                                                        <asp:TemplateField HeaderText="Category">
                                                            <ItemTemplate>
                                                                <ajax:ComboBox ID="ddlCategory" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="true" CaseSensitive="false" CssClass="CustomComboBoxStyle" DropDownStyle="DropDown" ItemInsertLocation="Append" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" RenderMode="Inline" TabIndex="4" Width="120px">
                                                                </ajax:ComboBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <FooterStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <%--4--%>
                                                        <asp:TemplateField HeaderText="SubCategory">
                                                            <ItemTemplate>
                                                                <ajax:ComboBox ID="ddlSubCategory" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="true" CaseSensitive="false" CssClass="CustomComboBoxStyle" DropDownStyle="DropDown" ItemInsertLocation="Append" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" RenderMode="Inline" TabIndex="4" Width="150px">
                                                                </ajax:ComboBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <FooterStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <%-- 5--%>
                                                        <asp:TemplateField HeaderText="Particulars">
                                                            <ItemTemplate>
                                                                <ajax:ComboBox ID="ddlItem" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="true" CaseSensitive="false" CssClass="Display_None" DropDownStyle="DropDown" Font-Size="Medium" Height="25px" ItemInsertLocation="Append" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" RenderMode="Inline" TabIndex="4" Visible="false" Width="270px">
                                                                </ajax:ComboBox>
                                                                <%--THIS START HERE--%>
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="TxtItemName" runat="server" AutoPostBack="True" CssClass="search_List" OnTextChanged="TxtItemName_TextChanged" Text='<%# Eval("ItemName") %>' ToolTip='<%# Eval("ItemToolTip") %>' Width="292px"></asp:TextBox>
                                                                        <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemName" runat="server" CompletionInterval="100" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="20" FirstRowSelected="true" ServiceMethod="GetCompletionItemNameList" ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="TxtItemName" UseContextKey="True">
                                                                        </ajax:AutoCompleteExtender>
                                                                        <ajax:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="TxtItemName" WatermarkCssClass="water" WatermarkText="Type Item Name" />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                <%--THIS END HERE--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <%-- 6--%>
                                                        <asp:TemplateField HeaderText="Description" Visible="false">
                                                            <ItemTemplate>
                                                                <ajax:ComboBox ID="ddlItemDescription" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="true" CaseSensitive="false" CssClass="CustomComboBoxStyle" DropDownStyle="DropDown" Height="25px" ItemInsertLocation="Append" OnSelectedIndexChanged="ddlItemDescription_SelectedIndexChanged" RenderMode="Inline" TabIndex="4" Width="200px">
                                                                </ajax:ComboBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlItemDescription" Display="None" Enabled="true" ErrorMessage="Please Select Description" InitialValue=" --Select ItemDescription--" SetFocusOnError="true" ValidationGroup="Add">
                                                            </asp:RequiredFieldValidator>
                                                                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="true" TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                                </ajax:ValidatorCalloutExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <%-- 7--%>
                                                        <asp:TemplateField HeaderText="QTY ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtqty1" runat="server" CssClass="TextBox"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Price ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtunitprice1" runat="server" CssClass="TextBox"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Price ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txttotalprice1" runat="server" CssClass="TextBox"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <%--Add Priority To Form--%><%--22--%>
                                                        <asp:TemplateField HeaderText="Set Priority">
                                                            <ItemTemplate>
                                                                <div style="">
                                                                    <asp:Image ID="ImgBtnEdit" runat="server" CssClass="Imagebutton" ImageUrl="~/Images/Icon/Gridadd.png" TabIndex="4" ToolTip="Set Priority" />
                                                                    <ajax:PopupControlExtender ID="popup" runat="server" CommitProperty="Value" DynamicServicePath="" Enabled="True" ExtenderControlID="" PopupControlID="PnlGrid" Position="Right" TargetControlID="ImgBtnEdit">
                                                                    </ajax:PopupControlExtender>
                                                                    <asp:Panel ID="PnlGrid" runat="server">
                                                                        <div class="PopupPanel">
                                                                            <asp:UpdatePanel ID="ProcessEntry" runat="server">
                                                                                <ContentTemplate>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <div id="DivTabControl" runat="server" class="scrollableDiv">
                                                                                                    <table style="width: 100%">
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <div id="Div1" runat="server" class="scrollableDiv">
                                                                                                                    <fieldset id="FS_TemplatePriorityDetails" runat="server" class="FieldSet" style="width: 100%">
                                                                                                                        <legend id="Lgd_TemplatePriorityDetails" runat="server" class="legend">Select Priority</legend>
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                                            <ContentTemplate>
                                                                                                                                <asp:GridView ID="GridTemplatePriority" runat="server" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="#">
                                                                                                                                    <Columns>
                                                                                                                                        <asp:TemplateField HeaderText="#" Visible="False">
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:TemplateField>
                                                                                                                                        <asp:BoundField DataField="PriorityId" HeaderText="PriorityId">
                                                                                                                                        <HeaderStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                        <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                        </asp:BoundField>
                                                                                                                                        <asp:TemplateField HeaderText="Select">
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:RadioButton ID="RBPriority" runat="server" GroupName="Prioritygroup" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                                                                                                                            </ItemTemplate>
                                                                                                                                            <HeaderStyle Width="10px" Wrap="False" />
                                                                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                                                        </asp:TemplateField>
                                                                                                                                        <asp:BoundField DataField="Prioriry" HeaderText="Priority">
                                                                                                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                        </asp:BoundField>
                                                                                                                                    </Columns>
                                                                                                                                </asp:GridView>
                                                                                                                            </ContentTemplate>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </fieldset>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center">
                                                                                                <asp:CheckBox ID="ChkShowProcess" runat="server" AutoPostBack="True" CssClass="CheckBox" meta:resourcekey="ChkShowProcessResource1" OnCheckedChanged="ChkShowProcess_CheckedChanged" Text="Add Priority" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="Display_None" Wrap="true" />
                                                            <ItemStyle CssClass="Display_None" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:TemplateField>
                                                        <%--23--%>
                                                    </Columns>
                                                    <FooterStyle CssClass="ftr" />
                                                    <PagerStyle CssClass="pgr" />
                                                </asp:GridView>
                                                jrwoihjr;lewymhjortis
                                                <hr />
                                            </caption>

                                          
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td></td>--%>
                                    <td valign="middle">
                                        <asp:Label runat="server" ID="Lblremark" Text="REMARK :" CssClass="Label4"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TXTREMARK" runat="server" CssClass="TextBox" TextMode="MultiLine"
                                            Width="560px" TabIndex="6"></asp:TextBox>
                                    </td>
                                 <%--   <td colspan="1" class="Label" align="right" >
                                        <asp:Button CssClass="button" ID="BtnRefresh" runat="server" Text="Amount" ToolTip="Get Total Requisition Amount"
                                            OnClick="BtnRefresh_Click" TabIndex="7" />
                                        Total Amount :
                                        <asp:TextBox ID="lblTotalAmt" runat="server" CssClass="TextBoxNumericReadOnly" Enabled="false"></asp:TextBox>
                                    </td>--%>
                                </tr>
                              
                                
                         <%--       <tr>
                                    
                                    <td class="Label"  colspan="5">
                                        <span lang="EN-IN">&nbsp;&nbsp;&nbsp; &nbsp;NetTotal <span lang="EN-IN">: </span>
                                            <asp:TextBox ID="txtNetTotal" runat="server" CssClass="TextBoxNumeric" TabIndex="1"
                                                Width="150px"  Enabled="false"></asp:TextBox>
                                        </span>
                                    </td>
                                    </td>
                                      <td class="Label">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                     <td>
                                        &nbsp;
                                    </td>
                                </tr>--%>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset id="F3" runat="server" width="100%">
                            <table width="100%" cellspacing="8">
                                <tr>
                                    <td align="center" colspan="6">
                                        <table width="25%">
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="BtnUpdate" runat="server" CausesValidation="true" TabIndex="8" CssClass="button"
                                                        OnClick="BtnUpdate_Click" Text="Update" ValidationGroup="Add" />
                                                    <ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
                                                        TargetControlID="BtnUpdate">
                                                    </ajax:ConfirmButtonExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnSave" runat="server" CausesValidation="false" TabIndex="8" CssClass="button"
                                                         OnClick="BtnSave_Click1" Text="Save" ValidationGroup="Add" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" TabIndex="8" CssClass="button"
                                                        OnClick="BtnCancel_Click" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <table cellspacing="8" width="100%">
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
                </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <asp:GridView ID="ReportGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False" Visible="false"
                                        CssClass="mGrid" DataKeyNames="#,POTotQty,IndentTotQty" OnRowCommand="ReportGrid_RowCommand"
                                        OnRowDeleting="ReportGrid_RowDeleting" OnPageIndexChanging="ReportGrid_PageIndexChanging"
                                        OnRowDataBound="ReportGrid_RowDataBound" OnDataBound="ReportGrid_DataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblEstimateId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageAccepted" runat="server" TabIndex="29" CommandArgument='<%# Eval("#") %>'
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
                                                    </ajax:ConfirmButtonExtender>
                                                    <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RS"%>&PDFFlag=<%="NOPDF"%>'
                                                        target="_blank">
                                                        <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                                                            TabIndex="29" ToolTip="Print Indent Register" />
                                                    </a>
                                                    <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RS"%>&PDFFlag=<%="PDF"%>'
                                                        target="_blank">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png" TabIndex="29"
                                                            ToolTip="PDF of Indent Register" />
                                                    </a>
                                                    <asp:ImageButton ID="ImgNMail" runat="server" CommandArgument='<%# Eval("#") %>'
                                                        CommandName="MailIndent" ImageUrl="~/Images/Icon/Email-Blue.jpg" ToolTip="Mail Indent" />
                                                    <asp:ImageButton ID="IMGDELETEMR" runat="server" CommandArgument='<%# Eval("#") %>'
                                                        CommandName="DeleteMR" ImageUrl="~/Images/New Icon/Cancel__Black.png" ToolTip="Delete" />
                                                    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                        TargetControlID="IMGDELETEMR">
                                                    </ajax:ConfirmButtonExtender>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RequisitionNo" HeaderText="Indent No.">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TemplateTitle" HeaderText="Site">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RequisitionDate" HeaderText="Date">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IsCancel" HeaderText="Cancel">
                                                <HeaderStyle />
                                                <ItemStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ReqStatus" HeaderText="Indent Status">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                                <FooterStyle Wrap="False" CssClass="Display_None" />
                                            </asp:BoundField>
                                            <%--<asp:BoundField DataField="POSTATUS" HeaderText="PO Status">
<HeaderStyle Wrap="False" />
<ItemStyle Wrap="False" />
<FooterStyle Wrap="False" CssClass="Display_None" />
</asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="P.O. Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="GRDPOSTATUS" runat="server" Text='<%# Eval("POSTATUS") %>' ToolTip='<%# Eval("POINFO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmailStatus" HeaderText="Email Status">
                                                <HeaderStyle />
                                                <ItemStyle />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

