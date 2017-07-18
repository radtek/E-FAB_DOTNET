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
using IS.util;
using System.IO;
using Innolux.Portal.CommonFunction;
using System.Xml;


public partial class Alarm_OEE_IS_Hourly_Check_alarm : System.Web.UI.Page
{
  
         IS.util.special sp = new IS.util.special();
    //file f = new file();
    StreamWriter sw;
    FileInfo fi;
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];
    string conn_cel = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];
    string conn_pds = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_PDS_OLE_STD"];
    string conn_oeegw1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_MIDGW1"];

    //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_MIS"];
    func fc = new func();

    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string sql_temp4 = "";
    string sql_temp5 = "";
    string sql_stm = "";

    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    DataSet ds_temp4 = new DataSet();
    DataSet ds_temp5 = new DataSet();


    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
  
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string last_hour = DateTime.Now.AddDays(-1/24).ToString("yyyyMMddHH");
    string last_twohour = DateTime.Now.AddDays(-2/24).ToString("yyyyMMddHH");
    string SaveLocation = "";
    Int32 counter_oscar = 0;
    func xmlw = new func();
    func.alarm_format alarm_format = new func.alarm_format();
    
    
    
    //special sp = new special();
   // file f = new file();

    public void Alarm_create_xml(func.alarm_format alarm_format, string sys_id, string inxml_file_name)
    {
        DataSet ds_insertDB = new DataSet();
        string sysid = sys_id;
        string xml_file_name = "Sys";
        ArrayList element = new ArrayList();
        ArrayList element_text = new ArrayList();
        StreamWriter sw_oscar;


        //System.Text.Encoding encode = System.Text.Encoding.GetEncoding("big5");
        //StringWriter stringWriter = new StringWriterWithEncoding(Encoding.UTF8);



        DirectoryInfo di_oscar = new DirectoryInfo(Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd")); //DateTime.Now.ToString("yyyyMMdd")		

        FileInfo fi_oscar = new FileInfo(Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + inxml_file_name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + counter_oscar.ToString() + ".xml");
        SaveLocation = Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + inxml_file_name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + counter_oscar.ToString() + ".xml";


        if (!di_oscar.Exists)
        {
            di_oscar.Create();
        }

        //如果檔案存在則開啟覆寫，如果不存在則建立新的檔案
        //StreamWriter sw;
        if (fi_oscar.Exists == true)
        {
            sw_oscar = File.AppendText(Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + inxml_file_name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + counter_oscar.ToString() + ".xml");
        }
        else
        {
            sw_oscar = fi_oscar.CreateText();
        }

        //string xml_content = @"<?xml version=""1.0"" encoding=""big5""  ?><transaction><trx_id>AUTOREPORT</trx_id><type_id>1</type_id><fab_id>{0}</fab_id><sys_type>{1}</sys_type><eq_id>{2}</eq_id><alarm_id>{3}</alarm_id><alarm_text>{4}</alarm_text><mail_contenttype>T</mail_contenttype><alarm_comment value = ""{5}"" /><pc_ip>172.20.7.120</pc_ip><pc_name>AMS01</pc_name><operator>AMS01</operator><issue_date>20110804104843</issue_date></transaction>";


        XmlDocument doc = new XmlDocument();
        //doc.LoadXml(Server.MapPath(".") + "\\ALCS_SAMPLE\\AlarmTestXML.xml");
        doc.LoadXml(@"<?xml version=""1.0"" encoding=""big5""?>
<transaction>
<trx_id>AUTOREPORT</trx_id>
<type_id>I</type_id>
<fab_id>T1ARRAY</fab_id>
<sys_type>ALM_SMS</sys_type>
<eq_id>SMS</eq_id>
<alarm_id>67</alarm_id>
<alarm_text>Alarm 點檢測試AAA</alarm_text>
<mail_contenttype>T</mail_contenttype>
<alarm_comment value=""Alarm 點檢測試AAA""/>
<pc_ip>1</pc_ip>
<pc_name>1</pc_name>
<operator>1</operator>
<issue_date>2012-06-19 11:00:53</issue_date>
</transaction>");
        //doc.SelectSingleNode("//fab_id").NodeType=
        doc.SelectSingleNode("//fab_id").ChildNodes[0].InnerText = alarm_format.fab_id;
        doc.SelectSingleNode("//sys_type").ChildNodes[0].InnerText = alarm_format.sys_type;
        doc.SelectSingleNode("//eq_id").ChildNodes[0].InnerText = alarm_format.eq_id;
        doc.SelectSingleNode("//alarm_id").ChildNodes[0].InnerText = alarm_format.alarm_id;
        //doc.selectSingleNode("//issue_date").nodeTypedValue = rvreceivetime;
        //doc.selectSingleNode("//alarm_text").nodeTypedValue = phone;
        //doc.selectSingleNode("//mail_contenttype").nodeTypedValue = MAIL;
        doc.SelectSingleNode("//alarm_text").ChildNodes[0].InnerText = alarm_format.alarm_text;
        doc.SelectSingleNode("//alarm_comment").Attributes.GetNamedItem("value").InnerText = alarm_format.alarm_comment;
        //Save the document to a file.

        string SaveLocation1 = Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + inxml_file_name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + counter_oscar.ToString() + "aa.xml";
        doc.Save(SaveLocation1);

        sw_oscar.Close();
        //Upload("at.txt", "172.16.12.122", "anonymous", "");
        func.Upload(SaveLocation1, "172.16.12.124", "anonymous", "");


        counter_oscar++;


    }
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在這裡放置使用者程式碼以初始化網頁

        if (!IsPostBack)
        {
            alarm_format.trx_id = "1";
            alarm_format.type_id = "1";
            alarm_format.fab_id = "TFTPIA";
            alarm_format.sys_type = "OEE";
            alarm_format.eq_id = "OEE_ISCHK";
            alarm_format.alarm_id = "HOURLY_CHK";
            alarm_format.alarm_text = "T1OEE IS LAST HOUR NO DATA";
            alarm_format.mail_contenttype = "T";
            alarm_format.alarm_comment = "T1OEE IS LAST HOUR NO DATA";
            alarm_format.pc_ip = "1";
            alarm_format.pc_name = "1";
            alarm_format.operator1 = "1";
            alarm_format.issue_date = "1";


        }

        func.write_log("OEE_IS_Hourly_Check_alarm ", Server.MapPath("..\\") + "\\LOG\\", "log");
        
        //sw.WriteLine("Create log file");
        //sw.WriteLine(DateTime.Now.ToString("u") + "OEE_IS_Hourly_Check_alarm Program Start");

        //OEE_IS_Hourly_Check_alarm();
        OEE_GW_CHK();
        OEE_GET_RPT_DATA();


        func.delete_log_dir(Server.MapPath(".") + "\\File\\", "*.*", -60);
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);

        //func.delete_log_dir(Server.MapPath("..\\") + "\\LOG\\", -60);
        //DeleteLogFile("");

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
     
    }

    private void OEE_GET_RPT_DATA()
    {
        sql_temp5 = @"
select 'ARY' as SHOP,
       t.procedurename,
       to_char(t.lastrunstarttime, 'yyyy/MM/dd HH24:MI:ss') as lastrunstarttime,
       to_char(t.lastrunendtime, 'yyyy/MM/dd HH24:MI:ss') as lastrunendtime,
       t.lastdatatm,
       t.isrun,
       round((sysdate - t.lastrunstarttime) * 24, 1) as diff,
       round((sysdate - to_date(t.lastdatatm, 'yyyyMMdd HH24MISS')) * 24, 1) as diff2,
       case
         when round((sysdate - t.lastrunstarttime) * 24, 1) < 24 then
          'OK'
         else
          'NG'
       end as flag
  from mesoee_ary_txn.ldr_etl_cfg t

union all

select 'Cell' as SHOP,
       t1.procedurename,
       to_char(t1.lastrunstarttime, 'yyyy/MM/dd HH24:MI:ss') as lastrunstarttime,
       to_char(t1.lastrunendtime, 'yyyy/MM/dd HH24:MI:ss') as lastrunendtime,
       t1.lastdatatm,
       t1.isrun,
       round((sysdate - t1.lastrunstarttime) * 24, 1) as diff,
       round((sysdate - to_date(t1.lastdatatm, 'yyyyMMdd HH24MISS')) * 24,
             1) as diff2,
       case
         when round((sysdate - t1.lastrunstarttime) * 24, 1) < 24 then
          'OK'
         else
          'NG'
       end as flag
  from mesoee_tcl_txn.ldr_etl_cfg t1
 where t1.procedurename like '%ODS%'

union all

select 'CF' as SHOP,
       t2.procedurename,
       to_char(t2.lastrunstarttime, 'yyyy/MM/dd HH24:MI:ss') as lastrunstarttime,
       to_char(t2.lastrunendtime, 'yyyy/MM/dd HH24:MI:ss') as lastrunendtime,
       t2.lastdatatm,
       t2.isrun,
       round((sysdate - t2.lastrunstarttime) * 24, 1) as diff,
       round((sysdate - to_date(t2.lastdatatm, 'yyyyMMdd HH24MISS')) * 24,
             1) as diff2,
       case
         when round((sysdate - t2.lastrunstarttime) * 24, 1) < 24 then
          'OK'
         else
          'NG'
       end as flag
  from mesoee_t1f_txn.ldr_etl_cfg t2
 where t2.procedurename like '%ODS%'




";
        ds_temp5 = func.get_dataSet_access(sql_temp5, conn);

        for (int n = 0; n <= ds_temp5.Tables[0].Rows.Count - 1; n++)
        {

            if (ds_temp5.Tables[0].Rows[n]["FLAG"].ToString().Equals("NG"))
            {

                // create_xml(s, 0, "REPORT");//SEND 假日簡訊
                alarm_format.fab_id = "TFTPIA";
                alarm_format.sys_type = "OEE";
                alarm_format.eq_id = "OEE_ISCHK";
                alarm_format.alarm_id = "HOURLY_CHK";

                string alarm_text = "T1OEE GET RPT_DATA  SHOP:" + ds_temp5.Tables[0].Rows[n]["PROCEDURENAME"].ToString() + " OVER 12 小時,請確認!!!";
                alarm_format.alarm_text = alarm_text;
                alarm_format.alarm_comment = alarm_text;

                this.Alarm_create_xml(alarm_format, "OEE_ODS_RPT_CHK", "OEE_IS_ODS_RPT_Hourly_Check_Alarm");
                //func.write_log("OEE_GW SEND DATA CHECK log finish", Server.MapPath("..\\") + "\\LOG\\", "log");
                //sw.WriteLine("OEE_IS_Hourly_Check_Alarm log finish");
                //sw.WriteLine("");

                //create_xml(stn,0,"STN");//STN簡訊
                //create_xml(STN_CTP,0,"STN");//STN+CTP簡訊

            }




        }
    }

    private void OEE_GW_CHK()
    {
        sql_temp4 = @"

       select ot1.*,
       round((sysdate - to_date(substr(ot1.receivedtime, 0, 18),
                                'yyyy-MM-dd HH24:MI:ss')) * 24 * 60,
             2) diff_min,
       case when    round((sysdate - to_date(substr(ot1.receivedtime, 0, 18),
                                'yyyy-MM-dd HH24:MI:ss')) * 24 * 60,
             2)>30 then 'NG'
             else 'OK' end flag     
  from (
        
        select 'ARRAY' as shop, max(t.receivedtime) as receivedtime
          from ary_send_msg t
         where t.area = 'ARRAY'
        
        union
        
        select 'CELL' as shop, max(t.receivedtime) as receivedtime
          from cel_send_msg@mid12mid2.us.oracle.com t
         where t.area = 'CELL'
        
        union
        
        select 'CF' as shop, max(t.receivedtime) as receivedtime
          from cf_send_msg t
         where t.area = 'CF'
        
        ) ot1




";
        ds_temp4 = func.get_dataSet_access(sql_temp4, conn_oeegw1);

        for (int m = 0; m < ds_temp4.Tables[0].Rows.Count - 1; m++)
        {

            if (ds_temp4.Tables[0].Rows[m]["FLAG"].ToString().Equals("NG"))
            {

                // create_xml(s, 0, "REPORT");//SEND 假日簡訊
                alarm_format.fab_id = "TFTPIA";
                alarm_format.sys_type = "OEE";
                alarm_format.eq_id = "OEE_ISCHK";
                alarm_format.alarm_id = "HOURLY_CHK";

                string alarm_text = "T1OEE MIDGW SHOP:" + ds_temp4.Tables[0].Rows[m]["SHOP"].ToString() + " OVER 30 MINS ";
                alarm_format.alarm_text = alarm_text;
                alarm_format.alarm_comment = alarm_text;

                this.Alarm_create_xml(alarm_format, "OEE_CHK", "OEE_IS_Hourly_Check_Alarm");
                //func.write_log("OEE_GW SEND DATA CHECK log finish", Server.MapPath("..\\") + "\\LOG\\", "log");
                //sw.WriteLine("OEE_IS_Hourly_Check_Alarm log finish");
                //sw.WriteLine("");

                //create_xml(stn,0,"STN");//STN簡訊
                //create_xml(STN_CTP,0,"STN");//STN+CTP簡訊

            }




        }
    }

    private void OEE_IS_Hourly_Check_alarm()
    {
        string[] shop_area ={ "T0ARRAY", "T0CELL", "T1ARRAY", "T1CELL", "T1CF" };

        for (int i = 0; i <= shop_area.Length - 1; i++)
        {

            sql_stm = @"
select tb1.line,tb1.cutoffkey,count(tb1.line)as counter from empaidxsummhourly tb1 
where  tb1.cutoffkey=to_char(sysdate-1/24,'yyyyMMddHH24') and tb1.cutoffcycle='H'
      and tb1.line='{0}'

group by tb1.line,tb1.cutoffkey";

            sql_stm = string.Format(sql_stm, shop_area[i].ToString());

            DataSet ds;

            //ds = db.GetDataset(sql_stm, 2);
            ds = func.get_dataSet_access(sql_stm, conn);


            //if (1==1)
            if (ds.Tables[0].Rows.Count <= 0)
            {
                // create_xml(s, 0, "REPORT");//SEND 假日簡訊

                string alarm_text = "T1OEE SYSTEM SHOP  " + shop_area[i].ToString() + " " + last_hour + " LAST HOUR NO DATA PLEASE CHECK";
                alarm_format.alarm_text = alarm_text;
                alarm_format.alarm_comment = alarm_text;

                this.Alarm_create_xml(alarm_format, "OEE_CHK", "OEE_IS_Hourly_Check_Alarm");
                func.write_log("OEE_IS_Hourly_Check_Alarm log finish", Server.MapPath("..\\") + "\\LOG\\", "log");
                //sw.WriteLine("OEE_IS_Hourly_Check_Alarm log finish");
                //sw.WriteLine("");

                //create_xml(stn,0,"STN");//STN簡訊
                //create_xml(STN_CTP,0,"STN");//STN+CTP簡訊
            }
            else
            {
                // create_xml(s, 1, "REPORT");// NOT SEND

                //string alarm_text = s;
                // alarm_format.alarm_text = alarm_text;
                //alarm_format.alarm_comment = alarm_text;

                //this.Alarm_create_xml(alarm_format, "Holiday", "Holiday_Inout_SMS");
                //sw.WriteLine("SMS log finish");
                // sw.WriteLine("");
                //create_xml(stn,1,"STN");
                //create_xml(STN_CTP,1,"STN");
            }



        }
    }
    
}
