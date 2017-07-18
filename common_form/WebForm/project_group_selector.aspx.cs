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

public partial class common_form_WebForm_project_group_selector : System.Web.UI.Page
{
    dbutil DBUtil = new dbutil();
    string strSql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["user_dept"] = "TFTPIA/CIM1/MI";
        Session["user_name"] = "林志湳";
        if (!Page.IsPostBack)
            set_data_list();
        
    }

    private void set_data_list()
    {
        strSql = @"select distinct t.project_id,t.project_group from tms_project_group t 
        where t.project_dept in (select t1.authority_dept from tms_authority_dept t1 where t1.dept = '{0}' ) order by t.project_id ";

        strSql = string.Format(strSql, Session["user_dept"].ToString());

        ddlProjectGroup.DataTextField = "project_group";
        ddlProjectGroup.DataValueField = "project_id";
        ddlProjectGroup.DataSource = DBUtil.GetDataset(strSql);
        ddlProjectGroup.DataBind();
        ddlProjectGroup.Items.Insert(0, "");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        strSql = @"insert into tms_project_group (project_id, project_group, project_dept, create_dttm, create_user, last_update_dttm, last_update_user) 
                   values(project_seq.nextval,'{0}','{1}',sysdate,'{2}',sysdate,'{3}') ";

        strSql = string.Format(strSql, txtProjectGroup.Text, Session["user_dept"].ToString(), Session["user_name"].ToString(), Session["user_name"].ToString());
        DBUtil.ExecuteStatement(strSql);

        set_data_list();
        Response.Redirect("project_group_selector.aspx");
    }
}
