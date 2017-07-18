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

public partial class project_assign_mail : System.Web.UI.Page
{
    dbutil DBUtil = new dbutil();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            setProjectData();
        }
    }

    private void setProjectData()
    {
        OleDbDataReader dr;
        string strSql;
        OleDbConnection myConnection = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["dsn"]);

        myConnection.Open();

        strSql = "select * from tms_project where project_id = '" + Request.QueryString["project_id"].ToString() + "'";
        dr = DBUtil.GetDatareader(strSql, myConnection);

        while (dr.Read())
        {
            lblProjectNo.Text = dr["project_id"].ToString();
            lblStatus.Text = dr["status"].ToString();
            lblProjectDesc.Text = dr["project_desc"].ToString();
            lblAppilcant.Text = dr["applicant"].ToString();
            lblAppilcantDept.Text = dr["applicant_dept"].ToString();
            lblApplyDate.Text = dr["apply_date"].ToString();
        }

        string strAddress = "http://" + System.Configuration.ConfigurationSettings.AppSettings["dnsname"] + Context.Request.ApplicationPath;
        hyLink.NavigateUrl = strAddress + "/project_assign.aspx?project_id=" + lblProjectNo.Text + "&mail_to=Y";

        //process history
        strSql = "select replace(replace(process_comment,'\n','<br>'),' ','&nbsp;') process_comment,to_char(create_dttm,'yyyy/mm/dd hh24:mi:ss') create_dttm,create_user from tms_process_history where project_id = '" + Request.QueryString["project_id"].ToString() + "' order by create_dttm desc";
        DataSet ds = DBUtil.GetDataset(strSql, myConnection);
        if (ds.Tables[0].Rows.Count > 0)
        {
            dlProcessHistory.DataSource = ds;
            dlProcessHistory.DataBind();
            fs1.Visible = true;
        }

        myConnection.Close();
        myConnection.Dispose();
    }
}
