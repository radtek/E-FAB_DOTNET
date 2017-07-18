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
using System.Net;
using System.Text;
using System.Net.Mail;

public partial class check_onduty_mail_check_duty_time : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["onduty_check_mail"];

    string sql = "";
    string sql_temp = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    ArrayList arlist = new ArrayList();
    string after_five_day = DateTime.Now.AddDays(+5).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");


    protected void Page_Load(object sender, EventArgs e)
    {
        ds_temp1 = bind_data1();

        for (int i = 0; i <= ds_temp1.Tables[0].Rows.Count-1; i++)
        {

            call_mail(ds_temp1.Tables[0].Rows[i]["name"].ToString(), ds_temp1.Tables[0].Rows[i]["class"].ToString(), ds_temp1.Tables[0].Rows[i]["e_mail"].ToString());
        }

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
  

    }

    private DataSet bind_data1()
    {

        string sql = " SELECT  t.*,t1.e_mail                                 " +
" FROM onduty_schedule t,user_info t1                   " +
" where t.shift_date=format('" + after_five_day + "','yyyy/MM/dd')  " +
" and t.name=t1.username  and t.flag='Y'                               ";


        ds_temp = func.get_dataSet_access(sql, conn);

     

        return ds_temp;
    }

    public void call_mail(string name1, string class1,string e_mail1)
    {


        WebClient w = new WebClient();
        w.Encoding = Encoding.GetEncoding("Big5");
       // string strHTML = w.DownloadString("http://t1cimweb01/onduty_dotnet/record_assign_mail_sample.aspx?seq=" + sn + "");

        string strHTML = "";

        //string sql = " select count(*) as count_num from ( " +
        //" " +
        //" SELECT " +
        //" format(t1.date_time,'YYYY/MM/DD') as " +
        //" date_time,t1.stock_name,t1.stock_id,t1.price,round(t1.percentage*100,2)&'%' " +
        //" as percentage,t1.volumn,round(t1.K_percentage*100,2)&'%' as K_percentage FROM " +
        //" strong_up t1,(select stock_id from volumn t2 where t2.date_time>=format(now()- " +
        //" 7,'YYYY/MM/DD') )t3 where t1.date_time>format(now()-1,'YYYY/MM/DD') and t1.stock_id in (t3.stock_id) " +
        //" " +
        //" ) " +
        //" " +
        //" ";
        //ds_temp = func.get_dataSet_access(sql, conn);


        string title = "【溫馨提醒】 " + after_five_day + " " + name1 + " 您  值【 " + class1 + "  】班  (無內文)";
        //ArrayList maillist = func.FileToArray(Server.MapPath(".") + "\\maillist\\onduty.txt");
        //string receive = maillist[0].ToString();

       // string sql_receive = "select t.email from onduty_email_list t where t.cname='" + receive1 + "' ";

        //ds_temp1 = func.get_dataSet_access(sql_receive, conn);

        //string receive = ds_temp1.Tables[0].Rows[0]["email"].ToString();


        //receive = "oscar.hsieh@chimei-innolux.com";


        SendEmail("Onduty_Server@chimei-innolux.com", e_mail1, title, strHTML, "");// 
        //SendEmail("vsoscar@ms26.url.com.tw", "vsoscar2003@yahoo.com.hk", title, strHTML, "");//測試寄送程式 
        //jrrsc@ms96.url.com.tw 
        //fdlsongyy888@hotmail.com,vsoscar2003@yahoo.com.hk,liu.chang@msa.hinet.net,phyllis0531@hotmail.com,m8903157@yahoo.com.tw,benjamin6522@hotmail.com,dai.ww@msa.hinet.net,vrmouse@hotmail.com,roger.liu@infomax.com.tw,Yu.ChiaHao@gmail.com,chin-1520@yahoo.com.tw,heavenlibra@yahoo.com.tw,scm.shen@msa.hinet.net,bakery1202@gmail.com 
        //write_log("MAIL"); 
        //WebClient w1 = new WebClient();
        //w1.Encoding = Encoding.GetEncoding("utf-8");
        //string strHTML1 = w.DownloadString("http://vsoscar.no-ip.org/stock/three_top_open_cross.php");
        //SAVE_CREATE_FILE(strHTML1); 
        //Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
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
            //email.CC.Add(cca); 
            email.Bcc.Add(cca);
        }

        email.IsBodyHtml = true;
        smtp.Send(email);


    } 

}
