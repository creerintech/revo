<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialRequistnCancel.aspx.cs" Inherits="Transactions_MaterialRequistnCancel" Title="Material Indent Cancellation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">

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
  
      Search for Indent Cancellation :  
  <asp:TextBox ID="TxtSearch" runat="server" 
   CssClass="search" ToolTip="Enter The Text"
   Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged"></asp:TextBox>
 <div id="divwidth"></div>
 <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
TargetControlID="TxtSearch" CompletionInterval="100"                               
UseContextKey="True" FirstRowSelected ="true" 
ShowOnlyCurrentWordInCompletionListItem="true"  
ServiceMethod="GetCompletionList"
CompletionListCssClass="AutoExtender"
CompletionListItemCssClass="AutoExtenderList"                               
CompletionListHighlightedItemCssClass="AutoExtenderHighlight"                         
></ajax:AutoCompleteExtender>
  </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Material Indent Cancellation    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel12" runat="server">
<ContentTemplate>
<table width="100%" >
 <tr>
 <td>
  <fieldset id="F1" class="FieldSet" runat="server" width="100%">
   <table width="100%" cellspacing="6">
    <div>      
      <tr id="Tr2" runat="server">
        <td class="Label" width="13%">
            Cancellation Date :</td>
        <td>
             <asp:TextBox ID="TxtCancelDate"  CssClass="TextBox" runat="server"
    MaxLength="50" Width="120px" ></asp:TextBox>
    <ajax:CalendarExtender ID="CalendarExtender3" runat="server" 
    Format="dd-MMM-yyyy" PopupButtonID="ImageValid2" TargetControlID="TxtCancelDate" />
    <asp:ImageButton ID="ImageValid2" runat="server" CausesValidation="False" 
    CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />      
        </td>
        <td class="Label">        
            Cancelled By :</td>
        <td>
            <asp:Label ID="lblEmployee" runat="server" Font-Bold="True" 
                Text="lblEmployeeName"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
      <tr id="Tr1" runat="server">
        <td class="Label" width="13%">
        </td>
        <td>
            <asp:RadioButtonList ID="RdoType" runat="server" RepeatDirection="Horizontal" 
            CellPadding="25" AutoPostBack="True" 
               onselectedindexchanged="RdoType_SelectedIndexChanged">
            <asp:ListItem Value="R" Text="IndentWise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp" Selected="True"></asp:ListItem>
            <asp:ListItem Value="I" Text="ItemWise" ></asp:ListItem>
        </asp:RadioButtonList>             
        </td>
        <td class="Label">        
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    </div>
   </table>
  </fieldset>
 </td>
 </tr>
 
 <tr>
 <td>
  <fieldset id="Fieldset1" class="FieldSet" runat="server" width="100%">
   <div id="div1" runat="server">
  <table width="100%" cellspacing="6">
 
    <tr id="T1" runat="server">
    <td class="Label" width="13%">
        <asp:CheckBox ID="ChkFrmDate" runat="server" AutoPostBack="True" 
            CssClass="CheckBox" TabIndex="1" 
            Text=" From Date :" oncheckedchanged="ChkFrmDate_CheckedChanged" />
    </td>
    <td> 
        <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox" Width="120px"></asp:TextBox>
        <asp:ImageButton ID="ImageDate" runat="server" CausesValidation="false" 
        CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" 
        Format="dd-MMM-yyyy" PopupButtonID="ImageDate" TargetControlID="txtFromDate">
        </ajax:CalendarExtender>
    </td>
    <td class="Label">
        ToDate: 
    </td>
    <td>
        <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox" Width="120px"></asp:TextBox>
        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" 
        CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
        <ajax:CalendarExtender ID="CalendarExtender2" runat="server" 
        Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" TargetControlID="txtToDate">
        </ajax:CalendarExtender>
    </td>
    <td>
        <asp:Button ID="BtnShow" runat="server" CssClass="button" Text="Show" 
            ValidationGroup="Add" onclick="BtnShow_Click" />
        </td>
    </tr>
  
    <tr id="T2" runat="server" >
    <td class="Label" width="13%">
        Indent No :</td>
    <td >
    <asp:DropDownList ID="ddlReqNo" runat="server" CssClass="ComboBox" Width="250px" 
            AutoPostBack="True" onselectedindexchanged="ddlReqNo_SelectedIndexChanged"></asp:DropDownList>
    
        <asp:RequiredFieldValidator ID="Req_Name" runat="server" 
            ControlToValidate="ddlReqNo" Display="None" 
            ErrorMessage="Please Select Indent" InitialValue="0" SetFocusOnError="True" 
            ValidationGroup="Add"></asp:RequiredFieldValidator>
        <ajax:ValidatorCalloutExtender ID="VCE_Name" runat="server" Enabled="True" 
            TargetControlID="Req_Name" WarningIconImageUrl="~/Images/Icon/Warning.png">
        </ajax:ValidatorCalloutExtender>
    
    </td>
    <td class="Label" >
        From Site :</td>
        <td >
            <asp:Label ID="lblCafeteria" runat="server" Font-Bold="True" 
                Text="lblCafeteria" Width="150px"></asp:Label>
    </td>
        <td>
        <asp:Button ID="BtnShowReq" runat="server" CssClass="button" Text="Show" 
            ValidationGroup="Add" onclick="BtnShowReq_Click" />
        </td>
    </tr>
 
  </table>
  </div>
     
  </fieldset>
 </td>
 </tr>
  
 

 
 <tr>
 <td align="center">
  
  <fieldset id="Fieldset3" runat="server" width="100%"><div class="scrollableDiv">
           <asp:GridView ID="GridDetails" CssClass="mGrid" runat="server" AutoGenerateColumns="False"
           BackColor="White" BorderColor="#0CCCCC" BorderStyle="None" BorderWidth="1px" 
           ForeColor="Black" GridLines="Horizontal" Font-Bold="False" 
               onrowdatabound="GridDetails_RowDataBound" 
               ondatabound="GridDetails_DataBound">
           <Columns>
          <asp:TemplateField HeaderText="#" Visible="false">
           <ItemTemplate>
           <asp:Label ID="LblEntryId" runat="server" Text='<%#Eval("#") %>' width="30px">
           </asp:Label>
           </ItemTemplate>
           </asp:TemplateField>
           
            <asp:TemplateField HeaderText="All">
            <HeaderTemplate>
                <asp:CheckBox ID="GrdSelectAllHeaderR" runat="server" AutoPostBack="True"
                oncheckedchanged="GrdSelectAllHeaderR_CheckedChanged" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="GrdSelectAll1" runat="server" AutoPostBack="true" />
                  
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
          <asp:BoundField HeaderText="Indent No" DataField="RequisitionNo">
          <HeaderStyle HorizontalAlign="Left" Wrap="false" />
          <ItemStyle HorizontalAlign="Left" Wrap="false" />
          </asp:BoundField>           
          <asp:BoundField HeaderText="Indent Date" DataField="RequisitionDate">
          <HeaderStyle HorizontalAlign="Left" Wrap="false" />
          <ItemStyle HorizontalAlign="Left" Wrap="false" />
          </asp:BoundField>
          <asp:BoundField  HeaderText="Site" DataField="Cafeteria">
          <HeaderStyle HorizontalAlign="Left" Wrap="false" />
          <ItemStyle HorizontalAlign="Left" Wrap="false" />
          </asp:BoundField>
           <asp:BoundField HeaderText="CafeteriaId" DataField="CafeteriaId" >
           <HeaderStyle CssClass="Display_None" />
           <ItemStyle CssClass="Display_None"/>
           </asp:BoundField>               
           <asp:BoundField HeaderText="IsCancel" DataField="IsCancel">
           <HeaderStyle CssClass="Display_None" />
           <ItemStyle CssClass="Display_None"/>
           </asp:BoundField>
            
           </Columns>
           </asp:GridView></div>
           </fieldset>
           
 </td>
 </tr>
 
 <tr>
 <td align="center">
 <fieldset id="Fieldset4" runat="server" width="100%">
  <div class="scrollableDiv">
  
    <asp:GridView ID="GridDtlsItem" CssClass="mGrid" runat="server" AutoGenerateColumns="False"
           BackColor="White" BorderColor="#0CCCCC" BorderStyle="None" BorderWidth="1px" 
           ForeColor="Black" GridLines="Horizontal" Font-Bold="False" 
          ondatabound="GridDtlsItem_DataBound">
               
           <Columns>
          <asp:TemplateField HeaderText="#" Visible="false">
           <ItemTemplate>
           <asp:Label ID="LblEntryId" runat="server" Text='<%#Eval("#") %>' width="30px">
           </asp:Label>
           </ItemTemplate>
           </asp:TemplateField>
            
                  
            <asp:TemplateField HeaderText="All">
            <HeaderTemplate>
                <asp:CheckBox ID="GrdSelectAllHeaderI" runat="server" AutoPostBack="True" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="GrdSelectAll2" runat="server" AutoPostBack="true" />
                  
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="ItemCode" DataField="ItemCode" >
           <HeaderStyle HorizontalAlign="Left" Wrap="false" />
           <ItemStyle HorizontalAlign="Left" Wrap="false"/>
           </asp:BoundField>
          <asp:BoundField  HeaderText="Item" DataField="ItemName">
          <HeaderStyle HorizontalAlign="Left" Wrap="false" />
          <ItemStyle HorizontalAlign="Left" Wrap="false" />
          </asp:BoundField>
          <asp:BoundField  HeaderText="ItemDesc" DataField="ItemDesc">
          <HeaderStyle HorizontalAlign="Left" Wrap="false" />
          <ItemStyle HorizontalAlign="Left" Wrap="false" />
          </asp:BoundField>
          <asp:BoundField  HeaderText="Remark" DataField="RemarkForPO">
          <HeaderStyle HorizontalAlign="Left" Wrap="false" />
          <ItemStyle HorizontalAlign="Left" Wrap="false" />
          </asp:BoundField>  
           <asp:BoundField DataField="MinStockLevel" HeaderText="MinStockLevel"  >
           <HeaderStyle Wrap="false" CssClass="Display_None"/>
           <ItemStyle Wrap="false" CssClass="Display_None"/>
           </asp:BoundField>
           <asp:BoundField HeaderText="OrdQty" DataField="OrdQty">
           <HeaderStyle HorizontalAlign="Left" Wrap="false" />
           <ItemStyle HorizontalAlign="Left" Wrap="false" />
           </asp:BoundField>
            <asp:BoundField HeaderText="AvlQty" DataField="AvlQty" >
           <HeaderStyle Wrap="false" CssClass="Display_None"/>
           <ItemStyle Wrap="false" CssClass="Display_None"/>
           </asp:BoundField>
           <asp:BoundField HeaderText="Unit" DataField="Unit">
           <HeaderStyle HorizontalAlign="Left" Wrap="false" />
           <ItemStyle HorizontalAlign="Left" Wrap="false" />
           </asp:BoundField>
           <asp:BoundField HeaderText="ItemID" DataField="ItemId" >
           <HeaderStyle Wrap="false" CssClass="Display_None"/>
           <ItemStyle Wrap="false" CssClass="Display_None"/>
           </asp:BoundField>
           <asp:BoundField HeaderText="IsCancel" DataField="IsCancel" >
           <HeaderStyle Wrap="false" CssClass="Display_None"/>
           <ItemStyle Wrap="false" CssClass="Display_None"/>
           </asp:BoundField>
           <%--<asp:BoundField HeaderText="RequisitionCafeDtlsId" DataField="RequisitionCafeDtlsId" >
           <HeaderStyle Wrap="false" />
           <ItemStyle Wrap="false"/>
           </asp:BoundField>--%>
           </Columns>
           </asp:GridView>
  </div>
  </fieldset>
 </td>
 </tr>
  <tr>
  <td>
    <fieldset id="Fieldset2" class="FieldSet" runat="server" width="100%">
    <table width="100%">   
    <tr>       
        <td class="Label" width="13%">
            Reason :
         </td>
         <td align=left>
            <asp:TextBox ID="TxtCancelReason" runat="server" CssClass="TextBox" 
                Height="50px" TextMode="MultiLine" Width="98%"></asp:TextBox>
        </td>    
        </tr>
        </table>
    </fieldset>
    </td>
    </tr> 
 <tr><td>
<fieldset id="F3" runat="server" width="100%">
<table width="100%" cellspacing="8">    
    <tr>
    <td align="center" colspan="6">
    <table width="25%">
    <tr>
    <td align="center">
    <asp:Button ID="BtnUpdate" runat="server" CausesValidation="False" 
    CssClass="button" Text="Update" 
    ValidationGroup="Add" onclick="BtnUpdate_Click" />
    <ajax:ConfirmButtonExtender ID="CalenderButtonExtender1" runat="server" 
    ConfirmText="Would You Like To Update The Record ?" TargetControlID="BtnUpdate">
    </ajax:ConfirmButtonExtender>
    </td>
    <td>
    <asp:Button ID="BtnSave" runat="server" CausesValidation="False" 
    CssClass="button" Text="Save" ValidationGroup="Add" onclick="BtnSave_Click" />
    </td>
    <td>
    <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" 
    CssClass="button" Text="Cancel" onclick="BtnCancel_Click" />
    </td>
    </tr>
    </table>
    </td>
    </tr>
</table>
</fieldset>
</td></tr>

 <table cellspacing="8" width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:GridView ID="ReportGrid" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="#" 
                            onrowcommand="ReportGrid_RowCommand" 
                            onrowdeleting="ReportGrid_RowDeleting" 
                            onpageindexchanging="ReportGrid_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="#" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="LblEstimateId" runat="server" Text='<%# Eval("#") %>' 
                                            Width="15px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageGridEdit" runat="server" 
                                            CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                            CommandName="Select" 
                                            ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                        <asp:ImageButton ID="ImgBtnDelete" runat="server" 
                                            CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                            CommandName="Delete" 
                                            ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                        <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                            ConfirmText="Would You Like To Delete The Record..!" 
                                            TargetControlID="ImgBtnDelete">
                                        </ajax:ConfirmButtonExtender>
                                        <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&amp;Flag=<%="RC"%>&PDFFlag=<%="PDF"%>' 
                                            target="_blank">
                                        <asp:Image ID="ImgBtnPrint" runat="server" 
                                            ImageUrl="~/Images/New Icon/pdfImg.png" TabIndex="29" 
                                            ToolTip="PDF View of Requisition" />
                                        </a>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" 
                                        Wrap="false" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="RequisitionCancelNo" HeaderText="Indent Cancel No.">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CancelledDate" HeaderText="Cancel Date">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CancelledBy" HeaderText="Cancelled By">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Reason" HeaderText="Reason">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CancelType" HeaderText="CancelType">
                                    <HeaderStyle CssClass="Display_None"/>
                                    <ItemStyle CssClass="Display_None" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RequisitionCafeId" HeaderText="RequisitionCafeId">
                                    <HeaderStyle CssClass="Display_None" />
                                    <ItemStyle CssClass="Display_None"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Cafeteria" HeaderText="Location">
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
</asp:UpdatePanel>
</asp:Content>

