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

public partial class LH_REALITY_LH_LAM_DEFECT_ALARM_QUERY : System.Web.UI.Page
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
            ArrayList arlist_temp1 = new ArrayList();


            txtEstimateStartDate.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-2/24).ToString("yyyy/MM/dd"));
            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\hour.txt");

            DropDownList5.DataSource = arlist_temp1;
            DropDownList5.DataBind();
            DropDownList5.Text = DateTime.Now.AddHours(-6).ToString("HH");


            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\min.txt");

            DropDownList8.DataSource = arlist_temp1;
            DropDownList8.DataBind();
            DropDownList8.Text = "30";



            txtEstimateEndDate.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-2/24).ToString("yyyy/MM/dd"));
            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\hour.txt");

            DropDownList15.DataSource = arlist_temp1;
            DropDownList15.DataBind();
            DropDownList15.Text = DateTime.Now.AddHours(-1).ToString("HH");


            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\min.txt");

            DropDownList18.DataSource = arlist_temp1;
            DropDownList18.DataBind();
            DropDownList18.Text = "29";

           


            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\class.txt");

            DropDownList1.DataSource = arlist_temp1;
            DropDownList1.DataBind();
            //DropDownList1.Items.Insert(0, "請選擇");



           

           






            bind_data();


        }
    }
    

    protected DataSet bind_data()
    {
        string sql = " select dayhour, fab, shift, prod_name, productiontype, step_name, case when TOTAL_YIELD is null then '' else TOTAL_YIELD ||'%' end  TOTAL_YIELD , defectcode, " +
"        defectdesc, grade, defect_qty, move_qty,                                             " +
"        to_char(round(violateratio*100,2),'0.99') || '%' as defect_ratio,                    " +
"        to_char(round(std_violateratio*100,2),'0.99') || '%' as alarm_level ,           " +
"        OWNER                                                                                " +
" from dalarmm_alarm_detail D                                                                 " +
" WHERE D.DAYHOUR BETWEEN '" + txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd").Replace("/", "") + DropDownList5.Text + DropDownList8.Text + "' AND '" + txtEstimateEndDate.SelectedDate.Value.ToString("yyyy/MM/dd").Replace("/", "") + DropDownList15.Text + DropDownList18.Text + "'                               " +
"   AND D.FAB = 'H3'  and prod_name = '3.5N90'                               ";

        if (!DropDownList1.Text.Equals("請選擇"))
        {
            sql = sql + " and  d.shift ='" + DropDownList1 .Text+ "'       ";

        
        }

        sql = sql + "   and productiontype = 'ZTMC'                                                               ";

        sql = "select rownum,t.* from (" + sql + ")t  ";

        ds_temp = func.get_dataSet_access(sql, conn);

        GridView1.DataSource = ds_temp;
        GridView1.DataBind();



        return ds_temp;




    }
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        bind_data();
    }
    protected void btnExport_Click1(object sender, EventArgs e)
    {
        GridView gv = new GridView();
        gv.DataSource = bind_data();
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

        Response.AddHeader("content-disposition", "attachment;filename=LH_LAM_DEFECT.xls");

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
