<%@ Page Title="Generate Enquiry" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Generateenquiry.aspx.cs" Inherits="Transactions_Generateenquiry" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
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
    </style>


      <script type = "text/javascript">
        function PrintPanel() {
         var panel = document.getElementById("<%=pnlprint.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
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

    <%-- <script language="javascript" type="text/javascript">
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
    </script>--%>



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
                                                <asp:TextBox runat="server" ID="TXTKTO" CssClass="TextBox" Width="550px" AutoPostBack="true"></asp:TextBox>
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
            <%-- Search for Indent :
            <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
                Width="292px" AutoPostBack="True" ></asp:TextBox>--%>
            <div id="divwidth">
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="Server">
    Generate Enquiry
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
                                        <td class="Label">Select  Indent No :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpindentno" runat="server" CssClass="ComboBox" AutoPostBack="true" Width="142px" OnSelectedIndexChanged="drpindentno_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>

                                        <td class="Label">Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtdate" runat="server" CssClass="TextBox"></asp:TextBox>
                                            <ajax:CalendarExtender ID="CalDate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate">
                                            </ajax:CalendarExtender>
                                            <asp:DropDownList ID="drpcompany" runat="server" Visible="false" CssClass="ComboBox" Width="142px">
                                            </asp:DropDownList>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td class="Label">Enquiry No. :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblenquiryno" runat="server" CssClass="Label" Text="ENQ123"></asp:Label>
                                        </td>
                                    </tr>
                                </div>
                            </table>
                        </fieldset>
                    </td>
                </tr>

                </tr>
                <tr>
                    <td>
                        <fieldset id="F2" runat="server" width="100%">
                            <table width="100%" cellspacing="8">
                                <tr>
                                    <td colspan="6">
                                        <div id="DivDisp" runat="server" class="ScrollableDiv_FixHeightWidth4" width="90%">
                                            <asp:GridView ID="GrdRequisition" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowDataBound="GrdRequisition_RowDataBound" ShowFooter="True" OnRowCommand="GrdRequisition_RowCommand">
                                                <Columns>
                                                    <%-- 0--%><%-- 6--%><%-- 7--%>
                                                   
                                                    <%--8--%>







                                                    <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtParticulars" runat="server" Text='<%# Eval("ItemName") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>








                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                Visible="false">
                                                            </ajax:ComboBox>
                                                            <%--THIS START HERE--%>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="TxtItemName" runat="server" Text='<%# Eval("ItemDesc") %>'
                                                                        CssClass="search_List" Width="292px" AutoPostBack="True"></asp:TextBox>
                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemName" runat="server" TargetControlID="TxtItemName"
                                                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                        ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                    </ajax:AutoCompleteExtender>
                                                                    <ajax:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="TxtItemName"
                                                                        WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <%--THIS END HERE--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- 9--%>
                                                    <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty"  Visible="false" />
                                                    <%--  <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpparicular" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtqty" runat="server"  Text='<%# Eval("Qty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>


                                                             <asp:Button Text="Last 3  Supplier" runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" />
                                                        
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Supplier1">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlsupplier" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Supplier2">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlsupplier2" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Supplier3">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlsupplier3" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                     <asp:TemplateField HeaderText="Supplier4">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlsupplier4" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                     <asp:TemplateField HeaderText="Supplier5">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlsupplier5" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Supplier6">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlsupplier6" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>






                                                    <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtremark" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="ftr" />
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MayurInventory %>" SelectCommand="SELECT [RequisitionCafeId], [ItemId], [Qty] FROM [RequisitionCafeDtls]"></asp:SqlDataSource>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <%--<td></td>--%>
                                    <td valign="middle">
                                        <asp:Label ID="Lblremark" runat="server" CssClass="Label4" Text="Narration :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtnarration" runat="server" CssClass="TextBox" TabIndex="6" TextMode="MultiLine" Width="560px"></asp:TextBox>
                                    </td>
                                    <td align="right" class="Label" colspan="1">
                                        <asp:Button ID="BtnRefresh" runat="server" CssClass="button" TabIndex="7" Text="Amount" Visible="false" ToolTip="Get Total Requisition Amount" />
                                        <%-- Total Amount :--%>
                                        <asp:TextBox ID="lblTotalAmt" runat="server" Visible="false" CssClass="TextBoxNumericReadOnly" Enabled="false"></asp:TextBox>
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
                    <tr>
                        <td>
                            <fieldset id="F3" runat="server" width="100%">
                                <table cellspacing="8" width="100%">
                                    <tr>
                                        <td align="center" colspan="6">
                                            <table width="25%">
                                                <tr>
                                                    <td align="center">&nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="BtnSave" runat="server" CausesValidation="true" CssClass="button" TabIndex="8" Text="Save" ValidationGroup="Add" OnClick="BtnSave_Click" />
                                                    </td>

                                                     <td>
                                                        <asp:Button ID="btnupdate" runat="server" CausesValidation="true" CssClass="button" TabIndex="8" Text="Update" ValidationGroup="Add" OnClick="btnupdate_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" CssClass="button" TabIndex="8" Text="Cancel" />
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
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                        <asp:GridView ID="ReportGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="ReportGrid_RowCommand" OnPageIndexChanging="ReportGrid_PageIndexChanging"
                                            CssClass="mGrid" DataKeyNames="#">
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
                                                        
                                                            <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                                                                TabIndex="29" ToolTip="Print Indent Register" />

                                                       


                                                         <asp:ImageButton ID="ImageButton1" runat="server" TabIndex="29" CommandArgument='<%# Eval("#") %>'
                                                            CommandName="Print" ImageUrl="~/Images/New Icon/pdfImg.png" ToolTip="PDF of Indent Register" />



                                                           <%-- <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png" TabIndex="29"
                                                                ToolTip="PDF of Indent Register" CommandArgument='<%# Eval("#") %>'  CommandName="Delete" />--%>
                                                        
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
                                              
                                                <asp:BoundField DataField="date" HeaderText="Date">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="enno" HeaderText="ENQ NO">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="supplier" HeaderText="Supplier Name">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="reamrk" HeaderText="Remark">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                </asp:BoundField>

                                            </Columns>
                                        </asp:GridView>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </tr>
            </table>













<div style="display: none" >

            <asp:Panel ID="pnlprint" runat="server">

            <div style="width: 695px; height: 860px; border: 2px solid black; font-family: monospace; ">
		<div
			style="width: 695px; height: 162px; border: 2px solid black; margin-left: -2px; margin-top: -2px;">



            <div >
                <table >

                    <td style="width:130px;"></td>
                 
                    <td colspan="2" style="align-content:center; margin-top:30px;">  <asp:Image ID="image" runat="server" Height="100px" Width="100px" /></td>
                     </table>
                   

            </div>
			<div style="width: 350; margin-left: 325px; margin-top:-100px;">
				<table style="font-family: monospace;">
					<tr>
						<td colspan="2"><label style="font:200">PETROSAFE SAFETY SYSTEMS</label></td>
					</tr>
					<tr>
						<td style="line-height: 50px;">Address:</td>
						<td><asp:Label ID="lbladdress" Text="Scared World Wanwadi Pune" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td></td>
						<td></td>
					</tr>
					<tr>
						<td>Tel No. :</td>
						<td></td>
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
			style="width: 695px; height: 22px; border: 2px solid black; margin-left: -2px; margin-top: -2px; text-align: center;">
			<label>SUPPLIER  ENQUIRY </label>
		</div>
		<div>
			<div
				style="width: 330px; height: 250px; border: 2px solid black; margin-left: -2px; margin-top: -2px;">


				<%--<label><b><u>Supplier's Name & Address</u></b></label>--%>

                <table>
                    <caption>
                        Supplier&#39;s Name &amp; Address
                        </tr>
                        <tr>
                            <td> Name :</td>
                            <td>
                                <asp:Label ID="lblSuppname" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td> Address :</td>
                            <td>
                                <asp:Label ID="lblsuppadd" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Contact No. :</td>
                            <td>
                                <asp:Label ID="lblsuppcontact" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </caption>

				</table>
			</div>
			<div
				style="width: 363px; height: 250px; border: 2px solid black; margin-left: 330px; margin-top: -254px;">


				<table>
					<tr>
						<td colspan="2"><label><b><u> ENQUIRY FOR SUPPLIER </u></b></label></td>
					</tr>
					<tr>
						<td>Enquiry  No :</td>
						<td><asp:Label ID="lblenno" runat="server" Font-Bold="true"></asp:Label></td>
					</tr>
					<tr>
						<td>Date :</td>
						<td><asp:Label ID="lbldate" runat="server"></asp:Label></td>
					</tr>
					

					
					
				</table>

			</div>
			<div
				style="width: 363px; height: 80px; border: 2px solid black; margin-left: 330px; margin-top: -84px;">
				<table>
					<tr>
						<td>Contact Person : <asp:Label ID="lblcontactperson" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td>Contact No. : <asp:Label ID="lblcontactno" runat="server"></asp:Label></td>
						<td></td>
					</tr>
					<tr>
						<td>Site Name :</td>
						<td>HEAD OFFICE</td>
					</tr>

				</table>

			</div>

			
			
			<div
				style="width: 695px; height: 417px; border: 2px solid black; margin-left: -2px; margin-top: -2px; text-align: center;">
				<asp:GridView ID="grdprint" runat="server"  style="width: 695px;" OnRowDataBound="grdprint_RowDataBound"   >
                    </asp:GridView>
			</div> 

			</div>
			


				

			</div>
			</asp:Panel>
			</div>
           

		</div>

	</asp:Panel></div>                  
        

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

