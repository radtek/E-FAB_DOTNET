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
using System.Net.Mail;
public partial class Alarm_Holiday_inout_SMS_FOR_C2_R3 : System.Web.UI.Page
{
    IS.util.special sp = new IS.util.special();
    //file f = new file();
    StreamWriter sw;
    FileInfo fi;
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE"];
    string conn_ary = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_RPTDW"];
    string conn_cel = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];

    //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_MIS"];
    func fc = new func();

    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_lineyield = "";


    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    DataSet ds_temp4 = new DataSet();



    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
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
            alarm_format.fab_id = "T0ARRAY";
            alarm_format.sys_type = "WeekendReport2";
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
        sw.WriteLine(DateTime.Now.ToString("u") + "Holiday_inout_SMS_For_C2 R3 Program Start");



        string sql_stm = @"

select * from holiday_sms_t0ary t

";
       //ds = db.GetDataset(sql_stm, 2);
        ds_temp3 = func.get_dataSet_access(sql_stm, conn_cel);

        DataTable dt_x = new DataTable();

        dt_x = ds_temp3.Tables[0];

      

        //TFTIN   
        //TFTOUT  
        //TFTIN_MTD  
        //TFTOUT_MTD    
        //TPIN   
        //TPOUT  
        //TPIN_MTD  
        //TPOUT_MTD
     
        c2_format c2=new c2_format();

        DataRow dr = dt_x.NewRow();

        DataRow[] foundRows;



        foundRows = dt_x.Select("type='TFT'", "IN_QTY");

        c2.TFTIN = foundRows[0][1].ToString();


        foundRows = dt_x.Select("type='TFT'", "MTD_IN_QTY");


        c2.TFTIN_MTD = foundRows[0][2].ToString();

        foundRows = dt_x.Select("type='TFT'", "OUT_QTY");
        c2.TFTOUT = foundRows[0][3].ToString();

        foundRows = dt_x.Select("type='TFT'", "MTD_OUT_QTY");
        c2.TFTOUT_MTD = foundRows[0][4].ToString();
        
        //TP

        foundRows = dt_x.Select("type='TP'", "IN_QTY");

        c2.TPIN = foundRows[0][1].ToString();


        foundRows = dt_x.Select("type='TP'", "MTD_IN_QTY");


        c2.TPIN_MTD = foundRows[0][2].ToString();

        foundRows = dt_x.Select("type='TP'", "OUT_QTY");
        c2.TPOUT = foundRows[0][3].ToString();

        foundRows = dt_x.Select("type='TP'", "MTD_OUT_QTY");
        c2.TPOUT_MTD = foundRows[0][4].ToString();



        string sms_content = "C_2 TFT:" + c2.TFTIN + "," + c2.TFTIN_MTD + "," + c2.TFTOUT + "," + c2.TFTOUT_MTD + " TP:" + c2.TPIN + "," + c2.TPIN_MTD + "," + c2.TPOUT + "," + c2.TPOUT_MTD;
         
       
      

      

        //string sms_content = "C_2 " + ds_temp3.Tables[0].Rows[0]["MSG"].ToString() + " LY:" + LYTFTP + "/" + LYTFTE + "," + ds_temp3.Tables[0].Rows[1]["MSG"].ToString() + " LY:" + LYTPP + "/" + LYTPE;



        #region

        #endregion




        //假日簡訊


        //STN+F-CTP+B-CTP簡訊
        //string STN_CTP = stn + TP_FCTP + TP_BCTP;


        //將假日簡訊和STN簡訊合併發送(移除良率)
        //s = s + STN_CTP;

        //msgtable = sp.gethtml_2(ds.Tables[0]);
        //Response.Write( msgtable );
        //Response.End() ;   

        //OEE KPI
        string sql_OEE_KPI = "";
        #region
        ///舊版OEE，Mark by bunny 20090820
        #endregion


        string msgtable_OEE_KPI = "";




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
        DataSet ds_temp = new DataSet();

        ds_temp = func.get_dataSet_access("select count(*) from eis_holiday_maintain t where holiday_dttm='" + DateTime.Today.ToString("yyyy/MM/dd") + "'", conn_cel);
        s1 = ds_temp.Tables[0].Rows[0][0].ToString();

        //if(1==1)
        if ((DateTime.Today.DayOfWeek == DayOfWeek.Saturday && saturdaycheck != true) || (DateTime.Today.DayOfWeek == DayOfWeek.Sunday && sundaycheck != true) || s1.Equals("1"))
        //if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday || DateTime.Today.DayOfWeek == DayOfWeek.Sunday || s1.Equals("1"))
        {
            // create_xml(s, 0, "REPORT");//SEND 假日簡訊

            string alarm_text = sms_content;
            alarm_format.alarm_text = alarm_text;
            alarm_format.alarm_comment = alarm_text;

            //this.Alarm_create_xml(alarm_format, "Holiday", "Holiday_Inout_SMS_For_C2");
            sw.WriteLine("SMS_C2 log finish");
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

        sw.WriteLine(DateTime.Now.ToString("u") + "Holiday_Inout_SMS_For_C2 R3 Program End");
        sw.WriteLine("");

        sw.Close();

        func.delete_log_dir(Server.MapPath(".") + "\\File\\", "*.*", -60);
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);

        //func.delete_log_dir(Server.MapPath("..\\") + "\\LOG\\", -60);
        //DeleteLogFile
        member oscar = new member();

        oscar.mail_list = "oscar.hsieh@innolux.com,ELLISA.LIN@INNOLUX.COM";
        oscar.title = "C2 假日簡訊/MAPP 新版 <資料驗證> 【" + today .Replace("/","")+ "】";
        oscar.strHTML = sms_content;
        SendEmail("cim.alarm@innolux.com", oscar.mail_list, oscar.title, oscar.strHTML, "", "");//
        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");

    }

    class c2_format
    {
        private string _TFTIN;
        public string TFTIN
        {
            set { _TFTIN = value; }
            get { return _TFTIN; }
        }
        private string _TFTOUT;
        public string TFTOUT
        {
            set { _TFTOUT = value; }
            get { return _TFTOUT; }
        }
        private string _TFTIN_MTD;
        public string TFTIN_MTD
        {
            set { _TFTIN_MTD = value; }
            get { return _TFTIN_MTD; }
        }

        private string _TFTOUT_MTD;
        public string TFTOUT_MTD
        {
            set { _TFTOUT_MTD = value; }
            get { return _TFTOUT_MTD; }
        }


        private string _TPIN;
        public string TPIN
        {
            set { _TPIN = value; }
            get { return _TPIN; }
        }
        private string _TPOUT;
        public string TPOUT
        {
            set { _TPOUT = value; }
            get { return _TPOUT; }
        }
        private string _TPIN_MTD;
        public string TPIN_MTD
        {
            set { _TPIN_MTD = value; }
            get { return _TPIN_MTD; }
        }

        private string _TPOUT_MTD;
        public string TPOUT_MTD
        {
            set { _TPOUT_MTD = value; }
            get { return _TPOUT_MTD; }
        }

    }

    public class member
    {


        private string _today_detail;
        public string today_detail
        {
            set { _today_detail = value; }
            get { return _today_detail; }
        }

        private string _tool;
        public string tool
        {
            set { _tool = value; }
            get { return _tool; }
        }
        private string _website;
        public string website
        {
            set { _website = value; }
            get { return _website; }
        }
        private string _title;
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }

        private string _strHTML;
        public string strHTML
        {
            set { _strHTML = value; }
            get { return _strHTML; }
        }

        private string _mail_list;
        public string mail_list
        {
            set { _mail_list = value; }
            get { return _mail_list; }
        }





    } 
    public static void SendEmail(string from, string to, string subject, string body, string cca, string file_path)
    {
        SmtpClient smtp = new SmtpClient("10.56.196.147");
        MailMessage email = new MailMessage(from, to, subject, body);
        if (cca == "")
        {
        }
        else
        {
            email.CC.Add(cca);
            //email.Bcc.Add(cca);
        }

        if (!file_path.Equals(""))
        {
            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(file_path);
            email.Attachments.Add(attachment);

        }



        email.IsBodyHtml = true;
        smtp.Send(email);


    }

}
