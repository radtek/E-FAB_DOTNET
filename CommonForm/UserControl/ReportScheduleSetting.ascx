<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportScheduleSetting.ascx.cs" Inherits="CommonForm_UserControl_ReportScheduleSetting" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="Innolux" Namespace="Innolux.Portal.WebControls" %>
<%@ Register Src="~/CommonForm/UserControl/Scheduler/HourlySetting.ascx" TagPrefix="innolux" TagName="HourlySetting" %>
<%@ Register Src="~/CommonForm/UserControl/Scheduler/DailySetting.ascx" TagPrefix="innolux" TagName="DailySetting" %>
<%@ Register Src="~/CommonForm/UserControl/Scheduler/WeeklySetting.ascx" TagPrefix="innolux" TagName="WeeklySetting" %>
<%@ Register Src="~/CommonForm/UserControl/Scheduler/MonthlySetting.ascx" TagPrefix="innolux" TagName="MonthlySetting" %>
<%@ Register Src="~/CommonForm/UserControl/Scheduler/MinutelySetting.ascx" TagPrefix="innolux" TagName="MinutelySetting" %>
<%@ Import Namespace="Innolux.Portal.Common.Report.Scheduler" %>

<%--<asp:UpdatePanel runat="server" ID="upOpenBtn" UpdateMode="Conditional" RenderMode="Inline">
	<ContentTemplate>--%>
		<asp:Button ID="btnOpenModalPanel" runat="server" Text="Schedule" OnClick="btnOpenModalPanel_Click" />
		<asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
	<%--</ContentTemplate>
</asp:UpdatePanel>--%>

<asp:Button runat="server" ID="hiddenTargetControlForModalPopup" style="display:none"/>
    
<Innolux:ModalPanel
    ID="modalPanel" 
    runat="server" 
    Width="450px" 
    Title="Schedule setting" 
    ActivateControlId="hiddenTargetControlForModalPopup" 
    CloseImageUrl="~/Images/cancel.gif" 
    HeaderCss="SecurityDefaultTitle" 
    CssClass="modalPopup">

    <telerik:RadTabStrip ID="triggerTypeTabStrip" runat="server" Skin="Office2007" SelectedIndex="2" MultiPageID="TimeTypeMultiPage" CausesValidation="false">
        <Tabs>
			<telerik:RadTab runat="server" Text="Minutely"></telerik:RadTab> 
            <telerik:RadTab runat="server" Text="Hourly"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Daily" Selected="True"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Weekly"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Monthly"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="TimeTypeMultiPage" runat="server" SelectedIndex="2">
		<telerik:RadPageView ID="MinutelyPageView" runat="server">
            <innolux:MinutelySetting runat="server" ID="minutelySetting" />
        </telerik:RadPageView> 
        <telerik:RadPageView ID="HourlyPageView" runat="server">
            <innolux:HourlySetting runat="server" ID="hourlySetting" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="DailyPageView" runat="server">
            <innolux:DailySetting runat="server" ID="dailySetting" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="WeeklyPageView" runat="server">
            <innolux:WeeklySetting runat="server" ID="weeklySetting" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="MonthlyPageView" runat="server">
            <innolux:MonthlySetting runat="server" ID="monthlySetting" />
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2007" MultiPageID="RadMultiPage1" CausesValidation="false">
        <Tabs>
            <telerik:RadTab runat="server" Text="Other Setting" Selected="True"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Parameters"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Trigger List"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
       <telerik:RadPageView ID="RadPageView1" runat="server">
            <table cellspacing="1" cellpadding="1" width="100%" style="padding:0;background-color:#8DB2E3;">
                <tr>
                    <td style="background-color:#E6F3FF; width:30%; font-weight:bold;">Past</td>
                    <td style="background-color:#fff; vertical-align:bottom;">
                        <asp:TextBox ID="txtPastUnit" runat="server" Text="3" Width="50" />
                        <asp:DropDownList ID="ddlPastType" runat="server">
                            <asp:ListItem Text="Hours" Value="0" />
                            <asp:ListItem Text="Days" Value="1" Selected="True" />
                            <asp:ListItem Text="Weeks" Value="2" />
                            <asp:ListItem Text="Months" Value="3" />
                        </asp:DropDownList>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPastUnit" FilterType="Numbers" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color:#E6F3FF; width:30%; font-weight:bold;">Output Availability</td>
                    <td style="background-color:#fff; vertical-align:bottom;">
                        <asp:RadioButtonList ID="rbtnOutputAvail" runat="server" RepeatDirection="horizontal">
                            <asp:ListItem Text="Private" Value="0" Selected="true" />
                            <asp:ListItem Text="Public" Value="1" />
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color:#E6F3FF; width:30%; font-weight:bold;">Need Monitor</td>
                    <td style="background-color:#fff; vertical-align:bottom;">
						<asp:CheckBox ID="ckbMonitor" runat="server" />
                    </td>
                </tr> 
               <tr style="display:none">
					<td style="background-color:#E6F3FF; width:30%; font-weight:bold;">
						<asp:CheckBox ID="ckbCheckTrigger" runat="server" Text="Check Trigger" TextAlign="Left" />
					</td>
                    <td style="background-color:#fff; vertical-align:bottom;">
						<asp:DropDownList ID="ddlCheckTriggerID" runat="server" Width="250">
						</asp:DropDownList>
                    </td>
               </tr> 
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView2" runat="server">
            <asp:UpdatePanel ID="updd" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnOpenModalPanel" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ibtnSave" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <telerik:RadGrid ID="radGridParameters" runat="server" Skin="Office2007" AutoGenerateColumns="False" EnableViewState="False" OnNeedDataSource="RadGridParameters_NeedDataSource" OnInsertCommand="RadGridParameters_InsertCommand" AllowPaging="True" PageSize="5" OnDeleteCommand="RadGridParameters_DeleteCommand" >
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <MasterTableView CommandItemDisplay="Bottom" NoMasterRecordsText="None parameter">
                            <CommandItemTemplate>
                                <asp:ImageButton ID="btnInsertParam" runat="server" CommandName="InitInsert" ImageUrl="~/Images/Insert.gif"  Visible='<%# !radGridParameters.MasterTableView.IsItemInserted %>' />
                                <asp:ImageButton ID="btnDeleteParam" runat="server" CommandName="DeleteSelected" ImageUrl="~/Images/Delete.gif" OnClientClick="javascript:return confirm('Delete selected parameter?')"/>
                            </CommandItemTemplate>
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Key" DataField="Key" UniqueName="Text">
                                    <HeaderStyle Width="35%" />
                                    <ItemStyle Width="35%" Wrap="True" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Value" DataField="Value" UniqueName="Value">
                                    <ItemStyle Wrap="True" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <HeaderContextMenu EnableTheming="True" Skin="Office2007">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <FilterMenu EnableTheming="True" Skin="Office2007">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                    </telerik:RadGrid>
                </ContentTemplate>
            </asp:UpdatePanel>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView3" runat="server">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnOpenModalPanel" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ibtnSave" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <telerik:RadGrid ID="RadGrid2" runat="server" Skin="Office2007" AutoGenerateColumns="false" EnableViewState="true" 
                        OnItemDataBound="RadGrid2_ItemDataBound" OnItemCommand="RadGrid2_ItemCommand" 
                        OnNeedDataSource="RadGrid2_NeedDataSource" AllowPaging="True" PageSize="5">
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <MasterTableView NoMasterRecordsText="No schedule setted">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                    <HeaderTemplate>
                                        <asp:ImageButton 
                                            ID="btnRefresh" 
                                            runat="server" 
                                            ImageUrl="~/Images/Refresh.gif" 
                                            CommandName="refresh" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton 
                                            ID="ImageButton1" 
                                            runat="server" 
                                            ImageUrl="~/Images/Delete.gif" 
                                            CommandArgument='<%# Eval("Name")+"$"+Eval("Group")%>' 
                                            CommandName="delete" 
                                            OnClientClick="javascript:return confirm('Are you sure?')" 
                                            ToolTip='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="10px" />
                                    <ItemStyle Width="10px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn1">
                                    <ItemTemplate>
                                        <asp:LinkButton 
                                            ID="LinkButton1" 
                                            runat="server" 
                                            CommandName='<%# Eval("State") %>' 
                                            CommandArgument='<%# Eval("Name")+"$"+Eval("Group") %>'  >
                                            <asp:Image 
                                                ID="Image1" 
                                                runat="server" 
                                                ImageAlign="Middle" 
                                                ImageUrl='<%# ((Quartz.TriggerState)Eval("State")) == Quartz.TriggerState.Normal ? "~/Images/pause.gif" : "~/Images/play.gif" %>' />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10px" />
                                    <ItemStyle Width="10px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn2">
                                    <ItemTemplate>
                                        <asp:ImageButton 
                                            ID="btnExecute" 
                                            runat="server" 
                                            ImageUrl="~/Images/update.gif" 
                                            CommandArgument='<%# Eval("Name")+"$"+Eval("Group")%>' 
                                            CommandName="execute"
                                            OnClientClick="javascript:return confirm('Are you sure execute it immediately?')"   />
                                    </ItemTemplate>
                                    <HeaderStyle Width="10px" />
                                    <ItemStyle Width="10px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="UserId" DataField="UserId" UniqueName="UserId" />
                                <telerik:GridBoundColumn HeaderText="Frequency" DataField="Frequency" UniqueName="Frequency" />
                                <telerik:GridBoundColumn HeaderText="State" DataField="State" UniqueName="State" />
                                <telerik:GridDateTimeColumn DataField="NextFireDateTime" HeaderText="NextFireTime" UniqueName="NextFireTime" DataType="System.DateTime" />
                                <telerik:GridBoundColumn HeaderText="ID" DataField="Name" UniqueName="Name" Visible="False" />
                            </Columns>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                        </MasterTableView>
                        <HeaderContextMenu EnableTheming="True" Skin="Office2007">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <FilterMenu EnableTheming="True" Skin="Office2007">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                    </telerik:RadGrid>
                </ContentTemplate>
            </asp:UpdatePanel>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
        <ContentTemplate>
            <div style="text-align:right; padding:4px 5px 2px 5px;">
                <asp:ImageButton ID="ibtnSave" runat="server" ImageUrl="~/Images/save.gif" ValidationGroup="setSchedule" OnClick="btnOk_Click" />
            </div>    
        </ContentTemplate>
    </asp:UpdatePanel>
</Innolux:ModalPanel>


        
