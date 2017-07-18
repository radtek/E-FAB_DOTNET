using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using ChartFX.WebForms;
using Innolux.Portal.Report;
using Innolux.Portal.WebControls;

namespace Innolux.Portal.Report
{
    /// <summary>
    /// Summary description for ReportBase
    /// </summary>
    [Serializable]
    public class ReportBase
    {
        #region Public Propery
        
        // Data time: data type, start time, end time
        private QueryDateType dateType = QueryDateType.Daily;
        public QueryDateType DateType
        {
            get { return this.dateType; }
            set { this.dateType = value; }
        }
        /// <summary>
        /// 2008-09-12 -> '20080912'
        /// </summary>
        private string startDateTime;
        public string StartDateTime
        {
            get { return this.startDateTime; }
            set { this.startDateTime = value; }
        }
        private string endDateTime;
        public string EndDateTime
        {
            get { return this.endDateTime; }
            set { this.endDateTime = value; }
        }


        /// <summary>
        /// Main production Names list
        /// </summary>
        private IList<string> mainProductNames = new List<string>();
        public IList<string> MainProductNames
        {
            get { return this.mainProductNames; }
            set { this.mainProductNames = value; }
        }
        /// <summary>
        /// Sub production Names list
        /// </summary>
        private IList<string> subProductNames = new List<string>();
        public IList<string> SubProductNames
        {
            get { return this.subProductNames; }
            set { this.subProductNames = value; }
        }
        private IList<string> size = new List<string>();
        public IList<string> Size
        {
            get { return this.size; }
            set { this.size = value; }
        }
        private IList<string> model = new List<string>();
        public IList<string> Model
        {
            get { return this.model; }
            set { this.model = value; }
        }
        private string productFullName = "";
        public string ProductFullName
        {
            get { return this.productFullName; }
            set { this.productFullName = value; }
        }

        
        /// <summary>
        /// Batch Type or Lot Type
        /// </summary>
        private string batchType = "";
        public string BatchType
        {
            get { return this.batchType; }
            set { this.batchType = value; }
        }
        private bool isBatchID = true;
        public bool IsBatchID
        {
            get { return this.isBatchID; }
            set { this.isBatchID = value; }
        }
        private IList<string> batchIDs = new List<string>();
        public IList<string> BatchIDs
        {
            get { return this.batchIDs; }
            set { this.batchIDs = value; }
        }
        private bool isFilterBatchIDs = false;
        public bool IsFilterBatchIDs
        {
            get { return this.isFilterBatchIDs; }
            set { this.isFilterBatchIDs = value; }
        }



        // Step
        private string stepProcess = "IS/IF";
        public string StepProcess
        {
            get { return this.stepProcess; }
            set { this.stepProcess = value; }
        }
        private IList<string> stepIDs;
        public IList<string> StepIDs
        {
            get { return this.stepIDs; }
            set { this.stepIDs = value; }
        }
        private StepType stepName = StepType.CT1;
        public StepType StepName
        {
            get { return this.stepName; }
            set { this.stepName = value; }
        }


        /// <summary>
        /// Shop ID, = Shop Name
        /// </summary>
        private string shopID = "";
        public string ShopID
        {
            get { return this.shopID; }
            set { this.shopID = value; }
        }

        /// <summary>
        /// If select sub production or If query sub production
        /// </summary>
        private bool isHaveSubProduct = false;
        public bool IsHaveSubProduct
        {
            get { return this.isHaveSubProduct; }
            set { this.isHaveSubProduct = value; }
        }
        #endregion


        public ReportBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Public Modth 
        //public void GetDataTableRatio(DataTable dataSource, out double maxRatio, out double minRatio, params string[] columnNames)
        //{
        //    maxRatio = this.GetDataTableMaxRatio(dataSource, columnNames);
        //    minRatio = this.GetDataTableMinRatio(dataSource, columnNames);
        //}

        public double GetDataTableMaxRatio(DataTable dataSource, params string[] columnNames)
        {
            if (dataSource == null || dataSource.Rows.Count < 1)
                return 0;

            double max = this.GetDataTableValueByColumn(dataSource, columnNames[0], true);
            for (int i = 1; i < columnNames.Length; i++)
            {
                double temp = this.GetDataTableValueByColumn(dataSource, columnNames[i], true);
                max = Math.Max(max, temp);
            }
            return max;
        }

        /// <summary>
        /// noinclude %, Ratio 's column, get they rows max
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public double GetMaxOutput(DataTable dataSource)
        {
            return this.GetMaxOutput(dataSource, "%", "Ratio");
        }
        public double GetMaxOutput(DataTable dataSource, params string[] noColumnNames)
        {
            if (dataSource == null || dataSource.Columns.Count < 1 || dataSource.Rows.Count < 1)
                return 0.0;

            DataTable dt = dataSource.Copy();
            for (int i = dt.Columns.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < noColumnNames.Length; j++)
                {
                    if (dt.Columns[i].DataType.Name.Equals("String") || dt.Columns[i].DataType.Name.Equals("DateTime"))
                    {
                        dt.Columns.RemoveAt(i);
                        break;
                    }
                    if (dt.Columns[i].ColumnName.ToUpper().Contains(noColumnNames[j].ToUpper()))
                    {
                        dt.Columns.RemoveAt(i);
                        break;
                    }
                }
            }
            
            double maxOutput = 0.0;
            foreach (DataRow row in dt.Rows)
            {
                double temp = 0.0;
                for (int i = 0; i < row.ItemArray.Length; i++)
                    temp += ConvertToDouble(row[i].ToString());
                maxOutput = Math.Max(maxOutput, temp);
            }
            return maxOutput;
        }

        // only for Output is one column right, or multi-output column is wrong
        public double GetDataTableMaxOutput(DataTable dataSource, params string[] columnNames)
        {
            if (dataSource == null || dataSource.Rows.Count < 1)
                return 0;

            double max = this.GetDataTableValueByColumn(dataSource, columnNames[0], true);
            for (int i = 1; i < columnNames.Length; i++)
            {
                double temp = this.GetDataTableValueByColumn(dataSource, columnNames[i], true);
                max = Math.Max(max, temp);
            }
            return max;
        }

        public double GetDataTableMinRatio(DataTable dataSource, params string[] columnNames)
        {
            if (dataSource == null || dataSource.Rows.Count < 1)
                return 0;

            double min = this.GetDataTableValueByColumn(dataSource, columnNames[0], false);
            for (int i = 1; i < columnNames.Length; i++)
            {
                double temp = this.GetDataTableValueByColumn(dataSource, columnNames[i], false);
                min = Math.Min(min, temp);
            }
            return min;
        }


        // if isMax = true, get Ma
        private double GetDataTableValueByColumn(DataTable dataSource, string columnName, bool isMax)
        {
            if (dataSource == null || dataSource.Rows.Count < 1)
                return 0;

            double value = this.ConvertToDouble(dataSource.Rows[0][columnName].ToString());
            for (int i = 1; i < dataSource.Rows.Count; i++)
            {
                double temp = this.ConvertToDouble(dataSource.Rows[i][columnName].ToString());
                if (isMax == true)
                    value = Math.Max(value, temp);
                else
                    value = Math.Min(value, temp);
            }
            return value;
        }
        //private double GetDataTableValueByColumn(DataTable dataSource, int columnIndex, bool isMax)
        //{
        //    if (dataSource == null || dataSource.Rows.Count < 1)
        //        return 0;

        //    double value = this.ConvertToDouble(dataSource.Rows[0][columnIndex].ToString());
        //    for (int i = 1; i < dataSource.Rows.Count; i++)
        //    {
        //        double temp = this.ConvertToDouble(dataSource.Rows[0][columnIndex].ToString());
        //        if (isMax == true)
        //            value = Math.Max(value, temp);
        //        else
        //            value = Math.Min(value, temp);
        //    }
        //    return value;
        //}
        private double ConvertToDouble(string source)
        {
            if (string.IsNullOrEmpty(source))
                return 0.00;

            if (source.Contains("%"))
            {
                source = source.Replace("%", "");
                return double.Parse(source) / 100;
            }
            return double.Parse(source);
        }



        #region Chart operation
        public double GetDisplayStep(double number)
        {
            return this.GetDisplayStep(number, 5);
        }
        public double GetDisplayStep(double number, int count)
        {
            return this.GetDisplayRatioStep(number, count);
        }
        public double GetDisplayRatioStep(double number)
        {
            return this.GetDisplayRatioStep(number, 5);
        }
        public double GetDisplayRatioStep(double number, int count)
        {
            if (number >= 1.0 || number <= 0)
                return 0.2;
            else if (number >= 0.01)
                return Math.Round(number / count, 5);
            else if(number >= 0.0001)
                return Math.Round(number / count, 7);
            else
                return Math.Round(number / count, 9);
        }

        public double GetDisplayOutputStep(double number)
        {
            return this.GetDisplayOutputStep(number, 5);
        }

        public double GetDisplayOutputStep(double number, int count)
        {
            //if (number < 1)
            //    return this.GetDisplayRatioStep(number, count);
            if (number < 5.5)
                return 1;
            return Math.Round(number / count, 2);
        }

        public double GetDisplayOutputStep(double number, int count, bool NoRounding)
        {
            //if (number < 1)
            //    return this.GetDisplayRatioStep(number, count);
            if (NoRounding = false && number < 5.5)
                return 1;
            return Math.Round(number / count, 2);
        }

        public double GetDisplayMaxRatio(double number)
        {
            number = number * 1.1;
            if (number > 1.1 || number <= 0)
                return 1.1;

            return number;
        }

        public double GetDisplayMaxOutput(double number)
        {
            number = number * 1.1;
            if(number < 5.5)
                return 5.5;
            return number;            
        }

        public double GetDisplayMaxOutput(double number, bool NoRounding)
        {
            number = number * 1.1;
            if (NoRounding = false && number < 5.5)
                return 5.5;
            return number;
        }
        
        public void InitChart(ChartFX.WebForms.Chart chart)
        {
            if (chart == null)
                return;

            chart.Titles.Clear();
            chart.Series.Clear();
            chart.Points.Clear();
        }
        public void SetChartStyle(ChartFX.WebForms.Chart chart)
        {
            if (chart == null)
                return;

            chart.MenuBar.Visible = false;
            chart.ToolBar.Visible = false;
            chart.ContextMenus = false;
            chart.DataGrid.Visible = false;

            chart.View3D.Enabled = false;
            chart.EnableViewState = false;

            chart.LegendBox.Dock = ChartFX.WebForms.DockArea.Top;
            //chart.LegendBox.Height = 60;
            chart.LegendBox.AutoSize = true; 
            chart.LegendBox.Border = DockBorder.None;
            chart.ToolBar.Dock = DockArea.Top;

            chart.Width = 750;
            chart.Height = 350;
        }

        public void SetChartSeriesStyle(ChartFX.WebForms.Chart chart)
        {
            this.SetChartSeriesStyle(chart, null);
        }
        public void SetChartSeriesStyle(ChartFX.WebForms.Chart chart, DataTable dataSource)
        {
            for (int i = 0; i < chart.Series.Count; i++)
            {
                // Todo: display the point information in the buttom of the line
                if (chart.AxisX.Labels.Count > 0)
                {
                    int xpos = this.GetLastPos(dataSource, chart.Series[i].Text);
                    if (xpos < 0 || xpos > chart.AxisX.Labels.Count - 1)
                        xpos = chart.AxisX.Labels.Count - 1;
                    chart.Points[i, xpos].PointLabels.Visible = true;
                    if (chart.Series[i].Text.ToUpper().Contains("QTY") || chart.Series[i].Text.ToUpper().Contains("OUTPUT"))
                    {
                        chart.Series[i].Gallery = Gallery.Bar;
                        chart.Points[i, xpos].PointLabels.LineAlignment = System.Drawing.StringAlignment.Center;
                    }
                    else if (chart.Series[i].Text.Contains("%") || chart.Series[i].Text.ToUpper().Contains("RATIO") || chart.Series[i].Text.ToUpper().Contains("YIELD"))
                    {
                        chart.Series[i].Gallery = Gallery.Lines;
                        for (int j = 0; j < chart.AxisX.Labels.Count; j++)
                            chart.Points[i, j].PointLabels.LineAlignment = System.Drawing.StringAlignment.Far;
                    }
                }

                // Todo: 
                if (chart.Series[i].Text.Contains("$"))
                    chart.Series[i].Text = chart.Series[i].Text.Replace("$", "_");
            }
        }
        private int GetLastPos(DataTable dataSource, string columnName)
        {
            if(dataSource == null || dataSource.Rows.Count <1 || dataSource.Columns.Count < 1)
                return -1;

            for (int i = 0; i < dataSource.Columns.Count; i++)
            {
                string temp = dataSource.Columns[i].ColumnName;
                if (temp.Contains(columnName) == true)
                {
                    for (int j = dataSource.Rows.Count - 1; j > -1; j--)
                    {
                        object value = dataSource.Rows[j][columnName];
                        if (value != DBNull.Value && string.IsNullOrEmpty(value.ToString()) == false)
                            return j;
                    }
                }
            }
            return 0;
        }

        public void SetChartTitle(ChartFX.WebForms.Chart chart, string title)
        {
            if (chart == null)
                return;
            if (string.IsNullOrEmpty(title) == true)
                return;

            chart.Titles.Clear();
            List<TitleDockable> listTitle = new List<TitleDockable>();
            System.Drawing.Font font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            int start = 0;
            for (; ; )
            {
                int len = 75;
                if (start + len < title.Length)
                {
                    if (title[start + len].Equals('_') == false || title[start + len].Equals(',') == false)
                    {
                        for (int i = start + len; i > start + len - 20; i--)
                            if (title[i] == '_' || title[i] == ',')
                            {
                                len = i - start;
                                break;
                            }
                    }
                    TitleDockable td = new TitleDockable(title.Substring(start, len + 1));
                    td.Font = font;
                    listTitle.Add(td);
                }
                else
                {
                    TitleDockable td = new TitleDockable(title.Substring(start));
                    td.Font = font;
                    listTitle.Add(td);
                    break;
                }
                start = start + len + 1;
            }

            chart.LegendBox.Titles.Clear();
            chart.LegendBox.AutoSize = true; 
            for (int i = 0; i < listTitle.Count; i++)
                chart.LegendBox.Titles.Add(listTitle[i]);
            chart.LegendBox.ContentLayout = ContentLayout.Spread;
            chart.LegendBox.Border = DockBorder.None;
            chart.LegendBox.SizeToFit();
        }
        #endregion


        #region Table operation
        /// <summary>
        /// Add control in the table by Height
        /// </summary>
        /// <param name="table"></param>
        /// <param name="control"></param>
        public void AddControlInTable(Table table, Control control, Align align)
        {
            this.AddControlInTable(table, control, OrderBy.Row, 1, Align.Left);
        }
        // delete it
        public void AddControlInTable(Table table, Control control, PositionType position, int count)
        {
            if (table == null)
                table = new Table();

            switch (position)
            {
                case PositionType.Row:
                    if (table.Rows.Count < 1)
                        this.AddCellInNewRow(table, control, Align.Center);
                    else
                    {
                        int rows = table.Rows.Count;
                        if (table.Rows[rows - 1].Cells.Count < count)
                        {
                            TableCell tc = new TableCell();
                            tc.Controls.Add(control);
                            table.Rows[rows - 1].Cells.Add(tc);
                        }
                        else
                            this.AddCellInNewRow(table, control, Align.Center);
                    }
                    break;
                case PositionType.Column:
                    break;
            }
        }
        public void AddControlInTable(Table table, Control control, string title, OrderBy orderByRowOrColumn, int count, Align align)
        {
            if(table == null)
                table = new Table();

            switch (orderByRowOrColumn)
            {
                case OrderBy.Row:
                    int rows = table.Rows.Count;
                    if (rows < 1 || table.Rows[rows - 1].Cells.Count >= count) // 
                    {
                        this.AddCellInTable(table, new LiteralControl(title), rows + 1, 1, align);
                        this.AddCellInTable(table, control, rows + 2, 1, align);
                    }
                    else
                    {
                        int columns = table.Rows[rows - 1].Cells.Count;
                        this.AddCellInTable(table, new LiteralControl(title), rows - 1, columns + 1, align);
                        this.AddCellInTable(table, control, rows, columns + 1, align);
                    }
                    break;
                case OrderBy.Column:
                    break;
            }
        }

        private void AddCellInTable(Table table, Control control, int rowCount, int columnCount, Align align)
        {
            TableCell cell = new TableCell();
            cell.Controls.Add(control);
            this.SetCellAlign(cell, align);

            if (table.Rows.Count < rowCount)
            {
                TableRow row = new TableRow();
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }
            else
                table.Rows[rowCount - 1].Cells.Add(cell);
        }


        public void AddControlInTable(Table table, Control control, OrderBy orderByRowOrColumn, int count, Align align)
        {
            if (table == null)
                table = new Table();

            switch (orderByRowOrColumn)
            {
                case OrderBy.Row:
                    if (table.Rows.Count < 1)
                        this.AddCellInNewRow(table, control, align);
                    else
                    {
                        int rows = table.Rows.Count;
                        if (table.Rows[rows - 1].Cells.Count < count)
                        {
                            TableCell cell = new TableCell();
                            cell.Controls.Add(control);
                            this.SetCellAlign(cell, align);
                            table.Rows[rows - 1].Cells.Add(cell);
                        }
                        else
                            this.AddCellInNewRow(table, control, align);
                    }
                    break;
                case OrderBy.Column:
                    break;
            }
        }

        private void AddCellInNewRow(Table table, Control control, string title, Align align)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Controls.Add(control);
            this.SetCellAlign(cell, align);
            row.Cells.Add(cell);
            table.Rows.Add(row);
        }

        private void AddCellInNewRow(Table table, Control control, Align align)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Controls.Add(control);
            this.SetCellAlign(cell, align); 
            row.Cells.Add(cell);
            table.Rows.Add(row);
        }
        private void SetCellAlign(TableCell cell, Align align)
        {
            switch (align)
            {
                case Align.Left:
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    break;
                case Align.Center:
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    break;
                case Align.Right:
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    break;
                case Align.Top:
                    cell.VerticalAlign = VerticalAlign.Top;
                    break;
                case Align.Middle:
                    cell.VerticalAlign = VerticalAlign.Middle;
                    break;
                case Align.Bottom:
                    cell.VerticalAlign = VerticalAlign.Bottom;
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region GridView)
        public GroupHeaderRadGrid CreateRadGrid(DataTable dataSource)
        {
            // set RadGrid style
            GroupHeaderRadGrid grid = new GroupHeaderRadGrid();
            grid.AutoHeaderGroupBySplit = true;
            grid.Skin = "Office2007";
            //grid.Width = new Unit("100%");
            grid.HeaderTextSplitChar = '$';
            grid.BorderStyle = BorderStyle.None;
            grid.EnableViewState = false;
            grid.Visible = true;
            if (dataSource.Columns.Count < 1)
            {
                dataSource.Columns.Add("Null");
                dataSource.Rows.Add("No records selected");
            }
            else if (dataSource.Rows.Count < 1)
                dataSource.NewRow()[0] = "No records selected";

            grid.DataSource = dataSource;
            grid.Rebind();

            return grid;
        }

        #endregion

        #endregion

        #region Title and Chart Name operate
        public virtual string GetNameByStep()
        {
            return this.GetListString(this.stepIDs);
        }
        public virtual string GetNameByMainProduct()
        {
            return this.GetListString(this.mainProductNames);
        }
        public virtual string GetNameBySubProduct()
        {
            return this.GetListString(this.subProductNames);
        }
        public virtual string GetNameByProduct()
        {
            if (this.SubProductNames.Count > 0)
                return this.productFullName.Replace("'", "") + "_";
            else if (this.MainProductNames.Count > 0)
                return this.GetNameByMainProduct();
            else
                return string.Empty;
        }
        public virtual string GetNameByModel()
        {
            return this.GetListString(this.model);
        }
        public virtual string GetNameByBatchType()
        {
            return this.batchType.Replace("'", "").Replace(",", "+");
        }
        public virtual string GetNameByTimeRang()
        {
            return this.startDateTime.Replace("'", "") + "-" + this.endDateTime.Replace("'", "");
        }

        public string GetFullProdName(DataTable dataSource, bool isHaveSubprod)
        {
            if (dataSource == null || dataSource.Columns.Count < 1)
                return string.Empty;

            string prods = string.Empty;
            if (isHaveSubprod == false) // main prod
            {
                foreach(DataRow row in dataSource.Rows)
                    prods += string.Format(",{0}", row[0]);         
            }
            else
            {
                List<string> list = new List<string>();
                foreach (DataRow row in dataSource.Rows)
                {
                    string temp = string.Format("{0}", row[0]);
                    if (this.ItemInList(list, temp) == false)
                        list.Add(temp);
                }

                for(int i=0; i< list.Count; i++)
                {
                    prods += string.Format(",{0}", list[i]);

                    string subprods = string.Empty, filter = string.Format("{0}='{1}'", dataSource.Columns[0].ColumnName, list[i]);
                    foreach (DataRow rowSub in dataSource.Select(filter))
                        subprods += string.Format(",{0}", rowSub[1]);
                    if (subprods.Length > 0)
                        prods += string.Format("({0})", subprods.Substring(1));
                }                
            }
            if (prods.Length > 0)
                prods = prods.Substring(1);
            return prods;
        }

        private bool ItemInList(IList<string> list, string item)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i].Equals(item) == true)
                    return true;
            return false;
        }
        private string GetListString(IList<string> list)
        {
            if (list == null)
                return "";
            string temp = "";
            if (list.Count < 1)
                return temp;
            for (int i = 0; i < list.Count; i++)
                temp += "," + list[i].Replace("'", "");
            if (temp.Length > 0)
                temp = temp.Substring(1) + "_";
            return temp;
        }
        #endregion


        #region IList<string> operation
        public IList<string> ListCopy(IList<string> listSource)
        {
            IList<string> list = new List<string>();
            for (int i = 0; i < listSource.Count; i++)
                list.Add(listSource[i]);
            return list;
        }
        public void ListCopyTo(IList<string> listSource, IList<string> listDest)
        {
            listDest.Clear();
            for (int i = 0; i < listSource.Count; i++)
                listDest.Add(listSource[i]);
        }

        public IList<string> ListMove(IList<string> listSource)
        {
            IList<string> list = this.ListCopy(listSource);
            listSource.Clear();
            return list;
        }
        public void ListMoveTo(IList<string> listSource, IList<string> listDest)
        {
            this.ListCopyTo(listSource, listDest);
            listSource.Clear();
        }
        #endregion


        #region public static method
        public static IList<string> ConvertStringToList(string source, string spilt)
        {
            IList<string> list = new List<string>();

            if (string.IsNullOrEmpty(source))
                return list;

            string[] arr = source.Split(char.Parse(spilt));
            for (int i = 0; i < arr.Length; i++)
            {
                string str = arr[i];
                if (string.IsNullOrEmpty(str) == false && str != spilt)
                    list.Add(str);
            }
            return list;
        }

        public static IList<string> ConvertDataTableToList(DataTable dataSource)
        {
            IList<string> list = new List<string>();
            if (dataSource == null || dataSource.Rows.Count < 1)
                return list;

            foreach (DataRow row in dataSource.Rows)
            {
                string temp = row[0].ToString().Trim();
                if (string.IsNullOrEmpty(temp) == false)
                    list.Add("'" + row[0].ToString().Trim().Replace("'", "") + "'");
            }
            return list;
        }

        public static string ConvertListToString(IList<string> list)
        {
            if (list == null)
                return "";

            string tmp = "";
            for (int i = 0; i < list.Count; i++)
                tmp = "," + list[i] + tmp;
            if (tmp.Length > 0)
                tmp = tmp.Substring(1);
            return tmp;
        }


        public static DateTime ConvertStringToDateTime(string source)
        {
            if (string.IsNullOrEmpty(source))
                return DateTime.Now;
            return DateTime.Parse(source);
        }

        public static int ConvertStringToInt(string source)
        {
            if (string.IsNullOrEmpty(source))
                return 0;
            source = source.Trim();
            for (int i = 0; i < source.Length; i++)
                if (source[i] < '0' || source[i] > '9')
                    return 0;
            return int.Parse(source);
        }
        public static DateTime ShiftDateToDateTime(string shiftDate)
        {
            if (string.IsNullOrEmpty(shiftDate))
                return DateTime.Now;

            shiftDate = shiftDate.Replace("'", "").Replace("-", "").Replace(" ", "");
            if (shiftDate.Length >= 4)
            {
                int year = int.Parse(shiftDate.Substring(0, 4));
                if (shiftDate.Length == 4)
                    return new DateTime(year, 1, 1);
                else if (shiftDate.Length >= 6)
                {
                    int month = int.Parse(shiftDate.Substring(4, 2));
                    if (shiftDate.Length == 6)
                        return new DateTime(year, month, 1);
                    else if (shiftDate.Length >= 8)
                    {
                        int day = int.Parse(shiftDate.Substring(6, 2));
                        if (shiftDate.Length == 8)
                            return new DateTime(year, month, day);
                        else if (shiftDate.Length >= 10)
                        {
                            int hour = int.Parse(shiftDate.Substring(8, 2));
                            if (shiftDate.Length == 10)
                                return new DateTime(year, month, day, hour, 0, 0);
                        }
                    }
                }
            }
            return DateTime.Now;
        }
        public static double GetDoubleValue(object source)
        {
            if (source == null)
                return 0.0;
            if (string.IsNullOrEmpty(source.ToString()))
                return 0.0;
            else
                return double.Parse(source.ToString());
        }
        #endregion
    }
}