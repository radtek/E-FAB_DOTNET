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

public partial class EDA_EDA_FILE_LCM : System.Web.UI.Page
{
    private OracleConnection orcn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"]); 
    IS.util.special sp = new IS.util.special();
    //file f = new file();

     StreamWriter sw;
     DirectoryInfo di;//宣告目錄 
     FileInfo fi;//宣告檔案 


    //string conn = System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"];
    string conn =  System.Configuration.ConfigurationSettings.AppSettings["EDAEDA"];
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

    string yesturday_shiftday = DateTime.Now.AddDays(-1).ToString("yyyyMMdd")+"07";
    string today_shiftday = DateTime.Now.AddDays(+0).ToString("yyyyMMdd") + "07";
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");

    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string last_hour = DateTime.Now.AddDays(-1 / 24).ToString("yyyyMMddHH");
    string last_twohour = DateTime.Now.AddDays(-2 / 24).ToString("yyyyMMddHH");
    string SaveLocation = "";
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
            set {  _SPEC_FLAG= value; }
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

  
   
        



    }
    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {

        create_array_repair_test_file_ini();
        create_celltest_file_ini();



        //javascript 語法填入 字串 
        string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_self');window.top.close(this);</script>";
        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmClose);
        



    }

    public void create_array_repair_test_file_ini()
    {

        string test_step = "'1920', 'S920', 'P920'";
        string repair_step = "'1930', 'S930', 'P930'";

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
                 else '**' end as RT
                 
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
           and t.glass_start_time >=
               to_date('{2}', 'yyyyMMddHH24')
           and t.glass_start_time <
               to_date('{3}', 'yyyyMMddHH24')
           and t.glass_id = ot1.glass_id(+)
        
        ) ot3,
       lcdsys.array_defect_t ot2, lcdsys.array_lot_hst_t ot4
       
 where ot3.step_id = ot2.step_id
   and ot3.glass_start_time = ot2.glass_start_time
   and ot3.glass_id = ot2.glass_id
   and ot2.ITEM2 <> '-'
   and ot3.lot_id=ot4.lot_id
   and ot3.step_id=ot4.step_id 
  
   





";
       // sql_temp1 = string.Format(sql_temp1, test_step, repair_step, yesturday_shiftday,today_shiftday);


        sql_temp1 = sql_temp1.Replace("{0}", test_step).Replace("{1}", repair_step).Replace("{2}", yesturday_shiftday).Replace("{3}", today_shiftday);



        //OracleCommand cmd = new OracleCommand(sql_temp1, orcn); 
        //OracleDataAdapter da=new OracleDataAdapter(cmd); 
        //DataSet ds=new DataSet(); 
        //da.Fill(ds,"test");
        create_array_repair_test_file(func.get_dataSet_access_oracle_client(sql_temp1, conn).Tables[0], "txt", _EDA_FILE_LCM);

        GridView1.DataSource = func.get_dataSet_access_oracle_client(sql_temp1, conn);
        GridView1.DataBind();

        System.Diagnostics.Process.Start(Server.MapPath("winrar.exe"), " a -ep " + Server.MapPath(".") + "//FILE//T1ARRAY//" + DateTime.Now.ToString("yyyyMMdd")+".rar" + " "+ Server.MapPath(".") + "//FILE//T1ARRAY//" + DateTime.Now.ToString("yyyyMMdd"));

    
    }

    public void create_celltest_file_ini()
    {

        string test_step = "'6615','6630','6815','6830'";


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
       '&' as SPEC5_FLAG,
       '>' as SPEC6_FLAG,
       '<' as SPEC7_FLAG,
       '{' as SPEC8_FLAG,
       '}' as SPEC9_FLAG,
       '[' as SPEC10_FLAG,
       ']' as SPEC11_FLAG,
       '%No Code Data  Gate  C RK Leve   ND  R PAT Defect Pattern Name  English Defect Name  CEDC Cell Re Eng Def Name JC BD Spare      Spare' as SPEC12_FLAG,
       substr(ot1.DEFECT_CODE,2,4) as DEFECT_CODE ,
       LPAD(LTRIM(ot1.S), 5, '0') as S,
       
       LPAD(LTRIM(ot1.G), 5, '0') as G,
        case when ot1.DEFECT_COLOR is null then '*'
             else ot1.DEFECT_COLOR end as DEFECT_COLOR ,
       '** ****** *** * *** ********************' as SPEC13_FLAG,
        RPAD(RTRIM(substr(ot1.DEFECT_NAME,0,20)), 20, ' ') as DEFECT_NAME,
       '**** ******************** **    ********** **********' as SPEC14_FLAG,
       '$N Cod STime Pattern Switch Name  Spare      Spare     ' as SPEC15_FLAG,
       '@' as SPEC16_FLAG,
      
       ot1.DEFECT_JUDGE_CODE,
       
      
       ot1.CT_DEFECT_CODE
       
       
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
                t2.Item51 as CT_DEFECT_CODE
        
          from lcdsys.cell_component_t t1, lcdsys.cell_defect_t t2
         where t1.step_id in ( {2})
              
             and t1.component_START_TIME >=to_date('{0}', 'yyyyMMddHH24') 
             and t1.component_START_TIME<to_date('{1}', 'yyyyMMddHH24')
           and t1.component_id = t2.component_id
           and t1.step_id = t2.step_id
           and t1.component_start_time = t2.component_start_time
          
        
        ) ot1
        


";
        //sql_temp1 = string.Format(sql_temp1, yesturday_shiftday, today_shiftday);


        sql_temp1 = sql_temp1.Replace("{0}", yesturday_shiftday).Replace("{1}", today_shiftday).Replace("{2}", test_step);




        //OracleCommand cmd = new OracleCommand(sql_temp1, orcn); 
        //OracleDataAdapter da=new OracleDataAdapter(cmd); 
        //DataSet ds=new DataSet(); 
        //da.Fill(ds,"test");
        create_cell_test_file(func.get_dataSet_access_oracle_client(sql_temp1, conn).Tables[0], "txt", _EDA_FILE_LCM_CELL);

        GridView2.DataSource = func.get_dataSet_access_oracle_client(sql_temp1, conn);
        GridView2.DataBind();

        System.Diagnostics.Process.Start(Server.MapPath("winrar.exe"), " a -ep " + Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd") + ".zip" + " " + Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd"));
    }

    public void create_array_repair_test_file(DataTable dt, string file_type, EDA_FILE_LCM _EDA_FILE_LCM)
     {

        DataTable dt_chip = new DataTable(); 
        DataView dv = new DataView();

         dv = dt.DefaultView;

         dt_chip = dv.ToTable(true, "REPR_STARTTIME", "CHIP_ID");
         DataTable dt_index_chip_id = new DataTable();
         
         
         for (int i = 0; i <= dt_chip.Rows.Count - 1; i++)
         {
             
             
             //di = new DirectoryInfo(Server.MapPath(".") + "\\RUN_LOG\\" ); //DateTime.Now.ToString("yyyyMMdd") 
             di = new DirectoryInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//T1ARRAY//"+ DateTime.Now.ToString("yyyyMMdd")); //DateTime.Now.ToString("yyyyMMdd")
             //fi = new FileInfo(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
             fi = new FileInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//T1ARRAY//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_chip.Rows[i]["CHIP_ID"] + "." + file_type);

             if (!di.Exists)
             {
                 di.Create();//目錄不存在 產生目錄 
             }
             if (fi.Exists == true)
             {
                 //檔案存在 寫檔案 
                 //sw = File.AppendText(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
                 sw = File.AppendText(HttpContext.Current.Server.MapPath(".") + "//FILE//T1ARRAY//" + DateTime.Now.ToString("yyyyMMdd") + "//"+ dt_chip.Rows[i]["CHIP_ID"] + "." + file_type);
             }
             else
             {
                 sw = fi.CreateText(); //檔案不存在 產生檔案 
             }
             
             
             
             
             dv.RowFilter = "CHIP_ID='" + dt_chip.Rows[i]["CHIP_ID"].ToString()+ "'";
             dv.Sort = "REPR_STARTTIME";

             dt_index_chip_id = dv.ToTable();

             string aaa = "";

             for (int j = 0; j <= dt_index_chip_id.Rows.Count-1; j++)
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

                       

                 aaa=String.Format("{0:000}", j+1); // 輸出 0001

                 _EDA_FILE_LCM.AP_COUNT = aaa;

                 _EDA_FILE_LCM.S = dt_index_chip_id.Rows[j]["S"].ToString();

                 _EDA_FILE_LCM.G = dt_index_chip_id.Rows[j]["G"].ToString();

                 _EDA_FILE_LCM.RETYPE = dt_index_chip_id.Rows[j]["RETYPE"].ToString();

                 _EDA_FILE_LCM.REASON = dt_index_chip_id.Rows[j]["REASON"].ToString();

                 _EDA_FILE_LCM.RT = dt_index_chip_id.Rows[j]["RT"].ToString();

              
                 _EDA_FILE_LCM.SPEC3_FLAG = dt_index_chip_id.Rows[j]["SPEC3_FLAG"].ToString();

                 _EDA_FILE_LCM.SPEC4_FLAG = dt_index_chip_id.Rows[j]["SPEC4_FLAG"].ToString();



                 #endregion

                 if(j==0)
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
        DataView dv = new DataView();

        dv = dt.DefaultView;

        dt_chip = dv.ToTable(true, "CELL_CHIP_ID", "STEP_ID", "STARTTIME", "OPERATION_MODE");
        DataTable dt_index_chip_id = new DataTable();


        for (int i = 0; i <= dt_chip.Rows.Count - 1; i++)
        {


            //di = new DirectoryInfo(Server.MapPath(".") + "\\RUN_LOG\\" ); //DateTime.Now.ToString("yyyyMMdd") 
            di = new DirectoryInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd") + "//"); //DateTime.Now.ToString("yyyyMMdd")
            //fi = new FileInfo(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
            fi = new FileInfo(HttpContext.Current.Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_chip.Rows[i]["CELL_CHIP_ID"] + "_" + dt_chip.Rows[i]["STEP_ID"] + "_" + dt_chip.Rows[i]["OPERATION_MODE"] + "." + file_type);

            if (!di.Exists)
            {
                di.Create();//目錄不存在 產生目錄 
            }
            if (fi.Exists == true)
            {
                //檔案存在 寫檔案 
                //sw = File.AppendText(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
                sw = File.AppendText(HttpContext.Current.Server.MapPath(".") + "//FILE//T1CELL//" + DateTime.Now.ToString("yyyyMMdd") + "//" + dt_chip.Rows[i]["CELL_CHIP_ID"] + "_" + dt_chip.Rows[i]["STEP_ID"] + "_" + dt_chip.Rows[i]["OPERATION_MODE"] + "." + file_type);
            }
            else
            {
                sw = fi.CreateText(); //檔案不存在 產生檔案 
            }




            dv.RowFilter = "CELL_CHIP_ID='" + dt_chip.Rows[i]["CELL_CHIP_ID"].ToString() + "'" + " and STEP_ID='" + dt_chip.Rows[i]["STEP_ID"].ToString() + "'";
            dv.Sort = "STARTTIME";

            dt_index_chip_id = dv.ToTable();

            string aaa = "";
            string DEFECT_COUNT = "";

            for (int j = 0; j <= dt_index_chip_id.Rows.Count - 1; j++)
            {
                #region initial data

                _EDA_FILE_LCM_CELL.CELL_CHIP_ID= dt_index_chip_id.Rows[j]["CELL_CHIP_ID"].ToString();

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


              


                #endregion

                if (j == 0)
                {
                    //  1 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.CELL_CHIP_ID + " " + _EDA_FILE_LCM_CELL.STEP_ID + " " + _EDA_FILE_LCM_CELL.SOURCE_CARRIER_ID);
                    //  2 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.EQUIP_ID + " " + _EDA_FILE_LCM_CELL.SPEC_FLAG + " " + _EDA_FILE_LCM_CELL.OPERATION_MODE + " " + _EDA_FILE_LCM_CELL.SPEC1_FLAG + " " + _EDA_FILE_LCM_CELL.TACT_TIME + " " + _EDA_FILE_LCM_CELL.SPEC2_FLAG + " " + _EDA_FILE_LCM_CELL.STARTTIME + " " + _EDA_FILE_LCM_CELL.ENDTIME + " " + _EDA_FILE_LCM_CELL.OPERATOR_ID);

                    //  3 Row
                    sw.WriteLine(" ");

                    //  4 Row
                    sw.WriteLine(" ");
                    //  5 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC3_FLAG + " " + _EDA_FILE_LCM_CELL.DEFECT_COUNT );

                    //  6 Row
                    sw.WriteLine(" ");
                    //  7 Row
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC4_FLAG + " " + _EDA_FILE_LCM_CELL.BATCH_ID);


                    //  8 Row
                    sw.WriteLine(" ");
                    //  9 Row
                    sw.WriteLine(" ");

                    //  10 Row
                    sw.WriteLine(" ");
                    //  11Row
                    sw.WriteLine(" ");

                    //  12 Row
                    sw.WriteLine(" ");

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
                sw.WriteLine(_EDA_FILE_LCM_CELL.DEFECT_SEQ_NO + " " + _EDA_FILE_LCM_CELL.DEFECT_CODE + " " + _EDA_FILE_LCM_CELL.S + " " + _EDA_FILE_LCM_CELL.G + " " + _EDA_FILE_LCM_CELL.DEFECT_COLOR + " " + _EDA_FILE_LCM_CELL.SPEC13_FLAG + " " + _EDA_FILE_LCM_CELL.DEFECT_NAME + " " + _EDA_FILE_LCM_CELL.SPEC14_FLAG);


                // last Row  Add  Finished symbol  '@'
                if (j == dt_index_chip_id.Rows.Count - 1)
                {
                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC15_FLAG);

                    sw.WriteLine(" ");

                    sw.WriteLine(" ");

                    sw.WriteLine(_EDA_FILE_LCM_CELL.SPEC16_FLAG);


                }



            }
            sw.Close();





        }














    }
}
