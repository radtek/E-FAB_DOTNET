<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oldToNewWORKNO.aspx.cs" Inherits="oldToNewWORKNO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>舊工號轉新工號</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<br />
        <br />
        &nbsp;
        <br />
        <table style="width: 604px; height: 159px">
            <tr>
                <td>
                </td>
                <td>
                    For InnoView</td>
                <td style="width: 276px">
                </td>
            </tr>
            <tr>
                <td>
                    單一工號 :&nbsp;</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                <td style="width: 276px">
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Convert" /></td>
            </tr>
            <tr>
                <td>
                    多重工號 :</td>
                <td>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                <td style="width: 276px">
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Convert_Multiple" /></td>
            </tr>
            <tr>
                <td style="height: 21px">
                </td>
                <td style="height: 21px">
                    For TFT-Meeting</td>
                <td style="width: 276px; height: 21px">
                </td>
            </tr>
            <tr>
                <td>
                    單一工號 :&nbsp;</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                <td style="width: 276px">
                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Convert" /></td>
            </tr>
            <tr>
                <td style="height: 22px">
                    多重工號 :</td>
                <td style="height: 22px">
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                <td style="width: 276px; height: 22px">
                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Convert_Multiple" />ex. TWN</td>
            </tr>
            <tr>
                <td style="height: 22px">
                </td>
                <td style="height: 22px">
                    For Lam-Meeting</td>
                <td style="width: 276px; height: 22px">
                </td>
            </tr>
            <tr>
                <td style="height: 22px">
                    單一工號 :&nbsp;</td>
                <td style="height: 22px">
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                <td style="width: 276px; height: 22px">
                    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Convert" /></td>
            </tr>
            <tr>
                <td style="height: 22px">
                    多重工號 :</td>
                <td style="height: 22px">
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                <td style="width: 276px; height: 22px">
                    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Convert_Multiple" /></td>
            </tr>
            <tr>
                <td style="height: 22px">
                </td>
                <td style="height: 22px">
                </td>
                <td style="width: 276px; height: 22px">
                </td>
            </tr>
            <tr>
                <td style="height: 22px">
                </td>
                <td style="height: 22px">
                </td>
                <td style="width: 276px; height: 22px">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
