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

public partial class Alarm_t1newalarm_queue_chk : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ALCS_XLS"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_T1NEWALARM"];
    FileInfo fi;
    DirectoryInfo di;
    StreamWriter sw;
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_minus7 = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    DataTable dt = new DataTable();
    ArrayList arlist_temp1 = new ArrayList();
    func.alarm_format alarm_format = new func.alarm_format();
    Int32 counter_oscar = 0;
    string SaveLocation = "";
    string alarm_text = "";
    Boolean Check_flag = true;
    string delay_level_min = "20";  // mins
    string queue_level_count = "60"; // counter in 12 hrs
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            alarm_format.trx_id = "1";
            alarm_format.type_id = "1";
            alarm_format.fab_id = "T1ARRAY";
            alarm_format.sys_type = "ALM_SMS";
            alarm_format.eq_id = "SMS";
            alarm_format.alarm_id = "67";
            alarm_format.alarm_text = @"T1Alarm 點檢測試 KIDD";
            alarm_format.mail_contenttype = "T";
            alarm_format.alarm_comment = @"T1Alarm 點檢測試 KIDD";
            alarm_format.pc_ip = "1";
            alarm_format.pc_name = "1";
            alarm_format.operator1 = "1";
            alarm_format.issue_date = "1";
            

        }

        func.write_log("t1newalarm_queue_chk.  ", Server.MapPath("..\\") + "\\LOG\\", "log");

        // C:\Program Files\AMS\AMS.FAS\XML File\Queue



        chk_xml_file_num(@"D:\CIM-SE-RPT-WEB\E-FAB_dotnet\Alarm\File\20131003", "*.xml");
       



            StreamWriter sw_oscar;



            // 在這裡放置使用者程式碼以初始化網頁
            //FileInfo fi ;
            //DirectoryInfo di;
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
            sw.WriteLine(DateTime.Now.ToString("u") + " SERVER_XML_CHECK Program Start");

            alarm_format.alarm_text = today_detail + alarm_text;

            alarm_format.alarm_comment = today_detail + alarm_text;

            //this.Alarm_create_xml(alarm_format, "T1NewAlarm", " SERVER_SELF_CHECK_SMS");
            sw.WriteLine("SMS log finish");
            sw.WriteLine("");
            sw.WriteLine(DateTime.Now.ToString("u") + " SERVER_XML_CHECK Program End");
            sw.WriteLine("");

            sw.Close();

      

        func.delete_log_dir(Server.MapPath(".") + "\\File\\", "*.*", -60);
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);

        //func.delete_log_dir(Server.MapPath("..\\") + "\\LOG\\", -60);
        //DeleteLogFile("");

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");



    }


    public void chk_xml_file_num(string file_path, string file_type)
    {
        //DirectoryInfo dir = new DirectoryInfo(Server.MapPath(".") + "\\CF\\Save_file\\"); 
        DirectoryInfo dir = new DirectoryInfo(file_path);
        // FileInfo[] files = dir.GetFiles("*.xml"); 
        //FileInfo[] files = dir.GetFiles(file_type); 

        //Destination = @"http:\\10.56.131.22\LCM_EDAFILE\DfSender_LCM4\COMPRESSED_FILE";
        // Destination = @"Z:\";
        //Destination = @"\\10.56.195.215\Shipping\DfSender_LCM4\COMPRESSED_FILE";


        Int32 alarm_num = 20;


        FileInfo[] files = dir.GetFiles(file_type);


        if (files.Length >= alarm_num)
        {
            this.Alarm_create_xml(alarm_format, "T1NewAlarm", " SERVER_SELF_CHECK_QUEUE");
        }







    } 

      

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
            // sw_oscar = fi_oscar.CreateText();
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

        string SaveLocation1 = Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + inxml_file_name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + counter_oscar.ToString() + "_oscar.xml";
        doc.Save(SaveLocation1);




        //sw_oscar.Close();
        //Upload("at.txt", "172.16.12.122", "anonymous", "");
        func.Upload(SaveLocation1, "172.16.12.124", "anonymous", "");


        counter_oscar++;







    }
}
