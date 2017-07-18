<%@ Page Language="C#" AutoEventWireup="true"  MaintainScrollPositionOnPostback="true" EnableEventValidation="false"  CodeFile="version_status.aspx.cs" Inherits="version_status" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>治工具改版統計</title>
    <link href="app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            </asp:ScriptManager>
            <br />
            <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
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
                    <td align="middle" width="100%">
                        <table align="center" cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="100%"
                            bordercolorlight="#7a9cb7" border="1">
                            <tr>
                                <td background="" colspan="4" class="pageTitle">
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>治工具改版統計</strong></span></td>
                                            <td align="right" style="font-size: 12px; color: navy">
                                                * 表示必填!</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" valign="middle" style="height: 10px; width: 5%;
                                    text-align: center;">
                                    TAPEOUT_DATE<br />
                                    (From_Date)<br />
                                </td>
                                    <td style="text-align: left; width: 127px; height: 22px;" valign="top">
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                        <ContentTemplate>
                                            &nbsp;From:&nbsp;
                                    <telerik:RadDatePicker ID="txtEstimateStartDate" runat="server" EnableTyping="False"
                                        Skin="Office2007" SkinID="Office2007">
                                        <DateInput ID="DateInput1" runat="server" DateFormat="yyyy/MM/dd" Font-Size="10pt"
                                            ReadOnly="True" Skin="Office2007">
                                        </DateInput>
                                        <Calendar ID="Calendar1" runat="server" Skin="Office2007">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Date="" ItemStyle-CssClass="rcToday" Repeatable="Today">
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                   
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    
                                    </td>
                                    <td class="pageTD" style="width: 10%; height: 22px">
                                        TAPEOUT_DATE<br />
                                        (T0_Date)<br />
                                    
                                    </td>
                                <td colspan="1" align="left" valign="top" style="text-align: left; height: 22px;" ><asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                                    <ContentTemplate>
                                        &nbsp;To:&nbsp;
                                        <telerik:RadDatePicker ID="txtEstimateEndDate" runat="server" EnableTyping="False"
                                        Skin="Office2007" SkinID="Office2007">
                                            <DateInput ID="DateInput2" runat="server" DateFormat="yyyy/MM/dd" Font-Size="10pt"
                                            ReadOnly="True" Skin="Office2007">
                                            </DateInput>
                                            <Calendar ID="Calendar2" runat="server" Skin="Office2007">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Date="" ItemStyle-CssClass="rcToday" Repeatable="Today">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                    &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" style="height: 5px; width: 5%;">
                                    FAB&nbsp;</td>
                                <td style="width: 127px; height: 5px; text-align: left;" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList1" runat="server" Height="24px" Width="105px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    &nbsp;
                                </td>
                                <td class="pageTD" align="center" style="height: 5px; width: 10%;">
                                    PROCESS</td>
                                <td style="width: 37px; height: 5px;">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList2" runat="server" Height="24px" Width="105px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" style="height: 10px; width: 5%;">
                                    LAYER</td>
                                <td style="height: 10px; text-align: left;" valign="top" colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList3" runat="server" Height="24px" Width="105px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    &nbsp; &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="pageTD" colspan="8" style="height: 35px">
                                    <asp:Button ID="ButtonQuery" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Query" OnClick="ButtonQuery_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnExport" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Export" OnClick="btnExport_Click1" />&nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                    &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
                                    &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                    &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <fieldset>
                                        <legend id="Legend5" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
                                            color: black">&nbsp;&nbsp;&nbsp;查詢結果
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="height: 175px">
                                                    <%--<asp:GridView ID="gvTask" runat="server" Font-Names="Arial" Font-Size="12px" Width="100%"
                                                        AutoGenerateColumns="False" CellPadding="4" EmptyDataText="No Task!" OnRowDataBound="gvTask_RowDataBound"
                                                        ForeColor="#333333" GridLines="None">--%>
                                                    <asp:GridView ID="GridView1" runat="server" Font-Names="Arial" Font-Size="12px" Width="1000px"
                                                        AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView1_RowDataBound">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                            <asp:BoundField HeaderText="FAB" DataField="FAB" />
                                                            <asp:BoundField HeaderText="PROCESS" DataField="PROCESS" />
                                                            <asp:BoundField HeaderText="LAYER" DataField="LAYER" />
                                                            <asp:TemplateField HeaderText="A">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink1" target="_blank"  NavigateUrl='<%#"http://"+ Server.MachineName +Context.Request.ApplicationPath+"/version_status_detail.aspx?FAB=" + DataBinder.Eval(Container.DataItem, "FAB")+"&PROCESS="+DataBinder.Eval(Container.DataItem, "PROCESS")+"&LAYER="+DataBinder.Eval(Container.DataItem, "LAYER")+"&Flag=A"+"&IsAuthenticated=passed&user=AA"+"&Start="+txtEstimateStartDate.SelectedDate.ToString()+"&End="+txtEstimateEndDate.SelectedDate.ToString() %> '
                                                                        Text='<%# Bind("A") %>' ForeColor="DeepPink" runat="server"></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="B">
                                                            <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink2" target="_blank" NavigateUrl='<%#"http://"+ Server.MachineName +Context.Request.ApplicationPath+"/version_status_detail.aspx?FAB=" + DataBinder.Eval(Container.DataItem, "FAB")+"&PROCESS="+DataBinder.Eval(Container.DataItem, "PROCESS")+"&LAYER="+DataBinder.Eval(Container.DataItem, "LAYER")+"&Flag=B"+"&IsAuthenticated=passed&user=AA"+"&Start="+txtEstimateStartDate.SelectedDate.ToString()+"&End="+txtEstimateEndDate.SelectedDate.ToString() %> '
                                                                        Text='<%# Bind("B") %>' ForeColor="DeepPink" runat="server"></asp:HyperLink>
                                                                </ItemTemplate>
                                                            
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="C">
                                                            <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink3"  target="_blank" NavigateUrl='<%#"http://"+ Server.MachineName +Context.Request.ApplicationPath+"/version_status_detail.aspx?FAB=" + DataBinder.Eval(Container.DataItem, "FAB")+"&PROCESS="+DataBinder.Eval(Container.DataItem, "PROCESS")+"&LAYER="+DataBinder.Eval(Container.DataItem, "LAYER")+"&Flag=C"+"&IsAuthenticated=passed&user=AA"+"&Start="+txtEstimateStartDate.SelectedDate.ToString()+"&End="+txtEstimateEndDate.SelectedDate.ToString()  %> '
                                                                        Text='<%# Bind("C") %>' ForeColor="DeepPink" runat="server"></asp:HyperLink>
                                                                </ItemTemplate>
                                                            
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="D">
                                                             <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink4" target="_blank" NavigateUrl='<%#"http://"+ Server.MachineName +Context.Request.ApplicationPath+"/version_status_detail.aspx?FAB=" + DataBinder.Eval(Container.DataItem, "FAB")+"&PROCESS="+DataBinder.Eval(Container.DataItem, "PROCESS")+"&LAYER="+DataBinder.Eval(Container.DataItem, "LAYER")+"&Flag=D"+"&IsAuthenticated=passed&user=AA"+"&Start="+txtEstimateStartDate.SelectedDate.ToString()+"&End="+txtEstimateEndDate.SelectedDate.ToString() %> '
                                                                        Text='<%# Bind("D") %>' ForeColor="DeepPink" runat="server"></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="OTHER">
                                                            
                                                            <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink5"  target="_blank" NavigateUrl='<%#"http://"+ Server.MachineName +Context.Request.ApplicationPath+"/version_status_detail.aspx?FAB=" + DataBinder.Eval(Container.DataItem, "FAB")+"&PROCESS="+DataBinder.Eval(Container.DataItem, "PROCESS")+"&LAYER="+DataBinder.Eval(Container.DataItem, "LAYER")+"&Flag=OTHER"+"&IsAuthenticated=passed&user=AA"+"&Start="+txtEstimateStartDate.SelectedDate.ToString()+"&End="+txtEstimateEndDate.SelectedDate.ToString() %> '
                                                                        Text='<%# Bind("OTHER") %>' ForeColor="DeepPink" runat="server"></asp:HyperLink>
                                                                </ItemTemplate>
                                                            
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total">
                                                            
                                                             <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink6" target="_blank" NavigateUrl='<%#"http://"+ Server.MachineName +Context.Request.ApplicationPath+"/version_status_detail.aspx?FAB=" + DataBinder.Eval(Container.DataItem, "FAB")+"&PROCESS="+DataBinder.Eval(Container.DataItem, "PROCESS")+"&LAYER="+DataBinder.Eval(Container.DataItem, "LAYER")+"&Flag=Total"+"&IsAuthenticated=passed&user=AA"+"&Start="+txtEstimateStartDate.SelectedDate.ToString()+"&End="+txtEstimateEndDate.SelectedDate.ToString() %> '
                                                                        Text='<%# Bind("Total") %>' ForeColor="DeepPink" runat="server"></asp:HyperLink>
                                                                </ItemTemplate>
                                                            
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="font-size: 12pt; background-image: url(images/tables/default_r.gif)">
                        <img src="images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td style="height: 9px">
                        <img src="images/tables/default_lb.gif"></td>
                    <td style="background-image: url(images/tables/default_b.gif); height: 9px;">
                        <img height="9" src="images/tables/transparent.gif"></td>
                    <td style="height: 9px">
                        <img src="images/tables/default_rb.gif"></td>
                </tr>
            </table>
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

