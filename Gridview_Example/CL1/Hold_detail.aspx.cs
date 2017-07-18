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

public partial class CL1_Hold_detail : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CL1_MES"];
    string sql_temp = "";
    string sql_temp1 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    ArrayList arlist_temp1 = new ArrayList();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtEstimateStartDate.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd"));
            txtEstimateEndDate.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-0).ToString("yyyy/MM/dd"));

            #region shop

            arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\shop.txt");

            DropDownList1.DataSource = arlist_temp1;

            DropDownList1.DataBind();

            #endregion

            #region production_type

            string production_type = "select distinct(t.productiontype) as productiontype  from lot t";
            ds_temp1 = func.get_dataSet_access(production_type,conn);

            ListBox1.DataTextField = "productiontype";
            ListBox1.DataValueField = "productiontype";
            ListBox1.DataSource = ds_temp1.Tables[0];
            ListBox1.DataBind();


            #endregion

            //#region scrap

            //arlist_temp1 = func.FileToArray(Server.MapPath(".") + "\\config\\scrap_type.txt");

            //DropDownList2.DataSource = arlist_temp1;

            //DropDownList2.DataBind();
            ListBox2.Items.Add("請選擇");
            ListBox4.Items.Add("請選擇");

            //#endregion

           bind_data2();


        }

    }
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        bind_data();
    }
    protected void btnExport_Click1(object sender, EventArgs e)
    {
        GridView gv = new GridView();
        gv.DataSource = bind_data();
        gv.DataBind();
        ExportExcel(gv);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
    }

    protected DataSet bind_data()
    {
        string sql =" select to_char(t.lasteventtime - 7 / 24, 'yyyy/MM/dd') as shift_day_key,  "+
"        t.lasteventtime,                                                   "+
"        t.factoryname,                                                     "+
"        t.productspecname,                                                 "+
"        t.processflowname,                                                 "+
"        t2.producttype,                                                    "+
"        t.lotname,                                                         "+
"        t2.productname,                                                    "+
"        t2.processoperationname,                                           "+
"        t.lasteventname,                                                   "+
"        t.productiontype,                                                  "+
"        t.carriername,                                                     "+
"        t.lasteventuser,                                                   "+
"        t2.subproductquantity1,                                            "+
"        round((sysdate-t.lasteventtime)*24,2) as hold_hrs,                 "+
"        t.reasoncode                                                       "+
"   from lot t, product t2                                                  "+
"  where t.lasteventname = 'HOLD'                                           ";

        sql += "    and to_char(t.lasteventtime - 7 / 24, 'yyyy/MM/dd') >=                 ";
        sql += "        to_char(to_date('" + txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd") + "', 'yyyy/MM/dd') , 'yyyy/MM/dd')        ";


        sql += "    and to_char(t.lasteventtime - 7 / 24, 'yyyy/MM/dd') <                 ";
       sql += "        to_char(to_date('" + txtEstimateEndDate.SelectedDate.Value.ToString("yyyy/MM/dd") + "', 'yyyy/MM/dd') , 'yyyy/MM/dd')        ";



if(!DropDownList1.SelectedValue.Equals(""))
{
    sql+="        and t.factoryname='"+DropDownList1.SelectedValue+"'                                            ";

}

        if(!TextBox1.Text.Equals(""))
{
    sql+="        and t.hold_hrs>='"+TextBox1.Text+"'                                            ";

}

if (!ListBox2.Items[0].Text.Equals("請選擇"))
        {
            sql = sql + "and t.productiontype in (" + combine_List_box(ListBox2) + ")";
        }        
        if(!ListBox4.Items[0].Text.Equals("請選擇"))
        {
           sql = sql + "and t.productspecname in (" + combine_List_box(ListBox4) + ")";
        
        }

                                                                                                                                                                          
        

      

        sql += " order by t.lasteventname desc ";

        sql = "select rownum,t.* from (" + sql + ")t  ";

        ds_temp = func.get_dataSet_access(sql, conn);

        GridView1.DataSource = ds_temp;
        GridView1.DataBind();



        return ds_temp;




    }


    protected DataSet bind_data2()
    {
 string sql =" select to_char(t.lasteventtime - 7 / 24, 'yyyy/MM/dd') as shift_day_key,  "+
"        t.lasteventtime,                                                   "+
"        t.factoryname,                                                     "+
"        t.productspecname,                                                 "+
"        t.processflowname,                                                 "+
"        t2.producttype,                                                    "+
"        t.lotname,                                                         "+
"        t2.productname,                                                    "+
"        t2.processoperationname,                                           "+
"        t.lasteventname,                                                   "+
"        t.productiontype,                                                  "+
"        t.carriername,                                                     "+
"        t.lasteventuser,                                                   "+
"        t2.subproductquantity1,                                            "+
"        round((sysdate-t.lasteventtime)*24,2) as hold_hrs,                 "+
"        t.reasoncode                                                       "+
"   from lot t, product t2                                                  "+
"  where t.lasteventname = 'HOLD'                                           ";

        sql += "    and to_char(t.lasteventtime - 7 / 24, 'yyyy/MM/dd') >=                 ";
        sql += "        to_char(to_date('" + txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd") + "', 'yyyy/MM/dd') , 'yyyy/MM/dd')        ";


        sql += "    and to_char(t.lasteventtime - 7 / 24, 'yyyy/MM/dd') <                 ";
       sql += "        to_char(to_date('" + txtEstimateEndDate.SelectedDate.Value.ToString("yyyy/MM/dd") + "', 'yyyy/MM/dd') , 'yyyy/MM/dd')        ";



      sql += " order by t.lasteventname desc ";

        sql = "select rownum,t.* from (" + sql + ")t  ";

        ds_temp = func.get_dataSet_access(sql, conn);

        GridView1.DataSource = ds_temp;
        GridView1.DataBind();



        return ds_temp;




    }

    protected void Button11_Click(object sender, EventArgs e)//add select
    {
        ListBox2.Items.Clear();
        if (ListBox1.SelectedItem != null)
        {
            foreach (ListItem item in ListBox1.Items)
            {
                if (item.Selected == true)
                {
                    ListBox2.Items.Add(item);
                }
            }

            foreach (ListItem item in ListBox2.Items)
            {
                if (item.Selected == true)
                {
                    ListBox1.Items.Remove(item);
                }
            }
        }
        ListBox2_OnTextChanged();
    }
    protected void Button12_Click(object sender, EventArgs e) //add select all
    {
        ListBox2.Items.Clear();
        foreach (ListItem item in ListBox1.Items)
        {
            ListBox2.Items.Add(item);
        }
        ListBox1.Items.Clear();
        ListBox2_OnTextChanged();
    }


    protected void Button13_Click(object sender, EventArgs e) //remove select
    {
        if (ListBox2.SelectedItem != null)
        {
            foreach (ListItem item in ListBox2.Items)
            {
                if (item.Selected == true)
                {
                    ListBox1.Items.Add(item);
                }
            }

            foreach (ListItem item in ListBox1.Items)
            {
                if (item.Selected == true)
                {
                    ListBox2.Items.Remove(item);
                }
            }
        }

        ListBox2_OnTextChanged();
    }


    protected void Button14_Click(object sender, EventArgs e)//remove select all
    {
        foreach (ListItem item in ListBox2.Items)
        {
            ListBox1.Items.Add(item);
        }
        ListBox2.Items.Clear();
        ListBox3.Items.Clear();
        ListBox4.Items.Clear();


    }




    protected void Button21_Click(object sender, EventArgs e)
    {
        ListBox4.Items.Clear();
        if (ListBox3.SelectedItem != null)   //左邊 ListBox 有選Item ,加到 右邊 ListBox
        {
            foreach (ListItem item in ListBox3.Items)
            {
                if (item.Selected == true)
                {
                    ListBox4.Items.Add(item);
                }
            }

            foreach (ListItem item in ListBox4.Items)//右邊 ListBox 有的Item,左邊 ListBox 將Item 移除
            {
                if (item.Selected == true)
                {
                    ListBox3.Items.Remove(item);
                }
            }
        }

    }



    protected void Button22_Click(object sender, EventArgs e) //將左邊 ListBox 中的Item ,加到右邊 ListBox
    {
        ListBox4.Items.Clear();
        foreach (ListItem item in ListBox3.Items)
        {
            ListBox4.Items.Add(item);
        }
        ListBox3.Items.Clear();  //將左邊 ListBox中的Item,移除


    }
    protected void Button23_Click(object sender, EventArgs e)
    {
        if (ListBox4.SelectedItem != null)  //右邊ListBox 選擇非空  加入左邊ListBox
        {
            foreach (ListItem item in ListBox4.Items)
            {
                if (item.Selected == true)
                {
                    ListBox3.Items.Add(item);//加入左邊ListBox
                }
            }

            foreach (ListItem item in ListBox3.Items)//左邊ListBox 中出現的 Item,u.右邊Item 中 Remove
            {
                if (item.Selected == true)
                {
                    ListBox4.Items.Remove(item);
                }
            }
        }


    }



    protected void Button24_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in ListBox4.Items) //右邊ListBox 有的Item 加入在左邊的 ListBox 
        {
            ListBox3.Items.Add(item);
        }
        ListBox4.Items.Clear();

        //將右邊ListBox 中的Item Remove


    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }

            //ImageButton btnDel = new ImageButton();
            //btnDel = (ImageButton)e.Row.FindControl("btnDel");
            ////btnDel.CommandArgument = ((Label)e.Row.FindControl("lblDefectType")).Text;
            //btnDel.Attributes["onclick"] = "javascript:return confirm('確認刪除否? 【MODEL_NAME】:" + ((DataRowView)e.Row.DataItem)["MODEL_NAME"] + " 【TOOL_ID】:" + ((DataRowView)e.Row.DataItem)["TOOL_ID"] + "【SN】:" + ((DataRowView)e.Row.DataItem)["SN"] + "');";




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

        }
    }
    
    protected void ListBox2_OnTextChanged()
    {


        string sql_productiontype = combine_List_box(ListBox2);

        #region sql_area
		 sql_temp1=" select  distinct(ot1.productspecname) as productspecname                   "+
"                                                                            "+
" from (                                                                     "+
"                                                                            "+
" select to_char(t.lasteventtime - 7 / 24, 'yyyy/MM/dd') as shift_day_key,   "+
"        t.lasteventtime,                                                    "+
"        t.factoryname,                                                      "+
"        t.productspecname,                                                  "+
"        t.processflowname,                                                  "+
"        t2.producttype,                                                     "+
"        t.lotname,                                                          "+
"        t2.productname,                                                     "+
"        t2.processoperationname,                                            "+
"        t.lasteventname,                                                    "+
"        t.productiontype,                                                   "+
"        t.carriername,                                                      "+
"        t.lasteventuser,                                                    "+
"        t2.subproductquantity1,                                             "+
"        round((sysdate-t.lasteventtime)*24,2) as hold_hrs,                  "+
"        t.reasoncode                                                        "+
"   from lot t, product t2                                                   "+
"  where t.lasteventname = 'HOLD'                                            "+
"    and to_char(t.lasteventtime - 7 / 24, 'yyyy/MM/dd') >=                  "+
"        to_char(to_date('"+txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd")+"', 'yyyy/MM/dd') , 'yyyy/MM/dd')         "+
"        and to_char(t.lasteventtime - 7 / 24, 'yyyy/MM/dd') <               "+
"        to_char(to_date('"+txtEstimateEndDate.SelectedDate.Value.ToString("yyyy/MM/dd")+"', 'yyyy/MM/dd') , 'yyyy/MM/dd')         "+
"        and t.factoryname='"+DropDownList1.SelectedValue+"'                                             "+
"        and round((sysdate-t.lasteventtime)*24,2)>100                       "+
"        and  t.productiontype in ("+sql_productiontype+")                                         "+
"      order by t.lasteventname desc                                         "+
"                                                                            "+
" )ot1                                                                       ";
	#endregion
        
        //string sql = "";
        //sql="select distinct(t.productname) from ("++") t";

        ListBox3.Items.Clear();
        

        DataSet ds_process = new DataSet();
        ds_process = func.get_dataSet_access(sql_temp1, conn);

        ListBox3.DataTextField = "productspecname";
        ListBox3.DataValueField = "productspecname";
        ListBox3.DataSource = ds_process.Tables[0];
        ListBox3.DataBind();



        //ListBox1.DataTextField = "productiontype";
        //ListBox1.DataValueField = "productiontype";
        //ListBox1.DataSource = ds_temp1.Tables[0];
        //ListBox1.DataBind();




    }



    private static string combine_List_box(ListBox Source_list)
    {

        ListBox Target = new ListBox();
        Target = Source_list;
        string initial = "";

        if (Target.SelectedItem is Nullable)
        {

            initial = "'" + "'";
        }

        else
        {

            //string initial = "";
            for (int i = 0; i <= Target.Items.Count - 1; i++)
            {
                if (i == 0)
                {
                    initial = initial + "'" + Target.Items[i] + "'";
                }

                else
                {

                    initial = initial + ",'" + Target.Items[i] + "'";
                }


            }


        }
        if (initial == "")
        {
            initial = "''";
        }
        return initial;


    }
    private void ExportExcel(GridView SeriesValuesDataGrid)
    {
        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader("content-disposition", "attachment;filename=Night_Inspec.xls");

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
 //   protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
 //   {
 //       sql_temp = " select distinct (op1.productiontype) as productiontype  " +
 //"   from (select t2.factoryname,                          " +
 //"                t2.productrequestname,                   " +
 //"                t.productspecname,                       " +
 //"                t.processflowname,                       " +
 //"                t2.producttype,                          " +
 //"                t.lotname,                               " +
 //"                t2.productname,                          " +
 //"                t2.processoperationname,                 " +
 //"                t.eventname,                             " +
 //"                t.eventtime,                             " +
 //"                t.productiontype,                        " +
 //"                t.carriername,                           " +
 //"                t.eventcomment,                          " +
 //"                t.eventuser,                             " +
 //"                t2.subproductquantity1                   " +
 //"           from lothistory t, product t2                 " +
 //"          where t.lotname in                             " +
 //"                (select t3.lotname                       " +
 //"                   from cl_scrap t3                      " +
 //"                  where t3.shift_date >= '" + txtEstimateStartDate.SelectedDate.Value.ToString("yyyy/MM/dd").Replace("/", "") + "'      " +
 //"                    and t3.shift_date < '" + txtEstimateEndDate.SelectedDate.Value.ToString("yyyy/MM/dd").Replace("/", "") + "')      " +
 //"            and t.eventname in ('SCRAP', 'UNSCRAP')      " +
 //"            and t.timekey = t2.lasteventtimekey          ";
 //       if (!DropDownList1.SelectedValue.Equals("請選擇"))
 //       {
 //           sql_temp += "            and t2.factoryname = '" + DropDownList1.SelectedValue + "'                   ";

 //       }

 //       if (!DropDownList2.SelectedValue.Equals("請選擇"))
 //       {

 //           sql_temp += "            and t.eventname = '" + DropDownList2.SelectedValue + "') op1               ";
 //       }


 //       ds_temp = func.get_dataSet_access(sql_temp, conn);
 //       ListBox1.DataSource = ds_temp.Tables[0];
 //       ListBox1.DataTextField = "productiontype";
 //       ListBox1.DataValueField = "productiontype";

 //       ListBox1.DataBind();
 //   }
}
