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

public partial class OEE_oee_data_check : System.Web.UI.Page
{
    //file f = new file();
    StreamWriter sw;
    FileInfo fi;
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1_LH"];

    //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_MIS"];
   

    string sql_temp = "";
    string sql_temp1 = "";


    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
   

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string before30_detail = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd HH:mm:ss");
    string today_detail_1 = DateTime.Now.AddDays(+0).ToString("yyyy-MM-dd HH:mm:ss");
    string SaveLocation = "";
    Int32 counter_oscar = 0;

   
    protected void Page_Load(object sender, EventArgs e)
    {

        sql_temp1= @"
        select distinct(t.equipmentid) from oeemgr.equipment t
where t.line='{0}' and t.moduletype='{1}'

       ";

        sql_temp1 = string.Format(sql_temp1, "CTP", "MAIN");
        
        ds_temp1=func.get_dataSet_access(sql_temp1,conn);



              for (int i = 0; i <= ds_temp1.Tables[0].Rows.Count-1; i++)
			{

                   sql_temp = @"
select tt.line,tt.equipmentid,tt.triggerdatetime,tt.oldstateid,tt.newstateid,tt.semioldstateid,tt.seminewstateid from oeemgr.EMPAEVENTHISTORY tt
where tt.equipmentid='{0}'
and tt.line='{1}'
AND tt.TRIGGERDATETIME >= '{2}'
AND tt.TRIGGERDATETIME <= '{3}'

";
                   sql_temp = string.Format(sql_temp, ds_temp1.Tables[0].Rows[i][0].ToString(),"CTP", before30_detail, today_detail_1);

                   ds_temp = func.get_dataSet_access(sql_temp, conn);

                   string line = ""; 
                  string equipmentid="";
                  string semioldstateid = "";
                   string seminewstateid = "";
                   string oldstateid = "";
                   string newstateid = "";
                   string triggerdatetime = "";

                   for (int j = 0; j <= ds_temp.Tables[0].Rows.Count-1; j++)
                   {

                       if (j == 0)
                       {
                          
                             line =  ds_temp.Tables[0].Rows[j]["line"].ToString();
                             equipmentid= ds_temp.Tables[0].Rows[j]["equipmentid"].ToString();
                           
                           triggerdatetime = ds_temp.Tables[0].Rows[j]["triggerdatetime"].ToString();
                           oldstateid = ds_temp.Tables[0].Rows[j]["oldstateid"].ToString();
                           newstateid = ds_temp.Tables[0].Rows[j]["newstateid"].ToString();
                           semioldstateid = ds_temp.Tables[0].Rows[j]["semioldstateid"].ToString();
                           seminewstateid = ds_temp.Tables[0].Rows[j]["seminewstateid"].ToString();
                           

                       }
                       else

                       {
                           if (!ds_temp.Tables[0].Rows[j]["semioldstateid"].ToString().Equals(seminewstateid))
                           {
                               Response.Write(ds_temp.Tables[0].Rows[j]["line"].ToString() + " " + ds_temp.Tables[0].Rows[j]["equipmentid"].ToString() + " " + ds_temp.Tables[0].Rows[j]["triggerdatetime"].ToString() + " " + ds_temp.Tables[0].Rows[j]["semioldstateid"].ToString()+" "+ds_temp.Tables[0].Rows[j]["seminewstateid"].ToString()+"<BR>");

                               func.write_log(ds_temp.Tables[0].Rows[j]["line"].ToString() + " " + ds_temp.Tables[0].Rows[j]["equipmentid"].ToString() + " " + ds_temp.Tables[0].Rows[j]["triggerdatetime"].ToString() + " " + ds_temp.Tables[0].Rows[j]["semioldstateid"].ToString() + " " + ds_temp.Tables[0].Rows[j]["seminewstateid"].ToString(), Server.MapPath(".") + "\\log\\", "log");

                               #region Fixed Data
                               string fix_state = @"update oeemgr.empaeventhistory
   set 
       semioldstateid = '{0}',
       oldstateid='{1}'
 where line = '{2}'
   and equipmentid = '{3}'
   and triggerdatetime = '{4}'";


                               fix_state = string.Format(fix_state, seminewstateid, newstateid, ds_temp.Tables[0].Rows[j]["line"].ToString(), ds_temp.Tables[0].Rows[j]["equipmentid"].ToString(), ds_temp.Tables[0].Rows[j]["triggerdatetime"].ToString());

                               func.get_sql_execute(fix_state, conn);


                               #endregion
                             

                           }

                           triggerdatetime = ds_temp.Tables[0].Rows[j]["triggerdatetime"].ToString();
                           oldstateid = ds_temp.Tables[0].Rows[j]["oldstateid"].ToString();
                           newstateid = ds_temp.Tables[0].Rows[j]["newstateid"].ToString();
                           semioldstateid = ds_temp.Tables[0].Rows[j]["semioldstateid"].ToString();
                           seminewstateid = ds_temp.Tables[0].Rows[j]["seminewstateid"].ToString();

                       
                       }



                       
                   }




			 
			}


            func.write_log("OEELH_fixed_data_Daily", Server.MapPath("../") + "\\LOG\\", "log");
            func.delete_log_file(Server.MapPath(".") + "\\LOG\\", "*.log", -30); 

            func.delete_log_file(Server.MapPath("../") + "\\LOG\\", "*.log", -30);

            //javascript 語法填入 字串 
            string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_self');window.top.close(this);</script>";
            //呼叫 javascript 
            this.Page.RegisterStartupScript("", frmClose);
       


    }
}
