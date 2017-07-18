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
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Net.Mail;

public partial class epaper_ARRAY_Array_scrap_detail_epaper1 : System.Web.UI.Page
{
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE"];
    string conn2 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_SSODB_OLE"];

    string sql = "";
    string sql1 = "";

    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();

    string Today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    string yesterday = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

    protected void Page_Load(object sender, EventArgs e)
    {
        //**************針對 Title 呈現  Status  start *********//
        string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE"];
        string conn2 = System.Configuration.ConfigurationSettings.AppSettings["RPTEST"];
        DataSet ds_temp = new DataSet();
        sql = @"

         select (case                                                               
        when substr(t.lot_id, 6, 1) = 'A' then                              
         'TFT'                                                              
        else                                                                
         'TP'                                                               
      end) as CATEGORY,                                                     
      t.lot_id,                                                             
      t.lot_type_attribute as TYPE,
      t.cmmt_code as Scrap_type,                                                  
      case when  t.cmmt_code='Unscrap'  then -t.scrap_qty
               else t.scrap_qty end  as QTY,   
                                                      
      t.stage,                                                              
      to_char(t.claim_dttm,'YYYY/MM/DD HH24:MM') as ScrapTime,              
      Convert( t.SCRAP_CMMT , 'UTF8','ZHT16MSWIN950' ) as SCRAP_CMMT  
                                                                            
 from innrpt.scrap_lot_history t                                            
where t.lot_type <> 'MQC'                                                   
  and t.shop = 'T0Array'                                                    
  and to_char(t.claim_dttm, 'YYYY/MM/DD HH24:MM')>='{0} 07:00'
  and to_char(t.claim_dttm, 'YYYY/MM/DD HH24:MM')<'{1} 06:59'     
                                                                            
order by (case                                                              
           when substr(t.lot_id, 6, 1) = 'A' then                           
            'TFT'                                                           
           else                                                             
            'TP'                                                            
         end) desc,                                                         
         t.claim_dttm  
";
        sql = string.Format(sql, yesterday, Today);

        sql = "select sum(ot.qty) as count  from(" + sql + ") ot";
        ds_temp1 = func.get_dataSet_access(sql, conn1);


        sql1 = @"

         select (case                                                               
        when substr(t.lot_id, 6, 1) = 'A' then                              
         'TFT'                                                              
        else                                                                
         'TP'                                                               
      end) as CATEGORY,                                                     
      t.lot_id,                                                             
      t.lot_type_attribute as TYPE,
      t.cmmt_code as Scrap_type,                                                  
      case when  t.cmmt_code='Unscrap'  then -t.scrap_qty
               else t.scrap_qty end  as QTY,   
                                                      
      t.stage,                                                              
      to_char(t.claim_dttm,'YYYY/MM/DD HH24:MM') as ScrapTime,              
      Convert( t.SCRAP_CMMT , 'UTF8','ZHT16MSWIN950' ) as SCRAP_CMMT  
                                                                            
 from innrpt.scrap_lot_history t                                            
where t.lot_type <> 'MQC'                                                   
  and t.shop = 'T1Array'                                                    
  and to_char(t.claim_dttm, 'YYYY/MM/DD HH24:MM')>='{0} 07:00'
  and to_char(t.claim_dttm, 'YYYY/MM/DD HH24:MM')<'{1} 06:59'     
                                                                            
order by (case                                                              
           when substr(t.lot_id, 6, 1) = 'A' then                           
            'TFT'                                                           
           else                                                             
            'TP'                                                            
         end) desc,                                                         
         t.claim_dttm  
";
        sql1 = string.Format(sql1, yesterday, Today);

        sql1 = "select sum(ot.QTY) as count  from(" + sql1 + ") ot";
        ds_temp2 = func.get_dataSet_access(sql1, conn1);


        //**************針對 Title 呈現  Status  end *********//

        WebClient w = new WebClient();
        w.Encoding = Encoding.GetEncoding("big5");
        string strHTML = w.DownloadString("http://t1cimweb01/E-FAB_dotnet/epaper/ARRAY/sample/Array_scrap_detail1.aspx?area=T0");
        string strHTML1 = w.DownloadString("http://t1cimweb01/E-FAB_dotnet/epaper/ARRAY/sample/Array_scrap_detail1.aspx?area=T1");
        string title = "[CIM 電子報系統] " + DateTime.Now.ToString("yyyy/MM/dd") + " T0Array ScrapDetail 【共 " + ds_temp1.Tables[0].Rows[0][0] + "  片資料 】 ";
        string title1 = "[CIM 電子報系統] " + DateTime.Now.ToString("yyyy/MM/dd") + " T1Array ScrapDetail 【共 " + ds_temp2.Tables[0].Rows[0][0] + " 片資料 】 ";

        //SendEmail("Alarm_Server@innolux.com.tw", "oscar.hsieh@inl,bunny.su@inl", title, strHTML, "");//

        ArrayList maillist = new ArrayList();
        maillist = func.FileToArray(Server.MapPath("..\\") + "\\maillist\\T0Array_Scrap_detail.txt");
        //maillist[0] = "oscar.hsieh@innolux.com";


        ArrayList maillist1 = new ArrayList();
        maillist1 = func.FileToArray(Server.MapPath("..\\") + "\\maillist\\T1Array_Scrap_detail.txt");
        //maillist1[0] = "oscar.hsieh@innolux.com";
        SendEmail("cim.alarm@chimei-innolux.com", maillist[0].ToString(), title, strHTML, "");//
        SendEmail("cim.alarm@chimei-innolux.com", maillist1[0].ToString(), title1, strHTML1, "");//

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
    }



    public static void SendEmail(string from, string to, string subject, string body, string cca)
    {
        SmtpClient smtp = new SmtpClient("10.56.196.147");
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
}
