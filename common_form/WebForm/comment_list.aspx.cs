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
using System.Data.OleDb;
using System.IO;

public partial class comment_list : System.Web.UI.Page
{
    dbutil DBUtil = new dbutil();
    string project_id = string.Empty;
    string strSql;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["project_id"] != null)
        {
            DBUtil.setUserData();
            project_id = Request.QueryString["project_id"].ToString();

            //if (!IsPostBack)
            //{

                setProject();

            //}


        }
    }

    private void setProject()
    {

        DataSet ds = new DataSet();

        ds.Clear();
        strSql = "select project_name from tms_project where project_id = '" + Request.QueryString["project_id"].ToString() + "'";
        ds = DBUtil.GetDataset(strSql);
        lblTitle.InnerText = ds.Tables[0].Rows[0]["project_name"].ToString();

        //process history
        strSql = "select replace(replace(process_comment,'\n','<br>'),' ','&nbsp;') process_comment,to_char(create_dttm,'yyyy/mm/dd hh24:mi:ss') create_dttm,create_user from tms_process_history where project_id = '" + Request.QueryString["project_id"].ToString() + "'order by create_dttm desc";
        //strSql = "select replace(process_comment,'\r\n','<br>') process_comment,to_char(create_dttm,'yyyy/mm/dd hh24:mi:ss') create_dttm,create_user from tms_process_history where task_id = '101' order by create_dttm desc";
        ds = DBUtil.GetDataset(strSql);
        dlProcessHistory.DataSource = ds;
        dlProcessHistory.DataBind();

        ////附件
        strSql = "select * from tms_attachment where project_id = '" + Request.QueryString["project_id"].ToString() + "' order by file_desc";
        //strSql = "select * from tms_attachment where task_id = '101'";
        ds = DBUtil.GetDataset(strSql);
        dlAttach.DataSource = ds;
        dlAttach.DataBind();

        if (ds.Tables[0].Rows.Count > 0)
            fsAttach.Visible = true;

    }
}
