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

public partial class CF_CURRENT_COMPONENT_QUERY : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CFT"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_HOLIDAY_MAINTAIN_XLS"];




    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string sql_temp4 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    ArrayList AL_temp = new ArrayList();
    DataTable DT_TEMP = new DataTable();

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
            //sql_temp = "SELECT * FROM eis_holiday_maintain order by holiday_dttm desc ";

            //ds_temp1 = func.get_dataSet_access(sql_temp, conn);
            //DT = ds_temp1.Tables[0];
            //GridView1.DataSource = ds_temp1.Tables[0];
            //GridView1.DataBind();




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


                //#region insert data to New Alarm Server mapping Db
                //sql_temp = "SELECT * FROM  [holiday_maintain$] ";

                //ds_temp1 = func.get_dataSet_access(sql_temp, conn1);
                //DT = ds_temp1.Tables[0];

                //insert_eis_holiday_maintain();


                //#endregion





            }

            LabelX.Text = " <br>上傳成功!!!" + myLabel.ToString();


        }

        #region MyRegion
                          
        //Build up temp Data Table For N count Query Data


        String[] Column_Name ={ "vendor_lot_id", "lot_id", "prod_name", "step_name", "glass_id", "last_Track_out", "cst_id" };

        DataColumn column = new DataColumn();

        for (int j = 0; j <= Column_Name.Length - 1; j++)
        {

            column = new DataColumn(Column_Name[j], typeof(System.String));
            DT_TEMP.Columns.Add(column);

        }


        #endregion

       


          AL_temp = func.FileToArray(Server.MapPath("upload_file\\component_query.txt"));

for (int k = 0; k <= AL_temp.Count-1; k++)
			{

              sql_temp3 = @"select t.vendor_lot_id,t.lot_id,tt.prod_name,tt.step_name,t.glass_id,to_char(tt.last_step_trkout_dttm,'yyyy/MM/dd HH24:MI:SS') as last_Track_out,tt.cst_id from substrate t,lot tt
where t.glass_id='{0}' and t.lot_id=tt.lot_id";

              sql_temp3 = string.Format(sql_temp3, AL_temp[k].ToString());

              ds_temp3 =func.get_dataSet_access(sql_temp3, conn);

              DataRow row = DT_TEMP.NewRow(); 
          

              for (int M = 0; M <= ds_temp3.Tables[0].Rows.Count-1; M++)
              {

                  //row.ItemArray= new object[1,2,3];
                  
                  //row.ItemArray = new object[] { ds_temp3.Tables[0].Rows[M][0].ToString(), ds_temp3.Tables[0].Rows[M][1].ToString(), ds_temp3.Tables[0].Rows[M][2].ToString(), ds_temp3.Tables[0].Rows[M[3].ToString(), ds_temp3.Tables[0].Rows[M][4].ToString(),ds_temp3.Tables[0].Rows[M][5].ToString(),ds_temp3.Tables[0].Rows[M][6].ToString()};
                  
                
                  row[0] = ds_temp3.Tables[0].Rows[M][0].ToString(); 
                  row[1] = ds_temp3.Tables[0].Rows[M][1].ToString(); 
                  row[2] = ds_temp3.Tables[0].Rows[M][2].ToString(); 
                  row[3] = ds_temp3.Tables[0].Rows[M][3].ToString(); 
                  row[4] = ds_temp3.Tables[0].Rows[M][4].ToString(); 
                  row[5] = ds_temp3.Tables[0].Rows[M][5].ToString(); 
                  row[6] = ds_temp3.Tables[0].Rows[M][6].ToString(); 



                  DT_TEMP.Rows.Add(row); 


              }
			 
			}

        //sql_temp = "SELECT * FROM eis_holiday_maintain order by holiday_dttm desc ";

        //ds_temp1 = func.get_dataSet_access(sql_temp, conn);
        GridView1.DataSource = DT_TEMP;
        GridView1.DataBind();
       





    }

    public DataTable Bind_data1()
    {

     

        #region MyRegion

        //Build up temp Data Table For N count Query Data


        String[] Column_Name ={ "vendor_lot_id", "lot_id", "prod_name", "step_name", "glass_id", "last_Track_out", "cst_id" };

        DataColumn column = new DataColumn();

        for (int j = 0; j <= Column_Name.Length - 1; j++)
        {

            column = new DataColumn(Column_Name[j], typeof(System.String));
            DT_TEMP.Columns.Add(column);

        }


        #endregion




        AL_temp = func.FileToArray(Server.MapPath("upload_file\\component_query.txt"));

        for (int k = 0; k <= AL_temp.Count - 1; k++)
        {

            sql_temp3 = @"select t.vendor_lot_id,t.lot_id,tt.prod_name,tt.step_name,t.glass_id,to_char(tt.last_step_trkout_dttm,'yyyy/MM/dd HH24:MI:SS') as last_Track_out,tt.cst_id from substrate t,lot tt
where t.glass_id='{0}' and t.lot_id=tt.lot_id";

            sql_temp3 = string.Format(sql_temp3, AL_temp[k].ToString());

            ds_temp3 = func.get_dataSet_access(sql_temp3, conn);

            DataRow row = DT_TEMP.NewRow();


            for (int M = 0; M <= ds_temp3.Tables[0].Rows.Count - 1; M++)
            {

                //row.ItemArray= new object[1,2,3];

                //row.ItemArray = new object[] { ds_temp3.Tables[0].Rows[M][0].ToString(), ds_temp3.Tables[0].Rows[M][1].ToString(), ds_temp3.Tables[0].Rows[M][2].ToString(), ds_temp3.Tables[0].Rows[M[3].ToString(), ds_temp3.Tables[0].Rows[M][4].ToString(),ds_temp3.Tables[0].Rows[M][5].ToString(),ds_temp3.Tables[0].Rows[M][6].ToString()};


                row[0] = ds_temp3.Tables[0].Rows[M][0].ToString();
                row[1] = ds_temp3.Tables[0].Rows[M][1].ToString();
                row[2] = ds_temp3.Tables[0].Rows[M][2].ToString();
                row[3] = ds_temp3.Tables[0].Rows[M][3].ToString();
                row[4] = ds_temp3.Tables[0].Rows[M][4].ToString();
                row[5] = ds_temp3.Tables[0].Rows[M][5].ToString();
                row[6] = ds_temp3.Tables[0].Rows[M][6].ToString();



                DT_TEMP.Rows.Add(row);


            }

        }

        //sql_temp = "SELECT * FROM eis_holiday_maintain order by holiday_dttm desc ";

        //ds_temp1 = func.get_dataSet_access(sql_temp, conn);
        GridView1.DataSource = DT_TEMP;
        GridView1.DataBind();
        return DT_TEMP;
     
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView gv = new GridView();
        gv.DataSource = Bind_data1();
        gv.DataBind();
        ExportExcel(gv); 


    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




            //string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"]; 
            //string strSql_Pro; 
            //string snn1; 

            ////GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet(); 

            //strSql_Pro = "select distinct(t.prod_name) from tlms_tmp t "; 
            //strSql_Pro += "where t.tool_id='" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "'"; 


            //ds = func.get_dataSet_access(strSql_Pro, conn); 


            //((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0]; 
            //((DataList)e.Row.FindControl("DataList1")).DataBind(); 



            //strTaskID = ((DataRowView)e.Row.DataItem)["task_id"].ToString(); 
            // dv.RowFilter = "task_id=" + strTaskID; 
            //dv.Sort = "is_owner desc"; 

            //task member datalist 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataSource = dv; 
            //((DataList)e.Row.FindControl("dlTaskMember")).DataBind(); 

            //image link to task content 

            //string sMessage = String.Format("return(OpenTask('{0}'));", strTaskID); 
            //((ImageButton)e.Row.FindControl("btnEdit")).OnClientClick = sMessage;//"if (OpenTask('" + sMessage + "')==false) {return false;}"; 
            //Int32 percent_value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "percent1")); 
            //Int32 countX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "count1"));
            //Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 

            //string pp = DataBinder.Eval(e.Row.DataItem, "Current_price").ToString();

            //Int32 pricexx = Convert.ToInt32(price1); 



            // if (percent_value >0) 
            //e.Row.Cells[0].BackColor = Color.Yellow; 
            // e.Row.Cells[6].Style.Add("background-color", "#FFFF80"); 
            //if (countX >= 3)
               // e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
                //e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
                //e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    } 


    private void ExportExcel(GridView SeriesValuesDataGrid)
    {

        string filename = "";
        string today_detail_char = DateTime.Now.AddDays(+0).ToString("yyyy/MM/ddHHmmss").Replace("/", "");
        filename = "Component_Query_" + today_detail_char + ".xls";
        filename = "attachment;filename=" + filename;
        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader("content-disposition", filename);

        //Response.AddHeader("content-disposition", "attachment;filename=\"" + filename + "\";"); 

        Response.Charset = "big5";

        // If you want the option to open the Excel file without saving than 

        // comment out the line below 

        // Response.Cache.SetCacheability(HttpCacheability.NoCache); 

        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        SeriesValuesDataGrid.AllowPaging = false;
        SeriesValuesDataGrid.DataBind();

        SeriesValuesDataGrid.RenderControl(htmlWrite);

        string head = " <html> " +
        " <head><meta http-equiv='Content-Type' content='text/html; charset=big5'></head> " +
        " <body> ";

        string footer = " </body>" +
        " </html>";

        Response.Write(head + stringWrite.ToString() + footer);

        Response.End();

        SeriesValuesDataGrid.AllowPaging = true;
        SeriesValuesDataGrid.DataBind();





    } 




}
