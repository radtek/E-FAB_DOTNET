<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="UserControls_MenuHeader" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript" language="javascript">
function OpenNewWind(link_page){
    var width = "900";
	var height = "680";
	if (OpenNewWind.arguments.length>1)
        width = OpenNewWind.arguments[1];
	if (OpenNewWind.arguments.length>2)
        height = OpenNewWind.arguments[2];

    var left = (screen.width/2) - width/2;
    var top = (screen.height/2) - height/2;

    var styleStr = 'toolbar=yes,location=no,status=yes,menubar=yes,scrollbars=yes,resizable=yes,width='+width+',height='+height+',left='+left+',top='+top+',screenX='+left+',screenY='+top;
    var msgWindow = window.open(link_page,"", styleStr);
}

function transferpage(link_page){
    if(top.usrID == null)
     { 
         location = link_page ;
     } 

}
</script>


    <div style="margin:0; padding:0; background-color:#638EC8;font-color:black" >
        <asp:Image ID="titleImage" runat="server" SkinID="pageTitleImage" Width="100%" Height="117" BackColor="#638EC8" />
        <div id="sing_out_layer"> 
			<asp:LoginView ID="LoginView" runat="server">
				<AnonymousTemplate>
					<asp:HyperLink ID="LoginLink" runat="server" NavigateUrl="~/Login.aspx">Login</asp:HyperLink>|
				</AnonymousTemplate>
				<LoggedInTemplate>
					<asp:LoginStatus ID="MemberLoginStatus" LogoutText="Log Out" ForeColor="Green" Font-Bold="true" runat="server" LogoutAction="RedirectToLoginPage"  />					
					<div id="welcome_member"><font color=Green style="font-weight:bold">Welcome,<asp:LoginName ID="MemberName" runat="server" /> | </font></div>
					<asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="inline">
						<ContentTemplate>
							<asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick" Enabled="False">
							</asp:Timer>    
						</ContentTemplate>
					</asp:UpdatePanel>
				</LoggedInTemplate>
			 </asp:LoginView>
        </div>
    </div>
