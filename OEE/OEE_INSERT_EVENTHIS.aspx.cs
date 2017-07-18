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

public partial class OEE_OEE_INSERT_EVENTHIS : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_RE_OLE"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_OLE"];


    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string sql_temp4 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    DataSet ds_temp4 = new DataSet();
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_minus7 = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");

    ArrayList arlist_temp1 = new ArrayList();

    
    protected void Page_Load(object sender, EventArgs e)
    {
        //2013-03-01 04:00:00.000
        //2013-03-01 05:00:00.000

        if (!IsPostBack)
        {
            TextBox1.Text = "2013-03-01 04:00:00.000";
            TextBox2.Text = "2013-03-01 05:00:00.000";

            Button1_Click(null, null);
      

        }



    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        sql_temp3 = @"

select t.* from sche_event_oscar t
where t.flag='N'
order by t.starttime



";
        ds_temp3 = func.get_dataSet_access(sql_temp3, conn1);


        if (ds_temp3.Tables[0].Rows.Count>0)
        {

            TextBox1.Text = ds_temp3.Tables[0].Rows[0][0].ToString();
            TextBox2.Text = ds_temp3.Tables[0].Rows[0][1].ToString();
            
            
            sql_temp = @"


   select t.*  from empaeventhistory t
where t.triggerdatetime>='{0}'
  and t.triggerdatetime<'{1}'

";
            sql_temp = string.Format(sql_temp, TextBox1.Text, TextBox2.Text);

            ds_temp = func.get_dataSet_access(sql_temp, conn);


            for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
            {

                sql_temp1 = @"

        insert into empaeventhistory
  (line, equipmentid, equipmentmodel, motherequipmentid, moduleid, messagekind, messageid, messagecode, messagename, triggerdatetime, oldstateid, newstateid, semioldstateid, seminewstateid, runratio, idleratio, engratio, setupratio, pmratio, pmmqcratio, eqdratio, alarmratio, dmqcratio, nstratio, lotid, waferid, recipeid, foupid, slotid, batchid, equipmentmode, deviceid, operationid, inputuserid, inputsource, comments, epttrigger, codedesc, reasoncode, processgroup, mescomments, isshorttermidle, stihcomments)
values
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', '{30}', '{31}', '{32}', '{33}', '{34}', '{35}', '{36}', '{37}', '{38}', '{39}', '{40}', '{41}', '{42}')


";
                sql_temp1 = string.Format(sql_temp1, ds_temp.Tables[0].Rows[i]["line"].ToString(), ds_temp.Tables[0].Rows[i]["equipmentid"].ToString(), ds_temp.Tables[0].Rows[i]["equipmentmodel"].ToString(), ds_temp.Tables[0].Rows[i]["motherequipmentid"].ToString(), ds_temp.Tables[0].Rows[i]["moduleid"].ToString(), ds_temp.Tables[0].Rows[i]["messagekind"].ToString(), ds_temp.Tables[0].Rows[i]["messageid"].ToString(), ds_temp.Tables[0].Rows[i]["messagecode"].ToString(), ds_temp.Tables[0].Rows[i]["messagename"].ToString(), ds_temp.Tables[0].Rows[i]["triggerdatetime"].ToString(), ds_temp.Tables[0].Rows[i]["oldstateid"].ToString(), ds_temp.Tables[0].Rows[i]["newstateid"].ToString(), ds_temp.Tables[0].Rows[i]["semioldstateid"].ToString(), ds_temp.Tables[0].Rows[i]["seminewstateid"].ToString(), ds_temp.Tables[0].Rows[i]["runratio"].ToString(), ds_temp.Tables[0].Rows[i]["idleratio"].ToString(), ds_temp.Tables[0].Rows[i]["engratio"].ToString(), ds_temp.Tables[0].Rows[i]["setupratio"].ToString(), ds_temp.Tables[0].Rows[i]["pmratio"].ToString(), ds_temp.Tables[0].Rows[i]["pmmqcratio"].ToString(), ds_temp.Tables[0].Rows[i]["eqdratio"].ToString(), ds_temp.Tables[0].Rows[i]["alarmratio"].ToString(), ds_temp.Tables[0].Rows[i]["dmqcratio"].ToString(), ds_temp.Tables[0].Rows[i]["nstratio"].ToString(), ds_temp.Tables[0].Rows[i]["lotid"].ToString(), ds_temp.Tables[0].Rows[i]["waferid"].ToString(), ds_temp.Tables[0].Rows[i]["recipeid"].ToString(), ds_temp.Tables[0].Rows[i]["foupid"].ToString(), ds_temp.Tables[0].Rows[i]["slotid"].ToString(), ds_temp.Tables[0].Rows[i]["batchid"].ToString(), ds_temp.Tables[0].Rows[i]["equipmentmode"].ToString(), ds_temp.Tables[0].Rows[i]["deviceid"].ToString(), ds_temp.Tables[0].Rows[i]["operationid"].ToString(), ds_temp.Tables[0].Rows[i]["inputuserid"].ToString(), ds_temp.Tables[0].Rows[i]["inputsource"].ToString(), ds_temp.Tables[0].Rows[i]["comments"].ToString(), ds_temp.Tables[0].Rows[i]["epttrigger"].ToString(), ds_temp.Tables[0].Rows[i]["codedesc"].ToString(), ds_temp.Tables[0].Rows[i]["reasoncode"].ToString(), ds_temp.Tables[0].Rows[i]["processgroup"].ToString(), ds_temp.Tables[0].Rows[i]["mescomments"].ToString(), ds_temp.Tables[0].Rows[i]["isshorttermidle"].ToString(), ds_temp.Tables[0].Rows[i]["stihcomments"].ToString());

                func.get_sql_execute(sql_temp1, conn1);
            }


            sql_temp2 = @"
    select count(t.line)  from empaeventhistory t
where t.triggerdatetime>='{0}'
  and t.triggerdatetime<'{1}'

";
            sql_temp2 = string.Format(sql_temp2, TextBox1.Text, TextBox2.Text);

            Label1.Text = func.get_dataSet_access(sql_temp2, conn).Tables[0].Rows[0][0].ToString();


            sql_temp3 = @"
    select count(t.line)  from empaeventhistory t
where t.triggerdatetime>='{0}'
  and t.triggerdatetime<'{1}'

";
            sql_temp3 = string.Format(sql_temp3, TextBox1.Text, TextBox2.Text);

            Label2.Text = func.get_dataSet_access(sql_temp3, conn1).Tables[0].Rows[0][0].ToString();


            sql_temp4 = @"
   
update sche_event_oscar

   set    flag = 'Y', 
       dttm=sysdate,
       SRC_COUNTER='{2}',
       DES_COUNTER='{3}'
 where starttime='{0}' and  endtime='{1}'
 

";
            sql_temp4 = string.Format(sql_temp4, TextBox1.Text, TextBox2.Text, Label1.Text, Label2.Text);
            func.get_sql_execute(sql_temp4, conn1);

         
        }



        //javascript 語法填入 字串 
        string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_self');window.top.close(this);</script>";
        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmClose);
         
        
        
     


    }
}
