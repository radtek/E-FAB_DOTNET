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
using System.Data.OracleClient;
using System.IO;
using Innolux.Portal.EntLibBlock.DataAccess;

public partial class gridview : System.Web.UI.Page
{
    dbutil DBUtil = new dbutil();
    DataView dv = new DataView();
    string project_id = string.Empty;
    string strSql;
    DataSet ds = new DataSet();
    DataSet dsAttach = new DataSet();

    protected DbAccessHelper m_objDB = new DbAccessHelper("TMSDB");

    protected void Page_Load(object sender, EventArgs e)
    {
        
            project_id = "134";
            lblProjectNo.Text = project_id;
            Session["user_name"] = "林志湳";
            Session["user_dept"] = "TFTPIA/CIM1/MI";

            if (!IsPostBack)
            {
                setProjectGroup();
                setProject();
                showDetail();
            }

  
    }

    private void showDetail()
    {
        btnShowDetail1.Attributes.Add("onclick", "showHideAnswer('" + gvTask.ClientID.ToString() + "','" + btnShowDetail1.ClientID + "');");
        btnShowDetail2.Attributes.Add("onclick", "showHideAnswer('" + dlProcessHistory.ClientID.ToString() + "','" + btnShowDetail2.ClientID + "');");
    }

    private void setProject()
    {

        DataSet ds = new DataSet();

        strSql = "select project_id, project_desc,project_name, applicant, applicant_dept, to_char(apply_date,'yyyy/mm/dd') apply_date, to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date, ";
        strSql += "to_char(actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(actual_end_date,'yyyy/mm/dd') actual_end_date, priority, status,project_group_id,project_price ";
        strSql += "from tms_project t ";
        strSql += "where project_id = " + project_id;
        IDataReader dr = m_objDB.ExecuteReader(strSql);

        while (dr.Read())
        {
            lblProjectNo.Text = dr["project_id"].ToString();
            lblProjectName.Text = dr["project_name"].ToString();
            ddlStatus.SelectedValue = dr["status"].ToString();
            lblOriginalStatus.Text = dr["status"].ToString();
            txtProjectDesc.Text = dr["project_desc"].ToString();
            lblAppilcant.Text = dr["applicant"].ToString();
            lblAppilcantDept.Text = dr["applicant_dept"].ToString();
            lblApplyDate.Text = dr["apply_date"].ToString();
            txtEstimateStartDate.SelectedDate = Convert.ToDateTime(dr["Estimate_Start_Date"].ToString());
            txtEstimateEndDate.SelectedDate = Convert.ToDateTime(dr["Estimate_End_Date"].ToString());
            lblActualStartDate.Text = dr["Actual_Start_Date"].ToString();
            lblActualEndDate.Text = dr["Actual_End_Date"].ToString();
            ddlPiority.SelectedValue = dr["PRIORITY"].ToString();
            ddlProjectGroup.SelectedValue = dr["project_group_id"].ToString();
            txtPrice.Text = dr["project_price"].ToString();
        }

        ////附件
        strSql = "select * from tms_attachment where project_id = '134' ";
        //strSql = "select * from tms_attachment where task_id = '101'";
        dsAttach = m_objDB.ExecuteDataSet(strSql);
        DataView dvAttach = dsAttach.Tables[0].DefaultView;
        //dv.RowFilter = "ai_id is null ";
        dlAttach.DataSource = dvAttach;
        dlAttach.DataBind();

        //process history
        ds.Clear();
        strSql = "select replace(replace(process_comment,'\n','<br>'),' ','&nbsp;') process_comment,to_char(create_dttm,'yyyy/mm/dd hh24:mi:ss') create_dttm,create_user from tms_process_history where project_id = '134' order by create_dttm desc";
        //strSql = "select replace(process_comment,'\r\n','<br>') process_comment,to_char(create_dttm,'yyyy/mm/dd hh24:mi:ss') create_dttm,create_user from tms_process_history where task_id = '101' order by create_dttm desc";
        ds = m_objDB.ExecuteDataSet(strSql);
        dlProcessHistory.DataSource = ds;
        dlProcessHistory.DataBind();

        //get task member
        ds.Clear();
        strSql = "select a.task_id,b.member_name,b.is_owner from tms_task a , tms_member b where a.project_id = " + project_id + " and a.task_id = b.task_id ";
        ds = m_objDB.ExecuteDataSet(strSql);
        dv = ds.Tables[0].DefaultView;

        //project task
        strSql = "select rownum rn,t.* from ( ";
        strSql += "select task_id,task_desc, to_char(estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(estimate_end_date,'yyyy/mm/dd') estimate_end_date, status, ";
        strSql += "to_char(actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(actual_end_date,'yyyy/mm/dd') actual_end_date ";
        strSql += "from tms_task where project_id = '" + project_id + "' order by estimate_end_date,estimate_start_date) t";
        ds = m_objDB.ExecuteDataSet(strSql);
        gvTask.DataSource = ds;
        gvTask.DataBind();

        //initField();
    }

    private void setProjectGroup()
    {
        strSql = @"select distinct t.project_id,t.project_group from tms_project_group t 
        where t.project_dept in (select t1.authority_dept from tms_authority_dept t1 where t1.dept = '{0}' ) order by t.project_id ";

        strSql = string.Format(strSql, Session["user_dept"].ToString());

        ddlProjectGroup.DataTextField = "project_group";
        ddlProjectGroup.DataValueField = "project_id";
        ddlProjectGroup.DataSource = m_objDB.ExecuteDataSet(strSql);
        ddlProjectGroup.DataBind();
        ddlProjectGroup.Items.Insert(0, "");

    }

    protected void gvTask_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString();
            dv.RowFilter = "task_id=" + strTaskID;
            dv.Sort = "is_owner desc";

            //task member datalist
            ((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv;
            ((DataList)e.Row.FindControl("dlTaskMember")).DataBind();

            //image link to task content

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID);
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}";

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
    }

   
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Innolux.Portal.Report.ReportExcel excel = new Innolux.Portal.Report.ReportExcel(new string[] { "Project Schedule" });

        int top = 1;
        strSql = @"select t.project_name project,to_char(t.estimate_start_date,'yyyy/mm/dd') 預計開始日期,to_char(t.estimate_end_date,'yyyy/mm/dd') 預計完成日期,
            to_char(t.actual_start_date,'yyyy/mm/dd') 實際開始日期,to_char(t.actual_end_date,'yyyy/mm/dd') 實際完成日期,
            t.applicant owner,t.status,t1.project_group from tms_project t,tms_project_group t1 
            where t.project_group_id=t1.project_id(+) and t.project_id = {0} ";
        strSql = string.Format(strSql, lblProjectNo.Text);
        DataTable dtProject = m_objDB.ExecuteDataSet(strSql).Tables[0];
        excel.SetText(excel.Sheets[0], "▼Project", top, 1, top, dtProject.Columns.Count);
        top++;
        excel.ImportDataTable(excel.Sheets[0], dtProject, top, new int[] { 1 });
        excel.Sheets[0].Range[top + 1, 2, top + 1 + dtProject.Rows.Count, 5].NumberFormat = "yyyy/mm/dd";

        top += dtProject.Rows.Count + 2;

        /*excel.SetText(excel.Sheets[0], "Project Name: " + dtProject.Rows[0]["project"].ToString(), 1, 1, 1, 1);
        excel.SetText(excel.Sheets[0], "Project Group: " + dtProject.Rows[0]["project_group"].ToString(), 1, 2, 1, 2);
        excel.SetText(excel.Sheets[0], "預計開始日期: " + dtProject.Rows[0]["estimate_start_date"].ToString(), 2, 1, 2, 1);
        excel.SetText(excel.Sheets[0], "預計完成日期: " + dtProject.Rows[0]["estimate_end_date"].ToString(), 2, 2, 2, 2);
        excel.SetText(excel.Sheets[0], "實際開始日期: " + dtProject.Rows[0]["actual_start_date"].ToString(), 3, 1, 3, 1);
        excel.SetText(excel.Sheets[0], "實際完成日期: " + dtProject.Rows[0]["actual_end_date"].ToString(), 3, 2, 3, 2);
        excel.SetText(excel.Sheets[0], "Owner: " + dtProject.Rows[0]["applicant"].ToString(), 4, 1, 4, 1);
        excel.SetText(excel.Sheets[0], "Status: " + dtProject.Rows[0]["status"].ToString(), 4, 2, 4, 2);
        */

        strSql = @"select t.task_desc Task,to_char(t.estimate_start_date,'yyyy/mm/dd') 預計開始日期,to_char(t.estimate_end_date,'yyyy/mm/dd') 預計完成日期,
            to_char(t.actual_start_date,'yyyy/mm/dd') 實際開始日期,to_char(t.actual_end_date,'yyyy/mm/dd') 實際完成日期,
            t.status from tms_task t where t.project_id={0} order by t.estimate_end_date,t.task_id";

        strSql = string.Format(strSql, lblProjectNo.Text);
        DataTable dtTask = m_objDB.ExecuteDataSet(strSql).Tables[0];
        excel.SetText(excel.Sheets[0], "▼Task", top, 1, top, dtTask.Columns.Count);
        top++;
        excel.ImportDataTable(excel.Sheets[0], dtTask, top, new int[] { 1 });
        excel.Sheets[0].Range[top + 1, 2, top + 1 + dtTask.Rows.Count, 5].NumberFormat = "yyyy/mm/dd";

        top += dtTask.Rows.Count + 2;

        strSql = @"select t.task_desc Task,t1.ai_desc AI,to_char(t1.estimate_start_date,'yyyy/mm/dd') 預計開始日期,to_char(t1.estimate_end_date,'yyyy/mm/dd') 預計完成日期,
            to_char(t1.actual_start_date,'yyyy/mm/dd') 實際開始日期,to_char(t1.actual_end_date,'yyyy/mm/dd') 實際完成日期,t1.member_name owner,t1.status,t1.progress|| '%' 進度,t1.ai_hour 工時_Hr
            from tms_task t,tms_action_item t1 where t.task_id=t1.task_id and t.project_id={0} order by t.estimate_end_date,t.task_id";

        strSql = string.Format(strSql, lblProjectNo.Text);
        DataTable dtAI = m_objDB.ExecuteDataSet(strSql).Tables[0];
        excel.SetText(excel.Sheets[0], "▼Action Item", top, 1, top, dtAI.Columns.Count);
        top++;
        excel.ImportDataTable(excel.Sheets[0], dtAI, top, new int[] { 1, 3, 4 });
        excel.Sheets[0].Range[top + 1, 3, top + 1 + dtAI.Rows.Count, 6].NumberFormat = "yyyy/mm/dd";



        excel.ToExcel("Project_" + DateTime.Now.ToString("yyyyMMdd"), Innolux.Portal.Report.DownloadExcelType.PromptDialog);
    }

    public Boolean display(string is_owner)
    {
        if (is_owner == "Y")
            return true;
        else
            return false;
    }
    protected void gvTask_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
