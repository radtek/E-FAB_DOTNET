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

public partial class Alarm_holiday_maintain : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_HOLIDAY_MAINTAIN_XLS"];
   
   


    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    //定義OLE======================================================

    //1.檔案位置

    //private const string FileName =  "C:\\test.xls";
    //private const string FileName = @"D:\\CIM-SE-RPT-WEB\\E-FAB_dotnet\\Alarm\\Alarm_sample.xls";



    //2.提供者名稱

    //private const string ProviderName = "Microsoft.Jet.OLEDB.4.0;";

    //3.Excel版本，Excel 8.0 針對Excel2000及以上版本，Excel5.0 針對Excel97。

    //private const string ExtendedString = "'Excel 8.0;";

    //4.第一行是否為標題

    //private const string Hdr = "Yes;";

    //5.IMEX=1 通知驅動程序始終將「互混」數據列作為文本讀取

    //private const string IMEX = "0';";

    //=============================================================



    //連線字串

    //string cs =

    //        "Data Source=" + FileName + ";" +

    //        "Provider=" + ProviderName +

    //        "Extended Properties=" + ExtendedString +

    //        "HDR=" + Hdr +

    //        "IMEX=" + IMEX;

    DataTable DT = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sql_temp = "SELECT * FROM eis_holiday_maintain order by holiday_dttm desc ";

            ds_temp1 = func.get_dataSet_access(sql_temp, conn);
            DT = ds_temp1.Tables[0];
            GridView1.DataSource = ds_temp1.Tables[0];
            GridView1.DataBind();




        }





    }

    public void insert_eis_holiday_maintain()
    {

        for (int i = 0; i <= DT.Rows.Count - 1; i++)
        {

            if (i >= 0)
            {
                sql_temp1 = @"insert into eis_holiday_maintain
  (holiday_dttm, holiday_desc, update_dttm)
values
  ('{0}', '{1}', sysdate)";


                sql_temp1 = string.Format(sql_temp1, DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString());


                try
                {
                    func.get_sql_execute(sql_temp1, conn);
                }
                catch (Exception)
                {
                    //                    sql_temp2 = @"insert into brm_user
                    //  (user_id, user_name, user_password, user_group_id, department_id, auth_level, user_e_mail, user_sms_num, user_mobil_tel, user_start_time, user_end_time, createuser_id, lastmodifieduser_id, createuser_name, lastmodifieduser_name, createdate, lastmodifieddate, mycomment)
                    //values
                    //  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',  '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}')";


                    //                    sql_temp2 = string.Format(sql_temp2, DT.Rows[i][12].ToString() + "_01", DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString(), "G002", DT.Rows[i][16].ToString(), "1", DT.Rows[i][18].ToString(), DT.Rows[i][19].ToString(), DT.Rows[i][20].ToString(), DT.Rows[i][21].ToString(), DT.Rows[i][22].ToString(), DT.Rows[i][23].ToString(), DT.Rows[i][24].ToString(), DT.Rows[i][25].ToString(), DT.Rows[i][26].ToString(), today_detail, DT.Rows[i][28].ToString(), DT.Rows[i][29].ToString());

                    //                    func.get_sql_execute(sql_temp2, conn1);

                    Label1.Text = "eis_holiday_maintain:HOLIDAY_DTTM " + DT.Rows[i][1].ToString() + " HOLIDAY_DTTM 1 item Reapted!!!";
                    //Response.Write("user_id:" + DT.Rows[i][12].ToString()+" Account Reapted!!!");

                }



            }


        }





    }

    

    protected void ButtonUpload_Click(object sender, EventArgs e)
    {

        System.Text.StringBuilder myLabel = new System.Text.StringBuilder();

        string strSql123 = "";
        for (int i = 1; i <= Request.Files.Count; i++)
        {
            FileUpload myFL = new FileUpload();
            myFL = (FileUpload)Page.FindControl("FileUpload" + i);
            if ((myFL.PostedFile != null) && (myFL.PostedFile.ContentLength > 0))
            {

                string fn = System.IO.Path.GetFileName(myFL.PostedFile.FileName);
                string SaveLocation = Server.MapPath("upload_file/") + fn;
                int file_size = myFL.PostedFile.ContentLength;
                string file_type = myFL.PostedFile.ContentType;
                //try
                //{
                //    myFL.PostedFile.SaveAs(SaveLocation);

                //    OleDbConnection myConnection = new OleDbConnection(ConfigurationSettings.AppSettings["dsnn"]);

                //    string strClientIP;

                //    strClientIP = Request.ServerVariables["remote_host"].ToString();


                //    strSql123 = "insert into mms_meeting_upload_file";

                //    strSql123 += "(aid, aid2, file_path, file_name, file_size, file_type, ip, user_id) values";

                //    strSql123 += "('" + Request.QueryString["aid"].ToString() + "', '" + Request.QueryString["aid2"].ToString() + "', '" + SaveLocation + "', '" + fn + "', '" + file_size + "', '" + file_type + "', '" + strClientIP + "', 'test')";
                //    //strSql123 += "(1, 2, '" + SaveLocation + "', '" + fn + "', '" + file_size + "', '" + file_type + "', '" + strClientIP + "', 'test')"; 


                //    OleDbCommand myCommand = new OleDbCommand(strSql123, myConnection);
                //    myConnection.Open();
                //    OleDbDataReader MyReader = myCommand.ExecuteReader();
                //    myLabel.Append("<hr>--- " + fn);

                //    Label2.Text = " 上傳成功!!!" + myLabel.ToString();

                //}
                //catch (Exception Ex)
                //{
                //    Response.Write("銝撜瑼獢憭望");
                //}

                myFL.PostedFile.SaveAs(SaveLocation);

                myLabel.Append("<BR><hr>--- " + fn);

               // LabelX.Text = " <br>上傳成功!!!" + myLabel.ToString();


                #region insert data to New Alarm Server mapping Db
                sql_temp = "SELECT * FROM  [holiday_maintain$] ";

                ds_temp1 = func.get_dataSet_access(sql_temp, conn1);
                DT = ds_temp1.Tables[0];

                insert_eis_holiday_maintain();
              

                #endregion




                
            }

            LabelX.Text = " <br>上傳成功!!!" + myLabel.ToString();


        }

        sql_temp = "SELECT * FROM eis_holiday_maintain order by holiday_dttm desc ";

        ds_temp1 = func.get_dataSet_access(sql_temp, conn);
        GridView1.DataSource = ds_temp1.Tables[0];
        GridView1.DataBind();



    }

    
}
