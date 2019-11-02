<%@ Page Title="Project Master " Language="C#" MasterPageFile="~/MasterPages/MasterPage_RN.master" AutoEventWireup="true" CodeFile="Project_master.aspx.cs" Inherits="Masters_Project_master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
    <input type="hidden" id="hiddenbox" runat="server" value=""/>



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
  

 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
        Project  Master     
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
              Select   Customer :
            </td>
            
            <td align="left">
            <asp:DropDownList ID="ddlcustomer" runat="server" CssClass="ComboBox" AutoPostBack="true"
            Width="300px">
            </asp:DropDownList>
            </td>
            
            <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ControlToValidate="ddlcustomer" Display="None"
            ErrorMessage="Customer Is Required" SetFocusOnError="True"
            ValidationGroup="Add">
            </asp:RequiredFieldValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
            Enabled="True" TargetControlID="RequiredFieldValidator1"
            WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender>
            </td>
        </tr>
     <tr>
         <td class="Label">
                 Project No :
         </td>

         <td align="left>
               <asp:Label ID="lblprojectno" runat="server" CssClass="TextBox"  Enabled="false" Width="300px"></asp:Label>
         </td>
     </tr>

      <tr>
         <td class="Label">
               Unique No :
         </td>

         <td align="left>
               <asp:Label ID="lblunique" runat="server" Enabled="false" CssClass="TextBox"  Width="300px"></asp:Label>
         </td>
     </tr>

      <tr>
            <td class="Label">
                Project Name :
            </td>
            
            <td align="left">
           <asp:TextBox ID="txtproject" runat="server" CssClass="TextBox"  Width="300px"></asp:TextBox>
            </td> 
            
            <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
            ControlToValidate="txtproject" Display="None"
            ErrorMessage="Project Name  Is Required" SetFocusOnError="True"
            ValidationGroup="Add">
            </asp:RequiredFieldValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
            Enabled="True" TargetControlID="RequiredFieldValidator3"
            WarningIconImageUrl="~/Images/Icon/Warning.png">
            </ajax:ValidatorCalloutExtender>
            </td>
        </tr>



        <tr>
            <td class="Label">
                Comapany :
            </td>
            
            <td align="left">
            <asp:DropDownList ID="ddlcompany" runat="server" Width="300px" CssClass="TextBox"></asp:DropDownList>
            </td>
            
          
        </tr>
        
     
        
       <tr>
            <td class="Label" colspan="1">
                   Project Start Date :
                 </td>  
                 <td>
                     <table>
                         <tr>
                             <td>
                                 <asp:TextBox ID="txtprojectstartdate" runat="server" CssClass="TextBox" MaxLength="13" 
                                     Width="120px">
                                 </asp:TextBox>
                                 <ajax:CalendarExtender ID="txtcalender1" runat="server" TargetControlID="txtprojectstartdate" Format="dd-MM-yyyy"></ajax:CalendarExtender>
                                
                             </td>
                             <td class="Label">
                                Project End Date :
                             </td>
                             <td>
                                 <asp:TextBox ID="txtprojectenddate" runat="server" CssClass="TextBox" MaxLength="13" 
                                     Width="120px">
                                 </asp:TextBox>
                                  <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtprojectenddate" Format="dd-MM-yyyy"></ajax:CalendarExtender>
                              
                                 
                             </td>
                         </tr>
                     </table>                            
                 </td>
           
        </tr>
            
            
        
        
      
        <tr>
            
                 <td colspan="2">
                     </td>
                 <tr>
                     <td class="Label" style="height: 20px">Contact Person :</td>
                     <td style="height: 20px">
                         <asp:TextBox ID="txtcontactpersonname" runat="server" CssClass="TextBox" Width="300px"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td class="Label">Contact No : </td>
                     <td>
                         <asp:TextBox ID="TxtContactNo" runat="server" CssClass="TextBox" MaxLength="15" Width="300px"></asp:TextBox>
                         <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers" TargetControlID="TxtContactNo" ValidChars="+">
                         </ajax:FilteredTextBoxExtender>
                     </td>
                 </tr>
                 <tr>
                     <td class="Label">Mail Id :</td>
                     <td>
                         <asp:TextBox ID="TxtPEmail" runat="server" CssClass="TextBox" MaxLength="50" Width="300px"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="REV2" runat="server" ControlToValidate="TxtPEmail" Display="None" ErrorMessage="Please Enter Valid Email ID..!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Add">
                        </asp:RegularExpressionValidator>
                         <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="REV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                         </ajax:ValidatorCalloutExtender>
                     </td>
                 </tr>
                 <tr>
                     <td class="Label">Remark : </td>
                     <td>
                         <asp:TextBox ID="txtremark" runat="server" CssClass="TextBox" MaxLength="15" TextMode="MultiLine" Width="300px"></asp:TextBox>
                         <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers" TargetControlID="TxtContactNo" ValidChars="+">
                         </ajax:FilteredTextBoxExtender>
                     </td>
                 </tr>
                 <tr>
                     <td colspan="2"></td>
                 </tr>
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
            ValidationGroup="Add" OnClick="BtnUpdate_Click"  />
            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server"
            ConfirmText="Would You Want To Upadte The Record ?" 
            TargetControlID="BtnUpdate">
            </ajax:ConfirmButtonExtender> 
            </td>
            
            <td>
            <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button"
            ValidationGroup="Add" OnClick="BtnSave_Click"  />
            </td>
            
            <td>
            <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="button"
            ValidationGroup="Add" OnClick="BtnDelete_Click"  />
            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server"
            ConfirmText="Would You Want To Delete This Record ?"
            TargetControlID="BtnDelete">
            </ajax:ConfirmButtonExtender>
            </td> 
            
            <td>
            <asp:Button ID="BtnCancel" runat="server" Text="Cancel"
            CssClass="button" CausesValidation="False" />
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
     Project  List     
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
                 Text='<%# Eval("ProjectName") %>'>
              </asp:LinkButton>
                                 
            </li>
            </ItemTemplate>    
            </asp:Repeater>
    </ul>
         </div> 
       </ContentTemplate>
 </asp:UpdatePanel>   
</asp:Content>

