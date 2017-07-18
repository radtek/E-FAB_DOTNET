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
using System.Text;

public partial class Alarm_ary_hold_lot_mail : System.Web.UI.Page
{
    IS.util.special sp = new IS.util.special();
    file f = new file();
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


    #region MyRegion

     //select t.*,t.rowid  from std_alarm_level@ods2stdman t
 
     //select t.*,t.rowid from std_alarm_maillist@ods2stdman t

    #endregion
   

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
  
  

    private void create_xml(string s, bool incontrol, string inxml_file_name, string inUser_ID)
    {
        DataSet ds_insertDB = new DataSet();
        string sysid = "ARY_HOLD_LOT", xml_file_name = "";
        ArrayList element = new ArrayList();
        ArrayList element_text = new ArrayList();

        DirectoryInfo di = new DirectoryInfo(Server.MapPath(".") + "/" + DateTime.Now.ToString("yyyyMMdd") + "_ALCS_" + DateTime.Now.Hour.ToString());
        if (!di.Exists)
        {
            di.Create();
        }
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

        xmlw.Create_ALCS_xml(Server.MapPath(".") + "/" + DateTime.Now.ToString("yyyyMMdd") + "_ALCS_" + DateTime.Now.Hour.ToString(), xml_file_name, element, element_text);
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
                ff.upload(Server.MapPath(".") + "/" + DateTime.Now.ToString("yyyyMMdd") + "_ALCS_" + DateTime.Now.Hour.ToString() + "/" + xml_file_name);
                //sp.Send_mail("File Upload Success","CIM CENTRAL MAIL SYSTEM<cimalarm@INNOLUX.COM>","bunny.su@INNOLUX.COM","[Array Hold Lot] XML  File Upload ALCS Success",1,Server.MapPath(".")+"/"+ DateTime.Now.ToString("yyyyMMdd")+"_ALCS_" + DateTime.Now.Hour.ToString() + "/" + xml_file_name);

                string insertDB_phone = " begin insert into std_alarm_event_history@ods2stdman ( SYSTEM,TYPE,USER_ID,USER_LEVEL,PHONE_DTTM ) " +
                    " values('Array_Hold_Lot','PHONE','" + inUser_ID + "','" + inxml_file_name + "',sysdate); commit; end;";
                ds_insertDB = db.GetDataset(insertDB_phone, 1);
            }
            catch (Exception ex)
            {
                //sp.Send_mail("File Upload Fail"+ex.Message,"CIM CENTRAL MAIL SYSTEM<cimalarm@INNOLUX.COM>","bunny.su@INNOLUX.COM","[CIM REPORT MSG] :WeekendReport MSG:XML  File Upload ALCS Fail",0,null);
                sp.Send_mail("File Upload Fail" + ex.Message, "CIM CENTRAL MAIL SYSTEM<cimalarm@INNOLUX.COM>", "OSCAR.HSIEH@INNOLUX.COM", "[Array Hold Lot] XML  File Upload ALCS Fail", 0, null);

            }
        }

    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在這裡放置使用者程式碼以初始化網頁
        FileInfo fi;
        
        bool Enable = true;
        bool Disable = false;


        if (!IsPostBack)
        {
            alarm_format.trx_id = "1";
            alarm_format.type_id = "1";
            alarm_format.fab_id = "T1ARRAY";
            alarm_format.sys_type = "ARY_HOLD_LOT";
            alarm_format.eq_id = "1";
            alarm_format.alarm_id = "MSG";
            alarm_format.alarm_text = "ARY HOLD LOT";
            alarm_format.mail_contenttype = "T";
            alarm_format.alarm_comment = "ARY HOLD LOT";
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
        sw.WriteLine(DateTime.Now.ToString("u") + "ARY_HOLD_LOT Program Start");


        //*/			

        //選出所有的Hold Lot事件和Group_id及User_id作Join，選出時間為 4~8hr及大於12hr
        //string sql_event = " select T1.LOT_ID LOT_ID,T1.CST_ID CST_ID,T1.LOT_TYPE AS TYPE,T2.HOLD_SUBSTRATE_QTY as QTY," +
        //    " T1.PRIORITY PRIORITY,SUBSTR(T2.HOLD_REASON_CODE,1,INSTR(T2.HOLD_REASON_CODE,':')-1) AS DEPT, " +
        //    " SUBSTR(T2.HOLD_REASON_CODE,INSTR(T2.HOLD_REASON_CODE,':')+1,LENGTH(T2.HOLD_REASON_CODE)) AS CODE, " +
        //    " T2.HOLDING_STEP_NAME AS STEP_NAME,to_char(T2.HOLD_DTTM,'yyyy/mm/dd hh24:mi:ss') HOLD_STARTTIME, " +
        //    " Round(((SYSDATE) - (T2.HOLD_DTTM))*24,1) as Hours , " + //"H/T(hrs)"
        //    " Convert( T1.LOT_CMMT || ' ', 'UTF8','ZHT16MSWIN950' ) as LOT_COMMENT,T2.TA, " +
        //    " 'T' || substr(t1.lot_id, 5, 1) || '-' || SUBSTR(T2.HOLD_REASON_CODE, 1, INSTR(T2.HOLD_REASON_CODE, ':') - 1) AS GROUP_ID, " +
        //    " T3.user_id AS USER_ID " +
        //    " from LOT T1, HOLD_LOT_HISTORY T2,std_alarm_level@ods2stdman T3, " +
        //    " ( " +
        //    " SELECT HL.LOT_ID,MAX(HL.HOLD_DTTM) AS HOLD_DTTM FROM HOLD_LOT_HISTORY HL " +
        //    " WHERE NVL(TO_CHAR(HL.RELEASE_TIME ,'YYYY/MM/DD'),'NULL') ='NULL' " +
        //    " AND HL.HOLD_REASON_CODE LIKE '%:%' AND HL.HOLD_REASON_CODE IS NOT NULL " +
        //    " GROUP BY HL.LOT_ID " +
        //    "  ) T4 " +
        //    " where T2.LOT_ID = T4.LOT_ID AND T2.HOLD_DTTM = T4.HOLD_DTTM AND T1.LOT_ID=T2.LOT_ID and T1.FAB=T2.FAB and T1.SHOP=T2.SHOP " +
        //    " and nvl( T1.Lot_Type, ' ' ) <> 'MQC' " +
        //    " and T1.PROC_STATE='OnHold' and T1.LOT_ID<>' ALL' " +
        //    " and NVL(TO_CHAR(T2.RELEASE_TIME ,'YYYY/MM/DD'),'NULL') ='NULL' " +
        //    " and T1.STEP_NAME=T2.HOLDING_STEP_NAME AND T2.HOLD_REASON_CODE LIKE '%:%' " +
        //    " AND T2.HOLD_REASON_CODE IS NOT NULL " +
        //    " and t1.shop in ('T1Array','T0Array') and t1.terminate_dttm is null " +
        //    " and (Round(((SYSDATE) - (T2.HOLD_DTTM)) * 24, 2) > 4  " +
        //    " and Round(((SYSDATE) - (T2.HOLD_DTTM)) * 24, 2) < 8 " +
        //    " or Round(((SYSDATE) - (T2.HOLD_DTTM)) * 24, 2) > 12) " +
        //    " and T3.group_id = 'T' || substr(t1.lot_id, 5, 1) || '-' || SUBSTR(T2.HOLD_REASON_CODE, 1, INSTR(T2.HOLD_REASON_CODE, ':') - 1) ";

        string sql_event = @"
select T1.LOT_ID LOT_ID,
        T1.CST_ID CST_ID,
        T1.LOT_TYPE AS TYPE,
        T2.HOLD_SUBSTRATE_QTY as QTY,
        T1.PRIORITY PRIORITY,
        SUBSTR(T2.HOLD_REASON_CODE, 1, INSTR(T2.HOLD_REASON_CODE, ':') - 1) AS DEPT,
        SUBSTR(T2.HOLD_REASON_CODE,
               INSTR(T2.HOLD_REASON_CODE, ':') + 1,
               LENGTH(T2.HOLD_REASON_CODE)) AS CODE,
        T2.HOLDING_STEP_NAME AS STEP_NAME,
        to_char(T2.HOLD_DTTM, 'yyyy/mm/dd hh24:mi:ss') HOLD_STARTTIME,
        Round(((SYSDATE) - (T2.HOLD_DTTM)) * 24, 1) as Hours,
        T5.RemQtime,
        T5.MRemQtime,
        Convert(T1.LOT_CMMT , 'UTF8', 'ZHT16MSWIN950') as LOT_COMMENT,
        T2.TA,
        'T' || substr(t1.lot_id, 5, 1) || '-' ||
        SUBSTR(T2.HOLD_REASON_CODE, 1, INSTR(T2.HOLD_REASON_CODE, ':') - 1) AS GROUP_ID,
        T3.user_id AS USER_ID
   from LOT T1,
        HOLD_LOT_HISTORY T2,
        std_alarm_level@ods2stdman T3,
        (SELECT HL.LOT_ID, MAX(HL.HOLD_DTTM) AS HOLD_DTTM
           FROM HOLD_LOT_HISTORY HL
          WHERE NVL(TO_CHAR(HL.RELEASE_TIME, 'YYYY/MM/DD'), 'NULL') = 'NULL'
            AND (HL.HOLD_REASON_CODE LIKE '%:%' or  HL.HOLD_REASON_CODE like '%Abnormal_End_Hold%' )
            AND HL.HOLD_REASON_CODE IS NOT NULL
          GROUP BY HL.LOT_ID) T4,(
          select a.RemQtime,a.MRemQtime,a.appid from mfg_lot_qtime a
          )T5
  where T2.LOT_ID = T4.LOT_ID
    AND T2.HOLD_DTTM = T4.HOLD_DTTM
    and substr(t1.cst_id, 0, 2) in ('1A', '1T', '1C', '1E', '0A')
    AND T1.LOT_ID = T2.LOT_ID
    and T1.FAB = T2.FAB
    and T1.SHOP = T2.SHOP
    and nvl(T1.Lot_Type, ' ') <> 'MQC'
    and T1.PROC_STATE = 'OnHold'
    and T1.LOT_ID <> ' ALL'
    and NVL(TO_CHAR(T2.RELEASE_TIME, 'YYYY/MM/DD'), 'NULL') = 'NULL'
    and T1.STEP_NAME = T2.HOLDING_STEP_NAME
    AND (T2.HOLD_REASON_CODE LIKE '%:%' or  T2.HOLD_REASON_CODE  like '%Abnormal_End_Hold%' )
    AND T2.HOLD_REASON_CODE IS NOT NULL
    and t1.shop in ('T1Array', 'T0Array')
    and t1.terminate_dttm is null
    and Round(((SYSDATE) - (T2.HOLD_DTTM)) * 24, 2) > 12
    and T3.group_id =
        'T' || substr(t1.lot_id, 5, 1) || '-' ||
        SUBSTR(T2.HOLD_REASON_CODE, 1, INSTR(T2.HOLD_REASON_CODE, ':') - 1)
    and T2.LOT_ID=T5.appid(+)

";


        DataSet ds_event;
        ds_event = func.get_dataSet_access(sql_event,conn);
        if (ds_event.Tables[0].Rows.Count > 0)
        {
            // 利用DataView取得Hold Lot事件
            DataView dv_engineer = ds_event.Tables[0].DefaultView;
            DataView dv_Manager_L3 = ds_event.Tables[0].DefaultView;
            DataView dv_Manager_L2 = ds_event.Tables[0].DefaultView;

            string sql_userid = " select distinct(user_id) from std_alarm_level@ods2stdman ";
            DataSet ds_userid = func.get_dataSet_access(sql_userid, conn);

            // 以User_id來選出符合寄送的Hold Lot
            for (int i = 0; i < ds_userid.Tables[0].Rows.Count; i++)
            {
                // 用DataView的RowFilter功能以User_ID來選出符合寄送的Hold Lot
                dv_engineer.RowFilter = " USER_ID = '" + ds_userid.Tables[0].Rows[i]["user_id"] + "'";
                if (dv_engineer.Count > 0)
                {
                    //把選出符合寄送的Hold Lot轉換成HTML格式(Hold_Lot Table, 篩選出的DataView)
                    string html_table_engineer = this.create_html_table(ds_event.Tables[0], dv_engineer);

                    //發信函式(User_id Table, 轉換好的html格式, 上傳XML檔案的分類, 控制User_ID的i, 信件title的分類)
                    this.mysendmail(ds_userid.Tables[0], html_table_engineer, "_engineer", i, " > 12HR", dv_engineer.Count);

                    string xml_file_name = ds_userid.Tables[0].Rows[i]["user_id"].ToString() + "_engineer";
                    string alarmtext = "There are lots exceed process time for 24hrs, please check";
                   
                    //create_xml(寄送內容、啟動(0)/關閉(1)、檔案名稱)
                    //this.create_xml(alarmtext, Enable, xml_file_name, ds_userid.Tables[0].Rows[i]["user_id"].ToString());
                    
                    alarm_format.alarm_text = alarmtext;
                    alarm_format.alarm_comment = alarmtext;
                    alarm_format.sys_type = "ARY_HOLD_LOT";
                    alarm_format.eq_id = xml_file_name;
                    this.Alarm_create_xml(alarm_format, "ARY_HOLD_LOT", "ARY_HOLD_LOT_" + xml_file_name);
                    sw.WriteLine("There are lots exceed process time for 24hrs, please check");
                    sw.WriteLine("");
                }

                // 選出超過24hr的Hold Lot事件 && DateTime.Now.Hour.ToString()=="13"


                //dv_Manager_L3.RowFilter = " USER_ID = '" + ds_userid.Tables[0].Rows[i]["user_id"] + "' and Hours > 24";
                //if (dv_Manager_L3.Count > 0)
                //{
                //    string html_table_Manager_L3 = this.create_html_table(ds_event.Tables[0], dv_Manager_L3);
                //    this.mysendmail(ds_userid.Tables[0], html_table_Manager_L3, "_manager_level3", i, " > 24HR");
                //    //三級主管只有在每日13:00的時候會收到alarm phone
                //    if (DateTime.Now.Hour.ToString() == "13")
                //    {
                //        string xml_file_name = ds_userid.Tables[0].Rows[i]["user_id"].ToString() + "_manager_level3";
                //        string alarmtext = "There are lots exceed process time for 24hrs, please check";
                      
                //        //create_xml(寄送內容、啟動(0)/關閉(1)、檔案名稱)
                //       // this.create_xml(alarmtext, Enable, xml_file_name, ds_userid.Tables[0].Rows[i]["user_id"].ToString());
                //        alarm_format.alarm_text = alarmtext;
                //        alarm_format.alarm_comment = alarmtext;
                //        alarm_format.sys_type = "ARY_HOLD_LOT";
                //        alarm_format.eq_id = xml_file_name;
                //        this.Alarm_create_xml(alarm_format, "ARY_HOLD_LOT", "ARY_HOLD_LOT_" + xml_file_name);
                //        sw.WriteLine("There are lots exceed process time for 24hrs, please check");
                //        sw.WriteLine("");
                //    }

                //}



                //選出超過48hr的Hold Lot事件 && DateTime.Now.Hour.ToString()=="13"

                //dv_Manager_L2.RowFilter = " USER_ID = '" + ds_userid.Tables[0].Rows[i]["user_id"] + "' and Hours > 48";
                //if (dv_Manager_L2.Count > 0)
                //{
                //    string html_table_Manager_L2 = this.create_html_table(ds_event.Tables[0], dv_Manager_L2);
                //   this.mysendmail(ds_userid.Tables[0], html_table_Manager_L2, "_manager_level2", i, " > 48HR");
                //    //二級主管只有在每日13:00的時候會收到alarm phone
                //    if (DateTime.Now.Hour.ToString() == "13")
                //    {
                //        string xml_file_name = ds_userid.Tables[0].Rows[i]["user_id"].ToString() + "_manager_level2";
                //        string alarmtext = "There are lots exceed process time for 48hrs, please check";

                       
                //        //create_xml(寄送內容、啟動(0)/關閉(1)、檔案名稱)
                //        //this.create_xml(alarmtext, Enable, xml_file_name, ds_userid.Tables[0].Rows[i]["user_id"].ToString());
                //        alarm_format.alarm_text = alarmtext;
                //        alarm_format.alarm_comment = alarmtext;
                //        alarm_format.sys_type = "ARY_HOLD_LOT";
                //        alarm_format.eq_id = xml_file_name;
                //        this.Alarm_create_xml(alarm_format, "ARY_HOLD_LOT", "ARY_HOLD_LOT_"+ xml_file_name);
                //        sw.WriteLine("There are lots exceed process time for 48hrs, please check");
                //        sw.WriteLine("");
                //    }

                //}


            }//end of for				
        }// end of if			



        sw.WriteLine(DateTime.Now.ToString("u") + "ARY_HOLD_LOT Program End");
        sw.WriteLine("");

        sw.Close();

        func.delete_log_dir(Server.MapPath(".") + "\\File\\", "*.*", -60);
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);

        //*/
        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
    }// end of page_load

    string create_html_table(DataTable inDT, DataView inDV)
    {
        StringBuilder sb = new StringBuilder();
       
        sb.Append("<table border='1' background-color=#DDE3FF width=100% cellspacing='1' cellpadding='1'>");
        //sb.Append("<table border=1 background-color=#DDE3FF;border-bottom= 1px solid #DDE3FF; border-right=1px solid #DDE3FF;border-left=1px solid #DDE3FF;border-top=1px solid #ffffff;   width=100%   cellspacing=1 cellpadding=1  >");
        //用來增加meta table中的欄位no
        sb.Append("<tr>");
        sb.Append("<td bgcolor='#507CD1' align='center'>");
        sb.Append("<font width='10%' color='#ffffff' size='2'>");
        sb.Append("NO");
        sb.Append("</fone></td>");
        foreach (DataColumn myCol in inDT.Columns)
        {
            sb.Append("<td bgcolor='#507CD1' align='center'>");
            sb.Append("<font width='10%' color='#ffffff' size='2'>");
            sb.Append(myCol.Caption);
            sb.Append("</fone></td>");

        }
        for (int k = 0; k < inDV.Count; k++)
        {
            sb.Append("<tr>");

            //number欄位
            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(k + 1);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["LOT_ID"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["CST_ID"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["TYPE"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["QTY"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["PRIORITY"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["DEPT"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["CODE"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["STEP_NAME"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["HOLD_STARTTIME"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#FFFF00' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["HOURS"]);
            sb.Append("</fone></td>");
            sb.Append("<td bgcolor='#FFFF00' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["REMQTIME"]);
            sb.Append("</fone></td>");
            sb.Append("<td bgcolor='#FFFF00' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["MREMQTIME"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='left'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["LOT_COMMENT"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["TA"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["GROUP_ID"]);
            sb.Append("</fone></td>");

            sb.Append("<td bgcolor='#EFF3FB' align='center'><font color='#696969' size='2'>");
            sb.Append(inDV[k]["USER_ID"]);
            sb.Append("</fone></td>");

            /*
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["LOT_ID"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["CST_ID"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["LOT_TYPE"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["QTY"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["PRIORITY"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["DEPT"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["HOLD_REASON_CODE"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["HOLDING_STEP_NAME"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["HOLD_START_TIME"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["HOURS"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["LOT_COMMENT"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["TA"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["GROUP_ID"]+"&nbsp;</b></td>");
            sb.Append("<td bgcolor=#EFF3FB font-size=11px;color= #696969;font-family= Verdana, Arial, Helvetica, sans-serif;><b>&nbsp;"+inDV[k]["USER_ID"]+"&nbsp;</b></td>");
            //*/
            sb.Append("</tr>");//DDE3FF							
        }//end of for
        sb.Append("</table>");
        return sb.ToString();
    }

    void mysendmail(DataTable inDT, string inString, string inlevel, int index, string situation,Int32 dataCount)
    {
        //bool Enable = true;
        //bool Disable = false;
        DataSet ds_mail_list = new DataSet();
        DataSet ds_insertDB = new DataSet();

        string sp_body = "<font size=5 color=red > <b> CIM 電子報系統 -- " + DateTime.Today.ToString("MM/dd") + " ARY HOLD LOT ( " + inDT.Rows[index]["user_id"] + " )</b></font><br><br>" +
            "<br> <b> 1. ARY HOLD LOT " + "<font size=5 color=red >" + situation + "</font></b>" +
            "<br>" + inString + 
            "<br> <b> 2. 異常問題排除 </b> " +
            "<br>此為CIM 電子報系統自動寄發之信件，如對信件內容有問題請洽ARMFG 陳沅銘(63104) 黃超楠(63417)" +
            "<br>若對信件寄送服務有問題請洽CIM 謝正一(64179) 潘廷勇(64173) 。";


        string sql_mail_list = "select * from std_alarm_maillist@ods2stdman t where t.enable_flag = 'Y' and t.user_id = '" + inDT.Rows[index]["user_id"] + "'";
        //ds_mail_list = db.GetDataset(sql_mail_list, 1);
        ds_mail_list = func.get_dataSet_access(sql_mail_list,conn);



        if (ds_mail_list.Tables[0].Rows.Count > 0) //Tables[0].Rows.Count > 0
        {
            for (int i = 0; i < ds_mail_list.Tables[0].Rows.Count; i++)
            {
                string sp_to = ds_mail_list.Tables[0].Rows[i]["USER_MAIL"].ToString();
                //sp_to = "oscar.hsieh@innolux.com";
                string sp_cc = "";
                string level = inDT.Rows[index]["user_id"].ToString() + inlevel;
                sp.Send_mail(sp_body, "CIM CENTRAL MAIL SYSTEM<cim.alarm@INNOLUX.COM>", sp_to, sp_cc, "[CIM 電子報系統] : " + DateTime.Today.ToString("MM/dd") + " ARY Hold Lot ( " + inDT.Rows[index]["user_id"] + " )" + situation + "【共 " + dataCount.ToString() + " 筆資料 】  ", 0, null);
                string insertDB_mail = " begin insert into std_alarm_event_history@ods2stdman ( SYSTEM,TYPE,USER_ID,USER_MAIL,USER_LEVEL, MAIL_DTTM ) " +
                    " values('Array_Hold_Lot','MAIL','" + inDT.Rows[index]["user_id"].ToString() + "','" + sp_to + "','" + level + "',sysdate); commit; end;";
                ds_insertDB = db.GetDataset(insertDB_mail, 1);
            }
        }
        //*/


        //Response.Write( sp_to );
        //Response.End();


        //string s = "There are lots exceed process time, please check";						
        //string xml_file_name = inDT.Rows[index]["user_id"].ToString() + inlevel;						
        //create_xml(寄送內容、啟動(0)/關閉(1)、檔案名稱)
        //this.create_xml(s,Disable,xml_file_name);
        //string sp_to = "bunny.su@INNOLUX.COM" ;			
        //string sp_to = "Amorock.Chen@INNOLUX.COM" ;


    }

}