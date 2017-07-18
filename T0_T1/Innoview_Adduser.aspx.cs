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

public partial class T0_T1_Innoview_Adduser : System.Web.UI.Page
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
        sql_temp = " select empno, deptno, deptname, cname, oldempno, email, ext, master, dttm  " +
"   from mcemployee                                                          ";

        #endregion


        func.write_log("T0T1 InnoView Add User",Server.MapPath("../")+"\\LOG\\","log");
        func.delete_log_file(Server.MapPath("../") + "\\LOG\\","*.log",-30);

        
        ds_temp = func.get_dataSet_access(sql_temp, conn);

       for (int i = 0; i <= ds_temp.Tables[0].Rows.Count-1; i++)
			
       
            {

                sql_temp2 = "select count(*) as count1 from iv_users t where t.empno='" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@INNOLUX.COM", "") + "' ";
                
           ds_temp2=func.get_dataSet_access(sql_temp2,conn1);

                if (Convert.ToInt32(ds_temp2.Tables[0].Rows[0]["count1"].ToString()) > 0)
                {
                    #region update innoview config
                    string update = " update iv_users                          " +
"    set empno = '" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@INNOLUX.COM", "") + "',                  " +
"        cname = '" + ds_temp.Tables[0].Rows[i]["cname"] + "',                  " +
"        ename = '" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@INNOLUX.COM", "") + "',                  " +
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
"  where empno='" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@INNOLUX.COM", "") + "'                          ";
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
"   ('" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@INNOLUX.COM", "") + "',            " +
"    '" + ds_temp.Tables[0].Rows[i]["cname"] + "',            " +
"    '" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@INNOLUX.COM", "") + "',            " +
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

                string insert_priviledge1 = "insert into iv_usergroup" +
    "  (empno, groupid)      " +
    "values                  " +
    "  ('" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@INNOLUX.COM", "") + "', '133')  ";
                string insert_priviledge2 = "insert into iv_usergroup" +
"  (empno, groupid)      " +
"values                  " +
"  ('" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@INNOLUX.COM", "") + "', '150')  ";

                string check_priviledge_133 = "select count(*) as count2 from iv_usergroup t where t.empno='" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@INNOLUX.COM", "") + "' and  t.groupid=133 ";
                ds_temp3 = func.get_dataSet_access(check_priviledge_133, conn1);


                if (Convert.ToInt32(ds_temp3.Tables[0].Rows[0]["count2"].ToString()) == 0)
                {
                    func.get_sql_execute(insert_priviledge1, conn1);
                }
                ds_temp3.Clear();

                string check_priviledge_150 = "select count(*) as count2 from iv_usergroup t where t.empno='" + ds_temp.Tables[0].Rows[i]["email"].ToString().Replace("@INNOLUX.COM", "") + "' and  t.groupid=150 ";
                ds_temp3 = func.get_dataSet_access(check_priviledge_150, conn1);

                if (Convert.ToInt32(ds_temp3.Tables[0].Rows[0]["count2"].ToString()) == 0)
                {
                    func.get_sql_execute(insert_priviledge2, conn1);

                }
                


			}

            Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");


    }


  

}
