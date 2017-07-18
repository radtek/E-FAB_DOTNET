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
using IS.util;
using System.IO;
using Innolux.Portal.CommonFunction;


public partial class Alarm_Holiday_inout_SMS_FOR_C2 : System.Web.UI.Page
{
    IS.util.special sp = new IS.util.special();
    //file f = new file();
    StreamWriter sw;
    FileInfo fi;
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE"];
    string conn_ary = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_RPTDW"];
    string conn_cel = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];

    //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_MIS"];
    func fc = new func();

    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_lineyield="";


    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    DataSet ds_temp4 = new DataSet();



    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string SaveLocation = "";
    Int32 counter_oscar = 0;
    func xmlw = new func();
    func.alarm_format alarm_format = new func.alarm_format();



    //special sp = new special();
    // file f = new file();

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
       func.Upload(SaveLocation, "172.16.12.124", "anonymous", "");


        counter_oscar++;
        //System.Text.Encoding encode = System.Text.Encoding.GetEncoding("big5"); 
        //element.Add("trx_id");

        //element_text.Add(alarm_format.trx_id);

        //element.Add("type_id");

        //element_text.Add(alarm_format.type_id);


        //element.Add("fab_id");

        //element_text.Add(alarm_format.fab_id);

        //element.Add("sys_type");
        //element_text.Add(alarm_format.sys_type);

        //element.Add("eq_id");
        //element_text.Add(alarm_format.eq_id);

        //element.Add("alarm_id");
        //element_text.Add(alarm_format.alarm_id);

        //element.Add("alarm_text");
        //element_text.Add(alarm_format.alarm_text);

        //element.Add("mail_contenttype");
        //element_text.Add(alarm_format.mail_contenttype);

        //element.Add("alarm_comment");
        //element_text.Add(alarm_format.alarm_comment);

        //element.Add("pc_ip");
        //element_text.Add(alarm_format.pc_ip);

        //element.Add("pc_name");
        //element_text.Add(alarm_format.pc_name);

        //element.Add("operator");
        //element_text.Add(alarm_format.operator1);

        //element.Add("issue_date");
        //element_text.Add(alarm_format.issue_date);




        //xml_file_name = sysid + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + "_" + inxml_file_name + ".xml";

        //xmlw.Create_Alarm_xml(Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "_ALARM_" + DateTime.Now.Hour.ToString(), xml_file_name, element, element_text);


        //// strClientIP = Request.ServerVariables["remote_host"].ToString(); 




        //SaveLocation = Server.MapPath(".") + "\\File\\" + DateTime.Now.ToString("yyyyMMdd") + "_ALARM_" + DateTime.Now.Hour.ToString() + "\\" + xml_file_name;





    }//end of create_xml
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在這裡放置使用者程式碼以初始化網頁

        if (!IsPostBack)
        {
            alarm_format.trx_id = "1";
            alarm_format.type_id = "1";
            alarm_format.fab_id = "T0ARRAY";
            alarm_format.sys_type = "WeekendReport";
            alarm_format.eq_id = "REPORT";
            alarm_format.alarm_id = "MSG";
            alarm_format.alarm_text = "T1 Alarm Server Test";
            alarm_format.mail_contenttype = "T";
            alarm_format.alarm_comment = "T1 Alarm Server Test Test";
            alarm_format.pc_ip = "1";
            alarm_format.pc_name = "1";
            alarm_format.operator1 = "1";
            alarm_format.issue_date = "1";


        }

        StreamWriter sw_oscar;



        // 在這裡放置使用者程式碼以初始化網頁
        //FileInfo fi ;
        DirectoryInfo di;
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
        sw.WriteLine(DateTime.Now.ToString("u") + "Holiday_inout_SMS_For_C2 Program Start");

      

        string sql_stm = @"

select Type,sum(SHIPQTY) as DOUT,Sum(Diff_Out) as MOUTDiff ,
            sum(INPUTQTY) as DIN ,sum(Diff_In) as MINDiff,
             TYPE||' O:'||sum(SHIPQTY)||'/'||Sum(Diff_Out)||', I:'||sum(INPUTQTY)||
             '/'||sum(Diff_In)
             as msg            
from(
   select Type,lottype,sum(SHIPQTY) as SHIPQTY,Sum(Diff_Out) as Diff_Out,sum(INPUTQTY) as INPUTQTY,
          sum(Diff_In) as Diff_In,sum(DScrapQty)as DScrapQty,
          round(sum(SHIPQTY)/decode((sum(SHIPQTY)+sum(DScrapQty)),0,1,(sum(SHIPQTY)+sum(DScrapQty)))*100,2) as LY,
           Decode(Type||lottype,'TFTP',
           round(sum(SHIPQTY)/decode((sum(SHIPQTY)+sum(DScrapQty)),0,1,(sum(SHIPQTY)+sum(DScrapQty)))*100,2),0) as LYTFTP,
           Decode(Type||lottype,'TFTE',
           round(sum(SHIPQTY)/decode((sum(SHIPQTY)+sum(DScrapQty)),0,1,(sum(SHIPQTY)+sum(DScrapQty)))*100,2),0) as LYTFTE,
           Decode(Type||lottype,'TPP',
           round(sum(SHIPQTY)/decode((sum(SHIPQTY)+sum(DScrapQty)),0,1,(sum(SHIPQTY)+sum(DScrapQty)))*100,2),0) as LYTPP,
           Decode(Type||lottype,'TPE',
           round(sum(SHIPQTY)/decode((sum(SHIPQTY)+sum(DScrapQty)),0,1,(sum(SHIPQTY)+sum(DScrapQty)))*100,2),0) as LYTPE

   from  (
       select     'TFT' as Type,k1.prod_name,t3.lot_type as lotType, 
           nvl(T3.IN_QTY,0) - nvl(T3.destroy_qty,0) + nvl(T3.canceldestroy_qty,0) as INPUTQTY ,
           T3.out_qty - 
           innrpt.RPT_T0ARRAYDAILYMEETING.get_single_product_unship_qty('T0Array',k1.prod_name,
              t3.lot_type,to_date((select to_char(sysdate-1,'yyyyMMdd') from dual)||'070000','yyyymmddhh24miss'),
              to_date((select to_char(sysdate,'yyyyMMdd') from dual)||'070000','yyyymmddhh24miss')) as SHIPQTY,
           innrpt.RPT_T0ARRAYDAILYMEETING.Get_Acc_Act_InPut_QTY_oscar('Fab1','T0Array',k1.prod_name,
                (select to_char(sysdate-1,'yyyyMMdd') from dual),t3.lot_type) -
           STDMAN_RPT.Get_Acc_Plan_QTY ('DTAGINPUT','Fab1','T0Array',k1.prod_name,'NA','NA','DAY', 
               (select to_char(sysdate-1,'yyyyMMdd') from dual),'NA','SUBSTRATE' , t3.lot_type  ) as Diff_In,           
           innrpt.RPT_T0ARRAYDAILYMEETING.Get_Acc_Act_OutPut_QTY('Fab1','T0Array',k1.prod_name,
               (select to_char(sysdate-1,'yyyyMMdd') from dual),t3.lot_type) -
           STDMAN_RPT.Get_Acc_Plan_QTY ('DTAGOUTPUT','Fab1','T0Array',k1.prod_name,'NA','NA','DAY', 
               (select to_char(sysdate-1,'yyyyMMdd') from dual),'NA','SUBSTRATE' , t3.lot_type  )  -
           innrpt.RPT_T0ARRAYDAILYMEETING.get_single_product_unship_qty('T0Array',k1.prod_name,t3.lot_type,
                to_date((select substr(to_char(sysdate-1,'yyyyMMdd'),0,6)||'01' from dual)||'070000','yyyymmddhh24miss'),to_date((select to_char(sysdate,'yyyyMMdd') from dual)||'070000','yyyymmddhh24miss')) as Diff_Out,
           nvl(T3.Scrap_Qty,0) as DScrapQty           
           from 
               (
               --Get Product list for TFT                  
                select ot3.prod_name  from STDMAN.e_fab_prod_setting OT3
                where 
                OT3.Section in ('TFT','IPS','EPD')
                --OT3.Section in ('TFT','IPS')
                and OT3.SHOP = 'T0ARRAY_DAILY'
                and OT3.e_Lot_Flag||OT3.p_Lot_Flag <> 'NN'
                 
		             ) k1, DAILY_IN_OUT_SUM t3
 		             where t3.shift_day_key = (select to_char(sysdate-1,'yyyyMMdd') from dual)||'.Fab1'
                and t3.prod_id= k1.prod_name||'.T0Array.Fab1' 
      union all (
      select     'TP'as Type,k1.prod_name,t3.lot_type,
           nvl(T3.IN_QTY,0) - nvl(T3.destroy_qty,0) + nvl(T3.canceldestroy_qty,0) as INPUTQTY,
           T3.out_qty - 
           innrpt.RPT_T0ARRAYDAILYMEETING.get_single_product_unship_qty('T0Array',k1.prod_name,
              t3.lot_type,to_date((select to_char(sysdate-1,'yyyyMMdd') from dual)||'070000','yyyymmddhh24miss'),
              to_date((select to_char(sysdate,'yyyyMMdd') from dual)||'070000','yyyymmddhh24miss')) as SHIPQTY,
           innrpt.RPT_T0ARRAYDAILYMEETING.Get_Acc_Act_InPut_QTY_oscar('Fab1','T0Array',k1.prod_name,(select to_char(sysdate-1,'yyyyMMdd') from dual),t3.lot_type) -
            STDMAN_RPT.Get_Acc_Plan_QTY ('DTAGINPUT','Fab1','T0Array',k1.prod_name,'NA','NA','DAY', 
           (select to_char(sysdate-1,'yyyyMMdd') from dual),'NA','SUBSTRATE' , t3.lot_type  ) as Diff_In,           
           innrpt.RPT_T0ARRAYDAILYMEETING.Get_Acc_Act_OutPut_QTY('Fab1','T0Array',k1.prod_name,(select to_char(sysdate-1,'yyyyMMdd') from dual),t3.lot_type) -
           STDMAN_RPT.Get_Acc_Plan_QTY ('DTAGOUTPUT','Fab1','T0Array',k1.prod_name,'NA','NA','DAY', 
                                                (select to_char(sysdate-1,'yyyyMMdd') from dual),'NA','SUBSTRATE' , t3.lot_type  )  -
           innrpt.RPT_T0ARRAYDAILYMEETING.get_single_product_unship_qty('T0Array',k1.prod_name,t3.lot_type,
           to_date((select substr(to_char(sysdate-1,'yyyyMMdd'),0,6)||'01' from dual)||'070000','yyyymmddhh24miss'),to_date((select to_char(sysdate,'yyyyMMdd') from dual)||'070000','yyyymmddhh24miss')) as Diff_Out,    
           nvl(T3.Scrap_Qty,0) as DScrapQty                  
           from 
               (
               --Get Product list for TP                
                select ot3.prod_name from STDMAN.e_fab_prod_setting OT3
                where 
                OT3.Section in ('METBRI','ITOBRI')
                and OT3.SHOP = 'T0ARRAY_DAILY'            
                and OT3.e_Lot_Flag||OT3.p_Lot_Flag <> 'NN'                      
		             ) k1, DAILY_IN_OUT_SUM t3
 		             where t3.shift_day_key = (select to_char(sysdate-1,'yyyyMMdd') from dual)||'.Fab1'
                and t3.prod_id= k1.prod_name||'.T0Array.Fab1'                                
         )
  )          
  group by Type,LotType         
) 
group by Type
order by Type











";


       

   

        //ds = db.GetDataset(sql_stm, 2);
        ds_temp3 = func.get_dataSet_access(sql_stm, conn_ary);

         sql_lineyield = @"select tb.type,tb.lottype,tb.SHIPQTY,tb.Diff_Out,tb.INPUTQTY,tb.Diff_In,tb.DScrapQty,
case when tb.LY=0 and tb.DScrapQty=0 then 'NA' else to_char(tb.LY) end LY  ,
case when tb.LY=0 and tb.DScrapQty=0 then 'NA' else  to_char(tb.LYTFTP) end LYTFTP,
case when tb.LY=0 and tb.DScrapQty=0 then 'NA' else  to_char(tb.LYTFTE) end LYTFTE,
case when tb.LY=0 and tb.DScrapQty=0 then 'NA' else  to_char(tb.LYTPP) end LYTPP,
case when tb.LY=0 and tb.DScrapQty=0 then 'NA' else  to_char(tb.LYTPE) end LYTPE
 from 
(
   select Type,lottype,sum(SHIPQTY) as SHIPQTY,Sum(Diff_Out) as Diff_Out,sum(INPUTQTY) as INPUTQTY,
          sum(Diff_In) as Diff_In,sum(DScrapQty)as DScrapQty,
          round(sum(SHIPQTY)/decode((sum(SHIPQTY)+sum(DScrapQty)),0,1,(sum(SHIPQTY)+sum(DScrapQty)))*100,2) as LY,
           Decode(Type||lottype,'TFTP',
           round(sum(SHIPQTY)/decode((sum(SHIPQTY)+sum(DScrapQty)),0,1,(sum(SHIPQTY)+sum(DScrapQty)))*100,2),0) as LYTFTP,
           Decode(Type||lottype,'TFTE',
           round(sum(SHIPQTY)/decode((sum(SHIPQTY)+sum(DScrapQty)),0,1,(sum(SHIPQTY)+sum(DScrapQty)))*100,2),0) as LYTFTE,
           Decode(Type||lottype,'TPP',
           round(sum(SHIPQTY)/decode((sum(SHIPQTY)+sum(DScrapQty)),0,1,(sum(SHIPQTY)+sum(DScrapQty)))*100,2),0) as LYTPP,
           Decode(Type||lottype,'TPE',
           round(sum(SHIPQTY)/decode((sum(SHIPQTY)+sum(DScrapQty)),0,1,(sum(SHIPQTY)+sum(DScrapQty)))*100,2),0) as LYTPE

   from  (
       select     'TFT' as Type,k1.prod_name,t3.lot_type as lotType, 
           nvl(T3.IN_QTY,0) - nvl(T3.destroy_qty,0) + nvl(T3.canceldestroy_qty,0) as INPUTQTY ,
           T3.out_qty - 
           innrpt.RPT_T0ARRAYDAILYMEETING.get_single_product_unship_qty('T0Array',k1.prod_name,
              t3.lot_type,to_date((select to_char(sysdate-1,'yyyyMMdd') from dual)||'070000','yyyymmddhh24miss'),
              to_date((select to_char(sysdate,'yyyyMMdd') from dual)||'070000','yyyymmddhh24miss')) as SHIPQTY,
           innrpt.RPT_T0ARRAYDAILYMEETING.Get_Acc_Act_InPut_QTY_oscar('Fab1','T0Array',k1.prod_name,
                (select to_char(sysdate-1,'yyyyMMdd') from dual),t3.lot_type) -
           STDMAN_RPT.Get_Acc_Plan_QTY ('DTAGINPUT','Fab1','T0Array',k1.prod_name,'NA','NA','DAY', 
               (select to_char(sysdate-1,'yyyyMMdd') from dual),'NA','SUBSTRATE' , t3.lot_type  ) as Diff_In,           
           innrpt.RPT_T0ARRAYDAILYMEETING.Get_Acc_Act_OutPut_QTY('Fab1','T0Array',k1.prod_name,
               (select to_char(sysdate-1,'yyyyMMdd') from dual),t3.lot_type) -
           STDMAN_RPT.Get_Acc_Plan_QTY ('DTAGOUTPUT','Fab1','T0Array',k1.prod_name,'NA','NA','DAY', 
               (select to_char(sysdate-1,'yyyyMMdd') from dual),'NA','SUBSTRATE' , t3.lot_type  )  -
           innrpt.RPT_T0ARRAYDAILYMEETING.get_single_product_unship_qty('T0Array',k1.prod_name,t3.lot_type,
                to_date((select substr(to_char(sysdate-1,'yyyyMMdd'),0,6)||'01' from dual)||'070000','yyyymmddhh24miss'),to_date((select to_char(sysdate,'yyyyMMdd') from dual)||'070000','yyyymmddhh24miss')) as Diff_Out,
           nvl(T3.Scrap_Qty,0) as DScrapQty           
           from 
               (
               --Get Product list for TFT                  
                select ot3.prod_name  from STDMAN.e_fab_prod_setting OT3
                where 
                OT3.Section in ('TFT','IPS','EPD')
                --OT3.Section in ('TFT','IPS')
                and OT3.SHOP = 'T0ARRAY_DAILY'
                and OT3.e_Lot_Flag||OT3.p_Lot_Flag <> 'NN'
                 
		             ) k1, DAILY_IN_OUT_SUM t3
 		             where t3.shift_day_key = (select to_char(sysdate-1,'yyyyMMdd') from dual)||'.Fab1'
                and t3.prod_id= k1.prod_name||'.T0Array.Fab1' 
      union all (
      select     'TP'as Type,k1.prod_name,t3.lot_type,
           nvl(T3.IN_QTY,0) - nvl(T3.destroy_qty,0) + nvl(T3.canceldestroy_qty,0) as INPUTQTY,
           T3.out_qty - 
           innrpt.RPT_T0ARRAYDAILYMEETING.get_single_product_unship_qty('T0Array',k1.prod_name,
              t3.lot_type,to_date((select to_char(sysdate-1,'yyyyMMdd') from dual)||'070000','yyyymmddhh24miss'),
              to_date((select to_char(sysdate,'yyyyMMdd') from dual)||'070000','yyyymmddhh24miss')) as SHIPQTY,
           innrpt.RPT_T0ARRAYDAILYMEETING.Get_Acc_Act_InPut_QTY_oscar('Fab1','T0Array',k1.prod_name,(select to_char(sysdate-1,'yyyyMMdd') from dual),t3.lot_type) -
            STDMAN_RPT.Get_Acc_Plan_QTY ('DTAGINPUT','Fab1','T0Array',k1.prod_name,'NA','NA','DAY', 
           (select to_char(sysdate-1,'yyyyMMdd') from dual),'NA','SUBSTRATE' , t3.lot_type  ) as Diff_In,           
           innrpt.RPT_T0ARRAYDAILYMEETING.Get_Acc_Act_OutPut_QTY('Fab1','T0Array',k1.prod_name,(select to_char(sysdate-1,'yyyyMMdd') from dual),t3.lot_type) -
           STDMAN_RPT.Get_Acc_Plan_QTY ('DTAGOUTPUT','Fab1','T0Array',k1.prod_name,'NA','NA','DAY', 
                                                (select to_char(sysdate-1,'yyyyMMdd') from dual),'NA','SUBSTRATE' , t3.lot_type  )  -
           innrpt.RPT_T0ARRAYDAILYMEETING.get_single_product_unship_qty('T0Array',k1.prod_name,t3.lot_type,
           to_date((select substr(to_char(sysdate-1,'yyyyMMdd'),0,6)||'01' from dual)||'070000','yyyymmddhh24miss'),to_date((select to_char(sysdate,'yyyyMMdd') from dual)||'070000','yyyymmddhh24miss')) as Diff_Out,    
           nvl(T3.Scrap_Qty,0) as DScrapQty                  
           from 
               (
               --Get Product list for TP                
                select ot3.prod_name from STDMAN.e_fab_prod_setting OT3
                where 
                OT3.Section in ('METBRI','ITOBRI')
                and OT3.SHOP = 'T0ARRAY_DAILY'            
                and OT3.e_Lot_Flag||OT3.p_Lot_Flag <> 'NN'                      
		             ) k1, DAILY_IN_OUT_SUM t3
 		             where t3.shift_day_key = (select to_char(sysdate-1,'yyyyMMdd') from dual)||'.Fab1'
                and t3.prod_id= k1.prod_name||'.T0Array.Fab1'                                
         )
  )          
  group by Type,LotType         
) tb

order by tb.type, case when tb.lottype='P' then 1 else 2 end";


         ds_temp4 = func.get_dataSet_access(sql_lineyield, conn_ary);

         DataTable dt_x = new DataTable();

         dt_x = ds_temp4.Tables[0];

        //table.Select(" id='id1' ")[0][" exp1 "];
        string LYTFTP = "NA";
        string LYTFTE = "NA";
        string LYTPP = "NA";
        string LYTPE = "NA";

        object x1 = dt_x.Compute( "COUNT(type)","type='TFT' and lottype='P'" );
        if (Convert.ToInt32(x1) > 0)
        {
            //DataRow  one time can get row[i]["columnName"]
            object LYTFT_P = dt_x.Select(" type='TFT' and lottype='P' ")[0]["LYTFTP"];
            
            LYTFTP = LYTFT_P.ToString();
            if (!LYTFTP.Equals("NA"))
            {
                if (Convert.ToDouble(LYTFT_P.ToString()) >= 100)
                {

                    LYTFTP = "100";
                }
            }
               
        }


        object x2 = dt_x.Compute("COUNT(type)", "type='TFT' and lottype='E'");
        if (Convert.ToInt32(x2) > 0)
        {
            object LYTFT_E = dt_x.Select(" type='TFT' and lottype='E' ")[0]["LYTFTE"];
            LYTFTE = LYTFT_E.ToString();
            if (!LYTFTE.Equals("NA"))
            {

                if (Convert.ToDouble(LYTFT_E.ToString()) >= 100)
                {

                    LYTFTE = "100";
                }
            }

           
        }

        object x3 = dt_x.Compute("COUNT(type)", "type='TP' and lottype='P'");
        if (Convert.ToInt32(x3) > 0)
        {
            object LYTP_P = dt_x.Select(" type='TP' and lottype='P' ")[0]["LYTPP"];
            LYTPP = LYTP_P.ToString();

               if (!LYTPP.Equals("NA"))
               {
                     if (Convert.ToDouble(LYTP_P.ToString()) >= 100)
            {

                LYTPP = "100";
            }

               }
         


        }

        object x4 = dt_x.Compute("COUNT(type)", "type='TP' and lottype='E'");
        if (Convert.ToInt32(x4) > 0)
        {
            object LYTP_E = dt_x.Select(" type='TP' and lottype='E' ")[0]["LYTPE"];
            LYTPE = LYTP_E.ToString();

               if (!LYTPE.Equals("NA"))
               {
                 
            if (Convert.ToDouble(LYTP_E.ToString()) >= 100)
            {

                LYTPE = "100";
            }


               
               }

        }

         string sms_content = "C_2 " + ds_temp3.Tables[0].Rows[0]["MSG"].ToString() + " LY:" + LYTFTP + "/" + LYTFTE + "," + ds_temp3.Tables[0].Rows[1]["MSG"].ToString() + " LY:" + LYTPP + "/" + LYTPE;

      

        #region
        
        #endregion


       

        //假日簡訊
        

        //STN+F-CTP+B-CTP簡訊
        //string STN_CTP = stn + TP_FCTP + TP_BCTP;
        

        //將假日簡訊和STN簡訊合併發送(移除良率)
        //s = s + STN_CTP;

        //msgtable = sp.gethtml_2(ds.Tables[0]);
        //Response.Write( msgtable );
        //Response.End() ;   

        //OEE KPI
        string sql_OEE_KPI = "";
        #region
        ///舊版OEE，Mark by bunny 20090820
        #endregion


        string msgtable_OEE_KPI = "";




        string s1 = "0";

        bool saturdaycheck = false;
        bool sundaycheck = false;
        if (DateTime.Today.ToString("yyyyMMdd") == "20090124")
        {
            saturdaycheck = true;
        }
        if (DateTime.Today.ToString("yyyyMMdd") == "20090125")
        {
            sundaycheck = true;
        }

        //s1 = db.Execute_Scalar("select count(*) from eis_holiday_maintain t where holiday_dttm='" + DateTime.Today.ToString("yyyy/MM/dd") + "'", 2);
        DataSet ds_temp = new DataSet();

        ds_temp = func.get_dataSet_access("select count(*) from eis_holiday_maintain t where holiday_dttm='" + DateTime.Today.ToString("yyyy/MM/dd") + "'", conn_cel);
        s1 = ds_temp.Tables[0].Rows[0][0].ToString();

          //if(1==1)
        if ((DateTime.Today.DayOfWeek == DayOfWeek.Saturday && saturdaycheck != true) || (DateTime.Today.DayOfWeek == DayOfWeek.Sunday && sundaycheck != true) || s1.Equals("1"))
            //if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday || DateTime.Today.DayOfWeek == DayOfWeek.Sunday || s1.Equals("1"))
            {
                // create_xml(s, 0, "REPORT");//SEND 假日簡訊

                string alarm_text = sms_content;
                alarm_format.alarm_text = alarm_text;
                alarm_format.alarm_comment = alarm_text;

                this.Alarm_create_xml(alarm_format, "Holiday", "Holiday_Inout_SMS_For_C2");
                sw.WriteLine("SMS_C2 log finish");
                sw.WriteLine("");

                //create_xml(stn,0,"STN");//STN簡訊
                //create_xml(STN_CTP,0,"STN");//STN+CTP簡訊
            }
            else
            {
                // create_xml(s, 1, "REPORT");// NOT SEND

                //string alarm_text = s;
                // alarm_format.alarm_text = alarm_text;
                //alarm_format.alarm_comment = alarm_text;

                //this.Alarm_create_xml(alarm_format, "Holiday", "Holiday_Inout_SMS");
                //sw.WriteLine("SMS log finish");
                // sw.WriteLine("");
                //create_xml(stn,1,"STN");
                //create_xml(STN_CTP,1,"STN");
            }

        sw.WriteLine(DateTime.Now.ToString("u") + "Holiday_Inout_SMS_For_C2 Program End");
        sw.WriteLine("");

        sw.Close();

        func.delete_log_dir(Server.MapPath(".") + "\\File\\", "*.*", -60);
        func.delete_log_file(Server.MapPath("..\\") + "\\LOG\\", "*.log", -60);

        //func.delete_log_dir(Server.MapPath("..\\") + "\\LOG\\", -60);
        //DeleteLogFile("");

        Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");

    }
}
