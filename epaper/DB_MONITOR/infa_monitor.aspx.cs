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

public partial class DB_MONITOR_infa_monitor : System.Web.UI.Page
{
    IS.util.special sp = new IS.util.special();
    //file f = new file();
    StreamWriter sw;
    FileInfo fi;
   

    //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_MIS"];
  
   


   
    string SaveLocation = "";
    Int32 counter_oscar = 0;
    func xmlw = new func();
    func.alarm_format alarm_format = new func.alarm_format();

    
    
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_INFA_TEST"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_PARS1_OLE_ONDUTY"];


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

    ArrayList arlist_temp1 = new ArrayList();

    
    protected void Page_Load(object sender, EventArgs e)
    {  

      if(!IsPostBack)
      {




          alarm_format.trx_id = "1";
          alarm_format.type_id = "1";
          alarm_format.fab_id = "T1ARRAY";
          alarm_format.sys_type = "ALM_SMS";
          alarm_format.eq_id = "SMS";
          alarm_format.alarm_id = "67";
          alarm_format.alarm_text = @"T1 Alarm Server SMS Test OK";
          alarm_format.mail_contenttype = "T";
          alarm_format.alarm_comment = @"T1 Alarm Server SMS Test OK";
          alarm_format.pc_ip = "1";
          alarm_format.pc_name = "1";
          alarm_format.operator1 = "1";
          alarm_format.issue_date = "1";



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
          sw.WriteLine(DateTime.Now.ToString("u") + "DB MONITOR Program Start");


          sql_temp1 = @"select t.db_name,t.db_tns from infra_db_monitor t where t.flag='Y'";

          ds_temp1 = func.get_dataSet_access(sql_temp1, conn1);

          for (int i = 0; i <= ds_temp1.Tables[0].Rows.Count - 1; i++)
          {


              sql_temp = @"select '1' as oscar from dual";

              try
              {
                  ds_temp = func.get_dataSet_access(sql_temp, ds_temp1.Tables[0].Rows[i]["db_tns"].ToString());
              }
              catch (Exception)
              {

                  Response.Write(ds_temp1.Tables[0].Rows[i]["db_name"].ToString() + " DB Connection TimeOut<BR>");

                  if (1 == 1)
                  {
                      alarm_format.fab_id = "TFTPIA";
                      alarm_format.sys_type = "DB";
                      alarm_format.eq_id = "DB";
                      alarm_format.alarm_id = "DB_CHK";
                  }
                  
                  
                  
                  alarm_format.alarm_text = ds_temp1.Tables[0].Rows[i]["db_name"].ToString().Replace("C2","C_2") + " DB Connection TimeOut";

                  alarm_format.alarm_comment = ds_temp1.Tables[0].Rows[i]["db_name"].ToString().Replace("C2", "C_2") + " DB Connection TimeOut";
                  this.Alarm_create_xml(alarm_format, "DB_MONITOR", "DB_MONITOR_SMS");
                  sw.WriteLine(ds_temp1.Tables[0].Rows[i]["db_name"].ToString() + " DB Connection TimeOut");

              }


          }

         


          sw.WriteLine("");
          sw.WriteLine(DateTime.Now.ToString("u") + "DB MONITOR Program End");
          sw.WriteLine("");

          sw.Close();

          func.delete_log_dir(Server.MapPath(".") + "\\File\\", "*.*", -60);
          func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);



          Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");

      


      
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
            sw_oscar = fi_oscar.CreateText();
        }

        //string xml_content = @"<?xml version=""1.0"" encoding=""big5""  ?><transaction><trx_id>AUTOREPORT</trx_id><type_id>1</type_id><fab_id>{0}</fab_id><sys_type>{1}</sys_type><eq_id>{2}</eq_id><alarm_id>{3}</alarm_id><alarm_text>{4}</alarm_text><mail_contenttype>T</mail_contenttype><alarm_comment value = ""{5}"" /><pc_ip>172.20.7.120</pc_ip><pc_name>AMS01</pc_name><operator>AMS01</operator><issue_date>20110804104843</issue_date></transaction>";


        string xml_content2 = @"<?xml version=""1.0"" ?>
<transaction>
	<trx_id>AUTOREPORT</trx_id>
	<type_id>1</type_id>
	<fab_id>{0}</fab_id>
	<sys_type>{1}</sys_type>
	<eq_id>{2}</eq_id>
	<alarm_id>{3}</alarm_id>
	<alarm_text>{4}</alarm_text>
	<mail_contenttype >T</mail_contenttype >
	<alarm_comment value =""{5}"" />
	<pc_ip>10.56.131.22</pc_ip>
	<pc_name>CIMWEB01</pc_name>
	<operator>CIMWEB01</operator>
	<issue_date>20110804104843</issue_date>
</transaction>
";
        xml_content2 = string.Format(xml_content2, alarm_format.fab_id, alarm_format.sys_type, alarm_format.eq_id, alarm_format.alarm_id, alarm_format.alarm_text, alarm_format.alarm_comment);
        //xml_content = string.Format(xml_content, alarm_format.fab_id, alarm_format.sys_type, alarm_format.eq_id, alarm_format.alarm_id, alarm_format.alarm_text, alarm_format.alarm_comment);


        // byte[] b = Encoding.Default.GetBytes(xml_content);//將字串轉為byte[]

        // byte[] c = Encoding.Convert(Encoding.Default, Encoding.Unicode, b);//進行轉碼,參數1,來源編碼,參數二,目標編碼,參數三,欲編碼變數



        //sw_oscar.WriteLine(Encoding.Unicode.GetString(c));
        //sw_oscar.WriteLine(xml_content);
        sw_oscar.WriteLine(xml_content2);

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
}
