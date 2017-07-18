<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CF_AOI_FILE_NG_PAPERX.aspx.cs" Inherits="EDA_CF_AOI_FILE_NG_PARSER2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml" >


<head id="Head1" runat="server">
    <title>T1CF AOI MONITOR EDA NG</title>
     <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;<br />
        <br />
        <br />
        <br />
        &nbsp;<table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0"
            width="100%">
            <tr>
                <td style="width: 10px; height: 9px">
                    <img src="../images/tables/default_lt.gif" /></td>
                <td style="background-image: url(../images/tables/default_t.gif); width: 888px; height: 9px">
                    <img height="9" src="../images/tables/transparent.gif" /></td>
                <td style="width: 10px; height: 9px">
                    <img src="../images/tables/default_rt.gif" /></td>
            </tr>
            <tr>
                <td style="background-image: url(../images/tables/default_l.gif); width: 10px">
                    <img src="../images/tables/transparent.gif" width="9" /></td>
                <td align="middle" style="width: 888px; height: 500px">
                    <table align="center" border="1" bordercolordark="#ffffff" bordercolorlight="#7a9cb7"
                        cellpadding="2" cellspacing="0" width="90%">
                        <tr>
                            <td background="" class="pageTitle" colspan="4" style="height: 24px; text-align: left">
                                <table width="100%">
                                    <tr>
                                        <td align="left" style="width: 533px; height: 30px;">
                                            <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>CF AOI
                                                MONITOR&nbsp; FILE&nbsp; To&nbsp; EDA_NG-查詢</strong></span></td>
                                        <td align="right" style="font-size: 12px; color: navy; height: 30px;">
                                        </td>
                                    </tr>
                                </table>
                                <span style="font-size: 16pt; font-family: Century Gothic"><span style="font-size: 8pt;
                                    font-family: Times New Roman"></span></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="pageTD" style="width: 15%; height: 13px">
                                開始時間From</td>
                            <td align="left" colspan="3" style="height: 13px">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <telerik:raddatepicker id="txtEstimateSTARTTIME" runat="server" enabletyping="False"
                                            skin="Office2007" skinid="Office2007">
                                            <DateInput __designer:dtid="562975723225091" DateFormat="yyyy/MM/dd" Font-Size="10pt"
                                                    ReadOnly="True" Skin="Office2007">
                                            </DateInput>
                                            <Calendar __designer:dtid="562975723225092" Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                <SpecialDays __designer:dtid="562975723225093">
                                                    <telerik:RadCalendarDay __designer:dtid="562975723225094" Date="" Repeatable="Today">
                                                        <ItemStyle __designer:dtid="562975723225095" CssClass="rcToday"  />
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DatePopupButton __designer:dtid="562975723225096" HoverImageUrl="" ImageUrl=""  />
                                        </telerik:raddatepicker>
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="pageTD" colspan="8" style="height: 35px; text-align: left">
                                <asp:Button ID="ButtonQuery" runat="server" Style="font-size: 12px;
                                    width: 100px; font-family: Arial" Text="查詢" />
                                &nbsp; &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="EXPORT_TOEXCEL" />
                                &nbsp; &nbsp; 共 &nbsp;
                                <asp:Label ID="Label1" runat="server" ForeColor="Blue" Text="Label"></asp:Label>
                                &nbsp; 筆資料 &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="8" style="height: 35px">
                                &nbsp;<asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#DEDFDE"
            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
            <RowStyle BackColor="#F7F7DE" />
            <FooterStyle BackColor="#CCCC99" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="font-size: 12pt; background-image: url(../images/tables/default_r.gif);
                    width: 10px">
                    <img src="../images/tables/transparent.gif" width="9" /></td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 10px; height: 15px">
                    <img src="../images/tables/default_lb.gif" /></td>
                <td style="background-image: url(../images/tables/default_b.gif); height: 15px">
                    <img height="9" src="../images/tables/transparent.gif" /></td>
                <td style="width: 10px; height: 15px">
                    <img src="../images/tables/default_rb.gif" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
