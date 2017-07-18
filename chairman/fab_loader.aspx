<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fab_loader.aspx.cs" Inherits="chairman_fab_loader" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Fab loading</title>
     <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            </asp:ScriptManager>
   <table  id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
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
                        <table align="center" cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="100%"
                            bordercolorlight="#7a9cb7" border="1">
                            <tr>
                                <td background="" colspan="4" class="pageTitle">
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>Fab Loading</strong></span></td>
                                            <td align="right" style="font-size: 12px; color: navy">
                                                </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="font-size: 8pt">
                                <td class="pageTD" align="center" valign="middle" style="height: 4px; width: 16%;
                                    text-align: center;">
                                    &nbsp;<br />
                                    StartTime</td>
                                    <td style="text-align: left; width: 127px; height: 4px;" valign="top">
                                        <asp:TextBox ID="txtCalendar1" runat="server" Width="91px"></asp:TextBox><obout:calendar
                                            id="Calendar1" runat="server" columns="1" dateformat="yyyy/MM/dd" datepickerimagepath="~/images/calendar.gif"
                                            datepickermode="True" fulldaynames="璆,銝,鈭,銝,?鈭,摮" scriptpath="~/js/" shortdaynames="璆,銝,鈭,銝,?鈭,摮"
                                            stylefolder="~/css/" textboxid="txtCalendar1"> </obout:calendar></td>
                                   
                               
                            </tr>
                            <tr style="font-size: 8pt">
                                <td align="center" class="pageTD" style="width: 16%; height: 4px; text-align: center"
                                    valign="middle">
                                    EndTime</td>
                                <td style="width: 127px; height: 4px; text-align: left" valign="top">
                                    <asp:TextBox ID="txtCalendar2" runat="server" Width="91px"></asp:TextBox><br />
                                    <obout:calendar
                                            id="Calendar2" runat="server" columns="1" dateformat="yyyy/MM/dd" datepickerimagepath="~/images/calendar.gif"
                                            datepickermode="True" fulldaynames="璆,銝,鈭,銝,?鈭,摮" scriptpath="~/js/" shortdaynames="璆,銝,鈭,銝,?鈭,摮"
                                            stylefolder="~/css/" textboxid="txtCalendar2">
                                    </obout:Calendar>
                                </td>
                            </tr>
                            <tr style="font-size: 8pt">
                                <td align="center" class="pageTD" style="width: 16%; height: 4px; text-align: center"
                                    valign="middle">
                                    DTTM</td>
                                <td style="width: 127px; height: 4px; text-align: left" valign="top">
                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="127px">
                                    </asp:DropDownList><asp:Button ID="Button1" runat="server" Text="查詢資料" Width="94px" OnClick="Button1_Click" /></td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td align="center" class="pageTD" style="width: 16%; height: 2px; text-align: center"
                                    valign="middle">
                                    檔案上傳<br />
                                </td>
                                <td style="width: 127px; height: 2px; text-align: left" valign="top">
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                        <ContentTemplate>
                                            &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
                                   
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td align="center" class="pageTD" style="width: 16%; height: 8px; text-align: center"
                                    valign="middle">
                                    範例檔格式下載</td>
                                <td style="width: 127px; height: 8px; text-align: left" valign="top">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../chairman/fab_loading.xls">EXCEL格式下載</asp:HyperLink><asp:Button
                                        ID="ButtonUpload" runat="server" OnClick="ButtonUpload_Click" Style="font-size: 12px;
                                        width: 100px; font-family: Arial" Text="Upload" />
                                    
                                    </td>
                            </tr>
                            <tr>
                                <td class="pageTD"  align="left" colspan="8" style="height: 35px">
                                  
                                    
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                    
                                    <asp:Label ID="LabelX" runat="server"></asp:Label>
                                   </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView1" runat="server" CellPadding="4" OnPreRender="GridView1_PreRender" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px">
                            <RowStyle BackColor="White" HorizontalAlign="Left" ForeColor="#330099" />
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                            <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="#FFFFCC" />
                          
                        </asp:GridView>
                       
                   
                    </td>
                    <td style="font-size: 12pt; background-image: url(images/tables/default_r.gif)">
                        <img src="images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td style="height: 9px">
                        <img src="images/tables/default_lb.gif"></td>
                    <td style="background-image: url(images/tables/default_b.gif); height: 9px;">
                        <img height="9" src="images/tables/transparent.gif"></td>
                    <td style="height: 9px">
                        <img src="images/tables/default_rb.gif"></td>
                </tr>
            </table>
    
    </div>
    </form>
</body>
</html>

