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

public partial class epaper_ARRAY_Array_scrap_detail_epaper : System.Web.UI.Page
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
        DataSet ds_temp = new DataSet();
         sql = " select T1.LOT_ID LOT_ID,                                           " +
"        T1.PROD_NAME PRODUCT,                                       " +
"        T3.GLASS_ID GLASS_ID,                                       " +
"        T1.LOT_TYPE LOT_TYPE,                                       " +
"        T1.STAGE STAGE,                                             " +
"        T1.CLAIM_STEP STEP_NO,                                      " +
"        T1.PRIORITY PRIORITY,                                       " +
"        to_char(T1.CLAIM_DTTM, 'yyyy/mm/dd hh24:mi:ss') SCRAP_TIME, " +
"        T3.SCRAP_REASON_CODE SCRAP_REASON_CODE,                     " +
"        Convert( T1.SCRAP_CMMT , 'UTF8', 'ZHT16MSWIN950' ) as SCRAP_CMMT ,                                  " +
"                                                                    " +
"        T1.TA_ID      TAID,                                         " +
"        T1.SCRAP_TYPE as SCRAP_UNSCRAP                             " +
"                                                                    " +
"   from SCRAP_LOT_HISTORY T1, LOT T2, SCRAP_SUBSTRATE_HISTORY T3    " +
"  where T1. CLAIM_DTTM = T3. CLAIM_DTTM                             " +
"    and T1. LOT_ID = T3. LOT_ID                                     " +
"    and T1. LOT_ID = T2. LOT_ID                                     " +
"    and to_date(T1.SHIFT_DATE, 'yyyy/mm/dd') between                " +
"        to_date('" + yesterday + "', 'yyyy/mm/dd') and                      " +
"        to_date('" + Today + "', 'yyyy/mm/dd')                          " +
"    and T1.SHOP = 'T0Array'                                         " +
"    and T1.LOT_TYPE in ('E', 'P')                                   " +
"    and T1.scrap_type = 'S'                                         ";

        sql = "select count(*) as count  from(" + sql + ")";
        ds_temp1 = func.get_dataSet_access(sql, conn1);


        sql1 = " select T1.LOT_ID LOT_ID,                                           " +
"        T1.PROD_NAME PRODUCT,                                       " +
"        T3.GLASS_ID GLASS_ID,                                       " +
"        T1.LOT_TYPE LOT_TYPE,                                       " +
"        T1.STAGE STAGE,                                             " +
"        T1.CLAIM_STEP STEP_NO,                                      " +
"        T1.PRIORITY PRIORITY,                                       " +
"        to_char(T1.CLAIM_DTTM, 'yyyy/mm/dd hh24:mi:ss') SCRAP_TIME, " +
"        T3.SCRAP_REASON_CODE SCRAP_REASON_CODE,                     " +
"        Convert( T1.SCRAP_CMMT , 'UTF8', 'ZHT16MSWIN950' ) as SCRAP_CMMT ,                                  " +
"                                                                    " +
"        T1.TA_ID      TAID,                                         " +
"        T1.SCRAP_TYPE as SCRAP_UNSCRAP                             " +
"                                                                    " +
"   from SCRAP_LOT_HISTORY T1, LOT T2, SCRAP_SUBSTRATE_HISTORY T3    " +
"  where T1. CLAIM_DTTM = T3. CLAIM_DTTM                             " +
"    and T1. LOT_ID = T3. LOT_ID                                     " +
"    and T1. LOT_ID = T2. LOT_ID                                     " +
"    and to_date(T1.SHIFT_DATE, 'yyyy/mm/dd') between                " +
"        to_date('" + yesterday + "', 'yyyy/mm/dd') and                      " +
"        to_date('" + Today + "', 'yyyy/mm/dd')                          " +
"    and T1.SHOP = 'T1Array'                                         " +
"    and T1.LOT_TYPE in ('E', 'P')                                   " +
"    and T1.scrap_type = 'S'                                         ";

        sql1 = "select count(*) as count  from(" + sql1 + ")";
        ds_temp2 = func.get_dataSet_access(sql1, conn1);


        //**************針對 Title 呈現  Status  end *********//

        WebClient w = new WebClient();
        w.Encoding = Encoding.GetEncoding("big5");
        string strHTML = w.DownloadString("http://t1cimweb01/E-FAB_dotnet/epaper/ARRAY/sample/Array_scrap_detail.aspx?area=T0");
        string strHTML1 = w.DownloadString("http://t1cimweb01/E-FAB_dotnet/epaper/ARRAY/sample/Array_scrap_detail.aspx?area=T1");
        string title = "[CIM 電子報系統] " + DateTime.Now.ToString("yyyy/MM/dd") + " T0Array ScrapDetail 【共 " + ds_temp1 .Tables[0].Rows[0][0]+ "  片資料 】 ";
        string title1 = "[CIM 電子報系統] " + DateTime.Now.ToString("yyyy/MM/dd") + " T1Array ScrapDetail 【共 " + ds_temp2.Tables[0].Rows[0][0] + " 片資料 】 ";

        //SendEmail("Alarm_Server@innolux.com.tw", "oscar.hsieh@inl,bunny.su@inl", title, strHTML, "");//

        ArrayList maillist = new ArrayList();
        maillist = func.FileToArray(Server.MapPath("..\\") + "\\maillist\\T0Array_Scrap_detail.txt");
        //maillist[0] = "oscar.hsieh@chimei-innolux.com";


        ArrayList maillist1 = new ArrayList();
        maillist1 = func.FileToArray(Server.MapPath("..\\") + "\\maillist\\T1Array_Scrap_detail.txt");
       // maillist1[0] = "oscar.hsieh@chimei-innolux.com";
        SendEmail("cim.alarm@chimei-innolux.com", maillist[0].ToString(), title, strHTML, "");//
        SendEmail("cim.alarm@chimei-innolux.com", maillist1[0].ToString(), title1, strHTML1, "");//

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
