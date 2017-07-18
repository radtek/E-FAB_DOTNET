<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="header" %>
<link rel="stylesheet" type="text/css" href="<%=Context.Request.ApplicationPath%>/include/js/DDLevelsMenu/ddlevelsmenu-base.css" />
<link rel="stylesheet" type="text/css" href="<%=Context.Request.ApplicationPath%>/include/js/DDLevelsMenu/ddlevelsmenu-topbar.css" />
<link rel="stylesheet" type="text/css" href="<%=Context.Request.ApplicationPath%>/include/js/DDLevelsMenu/ddlevelsmenu-sidebar.css" />

<script type="text/javascript" src="<%=Context.Request.ApplicationPath%>/include/js/DDLevelsMenu/ddlevelsmenu.js"></script>

<style type="text/css">
<!--
A.menu {	
	color:#000000;	
	text-decoration:none;
	font-size: 16px;
	font-weight: bold;
}
A.menu:hover {		
	text-decoration:underline;
	color:#FCA20C;
}
A {	
	color:#000000;	
	text-decoration:underline;
	font-size: 12px;	
}
A:hover {		
	text-decoration:underline;
	color:#FCA20C;
}
-->
</style>
<table id="tb_banner" background="<%=Context.Request.ApplicationPath%>/images/store/SOFU/images/top_bg.gif"
    border="0" cellpadding="0" cellspacing="0" width="100%">
    <tbody>
        <tr valign="top">
            <td width="28">
                <img src="<%=Context.Request.ApplicationPath%>/images/store/SOFU/images/logoleft.gif"
                    alt="" width="28" height="102" /></td>
            <td width="200">
                <a href="<%=Context.Request.ApplicationPath%>/Default.aspx">
                    <img src="<%=Context.Request.ApplicationPath%>/images/store/SOFU/logo.png" alt="回首頁"
                        width="210" height="102" border="0" /></a></td>
            <td width="20">
                <img src="<%=Context.Request.ApplicationPath%>/images/store/SOFU/images/logoright.gif"
                    alt="" width="20" height="102" /></td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr>
                            <td colspan="8" align="right" height="34" valign="bottom">
                            </td>
                        </tr>
                        <tr background="<%=Context.Request.ApplicationPath%>/images/store/SOFU/images/menu_bg.png">
                            <td width="187" valign="bottom" align="center">
                                <div id="task_Link">
                                    <a href="#" rel="menu_task_Link" class="menu">Task Management</a>
                                </div>
                            </td>
                            <td>
                                <img src="<%=Context.Request.ApplicationPath%>/images/store/SOFU/images/menu_line.png" /></td>
                            <td width="187" align="center" valign="bottom">
                                <div id="project_Link">
                                    <a href="#" rel="menu_project_Link" class="menu">Project Management</a></div>
                            </td>
                            <td>
                                <img src="<%=Context.Request.ApplicationPath%>/images/store/SOFU/images/menu_line.png" /></td>
                            <td width="187" align="center" valign="bottom">
                                <div id="calendar_Link">
                                    <a href="#" rel="menu_calendar_Link" class="menu">Calendar</a></div>
                            </td>
                            <td>
                                <img src="<%=Context.Request.ApplicationPath%>/images/store/SOFU/images/menu_line.png" /></td>
                            <td width="187" align="center" valign="bottom">
                                <div id="analysis_report_Link">
                                    <a href="#" rel="menu_report_Link" class="menu">Report</a></div>
                            </td>
                            <td>
                                <img src="<%=Context.Request.ApplicationPath%>/images/store/SOFU/images/menu_line.png" /></td>
                        </tr>
                        <%--<tr>
                            <td colspan="8" align="right" height="21">
                            </td>
                        </tr>--%>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
<ul id="menu_task_Link" class="ddsubmenustyle">
    <li><a href="<%=Context.Request.ApplicationPath%>/my_task.aspx">My Task</a></li>
    <li><a href="<%=Context.Request.ApplicationPath%>/dept_task.aspx">Department Task</a></li>
    <li><a href="<%=Context.Request.ApplicationPath%>/task_apply.aspx">New Task</a></li>
    <li><a href="<%=Context.Request.ApplicationPath%>/trace_list_maintain.aspx">My Trace Maintain</a></li>
    <li><a href="<%=Context.Request.ApplicationPath%>/dept_trace_list.aspx">Department Trace List</a></li>
</ul>
<ul id="menu_project_Link" class="ddsubmenustyle">
    <li><a href="<%=Context.Request.ApplicationPath%>/my_project.aspx">My Project</a></li>
    <li><a href="<%=Context.Request.ApplicationPath%>/dept_project.aspx">Department Project</a></li>
    <li><a href="<%=Context.Request.ApplicationPath%>/project_apply.aspx">New Project</a></li>
</ul>
<ul id="menu_calendar_Link" class="ddsubmenustyle">
    <li><a href="<%=Context.Request.ApplicationPath%>/Calendar_Project.aspx">Calendar Project</a></li>
    <li><a href="<%=Context.Request.ApplicationPath%>/Calendar_MemberTask.aspx">Member Task</a></li>
</ul>
<ul id="menu_report_Link" class="ddsubmenustyle">
    <li><a href="<%=Context.Request.ApplicationPath%>/analysis_report/analysis_trend_chart_by_people.aspx">
        Analysis Member Report</a></li>
    <li><a href="<%=Context.Request.ApplicationPath%>/analysis_report/analysis_trend_chart_by_fabarea.aspx">
        Analysis Fab Report</a></li>
    <li><a href="<%=Context.Request.ApplicationPath%>/analysis_report/task_status_report_by_dept.aspx">
        Dept Task Statistics Report</a></li>
</ul>

<script type="text/javascript">
ddlevelsmenu.setup("task_Link", "topbar") //ddlevelsmenu.setup("mainmenuid", "topbar|sidebar")
ddlevelsmenu.setup("project_Link", "topbar") //ddlevelsmenu.setup("mainmenuid", "topbar|sidebar")
ddlevelsmenu.setup("calendar_Link", "topbar") //ddlevelsmenu.setup("mainmenuid", "topbar|sidebar")
ddlevelsmenu.setup("analysis_report_Link", "topbar")//ddlevelsmenu.setup("mainmenuid", "topbar|sidebar")
</script>

