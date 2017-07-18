<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProjectGroupSelector.ascx.cs"
    Inherits="common_form_user_control_ProjectGroupSelector" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table>
    <tr>
        <td>
            <asp:Label runat="server" CssClass="pageLabel" ID="lblProjectID"></asp:Label></td>
        <td>
            <asp:Label runat="server" CssClass="pageLabel" ID="lblProjectGroup"></asp:Label>
            <asp:Image ID="ModalPopOpenImg" runat="server" ImageUrl="~/Images/Edit.gif" ImageAlign="Middle"
                Style="cursor: pointer" />
        </td>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="200px" Width="550px" BackColor="WhiteSmoke"
    Style="display: none" BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px"
    HorizontalAlign="Center">
    <span style="color: #ffffff"></span>
    <table style="width: 100%">
        <tr>
            <td style="font-weight: bold; color: white; background-color: gray">
                <table width="100%">
                    <tr>
                        <td align="left" style="font-size: 15px">
                            Project Group</td>
                        <td align="right">
                            <asp:ImageButton ImageUrl="~/images/cancel.gif" runat="server" ID="imgCancel" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table cellspacing="10" bordercolordark="#ffffff" cellpadding="5" width="95%" bordercolorlight="#7a9cb7"
        border="0">
        <tr>
            <td class="pageTD2" align="center" style="width:30%;" >
                選擇 Project Group
            </td>
            <td align="left">
                <asp:DropDownList runat="server" ID="ddlProjectGroup" >
                </asp:DropDownList>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return check_Project_field();" />
            </td>
        </tr>
        <tr>
            <td class="pageTD2" align="center" style="width:30%;">
                 新增 Project Group
                
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtProjectGroup" runat="server" Width="85%" MaxLength="1000"></asp:TextBox>
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClientClick="return check_Project_field();" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="ModalPopOpenImg"
    PopupControlID="Panel1" CancelControlID="imgCancel">
</cc1:ModalPopupExtender>
