<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="StockLocationMaster.aspx.cs" Inherits="Masters_StockLocationMaster" Title="Stock Location" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<input type="hidden" id="hiddenbox" runat="server" value=""/>
<script type="text/javascript" language="javascript">

function DeleteEquipFunction()
 { 
        var gridViewCtlId = '<%=GridDetails.ClientID%>';
        var ctlGridViewItems = null;
        var ItemID;
        ctlGridViewItems = document.getElementById(gridViewCtlId);
        ItemID =  ctlGridViewItems.rows[1].cells[2].innerText;        
        if(ItemID==0)
        {        
        if(confirm("There is no record to delete")==true)
        {
        document.getElementById('<%= hiddenbox.ClientID%>').value="0"; 
        }
        else
        {
         document.getElementById('<%= hiddenbox.ClientID%>').value="0"; 
        }
        }
        else
        {
        if(confirm("Are you sure you want to delete?")==true)
        {
        document.getElementById('<%= hiddenbox.ClientID%>').value="1";
        return true;
        }
        else
        {
         document.getElementById('<%= hiddenbox.ClientID%>').value="0";
         return false;
         }
        }
}
</script>


<ajax:ToolkitScriptManager ID="ToolScriptManager" runat="server" />
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
  
      Search For Site :
    <asp:TextBox ID="TxtSearch" runat="server" CssClass="search" 
          ToolTip="Enter The Text" Width="292px" AutoPostBack="True" 
          ontextchanged="TxtSearch_TextChanged">
     </asp:TextBox>

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
    Site Master     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
<asp:UpdatePanel ID="AjaxPanelUpdateEntry" runat="server">
<ContentTemplate>
    <table style="width: 100%">
        <tr>
            
                 <td align="center" >
          <asp:UpdateProgress ID="UpdateProgress2" runat="server" >
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
 <fieldset id="fieldset" runat="server" class="FieldSet">
 <table width="100%" cellspacing="6">
        <tr>
            <td class="Label">
            </td>
            <td align="left">
            </td>
                <td>
                </td>
        </tr>
        <tr>
            <td class="Label">
                Comapany :
            </td>
            
            <td align="left">
            <asp:DropDownList ID="ddlcompany" runat="server" Width="300px" CssClass="TextBox"></asp:DropDownList>
            </td>
            
            <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ControlToValidate="TxtStockLocation" Display="None"
            ErrorMessage="Stock Location Is Required" SetFocusOnError="True"
            ValidationGroup="Add">
            </asp:RequiredFieldValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
            Enabled="True" TargetControlID="Req1"
            WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        
        
        <tr>
            <td class="Label">
                Site :
            </td>
            
            <td align="left">
            <asp:TextBox ID="TxtStockLocation" runat="server" CssClass="TextBox"
            Width="300px">
            </asp:TextBox>
            </td>
            
            <td>
            <asp:RequiredFieldValidator ID="Req1" runat="server"
            ControlToValidate="TxtStockLocation" Display="None"
            ErrorMessage="Stock Location Is Required" SetFocusOnError="True"
            ValidationGroup="Add">
            </asp:RequiredFieldValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
            Enabled="True" TargetControlID="Req1"
            WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        
        <tr>
           <td class="Label">
                Abbreviation :
            </td>
            <td colspan="2">
                <asp:TextBox ID="Txtabbreviations" runat="server" CssClass="TextBox" ToolTip="Short Name For Site Which Bind With Transaction Number"
                    MaxLength="5" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="Txtabbreviations" Display="None" 
                    ErrorMessage="Abbreviations is Required For Generating Code!" SetFocusOnError="True" 
                    ValidationGroup="Add">
                </asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" 
                    TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>
            </td>
            
            </tr>
            
            
        <tr>
        <td class="Label">Address :</td>
        <td colspan="2"> 
            <asp:TextBox ID="TxtSiteAddr" runat="server" CssClass="TextBox" TextMode="MultiLine"
            Width="300px">
            </asp:TextBox>
        </td>
        <td></td>
        </tr>
        
        <tr>
            <td class="Label"> Is Central Site :
            </td>
            <td align="left">
                <asp:CheckBox ID="chkCenLoc" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        
        
        <tr>
            <td colspan="3">
               <fieldset id="fieldset3"  width="100%" runat="server" class="FieldSet">
               <legend><b>Contact Person Details</b></legend>
                <table width="100%">
                 <tr>
                 <td class="Label">
                     Contact Person Name :</td>
                 <td>
                <asp:DropDownList ID="ddlPersonName" runat="server" Width="300px" CssClass="ComboBox"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RFV2" runat="server" 
                ControlToValidate="ddlPersonName" Display="None" 
                ErrorMessage="Person Name is Required" SetFocusOnError="True" 
                ValidationGroup="AddGrid" InitialValue="0"></asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="VCE1" runat="server" Enabled="True" 
                TargetControlID="RFV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                </ajax:ValidatorCalloutExtender>
                 </td>
                 </tr>
                 <tr>
                 <td class="Label">
                    Contact No : 
                 </td>
                 <td>
                    <asp:TextBox ID="TxtContactNo" runat="server" CssClass="TextBox" MaxLength=15
                    Width="300px"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TxtContactNo"
                    FilterType="Custom,Numbers" ValidChars="+"></ajax:FilteredTextBoxExtender>
                 </td>
                 </tr>
                    <tr>
                        <td class="Label">
                            Mail Id :</td>
                        <td>
                        <asp:TextBox ID="TxtPEmail" runat="server" CssClass="TextBox" Width="300px" MaxLength=50></asp:TextBox>
                        <asp:RegularExpressionValidator ID="REV2" runat="server" Display="None" 
                        ErrorMessage="Please Enter Valid Email ID..!" ControlToValidate="TxtPEmail" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Add">
                        </asp:RegularExpressionValidator>
                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" 
                        Enabled="True" TargetControlID="REV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                        </ajax:ValidatorCalloutExtender>  
                        </td>
                    </tr>
                    <tr>
                    <td class="Label">Address:</td>
                    <td>
                     <asp:TextBox ID="TxtAddress" runat="server" CssClass="TextBox" Width="300px" TextMode="MultiLine"></asp:TextBox>
                     <asp:ImageButton ID="ImgAddGrid" runat="server" 
                        ImageUrl="~/Images/Icon/Gridadd.png" onclick="ImgAddGrid_Click" 
                        ToolTip="Add Grid" ValidationGroup="AddGrid" />
                    </td>
                    </tr>
                 
                 <tr>
<td colspan="2">
<asp:UpdatePanel ID="UpdatePanel6" runat="server">
<ContentTemplate>
   <table width="100%" cellspacing="10">
     <tr>
       <td>
         <div class="scrollableDiv">
          <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" 
          BackColor="White" BorderStyle="None" BorderWidth="1px" CssClass="mGrid" 
          Font-Bold="False" ForeColor="Black" GridLines="Horizontal" 
                 onrowcommand="GridDetails_RowCommand" onrowdeleting="GridDetails_RowDeleting">
          <Columns>
              <asp:TemplateField HeaderText="#" Visible="False">
                  <ItemTemplate>
                      <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
                  <ItemTemplate>
                      <asp:ImageButton ID="ImageGridEdit" runat="server" 
                          CommandArgument="<%#((GridViewRow)Container).RowIndex %>" 
                          CommandName="SelectGrid" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                      <asp:ImageButton ID="ImageBtnDelete" runat="server" 
                          CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="Delete" OnClientClick="DeleteEquipFunction();" 
                          ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                  </ItemTemplate>
                  <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  <HeaderStyle Wrap="False" />
                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
              </asp:TemplateField>
              <asp:BoundField DataField="EmployeeId" HeaderText="EmployeeId" >
               <HeaderStyle Wrap="False" CssClass="Display_None"  />
                  <ItemStyle Wrap="False" CssClass="Display_None" />
                  </asp:BoundField>
              <asp:BoundField HeaderText="Contact Person Name" DataField="PersonName">
                  <HeaderStyle Wrap="false" />
                  <ItemStyle Wrap="false" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Contact No" DataField="ContactNo" >
              </asp:BoundField>
              <asp:BoundField HeaderText="Mail Id" DataField="EmailId">
                  <HeaderStyle Wrap="false" />
                  <ItemStyle Wrap="false" />
              </asp:BoundField>
              <asp:BoundField HeaderText="Address" DataField="Address">
                  <HeaderStyle Wrap="false" />
                  <ItemStyle Wrap="false" />
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
    </fieldset>
                </td>
        </tr>
        <tr>
            <td class="Label">
                &nbsp;</td>
            <td align="left">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td class="Label">
                &nbsp;</td>
            <td align="left">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        </table>
               </fieldset>
    </td>
     </tr>
     
          <tr>
           <td>  
           <fieldset id="fieldset1" runat="server" class="FieldSet">
            <table width="100%">
                 <tr>
                 <td align="center">
                 <table align="center" width="25%">
              
                 <tr>
            <td>
            <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="button"
            ValidationGroup="Add" onclick="BtnUpdate_Click" />
            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server"
            ConfirmText="Would You Want To Upadte The Record ?" 
            TargetControlID="BtnUpdate">
            </ajax:ConfirmButtonExtender> 
            </td>
            
            <td>
            <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button"
            ValidationGroup="Add" onclick="BtnSave_Click" />
            </td>
            
            <td>
            <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="button"
            ValidationGroup="Add" onclick="BtnDelete_Click" />
            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server"
            ConfirmText="Would You Want To Delete This Record ?"
            TargetControlID="BtnDelete">
            </ajax:ConfirmButtonExtender>
            </td> 
            
            <td>
            <asp:Button ID="BtnCancel" runat="server" Text="Cancel"
            CssClass="button" CausesValidation="False" onclick="BtnCancel_Click"/>
            </td>
            
        </tr>
                 
                 
               
                  </table>
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
<asp:Content ID="Content4" ContentPlaceHolderID="ReportTitle" Runat="Server">
    Site List     
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
              <asp:LinkButton ID="lbtn_List" CssClass="linkButton" CausesValidation="false"
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

