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

public partial class common_form_user_control_ProjectGroupSelector : System.Web.UI.UserControl
{
    dbutil DBUtil = new dbutil();
    string strSql = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        set_data_list();
        txtProjectGroup.Text = "";
        //ddlProjectGroup.SelectedValue = "";
    
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

        strSql = string.Format(strSql,txtProjectGroup.Text,Session["user_dept"].ToString(),Session["user_name"].ToString(),Session["user_name"].ToString());
        DBUtil.ExecuteStatement(strSql);

        set_data_list();
    }
}
