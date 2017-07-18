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

#region MyRegion
//Date:2012/03/28
//Bunny
#endregion

public partial class TFT_TP_KPI_IN_OUT_CVP_DrillDown_Report : System.Web.UI.Page
{
    private Innolux.Portal.Report.ReportBase reportBase = new ReportBase();
    DbAccessHelper m_objDB = new DbAccessHelper("T2PRPT");
    string sShop = string.Empty;    
    protected void Page_Load(object sender, EventArgs e)
    {
        sShop = Request.QueryString["Shop"];
        this.DisplayMultiDataTable();        
    }

    public void DisplayMultiDataTable()
    {
        string sSql_Shop = @" select distinct t.shop from mps_pp t
                                where t.shop like '{0}' || '%'
                                order by shop ";
        sSql_Shop = string.Format(sSql_Shop, Shop_Transfer(sShop));

        DataTable dt_shop = m_objDB.ExecuteDataSet(sSql_Shop).Tables[0];

        //reportBase的function AddControlInTable會根據你傳進去的control在table中排列好
        for (int i = 0; i < dt_shop.Rows.Count; i++)
        {
            DataTable dt = this.Get_Chart_DataTable(dt_shop.Rows[i]["shop"].ToString());
            reportBase.AddControlInTable(this.Table1, this.DoChart(dt, "IN"), OrderBy.Row, 2, Align.Top);
            reportBase.AddControlInTable(this.Table1, this.DoChart(dt, "OUT"), OrderBy.Row, 2, Align.Top);
            reportBase.AddControlInTable(this.Table1, this.DoGrid(dt, "IN"), OrderBy.Row, 2, Align.Top);
            reportBase.AddControlInTable(this.Table1, this.DoGrid(dt, "OUT"), OrderBy.Row, 2, Align.Top);

        }        
    }

    #region DoChart，動態產生Chart
    protected Dundas.Charting.WebControl.Chart DoChart(DataTable inDT, string sType)
    {
        Dundas.Charting.WebControl.Chart chart = new Dundas.Charting.WebControl.Chart();
        DataTable dataSource = inDT.Copy();
        DataView dv = dataSource.DefaultView;
        this.SetChartStyle(chart);

        ChartArea chartarea;
        chartarea = chart.ChartAreas.Add("DoChart");
        chartarea.AxisX.Interval = 2;
        chartarea.AxisY.Title = "PLAN QTY";
        chartarea.AxisY2.Title = "ACTUAL QTY";

        chartarea.AxisY.LabelStyle.Format = "N0";
        chartarea.AxisY2.LabelStyle.Format = "N0";
        chartarea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
        chartarea.AxisX.MinorGrid.LineColor = System.Drawing.Color.Silver;
        chartarea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
        chartarea.AxisY.MinorGrid.LineColor = System.Drawing.Color.Silver;
        chartarea.AxisY2.MajorGrid.Enabled = false;
        chartarea.AxisY2.MinorGrid.Enabled = false;

        //設定Chart的Title
        DataTable dtDistinct = dataSource.DefaultView.ToTable(true, "SHOP");
        string sShop = dtDistinct.Rows[0]["SHOP"].ToString();
        sShop = sShop.Replace("T0", "C2");

        chart.Titles.Add(sShop + "_" + sType + "PUT");

        //設定Series(DAILY_PLAN_QTY,MTD_PLAN_QTY,DAILY_ACTUAL_QTY,MTD_ACTUAL_QTY)
        for (int i = 0; i < dataSource.Columns.Count; i++)
        {
            if (dataSource.Columns[i].ColumnName.ToUpper().Contains("QTY"))
            {
                Series series;
                series = chart.Series.Add(dataSource.Columns[i].ColumnName);
                series.ChartArea = chartarea.Name;
                series.EmptyPointStyle.BorderWidth = 0;
                series.ShowLabelAsValue = false;

                if (dataSource.Columns[i].ColumnName.ToUpper().Contains("MTD"))
                {
                    series.Type = SeriesChartType.Line;
                    series.MarkerStyle = MarkerStyle.Circle;
                    series.MarkerSize = 6;
                    series.MarkerBorderWidth = 1;
                    series.MarkerBorderColor = System.Drawing.Color.Black;
                    series.BorderWidth = 1;
                    series.YAxisType = AxisType.Primary;
                    series.ToolTip = "#VALX #SERIESNAME:#VAL{N0}";
                    series.LabelFormat = "N0";
                }
                else
                {
                    series.Type = SeriesChartType.Column;
                    series.YAxisType = AxisType.Secondary;
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
                dv.RowFilter = "shift_date='" + ThisMonBeginDay.ToString("yyyyMMdd") + "' and type='" + sType + "'";
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
        dv.RowFilter = " type= '" + sType + "'";
        DataTable dt = dv.ToTable(false, "SHIFT_DATE", "SHOP", "type", "DAILY_ACTUAL_QTY", "DAILY_PLAN_QTY", "MTD_ACTUAL_QTY", "MTD_PLAN_QTY");

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
        grid.PageSize = 16;
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
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
    }

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //PageIndexChanging的時候，再利用shop及type去重抓一次資料，塞給GridView
        GridView GridView = sender as GridView;
        GridView.PageIndex = e.NewPageIndex;
        string sShop = GridView.Rows[0].Cells[1].Text;
        string sType = GridView.Rows[0].Cells[2].Text;
        DataTable dt = this.Get_Chart_DataTable(sShop);
        DataView dv = dt.DefaultView;
        dv.RowFilter = "type='" + sType + "'";
        GridView.DataSource = dv.ToTable();
        GridView.DataBind();
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 3; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Text = string.Format("{0:#,##0}", Convert.ToInt32(e.Row.Cells[i].Text)); 
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
    protected DataTable Get_Chart_DataTable(string inShop)
    {
        string sDate = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
        string sSql = @" select
                        MPS.shift_date,MPS.SHOP,MPS.type,nvl(DAILY_ACTUAL_QTY,0) as DAILY_ACTUAL_QTY,
                        MPS.DAILY_PLAN_QTY,
                        DW_SUM.GET_MTD_ACTUAL_QTY(MPS.SHOP,MPS.SHIFT_DATE,MPS.TYPE) as MTD_ACTUAL_QTY,
                        --nvl(MTD_ACTUAL_QTY,0) as MTD_ACTUAL_QTY,
                        --MPS.MTD_PLAN_QTY                        
                        DW_SUM.GET_MTD_PLAN_QTY(MPS.SHOP,MPS.SHIFT_DATE,MPS.TYPE) as MTD_PLAN_QTY
                        from
                        (
                            select A.shift_date,A.shop,A.type,sum(a.DAILY_PLAN_QTY) as DAILY_PLAN_QTY from
                            (
                                select t.shift_date,t.shop,t.type,t.prod_name,
                                decode(t.shop,
                                'T0CEL',
                                round(nvl(sum(t.qty),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'CUT','CHIP','T0Cell'),0),
                                'T1CEL',
                                round(nvl(sum(t.qty),0)/DW_SUM.GET_PROD_MAPPING_MFG_MOVE(t.prod_name,'SUBSTRATE','CHIP','T1Cell'),0),
                                sum(t.qty)) as DAILY_PLAN_QTY
                                --,DW_SUM.GET_MTD_PLAN_QTY(t.shop,t.shift_date,t.type) as MTD_PLAN_QTY
                                from mps_pp t                        
                                where t.shift_date between substr('{0}',1,6) || '01' and '{0}'
                                and t.shop = '{1}'                        
                                and t.kind = 'DAILY'
                                group by t.shop,t.shift_date,t.type,t.prod_name
                            )A                            
                            group by A.shift_date,A.shop,A.type
                        )MPS,
                        (
                                select t.shift_date,t.shop,t.data_type,sum(t.qty) as DAILY_ACTUAL_QTY
                                --,DW_SUM.GET_MTD_ACTUAL_QTY(t.shop,t.shift_date,t.data_type) as MTD_ACTUAL_QTY 
                                from daily_in_out_sum t
                                --where t.shift_date like substr('{0}',1,6) || '%'
                                where t.shift_date between substr('{0}',1,6) || '01' and '{0}'
                                and t.shop = '{1}'
                                --and t.data_type = 'IN'
                                group by t.shift_date,t.shop,t.data_type
                        )D_IN_OUT
                        where MPS.shift_date = D_IN_OUT.shift_date(+)
                        and MPS.shop = D_IN_OUT.shop(+)
                        and MPS.type = D_IN_OUT.DATA_TYPE(+)
                        order by type,shift_date ";

        sSql = string.Format(sSql, sDate, inShop);
        DataTable dt = m_objDB.ExecuteDataSet(sSql).Tables[0];
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
