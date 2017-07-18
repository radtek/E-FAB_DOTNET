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

public partial class OEE_T1OEE_Eventhistory_chk : System.Web.UI.Page
{

    IS.util.special sp = new IS.util.special();
    //file f = new file();
    StreamWriter sw;
    FileInfo fi;
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];
    string conn_cel = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];
    string conn_pds = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_PDS_OLE_STD"];

    //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_MIS"];
    func fc = new func();

    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string sql_stm = "";

    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();


    string today = DateTime.Now.AddDays(+0).ToString("yyyy-MM-dd");

    string yesturday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

    string now_hour_oee_time_format = DateTime.Now.AddDays(1 / 24).ToString("yyyy-MM-dd HH");
    string last_hour_oee_time_format= DateTime.Now.AddDays(-1/24).ToString("yyyy-MM-dd HH");


    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");

    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string last_hour = DateTime.Now.AddDays(-1 / 24).ToString("yyyyMMddHH");
    string last_twohour = DateTime.Now.AddDays(-2 / 24).ToString("yyyyMMddHH");
    string SaveLocation = "";
    Int32 counter_oscar = 0;
    func xmlw = new func();
    func.alarm_format alarm_format = new func.alarm_format();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        sql_temp3 = @"

  select t.line,t.equipmentid,count(t.line) from equipment t
where t.equipmentid like '%00'
and t.line in ('T1ARRAY','T0ARRAY','T1CF','T0CELL','T1CELL') 
--and t.equipmentid like '1APHT%'
group by t.line,t.equipmentid

";
        ds_temp3 = func.get_dataSet_access(sql_temp3, conn);

        for (int j = 0; j <= ds_temp3.Tables[0].Rows.Count - 1; j++)
        {

            Event_chk(ds_temp3.Tables[0].Rows[j]["line"].ToString(), ds_temp3.Tables[0].Rows[j]["equipmentid"].ToString());


        }


        




        //javascript 語法填入 字串 
        string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_self');window.top.close(this);</script>";
        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmClose);
    }

    public void Event_chk(string line,string equipmentid)
    {

      


        
        
        
        sql_temp = @"
        select t.line,t.equipmentid,t.messagename,t.triggerdatetime,t.semioldstateid,t.seminewstateid,t.inputuserid,t.inputsource,t.comments from empaeventhistory t
where t.line='{0}' and t.equipmentid='{1}' 
and t.triggerdatetime>='{2}'
and t.triggerdatetime<'{3}'
and t.semioldstateid<>t.seminewstateid 
order by t.triggerdatetime 

";

        // Daily Chk
        sql_temp = string.Format(sql_temp, line, equipmentid, yesturday + " 07:00", today + " 07:00");

        //hourly Chk


        //sql_temp = string.Format(sql_temp, line, equipmentid, last_hour_oee_time_format + ":00", now_hour_oee_time_format + ":00");



        ds_temp = func.get_dataSet_access(sql_temp, conn);

        string pre_new_state = "";
        string old_state = "";
        string new_state = "";
        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count-1; i++)
        {

            if (i == 0)
            {
                pre_new_state = ds_temp.Tables[0].Rows[i]["SEMINEWSTATEID"].ToString();

                old_state = ds_temp.Tables[0].Rows[i]["SEMIOLDSTATEID"].ToString();

                new_state = ds_temp.Tables[0].Rows[i]["SEMINEWSTATEID"].ToString();

            }
            else
            {

                if (!ds_temp.Tables[0].Rows[i]["SEMIOLDSTATEID"].ToString().Equals(pre_new_state))
                {

                    sql_temp1 = @"

insert into empaeventhistory_chk
  (line, equipmentid, messagename, triggerdatetime, semioldstateid, seminewstateid, inputuserid, inputsource, comments, flag,dttm)
values
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}',{10})
";


                    sql_temp1 = string.Format(sql_temp1, ds_temp.Tables[0].Rows[i]["LINE"].ToString(), ds_temp.Tables[0].Rows[i]["EQUIPMENTID"].ToString(), ds_temp.Tables[0].Rows[i]["MESSAGENAME"].ToString(), ds_temp.Tables[0].Rows[i]["TRIGGERDATETIME"].ToString(), ds_temp.Tables[0].Rows[i]["SEMIOLDSTATEID"].ToString(), ds_temp.Tables[0].Rows[i]["SEMINEWSTATEID"].ToString(), ds_temp.Tables[0].Rows[i]["INPUTUSERID"].ToString(), ds_temp.Tables[0].Rows[i]["INPUTSOURCE"].ToString(), ds_temp.Tables[0].Rows[i]["COMMENTS"].ToString(),"N","sysdate");

                    func.get_sql_execute(sql_temp1,conn);




                }

                pre_new_state = ds_temp.Tables[0].Rows[i]["SEMINEWSTATEID"].ToString();

                old_state = ds_temp.Tables[0].Rows[i]["SEMIOLDSTATEID"].ToString();

                new_state = ds_temp.Tables[0].Rows[i]["SEMINEWSTATEID"].ToString();
            
            }




        }



    }
}
