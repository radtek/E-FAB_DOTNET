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

public partial class CF_CF_FULL_AUTO_CF_fwfullautomonitor : System.Web.UI.Page
{
    string start_date = "";
    string end_date = "";
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CFT"];
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    string today_minus17 = DateTime.Now.AddDays(-17).ToString("yyyy/MM/dd");

    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm");
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    
    Int32 counter_total = 0;
    Int32 counter_auto_ratio = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.AllowPaging = false;
        if (!IsPostBack)
        {
            txtEstimateStartDate.SelectedDate = DateTime.Now.AddDays(-0);
            string[] choose_meeting_type ={ "ALL", "Array", "T1-Array", "T0-Array", "Cell", "CF", "CoverLens" };
            //string conn_str = System.Configuration.ConfigurationSettings.AppSettings["dsnn"];
            //string sql_area = "";
           
           

           

            Button1_Click(null, null);
            

        }


       


    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //gvNewsList.PageIndex = e.NewPageIndex;
        BindGridView();


    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if ((e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate)) || (e.Row.RowState == DataControlRowState.Edit))
        //    {
        //        ((DropDownList)e.Row.FindControl("lblStatus_Edit_Drop")).SelectedValue = ((DataRowView)e.Row.DataItem)["status"].ToString();

        //        //((HyperLink)e.Row.FindControl("HyperLink2")).Visible = false;

        //    }

        //    //e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
        //}
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //string DEADLINE = (Label)e.Row.FindControl("DEADLINE").ToString();

        //    String DEADLINE = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DEADLINE"));



        //    // DataBinder.Eval(Container.DataItem, "Price", "{0:c}") 

        //    //if (DEADLINE == "DEADLINE") 緪格砌?銝剔洵撟曉銝剔撘蝥 "DEADLINE"  槫瑁?
        //    //{

        //    e.Row.Cells[15].Style.Add("background-color", "#FFFF80"); //蝚?5甈雿  靽格寧箄瘥憿鞎蝥 #FFFF80
        //    //e.Row.Cells[15].BackColor=

        //    // }
        //}

    }

    private void BindGridView()
    {

        //txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "");

        if (DropDownList2.SelectedValue == "weekly")
     {
         sql_temp1 = " select min(t1.shift_date) as start_date,max(t1.shift_date) as end_date from shift_date t,shift_date t1 " +
 " where t.shift_date='" + txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "") + "'                                                                          " +
 "  and t.year=t1.year and t.week=t1.week                                                                 ";

         ds_temp = func.get_dataSet_access(sql_temp1, conn);


         start_date = ds_temp.Tables[0].Rows[0]["start_date"].ToString();
         end_date = ds_temp.Tables[0].Rows[0]["end_date"].ToString();

       
     }


     if (DropDownList2.SelectedValue == "monthly")
     {
        
        

         start_date = txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "").Substring(0,6)+"01";
         end_date = txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "").Substring(0, 6) + "31";
     
     }





     string sql = " select case when  substr(os1.EQP,4,10) is null then '總計' else substr(os1.EQP,4,10) end  as EQP,os1.manu,os1.auto,os1.total,os1.auto_ratio ,                                                                                      " +
 "       case                                                                                         " +
 "         when os2.eq2eq_cnt is null then                                                            " +
 "          0                                                                                         " +
 "         else                                                                                       " +
 "          os2.eq2eq_cnt                                                                             " +
 "       end as eq2eq,                                                                                " +
 "      case                                                                                          " +
 "         when os2.eq2eq_cnt is null then                                                            " +
 "          0                                                                                         " +
 "         else                                                                                       " +
 "          round((os2.eq2eq_cnt/os1.total)*100,2)                                                    " +
 "       end as eq2eq_P                                                                               " +
 "  from (select ot1.eqp,                                                       " +
 "               ot1.manu,                                                                            " +
 "               ot1.auto,                                                                            " +
 "               ot1.total,                                                                           " +
 "               round(ot1.auto / ot1.total, 3) * 100 as auto_ratio                                   " +
 "          from (select t.eqp,                                                                       " +
 "                       sum(case                                                                     " +
 "                             when t.briefdescription = 'Blank' then                                 " +
 "                              1                                                                     " +
 "                             else                                                                   " +
 "                              0                                                                     " +
 "                           end) as manu,                                                            " +
 "                       sum(case                                                                     " +
 "                             when t.briefdescription = 'DSPsrv_SetUp' then                          " +
 "                              1                                                                     " +
 "                             else                                                                   " +
 "                              0                                                                     " +
 "                           end) as auto,                                                            " +
 "                       count(t.briefdescription) as total                                           " +
 "                  from fwfullautomonitor t                                                          " +
 "                 where t.txntimestamp between '" + start_date + " 070000000' AND                              " +
 "                       '" + end_date + " 070000000'                                                         " +
 "                 group by t.eqp) ot1                                                                " +
 "        union all                                                                                   " +
 "        select '總計',                                                                              " +
 "               ot1.manu,                                                                            " +
 "               ot1.auto,                                                                            " +
 "               ot1.total,                                                                           " +
 "               round(ot1.auto / ot1.total, 3) * 100 as auto_ratio                                   " +
 "          from (select sum(case                                                                     " +
 "                             when t.briefdescription = 'Blank' then                                 " +
 "                              1                                                                     " +
 "                             else                                                                   " +
 "                              0                                                                     " +
 "                           end) as manu,                                                            " +
 "                       sum(case                                                                     " +
 "                             when t.briefdescription = 'DSPsrv_SetUp' then                          " +
 "                              1                                                                     " +
 "                             else                                                                   " +
 "                              0                                                                     " +
 "                           end) as auto,                                                            " +
 "                       count(t.briefdescription) as total                                           " +
 "                  from fwfullautomonitor t                                                          " +
 "                 where t.txntimestamp between '" + start_date + " 070000000' AND                              " +
 "                       '" + end_date + " 070000000') ot1) os1,                                              " +
 "       (SELECT case                                                                                 " +
 "                 when tonode = '1FBBL100' then                                                      " +
 "                  'B'                                                                               " +
 "                 when tonode = '1FIFL100' then                                                      " +
 "                  'IF'                                                                              " +
 "                 when tonode = '1FISL100' then                                                      " +
 "                  'IS'                                                                              " +
 "                 when tonode = '1FPSL100' then                                                      " +
 "                  'PS'                                                                              " +
 "                 when tonode = '1FRRL100' then                                                      " +
 "                  'R'                                                                               " +
 "                 when tonode = '1FITL100' then                                                      " +
 "                  'ITL'                                                                             " +
 "                 else                                                                               " +
 "                  'NA'                                                                              " +
 "               end as tonode,                                                                       " +
 "               count(tid) as eq2eq_cnt                                                              " +
 "          FROM cf_fweq2eqhistory                                                                    " +
 "         WHERE txntimestamp between '" + start_date + " 070000000' AND                                        " +
 "               '" + end_date + " 070000000'                                                                 " +
 "         group by tonode                                                                            " +
 "                                                                                                    " +
 "        ) os2                                                                                       " +
"  where substr(os1.eqp,4,10) = os2.tonode(+)                                   " +
 " order by os1.eqp     ";
      
        DataSet ds_meeting_type = new DataSet();
        ds_meeting_type = func.get_dataSet_access(sql, conn);
        GridView1.DataSource = ds_meeting_type.Tables[0];
        GridView1.DataBind();


    }

    private void doChart()
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
        series.Name = "total";
        series.ShowLabelAsValue = true;
        series.LabelBackColor = Color.Ivory;
     

        // Add series into the chart's series collection


        for (int i = 0; i <= Chart1.Series.Count-1; i++)
        {
            if (Chart1.Series[i].Name == "total")
           //Chart1.Series[i].Name = "total";
            counter_total++;
        }

        if (counter_total<=0)
        {

            Chart1.Series.Add(series);
        }
        
        
        

        　　　　　
        
  
        　
        
    
            


        

       

        Series series1 = new Series("Line");
        series1.Type = SeriesChartType.Spline;
        series1.BorderWidth = 3;
        series1.ShadowOffset = 2;
        series1.Name = "auto_ratio";
        series1.ShowLabelAsValue = true;
        series1.LabelBackColor = Color.Gold;




        // Add series into the chart's series collection

        for (int i = 0; i <= Chart1.Series.Count - 1; i++)
        {
            if(Chart1.Series[i].Name == "auto_ratio")
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
        series2.Name = "eq2eq_P";
        series2.ShowLabelAsValue = true;
        series2.LabelBackColor = Color.SkyBlue;
        series2.Color = Color.Pink;

        for (int i = 0; i <= Chart1.Series.Count - 1; i++)
        {
            if (Chart1.Series[i].Name == "eq2eq_P")
                counter_auto_ratio++;
        }


        if (counter_auto_ratio <= 0)
        {

            Chart1.Series.Add(series2);
        }


        //CreateYAxis(Chart1, Chart1.ChartAreas["Default"], Chart1.Series["auto_ratio"], 0, 8);   
        
 

          

        

       

        
        
     

        DataSet ds22 = new DataSet();
        DataSet ds33 = new DataSet();

        string[] Meeting_type = { "其他", "宣導事項", "工程實驗", "改善對策", "機台保養", "決議事項", "重大決議", "生產狀況", "生產相關", "異常報告", "良率相關", "長期事項" };


        string sql = "";


        //string sql_product;

        if (DropDownList2.SelectedValue == "weekly")
        {
            sql_temp1 = " select min(t1.shift_date) as start_date,max(t1.shift_date) as end_date from shift_date t,shift_date t1 " +
    " where t.shift_date='" + txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "") + "'                                                                          " +
    "  and t.year=t1.year and t.week=t1.week                                                                 ";

            ds_temp = func.get_dataSet_access(sql_temp1, conn);


            start_date = ds_temp.Tables[0].Rows[0]["start_date"].ToString();
            end_date = ds_temp.Tables[0].Rows[0]["end_date"].ToString();


        }


        if (DropDownList2.SelectedValue == "monthly")
        {



            start_date = txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "").Substring(0, 6) + "01";
            end_date = txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "").Substring(0, 6) + "31";

        }





        string sql_temp2= " select case when  substr(os1.EQP,4,10) is null then '總計' else substr(os1.EQP,4,10) end  as EQP,os1.manu,os1.auto,os1.total,os1.auto_ratio ,                                                                                      " +
 "       case                                                                                         " +
 "         when os2.eq2eq_cnt is null then                                                            " +
 "          0                                                                                         " +
 "         else                                                                                       " +
 "          os2.eq2eq_cnt                                                                             " +
 "       end as eq2eq,                                                                                " +
 "      case                                                                                          " +
 "         when os2.eq2eq_cnt is null then                                                            " +
 "          0                                                                                         " +
 "         else                                                                                       " +
 "          round((os2.eq2eq_cnt/os1.total)*100,2)                                                    " +
 "       end as eq2eq_P                                                                               " +
 "  from (select ot1.eqp,                                                       " +
 "               ot1.manu,                                                                            " +
 "               ot1.auto,                                                                            " +
 "               ot1.total,                                                                           " +
 "               round(ot1.auto / ot1.total, 3) * 100 as auto_ratio                                   " +
 "          from (select t.eqp,                                                                       " +
 "                       sum(case                                                                     " +
 "                             when t.briefdescription = 'Blank' then                                 " +
 "                              1                                                                     " +
 "                             else                                                                   " +
 "                              0                                                                     " +
 "                           end) as manu,                                                            " +
 "                       sum(case                                                                     " +
 "                             when t.briefdescription = 'DSPsrv_SetUp' then                          " +
 "                              1                                                                     " +
 "                             else                                                                   " +
 "                              0                                                                     " +
 "                           end) as auto,                                                            " +
 "                       count(t.briefdescription) as total                                           " +
 "                  from fwfullautomonitor t                                                          " +
 "                 where t.txntimestamp between '" + start_date + " 070000000' AND                              " +
 "                       '" + end_date + " 070000000'                                                         " +
 "                 group by t.eqp) ot1                                                                " +
") os1,                                              " +
 "       (SELECT case                                                                                 " +
 "                 when tonode = '1FBBL100' then                                                      " +
 "                  'B'                                                                               " +
 "                 when tonode = '1FIFL100' then                                                      " +
 "                  'IF'                                                                              " +
 "                 when tonode = '1FISL100' then                                                      " +
 "                  'IS'                                                                              " +
 "                 when tonode = '1FPSL100' then                                                      " +
 "                  'PS'                                                                              " +
 "                 when tonode = '1FRRL100' then                                                      " +
 "                  'R'                                                                               " +
 "                 when tonode = '1FITL100' then                                                      " +
 "                  'ITL'                                                                             " +
 "                 else                                                                               " +
 "                  'NA'                                                                              " +
 "               end as tonode,                                                                       " +
 "               count(tid) as eq2eq_cnt                                                              " +
 "          FROM cf_fweq2eqhistory                                                                    " +
 "         WHERE txntimestamp between '" + start_date + " 070000000' AND                                        " +
 "               '" + end_date + " 070000000'                                                                 " +
 "         group by tonode                                                                            " +
 "                                                                                                    " +
 "        ) os2                                                                                       " +
"  where substr(os1.eqp,4,10) = os2.tonode(+)                                   " +
 " order by os1.eqp     ";



        ds22 = func.get_dataSet_access(sql_temp2, conn);




        //ds = dbutil.GetDataset(sql_grade);


        DataView dv_times = ds22.Tables[0].DefaultView;

        dv_times.RowFilter = " ";

        //dv_FAB.Sort = "";
        //DataTable dt_meeting_type;
        //將重複的 Table 欄位"event_type"   data distinct出來

        //dt_meeting_type = dv_times.ToTable(true, "event_type");
        //dv_FAB.Sort = "";

        //DataView TypeView = dt_meeting_type.DefaultView;

        //Chart1.Series["total"].Type = SeriesChartType.Line;
        Chart1.Series["auto_ratio"].YAxisType = AxisType.Secondary;
        Chart1.ChartAreas["Default"].AxisY2.LabelStyle.Format = "P0";
        Chart1.ChartAreas["Default"].AxisY2.Title = "Auto_Ratio(%)";
        Chart1.ChartAreas["Default"].AxisY2.TitleFont = new Font("Times New Roman", 12, FontStyle.Bold);

        Chart1.ChartAreas["Default"].AxisY2.TitleColor = Color.Red;

        Chart1.ChartAreas["Default"].AxisX.LabelsAutoFit =false;



        for (int x = 0; x <= dv_times.Count - 1; x++)
        {

            Chart1.Series["manu"].Points.AddXY(dv_times[x]["eqp"], Convert.ToDouble(dv_times[x]["manu"]));
            Chart1.Series["auto"].Points.AddXY(dv_times[x]["eqp"], Convert.ToDouble(dv_times[x]["auto"]));
            Chart1.Series["total"].Points.AddXY(dv_times[x]["eqp"], Convert.ToDouble(dv_times[x]["total"]));
            Chart1.Series["auto_ratio"].Points.AddXY(dv_times[x]["eqp"], Convert.ToDouble(dv_times[x]["auto_ratio"]));
            Chart1.Series["eq2eq_P"].Points.AddXY(dv_times[x]["eqp"], Convert.ToDouble(dv_times[x]["eq2eq_P"]));
          
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGridView();
        doChart();
    }


    public void CreateYAxis(Chart chart, ChartArea area, Series series, float axisOffset, float labelsSize)
    {
        // Create new chart area for original series
        ChartArea areaSeries = chart.ChartAreas.Add("ChartArea_" + series.Name);
        areaSeries.BackColor = Color.Transparent;
        areaSeries.BorderColor = Color.Transparent;
        areaSeries.Position.FromRectangleF(area.Position.ToRectangleF());
        areaSeries.InnerPlotPosition.FromRectangleF(area.InnerPlotPosition.ToRectangleF());
        areaSeries.AxisX.MajorGrid.Enabled = false;
        areaSeries.AxisX.MajorTickMark.Enabled = false;
        areaSeries.AxisX.LabelStyle.Enabled = false;
        areaSeries.AxisY.MajorGrid.Enabled = false;
        areaSeries.AxisY.MajorTickMark.Enabled = false;
        areaSeries.AxisY.LabelStyle.Enabled = false;
        areaSeries.AxisY.StartFromZero = area.AxisY.StartFromZero;

        series.ChartArea = areaSeries.Name;

        // Create new chart area for axis
        ChartArea areaAxis = chart.ChartAreas.Add("AxisY_" + series.ChartArea);
        areaAxis.BackColor = Color.Transparent;
        areaAxis.BorderColor = Color.Transparent;
        areaAxis.Position.FromRectangleF(chart.ChartAreas[series.ChartArea].Position.ToRectangleF());
        areaAxis.InnerPlotPosition.FromRectangleF(chart.ChartAreas[series.ChartArea].InnerPlotPosition.ToRectangleF());

        // Create a copy of specified series
        Series seriesCopy = chart.Series.Add(series.Name + "_Copy");
        seriesCopy.ChartType = series.ChartType;
        foreach (DataPoint point in series.Points)
        {
            seriesCopy.Points.AddXY(point.XValue, point.YValues[0]);
        }

        // Hide copied series
        seriesCopy.ShowInLegend = false;
        seriesCopy.Color = Color.Transparent;
        seriesCopy.BorderColor = Color.Transparent;
        seriesCopy.ChartArea = areaAxis.Name;

        // Disable grid lines & tickmarks
        areaAxis.AxisX.LineWidth = 0;
        areaAxis.AxisX.MajorGrid.Enabled = false;
        areaAxis.AxisX.MajorTickMark.Enabled = false;
        areaAxis.AxisX.LabelStyle.Enabled = false;
        areaAxis.AxisY.MajorGrid.Enabled = false;
        areaAxis.AxisY.StartFromZero = area.AxisY.StartFromZero;

        // Adjust area position
        areaAxis.Position.X -= axisOffset;
        areaAxis.InnerPlotPosition.X += labelsSize;
    }

}
