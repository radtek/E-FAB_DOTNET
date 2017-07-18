<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tool_query.aspx.cs" Inherits="Gridview_Example_Tool_query" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tooling查詢</title>
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
                                <td background="" colspan="4" class="pageTitle">
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>治工具資訊</strong></span></td>
                                            <td align="right" style="font-size: 12px; color: navy">
                                                * 表示必填!</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" align="center" style="height: 95px; width: 4%;">
                                    FAB&nbsp;</td>
                                <td style="width: 65px; height: 95px;" valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" Width="100px">
                                                    <asp:ListItem>T0</asp:ListItem>
                                                    <asp:ListItem>T1</asp:ListItem>
                                                    <asp:ListItem>T2</asp:ListItem>
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
                                <td class="pageTD" align="center" style="height: 95px; width: 5%;">
                                    PROCESS</td>
                                <td style="width: 37px; height: 95px;">
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
                            </tr>
                            <tr>
                                <td class="pageTD" align="center" style="height: 28px; width: 4%;">
                                    LAYER</td>
                                <td style="width: 65px">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ListBox ID="ListBox5" runat="server" SelectionMode="Multiple" Width="100px"></asp:ListBox>
                                            </td>
                                            <td style="width: 29px">
                                                <asp:Button ID="Button31" runat="server" Height="20px" OnClick="Button31_Click" Text=">"
                                                    Width="30px" /><br />
                                                <asp:Button ID="Button32" runat="server" Height="20px" OnClick="Button32_Click" Text=">>"
                                                    Width="30px" />
                                                <asp:Button ID="Button33" runat="server" Height="20px" OnClick="Button33_Click" Text="<"
                                                    Width="30px" />
                                                <asp:Button ID="Button34" runat="server" Height="20px" OnClick="Button34_Click" Text="<<"
                                                    Width="30px" />
                                            </td>
                                            <td>
                                                <asp:ListBox ID="ListBox6" runat="server" SelectionMode="Multiple" Width="100px"></asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="pageTD" align="center" style="height: 28px; width: 5%;">
                                    MODEL_NAME</td>
                                <td style="width: 47px">
                                    <table>
                                        <tr>
                                            <td style="width: 37px; font-size: 12pt;">
                                                <asp:ListBox ID="ListBox7" runat="server" SelectionMode="Multiple" Width="153px"
                                                    Height="70px"></asp:ListBox></td>
                                            <td align="left" style="height: 28px; width: 9px;">
                                                <asp:Button ID="Button41" runat="server" Height="20px" OnClick="Button41_Click" Text=">"
                                                    Width="30px" /><br />
                                                <asp:Button ID="Button42" runat="server" Height="20px" OnClick="Button42_Click" Text=">>"
                                                    Width="30px" />
                                                <asp:Button ID="Button43" runat="server" Height="20px" OnClick="Button43_Click" Text="<"
                                                    Width="30px" />
                                                <asp:Button ID="Button44" runat="server" Height="20px" OnClick="Button44_Click" Text="<<"
                                                    Width="30px" /></td>
                                            <td style="width: 2px; font-size: 12pt;">
                                                <asp:ListBox ID="ListBox8" runat="server" SelectionMode="Multiple" Width="150px"
                                                    Height="70px"></asp:ListBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" style="height: 42px; width: 4%;">
                                    TOOL_ID<br />
                                    (Query1)&nbsp;</td>
                                <td colspan="3" align="left" style="height: 42px; width: 109px;">
                                    <%--<span style="display: none">--%>
                                    <asp:TextBox ID="TextBox1" runat="server" Width="286px"></asp:TextBox><%-- </span>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" colspan="8" style="height: 35px">
                                    <asp:Button ID="ButtonQuery" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Query" OnClick="ButtonQuery_Click" />
                                    <asp:Button ID="ButtonQuery1" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Query1" OnClick="ButtonQuery1_Click" />&nbsp;
                                    <asp:Button ID="btnTask" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Add Task" OnClientClick="return AddTask();" />
                                    <asp:Button ID="btnExport" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Export" OnClick="btnExport_Click1" />
                                    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Gridview_Example/UPLOAD.aspx">上傳檔案</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <fieldset>
                                        <legend id="Legend5" runat="server" style="font-weight: bold; font-size: 12px; font-family: Arial;
                                            color: black">&nbsp;<asp:Image ID="btnShowDetail1" runat="server" ImageUrl="~/images/close13.gif" />&nbsp;&nbsp;查詢結果
                                        </legend>
                                        <table width="100%">
                                            <tr>
                                                <td style="height: 175px">
                                                    <%--<asp:GridView ID="gvTask" runat="server" Font-Names="Arial" Font-Size="12px" Width="100%"
                                                        AutoGenerateColumns="False" CellPadding="4" EmptyDataText="No Task!" OnRowDataBound="gvTask_RowDataBound"
                                                        ForeColor="#333333" GridLines="None">--%>
                                                    <asp:GridView ID="gvTask" runat="server" Font-Names="Arial" Font-Size="12px" Width="1000px"
                                                        AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" OnRowDataBound="gvTask_RowDataBound" EmptyDataText="No Task!"
                                                        OnRowEditing="gvTask_RowEditing" OnRowUpdating="gvTask_RowUpdating" OnRowDeleting="gvTask_RowDeleting"
                                                        OnRowCancelingEdit="gvTask_RowCancelingEdit">
                                                        <RowStyle ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SITE">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    SITE:</br>
                                                                    <asp:Label ID="lblSITE" runat="server" ForeColor="Red" Text='<%# Bind("SITE") %>'></asp:Label></br>
                                                                    FAB:</br>
                                                                    <asp:Label ID="lblFAB" runat="server" ForeColor="Red" Text='<%# Bind("FAB") %>'></asp:Label></br>
                                                                    PROCESS:</br>
                                                                    <asp:Label ID="lblPROCESS" runat="server" ForeColor="Red" Text='<%# Bind("PROCESS") %>'></asp:Label></br>
                                                                    LAYER:</br>
                                                                    <asp:Label ID="lblLAYER" runat="server" ForeColor="Red" Text='<%# Bind("LAYER") %>'></asp:Label></br>
                                                                    SN:</br>
                                                                    <asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("SN") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    SITE:</br>
                                                                    <asp:TextBox ID="lblSITE" runat="server" ForeColor="Red" Text='<%# Bind("SITE") %>'></asp:TextBox></br>
                                                                    FAB:</br>
                                                                    <asp:TextBox ID="lblFAB" runat="server" ForeColor="Red" Text='<%# Bind("FAB") %>'></asp:TextBox></br>
                                                                    PROCESS:</br>
                                                                    <asp:TextBox ID="lblPROCESS" runat="server" ForeColor="Red" Text='<%# Bind("PROCESS") %>'></asp:TextBox></br>
                                                                    LAYER:</br>
                                                                    <asp:TextBox ID="lblLAYER" runat="server" ForeColor="Red" Text='<%# Bind("LAYER") %>'></asp:TextBox></br>
                                                                     SN:</br>
                                                                    <asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("SN") %>'></asp:Label></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MODEL_NAME">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    MODEL_NAME:</br>
                                                                    <asp:Label ID="lblMODEL_NAME" runat="server" ForeColor="Red" Text='<%# Bind("MODEL_NAME") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    MODEL_NAME:</br>
                                                                    <asp:TextBox ID="lblMODEL_NAME" runat="server" ForeColor="Red" Text='<%# Bind("MODEL_NAME") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="EQ_VENDER">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    EQ_VENDER:</br>
                                                                    <asp:Label ID="lblEQ_VENDER" runat="server" ForeColor="Red" Text='<%# Bind("EQ_VENDER") %>'></asp:Label></br>
                                                                    EQ_TYPE:</br>
                                                                    <asp:Label ID="lblEQ_TYPE" runat="server" ForeColor="Red" Text='<%# Bind("EQ_TYPE") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    EQ_VENDER:</br>
                                                                    <asp:TextBox ID="lblEQ_VENDER" runat="server" ForeColor="Red" Text='<%# Bind("EQ_VENDER") %>'></asp:TextBox></br>
                                                                    EQ_TYPE:</br>
                                                                    <asp:TextBox ID="lblEQ_TYPE" runat="server" ForeColor="Red" Text='<%# Bind("EQ_TYPE") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TOOL_ID">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    TOOL_ID:</br>
                                                                    <asp:Label ID="lblTOOL_ID" runat="server" ForeColor="Red" Text='<%# Bind("TOOL_ID") %>'></asp:Label></br>
                                                                    MASK_SIZE:</br>
                                                                    <asp:Label ID="lblMASK_SIZE" runat="server" ForeColor="Red" Text='<%# Bind("MASK_SIZE") %>'></asp:Label></br>
                                                                    TOOL_Version:</br>
                                                                    <asp:Label ID="lblTOOL_Version" runat="server" ForeColor="Red" Text='<%# Bind("TOOL_Version") %>'></asp:Label></br>
                                                                    REASON:</br>
                                                                    <asp:Label ID="lblREASON" runat="server" ForeColor="Red" Text='<%# Bind("REASON") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    TOOL_ID:</br>
                                                                    <asp:TextBox ID="lblTOOL_ID" runat="server" ForeColor="Red" Text='<%# Bind("TOOL_ID") %>'></asp:TextBox></br>
                                                                    MASK_SIZE:</br>
                                                                    <asp:TextBox ID="lblMASK_SIZE" runat="server" ForeColor="Red" Text='<%# Bind("MASK_SIZE") %>'></asp:TextBox></br>
                                                                    TOOL_Version:</br>
                                                                    <asp:TextBox ID="lblTOOL_Version" runat="server" ForeColor="Red" Text='<%# Bind("TOOL_Version") %>'></asp:TextBox></br>
                                                                    REASON:</br>
                                                                    <asp:TextBox ID="lblREASON" runat="server" ForeColor="Red" Text='<%# Bind("REASON") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="LIBRARY">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    LIBRARY:</br>
                                                                    <asp:Label ID="lblLIBRARY" runat="server" ForeColor="Red" Text='<%# Bind("LIBRARY") %>'></asp:Label></br>
                                                                    PIXEL_CELL:</br>
                                                                    <asp:Label ID="lblPIXEL_CELL" runat="server" ForeColor="Red" Text='<%# Bind("PIXEL_CELL") %>'></asp:Label></br>
                                                                    PANEL_CELL:</br>
                                                                    <asp:Label ID="lblPANEL_CELL" runat="server" ForeColor="Red" Text='<%# Bind("PANEL_CELL") %>'></asp:Label></br>
                                                                    SUB_CELL:</br>
                                                                    <asp:Label ID="lblSUB_CELL" runat="server" ForeColor="Red" Text='<%# Bind("SUB_CELL") %>'></asp:Label></br>
                                                                    MASK_CELL:</br>
                                                                    <asp:Label ID="lblMASK_CELL" runat="server" ForeColor="Red" Text='<%# Bind("MASK_CELL") %>'></asp:Label></br>
                                                                    GDS:</br>
                                                                    <asp:Label ID="lblGDS" runat="server" ForeColor="Red" Text='<%# Bind("GDS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    LIBRARY:</br>
                                                                    <asp:TextBox ID="lblLIBRARY" runat="server" ForeColor="Red" Text='<%# Bind("LIBRARY") %>'></asp:TextBox></br>
                                                                    PIXEL_CELL:</br>
                                                                    <asp:TextBox ID="lblPIXEL_CELL" runat="server" ForeColor="Red" Text='<%# Bind("PIXEL_CELL") %>'></asp:TextBox></br>
                                                                    PANEL_CELL:</br>
                                                                    <asp:TextBox ID="lblPANEL_CELL" runat="server" ForeColor="Red" Text='<%# Bind("PANEL_CELL") %>'></asp:TextBox></br>
                                                                    SUB_CELL:</br>
                                                                    <asp:TextBox ID="lblSUB_CELL" runat="server" ForeColor="Red" Text='<%# Bind("SUB_CELL") %>'></asp:TextBox></br>
                                                                    MASK_CELL:</br>
                                                                    <asp:TextBox ID="lblMASK_CELL" runat="server" ForeColor="Red" Text='<%# Bind("MASK_CELL") %>'></asp:TextBox></br>
                                                                    GDS:</br>
                                                                    <asp:TextBox ID="lblGDS" runat="server" ForeColor="Red" Text='<%# Bind("GDS") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="OWNER">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    OWNER:</br>
                                                                    <asp:Label ID="lblOWNER" runat="server" ForeColor="Red" Text='<%# Bind("OWNER") %>'></asp:Label></br>
                                                                    DESIGNER:</br>
                                                                    <asp:Label ID="lblDESIGNER" runat="server" ForeColor="Red" Text='<%# Bind("DESIGNER") %>'></asp:Label></br>
                                                                    TAPEOUT_DATE:</br>
                                                                    <asp:Label ID="lblTAPEOUT_DATE" runat="server" ForeColor="Red" Text='<%# Bind("TAPEOUT_DATE") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    OWNER:</br>
                                                                    <asp:TextBox ID="lblOWNER" runat="server" ForeColor="Red" Text='<%# Bind("OWNER") %>'></asp:TextBox></br>
                                                                    DESIGNER:</br>
                                                                    <asp:TextBox ID="lblDESIGNER" runat="server" ForeColor="Red" Text='<%# Bind("DESIGNER") %>'></asp:TextBox></br>
                                                                    TAPEOUT_DATE:</br>
                                                                     <asp:TextBox ID="lblTAPEOUT_DATE" runat="server" ForeColor="Red" Text='<%# Bind("TAPEOUT_DATE") %>'></asp:TextBox></br>
                                                                    <%--<telerik:RadDatePicker ID="txtEstimateTAPEOUT_DATE" runat="server" Skin="Office2007"
                                                                        SkinID="Office2007" EnableTyping="False" SelectedDate='<%# Convert.ToDateTime(Eval("TAPEOUT_DATE")) %>'>
                                                                        <DateInput ID="TAPEOUT_DATE" Skin="Office2007" DateFormat="yyyy/MM/dd" ReadOnly="True"
                                                                            runat="server" Font-Size="10pt">
                                                                        </DateInput>
                                                                        <Calendar ID="Calendar1" Skin="Office2007" runat="server">
                                                                            <SpecialDays>
                                                                                <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday">
                                                                                </telerik:RadCalendarDay>
                                                                            </SpecialDays>
                                                                        </Calendar>
                                                                    </telerik:RadDatePicker>--%>
                                                                    </br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="RELEASE">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    RELEASE:</br>
                                                                    <asp:Label ID="lblRELEASE" runat="server" ForeColor="Red" Text='<%# Bind("RELEASE") %>'></asp:Label></br>
                                                                    RELEASE_DATE:</br>
                                                                    <asp:Label ID="lblRELEASE_DATE" runat="server" ForeColor="Red" Text='<%# Bind("RELEASE_DATE") %>'></asp:Label></br>
                                                                    SCRAP:</br>
                                                                    <asp:Label ID="lblSCRAP" runat="server" ForeColor="Red" Text='<%# Bind("SCRAP") %>'></asp:Label></br>
                                                                    SCRAP_DATE:</br>
                                                                    <asp:Label ID="lblSCRAP_DATE" runat="server" ForeColor="Red" Text='<%# Bind("SCRAP_DATE") %>'></asp:Label></br>
                                                                    DISCARD:</br>
                                                                    <asp:Label ID="lblDISCARD" runat="server" ForeColor="Red" Text='<%# Bind("DISCARD") %>'></asp:Label></br>
                                                                    DISCARD_DATE:</br>
                                                                    <asp:Label ID="lblDISCARD_DATE" runat="server" ForeColor="Red" Text='<%# Bind("DISCARD_DATE") %>'></asp:Label></br>
                                                                    TEST:</br>
                                                                    <asp:Label ID="lblTEST" runat="server" ForeColor="Red" Text='<%# Bind("TEST") %>'></asp:Label></br>
                                                                    STATUS:</br>
                                                                    <asp:Label ID="lblSTATUS" runat="server" ForeColor="Red" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    RELEASE:</br>
                                                                    <asp:TextBox ID="lblRELEASE" runat="server" ForeColor="Red" Text='<%# Bind("RELEASE") %>'></asp:TextBox></br>
                                                                    RELEASE_DATE:</br>
                                                                    <asp:TextBox ID="lblRELEASE_DATE" runat="server" ForeColor="Red" Text='<%# Bind("RELEASE_DATE") %>'></asp:TextBox></br>
                                                                    SCRAP:</br>
                                                                    <asp:TextBox ID="lblSCRAP" runat="server" ForeColor="Red" Text='<%# Bind("SCRAP") %>'></asp:TextBox></br>
                                                                    SCRAP_DATE:</br>
                                                                    <asp:TextBox ID="lblSCRAP_DATE" runat="server" ForeColor="Red" Text='<%# Bind("SCRAP_DATE") %>'></asp:TextBox></br>
                                                                    DISCARD:</br>
                                                                    <asp:TextBox ID="lblDISCARD" runat="server" ForeColor="Red" Text='<%# Bind("DISCARD") %>'></asp:TextBox></br>
                                                                    DISCARD_DATE:</br>
                                                                    <asp:TextBox ID="lblDISCARD_DATE" runat="server" ForeColor="Red" Text='<%# Bind("DISCARD_DATE") %>'></asp:TextBox></br>
                                                                    TEST:</br>
                                                                    <asp:TextBox ID="lblTEST" runat="server" ForeColor="Red" Text='<%# Bind("TEST") %>'></asp:TextBox></br>
                                                                    STATUS:</br>
                                                                    <asp:TextBox ID="lblSTATUS" runat="server" ForeColor="Red" Text='<%# Bind("STATUS") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TOOL_PROD">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:DataList ID="DataList1" runat="server" CellPadding="4" ForeColor="#333333" RepeatColumns="1">
                                                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="Black" />
                                                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                        <%--<ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" NavigateUrl='<%#Context.Request.ApplicationPath+"/upload_file/" + DataBinder.Eval(Container.DataItem, "file_name") %> '
                                                                    Text='<%# Bind("prod_name") %>' runat="server"></asp:HyperLink>
                                                            </ItemTemplate>--%>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltool_prod" runat="server" ForeColor="green" Text='<%# Bind("prod_name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    </asp:DataList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UPDATE_USER">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    UPDATE_USER:</br>
                                                                    <asp:Label ID="lblUPDATE_USER" runat="server" ForeColor="Red" Text='<%# Bind("UPDATE_USER") %>'></asp:Label></br>
                                                                    DTTM:</br>
                                                                    <asp:Label ID="lblDTTM" runat="server" ForeColor="Red" Text='<%# Bind("DTTM") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ImageUrl="~/images/bdelete.gif" ID="btnDel" runat="server" CommandName="Delete"
                                                                        ToolTip="刪除" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowEditButton="True" ButtonType="Button" />
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

