<%@ Page Language="C#" AutoEventWireup="true" CodeFile="scrap_detail.aspx.cs" Inherits="CL1_scrap_detail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CL1_Scrap_Detail</title>
   <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
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
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>CL1_Scrap_Detail</strong></span></td>
                                            <td align="right" style="font-size: 12px; color: navy">
                                                * 表示必填!</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" align="center" style="height: 23px; width: 20%;">
                                    Start Time</td>
                                <td style="width: 40px; height: 23px;" valign="top">
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
                                  
                                  
                                </td>
                                <td class="pageTD" align="center" style="height: 23px; width: 20%;">
                                    End &nbsp;Time</td>
                                <td style="width: 47px; height: 23px;" valign="top">
                                   
                                            <telerik:RadDatePicker ID="txtEstimateEndDate" runat="server" EnableTyping="False"
                                                Skin="Office2007" SkinID="Office2007">
                                                <DateInput DateFormat="yyyy/MM/dd" Font-Size="10pt"
                                                    ReadOnly="True" Skin="Office2007">
                                                </DateInput>
                                                <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Date="" Repeatable="Today">
                                                            <ItemStyle CssClass="rcToday" />
                                                        </telerik:RadCalendarDay>
                                                    </SpecialDays>
                                                </Calendar>
                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                            </telerik:RadDatePicker>
                                       
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="pageTD" style="width: 5%; height: 12px">
                                    SHOP</td>
                                <td style="width: 40px; height: 12px" valign="middle">
                                   
                                            <asp:DropDownList ID="DropDownList1" runat="server">
                                            </asp:DropDownList>&nbsp;
                                </td>
                                <td align="center" class="pageTD" style="width: 5%; height: 12px" valign="top">
                                    SCRAP FLAG</td>
                                <td style="width: 37px; height: 12px" valign="middle">
                                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="pageTD" align="center" style="height: 74px; width: 5%;">
                                    LOT TYPE</td>
                                <td style="width: 40px; height: 74px;" valign="top">
                                 
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" Width="100px">
                                                           
                                                        </asp:ListBox>
                                                    </td>
                                                    <td style="height: 95px; width: 1px;">
                                                        <asp:Button ID="Button11" runat="server" Height="20px" OnClick="Button11_Click" Text=">"
                                                            Width="30px" /><br />
                                                        <asp:Button ID="Button12" runat="server" Height="20px" OnClick="Button12_Click" Text=">>"
                                                            Width="30px" />
                                                        <asp:Button ID="Button13" runat="server" Height="20px" OnClick="Button13_Click" Text="<"
                                                            Width="30px" />
                                                        <asp:Button ID="Button14" runat="server" Height="20px" OnClick="Button14_Click" Text="<<"
                                                            Width="30px" /></td>
                                                    <td style="width: 47px; height: 95px;">
                                                        <asp:ListBox ID="ListBox2" runat="server" SelectionMode="Multiple" Width="100px"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                      
                                </td>
                                <td class="pageTD" align="center" style="height: 74px; width: 5%;">
                                    LOT ID</td>
                                <td style="width: 37px; height: 74px;">
                              
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ListBox ID="ListBox3" runat="server" SelectionMode="Multiple" Width="100px"></asp:ListBox></td>
                                                    <td align="left" style="height: 95px; width: 9px;">
                                                        <asp:Button ID="Button21" runat="server" Height="20px" OnClick="Button21_Click" Text=">"
                                                            Width="30px" /><br />
                                                        <asp:Button ID="Button22" runat="server" Height="20px" OnClick="Button22_Click" Text=">>"
                                                            Width="30px" />
                                                        <asp:Button ID="Button23" runat="server" Height="20px" OnClick="Button23_Click" Text="<"
                                                            Width="30px" />
                                                        <asp:Button ID="Button24" runat="server" Height="20px" OnClick="Button24_Click" Text="<<"
                                                            Width="30px" /></td>
                                                    <td style="width: 2px; height: 95px;">
                                                        <asp:ListBox ID="ListBox4" runat="server" SelectionMode="Multiple" Width="100px"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                       
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" colspan="8" style="height: 35px">
                                    <asp:Button ID="ButtonQuery" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Query" OnClick="ButtonQuery_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnExport" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Export" OnClick="btnExport_Click1" />&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;</td>
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
                                                        BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView1_RowDataBound" EmptyDataText="No Record!"
                                                         
                                                        >
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lblrownum" runat="server" ForeColor="Blue"  Text='<%# Bind("rownum") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="productspecname">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lblproductspecname" runat="server" ForeColor="Blue"  Text='<%# Bind("productspecname") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                             <asp:TemplateField HeaderText="processflowname">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lblprocessflowname" runat="server" ForeColor="Blue" Text='<%# Bind("processflowname") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="producttype">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lblproducttype" runat="server" ForeColor="Blue"  Text='<%# Bind("producttype") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="lotname">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lbllotname" runat="server" ForeColor="Blue" Text='<%# Bind("lotname") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="productname">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lblproductname" runat="server" ForeColor="Blue"  Text='<%# Bind("productname") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="processoperationname">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lblprocessoperationname" runat="server" ForeColor="Blue"  Text='<%# Bind("processoperationname") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="eventname">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lbleventname" runat="server" ForeColor="Blue"  Text='<%# Bind("eventname") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="eventtime">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lbleventtime" runat="server" ForeColor="Blue"  Text='<%# Bind("eventtime") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="productiontype">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lblproductiontype" runat="server" ForeColor="Blue" Text='<%# Bind("productiontype") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="carriername">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lblcarriername" runat="server" ForeColor="Blue"  Text='<%# Bind("carriername") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="eventcomment">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lbleventcomment" runat="server" ForeColor="Blue"  Text='<%# Bind("eventcomment") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                             <asp:TemplateField HeaderText="eventuser">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lbleventuser" runat="server" ForeColor="Blue"  Text='<%# Bind("eventuser") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                             <asp:TemplateField HeaderText="subproductquantity1">
                                                              <ItemTemplate>
                                                          
                                                                    <asp:Label ID="lblsubproductquantity1" runat="server" ForeColor="Blue"  Text='<%# Bind("subproductquantity1") %>'></asp:Label></br>
                                                                   
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
