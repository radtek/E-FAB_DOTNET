<%@ Page Language="C#" AutoEventWireup="true" CodeFile="defect_type_maintain.aspx.cs"
    Inherits="control_limit_maintain_defect_type_maintain" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>品質監控系統-Defect Type維護</title>
    <meta http-equiv="Content-Type" content="text/html; charset=big5" />
    <meta http-equiv="Page-Enter" content="blendTrans(duration=0.5)" />
    <meta http-equiv="Page-Exit" content="blendTrans(duration=0.5)" />
</head>
<body background="images/bg_line2.gif">
    <form id="form1" runat="server">
        <div>
            <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" style="background-image: url(images/bg23.gif)">
                <tr>
                    <td>
                        <img src="images/tables/default_lt.gif" /></td>
                    <td style="background-image: url(images/tables/default_t.gif)">
                        <img height="9" src="images/tables/transparent.gif" /></td>
                    <td>
                        <img src="images/tables/default_rt.gif" /></td>
                </tr>
                <tr>
                    <td style="background-image: url(images/tables/default_l.gif)">
                        <img src="images/tables/transparent.gif" width="9"></td>
                    <td align="middle">
                        <table cellspacing="0" cellpadding="0" width="750" border="0">
                            <tr>
                                <td align="left" colspan="3" style="height: 100px">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="0" bordercolordark="#ffffff" cellpadding="1" width="750" bordercolorlight="#7a9cb7"
                                                    border="1">
                                                    <tr>
                                                        <td align="middle" background="" colspan="4" height="40" style="background-image: url(images/tables/title.jpg)">
                                                            <span id="lblTitle" style="font-weight: bold;">
                                                                <hr style="font-size: 18px; color: navy">
                                                            </span><strong><span style="font-size: 18px; color: navy">Array 會議新增</span></strong></td>
                                                    </tr>
                                                </table>
                                                <table cellspacing="0" bordercolordark="#ffffff" cellpadding="1" width="750" bordercolorlight="#7a9cb7"
                                                    border="1">
                                                    <tr>
                                                        <td style="background-image: url(images/bg_gray.gif); width: 15%; height: 29px;" align="right">
                                                            <span id="lblDescription" style="font-weight: bold; font-size: 11px; font-family: Arial">
                                                                Defect Type </span>
                                                        </td>
                                                        <td align="left" style="height: 29px; width: 35%">
                                                            <asp:TextBox ID="txtDefectType" runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td style="background-image: url(images/bg_gray.gif); width: 15%; height: 29px;" align="right">
                                                            <span id="Span1" style="font-weight: bold; font-size: 11px; font-family: Arial">&nbsp;</span></td>
                                                        <td align="left" style="height: 29px; width: 35%">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="background-image: url(images/bg_gray.gif); width: 15%;
                                                            height: 29px">
                                                            <strong><span style="font-size: 8pt; font-family: Arial">Defect
                                                                Desc</span></strong></td>
                                                        <td align="left" style="width: 35%; height: 29px">
                                                            <asp:TextBox ID="txtDefectDesc" runat="server" Width="300px"></asp:TextBox></td>
                                                        <td align="right" style="background-image: url(images/bg_gray.gif); width: 15%;
                                                            height: 29px">
                                                        </td>
                                                        <td align="left" style="width: 35%; height: 29px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="background-image: url(images/bg_gray.gif); width: 15%;
                                                            height: 29px">
                                                        </td>
                                                        <td align="left" style="width: 35%; height: 29px">
                                                        </td>
                                                        <td align="right" style="background-image: url(images/bg_gray.gif); width: 15%;
                                                            height: 29px">
                                                        </td>
                                                        <td align="left" style="width: 35%; height: 29px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="background-image: url(images/bg_gray.gif); width: 15%;
                                                            height: 29px">
                                                            abc</td>
                                                        <td align="left" style="width: 35%; height: 29px">
                                                        </td>
                                                        <td align="right" style="background-image: url(images/bg_gray.gif); width: 15%;
                                                            height: 29px">
                                                        </td>
                                                        <td align="left" style="width: 35%; height: 29px">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table cellspacing="0" bordercolordark="#ffffff" cellpadding="1" width="750" bordercolorlight="#7a9cb7"
                                                    border="1">
                                                    <tr>
                                                        <td style="background-image: url(images/bg_gray.gif)" align="middle" colspan="4">
                                                            <asp:Button ID="btnQuery" runat="server" Style="font-size: 11px; font-family: Arial;
                                                                width: 87px;" Text="Query" OnClick="btnQuery_Click" />
                                                            <asp:Button ID="btnAdd" runat="server" Style="font-size: 11px; font-family: Arial;
                                                                width: 87px;" Text="Add" OnClick="btnAdd_Click" OnClientClick="return check_field();" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblMessages" runat="server" Font-Size="14px"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <fieldset>
                                                    <legend id="legend1" runat="server" visible="false" style="font-weight: bold; font-size: 12px;
                                                        font-family: Arial; color: black">
                                                        <img src="images/topic_result.gif" />
                                                    </legend>
                                                    <asp:GridView ID="GridView1" runat="server" Font-Names="Arial" Font-Size="11px" Width="100%"
                                                        AutoGenerateColumns="False" CellPadding="4" GridLines="None" ForeColor="#333333"
                                                        AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                                                        OnRowDeleting="GridView1_RowDeleting" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                                        OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowSorting="True">
                                                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                                        <Columns>
                                                            <asp:CommandField ShowEditButton="True" ButtonType="Button" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnDel" runat="server" Text="刪除" CommandName="Delete" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Defect Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDefectType" runat="server" Text='<%# Bind("ma_name") %>'></asp:Label>
                                                                    <asp:Label ID="lblSN" runat="server" Visible="false" Text='<%# Bind("sn") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtDefectType" runat="server" Width="100px" Text='<%# Bind("ma_name") %>'></asp:TextBox>
                                                                    <asp:Label ID="lblSN_Edit" runat="server" Visible="false" Text='<%# Bind("sn") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Defect Desc">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDefectDesc" runat="server" Text='<%# Bind("ma_desc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtDefectDesc" runat="server" Width="150px" Text='<%# Bind("ma_desc") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Create User">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreateUser" runat="server" Text='<%# Bind("create_user") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Create Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreateDate" runat="server" Text='<%# Bind("create_dttm") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Modify User">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblModifyUser" runat="server" Text='<%# Bind("update_user") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Modify Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblModifyDate" runat="server" Text='<%# Bind("update_dttm") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle BackColor="#E3EAEB" />
                                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#7C6F57" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="font-size: 12pt; background-image: url(images/tables/default_r.gif)">
                        <img src="images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td>
                        <img src="images/tables/default_lb.gif"></td>
                    <td style="background-image: url(images/tables/default_b.gif)">
                        <img height="9" src="images/tables/transparent.gif"></td>
                    <td>
                        <img src="images/tables/default_rb.gif"></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

<script type="text/javascript" language="javascript">
function check_field()
{
if( document.getElementById("txtDefectType").value=="")
    {
        alert("請輸入Defect Type");
        return false;
    }
else            
    {
        return true;
    }
    
}
</script>

