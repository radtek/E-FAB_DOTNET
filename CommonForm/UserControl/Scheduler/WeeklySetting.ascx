<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WeeklySetting.ascx.cs" Inherits="CommonForm_UserControl_Scheduler_WeeklySetting" %>
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
        <td style="background-color:#E6F3FF; width:40%; font-weight:bold;">Day of week</td>
        <td style="background-color:#fff">
            <asp:DropDownList ID="ddlDayOfWeek" runat="server" Width="50">
                <asp:ListItem Text="Mon" Value="1" />
                <asp:ListItem Text="TUE" Value="2" />
                <asp:ListItem Text="WED" Value="3" />
                <asp:ListItem Text="THU" Value="4" />
                <asp:ListItem Text="FRI" Value="5" />
                <asp:ListItem Text="SAT" Value="6" />
                <asp:ListItem Text="SUN" Value="0" />
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