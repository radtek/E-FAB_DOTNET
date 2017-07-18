<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pp_performance_daily_monitor_create_excel.aspx.cs" Inherits="OEE_pp_performance_daily_monitor_create_excel" %>

<%@ register assembly="DundasWebChart" namespace="Dundas.Charting.WebControl"tagprefix="DCWC" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>PP Performance Daily Monitor</title>
     <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
   
 <%--<div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">--%>
       
      <div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">
          <asp:ScriptManager ID="ScriptManager1" runat="server">
          </asp:ScriptManager>
       
            <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 9px; width: 10px;">
                        <img src="../images/tables/default_lt.gif" /></td>
                    <td style="background-image: url(../images/tables/default_t.gif); height: 9px; width: 894px;">
                        <img height="9" src="../images/tables/transparent.gif" /></td>
                    <td style="height: 9px; width: 10px;">
                        <img src="../images/tables/default_rt.gif" /></td>
                </tr>
                <tr>
                    <td style="background-image: url(../images/tables/default_l.gif); width: 10px; height: 910px;">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                    <td align="middle" style="width: 894px; height: 910px;">
                        <table align="center" cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="90%"
                            bordercolorlight="#7a9cb7" border="1">
                            <tr>
                                <td background="" colspan="4" class="pageTitle" style="height: 24px; text-align: left;">
                                    <table width="100%"  >
                                        <tr>
                                            <td align="left" style="width: 533px">
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong> PP Performance Daily Monitor-查詢</strong></span></td>
                                            <td align="right" style="font-size: 12px; color: navy">
                                                </td>
                                        </tr>
                                    </table>
                                    <span style="font-size: 16pt; font-family: Century Gothic"><span style="font-size: 8pt;
                                        font-family: Times New Roman"></span></span>
                                
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="pageTD" style="width: 15%; height: 24px">
                                    開始時間From</td>
                                <td align="left" style="width: 107px; height: 24px">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <telerik:RadDatePicker ID="txtEstimateSTARTTIME" runat="server" EnableTyping="False"
                                                Skin="Office2007" SkinID="Office2007">
                                            <DateInput DateFormat="yyyy/MM/dd" Font-Size="10pt"
                                                    ReadOnly="True" Skin="Office2007">
                                            </DateInput>
                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Date="" Repeatable="Today">
                                                        <ItemStyle CssClass="rcToday" />
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadDatePicker>
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                    </td>
                                <td align="center" class="pageTD" style="width: 12%; height: 24px">
                                    結束時間End</td>
                                <td align="left" style="width: 106px; height: 24px" >
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <telerik:RadDatePicker ID="txtEstimateEndTime" runat="server" EnableTyping="False"
                                                Skin="Office2007" SkinID="Office2007">
                                            <DateInput DateFormat="yyyy/MM/dd" Font-Size="10pt"
                                                    ReadOnly="True" Skin="Office2007">
                                            </DateInput>
                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Date="" Repeatable="Today">
                                                        <ItemStyle CssClass="rcToday" />
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadDatePicker>
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                    </td>
                            </tr>
                            <tr>
                                <td class="pageTD" colspan="8" style="height: 35px; text-align: left;">
                                    <asp:Button ID="ButtonQuery" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="查詢" OnClick="ButtonQuery_Click"  />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click"
                                        Text="ExportToExcel" />
                                    &nbsp; 共 &nbsp;
                                    <asp:Label ID="Label1" runat="server" ForeColor="Blue" Text="Label"></asp:Label>
                                    &nbsp; 筆資料&nbsp;&nbsp;<span style="color: red">(一開始預設抓1天資料) </span>
                                </td>
                            </tr>
                            <tr>
                                <td  colspan="8" style="height: 35px">
                                    &nbsp;<asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                        OnRowDataBound="GridView1_RowDataBound">
                                        <RowStyle BackColor="#F7F7DE" />
                                        <FooterStyle BackColor="#CCCC99" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                          <%--  <asp:BoundField  HeaderText="RN" />--%>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" style="height: 35px">
                                    </td>
                            </tr>
                          
                        </table>
                    </td>
                    <td style="font-size: 12pt; background-image: url(../images/tables/default_r.gif); width: 10px; height: 910px;">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td style="height: 15px; width: 10px;">
                        <img src="../images/tables/default_lb.gif"></td>
                    <td style="background-image: url(../images/tables/default_b.gif); height: 15px; width: 894px;">
                        <img height="9" src="../images/tables/transparent.gif"></td>
                    <td style="height: 15px; width: 10px;">
                        <img src="../images/tables/default_rb.gif"></td>
                       
                </tr>
            
            </table>
   </div>

    </form>
</body>
</html>
