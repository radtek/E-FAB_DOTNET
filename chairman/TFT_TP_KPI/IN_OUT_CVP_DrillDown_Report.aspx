<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IN_OUT_CVP_DrillDown_Report.aspx.cs"
    Inherits="TFT_TP_KPI_IN_OUT_CVP_DrillDown_Report" %>

<%@ Register Src="~/CommonForm/UserControl/UpdateProgress.ascx" TagPrefix="innolux"
    TagName="UpdateProgress" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="Innolux" Namespace="Innolux.Portal.WebControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IN_OUT_CVP_DrillDown_Report</title>

    <script type="text/javascript">
		document.write('<div id="loadDiv" style="padding-top:150; padding-left:150; font-size:13pt;">'+ '頁面正在載入,請等待......</div>');   
		function   window.onload()   
		{   
			hiddenDiv.style.display=""; 
			loadDiv.removeNode(true); 
		}        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Innolux:updateprogress runat="server" id="updateProgress" />
        <div id="hiddenDiv">
            <%--style="display: none"--%>
            <div>
                <%--<div class="SecurityDefaultTitle">
                    <span style="font-size: 10pt; font-family: Arial">IN_OUT_CVP_DrillDown_Report</span>
                </div>--%>
                <asp:UpdatePanel runat="server" ID="UpdatePanel01">
                    <ContentTemplate>
                        <asp:Table ID="Table1" runat="server" Visible="True" />                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
