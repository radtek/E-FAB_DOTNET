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

public partial class epaper_ARRAY_sample_ary_hold_lot_fortd : System.Web.UI.Page
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
           
                bind_data1();

          
        }
    }

    protected DataSet bind_data1()
    {



        sql = @"


        select 
t1.shop,

       T1.LOT_ID LOT_ID,
       T1.CST_ID CST_ID,
       T1.LOT_TYPE LOT_TYPE,
       T2.HOLDING_STEP_NAME STEP_NAME,
       T1.STEP_DESC STEP_DESC,
       T1.PROC_STATE STATUS,
       T1.CUT_SIZE CUT_SIZE,
       T2.HOLD_SUBSTRATE_QTY CHIP_QTY,
       T1.PRIORITY PRIORITY,
       to_char(T2.HOLD_DTTM, 'yyyy/mm/dd hh24:mi:ss') HOLD_START_TIME,
       T1.MODULE MODULE,
       NVL(T1.LOT_OWNER_ID, 'NA') OWNER,
       Convert( T1.LOT_CMMT || ' ', 'UTF8','ZHT16MSWIN950' ) LOT_COMMENT,
       T2.TA,
       Round(((SYSDATE) - (T2.HOLD_DTTM)) * 24, 2) as ""(hrs)"",
       T2.HOLD_REASON_CODE CODE,
       T1.PROCPLAN_NAME PLAN_NAME
  from RPTDW.LOT T1, RPTDW.HOLD_LOT_HISTORY T2
 where T1.LOT_ID = T2.LOT_ID
   and T1.FAB = T2.FAB
   and T1.SHOP = T2.SHOP
   and T1.PROC_STATE = 'OnHold'
   and T1.LOT_ID <> ' ALL'
   and NVL(TO_CHAR(T2.RELEASE_TIME, 'YYYY/MM/DD'), 'NULL') = 'NULL'
   and T1.STEP_NAME = T2.HOLDING_STEP_NAME
   and t1.terminate_dttm is null
   --and T1.SHOP = 'T1Array'
   and substr(HOLDING_STEP_NAME,2,1)='9'
   and substr(HOLDING_STEP_NAME,1,1)<>'0'
   and substr(HOLDING_STEP_NAME,4,1)='0'
    and substr(HOLDING_STEP_NAME,5,1)='_'
   
   order by t1.shop,T2.HOLDING_STEP_NAME


";


        sql = @"

  select 


       T1.LOT_ID LOT_ID,
       T1.CST_ID CST_ID,
       T1.LOT_TYPE LOT_TYPE,
       T2.HOLDING_STEP_NAME STEP_NAME,
       T1.STEP_DESC STEP_DESC,
     
       T2.HOLD_SUBSTRATE_QTY CHIP_QTY,
    
       Convert( T1.LOT_CMMT || ' ', 'UTF8','ZHT16MSWIN950' ) LOT_COMMENT
       
  from RPTDW.LOT T1, RPTDW.HOLD_LOT_HISTORY T2
 where T1.LOT_ID = T2.LOT_ID
   and T1.FAB = T2.FAB
   and T1.SHOP = T2.SHOP
   and T1.PROC_STATE = 'OnHold'
   and T1.LOT_ID <> ' ALL'
   and NVL(TO_CHAR(T2.RELEASE_TIME, 'YYYY/MM/DD'), 'NULL') = 'NULL'
   and T1.STEP_NAME = T2.HOLDING_STEP_NAME
   and t1.terminate_dttm is null
   --and T1.SHOP = 'T1Array'
   and substr(HOLDING_STEP_NAME,2,1)='9'
   and substr(HOLDING_STEP_NAME,1,1)<>'0'
   and substr(HOLDING_STEP_NAME,4,1)='0'
    and substr(HOLDING_STEP_NAME,5,1)='_'
   
   order by t1.shop,T2.HOLDING_STEP_NAME

";


        // sql = "select rownum,t.* from (" + sql + ")t  ";

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
