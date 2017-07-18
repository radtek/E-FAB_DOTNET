<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_balance.aspx.cs" Inherits="TP_TP_balance" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TP_Balance</title>
    <script type="text/javascript">
		document.write('<div id="loadDiv" style="padding-top:150; padding-left:150; font-size:13pt;">'+ '頁面正在載入,請等待......</div>');   
		function   window.onload()   
		{   
			hiddenDiv.style.display=""; 
			loadDiv.removeNode(true); 
		}  
    </script>
    <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
 
        <div id="hiddenDiv" style="display: none inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            </asp:ScriptManager>
            <br />
            <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" width="85%">
                <tr>
                    <td style="height: 9px">
                        <img src="images/tables/default_lt.gif" /></td>
                    <td style="background-image: url(images/tables/default_t.gif); height: 9px;">
                        <img height="9" src="images/tables/transparent.gif" /></td>
                    <td style="height: 9px">
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
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>TP Balance&nbsp;
                                                    <asp:Label ID="Label1" runat="server" Text="Label" Width="95px"></asp:Label></strong></span></td>
                                            <td align="right" style="font-size: 12px; color: navy">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <fieldset>
                                        <legend id="Legend5" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
                                            color: black">&nbsp;呈現結果 </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="height: 386px">
                                                    <DCWC:Chart ID="Chart1" runat="server" Height="390px" Width="631px" BackColor="Wheat"
                                                        EnableViewState="True" BorderLineColor="Blue">
                                                        <Legends>
                                                            <DCWC:Legend Alignment="Center" Docking="Top" Name="Default">
                                                            </DCWC:Legend>
                                                        </Legends>
                                                        <Titles>
                                                            <DCWC:Title Name="Title1" Text="TP Balance Trend Chart(月統計) ">
                                                            </DCWC:Title>
                                                        </Titles>
                                                        <BorderSkin SkinStyle="FrameThin5" />
                                                        <Series>
                                                            <DCWC:Series BackGradientEndColor="Yellow" BorderColor="64, 64, 64" Color="BlueViolet"
                                                                Name="期初WIP+Input" ShadowOffset="1" ShowLabelAsValue="True" BorderStyle="NotSet"
                                                                BackGradientType="TopBottom" CustomAttributes="DrawingStyle=Cylinder">
                                                            </DCWC:Series>
                                                            <DCWC:Series BackGradientEndColor="Yellow" BorderColor="64, 64, 64" Color="Chartreuse"
                                                                Name="Output+Scrap+期末WIP+Destroy" ShadowOffset="1" ShowLabelAsValue="True" BorderStyle="NotSet"
                                                                BackGradientType="TopBottom" CustomAttributes="DrawingStyle=Cylinder">
                                                            </DCWC:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <DCWC:ChartArea BackColor="224, 224, 224" BackGradientEndColor="CornflowerBlue" BackGradientType="LeftRight"
                                                                Name="Default" AlignOrientation="None">
                                                                <AxisY Title="【片數】">
                                                                </AxisY>
                                                            </DCWC:ChartArea>
                                                        </ChartAreas>
                                                    </DCWC:Chart>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px">
                                                    <asp:GridView ID="GridView1" runat="server" Font-Names="Arial" Font-Size="12px" Width="1000px"
                                                        AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView1_RowDataBound"
                                                        EmptyDataText="No Record!">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrownum" runat="server" ForeColor="Blue" Text='<%# Bind("rownum") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="shop">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblshop" runat="server" ForeColor="Blue" Text='<%# Bind("shop") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="期初WIP+Input(1)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbegin_in" runat="server" ForeColor="Blue" Text='<%# Bind("begin_in") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Output+Scrap+期末WIP(2)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblscrap_end_out_destory" runat="server" ForeColor="Blue" Text='<%# Bind("scrap_end_out_destory") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 183px">
                                                    <%--<asp:GridView ID="gvTask" runat="server" Font-Names="Arial" Font-Size="12px" Width="100%"
                                                        AutoGenerateColumns="False" CellPadding="4" EmptyDataText="No Task!" OnRowDataBound="gvTask_RowDataBound"
                                                        ForeColor="#333333" GridLines="None">--%>
                                                    <asp:GridView ID="GridView2" runat="server" Font-Names="Arial" Font-Size="12px" Width="1000px"
                                                        AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView2_RowDataBound"
                                                        EmptyDataText="No Record!">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrownum" runat="server" ForeColor="Blue" Text='<%# Bind("rownum") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="shop">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblshop" runat="server" ForeColor="Blue" Text='<%# Bind("shop") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="期初WIP">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbegin_wip" runat="server" ForeColor="Blue" Text='<%# Bind("begin_wip") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Input">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblin_qty" runat="server" ForeColor="Blue" Text='<%# Bind("in_qty") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Out_qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblout_qty" runat="server" ForeColor="Blue" Text='<%# Bind("out_qty") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Scrap_total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblscrap_total" runat="server" ForeColor="Blue" Text='<%# Bind("scrap_total") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="期末WIP">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblend_wip" runat="server" ForeColor="Blue" Text='<%# Bind("end_wip") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Destroy_qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldestroy_qty" runat="server" ForeColor="Blue" Text='<%# Bind("destroy_qty") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="期初WIP+Input(1)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbegin_in" runat="server" ForeColor="Blue" Text='<%# Bind("begin_in") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Output+Scrap+期末WIP+Destroy(2)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblscrap_end_out" runat="server" ForeColor="Blue" Text='<%# Bind("scrap_end_out_destory") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="diff(1-2)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldiff" runat="server" ForeColor="Blue" Text='<%# Bind("diff") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 183px">
                                                    <asp:GridView ID="GridView3" runat="server" Font-Names="Arial" Font-Size="12px" Width="1000px"
                                                        AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView3_RowDataBound"
                                                        EmptyDataText="No Record!">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrownum" runat="server" ForeColor="Blue" Text='<%# Bind("rownum") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Shop">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblshop" runat="server" ForeColor="Blue" Text='<%# Bind("shop") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Shift_date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblshift_day" runat="server" ForeColor="Blue" Text='<%# Bind("shift_day") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Prod_id">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprod_id" runat="server" ForeColor="Blue" Text='<%# Bind("prod_id") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="期初WIP">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbegin_wip" runat="server" ForeColor="Blue" Text='<%# Bind("begin_wip") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Input">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblin_qty" runat="server" ForeColor="Blue" Text='<%# Bind("in_qty") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Out_qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblout_qty" runat="server" ForeColor="Blue" Text='<%# Bind("out_qty") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Scrap_total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblscrap_total" runat="server" ForeColor="Blue" Text='<%# Bind("scrap_total") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="期末WIP">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblend_wip" runat="server" ForeColor="Blue" Text='<%# Bind("end_wip") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Destroy_qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldestroy_qty" runat="server" ForeColor="Blue" Text='<%# Bind("destroy_qty") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="期初WIP+Input(1)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbegin_in" runat="server" ForeColor="Blue" Text='<%# Bind("begin_in") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Output+Scrap+期末WIP+Destroy(2)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblscrap_end_out_destory" runat="server" ForeColor="Blue" Text='<%# Bind("scrap_end_out_destory") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="diff(1-2)">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="hlParent" target="_blank" ForeColor="red" runat="server" Text='<% #(Eval("diff")) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="output_type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbloutput_type" runat="server" ForeColor="Blue" Text='<%# Bind("output_type") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
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

