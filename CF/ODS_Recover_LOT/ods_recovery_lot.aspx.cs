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
using System.Data.OracleClient;
using System.IO;

public partial class CF_ODS_Recover_LOT_ods_recovery_lot : System.Web.UI.Page
{

    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CFT"];

    string sql = "";
    string sql2 = "";
    string sql3 = "";

    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    Int32 count_num = 0;
    string run_type = "";
    string flag = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            flag = Request.QueryString["flag"];

            if (Request.QueryString["flag"] != null && Request.QueryString["flag"] != "")
            {
                flag = "ASSIGN";
            }
            else
            {

                flag = "FUCK";
            
           }

           if (flag.Equals("ASSIGN"))
            {

                run_type = "ONE";
            
            }
            else
            {
               
                run_type = "ALL";

            
            }
            
            
            //Button1_Click(null, null);

            if (run_type.Equals("ALL"))
            {
                func.delete_log_file(Server.MapPath(".") + "\\lot_list\\", "*.txt", 0.5);
            }
          

            sql3 = @"select 

case when instr(ot2.wipid,' ')>0 then trim(substr(ot2.wipid,0,instr(ot2.wipid,' ')))
     else trim(ot2.wipid) end wipid
  from (

select distinct(ot1.aaa) as wipid from (

select trim( substr(substr(tt.errmsg,instr(tt.errmsg,'wipid='),21),7,20) ) as aaa  from dw_etl_runlog tt
where tt.procedurename like '%' and tt.lastrunsysdate>sysdate-1
and
tt.errmsg like '%wipid=%'
order by tt.lastrunsysdate desc
) ot1

) ot2 ";

            ds_temp1 = func.get_dataSet_access(sql3, conn);

            if (run_type.Equals("ALL"))
            {
                if (ds_temp1.Tables[0].Rows.Count == 0)
                {

                    write_log("oscar_test", Server.MapPath(".") + "\\lot_list\\", "AAA");
                }
                else
                {
                    for (int i = 0; i <= ds_temp1.Tables[0].Rows.Count - 1; i++)
                    {

                        write_log(ds_temp1.Tables[0].Rows[i][0].ToString(), Server.MapPath(".") + "\\lot_list\\", "AAA");

                    }


                    func.write_log("ODS_Recover_Lot " + ds_temp1.Tables[0].Rows.Count.ToString() + " Lots", Server.MapPath("..\\..\\") + "\\LOG\\", "log");
                
                }
                 
                
               

            }
            else
            {
                ArrayList arlist = new ArrayList();

                arlist = func.FileToArray(Server.MapPath(".") + "\\lot_list\\lot_list.txt");



                func.write_log("ODS_Recover_Lot Assign Lots " + arlist.Count.ToString(), Server.MapPath("..\\..\\") + "\\LOG\\", "log");
              
            }
            
          

            

            func.write_log("ODS_Recover_Lot " + ds_temp1.Tables[0].Rows.Count.ToString() +" Lots", Server.MapPath("..\\..\\") + "\\LOG\\", "log");
            Button1_Click(null, null);
            Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.open('','_self'); window.close();\",null)</script>");
        
        }

        
      

      


    }



    public static void write_log(string program_name, string file_path, string file_type)
    {
        StreamWriter sw;
        DirectoryInfo di;//宣告目錄 
        FileInfo fi;//宣告檔案 
        string program_name1 = program_name;
        //di = new DirectoryInfo(Server.MapPath(".") + "\\RUN_LOG\\" ); //DateTime.Now.ToString("yyyyMMdd") 
        di = new DirectoryInfo(file_path); //DateTime.Now.ToString("yyyyMMdd") 
        //fi = new FileInfo(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
        fi = new FileInfo(file_path + "lot_list.txt");

        if (!di.Exists)
        {
            di.Create();//目錄不存在 產生目錄 
        }
        if (fi.Exists == true)
        {
            //檔案存在 寫檔案 
            //sw = File.AppendText(Server.MapPath(".") + "\\RUN_LOG\\" + DateTime.Now.ToString("yyyyMMdd") + ".log"); 
            sw = File.AppendText(file_path + "lot_list.txt");
        }
        else
        {
            sw = fi.CreateText(); //檔案不存在 產生檔案 
        }


        sw.WriteLine(program_name1);
       
        sw.Close();


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ArrayList arlist = new ArrayList();

        arlist = func.FileToArray(Server.MapPath(".")+"\\lot_list\\lot_list.txt");



        for (int i = 0; i <= arlist.Count-1; i++)
        {

            sql = @"select count(t.lot_id) as counter from lot t
where t.lot_id='{0}'";

            sql = string.Format(sql, arlist[i].ToString());

            ds_temp = func.get_dataSet_access(sql, conn);

            if (Convert.ToInt32(ds_temp.Tables[0].Rows[0]["counter"]) >= 1)
            {

            }
            else
            {


                sql2 = @"insert into lot
  (lot_id, array_ship_dttm,fab ,shop,cf_lotstart_dttm,glass_qty )
values
  ('{0}', '','Fab1', 'T1CF',sysdate,1)";
                sql2 = string.Format(sql2, arlist[i].ToString());

                func.get_sql_execute(sql2, conn);
            
            }
            CallingOracleStoredProc(arlist[i].ToString()); 

        }

       

    }

    public void CallingOracleStoredProc(string lot_id)
    {

        using (OracleConnection objConn = new OracleConnection("Data Source=ODS_CF; User ID=innrpt; Password=plumcft"))
        {

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = objConn;

            objCmd.CommandText = "ods_t1f_oscar.main_lot"; // "package.procedure" 

            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("vLot_id_s", OracleType.VarChar).Value = lot_id;

            // objCmd.Parameters.Add("pout_count", OracleType.Number).Direction = ParameterDirection.Output; 



            try
            {

                objConn.Open();

                objCmd.ExecuteNonQuery();

                //System.Console.WriteLine("Number of employees in department 20 is {0}", objCmd.Parameters["pout_count"].Value); 

            }

            catch (Exception ex)
            {

                System.Console.WriteLine("Exception: {0}", ex.ToString());

            }



            objConn.Close();

        }

    } 



}
