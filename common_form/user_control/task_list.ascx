<%@ Control Language="C#" AutoEventWireup="true" CodeFile="task_list.ascx.cs" Inherits="common_form_user_control_task_list" %>
<fieldset>
    <legend id="legend1" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
        color: black">Task Detial (<asp:Image runat="server" ID="image1" ImageUrl="~/images/td/sign.gif" />是Owner
        ) </legend>
    <table width="100%" height="10px">
        <tr>
            <td width="25%" align="right">
            </td>
            <td align="left">
                <table>
                    <tr>
                        <td style="background-color: Gold;" width="30px" height="10px">
                        </td>
                        <td style="font-size: 11px; font-family: Arial; color: navy">
                            此背景顏色顯示 Task 或 AI 已逾期 &nbsp;</td> 
                        <td style="background-color: lightgreen;" width="30px" height="10px">
                        </td>
                        <td style="font-size: 11px; font-family: Arial; color: navy">
                            此背景顏色顯示在最近七日Close的AI &nbsp;</td> 
                        <td style="background-color: #D8D7BC;" width="30px" height="10px">
                        </td>
                        <td style="font-size: 11px; font-family: Arial; color: navy" colspan="3">
                            此背景顏色顯示主管追蹤的Task</td>    
                    <%--</tr>
                    <tr>
                        
                    </tr>
                    <tr>
                        
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvTask" runat="server" AutoGenerateColumns="False" CellPadding="4"
        Font-Size="9pt" GridLines="None" Width="100%" Font-Names="Arial" EmptyDataText="No Task!"
        PageSize="20" OnRowDataBound="gvTask_RowDataBound" AllowPaging="True" OnPageIndexChanging="gvTask_PageIndexChanging"
        ForeColor="#333333">
        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="btnShowDetail" runat="server" ImageUrl="~/images/close13.gif" />
                </ItemTemplate>
                <ItemStyle Width="40px" Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="task_id" DataNavigateUrlFormatString="~/task_assign.aspx?task_id={0}"
                HeaderText="Task ID" DataTextField="task_id" Target="_blank"></asp:HyperLinkField>
            <%--<asp:HyperLinkField DataNavigateUrlFields="task_id" DataNavigateUrlFormatString="~/task_assign.aspx?task_id={0}"
                HeaderText="Task" DataTextField="task_desc" Target="_blank" ItemStyle-Font-Bold="true"
                ItemStyle-HorizontalAlign="Left" ControlStyle-Font-Underline="false"></asp:HyperLinkField>--%>
            <asp:BoundField HeaderText="Task" DataField="task_desc" ItemStyle-Width="200px" ItemStyle-Font-Bold="true"
                ItemStyle-HorizontalAlign="Left" />
            <asp:HyperLinkField DataNavigateUrlFields="project_id" DataNavigateUrlFormatString="~/project_assign.aspx?project_id={0}"
                HeaderText="Project" DataTextField="project_name" Target="_self" ItemStyle-Width="100px"
                ItemStyle-Font-Bold="true" ControlStyle-Font-Underline="false" ControlStyle-ForeColor="black">
            </asp:HyperLinkField>
            <%--<asp:BoundField HeaderText="Project" DataField="project_name" ItemStyle-Width="100px"
                ItemStyle-Font-Bold="true" />--%>
            <asp:BoundField HeaderText="Type" DataField="task_type" />
            <asp:BoundField HeaderText="建立日期" DataField="apply_date" />
            <asp:BoundField HeaderText="預計開始日" DataField="estimate_start_date" ItemStyle-ForeColor="Blue" />
            <asp:BoundField HeaderText="預計完成日" DataField="estimate_end_date" ItemStyle-ForeColor="Blue" />
            <asp:BoundField HeaderText="實際開始日" DataField="actual_start_date" />
            <asp:BoundField HeaderText="實際完成日" DataField="actual_end_date" />
            <%--<asp:BoundField HeaderText="優先權" DataField="priority" />--%>
            <asp:BoundField HeaderText="Status" DataField="status" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                    <img src="images/td/user.gif" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:DataList ID="dlMember" runat="server" RepeatDirection="Horizontal" RepeatColumns="1">
                        <ItemTemplate>
                            <asp:Image runat="server" ID="image1" ImageUrl="~/images/td/sign.gif" Visible='<%# display(Eval("is_owner").ToString()) %>' />
                            <asp:Label runat="server" ID="lblMember" Text='<%# Bind("member_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:DataList>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:GridView ID="gvAI" runat="server" BackColor="White" BorderColor="#999999" BorderWidth="1px"
        CellPadding="2" BorderStyle="Solid" ForeColor="Black" Visible="False" AutoGenerateColumns="False"
        Width="100%" OnRowDataBound="gvAI_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="No" DataField="rn">
                <HeaderStyle Width="20px" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="task_id,ai_id" DataNavigateUrlFormatString="~/action_item.aspx?task_id={0}&ai_id={1}"
                HeaderText="Action Item" DataTextField="ai_desc" Target="_blank" ControlStyle-ForeColor="Brown">
                <HeaderStyle Width="180px" />
            </asp:HyperLinkField>
            <asp:BoundField HeaderText="預計開始日" DataField="estimate_start_date">
                <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="預計完成日" DataField="estimate_end_date">
                <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="實際開始日" DataField="actual_start_date">
                <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="實際完成日" DataField="actual_end_date">
                <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Owner" DataField="member_name">
                <HeaderStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Status" DataField="status">
                <HeaderStyle Width="70px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="進度">
                <ItemTemplate>
                    <asp:Label ID="lblProgress" runat="server" Text='<%# Eval("progress") + "%" %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="工時(Hr)" DataField="ai_hour" ItemStyle-HorizontalAlign="Center">
                <HeaderStyle Width="50px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Fab" DataField="fab_area">
                <HeaderStyle Width="50px" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle BackColor="#C6C3C6" Font-Bold="True" ForeColor="Black" />
    </asp:GridView>
    <asp:Label runat="server" ID="lblSql" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblStatus" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblEmpno" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblDept" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblAIExpand" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblUser" Style="display: none"></asp:Label>
</fieldset>

<script language='javascript' type="text/javascript">
function showHideAnswer(obj,imgObj)
{
    if(document.getElementById(obj).style.display=='block'){
        document.getElementById(imgObj).src = "images/open13.gif";
	    document.getElementById(obj).style.display='none';
    }else{
        document.getElementById(imgObj).src = "images/close13.gif";
	    document.getElementById(obj).style.display='block';
    }		
}

</script>

