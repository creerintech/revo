<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="approvecomparisionsheet.aspx.cs" Inherits="Transactions_approvecomparisionsheet" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
   <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
 
  
<script type="text/javascript">

function ShowPOP()
{
document.getElementById('<%= dialog.ClientID %>').style.display = "block";
}



function HidePOP()
{
document.getElementById('<%= dialog.ClientID %>').style.display = "none";
}

        
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
  
   
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
        <%--<ProgressTemplate>            
        <div id="progressBackgroundFilter"></div>
        <div id="processMessage">   
        <center><span class="SubTitle">Loading....!!! </span></center>
        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
        </div>
        </ProgressTemplate>--%>
        </asp:UpdateProgress>
        
            Search for Estimate :
        <asp:TextBox ID="TxtSearch" runat="server" 
        CssClass="search" ToolTip="Enter The Text"    Width="292px" 
        AutoPostBack="True" ></asp:TextBox> 
        <div id="divwidth"></div>
        <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
        TargetControlID="TxtSearch"   CompletionInterval="100"  UseContextKey="True" FirstRowSelected ="true"                              
        ShowOnlyCurrentWordInCompletionListItem="true"  ServiceMethod="GetCompletionList"
        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" >                         
        </ajax:AutoCompleteExtender> 
        </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
   <asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
       Approve &amp; Authorise Estimate       
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
      <ContentTemplate>
      <table width="100%" cellpadding="8">
     
         <tr >
           <td align="center" >
         
              <fieldset id="Fieldset2"  class="FieldSet" runat="server">
              <legend id="Legend3" class="legend" runat="server">Estimate </legend>
                     <div id="div1" class="ScrollableDiv_FixHeightWidthAPP">
                     <table width="100%">
                     <tr><td>
                     <table width="100%">
                    
                     
                     <tr><td colspan="3"> <hr /></td></tr>
                     </table>
                     </td></tr>
                         <tr>
                            <td align="center">        
                            <asp:GridView ID="GrdReq" runat="server" AutoGenerateColumns="False" Width="100%"
                                 CssClass="mGrid" BackColor="White" BorderColor="#0CCCCC" PageSize="20"
                                 BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                                 AllowPaging="false" DataKeyNames="#"  onrowdatabound="GrdReq_RowDataBound"   
                                    onrowcommand="GrdReq_RowCommand" >
                            <columns>
                          <asp:TemplateField HeaderText="#" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="LblEntryId" runat="server" Text='<%# Eval("#") %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                       
                                <asp:TemplateField>
                                <ItemTemplate>
                            
                                <asp:ImageButton ID="ImageApproved" runat="server"  OnClick="ImageApproved_Click" 
                                ImageUrl="~/Images/New Icon/ImgApprove.jpg" ToolTip="Would You Like To Approve This Indent?" />

                                <asp:ImageButton ID="ImageAuthorised" runat="server" Visible="false" OnClick="ImageAuthorised_Click" 
                                ImageUrl="~/Images/New Icon/ImgAuthorised.jpg" ToolTip="Would You Like To Authorise This Indent?"  />
                                
                                <asp:ImageButton ID="ImageSuccess" runat="server" Visible="false"
                                ImageUrl="~/Images/New Icon/Success1.jpg" ToolTip="This is Authorised Indent, Can't Change Its Status" />
                                
                                <asp:ImageButton ID="ImageCancel" runat="server" Visible="false"
                                ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Cancel"  OnClick="ImageCancel_Click" />
                                </ItemTemplate>
                                
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                </asp:TemplateField>
                                           
                                <asp:TemplateField>
                                <ItemTemplate>  
                                <asp:ImageButton ID="ImageGridEdit" runat="server"  Visible="false"
                                CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                CommandName="PDF" ImageUrl="~/Images/New Icon/ViewRecord.png" ToolTip="Show Items In Indent" />
                                
                                
                                <asp:ImageButton ID="ImageGridShowPopup1" runat="server"  Visible="false"
                                CommandArgument="<%#((GridViewRow)Container).RowIndex %>" 
                                CommandName="SHOWPOPUP" ImageUrl="~/Images/New Icon/ViewRecord.png" ToolTip="Enter Remark Item Wise" />
                                
                                  <%--  <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="RS"%>&PDFFlag=<%="PDF"%>' target="_blank">
                                    <asp:Image ID="Image1" runat="server" 
                                    ImageUrl="~/Images/New Icon/pdfImg.png" TabIndex="29" 
                                    ToolTip="PDF View of Indent" />
                                    </a>--%>

                                      <asp:ImageButton ID="ImageButton1" runat="server"  
                                CommandArgument='<%# Eval("#") %>'
                                CommandName="Print" ImageUrl="~/Images/New Icon/pdfImg.png" ToolTip="PDF View " />



                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"
                                Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                <ItemTemplate>
                                <a href="../PrintReport/PrintCrystalReport.aspx?ID=<%# Eval("#")%>&Flag=<%="PO"%>" target="_blank">
                                <asp:Image ID="ImgGridPrint" runat="server" CssClass="Imagebutton"
                                ImageUrl="~/Images/Icon/GridPrint.png" ToolTip="Print Record" Visible="false"/></a> 

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                </asp:TemplateField>
                            <asp:BoundField DataField="project" HeaderText="Project">
                                     <HeaderStyle Wrap="False" />
                                     <ItemStyle Wrap="False" />
                                 </asp:BoundField>
                                 
                                 <asp:BoundField DataField="enquiry" HeaderText="Enquiry">
                                     <HeaderStyle Wrap="False" />
                                     <ItemStyle Wrap="False" />
                                 </asp:BoundField>
                                 
                             <asp:BoundField DataField="status" HeaderText="Status">
                                    <HeaderStyle Wrap="False" CssClass ="Display_None" />
                                    <ItemStyle Wrap="False" CssClass ="Display_None" />
                                </asp:BoundField>
                             
                       
                           </columns>
                            </asp:GridView>
                            </td>
                         </tr>  
                         <tr>
            <td align="center" colspan="2" >
                <table align="center" width="25%">
                    <tr>
                        <td>
                        <asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" 
                                ValidationGroup="Add" OnClick="BtnSave_Click" />
                        </td>
                        <td>
                        <asp:Button ID="BtnAuthorized" CssClass="button" runat="server" Text="Authorized" 
                            ValidationGroup="Add" Visible="False"  />
                        </td>
                        <td>
                          <asp:Button ID="BtnCancel" CssClass="button" runat="server" Text="Cancel" 
                               ValidationGroup="Add"  CausesValidation="False"  />
                        </td>
                    </tr>

                </table>
            </td>
         </tr>
                     </table>
                     </div>
              </fieldset>
           
            </td>
         </tr>
         
         <tr id="TR_PODtls" runat="server">
              <td align="left" colspan="2">
                  <fieldset ID="Fieldset1" runat="server" class="FieldSet" style="width: 100%">
                      <legend ID="Legend1" runat="server" class="legend" 
                          onclick="return Legend1_onclick()">Estimate  Details</legend>
                            <table width="99%">
                              <tr>
                              <td>
                      <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true">
                          <ContentTemplate>
                            <asp:GridView ID="GrdReqDtls" runat="server" AutoGenerateColumns="False" 
                                  CssClass="mGrid">
                                  <Columns>
                                    
                                      <asp:BoundField DataField="ItemId" HeaderText="ItemId">
                                          <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" 
                                              VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle CssClass="Display_None" HorizontalAlign="Left" 
                                              VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="ItemCode" HeaderText="Code">
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                      </asp:BoundField>
                                      <asp:BoundField DataField="ItemName" HeaderText="Item">
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="Qty" HeaderText="Ord. Qty">
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="Rate" HeaderText="Rate">
                                          <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                          <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                      </asp:BoundField>
                                      <asp:BoundField DataField="SuplierName" HeaderText="Supplier Name">
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                      </asp:BoundField>
                                       <asp:BoundField DataField="Emp" HeaderText="By Employee">
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                      
                                          <asp:BoundField DataField="Priority" HeaderText="Priority">
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                      
                                            <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                            <asp:Label runat="server" ID="LblRemark" Text='<%# Eval("Remark") %>' ToolTip='<%#Eval("RemarkFull") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                            </asp:TemplateField>
                                
                                      
                                  </Columns>
                              </asp:GridView>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                      </td>
                      </tr>
                      
                              <tr>
                                  <td align="left">
                                      <asp:LinkButton ID="hyl_Hide" runat="server" CssClass="linkButton">Hide</asp:LinkButton>
                                  </td>
                              </tr>
                      </table>
                      
                  </fieldset>
              </td>
          </tr>
           
         
       </table>
       
       
       
       <div id="dialog" class="PopUpSample" runat="server">
        <div id="progressBackgroundFilter1"></div>
          <div id="Div2" class="PopUpSample" runat="server">
       <table width="90%" cellspacing="16px">
       <tr>
       <td colspan="2"><asp:Label runat="server" ID="LBLTEXT" Text="MATERIAL INDENT DETAILS.." CssClass="LabelPOP"></asp:Label></td>
       <td runat="server" align="right">
            <asp:ImageButton ID="ImgBtnClose" runat="server" OnClientClick="javascript:HidePOP();"            
            ImageUrl="~/Images/New Icon/close-button1.png" ToolTip="Close" 
                onclick="ImgBtnClose_Click"  />    
       </td>
       </tr>
       <tr>
       <td colspan="3">
            <div id="divPrintsd" class="ScrollableDiv_FixHeightWidthPOP">
                         <asp:GridView ID="GRDPOPUPFOREDIT" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                  <Columns>
                                        <asp:BoundField DataField="RequestId" HeaderText="RequestID">
                                          <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" 
                                              VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle CssClass="Display_None" HorizontalAlign="Left" 
                                              VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                       <asp:BoundField DataField="RequestNo" HeaderText="Indent No">
                                          <HeaderStyle  HorizontalAlign="Center" 
                                              VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle  HorizontalAlign="Left" 
                                              VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                         <asp:BoundField DataField="RequestDate" HeaderText="Indent Date">
                                          <HeaderStyle  HorizontalAlign="Center" 
                                              VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle  HorizontalAlign="Left" 
                                              VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="ItemId" HeaderText="ItemId">
                                          <HeaderStyle CssClass="Display_None" HorizontalAlign="Center" 
                                              VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle CssClass="Display_None" HorizontalAlign="Left" 
                                              VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="ItemCode" HeaderText="Code">
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                      </asp:BoundField>
                                      <asp:BoundField DataField="ItemName" HeaderText="Item">
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                     
                                      <asp:BoundField DataField="Qty" HeaderText="Ord. Qty">
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                  
                                      <asp:BoundField DataField="Rate" HeaderText="Rate">
                                          <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                          <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                      </asp:BoundField>
                                      <asp:BoundField DataField="Emp" HeaderText="By Employee">
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" />
                                      </asp:BoundField>
                                      <asp:BoundField DataField="Priority" HeaderText="Priority">
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                      </asp:BoundField>
                                         <asp:BoundField DataField="Location" HeaderText="Site">
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="true" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" />
                                      </asp:BoundField>
                                      <asp:TemplateField HeaderText="Remark">
                                      <ItemTemplate>
                                      <asp:Label runat="server" ID="LblRemark" Text='<%# Eval("Remark") %>' ToolTip='<%#Eval("RemarkFull") %>' ></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                      </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark For Authorisation">
                                      <ItemTemplate>
                                      <asp:TextBox runat="server" ID="TXTXREMARKFORAUTH" Width="200px" TextMode="MultiLine" Text='<%# Eval("RemarkAuth") %>' ToolTip="Remark Field For Authorisation"></asp:TextBox>
                                      </ItemTemplate>
                                      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                      </asp:TemplateField>
                                      
                                  </Columns>
                              </asp:GridView>

            </div>
       </td>
       </tr>
        <tr>
        <td class="Label">
        <b>Remark :</b>
        </td>
        <td>
         <asp:TextBox ID="TxtRemarkAl" width="750px" TextMode="MultiLine" runat="server" CssClass="TextBox"></asp:TextBox>
        </td>
        </tr>
       <tr>
       <td align="center" colspan="3">
        <asp:Button ID="BtnUpdate" CssClass="button" runat="server" Text="Update" 
               CausesValidation="false" />
<ajax:RoundedCornersExtender ID="RoundedCornersExtender5" runat="server" TargetControlID="BtnUpdate" Corners="All" Radius="8" BorderColor="Gray"></ajax:RoundedCornersExtender>          
               
       </td>
       </tr>
       </table>
       </div>
       </div>




















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
 </asp:UpdatePanel>
</asp:Content>