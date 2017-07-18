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

public partial class Default3 : System.Web.UI.Page
{

    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_EDA"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_SSODB_OLE"];
    string conn2 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_PARS1_OLE_ARSNEW"];
    string conn3 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_PARS1_OLE_LHEDA"];
    string sql = "";
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    Int32 count_num = 0;

    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        { 
        
           sql_temp= @"
   
select * from daily_array_lr_ng_defect_t tt
where tt.day_key > to_char(sysdate-30,'yyyyMMdd')
and tt.step_id='1930' and tt.product_id='BCFB1A'

";

           ds_temp = func.get_dataSet_access(sql_temp, conn);

          GridView1.DataSource=ds_temp.Tables[0];
          GridView1.DataBind();

          GridView1.Visible = false;

          DataView dv = new DataView();
          dv = ds_temp.Tables[0].DefaultView;
          DataView dv1 = new DataView();
          dv1 = ds_temp.Tables[0].DefaultView;
          dv1.Sort = "day_key asc";
        


          DataTable DT_SHIFT_DAY = new DataTable();
          DataTable DT_DEFET_CODE = new DataTable();


          DT_SHIFT_DAY = dv1.ToTable(true, "day_key");

          DT_DEFET_CODE = dv.ToTable(true, "defect_code");

          DataTable DT = new DataTable();




          //DT.Columns.Add();
          DT.Columns.Add("shfit_day", typeof(string));

          for (int i = 0; i <= DT_DEFET_CODE.Rows.Count-1; i++)
          {
              DT.Columns.Add(DT_DEFET_CODE.Rows[i][0].ToString(), typeof(string));

          }

       

          for (int j = 0; j <= DT_SHIFT_DAY.Rows.Count-1; j++)
          {

              DataRow drNew = DT.NewRow();
              for (int k = 0; k <= DT_DEFET_CODE.Rows.Count-1; k++)
              {

                  sql_temp1 = @"

 
select case
         when sum(tt.defect_count) is null then
          0
         else
          sum(tt.defect_count)
       end as counter
  from daily_array_lr_ng_defect_t tt
 where tt.day_key > to_char(sysdate - 30, 'yyyyMMdd')
   and tt.step_id = '1930'
   and tt.product_id = 'BCFB1A'
   and tt.day_key = '{0}'
   and tt.defect_code = '{1}'

";
                  sql_temp1 = string.Format(sql_temp1, DT_SHIFT_DAY.Rows[j]["day_key"].ToString(), DT_DEFET_CODE.Rows[k]["defect_code"].ToString());


                  ds_temp1=func.get_dataSet_access(sql_temp1,conn);

                 // DataRow dr = DT.NewRow();
                  drNew[0] = DT_SHIFT_DAY.Rows[j][0].ToString();
                  drNew[k + 1] = ds_temp1.Tables[0].Rows[0][0].ToString();



              
              }


              DT.Rows.Add(drNew);
             



                
                  

          }

          GridView2.DataSource = DT;
          GridView2.DataBind();
           





               

        }





    }
}
