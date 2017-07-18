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



public partial class Holiday_inout_SMS_NEW : System.Web.UI.Page
{
    IS.util.special sp = new IS.util.special();
    //file f = new file();
    StreamWriter sw;
    FileInfo fi;
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE"];
        string conn_cel = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];

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
    #region Initial data
    string t1a_input = "0";
    string t1a_mtd_input = "0";
    string t1a_output = "0";
    string t1a_mtd_output = "0";
    string t1a_n_cst = "0";

    string t1c_input = "0";
    string t1c_mtd_input = "0";
    string t1c_output = "0";
    string t1c_mtd_output = "0";
    string t1c_n_cst = "0";
    string t1c_w_cst = "0";

    string t0c_input = "0";
    string t0c_mtd_input = "0";
    string t0c_output = "0";
    string t0c_mtd_output = "0";


    string t1f_input = "0";
    string t1f_mtd_input = "0";
    string t1f_output = "0";
    string t1f_mtd_output = "0";
    string t1f_n_cst = "0";
    string t1f_w_cst = "0";
    string t1f_array_wip = "0";
    string t1f_cf_wip = "0";
       
    #endregion
    
    
    
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
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在這裡放置使用者程式碼以初始化網頁

        if (!IsPostBack)
        {
            alarm_format.trx_id = "1";
            alarm_format.type_id = "1";
            alarm_format.fab_id = "T1ARRAY";
            alarm_format.sys_type = "WeekendReport";
            alarm_format.eq_id = "REPORT";
            alarm_format.alarm_id = "MSG";
            alarm_format.alarm_text = "T1 Alarm Server Test";
            alarm_format.mail_contenttype = "T";
            alarm_format.alarm_comment = "T1 Alarm Server Test Test";
            alarm_format.pc_ip = "1";
            alarm_format.pc_name = "1";
            alarm_format.operator1 = "1";
            alarm_format.issue_date = "1";


        }

        StreamWriter sw_oscar;



        // 在這裡放置使用者程式碼以初始化網頁
        //FileInfo fi ;
        DirectoryInfo di;
        int now_hour = Convert.ToInt32(DateTime.Now.ToString("HH"));//抓取執行當下小時
        int now_min = Convert.ToInt32(DateTime.Now.ToString("mm"));//抓取執行當下分鐘

        //di = new DirectoryInfo(Server.MapPath("..\\") + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd")); //DateTime.Now.ToString("yyyyMMdd")		

        fi = new FileInfo(Server.MapPath("..\\") + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log");


        //if (!di.Exists)
        //{
        //    di.Create();
        //}

        //如果檔案存在則開啟覆寫，如果不存在則建立新的檔案
        //StreamWriter sw;
        if (fi.Exists == true)
        {
            sw = File.AppendText(Server.MapPath("..\\") + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log");
        }
        else
        {
            sw = fi.CreateText();
        }

        sw.WriteLine("Create log file");
        sw.WriteLine(DateTime.Now.ToString("u") + "Holiday_inout_SMS Program Start");

        string t0a = "T0A:", t1a = "T1A:", t0c = "T0C:", t1c = "T1C:", t1f = "T1F:";
        string s = "";
       
        int i = 0;

        // 1row
        string sql_t1a = @"select shop, in_qty, mtd_in_qty, out_qty, mtd_out_qty, empty_cst from holiday_sms_ary";

        // 2row
        string sql_t0t1_cel = @"select shop, in_qty, mtd_in_qty, out_qty, mtd_out_qty, n_empty_cst, w_empty_cst from holiday_sms_cel
order by shop";
        
        // 1row
        string sql_t1f = @"select shop, in_qty, mtd_in_qty, out_qty, mtd_out_qty, empty_cst, ary_cellbank, cf_cellbank from holiday_sms_cf";


        #region Filled data

        func.Holiday_SMS holiday_sms_data = new func.Holiday_SMS();

        DataSet ds_t1a = new DataSet();

        DataSet ds_t1c = new DataSet();

        DataSet ds_t1f=new DataSet();

        ds_t1a = func.get_dataSet_access(sql_t1a, conn_cel);

        holiday_sms_data.t1a_input = ds_t1a.Tables[0].Rows[0]["in_qty"].ToString();
        holiday_sms_data.t1a_mtd_input=ds_t1a.Tables[0].Rows[0]["mtd_in_qty"].ToString();
        holiday_sms_data.t1a_output = ds_t1a.Tables[0].Rows[0]["out_qty"].ToString();
        holiday_sms_data.t1a_mtd_output = ds_t1a.Tables[0].Rows[0]["mtd_out_qty"].ToString();
        holiday_sms_data.t1a_n_cst = ds_t1a.Tables[0].Rows[0]["empty_cst"].ToString();

        ds_t1c = func.get_dataSet_access(sql_t0t1_cel, conn_cel);

        holiday_sms_data.t1c_input = ds_t1c.Tables[0].Rows[1]["in_qty"].ToString();
        holiday_sms_data.t1c_mtd_input = ds_t1c.Tables[0].Rows[1]["mtd_in_qty"].ToString();
        holiday_sms_data.t1c_output = ds_t1c.Tables[0].Rows[1]["out_qty"].ToString();
        holiday_sms_data.t1c_mtd_output = ds_t1c.Tables[0].Rows[1]["mtd_out_qty"].ToString();
        holiday_sms_data.t1c_n_cst = ds_t1c.Tables[0].Rows[1]["n_empty_cst"].ToString();
        holiday_sms_data.t1c_w_cst = ds_t1c.Tables[0].Rows[1]["w_empty_cst"].ToString();

        holiday_sms_data.t0c_input = ds_t1c.Tables[0].Rows[0]["in_qty"].ToString();
        holiday_sms_data.t0c_mtd_input = ds_t1c.Tables[0].Rows[0]["mtd_in_qty"].ToString();
        holiday_sms_data.t0c_output = ds_t1c.Tables[0].Rows[0]["out_qty"].ToString();
        holiday_sms_data.t0c_mtd_output = ds_t1c.Tables[0].Rows[0]["mtd_out_qty"].ToString();
        
        ds_t1f = func.get_dataSet_access(sql_t1f, conn_cel);

        holiday_sms_data.t1f_input = ds_t1f.Tables[0].Rows[0]["in_qty"].ToString();
        holiday_sms_data.t1f_mtd_input = ds_t1f.Tables[0].Rows[0]["mtd_in_qty"].ToString();
        holiday_sms_data.t1f_output = ds_t1f.Tables[0].Rows[0]["out_qty"].ToString();
        holiday_sms_data.t1f_mtd_output = ds_t1f.Tables[0].Rows[0]["mtd_out_qty"].ToString();
        holiday_sms_data.t1f_w_cst = ds_t1f.Tables[0].Rows[0]["empty_cst"].ToString();
        holiday_sms_data.t1f_array_wip = ds_t1f.Tables[0].Rows[0]["ary_cellbank"].ToString();
        holiday_sms_data.t1f_cf_wip = ds_t1f.Tables[0].Rows[0]["cf_cellbank"].ToString();




        #endregion


       

        // T1ARRAY 6 ROW

        t1a += holiday_sms_data.t1a_input + "," + holiday_sms_data.t1a_mtd_input + "," + holiday_sms_data.t1a_output + "," + holiday_sms_data.t1a_mtd_output + "," + holiday_sms_data.t1a_n_cst + "," + holiday_sms_data.t1f_array_wip;

        // T1CELL  6 ROW
        t1c += holiday_sms_data.t1c_input + "," + holiday_sms_data.t1c_mtd_input + "," + holiday_sms_data.t1c_output + "," + holiday_sms_data.t1c_mtd_output + "," + holiday_sms_data.t1c_n_cst + "," + holiday_sms_data.t1c_w_cst;

        // T0CELL  4 ROW
        t0c += holiday_sms_data.t0c_input + "," + holiday_sms_data.t0c_mtd_input + "," + holiday_sms_data.t0c_output + "," + holiday_sms_data.t0c_mtd_output;

        // T1CF  6 ROW
        t1f += holiday_sms_data.t1f_input + "," + holiday_sms_data.t1f_mtd_input + "," + holiday_sms_data.t1f_output + "," + holiday_sms_data.t1f_mtd_output + "," + holiday_sms_data.t1f_w_cst + "," + holiday_sms_data.t1f_cf_wip;





      

        //假日簡訊
        s = t1a + t1c + t0c+ t1f;
         
         #region PURGE HIS DATA


        string purge_SMS_HIS = @"delete holiday_sms_his t
 where t.shiftdate<=to_char(sysdate-90,'yyyyMMdd')
       
        ";
        purge_SMS_HIS = string.Format(purge_SMS_HIS, s);
        func.get_sql_execute(purge_SMS_HIS, conn_cel);



        #endregion

        string insert_SMS_HIS = @"insert into holiday_sms_his
  (shiftdate, message, dttm)
values
(to_char(sysdate-1,'yyyyMMdd'), '{0}', to_char(sysdate,'yyyyMMddHH24MISS'))
       
        ";
        insert_SMS_HIS = string.Format(insert_SMS_HIS, s);
        func.get_sql_execute(insert_SMS_HIS, conn_cel);

        string s1 = "0";

        bool saturdaycheck = false;
        bool sundaycheck = false;
        if (DateTime.Today.ToString("yyyyMMdd") == "20090124")
        {
            saturdaycheck = true;
        }
        if (DateTime.Today.ToString("yyyyMMdd") == "20090125")
        {
            sundaycheck = true;
        }

        //s1 = db.Execute_Scalar("select count(*) from eis_holiday_maintain t where holiday_dttm='" + DateTime.Today.ToString("yyyy/MM/dd") + "'", 2);
        DataSet ds_temp=new DataSet();

        ds_temp = func.get_dataSet_access("select count(*) from eis_holiday_maintain t where holiday_dttm='" + DateTime.Today.ToString("yyyy/MM/dd") + "'", conn_cel);
        s1 = ds_temp.Tables[0].Rows[0][0].ToString();

        //if(1==1)
        if ((DateTime.Today.DayOfWeek == DayOfWeek.Saturday && saturdaycheck != true) || (DateTime.Today.DayOfWeek == DayOfWeek.Sunday && sundaycheck != true) || s1.Equals("1"))
        			if(DateTime.Today.DayOfWeek==DayOfWeek.Saturday || DateTime.Today.DayOfWeek==DayOfWeek.Sunday || s1.Equals("1"))
        {
           // create_xml(s, 0, "REPORT");//SEND 假日簡訊

            string alarm_text = s;
            alarm_format.alarm_text = alarm_text;
            alarm_format.alarm_comment = alarm_text;
         
           this.Alarm_create_xml(alarm_format, "Holiday", "Holiday_Inout_SMS");
           sw.WriteLine("SMS log finish");
           sw.WriteLine("");

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

        sw.WriteLine(DateTime.Now.ToString("u") + "Holiday_Inout_SMS Program End");
        sw.WriteLine("");

        sw.Close();

        func.delete_log_dir(Server.MapPath(".") + "\\File\\", "*.*", -60);
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);

        //func.delete_log_dir(Server.MapPath("..\\") + "\\LOG\\", -60);
        //DeleteLogFile("");

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
     
    }
}
