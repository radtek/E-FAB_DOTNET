<%@ Page Language="C#" AutoEventWireup="true" CodeFile="infa_monitor.aspx.cs" Inherits="DB_MONITOR_infa_monitor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Infa_Monitor</title>
</head>
<body>
    
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            </asp:ScriptManager>
   <%--<div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">--%>
       
      <div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">
       
            <table id="Table3" align="left"border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 9px; width: 10px;">
                        <img src="../images/tables/default_lt.gif" /></td>
                    <td style="background-image: url(../images/tables/default_t.gif); height: 9px; width: 888px;">
                        <img height="9" src="../images/tables/transparent.gif" /></td>
                    <td style="height: 9px; width: 12px;">
                        <img src="../images/tables/default_rt.gif" /></td>
                </tr>
                <tr>
                    <td style="background-image: url(../images/tables/default_l.gif); width: 10px;">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                    <td  style="width: 888px; height: 200px; text-align: center;">
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
                    <td style="font-size: 12pt; background-image: url(../images/tables/default_r.gif); width: 12px;">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td style="height: 9px; width: 10px;">
                        <img src="../images/tables/default_lb.gif"></td>
                    <td style="background-image: url(../images/tables/default_b.gif); height: 9px; width: 888px;">
                        <img height="9" src="../images/tables/transparent.gif"></td>
                    <td style="height: 9px; width: 12px;">
                        <img src="../images/tables/default_rb.gif"></td>
                </tr>
            </table>
   </div>

    </form>
</body>
</html>
