<%@ Page Language="C#" AutoEventWireup="true" CodeFile="iching.aspx.cs" Inherits="iching" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 417px; height: 159px">
            <tr>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    Range
                </td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    <asp:TextBox ID="TextBox2" runat="server" Width="65px"></asp:TextBox></td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    Repeat
                </td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    <asp:TextBox ID="TextBox3" runat="server" Width="64px"></asp:TextBox></td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    卜卦
                </td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    <asp:TextBox ID="TextBox1" runat="server" Width="183px"></asp:TextBox></td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    上卦
                </td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    <asp:TextBox ID="TextBox4" runat="server" Width="82px"></asp:TextBox>&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="Label" Width="50px"></asp:Label></td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    <asp:TextBox ID="TextBox6" runat="server" Height="17px" Width="64px"></asp:TextBox>&nbsp;
                    <asp:Label ID="Label3" runat="server" Text="Label" Width="50px"></asp:Label></td>
            </tr>
            <tr>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    下卦
                </td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    <asp:TextBox ID="TextBox5" runat="server" Width="81px"></asp:TextBox>&nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Label" Width="50px"></asp:Label></td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    <asp:TextBox ID="TextBox7" runat="server" Width="64px"></asp:TextBox>&nbsp;
                    <asp:Label ID="Label4" runat="server" Text="Label" Width="50px"></asp:Label></td>
            </tr>
            <tr>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    &nbsp;</td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="自動產生查詢" />&nbsp;</td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="輸入查詢" />&nbsp;</td>
            </tr>
            <tr>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    &nbsp;</td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    <asp:HyperLink ID="HyperLink1" runat="server" Width="106px">HyperLink</asp:HyperLink>&nbsp;</td>
                <td style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                    width: 225px; border-bottom: black thin solid; height: 21px; background-color: #ffffcc">
                    <asp:HyperLink ID="HyperLink2" runat="server" Width="123px">HyperLink</asp:HyperLink>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
