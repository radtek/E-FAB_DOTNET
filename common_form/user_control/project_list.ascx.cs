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
using System.Drawing;

public partial class common_form_user_control_project_list : System.Web.UI.UserControl
{
    public string strSqlTemp = string.Empty;
    dbutil DBUtil = new dbutil();
    DataSet ds = new DataSet();
    string strSql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Task_list1.Visible = false;
    }

    //public void bindGV(string strSql)
    //{
    //    lblSql.Text = strSql;

    //    ds = DBUtil.GetDataset(lblSql.Text);
    //    GridView1.DataSource = ds;
    //    GridView1.DataBind();

    //}

    public void getMyProject(string start_date,string end_date,string project_name,string project_desc,string status,string applicant,string project_group)
    {
        strSql = "select a.*,rownum rn from ( ";
        strSql += "select t.project_id , t.project_name , t.applicant, t.apply_date , to_char(t.estimate_start_date,'YYYY/MM/DD') estimate_start_date , to_char(t.estimate_end_date,'YYYY/MM/DD') estimate_end_date , to_char(t.actual_start_date,'YYYY/MM/DD') actual_start_date, to_char(t.actual_end_date,'YYYY/MM/DD') actual_end_date , t.priority, t.status,x.ai_total,x.ai_processing,x.ai_hour,x.ai_unclose_ratio,t1.project_group ";
        strSql += " from tms_project t,tms_project_group t1, ";
        strSql += "(select t2.*,decode(t2.ai_total,0,0,round(((t2.ai_total-t2.ai_processing)/t2.ai_total),4)*100) ai_unclose_ratio from ( ";
        strSql += "select t.project_id,count(*) ai_total, ";
        strSql += "sum(decode(t1.status,'Receiving',1,'Processing',1,'Pending',1,0)) ai_processing, ";
        strSql += "sum(t1.ai_hour) ai_hour ";
        strSql += "from tms_task t,tms_action_item t1  ";
        strSql += "where t.task_id=t1.task_id  ";
        strSql += "and nvl(t1.status,'N/A') <> 'Cancel' ";
        strSql += "group by t.project_id) t2) x ";
        strSql += "where t.project_id=x.project_id(+) and t.project_group_id = t1.project_id(+) and t.applicant ='" + applicant + "' ";

        if (status == "CloseLast7Day")
        {
            strSql += "and t.actual_end_date between sysdate-6 and sysdate+1 ";
            strSql += "and t.status = 'Close' ";
        }
        else
        {
            if (start_date != "")
                strSql += " and t.apply_date >= to_date('" + start_date + "','yyyy/mm/dd')";
            if (end_date != "")
                strSql += " and t.apply_date <= to_date('" + end_date + "','yyyy/mm/dd') +1 ";

            if (status == "All")
                strSql += "";
            else if (status == "Delay")
                strSql += " and t.estimate_end_date+1 < sysdate and t.status in ('Receiving','Processing')";
            else
                strSql += " and t.status = '" + status + "'";
        }

        if (project_name != "")
            strSql += " and upper(t.project_name) like '%" + project_name.ToUpper() + "%'";
        if (project_desc != "")
            strSql += " and upper(t.project_desc) like '%" + project_desc.ToUpper() + "%'";
        if (project_group != "")
            strSql += " and t.project_group_id = " + project_group ;

        strSql += " order by t1.project_group,t.estimate_end_date,t.estimate_start_date ) a ";
        //為了下一頁使用
        lblSql.Text = strSql;

        ds = DBUtil.GetDataset(lblSql.Text);
        GridView1.DataSource = ds;
        GridView1.DataBind();

    }

    public void getDeptProject(string start_date, string end_date, string project_name, string project_desc, string status, string applicant_dept, string applicant, string project_group)
    {
        strSql = "select a.*,rownum rn from ( ";
        strSql += "select t.project_id , t.project_name , t.applicant, t.apply_date , to_char(t.estimate_start_date,'YYYY/MM/DD') estimate_start_date , to_char(t.estimate_end_date,'YYYY/MM/DD') estimate_end_date , to_char(t.actual_start_date,'YYYY/MM/DD') actual_start_date, to_char(t.actual_end_date,'YYYY/MM/DD') actual_end_date , t.priority, t.status,x.ai_total,x.ai_processing,x.ai_hour,x.ai_unclose_ratio,t1.project_group  ";
        strSql += " from tms_project t,tms_project_group t1, ";
        strSql += "(select t2.*,decode(t2.ai_total,0,0,round(((t2.ai_total-t2.ai_processing)/t2.ai_total),4)*100) ai_unclose_ratio from ( ";
        strSql += "select t.project_id,count(*) ai_total, ";
        strSql += "sum(decode(t1.status,'Receiving',1,'Processing',1,'Pending',1,0)) ai_processing, ";
        strSql += "sum(t1.ai_hour) ai_hour ";
        strSql += "from tms_task t,tms_action_item t1  ";
        strSql += "where t.task_id=t1.task_id  ";
        strSql += "and nvl(t1.status,'N/A') <> 'Cancel' ";
        strSql += "group by t.project_id) t2) x where t.project_id=x.project_id(+) and t.project_group_id = t1.project_id(+) ";

        if (status != "")
        {
            if (status == "CloseLast7Day")
            {
                strSql += "and t.actual_end_date between sysdate-6 and sysdate+1 ";
                strSql += "and t.status = 'Close' ";
            }
            else
            {
                if (start_date != "")
                    strSql += " and t.apply_date >= to_date('" + start_date + "','yyyy/mm/dd')";
                if (end_date != "")
                    strSql += " and t.apply_date <= to_date('" + end_date + "','yyyy/mm/dd') +1 ";

                if (status == "All")
                    strSql += "";
                else if (status == "Delay")
                    strSql += " and t.estimate_end_date+1 < sysdate and t.status in ('Receiving','Processing')";
                else
                    strSql += " and t.status = '" + status + "'";
            }
        }

        if (project_name != "")
            strSql += " and upper(t.project_name) like '%" + project_name.ToUpper() + "%'";
        if (project_desc != "")
            strSql += " and upper(t.project_desc) like '%" + project_desc.ToUpper() + "%'";

        if (applicant_dept != "")
            strSql += " and t.applicant_dept in (" + applicant_dept + ")";
        else
            strSql += "and t.applicant_dept in (select t.authority_dept from tms_authority_dept t where t.dept = '" + Session["user_dept"].ToString() + "') ";

        if (applicant != "")
            strSql += " and t.applicant in (" + applicant + ")";
        if (project_group != "")
            strSql += " and t.project_group_id = " + project_group;

        strSql += " order by t1.project_group,t.estimate_end_date,t.estimate_start_date ) a";
        //為了下一頁使用
        lblSql.Text = strSql;

        ds = DBUtil.GetDataset(lblSql.Text);
        GridView1.DataSource = ds;
        GridView1.DataBind();

    }

    public void getCalendarProject(string strSelectedDate)
    {
        strSql = "select a.*,rownum rn from ( ";
        strSql += "select t.project_id , t.project_name , t.applicant, t.apply_date , to_char(t.estimate_start_date,'YYYY/MM/DD') estimate_start_date , to_char(t.estimate_end_date,'YYYY/MM/DD') estimate_end_date , to_char(t.actual_start_date,'YYYY/MM/DD') actual_start_date, to_char(t.actual_end_date,'YYYY/MM/DD') actual_end_date , t.priority, t.status,x.ai_total,x.ai_processing,x.ai_hour,x.ai_unclose_ratio,t1.project_group  ";
        strSql += " from tms_project t,tms_project_group t1, ";
        strSql += "(select t2.*,decode(t2.ai_total,0,0,round(((t2.ai_total-t2.ai_processing)/t2.ai_total),4)*100) ai_unclose_ratio from ( ";
        strSql += "select t.project_id,count(*) ai_total, ";
        strSql += "sum(decode(t1.status,'Receiving',1,'Processing',1,'Pending',1,0)) ai_processing, ";
        strSql += "sum(t1.ai_hour) ai_hour ";
        strSql += "from tms_task t,tms_action_item t1  ";
        strSql += "where t.task_id=t1.task_id  ";
        strSql += "and nvl(t1.status,'N/A') <> 'Cancel' ";
        strSql += "group by t.project_id) t2) x where t.project_id=x.project_id(+) and t.project_group_id = t1.project_id(+) ";
        strSql += "and to_date('" + strSelectedDate + "','yyyy/mm/dd') between t.estimate_start_date and t.estimate_end_date and nvl(t.status,'N/A') not in ('Cancel') ) a";

        lblSql.Text = strSql;

        ds = DBUtil.GetDataset(lblSql.Text);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        ds = DBUtil.GetDataset(lblSql.Text);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime estimate_end_date = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["estimate_end_date"].ToString());
            if (((DataRowView)e.Row.DataItem)["status"].ToString() == "Receiving" || ((DataRowView)e.Row.DataItem)["status"].ToString() == "Processing")
            {
                if (dsutil.DateDiff("day", estimate_end_date, DateTime.Now) > 1)
                {
                    //過期變顏色
                    e.Row.BackColor = Color.Gold;
                }
            }
        }
    }
}
