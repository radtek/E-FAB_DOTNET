<%@ Control Language="C#" AutoEventWireup="true" CodeFile="my_trace_list.ascx.cs"
    Inherits="common_form_user_control_my_trace_list" %>
<fieldset>
    <asp:GridView ID="gvTask" runat="server" AutoGenerateColumns="False" CellPadding="4"
        Font-Size="9pt" GridLines="None" Width="100%" Font-Names="Arial" EmptyDataText="No Task!"
        PageSize="20" OnRowDataBound="gvTask_RowDataBound" AllowPaging="True" OnPageIndexChanging="gvTask_PageIndexChanging"
        ForeColor="#333333">
        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <%--<asp:Button ID="btnTrace" runat="server" Text="Add" OnClick="Trace_Click" OnClientClick="return confirm('確定嗎?')"
                        Font-Names="Arial" Font-Bold="True" BorderStyle="Outset"/>--%>
                    <asp:CheckBox runat="server" ID="cbHead" OnClick="javascript: return select_deselectAll (this.checked, this.id);">
                    </asp:CheckBox>
                </HeaderTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Task ID">
                <ItemTemplate>
                    <asp:HyperLink ID="HyLinkTaskID" runat="server" NavigateUrl='<%# Eval("task_id", "~/task_assign.aspx?task_id={0}") %>'
                        Target="_blank" Text='<%# Eval("task_id") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Task" DataField="task_desc">
                <ItemStyle Font-Bold="True" HorizontalAlign="Left" Width="200px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Project" DataField="project_name">
                <ItemStyle Font-Bold="True" Width="100px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Type" DataField="task_type" />
            <asp:BoundField HeaderText="建立日期" DataField="apply_date" />
            <asp:BoundField HeaderText="預計開始日" DataField="estimate_start_date">
                <ItemStyle ForeColor="Blue" />
            </asp:BoundField>
            <asp:BoundField HeaderText="預計完成日" DataField="estimate_end_date">
                <ItemStyle ForeColor="Blue" />
            </asp:BoundField>
            <asp:BoundField HeaderText="實際開始日" DataField="actual_start_date" />
            <asp:BoundField HeaderText="實際完成日" DataField="actual_end_date" />
            <asp:BoundField HeaderText="Status" DataField="status">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField>
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
                <HeaderStyle Width="75px" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
        </Columns>
        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:Label runat="server" ID="lblSql" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblStatus" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblEmpno" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblDept" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblAIExpand" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblCheckState" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblUser" Style="display: none"></asp:Label>
</fieldset>

<script>
function select_deselectAll (chkVal, idVal) 
{ 
	//var frm = document.forms[0];
	var frm = document.getElementsByTagName("input");
     
	// Loop through all elements
	for (i=0; i<frm.length; i++) 
	{
		var elementName = frm[i].id;
        
		var pos = elementName.indexOf("gvTask");
		if(pos > 0)
		{
		    // Check if main checkbox is checked, then select or deselect datagrid checkboxes 
			if(chkVal == true) 
			{
				frm[i].checked = true;
			}
			else 
			{
				frm[i].checked = false;
			}
		}
	}
}
</script>

