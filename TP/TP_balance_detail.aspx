<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_balance_detail.aspx.cs"
    Inherits="TP_TP_balance_detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TP_balance_detail</title>
    <script type="text/javascript">
		document.write('<div id="loadDiv" style="padding-top:150; padding-left:150; font-size:13pt;">'+ '頁面正在載入,請等待......</div>');   
		function   window.onload()   
		{   
			hiddenDiv.style.display=""; 
			loadDiv.removeNode(true); 
		}  
    </script>
</head>
<body>
    <form id="hiddenDiv" runat="server">
        <div>
            <fieldset>
                <legend align="center" style="color: blue; text-align: center"><span style="font-size: 16pt">
                    <strong>Diff_detail</strong></span></legend>
                <table hight="100%" width="100%">
                    <tr>
                        <td align='center' valign='middle'>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EmptyDataText="No Record!"
                                Font-Names="Arial" Font-Size="12px" OnRowDataBound="GridView1_RowDataBound" Width="1000px">
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
                                    <asp:TemplateField HeaderText="lot">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllot_id" runat="server" ForeColor="Blue" Text='<%# Bind("lot_id") %>'></asp:Label></br>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="glass_id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblglass_id" runat="server" ForeColor="Blue" Text='<%# Bind("glass_id") %>'></asp:Label></br>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="input_product">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinput_product" runat="server" ForeColor="Blue" Text='<%# Bind("input_product") %>'></asp:Label></br>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Now_product">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprod_name" runat="server" ForeColor="Blue" Text='<%# Bind("prod_name") %>'></asp:Label></br>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="lot_type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllot_type" runat="server" ForeColor="Blue" Text='<%# Bind("lot_type") %>'></asp:Label></br>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="step_seq">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstep_seq" runat="server" ForeColor="Blue" Text='<%# Bind("step_seq") %>'></asp:Label></br>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="step_desc">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstep_desc" runat="server" ForeColor="Blue" Text='<%# Bind("step_desc") %>'></asp:Label></br>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="cst_id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcst_id" runat="server" ForeColor="Blue" Text='<%# Bind("cst_id") %>'></asp:Label></br>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="vendor_lot_id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblvendor_lot_id" runat="server" ForeColor="Blue" Text='<%# Bind("vendor_lot_id") %>'></asp:Label></br>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#DEDFDE" Font-Bold="True" ForeColor="Black" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
