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
using IS.util;
using Innolux.Portal.CommonFunction;
using System.Text;

public partial class Alarm_Array_test_repeat_monitor : System.Web.UI.Page
{
    IS.util.special sp = new IS.util.special();
    file f = new file();
    StreamWriter sw;
    FileInfo fi;
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE"];

    //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_MIS"];
    func fc = new func();

    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";

    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();


    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string SaveLocation = "";
    Int32 counter_oscar = 0;
    func xmlw = new func();
    func.alarm_format alarm_format = new func.alarm_format();

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            alarm_format.trx_id = "1";
            alarm_format.type_id = "1";
            alarm_format.fab_id = "T1ARRAY";
            alarm_format.sys_type = "ARRAY_TEST";
            alarm_format.eq_id = "ARRAY_TEST";
            alarm_format.alarm_id = "1111";
            alarm_format.alarm_text = "T1 Alarm Server Test";
            alarm_format.mail_contenttype = "T";
            alarm_format.alarm_comment = "T1 Alarm Server Test Test";
            alarm_format.pc_ip = "1";
            alarm_format.pc_name = "1";
            alarm_format.operator1 = "1";
            alarm_format.issue_date = "1";


        }

        StreamWriter sw_oscar;



        //sw.Close();

        //sw.
        //*/			


        //設定 INITIAL 

        //string[] condition1 = { "1150", "1250", "1350", "1450", "1650" };
        //string[] condition2 = { "1138B", "1238B", "1338B", "1438B", "1638B" };


        //*** 檢查 1150  1138B
        Array_Test_Repeat(Convert.ToInt32(today_hour));

      


       // DeleteLogFile("");

       // Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
     

        func.delete_log_dir(Server.MapPath(".") + "\\File\\", "*.*", -60);
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);

        //func.delete_log_dir(Server.MapPath("..\\") + "\\LOG\\", -60);
        //DeleteLogFile("");

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
    }

    private void Array_Test_Repeat(int now_hour)
    {
        
         func.write_log("Array_test_repeat log ", Server.MapPath("..\\") + "\\LOG\\", "log");
        
        string sql_array_test = "";
        sql_array_test = sql_array_test + "select count(*) as count1 from (                                          ";
        sql_array_test = sql_array_test + "                                                                ";
        sql_array_test = sql_array_test + "select  t.lot_id,count(*) as count1                             ";
        sql_array_test = sql_array_test + "from innrpt.move_history t                                      ";
        sql_array_test = sql_array_test + "where t.shift_date >=  to_char(sysdate-3-7/24 ,'yyyymmdd')      ";
        sql_array_test = sql_array_test + "and t.eqp_id like '1ATES%'                                      ";
        sql_array_test = sql_array_test + "and t.lot_id in (                                               ";
        sql_array_test = sql_array_test + "select h.lot_id from innrpt.move_history h                      ";
        sql_array_test = sql_array_test + "where h.shift_date =  to_char(sysdate-8/24 ,'yyyymmdd')         ";
        sql_array_test = sql_array_test + "and h.eqp_id like '1ATES%'                                      ";
        sql_array_test = sql_array_test + ")                                                               ";
        sql_array_test = sql_array_test + "group by t.lot_id                                               ";
        sql_array_test = sql_array_test + "                                                                ";
        sql_array_test = sql_array_test + "                                                                ";
        sql_array_test = sql_array_test + ")t1 where t1.count1>1                                           ";
        sql_array_test = sql_array_test + "union all                                                       ";
        sql_array_test = sql_array_test + "                                                                ";
        sql_array_test = sql_array_test + "select count(*) from (                                          ";
        sql_array_test = sql_array_test + "                                                                ";
        sql_array_test = sql_array_test + "select  t.lot_id,count(*) as count1                             ";
        sql_array_test = sql_array_test + "from innrpt.move_history t                                      ";
        sql_array_test = sql_array_test + "where t.shift_date >=  to_char(sysdate-3-7/24 ,'yyyymmdd')      ";
        sql_array_test = sql_array_test + "and t.eqp_id like '1ATES%'                                      ";
        sql_array_test = sql_array_test + "and t.lot_id in (                                               ";
        sql_array_test = sql_array_test + "select h.lot_id from innrpt.move_history h                      ";
        sql_array_test = sql_array_test + "where h.shift_date =  to_char(sysdate-7/24 ,'yyyymmdd')         ";
        sql_array_test = sql_array_test + "and h.eqp_id like '1ATES%'			                 ";
        sql_array_test = sql_array_test + ")			                                         ";
        sql_array_test = sql_array_test + "group by t.lot_id			                         ";
        sql_array_test = sql_array_test + "                                                                ";
        sql_array_test = sql_array_test + "                                                                ";
        sql_array_test = sql_array_test + ")t1 where t1.count1>1			                         ";




        DataSet ds_temp;

        ds_temp = func.get_dataSet_access(sql_array_test, conn);
        int abcd0 = Convert.ToInt16(ds_temp.Tables[0].Rows[0]["count1"].ToString());
        int efgh0 = Convert.ToInt16(ds_temp.Tables[0].Rows[1]["count1"].ToString());
        //if(1==1)
        if (Convert.ToInt16(ds_temp.Tables[0].Rows[0]["count1"].ToString()) < 7 && Convert.ToInt16(ds_temp.Tables[0].Rows[1]["count1"].ToString()) >= 7)
        {

            string alarm_text = " Test Area retest lot over 7 ; Test Area retest lot over 7  ";
            //string alarm_text = "1150 step less 250Sub 1138B greater 150Sub";
            alarm_format.alarm_text = alarm_text;
            alarm_format.alarm_comment = alarm_text;


            //由時間 去判斷 發送的 event  
            // 00~09    day1
            // 09~19    day2
            // 09~24    day3
            // 19~24    day4
            // 非 09~19 day5  call celphone
            //if (1 == 1)
            //if (now_hour >= 0 && now_hour < 9)
            //{
            //    this.create_xml(alarm_text, true, "1150", "ARRAY_PH_WIP_DAY1");
            //    sw.WriteLine("1150 log finish");
            //    sw.WriteLine("");

            //}

            // 判斷日期 是否為 六(7)日(1)  
            string sql_getdate = " select to_char(sysdate,'D') as date_num from dual ";
            DataSet ds_temp_date = new DataSet();
            ds_temp_date = func.get_dataSet_access(sql_getdate, conn);
            int abcde = Convert.ToInt16(ds_temp_date.Tables[0].Rows[0]["date_num"].ToString());

            if (abcde == 1 || abcde == 7)
            {
                //this.create_xml(alarm_text, true, "1920", "ARRAY_TEST_RETEST_DAY5");
                this.Alarm_create_xml(alarm_format, "Sys", "Array_Test_Repeat");
                sw.WriteLine("Array_Test_Repeat 1920 log finish");
                sw.WriteLine("");

            }

            else
            {

                // if (1 == 1)
                if (now_hour >= 9 && now_hour < 19)
                {
                    //this.create_xml(alarm_text, true, "1920", "ARRAY_TEST_RETEST_DAY2");
                    this.Alarm_create_xml(alarm_format, "Sys", "Array_Test_Repeat");
                    sw.WriteLine("Array_Test_Repeat 1920 log finish");
                    sw.WriteLine("");

                }

                else
                {
                    //this.create_xml(alarm_text, true, "1920", "ARRAY_TEST_RETEST_DAY5");
                    this.Alarm_create_xml(alarm_format, "Sys", "Array_Test_Repeat");
                    sw.WriteLine("Array_Test_Repeat 1920 log finish");
                    sw.WriteLine("");

                }

            }








        }

        // int bbb =Convert.ToInt16(ds_T0A.Tables[1].Rows[0]["count1"].ToString());
        if (Convert.ToInt16(ds_temp.Tables[0].Rows[0]["count1"].ToString()) >= 7 && (Convert.ToInt16(ds_temp.Tables[0].Rows[0]["count1"].ToString()) < Convert.ToInt16(ds_temp.Tables[0].Rows[1]["count1"].ToString())) && Convert.ToInt16(ds_temp.Tables[0].Rows[1]["count1"].ToString()) >= 7)
        {

            string alarm_text = " Test Area retest lot over 7 ; Test Area retest lot over 7 ";
            alarm_format.alarm_text = alarm_text;
            alarm_format.alarm_comment = alarm_text;


            //由時間 去判斷 發送的 event  
            // 00~09    day1
            // 09~19    day2
            // 09~24    day3
            // 19~24    day4
            // 非 09~19 day5  call celphone
            //if (1 == 1)
            //if (now_hour >= 0 && now_hour < 9)
            //{
            //    this.create_xml(alarm_text, true, "1150", "ARRAY_PH_WIP_DAY1");
            //    sw.WriteLine("1150 log finish");
            //    sw.WriteLine("");

            //}

            // 判斷日期 是否為 六(7)日(1)  
            string sql_getdate = "select to_char(sysdate,'D') as date_num from dual ";
            DataSet ds_getdate;
            ds_getdate = func.get_dataSet_access(sql_getdate, conn);
            int abcde = Convert.ToInt16(ds_getdate.Tables[0].Rows[0]["date_num"].ToString());
            //if(1==1) 
            if (abcde == 1 || abcde == 7)
            {
                //this.create_xml(alarm_text, true, "1920", "ARRAY_TEST_RETEST_DAY5");
                //sw.WriteLine("1920 log finish");
                //sw.WriteLine("");
                this.Alarm_create_xml(alarm_format, "Sys", "Array_Test_Repeat");
                sw.WriteLine("Array_Test_Repeat 1920 log finish");
                sw.WriteLine("");


            }

            else
            {

                //if (1 == 1)
                if (now_hour >= 9 && now_hour < 19)
                {
                    //this.create_xml(alarm_text, true, "1920", "ARRAY_TEST_RETEST_DAY2");
                    //sw.WriteLine("1920 log finish");
                    //sw.WriteLine("");
                    this.Alarm_create_xml(alarm_format, "Sys", "Array_Test_Repeat");
                    sw.WriteLine("Array_Test_Repeat 1920 log finish");
                    sw.WriteLine("");

                }

                else
                {
                    //this.create_xml(alarm_text, true, "1920", "ARRAY_TEST_RETEST_DAY5");
                    //sw.WriteLine("1920 log finish");
                    // sw.WriteLine("");
                    this.Alarm_create_xml(alarm_format, "Sys", "Array_Test_Repeat");
                    sw.WriteLine("Array_Test_Repeat 1920 log finish");
                    sw.WriteLine("");


                }

            }








        }
    }

    //this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
    public void Alarm_create_xml(func.alarm_format alarm_format, string sys_id, string inxml_file_name)
    {
        DataSet ds_insertDB = new DataSet();
        string sysid = sys_id;
        string xml_file_name = "Sys";
        ArrayList element = new ArrayList();
        ArrayList element_text = new ArrayList();
        StreamWriter sw_oscar;
        System.Text.Encoding encode = System.Text.Encoding.GetEncoding("big5");
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

        string xml_content = @"<?xml version=""1.0"" encoding=""big5""?><transaction><trx_id>AUTOREPORT</trx_id><type_id>1</type_id><fab_id>{0}</fab_id><sys_type>{1}</sys_type><eq_id>{2}</eq_id><alarm_id>{3}</alarm_id><alarm_text>{4}</alarm_text><mail_contenttype>T</mail_contenttype><alarm_comment value = ""{5}"" /><pc_ip>172.20.7.120</pc_ip><pc_name>AMS01</pc_name><operator>AMS01</operator><issue_date>20110804104843</issue_date></transaction>";


        xml_content = string.Format(xml_content, alarm_format.fab_id, alarm_format.sys_type, alarm_format.eq_id, alarm_format.alarm_id, alarm_format.alarm_text, alarm_format.alarm_comment);

        sw_oscar.WriteLine(xml_content);

        sw_oscar.Close();
        //Upload("at.txt", "172.16.12.122", "anonymous", "");
        func.Upload(SaveLocation, "172.16.12.124", "anonymous", "");


        counter_oscar++;
        //System.Text.Encoding encode = System.Text.Encoding.GetEncoding("big5"); 
        //element.Add("trx_id");

        //element_text.Add(alarm_format.trx_id);

        //element.Add("type_id");

        //element_text.Add(alarm_format.type_id);


        //element.Add("fab_id");

        //element_text.Add(alarm_format.fab_id);

        //element.Add("sys_type");
        //element_text.Add(alarm_format.sys_type);

        //element.Add("eq_id");
        //element_text.Add(alarm_format.eq_id);

        //element.Add("alarm_id");
        //element_text.Add(alarm_format.alarm_id);

        //element.Add("alarm_text");
        //element_text.Add(alarm_format.alarm_text);

        //element.Add("mail_contenttype");
        //element_text.Add(alarm_format.mail_contenttype);

        //element.Add("alarm_comment");
        //element_text.Add(alarm_format.alarm_comment);

        //element.Add("pc_ip");
        //element_text.Add(alarm_format.pc_ip);

        //element.Add("pc_name");
        //element_text.Add(alarm_format.pc_name);

        //element.Add("operator");
        //element_text.Add(alarm_format.operator1);

        //element.Add("issue_date");
        //element_text.Add(alarm_format.issue_date);




        //xml_file_name = sysid + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + "_" + inxml_file_name + ".xml";

        //xmlw.Create_Alarm_xml(Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "_ALARM_" + DateTime.Now.Hour.ToString(), xml_file_name, element, element_text);


        //// strClientIP = Request.ServerVariables["remote_host"].ToString(); 




        //SaveLocation = Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "_ALARM_" + DateTime.Now.Hour.ToString() + "\\" + xml_file_name;





    }//end of create_xml

    public void create_xml(string s, bool incontrol, string inxml_file_name, string sys_id)
    {
        DataSet ds_insertDB = new DataSet();
        string sysid = sys_id, xml_file_name = "";
        ArrayList element = new ArrayList();
        ArrayList element_text = new ArrayList();

        DirectoryInfo di = new DirectoryInfo(Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "_ALCS_" + DateTime.Now.Hour.ToString());
        if (!di.Exists)
        {
            di.Create();
        }

        /*
        if( fi.Exists == true )
        {
            sw = File.AppendText(Server.MapPath(".") + "\\log\\" +  DateTime.Now.ToString("yyyyMMdd") + "\\" + DateTime.Now.ToString("yyyyMMdd") +".log");
        }
        //*/

        file xmlw = new file();

        element.Add("EVENTID");
        element_text.Add("AlarmReport");
        element.Add("SYSTEMID");
        element_text.Add(sysid);
        element.Add("EQPID");
        element_text.Add(inxml_file_name);
        element.Add("ALARMID");
        element_text.Add("MSG");
        element.Add("ALARMTEXT");
        element_text.Add(s);

        xml_file_name = sysid + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + inxml_file_name + ".xml";

        // xmlw.Alarm_create_xml(Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "_ALCS_" + DateTime.Now.Hour.ToString(), xml_file_name, element, element_text);
        if (incontrol == true)
        {
            FTPFactory ff = new FTPFactory();
            ff.setDebug(true);
            ff.setRemoteHost("172.16.12.78");
            ff.setRemoteUser("CIMFTP");
            ff.setRemotePass("ALCS_cim13579");
            ff.login();

            try
            {

                ff.upload(Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "_ALCS_" + DateTime.Now.Hour.ToString() + "/" + xml_file_name);
                sp.Send_mail("File Upload Success", "CIM CENTRAL MAIL SYSTEM<cimalarm@innolux.com.tw>", "oscar.hsieh@innolux.com.tw", "", "[ARRAY_PH_WIP_Status]" + inxml_file_name + "-XML File Upload ALCS Success", 0, null);
                sw.WriteLine(DateTime.Now.ToString("u") + " " + inxml_file_name + " File Upload Success");
            }
            catch (Exception ex)
            {
                sp.Send_mail("File Upload Fail" + ex.Message, "CIM CENTRAL MAIL SYSTEM<cimalarm@innolux.com.tw>", "oscar.hsieh@innolux.com.tw", "[ARRAY_PH_WIP_Status]" + inxml_file_name + "-XML File Upload ALCS Fail", 0, null);
                sw.WriteLine(DateTime.Now.ToString("u") + " " + inxml_file_name + " File Upload Fail");
            }
        }





    }//end of create_xml
}
