<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoveTacttimeCheck.aspx.cs"
    Inherits="epaper_ARRAY_sample_MoveTacttimeCheck" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MoveTacttimeCheck</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Get Movements &nbsp;But&nbsp; No TactTime Data ( Equip Data List) &nbsp;<br />
            <br />
            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#DEDFDE"
                BorderStyle="None" BorderWidth="1px" CellPadding="4" EmptyDataText="No Record!"
                ForeColor="Black" GridLines="Vertical" Height="199px" OnRowDataBound="GridView1_RowDataBound"
                Width="385px" AutoGenerateColumns="False">
                <RowStyle BackColor="#F7F7DE" />
                <Columns>
                    <asp:BoundField HeaderText="SN" />
                    <asp:BoundField DataField="SHIFTDATE" HeaderText="SHIFTDATE" />
                    <asp:TemplateField HeaderText="EQUIPMENTID">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:HyperLink ID="EQUIPMENTID" Target="_blank" NavigateUrl='http://10.56.131.22/oeeweb2_2005/rpt-item/ProcessView/GlassHistory.aspx'
                                Text='<%# Bind("EQUIPMENTID") %>' ForeColor="#0000FF" runat="server"></asp:HyperLink>
                        </ItemTemplate>
   
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="STATUS">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:HyperLink ID="STATUS" Target="_blank" NavigateUrl='http://10.56.131.22/oeeweb2_2005/rpt-item/EQPMonitor/EQPStatusMonitor1.aspx'
                                Text='<%# Bind("STATUS") %>' ForeColor="#808080" runat="server"></asp:HyperLink>
                        </ItemTemplate>
   
                    </asp:TemplateField>
                  
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
