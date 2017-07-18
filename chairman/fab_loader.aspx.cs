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

public partial class chairman_fab_loader : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_FAB_LOAD_XLS"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_T2PRPT"];
    string subject = "";
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";

    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataTable DT = new DataTable();

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string tomorrow = DateTime.Now.AddDays(+1).ToString("yyyy/MM/dd");
    string today_minus_90 = DateTime.Now.AddDays(-90).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string today_detail1 = DateTime.Now.AddDays(+0).ToString("yyyyMMdd HH:mm:ss");
    
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            txtCalendar1.Text = today_minus_90;

            txtCalendar2.Text = tomorrow;




            sql_temp1 = @"
            select distinct(t.dttm) as dttm from fab_loading t
where t.dttm>='{0} '
and t.dttm<='{1} '
order by t.dttm desc

";

            sql_temp1 = string.Format(sql_temp1, txtCalendar1.Text.Replace("/", ""), txtCalendar2.Text.Replace("/", ""));
            
            DT = func.get_dataSet_access(sql_temp1, conn1).Tables[0];

            DropDownList1.DataTextField = "dttm";
            DropDownList1.DataValueField = "dttm";

            
            DropDownList1.DataSource = DT;

            DropDownList1.DataBind();
            
            
            
            
            sql_temp = @"

select ot2.name, ot2.site, ot2.plant,ot2.item, ot2.unit, round(ot2.datetime1,2) as datetime1,  round(ot2.datetime2,2) as datetime2,  round(ot2.datetime3,2) as datetime3, ot2.remark,ot2.dttm from fab_loading ot2

where ot2.dttm=(


select max(t.dttm) from fab_loading t
where t.dttm>='{0} '
and t.dttm<='{1} '
)

";

            sql_temp = string.Format(sql_temp, txtCalendar1.Text.Replace("/", ""), txtCalendar2.Text.Replace("/", ""));
            
            ds_temp1 = func.get_dataSet_access(sql_temp, conn1);
            GridView1.DataSource = ds_temp1.Tables[0];
            GridView1.DataBind();

            //insert_fab_loading();
        }





        
      
    }

    public void insert_fab_loading()
    {
        sql_temp = @"select * from  [Loading$]";


        ds_temp = func.get_dataSet_access(sql_temp, conn);

        DT = ds_temp.Tables[0];


        subject = DT.Columns[0].ColumnName;

        //GridView1.DataSource = ds_temp.Tables[0];

        //GridView1.DataBind();

        for (int i = 0; i <= 31 - 1; i++)
        {

            if (i >= 0)
            {
                sql_temp1 = @"insert into fab_loading
  (name, sn, site, plant, item, unit, datetime1, datetime2, datetime3, remark, dttm)
values
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')";


                sql_temp1 = string.Format(sql_temp1,subject,i.ToString(),DT.Rows[i][0].ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), DT.Rows[i][3].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][5].ToString(), DT.Rows[i][6].ToString(), DT.Rows[i][7].ToString(),  today_detail1);


                try
                {
                    func.get_sql_execute(sql_temp1, conn1);
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
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

        insert_fab_loading();


        sql_temp1 = @"
            select distinct(t.dttm) as dttm from fab_loading t
where t.dttm>='{0} '
and t.dttm<='{1} '
order by t.dttm desc

";

        sql_temp1 = string.Format(sql_temp1, txtCalendar1.Text.Replace("/", ""), txtCalendar2.Text.Replace("/", ""));

        DT = func.get_dataSet_access(sql_temp1, conn1).Tables[0];

        DropDownList1.DataTextField = "dttm";
        DropDownList1.DataValueField = "dttm";


        DropDownList1.DataSource = DT;

        DropDownList1.DataBind();

        Button1_Click(null, null);


    }



    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        Int32 rownum = 0;
        Int32 colnum = 10;

        Int32 counter = 1;

        #region merge column  RowSpan
        for (int i = 0; i <= colnum - 1; i++)
        {

            counter = 1; //change column reset counter

            if (i == 0 || i == 1 || i == 2 || i == 8 || i == 9) //assign  column  to Rowspan
            {
                for (int j = 1; j <= gv.Rows.Count - 1; j++)
                {



                    if (GridView1.Rows[j].Cells[i].Text.Trim() == GridView1.Rows[(j - 1)].Cells[i].Text.Trim())
                    {
                        counter++;
                        GridView1.Rows[j - counter + 1].Cells[i].RowSpan = counter;


                        GridView1.Rows[j].Cells[i].Visible = false;


                    }

                    else
                    {
                        counter = 1;
                        GridView1.Rows[j].Cells[i].RowSpan = counter;

                    }







                }


            }







        }
        #endregion

      

        //********************************//

        // merge row  ColumnSpan
       //#region merge row  ColumnSpan

       // for (int i = 0; i <= gv.Rows.Count - 1; i++)
       // {

       //     counter = 1;  // change row reset counter
       //     for (int j = 1; j <= colnum - 1; j++)
       //     {

       //         if (GridView1.Rows[i].Cells[j].Text.Trim() == GridView1.Rows[i].Cells[j - 1].Text.Trim())
       //         {
       //             counter++;
       //             GridView1.Rows[i].Cells[j - counter + 1].ColumnSpan = counter;


       //             GridView1.Rows[i].Cells[j].Visible = false;


       //         }

       //         else
       //         {
       //             counter = 1;
       //             GridView1.Rows[i].Cells[j].ColumnSpan = counter;

       //         }







       //     }
       // #endregion
       








        //}


    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        sql_temp = @"

select ot2.name, ot2.site, ot2.plant,ot2.item, ot2.unit, round(ot2.datetime1,2) as datetime1,  round(ot2.datetime2,2) as datetime2,  round(ot2.datetime3,2) as datetime3, ot2.remark,ot2.dttm from fab_loading ot2

where ot2.dttm='{0}'





";

        sql_temp = string.Format(sql_temp, DropDownList1.SelectedValue.ToString());

        ds_temp1 = func.get_dataSet_access(sql_temp, conn1);
        GridView1.DataSource = ds_temp1.Tables[0];
        GridView1.DataBind();

    }
}
