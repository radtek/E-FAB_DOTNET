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

public partial class OEE_st_C2A_tacttime_config_sendmail : System.Web.UI.Page
{
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_OLE"];
    string conn2 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_SSODB_OLE"];

    string sql_temp = "";
    string sql1 = "";

    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();

    string Today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    string yesterday = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

    protected void Page_Load(object sender, EventArgs e)
    {
        //**************針對 Title 呈現  Status  start *********//
        sql_temp = @"

      
   select t.line,t.equipmentid,t.productid,t.stepid,t.standardtime,t.tacttime,t.userid from empasttarget t
where (t.equipmentid like '0AANO%00'
or t.equipmentid like '0ACOA%00'
or t.equipmentid like '0ACVD%00'
or t.equipmentid like '0ADET%00'
or t.equipmentid like '0ADEV%00'
or t.equipmentid like '0AORO%00'
or t.equipmentid like '0APDC%00'
or t.equipmentid like '0APVD%00'
or t.equipmentid like '0ASTR%00'
or t.equipmentid like '0AWET%00'
or t.equipmentid like '0AFLP%00'
or t.equipmentid like '0AREF%00'
or t.equipmentid like '0ATES%00'
or t.equipmentid like '0AEXP%00'
)
and t.standardtime=0 and t.tacttime=0
and t.userid='SYSTEM'
and t.processgroup=' '
--and t.productid='700V1A'
--and t.stepid='1GE_Photo_01'
and t.equipmentid not in ('0ATES200','0ATES300')
and t.productid not like '%MQC%'
order by t.equipmentid,t.productid

";
        ds_temp1 = func.get_dataSet_access(sql_temp, conn1);

        DataView dv = new DataView();

        dv = ds_temp1.Tables[0].DefaultView;

        DataTable DT = new DataTable();

        DT = dv.ToTable(true, "line", "equipmentid", "productid", "stepid", "standardtime", "tacttime", "userid");

        //**************針對 Title 呈現  Status  end *********//

        WebClient w = new WebClient();
        w.Encoding = Encoding.GetEncoding("big5");
        string strHTML = w.DownloadString("http://10.56.131.22/E-FAB_dotnet/oee/st_C2A_tacttime_config.aspx");

        string title = "[CIM 電子報系統] " + DateTime.Now.ToString("yyyy/MM/dd") + "<<溫馨提醒>>  C2OEE ST Tacttime 未設定 【共 " + DT.Rows.Count.ToString() + "  筆資料 】 ";


        //SendEmail("Alarm_Server@innolux.com.tw", "oscar.hsieh@inl,bunny.su@inl", title, strHTML, "");//

        ArrayList maillist = new ArrayList();
        maillist = func.FileToArray(Server.MapPath("..\\") + "epaper\\maillist\\OEE_ST_C2_TACTTIME.txt");
        //maillist[0] = "oscar.hsieh@chimei-innolux.com";



        // maillist1[0] = "oscar.hsieh@chimei-innolux.com";
        SendEmail("cim.alarm@innolux.com", maillist[0].ToString(), title, strHTML, "");//


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
