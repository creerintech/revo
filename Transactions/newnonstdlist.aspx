<%@ Page Title="Non Std Master" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="newnonstdlist.aspx.cs" Inherits="Transactions_newnonstdlist" %>

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
   Non Std Master
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
                                        <td class="Label">Select Interlock :
                                        </td>

                                        <td align="left">
                                            <asp:DropDownList ID="drpproject" runat="server" Width="150px" CssClass="ComboBox" AutoPostBack="true"></asp:DropDownList>
                                        </td>

                                        <td class="Label">Tiltle :
                                        </td>

                                        <td align="left">
                                            <asp:TextBox ID="txttitle" runat="server" CssClass="TextBox"></asp:TextBox>
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
                                    <td colspan="3">
                                        <div id="DivDisp" runat="server" class="ScrollableDiv_FixHeightWidth4" width="90%">

                                            <asp:GridView ID="grditem" Visible="false" runat="server" CssClass="mGrid" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="grditem_RowDataBound" OnRowCommand="grditem_RowCommand">
                                                   <Columns>





                                                     


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
                                                                    <asp:TextBox ID="TxtItemName" runat="server" Text='<%# Eval("ItemName") %>' 
                                                                        CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemName_TextChanged"></asp:TextBox>
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
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />


                                                         <FooterTemplate>


                                                              <asp:Button ID="btndeleteitem" runat="server" Text="Delete Row"  BackColor="#00ffcc" ToolTip="Add  New  Row" Height="30px"
                        onclick="btndeleteitem_Click" />
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add  New  Row"  BackColor="#00ffcc" ToolTip="Add  New  Row" Height="30px"
                        onclick="ButtonAdd_Click1" />
                   
                     
                </FooterTemplate>
                                                    
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Valve Size ">
                    <ItemTemplate>
                          <asp:TextBox ID="txtvalvesize" runat="server"   CssClass="TextBox" ></asp:TextBox>
                                                        
                </ItemTemplate>

                </asp:TemplateField>


                                                          <asp:TemplateField HeaderText="Schedule ">
                                                           <ItemTemplate>
                                                               <asp:TextBox ID="txtSchedule" runat="server"  Text='<%# Eval("shecdule") %>' CssClass="TextBox" ></asp:TextBox>
                                                           </ItemTemplate>
                                                         


                                                       </asp:TemplateField>

                                                       <asp:TemplateField HeaderText="Pipe Length ">
                                                           <ItemTemplate>
                                                               <asp:TextBox ID="txtqty" runat="server" Text='<%# Eval("length") %>' CssClass="TextBox"></asp:TextBox>
                                                           </ItemTemplate>
                                                           
             
                                                       </asp:TemplateField>

                                                      

                                                         <asp:TemplateField HeaderText="UOM ">
                                                           <ItemTemplate>
                                                               <asp:TextBox ID="txtUOM" runat="server" Text='<%# Eval("uom") %>' CssClass="TextBox"></asp:TextBox>
                                                           </ItemTemplate>

                                                       </asp:TemplateField>




                                                       <asp:TemplateField HeaderText="Bracket Qty ">
                                                           <ItemTemplate>
                                                               <asp:TextBox ID="txtbqty" runat="server" Text='<%# Eval("qty") %>' CssClass="TextBox"></asp:TextBox>
                                                           </ItemTemplate>

                                                       </asp:TemplateField>
                                                    


                                                   </Columns>
                                               </asp:GridView>



                                           <asp:Label ID="lblforPipeBracket" runat="server" Text="For Pipe Bracket" Font-Bold="true" Font-Size="Larger" ForeColor="Red"></asp:Label>



                                             <asp:GridView ID="grdpipebracket" runat="server" CssClass="mGrid" AutoGenerateColumns="false" ShowFooter="true" OnDataBound="grdpipebracket_DataBound" OnRowCommand="grdpipebracket_RowCommand" OnRowDeleting="grdpipebracket_RowDeleting" >
                                                   <Columns>




                                                         <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageAccepted" runat="server" TabIndex="29" CommandArgument='<%# Eval("RowNumber") %>'
                                                                CommandName="Delete" ImageUrl="~/Images/New Icon/GridDelete.png" ToolTip="Delete " />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <ajax:ComboBox ID="ddlItempipebracket" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                Visible="false" OnSelectedIndexChanged="ddlItempipebracket_SelectedIndexChanged">
                                                            </ajax:ComboBox>
                                                            <%--THIS START HERE--%>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="TxtItemNamepipebracket" runat="server" Text='<%# Eval("ItemName") %>' 
                                                                        CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemNamepipebracket_TextChanged"></asp:TextBox>
                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemNamepipebracket" runat="server" TargetControlID="TxtItemNamepipebracket"
                                                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                        ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                    </ajax:AutoCompleteExtender>
                                                                    <ajax:TextBoxWatermarkExtender ID="TBWE2pipebracket" runat="server" TargetControlID="TxtItemNamepipebracket"
                                                                        WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <%--THIS END HERE--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />


                                                         <FooterTemplate>
                 <asp:Button ID="btnpipebracket" runat="server" Text="Add  New  Row"  BackColor="#00ffcc" ToolTip="Add  New  Row" Height="30px"
                        onclick="btnpipebracket_Click" />
                   
                     
                </FooterTemplate>
                                                    </asp:TemplateField>





                                                        <asp:TemplateField HeaderText="Pipe Size ">
                    <ItemTemplate>
                          <asp:TextBox ID="txtvalvesize" runat="server"   CssClass="TextBox" ></asp:TextBox>
                                                        
                </ItemTemplate>

                </asp:TemplateField>


                                                          <asp:TemplateField HeaderText="Schedule ">
                                                           <ItemTemplate>
                                                               <asp:TextBox ID="txtSchedule" runat="server"  Text='<%# Eval("shecdule") %>' CssClass="TextBox" ></asp:TextBox>
                                                           </ItemTemplate>
                                                         


                                                       </asp:TemplateField>

                                                      


                                                      
                                                    


                                                   </Columns>
                                               </asp:GridView>




                                                               <asp:Label ID="Label4" runat="server" Text=" For Plate, U-shape, L-shape  Bracket" Font-Bold="true" Font-Size="Larger" ForeColor="Red"></asp:Label>



                                            <asp:GridView ID="grdplate" runat="server" CssClass="mGrid" AutoGenerateColumns="false" ShowFooter="true" OnRowCommand="grdplate_RowCommand" OnRowDeleting="grdplate_RowDeleting">
                                                <Columns>




                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageAccepted" runat="server" TabIndex="29" CommandArgument='<%# Eval("RowNumber") %>'
                                                                CommandName="Delete" ImageUrl="~/Images/New Icon/GridDelete.png" ToolTip="Delete " />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <ajax:ComboBox ID="ddlItemplate" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                Visible="false" OnSelectedIndexChanged="ddlItemplate_SelectedIndexChanged">
                                                            </ajax:ComboBox>
                                                            <%--THIS START HERE--%>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="TxtItemNameplate" runat="server" Text='<%# Eval("ItemName") %>'
                                                                        CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemNameplate_TextChanged"></asp:TextBox>
                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemNameplate" runat="server" TargetControlID="TxtItemNameplate"
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


                                                        <FooterTemplate>
                                                            <asp:Button ID="btnplate" runat="server" Text="Add  New  Row" BackColor="#00ffcc" ToolTip="Add  New  Row" Height="30px"
                                                                OnClick="btnplate_Click" />


                                                        </FooterTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Thickness ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtthickenss" runat="server" Text='<%# Eval("Thickness") %>' CssClass="TextBox"></asp:TextBox>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                   



                                                </Columns>
                                            </asp:GridView>



                                              <asp:Label ID="Label5" runat="server" Text="For Adaptor" Font-Bold="true" Font-Size="Larger" ForeColor="Red"></asp:Label>

                                             <asp:GridView ID="grdadapter" runat="server" CssClass="mGrid" AutoGenerateColumns="false" ShowFooter="true" OnRowCommand="grdadapter_RowCommand" OnRowDeleting="grdadapter_RowDeleting">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageAccepted" runat="server" TabIndex="29" CommandArgument='<%# Eval("RowNumber") %>'
                                                                CommandName="Delete" ImageUrl="~/Images/New Icon/GridDelete.png" ToolTip="Delete " />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <ajax:ComboBox ID="ddlItemadapter" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                Visible="false" OnSelectedIndexChanged="ddlItemadapter_SelectedIndexChanged">
                                                            </ajax:ComboBox>
                                                            <%--THIS START HERE--%>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="TxtItemNameadapter" runat="server" Text='<%# Eval("ItemName") %>'
                                                                        CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemNameadapter_TextChanged"></asp:TextBox>
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


                                                        <FooterTemplate>
                                                            <asp:Button ID="btnadapter" runat="server" Text="Add  New  Row" BackColor="#00ffcc" ToolTip="Add  New  Row" Height="30px"
                                                                OnClick="btnadapter_Click" />


                                                        </FooterTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Adaptor Sizes ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtadaptersize" runat="server" Text='<%# Eval("AdaptorSizes") %>' CssClass="TextBox"></asp:TextBox>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                </Columns>
                                            </asp:GridView>






                                              <asp:Label ID="Label6" runat="server" Text="For Handwheel" Font-Bold="true" Font-Size="Larger" ForeColor="Red"></asp:Label>
                                                <asp:GridView ID="grdhandwheel" runat="server" CssClass="mGrid" AutoGenerateColumns="false" ShowFooter="true" OnRowDeleting="grdhandwheel_RowDeleting" OnRowCommand="grdhandwheel_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageAccepted" runat="server" TabIndex="29" CommandArgument='<%# Eval("RowNumber") %>'
                                                                CommandName="Delete" ImageUrl="~/Images/New Icon/GridDelete.png" ToolTip="Delete " />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <ajax:ComboBox ID="ddlItemhandwheel" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                Visible="false" OnSelectedIndexChanged="ddlItemhandwheel_SelectedIndexChanged">
                                                            </ajax:ComboBox>
                                                            <%--THIS START HERE--%>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="TxtItemNamehandwheel" runat="server" Text='<%# Eval("ItemName") %>'
                                                                        CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemNamehandwheel_TextChanged"></asp:TextBox>
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


                                                        <FooterTemplate>
                                                            <asp:Button ID="btnhandwheel" runat="server" Text="Add  New  Row" BackColor="#00ffcc" ToolTip="Add  New  Row" Height="30px"
                                                                OnClick="btnhandwheel_Click" />


                                                        </FooterTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Handwheel Size ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txthandwheel" runat="server" Text='<%# Eval("HandwheelSizes") %>' CssClass="TextBox"></asp:TextBox>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                



                                                </Columns>
                                            </asp:GridView>



                                                                                          <asp:Label ID="Label9" runat="server" Text="For Lever" Font-Bold="true" Font-Size="Larger" ForeColor="Red"></asp:Label>

                                               <asp:GridView ID="grdlever" runat="server" CssClass="mGrid" AutoGenerateColumns="false" ShowFooter="true" OnRowDeleting="grdlever_RowDeleting" OnRowCommand="grdlever_RowCommand">
                                                <Columns>
                                                      <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageAccepted" runat="server" TabIndex="29" CommandArgument='<%# Eval("RowNumber") %>'
                                                                CommandName="Delete" ImageUrl="~/Images/New Icon/GridDelete.png" ToolTip="Delete " />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <ajax:ComboBox ID="ddlItemlever" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                Visible="false" OnSelectedIndexChanged="ddlItemlever_SelectedIndexChanged">
                                                            </ajax:ComboBox>
                                                            <%--THIS START HERE--%>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="TxtItemNamelever" runat="server" Text='<%# Eval("ItemName") %>'
                                                                        CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemNamelever_TextChanged"></asp:TextBox>
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


                                                        <FooterTemplate>
                                                            <asp:Button ID="btnlever" runat="server" Text="Add  New  Row" BackColor="#00ffcc" ToolTip="Add  New  Row" Height="30px"
                                                                OnClick="btnlever_Click" />


                                                        </FooterTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Lever Sizes ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtlever" runat="server" Text='<%# Eval("LeverSizes") %>' CssClass="TextBox"></asp:TextBox>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                  



                                                </Columns>
                                            </asp:GridView>

                                            <asp:Label ID="Label10" runat="server" Text="For Key Cabinets" Font-Bold="true" Font-Size="Larger" ForeColor="Red" Visible="false"></asp:Label>

                                             <asp:GridView ID="grdkey" runat="server" CssClass="mGrid" AutoGenerateColumns="false" ShowFooter="true" Visible="false">
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <ajax:ComboBox ID="ddlItemkey" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                Visible="false" OnSelectedIndexChanged="ddlItemkey_SelectedIndexChanged">
                                                            </ajax:ComboBox>
                                                            <%--THIS START HERE--%>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="TxtItemNamekey" runat="server" Text='<%# Eval("ItemName") %>'
                                                                        CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemNamekey_TextChanged"></asp:TextBox>
                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemNamkey" runat="server" TargetControlID="TxtItemNamekey"
                                                                        CompletionInterval="100" UseContextKey="True" FirstRowSelected="true" CompletionSetCount="20"
                                                                        ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionItemNameList"
                                                                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                    </ajax:AutoCompleteExtender>
                                                                    <ajax:TextBoxWatermarkExtender ID="TBWE2key" runat="server" TargetControlID="TxtItemNamekey"
                                                                        WatermarkText="Type Item Name" WatermarkCssClass="water" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <%--THIS END HERE--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="292px" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />


                                                        <FooterTemplate>
                                                            <asp:Button ID="btnkey" runat="server" Text="Add  New  Row" BackColor="#00ffcc" ToolTip="Add  New  Row" Height="30px"
                                                                OnClick="btnkey_Click" />


                                                        </FooterTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Key Cabinet Sizes ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtkey" runat="server" Text='<%# Eval("KeyCabinet") %>' CssClass="TextBox"></asp:TextBox>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                  



                                                </Columns>
                                            </asp:GridView>
                                        
                                        
                                        
                                        
                                        
                                        
                                        
                                        
                                        



                                          </div>


                                    </td>
                                </tr>
                                <tr>
                                    <td>
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
                                                    <asp:Button ID="BtnUpdate" runat="server" CausesValidation="true" TabIndex="8" CssClass="button" OnClick="BtnUpdate_Click"
                                                         Text="Update" ValidationGroup="Add" />
                                                    <ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
                                                        TargetControlID="BtnUpdate">
                                                    </ajax:ConfirmButtonExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnSave" runat="server" CausesValidation="false" TabIndex="8" CssClass="button"
                                                         OnClick="BtnSave_Click" Text="Save" ValidationGroup="Add" />
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
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true" >
                                <ContentTemplate>
                                    <asp:GridView ID="ReportGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="ReportGrid_RowCommand" OnRowDataBound="ReportGrid_RowDataBound"
                                        CssClass="mGrid" >

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
                                            <asp:BoundField DataField="TemplateName" HeaderText="TemplateName">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="title" HeaderText="title">
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
        </ContentTemplate>
        
                                              
                                                <Triggers>
<asp:PostBackTrigger ControlID="grditem" />
</Triggers>
    </asp:UpdatePanel>
</asp:Content>
