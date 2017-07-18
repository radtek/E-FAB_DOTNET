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
using System.Drawing;

public partial class chairman_cycle_time : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_T2PRPT"];
   // string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_PARS1_OLE_ONDUTY"];
    string subject = "";
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";

    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    string today_detail1 = DateTime.Now.AddDays(+0).ToString("yyyyMMdd HH:mm:ss");
    Int32 row_num1 = 0;
    Int32 column_num1 = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.Visible = false;

//        #region old sql
//        sql_temp = @"select ot4.shift_date,ot4.product,ot4.plant,ot4.fab,ot4.shop,ot4.target,ot4.act_cycletime,ot4.diff from ( 
//
//select ot3.shift_date,
//      case when ot3.shop='T1ARY' then 'TFT' 
//       when ot3.shop='T1CEL' then 'TFT' 
//       when ot3.shop='T1CF' then 'TFT' 
//        when ot3.shop='T2ARY' then 'TFT' 
//       when ot3.shop='T2CEL' then 'TFT' 
//       when ot3.shop='T2CF' then 'TFT' 
//      else 'SG'
//      end as product,
//      case when ot3.shop='T1ARY' then 'T1' 
//       when ot3.shop='T1CEL' then 'T1' 
//       when ot3.shop='T1CF' then 'T1' 
//        when ot3.shop='T2ARY' then 'T2' 
//       when ot3.shop='T2CEL' then 'T2' 
//       when ot3.shop='T2CF' then 'T2' 
//       when ot3.shop='C1' then 'C1' 
//       when ot3.shop='T0ARY' then 'C2' 
//       when ot3.shop='T0CEL' then 'C2' 
//        when ot3.shop='C3' then 'C3' 
//        when ot3.shop='C5' then 'C5' 
//         when ot3.shop='LAM' then 'LAM' 
//          when ot3.shop='WIS' then 'WIS' 
//            when ot3.shop='FCTP' then 'FCTP' 
//      end as plant,
//       case when ot3.shop='T1ARY' then 'ARRAY' 
//       when ot3.shop='T1CEL' then 'CELL' 
//       when ot3.shop='T1CF' then 'CF' 
//        when ot3.shop='T2ARY' then 'ARRAY' 
//       when ot3.shop='T2CEL' then 'CELL' 
//       when ot3.shop='T2CF' then 'CF' 
//       when ot3.shop='C1' then 'C1' 
//       when ot3.shop='T0ARY' then 'ARRAY' 
//       when ot3.shop='T0CEL' then 'CELL' 
//        when ot3.shop='C3' then 'C3' 
//        when ot3.shop='C5' then 'C5' 
//         when ot3.shop='LAM' then 'LAM' 
//          when ot3.shop='WIS' then 'WIS' 
//            when ot3.shop='FCTP' then 'FCTP' 
//      end as FAB,
//      ot3.shop,
//      ot3.act_cycletime,
//      ot3.target,
//      ot3.diff
//      
//  from (
//
//
//select ot1.*,ot2.target,(ot2.target -ot1.act_cycletime) as diff from (
//select substr(shift_date,0,6) as shift_date,shop,round(
//(case when sum(t.qty) <=0 then 0 else sum(t.cycletime*t.qty)/sum(t.qty) end )
//,2
//) as act_cycletime from daily_in_out_sum t
//where t.shift_date like to_char(sysdate,'yyyyMM')||'%' and t.data_type='OUT'
//
//group by  substr(shift_date,0,6),shop
//
//) ot1,(select * from cycle_time_conf)ot2
//where ot1.shop=ot2.plant_fab(+)
//
//) ot3
//
//) ot4
//
//order by case when ot4.product='TFT' then 1 else 2  end
//, case when ot4.PLANT='T1' then 1 
//       when ot4.PLANT='T2' then 2
//       when ot4.PLANT='C1' then 3
//       when ot4.PLANT='C2' then 4
//       when ot4.PLANT='C3' then 5
//       when ot4.PLANT='C5' then 6
//       when ot4.PLANT='LAM' then 7
//       when ot4.PLANT='WIS' then 8
//       when ot4.PLANT='FCTP' then 9
//  end ,
//
//case when ot4.shop='T1' then 1 
//       when ot4.shop='T1ARY' then 2
//       when ot4.shop='T1CF' then 3
//       when ot4.shop='T1CEL' then 4
//       when ot4.shop='T2ARY' then 5
//       when ot4.shop='T2CF' then 6
//       when ot4.shop='T2CEL' then 7
//       when ot4.shop='T0ARY' then 8
//       when ot4.shop='T0CEL' then 9
//       when ot4.shop='C3' then 10
//       when ot4.shop='C5' then 11
//       when ot4.shop='LAM' then 12
//       when ot4.shop='WIS' then 13
//       when ot4.shop='FCTP' then 13
//  end 
//
//
//
//";
//        #endregion

        sql_temp = @"
select ot5.shift_date,ot5.product,ot5.plant,ot5.fab,ot5.shop,ot5.product_type,ot5.target,ot5.act_cycletime,ot5.diff from 
(
select ot4.shift_date,ot4.product,ot4.plant,ot4.fab,ot4.shop,ot4.product_type,ot4.target,ot4.act_cycletime,ot4.diff from ( 

select ot3.shift_date,
      case when ot3.shop='T1ARY' then 'TFT' 
       when ot3.shop='T1CEL' then 'TFT' 
       when ot3.shop='T1CF' then 'TFT' 
        when ot3.shop='T2ARY' then 'TFT' 
       when ot3.shop='T2CEL' then 'TFT' 
       when ot3.shop='T2CF' then 'TFT' 
      else 'SG'
      end as product,
      case when ot3.shop='T1ARY' then 'T1' 
       when ot3.shop='T1CEL' then 'T1' 
       when ot3.shop='T1CF' then 'T1' 
        when ot3.shop='T2ARY' then 'T2' 
       when ot3.shop='T2CEL' then 'T2' 
       when ot3.shop='T2CF' then 'T2' 
       when ot3.shop='C1' then 'C1' 
       when ot3.shop='T0ARY' then 'C2' 
       when ot3.shop='T0CEL' then 'C2' 
        when ot3.shop='C3' then 'C3' 
        when ot3.shop='C5' then 'C5' 
         when ot3.shop='LAM' then 'LAM' 
          when ot3.shop='WIS' then 'WIS' 
            when ot3.shop='FCTP' then 'FCTP' 
      end as plant,
       case when ot3.shop='T1ARY' then 'ARRAY' 
       when ot3.shop='T1CEL' then 'CELL' 
       when ot3.shop='T1CF' then 'CF' 
        when ot3.shop='T2ARY' then 'ARRAY' 
       when ot3.shop='T2CEL' then 'CELL' 
       when ot3.shop='T2CF' then 'CF' 
       when ot3.shop='C1' then 'C1' 
       when ot3.shop='T0ARY' then 'ARRAY' 
       when ot3.shop='T0CEL' then 'CELL' 
        when ot3.shop='C3' then 'C3' 
        when ot3.shop='C5' then 'C5' 
         when ot3.shop='LAM' then 'LAM' 
          when ot3.shop='WIS' then 'WIS' 
            when ot3.shop='FCTP' then 'FCTP' 
      end as FAB,
      ot3.shop,
      ot3.product_type,
      ot3.act_cycletime,
      ot3.target,
      ot3.diff
      
  from (


select ot1.*,case when ot2.target is null then '0' else ot2.target end target,(ot2.target -ot1.act_cycletime) as diff from (
select substr(shift_date,0,6) as shift_date,shop,product_type,round(
(case when sum(nvl(t.qty,0)+nvl(t.unship_cnt,0)) <=0 then 0     
     
     when t.SHOP<>'' then  sum(nvl(t.cycletime,0)*(nvl(t.qty,0)+nvl(t.unship_cnt,0)))/sum(nvl(t.qty,0)+nvl(t.unship_cnt,0))
        else 
      sum(nvl(t.cycletime,0)*nvl(t.qty,0))/sum(nvl(t.qty,0))
     
end )
,2
) as act_cycletime from 

(select tt.*,case when  tv.product is null then tt.shop else tv.product end product_type from daily_in_out_sum tt, (

select prod_name,shop,producttype,prod_size,panel_qty,product from (

select t.prod_name,case when  substr(t.shop,0,2)='T0' then 'T0ARY'
                             else 'T1ARY' end as shop
                  ,'Product' as producttype
                  ,'' as prod_size
                  ,'' as panel_qty
                  ,t.section as product

 from t01ary_product_setting_v@dw2t2pcgw t
where t.section in ('TFT','METBRI','ITOBRI','IPS','TP')

union all

select * from daily_product_tp_v t

)ot1

group by prod_name,shop,producttype,prod_size,panel_qty,product



) tv
where tt.shift_date like to_char(sysdate,'yyyyMM')||'%' and tt.data_type='OUT'
and tt.prod_name=tv.PROD_NAME(+)
and nvl(tt.cycletime,0) >0
and (nvl(tt.qty,0)+ nvl(tt.unship_cnt,0)) >0
) t
where t.shift_date like to_char(sysdate,'yyyyMM')||'%' and t.data_type='OUT'

group by  substr(shift_date,0,6),shop,product_type

) ot1,(select * from cycle_time_conf)ot2
where ot1.shop=ot2.plant_fab(+)
    and  ot1.product_type=ot2.product_type(+)

) ot3

) ot4

order by case when ot4.product='TFT' then 1 else 2  end
, case when ot4.PLANT='T1' then 1 
       when ot4.PLANT='T2' then 2
       when ot4.PLANT='C1' then 3
       when ot4.PLANT='C2' then 4
       when ot4.PLANT='C3' then 5
       when ot4.PLANT='C5' then 6
       when ot4.PLANT='LAM' then 7
       when ot4.PLANT='WIS' then 8
       when ot4.PLANT='FCTP' then 9
       else 10
  end ,

case when ot4.shop='T1' then 1 
       when ot4.shop='T1ARY' then 2
       when ot4.shop='T1CF' then 3
       when ot4.shop='T1CEL' then 4
       when ot4.shop='T2ARY' then 5
       when ot4.shop='T2CF' then 6
       when ot4.shop='T2CEL' then 7
       when ot4.shop='T0ARY' then 8
       when ot4.shop='T0CEL' then 9
       when ot4.shop='C3' then 10
       when ot4.shop='C5' then 11
       when ot4.shop='LAM' then 12
       when ot4.shop='WIS' then 13
       when ot4.shop='FCTP' then 13
       else 14
  end ,
  
  case when ot4.product_type='IPS' then 1
       when ot4.product_type='TP' then 2
       when ot4.product_type='TFT' then 3
       when ot4.product_type='METBRI' then 4
       when ot4.product_type='ITOBRI' then 5
when ot4.product_type='A' then 6
when ot4.product_type='NONA' then 7
       else 8
  end

) ot5

where ot5.target>0











";

        ds_temp1 = func.get_dataSet_access(sql_temp, conn);

      
        GridView1.DataSource = ds_temp1.Tables[0];
        GridView1.DataBind();



        DT1 = func.Table_transport1(ds_temp1.Tables[0]);

        column_num1 = DT1.Columns.Count;

        GridView2.DataSource = DT1;
        GridView2.DataBind();
        GridView2.HeaderRow.Visible = false;

//        sql_temp1 = @"select ot1.shift_date,ot1.shop,ot1.cycletime,ot2.target from (
//
//
//select  t.shift_date,t.shop,round((case when sum(t.cycletime*t.qty)/sum(t.qty)  is null then 0 else sum(t.cycletime*t.qty)/sum(t.qty) end ),2) as cycletime from daily_in_out_sum t
//where t.shift_date like  to_char(sysdate-30,'yyyyMM')||'%' and t.data_type='OUT'
//
//group by t.shift_date,t.shop
//)ot1,
//(
//
//select * from cycle_time_conf
//) ot2
//
//where ot1.shop=ot2.plant_fab(+)";

        //ds_temp2 = func.get_dataSet_access(sql_temp1, conn);




        //doChart();

       
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        Double diff = 0;

        string smallblack = "";

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

           
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {

                if (e.Row.Cells[0].Text.Equals("DIFF"))
                {

                    diff = Convert.ToDouble(e.Row.Cells[i].Text.ToString());





                    if (diff < 0)
                    {
                        e.Row.Cells[i].ForeColor = Color.Red;
                    
                    }
                       

                }  
                
                
                
                if (e.Row.Cells[0].Text.Equals("SHOP"))
                {


                  
                
                  
                   // window.open('test.htm','pop','scrollbars=no,toolbar=no,menubar=no,location=no,width=400,height=400,top=10,left=100')
                    string sShop = string.Empty;
                
                    sShop = e.Row.Cells[i].Text;

                    string strJavaScript = string.Empty;  

                    // strJavaScript = "window.open('OpenPage.aspx?,'height=450,width=450,top=300,left=300,scrollbars=yes');";  

                    
            
                     
                    
                 

                
                 
                    HtmlAnchor haLot = new HtmlAnchor();
                   // haLot.HRef = "#";
                    //haLot.HRef = "cycle_time_Report_DrillDown.aspx?shop=" + sShop;
                    haLot.Target = "_BLANK";
                    
                    haLot.HRef = "javascript:window.open('cycle_time_Report_DrillDown.aspx?shop=" + sShop + "', '_blank', 'height=800, width=800, left=0, top=0, location=no, menubar=no, resizable=yes, scrollbars=yes, titlebar=no, toolbar=no', true); window.top.open('','_self');window.top.close(this);";
                    //haLot.Attributes.Add("Onclick", "DrillDownClick('" + sShop + "'); return false;");
                    haLot.InnerHtml = string.Format("<font style=\"font-weight: bold; color: blue\">{0}</font>", e.Row.Cells[i].Text);
                  
                
                   
                  
                   

                    e.Row.Cells[i].Controls.Add(haLot);


                
                }

             
             

                //e.Item.Cells[i].Text = "<a href='#' style='text-decoration: underline; font-weight: bold; color: blue;' onclick=\"javascript:CVP_DrillDownClick('" + e.Item.Cells[i].Text + "');\">" + e.Item.Cells[i].Text + "</a>";
            }

            //for (int j = 14; j < e.Row.Cells.Count; j++)
            //if (e.Row.Cells[0].Text.Equals("PLANT"))
            //{





            //    // window.open('test.htm','pop','scrollbars=no,toolbar=no,menubar=no,location=no,width=400,height=400,top=10,left=100')
            //    string sShop = string.Empty;

            //    sShop = e.Row.Cells[j].Text;

            //    string strJavaScript = string.Empty;

            //    // strJavaScript = "window.open('OpenPage.aspx?,'height=450,width=450,top=300,left=300,scrollbars=yes');";  









            //    HtmlAnchor haLot = new HtmlAnchor();
            //    // haLot.HRef = "#";
            //    //haLot.HRef = "cycle_time_Report_DrillDown.aspx?shop=" + sShop;
            //    haLot.Target = "_BLANK";

            //    haLot.HRef = "javascript:window.open('cycle_time_Report_DrillDown.aspx?shop=" + sShop + "', '_blank', 'height=800, width=800, left=0, top=0, location=no, menubar=no, resizable=yes, scrollbars=yes, titlebar=no, toolbar=no', true); window.top.open('','_self');window.top.close(this);";
            //    //haLot.Attributes.Add("Onclick", "DrillDownClick('" + sShop + "'); return false;");
            //    haLot.InnerHtml = string.Format("<font style=\"font-weight: bold; color: blue\">{0}</font>", e.Row.Cells[j].Text);






            //    e.Row.Cells[j].Controls.Add(haLot);



            //}
                
            
            
            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('蝣箄芸芷文? 箖tock_id?" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 穊nd Time?" + ((DataRowView)e.Row.DataItem)["date1"] + "箖N?" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




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
            // e.Row.Cells[2].Style.Add("background-color", "#FFFFB3"); 

            //if (Convert.ToDouble(pp) > priceX) 
            // e.Row.Cells[4].Style.Add("background-color", "#FF9DFF"); 


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            //if (e.Row.RowIndex != -1)
            //{
            //    int RN = e.Row.RowIndex + 1;
            //    e.Row.Cells[1].Text = RN.ToString();
            //}

            //Int32 counter = 1;

            //for (int j = 1; j <= e.Row.Cells.Count - 1; j++)
            //{
              
            //    if ((e.Row.Cells[j].Text.Equals(e.Row.Cells[j - 1].Text)))
            //    {
            //        counter++;

                 
            //        e.Row.Cells[j-counter+1].ColumnSpan=counter;

            //        e.Row.Cells[j].Visible = false;
                    
                    
                   
            //        //e.Row.Cells.Remove(e.Row.Cells[j]);


            //    }
            //    else
            //    {
            //        counter = 1;
            //        e.Row.Cells[j].ColumnSpan = counter;

            //    }


            //}





        }
    } 


//    public void DisplayMultiDataTable()
//    {
//        string sSql_Shop = @" select distinct t.shop from mps_pp t
//                                where t.shop like '{0}' || '%'
//                                order by shop ";
//        sSql_Shop = string.Format(sSql_Shop, Shop_Transfer(sShop));

//        DataTable dt_shop = m_objDB.ExecuteDataSet(sSql_Shop).Tables[0];

//        //reportBase蓪unction AddControlInTable緧緛?撜斢撱蓳ontrol憡table銝剜槫末
//        for (int i = 0; i < dt_shop.Rows.Count; i++)
//        {
//            DataTable dt = this.Get_Chart_DataTable(dt_shop.Rows[i]["shop"].ToString());
//            reportBase.AddControlInTable(this.Table1, this.DoChart(dt, "IN"), OrderBy.Row, 2, Align.Top);
//            reportBase.AddControlInTable(this.Table1, this.DoChart(dt, "OUT"), OrderBy.Row, 2, Align.Top);
//            reportBase.AddControlInTable(this.Table1, this.DoGrid(dt, "IN"), OrderBy.Row, 2, Align.Top);
//            reportBase.AddControlInTable(this.Table1, this.DoGrid(dt, "OUT"), OrderBy.Row, 2, Align.Top);

//        }
//    }



    //private void doChart()
    //{


    //    //clear all point 
    //    //foreach (Series series in Chart1.Series)
    //    //{
    //    //    series.Points.Clear();
    //    //}
    //    //Chart1.Series.Clear(); 

    //    DataSet ds22 = new DataSet();

    //    string[] arr_shop = { "T0Array", "T0Cell", "T1Array", "T1Cell", "T1CF" };

    //    string sql_grade = "";
    //    #region t2 sample

    //    string sSql_IN = this.Get_Input_Grid_SQL();
    //    string sSql_OUT = this.Get_Output_Grid_SQL();
    //    DataTable dt_in = m_objDB.ExecuteDataSet(sSql_IN).Tables[0];
    //    DataTable dt_out = m_objDB.ExecuteDataSet(sSql_OUT).Tables[0];
    //    DataView dv_in = dt_in.DefaultView;
    //    DataView dv_out = dt_out.DefaultView;

    //    dv_in.RowFilter = "shop in ('C2$ARY','C2$CEL','C3$C3','C5$C5','FCTP$FCTP','LAM$LAM','WIS$WIS')";
    //    dv_out.RowFilter = "shop in ('C2$ARY','C2$CEL','C3$C3','C5$C5','FCTP$FCTP','LAM$LAM','WIS$WIS')";

    //    this.RG_SG.DataSource = this.RoTation(dv_in.ToTable(), dv_out.ToTable(), "SG");
    //    this.RG_SG.DataBind();

    //    #endregion

    //    #region DataTable

    //    string strText;
    //    string strExpr;
    //    string strSort;
    //    DataRow[] foundRows;
    //    DataTable myTable;
    //    myTable = ds.Tables["Orders"];

    //    // Setup Filter and Sort Criteria 
    //    strExpr = "OrderDate >= '01.03.1998' AND OrderDate <= '31.03.1998'";
    //    strSort = "OrderDate DESC";

    //    // Use the Select method to find all rows matching the filter. 
    //    foundRows = myTable.Select(strExpr, strSort);

    //    // Apply all Columns to the TextBox, this 
    //    // must be done Row-By-Row. 
    //    strText = null;
    //    for (int i = 0; i <= foundRows.GetUpperBound(0); i++)
    //    {
    //        for (int j = 0; j <= foundRows[i].ItemArray.GetUpperBound(0); j++)
    //        {
    //            strText = strText + foundRows[i][j].ToString() + "\t";
    //        }
    //        //strText = strText + "\r\n";
    //        //textBox.Text = strText;
    //    } 


    //    #endregion
      
       

    //    string Connect_String22 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ARY"];
    //    OleDbConnection myConnection22 = new OleDbConnection(Connect_String22);
    //    myConnection22.Open();
    //    OleDbCommand myCommand22 = new OleDbCommand(sql_grade, myConnection22);
    //    OleDbDataAdapter oda22 = new OleDbDataAdapter(myCommand22);
    //    oda22.Fill(ds22);

    //    //ds = dbutil.GetDataset(sql_grade); 

    //    DataView dv = ds22.Tables[0].DefaultView;

    //    dv.RowFilter = "";
    //    dv.Sort = "DATE1 asc";

    //    string dep_key = "";

    //    //if (fab_area.SelectedValue == "MOD") 
    //    //{ 
    //    // ChartTop.Titles["Title2"].Text = "MOD FD"; 
    //    // ChartTop.ChartAreas["CT1"].Position.X = 0; 
    //    // ChartTop.ChartAreas["CT1"].Position.Y = 22; 
    //    // ChartTop.ChartAreas["CT1"].Position.Width = 95; 
    //    // ChartTop.ChartAreas["CT1"].Position.Height = 70; 
    //    // ChartTop.ChartAreas["CT2"].Visible = false; 
    //    // ChartTop.ChartAreas["CT3"].Visible = false; 
    //    // ChartTop.ChartAreas["CT4"].Visible = false; 
    //    //} 

    //    for (int i = 0; i < arr_shop.Length; i++)
    //    {
    //        dep_key = arr_shop[i];
    //        dv.RowFilter = " shop='" + dep_key + "' ";
    //        if (dv.Count > 0)
    //        {
    //            for (int j = 0; j < arr_shop.Length; j++)
    //            {

    //                switch (j)
    //                {
    //                    case 0:
    //                        Chart1.Series["Plan"].Points.AddXY(dv[j]["date1"], Convert.ToDouble(dv[j]["Plan"]));
    //                        Chart1.Series["Actual"].Points.AddXY(dv[j]["date1"], Convert.ToDouble(dv[j]["Actual"]));
    //                        break;
    //                    case 1:
    //                        Chart2.Series["Plan"].Points.AddXY(dv[j]["date1"], Convert.ToDouble(dv[j]["Plan"]));
    //                        Chart2.Series["Actual"].Points.AddXY(dv[j]["date1"], Convert.ToDouble(dv[j]["Actual"]));
    //                        break;
    //                    case 2:
    //                        Chart3.Series["Plan"].Points.AddXY(dv[j]["date1"], Convert.ToDouble(dv[j]["Plan"]));
    //                        Chart3.Series["Actual"].Points.AddXY(dv[j]["date1"], Convert.ToDouble(dv[j]["Actual"]));
    //                        break;
    //                    case 3:
    //                        Chart4.Series["Plan"].Points.AddXY(dv[j]["date1"], Convert.ToDouble(dv[j]["Plan"]));
    //                        Chart4.Series["Actual"].Points.AddXY(dv[j]["date1"], Convert.ToDouble(dv[j]["Actual"]));
    //                        break;
    //                    case 4:
    //                        Chart5.Series["Plan"].Points.AddXY(dv[j]["date1"], Convert.ToDouble(dv[j]["Plan"]));
    //                        Chart5.Series["Actual"].Points.AddXY(dv[j]["date1"], Convert.ToDouble(dv[j]["Actual"]));
    //                        break;
    //                    case 5:

    //                        break;

    //                }







    //            }



    //        }
    //        else
    //        {
    //            Chart1.Series["Plan"].Points.AddXY(dep_key, double.NaN);
    //            Chart1.Series["Actual"].Points.AddXY(dep_key, double.NaN);


    //        }
    //    }


    //    //for (int i = 0; i < ds22.Tables[0].Rows.Count; i++) 
    //    //{ 
    //    // Chart1.Series["Completed"].Points.AddXY(dep_key, Convert.ToDouble(ds22.Tables[0].Rows[i]["Completed"])); 
    //    // Chart1.Series["Ongoing"].Points.AddXY(dep_key, Convert.ToDouble(ds22.Tables[0].Rows[i]["Ongoing"])); 
    //    // Chart1.Series["New"].Points.AddXY(dep_key, Convert.ToDouble(ds22.Tables[0].Rows[i]["New"])); 
    //    // Chart1.Series["Total"].Points.AddXY(dep_key, Convert.ToDouble(ds22.Tables[0].Rows[i]["Total"])); 
    //    // Chart1.Series["Achieved Benefit"].Points.AddXY(dep_key, Convert.ToDouble(ds22.Tables[0].Rows[i]["Achieveed_benefit"])); 

    //    //} 

    //    //foreach (DataRow dr in ds22.Tables[0].Rows) 
    //    //{ 
    //    // Chart1.Series["Plan"].Points.AddXY(dep_key, Convert.ToDouble(r["plan"])); 
    //    // Chart1.Series["Actual"].Points.AddXY(dep_key, Convert.ToDouble(r["Actual"])); 

    //    //} 



    //    /* 
    //    ChartTop.ChartAreas["CT1"].AxisY.SubAxes.Add(new SubAxis("SubAxisY")); 
    //    ChartTop.Series["CT1_G1"].YSubAxisName = "SubAxisY"; 
    //    ChartTop.Series["CT1_G1"].YAxisType = (AxisType)AxisType.Secondary; 
    //    ChartTop.Series["CT1_G5"].YAxisType = (AxisType)AxisType.Secondary; 
    //    ChartTop.Series["CT1_G9"].YAxisType = (AxisType)AxisType.Secondary; 
    //    ChartTop.Series["CT1_NG"].YAxisType = (AxisType)AxisType.Secondary;*/

    //    dv.RowFilter = "";
    //    dv.Sort = "DATE1 asc";

    //    dep_key = "";


    //} 

    protected void GridView2_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        Int32 aa = gv.Columns.Count;

        Int32 bb = gv.Rows.Count;

        Int32 rownum = 0;
        Int32 colnum = 6;

        colnum = column_num1;

        Int32 counter = 1;

        //merge column  RowSpan
        //for (int i = 0; i <= colnum - 1; i++)
        //{

        //    counter = 1;
        //    for (int j = 1; j <= gv.Rows.Count - 1; j++)
        //    {

        //        if (GridView2.Rows[j].Cells[i].Text.Trim() == GridView2.Rows[(j - 1)].Cells[i].Text.Trim())
        //        {
        //            counter++;
        //            GridView2.Rows[j - counter + 1].Cells[i].RowSpan = counter;


        //            GridView2.Rows[j].Cells[i].Visible = false;


        //        }

        //        else
        //        {
        //            counter = 1;
        //            GridView2.Rows[j].Cells[i].RowSpan = counter;

        //        }



        //    }








        //}



        // merge row  ColumnSpan
        for (int i = 0; i <= gv.Rows.Count - 1; i++)
        {

            counter = 1;
            for (int j = 1; j <= colnum - 1; j++)
            {

                if (GridView2.Rows[i].Cells[j].Text.Trim() == GridView2.Rows[i].Cells[j - 1].Text.Trim())
                {
                    counter++;
                    GridView2.Rows[i].Cells[j - counter + 1].ColumnSpan = counter;


                    GridView2.Rows[i].Cells[j].Visible = false;


                }

                else
                {
                    counter = 1;
                    GridView2.Rows[i].Cells[j].ColumnSpan = counter;

                }



            }








        }

           
        }

      


    //    //#region merge_column data
    //    //foreach (int mergeColumn in mergeColumns)
    //    //{
    //    //    int i = 1;

    //    //    //  GridView Row Coount 
    //    //    foreach (GridViewRow wkItem in GridView2.Rows)
    //    //    {
    //    //        if (wkItem.RowIndex != 0)
    //    //        {
    //    //            if (wkItem.Cells[mergeColumn].Text.Trim() == GridView1.Rows[(wkItem.RowIndex - i)].Cells[mergeColumn].Text.Trim())
    //    //            {
    //    //                GridView2.Rows[(wkItem.RowIndex - i)].Cells[mergeColumn].RowSpan += 1;
    //    //                wkItem.Cells[mergeColumn].Visible = false;
    //    //                i += 1;
    //    //            }
    //    //            else
    //    //            {
    //    //                GridView2.Rows[(wkItem.RowIndex)].Cells[mergeColumn].RowSpan += 1;
    //    //                i = 1;
    //    //            }
    //    //        }
    //    //        else
    //    //        {
    //    //            wkItem.Cells[mergeColumn].RowSpan = 1;
    //    //        }


    //    //    }


    //    //}
    //    //#endregion
        
        
    //}


}
