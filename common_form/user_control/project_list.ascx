<%@ Control Language="C#" AutoEventWireup="true" CodeFile="project_list.ascx.cs"
    Inherits="common_form_user_control_project_list" %>
<%@ Register Src="task_list.ascx" TagName="task_list" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<fieldset>
    <legend id="legend1" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
        color: black">Project Detail </legend>
    <table width="100%" height="10px">
        <tr>
            <td width="70%" align="right">
            </td>
            <td align="right">
                <table>
                    <tr>
                        <td style="background-color: Gold;" width="30px" height="10px">
                        </td>
                        <td style="font-size: 11px; font-family: Arial; color: navy">
                            此背景顏色顯示 Project 已逾期</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
        Font-Size="9pt" GridLines="None" Width="100%" Font-Names="Arial" EmptyDataText="No Project!"
        AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" ForeColor="#333333"
        OnRowDataBound="GridView1_RowDataBound" PageSize="20">
        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <%--<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName=""
                    Text="Task Detail" OnClick="LinkButton2_Click"></asp:LinkButton>--%>
                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/images/td/zoom2.gif"
                        OnClientClick='<%# string.Format("return OpenTask({0});",Eval("project_id") ) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# "~/project_assign.aspx?project_id=" + Eval("project_id")  %>'
                        ImageUrl="~/images/td/btn_edit.gif"></asp:HyperLink>
                    <%--<a href='<%# "project_assign.aspx?project_id=" + Eval("project_id")  %>'> <asp:Image runat="server" ImageUrl="~/images/td/btn_edit.gif" /></a>--%>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:HyperLinkField DataNavigateUrlFields="project_id" DataNavigateUrlFormatString="~/project_assign.aspx?project_id={0}"
            Text="Edit" Target="_self"></asp:HyperLinkField>--%>
            <asp:BoundField HeaderText="No" DataField="rn">
                <ItemStyle Font-Bold="true" />
                <HeaderStyle Width="20px" />
            </asp:BoundField>
            <asp:BoundField DataField="project_name" HeaderText="Project">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="100px" Font-Bold="true" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="project_group" HeaderText="Project Group">
                <ItemStyle Width="50px" Font-Bold="true" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="applicant" HeaderText="專案管理者">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="estimate_end_date" HeaderText="預計完成日">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="actual_start_date" HeaderText="實際開始日">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="actual_end_date" HeaderText="實際完成日">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="priority" HeaderText="優先權">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="status" HeaderText="Status">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Comment">
                <ItemTemplate>
                    <asp:ImageButton runat="server" ID="ImageButton3" ImageUrl="~/images/td/comment_yellow.gif"
                        OnClientClick='<%# string.Format("return OpenComment({0});",Eval("project_id") ) %>' />
                </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="甘特圖" ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton2" runat="server" Text="甘特圖" OnClientClick='<%# string.Format("return OpenGantt({0});",Eval("project_id") ) %>'
                        BorderStyle="Groove" BackColor="#8DABE2" ImageUrl="~/images/td/chart.gif" Width="30px" />
                    <%--<asp:Button ID="Button1" runat="server" Text="甘特圖" OnClientClick='<%# string.Format("return OpenGantt({0});",Eval("project_id") ) %>'
                    BorderStyle="Groove" BackColor="#8DABE2" />--%>
                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("project_id") %>' Visible="false"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <table cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="100%" bordercolorlight="#7a9cb7"
                        border="1">
                        <tr>
                            <td width="70%">
                                AI總數 :
                            </td>
                            <td width="30%">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("ai_total") %>'></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                未完成AI數 :
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("ai_processing") %>'></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                已完成AI百分比 :
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("ai_unclose_ratio")+ "%" %>'></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                累積完成工時(Hr) :
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("ai_hour") %>'></asp:Label>&nbsp;
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <HeaderStyle Width="50px" />
            </asp:TemplateField>
        </Columns>
        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:Label ID="lblSql" runat="server" Style="display: none"></asp:Label>
</fieldset>
<br />
<%--<uc1:task_list ID="Task_list1" runat="server" Visible="false" />--%>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Office2007">
</telerik:RadWindowManager>

<script language="javascript">
function OpenGantt(project_id)
{
   //w = window.open("Gantt_Chart.aspx?project_id="+ project_id ,"gantt_chart", "height=350, width=900, left=200, top=150, " +  "location=no,	menubar=no, resizable=yes, " + "scrollbars=yes, titlebar=no, toolbar=no", true);
   //w.focus();
    var oWnd = radopen("Gantt_Chart.aspx?project_id=" + project_id, null );
   oWnd.setSize(900,450);           
   oWnd.center();
   return false;
}

function OpenTask(project_id)
{
   var oWnd = radopen("task_list.aspx?project_id=" + project_id, null );
   oWnd.setSize(1000,500);           
   oWnd.center();
   //oWnd.Maximize();
   return false;
}
function OpenComment(project_id)
{
   var oWnd = radopen("common_form/WebForm/comment_list.aspx?project_id=" + project_id, null );
   oWnd.setSize(900,500);           
   oWnd.center();
   //oWnd.Maximize();
   return false;
}
</script>

