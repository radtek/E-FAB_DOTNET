<%@ Page Language="C#" AutoEventWireup="true" CodeFile="night_inspection_data.aspx.cs" Inherits="night_inspection_night_inspection_data" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>夜巡資料-Data</title>
    <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">
            <fieldset>
                <legend align="center" style="width: 167px; color: blue; text-align: center">主管夜巡系統-Assign</legend>
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
                                                            <asp:TemplateField HeaderText="開始時間">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    SN:</br>
                                                                    <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl='<%#"http://"+ Server.MachineName +Context.Request.ApplicationPath+"/night_inspection/night_inspection_record.aspx?SN=" + DataBinder.Eval(Container.DataItem, "SN") %> '
                                                                        Text='<%# Bind("SN") %>' ForeColor="#3617E3" runat="server"></asp:HyperLink>
                                                                    </br>  開始時間:</br>
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
                                                                    <asp:TextBox ID="lblabnormal_description" runat="server" ForeColor="DarkGreen" Width="250px"
                                                                        Text='<%# Bind("abnormal_description") %>'></asp:TextBox></br>
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
                                                                    <asp:TextBox ID="lblarea_owner_phone" runat="server" ForeColor="DarkGreen" Text='<%# Bind("area_owner_phone") %>'></asp:TextBox></br>
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
                                                                    結案時間:</br>
                                                                    <asp:Label ID="lbldttm_close" runat="server" ForeColor="DarkGreen" Text='<%# Bind("dttm_close") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="上傳檔案">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:DataList ID="DataList1" runat="server" CellPadding="4" ForeColor="#333333" RepeatColumns="1">
                                                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="Black" />
                                                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                       
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="HyperLink12" Target="_blank" NavigateUrl='<%#"http://"+ Server.MachineName +Context.Request.ApplicationPath+"/night_inspection/upload_file/" + DataBinder.Eval(Container.DataItem, "file_name") %> '
                                                                        Text='<%# Bind("file_name") %>' ForeColor="#3617E3" runat="server"></asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    </asp:DataList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                                                    </asp:GridView>
            </fieldset>
            &nbsp;<br />
           
    </form>
</body>
</html>



