<%@ Page Language="C#" AutoEventWireup="true" CodeFile="project_assign_mail.aspx.cs"
    Inherits="project_assign_mail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Project Assign Mail</title>
    <link href="app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="2" width="100%" border="1" style="border-color: White">
                <tr>
                    <td align="middle" colspan="6">
                        <span id="lblTitle">Project Assign
                    </td>
                </tr>
                <tr>
                    <td align="center" style="font-weight: bold; font-size: 12px; font-family: Arial;
                        background-color: Silver;">
                        Project Id
                    </td>
                    <td align="left"  colspan="2">
                        <asp:Label ID="lblProjectNo" runat="server" Style="font-size: 13px; font-family: Arial;
                            color: Navy;">
                        </asp:Label>
                    </td>
                    <td align="center" style="font-weight: bold; font-size: 12px; font-family: Arial;
                        background-color: Silver;">
                        Status
                    </td>
                    <td align="left"  colspan="2">
                        <asp:Label ID="lblStatus" runat="server" Style="font-size: 13px; font-family: Arial;
                            color: Navy;"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="font-weight: bold; font-size: 12px; font-family: Arial;
                        background-color: Silver;">
                        Project Description
                    </td>
                    <td align="left" colspan="5">
                        <asp:Label ID="lblProjectDesc" runat="server" Style="font-size: 13px; font-family: Arial;
                            color: Navy;"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="font-weight: bold; font-size: 12px; font-family: Arial;
                        background-color: Silver;">
                        申請者
                    </td>
                    <td align="left">
                        <asp:Label ID="lblAppilcant" runat="server" Style="font-size: 13px; font-family: Arial;
                            color: Navy;"> </asp:Label>
                    </td>
                    <td align="center" style="font-weight: bold; font-size: 12px; font-family: Arial;
                        background-color: Silver;">
                        申請部門
                    </td>
                    <td align="left">
                        <asp:Label ID="lblAppilcantDept" runat="server" Style="font-size: 13px; font-family: Arial;
                            color: Navy;"> </asp:Label>
                    </td>
                    <td align="center" style="font-weight: bold; font-size: 12px; font-family: Arial;
                        background-color: Silver;">
                        申請時間
                    </td>
                    <td align="left">
                        <asp:Label ID="lblApplyDate" runat="server" Style="font-size: 13px; font-family: Arial;
                            color: Navy;"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="font-weight: bold; font-size: 12px; font-family: Arial;
                        background-color: Silver;">
                        詳細的工作內容
                    </td>
                    <td align="left" colspan="5">
                        <asp:HyperLink ID="hyLink" runat="server" Text="請按這裡" Style="font-size: 13px; font-family: Arial;
                            color: Navy;"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <fieldset id="fs1" runat="server" visible="false">
                            <legend id="legend2" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
                                color: black">Process History &nbsp;</legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:DataList ID="dlProcessHistory" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
                                            Width="100%">
                                            <ItemTemplate>
                                                <table width="100%" cellspacing="0" cellpadding="2" 
                                                    border="1" style="border-color:White">
                                                    <tr>
                                                        <td align="left" style="background-color: #DEDFDE">
                                                            <asp:Label ID="lblProcessTitle" Font-Size="12px" runat="server" CssClass="pageLabel"
                                                                Text='<%# Eval("create_user") + " 於  " + Eval("create_dttm")  %>'> </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lblProcessHistory" runat="server" CssClass="pageLabel2" Text='<%# Bind("process_comment") %>'> </asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
