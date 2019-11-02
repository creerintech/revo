<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="AssignStock.aspx.cs" Inherits="Transactions_AssignStock" Title="Material Issue Register" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
<script type="text/javascript" language="javascript"> 
        function check()
        {
            var rb1=document.getElementById('<%= RdoType.ClientID %>');
            var rb2=rb1.getElementsByTagName('input');    
            if(rb2[0].checked)
            {
            document.getElementById('<%= TRINW.ClientID %>').style.visibility = "hidden";
            document.getElementById('<%= TRCATEGORY.ClientID %>').style.visibility = "visible";

            }
            else 
            {
            document.getElementById('<%= TRCATEGORY.ClientID %>').style.visibility = "visible";
            document.getElementById('<%= TRINW.ClientID %>').style.visibility = "hidden";

            }
        }


        function SETVALUE(objGrid) {
            var _GridDetails = document.getElementById('<%= GRDCATEGORYITEM.ClientID %>');
            var rowIndex = objGrid.offsetParent.parentNode.rowIndex;
            var ddlsupp = (_GridDetails.rows[rowIndex].cells[6].children[0]);
            if (ddlsupp.value == 0) {
                suppId.value = 0;
                alert("Please select Supplier");
            }

        }

        function ValueOfDDL(objGrid) {
          
            var _GridDetails = document.getElementById('<%= GridDetails1.ClientID %>');
            var rowIndex = objGrid.offsetParent.parentNode.rowIndex;
            var DDLSUPLIER = (_GridDetails.rows[rowIndex].cells[15].children[0]);

            var ORDQTY = (objGrid.options[objGrid.selectedIndex].text);
           
            if (ORDQTY.length > 2) {
                var n = ORDQTY.indexOf("-");
                ORDQTY = ORDQTY.substring(n+2, ORDQTY.length);
            }
            DDLSUPLIER.value = ORDQTY;
            
        }
        
        </script>
<ajax:ToolkitScriptManager ID="ToolScriptManager1" runat="server">
</ajax:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
    Search for Issue: 
                    <asp:TextBox ID="TxtSearch" runat="server" 
                    CssClass="search" ToolTip="Enter The Text"
                    Width="292px" AutoPostBack="True" ontextchanged="TxtSearch_TextChanged">
                    </asp:TextBox>
                    <div id="divwidth"></div>
                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender1"   runat="server" 
                    TargetControlID="TxtSearch"    CompletionInterval="100"                               
                    UseContextKey="True" FirstRowSelected ="true" 
                    ShowOnlyCurrentWordInCompletionListItem="true"  
                    ServiceMethod="GetCompletionList"
                    CompletionListCssClass="AutoExtender"
                    CompletionListItemCssClass="AutoExtenderList"
                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight">                        
                    </ajax:AutoCompleteExtender> 
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    Material Issue Register        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
  <table width="100%">
   <tr>
  <td>
<fieldset class="FieldSet" id="FieldSet4" runat="server">
<legend class="legend">Stock Details</legend>
  
    <table width="100%" cellspacing="6" id="T1">
        
        <tr>
            <td class="Label">
                Issue No :</td>
            <td>
               <asp:Label ID="TxtStockNo" runat="server" CssClass="Label" Font-Bold="true"></asp:Label>
               </td>
            <td class="Label">
                Issue Date :
                </td>                
            <td>
                <asp:TextBox ID="TxtStockDate" runat="server" Width="80px" CssClass="TextBox"></asp:TextBox>                
                <asp:ImageButton ID="ImageDate" runat="server" CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />                
                <ajax:CalendarExtender ID="CalDate" runat="server" Format="dd-MMM-yyyy" TargetControlID="TXtStockDate" PopupButtonID="ImageDate">
                </ajax:CalendarExtender>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chk_close" runat="server" CssClass="CheckBox" />
                <label ID="lblclose" class="Label" runat="server">Close Requisition</label>
           </td>
        </tr>        
        <tr>
            <td class="Label" align="right">
                Consumption Date :</td>
            <td>
                <asp:TextBox ID="TxtConsumptionDate" runat="server" CssClass="TextBox" 
                    Width="80px"></asp:TextBox>
                <ajax:CalendarExtender ID="CalendarExtender3" runat="server" 
                    Format="dd-MMM-yyyy" PopupButtonID="ImagCDate" 
                    TargetControlID="TxtConsumptionDate">
                </ajax:CalendarExtender>
                <asp:ImageButton ID="ImagCDate" runat="server" CausesValidation="false" 
                    CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
            </td>
            <td class="Label">
                &nbsp;</td>
            <td>
                <asp:Label ID="LblNo" runat="server" CssClass="Label_Dynamic"></asp:Label>
            </td>
        </tr>        
        <tr>
            <td align="right" class="Label">
                &nbsp;</td>
            <td align="left" class="Label">
                <asp:RadioButtonList ID="RdoType" runat="server" AutoPostBack="True" 
                    CellPadding="25"  
                    onselectedindexchanged="RdoType_SelectedIndexChanged" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Text="Inward&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" 
                        Value="IW"></asp:ListItem>
                    <asp:ListItem Text="ItemWise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" Value="I"></asp:ListItem>
                    <asp:ListItem Text="RequisitionWise" Value="RQ"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="Label">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
 
       
        <tr id="TRINW" runat="server">
        
         <td class="Label"> 
              From Date :
            </td>
            <td style="width: 373px">
             <asp:TextBox runat="server" Width="80px" CssClass="TextBox" ID="TxtFromDate"></asp:TextBox>
                <asp:ImageButton ID="ImageFromDate" runat="server" CausesValidation="false"
                CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                PopupButtonID="ImageFromDate" TargetControlID="TxtFromDate">
                </ajax:CalendarExtender>&nbsp;&nbsp;&nbsp;&nbsp; To Date :     
               <asp:TextBox runat="server" Width="80px" CssClass="TextBox" ID="TxtToDate"></asp:TextBox>
                <asp:ImageButton ID="ImageToDate" runat="server" CausesValidation="false"
                CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                PopupButtonID="ImageToDate" TargetControlID="TxtToDate">
                </ajax:CalendarExtender>        
            </td>
            <td class="Label"> 
                Inward No :</td>
            <td>
<%--<asp:DropDownList ID="DDLINV" runat="server" CssClass="ComboBox" Width="190px"></asp:DropDownList>--%>
<ajax:ComboBox ID="DDLINV" runat="server" DropDownStyle="DropDown" 
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="180px" CssClass="CustomComboBoxStyle" >
</ajax:ComboBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="DDLINV" Display="None" 
                    ErrorMessage="Please Select Inward No. For Issue" InitialValue="0" 
                    ValidationGroup="AddGrid"></asp:RequiredFieldValidator>
           <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" 
                  TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                 </ajax:ValidatorCalloutExtender> 
                 &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="BtnShow" runat="server" Text="Show" CssClass="button" 
                    onclick="BtnShow_Click"  ValidationGroup="AddGrid" ToolTip="Show Items In Inward" />   
            </td>
               
        
        </tr>
        <tr id="TRCATEGORY" runat="server">
            <td class="Label" >
                Category :</td>
            <td >
              <%-- <asp:DropDownList ID="DDLCATEGORY" CssClass="ComboBox" runat="server" 
                    Width="190px" AutoPostBack="True" 
                    onselectedindexchanged="DDLCATEGORY_SelectedIndexChanged">
               </asp:DropDownList>--%>
               
<ajax:ComboBox ID="DDLCATEGORY" runat="server" DropDownStyle="DropDown" 
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" AutoPostBack="True" 
                    onselectedindexchanged="DDLCATEGORY_SelectedIndexChanged"
ItemInsertLocation="Append" Width="180px" CssClass="CustomComboBoxStyle" >
</ajax:ComboBox>

                <asp:RequiredFieldValidator ID="RFV1" runat="server" 
                    ControlToValidate="DDLCATEGORY" Display="None" 
                    ErrorMessage="Please Select Category" InitialValue="0" 
                    ValidationGroup="AddGridCategory"></asp:RequiredFieldValidator>
                &nbsp;&nbsp;&nbsp;&nbsp;
           <ajax:ValidatorCalloutExtender ID="VCE1" runat="server" Enabled="True" 
                  TargetControlID="RFV1" WarningIconImageUrl="~/Images/Icon/Warning.png">
                 </ajax:ValidatorCalloutExtender>    
                 
                 <asp:Button ID="BtnShowCateGory" runat="server" Text="Add" CssClass="Display_None" 
                   ValidationGroup="AddGridCategory" ToolTip="Add All Item To Grid" 
                    onclick="BtnShowCateGory_Click" /> 
                    
                    Sub Category :
                         <ajax:ComboBox ID="ddlSubcategory" runat="server" DropDownStyle="DropDown" 
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" AutoPostBack="True" 
                    onselectedindexchanged="ddlSubcategory_SelectedIndexChanged"
ItemInsertLocation="Append" Width="180px" CssClass="CustomComboBoxStyle" >
</ajax:ComboBox>
   
            </td>
            <td class="Label" >
                Items :
            </td>
            <td align="left">
           

                 
                 <%--THIS START HERE--%>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                <asp:TextBox ID="TxtItemName" runat="server" 
                CssClass="search_List" Width="200px" AutoPostBack="True" ontextchanged="TxtItemName_TextChanged" ></asp:TextBox>

                <ajax:AutoCompleteExtender ID="AutoCompleteExtenderItemName"   runat="server" 
                TargetControlID="TxtItemName"    CompletionInterval="100"                               
                UseContextKey="True" FirstRowSelected ="true" 
                CompletionSetCount="20" 
                ShowOnlyCurrentWordInCompletionListItem="true"  
                ServiceMethod="GetCompletionItemNameList"
                CompletionListCssClass="AutoExtender"
                CompletionListItemCssClass="AutoExtenderList"
                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"                         
                ></ajax:AutoCompleteExtender> 

                <ajax:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="TxtItemName" 
                WatermarkText="Type Item Name" WatermarkCssClass="water" />
                </ContentTemplate>
                </asp:UpdatePanel >
                <%--THIS END HERE--%>
                 
            
            </td>
        </tr>
        <tr id="TRDiscription" runat="server">
        <td class="Label"> Description :</td>
        <td>                     
            <ajax:ComboBox ID="ddldesc" runat="server" AutoCompleteMode="SuggestAppend" 
                        CaseSensitive="false" CssClass="CustomComboBoxStyle" DropDownStyle="DropDown" 
                        ItemInsertLocation="Append" RenderMode="Inline" 
    Width="180px">
                    </ajax:ComboBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="ddldesc" Display="None" 
                    ErrorMessage="Please Select Description" InitialValue=" --Select ItemDescription--" 
                    ValidationGroup="AddGridItem"></asp:RequiredFieldValidator>
                &nbsp;&nbsp;&nbsp;&nbsp;
                 <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" 
                  TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                 </ajax:ValidatorCalloutExtender> 
                    <asp:ImageButton ID="BtnShowItems" runat="server" 
                CssClass="Imagebutton" Height="20px" ImageUrl="~/Images/Icon/Gridadd.png" 
                onclick="BtnShowItems_Click" ToolTip="Add Particular Item To Grid" 
                ValidationGroup="AddGridItem" Width="20px" />
                    </td>
        <td align="left">                    &nbsp;</td>
        <td>  <ajax:ComboBox ID="DDLITEMS" runat="server" DropDownStyle="DropDown" 
                AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" Visible="false"
                ItemInsertLocation="Append" Width="180px" CssClass="CustomComboBoxStyle" AutoPostBack="True" 
                onselectedindexchanged="DDLITEMS_SelectedIndexChanged" >
                </ajax:ComboBox>
                 <asp:RequiredFieldValidator ID="RFV2" runat="server" 
                    ControlToValidate="DDLITEMS" Display="None" 
                    ErrorMessage="Please Select Item" InitialValue="0" 
                    ValidationGroup="AddGridItem"></asp:RequiredFieldValidator>
                &nbsp;&nbsp;&nbsp;&nbsp;
                 <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" 
                  TargetControlID="RFV2" WarningIconImageUrl="~/Images/Icon/Warning.png">
                 </ajax:ValidatorCalloutExtender> 
                 
                 </td>
        </tr>
        
        <tr id="TrReq" runat="server">
         <td class="Label">From Date : </td>
         <td style="width: 373px"> 
             <asp:TextBox ID="TxtFromDateR" runat="server" CssClass="TextBox" Width="80px" ></asp:TextBox>
             <ajax:CalendarExtender ID="TxtFromDateR_CalendarExtender" runat="server" 
                 Format="dd-MMM-yyyy" PopupButtonID="ImageFromDate" 
                 TargetControlID="TxtFromDateR">
             </ajax:CalendarExtender>
             <asp:ImageButton ID="ImageFromDate0" runat="server" CausesValidation="false" 
                 CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
             &nbsp;&nbsp;&nbsp;&nbsp; To Date :
             <asp:TextBox ID="TxtToDateR" runat="server" CssClass="TextBox" Width="80px"></asp:TextBox>
             <ajax:CalendarExtender ID="TxtToDateR_CalendarExtender" runat="server" 
                 Format="dd-MMM-yyyy" PopupButtonID="ImageToDate" TargetControlID="TxtToDateR">
             </ajax:CalendarExtender>
             <asp:ImageButton ID="ImageToDate0" runat="server" CausesValidation="false" 
                 CssClass="Imagebutton" ImageUrl="~/Images/Icon/DateSelector.png" />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:ImageButton ID="IMREFRESH" runat="server" CausesValidation="false" 
                 CssClass="Imagebutton" ImageUrl="~/Images/Icon/GridUpdate.png" ToolTip="Get Requisition Of This Date"
                 onclick="IMREFRESH_Click" />
                 
         </td>
         <td class="Label">Requisition No:</td>
         <td>
            <%-- <asp:DropDownList ID="ddlRequisitnNo" runat="server" CssClass="ComboBox" 
                 Width="250px">
             </asp:DropDownList>--%>
             
<ajax:ComboBox ID="ddlRequisitnNo" runat="server" DropDownStyle="DropDown" 
AutoCompleteMode="SuggestAppend" CaseSensitive="false" RenderMode="Inline" 
ItemInsertLocation="Append" Width="240px" CssClass="CustomComboBoxStyle" >
</ajax:ComboBox>

              <asp:RequiredFieldValidator ID="RFV3" runat="server" 
                 ControlToValidate="ddlRequisitnNo" Display="None" 
                 ErrorMessage="Please Select Item" InitialValue="0" 
                 ValidationGroup="AddGridItem"></asp:RequiredFieldValidator>
                 <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" 
                 Enabled="True" TargetControlID="RFV3" 
                 WarningIconImageUrl="~/Images/Icon/Warning.png">
             </ajax:ValidatorCalloutExtender>
             &nbsp;&nbsp;<asp:Label ID="lblRequisitionNo" runat="server" CssClass="Label_Dynamic" 
                 Text="LblRequisitionNo"></asp:Label><asp:Button ID="BtnShowRequisitn" runat="server" CssClass="button" Text="Show" 
                 ToolTip="ShowGrid" ValidationGroup="AddGrid" 
                 onclick="BtnShowRequisitn_Click1" /> 
                
             &nbsp;</td>
        </tr>
            <tr runat="server" id="TRGRIDINWARD">
                <td colspan="4">                
              
                <div class="ScrollableDiv_FixHeightWidth5">                 
                    <asp:GridView ID="GridDetails1" runat="server" AutoGenerateColumns="False" 
                        CssClass="mGrid" onrowdatabound="GridDetails1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="LblEntryId" runat="server" Text='<%#Eval("#") %>' width="30px">
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="Display_None" />
                                <ItemStyle CssClass="Display_None" />
                            </asp:TemplateField>  
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId">
                                <HeaderStyle Wrap="false" CssClass="Display_None"/>
                                <ItemStyle Wrap="false" CssClass="Display_None"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Item" HeaderText="Item">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                            
                            
                    <asp:BoundField DataField="ItemDetailsId" HeaderText="ItemDetailsId">
                    <HeaderStyle Wrap="False" CssClass="Display_None"/>
                    <ItemStyle Wrap="False" CssClass="Display_None"/>
                    </asp:BoundField>
           
                     <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                             <asp:BoundField DataField="Unit" HeaderText="Unit">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="Inward" HeaderText="Inward Qty">
                                <HeaderStyle Wrap="false"  />
                                <ItemStyle Wrap="false" HorizontalAlign="Right"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Rate" HeaderText="Rate">
                                <HeaderStyle Wrap="false" CssClass="Display_None" />
                                <ItemStyle Wrap="false" HorizontalAlign="Right" CssClass="Display_None"/>
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="Outward Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOutward" runat="server" Text='<%#Eval("Outward") %>' 
                                    CssClass="TextBox" ontextchanged="txtOutward_TextChanged" AutoPostBack="true" TabIndex="4" Width="80px"></asp:TextBox>
                                    
                                      <asp:DropDownList ID="ddlUnitConversion" runat="server" CssClass="Display_None" TabIndex="4"
                                      onchange="javascript:ValueOfDDL(this);"
                                        Width="150px">
                                    </asp:DropDownList>
                                     <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtOutward"
                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtAvlQty" runat="server" Text='<%#Eval("Inward") %>' 
                                    CssClass="Display_None" ></asp:TextBox>
                                    
                               
                              
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50px" Wrap="false"/>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pending Qty">
                                <ItemTemplate>
                                    <asp:Label ID="LblPending" runat="server" Text='<%#Eval("Pending") %>' CssClass="Label"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Right"/>
                            </asp:TemplateField>
                            
                                <asp:BoundField DataField="Amount" HeaderText="Amount">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" HorizontalAlign="Right"/>
                            </asp:BoundField>
                           
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="ComboBox" TabIndex="4"
                                        Width="150px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFV21" runat="server" 
                                    ControlToValidate="ddlLocation" Display="None" 
                                    ErrorMessage="Please Select Location To Transfer" InitialValue="0" 
                                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" 
                                    TargetControlID="RFV21" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                    </ajax:ValidatorCalloutExtender> 
                                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="LocationId">
                             <ItemTemplate>
                                 <asp:Label ID="lblLocID" runat="server" Text='<%# Eval("LocID") %>' Width="30px"></asp:Label>
                             </ItemTemplate>
                                 <HeaderStyle CssClass="Display_None" />
                                 <ItemStyle CssClass="Display_None" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="UnitId">
                             <ItemTemplate>
                                 <asp:Label ID="lblUnitId" runat="server" Text='<%# Eval("UnitConvDtlsId") %>' Width="30px"></asp:Label>
                             </ItemTemplate>
                                 <HeaderStyle CssClass="Display_None" />
                                 <ItemStyle CssClass="Display_None" />
                           </asp:TemplateField>
                           
                             <asp:TemplateField HeaderText="Tower">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlTower" runat="server" CssClass="ComboBox" TabIndex="4"
                                        Width="150px">
                                    </asp:DropDownList>
                                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"/>
                            </asp:TemplateField>
                            
                                <asp:TemplateField HeaderText="Final Outward Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtUnitWiseOutwardQty" runat="server" Text='<%#Eval("Outward") %>' 
                                    CssClass="TextBox"  AutoPostBack="true" TabIndex="4" Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50px" Wrap="false" CssClass="Display_None"/>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Wrap="false" CssClass="Display_None"/>
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
                </div> 
                </td>
            </tr>
            
            
            <tr runat="server" id="TRGRIDCATEGORYITEM">
                <td colspan="4">                
             
                <div class="ScrollableDiv_FixHeightWidth5">                 
                    <asp:GridView ID="GRDCATEGORYITEM" runat="server" AutoGenerateColumns="False" 
                        CssClass="mGrid" onrowdatabound="GRDCATEGORYITEM_RowDataBound" >
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="LblEntryId" runat="server" Text='<%#Eval("#") %>' width="30px">
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="Display_None" />
                                <ItemStyle CssClass="Display_None" />
                            </asp:TemplateField>  
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId">
                                <HeaderStyle Wrap="false" CssClass="Display_None"/>
                                <ItemStyle Wrap="false" CssClass="Display_None"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Item" HeaderText="Item">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ItemDetailsId" HeaderText="ItemDetailsId">
                    <HeaderStyle Wrap="False" CssClass="Display_None"/>
                    <ItemStyle Wrap="False" CssClass="Display_None"/>
                    </asp:BoundField>
           
                     <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                             <asp:BoundField DataField="Unit" HeaderText="Unit">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="Available" HeaderText="Stock IN Hand">
                                <HeaderStyle Wrap="false"  />
                                <ItemStyle Wrap="false" HorizontalAlign="Right"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Rate" HeaderText="Rate">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" HorizontalAlign="Right"/>
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="Outward Qty">
                                <ItemTemplate>
                                  <asp:DropDownList ID="ddlUnitConversion" runat="server" CssClass="ComboBox" TabIndex="4"
                                        Width="150px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOutward" runat="server" Text='<%#Eval("Outward") %>' 
                                    CssClass="TextBox"  AutoPostBack="true" TabIndex="4" 
                                        ontextchanged="txtOutward_TextChanged1"></asp:TextBox>
                                        
                                     <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtOutward"
                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtAvlQty" runat="server" Text='<%#Eval("Available") %>' 
                                    CssClass="Display_None" ></asp:TextBox>
                                    
                                
                              
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Amount" HeaderText="Amount">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" HorizontalAlign="Right"/>
                            </asp:BoundField>
                                                   
                          
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="ComboBox" TabIndex="4" Enabled="false"
                                        Width="150px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFV21" runat="server" 
                                    ControlToValidate="ddlLocation" Display="None" 
                                    ErrorMessage="Please Select Location To Transfer" InitialValue="0" 
                                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" 
                                    TargetControlID="RFV21" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                    </ajax:ValidatorCalloutExtender> 
                                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="LocationId">
                             <ItemTemplate>
                                 <asp:Label ID="lblLocID" runat="server" Text='<%# Eval("LocID") %>' Width="30px"></asp:Label>
                             </ItemTemplate>
                                 <HeaderStyle CssClass="Display_None" />
                                 <ItemStyle CssClass="Display_None" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="UnitId">
                             <ItemTemplate>
                                 <asp:Label ID="lblUnitId" runat="server" Text='<%# Eval("UnitConvDtlsId") %>' Width="30px"></asp:Label>
                             </ItemTemplate>
                                 <HeaderStyle CssClass="Display_None" />
                                 <ItemStyle CssClass="Display_None" />
                           </asp:TemplateField>
                           
                           
                           
                        </Columns>
                    </asp:GridView>
                </div> 
                </td>
            </tr>
            
            <tr runat="server" id="TRGRIDREQUISITION">
                <td colspan="4">                
                
                <div class="ScrollableDiv_FixHeightWidth5">                 
                    <asp:GridView ID="GRDREQUISITION" runat="server" AutoGenerateColumns="False" 
                        CssClass="mGrid" onrowdatabound="GRDREQUISITION_RowDataBound" >
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Label ID="LblEntryId" runat="server" Text='<%#Eval("#") %>' width="30px">
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="Display_None" />
                                <ItemStyle CssClass="Display_None" />
                            </asp:TemplateField>  
                            <asp:BoundField DataField="ItemId" HeaderText="ItemId">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Item" HeaderText="Item">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="ItemDetailsId" HeaderText="ItemDetailsId">
                    <HeaderStyle Wrap="False" CssClass="Display_None" />
                    <ItemStyle Wrap="False" CssClass="Display_None"/>
                    </asp:BoundField>
           
                     <asp:BoundField DataField="ItemDesc" HeaderText="Description">
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                             <asp:BoundField DataField="Unit" HeaderText="Unit">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" />
                            </asp:BoundField>
                          
                            <asp:BoundField DataField="Available" HeaderText="Stock IN Hand">
                                <HeaderStyle Wrap="false"  />
                                <ItemStyle Wrap="false" HorizontalAlign="Right"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Inward" HeaderText="Requisition Qty">
                                <HeaderStyle Wrap="false"  />
                                <ItemStyle Wrap="false" HorizontalAlign="Right"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Rate" HeaderText="Rate">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" HorizontalAlign="Right"/>
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="Outward Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOutward" runat="server" Text='<%#Eval("Outward") %>' 
                                    CssClass="TextBox"  AutoPostBack="true" TabIndex="4" Width="100px"
                                        ontextchanged="txtOutward_TextChanged2"></asp:TextBox>
                                          <asp:DropDownList ID="ddlUnitConversion" runat="server" CssClass="ComboBox" TabIndex="4"
                                        Width="120px">
                                    </asp:DropDownList>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtOutward"
                    FilterType="Custom,Numbers" ValidChars="."></ajax:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtAvlQty" runat="server" Text='<%#Eval("Available") %>' 
                                    CssClass="Display_None" ></asp:TextBox>
                                    
                              
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                                <asp:BoundField DataField="Amount" HeaderText="Amount">
                                <HeaderStyle Wrap="false" />
                                <ItemStyle Wrap="false" HorizontalAlign="Right"/>
                            </asp:BoundField>
                           
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="ComboBox" TabIndex="4"
                                        Width="150px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFV21" runat="server" 
                                    ControlToValidate="ddlLocation" Display="None" 
                                    ErrorMessage="Please Select Location To Transfer" InitialValue="0" 
                                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" 
                                    TargetControlID="RFV21" WarningIconImageUrl="~/Images/Icon/Warning.png">
                                    </ajax:ValidatorCalloutExtender> 
                                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="LocationId">
                             <ItemTemplate>
                                 <asp:Label ID="lblLocID" runat="server" Text='<%# Eval("LocID") %>' Width="30px"></asp:Label>
                             </ItemTemplate>
                                 <HeaderStyle CssClass="Display_None" />
                                 <ItemStyle CssClass="Display_None" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="UnitId">
                             <ItemTemplate>
                                 <asp:Label ID="lblUnitId" runat="server" Text='<%# Eval("UnitConvDtlsId") %>' Width="30px"></asp:Label>
                             </ItemTemplate>
                                 <HeaderStyle CssClass="Display_None" />
                                 <ItemStyle CssClass="Display_None" />
                           </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div> 
                </td>
            </tr>
            </table>
        
    </fieldset>


 </td>
  </tr>

   <tr>
  <td>
  
   <fieldset class="FieldSet" id="FieldSet1" runat="server">
  <table width="100%" cellspacing="10px">
<tr>
<td class="Label">
    Remark / Name Of Contractor :
</td>
<td colspan="2">
<asp:TextBox runat="server" ID="TxtRemark" Width="850px" CssClass="TextBox"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="3" align="center">
<table align="center" width="25%">
<tr>
<td>

<asp:Button ID="BtnUpdate" CssClass="button" runat="server" Text="Update" 
        ValidationGroup="Add" onclick="BtnUpdate_Click" />

<ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Would You Like To Update The Record ?"
TargetControlID="BtnUpdate">

</ajax:ConfirmButtonExtender>
</td>           

<td>
<asp:Button ID="BtnSave" CssClass="button" runat="server" Text="Save" TabIndex="5"
        ValidationGroup="Add" Height="28px" onclick="BtnSave_Click"  />
</td>

<td>
<asp:Button ID="BtnDelete" CssClass="button" runat="server" Text="Delete" TabIndex="6"
        CausesValidation="false" onclick="BtnDelete_Click" />

</td>

<td>
<asp:Button ID="BtnCancel" CssClass="button" runat="server" Text="Cancel" 
        CausesValidation="false" onclick="BtnCancel_Click"  />
</td>
</tr>
</table>
</td>
</tr>


<tr>
          <td align="center" colspan="2">
              <div ID="Div5" runat="server" class="scrollableDiv">
                 <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                         <asp:GridView ID="ReportGrid1" runat="server" AllowPaging="True" 
                             AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="#" 
                             onrowcommand="ReportGrid1_RowCommand" 
                             onrowdeleting="ReportGrid1_RowDeleting" 
                             ondatabound="ReportGrid1_DataBound" 
                             onrowdatabound="ReportGrid1_RowDataBound" 
                             onpageindexchanging="ReportGrid1_PageIndexChanging">
                             <Columns>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                         <asp:ImageButton ID="ImageGridEdit" runat="server" 
                                             CommandArgument='<%# Eval("#") %>' CommandName="Select" 
                                             ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                         <asp:ImageButton ID="ImgBtnDelete" runat="server" 
                                         CommandArgument='<%# Eval("#")%>'
                                         CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete"/>
                                     <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                         ConfirmText="Would You Like To Delete The Record..!" 
                                         TargetControlID="ImgBtnDelete">
                                      </ajax:ConfirmButtonExtender>
                                      <%--   <a href='../PrintReport/StockAssignPrint.aspx?ID=<%# Eval("#")%>&amp;Flag=<%="PS"%>' 
                                             target="_blank">
                                         <asp:Image ID="ImgBtnPrint" runat="server" 
                                             ImageUrl="~/Images/Icon/GridPrint.png" TabIndex="29" 
                                             ToolTip="Print Issuse" />
                                         </a>--%>

                                        <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="IS"%>&PDFFlag=<%="NOPDF"%>' target="_blank">
                                        <asp:Image ID="Image1" runat="server" 
                                        ImageUrl="~/Images/Icon/GridPrint.png" TabIndex="29" 
                                        ToolTip="Print Request Register" />
                                        </a>

                                        <a href='../CrystalPrint/PrintCryRpt.aspx?ID=<%# Eval("#")%>&Flag=<%="IS"%>&PDFFlag=<%="PDF"%>' target="_blank">
                                        <asp:Image ID="Image2" runat="server" 
                                        ImageUrl="~/Images/New Icon/pdfImg.png" TabIndex="29" 
                                        ToolTip="PDF of Request Register" />
                                        </a>

                                     </ItemTemplate>
                                     <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     <HeaderStyle Width="20px" />
                                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="55px" 
                                         Wrap="false" />
                                 </asp:TemplateField>
                                 <asp:BoundField DataField="Name" HeaderText="Stock No.">
                                     <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                   <asp:BoundField DataField="Date" HeaderText="Stock Date">
                                     <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Type" HeaderText="Type">
                                     <HeaderStyle Wrap="False" />
                                     <ItemStyle Wrap="False" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Status" HeaderText="Status">
                                     <HeaderStyle Wrap="False" />
                                     <ItemStyle Wrap="False" />
                                 </asp:BoundField>
                             </Columns>
                         </asp:GridView>
                     </ContentTemplate>
                 </asp:UpdatePanel>
             </div>
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

