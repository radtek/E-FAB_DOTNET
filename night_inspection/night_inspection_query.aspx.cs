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

public partial class night_inspection_night_inspection_query : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OARPT_MEETUSER"];
    DataSet ds_temp = new DataSet();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ArrayList arlist_temp1 = new ArrayList();

            
            txtEstimateStartDate.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd"));
            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\hour.txt");

            DropDownList5.DataSource = arlist_temp1;
            DropDownList5.DataBind();
            DropDownList5.Text = DateTime.Now.ToString("HH");


            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\min.txt");

            DropDownList8.DataSource = arlist_temp1;
            DropDownList8.DataBind();
            DropDownList8.Text = DateTime.Now.ToString("mm");



            txtEstimateEndDate.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd"));
            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\hour.txt");

            DropDownList15.DataSource = arlist_temp1;
            DropDownList15.DataBind();
            DropDownList15.Text = DateTime.Now.ToString("HH");


            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\min.txt");

            DropDownList18.DataSource = arlist_temp1;
            DropDownList18.DataBind();
            DropDownList18.Text = DateTime.Now.ToString("mm");


            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\abnormal_type.txt");

            DropDownList1.DataSource = arlist_temp1;
            DropDownList1.DataBind();


            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\abnormal_area.txt");
            DropDownList2.DataSource = arlist_temp1;
            DropDownList2.DataBind();


            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\dep.txt");
            DropDownList3.DataSource = arlist_temp1;
            DropDownList3.DataBind();

            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\flag_type_query.txt");
            DropDownList12.DataSource = arlist_temp1;
            DropDownList12.DataBind();






            bind_data();
        
        
        }
    }

    protected DataSet bind_data()

    {
        string sql = " select t1.* from night_inspection_record t1                             " +
 " where 1=1                                                           ";
        if (!txtEstimateStartDate.SelectedDate.Value.Equals(""))
        {
            sql += " and  t1.start_time>=to_date('" + txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd") + " " + DropDownList5.SelectedValue + ":" + DropDownList8.SelectedValue + "','YYYY/MM/DD HH24:MI')     ";
        
        }

        if (!txtEstimateEndDate.SelectedDate.Value.Equals(""))
        {
            sql += " and  t1.start_time<to_date('" + txtEstimateEndDate.SelectedDate.Value.ToString("yyyy/MM/dd") + " " + DropDownList15.SelectedValue + ":" + DropDownList18.SelectedValue + "','YYYY/MM/DD HH24:MI')     ";

        }

        if (!DropDownList1.SelectedValue.Equals("請選擇"))
        {
            sql += " and t1.abnormal_type='" + DropDownList1.SelectedValue + "'                                          ";

        }

        if (!DropDownList2.SelectedValue.Equals("請選擇"))
        {
            sql += " and t1.abnormal_area='" + DropDownList2 .SelectedValue+ "'                                                ";

        }


        if (!DropDownList3.SelectedValue.Equals("請選擇"))
        {
            sql += " and t1.dep='" + DropDownList3.SelectedValue + "'                                                        ";

        }

        if (!DropDownList12.SelectedValue.Equals("請選擇"))
        {
            sql += " and t1.open_close_flag in (" + DropDownList12.SelectedValue + ")                                                        ";

        }

        

        sql += " order by start_time desc ";

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


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            string strSql_file_name;
            string snn1;

            //GridViewRow row = GridView2.Rows[e.RowIndex]; 



            DataSet ds = new DataSet();

           


            strSql_file_name= " select distinct (t3.file_name)            "+
 "  from (                                   "+
 "        select *                           "+
 "          from night_inspection_file t     "+
 "         where t.sn = '" + ((DataRowView)e.Row.DataItem)["sn"] + "'     " +
 "         order by t.dttm desc) t3          ";



            ds = func.get_dataSet_access(strSql_file_name, conn);


            ((DataList)e.Row.FindControl("DataList1")).DataSource = ds.Tables[0];
            ((DataList)e.Row.FindControl("DataList1")).DataBind();

            String Flag_satus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "open_close_flag"));

            if (Flag_satus == "Open")
                //e.Row.Cells[0].BackColor = Color.Yellow; 
                e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            if (Flag_satus == "Closed")
                e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            if (Flag_satus == "Cancel")
                e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 




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
