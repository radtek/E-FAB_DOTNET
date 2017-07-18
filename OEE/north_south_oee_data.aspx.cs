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

public partial class OEE_north_south_oee_data : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ALCS_XLS"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];
    string conn2 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_MIS"];



    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    string today = DateTime.Now.AddDays(+0).ToString("yyyyMMdd");
    string yesturday = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
    string before_60 = DateTime.Now.AddDays(-60).ToString("yyyyMMdd");
    string today_minus7 = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");

    ArrayList arlist_temp1 = new ArrayList();
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)

        {
            txtEstimateSTARTTIME.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd"));
            txtEstimateEndTime.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd"));

            //Button1_Click(null, null);

            //Button2_Click(null, null);

            //Button3_Click(null, null);
        
        }

       
        //javascript 語法填入 字串 
      //string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_self');window.top.close(this);</script>";
        //呼叫 javascript 
      //this.Page.RegisterStartupScript("", frmClose);
        






    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        func.write_log("north_south_oee_data ", Server.MapPath("..\\") + "\\LOG\\", "log");
        
        sql_temp3 = @"

     
delete north_source_data
 where starttime='{0}'

";

        sql_temp3 = string.Format(sql_temp3, yesturday);

        func.get_sql_execute(sql_temp3, conn2);

        sql_temp3 = @"

     
delete north_source_data
 where starttime<'{0}'

";

        sql_temp3 = string.Format(sql_temp3, before_60);
        func.get_sql_execute(sql_temp3, conn2);



        yesturday = txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd");
        today=txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd");
        
        sql_temp = @"

   

select  'T'||substr(ot7.EQPID,0,1) as shop,ot7.STARTTIME,ot7.endtime,ot7.EQPID,
       ot7.RUN+ot7.E_RUN+ot7.SETUP+ot7.IDLE as up_time
       ,ot7.RUN+ot7.E_RUN+ot7.SETUP as run_time
       ,( ot7.RUN+ot7.E_RUN+ot7.SETUP+ (ot7.IDLE+ot7.OFF)*ot8.up_p  )   as Up_time_Modify
       ,( ot7.RUN+ot7.E_RUN+ot7.SETUP+(ot7.IDLE+ot7.OFF)*ot8.up_p*ot8.uu_p  ) as Run_time_Modify
       ,ot8.up_p as UP_Plan
       ,ot8.uu_p as Run_Plan
       , ((24*60-ot7.off)/60) as operation_time,
       sysdate as dttm
 from (
select ot6.*,round(ot6.run2*ot6.up2,4) as oee2 from (

select ot5.*,
       round(ot5.run2_non * ot5.up2_non, 4) as oee2_non,
       case
         when ((ot5.run + ot5.E_RUN+ot5.setup) + (ot5.idle + ot5.off) * 0.91*(select t.run2 from empapp t where t.name='D')) > 0 then
          round(((ot5.run + ot5.E_RUN+ot5.setup) + (ot5.idle + ot5.off) * 0.91*(select t.run2 from empapp t where t.name='D'))/
                ((ot5.run +ot5.E_RUN+ ot5.setup) + (ot5.idle + ot5.off) * (select t.up from empapp t where t.name='D' )),
                4)
         else
          0
       end Run2,
         case
         when (ot5.run + ot5.setup + ot5.idle + ot5.amhs_idle+ot5.off * (select t.run2 from empapp t where t.name='D' )) > 0 then
          round((ot5.run + ot5.setup + ot5.idle + ot5.amhs_idle+ot5.off * (select t.run2 from empapp t where t.name='D' )) /
                ((select to_date('{0}', 'yyyyMMdd') -
                                 to_date('{1}', 'yyyyMMdd')
                            from dual) * 24 * 60),
                4)
         else
          0
       end up2
       

  from (select ot4.*,
               
               case
                 when ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle > 0 then
                  round((ot4.run + ot4.setup) /
                        (ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle),
                        4)
                 else
                  0
               end as Run2_non,
               case
                 when (select to_date('{0}', 'yyyyMMdd') -
                              to_date('{1}', 'yyyyMMdd') 
                         from dual) > 0 then
                  round((ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle) /
                        ((select to_date('{0}', 'yyyyMMdd') -
                                 to_date('{1}', 'yyyyMMdd') 
                            from dual) * 24 * 60),
                        4)
                 else
                  0
               end as Up2_non
        
          from (
                
                select '{1}' as starttime, '{0}' as endtime, ot3.*
                  from (
                         
                         select ot2.*
                           from (
                                  
                                  select ot1.eqpid,
                                          ot1.act,
                                          sum(ot1.run) as run,
                                          sum(ot1.E_RUN) as e_run,
                                          sum(ot1.M_RUN) as m_run,
                                          sum(ot1.setup) as setup,
                                          sum(ot1.P_setup) as p_setup,
                                          sum(ot1.e_setup) as e_setup,
                                          sum(ot1.idle) as idle,
                                          sum(ot1.amhs_idle) as amhs_idle,
                                          sum(ot1.off) as off
                                    from (select idx.cutoffkey,
                                                  eq.modulename as EQPID,
                                                  'Act.' as Act,
                                                  --ROUND(idx.TTM / 60, 2) as TTM,
                                                  ROUND(idx.prd / 60, 2) as RUN,
                                                  ROUND(idx.ENG / 60, 2) as E_RUN,
                                                  '0' as M_RUN,
                                                  ROUND(idx.setup / 60, 2) as SETUP,
                                                  '0' as P_SETUP,
                                                  '0' as E_SETUP,
                                                  ROUND(idx.sby / 60, 2) as IDLE,
                                                  --ROUND(idx.ENG / 60, 2) as ENG,
                                                  
                                                  /*ROUND(idx.pm / 60, 2) as PM,
                                                                                                 ROUND(idx.pmmqc / 60, 2) as PM_MQC,
                                                                                                 ROUND(idx.eqd / 60, 2) as EQ_D,
                                                                                                 ROUND(idx.alm / 60, 2) as ALARM,
                                                                                                 ROUND(idx.dmqc / 60, 2) as D_MQC,*/
                                                  '0' as AMHS_IDLE,
                                                  ROUND(idx.nst / 60, 2) as off
                                             from empaidxsummdaily idx, equipment eq
                                            where idx.line = eq.line
                                              and idx.equipmentid = eq.modulename
                                              and eq.line in ('T1ARRAY','T0ARRAY')
                                              and eq.area in('1A-PHOTO','0A-PHOTO','0A-TF','1A-TF','1A-ETCH')
                                              and eq.moduletype = 'MAIN'
                                              and (eq.equipmentid like '1APHT%' or eq.equipmentid like '0AEXP%' or eq.equipmentid like '0APVD%' or eq.equipmentid like '0ACOA%' or eq.equipmentid like '1APVD%' or eq.equipmentid like '1ACVD%' or eq.equipmentid like '1AANO%' or eq.equipmentid like '1AWET%' )
                                              and idx.cutoffcycle = 'D'
                                              and idx.cutoffkey >= '{1}'
                                              and idx.cutoffkey < '{0}'
                                           --order by EQPID
                                           
                                           ) ot1
                                   group by ot1.eqpid, ot1.act
                                  
                                  --order by EQPID 
                                  ) ot2
                         
                         
                         ) ot3
                
                 order by ot3.eqpid, ot3.act
                
                ) ot4) ot5
)ot6

) ot7,
(select * from pp_north_south ) ot8
where ot7.eqpid(+)= ot8.eq








";
        sql_temp = string.Format(sql_temp, today, yesturday);

        ds_temp = func.get_dataSet_access(sql_temp, conn1);

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
        {
            sql_temp1 = @"

       
insert into north_source_data
  (shop, starttime, endtime, eqpid, up_time, run_time, up_time_modify, run_time_modify, up_plan, run_plan, operation_time, dttm)
values
  ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, {7}, {8}, {9}, {10}, sysdate)

";
            sql_temp1 = string.Format(sql_temp1, ds_temp.Tables[0].Rows[i][0].ToString(), ds_temp.Tables[0].Rows[i][1].ToString(), ds_temp.Tables[0].Rows[i][2].ToString(), ds_temp.Tables[0].Rows[i][3].ToString(), ds_temp.Tables[0].Rows[i][4].ToString(), ds_temp.Tables[0].Rows[i][5].ToString(), ds_temp.Tables[0].Rows[i][6].ToString(), ds_temp.Tables[0].Rows[i][7].ToString(), ds_temp.Tables[0].Rows[i][8].ToString(), ds_temp.Tables[0].Rows[i][9].ToString(), ds_temp.Tables[0].Rows[i][10].ToString());

            func.get_sql_execute(sql_temp1, conn2);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        func.write_log("asset_utilization_ratio ", Server.MapPath("..\\") + "\\LOG\\", "log");

        yesturday = txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd");
        today = txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd");

        sql_temp = @"

select 

       case when  substr(eq.modulename,0,2)='0A' then 'T0ARRAY'
            when  substr(eq.modulename,0,2)='1A' then 'T1ARRAY'
            when  substr(eq.modulename,0,2)='0C' then 'T0CELL'
            when  substr(eq.modulename,0,2)='1C' then 'T1CELL'
            when  substr(eq.modulename,0,2)='1F' then 'T1CF'
            when  substr(eq.modulename,0,2)='1W' then 'T1CELL'
            when  substr(eq.modulename,0,2)='0W' then 'T0CELL'
            else 'NA'
            end SHOP,
            
    
       idx.cutoffkey as shiftdate,
       eq.modulename as EQPID,
      -- ROUND(idx.ttm / 60, 2) as TTM,
       ROUND(idx.prd / 60/60, 3) as RUN,
       ROUND(idx.sby / 60/60, 3) as IDLE,
       ROUND(idx.ENG /60/60, 3) as ENG,
       ROUND(idx.setup / 60/60, 3) as SETUP,
       ROUND(idx.pm / 60/60, 3) as PM,
       ROUND(idx.pmmqc / 60/60, 3) as PM_MQC,
       ROUND(idx.eqd / 60/60, 3) as EQ_D,
       ROUND(idx.alm /60/60, 3) as ALARM,
       ROUND(idx.dmqc /60/60, 3) as D_MQC,
       ROUND(idx.nst / 60/60, 3) as off,
       '0' as ERUN,
       '0' as MRUN,
       '0' as P_SET,
       '0' as E_SET,
       to_char(sysdate,'yyyyMMddHH24MISS') as dttm
   from empaidxsummdaily idx, equipment eq,asset_utilization_ratio aur
 where 
   --idx.line = eq.line
     idx.equipmentid = eq.modulename
   -- eq.line = 'T1ARRAY'
   --and eq.area = '1A-PHOTO'
   --and eq.modelname = '1APHT'
   --and eq.moduletype = 'MAIN'
   and idx.cutoffcycle = 'D'
   and idx.cutoffkey >='{1}' 
   and idx.cutoffkey<'{0}'
   and aur.eqpid= eq.modulename
 order by 1,2,3







";
        sql_temp = string.Format(sql_temp, today, yesturday);

        ds_temp = func.get_dataSet_access(sql_temp, conn1);

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
        {
            sql_temp1 = @"

       
insert into asset_utilization_ratio
  (shop, shiftdate, eqpid, run, idle, eng, setup, pm, pm_mqc, eq_d, alarm, d_mqc, off, erun, mrun, p_set, e_set, dttm)
values
  ('{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, sysdate)

";
            sql_temp1 = string.Format(sql_temp1, ds_temp.Tables[0].Rows[i][0].ToString(), ds_temp.Tables[0].Rows[i][1].ToString(), ds_temp.Tables[0].Rows[i][2].ToString(), ds_temp.Tables[0].Rows[i][3].ToString(), ds_temp.Tables[0].Rows[i][4].ToString(), ds_temp.Tables[0].Rows[i][5].ToString(), ds_temp.Tables[0].Rows[i][6].ToString(), ds_temp.Tables[0].Rows[i][7].ToString(), ds_temp.Tables[0].Rows[i][8].ToString(), ds_temp.Tables[0].Rows[i][9].ToString(), ds_temp.Tables[0].Rows[i][10].ToString(), ds_temp.Tables[0].Rows[i][11].ToString(), ds_temp.Tables[0].Rows[i][12].ToString(), ds_temp.Tables[0].Rows[i][13].ToString(), ds_temp.Tables[0].Rows[i][14].ToString(), ds_temp.Tables[0].Rows[i][15].ToString(), ds_temp.Tables[0].Rows[i][16].ToString());

            func.get_sql_execute(sql_temp1, conn2);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        func.write_log("cell_beol ", Server.MapPath("..\\") + "\\LOG\\", "log");

        yesturday = txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd");
        today = txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd");

        sql_temp = @"

select ot1.*,to_char(sysdate,'yyyyMMddHH24MISS') as DTTM from (
select substr(t.line, 0, 2) as shop,
       'MES' as SRC_SYS_CD,
       case
         when substr(t.line, 0, 2) = 'T0' or substr(t.line, 0, 2) = 'T1' then
          'CNL1'
         when substr(t.line, 0, 2) = 'T2' then
          'CNL2'
         else
          'NA'
       end PLANT_LOC_ID
       
      ,
      t.cutoffkey as MFG_DT,
      t1.EQUIP_PROC_TYPE_CD,
      t.equipmentid,
      round(t.eng/3600,2) as eng,
      round(t.dmqc/3600,2) as dmqc,
      '0' as PE_WARM_UP_MEAS,
      round(t.pmmqc/3600,2) as OTH_WARM_UP_MEAS,
      round((t.eqd/3600+t.alm/3600),2) as EQ_DOWN_MEAS,
      '0' as PE_DOWN_MEAS,
      round(t.pm/3600,2) as PM_MEAS,
      round(t.nst/3600,2)   as SHDN_MEAS,
      '0 'as FAC_DOWN_MEAS,
      '0' as SYS_DOWN_MEAS,
      '0' as EQ_MOD_MEAS,
     round((t.dmqc/3600+t.PMMQC/3600+t.eqd/3600+t.alm/3600+t.pm/3600+t.nst/3600 ),2)    as DOWN_TIME_MEAS,
      round(t.prd/3600,2) as RUN_MEAS,
      '0' as MRUN_MEAS,
      round(t.Eng/3600,2) as ERUN_MEAS,
      round(t.sby/3600,2) as IDLE_MEAS,
      round(t.setup/3600,2) as PSETUP_MEAS,
      '0' as ESETUP_MEAS,
      '0' as MTRL_CHG_MEAS,
      '0' as AMHS_MEAS,
      round((t.dmqc/3600+t.pmmqc/3600+t.eqd/3600+t.alm/3600+t.pm/3600+t.nst/3600+t.prd/3600+t.eng/3600+t.sby/3600+t.setup/3600),2)  as TTL_TIME_MEAS,
      round((t.prd/3600+t.eng/3600+t.sby/3600+t.setup/3600),2)  as UP_TIME_MEAS,
       round(( t.prd/3600+t.eng/3600+t.setup/3600 ),2)as RUN_TIME_MEAS,
      round(t.eng/3600,2)  as ELOT_MEAS,
       round(((t.prd/3600)*t1.uph),2) as TGT_MOVE_QTY,
       '0' as MOVE_PROD_QTY,
       '0' as MOVE_ENG_QTY,
       case when t2.TGT_UPH_MEAS is null then 0 else round(t2.TGT_UPH_MEAS,2) end as TGT_UPH_MEAS ,
       case when (t.movement=0 or t.prd=0) then 0
       else  round((3600/(t.movement/(t.prd/3600)) ),2)end WTT_MEAS,
       '0' as TGT_UPH_RATE,
       round((t1.up_pct*t1.run_pct*t1.uph_pct),2) as TGT_OEE_RATE,
       '0' as TGT_UP_RATE,
       '0' as TGT_RUN_RATE,
       round((t.prd/3600+t.eng/3600+t.sby/3600+t.setup/3600)-t.sby/3600+(t.sby/3600+t.nst/3600)*t1.up_pct,2)  as UP_TIME_MOD_MEAS,
       round((t.prd/3600+t.eng/3600+t.setup/3600)+(t.sby/3600+t.nst/3600)*t1.up_pct*t1.run_pct,2) as RUN_TIME_MOD_MEAS,
       round((t.nst/3600+t.sby/3600),2)  as SHDW_IDLE_TIME_MEAS
       
  from empaidxsummdaily t,cel_pp_beol_cofig t1,(
select t.cutoffcycle,
       t.equipmentid,
    3600/(case when  (sum(t.totalprocessedcount * t.endtacttime) / (case
         when sum(t.totalprocessedcount) = 0 then
          1
         else
          sum(t.totalprocessedcount)
       end)) =0 then 1
       else (sum(t.totalprocessedcount * t.endtacttime) / (case
         when sum(t.totalprocessedcount) = 0 then
          1
         else
          sum(t.totalprocessedcount)
       end)) end)  as TGT_UPH_MEAS
           
       
       
       
       
  from empastsummdaily t

 where  t.cutoffkey>='{1}'
        and t.cutoffkey<'{0}'
   and t.cutoffcycle = 'D'
/*and 
t.equipmentid in (
     
      '1CSSB400', '1CSSB500', '1CSSB600', '1CSSB700',
      '1CSSB800', '1CSSB900', '1CSSBA00', '1CSSBB00',
      '1CPAL100', '1CLTS100', '1CLTS200'
     
     )*/

 group by t.cutoffcycle, t.equipmentid
  
  
  
  
  
  
  
  )t2
 where      t.cutoffkey>='{1}'
        and t.cutoffkey<'{0}'
   and t.cutoffcycle = 'D'
   and t.equipmentid in (
   
        
       select t5.equip_id from  cel_pp_beol_cofig t5 where t5.plant_loc_id='CNL1'
     /*   '1CSSB400', '1CSSB500', '1CSSB600', '1CSSB700',
        '1CSSB800', '1CSSB900', '1CSSBA00', '1CSSBB00',
        '1CPAL100', '1CLTS100', '1CLTS200'
       */
       )
  
   and t.equipmentid= t1.equip_id
   and t.equipmentid=t2.equipmentid(+)

)ot1
order by ot1.equipmentid ,ot1.mfg_dt








";
        sql_temp = string.Format(sql_temp, today, yesturday);

        ds_temp = func.get_dataSet_access(sql_temp, conn1);

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
        {
            sql_temp1 = @"
insert into cell_beol
(shop, src_sys_cd, plant_loc_id, mfg_dt, equip_proc_type_cd, equip_id, eng_meas, eq_warm_up_meas, pe_warm_up_meas, oth_warm_up_meas, eq_down_meas, pe_down_meas, pm_meas, shdn_meas, fac_down_meas, sys_down_meas, eq_mod_meas, down_time_meas, run_meas, mrun_meas, erun_meas, idle_meas, psetup_meas, esetup_meas, mtrl_chg_meas, amhs_meas, ttl_time_meas, up_time_meas, run_time_meas, elot_meas, tgt_move_qty, move_prod_qty, move_eng_qty, tgt_uph_meas, wtt_meas, tgt_uph_rate, tgt_oee_rate, tgt_up_rate, tgt_run_rate, up_time_mod_meas, run_time_mod_meas, shdw_idle_time_meas, dttm)
values
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7},  {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},sysdate)
      


";
            sql_temp1 = string.Format(sql_temp1, ds_temp.Tables[0].Rows[i][0].ToString(), ds_temp.Tables[0].Rows[i][1].ToString(), ds_temp.Tables[0].Rows[i][2].ToString(), ds_temp.Tables[0].Rows[i][3].ToString(), ds_temp.Tables[0].Rows[i][4].ToString(), ds_temp.Tables[0].Rows[i][5].ToString(), ds_temp.Tables[0].Rows[i][6].ToString(), ds_temp.Tables[0].Rows[i][7].ToString(), ds_temp.Tables[0].Rows[i][8].ToString(), ds_temp.Tables[0].Rows[i][9].ToString(), ds_temp.Tables[0].Rows[i][10].ToString(), ds_temp.Tables[0].Rows[i][11].ToString(), ds_temp.Tables[0].Rows[i][12].ToString(), ds_temp.Tables[0].Rows[i][13].ToString(), ds_temp.Tables[0].Rows[i][14].ToString(), ds_temp.Tables[0].Rows[i][15].ToString(), ds_temp.Tables[0].Rows[i][16].ToString(), ds_temp.Tables[0].Rows[i][17].ToString(), ds_temp.Tables[0].Rows[i][18].ToString(), ds_temp.Tables[0].Rows[i][19].ToString(), ds_temp.Tables[0].Rows[i][20].ToString(), ds_temp.Tables[0].Rows[i][21].ToString(), ds_temp.Tables[0].Rows[i][22].ToString(), ds_temp.Tables[0].Rows[i][23].ToString(), ds_temp.Tables[0].Rows[i][24].ToString(), ds_temp.Tables[0].Rows[i][25].ToString(), ds_temp.Tables[0].Rows[i][26].ToString(), ds_temp.Tables[0].Rows[i][27].ToString(), ds_temp.Tables[0].Rows[i][28].ToString(), ds_temp.Tables[0].Rows[i][29].ToString(), ds_temp.Tables[0].Rows[i][30].ToString(), ds_temp.Tables[0].Rows[i][31].ToString(), ds_temp.Tables[0].Rows[i][32].ToString(), ds_temp.Tables[0].Rows[i][33].ToString(), ds_temp.Tables[0].Rows[i][34].ToString(), ds_temp.Tables[0].Rows[i][35].ToString(), ds_temp.Tables[0].Rows[i][36].ToString(), ds_temp.Tables[0].Rows[i][37].ToString(), ds_temp.Tables[0].Rows[i][38].ToString(), ds_temp.Tables[0].Rows[i][39].ToString(), ds_temp.Tables[0].Rows[i][40].ToString(), ds_temp.Tables[0].Rows[i][41].ToString());

            func.get_sql_execute(sql_temp1, conn2);
        }
    }
}
