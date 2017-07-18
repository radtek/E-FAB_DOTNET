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
using Innolux.Portal.Report;
using Innolux.Portal.WebControls;
using Innolux.Portal.EntLibBlock.DataAccess;
using Innolux.Portal.CommonFunction;
using Telerik.Web.UI;
using Dundas.Charting.WebControl;

public partial class chairman_cycle_time_Report_DrillDown : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_FAB_LOAD_XLS"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_T2PRPT"];
    string subject = "";
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sSql = "";

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
   
     

    private Innolux.Portal.Report.ReportBase reportBase = new ReportBase();
    //DbAccessHelper m_objDB = new DbAccessHelper("T2PRPT");
    string sShop = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sShop = Request.QueryString["Shop"];
        //sShop = "C5";
        this.DisplayMultiDataTable();
    }

    public void DisplayMultiDataTable()
    {
//        string sSql_Shop = @" select distinct t.shop from daily_in_out_sum t
//                                where t.shop like '{0}' || '%'
//                                order by shop ";
        //sSql_Shop = string.Format(sSql_Shop, Shop_Transfer(sShop));
        // sSql_Shop = string.Format(sSql_Shop, sShop);

       sql_temp = @"select ot5.shift_date,ot5.product,ot5.plant,ot5.fab,ot5.shop,ot5.product_type,ot5.target,ot5.act_cycletime,ot5.diff from 
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
(case when sum(t.qty+nvl(t.unship_cnt,0)) <=0 then 0     
     
     when t.SHOP<>'' then  sum(t.cycletime*(t.qty+nvl(t.unship_cnt,0)))/sum(t.qty+nvl(t.unship_cnt,0))
        else 
      sum(t.cycletime*t.qty)/sum(t.qty)
     
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
and tt.prod_name=tv.PROD_NAME(+)) t
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


       ds_temp1 = func.get_dataSet_access(sql_temp, conn1);

       DataView dv = ds_temp1.Tables[0].DefaultView;
       dv.RowFilter = " shop='" + sShop + "'  ";

       DT1 = dv.ToTable("PRODUCT_TYPE");


       

    
        //reportBase的function AddControlInTable會根據你傳進去的control在table中排列好
       for (int i = 0; i < DT1.Rows.Count; i++)
        {
            DataTable dt = this.Get_Chart_DataTable(sShop, DT1.Rows[i]["PRODUCT_TYPE"].ToString());  // (T1ARY,IPS)

            reportBase.AddControlInTable(this.Table1, this.DoChart(dt, "CYCLE_TIME", DT1.Rows[i]["PRODUCT_TYPE"].ToString()), OrderBy.Row, 1, Align.Top);
          
            //reportBase.AddControlInTable(this.Table1, this.DoChart(dt, "CYCLE_TIME"), OrderBy.Row, 2, Align.Top);
            reportBase.AddControlInTable(this.Table1, this.DoGrid(dt, "CYCLE_TIME"), OrderBy.Row, 1, Align.Top);
            //reportBase.AddControlInTable(this.Table1, this.DoGrid(dt, "CYCLE_TIME"), OrderBy.Row, 2, Align.Top);

        }

        
    }

    #region DoChart，動態產生Chart
    protected Dundas.Charting.WebControl.Chart DoChart(DataTable inDT, string sType,string product_type)
    {
        Dundas.Charting.WebControl.Chart chart = new Dundas.Charting.WebControl.Chart();
        DataTable dataSource = inDT.Copy();
        DataView dv = dataSource.DefaultView;
        this.SetChartStyle(chart);

        ChartArea chartarea;
        chartarea = chart.ChartAreas.Add("DoChart");
        chartarea.AxisX.Interval = 2;
        chartarea.AxisY.Title = "CYCLE_TIME";
        //chartarea.AxisY2.Title = "TARGET";

        chartarea.AxisY.LabelStyle.Format = "N0";
        //chartarea.AxisY2.LabelStyle.Format = "N0";
        chartarea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
        chartarea.AxisX.MinorGrid.LineColor = System.Drawing.Color.Silver;
        chartarea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
        chartarea.AxisY.MinorGrid.LineColor = System.Drawing.Color.Silver;
        //chartarea.AxisY2.MajorGrid.Enabled = false;
        //chartarea.AxisY2.MinorGrid.Enabled = false;
    
        //設定Chart的Title
        DataTable dtDistinct = dataSource.DefaultView.ToTable(true, "SHOP");
        string sShop = dtDistinct.Rows[0]["SHOP"].ToString();
        sShop = sShop.Replace("T0", "C2");

        //chart.Titles.Add(sShop + "_" + sType + "PUT");
        chart.Titles.Add(sShop + "_" + product_type + "_" + sType);

        //設定Series(DAILY_PLAN_QTY,MTD_PLAN_QTY,DAILY_ACTUAL_QTY,MTD_ACTUAL_QTY)
        for (int i = 0; i < dataSource.Columns.Count; i++)
        {
            if (dataSource.Columns[i].ColumnName.ToUpper().Contains("CYCLE_TIME") || dataSource.Columns[i].ColumnName.ToUpper().Contains("TARGET"))
            {
                Series series;
                series = chart.Series.Add(dataSource.Columns[i].ColumnName);
                series.ChartArea = chartarea.Name;
                series.EmptyPointStyle.BorderWidth = 0;
                series.ShowLabelAsValue = false;
             
                 if (dataSource.Columns[i].ColumnName.ToUpper().Contains("TARGET"))
                {
                    series.Type = SeriesChartType.Line;
                    series.MarkerStyle = MarkerStyle.Circle;
                    series.MarkerSize = 6;
                    series.MarkerBorderWidth = 1;
                    series.MarkerBorderColor = System.Drawing.Color.Black;
                    series.BorderWidth = 1;
                    //series.YAxisType = AxisType.Secondary;
                    series.ToolTip = "#VALX #SERIESNAME:#VAL{N0}";
                    series.LabelFormat = "N0";
                }
                else
                {
                    series.Type = SeriesChartType.Column;
                    series.YAxisType = AxisType.Primary;
                    series.BorderColor = System.Drawing.Color.Black;
                    series.BorderWidth = 1;
                    series.ToolTip = "#VALX #SERIESNAME:#VAL{N0}";
                    series.LabelFormat = "N0";
                }
            }



        }

        //日期迴圈，補足月初到月底的每一天都要呈現在Chart上面
        //因為來源的Table有些會缺少月中的某些天(MPS沒有modeling)
        System.DateTime dt = System.DateTime.Now;
        System.DateTime ThisMonBeginDay = new System.DateTime(dt.Year, dt.Month, 1);
        System.DateTime ThisMonEndDay = ThisMonBeginDay.AddMonths(1).AddDays(-1);

        while (DateDiff(ThisMonBeginDay, ThisMonEndDay) >= 0)
        {
            foreach (Series series in chart.Series)
            {
                //用dataview來作rowfilter，有存在dv中的shift_date就呈現該column的值，不然就畫0
                //dv.RowFilter = "shift_date='" + ThisMonBeginDay.ToString("yyyyMMdd") + "' and type='" + sType + "'";
                dv.RowFilter = "shift_date='" + ThisMonBeginDay.ToString("yyyyMMdd") + "'";
                if (dv.Count > 0)
                {
                    series.Points.AddXY(ThisMonBeginDay.ToString("yyyyMMdd"), dv[0][series.Name].ToString());
                }
                else
                {
                    series.Points.AddXY(ThisMonBeginDay.ToString("yyyyMMdd"), Double.NaN);
                }

            }
            ThisMonBeginDay = ThisMonBeginDay.AddDays(1);
        }
        return chart;
    }
    #endregion


    #region DoGrid，動態產生Grid
    public GridView DoGrid(DataTable sourceTable, string sType)
    {
        DataTable dataSource = sourceTable.Copy();
        DataView dv = dataSource.DefaultView;
        //dv.RowFilter = " type= '" + sType + "'";
        dv.Sort = "  SHIFT_DATE ";
      
        DataTable dt = dv.ToTable(false, "SHIFT_DATE", "SHOP", "CYCLE_TIME","TARGET");

        GridView grid = new GridView();
        grid.BorderStyle = BorderStyle.Solid;
        grid.BorderWidth = 1;
        grid.CellSpacing = 1;
        grid.BackColor = System.Drawing.Color.LightBlue;
        grid.GridLines = GridLines.None;
        grid.BorderColor = System.Drawing.Color.Silver;
        grid.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(208, 215, 229);
        grid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
        grid.RowStyle.BackColor = System.Drawing.Color.White;
        grid.RowStyle.ForeColor = System.Drawing.Color.Black;
        grid.RowStyle.Font.Size = FontUnit.Point(8);
        grid.Width = 750;
        grid.AllowPaging = true;
        grid.PageSize = 40;
        form1.Controls.Add(grid);
        //定義RowCreated，目的是為了隱藏欄位
        grid.RowCreated += new GridViewRowEventHandler(GridView_RowCreated);
        grid.RowDataBound += new GridViewRowEventHandler(GridView_RowDataBound);
        grid.DataSource = dt;
        grid.DataBind();
        //定義PageIndexChanging，目的是為了換頁
        grid.PageIndexChanging += new GridViewPageEventHandler(GridView_PageIndexChanging);

        return grid;
    }
    #endregion


    #region Grid Event
    protected void GridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = true;
            e.Row.Cells[1].Visible = true;
            e.Row.Cells[2].Visible = true;
        }
    }

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //PageIndexChanging的時候，再利用shop及type去重抓一次資料，塞給GridView
        GridView GridView = sender as GridView;
        GridView.PageIndex = e.NewPageIndex;
        string sShop = GridView.Rows[0].Cells[1].Text;
        string sType = GridView.Rows[0].Cells[2].Text;
        DataTable dt = this.Get_Chart_DataTable(sShop,"");
        DataView dv = dt.DefaultView;
       // dv.RowFilter = "type='" + sType + "'";
        GridView.DataSource = dv.ToTable();
        GridView.DataBind();
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 3; i < e.Row.Cells.Count; i++)
            {
                //e.Row.Cells[i].Text = string.Format("{0:#,##0}", Convert.ToInt32(e.Row.Cells[i].Text));
            }
        }
    }
    #endregion


    #region Rotation，使Grid轉向，以日期欄位為Column，目前並無使用
    protected DataTable RoTation(DataTable dt_IN)
    {
        DataTable TargetTable = new DataTable();
        string columnName = "";

        System.DateTime dt = System.DateTime.Now;
        System.DateTime ThisMonBeginDay = new System.DateTime(dt.Year, dt.Month, 1);
        System.DateTime ThisMonEndDay = ThisMonBeginDay.AddMonths(1).AddDays(-1);

        ArrayList ALShift_Date = new ArrayList();
        TargetTable.Columns.Add("RAWDATA");
        while (DateDiff(ThisMonBeginDay, ThisMonEndDay) >= 0)
        {
            TargetTable.Columns.Add(ThisMonBeginDay.ToString("MMdd"), typeof(string));
            ALShift_Date.Add(ThisMonBeginDay.ToString("yyyyMMdd"));
            ThisMonBeginDay = ThisMonBeginDay.AddDays(1);
        }

        int i = 0;

        foreach (DataColumn dc in dt_IN.Columns)
        {
            columnName = dc.ColumnName.ToUpper();
            if (!columnName.Contains("SHIFT_DATE"))
            {
                DataRow targetRow = TargetTable.NewRow();
                targetRow["RAWDATA"] = dc.ColumnName;
                DataView dv = dt_IN.DefaultView;
                for (int j = 0; j < ALShift_Date.Count; j++)
                {
                    dv.RowFilter = "shift_date='" + ALShift_Date[j].ToString() + "'";
                    if (dt_IN.DefaultView.Count > 0)
                    {
                        targetRow[j + 1] = dv[0][dc.ColumnName].ToString();
                    }
                    else
                    {
                        targetRow[j + 1] = "0";
                    }

                }

                TargetTable.Rows.Add(targetRow);
            }
        }
        return TargetTable;

    }
    #endregion


    #region 日期迴圈
    protected int DateDiff(DateTime StartDate, DateTime EndDate)
    {
        TimeSpan dateDiff = EndDate - StartDate;
        return dateDiff.Days;
    }
    #endregion


    #region Get Grid Data()
    protected DataTable Get_Chart_DataTable(string inShop,string product_type)
    {
        string sDate = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
//        string sSql = @" select
//                        MPS.shift_date,MPS.SHOP,MPS.type,nvl(DAILY_ACTUAL_QTY,0) as DAILY_ACTUAL_QTY,
//                        MPS.DAILY_PLAN_QTY,
//                        DW_SUM.GET_MTD_ACTUAL_QTY(MPS.SHOP,MPS.SHIFT_DATE,MPS.TYPE) as MTD_ACTUAL_QTY,
//                        --nvl(MTD_ACTUAL_QTY,0) as MTD_ACTUAL_QTY,
//                        --MPS.MTD_PLAN_QTY                        
//                        DW_SUM.GET_MTD_PLAN_QTY(MPS.SHOP,MPS.SHIFT_DATE,MPS.TYPE) as MTD_PLAN_QTY
//                        from
//                        (
//                            select A.shift_date,A.shop,A.type,sum(a.DAILY_PLAN_QTY) as DAILY_PLAN_QTY from
//                            (
//                                select t.shift_date,t.shop,t.type,t.prod_name,
//                                decode(t.shop,
//                                'T0CEL',
//                                round(nvl(sum(t.qty),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'CUT','CHIP','T0Cell'),0),
//                                'T1CEL',
//                                round(nvl(sum(t.qty),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'SUBSTRATE','CHIP','T1Cell'),0),
//                                sum(t.qty)) as DAILY_PLAN_QTY
//                                --,DW_SUM.GET_MTD_PLAN_QTY(t.shop,t.shift_date,t.type) as MTD_PLAN_QTY
//                                from mps_pp t                        
//                                where t.shift_date between substr('{0}',1,6) || '01' and '{0}'
//                                and t.shop = '{1}'                        
//                                and t.kind = 'DAILY'
//                                group by t.shop,t.shift_date,t.type,t.prod_name
//                            )A                            
//                            group by A.shift_date,A.shop,A.type
//                        )MPS,
//                        (
//                                select t.shift_date,t.shop,t.data_type,sum(t.qty) as DAILY_ACTUAL_QTY
//                                --,DW_SUM.GET_MTD_ACTUAL_QTY(t.shop,t.shift_date,t.data_type) as MTD_ACTUAL_QTY 
//                                from daily_in_out_sum t
//                                --where t.shift_date like substr('{0}',1,6) || '%'
//                                where t.shift_date between substr('{0}',1,6) || '01' and '{0}'
//                                and t.shop = '{1}'
//                                --and t.data_type = 'IN'
//                                group by t.shift_date,t.shop,t.data_type
//                        )D_IN_OUT
//                        where MPS.shift_date = D_IN_OUT.shift_date(+)
//                        and MPS.shop = D_IN_OUT.shop(+)
//                        and MPS.type = D_IN_OUT.DATA_TYPE(+)
//                        order by type,shift_date ";

        if (inShop.Equals(product_type))
        {
            sSql = @" 

select ot1.*,ot2.target from (

select t.shift_date ,t.shop,round((case when sum(t.qty+nvl(t.unship_cnt,0))  <=0 then 0 
                                        

else sum(t.cycletime*(t.qty+nvl(t.unship_cnt,0)))/sum(t.qty+nvl(t.unship_cnt,0)) end ),2)  as cycle_time from daily_in_out_sum t

where t.shop='{1}'
and   t.shift_date between substr('{0}',1,6) || '01' and '{0}'
and t.data_type='OUT'
and nvl(t.qty + nvl(t.unship_cnt, 0),0)>0
and nvl(t.cycletime,0)>0
--and t.qty>0

group by t.shift_date,t.shop
)ot1
,
(select t.* from cycle_time_conf t)ot2
where ot1.shop=ot2.plant_fab ";
        }
        else
        {

           

            sSql = @" 

 select ot3.shift_date,ot3.shop,ot3.cycle_time,ot3.target from (

select ot1.*,ot2.target from (

select ot10.*,'{2}' as product_type from (
select t.shift_date ,t.shop,round((case when sum(t.qty+nvl(t.unship_cnt,0))  <=0 then 0 
                                        

else sum(t.cycletime*(t.qty+nvl(t.unship_cnt,0)))/sum(t.qty+nvl(t.unship_cnt,0)) end ),2)  as cycle_time  from daily_in_out_sum t

where t.shop='{1}'
and   t.shift_date between substr('{0}',1,6) || '01' and '{0}'
and t.data_type='OUT'
and nvl(t.qty + nvl(t.unship_cnt, 0),0)>0
and nvl(t.cycletime,0)>0
and t.prod_name in ((select ob1.prod_name from 
(

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



) ob1
where ob1.shop='{1}' and ob1.product='{2}'))
--and t.qty>0

group by t.shift_date,t.shop
) ot10
where ot10.cycle_time>0


  ) ot1
,
(select t.* from cycle_time_conf t)ot2
where ot1.shop=ot2.plant_fab 
      and ot1.product_type=ot2.product_type(+)
)ot3

group by ot3.shift_date,ot3.shop,ot3.cycle_time,ot3.target
order by ot3.shift_date 





";
        
        
        }



        sSql = string.Format(sSql, sDate, inShop, product_type);

        DataTable dt = func.get_dataSet_access(sSql, conn1).Tables[0];
        //DataTable dt = m_objDB.ExecuteDataSet(sSql).Tables[0];
        return dt;
    }
    #endregion


    #region 【Shop Transfer，因為合併欄位後，合併的項目無法作超連結綁定，因此設計無論點T1ARY,T1CF,T1CEL都會呈現三區的資料】
    public string Shop_Transfer(string inShop)
    {
        switch (inShop)
        {
            case "T1ARY":
                inShop = "T1";
                break;
            case "T1CF":
                inShop = "T1";
                break;
            case "T1CEL":
                inShop = "T1";
                break;
            case "T2ARY":
                inShop = "T2";
                break;
            case "T2CF":
                inShop = "T2";
                break;
            case "T2CEL":
                inShop = "T2";
                break;
            case "T0ARY":
                inShop = "T0";
                break;
            case "T0CEL":
                inShop = "T0";
                break;
            default:
                break;
        }
        return inShop;
    }
    #endregion



    public void SetChartStyle(Dundas.Charting.WebControl.Chart chart)
    {
        if (chart == null)
            return;

        chart.Width = 750;
        chart.Height = 350;
        chart.Palette = ChartColorPalette.Dundas;
        chart.BackGradientEndColor = System.Drawing.Color.White;
        chart.BackGradientType = GradientType.TopBottom;
        chart.BorderLineWidth = 0;
        chart.BorderLineStyle = ChartDashStyle.Solid;
        chart.BackColor = System.Drawing.Color.FromArgb(222, 230, 240);
        chart.BorderLineColor = System.Drawing.Color.FromArgb(64, 0, 0, 0);

        chart.BorderSkin.FrameBackColor = System.Drawing.Color.CornflowerBlue;
        chart.BorderSkin.FrameBackGradientEndColor = System.Drawing.Color.CornflowerBlue;
        chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

        chart.Legends["Default"].LegendStyle = LegendStyle.Table;
        chart.Legends["Default"].Docking = LegendDocking.Top;
        chart.Legends["Default"].Alignment = System.Drawing.StringAlignment.Center;
        chart.Legends["Default"].TableStyle = LegendTableStyle.Auto;
    }
}
