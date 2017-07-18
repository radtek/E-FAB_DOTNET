<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CFTStepIdSelector.ascx.cs" Inherits="CommonForm_UserControl_CFTStepIdSelector" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="Innolux" Namespace="Innolux.Portal.WebControls" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="fluent" Namespace="Fluent" Assembly="Fluent.ListTransfer" %>

<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" RenderMode="inline">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="OkButton" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <telerik:RadTextBox runat="server" ID="TextBox1" Width="150" ReadOnly="true" Skin="Office2007"></telerik:RadTextBox>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:Image ID="CfStepOpenImg" runat="server" ImageUrl="~/Images/Edit.gif" ImageAlign="Middle" Style="cursor:pointer" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="TextBox1" >
</asp:RequiredFieldValidator>

<asp:Panel ID="popupPanel" runat="server" CssClass="modalPopup" Style="display: none; text-align:center;" Width="250px" >
	<div class="SecurityDefaultTitle">
		<div style="float:left; padding:2px; font-size:10pt;">CF Setp ID Selector</div>
		<asp:Image ID="CfStepCloseImg" runat="server" ImageUrl="~/Images/Cancel.gif" Style="float:right; padding:2px; cursor:pointer;" />
	</div>
	<table border="0" style="width:100%;">
		<tr>
            <td colspan="3" style="text-align:left; margin-left:5px;">
                Process Name:
                <asp:DropDownList ID="ddlGroup" runat="server" Width="80" AutoPostBack="true" 
                    CausesValidation="false" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                    <asp:ListItem Text="-select-" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="IF" Value="2510"></asp:ListItem>
                    <asp:ListItem Text="IS" Value="2910"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
		<tr>
			<td valign="top">
				<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
						<asp:ListBox ID="lsbLeft" Runat="server" SelectionMode="Multiple" Rows="8" Width="100" DataTextField="TEXT" DataValueField="TEXT">
						</asp:ListBox>
					</ContentTemplate>
                </asp:UpdatePanel>
			</td>
			<td valign="middle">
				
				<a href="#" onclick="<%= ListTransfer1.ClientMoveSelected %>" ><img border="0" src="../../../images/right.gif"></a><br /><br />
				<a href="#" onclick="<%= ListTransfer1.ClientMoveBackSelected %>"><img border="0" src="../../../images/left.gif"></a><br /><br />
				<a href="#" onclick="<%= ListTransfer1.ClientMoveAll %>"><img border="0" src="../../../images/rightAll.gif"></a><br /><br />
				<a href="#" onclick="<%= ListTransfer1.ClientMoveBackAll %>"><img border="0" src="../../../images/leftAll.gif"></a>
			</td>
			<td valign="top">
				<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
						<asp:ListBox ID="lsbRight" Runat="server"  SelectionMode="Multiple" Rows="8" Width="100">
						</asp:ListBox>
					</ContentTemplate>
                </asp:UpdatePanel>
                <fluent:ListTransfer Runat="server" ID="ListTransfer1" EnableClientSide="True" ListControlTo="lsbRight"  ListControlFrom="lsbLeft" /> 
			</td>
		</tr>
		<tr>
            <td colspan="3" style="text-align:right">
                <asp:ImageButton ID="OkButton" runat="server" ImageUrl="~/Images/save.gif" CausesValidation="false" OnClick="OkButton_Click" />
            </td>
        </tr>
	</table> 

</asp:Panel>

<ajaxToolkit:ModalPopupExtender 
	ID="ModalPopupExtender" runat="server" 
    TargetControlID="CfStepOpenImg"
    PopupControlID="popupPanel" 
    CancelControlID="CfStepCloseImg"/>
