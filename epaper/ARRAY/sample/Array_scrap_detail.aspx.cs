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

public partial class epaper_ARRAY_sample_Array_scrap_detail : System.Web.UI.Page
{
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE"];
    string conn2 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_SSODB_OLE"];

    string sql = "";

    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();

    string Today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    string yesterday = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["area"] = Request["area"].Trim().ToUpper();
            //Session["area"] = "T0";

            if (Session["area"].ToString().Equals("T0"))
            {
                bind_data1();

            }
            else
            {

                bind_data2();
            }
            //bind_data1();
        }
    }

    protected DataSet bind_data1()
    {

       

         sql = " select T1.LOT_ID LOT_ID,                                           "+
"        T1.PROD_NAME PRODUCT,                                       "+
"        T3.GLASS_ID GLASS_ID,                                       "+
"        T1.LOT_TYPE LOT_TYPE,                                       "+
"        T1.STAGE STAGE,                                             "+
"        T1.CLAIM_STEP STEP_NO,                                      "+
"        T1.PRIORITY PRIORITY,                                       "+
"        to_char(T1.CLAIM_DTTM, 'yyyy/mm/dd hh24:mi:ss') SCRAP_TIME, "+
"        T3.SCRAP_REASON_CODE SCRAP_REASON_CODE,                     "+
"        Convert( T1.SCRAP_CMMT || ' ', 'UTF8','ZHT16MSWIN950'   ) as SCRAP_CMMT ,                                     " +
"                                                                    "+
"        T1.TA_ID      TAID,                                         "+
"        T1.SCRAP_TYPE as SCRAP_UNSCRAP                             "+
"                                                                    "+
"   from SCRAP_LOT_HISTORY T1, LOT T2, SCRAP_SUBSTRATE_HISTORY T3    "+
"  where T1. CLAIM_DTTM = T3. CLAIM_DTTM                             "+
"    and T1. LOT_ID = T3. LOT_ID                                     "+
"    and T1. LOT_ID = T2. LOT_ID                                     "+
"    and to_date(T1.SHIFT_DATE, 'yyyy/mm/dd') between                "+
"        to_date('"+yesterday+"', 'yyyy/mm/dd') and                      "+
"        to_date('" + Today + "', 'yyyy/mm/dd')                          " +
"    and T1.SHOP = 'T0Array'                                         "+
"    and T1.LOT_TYPE in ('E', 'P')                                   "+
"    and T1.scrap_type = 'S' order by  case when  T1.LOT_TYPE ='E' then 1 else 2  end ,case when substr(T1.LOT_ID,6,1)='T' then 1 when  substr(T1.LOT_ID,6,1)='A' then 2 else 3 end , to_char(T1.CLAIM_DTTM, 'yyyy/mm/dd hh24:mi:ss')                                           ";


       // sql = "select rownum,t.* from (" + sql + ")t  ";

         ds_temp1 = func.get_dataSet_access(sql, conn1);

         GridView1.DataSource = ds_temp1;
        GridView1.DataBind();



        return ds_temp1;




    }

    protected DataSet bind_data2()
    {

        sql = " select T1.LOT_ID LOT_ID,                                           " +
"        T1.PROD_NAME PRODUCT,                                       " +
"        T3.GLASS_ID GLASS_ID,                                       " +
"        T1.LOT_TYPE LOT_TYPE,                                       " +
"        T1.STAGE STAGE,                                             " +
"        T1.CLAIM_STEP STEP_NO,                                      " +
"        T1.PRIORITY PRIORITY,                                       " +
"        to_char(T1.CLAIM_DTTM, 'yyyy/mm/dd hh24:mi:ss') SCRAP_TIME, " +
"        T3.SCRAP_REASON_CODE SCRAP_REASON_CODE,                     " +
"        Convert( T1.SCRAP_CMMT || ' ', 'UTF8','ZHT16MSWIN950'   ) as SCRAP_CMMT ,                                   " +
"                                                                    " +
"        T1.TA_ID      TAID,                                         " +
"        T1.SCRAP_TYPE as SCRAP_UNSCRAP                             " +
"                                                                    " +
"   from SCRAP_LOT_HISTORY T1, LOT T2, SCRAP_SUBSTRATE_HISTORY T3    " +
"  where T1. CLAIM_DTTM = T3. CLAIM_DTTM                             " +
"    and T1. LOT_ID = T3. LOT_ID                                     " +
"    and T1. LOT_ID = T2. LOT_ID                                     " +
"    and to_date(T1.SHIFT_DATE, 'yyyy/mm/dd') between                " +
"        to_date('" + yesterday + "', 'yyyy/mm/dd') and                      " +
"        to_date('" + Today + "', 'yyyy/mm/dd')                          " +
"    and T1.SHOP = 'T1Array'                                         " +
"    and T1.LOT_TYPE in ('E', 'P')                                   " +
"    and T1.scrap_type = 'S'  order by  case when  T1.LOT_TYPE ='E' then 1 else 2  end ,case when substr(T1.LOT_ID,6,1)='T' then 1 when  substr(T1.LOT_ID,6,1)='A' then 2 else 3 end , to_char(T1.CLAIM_DTTM, 'yyyy/mm/dd hh24:mi:ss')                                      ";

        sql = "select rownum,t.* from (" + sql + ")t  ";

        ds_temp1 = func.get_dataSet_access(sql, conn1);

        GridView1.DataSource = ds_temp1;
        GridView1.DataBind();



        return ds_temp1;




    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            #region 自動編碼


            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            } 

            #endregion
           

 //           string strSql_file_name;
 //           string snn1;

 //           //GridViewRow row = GridView2.Rows[e.RowIndex]; 



 //           DataSet ds = new DataSet();




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




           

        }
    } 

}
