<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RealTimeKPI.aspx.cs" Inherits="RealTimeKPI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>RealTimeKPI</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
    
        <br />
        
<fieldset > 
<legend align="center" style="color:blue;text-align:center"><span style="font-size: 16pt">
    <strong>RealTimeKPI:</strong></span></legend> 
    <table hight="100%" width="100%">
        <tr>
            <td align="center" valign="middle">
               <table style="width: 393px; height: 37px">
            <tr>
                <td style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                    border-top-color: gray; border-right-style: solid; border-left-style: solid;
                    border-right-color: gray; border-bottom-style: solid">
                    SHOP</td>
                <td style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                    border-top-color: gray; border-right-style: solid; border-left-style: solid;
                    border-right-color: gray; border-bottom-style: solid; text-align: left;">
                    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" >
                        <asp:ListItem>請選擇</asp:ListItem>
                        <asp:ListItem>T0CELL</asp:ListItem>
                        <asp:ListItem>T1CELL</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                    border-top-color: gray; border-right-style: solid; border-left-style: solid;
                    border-right-color: gray; border-bottom-style: solid">
                    &nbsp;</td>
                <td style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                    border-top-color: gray; border-right-style: solid; border-left-style: solid;
                    border-right-color: gray; border-bottom-style: solid; text-align: left;">
                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="ExportToExcel" /></td>
            </tr>
        </table>
                <br />
                <table style="width: 800px; height: 200px">
                    <tr>
                        <td style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                    border-top-color: gray; border-right-style: solid; border-left-style: solid;
                    border-right-color: gray; border-bottom-style: solid" colspan="2">
                            <span style="font-size: 16pt"><strong>
                            即時投入資訊</strong></span></td>
                    </tr>
                    <tr>
                        <td style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                    border-top-color: gray; border-right-style: solid; border-left-style: solid;
                    border-right-color: gray; border-bottom-style: solid" colspan="2">
                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" Height="400px" Width="600px" EmptyDataText="No Record!!!">
                                 <Columns>
                                                            <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                            <asp:TemplateField HeaderText="cut_time">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcut_time" runat="server" ForeColor="#000000" Text='<%# Bind("cut_time") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblcut_time" runat="server" ForeColor="#000000" Text='<%# Bind("cut_time") %>'></asp:Label></br>
                                                                    <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="shop">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblshop" runat="server" ForeColor="#000000" Text='<%# Bind("shop") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblshop" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("shop") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                            <asp:TemplateField HeaderText="product">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblproduct" runat="server" ForeColor="#000000" Text='<%# Bind("product") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblproduct" runat="server" ForeColor="#000000" Width="250px"
                                                                        Text='<%# Bind("product") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="月目標">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl月目標" runat="server" ForeColor="#000000" Text='<%# Bind("月目標") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl月目標" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("月目標") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        
                                                            <asp:TemplateField HeaderText="日目標">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl日目標" runat="server" ForeColor="#000000" Text='<%# Bind("日目標") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl日目標" runat="server" ForeColor="#000000" Width="250px"
                                                                        Text='<%# Bind("日目標") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="日實積">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl日實積" runat="server" ForeColor="#000000" Text='<%# Bind("日實積") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl日實積" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("日實積") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="累積計畫">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl累積計畫" runat="server" ForeColor="#000000" Text='<%# Bind("累積計畫") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl累積計畫" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("累積計畫") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="累積實績">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl累積實績" runat="server" ForeColor="#000000" Text='<%# Bind("累積實績") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl累積實績" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("累積實績") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                          
                                                            <asp:TemplateField HeaderText="差異">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl差異" runat="server" ForeColor="#000000" Text='<%# Bind("差異") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl差異" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("差異") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                <RowStyle BackColor="#EFF3FB" Height="10px" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
                <table style="width: 800px; height: 200px">
                    <tr>
                        <td colspan="2" style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                            border-top-color: gray; border-right-style: solid; border-left-style: solid;
                            border-right-color: gray; border-bottom-style: solid">
                            <span style="font-size: 16pt"><strong>
                            即時產出資訊</strong></span></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                            border-top-color: gray; border-right-style: solid; border-left-style: solid;
                            border-right-color: gray; border-bottom-style: solid">
                            <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#CC9966"
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="GridView2_RowDataBound" AutoGenerateColumns="False" Height="400px" Width="600px" EmptyDataText="No Record!!!">
                                <Columns>
                                                            <asp:TemplateField HeaderText="RN"></asp:TemplateField>
                                                            <asp:TemplateField HeaderText="cut_time">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcut_time" runat="server" ForeColor="#000000" Text='<%# Bind("cut_time") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblcut_time" runat="server" ForeColor="#000000" Text='<%# Bind("cut_time") %>'></asp:Label></br>
                                                                    <%-- SN :</br> 
<asp:Label ID="lblSN" runat="server" ForeColor="Red" Text='<%# Bind("sn") %>'></asp:Label></br>--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="shop">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblshop" runat="server" ForeColor="#000000" Text='<%# Bind("shop") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblshop" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("shop") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                            <asp:TemplateField HeaderText="product">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblproduct" runat="server" ForeColor="#000000" Text='<%# Bind("product") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblproduct" runat="server" ForeColor="#000000" Width="250px"
                                                                        Text='<%# Bind("product") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="月目標">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl月目標" runat="server" ForeColor="#000000" Text='<%# Bind("月目標") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl月目標" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("月目標") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        
                                                            <asp:TemplateField HeaderText="日目標">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl日目標" runat="server" ForeColor="#000000" Text='<%# Bind("日目標") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl日目標" runat="server" ForeColor="#000000" Width="250px"
                                                                        Text='<%# Bind("日目標") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="日實積_P">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl日實積_P" runat="server" ForeColor="#000000" Text='<%# Bind("日實積_P") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl日實積_P" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("日實積_P") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="日實積_E">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl日實積_E" runat="server" ForeColor="#000000" Text='<%# Bind("日實積_E") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl日實積_E" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("日實積_E") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="累積計畫">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl累積計畫" runat="server" ForeColor="#000000" Text='<%# Bind("累積計畫") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl累積計畫" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("累積計畫") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="累積實績_P">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl累積實績_P" runat="server" ForeColor="#000000" Text='<%# Bind("累積實績_P") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl累積實績_P" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("累積實績_P") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                             <asp:TemplateField HeaderText="累積實績_E">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl累積實績_E" runat="server" ForeColor="#000000" Text='<%# Bind("累積實績_E") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl累積實績_E" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("累積實績_E") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                             <asp:TemplateField HeaderText="G9">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblG9" runat="server" ForeColor="#000000" Text='<%# Bind("G9") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lblG9" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("G9") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="差異">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl差異" runat="server" ForeColor="#000000" Text='<%# Bind("差異") %>'></asp:Label></br>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="lbl差異" runat="server" ForeColor="#000000" Width="250px" Text='<%# Bind("差異") %>'></asp:TextBox></br>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                <RowStyle BackColor="White" ForeColor="#330099" />
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                <AlternatingRowStyle BackColor="Control" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                            border-top-color: gray; border-right-style: solid; border-left-style: solid;
                            border-right-color: gray; border-bottom-style: solid">
                            <strong><span style="font-size: 16pt">Hourly Move</span></strong></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                            border-top-color: gray; border-right-style: solid; border-left-style: solid;
                            border-right-color: gray; border-bottom-style: solid">
                            &nbsp;<table style="width: 393px; height: 37px">
                                <tr>
                                    <td style="border-left-color: gray; border-bottom-color: gray; width: 209px; border-top-style: solid;
                                        border-top-color: gray; border-right-style: solid; border-left-style: solid;
                                        text-align: left; border-right-color: gray; border-bottom-style: solid">
                                        <asp:ListBox ID="ListBox1" runat="server" Height="86px" Width="147px" SelectionMode="Multiple"></asp:ListBox></td>
                                    <td style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                                        border-top-color: gray; border-right-style: solid; border-left-style: solid;
                                        text-align: left; border-right-color: gray; border-bottom-style: solid; width: 35px;">
                                        <asp:Button ID="Button11" runat="server" Text=">>" Width="24px" OnClick="Button11_Click"/><br />
                                        <asp:Button ID="Button12" runat="server" Text=">" Width="24px" OnClick="Button12_Click"/><br />
                                        <asp:Button ID="Button13" runat="server" Text="<" Width="24px" OnClick="Button13_Click"/><br />
                                        <asp:Button ID="Button14" runat="server" Text="<<" Width="24px" OnClick="Button14_Click"/></td>
                                    <td style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                                        border-top-color: gray; border-right-style: solid; border-left-style: solid;
                                        text-align: left; border-right-color: gray; border-bottom-style: solid">
                                        <asp:ListBox ID="ListBox2" runat="server" Height="86px" Width="147px" SelectionMode="Multiple"></asp:ListBox></td>
                                </tr>
                                <tr>
                                    <td style="border-left-color: gray; border-bottom-color: gray; width: 209px; border-top-style: solid;
                                        border-top-color: gray; border-right-style: solid; border-left-style: solid;
                                        height: 23px; text-align: left; border-right-color: gray; border-bottom-style: solid">
                                        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Submit" />
                                        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="ExportToExcel" Width="96px" /></td>
                                    <td style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                                        border-top-color: gray; border-right-style: solid; border-left-style: solid;
                                        height: 23px; text-align: left; border-right-color: gray; border-bottom-style: solid; width: 35px;">
                                    </td>
                                    <td style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                                        border-top-color: gray; border-right-style: solid; border-left-style: solid;
                                        height: 23px; text-align: left; border-right-color: gray; border-bottom-style: solid">
                                    </td>
                                </tr>
                            </table>
                        
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-left-color: gray; border-bottom-color: gray; border-top-style: solid;
                            border-top-color: gray; border-right-style: solid; border-left-style: solid;
                            border-right-color: gray; border-bottom-style: solid">
                            <asp:GridView ID="GridView3" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" EmptyDataText="No Record!!!">
                                <RowStyle BackColor="#F7F7DE" />
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br /> 
 
</fieldset> 
</div>
    </form>
</body>
</html>
