<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="main.aspx.cs" Inherits="_Default" ResponseEncoding="big5" EnableEventValidation="false"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <LINK REL="SHORTCUT ICON" HREF="img/doiMenu.ico" />    
    <title>夜巡系統</title>    
    <style type="text/css">
    <!--
    body, table {
	    font-family: Tahoma, Verdana, Arial;
	    font-size: 9pt;
	    text-decoration: none;
    }
    -->
    </style>
    <script type="text/javascript" src="js/doiMenuDOM.js"></script>
    <script type="text/javascript" src="js/functions.js"></script>
    <script type="text/javascript" src="js/menuData.js"></script><!-- 動態menu在此設定 -->               
    
    
</head>
<body>
    <form id="form1" runat="server">
    <!--
    <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Blue" Width="226px" Height="13px"></asp:Label>
    -->
    <div align="center" style="background: url(images/banner/night.jpg);"><!-- 設定背景圖案background-color:blue     images/banner/top_part.gif-->    
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr> 
           
            <td align="left" style="height: 118px; width: 101%;">
                <table width="100%" border="0">
                    
                </table>
                </td>                
        </tr> 
        <!--  
        <tr>
        <td colspan="3" align="left" style="width: 100px; height: 20px">
        <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Blue" Width="226px" Height="13px"></asp:Label></td>
            </td>
        </tr> 
        -->
        
        <!-- ///////////////////////////////////////////////////////////////////////////// -->  
        <tr>
        <td style="height: 19px; width: 100%; background-color: black;">
      <script language="JavaScript" type="text/JavaScript">
                                mm0.SetPosition('relative',0,0);
                                mm0.SetCorrection(30,-18); //設定submenu的出現位置
                                mm0.SetCellSpacing(1);
                                mm0.SetItemDimension(230,40);
                                mm0.SetFont('verdana,tahoma,arial','14pt');
                                mm0.SetItemText('black','center','bold','','');
                                //mm0.SetShadow(true,'#B0B0B0',6);
                                mm0.SetItemBackground('#DBF0F7','','','');//#DBF0F7
                                //mm0.SetBackground('blue','','','');
                                mm0._pop.SetCorrection(0,0);
                                mm0._pop.SetItemDimension(170,20);
                                mm0._pop.SetPaddings(1);
                                mm0._pop.SetBackground('whitesmoke','../img/xp.gif','repeat-y','top left');
                                mm0._pop.SetSeparator(125,'right','gray','');
                                mm0._pop.SetExpandIcon(true,'',6);
                                mm0._pop.SetFont('tahoma,verdana,arial','12pt');
                                mm0._pop.SetBorder(2,'gray','solid');
                                mm0._pop.SetShadow(true,'#B0B0B0',6);
                                mm0._pop.SetDelay(500);
                                mm0._pop.SetItemTextHL('red','','','','');
                                mm0.Build();
                            </script>
        </td>
        
        </tr>         
        <tr>
            <td colspan="2" style="height: 718px">
                <iframe id="mainiframe" name="mainiframe" width="100%" height="730px" frameborder="0" scrolling="yes" src="top.aspx">
            </td>
        </tr>    
    </table>
    
    </div>
    </form>    
</body>
</html>
