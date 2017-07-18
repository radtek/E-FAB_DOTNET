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

public partial class oldToNewWORKNO : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["innoview"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_SSODB_OLE"];
    string conn2 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_PARS1_OLE_ARSNEW"];
    string conn3 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_PARS1_OLE_LHEDA"];
    string sql = "";
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
    Int32 count_num = 0;

    
    protected void Page_Load(object sender, EventArgs e)
    {

      

   }


    protected DataSet bind_data( string sn)
    {

        sql = "  select t3.empno,t3.groupid  from iv_usergroup   t3 " +
             "  where t3.empno ='" + sn + "'                  ";

        sql_temp = " select t.empno,upper(t.ename) as ename from fwempinfo t " +
                   " where t.empno='" + sn + "'                               ";


        ds_temp = func.get_dataSet_access(sql, conn);

        ds_temp1 = func.get_dataSet_access(sql_temp, conn1);  // 1 record

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count-1; i++)
        {
            sql_temp1 = " select count(t3.empno)  as sum_num from iv_usergroup   t3 " +
" where t3.empno ='" + ds_temp1.Tables[0].Rows[0]["ename"] + "' and  t3.groupid='" + ds_temp.Tables[0].Rows[i]["groupid"] + "'       ";

            ds_temp2 = func.get_dataSet_access(sql_temp1, conn);
            count_num = Convert.ToInt32(ds_temp2.Tables[0].Rows[0]["sum_num"].ToString());

            if (count_num >= 1)
            {


            }
            else
            {
                sql_temp2 = " insert into iv_usergroup " +
   "   (empno, groupid)       " +
   " values                   " +
   "   ('" + ds_temp1.Tables[0].Rows[0]["ename"] + "', '" + ds_temp.Tables[0].Rows[i]["groupid"] + "')   ";

                func.get_sql_execute(sql_temp2, conn);
           
            }
        
        }




        return ds_temp;




    }


    protected DataSet bind_data1(string sn)
    {

        sql = "  select t3.username1,t3.group1  from tft_verify_user   t3 " +
             "  where t3.username1 ='" + sn + "'                  ";

        sql_temp = " select t.empno,upper(t.ename) as ename from fwempinfo t " +
                   " where t.empno='" + sn + "'                               ";


        ds_temp = func.get_dataSet_access(sql, conn2);

        ds_temp1 = func.get_dataSet_access(sql_temp, conn1);  // 1 record

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
        {
            sql_temp1 = " select count(t3.username1)  as sum_num from tft_verify_user   t3 " +
" where t3.username1 ='" + ds_temp1.Tables[0].Rows[0]["ename"] + "' and  t3.group1='" + ds_temp.Tables[0].Rows[i]["group1"] + "'       ";

            ds_temp2 = func.get_dataSet_access(sql_temp1, conn2);
            count_num = Convert.ToInt32(ds_temp2.Tables[0].Rows[0]["sum_num"].ToString());

            if (count_num >= 1)
            {


            }
            else
            {
                sql_temp2 = " insert into tft_verify_user " +
   "   (username1, group1)       " +
   " values                   " +
   "   ('" + ds_temp1.Tables[0].Rows[0]["ename"] + "', '" + ds_temp.Tables[0].Rows[i]["group1"] + "')   ";

                func.get_sql_execute(sql_temp2, conn2);

            }

        }




        return ds_temp;




    }


    protected DataSet bind_data2(string sn)
    {

        sql = "  select t3.username1,t3.group1  from lam_verify_user   t3 " +
             "  where t3.username1 ='" + sn + "'                  ";

        sql_temp = " select t.empno,upper(t.ename) as ename from fwempinfo t " +
                   " where t.empno='" + sn + "'                               ";


        ds_temp = func.get_dataSet_access(sql, conn3);

        ds_temp1 = func.get_dataSet_access(sql_temp, conn1);  // 1 record

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
        {
            sql_temp1 = " select count(t3.username1)  as sum_num from lam_verify_user   t3 " +
" where t3.username1 ='" + ds_temp1.Tables[0].Rows[0]["ename"] + "' and  t3.group1='" + ds_temp.Tables[0].Rows[i]["group1"] + "'       ";

            ds_temp2 = func.get_dataSet_access(sql_temp1, conn3);
            count_num = Convert.ToInt32(ds_temp2.Tables[0].Rows[0]["sum_num"].ToString());

            if (count_num >= 1)
            {


            }
            else
            {
                sql_temp2 = " insert into lam_verify_user " +
   "   (username1, group1)       " +
   " values                   " +
   "   ('" + ds_temp1.Tables[0].Rows[0]["ename"] + "', '" + ds_temp.Tables[0].Rows[i]["group1"] + "')   ";

                func.get_sql_execute(sql_temp2, conn3);

            }

        }




        return ds_temp;




    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        bind_data(TextBox1.Text.ToUpper());

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        
        sql_temp3 = " select t.empno from fwempinfo t " +
" where t.empno like '%" + TextBox2.Text.ToUpper() + "%'      ";


        ds_temp3 = func.get_dataSet_access(sql_temp3, conn1);

        for (int i = 0; i <= ds_temp3.Tables[0].Rows.Count-1; i++)
        {
             bind_data(ds_temp3.Tables[0].Rows[i]["empno"].ToString().ToUpper());

        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        bind_data1(TextBox3.Text.ToUpper());
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        sql_temp3 = " select t.empno from fwempinfo t " +
" where t.empno like '%" + TextBox4.Text.ToUpper() + "%'      ";


        ds_temp3 = func.get_dataSet_access(sql_temp3, conn1);

        for (int i = 0; i <= ds_temp3.Tables[0].Rows.Count - 1; i++)
        {
            bind_data1(ds_temp3.Tables[0].Rows[i]["empno"].ToString().ToUpper());

        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        bind_data2(TextBox5.Text.ToUpper());
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        sql_temp3 = " select t.empno from fwempinfo t " +
" where t.empno like '%" + TextBox6.Text.ToUpper() + "%'      ";


        ds_temp3 = func.get_dataSet_access(sql_temp3, conn1);

        for (int i = 0; i <= ds_temp3.Tables[0].Rows.Count - 1; i++)
        {
            bind_data2(ds_temp3.Tables[0].Rows[i]["empno"].ToString().ToUpper());

        }
    }
}
