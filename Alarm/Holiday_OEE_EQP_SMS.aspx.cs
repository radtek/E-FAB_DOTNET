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

public partial class Alarm_Holiday_OEE_EQP_SMS : System.Web.UI.Page
{
    IS.util.special sp = new IS.util.special();
    //file f = new file();
    StreamWriter sw;
    FileInfo fi;
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];
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
            alarm_format.sys_type ="OEE_EQP_T0A";
            alarm_format.eq_id = "1";
            alarm_format.alarm_id = "1";
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
        sw.WriteLine(DateTime.Now.ToString("u") + "OEE_EQP_SMS Program Start");



        string sql_T0A = " select EQPID,EQ_UPTIME,EQ_UTIL,EQ_MOVE from " +
                 " ( " +
                 " select SUBSTR(t.equipmentid,0,6) as EQPID, ROUND(t.epr_efcyav,0) || '%' as EQ_Uptime, ROUND(t.epr_efcyop,0) || '%' as EQ_Util, ROUND(t.movement,0) as EQ_Move " +
                 " from empaidxsummdaily t, equipment eq " +
                 " where t.equipmentid = eq.modulename " +
                 " and eq.moduletype='MAIN' and eq.modelname in ('0ACVD','0APVD') " +
                 " and t.cutoffcycle='D' " +
                 " and t.cutoffkey = to_char(sysdate-1,'yyyymmdd') " +
                 " and t.ttm <> t.nst " +
                 " order by t.epr_efcyav,t.epr_efcyop " +
                 " ) " +
                 " where rownum < 3 " +
                 " union all " +
                 " select EQPID,EQ_UPTIME,EQ_UTIL,EQ_MOVE from " +
                 " ( " +
                 " select SUBSTR(t.equipmentid,0,6) as EQPID, ROUND(t.epr_efcyav,0) || '%' as EQ_Uptime, ROUND(t.epr_efcyop,0) || '%' as EQ_Util, ROUND(t.movement,0) as EQ_Move " +
                 " from empaidxsummdaily t, equipment eq " +
                 " where t.equipmentid = eq.modulename " +
                 " and eq.moduletype='MAIN' and eq.modelname in ('0ADET','0AWET','0ASTR') " +
                 " and t.cutoffcycle='D' " +
                 " and t.cutoffkey = to_char(sysdate-1,'yyyymmdd') " +
                 " and t.ttm <> t.nst " +
                 " order by t.epr_efcyav,t.epr_efcyop " +
                 " ) " +
                 " where rownum < 3 " +
                 " union all " +
                 " select EQPID,EQ_UPTIME,EQ_UTIL,EQ_MOVE from " +
                 " ( " +
                 " select SUBSTR(t.equipmentid,0,6) as EQPID, ROUND(t.epr_efcyav,0) || '%' as EQ_Uptime, ROUND(t.epr_efcyop,0) || '%' as EQ_Util, ROUND(t.movement,0) as EQ_Move " +
                 " from empaidxsummdaily t, equipment eq " +
                 " where t.equipmentid = eq.modulename " +
                 " and eq.moduletype='MAIN' and eq.modelname in ('0ACOA','0AEXP','0ADEV') " +
                 " and t.cutoffcycle='D' " +
                 " and t.ttm <> t.nst " +
                 " and t.cutoffkey = to_char(sysdate-1,'yyyymmdd') " +
                 " order by t.epr_efcyav,t.epr_efcyop " +
                 " ) " +
                 " where rownum < 3 ";

        DataSet ds_T0A;
        ds_T0A = func.get_dataSet_access(sql_T0A,conn);
        string T0A = "T0A:";
        if (ds_T0A.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds_T0A.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ds_T0A.Tables[0].Columns.Count; j++)
                {
                    //如果不是最後一個資料，則用",
                    if (j != 3)
                    {
                        T0A = T0A + ds_T0A.Tables[0].Rows[i][j].ToString() + ",";
                    }
                    else if (i == 5 && j == 3)
                    {
                        T0A = T0A + ds_T0A.Tables[0].Rows[i][j].ToString();
                    }
                    else
                    {
                        T0A = T0A + ds_T0A.Tables[0].Rows[i][j].ToString() + ";";
                    }
                }
            }

        }// end of if			
        //this.create_xml(T0A,Enable,"T0A");
       
        T0A = T0A.Replace("%", " ");
        T0A = T0A.Replace(";", " ");

        string sql_T1A = " select EQPID,EQ_UPTIME,EQ_UTIL,EQ_MOVE from " +
            " ( " +
            " select SUBSTR(t.equipmentid,0,6) as EQPID, ROUND(t.epr_efcyav,0) || '%' as EQ_Uptime, ROUND(t.epr_efcyop,0) || '%' as EQ_Util, ROUND(t.movement,0) as EQ_Move " +
            " from empaidxsummdaily t, equipment eq " +
            " where t.equipmentid = eq.modulename " +
            " and eq.moduletype='MAIN' and eq.modelname in ('1ACVD','1APVD') " +
            " and t.cutoffcycle='D' " +
            " and t.cutoffkey = to_char(sysdate-1,'yyyymmdd') " +
            " and t.ttm <> t.nst " +
            " order by t.epr_efcyav,t.epr_efcyop " +
            " ) " +
            " where rownum < 3 " +
            " union all " +
            " select EQPID,EQ_UPTIME,EQ_UTIL,EQ_MOVE from " +
            " ( " +
            " select SUBSTR(t.equipmentid,0,6) as EQPID, ROUND(t.epr_efcyav,0) || '%' as EQ_Uptime, ROUND(t.epr_efcyop,0) || '%' as EQ_Util, ROUND(t.movement,0) as EQ_Move " +
            " from empaidxsummdaily t, equipment eq " +
            " where t.equipmentid = eq.modulename " +
            " and eq.moduletype='MAIN' and eq.modelname in ('1ADET','1AWET','1ASTR') " +
            " and t.cutoffcycle='D' " +
            " and t.cutoffkey = to_char(sysdate-1,'yyyymmdd') " +
            " and t.ttm <> t.nst " +
            " order by t.epr_efcyav,t.epr_efcyop " +
            " ) " +
            " where rownum < 3 " +
            " union all " +
            " select EQPID,EQ_UPTIME,EQ_UTIL,EQ_MOVE from " +
            " ( " +
            " select SUBSTR(t.equipmentid,0,6) as EQPID, ROUND(t.epr_efcyav,0) || '%' as EQ_Uptime, ROUND(t.epr_efcyop,0) || '%' as EQ_Util, ROUND(t.movement,0) as EQ_Move " +
            " from empaidxsummdaily t, equipment eq " +
            " where t.equipmentid = eq.modulename " +
            " and eq.moduletype='MAIN' and eq.modelname in ('1APHT') " +
            " and t.cutoffcycle='D' " +
            " and t.cutoffkey = to_char(sysdate-1,'yyyymmdd') " +
            " and t.ttm <> t.nst " +
            " order by t.epr_efcyav,t.epr_efcyop " +
            " ) " +
            " where rownum < 3 ";

        DataSet ds_T1A;
      
        ds_T1A = func.get_dataSet_access(sql_T1A, conn);

        string T1A = "T1A:";
        if (ds_T1A.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds_T1A.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ds_T1A.Tables[0].Columns.Count; j++)
                {
                    if (j != 3)
                    {
                        T1A = T1A + ds_T1A.Tables[0].Rows[i][j].ToString() + ",";
                    }
                    else if (i == 5 && j == 3)
                    {
                        T1A = T1A + ds_T1A.Tables[0].Rows[i][j].ToString();
                    }
                    else
                    {
                        T1A = T1A + ds_T1A.Tables[0].Rows[i][j].ToString() + ";";
                    }
                }
            }

        }// end of if			
      
        T1A = T1A.Replace("%", " ");
        T1A = T1A.Replace(";", " ");

        //T1Cell機況簡訊
        string sql_T1C = " select EQPID,EQ_UPTIME,EQ_UTIL,EQ_MOVE from " +
            " ( " +
            " select SUBSTR(t.equipmentid,0,6) as EQPID, ROUND(t.efcyav,0) || '%' as EQ_Uptime, ROUND(t.efcyop,0) || '%' as EQ_Util, ROUND(t.movement,0) as EQ_Move " +
            " from empaidxsummdaily t, equipment eq " +
            " where t.equipmentid = eq.modulename " +
            " and eq.moduletype='MAIN' and eq.modelname in ('1CASM','1CPI','1CRUB','1CPAL') " +
            " and t.cutoffcycle='D' " +
            " and t.cutoffkey = to_char(sysdate-1,'yyyymmdd') " +
            " and t.ttm <> t.nst " +
            " order by t.epr_efcyav,t.epr_efcyop " +
            " ) " +
            " where rownum < 3 " +
            " union all " +
            " select EQPID,EQ_UPTIME,EQ_UTIL,EQ_MOVE from " +
            " ( " +
            " select SUBSTR(t.equipmentid,0,6) as EQPID, ROUND(t.efcyav,0) || '%' as EQ_Uptime, ROUND(t.efcyop,0) || '%' as EQ_Util, ROUND(t.movement,0) as EQ_Move " +
            " from empaidxsummdaily t, equipment eq " +
            " where t.equipmentid = eq.modulename " +
            " and eq.moduletype='MAIN' and eq.modelname in ('0CASM','0CPI','0CRUB') " +
            " and t.cutoffcycle='D' " +
            " and t.cutoffkey = to_char(sysdate-1,'yyyymmdd') " +
            " and t.ttm <> t.nst " +
            " order by t.epr_efcyav,t.epr_efcyop " +
            " ) " +
            " where rownum < 3 " +
            " union all " +
            " select EQPID,EQ_UPTIME,EQ_UTIL,EQ_MOVE from " +
            " ( " +
            " select SUBSTR(t.equipmentid,0,6) as EQPID, ROUND(t.efcyav,0) || '%' as EQ_Uptime, ROUND(t.efcyop,0) || '%' as EQ_Util, ROUND(t.movement,0) as EQ_Move " +
            " from empaidxsummdaily t, equipment eq " +
            " where t.equipmentid = eq.modulename " +
            " and eq.moduletype='MAIN' and eq.modelname in ('1CPTS','1CLSR') " +
            " and t.cutoffcycle='D' " +
            " and t.cutoffkey = to_char(sysdate-1,'yyyymmdd') " +
            " and t.ttm <> t.nst " +
            " order by t.epr_efcyav,t.epr_efcyop " +
            " ) " +
            " where rownum < 3 ";



        DataSet ds_T1C;
       
        ds_T1C = func.get_dataSet_access(sql_T1C, conn);
        string T1C = "T1C:";
        if (ds_T1C.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds_T1C.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ds_T1C.Tables[0].Columns.Count; j++)
                {
                    if (j != 3)
                    {
                        T1C = T1C + ds_T1C.Tables[0].Rows[i][j].ToString() + ",";
                    }
                    else if (i == 5 && j == 3)
                    {
                        T1C = T1C + ds_T1C.Tables[0].Rows[i][j].ToString();
                    }
                    else
                    {
                        T1C = T1C + ds_T1C.Tables[0].Rows[i][j].ToString() + ";";
                    }
                }
            }

        }// end of if			
        //*/
      
        T1C = T1C.Replace("%", " ");
        T1C = T1C.Replace(";", " ");

        //T1CF & TDTest機況簡訊
        string sql_T1F = " select EQPID,EQ_UPTIME,EQ_UTIL,EQ_MOVE from " +
            " ( " +
            " select SUBSTR(t.equipmentid,0,6) as EQPID, ROUND(t.epr_efcyav,0) || '%' as EQ_Uptime, ROUND(t.epr_efcyop,0) || '%' as EQ_Util, ROUND(t.movement,0) as EQ_Move " +
            " from empaidxsummdaily t, equipment eq " +
            " where t.equipmentid = eq.modulename " +
            " and eq.moduletype='MAIN' and eq.modelname in ('1FBML','1FRRL','1FBBL','1FGGL','1FIFL','1FITO','1FISL','1FITL','1FPSL','1FOCL') " +
            " and t.cutoffcycle='D' " +
            " and t.cutoffkey = to_char(sysdate-1,'yyyymmdd') " +
            " and t.ttm <> t.nst " +
            " order by t.epr_efcyav,t.epr_efcyop " +
            " ) " +
            " where rownum < 3 " +
            " union all " +
            " select EQPID,EQ_UPTIME,EQ_UTIL,EQ_MOVE from " +
            " ( " +
            " select SUBSTR(t.equipmentid,0,6) as EQPID, ROUND(t.epr_efcyav,0) || '%' as EQ_Uptime, ROUND(t.epr_efcyop,0) || '%' as EQ_Util, ROUND(t.movement,0) as EQ_Move " +
            " from empaidxsummdaily t, equipment eq " +
            " where t.equipmentid = eq.modulename " +
            " and eq.moduletype='MAIN' and eq.modelname in ('0ALSR','0ALWS','0ATES') " +
            " and t.cutoffcycle='D' " +
            " and t.cutoffkey = to_char(sysdate-1,'yyyymmdd') " +
            " and t.ttm <> t.nst " +
            " order by t.epr_efcyav,t.epr_efcyop " +
            " ) " +
            " where rownum < 3 " +
            " union all " +
            " select EQPID,EQ_UPTIME,EQ_UTIL,EQ_MOVE from " +
            " ( " +
            " select SUBSTR(t.equipmentid,0,6) as EQPID, ROUND(t.epr_efcyav,0) || '%' as EQ_Uptime, ROUND(t.epr_efcyop,0) || '%' as EQ_Util, ROUND(t.movement,0) as EQ_Move " +
            " from empaidxsummdaily t, equipment eq " +
            " where t.equipmentid = eq.modulename " +
            " and eq.moduletype='MAIN' and eq.modelname in ('1ALSR','1ALWS','1ATES') " +
            " and t.cutoffcycle='D' " +
            " and t.cutoffkey = to_char(sysdate-1,'yyyymmdd') " +
            " and t.ttm <> t.nst " +
            " order by t.epr_efcyav,t.epr_efcyop " +
            " ) " +
            " where rownum < 3 ";



        DataSet ds_T1F;
      
        ds_T1F = func.get_dataSet_access(sql_T1F, conn);
        string T1F = "T1F:";
        if (ds_T1F.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds_T1F.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ds_T1F.Tables[0].Columns.Count; j++)
                {
                    if (j != 3)
                    {
                        T1F = T1F + ds_T1F.Tables[0].Rows[i][j].ToString() + ",";
                    }
                    else if (i == 5 && j == 3)
                    {
                        T1F = T1F + ds_T1F.Tables[0].Rows[i][j].ToString();
                    }
                    else if (i == 1 && j == 3)
                    {
                        T1F = T1F + ds_T1F.Tables[0].Rows[i][j].ToString() + ";" + "TDTEST:";
                    }
                    else
                    {
                        T1F = T1F + ds_T1F.Tables[0].Rows[i][j].ToString() + ";";
                    }
                }
            }

        }// end of if			
        //*/
      
        T1F = T1F.Replace("%", " ");
        T1F = T1F.Replace(";", " ");

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
            if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday || DateTime.Today.DayOfWeek == DayOfWeek.Sunday || s1.Equals("1"))
            {
                // create_xml(s, 0, "REPORT");//SEND 假日簡訊

                string alarm_text = T0A;
                alarm_format.alarm_text = alarm_text;
                alarm_format.alarm_comment = alarm_text;
                alarm_format.sys_type = "OEE_EQP_T0A";

                this.Alarm_create_xml(alarm_format, "OEE_EQP", "Holiday_OEE_EQP_T0A_SMS");
                sw.WriteLine("Holiday_OEE_EQP_T0A_SMS log finish");
                sw.WriteLine("");

                alarm_text = T1A;
                alarm_format.alarm_text = alarm_text;
                alarm_format.alarm_comment = alarm_text;
                alarm_format.sys_type = "OEE_EQP_T1A";
                this.Alarm_create_xml(alarm_format, "Holiday", "Holiday_OEE_EQP_T1A_SMS");
                sw.WriteLine("Holiday_OEE_EQP_T1A_SMS log finish");
                sw.WriteLine("");

               alarm_text = T1C;
                alarm_format.alarm_text = alarm_text;
                alarm_format.alarm_comment = alarm_text;
                alarm_format.sys_type = "OEE_EQP_T1C";
                this.Alarm_create_xml(alarm_format, "Holiday", "Holiday_OEE_EQP_T1C_SMS");
                sw.WriteLine("Holiday_OEE_EQP_T1C_SMS log finish");
                sw.WriteLine("");

                alarm_text = T1F;
                alarm_format.alarm_text = alarm_text;
                alarm_format.alarm_comment = alarm_text;
                alarm_format.sys_type = "OEE_EQP_T1F";
                this.Alarm_create_xml(alarm_format, "Holiday", "Holiday_OEE_EQP_T1F_SMS");
                sw.WriteLine("Holiday_OEE_EQP_T1F_SMS log finish");
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

        sw.WriteLine(DateTime.Now.ToString("u") + "OEE_EQP_SMS Program End");
        sw.WriteLine("");

        sw.Close();

        func.delete_log_dir(Server.MapPath(".") + "\\File\\", "*.*", -60);
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);

        //func.delete_log_dir(Server.MapPath("..\\") + "\\LOG\\", -60);
        //DeleteLogFile("");

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
        //2009-08-10 Original Version with OEE
        //			string sp_body ="<font size=5 color=red > <b> CIM 電子報系統 -- "+ DateTime.Today.ToString("MM/dd") +" T0/T1/LH/CN_STN/CTP 生產良率資訊快遞</b></font><br><br>" +
        //				"<br> <b> 1. 生產良率資訊總表 </b>" +
        //				"<br>" + msgtable +
        //				"<br> <b> 2.  生產良率資訊細項連結 </a></b>"  +
        //				"<br> <b> 2.1 <a href='http://172.16.15.25/IS/EIP/fabinfo.asp' target='_blank'> 竹南區連結 </a></b> " +
        //				"<br> <b> 2.2 <a href='http://10.56.131.19/IS/EIP/fabinfo.asp' target='_blank'> 龍華區連結 </a></b><br> " +
        //				"<br> <b> 3. T0/T1 OEE KPI資訊總表 </b>" +
        //				"<br>" + msgtable_OEE_KPI +				
        //				"<br> <b> 4.  OEE KPI資訊細項連結 </a></b>"  +
        //				"<br> <b> 4.1 <a href='http://t1cimweb/oeeweb2/main.aspx' target='_blank'> OEE KPI連結 </a></b> " +
        //				"<br> <br><b> 5. 假日簡訊資訊細項連結 </b> " +
        //				"<br><b> 5.1 <a href='http://172.16.15.25/RuleExplain.htm' target='_blank'> 簡訊連結 </a></b>" +				
        //				"<br><br> <b>5.2.1 T0/T1簡訊內容如下 :<br>"+s+ "</b>" +
        //				"<br><br> <b>5.2.2 CN_STN簡訊內容如下 :<br>"+stn+ "</b>" +				
        //				"<br><font size=3 color=blue >以上資訊若當日無值則以空白表示 </font><br>" +
        //				"<br> <b> 6. 異常問題排除 </b> " +  
        //				"<br>此為CIM電子報系統自動寄發之信件，<br>如需CIM服務， " +
        //				"請洽 徐展文(62404) 蘇智宏(63590)。</font> <br><br>" ;



        //EDA KPI
        //string sql_EDA_OUTPUT = "", sql_EDA_DEFECT = "";
        //string msgtable_EDA_KPI = "";
        //string aryArrayDefect = " PCMP0, PCM09, PCDB0, PCDB9, PCLV1, PCDBE, PCDBA, PCDB5 ";
        //string aryCellDefect = " PCC07, PCDB1, PCDK1, PCM21";
        //string aryCFDefect = " PCCF1, PCM03, PCM14, PCMH1, PCMV1, PCCFR, PCCFB, PCCFG";

        //sql_EDA_OUTPUT = " select Product,max(total_count) Output, nvl(max(g1_ratio), 0) || '%' g1_ratio, nvl(max(g2_ratio), 0)|| '%' g2_ratio, " +
        //    " nvl(max(g3_ratio), 0)|| '%' g3_ratio " +
        //    " from (select product_id Product, grade, decode(grade, 'G1', defect_ratio) g1_ratio, " +
        //    " decode(grade, 'G2', defect_ratio) g2_ratio, decode(grade, 'G3', defect_ratio) g3_ratio, " +
        //    " total_count  from ( " +
        //    " select product_id, grade, sum(defect_count) defect_count, max(total_count) total_count, " +
        //    " round(sum(defect_count) / max(total_count), 4)*100 defect_ratio " +
        //    " from ( " +
        //    " select product_id, grade, defect_count, sum(defect_count) over(partition by product_id) total_count " +
        //    " from (select to_char(to_date(datetm, 'yyyymmddhh24') - 7 / 24, 'yyyymmdd') date_key, " +
        //    " t.product_id,t.grade,t.defect_count " +
        //    " from qe_daily_cell_defect_t t where t.step_id = '7300' " +
        //    " and datetm between to_char(sysdate-1,'YYYYMMDD')||'07' and to_char(sysdate,'YYYYMMDD')||'06')) " +
        //    " group by product_id, grade))  group by Product";

        //sql_EDA_DEFECT = " select * from ( select product_id, defect_code, sum(defect_count) defect_count, " +
        //    " max(total_count) total_count, round(sum(defect_count) / max(total_count), 5) * 100 defect_ratio " +
        //    " from (select product_id, defect_code, defect_count, sum(defect_count) over(partition by product_id) total_count " +
        //    " from (select to_char(to_date(datetm, 'yyyymmddhh24') - 7 / 24,'yyyymmdd') date_key, " +
        //    " t.product_id, t.defect_code, t.defect_count " +
        //    " from qe_daily_cell_defect_t t " +
        //    " where t.step_id = '7300' " +
        //    " and datetm between to_char(sysdate-1,'YYYYMMDD')||'07' and to_char(sysdate,'YYYYMMDD')||'06')) " +
        //    " where defect_code not in ('PC000')  group by product_id, defect_code ) order by product_id , defect_ratio desc ";

        //DataSet ds_EDA_OUTPUT, ds_EDA_DEFECT;
        //ds_EDA_OUTPUT = db.GetDataset(sql_EDA_OUTPUT, 4);
        //ds_EDA_DEFECT = db.GetDataset(sql_EDA_DEFECT, 4);

        //ds_EDA_OUTPUT.Tables[0].Columns.Add("ARRAY Defect");
        //ds_EDA_OUTPUT.Tables[0].Columns.Add("Cell Defect");
        //ds_EDA_OUTPUT.Tables[0].Columns.Add("CF Defect");

        //for (int ii = 0; ii < ds_EDA_OUTPUT.Tables[0].Rows.Count; ii++)
        //{

        //    for (int j = 0; j < ds_EDA_DEFECT.Tables[0].Rows.Count; j++)
        //    {
        //        if (ds_EDA_DEFECT.Tables[0].Rows[j]["product_id"].ToString() == ds_EDA_OUTPUT.Tables[0].Rows[ii]["Product"].ToString())
        //        {
        //            if (aryArrayDefect.IndexOf(ds_EDA_DEFECT.Tables[0].Rows[j]["defect_code"].ToString()) > 0)
        //            {
        //                if (Convert.ToDouble(ds_EDA_DEFECT.Tables[0].Rows[j]["defect_ratio"]) > 0.5)
        //                    ds_EDA_OUTPUT.Tables[0].Rows[ii]["ARRAY Defect"] = "<font color='red'> " + ds_EDA_OUTPUT.Tables[0].Rows[ii]["ARRAY Defect"] + ds_EDA_DEFECT.Tables[0].Rows[j]["defect_code"].ToString() + ":" + ds_EDA_DEFECT.Tables[0].Rows[j]["defect_ratio"].ToString() + "%</font> <br>";
        //                else
        //                    ds_EDA_OUTPUT.Tables[0].Rows[ii]["ARRAY Defect"] = ds_EDA_OUTPUT.Tables[0].Rows[ii]["ARRAY Defect"] + ds_EDA_DEFECT.Tables[0].Rows[j]["defect_code"].ToString() + ":" + ds_EDA_DEFECT.Tables[0].Rows[j]["defect_ratio"].ToString() + "%<br>";
        //            }
        //            if (aryCellDefect.IndexOf(ds_EDA_DEFECT.Tables[0].Rows[j]["defect_code"].ToString()) > 0)
        //            {
        //                if (Convert.ToDouble(ds_EDA_DEFECT.Tables[0].Rows[j]["defect_ratio"]) > 0.5)
        //                    ds_EDA_OUTPUT.Tables[0].Rows[ii]["Cell Defect"] = "<font color='red'> " + ds_EDA_OUTPUT.Tables[0].Rows[ii]["Cell Defect"].ToString() + ds_EDA_DEFECT.Tables[0].Rows[j]["defect_code"].ToString() + ":" + ds_EDA_DEFECT.Tables[0].Rows[j]["defect_ratio"] + "%</font><br>";
        //                else
        //                    ds_EDA_OUTPUT.Tables[0].Rows[ii]["Cell Defect"] = ds_EDA_OUTPUT.Tables[0].Rows[ii]["Cell Defect"].ToString() + ds_EDA_DEFECT.Tables[0].Rows[j]["defect_code"].ToString() + ":" + ds_EDA_DEFECT.Tables[0].Rows[j]["defect_ratio"] + "%<br>";
        //            }
        //            if (aryCFDefect.IndexOf(ds_EDA_DEFECT.Tables[0].Rows[j]["defect_code"].ToString()) > 0)
        //            {
        //                if (Convert.ToDouble(ds_EDA_DEFECT.Tables[0].Rows[j]["defect_ratio"]) > 0.5)
        //                    ds_EDA_OUTPUT.Tables[0].Rows[ii]["CF Defect"] = "<font color='red'> " + ds_EDA_OUTPUT.Tables[0].Rows[ii]["CF Defect"] + ds_EDA_DEFECT.Tables[0].Rows[j]["defect_code"].ToString() + ":" + ds_EDA_DEFECT.Tables[0].Rows[j]["defect_ratio"].ToString() + "%</font><br>";
        //                else
        //                    ds_EDA_OUTPUT.Tables[0].Rows[ii]["CF Defect"] = ds_EDA_OUTPUT.Tables[0].Rows[ii]["CF Defect"] + ds_EDA_DEFECT.Tables[0].Rows[j]["defect_code"].ToString() + ":" + ds_EDA_DEFECT.Tables[0].Rows[j]["defect_ratio"].ToString() + "%<br>";
        //            }
        //        }
        //    }
        //    //ds_EDA_OUTPUT.Tables[0].Rows[ii]["ARRAY Defect"] = ds_EDA_OUTPUT.Tables[0].Rows[ii]["ARRAY Defect"].ToString().Substring(1,ds_EDA_OUTPUT.Tables[0].Rows[ii]["ARRAY Defect"].ToString().Length-4) ;
        //    //ds_EDA_OUTPUT.Tables[0].Rows[ii]["Cell Defect"]  = ds_EDA_OUTPUT.Tables[0].Rows[ii]["Cell Defect"].ToString().Substring(1,ds_EDA_OUTPUT.Tables[0].Rows[ii]["Cell Defect"].ToString().Length-4) ;
        //    //ds_EDA_OUTPUT.Tables[0].Rows[ii]["CF Defect"]    =  ds_EDA_OUTPUT.Tables[0].Rows[ii]["CF Defect"].ToString().Substring(1,ds_EDA_OUTPUT.Tables[0].Rows[ii]["CF Defect"].ToString().Length-4) ;
        //}




        //msgtable_EDA_KPI = sp.gethtml_4(ds_EDA_OUTPUT.Tables[0]);



        //string sp_body = "<font size=5 color=red > <b> CIM 電子報系統 -- " + DateTime.Today.ToString("MM/dd") + " T0/T1/LH/CTP 生產良率資訊快遞</b></font><br><br>" +
        //    " <b> 1. 生產良率資訊總表 (<a href='http://172.16.15.25/IS/EIP/fabinfo.asp' target='_blank'>竹南區連結</a>；<a href='http://10.56.131.19/IS/EIP/fabinfo.asp' target='_blank'>龍華區連結</a>；<a href='http://172.16.15.25/RuleExplain.htm' target='_blank'>生產良率定義</a></b>)" +
        //    "<br>" + msgtable +
        //    "<br> <b> 2. T0/T1 OEE KPI資訊總表 (<a href='http://172.16.12.83:8085/oeeweb2/main.aspx' target='_blank'>OEE KPI連結</a>)</b>" +
        //    "<br>" + msgtable_OEE_KPI +
        //    "<br> <b> 3. Module Defect 成因 Yield Summary (<a href='http://t1cimweb02/QMS/report/module_defect_cause.aspx' target='_blank'>Module Defect連結</a>) </b>" +
        //    "<br>" + msgtable_EDA_KPI +
        //    "<br> <b> 4. 異常問題排除 </b> " +
        //    "<br>此為CIM電子報系統自動寄發之信件，<br>如需CIM服務， " +
        //    "請洽 蘇智宏(64177) 。</font> <br><br>";





        //sp_to = "Jeffrey.Yang@CHIMEI-INNOLUX.COM,Kevin.Chung@CHIMEI-INNOLUX.COM,Chipao.Chu@CHIMEI-INNOLUX.COM,Janson.Deng@CHIMEI-INNOLUX.COM,Richard.Chuang@CHIMEI-INNOLUX.COM,James.Lin@CHIMEI-INNOLUX.COM,Snoopy.Hsieh@CHIMEI-INNOLUX.COM,William.Wang@CHIMEI-INNOLUX.COM,Matt.Wan@CHIMEI-INNOLUX.COM,Mark.Tseng@CHIMEI-INNOLUX.COM,Jemin.Lin@CHIMEI-INNOLUX.COM,Lewis.Lu@CHIMEI-INNOLUX.COM,Wimbly.Chang@CHIMEI-INNOLUX.COM,Nicholas.Chou@CHIMEI-INNOLUX.COM,Phyllis.Lu@CHIMEI-INNOLUX.COM,Simon01.Lin@CHIMEI-INNOLUX.COM,Joey.Hung@CHIMEI-INNOLUX.COM,Fleming.Lin@CHIMEI-INNOLUX.COM,Sean.Chen@CHIMEI-INNOLUX.COM,Terry01.Liu@CHIMEI-INNOLUX.COM,Tommy.Wang@CHIMEI-INNOLUX.COM,Perry.Su@CHIMEI-INNOLUX.COM,Ken.Kuo@CHIMEI-INNOLUX.COM,Jc.Kung@CHIMEI-INNOLUX.COM,Jimmy.Hsu@CHIMEI-INNOLUX.COM,Lifeng.Chiu@CHIMEI-INNOLUX.COM,Sam.Chang@CHIMEI-INNOLUX.COM,CC.Chan@CHIMEI-INNOLUX.COM,YY.Fan@CHIMEI-INNOLUX.COM,Hsinwei.Huang@CHIMEI-INNOLUX.COM,Lynn.Liao@CHIMEI-INNOLUX.COM,Mina.Hu@CHIMEI-INNOLUX.COM,Yujen.Su@CHIMEI-INNOLUX.COM,CT.Lai@CHIMEI-INNOLUX.COM,Ian.Chan@CHIMEI-INNOLUX.COM,Anders.Lin@CHIMEI-INNOLUX.COM,TH.Chang@CHIMEI-INNOLUX.COM,Mingfu.Chen@CHIMEI-INNOLUX.COM,Morpher.Liu@CHIMEI-INNOLUX.COM,CH.Chang@CHIMEI-INNOLUX.COM,Jane01.Hsu@CHIMEI-INNOLUX.COM,Scott.Chen@CHIMEI-INNOLUX.COM,Ramon.Lan@CHIMEI-INNOLUX.COM,Benson.Lee@CHIMEI-INNOLUX.COM,HH.Huang@CHIMEI-INNOLUX.COM,YJ.Su@CHIMEI-INNOLUX.COM,Willson.Lei@CHIMEI-INNOLUX.COM,Eric.Chu@CHIMEI-INNOLUX.COM,Chhuang.Huang@CHIMEI-INNOLUX.COM,CC.Huang@CHIMEI-INNOLUX.COM,mike.lin@CHIMEI-INNOLUX.COM,chenshin.lee@CHIMEI-INNOLUX.COM,Petty.Dong@CHIMEI-INNOLUX.COM,Eartha.Wang@CHIMEI-INNOLUX.COM,MILTON.CHANG@CHIMEI-INNOLUX.COM";
        //sp_cc = "YS.Hsiao@CHIMEI-INNOLUX.COM,grace.wu@CHIMEI-INNOLUX.COM,WH.Chen@CHIMEI-INNOLUX.COM,Bunny.su@CHIMEI-INNOLUX.COM,Brandon.Lin@CHIMEI-INNOLUX.COM,Miles.Lin@CHIMEI-INNOLUX.COM,EDDIE.LU@CHIMEI-INNOLUX.COM,LEON.LIN@CHIMEI-INNOLUX.COM,HAOCHAN.CHEN@CHIMEI-INNOLUX.COM,MARKSULO.WANG@CHIMEI-INNOLUX.COM,DAY.YU@CHIMEI-INNOLUX.COM,SATYR.LIU@CHIMEI-INNOLUX.COM,WENJAY.LIN@CHIMEI-INNOLUX.COM,SIMTH.CHANG@CHIMEI-INNOLUX.COM,CHUNGHUI.CHEN@CHIMEI-INNOLUX.COM,SAM.LIU@CHIMEI-INNOLUX.COM,ALLEN.KUO@CHIMEI-INNOLUX.COM,ALLEN.JUANG@CHIMEI-INNOLUX.COM,DUNCAN.TAO@CHIMEI-INNOLUX.COM";
        ////EDDIE.LU@CHIMEI-INNOLUX.COM,LEON.LIN@CHIMEI-INNOLUX.COM,HAOCHAN.CHEN@CHIMEI-INNOLUX.COM,MARKSULO.WANG@CHIMEI-INNOLUX.COM,DAY.YU@CHIMEI-INNOLUX.COM,SATYR.LIU@CHIMEI-INNOLUX.COM,WENJAY.LIN@CHIMEI-INNOLUX.COM,SIMTH.CHANG@CHIMEI-INNOLUX.COM,CHUNGHUI.CHEN@CHIMEI-INNOLUX.COM,SAM.LIU@CHIMEI-INNOLUX.COM,ALLEN.KUO@CHIMEI-INNOLUX.COM,ALLEN.JUANG@CHIMEI-INNOLUX.COM





        ////sp_to="";
        ////sp_cc = "BUNNY.SU@CHIMEI-INNOLUX.COM" ;

        //sp.Send_mail(sp_body, "CIM CENTRAL MAIL SYSTEM<cimalarm@CHIMEI-INNOLUX.COM>", sp_to, sp_cc, "[CIM 電子報系統] : " + DateTime.Today.ToString("MM/dd") + " T0/T1/LH/CN_STN/CTP/CL1 生產良率資訊快遞", 0, null);
        // Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
    }
}
