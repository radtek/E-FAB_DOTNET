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
using System.Data.OracleClient;

public partial class EDA_EDA_LCM_CHK_REPORT : System.Web.UI.Page
{
    private OracleConnection orcn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"]);
    

   
    


    //string conn = System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"];
    string conn = System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"];
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string sql_temp4 = "";
    string sql_stm = "";

    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    DataSet ds_temp4 = new DataSet();
    DataSet ds_temp5 = new DataSet();

    DataTable dt_t1array_south_step = new DataTable();

    DataTable dt_t1cell_south_step = new DataTable();

    string yesturday_shiftday = DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "0550";
    string today_shiftday = DateTime.Now.AddDays(+0).ToString("yyyyMMdd") + "0550";
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");

    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string today_detail1 = DateTime.Now.AddDays(+0).ToString("yyyyMMddHHmmss");
    string last_hour = DateTime.Now.AddDays(-1 / 24).ToString("yyyyMMddHH");
    string last_twohour = DateTime.Now.AddDays(-2 / 24).ToString("yyyyMMddHH");
    string SaveLocation = "";

    string mail_content = "";
    Int32 counter_oscar = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
//        string sql_temp = @" 
//select ot5.shiftdate,ot5.sites,ot5.shop_desc,sum(case when ot5.gsflag='SUBSTRATE' then ot5.qty else 0 end ) as SUB ,
//
//     sum(case when ot5.gsflag='CUT' then ot5.qty else 0 end ) as CUT
//    ,sum(case when ot5.gsflag='CHIP' then ot5.qty else 0 end ) as CHIP
//
//from (
//select ot2.shiftdate,ot2.sites,ot4.shop_desc,ot2.gsflag,ot2.qty from (
//
//
//select substr('{0}',0,8) as shiftdate,ot1.sites,ot1.gsflag ,count(ot1.sites) as qty from (
//
//select t.sites,t1.gsflag,t1.glassid,count(t1.glassid)as counter
//
//from td_wms_shipping_file t, mfginfo@eda2tclmes t1
//where t.carton_id=t1.carton_id
//and t.mes_ship_date=t1.mes_ship_date
//and t.shippingdate>='{0}'
//and t.shippingdate<'{1}'
//and t.shippingfrom in ('T1')
//and t.sites in (select distinct(t.shiipping_shop) from td_lcm_shipping_shop t)
//group by t.sites,t1.gsflag,t1.glassid
//
//) ot1
//group by ot1.sites,ot1.gsflag
//) ot2
//
//,(
//select * from td_lcm_shipping_shop t1
//)ot4
//
//where ot2.sites=ot4.shiipping_shop
//)ot5
//group by ot5.shiftdate,ot5.sites,ot5.shop_desc
//
//
//union 
//
//select 'Total','','',sum(SUB),sum(CUT),sum(CHIP) from (
//
//select ot5.shiftdate,ot5.sites,ot5.shop_desc,sum(case when ot5.gsflag='SUBSTRATE' then ot5.qty else 0 end ) as SUB ,
//
//     sum(case when ot5.gsflag='CUT' then ot5.qty else 0 end ) as CUT
//    ,sum(case when ot5.gsflag='CHIP' then ot5.qty else 0 end ) as CHIP
//
//from (
//select ot2.shiftdate,ot2.sites,ot4.shop_desc,ot2.gsflag,ot2.qty from (
//
//
//select substr('{0}',0,8) as shiftdate,ot1.sites,ot1.gsflag ,count(ot1.sites) as qty from (
//
//select t.sites,t1.gsflag,t1.glassid,count(t1.glassid)as counter
//
//from td_wms_shipping_file t, mfginfo@eda2tclmes t1
//where t.carton_id=t1.carton_id
//and t.mes_ship_date=t1.mes_ship_date
//and t.shippingdate>='{0}'
//and t.shippingdate<'{1}'
//and t.shippingfrom in ('T1')
//and t.sites in (select distinct(t.shiipping_shop) from td_lcm_shipping_shop t)
//group by t.sites,t1.gsflag,t1.glassid
//
//) ot1
//group by ot1.sites,ot1.gsflag
//) ot2
//
//,(
//select * from td_lcm_shipping_shop t1
//)ot4
//
//where ot2.sites=ot4.shiipping_shop
//)ot5
//group by ot5.shiftdate,ot5.sites,ot5.shop_desc
//
//)ot7
//
//
//
// ";

        string sql_temp = @" 

select bb4.shiftdate,bb4.sites,bb4.shop_desc,sum(bb4.SUB)as SUB,sum(bb4.CUT) as CUT,sum(bb4.chip) as CHIP,sum(bb4.step_1930) as step_1930,sum(bb4.step_3600) as step_3600,sum(bb4.step_3650) as step_3650,sum(bb4.step_4600) as step_4600,sum(bb4.step_4650) as step_4650 from 
(

select bb3.shiftdate,bb3.sites,bb3.shop_desc,bb3.sub,bb3.cut,bb3.chip,sum(step_1930) as step_1930,  
       sum(step_3600) as step_3600,  sum(step_3650) as step_3650, sum(step_4600) as step_4600,
       sum(step_4650) as step_4650



 from (

select bb1.*, case when bb2.north_step='1930' then bb2.counter else 0 end step_1930,
              case when bb2.north_step='3600' then bb2.counter else 0 end step_3600,
              case when bb2.north_step='3650' then bb2.counter else 0 end step_3650,
              case when bb2.north_step='4600' then bb2.counter else 0 end step_4600,
              case when bb2.north_step='4650' then bb2.counter else 0 end step_4650
from (

select ot5.shiftdate,ot5.sites,ot5.shop_desc,sum(case when ot5.gsflag='SUBSTRATE' then ot5.qty else 0 end ) as SUB ,

     sum(case when ot5.gsflag='CUT' then ot5.qty else 0 end ) as CUT
    ,sum(case when ot5.gsflag='CHIP' then ot5.qty else 0 end ) as CHIP

from (
select ot2.shiftdate,ot2.sites,ot4.shop_desc,ot2.gsflag,ot2.qty from (


select substr('{0}',0,8) as shiftdate,ot1.sites,ot1.gsflag ,count(ot1.sites) as qty from (

select t.sites,t1.gsflag,t1.glassid,count(t1.glassid)as counter

from td_wms_shipping_file t, mfginfo@eda2tclmes t1
where t.carton_id=t1.carton_id
and t.mes_ship_date=t1.mes_ship_date
and t.shippingdate>='{0}'
and t.shippingdate<'{1}'
and t.shippingfrom in ('T1')
and t.sites in (select distinct(t.shiipping_shop) from td_lcm_shipping_shop t)
group by t.sites,t1.gsflag,t1.glassid

) ot1
group by ot1.sites,ot1.gsflag
) ot2

,(
select * from td_lcm_shipping_shop t1
)ot4

where ot2.sites=ot4.shiipping_shop
)ot5
group by ot5.shiftdate,ot5.sites,ot5.shop_desc

) bb1,(

select 'Array' as North_shop,'1930' as north_step,ot7.south_SHOP,count(ot7.CHIP_ID) as counter from (

select ot6.CHIP_ID,ot6.south_shop,'SUB' as glass_type ,count(ot6.T_FLAG) as counter  from (


select 
       'T' as T_FLAG,
       'G' as G_FLAG,
       RPAD(RTRIM('G'), 2, ' ') as G_2_FLAG,
       '****' as spec_flag,
       '*********** ********* ************** ************** ****** ********* ****** ************** **************' as spec1_flag,
       '>No Data  Gate  Code Repr Deft Ty RT T1   T2 R Analysis Mod ADC    DefectPictureName       Code Repr Deft Ty RT DefectPictureName   ' as spec2_flag,
       '** * OTHERS       *      *********************** **** ************ ** ***********************' as spec3_flag,
       '@' as spec4_flag,
       ot3.step_id,
       'Y'||SUBSTR(ot2.CHIP_ID,7,20) as CHIP_ID ,
       ot3.GLASS_ID,
       RPAD(RTRIM(ot4.item3), 4, ' ') as lot_type,
       
       ot3.LOT_ID,
       RPAD(RTRIM(ot3.PRODUCT_ID), 8, ' ') as PRODUCT_ID,
     
       RPAD(RTRIM(ot3.EQUIP_ID), 9, ' ') as repr_EQUIP_ID,
       
       to_char(ot3.GLASS_START_TIME,'yyyyMMddHH24MISS') as REPR_STARTTIME,
       to_char(ot3.UPDATE_TIME,'yyyyMMddHH24MISS') as REPR_ENDTIME,
       RPAD(RTRIM(ot3.ITEM2), 8, ' ') as operator_id,
     
       case when RPAD(RTRIM(ot3.TEST_EQID), 9, ' ') is null then '*********'
       else
       RPAD(RTRIM(ot3.TEST_EQID), 9, ' ') end  as TEST_EQUIP_ID,
       
       case when to_char(ot3.TEST_STARTTIME,'yyyyMMddHH24MISS') is null then '**************'
       else
       to_char(ot3.TEST_STARTTIME,'yyyyMMddHH24MISS') end as TEST_STARTTIME,
       case when to_char(ot3.TEST_ENDTIME,'yyyyMMddHH24MISS') is null then '**************'
       else
       to_char(ot3.TEST_ENDTIME,'yyyyMMddHH24MISS')end as TEST_ENDTIME,
   
       LPAD(LTRIM(s), 5, '0') as s,
      
       
       LPAD(LTRIM(g), 5, '0') as g,
       substr(ot2.Item2,2,10) as RETYPE,
       
       ot2.item4 as defect_judge,
       ot2.item5 as defect_result,
       case when substr(ot2.item51,0,12) is null then '************'
       else
       RPAD(RTRIM(substr(ot2.item51,0,12)), 12, ' ')end as reason,
       case when ot2.item4='AR' and ot2.item5='2DP' then 'D '
            when    ot2.item4='AR' and ot2.item5='DP' then 'N '
            when     ot2.item4='AR' and ot2.item5='NP' then 'G '
            when     ot2.item4='NR' and ot2.item5='NP' then 'G '
            when     ot2.item4='F'  then 'B '
            when     ot2.item4='MD' then 'X '
            when    ot2.item4='CP' then 'L '
            when     ot2.item4='CR' then 'L '
            when     ot2.item4='NR' and ot2.item5='BP' then 'B '
            when     ot2.item4='NR' and ot2.item5='DP' then 'N '
                 else '**' end as RT,
       'TFTR2' as SOUTH_STEP,
       ot5.sites  as SOUTH_SHOP
                 
  from (select t.*,
               ot1.equip_id as test_eqid,
               ot1.glass_start_time as test_starttime,
               ot1.update_time as test_endtime
          from lcdsys.array_glass_t t,
               (
                
                select t.*
                  from lcdsys.array_glass_t t
                 where t.step_id in ('1920', 'S920', 'P920')
                
                ) ot1
         where t.step_id in ('1930', 'S930', 'P930')
          
           and t.glass_id = ot1.glass_id(+)
        
        ) ot3,
       lcdsys.array_defect_t ot2, lcdsys.array_lot_hst_t ot4,
  (

   select t.sites,
case when t1.gsflag='CHIP' then substr(t1.glassid,0,14)
     when t1.gsflag='CUT' then substr(t1.glassid,0,14)
       else t1.glassid end transfer_subid

from td_wms_shipping_file t, mfginfo@eda2tclmes t1
where t.carton_id=t1.carton_id
and t.mes_ship_date=t1.mes_ship_date
and t.shippingdate>='{0}'
and t.shippingdate<'{1}'
and t.shippingfrom in ('T1')
and t.sites in (select distinct(t.shiipping_shop) from td_lcm_shipping_shop t)
) ot5
       
 where ot3.step_id = ot2.step_id
   and ot3.glass_start_time = ot2.glass_start_time
   and ot3.glass_id = ot2.glass_id
   and ot2.ITEM2 <> '-'
   and ot3.lot_id=ot4.lot_id
   and ot3.step_id=ot4.step_id 
   and ot3.glass_id=ot5.transfer_subid
) ot6
 
 
group by ot6.CHIP_ID,ot6.south_shop,'1930','SUB'


)ot7

group by 'Array',ot7.south_SHOP,ot7.glass_type  




union 


select ot3.shop,ot3.south_step,ot3.south_shop,count(ot3.south_step) as counter from (

select 'Cell' as shop, ot2.CELL_CHIP_ID,ot2.SOUTH_STEP,ot2.SOUTH_SHOP,count(ot2.step_id) as counter  from (

select 
        'Y'||substr(ot1.CELL_CHIP_ID,7,20) as CELL_CHIP_ID ,
       ot1.step_id,
       ot1.equip_id,
      
      
       RPAD(RTRIM(ot1.SOURCE_CARRIER_ID), 8, ' ') as SOURCE_CARRIER_ID,
       '******' as spec_flag,
       case when ot1.step_id ='6615' then 'OY'
            when ot1.step_id ='6620' then 'OL'
            when ot1.step_id ='6630' then 'OY'
            when ot1.step_id ='6810' then 'OY'
            when ot1.step_id ='6815' then 'OY'
            when ot1.step_id ='6820' then 'OL'
            when ot1.step_id ='6830' then 'OY'
            else '**' end as OPERATION_MODE,
            
       '********' as SPEC1_FLAG,
       LPAD(LTRIM(round((case when (ot1.ENDTIME-ot1.STARTTIME)*24*60*60 >999 then 999
              else (ot1.ENDTIME-ot1.STARTTIME)*24*60*60 end),0)), 3, '0')  as TACT_TIME,
       '***' as SPEC2_FLAG,
       to_char(ot1.STARTTIME,'yyyyMMddHH24MISS') as STARTTIME,
        to_char(ot1.ENDTIME,'yyyyMMddHH24MISS') as ENDTIME,
        LPAD(LTRIM(ot1.OPERATOR_ID), 8, '0') as OPERATOR_ID,
       '**************** ** ****' as SPEC3_FLAG,
     
       '* **** **** ****' as SPEC4_FLAG,
       
       
       RPAD(RTRIM(ot1.Batch_Id), 20, ' ') as Batch_Id,
       '& **' as SPEC5_FLAG,
       '> * ******' as SPEC6_FLAG,
       '< **********' as SPEC7_FLAG,
       '{' as SPEC8_FLAG,
       '}' as SPEC9_FLAG,
       '[' as SPEC10_FLAG,
       ']' as SPEC11_FLAG,
       '%No Code Data  Gate  C RK Leve   ND  R PAT Defect Pattern Name  English Defect Name  CEDC Cell Re Eng Def Name JC BD Spare      Spare' as SPEC12_FLAG,
       substr(ot1.DEFECT_CODE,2,4) as DEFECT_CODE ,
        case when LPAD(LTRIM(ot1.S), 5, '0')='000-1' then '00000'
           else  LPAD(LTRIM(ot1.S), 5, '0') end as S,
       
       case when LPAD(LTRIM(ot1.G), 5, '0')='000-1' then '00000'
          else  LPAD(LTRIM(ot1.G), 5, '0') end as G,
        case when ot1.DEFECT_COLOR is null then '*'
             else ot1.DEFECT_COLOR end as DEFECT_COLOR ,
       '** ****** *** * *** ********************' as SPEC13_FLAG,
        RPAD(RTRIM(substr(ot1.DEFECT_NAME,0,20)), 20, ' ') as DEFECT_NAME,
       '**** ******************** **    ********** **********' as SPEC14_FLAG,
       '$N Cod STime Pattern Switch Name  Spare      Spare     ' as SPEC15_FLAG,
       '@' as SPEC16_FLAG,
      
       ot1.DEFECT_JUDGE_CODE,
       
      
       ot1.CT_DEFECT_CODE,
       case when ot1.step_id='6615' then '3600' 
            when ot1.step_id='6630' then '3650'  
            when ot1.step_id='6810' then '4600' 
            when ot1.step_id='6815' then '4600' 
            when ot1.step_id='6830' then '4650' 
          else 'NA' end  AS SOUTH_STEP,
       ot1.sites as SOUTH_SHOP,
       '**** **** ************ **** ***'as SPEC17_FLAG,
       '** **** ******************** **' as SPEC18_FLAG,
       '************ * ************************* * *************************' as SPEC19_FLAG,
       '**************** ** ************************* *************************' as SPEC20_FLAG,
       '************ *' as SPEC21_FLAG,
       '** * ***** ***** ****** ******' as SPEC22_FLAG,
       '* ** ************' as SPEC23_FLAG,
       '************ ************ ************ * * * * * ** ****' as SPEC24_FLAG,
       '************************* ************************* ************************* ************************* *************************' as SPEC25_FLAG,
       '************************* ************************* ************************* ************************* *************************' as SPEC26_FLAG,
       '** ********** **********' as SPEC27_FLAG,
       '** *** ***** ******************** ********** **********' as SPEC28_FLAG,
       '*' as SPEC29_FLAG,
        ot1.grade,
     
        RPAD(RTRIM(substr(ot1.maindefect,2,4)), 4, ' ')  as maindefect,
        RPAD(RTRIM(substr( ot1.maindefect_desc   ,0,20)),20,' ')  as maindefect_desc,
        '**' as SPEC30_FLAG
            
  from (
        
        select t1.step_id,
                
                t1.EQUIP_ID,
                t1.Item2 as OPERATOR_ID,
                t1.Item3 as SOURCE_CARRIER_ID,
                case
                  when t1.Date_Item1 is null then
                   t1.COMPONENT_START_TIME
                  else
                   t1.Date_Item1
                end STARTTIME,
                
                t1.COMPONENT_START_TIME as ENDTIME,
                t1.Batch_Id,
                t2.chip_id as CELL_CHIP_ID,
                t2.s as S,
                t2.g as G,
                t2.Item1 as DEFECT_CODE,
                t2.Item3 as DEFECT_JUDGE_CODE,
                t2.Item4 as DEFECT_NAME,
                t2.Item5 as DEFECT_COLOR,
                t2.Item51 as CT_DEFECT_CODE,
                t3.sites as sites,
                t4.item4 as grade,
                t4.item2 as maindefect,
                t6.item4 as maindefect_desc
          from lcdsys.cell_component_t t1, lcdsys.cell_defect_t t2 ,
          (

     select t.sites,
       t1.glassid
from td_wms_shipping_file t, mfginfo@eda2tclmes t1
where t.carton_id=t1.carton_id
and t.mes_ship_date=t1.mes_ship_date
and t.shippingdate>='{0}'
and t.shippingdate<'{1}'
and t.shippingfrom in ('T1')
and t.sites in (select distinct(t.shiipping_shop) from td_lcm_shipping_shop t)
and t1.gsflag='CHIP'



) t3 ,lcdsys.cell_chip_t t4, (

select t5.step_id,t5.component_start_time,t5.component_id,t5.defect_seq_no,t5.item4 from lcdsys.cell_defect_t t5
where t5.item7=1  and t5.step_id in ( '6615','6630','6810','6815','6830') 
) t6
         where t1.step_id in (   '6615','6630','6810','6815','6830' )
              
            
           
           and t1.component_id = t2.component_id
           and t1.step_id = t2.step_id
           and t1.component_start_time = t2.component_start_time
           and t1.component_id=t3.glassid
           and t2.component_id=t4.component_id
           and t2.component_start_time = t4.component_start_time
           and t2.step_id = t4.step_id
           and t2.step_id = t6.step_id
           and t2.component_start_time = t6.component_start_time
           and t1.component_id=t6.component_id
          
        ) ot1

) ot2
group by 'Cell', ot2.CELL_CHIP_ID,ot2.SOUTH_STEP,ot2.SOUTH_SHOP

) ot3


group by ot3.shop,ot3.south_step,ot3.south_shop

)bb2

where bb1.sites=bb2.south_shop
) bb3

group by bb3.shiftdate,bb3.sites,bb3.shop_desc,bb3.sub,bb3.cut,bb3.chip


union 

select substr('{0}',0,8),t.shiipping_shop,t.shop_desc,0,0,0,0,0,0,0,0 from td_lcm_shipping_shop t


)bb4

group by bb4.shiftdate,bb4.sites,bb4.shop_desc



union 

select 'Total','','',sum(bb5.SUB),SUM(bb5.CUT),sum(bb5.CHIP),sum(bb5.STEP_1930),sum(bb5.step_3600),sum(bb5.step_3650),sum(bb5.step_4600),sum(bb5.step_4650) from (

select bb4.shiftdate,bb4.sites,bb4.shop_desc,sum(bb4.SUB)as SUB,sum(bb4.CUT) as CUT,sum(bb4.chip) as CHIP,sum(bb4.step_1930) as step_1930,sum(bb4.step_3600) as step_3600,sum(bb4.step_3650) as step_3650,sum(bb4.step_4600) as step_4600,sum(bb4.step_4650) as step_4650 from 
(

select bb3.shiftdate,bb3.sites,bb3.shop_desc,bb3.sub,bb3.cut,bb3.chip,sum(step_1930) as step_1930,  
       sum(step_3600) as step_3600,  sum(step_3650) as step_3650, sum(step_4600) as step_4600,
       sum(step_4650) as step_4650



 from (

select bb1.*, case when bb2.north_step='1930' then bb2.counter else 0 end step_1930,
              case when bb2.north_step='3600' then bb2.counter else 0 end step_3600,
              case when bb2.north_step='3650' then bb2.counter else 0 end step_3650,
              case when bb2.north_step='4600' then bb2.counter else 0 end step_4600,
              case when bb2.north_step='4650' then bb2.counter else 0 end step_4650
from (

select ot5.shiftdate,ot5.sites,ot5.shop_desc,sum(case when ot5.gsflag='SUBSTRATE' then ot5.qty else 0 end ) as SUB ,

     sum(case when ot5.gsflag='CUT' then ot5.qty else 0 end ) as CUT
    ,sum(case when ot5.gsflag='CHIP' then ot5.qty else 0 end ) as CHIP

from (
select ot2.shiftdate,ot2.sites,ot4.shop_desc,ot2.gsflag,ot2.qty from (


select substr('{0}',0,8) as shiftdate,ot1.sites,ot1.gsflag ,count(ot1.sites) as qty from (

select t.sites,t1.gsflag,t1.glassid,count(t1.glassid)as counter

from td_wms_shipping_file t, mfginfo@eda2tclmes t1
where t.carton_id=t1.carton_id
and t.mes_ship_date=t1.mes_ship_date
and t.shippingdate>='{0}'
and t.shippingdate<'{1}'
and t.shippingfrom in ('T1')
and t.sites in (select distinct(t.shiipping_shop) from td_lcm_shipping_shop t)
group by t.sites,t1.gsflag,t1.glassid

) ot1
group by ot1.sites,ot1.gsflag
) ot2

,(
select * from td_lcm_shipping_shop t1
)ot4

where ot2.sites=ot4.shiipping_shop
)ot5
group by ot5.shiftdate,ot5.sites,ot5.shop_desc

) bb1,(

select 'Array' as North_shop,'1930' as north_step,ot7.south_SHOP,count(ot7.CHIP_ID) as counter from (

select ot6.CHIP_ID,ot6.south_shop,'SUB' as glass_type ,count(ot6.T_FLAG) as counter  from (


select 
       'T' as T_FLAG,
       'G' as G_FLAG,
       RPAD(RTRIM('G'), 2, ' ') as G_2_FLAG,
       '****' as spec_flag,
       '*********** ********* ************** ************** ****** ********* ****** ************** **************' as spec1_flag,
       '>No Data  Gate  Code Repr Deft Ty RT T1   T2 R Analysis Mod ADC    DefectPictureName       Code Repr Deft Ty RT DefectPictureName   ' as spec2_flag,
       '** * OTHERS       *      *********************** **** ************ ** ***********************' as spec3_flag,
       '@' as spec4_flag,
       ot3.step_id,
       'Y'||SUBSTR(ot2.CHIP_ID,7,20) as CHIP_ID ,
       ot3.GLASS_ID,
       RPAD(RTRIM(ot4.item3), 4, ' ') as lot_type,
       
       ot3.LOT_ID,
       RPAD(RTRIM(ot3.PRODUCT_ID), 8, ' ') as PRODUCT_ID,
     
       RPAD(RTRIM(ot3.EQUIP_ID), 9, ' ') as repr_EQUIP_ID,
       
       to_char(ot3.GLASS_START_TIME,'yyyyMMddHH24MISS') as REPR_STARTTIME,
       to_char(ot3.UPDATE_TIME,'yyyyMMddHH24MISS') as REPR_ENDTIME,
       RPAD(RTRIM(ot3.ITEM2), 8, ' ') as operator_id,
     
       case when RPAD(RTRIM(ot3.TEST_EQID), 9, ' ') is null then '*********'
       else
       RPAD(RTRIM(ot3.TEST_EQID), 9, ' ') end  as TEST_EQUIP_ID,
       
       case when to_char(ot3.TEST_STARTTIME,'yyyyMMddHH24MISS') is null then '**************'
       else
       to_char(ot3.TEST_STARTTIME,'yyyyMMddHH24MISS') end as TEST_STARTTIME,
       case when to_char(ot3.TEST_ENDTIME,'yyyyMMddHH24MISS') is null then '**************'
       else
       to_char(ot3.TEST_ENDTIME,'yyyyMMddHH24MISS')end as TEST_ENDTIME,
   
       LPAD(LTRIM(s), 5, '0') as s,
      
       
       LPAD(LTRIM(g), 5, '0') as g,
       substr(ot2.Item2,2,10) as RETYPE,
       
       ot2.item4 as defect_judge,
       ot2.item5 as defect_result,
       case when substr(ot2.item51,0,12) is null then '************'
       else
       RPAD(RTRIM(substr(ot2.item51,0,12)), 12, ' ')end as reason,
       case when ot2.item4='AR' and ot2.item5='2DP' then 'D '
            when    ot2.item4='AR' and ot2.item5='DP' then 'N '
            when     ot2.item4='AR' and ot2.item5='NP' then 'G '
            when     ot2.item4='NR' and ot2.item5='NP' then 'G '
            when     ot2.item4='F'  then 'B '
            when     ot2.item4='MD' then 'X '
            when    ot2.item4='CP' then 'L '
            when     ot2.item4='CR' then 'L '
            when     ot2.item4='NR' and ot2.item5='BP' then 'B '
            when     ot2.item4='NR' and ot2.item5='DP' then 'N '
                 else '**' end as RT,
       'TFTR2' as SOUTH_STEP,
       ot5.sites  as SOUTH_SHOP
                 
  from (select t.*,
               ot1.equip_id as test_eqid,
               ot1.glass_start_time as test_starttime,
               ot1.update_time as test_endtime
          from lcdsys.array_glass_t t,
               (
                
                select t.*
                  from lcdsys.array_glass_t t
                 where t.step_id in ('1920', 'S920', 'P920')
                
                ) ot1
         where t.step_id in ('1930', 'S930', 'P930')
          
           and t.glass_id = ot1.glass_id(+)
        
        ) ot3,
       lcdsys.array_defect_t ot2, lcdsys.array_lot_hst_t ot4,
  (

   select t.sites,
case when t1.gsflag='CHIP' then substr(t1.glassid,0,14)
     when t1.gsflag='CUT' then substr(t1.glassid,0,14)
       else t1.glassid end transfer_subid

from td_wms_shipping_file t, mfginfo@eda2tclmes t1
where t.carton_id=t1.carton_id
and t.mes_ship_date=t1.mes_ship_date
and t.shippingdate>='{0}'
and t.shippingdate<'{1}'
and t.shippingfrom in ('T1')
and t.sites in (select distinct(t.shiipping_shop) from td_lcm_shipping_shop t)
) ot5
       
 where ot3.step_id = ot2.step_id
   and ot3.glass_start_time = ot2.glass_start_time
   and ot3.glass_id = ot2.glass_id
   and ot2.ITEM2 <> '-'
   and ot3.lot_id=ot4.lot_id
   and ot3.step_id=ot4.step_id 
   and ot3.glass_id=ot5.transfer_subid
) ot6
 
 
group by ot6.CHIP_ID,ot6.south_shop,'1930','SUB'


)ot7

group by 'Array',ot7.south_SHOP,ot7.glass_type  




union 


select ot3.shop,ot3.south_step,ot3.south_shop,count(ot3.south_step) as counter from (

select 'Cell' as shop, ot2.CELL_CHIP_ID,ot2.SOUTH_STEP,ot2.SOUTH_SHOP,count(ot2.step_id) as counter  from (

select 
        'Y'||substr(ot1.CELL_CHIP_ID,7,20) as CELL_CHIP_ID ,
       ot1.step_id,
       ot1.equip_id,
      
      
       RPAD(RTRIM(ot1.SOURCE_CARRIER_ID), 8, ' ') as SOURCE_CARRIER_ID,
       '******' as spec_flag,
       case when ot1.step_id ='6615' then 'OY'
            when ot1.step_id ='6620' then 'OL'
            when ot1.step_id ='6630' then 'OY'
            when ot1.step_id ='6810' then 'OY'
            when ot1.step_id ='6815' then 'OY'
            when ot1.step_id ='6820' then 'OL'
            when ot1.step_id ='6830' then 'OY'
            else '**' end as OPERATION_MODE,
            
       '********' as SPEC1_FLAG,
       LPAD(LTRIM(round((case when (ot1.ENDTIME-ot1.STARTTIME)*24*60*60 >999 then 999
              else (ot1.ENDTIME-ot1.STARTTIME)*24*60*60 end),0)), 3, '0')  as TACT_TIME,
       '***' as SPEC2_FLAG,
       to_char(ot1.STARTTIME,'yyyyMMddHH24MISS') as STARTTIME,
        to_char(ot1.ENDTIME,'yyyyMMddHH24MISS') as ENDTIME,
        LPAD(LTRIM(ot1.OPERATOR_ID), 8, '0') as OPERATOR_ID,
       '**************** ** ****' as SPEC3_FLAG,
     
       '* **** **** ****' as SPEC4_FLAG,
       
       
       RPAD(RTRIM(ot1.Batch_Id), 20, ' ') as Batch_Id,
       '& **' as SPEC5_FLAG,
       '> * ******' as SPEC6_FLAG,
       '< **********' as SPEC7_FLAG,
       '{' as SPEC8_FLAG,
       '}' as SPEC9_FLAG,
       '[' as SPEC10_FLAG,
       ']' as SPEC11_FLAG,
       '%No Code Data  Gate  C RK Leve   ND  R PAT Defect Pattern Name  English Defect Name  CEDC Cell Re Eng Def Name JC BD Spare      Spare' as SPEC12_FLAG,
       substr(ot1.DEFECT_CODE,2,4) as DEFECT_CODE ,
        case when LPAD(LTRIM(ot1.S), 5, '0')='000-1' then '00000'
           else  LPAD(LTRIM(ot1.S), 5, '0') end as S,
       
       case when LPAD(LTRIM(ot1.G), 5, '0')='000-1' then '00000'
          else  LPAD(LTRIM(ot1.G), 5, '0') end as G,
        case when ot1.DEFECT_COLOR is null then '*'
             else ot1.DEFECT_COLOR end as DEFECT_COLOR ,
       '** ****** *** * *** ********************' as SPEC13_FLAG,
        RPAD(RTRIM(substr(ot1.DEFECT_NAME,0,20)), 20, ' ') as DEFECT_NAME,
       '**** ******************** **    ********** **********' as SPEC14_FLAG,
       '$N Cod STime Pattern Switch Name  Spare      Spare     ' as SPEC15_FLAG,
       '@' as SPEC16_FLAG,
      
       ot1.DEFECT_JUDGE_CODE,
       
      
       ot1.CT_DEFECT_CODE,
       case when ot1.step_id='6615' then '3600' 
            when ot1.step_id='6630' then '3650'  
            when ot1.step_id='6810' then '4600' 
            when ot1.step_id='6815' then '4600' 
            when ot1.step_id='6830' then '4650' 
          else 'NA' end  AS SOUTH_STEP,
       ot1.sites as SOUTH_SHOP,
       '**** **** ************ **** ***'as SPEC17_FLAG,
       '** **** ******************** **' as SPEC18_FLAG,
       '************ * ************************* * *************************' as SPEC19_FLAG,
       '**************** ** ************************* *************************' as SPEC20_FLAG,
       '************ *' as SPEC21_FLAG,
       '** * ***** ***** ****** ******' as SPEC22_FLAG,
       '* ** ************' as SPEC23_FLAG,
       '************ ************ ************ * * * * * ** ****' as SPEC24_FLAG,
       '************************* ************************* ************************* ************************* *************************' as SPEC25_FLAG,
       '************************* ************************* ************************* ************************* *************************' as SPEC26_FLAG,
       '** ********** **********' as SPEC27_FLAG,
       '** *** ***** ******************** ********** **********' as SPEC28_FLAG,
       '*' as SPEC29_FLAG,
        ot1.grade,
     
        RPAD(RTRIM(substr(ot1.maindefect,2,4)), 4, ' ')  as maindefect,
        RPAD(RTRIM(substr( ot1.maindefect_desc   ,0,20)),20,' ')  as maindefect_desc,
        '**' as SPEC30_FLAG
            
  from (
        
        select t1.step_id,
                
                t1.EQUIP_ID,
                t1.Item2 as OPERATOR_ID,
                t1.Item3 as SOURCE_CARRIER_ID,
                case
                  when t1.Date_Item1 is null then
                   t1.COMPONENT_START_TIME
                  else
                   t1.Date_Item1
                end STARTTIME,
                
                t1.COMPONENT_START_TIME as ENDTIME,
                t1.Batch_Id,
                t2.chip_id as CELL_CHIP_ID,
                t2.s as S,
                t2.g as G,
                t2.Item1 as DEFECT_CODE,
                t2.Item3 as DEFECT_JUDGE_CODE,
                t2.Item4 as DEFECT_NAME,
                t2.Item5 as DEFECT_COLOR,
                t2.Item51 as CT_DEFECT_CODE,
                t3.sites as sites,
                t4.item4 as grade,
                t4.item2 as maindefect,
                t6.item4 as maindefect_desc
          from lcdsys.cell_component_t t1, lcdsys.cell_defect_t t2 ,
          (

     select t.sites,
       t1.glassid
from td_wms_shipping_file t, mfginfo@eda2tclmes t1
where t.carton_id=t1.carton_id
and t.mes_ship_date=t1.mes_ship_date
and t.shippingdate>='{0}'
and t.shippingdate<'{1}'
and t.shippingfrom in ('T1')
and t.sites in (select distinct(t.shiipping_shop) from td_lcm_shipping_shop t)
and t1.gsflag='CHIP'



) t3 ,lcdsys.cell_chip_t t4, (

select t5.step_id,t5.component_start_time,t5.component_id,t5.defect_seq_no,t5.item4 from lcdsys.cell_defect_t t5
where t5.item7=1  and t5.step_id in ( '6615','6630','6810','6815','6830') 
) t6
         where t1.step_id in (   '6615','6630','6810','6815','6830' )
              
            
           
           and t1.component_id = t2.component_id
           and t1.step_id = t2.step_id
           and t1.component_start_time = t2.component_start_time
           and t1.component_id=t3.glassid
           and t2.component_id=t4.component_id
           and t2.component_start_time = t4.component_start_time
           and t2.step_id = t4.step_id
           and t2.step_id = t6.step_id
           and t2.component_start_time = t6.component_start_time
           and t1.component_id=t6.component_id
          
        ) ot1

) ot2
group by 'Cell', ot2.CELL_CHIP_ID,ot2.SOUTH_STEP,ot2.SOUTH_SHOP

) ot3


group by ot3.shop,ot3.south_step,ot3.south_shop

)bb2

where bb1.sites=bb2.south_shop
) bb3

group by bb3.shiftdate,bb3.sites,bb3.shop_desc,bb3.sub,bb3.cut,bb3.chip


union 

select substr('{0}',0,8),t.shiipping_shop,t.shop_desc,0,0,0,0,0,0,0,0 from td_lcm_shipping_shop t


)bb4

group by bb4.shiftdate,bb4.sites,bb4.shop_desc




)bb5




";
        //yesturday_shiftday = "2013120607";

        //today_shiftday = "2013120707";

        sql_temp = @" select  case when t.sites is null then '外賣' else t.sites end wms_site,
       case when t2.shiipping_shop is null then 'NA' else  t2.shiipping_shop end  ship_shop,
       case when t2.shop_desc is null then 'NA' else t2.shop_desc end shop_desc ,
       count(t.carton_id) box_cnt
       from td_wms_shipping_file t, td_lcm_shipping_shop t2
       where t.shippingdate >= '{0}'
        and t.shippingdate < '{1}'
        and t.sites = t2.trans_shop(+)
        group by t.sites, t2.shiipping_shop,t2.shop_desc
";


        sql_temp = string.Format(sql_temp, yesturday_shiftday, today_shiftday);
        //sql_temp = sql_temp.Replace("{0}", yesturday_shiftday).Replace("{1}", today_shiftday);

       

        //sql_temp = string.Format(sql_temp, yesturday_shiftday, today_shiftday);


        ds_temp5 = func.get_dataSet_access_oracle_client(sql_temp, conn);

        GridView1.DataSource = ds_temp5.Tables[0];
        GridView1.DataBind();

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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
