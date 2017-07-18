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

public partial class common_form_WebForm_task_remind_content : System.Web.UI.Page
{
    dbutil DBUtil = new dbutil();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            title.InnerText = "TMS系統提醒你，今天【" + DateTime.Now.ToString("yyyy/MM/dd") + "】的工作事項如下:";
            setTaskData();
        }
    }

    private void setTaskData()
    {
        DataSet ds = new DataSet();

        string strSql = "";

        //今日到期 Task
        strSql = @"select  'Delay' type,t.task_id, t.task_desc, to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date,
        to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date, t.priority, t.status, t.task_type,t2.project_name
        from tms_task t,tms_member t1,tms_project t2
                where t.task_id=t1.task_id and t.project_id=t2.project_id(+) 
                and t1.member_id='{0}'
                and t.estimate_end_date<to_date(to_char(sysdate,'yyyy/mm/dd'),'yyyy/mm/dd')
                and t.status not in ('Close','Cancel')
        union
        select '今日到期' type, t.task_id, t.task_desc, to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date,
        to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date, t.priority, t.status, t.task_type,t2.project_name
        from tms_task t,tms_member t1,tms_project t2
                where t.task_id=t1.task_id and t.project_id=t2.project_id(+)
                and t1.member_id='{0}' 
                and t.estimate_end_date=to_date(to_char(sysdate,'yyyy/mm/dd'),'yyyy/mm/dd')
                and t.status not in ('Close','Cancel')
        union
        select '明日到期' type,t.task_id, t.task_desc, to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date,
        to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date, t.priority, t.status, t.task_type,t2.project_name
        from tms_task t,tms_member t1,tms_project t2
                where t.task_id=t1.task_id and t.project_id=t2.project_id(+) 
                and t1.member_id='{0}'
                and t.estimate_end_date=to_date(to_char(sysdate+1,'yyyy/mm/dd'),'yyyy/mm/dd')
                and t.status not in ('Close','Cancel')
        union
        select '處理中' type,t.task_id, t.task_desc, to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date,
        to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date, t.priority, t.status, t.task_type,t2.project_name
        from tms_task t,tms_member t1,tms_project t2
                where t.task_id=t1.task_id and t.project_id=t2.project_id(+) 
                and t1.member_id='{0}'
                and t.estimate_end_date>to_date(to_char(sysdate+1,'yyyy/mm/dd'),'yyyy/mm/dd')
                and t.status not in ('Close','Cancel') ";

        strSql = string.Format(strSql, Request.QueryString["empno"].ToString());
        ds = DBUtil.GetDataset(strSql);
        gvTask.DataSource = ds;
        gvTask.DataBind();

        //        //明日到期 Task
        //        strSql = @"select  t.task_id, t.task_desc, to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date,
        //        to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date, t.priority, t.status, t.task_type
        //        from tms_task t,tms_member t1
        //                where t.task_id=t1.task_id 
        //                and t1.member_id='TWN050555' 
        //                and t.estimate_end_date=to_date(to_char(sysdate+1,'yyyy/mm/dd'),'yyyy/mm/dd')
        //                and t.status not in ('Close','Cancel')";

        //        ds = DBUtil.GetDataset(strSql);
        //        gvTomorrowTask.DataSource = ds;
        //        gvTomorrowTask.DataBind();

        //        //Delay Task
        //        strSql = @"select  t.task_id, t.task_desc, to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date,
        //        to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date, t.priority, t.status, t.task_type
        //        from tms_task t,tms_member t1
        //                where t.task_id=t1.task_id 
        //                
        //                and t.estimate_end_date<to_date(to_char(sysdate,'yyyy/mm/dd'),'yyyy/mm/dd')
        //                and t.status not in ('Close','Cancel')";

        //        ds = DBUtil.GetDataset(strSql);
        //        gvDelay.DataSource = ds;
        //        gvDelay.DataBind(); 



        //今日到期 Action Item
        strSql = @" select 'Delay' type,t.task_id,t.ai_id,t.ai_desc, to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date,
                to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date, t.member_name, t.status, t.progress,t1.task_desc
                from tms_action_item t,tms_task t1
                where t.task_id=t1.task_id and t.estimate_end_date<to_date(to_char(sysdate,'yyyy/mm/dd'),'yyyy/mm/dd')
                and t.status not in ('Close','Cancel')
                and t.member_id='{0}'
                union
                select '今日到期' type, t.task_id,t.ai_id,t.ai_desc, to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date,
                to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date, t.member_name, t.status, t.progress,t1.task_desc
                from tms_action_item t,tms_task t1
                where t.task_id=t1.task_id and t.estimate_end_date=to_date(to_char(sysdate,'yyyy/mm/dd'),'yyyy/mm/dd')
                and t.status not in ('Close','Cancel')
                and t.member_id='{0}'
                union
                select '明日到期' type,t.task_id,t.ai_id,t.ai_desc, to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date,
                to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date, t.member_name, t.status, t.progress,t1.task_desc
                from tms_action_item t,tms_task t1
                where t.task_id=t1.task_id and t.estimate_end_date=to_date(to_char(sysdate+1,'yyyy/mm/dd'),'yyyy/mm/dd')
                and t.status not in ('Close','Cancel')
                and t.member_id='{0}'
            union
                select '處理中' type,t.task_id,t.ai_id,t.ai_desc, to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date,
                to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date, t.member_name, t.status, t.progress,t1.task_desc
                from tms_action_item t,tms_task t1
                where t.task_id=t1.task_id and t.estimate_end_date>to_date(to_char(sysdate+1,'yyyy/mm/dd'),'yyyy/mm/dd')
                and t.status not in ('Close','Cancel')
                and t.member_id='{0}'";

        strSql = string.Format(strSql, Request.QueryString["empno"].ToString());
        ds = DBUtil.GetDataset(strSql);
        gvAI.DataSource = ds;
        gvAI.DataBind();

        ////        //明日到期 Action Item
        ////        strSql = @"select t.task_id,t.ai_id,t.ai_desc, to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date,
        ////                to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date, t.member_name, t.status, t.progress,t1.task_desc
        ////                from tms_action_item t,tms_task t1
        ////                where t.task_id=t1.task_id and t.estimate_end_date=to_date(to_char(sysdate+1,'yyyy/mm/dd'),'yyyy/mm/dd')
        ////                and t.status not in ('Close','Cancel')
        ////                and t.member_id='TWN050555'";

        ////        ds = DBUtil.GetDataset(strSql);
        ////        gvTomorrowAI.DataSource = ds;
        ////        gvTomorrowAI.DataBind();

        ////        //逾期 Action Item
        ////        strSql = @"select t.task_id,t.ai_id,t.ai_desc, to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date,
        ////                to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date, t.member_name, t.status, t.progress,t1.task_desc
        ////                from tms_action_item t,tms_task t1
        ////                where t.task_id=t1.task_id and t.estimate_end_date<to_date(to_char(sysdate,'yyyy/mm/dd'),'yyyy/mm/dd')
        ////                and t.status not in ('Close','Cancel')
        ////                ";

        ////        ds = DBUtil.GetDataset(strSql);
        ////        gvDelayAI.DataSource = ds;
        ////        gvDelayAI.DataBind();      
    }
}
