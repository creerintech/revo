<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialComsumption.aspx.cs" Inherits="Transactions_MaterialComsumption" Title="Material Consumption Register" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">

<script language="javascript" type="text/javascript">

function ClickDoneBtn40()
{
var key = window.event.keyCode;
if (key == 9)
{
document.getElementById("txtDate").focus();
}
else
{
}
}

function FocusToDDLSupplier()
{
document.getElementById('<%= ddlIssueNo.ClientID %>').focus();
}



function CalculateNetAmtForGrid(objGrid)
{
var _GridDetails = document.getElementById('<%= GridIssueDetails.ClientID %>');  
var rowIndex=objGrid.offsetParent.parentNode.rowIndex;
var ISSUEQTY=(_GridDetails.rows[rowIndex].cells[7].children[0]);
var RATE=(_GridDetails.rows[rowIndex].cells[8].children[0]);
var AMOUNT=(_GridDetails.rows[rowIndex].cells[9].children[0]);
var CONSQTY=(_GridDetails.rows[rowIndex].cells[10].children[0]);
var PENQTY=(_GridDetails.rows[rowIndex].cells[12].children[0]);
var PENAMT=(_GridDetails.rows[rowIndex].cells[13].children[0]);
var LOCATIONID=(_GridDetails.rows[rowIndex].cells[14].children[0]);
var LOCATION=(_GridDetails.rows[rowIndex].cells[15].children[0]);
var CONSAMT=(_GridDetails.rows[rowIndex].cells[11].children[0]);
var total=0;
if (ISSUEQTY.value=="" || isNaN(ISSUEQTY.value))
{
ISSUEQTY.value=0;           
}

if (RATE.value=="" || isNaN(RATE.value))
{
RATE.value=0;           
}

if (AMOUNT.value=="" || isNaN(AMOUNT.value))
{
AMOUNT.value=0;           
}

if (CONSQTY.value=="" || isNaN(CONSQTY.value))
{
CONSQTY.value=0;           
}

if (PENQTY.value=="" || isNaN(PENQTY.value))
{
PENQTY.value=0;           
}

if (PENAMT.value=="" || isNaN(PENAMT.value))
{
PENAMT.value=0;           
}

if (parseFloat(ISSUEQTY.value) < parseFloat(CONSQTY.value)) {
    alert("Consumption Quantity Not Greater Than Issue Quantity");
    CONSQTY.value = "0";
    CONSQTY.focus();
}



if(parseFloat(ISSUEQTY.value)==0)
{
PENQTY.value=0;
}
else
{
PENQTY.value=(parseFloat(ISSUEQTY.value)-parseFloat(CONSQTY.value));
if(parseFloat(PENQTY.value)<0)
{
PENQTY.value=0;
}
}
PENAMT.value=(parseFloat(PENQTY.value)*parseFloat(RATE.value));
CONSAMT.value=(parseFloat(CONSQTY.value)*parseFloat(RATE.value));

}    
</script>


<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" >
<ProgressTemplate>            
<div id="progressBackgroundFilter"></div>
<div id="processMessage">   
<center><span class="SubTitle">Loading....!!! </span></center>
<asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px"  />                                
</div>
</ProgressTemplate>
</asp:UpdateProgress>
Search for Consumption Record : 
<asp:TextBox ID="TxtSearch" runat="server" 
CssClass="search" ToolTip="Enter The Text"
Width="292px" AutoPostBack="True"  ontextchanged="TxtSearch_TextChanged" ></asp:TextBox>
<div id="divwidth"></div>
<ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
TargetControlID="TxtSearch"    CompletionInterval="100"                               
UseContextKey="True" FirstRowSelected ="True" 
ShowOnlyCurrentWordInCompletionListItem="True"  
ServiceMethod="GetCompletionList"
CompletionListCssClass="AutoExtender"
CompletionListItemCssClass="AutoExtenderList"
CompletionListHighlightedItemCssClass="AutoExtenderHighlight" DelimiterCharacters="" Enabled="True" ServicePath="" ></ajax:AutoCompleteExtender> 
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
Material Consumption Register
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<table width="100%">
<tr>
<td>
<fieldset class="FieldSet" id="FieldSet4" runat="server">
<table width="100%" cellspacing="6" id="T1">
<tr>
<td class="Label">
Consumption No :</td>
<td>
<asp:Label ID="TxtConsumptionNo" runat="server" CssClass="Label" Font-Bold="true"></asp:Label>
</td>
<td class="Label">
Date :
</td>                
<td>
<asp:TextBox ID="TxtDate" runat="server" Width="100px" CssClass="TextBox"></asp:TextBox>                
<asp:ImageButton ID="ImageDate" runat="server" CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />                
<ajax:CalendarExtender ID="CalDate" runat="server" Format="dd-MMM-yyyy" TargetControlID="TxtDate" PopupButtonID="ImageDate">
</ajax:CalendarExtender>
</td>
</tr>                

<tr id="TrReq" runat="server">
<td class="Label">From Date : </td>
<td style="width: 373px"> 
<asp:TextBox ID="TxtFromDateR" runat="server" CssClass="TextBox" Width="80px" ></asp:TextBox>
<ajax:CalendarExtender ID="TxtFromDateR_CalendarExtender" runat="server" 
Format="dd-MMM-yyyy" PopupButtonID="ImageFromDate" 
TargetControlID="TxtFromDateR">
</ajax:CalendarExtender>
<asp:ImageButton ID="ImageFromDate0" runat="server" CausesValidation="false" 
CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
&nbsp;&nbsp;&nbsp;&nbsp; To Date :
<asp:TextBox ID="TxtToDateR" runat="server" CssClass="TextBox" Width="80px"></asp:TextBox>
<ajax:CalendarExtender ID="TxtToDateR_CalendarExtender" runat="server" 
Format="dd-MMM-yyyy" PopupButtonID="ImageToDate" TargetControlID="TxtToDateR">
</ajax:CalendarExtender>
<asp:ImageButton ID="ImageToDate0" runat="server" CausesValidation="false" 
CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:ImageButton ID="IMREFRESH" runat="server" CausesValidation="false" onclick="IMREFRESH_Click"
CssClass="Imagebutton" ImageUrl="~/Images/Icon/GridUpdate.png" ToolTip="Get Issue Of This Date"/>

</td>
<td class="Label">Issue No:</td>
<td>
<%--<asp:DropDownList ID="ddlIssueNo" runat="server" CssClass="ComboBox" 
Width="250px">
</asp:DropDownList>--%>

          
<ajax:ComboBox ID="ddlIssueNo" runat="server" DropDownStyle="DropDown" 
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle" >
</ajax:ComboBox>

<asp:RequiredFieldValidator ID="RFV3" runat="server" 
ControlToValidate="ddlIssueNo" Display="None" 
ErrorMessage="Please Select Item" InitialValue="0" 
ValidationGroup="AddGridItem"></asp:RequiredFieldValidator>
<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" 
Enabled="True" TargetControlID="RFV3" 
WarningIconImageUrl="~/Images/Icon/Warning.png">
</ajax:ValidatorCalloutExtender>

<asp:Label ID="LBLISSUENO" runat="server" CssClass="Label_Dynamic"></asp:Label>
&nbsp;&nbsp;&nbsp;
<asp:Button ID="BtnShowIssue" runat="server" CssClass="button" Text="Show" 
ToolTip="Show Items Of Issue Register" ValidationGroup="AddGrid" onclick="BtnShowIssue_Click" /> 

&nbsp;</td>
</tr>
<tr runat="server" id="TRGRIDINWARD">
<td colspan="4">                
<div class="ScrollableDiv_FixHeightWidth5">                 
<asp:GridView ID="GridIssueDetails" runat="server" AutoGenerateColumns="False" CssClass="mGrid" >
    <Columns>
        <asp:TemplateField HeaderText="Sr.No">
        <ItemTemplate>
        <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'>
        </asp:Label>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" Width="6%" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="#">
            <ItemTemplate>
                <asp:Label ID="LblEntryId" runat="server" Text='<%#Eval("#") %>' width="30px">
                </asp:Label>
            </ItemTemplate>
            <HeaderStyle CssClass="Display_None" />
            <ItemStyle CssClass="Display_None" />
        </asp:TemplateField>  
        
        <asp:BoundField DataField="ItemId" HeaderText="ItemId">
            <HeaderStyle Wrap="false" CssClass="Display_None"/>
            <ItemStyle Wrap="false" CssClass="Display_None"/>
        </asp:BoundField>
        <asp:BoundField DataField="Item" HeaderText="Item">
            <HeaderStyle Wrap="false" />
            <ItemStyle Wrap="false" />
        </asp:BoundField>
                    <asp:BoundField DataField="ItemDetailsId" HeaderText="ItemDetailsId">
                    <HeaderStyle Wrap="False" CssClass="Display_None"/>
                    <ItemStyle Wrap="False" CssClass="Display_None"/>
                    </asp:BoundField>
           
                     <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Unit" HeaderText="Unit">
            <HeaderStyle Wrap="false" />
            <ItemStyle Wrap="false" />
        </asp:BoundField>
        
         <asp:TemplateField HeaderText="Issue Qty">
            <ItemTemplate>
                <asp:TextBox ID="txtGrdIssue" runat="server" Text='<%#Eval("Issue") %>' 
                CssClass="TextBoxGrid" Enabled="false" TabIndex="4" Width="80px"></asp:TextBox>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50px" Wrap="false"/>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Wrap="false"/>
        </asp:TemplateField>
        
         <asp:TemplateField HeaderText="Rate">
            <ItemTemplate>
                <asp:TextBox ID="txtGrdRate" runat="server" Text='<%#Eval("Rate") %>' 
                CssClass="TextBoxGrid" Enabled="false" TabIndex="4" Width="80px"></asp:TextBox>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50px" Wrap="false"/>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Wrap="false"/>
        </asp:TemplateField>
        
     
          <asp:TemplateField HeaderText="Amount">
            <ItemTemplate>
                <asp:TextBox ID="txtGrdAmount" runat="server" Text='<%#Eval("Amount") %>' 
                CssClass="TextBoxGrid" Enabled="false" TabIndex="4" Width="80px"></asp:TextBox>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50px" Wrap="false"/>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Wrap="false"/>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Consumption Qty">
            <ItemTemplate>
                <asp:TextBox ID="txtGrdConsumption" runat="server" Text='<%#Eval("Consumption") %>'  onkeyup="CalculateNetAmtForGrid(this);"
                CssClass="TextBox" AutoPostBack="false" TabIndex="4" Width="80px" MaxLength="8"></asp:TextBox>
                 <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtGrdConsumption"
                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50px" Wrap="false"/>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Wrap="false"/>
        </asp:TemplateField>
        
           <asp:TemplateField HeaderText="Consumption Amount">
            <ItemTemplate>
                <asp:TextBox ID="txtGrdConsumptionAmt" runat="server" Text='<%#Eval("ConsumptionAmt") %>' 
                CssClass="TextBoxGrid" Enabled="false" TabIndex="4" Width="80px"></asp:TextBox>
               
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50px" Wrap="false"/>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Wrap="false"/>
        </asp:TemplateField>
        
           <asp:TemplateField HeaderText="Pending Qty">
            <ItemTemplate>
                <asp:TextBox ID="txtGrdPending" runat="server" Text='<%#Eval("Pending") %>' 
                CssClass="TextBoxGrid" Enabled="false" TabIndex="4" Width="80px"></asp:TextBox>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50px" Wrap="false"/>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Wrap="false"/>
        </asp:TemplateField>
        
            <asp:TemplateField HeaderText="Pending Amount">
            <ItemTemplate>
                <asp:TextBox ID="txtGrdPendingAmount" runat="server" Text='<%#Eval("PendingAmount") %>' 
                CssClass="TextBoxGrid" Enabled="false" TabIndex="4" Width="80px"></asp:TextBox>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50px" Wrap="false"/>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" Wrap="false"/>
        </asp:TemplateField>
       
        <asp:TemplateField HeaderText="LocationId">
         <ItemTemplate>
          <asp:TextBox ID="txtGrdLocationID" runat="server" Text='<%#Eval("LocID") %>' 
                CssClass="TextBoxGrid" Enabled="false" TabIndex="4" Width="100px"></asp:TextBox>
         </ItemTemplate>
             <HeaderStyle CssClass="Display_None" />
             <ItemStyle CssClass="Display_None" />
       </asp:TemplateField>
       
        <asp:TemplateField HeaderText="Location">
            <ItemTemplate>
                <asp:TextBox ID="txtGrdLocation" runat="server" Text='<%#Eval("Location") %>' 
                CssClass="TextBox" Enabled="false" TabIndex="4" Width="100px"></asp:TextBox>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50px" Wrap="false"/>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Wrap="false"/>
        </asp:TemplateField>
        
          <asp:BoundField DataField="UnitId" HeaderText="UnitID">
            <HeaderStyle Wrap="false" CssClass="Display_None"/>
            <ItemStyle Wrap="false" CssClass="Display_None"/>
        </asp:BoundField>
        
    </Columns>
</asp:GridView>
</div> 
</td>
</tr>
</table>

</fieldset>


</td>
</tr>

<tr>
<td>

<fieldset class="FieldSet" id="FieldSet1" runat="server">
<table width="100%">
<tr>
<td colspan="3" align="center">
<table align="center" width="25%">
<tr>
<td>

<asp:Button ID="BtnUpdate" CssClass="button" runat="server" Text="Update" 
ValidationGroup="Add" onclick="BtnUpdate_Click" />

<ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
TargetControlID="BtnUpdate">

</ajax:ConfirmButtonExtender>
</td>           

<td>
<asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" TabIndex="5"
ValidationGroup="Add" Height="28px" onclick="BtnSave_Click"  />
</td>

<td>
<asp:Button ID="BtnCancel" CssClass="button" runat="server" Text="Cancel" 
CausesValidation="false" onclick="BtnCancel_Click"  />
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
     <asp:GridView ID="ReportGrid1" runat="server" AllowPaging="True" 
         AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="#"  onrowcommand="ReportGrid1_RowCommand" 
                             onrowdeleting="ReportGrid1_RowDeleting" 
                             onpageindexchanging="ReportGrid1_PageIndexChanging" >
         <Columns>
             <asp:TemplateField>
                 <ItemTemplate>
                     <asp:ImageButton ID="ImageGridEdit" runat="server" 
                         CommandArgument='<%# Eval("#") %>' CommandName="Select" 
                         ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                     <asp:ImageButton ID="ImgBtnDelete" runat="server" 
                     CommandArgument='<%# Eval("#")%>'
                     CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete"/>
                 <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                     ConfirmText="Would You Like To Delete The Record..!" 
                     TargetControlID="ImgBtnDelete">
                  </ajax:ConfirmButtonExtender>
                     <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="CS"%>&PDFFlag=<%="NOPDF"%>' 
                         target="_blank">
                     <asp:Image ID="ImgBtnPrint" runat="server" 
                         ImageUrl="~/Images/Icon/GridPrint.png" TabIndex="29" 
                         ToolTip="Print" />
                     </a>
                      <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="CS"%>&PDFFlag=<%="PDF"%>' target="_blank">
                         <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/New Icon/pdfImg.png"
                                ToolTip="PDF" TabIndex="29" />
                         </a>
                 </ItemTemplate>
                 <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                 <HeaderStyle Width="20px" />
                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="55px" 
                     Wrap="false" />
             </asp:TemplateField>
             <asp:BoundField DataField="Name" HeaderText="Consumption No.">
                 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
             </asp:BoundField>
               <asp:BoundField DataField="Date" HeaderText="Date">
                 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
             </asp:BoundField>
             
             <asp:BoundField DataField="Amount" HeaderText="Amount">
                 <HeaderStyle Wrap="False" />
                 <ItemStyle Wrap="False" />
             </asp:BoundField>
         </Columns>
     </asp:GridView>
 </ContentTemplate>
</asp:UpdatePanel>
</div>
</td>
</tr>
</table>
</fieldset>

</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

