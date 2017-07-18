<%@ Page Language="C#" AutoEventWireup="true" CodeFile="st_tacttime_config.aspx.cs" Inherits="OEE_st_tacttime_config" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>st tacttime config remind</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span style="font-size: 14pt"><span style="font-family: Century Gothic">
        <strong><span style="color: #0000ff">
            T1OEE &nbsp; ST Tactime&nbsp; Setting &nbsp;Config&nbsp; Remind</span></strong><br />
        </span></span>
        <br />
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#DEDFDE"
            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GridView1_RowDataBound">
            <RowStyle BackColor="#F7F7DE" />
            <FooterStyle BackColor="#CCCC99" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="SN" />
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
