<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CF_fwfullautomonitor.aspx.cs" Inherits="CF_CF_FULL_AUTO_CF_fwfullautomonitor" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CF FullAuto
            Ratio</title>
    <style type="text/css">
#container {
  margin: 0 auto;
  width: 85%;
  font size='3'
}
</style>
<meta http-equiv="Content-Type" content="text/html; charset=big5" />
 <meta http-equiv="Page-Enter" content="blendTrans(duration=0.5)" />
  <meta http-equiv="Page-Exit" content="blendTrans(duration=0.5)" />
</head>
<body background="images/bg_line2.gif" style="background-color: #ffffff; text-align: center;">
    <form id="form1" runat="server">
    <div> 
        <span style="font-size: 14pt; color: #3333ff">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
                </asp:ScriptManager>
            <br />
            <fieldset > 
<legend align="center" style="color:blue;text-align:center"><strong><span style="color: #3300ff">
    CF FullAuto Ratio&nbsp;</span></strong></legend> 
 <table style="width: 650px; height: 84px">
                <tr>
                    <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; height: 58px;">
                        <span style="color: #000000"><strong>選擇日期</strong></span></td>
                    <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; height: 58px; text-align: left;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                        <ContentTemplate>
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
                                    </asp:UpdatePanel></td>
                    <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; height: 58px; width: 14px;">
                    </td>
                </tr>
                <tr>
                    <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; height: 32px;">
                        <strong><span style="color: #000000">統計型態</span></strong></td>
                    <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; height: 32px; text-align: left;">
                        &nbsp;<asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>weekly</asp:ListItem>
                            <asp:ListItem>monthly</asp:ListItem>
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; height: 32px; width: 14px;">
                    </td>
                </tr>
                <tr>
                    <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid">
                        &nbsp;</td>
                    <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; text-align: left;">
                        <asp:Button ID="Button1" runat="server" Text="送出" OnClick="Button1_Click" />(Data
                        From 2011/05/02)</td>
                    <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; border-bottom: gray thin solid; width: 14px;">
                    </td>
                </tr>
            </table>
            <DCWC:Chart ID="Chart1" runat="server" Height="396px" Width="670px" BackColor="Wheat"
                EnableViewState="True" BorderLineColor="Blue">
                <Legends>
                    <DCWC:Legend Alignment="Center" Docking="Top" Name="Default">
                    </DCWC:Legend>
                </Legends>
                <Titles>
                    <DCWC:Title Name="Title1" Text="CF FullAuto Ratio(統計) ">
                    </DCWC:Title>
                </Titles>
                <BorderSkin SkinStyle="FrameThin5" />
                <Series>
                    <DCWC:Series BackGradientEndColor="Yellow" BorderColor="64, 64, 64" Color="BlueViolet"
                        Name="manu" ShadowOffset="1" BorderStyle="NotSet" CustomAttributes="DrawingStyle=Cylinder">
                    </DCWC:Series>
                    <DCWC:Series BackGradientEndColor="Yellow" BorderColor="64, 64, 64" Color="Red"
                        Name="auto" ShadowOffset="1" BorderStyle="NotSet" CustomAttributes="DrawingStyle=Cylinder">
                    </DCWC:Series>
                    
                    
                    
                </Series>
                <ChartAreas>
                    <DCWC:ChartArea BackColor="224, 224, 224" BackGradientEndColor="CornflowerBlue" BackGradientType="LeftRight"
                        Name="Default" AlignOrientation="None">
                        <AxisY Title="【次數】">
                            <MajorGrid Enabled="False" />
                            <MajorTickMark Style="none" />
                        </AxisY>
                        <AxisX LabelsAutoFit="False" LineStyle="NotSet" Margin="False" MarksNextToAxis="False">
                            <MajorGrid Enabled="False" />
                          
                        </AxisX>
                        <AxisY2>
                            <MajorGrid Enabled="False" />
                        </AxisY2>
                    </DCWC:ChartArea>
                </ChartAreas>
            </DCWC:Chart>
            <br/>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2" Font-Size="Large"
             ForeColor="Black" GridLines="None" AllowPaging="True"  BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" Height="335px" Width="670px"  OnPageIndexChanging="GridView1_PageIndexChanging">
            <FooterStyle BackColor="Tan" />
            <Columns>
                
                <asp:BoundField DataField="eqp" HeaderText="EQP"   >
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="manu" HeaderText="MANU"   >
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="auto" HeaderText="AUTO"   >
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
            
             <asp:BoundField DataField="total" HeaderText="TOTAL"   >
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="auto_ratio" HeaderText="AUTO_RATIO(%)"    >
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                
                     <asp:BoundField DataField="eq2eq" HeaderText="直送批數"   >
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                
                     <asp:BoundField DataField="eq2eq_p" HeaderText="EQ2EQ(%)"   >
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
             
                
       
          
               
            
                
               
                
            </Columns>
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <AlternatingRowStyle BackColor="PaleGoldenrod" />    
        </asp:GridView>
</fieldset> 

           
            <br />
            </span>
   </div>
    </form>
   
</body>
</html>
