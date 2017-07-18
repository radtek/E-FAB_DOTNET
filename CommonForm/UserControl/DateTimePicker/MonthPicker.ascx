<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MonthPicker.ascx.cs" Inherits="CommonForm_UserControl_DateTimePicker_MonthPicker" %>
<table>
    <tr>
        <td style="text-align:right;">Start Month :</td>
        <td>
            <asp:DropDownList ID="ddlStartYear" runat="server">
			</asp:DropDownList>
			<asp:DropDownList ID="ddlStartMonth" runat="server" Width="50">
				<asp:ListItem Text="01" Value="01" />
				<asp:ListItem Text="02" Value="02" />
				<asp:ListItem Text="03" Value="03" />
				<asp:ListItem Text="04" Value="04" />
				<asp:ListItem Text="05" Value="05" />
				<asp:ListItem Text="06" Value="06" />
				<asp:ListItem Text="07" Value="07" />
				<asp:ListItem Text="08" Value="08" />
				<asp:ListItem Text="09" Value="09" />
				<asp:ListItem Text="10" Value="10" />
				<asp:ListItem Text="11" Value="11" />
				<asp:ListItem Text="12" Value="12" />
			</asp:DropDownList>   
        </td>
    </tr>
    <tr>
        <td style="text-align:right;">End Month :</td>
        <td>
            <asp:DropDownList ID="ddlEndYear" runat="server">
			</asp:DropDownList>
			<asp:DropDownList ID="ddlEndMonth" runat="server" Width="50">
				<asp:ListItem Text="01" Value="01" />
				<asp:ListItem Text="02" Value="02" />
				<asp:ListItem Text="03" Value="03" />
				<asp:ListItem Text="04" Value="04" />
				<asp:ListItem Text="05" Value="05" />
				<asp:ListItem Text="06" Value="06" />
				<asp:ListItem Text="07" Value="07" />
				<asp:ListItem Text="08" Value="08" />
				<asp:ListItem Text="09" Value="09" />
				<asp:ListItem Text="10" Value="10" />
				<asp:ListItem Text="11" Value="11" />
				<asp:ListItem Text="12" Value="12" />
			</asp:DropDownList>   
        </td>
    </tr>
</table>