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

public partial class CheckUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strSql = "";
        DataSet ds = new DataSet();
        dbutil DBUtil = new dbutil();

        if (Session["user_name"] == null)
        {
            strSql = "select cname,dept from tms_empinfo t where t.empno='" + HttpContext.Current.User.Identity.Name.Substring(HttpContext.Current.User.Identity.Name.IndexOf("\\") + 1).ToUpper() + "'";
            ds = DBUtil.GetDataset(strSql);

            if (ds.Tables[0].Rows.Count == 0)
            {
                deny_message();
            }
            else
            {
                Session["user_name"] = ds.Tables[0].Rows[0]["cname"].ToString();
                Session["user_dept"] = ds.Tables[0].Rows[0]["dept"].ToString();
            }
        }
    }

    private void deny_message()
    {
        Response.Write("<br><br><center><table border=1 cellpadding=20 bordercolor=black bgcolor=#EEEEEE width=750>");
        Response.Write("<tr><td style='font:9pt Verdana'>");
        Response.Write("你沒有查詢此工作的權限 !");
        Response.Write("</td></tr></table></center>");
        Response.End();
    }
}
