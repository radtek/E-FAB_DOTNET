<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucMultiSelectListBox.ascx.cs"
    Inherits="UserControls_MultiSelectListBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate>
    <div>
    <asp:TextBox ID="selectedTxt" runat="server"></asp:TextBox>
        <asp:ImageButton ID="ModalPopOpenImg" runat="server" ImageUrl="~/Fabinfo/AOE/Images/Edit.gif" EnableTheming="True" /></div>


<asp:Panel ID="Panel1" runat="server" Style="display: none" CssClass="modalPopup">
    <asp:UpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                <td colspan="3" align="center" style="height: 19px">
                    <asp:Label ID="SelectorTxt" runat="server" Font-Bold="True" Font-Names="Arial Black" Font-Size="Medium" ForeColor="DarkSeaGreen">New</asp:Label></td>
                </tr>
                <tr>
                    <td style="padding: 0px;">
                        <asp:ListBox ID="lbxSource" runat="server" Rows="10" Width="186px" SelectionMode="Multiple">
                        </asp:ListBox>
                    </td>
                    <td style="padding: 0px;">
                        <asp:ImageButton ID="btnAllToRight" runat="server" ImageUrl="~/Fabinfo/AOE/Images/right2.gif" CommandName="AllToRight"
                            OnCommand="listBoxOperate_OnCommand" /><br />
                        <asp:ImageButton ID="btnToRight" runat="server" ImageUrl="~/Fabinfo/AOE/Images/right.gif" CommandName="ToRight"
                            OnCommand="listBoxOperate_OnCommand" /><br />
                        <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Fabinfo/AOE/Images/left.gif" CommandName="ToLeft"
                            OnCommand="listBoxOperate_OnCommand" /><br />
                        <asp:ImageButton ID="btnAllDel" runat="server" ImageUrl="~/Fabinfo/AOE/Images/left2.gif" CommandName="AllToLeft"
                            OnCommand="listBoxOperate_OnCommand" /></td>
                    <td style="padding: 0px;">
                        <asp:ListBox ID="lbxTo" runat="server" Rows="10" Width="186px" SelectionMode="Multiple">
                        </asp:ListBox>
                    </td>
                    <td style="padding: 0px;">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Fabinfo/AOE/Images/up2.gif" CommandName="ToTop"
                            OnCommand="listBoxOperate_OnCommand" /><br />
                        <asp:ImageButton ID="btnUp" runat="server" ImageUrl="~/Fabinfo/AOE/Images/up.gif" CommandName="ToUp"
                            OnCommand="listBoxOperate_OnCommand" /><br />
                        <asp:ImageButton ID="btnDown" runat="server" ImageUrl="~/Fabinfo/AOE/Images/down.gif" CommandName="ToDown"
                            OnCommand="listBoxOperate_OnCommand" /><br />
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Fabinfo/AOE/Images/down2.gif" CommandName="ToBottom"
                            OnCommand="listBoxOperate_OnCommand" />
                        </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="OkButton" runat="server" ImageUrl="~/Fabinfo/AOE/Images/save.gif" OnClick="OkButton_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="true" PopupControlID="Panel1" EnableViewState="false" TargetControlID="ModalPopOpenImg">
    </ajaxToolkit:ModalPopupExtender>
           </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="OkButton" EventName="Click" />
    </Triggers>
    </asp:UpdatePanel>
    

