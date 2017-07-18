<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderMenuBar.ascx.cs" Inherits="UserControls_HeaderMenuBar" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadMenu 
    ID="RadMenu1" 
    runat="server" 
    Flow="Horizontal" 
    Skin="Hay"   
    EnableEmbeddedSkins="true" 
    ClickToOpen="false" 
    Width="99.9%">
    <CollapseAnimation Duration="200" Type="OutQuint" />
</telerik:RadMenu>