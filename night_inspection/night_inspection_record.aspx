<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="night_inspection_record.aspx.cs"
    Inherits="night_inspection_night_inspection_record" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>夜巡資料</title>
    <link href="../app_themes/layout/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: inline; z-index: 105; left: 10px; width: 90%; color: black;
            top: 0px; height: 16px; background-color: white">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            </asp:ScriptManager>
            <br />
            <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
                <tr>
                    <td style="height: 9px">
                        <img src="../images/tables/default_lt.gif" /></td>
                    <td style="background-image: url(../images/tables/default_t.gif); height: 9px;">
                        <img height="9" src="../images/tables/transparent.gif" /></td>
                    <td style="height: 9px; width: 10px;">
                        <img src="../images/tables/default_rt.gif" /></td>
                </tr>
                <tr>
                    <td style="background-image: url(../images/tables/default_l.gif)">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                    <td align="middle" width="100%">
                        <table align="center" cellspacing="0" bordercolordark="#ffffff" cellpadding="2" width="100%"
                            bordercolorlight="#7a9cb7" border="1">
                            <tr>
                                <td background="" colspan="4" class="pageTitle">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" style="height: 30px">
                                                <span id="Span1" style="font-size: 16pt; font-family: Century Gothic"><strong>夜巡資料-更新</strong></span></td>
                                            <td align="right" style="font-size: 12px; color: navy; height: 30px;">
                                                <span style="color: #ff0000">*</span> 表示必填!</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td align="center" class="pageTD" colspan="4" style="height: 10px; text-align: left"
                                    valign="middle">
                                    <span style="color: navy">Step 1</span></td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" style="height: 7px; width: 10%;">
                                    紀錄人員<span style="color: red">*</span></td>
                                <td style="height: 7px; text-align: left;" valign="top" colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="Label3" runat="server" Text="Label" Width="151px"></asp:Label>&nbsp;
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    &nbsp;&nbsp;&nbsp;&nbsp;</td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" valign="middle" style="height: 22px; width: 10%;
                                    text-align: center;">
                                    發生時間<span style="color: red">*</span></td>
                                &nbsp;
                                <td style="text-align: left; width: 341px; height: 22px;" valign="top">
                                    &nbsp;<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="Label4" runat="server" Text="Label" Width="144px"></asp:Label><br />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    &nbsp;
                                </td>
                                <td class="pageTD" style="width: 10%; height: 22px; text-align: center;">
                                    異常種類<span style="color: red">*</span></td>
                                <td colspan="1" align="left" valign="top" style="text-align: left; height: 22px;
                                    width: 327px;">
                                    &nbsp;&nbsp;
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList1" runat="server">
                                            </asp:DropDownList>
                                            <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" style="height: 5px; width: 10%;">
                                    異常發生廠區<span style="color: red">*&nbsp;</span></td>
                                <td style="width: 341px; height: 5px; text-align: left;" valign="top">
                                    &nbsp;&nbsp;
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList2" runat="server">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    &nbsp;
                                </td>
                                <td class="pageTD" align="center" style="height: 5px; width: 10%;">
                                    部門<span style="color: red">*</span></td>
                                <td style="width: 327px; height: 5px; text-align: left;">
                                    &nbsp;<asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList3" runat="server">
                                            </asp:DropDownList>&nbsp;
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" style="height: 5px; width: 10%;">
                                    上傳檔案</td>
                                <td style="height: 5px; text-align: left;" valign="top" colspan="3">
                                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" OnClientClick="return check_field();"
                                        Text="上傳Link" Visible="False" />&nbsp;
                         
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" style="height: 5px; width: 10%;">
                                    異常狀況敘述<span style="color: red">*&nbsp;</span></td>
                                <td style="height: 5px; text-align: left;" valign="top" colspan="3">
                                    &nbsp;<asp:TextBox ID="TextBox5" runat="server" Height="100px" TextMode="MultiLine" Width="700px"></asp:TextBox>
                                    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" style="height: 5px; width: 10%;">
                                    通知廠區負責人<span style="color: red">*&nbsp;</span></td>
                                <td style="width: 341px; height: 5px; text-align: left;" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查詢電話" />
                                    &nbsp;
                                </td>
                                <td class="pageTD" align="center" style="height: 5px; width: 10%;">
                                    廠區負責人電話</td>
                                <td style="width: 327px; height: 5px; text-align: left;">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td align="center" class="pageTD" colspan="4" style="height: 10px; text-align: left"
                                    valign="middle">
                                    <span style="color: deeppink">Step 2</span></td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" valign="middle" style="height: 22px; width: 10%;
                                    text-align: center;">
                                   矯正對策<span style="color: red"></span></td>
                                <td style="text-align: left; height: 22px;" valign="top" colspan="3">
                                    &nbsp;<asp:TextBox ID="TextBox6" runat="server" Height="100px" TextMode="MultiLine" Width="700px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" valign="middle" style="height: 22px; width: 10%;
                                    text-align: center;">
                                    是否結案<span style="color: red"></span></td>
                                <td style="text-align: left; height: 22px;" valign="top" colspan="3"><asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        &nbsp;<asp:DropDownList ID="DropDownList4" runat="server" Height="24px" Width="105px">
                                            </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td align="center" class="pageTD" style="width: 10%; height: 22px; text-align: center"
                                    valign="middle">
                                    上傳檔案</td>
                                <td colspan="3" style="height: 22px; text-align: left" valign="top">
                                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" OnClientClick="return check_field22();"
                                        Text="上傳Link" Visible="False" />&nbsp;</td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td class="pageTD" align="center" valign="middle" style="height: 22px; width: 10%;
                                    text-align: center;">
                                    <span style="color: red"><span style="color: #000000">結案人</span></span></td>
                                <td style="text-align: left; height: 22px;" valign="top" colspan="3">
                                  <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="pageTD" colspan="8" style="height: 35px; text-align: center;">
                                    <asp:Button ID="Button2" runat="server"  OnClientClick="return check_field1();" OnClick="Button2_Click"
                                        Text="Add(Step2)" Visible="False" />
                                    <asp:Button ID="ButtonQuery" runat="server" Style="font-size: 12px; font-family: Arial;
                                        width: 100px;" Text="Modify(Step1)" OnClientClick="return check_field();" OnClick="ButtonQuery_Click"
                                        Visible="False" />
                                    &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                    &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
                                    &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                    &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
                                    &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                    &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td style="font-size: 12pt; background-image: url(../images/tables/default_r.gif);
                        width: 10px;">
                        <img src="../images/tables/transparent.gif" width="9"></td>
                </tr>
                <tr style="font-size: 12pt">
                    <td style="height: 9px">
                        <img src="../images/tables/default_lb.gif"></td>
                    <td style="background-image: url(../images/tables/default_b.gif); height: 9px;">
                        <img height="9" src="../images/tables/transparent.gif"></td>
                    <td style="height: 9px; width: 10px;">
                        <img src="../images/tables/default_rb.gif"></td>
                </tr>
            </table>
    </form>
</body>
</html>

<script language="javascript">
function check_field()
{
    if( document.getElementById("TextBox1").value=="")
    {
        alert("請輸入 紀錄人員!!!");
        return false;
    }
    else if( document.getElementById("txtEstimateStartDate").value=="")
    {
        alert("請輸入 發生時間!!!");
        return false;
    }
    else if( document.getElementById("DropDownList1").value=="")
    {
        alert("請輸入 異常種類!!!");
        return false;
    }
      else if( document.getElementById("DropDownList2").value=="" )
    {
        alert("請輸入 異常發生廠區!!!");
        return false;
    }
    
    else if( document.getElementById("DropDownList3").value=="" )
    {
        alert("請輸入 部門!!!");
        return false;
    }
    
    else if( document.getElementById("TextBox5").value=="" )
    {
        alert("請輸入 異常狀況敘述!!!");
        return false;
    }
      
     else if( document.getElementById("TextBox4").value=="" )
    {
        alert("請輸入 通知廠區負責人!!!");
        return false;
    }
   
   
   else if( document.getElementById("DropDownList4").value=="Closed"&& document.getElementById("TextBox6").value==""  )
    {
        alert("請輸入 矯正對策 !!!");
        return false;
    }
    
    
   
   else if( document.getElementById("DropDownList4").value=="Closed"&& document.getElementById("TextBox3").value==""  )
    {
        alert("請輸入 結案人!!!");
        return false;
    }
    
    
   
     
     
    else
    {
        return true;
    }
     
}

function check_field1()
{
    if( document.getElementById("TextBox6").value=="")
    {
        alert("請輸入 矯正對策!!!");
        return false;
    }
   
   
   
    
    
   
   else if(document.getElementById("TextBox3").value==""  )
    {
        alert("請輸入 結案人!!!");
        return false;
    }
    
      else if(document.getElementById("DropDownList4").value=="Closed"  )
    {
        return confirm('確認要將此紀錄【Closed】!!!');

    }
    
   
     
     
    else
    {
        return true;
    }
     
}

function check_field22()
{
    if( document.getElementById("TextBox6").value=="")
    {
        alert("請輸入 矯正對策!!!");
        return false;
    }
   
   
   
    
    
   
   else if(document.getElementById("TextBox3").value==""  )
    {
        alert("請輸入 結案人!!!");
        return false;
    }
    
  
    
   
     
     
    else
    {
        return true;
    }
     
}



function AddTask()
{
//   w = window.open("task_apply.aspx?project_id="+ document.getElementById('lblProjectNo').innerText ,"Add_task", "height=600, width=950, left=200, top=150, " +  "location=no,	menubar=no, resizable=yes, " + "scrollbars=yes, titlebar=no, toolbar=no", true);
//   w.focus();
   return false;
}

function OpenTask(task_id)
{
//   w = window.open("task_assign.aspx?task_id="+ task_id ,"task_maintain", "height=600, width=950, left=200, top=150, " +  "location=yes,	menubar=yes, resizable=yes, " + "scrollbars=yes, titlebar=no, toolbar=yes", true);
//   w.focus();
   return false;
}

function showHideAnswer(obj,imgObj)
{
    if (document.getElementById(obj) == null)
        return;
    if(document.getElementById(obj).style.display=='none'){
        document.getElementById(imgObj).src = "../images/close13.gif";
	    document.getElementById(obj).style.display='block';
    }else{
        document.getElementById(imgObj).src = "../images/open13.gif";
	    document.getElementById(obj).style.display='none';
    }		
}
</script>

