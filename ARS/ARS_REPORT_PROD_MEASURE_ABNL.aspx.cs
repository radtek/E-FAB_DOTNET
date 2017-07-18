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

using System.Reflection;


public partial class ARS_ARS_REPORT_PROD_MEASURE_ABNL : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_PARS1"];

    string Date_str;

    DataSet ds_temp = new DataSet();

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtEstimateStartDate.SelectedDate = DateTime.Now.AddDays(-14);
            txtEstimateEndDate.SelectedDate = DateTime.Now.AddDays(-7);


            bind_data();
        }



    }

    protected DataSet bind_data()
    {

        string sql_str = @" 
       select ot3.*,round((case when ot3.CLOSE=0 then 0 else ot3.CLOSE/ot3.total end)*100,2) as ratio from (

select ot2.dept,sum(ot2.CLOSE_OVER_7) CLOSE_OVER_7,sum(ot2.step1) step1,sum(ot2.step2) step2,sum(ot2.step3) step3 ,sum(ot2.step4) step4
,sum(ot2.STEP_REVIEW) STEP_REVIEW ,sum(ot2.CLOSE_R) CLOSE_R ,sum(ot2.CLOSE) CLOSE ,sum(ot2.Total) Total





from (

select ot1.dept
,case when ot1.sheet_status='CLOSE_OVER_7' then ot1.CNTS else 0 end as CLOSE_OVER_7
,case when ot1.sheet_status='STEP1' then ot1.CNTS else 0 end as step1 
,case when ot1.sheet_status='STEP2' then ot1.CNTS else 0 end as step2
,case when ot1.sheet_status='STEP3' then ot1.CNTS else 0 end as step3
,case when ot1.sheet_status='STEP4' then ot1.CNTS else 0 end as step4

,case when ot1.sheet_status='STEP_REVIEW' then ot1.CNTS else 0 end as STEP_REVIEW
,case when ot1.sheet_status='CLOSE_R' then ot1.CNTS else 0 end as CLOSE_R
,case when ot1.sheet_status='CLOSE' then ot1.CNTS else 0 end as CLOSE
,case when ot1.sheet_status='CLOSE_OVER_7' then ot1.CNTS else 0 end+case when ot1.sheet_status='STEP1' then ot1.CNTS else 0 end
+case when ot1.sheet_status='STEP2' then ot1.CNTS else 0 end+case when ot1.sheet_status='STEP3' then ot1.CNTS else 0 end+
case when ot1.sheet_status='STEP4' then ot1.CNTS else 0 end +case when ot1.sheet_status='STEP_REVIEW' then ot1.CNTS else 0 end
+case when ot1.sheet_status='CLOSE_R' then ot1.CNTS else 0 end+
case when ot1.sheet_status='CLOSE' then ot1.CNTS else 0 end as Total




from (

select ob1.dept,ob1.sheet_status, count(ob1.sheet_status) as CNTS from (


select t.fab||'_'||t.abnl_eqdep as dept,
case when (t1.update_time-t.isu_time)>7 then 'CLOSE_OVER_7'
     when (t1.update_time-t.isu_time)<=7 then 'CLOSE'
     end as sheet_status

,t1.update_time
from abnl_main t,step_data t1
where t.isu_time>=to_date('{0}','yyyy/MM/dd HH')
and t.isu_time<=to_date('{1}','yyyy/MM/dd HH')
and t.abnl_type in ('產品異常','{2}')
and t.sheet_no=t1.sheet_no(+)
and t1.step_id='CLOSE'

union all

select t10.fab||'_'||t10.abnl_eqdep as dept,t10.sheet_status,sysdate from abnl_main t10
where t10.isu_time>=to_date('{0}','yyyy/MM/dd HH')
and t10.isu_time<=to_date('{1}','yyyy/MM/dd HH')
and t10.abnl_type in ('產品異常','{2}')
and t10.sheet_status<>'CLOSE'
and t10.fail_flag is null

)ob1
group by ob1.dept,ob1.sheet_status







)ot1

)ot2

group by ot2.dept

)ot3

union all


select ot5.*,round((case when ot5.CLOSE=0 then 0 else ot5.CLOSE/ot5.Total end)*100 ,2) ratio   from (


select 'Summary',sum(ot4.CLOSE_OVER_7) CLOSE_OVER_7,sum(ot4.step1) step1,sum(ot4.step2) step2,sum(ot4.step3) step3,sum(ot4.step4) step4,
sum(ot4.STEP_REVIEW) STEP_REVIEW
,sum(ot4.CLOSE_R) CLOSE_R,sum(ot4.CLOSE) CLOSE,sum(ot4.Total) Total
 from (

select ot3.*,round((case when ot3.CLOSE=0 then 0 else ot3.CLOSE/ot3.total end)*100,2) as ratio from (

select ot2.dept,sum(ot2.CLOSE_OVER_7) CLOSE_OVER_7,sum(ot2.step1) step1,sum(ot2.step2) step2,sum(ot2.step3) step3 ,sum(ot2.step4) step4
,sum(ot2.STEP_REVIEW) STEP_REVIEW ,sum(ot2.CLOSE_R) CLOSE_R ,sum(ot2.CLOSE) CLOSE ,sum(ot2.Total) Total





from (

select ot1.dept
,case when ot1.sheet_status='CLOSE_OVER_7' then ot1.CNTS else 0 end as CLOSE_OVER_7
,case when ot1.sheet_status='STEP1' then ot1.CNTS else 0 end as step1 
,case when ot1.sheet_status='STEP2' then ot1.CNTS else 0 end as step2
,case when ot1.sheet_status='STEP3' then ot1.CNTS else 0 end as step3
,case when ot1.sheet_status='STEP4' then ot1.CNTS else 0 end as step4

,case when ot1.sheet_status='STEP_REVIEW' then ot1.CNTS else 0 end as STEP_REVIEW
,case when ot1.sheet_status='CLOSE_R' then ot1.CNTS else 0 end as CLOSE_R
,case when ot1.sheet_status='CLOSE' then ot1.CNTS else 0 end as CLOSE
,case when ot1.sheet_status='CLOSE_OVER_7' then ot1.CNTS else 0 end+case when ot1.sheet_status='STEP1' then ot1.CNTS else 0 end
+case when ot1.sheet_status='STEP2' then ot1.CNTS else 0 end+case when ot1.sheet_status='STEP3' then ot1.CNTS else 0 end+
case when ot1.sheet_status='STEP4' then ot1.CNTS else 0 end +case when ot1.sheet_status='STEP_REVIEW' then ot1.CNTS else 0 end
+case when ot1.sheet_status='CLOSE_R' then ot1.CNTS else 0 end+
case when ot1.sheet_status='CLOSE' then ot1.CNTS else 0 end as Total




from (

select ob1.dept,ob1.sheet_status, count(ob1.sheet_status) as CNTS from (


select t.fab||'_'||t.abnl_eqdep as dept,
case when (t1.update_time-t.isu_time)>7 then 'CLOSE_OVER_7'
     when (t1.update_time-t.isu_time)<=7 then 'CLOSE'
     end as sheet_status

,t1.update_time
from abnl_main t,step_data t1
where t.isu_time>=to_date('{0}','yyyy/MM/dd HH')
and t.isu_time<=to_date('{1}','yyyy/MM/dd HH')
and t.abnl_type in ('產品異常','{2}')
and t.sheet_no=t1.sheet_no(+)
and t1.step_id='CLOSE'

union all

select t10.fab||'_'||t10.abnl_eqdep as dept,t10.sheet_status,sysdate from abnl_main t10
where t10.isu_time>=to_date('{0}','yyyy/MM/dd HH')
and t10.isu_time<=to_date('{1}','yyyy/MM/dd HH')
and t10.abnl_type in ('產品異常','{2}')
and t10.sheet_status<>'CLOSE'
and t10.fail_flag is null

)ob1
group by ob1.dept,ob1.sheet_status







)ot1

)ot2

group by ot2.dept

)ot3

)ot4
) ot5


";
        sql_str = string.Format(sql_str, txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd")+" 07", txtEstimateEndDate.SelectedDate.Value.ToString("yyyy/MM/dd")+" 07", "產品量測異常");

        ds_temp = func.get_dataSet_access(sql_str, conn);

         GridView1.DataSource = ds_temp;
        //GridView1.DataSource = ds_main;
        GridView1.DataBind();
        ds_temp.Clear();
        return ds_temp;

    }

     protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }

        }

    }



    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        bind_data();
    }
    protected void btnExport_Click1(object sender, EventArgs e)
    {
        string sql_str = @" 
       select ot3.*,round((case when ot3.CLOSE=0 then 0 else ot3.CLOSE/ot3.total end)*100,2) as ratio from (

select ot2.dept,sum(ot2.CLOSE_OVER_7) CLOSE_OVER_7,sum(ot2.step1) step1,sum(ot2.step2) step2,sum(ot2.step3) step3 ,sum(ot2.step4) step4
,sum(ot2.STEP_REVIEW) STEP_REVIEW ,sum(ot2.CLOSE_R) CLOSE_R ,sum(ot2.CLOSE) CLOSE ,sum(ot2.Total) Total





from (

select ot1.dept
,case when ot1.sheet_status='CLOSE_OVER_7' then ot1.CNTS else 0 end as CLOSE_OVER_7
,case when ot1.sheet_status='STEP1' then ot1.CNTS else 0 end as step1 
,case when ot1.sheet_status='STEP2' then ot1.CNTS else 0 end as step2
,case when ot1.sheet_status='STEP3' then ot1.CNTS else 0 end as step3
,case when ot1.sheet_status='STEP4' then ot1.CNTS else 0 end as step4

,case when ot1.sheet_status='STEP_REVIEW' then ot1.CNTS else 0 end as STEP_REVIEW
,case when ot1.sheet_status='CLOSE_R' then ot1.CNTS else 0 end as CLOSE_R
,case when ot1.sheet_status='CLOSE' then ot1.CNTS else 0 end as CLOSE
,case when ot1.sheet_status='CLOSE_OVER_7' then ot1.CNTS else 0 end+case when ot1.sheet_status='STEP1' then ot1.CNTS else 0 end
+case when ot1.sheet_status='STEP2' then ot1.CNTS else 0 end+case when ot1.sheet_status='STEP3' then ot1.CNTS else 0 end+
case when ot1.sheet_status='STEP4' then ot1.CNTS else 0 end +case when ot1.sheet_status='STEP_REVIEW' then ot1.CNTS else 0 end
+case when ot1.sheet_status='CLOSE_R' then ot1.CNTS else 0 end+
case when ot1.sheet_status='CLOSE' then ot1.CNTS else 0 end as Total




from (

select ob1.dept,ob1.sheet_status, count(ob1.sheet_status) as CNTS from (


select t.fab||'_'||t.abnl_eqdep as dept,
case when (t1.update_time-t.isu_time)>7 then 'CLOSE_OVER_7'
     when (t1.update_time-t.isu_time)<=7 then 'CLOSE'
     end as sheet_status

,t1.update_time
from abnl_main t,step_data t1
where t.isu_time>=to_date('{0}','yyyy/MM/dd HH')
and t.isu_time<=to_date('{1}','yyyy/MM/dd HH')
and t.abnl_type in ('產品異常','{2}')
and t.sheet_no=t1.sheet_no(+)
and t1.step_id='CLOSE'

union all

select t10.fab||'_'||t10.abnl_eqdep as dept,t10.sheet_status,sysdate from abnl_main t10
where t10.isu_time>=to_date('{0}','yyyy/MM/dd HH')
and t10.isu_time<=to_date('{1}','yyyy/MM/dd HH')
and t10.abnl_type in ('產品異常','{2}')
and t10.sheet_status<>'CLOSE'
and t10.fail_flag is null

)ob1
group by ob1.dept,ob1.sheet_status







)ot1

)ot2

group by ot2.dept

)ot3

union all


select ot5.*,round((case when ot5.CLOSE=0 then 0 else ot5.CLOSE/ot5.Total end)*100 ,2) ratio   from (


select 'Summary',sum(ot4.CLOSE_OVER_7) CLOSE_OVER_7,sum(ot4.step1) step1,sum(ot4.step2) step2,sum(ot4.step3) step3,sum(ot4.step4) step4,
sum(ot4.STEP_REVIEW) STEP_REVIEW
,sum(ot4.CLOSE_R) CLOSE_R,sum(ot4.CLOSE) CLOSE,sum(ot4.Total) Total
 from (

select ot3.*,round((case when ot3.CLOSE=0 then 0 else ot3.CLOSE/ot3.total end)*100,2) as ratio from (

select ot2.dept,sum(ot2.CLOSE_OVER_7) CLOSE_OVER_7,sum(ot2.step1) step1,sum(ot2.step2) step2,sum(ot2.step3) step3 ,sum(ot2.step4) step4
,sum(ot2.STEP_REVIEW) STEP_REVIEW ,sum(ot2.CLOSE_R) CLOSE_R ,sum(ot2.CLOSE) CLOSE ,sum(ot2.Total) Total





from (

select ot1.dept
,case when ot1.sheet_status='CLOSE_OVER_7' then ot1.CNTS else 0 end as CLOSE_OVER_7
,case when ot1.sheet_status='STEP1' then ot1.CNTS else 0 end as step1 
,case when ot1.sheet_status='STEP2' then ot1.CNTS else 0 end as step2
,case when ot1.sheet_status='STEP3' then ot1.CNTS else 0 end as step3
,case when ot1.sheet_status='STEP4' then ot1.CNTS else 0 end as step4

,case when ot1.sheet_status='STEP_REVIEW' then ot1.CNTS else 0 end as STEP_REVIEW
,case when ot1.sheet_status='CLOSE_R' then ot1.CNTS else 0 end as CLOSE_R
,case when ot1.sheet_status='CLOSE' then ot1.CNTS else 0 end as CLOSE
,case when ot1.sheet_status='CLOSE_OVER_7' then ot1.CNTS else 0 end+case when ot1.sheet_status='STEP1' then ot1.CNTS else 0 end
+case when ot1.sheet_status='STEP2' then ot1.CNTS else 0 end+case when ot1.sheet_status='STEP3' then ot1.CNTS else 0 end+
case when ot1.sheet_status='STEP4' then ot1.CNTS else 0 end +case when ot1.sheet_status='STEP_REVIEW' then ot1.CNTS else 0 end
+case when ot1.sheet_status='CLOSE_R' then ot1.CNTS else 0 end+
case when ot1.sheet_status='CLOSE' then ot1.CNTS else 0 end as Total




from (

select ob1.dept,ob1.sheet_status, count(ob1.sheet_status) as CNTS from (


select t.fab||'_'||t.abnl_eqdep as dept,
case when (t1.update_time-t.isu_time)>7 then 'CLOSE_OVER_7'
     when (t1.update_time-t.isu_time)<=7 then 'CLOSE'
     end as sheet_status

,t1.update_time
from abnl_main t,step_data t1
where t.isu_time>=to_date('{0}','yyyy/MM/dd HH')
and t.isu_time<=to_date('{1}','yyyy/MM/dd HH')
and t.abnl_type in ('產品異常','{2}')
and t.sheet_no=t1.sheet_no(+)
and t1.step_id='CLOSE'

union all

select t10.fab||'_'||t10.abnl_eqdep as dept,t10.sheet_status,sysdate from abnl_main t10
where t10.isu_time>=to_date('{0}','yyyy/MM/dd HH')
and t10.isu_time<=to_date('{1}','yyyy/MM/dd HH')
and t10.abnl_type in ('產品異常','{2}')
and t10.sheet_status<>'CLOSE'
and t10.fail_flag is null

)ob1
group by ob1.dept,ob1.sheet_status







)ot1

)ot2

group by ot2.dept

)ot3

)ot4
) ot5















        ";
        sql_str = string.Format(sql_str, txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd")+" 07", txtEstimateEndDate.SelectedDate.Value.ToString("yyyy/MM/dd")+" 07", "產品量測異常");

        ds_temp = func.get_dataSet_access(sql_str, conn);



        GridView gv = new GridView();
        gv.DataSource = ds_temp.Tables[0];
        gv.DataBind();
        ExportExcel(gv); 



    }

    private void ExportExcel(GridView SeriesValuesDataGrid)
    {
        string today_detail_char = DateTime.Now.AddDays(+0).ToString("yyyyMMddHHmmss");
        string file_name = "attachment;filename=ARS_PROD_REPORT_";
        file_name = file_name + today_detail_char + ".xls";
        
        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader("content-disposition", file_name);

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


}
