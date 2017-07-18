<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Array_scrap_detail.aspx.cs"
    Inherits="epaper_ARRAY_sample_Array_scrap_detail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Scrap_Detail-Data</title>
    <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">
            <fieldset>
                <legend align="center" style="width: 167px; color: blue; text-align: center">Daily
                    ScrapDetail </legend>
                <asp:GridView ID="GridView1" runat="server" Font-Names="Arial" Font-Size="12px" Width="1000px"
                    AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC"
                    OnRowDataBound="GridView1_RowDataBound" BorderStyle="None" BorderWidth="1px"
                    EmptyDataText="No Record!">
                    <RowStyle ForeColor="#000066" />
                    <Columns>
                        <asp:TemplateField HeaderText="RN">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LOT_ID">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                               LOT_ID:<br>
                                <asp:Label ID="lblLOT_ID" runat="server" ForeColor="DarkGreen" Text='<%# Bind("LOT_ID") %>'></asp:Label><br>
                                PRODUCT:<br>
                                <asp:Label ID="lblPRODUCT" runat="server" ForeColor="DarkGreen" Text='<%# Bind("PRODUCT") %>'></asp:Label><br>
                                GLASS_ID:<br>
                                <asp:Label ID="lblGLASS_ID" runat="server" ForeColor="DarkGreen" Text='<%# Bind("GLASS_ID") %>'></asp:Label><br>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TYPE">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                                TYPE:<br>
                                <asp:Label ID="lblLOT_TYPE" runat="server" ForeColor="DarkGreen" Text='<%# Bind("LOT_TYPE") %>'></asp:Label><br>
                                STAGE:<br>
                                <asp:Label ID="lblabnormal_area" runat="server" ForeColor="DarkGreen" Text='<%# Bind("STAGE") %>'></asp:Label><br>
                                STEP_NO:<br>
                                <asp:Label ID="lbldep" runat="server" ForeColor="DarkGreen" Text='<%# Bind("STEP_NO") %>'></asp:Label><br>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PRIORITY">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                                PRIORITY:<br>
                                <asp:Label ID="lblPRIORITY" runat="server" ForeColor="DarkGreen" Text='<%# Bind("PRIORITY") %>'></asp:Label><br>
                                SCRAP_TIME:<br>
                                <asp:Label ID="lblSCRAP_TIME" runat="server" ForeColor="DarkGreen" Text='<%# Bind("SCRAP_TIME") %>'></asp:Label><br>
                                REASON_CODE:<br>
                                <asp:Label ID="lblSCRAP_REASON_CODE" runat="server" ForeColor="DarkGreen" Text='<%# Bind("SCRAP_REASON_CODE") %>'></asp:Label><br>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCRAP_CMMT">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                                SCRAP_CMMT:<br>
                                <asp:Label ID="lblSCRAP_CMMT" runat="server" ForeColor="DarkGreen" Text='<%# Bind("SCRAP_CMMT") %>'></asp:Label><br>
                                TAID:<br>
                                <asp:Label ID="lblTAID" runat="server" ForeColor="DarkGreen" Text='<%# Bind("TAID") %>'></asp:Label><br>
                                SCRAP_UNSCRAP:<br>
                                <asp:Label ID="lblSCRAP_UNSCRAP" runat="server" ForeColor="DarkGreen" Text='<%# Bind("SCRAP_UNSCRAP") %>'></asp:Label><br>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                </asp:GridView>
             <table border="0" cellpadding="0" cellspacing="0" style="background-color: white"
                width="90%">
                <tr>
                    <td bgcolor="gray" height="28" style="font-size: 11px; color: #ffffff; line-height: 16px;
                        font-family: Verdana,?啁敦??; text-align: center; text-decoration: none">
                        奇美電子股份有限公司 版權所有 Copyright &copy; 2010 Chimei-Innolux  Corp., Design By CIM 謝正一(64179)</td>
                </tr>
</table></fieldset>
            &nbsp;<br />
    </form>
</body>
</html>
