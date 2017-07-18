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
using System.Text.RegularExpressions;
using System.IO;

public partial class VPN_vpn_parser : System.Web.UI.Page
{


    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_PARS1_OLE_ARSNEW"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_T1NEWALARM"];


    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today1 = DateTime.Now.AddDays(+0).ToString("yyyy-MM-dd");
    string yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
    string yesterday1 = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
    string today_minus7 = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
    string today_minus7_1 = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string Filename = "";

    ArrayList arlist_temp1 = new ArrayList();
    
    protected void Page_Load(object sender, EventArgs e)
    {


        //func.TextParser parser = new func.TextParser(txtTextToParse.Text);
 
        //lstResults.Items.Clear();
        //while (!parser.EndOfText)
        //{
        //    while (!parser.EndOfText && !Char.IsLetterOrDigit(parser.Peek()))
        //        parser.MoveAhead();

        //    int start = parser.Position;

        //    while (Char.IsLetterOrDigit(parser.Peek()))
        //        parser.MoveAhead();

        //    if (parser.Position > start)
        //        lstResults.Items.Add(parser.Extract(start, parser.Position));
        //}
        if (!IsPostBack)
        {
            txtCalendar1.Text = yesterday1;
            txtCalendar2.Text = today;
            //CopyFileFromVPN("*.txt", -30);
           Bind_data();

           Label3.Text = yesterday;


            if (Request.QueryString["LOADER"] == "START")
            {
                //CopyFileFromVPN("*.txt", -30, txtCalendar1.Text);
                parser_insert_data(txtCalendar1.Text);

                Response.Write("<script language=\"javascript\">setTimeout(\"window.opener=null; window.close();\",null)</script>");
                
            }
        }

        lblAIExpand.Text = DropDownList1.SelectedValue;
        
        

        //func.write_log("VPNLOG_PARSER",Server.MapPath("..\\")+"\\LOG\\","log");
        


    }


    public void CopyFileFromVPN(string file_type, Double dayAgo, string startTime)
    {


        Microsoft.VisualBasic.Devices.Network Network = new Microsoft.VisualBasic.Devices.Network();

        string fileName = "";
        fileName = startTime.ToString().Replace("/", "-") + "-VPNLOG.txt";
        //Filename = startTime.ToString().Replace("/", "-") + "-VPNLOG.txt";
        string sourcefile = @"\\172.16.12.138\vpnlog\" + fileName;
        string destinationfile = Server.MapPath(".") + "\\File\\" + fileName;
        try
        {
            //Response.Write("VPNLOG" + "_" + "innolux");
            Network.DownloadFile(sourcefile, destinationfile, "VPNLOG", "VPN@innolux", false, 500, true);


        }
        catch (Exception)
        {
            
            throw;
        }


        //string drive_id=func.get_netdrive_id();
        //func.start_process("net use z: \\172.16.12.138\vpnlog innolux /USER:172.16.12.138\vpnlog ");
        ////
        //DirectoryInfo dir = new DirectoryInfo("z:\\");
        //// FileInfo[] files = dir.GetFiles("*.xls"); 
        //FileInfo[] files = dir.GetFiles(file_type);




        //string fileName = "";
        //for (int i = 0; i <= files.Length - 1; i++)
        //{


        //    if (files[i].CreationTime > DateTime.Now.AddDays(dayAgo))
        //    {
        //        string sourceFile = System.IO.Path.Combine(@"z:\", files[i].Name);
        //        string destFile = System.IO.Path.Combine(Server.MapPath(".") + "\\File\\", files[i].Name);

        //        //files[i].CopyTo(Server.MapPath(".")+"\\File\\");

        //        //fileName = files[i].Name;
        //        //File.Copy(files[i].Name, Server.MapPath(".") + "\\File\\" + fileName, true);
        //        System.IO.File.Copy(sourceFile, destFile, true);

        //    }



        //}
           
        
          // func.start_process("net use use z:/delete ");
           
         
    }



    public void parser_insert_data(string startTime)
    {
        //CopyFileFromVPN("*.txt", -30, startTime);
        #region Loader Source File

        Filename = startTime.ToString().Replace("/","-") + "-VPNLOG.txt";

        //Filename = "2011-10-20-VPNLOG.txt";

        arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\File\\" + Filename);
        Literal1.Text = "";
        Int32 counter = 0;
        for (int i = 0; i <= arlist_temp1.Count - 1; i++)
        {

            if (arlist_temp1[i].ToString().IndexOf("Duration") > 0)
            {

                Literal1.Text += "【" + counter + "】" + arlist_temp1[i].ToString() + "<br>";

                counter++;


                sql_temp = @"insert into vpn_log
  (vpn_item, dttm,user_id,login_id, hour, min, sec,log_dttm)
values
  ('{0}', to_date('{1}','yyyy/MM/dd HH24:MI:SS'),'{2}','{3}','{4}','{5}','{6}','{7}')";

                
                string  [] aaaaa=arlist_temp1[i].ToString().Split(',', '=', ':');

                sql_temp = string.Format(sql_temp, arlist_temp1[i].ToString(), today_detail, "SYSTEM LOADER", aaaaa[9].ToString().Trim(), aaaaa[15].ToString().Trim(), aaaaa[16].ToString().Trim(), aaaaa[17].ToString().Trim(), arlist_temp1[i].ToString().Substring(0,20).Trim());

                func.get_sql_execute(sql_temp, conn);



            }

        }


        #endregion


        #region Transfer Duration time => totoal mins 

        sql_temp = @"select t.*,t.rowid from vpn_log  t ";

        ds_temp = func.get_dataSet_access(sql_temp, conn);

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
        {

            ds_temp.Tables[0].Rows[i]["HOUR"].ToString().Replace("h", "");
            ds_temp.Tables[0].Rows[i]["MIN"].ToString().Replace("m", "");
            ds_temp.Tables[0].Rows[i]["SEC"].ToString().Replace("s", "");

            Double total_hour = (Convert.ToDouble(ds_temp.Tables[0].Rows[i]["HOUR"].ToString().Replace("h", "")) * 60 * 60 + Convert.ToDouble(ds_temp.Tables[0].Rows[i]["MIN"].ToString().Replace("m", "")) * 60 + Convert.ToDouble(ds_temp.Tables[0].Rows[i]["SEC"].ToString().Replace("s", "")) * 1) / 3600;


            sql_temp3 = @"update vpn_log t
   set 
      
       total_hour = '{0}'
 where t.rowid='{1}'";


            sql_temp3 = string.Format(sql_temp3, total_hour.ToString(), ds_temp.Tables[0].Rows[i]["ROWID"].ToString());

            func.get_sql_execute(sql_temp3,conn);




            // [9]   user_id
            // [15]  hours
            // [16]  min
            // [17]  sec

        }
      

        #endregion

        func.write_log("VPNLOG_PARSER", Server.MapPath("..\\") + "\\LOG\\", "log");
        func.delete_log_file(Server.MapPath(".") + "\\File\\", "*.txt",-60);
      
    }

    //private ArrayList SplitSentences(string sSourceText)
    //{

    //    // create a local string variable

    //    // set to contain the string passed it

    //    string sTemp = sSourceText;



    //    // create the array list that will

    //    // be used to hold the sentences

    //    ArrayList al = new ArrayList();



    //    // split the sentences with a regular expression

    //    string[] splitSentences = Regex.Split(sTemp, @"(?<=['""A-Za-z0-9][\.\!\?])\s+(?=[A-Z])");



    //    // loop the sentences

    //    for (int i = 0; i < splitSentences.Length; i++)
    //    {

    //        // clean up the sentence one more time, trim it,

    //        // and add it to the array list

    //        string sSingleSentence = splitSentences[i].Replace(Environment.NewLine, string.Empty);

    //        al.Add(sSingleSentence.Trim());

    //    }



    //    // update the statistics displayed on the text

    //    // characters

    //    //lblCharCount.Text = "Character Count: " + GenerateCharacterCount(sTemp).ToString();

    //    // sentences

    //    //lblSentenceCount.Text = "Sentence Count: " + GenerateSentenceCount(splitSentences).ToString();

    //    // words

    //    //lblWordCount.Text = "Word Count: " + GenerateWordCount(al).ToString();



    //    // return the arraylist with

    //    // all sentences added

    //    return al;

    //}


    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        Literal1.Visible = false;
        sql_temp = @"

select t.login_id,substr(t.log_dttm,0,10) as log_dttm   from vpn_log t
where t.log_dttm>'{0}'
     and  t.log_dttm<'{1}'";
        if (!TextBox1.Text.Equals(""))
        {
            sql_temp += " and upper(login_id) like '%" + TextBox1.Text.ToString().ToUpper() + "%' ";
        
        
        }
     





        string.Format(sql_temp, txtCalendar1.Text.Replace("/", "-"), txtCalendar2.Text.Replace("/", "-"));
        sql_temp = string.Format(sql_temp, txtCalendar1.Text.Replace("/", "-"), txtCalendar2.Text.Replace("/", "-"));



        //sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList3.SelectedValue.ToString() + DropDownList4.SelectedValue.ToString(), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList5.SelectedValue.ToString() + DropDownList6.SelectedValue.ToString());




        GridView gv = new GridView();

        ds_temp = func.get_dataSet_access(sql_temp, conn);
        // Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();
        gv.DataSource = ds_temp.Tables[0];
        gv.DataBind();
        //GridView1.DataSource = ds_temp.Tables[0];
        //GridView1.DataBind();
        Label2.Text = ds_temp.Tables[0].Rows.Count.ToString();


        sql_temp3 =

         @"select * from (

select t.login_id,substr(t.log_dttm,0,10) as log_dttm,round(sum(t.total_hour),1) as total_hour   from vpn_log t
where t.log_dttm>'{0}'
     and  t.log_dttm<'{1}' ";

         if (!TextBox1.Text.Equals(""))
        {
            sql_temp3 += " and upper(login_id) like '%" + TextBox1.Text.ToString().ToUpper() + "%' ";
        
        
        }

        sql_temp3 += @"

group by  t.login_id,substr(t.log_dttm,0,10)

) 
order by log_dttm desc
";

        //sql_temp3 = string.Format(sql_temp, txtCalendar1.Text.Replace("/", "-"), txtCalendar2.Text.Replace("/", "-"));

        sql_temp3 = string.Format(sql_temp3, txtCalendar1.Text.Replace("/", "-"), txtCalendar2.Text.Replace("/", "-"));



        //sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList3.SelectedValue.ToString() + DropDownList4.SelectedValue.ToString(), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList5.SelectedValue.ToString() + DropDownList6.SelectedValue.ToString());






        ds_temp3 = func.get_dataSet_access(sql_temp3, conn);
        // Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();

        GridView1.DataSource = ds_temp3.Tables[0];
        GridView1.DataBind();
        Label1.Text = ds_temp3.Tables[0].Rows.Count.ToString();

    }
    protected void Button_LAODER_Click(object sender, EventArgs e)
    {

        parser_insert_data(txtCalendar1.Text);


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        sql_temp = @"select * from (

select t.login_id,substr(t.log_dttm,0,10) as log_dttm,round(sum(t.total_hour),1) as total_hour   from vpn_log t
where t.log_dttm>'{0}'
     and  t.log_dttm<'{1}'
 ";
        if (!TextBox1.Text.Equals(""))
        {
            sql_temp += "and upper(login_id) like '%" + TextBox1.Text.ToString().ToUpper() + "%' ";
        }

        sql_temp += @"
group by  t.login_id,substr(t.log_dttm,0,10)

) 
order by log_dttm desc
";



        //sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList3.SelectedValue.ToString() + DropDownList4.SelectedValue.ToString(), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList5.SelectedValue.ToString() + DropDownList6.SelectedValue.ToString());

      


        GridView gv = new GridView();

        sql_temp = string.Format(sql_temp, txtCalendar1.Text.Replace("/", "-"), txtCalendar2.Text.Replace("/", "-"));

        ds_temp = func.get_dataSet_access(sql_temp, conn);
        //Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();
        gv.DataSource = ds_temp.Tables[0];
        gv.DataBind();
        ExportExcel(gv); 

    }


    public void Bind_data()
    {
        Literal1.Visible = false;
        sql_temp = @"

select t.login_id,substr(t.log_dttm,0,10) as log_dttm   from vpn_log t
where t.log_dttm>'{0}'
     and  t.log_dttm<'{1}'
 ";    
      if(!TextBox1.Text.Equals(""))

      {
          sql_temp += " and upper(login_id) like '%" + TextBox1.Text.ToString().ToUpper() + "%'  ";
      
      }
       
       





        string.Format(sql_temp, txtCalendar1.Text.Replace("/", "-"), txtCalendar2.Text.Replace("/", "-"));
        sql_temp = string.Format(sql_temp, txtCalendar1.Text.Replace("/", "-"), txtCalendar2.Text.Replace("/", "-"));



        //sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList3.SelectedValue.ToString() + DropDownList4.SelectedValue.ToString(), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList5.SelectedValue.ToString() + DropDownList6.SelectedValue.ToString());




        GridView gv = new GridView();

        ds_temp = func.get_dataSet_access(sql_temp, conn);
        // Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();
        gv.DataSource = ds_temp.Tables[0];
        gv.DataBind();
        //GridView1.DataSource = ds_temp.Tables[0];
        //GridView1.DataBind();
        Label2.Text = ds_temp.Tables[0].Rows.Count.ToString();


        sql_temp3 =

         @"select * from (

select t.login_id,substr(t.log_dttm,0,10) as log_dttm,round(sum(t.total_hour),1) as total_hour   from vpn_log t
where t.log_dttm>'{0}'
     and  t.log_dttm<'{1}'

 ";
        if (!TextBox1.Text.Equals(""))
        {
            sql_temp3 += "and upper(login_id) like '%" + TextBox1.Text.ToString().ToUpper() + "%' ";
        }

        sql_temp3 += @"
group by  t.login_id,substr(t.log_dttm,0,10)

) 
order by log_dttm desc
";

        //sql_temp3 = string.Format(sql_temp, txtCalendar1.Text.Replace("/", "-"), txtCalendar2.Text.Replace("/", "-"));

        sql_temp3 = string.Format(sql_temp3, txtCalendar1.Text.Replace("/", "-"), txtCalendar2.Text.Replace("/", "-"));



        //sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList3.SelectedValue.ToString() + DropDownList4.SelectedValue.ToString(), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList5.SelectedValue.ToString() + DropDownList6.SelectedValue.ToString());






        ds_temp3 = func.get_dataSet_access(sql_temp3, conn);
        // Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();

        GridView1.DataSource = ds_temp3.Tables[0];
        GridView1.DataBind();
        Label1.Text = ds_temp3.Tables[0].Rows.Count.ToString();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
    }

    private void ExportExcel(GridView SeriesValuesDataGrid)
    {

        string filename = "";
        string today_detail_char = DateTime.Now.AddDays(+0).ToString("yyyy/MM/ddHHmmss").Replace("/", "");
        filename = "VpnHistory_" + today_detail_char + ".xls";
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

   
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {



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
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {



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
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            string temp_dttm = DataBinder.Eval(e.Row.DataItem, "log_dttm").ToString();
            string temp_login_id = DataBinder.Eval(e.Row.DataItem, "login_id").ToString();

            sql_temp2 = @"select t.vpn_item,t.dttm,t.user_id,t.login_id,t.hour,t.min,t.sec,round(t.total_hour,3) as total_hour,t.log_dttm from vpn_log t
where substr(log_dttm,0,10)='{0}' and upper(t.login_id) like '%{1}%' order by  log_dttm desc";


            sql_temp2 = string.Format(sql_temp2, temp_dttm, temp_login_id.ToString().ToUpper());
            ds_temp2 = func.get_dataSet_access(sql_temp2, conn);

            GridView2.DataSource = ds_temp2.Tables[0];
            GridView2.DataBind();



            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

            if (ds_temp2.Tables[0].Rows.Count == 0)
            {
                System.Web.UI.WebControls.Image btnShowDetail = new System.Web.UI.WebControls.Image();
                btnShowDetail = (System.Web.UI.WebControls.Image)e.Row.FindControl("btnShowDetail");
                btnShowDetail.Visible = false;
            }
            else
            {
                //********************************************************* 
                //新增一個新的GridViewRow 
                #region
                GridViewRow r = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                r.Cells.Add(new TableCell());
                r.Cells.Add(new TableCell());

                r.Cells[1].ColumnSpan = GridView1.Columns.Count - 1;

                GridView2.Visible = true;
                GridView2.RenderControl(hw);
                GridView2.Visible = false;

                r.Cells[1].Text = sw.ToString();
                sw.Close();

                r.ID = "Detail_" + e.Row.RowIndex.ToString();

                r.HorizontalAlign = HorizontalAlign.Left;
                e.Row.Parent.Controls.Add(r);

                System.Web.UI.WebControls.Image btnShowDetail = new System.Web.UI.WebControls.Image();
                btnShowDetail = (System.Web.UI.WebControls.Image)e.Row.FindControl("btnShowDetail");
                btnShowDetail.Attributes.Add("onclick", "showHideAnswer('GridView1_" + r.ID + "','" + e.Row.ClientID.ToString() + "_" + btnShowDetail.ID + "');");
                //btnShowDetail.Attributes.Add("onclick", "showHideAnswer('" + this.ClientID.ToString() + "_GridView1_" + r.ID + "','" + e.Row.ClientID.ToString() + "_" + btnShowDetail.ID + "');"); 

                if (lblAIExpand.Text == "Y")
                {
                    r.Style["display"] = "block";
                    btnShowDetail.ImageUrl = "~/images/close13.gif";
                }
                else
                {
                    r.Style["display"] = "none";
                    btnShowDetail.ImageUrl = "~/images/open13.gif";
                }


                #endregion
                //********************************************************* 
            } 

          

        }
    }
}
