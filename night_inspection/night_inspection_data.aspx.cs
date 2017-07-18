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

public partial class night_inspection_night_inspection_data : System.Web.UI.Page
{

    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OARPT_MEETUSER"];
    DataSet ds_temp = new DataSet();

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
           Session["sn"] = Request["sn"].Trim().ToUpper();
           //Session["sn"]="20100615085124";

           bind_data();
        }
        
    }

    protected DataSet bind_data()
    {
         
        string sql = " select t1.* from night_inspection_record t1                             " +
 " where 1=1     and t1.sn='" + Session["sn"] .ToString()+ "'                                                      ";
   

        sql = "select rownum,t.* from (" + sql + ")t  ";

        ds_temp = func.get_dataSet_access(sql, conn);

        GridView1.DataSource = ds_temp;
        GridView1.DataBind();



        return ds_temp;




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




            strSql_file_name = " select distinct (t3.file_name)            " +
 "  from (                                   " +
 "        select *                           " +
 "          from night_inspection_file t     " +
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
