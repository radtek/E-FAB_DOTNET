<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vpn_parser.aspx.cs" Inherits="VPN_vpn_parser" %>


<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>T1Vpn Log History 查詢</title>
    <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">
            <br />
            <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
                <tr>
                    <td style="height: 9px; width: 10px;">
                        <img src="../images/tables/default_lt.gif" /></td>
                    <td style="background-image: url(../images/tables/default_t.gif); height: 9px;">
                        <img height="9" src="../images/tables/transparent.gif" /></td>
                    <td style="height: 9px; width: 10px;">
                        <img src="../images/tables/default_rt.gif" /></td>
                </tr>
                <tr>
                    <td style="background-image: url(../images/tables/default_l.gif); width: 10px;">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                    <td align="middle" width="100%">
                        <table align="center" cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="100%"
                            bordercolorlight="#7a9cb7" border="1">
                            <tr>
                                <td background="" colspan="6" class="pageTitle">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" style="height: 30px">
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>T1Vpn Log History 查詢</strong></span></td>
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
                                <td style="text-align: left; width: 135px; height: 22px;" valign="top">
                                    <asp:TextBox ID="txtCalendar1" runat="server" Width="91px"></asp:TextBox><obout:Calendar
                                        ID="Calendar1" runat="server" Columns="1" DateFormat="yyyy/MM/dd" DatePickerImagePath="~/images/calendar.gif"
                                        DatePickerMode="True" FullDayNames="璆,銝,鈭,銝,?鈭,摮" ScriptPath="~/js/" ShortDayNames="璆,銝,鈭,銝,?鈭,摮"
                                        StyleFolder="~/css/" TextBoxId="txtCalendar1">
                                    </obout:Calendar>
                                </td>
                                 <td class="pageTD" align="center" valign="middle" style="height: 22px; width: 10%;
                                    text-align: center;">
                                    資料時間範圍&nbsp;End</td>
                               <td style="text-align: left; width: 209px; height: 22px;" valign="top">
                                    <asp:TextBox ID="txtCalendar2" runat="server" Width="91px"></asp:TextBox><obout:Calendar
                                        ID="Calendar2" runat="server" Columns="1" DateFormat="yyyy/MM/dd" DatePickerImagePath="~/images/calendar.gif"
                                        DatePickerMode="True" FullDayNames="璆,銝,鈭,銝,?鈭,摮" ScriptPath="~/js/" ShortDayNames="璆,銝,鈭,銝,?鈭,摮"
                                        StyleFolder="~/css/" TextBoxId="txtCalendar2">
                                    </obout:Calendar>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td align="center" class="pageTD" style="width: 10%; height: 22px; text-align: center"
                                    valign="middle">
                                    查詢字串1</td>
                                <td style="width: 135px; height: 22px; text-align: left" valign="top">
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>(ex.Oscar.Hsieh)</td>
                                <td align="center" class="pageTD" style="width: 10%; height: 22px; text-align: center"
                                    valign="middle">
                                    &nbsp;</td>
                                <td style="width: 209px; height: 22px; text-align: left" valign="top">
                                    EXPAND
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem>N</asp:ListItem>
                                        <asp:ListItem>Y</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="pageTD" colspan="8" style="height: 35px; text-align: left;">
                                    <table style="width: 236px; height: 7px">
                                        <tr>
                                        
                                        <td align='left' valign='top' style="width: 14px; height: 17px;">
                                                <asp:Button ID="Button_LAODER" runat="server" Style="font-size: 12px; font-family: Arial;
                                                    width: 100px;" Text="Loader" OnClick="Button_LAODER_Click" Visible="False" />
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td align='left' valign='top' style="width: 14px; height: 17px;">
                                                <asp:Button ID="ButtonQuery" runat="server" Style="font-size: 12px; font-family: Arial;
                                                    width: 100px;" Text="Query" OnClick="ButtonQuery_Click" />
                                                &nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td align='left' valign='top' style="width: 25px; height: 17px;">
                                                <asp:Button ID="Button1" runat="server" Text="ExportToExcel" OnClick="Button1_Click" /></td>
                                            <td align="left" style="width: 114px; height: 17px" valign="top">
                                            </td>
                                            <td align="left" style="width: 114px; height: 17px" valign="top">
                                            </td>
                                        </tr>
                                    </table>
                                              Sumary Data 有        
                                                <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                                                筆 &nbsp;&nbsp; / &nbsp;
                                              Row  Data 有        
                                                <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Green"></asp:Label>
                                    筆 &nbsp; (目前系統最新更新資料到
                                    <asp:Label ID="Label3" runat="server" ForeColor="Fuchsia" Text="Label" Width="74px"></asp:Label>)</td>
                            </tr>
                            <tr>
                                <td colspan="8" style="height: 271px">
                                    <fieldset>
                                        <legend align="center" style="color: blue; text-align: center"><strong><span style="font-family: Century Gothic">
                                            查詢結果</span></strong>:</legend>
                                        
                                        <table hight="100%" width="100%">
                                            <tr>
                                            <td align='center' valign='middle'>
                                                &nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound" Height="213px" Width="687px">
                                                    <RowStyle BackColor="#EFF3FB" Font-Names="Century Gothic" Font-Size="Larger" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="SN" />
                                                        <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Image ID="btnShowDetail" runat="server" ImageUrl="~/images/close13.gif" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        <asp:BoundField DataField="login_id" HeaderText="login_id" />
                                                        <asp:BoundField DataField="log_dttm" HeaderText="log_dttm" ReadOnly="True" />
                                                        <asp:BoundField DataField="total_hour" HeaderText="total_hour" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
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
                                                    <asp:BoundField DataField="vpn_item" HeaderText="vpn_item" />
                                                    <asp:BoundField DataField="dttm" HeaderText="dttm" />
                                                    <asp:BoundField DataField="user_id" HeaderText="user_id" />
                                                   <asp:BoundField DataField="login_id" HeaderText="login_id" />
                                                    <asp:BoundField DataField="hour" HeaderText="hour" />
                                                     <asp:BoundField DataField="min" HeaderText="min" />
                                                      <asp:BoundField DataField="sec" HeaderText="sec" />
                                                       <asp:BoundField DataField="total_hour" HeaderText="total_hour" />
                                                       
                                                        <asp:BoundField DataField="log_dttm" HeaderText="log_dttm" />
                                                       
                                                       
                                                  
                                                </Columns>
                                        </asp:GridView>
                                        <asp:Label runat="server" ID="lblAIExpand" Style="display: none"></asp:Label> 
                                            </td>
                                            
                                            </tr>
                                            <tr>
                                                <td align='center' valign='middle'>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal></fieldset>
                                    &nbsp;<span style="font-size: 9pt"></span></td>
                            </tr>
                        </table>
                    </td>
                    <td style="font-size: 12pt; background-image: url(../images/tables/default_r.gif); width: 10px;">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td style="height: 9px; width: 10px;">
                        <img src="../images/tables/default_lb.gif"></td>
                    <td style="background-image: url(../images/tables/default_b.gif); height: 9px;">
                        <img height="9" src="../images/tables/transparent.gif"></td>
                    <td style="height: 9px; width: 10px;">
                        <img src="../images/tables/default_rb.gif"></td>
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