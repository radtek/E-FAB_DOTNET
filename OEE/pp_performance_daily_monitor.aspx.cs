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
using Dundas.Charting.WebControl;
using System.Drawing;

public partial class OEE_pp_performance_daily_monitor : System.Web.UI.Page
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
    Int32 counter_total = 0;
    Int32 counter1_total = 0;
    Int32 counter_auto_ratio = 0;
    Int32 counter1_auto_ratio = 0;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {


            txtEstimateSTARTTIME.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd"));
            txtEstimateEndTime.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd"));




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

select '1A' as shop,
       ot5.cutoffkey as shiftdate,
       case when ot5.target >0 then ot5.target 
       else (select round(avg(t.up_p * t.uu_p) ,4)as target  from pp_north_south t
where t.eq like '1A%'
     ) end oee_target ,
       ot5.oee as oee_ACTUAL,
       ot5.Reaching_rate,
       ot5.IDLE_TIME,
       ot5.IDLE_RATE
  from (
        
        select ot4.cutoffkey,
                sum(ot4.target) target,
                sum(ot4.oee) oee,
                sum(ot4.Reaching_rate) Reaching_rate,
                sum(ot4.IDLE_TIME) IDLE_TIME,
                sum(ot4.IDLE_RATE) IDLE_RATE
          from (select ot3.cutoffkey,
                        round(avg(ot3.target), 4) target,
                        round(avg(ot3.oee), 4) oee,
                        round(avg(ot3.Reaching_rate), 4) Reaching_rate,
                        round(avg(ot3.IDLE_TIME), 4) IDLE_TIME,
                        round(avg(ot3.IDLE_RATE), 4) IDLE_RATE
                   from (
                         
                         select t.cutoffkey,
                                 t.equipmentid,
                                 ot2.target,
                                 ROUND(t.uptm / 100 * t.utilop / 100, 2) as OEE,
                                 round((ROUND(t.uptm / 100 * t.utilop / 100, 2) /
                                       ot2.target),
                                       2) as Reaching_rate,
                                 ROUND(t.sby / 60 / 60, 2) as IDLE_TIME,
                                 ROUND(t.sby / 60 / 60 / 24, 2) as IDLE_RATE
                           from empaidxsummdaily t,
                                 (select t.eq, t.up_p * t.uu_p as target
                                    from pp_north_south t
                                  
                                  ) ot2
                          where t.cutoffkey like substr('{0}',0,6)||'%'
                            and t.cutoffcycle = 'D'
                            and t.equipmentid like '1A'||'%'
                            and t.equipmentid = ot2.eq
                         
                         ) ot3
                 
                  group by ot3.cutoffkey
                 
                 union
                 
                 select tt.shift_date, 0, 0, 0, 0, 0
                   from innrpt.shift_date@oeemgr2ods tt
                  where tt.shift_date like substr('{0}',0,6)||'%'
                 --order by tt.shift_date 
                 ) ot4
        
         group by ot4.cutoffkey
        
         order by cutoffkey
        
        ) ot5
        
  union all      
select '1A','MTD',round(avg(ot7.target), 4) target,
       round(avg(ot7.oee), 4) oee,
       round(avg(ot7.Reaching_rate), 4) Reaching_rate,
       round(avg(ot7.IDLE_TIME), 4) IDLE_TIME,
       round(avg(ot7.IDLE_RATE), 4) IDLE_RATE
  from (select '1A' as shop,
               ot5.cutoffkey,
               case
                 when ot5.target > 0 then
                  ot5.target
                 else
                  (select round(avg(t.up_p * t.uu_p), 4) as target
                     from pp_north_south t
                    where t.eq like '1A'||'%')
               end target,
               ot5.oee,
               ot5.Reaching_rate,
               ot5.IDLE_TIME,
               ot5.IDLE_RATE
          from (
                
                select ot4.cutoffkey,
                        sum(ot4.target) target,
                        sum(ot4.oee) oee,
                        sum(ot4.Reaching_rate) Reaching_rate,
                        sum(ot4.IDLE_TIME) IDLE_TIME,
                        sum(ot4.IDLE_RATE) IDLE_RATE
                  from (select ot3.cutoffkey,
                                round(avg(ot3.target), 4) target,
                                round(avg(ot3.oee), 4) oee,
                                round(avg(ot3.Reaching_rate), 4) Reaching_rate,
                                round(avg(ot3.IDLE_TIME), 4) IDLE_TIME,
                                round(avg(ot3.IDLE_RATE), 4) IDLE_RATE
                           from (
                                 
                                 select t.cutoffkey,
                                         t.equipmentid,
                                         ot2.target,
                                         ROUND(t.uptm / 100 * t.utilop / 100, 2) as OEE,
                                         round((ROUND(t.uptm / 100 * t.utilop / 100,
                                                      2) / ot2.target),
                                               2) as Reaching_rate,
                                         ROUND(t.sby / 60 / 60, 2) as IDLE_TIME,
                                         ROUND(t.sby / 60 / 60 / 24, 2) as IDLE_RATE
                                   from empaidxsummdaily t,
                                         (select t.eq, t.up_p * t.uu_p as target
                                            from pp_north_south t
                                          
                                          ) ot2
                                  where t.cutoffkey like substr('{0}',0,6)||'%'
                                    and t.cutoffcycle = 'D'
                                    and t.equipmentid like '1A'||'%'
                                    and t.equipmentid = ot2.eq
                                 
                                 ) ot3
                         
                          group by ot3.cutoffkey
                         
                         ) ot4
                
                 group by ot4.cutoffkey
                
                 order by cutoffkey
                
                ) ot5) ot7



";


            sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd"));


            Bind_data(sql_temp, conn1);

        }











    }

    public DataSet Bind_data(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);

        Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();

        DataTable dt_performance = new DataTable();

        DataTable dt_dhchart = new DataTable();

        dt_dhchart = ds_temp.Tables[0];

        dt_performance = func.Table_transport1(ds_temp.Tables[0]);

        dt_performance.Rows[0].Delete();

        for (int i = 0; i <= dt_performance.Columns.Count - 1; i++)
        {
            dt_performance.Columns[i].ColumnName = i.ToString();

            //dt_performance.Columns[i].ColumnName = "AAA";
        }

       

        GridView1.DataSource = dt_performance;


        GridView1.DataBind();

        doChart(dt_dhchart);
        doChart2(dt_dhchart);
   
        return ds_temp;

    }

    protected void ButtonQuery_Click(object sender, EventArgs e)
    {

        sql_temp = @"


select '1A' as shop,
       ot5.cutoffkey as shiftdate,
       case when ot5.target >0 then ot5.target 
       else (select round(avg(t.up_p * t.uu_p) ,4)as target  from pp_north_south t
where t.eq like '1A%'
     ) end oee_target ,
       ot5.oee as oee_ACTUAL,
       ot5.Reaching_rate,
       ot5.IDLE_TIME,
       ot5.IDLE_RATE
  from (
        
        select ot4.cutoffkey,
                sum(ot4.target) target,
                sum(ot4.oee) oee,
                sum(ot4.Reaching_rate) Reaching_rate,
                sum(ot4.IDLE_TIME) IDLE_TIME,
                sum(ot4.IDLE_RATE) IDLE_RATE
          from (select ot3.cutoffkey,
                        round(avg(ot3.target), 4) target,
                        round(avg(ot3.oee), 4) oee,
                        round(avg(ot3.Reaching_rate), 4) Reaching_rate,
                        round(avg(ot3.IDLE_TIME), 4) IDLE_TIME,
                        round(avg(ot3.IDLE_RATE), 4) IDLE_RATE
                   from (
                         
                         select t.cutoffkey,
                                 t.equipmentid,
                                 ot2.target,
                                 ROUND(t.uptm / 100 * t.utilop / 100, 2) as OEE,
                                 round((ROUND(t.uptm / 100 * t.utilop / 100, 2) /
                                       ot2.target),
                                       2) as Reaching_rate,
                                 ROUND(t.sby / 60 / 60, 2) as IDLE_TIME,
                                 ROUND(t.sby / 60 / 60 / 24, 2) as IDLE_RATE
                           from empaidxsummdaily t,
                                 (select t.eq, t.up_p * t.uu_p as target
                                    from pp_north_south t
                                  
                                  ) ot2
                          where t.cutoffkey like substr('{0}',0,6)||'%'
                            and t.cutoffcycle = 'D'
                            and t.equipmentid like '1A'||'%'
                            and t.equipmentid = ot2.eq
                         
                         ) ot3
                 
                  group by ot3.cutoffkey
                 
                 union
                 
                 select tt.shift_date, 0, 0, 0, 0, 0
                   from innrpt.shift_date@oeemgr2ods tt
                  where tt.shift_date like substr('{0}',0,6)||'%'
                 --order by tt.shift_date 
                 ) ot4
        
         group by ot4.cutoffkey
        
         order by cutoffkey
        
        ) ot5
        
  union all      
select '1A','MTD',round(avg(ot7.target), 4) target,
       round(avg(ot7.oee), 4) oee,
       round(avg(ot7.Reaching_rate), 4) Reaching_rate,
       round(avg(ot7.IDLE_TIME), 4) IDLE_TIME,
       round(avg(ot7.IDLE_RATE), 4) IDLE_RATE
  from (select '1A' as shop,
               ot5.cutoffkey,
               case
                 when ot5.target > 0 then
                  ot5.target
                 else
                  (select round(avg(t.up_p * t.uu_p), 4) as target
                     from pp_north_south t
                    where t.eq like '1A'||'%')
               end target,
               ot5.oee,
               ot5.Reaching_rate,
               ot5.IDLE_TIME,
               ot5.IDLE_RATE
          from (
                
                select ot4.cutoffkey,
                        sum(ot4.target) target,
                        sum(ot4.oee) oee,
                        sum(ot4.Reaching_rate) Reaching_rate,
                        sum(ot4.IDLE_TIME) IDLE_TIME,
                        sum(ot4.IDLE_RATE) IDLE_RATE
                  from (select ot3.cutoffkey,
                                round(avg(ot3.target), 4) target,
                                round(avg(ot3.oee), 4) oee,
                                round(avg(ot3.Reaching_rate), 4) Reaching_rate,
                                round(avg(ot3.IDLE_TIME), 4) IDLE_TIME,
                                round(avg(ot3.IDLE_RATE), 4) IDLE_RATE
                           from (
                                 
                                 select t.cutoffkey,
                                         t.equipmentid,
                                         ot2.target,
                                         ROUND(t.uptm / 100 * t.utilop / 100, 2) as OEE,
                                         round((ROUND(t.uptm / 100 * t.utilop / 100,
                                                      2) / ot2.target),
                                               2) as Reaching_rate,
                                         ROUND(t.sby / 60 / 60, 2) as IDLE_TIME,
                                         ROUND(t.sby / 60 / 60 / 24, 2) as IDLE_RATE
                                   from empaidxsummdaily t,
                                         (select t.eq, t.up_p * t.uu_p as target
                                            from pp_north_south t
                                          
                                          ) ot2
                                  where t.cutoffkey like substr('{0}',0,6)||'%'
                                    and t.cutoffcycle = 'D'
                                    and t.equipmentid like '1A'||'%'
                                    and t.equipmentid = ot2.eq
                                 
                                 ) ot3
                         
                          group by ot3.cutoffkey
                         
                         ) ot4
                
                 group by ot4.cutoffkey
                
                 order by cutoffkey
                
                ) ot5) ot7



";







        sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd"));


        



        ds_temp = func.get_dataSet_access(sql_temp, conn1);


        Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();


        DataTable dt_performance11= new DataTable();

        dt_performance11 = func.Table_transport1(ds_temp.Tables[0]);

        dt_performance11.Rows[0].Delete();


        DataTable dt_performance = new DataTable();

        DataTable dt_dhchart = new DataTable();

        dt_dhchart = ds_temp.Tables[0];

        dt_performance = func.Table_transport1(ds_temp.Tables[0]);

        dt_performance.Rows[0].Delete();

        for (int i = 0; i <= dt_performance.Columns.Count - 1; i++)
        {
            dt_performance.Columns[i].ColumnName = i.ToString();

            //dt_performance.Columns[i].ColumnName = "AAA";
        }








        GridView1.DataSource = dt_performance11;
        GridView1.DataBind();

        doChart(dt_dhchart);
        doChart2(dt_dhchart);

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
       ROUND(idx.prd / 60/60, 3) as RUN,
       ROUND(idx.sby / 60/60, 3) as IDLE,
       ROUND(idx.ENG /60/60, 3) as ENG,
       ROUND(idx.setup / 60/60, 3) as SETUP,
       ROUND(idx.pm / 60/60, 3) as PM,
       ROUND(idx.pmmqc / 60/60, 3) as PM_MQC,
       ROUND(idx.eqd / 60/60, 3) as EQ_D,
       ROUND(idx.alm /60/60, 3) as ALARM,
       ROUND(idx.dmqc /60/60, 3) as D_MQC,
       ROUND(idx.nst / 60/60, 3) as off,
       '0' as ERUN,
       '0' as MRUN,
       '0' as P_SET,
       '0' as E_SET,
       to_char(sysdate,'yyyyMMddHH24MISS') as dttm
  from empaidxsummdaily idx, equipment eq,asset_utilization_ratio aur
 where 
   --idx.line = eq.line
     idx.equipmentid = eq.modulename
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



        sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd"));




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

        if (e.Row.RowType == DataControlRowType.Header)  //在Bound事件時，判
        {

            //取得該GridView的表頭                                       

            TableCellCollection tcHeader = e.Row.Cells;

            //tcHeader.Count;

            //清除先前設定的表頭                                         

            tcHeader.Clear();
            //tcHeader.Count;


            ////新增第一層表頭                                             

            //tcHeader.Add(new TableHeaderCell());

            ////該GridView有九個Column   
            
               

            ////tcHeader[0].Attributes.Add("colspan", e.Row.Cells.Count.ToString());

            ////該表頭所要顯示的內容                                       

            //// 功用是用來第一個表頭的結尾  做為下一行的開始              

            ////若未在表頭內容加上就會看到下一層表頭"資料"出現在"全部資訊"?

            //for (int i = 0; i <= e.Row..Count-1; i++)
            //{

            //    tcHeader[i].Text = "RN"+i.ToString();

            //}
          



            ////下一層的表頭                                               

            //tcHeader.Add(new TableHeaderCell());

            //tcHeader[1].Attributes.Add("colspan", "5");

            ////設定背景顏色                                               

            //tcHeader[1].Attributes.Add("bgcolor", "#006699");

            //tcHeader[1].Text = "資料";



            //tcHeader.Add(new TableHeaderCell());

            //tcHeader[2].Attributes.Add("colspan", "4");

            //tcHeader[2].Attributes.Add("bgcolor", "#006699");

            //tcHeader[2].Text = "功能";



            //tcHeader.Add(new TableHeaderCell());

            //tcHeader[3].Attributes.Add("bgcolor", "#006611");

            //tcHeader[3].Text = "主機名稱";



            //tcHeader.Add(new TableHeaderCell());

            //tcHeader[4].Attributes.Add("bgcolor", "#006611");

            //tcHeader[4].Text = "主機識別碼";



            //tcHeader.Add(new TableHeaderCell());

            //tcHeader[5].Attributes.Add("bgcolor", "#006611");

            //tcHeader[5].Text = "主機狀態";



            //tcHeader.Add(new TableHeaderCell());

            //tcHeader[6].Attributes.Add("bgcolor", "#006611");

            //tcHeader[6].Text = "更新人員";



            //tcHeader.Add(new TableHeaderCell());

            //tcHeader[7].Attributes.Add("bgcolor", "#006611");

            //tcHeader[7].Text = "更新時間";



            //tcHeader.Add(new TableHeaderCell());

            //tcHeader[8].Attributes.Add("bgcolor", "#006611");

            //tcHeader[8].Text = "編輯";



            //tcHeader.Add(new TableHeaderCell());

            //tcHeader[9].Attributes.Add("bgcolor", "#006611");

            //tcHeader[9].Text = "刪除";



            //tcHeader.Add(new TableHeaderCell());

            //tcHeader[10].Attributes.Add("colspan", "2");

            //tcHeader[10].Attributes.Add("bgcolor", "#006611");

            //tcHeader[10].Text = "選取";

        }                                                                


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

            //string shiftdate_C = DataBinder.Eval(e.Row.DataItem, "SHIFTDATE").ToString();

            for (int i = 0; i <= e.Row.Cells.Count-1; i++)
            {
                if (e.Row.Cells[0].Text.Equals("SHIFTDATE"))
                {

                    e.Row.Cells[i].Style.Add("background-color", "#999999");

                }
           

            }
           

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

            //string run2_non = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SHIFTDATE")).ToString("P2");

            //e.Row.Cells[14].Text = run2_non;

            //string UP2_NON = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "UP2_NON")).ToString("P2");

            //e.Row.Cells[15].Text = UP2_NON;

            //string OEE2_NON = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OEE2_NON")).ToString("P2");

            //e.Row.Cells[16].Text = OEE2_NON;

            //string RUN2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RU3388N2")).ToString("P2");

            //e.Row.Cells[17].Text = RUN2;

            //string UP2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "UP2")).ToString("P2");

            //e.Row.Cells[18].Text = UP2;


            //string OEE2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OEE2")).ToString("P2");

            //e.Row.Cells[19].Text = OEE2;








            #endregion



            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            //if (e.Row.RowIndex != -1)
            //{
            //    int RN = e.Row.RowIndex + 1;
            //    e.Row.Cells[0].Text = RN.ToString();
            //}
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
        filename = "T0T1OEE_pp_performance_daily_monitor_" + today_detail_char + ".xls";
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



    private void doChart( DataTable dt)
    {
        //clear all point
        foreach (Series seriesx in Chart1.Series)
        {
            seriesx.Points.Clear();
        }



        //Chart1.Series.Remove(Chart1.Series["total"]);
        //Chart1.Series.Remove(Chart1.Series["auto_ratio"]);




        // Create new data series and set it's visual attributes
        Series series = new Series("Line");
        //Series series = new Series("Spline");
        series.Type = SeriesChartType.Spline;
        series.BorderWidth = 3;
        series.ShadowOffset = 2;
        series.Name = "OEE_TARGET";
        series.ShowLabelAsValue = true;
        series.LabelBackColor = Color.Ivory;


        // Add series into the chart's series collection


        for (int i = 0; i <= Chart1.Series.Count - 1; i++)
        {
            if (Chart1.Series[i].Name == "OEE_TARGET")
                //Chart1.Series[i].Name = "total";
                counter_total++;
        }

        if (counter_total <= 0)
        {

            Chart1.Series.Add(series);
        }



        Series series1 = new Series("Line");
        series1.Type = SeriesChartType.Spline;
        series1.BorderWidth = 3;
        series1.ShadowOffset = 2;
        series1.Name = "OEE_ACTUAL";
        series1.ShowLabelAsValue = true;
        series1.LabelBackColor = Color.Gold;




        // Add series into the chart's series collection

        for (int i = 0; i <= Chart1.Series.Count - 1; i++)
        {
            if (Chart1.Series[i].Name == "OEE_ACTUAL")
                counter_auto_ratio++;
        }


        if (counter_auto_ratio <= 0)
        {

            Chart1.Series.Add(series1);
        }

        // Show AxisX labels every value

        Chart1.ChartAreas["Default"].AxisX.LabelStyle.Interval = 1;
        Chart1.ChartAreas["Default"].AxisX.MajorGrid.Interval = 1;
        Chart1.ChartAreas["Default"].AxisX.MajorTickMark.Interval = 1;


        Series series2 = new Series("Line");
        series2.Type = SeriesChartType.Spline;
        series2.BorderWidth = 3;
        series2.ShadowOffset = 2;
        series2.Name = "REACHING_RATE";
        series2.ShowLabelAsValue = true;
        series2.LabelBackColor = Color.SkyBlue;
        series2.Color = Color.Pink;

        for (int i = 0; i <= Chart1.Series.Count - 1; i++)
        {
            if (Chart1.Series[i].Name == "REACHING_RATE")
                counter_auto_ratio++;
        }


        if (counter_auto_ratio <= 0)
        {

            Chart1.Series.Add(series2);
        }


        DataSet ds22 = new DataSet();
        DataSet ds33 = new DataSet();

       // string[] Meeting_type = { "其他", "宣導事項", "工程實驗", "改善對策", "機台保養", "決議事項", "重大決議", "生產狀況", "生產相關", "異常報告", "良率相關", "長期事項" };


        string sql = "";


        //string sql_product;

        
            // Chart1.Series.Add

            //start_date = txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "").Substring(0, 6) + "01";
            //end_date = txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "").Substring(0, 6) + "31";


        //ds = dbutil.GetDataset(sql_grade);


        DataView dv_times = dt.DefaultView;

        dv_times.RowFilter = " ";

        //dv_FAB.Sort = "";
        //DataTable dt_meeting_type;
        //將重複的 Table 欄位"event_type"   data distinct出來

        //dt_meeting_type = dv_times.ToTable(true, "event_type");
        //dv_FAB.Sort = "";

        //DataView TypeView = dt_meeting_type.DefaultView;

        //Chart1.Series["total"].Type = SeriesChartType.Line;
        Chart1.Series["OEE_TARGET"].YAxisType = AxisType.Secondary;
        Chart1.ChartAreas["Default"].AxisY2.LabelStyle.Format = "P0";
        Chart1.ChartAreas["Default"].AxisY2.Title = "OEE_TARGET(%)";
        Chart1.ChartAreas["Default"].AxisY2.TitleFont = new Font("Times New Roman", 12, FontStyle.Bold);

        Chart1.ChartAreas["Default"].AxisY2.TitleColor = Color.Red;

        Chart1.ChartAreas["Default"].AxisX.LabelsAutoFit = false;



        for (int x = 0; x <= dv_times.Count - 1; x++)
        {

            Chart1.Series["OEE_TARGET"].Points.AddXY(dv_times[x]["shiftdate"], Convert.ToDouble(dv_times[x]["OEE_TARGET"]));
            Chart1.Series["OEE_ACTUAL"].Points.AddXY(dv_times[x]["shiftdate"], Convert.ToDouble(dv_times[x]["OEE_ACTUAL"]));
            Chart1.Series["REACHING_RATE"].Points.AddXY(dv_times[x]["shiftdate"], Convert.ToDouble(dv_times[x]["REACHING_RATE"]));
         
            //for (int x1 = 0; x1 < Meeting_type.Length - 1; x1++)
            //{
            //    dv_times.RowFilter = "event_type='" + Meeting_type[x] + "'";

            //    if (dv_times[x]["TIMES"].ToString() == "")
            //    {
            //        Chart1.Series[Meeting_type[x]].Points.AddXY(dv_times[x]["short_date"], double.NaN);

            //    }

            //    Chart1.Series[Meeting_type[x]].Points.AddXY(dv_times[x]["short_date"], Convert.ToDouble(dv_times[x]["TIMES"]));
            //}
        }


        //for (int x = Convert.ToInt32(TextBox1.Text); x >= 0; x--)
        //{


        //    string date_key = DateTime.Now.AddDays(-x).ToString("yyyy/MM/dd");

        //    dv_type.RowFilter = "date1='" + date_key + "'";


        //    if (TypeView.Count > 0)
        //    {



        //        for (int j = 0; j < TypeView.Count; j++)
        //        {
        //            string typeCode = TypeView[j]["type"].ToString();
        //            dv_type.RowFilter = "date1='" + date_key + "' and type='" + typeCode + "'";

        //            if (dv_type.Count > 0)
        //            {
        //                if (typeCode != "")
        //                {
        //                    Chart1.Series[typeCode].Points.AddXY(date_key, Convert.ToDouble(dv_type[0]["due_time"]));
        //                }



        //            }
        //            else
        //            {
        //                if (typeCode != "")
        //                {

        //                    Chart1.Series[typeCode].Points.AddXY(date_key, Double.NaN);

        //                }



        //            }
        //        }
        //    }

        //    else
        //    {

        //        for (int j = 0; j < TypeView.Count; j++)
        //        {
        //            string typeCode = TypeView[j]["type"].ToString();

        //            if (typeCode != "")
        //            {

        //                Chart1.Series[typeCode].Points.AddXY(date_key, Double.NaN);

        //            }


        //        }
        //    }
        //}



    }

    private void doChart2(DataTable dt)
    {
        //clear all point
        foreach (Series seriesx in Chart2.Series)
        {
            seriesx.Points.Clear();
        }



        //Chart1.Series.Remove(Chart1.Series["total"]);
        //Chart1.Series.Remove(Chart1.Series["auto_ratio"]);




        // Create new data series and set it's visual attributes
        Series series = new Series("Line");
        //Series series = new Series("Spline");
        series.Type = SeriesChartType.Spline;
        series.BorderWidth = 3;
        series.ShadowOffset = 2;
        series.Name = "IDLE_TIME";
        series.ShowLabelAsValue = true;
        series.LabelBackColor = Color.Ivory;


        // Add series into the chart's series collection


        for (int i = 0; i <= Chart2.Series.Count - 1; i++)
        {
            if (Chart2.Series[i].Name == "IDLE_TIME")
                //Chart1.Series[i].Name = "total";
                counter1_total++;
        }

        if (counter1_total <= 0)
        {

            Chart2.Series.Add(series);
        }



        Series series1 = new Series("Line");
        series1.Type = SeriesChartType.Spline;
        series1.BorderWidth = 3;
        series1.ShadowOffset = 2;
        series1.Name = "IDLE_RATE";
        series1.ShowLabelAsValue = true;
        series1.LabelBackColor = Color.Gold;




        // Add series into the chart's series collection

        for (int i = 0; i <= Chart2.Series.Count - 1; i++)
        {
            if (Chart2.Series[i].Name == "IDLE_RATE")
                counter1_auto_ratio++;
        }


        if (counter1_auto_ratio <= 0)
        {

            Chart2.Series.Add(series1);
        }

        // Show AxisX labels every value

        Chart2.ChartAreas["Default"].AxisX.LabelStyle.Interval = 1;
        Chart2.ChartAreas["Default"].AxisX.MajorGrid.Interval = 1;
        Chart2.ChartAreas["Default"].AxisX.MajorTickMark.Interval = 1;


        

        DataSet ds22 = new DataSet();
        DataSet ds33 = new DataSet();

        // string[] Meeting_type = { "其他", "宣導事項", "工程實驗", "改善對策", "機台保養", "決議事項", "重大決議", "生產狀況", "生產相關", "異常報告", "良率相關", "長期事項" };


        string sql = "";


        //string sql_product;


        // Chart1.Series.Add

        //start_date = txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "").Substring(0, 6) + "01";
        //end_date = txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "").Substring(0, 6) + "31";


        //ds = dbutil.GetDataset(sql_grade);


        DataView dv_times = dt.DefaultView;

        dv_times.RowFilter = " ";

        //dv_FAB.Sort = "";
        //DataTable dt_meeting_type;
        //將重複的 Table 欄位"event_type"   data distinct出來

        //dt_meeting_type = dv_times.ToTable(true, "event_type");
        //dv_FAB.Sort = "";

        //DataView TypeView = dt_meeting_type.DefaultView;

        //Chart1.Series["total"].Type = SeriesChartType.Line;
        Chart2.Series["IDLE_TIME"].YAxisType = AxisType.Secondary;
        Chart2.ChartAreas["Default"].AxisY2.LabelStyle.Format = "P0";
        Chart2.ChartAreas["Default"].AxisY2.Title = "IDLE_TIME";
        Chart2.ChartAreas["Default"].AxisY2.TitleFont = new Font("Times New Roman", 12, FontStyle.Bold);

        Chart2.ChartAreas["Default"].AxisY2.TitleColor = Color.Red;

        Chart2.ChartAreas["Default"].AxisX.LabelsAutoFit = false;



        for (int x = 0; x <= dv_times.Count - 1; x++)
        {

            Chart2.Series["IDLE_TIME"].Points.AddXY(dv_times[x]["shiftdate"], Convert.ToDouble(dv_times[x]["IDLE_TIME"]));
            Chart2.Series["IDLE_RATE"].Points.AddXY(dv_times[x]["shiftdate"], Convert.ToDouble(dv_times[x]["IDLE_RATE"]));
            

            //for (int x1 = 0; x1 < Meeting_type.Length - 1; x1++)
            //{
            //    dv_times.RowFilter = "event_type='" + Meeting_type[x] + "'";

            //    if (dv_times[x]["TIMES"].ToString() == "")
            //    {
            //        Chart1.Series[Meeting_type[x]].Points.AddXY(dv_times[x]["short_date"], double.NaN);

            //    }

            //    Chart1.Series[Meeting_type[x]].Points.AddXY(dv_times[x]["short_date"], Convert.ToDouble(dv_times[x]["TIMES"]));
            //}
        }


        //for (int x = Convert.ToInt32(TextBox1.Text); x >= 0; x--)
        //{


        //    string date_key = DateTime.Now.AddDays(-x).ToString("yyyy/MM/dd");

        //    dv_type.RowFilter = "date1='" + date_key + "'";


        //    if (TypeView.Count > 0)
        //    {



        //        for (int j = 0; j < TypeView.Count; j++)
        //        {
        //            string typeCode = TypeView[j]["type"].ToString();
        //            dv_type.RowFilter = "date1='" + date_key + "' and type='" + typeCode + "'";

        //            if (dv_type.Count > 0)
        //            {
        //                if (typeCode != "")
        //                {
        //                    Chart1.Series[typeCode].Points.AddXY(date_key, Convert.ToDouble(dv_type[0]["due_time"]));
        //                }



        //            }
        //            else
        //            {
        //                if (typeCode != "")
        //                {

        //                    Chart1.Series[typeCode].Points.AddXY(date_key, Double.NaN);

        //                }



        //            }
        //        }
        //    }

        //    else
        //    {

        //        for (int j = 0; j < TypeView.Count; j++)
        //        {
        //            string typeCode = TypeView[j]["type"].ToString();

        //            if (typeCode != "")
        //            {

        //                Chart1.Series[typeCode].Points.AddXY(date_key, Double.NaN);

        //            }


        //        }
        //    }
        //}



    }
}
