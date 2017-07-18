<%@ Page Language="C#" AutoEventWireup="true" CodeFile="north_south_oee_data.aspx.cs" Inherits="OEE_north_south_oee_data" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>NORTH_SOUTH_OEE_DATA</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table align="center" border="1" bordercolordark="#ffffff" bordercolorlight="#7a9cb7"
            cellpadding="2" cellspacing="0" width="90%">
            <tr>
                <td align="center" class="pageTD" style="width: 15%; height: 24px">
                    &nbsp;</td>
                <td align="left" style="width: 107px; height: 24px">
                    Add Data For North South&nbsp; Data</td>
                <td align="center" class="pageTD" style="width: 12%; height: 24px">
                    &nbsp;</td>
                <td align="left" style="width: 106px; height: 24px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="pageTD" style="width: 15%; height: 24px">
                    開始時間From</td>
                <td align="left" style="width: 107px; height: 24px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <telerik:raddatepicker id="txtEstimateSTARTTIME" runat="server" enabletyping="False"
                                skin="Office2007" skinid="Office2007">
                                            <DateInput DateFormat="yyyy/MM/dd" Font-Size="10pt"
                                                    ReadOnly="True" Skin="Office2007">
                                            </DateInput>
                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Date="" Repeatable="Today">
                                                        <ItemStyle CssClass="rcToday"  />
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DatePopupButton HoverImageUrl="" ImageUrl=""  />
                                        </telerik:raddatepicker>
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td align="center" class="pageTD" style="width: 12%; height: 24px">
                    結束時間End</td>
                <td align="left" style="width: 106px; height: 24px">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <telerik:raddatepicker id="txtEstimateEndTime" runat="server" enabletyping="False"
                                skin="Office2007" skinid="Office2007">
                                            <DateInput DateFormat="yyyy/MM/dd" Font-Size="10pt"
                                                    ReadOnly="True" Skin="Office2007">
                                            </DateInput>
                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Date="" Repeatable="Today">
                                                        <ItemStyle CssClass="rcToday"  />
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DatePopupButton HoverImageUrl="" ImageUrl=""  />
                                        </telerik:raddatepicker>
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center" class="pageTD" style="width: 15%; height: 24px">
                    &nbsp;</td>
                <td align="left" style="width: 107px; height: 24px">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="執行" /></td>
                <td align="center" class="pageTD" style="width: 12%; height: 24px">
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="產能利用率" />&nbsp;</td>
                <td align="left" style="width: 106px; height: 24px">
                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="CELL_BEOL" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
