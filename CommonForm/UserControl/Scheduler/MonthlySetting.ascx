<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MonthlySetting.ascx.cs" Inherits="CommonForm_UserControl_Scheduler_MonthlySetting" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<table cellspacing="1" cellpadding="1" width="100%" style="padding:0;background-color:#8DB2E3;">
    <tr>
        <td style="background-color:#E6F3FF; width:40%; font-weight:bold;">Start DateTime
            <br />
            <span style="font-size:7pt; font-style:italic; font-weight:normal">
                Empty for start immediately
            </span>
        </td>
        <td style="background-color:#fff; vertical-align:bottom;">
            <asp:TextBox ID="txtStartTime" runat="server" Width="120"></asp:TextBox>
            <br />(yyyy/mm/dd hh:mi:ss)
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                TargetControlID="txtStartTime" 
                Mask="9999/99/99 99:99:99"
                MessageValidatorTip="true"
                MaskType="DateTime"
                AutoComplete="false" 
                ClearTextOnInvalid="True" 
                UserDateFormat="YearMonthDay" 
                UserTimeFormat="TwentyFourHour" 
                AcceptAMPM="false"/>
        </td>
    </tr>
    <tr>
        <td style="background-color:#E6F3FF; width:40%; font-weight:bold;">End DateTime
            <br />
            <span style="font-size:7pt; font-style:italic; font-weight:normal">
                Empty for stop never
            </span>
        </td>
        <td style="background-color:#fff; vertical-align:bottom;">
            <asp:TextBox ID="txtEndTime" runat="server" Width="120"></asp:TextBox>
            <br />(yyyy/mm/dd hh:mi:ss)
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                TargetControlID="txtEndTime" 
                Mask="9999/99/99 99:99:99"
                MessageValidatorTip="true"
                MaskType="DateTime"
                AutoComplete="false" 
                ClearTextOnInvalid="True" 
                UserDateFormat="YearMonthDay" 
                UserTimeFormat="TwentyFourHour" 
                AcceptAMPM="false"/>
        </td>
    </tr>
    <tr style="height:25px">
        <td style="background-color:#E6F3FF; width:40%; font-weight:bold;">Day of month</td>
        <td style="background-color:#fff">
            <asp:DropDownList ID="ddlDayOfMonth" runat="server" Width="50">
                <asp:ListItem Text="1" Value="1" />
                <asp:ListItem Text="2" Value="2" />
                <asp:ListItem Text="3" Value="3" />
                <asp:ListItem Text="4" Value="4" />
                <asp:ListItem Text="5" Value="5" />
                <asp:ListItem Text="6" Value="6" />
                <asp:ListItem Text="7" Value="7" />
                <asp:ListItem Text="8" Value="8" />
                <asp:ListItem Text="9" Value="9" />
                <asp:ListItem Text="10" Value="10" />
                <asp:ListItem Text="11" Value="11" />
                <asp:ListItem Text="12" Value="12" />
                <asp:ListItem Text="13" Value="13" />
                <asp:ListItem Text="14" Value="14" />
                <asp:ListItem Text="15" Value="15" />
                <asp:ListItem Text="16" Value="16" />
                <asp:ListItem Text="17" Value="17" />
                <asp:ListItem Text="18" Value="18" />
                <asp:ListItem Text="19" Value="19" />
                <asp:ListItem Text="20" Value="20" />
                <asp:ListItem Text="21" Value="21" />
                <asp:ListItem Text="22" Value="22" />
                <asp:ListItem Text="23" Value="23" />
                <asp:ListItem Text="24" Value="24" />
                <asp:ListItem Text="25" Value="25" />
                <asp:ListItem Text="26" Value="26" />
                <asp:ListItem Text="27" Value="27" />
                <asp:ListItem Text="28" Value="28" />
                <asp:ListItem Text="29" Value="29" />
                <asp:ListItem Text="30" Value="30" />
                <asp:ListItem Text="31" Value="31" />
            </asp:DropDownList>
        </td>
    </tr>
    <tr style="height:25px">
        <td style="background-color:#E6F3FF; width:40%; font-weight:bold;">Time of day</td>
        <td style="background-color:#fff">
            <asp:TextBox ID="txtTimeOfDay" runat="server" Width="50"></asp:TextBox>(hh:mi)
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                TargetControlID="txtTimeOfDay" 
                Mask="99:99"
                MessageValidatorTip="true"
                MaskType="time"
                AcceptAMPM="false"/>
        </td>
    </tr>
</table>