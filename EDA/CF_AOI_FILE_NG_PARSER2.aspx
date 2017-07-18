﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CF_AOI_FILE_NG_PARSER2.aspx.cs" Inherits="CF_AOI_FILE_NG_PARSER2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CF AOI MONITOR  FILE  TO EDA_NG</title>
     <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
 <script type="text/javascript"> 
document.write('<div id="loadDiv" style="padding-top:150; padding-left:150; font-size:13pt;">'+ '頁面正在載入中......</div>'); 
function window.onload() 
{ 
hiddenDiv.style.display=""; 
loadDiv.removeNode(true); 
} 
</script> 


<body>

 
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
          </asp:ScriptManager>
         

   
   
 <%--<div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">--%>
       
      <div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">
        </div> 
       <div id='hiddenDiv'> 

            <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 9px; width: 10px;">
                        <img src="../images/tables/default_lt.gif" /></td>
                    <td style="background-image: url(../images/tables/default_t.gif); height: 9px; width: 888px;">
                        <img height="9" src="../images/tables/transparent.gif" /></td>
                    <td style="height: 9px; width: 10px;">
                        <img src="../images/tables/default_rt.gif" /></td>
                </tr>
                <tr>
                    <td style="background-image: url(../images/tables/default_l.gif); width: 10px;">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                    <td align="middle" style="width: 888px; height: 500px;">
                        <table  onLoad="alert('Working hard or hardly working?')"  align="center" cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="90%"
                            bordercolorlight="#7a9cb7" border="1">
                            <tr>
                                <td background="" colspan="4" class="pageTitle" style="height: 24px; text-align: left;">
                                    <table width="100%"  >
                                        <tr>
                                            <td align="left" style="width: 533px">
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong> CF AOI/MACRO
                                                    MONITOR&nbsp; FILE TO EDANG -查詢</strong></span></td>
                                            <td align="right" style="font-size: 12px; color: navy">
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
                                <td align="left" style="height: 13px" colspan="3">
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
                            </tr>
                            <tr>
                                <td align="center" class="pageTD" style="width: 15%; height: 13px">
                                    TYPE</td>
                                <td align="left" colspan="3" style="height: 13px">
                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="103px">
                                        <asp:ListItem>AOI</asp:ListItem>
                                        <asp:ListItem>MACRO</asp:ListItem>
                                        <asp:ListItem>MTREPAIR</asp:ListItem>
                                        <asp:ListItem>TAREPAIR</asp:ListItem>
                                        <asp:ListItem>TPLASER</asp:ListItem>
                                        <asp:ListItem>PSHEIGHT</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="pageTD" colspan="8" style="height: 35px; text-align: left;">
                                    <asp:Button ID="Button2" runat="server" Text="Query" OnClick="Button2_Click" />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click"
                                        Text="ExportToExcel" />
                                    &nbsp; 共 &nbsp;
                                    <asp:Label ID="Label1" runat="server" ForeColor="Blue" Text="Label"></asp:Label>
                                    &nbsp; 筆資料&nbsp;&nbsp;
    </td>
                            </tr>
                            <tr>
                                <td  colspan="8" style="height: 35px">
                                    &nbsp; &nbsp;
                                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#DEDFDE"
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
                    <td style="font-size: 12pt; background-image: url(../images/tables/default_r.gif); width: 10px;">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td style="height: 15px; width: 10px;">
                        <img src="../images/tables/default_lb.gif"></td>
                    <td style="background-image: url(../images/tables/default_b.gif); height: 15px;">
                        <img height="9" src="../images/tables/transparent.gif"></td>
                    <td style="height: 15px; width: 10px;">
                        <img src="../images/tables/default_rb.gif"></td>
                       
                </tr>
            
            </table>
   </div>

    </form>
</body>
</html>
