﻿using System;
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

public partial class TP_TP_balance : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE_STD"];
    string sql_temp = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    ArrayList arlist_temp1 = new ArrayList();
    string get_product = "";
    string Array_product = "";
    string CF_product = "";
    string Cell_product = "";
    string[] shop ={ "Array", "T1CF", "Cell" };
    string[] shop_product ={ "", "", "" };
    
   

    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            for (int i = 0; i < shop.Length - 1; i++)
            {
                get_product = " select t.product from tp_balance t" +
" where t.fab like '%" + shop[i] + "%'        ";

                ds_temp2 = func.get_dataSet_access(get_product, conn);

                shop_product[i] = combine_Array_to_string(ds_temp2);
            }
            Array_product = shop_product[0];
            CF_product = shop_product[1];
            Cell_product = shop_product[2];
            bind_data1();

            bind_data2();
            bind_data3();
        }

        GridView1.Visible = false;
        Label1.Text = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    }



    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
        
    }

    public DataSet bind_data1()
    {
        string sql = "select rownum, tt.*                                                                                                                        " +
"  from (select replace(op1.shop, '.Fab1', '') as shop,                                                                                     " +
"               replace(op1.shop, '.Fab1', '') || '_' || op1.prod_id as shop_product,                                                       " +
"               max(op1.shift_day) as shift_day,                                                                                            " +
"               op1.prod_id,                                                                                                                " +
"               sum(op1.begin_wip) as begin_wip,                                                                                            " +
"               sum(op1.in_qty) as in_qty,                                                                                                  " +
"                                                                                                                                           " +
"               sum(op1.begin_wip) + sum(op1.in_qty) as begin_in,                                                                           " +
"               sum(op1.scrap_total) as scrap_total,                                                                                        " +
"               sum(op1.end_wip) as end_wip,                                                                                                " +
"               sum(op1.out_qty) as out_qty,                                                                                                " +
"               sum(op1.destroy_qty) as destroy_qty,                                                                                        " +
"               sum(op1.scrap_total) + sum(op1.end_wip) + sum(op1.out_qty)+sum(op1.destroy_qty) as scrap_end_out_destory,                   " +
"                                                                                                                                           " +
"               sum(op1.begin_wip) + sum(op1.in_qty) - sum(op1.scrap_total) -                                                               " +
"               sum(op1.end_wip) - sum(op1.out_qty)-sum(op1.destroy_qty) as diff,                                                           " +
"               op1.output_type                                                                                                             " +
"          from (select substr(t.shift_day_key, 0, 8) as shift_day,                                                                         " +
"                       case                                                                                                                " +
"                         when substr(t.shift_day_key, 7, 2) = '01' then                                                                    " +
"                          t.dt_starttm_wip_qty                                                                                             " +
"                         else                                                                                                              " +
"                          0                                                                                                                " +
"                       end begin_wip,                                                                                                      " +
"                       case                                                                                                                " +
"                         when substr(t.shift_day_key, 0, 8) =                                                                              " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                                                              " +
"                          t.dt_starttm_wip_qty                                                                                             " +
"                         else                                                                                                              " +
"                          0                                                                                                                " +
"                       end end_wip,                                                                                                        " +
"                       t.dt_starttm_wip_qty as current_wip,                                                                                " +
"                       case when substr(t.shift_day_key, 0, 8) =                                                                           " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else t.in_qty end in_qty,                                  " +
"                       case when substr(t.shift_day_key, 0, 8) =                                                                           " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else t.out_qty end out_qty,                                " +
"                        case when substr(t.shift_day_key, 0, 8) =                                                                          " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else (t.scrap_qty - t.unship_qty) end scrap_total,         " +
"                        case when substr(t.shift_day_key, 0, 8) =                                                                          " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else t.destroy_qty end destroy_qty,                        " +
"                       t.output_type,                                                                                                      " +
"                       substr(t.prod_id, 0, 6) as prod_id,                                                                                 " +
"                       substr(t.prod_id, 8, 15) as SHOP                                                                                    " +
"                  from daily_in_out_sum t                                                                                                  " +
"                 where substr(t.prod_id, 0, 6) in                                                                                          " +
"                       (" + Array_product + "  )                              " +
"                       and substr(t.shift_day_key, 0, 6) =                                                                                 " +
"                       substr(to_char(sysdate, 'yyyyMMdd'), 0, 6)) op1                                                                     " +
"         group by op1.output_type, op1.shop, op1.prod_id                                                                                   " +
"        union                                                                                                                              " +
"        select substr(replace(op1.shop, '.Fab1', ''), 4, 5) as shop,                                                                       " +
"               substr(replace(op1.shop, '.Fab1', ''), 4, 5) || '_' ||                                                                      " +
"               op1.prod_id as shop_product,                                                                                                " +
"               max(op1.shift_day) as shift_day,                                                                                            " +
"               op1.prod_id,                                                                                                                " +
"               sum(op1.begin_wip) as begin_wip,                                                                                            " +
"               sum(op1.in_qty) as in_qty,                                                                                                  " +
"                                                                                                                                           " +
"               sum(op1.begin_wip) + sum(op1.in_qty) as begin_in,                                                                           " +
"               sum(op1.scrap_total) as scrap_total,                                                                                        " +
"               sum(op1.end_wip) as end_wip,                                                                                                " +
"               sum(op1.out_qty) as out_qty,                                                                                                " +
"                sum(op1.destroy_qty) as destroy_qty,                                                                                       " +
"               sum(op1.scrap_total) + sum(op1.end_wip) + sum(op1.out_qty) as scrap_end_out_destory,                                        " +
"                                                                                                                                           " +
"               sum(op1.begin_wip) + sum(op1.in_qty) - sum(op1.scrap_total) -                                                               " +
"               sum(op1.end_wip) - sum(op1.out_qty) as diff,                                                                                " +
"               op1.output_type                                                                                                             " +
"          from (select substr(t.shift_day_key, 0, 8) as shift_day,                                                                         " +
"                       case                                                                                                                " +
"                         when substr(t.shift_day_key, 7, 2) = '01' then                                                                    " +
"                          t.dt_starttm_wip_qty                                                                                             " +
"                         else                                                                                                              " +
"                          0                                                                                                                " +
"                       end begin_wip,                                                                                                      " +
"                       case                                                                                                                " +
"                         when substr(t.shift_day_key, 0, 8) =                                                                              " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                                                              " +
"                          t.dt_starttm_wip_qty                                                                                             " +
"                         else                                                                                                              " +
"                          0                                                                                                                " +
"                       end end_wip,                                                                                                        " +
"                       t.dt_starttm_wip_qty as current_wip,                                                                                " +
"                       case when substr(t.shift_day_key, 0, 8) =                                                                           " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else t.in_qty end in_qty,                                  " +
"                       case when substr(t.shift_day_key, 0, 8) =                                                                           " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else t.out_qty end out_qty,                                " +
"                       case                                                                                                                " +
"                         when t.unship_qty is null then                                                                                    " +
"                          t.scrap_qty                                                                                                      " +
"                         else                                                                                                              " +
"                          t.scrap_qty - t.unship_qty                                                                                       " +
"                       end scrap_total,                                                                                                    " +
"                        case when substr(t.shift_day_key, 0, 8) =                                                                          " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else nvl(t.destroy_qty,0)  end destroy_qty,                " +
"                                                                                                                                           " +
"                       t.output_type,                                                                                                      " +
"                       substr(t.prod_id, 0, 9) as prod_id,                                                                                 " +
"                       substr(t.prod_id, 8, 15) as SHOP                                                                                    " +
"                  from daily_in_out_sum@ods2cf t                                                                                           " +
"                 where substr(t.prod_id, 0, 9) in (" + CF_product + ")                                                               " +
"                   and substr(t.shift_day_key, 0, 6) =                                                                                     " +
"                       substr(to_char(sysdate, 'yyyyMMdd'), 0, 6)) op1                                                                     " +
"         group by op1.output_type, op1.shop, op1.prod_id,op1.destroy_qty) tt                                                               ";
                                                                                                                                        

        sql = "select rownum,tt.* from (" + sql + ") tt";
        ds_temp = func.get_dataSet_access(sql, conn);

        GridView1.DataSource = ds_temp;
        GridView1.DataBind();

        doChart1(sql);

        return ds_temp;




    }



    public DataSet bind_data2()
    {
        string sql =
         " select tt3.* from( select tt2.shop,                                                                                              " +
"                                                                                                               " +
"        sum(tt2.begin_wip) as begin_wip,                                                                       " +
"        sum(tt2.in_qty)as in_qty,                                                                              " +
"        sum(tt2.begin_in)as begin_in,                                                                          " +
"        sum(tt2.scrap_total) as scrap_total,                                                                   " +
"        sum(tt2.end_wip) as end_wip,                                                                           " +
"        sum(tt2.out_qty) as out_qty,                                                                           " +
"        sum(tt2.destroy_qty) as destroy_qty,                                                                   " +
"        sum(tt2.scrap_end_out_destory)as scrap_end_out_destory,                                                " +
"        sum(tt2.diff) as diff,                                                                                 " +
"        tt2.output_type                                                                                        " +
"   from (                                                                                                      " +
"                                                                                                               " +
"         select rownum, tt.*                                                                                   " +
"           from (select replace(op1.shop, '.Fab1', '') as shop,                                                " +
"                         replace(op1.shop, '.Fab1', '') || '_' || op1.prod_id as shop_product,                 " +
"                         max(op1.shift_day) as shift_day,                                                      " +
"                         op1.prod_id,                                                                          " +
"                         sum(op1.begin_wip) as begin_wip,                                                      " +
"                         sum(op1.in_qty) as in_qty,                                                            " +
"                                                                                                               " +
"                         sum(op1.begin_wip) + sum(op1.in_qty) as begin_in,                                     " +
"                         sum(op1.scrap_total) as scrap_total,                                                  " +
"                         sum(op1.end_wip) as end_wip,                                                          " +
"                         sum(op1.out_qty) as out_qty,                                                          " +
"                         sum(op1.destroy_qty) as destroy_qty,                                                  " +
"                         sum(op1.scrap_total) + sum(op1.end_wip) +                                             " +
"                         sum(op1.out_qty) + sum(op1.destroy_qty) as scrap_end_out_destory,                     " +
"                                                                                                               " +
"                         sum(op1.begin_wip) + sum(op1.in_qty) -                                                " +
"                         sum(op1.scrap_total) - sum(op1.end_wip) -                                             " +
"                         sum(op1.out_qty) - sum(op1.destroy_qty) as diff,                                      " +
"                         op1.output_type                                                                       " +
"                    from (select substr(t.shift_day_key, 0, 8) as shift_day,                                   " +
"                                 case                                                                          " +
"                                   when substr(t.shift_day_key, 7, 2) = '01' then                              " +
"                                    t.dt_starttm_wip_qty                                                       " +
"                                   else                                                                        " +
"                                    0                                                                          " +
"                                 end begin_wip,                                                                " +
"                                 case                                                                          " +
"                                   when substr(t.shift_day_key, 0, 8) =                                        " +
"                                        substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                        " +
"                                    t.dt_starttm_wip_qty                                                       " +
"                                   else                                                                        " +
"                                    0                                                                          " +
"                                 end end_wip,                                                                  " +
"                                 t.dt_starttm_wip_qty as current_wip,                                          " +
"                                 case                                                                          " +
"                                   when substr(t.shift_day_key, 0, 8) =                                        " +
"                                        substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                        " +
"                                    0                                                                          " +
"                                   else                                                                        " +
"                                    t.in_qty                                                                   " +
"                                 end in_qty,                                                                   " +
"                                 case                                                                          " +
"                                   when substr(t.shift_day_key, 0, 8) =                                        " +
"                                        substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                        " +
"                                    0                                                                          " +
"                                   else                                                                        " +
"                                    t.out_qty                                                                  " +
"                                 end out_qty,                                                                  " +
"                                 case                                                                          " +
"                                   when substr(t.shift_day_key, 0, 8) =                                        " +
"                                        substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                        " +
"                                    0                                                                          " +
"                                   else                                                                        " +
"                                    (t.scrap_qty - t.unship_qty)                                               " +
"                                 end scrap_total,                                                              " +
"                                 case                                                                          " +
"                                   when substr(t.shift_day_key, 0, 8) =                                        " +
"                                        substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                        " +
"                                    0                                                                          " +
"                                   else                                                                        " +
"                                    t.destroy_qty                                                              " +
"                                 end destroy_qty,                                                              " +
"                                 t.output_type,                                                                " +
"                                 substr(t.prod_id, 0, 6) as prod_id,                                           " +
"                                 substr(t.prod_id, 8, 15) as SHOP                                              " +
"                            from daily_in_out_sum t                                                            " +
"                           where substr(t.prod_id, 0, 6) in                                                    " +
"                                 (" + Array_product + ")                                                                    " +
"                             and substr(t.shift_day_key, 0, 6) =                                               " +
"                                 substr(to_char(sysdate, 'yyyyMMdd'), 0, 6)) op1                               " +
"                   group by op1.output_type, op1.shop, op1.prod_id                                             " +
"                  union                                                                                        " +
"                  select substr(replace(op1.shop, '.Fab1', ''), 4, 5) as shop,                                 " +
"                         substr(replace(op1.shop, '.Fab1', ''), 4, 5) || '_' ||                                " +
"                         op1.prod_id as shop_product,                                                          " +
"                         max(op1.shift_day) as shift_day,                                                      " +
"                         op1.prod_id,                                                                          " +
"                         sum(op1.begin_wip) as begin_wip,                                                      " +
"                         sum(op1.in_qty) as in_qty,                                                            " +
"                                                                                                               " +
"                         sum(op1.begin_wip) + sum(op1.in_qty) as begin_in,                                     " +
"                         sum(op1.scrap_total) as scrap_total,                                                  " +
"                         sum(op1.end_wip) as end_wip,                                                          " +
"                         sum(op1.out_qty) as out_qty,                                                          " +
"                         sum(op1.destroy_qty) as destroy_qty,                                                  " +
"                         sum(op1.scrap_total) + sum(op1.end_wip) +                                             " +
"                         sum(op1.out_qty) as scrap_end_out_destory,                                            " +
"                                                                                                               " +
"                         sum(op1.begin_wip) + sum(op1.in_qty) -                                                " +
"                         sum(op1.scrap_total) - sum(op1.end_wip) -                                             " +
"                         sum(op1.out_qty) as diff,                                                             " +
"                         op1.output_type                                                                       " +
"                    from (select substr(t.shift_day_key, 0, 8) as shift_day,                                   " +
"                                 case                                                                          " +
"                                   when substr(t.shift_day_key, 7, 2) = '01' then                              " +
"                                    t.dt_starttm_wip_qty                                                       " +
"                                   else                                                                        " +
"                                    0                                                                          " +
"                                 end begin_wip,                                                                " +
"                                 case                                                                          " +
"                                   when substr(t.shift_day_key, 0, 8) =                                        " +
"                                        substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                        " +
"                                    t.dt_starttm_wip_qty                                                       " +
"                                   else                                                                        " +
"                                    0                                                                          " +
"                                 end end_wip,                                                                  " +
"                                 t.dt_starttm_wip_qty as current_wip,                                          " +
"                                 case                                                                          " +
"                                   when substr(t.shift_day_key, 0, 8) =                                        " +
"                                        substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                        " +
"                                    0                                                                          " +
"                                   else                                                                        " +
"                                    t.in_qty                                                                   " +
"                                 end in_qty,                                                                   " +
"                                 case                                                                          " +
"                                   when substr(t.shift_day_key, 0, 8) =                                        " +
"                                        substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                        " +
"                                    0                                                                          " +
"                                   else                                                                        " +
"                                    t.out_qty                                                                  " +
"                                 end out_qty,                                                                  " +
"                                 case                                                                          " +
"                                   when t.unship_qty is null then                                              " +
"                                    t.scrap_qty                                                                " +
"                                   else                                                                        " +
"                                    t.scrap_qty - t.unship_qty                                                 " +
"                                 end scrap_total,                                                              " +
"                                 case                                                                          " +
"                                   when substr(t.shift_day_key, 0, 8) =                                        " +
"                                        substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                        " +
"                                    0                                                                          " +
"                                   else                                                                        " +
"                                    nvl(t.destroy_qty, 0)                                                      " +
"                                 end destroy_qty,                                                              " +
"                                                                                                               " +
"                                 t.output_type,                                                                " +
"                                 substr(t.prod_id, 0, 9) as prod_id,                                           " +
"                                 substr(t.prod_id, 8, 15) as SHOP                                              " +
"                            from daily_in_out_sum@ods2cf t                                                     " +
"                           where substr(t.prod_id, 0, 9) in                                                    " +
"                                 (" + CF_product + ")                                                    " +
"                             and substr(t.shift_day_key, 0, 6) =                                               " +
"                                 substr(to_char(sysdate, 'yyyyMMdd'), 0, 6)) op1                               " +
"                   group by op1.output_type,                                                                   " +
"                            op1.shop,                                                                          " +
"                            op1.prod_id,                                                                       " +
"                            op1.destroy_qty) tt                                                                " +
"                                                                                                               " +
"         ) tt2                                                                                                 " +
"                                                                                                               " +
"         group by tt2.output_type, tt2.shop                                                                    " +
"                                                                                                               " +
"         order by diff desc   ) tt3                                                                                 ";





        sql = "select rownum,t.* from (" + sql + ")t  ";

        ds_temp = func.get_dataSet_access(sql, conn);

        GridView2.DataSource = ds_temp;
        GridView2.DataBind();



        return ds_temp;




    }

    public DataSet bind_data3()
    {
        string sql =
//" select replace(op1.shop, '.Fab1', '') as shop,                                               " +
//"                                                                                              " +
//"        max(op1.shift_day) as shift_day,                                                      " +
//"        op1.prod_id,                                                                          " +
//"        sum(op1.begin_wip) as begin_wip,                                                      " +
//"        sum(op1.in_qty) as in_qty,                                                            " +
//"        sum(op1.begin_wip) + sum(op1.in_qty) as begin_in,                                     " +
//"        sum(op1.scrap_total) as scrap_total,                                                  " +
//"        sum(op1.end_wip) as end_wip,                                                          " +
//"                                                                                              " +
//"        sum(op1.out_qty) as out_qty,                                                          " +
//"        sum(op1.scrap_total) + sum(op1.end_wip) + sum(op1.out_qty) as scrap_end_out,          " +
//"        sum(op1.begin_wip) + sum(op1.in_qty) - sum(op1.scrap_total) -                         " +
//"        sum(op1.end_wip) - sum(op1.out_qty) as diff,                                          " +
//"                                                                                              " +
//"        op1.output_type                                                                       " +
//"                                                                                              " +
//"   from (                                                                                     " +
//"                                                                                              " +
//"         select substr(t.shift_day_key, 0, 8) as shift_day,                                   " +
//"                 case                                                                         " +
//"                   when substr(t.shift_day_key, 7, 2) = '01' then                             " +
//"                    t.dt_starttm_wip_qty                                                      " +
//"                   else                                                                       " +
//"                    0                                                                         " +
//"                 end begin_wip,                                                               " +
//"                 case                                                                         " +
//"                   when substr(t.shift_day_key, 0, 8) =                                       " +
//"                        substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                       " +
//"                    t.dt_starttm_wip_qty                                                      " +
//"                   else                                                                       " +
//"                    0                                                                         " +
//"                 end end_wip,                                                                 " +
//"                 t.dt_starttm_wip_qty as current_wip,                                         " +
//"                 t.in_qty,                                                                    " +
//"                 t.out_qty,                                                                   " +
//"                 (t.scrap_qty - t.unship_qty) as scrap_total,                                 " +
//"                                                                                              " +
//"                 t.output_type,                                                               " +
//"                 substr(t.prod_id, 0, 6) as prod_id,                                          " +
//"                 substr(t.prod_id, 8, 15) as SHOP                                             " +
//"           from daily_in_out_sum t                                                            " +
//"                                                                                              " +
//"          where substr(t.prod_id, 0, 6) in                                                    " +
//"                (" + Array_product + ")" +
//"               /* ('200D0T', '200E0T', '300A0T', '970A0T', '970D0T')*/                        " +
//"            and substr(t.shift_day_key, 0, 6) =                                               " +
//"                substr(to_char(sysdate, 'yyyyMMdd'), 0, 6)                                    " +
//"                                                                                              " +
//"         ) op1                                                                                " +
//"                                                                                              " +
//"  group by op1.output_type, op1.shop, op1.prod_id                                             " +
//"                                                                                              " +
//" union                                                                                        " +
//"                                                                                              " +
//" select substr(replace(op1.shop, '.Fab1', ''), 4, 5) as shop,                                 " +
//"        max(op1.shift_day) as shift_day,                                                      " +
//"                                                                                              " +
//"        op1.prod_id,                                                                          " +
//"        sum(op1.begin_wip) as begin_wip,                                                      " +
//"        sum(op1.in_qty) as in_qty,                                                            " +
//"        sum(op1.begin_wip) + sum(op1.in_qty) as begin_in,                                     " +
//"        sum(op1.scrap_total) as scrap_total,                                                  " +
//"        sum(op1.end_wip) as end_wip,                                                          " +
//"                                                                                              " +
//"        sum(op1.out_qty) as out_qty,                                                          " +
//"        sum(op1.scrap_total) + sum(op1.end_wip) + sum(op1.out_qty) as scrap_end_out,          " +
//"        sum(op1.begin_wip) + sum(op1.in_qty) - sum(op1.scrap_total) -                         " +
//"        sum(op1.end_wip) - sum(op1.out_qty) as diff,                                          " +
//"                                                                                              " +
//"        op1.output_type                                                                       " +
//"   from (select substr(t.shift_day_key, 0, 8) as shift_day,                                   " +
//"                case                                                                          " +
//"                  when substr(t.shift_day_key, 7, 2) = '01' then                              " +
//"                   t.dt_starttm_wip_qty                                                       " +
//"                  else                                                                        " +
//"                   0                                                                          " +
//"                end begin_wip,                                                                " +
//"                case                                                                          " +
//"                  when substr(t.shift_day_key, 0, 8) =                                        " +
//"                       substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                        " +
//"                   t.dt_starttm_wip_qty                                                       " +
//"                  else                                                                        " +
//"                   0                                                                          " +
//"                end end_wip,                                                                  " +
//"                t.dt_starttm_wip_qty as current_wip,                                          " +
//"                t.in_qty,                                                                     " +
//"                t.out_qty,                                                                    " +
//"                case                                                                          " +
//"                  when t.unship_qty is null then                                              " +
//"                   t.scrap_qty                                                                " +
//"                  else                                                                        " +
//"                   t.scrap_qty - t.unship_qty                                                 " +
//"                end scrap_total,                                                              " +
//"                t.output_type,                                                                " +
//"                substr(t.prod_id, 0, 9) as prod_id,                                           " +
//"                substr(t.prod_id, 8, 15) as SHOP                                              " +
//"           from daily_in_out_sum@ods2cf t                                                     " +
//"          where substr(t.prod_id, 0, 9) in (" + CF_product + ")                         " +
//"            and substr(t.shift_day_key, 0, 6) =                                               " +
//"                substr(to_char(sysdate, 'yyyyMMdd'), 0, 6)) op1                               " +
//"  group by op1.output_type, op1.shop, op1.prod_id                                             ";

"select rownum, tt.*                                                                                                                        " +
"  from (select replace(op1.shop, '.Fab1', '') as shop,                                                                                     " +
"               replace(op1.shop, '.Fab1', '') || '_' || op1.prod_id as shop_product,                                                       " +
"               max(op1.shift_day) as shift_day,                                                                                            " +
"               op1.prod_id,                                                                                                                " +
"               sum(op1.begin_wip) as begin_wip,                                                                                            " +
"               sum(op1.in_qty) as in_qty,                                                                                                  " +
"                                                                                                                                           " +
"               sum(op1.begin_wip) + sum(op1.in_qty) as begin_in,                                                                           " +
"               sum(op1.scrap_total) as scrap_total,                                                                                        " +
"               sum(op1.end_wip) as end_wip,                                                                                                " +
"               sum(op1.out_qty) as out_qty,                                                                                                " +
"               sum(op1.destroy_qty) as destroy_qty,                                                                                        " +
"               sum(op1.scrap_total) + sum(op1.end_wip) + sum(op1.out_qty)+sum(op1.destroy_qty) as scrap_end_out_destory,                   " +
"                                                                                                                                           " +
"               sum(op1.begin_wip) + sum(op1.in_qty) - sum(op1.scrap_total) -                                                               " +
"               sum(op1.end_wip) - sum(op1.out_qty)-sum(op1.destroy_qty) as diff,                                                           " +
"               op1.output_type                                                                                                             " +
"          from (select substr(t.shift_day_key, 0, 8) as shift_day,                                                                         " +
"                       case                                                                                                                " +
"                         when substr(t.shift_day_key, 7, 2) = '01' then                                                                    " +
"                          t.dt_starttm_wip_qty                                                                                             " +
"                         else                                                                                                              " +
"                          0                                                                                                                " +
"                       end begin_wip,                                                                                                      " +
"                       case                                                                                                                " +
"                         when substr(t.shift_day_key, 0, 8) =                                                                              " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                                                              " +
"                          t.dt_starttm_wip_qty                                                                                             " +
"                         else                                                                                                              " +
"                          0                                                                                                                " +
"                       end end_wip,                                                                                                        " +
"                       t.dt_starttm_wip_qty as current_wip,                                                                                " +
"                       case when substr(t.shift_day_key, 0, 8) =                                                                           " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else t.in_qty end in_qty,                                  " +
"                       case when substr(t.shift_day_key, 0, 8) =                                                                           " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else t.out_qty end out_qty,                                " +
"                        case when substr(t.shift_day_key, 0, 8) =                                                                          " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else (t.scrap_qty - t.unship_qty) end scrap_total,         " +
"                        case when substr(t.shift_day_key, 0, 8) =                                                                          " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else t.destroy_qty end destroy_qty,                        " +
"                       t.output_type,                                                                                                      " +
"                       substr(t.prod_id, 0, 6) as prod_id,                                                                                 " +
"                       substr(t.prod_id, 8, 15) as SHOP                                                                                    " +
"                  from daily_in_out_sum t                                                                                                  " +
"                 where substr(t.prod_id, 0, 6) in                                                                                          " +
"                       (" + Array_product + "  )                              " +
"                       and substr(t.shift_day_key, 0, 6) =                                                                                 " +
"                       substr(to_char(sysdate, 'yyyyMMdd'), 0, 6)) op1                                                                     " +
"         group by op1.output_type, op1.shop, op1.prod_id                                                                                   " +
"        union                                                                                                                              " +
"        select substr(replace(op1.shop, '.Fab1', ''), 4, 5) as shop,                                                                       " +
"               substr(replace(op1.shop, '.Fab1', ''), 4, 5) || '_' ||                                                                      " +
"               op1.prod_id as shop_product,                                                                                                " +
"               max(op1.shift_day) as shift_day,                                                                                            " +
"               op1.prod_id,                                                                                                                " +
"               sum(op1.begin_wip) as begin_wip,                                                                                            " +
"               sum(op1.in_qty) as in_qty,                                                                                                  " +
"                                                                                                                                           " +
"               sum(op1.begin_wip) + sum(op1.in_qty) as begin_in,                                                                           " +
"               sum(op1.scrap_total) as scrap_total,                                                                                        " +
"               sum(op1.end_wip) as end_wip,                                                                                                " +
"               sum(op1.out_qty) as out_qty,                                                                                                " +
"                sum(op1.destroy_qty) as destroy_qty,                                                                                       " +
"               sum(op1.scrap_total) + sum(op1.end_wip) + sum(op1.out_qty) as scrap_end_out_destory,                                        " +
"                                                                                                                                           " +
"               sum(op1.begin_wip) + sum(op1.in_qty) - sum(op1.scrap_total) -                                                               " +
"               sum(op1.end_wip) - sum(op1.out_qty) as diff,                                                                                " +
"               op1.output_type                                                                                                             " +
"          from (select substr(t.shift_day_key, 0, 8) as shift_day,                                                                         " +
"                       case                                                                                                                " +
"                         when substr(t.shift_day_key, 7, 2) = '01' then                                                                    " +
"                          t.dt_starttm_wip_qty                                                                                             " +
"                         else                                                                                                              " +
"                          0                                                                                                                " +
"                       end begin_wip,                                                                                                      " +
"                       case                                                                                                                " +
"                         when substr(t.shift_day_key, 0, 8) =                                                                              " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then                                                              " +
"                          t.dt_starttm_wip_qty                                                                                             " +
"                         else                                                                                                              " +
"                          0                                                                                                                " +
"                       end end_wip,                                                                                                        " +
"                       t.dt_starttm_wip_qty as current_wip,                                                                                " +
"                       case when substr(t.shift_day_key, 0, 8) =                                                                           " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else t.in_qty end in_qty,                                  " +
"                       case when substr(t.shift_day_key, 0, 8) =                                                                           " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else t.out_qty end out_qty,                                " +
"                       case                                                                                                                " +
"                         when t.unship_qty is null then                                                                                    " +
"                          t.scrap_qty                                                                                                      " +
"                         else                                                                                                              " +
"                          t.scrap_qty - t.unship_qty                                                                                       " +
"                       end scrap_total,                                                                                                    " +
"                        case when substr(t.shift_day_key, 0, 8) =                                                                          " +
"                              substr(to_char(sysdate, 'yyyyMMdd'), 0, 8) then 0 else nvl(t.destroy_qty,0)  end destroy_qty,                " +
"                                                                                                                                           " +
"                       t.output_type,                                                                                                      " +
"                       substr(t.prod_id, 0, 9) as prod_id,                                                                                 " +
"                       substr(t.prod_id, 8, 15) as SHOP                                                                                    " +
"                  from daily_in_out_sum@ods2cf t                                                                                           " +
"                 where substr(t.prod_id, 0, 9) in (" + CF_product + ")                                                               " +
"                   and substr(t.shift_day_key, 0, 6) =                                                                                     " +
"                       substr(to_char(sysdate, 'yyyyMMdd'), 0, 6)) op1                                                                     " +
"         group by op1.output_type, op1.shop, op1.prod_id,op1.destroy_qty) tt                                                               ";
                                                                                                                                        
         
  


        sql = "select rownum,tt.* from (" + sql + ") tt";
        ds_temp = func.get_dataSet_access(sql, conn);

        GridView3.DataSource = ds_temp;
        GridView3.DataBind();

       // doChart1(string sql);

        return ds_temp;




    }


    private void doChart1(string sqlX)
    {


        //clear all point
        string sql_str1 = sqlX;
        foreach (Series series in Chart1.Series)
        {
            series.Points.Clear();
        }
        //Chart1.Series.Clear();

        DataSet ds22 = new DataSet();
        DataSet ds33 = new DataSet();

        // string[] onduty_type = { "詢問問題", "操作錯誤(MO)", "系統BUG", "設備", "整合Modeling問題" };





        //string sql_product;



        ds22 = func.get_dataSet_access(sql_str1, conn);




        //ds = dbutil.GetDataset(sql_grade);


        DataView dv_times = ds22.Tables[0].DefaultView;

        dv_times.RowFilter = " ";

        //dv_FAB.Sort = "";
        // DataTable FABTable;
        //將重複的data distinct出來
        // FABTable = dv_FAB.ToTable(true, "FAB");
        //dv_FAB.Sort = "";

        //string sql_current_week = "";

        //sql_current_week = sql_current_week + "select * from (                                                                                                     ";
        //sql_current_week = sql_current_week + "select distinct('W'||substr(to_char(t.calltime,'yyyy'),3,2)||to_char(t.calltime+7,'WW')) as week_num from  onduty t   ";
        //sql_current_week = sql_current_week + "                                                                                                  ";
        //sql_current_week = sql_current_week + "order by week_num desc                                                                                              ";
        //sql_current_week = sql_current_week + ")                                                                                                                   ";
        //sql_current_week = sql_current_week + "where rownum<2      ";

        // DataSet da_current_week = new DataSet();
        // da_current_week = get_dataSet_access(sql_current_week);



        for (int x = 0; x <= dv_times.Count - 1; x++)
        {

            Chart1.Series["期初WIP+Input"].Points.AddXY(dv_times[x]["shop_product"], Convert.ToDouble(dv_times[x]["begin_in"]));
            Chart1.Series["Output+Scrap+期末WIP+Destroy"].Points.AddXY(dv_times[x]["shop_product"], Convert.ToDouble(dv_times[x]["scrap_end_out_destory"]));

        }



    }





    public string combine_Array_to_string(DataSet ds_tempX)
    {
        string initial = "";

        ds_temp1 = ds_tempX;



        for (int i = 0; i <= ds_temp1.Tables[0].Rows.Count - 1; i++)
        {
            if (i == 0)
            {
                initial = "'" + ds_temp1.Tables[0].Rows[i]["product"].ToString() + "'";
            }
            else
            {
                initial = initial + "," + "'" + ds_temp1.Tables[0].Rows[i]["product"].ToString() + "'";


            }

        }


        return initial;


    }




    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }

            //ImageButton btnDel = new ImageButton();
            //btnDel = (ImageButton)e.Row.FindControl("btnDel");
            ////btnDel.CommandArgument = ((Label)e.Row.FindControl("lblDefectType")).Text;
            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【MODEL_NAME】:" + ((DataRowView)e.Row.DataItem)["MODEL_NAME"] + " 【TOOL_ID】:" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');";




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

        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }

            //ImageButton btnDel = new ImageButton();
            //btnDel = (ImageButton)e.Row.FindControl("btnDel");
            ////btnDel.CommandArgument = ((Label)e.Row.FindControl("lblDefectType")).Text;
            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【MODEL_NAME】:" + ((DataRowView)e.Row.DataItem)["MODEL_NAME"] + " 【TOOL_ID】:" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');";


           

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

        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }

            //ImageButton btnDel = new ImageButton();
            //btnDel = (ImageButton)e.Row.FindControl("btnDel");
            ////btnDel.CommandArgument = ((Label)e.Row.FindControl("lblDefectType")).Text;
            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【MODEL_NAME】:" + ((DataRowView)e.Row.DataItem)["MODEL_NAME"] + " 【TOOL_ID】:" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');";

            #region link
            string link = DataBinder.Eval(e.Row.DataItem, "diff").ToString();
            string link_prod_id = DataBinder.Eval(e.Row.DataItem, "prod_id").ToString();
            string link_shop = DataBinder.Eval(e.Row.DataItem, "shop").ToString();
           
            if (link != null && link.Length > 0)
            {
                 string today= DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd").Replace("/", "");
                
                // "FindControl" borrowed directly from DOK 
                HyperLink hlParent = (HyperLink)e.Row.FindControl("hlParent");
                if (hlParent != null)
                {
                    // Do some manipulation of the link value 
                   // string newLink = "http://" + link;
                    string newLink = "TP_balance_detail.aspx?date1=" + today + "&shop=" + link_shop + "&prodid=" + link_prod_id + "";


                    // Set the Url of the HyperLink 
                    hlParent.NavigateUrl = newLink;
                }
            }


            #endregion 


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

        }
    }



    private static string combine_List_box(ListBox Source_list)
    {

        ListBox Target = new ListBox();
        Target = Source_list;
        string initial = "";

        if (Target.SelectedItem is Nullable)
        {

            initial = "'" + "'";
        }

        else
        {

            //string initial = "";
            for (int i = 0; i <= Target.Items.Count - 1; i++)
            {
                if (i == 0)
                {
                    initial = initial + "'" + Target.Items[i] + "'";
                }

                else
                {

                    initial = initial + ",'" + Target.Items[i] + "'";
                }


            }


        }
        if (initial == "")
        {
            initial = "''";
        }
        return initial;


    }
    private void ExportExcel(GridView SeriesValuesDataGrid)
    {
        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader("content-disposition", "attachment;filename=Night_Inspec.xls");

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
