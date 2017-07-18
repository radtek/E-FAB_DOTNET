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

public partial class T0_T1_Innoview_add_TP_MEETING : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_SSODB_OLE"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["innoview"];
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region user_sso 30 days data
        sql_temp = " select t.* from mcemployee t where t.deptno like '8TPI%'  or t.deptno like '8N19%'  ";


        #endregion

        ds_temp = func.get_dataSet_access(sql_temp, conn);

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
        {

            sql_temp2 = "select count(*) as count1 from iv_users t where t.empno='" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "' ";

            ds_temp2 = func.get_dataSet_access(sql_temp2, conn1);

            if (Convert.ToInt32(ds_temp2.Tables[0].Rows[0]["count1"].ToString()) > 0)
            {
                #region update innoview config
                string update = " update iv_users                          " +
"    set empno = '" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "',                  " +
"        cname = '" + ds_temp.Tables[0].Rows[i]["cname"] + "',                  " +
"        ename = '" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "',                  " +
"        email = '" + ds_temp.Tables[0].Rows[i]["email"] + "',                " +
"        dept ='" + ds_temp.Tables[0].Rows[i]["deptno"] + "',                    " +
"        ext = '" + ds_temp.Tables[0].Rows[i]["ext"] + "',                      " +
"        title = '',                  " +
"        isleader ='" + ds_temp.Tables[0].Rows[i]["master"] + "',         " +
"        leaderlevel = '',     " +
"        syslevel = '',          " +
"        writedate = sysdate,          " +
"        cminl_empno ='" + ds_temp.Tables[0].Rows[i]["empno"] + "',     " +
"        original_empno = '" + ds_temp.Tables[0].Rows[i]["oldempno"] + "' " +
"  where empno='" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "'                          ";
                #endregion

                func.get_sql_execute(update, conn1);

            }
            else
            {
                #region insert innoview config
                string insert1 = " insert into iv_users   " +
"   (empno,              " +
"    cname,              " +
"    ename,              " +
"    email,              " +
"    dept,               " +
"    ext,                " +
"    title,              " +
"    isleader,           " +
"    leaderlevel,        " +
"    syslevel,           " +
"    writedate,          " +
"    cminl_empno,        " +
"    original_empno)     " +
" values                 " +
"   ('" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "',            " +
"    '" + ds_temp.Tables[0].Rows[i]["cname"] + "',            " +
"    '" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "',            " +
"    '" + ds_temp.Tables[0].Rows[i]["email"] + "',            " +
"    '" + ds_temp.Tables[0].Rows[i]["deptno"] + "',             " +
"    '" + ds_temp.Tables[0].Rows[i]["ext"] + "',              " +
"    '',            " +
"    '" + ds_temp.Tables[0].Rows[i]["master"] + "',         " +
"    '',      " +
"    '',         " +
"    sysdate,        " +
"    '" + ds_temp.Tables[0].Rows[i]["empno"] + "',      " +
"    '" + ds_temp.Tables[0].Rows[i]["oldempno"] + "')   ";
                #endregion

                func.get_sql_execute(insert1, conn1);




            }

            string insert_priviledge181 = "insert into iv_usergroup" +
"  (empno, groupid)      " +
"values                  " +
"  ('" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "', '181')  ";


            string insert_priviledge182 = "insert into iv_usergroup" +
"  (empno, groupid)      " +
"values                  " +
"  ('" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "', '182')  ";

            string insert_priviledge183 = "insert into iv_usergroup" +
"  (empno, groupid)      " +
"values                  " +
"  ('" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "', '183')  ";

            string insert_priviledge184 = "insert into iv_usergroup" +
"  (empno, groupid)      " +
"values                  " +
"  ('" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "', '184')  ";


            string insert_priviledge185 = "insert into iv_usergroup" +
"  (empno, groupid)      " +
"values                  " +
"  ('" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "', '185')  ";


            string insert_priviledge186 = "insert into iv_usergroup" +
"  (empno, groupid)      " +
"values                  " +
"  ('" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "', '186')  ";




            string check_priviledge_181 = "select count(*) as count2 from iv_usergroup t where t.empno='" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "' and  t.groupid=181 ";
            ds_temp3 = func.get_dataSet_access(check_priviledge_181, conn1);


            if (Convert.ToInt32(ds_temp3.Tables[0].Rows[0]["count2"].ToString()) == 0)
            {
                func.get_sql_execute(insert_priviledge181, conn1);
            }
            ds_temp3.Clear();

            string check_priviledge_182 = "select count(*) as count2 from iv_usergroup t where t.empno='" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "' and  t.groupid=182 ";
            ds_temp3 = func.get_dataSet_access(check_priviledge_182, conn1);

            if (Convert.ToInt32(ds_temp3.Tables[0].Rows[0]["count2"].ToString()) == 0)
            {
                func.get_sql_execute(insert_priviledge182, conn1);

            }

            string check_priviledge_183 = "select count(*) as count2 from iv_usergroup t where t.empno='" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "' and  t.groupid=183 ";
            ds_temp3 = func.get_dataSet_access(check_priviledge_183, conn1);

            if (Convert.ToInt32(ds_temp3.Tables[0].Rows[0]["count2"].ToString()) == 0)
            {
                func.get_sql_execute(insert_priviledge183, conn1);

            }


            string check_priviledge_184 = "select count(*) as count2 from iv_usergroup t where t.empno='" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "' and  t.groupid=184 ";
            ds_temp3 = func.get_dataSet_access(check_priviledge_184, conn1);

            if (Convert.ToInt32(ds_temp3.Tables[0].Rows[0]["count2"].ToString()) == 0)
            {
                func.get_sql_execute(insert_priviledge184, conn1);

            }


            string check_priviledge_185 = "select count(*) as count2 from iv_usergroup t where t.empno='" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "' and  t.groupid=185 ";
            ds_temp3 = func.get_dataSet_access(check_priviledge_185, conn1);

            if (Convert.ToInt32(ds_temp3.Tables[0].Rows[0]["count2"].ToString()) == 0)
            {
                func.get_sql_execute(insert_priviledge185, conn1);

            }


            string check_priviledge_186 = "select count(*) as count2 from iv_usergroup t where t.empno='" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@CHIMEI-INNOLUX.COM", "") + "' and  t.groupid=186 ";
            ds_temp3 = func.get_dataSet_access(check_priviledge_186, conn1);

            if (Convert.ToInt32(ds_temp3.Tables[0].Rows[0]["count2"].ToString()) == 0)
            {
                func.get_sql_execute(insert_priviledge186, conn1);

            }


           



        }

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");


    }


}
