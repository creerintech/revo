<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialInwardRegister1.aspx.cs" Inherits="Transactions_MaterialInwardRegister1" Title="Material Inward Register" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<script language="javascript" type="text/javascript">

function CalcVatdiscPer()
{
document.getElementById('<%= txtDiscountPer.ClientID %>').value=((document.getElementById('<%= txtDiscount.ClientID %>').value/document.getElementById('<%= txtSubTotal.ClientID %>').value)*100).toFixed(2);
document.getElementById('<%= txtVATPer.ClientID %>').value=((document.getElementById('<%= txtVATAmount.ClientID %>').value/document.getElementById('<%= txtSubTotal.ClientID %>').value)*100).toFixed(2);
}

function ClickDoneBtn40()
{
var key = window.event.keyCode;
if (key == 9)
{
document.getElementById("txtBillDate").focus();
}
else
{
}
}


function ClickDoneBtn5()
{
var key = window.event.keyCode;
if (key == 9)
{
document.getElementById("txtBillNo").focus();
}
else
{
}
}


function ClickDoneBtn2()
{
var key = window.event.keyCode;
if (key == 13)
{
document.getElementById('<%= ImgBtnAddTemplate.ClientID %>').click();
}
else
{
}
}

function ClickDoneBtn1()
{
var key = window.event.keyCode;
if (key == 13)
{
document.getElementById('<%= ImgBtnAddItem.ClientID %>').click();
}
else
{
}
}

function ClickDoneBtn()
{
var key = window.event.keyCode;
if (key == 13)
{
document.getElementById('<%= BtnDone.ClientID %>').click();
}
else
{
}
}


function FocusToDDL()
{
var key = window.event.keyCode;
if (key = 9)
{
document.getElementById('<%= ddlItems.ClientID %>').focus();
}
else
{
}

}

function FocusToDDLSupplier()
{
document.getElementById('<%= ddlSuplier.ClientID %>').focus();
}
function FocusToBillNo()
{
document.getElementById('<%= ddlCategory.ClientID %>').focus();
}


function CalSubGrandTotal()
{
var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');   
var _TxtDiscountPer = document.getElementById('<%= txtDiscountPer.ClientID %>');   
var _TxtDiscount = document.getElementById('<%= txtDiscount.ClientID %>');   
var _TxtVatPer = document.getElementById('<%= txtVATPer.ClientID %>');   
var _TxtVat = document.getElementById('<%= txtVATAmount.ClientID %>');   
var _txtOtherCharges = document.getElementById('<%= txtOtherCharges.ClientID %>');   
var _TxtGrandTotal = document.getElementById('<%= txtNetTotal.ClientID %>');   

var _DisAmt=0,_VatAmt=0;
var t1=0,t2=0,t3=0,t4=0,t5=0,t6=0,t7=0,t8=0;

_DisAmt=(_TxtSubTotal.value)*(_TxtDiscount.value)/100;
_VatAmt=(_TxtSubTotal.value)*(_TxtVat.value)/100;
t1=parseFloat(_TxtSubTotal.value)+parseFloat(_VatAmt) -parseFloat(_DisAmt);
//       t3=parseFloat(t1) + parseFloat(_VatAmt);

//       t4=parseFloat(t3)- parseFloat(_DisAmt);
_TxtGrandTotal.value=parseFloat(t1).toFixed(2);
}

function CalPercentage_Amount(TxtBoxId)
{
var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');   
var _TxtDiscountPer = document.getElementById('<%= txtDiscountPer.ClientID %>');   
var _TxtDiscount = document.getElementById('<%= txtDiscount.ClientID %>');   
var _TxtVatPer = document.getElementById('<%= txtVATPer.ClientID %>');   
var _TxtVat = document.getElementById('<%= txtVATAmount.ClientID %>');
var _txtDekhrekhPer = document.getElementById('<%= txtDekhrekhPer.ClientID %>');
var _txtDekhrekhAmt = document.getElementById('<%= txtDekhrekhAmt.ClientID %>'); 
var _txtHamaliPer = document.getElementById('<%= txtHamaliPer.ClientID %>');
var _txtHamaliAmt = document.getElementById('<%= txtHamaliAmt.ClientID %>');
var _txtCESSPer = document.getElementById('<%= txtCESSPer.ClientID %>');
var _txtCESSAmt = document.getElementById('<%= txtCESSAmt.ClientID %>');
var _txtFreightPer = document.getElementById('<%= txtFreightPer.ClientID %>');
var _txtFreightAmt = document.getElementById('<%= txtFreightAmt.ClientID %>');
var _txtPackingPer = document.getElementById('<%= txtPackingPer.ClientID %>');
var _txtPackingAmt = document.getElementById('<%= txtPackingAmt.ClientID %>');
var _txtPostagePer = document.getElementById('<%= txtPostagePer.ClientID %>');
var _txtPostageAmt = document.getElementById('<%= txtPostageAmt.ClientID %>');   
var _txtOtherCharges = document.getElementById('<%= txtOtherCharges.ClientID %>');   
var _TxtGrandTotal = document.getElementById('<%= txtNetTotal.ClientID %>');   


var t1=0,t2=0,t3=0,t4=0,t5=0,SubTot=0,OthChrg=0,TH1=0,TH2=0,TH11=0,TH22=0;
var DR1=0,DR2=0,HM1=0,HM2=0,CS1=0,CS2=0,FR1=0,FR2=0,PK1=0,PK2=0,PS1=0,PS2=0;
if(_TxtSubTotal.value != "") SubTot=_TxtSubTotal.value;
if(_txtOtherCharges.value != "") OthChrg=_txtOtherCharges.value;

if(_TxtDiscountPer.value != "") TH1=_TxtDiscountPer.value; 
if(_TxtDiscount.value != "") TH2=_TxtDiscount.value;

if(_TxtVatPer.value != "") TH11=_TxtVatPer.value; 
if(_TxtVat.value != "") TH22=_TxtVat.value;

if(_txtDekhrekhPer.value != "") DR1=_txtDekhrekhPer.value; 
if(_txtDekhrekhAmt.value != "") DR2=_txtDekhrekhAmt.value;

if(_txtHamaliPer.value != "") HM1=_txtHamaliPer.value; 
if(_txtHamaliAmt.value != "") HM2=_txtHamaliAmt.value;

if(_txtCESSPer.value != "") CS1=_txtCESSPer.value; 
if(_txtCESSAmt.value != "") CS2=_txtCESSAmt.value;

if(_txtFreightPer.value != "") FR1=_txtFreightPer.value; 
if(_txtFreightAmt.value != "") FR2=_txtFreightAmt.value;

if(_txtPackingPer.value != "") PK1=_txtPackingPer.value; 
if(_txtPackingAmt.value != "") PK2=_txtPackingAmt.value;

if(_txtPostagePer.value != "") PS1=_txtPostagePer.value; 
if(_txtPostageAmt.value != "") PS2=_txtPostageAmt.value;

if(TxtBoxId==_TxtDiscountPer)
{
if(_TxtDiscountPer.value!="")
{            
TH1=_TxtDiscountPer.value;            
TH2=parseFloat((parseFloat(SubTot)*parseFloat(TH1))/100).toFixed(2);  
_TxtDiscount.value = parseFloat(TH2).toFixed(2); 
}
else
{
TH1="0";            
TH2="0"; 
_TxtDiscount.value = "0";
}
}
if(TxtBoxId==_TxtDiscount)
{      
if(_TxtDiscount.value!="")
{      

TH2=_TxtDiscount.value;            
TH1=parseFloat((parseFloat(TH2)*100)/parseFloat(SubTot)).toFixed(2);
_TxtDiscountPer.value = parseFloat(TH1).toFixed(2);
}
else
{
TH2="0";            
TH1="0";
_TxtDiscountPer.value = "0";
}
} 
if(TxtBoxId==_TxtVatPer)
{
if(_TxtVatPer.value!="")
{            
TH11=_TxtVatPer.value;            
TH22=parseFloat((parseFloat(SubTot)*parseFloat(TH11))/100).toFixed(2);
_TxtVat.value = parseFloat(TH22).toFixed(2); 
}
else
{
TH11="0";            
TH22="0";
_TxtVat.value = "0";
}
}
if(TxtBoxId==_TxtVat)
{      
if(_TxtVat.value!="")
{      

TH22=_TxtVat.value;            
TH11=parseFloat((parseFloat(TH22)*100)/parseFloat(SubTot)).toFixed(2);                
_TxtVatPer.value = parseFloat(TH11).toFixed(2);
}
else
{
TH22="0";            
TH11="0";
_TxtVatPer.value = "0";
}
} 

if(TxtBoxId==_txtCESSPer)
{
if(_txtCESSPer.value!="")
{            
CS1=_txtCESSPer.value;            
CS2=parseFloat((parseFloat(SubTot)*parseFloat(CS1))/100).toFixed(2);
_txtCESSAmt.value = parseFloat(CS2).toFixed(2); 
}
else
{
CS1="0";            
CS2="0";
_txtCESSAmt.value = "0";
}
}
if(TxtBoxId==_txtCESSAmt)
{      
if(_txtCESSAmt.value!="")
{      
CS2=_txtCESSAmt.value;            
CS1=parseFloat((parseFloat(CS2)*100)/parseFloat(SubTot)).toFixed(2);                
_txtCESSPer.value = parseFloat(CS1).toFixed(2);
}
else
{
CS2="0";            
CS1="0";
_txtCESSPer.value = "0";
}
}     

t1=parseFloat(SubTot)+parseFloat(TH22)+parseFloat(OthChrg)+parseFloat(DR2)+parseFloat(HM2)+parseFloat(CS2)+parseFloat(FR2)+parseFloat(PK2)+parseFloat(PS2) -parseFloat(TH2);
_TxtGrandTotal.value=parseFloat(t1).toFixed(2);
}

function CalPercentage_Amount1() {
    var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');
    var _TxtDiscountPer = document.getElementById('<%= txtDiscountPer.ClientID %>');
    var _TxtDiscount = document.getElementById('<%= txtDiscount.ClientID %>');
    var _TxtVatPer = document.getElementById('<%= txtVATPer.ClientID %>');
    var _TxtVat = document.getElementById('<%= txtVATAmount.ClientID %>');
    var _txtDekhrekhPer = document.getElementById('<%= txtDekhrekhPer.ClientID %>');
    var _txtDekhrekhAmt = document.getElementById('<%= txtDekhrekhAmt.ClientID %>');
    var _txtHamaliPer = document.getElementById('<%= txtHamaliPer.ClientID %>');
    var _txtHamaliAmt = document.getElementById('<%= txtHamaliAmt.ClientID %>');
    var _txtCESSPer = document.getElementById('<%= txtCESSPer.ClientID %>');
    var _txtCESSAmt = document.getElementById('<%= txtCESSAmt.ClientID %>');
    var _txtFreightPer = document.getElementById('<%= txtFreightPer.ClientID %>');
    var _txtFreightAmt = document.getElementById('<%= txtFreightAmt.ClientID %>');
    var _txtPackingPer = document.getElementById('<%= txtPackingPer.ClientID %>');
    var _txtPackingAmt = document.getElementById('<%= txtPackingAmt.ClientID %>');
    var _txtPostagePer = document.getElementById('<%= txtPostagePer.ClientID %>');
    var _txtPostageAmt = document.getElementById('<%= txtPostageAmt.ClientID %>');
    var _txtOtherCharges = document.getElementById('<%= txtOtherCharges.ClientID %>');
    var _TxtGrandTotal = document.getElementById('<%= txtNetTotal.ClientID %>');


    var t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, SubTot = 0, OthChrg = 0, TH1 = 0, TH2 = 0, TH11 = 0, TH22 = 0;
    var DR1 = 0, DR2 = 0, HM1 = 0, HM2 = 0, CS1 = 0, CS2 = 0, FR1 = 0, FR2 = 0, PK1 = 0, PK2 = 0, PS1 = 0, PS2 = 0;
    if (_TxtSubTotal.value != "") SubTot = _TxtSubTotal.value;
    if (_txtOtherCharges.value != "") OthChrg = _txtOtherCharges.value;

    if (_TxtDiscountPer.value != "") TH1 = _TxtDiscountPer.value;
    if (_TxtDiscount.value != "") TH2 = _TxtDiscount.value;

    if (_TxtVatPer.value != "") TH11 = _TxtVatPer.value;
    if (_TxtVat.value != "") TH22 = _TxtVat.value;

    if (_txtDekhrekhPer.value != "") DR1 = _txtDekhrekhPer.value;
    if (_txtDekhrekhAmt.value != "") DR2 = _txtDekhrekhAmt.value;

    if (_txtHamaliPer.value != "") HM1 = _txtHamaliPer.value;
    if (_txtHamaliAmt.value != "") HM2 = _txtHamaliAmt.value;

    if (_txtCESSPer.value != "") CS1 = _txtCESSPer.value;
    if (_txtCESSAmt.value != "") CS2 = _txtCESSAmt.value;

    if (_txtFreightPer.value != "") FR1 = _txtFreightPer.value;
    if (_txtFreightAmt.value != "") FR2 = _txtFreightAmt.value;

    if (_txtPackingPer.value != "") PK1 = _txtPackingPer.value;
    if (_txtPackingAmt.value != "") PK2 = _txtPackingAmt.value;

    if (_txtPostagePer.value != "") PS1 = _txtPostagePer.value;
    if (_txtPostageAmt.value != "") PS2 = _txtPostageAmt.value;

    if (TxtBoxId == _TxtDiscountPer) {
        if (_TxtDiscountPer.value != "") {
            TH1 = _TxtDiscountPer.value;
            TH2 = parseFloat((parseFloat(SubTot) * parseFloat(TH1)) / 100).toFixed(2);
            _TxtDiscount.value = parseFloat(TH2).toFixed(2);
        }
        else {
            TH1 = "0";
            TH2 = "0";
            _TxtDiscount.value = "0";
        }
    }
    if (TxtBoxId == _TxtDiscount) {
        if (_TxtDiscount.value != "") {

            TH2 = _TxtDiscount.value;
            TH1 = parseFloat((parseFloat(TH2) * 100) / parseFloat(SubTot)).toFixed(2);
            _TxtDiscountPer.value = parseFloat(TH1).toFixed(2);
        }
        else {
            TH2 = "0";
            TH1 = "0";
            _TxtDiscountPer.value = "0";
        }
    }
    if (TxtBoxId == _TxtVatPer) {
        if (_TxtVatPer.value != "") {
            TH11 = _TxtVatPer.value;
            TH22 = parseFloat((parseFloat(SubTot) * parseFloat(TH11)) / 100).toFixed(2);
            _TxtVat.value = parseFloat(TH22).toFixed(2);
        }
        else {
            TH11 = "0";
            TH22 = "0";
            _TxtVat.value = "0";
        }
    }
    if (TxtBoxId == _TxtVat) {
        if (_TxtVat.value != "") {

            TH22 = _TxtVat.value;
            TH11 = parseFloat((parseFloat(TH22) * 100) / parseFloat(SubTot)).toFixed(2);
            _TxtVatPer.value = parseFloat(TH11).toFixed(2);
        }
        else {
            TH22 = "0";
            TH11 = "0";
            _TxtVatPer.value = "0";
        }
    }

    if (TxtBoxId == _txtCESSPer) {
        if (_txtCESSPer.value != "") {
            CS1 = _txtCESSPer.value;
            CS2 = parseFloat((parseFloat(SubTot) * parseFloat(CS1)) / 100).toFixed(2);
            _txtCESSAmt.value = parseFloat(CS2).toFixed(2);
        }
        else {
            CS1 = "0";
            CS2 = "0";
            _txtCESSAmt.value = "0";
        }
    }
    if (TxtBoxId == _txtCESSAmt) {
        if (_txtCESSAmt.value != "") {
            CS2 = _txtCESSAmt.value;
            CS1 = parseFloat((parseFloat(CS2) * 100) / parseFloat(SubTot)).toFixed(2);
            _txtCESSPer.value = parseFloat(CS1).toFixed(2);
        }
        else {
            CS2 = "0";
            CS1 = "0";
            _txtCESSPer.value = "0";
        }
    }

    t1 = parseFloat(SubTot) + parseFloat(TH22) + parseFloat(OthChrg) + parseFloat(DR2) + parseFloat(HM2) + parseFloat(CS2) + parseFloat(FR2) + parseFloat(PK2) + parseFloat(PS2) - parseFloat(TH2);
    _TxtGrandTotal.value = parseFloat(t1).toFixed(2);
} 


function callradio()
{  
var radio = document.getElementById('<%= rdoInwardType.ClientID%>');
for (var x = 0; x < radio.length; x ++) {
alert(radio.length);
if (radio[x].checked) 
{
alert("IN Checked");
document.getElementById('<%= TRTemplate.ClientID%>').style.visibility="hidden";
document.getElementById('<%= TRItem.ClientID%>').style.visibility="hidden";
document.getElementById('<%= TRPO.ClientID%>').style.visibility="visible";
}
else
{
alert("Out Checked");
document.getElementById('<%= TRTemplate.ClientID%>').style.visibility="visible";
document.getElementById('<%= TRItem.ClientID%>').style.visibility="visible";
document.getElementById('<%= TRPO.ClientID%>').style.visibility="hidden";
}
}

}  

function EnableControl(clicked_id)
{
alert(clicked_id);
if(clicked_id=="ImgBtnAddTemplate")
{
document.getElementById('<%= ddlCategory.ClientID%>').disabled = true;
document.getElementById('<%= ddlItems.ClientID%>').disabled = true;
document.getElementById('<%= ddlSuplier.ClientID%>').disabled = true;
document.getElementById('<%= ddlTemplate.ClientID%>').disabled = false;
}
else
{
document.getElementById('<%= ddlTemplate.ClientID%>').disabled = true;
document.getElementById('<%= ddlCategory.ClientID%>').disabled = false;
document.getElementById('<%= ddlItems.ClientID%>').disabled = false;
document.getElementById('<%= ddlSuplier.ClientID%>').disabled = false;
}

}
function IsValid() {
var textbox = $get("ddlSuplierPopUp");
if (textbox.value == "") {
return false;
}
else
return true;
}




function CalculateNetAmtForGrid(objGrid)
{

var _GridDetails = document.getElementById('<%= GrdInwardPO.ClientID %>');
var rowIndex = objGrid.offsetParent.parentNode.rowIndex;

var ORDQTY = (_GridDetails.rows[rowIndex].cells[8].children[0]);
var INWQTY = (_GridDetails.rows[rowIndex].cells[9].children[0]);

var PENQTY=(_GridDetails.rows[rowIndex].cells[11].children[0]);
var INWRATE=(_GridDetails.rows[rowIndex].cells[12].children[0]);
var PORATE=(_GridDetails.rows[rowIndex].cells[14].children[0]);
var DIFF=(_GridDetails.rows[rowIndex].cells[15].children[0]);
var AMOUNT=(_GridDetails.rows[rowIndex].cells[16].children[0]);
var PERTAX=(_GridDetails.rows[rowIndex].cells[17].children[0]);
var TAX=(_GridDetails.rows[rowIndex].cells[18].children[0]);
var PERDISC=(_GridDetails.rows[rowIndex].cells[19].children[0]);
var DISC=(_GridDetails.rows[rowIndex].cells[20].children[0]);
var NETAMT=(_GridDetails.rows[rowIndex].cells[21].children[0]);
var LOCID = (_GridDetails.rows[rowIndex].cells[27].children[0]);
var PENDINGQRDQTY = (_GridDetails.rows[rowIndex].cells[29].children[0]);
var total=0;

var _TxtSubTotal = document.getElementById('<%= txtSubTotal.ClientID %>');   
var _TxtDiscountPer = document.getElementById('<%= txtDiscountPer.ClientID %>');   
var _TxtDiscount = document.getElementById('<%= txtDiscount.ClientID %>');   
var _TxtVatPer = document.getElementById('<%= txtVATPer.ClientID %>');   
var _TxtVat = document.getElementById('<%= txtVATAmount.ClientID %>');   
var _txtOtherCharges = document.getElementById('<%= txtOtherCharges.ClientID %>');   
var _TxtGrandTotal = document.getElementById('<%= txtNetTotal.ClientID %>');
var _txtst = document.getElementById('<%= txtst.ClientID %>');

var ORDSTR = ORDQTY.value;
var PENSTR = PENQTY.value;
if (ORDQTY.value.length > 2) {
    var n = ORDSTR.indexOf("-");
    ORDSTR = ORDSTR.substring(0, ORDSTR.indexOf("-"));
}
else {
}
if (ORDSTR == "" || isNaN(ORDSTR)) {
    ORDSTR.value = 0;
}

if (INWQTY.value=="" || isNaN(INWQTY.value))
{
INWQTY.value=0;
}
if (PENDINGQRDQTY.value == "" || isNaN(PENDINGQRDQTY.value)) {
    PENDINGQRDQTY.value = 0;
}


if (PENQTY.value=="" || isNaN(PENQTY.value))
{
PENQTY.value=0;           
}

if (INWRATE.value=="" || isNaN(INWRATE.value))
{
INWRATE.value=0;           
}

if (PORATE.value=="" || isNaN(PORATE.value))
{
PORATE.value=0;           
}

if (DIFF.value=="" || isNaN(DIFF.value))
{
DIFF.value=0;           
}

if (AMOUNT.value=="" || isNaN(AMOUNT.value))
{
AMOUNT.value=0;           
}

if (PERTAX.value=="" || isNaN(PERTAX.value))
{
PERTAX.value=0;           
}

if (TAX.value=="" || isNaN(TAX.value))
{
TAX.value=0;           
}

if (PERDISC.value=="" || isNaN(PERDISC.value))
{
PERDISC.value=0;           
}

if (DISC.value=="" || isNaN(DISC.value))
{
DISC.value=0;           
}

if (NETAMT.value=="" || isNaN(NETAMT.value))
{
NETAMT.value=0;           
}

if (LOCID.value=="" || isNaN(LOCID.value))
{
LOCID.value=0;           
}


if(parseFloat(ORDSTR)==0)
{
PENQTY.value=0;
}
else
{
if(parseFloat(_txtst.value)==0)
{
    PENQTY.value = parseFloat(parseFloat(ORDSTR) - parseFloat(INWQTY.value)).toFixed(2);
    PENQTY.value = parseFloat(parseFloat(PENDINGQRDQTY.value) - parseFloat(INWQTY.value)).toFixed(2);
}
else
{
    PENQTY.value = parseFloat(parseFloat(ORDSTR) - parseFloat(INWQTY.value)).toFixed(2);
    PENQTY.value = parseFloat(parseFloat(PENDINGQRDQTY.value) - parseFloat(INWQTY.value)).toFixed(2);
if(parseFloat(PENQTY.title)<parseFloat(INWQTY.value))
{

}
}
if(parseFloat(PENQTY.value)<0)
{
PENQTY.value=0;
}
}

AMOUNT.value=parseFloat(parseFloat(INWQTY.value)*parseFloat(INWRATE.value)).toFixed(2);
DIFF.value=parseFloat(parseFloat(PORATE.value)-parseFloat(INWRATE.value)).toFixed(2);
DISC.value=parseFloat(((parseFloat(INWQTY.value)*parseFloat(INWRATE.value))/100)*parseFloat(PERDISC.value)).toFixed(2);
TAX.value=parseFloat((((parseFloat(INWQTY.value)*parseFloat(INWRATE.value))-parseFloat(DISC.value))/100)*parseFloat(PERTAX.value)).toFixed(2);
NETAMT.value=parseFloat(parseFloat(AMOUNT.value)+(parseFloat(TAX.value)-parseFloat(DISC.value))).toFixed(2);


var FSUBTOTAL=0;
var FNETTOTAL=0;
var PerTax=0;
var AmtTax=0;
var PerDisc=0;
var AmtDisc=0;

var FAMOUNT;
var FNETAMT;
var FPERTAX;
var FTAX;
var FPERDISC;
var FDISC;
for (var i=1;i<_GridDetails.rows.length;i++)
{ 
FAMOUNT=(_GridDetails.rows[i].cells[16].children[0]);
FNETAMT=(_GridDetails.rows[i].cells[21].children[0]);
FPERTAX=(_GridDetails.rows[i].cells[17].children[0]);
FTAX=(_GridDetails.rows[i].cells[18].children[0]);
FPERDISC=(_GridDetails.rows[i].cells[19].children[0]);
FDISC=(_GridDetails.rows[i].cells[20].children[0]);

FSUBTOTAL=parseFloat(FAMOUNT.value)+parseFloat(FSUBTOTAL);
FNETTOTAL=parseFloat(FNETAMT.value)+parseFloat(FNETTOTAL);
AmtTax=parseFloat(FTAX.value)+parseFloat(AmtTax);
AmtDisc=parseFloat(FDISC.value)+parseFloat(AmtDisc);

}
_TxtSubTotal.value=parseFloat(FSUBTOTAL).toFixed(2);
_TxtGrandTotal.value=parseFloat(FNETTOTAL).toFixed(2);
_TxtVat.value=parseFloat(AmtTax).toFixed(2);
_TxtDiscount.value=parseFloat(AmtDisc).toFixed(2);

CalPercentage_Amount1();
}

      
function ValueOfDDL(objGrid)
{
var _GridDetails = document.getElementById('<%= GrdInwardPO.ClientID %>');  
var rowIndex=objGrid.offsetParent.parentNode.rowIndex;
var DDLSUPLIER=(_GridDetails.rows[rowIndex].cells[26].children[0]);
var LOCID=(_GridDetails.rows[rowIndex].cells[27].children[0]);
(_GridDetails.rows[rowIndex].cells[26].children[0]).value=(_GridDetails.rows[rowIndex].cells[25].children[0]).options[(_GridDetails.rows[rowIndex].cells[25].children[0]).selectedIndex].value;

}    
</script>
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:UpdateProgress ID="UpdateProgress1" runat="server" >
<ProgressTemplate>            
<div id="progressBackgroundFilter"></div>
<div id="processMessage">   
<center><span class="SubTitle">Loading....!!! </span></center>
<asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
</div>
</ProgressTemplate>
</asp:UpdateProgress>

    Search for Inward Material :
<asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged"></asp:TextBox>
<div id="divwidth"></div>
<ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
TargetControlID="TxtSearch" CompletionInterval="100" UseContextKey="True" FirstRowSelected ="true" 
ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionList"
CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                
</ajax:AutoCompleteExtender> 
         

</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Material Inward Register       
            
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<table width="100%" cellspacing="6">
<tr>
<td colspan="2">
<fieldset id="Fieldset1" runat="server" class="FieldSet" width="100%">
<asp:UpdatePanel ID="UpdatePanel4" runat="server">
<ContentTemplate>
<table width="100%">
<tr><td colspan="6" align="right">
 <asp:ImageButton ID="ImageButton3" runat="server" CssClass="Imagebutton" Height="20px" Width="20px"
 ImageUrl="~/Images/New Icon/Refresh-2-icon.png"  
ToolTip="Please Click If New Item, Template, Purchase Order, Category, Supplier, Location Are Add In Respective Master " 
onclick="IMGITEMREFRESH_Click"/> </td></tr> 
<tr>
<td class="Label">
    Inward Type :</td>
<td colspan="1">
<asp:RadioButtonList ID="rdoInwardType" runat="server" AutoPostBack="true"
RepeatDirection="Horizontal" CssClass="RadioButton" TabIndex="1"
onselectedindexchanged="rdoInwardType_SelectedIndexChanged" >
<asp:ListItem Selected="True" Value="0" >&nbsp;Purchase Order&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
<asp:ListItem Value="1" >&nbsp;Without Purchase Order</asp:ListItem>
</asp:RadioButtonList>

</td>
<td  > 
<asp:ImageButton ID="ImageButton2" runat="server" CssClass="Imagebutton" Height="20px" Width="20px"
 ImageUrl="~/Images/New Icon/Refresh-2-icon.png"  Visible="false"
ToolTip="Please Click If New Item, Template, Purchase Order, Category, Supplier, Location Are Add In Respective Master " 
onclick="IMGITEMREFRESH_Click"/></td>

<td>&nbsp;</td>
</tr>
<tr>
<td class="Label">
    Inward No :</td>
<td>
<asp:TextBox ID="TxtInwardNo" runat="server" CssClass="TextBox" Width="170px" TabIndex="2"></asp:TextBox>
<asp:TextBox ID="txtst" runat="server" CssClass="TextBox" Width="0px" TabIndex="2"></asp:TextBox>
</td>
<td class="Label">
    Inward Date:</td>
<td>
<asp:TextBox ID="TxtInwardDate" runat="server" CssClass="TextBox" 
ReadOnly="true" Width="170px" TabIndex="3"></asp:TextBox>
<asp:ImageButton ID="ImageInwardDate" runat="server" CausesValidation="false" 
CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="4"/>
<ajax:CalendarExtender ID="CalendarExtender1" runat="server" 
Format="dd-MMM-yyyy" PopupButtonID="ImageInwardDate" 
TargetControlID="TxtInwardDate" >
</ajax:CalendarExtender>
</td>
</tr>
<%--Add Design For Item Wise Or CateGory Wise--%>
<tr id="TRRadio" runat="server">
<td class="Label"></td>
<td colspan="3">
<asp:RadioButtonList ID="rdoTemplateItem" runat="server" AutoPostBack="true"
RepeatDirection="Horizontal" CssClass="RadioButton" TabIndex="5"
onselectedindexchanged="rdoTemplateItem_SelectedIndexChanged" >
<asp:ListItem Selected="True" Value="0" >&nbsp;Template Wise&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
<asp:ListItem Value="1" >&nbsp;Item Wise</asp:ListItem>
</asp:RadioButtonList>
<asp:Button  runat="server" ID="BtnForPopUp" CssClass="Display_None" OnClick="BtnForPopUp_Click"/>
<ajax:ModalPopupExtender ID="MPE" runat="server" TargetControlID="BtnForPopUp" PopupControlID="pnlPrint"
                        BackgroundCssClass="modalBackground" DropShadow="true"
                        CancelControlID="ImgClose" PopupDragHandleControlID="pnlPrint" OnOkScript="IsValid();" />

<asp:Panel ID="pnlPrint" runat="server" Width="100%"
Style="display: none; padding:10px; border:1px; border-style:solid;" BackColor="#E0ECF8" >
<fieldset class="FieldSet" id="Fieldset3" runat="server" width="95%">
        
<table width="100%" cellspacing="7">
    <tr>
        <td align="right" colspan="2">                                
                <asp:ImageButton ID="ImgClose" runat="server"                                                
                            ImageUrl="~/Images/New Icon/GridDelete.png" 
                               ToolTip="Close..!!" />
        </td>
    </tr>                      
    <tr>
        <td align="right" colspan="2">
        </td>
     <tr>
        <td class="Label">                                
              
            Suplier :</td>
         <td>
             <asp:DropDownList ID="ddlSuplierPopUp" runat="server" CssClass="ComboBox"  onKeydown="javascript:ClickDoneBtn();"
                 Width="172px">
             </asp:DropDownList>
            
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                 ControlToValidate="ddlSuplierPopUp" Display="None" Enabled="true" 
                 ErrorMessage="Suplier Name Is Required" InitialValue="0" SetFocusOnError="true" 
                 ValidationGroup="AddPOPSuplier">
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             </asp:RequiredFieldValidator>
             <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" 
                 Enabled="true" TargetControlID="RequiredFieldValidator1" 
                 WarningIconImageUrl="~/Images/Icon/Warning.png">
             </ajax:ValidatorCalloutExtender>
         </td>
    </tr>                      
    <tr>
       <td align="center" colspan="2">
           <asp:Button ID="BtnDone" runat="server" CssClass="button" 
               onclick="BtnDone_Click" Text="Done" ValidationGroup="AddPOPSuplier" />
        </td>
    </tr>
    </table>
</fieldset></asp:Panel>
</td>
</tr>

<tr>
<td class="Label">
    Challan / Bill No :</td>
<td>
<asp:TextBox ID="txtBillNo" runat="server" CssClass="TextBox" Width="170px" TabIndex="14"
         AutoPostBack="True" ontextchanged="txtBillNo_TextChanged" onKeydown="javascript:ClickDoneBtn40();"></asp:TextBox>
</td>
<td class="Label">
    Inward Through :</td>
<td>
<asp:RadioButtonList ID="rdoInwardThrough" runat="server" TabIndex="7"
RepeatDirection="Horizontal" CssClass="RadioButton">
<asp:ListItem  Value="0">&nbsp;Cash&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
<asp:ListItem Selected="True" Value="1">&nbsp;Credit</asp:ListItem>
</asp:RadioButtonList>
</td>
</tr>
<tr id="TRTemplate" runat="server">

<td class="Label">
    Template :</td>
<td>
<%--<asp:DropDownList ID="ddlTemplate" runat="server" TabIndex="8" onKeydown="javascript:ClickDoneBtn2();"
CssClass="ComboBox"  Width="172px">
</asp:DropDownList>--%>

 <ajax:ComboBox ID="ddlTemplate" runat="server" DropDownStyle="DropDown" 
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" onKeydown="javascript:ClickDoneBtn2();"
ItemInsertLocation="Append" Width="170px" CssClass="CustomComboBoxStyle" >
</ajax:ComboBox>

<asp:RequiredFieldValidator ID="RFVTEMPLATE" runat="server" 
ControlToValidate="ddlTemplate" Display="None" Enabled="true" 
ErrorMessage="Please Select Template" InitialValue="0" SetFocusOnError="true" 
ValidationGroup="AddTemplate">
</asp:RequiredFieldValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="true" 
TargetControlID="RFVTEMPLATE" WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>
<asp:ImageButton ID="ImgBtnAddTemplate" runat="server" CssClass="Imagebutton" 
 ImageUrl="~/Images/New Icon/AddImageNew.png" TabIndex="9" ValidationGroup="AddTemplate"
  ToolTip="Add Items Of Template" onclick="ImgBtnAddTemplate_Click"/>
</td>
<td class="Label">
    Category :</td>
<td>
<%--<asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" 
CssClass="ComboBox"  Width="172px" TabIndex="10" OnBlur="javascript:FocusToDDLSupplier();"
onselectedindexchanged="ddlCategory_SelectedIndexChanged">
</asp:DropDownList>--%>

 <ajax:ComboBox ID="ddlCategory" runat="server" DropDownStyle="DropDown" AutoPostBack="True" 
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" onKeydown="javascript:FocusToDDLSupplier();"
ItemInsertLocation="Append" Width="170px" CssClass="CustomComboBoxStyle" onselectedindexchanged="ddlCategory_SelectedIndexChanged">
</ajax:ComboBox>

</td>
</tr>
<tr id="TRItem" runat="server">

<td class="Label">
<asp:ImageButton ID="IMGITEMREFRESH" runat="server" CssClass="Imagebutton" Height="20px" Width="20px"
 ImageUrl="~/Images/New Icon/Refresh-2-icon.png" Visible="false" 
ToolTip="Please Click if New Item add In Item Master And Add in this Field" onclick="IMGITEMREFRESH_Click"/>              

    Items :</td>
<td>
<ajax:ComboBox ID="ddlItems" runat="server" DropDownStyle="DropDown" AutoPostBack="true" onKeydown="javascript:ClickDoneBtn1();"
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" Height="15px" Visible="false"
ItemInsertLocation="Append" Width="152px" CssClass="CustomComboBoxStyle">
</ajax:ComboBox>
<asp:RequiredFieldValidator ID="RFVITEM" runat="server" 
ControlToValidate="ddlItems" Display="None" Enabled="true" 
ErrorMessage="Item Is Required" InitialValue="0" SetFocusOnError="true" 
ValidationGroup="AddItem">
</asp:RequiredFieldValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="true" 
TargetControlID="RFVITEM" WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>
           
 
 <%--THIS START HERE--%>
<asp:UpdatePanel ID="UpdatePanel5" runat="server">
  <Triggers ><asp:PostBackTrigger ControlID ="ImgBtnAddItem" /></Triggers>
<ContentTemplate>
<asp:TextBox ID="TxtItemName" runat="server"
CssClass="search_List" Width="170px"   AutoPostBack="True" ontextchanged="TxtItemName_TextChanged"></asp:TextBox>

<ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemName"   runat="server" 
TargetControlID="TxtItemName"    CompletionInterval="100"                               
UseContextKey="True" FirstRowSelected ="true" 
CompletionSetCount="20" 
ShowOnlyCurrentWordInCompletionListItem="true"  
ServiceMethod="GetCompletionItemNameList"
CompletionListCssClass="AutoExtender"
CompletionListItemCssClass="AutoExtenderList"
CompletionListHighlightedItemCssClass="AutoExtenderHighlight"                         
></ajax:AutoCompleteExtender> 

<ajax:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="TxtItemName" 
WatermarkText="Type Item Name" WatermarkCssClass="water" />

<asp:ImageButton ID="ImgBtnAddItem" runat="server" CssClass="Imagebutton" 
 ImageUrl="~/Images/New Icon/AddImageNew.png" TabIndex="12" ValidationGroup="AddItem"
ToolTip="Add Items" onclick="ImgBtnAddItem_Click"/>   
</ContentTemplate>
</asp:UpdatePanel >
<%--THIS END HERE--%>



</td>
<td class="Label">
    Suplier :</td>
<td>
<%--<asp:DropDownList ID="ddlSuplier" runat="server" TabIndex="13" 
 Width="172px" CssClass="ComboBox"  onKeydown="javascript:ClickDoneBtn5();" >
</asp:DropDownList>--%>

 <ajax:ComboBox ID="ddlSuplier" runat="server" DropDownStyle="DropDown" 
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="170px" CssClass="CustomComboBoxStyle" onKeydown="javascript:ClickDoneBtn5();" >
</ajax:ComboBox>

<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
ControlToValidate="ddlSuplier" Display="None" Enabled="true" 
ErrorMessage="Please Select Suplier" InitialValue="0" SetFocusOnError="true" 
ValidationGroup="AddItem">
</asp:RequiredFieldValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="true" 
TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>




<asp:Label runat="server" ID="LblSuplier" Text="" Font-Bold="true" CssClass="Display_None"></asp:Label>
<asp:Label runat="server" ID="LblSuplierID" Text="" CssClass="Display_None" ></asp:Label>
</td>
</tr>
<%--End Design For Item Wise Or CateGory Wise--%>
<tr id="TRPO" runat="server">
<td class="Label">
    PO No :</td>
<td>
<ajax:ComboBox ID="ddlPONo" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="500px" CssClass="CustomComboBoxStyle" onselectedindexchanged="ddlPONo_SelectedIndexChanged">
</ajax:ComboBox>
<asp:RequiredFieldValidator ID="RFV1" runat="server" 
ControlToValidate="ddlPONo" Display="None" Enabled="true" 
ErrorMessage="Po No Is Required" InitialValue="0" SetFocusOnError="true" 
ValidationGroup="Add">
</asp:RequiredFieldValidator>
<ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="true" 
TargetControlID="RFV1" WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>
<asp:Label ID="LblPONo" runat="server" CssClass="Display_None"></asp:Label>
</td>
<td class="Label">
    Supplier Name :</td>
<td>
<asp:TextBox ID="txtSupplierName" runat="server" CssClass="TextBox" TabIndex="15" 
Width="170px"></asp:TextBox>
</td>
</tr>

<tr>
<td class="Label">
    Billing Address :</td>
<td colspan="1">
<asp:TextBox ID="TxtBillingAddress" runat="server" CssClass="TextBox" TabIndex="16"
Width="520px" TextMode="MultiLine" Height="20px"></asp:TextBox>
</td>
<td  class="Label">Bill Date :
</td><td>
<asp:TextBox ID="txtBillDate" runat="server" CssClass="TextBox" TabIndex="17" OnBlur="javascript:FocusToDDL();"
ReadOnly="false" Width="120px"></asp:TextBox>
<asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" 
CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
<ajax:CalendarExtender ID="CalendarExtender3" runat="server" 
Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" 
TargetControlID="txtBillDate">
</ajax:CalendarExtender>
</td>
</tr>
<tr>
<td class="Label">
    Shipping Adress :</td>
<td colspan="1">
<asp:TextBox ID="TxtShippingAddress" runat="server" CssClass="TextBox" TabIndex="18"
Width="520px" TextMode="MultiLine" Height="20px"></asp:TextBox>
</td>
<td class="Label">Inward No :</td>
<td>
<asp:TextBox ID="txtUserInwardNo" runat="server" CssClass="TextBox" TabIndex="19"
ReadOnly="false" Width="120px"></asp:TextBox>
</td>
</tr>
<tr>
<td class="Label">
    Scan Copy of Invoice :</td>
<td colspan="2">
            <asp:UpdatePanel ID ="updatePanel7" runat ="server" >
            <Triggers ><asp:PostBackTrigger ControlID="lnkCompany" /></Triggers>
            <ContentTemplate >
            <asp:FileUpload ID="CompanyLogoUpload" CssClass ="TextBox" runat="server" BorderStyle="None" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="CompanyLogoUpload"
            Display="None" ErrorMessage="Upload Image File only" SetFocusOnError="True" ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G)|(b|B)(m|M)(p|P))$"	
            ValidationGroup="Add"></asp:RegularExpressionValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RegularExpressionValidator6"
            WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender> 
            <asp:LinkButton ID="lnkCompany" runat="server" ValidationGroup="AddGridComp" onclick="lnkCompany_Click" >Upload</asp:LinkButton> 
            <asp:LinkButton ID="lnkCompanyCancel" runat="server" onclick="lnkCompanyCancel_Click">Cancel</asp:LinkButton>                                                                  
            </ContentTemplate> 
            </asp:UpdatePanel> 
            </td>
<td>
<asp:Label ID="lblLogopath" runat="server" Text="Label" Visible="false"></asp:Label>
<asp:Image ID="ImgCompanyLogo" runat="server" Height="40px" Width="50px" />
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</fieldset>
</td>
</tr>

<tr>
<td colspan="5">
<fieldset id="Fieldset2" class="FieldSet" runat="server" width="100%">
<legend id="Legend3" class="legend" runat="server" tabindex="20">  Material Request</legend>
<div id="divGridDetails" class="ScrollableDiv_FixHeightWidth4">
<table width="100%">
<tr>
<td>
<asp:GridView ID="GrdInwardPO" runat="server" 
AutoGenerateColumns="False" BackColor="White" BorderColor="#0CCCCC" 
BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="mGrid" 
DataKeyNames="#" ForeColor="Black" onrowdatabound="GrdInwardPO_RowDataBound" 
Width="100%" onrowcommand="GrdInwardPO_RowCommand" 
onrowdeleting="GrdInwardPO_RowDeleting">
<Columns>
<asp:TemplateField HeaderText="#" Visible="False">
</asp:TemplateField>


<asp:TemplateField>
<ItemTemplate>
<asp:ImageButton ID="ImgBtnDelete" runat="server" 
CommandArgument='<%# Eval("#") %>' 
CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />                                             
<ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
ConfirmText="Would You Like To Delete The Record..!" 
TargetControlID="ImgBtnDelete">
</ajax:ConfirmButtonExtender>



 <asp:ImageButton ID="ImageGridEdit" runat="server" 
 CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
 CommandName="Select" ImageUrl="~/Images/New Icon/ViewRecord.png" ToolTip="Show Last 10 Rate Of Items"  Visible="true"/>
                                
                                
                                
</ItemTemplate>
<FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<HeaderStyle Width="30px" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" 
Wrap="false" />
</asp:TemplateField>

<asp:BoundField DataField="ItemId" HeaderText="ItemId">
<HeaderStyle CssClass="Display_None" Wrap="False" />
<ItemStyle CssClass="Display_None" Wrap="False" />
</asp:BoundField>
<asp:BoundField DataField="CategoryName" HeaderText="Category">
<HeaderStyle Wrap="False" CssClass="Display_None"/>
<ItemStyle Wrap="False" CssClass="Display_None"/>
<FooterStyle Wrap="False" CssClass="Display_None"/>
</asp:BoundField>
<asp:BoundField DataField="SubCategory" HeaderText="SubCategory">
<HeaderStyle Wrap="False" CssClass="Display_None"/>
<ItemStyle Wrap="False" CssClass="Display_None"/>
<FooterStyle Wrap="False" CssClass="Display_None"/>
</asp:BoundField>
<asp:BoundField DataField="ItemCode" HeaderText="ItemCode">
<HeaderStyle Wrap="False" CssClass="Display_None"/>
<ItemStyle Wrap="False" CssClass="Display_None"/>
<FooterStyle Wrap="False" CssClass="Display_None"/>
</asp:BoundField>
<asp:BoundField DataField="Item" HeaderText="Item">
<HeaderStyle Wrap="False" />
<ItemStyle Wrap="False" />
</asp:BoundField>
<asp:BoundField DataField="ItemDesc" HeaderText="Description">
<HeaderStyle Wrap="False" />
<ItemStyle Wrap="False" />
</asp:BoundField>

<asp:BoundField DataField="OrderQty" HeaderText="Ord.Qty">
<HeaderStyle Wrap="False" />
<ItemStyle Wrap="False" />
</asp:BoundField>
<asp:TemplateField HeaderText="EDITSABLElblOrderQty" >
<ItemTemplate>
<asp:TextBox ID="LblOrder" runat="server" CssClass="TextBox" 
Text='<%# Bind("OrderQty") %>' ></asp:TextBox>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"
    Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="Display_None"
    Wrap="False"  />

</asp:TemplateField>
<asp:TemplateField HeaderText="Inw.Qty">
<ItemTemplate>

    
    <asp:TextBox ID="GrdtxtInwardQty" runat="server" AutoPostBack="false" onkeyup="CalculateNetAmtForGrid(this);" 
        CssClass="TextBox" MaxLength="10" 
        Text='<%# Bind("InwardQty") %>' TextMode="SingleLine" Width="50px" Height="20px"></asp:TextBox>


    <ajax:ComboBox ID="ddlUnit" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
    AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
    ItemInsertLocation="Append" Width="100px" CssClass="CustomComboBoxStyle" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
    </ajax:ComboBox>  

</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" 
    Wrap="False"  />
</asp:TemplateField>


<asp:TemplateField HeaderText="Actual.Qty">
<ItemTemplate>
    <asp:TextBox ID="GrdtxtActualQty" runat="server" Enabled="false"
        CssClass="TextBoxGrid" TextMode="SingleLine" Width="50px"></asp:TextBox>
      
          
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" CssClass="Display_None" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" CssClass="Display_None"/>
    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" CssClass="Display_None"/>
</asp:TemplateField>


<asp:TemplateField HeaderText="Pen.Qty">
<ItemTemplate>
          
          <asp:TextBox ID="GrdtxtPendingQty" runat="server" CssClass="TextBoxGrid" Enabled="false" ToolTip='<%# Eval("PendingQty") %>'
        Text='<%# Eval("PendingQty") %>'  Width="50px" ></asp:TextBox>
        
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Inw.Rate">
<ItemTemplate>
    <asp:TextBox ID="GrdtxtInwardRate" runat="server" AutoPostBack="false" onkeyup="CalculateNetAmtForGrid(this);" 
        CssClass="TextBoxGrid" MaxLength="10" Enabled="false"
        Text='<%# Bind("InwardRate") %>' TextMode="SingleLine" Width="50px"></asp:TextBox>
        <%--ontextchanged="GrdtxtInwardRate_TextChanged" --%>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  
    Wrap="False" />
</asp:TemplateField>
<asp:BoundField DataField="PORate" HeaderText="PORate1">
<HeaderStyle Wrap="False" CssClass="Display_None"/>
<ItemStyle Wrap="False" CssClass="Display_None"/>
<FooterStyle Wrap="False" CssClass="Display_None"/>
</asp:BoundField>
<asp:TemplateField HeaderText="PORate" Visible="true">
<ItemTemplate>
   
    
     <asp:TextBox ID="lblPORate" runat="server" CssClass="TextBoxGrid" Enabled="false"  Width="50px"
        Text='<%# Eval("PORate") %>' ></asp:TextBox>    
        
</ItemTemplate>

<FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
<HeaderStyle Width="30px" CssClass="Display_None"/>
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Wrap="false" CssClass="Display_None"/>

</asp:TemplateField>
<asp:TemplateField HeaderText="Difference">
<ItemTemplate>

         <asp:TextBox ID="GrdtxtDifference" runat="server" CssClass="TextBoxGrid" Enabled="false"  Width="50px"
        Text='<%# Eval("Diff") %>' ></asp:TextBox>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"
    Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"
    Wrap="False" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Amount">
<ItemTemplate>
           <asp:TextBox ID="GrdtxtAmount" runat="server" CssClass="TextBoxGrid" Enabled="false" Width="90px"
        Text='<%# Eval("Amount") %>' ></asp:TextBox>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Tax(%)">
<ItemTemplate>
    <asp:TextBox ID="GrdtxtTax" runat="server" AutoPostBack="false" onkeyup="CalculateNetAmtForGrid(this);" 
        CssClass="TextBoxGrid" MaxLength="10" Enabled="false"
        Text='<%# Bind("TaxPer") %>' TextMode="SingleLine" Width="50px"></asp:TextBox>
        <%--ontextchanged="GrdtxtTax_TextChanged" --%>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
</asp:TemplateField>
<asp:TemplateField HeaderText="TaxAmount">
<ItemTemplate>
  
        
          <asp:TextBox ID="GrdtxtTaxAmnt" runat="server" CssClass="TextBoxGrid" Enabled="false"  Width="50px"
        Text='<%# Eval("TaxAmount") %>' ></asp:TextBox>
        
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Disc(%)">
<ItemTemplate>
    <asp:TextBox ID="GrdtxtDiscPer" runat="server" AutoPostBack="false" onkeyup="CalculateNetAmtForGrid(this);" 
        CssClass="TextBoxGrid" MaxLength="10" Text='<%# Bind("DiscPer") %>' 
        TextMode="SingleLine" Width="50px" Enabled="false"
        ></asp:TextBox>
       
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Disc(Rs)">
<ItemTemplate>
    <asp:TextBox ID="GrdtxtDiscAmt" runat="server"  Enabled="false" 
        CssClass="TextBoxGrid" MaxLength="10" Text='<%# Bind("DiscAmt") %>' 
        TextMode="SingleLine" Width="50px" ></asp:TextBox>
    
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
</asp:TemplateField>
<asp:TemplateField HeaderText="NetAmt">
<ItemTemplate>
  
         <asp:TextBox ID="GrdtxtNetAmnt" runat="server" CssClass="TextBox" Enabled="false" Width="90px"
        Text='<%# Eval("NetAmount") %>' ></asp:TextBox>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
    Wrap="False" />
</asp:TemplateField>
<asp:TemplateField HeaderText="ExpDelDate">
<ItemTemplate>
    <asp:TextBox ID="txtExpDelDate" runat="server" CssClass="TextBox" 
        Text='<%# Bind("ExpDelDate") %>' ></asp:TextBox>
    <asp:ImageButton ID="ImageExpDate" runat="server" CausesValidation="false" 
        CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
    <ajax:CalendarExtender ID="CalendarExtender4" runat="server" 
        Format="dd-MMM-yyyy" PopupButtonID="ImageExpDate" 
        TargetControlID="txtExpDelDate">
    </ajax:CalendarExtender>
</ItemTemplate>
<HeaderStyle Wrap="False" CssClass="Display_None" />
<ItemStyle Wrap="False" CssClass="Display_None" />
</asp:TemplateField>
<asp:TemplateField HeaderText="ActDelDate">
<ItemTemplate>
    <asp:TextBox ID="txtActDelDate" runat="server" CssClass="TextBox" ReadOnly="true"
        Text='<%# Bind("ActDelDate") %>' ></asp:TextBox>
    <asp:ImageButton ID="ImageActDate" runat="server" CausesValidation="false" Enabled="false" 
        CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" 
        Format="dd-MMM-yyyy" PopupButtonID="ImageActDate" 
        TargetControlID="txtActDelDate">
    </ajax:CalendarExtender>
</ItemTemplate>
<HeaderStyle CssClass="Display_None" Wrap="False" />
<ItemStyle CssClass="Display_None" Wrap="False" />
</asp:TemplateField>
<asp:BoundField DataField="UnitId" HeaderText="UnitId" >
<HeaderStyle CssClass="Display_None" Wrap="True" />
<ItemStyle CssClass="Display_None" />
</asp:BoundField>
<asp:BoundField DataField="SuplierId" HeaderText="SuplierId">
<HeaderStyle CssClass="Display_None" Wrap="False" />
<ItemStyle CssClass="Display_None" Wrap="False" />
</asp:BoundField>


<asp:TemplateField HeaderText="Location">
<ItemTemplate>
<asp:DropDownList ID="ddlLocation" runat="server" CssClass="ComboBox" AutoPostBack="false"  onkeyup="javascript:ValueOfDDL(this);"
Width="150px" OnBlur="javascript:FocusToDDL();" >
</asp:DropDownList>

<asp:RequiredFieldValidator ID="RFV21" runat="server" 
ControlToValidate="ddlLocation" Display="None" 
ErrorMessage="Please Select Location To Transfer" InitialValue="0" 
ValidationGroup="Add"></asp:RequiredFieldValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" 
TargetControlID="RFV21" WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender> 
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
</asp:TemplateField>
                            
<asp:TemplateField HeaderText="LocationId">
<ItemTemplate>

   <asp:TextBox ID="lblLocID" runat="server" CssClass="TextBoxGrid" Enabled="true"
        Text='<%# Eval("LocID") %>' ></asp:TextBox>
</ItemTemplate>
<HeaderStyle CssClass="Display_None" />
<ItemStyle CssClass="Display_None" />
</asp:TemplateField>

<asp:TemplateField HeaderText="PRDQTY">
<ItemTemplate>
          
          <asp:TextBox ID="GRDORDQTYL" runat="server" CssClass="TextBoxGrid" Enabled="false" 
        Text='<%# Eval("OrderQty") %>'  Width="50px" ></asp:TextBox>
        
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="Display_None"
    Wrap="False" />
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="Display_None"
    Wrap="False" />
</asp:TemplateField>


<asp:TemplateField HeaderText="TaxAmount">
<ItemTemplate>
        <asp:TextBox ID="GrdtxtUnitOrdQty" runat="server" CssClass="TextBoxGrid" Enabled="false"  Width="50px"
        Text='<%# Eval("PENDINGORDERQTY") %>' ></asp:TextBox>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
Wrap="False" CssClass="Display_None"/>
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
Wrap="False" CssClass="Display_None"/>
</asp:TemplateField>



</Columns>
</asp:GridView>
</td>
</tr>
   
<tr id="TR_RateDtls" runat="server">
              <td align="left">
                  <fieldset ID="Fieldset4" runat="server" class="FieldSet" style="width: 99%">
                      <legend ID="Legend1" runat="server" class="legend" 
                          onclick="return Legend1_onclick()">Last 10 Purchase Rates</legend>
                            <table width="100%">
                              <tr>
                                  <td>
                      <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true">
                          <ContentTemplate>
                              <asp:GridView ID="GrdRateDtls" runat="server" AutoGenerateColumns="False" 
                                  CssClass="mGrid">
                                  <Columns>
                                   
                                       <asp:BoundField DataField="BillDate" HeaderText="Bill Date">
                                          <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>  
                                      <asp:BoundField DataField="InwardNo" HeaderText="Inward No">
                                          <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>  
                                       <asp:BoundField DataField="SuplierName" HeaderText="Supplier Name">
                                          <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="InwardRate" HeaderText="Rate">
                                          <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                                                          
                                  </Columns>
                              </asp:GridView>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                      </td>
                      </tr>
                      
                              <tr>
                                  <td align="left">
                                      <asp:LinkButton ID="hyl_Hide" runat="server" CssClass="linkButton" onclick="hyl_Hide_Click" 
                                         >Hide</asp:LinkButton>
                                  </td>
                              </tr>
                      </table>
                      
                  </fieldset>
              </td>
          </tr>
</table>



</div>
</fieldset>

</td>
</tr>

<tr>
<td align="left" colspan="2">
<table width="100%">


<tr id="Tr1" runat="server">

<td class="Label"   align="right">Sub Total :</td><td class="Label" align="right">
<asp:TextBox ID="txtSubTotal" runat="server" CssClass="TextBox" onkeyup="CalPercentage_Amount(this);"  Width="128px" style="text-align:right" TabIndex="19"></asp:TextBox>
    Rs/-</td>

<td class="Label"  align="right">Discount :</td><td class="Label" align="right">
<asp:TextBox ID="txtDiscountPer" onkeyup="CalPercentage_Amount(this);" runat="server" TabIndex="20"
CssClass="Display_None" Width="38px" style="text-align:right"></asp:TextBox>
    
<asp:TextBox ID="txtDiscount"  onkeyup="CalPercentage_Amount(this);"  OnChange="CalPercentage_Amount(this);"  runat="server" TabIndex="21"
CssClass="TextBox" Width="70px" style="text-align:right" AutoPostBack="true" ></asp:TextBox>
    Rs/-</td>

<td class="Label"  align="right">VAT :</td><td class="Label" align="right">
<asp:TextBox ID="txtVATPer" runat="server" onkeyup="CalPercentage_Amount(this);"  TabIndex="22"
CssClass="Display_None" Width="38px" style="text-align:right"></asp:TextBox>
    
<asp:TextBox ID="txtVATAmount" runat="server"  onkeyup="CalPercentage_Amount(this);" OnChange="CalPercentage_Amount(this);"  TabIndex="23"
CssClass="TextBox" Width="70px" style="text-align:right" AutoPostBack="true"  ></asp:TextBox>
    Rs/-</td>

</tr>

<tr id="Tr4" runat="server">

<td class="Label" align="right">Dekhrekh :</td><td class="Label" align="right">
<asp:TextBox ID="txtDekhrekhPer" runat="server" onkeyup="CalPercentage_Amount(this);" 
CssClass="Display_None" Width="38px" style="text-align:right" ReadOnly="true"></asp:TextBox>

<asp:TextBox ID="txtDekhrekhAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="24"
CssClass="TextBox" Width="70px" style="text-align:right"></asp:TextBox>
    Rs/-</td>

<td class="Label" align="right">Hamali :</td><td class="Label" align="right">
<asp:TextBox ID="txtHamaliPer" runat="server" onkeyup="CalPercentage_Amount(this);" 
CssClass="Display_None" Width="38px" style="text-align:right" ReadOnly="true"></asp:TextBox>

<asp:TextBox ID="txtHamaliAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="25"
CssClass="TextBox" Width="70px" style="text-align:right"></asp:TextBox>
    Rs/-</td>

<td class="Label"  align="right">Cess :</td><td class="Label" align="right">
<asp:TextBox ID="txtCESSPer" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="26"
CssClass="TextBox" Width="38px" style="text-align:right"></asp:TextBox>
    %
<asp:TextBox ID="txtCESSAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="27"
CssClass="TextBox" Width="70px" style="text-align:right"></asp:TextBox>
    Rs/-</td>
</tr>

<tr id="Tr7" runat="server">

<td class="Label"  align="right">Freight :</td><td class="Label" align="right">
<asp:TextBox ID="txtFreightPer" runat="server" onkeyup="CalPercentage_Amount(this);" 
CssClass="Display_None" Width="38px" style="text-align:right" ReadOnly="true"></asp:TextBox>

<asp:TextBox ID="txtFreightAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="28"
CssClass="TextBox" Width="70px" style="text-align:right"></asp:TextBox>
    Rs/-</td>

<td class="Label"  align="right">Packing :</td><td class="Label" align="right">
<asp:TextBox ID="txtPackingPer" runat="server" onkeyup="CalPercentage_Amount(this);" 
CssClass="Display_None" Width="38px" style="text-align:right" ReadOnly="false"></asp:TextBox>

<asp:TextBox ID="txtPackingAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="29"
CssClass="TextBox" Width="70px" style="text-align:right"></asp:TextBox>
    Rs/-</td>

<td class="Label"  align="right">Postage :</td><td class="Label" align="right">
<asp:TextBox ID="txtPostagePer" runat="server" onkeyup="CalPercentage_Amount(this);" 
CssClass="Display_None" Width="38px" style="text-align:right" ReadOnly="false"></asp:TextBox>

<asp:TextBox ID="txtPostageAmt" runat="server" onkeyup="CalPercentage_Amount(this);" TabIndex="30"
CssClass="TextBox" Width="70px" style="text-align:right" AutoPostBack="false"></asp:TextBox>
    Rs/-</td>

</tr>

<tr >
<td class="Label">

    &nbsp;</td>
<td class="Label">

    &nbsp;</td>
<td class="Label"  align="right">Other Charges :</td><td class="Label" align="right">
<asp:TextBox ID="txtOtherCharges" runat="server" CssClass="TextBox" Width="128px" style="text-align:right" TabIndex="31"
onkeyup="CalPercentage_Amount(this);"></asp:TextBox>
    Rs/-</td>

<td align="left" class="Label">
    Grand Total :</td><td class="Label" align="right">
<asp:TextBox ID="txtNetTotal" runat="server" CssClass="TextBox" Width="128px" style="text-align:right" TabIndex="32"></asp:TextBox>
        Rs/-</td>

</tr>

</table>
</td>
</tr>
<tr>
<td class="Label">
Vehicle No. :

</td>
<td>
    <asp:TextBox ID="txtVehicle" runat="server" CssClass="TextBox"></asp:TextBox>
</td>

</tr>
<tr>


<td class="Label"> Time In :</td>
<td>
  <MKB:TimeSelector ID="TimeInSelector" runat="server" 
                                 SelectedTimeFormat="Twelve" TabIndex="30">
                            </MKB:TimeSelector>
  
    </td>

    
</tr>
<tr>
<td class="Label">
 Time Out :</td>
 <td>
   <MKB:TimeSelector ID="TimeOutSelector" runat="server" 
        SelectedTimeFormat="Twelve" TabIndex="30">
    </MKB:TimeSelector>
 </td>
</tr>

    <caption>
       
        <tr>
            <td class="Label">
                Note :
            </td>
            <td align="left">
                <asp:TextBox ID="txtInstruction" runat="server" CssClass="TextBox" 
                    Height="25px" TabIndex="33" TextMode="MultiLine" Width="890px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <table align="center" width="25%">
                    <tr>
                        <td>
                            <asp:Button ID="BtnUpdate" runat="server" AccessKey="u" 
                                CausesValidation="false" CssClass="button" onclick="BtnUpdate_Click" 
                                TabIndex="34" Text="Update" />
                            <ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" 
                                ConfirmText="Would You Like To Update The Record ?" TargetControlID="BtnUpdate">
                            </ajax:ConfirmButtonExtender>
                        </td>
                        <td>
                            <asp:Button ID="BtnSave" runat="server" AccessKey="s" CssClass="button" 
                                onclick="BtnSave_Click" TabIndex="35" Text="Save" ValidationGroup="Add" />
                        </td>
                        <td>
                            <asp:Button ID="BtnCancel" runat="server" AccessKey="c" 
                                CausesValidation="False" CssClass="button" onclick="BtnCancel_Click" 
                                TabIndex="36" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <div ID="Div5" runat="server" class="scrollableDiv">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:GridView ID="ReportGrid" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="#" 
                                onpageindexchanging="ReportGrid_PageIndexChanging" 
                                onrowcommand="ReportGrid_RowCommand" onrowdatabound="ReportGrid_RowDataBound" 
                                onrowdeleting="ReportGrid_RowDeleting" TabIndex="37">
                                <Columns>
                                    <asp:TemplateField HeaderText="#" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="InwardId" runat="server" Text='<%# Eval("#") %>' Width="15px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageGridEdit" runat="server" 
                                                CommandArgument='<%# Eval("#") %>' CommandName="Select" 
                                                ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                            <asp:ImageButton ID="ImgBtnDelete" runat="server" 
                                                CommandArgument='<%# Eval("#") %>' CommandName="Delete" 
                                                ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                                ConfirmText="Would You Like To Delete The Record..!" 
                                                TargetControlID="ImgBtnDelete">
                                            </ajax:ConfirmButtonExtender>
                                            <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&amp;Flag=<%="IN"%>&amp;PDFFlag=<%="NOPDF"%>' 
                                                target="_blank">
                                            <asp:Image ID="ImgBtnPrint" runat="server" 
                                                ImageUrl="~/Images/Icon/GridPrint.png" TabIndex="37" 
                                                ToolTip="Print Inward Details" />
                                            </a>
                                            <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&amp;Flag=<%="IN"%>&amp;PDFFlag=<%="PDF"%>' 
                                                target="_blank">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png" 
                                                TabIndex="29" ToolTip="PDF Inward Details" />
                                            </a>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle Width="20px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" 
                                            Wrap="false" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SuplierName" HeaderText="SuplierName">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InwardNo" HeaderText="Inward No">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InwardDate" HeaderText="Inward Date">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Amount" HeaderText="Amount">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InwardId" HeaderText="Inward Id">
                                        <HeaderStyle CssClass="Display_None" />
                                        <ItemStyle CssClass="Display_None" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SuplierId" HeaderText="Suplier Id">
                                        <HeaderStyle CssClass="Display_None" />
                                        <ItemStyle CssClass="Display_None" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="POId" HeaderText="POId">
                                        <HeaderStyle CssClass="Display_None" Wrap="False" />
                                        <ItemStyle CssClass="Display_None" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Type" HeaderText="Inward Type">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </caption>


</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

