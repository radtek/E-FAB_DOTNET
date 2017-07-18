<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lottory.aspx.cs" Inherits="lottory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>抽籤</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 431px; height: 115px">
            <tr>
                <td>
                </td>
                <td>
                    過年值班抽籤</td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td>
                    多少人</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td>
                    取多少人</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                <td style="width: 100px">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Execute" /></td>
            </tr>
            <tr>
                <td>
                    執行結果</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
       
    
    </div>
    </form>
</body>
</html>
