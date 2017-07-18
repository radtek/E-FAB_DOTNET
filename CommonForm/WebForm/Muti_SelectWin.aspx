<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Muti_SelectWin.aspx.cs" Inherits="Muti_SelectWin" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

    
	<head id="Head1" runat="server">
		<title></title>
		<style type="text/css">
		body
		{
		    font: normal 11px Arial, Verdana, Sans-serif;
		}
		</style>
		<script type="text/javascript" src ="../../CommonForm/JS/CommonForm.js" language ="javascript"></script>	
		 
 <script type="text/javascript" language="javascript">
 function GetRadWindow()
    {
	    var oWindow = null;
	    if (window.radWindow) oWindow = window.radWindow;
	    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
	    return oWindow;
    }						
function OK_Clicked(iCount)
{
    var oWindow = GetRadWindow();
	
    //Get current content of text area   
    var oNewText = document.getElementById("listBoxR") ;
    var TextSum="";
    var selectText="";
    if (oNewText.length>0)
    {
        for(i=0; i<oNewText.length; i++)  
        {
           selectText = oNewText.options[i].text;
           TextSum +=selectText +"," ;
        }
        oNewText=TextSum.substr(0,TextSum.length-1);
        oWindow.close(oNewText);
    }
    else
    {
        oWindow.close("");
    }		
}
 
 </script>
		
	</head>
	
	<body>
		<form id="Form2" method="post" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
        <table style="width: 271px">
            <tr>
                <td rowspan="1" style="width: 15px; text-align: center">
                    <telerik:RadComboBox ID="RadComboBox1" runat="server" CloseDropDownOnBlur="False"
                        MarkFirstMatch="True" Skin="Office2007" Width="160px">
                        
                    </telerik:RadComboBox>
                </td>
                <td style="width: 150px; text-align: center">
                </td>
                <td colspan="2" rowspan="1" style="text-align: center; ">
                    <asp:Button ID="Button_RadSelect" runat="server" Font-Bold="True" 
                                                                     Font-Italic="False"
                                                                     OnClick="Button_RadSelect_Click" 
                                                                     CssClass ="RadUploadSelectButton"
                                                                     Text="Add" 
                                                                     Height="21px" 
                                                                     Width="63px" />
                </td>
             </tr>
             <tr>
                <td rowspan="4" style="text-align: center">
                    <asp:ListBox ID="listBoxL" runat="server" Height="200px" 
                                                              Width="160px" 
                                                              SelectionMode="Multiple" 
                                                              BackColor="White">
                    </asp:ListBox></td>
                <td style=" text-align: center">
                    <asp:Button ID="Button_Select" runat="server"  OnClick="Button_Select_Click" 
                                                                   Text=">" 
                                                                   Font-Italic="False" 
                                                                   Width="23px" /></td>
                <td colspan="2" rowspan="4" style="text-align: center;">
                    <asp:ListBox ID="listBoxR" runat="server" Height="200px" 
                                                              Width="160px" 
                                                              SelectionMode="Multiple" 
                                                              BackColor="White">
                    </asp:ListBox></td>
            </tr>
            <tr>
                <td style="width: 150px; text-align: center">
                    <asp:Button ID="Button_UnSelect" runat="server" OnClick="Button_UnSelect_Click"
                        Text="<" Font-Italic="False" Width="23px" /></td>
            </tr>
            <tr>
                <td style="  text-align: center">
                    <asp:Button ID="Button_SelectAll" runat="server" OnClick="Button_SelectAll_Click"
                        Text=">>" Font-Italic="False" Width="23px" /></td>
            </tr>
            <tr>
                <td style=" text-align: center">
                    <asp:Button ID="Button_UnSelectAll" runat="server"
                        Text="<<" Font-Italic="False" Width="23px" OnClick="Button_UnSelectAll_Click" /></td>
            </tr>
           
        </table>
                    <br />
                    <table style="width: 337px; height: 21px">
                        <tr>
                            <td style="width: 267px">
                            </td>
                            <td style="width: 100px">
                                <asp:Button ID="Button_ok" runat="server" Text="OK" 
                                                              Font-Italic="False" 
                                                              OnClick="Button_ok_Click" 
                                                              CssClass ="RadUploadSelectButton"
                                                              Width="60px" OnClientClick="javascript:return OK_Clicked(1);" /></td>
                            <td style="width: 100px">
                    <asp:Button ID="Button_cancel" runat="server" Text="Cancel" 
                                                                  Font-Italic="False" 
                                                                  CssClass ="RadUploadSelectButton"
                                                                  OnClick="Button_cancel_Click" /></td>
                        </tr>
                    </table>
         </ContentTemplate>
         </asp:UpdatePanel>
             
            
		</form>
	</body>
</html>

