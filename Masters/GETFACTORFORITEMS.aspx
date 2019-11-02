<%@ Page Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="GETFACTORFORITEMS.aspx.cs" Inherits="Masters_GETFACTORFORITEMS" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchContent" Runat="Server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
  <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
        <ProgressTemplate>            
        <div id="progressBackgroundFilter"></div>
        <div id="processMessage">   
        <center><span class="SubTitle">Loading....!!! </span></center>
        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icon/updateprogress.gif" Height="20px" Width="120px" />                                
        </div>
        </ProgressTemplate>
        </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" Runat="Server">
    <asp:Button runat="server" ID="BTNADD" Text="CALCULATE" CssClass="buttonpayment" 
        onclick="BTNADD_Click" />
        
        <asp:GridView ID="GrdUnitCal" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            BorderStyle="None" BorderWidth="1px" CssClass="mGrid" Font-Bold="False" ForeColor="Black"
                                                                            GridLines="Horizontal">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="#" Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ImageGridEdit" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                                                            CommandName="SelectGrid" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                                                                        <asp:ImageButton ID="ImageBtnDelete" runat="server" CommandArgument='<%#Eval("#") %>'
                                                                                            CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                                        <ajax:ConfirmButtonExtender ID="ConfirmButton" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                                                            TargetControlID="ImageBtnDelete">
                                                                                        </ajax:ConfirmButtonExtender>
                                                                                    </ItemTemplate>
                                                                                    
                                                                                    <ControlStyle CssClass="Display_None" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                                    <HeaderStyle Wrap="False" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                                                                                                        
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="ItemID" DataField="ItemID">   
                                                                                    <ControlStyle CssClass="Display_None" />
                                                                                    <FooterStyle CssClass="Display_None"/>
                                                                                    <HeaderStyle CssClass="Display_None"/>
                                                                                    <ItemStyle CssClass="Display_None"/>                                                                                 
                                                                                </asp:BoundField>                                            
                                                                                                                    
                                                                                <asp:BoundField HeaderText="From_Factor" DataField="From_Factor">                                                                                                                                                                                                                                                         
                                                                                </asp:BoundField>                                                                                
                                                                                
                                                                                <asp:BoundField HeaderText="From_UnitID" DataField="From_UnitID">
                                                                                    <ControlStyle CssClass="Display_None" />
                                                                                    <FooterStyle CssClass="Display_None"/>
                                                                                    <HeaderStyle CssClass="Display_None"/>
                                                                                    <ItemStyle CssClass="Display_None"/>                                                                                    
                                                                                </asp:BoundField>
                                                                                
                                                                                <asp:BoundField HeaderText="From_UnitName" DataField="From_UnitName">                                                                                    
                                                                                </asp:BoundField>
                                                                                
                                                                                <asp:BoundField HeaderText="To_Factor" DataField="To_Factor">                                                                                    
                                                                                </asp:BoundField>                                                                                
                                                                                
                                                                                <asp:BoundField HeaderText="To_UnitID" DataField="To_UnitID">                                                                                    
                                                                                    <ControlStyle CssClass="Display_None" />
                                                                                    <FooterStyle CssClass="Display_None"/>
                                                                                    <HeaderStyle CssClass="Display_None"/>
                                                                                    <ItemStyle CssClass="Display_None"/>
                                                                                </asp:BoundField>
                                                                                
                                                                                <asp:BoundField HeaderText="To_UnitName" DataField="To_UnitName">                                                                                    
                                                                                </asp:BoundField>
                                                                                
                                                                                <asp:BoundField HeaderText="Factor_Desc" DataField="Factor_Desc">                                                                                    
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        
                                                                        
                                                                        
                                                                        <asp:GridView ID="GridDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                            BorderStyle="None" BorderWidth="1px" CssClass="mGrid" Font-Bold="False" ForeColor="Black"
                                                                            GridLines="Horizontal">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="#" Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="LblEntryId" runat="server" Text='<% #Eval("#") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ImageGridEdit" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                                                            CommandName="SelectGrid" ImageUrl="~/Images/Icon/GridEdit.png" ToolTip="Edit" />
                                                                                        <asp:ImageButton ID="ImageBtnDelete" runat="server" CommandArgument='<%#Eval("#") %>'
                                                                                            CommandName="Delete" ImageUrl="~/Images/Icon/GridDelete.png" ToolTip="Delete" />
                                                                                        <ajax:ConfirmButtonExtender ID="ConfirmButton" runat="server" ConfirmText="Would You Like To Delete The Record..!"
                                                                                            TargetControlID="ImageBtnDelete">
                                                                                        </ajax:ConfirmButtonExtender>
                                                                                    </ItemTemplate>
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None"/>
                                                                                    <HeaderStyle Wrap="False" CssClass="Display_None"/>
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None"/>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="LocationId" DataField="LocationId">
                                                                                    <HeaderStyle CssClass="Display_None" />
                                                                                    <ItemStyle CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Site">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="GrdtxtLocation" runat="server" CssClass="TextBoxGrid" MaxLength="10"
                                                                                            Text='<%# Bind("Location") %>' TextMode="SingleLine" Width="120px" Enabled="false"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="SuplierName" DataField="SuplierName">
                                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderText="SupplierId" DataField="SupplierId">
                                                                                    <HeaderStyle CssClass="Display_None" />
                                                                                    <ItemStyle CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderText="Last Purchase Rate" DataField="PurchaseRate">
                                                                                    <HeaderStyle Wrap="false" CssClass="Display_None" />
                                                                                    <ItemStyle Wrap="false" CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Opening Stock">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="GrdtxtOpeningStock" runat="server" CssClass="TextBoxGrid" MaxLength="10"
                                                                                            Text='<%# Bind("OpeningStock") %>' TextMode="SingleLine" Width="50px" Enabled="false"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Purchase Rate">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="GrdtxtPurchaseRate" runat="server" CssClass="TextBoxGrid" MaxLength="10"
                                                                                            Text='<%# Bind("PurchaseRate") %>' TextMode="SingleLine" Width="50px" Enabled="false"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" CssClass="Display_None" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField HeaderText="Item Description" DataField="ItemDesc">
                                                                                    <HeaderStyle Wrap="false" />
                                                                                    <ItemStyle Wrap="false" />
                                                                                </asp:BoundField>
                                                                                <%--<10>--%>
                                                                                <asp:BoundField DataField="MainUnitFactor" HeaderText="Qty">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <%--<11>--%>
                                                                                <asp:BoundField DataField="UnitID" HeaderText="UnitID">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <%--<12>--%><asp:BoundField DataField="MainUnit" HeaderText="From Unit">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <%--<13>--%>
                                                                                <asp:BoundField DataField="SubUnitFactor" HeaderText="Qty">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <%--<14>--%>
                                                                                <asp:BoundField DataField="SubUnitID" HeaderText="SubUnitID">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Display_None" />
                                                                                </asp:BoundField>
                                                                                <%--<15>--%>
                                                                                <asp:BoundField DataField="SubUnit" HeaderText="To Unit">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        
                                                                        
</asp:Content>

