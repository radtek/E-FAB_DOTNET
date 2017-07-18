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
using System.IO;
using System.Text;
using System.Xml;

public partial class Alarm_ONDUTY_EARTHQUACK_SMS : System.Web.UI.Page
{
    private WebReference.wsAlarmMsg.Service F_ws;
    private WebReference.wsAlarmMsg.clsAlarmMessage F_AlarmMsg;
    private string tmpHostIP = "";

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string SaveLocation = "";
    Int32 counter_oscar = 0;
    func xmlw = new func();
    func.alarm_format alarm_format = new func.alarm_format();
    string[] item ={ "點檢OK", "點檢NG", "RE點檢OK" };
    StreamWriter sw;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList1.DataSource = item;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "請選擇");

            TextBox_trx_id.Text = "1";
            TextBox_type_id.Text = "1";
            TextBox_fab_id.Text = "T1ARRAY";
            TextBox_sys_type.Text = "EARTHQUACK";

            TextBox_eq_id.Text = "CIM";
            TextBox_alarm_id.Text = "MSG";
            TextBox_alarm_text.Text = "T1 Alarm Server Test";
            TextBox_mail_contenttype.Text = "T";
            TextBox_alarm_comment.Text = "T1 Alarm Server Test Test";
            TextBox_pc_ip.Text = "1";
            TextBox_pc_name.Text = "1";
            TextBox_operator.Text = "1";
            TextBox_issue_date.Text = "1";

            alarm_format.trx_id = "1";
            alarm_format.type_id = "1";
            alarm_format.fab_id = "T1ARRAY";
            alarm_format.sys_type = "EARTHQUACK";
            alarm_format.eq_id = "CIM";
            alarm_format.alarm_id = "MSG";
            alarm_format.alarm_text = "T1 Alarm Server Test";
            alarm_format.mail_contenttype = "T";
            alarm_format.alarm_comment = "T1 Alarm Server Test Test";
            alarm_format.pc_ip = "1";
            alarm_format.pc_name = "1";
            alarm_format.operator1 = "1";
            alarm_format.issue_date = "1";
        
        
        }

        F_ws = new WebReference.wsAlarmMsg.Service();
        F_AlarmMsg = new WebReference.wsAlarmMsg.clsAlarmMessage();



    }
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue.Equals("請選擇"))
        {
        string  sql_temp2 = " <script language='javascript' type='text/JavaScript'>" +
        " alert('請選擇 功能 !!!'); " +
        " </script> ";

        string frmSscript = sql_temp2;
        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmSscript); 
        }
       

       



        string alarm_text = TextBox_alarm_text.Text;
        alarm_format.fab_id = "T1ARRAY";
        alarm_format.sys_type = TextBox_sys_type.Text;
        alarm_format.eq_id = TextBox_eq_id.Text;
        alarm_format.alarm_id = TextBox_alarm_id.Text;
        alarm_format.alarm_text = alarm_text;
        alarm_format.alarm_comment = alarm_text;

        DirectoryInfo di;
        FileInfo fi;
        int now_hour = Convert.ToInt32(DateTime.Now.ToString("HH"));//抓取執行當下小時
        int now_min = Convert.ToInt32(DateTime.Now.ToString("mm"));//抓取執行當下分鐘

        //di = new DirectoryInfo(Server.MapPath("..\\") + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd")); //DateTime.Now.ToString("yyyyMMdd")		

        fi = new FileInfo(Server.MapPath("..\\") + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log");


        //if (!di.Exists)
        //{
        //    di.Create();
        //}

        //如果檔案存在則開啟覆寫，如果不存在則建立新的檔案
        //StreamWriter sw;
        if (fi.Exists == true)
        {
            sw = File.AppendText(Server.MapPath("..\\") + "\\LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log");
        }
        else
        {
            sw = fi.CreateText();
        }

        sw.WriteLine("Create log file");
        sw.WriteLine(DateTime.Now.ToString("u") + "EARTHQUACK_SMS Program Start");

        this.Alarm_create_xml(alarm_format, "EARTHQUACK", "EARTHQUACK_SMS");
        if (DropDownList1.SelectedValue.Equals("點檢OK"))
        {
            sw.WriteLine("EARTHQUACK_SMS Check OK log finish");
        }

        if (DropDownList1.SelectedValue.Equals("點檢NG"))
        {
            sw.WriteLine("EARTHQUACK_SMS Check NG log finish");
        }

        if (DropDownList1.SelectedValue.Equals("RE點檢OK"))
        {
            sw.WriteLine("EARTHQUACK_SMS Re Check OK log finish");
        }
      
        sw.WriteLine(DateTime.Now.ToString("u") + "EARTHQUACK_SMS Program End");
        sw.WriteLine("");
        sw.Close();



       

    }

    public void Alarm_create_xml(func.alarm_format alarm_format, string sys_id, string inxml_file_name)
    {
        DataSet ds_insertDB = new DataSet();
        string sysid = sys_id;
        string xml_file_name = "Sys";
        ArrayList element = new ArrayList();
        ArrayList element_text = new ArrayList();
        StreamWriter sw_oscar;
        System.Text.Encoding encode = System.Text.Encoding.GetEncoding("big5");
        //StringWriter stringWriter = new StringWriterWithEncoding(Encoding.UTF8);



        DirectoryInfo di_oscar = new DirectoryInfo(Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd")); //DateTime.Now.ToString("yyyyMMdd")		

        FileInfo fi_oscar = new FileInfo(Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + inxml_file_name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + counter_oscar.ToString() + ".xml");
        SaveLocation = Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + inxml_file_name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + counter_oscar.ToString() + ".xml";


        if (!di_oscar.Exists)
        {
            di_oscar.Create();
        }

        //如果檔案存在則開啟覆寫，如果不存在則建立新的檔案
        //StreamWriter sw;
        if (fi_oscar.Exists == true)
        {
            sw_oscar = File.AppendText(Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + inxml_file_name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + counter_oscar.ToString() + ".xml");
        }
        else
        {
            sw_oscar = fi_oscar.CreateText();
        }

        string xml_content = @"<?xml version=""1.0"" encoding=""big5""?><transaction><trx_id>AUTOREPORT</trx_id><type_id>1</type_id><fab_id>{0}</fab_id><sys_type>{1}</sys_type><eq_id>{2}</eq_id><alarm_id>{3}</alarm_id><alarm_text>{4}</alarm_text><mail_contenttype>T</mail_contenttype><alarm_comment value = ""{5}"" /><pc_ip>172.20.7.120</pc_ip><pc_name>AMS01</pc_name><operator>AMS01</operator><issue_date>20110804104843</issue_date></transaction>";


        xml_content = string.Format(xml_content, alarm_format.fab_id, alarm_format.sys_type, alarm_format.eq_id, alarm_format.alarm_id, alarm_format.alarm_text, alarm_format.alarm_comment);

       

        sw_oscar.WriteLine(xml_content);

        sw_oscar.Close();
        //Upload("at.txt", "172.16.12.122", "anonymous", "");
        //func.Upload(SaveLocation, "172.16.12.124", "anonymous", "");


        counter_oscar++;


       // XmlDocument doc = new XmlDocument(new XmlDeclaration("1.0", "big5", null), XmlElement.Parse(xml_content));
        
         if (DropDownList1.SelectedValue.Equals("請選擇"))
        {
        string  sql_temp2 = " <script language='javascript' type='text/JavaScript'>" +
        " alert('請選擇 功能!!!'); " +
        " </script> ";

        string frmSscript = sql_temp2;
        //緧?javascript 
        this.Page.RegisterStartupScript("", frmSscript); 
        }
        
       
        //F_ws.Url = "http://" + txtHostIP.Text + "/wsAlarmMsg/Service.asmx";//隤銵頛詨?       
        //F_ws.Url = "http://" + tmpHostIP + "/wsAlarmMsg/Service.asmx";//隤槫敺
        F_AlarmMsg.trx_id = TextBox_trx_id.Text;
        F_AlarmMsg.type_id = TextBox_type_id.Text.Trim();
        F_AlarmMsg.fab_id = TextBox_fab_id.Text.Trim();
        F_AlarmMsg.sys_type = TextBox_sys_type.Text.Trim();
        F_AlarmMsg.eq_id = TextBox_eq_id.Text.Trim();
        F_AlarmMsg.alarm_id = TextBox_alarm_id.Text.Trim();
        F_AlarmMsg.alarm_text = TextBox_alarm_text.Text.Trim();
        F_AlarmMsg.mail_contenttype = TextBox_mail_contenttype.Text.Trim();//txtmail_contenttype.Text.Trim();
        F_AlarmMsg.alarm_comment = TextBox_alarm_comment.Text.Trim();
        F_AlarmMsg.pc_ip = TextBox_pc_name.Text.Trim();
        F_AlarmMsg.pc_name = TextBox_pc_name.Text.Trim();
        F_AlarmMsg.Operator = TextBox_operator.Text.Trim();
        F_AlarmMsg.issue_date = TextBox_issue_date.Text.Trim();




    }//end of create_xml


   

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue.Equals("點檢OK"))
        {
            TextBox_alarm_text.Text = "XXXX年:XX月:XX日 XX時:XX分 發生 XX 事件,經 CIM 確認系統正常";
            alarm_format.alarm_text = "XXXX年:XX月:XX日 XX時:XX分 發生 XX 事件,經 CIM 確認系統正常";
            TextBox_alarm_comment.Text = "XXXX年:XX月:XX日 XX時:XX分 發生 XX 事件,經 CIM 確認系統正常";
            alarm_format.alarm_comment = "XXXX年:XX月:XX日 XX時:XX分 發生 XX 事件,經 CIM 確認系統正常";
        }

        if (DropDownList1.SelectedValue.Equals("點檢NG"))
        {
            TextBox_alarm_text.Text = "XXXX年:XX月:XX日 XX時:XX分 發生 XX 事件, CIM XX系統異常,暫停服務,待系統回復後另行公告";
            alarm_format.alarm_text = "XXXX年:XX月:XX日 XX時:XX分 發生 XX 事件, CIM XX系統異常,暫停服務,待系統回復後另行公告";
            TextBox_alarm_comment.Text = "XXXX年:XX月:XX日 XX時:XX分 發生 XX 事件, CIM XX系統異常,暫停服務,待系統回復後另行公告";
            alarm_format.alarm_comment = "XXXX年:XX月:XX日 XX時:XX分 發生 XX 事件, CIM XX系統異常,暫停服務,待系統回復後另行公告";
        }


        if (DropDownList1.SelectedValue.Equals("RE點檢OK"))
        {
            TextBox_alarm_text.Text = "XXXX年:XX月:XX日 XX時:XX分 發生 XX 事件, CIM XX系統異常,於XX月:XX日 XX時:XX分回復正常";
            alarm_format.alarm_text = "XXXX年:XX月:XX日 XX時:XX分 發生 XX 事件, CIM XX系統異常,於XX月:XX日 XX時:XX分回復正常";
            TextBox_alarm_comment.Text = "XXXX年:XX月:XX日 XX時:XX分 發生 XX 事件, CIM XX系統異常,於XX月:XX日 XX時:XX分回復正常";
            alarm_format.alarm_comment = "XXXX年:XX月:XX日 XX時:XX分 發生 XX 事件, CIM XX系統異常,於XX月:XX日 XX時:XX分回復正常";
        }


    }
}
