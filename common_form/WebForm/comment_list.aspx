<%@ Page Language="C#" AutoEventWireup="true" CodeFile="comment_list.aspx.cs" Inherits="comment_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Comment List</title>
    <link href="../../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="98%" bordercolorlight="#7a9cb7"
                border="1">
                <tr>
                    <td align="left" background="" colspan="6" class="pageTitle3">
                        <span id="lblTitle" runat="server"> </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <fieldset runat="server" id="fsAttach" visible="false">
                            <legend id="legend1" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
                                color: black">Project 附件 &nbsp;</legend>
                            <asp:DataList ID="dlAttach" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../../images/td/attach.png" /></td>
                                            <td>
                                                <asp:HyperLink runat="server" ID="hyAttach" NavigateUrl='<%# Bind("FILE_Link") %>'
                                                    Text='<%# Bind("FILE_Desc") %>' Font-Size="13px" Target="_blank" ForeColor="blue" />&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <fieldset>
                            <legend id="legend2" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
                                color: black">Process History &nbsp;</legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:DataList ID="dlProcessHistory" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
                                            Width="100%">
                                            <ItemTemplate>
                                                <table width="100%" cellspacing="0" bordercolordark="#ffffff" cellpadding="2" bordercolorlight="#7a9cb7"
                                                    border="1">
                                                    <tr>
                                                        <td align="left" style="background-color: #DEDFDE">
                                                            <img src="../../images/td/process.png" height="18px" style="vertical-align: middle" />
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
