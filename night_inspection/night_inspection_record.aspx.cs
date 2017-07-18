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

public partial class night_inspection_night_inspection_record : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OARPT_MEETUSER"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_SSODB_OLE"];
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();

    //string[] flag_str = {"Open","Closed","Cancel"};

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["sn"] = Request["sn"].Trim().ToUpper();

            //Session["sn"] = "20100614161826";

            string sql = " select sn,                                                         " +
"        record_person,                                              " +
"        to_char(start_time, 'YYYY/MM/DD HH24MISS') as start_time1, start_time,  " +
"        abnormal_type,                                              " +
"        abnormal_area,                                              " +
"        dep,                                                        " +
"        abnormal_description,                                       " +
"        area_owner,                                                 " +
"        area_owner_phone,                                           " +
"        policy,                                                     " +
"        open_close_flag,                                            " +
"        close_person,                                               " +
"        dttm_creste,                                                " +
"        dttm_close                                                  " +
"   from night_inspection_record t                                  " +
"  where t.sn = '" + Session["sn"] + "'                                   ";

            //Label1.Text = DateTime.Now.AddDays(+0).ToString("yyyyMMddHHmmss");
            Label1.Text = Session["sn"].ToString();
            //Label1.Text = func.get_dataSet_access(sql, conn).Tables[0].Rows[0]["SN"].ToString();

            ds_temp = func.get_dataSet_access(sql, conn);

            Label3.Text = ds_temp.Tables[0].Rows[0]["record_person"].ToString();
            //Label3.Text = ds_temp.Tables[0].Rows[0]["record_person"].ToString();
            
            ArrayList arlist_temp1=new ArrayList();

           

            //arlist_temp1=func.FileToArray(Server.MapPath(".")+"\\config\\hour.txt");

            //DropDownList5.DataSource = arlist_temp1;
            //DropDownList5.DataBind();

            //DropDownList5.Text = ds_temp.Tables[0].Rows[0]["start_time1"].ToString().Substring(11, 2);


            //arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\min.txt");

            //DropDownList8.DataSource = arlist_temp1;
            //DropDownList8.DataBind();

            //DropDownList8.Text = ds_temp.Tables[0].Rows[0]["start_time1"].ToString().Substring(13, 2);

            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\flag_type.txt");

            DropDownList4.DataSource = arlist_temp1;

            DropDownList4.DataBind();

            DropDownList4.Text = ds_temp.Tables[0].Rows[0]["open_close_flag"].ToString();


            //txtEstimateStartDate.SelectedDate = Convert.ToDateTime(ds_temp.Tables[0].Rows[0]["start_time"].ToString());

            Label4.Text = ds_temp.Tables[0].Rows[0]["start_time"].ToString();


            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\abnormal_type.txt");

            DropDownList1.DataSource = arlist_temp1;

            DropDownList1.DataBind();
            DropDownList1.Text = ds_temp.Tables[0].Rows[0]["abnormal_type"].ToString();


            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\abnormal_area.txt");

            DropDownList2.DataSource = arlist_temp1;
            DropDownList2.DataBind();
            DropDownList2.Text = ds_temp.Tables[0].Rows[0]["abnormal_area"].ToString();



            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\dep.txt");

            DropDownList3.DataSource = arlist_temp1;
            DropDownList3.DataBind();
            DropDownList3.Text = ds_temp.Tables[0].Rows[0]["dep"].ToString();

            TextBox5.Text = ds_temp.Tables[0].Rows[0]["abnormal_description"].ToString();
            Label2.Visible = false;

            TextBox4.Text = ds_temp.Tables[0].Rows[0]["area_owner"].ToString();

            TextBox2.Text = ds_temp.Tables[0].Rows[0]["area_owner_phone"].ToString();

            TextBox6.Text = ds_temp.Tables[0].Rows[0]["policy"].ToString();

            DropDownList4.Text = ds_temp.Tables[0].Rows[0]["open_close_flag"].ToString();

            TextBox3.Text = ds_temp.Tables[0].Rows[0]["close_person"].ToString();

            if (DropDownList4.Text.Equals("Open"))
            {
                Button2.Visible = true;
                ButtonQuery.Visible = true;
                Button3.Visible = true;
                Button4.Visible = true;
            }

            string strSql_file_name = " select distinct (t3.file_name)  as file_name           " +
 "  from (                                   " +
 "        select *                           " +
 "          from night_inspection_file t     " +
 "         where t.sn = '" + Label1.Text + "'     " +
 "         order by t.dttm desc) t3          ";
            ds_temp1 = func.get_dataSet_access(strSql_file_name, conn);
            // DataList1.DataKeyField = "file_name";

            //ArrayList arlist_temp = new ArrayList();
            //for (int i = 0; i <ds_temp1.Tables[0].Rows.Count; i++)
            //{
                
            //    arlist_temp.Add(ds_temp1.Tables[0].Rows[i][0].ToString());

            //}
            

            //DataList1.DataSource =arlist_temp ;
            //DataList1.DataBind();
            //Button2.Attributes["OnClientClick"] = "javascript:return confirm('確認修改否? [狀態:" + DropDownList4.SelectedValue + "]');";

        }

        //Button2.Attributes["OnClientClick"] = "javascript:return confirm('確認修改否? [狀態:" + DropDownList4.SelectedValue + "]');";
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string sql =" select t2.cname,t2.email,t2.ext,t2.writedate from fwempinfo  t2 "+
" where t2.cname='"+TextBox4.Text+"'                                         "+
" and t2.writedate in(                                            "+
"                                                                 "+
" select max(t.writedate) as writedate                            "+
"   from fwempinfo t                                              "+
"  where t.cname = '"+TextBox4.Text+"'                                       "+
"                                                                 "+
" )                                                               ";

        ds_temp = func.get_dataSet_access(sql, conn1);

        if (ds_temp.Tables[0].Rows.Count > 0)
        {

            TextBox2.Text = ds_temp.Tables[0].Rows[0]["ext"].ToString();
        }
       
    }
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        string sql_update = "";

        if (DropDownList4.SelectedValue.Equals("Closed") || DropDownList4.SelectedValue.Equals("Cancel"))
        {
            sql_update = " update night_inspection_record                          " +
"    set           " +
//"        record_person = '" + Label3.Text + "',                 " +
//"        start_time = to_date('" + txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd") + " " + DropDownList5.SelectedValue + ":" + DropDownList8.SelectedValue + "','yyyy/MM/dd hh24:mi') ," +
"        abnormal_type = '" + DropDownList1.SelectedValue + "',                 " +
"        abnormal_area = '" + DropDownList2.SelectedValue + "',                 " +
"        dep = '" + DropDownList3.SelectedValue + "',                                     " +
"        abnormal_description = '" + TextBox5.Text + "',   " +
"        area_owner = '" + TextBox4.Text + "',                       " +
"        area_owner_phone = '" + TextBox2.Text + "',           " +
//"        policy = '" + TextBox6.Text + "',                               " +
//"        open_close_flag = '" + DropDownList4.SelectedValue + "',             " +
//"        close_person = '" + TextBox3.Text + "',                   " +
                //"        dttm_creste = v_dttm_creste,                     " +
"        dttm_close = sysdate                        " +
"  where sn='" + Session["sn"] + "'                                        ";
        }

        else

        {
            sql_update = " update night_inspection_record                          " +
"    set                                       " +
//"        record_person = '" + Label3.Text + "',                 " +
//"        start_time = to_date('" + txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd") + " " + DropDownList5.SelectedValue + ":" + DropDownList8.SelectedValue + "','yyyy/MM/dd hh24:mi') ," +
"        abnormal_type = '" + DropDownList1.SelectedValue + "',                 " +
"        abnormal_area = '" + DropDownList2.SelectedValue + "',                 " +
"        dep = '" + DropDownList3.SelectedValue + "',                                     " +
"        abnormal_description = '" + TextBox5.Text + "',   " +
"        area_owner = '" + TextBox4.Text + "',                       " +
"        area_owner_phone = '" + TextBox2.Text + "' ,          " +
//"        policy = '" + TextBox6.Text + "',                               " +
//"        open_close_flag = '" + DropDownList4.SelectedValue + "',             " +
//"        close_person = '" + TextBox3.Text + "'                   " +
                //"        dttm_creste = v_dttm_creste,                     " +
"        dttm_close = sysdate                        " +
"  where sn='" + Session["sn"] + "'                                        ";
        
        }





        func.get_sql_execute(sql_update, conn);

       Label2.Visible=true;
       Label2.Text = "  資料更新 完成 !!!";

       Label2.ForeColor = Color.Red;


       Response.Write("<script language='javascript' type='text/JavaScript'>\n");
       Response.Write("alert('資料更新 完成!!!');\n");
       //Response.Write("opener.document.location.reload(); window.opener=null; window.close();\n");

       Response.Write("location = 'night_inspection_query.aspx';\n");

       //Response.Write(" window.navigate(window.location.href);\n");

       Response.Write("window.opener.location=window.opener.location;\n");
       //Response.Write("opener.document.location.reload();\n");
       Response.Write("window.opener=null; window.close();\n");
       Response.Write("</script>");
       


      // Page_Load(null, null);

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string sql_update = "";

      
            sql_update = " update night_inspection_record                          " +
"    set           " +
                //"        record_person = '" + Label3.Text + "',                 " +
                //"        start_time = to_date('" + txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd") + " " + DropDownList5.SelectedValue + ":" + DropDownList8.SelectedValue + "','yyyy/MM/dd hh24:mi') ," +
                //"        abnormal_type = '" + DropDownList1.SelectedValue + "',                 " +
                //"        abnormal_area = '" + DropDownList2.SelectedValue + "',                 " +
                //"        dep = '" + DropDownList3.SelectedValue + "',                                     " +
                //"        abnormal_description = '" + TextBox5.Text + "',   " +
                //"        area_owner = '" + TextBox4.Text + "',                       " +
                //"        area_owner_phone = '" + TextBox2.Text + "',           " +
                "        policy = '" + TextBox6.Text + "',                               " +
                "        open_close_flag = '" + DropDownList4.SelectedValue + "',             " +
                "        close_person = '" + TextBox3.Text + "',                   " +
                //"        dttm_creste = v_dttm_creste,                     " +
"        dttm_close = sysdate                        " +
"  where sn='" + Session["sn"] + "'                                        ";
     
        
        





        func.get_sql_execute(sql_update, conn);

        Label2.Visible = true;
        Label2.Text = "  資料更新 完成 !!!";

        Label2.ForeColor = Color.Red;

        Response.Write("<script language='javascript' type='text/JavaScript'>\n");
        Response.Write("alert('資料更新 完成!!!');\n");
        //Response.Write("opener.document.location.reload(); window.opener=null; window.close();\n");

        Response.Write("location = 'night_inspection_query.aspx';\n");

        //Response.Write(" window.navigate(window.location.href);\n");

        Response.Write("window.opener.location=window.opener.location;\n");
        //Response.Write("opener.document.location.reload();\n");
        Response.Write("window.opener=null; window.close();\n");
        Response.Write("</script>");
       
       

        
        //Button2 = (Button)e.Row.FindControl("btnDel");
        //btnDel.CommandArgument = ((Label)e.Row.FindControl("lblDefectType")).Text;
        //Button2.Attributes["onclick"] = "javascript:return confirm('確認修改否? [狀態:" + DropDownList4.SelectedValue + "]');";

       // Page_Load(null, null);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        Response.Write("<script language='javascript'>\n");
        Response.Write("window.open('UPLOAD.aspx?sn=" + Label1.Text + "', '_blank', 'height=800, width=1000, left=0, top=0, location=no,	menubar=no, resizable=yes, scrollbars=yes, titlebar=no, toolbar=no', true);\n");
        //Response.Write("window.opener=null; window.close();\n");
        Response.Write("</script>");

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>\n");
        Response.Write("window.open('UPLOAD.aspx?sn=" + Label1.Text + "', '_blank', 'height=800, width=1000, left=0, top=0, location=no,	menubar=no, resizable=yes, scrollbars=yes, titlebar=no, toolbar=no', true);\n");
        //Response.Write("window.opener=null; window.close();\n");
        Response.Write("</script>");
    }
}
