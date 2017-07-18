<%@ Page Language="C#" AutoEventWireup="true" CodeFile="project_group_selector.aspx.cs"
    Inherits="common_form_WebForm_project_group_selector" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Group</title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" style="border: 1; border-color: Gray; border-width: 1px; border-style: solid">
            <tr>
                <td align="center">
                    <span style="color: #ffffff"></span>
                    <table style="width: 100%">
                        <tr>
                            <td style="font-weight: bold; color: White; background-color: Gray">
                                <table width="100%">
                                    <tr>
                                        <td align="left" style="font-size: 15px">
                                            Project Group</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table cellspacing="5" bordercolordark="#ffffff" cellpadding="5" width="90%" bordercolorlight="#7a9cb7"
                        border="0">
                        <tr>
                            <td class="pageTD2" align="center" style="width: 30%;">
                                選擇 Project Group
                            </td>
                            <td align="left">
                                <asp:DropDownList runat="server" ID="ddlProjectGroup">
                                </asp:DropDownList>
                                <asp:Button ID="btnSave" runat="server" Text="OK" OnClientClick="return check_ddl_field();" />
                            </td>
                        </tr>
                        <tr>
                            <td class="pageTD2" align="center" style="width: 30%;">
                                新增 Project Group
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtProjectGroup" runat="server" Width="85%" MaxLength="1000"></asp:TextBox>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClientClick="return check_txt_field();"
                                    OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<script language="javascript">
function check_ddl_field()
{
    if( document.getElementById("ddlProjectGroup").value=="")
    {
        alert("請選擇Project Group");
        return false;
    }
    else
    {
        return true;
   }
     
}

function check_txt_field()
{
    if( document.getElementById("txtProjectGroup").value=="")
    {
        alert("請輸入Project Group");
        return false;
    }
    else
    {
        return true;
   }
     
}

</script>