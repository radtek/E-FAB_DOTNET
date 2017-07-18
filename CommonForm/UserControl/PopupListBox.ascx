<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopupListBox.ascx.cs" Inherits="CommonForm_UserControl_PopupListBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI"
    Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script  type="text/javascript" language ="javascript">
    // Todo: 
    function CallBackFunction2_<%=txtSelect.ClientID %>(radWindow, returnValue)
    {
        var oArea = document.getElementById("<%= this.txtSelect.ClientID %>");
        var objSelected = document.getElementById("<%= this.HiddenField1.ClientID %>");
        
        oArea.value = objSelected.value = "";
        if (returnValue) {
//            alert(returnValue);
            oArea.value = objSelected.value = returnValue;
        }  
    }
    
    function changeTooltips_<%= txtSelect.ClientID %>(){
//        var obj = document.getElementById("<%= txtSelect.ClientID %>");
//        alert(obj.value + "- " + obj.title);
        var tooltips = document.getElementById("<%= txtSelect.ClientID %>").value;  
        document.getElementById("<%= txtSelect.ClientID %>").title = tooltips;
        document.getElementById("<%= ImageButton1.ClientID %>").title =  tooltips;
    }
    
</script>    


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
 
<table>
    <tr>
        <td style="width: 100px; height: 22px;">
            <asp:TextBox ID="txtSelect" runat="server" Width="150px" Enabled="false"></asp:TextBox></td>
        <td style="width: 20px; height: 22px;">
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/AddRecord.gif" 
                                                              Height="21px" 
                                                              Width="17px"
                                                              
                                                              OnClick="ImageButton1_Click"/>
        </td>
        <td style="width: 61px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select  it!" ControlToValidate="txtSelect" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
    </tr>
</table>
<%--<asp:TextBox ID="PopContent" runat="server"></asp:TextBox>--%>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin ="Office2007">
</telerik:RadWindowManager> 
<%--        <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID ="txtSelect" 
                                                                      PopupControlID ="PopContent"  
                                                                      PopupPosition="Left"
                                                                      OffsetX="0"
                                                                      OffsetY="20">
        </cc1:HoverMenuExtender>--%>
    </ContentTemplate>
</asp:UpdatePanel> 
<asp:HiddenField ID="HiddenField1" runat="server" /> 