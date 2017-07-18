<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDA_LCM_CHK_REPORT.aspx.cs" Inherits="EDA_EDA_LCM_CHK_REPORT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EDA LCM Check Report</title>
</head>
<body style="font-size: 12pt">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <span style="color: #0000ff"><span style="font-family: Century Gothic">
            <br />
            <br />
            <strong>
            T1 Defect File 傳輸 Daily Check(外賣/NA不會產檔</strong><strong>)<br />
            <br />
            </strong>
        </span>
            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#DEDFDE"
                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GridView1_RowDataBound"
                Height="26px" Width="440px" EmptyDataText="Today No Data!!!">
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
            <strong></strong>
        </span>
    
    </div>
    </form>
</body>
</html>
