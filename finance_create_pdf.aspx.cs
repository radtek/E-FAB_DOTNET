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
using System.Net.Mail;

public partial class epaper_finance_create_pdf : System.Web.UI.Page
{
    string today_yyyymmdd = DateTime.Now.AddDays(+0).ToString("yyyyMMdd");
    string today_yyyymmddHH = DateTime.Now.AddDays(+0).ToString("yyyyMMddHH");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyyMMddHHmmss");
    string today_HH = DateTime.Now.AddDays(+0).ToString("HH");
    string tool = "";
    string website = "";
    string title = "";
    string strHTML = "";
    string mail_list = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        member oscar = new member();

        oscar.today_detail = DateTime.Now.AddDays(+0).ToString("yyyyMMddHHmmss");

        oscar.tool = Server.MapPath(".") + "\\wkhtmltopdf\\bin\\wkhtmltopdf.exe";
        //http://t1cimweb02/EDA_EYE/cf/monitor/cf_daily_monitor.aspx
        //http://10.56.131.22/onduty_dotnet2
        //http://10.56.131.22/onduty_dotnet2/Onduty_check/show_login.aspx
        oscar.website = "http://10.56.131.22/onduty_dotnet2/onduty_query.aspx";

        oscar.title = " 值班紀錄快遞【" + today_yyyymmddHH + "】";

        oscar.strHTML = "值班的路上 平安喜樂<br>";
        oscar.mail_list = "oscar.hsieh@innolux.com";

        PDF_FACTORY(oscar);



        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null;window.open('','_self');  window.close();\",null)</script>");

    }

    private void PDF_FACTORY(member oscar)
    {
        Send_PDF(oscar.tool, oscar.title, oscar.strHTML, oscar.title, oscar.website,oscar.today_detail);
        SendEmail("cim.alarm@innolux.com", oscar.mail_list, oscar.title, oscar.strHTML, "", Server.MapPath(".") + "\\File\\" + oscar.today_detail + ".pdf");//

    }

    public class member 

    {
        

        private string _today_detail;
        public string today_detail
        {
            set { _today_detail = value; }
            get { return _today_detail; }
        }

        private string _tool;
        public string tool
        {
            set { _tool = value; }
            get { return _tool; }
        }
        private string _website;
        public string website
        {
            set { _website = value; }
            get { return _website; }
        }
        private string _title;
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }

        private string _strHTML;
        public string strHTML
        {
            set { _strHTML = value; }
            get { return _strHTML; }
        }

        private string _mail_list;
        public string mail_list
        {
            set { _mail_list = value; }
            get { return _mail_list; }
        }
       




    } 


    private void Send_PDF(string tool, string title, string strHTML, string mail_list, string website,string _today_detail)
    {
        func.CreatePDF(website, tool, Server.MapPath(".") + "\\File\\" + _today_detail + ".pdf");
        func.write_log("Send 值班快遞==> ", Server.MapPath(".") + "\\LOG\\", "log");
        func.delete_log_file(Server.MapPath(".") + "\\LOG\\", "*.log", -30);
        func.delete_log_file(Server.MapPath(".") + "\\File\\", "*.pdf", -3);

        System.Threading.Thread.Sleep(80000);
    }
    public static void SendEmail(string from, string to, string subject, string body, string cca, string file_path)
    {
        SmtpClient smtp = new SmtpClient("10.56.196.147");
        MailMessage email = new MailMessage(from, to, subject, body);
        if (cca == "")
        {
        }
        else
        {
            email.CC.Add(cca);
            //email.Bcc.Add(cca);
        }

        if (!file_path.Equals(""))
        {
            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(file_path);
            email.Attachments.Add(attachment);

        }



        email.IsBodyHtml = true;
        smtp.Send(email);


    } 
}
