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
using System.Xml;

public partial class EDA_PDS_DATA_RESTORE : System.Web.UI.Page
{
    //file f = new file();
  
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_EDAHIS_TEST"];
    

    //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_MIS"];
    func fc = new func();

    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_stm = "";
    string sql_oee_db_chk = "";

    DataSet ds_temp = new DataSet();

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string ThreeDayAgo = DateTime.Now.AddDays(-3).ToString("yyyyMMdd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");

    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string last_hour = DateTime.Now.AddDays(-1 / 24).ToString("yyyyMMddHH");
    string last_twohour = DateTime.Now.AddDays(-2 / 24).ToString("yyyyMMddHH");
    string last_Threehour = DateTime.Now.AddDays(-3 / 24).ToString("yyyyMMddHH");
    string SaveLocation = "";
    string tmp_glass_table = "TMP_PDS_GLASSID";//"TMP_PDS_GLASSID";
    string tmp_pds_table = "PDS_CELL_ESD";// "PDS_CELL_ESD";
    string step_id = "'6100'";// "'6100'";
    string parameter_name = "'ESD Data'";// "'ESD Data'";
    string parameter_collection = "";//  "'PDSG_1CPIL100_1CPIL140', 'PDSG_1CPIL100_1CPIL150', 'PDSG_1CPIL200_1CPIL240', 'PDSG_1CPIL200_1CPIL250'";

    string sql_config = "";
    string starttime = "";
    string endtime = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        INSERT_PDS_DATA();

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");


    }

    private void INSERT_PDS_DATA()
    {
        sql_config = @"select * from (
select t.* from pds_config t
where t.flag<>'N'
order by t.starttime
)ot1
where
rownum<=1


";
        ds_temp = func.get_dataSet_access(sql_config, conn);

        if (ds_temp.Tables[0].Rows.Count >= 0)
        {

            starttime = ds_temp.Tables[0].Rows[0]["starttime"].ToString();
            endtime = ds_temp.Tables[0].Rows[0]["endtime"].ToString();
            step_id = ds_temp.Tables[0].Rows[0]["step_id"].ToString();
            parameter_name = ds_temp.Tables[0].Rows[0]["parameter_name"].ToString();
            parameter_collection = ds_temp.Tables[0].Rows[0]["parameter_coll"].ToString();
            string tmpsql = @"
                           select distinct (glass_id) as glass_id
     from cell_component_hst_t
    where step_id in ({0})
        
      and component_start_time between
          to_date('{1}', 'YYYYMMDD HH24MISS') and
          to_date('{2}', 'YYYYMMDD HH24MISS')
         

        ";

            tmpsql = string.Format(tmpsql, step_id, starttime, endtime);

            create_table(tmp_glass_table, tmpsql);

            insert_des_table(tmp_pds_table, tmp_glass_table, step_id, starttime, endtime, parameter_collection, parameter_name);



            drop_table(tmp_glass_table);

            update_config(starttime);
        }
    }

    private void update_config(string starttime)
    {

        string sql_config = @"

update pds_config
   set 
       flag = 'N'
     
       
 where  starttime='{0}'";

        sql_config = string.Format(sql_config, starttime);
        func.get_sql_execute(sql_config, conn);

    }

    public void insert_des_table(string table_name,string glass_table,string stepid,string starttime,string endtime,string para_coll,string para_name)
    {
        string sql_des = @"
       insert into {0}
select t.* from cell_pds_compt_summary_t t, {1} t1
where 1=1
 and step_id in ({2})
 and t.glass_id=t1.glass_id  
 and component_start_time between to_date('{3}', 'YYYYMMDD HH24:MI:SS') 
                              and to_date('{4}', 'YYYYMMDD HH24:MI:SS') 
 and param_collection in ({5})  
 and param_name in ({6})


";
        sql_des = string.Format(sql_des, table_name, glass_table, stepid, starttime, endtime, para_coll, para_name);

        func.get_sql_execute(sql_des, conn);
    
    }


    public void create_table (string tmp_table,string tmpsql)
    {
      string sql_create=@" create table   {0} as 
    {1}
           ";

      sql_create = string.Format(sql_create, tmp_table, tmpsql);
      func.get_sql_execute(sql_create, conn);
      

    }


    public void drop_table(string table_name)

    {
        string sql_drop = @" drop TABLE {0}  ";
        sql_drop = string.Format(sql_drop, table_name);
        func.get_sql_execute(sql_drop, conn);

    
    }
}
