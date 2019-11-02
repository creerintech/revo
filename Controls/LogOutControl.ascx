<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LogOutControl.ascx.cs" Inherits="Controls_LogOutControl" %>
   <div class="headerBottom">
			<div class="top_infoBottom">			
				<div class="top_info_rightBottom">
					<p><b>Welcome :<asp:Label ID="Label1" runat="server" oninit="Label1_Init" CssClass="LabelLoginName"></asp:Label> &nbsp;&nbsp;&nbsp;</b>
					<a href="../Masters/ChangePassword.aspx" CssClass="LabelLoginName">Change Password</a> /
					    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" CssClass="LabelLoginName">SignOut</asp:LinkButton>
                         </p>
				</div>	
			
		</div>