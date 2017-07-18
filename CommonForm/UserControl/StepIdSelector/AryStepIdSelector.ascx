<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AryStepIdSelector.ascx.cs" Inherits="CommonForm_UserControl_AryStepIdSelector" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="Innolux" Namespace="Innolux.Portal.WebControls" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" RenderMode="inline">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="OkButton" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <telerik:RadTextBox runat="server" ID="TextBox1" Width="150" ReadOnly="true" Skin="Office2007"></telerik:RadTextBox>
        <%--<asp:TextBox ID="TextBox1" runat="server" Width="150" ReadOnly="True"></asp:TextBox>--%>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Edit.gif" ImageAlign="Middle" Style="cursor:pointer" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="TextBox1" >
</asp:RequiredFieldValidator>

<Innolux:ModalPanel
    ID="modalPanel" 
    runat="server" 
    Width="250px" 
    ActivateControlId="Image1" 
    CloseImageUrl="~/Images/cancel.gif" 
    BackgroundCss="modalBackground" 
    HeaderCss="SecurityDefaultTitle" 
    CssClass="modalPopup" 
    Title="Ary Setp ID">
    
    <table width="100%">
        <tr>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                Process Name:
                <asp:DropDownList ID="ddlGroup" runat="server" Width="100" AutoPostBack="true" 
                    CausesValidation="false" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                    <asp:ListItem Text="NG" Value="0"></asp:ListItem>
                    <asp:ListItem Text="LR+NG" Value="1"></asp:ListItem>
                </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:ListBox ID="lsbLeft" runat="server" Rows="6" Width="100" SelectionMode="Multiple">
                        </asp:ListBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <%--<asp:Button ID="btnAddAll" runat="server" Text=">>" OnClick="btnAddAll_Click" CausesValidation="false" /><br />--%>
                <asp:Button ID="btnToRight" runat="server" Text="-->" OnClick="btnToRight_Click" CausesValidation="false" /><br />
                <asp:Button ID="btnToLeft" runat="server" Text="<--" OnClick="btnToLeft_Click" CausesValidation="false" /><br />
               <%-- <asp:Button ID="btnClearAll" runat="server" Text="<<" OnClick="btnClearAll_Click" CausesValidation="false" />--%>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGroup" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnToRight" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnToLeft" EventName="Click" />
                        <%--<asp:AsyncPostBackTrigger ControlID="btnClearAll" EventName="Click" />--%>
                        <%--<asp:AsyncPostBackTrigger ControlID="btnAddAll" EventName="Click" />--%>
                    </Triggers>
                    <ContentTemplate>
                        <asp:ListBox ID="lsbRight" runat="server" Rows="6" Width="100" SelectionMode="Multiple">
                        </asp:ListBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align:right">
                <asp:ImageButton ID="OkButton" runat="server" ImageUrl="~/Images/save.gif" CausesValidation="false" OnClick="OkButton_Click" />
            </td>
        </tr>
    </table>
    
</Innolux:ModalPanel>