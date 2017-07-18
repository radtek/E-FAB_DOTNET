<%@ Page Language="C#" AutoEventWireup="true" CodeFile="report_creat_time.aspx.cs"
    Inherits="report_creat_time" %>

<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>Report 產生 Interval Time</title>
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
                                <td background="" colspan="4" class="pageTitle" style="height: 59px">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" style="height: 30px">
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>查詢Report
                                                    產生 Interval Time</strong></span></td>
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
                                            <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound">
                                            <RowStyle BackColor="#F7F7DE" />
                                            <FooterStyle BackColor="#CCCC99" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField  HeaderText="SN" />
                                                    <asp:BoundField DataField="SHOP" HeaderText="SHOP" />
                                                    <asp:BoundField DataField="PROCEDURENAME" HeaderText="PROCEDURENAME" />
                                                    <asp:BoundField DataField="LASTRUNTM" HeaderText="LASTRUNTM" />
                                                   <asp:BoundField DataField="LASTRUNSYSDATE" HeaderText="LASTRUNSYSDATE" />
                                                    <asp:BoundField DataField="LASTRUNMAXTM" HeaderText="LASTRUNMAXTM" />
                                                     <asp:BoundField DataField="CHK_TIME" HeaderText="CHK_TIME" />
                                                      <asp:BoundField DataField="DESCRIBE" HeaderText="DESCRIBE" />
                                                       <asp:BoundField DataField="INSPECTOR" HeaderText="INSPECTOR" />
                                                       
                                                        <asp:BoundField DataField="DIFFER_1" HeaderText="DIFFER_1" />
                                                        <asp:BoundField DataField="DIFFER_2" HeaderText="DIFFER_2" />
                                                        <asp:BoundField DataField="JUDGE" HeaderText="JUDGE" />
                                                       
                                                  
                                                </Columns>
                                        </asp:GridView>
                                            </td>
                                            
                                            </tr>
                                            <tr>
                                                <td align='center' valign='middle'>
                                                    <span style="font-size: 16pt;"><span><span style="color: lightsalmon"><strong>ALCS Hourly Check(Alarm mechanism</strong><strong>)</strong></span></span></span><asp:GridView ID="GridView5" runat="server" Font-Names="Century Gothic" Font-Size="13pt"
                                                        Width="1000px" AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView5_RowDataBound"
                                                        EmptyDataText="No Record!!!">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MAX_DTTM">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAX_DTTM" runat="server" ForeColor="#000000" Text='<%# Bind("MAX_DTTM") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblMAX_DTTM" runat="server" ForeColor="#000000" Text='<%# Bind("MAX_DTTM") %>'></asp:Label></br>
                                                                    <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="DTTM">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDTTM" runat="server" ForeColor="#000000" Text='<%# Bind("DTTM") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblDTTM" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("DTTM") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="COUNTER">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCOUNTER" runat="server" ForeColor="#000000" Text='<%# Bind("COUNTER") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblCOUNTER" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("COUNTER") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                           
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                              <tr>
                                                <td align='center' valign='middle'>
                                                    <span style="font-size: 16pt;"><span style="color: #3300ff"><strong>T1NewAlarmServer Info(Alarm mechanism)</strong></span></span><asp:GridView ID="GridView7" runat="server" Font-Names="Century Gothic" Font-Size="13pt"
                                                        Width="1000px" AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView7_RowDataBound"
                                                        EmptyDataText="No Record!!!">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MAX_DTTM">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAX_DTTM" runat="server" ForeColor="#000000" Text='<%# Bind("MAX_DTTM") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblMAX_DTTM" runat="server" ForeColor="#000000" Text='<%# Bind("MAX_DTTM") %>'></asp:Label></br>
                                                                    <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="DTTM">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldttm" runat="server" ForeColor="#000000" Text='<%# Bind("dttm") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbldttm" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("dttm") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="COUNTER">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCOUNTER" runat="server" ForeColor="#000000" Text='<%# Bind("COUNTER") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblCOUNTER" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("COUNTER") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="DIFF_MIN">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDIFF_MIN" runat="server" ForeColor="#000000" Text='<%# Bind("DIFF_MIN") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblDIFF_MIN" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("DIFF_MIN") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                         
                                                          
                                                           
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='center' valign='middle'>
                                                    <span style="font-size: 16pt;"><span style="color: blueviolet"><strong>OEE IndexSummary Hourly Report(Alarm mechanism</strong><strong>)</strong></span></span><asp:GridView ID="GridView4" runat="server" Font-Names="Century Gothic" Font-Size="13pt"
                                                        Width="1000px" AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView4_RowDataBound"
                                                        EmptyDataText="No Record!!!">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                            <asp:TemplateField HeaderText="LINE">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblline" runat="server" ForeColor="#000000" Text='<%# Bind("LINE") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblline" runat="server" ForeColor="#000000" Text='<%# Bind("LINE") %>'></asp:Label></br>
                                                                    <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="CUTOFFKEY">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCUTOFFKEY" runat="server" ForeColor="#000000" Text='<%# Bind("CUTOFFKEY") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblCUTOFFKEY" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("CUTOFFKEY") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="COUNTER">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCOUNTER" runat="server" ForeColor="#000000" Text='<%# Bind("COUNTER") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblCOUNTER" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("COUNTER") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                           
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td align='center' valign='middle'>
                                                    <span style="font-size: 16pt;"><span><span style="color: slategray"><strong>OEE MIDGW
                                                        SEND MSG Report(Alarm mechanism</strong><strong>)</strong></span></span></span><asp:GridView ID="GridView9" runat="server" Font-Names="Century Gothic" Font-Size="13pt"
                                                        Width="1000px" AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView9_RowDataBound"
                                                        EmptyDataText="No Record!!!">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                                <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SHOP">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSHOP" runat="server" ForeColor="#000000" Text='<%# Bind("SHOP") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblSHOP" runat="server" ForeColor="#000000" Text='<%# Bind("SHOP") %>'></asp:Label></br>
                                                                        <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="RECEIVEDTIME">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRECEIVEDTIME" runat="server" ForeColor="#000000" Text='<%# Bind("RECEIVEDTIME") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblRECEIVEDTIME" runat="server" ForeColor="#000000" Text='<%# Bind("RECEIVEDTIME") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DIFF_MIN">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDIFF_MIN" runat="server" ForeColor="#000000" Text='<%# Bind("DIFF_MIN") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblDIFF_MIN" runat="server" ForeColor="#000000" Text='<%# Bind("DIFF_MIN") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="FLAG">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFLAG" runat="server" ForeColor="#000000" Text='<%# Bind("FLAG") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblFLAG" runat="server" ForeColor="#000000" Text='<%# Bind("FLAG") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        
                                             
                                             <tr>
                                                <td align='center' valign='middle'>
                                                    <span style="font-size: 16pt;"><span style="color: mediumspringgreen"><span
                                                        style="color: royalblue"><strong>OEE DB Information(Alarm mechanism)</strong><br />
                                                    </span>
                                                        <asp:GridView ID="GridView11" runat="server" Font-Names="Century Gothic" Font-Size="13pt"
                                                        Width="1000px" AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView6_RowDataBound"
                                                        EmptyDataText="No Record!!!">
                                                            <RowStyle ForeColor="Black" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                                <asp:TemplateField HeaderText="line">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblline" runat="server" ForeColor="#000000" Text='<%# Bind("line") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblline" runat="server" ForeColor="#000000" Text='<%# Bind("line") %>'></asp:Label></br>
                                                                        <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="max_time">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblmax_time" runat="server" ForeColor="#000000" Text='<%# Bind("max_time") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblmax_time" runat="server" ForeColor="#000000" Text='<%# Bind("max_time") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="DIFF_MIN">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldiff_min" runat="server" ForeColor="#000000" Text='<%# Bind("diff_min") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lbldiff_min" runat="server" ForeColor="#000000" Text='<%# Bind("diff_min") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField HeaderText="FLAG">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFLAG" runat="server" ForeColor="#000000" Text='<%# Bind("FLAG") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblFLAG" runat="server" ForeColor="#000000" Text='<%# Bind("FLAG") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                        </asp:GridView>
                                                        <br />
                                                        <strong><span style="color: #9900ff">OEE GET RPT MOVE/SCRAP/REWORK(Alarm mechanism)</span></strong><asp:GridView ID="GridView12" runat="server" Font-Names="Century Gothic" Font-Size="13pt"
                                                        Width="1000px" AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView6_RowDataBound"
                                                        EmptyDataText="No Record!!!">
                                                            <RowStyle ForeColor="Black" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SHOP">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSHOP" runat="server" ForeColor="#000000" Text='<%# Bind("SHOP") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lblSHOP" runat="server" ForeColor="#000000" Text='<%# Bind("SHOP") %>'></asp:Label></br>
                                                                        <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PROCEDURENAME">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPROCEDURENAME" runat="server" ForeColor="#000000" Text='<%# Bind("PROCEDURENAME") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblPROCEDURENAME" runat="server" ForeColor="#000000" Text='<%# Bind("PROCEDURENAME") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="LASTRUNSTARTTIME">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLASTRUNSTARTTIME" runat="server" ForeColor="#000000" Text='<%# Bind("LASTRUNSTARTTIME") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblLASTRUNSTARTTIME" runat="server" ForeColor="#000000" Text='<%# Bind("LASTRUNSTARTTIME") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="LASTRUNENDTIME">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLASTRUNENDTIME" runat="server" ForeColor="#000000" Text='<%# Bind("LASTRUNENDTIME") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblLASTRUNENDTIME" runat="server" ForeColor="#000000" Text='<%# Bind("LASTRUNENDTIME") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="LASTDATATM">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLASTDATATM" runat="server" ForeColor="#000000" Text='<%# Bind("LASTDATATM") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblLASTDATATM" runat="server" ForeColor="#000000" Text='<%# Bind("LASTDATATM") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="ISRUN">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblISRUN" runat="server" ForeColor="#000000" Text='<%# Bind("ISRUN") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblISRUN" runat="server" ForeColor="#000000" Text='<%# Bind("ISRUN") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                               
                                                                
                                                                
                                                                 <asp:TemplateField HeaderText="DIFF">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDIFF" runat="server" ForeColor="#000000" Text='<%# Bind("DIFF") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblDIFF" runat="server" ForeColor="#000000" Text='<%# Bind("DIFF") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                  <asp:TemplateField HeaderText="DIFF2">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDIFF2" runat="server" ForeColor="#000000" Text='<%# Bind("DIFF2") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblDIFF2" runat="server" ForeColor="#000000" Text='<%# Bind("DIFF2") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                
                                                                  <asp:TemplateField HeaderText="FLAG">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFLAG" runat="server" ForeColor="#000000" Text='<%# Bind("FLAG") %>'></asp:Label></br>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="lblFLAG" runat="server" ForeColor="#000000" Text='<%# Bind("FLAG") %>'
                                                                            Width="250px"></asp:TextBox></br>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                        </asp:GridView>
                                                        <strong>
                                                        <br />
                                                        OEE Partition Information</strong></span></span><asp:GridView ID="GridView6" runat="server" Font-Names="Century Gothic" Font-Size="13pt"
                                                        Width="1000px" AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView6_RowDataBound"
                                                        EmptyDataText="No Record!!!">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                            <asp:TemplateField HeaderText="min_partition">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmin_partion" runat="server" ForeColor="#000000" Text='<%# Bind("min_partition") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblmin_partion" runat="server" ForeColor="#000000" Text='<%# Bind("min_partition") %>'></asp:Label></br>
                                                                    <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="max_partition">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmax_partition" runat="server" ForeColor="#000000" Text='<%# Bind("max_partition") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblmax_partition" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("max_partition") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                         
                                                          
                                                           
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                    </asp:GridView>
                                                    <strong><span style="font-size: 16pt; color: #330099">OEE ST Weekly Information</span></strong>
                                                    <br />
                                                    <asp:GridView ID="GridView8" runat="server" Font-Names="Century Gothic" Font-Size="13pt"
                                                        Width="1000px" AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView8_RowDataBound"
                                                        EmptyDataText="No Record!!!">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TYPE">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmin_partion" runat="server" ForeColor="#000000" Text='<%# Bind("TYPE") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblmin_partion" runat="server" ForeColor="#000000" Text='<%# Bind("TYPE") %>'></asp:Label></br>
                                                                    <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="COUNT">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCOUNT" runat="server" ForeColor="#000000" Text='<%# Bind("COUNT") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblCOUNT" runat="server" ForeColor="#000000" Text='<%# Bind("COUNT") %>'
                                                                        Width="250px"></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="CUTOFFKEY">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCUTOFFKEY" runat="server" ForeColor="#000000" Text='<%# Bind("CUTOFFKEY") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblCUTOFFKEY" runat="server" ForeColor="#000000" Text='<%# Bind("CUTOFFKEY") %>'
                                                                        Width="250px"></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="STARTTIME">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSTARTTIME" runat="server" ForeColor="#000000" Text='<%# Bind("STARTTIME") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblSTARTTIME" runat="server" ForeColor="#000000" Text='<%# Bind("STARTTIME") %>'
                                                                        Width="250px"></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="ENDTIME">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblENDTIME" runat="server" ForeColor="#000000" Text='<%# Bind("ENDTIME") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblENDTIME" runat="server" ForeColor="#000000" Text='<%# Bind("ENDTIME") %>'
                                                                        Width="250px"></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="DURATION">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDURATION" runat="server" ForeColor="#000000" Text='<%# Bind("DURATION") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblDURATION" runat="server" ForeColor="#000000" Text='<%# Bind("DURATION") %>'
                                                                        Width="250px"></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                    </asp:GridView>
                                                    <strong><span style="font-size: 16pt; color: #ff3399">OEE North Side Infomation</span></strong>
                                                    <asp:GridView ID="GridView10" runat="server" Font-Names="Century Gothic" Font-Size="13pt"
                                                        Width="1000px" AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView10_RowDataBound"
                                                        EmptyDataText="No Record!!!">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="SHIFTDATE">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSHIFTDATE" runat="server" ForeColor="#000000" Text='<%# Bind("SHIFTDATE") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblSHIFTDATE" runat="server" ForeColor="#000000" Text='<%# Bind("SHIFTDATE") %>'
                                                                        Width="250px"></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ITEM_NAME">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblITEM_NAME" runat="server" ForeColor="#000000" Text='<%# Bind("ITEM_NAME") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblITEM_NAME" runat="server" ForeColor="#000000" Text='<%# Bind("ITEM_NAME") %>'
                                                                        Width="250px"></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='center' valign='middle'>
                                                    <strong><span style="font-size: 16pt; color: forestgreen">CF Report Error Log</span></strong><asp:GridView ID="GridView1" runat="server" Font-Names="Century Gothic" Font-Size="13pt"
                                                        Width="1000px" AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView1_RowDataBound"
                                                        EmptyDataText="No Record!!!">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                            <asp:TemplateField HeaderText="procedurename">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblshop" runat="server" ForeColor="#000000" Text='<%# Bind("procedurename") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblshop" runat="server" ForeColor="#000000" Text='<%# Bind("procedurename") %>'></asp:Label></br>
                                                                    <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="errmsg">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAIL_FLAG" runat="server" ForeColor="#000000" Text='<%# Bind("errmsg") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblMAIL_FLAG" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("errmsg") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                          
                                                            <asp:TemplateField HeaderText="counter">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblendtime" runat="server" ForeColor="#000000" Text='<%# Bind("counter") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblendtime" runat="server" ForeColor="#000000" Width="250px"
                                                                        Text='<%# Bind("counter") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                        
                                                          
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align='center' valign='middle'>
                                                    <strong><span style="font-size: 16pt; color: red">OEE Daily Report<br />
                                                    </span></strong>
                                                
                                                    <asp:GridView ID="GridView3" runat="server" Font-Names="Century Gothic" Font-Size="13pt"
                                                        Width="1000px" AutoGenerateColumns="False" CellPadding="3" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView3_RowDataBound"
                                                        EmptyDataText="No Record!!!">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                            <asp:TemplateField HeaderText="shop">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblshop" runat="server" ForeColor="#000000" Text='<%# Bind("shop") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblshop" runat="server" ForeColor="#000000" Text='<%# Bind("shop") %>'></asp:Label></br>
                                                                    <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="MAIL_FLAG">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAIL_FLAG" runat="server" ForeColor="#000000" Text='<%# Bind("MAIL_FLAG") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblMAIL_FLAG" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("MAIL_FLAG") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="start_time">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstart_time" runat="server" ForeColor="#000000" Text='<%# Bind("start_time") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblstart_time" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("start_time") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                            <asp:TemplateField HeaderText="endtime">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblend_time" runat="server" ForeColor="#000000" Text='<%# Bind("end_time") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblendtime" runat="server" ForeColor="#000000" Width="250px"
                                                                        Text='<%# Bind("endtime") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                        
                                                            <asp:TemplateField HeaderText="report_id">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblreport_id" runat="server" ForeColor="#000000" Text='<%# Bind("report_id") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblreport_id" runat="server" ForeColor="#000000" Width="250px"
                                                                        Text='<%# Bind("report_id") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="server_ip">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblserver_ip" runat="server" ForeColor="#000000" Text='<%# Bind("server_ip") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblserver_ip" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("server_ip") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            
                                            
                                        </table>
                                    </fieldset>
                                    &nbsp;<span style="font-size: 9pt"></span></td>
                            </tr>
                        </table>
                    </td>
                    <td style="font-size: 12pt; background-image: url(images/tables/default_r.gif); width: 10px;">
                        <img src="images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td style="height: 9px; width: 10px;">
                        <img src="images/tables/default_lb.gif"></td>
                    <td style="background-image: url(images/tables/default_b.gif); height: 9px;">
                        <img height="9" src="images/tables/transparent.gif"></td>
                    <td style="height: 9px; width: 10px;">
                        <img src="images/tables/default_rb.gif"></td>
                </tr>
            </table>
    </form>
</body>
</html>

<script language="javascript"> 
function check_field() 
{ 
if( document.getElementById("TextBox1").value=="") 
{ 
alert("請輸入 紀錄人員!!!"); 
return false; 
} 
else if( document.getElementById("txtEstimateStartDate").value=="") 
{ 
alert("請輸入 發生時間!!!"); 
return false; 
} 
else if( document.getElementById("DropDownList1").value=="請選擇") 
{ 
alert("請選擇 異常種類!!!"); 
return false; 
} 
else if( document.getElementById("DropDownList2").value=="請選擇" ) 
{ 
alert("請選擇 異常發生廠區!!!"); 
return false; 
} 

else if( document.getElementById("DropDownList3").value=="請選擇" ) 
{ 
alert("請選擇 部門!!!"); 
return false; 
} 

else if( document.getElementById("TextBox5").value=="" ) 
{ 
alert("請輸入 異常狀況敘述!!!"); 
return false; 
} 

else if( document.getElementById("TextBox4").value=="" ) 
{ 
alert("請輸入 通知廠區負責人!!!"); 
return false; 
} 




else 
{ 
return true; 
} 

} 


function AddTask() 
{ 
// w = window.open("task_apply.aspx?project_id="+ document.getElementById('lblProjectNo').innerText ,"Add_task", "height=600, width=950, left=200, top=150, " + "location=no, menubar=no, resizable=yes, " + "scrollbars=yes, titlebar=no, toolbar=no", true); 
// w.focus(); 
return false; 
} 

function OpenTask(task_id) 
{ 
// w = window.open("task_assign.aspx?task_id="+ task_id ,"task_maintain", "height=600, width=950, left=200, top=150, " + "location=yes, menubar=yes, resizable=yes, " + "scrollbars=yes, titlebar=no, toolbar=yes", true); 
// w.focus(); 
return false; 
} 

function showHideAnswer(obj,imgObj) 
{ 
if (document.getElementById(obj) == null) 
return; 
if(document.getElementById(obj).style.display=='none'){ 
document.getElementById(imgObj).src = "../images/close13.gif"; 
document.getElementById(obj).style.display='block'; 
}else{ 
document.getElementById(imgObj).src = "../images/open13.gif"; 
document.getElementById(obj).style.display='none'; 
} 
} 
</script>

