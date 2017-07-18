<%@ Page Language="C#" AutoEventWireup="true" CodeFile="top.aspx.cs" Inherits="ary_top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TOP 首頁</title>
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
<body background="" style="background-color: #ffffff">
    <form id="form1" runat="server">
        <br />
    <div id="container" align="center">
    
   <%-- <marquee  id="alarm_scroller"  style="FILTER: Alpha(Opacity=200, FinishOpacity=0, Style=2, StartX=150, StartY=150, FinishX=0, FinishY=0); WIDTH: 500px; COLOR:#0000FF; HEIGHT: 50px; TEXT-ALIGN: center" scrollAmount=2 scrollDelay=100 direction=up width=528 bgColor=#FFFF80 height=390 align="middle" border="0" runat="server" >  </marquee>--%>
        
        
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" Font-Size="X-Small"
            DataSourceID="SqlDataSource1" AllowPaging="True" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" Height="305px" Width="949px" BorderStyle="None" PageSize="5">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        
                
               <Columns>
                                                            <asp:TemplateField HeaderText="RN" >
                                                                  <ItemTemplate>
                                                                   
                                                                    <asp:Label ID="lblrownum" runat="server" ForeColor="DarkGreen" Text='<%# Bind("rownum") %>'></asp:Label></br>
                                                                   
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="開始時間">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    SN:</br>
                                                                    <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl='<%#"http://"+ Server.MachineName +Context.Request.ApplicationPath+"/night_inspection/night_inspection_record.aspx?SN=" + DataBinder.Eval(Container.DataItem, "SN") %> '
                                                                        Text='<%# Bind("SN") %>' ForeColor="#3617E3" runat="server"></asp:HyperLink>
                                                                        </br>
                                                                    開始時間:</br>
                                                                    <asp:Label ID="lblstart_time" runat="server" ForeColor="DarkGreen" Text='<%# Bind("start_time") %>'></asp:Label></br>
                                                                    紀錄人員:</br>
                                                                    <asp:Label ID="lblrecord_person" runat="server" ForeColor="DarkGreen" Text='<%# Bind("record_person") %>'></asp:Label></br>
                                                                    
                                                                   
                                                                </ItemTemplate>
                                                               
                                                            </asp:TemplateField>
                                                            
                                                             <asp:TemplateField HeaderText="異常">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    Abnormal_type:</br>
                                                                    <asp:Label ID="lblabnormal_type" runat="server" ForeColor="DarkGreen" Text='<%# Bind("abnormal_type") %>'></asp:Label></br>
                                                                    Abnormal_area:</br>
                                                                    <asp:Label ID="lblabnormal_area" runat="server" ForeColor="DarkGreen" Text='<%# Bind("abnormal_area") %>'></asp:Label></br>
                                                                    Dep:</br>
                                                                    <asp:Label ID="lbldep" runat="server" ForeColor="DarkGreen" Text='<%# Bind("dep") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="異常敘述">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                   
                                                                    <asp:Label ID="lblabnormal_description" runat="server" ForeColor="DarkGreen" Text='<%# Bind("abnormal_description") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                  
                                                                    <asp:TextBox ID="lblabnormal_description" runat="server" ForeColor="DarkGreen" Width="250px" Text='<%# Bind("abnormal_description") %>'></asp:TextBox></br>
                                                                    
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="負責人">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    人員:</br>
                                                                    <asp:Label ID="lblarea_owner" runat="server" ForeColor="DarkGreen" Text='<%# Bind("area_owner") %>'></asp:Label></br>
                                                                    電話:</br>
                                                                    <asp:Label ID="lblarea_owner_phone" runat="server" ForeColor="DarkGreen" Text='<%# Bind("area_owner_phone") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                  
                                                                    <asp:TextBox ID="lblarea_owner_phone" runat="server" ForeColor="DarkGreen"  Text='<%# Bind("area_owner_phone") %>'></asp:TextBox></br>
                                                                    
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="對策">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                   
                                                                    <asp:Label ID="lblpolicy" runat="server" ForeColor="DarkGreen" Text='<%# Bind("policy") %>'></asp:Label></br>
                                                                   
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="狀態">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    結案:</br>
                                                                    <asp:Label ID="lblopen_close_flag" runat="server" ForeColor="DarkGreen" Text='<%# Bind("open_close_flag") %>'></asp:Label></br>
                                                                    結案人:</br>
                                                                    <asp:Label ID="lblclose_person" runat="server" ForeColor="DarkGreen" Text='<%# Bind("close_person") %>'></asp:Label></br>
                                                                    最後更新時間:</br>
                                                                    <asp:Label ID="lbldttm_close" runat="server" ForeColor="DarkGreen" Text='<%# Bind("dttm_close") %>'></asp:Label></br>
                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                           
                                                        </Columns>
                
      
            <SelectedRowStyle BackColor="#009999" ForeColor="#CCFF99" Font-Bold="True" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        <RowStyle BackColor="White" ForeColor="#003399" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCONN_OARPT_MEETUSER %>"
            ProviderName="<%$ ConnectionStrings:DBCONN_OARPT_MEETUSER.ProviderName %>" SelectCommand="select rownum,tb2.* from (

select sn,
       record_person,
       to_char(start_time,'YYYY/MM/DD')as start_time,
       abnormal_type,
       abnormal_area,
       dep,
       abnormal_description,
       area_owner,
       area_owner_phone,
       policy,
       open_close_flag,
       close_person,
       dttm_creste,
       to_char(dttm_close,'YYYY/MM/DD')as dttm_close
  from night_inspection_record
  order by start_time desc

)tb2 
">
        </asp:SqlDataSource>
        <br />
    
    </div>
    </form>
    <table border="0" cellpadding="0" cellspacing="0" style="background-color: white"
                width="100%">
                <tr>
                    <td bgcolor="gray" height="28" style="font-size: 11px; color: #ffffff; line-height: 16px;
                        font-family: Verdana,?啁敦?��?; text-align: center; text-decoration: none">
                        奇美電子股份有限公司 版權所有 Copyright &copy; 2010 Chimei-Innolux  Corp., Design By CIM 謝正一(64179)</td>
                </tr>
</table>
</body>
</html>
