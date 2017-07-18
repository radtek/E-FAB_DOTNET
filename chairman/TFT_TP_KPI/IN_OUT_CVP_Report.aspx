<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IN_OUT_CVP_Report.aspx.cs"
    Inherits="TFT_TP_KPI_IN_OUT_CVP_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="Innolux" Namespace="Innolux.Portal.WebControls" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IN_OUT_CVP_Report</title>

    <script language="javascript" type="text/javascript">		
    function CVP_DrillDownClick(Shop)
    {
        //if( IndexName == "DAILY TARGET INPUT" || IndexName == "MPS INPUT" || IndexName == "MPS OUTPUT" )
        //{
        //    return;
        //}
        //radopen(Url,Name)
        //# Url. This supplies the URL for the content window. If this is given an argument of null, the NavigateUrl property set for the window on the server is used.
        //# Name. This is the ID of particular (existing) RadWindow object to show. If this is given an argument of null, the function creates a new window.
        var oWnd = radopen("IN_OUT_CVP_DrillDown_Report.aspx?shop=" + Shop , null );
        //oWnd.setSize(750,500);           
        //oWnd.center();
        oWnd.Maximize();
    }
    
    function OPENTEST()
    {
        var url = "http://www.google.com.tw";
        var oWnd = radopen(url, "RadWindow1");
        oWnd.Maximize();
    }          
    
    
    function DrillDownClick(Shop)
    {           
        //radopen(Url,Name)
        //# Url. This supplies the URL for the content window. If this is given an argument of null, the NavigateUrl property set for the window on the server is used.
        //# Name. This is the ID of particular (existing) RadWindow object to show. If this is given an argument of null, the function creates a new window.
        var oWnd = radopen("./IN_OUT_CVP_DrillDown_Report.aspx?shop=" + Shop , null );
        //oWnd.setSize(750,500);           
        //oWnd.center();
        oWnd.Maximize();
    }        
            
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Office2007">
            </telerik:RadWindowManager>
            <fieldset style="padding: 0; width: 80%; direction: ltr">
                <legend>IN/OUT CVP REPORT</legend>
                <Innolux:GroupHeaderRadGrid runat="server" ID="RG_TFT" EnableViewState="False" AutoHeaderGroupBySplit="True"
                    HeaderTextSplitChar="$" AutoSameMergeRow="True" MergeColumnIndexes="1,2" GridLines="None"
                    AutoGenerateColumns="True" Skin="Office2007" OnItemDataBound="RG_TFT_ItemDataBound">
                    <HeaderContextMenu EnableTheming="True" Skin="Office2007">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </HeaderContextMenu>
                    <MasterTableView EnableViewState="False">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                    </MasterTableView>
                    <FilterMenu EnableTheming="True" Skin="Office2007">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </FilterMenu>
                    <ItemStyle HorizontalAlign="Center" />
                    <AlternatingItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </Innolux:GroupHeaderRadGrid>
                <hr />
                <Innolux:GroupHeaderRadGrid runat="server" ID="RG_SG" EnableViewState="False" AutoHeaderGroupBySplit="True"
                    HeaderTextSplitChar="$" AutoSameMergeRow="True" MergeColumnIndexes="1,2" GridLines="None"
                    AutoGenerateColumns="True" Skin="Office2007" OnItemDataBound="RG_SG_ItemDataBound">
                    <HeaderContextMenu EnableTheming="True" Skin="Office2007">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </HeaderContextMenu>
                    <MasterTableView EnableViewState="False">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                    </MasterTableView>
                    <FilterMenu EnableTheming="True" Skin="Office2007">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </FilterMenu>
                    <ItemStyle HorizontalAlign="Center" />
                    <AlternatingItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </Innolux:GroupHeaderRadGrid>
            </fieldset>
        </div>
    </form>
</body>
</html>
