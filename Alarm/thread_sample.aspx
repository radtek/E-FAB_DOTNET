<%@ Page Language="C#" AutoEventWireup="true" CodeFile="thread_sample.aspx.cs" Inherits="Alarm_thread_sample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Start" />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="End" /><br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="56px" TextMode="MultiLine" Width="650px"></asp:TextBox></div>
    </form>
</body>
</html>
