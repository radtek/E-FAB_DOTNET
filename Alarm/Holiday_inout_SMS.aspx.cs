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



public partial class Alarm_Holiday_inout_SMS : System.Web.UI.Page
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
        string stn = "STN:";
        string TP_FCTP = "F-CTP:";
        string TP_BCTP = "B-CTP:";
        int i = 0;

        string sql_stm = " select * from (select upper(t1.shop) as Shop,to_char(t1.daily_in) as \"Input(Normal/Rework)\" ,to_char(t1.daily_out) as output " +
            " from eis_in_out_sum@cell2ary t1 , eis_yield_sum@cell2ary t2 " +
            " where t1.shift_day = t2.shift_day and t1.shift_day = to_char(sysdate-1,'YYYYMMDD') and t1.shop = t2.shop " +
            " union " +
            " select t1.shop,to_char(t1.daily_in) ,to_char(t1.daily_out) " +
            " from eis_in_out_sum t1 ,  eis_yield_sum t2 " +
            " where t1.shift_day = to_char(sysdate-1,'YYYYMMDD') and t1.shop in ('T0CELL','T1CELL') " +
            " and t1.shift_day = t2.shift_day and t1.shop = t2.shop " +
            " union " +
            " select replace(t1.shop,'STN','CN_STN') shop,to_char(t1.daily_in) ,to_char(t1.daily_out) " +
            " from eis_in_out_sum t1  " +
            //" where t1.shift_day = to_char(sysdate-1,'YYYYMMDD') and t1.shop in ('LH_FEOL2','LH_BEOLS','LH_BEOLL','STN') " +
            " where t1.shift_day = to_char(sysdate-1,'YYYYMMDD') and t1.shop in ('LH_FEOL2','LH_BEOLS','LH_BEOLL') " +
            " union " +
            " select t1.shop,to_char(t1.daily_in_normal)||' / '||to_char(t1.daily_in_rework), to_char(t1.daily_out) " +
            " from eis_in_out_sum@cell2cf t1 , eis_yield_sum@cell2cf t2 where t1.shift_day = t2.shift_day " +
            " and t1.shift_day = to_char(sysdate-1,'YYYYMMDD') and t1.shop = t2.shop " +
            //F-CTP
            " union select t1.shop,to_char(t1.daily_in),to_char(t1.daily_out)" +
            " from eis_in_out_sum t1, others_daily_yield_sum t2 where t1.shift_day = to_char(sysdate - 1, 'YYYYMMDD') " +
            " and t1.SHOP = 'TP_F-CTP' and t1.shift_day = t2.shift_date and t2.shop = 'TP_CTP' " +
            " and t2.yield_name = 'TotalYield' " +
            //B-CTP
            " union " +
            " select t1.shop, to_char(t1.daily_in),to_char(t1.daily_out)" +
            " from eis_in_out_sum t1, others_daily_yield_sum t2 " +
            " where t1.shift_day = to_char(sysdate - 1, 'YYYYMMDD') " +
            " and t1.SHOP = 'TP_B-CTP' " +
            " and t1.shift_day = t2.shift_date " +
            " and t2.shop = 'TP_CTP' " +
            " and t2.yield_name = 'TotalYield' " +

            //CoverLens
            //" union select 'CL1' as shop, to_char(t.daily_in),to_char(t.daily_out) from cl_in_out t where t.shift_date = to_char(sysdate - 1, 'YYYYMMDD') "+

            //" union " +
            //" select * from " +
            //" ( "+
            //" select 'CL1' as shop, to_char(t.daily_in), to_char(t.daily_out) "+
            //" from cl_in_out t where t.shift_date = to_char(sysdate - 1, 'YYYYMMDD') "+
            //" union select 'CL1' as shop, '0','0' from dual order by 2 desc "+
            //" ) where rownum = 1 "+
            " ) order by decode(shop,'T0ARRAY',0,'T1ARRAY',1,'T0CELL',2,'T1CELL',3,'T1CF',4,'LH_FEOL2',5,'LH_BEOLS',6,'LH_BEOLL',7,'CN_STN',8,'TP_F-CTP',9,'TP_B-CTP',10)";


        //Response.Write(sql_stm);
        //Response.End();
        /// <summary>
        /// DB
        /// 1.array
        /// 2.cel
        /// 3.cf
        /// 4.eda
        /// 5.sso
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="S_dbname"></param>
        /// <returns></returns>

        DataSet ds;

        //ds = db.GetDataset(sql_stm, 2);
        ds = func.get_dataSet_access(sql_stm, conn_cel);


        #region
        //mark by bunny 20091109
        //將良率給移除
        #endregion
       

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            if (ds.Tables[0].Rows[i]["Shop"].ToString() == "T0ARRAY")
                t0a = t0a + ds.Tables[0].Rows[i]["Input(Normal/Rework)"].ToString() + "," + ds.Tables[0].Rows[i]["output"].ToString();
            else if (ds.Tables[0].Rows[i]["Shop"].ToString() == "T1ARRAY")
                t1a = t1a + ds.Tables[0].Rows[i]["Input(Normal/Rework)"].ToString() + "," + ds.Tables[0].Rows[i]["output"].ToString();
            else if (ds.Tables[0].Rows[i]["Shop"].ToString() == "T0CELL")
                t0c = t0c + ds.Tables[0].Rows[i]["Input(Normal/Rework)"].ToString() + "," + ds.Tables[0].Rows[i]["output"].ToString();
            else if (ds.Tables[0].Rows[i]["Shop"].ToString() == "T1CELL")
                t1c = t1c + ds.Tables[0].Rows[i]["Input(Normal/Rework)"].ToString() + "," + ds.Tables[0].Rows[i]["output"].ToString();
            else if (ds.Tables[0].Rows[i]["Shop"].ToString() == "T1CF")
                t1f = t1f + ds.Tables[0].Rows[i]["Input(Normal/Rework)"].ToString().Replace(" ", "") + "," + ds.Tables[0].Rows[i]["output"].ToString();
            //else if ( ds.Tables[0].Rows[i]["Shop"].ToString() == "CN_STN" )
            //	stn = stn + ds.Tables[0].Rows[i]["Input(Normal/Rework)"].ToString() + "," + ds.Tables[0].Rows[i]["output"].ToString()   ;	
            else if (ds.Tables[0].Rows[i]["Shop"].ToString() == "TP_F-CTP")
                TP_FCTP = TP_FCTP + ds.Tables[0].Rows[i]["Input(Normal/Rework)"].ToString() + "," + ds.Tables[0].Rows[i]["output"].ToString();
            else if (ds.Tables[0].Rows[i]["Shop"].ToString() == "TP_B-CTP")
                TP_BCTP = TP_BCTP + ds.Tables[0].Rows[i]["Input(Normal/Rework)"].ToString() + "," + ds.Tables[0].Rows[i]["output"].ToString();

        //假日簡訊
        s = t0a + t1a + t0c + t1c + t1f;

        //STN+F-CTP+B-CTP簡訊
        //string STN_CTP = stn + TP_FCTP + TP_BCTP;
        string STN_CTP = TP_FCTP + TP_BCTP;   

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
