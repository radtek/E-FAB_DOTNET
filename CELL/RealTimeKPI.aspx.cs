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
using Excel;
using System.Reflection;
 


public partial class RealTimeKPI : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    string today_minus17 = DateTime.Now.AddDays(-17).ToString("yyyy/MM/dd");

    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm");
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string sql_temp4 = "";
    string sql_temp5 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    DataSet ds_temp4 = new DataSet();
    DataSet ds_temp5 = new DataSet(); 


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            sql_temp = " select *                                                                   " +
        "   from (select to_char(to_date(t.cut_time, 'YYYYMMDDHH24MISS'),            " +
        "                        'MM/DD HH24:MI') cut_time,                          " +
        "                t.shop,                                                     " +
        "                t.product,                                                  " +
        "                t.month_plan as 月目標,                                     " +
        "                t.today_plan as 日目標,                                     " +
        "                t.today_actual_p as 日實積,                                 " +
        "                t.acc_plan as 累積計畫,                                     " +
        "                t.acc_actual_p as 累積實績,                                 " +
        "                (t.acc_actual_p - t.acc_plan) as 差異                       " +
        "           from real_kpi_in_out_sum t                                       " +
        "          where t.cut_time > to_char(sysdate - 1, 'YYYYMMDD')               " +
        "            and t.shop = '" + DropDownList1.SelectedValue + "'                                           " +
        "            and t.last_flag = 'Y'                                           " +
        "            and t.type = 'INPUT'                                            " +
        "          order by t.product)                                               " +
        " union all                                                                  " +
        " select tb.* from (                                                         " +
        "                                                                            " +
        " select ta.* from (                                                         " +
        "                                                                            " +
        " select '',                                                                 " +
        "        '" + DropDownList1.SelectedValue + "',                                                           " +
        "        case                                                                " +
        "          when t.product like '%TFT%' then                                  " +
        "           'TFT 總合'                                                       " +
        "          else                                                              " +
        "           'CF 總合'                                                        " +
        "        end product,                                                        " +
        "        sum(t.month_plan),                                                  " +
        "        sum(today_plan),                                                    " +
        "        sum(today_actual_p),                                                " +
        "        sum(acc_plan),                                                      " +
        "        sum(acc_actual_p),                                                  " +
        "        sum(t.acc_actual_p - t.acc_plan)                                    " +
        "   from real_kpi_in_out_sum t                                               " +
        "  where t.cut_time > to_char(sysdate - 1, 'YYYYMMDD')                       " +
        "    and t.shop = '" + DropDownList1.SelectedValue + "'                                                   " +
        "    and t.last_flag = 'Y'                                                   " +
        "    and t.type = 'INPUT'                                                    " +
        "  group by case                                                             " +
        "             when t.product like '%TFT%' then                               " +
        "              'TFT 總合'                                                    " +
        "             else                                                           " +
        "              'CF 總合'                                                     " +
        "           end                                                              " +
        " )ta                                                                        " +
        "                                                                            " +
        " order by case when ta.product like 'TFT 總合' then 0 else 1 end            " +
        "                                                                            " +
        " )tb                                                                        ";



            sql_temp1 = " select * from (                                                                                                                                                                          " +
    " select to_char(to_date(t.cut_time, 'YYYYMMDDHH24MISS'), 'MM/DD HH24:MI') cut_time,                                                                                                       " +
    "        t.shop,                                                                                                                                                                           " +
    "        t.product,                                                                                                                                                                        " +
    "        t.month_plan as 月目標,                                                                                                                                                           " +
    "        t.today_plan as 日目標,                                                                                                                                                           " +
    "        t.today_actual_p as 日實積_P,                                                                                                                                                       " +
    "        t.today_actual_E as 日實積_E,                                                                                                                                                       " +
    "        t.acc_plan as 累積計畫,                                                                                                                                                           " +
    "        t.acc_actual_p as 累積實績_P,                                                                                                                                                       " +
    "        t.acc_actual_e as 累積實績_E,                                                                                                                                                       " +
     "        ( t.acc_g9) as G9,                                                                                                                                             " +
    "        ( t.acc_actual_p+t.acc_actual_e-t.acc_plan-t.acc_g9) as 差異                                                                                                                                             " +
    "   from real_kpi_in_out_sum t                                                                                                                                                             " +
    "  where t.cut_time > to_char(sysdate - 1, 'YYYYMMDD')                                                                                                                                     " +
    "    and t.shop = '" + DropDownList1.SelectedValue + "'                                                                                                                                                                 " +
    "    and t.last_flag = 'Y'                                                                                                                                                                 " +
    "    and t.type = 'OUTPUT'                                                                                                                                                                  " +
    "  order by t.product                                                                                                                                                                      " +
    " )                                                                                                                                                                                        " +
    "                                                                                                                                                                                          " +
    "                                                                                                                                                                                          " +
    "  union all                                                                                                                                                                               " +
    "                                                                                                                                                                                          " +
    "  select '','" + DropDownList1.SelectedValue + "',case when t.product like '%CUT%' then 'CUT 總合' when t.product like '%CHIP%' then 'CHIP 總合'  when t.product like '%(PA)%' then 'PA 總合'                                                                                                                 " +
    "               when t.product like '%SUB' then '未PA 總合' end ,sum(t.month_plan),sum(today_plan),sum(today_actual_p),sum(today_actual_e),sum(acc_plan),sum(acc_actual_p),sum(acc_actual_e),sum( t.acc_g9),sum( t.acc_actual_p+t.acc_actual_e-t.acc_plan-t.acc_g9) from  real_kpi_in_out_sum t     " +
    "    where t.cut_time >  to_char(sysdate - 1, 'YYYYMMDD')                                                                                                                                  " +
    "    and t.shop = '" + DropDownList1.SelectedValue + "'                                                                                                                                                                 " +
    "    and t.last_flag = 'Y'                                                                                                                                                                 " +
    "    and t.type = 'OUTPUT'                                                                                                                                                                  " +
    " group by case when t.product like '%CUT%' then 'CUT 總合'  when t.product like '%CHIP%' then 'CHIP 總合' when t.product like '%(PA)%' then 'PA 總合' when t.product like '%SUB' then '未PA 總合' end                                                                                                                              " +
    "                                                                                                                                                                     ";

            Bind_data(sql_temp, sql_temp1, conn);

            //get_step(DropDownList1.SelectedValue);

        
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
        sql_temp = " select *                                                                   " +
       "   from (select to_char(to_date(t.cut_time, 'YYYYMMDDHH24MISS'),            " +
       "                        'MM/DD HH24:MI') cut_time,                          " +
       "                t.shop,                                                     " +
       "                t.product,                                                  " +
       "                t.month_plan as 月目標,                                     " +
       "                t.today_plan as 日目標,                                     " +
       "                t.today_actual_p as 日實積,                                 " +
       "                t.acc_plan as 累積計畫,                                     " +
       "                t.acc_actual_p as 累積實績,                                 " +
       "                (t.acc_actual_p - t.acc_plan) as 差異                       " +
       "           from real_kpi_in_out_sum t                                       " +
       "          where t.cut_time > to_char(sysdate - 1, 'YYYYMMDD')               " +
       "            and t.shop = '" + DropDownList1.SelectedValue + "'                                           " +
       "            and t.last_flag = 'Y'                                           " +
       "            and t.type = 'INPUT'                                            " +
       "          order by t.product)                                               " +
       " union all                                                                  " +
       " select tb.* from (                                                         " +
       "                                                                            " +
       " select ta.* from (                                                         " +
       "                                                                            " +
       " select '',                                                                 " +
       "        '" + DropDownList1.SelectedValue + "',                                                           " +
       "        case                                                                " +
       "          when t.product like '%TFT%' then                                  " +
       "           'TFT 總合'                                                       " +
       "          else                                                              " +
       "           'CF 總合'                                                        " +
       "        end product,                                                        " +
       "        sum(t.month_plan),                                                  " +
       "        sum(today_plan),                                                    " +
       "        sum(today_actual_p),                                                " +
       "        sum(acc_plan),                                                      " +
       "        sum(acc_actual_p),                                                  " +
       "        sum(t.acc_actual_p - t.acc_plan)                                    " +
       "   from real_kpi_in_out_sum t                                               " +
       "  where t.cut_time > to_char(sysdate - 1, 'YYYYMMDD')                       " +
       "    and t.shop = '" + DropDownList1.SelectedValue + "'                                                   " +
       "    and t.last_flag = 'Y'                                                   " +
       "    and t.type = 'INPUT'                                                    " +
       "  group by case                                                             " +
       "             when t.product like '%TFT%' then                               " +
       "              'TFT 總合'                                                    " +
       "             else                                                           " +
       "              'CF 總合'                                                     " +
       "           end                                                              " +
       " )ta                                                                        " +
       "                                                                            " +
       " order by case when ta.product like 'TFT 總合' then 0 else 1 end            " +
       "                                                                            " +
       " )tb                                                                        ";




        sql_temp1 = " select * from (                                                                                                                                                                          " +
" select to_char(to_date(t.cut_time, 'YYYYMMDDHH24MISS'), 'MM/DD HH24:MI') cut_time,                                                                                                       " +
"        t.shop,                                                                                                                                                                           " +
"        t.product,                                                                                                                                                                        " +
"        t.month_plan as 月目標,                                                                                                                                                           " +
"        t.today_plan as 日目標,                                                                                                                                                           " +
"        t.today_actual_p as 日實積_P,                                                                                                                                                       " +
"        t.today_actual_E as 日實積_E,                                                                                                                                                       " +
"        t.acc_plan as 累積計畫,                                                                                                                                                           " +
"        t.acc_actual_p as 累積實績_P,                                                                                                                                                       " +
"        t.acc_actual_e as 累積實績_E,                                                                                                                                                       " +
  "        ( t.acc_g9) as G9,                                                                                                                                             " +
"        ( t.acc_actual_p+t.acc_actual_e-t.acc_plan-t.acc_g9) as 差異                                                                                                                                             " +
"   from real_kpi_in_out_sum t                                                                                                                                                             " +
"  where t.cut_time > to_char(sysdate - 1, 'YYYYMMDD')                                                                                                                                     " +
"    and t.shop = '" + DropDownList1.SelectedValue + "'                                                                                                                                                                 " +
"    and t.last_flag = 'Y'                                                                                                                                                                 " +
"    and t.type = 'OUTPUT'                                                                                                                                                                  " +
"  order by t.product                                                                                                                                                                      " +
" )                                                                                                                                                                                        " +
"                                                                                                                                                                                          " +
"                                                                                                                                                                                          " +
"  union all                                                                                                                                                                               " +
"                                                                                                                                                                                          " +
"  select '','" + DropDownList1.SelectedValue + "',case when t.product like '%CUT%' then 'CUT 總合' when t.product like '%CHIP%' then 'CHIP 總合'  when t.product like '%(PA)%' then 'PA 總合'                                                                                                                 " +
"               when t.product like '%SUB' then '未PA 總合' end ,sum(t.month_plan),sum(today_plan),sum(today_actual_p),sum(today_actual_e),sum(acc_plan),sum(acc_actual_p),sum(acc_actual_e),sum(t.acc_g9),sum(t.acc_actual_p+t.acc_actual_e-t.acc_plan-t.acc_g9) from  real_kpi_in_out_sum t     " +
"    where t.cut_time >  to_char(sysdate - 1, 'YYYYMMDD')                                                                                                                                  " +
"    and t.shop = '" + DropDownList1.SelectedValue + "'                                                                                                                                                                 " +
"    and t.last_flag = 'Y'                                                                                                                                                                 " +
"    and t.type = 'OUTPUT'                                                                                                                                                                  " +
" group by case when t.product like '%CUT%' then 'CUT 總合'  when t.product like '%CHIP%' then 'CHIP 總合' when t.product like '%(PA)%' then 'PA 總合' when t.product like '%SUB' then '未PA 總合' end                                                                                                                              " +
"                                                                                                                                                                     ";

        Bind_data(sql_temp, sql_temp1, conn);

        GridView3.DataSource = "";
        GridView3.DataBind();


    }

    public void get_step(string shop)
    {
        if (DropDownList1.SelectedValue.Equals("T0CELL"))
        {
            sql_temp4 = " select distinct innrpt.rpt.get_ksrstepname(decode(t.activity,                                      " +
"                                                   'LotStart',                                      " +
"                                                   decode(substr(t.step_name,                       " +
"                                                                 1,                                 " +
"                                                                 4),                                " +
"                                                          '3090',                                   " +
"                                                          DECODE(T.EQP_ID,                          " +
"                                                                 '1CPCT100',                        " +
"                                                                 '6060',                            " +
"                                                                 '3060'),                           " +
"                                                          '6090',                                   " +
"                                                          '6060',                                   " +
"                                                          '6610',                                   " +
"                                                          '6500',                                   " +
"                                                          '6540',                                   " +
"                                                          '6500',                                   " +
"                                                          '6927',                                   " +
"                                                          '6500',                                   " +
"                                                          '6530',                                   " +
"                                                          '3510',                                   " +
"                                                          '3520',                                   " +
"                                                          '3510',                                   " +
"                                                          '3510',                                   " +
"                                                          '6500'),                                  " +
"                                                   t.step_name))as step                             " +
"   from move_history t                                                                              " +
"  where t.shift_date > to_char(sysdate - 3, 'YYYYMMDD')                                             " +
"    and decode(t.activity,                                                                          " +
"               'LotStart',                                                                          " +
"               decode(substr(t.step_name, 1, 4),                                                    " +
"                      '3090',                                                                       " +
"                      DECODE(T.EQP_ID, '1CPCT100', '6060', '3060'),                                 " +
"                      '6090',                                                                       " +
"                      '6060',                                                                       " +
"                      '6610',                                                                       " +
"                      '6500',                                                                       " +
"                      '6540',                                                                       " +
"                      '6500',                                                                       " +
"                      '6927',                                                                       " +
"                      '6500',                                                                       " +
"                      '6530',                                                                       " +
"                      '3510',                                                                       " +
"                      '3520',                                                                       " +
"                      '3510',                                                                       " +
"                      '3510',                                                                       " +
"                      '6500',                                                                       " +
"                      '3920',                                                                       " +
"                      decode(t.prod_name,                                                           " +
"                             '800G0C',                                                              " +
"                             '3510',                                                                " +
"                             '350G0C',                                                              " +
"                             '3510',                                                                " +
"                             '3920'),                                                               " +
"                      '3520',                                                                       " +
"                      decode(t.prod_name,                                                           " +
"                             '800G0C',                                                              " +
"                             '3510',                                                                " +
"                             '350G0C',                                                              " +
"                             '3510',                                                                " +
"                             '3520'),                                                               " +
"                      '3511',                                                                       " +
"                      decode(t.prod_name,                                                           " +
"                             '800G0C',                                                              " +
"                             '3510',                                                                " +
"                             '350G0C',                                                              " +
"                             '3510',                                                                " +
"                             '3511')),                                                              " +
"               '3515',                                                                              " +
"               '3511',                                                                              " +
"               '6530',                                                                              " +
"               decode(substr(t.eqp_id, 1, 2),                                                       " +
"                      '1C',                                                                         " +
"                      '6500',                                                                       " +
"                      decode(t.eqp_id, '0CSSB100', '3515', '3511')),                                " +
"               '6605',                                                                              " +
"               '6500',                                                                              " +
"               '3395',                                                                              " +
"               '6500',                                                                              " +
"               '6531',                                                                              " +
"               '6500',                                                                              " +
"               '3513',                                                                              " +
"               '6500',                                                                              " +
"               '3941',                                                                              " +
"               '3060',                                                                              " +
"               '3940',                                                                              " +
"               '6060',                                                                              " +
"               t.step_name) like '3%'                                                               " +
" union                                                                                              " +
" select '6063_St_Precut_Chip'                                                                       " +
"   from dual                                                                                        " +
" union                                                                                              " +
" select '6940_Ary_Panel_Insert' from dual order by 1                                                ";


        }
        else
        {

            sql_temp4 = " select distinct innrpt.rpt.get_ksrstepname(decode(t.activity,                                           " +
"                                                   'LotStart',                                           " +
"                                                   decode(substr(t.step_name,                            " +
"                                                                 1,                                      " +
"                                                                 4),                                     " +
"                                                          '3090',                                        " +
"                                                          DECODE(T.EQP_ID,                               " +
"                                                                 '1CPCT100',                             " +
"                                                                 '6060',                                 " +
"                                                                 '3060'),                                " +
"                                                          '6090',                                        " +
"                                                          '6060',                                        " +
"                                                          '6610',                                        " +
"                                                          '6500',                                        " +
"                                                          '6540',                                        " +
"                                                          '6500',                                        " +
"                                                          '6927',                                        " +
"                                                          '6500',                                        " +
"                                                          '6530',                                        " +
"                                                          '3510',                                        " +
"                                                          '3520',                                        " +
"                                                          '3510',                                        " +
"                                                          '3510',                                        " +
"                                                          '6500'),                                       " +
"                                                   t.step_name))as step                                  " +
"   from move_history t                                                                                   " +
"  where t.shift_date > to_char(sysdate - 3, 'YYYYMMDD')                                                  " +
"    and decode(t.activity,                                                                               " +
"               'LotStart',                                                                               " +
"               decode(substr(t.step_name, 1, 4),                                                         " +
"                      '3090',                                                                            " +
"                      DECODE(T.EQP_ID, '1CPCT100', '6060', '3060'),                                      " +
"                      '6090',                                                                            " +
"                      '6060',                                                                            " +
"                      '6610',                                                                            " +
"                      '6500',                                                                            " +
"                      '6540',                                                                            " +
"                      '6500',                                                                            " +
"                      '6927',                                                                            " +
"                      '6500',                                                                            " +
"                      '6530',                                                                            " +
"                      '3510',                                                                            " +
"                      '3520',                                                                            " +
"                      '3510',                                                                            " +
"                      '3510',                                                                            " +
"                      '6500',                                                                            " +
"                      '3920',                                                                            " +
"                      decode(t.prod_name,                                                                " +
"                             '800G0C',                                                                   " +
"                             '3510',                                                                     " +
"                             '350G0C',                                                                   " +
"                             '3510',                                                                     " +
"                             '3920'),                                                                    " +
"                      '3520',                                                                            " +
"                      decode(t.prod_name,                                                                " +
"                             '800G0C',                                                                   " +
"                             '3510',                                                                     " +
"                             '350G0C',                                                                   " +
"                             '3510',                                                                     " +
"                             '3520'),                                                                    " +
"                      '3511',                                                                            " +
"                      decode(t.prod_name,                                                                " +
"                             '800G0C',                                                                   " +
"                             '3510',                                                                     " +
"                             '350G0C',                                                                   " +
"                             '3510',                                                                     " +
"                             '3511')),                                                                   " +
"               '3515',                                                                                   " +
"               '3511',                                                                                   " +
"               '6530',                                                                                   " +
"               decode(substr(t.eqp_id, 1, 2),                                                            " +
"                      '1C',                                                                              " +
"                      '6500',                                                                            " +
"                      decode(t.eqp_id, '0CSSB100', '3515', '3511')),                                     " +
"               '6605',                                                                                   " +
"               '6500',                                                                                   " +
"               '3395',                                                                                   " +
"               '6500',                                                                                   " +
"               '6531',                                                                                   " +
"               '6500',                                                                                   " +
"               '3513',                                                                                   " +
"               '6500',                                                                                   " +
"               '3941',                                                                                   " +
"               '3060',                                                                                   " +
"               '3940',                                                                                   " +
"               '6060',                                                                                   " +
"               t.step_name) like '6%'                                                                    " +
"  order by 1                                                                                             ";
        
        }

        ds_temp4 = func.get_dataSet_access(sql_temp4, conn);

        ListBox1.DataSource = ds_temp4.Tables[0];
        ListBox1.DataTextField="STEP";
        ListBox1.DataValueField ="STEP";
        ListBox1.DataBind();

    
    
    }


    public DataSet Bind_data(string sqlX, string sqlX1, string connx)
    {
        sql_temp = sqlX;


        sql_temp1 = sqlX1;

        ds_temp = func.get_dataSet_access(sql_temp, connx);



        GridView1.DataSource = ds_temp.Tables[0];


        GridView1.DataBind();

        ds_temp1 = func.get_dataSet_access(sql_temp1, connx);

        GridView2.DataSource = ds_temp1.Tables[0];


        GridView2.DataBind();



        return ds_temp;

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
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            //Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
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

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            //Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
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


    protected void Button2_Click(object sender, EventArgs e) 
{
    string[] title1 ={  "CUT_TIME ", "SHOP", "PRODUCT", "月目標", "日目標", "日實積", "累積計畫", "累積實績", "差異" };

    string[] title2 ={ " CUT_TIME", "SHOP", "PRODUCT", "月目標", "日目標", "日實積_P", "日實積_E", "累積計畫", "累積實績_P", "累積實績_E", "G9", "差異" };


    sql_temp2 = " select *                                                                   " +
        "   from (select to_char(to_date(t.cut_time, 'YYYYMMDDHH24MISS'),            " +
        "                        'MM/DD HH24:MI') cut_time,                          " +
        "                t.shop,                                                     " +
        "                t.product,                                                  " +
        "                t.month_plan as 月目標,                                     " +
        "                t.today_plan as 日目標,                                     " +
        "                t.today_actual_p as 日實積,                                 " +
        "                t.acc_plan as 累積計畫,                                     " +
        "                t.acc_actual_p as 累積實績,                                 " +
        "                (t.acc_actual_p - t.acc_plan) as 差異                       " +
        "           from real_kpi_in_out_sum t                                       " +
        "          where t.cut_time > to_char(sysdate - 1, 'YYYYMMDD')               " +
        "            and t.shop = '" + DropDownList1.SelectedValue + "'                                           " +
        "            and t.last_flag = 'Y'                                           " +
        "            and t.type = 'INPUT'                                            " +
        "          order by t.product)                                               " +
        " union all                                                                  " +
        " select tb.* from (                                                         " +
        "                                                                            " +
        " select ta.* from (                                                         " +
        "                                                                            " +
        " select '',                                                                 " +
        "        '" + DropDownList1.SelectedValue + "',                                                           " +
        "        case                                                                " +
        "          when t.product like '%TFT%' then                                  " +
        "           'TFT 總合'                                                       " +
        "          else                                                              " +
        "           'CF 總合'                                                        " +
        "        end product,                                                        " +
        "        sum(t.month_plan),                                                  " +
        "        sum(today_plan),                                                    " +
        "        sum(today_actual_p),                                                " +
        "        sum(acc_plan),                                                      " +
        "        sum(acc_actual_p),                                                  " +
        "        sum(t.acc_actual_p - t.acc_plan)                                    " +
        "   from real_kpi_in_out_sum t                                               " +
        "  where t.cut_time > to_char(sysdate - 1, 'YYYYMMDD')                       " +
        "    and t.shop = '" + DropDownList1.SelectedValue + "'                                                   " +
        "    and t.last_flag = 'Y'                                                   " +
        "    and t.type = 'INPUT'                                                    " +
        "  group by case                                                             " +
        "             when t.product like '%TFT%' then                               " +
        "              'TFT 總合'                                                    " +
        "             else                                                           " +
        "              'CF 總合'                                                     " +
        "           end                                                              " +
        " )ta                                                                        " +
        "                                                                            " +
        " order by case when ta.product like 'TFT 總合' then 0 else 1 end            " +
        "                                                                            " +
        " )tb                                                                        ";

    sql_temp3 = " select * from (                                                                                                                                                                          " +
   " select to_char(to_date(t.cut_time, 'YYYYMMDDHH24MISS'), 'MM/DD HH24:MI') cut_time,                                                                                                       " +
   "        t.shop,                                                                                                                                                                           " +
   "        t.product,                                                                                                                                                                        " +
   "        t.month_plan as 月目標,                                                                                                                                                           " +
   "        t.today_plan as 日目標,                                                                                                                                                           " +
   "        t.today_actual_p as 日實積_P,                                                                                                                                                       " +
   "        t.today_actual_E as 日實積_E,                                                                                                                                                       " +
   "        t.acc_plan as 累積計畫,                                                                                                                                                           " +
   "        t.acc_actual_p as 累積實績_P,                                                                                                                                                       " +
   "        t.acc_actual_e as 累積實績_E,                                                                                                                                                       " +
     "        ( t.acc_g9) as G9,                                                                                                                                             " +
   "        ( t.acc_actual_p+t.acc_actual_e-t.acc_plan-t.acc_g9) as 差異                                                                                                                                             " +
   "   from real_kpi_in_out_sum t                                                                                                                                                             " +
   "  where t.cut_time > to_char(sysdate - 1, 'YYYYMMDD')                                                                                                                                     " +
   "    and t.shop = '" + DropDownList1.SelectedValue + "'                                                                                                                                                                 " +
   "    and t.last_flag = 'Y'                                                                                                                                                                 " +
   "    and t.type = 'OUTPUT'                                                                                                                                                                  " +
   "  order by t.product                                                                                                                                                                      " +
   " )                                                                                                                                                                                        " +
   "                                                                                                                                                                                          " +
   "                                                                                                                                                                                          " +
   "  union all                                                                                                                                                                               " +
   "                                                                                                                                                                                          " +
   "  select '','" + DropDownList1.SelectedValue + "',case when t.product like '%CUT%' then 'CUT 總合' when t.product like '%CHIP%' then 'CHIP 總合'  when t.product like '%(PA)%' then 'PA 總合'                                                                                                                 " +
   "               when t.product like '%SUB' then '未PA 總合' end ,sum(t.month_plan),sum(today_plan),sum(today_actual_p),sum(today_actual_e),sum(acc_plan),sum(acc_actual_p),sum(acc_actual_e),sum(t.acc_g9),sum(t.acc_actual_p+t.acc_actual_e-t.acc_plan-t.acc_g9) from  real_kpi_in_out_sum t     " +
   "    where t.cut_time >  to_char(sysdate - 1, 'YYYYMMDD')                                                                                                                                  " +
   "    and t.shop = '" + DropDownList1.SelectedValue + "'                                                                                                                                                                 " +
   "    and t.last_flag = 'Y'                                                                                                                                                                 " +
   "    and t.type = 'OUTPUT'                                                                                                                                                                  " +
   " group by case when t.product like '%CUT%' then 'CUT 總合'  when t.product like '%CHIP%' then 'CHIP 總合' when t.product like '%(PA)%' then 'PA 總合' when t.product like '%SUB' then '未PA 總合' end                                                                                                                              " +
   "                                                                                                                                                                     ";
       
        
        
        
        ds_temp2 = func.get_dataSet_access(sql_temp2, conn);
        ds_temp3 = func.get_dataSet_access(sql_temp3, conn); 
Excel.Application ExlApp; 
Excel.Workbook ExlBook; 
Excel.Worksheet ExlSheet; 
ExlApp = new Excel.Application(); 

string SavePath = @"" + Server.MapPath(".") +"\\Save_file\\";
string FileName = DateTime.Now.AddDays(0).ToString("yyyyMMddHHmmss") +"_" +DropDownList1 .SelectedValue+ "_RealTimeKPI"; 

ExlApp.Application.DisplayAlerts = true; 
ExlApp.Application.Visible = true;

ExlBook = ExlApp.Workbooks.Add(Server.MapPath(".") + "\\RealTimeKPI.xls"); 

//ExlSheet = (Excel.Worksheet)ExlBook.Sheets.Add(Missing.Value, Missing.Value, ds_temp2.Tables[0].Rows.Count, Missing.Value);
ExlSheet = (Excel.Worksheet)ExlBook.Sheets.Add(Missing.Value, Missing.Value, 1, Missing.Value); 




ExlSheet = (Excel.Worksheet)ExlBook.Worksheets.get_Item(1); 

// ExlSheet = (Excel.Worksheet)ExlBook.Worksheets.get_Item(0); 

ExlSheet.Name = "即時投入產出資訊";

ExlSheet.Cells[1, 1] = "即時投入資訊";

int title1_X = 1;
int title1_Y = 2;

for (int i = 0; i <= title1.Length-1; i++)
{
    ExlSheet.Cells[title1_Y,title1_X + i] = title1[i];
}


int excel_title_X = 1; 
int excel_title_Y = 3;


int excel_title_Y_temp = 0;

// Run First DataSet Data Input
for (int i = 0; i < ds_temp2.Tables[0].Rows.Count; i++) 
{
    for (int j = 0; j < ds_temp2.Tables[0].Columns.Count; j++) 
{
    ExlSheet.Cells[excel_title_Y + i, excel_title_X + j] = ds_temp2.Tables[0].Rows[i][j].ToString();


    excel_title_Y_temp = excel_title_Y + i;
// write font change red for delay date 
//ExlSheet.get_Range(ExlSheet.Cells[excel_title_Y + i, excel_title_X + j], ExlSheet.Cells[excel_title_Y + i, excel_title_X + j]).Font.Color =200; 


} 
}

excel_title_Y_temp = excel_title_Y_temp + 1;

ExlSheet.Cells[excel_title_Y_temp, 1] = "即時產出資訊";

int title2_X = 1;
int title2_Y = excel_title_Y_temp+1;



for (int i = 0; i <= title2.Length - 1; i++)
{
    ExlSheet.Cells[title2_Y, title2_X + i] = title2[i];
}


excel_title_Y_temp = excel_title_Y_temp + 2;

// Run 2nd DataSet Data Output
for (int i = 0; i < ds_temp3.Tables[0].Rows.Count; i++)
{
    for (int j = 0; j < ds_temp3.Tables[0].Columns.Count; j++)
    {
        ExlSheet.Cells[excel_title_Y_temp + i, excel_title_X + j] = ds_temp3.Tables[0].Rows[i][j].ToString();

        // write font change red for delay date 
        //ExlSheet.get_Range(ExlSheet.Cells[excel_title_Y + i, excel_title_X + j], ExlSheet.Cells[excel_title_Y + i, excel_title_X + j]).Font.Color =200; 


    }
}



ExlSheet.get_Range("A1", "P100").EntireColumn.AutoFit();

ExlSheet.get_Range("A1", "P100").EntireRow.AutoFit();

ExlSheet.get_Range("A1", "I1").Merge(false);

ExlSheet.get_Range("A1", "I1").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Fuchsia); //背景顏色 



ExlSheet.get_Range(ExlSheet.Cells[title2_Y-1, 1], ExlSheet.Cells[title2_Y-1,12 ]).Merge(false);


ExlSheet.get_Range(ExlSheet.Cells[title2_Y-1, 1], ExlSheet.Cells[title2_Y-1, 12]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);









// initial position (excel_title_Y,excel_title_X)=(4,1) 

ExlApp.Visible = false; 
ExlApp.UserControl = false; 



ExlBook.SaveAs(SavePath + FileName, Excel.XlFileFormat.xlWorkbookNormal, 
null, null, false, false, Excel.XlSaveAsAccessMode.xlShared, 
false, false, null, null, null); 

ExlBook.Close(null, null, null); 
ExlApp.Workbooks.Close(); 
ExlApp.Quit(); 

System.Runtime.InteropServices.Marshal.ReleaseComObject(ExlApp); 
System.Runtime.InteropServices.Marshal.ReleaseComObject(ExlSheet); 
System.Runtime.InteropServices.Marshal.ReleaseComObject(ExlBook); 
ExlSheet = null; 
ExlBook = null; 
ExlApp = null; 




string ls_file = FileName + ".xls"; 
string ls_filename = Server.MapPath(".") + "\\Save_file\\" + ls_file; 
//銝頛瑼獢鞈User蝡? 
if (System.IO.File.Exists(ls_filename)) 
{ 
Response.ContentType = "application/save-as"; 
Response.AddHeader("content-disposition", "attachment; filename=" + ls_file); 
Response.WriteFile(ls_filename); 
} 
else 
{
    Response.Write("<script language=JavaScript>alert('檔案不存在，請重新執行！')</script>");
// Response.Write("<script language=JavaScript>location.href= ('Default.aspx');</script>"); 
} 

//慦擗Server銝豍芣?甇斗挾毇毇神,憒槼訾豢挾諙貉潛典潭瞉銝撠瑼獢) 
//if (System.IO.File.Exists(ls_filename)) 
//{ 
// System.IO.File.Delete(ls_filename); 
//} 



}



    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_step(DropDownList1.SelectedValue);
        foreach (ListItem item in ListBox2.Items)
        {
            ListBox1.Items.Add(item);
        }
        ListBox2.Items.Clear();

    }


    protected void Button12_Click(object sender, EventArgs e)//add select 
    {


        if (ListBox1.SelectedItem != null)
        {
            foreach (ListItem item in ListBox1.Items)
            {
                if (item.Selected == true)
                {
                    ListBox2.Items.Add(item);
                }
            }

            foreach (ListItem item in ListBox2.Items)
            {
                if (item.Selected == true)
                {
                    ListBox1.Items.Remove(item);
                }
            }
        }

       
    }
    protected void Button11_Click(object sender, EventArgs e) //add select all 
    {
      



        foreach (ListItem item in ListBox1.Items)
        {
            ListBox2.Items.Add(item);
        }
        ListBox1.Items.Clear();

       
    }


    protected void Button13_Click(object sender, EventArgs e) //remove select 
    {
        if (ListBox2.SelectedItem != null)
        {
            foreach (ListItem item in ListBox2.Items)
            {
                if (item.Selected == true)
                {
                    ListBox1.Items.Add(item);
                }
            }

            foreach (ListItem item in ListBox1.Items)
            {
                if (item.Selected == true)
                {
                    ListBox2.Items.Remove(item);
                }
            }
        }

       
    }


    protected void Button14_Click(object sender, EventArgs e)//remove select all 
    {
        foreach (ListItem item in ListBox2.Items)
        {
            ListBox1.Items.Add(item);
        }
        ListBox2.Items.Clear();
    }

    public string string_combind()
    {

       

            string maillist1 = "";
            string maillist2 = "";
            Int32 Counterx = 0;
            for (int i = 0; i <= ListBox2.Items.Count - 1; i++)
            {

                if (Counterx == 0)
                {
                    maillist1 = "'"+ListBox2.Items[i].Text+"'";
                }
                else

                    maillist1 = ",'"+ListBox2.Items[i].Text +"'"; //呈現每一個 DataSet Row[i] 
                maillist2 = maillist2 + maillist1; //將每個 DataSet Row[i] 的值串起來 
                Counterx++;
            }
            return maillist2;
       
     
    }


    protected void Button3_Click(object sender, EventArgs e)
    {

        if (DropDownList1.SelectedValue.Equals("T1CELL"))
        {
            sql_temp5 = " SELECT v.shift_date,                                                                        " +
"        v.shop,                                                                              " +
"        v.step,                                                                              " +
"        v.eqp_id,                                                                            " +
"        v.GLASS_TYPE,                                                                        " +
"        v.product,                                                                           " +
"        v.UNIT,                                                                              " +
"        v.MOVE,                                                                              " +
"        v.DSHIFTMOVE,                                                                        " +
"        v.NSHIFTMOVE,                                                                        " +
"        v.move_07,                                                                           " +
"        v.move_08,                                                                           " +
"        v.move_09,                                                                           " +
"        v.move_10,                                                                           " +
"        v.move_11,                                                                           " +
"        v.move_12,                                                                           " +
"        v.move_13,                                                                           " +
"        v.move_14,                                                                           " +
"        v.move_15,                                                                           " +
"        v.move_16,                                                                           " +
"        v.move_17,                                                                           " +
"        v.move_18,                                                                           " +
"        v.move_19,                                                                           " +
"        v.move_20,                                                                           " +
"        v.move_21,                                                                           " +
"        v.move_22,                                                                           " +
"        v.move_23,                                                                           " +
"        v.move_24,                                                                           " +
"        v.move_01,                                                                           " +
"        v.move_02,                                                                           " +
"        v.move_03,                                                                           " +
"        v.move_04,                                                                           " +
"        v.move_05,                                                                           " +
"        v.move_06                                                                            " +
"   FROM innrpt.rpt_mfg_eqp_move2_v V                                                         " +
"  WHERE (V.shop = 'T1Cell' or STEP = '6063_St_Precut_Chip' or                                " +
"        STEP = '6940_Ary_Panel_Insert')                                                      " +
"    AND V.shift_date between to_date('2011/6/10', 'yyyy/mm/dd') and                          " +
"        to_date('2011/6/10', 'yyyy/mm/dd')                                                   " +
"    and STEP in (" + string_combind() + ")                                                                           " +
"  order by 1, 2, 3, 4, 5, 6, 7                                                               ";

        }
        else
        {
            sql_temp5 = " SELECT v.shift_date,                                                       " +
 "        v.shop,                                                             " +
 "        v.step,                                                             " +
 "        v.eqp_id,                                                           " +
 "        v.GLASS_TYPE,                                                       " +
 "        v.product,                                                          " +
 "        v.UNIT,                                                             " +
 "        v.MOVE,                                                             " +
 "        v.DSHIFTMOVE,                                                       " +
 "        v.NSHIFTMOVE,                                                       " +
 "        v.move_07,                                                          " +
 "        v.move_08,                                                          " +
 "        v.move_09,                                                          " +
 "        v.move_10,                                                          " +
 "        v.move_11,                                                          " +
 "        v.move_12,                                                          " +
 "        v.move_13,                                                          " +
 "        v.move_14,                                                          " +
 "        v.move_15,                                                          " +
 "        v.move_16,                                                          " +
 "        v.move_17,                                                          " +
 "        v.move_18,                                                          " +
 "        v.move_19,                                                          " +
 "        v.move_20,                                                          " +
 "        v.move_21,                                                          " +
 "        v.move_22,                                                          " +
 "        v.move_23,                                                          " +
 "        v.move_24,                                                          " +
 "        v.move_01,                                                          " +
 "        v.move_02,                                                          " +
 "        v.move_03,                                                          " +
 "        v.move_04,                                                          " +
 "        v.move_05,                                                          " +
 "        v.move_06                                                           " +
 "   FROM innrpt.rpt_mfg_eqp_move2_v V                                        " +
 "  WHERE (V.shop = 'T0Cell' or STEP = '6063_St_Precut_Chip' or               " +
 "        STEP = '6940_Ary_Panel_Insert')                                     " +
 "    AND V.shift_date between to_date('2011/6/10', 'yyyy/mm/dd') and         " +
 "        to_date('2011/6/10', 'yyyy/mm/dd')                                  " +
 "    and STEP in                                                             " +
 "        (" + string_combind() + ")                                                                  " +
 "  order by 1, 2, 3, 4, 5, 6, 7                                              ";
        
        }

        ds_temp5 = func.get_dataSet_access(sql_temp5, conn);

        GridView3.DataSource = ds_temp5.Tables[0];
        GridView3.DataBind();

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
}
