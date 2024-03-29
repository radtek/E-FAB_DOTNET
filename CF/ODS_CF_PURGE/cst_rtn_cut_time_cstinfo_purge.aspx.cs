﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CF_ODS_CF_PURGE_cst_rtn_cut_time_cstinfo_purge : System.Web.UI.Page
{

    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CFT"];
  
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

        sql = " delete cst_rtn_cut_time_cstinfo t1                                          " +
"  where t1.cut_time < to_char(to_date((select substr(min(t.cut_time), 0, 8)  " +
"                                        from cst_rtn_cut_time_cstinfo t),    " +
"                                      'yyyyMMdd') + 1,                       " +
"                              'yyyyMMdd')                                    " +
"    and t1.cut_time <= to_char(sysdate - 60, 'yyyyMMddHH')                   ";

        func.get_sql_execute(sql, conn);

        func.write_log("cst_rtn_cut_time_cstinfo_purge", Server.MapPath("..\\") + "..\\LOG\\", "log");
        func.delete_log_file(Server.MapPath("..\\") + "..\\LOG\\", "*.log", -60);

        //javascript 語法填入 字串 
        string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_parent','');window.top.close(this);</script>";
        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmClose);

       

    }
}
