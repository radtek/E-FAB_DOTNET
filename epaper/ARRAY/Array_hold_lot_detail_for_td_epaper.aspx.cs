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
using System.Text;
using System.Net.Mail;

public partial class epaper_ARRAY_Array_hold_lot_detail_for_td_epaper : System.Web.UI.Page
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
        sql = @"


        select 
t1.shop,

       T1.LOT_ID LOT_ID,
       T1.CST_ID CST_ID,
       T1.LOT_TYPE LOT_TYPE,
       T2.HOLDING_STEP_NAME HOLDING_STEP_NAME,
       T1.STEP_DESC STEP_DESC,
       T1.PROC_STATE STATUS,
       T1.CUT_SIZE CUT_SIZE,
       T2.HOLD_SUBSTRATE_QTY SUBSTRATE_CHIP_QTY,
       T1.PRIORITY PRIORITY,
       to_char(T2.HOLD_DTTM, 'yyyy/mm/dd hh24:mi:ss') HOLD_START_TIME,
       T1.MODULE MODULE,
       NVL(T1.LOT_OWNER_ID, 'NA') LOT_OWNER,
       Convert( T1.LOT_CMMT, 'UTF8','ZHT16MSWIN950' ) LOT_COMMENT,
       T2.TA,
       Round(((SYSDATE) - (T2.HOLD_DTTM)) * 24, 2) as ""H/T(hrs)"",
       T2.HOLD_REASON_CODE HOLD_REASON_CODE,
       T1.PROCPLAN_NAME
  from RPTDW.LOT T1, RPTDW.HOLD_LOT_HISTORY T2
 where T1.LOT_ID = T2.LOT_ID
   and T1.FAB = T2.FAB
   and T1.SHOP = T2.SHOP
   and T1.PROC_STATE = 'OnHold'
   and T1.LOT_ID <> ' ALL'
   and NVL(TO_CHAR(T2.RELEASE_TIME, 'YYYY/MM/DD'), 'NULL') = 'NULL'
   and T1.STEP_NAME = T2.HOLDING_STEP_NAME
   and t1.terminate_dttm is null
   --and T1.SHOP = 'T1Array'
   and substr(HOLDING_STEP_NAME,2,1)='9'
   and substr(HOLDING_STEP_NAME,1,1)<>'0'
   and substr(HOLDING_STEP_NAME,4,1)='0'
    and substr(HOLDING_STEP_NAME,5,1)='_'
   
   order by t1.shop,T2.HOLDING_STEP_NAME


";
        ds_temp1 = func.get_dataSet_access(sql, conn1);


        //**************針對 Title 呈現  Status  end *********//

        WebClient w = new WebClient();
        w.Encoding = Encoding.GetEncoding("big5");
        string strHTML = w.DownloadString("http://10.56.131.22/E-FAB_dotnet/epaper/ARRAY/sample/ary_hold_lot_fortd.aspx");
        //string strHTML1 = w.DownloadString("http://t1cimweb01/E-FAB_dotnet/epaper/ARRAY/sample/Array_scrap_detail.aspx?area=T1");
        string title = "[CIM 電子報系統] " + DateTime.Now.ToString("yyyy/MM/dd") + " Array HoldLotDetail For TD 【共 " + ds_temp1.Tables[0].Rows.Count.ToString() + "  筆資料 】 ";
        //string title1 = "[CIM 電子報系統] " + DateTime.Now.ToString("yyyy/MM/dd") + " T1Array ScrapDetail 【共 " + ds_temp2.Tables[0].Rows[0][0] + " 片資料 】 ";

        //SendEmail("Alarm_Server@innolux.com.tw", "oscar.hsieh@inl,bunny.su@inl", title, strHTML, "");//

        ArrayList maillist = new ArrayList();
        maillist = func.FileToArray(Server.MapPath("..\\") + "\\maillist\\Array_HoldLot_detail_for_td.txt");
        //maillist[0] = "oscar.hsieh@chimei-innolux.com";


        //ArrayList maillist1 = new ArrayList();
        //maillist1 = func.FileToArray(Server.MapPath("..\\") + "\\maillist\\T1Array_Scrap_detail.txt");
        // maillist1[0] = "oscar.hsieh@chimei-innolux.com";
        SendEmail("cim.alarm@innolux.com", maillist[0].ToString(), title, strHTML, "");//
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
