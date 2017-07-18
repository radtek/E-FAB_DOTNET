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
using System.Net;
using System.Runtime.InteropServices;
using System.Net.Mail;
using SharpZip = ICSharpCode.SharpZipLib.Zip;
using Ionic.Zip;  

public partial class EDA_EDA_FILE_LCM_Enhance_test : System.Web.UI.Page
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

    string yesturday_shiftday = DateTime.Now.AddDays(-2).ToString("yyyyMMdd") + "07";
    string today_shiftday = DateTime.Now.AddDays(+0).ToString("yyyyMMdd") + "07";
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


    #region EDA_SPEC_variable

    public class EDA_FILE_LCM
    {


        private string _T_FLAG;
        public string T_FLAG
        {
            set { _T_FLAG = value; }
            get { return _T_FLAG; }
        }
        private string _G_FLAG;
        public string G_FLAG
        {
            set { _G_FLAG = value; }
            get { return _G_FLAG; }
        }
        private string _CHIP_ID;
        public string CHIP_ID
        {
            set { _CHIP_ID = value; }
            get { return _CHIP_ID; }
        }

        private string _G_2_FLAG;
        public string G_2_FLAG
        {
            set { _G_2_FLAG = value; }
            get { return _G_2_FLAG; }
        }

        private string _SPEC_FLAG;
        public string SPEC_FLAG
        {
            set { _SPEC_FLAG = value; }
            get { return _SPEC_FLAG; }
        }
        private string _PRODUCT_ID;
        public string PRODUCT_ID
        {
            set { _PRODUCT_ID = value; }
            get { return _PRODUCT_ID; }
        }
        private string _TEST_EQUIP_ID;
        public string TEST_EQUIP_ID
        {
            set { _TEST_EQUIP_ID = value; }
            get { return _TEST_EQUIP_ID; }
        }


        private string _TEST_STARTTIME;
        public string TEST_STARTTIME
        {
            set { _TEST_STARTTIME = value; }
            get { return _TEST_STARTTIME; }
        }

        private string _TEST_ENDTIME;
        public string TEST_ENDTIME
        {
            set { _TEST_ENDTIME = value; }
            get { return _TEST_ENDTIME; }
        }


        private string _REPR_EQUIP_ID;
        public string REPR_EQUIP_ID
        {
            set { _REPR_EQUIP_ID = value; }
            get { return _REPR_EQUIP_ID; }
        }

        private string _OPERATOR_ID;
        public string OPERATOR_ID
        {
            set { _OPERATOR_ID = value; }
            get { return _OPERATOR_ID; }
        }

        private string _REPR_STARTTIME;
        public string REPR_STARTTIME
        {
            set { _REPR_STARTTIME = value; }
            get { return _REPR_STARTTIME; }
        }


        private string _REPR_ENDTIME;
        public string REPR_ENDTIME
        {
            set { _REPR_ENDTIME = value; }
            get { return _REPR_ENDTIME; }
        }

        private string _LOT_TYPE;
        public string LOT_TYPE
        {
            set { _LOT_TYPE = value; }
            get { return _LOT_TYPE; }
        }

        private string _SPEC1_FLAG;
        public string SPEC1_FLAG
        {
            set { _SPEC1_FLAG = value; }
            get { return _SPEC1_FLAG; }
        }

        private string _GLASS_ID;
        public string GLASS_ID
        {
            set { _GLASS_ID = value; }
            get { return _GLASS_ID; }
        }

        private string _SPEC2_FLAG;
        public string SPEC2_FLAG
        {
            set { _SPEC2_FLAG = value; }
            get { return _SPEC2_FLAG; }
        }

        private string _AP_COUNT;
        public string AP_COUNT
        {
            set { _AP_COUNT = value; }
            get { return _AP_COUNT; }
        }

        private string _S;
        public string S
        {
            set { _S = value; }
            get { return _S; }
        }


        private string _G;
        public string G
        {
            set { _G = value; }
            get { return _G; }
        }

        private string _REASON;
        public string REASON
        {
            set { _REASON = value; }
            get { return _REASON; }
        }

        private string _RT;
        public string RT
        {
            set { _RT = value; }
            get { return _RT; }
        }


        private string _RETYPE;
        public string RETYPE
        {
            set { _RETYPE = value; }
            get { return _RETYPE; }
        }


        private string _SPEC3_FLAG;
        public string SPEC3_FLAG
        {
            set { _SPEC3_FLAG = value; }
            get { return _SPEC3_FLAG; }
        }

        private string _SPEC4_FLAG;
        public string SPEC4_FLAG
        {
            set { _SPEC4_FLAG = value; }
            get { return _SPEC4_FLAG; }
        }



        private string _SOUTH_STEP;
        public string SOUTH_STEP
        {
            set { _SOUTH_STEP = value; }
            get { return _SOUTH_STEP; }
        }

        private string _SOUTH_SHOP;
        public string SOUTH_SHOP
        {
            set { _SOUTH_SHOP = value; }
            get { return _SOUTH_SHOP; }
        }



    }
    #endregion


    #region EDA_SPEC_variable_CELL
    public class EDA_FILE_LCM_CELL
    {


        private string _CELL_CHIP_ID;
        public string CELL_CHIP_ID
        {
            set { _CELL_CHIP_ID = value; }
            get { return _CELL_CHIP_ID; }
        }
        private string _STEP_ID;
        public string STEP_ID
        {
            set { _STEP_ID = value; }
            get { return _STEP_ID; }
        }
        private string _SOURCE_CARRIER_ID;
        public string SOURCE_CARRIER_ID
        {
            set { _SOURCE_CARRIER_ID = value; }
            get { return _SOURCE_CARRIER_ID; }
        }

        private string _EQUIP_ID;
        public string EQUIP_ID
        {
            set { _EQUIP_ID = value; }
            get { return _EQUIP_ID; }
        }

        private string _SPEC_FLAG;
        public string SPEC_FLAG
        {
            set { _SPEC_FLAG = value; }
            get { return _SPEC_FLAG; }
        }
        private string _OPERATION_MODE;
        public string OPERATION_MODE
        {
            set { _OPERATION_MODE = value; }
            get { return _OPERATION_MODE; }
        }
        private string _SPEC1_FLAG;
        public string SPEC1_FLAG
        {
            set { _SPEC1_FLAG = value; }
            get { return _SPEC1_FLAG; }
        }


        private string _TACT_TIME;
        public string TACT_TIME
        {
            set { _TACT_TIME = value; }
            get { return _TACT_TIME; }
        }

        private string _SPEC2_FLAG;
        public string SPEC2_FLAG
        {
            set { _SPEC2_FLAG = value; }
            get { return _SPEC2_FLAG; }
        }


        private string _STARTTIME;
        public string STARTTIME
        {
            set { _STARTTIME = value; }
            get { return _STARTTIME; }
        }

        private string _ENDTIME;
        public string ENDTIME
        {
            set { _ENDTIME = value; }
            get { return _ENDTIME; }
        }

        private string _OPERATOR_ID;
        public string OPERATOR_ID
        {
            set { _OPERATOR_ID = value; }
            get { return _OPERATOR_ID; }
        }


        private string _SPEC3_FLAG;
        public string SPEC3_FLAG
        {
            set { _SPEC3_FLAG = value; }
            get { return _SPEC3_FLAG; }
        }

        private string _DEFECT_COUNT;
        public string DEFECT_COUNT
        {
            set { _DEFECT_COUNT = value; }
            get { return _DEFECT_COUNT; }
        }

        private string _SPEC4_FLAG;
        public string SPEC4_FLAG
        {
            set { _SPEC4_FLAG = value; }
            get { return _SPEC4_FLAG; }
        }

        private string _BATCH_ID;
        public string BATCH_ID
        {
            set { _BATCH_ID = value; }
            get { return _BATCH_ID; }
        }

        private string _SPEC5_FLAG;
        public string SPEC5_FLAG
        {
            set { _SPEC5_FLAG = value; }
            get { return _SPEC5_FLAG; }
        }

        private string _SPEC6_FLAG;
        public string SPEC6_FLAG
        {
            set { _SPEC6_FLAG = value; }
            get { return _SPEC6_FLAG; }
        }

        private string _SPEC7_FLAG;
        public string SPEC7_FLAG
        {
            set { _SPEC7_FLAG = value; }
            get { return _SPEC7_FLAG; }
        }


        private string _SPEC8_FLAG;
        public string SPEC8_FLAG
        {
            set { _SPEC8_FLAG = value; }
            get { return _SPEC8_FLAG; }
        }

        private string _SPEC9_FLAG;
        public string SPEC9_FLAG
        {
            set { _SPEC9_FLAG = value; }
            get { return _SPEC9_FLAG; }
        }

        private string _SPEC10_FLAG;
        public string SPEC10_FLAG
        {
            set { _SPEC10_FLAG = value; }
            get { return _SPEC10_FLAG; }
        }


        private string _SPEC11_FLAG;
        public string SPEC11_FLAG
        {
            set { _SPEC11_FLAG = value; }
            get { return _SPEC11_FLAG; }
        }


        private string _SPEC12_FLAG;
        public string SPEC12_FLAG
        {
            set { _SPEC12_FLAG = value; }
            get { return _SPEC12_FLAG; }
        }

        private string _DEFECT_SEQ_NO;
        public string DEFECT_SEQ_NO
        {
            set { _DEFECT_SEQ_NO = value; }
            get { return _DEFECT_SEQ_NO; }
        }


        private string _DEFECT_CODE;
        public string DEFECT_CODE
        {
            set { _DEFECT_CODE = value; }
            get { return _DEFECT_CODE; }
        }



        private string _S;
        public string S
        {
            set { _S = value; }
            get { return _S; }
        }



        private string _G;
        public string G
        {
            set { _G = value; }
            get { return _G; }
        }



        private string _DEFECT_COLOR;
        public string DEFECT_COLOR
        {
            set { _DEFECT_COLOR = value; }
            get { return _DEFECT_COLOR; }
        }


        private string _SPEC13_FLAG;
        public string SPEC13_FLAG
        {
            set { _SPEC13_FLAG = value; }
            get { return _SPEC13_FLAG; }
        }


        private string _DEFECT_NAME;
        public string DEFECT_NAME
        {
            set { _DEFECT_NAME = value; }
            get { return _DEFECT_NAME; }
        }


        private string _SPEC14_FLAG;
        public string SPEC14_FLAG
        {
            set { _SPEC14_FLAG = value; }
            get { return _SPEC14_FLAG; }
        }


        private string _SPEC15_FLAG;
        public string SPEC15_FLAG
        {
            set { _SPEC15_FLAG = value; }
            get { return _SPEC15_FLAG; }
        }


        private string _SPEC16_FLAG;
        public string SPEC16_FLAG
        {
            set { _SPEC16_FLAG = value; }
            get { return _SPEC16_FLAG; }
        }



        private string _SOUTH_STEP;
        public string SOUTH_STEP
        {
            set { _SOUTH_STEP = value; }
            get { return _SOUTH_STEP; }
        }

        private string _SOUTH_SHOP;
        public string SOUTH_SHOP
        {
            set { _SOUTH_SHOP = value; }
            get { return _SOUTH_SHOP; }
        }

        private string _SPEC17_FLAG;
        public string SPEC17_FLAG
        {
            set { _SPEC17_FLAG = value; }
            get { return _SPEC17_FLAG; }
        }


        private string _SPEC18_FLAG;
        public string SPEC18_FLAG
        {
            set { _SPEC18_FLAG = value; }
            get { return _SPEC18_FLAG; }
        }

        private string _SPEC19_FLAG;
        public string SPEC19_FLAG
        {
            set { _SPEC19_FLAG = value; }
            get { return _SPEC19_FLAG; }
        }


        private string _SPEC20_FLAG;
        public string SPEC20_FLAG
        {
            set { _SPEC20_FLAG = value; }
            get { return _SPEC20_FLAG; }
        }

        private string _SPEC21_FLAG;
        public string SPEC21_FLAG
        {
            set { _SPEC21_FLAG = value; }
            get { return _SPEC21_FLAG; }
        }


        private string _SPEC22_FLAG;
        public string SPEC22_FLAG
        {
            set { _SPEC22_FLAG = value; }
            get { return _SPEC22_FLAG; }
        }

        private string _SPEC23_FLAG;
        public string SPEC23_FLAG
        {
            set { _SPEC23_FLAG = value; }
            get { return _SPEC23_FLAG; }
        }
        private string _SPEC24_FLAG;
        public string SPEC24_FLAG
        {
            set { _SPEC24_FLAG = value; }
            get { return _SPEC24_FLAG; }
        }

        private string _SPEC25_FLAG;
        public string SPEC25_FLAG
        {
            set { _SPEC25_FLAG = value; }
            get { return _SPEC25_FLAG; }
        }
        private string _SPEC26_FLAG;
        public string SPEC26_FLAG
        {
            set { _SPEC26_FLAG = value; }
            get { return _SPEC26_FLAG; }
        }

        private string _SPEC27_FLAG;
        public string SPEC27_FLAG
        {
            set { _SPEC27_FLAG = value; }
            get { return _SPEC27_FLAG; }
        }
        private string _SPEC28_FLAG;
        public string SPEC28_FLAG
        {
            set { _SPEC28_FLAG = value; }
            get { return _SPEC28_FLAG; }
        }
        private string _SPEC29_FLAG;
        public string SPEC29_FLAG
        {
            set { _SPEC29_FLAG = value; }
            get { return _SPEC29_FLAG; }
        }




    }
    #endregion

    #region 網路磁碟機

    [DllImport("mpr.dll", EntryPoint = "WNetAddConnection2")]
    public static extern uint WNetAddConnection2([In] NETRESOURCE lpNetResource, string lpPassword, string lpUsername, uint dwFlags);

    [DllImport("Mpr.dll")]
    public static extern uint WNetCancelConnection2(string lpName, uint dwFlags, bool fForce);

    [StructLayout(LayoutKind.Sequential)]
    public class NETRESOURCE
    {

        public int dwScope;
        public int dwType;
        public int dwDisplayType;
        public int dwUsage;
        public string LocalName;
        public string RemoteName;
        public string Comment;
        public string Provider;
    }

    // remoteNetworkPath format:  @"\\192.168.1.48\sharefolder"
    // localDriveName format:     @"O:"
    public static bool CreateMap(string userName, string password, string remoteNetworkPath, string localDriveName)
    {

        NETRESOURCE myNetResource = new NETRESOURCE();
        myNetResource.dwScope = 2;       //2:RESOURCE_GLOBALNET
        myNetResource.dwType = 1;        //1:RESOURCETYPE_ANY 
        myNetResource.dwDisplayType = 3; //3:RESOURCEDISPLAYTYPE_GENERIC
        myNetResource.dwUsage = 1;       //1: RESOURCEUSAGE_CONNECTABLE
        myNetResource.LocalName = localDriveName;
        myNetResource.RemoteName = remoteNetworkPath;
        myNetResource.Provider = null;

        uint nret = WNetAddConnection2(myNetResource, password, userName, 1);

        if (nret == 0)

            return true;

        else

            return false;

    }

    // localDriveName format:     @"O:"
    public static bool DeleteMap(string localDriveName)
    {

        uint nret = WNetCancelConnection2(localDriveName, 1, true);
        if (nret == 0)
            return true;
        else
            return false;

    }

    public bool FileCopy2(string filepath, string filename, string username, string password, string desfilepath, string desfilename)
    {
        try
        {
            bool bln = false;

            bln = CreateMap(username, password, @"\\" + filepath, @"P:");

            if (bln)
            {
                File.Copy("P:\\" + filename, desfilepath + "\\" + desfilename, true);

                bln = DeleteMap(@"P:");
            }

            return bln;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        //create_array_repair_test_file_ini();
        create_celltest_file_ini();
        // string add_drive_id = ""; 





        //add_drive_id = func.get_netdrive_id();

        //Innolux.Portal.Common.NetworkDrive NetDrive = new NetworkDrive();
        //oscar.Portal.Common.NetworkDrive NetDrive = new NetworkDrive();
        //          oscar.Portal.Common.NetworkDrive NetDrive = new oscar.Portal.Common.NetworkDrive();
        ////     oNetDrive.UnMapDrive();

        ////        //Map drive with specified user credentials


        ////oNetDrive.LocalDrive = @"H:";
        ////oNetDrive.Persistent = true;
        ////oNetDrive.PromptForCredentials = false;
        ////oNetDrive.SaveCredentials = true;



        ////oNetDrive.ShareName = @"\\10.56.195.215\Shipping\DfSender_LCM4\COMPRESSED_FILE";
        ////oNetDrive.MapDrive("Administrator","T@cim!@#");



        ////copy_file_to_MQ(HttpContext.Current.Server.MapPath(".") + "//FILE//SEND//", "*.zip", -5, @"G:");






        //     //oNetDrive.UnMapDrive();


        //     NetDrive.Persistent = true;
        //     NetDrive.PromptForCredentials = false;
        //     // NetDrive.ShareName = Innolux.Portal.Common.WebServer.Instance.RootPath; 
        //     NetDrive.ShareName = @"\\172.16.12.61\e-meeting\InnoView_E-Meeting";
        //     //NetDrive.ShareName = @"\\10.56.195.215\Shipping\DfSender_LCM4\COMPRESSED_FILE";
        //     NetDrive.SaveCredentials = true;
        //     NetDrive.FindNextFreeDrive = true;


        //     NetDrive.MapDrive(@"t1fab\fabuser", @"FABabc123"); 
        //     //NetDrive.MapDrive(@"10.56.195.215\Administrator", @"T@cim!@#");

        //CreateMap("ImpersonateUser", "CIMabc123", @"\\10.56.195.215\Shipping\DfSender_LCM4\COMPRESSED_FILE", @"P:");
        //CreateMap("ImpersonateUser", "CIMabc123", @"\\10.56.195.215\Shipping\DfSender_LCM4\COMPRESSED_FILE",@"P:");



        //System.Threading.Thread.Sleep(50000);

        copy_file_to_MQ(HttpContext.Current.Server.MapPath(".") + "\\FILE\\SEND\\", "*.zip", -5, @"10.56.195.215", "anonymous", "");

        //DeleteMap(@"P:");
        //IWshNetwork_Class network = new IWshNetwork_Class();

        //object username = "administrator";
        //object password = "T@cim!@#";
        //network.MapNetworkDrive("z:", @"\\10.56.195.215\Shipping\DfSender_LCM4\COMPRESSED_FILE",  System.Type.Missing, ref username, ref password);


        //network.RemoveNetworkDrive("Z:");

        #region email info


//        string email_eda_lcm_result = @" select t.sites,t1.gsflag,count(t.sites)
//
//from td_wms_shipping_file t, mfginfo@eda2tclmes t1
//where t.carton_id=t1.carton_id
//and t.mes_ship_date=t1.mes_ship_date
//and t.shippingdate>='{0}'
//and t.shippingdate<'{1}'
//and t.shippingfrom in ('T1')
//and t.sites in (select distinct(t.shiipping_shop) from td_lcm_shipping_shop t)
//
//group by t.sites,t1.gsflag";

//        email_eda_lcm_result = string.Format(email_eda_lcm_result, yesturday_shiftday, today_shiftday);


//        ds_temp5 = func.get_dataSet_access_oracle_client(email_eda_lcm_result, conn);


//        for (int j = 0; j <= ds_temp5.Tables[0].Rows.Count - 1; j++)
//        {
//            for (int k = 0; k <= ds_temp5.Tables[0].Columns.Count - 1; k++)
//            {
//                mail_content += ds_temp5.Tables[0].Rows[j][k].ToString() + ",";
//            }

//        }

//        mail_content += "Qty";

        string title = "[CIM 電子報系統] " + DateTime.Now.ToString("yyyy/MM/dd") + " EDA LCM FILE CHK Report";
        string strHTML = " ";

        ArrayList maillist = new ArrayList();
        maillist = func.FileToArray(Server.MapPath(".") + "\\maillist\\eda_lcm.txt");
        // maillist1[0] = "oscar.hsieh@chimei-innolux.com";

        strHTML = mail_content;
        SendEmail("cim.alarm@innolux.com", maillist[0].ToString(), title, strHTML, "", "");//


        #endregion


        string frmClose = @"<script language='javascript' type='text/JavaScript'> 
window.opener=null; 

window.open('','_self'); 

window.close();

</script>";

        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmClose);





    }



    protected void zip_sample(string north_shop,string south_shop,string south_step)
    {
        string zipFile = "";
        string zipFile1 = "";
        string file_path = "";
        //string zipFile = @"C:\test.zip";
  
        // SAVE FILE PATH
        zipFile = Server.MapPath(".") + "\\FILE\\" + "" + north_shop + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + south_shop + "\\T11_" + south_step + "_" + today_detail1 + ".zip";
        zipFile1 = Server.MapPath(".") + "\\FILE\\SEND"+"\\" + south_shop + "\\T11_" + south_step + "_" + today_detail1 + ".zip";
        
        
        
        
        ZipFile zip = new ZipFile(zipFile);

       
        ZipFile zip1 = new ZipFile(zipFile1);

        file_path = Server.MapPath(".") + "\\FILE\\" + "" + north_shop + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + south_shop + "\\" + south_step;


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


            // Glass id 5 digit
            zip.AddDirectory(@"" + file_path + @"/" + subdir[i].Name.ToString());

            zip1.AddDirectory(@"" + file_path + @"/" + subdir[i].Name.ToString());


                    

                    // File.Copy(files[j].,Destination);
           

        }

        //zip.AddDirectory(@"C:\ProjectSamplePack");




        zip.Save();
        zip1.Save();

        
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





    public void create_array_repair_test_file_ini()
    {

        string test_step = "'1920', 'S920', 'P920'";
        string repair_step = "'1930', 'S930', 'P930'";

        // add verify sites
        // string sites = "'NGB','NH','TN'";

        EDA_FILE_LCM _EDA_FILE_LCM = new EDA_FILE_LCM();





        sql_temp1 = @"

       select 
       'T' as T_FLAG,
       'G' as G_FLAG,
       RPAD(RTRIM('G'), 2, ' ') as G_2_FLAG,
       '****' as spec_flag,
       '*********** ********* ************** ************** ****** ********* ****** ************** **************' as spec1_flag,
       '>No Data  Gate  Code Repr Deft Ty RT T1   T2 R Analysis Mod ADC    DefectPictureName       Code Repr Deft Ty RT DefectPictureName   ' as spec2_flag,
       '** * OTHERS       *      *********************** **** ************ ** ***********************' as spec3_flag,
       '@' as spec4_flag,
       ot3.step_id,
       'Y'||SUBSTR(ot2.CHIP_ID,7,20) as CHIP_ID ,
       ot3.GLASS_ID,
       RPAD(RTRIM(ot4.item3), 4, ' ') as lot_type,
       
       ot3.LOT_ID,
       RPAD(RTRIM(ot3.PRODUCT_ID), 8, ' ') as PRODUCT_ID,
     
       RPAD(RTRIM(ot3.EQUIP_ID), 9, ' ') as repr_EQUIP_ID,
       
       to_char(ot3.GLASS_START_TIME,'yyyyMMddHH24MISS') as REPR_STARTTIME,
       to_char(ot3.UPDATE_TIME,'yyyyMMddHH24MISS') as REPR_ENDTIME,
       RPAD(RTRIM(ot3.ITEM2), 8, ' ') as operator_id,
     
       case when RPAD(RTRIM(ot3.TEST_EQID), 9, ' ') is null then '*********'
       else
       RPAD(RTRIM(ot3.TEST_EQID), 9, ' ') end  as TEST_EQUIP_ID,
       
       case when to_char(ot3.TEST_STARTTIME,'yyyyMMddHH24MISS') is null then '**************'
       else
       to_char(ot3.TEST_STARTTIME,'yyyyMMddHH24MISS') end as TEST_STARTTIME,
       case when to_char(ot3.TEST_ENDTIME,'yyyyMMddHH24MISS') is null then '**************'
       else
       to_char(ot3.TEST_ENDTIME,'yyyyMMddHH24MISS')end as TEST_ENDTIME,
   
       LPAD(LTRIM(s), 5, '0') as s,
      
       
       LPAD(LTRIM(g), 5, '0') as g,
       substr(ot2.Item2,2,10) as RETYPE,
       
       ot2.item4 as defect_judge,
       ot2.item5 as defect_result,
       case when substr(ot2.item51,0,12) is null then '************'
       else
       RPAD(RTRIM(substr(ot2.item51,0,12)), 12, ' ')end as reason,
       case when ot2.item4='AR' and ot2.item5='2DP' then 'D '
            when    ot2.item4='AR' and ot2.item5='DP' then 'N '
            when     ot2.item4='AR' and ot2.item5='NP' then 'G '
            when     ot2.item4='NR' and ot2.item5='NP' then 'G '
            when     ot2.item4='F'  then 'B '
            when     ot2.item4='MD' then 'X '
            when    ot2.item4='CP' then 'L '
            when     ot2.item4='CR' then 'L '
            when     ot2.item4='NR' and ot2.item5='BP' then 'B '
            when     ot2.item4='NR' and ot2.item5='DP' then 'N '
                 else '**' end as RT,
       'TFTR2' as SOUTH_STEP,
       ot5.sites  as SOUTH_SHOP
                 
  from (select t.*,
               ot1.equip_id as test_eqid,
               ot1.glass_start_time as test_starttime,
               ot1.update_time as test_endtime
          from lcdsys.array_glass_t t,
               (
                
                select t.*
                  from lcdsys.array_glass_t t
                 where t.step_id in ({0})
                
                ) ot1
         where t.step_id in ({1})
          
           and t.glass_id = ot1.glass_id(+)
        
        ) ot3,
       lcdsys.array_defect_t ot2, lcdsys.array_lot_hst_t ot4,
  (

   select t.sites,
case when t1.gsflag='CHIP' then substr(t1.glassid,0,14)
     when t1.gsflag='CUT' then substr(t1.glassid,0,14)
       else t1.glassid end transfer_subid

from td_wms_shipping_file t, mfginfo@eda2tclmes t1
where t.carton_id=t1.carton_id
and t.mes_ship_date=t1.mes_ship_date
and t.shippingdate>='{2}'
and t.shippingdate<'{3}'
and t.shippingfrom in ('T1')
and t.sites in (select distinct(t.shiipping_shop) from td_lcm_shipping_shop t)
) ot5
       
 where ot3.step_id = ot2.step_id
   and ot3.glass_start_time = ot2.glass_start_time
   and ot3.glass_id = ot2.glass_id
   and ot2.ITEM2 <> '-'
   and ot3.lot_id=ot4.lot_id
   and ot3.step_id=ot4.step_id 
   and ot3.glass_id=ot5.transfer_subid
  




";




        // sql_temp1 = string.Format(sql_temp1, test_step, repair_step, yesturday_shiftday,today_shiftday);


        sql_temp1 = sql_temp1.Replace("{0}", test_step).Replace("{1}", repair_step).Replace("{2}", yesturday_shiftday).Replace("{3}", today_shiftday);



        //OracleCommand cmd = new OracleCommand(sql_temp1, orcn); 
        //OracleDataAdapter da=new OracleDataAdapter(cmd); 
        //DataSet ds=new DataSet(); 
        //da.Fill(ds,"test");

        // check  if file exits
        if (get_dataSet_access_oracle_client(sql_temp1, conn).Tables[0].Rows.Count > 0)
        {

            create_array_repair_test_file(get_dataSet_access_oracle_client(sql_temp1, conn).Tables[0], "txt", _EDA_FILE_LCM);

            GridView1.DataSource = get_dataSet_access_oracle_client(sql_temp1, conn);
            GridView1.DataBind();
            for (int k = 0; k <= dt_t1array_south_step.Rows.Count - 1; k++)
            {

                System.Diagnostics.Process.Start(Server.MapPath("winrar.exe"), " a -ep1  " + Server.MapPath(".") + "//FILE//T1ARRAY//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_t1array_south_step.Rows[k]["SOUTH_SHOP"].ToString() + "//" + "T1_" + dt_t1array_south_step.Rows[k]["SOUTH_STEP"].ToString() + "_" + today_detail1 + ".zip" + " " + Server.MapPath(".") + "//FILE//T1ARRAY//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_t1array_south_step.Rows[k]["SOUTH_SHOP"].ToString() + "//" + dt_t1array_south_step.Rows[k]["SOUTH_STEP"].ToString()).WaitForExit();
                System.Diagnostics.Process.Start(Server.MapPath("winrar.exe"), " a -ep1  " + Server.MapPath(".") + "//FILE//SEND//" + dt_t1array_south_step.Rows[k]["SOUTH_SHOP"].ToString() + "//" + "T1_" + dt_t1array_south_step.Rows[k]["SOUTH_STEP"].ToString() + "_" + today_detail1 + ".zip" + " " + Server.MapPath(".") + "//FILE//T1ARRAY//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_t1array_south_step.Rows[k]["SOUTH_SHOP"].ToString() + "//" + dt_t1array_south_step.Rows[k]["SOUTH_STEP"].ToString()).WaitForExit();

            }

        }



    }

    public void create_celltest_file_ini()
    {

        string test_step = "'6615','6630','6810','6815','6830'";

        // add verify sites
        //string sites = "'NGB','NH','TN'";
        // site config in table td_lcm_shipping_shop

        EDA_FILE_LCM_CELL _EDA_FILE_LCM_CELL = new EDA_FILE_LCM_CELL();



        sql_temp1 = @"

select 
        'Y'||substr(ot1.CELL_CHIP_ID,7,20) as CELL_CHIP_ID ,
       ot1.step_id,
       ot1.equip_id,
      
      
       RPAD(RTRIM(ot1.SOURCE_CARRIER_ID), 8, ' ') as SOURCE_CARRIER_ID,
       '******' as spec_flag,
       case when ot1.step_id ='6615' then 'OY'
            when ot1.step_id ='6620' then 'OL'
            when ot1.step_id ='6630' then 'OY'
            when ot1.step_id ='6810' then 'OY'
            when ot1.step_id ='6815' then 'OY'
            when ot1.step_id ='6820' then 'OL'
            when ot1.step_id ='6830' then 'OY'
            else '**' end as OPERATION_MODE,
            
       '********' as SPEC1_FLAG,
       LPAD(LTRIM(round((case when (ot1.ENDTIME-ot1.STARTTIME)*24*60*60 >999 then 999
              else (ot1.ENDTIME-ot1.STARTTIME)*24*60*60 end),0)), 3, '0')  as TACT_TIME,
       '***' as SPEC2_FLAG,
       to_char(ot1.STARTTIME,'yyyyMMddHH24MISS') as STARTTIME,
        to_char(ot1.ENDTIME,'yyyyMMddHH24MISS') as ENDTIME,
        LPAD(LTRIM(ot1.OPERATOR_ID), 8, '0') as OPERATOR_ID,
       '**************** ** ****' as SPEC3_FLAG,
     
       '* **** **** ****' as SPEC4_FLAG,
       
       
       RPAD(RTRIM(ot1.Batch_Id), 20, ' ') as Batch_Id,
       '& **' as SPEC5_FLAG,
       '> * ******' as SPEC6_FLAG,
       '< **********' as SPEC7_FLAG,
       '{' as SPEC8_FLAG,
       '}' as SPEC9_FLAG,
       '[' as SPEC10_FLAG,
       ']' as SPEC11_FLAG,
       '%No Code Data  Gate  C RK Leve   ND  R PAT Defect Pattern Name  English Defect Name  CEDC Cell Re Eng Def Name JC BD Spare      Spare' as SPEC12_FLAG,
       substr(ot1.DEFECT_CODE,2,4) as DEFECT_CODE ,
        case when LPAD(LTRIM(ot1.S), 5, '0')='000-1' then '00000'
           else  LPAD(LTRIM(ot1.S), 5, '0') end as S,
       
       case when LPAD(LTRIM(ot1.G), 5, '0')='000-1' then '00000'
          else  LPAD(LTRIM(ot1.G), 5, '0') end as G,
        case when ot1.DEFECT_COLOR is null then '*'
             else ot1.DEFECT_COLOR end as DEFECT_COLOR ,
       '** ****** *** * *** ********************' as SPEC13_FLAG,
        RPAD(RTRIM(substr(ot1.DEFECT_NAME,0,20)), 20, ' ') as DEFECT_NAME,
       '**** ******************** **    ********** **********' as SPEC14_FLAG,
       '$N Cod STime Pattern Switch Name  Spare      Spare     ' as SPEC15_FLAG,
       '@' as SPEC16_FLAG,
      
       ot1.DEFECT_JUDGE_CODE,
       
      
       ot1.CT_DEFECT_CODE,
       case when ot1.step_id='6615' then '3600' 
            when ot1.step_id='6630' then '3650'  
            when ot1.step_id='6810' then '4600' 
            when ot1.step_id='6815' then '4600' 
            when ot1.step_id='6830' then '4650' 
          else 'NA' end  AS SOUTH_STEP,
       ot1.sites as SOUTH_SHOP,
       '**** **** ************ **** ***'as SPEC17_FLAG,
       '** **** ******************** **' as SPEC18_FLAG,
       '************ * ************************* * *************************' as SPEC19_FLAG,
       '**************** ** ************************* *************************' as SPEC20_FLAG,
       '************ *' as SPEC21_FLAG,
       '** * ***** ***** ****** ******' as SPEC22_FLAG,
       '* ** ************' as SPEC23_FLAG,
       '************ ************ ************ * * * * * ** ****' as SPEC24_FLAG,
       '************************* ************************* ************************* ************************* *************************' as SPEC25_FLAG,
       '************************* ************************* ************************* ************************* *************************' as SPEC26_FLAG,
       '** ********** **********' as SPEC27_FLAG,
       '** *** ***** ******************** ********** **********' as SPEC28_FLAG,
       '*' as SPEC29_FLAG
       
            
  from (
        
        select t1.step_id,
                
                t1.EQUIP_ID,
                t1.Item2 as OPERATOR_ID,
                t1.Item3 as SOURCE_CARRIER_ID,
                case
                  when t1.Date_Item1 is null then
                   t1.COMPONENT_START_TIME
                  else
                   t1.Date_Item1
                end STARTTIME,
                
                t1.COMPONENT_START_TIME as ENDTIME,
                t1.Batch_Id,
                t2.chip_id as CELL_CHIP_ID,
                t2.s as S,
                t2.g as G,
                t2.Item1 as DEFECT_CODE,
                t2.Item3 as DEFECT_JUDGE_CODE,
                t2.Item4 as DEFECT_NAME,
                t2.Item5 as DEFECT_COLOR,
                t2.Item51 as CT_DEFECT_CODE,
                t3.sites as sites
          from lcdsys.cell_component_t t1, lcdsys.cell_defect_t t2 ,
          (

     select t.sites,
       t1.glassid
from td_wms_shipping_file t, mfginfo@eda2tclmes t1
where t.carton_id=t1.carton_id
and t.mes_ship_date=t1.mes_ship_date
and t.shippingdate>='{0}'
and t.shippingdate<'{1}'
and t.shippingfrom in ('T1')
and t.sites in (select distinct(t.shiipping_shop) from td_lcm_shipping_shop t)
and t1.gsflag='CHIP'



) t3
         where t1.step_id in (   {2} )
              
            
           and t1.component_id = t2.component_id
           and t1.step_id = t2.step_id
           and t1.component_start_time = t2.component_start_time
           and t1.component_id=t3.glassid
          
        ) ot1

";


        //sql_temp1 = string.Format(sql_temp1, yesturday_shiftday, today_shiftday);


        sql_temp1 = sql_temp1.Replace("{0}", yesturday_shiftday).Replace("{1}", today_shiftday).Replace("{2}", test_step);




        //OracleCommand cmd = new OracleCommand(sql_temp1, orcn); 
        //OracleDataAdapter da=new OracleDataAdapter(cmd); 
        //DataSet ds=new DataSet(); 
        //da.Fill(ds,"test");

        // Check if  file exits
        if (get_dataSet_access_oracle_client(sql_temp1, conn).Tables[0].Rows.Count > 0)
        {
            create_cell_test_file(get_dataSet_access_oracle_client(sql_temp1, conn).Tables[0], "txt", _EDA_FILE_LCM_CELL);

            GridView2.DataSource = get_dataSet_access_oracle_client(sql_temp1, conn);
            GridView2.DataBind();

            for (int k = 0; k <= dt_t1cell_south_step.Rows.Count - 1; k++)
            {

                //System.Diagnostics.Process.Start(Server.MapPath("winrar.exe"), " a -ep1  " + Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_t1cell_south_step.Rows[k]["SOUTH_SHOP"].ToString() + "//" + "T1_" + dt_t1cell_south_step.Rows[k]["SOUTH_STEP"].ToString() + "_" + today_detail1 + ".zip" + " " + Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_t1cell_south_step.Rows[k]["SOUTH_SHOP"].ToString() + "//" + dt_t1cell_south_step.Rows[k]["SOUTH_STEP"].ToString()).WaitForExit();
                //System.Diagnostics.Process.Start(Server.MapPath("winrar.exe"), " a -ep1  " + Server.MapPath(".") + "//FILE//SEND//" + dt_t1cell_south_step.Rows[k]["SOUTH_SHOP"].ToString() + "//" + "T1_" + dt_t1cell_south_step.Rows[k]["SOUTH_STEP"].ToString() + "_" + today_detail1 + ".zip" + " " + Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_t1cell_south_step.Rows[k]["SOUTH_SHOP"].ToString() + "//" + dt_t1cell_south_step.Rows[k]["SOUTH_STEP"].ToString() + "//").WaitForExit();

                zip_sample("T1CELL", dt_t1cell_south_step.Rows[k]["SOUTH_SHOP"].ToString(), dt_t1cell_south_step.Rows[k]["SOUTH_STEP"].ToString());
            
            }


        }


        //System.Diagnostics.Process.Start(Server.MapPath("winrar.exe"), " a -ep1  " + Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd") + ".zip" + " " + Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd"));
    }

    public void create_array_repair_test_file(DataTable dt, string file_type, EDA_FILE_LCM _EDA_FILE_LCM)
    {

        DataTable dt_chip = new DataTable();


        DataView dv_south_step = new DataView();

        dv_south_step = dt.DefaultView;


        dt_t1array_south_step = dv_south_step.ToTable(true, "SOUTH_STEP", "SOUTH_SHOP");



        DataView dv = new DataView();

        dv = dt.DefaultView;

        dt_chip = dv.ToTable(true, "REPR_STARTTIME", "CHIP_ID", "SOUTH_STEP", "SOUTH_SHOP");
        DataTable dt_index_chip_id = new DataTable();


        for (int i = 0; i <= dt_chip.Rows.Count - 1; i++)
        {
            // add send dir
            di_send = new DirectoryInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//SEND//" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString()); //DateTime.Now.ToString("yyyyMMdd")

            //di = new DirectoryInfo(Server.MapPath(".") + "\\RUN_LOG\\" ); //DateTime.Now.ToString("yyyyMMdd") 
            di = new DirectoryInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//T1ARRAY//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString() + "//" + dt_chip.Rows[i]["SOUTH_STEP"].ToString() + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString().Substring(0, 5) + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString().Substring(0, 8)); //DateTime.Now.ToString("yyyyMMdd")
            //fi = new FileInfo(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
            fi = new FileInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//T1ARRAY//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString() + "//" + dt_chip.Rows[i]["SOUTH_STEP"].ToString() + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString().Substring(0, 5) + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString().Substring(0, 8) + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString() + "." + file_type);

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
                sw = File.AppendText(HttpContext.Current.Server.MapPath(".") + "//FILE//T1ARRAY//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString() + "//" + dt_chip.Rows[i]["SOUTH_STEP"].ToString() + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString().Substring(0, 5) + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString().Substring(0, 8) + "//" + dt_chip.Rows[i]["CHIP_ID"].ToString() + "." + file_type);
            }
            else
            {
                sw = fi.CreateText(); //檔案不存在 產生檔案 
            }




            dv.RowFilter = "CHIP_ID='" + dt_chip.Rows[i]["CHIP_ID"].ToString() + "' and SOUTH_STEP='" + dt_chip.Rows[i]["SOUTH_STEP"].ToString() + "' and SOUTH_SHOP='" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString() + "'";
            dv.Sort = "REPR_STARTTIME";

            dt_index_chip_id = dv.ToTable();

            string aaa = "";

            for (int j = 0; j <= dt_index_chip_id.Rows.Count - 1; j++)
            {
                #region initial data

                _EDA_FILE_LCM.T_FLAG = "T";

                _EDA_FILE_LCM.G_FLAG = "G";

                _EDA_FILE_LCM.CHIP_ID = dt_index_chip_id.Rows[j]["CHIP_ID"].ToString();

                _EDA_FILE_LCM.G_2_FLAG = dt_index_chip_id.Rows[j]["G_2_FLAG"].ToString();

                _EDA_FILE_LCM.SPEC_FLAG = dt_index_chip_id.Rows[j]["SPEC_FLAG"].ToString();

                _EDA_FILE_LCM.PRODUCT_ID = dt_index_chip_id.Rows[j]["PRODUCT_ID"].ToString();

                _EDA_FILE_LCM.TEST_EQUIP_ID = dt_index_chip_id.Rows[j]["TEST_EQUIP_ID"].ToString();

                _EDA_FILE_LCM.TEST_STARTTIME = dt_index_chip_id.Rows[j]["TEST_STARTTIME"].ToString();

                _EDA_FILE_LCM.TEST_ENDTIME = dt_index_chip_id.Rows[j]["TEST_ENDTIME"].ToString();

                _EDA_FILE_LCM.REPR_EQUIP_ID = dt_index_chip_id.Rows[j]["REPR_EQUIP_ID"].ToString();

                _EDA_FILE_LCM.OPERATOR_ID = dt_index_chip_id.Rows[j]["OPERATOR_ID"].ToString();

                _EDA_FILE_LCM.REPR_STARTTIME = dt_index_chip_id.Rows[j]["REPR_STARTTIME"].ToString();

                _EDA_FILE_LCM.REPR_ENDTIME = dt_index_chip_id.Rows[j]["REPR_ENDTIME"].ToString();


                _EDA_FILE_LCM.LOT_TYPE = dt_index_chip_id.Rows[j]["LOT_TYPE"].ToString();

                _EDA_FILE_LCM.SPEC1_FLAG = dt_index_chip_id.Rows[j]["SPEC1_FLAG"].ToString();

                _EDA_FILE_LCM.GLASS_ID = dt_index_chip_id.Rows[j]["GLASS_ID"].ToString();

                _EDA_FILE_LCM.SPEC2_FLAG = dt_index_chip_id.Rows[j]["SPEC2_FLAG"].ToString();

                //aaa = Convert.ToString(j + 1);



                aaa = String.Format("{0:000}", j + 1); // 輸出 0001

                _EDA_FILE_LCM.AP_COUNT = aaa;

                _EDA_FILE_LCM.S = dt_index_chip_id.Rows[j]["S"].ToString();

                _EDA_FILE_LCM.G = dt_index_chip_id.Rows[j]["G"].ToString();

                _EDA_FILE_LCM.RETYPE = dt_index_chip_id.Rows[j]["RETYPE"].ToString();

                _EDA_FILE_LCM.REASON = dt_index_chip_id.Rows[j]["REASON"].ToString();

                _EDA_FILE_LCM.RT = dt_index_chip_id.Rows[j]["RT"].ToString();


                _EDA_FILE_LCM.SPEC3_FLAG = dt_index_chip_id.Rows[j]["SPEC3_FLAG"].ToString();

                _EDA_FILE_LCM.SPEC4_FLAG = dt_index_chip_id.Rows[j]["SPEC4_FLAG"].ToString();

                _EDA_FILE_LCM.SOUTH_STEP = dt_index_chip_id.Rows[j]["SOUTH_STEP"].ToString();

                _EDA_FILE_LCM.SOUTH_SHOP = dt_index_chip_id.Rows[j]["SOUTH_SHOP"].ToString();
                #endregion

                if (j == 0)
                {
                    //  First Row
                    sw.WriteLine(_EDA_FILE_LCM.T_FLAG + " " + _EDA_FILE_LCM.G_FLAG + " " + _EDA_FILE_LCM.CHIP_ID + " " + _EDA_FILE_LCM.G_2_FLAG + " " + _EDA_FILE_LCM.SPEC_FLAG + " " + _EDA_FILE_LCM.PRODUCT_ID + " " + _EDA_FILE_LCM.TEST_EQUIP_ID + " " + _EDA_FILE_LCM.TEST_STARTTIME + " " + _EDA_FILE_LCM.TEST_ENDTIME + " " + _EDA_FILE_LCM.REPR_EQUIP_ID + " " + _EDA_FILE_LCM.OPERATOR_ID + " " + _EDA_FILE_LCM.REPR_STARTTIME + " " + _EDA_FILE_LCM.REPR_ENDTIME + " " + _EDA_FILE_LCM.LOT_TYPE + " " + _EDA_FILE_LCM.SPEC1_FLAG + " " + _EDA_FILE_LCM.GLASS_ID);
                    //  Second Row
                    sw.WriteLine(_EDA_FILE_LCM.SPEC2_FLAG);



                }

                // Third Row   3~N
                sw.WriteLine(_EDA_FILE_LCM.AP_COUNT + " " + _EDA_FILE_LCM.S + " " + _EDA_FILE_LCM.G + " " + _EDA_FILE_LCM.RETYPE + " " + _EDA_FILE_LCM.REASON + " " + _EDA_FILE_LCM.RT + " " + _EDA_FILE_LCM.RETYPE + " " + _EDA_FILE_LCM.SPEC3_FLAG);


                // last Row  Add  Finished symbol  '@'
                if (j == dt_index_chip_id.Rows.Count - 1)
                {
                    sw.WriteLine(_EDA_FILE_LCM.SPEC4_FLAG);

                }



            }
            sw.Close();





        }














    }



    public void create_cell_test_file(DataTable dt, string file_type, EDA_FILE_LCM_CELL _EDA_FILE_LCM_CELL)
    {

        DataTable dt_chip = new DataTable();



        DataView dv_south_step = new DataView();

        dv_south_step = dt.DefaultView;


        dt_t1cell_south_step = dv_south_step.ToTable(true, "SOUTH_STEP", "SOUTH_SHOP");

        DataView dv = new DataView();



        dv = dt.DefaultView;


        dt_chip = dv.ToTable(true, "CELL_CHIP_ID", "STEP_ID", "STARTTIME", "OPERATION_MODE", "SOUTH_STEP", "SOUTH_SHOP");
        DataTable dt_index_chip_id = new DataTable();


        for (int i = 0; i <= dt_chip.Rows.Count - 1; i++)
        {

            di_send = new DirectoryInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//SEND//" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString()); //DateTime.Now.ToString("yyyyMMdd")

            //di = new DirectoryInfo(Server.MapPath(".") + "\\RUN_LOG\\" ); //DateTime.Now.ToString("yyyyMMdd") 
            di = new DirectoryInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString() + "//" + dt_chip.Rows[i]["SOUTH_STEP"].ToString() + "//" + dt_chip.Rows[i]["CELL_CHIP_ID"].ToString().Substring(0, 5) + "//" + dt_chip.Rows[i]["CELL_CHIP_ID"].ToString().Substring(0, 8)); //DateTime.Now.ToString("yyyyMMdd")
            //fi = new FileInfo(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
            fi = new FileInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString() + "//" + dt_chip.Rows[i]["SOUTH_STEP"].ToString() + "//" + dt_chip.Rows[i]["CELL_CHIP_ID"].ToString().Substring(0, 5) + "//" + dt_chip.Rows[i]["CELL_CHIP_ID"].ToString().Substring(0, 8) + "//" + dt_chip.Rows[i]["CELL_CHIP_ID"] + "." + file_type);

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
                fi.Delete();
                sw = fi.CreateText();


                //檔案存在 寫檔案 
                //sw = File.AppendText(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
                //sw = File.AppendText(HttpContext.Current.Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_chip.Rows[i]["SOUTH_STEP"].ToString() + "//" + dt_chip.Rows[i]["CELL_CHIP_ID"].ToString().Substring(0, 5) + "//" + dt_chip.Rows[i]["CELL_CHIP_ID"].ToString().Substring(0, 8) + "//" + dt_chip.Rows[i]["CELL_CHIP_ID"] + "_" + dt_chip.Rows[i]["SOUTH_STEP"] + "_" + dt_chip.Rows[i]["OPERATION_MODE"] + "." + file_type);
            }
            else
            {
                sw = fi.CreateText(); //檔案不存在 產生檔案 
            }



            dv.RowFilter = "CELL_CHIP_ID='" + dt_chip.Rows[i]["CELL_CHIP_ID"].ToString() + "'" + " and STEP_ID='" + dt_chip.Rows[i]["STEP_ID"].ToString() + "' and SOUTH_STEP='" + dt_chip.Rows[i]["SOUTH_STEP"].ToString() + "' and SOUTH_SHOP='" + dt_chip.Rows[i]["SOUTH_SHOP"].ToString() + "'";
            dv.Sort = "STARTTIME";

            dt_index_chip_id = dv.ToTable();

            string aaa = "";
            string DEFECT_COUNT = "";

            for (int j = 0; j <= dt_index_chip_id.Rows.Count - 1; j++)
            {
                #region initial data

                _EDA_FILE_LCM_CELL.CELL_CHIP_ID = dt_index_chip_id.Rows[j]["CELL_CHIP_ID"].ToString();

                _EDA_FILE_LCM_CELL.STEP_ID = dt_index_chip_id.Rows[j]["STEP_ID"].ToString();

                _EDA_FILE_LCM_CELL.SOURCE_CARRIER_ID = dt_index_chip_id.Rows[j]["SOURCE_CARRIER_ID"].ToString();

                _EDA_FILE_LCM_CELL.EQUIP_ID = dt_index_chip_id.Rows[j]["EQUIP_ID"].ToString();

                _EDA_FILE_LCM_CELL.SPEC_FLAG = dt_index_chip_id.Rows[j]["SPEC_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.OPERATION_MODE = dt_index_chip_id.Rows[j]["OPERATION_MODE"].ToString();

                _EDA_FILE_LCM_CELL.SPEC1_FLAG = dt_index_chip_id.Rows[j]["SPEC1_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.TACT_TIME = dt_index_chip_id.Rows[j]["TACT_TIME"].ToString();

                _EDA_FILE_LCM_CELL.SPEC2_FLAG = dt_index_chip_id.Rows[j]["SPEC2_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.STARTTIME = dt_index_chip_id.Rows[j]["STARTTIME"].ToString();

                _EDA_FILE_LCM_CELL.ENDTIME = dt_index_chip_id.Rows[j]["ENDTIME"].ToString();

                _EDA_FILE_LCM_CELL.OPERATOR_ID = dt_index_chip_id.Rows[j]["OPERATOR_ID"].ToString();

                _EDA_FILE_LCM_CELL.SPEC3_FLAG = dt_index_chip_id.Rows[j]["SPEC3_FLAG"].ToString();

                DEFECT_COUNT = String.Format("{0:000}", dt_index_chip_id.Rows.Count); // 輸出 0001

                _EDA_FILE_LCM_CELL.DEFECT_COUNT = DEFECT_COUNT;

                _EDA_FILE_LCM_CELL.SPEC4_FLAG = dt_index_chip_id.Rows[j]["SPEC4_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.BATCH_ID = dt_index_chip_id.Rows[j]["BATCH_ID"].ToString();

                _EDA_FILE_LCM_CELL.SPEC5_FLAG = dt_index_chip_id.Rows[j]["SPEC5_FLAG"].ToString();


                _EDA_FILE_LCM_CELL.SPEC6_FLAG = dt_index_chip_id.Rows[j]["SPEC6_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC7_FLAG = dt_index_chip_id.Rows[j]["SPEC7_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC8_FLAG = dt_index_chip_id.Rows[j]["SPEC8_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC9_FLAG = dt_index_chip_id.Rows[j]["SPEC9_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC10_FLAG = dt_index_chip_id.Rows[j]["SPEC10_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC11_FLAG = dt_index_chip_id.Rows[j]["SPEC11_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC12_FLAG = dt_index_chip_id.Rows[j]["SPEC12_FLAG"].ToString();

                //aaa = Convert.ToString(j + 1);



                aaa = String.Format("{0:000}", j + 1); // 輸出 0001

                _EDA_FILE_LCM_CELL.DEFECT_SEQ_NO = aaa;

                _EDA_FILE_LCM_CELL.DEFECT_CODE = dt_index_chip_id.Rows[j]["DEFECT_CODE"].ToString();

                _EDA_FILE_LCM_CELL.S = dt_index_chip_id.Rows[j]["S"].ToString();

                _EDA_FILE_LCM_CELL.G = dt_index_chip_id.Rows[j]["G"].ToString();

                _EDA_FILE_LCM_CELL.DEFECT_COLOR = dt_index_chip_id.Rows[j]["DEFECT_COLOR"].ToString();

                _EDA_FILE_LCM_CELL.SPEC13_FLAG = dt_index_chip_id.Rows[j]["SPEC13_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.DEFECT_NAME = dt_index_chip_id.Rows[j]["DEFECT_NAME"].ToString();

                _EDA_FILE_LCM_CELL.SPEC14_FLAG = dt_index_chip_id.Rows[j]["SPEC14_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC15_FLAG = dt_index_chip_id.Rows[j]["SPEC15_FLAG"].ToString();


                _EDA_FILE_LCM_CELL.SPEC16_FLAG = dt_index_chip_id.Rows[j]["SPEC16_FLAG"].ToString();


                _EDA_FILE_LCM_CELL.SOUTH_STEP = dt_index_chip_id.Rows[j]["SOUTH_STEP"].ToString();
                _EDA_FILE_LCM_CELL.SOUTH_SHOP = dt_index_chip_id.Rows[j]["SOUTH_SHOP"].ToString();

                _EDA_FILE_LCM_CELL.SPEC17_FLAG = dt_index_chip_id.Rows[j]["SPEC17_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC18_FLAG = dt_index_chip_id.Rows[j]["SPEC18_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC19_FLAG = dt_index_chip_id.Rows[j]["SPEC19_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC20_FLAG = dt_index_chip_id.Rows[j]["SPEC20_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC21_FLAG = dt_index_chip_id.Rows[j]["SPEC21_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC22_FLAG = dt_index_chip_id.Rows[j]["SPEC22_FLAG"].ToString();
                _EDA_FILE_LCM_CELL.SPEC23_FLAG = dt_index_chip_id.Rows[j]["SPEC23_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC24_FLAG = dt_index_chip_id.Rows[j]["SPEC24_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC25_FLAG = dt_index_chip_id.Rows[j]["SPEC25_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC26_FLAG = dt_index_chip_id.Rows[j]["SPEC26_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC27_FLAG = dt_index_chip_id.Rows[j]["SPEC27_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC28_FLAG = dt_index_chip_id.Rows[j]["SPEC28_FLAG"].ToString();

                _EDA_FILE_LCM_CELL.SPEC29_FLAG = dt_index_chip_id.Rows[j]["SPEC29_FLAG"].ToString();

                #endregion

                if (j == 0)
                {
                    //  1 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.CELL_CHIP_ID + " " + _EDA_FILE_LCM_CELL.SOUTH_STEP + " " + _EDA_FILE_LCM_CELL.SOURCE_CARRIER_ID + " " + _EDA_FILE_LCM_CELL.SPEC17_FLAG);
                    //  2 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.EQUIP_ID + " " + _EDA_FILE_LCM_CELL.SPEC_FLAG + " " + _EDA_FILE_LCM_CELL.OPERATION_MODE + " " + _EDA_FILE_LCM_CELL.SPEC1_FLAG + " " + _EDA_FILE_LCM_CELL.TACT_TIME + " " + _EDA_FILE_LCM_CELL.SPEC2_FLAG + " " + _EDA_FILE_LCM_CELL.STARTTIME + " " + _EDA_FILE_LCM_CELL.ENDTIME + " " + _EDA_FILE_LCM_CELL.OPERATOR_ID);

                    //  3 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC18_FLAG);

                    //  4 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC19_FLAG);
                    //  5 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC3_FLAG + " " + _EDA_FILE_LCM_CELL.DEFECT_COUNT);

                    //  6 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC20_FLAG);
                    //  7 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC4_FLAG + " " + _EDA_FILE_LCM_CELL.BATCH_ID + " " + _EDA_FILE_LCM_CELL.SPEC21_FLAG);


                    //  8 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC22_FLAG);
                    //  9 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC23_FLAG);

                    //  10 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC24_FLAG);
                    //  11Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC25_FLAG);

                    //  12 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC26_FLAG);

                    //  13 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC5_FLAG);

                    //  14 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC6_FLAG);

                    //  15 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC7_FLAG);


                    //  16 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC8_FLAG);


                    //  17 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC9_FLAG);


                    //  18 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC10_FLAG);


                    //  19 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC11_FLAG);



                    //  20 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC12_FLAG);








                }

                // Third Row   21~N
                sw.WriteLine(_EDA_FILE_LCM_CELL.DEFECT_SEQ_NO + " " + _EDA_FILE_LCM_CELL.DEFECT_CODE + " " + _EDA_FILE_LCM_CELL.S + " " + _EDA_FILE_LCM_CELL.G + " " + _EDA_FILE_LCM_CELL.DEFECT_COLOR + " " + _EDA_FILE_LCM_CELL.SPEC13_FLAG + " " + _EDA_FILE_LCM_CELL.DEFECT_NAME + " " + _EDA_FILE_LCM_CELL.SPEC14_FLAG + " " + _EDA_FILE_LCM_CELL.SPEC27_FLAG);


                // last Row  Add  Finished symbol  '@'
                if (j == dt_index_chip_id.Rows.Count - 1)
                {
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC15_FLAG);

                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC28_FLAG);

                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC29_FLAG);

                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC16_FLAG);


                }



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
        if (cca == "")
        {
        }
        else
        {
            email.CC.Add(cca);
        }

        if (file_path.Equals(""))
        {

        }
        else
        {
            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(file_path);
            email.Attachments.Add(attachment);
        }



        email.IsBodyHtml = true;
        smtp.Send(email);


    } 
    

}
