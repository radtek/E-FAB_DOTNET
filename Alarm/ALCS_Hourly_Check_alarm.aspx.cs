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
using System.Xml;

public partial class Alarm_ALCS_Hourly_Check_alarm : System.Web.UI.Page
{
    IS.util.special sp = new IS.util.special();
    //file f = new file();
    StreamWriter sw;
    FileInfo fi;
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ALCS"];
    string conn_cel = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];
    string conn_oee = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_OLE"];

    //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_MIS"];
    func fc = new func();

    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_stm = "";
    string sql_oee_db_chk = "";

    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();



    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string ThreeDayAgo = DateTime.Now.AddDays(-3).ToString("yyyyMMdd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");

    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string last_hour = DateTime.Now.AddDays(-1/24).ToString("yyyyMMddHH");
    string last_twohour = DateTime.Now.AddDays(-2/24).ToString("yyyyMMddHH");
    string last_Threehour = DateTime.Now.AddDays(-3 / 24).ToString("yyyyMMddHH");
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
        // 在這裡放置使用者程式碼以初始化網頁 20120210

        if (!IsPostBack)
        {
            alarm_format.trx_id = "1";
            alarm_format.type_id = "1";
            alarm_format.fab_id = "TFTPIA";
            alarm_format.sys_type = "ALCS";
            alarm_format.eq_id = "ALCS_ISCHK";
            alarm_format.alarm_id = "HOURLY_CHK";
            alarm_format.alarm_text = "ALCS LAST HOUR NO DATA";
            alarm_format.mail_contenttype = "T";
            alarm_format.alarm_comment = "ALCS LAST HOUR NO DATA";
            alarm_format.pc_ip = "1";
            alarm_format.pc_name = "1";
            alarm_format.operator1 = "1";
            alarm_format.issue_date = "1";


        }

        StreamWriter sw_oscar;



        // 在這裡放置使用者程式碼以初始化網頁
        //FileInfo fi ;
        //DirectoryInfo di;
        //int now_hour = Convert.ToInt32(DateTime.Now.ToString("HH"));//抓取執行當下小時
        //int now_min = Convert.ToInt32(DateTime.Now.ToString("mm"));//抓取執行當下分鐘

        //di = new DirectoryInfo(Server.MapPath("..\\") + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd")); //DateTime.Now.ToString("yyyyMMdd")		

        //fi = new FileInfo(Server.MapPath("..\\") + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log");
       // func.write_log("ALCS_Hourly_Check_alarm ", Server.MapPath("..\\") + "\\LOG\\", "log");

        //if (!di.Exists)
        //{
        //    di.Create();
        //}

        //如果檔案存在則開啟覆寫，如果不存在則建立新的檔案
        //StreamWriter sw;
        //if (fi.Exists == true)
        //{
        //    sw = File.AppendText(Server.MapPath("..\\") + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log");
        //}
        //else
        //{
        //    sw = fi.CreateText();
        //}

        //sw.WriteLine("Create log file");
        //sw.WriteLine(DateTime.Now.ToString("u") + "OEE_IS_Hourly_Check_alarm Program Start");

        //string[] shop_area ={ "T0ARRAY", "T0CELL", "T1ARRAY", "T1CELL", "T1CF" };


        ALCS_Hourly_Check_Alarm();

        func.write_log("ALCS_Hourly_Check_Alarm log", Server.MapPath("..\\") + "\\LOG\\", "log");

          

        T1OEEDB_CHK();

        

        //sw.WriteLine(DateTime.Now.ToString("u") + "ALCS_Hourly_Check_alarm Program End");
        //sw.WriteLine("");

        //sw.Close();

        // Delete ALCS DateTime DB Data  
        sql_temp2 = @"delete alarmlasttime t
 where to_char(t.dttm,'yyyyMMdd')<='{0}'";

        sql_temp2 = string.Format(sql_temp2, ThreeDayAgo);
        func.get_sql_execute(sql_temp2, conn);

        func.delete_log_dir(Server.MapPath(".") + "\\File\\", "*.*", -60);
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);

        //func.delete_log_dir(Server.MapPath("..\\") + "\\LOG\\", -60);
        //DeleteLogFile("");

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");

    }

    private void T1OEEDB_CHK()
    {

        func.write_log("T1OEEDB_CHK log ", Server.MapPath("..\\") + "\\LOG\\", "log");
        
        sql_oee_db_chk = @"


         select ot1.* from (

select t.line,
       max(substr(t.lasttriggerdatetime, 0, 19)) as max_time,
       round((sysdate - to_date(max(substr(t.lasttriggerdatetime, 0, 19)),
                                'yyyy-MM-dd HH24:MI:ss')) * 24 * 60,
             2) as diff_min,
       case when  round((sysdate - to_date(max(substr(t.lasttriggerdatetime, 0, 19)),
                                'yyyy-MM-dd HH24:MI:ss')) * 24 * 60,
             2)>30 then 'NG' else 'OK' end as flag
  from empastatus t
 group by t.line
) ot1,(select * from empastatus_chk_conf t 
where t.turn_flag='Yes'  )ot2 
where ot1.line=ot2.line


";

        ds_temp3 = func.get_dataSet_access(sql_oee_db_chk, conn_oee);

        for (int i = 0; i <= ds_temp3.Tables[0].Rows.Count - 1; i++)
        {

            if (ds_temp3.Tables[0].Rows[i]["flag"].Equals("NG"))
            {

                string alarm_x = "T1OEE DB  " + ds_temp3.Tables[0].Rows[i]["line"].ToString() + " TIME OVER 30 MINS PLEASE CHECK";
                alarm_format.alarm_text = alarm_x;
                alarm_format.alarm_comment = alarm_x;

                alarm_format.fab_id = "T1ARRAY";
                alarm_format.sys_type = "OEE";
                alarm_format.eq_id = "DB";
                alarm_format.alarm_id = "CHK";

                this.Alarm_create_xml(alarm_format, "T1OEE_CHK", "T1OEE_DB_Hourly_Check_Alarm");

            }


        }

        func.write_log("T1OEEDB_CHK log End", Server.MapPath("..\\") + "\\LOG\\", "log");
    }

    private void ALCS_Hourly_Check_Alarm()
    {

        func.write_log("ALCS_Hourly_Check_Alarm log ", Server.MapPath("..\\") + "\\LOG\\", "log");
        
        
        sql_stm = @"
select max(to_char(t.dttm,'yyyyMMddHH24MISS'))as max_dttm,to_char(t.dttm,'yyyyMMddHH24')as dttm ,count(t.sn) as counter from alarmlasttime t
where t.dttm>sysdate-1/24
   
group by to_char(t.dttm,'yyyyMMddHH24')";



        DataSet ds;

        //ds = db.GetDataset(sql_stm, 2);
        ds = func.get_dataSet_access(sql_stm, conn);


        //if (1==1)
        if (ds.Tables[0].Rows.Count <= 0)
        {
            // create_xml(s, 0, "REPORT");//SEND 假日簡訊

            string alarm_text = "ALCS " + last_hour + " LAST HOUR NO DATA PLEASE CHECK";
            alarm_format.alarm_text = alarm_text;
            alarm_format.alarm_comment = alarm_text;

            this.Alarm_create_xml(alarm_format, "ALCS_CHK", "ALCS_Hourly_Check_Alarm");
            func.write_log("ALCS_Hourly_Check_Alarm log finish", Server.MapPath("..\\") + "\\LOG\\", "log");
            //sw.WriteLine("ALCS_Hourly_Check_Alarm log finish");
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
