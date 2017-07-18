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
using System.IO;

public partial class AlarmServerFail : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ALARM"];
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    string today_minus1 = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm");
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2= "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            txtCalendar1.Text = today_minus1;
            txtCalendar2.Text = today;
            sql_temp = " select  t3.eventsubject,t3.EventName,t3.address,count(t3.Finished) as fail_count  from ( " +
"                                                                                          " +
" SELECT t1.EventNo,                                                                       " +
"        t1.eventsubject,                                                                  " +
"        t1.EventName,                                                                     " +
"        t2.address,                                                                       " +
"        t1.Messages,                                                                      " +
"        t1.eventdate,                                                                     " +
"        t2.Finished,                                                                      " +
"        t2.subject,                                                                       " +
"        t2.description                                                                    " +
"   FROM Event AS t1, process AS t2                                                        " +
"  WHERE ((t1.EventNo) = t2.eventno)                                                       " +
"  and t2.Finished='F'                                                                     " +
"   and t1.EventDate >= #" + txtCalendar1.Text + "#                                             " +
"   and t1.EventDate <= #" + txtCalendar2.Text + "#                                             " +
"  order by t1.eventdate desc                                                              " +
"                                                                                          " +
" )t3                                                                                      " +
"                                                                                          " +
" group by t3.eventsubject,t3.EventName,t3.address                                         " +
" order by count(t3.Finished) desc                                                         ";

            Bind_data(sql_temp, conn);

        }

        lblAIExpand.Text = DropDownList1.SelectedValue;




    }


    public DataSet Bind_data(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);



        GridView1.DataSource = ds_temp.Tables[0];


        GridView1.DataBind();



        return ds_temp;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {




        sql_temp = " select  t3.eventsubject,t3.EventName,t3.address,count(t3.Finished) as fail_count  from ( " +
"                                                                                          " +
" SELECT t1.EventNo,                                                                       " +
"        t1.eventsubject,                                                                  " +
"        t1.EventName,                                                                     " +
"        t2.address,                                                                       " +
"        t1.Messages,                                                                      " +
"        t1.eventdate,                                                                     " +
"        t2.Finished,                                                                      " +
"        t2.subject,                                                                       " +
"        t2.description                                                                    " +
"   FROM Event AS t1, process AS t2                                                        " +
"  WHERE ((t1.EventNo) = t2.eventno)                                                       " +
"  and t2.Finished='F'                                                                     " +
"   and t1.EventDate >= #" + txtCalendar1.Text + "#                                             " +
"   and t1.EventDate <= #" + txtCalendar2.Text + "#                                             " +
"  order by t1.eventdate desc                                                              " +
"                                                                                          " +
" )t3                                                                                      " +
"                                                                                          " +
" group by t3.eventsubject,t3.EventName,t3.address                                         " +
" order by count(t3.Finished) desc                                                         ";


        GridView gv = new GridView();
        gv.DataSource = Bind_data(sql_temp, conn);
        gv.DataBind();
        ExportExcel(gv);




    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
    }

    private void ExportExcel(GridView SeriesValuesDataGrid)
    {

        string filename = "";

        filename = "AlarmServer_fail_" + today_detail + ".xls";

        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader("content-disposition", "attachment;filename=AlarmServer_fail.xls");

        //Response.AddHeader("content-disposition", "attachment;filename=\"" + filename + "\";");

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

    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        sql_temp = " select  t3.eventsubject,t3.EventName,t3.address,count(t3.Finished) as fail_count  from ( " +
"                                                                                          " +
" SELECT t1.EventNo,                                                                       " +
"        t1.eventsubject,                                                                  " +
"        t1.EventName,                                                                     " +
"        t2.address,                                                                       " +
"        t1.Messages,                                                                      " +
"        t1.eventdate,                                                                     " +
"        t2.Finished,                                                                      " +
"        t2.subject,                                                                       " +
"        t2.description                                                                    " +
"   FROM Event AS t1, process AS t2                                                        " +
"  WHERE ((t1.EventNo) = t2.eventno)                                                       " +
"  and t2.Finished='F'                                                                     " +
"   and t1.EventDate >= #" + txtCalendar1.Text + "#                                             " +
"   and t1.EventDate <= #" + txtCalendar2.Text + "#                                             " +
"  order by t1.eventdate desc                                                              " +
"                                                                                          " +
" )t3                                                                                      " +
"                                                                                          " +
" group by t3.eventsubject,t3.EventName,t3.address                                         " +
" order by count(t3.Finished) desc                                                         ";

        Bind_data(sql_temp, conn);
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




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
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            // string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            //Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 

            sql_temp2 = " select t1.eventno,t1.name,t1.address,t1.senddate,t1.finished,t1.description from event t,process t1  "+
" where t.eventname='" + ((DataRowView)e.Row.DataItem)["EventName"].ToString() + "' and t.eventno=t1.eventno                                                 " +
" and  t.eventdate>=#" + txtCalendar1.Text + "#  and   t.eventdate<=#" + txtCalendar2.Text + "#                                      " +
" and t1.finished='F'  order by t.eventdate desc                                                       ";
           
            ds_temp2 = func.get_dataSet_access(sql_temp2,conn);

            GridView2.DataSource = ds_temp2.Tables[0];
            GridView2.DataBind(); 



            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

            if (ds_temp2.Tables[0].Rows.Count == 0)
            {
                System.Web.UI.WebControls.Image btnShowDetail = new System.Web.UI.WebControls.Image();
                btnShowDetail = (System.Web.UI.WebControls.Image)e.Row.FindControl("btnShowDetail");
                btnShowDetail.Visible = false;
            }
            else
            { 
              //********************************************************* 
//新增一個新的GridViewRow 
#region 
GridViewRow r = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal); 
StringWriter sw = new StringWriter(); 
HtmlTextWriter hw = new HtmlTextWriter(sw); 

r.Cells.Add(new TableCell()); 
r.Cells.Add(new TableCell());

r.Cells[1].ColumnSpan = GridView2.Columns.Count - 1; 

GridView2.Visible = true; 
GridView2.RenderControl(hw); 
GridView2.Visible = false; 

r.Cells[1].Text = sw.ToString(); 
sw.Close();

r.ID = "Detail_" + e.Row.RowIndex.ToString(); 

r.HorizontalAlign = HorizontalAlign.Left; 
e.Row.Parent.Controls.Add(r); 

System.Web.UI.WebControls.Image btnShowDetail = new System.Web.UI.WebControls.Image(); 
btnShowDetail = (System.Web.UI.WebControls.Image)e.Row.FindControl("btnShowDetail");
btnShowDetail.Attributes.Add("onclick", "showHideAnswer('GridView1_" + r.ID + "','" + e.Row.ClientID.ToString() + "_" + btnShowDetail.ID + "');"); 
//btnShowDetail.Attributes.Add("onclick", "showHideAnswer('" + this.ClientID.ToString() + "_GridView1_" + r.ID + "','" + e.Row.ClientID.ToString() + "_" + btnShowDetail.ID + "');"); 

if (lblAIExpand.Text == "Y") 
{ 
r.Style["display"] = "block"; 
btnShowDetail.ImageUrl = "~/images/close13.gif"; 
} 
else 
{ 
r.Style["display"] = "none"; 
btnShowDetail.ImageUrl = "~/images/open13.gif"; 
} 


#endregion 
//********************************************************* 
} 

            
            
            




        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //ImageButton btnDel = new ImageButton(); 
            //btnDel = (ImageButton)e.Row.FindControl("btnDel"); 

            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【Stock_id】:" + ((DataRowView)e.Row.DataItem)["stock_id"] + " 【End Time】:" + ((DataRowView)e.Row.DataItem)["date1"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');"; 




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
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            // string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            //Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    } 
}
