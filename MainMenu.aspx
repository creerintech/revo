<%@ Page Language="C#" MasterPageFile="~/MasterPages/MainMenu.master" AutoEventWireup="true" CodeFile="MainMenu.aspx.cs" Inherits="MainMenu" Title="Main Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainMenuTitleContent" Runat="Server">
Main Menu 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainMenuBody" Runat="Server">
<br />
<fieldset>     
<table width="80%" align="center" cellpadding="2" cellspacing="2">
        <tr>
            <td align ="center" >
                <asp:Image ID="ImgBtnMarketing" runat="server"  CssClass="Imagebutton"
                  ImageUrl="~/Images/New Icon/TechBuild.png" 
                     BorderColor="#333333" Height="100px" Width="100px"
                    BorderStyle="Ridge" ToolTip="Sales Module"  />
           
                </td>
        
            <td align ="center" >
                  
                <asp:Image ID="ImgDesign" runat="server"  CssClass="Imagebutton"
                    ImageUrl="~/Images/New Icon/TechBuild.png" 
                     BorderColor="#333333" Height="100px" Width="100px"
                    BorderStyle="Ridge" ToolTip="Sales Module PRO"  />
                </td>
        
        </tr>
        <tr>
          
            <td class="SubTitle" align="center">
                <a href="ProjectStatus/ProjectStatus.aspx?ModuleType=<%="Marketing" %>" >Build Time</a></td>
            <td class="SubTitle" align="center">
                <a href="ProjectStatus/ProjectStatus.aspx?ModuleType=<%="Design" %>" >Build Time</a></td></td>
        </tr>
        <tr>
           
            <td class="SubTitle">
                &nbsp;</td>
            <td class="SubTitle">
                &nbsp;</td>
        </tr>
        <tr>
          
            <td class="SubTitle">
             
                <asp:Image ID="ImgSalart" runat="server"  CssClass="Imagebutton"
                     ImageUrl="~/Images/New Icon/TechBuild.png" 
                     BorderColor="#333333" Height="100px" Width="100px"
                     BorderStyle="Ridge" ToolTip="MMS"  />
                      </td>
            <td class="SubTitle">
                   
                <asp:Image ID="Image1" runat="server"  CssClass="Imagebutton"
                     ImageUrl="~/Images/New Icon/TechBuild.png" 
                     BorderColor="#333333" Height="100px" Width="100px"
                     BorderStyle="Ridge" ToolTip="MMS PRO"  />
                   </td>
        </tr>
        <tr>
          
            <td class="SubTitle" align="center">
                 <a href="ProjectStatus/ProjectStatus.aspx?ModuleType=<%="Salary" %>" >MMS</a></td>
            <td class="SubTitle">
                <a href="ProjectStatus/ProjectStatus.aspx?ModuleType=<%="Salary" %>" >MMS PRO</a></td>
        </tr>
        <tr>
           
            <td class="SubTitle" align="center">
                 &nbsp;</td>
            <td class="SubTitle">
                &nbsp;</td>
        </tr>
     
    </table>
 </fieldset> 
 
</asp:Content>
