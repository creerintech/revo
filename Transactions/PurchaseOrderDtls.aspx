<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="PurchaseOrderDtls.aspx.cs" Inherits="Transactions_PurchaseOrderDtls"
    Title="Purchase Order" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" runat="Server">
    <asp:HiddenField ID="SetFlagHidden" runat="server" Value="0" />

    <script language="javascript" type="text/javascript">

        function RadioCheck(rb) {
            var gv = document.getElementById("<%=GridLINKSUPPLIERRATE.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            var row = rb.parentNode.parentNode;
            var getrow = 0;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked == true) {
                        getrow = i;
                    }
                }
            }

            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {

                    rbs[i].checked = false;

                }
            }
            rbs[getrow].checked = true;

        }


        function ShowPOP() {
            document.getElementById('<%= dialog.ClientID %>').style.display = "block";
        }

        function SetFlag() {
            document.getElementById('<%= SetFlagHidden.ClientID %>').value = 1;
        }

        function HidePOP() {
            document.getElementById('<%= dialog.ClientID %>').style.display = "none";
        }


        function ShowPOPTermsPOSupplier() {

            document.getElementById('<%= DIVTERMSSUPPLIER.ClientID %>').style.display = "block";

        }

        function HidePOPTermsPOSupplier() {

            document.getElementById('<%= DIVTERMSSUPPLIER.ClientID %>').style.display = "none";

        }


        function HidePOPForceClose(s) {

            document.getElementById('<%= dialog.ClientID %>').style.display = "none";
        }

        function HidePOPForceCloseCode() {

            document.getElementById('<%= dialog.ClientID %>').style.display = "none";
        }


        function HideALLPOPForceClose(s) {

            document.getElementById('<%= dialog.ClientID %>').style.display = "none";
            document.getElementById('<%= DIVTERMSSUPPLIER.ClientID %>').style.display = "none";

        }

        function SETVALUE(objGrid) {
            var _GridDetails = document.getElementById('<%= GrdReqPO.ClientID %>');
            var rowIndex = objGrid.offsetParent.parentNode.rowIndex;
            var ddlsupp = (_GridDetails.rows[rowIndex].cells[18].children[0]);
            
            console.log("ddlsupp",ddlsupp.value);
            var suppId = (_GridDetails.rows[rowIndex].cells[30].children[0]);
             console.log("suppId",suppId.value);
            if (ddlsupp.value == 0) {
                suppId.value = 0;
                alert("Please select Supplier");
            }
            if (ddlsupp.value > 0) {
                suppId.value = ddlsupp.value;
            }
        }

        function CalculateGrid(objGrid) {
            var _GridDetails = document.getElementById('<%= GrdReqPO.ClientID %>');
            var rowIndex = objGrid.offsetParent.parentNode.rowIndex;

            var Rate = (_GridDetails.rows[rowIndex].cells[20].children[0]);
            var OrdQty = (_GridDetails.rows[rowIndex].cells[19].children[0]);
              console.log("Rate", Rate.value);
              
              console.log("OrdQty", OrdQty.value);
              
//            var pervat = (_GridDetails.rows[rowIndex].cells[21].children[0]);
//            var vat = (_GridDetails.rows[rowIndex].cells[22].children[0]);
            var perdisc = (_GridDetails.rows[rowIndex].cells[28].children[0]);
            var disc = (_GridDetails.rows[rowIndex].cells[29].children[0]);
            
            var CGSTPer =  (_GridDetails.rows[rowIndex].cells[22].children[0]);
            var SGSTPer = (_GridDetails.rows[rowIndex].cells[24].children[0]);
            var IGSTPer =  (_GridDetails.rows[rowIndex].cells[26].children[0]);
            
             console.log("perdisc", perdisc.value);
             console.log("disc", disc.value);
             
             
             console.log("IGSTPer", IGSTPer.value);
           
              var CGSTGrdAmt =  (_GridDetails.rows[rowIndex].cells[23].children[0]);
            var SGSTGrdAmt = (_GridDetails.rows[rowIndex].cells[25].children[0]);
            var IGSTGrdAmt=  (_GridDetails.rows[rowIndex].cells[27].children[0]);

            if (Rate.value == "" || isNaN(Rate.value)) 
            {
                //alert("Please Enter Rate..!");
                Rate.value = 0;
            }

            if (OrdQty.value == "" || isNaN(OrdQty.value)) {
                OrdQty.value = 0;
            }

            if (CGSTPer.value == "" || isNaN(CGSTPer.value)) {
                CGSTPer.value = 0;
            }

            if (SGSTPer.value == "" || isNaN(SGSTPer.value)) {
                SGSTPer.value = 0;
            }
            
            if (IGSTPer.value == "" || isNaN(IGSTPer.value)) {
                IGSTPer.value = 0;
            }

            if (perdisc.value == "" || isNaN(perdisc.value)) {
                perdisc.value = 0;
            }

            if (disc.value == "" || isNaN(disc.value)) {
                disc.value = 0;
            }
            
            disc.value = parseFloat((perdisc.value / 100) * ((Rate.value * OrdQty.value))).toFixed(2);
             console.log("disc", disc.value);
            
            
             if (CGSTPer != "" && SGSTPer != "")
              {
               CGSTGrdAmt.value = parseFloat((CGSTPer.value / 100) * ((Rate.value * OrdQty.value))).toFixed(2);
            
                SGSTGrdAmt.value = parseFloat((SGSTPer.value / 100) * ((Rate.value * OrdQty.value))).toFixed(2);
              
              console.log("CGSTGrdAmt", CGSTGrdAmt.value);
               console.log("SGSTGrdAmt", SGSTGrdAmt.value);
          
            }
            if (IGSTPer != "" && IGSTPer != 0)
             {
              IGSTGrdAmt.value = parseFloat((IGSTPer.value / 100) * ((Rate.value * OrdQty.value))).toFixed(2);
                 console.log("IGSTGrdAmt", IGSTGrdAmt.value);
            }

        }


        function CalculateGrid1() {
            var _GridDetails = document.getElementById('<%= GrdReqPO.ClientID %>');

            for (var rowIndex = 1; rowIndex < _GridDetails.rows.length; rowIndex++) {

                var Rate = (_GridDetails.rows[rowIndex].cells[20].children[0]);
                var OrdQty = (_GridDetails.rows[rowIndex].cells[19].children[0]);
                var pervat = (_GridDetails.rows[rowIndex].cells[21].children[0]);
                var vat = (_GridDetails.rows[rowIndex].cells[22].children[0]);
                var perdisc = (_GridDetails.rows[rowIndex].cells[23].children[0]);
                var disc = (_GridDetails.rows[rowIndex].cells[24].children[0]);
                if (Rate.value == "" || isNaN(Rate.value)) {
                    Rate.value = 0;
                }

                if (OrdQty.value == "" || isNaN(OrdQty.value)) {
                    OrdQty.value = 0;
                }

                if (pervat.value == "" || isNaN(pervat.value)) {
                    pervat.value = 0;
                }

                if (vat.value == "" || isNaN(vat.value)) {
                    vat.value = 0;
                }

                if (perdisc.value == "" || isNaN(perdisc.value)) {
                    perdisc.value = 0;
                }

                if (disc.value == "" || isNaN(disc.value)) {
                    disc.value = 0;
                }
                disc.value = parseFloat((perdisc.value / 100) * ((Rate.value * OrdQty.value))).toFixed(2);
                vat.value = parseFloat((pervat.value / 100) * ((Rate.value * OrdQty.value) - disc.value)).toFixed(2);
            }
        }

        function CalculateGridLPR(objGrid) {

            var _GridDetails = document.getElementById('<%= GrdReqPO.ClientID %>');

            var rowIndex = objGrid;

            var Rate = (_GridDetails.rows[rowIndex].cells[20].children[0]);
            var OrdQty = (_GridDetails.rows[rowIndex].cells[19].children[0]);
            var pervat = (_GridDetails.rows[rowIndex].cells[21].children[0]);
            var vat = (_GridDetails.rows[rowIndex].cells[22].children[0]);
            var perdisc = (_GridDetails.rows[rowIndex].cells[23].children[0]);
            var disc = (_GridDetails.rows[rowIndex].cells[24].children[0]);
            if (Rate.value == "" || isNaN(Rate.value)) {
                Rate.value = 0;
            }

            if (OrdQty.value == "" || isNaN(OrdQty.value)) {
                OrdQty.value = 0;
            }

            if (pervat.value == "" || isNaN(pervat.value)) {
                pervat.value = 0;
            }

            if (vat.value == "" || isNaN(vat.value)) {
                vat.value = 0;
            }

            if (perdisc.value == "" || isNaN(perdisc.value)) {
                perdisc.value = 0;
            }

            if (disc.value == "" || isNaN(disc.value)) {
                disc.value = 0;
            }
            disc.value = parseFloat((perdisc.value / 100) * ((Rate.value * OrdQty.value))).toFixed(2);
            vat.value = parseFloat((pervat.value / 100) * ((Rate.value * OrdQty.value) - disc.value)).toFixed(2);
        }

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
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="Server">
    Purchase Order
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
                                                        <td class="Label">
                                                            PO Through :
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rdoPOThrough" runat="server" RepeatDirection="Horizontal"
                                                                CssClass="RadioButton" OnSelectedIndexChanged="rdoPOThrough_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                                <asp:ListItem Selected="True" Value="0">&#160;Indent Wise&#160;&#160;&#160;&#160;</asp:ListItem>
                                                                <asp:ListItem Value="1">&#160;Item Wise</asp:ListItem>

                                                                  <asp:ListItem Value="2">&#160;Supplier Comparsion</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
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
                                                    </tr>

                                                     
                                                    <tr id="trsupcon" runat="server">
                                                         <td class="Label">
                                                          Enquiry No:
                                                        </td>
                                                        <td colspan="1">
                                                            <asp:DropDownList ID="drpenquiry" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpenquiry_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>

                                                          <td class="Label">
                                                        Select Supplier:
                                                        </td>
                                                        <td colspan="1">
                                                            <asp:DropDownList ID="drpsupplier" runat="server" Width="200px" AutoPostBack="true"  OnSelectedIndexChanged="drpsupplier_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>

                                                        
                                                   
                                                    </tr>


                                                   




                                                    <tr>



                                                        



                                                        <td class="Label">
                                                            PO Quot. No :
                                                        </td>
                                                        <td colspan="1">
                                                            <asp:TextBox ID="TXTPOQTNO" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td class="Label">
                                                            Quot. Dt :
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="TXTPOQTDATE" runat="server" CssClass="TextBox" Width="80px"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                                                ImageUrl="~/Images/Icon/DateSelector.png" meta:resourcekey="ImageFromDateResource1" />
                                                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy HH':'mm':'ss"
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
                                                            <ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                                                Width="270px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                            </ajax:ComboBox>
                                                        </td>
                                                        <td class="Label_Dynamic" align="right" rowspan="5" valign="middle">
                                                            Rate :
                                                        </td>
                                                        <td align="left" rowspan="5">
                                                            <asp:ListBox ID="lstSupplierRate" runat="server" Width="300px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="lstSupplierRate_SelectedIndexChanged"></asp:ListBox>
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
                                                            <ajax:ComboBox ID="ddlsubcategory" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                                                Width="270px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged">
                                                            </ajax:ComboBox>
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
                                                            <ajax:ComboBox ID="ddlItem" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                                                Width="270px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                                            </ajax:ComboBox>
                                                        </td>
                                                        <td align="right" class="Label" valign="top">
                                                            CGST(%) :
                                                        </td>
                                                        <td align="left" valign="middle">
                                                            <asp:TextBox ID="txtCGSTItemPer" runat="server" CssClass="TextBox" Width="60px" AutoPostBack="True"
                                                                OnTextChanged="txtdiscper_TextChanged"></asp:TextBox>
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
                                                                OnTextChanged="txtdiscper_TextChanged"></asp:TextBox>
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
                                                                OnTextChanged="txtdiscper_TextChanged"></asp:TextBox>
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
                                                            <ajax:ComboBox ID="ddlItemDesc" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" ItemInsertLocation="Append"
                                                                Width="270px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                                            </ajax:ComboBox>
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
                                                        <td>
                                                        </td>
                                                        <td valign="top">
                                                        </td>
                                                        <td align="right" class="Label" valign="top">
                                                        </td>
                                                        <td align="left" valign="middle">
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
                            <tr>
                                <td colspan="2">
                                    <fieldset id="Fieldset2" class="FieldSet" runat="server" style="width: 100%">
                                        <legend id="Legend3" class="legend" runat="server">Material Indent Details</legend>
                                        <div id="divGridDetails" class="ScrollableDiv_FixHeightWidth3">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GrdReqPO" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            CssClass="mGrid" BackColor="White" BorderColor="#0CCCCC" BorderStyle="None" BorderWidth="1px"
                                                            CellPadding="4" ForeColor="Black" AllowPaging="false" OnRowDataBound="GrdReqPO_RowDataBound"
                                                            OnRowCommand="GrdReqPO_RowCommand" DataKeyNames="#">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- 0--%>
                                                                <asp:TemplateField HeaderText="All">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="GrdSelectAllHeader" runat="server" AutoPostBack="true" OnCheckedChanged="GrdSelectAllHeader_CheckedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="GrdSelectAll" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <%-- 1--%>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageGridEdit" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                                            CommandName="Select" ImageUrl="~/Images/New Icon/ViewRecord.png" ToolTip="Item Details" />
                                                                        <asp:ImageButton ID="ImgBtnDelete" runat="server" Visible="false" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                                            CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                        <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                                            TargetControlID="ImgBtnDelete">
                                                                        </ajax:ConfirmButtonExtender>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <HeaderStyle Width="10px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" Wrap="false" />
                                                                </asp:TemplateField>
                                                                <%--2--%>
                                                                <asp:BoundField DataField="ItemCode" HeaderText="Code">
                                                                    <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="False" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%-- 3--%>
                                                                <asp:BoundField HeaderText="Particular" DataField="ItemName" ItemStyle-Width="400px">
                                                                </asp:BoundField>
                                                                <%--4--%>
                                                                <asp:BoundField DataField="ItemDetailsId" HeaderText="ItemDetailsId">
                                                                    <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="False" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%-- 5--%>
                                                                <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                                                                    <HeaderStyle Width="50px" Wrap="true" />
                                                                    <ItemStyle Width="50" Wrap="true" />
                                                                </asp:BoundField>
                                                                <%--6--%>
                                                                <asp:BoundField HeaderText="Req.Qty" DataField="ReqQty">
                                                                    <HeaderStyle Wrap="false" />
                                                                    <ItemStyle Wrap="false" />
                                                                </asp:BoundField>
                                                                <%--7--%>
                                                                <asp:BoundField HeaderText="Unit" DataField="Unit">
                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%--8--%>
                                                                <asp:BoundField HeaderText="Tot.Ord" DataField="TotOrdQty">
                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%-- 9--%>
                                                                <asp:BoundField HeaderText="ReqByCafeteria" DataField="ReqByCafeteria">
                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%-- 10--%>
                                                                <asp:BoundField HeaderText="Transit Qty" DataField="TransitQty">
                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%-- 11--%>
                                                                <asp:BoundField HeaderText="Avl.Stock" DataField="AvailableStock">
                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%-- 12--%>
                                                                <asp:BoundField HeaderText="Bal.Qty" DataField="RemQty">
                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%--13--%>
                                                                <asp:BoundField HeaderText="MinStockLevel" DataField="MinStockLevel">
                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%-- 14--%>
                                                                <asp:BoundField HeaderText="Unit" DataField="Unit">
                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%-- 15--%>
                                                                <asp:BoundField HeaderText="Del.Period" DataField="DeliveryPeriod">
                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%-- 16--%>
                                                                <asp:BoundField HeaderText="Site" DataField="StoreLocation">
                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%--17--%>
                                                                <asp:TemplateField HeaderText="Last Purchase Record">
                                                                    <ItemTemplate>
                                                                        <div style="">
                                                                            <div style="">
                                                                                <asp:LinkButton runat="server" ID="LINKLASTPURCHASE" Text="LAST 3 PURCHASE RATE&nbsp;&nbsp;"
                                                                                    OnClientClick="javascript:HideALLPOPForceClose();"></asp:LinkButton>
                                                                                <ajax:PopupControlExtender ID="popupLINKLASTPURCHASE" runat="server" OnPreRender="ONLOADALLRECORD_OnLoad"
                                                                                    PopupControlID="PnlGrid" Position="Left" CommitProperty="Value" TargetControlID="LINKLASTPURCHASE"
                                                                                    DynamicServicePath="" Enabled="True" ExtenderControlID="">
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
                                                                                                                            <asp:UpdatePanel ID="UpdatePanelLINKLASTPURCHASE" runat="server">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:RadioButtonList ID="RdoLASTPURCHASE" runat="server" CellPadding="25" RepeatDirection="Vertical"
                                                                                                                                        CssClass="RadioButton">
                                                                                                                                    </asp:RadioButtonList>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="center">
                                                                                                            <asp:CheckBox ID="CHKLINKLASTPURCHASE" runat="server" Text="Confirm" OnClientClick="javascript:HideALLPOPForceClose();"
                                                                                                                CssClass="CheckBox" AutoPostBack="True" OnCheckedChanged="CHKLINKLASTPURCHASE_CheckedChanged" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                            <div style="">
                                                                                <asp:LinkButton runat="server" ID="LINKSUPPLIERRATE" Text="SUPPLIER WISE RATE&nbsp;&nbsp;"
                                                                                    OnClientClick="javascript:HideALLPOPForceClose();"></asp:LinkButton>
                                                                                <ajax:PopupControlExtender ID="PopupLINKSUPPLIERRATE" runat="server" OnPreRender="ONLOADALLRECORD_OnLoad"
                                                                                    PopupControlID="PanelLINKSUPPLIERRATE" Position="Left" CommitProperty="Value"
                                                                                    TargetControlID="LINKSUPPLIERRATE" DynamicServicePath="" Enabled="True" ExtenderControlID="">
                                                                                </ajax:PopupControlExtender>
                                                                                <asp:Panel ID="PanelLINKSUPPLIERRATE" runat="server">
                                                                                    <div class="PopupPanel">
                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <div id="DivLINKSUPPLIERRATE" runat="server" class="scrollableDiv">
                                                                                                                <table style="width: 100%">
                                                                                                                    <tr>
                                                                                                                        <td align="left">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:GridView ID="GridLINKSUPPLIERRATE" runat="server" AutoGenerateColumns="False"
                                                                                                                                        DataKeyNames="#" CssClass="mGrid">
                                                                                                                                        <Columns>
                                                                                                                                            <asp:TemplateField HeaderText="#" Visible="False">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:TemplateField>
                                                                                                                                            <asp:BoundField DataField="SuplierID" HeaderText="SupplierID">
                                                                                                                                                <HeaderStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                                                                                                                    Wrap="False" />
                                                                                                                                                <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                                                                                                                    Wrap="False" />
                                                                                                                                            </asp:BoundField>
                                                                                                                                            <asp:TemplateField HeaderText="Select">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <asp:RadioButton runat="server" ID="RBSupplierList" GroupName="SuplierListLINKSUPPLIERRATE"
                                                                                                                                                        onchange="javascript:RadioCheck(this);" />
                                                                                                                                                </ItemTemplate>
                                                                                                                                                <HeaderStyle Width="10px" Wrap="False" />
                                                                                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                                                            </asp:TemplateField>
                                                                                                                                            <asp:BoundField DataField="SuplierName" HeaderText="Suplier">
                                                                                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                            </asp:BoundField>
                                                                                                                                            <asp:TemplateField HeaderText="Rate">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <asp:TextBox ID="GridLINKSUPPLIERRATERATE" runat="server" CssClass="TextBoxNumeric"
                                                                                                                                                        MaxLength="10" Text='<%# Bind("Rate") %>' TextMode="SingleLine" Width="60px"></asp:TextBox>
                                                                                                                                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderrate" runat="server" FilterType="Numbers, Custom"
                                                                                                                                                        ValidChars="." TargetControlID="GridLINKSUPPLIERRATERATE" />
                                                                                                                                                </ItemTemplate>
                                                                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="60px" />
                                                                                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="60px" />
                                                                                                                                            </asp:TemplateField>
                                                                                                                                        </Columns>
                                                                                                                                    </asp:GridView>
                                                                                                                                    </asp:GridView>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="center">
                                                                                                            <asp:CheckBox ID="ChkLINKSUPPLIERRATE" runat="server" Text="Confirm" OnClientClick="javascript:HideALLPOPForceClose();"
                                                                                                                CssClass="CheckBox" AutoPostBack="True" OnCheckedChanged="CHKLINKSUPPLIERRATE_CheckedChanged" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                            <div style="">
                                                                                <asp:LinkButton runat="server" ID="LINKALLRECORD" Text="VIEW ALL" OnClientClick="javascript:HideALLPOPForceClose();"></asp:LinkButton>
                                                                                <ajax:PopupControlExtender ID="PopupLINKALLRECORD" runat="server" OnPreRender="ONLOADALLRECORD_OnLoad"
                                                                                    PopupControlID="PanelLINKALLRECORD" Position="Left" CommitProperty="Value" TargetControlID="LINKALLRECORD"
                                                                                    DynamicServicePath="" Enabled="True" ExtenderControlID="">
                                                                                </ajax:PopupControlExtender>
                                                                                <asp:Panel ID="PanelLINKALLRECORD" runat="server">
                                                                                    <div class="PopupPanel">
                                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <div id="Div2" runat="server" class="scrollableDiv">
                                                                                                                <table style="width: 100%">
                                                                                                                    <tr>
                                                                                                                        <td align="left">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:GridView ID="GridLINKALLRECORD" runat="server" AutoGenerateColumns="False" DataKeyNames="#"
                                                                                                                                        CssClass="mGrid">
                                                                                                                                        <Columns>
                                                                                                                                            <asp:TemplateField HeaderText="#" Visible="False">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:TemplateField>
                                                                                                                                            <asp:BoundField DataField="SuplierID" HeaderText="SupplierID">
                                                                                                                                                <HeaderStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                                                                                                                    Wrap="False" />
                                                                                                                                                <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                                                                                                                    Wrap="False" />
                                                                                                                                            </asp:BoundField>
                                                                                                                                            <asp:TemplateField HeaderText="Select">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <asp:RadioButton runat="server" ID="RBLINKALLRECORD" GroupName="SuplierListLINKALLRECORD" />
                                                                                                                                                </ItemTemplate>
                                                                                                                                                <HeaderStyle Width="10px" Wrap="False" />
                                                                                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                                                            </asp:TemplateField>
                                                                                                                                            <asp:BoundField DataField="PODate" HeaderText="Date">
                                                                                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                            </asp:BoundField>
                                                                                                                                            <asp:BoundField DataField="ItemName" HeaderText="Item">
                                                                                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                            </asp:BoundField>
                                                                                                                                            <asp:BoundField DataField="ItemDesc" HeaderText="ItemDesc">
                                                                                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                            </asp:BoundField>
                                                                                                                                            <asp:BoundField DataField="Supplierrate" HeaderText="Supplier With Rate">
                                                                                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                                                                                                            </asp:BoundField>
                                                                                                                                        </Columns>
                                                                                                                                    </asp:GridView>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td align="center">
                                                                                                            <asp:CheckBox ID="ChkLINKALLRECORD" runat="server" Text="Confirm" OnClientClick="javascript:HideALLPOPForceClose();"
                                                                                                                CssClass="CheckBox" AutoPostBack="True" OnCheckedChanged="ChkLINKALLRECORD_CheckedChanged" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                                </asp:TemplateField>
                                                                <%-- 18--%>
                                                                <asp:TemplateField HeaderText="Vendor">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="GrdddlVendor" runat="server" Width="150px" AutoPostBack="true"
                                                                            OnSelectedIndexChanged="GrdddlVendor_SelectedIndexChanged" onchange="javascript:SETVALUE(this);">
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="GrdlblVendorName" runat="server" Text='<%# Eval("VendorName") %>'
                                                                            CssClass="Display_None"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <%--19--%>
                                                                <asp:TemplateField HeaderText="OrdQty">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrdtxtOrdQty" runat="server" CssClass="TextBoxNumeric" onKeydown="javascript:CalculateGrid(this);"
                                                                            OnChange="javascript:CalculateGrid(this);" MaxLength="10" Text='<%# Bind("OrdQty") %>'
                                                                            Width="40px"></asp:TextBox>
                                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderordqty" runat="server" FilterType="Numbers, Custom"
                                                                            ValidChars="." TargetControlID="GrdtxtOrdQty" />
                                                                        <asp:DropDownList ID="GrdddlUNITCONVERT" runat="server" Width="80px" AutoPostBack="true"
                                                                            OnSelectedIndexChanged="GrdddlUNITCONVERT_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                </asp:TemplateField>
                                                                <%-- 20--%>
                                                                <asp:TemplateField HeaderText="Rate">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrdtxtRate" runat="server" CssClass="TextBoxNumeric" onKeydown="javascript:CalculateGrid(this);"
                                                                            OnChange="javascript:CalculateGrid(this);" MaxLength="10" Text='<%# Bind("AvgPurRate") %>'
                                                                            TextMode="SingleLine" Width="60px"></asp:TextBox>
                                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderrate" runat="server" FilterType="Numbers, Custom"
                                                                            ValidChars="." TargetControlID="GrdtxtRate" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="60px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="60px" />
                                                                </asp:TemplateField>
                                                                <%-- 21--%>
                                                                <asp:TemplateField HeaderText="GST (%)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrdtxtPerVAT" runat="server" CssClass="TextBoxNumeric" onkeydown="javascript:CalculateGrid(this);"
                                                                            OnChange="javascript:CalculateGrid(this);" MaxLength="10" Text='<%# Bind("perGST") %>'
                                                                            Width="40px"></asp:TextBox>
                                                                        <asp:RangeValidator runat="server" ID="RGrdtxtPerVAT" ControlToValidate="GrdtxtPerVAT"
                                                                            Type="Double" MinimumValue="0.00" MaximumValue="12.50" ErrorMessage="***" />
                                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderpervat" runat="server" FilterType="Numbers, Custom"
                                                                            ValidChars="." TargetControlID="GrdtxtPerVAT" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                </asp:TemplateField>
                                                                <%-- 22--%>
                                                                <asp:TemplateField HeaderText="CGST (%)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrdtxtCGSTPer" runat="server" CssClass="TextBoxNumeric" onkeydown="javascript:CalculateGrid(this);"
                                                                            OnChange="javascript:CalculateGrid(this);" MaxLength="10" Text='<%# Bind("CGSTPer") %>'
                                                                            Width="40px"></asp:TextBox>
                                                                        <asp:RangeValidator runat="server" ID="RGrdtxtCGSTPer" ControlToValidate="GrdtxtCGSTPer"
                                                                            Type="Double" MinimumValue="0.00" MaximumValue="12.50" ErrorMessage="***" />
                                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderperCGST" runat="server"
                                                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="GrdtxtCGSTPer" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                </asp:TemplateField>
                                                                <%-- 23--%>
                                                                <asp:TemplateField HeaderText="CGSTAmt">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrdtxtCGSTAmt" runat="server" CssClass="TextBoxNumericReadOnly"
                                                                            Enabled="false" MaxLength="10" Text='<%# Bind("CGSTAmt") %>' Width="40px"></asp:TextBox>
                                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderCGST" runat="server" FilterType="Numbers, Custom"
                                                                            ValidChars="." TargetControlID="GrdtxtCGSTAmt" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                </asp:TemplateField>
                                                                <%--24--%>
                                                                <asp:TemplateField HeaderText="SGST (%)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrdtxtSGSTPer" runat="server" CssClass="TextBoxNumeric" onkeydown="javascript:CalculateGrid(this);"
                                                                            OnChange="javascript:CalculateGrid(this);" MaxLength="10" Text='<%# Bind("SGSTPer") %>'
                                                                            Width="40px"></asp:TextBox>
                                                                        <asp:RangeValidator runat="server" ID="RGrdtxtSGSTPer" ControlToValidate="GrdtxtSGSTPer"
                                                                            Type="Double" MinimumValue="0.00" MaximumValue="12.50" ErrorMessage="***" />
                                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderperSGST" runat="server"
                                                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="GrdtxtSGSTPer" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                </asp:TemplateField>
                                                                <%--25--%>
                                                                <asp:TemplateField HeaderText="SGSTAmt">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrdtxtSGSTAmt" runat="server" CssClass="TextBoxNumericReadOnly"
                                                                            Enabled="false" MaxLength="10" Text='<%# Bind("SGSTAmt") %>' Width="40px"></asp:TextBox>
                                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderSGST" runat="server" FilterType="Numbers, Custom"
                                                                            ValidChars="." TargetControlID="GrdtxtSGSTAmt" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                </asp:TemplateField>
                                                                <%-- 26--%>
                                                                <asp:TemplateField HeaderText="IGST (%)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrdtxtIGSTPer" runat="server" CssClass="TextBoxNumeric" onkeydown="javascript:CalculateGrid(this);"
                                                                            OnChange="javascript:CalculateGrid(this);" MaxLength="10" Text='<%# Bind("IGSTPer") %>'
                                                                            Width="40px"></asp:TextBox>
                                                                        <asp:RangeValidator runat="server" ID="RGrdtxtIGSTPer" ControlToValidate="GrdtxtIGSTPer"
                                                                            Type="Double" MinimumValue="0.00" MaximumValue="12.50" ErrorMessage="***" />
                                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderperIGST" runat="server"
                                                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="GrdtxtIGSTPer" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                </asp:TemplateField>
                                                                <%-- 27--%>
                                                                <asp:TemplateField HeaderText="IGSTAmt">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrdtxtIGSTAmt" runat="server" CssClass="TextBoxNumericReadOnly"
                                                                            Enabled="false" MaxLength="10" Text='<%# Bind("IGSTAmt") %>' Width="40px"></asp:TextBox>
                                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderIGST" runat="server" FilterType="Numbers, Custom"
                                                                            ValidChars="." TargetControlID="GrdtxtIGSTAmt" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                </asp:TemplateField>
                                                                <%-- 28--%>
                                                                <asp:TemplateField HeaderText="DISC (%)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrdtxtPerDISC" runat="server" CssClass="TextBoxNumeric" onKeydown="javascript:CalculateGrid(this);"
                                                                            OnChange="javascript:CalculateGrid(this);" MaxLength="10" Text='<%# Bind("perdisc") %>'
                                                                            Width="40px"></asp:TextBox>
                                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderperdisc" runat="server"
                                                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="GrdtxtPerDISC" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                </asp:TemplateField>
                                                                <%-- 29--%>
                                                                <asp:TemplateField HeaderText="DISC">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrdtxtDISC" runat="server" CssClass="TextBoxNumericReadOnly" Enabled="false"
                                                                            MaxLength="10" Text='<%# Bind("disc") %>' Width="40px"></asp:TextBox>
                                                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtenderdisc" runat="server" FilterType="Numbers, Custom"
                                                                            ValidChars="." TargetControlID="GrdtxtDISC" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Width="40px" />
                                                                </asp:TemplateField>
                                                                <%-- 30--%>
                                                                <asp:TemplateField HeaderText="Vendor1">
                                                                    <ItemTemplate>
                                                                        <%--  <asp:Label ID="GrdlblVendorID" runat="server" Text='<%# Eval("VendorID") %>' Width="30px"></asp:Label>--%>
                                                                        <asp:TextBox ID="GrdtxtVendorID" runat="server" CssClass="TextBoxNumeric" MaxLength="10"
                                                                            Text='<%# Bind("VendorID") %>' Width="20px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="120px" CssClass="Display_None" />
                                                                    <ItemStyle Width="120px" CssClass="Display_None" />
                                                                </asp:TemplateField>
                                                                <%-- 31--%>
                                                                <asp:BoundField HeaderText="ItemId" DataField="ItemId">
                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                                <%-- 32--%>
                                                                <asp:TemplateField HeaderText="Remark">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="GrdtxtRemarkForPO" runat="server" CssClass="TextBox" TextMode="MultiLine"
                                                                            Text='<%# Bind("RemarkForPO") %>' Width="190px">
                                                                        </asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- 33--%>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImgAddSupplier" runat="server" ValidationGroup="AddSupplier"
                                                                            ImageUrl="~/Images/Icon/Gridadd.png" OnClick="ImgAddSupplier_Click" ToolTip="Add To Purchase Order" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <%-- 34--%>
                                                                <asp:BoundField HeaderText="RequisitionCafeId" DataField="RequisitionCafeId">
                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>



                                                    <td align="left">
                                                        <asp:LinkButton ID="hyl_AddAll" runat="server" CssClass="linkButton" ToolTip="Add All Item To Purchase Order"
                                                            OnClick="hyl_AddAll_Click">Add All</asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="hylAddCancel" runat="server" CssClass="linkButton" OnClick="hylAddCancel_Click">Cancel</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </fieldset>
                                </td>
                            </tr>

                            <asp:Label ID="tremsandcondition" runat="server" Visible="false"></asp:Label>
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
            <div id="dialog" class="PopUpSample" runat="server">
                <div id="progressBackgroundFilter1">
                </div>
                <div id="DivSHOWPOPUP" class="PopUpSample" runat="server">
                    <table width="95%" cellspacing="16px">
                        <tr>
                            <td colspan="2">
                                <asp:Label runat="server" ID="LBLTEXT" Text="ARE YOU SURE TO DELETE PURCHASE ORDER"
                                    CssClass="LabelPOP"></asp:Label>
                            </td>
                            <td id="Td1" runat="server" align="right">
                                <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="javascript:HidePOPForceClose(2);"
                                    ImageUrl="~/Images/New Icon/close-button1.png" ToolTip="Close" OnClick="ImgBtnClose_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="divPrintsd" class="ScrollableDiv_FixHeightWidthPOP">
                                    <asp:GridView ID="GRDPOPUPFOREDIT" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                        <Columns>
                                            <asp:BoundField DataField="POID" HeaderText="POID">
                                                <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" VerticalAlign="Middle"
                                                    Wrap="False" />
                                                <ItemStyle CssClass="Display_None" HorizontalAlign="Left" VerticalAlign="Middle"
                                                    Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PONO" HeaderText="Purchase Order No">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PODate" HeaderText="Purchase Order Date">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SuplierName" HeaderText="Supplier Name">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="POAmount" HeaderText="Amount">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Status" HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="Label">
                                Remark:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtRemarkAlL" Width="760px" TextMode="MultiLine" runat="server"
                                    CssClass="TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="BTNCANCELPO" CssClass="buttonpayment" runat="server" Text="CANCEL PURCHASE ORDER"
                                    CausesValidation="false" OnClick="BTNCANCELPO_Click" />
                                <ajax:RoundedCornersExtender ID="RoundedCornersExtender5" runat="server" TargetControlID="BTNCANCELPO"
                                    Corners="All" Radius="8" BorderColor="Gray">
                                </ajax:RoundedCornersExtender>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="DIVTERMSSUPPLIER" class="PopUpSample" runat="server">
                <div id="progressBackgroundFilter">
                </div>
                <div id="Div7" class="PopUpSample" runat="server">
                    <table width="98%" cellspacing="16px">
                        <tr>
                            <td colspan="2">
                                <asp:Label runat="server" ID="Label1" Text="Terms & Conditions :" CssClass="LabelPOP"></asp:Label>
                                <asp:Label runat="server" ID="LBLSUPPLERID" CssClass="Display_None"></asp:Label>
                            </td>
                            <td id="Td2" runat="server" align="right">
                                <asp:ImageButton ID="ImageButton3" runat="server" OnClientClick="javascript:HidePOPTermsPOSupplier();"
                                    ImageUrl="~/Images/New Icon/close-button1.png" ToolTip="Close Box" OnClick="ImgBtnClose_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Label" colspan="3">
                                <asp:TextBox runat="server" ID="POPUPTXTTERMSSUPPLIER" CssClass="TextBox" Width="850px"
                                    Height="100px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left">
                                <asp:Label runat="server" ID="Label2" Text="Payment Conditions :" CssClass="LabelPOP"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Label" colspan="3">
                                <asp:TextBox runat="server" ID="POPUPTXTTERMSSUPPLIERPayment" CssClass="TextBox"
                                    Width="850px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <asp:Button ID="BTNPOPUPTERMS" CssClass="button" runat="server" Text="SET" CausesValidation="false"
                                    OnClick="BTNPOPUPTERMS_Click" />
                                <ajax:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="BTNPOPUPTERMS"
                                    Corners="All" Radius="8" BorderColor="Gray">
                                </ajax:RoundedCornersExtender>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
