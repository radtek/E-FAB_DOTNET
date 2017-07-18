<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cycle_time.aspx.cs" Inherits="chairman_cycle_time" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Cycletime</title>
     <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
   <script language="javascript" type="text/javascript">		
    function CVP_DrillDownClick(Shop)
    {
        //if( IndexName == "DAILY TARGET INPUT" || IndexName == "MPS INPUT" || IndexName == "MPS OUTPUT" )
        //{
        //    return;
        //}
        //radopen(Url,Name)
        //# Url. This supplies the URL for the content window. If this is given an argument of null, the NavigateUrl property set for the window on the server is used.
        //# Name. This is the ID of particular (existing) RadWindow object to show. If this is given an argument of null, the function creates a new window.
        var oWnd = radopen("IN_OUT_CVP_DrillDown_Report.aspx?shop=" + Shop , null );
        //oWnd.setSize(750,500);           
        //oWnd.center();
        oWnd.Maximize();
    }
    
    function OPENTEST()
    {
        var url = "http://www.google.com.tw";
        var oWnd = radopen(url, "RadWindow1");
        oWnd.Maximize();
    }          
    
    
    function DrillDownClick(Shop)
    {           
        //radopen(Url,Name)
        //# Url. This supplies the URL for the content window. If this is given an argument of null, the NavigateUrl property set for the window on the server is used.
        //# Name. This is the ID of particular (existing) RadWindow object to show. If this is given an argument of null, the function creates a new window.
        var oWnd = radopen("cycle_time_Report_DrillDown.aspx?shop=" + Shop , null );
        //oWnd.setSize(750,500);           
        //oWnd.center();
        oWnd.Maximize();
    }        
            
    </script>
     
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            </asp:ScriptManager>
   <table  id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
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
                                <td background="" colspan="3" class="pageTitle">
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>PPC Cycletime</strong></span></td>
                                            <td align="right" style="font-size: 12px; color: navy">
                                                </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td  align="center" valign="middle" style="height: 10px;
                                    text-align: center;" colspan="3">
                                    <br />
                                    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
        <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView2_RowDataBound" OnPreRender="GridView2_PreRender" >
            <RowStyle BackColor="#F7F7DE" />
            <FooterStyle BackColor="#CCCC99" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
                                    &nbsp;&nbsp;
                                </td>
                                   
                               
                            </tr>
                        </table>
                        &nbsp;<br />
                       
                   
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
    
    </div>
        &nbsp;
    </form>
</body>
</html>