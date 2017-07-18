<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DefectCodeSelector.ascx.cs" Inherits="CommonForm_UserControl_DefectCodeSelector" %>
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
<asp:Image ID="DefOpenImg" runat="server" ImageUrl="~/Images/Edit.gif" ImageAlign="Middle" Style="cursor:pointer" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="TextBox1" >
</asp:RequiredFieldValidator>

<asp:Panel ID="popupPanel" runat="server" CssClass="modalPopup" Style="display: none; text-align:center;" Width="350px" >
	<div class="SecurityDefaultTitle">
		<div style="float:left; padding:2px; font-size:10pt;">Defect Code Selector</div>
		<asp:Image ID="DefCloseImg" runat="server" ImageUrl="~/Images/Cancel.gif" Style="float:right; padding:2px; cursor:pointer;" />
	</div>

    <table style="width:100%">
        <tr>
            <td colspan="2">
                Defect Code Group
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtSearch" runat="server" Width="77px"></asp:TextBox>
                <asp:ImageButton ID="btnSearch" runat="server" ImageAlign="Top" ImageUrl="~/Images/find.gif" ValidationGroup="Search" OnClick="btnSearch_Click" />
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="*" ControlToValidate="txtSearch" ValidationGroup="Search">
                </asp:RequiredFieldValidator>
            </td>
            <td>Chosen</td>
        </tr>
        <tr>
            <td rowspan="3">
                <asp:ListBox ID="lsbDefGroup" runat="server" Rows="7" Width="80px" SelectionMode="multiple"></asp:ListBox></td>
            <td rowspan="3">
                <asp:Button ID="btnFilter" runat="server" Text="-->" CausesValidation="False" OnClick="btnFilter_Click" /><br />
                <asp:Button ID="btnAll" runat="server" Text="All" CausesValidation="False" OnClick="btnAll_Click" />
            </td>
            <td rowspan="3">
				<asp:UpdatePanel runat="server" ID="up1" UpdateMode="conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnAll" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
						<asp:ListBox ID="lsbAvaliableDefCode" runat="server" Rows="7" Width="80" SelectionMode="multiple"></asp:ListBox>
					</ContentTemplate>
                </asp:UpdatePanel>                                      
            </td>
            <td></td>
            <td rowspan="3">
				<asp:ListBox ID="lsbChosenDefCode" runat="server" Rows="7" Width="80px" SelectionMode="multiple"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td>
				<fluent:ListTransfer 
					Runat="server" 
					ID="ListTransfer1" 
					EnableClientSide="True" 
					ListControlTo="lsbChosenDefCode"  
					ListControlFrom="lsbAvaliableDefCode" />
				<input type="button" onclick="<%= ListTransfer1.ClientMoveSelected %>" value="-->" /><br />
				<input type="button" onclick="<%= ListTransfer1.ClientMoveAll %>" value=">>" />
            </td>
        </tr>
        <tr>
            <td>
				<input type="button" onclick="<%= ListTransfer1.ClientMoveBackSelected %>" value="<--" /><br />
				<input type="button" onclick="<%= ListTransfer1.ClientMoveBackAll %>" value="<<" />
            </td>
        </tr>
        <tr>
            <td colspan="5" style="text-align: right;">
                <asp:ImageButton ID="OkButton" runat="server" ImageUrl="~/Images/save.gif" CausesValidation="false" OnClick="OkButton_Click" />
            </td>
        </tr>
    </table>

</asp:Panel>

<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" 
    TargetControlID="DefOpenImg"
    PopupControlID="popupPanel" 
    CancelControlID="DefCloseImg"/>