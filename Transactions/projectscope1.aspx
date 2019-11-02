<%@ Page Title="Project Scope" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="projectscope1.aspx.cs" Inherits="Transactions_projectscope1" %>



<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
    <style type="text/css">
        .water {
            width: 310px;
            border: 0;
            background: #FFF url(../Images/MasterPages/input.gif) no-repeat;
            padding: 4px;
            font-weight: bold;
            margin: 0 0 0 3px;
            color: Gray;
        }
   
        @media print {

        #break_div {
                page-break-after: always;
                page-break-before: always;

            }
        .table2 {
                margin-top: 10px;
            }
        div.total_a_plus_b{
          
            page-break-before: always;

        }
        }

 

    </style>



    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">


        function btnsummery_click() {

            $("[id*=txttotalprice1]").each(function () {
                grandTotal = grandTotal + parseFloat($(this).val());
            });
            $("[id*=lblGrandTotal1]").html(grandTotal.toString());
            $("[id*=hdn1]").val(grandTotal.toString());
            $("[id*=Lblgggtotal]").html(Number($("[id*=lblpdprice]").html())  + Number($("[id*=lblGrandTotal1]").html()));
            $("[id*=hdn4]").val(Number($("[id*=lblpdprice]").html())  + Number($("[id*=lblGrandTotal1]").html()));
        }

        function txttotalprice_function() {
            var grandTotal = 0;
            $("[id*=txttotalprice1]").each(function () {
                grandTotal = grandTotal + parseFloat($(this).val());
            });
            $("[id*=lblGrandTotal1]").html(grandTotal.toString());
            $("[id*=hdn1]").val(grandTotal.toString());
            $("[id*=Lblgggtotal]").html(Number($("[id*=lblpdprice]").html()) + Number($("[id*=lblGrandTotal1]").html()));
            $("[id*=hdn4]").val(Number($("[id*=lblpdprice]").html())  + Number($("[id*=lblGrandTotal1]").html()));
        }
        function txttotalpriceitem_function() {
            var demo = 0;
            $("[id*=txttotalpriceitem1]").each(function () {
                demo = demo + parseFloat($(this).val());
            });
            $("[id*=lblGrandTotalitem1]").html(demo.toString());
            $("[id*=hdn2]").val(demo.toString());
            $("[id*=Lblgggtotal]").html(Number($("[id*=lblpdprice]").html())  + Number($("[id*=lblGrandTotal1]").html()));
            $("[id*=hdn4]").val(Number($("[id*=lblpdprice]").html())  + Number($("[id*=lblGrandTotal1]").html()));

        }



        function txttotalpricepd_function() {
            var sum = 0;
            $("[class*=txtpdprice123]").each(function () { sum += +$(this).val(); });
            $("[id*=lblpdprice]").html(sum);
            $("[id*=hdn3]").val(sum.toString());
            $("[id*=Lblgggtotal]").html(Number($("[id*=lblpdprice]").html())  + Number($("[id*=lblGrandTotal1]").html()));
            $("[id*=hdn4]").val(Number($("[id*=lblpdprice]").html())  + Number($("[id*=lblGrandTotal1]").html()));


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
        .AutoExtenderList {
            display: block;
            elevation: higher;
            position: relative;
            z-index: 9999;
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }
    </style>




    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlprint.ClientID %>");
            var printWindow = window.open('', '', 'height=800,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>



       <script type="text/javascript">
        function PrintPaneloffer() {
            var panel = document.getElementById("<%=Panel3.ClientID %>");
            var printWindow = window.open('', '', 'height=800,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>


    <script type="text/javascript">
        function PrintPanel1() {
            var panel = document.getElementById("<%=Panel1.ClientID %>");
            var printWindow = window.open('', '', 'height=800,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>


    <script type="text/javascript">
        function PrintPanel2() {
            var panel = document.getElementById("<%=Panel2.ClientID %>");
            var printWindow = window.open('', '', 'height=800,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>


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
                        <td>&nbsp;<asp:Label ID="Label1" Text="Revo MMS - Mail" runat="server" ForeColor="white"
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
                                            AutoPostBack="true">
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

                                                <asp:DropDownList ID="TXTKTO" runat="server" CssClass="TextBox" Width="550px" AutoPostBack="false"></asp:DropDownList>

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
                                            ValidationGroup="AddMail" CausesValidation="true" />
                                        &nbsp; &nbsp;<asp:Button ID="PopUpNoMail" Text="CANCEL" runat="server" CssClass="button"
                                            CommandName="no" />
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
                Width="292px" AutoPostBack="True"></asp:TextBox>
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
                                        <td class="Label">Project Scope  No :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtReqNo" runat="server" CssClass="TextBox" Visible="false" Width="180px"></asp:TextBox>
                                            <asp:Label ID="lblReqNo" runat="server" CssClass="Label2" Text=""></asp:Label>
                                        </td>
                                        <td class="Label">Project Scope Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtReqDate" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CssClass="Imagebutton"
                                                ImageUrl="~/Images/Icon/DateSelector.png" />
                                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                PopupButtonID="ImageButton2" TargetControlID="txtReqDate">
                                            </ajax:CalendarExtender>
                                        </td>

                                        <td class="Label"></td>
                                        <td>
                                            <asp:TextBox ID="txtCafe" runat="server" CssClass="TextBox" Visible="false" Width="150px"></asp:TextBox>
                                            <asp:Label ID="lblCafe" runat="server" CssClass="Label2" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Label">Project :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCostCentre" AutoPostBack="true" runat="server" CssClass="ComboBox" Width="250px" OnSelectedIndexChanged="ddlCostCentre_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCostCentre"
                                                Display="None" ErrorMessage="Please Select Project" SetFocusOnError="True"
                                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                            </ajax:ValidatorCalloutExtender>
                                        </td>



                                        <td class="Label">Revised :
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkrevised" runat="server" CssClass="CheckBox" />
                                        </td>



                                    </tr>






                                    <tr>
                                        <td class="Label">Updated BY :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblupdatedby" runat="server"></asp:Label>
                                        </td>



                                        <td class="Label">Purchase order date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpurchaseordewrdate" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CssClass="Imagebutton"
                                                ImageUrl="~/Images/Icon/DateSelector.png" />
                                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                PopupButtonID="ImageButton1" TargetControlID="txtpurchaseordewrdate">
                                            </ajax:CalendarExtender>
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


                                          
   <%--<asp:Button ID="btndemo" runat="server" CssClass="button" OnClick="btndemo_Click" />
                                            <asp:Literal ID = "ltTable" runat = "server" />--%>

                                            <asp:Label ID="lbl1" runat="server" Text="(A)" Font-Bold="true" Font-Size="X-Large"></asp:Label>

                                            <asp:GridView ID="Gridview1" CssClass="mGrid" OnRowDeleting="Gridview1_RowDeleting" runat="server" ShowFooter="true" AutoGenerateColumns="false" OnRowDataBound="Gridview1_RowDataBound" OnRowCommand="Gridview1_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageAccepted" runat="server" TabIndex="29" CommandArgument='<%# Eval("RowNumber") %>'
                                                                CommandName="Delete" ImageUrl="~/Images/New Icon/GridDelete.png" ToolTip="Delete " />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Sr No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl1" runat="server" Text='<%#  Eval("RowNumber") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>





                                                    <asp:TemplateField HeaderText="System Name">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpsystemname" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium">
                                                            </asp:DropDownList>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="System No ">
                                                        <ItemTemplate>


                                                            <asp:TextBox ID="txtsystem" runat="server" Width="80px" Text='<%#  Eval("systemno") %>' CssClass="TextBox"></asp:TextBox>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Valve Type ">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpvalvetype" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="ButtonAdd" runat="server" Text="Add  New  Row" BackColor="#00ffcc" ToolTip="Add  New  Row" Height="30px"
                                                                OnClick="ButtonAdd_Click" />


                                                        </FooterTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Valve Size ">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpvalvesize" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <FooterTemplate>


                                                            <asp:Button ID="Button5" Class="button55" runat="server" BackColor="#00ffcc" Text="Same as Above " Height="30px" ToolTip="Same As Above "
                                                                OnClick="Button5_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Valve Class ">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpvalveclass" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>





                                                    <asp:TemplateField HeaderText="Valve Operator ">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpvalveoperator" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Valve Tag No. ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtvalvetagno" runat="server" Text='<%#  Eval("valvetagno") %>' CssClass="TextBox"></asp:TextBox>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Interlock ">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpiterlock" runat="server" DropDownStyle="DropDown" AutoPostBack="true" OnSelectedIndexChanged="drpiterlock_SelectedIndexChanged"
                                                                Font-Size="Medium">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Keys ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtKeys" runat="server" Text='<%#  Eval("Keys") %>' CssClass="TextBox"></asp:TextBox>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Handwheel ">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drphandwheelsize" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Lever ">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drplever" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Currency ">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpCurrency" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Qty ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtqty" runat="server" Text="1" CssClass="TextBox"></asp:TextBox>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Unit Price ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtunitprice" AutoPostBack="true" runat="server" Text='<%#  Eval("unitprice") %>' OnTextChanged="txtunitprice_TextChanged" CssClass="TextBox"></asp:TextBox>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Total Price ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttotalprice1" onkeyup="txttotalprice_function()" runat="server" Text='<%#  Eval("totalprice") %>' CssClass="TextBox"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>Total(A):&nbsp;</b>
                                                            <asp:Label ID="lblGrandTotal1" runat="server" />

                                                        </FooterTemplate>
                                                    </asp:TemplateField>












                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdn1" runat="server" />
                                            <asp:HiddenField ID="hdn2" runat="server" />
                                            <asp:HiddenField ID="hdn3" runat="server" />
                                            <asp:HiddenField ID="hdn4" runat="server" />



                                            <div style="margin-top: 150px; text-align: center;">
                                                <asp:Button ID="btnsummery" runat="server" OnClick="btnsummery_Click" Text="Show Summary" Width="100px" CssClass="button" />
                                                <asp:GridView ID="Grisummery" HeaderStyle-HorizontalAlign="Center" EditRowStyle-HorizontalAlign="Center" CssClass="mGrid" runat="server" Width="80%" AutoGenerateColumns="true" OnRowDataBound="Grisummery_RowDataBound">
                                                </asp:GridView>



                                            </div>







                                            <div style="margin-top: 150px;">


                                                <asp:Label ID="Label19" runat="server" Text="(B)" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                                                <asp:GridView ID="grdpd" OnRowDeleting="grdpd_RowDeleting" runat="server" AllowPaging="True" ShowFooter="true" AutoGenerateColumns="False" Visible="true" OnRowDataBound="grdpd_RowDataBound" OnRowCommand="grdpd_RowCommand"
                                                    CssClass="mGrid">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageAccepted" runat="server" TabIndex="29" CommandArgument='<%# Eval("pd") %>'
                                                                    CommandName="Delete" ImageUrl="~/Images/New Icon/GridDelete.png" ToolTip="Delete " />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Description ">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="drppd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drppd_SelectedIndexChanged"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" />

                                                            <FooterTemplate>
                                                                <asp:Button ID="btnpdadd" runat="server" Text="+"
                                                                    OnClick="btnpdadd_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                         <asp:TemplateField HeaderText="Details">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtpdDetails"  CssClass="TextBox" runat="server" Text='<%# Eval("Details") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                          
                                                        </asp:TemplateField>




                                                         <asp:TemplateField HeaderText="Currency ">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="drppdCurrency" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Unit Price ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtpdprice" class="txtpdprice123" onkeyup="txttotalpricepd_function()" runat="server" Text='<%# Eval("uprice") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <b>Total(C):&nbsp;</b>
                                                                <asp:Label ID="lblpdprice" runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>






                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                          
                                          
                                               

                                            <div style="margin-top: 150px;">
                                                <asp:Label ID="Label18" runat="server" Text="(D (OPTIONAL ITEM))" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                                                <asp:GridView ID="grditem" runat="server" OnRowDeleting="grditem_RowDeleting" CssClass="mGrid" AutoGenerateColumns="false" OnRowDataBound="grditem_RowDataBound" ShowFooter="true" OnRowCommand="grditem_RowCommand">
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageAccepted" runat="server" TabIndex="29" CommandArgument='<%# Eval("RowNumber") %>'
                                                                    CommandName="Delete" ImageUrl="~/Images/New Icon/GridDelete.png" ToolTip="Delete " />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Sr No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl1" runat="server" Text='<%#  Eval("RowNumber") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Itemname">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlitem" runat="server" CssClass="ComboBox"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />

                                                            <FooterStyle HorizontalAlign="Center" />

                                                            <FooterTemplate>
                                                                <asp:Button ID="ButtonAdd123" runat="server" Text="+"
                                                                    OnClick="ButtonAdd_Click1" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>





                                                        <asp:TemplateField HeaderText="Qty ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtqty" runat="server" Text='<%# Eval("qty") %>' CssClass="TextBox"></asp:TextBox>
                                                            </ItemTemplate>



                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Unit Price ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtunitprice" runat="server" Text='<%# Eval("unitprice") %>' CssClass="TextBox"></asp:TextBox>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Currency ">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="drpCurrency" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Total Price ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txttotalpriceitem1" onkeyup="txttotalpriceitem_function()" runat="server" Text='<%# Eval("total") %>' CssClass="TextBox"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <b>Total(B):&nbsp;</b>
                                                                <asp:Label ID="lblGrandTotalitem1" runat="server" />
                                                            </FooterTemplate>


                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>

                                            </div>

                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>

                                    <td>
                                        <asp:Label runat="server" ID="Lblremark" Text="REMARK :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TXTREMARK" runat="server" CssClass="TextBox" TextMode="MultiLine"
                                            Width="360px"></asp:TextBox>
                                    </td>



                                    <td style="float: left; margin-left: -420px;">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="true" ForeColor="#ff0000" Font-Size="Larger" Text="GrandTotal (A+B) :"></asp:Label>
                                    </td>
                                    <td style="float: left; margin-left: -230px;">
                                        <asp:Label ID="Lblgggtotal" runat="server" Font-Bold="true" ForeColor="#ff0000" Font-Size="Larger" Text="0"></asp:Label>
                                    </td>




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
                                                        Text="Update" ValidationGroup="Add" OnClick="BtnUpdate_Click" />
                                                    <ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
                                                        TargetControlID="BtnUpdate">
                                                    </ajax:ConfirmButtonExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnSave" runat="server" CausesValidation="false" TabIndex="8" CssClass="button"
                                                        Text="Save" ValidationGroup="Add" OnClick="BtnSave_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" TabIndex="8" CssClass="button"
                                                        Text="Cancel" OnClick="BtnCancel_Click" />
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
                                    <asp:GridView ID="ReportGrid" runat="server"  AutoGenerateColumns="False" Visible="true" OnRowCommand="ReportGrid_RowCommand" 
                                        CssClass="mGrid" DataKeyNames="#,project,pdate">
                                        <Columns>


                                              <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgOrdersShow" runat="server" OnClick="Show_Hide_OrdersGrid"
                                                            ImageUrl="~/images/plus.png" CommandArgument="Show" />
                                                        <asp:Panel ID="pnlOrders" runat="server" Visible="false" Style="position: relative">
                                                            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" OnRowCommand="gvOrders_RowCommand"
                                                                CssClass="mGrid" OnRowDataBound="gvOrders_RowDataBound">
                                                                
                                                               <Columns>
                                                                     <asp:TemplateField>
                                                <ItemTemplate>
                                                   
                                                     <asp:ImageButton ID="ImageButton5" runat="server" TabIndex="29" CommandArgument='<%# Eval("Project No") %>'
                                                        CommandName="Printoffer" ImageUrl="~/Images/New Icon/pdfImg.png" ToolTip="PDF of Offer" />
                                                   
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Wrap="false" />
                                            </asp:TemplateField>

  <asp:BoundField DataField="Project No" HeaderText="Project No">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Purachse Date" HeaderText=" Date">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>

                                                               </Columns>
                                                            </asp:GridView>
                                                             <asp:Label ID="grandTotal" runat="server"></asp:Label>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>




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


                                                    <asp:ImageButton ID="ImageButton4" runat="server" TabIndex="29" CommandArgument='<%# Eval("#") %>'
                                                        CommandName="Oldprint"  Visible="false" ImageUrl="~/Images/New Icon/pdfImg.png" ToolTip="PDF of Indent Register" />
                                                    <asp:ImageButton ID="ImageButton3" runat="server" TabIndex="29" CommandArgument='<%# Eval("#") %>'
                                                        CommandName="RS" Visible="false" ImageUrl="~/Images/Icon/rlogo.png" ToolTip="PDF of Indent Register" />
                                                    <asp:ImageButton ID="ImgNMail" runat="server" CommandArgument='<%# Eval("#") %>'
                                                        CommandName="MailIndent" ImageUrl="~/Images/Icon/Email-Blue.jpg" ToolTip="Mail Indent" />
                                                    <asp:ImageButton ID="IMGDELETEMR" runat="server" CommandArgument='<%# Eval("#") %>'
                                                        CommandName="DeleteMR" ImageUrl="~/Images/New Icon/Cancel__Black.png" ToolTip="Delete" />
                                                    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                        TargetControlID="IMGDELETEMR">
                                                    </ajax:ConfirmButtonExtender>

                                                    <asp:ImageButton ID="ImageButton112" runat="server" TabIndex="29" CommandArgument='<%# Eval("#") %>'
                                                        CommandName="Print" Visible="false" ImageUrl="~/Images/New Icon/pdfImg.png" ToolTip="PDF of Indent Register" />



                                                     <asp:ImageButton ID="ImageButton5" runat="server" TabIndex="29" CommandArgument='<%# Eval("#") %>'
                                                        CommandName="Printoffer" ImageUrl="~/Images/New Icon/pdfImg.png" ToolTip="PDF of Offer" />
                                                   
                                                    
                                                   
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Wrap="false" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="#" HeaderText="Project No">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="project" HeaderText="Project">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="pdate" HeaderText="Date">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>


                                        </Columns>
                                    </asp:GridView>
                                <asp:GridView ID="grdofferitem" runat="server" ></asp:GridView>
                                </ContentTemplate>
          
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </table>











            <div style="display: none">

                <asp:Panel ID="pnlprint" runat="server">

                    <div style="width: 695px; font-family: monospace;">
                        <div style="border: 2px solid black;" id="break_div">
                            <div
                                style="width: 695px; border: 2px solid black; margin-left: -2px; margin-top: -2px;">



                                <div>
                                    <table>

                                        <td style="width: 130px;"></td>

                                        <td colspan="2" style="align-content: center; margin-top: 30px;">
                                            <asp:Image ID="image" runat="server" Height="100px" Width="100px" /></td>
                                    </table>


                                </div>
                                <div style="width: 350; margin-left: 325px; margin-top: -100px;">
                                    <table style="font-family: monospace;">
                                        <tr>
                                            <td colspan="2">
                                                <label style="font: 200">PETROSAFE SAFETY SYSTEMS</label></td>
                                        </tr>
                                        <tr>
                                            <td style="line-height: 50px;">Address:</td>
                                            <td>
                                                <asp:Label ID="lbladdress" Text="Scared World Wanwadi Pune" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Tel No. :</td>
                                            <td>
                                                <asp:Label ID="lbltel1" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>E-Mail :</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Website :</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>




                            </div>

                            <div
                                style="width: 363px; height: 160px; border: 2px solid black; margin-left: 330px;">


                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <label><b><u>Project Details </u></b></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Project  No :</td>
                                        <td>
                                            <asp:Label ID="lblenno" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Purchase Date :</td>
                                        <td>
                                            <asp:Label ID="lbldate" runat="server"></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td>Updated By :</td>
                                        <td>
                                            <asp:Label ID="Label15" runat="server"></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td>Updated  Date :</td>
                                        <td>
                                            <asp:Label ID="Label16" runat="server"></asp:Label></td>
                                    </tr>




                                </table>

                            </div>




                            <div
                                style="overflow: auto; border: 2px solid black; margin-left: -2px; margin-top: -2px; text-align: left;">

                                <label>SCOPE OF SUPPLY:</label>
                            </div>
                            <div
                                style="overflow: auto; min-height: 300px; border: 2px solid black; margin-left: -2px; margin-top: -2px; text-align: center;">
                                <asp:GridView ID="gv1" runat="server">
                                </asp:GridView>
                            </div>
                            <div style="width: 50%; float: right">
                                <table style="border: 1px solid black; margin-top: 5px; width: 100%; border-collapse: collapse;">

                                    <tr>
                                        <td style="border: 1px solid black;">Total
                                            <br />
                                            Value (A) INR </td>
                                        <td style="border: 1px solid black;">
                                            <asp:Label ID="lbltotalgridivew1" Width="80px" Font-Bold="true" runat="server"></asp:Label></td>
                                    </tr>

                                </table>
                            </div>



                          


                            <div class="table211" id="table211" style="width: 100%; border: 1px solid black; margin-left: -2px; text-align: center;">
                                <div style="width: 51%; float: right">


                                    <asp:GridView ID="gv3" runat="server" Width="100%">
                                    </asp:GridView>
                                </div>

                            </div>

                            <div style="width: 100%; border: 1px solid black; height: 250px; margin-left: -2px; text-align: center;">

                                <div style="width: 51%; float: right; margin-top: 10px;">
                                    <table style="border: 1px solid black; width: 100%; border-collapse: collapse;">

                                        <tr>


                                            <td style="border: 1px solid black;">Total
                                   
                                    Value (C) INR </td>
                                            <td style="border: 1px solid black;">
                                                <asp:Label ID="lblpdtotal" Width="80px" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>

                                    </table>
                                </div>

                            </div>


                              <div class="table2" style="margin-top: 100px; width: 100%; min-height: 200px; border: 1px solid black; margin-left: -2px; text-align: center;">


                               <div
                                style="overflow: auto; border: 2px solid black; margin-left: -2px; margin-top: -2px; text-align: left;">

                                <label>(B(Optional)):</label>
                            </div>
                                <asp:GridView ID="gv2" runat="server" Width="100%">
                                </asp:GridView>



                            </div>
                            <div class="table2" style="width: 100%; min-height: 200px; border: 1px solid black; margin-left: -2px; text-align: center;">


                                <div style="width: 51%; float: right; margin-top: 10px;">
                                    <table style="border: 1px solid black; width: 100%; border-collapse: collapse;">

                                        <tr>


                                            <td style="border: 1px solid black;">Total
                                
                                Value (B) INR </td>
                                            <td style="border: 1px solid black;">
                                                <asp:Label ID="lbltotalgvoitem1" Width="80px" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>

                                    </table>
                                </div>


                                <div style="width: 51%; float: right; margin-top: 10px;">
                                    <table style="border: 1px solid black; width: 100%; border-collapse: collapse;">

                                        <tr>


                                            <td style="border: 1px solid black;">Grand Total     Value (A) INR </td>


                                            <td style="border: 1px solid black;">
                                                <asp:Label ID="Label17" Width="80px" ForeColor="Red" Font-Size="Larger" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>

                                    </table>
                                </div>

                            </div>



                        </div>
                </asp:Panel>

            </div>








            <div style="display: none">

                <asp:Panel ID="Panel1" runat="server">

                    <div style="width: 695px; font-family: monospace;">
                        <div style="border: 2px solid black;" id="break_div">
                            <div
                                style="width: 695px; border: 2px solid black; margin-left: -2px; margin-top: -2px;">



                                <div>
                                    <table>

                                        <td style="width: 130px;"></td>

                                        <td colspan="2" style="align-content: center; margin-top: 30px;">
                                            <asp:Image ID="image1" runat="server" Height="100px" Width="100px" /></td>
                                    </table>


                                </div>
                                <div style="width: 350; margin-left: 325px; margin-top: -100px;">
                                    <table style="font-family: monospace;">
                                        <tr>
                                            <td colspan="2">
                                                <label style="font: 200">PETROSAFE SAFETY SYSTEMS</label></td>
                                        </tr>
                                        <tr>
                                            <td style="line-height: 50px;">Address:</td>
                                            <td>
                                                <asp:Label ID="Label5" Text="Scared World Wanwadi Pune" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Tel No. :</td>
                                            <td>
                                                <asp:Label ID="Label20" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>E-Mail :</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Website :</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>







                            <div
                                style="overflow: auto; border: 1px solid black; margin-left: -2px; margin-top: 10px; text-align: center;">

                                <asp:Label ID="lblrevisedscope" runat="server" Font-Bold="true" Font-Size="Larger" ForeColor="Red" Text="REVISED PROJECT SCOPE"></asp:Label>
                            </div>


                            <div
                                style="width: 440px; height: 120px; border: 1px solid black; margin-left: -2px; margin-top: -2px;">


                                <%--<label><b><u>Supplier's Name & Address</u></b></label>--%>

                                <table>
                                    <caption>
                                        </tr>
                        <tr>
                            <td>Project No :</td>
                            <td>
                                <asp:Label ID="lblSuppname" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </td>
                        </tr>
                                        <tr>
                                            <td>Purchase Date :</td>
                                            <td>
                                                <asp:Label ID="lblsuppadd" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Updated By :</td>
                                            <td>
                                                <asp:Label ID="lblsuppcontact" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Updated Date :</td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </caption>

                                </table>
                            </div>
                            <hr />

                            <div
                                style="overflow: auto; margin-left: -2px; margin-top: 10px; text-align: left;">

                                <label>SCOPE OF SUPPLY:</label>
                            </div>
                            <div
                                style="overflow: auto; min-height: 300px; border: 2px solid black; margin-left: -2px; margin-top: 10px; text-align: center;">
                                <asp:GridView ID="GridView2" runat="server">
                                </asp:GridView>
                            </div>



<%--                            <div
                                style="overflow: auto; margin-left: -2px; margin-top: 10px; text-align: left;">

                                <label>Offer for Spare key/Master Key/Key Cabinets:</label>
                            </div>
                            <div class="table2" style="margin-top: 10px; width: 100%; min-height: 200px; border: 1px solid black; margin-left: -2px; text-align: center;">


                                <asp:GridView ID="GridView3" runat="server" Width="100%">
                                </asp:GridView>



                            </div>--%>

                        </div>



                    </div>
                </asp:Panel>

            </div>



            <div style="display: none">

                <asp:Panel ID="Panel2" runat="server">

                    <div style="width: 695px; font-family: monospace;">
                        <div style="border: 2px solid black;" id="break_div">
                            <div
                                style="width: 695px; border: 2px solid black; margin-left: -2px; margin-top: -2px;">



                                <div>
                                    <table>

                                        <td style="width: 130px;"></td>

                                        <td colspan="2" style="align-content: center; margin-top: 30px;">
                                            <asp:Image ID="image2" runat="server" Height="100px" Width="100px" /></td>
                                    </table>


                                </div>
                                <div style="width: 350; margin-left: 325px; margin-top: -100px;">
                                    <table style="font-family: monospace;">
                                        <tr>
                                            <td colspan="2">
                                                <label style="font: 200">PETROSAFE SAFETY SYSTEMS</label></td>
                                        </tr>
                                        <tr>
                                            <td style="line-height: 50px;">Address:</td>
                                            <td>
                                                <asp:Label ID="Label9" Text="Scared World Wanwadi Pune" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Tel No. :</td>
                                            <td>
                                                <asp:Label ID="Label21" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>E-Mail :</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Website :</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>







                            <div
                                style="overflow: auto; border: 1px solid black; margin-left: -2px; margin-top: 10px; text-align: center;">

                                <asp:Label ID="Label10" runat="server" Font-Bold="true" Font-Size="Larger" ForeColor="Red" Text=" PROJECT SCOPE"></asp:Label>
                            </div>


                            <div
                                style="width: 440px; height: 120px; border: 1px solid black; margin-left: -2px; margin-top: -2px;">


                                <%--<label><b><u>Supplier's Name & Address</u></b></label>--%>

                                <table>
                                    <caption>
                                        </tr>
                        <tr>
                            <td>Project No :</td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </td>
                        </tr>
                                        <tr>
                                            <td>Purchase Date :</td>
                                            <td>
                                                <asp:Label ID="Label12" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Created By :</td>
                                            <td>
                                                <asp:Label ID="Label13" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Created Date :</td>
                                            <td>
                                                <asp:Label ID="Label14" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </caption>

                                </table>
                            </div>
                            <hr />

                            <div
                                style="overflow: auto; margin-left: -2px; margin-top: 10px; text-align: left;">

                                <label>SCOPE OF SUPPLY:</label>
                            </div>
                            <div
                                style="overflow: auto; min-height: 300px; border: 2px solid black; margin-left: -2px; margin-top: 10px; text-align: center;">
                                <asp:GridView ID="GridView4" runat="server">
                                </asp:GridView>
                            </div>



                      

                        </div>



                    </div>
                </asp:Panel>

            </div>





            
            <div style="display: none">

                <asp:Panel ID="Panel3" runat="server">

                    <div style="width: 695px; font-family: monospace;">
                        <div style="border: 2px solid black;" id="break_div">
                            <div
                                style="width: 695px; border: 2px solid black; margin-left: -2px; margin-top: -2px;">



                                <div>
                                    <table>

                                        <td style="width: 130px;"></td>

                                        <td colspan="2" style="align-content: center; margin-top: 30px;">
                                            <asp:Image ID="image3" runat="server" Height="100px" Width="100px" /></td>
                                    </table>


                                </div>
                                <div style="width: 350; margin-left: 325px; margin-top: -100px;">
                                    <table style="font-family: monospace;">
                                        <tr>
                                            <td colspan="2">
                                                <label style="font: 200">PETROSAFE SAFETY SYSTEMS</label></td>
                                        </tr>
                                        <tr>
                                            <td style="line-height: 50px;">Address:</td>
                                            <td>
                                                <asp:Label ID="Label22" Text="Scared World Wanwadi Pune" runat="server"></asp:Label></td>
                                        </tr>
                                     
                                        <tr>
                                            <td>Tel No. :</td>
                                            <td>
                                                <asp:Label ID="lbloffferrtelno" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>E-Mail :</td>
                                            <td><asp:Label ID="lblofferemail" runat="server"></asp:Label></td></td>
                                        </tr>
                                        <tr>
                                            <td>Website :</td>
                                             <td><asp:Label ID="lbloferrwebsite" runat="server"></asp:Label></td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>







                            <div
                                style="overflow: auto; border: 1px solid black; margin-left: -2px; margin-top: 10px; text-align: center;">

                                <asp:Label ID="Label24" runat="server" Font-Bold="true" Font-Size="Larger" ForeColor="Red" Text=" PROJECT SCOPE"></asp:Label>
                            </div>


                            <div
                                style="width: 100%; height: 100px; border: 1px solid black; margin-left: -2px; margin-top: -2px;">


                                <%--<label><b><u>Supplier's Name & Address</u></b></label>--%>

                                <table>
                                    <caption>
                                        </tr>
                        <tr>
                            <td>Project No :</td>
                            <td>
                                <asp:Label ID="lblofferprojectno" runat="server" ></asp:Label>
                            </td>
                     <td>Purchase Date :</td>
                                            <td>
                                                <asp:Label ID="lblofferpurchasedate" runat="server"></asp:Label>
                                            </td>   


                        </tr>
                                       
                                        <tr>
                                            <td>Created By :</td>
                                            <td>
                                                <asp:Label ID="lbloffercreateddaye" runat="server"></asp:Label>
                                            </td>
                                      
                                            <td>Created Date :</td>
                                            <td>
                                                <asp:Label ID="lbloffercreateddate" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </caption>

                                </table>
                            </div>
                            <hr />

                            <div
                                style="overflow: auto; margin-left: -2px; margin-top: 10px; text-align: left;">

                               <b> <label style="font-size:large">SCOPE OF SUPPLY: All material used is SS316 grade.</label></b>
                            </div>
                            <div
                                style="overflow: auto; min-height: 150px; border: 2px solid black; margin-left: -2px; margin-top: 10px; text-align: center;">
                                <asp:GridView ID="grdoffer" runat="server">
                                </asp:GridView>
                           <br />
                            <label style="margin-left:69%;">Total A:</label><label style="align:right"></label><asp:Label ID="lbloffergridview1total" Font-Bold="true" runat="server" Font-Size="Large"/>
                                <br />


                              <b>  <label style="margin-left:40%; font-size:large;">Optional:</label></b>
                            
                            <br /> <br />
                                <asp:GridView ID="grdpddetailsoffer" runat="server" style="margin-left: 36%;min-height:100px;border: 1px solid black;width: 64%;">
                                </asp:GridView>
                            
                                 <br />
                            <label style="margin-left:69%;">Total B:</label><label style="align:right"></label><asp:Label ID="lblofeeroptionaltotal" Font-Bold="true" runat="server" Font-Size="Large"/>
                                <br />
                                <br />
                            <label style="margin-left:69%;">Total A + Total B:</label><label style="align:right"></label><asp:Label ID="lbloffertotalab" Font-Bold="true" runat="server" Font-Size="Large"/>
                              
                            </div>



                      

                     

 <div class="total_a_plus_b">

                 <b>  <label style="margin-left:5%; font-size:large;">Offer for Spare Keys and P Cards (Optional):</label></b>
                                <asp:GridView ID="girdofferextra" runat="server" style="min-height:100px;border: 1px solid black;width: 74%;">
                                </asp:GridView>
                                 <br />
                            <label style="margin-left:69%;">Total C:</label><label style="align:right"></label><asp:Label ID="lblofferitemtotal" Font-Bold="true" runat="server" Font-Size="Large"/>
                                <br />
                                   </div>
                           </div>
                    
                </asp:Panel>

            </div>







        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>

