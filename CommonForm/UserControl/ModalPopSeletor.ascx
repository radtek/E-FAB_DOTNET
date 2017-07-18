<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalPopSeletor.ascx.cs" Inherits="Fabinfo_CF_Report_CF_Defect_Trend_ModalPopSeletor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="Innolux" Namespace="Innolux.Portal.WebControls" %>
<%@ Register TagPrefix="fluent" Namespace="Fluent" Assembly="Fluent.ListTransfer" %>

<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" RenderMode="inline">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="OkButton" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <telerik:RadTextBox runat="server" ID="TextBox1" Width="150" ReadOnly="true" Skin="Office2007"></telerik:RadTextBox>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:Image ID="ModalPopOpenImg" runat="server" ImageUrl="~/Images/Edit.gif" ImageAlign="Middle" Style="cursor:pointer" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="TextBox1" >
</asp:RequiredFieldValidator>
<asp:Panel ID="popupPanel" runat="server" CssClass="modalPopup" Style="display: none; text-align:center;" Width="350px" >
	<div class="SecurityDefaultTitle">
		<div style="float:left; padding:2px; font-size:10pt;"></div>
		<asp:Image ID="ModalPopCloseImg" runat="server" ImageUrl="~/Images/Cancel.gif" Style="float:right; padding:2px; cursor:pointer;" />
	</div>
    
    <table width="100%">
            <tr>
                <td colspan="3"><asp:Label ID="lblGroup" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" runat="server" id="tdDropdown">
                    <asp:DropDownList ID="ddlGroup" runat="server" Width="100%" 
                        AppendDataBoundItems="true" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                        <asp:ListItem Text="-select-" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="1">
                    <%--<asp:Button ID="Button1" runat="server" Text="Add" CausesValidation="false" />--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlGroup" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:ListBox ID="lsbLeft" runat="server" Rows="4" Width="150" SelectionMode="Multiple">
                            </asp:ListBox>
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server"
                                TargetControlID="lsbLeft" IsSorted="false" PromptPosition="Top">
                            </ajaxToolkit:ListSearchExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
					<%----%><fluent:ListTransfer Runat="server" ID="ListTransfer" EnableClientSide="True" ListControlTo="lsbRight"  ListControlFrom="lsbLeft" /> 
					<a href="#" onclick="<%= ListTransfer.ClientMoveSelected %>" ><img border="0" src="../../../images/right.gif"></a><br />
					<a href="#" onclick="<%= ListTransfer.ClientMoveBackSelected %>"><img border="0" src="../../../images/left.gif"></a><br />
					<a href="#" onclick="<%= ListTransfer.ClientMoveAll %>"><img border="0" src="../../../images/rightAll.gif"></a><br />
					<a href="#" onclick="<%= ListTransfer.ClientMoveBackAll %>"><img border="0" src="../../../images/leftAll.gif"></a>
                    <%--<asp:Button ID="btnAddAll" runat="server" Text=">>" OnClick="btnAddAll_Click" CausesValidation="false" /><br />
                    <asp:Button ID="btnToRight" runat="server" Text="-->" OnClick="btnToRight_Click" CausesValidation="false" /><br />
                    <asp:Button ID="btnToLeft" runat="server" Text="<--" OnClick="btnToLeft_Click" CausesValidation="false" /><br />
                    <asp:Button ID="btnClearAll" runat="server" Text="<<" OnClick="btnClearAll_Click" CausesValidation="false" />--%>
                </td>
                <td>
                            <asp:ListBox ID="lsbRight" runat="server" Rows="4" Width="150" SelectionMode="single">
                            </asp:ListBox></td>
            </tr>
            <tr>
                <td colspan="3" style="text-align:right">
                    <asp:ImageButton 
                        ID="OkButton" 
                        runat="server" 
                        ImageUrl="~/Images/save.gif" 
                        CausesValidation="false" 
                        OnClick="OkButton_Click" />
                   
                </td>
            </tr>
        </table>
    
</asp:Panel>

<ajaxToolkit:ModalPopupExtender 
	ID="ModalPopupExtender" runat="server" 
    TargetControlID="ModalPopOpenImg"
    PopupControlID="popupPanel" 
    CancelControlID="ModalPopCloseImg"/>

