<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CURRENT_COMPONENT_QUERY.aspx.cs" Inherits="CF_CURRENT_COMPONENT_QUERY" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CF Component Query</title>
     <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            </asp:ScriptManager>
   <table  id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
                <tr>
                    <td>
                        <img src="../images/tables/default_lt.gif" /></td>
                    <td style="background-image: url(../images/tables/default_t.gif)">
                        <img height="9" src="../images/tables/transparent.gif" /></td>
                    <td>
                        <img src="../images/tables/default_rt.gif" /></td>
                </tr>
                <tr>
                    <td style="background-image: url(../images/tables/default_l.gif)">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                    <td align="middle" width="100%">
                        <table align="center" cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="100%"
                            bordercolorlight="#7a9cb7" border="1">
                            <tr>
                                <td background="" colspan="4" class="pageTitle">
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>CF Component
                                                    Query</strong></span></td>
                                            <td align="right" style="font-size: 12px; color: navy">
                                                * 表示必填!</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" valign="middle" style="height: 10px; width: 16%;
                                    text-align: center;">
                                    檔案上傳<br />
                                    <br />
                                </td>
                                    <td style="text-align: left; width: 127px; height: 22px;" valign="top">
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                        <ContentTemplate>
                                            &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
                                   
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                        </td>
                                   
                               
                            </tr>
                            <tr style="font-size: 12pt">
                                <td align="center" class="pageTD" style="width: 16%; height: 22px; text-align: center"
                                    valign="middle">
                                    範例檔格式下載</td>
                                <td style="width: 127px; height: 22px; text-align: left" valign="top">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/CF/component_query.txt">EXCEL格式下載</asp:HyperLink><asp:Button
                                        ID="ButtonUpload" runat="server" OnClick="ButtonUpload_Click" Style="font-size: 12px;
                                        width: 100px; font-family: Arial" Text="Upload" /></td>
                            </tr>
                            <tr>
                                <td class="pageTD"  align="left" colspan="8" style="height: 35px">
                                  
                                    
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                    
                                    <asp:Label ID="LabelX" runat="server"></asp:Label>
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="ExportToExcel" /></td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False">
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField  HeaderText="RN" />
                                <asp:BoundField DataField="vendor_lot_id" HeaderText="vendor_lot_id" />
                                <asp:BoundField DataField="lot_id" HeaderText="lot_id" />
                                <asp:BoundField DataField="prod_name" HeaderText="prod_name" />
                                <asp:BoundField DataField="step_name" HeaderText="step_name" />
                                <asp:BoundField DataField="glass_id" HeaderText="glass_id" />
                                <asp:BoundField DataField="last_Track_out" HeaderText="last_Track_out" />
                                <asp:BoundField DataField="cst_id" HeaderText="cst_id" />
                            </Columns>
                        </asp:GridView>
                       
                   
                    </td>
                    <td style="font-size: 12pt; background-image: url(../images/tables/default_r.gif)">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td style="height: 9px">
                        <img src="../images/tables/default_lb.gif"></td>
                    <td style="background-image: url(../images/tables/default_b.gif); height: 9px;">
                        <img height="9" src="../images/tables/transparent.gif"></td>
                    <td style="height: 9px">
                        <img src="../images/tables/default_rb.gif"></td>
                </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
