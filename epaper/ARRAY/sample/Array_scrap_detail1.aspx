<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Array_scrap_detail1.aspx.cs" Inherits="epaper_ARRAY_sample_Array_scrap_detail1" %>

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
                <asp:GridView ID="GridView1" runat="server" Font-Names="Century Gothic" Font-Size="X-Small" Width="1000px"
                    AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC"
                    OnRowDataBound="GridView1_RowDataBound" BorderStyle="None" BorderWidth="1px"
                    EmptyDataText="No Record!">
                    <RowStyle ForeColor="#000066" Font-Size="X-Small" />
                    <Columns>
                        <asp:TemplateField HeaderText="RN">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CATEGORY">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                              <asp:Label ID="lblCATEGORY" runat="server" ForeColor="DarkGreen" Text='<%# Bind("CATEGORY") %>'></asp:Label><br> 
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="LOT_ID">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                              <asp:Label ID="lbllot_id" runat="server" ForeColor="DarkGreen" Text='<%# Bind("lot_id") %>'></asp:Label><br> 
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="TYPE">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                              <asp:Label ID="lblTYPE" runat="server" ForeColor="DarkGreen" Text='<%# Bind("TYPE") %>'></asp:Label><br> 
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="SCRAP_TYPE">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                              <asp:Label ID="lblSCRAP_TYPE" runat="server" ForeColor="DarkGreen" Text='<%# Bind("SCRAP_TYPE") %>'></asp:Label><br> 
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="QTY">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                              <asp:Label ID="lblQTY" runat="server" ForeColor="DarkGreen" Text='<%# Bind("QTY") %>'></asp:Label><br> 
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="STAGE">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                              <asp:Label ID="lblstage" runat="server" ForeColor="DarkGreen" Text='<%# Bind("stage") %>'></asp:Label><br> 
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="SCRAP_TIME">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                              <asp:Label ID="lblScrapTime" runat="server" ForeColor="DarkGreen" Text='<%# Bind("ScrapTime") %>'></asp:Label><br> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCRAP_CMMT">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                              <asp:Label ID="lblSCRAP_CMMT" runat="server" ForeColor="DarkGreen" Text='<%# Bind("SCRAP_CMMT") %>'></asp:Label><br> 
                            </ItemTemplate>
                        </asp:TemplateField>
            
                       
                        
                       
                    </Columns>
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" Wrap="True" />
                </asp:GridView>
             <table border="0" cellpadding="0" cellspacing="0" style="background-color: white"
                width="90%">
                <tr>
                    <td bgcolor="gray" height="28" style="font-size: 11px; color: #ffffff; line-height: 16px;
                        font-family: Verdana,?啁敦??; text-align: center; text-decoration: none">
                        群創光電股份有限公司 版權所有 Copyright &copy; 2010 Chimei-Innolux  Corp., Design By CIM 謝正一(64179)</td>
                </tr>
</table></fieldset>
            &nbsp;<br />
    </form>
</body>
</html>
