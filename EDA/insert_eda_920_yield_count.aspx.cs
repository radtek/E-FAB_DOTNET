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
using System.IO;

public partial class EDA_insert_eda_920_yield_count : System.Web.UI.Page
{
    private OracleConnection orcn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"]);

  
    
    IS.util.special sp = new IS.util.special();
    //file f = new file();

   
    DirectoryInfo di;//宣告目錄 
    FileInfo fi;//宣告檔案 


    //string conn = System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"];
    string conn = System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"];
    string conn_rpt = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_EDA"];

    
    string conn_cel = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];
    string conn_pds = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_PDS_OLE_STD"];
    string conn_oeegw1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_MIDGW1"];

    func fc = new func();

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

    string yesturday_shiftday = DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "07";
    string today_shiftday = DateTime.Now.AddDays(+0).ToString("yyyyMMdd") + "07";
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");

    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string last_hour = DateTime.Now.AddDays(-1 / 24).ToString("yyyyMMddHH");
    string last_twohour = DateTime.Now.AddDays(-2 / 24).ToString("yyyyMMddHH");
    string SaveLocation = "";
    Int32 counter_oscar = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        // get target lot
        sql_temp = @"

      select distinct(t.lot_id) as lot_id  from lcdsys.array_lot_hst_t t
	where t.lot_start_time between to_date('2015/04/01 00:00:00', 'YYYY/MM/DD HH24:MI:SS') and to_date('2015/05/01 00:00:00', 'YYYY/MM/DD HH24:MI:SS')
	and t.step_id in (select step_id from edarpt.array_step_group_t where step_group in ('Laser_Repair_01'))

      union all
select distinct(t.lot_id) as lot_id  from lcdsys.array_lot_hst_t@edarpt2hst t
	where t.lot_start_time between to_date('2015/04/01 00:00:00', 'YYYY/MM/DD HH24:MI:SS') and to_date('2015/05/01 00:00:00', 'YYYY/MM/DD HH24:MI:SS')
	and t.step_id in (select step_id from edarpt.array_step_group_t where step_group in ('Laser_Repair_01'))
 

";
        // union all 
        //select distinct(t.lot_id) as lot_id from lcdsys.array_lot_hst_t@edarpt2hst t
        //	where t.lot_start_time between to_date('2015/04/30 00:00:00', 'YYYY/MM/DD HH24:MI:SS') and to_date('2015/08/01 00:00:00', 'YYYY/MM/DD HH24:MI:SS')
        //	and t.step_id in (select step_id from edarpt.array_step_group_t where step_group in ('Laser_Repair_01')) 


        ds_temp = func.get_dataSet_access_oracle_client(sql_temp, conn);

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count-1; i++)
        {

            sql_temp2 = @"

        
        
       select  *  from (

       select t.product_id,t.lot_id,
       max(to_char(t.glass_start_time - 7 / 24, 'YYYY/MM/DD')) as inspection_datetime,
       sum(
           
           lcdapp.DEFECT_CODE_REPORT.get_real_test_count(t.glass_id)
           
           ) as nature_test_count,
       sum(t.nature_good_count) as nature_good_count

  from lcdsys.array_test_t t

 where t.lot_id in
      
       (
       
      '{0}'
    
  
  )
   and t.step_id in
      
       (
       
        (select step_id
           from edarpt.array_step_group_t
          where step_group in ('Laser_Repair_01'))
       
       )

 group by t.product_id,t.lot_id
 
 
 union
 
 
 select t.product_id,t.lot_id,
       max(to_char(t.glass_start_time - 7 / 24, 'YYYY/MM/DD')) as inspection_datetime,
       sum(
           
           eda_util.get_real_test_count_hst(t.glass_id)
           
           ) as nature_test_count,
       sum(t.nature_good_count) as nature_good_count

  from lcdsys.array_test_t@edarpt2hst t

 where t.lot_id in
      
       (
       
      '{0}'
    
  
  )
   and t.step_id in
      
       (
       
        (select step_id
           from edarpt.array_step_group_t
          where step_group in ('Laser_Repair_01'))
       
       )

       group by t.product_id,t.lot_id


)
            

";
            sql_temp2 = string.Format(sql_temp2, ds_temp.Tables[0].Rows[i]["lot_id"].ToString());

            ds_temp2 = func.get_dataSet_access_oracle_client(sql_temp2, conn);

            for (int j = 0; j <= ds_temp2.Tables[0].Rows.Count-1; j++)
            {

                sql_temp3 = @"

                     insert into eda_yield_920_tmp_his
  (product_id, lot_id, inspection_datetime, nature_test_count, nature_good_count)
values
  ('{0}', '{1}', '{2}', '{3}', '{4}')
";

                sql_temp3 = string.Format(sql_temp3, ds_temp2.Tables[0].Rows[j]["product_id"].ToString(), ds_temp2.Tables[0].Rows[j]["lot_id"].ToString(), ds_temp2.Tables[0].Rows[j]["inspection_datetime"].ToString(), ds_temp2.Tables[0].Rows[j]["nature_test_count"].ToString(), ds_temp2.Tables[0].Rows[j]["nature_good_count"].ToString());

                func.get_sql_execute(sql_temp3, conn_rpt);
            
            }



          
           
        }
       


    }
}
