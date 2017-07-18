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

public partial class OEE_OEE_INDEX_SUMM_PP : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_OLE"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_OLE"];


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


    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {


            txtEstimateSTARTTIME.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd"));
           




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
            sql_temp = @"select * from (

select  
eq.modulename as EQPID,
       (select to_char(sysdate-7,'yyyyWW') from dual   ) as dateweek,
       'Act.' as  Act,
       --ROUND(idx.TTM / 60, 2) as TTM,
       ROUND(idx.prd / 60, 2) as RUN,
       '0' as E_RUN,
       '0' as M_RUN,
       ROUND(idx.setup / 60, 2) as P_SETUP,
       '0' as E_SETUP,
       ROUND(idx.sby / 60, 2) as IDLE,
       --ROUND(idx.ENG / 60, 2) as ENG,
     
     /*ROUND(idx.pm / 60, 2) as PM,
     ROUND(idx.pmmqc / 60, 2) as PM_MQC,
     ROUND(idx.eqd / 60, 2) as EQ_D,
     ROUND(idx.alm / 60, 2) as ALARM,
     ROUND(idx.dmqc / 60, 2) as D_MQC,*/
       '0' as AMHS_IDLE,
       ROUND(idx.nst / 60, 2) as off
  from empaidxsummweekly idx, equipment eq
 where idx.line = eq.line
   and idx.equipmentid = eq.modulename
   and eq.line = 'T1ARRAY'
   and eq.area = '1A-PHOTO'
   and eq.moduletype = 'MAIN'
   and eq.equipmentid like '1APHT%'
   and idx.cutoffcycle = 'W'
   and idx.cutoffkey = (select to_char(sysdate-7,'yyyyWW') from dual   )
 --order by EQPID
 
union all

 select 

 distinct(tt.equipmentid),
  
    (select to_char(sysdate-7,'yyyyWW') from dual   ) as dateweek,
  'Tgt.' as ACT,
  0 as RUN,
  '0' as E_RUN,
  '0' as M_RUN,
  0 as P_SETUP,
  '0' as E_SETUP,
  0 as IDLE,
  '0' as AMHS_IDLE,
  0 as OFF
  
  from equipment tt
 where tt.equipmentid like '1APH%'

)t1

order by t1.eqpid ,t1.act
";


            //sql_temp = string.Format(sql_temp, '');


            Bind_data(sql_temp, conn);

        }











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

        sql_temp = @"select * from (

select  
eq.modulename as EQPID,
       (select to_char(sysdate-7,'yyyyWW') from dual   ) as dateweek,
       'Act.' as  Act,
       --ROUND(idx.TTM / 60, 2) as TTM,
       ROUND(idx.prd / 60, 2) as RUN,
       '0' as E_RUN,
       '0' as M_RUN,
       ROUND(idx.setup / 60, 2) as P_SETUP,
       '0' as E_SETUP,
       ROUND(idx.sby / 60, 2) as IDLE,
       --ROUND(idx.ENG / 60, 2) as ENG,
     
     /*ROUND(idx.pm / 60, 2) as PM,
     ROUND(idx.pmmqc / 60, 2) as PM_MQC,
     ROUND(idx.eqd / 60, 2) as EQ_D,
     ROUND(idx.alm / 60, 2) as ALARM,
     ROUND(idx.dmqc / 60, 2) as D_MQC,*/
       '0' as AMHS_IDLE,
       ROUND(idx.nst / 60, 2) as off
  from empaidxsummweekly idx, equipment eq
 where idx.line = eq.line
   and idx.equipmentid = eq.modulename
   and eq.line = 'T1ARRAY'
   and eq.area = '1A-PHOTO'
   and eq.moduletype = 'MAIN'
   and eq.equipmentid like '1APHT%'
   and idx.cutoffcycle = 'W'
   and idx.cutoffkey = (select to_char(sysdate-7,'yyyyWW') from dual   )
 --order by EQPID
 
union all

 select 

 distinct(tt.equipmentid),
  
    (select to_char(sysdate-7,'yyyyWW') from dual   ) as dateweek,
  'Tgt.' as ACT,
  0 as RUN,
  '0' as E_RUN,
  '0' as M_RUN,
  0 as P_SETUP,
  '0' as E_SETUP,
  0 as IDLE,
  '0' as AMHS_IDLE,
  0 as OFF
  
  from equipment tt
 where tt.equipmentid like '1APH%'

)t1

order by t1.eqpid ,t1.act

";



        //sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList3.SelectedValue.ToString() + DropDownList4.SelectedValue.ToString(), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList5.SelectedValue.ToString() + DropDownList6.SelectedValue.ToString());




       


        ds_temp = func.get_dataSet_access(sql_temp, conn1);


        Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();

        GridView1.DataSource = ds_temp.Tables[0];
        GridView1.DataBind();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        sql_temp = @"select tt.*,
       case when tt.action='disable' then round((sysdate - to_date(substr(tt.dttm, 0, 12), 'yyyyMMddHH24MISS')),
             2) 
            else round((sysdate - to_date(substr(tt.dttm, 0, 12), 'yyyyMMddHH24MISS')),
             2) 
             
            end as closed_Date
  from his_alarm_en_disable tt
 where tt.dttm>'{0}' and tt.dttm<='{1}'

";




        //sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList3.SelectedValue.ToString() + DropDownList4.SelectedValue.ToString(), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd") + DropDownList5.SelectedValue.ToString() + DropDownList6.SelectedValue.ToString());


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




    private void ExportExcel(GridView SeriesValuesDataGrid)
    {

        string filename = "";
        string today_detail_char = DateTime.Now.AddDays(+0).ToString("yyyy/MM/ddHHmmss").Replace("/", "");
        filename = "T1NewAlarmServer_" + today_detail_char + ".xls";
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
