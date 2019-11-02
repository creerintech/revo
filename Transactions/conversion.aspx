<%@ Page Title="Supplier Conversion Sheet" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="conversion.aspx.cs" Inherits="Transactions_conversion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    


            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=grdreport] td").bind("click", function () {
                $('[id*=txt1]').val($(this).html());
            });
            return false;
        }); 
    </script>

            
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


             <script type="text/javascript">
        function fnExcelReport() {
            var tab_text = '<html xmlns:x="urn:schemas-microsoft-com:office:excel">';
            tab_text = tab_text + '<head><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet>';
       
            tab_text = tab_text + '<x:Name>Test Sheet</x:Name>';

            tab_text = tab_text + '<x:WorksheetOptions><x:Panes></x:Panes></x:WorksheetOptions></x:ExcelWorksheet>';
            tab_text = tab_text + '</x:ExcelWorksheets></x:ExcelWorkbook></xml></head><body>';
           
            tab_text = tab_text + "<table border='1000px'>";
            tab_text = tab_text + $('#myTable').html();
            tab_text = tab_text + '</table></body></html>';

            var data_type = 'data:application/vnd.ms-excel';

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
                if (window.navigator.msSaveBlob) {
                    var blob = new Blob([tab_text], {
                        type: "application/csv;charset=utf-8;"
                    });
                    navigator.msSaveBlob(blob, 'Test file.xls');
                }
            } else {
                $('#test').attr('href', data_type + ', ' + encodeURIComponent(tab_text));
                $('#test').attr('download', 'SupplierConversionSheet.xls');
            }

        }
    </script>






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
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Supplier Enquiry Comparsion
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
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
                                            <asp:DropDownList ID="drpproject" runat="server" CssClass="ComboBox"  Width="142px" 
                                                AutoPostBack="true">
                                            </asp:DropDownList>

                                         
                                        </td>


                                        <td class="Label">Enquiry :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpenquiry" runat="server" CssClass="ComboBox"  Width="142px" OnSelectedIndexChanged="drpenquiry_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>

                                            <asp:Label ID="est" runat="server" Visible="false"></asp:Label>
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

                                        <asp:GridView ID="grdreport" CssClass="mGrid" runat="server" OnRowDataBound="grdreport_RowDataBound" ></asp:GridView>
                                    </div>
                                </td>

                                
                            </tr>
                               
                        </table>
                    </fieldset>
                </td>
            
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
                                             <a href="#" id="test" class="button"
                                onclick="javascript:fnExcelReport();">Export </a>
                                                </td>
                                            <td>
                                                <asp:Button ID="BtnSave" runat="server" CausesValidation="true" TabIndex="8" CssClass="button"
                                                    Text="Genratre PO" ValidationGroup="Add" Width="150px" OnClick="BtnSave_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" TabIndex="8" CssClass="button" Visible="false"
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
                                <asp:GridView ID="ReportGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False"  OnRowCommand="ReportGrid_RowCommand"
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

                                        <asp:BoundField DataField="project" HeaderText="Project">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="enquiry" HeaderText=" Enquiry ">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        </asp:BoundField>

                                           <asp:BoundField DataField="status" HeaderText=" Status ">
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
           


              <div id="myTable" style="display: none; ">

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
			<label>SUPPLIER  COMPARSION  SHEET </label>
		</div>
		<div>
			<div
				style="width: 330px; height: 250px; border: 2px solid black; margin-left: -2px; margin-top: -2px;">


				<%--<label><b><u>Supplier's Name & Address</u></b></label>--%>

                <table>
                    <caption>
                      
                        </tr>
                        <tr>
                            <td> Date :</td>
                            <td>
                                <asp:Label ID="lbdate" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                             
                            </td>
                        </tr>
                       
                    </caption>

				</table>
			</div>
			<div
				style="width: 363px; height: 250px; border: 2px solid black; margin-left: 330px; margin-top: -254px;">


				<table>
					<tr>
						<td colspan="2"><label><b><u>  SUPPLIER COMPARSION SHEET  </u></b></label></td>
					</tr>
					
					

					
					
				</table>

			</div>
			

			
			
			<div
				style="width: 695px; height: 417px; border: 2px solid black; margin-left: -2px; margin-top: -2px; text-align: center;">
				<asp:GridView ID="grdprint" runat="server"  style="width: 695px;"    >
                    </asp:GridView>
			</div> 

			</div>
			


				

			</div>




                  </asp:Panel>

              </div>



        </ContentTemplate>

         <Triggers>
            <asp:PostBackTrigger ControlID="BtnSave" />
        </Triggers>




    </asp:UpdatePanel>
</asp:Content>

