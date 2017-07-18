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
using System.Drawing;

public partial class control_limit_maintain_defect_type_maintain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
      
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        btnQuery_Click(null, null);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string strSql;
        OleDbDataReader reader;
        OleDbConnection myConnection = new OleDbConnection(System.Configuration.ConfigurationSettings.AppSettings["dsn"]);

        try
        {
            myConnection.Open();
        }
        catch
        {
            Response.Write("<br><br><center><table border=1 cellpadding=20 bordercolor=black bgcolor=#EEEEEE width=750>");
            Response.Write("<tr><td style='font:9pt Verdana'>");
            Response.Write("DB Connection 異常 !");
            Response.Write("</td></tr></table></center>");
            Response.End();
        }

        strSql = "select count(*) count from QE_MAINTAIN_CONFIG_T ";
        strSql = strSql + "where ma_name='" + txtDefectType.Text.Trim() + "' and ma_type='Defect_Type'";

        reader = dbutil.GetDatareader(strSql, myConnection);
        while (reader.Read())
        {

            if (Convert.ToInt16(reader["count"]) > 0)
            {
                Response.Write("<script language='javascript'>" + "\n");
                Response.Write("alert('新增失敗，Defect Type重複!!!');");
                Response.Write("window.close();</script>");
                return;
            }

        }
        reader.Close();

        strSql = "insert into QE_MAINTAIN_CONFIG_T ";
        strSql = strSql + "(sn,ma_name, ma_desc,ma_type, create_user, create_dttm, update_user, update_dttm) ";
        strSql = strSql + "values (QE_MAINTAIN_CONFIG_SEQ.NEXTVAL,";
        strSql = strSql + "'" + txtDefectType.Text + "',";
        strSql = strSql + "'" + txtDefectDesc.Text + "',";
        strSql = strSql + "'Defect_Type',";
        strSql = strSql + "'" + Session["user_id"] + "',";
        strSql = strSql + "sysdate,";
        strSql = strSql + "'" + Session["user_id"] + "',";
        strSql = strSql + "sysdate)";
      
        try
        {
            dbutil.ExecuteStatement(strSql, myConnection);
            btnQuery_Click(null, null);

            myConnection.Close();
            myConnection.Dispose();
        }
        catch (Exception Ex)
        {

            myConnection.Close();
            myConnection.Dispose();   
        } 
        
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strSql;
        DataSet ds = new DataSet();

        strSql = "select sn,ma_name,ma_desc,create_user,to_char(create_dttm,'yyyy/mm/dd hh24:mi:ss') create_dttm, ";
        strSql = strSql + "update_user,to_char(update_dttm,'yyyy/mm/dd hh24:mi:ss') update_dttm ";
        strSql = strSql + "from QE_MAINTAIN_CONFIG_T where ma_type='Defect_Type' ";
        if (txtDefectType.Text != "")
        {
            strSql = strSql + "and ma_name like '%" + txtDefectType.Text + "%' "; 
        }
        strSql = strSql + "order by ma_name";
        ds = dbutil.GetDataset(strSql);
        GridView1.DataSource = ds;
        GridView1.DataBind();

        if (ds.Tables[0].Rows.Count == 0)
        {
            lblMessages.Text = "No data found";
            lblMessages.ForeColor = Color.Red;
            legend1.Visible = false;
        }
        else
        {
            lblMessages.Text = ds.Tables[0].Rows.Count.ToString() + " records found";
            lblMessages.ForeColor = Color.Red;
            legend1.Visible = true;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType ==  DataControlRowType.DataRow)
        {
            Button btnDel = new Button();
            btnDel = (Button)e.Row.FindControl("btnDel");
            //btnDel.CommandArgument = ((Label)e.Row.FindControl("lblDefectType")).Text;
            btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? [" + ((DataRowView)e.Row.DataItem)["ma_name"] + "]');";
            

        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strSql;
        string sn;
        GridViewRow row = GridView1.Rows[e.RowIndex];

        if (row.RowState == DataControlRowState.Normal || row.RowState == DataControlRowState.Alternate)
            sn = ((Label)row.FindControl("lblSN")).Text;
        else
            sn = ((Label)row.FindControl("lblSN_Edit")).Text;

        strSql = "delete from QE_MAINTAIN_CONFIG_T where sn=" + sn ;
        dbutil.ExecuteStatement(strSql);
        GridView1.EditIndex = -1;
        btnQuery_Click(null, null);
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        btnQuery_Click(null, null);
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        btnQuery_Click(null, null);
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strSql;
        string sn;
        GridViewRow row = GridView1.Rows[e.RowIndex];

        if (row.RowState == DataControlRowState.Normal || row.RowState == DataControlRowState.Alternate)
            sn = ((Label)row.FindControl("lblSN")).Text;
        else
            sn = ((Label)row.FindControl("lblSN_Edit")).Text;

        strSql = "update QE_MAINTAIN_CONFIG_T ";
        strSql = strSql + "set ma_name='" + ((TextBox)row.FindControl("txtDefectType")).Text +"',";
        strSql = strSql + "ma_desc='" + ((TextBox)row.FindControl("txtDefectDesc")).Text + "',";
        strSql = strSql + "update_user='" + Session["user_id"] + "',";
        strSql = strSql + "update_dttm=sysdate ";
        strSql = strSql + "where sn= " + sn;
        dbutil.ExecuteStatement(strSql);
        GridView1.EditIndex = -1;
        btnQuery_Click(null, null);
    }



}
