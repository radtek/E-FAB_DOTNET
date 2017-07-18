using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Monitor_Alarm_Server_Monitor_AlarmServer03 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        func.Upload(Server.MapPath(".\\") + "\\Check_at.txt", "172.16.12.121", "anonymous", "");
        string add_drive_id = "";


        add_drive_id = func.get_netdrive_id();
   

      
        
        // FileSystemView view = FileSystemView.getFileSystemView();
        //func.start_process(@"net use "+add_drive_id+@": /delete");
        func.start_process(@"net use "+add_drive_id+@": \\172.16.12.121\c$ M01@dmin /USER:172.16.12.121\administrator");
        func.start_process(@"net use "+add_drive_id+@": /delete");
        //func.start_process(@"net use Z: /delete");
        //javascript 語法填入 字串 
        string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_parent','');window.top.close(this);</script>";
        //呼叫 javascript 
       this.Page.RegisterStartupScript("", frmClose);



    }


}
