<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calendar_Project.aspx.cs"
    Inherits="Calendar_Project" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/common_form/user_control/calendar_AddControl.ascx" TagName="calendar_AddControl"
    TagPrefix="uc3" %>
<%@ Register Src="~/common_form/user_control/project_list.ascx" TagName="project_list"
    TagPrefix="uc2" %>
<%@ Register Src="~/common_form/user_control/header.ascx" TagName="header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>專案行事曆</title>
    <link href="app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="display: inline; z-index: 105; left: 10px; width: 100%; color: black;
            top: 0px; height: 16px; background-color: white">
            <uc1:header ID="Header1" runat="server" />
            <br />
            <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <img src="images/tables/default_lt.gif" /></td>
                    <td style="background-image: url(images/tables/default_t.gif)">
                        <img height="9" src="images/tables/transparent.gif" /></td>
                    <td>
                        <img src="images/tables/default_rt.gif" /></td>
                </tr>
                <tr>
                    <td style="background-image: url(images/tables/default_l.gif)">
                        <img src="images/tables/transparent.gif" width="9"></td>
                    <td align="middle" width="100%">
                        <table cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="100%" bordercolorlight="#7a9cb7"
                            border="1">
                            <tr>
                                <td align="left" background="" colspan="6" class="pageTitle">
                                    <span id="lblTitle">專案行事曆</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" align="center">
                                    部門
                                </td>
                                <td align="left" colspan="3">
                                    <asp:CheckBoxList runat="server" ID="chkDept" RepeatDirection="Horizontal" CssClass="pageCheckbox"
                                        AutoPostBack="True" RepeatColumns="5">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" colspan="6">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td style="font-size: 12pt; background-image: url(images/tables/default_r.gif)">
                        <img src="images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td>
                        <img src="images/tables/default_lb.gif"></td>
                    <td style="background-image: url(images/tables/default_b.gif)">
                        <img height="9" src="images/tables/transparent.gif"></td>
                    <td>
                        <img src="images/tables/default_rb.gif"></td>
                </tr>
            </table>
            <br />
            <div align="center">
                <table>
                    <tr>
                        <td>
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
                        </td>
                        <td align="left" valign="top">
                            <div style="font-weight: bold; font-size: 12px; font-family: Arial; color: black;
                                text-align: left">
                                Status Color：
                                <asp:BulletedList ID="bllColor" runat="server">
                                </asp:BulletedList>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <%--<fieldset>
                <legend id="legend1" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
                    color: black">Project Detail </legend>--%>
                <uc2:project_list ID="Project_list1" runat="server" />
            <%--</fieldset>--%>
            <uc3:calendar_AddControl ID="Calendar_AddControl1" runat="server"></uc3:calendar_AddControl>
        </div>
    </form>
</body>
</html>
