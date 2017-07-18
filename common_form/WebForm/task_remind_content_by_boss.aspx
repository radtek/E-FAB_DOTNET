<%@ Page Language="C#" AutoEventWireup="true" CodeFile="task_remind_content_by_boss.aspx.cs"
    Inherits="common_form_WebForm_task_remind_content_by_boss" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>部門Delay的工作事項提醒</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <span id="title" runat="server"></span>
            <br />
            <br />
            <asp:Label ID="lblProject" runat="server" Text="Project" Font-Bold="True"></asp:Label>
            <asp:GridView ID="gvProject" runat="server" Font-Names="Arial" Font-Size="12px" Width="100%"
                AutoGenerateColumns="false" CellPadding="3" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="Inset" BorderWidth="1px" EmptyDataText="無任何Project未完成!">
                <Columns>
                    <asp:BoundField HeaderText="NO" DataField="rownum" ItemStyle-Width="10%" />
                    <asp:HyperLinkField DataNavigateUrlFields="project_id" DataNavigateUrlFormatString="http://t1cimweb02/tms/project_assign.aspx?project_id={0}&mail_to=Y"
                        HeaderText="Project" DataTextField="project_name" Target="_blank" ItemStyle-Width="10%">
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="Project Group" DataField="project_group" ItemStyle-Width="10%" />
                    <asp:BoundField HeaderText="部門" DataField="applicant_dept" ItemStyle-Width="20%" />
                    <asp:BoundField HeaderText="預計開始日" DataField="estimate_start_date" ItemStyle-Width="10%" />
                    <asp:BoundField HeaderText="預計完成日" DataField="estimate_end_date" ItemStyle-Width="10%" />
                    <asp:BoundField HeaderText="實際開始日" DataField="actual_start_date" ItemStyle-Width="10%" />
                    <asp:BoundField HeaderText="Status" DataField="status" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="10%" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                <PagerStyle BackColor="White" ForeColor="#000066" />
            </asp:GridView>
            <br />
            <asp:Label ID="lblTask" runat="server" Text="Task" Font-Bold="True"></asp:Label>
            <asp:GridView ID="gvTask" runat="server" Font-Names="Arial" Font-Size="12px" Width="100%"
                AutoGenerateColumns="false" CellPadding="3" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="Inset" BorderWidth="1px" EmptyDataText="無任何Task未完成!">
                <Columns>
                    <asp:BoundField HeaderText="NO" DataField="rownum" ItemStyle-Width="10%" />
                    <asp:HyperLinkField DataNavigateUrlFields="task_id" DataNavigateUrlFormatString="http://t1cimweb02/tms/task_assign.aspx?task_id={0}&mail_to=Y"
                        HeaderText="Task" DataTextField="task_desc" Target="_blank" ItemStyle-Width="15%">
                    </asp:HyperLinkField>
                    <asp:HyperLinkField DataNavigateUrlFields="project_id" DataNavigateUrlFormatString="http://t1cimweb02/tms/project_assign.aspx?project_id={0}&mail_to=Y"
                        HeaderText="Project" DataTextField="project_name" Target="_blank" ItemStyle-Width="10%">
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="部門" DataField="dept" ItemStyle-Width="20%" />
                    <asp:BoundField HeaderText="Type" DataField="task_type" ItemStyle-Width="10%" />
                    <asp:BoundField HeaderText="預計開始日" DataField="estimate_start_date" ItemStyle-Width="10%" />
                    <asp:BoundField HeaderText="預計完成日" DataField="estimate_end_date" ItemStyle-Width="10%" />
                    <asp:BoundField HeaderText="實際開始日" DataField="actual_start_date" ItemStyle-Width="10%" />
                    <asp:BoundField HeaderText="Status" DataField="status" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="10%" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                <PagerStyle BackColor="White" ForeColor="#000066" />
            </asp:GridView>
            <br />
            <asp:Label ID="lblAI" runat="server" Text="Action Item" Font-Bold="True"></asp:Label>
            <asp:GridView ID="gvAI" runat="server" Font-Names="Arial" Font-Size="12px" Width="100%"
                AutoGenerateColumns="false" CellPadding="3" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="Inset" BorderWidth="1px" EmptyDataText="無任何Action Item未完成!">
                <Columns>
                    <asp:BoundField HeaderText="NO" DataField="rownum" ItemStyle-Width="10%" />
                    <asp:HyperLinkField DataNavigateUrlFields="task_id,ai_id" DataNavigateUrlFormatString="http://t1cimweb02/tms/action_item.aspx?task_id={0}&ai_id={1}"
                        HeaderText="Action Item" DataTextField="ai_desc" Target="_blank" ItemStyle-Width="20%">
                    </asp:HyperLinkField>
                    <asp:HyperLinkField DataNavigateUrlFields="task_id" DataNavigateUrlFormatString="http://t1cimweb02/tms/task_assign.aspx?task_id={0}&mail_to=Y"
                        HeaderText="Task" DataTextField="task_desc" Target="_blank" ItemStyle-Width="20%">
                    </asp:HyperLinkField>
                     <asp:BoundField HeaderText="部門" DataField="dept" ItemStyle-Width="10%">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="預計開始日" DataField="estimate_start_date" ItemStyle-Width="10%">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="預計完成日" DataField="estimate_end_date" ItemStyle-Width="10%">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="實際開始日" DataField="actual_start_date" ItemStyle-Width="10%">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Status" DataField="status" ItemStyle-Width="10%"></asp:BoundField>
                    <asp:TemplateField HeaderText="進度" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblProgress" runat="server" Text='<%# Eval("progress") + "%" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                <PagerStyle BackColor="White" ForeColor="#000066" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
