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

public partial class OEE_oee_cell_beol : System.Web.UI.Page
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


            txtEstimateSTARTTIME.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd"));
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

select ot1.*,to_char(sysdate,'yyyyMMddHH24MISS') as DTTM,ot2.up_pct,ot2.run_pct,ot2.uph_pct,ot2.uph as uph_target from (
select substr(t.line, 0, 2) as shop,
       'MES' as SRC_SYS_CD,
       case
         when substr(t.line, 0, 2) = 'T0' or substr(t.line, 0, 2) = 'T1' then
          'CNL1'
         when substr(t.line, 0, 2) = 'T2' then
          'CNL2'
         else
          'NA'
       end PLANT_LOC_ID,
      t.cutoffkey as MFG_DT,
      t1.EQUIP_PROC_TYPE_CD,
      t.equipmentid as equip_id,
      '0' as eng_meas,
      round(t.dmqc/3600,2) as eq_warm_up_meas,
      '0' as PE_WARM_UP_MEAS,
      round(t.pmmqc/3600,2) as OTH_WARM_UP_MEAS,
      round((t.eqd/3600+t.alm/3600),2) as EQ_DOWN_MEAS,
      '0' as PE_DOWN_MEAS,
      round(t.pm/3600,2) as PM_MEAS,
      round(t.nst/3600,2)   as SHDN_MEAS,
      '0 'as FAC_DOWN_MEAS,
      '0' as SYS_DOWN_MEAS,
      '0' as EQ_MOD_MEAS,
     round((t.dmqc/3600+t.PMMQC/3600+t.eqd/3600+t.alm/3600+t.pm/3600+t.nst/3600 ),2)    as DOWN_TIME_MEAS,
      round(t.prd/3600,2) as RUN_MEAS,
      '0' as MRUN_MEAS,
      round(t.Eng/3600,2) as ERUN_MEAS,
      round(t.sby/3600,2) as IDLE_MEAS,
      round(t.setup/3600,2) as PSETUP_MEAS,
      '0' as ESETUP_MEAS,
      '0' as MTRL_CHG_MEAS,
      '0' as AMHS_MEAS,
      round((t.dmqc/3600+t.pmmqc/3600+t.eqd/3600+t.alm/3600+t.pm/3600+t.nst/3600+t.prd/3600+t.eng/3600+t.sby/3600+t.setup/3600),2)  as TTL_TIME_MEAS,
      round((t.prd/3600+t.eng/3600+t.sby/3600+t.setup/3600),2)  as UP_TIME_MEAS,
       round(( t.prd/3600+t.eng/3600+t.setup/3600 ),2)as RUN_TIME_MEAS,
      round(t.eng/3600,2)  as ELOT_MEAS,
       round(((t.prd/3600)*t1.uph),2) as TGT_MOVE_QTY,
      case when t2.oee_move is null then 0 else t2.oee_move end as MOVE_PROD_QTY,
       '0' as MOVE_ENG_QTY,
       case when t2.TGT_UPH_MEAS is null then 0 else round(t2.TGT_UPH_MEAS,2) end as TGT_UPH_MEAS ,
       case when (case when (t2.oee_move=0 or t.prd=0) then 0
       else  round((3600/(t2.oee_move/(t.prd/3600)) ),2)end) is null then 0 else (case when (t2.oee_move=0 or t.prd=0) then 0
       else  round((3600/(t2.oee_move/(t.prd/3600)) ),2)end) end WTT_MEAS,
       '0' as TGT_UPH_RATE,
       round((t1.up_pct*t1.run_pct*t1.uph_pct),2) as TGT_OEE_RATE,
       '0' as TGT_UP_RATE,
       '0' as TGT_RUN_RATE,
       round((t.prd/3600+t.eng/3600+t.sby/3600+t.setup/3600)-t.sby/3600+(t.sby/3600+t.nst/3600)*t1.up_pct,2)  as UP_TIME_MOD_MEAS,
       round((t.prd/3600+t.eng/3600+t.setup/3600)+(t.sby/3600+t.nst/3600)*t1.up_pct*t1.run_pct,2) as RUN_TIME_MOD_MEAS,
       round((t.nst/3600+t.sby/3600),2)  as SHDW_IDLE_TIME_MEAS,
       case when t2.oee_move is null then 0 else round(t2.oee_move/24,2) end as UPH_ACTUAL
       from empaidxsummdaily t,cel_pp_beol_cofig t1,(
select t.cutoffcycle,
       t.equipmentid,
    3600/(case when  (sum(t.totalprocessedcount * t.endtacttime) / (case
         when sum(t.totalprocessedcount) = 0 then
          1
         else
          sum(t.totalprocessedcount)
       end)) =0 then 1
       else (sum(t.totalprocessedcount * t.endtacttime) / (case
         when sum(t.totalprocessedcount) = 0 then
          1
         else
          sum(t.totalprocessedcount)
       end)) end)  as TGT_UPH_MEAS,
        sum(t.totalprocessedcount) oee_move
       
  from empastsummdaily t

 where  t.cutoffkey>='{0}'
        and t.cutoffkey<'{1}'
   and t.cutoffcycle = 'D'

 group by t.cutoffcycle, t.equipmentid
  
  )t2
 where      t.cutoffkey>='{0}'
        and t.cutoffkey<'{1}'
   and t.cutoffcycle = 'D'
   and t.equipmentid in (
   
   select t5.equip_id from  cel_pp_beol_cofig t5 where t5.plant_loc_id='CNL1'
   
       )
  
   and t.equipmentid= t1.equip_id
   and t.equipmentid=t2.equipmentid(+)

)ot1,(select t.equip_id,t.up_pct,t.run_pct,t.uph_pct,t.uph from cel_pp_beol_cofig t)ot2
where ot1.equip_id=ot2.equip_id
order by ot1.equip_id ,ot1.mfg_dt




";


            sql_temp = string.Format(sql_temp, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyyMMdd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyyMMdd"));


            Bind_data(sql_temp, conn1);

        }











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


select ot1.*,to_char(sysdate,'yyyyMMddHH24MISS') as DTTM,ot2.up_pct,ot2.run_pct,ot2.uph_pct,ot2.uph as uph_target from (
select substr(t.line, 0, 2) as shop,
       'MES' as SRC_SYS_CD,
       case
         when substr(t.line, 0, 2) = 'T0' or substr(t.line, 0, 2) = 'T1' then
          'CNL1'
         when substr(t.line, 0, 2) = 'T2' then
          'CNL2'
         else
          'NA'
       end PLANT_LOC_ID,
      t.cutoffkey as MFG_DT,
      t1.EQUIP_PROC_TYPE_CD,
      t.equipmentid as equip_id,
      '0' as eng_meas,
      round(t.dmqc/3600,2) as eq_warm_up_meas,
      '0' as PE_WARM_UP_MEAS,
      round(t.pmmqc/3600,2) as OTH_WARM_UP_MEAS,
      round((t.eqd/3600+t.alm/3600),2) as EQ_DOWN_MEAS,
      '0' as PE_DOWN_MEAS,
      round(t.pm/3600,2) as PM_MEAS,
      round(t.nst/3600,2)   as SHDN_MEAS,
      '0 'as FAC_DOWN_MEAS,
      '0' as SYS_DOWN_MEAS,
      '0' as EQ_MOD_MEAS,
     round((t.dmqc/3600+t.PMMQC/3600+t.eqd/3600+t.alm/3600+t.pm/3600+t.nst/3600 ),2)    as DOWN_TIME_MEAS,
      round(t.prd/3600,2) as RUN_MEAS,
      '0' as MRUN_MEAS,
      round(t.Eng/3600,2) as ERUN_MEAS,
      round(t.sby/3600,2) as IDLE_MEAS,
      round(t.setup/3600,2) as PSETUP_MEAS,
      '0' as ESETUP_MEAS,
      '0' as MTRL_CHG_MEAS,
      '0' as AMHS_MEAS,
      round((t.dmqc/3600+t.pmmqc/3600+t.eqd/3600+t.alm/3600+t.pm/3600+t.nst/3600+t.prd/3600+t.eng/3600+t.sby/3600+t.setup/3600),2)  as TTL_TIME_MEAS,
      round((t.prd/3600+t.eng/3600+t.sby/3600+t.setup/3600),2)  as UP_TIME_MEAS,
       round(( t.prd/3600+t.eng/3600+t.setup/3600 ),2)as RUN_TIME_MEAS,
      round(t.eng/3600,2)  as ELOT_MEAS,
       round(((t.prd/3600)*t1.uph),2) as TGT_MOVE_QTY,
      case when t2.oee_move is null then 0 else t2.oee_move end as MOVE_PROD_QTY,
       '0' as MOVE_ENG_QTY,
       case when t2.TGT_UPH_MEAS is null then 0 else round(t2.TGT_UPH_MEAS,2) end as TGT_UPH_MEAS ,
       case when (case when (t2.oee_move=0 or t.prd=0) then 0
       else  round((3600/(t2.oee_move/(t.prd/3600)) ),2)end) is null then 0 else (case when (t2.oee_move=0 or t.prd=0) then 0
       else  round((3600/(t2.oee_move/(t.prd/3600)) ),2)end) end WTT_MEAS,
       '0' as TGT_UPH_RATE,
       round((t1.up_pct*t1.run_pct*t1.uph_pct),2) as TGT_OEE_RATE,
       '0' as TGT_UP_RATE,
       '0' as TGT_RUN_RATE,
       round((t.prd/3600+t.eng/3600+t.sby/3600+t.setup/3600)-t.sby/3600+(t.sby/3600+t.nst/3600)*t1.up_pct,2)  as UP_TIME_MOD_MEAS,
       round((t.prd/3600+t.eng/3600+t.setup/3600)+(t.sby/3600+t.nst/3600)*t1.up_pct*t1.run_pct,2) as RUN_TIME_MOD_MEAS,
       round((t.nst/3600+t.sby/3600),2)  as SHDW_IDLE_TIME_MEAS,
       case when t2.oee_move is null then 0 else round(t2.oee_move/24,2) end as UPH_ACTUAL
       from empaidxsummdaily t,cel_pp_beol_cofig t1,(
select t.cutoffcycle,
       t.equipmentid,
    3600/(case when  (sum(t.totalprocessedcount * t.endtacttime) / (case
         when sum(t.totalprocessedcount) = 0 then
          1
         else
          sum(t.totalprocessedcount)
       end)) =0 then 1
       else (sum(t.totalprocessedcount * t.endtacttime) / (case
         when sum(t.totalprocessedcount) = 0 then
          1
         else
          sum(t.totalprocessedcount)
       end)) end)  as TGT_UPH_MEAS,
        sum(t.totalprocessedcount) oee_move
       
  from empastsummdaily t

 where  t.cutoffkey>='{0}'
        and t.cutoffkey<'{1}'
   and t.cutoffcycle = 'D'

 group by t.cutoffcycle, t.equipmentid
  
  )t2
 where      t.cutoffkey>='{0}'
        and t.cutoffkey<'{1}'
   and t.cutoffcycle = 'D'
   and t.equipmentid in (
   
   select t5.equip_id from  cel_pp_beol_cofig t5 where t5.plant_loc_id='CNL1'
   
       )
  
   and t.equipmentid= t1.equip_id
   and t.equipmentid=t2.equipmentid(+)

)ot1,(select t.equip_id,t.up_pct,t.run_pct,t.uph_pct,t.uph from cel_pp_beol_cofig t)ot2
where ot1.equip_id=ot2.equip_id
order by ot1.equip_id ,ot1.mfg_dt





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


select ot1.*,to_char(sysdate,'yyyyMMddHH24MISS') as DTTM,ot2.up_pct,ot2.run_pct,ot2.uph_pct,ot2.uph as uph_target from (
select substr(t.line, 0, 2) as shop,
       'MES' as SRC_SYS_CD,
       case
         when substr(t.line, 0, 2) = 'T0' or substr(t.line, 0, 2) = 'T1' then
          'CNL1'
         when substr(t.line, 0, 2) = 'T2' then
          'CNL2'
         else
          'NA'
       end PLANT_LOC_ID,
      t.cutoffkey as MFG_DT,
      t1.EQUIP_PROC_TYPE_CD,
      t.equipmentid as equip_id,
      '0' as eng_meas,
      round(t.dmqc/3600,2) as eq_warm_up_meas,
      '0' as PE_WARM_UP_MEAS,
      round(t.pmmqc/3600,2) as OTH_WARM_UP_MEAS,
      round((t.eqd/3600+t.alm/3600),2) as EQ_DOWN_MEAS,
      '0' as PE_DOWN_MEAS,
      round(t.pm/3600,2) as PM_MEAS,
      round(t.nst/3600,2)   as SHDN_MEAS,
      '0 'as FAC_DOWN_MEAS,
      '0' as SYS_DOWN_MEAS,
      '0' as EQ_MOD_MEAS,
     round((t.dmqc/3600+t.PMMQC/3600+t.eqd/3600+t.alm/3600+t.pm/3600+t.nst/3600 ),2)    as DOWN_TIME_MEAS,
      round(t.prd/3600,2) as RUN_MEAS,
      '0' as MRUN_MEAS,
      round(t.Eng/3600,2) as ERUN_MEAS,
      round(t.sby/3600,2) as IDLE_MEAS,
      round(t.setup/3600,2) as PSETUP_MEAS,
      '0' as ESETUP_MEAS,
      '0' as MTRL_CHG_MEAS,
      '0' as AMHS_MEAS,
      round((t.dmqc/3600+t.pmmqc/3600+t.eqd/3600+t.alm/3600+t.pm/3600+t.nst/3600+t.prd/3600+t.eng/3600+t.sby/3600+t.setup/3600),2)  as TTL_TIME_MEAS,
      round((t.prd/3600+t.eng/3600+t.sby/3600+t.setup/3600),2)  as UP_TIME_MEAS,
       round(( t.prd/3600+t.eng/3600+t.setup/3600 ),2)as RUN_TIME_MEAS,
      round(t.eng/3600,2)  as ELOT_MEAS,
       round(((t.prd/3600)*t1.uph),2) as TGT_MOVE_QTY,
      case when t2.oee_move is null then 0 else t2.oee_move end as MOVE_PROD_QTY,
       '0' as MOVE_ENG_QTY,
       case when t2.TGT_UPH_MEAS is null then 0 else round(t2.TGT_UPH_MEAS,2) end as TGT_UPH_MEAS ,
       case when (case when (t2.oee_move=0 or t.prd=0) then 0
       else  round((3600/(t2.oee_move/(t.prd/3600)) ),2)end) is null then 0 else (case when (t2.oee_move=0 or t.prd=0) then 0
       else  round((3600/(t2.oee_move/(t.prd/3600)) ),2)end) end WTT_MEAS,
       '0' as TGT_UPH_RATE,
       round((t1.up_pct*t1.run_pct*t1.uph_pct),2) as TGT_OEE_RATE,
       '0' as TGT_UP_RATE,
       '0' as TGT_RUN_RATE,
       round((t.prd/3600+t.eng/3600+t.sby/3600+t.setup/3600)-t.sby/3600+(t.sby/3600+t.nst/3600)*t1.up_pct,2)  as UP_TIME_MOD_MEAS,
       round((t.prd/3600+t.eng/3600+t.setup/3600)+(t.sby/3600+t.nst/3600)*t1.up_pct*t1.run_pct,2) as RUN_TIME_MOD_MEAS,
       round((t.nst/3600+t.sby/3600),2)  as SHDW_IDLE_TIME_MEAS,
       case when t2.oee_move is null then 0 else round(t2.oee_move/24,2) end as UPH_ACTUAL
       from empaidxsummdaily t,cel_pp_beol_cofig t1,(
select t.cutoffcycle,
       t.equipmentid,
    3600/(case when  (sum(t.totalprocessedcount * t.endtacttime) / (case
         when sum(t.totalprocessedcount) = 0 then
          1
         else
          sum(t.totalprocessedcount)
       end)) =0 then 1
       else (sum(t.totalprocessedcount * t.endtacttime) / (case
         when sum(t.totalprocessedcount) = 0 then
          1
         else
          sum(t.totalprocessedcount)
       end)) end)  as TGT_UPH_MEAS,
        sum(t.totalprocessedcount) oee_move
       
  from empastsummdaily t

 where  t.cutoffkey>='{0}'
        and t.cutoffkey<'{1}'
   and t.cutoffcycle = 'D'

 group by t.cutoffcycle, t.equipmentid
  
  )t2
 where      t.cutoffkey>='{0}'
        and t.cutoffkey<'{1}'
   and t.cutoffcycle = 'D'
   and t.equipmentid in (
   
   select t5.equip_id from  cel_pp_beol_cofig t5 where t5.plant_loc_id='CNL1'
   
       )
  
   and t.equipmentid= t1.equip_id
   and t.equipmentid=t2.equipmentid(+)

)ot1,(select t.equip_id,t.up_pct,t.run_pct,t.uph_pct,t.uph from cel_pp_beol_cofig t)ot2
where ot1.equip_id=ot2.equip_id
order by ot1.equip_id ,ot1.mfg_dt




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

            #region MyRegion

            //string run2_non = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "run2_non")).ToString("P2");

            //e.Row.Cells[14].Text = run2_non;

            //string UP2_NON = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "UP2_NON")).ToString("P2");

            //e.Row.Cells[15].Text = UP2_NON;

            //string OEE2_NON = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OEE2_NON")).ToString("P2");

            //e.Row.Cells[16].Text = OEE2_NON;

            //string RUN2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RUN2")).ToString("P2");

            //e.Row.Cells[17].Text = RUN2;

            //string UP2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "UP2")).ToString("P2");

            //e.Row.Cells[18].Text = UP2;


            //string OEE2 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "OEE2")).ToString("P2");

            //e.Row.Cells[19].Text = OEE2;

            #endregion



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
        filename = "T1CELL_BOLE" + today_detail_char + ".xls";
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
