<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CellGradeSelector.ascx.cs" Inherits="CommonForm_UserControl_CellGradeSelector" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="Innolux" Namespace="Innolux.Portal.WebControls" %>

<script type="text/javascript" language="javascript">

            function SelectG0NGGrades()
            {
               var g0 = document.getElementById('<%=rbG0.ClientID%>');
               var g1 = document.getElementById('<%=rbG1.ClientID%>');
               var g2 = document.getElementById('<%=rbG2.ClientID%>');
               var g3 = document.getElementById('<%=rbG3.ClientID%>');
               var g4 = document.getElementById('<%=rbG4.ClientID%>');               
               var g5 = document.getElementById('<%=rbG5.ClientID%>');
               var g6 = document.getElementById('<%=rbG6.ClientID%>');
               var g7 = document.getElementById('<%=rbG7.ClientID%>');
               var g8 = document.getElementById('<%=rbG8.ClientID%>');
               var g9 = document.getElementById('<%=rbG9.ClientID%>');
               var ng = document.getElementById('<%=rbNG.ClientID%>');
               var lr = document.getElementById('<%=rbLR.ClientID%>');
               var ll = document.getElementById('<%=rbLL.ClientID%>');
               var pr = document.getElementById('<%=rbPR.ClientID%>');
               var ho = document.getElementById('<%=rbHO.ClientID%>');
               var lt = document.getElementById('<%=rbLT.ClientID%>');               
               
               var g0ng = document.getElementById('<%=rbG0NG.ClientID%>');
               var na = document.getElementById('<%=rbNA.ClientID%>');
               
               if ( g0ng.checked == true )
               {
                   g0.checked = true;
                   g1.checked = true;
                   g2.checked = true;
                   g3.checked = true;
                   g4.checked = true;
                   g5.checked = true;
                   g6.checked = true;
                   g7.checked = true;
                   //Marked by Donaldg8.checked = true;
                   g9.checked = true;
                   ng.checked = true;                  
               }
               else
               {
                   g0.checked = false;
                   g1.checked = false;
                   g2.checked = false;
                   g3.checked = false;
                   g4.checked = false;
                   g5.checked = false;
                   g6.checked = false;
                   g7.checked = false;
                   //Marked by Donaldg8.checked = false;
                   g9.checked = false;
                   ng.checked = false;               
               }
               
//               lr.checked = false;
//               ll.checked = false;
//               pr.checked = false;
//               ho.checked = false;
//               lt.checked = false;
//               g8.checked = false;//Add by Donald
//               na.checked = false;//Add by Star
            }
            
            function ShowCheckBoxPanel(selectedGrade) 
            {
               var g0 = document.getElementById('<%=rbG0.ClientID%>');
               var g1 = document.getElementById('<%=rbG1.ClientID%>');
               var g2 = document.getElementById('<%=rbG2.ClientID%>');
               var g3 = document.getElementById('<%=rbG3.ClientID%>');
               var g4 = document.getElementById('<%=rbG4.ClientID%>');               
               var g5 = document.getElementById('<%=rbG5.ClientID%>');
               var g6 = document.getElementById('<%=rbG6.ClientID%>');
               var g7 = document.getElementById('<%=rbG7.ClientID%>');
               var g8 = document.getElementById('<%=rbG8.ClientID%>');
               var g9 = document.getElementById('<%=rbG9.ClientID%>');
               var ng = document.getElementById('<%=rbNG.ClientID%>');
               var lr = document.getElementById('<%=rbLR.ClientID%>');
               var ll = document.getElementById('<%=rbLL.ClientID%>');
               var pr = document.getElementById('<%=rbPR.ClientID%>');
               var ho = document.getElementById('<%=rbHO.ClientID%>');
               var lt = document.getElementById('<%=rbLT.ClientID%>');        
               
               var g0ng = document.getElementById('<%=rbG0NG.ClientID%>');                                    
               var na = document.getElementById('<%=rbNA.ClientID%>');
               /*
               if ( selectedGrade == 'LR' || selectedGrade == 'LL' || selectedGrade == 'PR' ||
                    selectedGrade == 'HO' || selectedGrade == 'LT' || selectedGrade == 'G8') //Modify by Donald - Add selectedGrade == 'G8'
                    {
                       g0.checked = false;
                       g1.checked = false;
                       g2.checked = false;
                       g3.checked = false;
                       g4.checked = false;
                       g5.checked = false;
                       g6.checked = false;
                       g7.checked = false;
                       //Marked by Donaldg8.checked = false;
                       g9.checked = false;
                       ng.checked = false;
                       
                       g0ng.checked = false;
                       
                       if ( selectedGrade != 'LR' ) lr.checked = false;
                       if ( selectedGrade != 'LL' ) ll.checked = false;
                       if ( selectedGrade != 'PR' ) pr.checked = false;
                       if ( selectedGrade != 'HO' ) ho.checked = false;
                       if ( selectedGrade != 'LT' ) lt.checked = false;    
                       if ( selectedGrade != 'G8' ) g8.checked = false; //Add by Donald                   
                    }   
               else 
               if ( selectedGrade == 'G0' || selectedGrade == 'G1' || selectedGrade == 'G2' ||
                    selectedGrade == 'G3' || selectedGrade == 'G4' || selectedGrade == 'G5' || 
                    selectedGrade == 'G6' || selectedGrade == 'G7' || //Marked by DonaldselectedGrade == 'G8' ||
                    selectedGrade == 'G9' || selectedGrade == 'NG' )
               {                                                                                                    
                     lr.checked = false;
                     ll.checked = false;
                     pr.checked = false;
                     ho.checked = false;
                     lt.checked = false;
                     g8.checked = false; //Add by Donald
                     
                     g0ng.checked = g0.checked == true && g1.checked == true && g2.checked == true &&
                                    g3.checked == true && g4.checked == true && g5.checked == true &&
                                    g6.checked == true && g7.checked == true && //Marked by Donaldg8.checked == true &&
                                    g9.checked == true && ng.checked == true;
                     
               }     
               */                       
                    
            }     
            
            function SelectedCellGradesList()
            {           

               var g0 = document.getElementById('<%=rbG0.ClientID%>');
               var g1 = document.getElementById('<%=rbG1.ClientID%>');
               var g2 = document.getElementById('<%=rbG2.ClientID%>');
               var g3 = document.getElementById('<%=rbG3.ClientID%>');
               var g4 = document.getElementById('<%=rbG4.ClientID%>');               
               var g5 = document.getElementById('<%=rbG5.ClientID%>');
               var g6 = document.getElementById('<%=rbG6.ClientID%>');
               var g7 = document.getElementById('<%=rbG7.ClientID%>');
               var g8 = document.getElementById('<%=rbG8.ClientID%>');
               var g9 = document.getElementById('<%=rbG9.ClientID%>');
               var ng = document.getElementById('<%=rbNG.ClientID%>');
               var lr = document.getElementById('<%=rbLR.ClientID%>');
               var ll = document.getElementById('<%=rbLL.ClientID%>');
               var pr = document.getElementById('<%=rbPR.ClientID%>');
               var ho = document.getElementById('<%=rbHO.ClientID%>');
               var lt = document.getElementById('<%=rbLT.ClientID%>');             
               var na = document.getElementById('<%=rbNA.ClientID%>');
               
               var txtCellGradeList = document.getElementById('<%=txtCellGrades.ClientID%>');                    


               // assign selected grades list
               resultStr = "";
               
               if ( g0.checked == true )
                    resultStr += ",G0";        
               if ( g1.checked == true )
                    resultStr += ",G1";                    
               if ( g2.checked == true )
                    resultStr += ",G2";        
               if ( g3.checked == true )
                    resultStr += ",G3";
               if ( g4.checked == true )
                    resultStr += ",G4";        
               if ( g5.checked == true )
                    resultStr += ",G5";                    
               if ( g6.checked == true )
                    resultStr += ",G6";        
               if ( g7.checked == true )
                    resultStr += ",G7";       
               if ( g8.checked == true )
                    resultStr += ",G8";        
               if ( g9.checked == true )
                    resultStr += ",G9";                    
               if ( ng.checked == true )
                    resultStr += ",NG";        
               if ( lr.checked == true )
                    resultStr += ",LR";
               if ( ll.checked == true )
                    resultStr += ",LL";
               if ( pr.checked == true )
                    resultStr += ",PR";
               if ( ho.checked == true )
                    resultStr += ",HO";
               if ( lt.checked == true )
                    resultStr += ",LT";
               if ( na.checked == true )
                    resultStr += ",NA";                                   
                                   
               if ( resultStr == "")
                    //Marked by DonaldtxtCellGradeList.value = "G0,G1,G2,G3,G4,G5,G6,G7,G8,G9,NG";
                    txtCellGradeList.value = "G0,G1,G2,G3,G4,G5,G6,G7,G9,NG"; //Modify by Donald - remove G8
               else
                    txtCellGradeList.value = resultStr.substr(1,resultStr.length-1);  

            }
            
            function validateCellGradeList(source, arguments)
            {
               var g0 = document.getElementById('<%=rbG0.ClientID%>');
               var g1 = document.getElementById('<%=rbG1.ClientID%>');
               var g2 = document.getElementById('<%=rbG2.ClientID%>');
               var g3 = document.getElementById('<%=rbG3.ClientID%>');
               var g4 = document.getElementById('<%=rbG4.ClientID%>');               
               var g5 = document.getElementById('<%=rbG5.ClientID%>');
               var g6 = document.getElementById('<%=rbG6.ClientID%>');
               var g7 = document.getElementById('<%=rbG7.ClientID%>');
               var g8 = document.getElementById('<%=rbG8.ClientID%>');
               var g9 = document.getElementById('<%=rbG9.ClientID%>');
               var ng = document.getElementById('<%=rbNG.ClientID%>');
               var lr = document.getElementById('<%=rbLR.ClientID%>');
               var ll = document.getElementById('<%=rbLL.ClientID%>');
               var pr = document.getElementById('<%=rbPR.ClientID%>');
               var ho = document.getElementById('<%=rbHO.ClientID%>');
               var lt = document.getElementById('<%=rbLT.ClientID%>'); 
               
               var g0ng = document.getElementById('<%=rbG0NG.ClientID%>'); 
               var na = document.getElementById('<%=rbNA.ClientID%>');
               
               arguments.IsValid  = g0.checked || g1.checked || g2.checked || g3.checked || g4.checked ||
                                    g5.checked || g6.checked || g7.checked || g8.checked || g9.checked ||
                                    ng.checked || lr.checked || ll.checked || pr.checked || ho.checked ||
                                    lt.checked || na.checked;
               if( arguments.IsValid == false )         
               {
                   g0.checked = true;
                   g1.checked = true;
                   g2.checked = true;
                   g3.checked = true;
                   g4.checked = true;
                   g5.checked = true;
                   g6.checked = true;
                   g7.checked = true;
                   //Marked by Donaldg8.checked = true;
                   g9.checked = true;
                   ng.checked = true;  

                   g0ng.checked = true;                      
               }
            }

</script>
<table>
	<tr runat="server" id="trMainProd">
		<td>
			<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false" RenderMode="Inline">
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
				</Triggers>
				<ContentTemplate>
					<asp:TextBox ID="txtCellGrades" runat="server" ReadOnly="True"></asp:TextBox>
				</ContentTemplate>
			</asp:UpdatePanel>
		</td>
		<td valign="bottom">
			<asp:Image ID="ModalPopOpenImg" runat="server" ImageUrl="~/Images/Edit.gif" ImageAlign="Bottom" Style="cursor:pointer" />
				<asp:CustomValidator ID="CustomValidator1" runat="server"
	                 ClientValidationFunction="validateCellGradeList" EnableClientScript="True"
	                 ErrorMessage="Please check at least one grade">
	            </asp:CustomValidator>
		</td>
	</tr>	
</table>

<Innolux:ModalPanel
    ID="modalPanel" 
    runat="server" 
    Width="275px" Height="230px"
    Title="Cell Grade Selector" 
    ActivateControlId="ModalPopOpenImg" 
    CloseImageUrl="~/Images/cancel.gif" 
    HeaderCss="SecurityDefaultTitle" 
    CssClass="modalPopup">
   
<div id="CellGradeListResult" style="width:275px;float:left; height:180px; overflow:auto; background-color:lemonchiffon;">
    <table>
    <tr><td colspan=3 valign="bottom">
    <asp:Label ID="Label1" runat="server" Text="Please Select Following Cell Grades :" ForeColor="Indigo"></asp:Label>
    <hr style="font-weight: bold" />
    </td></tr>
    <tr><td colspan= 3>
    <asp:CheckBox ID="rbG0NG" Text="Select All G0~NG" runat="server" GroupName="G0NG" onclick="SelectG0NGGrades();" checked="true"/>
    </td>
    </tr><tr><td>&nbsp;&nbsp;&nbsp;</td>
    <td style="border-right: fuchsia thin dotted; border-top: fuchsia thin dotted; border-left: fuchsia thin dotted; border-bottom: fuchsia thin dotted">    
    <asp:CheckBox ID="rbG0" Text="G0" runat="server" GroupName="G0" onclick="ShowCheckBoxPanel('G0');" checked="true"/>
    <asp:CheckBox ID="rbG1" Text="G1" runat="server" GroupName="G1" onclick="ShowCheckBoxPanel('G1');" checked="true"/>
    <asp:CheckBox ID="rbG2" Text="G2" runat="server" GroupName="G2" onclick="ShowCheckBoxPanel('G2');" checked="true"/>
    <asp:CheckBox ID="rbG3" Text="G3" runat="server" GroupName="G3" onclick="ShowCheckBoxPanel('G3');" checked="true"/>
    <asp:CheckBox ID="rbG4" Text="G4" runat="server" GroupName="G4" onclick="ShowCheckBoxPanel('G4');" checked="true"/>
    <asp:CheckBox ID="rbG5" Text="G5" runat="server" GroupName="G5" onclick="ShowCheckBoxPanel('G5');" checked="true"/><br />
    <asp:CheckBox ID="rbG6" Text="G6" runat="server" GroupName="G6" onclick="ShowCheckBoxPanel('G6');" checked="true"/>
    <asp:CheckBox ID="rbG7" Text="G7" runat="server" GroupName="G7" onclick="ShowCheckBoxPanel('G7');" checked="true"/>      
    <asp:CheckBox ID="rbG9" Text="G9" runat="server" GroupName="G9" onclick="ShowCheckBoxPanel('G9');" checked="true"/>
    <asp:CheckBox ID="rbNG" Text="NG" runat="server" GroupName="NG" onclick="ShowCheckBoxPanel('NG');" checked="true"/> 
    </td>
    <td>&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
    <td colspan=3 valign="bottom"><br />
    <asp:CheckBox ID="rbLR" Text="LR" runat="server" GroupName="LR" onclick="ShowCheckBoxPanel('LR');" checked="false"/>
    <asp:CheckBox ID="rbLL" Text="LL" runat="server" GroupName="LL" onclick="ShowCheckBoxPanel('LL');" checked="false"/>
    <asp:CheckBox ID="rbPR" Text="PR" runat="server" GroupName="PR" onclick="ShowCheckBoxPanel('PR');" checked="false"/>
    <asp:CheckBox ID="rbHO" Text="HO" runat="server" GroupName="HO" onclick="ShowCheckBoxPanel('HO');" checked="false"/>
    <asp:CheckBox ID="rbLT" Text="LT" runat="server" GroupName="LT" onclick="ShowCheckBoxPanel('LT');" checked="false"/>
    <asp:CheckBox ID="rbG8" Text="G8" runat="server" GroupName="G8" onclick="ShowCheckBoxPanel('G8');" checked="false"/>
    <asp:CheckBox ID="rbNA" Text="NA" runat="server" GroupName="NA" onclick="ShowCheckBoxPanel('NA');" checked="false"/>
    <hr style="font-weight: bold" />
    </td></tr>
    </table>    
</div>
<div style="text-align:right; vertical-align: bottom;">
<span style="float:left">&nbsp;</span><span style="float:right">&nbsp;</span>
<asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="SelectedCellGradesList();" OnClick="BtnClick_Click" CausesValidation="true" /></div>
</Innolux:ModalPanel>    


