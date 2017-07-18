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

public partial class OEE_pp_tacttime_report_query : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ALCS_XLS"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];


    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string sql_temp4 = "";
    string sql_temp5 = "";
    string sql_temp6 = "";
    string sql_temp7 = "";
    string sql_temp8 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    DataSet ds_temp3 = new DataSet();
    DataSet ds_temp4 = new DataSet();
    DataSet ds_temp5 = new DataSet();
    DataSet ds_temp6 = new DataSet();
    DataSet ds_temp7 = new DataSet();
    DataTable dt_temp = new DataTable();
    DataTable dt_temp1 = new DataTable();
    DataTable dt_temp2 = new DataTable();
    DataTable dt_temp3 = new DataTable();
    DataTable dt_temp4 = new DataTable();
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_minus7 = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");
    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm:ss");
    Int32 table_col_num = 0;
    Int32 table_row_num = 0;

    ArrayList arlist_temp1 = new ArrayList();

    string[] sline ={ "T0ARRAY" };

    string[] sArea ={ "0A-TF", "0A-ETCH", "0A-PHOTO","0A-TDTEST" };   //0A-ETCH //0A-PHOTO //0A-TDTEST //0A-TF  //AT

    string[] seqid ={ "0APVD", "0APDC", "0AINC", "0AWET", "0ASTR", "0AEXP", "0ADEV" };

    string[] Index ={ "Product type", "Product" };

    string[] Line ={ "T0 Array" };

    string[] Product_type ={ "TFT", "ITO_B", "MET_B" };
    

    protected void Page_Load(object sender, EventArgs e)
    {


        if(!IsPostBack)
        {

            string sql_temp8 = @"
select to_char(trunc(sysdate+7,'IW')-1,'yyyy/MM/dd') as datetime from dual
union
select to_char(trunc(sysdate,'IW')-1,'yyyy/MM/dd') as datetime from dual

";

            ds_temp7 = func.get_dataSet_access(sql_temp8, conn1);


            txtEstimateSTARTTIME.SelectedDate = Convert.ToDateTime(ds_temp7.Tables[0].Rows[0]["datetime"].ToString());
            txtEstimateEndTime.SelectedDate = Convert.ToDateTime(ds_temp7.Tables[0].Rows[1]["datetime"].ToString());



            DropDownList1.DataSource = Index;
            DropDownList1.DataBind();


            DropDownList2.DataSource = Line;
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0,"ALL");

            #region MyRegion




            #endregion

            sql_temp3 = @"


select distinct(ot1.product_type) as product_type from (


select      t.productid,
            t.product_type,
         t.moduale,
         case when t.equiptype like '0AEXP100' then '0AEXP-C' 
            when t.equiptype like '0AEXP200' then '0AEXP-C' 
            when t.equiptype like '0AEXP700' then '0AEXP-C'
            when t.equiptype like '0AEXP800' then '0AEXP-C' 
            when t.equiptype like '0AEXP900' then '0AEXP-C' 
            when t.equiptype like '0AEXPA00' then '0AEXP-C' 
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP400' then '0AEXP-N'
            when t.equiptype like '0AEXP500' then '0AEXP-N'
            when t.equiptype like '0AEXP600' then '0AEXP-N'
            when t.equiptype like '0AEXPB00' then '0AEXP-S'
            when t.equiptype like '0AEXPC00' then '0AEXP-S'
            else 
            substr(t.equiptype,0,5) end as equiptype,
            t.equiptype as equipid ,
         
            t.layer ,
            round(sum(t.counter1)/sum(t.counter) ,2) as tacttime,
             round(sum(t.counter)/0.95,0) as total_cnt
            
            from pp_mode_data_cal t
             where 1=1
             and t.dttm>=to_date('{0}'||' 07:00','yyyy/MM/dd HH24:MI')
             and t.dttm<to_date('{1}'||' 07:00','yyyy/MM/dd HH24:MI')


group by  t.productid,
         t.product_type,
         t.moduale,
         case when t.equiptype like '0AEXP100' then '0AEXP-C' 
            when t.equiptype like '0AEXP200' then '0AEXP-C' 
            when t.equiptype like '0AEXP700' then '0AEXP-C'
            when t.equiptype like '0AEXP800' then '0AEXP-C' 
            when t.equiptype like '0AEXP900' then '0AEXP-C' 
            when t.equiptype like '0AEXPA00' then '0AEXP-C' 
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP400' then '0AEXP-N'
            when t.equiptype like '0AEXP500' then '0AEXP-N'
            when t.equiptype like '0AEXP600' then '0AEXP-N'
            when t.equiptype like '0AEXPB00' then '0AEXP-S'
            when t.equiptype like '0AEXPC00' then '0AEXP-S'
            else 
            substr(t.equiptype,0,5) end  ,
            t.equiptype,
         
            t.layer
             

)ot1
order by ot1.product_type






";

            sql_temp3 = string.Format(sql_temp3, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyy/MM/dd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyy/MM/dd"));


            ds_temp3 = func.get_dataSet_access(sql_temp3, conn1);



            DropDownList3.DataTextField = "product_type";
            DropDownList3.DataSource = ds_temp3.Tables[0];
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, "ALL");



            string tmp_equip = "";
            string tmp_Product_type = "";
            if (DropDownList2.SelectedValue.Equals("T0 Array"))
            {

                tmp_equip = "0A";

            }
            else if (DropDownList2.SelectedValue.Equals("ALL"))
            {
                tmp_equip = "0A";

            }
            else
            {
                tmp_equip = "1A";


            }

            if (DropDownList3.SelectedValue.Equals("ALL"))
            {

                tmp_Product_type = "";

            }
            else
            {
                tmp_Product_type = "and ot2.product_type ='" + DropDownList3.SelectedValue + "'";

            }


            if (DropDownList1.SelectedValue.Equals("Product"))
            {
              sql_temp7 = @"

select distinct(ot2.productid) as productid  from (

select      t.productid,
            t.product_type,
         t.moduale,
         case when t.equiptype like '0AEXP100' then '0AEXP-C' 
            when t.equiptype like '0AEXP200' then '0AEXP-C' 
            when t.equiptype like '0AEXP700' then '0AEXP-C'
            when t.equiptype like '0AEXP800' then '0AEXP-C' 
            when t.equiptype like '0AEXP900' then '0AEXP-C' 
            when t.equiptype like '0AEXPA00' then '0AEXP-C' 
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP400' then '0AEXP-N'
            when t.equiptype like '0AEXP500' then '0AEXP-N'
            when t.equiptype like '0AEXP600' then '0AEXP-N'
            when t.equiptype like '0AEXPB00' then '0AEXP-S'
            when t.equiptype like '0AEXPC00' then '0AEXP-S'
            else 
            substr(t.equiptype,0,5) end as equiptype,
            t.equiptype as equipid ,
         
            t.layer ,
            round(sum(t.counter1)/sum(t.counter) ,2) as tacttime,
             round(sum(t.counter)/0.95,0) as total_cnt
            
            from pp_mode_data_cal t
             where 1=1
             and t.dttm>=to_date('{0}'||' 07:00','yyyy/MM/dd HH24:MI')
             and t.dttm<to_date('{1}'||' 07:00','yyyy/MM/dd HH24:MI')


group by  t.productid,
         t.product_type,
         t.moduale,
         case when t.equiptype like '0AEXP100' then '0AEXP-C' 
            when t.equiptype like '0AEXP200' then '0AEXP-C' 
            when t.equiptype like '0AEXP700' then '0AEXP-C'
            when t.equiptype like '0AEXP800' then '0AEXP-C' 
            when t.equiptype like '0AEXP900' then '0AEXP-C' 
            when t.equiptype like '0AEXPA00' then '0AEXP-C' 
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP400' then '0AEXP-N'
            when t.equiptype like '0AEXP500' then '0AEXP-N'
            when t.equiptype like '0AEXP600' then '0AEXP-N'
            when t.equiptype like '0AEXPB00' then '0AEXP-S'
            when t.equiptype like '0AEXPC00' then '0AEXP-S'
            else 
            substr(t.equiptype,0,5) end  ,
            t.equiptype,
         
            t.layer
             
) ot2
where ot2.equiptype like '{2}%'    {3}
order by    ot2.productid, 
            ot2.product_type,
            case when ot2.moduale='TF' then 1
              when ot2.moduale='PH' then 2
              when ot2.moduale='TF/PH' then 3
              when ot2.moduale='ET' then 4
              else 5 
              end,
          ot2.equiptype,
          ot2.equipid,
          case
            when ot2.layer = '高溫ITO' then
             1
            when ot2.layer = 'GMET' then
             2
            when ot2.layer = '常溫ITO' then
             3
            when ot2.layer = 'METITO' then
             4
            else
             5
          end


";

        sql_temp7 = string.Format(sql_temp7, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyy/MM/dd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyy/MM/dd"), tmp_equip,tmp_Product_type);
        
       
        ds_temp6 = func.get_dataSet_access(sql_temp7, conn1);

        //table_col_num = ds_temp6.Tables[0].Columns.Count;
        //table_row_num = ds_temp6.Tables[0].Rows.Count;
        //GridView1.DataSource = func.Table_transport1(ds_temp1.Tables[0]);

        DropDownList4.DataSource = ds_temp6.Tables[0];

        DropDownList4.DataBind();
        DropDownList4.Items.Insert(0,"ALL");

            }
            else
            {
                DropDownList4.Items.Insert(0,"NA");
            }


            DropDownList5.DataSource = sArea;

            DropDownList5.DataBind();

            DropDownList5.Items.Insert(0, "ALL");


            show_data("TF", seqid[0]);//
        
        }
       

        //layer_row_data(sline[0], sArea[0], seqid[0],"Y");  // First Run function  Create mode source data
        //layer_row_data(sline[0], sArea[0], seqid[1], "Y");  // First Run function  Create mode source data
        //layer_row_data(sline[0], sArea[0], seqid[2], "Y");  // First Run function  Create mode source data
        //layer_row_data(sline[0], sArea[1], seqid[3], "Y");  // First Run function  Create mode source data
        //layer_row_data(sline[0], sArea[1], seqid[4], "Y");  // First Run function  Create mode source data
        //layer_row_data(sline[0], sArea[2], seqid[5], "Y");  // First Run function  Create mode source data

        //calcular_mode_data_position(3);
        //calcular_mode_data_95(0.95);




        //GridView1.HeaderRow.Visible = false;



    }




    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        Int32 rownum = table_row_num;
        Int32 colnum = table_col_num;

        Int32 counter = 1;

        //merge column  RowSpan
        for (int i = 0; i <= colnum - 1; i++)
        {

            counter = 1;
            for (int j = 1; j <= gv.Rows.Count - 1; j++)
            {

                if (GridView1.Rows[j].Cells[i].Text.Trim() == GridView1.Rows[(j - 1)].Cells[i].Text.Trim())
                {
                    counter++;
                    GridView1.Rows[j - counter + 1].Cells[i].RowSpan = counter;


                    GridView1.Rows[j].Cells[i].Visible = false;


                }

                else
                {
                    counter = 1;
                    GridView1.Rows[j].Cells[i].RowSpan = counter;

                }



            }








        }



        // merge row  ColumnSpan
        for (int i = 0; i <= gv.Rows.Count - 1; i++)
        {

            counter = 1;
            for (int j = 1; j <= colnum - 1; j++)
            {

                if (GridView1.Rows[i].Cells[j].Text.Trim() == GridView1.Rows[i].Cells[j - 1].Text.Trim())
                {
                    counter++;
                    GridView1.Rows[i].Cells[j - counter + 1].ColumnSpan = counter;


                    GridView1.Rows[i].Cells[j].Visible = false;


                }

                else
                {
                    counter = 1;
                    GridView1.Rows[i].Cells[j].ColumnSpan = counter;

                }



            }








        }


    }

    private void layer_row_data(string line, string area, string eq, string remove_pivot_flag)
    {

        sql_temp = @"
select ot3.line,ot3.moduale,substr(ot3.equipmentid,0,8) as equipmentid,ot3.product_type,ot3.layer,
   sum(  case when ot3.remove_pilot='Yes' then 1 else 0 end ) as pilot_flag

 from (

select ot1.*,ot2.moduale,ot2.product_type,ot2.layer,ot2.up_bound,ot2.low_bound,ot2.remove_pilot from (


select ot2.line,ot2.equipmentid,ot2.enddatetime,ot2.productid,ot2.lotid,ot2.glassid,ot2.stepid,ot2.endtacttime from empaglasshistory ot2
where ot2.equipmentid in 
(

select ot1.equipmentid from (
select t.line,t.equipmentid,t.area,substr(t.equipmentid,3,3) eq from equipment t
where t.line='{0}' and  t.area='{2}' and t.modulelevel='0' 
)ot1
where ot1.eq='{1}'
)
and ot2.ENDDATETIME>(select to_char(next_day(sysdate,1)-14,'yyyy-MM-dd')||' 07:00' from dual)
and ot2.ENDDATETIME<=(select to_char(next_day(sysdate,1)-7,'yyyy-MM-dd')||' 07:00' from dual)
and ot2.line='{0}'
order by ot2.line,ot2.equipmentid,ot2.ENDDATETIME

)ot1,
(
select tt.moduale,tt.eq,tt.product,tt.product_type,tt.step,tt.layer,tt.up_bound,tt.low_bound,tt.remove_pilot from pp_tactime_config tt

) ot2

where substr(ot1.equipmentid,0,5)=ot2.eq(+)
and  ot1.productid=ot2.product(+)
and ot1.stepid=ot2.step(+)


)ot3

{3}
group by ot3.line,ot3.moduale,substr(ot3.equipmentid,0,8),ot3.product_type,ot3.layer





";
        if (!eq.Equals("0AINC"))
        {
            sql_temp = string.Format(sql_temp, line, eq.Substring(2, 3), area, "where ot3.moduale is not null ");

        }
        else
        {

            sql_temp = string.Format(sql_temp, line, eq.Substring(2, 3), area, " ");
        }


        ds_temp = func.get_dataSet_access(sql_temp, conn1);



        GridView1.DataSource = ds_temp.Tables[0];
        GridView1.DataBind();
        // T0ARRAY  0A-TF  0APVD
        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++) // layer distinct data TF/PVD
        {
            if ((ds_temp.Tables[0].Rows[i]["moduale"].ToString().Equals("")) && (ds_temp.Tables[0].Rows[i]["product_type"].ToString().Equals("")) && (ds_temp.Tables[0].Rows[i]["layer"].ToString().Equals("")))
            {
                sql_temp1 = @"

           select ot4.* from 

(

select ot1.*,ot2.moduale,ot2.product_type,ot2.layer,ot2.up_bound,ot2.low_bound,ot2.remove_pilot from (


select ot2.line,ot2.equipmentid,ot2.enddatetime,ot2.productid,ot2.lotid,ot2.glassid,ot2.stepid,ot2.endtacttime from empaglasshistory ot2
where ot2.equipmentid in 
(

select ot1.equipmentid from (
select t.line,t.equipmentid,t.area,substr(t.equipmentid,3,3) eq from equipment t
where t.line='{4}' and  t.area='{5}' and t.modulelevel='0' 
)ot1
where ot1.eq=substr('{6}',3,6)
)
and ot2.ENDDATETIME>(select to_char(next_day(sysdate,1)-14,'yyyy-MM-dd')||' 07:00' from dual)
and ot2.ENDDATETIME<=(select to_char(next_day(sysdate,1)-7,'yyyy-MM-dd')||' 07:00' from dual)
and ot2.line='T0ARRAY'
order by ot2.line,ot2.equipmentid,ot2.ENDDATETIME

)ot1,
(
select tt.moduale,tt.eq,tt.product,tt.product_type,tt.step,tt.layer,tt.up_bound,tt.low_bound,tt.remove_pilot from pp_tactime_config tt

) ot2

where substr(ot1.equipmentid,0,5)=ot2.eq(+)
and  ot1.productid=ot2.product(+)
and ot1.stepid=ot2.step(+)


) ot4
where  ot4.moduale is null 
and ot4.equipmentid like '{1}%' 
and ot4.product_type is null
and ot4.layer is null


";

            }


            else
            {

                sql_temp1 = @"

           select ot4.* from 

(

select ot1.*,ot2.moduale,ot2.product_type,ot2.layer,ot2.up_bound,ot2.low_bound,ot2.remove_pilot from (


select ot2.line,ot2.equipmentid,ot2.enddatetime,ot2.productid,ot2.lotid,ot2.glassid,ot2.stepid,ot2.endtacttime from empaglasshistory ot2
where ot2.equipmentid in 
(

select ot1.equipmentid from (
select t.line,t.equipmentid,t.area,substr(t.equipmentid,3,3) eq from equipment t
where t.line='{4}' and  t.area='{5}' and t.modulelevel='0' 
)ot1
where ot1.eq=substr('{6}',3,6)
)
and ot2.ENDDATETIME>(select to_char(next_day(sysdate,1)-14,'yyyy-MM-dd')||' 07:00' from dual)
and ot2.ENDDATETIME<=(select to_char(next_day(sysdate,1)-7,'yyyy-MM-dd')||' 07:00' from dual)
and ot2.line='T0ARRAY'
order by ot2.line,ot2.equipmentid,ot2.ENDDATETIME

)ot1,
(
select tt.moduale,tt.eq,tt.product,tt.product_type,tt.step,tt.layer,tt.up_bound,tt.low_bound,tt.remove_pilot from pp_tactime_config tt

) ot2

where substr(ot1.equipmentid,0,5)=ot2.eq(+)
and  ot1.productid=ot2.product(+)
and ot1.stepid=ot2.step(+)


) ot4
where  ot4.moduale='{0}' 
and ot4.equipmentid like '{1}%' 
and ot4.product_type='{2}' 
and ot4.layer='{3}'


";

            }





            sql_temp1 = string.Format(sql_temp1, ds_temp.Tables[0].Rows[i]["moduale"].ToString(), ds_temp.Tables[0].Rows[i]["equipmentid"].ToString(), ds_temp.Tables[0].Rows[i]["product_type"].ToString(), ds_temp.Tables[0].Rows[i]["layer"].ToString(), line, area, eq);

            ds_temp2 = func.get_dataSet_access(sql_temp1, conn1);

            // 0AINC  fillup MODUAL=TF  PRODUCT_TYPE=INC LAYER='INC'  up_bound='-'  low_bound='-'  remove_pilot='Yes' 
            if ((ds_temp.Tables[0].Rows[i]["moduale"].ToString().Equals("")) && (ds_temp.Tables[0].Rows[i]["product_type"].ToString().Equals("")) && (ds_temp.Tables[0].Rows[i]["layer"].ToString().Equals("")))
            {

                for (int j = 0; j <= ds_temp2.Tables[0].Rows.Count - 1; j++)
                {

                    ds_temp2.Tables[0].Rows[j]["moduale"] = "TF";
                    ds_temp2.Tables[0].Rows[j]["product_type"] = "INC";
                    ds_temp2.Tables[0].Rows[j]["layer"] = "INC";
                    ds_temp2.Tables[0].Rows[j]["up_bound"] = "-";
                    ds_temp2.Tables[0].Rows[j]["low_bound"] = "-";
                    ds_temp2.Tables[0].Rows[j]["remove_pilot"] = "Yes";


                }

            }


            if (remove_pivot_flag.ToString().Equals("Y"))
            {

                remove_pivot(ds_temp2.Tables[0]);  // filter  first glass 

            }
            else
            {
                nonremove_pivot(ds_temp2.Tables[0]);// non filter first glass


            }

            //GridView1.DataSource = remove_pivot(ds_temp2.Tables[0]);
            //GridView1.DataBind();

            calcular_mode_data();


        }

    }
    private DataTable nonremove_pivot(DataTable dt) // Remove pivot glass data
    {

        #region remove_pivot
        // NonRemove first glass tacttime
        #endregion
        string temp_lotid = "";



        for (int j = 0; j <= dt.Rows.Count - 1; j++) //insert temp physical table
        {
            sql_temp3 = @"

   insert into pp_frequncy_table
  (line, equipmentid, enddatetime, productid, lotid, glassid, stepid, endtacttime, moduale, product_type, layer, up_bound, low_bound, remove_pilot,dttm)
values
  ('{0}', '{1}', to_date('{2}','yyyy-MM-dd HH24:mi:ss'), '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}', '{10}', {11}, {12}, '{13}',sysdate)

";

            sql_temp3 = string.Format(sql_temp3, dt.Rows[j]["line"].ToString(), dt.Rows[j]["equipmentid"].ToString(), dt.Rows[j]["enddatetime"].ToString(), dt.Rows[j]["productid"].ToString(), dt.Rows[j]["lotid"].ToString(), dt.Rows[j]["glassid"].ToString(), dt.Rows[j]["stepid"].ToString(), dt.Rows[j]["endtacttime"].ToString(), dt.Rows[j]["moduale"].ToString(), dt.Rows[j]["product_type"].ToString(), dt.Rows[j]["layer"].ToString(), dt.Rows[j]["up_bound"].ToString().Replace("-", "0"), dt.Rows[j]["low_bound"].ToString().Replace("-", "0"), dt.Rows[j]["remove_pilot"].ToString());
            func.get_sql_execute(sql_temp3, conn1);
        }

        return dt;


    }

    private DataTable remove_pivot(DataTable dt) // Remove pivot glass data
    {

        #region remove_pivot
        // Remove first glass tacttime
        #endregion
        string temp_lotid = "";

        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            DataRow dr = dt.Rows[i];

            if (i == 0)
            {
                temp_lotid = dt.Rows[i]["lotid"].ToString();

            }

            else
            {
                if (!dt.Rows[i]["lotid"].ToString().Equals(temp_lotid))
                {
                    temp_lotid = dt.Rows[i]["lotid"].ToString();
                    dt.Rows.Remove(dr);

                }

            }



        }

        for (int j = 0; j <= dt.Rows.Count - 1; j++) //insert temp physical table
        {
            sql_temp3 = @"

   insert into pp_frequncy_table
  (line, equipmentid, enddatetime, productid, lotid, glassid, stepid, endtacttime, moduale, product_type, layer, up_bound, low_bound, remove_pilot,dttm)
values
  ('{0}', '{1}', to_date('{2}','yyyy-MM-dd HH24:mi:ss'), '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}', '{10}', {11}, {12}, '{13}',sysdate)

";

            sql_temp3 = string.Format(sql_temp3, dt.Rows[j]["line"].ToString(), dt.Rows[j]["equipmentid"].ToString(), dt.Rows[j]["enddatetime"].ToString(), dt.Rows[j]["productid"].ToString(), dt.Rows[j]["lotid"].ToString(), dt.Rows[j]["glassid"].ToString(), dt.Rows[j]["stepid"].ToString(), dt.Rows[j]["endtacttime"].ToString(), dt.Rows[j]["moduale"].ToString(), dt.Rows[j]["product_type"].ToString(), dt.Rows[j]["layer"].ToString(), dt.Rows[j]["up_bound"].ToString().Replace("-", "0"), dt.Rows[j]["low_bound"].ToString().Replace("-", "0"), dt.Rows[j]["remove_pilot"].ToString());
            func.get_sql_execute(sql_temp3, conn1);
        }

        return dt;


    }


    private void calcular_mode_data()  //source table pp_frequncy_table  filter outlier data  by low_bound, up_bound
    {

        string sql_temp2 = @"

       select t1.*,t1.endtacttime1*t1.counter as counter2  from (

select t.moduale,substr(t.equipmentid,0,8)as equiptype,t.product_type,t.layer,t.endtacttime1,count(t.line) as counter from (

select line, equipmentid, enddatetime, productid, lotid, glassid, stepid, endtacttime,
case when t.endtacttime<t.low_bound and t.endtacttime>t.up_bound then 0
     else t.endtacttime end endtacttime1
, moduale, product_type, layer, up_bound, low_bound, remove_pilot, dttm from pp_frequncy_table t

) t
where t.endtacttime1>0
group by t.moduale,substr(t.equipmentid,0,8),t.product_type,t.layer,t.endtacttime1
order by t.moduale,substr(t.equipmentid,0,8),t.product_type,t.layer,count(t.line) desc,t.endtacttime1
) t1


";

        ds_temp2 = func.get_dataSet_access(sql_temp2, conn1);


        for (int i = 0; i <= ds_temp2.Tables[0].Rows.Count - 1; i++)
        {

            sql_temp3 = @"

insert into pp_mode_data
  (moduale, equiptype, product_type, layer, endtacttime1, counter, counter1, dttm)
values
  ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, sysdate)


";

            sql_temp3 = string.Format(sql_temp3, ds_temp2.Tables[0].Rows[i]["moduale"].ToString(), ds_temp2.Tables[0].Rows[i]["equiptype"].ToString(), ds_temp2.Tables[0].Rows[i]["product_type"].ToString(), ds_temp2.Tables[0].Rows[i]["layer"].ToString(), ds_temp2.Tables[0].Rows[i]["endtacttime1"].ToString(), ds_temp2.Tables[0].Rows[i]["counter"].ToString(), ds_temp2.Tables[0].Rows[i]["counter2"].ToString());


            func.get_sql_execute(sql_temp3, conn1);

        }


        sql_temp4 = @"

      truncate table  pp_frequncy_table


";

        func.get_sql_execute(sql_temp4, conn1);



        GridView1.DataSource = ds_temp2.Tables[0];

        GridView1.DataBind();

        sql_temp5 = @"  
    select t.* from pp_mode_data t

";
        dt_temp2 = func.get_dataSet_access(sql_temp5, conn1).Tables[0];


        DataTable dt_moduale = dt_temp2.DefaultView.ToTable("moduale", true);

        DataTable dt_equiptype = dt_temp2.DefaultView.ToTable("equiptype", true);

        DataTable dt_product_type = dt_temp2.DefaultView.ToTable("product_type", true);

        DataTable dt_layer = dt_temp2.DefaultView.ToTable("layer", true);
















    }


    private DataSet show_data(string moduale, string equiptype)
    {



        string sql_temp6 = @"

select * from (

select 
            t.product_type,
         t.moduale,
         case when t.equiptype like '0AEXP100' then '0AEXP-C' 
            when t.equiptype like '0AEXP200' then '0AEXP-C' 
            when t.equiptype like '0AEXP700' then '0AEXP-C'
            when t.equiptype like '0AEXP800' then '0AEXP-C' 
            when t.equiptype like '0AEXP900' then '0AEXP-C' 
            when t.equiptype like '0AEXPA00' then '0AEXP-C' 
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP400' then '0AEXP-N'
            when t.equiptype like '0AEXP500' then '0AEXP-N'
            when t.equiptype like '0AEXP600' then '0AEXP-N'
            when t.equiptype like '0AEXPB00' then '0AEXP-S'
            when t.equiptype like '0AEXPC00' then '0AEXP-S'
            else 
            substr(t.equiptype,0,5) end as equiptype,
            t.equiptype as equipid ,
         
            t.layer ,
            round(sum(t.counter1)/sum(t.counter) ,2) as tacttime,
            '0.95' as pct,
            round(sum(t.counter)/1,0) as total_cnt,
             round(sum(t.counter)/0.95,0) as total_origin_cnt
            
            from {5} t
            where 1=1
             and t.dttm>=to_date('{0}'||' 07:00','yyyy/MM/dd HH24:MI')
             and t.dttm<to_date('{1}'||' 07:00','yyyy/MM/dd HH24:MI')


group by t.product_type,
         t.moduale,
         case when t.equiptype like '0AEXP100' then '0AEXP-C' 
            when t.equiptype like '0AEXP200' then '0AEXP-C' 
            when t.equiptype like '0AEXP700' then '0AEXP-C'
            when t.equiptype like '0AEXP800' then '0AEXP-C' 
            when t.equiptype like '0AEXP900' then '0AEXP-C' 
            when t.equiptype like '0AEXPA00' then '0AEXP-C' 
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP400' then '0AEXP-N'
            when t.equiptype like '0AEXP500' then '0AEXP-N'
            when t.equiptype like '0AEXP600' then '0AEXP-N'
            when t.equiptype like '0AEXPB00' then '0AEXP-S'
            when t.equiptype like '0AEXPC00' then '0AEXP-S'
            else 
            substr(t.equiptype,0,5) end  ,
            t.equiptype,
         
            t.layer
             
) ot2

 where 1=1

     and  ot2.equiptype like '{2}%' {3}  {4}

order by     ot2.product_type,
            case when ot2.moduale='TF' then 1
              when ot2.moduale='PH' then 2
              when ot2.moduale='TF/PH' then 3
              when ot2.moduale='ET' then 4
              else 5 
              end,
          ot2.equiptype,
          ot2.equipid,
          case
            when ot2.layer = '高溫ITO' then
             1
            when ot2.layer = 'GMET' then
             2
            when ot2.layer = '常溫ITO' then
             3
            when ot2.layer = 'METITO' then
             4
            else
             5
          end


";

        string tmp_equip = "";
        string tmp_Product_type = "";
        string tmp_Productid = "";
        string tmp_moduale = "";
        if (DropDownList2.SelectedValue.Equals("T0 Array"))
        {

            tmp_equip = "0A";

        }
        else if (DropDownList2.SelectedValue.Equals("ALL"))
        {
            tmp_equip = "0A";

        }
        else
        {
            tmp_equip = "1A";

        
        }

        if (DropDownList3.SelectedValue.Equals("ALL"))
        {

            tmp_Product_type = "";

        }
        else
        {
            tmp_Product_type = "and ot2.product_type ='"+DropDownList3.SelectedValue+"'";

        }

        if (DropDownList4.SelectedValue.Equals("ALL"))
        {

            tmp_Productid = "";

        }
        else
        {
            tmp_Productid = "and ot2.productid ='" + DropDownList4.SelectedValue + "'";

        }


        if (DropDownList5.SelectedValue.Equals("ALL"))
        {

            tmp_moduale = "";

        }
        else
        {

            if (DropDownList5.SelectedValue.Equals("0A-TDTEST"))
            {
                tmp_moduale = "and ot2.moduale ='TEST'";
            
            }
            else
            {
                tmp_moduale = "and ot2.moduale ='" + DropDownList5.SelectedValue.Substring(3, 2) + "'";
            
            }
           
        }

        if (DropDownList1.Text.Equals("Product type"))
        {
            sql_temp6 = string.Format(sql_temp6, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyy/MM/dd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyy/MM/dd"), tmp_equip, tmp_Product_type, tmp_moduale, "pp_mode_data_cal_np");
        }
        else
        {

            sql_temp6 = string.Format(sql_temp6, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyy/MM/dd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyy/MM/dd"), tmp_equip, tmp_Product_type, tmp_moduale, "pp_mode_data_cal");
        }

        //Response.Write(sql_temp6);
        ds_temp1 = func.get_dataSet_access(sql_temp6, conn1);

        table_col_num = ds_temp1.Tables[0].Columns.Count;
        table_row_num = ds_temp1.Tables[0].Rows.Count;
        //GridView1.DataSource = func.Table_transport1(ds_temp1.Tables[0]);
        
        GridView1.DataSource = ds_temp1.Tables[0];

        GridView1.DataBind();



        sql_temp7 = @"

select * from (

select      t.productid,
            t.product_type,
         t.moduale,
         case when t.equiptype like '0AEXP100' then '0AEXP-C' 
            when t.equiptype like '0AEXP200' then '0AEXP-C' 
            when t.equiptype like '0AEXP700' then '0AEXP-C'
            when t.equiptype like '0AEXP800' then '0AEXP-C' 
            when t.equiptype like '0AEXP900' then '0AEXP-C' 
            when t.equiptype like '0AEXPA00' then '0AEXP-C' 
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP400' then '0AEXP-N'
            when t.equiptype like '0AEXP500' then '0AEXP-N'
            when t.equiptype like '0AEXP600' then '0AEXP-N'
            when t.equiptype like '0AEXPB00' then '0AEXP-S'
            when t.equiptype like '0AEXPC00' then '0AEXP-S'
            else 
            substr(t.equiptype,0,5) end as equiptype,
            t.equiptype as equipid ,
         
            t.layer ,
            round(sum(t.counter1)/sum(t.counter) ,2) as tacttime,
           '0.95' as pct,
            round(sum(t.counter)/1,0) as total_cnt,
             round(sum(t.counter)/0.95,0) as total_origin_cnt
           
            
            from pp_mode_data_cal t
             where 1=1
             and t.dttm>=to_date('{0}'||' 07:00','yyyy/MM/dd HH24:MI')
             and t.dttm<to_date('{1}'||' 07:00','yyyy/MM/dd HH24:MI')


group by  t.productid,
         t.product_type,
         t.moduale,
         case when t.equiptype like '0AEXP100' then '0AEXP-C' 
            when t.equiptype like '0AEXP200' then '0AEXP-C' 
            when t.equiptype like '0AEXP700' then '0AEXP-C'
            when t.equiptype like '0AEXP800' then '0AEXP-C' 
            when t.equiptype like '0AEXP900' then '0AEXP-C' 
            when t.equiptype like '0AEXPA00' then '0AEXP-C' 
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP400' then '0AEXP-N'
            when t.equiptype like '0AEXP500' then '0AEXP-N'
            when t.equiptype like '0AEXP600' then '0AEXP-N'
            when t.equiptype like '0AEXPB00' then '0AEXP-S'
            when t.equiptype like '0AEXPC00' then '0AEXP-S'
            else 
            substr(t.equiptype,0,5) end  ,
            t.equiptype,
         
            t.layer
             
) ot2
where ot2.equiptype like '{2}%'    {3} {4}  {5}
order by    ot2.productid, 
            ot2.product_type,
            case when ot2.moduale='TF' then 1
              when ot2.moduale='PH' then 2
              when ot2.moduale='TF/PH' then 3
              when ot2.moduale='ET' then 4
              else 5 
              end,
          ot2.equiptype,
          ot2.equipid,
          case
            when ot2.layer = '高溫ITO' then
             1
            when ot2.layer = 'GMET' then
             2
            when ot2.layer = '常溫ITO' then
             3
            when ot2.layer = 'METITO' then
             4
            else
             5
          end


";

        sql_temp7 = string.Format(sql_temp7, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyy/MM/dd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyy/MM/dd"), tmp_equip, tmp_Product_type, tmp_Productid, tmp_moduale);
        
       
        ds_temp6 = func.get_dataSet_access(sql_temp7, conn1);

        //table_col_num = ds_temp6.Tables[0].Columns.Count;
        //table_row_num = ds_temp6.Tables[0].Rows.Count;
        //GridView1.DataSource = func.Table_transport1(ds_temp1.Tables[0]);

        GridView2.DataSource = ds_temp6.Tables[0];

        GridView2.DataBind();

        if (DropDownList1.SelectedValue.Equals("Product type"))
        {
            return ds_temp1;
        }
        else

        {
            return ds_temp6;
        }
  

    }


    private void calcular_mode_data_95(Double pert)  //source table calcular_mode_data  3 position
    {

        func.write_log("PP Tacttime  calcular_mode_data_95 start ", Server.MapPath("..\\") + "\\LOG\\", "log");

        sql_temp5 = @"  
    select t.* from pp_mode_data t

";
        dt_temp2 = func.get_dataSet_access(sql_temp5, conn1).Tables[0];



        DataTable dt_moduale = dt_temp2.DefaultView.ToTable(true, "moduale");

        DataTable dt_equiptype = dt_temp2.DefaultView.ToTable(true, "equiptype");

        DataTable dt_product_type = dt_temp2.DefaultView.ToTable(true, "product_type");

        DataTable dt_layer = dt_temp2.DefaultView.ToTable(true, "layer");

        dt_temp3.Columns.Add("moduale", typeof(string));
        dt_temp3.Columns.Add("equiptype", typeof(string));
        dt_temp3.Columns.Add("layer", typeof(string));
        dt_temp3.Columns.Add("endtacttime1", typeof(string));
        dt_temp3.Columns.Add("counter", typeof(string));
        dt_temp3.Columns.Add("counter1", typeof(string));
        dt_temp3.Columns.Add("product_type", typeof(string));
        dt_temp3.Columns.Add("per", typeof(string));

        for (int i = 0; i <= dt_moduale.Rows.Count - 1; i++)
        {






            for (int j = 0; j <= dt_equiptype.Rows.Count - 1; j++)
            {

                for (int k = 0; k <= dt_product_type.Rows.Count - 1; k++)
                {


                    for (int m = 0; m <= dt_layer.Rows.Count - 1; m++)
                    {







                        sql_temp = @" 
select t.moduale,
       t.equiptype,
       t.product_type,
       t.layer,
       t.endtacttime1,
       t.counter,
       t.counter1,
       t.counter / (select sum(tt.counter)
                      from pp_mode_data tt
                     where tt.moduale = '{0}'
                       and tt.equiptype = '{1}'
                       and tt.product_type = '{2}'
                       and tt.layer = '{3}') as per

  from pp_mode_data t
 where t.moduale = '{0}'
   and t.equiptype = '{1}'
   and t.product_type = '{2}'
   and t.layer = '{3}'
   order by t.counter desc

";

                        sql_temp = string.Format(sql_temp, dt_moduale.Rows[i][0].ToString(), dt_equiptype.Rows[j][0].ToString(), dt_product_type.Rows[k][0].ToString(), dt_layer.Rows[m][0].ToString());




                        dt_temp2 = func.get_dataSet_access(sql_temp, conn1).Tables[0];

                        Double calculate95 = 0;




                        // colection satisfication 95%  Data
                        for (int q = 0; q <= dt_temp2.Rows.Count - 1; q++)
                        {






                            if (calculate95 <= pert)
                            {

                                DataRow dRow = dt_temp3.NewRow();

                                dRow["moduale"] = dt_temp2.Rows[q]["moduale"];
                                dRow["equiptype"] = dt_temp2.Rows[q]["equiptype"];
                                dRow["layer"] = dt_temp2.Rows[q]["layer"];
                                dRow["endtacttime1"] = dt_temp2.Rows[q]["endtacttime1"];
                                dRow["counter"] = dt_temp2.Rows[q]["counter"];
                                dRow["counter1"] = dt_temp2.Rows[q]["counter1"];
                                dRow["product_type"] = dt_temp2.Rows[q]["product_type"];
                                dRow["per"] = dt_temp2.Rows[q]["per"];



                                dt_temp3.Rows.Add(dRow);




                            }
                            else
                            {

                            }

                            calculate95 += Convert.ToDouble(dt_temp2.Rows[q]["per"].ToString());



                        }






                        for (int p = 0; p <= dt_temp3.Rows.Count - 1; p++)
                        {

                            sql_temp3 = @"

insert into pp_mode_data_cal
  (moduale, equiptype, product_type, layer, endtacttime1, counter, counter1, dttm)
values
  ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, sysdate)


";

                            sql_temp3 = string.Format(sql_temp3, dt_temp3.Rows[p]["moduale"].ToString(), dt_temp3.Rows[p]["equiptype"].ToString(), dt_temp3.Rows[p]["product_type"].ToString(), dt_temp3.Rows[p]["layer"].ToString(), dt_temp3.Rows[p]["endtacttime1"].ToString(), dt_temp3.Rows[p]["counter"].ToString(), dt_temp3.Rows[p]["counter1"].ToString());


                            func.get_sql_execute(sql_temp3, conn1);


                        }


                        dt_temp3.Clear();





                    }


                }


            }

        }


        func.write_log("PP Tacttime  calcular_mode_data_95 End ", Server.MapPath("..\\") + "\\LOG\\", "log");


    }


    private void calcular_mode_data_position(Int32 row_num)  //source table calcular_mode_data  3 position
    {



        sql_temp5 = @"  
    select t.* from pp_mode_data t

";
        dt_temp2 = func.get_dataSet_access(sql_temp5, conn1).Tables[0];


        DataTable dt_moduale = dt_temp2.DefaultView.ToTable(true, "moduale");

        DataTable dt_equiptype = dt_temp2.DefaultView.ToTable(true, "equiptype");

        DataTable dt_product_type = dt_temp2.DefaultView.ToTable(true, "product_type");

        DataTable dt_layer = dt_temp2.DefaultView.ToTable(true, "layer");


        for (int i = 0; i <= dt_moduale.Rows.Count - 1; i++)
        {

            for (int j = 0; j <= dt_equiptype.Rows.Count - 1; j++)
            {

                for (int k = 0; k <= dt_product_type.Rows.Count - 1; k++)
                {


                    for (int m = 0; m <= dt_layer.Rows.Count - 1; m++)
                    {


                        sql_temp = @" 
select t.* from pp_mode_data t
where t.moduale='{0}' and t.equiptype='{1}' and t.product_type='{2}' and t.layer='{3}'
and rownum<={4}
";

                        sql_temp = string.Format(sql_temp, dt_moduale.Rows[i][0].ToString(), dt_equiptype.Rows[j][0].ToString(), dt_product_type.Rows[k][0].ToString(), dt_layer.Rows[m][0].ToString(), row_num);

                        ds_temp3 = func.get_dataSet_access(sql_temp, conn1);

                        for (int p = 0; p <= ds_temp3.Tables[0].Rows.Count - 1; p++)
                        {

                            sql_temp3 = @"

insert into pp_mode_data_cal
  (moduale, equiptype, product_type, layer, endtacttime1, counter, counter1, dttm)
values
  ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6}, sysdate)


";

                            sql_temp3 = string.Format(sql_temp3, ds_temp3.Tables[0].Rows[p]["moduale"].ToString(), ds_temp3.Tables[0].Rows[p]["equiptype"].ToString(), ds_temp3.Tables[0].Rows[p]["product_type"].ToString(), ds_temp3.Tables[0].Rows[p]["layer"].ToString(), ds_temp3.Tables[0].Rows[p]["endtacttime1"].ToString(), ds_temp3.Tables[0].Rows[p]["counter"].ToString(), ds_temp3.Tables[0].Rows[p]["counter1"].ToString());


                            func.get_sql_execute(sql_temp3, conn1);


                        }


                        ds_temp3.Clear();





                    }


                }


            }

        }




    }

    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        show_data("TF", seqid[0]);//

        if (DropDownList1.SelectedValue.Equals("Product type"))
        {
            GridView1.Visible = true;
            GridView2.Visible = false;
        }
        else
        {

            GridView1.Visible = false;
            GridView2.Visible = true;
        }


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView gv = new GridView();

       

        gv.DataSource =  show_data("TF", seqid[0]).Tables[0];
        gv.DataBind();
        ExportExcel(gv);
      


    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue.Equals("Product type"))
        {
            string[] productid ={ "NA" };

            sql_temp7 = @"  select 'NA' as productid from dual ";

            ds_temp6 = func.get_dataSet_access(sql_temp7, conn1);

            DropDownList4.DataTextField="productid";
            DropDownList4.DataSource = ds_temp6.Tables[0];
            DropDownList4.DataBind();
            DropDownList4.Items.Insert(0, "ALL");
        }
        else
        {

            if (DropDownList1.SelectedValue.Equals("Product"))
            {
                string tmp_equip = "";
                string tmp_Product_type = "";
                if (DropDownList2.SelectedValue.Equals("T0 Array"))
                {

                    tmp_equip = "0A";

                }
                else if (DropDownList2.SelectedValue.Equals("ALL"))
                {
                    tmp_equip = "0A";

                }
                else
                {
                    tmp_equip = "1A";


                }

                if (DropDownList3.SelectedValue.Equals("ALL"))
                {

                    tmp_Product_type = "";

                }
                else
                {
                    tmp_Product_type = "and ot2.product_type ='" + DropDownList3.SelectedValue + "'";

                }


                sql_temp7 = @"

select distinct(ot2.productid) as productid  from (

select      t.productid,
            t.product_type,
         t.moduale,
         case when t.equiptype like '0AEXP100' then '0AEXP-C' 
            when t.equiptype like '0AEXP200' then '0AEXP-C' 
            when t.equiptype like '0AEXP700' then '0AEXP-C'
            when t.equiptype like '0AEXP800' then '0AEXP-C' 
            when t.equiptype like '0AEXP900' then '0AEXP-C' 
            when t.equiptype like '0AEXPA00' then '0AEXP-C' 
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP400' then '0AEXP-N'
            when t.equiptype like '0AEXP500' then '0AEXP-N'
            when t.equiptype like '0AEXP600' then '0AEXP-N'
            when t.equiptype like '0AEXPB00' then '0AEXP-S'
            when t.equiptype like '0AEXPC00' then '0AEXP-S'
            else 
            substr(t.equiptype,0,5) end as equiptype,
            t.equiptype as equipid ,
         
            t.layer ,
            round(sum(t.counter1)/sum(t.counter) ,2) as tacttime,
             round(sum(t.counter)/0.95,0) as total_cnt
            
            from pp_mode_data_cal t
             where 1=1
             and t.dttm>=to_date('{0}'||' 07:00','yyyy/MM/dd HH24:MI')
             and t.dttm<to_date('{1}'||' 07:00','yyyy/MM/dd HH24:MI')


group by  t.productid,
         t.product_type,
         t.moduale,
         case when t.equiptype like '0AEXP100' then '0AEXP-C' 
            when t.equiptype like '0AEXP200' then '0AEXP-C' 
            when t.equiptype like '0AEXP700' then '0AEXP-C'
            when t.equiptype like '0AEXP800' then '0AEXP-C' 
            when t.equiptype like '0AEXP900' then '0AEXP-C' 
            when t.equiptype like '0AEXPA00' then '0AEXP-C' 
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP400' then '0AEXP-N'
            when t.equiptype like '0AEXP500' then '0AEXP-N'
            when t.equiptype like '0AEXP600' then '0AEXP-N'
            when t.equiptype like '0AEXPB00' then '0AEXP-S'
            when t.equiptype like '0AEXPC00' then '0AEXP-S'
            else 
            substr(t.equiptype,0,5) end  ,
            t.equiptype,
         
            t.layer
             
) ot2
where ot2.equiptype like '{2}%'    {3}
 order by ot2.productid

";

                sql_temp7 = string.Format(sql_temp7, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyy/MM/dd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyy/MM/dd"), tmp_equip, tmp_Product_type);


                ds_temp6 = func.get_dataSet_access(sql_temp7, conn1);

                //table_col_num = ds_temp6.Tables[0].Columns.Count;
                //table_row_num = ds_temp6.Tables[0].Rows.Count;
                //GridView1.DataSource = func.Table_transport1(ds_temp1.Tables[0]);

                DropDownList4.DataTextField = "productid";
                DropDownList4.DataSource = ds_temp6.Tables[0];

                DropDownList4.DataBind();
                DropDownList4.Items.Insert(0, "ALL");

            }
        
        }
        
      


    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue.Equals("Product"))
        {


            string tmp_equip = "";
            string tmp_Product_type = "";
            if (DropDownList2.SelectedValue.Equals("T0 Array"))
            {

                tmp_equip = "0A";

            }
            else if (DropDownList2.SelectedValue.Equals("ALL"))
            {
                tmp_equip = "0A";

            }
            else
            {
                tmp_equip = "1A";


            }

            if (DropDownList3.SelectedValue.Equals("ALL"))
            {

                tmp_Product_type = "";

            }
            else
            {
                tmp_Product_type = "and ot2.product_type ='" + DropDownList3.SelectedValue + "'";

            }


            sql_temp7 = @"

select distinct(ot2.productid) as productid  from (

select      t.productid,
            t.product_type,
         t.moduale,
         case when t.equiptype like '0AEXP100' then '0AEXP-C' 
            when t.equiptype like '0AEXP200' then '0AEXP-C' 
            when t.equiptype like '0AEXP700' then '0AEXP-C'
            when t.equiptype like '0AEXP800' then '0AEXP-C' 
            when t.equiptype like '0AEXP900' then '0AEXP-C' 
            when t.equiptype like '0AEXPA00' then '0AEXP-C' 
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP400' then '0AEXP-N'
            when t.equiptype like '0AEXP500' then '0AEXP-N'
            when t.equiptype like '0AEXP600' then '0AEXP-N'
            when t.equiptype like '0AEXPB00' then '0AEXP-S'
            when t.equiptype like '0AEXPC00' then '0AEXP-S'
            else 
            substr(t.equiptype,0,5) end as equiptype,
            t.equiptype as equipid ,
         
            t.layer ,
            round(sum(t.counter1)/sum(t.counter) ,2) as tacttime,
             round(sum(t.counter)/0.95,0) as total_cnt
            
            from pp_mode_data_cal t
             where 1=1
             and t.dttm>=to_date('{0}'||' 07:00','yyyy/MM/dd HH24:MI')
             and t.dttm<to_date('{1}'||' 07:00','yyyy/MM/dd HH24:MI')


group by  t.productid,
         t.product_type,
         t.moduale,
         case when t.equiptype like '0AEXP100' then '0AEXP-C' 
            when t.equiptype like '0AEXP200' then '0AEXP-C' 
            when t.equiptype like '0AEXP700' then '0AEXP-C'
            when t.equiptype like '0AEXP800' then '0AEXP-C' 
            when t.equiptype like '0AEXP900' then '0AEXP-C' 
            when t.equiptype like '0AEXPA00' then '0AEXP-C' 
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP300' then '0AEXP-N'
            when t.equiptype like '0AEXP400' then '0AEXP-N'
            when t.equiptype like '0AEXP500' then '0AEXP-N'
            when t.equiptype like '0AEXP600' then '0AEXP-N'
            when t.equiptype like '0AEXPB00' then '0AEXP-S'
            when t.equiptype like '0AEXPC00' then '0AEXP-S'
            else 
            substr(t.equiptype,0,5) end  ,
            t.equiptype,
         
            t.layer
            order by  t.productid 
) ot2
where ot2.equiptype like '{2}%'    {3}
order by ot2.productid

";

            sql_temp7 = string.Format(sql_temp7, txtEstimateSTARTTIME.SelectedDate.Value.ToString("yyyy/MM/dd"), txtEstimateEndTime.SelectedDate.Value.ToString("yyyy/MM/dd"), tmp_equip, tmp_Product_type);


            ds_temp6 = func.get_dataSet_access(sql_temp7, conn1);

            //table_col_num = ds_temp6.Tables[0].Columns.Count;
            //table_row_num = ds_temp6.Tables[0].Rows.Count;
            //GridView1.DataSource = func.Table_transport1(ds_temp1.Tables[0]);

            DropDownList4.DataTextField = "productid";
            DropDownList4.DataSource = ds_temp6.Tables[0];

            DropDownList4.DataBind();
            DropDownList4.Items.Insert(0, "ALL");
        
        }
    }

    private void ExportExcel(GridView SeriesValuesDataGrid)
    {

        string filename = "";
        string today_detail_char = DateTime.Now.AddDays(+0).ToString("yyyy/MM/ddHHmmss").Replace("/", "");
        filename = "pp_tacttime_report_query_" + today_detail_char + ".xls";
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
