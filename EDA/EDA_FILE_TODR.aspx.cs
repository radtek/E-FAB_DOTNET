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
using System.Data.OracleClient;
using System.Runtime.InteropServices;
using Ionic.Zip;
using System.Net;
using System.Net.Mail;

public partial class EDA_EDA_FILE_TODR : System.Web.UI.Page
{
    private OracleConnection orcn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"]);
    IS.util.special sp = new IS.util.special();
    //file f = new file();

    StreamWriter sw;
    DirectoryInfo di;//宣告目錄 
    DirectoryInfo di_send;//宣告目錄 
    FileInfo fi;//宣告檔案 


    //string conn = System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"];
    string conn = System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"];
    string conn_cel = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];
    string conn_pds = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_PDS_OLE_STD"];
    string conn_oeegw1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_MIDGW1"];

    //func fc = new func();

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
    DataSet ds_temp5 = new DataSet();

    DataTable dt_t1array_south_step = new DataTable();

    DataTable dt_t1cell_south_step = new DataTable();

    DataTable dt_todr_south_step = new DataTable();

    string yesturday_shiftday = DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "07";
    //string yesturday_shiftday ="2013120607";

    string today_shiftday = DateTime.Now.AddDays(+0).ToString("yyyyMMdd") + "07";
    //string today_shiftday ="2013120707";

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");

    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string today_detail1 = DateTime.Now.AddDays(+0).ToString("yyyyMMddHHmmss");
    string last_hour = DateTime.Now.AddDays(-1 / 24).ToString("yyyyMMddHH");
    string last_twohour = DateTime.Now.AddDays(-2 / 24).ToString("yyyyMMddHH");
    string SaveLocation = "";

    string mail_content = "";
    Int32 counter_oscar = 0;
    string tod_today_format = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string tod_today = DateTime.Now.AddDays(+0).ToString("yyyyMMdd");
    string tod_half_monthdate = DateTime.Now.AddDays(-15).ToString("yyyyMMdd");
    string tod_yesturday = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");


    #region EDA_SPEC_variable_TOD
    
    public class EDA_FILE_TOD
    {


        private string _SOUTH_STEP;
        public string SOUTH_STEP
        {
            set { _SOUTH_STEP = value; }
            get { return _SOUTH_STEP; }
        }
        private string _CODE;
        public string CODE
        {
            set { _CODE = value; }
            get { return _CODE; }
        }

        private string _SOUTH_SHOP;
        public string SOUTH_SHOP
        {
            set { _SOUTH_SHOP = value; }
            get { return _SOUTH_SHOP; }
        }


        private string _CHIP_ID;
        public string CHIP_ID
        {
            set { _CHIP_ID = value; }
            get { return _CHIP_ID; }
        }

        private string _DATA;
        public string DATA
        {
            set { _DATA = value; }
            get { return _DATA; }
        }


        private string _GATE;
        public string GATE
        {
            set { _GATE = value; }
            get { return _GATE; }
        }

        private string _STEP_ID;
        public string STEP_ID
        {
            set { _STEP_ID = value; }
            get { return _STEP_ID; }
        }

        private string _GLASS_START_TIME;
        public string GLASS_START_TIME
        {
            set { _GLASS_START_TIME = value; }
            get { return _GLASS_START_TIME; }
        }
       
       

      


       

       
        

       



        

    }
    #endregion

  
    protected void Page_Load(object sender, EventArgs e)
    {

        //create_array_repair_test_file_ini();
        //create_celltest_file_ini();

        create_tod_repair_test_file_ini();

        copy_file_to_MQ(HttpContext.Current.Server.MapPath(".") + "\\FILE\\SEND\\", "*.zip", -1, @"10.56.195.215", "anonymous", "");


        #region Delete tmp directory or file

        Delete_tmp_directory(HttpContext.Current.Server.MapPath(".") + "\\FILE\\T1ARRAY\\", -7);
        Delete_tmp_directory(HttpContext.Current.Server.MapPath(".") + "\\FILE\\T1CELL\\", -7);
        Delete_tmp_directory(HttpContext.Current.Server.MapPath(".") + "\\FILE\\TODR\\", -7);

        Delete_tmp_directory_file(HttpContext.Current.Server.MapPath(".") + "\\FILE\\SEND\\", "*.zip", -90);
        #endregion




        //#region email info


        ////          string email_eda_lcm_result = @" select ot2.shiftdate,ot2.sites,ot4.shop_desc,ot2.gsflag,ot2.qty from (
        ////
        ////
        ////select '20131204' as shiftdate,ot1.sites,ot1.gsflag ,count(ot1.sites) as qty from (
        ////
        ////select t.sites,t1.gsflag,t1.glassid,count(t1.glassid)as counter
        ////
        ////from td_wms_shipping_file t, mfginfo@eda2tclmes t1
        ////where t.carton_id=t1.carton_id
        ////and t.mes_ship_date=t1.mes_ship_date
        ////and t.shippingdate>='{0}'
        ////and t.shippingdate<'{1}'
        ////and t.shippingfrom in ('T1')
        ////and t.sites in (select distinct(t.shiipping_shop) from td_lcm_shipping_shop t)
        ////group by t.sites,t1.gsflag,t1.glassid
        ////
        ////) ot1
        ////group by ot1.sites,ot1.gsflag
        ////) ot2
        ////
        ////,(
        ////select * from td_lcm_shipping_shop t1
        ////)ot4
        ////
        ////where ot2.sites=ot4.shiipping_shop ";

        ////          email_eda_lcm_result = string.Format(email_eda_lcm_result, yesturday_shiftday, today_shiftday);


        ////          ds_temp5 = get_dataSet_access_oracle_client(email_eda_lcm_result, conn);


        ////          for (int j = 0; j <= ds_temp5.Tables[0].Rows.Count - 1; j++)
        ////          {
        ////              for (int k = 0; k <= ds_temp5.Tables[0].Columns.Count - 1; k++)
        ////              {
        ////                  // last column no comma
        ////                  if (k == ds_temp5.Tables[0].Columns.Count - 1)
        ////                  {
        ////                      mail_content += ds_temp5.Tables[0].Rows[j][k].ToString();

        ////                  }
        ////                  else

        ////                  {
        ////                      mail_content += ds_temp5.Tables[0].Rows[j][k].ToString() + ",";


        ////                  }


        ////              }

        ////              mail_content += "<BR>";

        ////          }

        ////          if (ds_temp5.Tables[0].Rows.Count==0)
        ////          {
        ////              mail_content += "  Today No Data!!!";
        ////          }
        //string strHTML = " ";
        //WebClient w = new WebClient();
        //w.Encoding = Encoding.GetEncoding("big5");
        //strHTML = w.DownloadString("http://10.56.131.22/E-FAB_dotnet/EDA/EDA_LCM_CHK_REPORT.aspx");
        //string title = "[CIM 電子報系統] " + DateTime.Now.ToString("yyyy/MM/dd") + " T1 Defect File 傳輸 Daily Check !";


        //ArrayList maillist = new ArrayList();
        //maillist = FileToArray(Server.MapPath(".") + "\\maillist\\eda_lcm.txt");
        ////maillist[0]= "oscar.hsieh@innolux.com";


        //SendEmail("cim.alarm@innolux.com", maillist[0].ToString(), title, strHTML, "", "");//


        //#endregion



        #region Delete tmp directory or file

        Delete_tmp_directory(HttpContext.Current.Server.MapPath(".") + "\\FILE\\T1ARRAY\\", -7);
        Delete_tmp_directory(HttpContext.Current.Server.MapPath(".") + "\\FILE\\T1CELL\\", -7);

        Delete_tmp_directory_file(HttpContext.Current.Server.MapPath(".") + "\\FILE\\SEND\\", "*.zip", -7);
        #endregion

        func.write_log("EDA_FILE_TODR", Server.MapPath("..\\") + "\\LOG\\", "log");


        string frmClose = @"<script language='javascript' type='text/JavaScript'> 
window.opener=null; 

window.open('','_self'); 

window.close();

</script>";

        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmClose);





    }

    public ArrayList FileToArray(string srtPath)
    {
        StreamReader ReadFile = new StreamReader(srtPath, // 檔案路徑
    System.Text.Encoding.Default); // 編碼方式

        ArrayList arrFile = new ArrayList(); //make our temporary storage object
        string strTmp;

        //loop through all the rows, stopping when we reach the end of file
        do
        {
            strTmp = ReadFile.ReadLine();
            if (strTmp != null)
            {
                strTmp = strTmp.Trim();
                if (strTmp.Length != 0) arrFile.Add(strTmp); //add each element to our ArrayList
            }
        } while (strTmp != null);
        ReadFile.Close();
        return arrFile;
    }

 
    protected void Delete_tmp_directory(string directory_path, double days)
    {
        // string strFolderPath = Server.MapPath(".") + "\\FILE\\T1ARRAY\\";

        string strFolderPath = directory_path;


        DirectoryInfo DIFO = new DirectoryInfo(strFolderPath);

        DirectoryInfo[] DIFO_SUB = DIFO.GetDirectories();

        for (int i = 0; i <= DIFO_SUB.Length - 1; i++)
        {
            if (DIFO_SUB[i].CreationTime < DateTime.Now.AddDays(days))
            {
                DIFO_SUB[i].Delete(true);
            }
        }




    }
    public void Delete_tmp_directory_file(string file_path, string file_type, Double dayAgo)
    {
        //DirectoryInfo dir = new DirectoryInfo(Server.MapPath(".") + "\\CF\\Save_file\\"); 
        DirectoryInfo dir = new DirectoryInfo(file_path);
        // FileInfo[] files = dir.GetFiles("*.xls"); 
        //FileInfo[] files = dir.GetFiles(file_type); 

        //Destination = @"http:\\10.56.131.22\LCM_EDAFILE\DfSender_LCM4\COMPRESSED_FILE";
        // Destination = @"Z:\";
        //Destination = @"\\10.56.195.215\Shipping\DfSender_LCM4\COMPRESSED_FILE";

        DirectoryInfo[] subdir = dir.GetDirectories();

        for (int i = 0; i <= subdir.Length - 1; i++)
        {

            FileInfo[] files = subdir[i].GetFiles(file_type);

            for (int j = 0; j <= files.Length - 1; j++)
            {
                if (files[j].CreationTime < DateTime.Now.AddDays(dayAgo))
                {
                    //files[j].CopyTo(Destination + "\\" + files[j].Name.ToString(), true);



                    files[j].Delete();

                    //Upload(files[j].FullName, ftpServerIP + @"/" + subdir[i].Name.ToString() + @"/COMPRESSED_FILE/", Account, ftppassword);

                    // File.Copy(files[j].,Destination);
                }


                // files[j].Delete();

            }

        }

        //Int32 counter = 0;
        // Display the name of all the files. 
        #region MyRegion
        //foreach (FileInfo file in files) 
        //{ 
        // counter = counter + 1; 
        // Response.Write(counter + "."); 

        // Response.Write("Name: " + file.Name + " "); 
        // Response.Write("<br/>"); 
        // Response.Write("Size: " + file.Length.ToString()); 
        // Response.Write("<br/>"); 
        //} 
        #endregion



    }
    public void copy_file_to_MQ(string file_path, string file_type, Double dayAgo, string ftpServerIP, string Account, string ftppassword)
    {
        //DirectoryInfo dir = new DirectoryInfo(Server.MapPath(".") + "\\CF\\Save_file\\"); 
        DirectoryInfo dir = new DirectoryInfo(file_path);
        // FileInfo[] files = dir.GetFiles("*.xls"); 
        //FileInfo[] files = dir.GetFiles(file_type); 

        //Destination = @"http:\\10.56.131.22\LCM_EDAFILE\DfSender_LCM4\COMPRESSED_FILE";
        // Destination = @"Z:\";
        //Destination = @"\\10.56.195.215\Shipping\DfSender_LCM4\COMPRESSED_FILE";

        DirectoryInfo[] subdir = dir.GetDirectories();

        for (int i = 0; i <= subdir.Length - 1; i++)
        {

            FileInfo[] files = subdir[i].GetFiles(file_type);

            for (int j = 0; j <= files.Length - 1; j++)
            {
                if (files[j].CreationTime > DateTime.Now.AddDays(dayAgo))
                {
                    //files[j].CopyTo(Destination + "\\" + files[j].Name.ToString(), true);





                    Upload(files[j].FullName, ftpServerIP + @"/" + subdir[i].Name.ToString() + @"/COMPRESSED_FILE/", Account, ftppassword);

                    // File.Copy(files[j].,Destination);
                }


                // files[j].Delete();

            }

        }

        //Int32 counter = 0;
        // Display the name of all the files. 
        #region MyRegion
        //foreach (FileInfo file in files) 
        //{ 
        // counter = counter + 1; 
        // Response.Write(counter + "."); 

        // Response.Write("Name: " + file.Name + " "); 
        // Response.Write("<br/>"); 
        // Response.Write("Size: " + file.Length.ToString()); 
        // Response.Write("<br/>"); 
        //} 
        #endregion



    }





    

    public void create_tod_repair_test_file_ini()
    {
        string AOI_step = "'T1A13WS'";

        //string AOI_step = "'1930','T1A13WS','1378B','S478B','P378B'";
        //string repair_step = "'T1A13WS'";

        // add verify sites
        // string sites = "'NGB','NH','TN'";

        EDA_FILE_TOD _EDA_FILE_TOD = new EDA_FILE_TOD();





        sql_temp1 = @"

select 'TODR' as SOUTH_STEP,
       'CLC1' as CODE,
       'C2M' as SOUTH_SHOP,
       'Y' || substr(tt.chip_id, 7, 12) as CHIP_ID,
        LPAD(RTRIM(tt.s), 5, '0') as DATA,
        LPAD(RTRIM(tt.g), 5, '0') as GATE,
       tt.STEP_ID,
       tt.glass_start_time
       
  from lcdsys.array_defect_t tt
 where 1 = 1
  
   and tt.step_id in ({2})
  
   and tt.item3 = '3'
   and tt.glass_start_time >=
       to_date('{0}', 'yyyyMMdd hh24mi')
   and tt.glass_start_time <
       to_date('{1}', 'yyyyMMdd hh24mi')
   order by tt.chip_id,tt.glass_start_time



";




        // sql_temp1 = string.Format(sql_temp1, test_step, repair_step, yesturday_shiftday,today_shiftday);


        sql_temp1 = string.Format(sql_temp1, tod_yesturday + " 0700", tod_today + " 0700", AOI_step);



        //OracleCommand cmd = new OracleCommand(sql_temp1, orcn); 
        //OracleDataAdapter da=new OracleDataAdapter(cmd); 
        //DataSet ds=new DataSet(); 
        //da.Fill(ds,"test");

        // check  if file exits
        if (get_dataSet_access_oracle_client(sql_temp1, conn).Tables[0].Rows.Count > 0)
        {

            create_tod_repair_test_file(get_dataSet_access_oracle_client(sql_temp1, conn).Tables[0], "txt", _EDA_FILE_TOD);

            GridView1.DataSource = get_dataSet_access_oracle_client(sql_temp1, conn);
            GridView1.DataBind();
            for (int k = 0; k <= dt_todr_south_step.Rows.Count - 1; k++)
            {

                System.Diagnostics.Process.Start(Server.MapPath("7z.exe"), " a -tzip " + Server.MapPath(".") + "//FILE//TODR//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_todr_south_step.Rows[k]["SOUTH_SHOP"].ToString() + "//" + "T1_" + dt_todr_south_step.Rows[k]["SOUTH_STEP"].ToString() + "_" + today_detail1 + ".zip" + " " + Server.MapPath(".") + "//FILE//TODR//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_todr_south_step.Rows[k]["SOUTH_SHOP"].ToString() + "//" + dt_todr_south_step.Rows[k]["SOUTH_STEP"].ToString() + "//*").WaitForExit();
                System.Diagnostics.Process.Start(Server.MapPath("7z.exe"), " a -tzip " + Server.MapPath(".") + "//FILE//SEND//" + dt_todr_south_step.Rows[k]["SOUTH_SHOP"].ToString() + "//" + "T1_" + dt_todr_south_step.Rows[k]["SOUTH_STEP"].ToString() + "_" + today_detail1 + ".zip" + " " + Server.MapPath(".") + "//FILE//TODR//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_todr_south_step.Rows[k]["SOUTH_SHOP"].ToString() + "//" + dt_todr_south_step.Rows[k]["SOUTH_STEP"].ToString() + "//*").WaitForExit();

            }

        }


        SendEmail("cim.alarm@innolux.com", "oscar.hsieh@innolux.com", "[CIM 電子報系統] " + tod_today_format + " T1 TOD File 傳輸 Daily Check OK !", "程式執行成功", "", "");


    }

   
    

    public void create_tod_repair_test_file(DataTable dt, string file_type, EDA_FILE_TOD _EDA_FILE_TOD)
    {
       
        
        DataTable dt_chip = new DataTable();


       


        DataView dv_south_step = new DataView();

        dv_south_step = dt.DefaultView;


        dt_todr_south_step = dv_south_step.ToTable(true, "SOUTH_SHOP","SOUTH_STEP");

        DataView dv = new DataView();






        dv = dt.DefaultView;

        dt_chip = dv.ToTable(true, "SOUTH_STEP", "CODE", "SOUTH_SHOP", "CHIP_ID", "DATA", "GATE");



        //DataColumn REPR_STARTTIME = dt_chip.Columns["REPR_STARTTIME"];
        //DataColumn CHIP_ID = dt_chip.Columns["CHIP_ID"];
        //DataColumn SOUTH_STEP = dt_chip.Columns["SOUTH_STEP"];

        //DataColumn SOUTH_SHOP = dt_chip.Columns["SOUTH_SHOP"];


        //dt_chip.PrimaryKey = new DataColumn[] { REPR_STARTTIME, CHIP_ID, SOUTH_STEP, SOUTH_SHOP };




        DataTable dt_index_chip_id = new DataTable();


        for (int i = 0; i <= dt_chip.Rows.Count - 1; i++)
        {
            // add send dir
            di_send = new DirectoryInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//SEND//" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString()); //DateTime.Now.ToString("yyyyMMdd")

            //di = new DirectoryInfo(Server.MapPath(".") + "\\RUN_LOG\\" ); //DateTime.Now.ToString("yyyyMMdd") 
            di = new DirectoryInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//TODR//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString() + "//" + dt_chip.Rows[i]["SOUTH_STEP"].ToString() + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString().Substring(0, 5) + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString().Substring(0, 8)); //DateTime.Now.ToString("yyyyMMdd")
            //fi = new FileInfo(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
            fi = new FileInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//TODR//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString() + "//" + dt_chip.Rows[i]["SOUTH_STEP"].ToString() + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString().Substring(0, 5) + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString().Substring(0, 8) + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString() + "." + file_type);

            if (!di_send.Exists)
            {
                di_send.Create();//目錄不存在 產生目錄 
            }



            if (!di.Exists)
            {
                di.Create();//目錄不存在 產生目錄 
            }
            if (fi.Exists == true)
            {
                //檔案存在 寫檔案 
                //sw = File.AppendText(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
                fi.Delete();
                sw = File.AppendText(HttpContext.Current.Server.MapPath(".") + "//FILE//TODR//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString() + "//" + dt_chip.Rows[i]["SOUTH_STEP"].ToString() + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString().Substring(0, 5) + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString().Substring(0, 8) + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString() + "." + file_type);

                //fi.Delete();
            }
            else
            {
                sw = fi.CreateText(); //檔案不存在 產生檔案 
            }


            dv.RowFilter = "CHIP_ID='" + dt_chip.Rows[i]["CHIP_ID"].ToString() +"'" ;

           // dv.RowFilter = "CHIP_ID='" + dt_chip.Rows[i]["CHIP_ID"].ToString() + "' and SOUTH_STEP='" + dt_chip.Rows[i]["SOUTH_STEP"].ToString() + "' and SOUTH_SHOP='" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString() + "'";
            dv.Sort = "GLASS_START_TIME";

            dt_index_chip_id = dv.ToTable();

           
            //  First Row
            sw.WriteLine("Code Data  Gate");

            // Second Row   2~N      

              for (int j = 0; j <= dt_index_chip_id.Rows.Count - 1; j++)
            {
                #region initial data

                _EDA_FILE_TOD.SOUTH_STEP = dt_index_chip_id.Rows[j]["SOUTH_STEP"].ToString();
                _EDA_FILE_TOD.CODE = dt_index_chip_id.Rows[j]["CODE"].ToString();
                _EDA_FILE_TOD.SOUTH_SHOP = dt_index_chip_id.Rows[j]["SOUTH_SHOP"].ToString();  //C2M
                _EDA_FILE_TOD.GATE = dt_index_chip_id.Rows[j]["GATE"].ToString();

                _EDA_FILE_TOD.DATA = dt_index_chip_id.Rows[j]["DATA"].ToString();

                _EDA_FILE_TOD.CHIP_ID = dt_index_chip_id.Rows[j]["CHIP_ID"].ToString();

              

               
                #endregion

             
                // Second Row   2~N
                sw.WriteLine(_EDA_FILE_TOD.CODE + " " + _EDA_FILE_TOD.DATA + " " + _EDA_FILE_TOD.GATE );


                // last Row  Add  Finished symbol  '@'
                //if (j == dt_index_chip_id.Rows.Count - 1)
                //{
                //    sw.WriteLine(_EDA_FILE_LCM.SPEC4_FLAG);

                //}



            }
            sw.Close();





        }














    }

  


    public DataSet get_dataSet_access_oracle_client(string strSql, string conn)
    {

        //"Password=pmeda;User ID=lcdapp;Data Source=peda1;Persist Security Info=True"
        //OracleCommand myCommand = new OracleCommand(strSql, myConnection);
        //OracleDataAdapter myAdapter = new OracleDataAdapter();
        //myAdapter.SelectCommand = myCommand;
        //DataSet ds = new DataSet();
        //myAdapter.Fill(ds);
        ////myCommand.Dispose();
        ////myAdapter.Dispose();
        //return ds;

        OracleConnection orcn = new OracleConnection(conn);


        OracleCommand cmd = new OracleCommand(strSql, orcn);


        OracleDataAdapter oda2 = new OracleDataAdapter(cmd);

        oda2.SelectCommand.CommandTimeout = 900;
        DataSet ds = new DataSet();
        oda2.Fill(ds, "test");
        return ds;

    }


    public void Upload(string filename, string ftpServerIP, string Account, string ftppassword)
    {

        FileInfo fileInf = new FileInfo(filename);

        string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;

        FtpWebRequest reqFTP;

        // Create FtpWebRequest object from the Uri provided

        reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileInf.Name));//此段關鍵

        // Provide the WebPermission Credintials

        reqFTP.Credentials = new NetworkCredential(Account, ftppassword);

        // By default KeepAlive is true, where the control connection is not closed

        // after a command is executed.

        reqFTP.KeepAlive = false;

        // Specify the command to be executed.

        reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

        // Specify the data transfer type.

        reqFTP.UseBinary = true;

        // Notify the server about the size of the uploaded file

        reqFTP.ContentLength = fileInf.Length;

        // The buffer size is set to 2kb

        int buffLength = 2048;

        byte[] buff = new byte[buffLength];

        int contentLen;

        // Opens a file stream (System.IO.FileStream) to read the file to be uploaded

        FileStream fs = fileInf.OpenRead();

        try
        {

            // Stream to which the file to be upload is written

            Stream strm = reqFTP.GetRequestStream();

            // Read from the file stream 2kb at a time

            contentLen = fs.Read(buff, 0, buffLength);

            // Till Stream content ends

            while (contentLen != 0)
            {

                // Write Content from the file stream to the FTP Upload Stream

                strm.Write(buff, 0, contentLen);

                contentLen = fs.Read(buff, 0, buffLength);

            }

            // Close the file stream and the Request Stream

            strm.Close();

            fs.Close();

        }


        catch (Exception ex)
        {

            //MessageBox.Show(ex.Message, "Upload Error");

        }

    }

    public void SendEmail(string from, string to, string subject, string body, string cca, string file_path)
    {
        SmtpClient smtp = new SmtpClient("10.56.196.147");
        MailMessage email = new MailMessage(from, to, subject, body);
        if (!cca.Equals(""))
        {
            email.CC.Add(cca);

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
