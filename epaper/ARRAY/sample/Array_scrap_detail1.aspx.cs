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

public partial class epaper_ARRAY_sample_Array_scrap_detail1 : System.Web.UI.Page
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



        sql = @"

         select (case                                                               
        when substr(t.lot_id, 6, 1) = 'A' then                              
         'TFT'                                                              
        else                                                                
         'TP'                                                               
      end) as CATEGORY,                                                     
      t.lot_id,                                                             
      t.lot_type_attribute as TYPE,
      t.cmmt_code as Scrap_type,                                                  
      case when  t.cmmt_code='Unscrap'  then -t.scrap_qty
               else t.scrap_qty end  as QTY,   
                                                      
      t.stage,                                                              
      to_char(t.claim_dttm,'YYYY/MM/DD HH24:MM') as ScrapTime,              
      Convert( t.SCRAP_CMMT || ' ', 'UTF8','ZHT16MSWIN950' ) as SCRAP_CMMT  
                                                                            
 from innrpt.scrap_lot_history t                                            
where t.lot_type <> 'MQC'                                                   
  and t.shop = 'T0Array'                                                    
  and to_char(t.claim_dttm, 'YYYY/MM/DD HH24:MM')>='{0} 07:00'
  and to_char(t.claim_dttm, 'YYYY/MM/DD HH24:MM')<'{1} 06:59'     
                                                                            
order by (case                                                              
           when substr(t.lot_id, 6, 1) = 'A' then                           
            'TFT'                                                           
           else                                                             
            'TP'                                                            
         end) desc,                                                         
         t.claim_dttm  
";
        sql = string.Format(sql, yesterday, Today);


        // sql = "select rownum,t.* from (" + sql + ")t  ";

        ds_temp1 = func.get_dataSet_access(sql, conn1);

        GridView1.DataSource = ds_temp1;
        GridView1.DataBind();



        return ds_temp1;




    }

    protected DataSet bind_data2()
    {

        sql = @"

         select (case                                                               
        when substr(t.lot_id, 6, 1) = 'A' then                              
         'TFT'                                                              
        else                                                                
         'TP'                                                               
      end) as CATEGORY,                                                     
      t.lot_id,                                                             
      t.lot_type_attribute as TYPE,
      t.cmmt_code as Scrap_type,                                                  
      case when  t.cmmt_code='Unscrap'  then -t.scrap_qty
               else t.scrap_qty end  as QTY,   
                                                      
      t.stage,                                                              
      to_char(t.claim_dttm,'YYYY/MM/DD HH24:MM') as ScrapTime,              
      Convert( t.SCRAP_CMMT || ' ', 'UTF8','ZHT16MSWIN950' ) as SCRAP_CMMT  
                                                                            
 from innrpt.scrap_lot_history t                                            
where t.lot_type <> 'MQC'                                                   
  and t.shop = 'T1Array'                                                    
  and to_char(t.claim_dttm, 'YYYY/MM/DD HH24:MM')>='{0} 07:00'
  and to_char(t.claim_dttm, 'YYYY/MM/DD HH24:MM')<'{1} 06:59'     
                                                                            
order by (case                                                              
           when substr(t.lot_id, 6, 1) = 'A' then                           
            'TFT'                                                           
           else                                                             
            'TP'                                                            
         end) desc,                                                         
         t.claim_dttm  
";
        sql =string.Format(sql,yesterday,Today);

       
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
