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

public partial class OEE_c2_tactime_enhance : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ALCS_XLS"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];


    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_minus7 = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");

    ArrayList arlist_temp1 = new ArrayList();


    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {


            txtEstimateSTARTTIME.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd"));
            txtEstimateEndTime.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd"));




            //#region hour
            //arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\hour.txt");





            //DropDownList3.DataSource = arlist_temp1;
            //DropDownList3.DataBind();
            //DropDownList3.Items.Insert(0, today_hour);



            //DropDownList5.DataSource = arlist_temp1;
            //DropDownList5.DataBind();
            //DropDownList5.Items.Insert(0, today_hour);

            //#endregion


            //#region min
            //arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\min.txt");

            //DropDownList4.DataSource = arlist_temp1;
            //DropDownList4.DataBind();
            //DropDownList4.Items.Insert(0, today_hour);


            //DropDownList6.DataSource = arlist_temp1;
            //DropDownList6.DataBind();
            //DropDownList6.Items.Insert(0, today_min);


            //#endregion
            sql_temp = @"



select ot2.line,ot2.equipmentid,ot2.enddatetime,ot2.productid,ot2.lotid,ot2.glassid,ot2.stepid,ot2.endtacttime from empaglasshistory ot2
where ot2.equipmentid in 
(

select ot1.equipmentid from (
select t.line,t.equipmentid,t.area,substr(t.equipmentid,3,3) eq from equipment t
where t.line='{0}' and  t.area='{1}' and t.modulelevel='0' 
)ot1
where ot1.eq='{2}'
)
and ot2.ENDDATETIME>'{3}'
and ot2.ENDDATETIME<='{4}'
and ot2.line='{0}'
order by ot2.line,ot2.equipmentid,ot2.ENDDATETIME


";




            sql_temp = string.Format(sql_temp, "T0ARRAY", "0A-TF", "PDC", txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyy-MM-dd") + " 07:00", txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd") + " 07:00");



            Bind_data_filter_pivote(sql_temp, conn1);

        }











    }


    public DataTable Bind_data_filter_pivote(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);



        DataTable dt = new DataTable();

       

        dt.Columns.Add("LINE", typeof(string));
        dt.Columns.Add("EQUIPMENTID", typeof(string));
        dt.Columns.Add("ENDDATETIME", typeof(string));
        dt.Columns.Add("PRODUCTID", typeof(string));
        dt.Columns.Add("LOTID", typeof(string));
        dt.Columns.Add("GLASSID", typeof(string));
        dt.Columns.Add("STEPID", typeof(string));
        dt.Columns.Add("ENDTACTTIME", typeof(string));
        dt.Columns.Add("PILOT", typeof(string));

        string spilot = "";
        string temp_lot = "";
        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count-1; i++)
        {

            if (i == 0)
            {
                spilot = "Y";
                temp_lot = ds_temp.Tables[0].Rows[i]["LOTID"].ToString();

            }
            else
            {
                if (ds_temp.Tables[0].Rows[i]["LOTID"].ToString().Equals(temp_lot))
                {

                    spilot = "N";
                    


                }
                else
                {
                    spilot = "Y";
                
                }

                temp_lot = ds_temp.Tables[0].Rows[i]["LOTID"].ToString();






               
            }


            DataRow dr_temp = dt.NewRow();

            dr_temp["LINE"] = ds_temp.Tables[0].Rows[i]["LINE"].ToString();
            dr_temp["EQUIPMENTID"] = ds_temp.Tables[0].Rows[i]["EQUIPMENTID"].ToString();
            dr_temp["ENDDATETIME"] = ds_temp.Tables[0].Rows[i]["ENDDATETIME"].ToString();
            dr_temp["PRODUCTID"] = ds_temp.Tables[0].Rows[i]["PRODUCTID"].ToString();
            dr_temp["LOTID"] = ds_temp.Tables[0].Rows[i]["LOTID"].ToString();
            dr_temp["GLASSID"] = ds_temp.Tables[0].Rows[i]["GLASSID"].ToString();
            dr_temp["STEPID"] = ds_temp.Tables[0].Rows[i]["STEPID"].ToString();
            dr_temp["ENDTACTTIME"] = ds_temp.Tables[0].Rows[i]["ENDTACTTIME"].ToString();
            dr_temp["PILOT"] = spilot;


            dt.Rows.Add(dr_temp); 

            

        }







        Label1.Text = dt.Rows.Count.ToString();

        GridView1.DataSource = dt;


        GridView1.DataBind();



        return dt;

    }

    public DataSet Bind_data(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);

        Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();

        GridView1.DataSource = ds_temp.Tables[0];


        GridView1.DataBind();



        return ds_temp;

    }

    protected void ButtonQuery_Click(object sender, EventArgs e)
    {

        sql_temp = @"

select ot6.*,round(ot6.run2*ot6.up2,4) as oee2 from (

select ot5.*,
       round(ot5.run2_non * ot5.up2_non, 4) as oee2_non,
       case
         when (ot5.run + ot5.setup + ot5.idle + ot5.amhs_idle * 0.91) > 0 then
          round((ot5.run + ot5.setup + ot5.idle * (select t.run2 from empapp t where t.name='D' )) /
                (ot5.run + ot5.setup + ot5.idle + ot5.amhs_idle * (select t.up from empapp t where t.name='D' )),
                4)
         else
          0
       end Run2,
         case
         when (ot5.run + ot5.setup + ot5.idle + ot5.amhs_idle+ot5.off * (select t.run2 from empapp t where t.name='D' )) > 0 then
          round((ot5.run + ot5.setup + ot5.idle + ot5.amhs_idle+ot5.off * (select t.run2 from empapp t where t.name='D' )) /
                ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd') + 1
                            from dual) * 24 * 60),
                4)
         else
          0
       end up2
       

  from (select ot4.*,
               
               case
                 when ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle > 0 then
                  round((ot4.run + ot4.setup) /
                        (ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle),
                        4)
                 else
                  0
               end as Run2_non,
               case
                 when (select to_date('{1}', 'yyyyMMdd') -
                              to_date('{0}', 'yyyyMMdd') + 1
                         from dual) > 0 then
                  round((ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle) /
                        ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd') + 1
                            from dual) * 24 * 60),
                        4)
                 else
                  0
               end as Up2_non
        
          from (
                
                select '{0}' as starttime, '{1}' as endtime, ot3.*
                  from (
                         
                         select ot2.*
                           from (
                                  
                                  select ot1.eqpid,
                                          ot1.act,
                                          sum(ot1.run) as run,
                                          sum(ot1.E_RUN) as e_run,
                                          sum(ot1.M_RUN) as m_run,
                                          sum(ot1.setup) as setup,
                                          sum(ot1.P_setup) as p_setup,
                                          sum(ot1.e_setup) as e_setup,
                                          sum(ot1.idle) as idle,
                                          sum(ot1.amhs_idle) as amhs_idle,
                                          sum(ot1.off) as off
                                    from (select idx.cutoffkey,
                                                  eq.modulename as EQPID,
                                                  'Act.' as Act,
                                                  --ROUND(idx.TTM / 60, 2) as TTM,
                                                  ROUND(idx.prd / 60, 2) as RUN,
                                                  '0' as E_RUN,
                                                  '0' as M_RUN,
                                                  ROUND(idx.setup / 60, 2) as SETUP,
                                                  '0' as P_SETUP,
                                                  '0' as E_SETUP,
                                                  ROUND(idx.sby / 60, 2) as IDLE,
                                                  --ROUND(idx.ENG / 60, 2) as ENG,
                                                  
                                                  /*ROUND(idx.pm / 60, 2) as PM,
                                                                                                 ROUND(idx.pmmqc / 60, 2) as PM_MQC,
                                                                                                 ROUND(idx.eqd / 60, 2) as EQ_D,
                                                                                                 ROUND(idx.alm / 60, 2) as ALARM,
                                                                                                 ROUND(idx.dmqc / 60, 2) as D_MQC,*/
                                                  '0' as AMHS_IDLE,
                                                  ROUND(idx.nst / 60, 2) as off
                                             from empaidxsummdaily idx, equipment eq
                                            where idx.line = eq.line
                                              and idx.equipmentid = eq.modulename
                                              and eq.line = 'T1ARRAY'
                                              and eq.area = '1A-PHOTO'
                                              and eq.moduletype = 'MAIN'
                                              and eq.equipmentid like '1APHT%'
                                              and idx.cutoffcycle = 'D'
                                              and idx.cutoffkey >= '{0}'
                                              and idx.cutoffkey <= '{1}'
                                           --order by EQPID
                                           
                                           ) ot1
                                   group by ot1.eqpid, ot1.act
                                  
                                  --order by EQPID 
                                  ) ot2
                         
                         union all
                         
                          (select
                           
                           distinct (tt.equipmentid),
                                    
                                    'Tgt.' as ACT,
                                    0 as RUN,
                                    0 as E_RUN,
                                    0 as M_RUN,
                                    0 as SETUP,
                                    0 as P_SETUP,
                                    0 as E_SETUP,
                                    0 as IDLE,
                                    0 as AMHS_IDLE,
                                    0 as OFF
                           
                             from equipment tt
                            where tt.equipmentid like '1APH%')
                         
                         ) ot3
                
                 order by ot3.eqpid, ot3.act
                
                ) ot4) ot5
)ot6

";



        sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd"));






        ds_temp = func.get_dataSet_access(sql_temp, conn1);


        Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();

        GridView1.DataSource = ds_temp.Tables[0];
        GridView1.DataBind();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        sql_temp = @"

select ot6.*,round(ot6.run2*ot6.up2,4) as oee2 from (

select ot5.*,
       round(ot5.run2_non * ot5.up2_non, 4) as oee2_non,
       case
         when (ot5.run + ot5.setup + ot5.idle + ot5.amhs_idle * 0.91) > 0 then
          round((ot5.run + ot5.setup + ot5.idle * (select t.run2 from empapp t where t.name='D' )) /
                (ot5.run + ot5.setup + ot5.idle + ot5.amhs_idle * (select t.up from empapp t where t.name='D' )),
                4)
         else
          0
       end Run2,
         case
         when (ot5.run + ot5.setup + ot5.idle + ot5.amhs_idle+ot5.off * (select t.run2 from empapp t where t.name='D' )) > 0 then
          round((ot5.run + ot5.setup + ot5.idle + ot5.amhs_idle+ot5.off * (select t.run2 from empapp t where t.name='D' )) /
                ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd') + 1
                            from dual) * 24 * 60),
                4)
         else
          0
       end up2
       

  from (select ot4.*,
               
               case
                 when ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle > 0 then
                  round((ot4.run + ot4.setup) /
                        (ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle),
                        4)
                 else
                  0
               end as Run2_non,
               case
                 when (select to_date('{1}', 'yyyyMMdd') -
                              to_date('{0}', 'yyyyMMdd') + 1
                         from dual) > 0 then
                  round((ot4.run + ot4.setup + ot4.idle + ot4.amhs_idle) /
                        ((select to_date('{1}', 'yyyyMMdd') -
                                 to_date('{0}', 'yyyyMMdd') + 1
                            from dual) * 24 * 60),
                        4)
                 else
                  0
               end as Up2_non
        
          from (
                
                select '{0}' as starttime, '{1}' as endtime, ot3.*
                  from (
                         
                         select ot2.*
                           from (
                                  
                                  select ot1.eqpid,
                                          ot1.act,
                                          sum(ot1.run) as run,
                                          sum(ot1.E_RUN) as e_run,
                                          sum(ot1.M_RUN) as m_run,
                                          sum(ot1.setup) as setup,
                                          sum(ot1.P_setup) as p_setup,
                                          sum(ot1.e_setup) as e_setup,
                                          sum(ot1.idle) as idle,
                                          sum(ot1.amhs_idle) as amhs_idle,
                                          sum(ot1.off) as off
                                    from (select idx.cutoffkey,
                                                  eq.modulename as EQPID,
                                                  'Act.' as Act,
                                                  --ROUND(idx.TTM / 60, 2) as TTM,
                                                  ROUND(idx.prd / 60, 2) as RUN,
                                                  '0' as E_RUN,
                                                  '0' as M_RUN,
                                                  ROUND(idx.setup / 60, 2) as SETUP,
                                                  '0' as P_SETUP,
                                                  '0' as E_SETUP,
                                                  ROUND(idx.sby / 60, 2) as IDLE,
                                                  --ROUND(idx.ENG / 60, 2) as ENG,
                                                  
                                                  /*ROUND(idx.pm / 60, 2) as PM,
                                                                                                 ROUND(idx.pmmqc / 60, 2) as PM_MQC,
                                                                                                 ROUND(idx.eqd / 60, 2) as EQ_D,
                                                                                                 ROUND(idx.alm / 60, 2) as ALARM,
                                                                                                 ROUND(idx.dmqc / 60, 2) as D_MQC,*/
                                                  '0' as AMHS_IDLE,
                                                  ROUND(idx.nst / 60, 2) as off
                                             from empaidxsummdaily idx, equipment eq
                                            where idx.line = eq.line
                                              and idx.equipmentid = eq.modulename
                                              and eq.line = 'T1ARRAY'
                                              and eq.area = '1A-PHOTO'
                                              and eq.moduletype = 'MAIN'
                                              and eq.equipmentid like '1APHT%'
                                              and idx.cutoffcycle = 'D'
                                              and idx.cutoffkey >= '{0}'
                                              and idx.cutoffkey <= '{1}'
                                           --order by EQPID
                                           
                                           ) ot1
                                   group by ot1.eqpid, ot1.act
                                  
                                  --order by EQPID 
                                  ) ot2
                         
                         union all
                         
                          (select
                           
                           distinct (tt.equipmentid),
                                    
                                    'Tgt.' as ACT,
                                    0 as RUN,
                                    0 as E_RUN,
                                    0 as M_RUN,
                                    0 as SETUP,
                                    0 as P_SETUP,
                                    0 as E_SETUP,
                                    0 as IDLE,
                                    0 as AMHS_IDLE,
                                    0 as OFF
                           
                             from equipment tt
                            where tt.equipmentid like '1APH%')
                         
                         ) ot3
                
                 order by ot3.eqpid, ot3.act
                
                ) ot4) ot5
)ot6

";



        sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd"));




        //if (!TextBox1.Text.Equals(""))
        //{

        //    sql_temp = sql_temp + " and upper(ob1.user_mobil_tel) like '%" + TextBox1.Text.ToString().ToUpper() + "%' or upper(ob1.user_sms_num) like '%" + TextBox1.Text.ToString().ToUpper() + "%' or upper(ob1.user_e_mail) like '%" + TextBox1.Text.ToString().ToUpper() + "%' ";


        //}

        //if (!TextBox3.Text.Equals(""))
        //{

        //    sql_temp = sql_temp + " and upper(ob1.event_id) like '%" + TextBox3.Text.ToString().ToUpper() + "%' ";


        //}

        //if (!TextBox_Msg.Text.Equals(""))
        //{

        //    sql_temp = sql_temp + " and upper(ob1.alarm_text) like '%" + TextBox_Msg.Text.ToString().ToUpper() + "%'  or upper(ob1.alarm_comment) like '%" + TextBox_Msg.Text.ToString().ToUpper() + "%'";


        //}



        GridView gv = new GridView();

        ds_temp = func.get_dataSet_access(sql_temp, conn1);
        Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();
        gv.DataSource = ds_temp.Tables[0];
        gv.DataBind();
        ExportExcel(gv);

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //e.Row.Cells[13] = Convert.ToDouble(e.Row.Cells[14].Text).ToString("P1");
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
            //Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 

            //string pp = DataBinder.Eval(e.Row.DataItem, "Current_price").ToString();

            //Int32 pricexx = Convert.ToInt32(price1); 



            // if (percent_value >0) 
            //e.Row.Cells[0].BackColor = Color.Yellow; 
            // e.Row.Cells[6].Style.Add("background-color", "#FFFF80"); 
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //e.Row.Cells[14].Style.Add("background-color", "#FF9DFF");

            string run2_non = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "run2_non")).ToString("P2");

            e.Row.Cells[14].Text = run2_non;

            string UP2_NON = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "UP2_NON")).ToString("P2");

            e.Row.Cells[15].Text = UP2_NON;

            string OEE2_NON = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OEE2_NON")).ToString("P2");

            e.Row.Cells[16].Text = OEE2_NON;

            string RUN2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RUN2")).ToString("P2");

            e.Row.Cells[17].Text = RUN2;

            string UP2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "UP2")).ToString("P2");

            e.Row.Cells[18].Text = UP2;


            string OEE2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OEE2")).ToString("P2");

            e.Row.Cells[19].Text = OEE2;

            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }
            //e.Row.Cells[i].Text = Convert.ToDouble(e.Row.Cells[i].Text).ToString("P1");
            // e.Row.Cells[13] = Convert.ToDouble(e.Row.Cells[14].Text).ToString("P1");
            //e.Row.Cells[14]= Convert.ToDouble(e.Row.Cells[14].Text).ToString("P1");
            // e.Row.Cells[15].Text = Convert.ToDouble(e.Row.Cells[15].Text).ToString("P1");
            //e.Row.Cells[16].Text = Convert.ToDouble(e.Row.Cells[16].Text).ToString("P1");
            //e.Row.Cells[17].Text = Convert.ToDouble(e.Row.Cells[17].Text).ToString("P1");
            //e.Row.Cells[18].Text = Convert.ToDouble(e.Row.Cells[18].Text).ToString("P1"); 
        }
    }




    private void ExportExcel(GridView SeriesValuesDataGrid)
    {

        string filename = "";
        string today_detail_char = DateTime.Now.AddDays(+0).ToString("yyyy/MM/ddHHmmss").Replace("/", "");
        filename = "T1OEE_" + today_detail_char + ".xls";
        filename = "attachment;filename=" + filename;
        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader("content-disposition", filename);

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
}
