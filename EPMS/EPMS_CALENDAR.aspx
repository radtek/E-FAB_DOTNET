<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EPMS_CALENDAR.aspx.cs" Inherits="EPMS_EPMS_CALENDAR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EPMS CALENDAR</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
           <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66"
                                BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                ForeColor="#663399" Height="500px" OnDayRender="Calendar1_DayRender" ShowGridLines="True"
                                Width="860px" OnSelectionChanged="Calendar1_SelectionChanged">
                                <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" BorderColor="Black" ForeColor="Black" />
                                <SelectorStyle BackColor="#FFCC66" />
                                <TodayDayStyle BackColor="#FFCC66" ForeColor="DarkRed" />
                                <OtherMonthDayStyle ForeColor="#CC9966" />
                                <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                            </asp:Calendar>
    </div>
     <asp:BulletedList ID="bllColor" runat="server">
                                </asp:BulletedList>
    </form>
</body>
</html>
