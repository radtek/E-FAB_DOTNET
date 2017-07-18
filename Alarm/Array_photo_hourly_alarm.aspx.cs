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
using System.Xml;



public partial class Alarm_Array_photo_hourly_alarm : System.Web.UI.Page
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
    Int32 counter_oscar=0;
    func xmlw = new func();
    func.alarm_format alarm_format = new func.alarm_format();
    int now_hour = Convert.ToInt32(DateTime.Now.ToString("HH"));//抓取執行當下小時
    int now_min = Convert.ToInt32(DateTime.Now.ToString("mm"));//抓取執行當下分鐘
    

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            alarm_format.trx_id = "1";
            alarm_format.type_id = "1";
            alarm_format.fab_id = "T1ARRAY";
            alarm_format.sys_type = "ARRAY_PHTO";
            alarm_format.eq_id = "1234";
            alarm_format.alarm_id = "1111";
            alarm_format.alarm_text = "T1 Alarm Server Test";
            alarm_format.mail_contenttype = "T";
            alarm_format.alarm_comment = "T1 Alarm Server Test Test";
            alarm_format.pc_ip = "1";
            alarm_format.pc_name = "1";
            alarm_format.operator1 = "1";
            alarm_format.issue_date = "1";
        
        
        }

     

       


        string[] condition1 = { "1150", "1250", "1350", "1450", "1650", "S150", "S250", "S350", "S450", "S550", "S850" };
        string[] condition2 = { "1138B", "1238B", "1338B", "1438B", "1638B", "S138B", "S238B", "S338B", "S438B", "S538B", "S838B" };
        string[] condition3 = { "250", "250", "250", "200","100", "200", "200", "200", "200", "200","200" };
        string[] condition4 = { "300", "350", "350", "300","300", "300", "300", "300", "300", "300","300" };

        for (int j = 0; j <= condition1.Length-1; j++)
        {

            if (j >= 0)
            {
                func.write_log("Array Photo hourly_CHK log " + condition1[j].ToString(), Server.MapPath("..\\") + "\\LOG\\", "log");

                //   S150                    S138B                      200                      150      
                wip_chk(condition1[j].ToString(), condition2[j].ToString(), condition3[j].ToString(), condition4[j].ToString());
               
            
            }


        }

        #region MyRegion
        ////*** 檢查 1150  1138B
        //string sql_T0A = "  select * from                                                             ";
        //sql_T0A = sql_T0A + " (select t.step_seq,sum(t.glass_qty) as total_wip from lot t                ";
        //sql_T0A = sql_T0A + " where t.prod_name is not null                                              ";
        //sql_T0A = sql_T0A + "   and t.fab='Fab1'                                                         ";
        //sql_T0A = sql_T0A + "   and t.shop='T1Array'                                                     ";
        //sql_T0A = sql_T0A + "   and t.terminate_dttm is null                                             ";
        //sql_T0A = sql_T0A + "   and t.array_ship_dttm is null                                            ";
        //sql_T0A = sql_T0A + "   and t.lot_id <> ' ALL'                                                   ";
        //sql_T0A = sql_T0A + "   and nvl(t.inv_state,' ') <> 'Created'                                    ";
        //sql_T0A = sql_T0A + "   and nvl(t.step_seq,'0') in ('" + condition1[0] + "')                                      ";
        //sql_T0A = sql_T0A + " group by t.step_seq                                                        ";
        //sql_T0A = sql_T0A + " )t1,                                                                       ";
        //sql_T0A = sql_T0A + " (select max(wip) as diff_pro_MAX_wip from (                                ";
        //sql_T0A = sql_T0A + " select ot.prod_name,ot.wip                                                 ";
        //sql_T0A = sql_T0A + " from (                                                                     ";
        //sql_T0A = sql_T0A + " select t.step_seq,t.prod_name,sum(t.glass_qty) as wip from lot t           ";
        //sql_T0A = sql_T0A + " where t.prod_name is not null                                              ";
        //sql_T0A = sql_T0A + "   and t.fab='Fab1'                                                         ";
        //sql_T0A = sql_T0A + "   and t.shop='T1Array'                                                     ";
        //sql_T0A = sql_T0A + "   and t.terminate_dttm is null                                             ";
        //sql_T0A = sql_T0A + "   and t.array_ship_dttm is null                                            ";
        //sql_T0A = sql_T0A + "   and t.lot_id <> ' ALL'                                                   ";
        //sql_T0A = sql_T0A + "   and nvl(t.inv_state,' ') <> 'Created'                                    ";
        //sql_T0A = sql_T0A + "   and nvl(t.step_seq,'0') in ('" + condition2[0] + "')                                     ";
        //sql_T0A = sql_T0A + " group by t.step_seq,t.prod_name                                            ";
        //sql_T0A = sql_T0A + " ) ot))t2                                                                   ";
        //// add by oscar 20090910 for when STEP NO DATA 
        //sql_T0A = sql_T0A + "union all select '1150',0,0 from dual";



        //DataSet ds_T0A;

        //ds_T0A = func.get_dataSet_access(sql_T0A, conn);




        //if (ds_T0A.Tables[0].Rows.Count>=2 && !ds_T0A.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString().Equals("") )


        //{
        //   if (Convert.ToInt16(ds_T0A.Tables[0].Rows[0]["total_wip"].ToString()) < 250 && Convert.ToInt16(ds_T0A.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString()) > 150)
        //        if(1==1)
        //{

        //    string alarm_text = "1150 step less 250Sub 1138B greater 150Sub";
        //    alarm_format.alarm_text = alarm_text;
        //    alarm_format.alarm_comment = alarm_text;
        //    //由時間 去判斷 發送的 event  
        //    // 00~09    day1
        //    // 09~19    day3
        //    // 09~24    day2
        //    // 19~24    day4
        //    //if (1 == 1)
        //    if (now_hour >= 0 && now_hour < 9)
        //    {
        //       // this.create_xml(alarm_text, true, "1150", "ARRAY_PH_WIP_DAY1");

        //        this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //        sw.WriteLine("1150 log finish");
        //        sw.WriteLine("");

        //    }

        //    //if (1 == 1)
        //    if (now_hour >= 9 && now_hour < 19)
        //    {
        //        //this.create_xml(alarm_text, true, "1150", "ARRAY_PH_WIP_DAY3");
        //        this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");

        //        sw.WriteLine("1150 log finish");
        //        sw.WriteLine("");

        //    }

        //    //if (1 == 1)
        //    //if (now_hour >= 9 && now_hour <= 24)
        //    //{
        //    //    //this.create_xml(alarm_text, true, "1150", "ARRAY_PH_WIP_DAY2");
        //    //    this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //    //    sw.WriteLine("1150 log finish");
        //    //    sw.WriteLine("");

        //    //}

        //    //if (1 == 1)
        //    if (now_hour >= 19 && now_hour <= 24)
        //    {
        //        //this.create_xml(alarm_text, true, "1150", "ARRAY_PH_WIP_DAY4");
        //        this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //        sw.WriteLine("1150 log finish");
        //        sw.WriteLine("");

        //    }






        //}

        //}

        ////int abcd0 = Convert.ToInt16(ds_T0A.Tables[0].Rows[0]["total_wip"].ToString());
        ////int efgh0 = Convert.ToInt16(ds_T0A.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString());
        ////if (1 == 1)



        ////*** 檢查 1150  1138B END


        ////*** 檢查 1250  1238B  Start
        //string sql_T0A1 = "  select * from                                                             ";
        //sql_T0A1 = sql_T0A1 + " (select t.step_seq,sum(t.glass_qty) as total_wip from lot t                ";
        //sql_T0A1 = sql_T0A1 + " where t.prod_name is not null                                              ";
        //sql_T0A1 = sql_T0A1 + "   and t.fab='Fab1'                                                         ";
        //sql_T0A1 = sql_T0A1 + "   and t.shop='T1Array'                                                     ";
        //sql_T0A1 = sql_T0A1 + "   and t.terminate_dttm is null                                             ";
        //sql_T0A1 = sql_T0A1 + "   and t.array_ship_dttm is null                                            ";
        //sql_T0A1 = sql_T0A1 + "   and t.lot_id <> ' ALL'                                                   ";
        //sql_T0A1 = sql_T0A1 + "   and nvl(t.inv_state,' ') <> 'Created'                                    ";
        //sql_T0A1 = sql_T0A1 + "   and nvl(t.step_seq,'0') in ('" + condition1[1] + "')                                      ";
        //sql_T0A1 = sql_T0A1 + " group by t.step_seq                                                        ";
        //sql_T0A1 = sql_T0A1 + " )t1,                                                                       ";
        //sql_T0A1 = sql_T0A1 + " (select max(wip) as diff_pro_MAX_wip from (                                ";
        //sql_T0A1 = sql_T0A1 + " select ot.prod_name,ot.wip                                                 ";
        //sql_T0A1 = sql_T0A1 + " from (                                                                     ";
        //sql_T0A1 = sql_T0A1 + " select t.step_seq,t.prod_name,sum(t.glass_qty) as wip from lot t           ";
        //sql_T0A1 = sql_T0A1 + " where t.prod_name is not null                                              ";
        //sql_T0A1 = sql_T0A1 + "   and t.fab='Fab1'                                                         ";
        //sql_T0A1 = sql_T0A1 + "   and t.shop='T1Array'                                                     ";
        //sql_T0A1 = sql_T0A1 + "   and t.terminate_dttm is null                                             ";
        //sql_T0A1 = sql_T0A1 + "   and t.array_ship_dttm is null                                            ";
        //sql_T0A1 = sql_T0A1 + "   and t.lot_id <> ' ALL'                                                   ";
        //sql_T0A1 = sql_T0A1 + "   and nvl(t.inv_state,' ') <> 'Created'                                    ";
        //sql_T0A1 = sql_T0A1 + "   and nvl(t.step_seq,'0') in ('" + condition2[1] + "')                                     ";
        //sql_T0A1 = sql_T0A1 + " group by t.step_seq,t.prod_name                                            ";
        //sql_T0A1 = sql_T0A1 + " ) ot))t2                                                                   ";
        //// add by oscar 20090910 for when STEP NO DATA 
        //sql_T0A1 = sql_T0A1 + "union all select '1250',0,0 from dual";



        //DataSet ds_T0A1;
        //ds_T0A1 = func.get_dataSet_access(sql_T0A1, conn);
        ////int abcd1 = Convert.ToInt16(ds_T0A1.Tables[0].Rows[0]["total_wip"].ToString());
        ////int efgh1 = Convert.ToInt16(ds_T0A1.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString());
        //if (ds_T0A1.Tables[0].Rows.Count >= 2 && !ds_T0A1.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString().Equals(""))
        //{
        //    if (Convert.ToInt16(ds_T0A1.Tables[0].Rows[0]["total_wip"].ToString()) < 250 && Convert.ToInt16(ds_T0A1.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString()) > 150)
        //    {

        //        string alarm_text1 = "1250 step less 250Sub 1238B step greater 150Sub";
        //        alarm_format.alarm_text = alarm_text1;
        //        alarm_format.alarm_comment = alarm_text1;
        //        // sw.Close();
        //        //由時間 去判斷 發送的 event  
        //        // 00~09    day1
        //        // 09~19    day2
        //        // 09~24    day3
        //        // 19~24    day4
        //        //if (1 == 1)
        //        if (now_hour >= 0 && now_hour < 9)
        //        {
        //            //this.create_xml(alarm_text1, true, "1250", "ARRAY_PH_WIP_DAY1");
        //            this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //            sw.WriteLine("1250 log finish");
        //            sw.WriteLine("");

        //        }

        //        //if (1 == 1)
        //        if (now_hour >= 9 && now_hour < 19)
        //        {
        //            //this.create_xml(alarm_text1, true, "1250", "ARRAY_PH_WIP_DAY3");
        //            this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //            sw.WriteLine("1250 log finish");
        //            sw.WriteLine("");

        //        }

        //        //if (1 == 1)
        //        //if (now_hour >= 9 && now_hour <= 24)
        //        //{
        //        //    //this.create_xml(alarm_text1, true, "1250", "ARRAY_PH_WIP_DAY2");
        //        //    this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //        //    sw.WriteLine("1250 log finish");
        //        //    sw.WriteLine("");

        //        //}

        //        //if (1 == 1)
        //        if (now_hour >= 19 && now_hour <= 24)
        //        {
        //            //this.create_xml(alarm_text1, true, "1250", "ARRAY_PH_WIP_DAY4");
        //            this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //            sw.WriteLine("1250 log finish");
        //            sw.WriteLine("");

        //        }

        //    }

        //}

        ////if (1 == 1)


        ////*** 檢查 1250  1238B  End


        ////*** 檢查 1350  1338B  Start
        //string sql_T0A2 = "  select * from                                                             ";
        //sql_T0A2 = sql_T0A2 + " (select t.step_seq,sum(t.glass_qty) as total_wip from lot t                ";
        //sql_T0A2 = sql_T0A2 + " where t.prod_name is not null                                              ";
        //sql_T0A2 = sql_T0A2 + "   and t.fab='Fab1'                                                         ";
        //sql_T0A2 = sql_T0A2 + "   and t.shop='T1Array'                                                     ";
        //sql_T0A2 = sql_T0A2 + "   and t.terminate_dttm is null                                             ";
        //sql_T0A2 = sql_T0A2 + "   and t.array_ship_dttm is null                                            ";
        //sql_T0A2 = sql_T0A2 + "   and t.lot_id <> ' ALL'                                                   ";
        //sql_T0A2 = sql_T0A2 + "   and nvl(t.inv_state,' ') <> 'Created'                                    ";
        //sql_T0A2 = sql_T0A2 + "   and nvl(t.step_seq,'0') in ('" + condition1[2] + "')                                      ";
        //sql_T0A2 = sql_T0A2 + " group by t.step_seq                                                        ";
        //sql_T0A2 = sql_T0A2 + " )t1,                                                                       ";
        //sql_T0A2 = sql_T0A2 + " (select max(wip) as diff_pro_MAX_wip from (                                ";
        //sql_T0A2 = sql_T0A2 + " select ot.prod_name,ot.wip                                                 ";
        //sql_T0A2 = sql_T0A2 + " from (                                                                     ";
        //sql_T0A2 = sql_T0A2 + " select t.step_seq,t.prod_name,sum(t.glass_qty) as wip from lot t           ";
        //sql_T0A2 = sql_T0A2 + " where t.prod_name is not null                                              ";
        //sql_T0A2 = sql_T0A2 + "   and t.fab='Fab1'                                                         ";
        //sql_T0A2 = sql_T0A2 + "   and t.shop='T1Array'                                                     ";
        //sql_T0A2 = sql_T0A2 + "   and t.terminate_dttm is null                                             ";
        //sql_T0A2 = sql_T0A2 + "   and t.array_ship_dttm is null                                            ";
        //sql_T0A2 = sql_T0A2 + "   and t.lot_id <> ' ALL'                                                   ";
        //sql_T0A2 = sql_T0A2 + "   and nvl(t.inv_state,' ') <> 'Created'                                    ";
        //sql_T0A2 = sql_T0A2 + "   and nvl(t.step_seq,'0') in ('" + condition2[2] + "')                                     ";
        //sql_T0A2 = sql_T0A2 + " group by t.step_seq,t.prod_name                                            ";
        //sql_T0A2 = sql_T0A2 + " ) ot))t2                                                                   ";
        //// add by oscar 20090910 for when STEP NO DATA 
        //sql_T0A2 = sql_T0A2 + "union all select '1350',0,0 from dual";



        //DataSet ds_T0A2;
        //ds_T0A2 = func.get_dataSet_access(sql_T0A2, conn);
        //int abcd2 = Convert.ToInt16(ds_T0A2.Tables[0].Rows[0]["total_wip"].ToString());
        //int efgh2 = Convert.ToInt16(ds_T0A2.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString());
        //if (ds_T0A2.Tables[0].Rows.Count >= 2 && !ds_T0A2.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString().Equals(""))
        //{
        //    //if(1==1)
        //    if (Convert.ToInt16(ds_T0A2.Tables[0].Rows[0]["total_wip"].ToString()) < 250 && Convert.ToInt16(ds_T0A2.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString()) > 150)
        //    {

        //        string alarm_text2 = "1350 step less 250Sub 1338B step greater 150Sub";
        //        alarm_format.alarm_text = alarm_text2;
        //        alarm_format.alarm_comment = alarm_text2;
        //        // sw.Close();
        //        //由時間 去判斷 發送的 event  
        //        // 00~09    day1
        //        // 09~19    day2
        //        // 09~24    day3
        //        // 19~24    day4
        //        //if (1 == 1)
        //        if (now_hour >= 0 && now_hour < 9)
        //        {
        //            //this.create_xml(alarm_text2, true, "1350", "ARRAY_PH_WIP_DAY1");
        //            this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //            sw.WriteLine("1350 log finish");
        //            sw.WriteLine("");

        //        }

        //        //if (1 == 1)
        //        if (now_hour >= 9 && now_hour < 19)
        //        {
        //            //this.create_xml(alarm_text2, true, "1350", "ARRAY_PH_WIP_DAY3");
        //            this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //            sw.WriteLine("1350 log finish");
        //            sw.WriteLine("");

        //        }

        //        //if (1 == 1)
        //        //if (now_hour >= 9 && now_hour <= 24)
        //        //{
        //        //    //this.create_xml(alarm_text2, true, "1350", "ARRAY_PH_WIP_DAY2");
        //        //    this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //        //    sw.WriteLine("1350 log finish");
        //        //    sw.WriteLine("");

        //        //}

        //        //if (1 == 1)
        //        if (now_hour >= 19 && now_hour <= 24)
        //        {
        //            //this.create_xml(alarm_text2, true, "1350", "ARRAY_PH_WIP_DAY4");
        //            this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //            sw.WriteLine("1350 log finish");
        //            sw.WriteLine("");

        //        }

        //    }


        //}


        ////*** 檢查 1350  1338B  End



        ////*** 檢查 1450  1438B  Start
        //string sql_T0A3 = "  select * from                                                             ";
        //sql_T0A3 = sql_T0A3 + " (select t.step_seq,sum(t.glass_qty) as total_wip from lot t                ";
        //sql_T0A3 = sql_T0A3 + " where t.prod_name is not null                                              ";
        //sql_T0A3 = sql_T0A3 + "   and t.fab='Fab1'                                                         ";
        //sql_T0A3 = sql_T0A3 + "   and t.shop='T1Array'                                                     ";
        //sql_T0A3 = sql_T0A3 + "   and t.terminate_dttm is null                                             ";
        //sql_T0A3 = sql_T0A3 + "   and t.array_ship_dttm is null                                            ";
        //sql_T0A3 = sql_T0A3 + "   and t.lot_id <> ' ALL'                                                   ";
        //sql_T0A3 = sql_T0A3 + "   and nvl(t.inv_state,' ') <> 'Created'                                    ";
        //sql_T0A3 = sql_T0A3 + "   and nvl(t.step_seq,'0') in ('" + condition1[3] + "')                                      ";
        //sql_T0A3 = sql_T0A3 + " group by t.step_seq                                                        ";
        //sql_T0A3 = sql_T0A3 + " )t1,                                                                       ";
        //sql_T0A3 = sql_T0A3 + " (select max(wip) as diff_pro_MAX_wip from (                                ";
        //sql_T0A3 = sql_T0A3 + " select ot.prod_name,ot.wip                                                 ";
        //sql_T0A3 = sql_T0A3 + " from (                                                                     ";
        //sql_T0A3 = sql_T0A3 + " select t.step_seq,t.prod_name,sum(t.glass_qty) as wip from lot t           ";
        //sql_T0A3 = sql_T0A3 + " where t.prod_name is not null                                              ";
        //sql_T0A3 = sql_T0A3 + "   and t.fab='Fab1'                                                         ";
        //sql_T0A3 = sql_T0A3 + "   and t.shop='T1Array'                                                     ";
        //sql_T0A3 = sql_T0A3 + "   and t.terminate_dttm is null                                             ";
        //sql_T0A3 = sql_T0A3 + "   and t.array_ship_dttm is null                                            ";
        //sql_T0A3 = sql_T0A3 + "   and t.lot_id <> ' ALL'                                                   ";
        //sql_T0A3 = sql_T0A3 + "   and nvl(t.inv_state,' ') <> 'Created'                                    ";
        //sql_T0A3 = sql_T0A3 + "   and nvl(t.step_seq,'0') in ('" + condition2[3] + "')                                     ";
        //sql_T0A3 = sql_T0A3 + " group by t.step_seq,t.prod_name                                            ";
        //sql_T0A3 = sql_T0A3 + " ) ot))t2                                                                   ";
        //// add by oscar 20090910 for when STEP NO DATA 
        //sql_T0A3 = sql_T0A3 + "union all select '1450',0,0 from dual";



        //DataSet ds_T0A3;
        //ds_T0A3 = func.get_dataSet_access(sql_T0A3, conn);
        ////int abcd3 = Convert.ToInt16(ds_T0A3.Tables[0].Rows[0]["total_wip"].ToString());
        ////int efgh3 = Convert.ToInt16(ds_T0A3.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString());

        //if (ds_T0A3.Tables[0].Rows.Count >= 2 && !ds_T0A3.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString().Equals(""))
        //{
        //    // if (1 == 1)  
        //    if (Convert.ToInt16(ds_T0A3.Tables[0].Rows[0]["total_wip"].ToString()) < 250 && Convert.ToInt16(ds_T0A3.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString()) > 150)
        //    {

        //        string alarm_text3 = "1450 step less 250Sub 1438B step greater 150Sub";
        //        alarm_format.alarm_text = alarm_text3;
        //        alarm_format.alarm_comment = alarm_text3;
        //        // sw.Close();
        //        //由時間 去判斷 發送的 event  
        //        // 00~09    day1
        //        // 09~19    day2
        //        // 09~24    day3
        //        // 19~24    day4
        //        //if (1 == 1)
        //        if (now_hour >= 0 && now_hour < 9)
        //        {
        //            //this.create_xml(alarm_text3, true, "1450", "ARRAY_PH_WIP_DAY1");
        //            this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //            sw.WriteLine("1450 log finish");
        //            sw.WriteLine("");

        //        }

        //        //if (1 == 1)
        //        if (now_hour >= 9 && now_hour < 19)
        //        {
        //            //this.create_xml(alarm_text3, true, "1450", "ARRAY_PH_WIP_DAY3");
        //            this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //            sw.WriteLine("1450 log finish");
        //            sw.WriteLine("");

        //        }

        //        //if (1 == 1)
        //        //if (now_hour >= 9 && now_hour <= 24)
        //        //{
        //        //    //this.create_xml(alarm_text3, true, "1450", "ARRAY_PH_WIP_DAY2");
        //        //    this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //        //    sw.WriteLine("1450 log finish");
        //        //    sw.WriteLine("");

        //        //}

        //        //if (1 == 1)
        //        if (now_hour >= 19 && now_hour <= 24)
        //        {
        //            //this.create_xml(alarm_text3, true, "1450", "ARRAY_PH_WIP_DAY4");
        //            this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //            sw.WriteLine("1450 log finish");
        //            sw.WriteLine("");

        //        }


        //    }

        //}




        ////*** 檢查 1450  1438B  End





        ////*** 檢查 1650  1638B  Start

        //string sql_T0A4 = "  select * from                                                             ";
        //sql_T0A4 = sql_T0A4 + " (select t.step_seq,sum(t.glass_qty) as total_wip from lot t                ";
        //sql_T0A4 = sql_T0A4 + " where t.prod_name is not null                                              ";
        //sql_T0A4 = sql_T0A4 + "   and t.fab='Fab1'                                                         ";
        //sql_T0A4 = sql_T0A4 + "   and t.shop='T1Array'                                                     ";
        //sql_T0A4 = sql_T0A4 + "   and t.terminate_dttm is null                                             ";
        //sql_T0A4 = sql_T0A4 + "   and t.array_ship_dttm is null                                            ";
        //sql_T0A4 = sql_T0A4 + "   and t.lot_id <> ' ALL'                                                   ";
        //sql_T0A4 = sql_T0A4 + "   and nvl(t.inv_state,' ') <> 'Created'                                    ";
        //sql_T0A4 = sql_T0A4 + "   and nvl(t.step_seq,'0') in ('" + condition1[4] + "')                                      ";
        //sql_T0A4 = sql_T0A4 + " group by t.step_seq                                                        ";
        //sql_T0A4 = sql_T0A4 + " )t1,                                                                       ";
        //sql_T0A4 = sql_T0A4 + " (select max(wip) as diff_pro_MAX_wip from (                                ";
        //sql_T0A4 = sql_T0A4 + " select ot.prod_name,ot.wip                                                 ";
        //sql_T0A4 = sql_T0A4 + " from (                                                                     ";
        //sql_T0A4 = sql_T0A4 + " select t.step_seq,t.prod_name,sum(t.glass_qty) as wip from lot t           ";
        //sql_T0A4 = sql_T0A4 + " where t.prod_name is not null                                              ";
        //sql_T0A4 = sql_T0A4 + "   and t.fab='Fab1'                                                         ";
        //sql_T0A4 = sql_T0A4 + "   and t.shop='T1Array'                                                     ";
        //sql_T0A4 = sql_T0A4 + "   and t.terminate_dttm is null                                             ";
        //sql_T0A4 = sql_T0A4 + "   and t.array_ship_dttm is null                                            ";
        //sql_T0A4 = sql_T0A4 + "   and t.lot_id <> ' ALL'                                                   ";
        //sql_T0A4 = sql_T0A4 + "   and nvl(t.inv_state,' ') <> 'Created'                                    ";
        //sql_T0A4 = sql_T0A4 + "   and nvl(t.step_seq,'0') in ('" + condition2[4] + "')                                     ";
        //sql_T0A4 = sql_T0A4 + " group by t.step_seq,t.prod_name                                            ";
        //sql_T0A4 = sql_T0A4 + " ) ot))t2                                                                   ";
        //// add by oscar 20090910 for when STEP NO DATA 
        //sql_T0A4 = sql_T0A4 + "union all select '1650',0,0 from dual";



        //DataSet ds_T0A4;
        //ds_T0A4 = func.get_dataSet_access(sql_T0A4, conn);
        ////int abcd4 = Convert.ToInt16(ds_T0A4.Tables[0].Rows[0]["total_wip"].ToString());
        ////int efgh4 = Convert.ToInt16(ds_T0A4.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString());

        //if (ds_T0A4.Tables[0].Rows.Count >= 2 && !ds_T0A4.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString().Equals(""))
        //{
        //    //if(1==1)
        //    if (Convert.ToInt16(ds_T0A4.Tables[0].Rows[0]["total_wip"].ToString()) < 100 && Convert.ToInt16(ds_T0A4.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString()) > 250)
        //    {

        //        string alarm_text4 = "1650 step less 100Sub 1638B step greater 250Sub";
        //        alarm_format.alarm_text = alarm_text4;
        //        alarm_format.alarm_comment = alarm_text4;
        //        // sw.Close();
        //        //由時間 去判斷 發送的 event  
        //        // 00~09    day1
        //        // 09~19    day2
        //        // 09~24    day3
        //        // 19~24    day4
        //        //if (1 == 1)
        //        if (now_hour >= 0 && now_hour < 9)
        //        {
        //            //this.create_xml(alarm_text4, true, "1650", "ARRAY_PH_WIP_DAY1");
        //            this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //            sw.WriteLine("1650 log finish");
        //            sw.WriteLine("");

        //        }

        //        //if (1 == 1)
        //        if (now_hour >= 9 && now_hour < 19)
        //        {
        //            //this.create_xml(alarm_text4, true, "1650", "ARRAY_PH_WIP_DAY3");
        //            this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //            sw.WriteLine("1650 log finish");
        //            sw.WriteLine("");

        //        }

        //        //if (1 == 1)
        //        //if (now_hour >= 9 && now_hour <= 24)
        //        //{
        //        //    //this.create_xml(alarm_text4, true, "1650", "ARRAY_PH_WIP_DAY2");
        //        //    this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //        //    sw.WriteLine("1650 log finish");
        //        //    sw.WriteLine("");

        //        //}

        //        //if (1 == 1)
        //        if (now_hour >= 19 && now_hour <= 24)
        //        {
        //            //this.create_xml(alarm_text4, true, "1650", "ARRAY_PH_WIP_DAY4");
        //            this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
        //            sw.WriteLine("1650 log finish");
        //            sw.WriteLine("");

        //        }


        //    }

        //}



        ////*** 檢查 1650  1638B  End

        #endregion



       

       
        func.delete_log_dir(Server.MapPath(".") + "\\File\\", "*.*", -60);
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);
       
        //func.delete_log_dir(Server.MapPath("..\\") + "\\LOG\\", -60);
        

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
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

    private bool DeleteDirectory(string strPath)
    {
        string[] strTemp;
        try
        {
            //槫方砲蝞敶銝豍隞?
            strTemp = System.IO.Directory.GetFiles(strPath);
            foreach (string str in strTemp)
            {
                System.IO.File.Delete(str);
            }
            //?擗摮蝞敶嚗毇?
            strTemp = System.IO.Directory.GetDirectories(strPath);
            foreach (string str in strTemp)
            {
                DeleteDirectory(str);
            }
            //?擗霂亦桀?
            System.IO.Directory.Delete(strPath);
            return true;
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message, "樄氆秤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    private void DeleteLogFile(string strPath)
    {

        for (int i = 7; i < 30; i++)  // 刪除檔案 前7天 到 前30天
        {

            for (int j = 0; j <= 24; j++)
            {
                string strPath1;
                strPath1 = strPath;
                DirectoryInfo di1;
                //(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.AddDays(-i).ToString("yyyyMMdd")
                di1 = new DirectoryInfo((Server.MapPath(".") + "\\File\\" + DateTime.Now.AddDays(-i).ToString("yyyyMMdd") + "_ALCS_" + j)); //DateTime.Now.ToString("yyyyMMdd") 
                DeleteDirectory((Server.MapPath(".") + "\\OEE_EQP_Status_SMS\\File\\" + DateTime.Now.AddDays(-i).ToString("yyyyMMdd") + "_ALCS_" + j));

            }



        }


    }

    private void wip_chk(string cur_step , string last_step, string cur_step_wip, string last_step_wip)
    {

                            //  S150               S138B               200                      150      

        
        
        //sw.WriteLine("Create log file");
        //sw.WriteLine(DateTime.Now.ToString("u") + "Array_Photo Program Start");
        //*** 檢查 1150  1138B
        string sql_T0A = "  select * from                                                             ";
        sql_T0A = sql_T0A + " (select t.step_seq,sum(t.glass_qty) as total_wip from lot t                ";
        sql_T0A = sql_T0A + " where t.prod_name is not null                                              ";
        sql_T0A = sql_T0A + "   and t.fab='Fab1'                                                         ";
        sql_T0A = sql_T0A + "   and t.shop='T1Array'                                                     ";
        sql_T0A = sql_T0A + "   and t.terminate_dttm is null                                             ";
        sql_T0A = sql_T0A + "   and t.array_ship_dttm is null                                            ";
        sql_T0A = sql_T0A + "   and t.lot_id <> ' ALL'                                                   ";
        sql_T0A = sql_T0A + "   and nvl(t.inv_state,' ') <> 'Created'                                    ";
        sql_T0A = sql_T0A + "   and nvl(t.step_seq,'0') in ('" + cur_step + "')                                      ";
        sql_T0A = sql_T0A + " group by t.step_seq                                                        ";
        sql_T0A = sql_T0A + " )t1,                                                                       ";
        sql_T0A = sql_T0A + " (select max(wip) as diff_pro_MAX_wip from (                                ";
        sql_T0A = sql_T0A + " select ot.prod_name,ot.wip                                                 ";
        sql_T0A = sql_T0A + " from (                                                                     ";
        sql_T0A = sql_T0A + " select t.step_seq,t.prod_name,sum(t.glass_qty) as wip from lot t           ";
        sql_T0A = sql_T0A + " where t.prod_name is not null                                              ";
        sql_T0A = sql_T0A + "   and t.fab='Fab1'                                                         ";
        sql_T0A = sql_T0A + "   and t.shop='T1Array'                                                     ";
        sql_T0A = sql_T0A + "   and t.terminate_dttm is null                                             ";
        sql_T0A = sql_T0A + "   and t.array_ship_dttm is null                                            ";
        sql_T0A = sql_T0A + "   and t.lot_id <> ' ALL'                                                   ";
        sql_T0A = sql_T0A + "   and nvl(t.inv_state,' ') <> 'Created'                                    ";
        sql_T0A = sql_T0A + "   and nvl(t.step_seq,'0') in ('" + last_step + "')                                     ";
        sql_T0A = sql_T0A + " group by t.step_seq,t.prod_name                                            ";
        sql_T0A = sql_T0A + " ) ot))t2                                                                   ";
        // add by oscar 20090910 for when STEP NO DATA 
        sql_T0A = sql_T0A + "union all select '" + cur_step + "',0,0 from dual";



        DataSet ds_T0A;

        ds_T0A = func.get_dataSet_access(sql_T0A, conn);




        if (ds_T0A.Tables[0].Rows.Count >= 2 && !ds_T0A.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString().Equals(""))
        {
            if (Convert.ToInt16(ds_T0A.Tables[0].Rows[0]["total_wip"].ToString()) < Convert.ToInt16(cur_step_wip) && Convert.ToInt16(ds_T0A.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString()) > Convert.ToInt16(last_step_wip))
                if (1 == 1)
                {

                    string alarm_text = "" + cur_step +" step " + "=" + ds_T0A.Tables[0].Rows[0]["total_wip"].ToString() + " Sub" + " less " + cur_step_wip + "Sub " + last_step + " step=" +ds_T0A.Tables[0].Rows[0]["diff_pro_MAX_wip"].ToString() + "Sub " + " greater " + last_step_wip + "Sub";
                    alarm_format.alarm_text = alarm_text;
                    alarm_format.alarm_comment = alarm_text;
                    //由時間 去判斷 發送的 event  
                    // 00~09    day1
                    // 09~19    day3
                    // 09~24    day2
                    // 19~24    day4
                    //if (1 == 1)
                    if (now_hour >= 0 && now_hour < 9)
                    {
                        // this.create_xml(alarm_text, true, "1150", "ARRAY_PH_WIP_DAY1");

                        this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
                    

                    }

                    //if (1 == 1)
                    if (now_hour >= 9 && now_hour < 19)
                    {
                        //this.create_xml(alarm_text, true, "1150", "ARRAY_PH_WIP_DAY3");
                        this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
                      

                    }

                    //if (1 == 1)
                    //if (now_hour >= 9 && now_hour <= 24)
                    //{
                    //    //this.create_xml(alarm_text, true, "1150", "ARRAY_PH_WIP_DAY2");
                    //    this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
                    //    sw.WriteLine("1150 log finish");
                    //    sw.WriteLine("");

                    //}

                    //if (1 == 1)
                    if (now_hour >= 19 && now_hour <= 24)
                    {
                        //this.create_xml(alarm_text, true, "1150", "ARRAY_PH_WIP_DAY4");
                        this.Alarm_create_xml(alarm_format, "Sys", "Array_photo");
                       
                    }






                }

        }
    
    }

}
