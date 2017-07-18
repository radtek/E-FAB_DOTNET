<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false"  CodeFile="UPLOAD.aspx.cs" Inherits="UPLOAD" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>資料上傳</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span style="font-family: Century Gothic">FileUpload  For <span style="color: #ff0066">
            <strong>夜巡</strong></span>&nbsp; 
            <hr />
       
        1.        </span>
        <asp:FileUpload ID="FileUpload1" runat="server" /><span style="font-family: Century Gothic">
        <br />
        2.        </span>
        <asp:FileUpload ID="FileUpload2" runat="server" /><span style="font-family: Century Gothic">
        <br />
        3.        </span>
        <asp:FileUpload ID="FileUpload3" runat="server" /><span style="font-family: Century Gothic">
        <br />
        4.        </span>
        <asp:FileUpload ID="FileUpload4" runat="server" /><span style="font-family: Century Gothic">
        <br />
        5.        </span>
        <asp:FileUpload ID="FileUpload5" runat="server" /><span style="font-family: Century Gothic">
        <hr />&nbsp;&nbsp; </span>

        <asp:Button ID="Button1" runat="server" Text="大量檔案，批次上傳！" onclick="Button1_Click" /><span
            style="font-family: Century Gothic"> 
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="關閉視窗" /></span></div>
    <p>
        <asp:Label ID="Label3" runat="server" Height="18px" Width="208px" ForeColor="#FF3300" Visible="False"></asp:Label><span
            style="font-family: Century Gothic"> </span>
    </p>
        <p>
        <asp:Label ID="Label1" runat="server" ForeColor="#FF3300"></asp:Label><span style="font-family: Century Gothic">
            <br />
        </span>

        <asp:Label ID="Label2" runat="server" ForeColor="blue"></asp:Label><span style="font-family: Century Gothic">
            </span>
        </p>
        <p>
            &nbsp;</p>
</form>


</body>
</html>
