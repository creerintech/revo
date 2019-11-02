<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="TaxMaster.aspx.cs" Inherits="Masters_TaxMaster" Title="Tax Template" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" >
    <ProgressTemplate>            
    <div id="progressBackgroundFilter"></div>
    <div id="processMessage">   
    <center><span class="SubTitle">Loading....!!! </span></center>
    <asp:Image ID="Image587" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
    </div>
    </ProgressTemplate>
    </asp:UpdateProgress>

    Search for Tax : 
      <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" 
        ToolTip="Enter The Text" Width="292px" AutoPostBack="True" 
        ontextchanged="TxtSearch_TextChanged" TabIndex="3"></asp:TextBox>
     <div id="divwidth"></div>
      <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"  
         runat="server" TargetControlID="TxtSearch" 
         CompletionInterval="100"                             
         UseContextKey="True" FirstRowSelected ="true" 
         ShowOnlyCurrentWordInCompletionListItem="true" ServiceMethod="GetCompletionList" CompletionListCssClass="AutoExtender"
         CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                     
         </ajax:AutoCompleteExtender> 
                              
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
     Tax Master  
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
 <asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
   <ContentTemplate >
     <table  style="width: 100%">
       <tr>
         <td align="center" >
          <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
            <ProgressTemplate>            
            <div id="progressBackgroundFilter"></div>
               <div id="processMessage">   
               <center><span class="SubTitle">Loading....!!! </span></center>
               <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" />                                
             </div> 
            </ProgressTemplate>
 </asp:UpdateProgress>
</td>
  </tr>
  </table>
  
  <table width="100%">
       <tr>
     <td>
   <fieldset id="fieldset1"  width="100%" runat="server" class="FieldSet">

    <table width="100%" cellspacing="6">
    
      <tr>
           
                <td colspan="3" align="center" class="Display_None">
        
                <asp:RadioButtonList ID="RdoTaxType" runat="server"
                CellPadding="25"  RepeatDirection="Horizontal" CssClass="RadioButton">
                <asp:ListItem Selected="True" Text="Service Tax&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" 
                Value="1"></asp:ListItem>
                <asp:ListItem Text="VAT" 
                Value="2"></asp:ListItem>
                </asp:RadioButtonList>
        
                </td>
        </tr>
        
        <tr>
            <td class="Label">
                GST Name :
            </td>
            
            <td align="left">
               <asp:TextBox ID="TxtTaxName" runat="server" CssClass="TextBox" 
                 MaxLength="150" Width="350px"></asp:TextBox>
            </td>
            
            <td>
                <asp:RequiredFieldValidator ID="Req1" runat="server" 
                    ControlToValidate="TxtTaxName" Display="None" 
                    ErrorMessage="Tax Name is Required" SetFocusOnError="True" 
                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
                    Enabled="True" TargetControlID="Req1" 
                    WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td class="Label">
                GST (%) :
            </td>
            
            <td align="left">
               <asp:TextBox ID="TxtTaxPer" runat="server" CssClass="TextBox" ToolTip="This is current Tax(%)"
                 MaxLength="50" Width="150px"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TxtTaxPer"
                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>
            </td>
            
            <td>
               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="TxtTaxPer" Display="None" 
                    ErrorMessage="Tax Percentage is Required" SetFocusOnError="True" 
                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                    Enabled="True" TargetControlID="RequiredFieldValidator1" 
                    WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>--%>
                
           
                                                                                                    
            </td>
        </tr>
         <tr>
            <td class="Label">
               New GST% App Date.:
            </td>
            
            <td align="left">
             <%--  <asp:TextBox ID="TXTEFFECTIVEDATE" runat="server" CssClass="TextBox" 
                 MaxLength="50" Width="150px"></asp:TextBox>
            
               <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
               PopupButtonID="TXTEFFECTIVEDATE" TargetControlID="TXTEFFECTIVEDATE" />--%>
               
                <asp:TextBox ID="TXTEFFECTIVEDATE" runat="server" CssClass="TextBox" TabIndex="4" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFV1" runat="server" ErrorMessage="New GST% Declaration Date Required"
                                ControlToValidate="TXTEFFECTIVEDATE" Display="None" SetFocusOnError="True"
                                ValidationGroup="AddGrid"></asp:RequiredFieldValidator>
                            <ajax:ValidatorCalloutExtender ID="VCE1" runat="server" TargetControlID="RFV1"
                                WarningIconImageUrl="~/Images/Icon/Warning.png" Enabled="True">
                            </ajax:ValidatorCalloutExtender>
                            <ajax:CalendarExtender ID="CalendarExtender12" runat="server" Format="dd MMM yyyy" PopupButtonID="ImgBtnDecDate" TargetControlID="TXTEFFECTIVEDATE" />
                            <asp:ImageButton ID="ImgBtnDecDate" runat="server" CausesValidation="False" CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" TabIndex="5" />

                            <asp:ImageButton ID="ImgBtnAdd" runat="server" OnClick="ImgBtnAdd_Click"
                                CssClass="Imagebutton" ImageUrl="~/Images/Icon/Gridadd.png"
                                TabIndex="8" ValidationGroup="AddGrid" />
                                                    
            </td>
            
            <td>
             
            </td>
        </tr>
        
         <tr>
                        <td colspan="3">
                            
                              <%--  <asp:GridView ID="GridDetails" runat="server" AllowPaging="false"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid"
                                    GridLines="None"
                                    PagerStyle-CssClass="pgr" Width="100%" OnRowCommand="GridDetails_RowCommand"
                                    OnRowDeleting="GridDetails_RowDeleting">--%>
                                    <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderStyle="None" BorderWidth="1px" CssClass="mGrid" Font-Bold="False" ForeColor="Black"
                GridLines="Horizontal" ToolTip="Last Updated Records" OnRowCommand="GridDetails_RowCommand"
                                    OnRowDeleting="GridDetails_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                                    CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Select"
                                                    ImageUrl="~/Images/Icon/GridEdit.png" TabIndex="1" ToolTip="Edit Record" />
                                                <asp:ImageButton ID="ImgBtnDelete" runat="server" CausesValidation="False" CssClass="Imagebutton"
                                                    CommandArgument='<%# Eval("#") %>' CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png"
                                                    ToolTip="Delete Record" TabIndex="1" />
                                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Would You Like To Delete This Record?"
                                                    TargetControlID="ImgBtnDelete">
                                                </ajax:ConfirmButtonExtender>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="GSTPer" HeaderText="GST (%)">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NewGSTApplicableDate" HeaderText="New GST Applicable Date">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="true" />
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
 <asp:BoundField DataField="GSTPer" HeaderText="GST (%)">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                        
                        </td>
                    </tr>
        
        
       <tr>
          <td colspan="3">
                <asp:GridView ID="Grd_Tax" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderStyle="None" BorderWidth="1px" CssClass="mGrid" Font-Bold="False" ForeColor="Black"
                GridLines="Horizontal" ToolTip="Last Updated Records">
                <Columns>
                <%--<asp:TemplateField HeaderText="Sr. No.">                        
                <ItemTemplate>
                <asp:Label ID="LblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" 
                Width="6%" />
                </asp:TemplateField>
                         
                <asp:TemplateField HeaderText="#" Visible="False">
                <ItemTemplate>
                <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField HeaderText="From" DataField="From"></asp:BoundField>
                <asp:BoundField HeaderText="To" DataField="To"></asp:BoundField>
                <asp:BoundField HeaderText="Tax(%)" DataField="Taxper"></asp:BoundField>--%>
                </Columns>
                </asp:GridView>
          </td>
        </tr>
    </table>
 </fieldset>
 </td>
     </tr> 
     
     
       <tr> 
     <td>
 <fieldset id="fieldset2" runat="server" class="FieldSet">
 <table width="100%">
    <tr>
         <td align="center" >
                <table  align="center" width="25%" >
                    <tr>
                        <td>
                  <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="button" TabIndex="1" 
                     ValidationGroup="Add" onclick="BtnUpdate_Click" />
                  <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                    ConfirmText="Would You Like To Update the Record ..! "
                    TargetControlID="BtnUpdate" >
                  </ajax:ConfirmButtonExtender>
                        </td>
                        
                        <td>
                           <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" 
                           TabIndex="1" ValidationGroup="Add" onclick="BtnSave_Click" />
                           
                        </td>
                        
                        <td>
                          <asp:Button ID="BtnDelete" runat="server" CssClass="Display_None" Text="Delete" 
                                ValidationGroup="Add" onclick="BtnDelete_Click"  TabIndex="1" />
                        </td>
                        
                        <td>
                           <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="button" 
                            TabIndex="1" CausesValidation="False" onclick="BtnCancel_Click" />
                        </td>
                        
                        
                    </tr>
                </table>
         </td>
        </tr>
 </table></fieldset>
 </td>
 
 </tr>
    </table>
  </ContentTemplate>
</asp:UpdatePanel>   
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ReportTitle" Runat="Server">
    Tax Template List  
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Report" Runat="Server">                 
 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate >
    <div class="ScrollableDiv_FixHeightWidthForRepeater">
    <ul id="subnav">
            <%--Ul Li Problem Solved repeater--%>
            <asp:Repeater ID="GrdReport" runat="server" 
                onitemcommand="GrdReport_ItemCommand">    
            <ItemTemplate>
            <li id="Menuitem" runat="server" >                              
              <asp:LinkButton ID="lbtn_List" CssClass="linkButton" CausesValidation="false" TabIndex="4"
                CommandName="Select" CommandArgument='<%# Eval("#") %>' runat="server"
                 Text='<%# Eval("Name") %>'>
              </asp:LinkButton>
                                 
            </li>
            </ItemTemplate>    
            </asp:Repeater>
    </ul>
      </div>
       </ContentTemplate>
 </asp:UpdatePanel>                  
</asp:Content>



