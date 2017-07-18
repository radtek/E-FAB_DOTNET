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

public partial class CL1_Innoview_Adduser : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_SSODB_OLE"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CL_innoview_OLE"];
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
        sql_temp = " select empno,                        " +
"        cname,                        " +
"        ename,                        " +
"        email,                        " +
"        dept,                         " +
"        ext,                          " +
"        title,                        " +
"        isleader,                     " +
"        leaderlevel,                  " +
"        syslevel,                     " +
"        writedate,                    " +
"        cminl_empno,                  " +
"        original_empno                " +
"   from fwempinfo                     " +
"  where cminl_empno is not null       " +
"    and writedate >= sysdate - 7     ";

        #endregion
        
        ds_temp = func.get_dataSet_access(sql_temp, conn);

       for (int i = 0; i <= ds_temp.Tables[0].Rows.Count-1; i++)
			
       
            {

                sql_temp2 = "select count(*) as count1 from iv_users t where t.empno='" + ds_temp.Tables[0].Rows[i]["empno"] + "' ";
                
           ds_temp2=func.get_dataSet_access(sql_temp2,conn1);

                if (Convert.ToInt32(ds_temp2.Tables[0].Rows[0]["count1"].ToString()) > 0)
                {
                    #region update innoview config
                    string update = " update iv_users                          " +
"    set empno = '" + ds_temp.Tables[0].Rows[i]["empno"] + "',                  " +
"        cname = '" + ds_temp.Tables[0].Rows[i]["cname"] + "',                  " +
"        ename = '" + ds_temp.Tables[0].Rows[i]["ename"] + "',                  " +
"        email = '" + ds_temp.Tables[0].Rows[i]["email"] + "',                " +
"        dept ='" + ds_temp.Tables[0].Rows[i]["dept"] + "',                    " +
"        ext = '" + ds_temp.Tables[0].Rows[i]["ext"] +"',                      " +
"        title = '" + ds_temp.Tables[0].Rows[i]["title"] + "',                  " +
"        isleader ='" + ds_temp.Tables[0].Rows[i]["isleader"] + "',         " +
"        leaderlevel = '" + ds_temp.Tables[0].Rows[i]["leaderlevel"] + "',     " +
"        syslevel = '" + ds_temp.Tables[0].Rows[i]["syslevel"] + "',          " +
"        writedate = sysdate,          " +
"        cminl_empno ='" + ds_temp.Tables[0].Rows[i]["cminl_empno"] + "',     " +
"        original_empno = '" + ds_temp.Tables[0].Rows[i]["original_empno"] + "' " +
"  where empno='" + ds_temp.Tables[0].Rows[i]["empno"] + "'                          ";
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
"   ('" + ds_temp.Tables[0].Rows[i]["empno"] + "',            " +
"    '" + ds_temp.Tables[0].Rows[i]["cname"] + "',            " +
"    '" + ds_temp.Tables[0].Rows[i]["ename"] + "',            " +
"    '" + ds_temp.Tables[0].Rows[i]["email"] + "',            " +
"    '" + ds_temp.Tables[0].Rows[i]["dept"] + "',             " +
"    '" + ds_temp.Tables[0].Rows[i]["ext"] + "',              " +
"    '" + ds_temp.Tables[0].Rows[i]["title"] + "',            " +
"    '" + ds_temp.Tables[0].Rows[i]["isleader"] + "',         " +
"    '" + ds_temp.Tables[0].Rows[i]["leaderlevel"] + "',      " +
"    '" + ds_temp.Tables[0].Rows[i]["syslevel"] + "',         " +
"    sysdate,        " +
"    '" + ds_temp.Tables[0].Rows[i]["cminl_empno"] + "',      " +
"    '" + ds_temp.Tables[0].Rows[i]["original_empno"] + "')   ";
                    #endregion
                   
                    func.get_sql_execute(insert1, conn1);

                    string insert_priviledge1 = "insert into iv_usergroup" +
"  (empno, groupid)      " +
"values                  " +
"  ('"+ds_temp.Tables[0].Rows[i]["empno"]+"', '133')  ";
                    string insert_priviledge2 = "insert into iv_usergroup" +
"  (empno, groupid)      " +
"values                  " +
"  ('" + ds_temp.Tables[0].Rows[i]["empno"] + "', '150')  ";

                    string check_priviledge_133="select count(*) as count2 from iv_usergroup t where t.empno='"+ds_temp.Tables[0].Rows[i]["empno"]+"' and  t.groupid=133 ";
                    ds_temp3=func.get_dataSet_access(check_priviledge_133,conn1);

                    
                    if(Convert.ToInt32(ds_temp3.Tables[0].Rows[0]["count2"].ToString())==0)
                    {
                         func.get_sql_execute(insert_priviledge1, conn1);
                    }
                    ds_temp3.Clear();

                       string check_priviledge_150="select count(*) as count2 from iv_usergroup t where t.empno='"+ds_temp.Tables[0].Rows[i]["empno"]+"' and  t.groupid=150 ";
                    ds_temp3=func.get_dataSet_access(check_priviledge_150,conn1);

                    if (Convert.ToInt32(ds_temp3.Tables[0].Rows[0]["count2"].ToString()) == 0)
                    {
                         func.get_sql_execute(insert_priviledge2, conn1);

                    }
                  
                  
                }
                


			}

            Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");


    }


  

}
