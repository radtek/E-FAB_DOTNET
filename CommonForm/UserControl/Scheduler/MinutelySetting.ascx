<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MinutelySetting.ascx.cs" Inherits="CommonForm_UserControl_Scheduler_MinutelySetting" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<table cellspacing="1" cellpadding="1" width="100%" style="padding:0;background-color:#8DB2E3;">
    <tr>
        <td style="background-color:#E6F3FF; width:40%; font-weight:bold;">
            Start DateTime
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
        <td style="background-color:#E6F3FF; width:40%; font-weight:bold;">
            End DateTime
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
        <td style="background-color:#E6F3FF; width:40%; font-weight:bold;">Interval Minute</td>
        <td style="background-color:#fff">
            <telerik:RadNumericTextBox ID="txtIntervalMinute" runat="server" ShowSpinButtons="True" 
                Type="number" MinValue="1" Skin="Office2007" width="50" 
                ButtonsPosition="left" Value="1" CausesValidation="false">
                <NumberFormat AllowRounding="True" KeepNotRoundedValue="False" DecimalDigits="0" />
            </telerik:RadNumericTextBox>
        </td>
    </tr>
    <tr style="height:25px">
        <td style="background-color:#E6F3FF; width:40%; font-weight:bold;">Repeat Count</td>
        <td style="background-color:#fff;">
            <telerik:RadNumericTextBox ID="txtRepeatCount" runat="server" ShowSpinButtons="True" 
                Type="number" MinValue="-1" Skin="Office2007" width="50" 
                ButtonsPosition="left" Value="0" CausesValidation="false">
                <NumberFormat AllowRounding="True" KeepNotRoundedValue="False" DecimalDigits="0" />
            </telerik:RadNumericTextBox>
            (-1 repeat continually)
        </td>
    </tr>
</table>