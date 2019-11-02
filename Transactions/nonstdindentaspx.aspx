<%@ Page Title="Material Indent (Non STD)" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" Async="true" CodeFile="nonstdindentaspx.aspx.cs" Inherits="Transactions_nonstdindentaspx" %>


<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
        rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstbox]').multiselect({
                includeSelectAllOption: true
            });
        });
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
    </style>
    <style>
        body {
            line-height: 1.6em;
            font-size: .7em;
            font-family: Arial, "Trebuchet MS", Tahoma, sans-serif;
        }

        .tr1 {
            padding-top: 20px;
            display: table-row;
            vertical-align: inherit;
            border-color: inherit;
        }

        .td1 {
            height: 20px;
        }

        .table1 {
            border-collapse: separate;
            border-spacing: 2px;
        }

        .fieldset1 {
            display: block;
            margin-inline-start: 2px;
            margin-inline-end: 2px;
            padding-block-start: 0.35em;
            padding-inline-start: 0.75em;
            padding-inline-end: 0.75em;
            padding-block-end: 0.625em;
            min-inline-size: min-content;
            border-width: 2px;
            border-style: groove;
            border-color: threedface;
            border-image: initial;
        }
    </style>
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var paneName = $("[id*=PaneName]").val() != "" ? $("[id*=PaneName]").val() : "collapseOne";

            //Remove the previous selected Pane.
            $("#accordion .in").removeClass("in");

            //Set the selected Pane.
            $("#" + paneName).collapse("show");

            //When Pane is clicked, save the ID to the Hidden Field.
            $(".panel-heading a").click(function () {
                $("[id*=PaneName]").val($(this).attr("href").replace("#", ""));
            });
        });
    </script>
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
            Search for Indent :
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
    Material Indent
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="8" class="table1">
                <tr class="tr1">
                    <td class="td1">
                        <fieldset id="F1" runat="server" width="100%" style="" class="fieldset1">
                            <table width="100%" cellspacing="8">
                                <div>
                                    <tr>
                                        <td class="Label">Indent No :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtReqNo" runat="server" CssClass="TextBox" Visible="false" ></asp:TextBox>
                                            <asp:Label ID="lblReqNo" runat="server" CssClass="Label2" Text="" Width="180px"></asp:Label>
                                        </td>
                                        <td class="Label">Indent Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtReqDate" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CssClass="Imagebutton"
                                                ImageUrl="~/Images/Icon/DateSelector.png" />
                                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                PopupButtonID="ImageButton2" TargetControlID="txtReqDate">
                                            </ajax:CalendarExtender>
                                        </td>
                                        <td class="Label">Expt. Delivery Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTempDate" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CssClass="Imagebutton"
                                                ImageUrl="~/Images/Icon/DateSelector.png" />
                                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                PopupButtonID="ImageButton1" TargetControlID="txtTempDate">
                                            </ajax:CalendarExtender>
                                        </td>
                                        <td class="Label">From :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCafe" runat="server" CssClass="TextBox" Visible="false" Width="150px"></asp:TextBox>
                                            <asp:Label ID="lblCafe" runat="server" CssClass="Label2" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="Label">Project :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCostCentre" runat="server" CssClass="ComboBox" Width="142px" EnableViewState="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCostCentre_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCostCentre"
                                                Display="None" ErrorMessage="Please Select Project" SetFocusOnError="True"
                                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                            </ajax:ValidatorCalloutExtender>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="drpvalve" Visible="false" runat="server" CssClass="ComboBox" Width="142px" EnableViewState="true" ViewStateMode="Enabled" AutoPostBack="true" OnSelectedIndexChanged="drpvalve_SelectedIndexChanged">
                                            </asp:DropDownList>




                                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                            <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
                                                rel="stylesheet" type="text/css" />
                                            <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
                                            <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
                                            <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
                                            <script type="text/javascript">







                                                function pageLoad(sender, args) {
                                                    $(document).ready(function () {

                                                        $('[id*=lstvalve]').multiselect({
                                                            includeSelectAllOption: true
                                                        });

                                                    });
                                                }


                                            </script>


                                            <asp:ListBox ID="lstvalve" runat="server" SelectionMode="Multiple"></asp:ListBox>


                                            <asp:Button runat="server" ID="btnaddtogrid" CssClass="button" Text="Add" OnClick="btnaddtogrid_Click" Height="40px" Width="75px" />
                                        </td>

                                        <td class="Label" >
                                            Revised :
                                        </td>
                                        
                                        <td style="width:20px;"> </td>
                                        <td>
                                            <asp:CheckBox ID="chkrevised" runat="server" CssClass="CheckBox" />
                                        </td>



                                    </tr>

                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>Remark :
                                        </td>
                                        <td colspan="6">
                                            <asp:TextBox ID="txtindremark" runat="server" Width="500px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </div>
                            </table>
                        </fieldset>
                    </td>
                </tr>

                <tr class="tr1">
                    <td class="td1">
                        <fieldset id="F2" runat="server" width="100%" class="fieldset1">
                            <table width="100%" cellspacing="8">
                                <tr>
                                    <td colspan="3">
                                        <div id="DivDisp" runat="server" class="ScrollableDiv_FixHeightWidth4" width="90%">


                                            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                                <div class="panel panel-default" id="dvplate" runat="server">
                                                    <div class="panel-heading" role="tab" id="headingOne">
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true"
                                                                aria-controls="collapseOne">
                                                                <asp:Label ID="lblgrditem" runat="server" Text="Pipe Bracket"></asp:Label>
                                                            </a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapseOne" class="panel-collapse collapse " role="tabpanel" aria-labelledby="headingOne">
                                                        <div class="panel-body" style="overflow: scroll">

                                                            <asp:GridView ID="grditem" runat="server" CssClass="mGrid" OnRowUpdating="grditem_RowUpdating" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="grditem_RowDataBound" OnRowCommand="grditem_RowCommand">
                                                                <Columns>



                                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">

                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkbox" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Particulars">
                                                                        <ItemTemplate>
                                                                            <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                                Visible="false" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged1">
                                                                            </ajax:ComboBox>
                                                                            <%--THIS START HERE--%>
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="TxtItemName1" runat="server" Text='<%# Eval("itemname") %>'
                                                                                        CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemName_TextChanged1"></asp:TextBox>
                                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemName1" runat="server" TargetControlID="TxtItemName1"
                                                                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                                        ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                                    </ajax:AutoCompleteExtender>
                                                                                    <ajax:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="TxtItemName1"
                                                                                        WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <%--THIS END HERE--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />


                                                                    </asp:TemplateField>





                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>


                                                                            <asp:DropDownList ID="ddlItemDescription" runat="server" AutoPostBack="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlItemDescription_SelectedIndexChanged1" target="_blank"></asp:DropDownList>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Bracket Qty ">


                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbqty" runat="server" Width="60px" Text='<%# Eval("qty") %>' CssClass="TextBox"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>







                                                                    <asp:TemplateField HeaderText="Pipe length for Unit Qty (Mtr)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtpopelenghtunitqty" Width="60px" runat="server" AutoPostBack="true" CssClass="TextBox" OnTextChanged="txtpopelenghtunitqty_TextChanged" OnClientClick="target ='_blank';"></asp:TextBox>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Available Stock(Mtr)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtavlqty" Width="60px" runat="server"></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>





                                                                    <asp:TemplateField HeaderText="Balance Stock(Mtr)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtbalqty" Width="60px" runat="server"></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Type">
                                                                        <ItemTemplate>
                                                                            <asp:RadioButtonList ID="rbitemtype" runat="server">
                                                                                <asp:ListItem Text="Inhouse" Value="Inhouse"></asp:ListItem>
                                                                                <asp:ListItem Text="Outsource" Value="Outsource"></asp:ListItem>
                                                                                <asp:ListItem Text="Inhouse/Outsource" Value="Inhouse/Outsource"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="DrawingPath" HeaderText="DrawingPath" Visible="false">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="View Drawing">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnview" runat="server" ForeColor="Red" CssClass="Label" OnClick="lnkView_Click" Text="View Drawing " OnClientClick="target ='_blank';" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Uplo ad  Drawing">

                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="fl1" runat="server" ViewStateMode="Enabled" OnClientClick="target ='_blank';" />
                                                                            <asp:Button ID="btniupload" Text="Upload" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" target="_blank" />

                                                                            <asp:Button ID="Button5" Text="Upload Multiple" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Selectmulti" target="_blank" />
                                                                        </ItemTemplate>

                                                                        <EditItemTemplate></EditItemTemplate>
                                                                    </asp:TemplateField>






                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="panel panel-default" id="dvsheet" runat="server">
                                                    <div class="panel-heading" role="tab" id="heading2">
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapse2" aria-expanded="true"
                                                                aria-controls="collapse2">
                                                                <asp:Label ID="lblgrdplate" runat="server" Text="Plate, U-shape, L-shape  Bracket"></asp:Label>
                                                            </a>
                                                        </h4>
                                                    </div>


                                                    <div id="collapse2" class="panel-collapse collapse " role="tabpanel" aria-labelledby="heading2">
                                                        <div class="panel-body" style="overflow: scroll">



                                                            <asp:GridView ID="grdplate" runat="server" CssClass="mGrid" OnRowUpdating="grdplate_RowUpdating" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="grdplate_RowDataBound" OnRowCommand="grdplate_RowCommand">
                                                                <Columns>



                                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">

                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkbox" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Particulars">
                                                                        <ItemTemplate>
                                                                            <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                                Visible="false" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged1">
                                                                            </ajax:ComboBox>
                                                                            <%--THIS START HERE--%>
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="TxtItemNameplate" runat="server" Text='<%# Eval("itemname") %>'
                                                                                        CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemName_TextChanged1"></asp:TextBox>
                                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemName1plate" runat="server" TargetControlID="TxtItemNameplate"
                                                                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                                        ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                                    </ajax:AutoCompleteExtender>
                                                                                    <ajax:TextBoxWatermarkExtender ID="TBWE2plate" runat="server" TargetControlID="TxtItemNameplate"
                                                                                        WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <%--THIS END HERE--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />


                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>


                                                                            <asp:DropDownList ID="ddlItemDescriptionplate" runat="server" AutoPostBack="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle"></asp:DropDownList>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>




                                                                    <asp:TemplateField HeaderText="Bracket Qty ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbqty" Width="60px" runat="server" Text='<%# Eval("qty") %>' CssClass="TextBox"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Area for Unit Qty (Sq.Mtr) ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtareaforunitqty" Width="60px" AutoPostBack="true" runat="server" CssClass="TextBox" OnTextChanged="txtareaforunitqty_TextChanged" OnClientClick="target ='_blank';"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Total Area (Sq.Mtr) ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="lbltotalareaplate" Width="60px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Available Stock (Sq.Mtr)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblavlqty" Width="60px" runat="server"></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Balance Stock(Sq.Mtr)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtbalqty" Width="60px" runat="server"></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type">
                                                                        <ItemTemplate>
                                                                            <asp:RadioButtonList ID="rbitemtype" runat="server">
                                                                                <asp:ListItem Text="Inhouse" Value="Inhouse"></asp:ListItem>
                                                                                <asp:ListItem Text="Outsource" Value="Outsource"></asp:ListItem>
                                                                                <asp:ListItem Text="Inhouse/Outsource" Value="Inhouse/Outsource"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="DrawingPath" HeaderText="DrawingPath" Visible="false">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="View Drawing">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnviewplate" runat="server" ForeColor="Red" CssClass="Label" OnClick="btnviewplate_Click" Text="View Drawing " OnClientClick="target ='_blank';" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Upload  Drawing">

                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="fl1" runat="server" ViewStateMode="Enabled" OnClientClick="target ='_blank';" />
                                                                            <asp:Button ID="btniupload" Text="Upload" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" target="_blank" />

                                                                            <asp:Button ID="Button5" Text="Upload Multiple" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Selectmulti" target="_blank" />
                                                                        </ItemTemplate>

                                                                        <EditItemTemplate></EditItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>
                                                            </asp:GridView>


                                                        </div>
                                                    </div>


                                                </div>

                                                <div class="panel panel-default" id="dvadpater" runat="server">
                                                    <div class="panel-heading" role="tab" id="heading3">
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapse3" aria-expanded="true"
                                                                aria-controls="collapse3">Adaptor</a>
                                                        </h4>
                                                    </div>


                                                    <div id="collapse3" class="panel-collapse collapse " role="tabpanel" aria-labelledby="heading3">
                                                        <div class="panel-body" style="overflow: scroll">


                                                            <asp:GridView ID="grdadapter" runat="server" CssClass="mGrid" OnRowUpdating="grdadapter_RowUpdating" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="grdadapter_RowDataBound" OnRowCommand="grdadapter_RowCommand">
                                                                <Columns>



                                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">

                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkbox" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Particulars">
                                                                        <ItemTemplate>
                                                                            <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                                Visible="false" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged1">
                                                                            </ajax:ComboBox>
                                                                            <%--THIS START HERE--%>
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="TxtItemNameadapter" runat="server" Text='<%# Eval("ItemName") %>'
                                                                                        CssClass="search_List" Width="292px" AutoPostBack="True"></asp:TextBox>
                                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemNameadapter" runat="server" TargetControlID="TxtItemNameadapter"
                                                                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                                        ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                                    </ajax:AutoCompleteExtender>
                                                                                    <ajax:TextBoxWatermarkExtender ID="TBWE2adapter" runat="server" TargetControlID="TxtItemNameadapter"
                                                                                        WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <%--THIS END HERE--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />


                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>


                                                                            <asp:DropDownList ID="ddlItemDescriptionadapter" runat="server" AutoPostBack="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlItemDescriptionadapter_SelectedIndexChanged" target="_blank"></asp:DropDownList>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>







                                                                    <asp:TemplateField HeaderText="Adaptor Qty ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbqty" runat="server" AutoPostBack="true" Width="60px" Text='<%# Eval("qty") %>' CssClass="TextBox" OnTextChanged="txtbqty_TextChanged" OnClientClick="target ='_blank';"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Available Stock">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblavlqty" Width="60px" runat="server"></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Balance Stock">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtbalqty" Width="60px" runat="server"></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Type">
                                                                        <ItemTemplate>
                                                                            <asp:RadioButtonList ID="rbitemtype" runat="server">
                                                                                <asp:ListItem Text="Inhouse" Value="Inhouse"></asp:ListItem>
                                                                                <asp:ListItem Text="Outsource" Value="Outsource"></asp:ListItem>
                                                                                <asp:ListItem Text="Inhouse/Outsource" Value="Inhouse/Outsource"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="DrawingPath" HeaderText="DrawingPath" Visible="false">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="View Drawing">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnviewadpater" runat="server" ForeColor="Red" CssClass="Label" OnClick="btnviewadpater_Click" Text="View Drawing " OnClientClick="target ='_blank';" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Upload  Drawing">

                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="fl1" runat="server" ViewStateMode="Enabled" OnClientClick="target ='_blank';" />
                                                                            <asp:Button ID="btniupload" Text="Upload" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" target="_blank" />

                                                                            <asp:Button ID="Button5" Text="Upload Multiple" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Selectmulti" target="_blank" />
                                                                        </ItemTemplate>

                                                                        <EditItemTemplate></EditItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>
                                                            </asp:GridView>

                                                        </div>
                                                    </div>


                                                </div>

                                                <div class="panel panel-default" id="dvhandwheel" runat="server">
                                                    <div class="panel-heading" role="tab" id="heading4">
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapse4" aria-expanded="true"
                                                                aria-controls="collapse4">Handwheel </a>
                                                        </h4>
                                                    </div>


                                                    <div id="collapse4" class="panel-collapse collapse " role="tabpanel" aria-labelledby="heading4">
                                                        <div class="panel-body" style="overflow: scroll">
                                                            <asp:GridView ID="grdhandwheel" runat="server" CssClass="mGrid" OnRowUpdating="grdhandwheel_RowUpdating" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="grdhandwheel_RowDataBound" OnRowCommand="grdhandwheel_RowCommand">
                                                                <Columns>



                                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">

                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkbox" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Particulars">
                                                                        <ItemTemplate>
                                                                            <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                                Visible="false" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged1">
                                                                            </ajax:ComboBox>
                                                                            <%--THIS START HERE--%>
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="TxtItemNamehandwheel" runat="server" Text='<%# Eval("ItemName") %>'
                                                                                        CssClass="search_List" Width="292px" AutoPostBack="True"></asp:TextBox>
                                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemNamehandwheel" runat="server" TargetControlID="TxtItemNamehandwheel"
                                                                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                                        ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                                    </ajax:AutoCompleteExtender>
                                                                                    <ajax:TextBoxWatermarkExtender ID="TBWE2handwhhel" runat="server" TargetControlID="TxtItemNamehandwheel"
                                                                                        WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <%--THIS END HERE--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />


                                                                    </asp:TemplateField>





                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>


                                                                            <asp:DropDownList ID="ddlItemDescriptionhandwheel" runat="server" AutoPostBack="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlItemDescriptionhandwheel_SelectedIndexChanged" target="_blank"></asp:DropDownList>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Handwheel Qty ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbqty" AutoPostBack="true" Width="60px" Text='<%# Eval("qty") %>' runat="server" CssClass="TextBox" OnTextChanged="txtbqty_TextChanged1" OnClientClick="target ='_blank';"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>





                                                                    <asp:TemplateField HeaderText="Available Stock">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblavlqty" Width="60px" runat="server"></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Balance Stock">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtbalqty" Width="60px" runat="server"></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Type">
                                                                        <ItemTemplate>
                                                                            <asp:RadioButtonList ID="rbitemtype" runat="server">
                                                                                <asp:ListItem Text="Inhouse" Value="Inhouse"></asp:ListItem>
                                                                                <asp:ListItem Text="Outsource" Value="Outsource"></asp:ListItem>
                                                                                <asp:ListItem Text="Inhouse/Outsource" Value="Inhouse/Outsource"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>




                                                                    <asp:BoundField DataField="DrawingPath" HeaderText="DrawingPath" Visible="false">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="View Drawing">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnviewhandwheel" runat="server" ForeColor="Red" CssClass="Label" OnClick="btnviewhandwheel_Click" Text="View Drawing " OnClientClick="target ='_blank';" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Upload  Drawing">

                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="fl1" runat="server" ViewStateMode="Enabled" OnClientClick="target ='_blank';" />
                                                                            <asp:Button ID="btniupload" Text="Upload" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" target="_blank" />

                                                                            <asp:Button ID="Button5" Text="Upload Multiple" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Selectmulti" target="_blank" />
                                                                        </ItemTemplate>

                                                                        <EditItemTemplate></EditItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>
                                                            </asp:GridView>

                                                        </div>
                                                    </div>

                                                </div>


                                                <div class="panel panel-default" id="dvlever" runat="server">
                                                    <div class="panel-heading" role="tab" id="heading5">
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapse5" aria-expanded="true"
                                                                aria-controls="collapse5">Lever </a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse5" class="panel-collapse collapse " role="tabpanel" aria-labelledby="heading5">
                                                        <div class="panel-body" style="overflow: scroll">
                                                            <asp:GridView ID="grdlever" runat="server" CssClass="mGrid" OnRowUpdating="grdlever_RowUpdating" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="grdlever_RowDataBound" OnRowCommand="grdlever_RowCommand">
                                                                <Columns>



                                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">

                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkbox" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Particulars">
                                                                        <ItemTemplate>
                                                                            <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                                Visible="false" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged1">
                                                                            </ajax:ComboBox>
                                                                            <%--THIS START HERE--%>
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="TxtItemNamelever" runat="server" Text='<%# Eval("ItemName") %>'
                                                                                        CssClass="search_List" Width="292px" AutoPostBack="True"></asp:TextBox>
                                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemNamelever" runat="server" TargetControlID="TxtItemNamelever"
                                                                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                                        ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                                    </ajax:AutoCompleteExtender>
                                                                                    <ajax:TextBoxWatermarkExtender ID="TBWE2lever" runat="server" TargetControlID="TxtItemNamelever"
                                                                                        WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <%--THIS END HERE--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />


                                                                    </asp:TemplateField>





                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>


                                                                            <asp:DropDownList ID="ddlItemDescriptionlever" runat="server" AutoPostBack="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlItemDescriptionlever_SelectedIndexChanged" target="_blank"></asp:DropDownList>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Lever Qty ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbqty" Width="60px" Text='<%# Eval("qty") %>' runat="server" CssClass="TextBox"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Lever length for unit Qty (MM) ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtlverunitqty" Width="60px" AutoPostBack="true" runat="server" CssClass="TextBox" OnTextChanged="txtlverunitqty_TextChanged" OnClientClick="target ='_blank';"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>







                                                                    <asp:TemplateField HeaderText="Total Lever Length (Mtr) ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltotalleverlength" Width="60px" runat="server"></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Available Stock">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblavlqty" Width="60px" runat="server"></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Balance Stock">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtbalqty" Width="60px" runat="server"></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Type">
                                                                        <ItemTemplate>
                                                                            <asp:RadioButtonList ID="rbitemtype" runat="server">
                                                                                <asp:ListItem Text="Inhouse" Value="Inhouse"></asp:ListItem>
                                                                                <asp:ListItem Text="Outsource" Value="Outsource"></asp:ListItem>
                                                                                <asp:ListItem Text="Inhouse/Outsource" Value="Inhouse/Outsource"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:BoundField DataField="DrawingPath" HeaderText="DrawingPath" Visible="false">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="View Drawing">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnviewlever" runat="server" ForeColor="Red" CssClass="Label" OnClick="btnviewlever_Click" Text="View Drawing " OnClientClick="target ='_blank';" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Upload  Drawing">

                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="fl1" runat="server" ViewStateMode="Enabled" OnClientClick="target ='_blank';" />
                                                                            <asp:Button ID="btniupload" Text="Upload" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" target="_blank" />

                                                                            <asp:Button ID="Button5" Text="Upload Multiple" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Selectmulti" target="_blank" />
                                                                        </ItemTemplate>

                                                                        <EditItemTemplate></EditItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>

                                                </div>




                                                <div class="panel panel-default" id="dvhook" runat="server">
                                                    <div class="panel-heading" role="tab" id="heading10">
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapse10" aria-expanded="true"
                                                                aria-controls="collapse10">U-Hook & SS chain </a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse10" class="panel-collapse collapse " role="tabpanel" aria-labelledby="heading10">
                                                        <div class="panel-body" style="overflow: scroll">
                                                            <asp:GridView ID="grdsschain" runat="server" CssClass="mGrid" OnRowUpdating="grdsschain_RowUpdating" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="grdsschain_RowDataBound" OnRowCommand="grdsschain_RowCommand">
                                                                <Columns>





                                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">

                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkbox" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Particulars">
                                                                        <ItemTemplate>
                                                                            <ajax:ComboBox ID="ddlItemsschain" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                                Visible="false" OnSelectedIndexChanged="ddlItemsschain_SelectedIndexChanged">
                                                                            </ajax:ComboBox>
                                                                            <%--THIS START HERE--%>
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="TxtItemNamesschain" runat="server" Text='<%# Eval("ItemName") %>'
                                                                                        CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemNamesschain_TextChanged"></asp:TextBox>
                                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemNamesschain" runat="server" TargetControlID="TxtItemNamesschain"
                                                                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                                        ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                                    </ajax:AutoCompleteExtender>
                                                                                    <ajax:TextBoxWatermarkExtender ID="TBWE2sschain" runat="server" TargetControlID="TxtItemNamesschain"
                                                                                        WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <%--THIS END HERE--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <FooterStyle HorizontalAlign="Center" />

                                                                        <FooterTemplate>
                                                                            <asp:Button ID="btnaddfooter" runat="server" Text="+"
                                                                                OnClick="btnaddfooter_Click" />
                                                                        </FooterTemplate>

                                                                    </asp:TemplateField>





                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>


                                                                            <asp:DropDownList ID="ddlItemDescriptionsschain" runat="server" AutoPostBack="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlItemDescriptionsschain_SelectedIndexChanged" target="_blank"></asp:DropDownList>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>










                                                                    <asp:TemplateField HeaderText=" Qty ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbqty" Text='<%# Eval("qty") %>' Width="60px" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText=" UOM ">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="drpsschain" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpsschain_SelectedIndexChanged" OnClientClick="target ='_blank';">
                                                                                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                                                <asp:ListItem Text="Mtr" Value="Mtr"></asp:ListItem>
                                                                                <asp:ListItem Text="Nos" Value="Nos"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Available Stock">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblavlqty" Width="60px" runat="server"></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Balance Stock">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblbalqty" Width="60px" runat="server"></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <%-- <asp:BoundField DataField="DrawingPath" HeaderText="DrawingPath" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="View Drawing">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnviewtagnonstdhardware" runat="server" ForeColor="Red" CssClass="Label" OnClick="btnviewtagnonstdhardware_Click" Text="View Drawing " OnClientClick="target ='_blank';" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Upload  Drawing">

                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="fl1" runat="server" ViewStateMode="Enabled" />
                                                            <asp:Button ID="btniupload" Text="Upload" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" />

                                                            <asp:Button ID="Button5" Text="Upload Multiple" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Selectmulti" />
                                                        </ItemTemplate>

                                                        <EditItemTemplate></EditItemTemplate>
                                                    </asp:TemplateField>--%>
                                                                </Columns>
                                                            </asp:GridView>

                                                        </div>
                                                    </div>

                                                </div>

                                                <br />
                                                <br />

                                                <hr />


                                                <div class="panel panel-default">
                                                    <div class="panel-heading" role="tab" id="heading7">
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapse7" aria-expanded="true"
                                                                aria-controls="collapse7">WoodenBox Sizes </a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse7" class="panel-collapse collapse " role="tabpanel" aria-labelledby="heading7">
                                                        <div class="panel-body" style="overflow: scroll">
                                                            <asp:GridView ID="grdwoodenbox" runat="server" CssClass="mGrid" OnRowUpdating="grdwoodenbox_OnRowUpdating" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="grdwoodenbox_OnRowDataBound" OnRowCommand="grdwoodenbox_OnRowCommand">
                                                                <Columns>



                                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">

                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkbox" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Particulars">
                                                                        <ItemTemplate>
                                                                            <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                                Visible="false" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged1">
                                                                            </ajax:ComboBox>
                                                                            <%--THIS START HERE--%>
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="TxtItemNamewooden" runat="server" Text='<%# Eval("ItemName") %>'
                                                                                        CssClass="search_List" Width="292px" AutoPostBack="True"></asp:TextBox>
                                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemNamwood" runat="server" TargetControlID="TxtItemNamewooden"
                                                                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                                        ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                                    </ajax:AutoCompleteExtender>
                                                                                    <ajax:TextBoxWatermarkExtender ID="TBWE2wood" runat="server" TargetControlID="TxtItemNamewooden"
                                                                                        WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <%--THIS END HERE--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />


                                                                    </asp:TemplateField>





                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>


                                                                            <asp:DropDownList ID="ddlItemDescriptionwood" runat="server" AutoPostBack="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle"></asp:DropDownList>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>








                                                                    <asp:TemplateField HeaderText="Wooden Box Sizes ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtwood" runat="server" Width="60px" Text='<%# Eval("WoodenBoxSize") %>' CssClass="TextBox"></asp:TextBox>
                                                                        </ItemTemplate>



                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="WoodenBox Qty ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbqty" Text='<%# Eval("qty") %>' Width="60px" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:BoundField DataField="DrawingPath" HeaderText="DrawingPath" Visible="false">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="View Drawing">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnviewwood" runat="server" ForeColor="Red" CssClass="Label" OnClick="btnviewwood_OnClick" Text="View Drawing " OnClientClick="target ='_blank';" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Upload  Drawing">

                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="fl1" runat="server" ViewStateMode="Enabled" OnClientClick="target ='_blank';" />
                                                                            <asp:Button ID="btniupload" Text="Upload" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" target="_blank" />

                                                                            <asp:Button ID="Button5" Text="Upload Multiple" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Selectmulti" target="_blank" />
                                                                        </ItemTemplate>

                                                                        <EditItemTemplate></EditItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>

                                                    </div>

                                                </div>





                                                <div class="panel panel-default">
                                                    <div class="panel-heading" role="tab" id="heading8">
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapse8" aria-expanded="true"
                                                                aria-controls="collapse8">Tag Plate </a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse8" class="panel-collapse collapse " role="tabpanel" aria-labelledby="heading8">
                                                        <div class="panel-body" style="overflow: scroll">
                                                            <asp:GridView ID="grdtagplate" runat="server" CssClass="mGrid" OnRowUpdating="grdtagplate_RowUpdating" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="grdtagplate_RowDataBound" OnRowCommand="grdtagplate_RowCommand">
                                                                <Columns>



                                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">

                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkbox" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Particulars">
                                                                        <ItemTemplate>
                                                                            <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                                Visible="false" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged1">
                                                                            </ajax:ComboBox>
                                                                            <%--THIS START HERE--%>
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="TxtItemNametagplate" runat="server" Text='<%# Eval("ItemName") %>'
                                                                                        CssClass="search_List" Width="292px" AutoPostBack="True"></asp:TextBox>
                                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemNamtagplate" runat="server" TargetControlID="TxtItemNametagplate"
                                                                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                                        ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                                    </ajax:AutoCompleteExtender>
                                                                                    <ajax:TextBoxWatermarkExtender ID="TBWE2tagplate" runat="server" TargetControlID="TxtItemNametagplate"
                                                                                        WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <%--THIS END HERE--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />


                                                                    </asp:TemplateField>





                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>


                                                                            <asp:DropDownList ID="ddlItemDescriptiontagplate" runat="server" AutoPostBack="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle"></asp:DropDownList>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>








                                                                    <asp:TemplateField HeaderText="Tag Sizes ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtwood" runat="server" Width="60px" Text='<%# Eval("WoodenBoxSize") %>' CssClass="TextBox"></asp:TextBox>
                                                                        </ItemTemplate>



                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Tag Plate Qty ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbqty" Text='<%# Eval("qty") %>' Width="60px" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>


                                                                    <asp:BoundField DataField="DrawingPath" HeaderText="DrawingPath" Visible="false">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="View Drawing">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnviewtagplate" runat="server" ForeColor="Red" CssClass="Label" OnClick="btnviewtagplate_Click" Text="View Drawing " OnClientClick="target ='_blank';" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Upload  Drawing">

                                                                        <ItemTemplate>
                                                                            <asp:FileUpload ID="fl1" runat="server" ViewStateMode="Enabled" OnClientClick="target ='_blank';" />
                                                                            <asp:Button ID="btniupload" Text="Upload" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" target="_blank" />

                                                                            <asp:Button ID="Button5" Text="Upload Multiple" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Selectmulti" target="_blank" />
                                                                        </ItemTemplate>

                                                                        <EditItemTemplate></EditItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>
                                                            </asp:GridView>

                                                        </div>
                                                    </div>

                                                </div>











                                                <div class="panel panel-default">
                                                    <div class="panel-heading" role="tab" id="heading9">
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapse9" aria-expanded="true"
                                                                aria-controls="collapse9">Non-std items hardware  </a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse9" class="panel-collapse collapse " role="tabpanel" aria-labelledby="heading9">
                                                        <div class="panel-body" style="overflow: scroll">
                                                            <asp:GridView ID="grdnonstdhardware" runat="server" CssClass="mGrid" OnRowUpdating="grdnonstdhardware_RowUpdating" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="grdnonstdhardware_RowDataBound" OnRowCommand="grdnonstdhardware_RowCommand">
                                                                <Columns>





                                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">

                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkbox" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Particulars">
                                                                        <ItemTemplate>
                                                                            <ajax:ComboBox ID="ddlItemnonstdhardware" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                                Visible="false" OnSelectedIndexChanged="ddlItemnonstdhardware_SelectedIndexChanged">
                                                                            </ajax:ComboBox>
                                                                            <%--THIS START HERE--%>
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="TxtItemNamenonstdhardware" runat="server" Text='<%# Eval("ItemName") %>'
                                                                                        CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemNamenonstdhardware_TextChanged"></asp:TextBox>
                                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemNamnonstdhardware" runat="server" TargetControlID="TxtItemNamenonstdhardware"
                                                                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                                        ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                                    </ajax:AutoCompleteExtender>
                                                                                    <ajax:TextBoxWatermarkExtender ID="TBWE2tagnonstdhardware" runat="server" TargetControlID="TxtItemNamenonstdhardware"
                                                                                        WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <%--THIS END HERE--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <FooterStyle HorizontalAlign="Center" />

                                                                        <FooterTemplate>
                                                                            <asp:Button ID="ButtonAdd123" runat="server" Text="+"
                                                                                OnClick="ButtonAdd123_Click" />
                                                                        </FooterTemplate>

                                                                    </asp:TemplateField>





                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>


                                                                            <asp:DropDownList ID="ddlItemDescriptiontagnonstdhardware" runat="server" AutoPostBack="true" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlItemDescriptiontagnonstdhardware_SelectedIndexChanged" target="_blank"></asp:DropDownList>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>










                                                                    <asp:TemplateField HeaderText=" Qty ">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbqty" Text='<%# Eval("qty") %>' Width="60px" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Available Stock">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblavlqty" Width="60px" runat="server"></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                    <%-- <asp:BoundField DataField="DrawingPath" HeaderText="DrawingPath" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="View Drawing">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnviewtagnonstdhardware" runat="server" ForeColor="Red" CssClass="Label" OnClick="btnviewtagnonstdhardware_Click" Text="View Drawing " OnClientClick="target ='_blank';" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Upload  Drawing">

                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="fl1" runat="server" ViewStateMode="Enabled" />
                                                            <asp:Button ID="btniupload" Text="Upload" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" />

                                                            <asp:Button ID="Button5" Text="Upload Multiple" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Selectmulti" />
                                                        </ItemTemplate>

                                                        <EditItemTemplate></EditItemTemplate>
                                                    </asp:TemplateField>--%>
                                                                </Columns>
                                                            </asp:GridView>

                                                        </div>
                                                    </div>

                                                </div>



                                                <asp:GridView ID="GrdRequisition" runat="server" AutoGenerateColumns="False" CssClass="mGrid1" Visible="false"
                                                    DataKeyNames="#" OnRowCommand="GrdRequisition_RowCommand" OnRowDataBound="GrdRequisition_RowDataBound"
                                                    OnRowDeleting="GrdRequisition_RowDeleting" ShowFooter="true" OnDataBound="GrdRequisition_DataBound">
                                                    <Columns>
                                                        <%-- 0--%>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="BtnAdd" runat="server" CommandName="InsertNew" TabIndex="5"
                                                                    ValidationGroup="Add" ImageUrl="~/Images/Icon/Gridadd.png" OnClick="BtnAdd_Click" />
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                            <FooterStyle CssClass="" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <%-- 1--%>
                                                        <asp:TemplateField HeaderText="All">
                                                            <HeaderTemplate>
                                                                <asp:Button ID="BtnShowAllAvQty" runat="server" Text="GetAllAvQty" CssClass="button" OnClick="BtnShowAllAvQty_Click" />
                                                                <asp:CheckBox ID="GrdSelectAllHeader" runat="server" AutoPostBack="true" OnCheckedChanged="GrdSelectAllHeader_CheckedChanged"
                                                                    Visible="false" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Button ID="BtnShowAvQty" runat="server" Text="GetAvQty" CssClass="button" OnClick="BtnShowAvQty_Click" />
                                                                <asp:CheckBox ID="GrdSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="GrdSelectAll_CheckedChanged" Visible="false" />
                                                                <asp:ImageButton ID="ImgBtnBlocked" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                    ImageUrl="~/Images/Icon/bolcked.png" ToolTip="Item Cancelled" />
                                                                <asp:ImageButton ID="ImgBtnDelete" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                    CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                                    TargetControlID="ImgBtnDelete">
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
                                                                <ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                    AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4"
                                                                    ItemInsertLocation="Append" Width="120px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                                </ajax:ComboBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Display_None" />
                                                            <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Display_None" />
                                                        </asp:TemplateField>
                                                        <%--4--%>
                                                        <asp:TemplateField HeaderText="SubCategory">
                                                            <ItemTemplate>
                                                                <ajax:ComboBox ID="ddlSubCategory" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                    AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4"
                                                                    ItemInsertLocation="Append" Width="150px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                                                </ajax:ComboBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Display_None" />
                                                            <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="Display_None" />
                                                        </asp:TemplateField>
                                                        <%-- 5--%>
                                                        <asp:TemplateField HeaderText="Particulars">
                                                            <ItemTemplate>
                                                                <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                    Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                    TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                    Visible="false" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                                                </ajax:ComboBox>
                                                                <%--THIS START HERE--%>
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="TxtItemName" runat="server" Text='<%# Eval("ItemName") %>' ToolTip='<%# Eval("ItemToolTip") %>'
                                                                            CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemName1_TextChanged"></asp:TextBox>
                                                                        <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemName" runat="server" TargetControlID="TxtItemName"
                                                                            CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                            ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                            CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                        </ajax:AutoCompleteExtender>
                                                                        <ajax:TextBoxWatermarkExtender ID="TBWE21" runat="server" TargetControlID="TxtItemName"
                                                                            WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                <%--THIS END HERE--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <%-- 6--%>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <ajax:ComboBox ID="ddlItemDescription" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                    AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4"
                                                                    Height="25px" ItemInsertLocation="Append" Width="200px" CssClass="CustomComboBoxStyle"
                                                                    OnSelectedIndexChanged="ddlItemDescription_SelectedIndexChanged">
                                                                </ajax:ComboBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlItemDescription"
                                                                    Display="None" Enabled="true" ErrorMessage="Please Select Description" SetFocusOnError="true"
                                                                    InitialValue=" --Select ItemDescription--" ValidationGroup="Add">
                                                                </asp:RequiredFieldValidator>
                                                                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="true"
                                                                    TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                                </ajax:ValidatorCalloutExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <%-- 7--%>
                                                        <asp:BoundField DataField="Location" HeaderText="Location">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" CssClass="Display_None" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" CssClass="Display_None" />
                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="true" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <%--8--%>
                                                        <asp:BoundField DataField="AvlQty" HeaderText="Avl.Qty">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="true" />
                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="true" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <%-- 9--%>
                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <ajax:ComboBox ID="ddlUnitConvertor" runat="server" DropDownStyle="DropDown" AutoPostBack="false"
                                                                    AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4"
                                                                    Height="15px" ItemInsertLocation="Append" Width="100px" CssClass="CustomComboBoxStyle">
                                                                </ajax:ComboBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <%-- 10--%>
                                                        <asp:BoundField DataField="TransitQty" HeaderText="Transit Qty">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        <%-- 11--%>
                                                        <asp:BoundField DataField="MinStockLevel" HeaderText="Min Stock">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="true" />
                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="true" />
                                                        </asp:BoundField>
                                                        <%--12--%>
                                                        <asp:BoundField DataField="MaxStockLevel" HeaderText="Max Stock">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                                        </asp:BoundField>
                                                        <%--13--%>
                                                        <asp:BoundField DataField="AvgRate" HeaderText="Rate">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <%--14--%>
                                                        <asp:BoundField DataField="AvgRateDate" HeaderText="Date">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <%--15--%>
                                                        <asp:BoundField DataField="Vendor" HeaderText="Vendor">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <%--16--%>
                                                        <asp:BoundField DataField="Rate" HeaderText="Rate">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <%--17--%>
                                                        <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Order Qty">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOrdQty" runat="server" CssClass="TextBox" AutoPostBack="false"
                                                                    Text='<%# Eval("txtOrdQty") %>' Width="50px" TabIndex="4"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="FTE_Mobile" runat="server" TargetControlID="txtOrdQty"
                                                                    FilterType="Custom,Numbers" ValidChars=".">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle Wrap="true" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:TemplateField>
                                                        <%--18--%>
                                                        <asp:TemplateField HeaderText="VendorId">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVendorId" runat="server" Text='<%# Eval("VendorId") %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:TemplateField>
                                                        <%--19--%>
                                                        <asp:BoundField DataField="ItemID" HeaderText="ItemID">
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <%--20--%>
                                                        <asp:TemplateField HeaderText="Available Quantity">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAvlQty" runat="server" CssClass="Display_None" Text='<%# Eval("txtAvlQty") %>'
                                                                    Width="80px"> </asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:TemplateField>
                                                        <%--21--%>
                                                        <%--Adding Priority to Grid--%>
                                                        <asp:TemplateField HeaderText="Priority">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Priority" runat="server" Text='<%# Eval("Priority") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                        </asp:TemplateField>
                                                        <%--Add Priority To Form--%>
                                                        <%--22--%>
                                                        <asp:TemplateField HeaderText="Set Priority">
                                                            <ItemTemplate>
                                                                <div style="">
                                                                    <asp:Image ID="ImgBtnEdit" runat="server" CssClass="Imagebutton" ImageUrl="~/Images/Icon/Gridadd.png"
                                                                        TabIndex="4" ToolTip="Set Priority" />
                                                                    <ajax:PopupControlExtender ID="popup" runat="server" PopupControlID="PnlGrid" Position="Right"
                                                                        CommitProperty="Value" TargetControlID="ImgBtnEdit" DynamicServicePath="" Enabled="True"
                                                                        ExtenderControlID="">
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
                                                                                                                    <fieldset id="FS_TemplatePriorityDetails" class="FieldSet fieldset1" runat="server" style="width: 100%">
                                                                                                                        <legend id="Lgd_TemplatePriorityDetails" class="legend" runat="server">Select Priority</legend>
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                                            <ContentTemplate>
                                                                                                                                <asp:GridView ID="GridTemplatePriority" runat="server" AutoGenerateColumns="False"
                                                                                                                                    DataKeyNames="#" CssClass="mGrid">
                                                                                                                                    <Columns>
                                                                                                                                        <asp:TemplateField HeaderText="#" Visible="False">
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:TemplateField>
                                                                                                                                        <asp:BoundField DataField="PriorityId" HeaderText="PriorityId">
                                                                                                                                            <HeaderStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                                                                                                                Wrap="False" />
                                                                                                                                            <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                                                                                                                Wrap="False" />
                                                                                                                                        </asp:BoundField>
                                                                                                                                        <asp:TemplateField HeaderText="Select">
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:RadioButton runat="server" ID="RBPriority" GroupName="Prioritygroup" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
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
                                                                                                <asp:CheckBox ID="ChkShowProcess" runat="server" Text="Add Priority" OnCheckedChanged="ChkShowProcess_CheckedChanged"
                                                                                                    CssClass="CheckBox" AutoPostBack="True" meta:resourcekey="ChkShowProcessResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle Wrap="true" CssClass="Display_None" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:TemplateField>
                                                        <%--23--%>
                                                        <%--End Adding Priority--%>
                                                        <asp:BoundField DataField="IsCancel" HeaderText="IsCancel">
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <%--24--%>
                                                        <asp:TemplateField HeaderText="PriorityID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="PriorityID" runat="server" Text='<%# Eval("PriorityID") %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:TemplateField>
                                                        <%--25--%>
                                                        <asp:BoundField DataField="CategoryId" HeaderText="CategoryId">
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <%--26--%>
                                                        <asp:BoundField DataField="SubcategoryId" HeaderText="SubcategoryId">
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <%--27--%>
                                                        <asp:BoundField DataField="ItemDetailsId" HeaderText="ItemDetailsId">
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:BoundField>
                                                        <%--28--%>
                                                        <asp:BoundField DataField="UnitConvDtlsId" HeaderText="UnitConvDtlsId">
                                                            <HeaderStyle CssClass="Display_None" />
                                                            <ItemStyle CssClass="Display_None" />
                                                            <FooterStyle CssClass="Display_None" />
                                                        </asp:BoundField>

                                                        <asp:TemplateField HeaderText="Required Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRequiredDate" runat="server" Text='<%# Eval("RequiredDate") %>' TabIndex="1"
                                                                    Width="100px" AutoPostBack="true"></asp:TextBox>
                                                                <ajax:CalendarExtender ID="CE1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtRequiredDate"
                                                                    TargetControlID="txtRequiredDate" CssClass="" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remark">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="GrdRemark" runat="server" CssClass="TextBox" Text='<%# Eval("Remark") %>'
                                                                    TextMode="MultiLine" TabIndex="4"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:BoundField DataField="DrawingPath" HeaderText="DrawingPath" Visible="false">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="View Drawing">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkView" CommandName="VIEW" PostBackUrl='<%# "Drawing.aspx?RowIndex=" +
                            Container.DataItemIndex %>'
                                                                    target="_blank" CssClass="Display_None">View Deal</asp:LinkButton>
                                                                <a href='<%# "../Reports/Drawing.aspx?RowIndex=" +
                            Container.DataItemIndex + "&FormName=MaterialRequisitionTemplate" %>'
                                                                    target="_blank">View Drawing PDF</a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>




                                                    </Columns>
                                                    <FooterStyle CssClass="ftr" />
                                                    <PagerStyle CssClass="pgr" />
                                                </asp:GridView>




                                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" CssClass="mGrid1"
                                                    DataKeyNames="TemplateID">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgOrdersShow" runat="server"
                                                                    ImageUrl="~/images/plus.png" CommandArgument="Show" />
                                                                <asp:Panel ID="pnlOrders" runat="server" Visible="false" Style="position: relative">
                                                                    <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false"
                                                                        CssClass="mGrid"
                                                                        DataKeyNames="ItemName">
                                                                        <Columns>





                                                                            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkboxSelectAll" AutoPostBack="true" Checked="true" runat="server" Text="Select All" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkbox" AutoPostBack="true" runat="server" />
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


                                                                            <asp:TemplateField HeaderText="ItemID">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitem" runat="server" Text='<%# Eval("ItemID") %>' Width="30px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="Display_None" />
                                                                                <ItemStyle CssClass="Display_None" />
                                                                                <FooterStyle CssClass="Display_None" />
                                                                            </asp:TemplateField>




                                                                            <%--  <asp:BoundField ItemStyle-Width="150px" DataField="ItemID" HeaderText="ItemID" Visible="false" />--%>

                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="SubcategoryId" HeaderText="SubcategoryId" Visible="false" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="CategoryId" HeaderText="CategoryId" Visible="false" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="CategoryName" Visible="false" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="SubCategory" HeaderText="SubCategory" Visible="false" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="ItemCode" HeaderText="ItemCode" Visible="false" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="AvlQty" HeaderText="AvlQty" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="TransitQty" HeaderText="TransitQty" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="MinStockLevel" HeaderText="MinStockLevel" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="MaxStockLevel" HeaderText="MaxStockLevel" />



                                                                            <asp:TemplateField HeaderText="Order Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtorderqty" runat="server" Text='<%# Bind("txtOrdQty") %>' Enabled="true"></asp:TextBox>


                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtorderqty" runat="server" Text='<%# Bind("txtOrdQty") %>' Enabled="true"></asp:TextBox>
                                                                                </EditItemTemplate>

                                                                            </asp:TemplateField>
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="DeliveryPeriod" HeaderText="DeliveryPeriod" Visible="false" />



                                                                            <asp:TemplateField HeaderText="ItemDetailsId">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemDetailsId" runat="server" Text='<%# Bind("ItemDetailsId") %>' Enabled="false"></asp:Label>


                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:Label ID="lblItemDetailsId" runat="server" Text='<%# Bind("ItemDetailsId") %>' Enabled="false"></asp:Label>
                                                                                </EditItemTemplate>

                                                                            </asp:TemplateField>



                                                                            <asp:TemplateField HeaderText="VendorId">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVendorId" runat="server" Text='<%# Eval("VendorId") %>' Width="30px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="Display_None" />
                                                                                <ItemStyle CssClass="Display_None" />
                                                                                <FooterStyle CssClass="Display_None" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Unit">
                                                                                <ItemTemplate>
                                                                                    <ajax:ComboBox ID="ddlUnitConvertor" runat="server" DropDownStyle="DropDown" AutoPostBack="false"
                                                                                        AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" TabIndex="4"
                                                                                        Height="15px" ItemInsertLocation="Append" Width="100px" CssClass="CustomComboBoxStyle">
                                                                                        <asp:ListItem Text="Nos" Value="1" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Kgs" Value="2"></asp:ListItem>
                                                                                        <asp:ListItem Text="Ltr" Value="3"></asp:ListItem>
                                                                                        <asp:ListItem Text="Roll" Value="4"></asp:ListItem>
                                                                                    </ajax:ComboBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                            </asp:TemplateField>

                                                                            <%--     <asp:BoundField ItemStyle-Width="150px" DataField="UnitConvDtlsId" HeaderText="UnitConvDtlsId"  Visible="false"/>--%>
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="AvgRate" HeaderText="AvgRate" Visible="false" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="AvgRateDate" HeaderText="AvgRateDate" Visible="false" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="Vendor" HeaderText="Vendor" Visible="false" />



                                                                            <asp:TemplateField HeaderText="Rate">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblrate" runat="server" Text='<%# Eval("Rate") %>' Width="30px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="Display_None" />
                                                                                <ItemStyle CssClass="Display_None" />
                                                                                <FooterStyle CssClass="Display_None" />
                                                                            </asp:TemplateField>

                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="Priority" HeaderText="Priority" Visible="false" />
                                                                            <asp:BoundField DataField="IsCancel" HeaderText="IsCancel">
                                                                                <HeaderStyle CssClass="Display_None" />
                                                                                <ItemStyle CssClass="Display_None" />
                                                                                <FooterStyle CssClass="Display_None" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="PriorityID" HeaderText="PriorityID" Visible="false" />


                                                                            <asp:TemplateField HeaderText="#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField HeaderText="Remark">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="GrdRemark" runat="server" CssClass="TextBox"
                                                                                        TextMode="MultiLine" TabIndex="4"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>




                                                                            <asp:BoundField DataField="DrawingPath" HeaderText="DrawingPath" Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="View Drawing">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" ID="lnkView" CommandName="VIEW" PostBackUrl='<%# "Drawing.aspx?RowIndex=" +
                            Container.DataItemIndex %>'
                                                                                        target="_blank" CssClass="Display_None">View Deal</asp:LinkButton>
                                                                                    <a href='<%# "../Reports/Drawing.aspx?RowIndex=" +
                            Container.DataItemIndex + "&FormName=MaterialRequisitionTemplate1" %>'
                                                                                        target="_blank">View Drawing PDF</a>
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



                                        </div>


                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
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
                                    <td colspan="1" class="Label" align="right">
                                        <asp:Button CssClass="Display_None" ID="BtnRefresh" runat="server" Text="Amount" ToolTip="Get Total Requisition Amount"
                                            OnClick="BtnRefresh_Click" TabIndex="7" />

                                        <asp:TextBox ID="lblTotalAmt" runat="server" CssClass="Display_None" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>


                                <asp:HiddenField ID="PaneName" runat="server" />
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
                <tr class="tr1">
                    <td class="td1">
                        <fieldset id="F3" runat="server" width="100%" class="fieldset1">
                            <table width="100%" cellspacing="8">
                                <tr>
                                    <td align="center" colspan="6">
                                        <table width="25%">
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="BtnUpdate" runat="server" CausesValidation="true" TabIndex="8" CssClass="button"
                                                        OnClick="BtnUpdate_Click" Text="Update" />
                                                    <ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
                                                        TargetControlID="BtnUpdate">
                                                    </ajax:ConfirmButtonExtender>
                                                </td>


                                                <td>
                                                    <asp:Button ID="btntempsave" runat="server" CausesValidation="false" TabIndex="8" CssClass="button"
                                                        OnClick="btntempsave_Click" Text="Temp Save" ValidationGroup="Add" Width="100px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="Btnshow" runat="server" CausesValidation="false" TabIndex="8" CssClass="button"
                                                        OnClick="Btnshow_Click" Text="Show Indent" ValidationGroup="Add" Width="100px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnSave" runat="server" CausesValidation="false" TabIndex="8" CssClass="button"
                                                        OnClick="BtnSave_Click1" Text="Save" ValidationGroup="Add" Width="100px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" TabIndex="8" CssClass="button"
                                                        OnClick="BtnCancel_Click" Text="Cancel" Width="100px" />
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
                        <td class="td1">
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
                                    <asp:GridView ID="ReportGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CssClass="mGrid" DataKeyNames="#,POTotQty,IndentTotQty" OnRowCommand="ReportGrid_RowCommand"
                                        OnRowDeleting="ReportGrid_RowDeleting" OnPageIndexChanging="ReportGrid_PageIndexChanging"
                                        OnRowDataBound="ReportGrid_RowDataBound" OnDataBound="ReportGrid_DataBound">
                                        <Columns>










                                            <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgOrdersShow" runat="server" OnClick="imgOrdersShow_Click"
                                                            ImageUrl="~/images/plus.png" CommandArgument="Show" />
                                                        <asp:Panel ID="pnlOrders" runat="server" Visible="false" Style="position: relative">
                                                            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" OnRowCommand="gvOrders_RowCommand"
                                                                CssClass="mGrid" OnRowDataBound="gvOrders_RowDataBound">
                                                                
                                                               <Columns>
                                                                     <asp:TemplateField>
                                                <ItemTemplate>
                                                   
                                                     <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("RequisitionCafeId")%>&Flag=<%="RS1"%>&PDFFlag=<%="PDF"%>'
                                                        target="_blank">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png" TabIndex="29"
                                                            ToolTip="PDF of Indent Register" />
                                                    </a>
                                                   
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Wrap="false" />
                                            </asp:TemplateField>

                                                                   <asp:BoundField DataField="RequisitionNo" HeaderText="Indent No">
                                                                       <HeaderStyle />
                                                                       <ItemStyle />
                                                                   </asp:BoundField>

                                                                   <asp:BoundField DataField="ReqStatus" HeaderText="Req.Status">
                                                                       <HeaderStyle />
                                                                       <ItemStyle />
                                                                   </asp:BoundField>


                                                               </Columns>
                                                            </asp:GridView>
                                                       
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
                                                    <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RS1"%>&PDFFlag=<%="NOPDF"%>'
                                                        target="_blank">
                                                        <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
                                                            TabIndex="29" ToolTip="Print Indent Register" />
                                                    </a>
                                                    <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RS1"%>&PDFFlag=<%="PDF"%>'
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


        <Triggers>
            <asp:PostBackTrigger ControlID="grditem" />
            <asp:PostBackTrigger ControlID="grdadapter" />
            <asp:PostBackTrigger ControlID="grdhandwheel" />
            <asp:PostBackTrigger ControlID="grdlever" />

            <asp:PostBackTrigger ControlID="grdplate" />
            <asp:PostBackTrigger ControlID="grdwoodenbox" />
            <asp:PostBackTrigger ControlID="grdtagplate" />
            <asp:PostBackTrigger ControlID="grdnonstdhardware" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
