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
using System.Drawing;

public partial class common_form_user_control_analysis_report : System.Web.UI.UserControl
{
    string strSql = "";
    dbutil DBUtil = new dbutil();
    DataSet ds = new DataSet();
    DateTime vStartDate, vEndDate;
    string vEmpno = string.Empty;
    string vDept = string.Empty;
    string vFab = string.Empty;
    string vAnalysisType = string.Empty;
    string vStatus = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (vAnalysisType == "People")
        {
            Title.Text = "此背景顏色顯示實際完成日在選擇區間內Close的AI";
        }
        else
        {

            if (vStatus == "'Close'")
            {
                Title.Text = "此背景顏色顯示在區間內所選擇的部門Close的AI";
            }
            else
            {
                Title.Text = "此背景顏色顯示在區間內所選擇的部門Receiving & Processing的AI";
            }
        }
    }

    public void getMyTask(string empno, string start_date, string end_date, string task_desc, string status, string task_type, string ai_expand, string analysis_type)
    {

        vStartDate = DateTime.ParseExact(start_date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        vEndDate = DateTime.ParseExact(end_date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        vEmpno = empno;
        vAnalysisType = analysis_type;
        vStatus = status;

        lblAIExpand.Text = ai_expand;
        lblAnalysisType.Text = analysis_type;

        strSql = "select distinct t.task_id, t.task_desc, t.applicant, t.applicant_dept, to_char(t.apply_date,'yyyy/mm/dd') apply_date, to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date, ";
        strSql += "to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date, t.priority, t.status, t.task_type, t.project_id, t.category_id, t.request_category,t2.project_name ";
        strSql += "from tms_task t,tms_member t1,tms_project t2,tms_action_item t3 where t.task_id=t1.task_id and t.task_id=t3.task_id(+) and t1.member_id = '" + empno + "' and t.project_id = t2.project_id(+) ";

        if (status == "CloseLast7Day")
        {
            //當選擇最近七日close 時連最近七日結案AI的task也要列出來
            strSql += "and ((t.status = 'Close' and t.actual_end_date between sysdate-7 and sysdate+1)  ";
            strSql += "or (t3.status = 'Close' and t3.actual_end_date between sysdate-7 and sysdate+1 and t3.member_id = '" + empno + "')) ";

            lblStatus.Text = status;
            lblEmpno.Text = empno;
        }
        else if (status == "CloseLastDay" && ai_expand == "N")
        {
            //分析報表by(人)-當選擇時間區間內close 時連最近結案的task要列出來
            string strTemp = string.Empty;
            strTemp += "and (t.status = 'Close' and t.actual_end_date between to_date('_fromDate','yyyymmdd') and to_date('_endDate','yyyymmdd')+1 and t1.member_id = '" + empno + "')";
            strSql += strTemp.Replace("_fromDate", start_date).Replace("_endDate", end_date);

        }
        else if (status == "CloseLastDay" && ai_expand == "Y")
        {
            //分析報表by(人)-當選擇時間區間內close 時連最近結案的task要列出來
            string strTemp = string.Empty;
            strTemp += "and (t3.status = 'Close' and t3.actual_end_date between to_date('_fromDate','yyyymmdd') and to_date('_endDate','yyyymmdd')+1 and t1.member_id = '" + empno + "')";
            strSql += strTemp.Replace("_fromDate", start_date).Replace("_endDate", end_date);

        }
        else
        {
            if (start_date != "")
                strSql += "and t.apply_date >= to_date('" + start_date + "','yyyy/mm/dd') ";
            if (end_date != "")
                strSql += "and t.apply_date <= to_date('" + end_date + "','yyyy/mm/dd') + 1 ";
            //if (status != "")
            //{
            if (status == "Receiving & Processing")
                strSql += "and t.status in ('Receiving','Processing') ";
            else if (status == "Delay")
                strSql += "and t.estimate_end_date+1 < sysdate and t.status in ('Receiving','Processing') ";
            else
                strSql += "and t.status = '" + status + "' ";
            //}

            lblStatus.Text = status;
            lblEmpno.Text = empno;
        }

        if (task_desc != "")
            strSql += "and upper(t.task_desc) like '%" + task_desc.ToUpper() + "%' ";
        if (task_type != "")
            strSql += "and t.task_type in (" + task_type + ") ";
        //else
        //    strSql += "and t.task_type in ('') ";
        //當專案cancel時,可是task還是processing,這樣的task不應該在查詢task的狀態是processing被查出來
        strSql += "and nvl(t2.status,'N/A') not in ('Cancel')";
        strSql += "order by t.project_id,estimate_end_date,estimate_start_date";
        lblSql.Text = strSql;

        ds = DBUtil.GetDataset(strSql);
        gvTask.DataSource = ds;
        gvTask.DataBind();
        //將sql的資料存起來,當gridview換頁的時候重抓資料要使用


    }


    public void getDeptTask(string dept, string empno, string start_date, string end_date, string task_desc, string status, string task_type, string ai_expand, string fab, string analysis_type, string year, string month)
    {
        lblAIExpand.Text = ai_expand;
        lblAnalysisType.Text = analysis_type;
        lblYear.Text = year;
        lblMonth.Text = month;
        vDept = dept.Replace("'", "");
        vFab = fab;
        vAnalysisType = analysis_type;
        vStatus = status;
        string sYM = year + month;
        lblYearMonth.Text = sYM;

        strSql = "select distinct t.task_id, t.task_desc, t.applicant, t.applicant_dept, to_char(t.apply_date,'yyyy/mm/dd') apply_date, to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date, ";
        strSql += "to_char(t.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(t.actual_end_date,'yyyy/mm/dd') actual_end_date, t.priority, t.status, t.task_type, t.project_id, t.category_id, t.request_category,t3.project_name ";
        strSql += "from tms_task t,tms_member t1,tms_empinfo t2,tms_project t3,tms_action_item t4 where t.task_id=t1.task_id and t1.member_id=t2.empno and t1.member_name=t4.member_name and t.project_id = t3.project_id(+) and t.task_id=t4.task_id(+)";
        if (dept != "")
            strSql += "and t2.dept in (" + dept + ") ";
        else
            strSql += "and t2.dept in (select t.authority_dept from tms_authority_dept t where t.dept = '" + Session["user_dept"].ToString() + "') ";
        if (empno != "")
            strSql += "and t1.member_id in (" + empno + ") ";

        if (status == "CloseLast7Day")
        {
            //當選擇最近七日close 時連最近七日結案AI的task也要列出來
            strSql += "and ((t.status = 'Close' and t.actual_end_date between sysdate-7 and sysdate+1)  ";
            if (empno != "")
                strSql += "or (t4.status = 'Close' and t4.actual_end_date between sysdate-7 and sysdate+1 and t4.member_id in (" + empno + "))) ";
            else
                strSql += "or (t4.status = 'Close' and t4.actual_end_date between sysdate-7 and sysdate+1 )) ";

            lblStatus.Text = status;
            lblEmpno.Text = empno;
            lblDept.Text = dept;
        }
        //分析報表by地 判斷所選擇的status不同，其所用的時間區間條件也不同
        else if (status == "'Close'")
        {
            if (sYM.Length > 4)
                strSql += "and to_char(t4.actual_end_date, 'yyyymm') = " + "'" + sYM + "' and t4.status = 'Close'";
            else
                strSql += "and to_char(t4.actual_end_date, 'yyyy') = " + "'" + year + "' and t4.status = 'Close'";
        
        }
        //分析報表by地 判斷所選擇的status不同，其所用的時間區間條件也不同
        else if (status == "'Receiving','Processing'")
        {
            if (sYM.Length > 4)
                strSql += "and to_char(t4.estimate_end_date, 'yyyymm') = " + "'" + sYM + "' and t4.status in ('Receiving','Processing')";
            else
                strSql += "and to_char(t4.estimate_end_date, 'yyyy') = " + "'" + year + "' and t4.status in ('Receiving','Processing')";

        }
        else
        {
            if (start_date != "")
                strSql += "and t.apply_date >= to_date('" + start_date + "','yyyy/mm/dd') ";
            if (end_date != "")
                strSql += "and t.apply_date <= to_date('" + end_date + "','yyyy/mm/dd')+1 ";
            //if (status != "")
            //{
            if (status == "Receiving & Processing")
                strSql += "and t.status in ('Receiving','Processing') ";
            else if (status == "All")
                strSql += "";
            else if (status == "Delay")
                strSql += "and t.estimate_end_date+1 < sysdate and t.status in ('Receiving','Processing') ";
            else
                strSql += "and t4.status in (" + status + ") ";
            //}

            lblStatus.Text = status;
            lblEmpno.Text = empno;
            lblDept.Text = dept;
        }

        //if (start_date != "")
        //    strSql += "and t.apply_date between to_date('" + start_date + "','yyyy/mm/dd') ";
        //if (end_date != "")
        //    strSql += "and to_date('" + end_date + "','yyyy/mm/dd')+1 ";
        //if (status != "")
        //    strSql += "and t.status in (" + status + ") ";

        if (task_desc != "")
            strSql += "and upper(t.task_desc) like '%" + task_desc.ToUpper() + "%' ";

        if (task_type != "")
            strSql += "and t.task_type in (" + task_type + ") ";

        if (fab != "")
            strSql += "and t4.fab_area = '" + fab + "'";
        //if (task_filedesc != "")
        //    strSql += "and exists (select 'X' from tms_attachment a where a.task_id = t.task_id and upper(a.file_desc) like '%" + task_filedesc.ToUpper() + "%')";

        //if (task_comment != "")
        //    strSql += "and exists (select 'X' from tms_process_history a where a.task_id = t.task_id and upper(a.process_comment) like '%" + task_comment.ToUpper() + "%')";
        //else
        //    strSql += "and t.task_type in ('') ";
        //當專案cancel時,可是task還是processing,這樣的task不應該在查詢task的狀態是processing被查出來
        strSql += "and nvl(t3.status,'N/A') not in ('Cancel')";
        strSql += "and t2.empno not in (select manager_id from tms_dept_manager)";
        strSql += "order by t.project_id,estimate_end_date,estimate_start_date ";
        lblSql.Text = strSql;

        ds = DBUtil.GetDataset(strSql);
        gvTask.DataSource = ds;
        gvTask.DataBind();
        //將sql的資料存起來,當gridview換頁的時候重抓資料要使用

    }




    public void bindGV(string strSqlTemp)
    {
        strSql = strSqlTemp;

        ds = DBUtil.GetDataset(strSql);
        gvTask.DataSource = ds;
        gvTask.DataBind();

    }

    protected void gvTask_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet ds = new DataSet();

            //DateTime estimate_end_date = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["estimate_end_date"].ToString());
            //if (((DataRowView)e.Row.DataItem)["status"].ToString() == "Receiving" || ((DataRowView)e.Row.DataItem)["status"].ToString() == "Processing")
            //{
            //    if (dsutil.DateDiff("day", estimate_end_date, DateTime.Now) > 1)
            //    {
            //        //過期變顏色
            //        e.Row.BackColor = Color.Gold;
            //    }
            //}

            strSql = "select distinct member_name,is_owner from tms_member where task_id = '" + ((DataRowView)e.Row.DataItem)["task_id"] + "' order by is_owner ";
            ds = DBUtil.GetDataset(strSql);
            ((DataList)e.Row.FindControl("dlMember")).DataSource = ds;
            ((DataList)e.Row.FindControl("dlMember")).DataBind();

            ds.Clear();
            strSql = "select rownum rn,t.* from (select a.task_id,a.ai_id,a.ai_desc, to_char(a.actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(a.actual_end_date,'yyyy/mm/dd') actual_end_date,to_char(a.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(a.estimate_end_date,'yyyy/mm/dd') estimate_end_date, a.member_id,a.member_name, a.status, a.progress, a.ai_hour, a.fab_area,b.dept from tms_action_item a,tms_empinfo b where a.member_id=b.empno and a.task_id = '" + ((DataRowView)e.Row.DataItem)["task_id"] + "' order by a.estimate_start_date,a.estimate_end_date) t";
            //strSql = "select t.*,rownum rn from tms_action_item t where task_id = '" + ((DataRowView)e.Row.DataItem)["task_id"] + "' ";
            ds = DBUtil.GetDataset(strSql);

            gvAI.DataSource = ds;
            gvAI.DataBind();

            if (ds.Tables[0].Rows.Count == 0)
            {
                System.Web.UI.WebControls.Image btnShowDetail = new System.Web.UI.WebControls.Image();
                btnShowDetail = (System.Web.UI.WebControls.Image)e.Row.FindControl("btnShowDetail");
                btnShowDetail.Visible = false;
            }
            else
            {
                //*********************************************************
                //新增一個新的GridViewRow
                #region
                GridViewRow r = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                r.Cells.Add(new TableCell());
                r.Cells.Add(new TableCell());

                r.Cells[1].ColumnSpan = gvTask.Columns.Count - 1;

                gvAI.Visible = true;
                gvAI.RenderControl(hw);
                gvAI.Visible = false;

                r.Cells[1].Text = sw.ToString();
                sw.Close();

                r.ID = "Detail_" + e.Row.RowIndex.ToString();

                r.HorizontalAlign = HorizontalAlign.Left;
                e.Row.Parent.Controls.Add(r);

                System.Web.UI.WebControls.Image btnShowDetail = new System.Web.UI.WebControls.Image();
                btnShowDetail = (System.Web.UI.WebControls.Image)e.Row.FindControl("btnShowDetail");
                //btnShowDetail.Attributes.Add("onclick", "showHideAnswer('task_list1_gvTask_" + r.ID + "','" + e.Row.ClientID.ToString() + "_" + btnShowDetail.ID + "');");
                btnShowDetail.Attributes.Add("onclick", "showHideAnswer('" + this.ClientID.ToString() + "_gvTask_" + r.ID + "','" + e.Row.ClientID.ToString() + "_" + btnShowDetail.ID + "');");

                if (lblAIExpand.Text == "Y")
                {
                    r.Style["display"] = "block";
                    btnShowDetail.ImageUrl = "~/images/close13.gif";
                }
                else
                {
                    r.Style["display"] = "none";
                    btnShowDetail.ImageUrl = "~/images/open13.gif";
                }


                #endregion
                //*********************************************************
            }

        }
    }

    protected void gvTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTask.PageIndex = e.NewPageIndex;
        ds = DBUtil.GetDataset(lblSql.Text);
        gvTask.DataSource = ds;
        gvTask.DataBind();
    }

    protected void gvAI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DateTime estimate_end_date = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["estimate_end_date"].ToString());
            //if (((DataRowView)e.Row.DataItem)["status"].ToString() == "Receiving" || ((DataRowView)e.Row.DataItem)["status"].ToString() == "Processing")
            //{
            //    if (dsutil.DateDiff("day", estimate_end_date, DateTime.Now) > 1)
            //    {
            //        //過期變顏色
            //        e.Row.BackColor = Color.Gold;
            //    }
            //}

            //最近七日結案的AI要hightlight出來
            //if (lblStatus.Text == "CloseLast7Day")
            //{

            string strStatus = ((DataRowView)e.Row.DataItem)["status"].ToString();

            if (lblAnalysisType.Text == "People")
            {


                if (strStatus == "Close")
                {
                    DateTime actual_end_date = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["actual_end_date"].ToString());
                    /*if (dsutil.DateDiff("day", actual_end_date, DateTime.Now) >= 0 && dsutil.DateDiff("day", actual_end_date, DateTime.Now) <= 7)
                    {
                        if (lblEmpno.Text != "")
                        {
                            if (lblEmpno.Text.IndexOf(((DataRowView)e.Row.DataItem)["member_id"].ToString()) >= 0)
                                //過期變顏色
                                e.Row.BackColor = Color.LightGreen;
                        }
                        else if (lblDept.Text != "")
                        {
                            if (lblDept.Text.IndexOf(((DataRowView)e.Row.DataItem)["dept"].ToString()) >= 0)
                                //過期變顏色
                                e.Row.BackColor = Color.LightGreen;
                        }
                        else
                            e.Row.BackColor = Color.LightGreen;

                    }*/

                    if (((DataRowView)e.Row.DataItem)["member_id"].ToString() == vEmpno && (actual_end_date >= vStartDate && actual_end_date <= vEndDate) && strStatus == "Close")
                        e.Row.BackColor = Color.LightGreen;
                }
            }

            else
            {
               
                //if (((DataRowView)e.Row.DataItem)["fab_area"].ToString() == vFab && ((DataRowView)e.Row.DataItem)["dept"].ToString() == vDept && (vStatus.IndexOf(strStatus)) > 0)
                //    e.Row.BackColor = Color.LightGreen;

                if (strStatus == "Close")
                {
                    if (((DataRowView)e.Row.DataItem)["fab_area"].ToString() == vFab && ((DataRowView)e.Row.DataItem)["dept"].ToString() == vDept && vStatus == "'Close'")
                    {
                        DateTime actual_end_date = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["actual_end_date"].ToString());
                        if (lblYearMonth.Text.Length <= 4)
                        {
                            if (actual_end_date.ToString("yyyy") == lblYear.Text.ToString())
                                e.Row.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            if (actual_end_date.ToString("yyyyMM") == lblYearMonth.Text.ToString() )
                                e.Row.BackColor = Color.LightGreen;
                        }
                    }
                }
                else if (((DataRowView)e.Row.DataItem)["fab_area"].ToString() == vFab && ((DataRowView)e.Row.DataItem)["dept"].ToString() == vDept && vStatus == "'Receiving','Processing'")
                {
                    if (strStatus == "Receiving" || strStatus == "Processing")
                    {
                        DateTime estimate_end_date = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["estimate_end_date"].ToString());
                        if (lblYearMonth.Text.Length <= 4)
                        {
                            if (estimate_end_date.ToString("yyyy") == lblYear.Text.ToString())
                                e.Row.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            if (estimate_end_date.ToString("yyyyMM") == lblYearMonth.Text.ToString())
                                e.Row.BackColor = Color.LightGreen;
                        }
                    }
                }

            }


        }
    }

    public Boolean display(string is_owner)
    {
        if (is_owner == "Y")
            return true;
        else
            return false;
    }
}
