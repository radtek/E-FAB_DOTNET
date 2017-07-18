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

public partial class CF_CF_FULL_AUTO_CF_fwfullautomonitor_loader : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CFT_STG"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CFT"];

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string yesturday = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

    string today_minus17 = DateTime.Now.AddDays(-17).ToString("yyyy/MM/dd");

    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm");
    string sql_temp = "";
    string sql_temp1 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet(); 

    protected void Page_Load(object sender, EventArgs e)
    {

        loader_fwfullautomonitor();

        loader_FWEQ2EQHISTORY();

        //javascript 語法填入 字串 
        string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_parent','');window.top.close(this);</script>";
        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmClose);

    }

    private void loader_fwfullautomonitor()
    {
        sql_temp = "select * from cft.fwfullautomonitor@repl2mes_cft where txntimestamp between '" + yesturday.Replace("/", "") + " 070000000' AND '" + today.Replace("/", "") + " 070000000'";

        ds_temp = func.get_dataSet_access(sql_temp, conn);

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
        {

            sql_temp1 = " insert into fwfullautomonitor                                                                                    " +
  "   (batchid, wipid, lottype, eqp, handle, txntimestamp, briefdescription, activity, userid)                       " +
  " values                                                                                                           " +
  "   ('" + ds_temp.Tables[0].Rows[i]["batchid"] + "', '" + ds_temp.Tables[0].Rows[i]["wipid"] + "', '" + ds_temp.Tables[0].Rows[i]["lottype"] + "', '" + ds_temp.Tables[0].Rows[i]["eqp"] + "', '" + ds_temp.Tables[0].Rows[i]["handle"] + "', '" + ds_temp.Tables[0].Rows[i]["txntimestamp"] + "', '" + ds_temp.Tables[0].Rows[i]["briefdescription"] + "', '" + ds_temp.Tables[0].Rows[i]["activity"] + "', '" + ds_temp.Tables[0].Rows[i]["userid"] + "')     ";

            func.get_sql_execute(sql_temp1, conn1);

        }

      
    }

    private void loader_FWEQ2EQHISTORY()
    {
        sql_temp = @" select t.* from CFT.FWEQ2EQHISTORY@repl2mes_cft  t
 WHERE txntimestamp between '" + yesturday.Replace("/", "") + " 070000000' AND '" + today.Replace("/", "") + " 070000000'";

        ds_temp = func.get_dataSet_access(sql_temp, conn);

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
        {
            sql_temp1 = @"insert into cf_fweq2eqhistory
  (tid, action, fromnode, fromport, tonode, toport, carrierid, carriertype, carrierstatus, lotid, userid, transfertype, location, lottype, txntimestamp)
values
  ('" + ds_temp.Tables[0].Rows[i]["tid"] + "', '" + ds_temp.Tables[0].Rows[i]["action"] + "', '" + ds_temp.Tables[0].Rows[i]["fromnode"] + "', '" + ds_temp.Tables[0].Rows[i]["fromport"] + "', '" + ds_temp.Tables[0].Rows[i]["tonode"] + "', '" + ds_temp.Tables[0].Rows[i]["toport"] + "', '" + ds_temp.Tables[0].Rows[i]["tonode"] + "', '" + ds_temp.Tables[0].Rows[i]["carrierid"] + "', '" + ds_temp.Tables[0].Rows[i]["carriertype"] + "', '" + ds_temp.Tables[0].Rows[i]["lotid"] + "', '" + ds_temp.Tables[0].Rows[i]["userid"] + "', '" + ds_temp.Tables[0].Rows[i]["transfertype"] + "', '" + ds_temp.Tables[0].Rows[i]["location"] + "', '" + ds_temp.Tables[0].Rows[i]["lottype"] + "', '" + ds_temp.Tables[0].Rows[i]["txntimestamp"] + "')";

            func.get_sql_execute(sql_temp1, conn1);

        }


    }
}
