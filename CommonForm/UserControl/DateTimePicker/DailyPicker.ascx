<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DailyPicker.ascx.cs" Inherits="CommonForm_UserControl_DateTimePicker_DailyPicker" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<table>
    <tr>
        <td style="text-align:right;">Start Date :</td>
        <td>
            <telerik:RadDatePicker ID="StartDatePicker" runat="server" Skin="Office2007">
            </telerik:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td style="text-align:right;">End Date :</td>
        <td>
            <telerik:RadDatePicker ID="EndDatePicker" runat="server" Skin="Office2007">
            </telerik:RadDatePicker>
        </td>
    </tr>
</table>