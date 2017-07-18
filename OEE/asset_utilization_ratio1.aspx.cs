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

public partial class OEE_asset_utilization_ratio1 : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ALCS_XLS"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];


    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_minus7 = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");

    ArrayList arlist_temp1 = new ArrayList();
    string s_dt = "";

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {


            txtEstimateSTARTTIME.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd"));
            s_dt = Convert.ToDateTime(txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyy/MM/dd")).AddDays(+1).ToString("yyyyMMdd");




            //#region hour
            //arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\hour.txt");





            //DropDownList3.DataSource = arlist_temp1;
            //DropDownList3.DataBind();
            //DropDownList3.Items.Insert(0, today_hour);



            //DropDownList5.DataSource = arlist_temp1;
            //DropDownList5.DataBind();
            //DropDownList5.Items.Insert(0, today_hour);

            //#endregion


            //#region min
            //arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\min.txt");

            //DropDownList4.DataSource = arlist_temp1;
            //DropDownList4.DataBind();
            //DropDownList4.Items.Insert(0, today_hour);


            //DropDownList6.DataSource = arlist_temp1;
            //DropDownList6.DataBind();
            //DropDownList6.Items.Insert(0, today_min);


            //#endregion
            sql_temp = @"

select 

       case when  substr(eq.modulename,0,2)='0A' then 'T0ARRAY'
            when  substr(eq.modulename,0,2)='1A' then 'T1ARRAY'
            when  substr(eq.modulename,0,2)='0C' then 'T0CELL'
            when  substr(eq.modulename,0,2)='1C' then 'T1CELL'
            when  substr(eq.modulename,0,2)='1F' then 'T1CF'
            when  substr(eq.modulename,0,2)='1W' then 'T1CELL'
            when  substr(eq.modulename,0,2)='0W' then 'T0CELL'
            else 'NA'
            end SHOP,
            
    
       idx.cutoffkey as shiftdate,
       eq.modulename as EQPID,
      -- ROUND(idx.ttm / 60, 2) as TTM,
       ROUND(idx.run/60,3)as run,
       ROUND(idx.mrun/60,3)as mrun,
       ROUND(idx.erun/60,3)as erun,
       ROUND(idx.e_set/60,3)as e_set,
       ROUND(idx.idle/60,3)as idle,
       ROUND(idx.amhs/60,3)as amhs,
       ROUND(idx.shutdown/60,3)as shutdown,
       ROUND(idx.p_set/60,3)as p_set,
       ROUND(idx.eng/60,3)as eng,
       ROUND(idx.pm/60,3)as pm,
       ROUND(idx.chmt/60,3)as chmt,
       ROUND(idx.ttldown/60,3)as ttldown,
       ROUND(idx.ttlwarmup/60,3)as ttlwarmup,
       ROUND(idx.SETUP/60,3)as SETUP,
       idx.dttm
       
      
  from mesidxsummdaily idx, equipment eq,asset_utilization_ratio aur
 where 
   --idx.line = eq.line
     idx.equipmentname = eq.modulename
   -- eq.line = 'T1ARRAY'
   --and eq.area = '1A-PHOTO'
   --and eq.modelname = '1APHT'
   --and eq.moduletype = 'MAIN'
   and idx.cutoffcycle = 'D'
   and idx.cutoffkey >='{0}' 
   and idx.cutoffkey<'{1}'
   and aur.eqpid= eq.modulename
  
 order by 1,2,3


";


            sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd"), s_dt);


            Bind_data(sql_temp, conn1);

        }


        s_dt = Convert.ToDateTime(txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyy/MM/dd")).AddDays(+1).ToString("yyyyMMdd");








    }

    public DataSet Bind_data(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);

        Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();

        GridView1.DataSource = ds_temp.Tables[0];


        GridView1.DataBind();



        return ds_temp;

    }

    protected void ButtonQuery_Click(object sender, EventArgs e)
    {

        sql_temp = @"


select 

       case when  substr(eq.modulename,0,2)='0A' then 'T0ARRAY'
            when  substr(eq.modulename,0,2)='1A' then 'T1ARRAY'
            when  substr(eq.modulename,0,2)='0C' then 'T0CELL'
            when  substr(eq.modulename,0,2)='1C' then 'T1CELL'
            when  substr(eq.modulename,0,2)='1F' then 'T1CF'
            when  substr(eq.modulename,0,2)='1W' then 'T1CELL'
            when  substr(eq.modulename,0,2)='0W' then 'T0CELL'
            else 'NA'
            end SHOP,
            
    
       idx.cutoffkey as shiftdate,
       eq.modulename as EQPID,
      -- ROUND(idx.ttm / 60, 2) as TTM,
       ROUND(idx.run/60,3)as run,
       ROUND(idx.mrun/60,3)as mrun,
       ROUND(idx.erun/60,3)as erun,
       ROUND(idx.e_set/60,3)as e_set,
       ROUND(idx.idle/60,3)as idle,
       ROUND(idx.amhs/60,3)as amhs,
       ROUND(idx.shutdown/60,3)as shutdown,
       ROUND(idx.p_set/60,3)as p_set,
       ROUND(idx.eng/60,3)as eng,
       ROUND(idx.pm/60,3)as pm,
       ROUND(idx.chmt/60,3)as chmt,
       ROUND(idx.ttldown/60,3)as ttldown,
       ROUND(idx.ttlwarmup/60,3)as ttlwarmup,
       ROUND(idx.SETUP/60,3)as SETUP,
       idx.dttm
       
      
  from mesidxsummdaily idx, equipment eq,asset_utilization_ratio aur
 where 
   --idx.line = eq.line
     idx.equipmentname = eq.modulename
   -- eq.line = 'T1ARRAY'
   --and eq.area = '1A-PHOTO'
   --and eq.modelname = '1APHT'
   --and eq.moduletype = 'MAIN'
   and idx.cutoffcycle = 'D'
   and idx.cutoffkey >='{0}' 
   and idx.cutoffkey<'{1}'
   and aur.eqpid= eq.modulename
  
 order by 1,2,3

";







        sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd"), s_dt);






        ds_temp = func.get_dataSet_access(sql_temp, conn1);


        Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();

        GridView1.DataSource = ds_temp.Tables[0];
        GridView1.DataBind();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {



        sql_temp = @"


select 

       case when  substr(eq.modulename,0,2)='0A' then 'T0ARRAY'
            when  substr(eq.modulename,0,2)='1A' then 'T1ARRAY'
            when  substr(eq.modulename,0,2)='0C' then 'T0CELL'
            when  substr(eq.modulename,0,2)='1C' then 'T1CELL'
            when  substr(eq.modulename,0,2)='1F' then 'T1CF'
            when  substr(eq.modulename,0,2)='1W' then 'T1CELL'
            when  substr(eq.modulename,0,2)='0W' then 'T0CELL'
            else 'NA'
            end SHOP,
            
    
       idx.cutoffkey as shiftdate,
       eq.modulename as EQPID,
      -- ROUND(idx.ttm / 60, 2) as TTM,
       ROUND(idx.run/60,3)as run,
       ROUND(idx.mrun/60,3)as mrun,
       ROUND(idx.erun/60,3)as erun,
       ROUND(idx.e_set/60,3)as e_set,
       ROUND(idx.idle/60,3)as idle,
       ROUND(idx.amhs/60,3)as amhs,
       ROUND(idx.shutdown/60,3)as shutdown,
       ROUND(idx.p_set/60,3)as p_set,
       ROUND(idx.eng/60,3)as eng,
       ROUND(idx.pm/60,3)as pm,
       ROUND(idx.chmt/60,3)as chmt,
       ROUND(idx.ttldown/60,3)as ttldown,
       ROUND(idx.ttlwarmup/60,3)as ttlwarmup,
       ROUND(idx.SETUP/60,3)as SETUP,
       idx.dttm
       
      
  from mesidxsummdaily idx, equipment eq,asset_utilization_ratio aur
 where 
   --idx.line = eq.line
     idx.equipmentname = eq.modulename
   -- eq.line = 'T1ARRAY'
   --and eq.area = '1A-PHOTO'
   --and eq.modelname = '1APHT'
   --and eq.moduletype = 'MAIN'
   and idx.cutoffcycle = 'D'
   and idx.cutoffkey >='{0}' 
   and idx.cutoffkey<'{1}'
   and aur.eqpid= eq.modulename
  
 order by 1,2,3

";



        sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd"), s_dt);




        //if (!TextBox1.Text.Equals(""))
        //{

        //    sql_temp = sql_temp + " and upper(ob1.user_mobil_tel) like '%" + TextBox1.Text.ToString().ToUpper() + "%' or upper(ob1.user_sms_num) like '%" + TextBox1.Text.ToString().ToUpper() + "%' or upper(ob1.user_e_mail) like '%" + TextBox1.Text.ToString().ToUpper() + "%' ";


        //}

        //if (!TextBox3.Text.Equals(""))
        //{

        //    sql_temp = sql_temp + " and upper(ob1.event_id) like '%" + TextBox3.Text.ToString().ToUpper() + "%' ";


        //}

        //if (!TextBox_Msg.Text.Equals(""))
        //{

        //    sql_temp = sql_temp + " and upper(ob1.alarm_text) like '%" + TextBox_Msg.Text.ToString().ToUpper() + "%'  or upper(ob1.alarm_comment) like '%" + TextBox_Msg.Text.ToString().ToUpper() + "%'";


        //}



        GridView gv = new GridView();

        ds_temp = func.get_dataSet_access(sql_temp, conn1);
        Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();
        gv.DataSource = ds_temp.Tables[0];
        gv.DataBind();
        ExportExcel(gv);

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //e.Row.Cells[13] = Convert.ToDouble(e.Row.Cells[14].Text).ToString("P1");
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
            //e.Row.Cells[14].Style.Add("background-color", "#FF9DFF");

            #region MyRegion

            //string run2_non = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "run2_non")).ToString("P2");

            //e.Row.Cells[14].Text = run2_non;

            //string UP2_NON = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "UP2_NON")).ToString("P2");

            //e.Row.Cells[15].Text = UP2_NON;

            //string OEE2_NON = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OEE2_NON")).ToString("P2");

            //e.Row.Cells[16].Text = OEE2_NON;

            //string RUN2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RUN2")).ToString("P2");

            //e.Row.Cells[17].Text = RUN2;

            //string UP2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "UP2")).ToString("P2");

            //e.Row.Cells[18].Text = UP2;


            //string OEE2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OEE2")).ToString("P2");

            //e.Row.Cells[19].Text = OEE2;

            #endregion



            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }
            //e.Row.Cells[i].Text = Convert.ToDouble(e.Row.Cells[i].Text).ToString("P1");
            // e.Row.Cells[13] = Convert.ToDouble(e.Row.Cells[14].Text).ToString("P1");
            //e.Row.Cells[14]= Convert.ToDouble(e.Row.Cells[14].Text).ToString("P1");
            // e.Row.Cells[15].Text = Convert.ToDouble(e.Row.Cells[15].Text).ToString("P1");
            //e.Row.Cells[16].Text = Convert.ToDouble(e.Row.Cells[16].Text).ToString("P1");
            //e.Row.Cells[17].Text = Convert.ToDouble(e.Row.Cells[17].Text).ToString("P1");
            //e.Row.Cells[18].Text = Convert.ToDouble(e.Row.Cells[18].Text).ToString("P1"); 
        }
    }




    private void ExportExcel(GridView SeriesValuesDataGrid)
    {

        string filename = "";
        string today_detail_char = DateTime.Now.AddDays(+0).ToString("yyyy/MM/ddHHmmss").Replace("/", "");
        filename = "T0T1OEE_asset_utilization_ratio" + today_detail_char + ".xls";
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
