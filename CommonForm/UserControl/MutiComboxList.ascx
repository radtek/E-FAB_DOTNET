<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MutiComboxList.ascx.cs" Inherits="MutiComboxList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../../App_Themes/Blue/css.css" rel="stylesheet" type="text/css" />

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate >
         <asp:TextBox ID="Text_Select" runat="server"  Width="145px"></asp:TextBox>
            <asp:panel  ID="Panel_Select" runat="server"  Height ="120px" BorderWidth ="1" BorderColor ="gray">
            <asp:Panel ID="Panel_CheckList" runat ="server" Height ="100px" BorderWidth  ="1px" 
                                                                        ScrollBars="Auto" 
                                                                        Width ="145px" 
                                                                        BorderColor ="gray"
                                                                        
                                                                        BackColor ="white">
            <asp:CheckBoxList ID="CheckList_Select" runat="server" 
                                      BorderColor ="gray"
                                      BackColor ="white"
                                      AutoPostBack ="False" 
                                      Width="121px"
                                      >
                
             </asp:CheckBoxList>
             </asp:Panel>
             <asp:Panel  ID ="Panel_Button" runat ="server" BackColor ="white" 
                                                            Height="16px" 
                                                            Width="144px" 
                                                            BackImageUrl ="~/App_Themes/Blue/Images/guidance2.jpg"
                                                            HorizontalAlign="Left">
                <table>
                    <tr >
                        <td style="width: 100px">
                            <asp:LinkButton  ID ="Link_SelectALl" Text ="Select All" 
                                                                  runat ="server" 
                                                                  OnClick ="cmdSelectAll" 
                                                                  Width="63px" 
                                                                  Height ="21px"
                                                                           >
                            </asp:LinkButton>
                        </td>
                        <td style="width: 100px">
                            <asp:LinkButton  ID ="Link_OK" Text ="OK" runat ="server" 
                                                                      OnClick ="cmdOK_Click" 
                                                                      Width="63px" 
                                                                      Height ="21px"
                                                                           >
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
             </asp:Panel>
            </asp:panel> 
            <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID ="Text_Select"
                                                                                 PopupControlID ="Panel_Select"
                                                                                 Position ="Bottom"
                                                                                 OffsetX =2>
                                                                                
            </cc1:PopupControlExtender>
     </ContentTemplate>
</asp:UpdatePanel>
