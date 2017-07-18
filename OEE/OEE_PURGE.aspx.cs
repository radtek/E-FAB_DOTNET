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

public partial class OEE_OEE_PURGE : System.Web.UI.Page
{
   
    //file f = new file();
    StreamWriter sw;
    FileInfo fi;
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];
   
    //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_MIS"];
    func fc = new func();

    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp1_1_1 = "";
    string sql_temp1_1_2 = "";
    string sql_temp1_2_1 = "";
    string sql_temp1_2_2 = "";
    string sql_temp1_3_1 = "";
    string sql_temp1_3_2 = "";
    string sql_temp1_4_1 = "";
    string sql_temp1_4_2 = "";

    string sql_temp2_1_1 = "";
    string sql_temp2_1_2 = "";
    string sql_temp2_1_3 = "";
   


    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    DataSet ds_temp4 = new DataSet();

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string SaveLocation = "";
    Int32 counter_oscar = 0;
   
    func.alarm_format alarm_format = new func.alarm_format();
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime nBeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
        int W=1; // Which Week?
        // First Week  
     
        DateTime nResultDate = nBeginDate.AddDays((W-1)*7+3);
        
        
        purge_future_data();

        //purge_partition();

        //if (nResultDate.ToString("yyyy/MM/dd").Equals(today))
        //   // if(1==1)
        //{
            
        //    purge_partition();
        //    add_partition();
        //}
       

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");


    }


    private void purge_future_data()

    {
        //DirectoryInfo di;
        //int now_hour = Convert.ToInt32(DateTime.Now.ToString("HH"));//抓取執行當下小時
        //int now_min = Convert.ToInt32(DateTime.Now.ToString("mm"));//抓取執行當下分鐘

        ////di = new DirectoryInfo(Server.MapPath("..\\") + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd")); //DateTime.Now.ToString("yyyyMMdd")		

        //FileInfo fi = new FileInfo(Server.MapPath("..\\") + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log");


        ////if (!di.Exists)
        ////{
        ////    di.Create();
        ////}

        ////如果檔案存在則開啟覆寫，如果不存在則建立新的檔案
        ////StreamWriter sw;
        //if (fi.Exists == true)
        //{
        //    sw = File.AppendText(Server.MapPath("..\\") + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log");
        //}
        //else
        //{
        //    sw = fi.CreateText();
        //}

        //sw.WriteLine("Create log file");
        //sw.WriteLine(DateTime.Now.ToString("u") + "T1OEE Purge Program Start");

        func.write_log("T1OEE Purge", Server.MapPath("..\\") + "\\LOG\\", "log");



        sql_temp = @"select t.* from empaglasshistory t
where t.enddatetime>to_char(sysdate+30,'yyyy-MM-dd HH24:MI:SS')";

        ds_temp = func.get_dataSet_access(sql_temp, conn);



        if (ds_temp.Tables[0].Rows.Count > 0)
        {
            //sw.WriteLine(DateTime.Now.ToString("u") + "Delete " + ds_temp.Tables[0].Rows.Count.ToString() + " Rows empaglasshistory");

            func.write_log(DateTime.Now.ToString("u") + "Delete " + ds_temp.Tables[0].Rows.Count.ToString() + " Rows empaglasshistory", Server.MapPath("..\\") + "\\LOG\\", "log");
            sql_temp = @"Delete  empaglasshistory t
where t.enddatetime>to_char(sysdate+30,'yyyy-MM-dd HH24:MI:SS')";

            
            func.get_sql_execute(sql_temp, conn);

        }


        sql_temp = @"select t.* from empaalarmhistory  t
where t.triggerdatetime>to_char(sysdate+30,'yyyy-MM-dd HH24:MI:SS')";

        ds_temp = func.get_dataSet_access(sql_temp, conn);



        if (ds_temp.Tables[0].Rows.Count > 0)
        {
            //sw.WriteLine(DateTime.Now.ToString("u") + "Delete " + ds_temp.Tables[0].Rows.Count.ToString() + " Rows empaalarmhistory");
            func.write_log(DateTime.Now.ToString("u") + "Delete " + ds_temp.Tables[0].Rows.Count.ToString() + " Rows empaalarmhistory", Server.MapPath("..\\") + "\\LOG\\", "log");
            sql_temp = @"Delete  empaalarmhistory t
where t.triggerdatetime>to_char(sysdate+30,'yyyy-MM-dd HH24:MI:SS')";

            func.get_sql_execute(sql_temp, conn);

        }



        sql_temp = @"select t.* from empaeventhistory   t
where t.triggerdatetime>to_char(sysdate+30,'yyyy-MM-dd HH24:MI:SS')";

        ds_temp = func.get_dataSet_access(sql_temp, conn);



        if (ds_temp.Tables[0].Rows.Count > 0)
        {
            //sw.WriteLine(DateTime.Now.ToString("u") + "Delete " + ds_temp.Tables[0].Rows.Count.ToString() + " Rows empaeventhistory");
            func.write_log(DateTime.Now.ToString("u") + "Delete " + ds_temp.Tables[0].Rows.Count.ToString() + " Rows empaeventhistory", Server.MapPath("..\\") + "\\LOG\\", "log");
            sql_temp = @"Delete  empaeventhistory t
where t.triggerdatetime>to_char(sysdate+30,'yyyy-MM-dd HH24:MI:SS')";

            func.get_sql_execute(sql_temp, conn);

        }




        sql_temp = @"select t.* from usr_porthistory    t
where t.neweventtime>to_char(sysdate+30,'yyyy-MM-dd HH24:MI:SS')";

        ds_temp = func.get_dataSet_access(sql_temp, conn);



        if (ds_temp.Tables[0].Rows.Count > 0)
        {
            //sw.WriteLine(DateTime.Now.ToString("u") + "Delete " + ds_temp.Tables[0].Rows.Count.ToString() + " Rows usr_porthistory");
            func.write_log(DateTime.Now.ToString("u") + "Delete " + ds_temp.Tables[0].Rows.Count.ToString() + " Rows usr_porthistory", Server.MapPath("..\\") + "\\LOG\\", "log");
            sql_temp = @"Delete  usr_porthistory t
where t.neweventtime>to_char(sysdate+30,'yyyy-MM-dd HH24:MI:SS')";

            func.get_sql_execute(sql_temp, conn);

        }

        //sw.WriteLine(DateTime.Now.ToString("u") + "T1OEE Purge Program End");
        //sw.WriteLine("");

        //sw.Close();

        // func.delete_log_dir(Server.MapPath(".") + "\\File\\", "*.*", -60);
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);
      
    }


    private void purge_partition()
    {

        sql_temp1_1_1 = @"
 select * from (
 
 
  select t.partition_name from user_ind_partitions t
 where t.tablespace_name like 'IXOEE_GLSHIS_IDX%'
       and t.partition_name like 'IXOEE_GLSHIS_2%' 
      
 order by t.partition_name
 
 ) ot1


       ";
        ds_temp=func.get_dataSet_access(sql_temp1_1_1,conn);
                
         sql_temp1_1_2 = @"

       ALTER TABLE EMPAMGR.EMPAGLASSHISTORY
DROP PARTITION {0}
UPDATE GLOBAL INDEXES

       ";

        sql_temp1_1_2=string.Format(sql_temp1_1_2,ds_temp.Tables[0].Rows[0][0].ToString());

        // oscar 20130311
         // func.get_sql_execute(sql_temp1_1_2,conn);   
          func.write_log(sql_temp1_1_2, Server.MapPath("..\\") + "\\LOG\\", "log");
        //func.write_log("EMPAGLASSHISTORY_drop_partition",Server.MapPath("..\\") + "\\LOG\\" , "log");
        ds_temp.Clear();

        sql_temp1_2_1 = @"
 select * from (
 select t.partition_name from user_ind_partitions t
 where t.tablespace_name like 'IXOEE_EVTHIS_IDX%'
       and t.partition_name like 'IXOEE_EVTHIS_2%' 
      
 order by t.partition_name
 ) ot1


";
         ds_temp=func.get_dataSet_access(sql_temp1_2_1,conn);

        sql_temp1_2_2 = @"
  ALTER TABLE EMPAMGR.EMPAEVENTHISTORY
DROP PARTITION {0}
UPDATE GLOBAL INDEXES
";

        sql_temp1_2_2=string.Format(sql_temp1_2_2,ds_temp.Tables[0].Rows[0][0].ToString());
        //func.get_sql_execute(sql_temp1_2_2,conn);
        func.write_log(sql_temp1_2_2, Server.MapPath("..\\") + "\\LOG\\", "log");
        //func.write_log("EMPAEVENTHISTORY_drop_partition",Server.MapPath("..\\") + "\\LOG\\" , "log");
        ds_temp.Clear();
        
        
        sql_temp1_3_1 = @"
 select * from (
 select t.partition_name from user_ind_partitions t
 where t.tablespace_name like 'IXOEE_ALMHIS_IDX%'
        and t.partition_name like 'IXOEE_ALMHIS_2%' 
      
 order by t.partition_name
 ) ot1

";
          ds_temp=func.get_dataSet_access(sql_temp1_3_1,conn);

        sql_temp1_3_2 = @"

     ALTER TABLE EMPAMGR.EMPAALARMHISTORY
DROP PARTITION  {0}
UPDATE GLOBAL INDEXES


";

        sql_temp1_3_2=string.Format(sql_temp1_3_2,ds_temp.Tables[0].Rows[0][0].ToString());

        //func.get_sql_execute(sql_temp1_3_2,conn);
        func.write_log(sql_temp1_3_2, Server.MapPath("..\\") + "\\LOG\\", "log");
        //func.write_log("EMPAALARMHISTORY_drop_partition",Server.MapPath("..\\") + "\\LOG\\" , "log");
        ds_temp.Clear();


        sql_temp1_4_1= @"
 select * from (
select t.partition_name from user_ind_partitions t
 where t.tablespace_name like 'IXOEE_PORHIS_IDX%'
       and t.partition_name like 'IXOEE_PORHIS_2%' 
     
 order by t.partition_name
 ) ot1

";
         
        ds_temp=func.get_dataSet_access(sql_temp1_4_1,conn);

      
        sql_temp1_4_2= @"
ALTER TABLE EMPAMGR.USR_PORTHISTORY
DROP PARTITION {0}
UPDATE GLOBAL INDEXES
";
         sql_temp1_4_2=string.Format(sql_temp1_4_2,ds_temp.Tables[0].Rows[0][0].ToString());
         
        //func.get_sql_execute(sql_temp1_4_2,conn);
         func.write_log(sql_temp1_4_2, Server.MapPath("..\\") + "\\LOG\\", "log");
        //func.write_log("USR_PORTHISTORY_drop_partition",Server.MapPath("..\\") + "\\LOG\\" , "log");
        ds_temp.Clear();


       //func.get_sql_execute(sql_temp1_2, conn);

       // func.write_log("EMPAEVENTHISTORY_drop_partition", Server.MapPath("..\\") + "\\LOG\\", "log");

       //func.get_sql_execute(sql_temp1_3, conn);

       // func.write_log("EMPAALARMHISTORY_drop_partition", Server.MapPath("..\\") + "\\LOG\\", "log");

       //func.get_sql_execute(sql_temp1_4, conn);

       // func.write_log("USR_PORTHISTORY_drop_partition", Server.MapPath("..\\") + "\\LOG\\", "log");

        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);
    
    }

    private void add_partition()
    {

        #region IXOEE_GLSHIS

        sql_temp2_1_1 = @" select  to_char(to_date(substr(max(t.partition_name),14,6),'yyyyMM')+32,'yyyy-MM')||'-01 00:00:00' as datetime1 from user_ind_partitions t
 where 
       t.tablespace_name like 'IXOEE_GLSHIS_IDX%'
      and t.partition_name like 'IXOEE_GLSHIS_2%' 
    
       and ROWNUM <50
 order by t.partition_name desc";

        ds_temp3 = func.get_dataSet_access(sql_temp2_1_1, conn);


        sql_temp2_1_2 = @"
       
select  'IXOEE_GLSHIS_'||to_char(to_date(substr(max(t.partition_name),14,6),'yyyyMM')+32,'yyyyMM')||'_DAT' as datetime2 from user_ind_partitions t
 where 
       t.tablespace_name like 'IXOEE_GLSHIS_IDX%'
      and t.partition_name like 'IXOEE_GLSHIS_2%' 
    
       and ROWNUM <50
 order by t.partition_name desc 

";

        ds_temp4 = func.get_dataSet_access(sql_temp2_1_2, conn);


        sql_temp2_1_3 = @"

        ALTER TABLE EMPAMGR.EMPAGLASSHISTORY
SPLIT PARTITION IXOEE_GLSHIS_SPLIT_DAT AT 
('{0}')
INTO (PARTITION {1}, 
     PARTITION IXOEE_GLSHIS_SPLIT_DAT)
UPDATE GLOBAL INDEXES


";
        sql_temp2_1_3 = string.Format(sql_temp2_1_3, ds_temp3.Tables[0].Rows[0][0].ToString(), ds_temp4.Tables[0].Rows[0][0].ToString());

        func.write_log(sql_temp2_1_3, Server.MapPath("..\\") + "\\LOG\\", "log");
        
        //func.get_sql_execute(sql_temp2_1_3, conn);

        //func.write_log("EMPAGLASSHISTORY_add_partition", Server.MapPath("..\\") + "\\LOG\\", "log");
        
        #endregion

        #region IXOEE_EVTHIS

        sql_temp2_1_1 = @" select  to_char(to_date(substr(max(t.partition_name),14,6),'yyyyMM')+62,'yyyy-MM')||'-01 00:00:00' as datetime1 from user_ind_partitions t
 where 
       t.tablespace_name like 'IXOEE_EVTHIS_IDX%'
      and t.partition_name like 'IXOEE_EVTHIS_2%' 
    
       and ROWNUM <50
 order by t.partition_name desc";

        ds_temp3 = func.get_dataSet_access(sql_temp2_1_1, conn);


        sql_temp2_1_2 = @"
       
select  'IXOEE_EVTHIS_'||to_char(to_date(substr(max(t.partition_name),14,6),'yyyyMM')+62,'yyyyMM')||'_DAT' as datetime2 from user_ind_partitions t
 where 
       t.tablespace_name like 'IXOEE_EVTHIS_IDX%'
      and t.partition_name like 'IXOEE_EVTHIS_2%' 
    
       and ROWNUM <50
 order by t.partition_name desc 

";

        ds_temp4 = func.get_dataSet_access(sql_temp2_1_2, conn);


        sql_temp2_1_3 = @"

        ALTER TABLE EMPAMGR.EMPAEVENTHISTORY
SPLIT PARTITION IXOEE_EVTHIS_SPLIT_DAT AT 
('{0}')
INTO (PARTITION {1}, 
     PARTITION IXOEE_EVTHIS_SPLIT_DAT)
UPDATE GLOBAL INDEXES


";
        sql_temp2_1_3 = string.Format(sql_temp2_1_3, ds_temp3.Tables[0].Rows[0][0].ToString(), ds_temp4.Tables[0].Rows[0][0].ToString());
        
        func.write_log(sql_temp2_1_3, Server.MapPath("..\\") + "\\LOG\\", "log");
       // func.get_sql_execute(sql_temp2_1_3, conn);
       //func.write_log("EMPAEVENTHISTORY_add_partition", Server.MapPath("..\\") + "\\LOG\\", "log");

        #endregion


        #region IXOEE_ALMHIS

        sql_temp2_1_1 = @" select  to_char(to_date(substr(max(t.partition_name),14,6),'yyyyMM')+62,'yyyy-MM')||'-01 00:00:00' as datetime1 from user_ind_partitions t
 where 
       t.tablespace_name like 'IXOEE_ALMHIS_IDX%'
      and t.partition_name like 'IXOEE_ALMHIS_2%' 
    
       and ROWNUM <50
 order by t.partition_name desc";

        ds_temp3 = func.get_dataSet_access(sql_temp2_1_1, conn);


        sql_temp2_1_2 = @"
       
select  'IXOEE_ALMHIS_'||to_char(to_date(substr(max(t.partition_name),14,6),'yyyyMM')+32,'yyyyMM')||'_DAT' as datetime2 from user_ind_partitions t
 where 
       t.tablespace_name like 'IXOEE_ALMHIS_IDX%'
      and t.partition_name like 'IXOEE_ALMHIS_2%' 
    
       and ROWNUM <50
 order by t.partition_name desc 

";

        ds_temp4 = func.get_dataSet_access(sql_temp2_1_2, conn);


        sql_temp2_1_3 = @"

        ALTER TABLE EMPAMGR.EMPAALARMHISTORY
SPLIT PARTITION IXOEE_ALMHIS_SPLIT_DAT AT 
('{0}')
INTO (PARTITION {1}, 
     PARTITION IXOEE_ALMHIS_SPLIT_DAT)
UPDATE GLOBAL INDEXES


";
        sql_temp2_1_3 = string.Format(sql_temp2_1_3, ds_temp3.Tables[0].Rows[0][0].ToString(), ds_temp4.Tables[0].Rows[0][0].ToString());
       
        func.write_log(sql_temp2_1_3, Server.MapPath("..\\") + "\\LOG\\", "log");

        //func.get_sql_execute(sql_temp2_1_3, conn);
        //func.write_log("EMPAALARMHISTORY_add_partition", Server.MapPath("..\\") + "\\LOG\\", "log");

        #endregion



        #region IXOEE_PORHIS

        sql_temp2_1_1 = @" select  to_char(to_date(substr(max(t.partition_name),14,6),'yyyyMM')+62,'yyyy-MM')||'-01 00:00:00' as datetime1 from user_ind_partitions t
 where 
       t.tablespace_name like 'IXOEE_PORHIS_IDX%'
      and t.partition_name like 'IXOEE_PORHIS_2%' 
    
       and ROWNUM <50
 order by t.partition_name desc";

        ds_temp3 = func.get_dataSet_access(sql_temp2_1_1, conn);


        sql_temp2_1_2 = @"
       
select  'IXOEE_PORHIS_'||to_char(to_date(substr(max(t.partition_name),14,6),'yyyyMM')+62,'yyyyMM')||'_DAT' as datetime2 from user_ind_partitions t
 where 
       t.tablespace_name like 'IXOEE_PORHIS_IDX%'
      and t.partition_name like 'IXOEE_PORHIS_2%' 
    
       and ROWNUM <50
 order by t.partition_name desc 

";

        ds_temp4 = func.get_dataSet_access(sql_temp2_1_2, conn);


        sql_temp2_1_3 = @"

        ALTER TABLE EMPAMGR.USR_PORTHISTORY
SPLIT PARTITION IXOEE_PORHIS_SPLIT_DAT AT 
('{0}')
INTO (PARTITION {1}, 
     PARTITION IXOEE_PORHIS_SPLIT_DAT)
UPDATE GLOBAL INDEXES


";
        sql_temp2_1_3 = string.Format(sql_temp2_1_3, ds_temp3.Tables[0].Rows[0][0].ToString(), ds_temp4.Tables[0].Rows[0][0].ToString());

        func.write_log(sql_temp2_1_3, Server.MapPath("..\\") + "\\LOG\\", "log");

        //func.get_sql_execute(sql_temp2_1_3, conn);
        //func.write_log("USR_PORTHISTORY_add_partition", Server.MapPath("..\\") + "\\LOG\\", "log");

        #endregion




       
       


      
    
    
    }

}
