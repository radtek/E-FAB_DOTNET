<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UpdateProgress.ascx.cs" Inherits="CommonForm_UserControl_UpdateProgress" %>
<%@ Register TagPrefix="Innolux" Namespace="Innolux.Portal.WebControls" %>
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
        <div id="IMGDIV" style="position:absolute; left:85%; top:0; visibility:visible; vertical-align:middle; border:solid 2px black; background-color:White; z-index:99999; vertical-align:middle; text-align:center; padding:10px;">
            <asp:Image ID="Image1" runat="server" ImageAlign="AbsMiddle" Width="16" Height="16" ImageUrl="~/Images/indicator_blue.gif" />
            <strong style="color:Red;">Loading...</strong>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>