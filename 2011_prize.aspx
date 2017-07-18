<%@ Page Language="C#" AutoEventWireup="true" CodeFile="2011_prize.aspx.cs" Inherits="_2011_prize" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>2011 旺年中獎名單 </title>
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
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>2011旺年會中獎名單</strong></span></td>
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
                                    員工姓名</td>
                                <td style="text-align: left; width: 341px; height: 22px;" valign="top">
                                    <asp:TextBox ID="txt_name" runat="server" Width="146px"></asp:TextBox>
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
                                    <span style="font-size: 14pt"><span style="color: red">Default &nbsp;Page About&nbsp; PIA&nbsp; 中獎人</span><span style="color: red"> , 恭喜中獎 !!!</span></span></td>
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
                                                            <asp:TemplateField HeaderText="prize_name">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprize_name" runat="server" ForeColor="#000000" Text='<%# Bind("prize_name") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblprize_name" runat="server" ForeColor="#000000" Text='<%# Bind("prize_name") %>'></asp:Label></br>
                                                                    <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="prize_item">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprize_item" runat="server" ForeColor="#000000" Text='<%# Bind("prize_item") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblprize_item" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("prize_item") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                            <asp:TemplateField HeaderText="sn">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsn" runat="server" ForeColor="#000000" Text='<%# Bind("sn") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbldep" runat="server" ForeColor="#000000" Width="250px"
                                                                        Text='<%# Bind("sn") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="emp_name">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblemp_name" runat="server" ForeColor="#000000" Text='<%# Bind("emp_name") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblemp_name" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("emp_name") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        
                                                            <asp:TemplateField HeaderText="dep">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldep" runat="server" ForeColor="#000000" Text='<%# Bind("dep") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblreport_id" runat="server" ForeColor="#000000" Width="250px"
                                                                        Text='<%# Bind("dep") %>'></asp:TextBox></br>
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

