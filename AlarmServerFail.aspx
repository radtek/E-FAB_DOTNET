<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlarmServerFail.aspx.cs"
    Inherits="AlarmServerFail" %>

<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>AlarmServer Fail 次數統計</title>
    <link href="app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">
            <br />
            <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
                <tr>
                    <td style="height: 9px; width: 10px;">
                        <img src="images/tables/default_lt.gif" /></td>
                    <td style="background-image: url(images/tables/default_t.gif); height: 9px;">
                        <img height="9" src="images/tables/transparent.gif" /></td>
                    <td style="height: 9px; width: 10px;">
                        <img src="images/tables/default_rt.gif" /></td>
                </tr>
                <tr>
                    <td style="background-image: url(images/tables/default_l.gif); width: 10px;">
                        <img src="images/tables/transparent.gif" width="9"></td>
                    <td align="middle" width="100%">
                        <table align="center" cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="100%"
                            bordercolorlight="#7a9cb7" border="1">
                            <tr>
                                <td background="" colspan="2" class="pageTitle">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" style="height: 30px">
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>查詢 AlarmServer
                                                    Fail 次數統計</strong></span></td>
                                            <td align="right" style="font-size: 12px; color: navy; height: 30px;">
                                                <span style="color: #ff0000"></span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" valign="middle" style="height: 22px; width: 10%;
                                    text-align: center;">
                                    資料時間範圍&nbsp;Start</td>
                                <td style="text-align: left; width: 341px; height: 22px;" valign="top">
                                    <asp:TextBox ID="txtCalendar1" runat="server" Width="91px"></asp:TextBox><obout:Calendar
                                        ID="Calendar1" runat="server" Columns="1" DateFormat="yyyy/MM/dd" DatePickerImagePath="~/images/calendar.gif"
                                        DatePickerMode="True" FullDayNames="璆,銝,鈭,銝,?鈭,摮" ScriptPath="~/js/" ShortDayNames="璆,銝,鈭,銝,?鈭,摮"
                                        StyleFolder="~/css/" TextBoxId="txtCalendar1">
                                    </obout:Calendar>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td align="center" class="pageTD" style="width: 10%; height: 22px; text-align: center"
                                    valign="middle">
                                    資料時間範圍&nbsp;End</td>
                                <td style="width: 341px; height: 22px; text-align: left" valign="top">
                                    <asp:TextBox ID="txtCalendar2" runat="server" Width="91px"></asp:TextBox><obout:Calendar
                                        ID="Calendar2" runat="server" Columns="1" DateFormat="yyyy/MM/dd" DatePickerImagePath="~/images/calendar.gif"
                                        DatePickerMode="True" FullDayNames="璆,銝,鈭,銝,?鈭,摮" ScriptPath="~/js/" ShortDayNames="璆,銝,鈭,銝,?鈭,摮"
                                        StyleFolder="~/css/" TextBoxId="txtCalendar2">
                                    </obout:Calendar>
                                    EXPAND
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem>N</asp:ListItem>
                                        <asp:ListItem>Y</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="pageTD" colspan="6" style="height: 35px; text-align: left;">
                                    <table style="width: 236px; height: 7px">
                                        <tr>
                                            <td align='left' valign='top' style="width: 14px; height: 17px;">
                                                <asp:Button ID="ButtonQuery" runat="server" Style="font-size: 12px; font-family: Arial;
                                                    width: 100px;" Text="Query" OnClick="ButtonQuery_Click" />
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td align='left' valign='top' style="width: 25px; height: 17px;">
                                                <asp:Button ID="Button1" runat="server" Text="ExportToExcel" OnClick="Button1_Click" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="height: 271px">
                                    <fieldset>
                                        <legend align="center" style="color: blue; text-align: center"><strong><span style="font-family: Century Gothic">
                                            查詢結果</span></strong>:</legend>
                                        <table hight="100%" width="100%">
                                            <tr>
                                                <td align='center' valign='middle'>
                                                    <asp:GridView ID="GridView1" runat="server" Font-Names="Century Gothic" Font-Size="13pt"
                                                        Width="1000px" AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView1_RowDataBound"
                                                        EmptyDataText="No Record!!!">
                                                        <RowStyle ForeColor="Black" />
                                                       
                                                          
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="btnShowDetail" runat="server" ImageUrl="~/images/close13.gif" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="EventSubject">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEventSubject" runat="server" ForeColor="#000000" Text='<%# Bind("EventSubject") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblEventSubject" runat="server" ForeColor="#000000" Text='<%# Bind("EventSubject") %>'></asp:Label></br>
                                                                    <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="EventName">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEventName" runat="server" ForeColor="#000000" Text='<%# Bind("EventName") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblEventName" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("EventName") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Address">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdress" runat="server" ForeColor="#000000" Text='<%# Bind("address") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbladdress" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("address") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fail_count">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblfail_count" runat="server" ForeColor="#000000" Text='<%# Bind("fail_count") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblfail_count" runat="server" ForeColor="#000000" Width="250px"
                                                                        Text='<%# Bind("fail_count") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                         <AlternatingRowStyle BackColor="White" />
                                                          <RowStyle BackColor="#F7FFFF" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                        AutoGenerateColumns="false" OnRowDataBound="GridView2_RowDataBound" Visible="false">
                                                        <RowStyle BackColor="#F7F7DE" />
                                                        <FooterStyle BackColor="#CCCC99" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                         <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                            
                                                            <asp:BoundField DataField="eventno" HeaderText="eventno" />
                                                            <asp:BoundField DataField="name" HeaderText="name" />
                                                            <asp:BoundField DataField="address" HeaderText="address" />
                                                            <asp:BoundField DataField="senddate" HeaderText="senddate" />
                                                            <asp:BoundField DataField="finished" HeaderText="finished" />
                                                            <asp:BoundField DataField="description" HeaderText="description" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Label runat="server" ID="lblAIExpand" Style="display: none"></asp:Label> 

                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <span style="font-size: 9pt"></span></td>
                            </tr>
                        </table>
                    </td>
                    <td style="font-size: 12pt; background-image: url(images/tables/default_r.gif); width: 10px;">
                        <img src="images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td style="height: 15px; width: 10px;">
                        <img src="images/tables/default_lb.gif"></td>
                    <td style="background-image: url(images/tables/default_b.gif); height: 15px;">
                        <img height="9" src="images/tables/transparent.gif"></td>
                    <td style="height: 15px; width: 10px;">
                        <img src="images/tables/default_rb.gif"></td>
                </tr>
            </table>
    </form>
</body>
</html>




