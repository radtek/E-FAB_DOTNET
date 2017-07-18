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
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Net.Mail;

public partial class epaper_ARRAY_OEETacttimeCheck : System.Web.UI.Page
{
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_RPT"];
    string conn2 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_SSODB_OLE"];
    string conn3 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_PARS1_OLE_ONDUTY"];

    string sql = "";
    string sql1 = "";

    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();

    string Today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    string yesterday = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

    protected void Page_Load(object sender, EventArgs e)
    {
        //**************針對 Title 呈現  Status  start *********//
      
        DataSet ds_temp = new DataSet();
     

        //**************針對 Title 呈現  Status  end *********//

        WebClient w = new WebClient();
        w.Encoding = Encoding.GetEncoding("big5");
        string strHTML = w.DownloadString("http://10.56.131.22/E-FAB_dotnet/epaper/ARRAY/sample/MoveTacttimeCheck.aspx");
        //string strHTML1 = w.DownloadString("http://t1cimweb01/E-FAB_dotnet/epaper/ARRAY/sample/Array_scrap_detail.aspx?area=T1");
        string title = "[CIM 電子報系統]<溫馨提醒> " + DateTime.Now.ToString("yyyy/MM/dd") + " OEE Tacttime Check !!!  " ;
        //string title1 = "[CIM 電子報系統] " + DateTime.Now.ToString("yyyy/MM/dd") + " T1Array ScrapDetail 【共 " + ds_temp2.Tables[0].Rows[0][0] + " 片資料 】 ";

        //SendEmail("Alarm_Server@innolux.com.tw", "oscar.hsieh@inl,bunny.su@inl", title, strHTML, "");//

        ArrayList maillist = new ArrayList();
        maillist = func.FileToArray(Server.MapPath("..\\") + "\\maillist\\Array_HoldLot_detail_for_td.txt");
        //maillist[0] = "oscar.hsieh@chimei-innolux.com";


        //ArrayList maillist1 = new ArrayList();
        //maillist1 = func.FileToArray(Server.MapPath("..\\") + "\\maillist\\T1Array_Scrap_detail.txt");
        // maillist1[0] = "oscar.hsieh@chimei-innolux.com";
        maillist[0] = "oscar.hsieh@innolux.com";


       string sqlCheck = @"select count(t.procedurename) counter from dw_etl_runlog t
                       where t.procedurename='OeeTactTimeCheck' and t.lastrunsysdate>sysdate-1/2
                     ";
        DataSet dsCheck = new DataSet();
        dsCheck = func.get_dataSet_access(sqlCheck, conn3);
        if (Convert.ToInt32(dsCheck.Tables[0].Rows[0]["counter"].ToString()) == 0)
        {
            SendEmail("cim.alarm@innolux.com", maillist[0].ToString(), title, strHTML, "");//

            string sqlInsert = @"insert into dw_etl_runlog
                          (procedurename, lastruntm, errcode, errmsg, lastrunshiftdate, lastrunsysdate, errlevel, duration)
                          values
                         ('OeeTactTimeCheck', '', 'Begin', 'OK', '', sysdate, '', '')";
            func.get_sql_execute(sqlInsert, conn3);
        }

        
        
        //SendEmail("cim.alarm@chimei-innolux.com", maillist1[0].ToString(), title1, strHTML1, "");//

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
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
