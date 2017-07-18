<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateTimeSelector.ascx.cs" Inherits="Fabinfo_CF_Report_CF_Defect_Trend_DateTimeSelector" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CommonForm/UserControl/DateTimePicker/DailyPicker.ascx" TagPrefix="innolux" TagName="DailyPicker" %>
<%@ Register Src="~/CommonForm/UserControl/DateTimePicker/HourlyPicker.ascx" TagPrefix="innolux" TagName="HourlyPicker" %>
<%@ Register Src="~/CommonForm/UserControl/DateTimePicker/WeeklyPicker.ascx" TagPrefix="innolux" TagName="WeeklyPicker" %>
<%@ Register Src="~/CommonForm/UserControl/DateTimePicker/MonthPicker.ascx" TagPrefix="innolux" TagName="MonthPicker" %>


<telerik:RadTabStrip 
	ID="TimeTypeTabStrip" 
	runat="server" 
	MultiPageID="TimeTypeMultiPage" 
	SelectedIndex="1" 
	Skin="Office2007" 
	CausesValidation="false"
	Width="250px">
    <Tabs>
        <telerik:RadTab runat="server" Text="Hourly"></telerik:RadTab>
        <telerik:RadTab runat="server" Text="Daily" Selected="True"></telerik:RadTab>
        <telerik:RadTab runat="server" Text="Weekly"></telerik:RadTab>
        <telerik:RadTab runat="server" Text="Monthly"></telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>

<telerik:RadMultiPage ID="TimeTypeMultiPage" runat="server" SelectedIndex="1">
    <telerik:RadPageView ID="HourPageView" runat="server">
		 <innolux:HourlyPicker runat="Server" ID="hourlyPicker" MaxDuration="7" />
    </telerik:RadPageView>
    <telerik:RadPageView ID="DayPageView" runat="server">
		 <innolux:DailyPicker runat="Server" ID="dailyPicker" MaxDuration="3" />
    </telerik:RadPageView>
    <telerik:RadPageView ID="WeekPageView" runat="server">
       <innolux:WeeklyPicker runat="Server" id="weeklyPicker" MaxDuration="15" />
    </telerik:RadPageView>
    <telerik:RadPageView ID="MonthPageView" runat="server">
       <innolux:MonthPicker runat="server" ID="monthPicker" /> 
    </telerik:RadPageView>
</telerik:RadMultiPage>