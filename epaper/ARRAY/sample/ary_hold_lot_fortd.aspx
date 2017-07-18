<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ary_hold_lot_fortd.aspx.cs" Inherits="epaper_ARRAY_sample_ary_hold_lot_fortd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title> ARY HOLD LOT HOURLY FOR TD</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        ARY HOLD LOT HOURLY FOR TD<br />
        <br />
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#DEDFDE"
            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" EmptyDataText="No Record!" OnRowDataBound="GridView1_RowDataBound" GridLines="Vertical">
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
