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
using System.Web.Mail;

public partial class common_form_WebForm_task_remind_mail : System.Web.UI.Page
{
    dbutil DBUtil = new dbutil();

    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        DataSet ds3 = new DataSet();
        string strMailTo = "", strMailSubject = "", strMailBody = "", strEmpno = "", strEmpname = "", strDept = "";
        //三級主管不寄每日工作摘要
        string strSql = string.Empty;
        //寄給部門內所有工程師每日工作摘要
        strSql = "select t1.empno,t1.cname from tms_dept_manager t,tms_empinfo t1 where t.dept=t1.dept and t.task_remind_mail='Y'";
        strSql += "and t.manager_id <> t1.empno and nvl(t1.disable,'N')='N'";
        ds = DBUtil.GetDataset(strSql);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            strEmpno = ds.Tables[0].Rows[i]["empno"].ToString();
            strEmpname = ds.Tables[0].Rows[i]["cname"].ToString();

            Server.Execute("task_remind_content.aspx?empno=" + strEmpno + "&remind=" + "eng", htw);

            strMailTo = strEmpno + "@innolux.com.tw";
            //strMailTo = "twn050314@innolux.com.tw";
            strMailSubject = "[Task Management System] " + strEmpname + "的每日【" + DateTime.Now.ToString("yyyy/MM/dd") + "】工作事項提醒";
            strMailBody = sw.ToString();

            DBUtil.sendMail(strMailBody, strMailTo, strMailSubject);
        }

        //寄給部門內各課三級主管該課delay的工作列表
        string strSql2 = string.Empty;
        strSql2 = @"select t.manager_id, t.manager_name, t.dept
                    from tms_dept_manager t
                    where t.manager_level = '3' and t.task_remind_mail='Y'";

        ds2 = DBUtil.GetDataset(strSql2);


        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            strEmpno = ds2.Tables[0].Rows[i]["manager_id"].ToString();
            strEmpname = ds2.Tables[0].Rows[i]["manager_name"].ToString();
            strDept = ds2.Tables[0].Rows[i]["dept"].ToString();

            Server.Execute("task_remind_content_by_boss.aspx?dept=" + strDept + "&remind=" + "manager", htw);

            strMailTo = strEmpno + "@innolux.com.tw";
            //strMailTo = "twn050314@innolux.com.tw";
            strMailSubject = "[Task Management System] " + strDept + " " + "Delay的工作事項提醒";
            strMailBody = sw.ToString();

            DBUtil.sendMail(strMailBody, strMailTo, strMailSubject);
        }

        //寄給部門二級主管該部門delay的工作列表
        string strSql3 = string.Empty;
        strSql3 = @"select *  from tms_dept_manager t  where t.manager_level = '2' and t.task_remind_mail = 'Y'";

        ds3 = DBUtil.GetDataset(strSql3);


        for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            strEmpno = ds3.Tables[0].Rows[i]["manager_id"].ToString();
            strEmpname = ds3.Tables[0].Rows[i]["manager_name"].ToString();
            strDept = ds3.Tables[0].Rows[i]["dept"].ToString();

            Server.Execute("task_remind_content_by_boss.aspx?dept=" + strDept + "&remind=" + "boss", htw);

            strMailTo = strEmpno + "@innolux.com.tw";
            //strMailTo = "twn050314@innolux.com.tw";
            strMailSubject = "[Task Management System] " + strDept + " " + "Delay的工作事項提醒";
            strMailBody = sw.ToString();

            DBUtil.sendMail(strMailBody, strMailTo, strMailSubject);
        }


        Response.Write("<script language='javascript'>" + "\n");
        Response.Write("window.opener=null; window.close();</script>");
    }
}
