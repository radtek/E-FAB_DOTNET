using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.OracleClient;

public partial class EDA_EDA_READ_FILE_DATA : System.Web.UI.Page
{
    //private OracleConnection orcn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["EDALCDAPP"]);
    IS.util.special sp = new IS.util.special();
    //file f = new file();

    StreamWriter sw;
    DirectoryInfo di;//宣告目錄 
    FileInfo fi;//宣告檔案 


    //string conn = System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"];
    string conn = System.Configuration.ConfigurationSettings.AppSettings["TMS"];
    string conn_cel = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];
    string conn_pds = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_PDS_OLE_STD"];
    string conn_oeegw1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_MIDGW1"];

    func fc = new func();

    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string sql_temp4 = "";
    string sql_stm = "";

    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    DataSet ds_temp4 = new DataSet();

    string yesturday_shiftday = DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "07";
    string today_shiftday = DateTime.Now.AddDays(+0).ToString("yyyyMMdd") + "07";
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");

    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string last_hour = DateTime.Now.AddDays(-1 / 24).ToString("yyyyMMddHH");
    string last_twohour = DateTime.Now.AddDays(-2 / 24).ToString("yyyyMMddHH");
    string SaveLocation = "";
    Int32 counter_oscar = 0;


   

    protected void Page_Load(object sender, EventArgs e)
    {

        sql_temp = @"select t.defect_content from defect_file_data t";


        DataTable DT=  func.get_dataSet_access_oracle_client(sql_temp, conn).Tables[0];



        write_log(DT.Rows[0]["defect_content"].ToString(), Server.MapPath(".") + "//FILE//AAA//", "txt");
        //write_log("AAAA", Server.MapPath(".") + "//FILE//AAA//", "txt");


        //javascript 語法填入 字串 
        string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_self');window.top.close(this);</script>";
        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmClose);




    }


    public  void write_log(string program_name, string file_path, string file_type)
    {
        StreamWriter sw;
        DirectoryInfo di;//宣告目錄 
        FileInfo fi;//宣告檔案 
        string program_name1 = program_name;
        //di = new DirectoryInfo(Server.MapPath(".") + "\\RUN_LOG\\" ); //DateTime.Now.ToString("yyyyMMdd") 
        di = new DirectoryInfo(file_path); //DateTime.Now.ToString("yyyyMMdd") 
        //fi = new FileInfo(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
        fi = new FileInfo(file_path + DateTime.Now.ToString("yyyyMMdd") + "." + file_type);

        if (!di.Exists)
        {
            di.Create();//目錄不存在 產生目錄 
        }
        if (fi.Exists == true)
        {
            //檔案存在 寫檔案 
            //sw = File.AppendText(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
            sw = File.AppendText(file_path + DateTime.Now.ToString("yyyyMMdd") + "." + file_type);
        }
        else
        {
            sw = fi.CreateText(); //檔案不存在 產生檔案 
        }

        sw.WriteLine(program_name1);
       
        sw.Close();


    }
}
