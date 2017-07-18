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

public partial class LH_REALITY_LH_LAM_DEFECT_ALARM_DETAIL : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_PARS1_OLE_LHEDA_LAM"];
    string sql = "";
    string sql_temp = "";

    DataSet ds_temp = new DataSet();

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");

    string today_last_1_hours = DateTime.Now.AddDays(-1 / 24).ToString("HH");

    string today_last_2_hours = DateTime.Now.AddDays(-2 / 24).ToString("HH");





    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["shift"] = Request.QueryString["shift"];
            Session["step_name"] = Request.QueryString["step_name"];
            Session["start"] = Request.QueryString["start"];
            Session["end"] = Request.QueryString["end"];
            Session["defectcode"] = Request.QueryString["defectcode"];

            //Session["shift"] = "D";
            //Session["step_name"] = "H3-HTH1";
            //Session["start"] = "2010122808";
            //Session["end"] = "2010122813";
            //Session["defectcode"] = "PCKKZ";

            bind_data1();

            bind_data2();


        }
    }


    protected DataSet bind_data1()
    {
        string sql = " select l.FACTORYNAME as FAB,p.prod_name,l.PRODUCTIONTYPE,l.OLDAREANAME as step_name, " +
"        d.MACHINENAME,d.DEFECTCODE,sum(1) as Defect_Qty                               " +
"   from lothistory_v l , defectdetail_v d ,                                           " +
"        ( select distinct p.PRODUCTSPECNAME,p.DESCRIPTION as prod_name                " +
"          from lh_productspec p where p.ACTIVESTATE = 'Active'                        " +
"        ) p                                                                           " +
"   where l.TIMEKEY between '" + Session["start"].ToString() + "3000'||'000000' and '" + Session["end"].ToString()+ "2959'||'999999'  " +
"     and l.FAIL = 1 and l.LOTNAME = d.LOTNAME and l.TIMEKEY = d.TIMEKEY               " +
"     and d.MAJORDEFECT = 'Y' and l.PRODUCTSPECNAME = p.PRODUCTSPECNAME                " +
"     and l.FACTORYNAME = 'H3'                                                         " +
"     AND p.prod_name = '3.5N90' and l.PRODUCTIONTYPE = 'ZTMC'                         " +
"     AND l.OLDAREANAME = '" + Session["step_name"].ToString() + "' and d.DEFECTCODE= '" + Session["defectcode"] .ToString()+ "'                          " +
" group by l.FACTORYNAME ,p.prod_name,l.PRODUCTIONTYPE,                                " +
"          l.OLDAREANAME ,d.MACHINENAME,d.DEFECTCODE                                   " +
" order by l.OLDAREANAME,d.MACHINENAME                                                 ";

     
                                                  

        sql = "select rownum,t.* from (" + sql + ")t  ";

        ds_temp = func.get_dataSet_access(sql, conn);

        GridView1.DataSource = ds_temp;
        GridView1.DataBind();



        return ds_temp;




    }


    protected DataSet bind_data2()
    {
        string sql = " select l.FACTORYNAME as FAB,decode(sign(to_char(l.EVENTTIME-7.5/24,'HH24')-12),-1,'D','N') as SHIFT, " +
"          p.prod_name,l.PRODUCTIONTYPE,l.OLDAREANAME as step_name,d.MACHINENAME,l.LOTNAME,            " +
"          d.DEFECTCODE,d.DEFECTDESC,'NG' as GRADE, L.EVENTTIME                                        " +
"   from lothistory_v l , defectdetail_v d ,                                                           " +
"        ( select distinct p.PRODUCTSPECNAME,p.DESCRIPTION as prod_name                                " +
"          from lh_productspec p where p.ACTIVESTATE = 'Active'                                        " +
"        ) p                                                                                           " +
"   where l.TIMEKEY between '" + Session["start"].ToString() + "3000'||'000000' and '" + Session["end"].ToString() + "2959'||'999999'                  " +
"     and l.FAIL = 1 and l.LOTNAME = d.LOTNAME and l.TIMEKEY = d.TIMEKEY                               " +
"     and d.MAJORDEFECT = 'Y' and l.PRODUCTSPECNAME = p.PRODUCTSPECNAME                                " +
"     and l.FACTORYNAME = 'H3'                                                                         " +
"     AND decode(sign(to_char(l.EVENTTIME-7.5/24,'HH24')-12),-1,'D','N') = '" + Session["shift"] .ToString()+ "'                         " +
"     AND p.prod_name = '3.5N90' and l.PRODUCTIONTYPE = 'ZTMC'                                         " +
"     AND l.OLDAREANAME = '" + Session["step_name"].ToString() + "' and d.DEFECTCODE= '" + Session["defectcode"].ToString() + "'                                          " +
" order by l.OLDAREANAME,d.MACHINENAME                                                                 ";




        sql = "select rownum,t.* from (" + sql + ")t  ";

        ds_temp = func.get_dataSet_access(sql, conn);

        GridView2.DataSource = ds_temp;
        GridView2.DataBind();



        return ds_temp;




    }
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        bind_data1();
    }
    protected void btnExport_Click1(object sender, EventArgs e)
    {
        GridView gv = new GridView();
        gv.DataSource = bind_data1();
        gv.DataBind();
        ExportExcel(gv);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
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


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //string strSql_file_name;
            //string snn1;

            //GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet();




            //           strSql_file_name = " select distinct (t3.file_name)            " +
            //"  from (                                   " +
            //"        select *                           " +
            //"          from night_inspection_file t     " +
            //"         where t.sn = '" + ((DataRowView)e.Row.DataItem)["sn"] + "'     " +
            //"         order by t.dttm desc) t3          ";



            //           ds = func.get_dataSet_access(strSql_file_name, conn);


            //           ((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0];
            //           ((DataList)e.Row.FindControl("DataList1")).DataBind();

            //           String Flag_satus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "open_close_flag"));

            //           if (Flag_satus == "Open")
            //               //e.Row.Cells[0].BackColor = Color.Yellow; 
            //               e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //           if (Flag_satus == "Closed")
            //               e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //           if (Flag_satus == "Cancel")
            //               e.Row.Cells[6].Style.Add("background-color", "#FF9DFF");




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


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //string strSql_file_name;
            //string snn1;

            //GridViewRow row = GridView2.Rows[e.RowIndex]; 



            //DataSet ds = new DataSet();




            //           strSql_file_name = " select distinct (t3.file_name)            " +
            //"  from (                                   " +
            //"        select *                           " +
            //"          from night_inspection_file t     " +
            //"         where t.sn = '" + ((DataRowView)e.Row.DataItem)["sn"] + "'     " +
            //"         order by t.dttm desc) t3          ";



            //           ds = func.get_dataSet_access(strSql_file_name, conn);


            //           ((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0];
            //           ((DataList)e.Row.FindControl("DataList1")).DataBind();

            //           String Flag_satus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "open_close_flag"));

            //           if (Flag_satus == "Open")
            //               //e.Row.Cells[0].BackColor = Color.Yellow; 
            //               e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //           if (Flag_satus == "Closed")
            //               e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //           if (Flag_satus == "Cancel")
            //               e.Row.Cells[6].Style.Add("background-color", "#FF9DFF");




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
}
