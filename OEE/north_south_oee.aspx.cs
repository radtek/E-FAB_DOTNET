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

public partial class OEE_north_south_oee : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ALCS_XLS"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];


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
    DateTime tmp_dt = DateTime.Now.AddDays(-2);
    string Vbef_yesterday = DateTime.Now.AddDays(-2).ToString("yyyyMMdd");

    DateTime tmp_bef_today = DateTime.Now.AddDays(-1);
    string Vbef_today = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");

    ArrayList arlist_temp1 = new ArrayList();


    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {


            txtEstimateSTARTTIME.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd"));
            txtEstimateEndTime.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd"));

             
            tmp_dt = Convert.ToDateTime(txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyy/MM/dd")).AddDays(-1);
            Vbef_yesterday = tmp_dt.ToString("yyyyMMdd");

            tmp_bef_today = Convert.ToDateTime(txtEstimateEndTime.SelectedDate.Value.ToString("yyyy/MM/dd")).AddDays(-1);
            Vbef_today = tmp_bef_today.ToString("yyyyMMdd");

            //#region hour
            //arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\hour.txt");





            //DropDownList3.DataSource = arlist_temp1;
            //DropDownList3.DataBind();
            //DropDownList3.Items.Insert(0, today_hour);



            //DropDownList5.DataSource = arlist_temp1;
            //DropDownList5.DataBind();
            //DropDownList5.Items.Insert(0, today_hour);

            //#endregion


            //#region min
            //arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\min.txt");

            //DropDownList4.DataSource = arlist_temp1;
            //DropDownList4.DataBind();
            //DropDownList4.Items.Insert(0, today_hour);


            //DropDownList6.DataSource = arlist_temp1;
            //DropDownList6.DataBind();
            //DropDownList6.Items.Insert(0, today_min);


            //#endregion
            sql_temp = @"



select  ot7.STARTTIME,ot7.endtime,ot7.EQPID,
       ot7.RUN+ot7.E_RUN+ot7.SETUP+ot7.IDLE as up_time
       ,ot7.RUN+ot7.E_RUN+ot7.SETUP as run_time
       ,( ot7.RUN+ot7.E_RUN+ot7.SETUP+ (ot7.IDLE+ot7.OFF)*ot8.up_p  )   as Up_time_Modify
       ,( ot7.RUN+ot7.E_RUN+ot7.SETUP+(ot7.IDLE+ot7.OFF)*ot8.up_p*ot8.uu_p  ) as Run_time_Modify
       ,ot8.up_p as UP_Plan
       ,ot8.uu_p as Run_Plan
       , round(((24*60-ot7.off)/60),2) as operation_time,
       case when ot9.movement1 is null then 0 else ot9.movement1 end  as movement,
       case when ot9.UPH is null then 0 else ot9.UPH end as UPH ,
       case when ot9.WTT is null then 0 else ot9.WTT end as WTT
      
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
                ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd')
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
                 when (select to_date('{1}', 'yyyyMMdd') -
                              to_date('{0}', 'yyyyMMdd') 
                         from dual) > 0 then
                  round((ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle) /
                        ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd') 
                            from dual) * 24 * 60),
                        4)
                 else
                  0
               end as Up2_non
        
          from (
                
                select '{0}' as starttime, '{1}' as endtime, ot3.*
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
                                          sum(ot1.off) as off,
                                          sum(ot1.movement)as movement
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
                                                  ROUND(idx.nst / 60, 2) as off,
                                                  idx.movement
                                             from empaidxsummdaily idx, equipment eq
                                            where idx.line = eq.line
                                              and idx.equipmentid = eq.modulename
                                              and eq.line in ('T1ARRAY','T0ARRAY')
                                             and eq.area in('1A-PHOTO','0A-PHOTO','0A-TF','1A-TF','1A-ETCH','0A-ETCH')
                                              and eq.moduletype = 'MAIN'
                                              and (eq.equipmentid like '1APHT%' or eq.equipmentid like '0AEXP%' or eq.equipmentid like '0APVD%' or eq.equipmentid like '0ACOA%' or eq.equipmentid like '1APVD%' or eq.equipmentid like '1ACVD%' or eq.equipmentid like '1AANO%' or eq.equipmentid like '1AWET%' or eq.equipmentid like '0ACVD%'  or eq.equipmentid like '0AWET%'  or eq.equipmentid like '0ADET%'   or eq.equipmentid like '0ADEV%')
                                              and idx.cutoffcycle = 'D'
                                              and idx.cutoffkey >= '{0}'
                                              and idx.cutoffkey < '{1}'
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
(select * from pp_north_south ) ot8,(
 select
       
        t.line,
        t.equipmentid,
        sum(t.totalprocessedcount) as movement1,
        case
          when sum(t.totalprocessedcount) = 0 then
           0
          else
          round(sum(t2.tacttime * t.totalprocessedcount) /
           sum(t.totalprocessedcount),4) 
        end as WTT,
        case
          when
               sum(t.totalprocessedcount) = 0 or sum(t2.tacttime * t.totalprocessedcount)=0 then
           0
          else
           round(3600 / (sum(t2.tacttime * t.totalprocessedcount) /
           sum(t.totalprocessedcount)),4)
        end as UPH
       
         from empastsummdaily t, empasttarget t2
        where 1 = 1
          -- and t.line = 'T0ARRAY'
          and t.Cutoffcycle = 'D'
          and t.cutoffkey >= '{0}'
          and t.cutoffkey < '{1}'
          --and t.equipmentid = t1.eq
          and t.line in ('T0ARRAY','T1ARRAY')
          and t.line = t2.line
          and t.equipmentid = t2.equipmentid
          and t.productid = t2.productid
          and t.stepid = t2.stepid
          and t.productid not like '%Don%'
          and t.productid not like '%MQC%'
          and t.productid not like '%ENG%'
          and trim(t.processgroup) is null 
        group by t.line, t.equipmentid

)ot9
where ot7.eqpid(+)= ot8.eq
and ot9.equipmentid(+)=ot8.eq 






";


            sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd"), Vbef_yesterday, Vbef_today);


            Bind_data(sql_temp, conn1);

        }


        tmp_dt = Convert.ToDateTime(txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyy/MM/dd")).AddDays(-1);
        Vbef_yesterday = tmp_dt.ToString("yyyyMMdd");


        tmp_bef_today = Convert.ToDateTime(txtEstimateEndTime.SelectedDate.Value.ToString("yyyy/MM/dd")).AddDays(-1);
        Vbef_today = tmp_bef_today.ToString("yyyyMMdd");




    }

    public DataSet Bind_data(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);

        Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();

        GridView1.DataSource = ds_temp.Tables[0];


        GridView1.DataBind();



        return ds_temp;

    }

    protected void ButtonQuery_Click(object sender, EventArgs e)
    {

        if (CheckBox1.Checked)
        {
            sql_temp = @"

select  ot7.STARTTIME,ot7.endtime,ot7.EQPID,
       ot7.RUN+ot7.E_RUN+ot7.SETUP+ot7.IDLE as up_time
       ,ot7.RUN+ot7.E_RUN+ot7.SETUP as run_time
       ,( ot7.RUN+ot7.E_RUN+ot7.SETUP+ (ot7.IDLE+ot7.OFF)*ot8.up_p  )   as Up_time_Modify
       ,( ot7.RUN+ot7.E_RUN+ot7.SETUP+(ot7.IDLE+ot7.OFF)*ot8.up_p*ot8.uu_p  ) as Run_time_Modify
       ,ot8.up_p as UP_Plan
       ,ot8.uu_p as Run_Plan
       , round(((24*60-ot7.off)/60),2) as operation_time,
       case when ot9.movement1 is null then 0 else ot9.movement1 end  as movement,
       case when ot9.UPH is null then 0 else ot9.UPH end as UPH ,
       case when ot9.WTT is null then 0 else ot9.WTT end as WTT
      
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
                ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd')
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
                 when (select to_date('{1}', 'yyyyMMdd') -
                              to_date('{0}', 'yyyyMMdd') 
                         from dual) > 0 then
                  round((ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle) /
                        ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd') 
                            from dual) * 24 * 60),
                        4)
                 else
                  0
               end as Up2_non
        
          from (
                
                select '{0}' as starttime, '{1}' as endtime, ot3.*
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
                                          sum(ot1.off) as off,
                                          sum(ot1.movement)as movement
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
                                                  ROUND(idx.nst / 60, 2) as off,
                                                  idx.movement
                                             from empaidxsummdaily idx, equipment eq
                                            where idx.line = eq.line
                                              and idx.equipmentid = eq.modulename
                                              and eq.line in ('T1ARRAY','T0ARRAY')
                                             and eq.area in('1A-PHOTO','0A-PHOTO','0A-TF','1A-TF','1A-ETCH','0A-ETCH')
                                              and eq.moduletype = 'MAIN'
                                              and (eq.equipmentid like '1APHT%' or eq.equipmentid like '0AEXP%' or eq.equipmentid like '0APVD%' or eq.equipmentid like '0ACOA%' or eq.equipmentid like '1APVD%' or eq.equipmentid like '1ACVD%' or eq.equipmentid like '1AANO%' or eq.equipmentid like '1AWET%' or eq.equipmentid like '0ACVD%'  or eq.equipmentid like '0AWET%'  or eq.equipmentid like '0ADET%'   or eq.equipmentid like '0ADEV%')
                                              and idx.cutoffcycle = 'D'
                                              and idx.cutoffkey >= '{0}'
                                              and idx.cutoffkey < '{1}'
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
(select * from pp_north_south ) ot8,(
 select
       
        t.line,
        t.equipmentid,
        sum(t.totalprocessedcount) as movement1,
        case
          when sum(t.totalprocessedcount) = 0 then
           0
          else
          round(sum(t2.tacttime * t.totalprocessedcount) /
           sum(t.totalprocessedcount),4) 
        end as WTT,
        case
          when
               sum(t.totalprocessedcount) = 0 or sum(t2.tacttime * t.totalprocessedcount)=0 then
           0
          else
           round(3600 / (sum(t2.tacttime * t.totalprocessedcount) /
           sum(t.totalprocessedcount)),4)
        end as UPH
       
         from empastsummdaily t, empasttarget t2
        where 1 = 1
          -- and t.line = 'T0ARRAY'
          and t.Cutoffcycle = 'D'
          and t.cutoffkey >= '{0}'
          and t.cutoffkey < '{1}'
          --and t.equipmentid = t1.eq
          and t.line in ('T0ARRAY','T1ARRAY')
          and t.line = t2.line
          and t.equipmentid = t2.equipmentid
          and t.productid = t2.productid
          and t.stepid = t2.stepid
          and t.productid not like '%Don%'
          and t.productid not like '%MQC%'
          and t.productid not like '%ENG%'
          and trim(t.processgroup) is null 
        group by t.line, t.equipmentid

)ot9
where ot7.eqpid(+)= ot8.eq
and ot9.equipmentid(+)=ot8.eq 





";
        
        }
        else
        {

            sql_temp = @"

select  ot7.STARTTIME,ot7.endtime,ot7.EQPID,
       ot7.RUN+ot7.E_RUN+ot7.SETUP+ot7.IDLE as up_time
       ,ot7.RUN+ot7.E_RUN+ot7.SETUP as run_time
       ,( ot7.RUN+ot7.E_RUN+ot7.SETUP+ (ot7.IDLE+ot7.OFF)*ot8.up_p  )   as Up_time_Modify
       ,( ot7.RUN+ot7.E_RUN+ot7.SETUP+(ot7.IDLE+ot7.OFF)*ot8.up_p*ot8.uu_p  ) as Run_time_Modify
       ,ot8.up_p as UP_Plan
       ,ot8.uu_p as Run_Plan
       , round(((24*60-ot7.off)/60),2) as operation_time,
         case when ot9.movement1 is null then 0 else ot9.movement1 end  as movement,
       case when ot9.UPH is null then 0 else ot9.UPH end as UPH ,
       case when ot9.WTT is null then 0 else ot9.WTT end as WTT
      
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
                ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd')
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
                 when (select to_date('{1}', 'yyyyMMdd') -
                              to_date('{0}', 'yyyyMMdd') 
                         from dual) > 0 then
                  round((ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle) /
                        ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd') 
                            from dual) * 24 * 60),
                        4)
                 else
                  0
               end as Up2_non
        
          from (
                
                select '{0}' as starttime, '{1}' as endtime, ot3.*
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
                                          sum(ot1.off) as off,
                                          sum(ot1.movement)as movement
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
                                                  ROUND(idx.nst / 60, 2) as off,
                                                  idx.movement
                                             from empaidxsummdaily idx, equipment eq
                                            where idx.line = eq.line
                                              and idx.equipmentid = eq.modulename
                                              and eq.line in ('T1ARRAY','T0ARRAY')
                                             and eq.area in('1A-PHOTO','0A-PHOTO','0A-TF','1A-TF','1A-ETCH','0A-ETCH')
                                              and eq.moduletype = 'MAIN'
                                              and (eq.equipmentid like '1APHT%' or eq.equipmentid like '0AEXP%' or eq.equipmentid like '0APVD%' or eq.equipmentid like '0ACOA%' or eq.equipmentid like '1APVD%' or eq.equipmentid like '1ACVD%' or eq.equipmentid like '1AANO%' or eq.equipmentid like '1AWET%' or eq.equipmentid like '0ACVD%'  or eq.equipmentid like '0AWET%'  or eq.equipmentid like '0ADET%'   or eq.equipmentid like '0ADEV%')
                                              and idx.cutoffcycle = 'D'
                                              and idx.cutoffkey >= '{0}'
                                              and idx.cutoffkey < '{1}'
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
(select * from pp_north_south ) ot8,(
 select
       
        t.line,
        t.equipmentid,
        sum(t.totalprocessedcount) as movement1,
        case
          when sum(t.totalprocessedcount) = 0 then
           0
          else
          round(sum(t2.tacttime * t.totalprocessedcount) /
           sum(t.totalprocessedcount),4) 
        end as WTT,
        case
          when
               sum(t.totalprocessedcount) = 0 or sum(t2.tacttime * t.totalprocessedcount)=0 then
           0
          else
           round(3600 / (sum(t2.tacttime * t.totalprocessedcount) /
           sum(t.totalprocessedcount)),4)
        end as UPH
       
         from empastsummdaily t, empasttarget t2
        where 1 = 1
          -- and t.line = 'T0ARRAY'
          and t.Cutoffcycle = 'D'
          and t.cutoffkey >= '{0}'
          and t.cutoffkey < '{1}'
          --and t.equipmentid = t1.eq
          and t.line in ('T0ARRAY','T1ARRAY')
          and t.line = t2.line
          and t.equipmentid = t2.equipmentid
          and t.productid = t2.productid
          and t.stepid = t2.stepid
          and t.productid not like '%Don%'
          and t.productid not like '%MQC%'
          and t.productid not like '%ENG%'
          and trim(t.processgroup) is null 
        group by t.line, t.equipmentid

)ot9
where ot7.eqpid(+)= ot8.eq
and ot9.equipmentid(+)=ot8.eq 


";
        }







        sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd"), Vbef_yesterday, Vbef_today);




        

        ds_temp = func.get_dataSet_access(sql_temp, conn1);


        Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();

        GridView1.DataSource = ds_temp.Tables[0];
        GridView1.DataBind();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        if (CheckBox1.Checked)
        {
            sql_temp = @"

select  ot7.STARTTIME,ot7.endtime,ot7.EQPID,
       ot7.RUN+ot7.E_RUN+ot7.SETUP+ot7.IDLE as up_time
       ,ot7.RUN+ot7.E_RUN+ot7.SETUP as run_time
       ,( ot7.RUN+ot7.E_RUN+ot7.SETUP+ (ot7.IDLE+ot7.OFF)*ot8.up_p  )   as Up_time_Modify
       ,( ot7.RUN+ot7.E_RUN+ot7.SETUP+(ot7.IDLE+ot7.OFF)*ot8.up_p*ot8.uu_p  ) as Run_time_Modify
       ,ot8.up_p as UP_Plan
       ,ot8.uu_p as Run_Plan
       , round(((24*60-ot7.off)/60) ,2)as operation_time
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
                ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd')
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
                 when (select to_date('{1}', 'yyyyMMdd') -
                              to_date('{0}', 'yyyyMMdd') 
                         from dual) > 0 then
                  round((ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle) /
                        ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd') 
                            from dual) * 24 * 60),
                        4)
                 else
                  0
               end as Up2_non
        
          from (
                
                select '{0}' as starttime, '{1}' as endtime, ot3.*
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
                                              and eq.area in('1A-PHOTO','0A-PHOTO','0A-TF','1A-TF','1A-ETCH','0A-ETCH')
                                              and eq.moduletype = 'MAIN'
                                              and (eq.equipmentid like '1APHT%' or eq.equipmentid like '0AEXP%' or eq.equipmentid like '0APVD%' or eq.equipmentid like '0ACOA%' or eq.equipmentid like '1APVD%' or eq.equipmentid like '1ACVD%' or eq.equipmentid like '1AANO%' or eq.equipmentid like '1AWET%' or eq.equipmentid like '0ACVD%'  or eq.equipmentid like '0AWET%'  or eq.equipmentid like '0ADET%'   or eq.equipmentid like '0ADEV%')
                                              and idx.cutoffcycle = 'D'
                                              and idx.cutoffkey >= '{0}'
                                              and idx.cutoffkey < '{1}'
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

        }
        else
        {

            sql_temp = @"

select  ot7.STARTTIME,ot7.endtime,ot7.EQPID,
       ot7.RUN+ot7.E_RUN+ot7.SETUP+ot7.IDLE as up_time
       ,ot7.RUN+ot7.E_RUN+ot7.SETUP as run_time
       ,( ot7.RUN+ot7.E_RUN+ot7.SETUP+ (ot7.IDLE+ot7.OFF)*ot8.up_p  )   as Up_time_Modify
       ,( ot7.RUN+ot7.E_RUN+ot7.SETUP+(ot7.IDLE+ot7.OFF)*ot8.up_p*ot8.uu_p  ) as Run_time_Modify
       ,ot8.up_p as UP_Plan
       ,ot8.uu_p as Run_Plan
       , round(((24*60-ot7.off)/60),2) as operation_time,
       case when ot9.movement1 is null then 0 else ot9.movement1 end  as movement,
       case when ot9.UPH is null then 0 else ot9.UPH end as UPH ,
       case when ot9.WTT is null then 0 else ot9.WTT end as WTT
      
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
                ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd')
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
                 when (select to_date('{1}', 'yyyyMMdd') -
                              to_date('{0}', 'yyyyMMdd') 
                         from dual) > 0 then
                  round((ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle) /
                        ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd') 
                            from dual) * 24 * 60),
                        4)
                 else
                  0
               end as Up2_non
        
          from (
                
                select '{0}' as starttime, '{1}' as endtime, ot3.*
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
                                          sum(ot1.off) as off,
                                          sum(ot1.movement)as movement
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
                                                  ROUND(idx.nst / 60, 2) as off,
                                                  idx.movement
                                             from empaidxsummdaily idx, equipment eq
                                            where idx.line = eq.line
                                              and idx.equipmentid = eq.modulename
                                              and eq.line in ('T1ARRAY','T0ARRAY')
                                             and eq.area in('1A-PHOTO','0A-PHOTO','0A-TF','1A-TF','1A-ETCH','0A-ETCH')
                                              and eq.moduletype = 'MAIN'
                                              and (eq.equipmentid like '1APHT%' or eq.equipmentid like '0AEXP%' or eq.equipmentid like '0APVD%' or eq.equipmentid like '0ACOA%' or eq.equipmentid like '1APVD%' or eq.equipmentid like '1ACVD%' or eq.equipmentid like '1AANO%' or eq.equipmentid like '1AWET%' or eq.equipmentid like '0ACVD%'  or eq.equipmentid like '0AWET%'  or eq.equipmentid like '0ADET%'   or eq.equipmentid like '0ADEV%')
                                              and idx.cutoffcycle = 'D'
                                              and idx.cutoffkey >= '{0}'
                                              and idx.cutoffkey < '{1}'
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
(select * from pp_north_south ) ot8,(
 select
       
        t.line,
        t.equipmentid,
        sum(t.totalprocessedcount) as movement1,
        case
          when sum(t.totalprocessedcount) = 0 then
           0
          else
          round(sum(t2.tacttime * t.totalprocessedcount) /
           sum(t.totalprocessedcount),4) 
        end as WTT,
        case
          when
               sum(t.totalprocessedcount) = 0 or sum(t2.tacttime * t.totalprocessedcount)=0 then
           0
          else
           round(3600 / (sum(t2.tacttime * t.totalprocessedcount) /
           sum(t.totalprocessedcount)),4)
        end as UPH
       
         from empastsummdaily t, empasttarget t2
        where 1 = 1
          -- and t.line = 'T0ARRAY'
          and t.Cutoffcycle = 'D'
          and t.cutoffkey >= '{0}'
          and t.cutoffkey < '{1}'
          --and t.equipmentid = t1.eq
          and t.line in ('T0ARRAY','T1ARRAY')
          and t.line = t2.line
          and t.equipmentid = t2.equipmentid
          and t.productid = t2.productid
          and t.stepid = t2.stepid
          and t.productid not like '%Don%'
          and t.productid not like '%MQC%'
          and t.productid not like '%ENG%'
          and trim(t.processgroup) is null 
        group by t.line, t.equipmentid

)ot9
where ot7.eqpid(+)= ot8.eq
and ot9.equipmentid(+)=ot8.eq 




";
        }



        sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd"), Vbef_yesterday, Vbef_today);




        //if (!TextBox1.Text.Equals(""))
        //{

        //    sql_temp = sql_temp + " and upper(ob1.user_mobil_tel) like '%" + TextBox1.Text.ToString().ToUpper() + "%' or upper(ob1.user_sms_num) like '%" + TextBox1.Text.ToString().ToUpper() + "%' or upper(ob1.user_e_mail) like '%" + TextBox1.Text.ToString().ToUpper() + "%' ";


        //}

        //if (!TextBox3.Text.Equals(""))
        //{

        //    sql_temp = sql_temp + " and upper(ob1.event_id) like '%" + TextBox3.Text.ToString().ToUpper() + "%' ";


        //}

        //if (!TextBox_Msg.Text.Equals(""))
        //{

        //    sql_temp = sql_temp + " and upper(ob1.alarm_text) like '%" + TextBox_Msg.Text.ToString().ToUpper() + "%'  or upper(ob1.alarm_comment) like '%" + TextBox_Msg.Text.ToString().ToUpper() + "%'";


        //}



        GridView gv = new GridView();

        ds_temp = func.get_dataSet_access(sql_temp, conn1);
        Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();
        gv.DataSource = ds_temp.Tables[0];
        gv.DataBind();
        ExportExcel(gv);

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //e.Row.Cells[13] = Convert.ToDouble(e.Row.Cells[14].Text).ToString("P1");
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
            //Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 

            //string pp = DataBinder.Eval(e.Row.DataItem, "Current_price").ToString();

            //Int32 pricexx = Convert.ToInt32(price1); 



            // if (percent_value >0) 
            //e.Row.Cells[0].BackColor = Color.Yellow; 
            // e.Row.Cells[6].Style.Add("background-color", "#FFFF80"); 
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
               //e.Row.Cells[14].Style.Add("background-color", "#FF9DFF");

            #region MyRegion

            //string run2_non = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "run2_non")).ToString("P2");

            //e.Row.Cells[14].Text = run2_non;

            //string UP2_NON = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "UP2_NON")).ToString("P2");

            //e.Row.Cells[15].Text = UP2_NON;

            //string OEE2_NON = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OEE2_NON")).ToString("P2");

            //e.Row.Cells[16].Text = OEE2_NON;

            //string RUN2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RUN2")).ToString("P2");

            //e.Row.Cells[17].Text = RUN2;

            //string UP2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "UP2")).ToString("P2");

            //e.Row.Cells[18].Text = UP2;


            //string OEE2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OEE2")).ToString("P2");

            //e.Row.Cells[19].Text = OEE2;

            #endregion

             

            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }
            //e.Row.Cells[i].Text = Convert.ToDouble(e.Row.Cells[i].Text).ToString("P1");
           // e.Row.Cells[13] = Convert.ToDouble(e.Row.Cells[14].Text).ToString("P1");
            //e.Row.Cells[14]= Convert.ToDouble(e.Row.Cells[14].Text).ToString("P1");
           // e.Row.Cells[15].Text = Convert.ToDouble(e.Row.Cells[15].Text).ToString("P1");
            //e.Row.Cells[16].Text = Convert.ToDouble(e.Row.Cells[16].Text).ToString("P1");
            //e.Row.Cells[17].Text = Convert.ToDouble(e.Row.Cells[17].Text).ToString("P1");
            //e.Row.Cells[18].Text = Convert.ToDouble(e.Row.Cells[18].Text).ToString("P1"); 
        }
    }




    private void ExportExcel(GridView SeriesValuesDataGrid)
    {

        string filename = "";
        string today_detail_char = DateTime.Now.AddDays(+0).ToString("yyyy/MM/ddHHmmss").Replace("/", "");
        filename = "T1OEE_" + today_detail_char + ".xls";
        filename = "attachment;filename=" + filename;
        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader("content-disposition", filename);

        //Response.AddHeader("content-disposition", "attachment;filename=\"" + filename + "\";"); 

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
}
