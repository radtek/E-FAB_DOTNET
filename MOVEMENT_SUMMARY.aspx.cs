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


public partial class MOVEMENT_SUMMARY : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CFT"];

    string Date_str;

    DataSet ds_temp = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtEstimateStartDate.SelectedDate = DateTime.Now.AddDays(-0);



        }

        string sql_file_path = " select max(t.file_name) as file_name from rpt_mvmt_file t";
        ds_temp=func.get_dataSet_access(sql_file_path, conn);

        string link =ds_temp.Tables[0].Rows[0][0].ToString();

        if (link == "")
        {
            HyperLink1.Visible = false;

        }
        else
        {
            

            HyperLink1.Text = link;
            HyperLink1.NavigateUrl = Context.Request.ApplicationPath + "/Save_file/" + link;
            //HyperLink1.NavigateUrl = "http://" +Server.MachineName+"/E-FAB_dotnet/Save_file/" + link;
 
 

        }

    }
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        Date_str = txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "").Substring(0, 8);
        bind_data();
        bind_data2();


        Page_Load(null,null);

    }
    protected void btnExport_Click1(object sender, EventArgs e)
    {
        //撱箇亟xcel Application   
        Excel.Application ExlApp;
        Excel.Workbook ExlBook;
        Excel.Worksheet ExlSheet;
        ExlApp = new Excel.Application();

        string SavePath = @"" + Server.MapPath(".") + "\\CF\\Save_file\\";
        string FileName = DateTime.Now.AddDays(0).ToString("yyyyMMddHHmmss") + "_CF_MVMT_AVG";


        // 澭Excel Message    
        ExlApp.Application.DisplayAlerts = false;
        ExlApp.Application.Visible = false;       // 閮剔演rue,槼賊憪撠望憿舐遷xcel瑼.   

        // 撱箇俐orkBook Object   
        // 皝瘜1 (憪憳銝壿heet)   
        //ExlBook = ExlApp.Workbooks.Add(Missing.Value);

        //緪?蝭甈EXCEL

        ExlBook = ExlApp.Workbooks.Add(Server.MapPath(".") + "\\CF\\CF_MVMT_AVG.xls");

        // 皝瘜2 (憪憳銝壿heet)   
        //ExlBook = (Excel.Workbook)ExlApp.Workbooks.Add(1);

        // 閮剖圃xcel緛辣璅憿   
        //ExlBook.Windows.get_Item(1).Caption = "鈭箏撌交瘥靘敼?梯”";

        // 撱箇俐orkSheet Object (蝐蝚砌詨樉貉身摰憒憭槫Ｗ嗾壿heet)   
        //ExlSheet = (Excel.Worksheet)ExlBook.Sheets.Add(Missing.Value, Missing.Value, 3, Missing.Value);

        // 緪啁洵撟曉Sheet   
        ExlSheet = (Excel.Worksheet)ExlBook.Worksheets.get_Item("Sheet2");

        // 閮剖娟heet樄迂   
        //ExlSheet.Name = "MVMT_AVG";
        Date_str = txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "").Substring(0, 8);

        ExlSheet.Cells[1, 2] = Date_str;


        ExlSheet.Cells[2, 1] = "DAILY_MOVE";

        Date_str = txtEstimateStartDate.SelectedDate.Value.ToString("yyyyMMdd").Replace("/", "").Substring(0, 8);

        string sql_str = " select 'DAILY_MOVE' from dual                          " +
" union all                                              " +
" select * from (                                        " +
" select t.shift_date from shift_date t                  " +
" where substr(t.shift_date,0,6)=substr('" + Date_str + "',0,6)  " +
" order by shift_date                                    " +
" )t2                                                    " +
" union all                                              " +
" select 'MAX_MVMT' from dual                            " +
"                                                        " +
" union all                                              " +
"                                                        " +
" select 'AVG_MOVE' from dual                            ";

        ds_temp = func.get_dataSet_access(sql_str, conn);

        int excel_title_X = 2;
        int excel_title_Y = 1;

        for (int i = 0; i < ds_temp.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < ds_temp.Tables[0].Columns.Count; j++)
            {
                ExlSheet.Cells[excel_title_X, excel_title_Y + i] = ds_temp.Tables[0].Rows[i][j].ToString();
            }
        }

        ds_temp.Clear();

        System.Data.DataTable DT_EXCEL = new System.Data.DataTable();
       

        DT_EXCEL = bind_data();


        int excel_num = 3;
        int excel_start_location = 1;
        for (int i = 0; i < DT_EXCEL.Rows.Count; i++)
        {

            for (int j = 0; j < DT_EXCEL.Columns.Count; j++)
            {
                ExlSheet.Cells[excel_num + i, excel_start_location + j] = DT_EXCEL.Rows[i][j].ToString();
            }


        }

        DT_EXCEL.Clear();

        DT_EXCEL = bind_data2();


        ExlSheet.Cells[21, 1] = "DAILY_MOVE";
        ExlSheet.Cells[21, 2] = "SHIFT";

         sql_str =
"  select 'DAILY_MOVE' from dual                                  " +
"  union all                                                      " +
"  select 'SHIFT' from dual                                       " +
" union all                                                       " +
" select * from (                                                 " +
" select t.shift_date from shift_date t                           " +
" where substr(t.shift_date,0,6)=substr('" + Date_str + "',0,6)   " +
" order by shift_date                                             " +
" )t2                                                             " +
" union all                                                       " +
" select 'MAX_MVMT' from dual                                     " +
"                                                                 " +
" union all                                                       " +
"                                                                 " +
" select 'AVG_MOVE' from dual                                     ";

        ds_temp = func.get_dataSet_access(sql_str, conn);

        int initial_titleX = 21;
        int initial_titleY = 1;

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count-1; i++)
        {
            for (int j = 0; j <= ds_temp.Tables[0].Columns.Count-1; j++)
            {
                ExlSheet.Cells[initial_titleX+j, initial_titleY + i] = ds_temp.Tables[0].Rows[i][j].ToString();
            }
           

        }
                                                                  

        ds_temp = func.get_dataSet_access(sql_str, conn);


        excel_num = 22;
        excel_start_location = 1;
        for (int i = 0; i < DT_EXCEL.Rows.Count; i++)
        {

            for (int j = 0; j < DT_EXCEL.Columns.Count; j++)
            {
                ExlSheet.Cells[excel_num + i, excel_start_location + j] = DT_EXCEL.Rows[i][j].ToString();
            }


        }








        // 撠撘憛怠乍xcel樉砌?  
        //ExlSheet.Cells[1, 1] = "ABC";

        //ExlSheet.Cells[1, 2] = "ABCDEFG_FOOL";



        //ExlSheet = (Excel.Worksheet)ExlBook.Worksheets.get_Item(2);

        //ExlSheet.Cells[1, 1] = "123456789";

        //ExlSheet.Cells[1, 2] = "3481_FUCK";


        // 憿舐遷xcel瑼,霈雿輻刻緪雿摮瑼縃氄芣? 
        ExlApp.Visible = true;
        ExlApp.UserControl = true;

        ExlBook.SaveAs(SavePath + FileName, Excel.XlFileFormat.xlWorkbookNormal,
        null, null, false, false, Excel.XlSaveAsAccessMode.xlShared,
        false, false, null, null, null);

        //關閉文件   
        ExlBook.Close(null, null, null);
        ExlApp.Workbooks.Close();
        ExlApp.Quit();

        //釋放資源   
        System.Runtime.InteropServices.Marshal.ReleaseComObject(ExlApp);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(ExlSheet);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(ExlBook);
        ExlSheet = null;
        ExlBook = null;
        ExlApp = null;

        string user_id = "oscar";

        string sql_str2 = " insert into rpt_mvmt_file                          " +
"   (file_path, file_name, user_id, dttm)          " +
" values                                             " +
"   ('" + Server.MapPath(".") + "\\CF\\Save_file\\" + FileName + ".xls', '" + FileName + ".xls'" + ", '" + user_id + "', sysdate)            ";

        func.get_sql_execute(sql_str2, conn);



        Page_Load(null, null);



        // Excel.Chart chart = default(Excel.Chart);

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.RowIndex != -1)
        //    {
        //        int id = e.Row.RowIndex + 1;
        //        e.Row.Cells[0].Text = id.ToString();
        //    }

        //}

    }

    protected System.Data.DataTable bind_data()
    {

        string sql_str = " select t4.*,t5.seq from (select t3.shift_day,t3.step,sum(t3.move_qty)move_qty from (  " +
"                                                                                       " +
" select t.shift_day,t.step,sum(t.move_qty)move_qty from eis_daily_hour_wipmove_sum t   " +
" where substr(t.shift_day,0,6)=substr('" + Date_str + "',0,6)                                  " +
" and t.step in(                                                                        " +
"                                                                                       " +
" 'BM_Line',                                                                            " +
" 'R_Line',                                                                             " +
" 'B_Line',                                                                             " +
" 'G_Line',                                                                             " +
" 'IF_Line',                                                                            " +
" 'ITO_Line',                                                                           " +
" 'ITO2_Line',                                                                          " +
" 'PS_Line',                                                                            " +
" 'IS_Line',                                                                            " +
" 'Shipping',                                                                           " +
" 'BM_OCLine',                                                                          " +
" 'R_OCLine',                                                                           " +
" 'B_OCLine',                                                                           " +
" 'G_OCLine',                                                                           " +
" 'PS_OCLine',                                                                          " +
" 'I3_IF',                                                                              " +
" 'I3_IS'                                                                               " +
"                                                                                       " +
"                                                                                       " +
" )                                                                                     " +
"                                                                                       " +
" group by shift_day,step                                                               " +
"                                                                                       " +
" union all                                                                             " +
"                                                                                       " +
" select t21.shift_date,t20.stage,t20.qty from rpt_mvmt_stage t20,shift_date t21        " +
" where substr(t21.shift_date,0,6)=substr('" + Date_str + "',0,6)                               " +
"                                                                                       " +
" )t3                                                                                   " +
" group by shift_day,step                                                               " +
" )t4,rpt_mvmt_stage t5                                                                 " +
" where t4.step=t5.stage                                                                ";





        ds_temp = func.get_dataSet_access(sql_str, conn);

        System.Data.DataTable dt_Adjust = new System.Data.DataTable();

        dt_Adjust = rotation(ds_temp, "STEP", "SHIFT_DAY");

        GridView1.DataSource = dt_Adjust;
        //GridView1.DataSource = ds_main;
        GridView1.DataBind();
        ds_temp.Clear();
        return dt_Adjust;

    }

    protected System.Data.DataTable bind_data2()
    {

        string sql_str = " select t4.*,t5.seq from (select t3.shift_day,t3.shift_hour,t3.step,sum(t3.move_qty)move_qty from (              " +
"                                                                                                                 " +
" select t.shift_day,case when t.shift_hour>=7 and t.shift_hour<19  then 'D' else 'N' end shift_hour,t.step       " +
" ,sum(t.move_qty)move_qty from eis_daily_hour_wipmove_sum t                                                      " +
" where substr(t.shift_day,0,6)=substr('" + Date_str + "',0,6)                                                            " +
" and t.step in(                                                                                                  " +
"                                                                                                                 " +
" 'BM_Line',                                                                                                      " +
" 'R_Line',                                                                                                       " +
" 'B_Line',                                                                                                       " +
" 'G_Line',                                                                                                       " +
" 'IF_Line',                                                                                                      " +
" 'ITO_Line',                                                                                                     " +
" 'ITO2_Line',                                                                                                    " +
" 'PS_Line',                                                                                                      " +
" 'IS_Line',                                                                                                      " +
" 'Shipping',                                                                                                     " +
" 'BM_OCLine',                                                                                                    " +
" 'R_OCLine',                                                                                                     " +
" 'B_OCLine',                                                                                                     " +
" 'G_OCLine',                                                                                                     " +
" 'PS_OCLine',                                                                                                    " +
" 'I3_IF',                                                                                                        " +
" 'I3_IS'                                                                                                         " +
"                                                                                                                 " +
"                                                                                                                 " +
" )                                                                                                               " +
"                                                                                                                 " +
" group by shift_day,step,shift_hour                                                                              " +
"                                                                                                                 " +
" union all                                                                                                       " +
"                                                                                                                 " +
" select t21.shift_date,t22.shift_hour,t20.stage,t20.qty from rpt_mvmt_stage t20,shift_date t21,                  " +
" (                                                                                                               " +
" select 'D' as shift_hour   from dual                                                                            " +
" union                                                                                                           " +
" select 'N' from dual                                                                                            " +
" )t22                                                                                                            " +
" where substr(t21.shift_date,0,6)=substr('" + Date_str + "',0,6)                                                         " +
"                                                                                                                 " +
" )t3                                                                                                             " +
" group by shift_day,shift_hour,step                                                                              " +
" )t4,rpt_mvmt_stage t5                                                                                           " +
" where t4.step=t5.stage                                                                                          ";






        ds_temp = func.get_dataSet_access(sql_str, conn);

        System.Data.DataTable dt_Adjust = new System.Data.DataTable();

        dt_Adjust = rotation2(ds_temp, "STEP", "SHIFT_DAY");

        GridView2.DataSource = dt_Adjust;
        //GridView1.DataSource = ds_main;
        GridView2.DataBind();
        ds_temp.Clear();
        return dt_Adjust;

    }





    private System.Data.DataTable rotation(DataSet ds, string row_name, string column_name)
    {
        DataView dv = ds.Tables[0].DefaultView;

        System.Data.DataTable ROWTable, COLTABLE;

        ROWTable = ds.Tables[0].DefaultView.ToTable(true, row_name, "SEQ");
        COLTABLE = ds.Tables[0].DefaultView.ToTable(true, column_name);

        DataView ROWDV = ROWTable.DefaultView;
        DataView COLDV = COLTABLE.DefaultView;
        ROWDV.Sort = "SEQ ";
        COLDV.Sort = column_name;

        System.Data.DataTable dtNew = new System.Data.DataTable();

        dtNew.Columns.Add("Move_Daily", typeof(string));

        //dtNew.Columns.Add("Qty", typeof(string));

        //蝵?撜鞊Ｙ▊lumn澭?
        /*for (int i = 0; i < dateTable.Rows.Count; i++)
        {
            dtNew.Columns.Add(dateTable.Rows[i][column_name].ToString(), typeof(string));
        }*/

        //string[] arr_date ={ };

        //for (int K = 0; K < 12; K++)
        //{

        //    arr_date[K] = DateTime.Now.Year.ToString()  ;

        ////}


        for (int i = 0; i <= COLDV.Count - 1; i++)
        {

            dtNew.Columns.Add(COLDV[i][column_name].ToString(), typeof(string));


        }

        int colnum = 0;

        for (int i = 0; i < ROWDV.Count; i++)
        {
            //豍 ROW ? dtNew

            DataRow drNew = dtNew.NewRow();
            drNew["Move_Daily"] = ROWDV[i][row_name].ToString();
            //drNew[1] = "Rework";
            colnum = 0;


            //foreach (DataRow dr in COLTABLE.Rows)
            //{
            //    //賥脖漲甇牽w?頨鋆撊lumn氄寞?
            //    dv.RowFilter = column_name + "='" + dr[0].ToString() + "' and " + row_name + "='" + ROWDV[i][row_name].ToString() + "' ";
            //    if (dv.Count > 0)
            //    {
            //        //drNew[colnum + 2] = (Convert.ToDouble(dv[0]["STEPID"])).ToString("#,##0");
            //       // drNew[colnum + 2] = dv[0]["STEPID"].ToString();
            //       // drNew[colnum + 3] = dv[0]["EQPID"].ToString();
            //        drNew[colnum + 2] = dv[0]["REWORK"].ToString();
            //       // drNew[colnum + 5] = dv[0]["TOTAL"].ToString();

            //    }

            //    else
            //        drNew[colnum + 2] = "0";

            //    colnum++;
            //}


            //Oscar 20100519
            for (int j = 0; j <= COLTABLE.Rows.Count - 1; j++)
            {
                dv.RowFilter = column_name + "='" + COLDV[j][0].ToString() + "' and " + row_name + "='" + ROWDV[i][row_name].ToString() + "' ";
                if (dv.Count > 0)
                {
                    //drNew[colnum + 2] = (Convert.ToDouble(dv[0]["STEPID"])).ToString("#,##0");
                    // drNew[colnum + 2] = dv[0]["STEPID"].ToString();
                    // drNew[colnum + 3] = dv[0]["EQPID"].ToString();
                    drNew[colnum + 1] = dv[0]["MOVE_QTY"].ToString();
                    // drNew[colnum + 5] = dv[0]["TOTAL"].ToString();


                }

                else
                    drNew[colnum + 1] = "N/A";



                colnum++;

            }

            dtNew.Rows.Add(drNew);


            //drNew = dtNew.NewRow();
            //drNew[txtCalendar1.Text.Replace("/", "")] = ROWDV[i][row_name].ToString();
            //drNew[1] = "Total";
            //colnum = 0;
            //for (int j = 0; j <= COLTABLE.Rows.Count - 1; j++)
            //{
            //    dv.RowFilter = column_name + "='" + COLTABLE.Rows[j][0].ToString() + "' and " + row_name + "='" + ROWDV[i][row_name].ToString() + "' ";
            //    if (dv.Count > 0)
            //    {
            //        //drNew[colnum + 2] = (Convert.ToDouble(dv[0]["STEPID"])).ToString("#,##0");
            //        // drNew[colnum + 2] = dv[0]["STEPID"].ToString();
            //        // drNew[colnum + 3] = dv[0]["EQPID"].ToString();
            //        drNew[colnum + 2] = dv[0]["TOTAL"].ToString();
            //        // drNew[colnum + 5] = dv[0]["TOTAL"].ToString();

            //    }

            //    else
            //        drNew[colnum + 2] = "0";

            //    colnum++;
            //}
            //dtNew.Rows.Add(drNew);



        }

        dtNew = DT_Add_Column(dtNew, "STEP", "Max_MVMT", "Avg_move");

        return dtNew;
    }

    private System.Data.DataTable rotation2(DataSet ds, string row_name, string column_name)
    {
        DataView dv = ds.Tables[0].DefaultView;

        System.Data.DataTable ROWTable, COLTABLE;

        ROWTable = ds.Tables[0].DefaultView.ToTable(true, row_name, "SEQ");
        COLTABLE = ds.Tables[0].DefaultView.ToTable(true, column_name);

        DataView ROWDV = ROWTable.DefaultView;
        DataView COLDV = COLTABLE.DefaultView;
        ROWDV.Sort = "SEQ ";
        COLDV.Sort = column_name;

        System.Data.DataTable dtNew = new System.Data.DataTable();

        dtNew.Columns.Add("Move_Daily", typeof(string));

        dtNew.Columns.Add("SHIFT", typeof(string));

        //蝵?撜鞊Ｙ▊lumn澭?
        /*for (int i = 0; i < dateTable.Rows.Count; i++)
        {
            dtNew.Columns.Add(dateTable.Rows[i][column_name].ToString(), typeof(string));
        }*/

        //string[] arr_date ={ };

        //for (int K = 0; K < 12; K++)
        //{

        //    arr_date[K] = DateTime.Now.Year.ToString()  ;

        ////}


        for (int i = 0; i <= COLDV.Count - 1; i++)
        {

            dtNew.Columns.Add(COLDV[i][column_name].ToString(), typeof(string));


        }

        int colnum = 1;

        for (int i = 0; i < ROWDV.Count; i++)
        {
            //豍 ROW ? dtNew

            DataRow drNew = dtNew.NewRow();
            drNew["Move_Daily"] = ROWDV[i][row_name].ToString();
            drNew[1] = "D";
            colnum = 1;


            //foreach (DataRow dr in COLTABLE.Rows)
            //{
            //    //賥脖漲甇牽w?頨鋆撊lumn氄寞?
            //    dv.RowFilter = column_name + "='" + dr[0].ToString() + "' and " + row_name + "='" + ROWDV[i][row_name].ToString() + "' ";
            //    if (dv.Count > 0)
            //    {
            //        //drNew[colnum + 2] = (Convert.ToDouble(dv[0]["STEPID"])).ToString("#,##0");
            //       // drNew[colnum + 2] = dv[0]["STEPID"].ToString();
            //       // drNew[colnum + 3] = dv[0]["EQPID"].ToString();
            //        drNew[colnum + 2] = dv[0]["REWORK"].ToString();
            //       // drNew[colnum + 5] = dv[0]["TOTAL"].ToString();

            //    }

            //    else
            //        drNew[colnum + 2] = "0";

            //    colnum++;
            //}


            //Oscar 20100519
            for (int j = 0; j <= COLTABLE.Rows.Count - 1; j++)
            {
                dv.RowFilter = column_name + "='" + COLDV[j][0].ToString() + "' and " + row_name + "='" + ROWDV[i][row_name].ToString() + "' and shift_hour='D'";
                if (dv.Count > 0)
                {
                    //drNew[colnum + 2] = (Convert.ToDouble(dv[0]["STEPID"])).ToString("#,##0");
                    // drNew[colnum + 2] = dv[0]["STEPID"].ToString();
                    // drNew[colnum + 3] = dv[0]["EQPID"].ToString();
                    drNew[j + 2] = dv[0]["MOVE_QTY"].ToString();
                    // drNew[colnum + 5] = dv[0]["TOTAL"].ToString();


                }

                else
                    drNew[j+2] = "N/A";



                colnum++;

            }

            dtNew.Rows.Add(drNew);


            DataRow drNew1 = dtNew.NewRow();
            drNew1["Move_Daily"] = ROWDV[i][row_name].ToString();
            drNew1[1] = "N";
            colnum = 2;


            //foreach (DataRow dr in COLTABLE.Rows)
            //{
            //    //賥脖漲甇牽w?頨鋆撊lumn氄寞?
            //    dv.RowFilter = column_name + "='" + dr[0].ToString() + "' and " + row_name + "='" + ROWDV[i][row_name].ToString() + "' ";
            //    if (dv.Count > 0)
            //    {
            //        //drNew[colnum + 2] = (Convert.ToDouble(dv[0]["STEPID"])).ToString("#,##0");
            //       // drNew[colnum + 2] = dv[0]["STEPID"].ToString();
            //       // drNew[colnum + 3] = dv[0]["EQPID"].ToString();
            //        drNew[colnum + 2] = dv[0]["REWORK"].ToString();
            //       // drNew[colnum + 5] = dv[0]["TOTAL"].ToString();

            //    }

            //    else
            //        drNew[colnum + 2] = "0";

            //    colnum++;
            //}


            //Oscar 20100519
            for (int j = 0; j <= COLTABLE.Rows.Count - 1; j++)
            {
                dv.RowFilter = column_name + "='" + COLDV[j][0].ToString() + "' and " + row_name + "='" + ROWDV[i][row_name].ToString() + "' and shift_hour='N'";
                if (dv.Count > 0)
                {
                    //drNew[colnum + 2] = (Convert.ToDouble(dv[0]["STEPID"])).ToString("#,##0");
                    // drNew[colnum + 2] = dv[0]["STEPID"].ToString();
                    // drNew[colnum + 3] = dv[0]["EQPID"].ToString();
                    drNew1[j + 2] = dv[0]["MOVE_QTY"].ToString();
                    // drNew[colnum + 5] = dv[0]["TOTAL"].ToString();


                }

                else
                    drNew1[j + 2] = "N/A";



                colnum++;

            }

            dtNew.Rows.Add(drNew1);


        



        }

        dtNew = DT_Add_Column2(dtNew, "STEP", "Max_MVMT", "Avg_move");

        return dtNew;
    }

    private System.Data.DataTable DT_Add_Column(System.Data.DataTable dt, string row_name, string column_name1, string column_name2)
    {
        DataView dv_origin = new DataView();


        DataView dv_addition = new DataView();
        DataView dv_addition1 = new DataView();


        string sql_add_col = "                                                                                                                                                                     " +
" select t5.step,t5.MAX_MVMT,t5.AVG_MOVE,t6.seq from (                                                                                                                " +
"                                                                                                                                                                     " +
"                                                                                                                                                                     " +
" select t4.step,sum(t4.MAX_MVMT)MAX_MVMT,sum(t4.AVG_MOVE)AVG_MOVE from (                                                                                             " +
"                                                                                                                                                                     " +
" select t2.step,max(t2.move_qty)Max_MVMT,round(avg(t2.move_qty))Avg_move from (select t.shift_day,t.step,sum(t.move_qty)move_qty from eis_daily_hour_wipmove_sum t   " +
" where substr(t.shift_day,0,6)=substr('" + Date_str + "',0,6)                                                                                                                " +
" and t.step in(                                                                                                                                                      " +
"                                                                                                                                                                     " +
" 'BM_Line',                                                                                                                                                          " +
" 'R_Line',                                                                                                                                                           " +
" 'B_Line',                                                                                                                                                           " +
" 'G_Line',                                                                                                                                                           " +
" 'IF_Line',                                                                                                                                                          " +
" 'ITO_Line',                                                                                                                                                         " +
" 'ITO2_Line',                                                                                                                                                        " +
" 'PS_Line',                                                                                                                                                          " +
" 'IS_Line',                                                                                                                                                          " +
" 'Shipping',                                                                                                                                                         " +
" 'BM_OCLine',                                                                                                                                                        " +
" 'R_OCLine',                                                                                                                                                         " +
" 'B_OCLine',                                                                                                                                                         " +
" 'G_OCLine',                                                                                                                                                         " +
" 'PS_OCLine',                                                                                                                                                        " +
" 'I3_IF',                                                                                                                                                            " +
" 'I3_IS'                                                                                                                                                             " +
"                                                                                                                                                                     " +
"                                                                                                                                                                     " +
" )                                                                                                                                                                   " +
"                                                                                                                                                                     " +
" group by shift_day,step ) t2                                                                                                                                        " +
" group by t2.step                                                                                                                                                    " +
"                                                                                                                                                                     " +
" union                                                                                                                                                               " +
" select t3.stage,0,0 from  rpt_mvmt_stage t3                                                                                                                         " +
"                                                                                                                                                                     " +
"                                                                                                                                                                     " +
" )t4                                                                                                                                                                 " +
" group by t4.step                                                                                                                                                    " +
"                                                                                                                                                                     " +
" )t5,rpt_mvmt_stage t6                                                                                                                                               " +
"                                                                                                                                                                     " +
" where t5.step=t6.stage                                                                                                                                              " +
"                                                                                                                                                                     " +
" order by t6.seq                                                                                                                                                     ";





        dt.Columns.Add(column_name1, typeof(string));
        dt.Columns.Add(column_name2, typeof(string));


        DataSet dc_add = new DataSet();

        dc_add = func.get_dataSet_access(sql_add_col, conn);

        dv_addition = dc_add.Tables[0].DefaultView;

        dv_addition1 = dc_add.Tables[0].DefaultView;

        dv_origin = dt.DefaultView;
        // dv_origin1 = dt.DefaultView;






        for (int i = 0; i < dc_add.Tables[0].Rows.Count; i++)
        {
            dv_addition.RowFilter = row_name + "='" + dc_add.Tables[0].Rows[i][row_name] + "'";

            if (dv_addition.Count > 0)
            {
                dt.Rows[i][column_name1] = dv_addition[0][column_name1].ToString();
                dt.Rows[i][column_name2] = dv_addition[0][column_name2].ToString();


            }







        }

        return dt;





    }

    private System.Data.DataTable DT_Add_Column2(System.Data.DataTable dt, string row_name, string column_name1, string column_name2)
    {
        DataView dv_origin = new DataView();


        DataView dv_addition = new DataView();
        DataView dv_addition1 = new DataView();


        string sql_add_col = " select tb3.*,tb4.seq from (                                                                                                                         " +
"                                                                                                                                                     " +
" select tb1.shift_day,tb1.step,tb1.shift_hour,max(tb1.Max_MVMT) as Max_MVMT,sum(Avg_move) as Avg_move  from (                                        " +
"                                                                                                                                                     " +
" select substr(t71.shift_day,0,6)as shift_day,t71.step,t71.shift_hour,max(t71.move_qty)as Max_MVMT,round(avg(t71.move_qty))as Avg_move from (        " +
"                                                                                                                                                     " +
" select t70.shift_day,t70.step,t70.shift_hour,sum(t70.move_qty)move_qty from (                                                                       " +
"                                                                                                                                                     " +
" select t.shift_day, t.step,case when t.shift_hour>=7 and t.shift_hour<19 then 'D' else 'N' end shift_hour  ,sum(t.move_qty) move_qty                " +
"   from eis_daily_hour_wipmove_sum t                                                                                                                 " +
"  where substr(t.shift_day, 0, 6) = substr('" + Date_str + "', 0, 6)                                                                                         " +
"    and t.step in ('BM_Line',                                                                                                                        " +
"                   'R_Line',                                                                                                                         " +
"                   'B_Line',                                                                                                                         " +
"                   'G_Line',                                                                                                                         " +
"                   'IF_Line',                                                                                                                        " +
"                   'ITO_Line',                                                                                                                       " +
"                   'ITO2_Line',                                                                                                                      " +
"                   'PS_Line',                                                                                                                        " +
"                   'IS_Line',                                                                                                                        " +
"                   'Shipping',                                                                                                                       " +
"                   'BM_OCLine',                                                                                                                      " +
"                   'R_OCLine',                                                                                                                       " +
"                   'B_OCLine',                                                                                                                       " +
"                   'G_OCLine',                                                                                                                       " +
"                   'PS_OCLine',                                                                                                                      " +
"                   'I3_IF',                                                                                                                          " +
"                   'I3_IS')                                                                                                                          " +
"  group by shift_day, step,shift_hour                                                                                                                " +
" )t70                                                                                                                                                " +
"                                                                                                                                                     " +
" group by shift_day,step,shift_hour                                                                                                                  " +
"                                                                                                                                                     " +
" )t71                                                                                                                                                " +
"                                                                                                                                                     " +
" group by  substr(shift_day,0,6),step,shift_hour                                                                                                     " +
"                                                                                                                                                     " +
"                                                                                                                                                     " +
"                                                                                                                                                     " +
" union all                                                                                                                                           " +
"                                                                                                                                                     " +
" select substr('" + Date_str + "',0,6)as shift_day,tt.stage, tt1.shift_hour, 0 as Max_MVMT,0 as Avg_move                                                     " +
"   from rpt_mvmt_stage tt,                                                                                                                           " +
"        (select 'D' as shift_hour                                                                                                                    " +
"           from dual                                                                                                                                 " +
"         union all                                                                                                                                   " +
"         select 'N' as shift_hour from dual) tt1,shift_date tt2                                                                                      " +
"         where substr(tt2.shift_date,0,8)=substr('" + Date_str + "',0,8)                                                                                     " +
"                                                                                                                                                     " +
"                                                                                                                                                     " +
" ) tb1                                                                                                                                               " +
"                                                                                                                                                     " +
"                                                                                                                                                     " +
" group by shift_day,step,shift_hour                                                                                                                  " +
"                                                                                                                                                     " +
" )tb3,rpt_mvmt_stage tb4                                                                                                                             " +
" where tb3.step=tb4.stage                                                                                                                            " +
"                                                                                                                                                     " +
" order by tb4.seq                                                                                                                                    ";






        dt.Columns.Add(column_name1, typeof(string));
        dt.Columns.Add(column_name2, typeof(string));


        DataSet dc_add = new DataSet();

        dc_add = func.get_dataSet_access(sql_add_col, conn);

        dv_addition = dc_add.Tables[0].DefaultView;

        dv_addition1 = dc_add.Tables[0].DefaultView;

        dv_origin = dt.DefaultView;
        // dv_origin1 = dt.DefaultView;






        for (int i = 0; i <= dc_add.Tables[0].Rows.Count-1; i++)
        {
            dv_addition.RowFilter = row_name + "='" + dc_add.Tables[0].Rows[i][row_name] + "' and shift_hour='" + dc_add.Tables[0].Rows[i]["shift_hour"] + "'";

            
            
            if (dv_addition.Count > 0)
            {
                dt.Rows[i][column_name1] = dv_addition[0][column_name1].ToString();
                dt.Rows[i][column_name2] = dv_addition[0][column_name2].ToString();


            }

        }

        return dt;





    }


}
