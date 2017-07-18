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
using System.Net.Mail;

public partial class MIS_EMPO : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_MIS"];  
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_SSODB_OLE"];
    string conn2 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE_STD2"];


    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";

    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();


    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    

    protected void Page_Load(object sender, EventArgs e)
    {
        func.write_log("T0/T1 RealTimeDept", Server.MapPath(".") + "\\LOG\\", "log");


       
        
       update_employee("mcemployee");
       update_employee("mcdept");
       check_mail_alive();

        //#region MAIL TO OWNER

        //string mail_title = "T0/T1 RealTimeEmpno 電子報-<==資料更新回報 ==> " + "【" + today + "】";
        //string strHTML = "";

        //ArrayList maillist = func.FileToArray(Server.MapPath(".") + "\\epaper\\maillist\\ARS_owner.txt");


        //SendEmail("cim.alarm01@chimei-innolux.com", maillist[0].ToString(), mail_title, strHTML, "");

        //#endregion

      
      
        func.delete_log_file(Server.MapPath(".\\") + "\\LOG\\", "*.log", -60);

      //javascript 語法填入 字串 
        string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_parent','');window.top.close(this);</script>";
        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmClose);







    }

    public void check_mail_alive()
    {
        func.write_log("T0/T1 Email Alive check ", Server.MapPath(".") + "\\LOG\\", "log");

        sql_temp1 = @"

        select t.* from system_mail_list t
where t.enable_flag='Y' and t.mail_addr like '%@INNOLUX.COM%'
";
        ds_temp2 = func.get_dataSet_access(sql_temp1, conn2);

        for (int i = 0; i <= ds_temp2.Tables[0].Rows.Count-1; i++)
        {

            sql_temp2 = @"
             select * from mcemployee@stdman2sso t
where t.email='{0}'

";
            sql_temp2 = string.Format(sql_temp2, ds_temp2.Tables[0].Rows[i]["MAIL_ADDR"].ToString().Trim());
            ds_temp = func.get_dataSet_access(sql_temp2,conn2);

            if (ds_temp.Tables[0].Rows.Count ==0)
            {
                sql_temp3 = @"

                 update system_mail_list
   set 
       enable_flag = 'N'
      
 where mail_addr = '{0}'
 


";
                sql_temp3 = string.Format(sql_temp3, ds_temp2.Tables[0].Rows[i]["MAIL_ADDR"].ToString().Trim());
                func.get_sql_execute(sql_temp3, conn2);

            }
      
          

            
        }

  

    }
    public  void update_employee(string job)
    {
        if (job == "mcemployee")
        {
            //DWGMIS.MIS_EMP_MAPPING_V 
            
            sql_temp = "SELECT *                                   " +
    "  FROM HCM_VW_EMP01            " +
    " WHERE (PTEXT = '竹科' or PTEXT = '南科' or PTEXT = '龍華')  " +
    "   AND STAT2TXT in ('在職中','留停中')                 " +
    "   AND COMID2 IS NOT NULL                  " +
    "   AND COMID2 <> ' '                       ";
            ds_temp = func.get_dataSet_access(sql_temp, conn);

            if (ds_temp.Tables[0].Rows.Count > 0)
            {
                sql_temp2 = " delete mcemployee where  1=1";

                func.get_sql_execute(sql_temp2, conn1);
            }

            for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
            {

                sql_temp1 = " insert into mcemployee                                                                   " +
    "   (empno, deptno, deptname, cname, oldempno, email, ext, master, dttm,hired,LEVED)                   " +
    " values                                                                                   " +
    "   ('" + ds_temp.Tables[0].Rows[i]["pernr"] + "', '" + ds_temp.Tables[0].Rows[i]["oshort"] + "', '" + ds_temp.Tables[0].Rows[i]["ostext"] + "', '" + ds_temp.Tables[0].Rows[i]["nachn"] + ds_temp.Tables[0].Rows[i]["vorna"] + "', '" + ds_temp.Tables[0].Rows[i]["rufnm"] + "', '" + ds_temp.Tables[0].Rows[i]["comid2"] + "', '" + ds_temp.Tables[0].Rows[i]["comid3"] + "', '" + ds_temp.Tables[0].Rows[i]["zjobcode2txt"] + "', to_date('" + today_detail + "','yyyy/MM/dd HH24:MI:ss'),'" + ds_temp.Tables[0].Rows[i]["hired"] + "','" + ds_temp.Tables[0].Rows[i]["LEVED"] + "') ";

                func.get_sql_execute(sql_temp1, conn1);


            }

            #region MAIL TO OWNER

            string mail_title = "T0/T1 RealTimeEmpno 電子報-<==資料更新回報 ==> " + "【" + today + "】";
            string strHTML = "";

            ArrayList maillist = func.FileToArray(Server.MapPath(".") + "\\epaper\\maillist\\ARS_owner.txt");

            #endregion
            SendEmail("cim.alarm@innolux.com", maillist[0].ToString(), mail_title, strHTML, "");
        
        }

        if (job == "mcdept")
        {

            sql_temp = "SELECT *                                   " +
    "  FROM HCM_VW_DEPT01           ";
   
            ds_temp = func.get_dataSet_access(sql_temp, conn);

            if (ds_temp.Tables[0].Rows.Count > 0)
            {
                sql_temp2 = " delete mcdept where  1=1";

                func.get_sql_execute(sql_temp2, conn1);
            }

            for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
            {
                sql_temp1 = "  insert into mcdept  " +
"    (oobjid,          " +
"     obegda,          " +
"     oendda,          " +
"     oshort,          " +
"     ostext,          " +
"     oftext,          " +
"     omager,          " +
"     osshort,         " +
"     osstext,         " +
"     odepno,          " +
"     oetext,          " +
"     ostrid,          " +
"     osdepno,         " +
"     ocosttype,       " +
"     odepno_05,       " +
"     ostext_05,       " +
"     odepno_10,       " +
"     ostext_10,       " +
"     odepno_20,       " +
"     ostext_20,       " +
"     odepno_30,       " +
"     odepno_40,       " +
"     ostext_40,       " +
"     odepno_50,       " +
"     ostext_50)       " +
"  values              " +
"    ('" + ds_temp.Tables[0].Rows[i]["oobjid"].ToString().Replace("'", "''") + "',        " +
"    to_date('" + ds_temp.Tables[0].Rows[i]["obegda"].ToString().Substring(0, 9) + "','yyyy/MM/dd') ,        " +
"    to_date('" + ds_temp.Tables[0].Rows[i]["oendda"].ToString().Substring(0, 9) + "','yyyy/MM/dd') ,        " +

"     '" + ds_temp.Tables[0].Rows[i]["oshort"].ToString().Replace("'", "''") + "',        " +
"     '" + ds_temp.Tables[0].Rows[i]["ostext"].ToString().Replace("'","''") + "',        " +
"     '" + ds_temp.Tables[0].Rows[i]["oftext"].ToString().Replace("'", "''") + "',        " +
"     '" + ds_temp.Tables[0].Rows[i]["omager"].ToString().Replace("'", "''") + "',        " +
"     '" + ds_temp.Tables[0].Rows[i]["osshort"].ToString().Replace("'", "''") + "',       " +
"     '" + ds_temp.Tables[0].Rows[i]["osstext"].ToString().Replace("'", "''") + "',       " +
"     '" + ds_temp.Tables[0].Rows[i]["odepno"].ToString().Replace("'", "''") + "',        " +
"     '" + ds_temp.Tables[0].Rows[i]["oetext"].ToString().Replace("'", "''") + "',        " +
"     '" + ds_temp.Tables[0].Rows[i]["ostrid"].ToString().Replace("'", "''") + "',        " +
"     '" + ds_temp.Tables[0].Rows[i]["osdepno"].ToString().Replace("'", "''") + "',       " +
"     '" + ds_temp.Tables[0].Rows[i]["ocosttype"].ToString().Replace("'", "''") + "',     " +
"     '" + ds_temp.Tables[0].Rows[i]["odepno_05"].ToString().Replace("'", "''") + "',     " +
"     '" + ds_temp.Tables[0].Rows[i]["ostext_05"].ToString().Replace("'", "''") + "',     " +
"     '" + ds_temp.Tables[0].Rows[i]["odepno_10"].ToString().Replace("'", "''") + "',     " +
"     '" + ds_temp.Tables[0].Rows[i]["ostext_10"].ToString().Replace("'", "''") + "',     " +
"     '" + ds_temp.Tables[0].Rows[i]["odepno_20"].ToString().Replace("'", "''") + "',     " +
"     '" + ds_temp.Tables[0].Rows[i]["ostext_20"].ToString().Replace("'", "''") + "',     " +
"     '" + ds_temp.Tables[0].Rows[i]["odepno_30"].ToString().Replace("'", "''") + "',     " +
"     '" + ds_temp.Tables[0].Rows[i]["odepno_40"].ToString().Replace("'", "''") + "',     " +
"     '" + ds_temp.Tables[0].Rows[i]["ostext_40"].ToString().Replace("'", "''") + "',     " +
"     '" + ds_temp.Tables[0].Rows[i]["odepno_50"].ToString().Replace("'", "''") + "',     " +
"     '" + ds_temp.Tables[0].Rows[i]["ostext_50"].ToString().Replace("'", "''") + "')     ";
                
                            func.get_sql_execute(sql_temp1, conn1);


            }


            #region MAIL TO OWNER

            string mail_title = "T0/T1 RealTimeDept 電子報-<==資料更新回報 ==> " + "【" + today + "】";
            string strHTML = "";

            ArrayList maillist = func.FileToArray(Server.MapPath(".") + "\\epaper\\maillist\\ARS_owner.txt");
            #endregion
            SendEmail("cim.alarm@innolux.com", maillist[0].ToString(), mail_title, strHTML, "");

        }


 
       

    } 

    public static void SendEmail(string from, string to, string subject, string body, string cca)
    {
        SmtpClient smtp = new SmtpClient("10.56.196.147");
        MailMessage email = new MailMessage(from, to, subject, body);
        if (cca == "")
        {
        }
        else
        {
            //email.CC.Add(cca); 
            email.Bcc.Add(cca);
        }

        email.IsBodyHtml = true;
        smtp.Send(email);


    } 
}
