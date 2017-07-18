<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WeeklyPicker.ascx.cs" Inherits="CommonForm_UserControl_DateTimePicker_WeeklyPicker" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
		<table>
			<tr>
				<td style="text-align:right;">Start Week :</td>
				<td>
					<asp:DropDownList ID="ddlStartYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" >
					</asp:DropDownList>			
					<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="inline">
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="ddlStartYear" EventName="SelectedIndexChanged" />
						</Triggers>
						<ContentTemplate>	
							<asp:DropDownList ID="ddlStartWeek" runat="server" Width="50" >
							</asp:DropDownList>
						</ContentTemplate>
					</asp:UpdatePanel>
					
				</td>
			</tr>
			<tr>
				<td style="text-align:right;">End Week :</td>
				<td>
					<asp:DropDownList ID="ddlEndYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" >
					</asp:DropDownList>		
					<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" RenderMode="inline">
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="ddlEndYear" EventName="SelectedIndexChanged" />
						</Triggers>
						<ContentTemplate>
							<asp:DropDownList ID="ddlEndWeek" runat="server" Width="50" >
							</asp:DropDownList>
						</ContentTemplate>
					</asp:UpdatePanel>	
					
				</td>
			</tr>
		</table>	
	</ContentTemplate>
</asp:UpdatePanel>
