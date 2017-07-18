<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pp_tacttime_report.aspx.cs" Inherits="OEE_pp_tacttime_report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>pp tacttime report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" BackColor="#CCCCCC" BorderColor="#999999"
            BorderStyle="Solid" BorderWidth="3px" CellPadding="4" ForeColor="Black"  OnPreRender="GridView1_PreRender" CellSpacing="2">
            <RowStyle BackColor="White" />
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
