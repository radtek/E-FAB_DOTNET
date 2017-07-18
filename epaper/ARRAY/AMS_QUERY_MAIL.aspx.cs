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
using System.Data.OleDb;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Net.Mail;

public partial class epaper_ARRAY_AMS_QUERY_MAIL : System.Web.UI.Page
{
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE"];
    string conn2 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_SSODB_OLE"];

    string sql = "";
    string sql1 = "";

    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();

    string Today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    string yesterday = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

    protected void Page_Load(object sender, EventArgs e)
    {
        //**************針對 Title 呈現  Status  start *********//
        string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE"];
        string conn2 = System.Configuration.ConfigurationSettings.AppSettings["RPTEST"];
       
        //ds_temp2 = func.get_dataSet_access(sql1, conn1);


        //**************針對 Title 呈現  Status  end *********//

        string ActionType = "變更後";
        string sheet_no = "LC1_N16042902_hellowkitty";
        string MailTo = "oscar.hsieh@innolux.com";
        MailMethod(ActionType, sheet_no, MailTo);
      

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
    }

    private static void MailMethod(string ActionType, string sheet_no,string sendTo)
    {
        WebClient w = new WebClient();
        w.Encoding = Encoding.GetEncoding("utf-8");
        string strHTML = w.DownloadString("http://10.56.131.10/T1_Innoview/Fabinfo/AMS/AMS_ARS_QUERY.aspx?sheet_no=" + sheet_no + "");


        //【異常品管理系統】<溫馨提醒>2016/05/10 單號 xxxxxxx 變更後 

        string title = "[異常品管理系統]<溫馨提醒> " + DateTime.Now.ToString("yyyy/MM/dd") + " 單號 " + sheet_no + "【 " + ActionType + " 】 ";


        //SendEmail("Alarm_Server@innolux.com.tw", "oscar.hsieh@inl,bunny.su@inl", title, strHTML, "");//

        ArrayList maillist = new ArrayList();
        //maillist = func.FileToArray(Server.MapPath("..\\") + "\\maillist\\T0Array_Scrap_detail.txt");
        sendTo = "oscar.hsieh@innolux.com";


        //maillist1[0] = "oscar.hsieh@innolux.com";
        SendEmail("cim.alarm@innolux.com", sendTo, title, strHTML, "");//
    }



    public static void SendEmail(string from, string to, string subject, string body, string cca)
    {
        SmtpClient smtp = new SmtpClient("10.56.196.147");
        MailMessage email = new MailMessage(from, to, subject, body);
        if (cca == "")
        {
        }
        else
        {
            email.CC.Add(cca);
        }

        email.IsBodyHtml = true;
        smtp.Send(email);


    } 
}
