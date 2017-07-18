<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultiDropDownList.ascx.cs" Inherits="CommonForm_UserControl_MultiDropDownList" %>
<HEAD>
		<TITLE>MultiselectionDropdown</TITLE>
	</HEAD>
	<script language="javascript">
			function SelectedIndexChanged(ctlId)
			{
   				var control = document.getElementById(ctlId+'DDList'); 
				var strSelText='';
				for(var i = 0; i < control.length; i ++)
				{ 
					if(control.options[i].selected)
					{
						strSelText +=control.options[i].text + ',';
					}
				}
				if (strSelText.length>0)
					strSelText=strSelText.substring(0,strSelText.length-1);
				var ddLabel = document.getElementById(ctlId+"DDLabel"); 
				ddLabel.innerHTML = strSelText;
				ddLabel.innerText  = strSelText;
				ddLabel.title = strSelText;
			}

			function OpenListBox(ctlId)
			{
				var lstBox = document.getElementById(ctlId+"DDList");
				if (lstBox.style.visibility == "visible")				
				{ CloseListBox(ctlId) ; }
				else
				{
					lstBox.style.visibility = "visible"; 
					lstBox.style.height="100px";
				}
			}

			function CloseListBox(ctlId)
			{
				var panel = document.getElementById(ctlId+"Panel2");
				var tabl = document.getElementById(ctlId+"Table2");
				var lstBox = document.getElementById(ctlId+"DDList");
				lstBox.style.visibility = "hidden"; 
				lstBox.style.height="0px";
				panel.style.height=tabl.style.height;
			}
	</script>
	<asp:panel id="Panel2" Height="1px" runat="server" Width="160px" BackColor="White">
		<TABLE id="Table2" style="TABLE-LAYOUT: fixed; HEIGHT: 20px" width="100%" borderColorLight="steelblue" runat="server" bordercolor="transparent">
			<TR id="rowDD" style="HEIGHT: 18px" runat="server">
				<TD noWrap>
					<asp:Label id="DDLabel" style="CURSOR: default" runat="server" Width="152px"
						Font-Names="Arial" height="16px" ForeColor="Transparent" BorderWidth="1px" BorderColor="GradientInactiveCaption"></asp:Label></TD>
				
			</TR>
		</TABLE>
		<DIV style="Z-INDEX: 9999; POSITION: absolute">
			<asp:ListBox id="DDList" Height="0px" runat="server" Width="100%" SelectionMode="Multiple" EnableTheming="True" Rows="1"></asp:ListBox></DIV>
	</asp:panel>