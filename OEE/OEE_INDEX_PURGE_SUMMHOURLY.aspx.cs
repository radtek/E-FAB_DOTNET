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

public partial class OEE_INDEX_PURGE_SUMMHOURLY : System.Web.UI.Page
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



    protected void Page_Load(object sender, EventArgs e)
    {

        #region Delete empaidxsummhourly

        delete_empaidxsummhourly();

        #endregion


        #region Delete empaidxsummdaily

        //delete_empaidxsummdaily();

        #endregion
        
        
       

        //javascript 語法填入 字串 
        string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_parent','');window.top.close(this);</script>";
        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmClose);



    }


    public void delete_empaidxsummhourly()
    {

        sql = " delete empaidxsummhourly t1                                          " +
"  where t1.cutoffkey < to_char(to_date((select substr(min(t.cutoffkey), 0, 8)  " +
"                                        from empaidxsummhourly t),    " +
"                                      'yyyyMMdd') + 1,                       " +
"                              'yyyyMMdd')                                    " +
"    and t1.cutoffkey <= to_char(sysdate - 90, 'yyyyMMddHH')                   ";

        func.get_sql_execute(sql, conn);
        func.write_log("oee_empaidxsummhourly_purge", Server.MapPath("..\\") + "\\LOG\\", "log");
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);


    }


    public void delete_empaidxsummdaily()
    {

        sql = " delete empaidxsummdaily t1                                          " +
"  where t1.cutoffkey < to_char(to_date((select substr(min(t.cutoffkey), 0, 8)  " +
"                                        from empaidxsummhourly t),    " +
"                                      'yyyyMMdd') + 1,                       " +
"                              'yyyyMMdd')                                    " +
"    and t1.cutoffkey <= to_char(sysdate - 90, 'yyyyMMddHH')                   ";

        func.get_sql_execute(sql, conn);
        func.write_log("oee_empaidxsummdaily_purge", Server.MapPath("..\\") + "\\LOG\\", "log");
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);


    }
}
