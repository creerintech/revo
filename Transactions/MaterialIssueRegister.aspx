<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialIssueRegister.aspx.cs" Inherits="Transactions_MaterialIssueRegister1" Title="Material Issue Register" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
    <script language="javascript" type="text/javascript">
    function oncheck()
    {
    var btn=document.getElementById('<%= LnkShow.ClientID %>');
    var btnname=btn.innerText ;
    if(btn.innerText=="Add Extra Item")
    {
    }
    else
    { 
    }
    }
    </script>
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
        Search For Issue No:
     <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" ToolTip="Enter The Text"
      Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged">
      </asp:TextBox>
     <div id="divwidth"></div>
     <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
      TargetControlID="TxtSearch" CompletionInterval="100"                             
      UseContextKey="True" FirstRowSelected ="true" ShowOnlyCurrentWordInCompletionListItem="true"
       ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender"
      CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
    </ajax:AutoCompleteExtender>
    </ContentTemplate>
  </asp:UpdatePanel>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Material Issue Register         
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server"> 
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
     <ContentTemplate >
  <table width="100%">
   <tr>
   <td>
   
     <asp:UpdatePanel ID="UpdatePanel8" runat="server">
     <ContentTemplate >
   <table width="100%">
   <tr>
   <td>
   <fieldset id="fieldset1" width="100%" runat="server" class="FieldSet">
    <legend class="legend" align="left">Material Issue Details</legend>
          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
     <ContentTemplate >
     <table width="100%" cellspacing="8">
          
           <tr>
               <td class="Label">
                    M.Issue No :</td>
                           <td>
                  <asp:TextBox ID="TxtMIssueNo1"  CssClass="TextBox" runat="server"
                   Width="160px" ReadOnly="True" Visible="false"></asp:TextBox>
                   <asp:Label ID="TxtMIssueNo" CssClass="Label" runat="server" Font-Bold="true"></asp:Label>
               </td>
               <td class="Label">
                   M.Issue BY :</td>
               <td>
                 <asp:DropDownList ID="ddlIssueTo1" runat="server" Width="162px" Visible="false"
                     CssClass="ComboBox" onselectedindexchanged="ddlIssueTo_SelectedIndexChanged">
                 </asp:DropDownList>
                 <asp:Label ID="ddlIssueTo" runat="server" CssClass="Label"></asp:Label>
                 <%--<asp:RequiredFieldValidator ID="RFV1" runat="server" ControlToValidate="ddlIssueTo" TargetControlId="ddlIssueTo"
                 Enabled="true" Display="None" ErrorMessage="Employee Name Is Required" SetFocusOnError="true"
                  ValidationGroup="Add" InitialValue="0"></asp:RequiredFieldValidator>
                 
                 <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="true"
                 TargetControlID="RFV1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                 </ajax:ValidatorCalloutExtender>--%>
                 
               </td>
               <td class="Label">
                   M.Issue Date :</td>
              <td>
                <asp:TextBox ID="TxtIssueDate" CssClass="TextBox" runat="server"
                  Width="120px"></asp:TextBox>
                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MMM/yyyy" 
                   PopupButtonID="Img_IssueDate" TargetControlID="TxtIssueDate" />
                <asp:ImageButton ID="Img_IssueDate" runat="server" CausesValidation="False" 
                   CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
               </td>
           </tr>
           
<tr><td colspan="6"></td></tr>
           <tr>
   <td colspan="6">
  <%-- <legend class="legend" align="left">Requisition Details</legend>--%>
  <hr style="width:100%"/>
   </td>
   </tr>
      <tr>
         <td class="Label">
                   Requisition No :
                   </td>
              <td>
                  <asp:DropDownList ID="ddlRequisitionNo" runat ="server" Width="182px"
                  CssClass="ComboBox" 
                       onselectedindexchanged="ddlRequisitionNo_SelectedIndexChanged" 
                      AutoPostBack="True"> 
                  </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="RFV3" runat="server" ControlToValidate="ddlRequisitionNo" Display="None"
                       TargetControlId="ddlRequisitionNo" Enabled="true" ErrorMessage="Requisition No Is Required"
                       SetFocusOnError="true" ValidationGroup="Add" InitialValue="0">
                       </asp:RequiredFieldValidator>
                       <ajax:ValidatorCalloutExtender ID="VCE_Name3" runat="server" Enabled="true" 
                       TargetControlID="RFV3" WarningIconImageUrl="~/Images/Icon/Warning.png">
                       </ajax:ValidatorCalloutExtender>
                       
                     
               </td>
                   <%--<td class="Label">
                       Issue To Cafeteria :
                   </td>
                   <td>
                    <asp:Label ID="lblCafeteria" CssClass="Label1" runat="server" Width="160px" align="left"
                       ReadOnly="True"></asp:Label>
                   </td>--%>
                     <td class="Label">
                         Requisition By :</td>
               <td>
                   <asp:TextBox ID="TxtReqBy1" CssClass="TextBox" runat="server"
                  Width="160px" ReadOnly="True" Visible="false"></asp:TextBox>
                  <asp:Label ID="TxtReqBy" CssClass="Label" runat="server"></asp:Label>
                
               </td>
               <td class="Label">
                   Requisition Date :</td>
                  <td>
                <asp:TextBox ID="TxtReqstnDate1" CssClass="TextBox" runat="server"
                  Width="120px" ReadOnly="True" Visible="false"></asp:TextBox>
                  <asp:Label ID="TxtReqstnDate" runat="server" CssClass="Label"></asp:Label>
               </td>
              </tr>
              <tr><td colspan="6"></td></tr>
             <tr><td colspan="6"><hr style="width:100%"/></td></tr>
         <tr> <td colspan="6"><asp:LinkButton runat="server" ID="LnkShow" Text="Add Extra Item" OnClientClick='javascript:oncheck();'></asp:LinkButton></td></tr>
             <tr>
          
               <td class="Label">
                   <asp:Label ID="lblCategory" runat="server" Text="Category :"></asp:Label></td>
               <td>
                      <asp:DropDownList ID="ddlCategory" runat ="server" Width="182px"
                  CssClass="ComboBox"  AutoPostBack="True" 
                          onselectedindexchanged="ddlCategory_SelectedIndexChanged"> 
                  </asp:DropDownList>
                
               </td>
               <td class="Label">
                  <asp:Label ID="lblItem" runat="server" Text="Items :"></asp:Label></td>
                  <td>
                   <asp:DropDownList ID="ddlItems" runat ="server" Width="162px"
                  CssClass="ComboBox"  AutoPostBack="True"> 
                  </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItems" Display="None"
                       TargetControlId="ddlItems" Enabled="true" ErrorMessage="Please Select Item To Add"
                       SetFocusOnError="true" ValidationGroup="AddItem" InitialValue="0">
                       </asp:RequiredFieldValidator>
                       <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="true" 
                       TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                       </ajax:ValidatorCalloutExtender>   
               </td>
               <td class="Label">Qty :  <asp:TextBox ID="TxtIssueQty" runat="server" CssClass="TextBox" Width="80px"></asp:TextBox>
                <ajax:FilteredTextBoxExtender ID="FTE_Tel" runat="server" TargetControlID="TxtIssueQty"
                                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>  </td>
               <td class="Label">Remark :  <asp:TextBox ID="TxtRemark" runat="server" CssClass="TextBox" TextMode="MultiLine"  Width="170px" Height="20px"></asp:TextBox>
                   &nbsp;&nbsp;
                  <asp:ImageButton ID="BtnAdd" runat="server" ToolTip="Add To List" ValidationGroup="AddItem"
                                            ImageUrl="~/Images/Icon/Gridadd.png" TabIndex="9" 
                          onclick="BtnAdd_Click" /> </td>
      
           </tr>
           </table>
           </ContentTemplate>
           </asp:UpdatePanel>
           
          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
             <ContentTemplate>
               <table width="100%" cellspacing="6">
           <tr>  <%--For The Item Details(grid)--%>
          <td>
          <div class="ScrollableDiv_FixHeightWidth4">
          <asp:GridView ID="GridDetails" CssClass="mGrid" runat="server" AutoGenerateColumns="False"
          BackColor="White" BorderColor="#0CCCCC" BorderStyle="None" BorderWidth="1px"
           ForeColor="Black" GridLines="Horizontal" Font-Bold="False" 
                  onrowdatabound="GridDetails_RowDataBound" 
                  onrowcommand="GridDetails_RowCommand">
            <Columns>
           <asp:BoundField HeaderText="ItemId" DataField="ItemId">
           <HeaderStyle Wrap="false" CssClass="Display_None" />
           <ItemStyle Wrap="false" CssClass="Display_None" />
           </asp:BoundField>
           <asp:BoundField HeaderText="Item Code" DataField="ItemCode">
           <HeaderStyle Wrap="false"  />
           <ItemStyle Wrap="false" HorizontalAlign="Left" />
           </asp:BoundField>
           <asp:BoundField HeaderText="Item" DataField="ItemName" ControlStyle-Width="25" HeaderStyle-Width="25" ItemStyle-Width="50" >
               <ControlStyle Width="25px" />
           <HeaderStyle Wrap="false" />
           <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" 
                   CssClass="mGridFont" />
           </asp:BoundField>
           
                <asp:BoundField DataField="AvailableQty" HeaderText="AvailableQty">
                    <HeaderStyle CssClass="Display_None" />
                    <ItemStyle Wrap="False" CssClass="Display_None" />
                </asp:BoundField>
           
               <asp:BoundField DataField="Qty" HeaderText="Requisition Qty" ControlStyle-Width="25">
                   <ControlStyle Width="25px" />
                   <HeaderStyle Wrap="False" />
                   <ItemStyle Wrap="False" CssClass="mGridFont" HorizontalAlign="Right"/>
               </asp:BoundField>
               
               <asp:TemplateField HeaderText="Issue Qty" ItemStyle-Width="25">
                   <ItemTemplate>
                       <asp:TextBox ID="TxtIssueQty" runat="server" CssClass="TextBox" Text='<%#Eval("IssueQty") %>' 
                           ontextchanged="TxtIssueQty_TextChanged"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FTE_IS" runat="server" TargetControlID="TxtIssueQty"
                                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender> 
                   </ItemTemplate>
                   <HeaderStyle Wrap="False" />
                   <ItemStyle Wrap="False" CssClass="mGridFont" />
               </asp:TemplateField>
           
                <asp:BoundField DataField="PendingQty" HeaderText="Pending Requisition Qty">
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" CssClass="mGridFont" HorizontalAlign="Right"/>
                </asp:BoundField>
                
                        <asp:TemplateField HeaderText="Notes"  >
                    <ItemTemplate>
                        <asp:TextBox ID="TxtNotes" runat="server" Text='<%#Eval("Notes") %>' CssClass="TextBox" 
                         TextMode="MultiLine" Width="250px" Height="15px"></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" CssClass="mGridFont" />
                </asp:TemplateField>
                
                <asp:BoundField DataField="Status" HeaderText="Issue Status Of Item">
                    <HeaderStyle Wrap="False" CssClass="Display_None" />
                    <ItemStyle Wrap="False" CssClass="Display_None" />
                </asp:BoundField>
           

           
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
    </ContentTemplate>
    </asp:UpdatePanel>
    </td>
    </tr>
   
   <tr>
  <td>
  <asp:UpdatePanel ID="UpdatePanel9" runat="server">
     <ContentTemplate>
   <table width="100%">
   <tr>
    <td>
   <fieldset id="fieldset4" width="100%" runat="server" class="FieldSet">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
             <ContentTemplate>
   <table width="100%">
   <tr>
   <td align="center">
        <table align="center" width="25%">
            <tr>
                <td>
                    <asp:Button ID="BtnUpdate" runat="server" CssClass="button" 
                      TabIndex="17" Text="Update" onclick="BtnUpdate_Click" 
                         />
                    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                      ConfirmText="Would You Like To Update the Record ..! " 
                      TargetControlID="BtnUpdate">
                    </ajax:ConfirmButtonExtender>
                </td>
                <td>
                    <asp:Button ID="BtnSave" runat="server" CssClass="button" 
                      TabIndex="18" Text="Save" onclick="BtnSave_Click" />
                </td>
                <%--<td>
                    <asp:Button ID="BtnDelete" runat="server" CssClass="button" 
                      Text="Delete" ValidationGroup="Add" onclick="BtnDelete_Click" />
                </td>--%>
                <td>
                    <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" 
                     CssClass="button" TabIndex="19" Text="Cancel" onclick="BtnCancel_Click"/>
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
    </asp:UpdatePanel>
    </td>
    </tr>
  
   <tr>
  <td>
   <%-- <legend class="legend" align="left">Issue Register List </legend>   --%>
  </td>
  </tr>
  
   <tr>
  <td>
  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
        <table width="100%" cellspacing="6">
        <tr>
        <td>
         <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
        <table width="100%" cellspacing="6">
        <tr>
        <td>
  <div class="scrollableDiv">
  <asp:GridView ID="GrdReport" CssClass="mGrid" runat="server" AutoGenerateColumns="False"
  BackColor="White" BorderColor="#0CCCCC" BorderStyle="None" AllowPaging="true" PageSize="10"
  BorderWidth="1px" ForeColor="Black" GridLines="Horizontal" Font-Bold="False" 
          onrowcommand="GrdReport_RowCommand" onrowdeleting="GrdReport_RowDeleting" 
          onpageindexchanging="GrdReport_PageIndexChanging">
  <Columns>
  <asp:TemplateField>
  <ItemTemplate>
  <asp:ImageButton ID="ImgGridEdit" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
  CommandName="SelectGrid" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
 <%-- <asp:ImageButton ID="ImagePrint" runat="server" CommandArgument='<%#Eval("#") %>' CommandName="Print"
  ImageUrl="~/Images/Icon/GridPrint.png" ToolTip="Print" />
  <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Want To Delete This Record ?"
  TargetControlID="ImagePrint">
  </ajax:ConfirmButtonExtender>--%>
    <asp:ImageButton ID="ImageBtnDelete" runat="server" 
    CommandArgument='<% #Eval("#") %>' CommandName="Delete" 
    ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" 
    ConfirmText="Would You want To Delete This Record ?" 
    TargetControlID="ImageBtnDelete">
    </ajax:ConfirmButtonExtender>

    <a href="../PrintReport/MaterialIssueRegisterPrint.aspx?ID=<%# Eval("#")%>" target="_blank">
    <asp:Image ID="ImgBtnPrint" runat="server" ImageUrl="~/Images/Icon/GridPrint.png"
    ToolTip="Print Issue Register" TabIndex="29" />
  </ItemTemplate>
  <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
  <HeaderStyle Wrap="false" Width="6px"/>
  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
  </asp:TemplateField>
  
 <asp:BoundField HeaderText="Issue No" DataField="IssueNo">
 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
 </asp:BoundField>
 
 <asp:BoundField HeaderText="IssueDate" DataField="IssueDate" DataFormatString="{0:d}">
 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
 </asp:BoundField>
 
 <asp:BoundField HeaderText="Issue To" DataField="EmpName">
 <HeaderStyle CssClass="Display_None"  />
 <ItemStyle CssClass="Display_None"  />
 </asp:BoundField>
 
 <asp:BoundField HeaderText="Requisition No" DataField="RequisitionNo">
 <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
 <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="false" />
 </asp:BoundField>
 
 <asp:BoundField HeaderText="Requisition Date" DataField="RequisitionDate" DataFormatString="{0:d}">
  <HeaderStyle CssClass="Display_None" />
  <ItemStyle CssClass="Display_None"  />
  </asp:BoundField>
 
 
      <asp:BoundField DataField="RequisitionCafeId" HeaderText="RequisitionId">
          <HeaderStyle CssClass="Display_None" Wrap="False" />
          <ItemStyle CssClass="Display_None" Wrap="False" />
      </asp:BoundField>
 
 
      <asp:BoundField DataField="IssueRegisterId" HeaderText="IssueRegisterId">
          <HeaderStyle CssClass="Display_None" Wrap="False" />
          <ItemStyle CssClass="Display_None" Wrap="False" />
      </asp:BoundField>
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
  </table>
 
    </ContentTemplate>
  </asp:UpdatePanel>
  </td>
  </tr>
  </table>
  </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>

