<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Enquiryreport.aspx.cs" Inherits="Reports_Enquiryreport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</ajax:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
  <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
        <ProgressTemplate>            
        <div id="progressBackgroundFilter" style="left: 0px; right: 0px; top: 0px; bottom: 0px"></div>
        <div id="processMessage">   
        <center><span class="SubTitle">Loading....!!! </span></center>
        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
        </div>
        </ProgressTemplate>
  </asp:UpdateProgress>
  </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Purchase Order Summary   

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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="UPEntry" runat="server">
<ContentTemplate>
<fieldset id="F1" runat="server" class="FieldSet">

 <table width="100%" cellspacing="5">
        <tr>
            <td colspan="5">
                </td>
           
        </tr>
        <tr>
            <td class="Label">
               
                </td>
            <td>
                 <table>
                 <tr>
                 <td>
                 <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Width="90px" >
                        </asp:TextBox>
                  <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" 
                    Format="dd-MMM-yyyy" PopupButtonID="ImageButton212" TargetControlID="txtFromDate" />
                  <asp:ImageButton ID="ImageButton212" runat="server" CausesValidation="False" 
                    CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="1" />      
                    </td>
                    <td class="Label">
                        &nbsp;&nbsp;&nbsp;To :</td>                
                     <td>
                     <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" 
                     Width="90px" OnTextChanged="txtToDate_TextChanged" ></asp:TextBox>
                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                    Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" TargetControlID="txtToDate" />
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                    CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" 
                    TabIndex="2"  /> 
                    </td> 
                    </tr> 
                    </table>                    
            </td>
            
           
           
        </tr>
      
        <tr>           
            <td class="Label" colspan="6"></td>
            <td align="left" colspan="1">
                &nbsp;<asp:Button ID="BtnShow" runat="server" CssClass="button" 
                    TabIndex="4" Text="Show" ValidationGroup="Add" 
                    ToolTip="Show Details"  />   
               <asp:Button ID="BtnCancel" runat="server" CssClass="button" 
                    TabIndex="5" Text="Cancel"  
                    ToolTip="Clear The Details"   />
            </td>
        </tr>
</table>
  <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblCount" runat="server" CssClass="SubTitle"></asp:Label>
            </td>
            <td align="right" valign="middle" >
               <asp:ImageButton ID="ImgBtnPrint" runat="server" TabIndex="6"
                  OnClientClick="javascript:CallPrint('divPrint')"
                    ImageUrl="~/Images/Icon/Print-Icon.png" 
                    ToolTip="Print Report"  />
                <asp:ImageButton ID="ImgBtnExport" runat="server" 
                    ImageUrl="~/Images/Icon/excel-icon.png" TabIndex="7"
                    ToolTip="Export To Excel"   />
            </td>
            </tr> 
            <tr>
         
                <td colspan="2" align="center">
                <div id="divPrint" class="ScrollableDiv_FixHeightWidth4" >
                    <asp:GridView ID="GrdReport" runat="server" ShowFooter="true"  AutoGenerateColumns="false"
                       CaptionAlign="Top" AllowPaging="false" CssClass="mGrid"   DataKeyNames="#"  OnRowCommand="ReportGrid_RowCommand"                     
                        Width="100%" 
                         PageSize="30" OnSelectedIndexChanged="GrdReport_SelectedIndexChanged" >
                        <Columns>
                         <asp:TemplateField>
                                                    <ItemTemplate>
                                                        
                                                        
                                                        

                                                       


                                                         <asp:ImageButton ID="ImageButton2" runat="server" TabIndex="29" CommandArgument='<%# Eval("#") %>'
                                                            CommandName="Print" ImageUrl="~/Images/New Icon/pdfImg.png" ToolTip="PDF of Indent Register" />



                                                        
                                                        
                                                      
                                                       
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle Width="20px" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Wrap="false" />
                                                </asp:TemplateField>
                            </Columns>
                       
                    </asp:GridView>
                    </div>
                </td>
        </tr>
            </table>


</fieldset>


    <div style="display: none" >

            <asp:Panel ID="pnlprint" runat="server">

            <div style="width: 695px; height: 860px; border: 2px solid black; font-family: monospace; ">
		<div
			style="width: 695px; height: 162px; border: 2px solid black; margin-left: -2px; margin-top: -2px;">
			<div style="width: 350; margin-left: 325px;">
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
				style="width: 695px; height: 417px; border: 2px solid black; margin-left: -2px; margin-top: -2px; text-align: center;">
				<asp:GridView ID="grdprint" runat="server"  style="width: 695px;" OnRowDataBound="grdprint_RowDataBound"   >
                    </asp:GridView>
			</div> 

			</div>
			


				

			</div>
			</asp:Panel>
			</div>
           









</ContentTemplate>

            <Triggers>
             <asp:PostBackTrigger ControlID ="ImgBtnExport" />
             </Triggers>
</asp:UpdatePanel>
</asp:Content>

