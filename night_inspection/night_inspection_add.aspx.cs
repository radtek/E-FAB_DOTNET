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
using System.Net.Mail;
using System.Net;
using System.Net.Sockets;
using System.Text;

public partial class night_inspection_night_inspection_add : System.Web.UI.Page
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
            string sql = "select max(t.sn)+1 as sn from night_inspection_record t";

           // Label1.Text = DateTime.Now.AddDays(+0).ToString("yyyyMMddHHmmss");
            //Label1.Text = func.get_dataSet_access(sql, conn).Tables[0].Rows[0]["SN"].ToString();

            ArrayList arlist_temp1=new ArrayList();

           

            arlist_temp1=func.FileToArray(Server.MapPath(".")+"\\config\\hour.txt");

            DropDownList5.DataSource = arlist_temp1;
            DropDownList5.DataBind();

            DropDownList5.Text = DateTime.Now.ToString("HH");


            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\min.txt");

            DropDownList8.DataSource = arlist_temp1;
            DropDownList8.DataBind();

            DropDownList8.Text = DateTime.Now.ToString("mm");

            //arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\flag_type.txt");

            //DropDownList4.DataSource = arlist_temp1;

            //DropDownList4.DataBind();


            txtEstimateStartDate.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd"));


            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\abnormal_type.txt");

            DropDownList1.DataSource = arlist_temp1;

            DropDownList1.DataBind();

            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\abnormal_area.txt");

            DropDownList2.DataSource = arlist_temp1;
            DropDownList2.DataBind();




            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\dep.txt");

            DropDownList3.DataSource = arlist_temp1;
            DropDownList3.DataBind();

            TextBox2.Text = "N/A";
            Label2.Visible = false;


        }

        Label1.Text = DateTime.Now.AddDays(+0).ToString("yyyyMMddHHmmss");
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
        string sql_str = " insert into night_inspection_record " +
"   (sn,                              " +
"    record_person,                   " +
"    start_time,                      " +
"    abnormal_type,                   " +
"    abnormal_area,                   " +
"    dep,                             " +
"    abnormal_description,            " +
"    area_owner,                      " +
"    area_owner_phone,                " +
//"    policy,                          " +
"    open_close_flag,                 " +
//"    close_person,                    " +
"    DTTM_CRESTE,DTTM_CLOSE)                            " +
" values                              " +
"   (" + Label1.Text+ ",                            " +
"    '" + TextBox1 .Text+ "',                 " +
"    to_date('" + txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd") + " " + DropDownList5.SelectedValue + ":" + DropDownList8 .SelectedValue+ "','yyyy/MM/dd hh24:mi')" + ",                    " +
"    '" + DropDownList1.SelectedValue+ "',                 " +
"    '" + DropDownList2.SelectedValue+ "',                 " +
"    '" + DropDownList3 .SelectedValue+ "',                           " +
"    '" + TextBox5.Text + "',          " +
"    '" + TextBox4 .Text+ "',                    " +
"    '" + TextBox2 .Text+ "',              " +
//"    '" + TextBox6 .Text+ "',                        " +
"    'Open',               " +
//"    '" + TextBox3 .Text+ "',                  " +
"    sysdate,'')                          ";

       func.get_sql_execute(sql_str, conn);

       Label2.Visible=true;
       Label2.Text = "  資料新增 完成 !!!";

       Label2.ForeColor = Color.Red;


       #region MyRegion

       string sql_mail = " select t2.cname,t2.email,t2.ext,t2.writedate from fwempinfo  t2 " +
" where t2.cname='" + TextBox4.Text + "'                                         " +
" and t2.writedate in(                                            " +
"                                                                 " +
" select max(t.writedate) as writedate                            " +
"   from fwempinfo t                                              " +
"  where t.cname = '" + TextBox4.Text + "'                                       " +
"                                                                 " +
" )                                                               ";

       ds_temp1 = func.get_dataSet_access(sql_mail, conn1);

       WebClient w = new WebClient();
       w.Encoding = Encoding.GetEncoding("Big5");
       string strHTML = w.DownloadString("http://t1cimweb01/E-FAB_dotnet/night_inspection/night_inspection_data.aspx?sn=" + Label1.Text);
       string title = "【主管夜巡系統】" + DateTime.Now.ToString("yyyy/MM/dd") + "  您被 Assign ==>SN:" + Label1.Text;
       //SendEmail("Alarm_Server@innolux.com.tw", "oscar.hsieh@inl,bunny.su@inl", title, strHTML, "");//
       
        //Session["sn"] = ds_temp1.Tables[0].Rows[0]["email"];
       //ArrayList maillist = new ArrayList();
       //maillist = func.FileToArray(Server.MapPath("..\\") + "\\maillist\\ArrayFlow.txt");
       string receive_man = "oscar.hsieh@chimei-innolux.com";
       if (ds_temp1.Tables[0].Rows.Count >0)
       {
           receive_man = ds_temp1.Tables[0].Rows[0]["email"].ToString();
           SendEmail("cim.alarm@chimei-innolux.com", receive_man, title, strHTML, "");//


       }
       
      
       #endregion


       Response.Write("<script language='javascript' type='text/JavaScript'>\n");
       Response.Write("alert('新增成功!!');\n");
       Response.Write("location = 'night_inspection_query.aspx';\n");
       //Response.Write("window.opener.location = window.opener.location;\n");
       //Response.Write("opener.document.location.reload();\n");
       Response.Write("window.opener=null; window.close();\n");
       Response.Write("</script>");
      
       




      // Page_Load(null, null);

       //Response.Redirect("night_inspection_add.aspx");

    }

    public static void SendEmail(string from, string to, string subject, string body, string cca)
    {
        SmtpClient smtp = new SmtpClient("10.56.130.63");
        MailMessage email = new MailMessage(from, to, subject, body);
        if (cca == "")
        {
        }
        else
        {
            email.CC.Add(cca);
        }

        email.IsBodyHtml = true;
        smtp.Send(email);


    } 

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>\n");
        Response.Write("window.open('UPLOAD.aspx?sn="+Label1.Text+"', '_blank', 'height=800, width=1000, left=0, top=0, location=no,	menubar=no, resizable=yes, scrollbars=yes, titlebar=no, toolbar=no', true);\n");
        //Response.Write("window.opener=null; window.close();\n");
        Response.Write("</script>");
    }
}
