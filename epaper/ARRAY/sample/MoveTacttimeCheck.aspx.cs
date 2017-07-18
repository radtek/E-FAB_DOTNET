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

public partial class epaper_ARRAY_sample_MoveTacttimeCheck : System.Web.UI.Page
{
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_OEE_RPT"];
   
    string sql = "";

    DataSet dsTemp1 = new DataSet();
    DataSet dsTemp2 = new DataSet();

    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    string yesterday = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Bind_data1();


        }
    }

    protected DataSet Bind_data1()
    {



        sql = @"



select to_char(sysdate-1,'yyyyMMdd') shiftdate,total.equipmentid,'NotRunStatus' status from  (


select aa.line,aa.equipmentid,aa.EmonID as motherequipmentid,aa.modulename,
           aa.productid,aa.stepid,aa.processgroup,aa.processtime,count(*) CNT,to_char(sysdate-1,'yyyyMMdd')||' 070000','PROCESS'
      from(
          select t.*,eqp.hrcheck,eqp.lrcheck,eqp.highrank,eqp.lowrank,
                 (case when t.endtacttime < nvl(eqp.lowrank,-1)  then 'L' 
                       when t.endtacttime > nvl(decode(eqp.highrank,0,999999,eqp.highrank),999999)  then 'H' 
                       else '' end ) checkinrank,
                 (case when eqp.mainseq in (1,0) then
                            decode(eqp.mainseq,t.sequence,'TRUE','FALSE')
                       else 'TRUE' end) sequenceflag,
                  eqp.equipmentid as EmonID
          from empaglasshistory t,equipment eqp
          where t.equipmentid=eqp.modulename
            and t.line=eqp.line 
            --and t.motherequipmentid=eqp.equipmentid
            and t.enddatetime >= to_char(to_date(to_char(sysdate-1,'yyyyMMdd')||' 070000','yyyymmdd hh24miss'),'yyyy-mm-dd hh24:mi:ss')
            and t.enddatetime < to_char(to_date(to_char(sysdate,'yyyyMMdd')||' 070000','yyyymmdd hh24miss'),'yyyy-mm-dd hh24:mi:ss')
            and t.savedtime<= to_date(to_char(sysdate,'yyyyMMdd')||' 070000','yyyyMMdd HH24MISS')
            --and t.line in(vLine,vLine2)
              and t.line in('T0CELL','T1CELL','T1CF','T1ARRAY','T0ARRAY')
            and t.equipmentid like '______00'
            --and t.equipmentid like '%1COCC100%'
            --and t.equipmentid='1CPHA100'
            --and t.slotid is not null
            --and t.semistate='RUN'
            and (upper(nvl(eqp.hrcheck,'FALSE'))='FALSE' and upper(nvl(eqp.lrcheck,'FALSE'))='FALSE')
            ) aa
    where aa.sequenceflag='TRUE'
      and aa.checkinrank is null
    group by  aa.line,aa.equipmentid,aa.EmonID,aa.modulename,aa.productid,aa.stepid,aa.processgroup,aa.processtime

) total 
group by total.equipmentid

minus 

(
select to_char(sysdate-1,'yyyyMMdd') shiftdate,total.equipmentid,'NotRunStatus' from  (


select aa.line,aa.equipmentid,aa.EmonID as motherequipmentid,aa.modulename,
           aa.productid,aa.stepid,aa.processgroup,aa.processtime,count(*) CNT,to_char(sysdate-1,'yyyyMMdd')||' 070000','PROCESS'
      from(
          select t.*,eqp.hrcheck,eqp.lrcheck,eqp.highrank,eqp.lowrank,
                 (case when t.endtacttime < nvl(eqp.lowrank,-1)  then 'L' 
                       when t.endtacttime > nvl(decode(eqp.highrank,0,999999,eqp.highrank),999999)  then 'H' 
                       else '' end ) checkinrank,
                 (case when eqp.mainseq in (1,0) then
                            decode(eqp.mainseq,t.sequence,'TRUE','FALSE')
                       else 'TRUE' end) sequenceflag,
                  eqp.equipmentid as EmonID
          from empaglasshistory t,equipment eqp
          where t.equipmentid=eqp.modulename
            and t.line=eqp.line 
            --and t.motherequipmentid=eqp.equipmentid
            and t.enddatetime >= to_char(to_date(to_char(sysdate-1,'yyyyMMdd')||' 070000','yyyymmdd hh24miss'),'yyyy-mm-dd hh24:mi:ss')
            and t.enddatetime < to_char(to_date(to_char(sysdate,'yyyyMMdd')||' 070000','yyyymmdd hh24miss'),'yyyy-mm-dd hh24:mi:ss')
            and t.savedtime<= to_date(to_char(sysdate,'yyyyMMdd')||' 070000','yyyyMMdd HH24MISS')
            --and t.line in(vLine,vLine2)
            and t.line in('T0CELL','T1CELL','T1CF','T1ARRAY','T0ARRAY')
            and t.equipmentid like '______00'
           
            --and t.equipmentid='1CPHA100'
            --and t.slotid is not null
           
            and t.semistate='RUN'
            and (upper(nvl(eqp.hrcheck,'FALSE'))='FALSE' and upper(nvl(eqp.lrcheck,'FALSE'))='FALSE')
            ) aa
    where aa.sequenceflag='TRUE'
      and aa.checkinrank is null
    group by  aa.line,aa.equipmentid,aa.EmonID,aa.modulename,aa.productid,aa.stepid,aa.processgroup,aa.processtime

) total 
group by total.equipmentid
  
)


union all

select to_char(sysdate-1,'yyyyMMdd') shiftdate,a.equipmentid , 'NoRowData' status from em_idxsumdaily a
where a.cutoffcycle='D' and a.cutoffkey=to_char(sysdate-1,'yyyyMMdd')
and a.equipmentid like '______00'
and a.move_qty>0
group by a.equipmentid
minus 
(
select to_char(sysdate-1,'yyyyMMdd') shiftdate,total.equipmentid,'NoRowData' status from  (


select aa.line,aa.equipmentid,aa.EmonID as motherequipmentid,aa.modulename,
           aa.productid,aa.stepid,aa.processgroup,aa.processtime,count(*) CNT,to_char(sysdate-1,'yyyyMMdd')||' 070000','PROCESS'
      from(
          select t.*,eqp.hrcheck,eqp.lrcheck,eqp.highrank,eqp.lowrank,
                 (case when t.endtacttime < nvl(eqp.lowrank,-1)  then 'L' 
                       when t.endtacttime > nvl(decode(eqp.highrank,0,999999,eqp.highrank),999999)  then 'H' 
                       else '' end ) checkinrank,
                 (case when eqp.mainseq in (1,0) then
                            decode(eqp.mainseq,t.sequence,'TRUE','FALSE')
                       else 'TRUE' end) sequenceflag,
                  eqp.equipmentid as EmonID
          from empaglasshistory t,equipment eqp
          where t.equipmentid=eqp.modulename
            and t.line=eqp.line 
            --and t.motherequipmentid=eqp.equipmentid
            and t.enddatetime >= to_char(to_date(to_char(sysdate-1,'yyyyMMdd')||' 070000','yyyymmdd hh24miss'),'yyyy-mm-dd hh24:mi:ss')
            and t.enddatetime < to_char(to_date(to_char(sysdate,'yyyyMMdd')||' 070000','yyyymmdd hh24miss'),'yyyy-mm-dd hh24:mi:ss')
            and t.savedtime<= to_date(to_char(sysdate,'yyyyMMdd')||' 070000','yyyyMMdd HH24MISS')
            --and t.line in(vLine,vLine2)
              and t.line in('T0CELL','T1CELL','T1CF','T1ARRAY','T0ARRAY')
            and t.equipmentid like '______00'
            --and t.equipmentid like '%1COCC100%'
            --and t.equipmentid='1CPHA100'
            --and t.slotid is not null
            --and t.semistate='RUN'
            --and (upper(nvl(eqp.hrcheck,'FALSE'))='FALSE' and upper(nvl(eqp.lrcheck,'FALSE'))='FALSE')
            ) aa
    where 1=1
      --and  aa.sequenceflag='TRUE'
      --and aa.checkinrank is null
    group by  aa.line,aa.equipmentid,aa.EmonID,aa.modulename,aa.productid,aa.stepid,aa.processgroup,aa.processtime

) total 
group by total.equipmentid

)

order by status



";


       

        // sql = "select rownum,t.* from (" + sql + ")t  ";

        dsTemp1 = func.get_dataSet_access(sql, conn1);

        GridView1.DataSource = dsTemp1;
        GridView1.DataBind();



        return dsTemp1;




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
