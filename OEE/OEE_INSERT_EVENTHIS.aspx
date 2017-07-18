<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OEE_INSERT_EVENTHIS.aspx.cs" Inherits="OEE_OEE_INSERT_EVENTHIS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>OEE_INSERT_EVENTHIS</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        From<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
        <br />
        To &nbsp;&nbsp;
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Execute" /><br />
        <br />
        Source Data count &nbsp;<asp:Label ID="Label1" runat="server" Text="Label" Width="80px"></asp:Label><br />
        <br />
        Destination Data count
        <asp:Label ID="Label2" runat="server" Text="Label" Width="65px"></asp:Label></div>
    </form>
</body>
</html>
