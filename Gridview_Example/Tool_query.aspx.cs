﻿using System;
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

public partial class Gridview_Example_Tool_query : System.Web.UI.Page
{
    dbutil DBUtil = new dbutil();
    //DataView dv = new DataView();
    string project_id = string.Empty;
    string strSql;
    DataSet ds = new DataSet();
    DataSet dsAttach = new DataSet();

    protected DbAccessHelper m_objDB = new DbAccessHelper("TMSDB");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //            string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"];
            //            string sql_MODEL_NAME = " select * from tlms_main ttt " +

            //" where ttt.tool_id like '%M190%' ";

            //           DataSet ds_abc=new DataSet();
            //            ds_abc=func.get_dataSet_access(sql_MODEL_NAME,conn);
            //            gvTask.DataSource = ds_abc.Tables[0];
            //            gvTask.DataBind();

        }


        //project_id = "134";
        //lblProjectNo.Text = project_id;
        //Session["user_name"] = "林志湳";
        //Session["user_dept"] = "TFTPIA/CIM1/MI";

        //if (!IsPostBack)
        //{
        //    setProjectGroup();
        //    setProject();
        //    showDetail();
        //}


    }

    //private void showDetail()
    //{
    //    btnShowDetail1.Attributes.Add("onclick", "showHideAnswer('" + gvTask.ClientID.ToString() + "','" + btnShowDetail1.ClientID + "');");
    //    btnShowDetail2.Attributes.Add("onclick", "showHideAnswer('" + dlProcessHistory.ClientID.ToString() + "','" + btnShowDetail2.ClientID + "');");
    //}

    //private void setProject()
    //{

    //    DataSet ds = new DataSet();

    //    strSql = "select project_id, project_desc,project_name, applicant, applicant_dept, to_char(apply_date,'yyyy/mm/dd') apply_date, to_char(t.estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(t.estimate_end_date,'yyyy/mm/dd') estimate_end_date, ";
    //    strSql += "to_char(actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(actual_end_date,'yyyy/mm/dd') actual_end_date, priority, status,project_group_id,project_price ";
    //    strSql += "from tms_project t ";
    //    strSql += "where project_id = " + project_id;
    //    IDataReader dr = m_objDB.ExecuteReader(strSql);

    //    while (dr.Read())
    //    {
    //        lblProjectNo.Text = dr["project_id"].ToString();
    //        lblProjectName.Text = dr["project_name"].ToString();
    //        ddlStatus.SelectedValue = dr["status"].ToString();
    //        lblOriginalStatus.Text = dr["status"].ToString();
    //        txtProjectDesc.Text = dr["project_desc"].ToString();
    //        lblAppilcant.Text = dr["applicant"].ToString();
    //        lblAppilcantDept.Text = dr["applicant_dept"].ToString();
    //        lblApplyDate.Text = dr["apply_date"].ToString();
    //        txtEstimateStartDate.SelectedDate = Convert.ToDateTime(dr["Estimate_Start_Date"].ToString());
    //        txtEstimateEndDate.SelectedDate = Convert.ToDateTime(dr["Estimate_End_Date"].ToString());
    //        lblActualStartDate.Text = dr["Actual_Start_Date"].ToString();
    //        lblActualEndDate.Text = dr["Actual_End_Date"].ToString();
    //        ddlPiority.SelectedValue = dr["PRIORITY"].ToString();
    //        ddlProjectGroup.SelectedValue = dr["project_group_id"].ToString();
    //        txtPrice.Text = dr["project_price"].ToString();
    //    }

    //    ////附件
    //    strSql = "select * from tms_attachment where project_id = '134' ";
    //    //strSql = "select * from tms_attachment where task_id = '101'";
    //    dsAttach = m_objDB.ExecuteDataSet(strSql);
    //    DataView dvAttach = dsAttach.Tables[0].DefaultView;
    //    //dv.RowFilter = "ai_id is null ";
    //    dlAttach.DataSource = dvAttach;
    //    dlAttach.DataBind();

    //    //process history
    //    ds.Clear();
    //    strSql = "select replace(replace(process_comment,'\n','<br>'),' ','&nbsp;') process_comment,to_char(create_dttm,'yyyy/mm/dd hh24:mi:ss') create_dttm,create_user from tms_process_history where project_id = '134' order by create_dttm desc";
    //    //strSql = "select replace(process_comment,'\r\n','<br>') process_comment,to_char(create_dttm,'yyyy/mm/dd hh24:mi:ss') create_dttm,create_user from tms_process_history where task_id = '101' order by create_dttm desc";
    //    ds = m_objDB.ExecuteDataSet(strSql);
    //    dlProcessHistory.DataSource = ds;
    //    dlProcessHistory.DataBind();

    //    //get task member
    //    ds.Clear();
    //    strSql = "select a.task_id,b.member_name,b.is_owner from tms_task a , tms_member b where a.project_id = " + project_id + " and a.task_id = b.task_id ";
    //    ds = m_objDB.ExecuteDataSet(strSql);
    //    dv = ds.Tables[0].DefaultView;

    //    //project task
    //    strSql = "select rownum rn,t.* from ( ";
    //    strSql += "select task_id,task_desc, to_char(estimate_start_date,'yyyy/mm/dd') estimate_start_date, to_char(estimate_end_date,'yyyy/mm/dd') estimate_end_date, status, ";
    //    strSql += "to_char(actual_start_date,'yyyy/mm/dd') actual_start_date, to_char(actual_end_date,'yyyy/mm/dd') actual_end_date ";
    //    strSql += "from tms_task where project_id = '" + project_id + "' order by estimate_end_date,estimate_start_date) t";
    //    ds = m_objDB.ExecuteDataSet(strSql);
    //    gvTask.DataSource = ds;
    //    gvTask.DataBind();

    //    //initField();
    //}

    //    private void setProjectGroup()
    //    {
    //        strSql = @"select distinct t.project_id,t.project_group from tms_project_group t 
    //        where t.project_dept in (select t1.authority_dept from tms_authority_dept t1 where t1.dept = '{0}' ) order by t.project_id ";

    //        strSql = string.Format(strSql, Session["user_dept"].ToString());

    //        ddlProjectGroup.DataTextField = "project_group";
    //        ddlProjectGroup.DataValueField = "project_id";
    //        ddlProjectGroup.DataSource = m_objDB.ExecuteDataSet(strSql);
    //        ddlProjectGroup.DataBind();
    //        ddlProjectGroup.Items.Insert(0, "");

    //    }

    protected void gvTask_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            ImageButton btnDel = new ImageButton();
            btnDel = (ImageButton)e.Row.FindControl("btnDel");
            //btnDel.CommandArgument = ((Label)e.Row.FindControl("lblDefectType")).Text;
            btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【MODEL_NAME】:" + ((DataRowView)e.Row.DataItem)["MODEL_NAME"] + " 【TOOL_ID】:" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');";




            string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"];
            string strSql_Pro;
            string snn1;

            //GridViewRow row = GridView2.Rows[e.RowIndex];



            DataSet ds = new DataSet();

            strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t ";
            strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'";


            ds = func.get_dataSet_access(strSql_Pro, conn);


            ((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0];
            ((DataList)e.Row.FindControl("DataList1")).DataBind();



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString();
            // dv.RowFilter = "task_id=" + strTaskID;
            //dv.Sort = "is_owner desc";

            //task member datalist
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv;
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind();

            //image link to task content

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID);
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}";

        }
    }

    

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //        Innolux.Portal.Report.ReportExcel excel = new Innolux.Portal.Report.ReportExcel(new string[] { "Project Schedule" });

        //        int top = 1;
        //        strSql = @"select t.project_name project,to_char(t.estimate_start_date,'yyyy/mm/dd') 預計開始日期,to_char(t.estimate_end_date,'yyyy/mm/dd') 預計完成日期,
        //            to_char(t.actual_start_date,'yyyy/mm/dd') 實際開始日期,to_char(t.actual_end_date,'yyyy/mm/dd') 實際完成日期,
        //            t.applicant owner,t.status,t1.project_group from tms_project t,tms_project_group t1 
        //            where t.project_group_id=t1.project_id(+) and t.project_id = {0} ";
        //        strSql = string.Format(strSql, lblProjectNo.Text);
        //        DataTable dtProject = m_objDB.ExecuteDataSet(strSql).Tables[0];
        //        excel.SetText(excel.Sheets[0], "▼Project", top, 1, top, dtProject.Columns.Count);
        //        top++;
        //        excel.ImportDataTable(excel.Sheets[0], dtProject, top, new int[] { 1 });
        //        excel.Sheets[0].Range[top + 1, 2, top + 1 + dtProject.Rows.Count, 5].NumberFormat = "yyyy/mm/dd";

        //        top += dtProject.Rows.Count + 2;

        //        /*excel.SetText(excel.Sheets[0], "Project Name: " + dtProject.Rows[0]["project"].ToString(), 1, 1, 1, 1);
        //        excel.SetText(excel.Sheets[0], "Project Group: " + dtProject.Rows[0]["project_group"].ToString(), 1, 2, 1, 2);
        //        excel.SetText(excel.Sheets[0], "預計開始日期: " + dtProject.Rows[0]["estimate_start_date"].ToString(), 2, 1, 2, 1);
        //        excel.SetText(excel.Sheets[0], "預計完成日期: " + dtProject.Rows[0]["estimate_end_date"].ToString(), 2, 2, 2, 2);
        //        excel.SetText(excel.Sheets[0], "實際開始日期: " + dtProject.Rows[0]["actual_start_date"].ToString(), 3, 1, 3, 1);
        //        excel.SetText(excel.Sheets[0], "實際完成日期: " + dtProject.Rows[0]["actual_end_date"].ToString(), 3, 2, 3, 2);
        //        excel.SetText(excel.Sheets[0], "Owner: " + dtProject.Rows[0]["applicant"].ToString(), 4, 1, 4, 1);
        //        excel.SetText(excel.Sheets[0], "Status: " + dtProject.Rows[0]["status"].ToString(), 4, 2, 4, 2);
        //        */

        //        strSql = @"select t.task_desc Task,to_char(t.estimate_start_date,'yyyy/mm/dd') 預計開始日期,to_char(t.estimate_end_date,'yyyy/mm/dd') 預計完成日期,
        //            to_char(t.actual_start_date,'yyyy/mm/dd') 實際開始日期,to_char(t.actual_end_date,'yyyy/mm/dd') 實際完成日期,
        //            t.status from tms_task t where t.project_id={0} order by t.estimate_end_date,t.task_id";

        //        strSql = string.Format(strSql, lblProjectNo.Text);
        //        DataTable dtTask = m_objDB.ExecuteDataSet(strSql).Tables[0];
        //        excel.SetText(excel.Sheets[0], "▼Task", top, 1, top, dtTask.Columns.Count);
        //        top++;
        //        excel.ImportDataTable(excel.Sheets[0], dtTask, top, new int[] { 1 });
        //        excel.Sheets[0].Range[top + 1, 2, top + 1 + dtTask.Rows.Count, 5].NumberFormat = "yyyy/mm/dd";

        //        top += dtTask.Rows.Count + 2;

        //        strSql = @"select t.task_desc Task,t1.ai_desc AI,to_char(t1.estimate_start_date,'yyyy/mm/dd') 預計開始日期,to_char(t1.estimate_end_date,'yyyy/mm/dd') 預計完成日期,
        //            to_char(t1.actual_start_date,'yyyy/mm/dd') 實際開始日期,to_char(t1.actual_end_date,'yyyy/mm/dd') 實際完成日期,t1.member_name owner,t1.status,t1.progress|| '%' 進度,t1.ai_hour 工時_Hr
        //            from tms_task t,tms_action_item t1 where t.task_id=t1.task_id and t.project_id={0} order by t.estimate_end_date,t.task_id";

        //        strSql = string.Format(strSql, lblProjectNo.Text);
        //        DataTable dtAI = m_objDB.ExecuteDataSet(strSql).Tables[0];
        //        excel.SetText(excel.Sheets[0], "▼Action Item", top, 1, top, dtAI.Columns.Count);
        //        top++;
        //        excel.ImportDataTable(excel.Sheets[0], dtAI, top, new int[] { 1, 3, 4 });
        //        excel.Sheets[0].Range[top + 1, 3, top + 1 + dtAI.Rows.Count, 6].NumberFormat = "yyyy/mm/dd";



        //        excel.ToExcel("Project_" + DateTime.Now.ToString("yyyyMMdd"), Innolux.Portal.Report.DownloadExcelType.PromptDialog);
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
        string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"];
        string strSql = "";
        string snn;
        // string snn1;
        GridViewRow row = gvTask.Rows[e.RowIndex];

        if (row.RowState == DataControlRowState.Normal || row.RowState == DataControlRowState.Alternate)
        {
            snn = ((Label)row.FindControl("lblSN")).Text;
            //snn1 = ((Label)row.FindControl("lblaid2")).Text;

        }

        else
        {
            snn = ((Label)row.FindControl("lblSN")).Text;
            // snn1 = ((Label)row.FindControl("lblaid2_Edit")).Text;

        }

        strSql = "delete from tlms_main where SN='" + snn + "'";
        func.get_sql_execute(strSql, conn);
        gvTask.EditIndex = -1;
        Bind_data();
    }



    protected void Button11_Click(object sender, EventArgs e)//add select
    {
        if (ListBox1.SelectedItem != null)
        {
            foreach (ListItem item in ListBox1.Items)
            {
                if (item.Selected == true)
                {
                    ListBox2.Items.Add(item);
                }
            }

            foreach (ListItem item in ListBox2.Items)
            {
                if (item.Selected == true)
                {
                    ListBox1.Items.Remove(item);
                }
            }
        }
        ListBox2_OnTextChanged();
    }
    protected void Button12_Click(object sender, EventArgs e) //add select all
    {
        foreach (ListItem item in ListBox1.Items)
        {
            ListBox2.Items.Add(item);
        }
        ListBox1.Items.Clear();
        ListBox2_OnTextChanged();
    }


    protected void Button13_Click(object sender, EventArgs e) //remove select
    {
        if (ListBox2.SelectedItem != null)
        {
            foreach (ListItem item in ListBox2.Items)
            {
                if (item.Selected == true)
                {
                    ListBox1.Items.Add(item);
                }
            }

            foreach (ListItem item in ListBox1.Items)
            {
                if (item.Selected == true)
                {
                    ListBox2.Items.Remove(item);
                }
            }
        }

        ListBox2_OnTextChanged();
    }


    protected void Button14_Click(object sender, EventArgs e)//remove select all
    {
        foreach (ListItem item in ListBox2.Items)
        {
            ListBox1.Items.Add(item);
        }
        ListBox2.Items.Clear();
    }




    protected void Button21_Click(object sender, EventArgs e)
    {
        if (ListBox3.SelectedItem != null)   //左邊 ListBox 有選Item ,加到 右邊 ListBox
        {
            foreach (ListItem item in ListBox3.Items)
            {
                if (item.Selected == true)
                {
                    ListBox4.Items.Add(item);
                }
            }

            foreach (ListItem item in ListBox4.Items)//右邊 ListBox 有的Item,左邊 ListBox 將Item 移除
            {
                if (item.Selected == true)
                {
                    ListBox3.Items.Remove(item);
                }
            }
        }
        ListBox4_OnTextChanged();
    }



    protected void Button22_Click(object sender, EventArgs e) //將左邊 ListBox 中的Item ,加到右邊 ListBox
    {
        foreach (ListItem item in ListBox3.Items)
        {
            ListBox4.Items.Add(item);
        }
        ListBox3.Items.Clear();  //將左邊 ListBox中的Item,移除

        ListBox4_OnTextChanged();
    }


    protected void Button23_Click(object sender, EventArgs e)
    {
        if (ListBox4.SelectedItem != null)  //右邊ListBox 選擇非空  加入左邊ListBox
        {
            foreach (ListItem item in ListBox4.Items)
            {
                if (item.Selected == true)
                {
                    ListBox3.Items.Add(item);//加入左邊ListBox
                }
            }

            foreach (ListItem item in ListBox3.Items)//左邊ListBox 中出現的 Item,u.右邊Item 中 Remove
            {
                if (item.Selected == true)
                {
                    ListBox4.Items.Remove(item);
                }
            }
        }

        ListBox4_OnTextChanged();
    }



    protected void Button24_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in ListBox4.Items) //右邊ListBox 有的Item 加入在左邊的 ListBox 
        {
            ListBox3.Items.Add(item);
        }
        ListBox4.Items.Clear(); //將右邊ListBox 中的Item Remove

        ListBox4_OnTextChanged();
    }




    protected void Button31_Click(object sender, EventArgs e)
    {
        if (ListBox5.SelectedItem != null)
        {
            foreach (ListItem item in ListBox5.Items)
            {
                if (item.Selected == true)
                {
                    ListBox6.Items.Add(item);
                }
            }

            foreach (ListItem item in ListBox6.Items)
            {
                if (item.Selected == true)
                {
                    ListBox5.Items.Remove(item);
                }
            }
        }

        ListBox6_OnTextChanged();
    }
    protected void Button32_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in ListBox5.Items)
        {
            ListBox6.Items.Add(item);
        }
        ListBox5.Items.Clear();  //將左邊 ListBox中的Item,移除

        ListBox6_OnTextChanged();
    }
    protected void Button33_Click(object sender, EventArgs e)
    {
        if (ListBox6.SelectedItem != null)  //右邊ListBox 選擇非空  加入左邊ListBox
        {
            foreach (ListItem item in ListBox6.Items)
            {
                if (item.Selected == true)
                {
                    ListBox5.Items.Add(item);//加入左邊ListBox
                }
            }

            foreach (ListItem item in ListBox5.Items)//左邊ListBox 中出現的 Item,u.右邊Item 中 Remove
            {
                if (item.Selected == true)
                {
                    ListBox6.Items.Remove(item);
                }
            }
        }

        ListBox6_OnTextChanged();
    }
    protected void Button34_Click(object sender, EventArgs e)
    {

        foreach (ListItem item in ListBox6.Items) //右邊ListBox 有的Item 加入在左邊的 ListBox 
        {
            ListBox5.Items.Add(item);
        }
        ListBox6.Items.Clear(); //將右邊ListBox 中的Item Remove

        ListBox6_OnTextChanged();
    }

    public DataTable Bind_data()
    {
        string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"];
        string sql_FAB = combine_List_box(ListBox2);
        string sql_PROCESS = combine_List_box(ListBox4);
        string sql_LAYER = combine_List_box(ListBox6);
        string sql_MODEL_NAME = combine_List_box(ListBox8);


        string sql = " select site,            " +
"        fab,             " +
"        process,         " +
"        model_name,      " +
"        layer,           " +
"        eq_vender,       " +
"        eq_type,         " +
"        tool_id,         " +
"        tool_version,    " +
"        reason,          " +
"        library,         " +
"        pixel_cell,      " +
"        panel_cell,      " +
"        sub_cell,        " +
"        mask_cell,       " +
"        gds,             " +
"        owner,           " +
"        designer,        " +
"        tapeout_date,    " +
"        mask_size,       " +
"        release,         " +
"        release_date,    " +
"        scrap,           " +
"        scrap_date,      " +
"        discard,         " +
"        discard_date,    " +
"        test,            " +
"        status,          " +
"        update_user,     " +
"        dttm,            " +
"        sn               " +
"   from tlms_main t      " +
"   where 1=1             ";
        if (ListBox2.Items.Count >= 1)
        {
            sql += "   and  trim(t.fab) in (" + sql_FAB + ")      ";
        }
        if (ListBox4.Items.Count >= 1)
        {
            sql += "   and  trim(t.process) in (" + sql_PROCESS + ")  ";

        }

        if (ListBox6.Items.Count >= 1)
        {
            sql += "   and  trim(t.layer)  in (" + sql_LAYER + ")   ";

        }
        if (ListBox6.Items.Count >= 1)
        {
            sql += "   and  trim(t.model_name) in(" + sql_MODEL_NAME + ")";

        }

        DataSet ds_query = new DataSet();
        ds_query = func.get_dataSet_access(sql, conn);

        gvTask.DataSource = ds_query.Tables[0];

        gvTask.DataBind();

        DataTable DT_QUERY = new DataTable();

        DT_QUERY = ds_query.Tables[0];

        return DT_QUERY;

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
    }

    private void ExportExcel(GridView SeriesValuesDataGrid)
    {
        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader("content-disposition", "attachment;filename=Tool_Query.xls");

        Response.Charset = "big5";

        // If you want the option to open the Excel file without saving than 

        // comment out the line below 

        // Response.Cache.SetCacheability(HttpCacheability.NoCache); 

        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        SeriesValuesDataGrid.AllowPaging = false;
        SeriesValuesDataGrid.DataBind();

        SeriesValuesDataGrid.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();

        SeriesValuesDataGrid.AllowPaging = true;
        SeriesValuesDataGrid.DataBind();


    }



    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        Bind_data();

    }
    protected void ListBox2_OnTextChanged()
    {
        string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"];

        string sql_fab = combine_List_box(ListBox2);

        string sql = "select distinct(t.process) as process from tlms_main t where t.fab in(" + sql_fab + ") and t.process is not null";

        ListBox3.Items.Clear();
        ListBox3.DataTextField = "process";
        ListBox3.DataValueField = "process";

        DataSet ds_process = new DataSet();
        ds_process = func.get_dataSet_access(sql, conn);


        ListBox3.DataSource = ds_process.Tables[0];
        ListBox3.DataBind();


    }

    protected void ListBox4_OnTextChanged()
    {
        string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"];

        string sql_process = combine_List_box(ListBox4);

        string sql = "select distinct(t.layer) as layer from tlms_main t where t.PROCESS in(" + sql_process + ") and t.layer is not null";

        ListBox5.Items.Clear();
        ListBox5.DataTextField = "layer";
        ListBox5.DataValueField = "layer";

        DataSet ds_process = new DataSet();
        ds_process = func.get_dataSet_access(sql, conn);


        ListBox5.DataSource = ds_process.Tables[0];
        ListBox5.DataBind();


    }


    protected void ListBox6_OnTextChanged()
    {
        string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"];

        string sql_LAYER = combine_List_box(ListBox6);

        string sql = "select distinct(t.model_name) as model_name from tlms_main t where t.LAYER in(" + sql_LAYER + ") and t.model_name is not null";

        ListBox7.Items.Clear();
        ListBox7.DataTextField = "model_name";
        ListBox7.DataValueField = "model_name";

        DataSet ds_process = new DataSet();
        ds_process = func.get_dataSet_access(sql, conn);


        ListBox7.DataSource = ds_process.Tables[0];
        ListBox7.DataBind();


    }



    private static string combine_List_box(ListBox Source_list)
    {

        ListBox Target = new ListBox();
        Target = Source_list;

        if (Target.SelectedItem is Nullable)
        {


        }

        else
        {

            string initial = "";
            for (int i = 0; i <= Target.Items.Count - 1; i++)
            {
                if (i == 0)
                {
                    initial = initial + "'" + Target.Items[i] + "'";
                }

                else
                {

                    initial = initial + ",'" + Target.Items[i] + "'";
                }


            }
            return initial;

        }



    }

    protected void Button41_Click(object sender, EventArgs e)
    {
        if (ListBox7.SelectedItem != null)
        {
            foreach (ListItem item in ListBox7.Items)
            {
                if (item.Selected == true)
                {
                    ListBox8.Items.Add(item);
                }
            }

            foreach (ListItem item in ListBox8.Items)
            {
                if (item.Selected == true)
                {
                    ListBox7.Items.Remove(item);
                }
            }
        }

    }
    protected void Button42_Click(object sender, EventArgs e)
    {

        foreach (ListItem item in ListBox7.Items)
        {
            ListBox8.Items.Add(item);
        }
        ListBox7.Items.Clear();  //將左邊 ListBox中的Item,移除


    }
    protected void Button43_Click(object sender, EventArgs e)
    {

        if (ListBox8.SelectedItem != null)  //右邊ListBox 選擇非空  加入左邊ListBox
        {
            foreach (ListItem item in ListBox8.Items)
            {
                if (item.Selected == true)
                {
                    ListBox7.Items.Add(item);//加入左邊ListBox
                }
            }

            foreach (ListItem item in ListBox7.Items)//左邊ListBox 中出現的 Item,u.右邊Item 中 Remove
            {
                if (item.Selected == true)
                {
                    ListBox8.Items.Remove(item);
                }
            }
        }

    }
    protected void Button44_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in ListBox8.Items) //右邊ListBox 有的Item 加入在左邊的 ListBox 
        {
            ListBox7.Items.Add(item);
        }
        ListBox8.Items.Clear(); //將右邊ListBox 中的Item Remove
    }

    public DataTable Bind_data2()
    {
        string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"];

        string sql = "select * from tlms_main t  " +
     "where t.tool_id='" + TextBox1.Text + "'";

        DataSet ds_tool = new DataSet();

        ds_tool = func.get_dataSet_access(sql, conn);

        gvTask.DataSource = ds_tool.Tables[0];

        gvTask.DataBind();

        DataTable dt_temp = new DataTable();

        dt_temp = ds_tool.Tables[0];

        return dt_temp;

    }
    protected void ButtonQuery1_Click(object sender, EventArgs e)
    {
        Bind_data2();


    }
    protected void btnExport_Click1(object sender, EventArgs e)
    {
        GridView gv = new GridView();

        if (TextBox1.Text == "")
        {
            gv.DataSource = Bind_data();
            gv.DataBind();
        }
        else
        {
            gv.DataSource = Bind_data2();
            gv.DataBind();

        }


        ExportExcel(gv);
    }

    protected void gvTask_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvTask.EditIndex = e.NewEditIndex;
        Bind_data();
    }


    protected void gvTask_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvTask.EditIndex = -1;
        Bind_data();
    }

    protected void gvTask_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string conn6 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"];
        string snn;
        string snn1 = "";

        
        string userid = "oscar";
        GridViewRow row = gvTask.Rows[e.RowIndex];

        if (row.RowState == DataControlRowState.Normal || row.RowState == DataControlRowState.Alternate)
        {
            snn = ((Label)row.FindControl("lblSN")).Text;
        }
        else
        {

            snn = ((Label)row.FindControl("lblSN")).Text;

        }

        //snn1 = ((Label)row.FindControl("lblonclass")).Text;

        if ((row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate)) || (row.RowState == DataControlRowState.Edit))
        {

            if (1 == 1)
            {
                string abc = "";


  



                abc = "   update tlms_main                           " +
"      set site = '" + ((TextBox)row.FindControl("lblSITE")).Text + "',                      " +
"          fab = '" + ((TextBox)row.FindControl("lblFAB")).Text + "',                        " +
"          process = '" + ((TextBox)row.FindControl("lblPROCESS")).Text + "',                " +
"          model_name = '" + ((TextBox)row.FindControl("lblMODEL_NAME")).Text + "',          " +
"          layer = '" + ((TextBox)row.FindControl("lblLAYER")).Text + "',                    " +
"          eq_vender = '" + ((TextBox)row.FindControl("lblEQ_VENDER")).Text + "',            " +
"          eq_type = '" + ((TextBox)row.FindControl("lblEQ_TYPE")).Text + "',                " +
"          tool_id = '" + ((TextBox)row.FindControl("lblTOOL_ID")).Text + "',                " +
"          tool_version = '" + ((TextBox)row.FindControl("lblTOOL_Version")).Text + "',      " +
"          reason = '" + ((TextBox)row.FindControl("lblREASON")).Text + "',                  " +
"          library = '" + ((TextBox)row.FindControl("lblLIBRARY")).Text + "',                " +
"          pixel_cell = '" + ((TextBox)row.FindControl("lblPIXEL_CELL")).Text + "',          " +
"          panel_cell = '" + ((TextBox)row.FindControl("lblPANEL_CELL")).Text + "',          " +
"          sub_cell = '" + ((TextBox)row.FindControl("lblSUB_CELL")).Text + "',              " +
"          mask_cell = '" + ((TextBox)row.FindControl("lblMASK_CELL")).Text + "',            " +
"          gds = '" + ((TextBox)row.FindControl("lblGDS")).Text + "',                        " +
"          owner = '" + ((TextBox)row.FindControl("lblOWNER")).Text + "',                    " +
"          designer = '" + ((TextBox)row.FindControl("lblDESIGNER")).Text + "',              " +
"          tapeout_date = '" + ((TextBox)row.FindControl("lblTAPEOUT_DATE")).Text + "',      " +
"          mask_size = '" + ((TextBox)row.FindControl("lblMASK_SIZE")).Text + "',            " +
"          release = '" + ((TextBox)row.FindControl("lblRELEASE")).Text + "',                " +
"          release_date = '" + ((TextBox)row.FindControl("lblRELEASE_DATE")).Text + "',      " +
"          scrap = '" + ((TextBox)row.FindControl("lblSCRAP")).Text + "',                    " +
"          scrap_date = '" + ((TextBox)row.FindControl("lblSCRAP_DATE")).Text + "',          " +
"          discard = '" + ((TextBox)row.FindControl("lblDISCARD")).Text + "',                " +
"          discard_date = '" + ((TextBox)row.FindControl("lblDISCARD_DATE")).Text + "',      " +
"          test = '" + ((TextBox)row.FindControl("lblTEST")).Text + "',                      " +
"          status = '" + ((TextBox)row.FindControl("lblSTATUS")).Text + "',                  " +
"          update_user = '" + userid + "',        " +
"          dttm = sysdate                      " +

"    where sn='" + snn + "'                                  ";


                func.get_sql_execute(abc, conn6);
                gvTask.EditIndex = -1;




            }
            else
            {

                Response.Write("<script language='javascript'>" + "\n");
                Response.Write("alert('璅谾極?[" + Session["user_id"] + "],瘝緧湔唳祇!!');");
                Response.Write("</script>");
                return;
            }

        }


    }

}
