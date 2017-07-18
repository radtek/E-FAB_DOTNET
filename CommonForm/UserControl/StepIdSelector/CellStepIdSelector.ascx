<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CellStepIdSelector.ascx.cs" Inherits="CommonForm_UserControl_CellStepIdSelector" %>

<asp:DropDownList ID="ddlStepName" runat="server" Width="150">
    <asp:ListItem Text="CT1(6605)" value="'6605'" Selected="true"></asp:ListItem>
    <asp:ListItem Text="CT1(6610)" value="'6610'"></asp:ListItem>
    <asp:ListItem Text="CT1" value="'6605','6610'"></asp:ListItem>
    <asp:ListItem Text="LR(CT1)" value="'6620'"></asp:ListItem>
    <asp:ListItem Text="CT3" value="'6630'"></asp:ListItem>
    <asp:ListItem Text="CT1+CT3" value="'6605','6610','6620','6630'"></asp:ListItem>
    <asp:ListItem Text="CT2" value="'6810'"></asp:ListItem>
    <asp:ListItem Text="LR(CT2)" value="'6820'"></asp:ListItem>
    <asp:ListItem Text="CT4" value="'6830'"></asp:ListItem>
    <asp:ListItem Text="CT2+CT4" value="'6810','6820','6830'"></asp:ListItem>
</asp:DropDownList>