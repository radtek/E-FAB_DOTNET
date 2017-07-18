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

public partial class _Default : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_PARS1_OLE_ONDUTY"];
   
    
   
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
   
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
   // Int32 count_num = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    
    {
        sql_temp1 = "select t.*  from lam_verify_user_temp t ";

        ds_temp1 = func.get_dataSet_access(sql_temp1, conn);


        for (int i = 0; i <=ds_temp1.Tables[0].Rows.Count-1; i++)
        {
            sql_temp2 = "select count(t.username1)  from lam_verify_user t where t.username1=upper('" + ds_temp1.Tables[0].Rows[i]["username1"] + "')";
            ds_temp2 = func.get_dataSet_access(sql_temp2, conn);
            if (Convert.ToInt32(ds_temp2.Tables[0].Rows[0][0].ToString())==0)
            {


                sql_temp3 = " insert into lam_verify_user " +
 "   (username1, group1)       " +
 " values                      " +
 "   ('" + ds_temp1.Tables[0].Rows[i]["username1"].ToString().ToUpper() + "', 'A')   ";
                func.get_sql_execute(sql_temp3, conn);
               
            }
        }

      
    }
}
