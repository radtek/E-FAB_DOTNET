<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LH_LAM_DEFECT_ALARM_QUERY.aspx.cs" Inherits="LH_REALITY_LH_LAM_DEFECT_ALARM_QUERY" %>



<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LH LAM DEFECT ALARM</title>
    <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            </asp:ScriptManager>
            <br />
            <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
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
                        <table align="center" cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="90%"
                            bordercolorlight="#7a9cb7" border="1">
                            <tr>
                                <td background="" colspan="4" class="pageTitle" style="height: 71px">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" style="width: 533px">
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><span style="font-family: Times New Roman">
                                                    LH LAM DEFECT ALARM</span><strong>-查詢</strong></span></td>
                                            <td align="right" style="font-size: 12px; color: navy">
                                                </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" align="center" style="height: 24px; width: 12%;">
                                    發生時間(開始)</td>
                                <td style="width: 107px; height: 24px;" align="left" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <telerik:RadDatePicker ID="txtEstimateStartDate" runat="server" EnableTyping="False"
                                                Skin="Office2007" SkinID="Office2007">
                                                <%-- <DateInput ID="DateInput1" runat="server" DateFormat="yyyy/MM/dd hh:mm:ss" Font-Size="10pt"--%>
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
                                            <br />
                                            <asp:DropDownList ID="DropDownList5" runat="server">
                                            </asp:DropDownList>時
                                            <asp:DropDownList ID="DropDownList8" runat="server">
                                            </asp:DropDownList>分
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <td class="pageTD" align="center" style="height: 24px; width: 12%;">
                                        發生時間(結束)</td>
                                    <td style="width: 107px; height: 24px;" align="left" valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <telerik:RadDatePicker ID="txtEstimateEndDate" runat="server" EnableTyping="False"
                                                    Skin="Office2007" SkinID="Office2007">
                                                    <DateInput DateFormat="yyyy/MM/dd" Font-Size="10pt" ReadOnly="True" Skin="Office2007">
                                                    </DateInput>
                                                    <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                        ViewSelectorText="x">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Date="" Repeatable="Today">
                                                                <ItemStyle CssClass="rcToday" />
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                </telerik:RadDatePicker>
                                                <br />
                                                <asp:DropDownList ID="DropDownList15" runat="server">
                                                </asp:DropDownList>時
                                                <asp:DropDownList ID="DropDownList18" runat="server">
                                                </asp:DropDownList>分
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                            </tr>
                            <tr>
                                <td align="center" class="pageTD" style="width: 12%; height: 24px">
                                    班別</td>
                                <td align="left" style="height: 24px" valign="top" colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList1" runat="server">
                                            </asp:DropDownList>
                                            <br />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" colspan="8" style="height: 35px">
                                    <asp:Button ID="ButtonQuery" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Query" OnClick="ButtonQuery_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnExport" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Export" OnClick="btnExport_Click1" />&nbsp; (預設抓前6小時資料)</td>
                            </tr>
                            <tr>
                                <td colspan="8" style="height: 100px">
                                    <fieldset>
                                        <legend id="Legend5" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
                                            color: black">&nbsp;&nbsp;&nbsp;查詢結果 </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="height: 30px">
                                                    <%--<asp:GridView ID="gvTask" runat="server" Font-Names="Arial" Font-Size="12px" Width="100%"
                                                        AutoGenerateColumns="False" CellPadding="4" EmptyDataText="No Task!" OnRowDataBound="gvTask_RowDataBound"
                                                        ForeColor="#333333" GridLines="None">--%>
                                                    <asp:GridView ID="GridView1" runat="server" Font-Names="Arial" Font-Size="12px" Width="1000px"
                                                        AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" OnRowDataBound="GridView1_RowDataBound"
                                                        BorderStyle="None" BorderWidth="1px" EmptyDataText="No Task!">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RN">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrownum" runat="server" ForeColor="DarkGreen" Text='<%# Bind("rownum") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DAYHOUR">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDAYHOUR" runat="server" ForeColor="DarkGreen" Text='<%# Bind("DAYHOUR") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                             <asp:TemplateField HeaderText="SHIFT">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSHIFT" runat="server" ForeColor="DarkGreen" Text='<%# Bind("SHIFT") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="PROD_NAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPROD_NAME" runat="server" ForeColor="DarkGreen" Text='<%# Bind("PROD_NAME") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PRODUCTIONTYPE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPRODUCTIONTYPE" runat="server" ForeColor="DarkGreen" Text='<%# Bind("PRODUCTIONTYPE") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="STEP_NAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSTEP_NAME" runat="server" ForeColor="DarkGreen" Text='<%# Bind("STEP_NAME") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="TOTAL_YIELD">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTOTAL_YIELD" runat="server" ForeColor="DarkGreen" Text='<%# Bind("TOTAL_YIELD") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                             <asp:TemplateField HeaderText="DEFECTCODE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDEFECTCODE" runat="server" ForeColor="DarkGreen" Text='<%# Bind("DEFECTCODE") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            
                                                             <asp:TemplateField HeaderText="DEFECTDESC">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDEFECTDESC" runat="server" ForeColor="DarkGreen" Text='<%# Bind("DEFECTDESC") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            
                                                             <asp:TemplateField HeaderText="GRADE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGRADE" runat="server" ForeColor="DarkGreen" Text='<%# Bind("GRADE") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            
                                                            
                                                            
                                                            
                                                            
                                                            
                                                            <asp:TemplateField HeaderText="DEFECT_QTY">
                                                              
                                                                <ItemTemplate>
                                                                   
                                                                    <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl='<%#"http://10.56.131.22" +Context.Request.ApplicationPath+"/LH_REALITY/LH_LAM_DEFECT_ALARM_DETAIL.aspx?shift=" + DataBinder.Eval(Container.DataItem, "shift")+"&step_name="+DataBinder.Eval(Container.DataItem, "STEP_NAME")+"&start="+txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd").Replace("/", "") + DropDownList5.Text+"&end="+txtEstimateEndDate.SelectedDate.Value.ToString("yyyy/MM/dd").Replace("/", "") + DropDownList15.Text+"&defectcode="+ DataBinder.Eval(Container.DataItem, "DEFECTCODE")%> '
                                                                        Text='<%# Bind("DEFECT_QTY") %>' ForeColor="#3617E3" runat="server"></asp:HyperLink>
                                      
                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MOVE_QTY">
                                                              
                                                                <ItemTemplate>
                                                              
                                                                    <asp:Label ID="lblMOVE_QTY" runat="server" ForeColor="DarkGreen" Text='<%# Bind("MOVE_QTY") %>'></asp:Label></br>
                                                              
                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DEFECT_RATIO">
                                                                
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldefect_ratio" runat="server" ForeColor="DarkGreen" Text='<%# Bind("defect_ratio") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                               
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ALARM_LEVEL">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblALARM_LEVEL" runat="server" ForeColor="DarkGreen" Text='<%# Bind("ALARM_LEVEL") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="OWNER">
                                                               
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOWNER" runat="server" ForeColor="DarkGreen" Text='<%# Bind("OWNER") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                      
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                        <AlternatingRowStyle BackColor="#E0E0E0" />
                                                    </asp:GridView>
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
                    <td style="height: 9px">
                        <img src="../images/tables/default_lb.gif"></td>
                    <td style="background-image: url(../images/tables/default_b.gif); height: 9px;">
                        <img height="9" src="../images/tables/transparent.gif"></td>
                    <td style="height: 9px">
                        <img src="../images/tables/default_rb.gif"></td>
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

