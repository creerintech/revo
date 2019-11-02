<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true"
    CodeFile="ItemMaster.aspx.cs" Inherits="Masters_ItemMaster" Title="Item Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
    <input type="hidden" id="hiddenbox" runat="server" value="" />
    <input type="hidden" id="hiddenbox1" runat="server" value="" />
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
    <style type="text/css">
        .water
        {
            color: Gray;
        }
    </style>

    <script type="text/javascript" language="javascript">

        function ChangeFunc() {

            var _TXTUNITQTY = document.getElementById('<%= TXTUNITQTY.ClientID %>');

            if (_TXTUNITQTY.value == "" || isNaN(_TXTUNITQTY.value) || _TXTUNITQTY.value == 0) {
                _TXTUNITQTY.value = 1;
            }
        }
    </script>

    <script type="text/javascript" language="javascript">

        function ChangeFunc1() {

            var _TXTSUBUNITQTY = document.getElementById('<%= TXTSUBUNITQTY.ClientID %>');

            if (_TXTSUBUNITQTY.value == "" || isNaN(_TXTSUBUNITQTY.value)) {
                _TXTSUBUNITQTY.value = 0;
            }
        }
    </script>

    <script type="text/javascript" language="javascript">

        function DeleteEquipFunction() {
            var gridViewCtlId = '<%=GridDetails.ClientID%>';
            var ctlGridViewItems = null;
            var ItemID;
            ctlGridViewItems = document.getElementById(gridViewCtlId);
            ItemID = ctlGridViewItems.rows[1].cells[2].innerText;
            alert(ItemID);
            if (ItemID == 0) {
                if (confirm("There is no record to delete") == true) {
                    document.getElementById('<%= hiddenbox.ClientID%>').value = "0";
                }
                else {
                    document.getElementById('<%= hiddenbox.ClientID%>').value = "0";
                }
            }
            else {
                if (confirm("Are you sure you want to delete?") == true) {
                    document.getElementById('<%= hiddenbox.ClientID%>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%= hiddenbox.ClientID%>').value = "0";
                    return false;
                }
            }
        }
        function openPopup() {

            window.open("YOURpopuppage.aspx", "_blank", "WIDTH=1080,HEIGHT=790,scrollbars=no, menubar=no,resizable=yes,directories=no,location=no");
        }

        function CalculateNetAmtForGrid() {

            var _GridDetails = document.getElementById('<%= GridDetails.ClientID %>');

            var total = 0;
            var _TxtENTERQTY = document.getElementById('<%= TxtOpeningStock.ClientID %>');
            var _TxtNETQTY = document.getElementById('<%= TXTNetOpeningStock.ClientID %>');
            var _TxtFlag = document.getElementById('<%= TXTUPDATEVALUE.ClientID %>');



            if (_TxtENTERQTY.value == "" || isNaN(_TxtENTERQTY.value)) {
                _TxtENTERQTY.value = 0;
            }

            if (_TxtNETQTY.value == "" || isNaN(_TxtNETQTY.value)) {
                _TxtNETQTY.value = 0;
            }

            var FSUBTOTAL = 0;
            var FAMOUNT;
            if (parseFloat(_TxtFlag.value) == 0) {
                for (var i = 1; i < _GridDetails.rows.length - 1; i++) {
                    FAMOUNT = (_GridDetails.rows[i].cells[6].children[0]);
                    if (FAMOUNT.value == "" || isNaN(FAMOUNT.value)) {
                        FAMOUNT.value = 0;
                    }
                    FSUBTOTAL = parseFloat(FAMOUNT.value) + parseFloat(FSUBTOTAL);
                }
            }
            else {
                for (var i = 1; i < _GridDetails.rows.length; i++) {
                    FAMOUNT = (_GridDetails.rows[i].cells[6].children[0]);
                    if (FAMOUNT.value == "" || isNaN(FAMOUNT.value)) {
                        FAMOUNT.value = 0;
                    }
                    FSUBTOTAL = parseFloat(FAMOUNT.value) + parseFloat(FSUBTOTAL);
                }
            }
            FSUBTOTAL = parseFloat(_TxtENTERQTY.value) + parseFloat(FSUBTOTAL);
            if (parseFloat(FSUBTOTAL) > parseFloat(_TxtNETQTY.value)) {
                var MSGX = "You Entered Total Opening Stock is " + parseFloat(_TxtNETQTY.value) + '\n' + "And In Fragmented its Exceed By " + (parseFloat(FSUBTOTAL) - parseFloat(_TxtNETQTY.value));
                alert(MSGX);
                 _TxtENTERQTY.value = 0;
                 return false;
                _TxtENTERQTY.focus();
            }

        }

        function CheckUnitDuplicate() {

            var D1 = document.getElementById('<%= DDLMAINUNIT.ClientID  %>');
            var D1Value = D1.options[D1.selectedIndex].value;
            var D2 = document.getElementById('<%= DDLSUBUNIT.ClientID  %>');
            var D2Value = D2.options[D2.selectedIndex].value;
            var T1 = document.getElementById('<%= TXTSUBUNITQTY.ClientID  %>');
            if (D1Value == D2Value) {
                if (T1.value > 1) {
                    T1.value = "1.00";
                }
            }
        }

        function CheckAllValidations() {
            var D1 = document.getElementById('<%= DDLMAINUNIT.ClientID  %>');
            var D1Value = D1.options[D1.selectedIndex].value;
            var D2 = document.getElementById('<%= DDLSUBUNIT.ClientID  %>');
            var D2Value = D2.options[D2.selectedIndex].value;
            var T1 = document.getElementById('<%= TXTUNITQTY.ClientID  %>');
            var T2 = document.getElementById('<%= TXTSUBUNITQTY.ClientID  %>');
            if (T1.value == "" || isNaN(T1.value)) {
                T1.value = 1;
                document.getElementById('<%= hiddenbox1.ClientID%>').value = "0";
                alert("T1 Please Enter Quantity..!");
                T1.focus();
                return false;
            }
            if (T2.value == "" || isNaN(T2.value)) {
                T2.value = 1;
                document.getElementById('<%= hiddenbox1.ClientID%>').value = "0";
                alert(" T2 Please Enter Quantity..!");
                T2.focus();
                return false;
            }
            if (D1Value == 0) {
                document.getElementById('<%= hiddenbox1.ClientID%>').value = "0";
                alert("Please Select Unit..!");
                D1.focus();
                return false;
            }
            if (D2Value == 0) {
                document.getElementById('<%= hiddenbox1.ClientID%>').value = "0";
                alert("Please Select Unit..!");
                D2.focus();
                return false;
            }
            else {
                document.getElementById('<%= hiddenbox1.ClientID%>').value = "1";
                return true;
            }
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
        <ProgressTemplate>            
        <div id="progressBackgroundFilter"></div>
        <div id="processMessage">   
        <center><span class="SubTitle">Loading....!!! </span></center>
        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
        </div>
        </ProgressTemplate>
        </asp:UpdateProgress>--%>
            <table>
                <tr>
                    <td>
                        Search for Category :
                    </td>
                    <td>
                        <asp:TextBox ID="TxtSearchCategory" runat="server" CssClass="search" ToolTip="Enter The Category"
                            Width="292px" AutoPostBack="True" OnTextChanged="TxtSearchCategory_TextChanged">
                        </asp:TextBox>
                        <ajax:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="TxtSearchCategory"
                            CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" ShowOnlyCurrentWordInCompletionListItem="true"
                            ServiceMethod="GetCompletionListCategory" CompletionListCssClass="AutoExtender"
                            CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                        </ajax:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Search for SubCategory :
                    </td>
                    <td>
                        <asp:TextBox ID="TxtSearchSubCategory" runat="server" CssClass="search" ToolTip="Enter The SubCategory"
                            Width="292px" AutoPostBack="True" OnTextChanged="TxtSearchSubCategory_TextChanged">
                        </asp:TextBox>
                        <ajax:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="TxtSearchSubCategory"
                            CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" ShowOnlyCurrentWordInCompletionListItem="true"
                            ServiceMethod="GetCompletionListSubCategory" CompletionListCssClass="AutoExtender"
                            CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                        </ajax:AutoCompleteExtender>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Search for Item :
                    </td>
                    <td>
                        <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Item"
                            Width="292px" AutoPostBack="True" OnTextChanged="TxtSearch_TextChanged">
                        </asp:TextBox>
                        <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtSearch" MinimumPrefixLength="1"
                            CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" ShowOnlyCurrentWordInCompletionListItem="true"
                            ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                        </ajax:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="IDPRINT" runat="server">
                            <ContentTemplate>
                                <asp:ImageButton ID="ImgBtnPrint" runat="server" Width="25px" Height="25px" ImageUrl="~/Images/Icon/printers_faxes.png"
                                    TabIndex="29" OnClientClick="aspnetForm.target ='_blank';" ToolTip="Print Item List"
                                    OnClick="ImgBtnPrint_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="ImgBtnPrintItem" runat="server" Width="25px" Height="25px" ImageUrl="~/Images/Icon/print.png"
                                    TabIndex="29" OnClientClick="aspnetForm.target ='_blank';" ToolTip="Print Item List With Unit Conversion"
                                    OnClick="ImgBtnPrintItem_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="ImgBtnPrintSubCategory" runat="server" Width="25px" Height="25px"
                                    ImageUrl="~/Images/Icon/printers_faxes.png" TabIndex="29" CssClass="Display_None"
                                    OnClientClick="aspnetForm.target ='_blank';" ToolTip="Print Item List as Per Sub-Category"
                                    OnClick="ImgBtnPrintSubCategory_Click" />


                                <asp:Label ID="lblcounttext" Text="Total No. of Items:- " runat="server"></asp:Label>
                                   <asp:Label ID="lblcount" Text="0" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="ImgBtnPrint" />
                                <asp:PostBackTrigger ControlID="ImgBtnPrintItem" />
                                <asp:PostBackTrigger ControlID="ImgBtnPrintSubCategory" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="Server">
    Item Master
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnPopHide" runat="server" Style="display: none;" />
            <asp:Panel ID="pnlInfo" runat="server" CssClass="ModelPopUpPanelBackGroundSmall"
                Style="display: none;">
                <table width="100%" class="PopUpHeader">
                    <tr style="background-color: Navy; text-align: center">
                        <td>
                            &nbsp;<asp:Label ID="popUpTitle" Text="Revo MMS - Item Master Entry" runat="server"
                                ForeColor="white" Font-Bold="true" Font-Size="12px"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%" style="margin: 5px 0 5px 0;">
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblPopUpYesNoMessage" runat="server" Font-Size="12px" Text="The Site & Supplier Already Exists In Below List Do You Want To Replace IT ? "></asp:Label>
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
            <%--pop up for change constant button--%>
            <table width="100%">
                <tr>
                    <td>
                        <fieldset id="fieldset1" width="100%" runat="server" class="FieldSet">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <table width="100%" cellspacing="6">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Label">
                                                Code :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtItemCode" runat="server" CssClass="TextBox" MaxLength="50" Width="140px"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td class="Label">
                                                Barcode :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtMfgBarcode" runat="server" CssClass="TextBox" MaxLength="50"
                                                    Width="140px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Label">
                                                Name :
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="TxtItemName" runat="server" CssClass="TextBox" MaxLength="500" Width="486px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="Req1" runat="server" ControlToValidate="TxtItemName"
                                                    Display="None" ErrorMessage="Item Name is Required" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" TargetControlID="Req1"
                                                    WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                </ajax:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Label">
                                                Category :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="ComboBox" AutoPostBack="true"
                                                    Width="142px" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="Req_Name" runat="server" ControlToValidate="ddlCategory"
                                                    Display="None" ErrorMessage="Please Select Category" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                    TargetControlID="Req_Name" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                </ajax:ValidatorCalloutExtender>
                                            </td>
                                            <td class="Label">
                                                Sub Category :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="ComboBox" Width="142px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Label">
                                                Delivery Period :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtDelivryPeriod" runat="server" CssClass="TextBox" MaxLength="50"
                                                    Width="140px"></asp:TextBox>
                                                (In Days)
                                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TxtDelivryPeriod"
                                                    FilterType="Custom,Numbers">
                                                </ajax:FilteredTextBoxExtender>
                                            </td>
                                            <td class="Label">
                                                GST(%) :
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList runat="server" ID="DDLTAXTEMPLATE" CssClass="ComboBox" Width="110px"
                                                            AutoPostBack="true" OnSelectedIndexChanged="DDLTAXTEMPLATE_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="TxtTaxPer" runat="server" CssClass="Display_None" MaxLength="50" Width="40px"
                                                            Enabled="false"></asp:TextBox>
                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TxtTaxPer"
                                                            FilterType="Custom,Numbers" ValidChars=".">
                                                        </ajax:FilteredTextBoxExtender>
                                                        <asp:DropDownList runat="server" ID="ddlGSTPer" CssClass="ComboBox" Width="110px"
                                                            AutoPostBack="true" >
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Label">
                                                Min Stock Level :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtMinStockLevel" runat="server" CssClass="TextBox" MaxLength="50"
                                                    Width="140px"></asp:TextBox>
                                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TxtMinStockLevel"
                                                    FilterType="Custom,Numbers" ValidChars=".">
                                                </ajax:FilteredTextBoxExtender>
                                            </td>
                                            <td class="Label">
                                                Re Order Level :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtReOrdLevel" runat="server" CssClass="TextBox" MaxLength="50"
                                                    Width="140px"></asp:TextBox>
                                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="TxtReOrdLevel"
                                                    FilterType="Custom,Numbers" ValidChars=".">
                                                </ajax:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Label">
                                                Max Stock Level :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtMaxStockLevel" runat="server" CssClass="TextBox" MaxLength="50"
                                                    Width="140px"></asp:TextBox>
                                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TxtMaxStockLevel"
                                                    FilterType="Custom,Numbers" ValidChars=".">
                                                </ajax:FilteredTextBoxExtender>
                                            </td>
                                            <td class="Label">
                                                As On Date :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtAsOnDate" runat="server" CssClass="TextBox" MaxLength="50" Width="110px"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="Img_AsOnDate" TargetControlID="TxtAsOnDate" />
                                                <asp:ImageButton ID="Img_AsOnDate" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                                    ImageUrl="~/Images/Icon/DateSelector.png" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Label">
                                                Total Opening Stock :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TXTNetOpeningStock" runat="server" CssClass="TextBox" MaxLength="10"
                                                    Width="60px"></asp:TextBox>
                                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="TXTNetOpeningStock"
                                                    FilterType="Custom,Numbers" ValidChars=".">
                                                </ajax:FilteredTextBoxExtender>
                                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="ComboBox" Width="80px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFV5" runat="server" ControlToValidate="ddlUnit"
                                                    Display="None" ErrorMessage="Unit is Required For Opening Stock" SetFocusOnError="True"
                                                    ValidationGroup="AddGrid" InitialValue="0"></asp:RequiredFieldValidator>
                                                <ajax:ValidatorCalloutExtender ID="VCE6" runat="server" Enabled="True" TargetControlID="RFV5"
                                                    WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                </ajax:ValidatorCalloutExtender>
                                                <asp:CheckBox ID="ChkKitchenAssign" runat="server" CssClass="Display_None" />
                                            </td>
                                            <td colspan="2" align="center">
                                                <asp:CheckBox ID="chkClub" runat="server" Text="&nbsp;&nbsp;CLUB ALL DESCRIPTION"
                                                    CssClass="CheckBox" ForeColor="Black" ToolTip="CHECK IF YOU WANT TO VIEW STOCK CLUBBING ALL DESCRIPTION INTO ONE.." />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Label">
                                                HSN Code :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtHSNCode" runat="server" CssClass="TextBox" MaxLength="50" Width="140px"></asp:TextBox>
                                            </td>
                                            <td class="Label">
                                                Remark :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="3" CssClass="TextBox"
                                                    MaxLength="500" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="Display_None">
                                            <td colspan="4">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <fieldset id="fieldset4" runat="server" class="FieldSet" width="100%">
                                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                    <ContentTemplate>
                                                                        <table width="100%" cellspacing="6">
                                                                            <tr>
                                                                                <td class="Label">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td class="Label">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td class="Label">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="Label">
                                                                                    Size :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlsize" runat="server" CssClass="ComboBox" ValidationGroup="AddGrid1"
                                                                                        Width="142px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsize"
                                                                                        Display="None" ErrorMessage="Size Is Required" SetFocusOnError="True" ValidationGroup="AddGrid1"
                                                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                                                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                                                                        TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                                                    </ajax:ValidatorCalloutExtender>
                                                                                </td>
                                                                                <td colspan="3" align="left">
                                                                                    <asp:ImageButton ID="ImgAddGridSize" runat="server" CssClass="Imagebutton" Height="16px"
                                                                                        ImageUrl="~/Images/Icon/Gridadd.png" OnClick="ImgAddGridSize_Click" ToolTip="Add Grid"
                                                                                        ValidationGroup="AddGrid1" Width="16px" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5">
                                                                                    <div class="scrollableDiv">
                                                                                        <asp:GridView ID="Grd_Size" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                                            BorderStyle="None" BorderWidth="1px" CssClass="mGrid" Font-Bold="False" ForeColor="Black"
                                                                                            GridLines="Horizontal" OnRowDeleting="Grd_Size_RowDeleting">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="#" Visible="False">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="ImageBtnDelete" runat="server" CommandArgument='<%#Eval("#") %>'
                                                                                                            CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                                                    </ItemTemplate>
                                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                                                                    <HeaderStyle Wrap="False" Width="20px" />
                                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="20px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField HeaderText="SizeId" DataField="SizeId"></asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Size" DataField="SizeName"></asp:BoundField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="TR_UnitConversion" runat="server">
                                            <td colspan="4">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <div class="ScrollableDiv_FixHeightWidth_N">
                                                                <asp:GridView ID="GrdUnitConversion" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                    BorderStyle="None" BorderWidth="1px" CssClass="mGrid" Font-Bold="False" ForeColor="Black"
                                                                    GridLines="Horizontal">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="#" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="All">
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="GrdSelectAllHeader" runat="server" AutoPostBack="true" OnCheckedChanged="GrdSelectAllHeader_CheckedChanged" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ChkSelect" runat="server" CssClass="CheckBox" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="UnitFactor" DataField="UnitFactor">
                                                                            <HeaderStyle Wrap="false" />
                                                                            <ItemStyle Wrap="false" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Qty" DataField="Qty">
                                                                            <HeaderStyle Wrap="false" />
                                                                            <ItemStyle Wrap="false" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="UnitConvDtlsId" DataField="UnitConvDtlsId">
                                                                            <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                            <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="Tr_hyl_Hide" runat="server">
                                            <td align="left" colspan="4">
                                                <asp:LinkButton ID="hyl_Hide" runat="server" CssClass="linkButton" OnClick="hyl_Hide_Click">Hide</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <fieldset id="fieldset3" runat="server" class="FieldSet" width="100%">
                                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                    <ContentTemplate>
                                                                        <table width="100%" cellspacing="6">
                                                                            <tr>
                                                                                <td class="Label2">
                                                                                    Stock Location :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlStockLocation" runat="server" CssClass="ComboBox" Width="142px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RFV3" runat="server" ControlToValidate="ddlStockLocation"
                                                                                        Display="None" ErrorMessage="Stock Location Required" SetFocusOnError="True"
                                                                                        ValidationGroup="AddGrid" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                                                                        TargetControlID="RFV3" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                                                    </ajax:ValidatorCalloutExtender>
                                                                                </td>
                                                                                <td class="Label">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td class="Label2">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Supplier :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="ComboBox" Width="142px"
                                                                                        ValidationGroup="AddGrid">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RFV2" runat="server" ControlToValidate="ddlSupplier"
                                                                                        Display="None" ErrorMessage="Supplier Name is Required" SetFocusOnError="True"
                                                                                        ValidationGroup="AddGrid" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                    <ajax:ValidatorCalloutExtender ID="VCE1" runat="server" Enabled="True" TargetControlID="RFV2"
                                                                                        WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                                                    </ajax:ValidatorCalloutExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="Label2">
                                                                                    Opening Stock :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TxtOpeningStock" runat="server" CssClass="TextBox" MaxLength="10"
                                                                                        Width="140px"></asp:TextBox>
                                                                                    <%--  onblur="CalculateNetAmtForGrid()"--%>
                                                                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="TxtOpeningStock"
                                                                                        FilterType="Custom,Numbers" ValidChars=".">
                                                                                    </ajax:FilteredTextBoxExtender>
                                                                                </td>
                                                                                <td class="Label">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td class="Label2" align="right">
                                                                                    Purchase Rate :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TxtPurchaseRate" runat="server" CssClass="TextBox" MaxLength="50"
                                                                                        Width="140px"></asp:TextBox>
                                                                                    <ajax:FilteredTextBoxExtender ID="F1ilteredTextBoxExtender10" runat="server" TargetControlID="TxtPurchaseRate"
                                                                                        FilterType="Custom,Numbers" ValidChars=".">
                                                                                    </ajax:FilteredTextBoxExtender>
                                                                                    <asp:TextBox ID="TXTUPDATEVALUE" runat="server" CssClass="Display_None" MaxLength="50"
                                                                                        Width="10px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="Label2">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Min Stock :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtMinstockSupp" runat="server" CssClass="TextBox" MaxLength="50"
                                                                                        Width="140px"></asp:TextBox>
                                                                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtMinstockSupp"
                                                                                        FilterType="Custom,Numbers" ValidChars=".">
                                                                                    </ajax:FilteredTextBoxExtender>
                                                                                </td>
                                                                                <td class="Label">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td class="Label2">
                                                                                    Re Order Level :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtReorderSupp" runat="server" CssClass="TextBox" MaxLength="50"
                                                                                        Width="140px"></asp:TextBox>
                                                                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtReorderSupp"
                                                                                        FilterType="Custom,Numbers" ValidChars=".">
                                                                                    </ajax:FilteredTextBoxExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="Label2">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Max Stock :
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TxtMaxStockSupp" runat="server" CssClass="TextBox" MaxLength="50"
                                                                                        Width="140px"></asp:TextBox>
                                                                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="TxtMaxStockSupp"
                                                                                        FilterType="Custom,Numbers" ValidChars=".">
                                                                                    </ajax:FilteredTextBoxExtender>
                                                                                </td>
                                                                                <td class="Label">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td class="Label">
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="Label2">
                                                                                    &nbsp;&nbsp;&nbsp; &nbsp; Description :
                                                                                </td>
                                                                                <td colspan="5">
                                                                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="TextBox" 
                                                                                        MaxLength="500" Width="500px"></asp:TextBox>
                                                                                        <%--onblur="CalculateNetAmtForGrid()"--%>
                                                                                </td>
                                                                                <td class="Label">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td class="Label">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="Label2">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Drawing No:
                                                                                </td>
                                                                                <td colspan="5">
                                                                                    <asp:TextBox ID="txtDrawingNo" runat="server" CssClass="TextBox" MaxLength="50" Width="150px"></asp:TextBox>
                                                                                    Drawing :
                                                                                    <asp:FileUpload ID="fUpload" runat="server" />
                                                                                    <asp:Button ID="btnUpload" runat="server" CssClass="button" Text="Upload" OnClick="btnUpload_Click" />
                                                                                    <asp:Label ID="lblFileup" runat="server" ForeColor="Red"></asp:Label>
                                                                                </td>
                                                                                </td>
                                                                                <td class="Label">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td class="Label">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="10">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td class="Label">
                                                                                                <asp:TextBox runat="server" ID="TXTUNITQTY" Width="80px" CssClass="TextBoxNumeric"
                                                                                                    onkeyup="ChangeFunc();"></asp:TextBox>
                                                                                            </td>
                                                                                            <ajax:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="TXTUNITQTY"
                                                                                                WatermarkText="Qty" WatermarkCssClass="water" />
                                                                                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="TXTUNITQTY"
                                                                                                FilterType="Custom,Numbers" ValidChars=".">
                                                                                            </ajax:FilteredTextBoxExtender>
                                                                                            <td class="Label">
                                                                                                <asp:DropDownList runat="server" ID="DDLMAINUNIT" Width="180px" CssClass="ComboBox"
                                                                                                    onchange="Javascript:CheckUnitDuplicate();" Enabled="true">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td class="Label">
                                                                                                <asp:Label runat="server" ID="LABELEQUAL" Text="&nbsp;&nbsp;=&nbsp;&nbsp;" CssClass="Label_Dynamic"></asp:Label>
                                                                                            </td>
                                                                                            <td class="Label">
                                                                                                <asp:TextBox runat="server" ID="TXTSUBUNITQTY" Width="80px" CssClass="TextBoxNumeric"
                                                                                                    onkeyup="ChangeFunc1();"></asp:TextBox>
                                                                                            </td>
                                                                                            <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TXTSUBUNITQTY"
                                                                                                WatermarkText="Qty" WatermarkCssClass="water" />
                                                                                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="TXTSUBUNITQTY"
                                                                                                FilterType="Custom,Numbers" ValidChars=".">
                                                                                            </ajax:FilteredTextBoxExtender>
                                                                                            <td class="Label">
                                                                                                <asp:DropDownList runat="server" ID="DDLSUBUNIT" Width="180px" CssClass="ComboBox"
                                                                                                    onchange="Javascript:CheckUnitDuplicate();">
                                                                                                </asp:DropDownList>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DDLSUBUNIT"
                                                                                                    Display="None" ErrorMessage="To Unit Required" SetFocusOnError="True" ValidationGroup="AddGrid"
                                                                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                                                                                    TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                                                                                </ajax:ValidatorCalloutExtender>
                                                                                            </td>
                                                                                            <td class="Label">
                                                                                                <asp:TextBox runat="server" ID="TextBox1" Width="100px" CssClass="Display_None"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:ImageButton ID="IMGBTNADDUNIT" runat="server" CssClass="Imagebutton" Height="19px"
                                                                                                    ImageUrl="~/Images/Icon/Gridadd.png" ToolTip="Add To Unit Conversion Grid" ValidationGroup="AddUnitGrid"
                                                                                                    Width="16px" OnClick="IMGBTNADDUNIT_Click" OnClientClick="javascript:CheckAllValidations();"
                                                                                                    Visible="False" />
                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                <asp:ImageButton ID="ImgAddGrid" runat="server" CssClass="Imagebutton" Height="16px"
                                                                                                    ImageUrl="~/Images/Icon/Gridadd.png" OnClick="ImgAddGrid_Click" ToolTip="Add Grid"
                                                                                                    ValidationGroup="AddGrid" Width="16px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <div class="ScrollableDiv_FixHeightWidth_N">
                                                                        <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            BorderStyle="None" BorderWidth="1px" CssClass="mGrid" Font-Bold="False" ForeColor="Black"
                                                                            GridLines="Horizontal" OnRowCommand="GridDetails_RowCommand" OnRowDeleting="GridDetails_RowDeleting"
                                                                            OnRowDataBound="GridDetails_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="#" Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ImageGridEdit" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                                                            CommandName="SelectGrid" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                                                                        <asp:ImageButton ID="ImageBtnDelete" runat="server" CommandArgument='<%#Eval("#") %>'
                                                                                            CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                                        <ajax:ConfirmButtonExtender ID="ConfirmButton" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                                                            TargetControlID="ImageBtnDelete">
                                                                                        </ajax:ConfirmButtonExtender>
                                                                                    </ItemTemplate>
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <HeaderStyle Wrap="False" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="LocationId" DataField="LocationId">
                                                                                    <HeaderStyle CssClass="Display_None" />
                                                                                    <ItemStyle CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Site">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="GrdtxtLocation" runat="server" CssClass="TextBoxGrid" MaxLength="10"
                                                                                            Text='<%# Bind("Location") %>' TextMode="SingleLine" Width="120px" Enabled="false"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="SuplierName" DataField="SuplierName">
                                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderText="SupplierId" DataField="SupplierId">
                                                                                    <HeaderStyle CssClass="Display_None" />
                                                                                    <ItemStyle CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderText="Last Purchase Rate" DataField="PurchaseRate">
                                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Opening Stock">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="GrdtxtOpeningStock" runat="server" CssClass="TextBoxGrid" MaxLength="10"
                                                                                            Text='<%# Bind("OpeningStock") %>' TextMode="SingleLine" Width="50px" Enabled="false"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Purchase Rate">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="GrdtxtPurchaseRate" runat="server" CssClass="TextBoxGrid" MaxLength="10"
                                                                                            Text='<%# Bind("PurchaseRate") %>' TextMode="SingleLine" Width="50px" Enabled="false"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="Item Description" DataField="ItemDesc">
                                                                                    <HeaderStyle Wrap="false" />
                                                                                    <ItemStyle Wrap="false" />
                                                                                </asp:BoundField>
                                                                                <%--<10>--%>
                                                                                <asp:BoundField DataField="MainUnitFactor" HeaderText="Qty">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <%--<11>--%>
                                                                                <asp:BoundField DataField="UnitID" HeaderText="UnitID">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <%--<12>--%><asp:BoundField DataField="MainUnit" HeaderText="From Unit">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <%--<13>--%>
                                                                                <asp:BoundField DataField="SubUnitFactor" HeaderText="Qty">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <%--<14>--%>
                                                                                <asp:BoundField DataField="SubUnitID" HeaderText="SubUnitID">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <%--<15>--%>
                                                                                <asp:BoundField DataField="SubUnit" HeaderText="To Unit">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="DrawingNo" HeaderText="DrawingNo">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="DrawingPath" HeaderText="DrawingPath">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="View Drawing">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton runat="server" ID="lnkView" CommandName="VIEW" PostBackUrl='<%# "Drawing.aspx?RowIndex=" +
                            Container.DataItemIndex %>' target="_blank" CssClass="Display_None">View Deal</asp:LinkButton>
                                                                                        <a href='<%# "../Reports/Drawing.aspx?RowIndex=" +
                            Container.DataItemIndex + "&FormName=ItemMaster" %>' target="_blank">View Drawing PDF</a>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                
                                                                                 <asp:TemplateField HeaderText="Min Stock">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="GrdtxtMinStockSupp" runat="server" CssClass="TextBoxGrid" MaxLength="10"
                                                                                            Text='<%# Bind("MinStockSupp") %>' TextMode="SingleLine" Width="50px" Enabled="false"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Max Stock">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="GrdtxtMaxStockSupp" runat="server" CssClass="TextBoxGrid" MaxLength="10"
                                                                                            Text='<%# Bind("MaxStockSupp") %>' TextMode="SingleLine" Width="50px" Enabled="false"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Re order">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="GrdtxtReorderSupp" runat="server" CssClass="TextBoxGrid" MaxLength="10"
                                                                                            Text='<%# Bind("ReorderSupp") %>' TextMode="SingleLine" Width="50px" Enabled="false"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr class="Display_None">
                                                                <td>
                                                                    <div class="ScrollableDiv_FixHeightWidth_N">
                                                                        <asp:GridView ID="GrdUnitCal" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            BorderStyle="None" BorderWidth="1px" CssClass="mGrid" Font-Bold="False" ForeColor="Black"
                                                                            GridLines="Horizontal">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="#" Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ImageGridEdit" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                                                            CommandName="SelectGrid" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                                                                        <asp:ImageButton ID="ImageBtnDelete" runat="server" CommandArgument='<%#Eval("#") %>'
                                                                                            CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                                        <ajax:ConfirmButtonExtender ID="ConfirmButton" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                                                            TargetControlID="ImageBtnDelete">
                                                                                        </ajax:ConfirmButtonExtender>
                                                                                    </ItemTemplate>
                                                                                    <ControlStyle CssClass="Display_None" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                                    <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="ItemID" DataField="ItemID">
                                                                                    <ControlStyle CssClass="Display_None" />
                                                                                    <FooterStyle CssClass="Display_None" />
                                                                                    <HeaderStyle CssClass="Display_None" />
                                                                                    <ItemStyle CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderText="From_Factor" DataField="From_Factor"></asp:BoundField>
                                                                                <asp:BoundField HeaderText="From_UnitID" DataField="From_UnitID">
                                                                                    <%-- <ControlStyle CssClass="Display_None" />
                                                                                    <FooterStyle CssClass="Display_None"/>
                                                                                    <HeaderStyle CssClass="Display_None"/>
                                                                                    <ItemStyle CssClass="Display_None"/>       --%>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderText="From_UnitName" DataField="From_UnitName"></asp:BoundField>
                                                                                <asp:BoundField HeaderText="To_Factor" DataField="To_Factor"></asp:BoundField>
                                                                                <asp:BoundField HeaderText="To_UnitID" DataField="To_UnitID">
                                                                                    <%-- <ControlStyle CssClass="Display_None" />
                                                                                    <FooterStyle CssClass="Display_None"/>
                                                                                    <HeaderStyle CssClass="Display_None"/>
                                                                                    <ItemStyle CssClass="Display_None"/>--%>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderText="To_UnitName" DataField="To_UnitName"></asp:BoundField>
                                                                                <asp:BoundField HeaderText="Factor_Desc" DataField="Factor_Desc"></asp:BoundField>
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
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset id="fieldset2" runat="server" class="FieldSet" width="100%">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                <table align="center" width="25%">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="BtnUpdate" runat="server" CssClass="button" Text="Update" ValidationGroup="Add"
                                                                OnClick="BtnUpdate_Click" />
                                                            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
                                                                TargetControlID="BtnUpdate">
                                                            </ajax:ConfirmButtonExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="BtnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="Add"
                                                                OnClick="BtnSave_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="BtnDelete" runat="server" CssClass="button" Text="Delete" ValidationGroup="Add"
                                                                OnClick="BtnDelete_Click" />
                                                            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" ConfirmText="Would You Like To Delete The Record ?"
                                                                TargetControlID="BtnDelete">
                                                            </ajax:ConfirmButtonExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="BtnCancel" runat="server" CausesValidation="false" CssClass="button"
                                                                OnClick="BtnCancel_Click" Text="Cancel" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ReportTitle" runat="Server">
    Item List
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Report" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="ScrollableDiv_FixHeightWidthForRepeater">
                <ul id="subnav">
                    <%--Ul Li Problem Solved repeater--%>
                    <asp:Repeater ID="GrdReport" runat="server" OnItemCommand="GrdReport_ItemCommand">
                        <ItemTemplate>
                            <li id="Menuitem" runat="server">
                                <asp:LinkButton ID="lbtn_List" CssClass="linkButton" CausesValidation="false" CommandName="Select"
                                    CommandArgument='<%# Eval("#") %>' runat="server" Text='<%# Eval("Name") %>'>
                                </asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                   <%-- <asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand">
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
                    </asp:Repeater>--%>
                </ul>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
