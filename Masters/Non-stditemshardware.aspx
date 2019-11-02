<%@ Page Title="Non-std items hardware" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Non-stditemshardware.aspx.cs" Inherits="Masters_Non_stditemshardware" %>

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
  Non-std items hardware
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="8">
                
                <tr>
                    <td>
                        <fieldset id="F2" runat="server" width="100%">
                            <table width="100%" cellspacing="8">
                                <tr>
                                    <td colspan="3">
                                        <div id="DivDisp" runat="server" class="ScrollableDiv_FixHeightWidth4" width="90%">


                                             <asp:GridView ID="grdwoodenbox" runat="server" CssClass="mGrid" AutoGenerateColumns="false" ShowFooter="true">
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <ajax:ComboBox ID="ddlItemwoden" runat="server" DropDownStyle="DropDown" AutoPostBack="true"
                                                                Font-Size="Medium" AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline"
                                                                TabIndex="4" Height="25px" ItemInsertLocation="Append" Width="270px" CssClass="Display_None"
                                                                Visible="false" OnSelectedIndexChanged="ddlItemwoden_OnSelectedIndexChanged_">
                                                            </ajax:ComboBox>
                                                            <%--THIS START HERE--%>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="TxtItemNamewooden" runat="server" Text='<%# Eval("ItemName") %>'
                                                                        CssClass="search_List" Width="292px" AutoPostBack="True" OnTextChanged="TxtItemNamewooden_OnTextChanged"></asp:TextBox>
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


                                                        <FooterTemplate>
                                                            <asp:Button ID="btnwoodenbox" runat="server" Text="Add  New  Row" BackColor="#00ffcc" ToolTip="Add  New  Row" Height="30px"
                                                                OnClick="btnwoodenbox_OnClick" />


                                                        </FooterTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtwoodenbox" Visible="false" runat="server" Text='<%# Eval("WoodenBoxSize") %>' CssClass="TextBox"></asp:TextBox>

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
                                            <asp:BoundField DataField="TemplateName" HeaderText="Particular">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="title" HeaderText="Size">
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

</Triggers>
    </asp:UpdatePanel>
</asp:Content>

