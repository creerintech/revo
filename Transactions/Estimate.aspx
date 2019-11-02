<%@ Page Title="ESTIMATE" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Estimate.aspx.cs" Inherits="Transactions_Estimate" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">





    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=imgOrdersShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
            $("[id*=imgProductsShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
        });
    </script>

    










    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=txtQuantity]").val("0");
        });
        $("body").on("change keyup", "[id*=txtQuantity]", function () {
            //Check whether Quantity value is valid Float number.
            var quantity = parseFloat($.trim($(this).val()));
            if (isNaN(quantity)) {
                quantity = 0;
            }

            //Update the Quantity TextBox.
            $(this).val(quantity);

            //Calculate and update Row Total.
            var row = $(this).closest("tr");
            $("[id*=lblTotal]", row).html(parseFloat($(".price", row).html()) * parseFloat($(this).val()));

            //Calculate and update Grand Total.
            var grandTotal = 0;
            $("[id*=lblTotal]").each(function () {
                grandTotal = grandTotal + parseFloat($(this).html());
            });
            $("[id*=lblGrandTotal]").html(grandTotal.toString());
        });
    </script>






     <script type="text/javascript">


             function CheckAll(oCheckbox) {
                 var GridView2 = document.getElementById("<%=gvCustomers.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }



                  </script>






















    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .Grid td {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }

        .Grid th {
            background-color: #3AC0F2;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }

        .ChildGrid td {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }

        .ChildGrid th {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }

        .Nested_ChildGrid td {
            background-color: #fff !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
        }

        .Nested_ChildGrid th {
            background-color: #2B579A !important;
            color: White;
            font-size: 10pt;
            line-height: 200%;
        }
    </style>
   <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=ttxprocuredqty]").val("0");
    });
    $("body").on("change keyup", "[id*=ttxprocuredqty]", function () {
        //Check whether Quantity value is valid Float number.
        var quantity = parseFloat($.trim($(this).val1()));
        if (isNaN(quantity)) {
            quantity = 0;
        }



        //Update the Quantity TextBox.
        $(this).val(quantity);


        //Calculate and update Row Total.
        var row = $(this).closest("tr");
        $("[id*=txtamount]", row).html(parseFloat($(".price", row).html()) * parseFloat($(this).val()));
 
        //Calculate and update Grand Total.
        var grandTotal = 0;
        $("[id*=lblTotal]").each(function () {
            grandTotal = grandTotal + parseFloat($(this).html());
        });
        $("[id*=lblGrandTotal]").html(grandTotal.toString());
    });
</script>






    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=Panel1.ClientID %>");
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

        .auto-style1 {
            font-size: 12px;
            text-align: right;
            vertical-align: middle;
            height: 29px;
        }

        .auto-style2 {
            width: 464px;
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
    ESTIMATE
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=imgOrdersShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
            $("[id*=imgProductsShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
        });
    </script>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="8">
                <tr>
                    <td>
                        <fieldset id="F1" runat="server" width="100%">
                            <table width="100%" cellspacing="8">
                                <div>
                                    <tr>
                                        <td class="Label">Project :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpparty" runat="server" CssClass="ComboBox" Width="142px"
                                                AutoPostBack="true">
                                            </asp:DropDownList>

                                            <asp:Label ID="est" runat="server" Visible="false"></asp:Label>
                                        </td>


                                    </tr>

                                    <tr>
                                        <td class="auto-style1">Select Product </td>

                                        <td style="width: 130px;">
                                            <asp:DropDownList ID="drptitle" runat="server" AutoPostBack="true" CssClass="ComboBox" OnSelectedIndexChanged="drptitle_SelectedIndexChanged" Width="142px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>Qty
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtitemqty" runat="server" CssClass="TextBox"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click1" CssClass="button" Width="100px" Text="Add To Grid" />

                                        </td>

                                    </tr>
                                </caption>
                                    </caption>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            </tr>
                <td>
                    <fieldset id="F2" runat="server" width="100%">
                        <table cellspacing="8" width="100%">
                            <tr>
                                <td colspan="6">
                                    <div id="DivDisp" runat="server" class="ScrollableDiv_FixHeightWidth4" width="90%">



                                        <%--<asp:GridView ID="gvCustomers" runat="server"  AutoGenerateColumns="false" CssClass="mGrid"
                                           >
                                            <Columns>



                                                <asp:TemplateField HeaderText="ItemName" ItemStyle-HorizontalAlign="Center">
                                                   
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtitemname" runat="server" Enabled="false" Text='<%# Bind("ItemName") %>'></asp:TextBox>


                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                              

                                                <asp:TemplateField HeaderText="ItemDesc" ItemStyle-HorizontalAlign="Center">
                                                   
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtitemdesc" runat="server" Enabled="false" Text='<%# Bind("ItemDesc") %>'></asp:TextBox>


                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                   
                                                     

                                                <asp:TemplateField HeaderText=" Req Qty" ItemStyle-HorizontalAlign="Center">
                                                   
                                                       <ItemTemplate>
                                                        <asp:TextBox ID="txtreqqty" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>


                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                   
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText=" L.P.P" ItemStyle-HorizontalAlign="Center">

                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtllp" runat="server" Text='<%# Bind("Rate") %>'></asp:TextBox>


                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtlpp1" runat="server" Text='<%# Bind("Rate") %>'></asp:TextBox>
                                                    </EditItemTemplate>

                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Todays Value">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtremark" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Avialble Qty ">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtavlqty" runat="server" Text="0"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="To be Procured">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtremark" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>--%>


                                        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                                            DataKeyNames="TemplateID" OnRowDataBound="OnRowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgOrdersShow" runat="server" OnClick="Show_Hide_OrdersGrid"
                                                            ImageUrl="~/images/plus.png" CommandArgument="Show" />
                                                        <asp:Panel ID="pnlOrders" runat="server" Visible="false" Style="position: relative">
                                                            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false"
                                                                CssClass="ChildGrid" OnRowDataBound="gvOrders_RowDataBound"
                                                                DataKeyNames="ItemName">
                                                                <Columns>
                                                                   



                                                                 <asp:TemplateField HeaderText="SelectAll" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkboxSelectAll" AutoPostBack="true" Checked="true" runat="server" Text="Select All" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkbox" OnCheckedChanged="chkbox_CheckedChanged" AutoPostBack="true" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                              
                                                                    <asp:TemplateField HeaderText="Particulars">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="TXTITEMNAME" runat="server" Text='<%# Bind("ItemName") %>' Enabled="false"></asp:Label>


                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:Label ID="TXTITEMNAME" runat="server" Text='<%# Bind("ItemName") %>' Enabled="false"></asp:Label>
                                                                        </EditItemTemplate>
                                                                        
                                                                    </asp:TemplateField>
                                                                     <asp:BoundField ItemStyle-Width="150px" DataField="ItemDesc" HeaderText="ItemDesc" />
                                                                    <asp:TemplateField HeaderText="Req Qty">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtreqqty" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>


                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>
                                                                        </EditItemTemplate>

                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="L.P.P">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtllp" runat="server" Text='<%# Bind("Rate") %>'></asp:TextBox>


                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtlpp1" runat="server" Text='<%# Bind("Rate") %>'></asp:TextBox>
                                                                        </EditItemTemplate>

                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Todays Value">
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txttodaysvalue" runat="server"  Text='<%# Bind("Rate") %>'  CssClass="val1"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Availabe Qty">
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtavlqty" runat="server" Text='<%# Bind("AvlQty") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderText="To be Procured">
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="ttxprocuredqty" CssClass="val2" runat="server" AutoPostBack="true" OnTextChanged="ttxprocuredqty_TextChanged"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                                   <asp:TemplateField HeaderText=" Procured Amount">
                                                                        <ItemTemplate>

                                                                              <asp:TextBox ID="txtamount" CssClass="val2" runat="server" AutoPostBack="true" ></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                     <asp:TemplateField HeaderText=" Total EStimate Amount"   >
                                                                        <ItemTemplate>

                                                                              <asp:TextBox ID="txtamount1" CssClass="val2" runat="server" AutoPostBack="true" ></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   

                                                     

                                                                </Columns>
                                                            </asp:GridView>
                                                             <asp:Label ID="grandTotal" runat="server"></asp:Label>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField ItemStyle-Width="150px" DataField="TemplateName" HeaderText="TemplateName" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="qty" HeaderText="Qty" />


                                            </Columns>
                                        </asp:GridView>




                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <%--<td></td>--%>
                                <%--<td valign="middle">
                                    <asp:Label ID="Lblremark" runat="server" CssClass="Label4" Text="Narration :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtnarration" runat="server" CssClass="TextBox" TabIndex="6" TextMode="MultiLine" Width="560px"></asp:TextBox>
                                </td>--%>
                                <%--  <td align="right" class="Label" colspan="1">
                                    <asp:Button ID="BtnRefresh" runat="server" CssClass="button" TabIndex="7" Text="Amount" ToolTip="Get Total Requisition Amount" />
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
            <tr>
                <td class="Label">Total  Estimate:
                                        <asp:TextBox ID="txtSubTotal" runat="server" CssClass="TextBoxNumeric" Width="120px"
                                            TabIndex="1" Enabled="false"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="Label">&nbsp;Avl. Stock value&nbsp; :
                                        <asp:TextBox ID="txtDisCountSub" runat="server" CssClass="TextBoxNumeric" Width="120px"
                                            onkeyup="Calculate_NetTotal();" TabIndex="1"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="Label">&nbsp;TO be Procured&nbsp; :
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="TextBoxNumeric" Width="120px"
                                            onkeyup="Calculate_NetTotal();" TabIndex="1"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="Label">&nbsp;Order Booked At&nbsp; :
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="TextBoxNumeric" Width="120px"
                                            onkeyup="Calculate_NetTotal();" TabIndex="1"></asp:TextBox>
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

                                                
                                                &nbsp;</td>
                                            <td>
                                                <asp:Button ID="BtnSave" runat="server" CausesValidation="true" TabIndex="8" CssClass="button"
                                                    Text="Save" ValidationGroup="Add" OnClick="BtnSave_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" TabIndex="8" CssClass="button"
                                                    Text="Cancel" />
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
                            <%--<asp:Button ID="Btn1" runat="server" BackColor="White" Enabled="false" Width="5%" />
                                <asp:Label runat="server" ID="lblBG" Text="- Generated" CssClass="Label4"></asp:Label>
                                <asp:Button ID="Button1" runat="server" BackColor="Yellow" Enabled="false" Width="5%" />
                                <asp:Label runat="server" ID="Label7" Text="- Approved" CssClass="Label4"></asp:Label>
                                <asp:Button ID="Button2" runat="server" BackColor="MediumSeaGreen" Enabled="false" Width="5%" />
                                <asp:Label runat="server" ID="Label8" Text="- Authorised" CssClass="Label4"></asp:Label>
                                <asp:Button ID="Button3" runat="server" BackColor="PowderBlue" Enabled="false" Width="5%" />
                                <asp:Label runat="server" ID="Label2" Text="-PO GENERATED" CssClass="Label4"></asp:Label>
                                <asp:Button ID="Button4" runat="server" BackColor="IndianRed" Enabled="false" Width="5%" />
                                <asp:Label runat="server" ID="Label3" Text="-Email Sent" CssClass="Label4"></asp:Label>--%>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:GridView ID="ReportGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="ReportGrid_RowCommand" OnPageIndexChanging="ReportGrid_PageIndexChanging"
                                    CssClass="mGrid" DataKeyNames="#" AlternatingRowStyle-BackColor="" Width="100%">
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

                                        <asp:BoundField DataField="EST_No" HeaderText="EST_No">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Project" HeaderText=" Project">
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
            </table>
            <div style="display: none">

                <asp:Panel ID="pnlprint1" runat="server">
                    <%-- --%>
                    <div>
                        <table style="width: 100%;">
                            <caption>
                                <h1>
                                    <asp:Label ID="lblcompany" runat="server" Text=" PETROSAFE SAFETY SYSTEMS"></asp:Label>
                                </h1>
                            </caption>


                            </tr>
    <tr>
        <td class="auto-style2">
            <h3>Address:
                <asp:Label ID="lbladdress" runat="server" Text="Pune"> </asp:Label></h3>
        </td>

        <td style="float: right; margin-right: 50px;">
            <h3>Tel No:
                <asp:Label ID="Label2" runat="server"> </asp:Label></h3>
        </td>
    </tr>

                            <tr>
                                <td class="auto-style2">
                                    <h3>E-Mail:
                                        <asp:Label ID="lblemail" runat="server"> </asp:Label></h3>
                                </td>
                            </tr>
                            <caption>
                                <hr />
                                <tr>
                                    <td class="auto-style2">
                                        <h3>EST NO:
                                        <asp:Label ID="lblest" runat="server"> </asp:Label>
                                        </h3>
                                    </td>
                                    <td style="float: right; margin-right: 50px;">
                                        <h3>Party:
                                        <asp:Label ID="lblparty" runat="server"> </asp:Label>
                                        </h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">
                                        <h3 style="width: 600px;">Date:
                                        <asp:Label ID="lbldate" runat="server" Text="10-12-1994"> </asp:Label>
                                        </h3>
                                    </td>
                                </tr>
                                <caption>
                                    <hr />
                                    <br />
                                    <br />
                                    <asp:GridView ID="grdprint" runat="server" BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="None" Width="1045px">
                                    </asp:GridView>
                        </table>
                        <table style="width: 100%">
                            <tr>
                                <td class="auto-style2">
                                    <h3>Total Estimate:
                                            <asp:Label ID="lbltotalestmate" runat="server" Text="EST12"> </asp:Label>
                                    </h3>
                                </td>
                                <td style="float: right; margin-right: 50px;">
                                    <h3>TO be Procured:
                                            <asp:Label ID="lbltobeprocured" runat="server" Text="EST12"> </asp:Label>
                                    </h3>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <h3>Avl. Qty:
                                            <asp:Label ID="lblavlqty" runat="server" Text="ACBSupp;lkier"> </asp:Label>
                                    </h3>
                                </td>
                                <td style="float: right; margin-right: 50px;">
                                    <h3>Order Booked At:
                                            <asp:Label ID="lblorderbookedat" runat="server" Text="EST12"> </asp:Label>
                                    </h3>
                                </td>
                            </tr>
                        </table>
                        </caption>
                        </caption>
                    </div>



                </asp:Panel>
            </div>



















            <div style="display: none">

            <asp:Panel ID="Panel1" runat="server">

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
						<td><asp:Label ID="Label3" Text="Scared World Wanwadi Pune" runat="server"></asp:Label></td>
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
			<label>ESTIMATE </label>
		</div>
		<div>
			<div
				style="width: 330px; height: 250px; border: 2px solid black; margin-left: -2px; margin-top: -2px;">


				<%--<label><b><u>Supplier's Name & Address</u></b></label>--%>

                <table>
                    <caption>
                        
                        </tr>
                        <tr>
                            <td> Total Estimate :</td>
                            <td>
                                <asp:Label ID="lbltotalestimate" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td> Avl.Stock Value :</td>
                            <td>
                                <asp:Label ID="lblavlqty1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>To Be Procured :</td>
                            <td>
                                <asp:Label ID="lbltobeprocured1" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td>Order Booked At:</td>
                            <td>
                                <asp:Label ID="lblorderbookedat1" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </caption>

				</table>
			</div>
			<div
				style="width: 363px; height: 250px; border: 2px solid black; margin-left: 330px; margin-top: -254px;">


				<table>
					
					<tr>
						<td>Project :</td>
						<td><asp:Label ID="lblproject" runat="server" Font-Bold="true"></asp:Label></td>
					</tr>
					<tr>
						<td>Date :</td>
						<td><asp:Label ID="Label4" runat="server"></asp:Label></td>
					</tr>
					

					
					
				</table>

			</div>
			<div
				style="width: 363px; height: 80px; border: 2px solid black; margin-left: 330px; margin-top: -84px;">
				<table>
					<tr>
						<td>Contact Person :</td>
					</tr>
					<tr>
						<td>Contact No. :</td>
						<td></td>
					</tr>
					<tr>
						<td>Site Name :</td>
						<td>HEAD OFFICE</td>
					</tr>

				</table>

			</div>

			
			
			<div
				style="width: 695px; height: 375px; border: 2px solid black; margin-left: -2px; margin-top: -2px; text-align: center;">
				<asp:GridView ID="GridView1" runat="server"  style="width: 695px;" GridLines="both"   ItemStyle-HorizontalAlign="center" >
                    </asp:GridView>
			</div> 

			</div>
			


				

			</div>
			
			</div>


		</div>

	</asp:Panel></div>    
        </ContentTemplate>







    </asp:UpdatePanel>

</asp:Content>


