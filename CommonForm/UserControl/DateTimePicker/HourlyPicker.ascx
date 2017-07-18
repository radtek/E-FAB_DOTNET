<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HourlyPicker.ascx.cs" Inherits="CommonForm_UserControl_DateTimePicker_HourlyPicker" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<table>
    <tr>
        <td style="text-align:right;">Start Hour :</td>
        <td>
            <telerik:RadDateTimePicker ID="StartHourPicker" runat="server" Skin="Office2007" Culture="Chinese (Taiwan)" Width="170px">
                <DateInput Skin="Office2007" DateFormat="yyyy/MM/dd HH:00:00">
                </DateInput>
                <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                </Calendar>
                <TimePopupButton HoverImageUrl="" ImageUrl="" />
                <TimeView CellSpacing="-1" Culture="Chinese (Taiwan)">
                </TimeView>
                <DatePopupButton HoverImageUrl="" ImageUrl="" />
            </telerik:RadDateTimePicker>
        </td>
    </tr>
    <tr>
        <td style="text-align:right;">End Hour :</td>
        <td >
            <telerik:RadDateTimePicker ID="EndHourPicker" runat="server" Skin="Office2007" Culture="Chinese (Taiwan)" Width="170px">
                <DateInput DateFormat="yyyy/MM/dd HH:00:00" Skin="Office2007">
                </DateInput>
                <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                </Calendar>
                <TimePopupButton HoverImageUrl="" ImageUrl="" />
                <TimeView CellSpacing="-1" Culture="Chinese (Taiwan)">
                </TimeView>
                <DatePopupButton HoverImageUrl="" ImageUrl="" />
            </telerik:RadDateTimePicker>
        </td>
    </tr>
</table>