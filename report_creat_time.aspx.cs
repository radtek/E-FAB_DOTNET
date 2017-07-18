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

public partial class report_creat_time : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];
    string conn2 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];
    string conn3 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ALCS"];
    string conn4 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_T1NEWALARM"];
    string conn5 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_MIDGW1"];
    string conn6 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_MIS"];
    string conn7 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_T1NEWALARM_ALCS"];
    string conn8 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CFT"];
   
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    string today_minus17 = DateTime.Now.AddDays(-17).ToString("yyyy/MM/dd");

    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm");
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string sql_temp4 = "";
    string sql_temp5 = "";
    string sql_temp6 = "";
    string sql_temp7 = "";
    string sql_temp8 = "";
    string sql_temp9 = "";
    string sql_temp10 = "";
    string sql_temp11 = "";
    string sql_temp12 = "";
    
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet(); 

    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            txtCalendar1.Text = today;
            sql_temp = @"

        select a.procedurename,a.errmsg,count(a.procedurename) as counter from dw_etl_runlog a
        where a.lastrunsysdate>sysdate-1
        and 1=1
         --a.procedurename='ODS_T1F.MAIN' 
        and a.errmsg<>'OK'
         group by a.procedurename,a.errmsg
        having count(a.procedurename)>10
        order by count(a.procedurename)  desc

             ";

            Bind_data(sql_temp, conn8);
            //Double abc = -76;
            //Response.Write(Convert.ToString(-78, 16 ));


        
        }

        sql_temp2 = @"

  SELECT SHOP,
        procedurename,
        lastruntm,
        to_char(lastrunsysdate, 'yyyymmdd hh24miss') as lastrunsysdate,
        lastrunmaxtm,
        TO_CHAR(SYSDATE, 'yyyymmdd hh24miss') AS CHK_TIME,
        describe,
        'CIM' as Inspector,
        a AS differ_1,
        b AS differ_2,
        c as Judge
   FROM ( select 'ARY_Loader' AS SHOP,
                t.procedurename,
                t.lastruntm,
                t.lastrunsysdate,
                t.lastrunmaxtm,
                1 AS SEQ,
                '1.lastrunmaxtm-lastruntm 差距20 min 以內(differ_1)2.CHK_TIME-lastruntm 25 min以內為合理(differ_2)' as describe,
                to_char(round((to_date(lastrunmaxtm, 'yyyymmdd hh24miss') -
                              to_date(substr(lastruntm, 1, 15),
                                       'yyyymmdd hh24miss')) * 24 * 60,
                              0)) as a,
                to_char(round((to_date((select to_char(sysdate,'yyyymmdd hh24miss') from dual), 'yyyymmdd hh24miss') -
                              to_date(substr(lastruntm, 1, 15),
                                       'yyyymmdd hh24miss')) * 24 * 60,
                              0)) as b,
                DECODE(INSTR(TO_CHAR(20 -
                                     round((to_date(lastrunmaxtm,
                                                    'yyyymmdd hh24miss') -
                                           to_date(substr(lastruntm, 1, 15),
                                                    'yyyymmdd hh24miss')) * 24 * 60,
                                           0)),
                             '-'),
                       '1',
                       'NG',
                       '0',
                       DECODE(INSTR(TO_CHAR(25 - round((to_date((select to_char(sysdate,'yyyymmdd hh24miss') from dual), 'yyyymmdd hh24miss') -
                                                       to_date(substr(lastruntm,
                                                                       1,
                                                                       15),
                                                                'yyyymmdd hh24miss')) * 24 * 60,
                                                       0)),
                                    '-'),
                              '1',
                              'NG',
                              'OK'),
                       NULL) AS C
           from dw_etl_cfg@cell2ary t
          where t.procedurename = 'ODS_T0A.MAIN'
         union
         select 'CEL_Loader' AS SHOP,
                t.procedurename,
                t.lastruntm,
                t.lastrunsysdate,
                t.lastrunmaxtm,
                2 AS SEQ,
                '1.lastrunmaxtm-lastruntm 差距20 min 以內(differ_1)2.CHK_TIME-lastruntm 25 min以內為合理(differ_2)' as describe,
                to_char(round((to_date(lastrunmaxtm, 'yyyymmdd hh24miss') -
                              to_date(substr(lastruntm, 1, 15),
                                       'yyyymmdd hh24miss')) * 24 * 60,
                              0)) as a,
                to_char(round((to_date((select to_char(sysdate,'yyyymmdd hh24miss') from dual), 'yyyymmdd hh24miss') -
                              to_date(substr(lastruntm, 1, 15),
                                       'yyyymmdd hh24miss')) * 24 * 60,
                              0)) as b,
                DECODE(INSTR(TO_CHAR(20 -
                                     round((to_date(lastrunmaxtm,
                                                    'yyyymmdd hh24miss') -
                                           to_date(substr(lastruntm, 1, 15),
                                                    'yyyymmdd hh24miss')) * 24 * 60,
                                           0)),
                             '-'),
                       '1',
                       'NG',
                       '0',
                       DECODE(INSTR(TO_CHAR(25 - round((to_date((select to_char(sysdate,'yyyymmdd hh24miss') from dual), 'yyyymmdd hh24miss') -
                                                       to_date(substr(lastruntm,
                                                                       1,
                                                                       15),
                                                                'yyyymmdd hh24miss')) * 24 * 60,
                                                       0)),
                                    '-'),
                              '1',
                              'NG',
                              'OK'),
                       NULL) AS C
           from dw_etl_cfg t
          where t.procedurename = 'ODS_TCL.MAIN'
            AND T.SHOP = 'T0Cell'
         union
         select 'CF_Loader' AS SHOP,
                t.procedurename,
                t.lastruntm,
                t.lastrunsysdate,
                t.lastrunmaxtm,
                3 AS SEQ,
                '1.lastrunmaxtm-lastruntm 差距20 min 以內(differ_1)2.CHK_TIME-lastruntm 25 min以內為合理(differ_2)' as describe,
                to_char(round((to_date(lastrunmaxtm, 'yyyymmdd hh24miss') -
                              to_date(substr(lastruntm, 1, 15),
                                       'yyyymmdd hh24miss')) * 24 * 60,
                              0)) as a,
                to_char(round((to_date((select to_char(sysdate,'yyyymmdd hh24miss') from dual), 'yyyymmdd hh24miss') -
                              to_date(substr(lastruntm, 1, 15),
                                       'yyyymmdd hh24miss')) * 24 * 60,
                              0)) as b,
                DECODE(INSTR(TO_CHAR(20 -
                                     round((to_date(lastrunmaxtm,
                                                    'yyyymmdd hh24miss') -
                                           to_date(substr(lastruntm, 1, 15),
                                                    'yyyymmdd hh24miss')) * 24 * 60,
                                           0)),
                             '-'),
                       '1',
                       'NG',
                       '0',
                       DECODE(INSTR(TO_CHAR(25 - round((to_date((select to_char(sysdate,'yyyymmdd hh24miss') from dual), 'yyyymmdd hh24miss') -
                                                       to_date(substr(lastruntm,
                                                                       1,
                                                                       15),
                                                                'yyyymmdd hh24miss')) * 24 * 60,
                                                       0)),
                                    '-'),
                              '1',
                              'NG',
                              'OK'),
                       NULL) AS C
           from dw_etl_cfg@cell2CF t
          where t.procedurename = 'ODS_T1F.MAIN'
         UNION
         select 'ARY_CUT_TIME_WIP' AS SHOP,
                t.procedurename,
                t.lastruntm,
                t.lastrunsysdate,
                t.lastrunmaxtm,
                4 AS SEQ,
                'CHK_TIME-lastrunsysdate 65 Min 為合理(differ_1)' as describe,
                to_char(round((sysdate - lastrunsysdate) * 24 * 60, 0)) as a,
                '0' as b,
                DECODE(INSTR(TO_CHAR(65 - round((sysdate - lastrunsysdate) * 24 * 60,
                                                0)),
                             '-'),
                       '1',
                       'NG',
                       '0',
                       'OK',
                       null) AS C
           from dw_etl_cfg@cell2ary t
          where t.procedurename = 'ODS_T0A.CUTTIMEWIP'
         union
         select 'CEL_CUT_TIME_WIP' AS SHOP,
                t.procedurename,
                t.lastruntm,
                t.lastrunsysdate,
                t.lastrunmaxtm,
                5 AS SEQ,
                'CHK_TIME-lastrunsysdate 65 Min 為合理(differ_1)' as describe,
                to_char(round((sysdate - lastrunsysdate) * 24 * 60, 0)) as a,
                '0' as b,
                DECODE(INSTR(TO_CHAR(65 - round((sysdate - lastrunsysdate) * 24 * 60,
                                                0)),
                             '-'),
                       '1',
                       'NG',
                       '0',
                       'OK',
                       null) AS C
           from dw_etl_cfg t
          where t.procedurename = 'ODS_TCL.CUTTIMEWIP'
            AND T.SHOP = 'T0Cell'
         union
         select 'CF_CUT_TIME_WIP' AS SHOP,
                t.procedurename,
                t.lastruntm,
                t.lastrunsysdate,
                t.lastrunmaxtm,
                6 AS SEQ,
                'CHK_TIME-lastrunsysdate 65 Min 為合理(differ_1)' as describe,
                to_char(round((sysdate - lastrunsysdate) * 24 * 60, 0)) as a,
                '0' as b,
                DECODE(INSTR(TO_CHAR(65 - round((sysdate - lastrunsysdate) * 24 * 60,
                                                0)),
                             '-'),
                       '1',
                       'NG',
                       '0',
                       'OK',
                       null) AS C
           from dw_etl_cfg@cell2CF t
          where t.procedurename = 'ODS_T1F.CUTTIMEWIP'
         union
         select 'ARY_EXCEPTION_CHK' AS SHOP,
                'ODS_T0A_MAIN' AS procedurename,
                NULL AS lastruntm,
                NULL AS lastrunsysdate,
                NULL AS lastrunmaxtm,
                7 AS SEQ,
                '15分鐘內 15 筆 Exception 內為合理 (differ_1)' as describe,
                TO_CHAR(COUNT(*)) as a,
                '0' as b,
                DECODE(INSTR(TO_CHAR(15 - COUNT(*)), '-'),
                       '1',
                       'NG',
                       '0',
                       'OK',
                       NULL) AS C
           from dw_etl_runlog@cell2ary t
          where t.procedurename like 'ODS_T0A%'
            and t.lastrunsysdate > sysdate - 1 / 96
            and t.errmsg <> 'OK'
         union
         select 'CEL_EXCEPTION_CHK' AS SHOP,
                'ODS_TCL_MAIN' AS procedurename,
                NULL AS lastruntm,
                NULL AS lastrunsysdate,
                NULL AS lastrunmaxtm,
                8 AS SEQ,
                '15分鐘內 15 筆 Exception 內為合理 (differ_1)' as describe,
                TO_CHAR(COUNT(*)) as a,
                '0' as b,
                DECODE(INSTR(TO_CHAR(15 - COUNT(*)), '-'),
                       '1',
                       'NG',
                       '0',
                       'OK',
                       NULL) AS C
           from dw_etl_runlog t
          where t.procedurename like 'ODS_TCL%'
            and t.lastrunsysdate > sysdate - 1 / 96
            and t.errmsg <> 'OK'
         union
         select 'CF_EXCEPTION_CHK' AS SHOP,
                'ODS_T1F_MAIN' AS procedurename,
                NULL AS lastruntm,
                NULL AS lastrunsysdate,
                NULL AS lastrunmaxtm,
                8 AS SEQ,
                '15分鐘內 15 筆 Exception 內為合理 (differ_1)' as describe,
                TO_CHAR(COUNT(*)) as a,
                '0' as b,
                DECODE(INSTR(TO_CHAR(15 - COUNT(*)), '-'),
                       '1',
                       'NG',
                       '0',
                       'OK',
                       NULL) AS C
           from dw_etl_runlog@cell2CF t
          where t.procedurename like 'ODS_T1F%'
            and t.lastrunsysdate > sysdate - 1 / 96
            and t.errmsg <> 'OK'
         union
         select 'ARY_STEPSEQ_NULL' AS SHOP,
                'MOVEHISTORY' AS procedurename,
                NULL AS lastruntm,
                NULL AS lastrunsysdate,
                NULL AS lastrunmaxtm,
                9 AS SEQ,
                'ARY MOVEHISTORY STEPSEQ IS NULL 每小時小於5筆內為合理(differ_1)' as describe,
                TO_CHAR(COUNT(*)) as a,
                '0' as b,
                DECODE(sign(5 - COUNT(*)), -1, 'NG', 1, 'OK', NULL) AS C
           from move_history@cell2ary t
          where t.shift_date >= to_char(sysdate - 1, 'YYYYMMDD')
            and T.Trkout_Dttm >= sysdate - 1 / 24
            AND T.STEP_SEQ IS NULL
         union
         select 'ARY_SESSION' AS SHOP,
                'ARY_SESSION' AS procedurename,
                NULL AS lastruntm,
                NULL AS lastrunsysdate,
                NULL AS lastrunmaxtm,
                10 AS SEQ,
                'ARY SESSION 總數小於100筆內為合理(differ_1)' as describe,
                TO_CHAR(COUNT(*)) as a,
                '0' as b,
                DECODE(sign(100 - COUNT(*)), -1, 'NG', 1, 'OK', NULL) AS C
           from v$session@cell2ary t
         union
         select 'CEL_SESSION' AS SHOP,
                'CEL_SESSION' AS procedurename,
                NULL AS lastruntm,
                NULL AS lastrunsysdate,
                NULL AS lastrunmaxtm,
                11 AS SEQ,
                'CEL SESSION 總數小於100筆內為合理(differ_1)' as describe,
                TO_CHAR(COUNT(*)) as a,
                '0' as b,
                DECODE(sign(100 - COUNT(*)), -1, 'NG', 1, 'OK', NULL) AS C
           from v$session t
         union
         select 'CF_SESSION' AS SHOP,
                'CF_SESSION' AS procedurename,
                NULL AS lastruntm,
                NULL AS lastrunsysdate,
                NULL AS lastrunmaxtm,
                12 AS SEQ,
                'CF SESSION 總數小於100筆內為合理(differ_1)' as describe,
                TO_CHAR(COUNT(*)) as a,
                '0' as b,
                DECODE(sign(100 - COUNT(*)), -1, 'NG', 1, 'OK', NULL) AS C
           from v$session@cell2cf t
         union
         select 'RPT_CHK_SYSDATE_DIFF' AS SHOP,
                'SYSDATE' AS procedurename,
                NULL AS lastruntm,
                NULL AS lastrunsysdate,
                NULL AS lastrunmaxtm,
                13 AS SEQ,
                'RPT Time(' || to_char(RPT, 'HH24MISS') || ') - MES Time(' ||
                to_char(MES, 'HH24MISS') ||
                ') 時間差正負10秒內為合理(differ_1)',
                to_char(diff, '999') as a,
                '0' as b,
                result AS C
           from rpt_chk_sysdate@cell2ary t)
  ORDER BY SEQ


";


        Bind_data2(sql_temp2, conn1);

        sql_temp3 = @"select tt.shop,tt.mail_flag,
case when tt.start_time is null  then tt.end_time else tt.start_time  end start_time,

case when tt.end_time is null  then tt.start_time else tt.end_time  end end_time,
tt.report_id,tt.server_ip

 from (

select substr(t.attr1_value, 0, instr(t.attr1_value, '_') - 1) as shop,
               t.attr2_value as MAIL_FLAG,
               
               case
                 when t.attr2_value like '%START' then
                  t.attr3_value
               end start_time,
               case
                 when t.attr2_value like '%END' then
                  t.attr3_value
               
               end end_time,
               
               t.attr5_value as report_id,
               t.attr6_value as server_ip
        
          from rpt_attribute t
         where t.attr4_value like '{0}%'
        
         order by case
                    when t.attr2_value like 'CREATE_FILE%' then
                     1
                    else
                     2
                  end,
                  t.attr3_value


) tt

where tt.mail_flag like '%END' order by  case when    tt.mail_flag like '%MAIL%' then 1 else 2 end ,tt.end_time 

";

        sql_temp3 = string.Format(sql_temp3, txtCalendar1.Text.Replace("/", ""));

        Bind_data3(sql_temp3, conn2);


        sql_temp4 = @"select tb1.line,tb1.cutoffkey,count(tb1.line)as counter from empaidxsummhourly tb1 
where  tb1.cutoffkey=to_char(sysdate-1/24,'yyyyMMddHH24') and tb1.cutoffcycle='H'
      and tb1.line in ('T1CF','T0CELL','T1CELL','T0ARRAY','T1ARRAY')

group by tb1.line,tb1.cutoffkey
order by         case when tb1.line='T0ARRAY' then 1
                      when tb1.line='T1ARRAY' then 2
                      when tb1.line='T0CELL' then 3
                      when tb1.line='T1CELL' then 4
                      when tb1.line='T1CF' then 4 end";



        Bind_data4(sql_temp4, conn2);


        sql_temp5 = @"select max(to_char(t.dttm,'yyyyMMddHH24MISS'))as max_dttm,to_char(t.dttm,'yyyyMMddHH24')as dttm ,count(t.sn) as counter from alarmlasttime t
where t.dttm>sysdate-1/24
   
group by to_char(t.dttm,'yyyyMMddHH24')";



        Bind_data5(sql_temp5, conn7);



        sql_temp6 = @" select min(t.partition_name) as min_partition,max(t.partition_name) as max_partition from user_ind_partitions t
 where t.tablespace_name like 'IXOEE_GLSHIS_IDX%'
       and t.partition_name like 'IXOEE_GLSHIS_2%' 
       --and ROWNUM = 1

 
 union all
 
 select min(t.partition_name),max(t.partition_name) from user_ind_partitions t
 where t.tablespace_name like 'IXOEE_EVTHIS_IDX%'
       and t.partition_name like 'IXOEE_EVTHIS_2%' 
       --and ROWNUM = 1
  union all      
  select min(t.partition_name),max(t.partition_name) from user_ind_partitions t
 where t.tablespace_name like 'IXOEE_ALMHIS_IDX%'
        and t.partition_name like 'IXOEE_ALMHIS_2%' 
       --and ROWNUM = 1
 union all         
  
 select min(t.partition_name),max(t.partition_name) from user_ind_partitions t
 where t.tablespace_name like 'IXOEE_PORHIS_IDX%'
       and t.partition_name like 'IXOEE_PORHIS_2%' 
       --and ROWNUM = 1


";


        Bind_data6(sql_temp6, conn2);


        sql_temp7 = @" select * from (
 
 select tb2.max_dttm,tb2.dttm,tb2.counter, round((sysdate-to_date(substr(tb2.max_dttm,0,16),'yyyy/MM/dd HH24:MI:ss'))*24*60,1) as diff_min from (
 
 select max(t.trans_date)as max_dttm,
       substr(t.trans_date,0,13)  as dttm,
        count(t.active_eventid) as counter
   from txn_alarm t
   where t.trans_date>to_char(sysdate -1/24,'yyyy/MM/dd HH24:MI:SS')
   
   group by substr(t.trans_date,0,13)  order by substr(t.trans_date,0,13) 
   
 )tb2
 
   
   
   
 ) tb1

union  all
select 'Queue(Counter under 60 is safety)','12', COUNT(T.LOGID) as counter, 0 from sum_alarm t
where t.logsequence>= to_char(sysdate-12/24,'yyyyMMDDHH24miSS') and t.status<>'ACK'
 
union  all
select 'Queue(Counter under 120 is safety)','24', COUNT(T.LOGID) as counter, 0 from sum_alarm t
where t.logsequence>= to_char(sysdate-1,'yyyyMMDDHH24miSS') and t.status<>'ACK'

";


        Bind_data7(sql_temp7, conn4);

        sql_temp8 = @" 
SELECT 'ST-W' as Type,COUNT(cutoffkey) as Count,cutoffkey,MIN(savedtime) as starttime,MAX(savedtime)as endtime, 
        round((to_date(MAX(savedtime),'yyyy-mm-dd hh24:mi:ss')-to_date(MIN(savedtime),'yyyy-mm-dd hh24:mi:ss'))*24*60,0) as duration 
    FROM EMPASTSUMMWEEKLY    
    WHERE cutoffcycle='W'    
    and  cutoffkey >=(select to_char(sysdate-180,'yyyyIW') from dual)
    GROUP BY cutoffkey 
    ORDER BY cutoffkey DESC
";


        Bind_data8(sql_temp8, conn2);



        sql_temp9 = @" 
select ot1.*,
       round((sysdate - to_date(substr(ot1.receivedtime, 0, 18),
                                'yyyy-MM-dd HH24:MI:ss')) * 24 * 60,
             2) diff_min,
       case when    round((sysdate - to_date(substr(ot1.receivedtime, 0, 18),
                                'yyyy-MM-dd HH24:MI:ss')) * 24 * 60,
             2)>30 then 'NG'
             else 'OK' end flag     
  from (
        
        select 'ARRAY' as shop, max(t.receivedtime) as receivedtime
          from ary_send_msg t
         where t.area = 'ARRAY'
        
        union
        
        select 'CELL' as shop, max(t.receivedtime) as receivedtime
          from cel_send_msg@mid12mid2.us.oracle.com t
         where t.area = 'CELL'
        
        union
        
        select 'CF' as shop, max(t.receivedtime) as receivedtime
          from cf_send_msg t
         where t.area = 'CF'
        
        ) ot1

";


        Bind_data9(sql_temp9, conn5);



        sql_temp10 = @" 
select 'north_source_data' as Item_Name,max(t1.starttime) as shiftdate from north_source_data t1
where( t1.shop like 'T0%' or t1.shop like 'T1%')

union all
select 'asset_utilization_ratio' as Item_Name,max(t2.shiftdate)as shiftdate from asset_utilization_ratio t2
where ( t2.shop like 'T0%' or t2.shop like 'T1%')


union all
select 'cell_beol' as Item_Name, max(t3.mfg_dt)as shiftdate from cell_beol t3
where ( t3.shop like 'T0%' or t3.shop like 'T1%')

";


        Bind_data10(sql_temp10, conn6);



        sql_temp11 = @" 
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

";


        Bind_data11(sql_temp11, conn2);




        sql_temp12 = @" 
select 'ARY' as SHOP,
       t.procedurename,
       to_char(t.lastrunstarttime, 'yyyy/MM/dd HH24:MI:ss') as lastrunstarttime,
       to_char(t.lastrunendtime, 'yyyy/MM/dd HH24:MI:ss') as lastrunendtime,
       t.lastdatatm,
       t.isrun,
       round((sysdate - t.lastrunstarttime) * 24, 1) as diff,
       round((sysdate - to_date(t.lastdatatm, 'yyyyMMdd HH24MISS')) * 24, 1) as diff2,
       case
         when round((sysdate - t.lastrunstarttime) * 24, 1) < 24 then
          'OK'
         else
          'NG'
       end as flag
  from mesoee_ary_txn.ldr_etl_cfg t

union all

select 'Cell' as SHOP,
       t1.procedurename,
       to_char(t1.lastrunstarttime, 'yyyy/MM/dd HH24:MI:ss') as lastrunstarttime,
       to_char(t1.lastrunendtime, 'yyyy/MM/dd HH24:MI:ss') as lastrunendtime,
       t1.lastdatatm,
       t1.isrun,
       round((sysdate - t1.lastrunstarttime) * 24, 1) as diff,
       round((sysdate - to_date(t1.lastdatatm, 'yyyyMMdd HH24MISS')) * 24,
             1) as diff2,
       case
         when round((sysdate - t1.lastrunstarttime) * 24, 1) < 24 then
          'OK'
         else
          'NG'
       end as flag
  from mesoee_tcl_txn.ldr_etl_cfg t1
 where t1.procedurename like '%ODS%'

union all

select 'CF' as SHOP,
       t2.procedurename,
       to_char(t2.lastrunstarttime, 'yyyy/MM/dd HH24:MI:ss') as lastrunstarttime,
       to_char(t2.lastrunendtime, 'yyyy/MM/dd HH24:MI:ss') as lastrunendtime,
       t2.lastdatatm,
       t2.isrun,
       round((sysdate - t2.lastrunstarttime) * 24, 1) as diff,
       round((sysdate - to_date(t2.lastdatatm, 'yyyyMMdd HH24MISS')) * 24,
             1) as diff2,
       case
         when round((sysdate - t2.lastrunstarttime) * 24, 1) < 24 then
          'OK'
         else
          'NG'
       end as flag
  from mesoee_t1f_txn.ldr_etl_cfg t2
 where t2.procedurename like '%ODS%'

";


        Bind_data12(sql_temp12, conn2); 

    }


    public DataSet Bind_data(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);



        GridView1.DataSource = ds_temp.Tables[0];


        GridView1.DataBind();



        return ds_temp;

    }

    public DataSet Bind_data2(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);



        GridView2.DataSource = ds_temp.Tables[0];


        GridView2.DataBind();



        return ds_temp;

    }

    public DataSet Bind_data3(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);



        GridView3.DataSource = ds_temp.Tables[0];


        GridView3.DataBind();



        return ds_temp;

    }
    public DataSet Bind_data4(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);



        GridView4.DataSource = ds_temp.Tables[0];


        GridView4.DataBind();



        return ds_temp;

    }

    public DataSet Bind_data5(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);



        GridView5.DataSource = ds_temp.Tables[0];


        GridView5.DataBind();



        return ds_temp;

    }
    public DataSet Bind_data6(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);



        GridView6.DataSource = ds_temp.Tables[0];


        GridView6.DataBind();



        return ds_temp;

    }

    public DataSet Bind_data7(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);



        GridView7.DataSource = ds_temp.Tables[0];


        GridView7.DataBind();



        return ds_temp;

    }
    public DataSet Bind_data8(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);



        GridView8.DataSource = ds_temp.Tables[0];


        GridView8.DataBind();



        return ds_temp;

    }

    public DataSet Bind_data9(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);



        GridView9.DataSource = ds_temp.Tables[0];


        GridView9.DataBind();



        return ds_temp;

    }

    public DataSet Bind_data10(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, conn6);



        GridView10.DataSource = ds_temp.Tables[0];


        GridView10.DataBind();



        return ds_temp;

    }

    public DataSet Bind_data11(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, conn2);



        GridView11.DataSource = ds_temp.Tables[0];


        GridView11.DataBind();



        return ds_temp;

    }

    public DataSet Bind_data12(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, conn2);



        GridView12.DataSource = ds_temp.Tables[0];


        GridView12.DataBind();



        return ds_temp;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {




        sql_temp = " select *                                                                                                                                     " +
"   from (select ot6.shop,                                                                                                                     " +
"                ot6.mail_flag,                                                                                                                " +
"                ot6.start_time,                                                                                                               " +
"                ot6.endtime,                                                                                                                  " +
"                round((to_date(substr(ot6.endtime, 0, 13),                                                                                    " +
"                               'YYYYMMDD HH24MISS') -                                                                                         " +
"                      to_date(substr(ot6.start_time, 0, 13),                                                                                  " +
"                               'YYYYMMDD HH24MISS')) * 24 * 60,                                                                               " +
"                      1) as interval_min,                                                                                                     " +
"                ot6.report_id,                                                                                                                " +
"                ot6.server_ip                                                                                                                 " +
"           from (select 'ARY' as shop,                                                                                                        " +
"                        attr5_name as mail_flag,                                                                                              " +
"                        attr3_value as endtime,                                                                                               " +
"                        to_char(to_date(substr(attr5_value,                                                                                   " +
"                                               instr(attr5_value, '_', 1, 2) + 1,                                                             " +
"                                               12),                                                                                           " +
"                                        'yyyymmddhh24mi'),                                                                                    " +
"                                'yyyymmdd hh24mi') as Start_time,                                                                             " +
"                        attr5_value as report_id,                                                                                             " +
"                        attr6_value as server_ip                                                                                              " +
"                   from rpt_attribute t                                                                                                       " +
"                  where (attr5_name like 'AUTO%' and                                                                                          " +
"                        attr5_value like '%A%xls')                                                                                            " +
"                    and (t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 070000' or                                                                           " +
"                        t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 150000')                                                                              " +
"                     or (attr2_value like 'SEND_MAIL_PROCESS_END%' and                                                                        " +
"                        attr5_value like '%A%xls')                                                                                            " +
"                    and (t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 070000' or                                                                           " +
"                        t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 150000')                                                                              " +
"                 union                                                                                                                        " +
"                 select 'CF' as shop,                                                                                                         " +
"                        attr5_name as mail_flag,                                                                                              " +
"                        attr3_value,                                                                                                          " +
"                        to_char(to_date(substr(attr5_value,                                                                                   " +
"                                               instr(attr5_value, '_', 1, 2) + 1,                                                             " +
"                                               12),                                                                                           " +
"                                        'yyyymmddhh24mi'),                                                                                    " +
"                                'yyyymmdd hh24mi'),                                                                                           " +
"                        attr5_value,                                                                                                          " +
"                        attr6_value                                                                                                           " +
"                   from rpt_attribute@ods2cf t                                                                                                " +
"                  where attr5_name like 'AUTO%'                                                                                               " +
"                    and attr5_value like '%C%xls'                                                                                             " +
"                    and (t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 070000' or                                                                           " +
"                        t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 150000')                                                                              " +
"                     or (attr2_value like 'SEND_MAIL_PROCESS_END%' and                                                                        " +
"                        attr5_value like '%C%xls')                                                                                            " +
"                    and (t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 070000' or                                                                           " +
"                        t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 150000')                                                                              " +
"                 union all                                                                                                                    " +
"                 select 'Cel' as shop,                                                                                                        " +
"                        attr5_name as mail_flag,                                                                                              " +
"                        attr3_value,                                                                                                          " +
"                        to_char(to_date(substr(attr5_value,                                                                                   " +
"                                               instr(attr5_value, '_', 1, 2) + 1,                                                             " +
"                                               12),                                                                                           " +
"                                        'yyyymmddhh24mi'),                                                                                    " +
"                                'yyyymmdd hh24mi'),                                                                                           " +
"                        attr5_value,                                                                                                          " +
"                        attr6_value                                                                                                           " +
"                   from rpt_attribute@ods2cell t                                                                                              " +
"                  where attr5_name like 'AUTO%'                                                                                               " +
"                    and attr5_value like '%C%xls'                                                                                             " +
"                    and (t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 070000' or                                                                           " +
"                        t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 150000') or                                                                           " +
"                        (attr2_value like 'SEND_MAIL_PROCESS_END%' and                                                                        " +
"                        attr5_value like '%C%xls') and                                                                                        " +
"                        (t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 070000' or                                                                           " +
"                        t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 150000')) ot6) tb                                                                     " +
"  order by  case when tb.mail_flag like '%MAIL_ATTACH_NAME%' then 1 when  tb.mail_flag like '%AUTO_CREATED_FILE_NAME%' then 2 end ,           " +
"  case                                                                                                                                        " +
"             when tb.report_id like '%T1Array_DAILY%' then                                                                                    " +
"              1                                                                                                                               " +
"             when tb.report_id like '%T0Array_DAILY%' then                                                                                    " +
"              2                                                                                                                               " +
"             when tb.report_id like '%T1Array_NOON%' then                                                                                     " +
"              3                                                                                                                               " +
"             when tb.report_id like '%T0Array_NOON%' then                                                                                     " +
"              4                                                                                                                               " +
"             when tb.report_id like '%T1CF_DAILY%' then                                                                                       " +
"              5                                                                                                                               " +
"             when tb.report_id like '%T1CF_NOON%' then                                                                                        " +
"              6                                                                                                                               " +
"             when tb.report_id like '%T1Cell_DAILY%' then                                                                                     " +
"              7                                                                                                                               " +
"             when tb.report_id like '%T1Cell_NOON%' then                                                                                      " +
"              8                                                                                                                               " +
"             when tb.report_id like '%T0Cell_DAILY%' then                                                                                     " +
"              9                                                                                                                               " +
"             when tb.report_id like '%T0Cell_NOON%' then                                                                                      " +
"              10                                                                                                                              " +
"           end asc                                                                                                                            ";


        GridView gv = new GridView();
        gv.DataSource = Bind_data(sql_temp, conn);
        gv.DataBind();
        ExportExcel(gv); 

     


    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
    }

    private void ExportExcel(GridView SeriesValuesDataGrid)
    {
        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader("content-disposition", "attachment;filename=Report_internal_time.xls");

        Response.Charset = "big5";

        // If you want the option to open the Excel file without saving than 

        // comment out the line below 

        // Response.Cache.SetCacheability(HttpCacheability.NoCache); 

        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        SeriesValuesDataGrid.AllowPaging = false;
        SeriesValuesDataGrid.DataBind();

        SeriesValuesDataGrid.RenderControl(htmlWrite);

        string head = " <html> " +
        " <head><meta http-equiv='Content-Type' content='text/html; charset=big5'></head> " +
        " <body> ";

        string footer = " </body>" +
        " </html>";

        Response.Write(head + stringWrite.ToString() + footer);

        Response.End();

        SeriesValuesDataGrid.AllowPaging = true;
        SeriesValuesDataGrid.DataBind();





    } 

    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        sql_temp = " select *                                                                                                                                     " +
"   from (select ot6.shop,                                                                                                                     " +
"                ot6.mail_flag,                                                                                                                " +
"                ot6.start_time,                                                                                                               " +
"                ot6.endtime,                                                                                                                  " +
"                round((to_date(substr(ot6.endtime, 0, 13),                                                                                    " +
"                               'YYYYMMDD HH24MISS') -                                                                                         " +
"                      to_date(substr(ot6.start_time, 0, 13),                                                                                  " +
"                               'YYYYMMDD HH24MISS')) * 24 * 60,                                                                               " +
"                      1) as interval_min,                                                                                                     " +
"                ot6.report_id,                                                                                                                " +
"                ot6.server_ip                                                                                                                 " +
"           from (select 'ARY' as shop,                                                                                                        " +
"                        attr5_name as mail_flag,                                                                                              " +
"                        attr3_value as endtime,                                                                                               " +
"                        to_char(to_date(substr(attr5_value,                                                                                   " +
"                                               instr(attr5_value, '_', 1, 2) + 1,                                                             " +
"                                               12),                                                                                           " +
"                                        'yyyymmddhh24mi'),                                                                                    " +
"                                'yyyymmdd hh24mi') as Start_time,                                                                             " +
"                        attr5_value as report_id,                                                                                             " +
"                        attr6_value as server_ip                                                                                              " +
"                   from rpt_attribute t                                                                                                       " +
"                  where (attr5_name like 'AUTO%' and                                                                                          " +
"                        attr5_value like '%A%xls')                                                                                            " +
"                    and (t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 070000' or                                                                           " +
"                        t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 150000')                                                                              " +
"                     or (attr2_value like 'SEND_MAIL_PROCESS_END%' and                                                                        " +
"                        attr5_value like '%A%xls')                                                                                            " +
"                    and (t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 070000' or                                                                           " +
"                        t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 150000')                                                                              " +
"                 union                                                                                                                        " +
"                 select 'CF' as shop,                                                                                                         " +
"                        attr5_name as mail_flag,                                                                                              " +
"                        attr3_value,                                                                                                          " +
"                        to_char(to_date(substr(attr5_value,                                                                                   " +
"                                               instr(attr5_value, '_', 1, 2) + 1,                                                             " +
"                                               12),                                                                                           " +
"                                        'yyyymmddhh24mi'),                                                                                    " +
"                                'yyyymmdd hh24mi'),                                                                                           " +
"                        attr5_value,                                                                                                          " +
"                        attr6_value                                                                                                           " +
"                   from rpt_attribute@ods2cf t                                                                                                " +
"                  where attr5_name like 'AUTO%'                                                                                               " +
"                    and (attr5_value like '%C%xls' or attr5_value like '%TP%xls'   )                                                                                          " +
"                    and (t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 070000' or                                                                           " +
"                        t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 150000')                                                                              " +
"                     or (attr2_value like 'SEND_MAIL_PROCESS_END%' and                                                                        " +
"                        (attr5_value like '%C%xls' or attr5_value like '%TP%xls' ))                                                                                            " +
"                    and (t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 070000' or                                                                           " +
"                        t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 150000')                                                                              " +
"                 union all                                                                                                                    " +
"                 select 'Cel' as shop,                                                                                                        " +
"                        attr5_name as mail_flag,                                                                                              " +
"                        attr3_value,                                                                                                          " +
"                        to_char(to_date(substr(attr5_value,                                                                                   " +
"                                               instr(attr5_value, '_', 1, 2) + 1,                                                             " +
"                                               12),                                                                                           " +
"                                        'yyyymmddhh24mi'),                                                                                    " +
"                                'yyyymmdd hh24mi'),                                                                                           " +
"                        attr5_value,                                                                                                          " +
"                        attr6_value                                                                                                           " +
"                   from rpt_attribute@ods2cell t                                                                                              " +
"                  where attr5_name like 'AUTO%'                                                                                               " +
"                    and attr5_value like '%C%xls'                                                                                             " +
"                    and (t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 070000' or                                                                           " +
"                        t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 150000') or                                                                           " +
"                        (attr2_value like 'SEND_MAIL_PROCESS_END%' and                                                                        " +
"                        attr5_value like '%C%xls') and                                                                                        " +
"                        (t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 070000' or                                                                           " +
"                        t.attr4_value = '" + txtCalendar1.Text.Replace("/", "") + "' || ' 150000')) ot6) tb                                                                     " +
"  order by  case when tb.mail_flag like '%MAIL_ATTACH_NAME%' then 1 when  tb.mail_flag like '%AUTO_CREATED_FILE_NAME%' then 2 end ,           " +
"  case                                                                                                                                        " +
"             when tb.report_id like '%T1Array_DAILY%' then                                                                                    " +
"              1                                                                                                                               " +
"             when tb.report_id like '%T0Array_DAILY%' then                                                                                    " +
"              2                                                                                                                               " +
"             when tb.report_id like '%T1Array_NOON%' then                                                                                     " +
"              3                                                                                                                               " +
"             when tb.report_id like '%T0Array_NOON%' then                                                                                     " +
"              4                                                                                                                               " +
"             when tb.report_id like '%T1CF_DAILY%' then                                                                                       " +
"              5                                                                                                                               " +
"             when tb.report_id like '%T1CF_NOON%' then                                                                                        " +
"              6                                                                                                                               " +
"             when tb.report_id like '%T1Cell_DAILY%' then                                                                                     " +
"              7                                                                                                                               " +
"             when tb.report_id like '%T1Cell_NOON%' then                                                                                      " +
"              8                                                                                                                               " +
"             when tb.report_id like '%T0Cell_DAILY%' then                                                                                     " +
"              9                                                                                                                               " +
"             when tb.report_id like '%T0Cell_NOON%' then                                                                                      " +
"              10                                                                                                                              " +
"           end asc                                                                                                                            ";

        Bind_data(sql_temp, conn);
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
           // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            //string[] StrAry1 = report_id1.Split('_');

            
            //Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

      ///  string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            string JUDGE = DataBinder.Eval(e.Row.DataItem, "JUDGE").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
           // string[] StrAry = report_id.Split('_');


           // string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            //Int32 pricexx = Convert.ToInt32(price1); 

            if (!JUDGE.Equals("OK"))
               for (int i = 0; i <=    e.Row.Cells.Count-1; i++)
			{
			 e.Row.Cells[i].Style.Add("background-color", "#FFFF80");
			}
          


           // if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            //e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }


    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            string endtime = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            string[] StrAry = report_id.Split('_');


            string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            string endtime1 = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            string[] StrAry1 = report_id1.Split('_');


            //Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            ////Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }

    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            ////Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }
    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {

       

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            ////Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            ////Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }
    protected void GridView8_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            ////Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }

    protected void GridView9_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            ////Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }


    protected void GridView10_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            ////Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }

    protected void GridView11_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            ////Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }


    protected void GridView12_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "end_time").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            ////Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }
  
}
