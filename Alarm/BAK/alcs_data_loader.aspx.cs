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

public partial class Alarm_alcs_data_loader : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ALCS_XLS"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_T1NEWALARM"];
    

    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    //定義OLE======================================================

    //1.檔案位置

    //private const string FileName =  "C:\\test.xls";
    //private const string FileName = @"D:\\CIM-SE-RPT-WEB\\E-FAB_dotnet\\Alarm\\Alarm_sample.xls";

       

    //2.提供者名稱

    //private const string ProviderName = "Microsoft.Jet.OLEDB.4.0;";

    //3.Excel版本，Excel 8.0 針對Excel2000及以上版本，Excel5.0 針對Excel97。

    //private const string ExtendedString = "'Excel 8.0;";

    //4.第一行是否為標題

    //private const string Hdr = "Yes;";

    //5.IMEX=1 通知驅動程序始終將「互混」數據列作為文本讀取

    //private const string IMEX = "0';";

    //=============================================================



    //連線字串

    //string cs =

    //        "Data Source=" + FileName + ";" +

    //        "Provider=" + ProviderName +

    //        "Extended Properties=" + ExtendedString +

    //        "HDR=" + Hdr +

    //        "IMEX=" + IMEX;

    DataTable DT = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)

        {
            sql_temp = "SELECT * FROM [Sheet1$] ";

            ds_temp1 = func.get_dataSet_access(sql_temp, conn);
            DT = ds_temp1.Tables[0];
            GridView1.DataSource = ds_temp1.Tables[0];
            GridView1.DataBind();
          


           
        }

     


       
    }

    public void insert_user_id()
    {

        for (int i = 0; i <= DT.Rows.Count-1; i++)
        {

            if (i >= 1)
            {
                sql_temp1 = @"insert into brm_user
  (user_id, user_name, user_password, user_group_id, department_id, auth_level, user_e_mail, user_sms_num, user_mobil_tel, user_start_time, user_end_time, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
values
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',  '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}')";


                sql_temp1 = string.Format(sql_temp1, DT.Rows[i][12].ToString(), DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString(), "G002", DT.Rows[i][16].ToString(), "1", DT.Rows[i][18].ToString(), DT.Rows[i][19].ToString(), DT.Rows[i][20].ToString(), DT.Rows[i][21].ToString(), DT.Rows[i][22].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, DT.Rows[i][28].ToString(), DT.Rows[i][29].ToString());


                try
                {
                    func.get_sql_execute(sql_temp1, conn1);
                }
                catch (Exception)
                {
                     
//                    sql_temp2 = @"insert into brm_user
//  (user_id, user_name, user_password, user_group_id, department_id, auth_level, user_e_mail, user_sms_num, user_mobil_tel, user_start_time, user_end_time, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
//values
//  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',  '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}')";


//                    sql_temp2 = string.Format(sql_temp2, DT.Rows[i][12].ToString() + "_01", DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString(), "G002", DT.Rows[i][16].ToString(), "1", DT.Rows[i][18].ToString(), DT.Rows[i][19].ToString(), DT.Rows[i][20].ToString(), DT.Rows[i][21].ToString(), DT.Rows[i][22].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, DT.Rows[i][28].ToString(), DT.Rows[i][29].ToString());

//                    func.get_sql_execute(sql_temp2, conn1);

                    Label1.Text = "brm_user:USER_ID " + DT.Rows[i][12].ToString() + " brm_user 1 item Reapted!!!";
                    //Response.Write("user_id:" + DT.Rows[i][12].ToString()+" Account Reapted!!!");

                }
               

            
            }
         

        }
       




    }

    public void insert_FAB_id()
    {

        for (int i = 0; i <= DT.Rows.Count - 1; i++)
        {

            if (i >= 1)
            {
                sql_temp1 = @"insert into brm_fab
  (fab_id, fab_name, fab_desc, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
values
  ('{0}', '{1}', '{2}','{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');";


                sql_temp1 = string.Format(sql_temp1,DT.Rows[i][3].ToString(),DT.Rows[i][3].ToString(),DT.Rows[i][3].ToString(),DT.Rows[i][23].ToString() ,DT.Rows[i][24].ToString(),DT.Rows[i][25].ToString(),DT.Rows[i][26].ToString(), today_detail, today_detail, today_detail);


                try
                {
                    func.get_sql_execute(sql_temp1, conn1);
                }
                catch (Exception)
                {
                    //                    sql_temp2 = @"insert into brm_user
                    //  (user_id, user_name, user_password, user_group_id, department_id, auth_level, user_e_mail, user_sms_num, user_mobil_tel, user_start_time, user_end_time, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
                    //values
                    //  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',  '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}')";


                    //                    sql_temp2 = string.Format(sql_temp2, DT.Rows[i][12].ToString() + "_01", DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString(), "G002", DT.Rows[i][16].ToString(), "1", DT.Rows[i][18].ToString(), DT.Rows[i][19].ToString(), DT.Rows[i][20].ToString(), DT.Rows[i][21].ToString(), DT.Rows[i][22].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, DT.Rows[i][28].ToString(), DT.Rows[i][29].ToString());

                    //                    func.get_sql_execute(sql_temp2, conn1);

                    Label3.Text = "<BR>brm_fab:FAB_ID " + DT.Rows[i][3].ToString() + " brm_fab 1 item Reapted!!!";
                    //Response.Write("user_id:" + DT.Rows[i][12].ToString()+" Account Reapted!!!");

                }



            }


        }





    }

    public void insert_SUBSYSTEM_id()
    {

        for (int i = 0; i <= DT.Rows.Count - 1; i++)
        {

            if (i >= 1)
            {
                sql_temp1 = @"insert into brm_subsystem
  (subsystem_id,
   subsystem_name,
   subsystem_desc,
   createuser_id,
   lastmodifieduser_id,
   createuser_name,
   lastmodifieduser_name,
   createdate,
   lastmodifieddate,
   mycomment)
values
  ('{0}',
   '{1}',
   '{2}',
   '{3}',
   '{4}',
   '{5}',
   '{6}',
   '{7}',
   '{8}',
   '{9}') ";


                sql_temp1 = string.Format(sql_temp1, DT.Rows[i][4].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, today_detail, today_detail);


                try
                {
                    func.get_sql_execute(sql_temp1, conn1);
                }
                catch (Exception)
                {
                    //                    sql_temp2 = @"insert into brm_user
                    //  (user_id, user_name, user_password, user_group_id, department_id, auth_level, user_e_mail, user_sms_num, user_mobil_tel, user_start_time, user_end_time, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
                    //values
                    //  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',  '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}')";


                    //                    sql_temp2 = string.Format(sql_temp2, DT.Rows[i][12].ToString() + "_01", DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString(), "G002", DT.Rows[i][16].ToString(), "1", DT.Rows[i][18].ToString(), DT.Rows[i][19].ToString(), DT.Rows[i][20].ToString(), DT.Rows[i][21].ToString(), DT.Rows[i][22].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, DT.Rows[i][28].ToString(), DT.Rows[i][29].ToString());

                    //                    func.get_sql_execute(sql_temp2, conn1);

                    Label4.Text = "<BR>brm_subsystem:SUBSYSTEM_ID " + DT.Rows[i][4].ToString() + " brm_subsystem  1 item Reapted!!!";
                    //Response.Write("user_id:" + DT.Rows[i][12].ToString()+" Account Reapted!!!");

                }



            }


        }





    }

    public void insert_Event_id()
    {

        for (int i = 0; i <= DT.Rows.Count - 1; i++)
        {

            if (i >= 1)
            {
                sql_temp1 = @"insert into brm_event
  (event_id, event_desc, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
values
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')";


                sql_temp1 = string.Format(sql_temp1, DT.Rows[i][1].ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, today_detail, today_detail);


                try
                {
                    func.get_sql_execute(sql_temp1, conn1);
                }
                catch (Exception)
                {
                    //                    sql_temp2 = @"insert into brm_user
                    //  (user_id, user_name, user_password, user_group_id, department_id, auth_level, user_e_mail, user_sms_num, user_mobil_tel, user_start_time, user_end_time, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
                    //values
                    //  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',  '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}')";


                    //                    sql_temp2 = string.Format(sql_temp2, DT.Rows[i][12].ToString() + "_01", DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString(), "G002", DT.Rows[i][16].ToString(), "1", DT.Rows[i][18].ToString(), DT.Rows[i][19].ToString(), DT.Rows[i][20].ToString(), DT.Rows[i][21].ToString(), DT.Rows[i][22].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, DT.Rows[i][28].ToString(), DT.Rows[i][29].ToString());

                    //                    func.get_sql_execute(sql_temp2, conn1);

                    Label2.Text = "<BR>brm_event:EVENT_ID " + DT.Rows[i][1].ToString() + " brm_event 1 item Reapted!!!";
                    //Response.Write("user_id:" + DT.Rows[i][12].ToString()+" Account Reapted!!!");

                }



            }


        }





    }

    public void insert_brm_normalalarm()
    {

        for (int i = 0; i <= DT.Rows.Count - 1; i++)
        {

            if (i >= 1)
            {
                sql_temp1 = @"insert into brm_normalalarm
  (fab_id,
   subsystem_id,
   eq_id,
   eq_name,
   alarm_id,
   alarm_desc,
   alarm_priority,
   alarm_msgfilter,
   alarm_sti,
   event_id,
   createuser_id,
   lastmodifieduser_id,
   createuser_name,
   lastmodifieduser_name,
   createdate,
   lastmodifieddate,
   mycomment)
values
  ('{0}',
   '{1}',
   '{2}',
   '{3}',
   '{4}',
   '{5}',
   '{6}',
   '{7}',
   '{8}',
   '{9}',
   '{10}',
   '{11}',
   '{12}',
   '{13}',
   '{14}',
   '{15}',
   '{16}')";

                //string tmp = "SPC_PDS/PH/1APHT100/DRUS8_PHONE";
                string short_time_idle = "60";

                if (DT.Rows[i][1].ToString().Contains("SPC_PDS") && DT.Rows[i][1].ToString().Contains("PHONE"))
                {

                    short_time_idle = "900";
                }
                else if (DT.Rows[i][1].ToString().Contains("SPC_PDS"))
                {

                    short_time_idle = "180";
                
                }

                sql_temp1 = string.Format(sql_temp1, DT.Rows[i][3].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][5].ToString(), DT.Rows[i][5].ToString(), DT.Rows[i][6].ToString(), "*", "3", "Y", short_time_idle, DT.Rows[i][1].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, today_detail, today_detail);


                try
                {
                    func.get_sql_execute(sql_temp1, conn1);
                }
                catch (Exception)
                {
                    //                    sql_temp2 = @"insert into brm_user
                    //  (user_id, user_name, user_password, user_group_id, department_id, auth_level, user_e_mail, user_sms_num, user_mobil_tel, user_start_time, user_end_time, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
                    //values
                    //  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',  '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}')";


                    //                    sql_temp2 = string.Format(sql_temp2, DT.Rows[i][12].ToString() + "_01", DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString(), "G002", DT.Rows[i][16].ToString(), "1", DT.Rows[i][18].ToString(), DT.Rows[i][19].ToString(), DT.Rows[i][20].ToString(), DT.Rows[i][21].ToString(), DT.Rows[i][22].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, DT.Rows[i][28].ToString(), DT.Rows[i][29].ToString());

                    //                    func.get_sql_execute(sql_temp2, conn1);

                    Label5.Text = "<BR>brm_normalalarm:" + DT.Rows[i][2].ToString() + "/" + DT.Rows[i][3].ToString() + "/" + DT.Rows[i][4].ToString() + "/" + DT.Rows[i][5].ToString() + "/" + DT.Rows[i][6].ToString() + " brm_normalalarm 5 Item Reapted!!!";
                    //Response.Write("user_id:" + DT.Rows[i][12].ToString()+" Account Reapted!!!");

                }



            }


        }





    }

    public void insert_brm_alarmlevel()
    {

        for (int i = 0; i <= DT.Rows.Count - 1; i++)
        {

            if (i >= 1)
            {
                sql_temp1 = @"insert into brm_alarmlevel
  (event_id,
   alarm_level,
   retry_count,
   recall_period,
   time_interval,
   confirmack,
   createuser_id,
   lastmodifieduser_id,
   createuser_name,
   lastmodifieduser_name,
   createdate,
   lastmodifieddate,
   mycomment)
values
  ('{0}',
   '{1}',
  '{2}',
   '{3}',
   '{4}',
  '{5}',
  '{6}',
   '{7}',
   '{8}',
   '{9}',
   '{10}',
   '{11}',
   '{12}')";


                sql_temp1 = string.Format(sql_temp1, DT.Rows[i][1].ToString(), "1", "0", "1", "30", "0", DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, today_detail, today_detail);


                try
                {
                    func.get_sql_execute(sql_temp1, conn1);
                }
                catch (Exception)
                {
                    //                    sql_temp2 = @"insert into brm_user
                    //  (user_id, user_name, user_password, user_group_id, department_id, auth_level, user_e_mail, user_sms_num, user_mobil_tel, user_start_time, user_end_time, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
                    //values
                    //  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',  '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}')";


                    //                    sql_temp2 = string.Format(sql_temp2, DT.Rows[i][12].ToString() + "_01", DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString(), "G002", DT.Rows[i][16].ToString(), "1", DT.Rows[i][18].ToString(), DT.Rows[i][19].ToString(), DT.Rows[i][20].ToString(), DT.Rows[i][21].ToString(), DT.Rows[i][22].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, DT.Rows[i][28].ToString(), DT.Rows[i][29].ToString());

                    //                    func.get_sql_execute(sql_temp2, conn1);

                    Label6.Text = "<BR>brm_alarmlevel:Eventid/Alarmlevel " + DT.Rows[i][1].ToString() + "/1" + " brm_alarmlevel 2 Item  Reapted!!!";
                    //Response.Write("user_id:" + DT.Rows[i][12].ToString()+" Account Reapted!!!");

                }



            }


        }





    }

    public void insert_brm_user_eventgroup()
    {

        for (int i = 0; i <= DT.Rows.Count - 1; i++)
        {

            if (i >= 1)
            {
                sql_temp1 = @"insert into brm_user_eventgroup
  (user_eventgroup_id,
   user_eventgroup_desc,
   mycomment,
   createuser_id,
   lastmodifieduser_id,
   createuser_name,
   lastmodifieduser_name,
   createdate,
   lastmodifieddate)
values
  ('{0}',
   '{1}',
   '{2}',
   '{3}',
   '{4}',
   '{5}',
   '{6}',
   '{7}',
   '{8}')";


                sql_temp1 = string.Format(sql_temp1, DT.Rows[i][2].ToString(), DT.Rows[i][2].ToString(), today_detail, DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, today_detail);


                try
                {
                    func.get_sql_execute(sql_temp1, conn1);
                }
                catch (Exception)
                {
                    //                    sql_temp2 = @"insert into brm_user
                    //  (user_id, user_name, user_password, user_group_id, department_id, auth_level, user_e_mail, user_sms_num, user_mobil_tel, user_start_time, user_end_time, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
                    //values
                    //  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',  '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}')";


                    //                    sql_temp2 = string.Format(sql_temp2, DT.Rows[i][12].ToString() + "_01", DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString(), "G002", DT.Rows[i][16].ToString(), "1", DT.Rows[i][18].ToString(), DT.Rows[i][19].ToString(), DT.Rows[i][20].ToString(), DT.Rows[i][21].ToString(), DT.Rows[i][22].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, DT.Rows[i][28].ToString(), DT.Rows[i][29].ToString());

                    //                    func.get_sql_execute(sql_temp2, conn1);

                    Label7.Text = "<BR>brm_user_eventgroup:USER_EVENTGROUP_ID " + DT.Rows[i][1].ToString() + " brm_user_eventgroup  1 Item  Reapted!!!";
                    //Response.Write("user_id:" + DT.Rows[i][12].ToString()+" Account Reapted!!!");

                }



            }


        }





    }

    public void insert_map_usereventgroup()
    {

        for (int i = 0; i <= DT.Rows.Count - 1; i++)
        {

            if (i >= 1)
            {
                sql_temp1 = @"

insert into map_usereventgroup
  (user_eventgroup_id,
   user_id,
   mycomment,
   createuser_id,
   lastmodifieduser_id,
   createuser_name,
   lastmodifieduser_name,
   createdate,
   lastmodifieddate)
values
  ('{0}',
   '{1}',
   '{2}',
   '{3}',
   '{4}',
   '{5}',
   '{6}',
   '{7}',
   '{8}')";


                sql_temp1 = string.Format(sql_temp1, DT.Rows[i][2].ToString(), DT.Rows[i][12].ToString(), today_detail, DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(),DT.Rows[i][25].ToString(),DT.Rows[i][26].ToString(), today_detail, today_detail);


                try
                {
                    func.get_sql_execute(sql_temp1, conn1);
                }
                catch (Exception)
                {
                    //                    sql_temp2 = @"insert into brm_user
                    //  (user_id, user_name, user_password, user_group_id, department_id, auth_level, user_e_mail, user_sms_num, user_mobil_tel, user_start_time, user_end_time, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
                    //values
                    //  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',  '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}')";


                    //                    sql_temp2 = string.Format(sql_temp2, DT.Rows[i][12].ToString() + "_01", DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString(), "G002", DT.Rows[i][16].ToString(), "1", DT.Rows[i][18].ToString(), DT.Rows[i][19].ToString(), DT.Rows[i][20].ToString(), DT.Rows[i][21].ToString(), DT.Rows[i][22].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, DT.Rows[i][28].ToString(), DT.Rows[i][29].ToString());

                    //                    func.get_sql_execute(sql_temp2, conn1);

                    Label8.Text = "<BR>map_usereventgroup:USER_EVENTGROUP_ID/USER_ID " + DT.Rows[i][1].ToString()+"/"+ " brm_user_eventgroup  1 Item  Reapted!!!";
                    //Response.Write("user_id:" + DT.Rows[i][12].ToString()+" Account Reapted!!!");

                }



            }


        }





    }

    public void insert_map_dispatchrule()
    {

        for (int i = 0; i <= DT.Rows.Count - 1; i++)
        {

            if (i >= 1)
            {
                sql_temp1 = @"

insert into map_dispatchrule
  (event_id,
   user_id,
   alarm_level,
   message_id1,
   message_id2,
   message_id3,
   user_eventgroup_id,
   isgrouprule,
   createuser_id,
   lastmodifieduser_id,
   createuser_name,
   lastmodifieduser_name,
   createdate,
   lastmodifieddate,
   mycomment)
values
  ('{0}',
   '{1}',
   '{2}',
   '{3}',
   '{4}',
   '{5}',
   '{6}',
   '{7}',
   '{8}',
   '{9}',
   '{10}',
   '{11}',
   '{12}',
   '{13}',
   '{14}')";


                sql_temp1 = string.Format(sql_temp1, DT.Rows[i][1].ToString(), DT.Rows[i][12].ToString(), "1", DT.Rows[i][7].ToString(), DT.Rows[i][8].ToString(), DT.Rows[i][9].ToString(), DT.Rows[i][2].ToString(), "1", DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, today_detail, today_detail);


                try
                {
                    sql_temp3 = @"select count(tt.event_id) as counter from map_dispatchrule tt
where tt.event_id='{0}'    ";

                    sql_temp3 = string.Format(sql_temp3, DT.Rows[i][1].ToString());
                    ds_temp3 = func.get_dataSet_access(sql_temp3, conn1);
                    if (ds_temp3.Tables[0].Rows[0][0].ToString().Equals("0"))
                    {
                        func.get_sql_execute(sql_temp1, conn1);


                    }
                    else
                    {

                        Label9.Text = "<BR>map_dispatchrule:EVENT_ID/USER_ID/ALARM_LEVEL/USER_EVENTGROUP_ID " + DT.Rows[i][1].ToString() + "/" + DT.Rows[i][12].ToString() + "/1/" + DT.Rows[i][2].ToString() + " map_dispatchrule  4 Item  Reapted!!!";
                    
                    }

                   
                }
                catch (Exception)
                {
                    //                    sql_temp2 = @"insert into brm_user
                    //  (user_id, user_name, user_password, user_group_id, department_id, auth_level, user_e_mail, user_sms_num, user_mobil_tel, user_start_time, user_end_time, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
                    //values
                    //  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',  '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}')";


                    //                    sql_temp2 = string.Format(sql_temp2, DT.Rows[i][12].ToString() + "_01", DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString(), "G002", DT.Rows[i][16].ToString(), "1", DT.Rows[i][18].ToString(), DT.Rows[i][19].ToString(), DT.Rows[i][20].ToString(), DT.Rows[i][21].ToString(), DT.Rows[i][22].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, DT.Rows[i][28].ToString(), DT.Rows[i][29].ToString());

                    //                    func.get_sql_execute(sql_temp2, conn1);

                    Label9.Text = "<BR>map_dispatchrule:EVENT_ID/USER_ID/ALARM_LEVEL/USER_EVENTGROUP_ID " + DT.Rows[i][1].ToString() + "/" + DT.Rows[i][12].ToString() + "/1/" + DT.Rows[i][2].ToString() + " map_dispatchrule  4 Item  Reapted!!!";
                    //Response.Write("user_id:" + DT.Rows[i][12].ToString()+" Account Reapted!!!");

                }



            }


        }





    }

    protected void ButtonUpload_Click(object sender, EventArgs e)
    {

        System.Text.StringBuilder myLabel = new System.Text.StringBuilder();

        string strSql123 = "";
        for (int i = 1; i <= Request.Files.Count; i++)
        {
            FileUpload myFL = new FileUpload();
            myFL = (FileUpload)Page.FindControl("FileUpload" + i);
            if ((myFL.PostedFile != null) && (myFL.PostedFile.ContentLength > 0))
            {

                string fn = System.IO.Path.GetFileName(myFL.PostedFile.FileName);
                string SaveLocation = Server.MapPath("upload_file/") + fn;
                int file_size = myFL.PostedFile.ContentLength;
                string file_type = myFL.PostedFile.ContentType;
                //try
                //{
                //    myFL.PostedFile.SaveAs(SaveLocation);

                //    OleDbConnection myConnection = new OleDbConnection(ConfigurationSettings.AppSettings["dsnn"]);

                //    string strClientIP;

                //    strClientIP = Request.ServerVariables["remote_host"].ToString();


                //    strSql123 = "insert into mms_meeting_upload_file";

                //    strSql123 += "(aid, aid2, file_path, file_name, file_size, file_type, ip, user_id) values";

                //    strSql123 += "('" + Request.QueryString["aid"].ToString() + "', '" + Request.QueryString["aid2"].ToString() + "', '" + SaveLocation + "', '" + fn + "', '" + file_size + "', '" + file_type + "', '" + strClientIP + "', 'test')";
                //    //strSql123 += "(1, 2, '" + SaveLocation + "', '" + fn + "', '" + file_size + "', '" + file_type + "', '" + strClientIP + "', 'test')"; 


                //    OleDbCommand myCommand = new OleDbCommand(strSql123, myConnection);
                //    myConnection.Open();
                //    OleDbDataReader MyReader = myCommand.ExecuteReader();
                //    myLabel.Append("<hr>--- " + fn);

                //    Label2.Text = " 上傳成功!!!" + myLabel.ToString();

                //}
                //catch (Exception Ex)
                //{
                //    Response.Write("銝撜瑼獢憭望");
                //}

                myFL.PostedFile.SaveAs(SaveLocation);


                #region insert data to New Alarm Server mapping Db
                sql_temp = "SELECT * FROM [Sheet1$] ";

                ds_temp1 = func.get_dataSet_access(sql_temp, conn);
                DT = ds_temp1.Tables[0];

                insert_user_id();
                insert_Event_id();
                insert_FAB_id();
                insert_SUBSYSTEM_id();
                insert_brm_normalalarm();
                insert_brm_alarmlevel();
                insert_brm_user_eventgroup();
                insert_map_usereventgroup();
                insert_map_dispatchrule();

                #endregion




                myLabel.Append("<BR><hr>--- " + fn);
                

            }

          

            LabelX.Text = " <br>上傳成功!!!" + myLabel.ToString();

        }

        sql_temp = "SELECT * FROM [Sheet1$] ";

        ds_temp1 = func.get_dataSet_access(sql_temp, conn);
        GridView1.DataSource = ds_temp1.Tables[0];
        GridView1.DataBind();


    }

    

}
