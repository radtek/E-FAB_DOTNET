<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gridview.aspx.cs" Inherits="gridview" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>專案任務指派</title>
    <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: inline; z-index: 105; left: 10px; width: 100%; color: black;
            top: 0px; height: 16px; background-color: white">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <br />
            <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <img src="../images/tables/default_lt.gif" /></td>
                    <td style="background-image: url(../images/tables/default_t.gif)">
                        <img height="9" src="../images/tables/transparent.gif" /></td>
                    <td>
                        <img src="../images/tables/default_rt.gif" /></td>
                </tr>
                <tr>
                    <td style="background-image: url(../images/tables/default_l.gif)">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                    <td align="middle" width="100%">
                        <table cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="100%" bordercolorlight="#7a9cb7"
                            border="1">
                            <tr>
                                <td align="middle" background="" colspan="6" class="pageTitle">
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <span id="lblTitle">專案任務指派</span></td>
                                            <td align="right" style="font-size: 12px; color: navy">
                                                * 表示必填!</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" align="center">
                                    Project Name
                                </td>
                                <td align="left" colspan="2">
                                    <asp:Label ID="lblProjectName" runat="server" CssClass="pageLabel"> </asp:Label>
                                </td>
                                <td class="pageTD" align="center">
                                    Project Group
                                    <asp:Image runat="server" ID="image1" ImageUrl="~/images/td/help-icon.png" ToolTip="可選擇Project Group，將專案歸屬到某個Group中。" />
                                </td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList runat="server" ID="ddlProjectGroup">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" align="center">
                                    <span style="vertical-align: middle; font-weight: bold; font-size: 18px; color: red">
                                        *</span> Description<img src="../images/td/reply.gif" />
                                </td>
                                <td align="left" colspan="5">
                                    <asp:TextBox ID="txtProjectDesc" Height="80px" runat="server" TextMode="MultiLine"
                                        Width="98%" MaxLength="1000"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" align="center">
                                    專案管理者<img src="../images/td/001_55.gif" height="18px" />
                                </td>
                                <td align="left" style="width: 213px">
                                    <asp:Label ID="lblAppilcant" runat="server" CssClass="pageLabel"> </asp:Label>
                                    <span style="display: none">
                                        <asp:Label ID="lblProjectNo" runat="server"></asp:Label></span></td>
                                <td class="pageTD" align="center">
                                    部門
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblAppilcantDept" runat="server" CssClass="pageLabel"> </asp:Label>
                                </td>
                                <td class="pageTD" align="center">
                                    專案建立日
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblApplyDate" runat="server" CssClass="pageLabel"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" align="center">
                                    <span style="vertical-align: middle; font-weight: bold; font-size: 18px; color: red">
                                        *</span> 預計開始日
                                </td>
                                <td align="left" style="width: 213px">
                                    <telerik:RadDatePicker ID="txtEstimateStartDate" runat="server" Skin="Office2007"
                                        SkinID="Office2007" EnableTyping="False">
                                        <DateInput ID="DateInput1" Skin="Office2007" DateFormat="yyyy/MM/dd" ReadOnly="True"
                                            runat="server" Font-Size="10pt">
                                        </DateInput>
                                        <Calendar ID="Calendar1" Skin="Office2007" runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday">
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                </td>
                                <td class="pageTD" align="center">
                                    <span style="vertical-align: middle; font-weight: bold; font-size: 18px; color: red">
                                        *</span> 預計完成日
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="txtEstimateEndDate" runat="server" Skin="Office2007" SkinID="Office2007"
                                        EnableTyping="False">
                                        <DateInput ID="DateInput2" Skin="Office2007" DateFormat="yyyy/MM/dd" ReadOnly="True"
                                            runat="server" Font-Size="10pt">
                                        </DateInput>
                                        <Calendar ID="Calendar2" Skin="Office2007" runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday">
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                </td>
                                <td class="pageTD" align="center">
                                    <span style="vertical-align: middle; font-weight: bold; font-size: 18px; color: red">
                                        *</span> Status
                                </td>
                                <td align="left">
                                    <asp:DropDownList runat="server" ID="ddlStatus" Width="150px">
                                        <asp:ListItem Text="Receiving" Value="Receiving"></asp:ListItem>
                                        <asp:ListItem Text="Processing" Value="Processing"></asp:ListItem>
                                        <asp:ListItem Text="Close" Value="Close"></asp:ListItem>
                                        <asp:ListItem Text="Cancel" Value="Cancel"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblOriginalStatus" runat="server" Visible="false" CssClass="pageLabel"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" align="center">
                                    實際開始日
                                </td>
                                <td align="left" style="width: 213px">
                                    &nbsp;<asp:Label ID="lblActualStartDate" runat="server" CssClass="pageLabel"> </asp:Label>
                                </td>
                                <td class="pageTD" align="center">
                                    實際完成日
                                </td>
                                <td align="left">
                                    &nbsp;<asp:Label ID="lblActualEndDate" runat="server" CssClass="pageLabel"> </asp:Label>
                                </td>
                                <td class="pageTD" align="center">
                                    <span style="vertical-align: middle; font-weight: bold; font-size: 18px; color: red">
                                        *</span> 優先度
                                </td>
                                <td align="left">
                                    <asp:DropDownList runat="server" ID="ddlPiority" Width="100px">
                                        <asp:ListItem Text="高" Value="高"></asp:ListItem>
                                        <asp:ListItem Text="中" Value="中" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="低" Value="低"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblOriginalPiority" runat="server" CssClass="pageLabel" Visible="false"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="pageTD" align="center">
                                    效益(金額)
                                </td>
                                <td align="left" colspan="1" style="width: 213px">
                                    <asp:TextBox ID="txtPrice" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td class="pageTD" align="center">
                                    附件<img src="../images/td/attach.png" />
                                </td>
                                <td align="left" colspan="3">
                                    <asp:DataList ID="dlAttach" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" ID="hyAttach" NavigateUrl='<%# Bind("FILE_Link") %>'
                                                Text='<%# Bind("FILE_Desc") %>' Font-Size="13px" Target="_blank" ForeColor="blue" />&nbsp;&nbsp;&nbsp;
                                        </ItemTemplate>
                                    </asp:DataList>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" align="center">
                                    附件描述
                                </td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtFileDesc1" runat="server" Width="340px"></asp:TextBox>
                                </td>
                                <td class="pageTD" align="center">
                                    附件上傳<img src="../images/td/attach.png" />
                                </td>
                                <td align="left" colspan="2">
                                    <input id="File1" style="width: 220px; height: 22px" type="file" size="19" name="File1"
                                        runat="server" />
                                    <font size="2pt">(上限10MB)</font>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <fieldset>
                                        <legend id="legend1" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
                                            color: black">處理過程
                                            <asp:CheckBox ID="chkMailToMember" runat="server" Text="E-Mail To Member"></asp:CheckBox>
                                            <img src="../images/td/email.gif" />
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td class="pageTD" align="center">
                                                    Comment<img src="../images/td/reply.gif" />
                                                    <br />
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:TextBox ID="txtComment" Height="80px" runat="server" TextMode="MultiLine" Width="98%"
                                                        MaxLength="4000"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" colspan="6">
                                    <asp:Button ID="btnSave" runat="server" Style="font-size: 11px; font-family: Arial;
                                        width: 87px;" Text="Save" OnClientClick="return check_field();" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnTask" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Add Task" OnClientClick="return AddTask();" />
                                    <asp:Button ID="btnExport" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Export" OnClick="btnExport_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <fieldset>
                                        <legend id="legend3" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
                                            color: black">&nbsp;<asp:Image ID="btnShowDetail1" runat="server" ImageUrl="~/images/close13.gif"/>&nbsp;&nbsp;Project Task (<asp:Image runat="server" ID="image2" ImageUrl="~/images/td/sign.gif" />是Owner
                                            ) </legend>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <%--<asp:GridView ID="gvTask" runat="server" Font-Names="Arial" Font-Size="12px" Width="100%"
                                                        AutoGenerateColumns="False" CellPadding="4" EmptyDataText="No Task!" OnRowDataBound="gvTask_RowDataBound"
                                                        ForeColor="#333333" GridLines="None">--%>
                                                    <asp:GridView ID="gvTask" runat="server" Font-Names="Arial" Font-Size="12px" Width="100%"
                                                        AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" OnRowDataBound="gvTask_RowDataBound" EmptyDataText="No Task!" OnRowDeleting="gvTask_RowDeleting">
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/td/btn_edit.gif"
                                                                        OnClientClick='<%# string.Format("return OpenTask({0});",Eval("task_id") ) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="No" DataField="rn" />
                                                            <asp:BoundField HeaderText="task_id" DataField="task_id" Visible="False" />
                                                            <asp:BoundField HeaderText="Task" DataField="task_desc">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="預計開始日" DataField="estimate_start_date" />
                                                            <asp:BoundField HeaderText="預計完成日" DataField="estimate_end_date" />
                                                            <asp:BoundField HeaderText="實際開始日" DataField="actual_start_date" />
                                                            <asp:BoundField HeaderText="實際完成日" DataField="actual_end_date" />
                                                            <asp:BoundField HeaderText="Status" DataField="status" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                                <HeaderTemplate>
                                                                    <img src="../images/td/user2.gif" height="20px" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DataList ID="dlTaskMember" runat="server" RepeatColumns="1" RepeatDirection="Vertical">
                                                                        <ItemTemplate>
                                                                            <asp:Image runat="server" ID="image1" ImageUrl="~/images/td/sign.gif" Visible='<%# display(Eval("is_owner").ToString()) %>' />
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("member_name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:DataList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ImageUrl="~/images/bdelete.gif" ID="btnDel" runat="server" CommandName="Delete"
                                                                        ToolTip="刪除" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <%--<RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" />--%>
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <fieldset>
                                        <legend id="legend2" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
                                            color: black">&nbsp;<asp:Image ID="btnShowDetail2" runat="server" ImageUrl="~/images/close13.gif"/>&nbsp;&nbsp;Process History </legend>
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
                                                                        <img src="../images/td/process.png" height="18px" style="vertical-align: middle" />
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
                    </td>
                    <td style="font-size: 12pt; background-image: url(../images/tables/default_r.gif)">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td>
                        <img src="../images/tables/default_lb.gif"></td>
                    <td style="background-image: url(../images/tables/default_b.gif)">
                        <img height="9" src="../images/tables/transparent.gif"></td>
                    <td>
                        <img src="../images/tables/default_rb.gif"></td>
                </tr>
            </table>
            <asp:TextBox ID="selfSubmit" runat="server" Style="display: none" Text="Y"></asp:TextBox>
        </div>
    </form>
</body>
</html>

<script language="javascript">
function check_field()
{
    if( document.getElementById("txtProjectDesc").value=="")
    {
        alert("請輸入Project Description");
        return false;
    }
    else if( document.getElementById("txtEstimateStartDate").value=="")
    {
        alert("請輸入預計開始日");
        return false;
    }
    else if( document.getElementById("txtEstimateEndDate").value=="")
    {
        alert("請輸入預計完成日");
        return false;
    }
      else if( document.getElementById("File1").value!="" && document.getElementById("txtFileDesc1").value=="")
    {
        alert("請輸入File Description");
        return false;
    }
      else if (document.getElementById("txtPrice").value!="" && isNaN(document.getElementById("txtPrice").value)==true)
    {
        alert("請輸入效益(金額)且為數字");
        return false;
    }
    else
    {
        return true;
    }
     
}


function AddTask()
{
//   w = window.open("task_apply.aspx?project_id="+ document.getElementById('lblProjectNo').innerText ,"Add_task", "height=600, width=950, left=200, top=150, " +  "location=no,	menubar=no, resizable=yes, " + "scrollbars=yes, titlebar=no, toolbar=no", true);
//   w.focus();
   return false;
}

function OpenTask(task_id)
{
//   w = window.open("task_assign.aspx?task_id="+ task_id ,"task_maintain", "height=600, width=950, left=200, top=150, " +  "location=yes,	menubar=yes, resizable=yes, " + "scrollbars=yes, titlebar=no, toolbar=yes", true);
//   w.focus();
   return false;
}

function showHideAnswer(obj,imgObj)
{
    if (document.getElementById(obj) == null)
        return;
    if(document.getElementById(obj).style.display=='none'){
        document.getElementById(imgObj).src = "../images/close13.gif";
	    document.getElementById(obj).style.display='block';
    }else{
        document.getElementById(imgObj).src = "../images/open13.gif";
	    document.getElementById(obj).style.display='none';
    }		
}
</script>

