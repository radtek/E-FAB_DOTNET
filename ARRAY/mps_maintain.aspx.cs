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

public partial class ARRAY_mps_maintain : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE_STD2"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_MPS_MAINTAIN_XLS"];
   
   


    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string sql_temp4 = "";
    string sql_temp6 = "";
    string date_temp = "";
    string shop_temp = "";
    string delete_sql ="";
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
    //private const string FileName = @"D:\\CIM-SE-RPT-WEB\\E-FAB_dotnet\\ARRAY\\mps_maintain.aspx";



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
            sql_temp = "SELECT * FROM [Sheet1$]   ";

            ds_temp1 = func.get_dataSet_access(sql_temp, conn1);
            DT = ds_temp1.Tables[0];
            GridView1.DataSource = ds_temp1.Tables[0];
            GridView1.DataBind();




        }





    }

    public void insert_mps_array_maintain()
    {

        delete_sql = @"
delete quantity tt
 where tt.fab='{0}' and tt.shop='{1}' and tt.date_value like '{2}%'";

        delete_sql = string.Format(delete_sql, DT.Rows[0][2].ToString(), DT.Rows[0][3].ToString(), DT.Rows[0][10].ToString().Substring(0,6));

        func.get_sql_execute(delete_sql, conn);

        for (int i = 0; i <= DT.Rows.Count - 1; i++)
        {
            date_temp = DT.Rows[i][10].ToString().Substring(0, 6);
            shop_temp = DT.Rows[i][3].ToString();

            if (i >= 0)
            {

                sql_temp6 = @"select 
       item,
       fab,
       shop,
       product_name,
        group_id,
       eq_id,
       value,
       date_type,
       date_value,
       stage_name,
       panel_size,
       lot_type,
       expect_value
  
      
  from quantity t
  where 
        t.item='{0}' 
        and t.fab='{1}' 
        and t.shop='{2}'
        and t.product_name='{3}' 
        and t.group_id='{4}' 
        and t.eq_id='{5}'
        and t.date_type='{6}' 
        and t.date_value='{7}'
        and t.stage_name='{8}' 
        and t.panel_size='{9}' 
        and t.lot_type='{10}'
     ";

                sql_temp6 = string.Format(sql_temp6, DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), DT.Rows[i][3].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][6].ToString(), DT.Rows[i][7].ToString(), DT.Rows[i][9].ToString(), DT.Rows[i][10].ToString(), DT.Rows[i][11].ToString(), DT.Rows[i][12].ToString(), DT.Rows[i][13].ToString());
                    
//                sql_temp3 = " select item,                             " +
//"        fab,                              " +
//"        shop,                             " +
//"        product_name,                     " +
//"         group_id,                        " +
//"        eq_id,                            " +
//"        value,                            " +
//"        date_type,                        " +
//"        date_value,                       " +
//"        stage_name,                       " +
//"        panel_size,                       " +
//"        lot_type,                         " +
//"        expect_value                      " +
//"                                          " +
//"                                          " +
//"   from quantity t                        " +
//"   where                                  " +
//"             t.item='" + DT.Rows[i][1].ToString() + "'                     " +
//"         and t.fab='" + DT.Rows[i][2].ToString() + "'                   " +
//"         and t.shop='" + DT.Rows[i][3].ToString() + "'                 " +
//"         and t.product_name='" + DT.Rows[i][4].ToString() + "'         " +
//"         and t.group_id='" + DT.Rows[i][6].ToString() + "'             " +
//"         and t.eq_id='" + DT.Rows[i][7].ToString() + "'                " +
//"         and t.date_type='" + DT.Rows[i][9].ToString() + "'            " +
//"         and t.date_value='" + DT.Rows[i][10].ToString() + "'           " +
//"         and t.stage_name='" + DT.Rows[i][11].ToString() + "'           " +
//"         and t.panel_size='" + DT.Rows[i][12].ToString() + "'           " +
//"         and t.lot_type='" + DT.Rows[i][13].ToString() + "'            ";





                ds_temp2 = func.get_dataSet_access(sql_temp6, conn);

                if (ds_temp2.Tables[0].Rows.Count> 0)
                {

                    sql_temp4 = @"update quantity
   set item = '{0}',
       fab = '{1}',
       shop = '{2}',
       product_name = '{3}',
       step_name = '{4}',
       group_id = '{5}',
       eq_id = '{6}',
       value ='{7}' ,
       date_type = '{8}',
       date_value = '{9}',
       stage_name = '{10}',
       panel_size = '{11}',
       lot_type = '{12}',
       expect_value ='{13}' ,
       update_dttm = sysdate,
       user_id = 'EXCEL_LOADER'
 where 
       item='{0}'
   and fab='{1}'
   and shop='{2}'
   and product_name='{3}'
   and step_name='{4}'
   and eq_id='{6}'
   and date_type='{8}'
   and date_value='{9}'
   and stage_name='{10}'
   and panel_size='{11}'
   and lot_type='{12}'";

                    sql_temp4 = string.Format(sql_temp4, DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), DT.Rows[i][3].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][5].ToString(), DT.Rows[i][6].ToString(), DT.Rows[i][7].ToString(), DT.Rows[i][8].ToString(), DT.Rows[i][9].ToString(), DT.Rows[i][10].ToString(), DT.Rows[i][11].ToString(), DT.Rows[i][12].ToString(), DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString());

                    func.get_sql_execute(sql_temp4, conn);
                }
                else
                {
                     
                sql_temp1 = @"insert into quantity
  (item, fab, shop, product_name, step_name, group_id, eq_id, value, date_type, date_value, stage_name, panel_size, lot_type, expect_value, update_dttm, user_id)
values
  ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}', '{8}', '{9}', '{10}', '{11}', '{12}','{13}',sysdate,'EXCEL_LOADER')";


                sql_temp1 = string.Format(sql_temp1, DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), DT.Rows[i][3].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][5].ToString(), DT.Rows[i][6].ToString(), DT.Rows[i][7].ToString(), DT.Rows[i][8].ToString(), DT.Rows[i][9].ToString(), DT.Rows[i][10].ToString(), DT.Rows[i][11].ToString(), DT.Rows[i][12].ToString(), DT.Rows[i][13].ToString(), DT.Rows[i][14].ToString());


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

                    Label1.Text = "quantity: id " + DT.Rows[i][0].ToString() + " quantity Row item Reapted!!!";
                    //Response.Write("user_id:" + DT.Rows[i][12].ToString()+" Account Reapted!!!");

                }

                
                }
              


            }


        }





    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

          
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
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


                #region insert data to ODS_ARY stdman mapping Db
                sql_temp = "SELECT * FROM  [Sheet1$] ";

                ds_temp1 = func.get_dataSet_access(sql_temp, conn1);
                DT = ds_temp1.Tables[0];

                insert_mps_array_maintain();
              

                #endregion




                
            }

            LabelX.Text = " <br>上傳成功!!!" + myLabel.ToString();


        }

        sql_temp = @"select t.* from quantity t 
where t.date_value like '{0}'  and t.shop='{1}' ";

        sql_temp = string.Format(sql_temp, date_temp + "%", shop_temp);

        ds_temp1 = func.get_dataSet_access(sql_temp, conn);
        GridView1.DataSource = ds_temp1.Tables[0];
        GridView1.DataBind();



    }

    
}
