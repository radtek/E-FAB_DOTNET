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


public partial class Gridview_Example_UPLOAD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //--註解：網站上的目錄路徑。所以不寫磁碟名稱（不寫 “實體”路徑）。
        //string saveDir = "ddd";
        //string appPath = Request.PhysicalApplicationPath;

        //string tempfileName = "";
        System.Text.StringBuilder myLabel = new System.Text.StringBuilder();
        ////如果事先宣告 using System.Text;
        ////便可改寫成 StringBuilder myLabel = new StringBuilder();

        //for(int i = 1; i <= Request.Files.Count; i++)
        //{
        //    FileUpload myFL = new FileUpload();
        //    myFL = (FileUpload)Page.FindControl("FileUpload" + i);

        //    if (myFL.HasFile) {
        //        string fileName = myFL.FileName;
        //        string pathToCheck = appPath + saveDir + fileName;

        //        //===========================================(Start)

        //  if (!System.IO.File.Exists(pathToCheck))

        //  {
        //            int my_counter  = 2;
        //            while (System.IO.File.Exists(pathToCheck))
        //            {
        //                //--檔名相同的話，目前上傳的檔名（改成 tempfileName），
        //                //  前面會用數字來代替。
        //                tempfileName = my_counter.ToString() + "_" + fileName;
        //                pathToCheck = appPath + saveDir + tempfileName;
        //                my_counter = my_counter + 1;
        //            }
        //            fileName = tempfileName;
        //            Label1.Text += "<br>抱歉，您上傳的檔名發生衝突，檔名修改如下---- " + fileName;

        //        //-- 完成檔案上傳的動作。
        //        string savePath = appPath + saveDir + fileName;
        //        myFL.SaveAs(savePath);


        //  }



        //        //===========================================(End)
        // myLabel.Append("<hr>檔名---- " + fn );
        //    }
        //}

        //Label2.Text = "上傳成功" + myLabel.ToString();
        string strSql123 = "";
        for (int i = 1; i <= Request.Files.Count; i++)
        {
            FileUpload myFL = new FileUpload();
            
            myFL = (FileUpload)Page.FindControl("FileUpload" + i);
            if (myFL.PostedFile.ContentLength < 15360000)
            //if (myFL.PostedFile.ContentLength < 30720000)
            {

                if ((myFL.PostedFile != null) && (myFL.PostedFile.ContentLength > 0))
                {

                    string fn = System.IO.Path.GetFileName(myFL.PostedFile.FileName);
                   // string SaveLocation = Server.MapPath("../") + "\\upload_file\\" + fn;
                    string SaveLocation = Server.MapPath("../upload_file/") + fn;
                    int file_size = myFL.PostedFile.ContentLength;
                    string file_type = myFL.PostedFile.ContentType;
                    try
                    {
                        myFL.PostedFile.SaveAs(SaveLocation);

                        //OleDbConnection myConnection = new OleDbConnection(ConfigurationSettings.AppSettings["dsnn"]);

                        //string strClientIP;

                        //strClientIP = Request.ServerVariables["remote_host"].ToString();


                        //strSql123 = "insert into mms_meeting_upload_file";

                        //strSql123 += "(aid, aid2, file_path, file_name, file_size, file_type, ip, user_id) values";

                        //strSql123 += "('" + Request.QueryString["aid"].ToString() + "', '" + Request.QueryString["aid2"].ToString() + "', '" + SaveLocation + "', '" + fn + "', '" + file_size + "', '" + file_type + "', '" + strClientIP + "', 'test')";
                        ////strSql123 += "(1, 2, '" + SaveLocation + "', '" + fn + "', '" + file_size + "', '" + file_type + "', '" + strClientIP + "', 'test')";


                        //OleDbCommand myCommand = new OleDbCommand(strSql123, myConnection);
                        //myConnection.Open();
                        //OleDbDataReader MyReader = myCommand.ExecuteReader();
                        myLabel.Append("<hr>檔名---- " + fn);

                        Label2.Text = "上傳成功" + myLabel.ToString();

                    }
                    catch (Exception Ex)
                    {
                        Response.Write("上傳檔案失敗");
                    }
                }
            }

            else

            {
                Label3.Visible = true;
                Label3.Text = "上傳檔案超過15MB...";
            
            }
 
          






        }

        parser_data_in_DB();

    }

    private static void parser_data_in_DB()
    {
        string user_id = "TWN050555";
        string max_sn = "";

        String connX = "Provider=Microsoft.Jet.OLEDB.4.0;" +
  "Data Source=" + "D:\\report\\Tooling\\upload_file\\Tool_Query_sample.xls" + ";" +
  "Extended Properties=Excel 8.0;";

        //String conn = " Provider=MSDASQL;Driver={Microsoft Excel Driver (*.xls)};DBQ=D:\\report\\Tooling\\upload_file\\Tool_Query.xls;ReadOnly=false ;";


      

         string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"];

        string sql_excel = "select * from [Sheet1$]";

        string sql_max_sn = "select max(TO_NUMBER(t.sn)) as max_num from tlms_main t  ";

        string sql_insert_db = " ";

        DataSet ds_max_sn = new DataSet();

        ds_max_sn = func.get_dataSet_access(sql_max_sn, conn1);

    
        max_sn = ds_max_sn.Tables[0].Rows[0]["max_num"].ToString();

        Int32 count_sn = Convert.ToInt32(max_sn);

        count_sn++; //initial SN

        // get excel data to  DataSet 
        DataSet ds_excel_insert = new DataSet();

        ds_excel_insert = func.get_dataSet_access(sql_excel, connX);

        for (int i = 0; i <= ds_excel_insert.Tables[0].Rows.Count-1; i++)
        {
            if (i == 050123587965)
            {

            }

            else
            {

                DataSet ds_dump = new DataSet();

                string str_dump = "";

                if(ds_excel_insert.Tables[0].Rows[i]["EQ_TYPE"].ToString()=="")
                {
                  str_dump = " select count(*) as COUNT_NUM from tlms_main t  " +

 " where t.site='" + ds_excel_insert.Tables[0].Rows[i]["SITE"].ToString() + "'   and t.fab='" + ds_excel_insert.Tables[0].Rows[i]["FAB"].ToString() + "'  and t.process='" + ds_excel_insert.Tables[0].Rows[i]["PROCESS"].ToString() + "' and t.model_name='" + ds_excel_insert.Tables[0].Rows[i]["MODEL_NAME"].ToString() + "' and t.layer='" + ds_excel_insert.Tables[0].Rows[i]["LAYER"].ToString() + "' and t.eq_vender='" + ds_excel_insert.Tables[0].Rows[i]["EQ_VENDER"].ToString() + "' and t.eq_type is null " +
 "                                                                                                                         " +
 " and t.tool_id='" + ds_excel_insert.Tables[0].Rows[i]["TOOL_ID"].ToString() + "' and t.tool_version='" + ds_excel_insert.Tables[0].Rows[i]["TOOL_VERSION"].ToString() + "'                                                                                  ";
                  
                }
                else
                {
                   str_dump = " select count(*) as COUNT_NUM from tlms_main t  "+

" where t.site='" + ds_excel_insert.Tables[0].Rows[i]["SITE"].ToString() + "'   and t.fab='" + ds_excel_insert.Tables[0].Rows[i]["FAB"].ToString() + "'  and t.process='" + ds_excel_insert.Tables[0].Rows[i]["PROCESS"].ToString() + "' and t.model_name='" + ds_excel_insert.Tables[0].Rows[i]["MODEL_NAME"].ToString() + "' and t.layer='" + ds_excel_insert.Tables[0].Rows[i]["LAYER"].ToString() + "' and t.eq_vender='" + ds_excel_insert.Tables[0].Rows[i]["EQ_VENDER"].ToString() + "' and t.eq_type='" + ds_excel_insert.Tables[0].Rows[i]["EQ_TYPE"].ToString() + "' " +
"                                                                                                                         " +
" and t.tool_id='" + ds_excel_insert.Tables[0].Rows[i]["TOOL_ID"].ToString() + "' and t.tool_version='" + ds_excel_insert.Tables[0].Rows[i]["TOOL_VERSION"].ToString() + "'                                                                                  ";
   
                
                }

               
               
                ds_dump = func.get_dataSet_access(str_dump, conn1);

                Int32 dump_num = 0;

                dump_num = Convert.ToInt32(ds_dump.Tables[0].Rows[0][0].ToString());

                if (dump_num >= 1)
                {
                    sql_insert_db = " update tlms_main                           " +
"    set site = '" + ds_excel_insert.Tables[0].Rows[i]["SITE"].ToString() + "',                      " +
"        fab = '" + ds_excel_insert.Tables[0].Rows[i]["FAB"].ToString() + "',                        " +
"        process = '" + ds_excel_insert.Tables[0].Rows[i]["PROCESS"].ToString() + "',                " +
"        model_name = '" + ds_excel_insert.Tables[0].Rows[i]["MODEL_NAME"].ToString() + "',          " +
"        layer = '" + ds_excel_insert.Tables[0].Rows[i]["LAYER"].ToString() + "',                    " +
"        eq_vender = '" + ds_excel_insert.Tables[0].Rows[i]["EQ_VENDER"].ToString() + "',            " +
"        eq_type = '" + ds_excel_insert.Tables[0].Rows[i]["EQ_TYPE"].ToString() + "',                " +
"        tool_id = '" + ds_excel_insert.Tables[0].Rows[i]["TOOL_ID"].ToString() + "',                " +
"        tool_version = '" + ds_excel_insert.Tables[0].Rows[i]["TOOL_VERSION"].ToString() + "',      " +
"        reason = '" + ds_excel_insert.Tables[0].Rows[i]["REASON"].ToString() + "',                  " +
"        library = '" + ds_excel_insert.Tables[0].Rows[i]["LIBRARY"].ToString() + "',                " +
"        pixel_cell = '" + ds_excel_insert.Tables[0].Rows[i]["PIXEL_CELL"].ToString() + "',          " +
"        panel_cell = '" + ds_excel_insert.Tables[0].Rows[i]["PANEL_CELL"].ToString() + "',          " +
"        sub_cell = '" + ds_excel_insert.Tables[0].Rows[i]["SUB_CELL"].ToString() + "',              " +
"        mask_cell = '" + ds_excel_insert.Tables[0].Rows[i]["MASK_CELL"].ToString() + "',            " +
"        gds = '" + ds_excel_insert.Tables[0].Rows[i]["GDS"].ToString() + "',                        " +
"        owner = '" + ds_excel_insert.Tables[0].Rows[i]["OWNER"].ToString() + "',                    " +
"        designer = '" + ds_excel_insert.Tables[0].Rows[i]["DESIGNER"].ToString() + "',              " +
"        tapeout_date = '" + ds_excel_insert.Tables[0].Rows[i]["TAPEOUT_DATE"] + "',      " +
"        mask_size = '" + ds_excel_insert.Tables[0].Rows[i]["MASK_SIZE"].ToString() + "',            " +
"        release = '" + ds_excel_insert.Tables[0].Rows[i]["RELEASE"].ToString() + "',                " +
"        release_date = '" + ds_excel_insert.Tables[0].Rows[i]["RELEASE_DATE"] + "',      " +
"        scrap = '" + ds_excel_insert.Tables[0].Rows[i]["SCRAP"].ToString() + "',                    " +
"        scrap_date = '" + ds_excel_insert.Tables[0].Rows[i]["SCRAP_DATE"] + "',          " +
"        discard = '" + ds_excel_insert.Tables[0].Rows[i]["DISCARD"].ToString() + "',                " +
"        discard_date = '" + ds_excel_insert.Tables[0].Rows[i]["DISCARD_DATE"].ToString() + "',      " +
"        test = '" + ds_excel_insert.Tables[0].Rows[i]["TEST"].ToString() + "',                      " +
"        status = '" + ds_excel_insert.Tables[0].Rows[i]["STATUS"].ToString() + "',                  " +
"        update_user = '" + user_id + "',        " +
"        dttm = sysdate ,                     " +
"         SN='" + ds_excel_insert.Tables[0].Rows[i]["SN"].ToString() + "'                                " +

"  where   site='" + ds_excel_insert.Tables[0].Rows[i]["SITE"].ToString() + "'  and                                   " +
" fab ='" + ds_excel_insert.Tables[0].Rows[i]["FAB"].ToString() + "' and  " +
" process ='" + ds_excel_insert.Tables[0].Rows[i]["PROCESS"].ToString() + "'    and " +
" model_name = '" + ds_excel_insert.Tables[0].Rows[i]["MODEL_NAME"].ToString() + "'  and  " +
" layer ='" + ds_excel_insert.Tables[0].Rows[i]["MODEL_NAME"].ToString() + "'  and  " +
"eq_vender ='" + ds_excel_insert.Tables[0].Rows[i]["EQ_VENDER"].ToString() + "'   and " +
"eq_type = '" + ds_excel_insert.Tables[0].Rows[i]["EQ_TYPE"].ToString() + "' and " +
" tool_id ='" + ds_excel_insert.Tables[0].Rows[i]["TOOL_ID"].ToString() + "'" +
"tool_version ='" + ds_excel_insert.Tables[0].Rows[i]["TOOL_VERSION"].ToString() + "' ";

                }
                else

                {
                    sql_insert_db = "  insert into tlms_main      " +
 "    (site,                    " +
 "     fab,                     " +
 "     process,                 " +
 "     model_name,              " +
 "     layer,                   " +
 "     eq_vender,               " +
 "     eq_type,                 " +
 "     tool_id,                 " +
 "     tool_version,            " +
 "     reason,                  " +
 "     library,                 " +
 "     pixel_cell,              " +
 "     panel_cell,              " +
 "     sub_cell,                " +
 "     mask_cell,               " +
 "     gds,                     " +
 "     owner,                   " +
 "     designer,                " +
 "     tapeout_date,            " +
 "     mask_size,               " +
 "     release,                 " +
 "     release_date,            " +
 "     scrap,                   " +
 "     scrap_date,              " +
 "     discard,                 " +
 "     discard_date,            " +
 "     test,                    " +
 "     status,                  " +
 "     update_user,             " +
 "     dttm,                    " +
 "     sn)                      " +
 "  values                      " +
 "    ('" + ds_excel_insert.Tables[0].Rows[i]["SITE"].ToString() + "',                  " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["FAB"].ToString() + "',                   " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["PROCESS"].ToString() + "',               " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["MODEL_NAME"].ToString() + "',            " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["LAYER"].ToString() + "',                 " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["EQ_VENDER"].ToString() + "',             " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["EQ_TYPE"].ToString() + "',               " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["TOOL_ID"].ToString() + "',               " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["TOOL_VERSION"].ToString() + "',          " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["REASON"].ToString() + "',                " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["LIBRARY"].ToString() + "',               " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["PIXEL_CELL"].ToString() + "',            " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["PANEL_CELL"].ToString() + "',            " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["SUB_CELL"].ToString() + "',              " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["MASK_CELL"].ToString() + "',             " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["GDS"].ToString() + "',                   " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["OWNER"].ToString() + "',                 " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["DESIGNER"].ToString() + "',              " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["TAPEOUT_DATE"] + "',          " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["MASK_SIZE"].ToString() + "',             " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["RELEASE"].ToString() + "',               " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["RELEASE_DATE"] + "',          " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["SCRAP"].ToString() + "',                 " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["SCRAP_DATE"] + "',            " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["DISCARD"].ToString() + "',               " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["DISCARD_DATE"] + "',          " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["TEST"].ToString() + "',                  " +
 "     '" + ds_excel_insert.Tables[0].Rows[i]["STATUS"].ToString() + "',                " +
 "     '" + user_id + "',           " +
 "     sysdate,                  " +
 "     " + count_sn + ")                   ";

                    func.get_sql_execute(sql_insert_db, conn1);

                    count_sn++;
                
                }


               
            }

           
           

        }
       



       

    
    }
 
}