<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductSelector.ascx.cs" Inherits="CommonForm_UserControl_TVProductSelector" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="Innolux" Namespace="Innolux.Portal.WebControls" %>

<script type="text/javascript" language="javascript">
function UpdateAllChildren(nodes, checked)
{
   var i;
   for (i=0; i<nodes.get_count(); i++)
   {
       if (checked)
       {
		   if (nodes.getNode(i).get_level() != 3)
		  { 
           nodes.getNode(i).check();
          } 
       }
       else
       {
           nodes.getNode(i).set_checked(false);
       }
       
       if (nodes.getNode(i).get_nodes().get_count()> 0)
       {
           UpdateAllChildren(nodes.getNode(i).get_nodes(), checked);
       }
   }
}

function ClientNodeChecked(sender, eventArgs)
{
	var childNodes = eventArgs.get_node().get_nodes();
	var isChecked = eventArgs.get_node().get_checked();
	var level = eventArgs.get_node().get_level();
	var enableProd = "<%= this.ShowMainProd %>"
	if (level == 2)
	{
		if (!isChecked)
		{  
			UpdateAllChildren(childNodes, isChecked);
			if (enableProd == "False")
			{
				eventArgs.get_node().set_checked(true);
			}
		}
		else
		{
			if (enableProd == "False")
			{
				eventArgs.get_node().set_checked(false);
			}
		}
	}
	else
	{
		UpdateAllChildren(childNodes, isChecked);
	}
}
function validateTreeViewCheckboxes(source, args)
{
   var tree = $find("<%= RadTreeView1.ClientID %>");
   args.IsValid = tree.get_checkedNodes().length > 0;   
}


</script>
<table>
	
	<tr>
		<td>Size:</td>
		<td>
			<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false" RenderMode="Inline">
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
				</Triggers>
				<ContentTemplate>
					<asp:TextBox ID="txtSize" runat="server" ReadOnly="True"></asp:TextBox>
				</ContentTemplate>
			</asp:UpdatePanel>
		</td>
	</tr>
	<tr>
		<td>Model:</td>
		<td>
			<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false" RenderMode="Inline">
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
				</Triggers>
				<ContentTemplate>
					<asp:TextBox ID="txtModel" runat="server" ReadOnly="True"></asp:TextBox>
				</ContentTemplate>
			</asp:UpdatePanel>
		</td>
	</tr>
	<tr runat="server" id="trMainProd">
		<td>Product:</td>
		<td>
			<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false" RenderMode="Inline">
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
				</Triggers>
				<ContentTemplate>
					<asp:TextBox ID="txtProduct" runat="server" ReadOnly="True"></asp:TextBox>
				</ContentTemplate>
			</asp:UpdatePanel>
		</td>
	</tr>
	<tr runat="server" id="trSubProd">
		<td>SubProduct:</td>
		<td>
			<asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false" RenderMode="Inline">
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
				</Triggers>
				<ContentTemplate>
					<asp:TextBox ID="txtSubProduct" runat="server" ReadOnly="True"></asp:TextBox>
				</ContentTemplate>
			</asp:UpdatePanel>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<asp:Image ID="ModalPopOpenImg" runat="server" ImageUrl="~/Images/Edit.gif" ImageAlign="Middle" Style="cursor:pointer" />
				<asp:CustomValidator ID="CustomValidator1" runat="server"
	   ClientValidationFunction="validateTreeViewCheckboxes" EnableClientScript="True"
	   ErrorMessage="Please check at least one node"
	   OnServerValidate="CustomValidator1_ServerValidate">
	</asp:CustomValidator>
		</td>
	</tr>
</table>

<Innolux:ModalPanel
    ID="modalPanel" 
    runat="server" 
    Width="250px" 
    Title="Product Selector" 
    ActivateControlId="ModalPopOpenImg" 
    CloseImageUrl="~/Images/cancel.gif" 
    HeaderCss="SecurityDefaultTitle" 
    CssClass="modalPopup">
   
<div style="width:250px;float:left; height:250px; overflow:auto; background-color: lemonchiffon;">
    <telerik:RadTreeView 
		ID="RadTreeView1" 
		CheckBoxes="True" 
		CheckChildNodes="false" 
		TriStateCheckBoxes="True" 
		runat="server" 
		Height="250px" 
		Width="250px" 
		Skin="Office2007" 
		OnNodeExpand="RadTreeView1_NodeExpand"
		OnClientNodeChecked="ClientNodeChecked"  >
        </telerik:RadTreeView>
</div>
<div style="text-align:right;">
<span style="float:left">
	<asp:CheckBox ID="ckbAll" runat="server" Text="Select all" />	
</span>
<span style="float:right">
<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="BtnClick_Click" CausesValidation="True" />
</span>
</div>
</Innolux:ModalPanel>    


