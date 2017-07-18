<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEST_TEMPLATE.aspx.cs" Inherits="Gridview_Example_TEST_TEMPLATE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server">
            <EmptyDataTemplate>
                姓名<br />
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br />
            </EmptyDataTemplate>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
