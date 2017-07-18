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
using System.Threading;

public partial class OEE_OEE_INDEX_PURGE_SUMMDAILY : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];

    string sql = "";

    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    Int32 count_num = 0;
    Int32 count_num1 = 0;
    Int32 count_num2 = 0;
    Int32 count_num3 = 0;
    string keep_day="180";
    string[] function_name ={ "delete_empaidxsummhourly", "delete_empaidxsummdaily", "delete_empastcount" };

    protected void Page_Load(object sender, EventArgs e)
    {

        

        #region Thread

        ThreadStart delete_empaidxsummhourly1 = new ThreadStart(delete_empaidxsummhourly);
        Thread thread1 = new Thread(delete_empaidxsummhourly1);
        thread1.Start();

        ThreadStart delete_empaidxsummdaily1 = new ThreadStart(delete_empaidxsummdaily);
        Thread thread2 = new Thread(delete_empaidxsummdaily1);
        thread2.Start();

        ThreadStart delete_empastcount1 = new ThreadStart(delete_empastcount);
        Thread thread3 = new Thread(delete_empastcount1);
        thread3.Start();




        ThreadStart Small_Black1 = new ThreadStart(Small_Black);
        Thread thread4 = new Thread(Small_Black1);
        thread4.Start();
        
        Thread.Sleep(600000);
        


        //thread2.Start();
        //thread3.Start();
        //thread1.Sleep(300000);
        //thread2.Sleep(300000);
        //thread3.Sleep(300000);

        //Thread.Sleep(30000); 
        
        #endregion
        
        



        #region Delete empaidxsummhourly

        //delete_empaidxsummhourly();

        #endregion


        #region Delete empaidxsummdaily

        //delete_empaidxsummdaily();

        #endregion

        #region Delete empastcount

        //delete_empastcount();

        #endregion


        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);


        //javascript 語法填入 字串 
        string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_parent','');window.top.close(this);</script>";
        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmClose);



    }

    public void Small_Black()
    {
        Int32 abc = 0;
        for (int i = 0; i <= 10; i++)
        {
            abc++;
        }





    } 

   



    public void delete_empaidxsummhourly()
    {

        sql = " delete empaidxsummhourly t1                                          " +
"  where t1.cutoffkey < to_char(to_date((select substr(min(t.cutoffkey), 0, 8)  " +
"                                        from empaidxsummhourly t),    " +
"                                      'yyyyMMdd') + 1,                       " +
"                              'yyyyMMdd')                                    " +
"    and t1.cutoffkey <= to_char(sysdate - "+keep_day+", 'yyyyMMddHH')                   ";

        func.get_sql_execute(sql, conn);
        func.write_log("oee_empaidxsummhourly_purge", Server.MapPath("..\\") + "\\LOG\\", "log");
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);


    }


    public void delete_empaidxsummdaily()
    {

        sql = " delete empaidxsummdaily t1                                          " +
"  where t1.cutoffkey < to_char(to_date((select substr(min(t.cutoffkey), 0, 8)  " +
"                                        from empaidxsummdaily t),    " +
"                                      'yyyyMMdd') + 1,                       " +
"                              'yyyyMMdd')                                    " +
"    and t1.cutoffkey <= to_char(sysdate - "+keep_day+", 'yyyyMMddHH')                   ";

        func.get_sql_execute(sql, conn);
        func.write_log("oee_empaidxsummdaily_purge", Server.MapPath("..\\") + "\\LOG\\", "log");
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);


    }


    public void delete_empastcount()
    {

        sql = " delete empastcount t1                                          " +
"  where t1.cutoffkey < to_char(to_date((select substr(min(t.cutoffkey), 0, 8)  " +
"                                        from empastcount t where t.cutoffcycle='D'),    " +
"                                      'yyyyMMdd') + 1,                       " +
"                              'yyyyMMdd')                                    " +
"    and t1.cutoffkey <= to_char(sysdate - "+keep_day+", 'yyyyMMddHH')  and t1.cutoffcycle='D'                  ";

        func.get_sql_execute(sql, conn);
        func.write_log("oee_empastcount_purge", Server.MapPath("..\\") + "\\LOG\\", "log");
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);


    }

}
