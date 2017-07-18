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

public partial class common_form_WebForm_task_remind_content_by_boss : System.Web.UI.Page
{
    dbutil DBUtil = new dbutil();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            string remind_type = Request.QueryString["remind"].ToString();
            string vDept = Request.QueryString["dept"].ToString();
            title.InnerText = "TMS系統提醒你，" + vDept + " " + "Delay的工作事項如下:";


            if (remind_type == "boss")
            {
                lblProject.Visible = true;
                lblTask.Visible = true;
                lblAI.Visible = false;
            }
            else if (remind_type == "manager")
            {
                lblProject.Visible = false;
                lblTask.Visible = true;
                lblAI.Visible = true;

            }
            setTaskData();

        }
    }

    private void setTaskData()
    {
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();

        string strSql = "";
        string remind_type = Request.QueryString["remind"].ToString();

        //該部門所有delay的prjoect和task
        if (remind_type == "boss")
        {
            strSql = @" select rownum, a.* from
                  (select distinct 
                       t.project_id,
                       t.applicant_dept,
                       t.project_desc,
                       t.project_name,
                       t.applicant,
                       t.apply_date,
                       to_char(t.estimate_start_date, 'YYYY/MM/DD') estimate_start_date,
                       to_char(t.estimate_end_date, 'YYYY/MM/DD') estimate_end_date,
                       to_char(t.actual_start_date, 'YYYY/MM/DD') actual_start_date,
                       to_char(t.actual_end_date, 'YYYY/MM/DD') actual_end_date,
                       t.priority,
                       t.status,
                       t1.project_group
                  from tms_project t,
                       tms_project_group t1             
                 where t.project_group_id = t1.project_id(+)
                   and t.estimate_end_date<to_date(to_char(sysdate,'yyyy/mm/dd'),'yyyy/mm/dd')
                   and t.status not in ('Close','Cancel')
                   and t.applicant_dept in
                       (select t.authority_dept
                          from tms_authority_dept t
                         where t.dept = '{0}')
                  order by applicant_dept, estimate_end_date, estimate_start_date)a";

            strSql = string.Format(strSql, Request.QueryString["dept"].ToString());
            ds = DBUtil.GetDataset(strSql);
            gvProject.DataSource = ds;
            gvProject.DataBind();


            strSql = @"select rownum, a.* from
                       (select distinct         
                                    t.task_id,
                                    t.task_desc,
                                    t2.dept,
                                    to_char(t.estimate_start_date, 'yyyy/mm/dd') estimate_start_date,
                                    to_char(t.estimate_end_date, 'yyyy/mm/dd') estimate_end_date,
                                    to_char(t.actual_start_date, 'yyyy/mm/dd') actual_start_date,
                                    to_char(t.actual_end_date, 'yyyy/mm/dd') actual_end_date,
                                    t.priority,
                                    t.status,
                                    t.task_type,
                                    t3.project_name,
                                    t3.project_id
                      from tms_task        t,
                           tms_member      t1,
                           tms_empinfo     t2,
                           tms_project     t3,
                           tms_action_item t4
                     where t.task_id = t1.task_id
                       and t1.member_id = t2.empno
                       and t.project_id = t3.project_id(+)
                       and t.task_id = t4.task_id(+)
                       and t1.is_owner='Y'
                       and t2.dept in (select t.authority_dept
                                              from tms_authority_dept t
                                             where t.dept = '{0}')
                       and t.estimate_end_date <to_date(to_char(sysdate,'yyyy/mm/dd'),'yyyy/mm/dd')
                       and t.status not in ('Close','Cancel')
                     order by dept, estimate_end_date, estimate_start_date) a";


            strSql = string.Format(strSql, Request.QueryString["dept"].ToString());
            ds = DBUtil.GetDataset(strSql);
            gvTask.DataSource = ds;
            gvTask.DataBind();
        }

        //各課delay的task和ai
        else if (remind_type == "manager")
        {
            strSql = @"select rownum, a.* from
                       (select distinct 
                                    t.task_id,
                                    t.task_desc,
                                    t2.dept,
                                    to_char(t.estimate_start_date, 'yyyy/mm/dd') estimate_start_date,
                                    to_char(t.estimate_end_date, 'yyyy/mm/dd') estimate_end_date,
                                    to_char(t.actual_start_date, 'yyyy/mm/dd') actual_start_date,
                                    to_char(t.actual_end_date, 'yyyy/mm/dd') actual_end_date,
                                    t.priority,
                                    t.status,
                                    t.task_type,
                                    t3.project_name,
                                    t3.project_id
                      from tms_task        t,
                           tms_member      t1,
                           tms_empinfo     t2,
                           tms_project     t3,
                           tms_action_item t4
                     where t.task_id = t1.task_id
                       and t1.member_id = t2.empno
                       and t.project_id = t3.project_id(+)
                       and t.task_id = t4.task_id(+)
                       and t2.dept in ('{0}')
                       and t.estimate_end_date <to_date(to_char(sysdate,'yyyy/mm/dd'),'yyyy/mm/dd')
                       and t.status not in ('Close','Cancel')
                     order by estimate_end_date, estimate_start_date)a";

            strSql = string.Format(strSql, Request.QueryString["dept"].ToString());
            ds2 = DBUtil.GetDataset(strSql);
            gvTask.DataSource = ds2;
            gvTask.DataBind();


            strSql = @"select rownum, a.* from
                  (select distinct
                       t.task_id,
                       t.ai_id,
                       t.ai_desc,
                       to_char(t.actual_start_date, 'yyyy/mm/dd') actual_start_date,
                       to_char(t.actual_end_date, 'yyyy/mm/dd') actual_end_date,
                       to_char(t.estimate_start_date, 'yyyy/mm/dd') estimate_start_date,
                       to_char(t.estimate_end_date, 'yyyy/mm/dd') estimate_end_date,
                       t.member_name,
                       t.status,
                       t.progress,
                       t1.task_desc,
                       t3.dept
                  from tms_action_item t, tms_task t1, tms_member t2, tms_empinfo t3
                 where t.task_id(+) = t1.task_id
                   and t1.task_id = t2.task_id
                   and t2.member_id = t3.empno  
                   and t.estimate_end_date <
                       to_date(to_char(sysdate, 'yyyy/mm/dd'), 'yyyy/mm/dd')
                   and t.status not in ('Close', 'Cancel')
                   and t3.dept in ('{0}')
                   order by estimate_end_date, estimate_start_date)a";

            strSql = string.Format(strSql, Request.QueryString["dept"].ToString());
            ds2 = DBUtil.GetDataset(strSql);
            gvAI.DataSource = ds2;
            gvAI.DataBind();
        }

    }

}