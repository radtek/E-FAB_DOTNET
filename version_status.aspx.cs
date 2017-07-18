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

public partial class version_status : System.Web.UI.Page
{
    DataSet ds_commmon = new DataSet();
    string connY = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_Meeting"];


    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {

            txtEstimateStartDate.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-365).ToString("yyyy/MM/dd"));
            txtEstimateEndDate.SelectedDate = DateTime.Now.AddDays(-0);

            

             string sql_fab = "                                                                                                          " +
"  select distinct(trim(t7.fab)) as fab from (                                                             " +
"                                                                                                          " +
"  select t5.*, (t5.A+t5.B+t5.C+t5.D+t5.other)total from (                                                 " +
"  select t4.fab,t4.process,t4.layer,sum(t4.A) A,sum(t4.B)B,sum(t4.C)C,sum(t4.D)D,sum(t4.other)other       " +
"   from (                                                                                                 " +
"  select t2.fab,                                                                                          " +
"         t2.process,                                                                                      " +
"         t2.layer,                                                                                        " +
"         case                                                                                             " +
"           when t2.tool_version = 'A' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end A,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'B' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end B,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'C' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end C,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'D' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end D,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version not in('A','B','C','D')  then                                             " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end other                                                                                        " +
"    from (                                                                                                " +
"                                                                                                          " +
"          select t.fab,                                                                                   " +
"                  t.process,                                                                              " +
"                  t.layer,                                                                                " +
"                  t.tool_version,                                                                         " +
"                  count(t.fab) count_num                                                                  " +
"            from tlms_main t                                                                              " +
"           where 1 = 1 and t.tapeout_date>='" + txtEstimateStartDate.SelectedDate.ToString() + "' and t.tapeout_date<'" + txtEstimateEndDate.SelectedDate.ToString() + "'          " +
"           group by t.fab, t.process, t.layer, t.tool_version                                             " +
"                                                                                                          " +
"          ) t2                                                                                            " +
"                                                                                                          " +
"  ) t4                                                                                                    " +
"                                                                                                          " +
"  group by t4.fab,t4.process,t4.layer                                                                     " +
"                                                                                                          " +
"  )t5                                                                                                     " +
"  union all                                                                                               " +
"                                                                                                          " +
"  select '','Total','',sum(t6.A),sum(t6.B),sum(t6.C),sum(t6.D),sum(t6.other),sum(t6.total) from (         " +
"                                                                                                          " +
"                                                                                                          " +
"                                                                                                          " +
"  select t5.*, (t5.A+t5.B+t5.C+t5.D+t5.other)total from (                                                 " +
"  select t4.fab,t4.process,t4.layer,sum(t4.A) A,sum(t4.B)B,sum(t4.C)C,sum(t4.D)D,sum(t4.other)other       " +
"   from (                                                                                                 " +
"  select t2.fab,                                                                                          " +
"         t2.process,                                                                                      " +
"         t2.layer,                                                                                        " +
"         case                                                                                             " +
"           when t2.tool_version = 'A' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end A,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'B' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end B,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'C' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end C,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'D' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end D,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version not in('A','B','C','D')  then                                             " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end other                                                                                        " +
"    from (                                                                                                " +
"                                                                                                          " +
"          select t.fab,                                                                                   " +
"                  t.process,                                                                              " +
"                  t.layer,                                                                                " +
"                  t.tool_version,                                                                         " +
"                  count(t.fab) count_num                                                                  " +
"            from tlms_main t                                                                              " +
"           where 1 = 1 and t.tapeout_date>='" + txtEstimateStartDate.SelectedDate.ToString() + "' and t.tapeout_date<'" + txtEstimateEndDate.SelectedDate.ToString() + "'                  " +
"            group by t.fab, t.process, t.layer, t.tool_version                                             " +
"                                                                                                          " +
"          ) t2                                                                                            " +
"                                                                                                          " +
"  ) t4                                                                                                    " +
"                                                                                                          " +
"  group by t4.fab,t4.process,t4.layer                                                                     " +
"                                                                                                          " +
"  )t5)t6                                                                                                  " +
"                                                                                                          " +
"                                                                                                          " +
"  ) t7                                                                                                    " +
"                                                                                                          ";
            ds_commmon=func.get_dataSet_access(sql_fab,connY);

            DropDownList1.DataTextField = "fab";
            DropDownList1.DataValueField = "fab";
            DropDownList1.DataSource = ds_commmon.Tables[0];
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "請選擇");
            ds_commmon.Clear();


        }
      

       

    }

    protected void DropDownList1_OnTextChanged()
    {




        string sql_process = "                                                                                                          " +
"  select distinct(trim(t7.process)) as process from (                                                             " +
"                                                                                                          " +
"  select t5.*, (t5.A+t5.B+t5.C+t5.D+t5.other)total from (                                                 " +
"  select t4.fab,t4.process,t4.layer,sum(t4.A) A,sum(t4.B)B,sum(t4.C)C,sum(t4.D)D,sum(t4.other)other       " +
"   from (                                                                                                 " +
"  select t2.fab,                                                                                          " +
"         t2.process,                                                                                      " +
"         t2.layer,                                                                                        " +
"         case                                                                                             " +
"           when t2.tool_version = 'A' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end A,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'B' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end B,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'C' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end C,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'D' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end D,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version not in('A','B','C','D')  then                                             " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end other                                                                                        " +
"    from (                                                                                                " +
"                                                                                                          " +
"          select t.fab,                                                                                   " +
"                  t.process,                                                                              " +
"                  t.layer,                                                                                " +
"                  t.tool_version,                                                                         " +
"                  count(t.fab) count_num                                                                  " +
"            from tlms_main t                                                                              " +
"           where 1 = 1 and t.tapeout_date>='" + txtEstimateStartDate.SelectedDate.ToString() + "' and t.tapeout_date<'" + txtEstimateEndDate.SelectedDate.ToString() + "'                   " +
"           group by t.fab, t.process, t.layer, t.tool_version                                             " +
"                                                                                                          " +
"          ) t2                                                                                            " +
"                                                                                                          " +
"  ) t4                                                                                                    " +
"                                                                                                          " +
"  group by t4.fab,t4.process,t4.layer                                                                     " +
"                                                                                                          " +
"  )t5                                                                                                     " +
"  union all                                                                                               " +
"                                                                                                          " +
"  select '','Total','',sum(t6.A),sum(t6.B),sum(t6.C),sum(t6.D),sum(t6.other),sum(t6.total) from (         " +
"                                                                                                          " +
"                                                                                                          " +
"                                                                                                          " +
"  select t5.*, (t5.A+t5.B+t5.C+t5.D+t5.other)total from (                                                 " +
"  select t4.fab,t4.process,t4.layer,sum(t4.A) A,sum(t4.B)B,sum(t4.C)C,sum(t4.D)D,sum(t4.other)other       " +
"   from (                                                                                                 " +
"  select t2.fab,                                                                                          " +
"         t2.process,                                                                                      " +
"         t2.layer,                                                                                        " +
"         case                                                                                             " +
"           when t2.tool_version = 'A' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end A,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'B' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end B,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'C' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end C,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'D' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end D,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version not in('A','B','C','D')  then                                             " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end other                                                                                        " +
"    from (                                                                                                " +
"                                                                                                          " +
"          select t.fab,                                                                                   " +
"                  t.process,                                                                              " +
"                  t.layer,                                                                                " +
"                  t.tool_version,                                                                         " +
"                  count(t.fab) count_num                                                                  " +
"            from tlms_main t                                                                              " +
"           where 1 = 1 and t.tapeout_date>='" + txtEstimateStartDate.SelectedDate.ToString() + "'  and t.tapeout_date<'" + txtEstimateEndDate.SelectedDate.ToString() + "'                 " +
"           and t.fab='" + DropDownList1.SelectedValue + "'   group by t.fab, t.process, t.layer, t.tool_version                                             " +
"                                                                                                          " +
"          ) t2                                                                                            " +
"                                                                                                          " +
"  ) t4                                                                                                    " +
"                                                                                                          " +
"  group by t4.fab,t4.process,t4.layer                                                                     " +
"                                                                                                          " +
"  )t5)t6                                                                                                  " +
"                                                                                                          " +
"                                                                                                          " +
"  ) t7  where t7.process<>'Total'                                                                                                  " +
"                                                                                                          ";


        DataSet ds_process = new DataSet();
        ds_process = func.get_dataSet_access(sql_process, connY);

        DropDownList2.DataTextField = "process";
        DropDownList2.DataValueField = "process";
        DropDownList2.DataSource = ds_process.Tables[0];
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "請選擇");


    }

    protected void DropDownList2_OnTextChanged()
    {




        string sql_layer = "                                                                                                          " +
"  select distinct(trim(t7.layer)) as layer from (                                                             " +
"                                                                                                          " +
"  select t5.*, (t5.A+t5.B+t5.C+t5.D+t5.other)total from (                                                 " +
"  select t4.fab,t4.process,t4.layer,sum(t4.A) A,sum(t4.B)B,sum(t4.C)C,sum(t4.D)D,sum(t4.other)other       " +
"   from (                                                                                                 " +
"  select t2.fab,                                                                                          " +
"         t2.process,                                                                                      " +
"         t2.layer,                                                                                        " +
"         case                                                                                             " +
"           when t2.tool_version = 'A' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end A,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'B' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end B,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'C' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end C,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'D' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end D,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version not in('A','B','C','D')  then                                             " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end other                                                                                        " +
"    from (                                                                                                " +
"                                                                                                          " +
"          select t.fab,                                                                                   " +
"                  t.process,                                                                              " +
"                  t.layer,                                                                                " +
"                  t.tool_version,                                                                         " +
"                  count(t.fab) count_num                                                                  " +
"            from tlms_main t                                                                              " +
"           where 1 = 1 and t.tapeout_date>='" + txtEstimateStartDate.SelectedDate.ToString() + "' and t.tapeout_date<'" + txtEstimateEndDate.SelectedDate.ToString() + "'                   " +
"          and t.fab='" + DropDownList1.SelectedValue + "' and t.process='" + DropDownList2.SelectedValue + "' group by t.fab, t.process, t.layer, t.tool_version                                             " +
"                                                                                                          " +
"          ) t2                                                                                            " +
"                                                                                                          " +
"  ) t4                                                                                                    " +
"                                                                                                          " +
"  group by t4.fab,t4.process,t4.layer                                                                     " +
"                                                                                                          " +
"  )t5                                                                                                     " +
"  union all                                                                                               " +
"                                                                                                          " +
"  select '','Total','',sum(t6.A),sum(t6.B),sum(t6.C),sum(t6.D),sum(t6.other),sum(t6.total) from (         " +
"                                                                                                          " +
"                                                                                                          " +
"                                                                                                          " +
"  select t5.*, (t5.A+t5.B+t5.C+t5.D+t5.other)total from (                                                 " +
"  select t4.fab,t4.process,t4.layer,sum(t4.A) A,sum(t4.B)B,sum(t4.C)C,sum(t4.D)D,sum(t4.other)other       " +
"   from (                                                                                                 " +
"  select t2.fab,                                                                                          " +
"         t2.process,                                                                                      " +
"         t2.layer,                                                                                        " +
"         case                                                                                             " +
"           when t2.tool_version = 'A' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end A,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'B' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end B,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'C' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end C,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version = 'D' then                                                                " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end D,                                                                                           " +
"          case                                                                                            " +
"           when t2.tool_version not in('A','B','C','D')  then                                             " +
"            t2.count_num                                                                                  " +
"           else                                                                                           " +
"            0                                                                                             " +
"         end other                                                                                        " +
"    from (                                                                                                " +
"                                                                                                          " +
"          select t.fab,                                                                                   " +
"                  t.process,                                                                              " +
"                  t.layer,                                                                                " +
"                  t.tool_version,                                                                         " +
"                  count(t.fab) count_num                                                                  " +
"            from tlms_main t                                                                              " +
"           where 1 = 1 and t.tapeout_date>='" + txtEstimateStartDate.SelectedDate.ToString() + "' and t.tapeout_date<'" + txtEstimateEndDate.SelectedDate.ToString() + "'                   " +
"          and t.fab='" + DropDownList1.SelectedValue + "' and t.process='" + DropDownList2.SelectedValue + "' group by t.fab, t.process, t.layer, t.tool_version                                             " +
"                                                                                                          " +
"          ) t2                                                                                            " +
"                                                                                                          " +
"  ) t4                                                                                                    " +
"                                                                                                          " +
"  group by t4.fab,t4.process,t4.layer                                                                     " +
"                                                                                                          " +
"  )t5)t6                                                                                                  " +
"                                                                                                          " +
"                                                                                                          " +
"  ) t7                                                                                                    " +
"                                                                                                          ";


        DataSet ds_layer = new DataSet();
        ds_layer = func.get_dataSet_access(sql_layer, connY);

        DropDownList3.DataTextField = "layer";
        DropDownList3.DataValueField = "layer";
        DropDownList3.DataSource = ds_layer.Tables[0];
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, "請選擇");


    }

    public DataTable Bind_data()
    {




        string sql = " select t5.*, (t5.A + t5.B + t5.C + t5.D + t5.other) total                           " +
"   from (select t4.fab,                                                              " +
"                t4.process,                                                          " +
"                t4.layer,                                                            " +
"                sum(t4.A) A,                                                         " +
"                sum(t4.B) B,                                                         " +
"                sum(t4.C) C,                                                         " +
"                sum(t4.D) D,                                                         " +
"                sum(t4.other) other                                                  " +
"           from (select t2.fab,                                                      " +
"                        t2.process,                                                  " +
"                        t2.layer,                                                    " +
"                        case                                                         " +
"                          when t2.tool_version = 'A' then                            " +
"                           t2.count_num                                              " +
"                          else                                                       " +
"                           0                                                         " +
"                        end A,                                                       " +
"                        case                                                         " +
"                          when t2.tool_version = 'B' then                            " +
"                           t2.count_num                                              " +
"                          else                                                       " +
"                           0                                                         " +
"                        end B,                                                       " +
"                        case                                                         " +
"                          when t2.tool_version = 'C' then                            " +
"                           t2.count_num                                              " +
"                          else                                                       " +
"                           0                                                         " +
"                        end C,                                                       " +
"                        case                                                         " +
"                          when t2.tool_version = 'D' then                            " +
"                           t2.count_num                                              " +
"                          else                                                       " +
"                           0                                                         " +
"                        end D,                                                       " +
"                        case                                                         " +
"                          when t2.tool_version not in ('A', 'B', 'C', 'D') then      " +
"                           t2.count_num                                              " +
"                          else                                                       " +
"                           0                                                         " +
"                        end other                                                    " +
"                   from (select t.fab,                                               " +
"                                t.process,                                           " +
"                                t.layer,                                             " +
"                                t.tool_version,                                      " +
"                                count(t.fab) count_num                               " +
"                           from tlms_main t                                          " +
"                          where 1 = 1                                                " +
"                            and t.tapeout_date >= '" + txtEstimateStartDate.SelectedDate.ToString() + "'          " +
"                            and t.tapeout_date < '" + txtEstimateEndDate.SelectedDate.ToString() + "'           ";
        if (DropDownList1.SelectedValue == "請選擇")
        {

        }
        else
        {
            sql+="                            and t.fab = '" + DropDownList1.SelectedValue + "'                                        ";
        }

        if (DropDownList2.SelectedValue == "請選擇")
        {

        }
        else
        {
            sql += "                            and t.process = '" + DropDownList2.SelectedValue + "'                                         ";
        }

        if (DropDownList3.SelectedValue == "請選擇")
        {

        }
        else
        {
            sql += "                            and t.layer = '" + DropDownList3.SelectedValue + "'                                        ";
        }



        sql += "                          group by t.fab, t.process, t.layer, t.tool_version) t2) t4 " +
"          group by t4.fab, t4.process, t4.layer) t5                                  " +
" union all                                                                           " +
" select '',                                                                          " +
"        'Total',                                                                     " +
"        '',                                                                          " +
"        sum(t6.A),                                                                   " +
"        sum(t6.B),                                                                   " +
"        sum(t6.C),                                                                   " +
"        sum(t6.D),                                                                   " +
"        sum(t6.other),                                                               " +
"        sum(t6.total)                                                                " +
"   from (select t5.*, (t5.A + t5.B + t5.C + t5.D + t5.other) total                   " +
"           from (select t4.fab,                                                      " +
"                        t4.process,                                                  " +
"                        t4.layer,                                                    " +
"                        sum(t4.A) A,                                                 " +
"                        sum(t4.B) B,                                                 " +
"                        sum(t4.C) C,                                                 " +
"                        sum(t4.D) D,                                                 " +
"                        sum(t4.other) other                                          " +
"                   from (select t2.fab,                                              " +
"                                t2.process,                                          " +
"                                t2.layer,                                            " +
"                                case                                                 " +
"                                  when t2.tool_version = 'A' then                    " +
"                                   t2.count_num                                      " +
"                                  else                                               " +
"                                   0                                                 " +
"                                end A,                                               " +
"                                case                                                 " +
"                                  when t2.tool_version = 'B' then                    " +
"                                   t2.count_num                                      " +
"                                  else                                               " +
"                                   0                                                 " +
"                                end B,                                               " +
"                                case                                                 " +
"                                  when t2.tool_version = 'C' then                    " +
"                                   t2.count_num                                      " +
"                                  else                                               " +
"                                   0                                                 " +
"                                end C,                                               " +
"                                case                                                 " +
"                                  when t2.tool_version = 'D' then                    " +
"                                   t2.count_num                                      " +
"                                  else                                               " +
"                                   0                                                 " +
"                                end D,                                               " +
"                                case                                                 " +
"                                  when t2.tool_version not in                        " +
"                                       ('A', 'B', 'C', 'D') then                     " +
"                                   t2.count_num                                      " +
"                                  else                                               " +
"                                   0                                                 " +
"                                end other                                            " +
"                           from (select t.fab,                                       " +
"                                        t.process,                                   " +
"                                        t.layer,                                     " +
"                                        t.tool_version,                              " +
"                                        count(t.fab) count_num                       " +
"                                   from tlms_main t                                  " +
"                                  where 1 = 1                                        " +
"                                    and t.tapeout_date >=                            " +
"                                        '" + txtEstimateStartDate.SelectedDate.ToString() + "'                    " +
"                                    and t.tapeout_date <                             " +
"                                        '" + txtEstimateEndDate.SelectedDate.ToString() + "'                    ";



  if (DropDownList1.SelectedValue == "請選擇")
        {

        }
        else
        {
            sql+="                            and t.fab = '" + DropDownList1.SelectedValue + "'                                         ";
        }

        if (DropDownList2.SelectedValue == "請選擇")
        {

        }
        else
        {
            sql += "                            and t.process = '" + DropDownList2.SelectedValue + "'                                         ";
        }

        if (DropDownList3.SelectedValue == "請選擇")
        {

        }
        else
        {
            sql += "                            and t.layer = '" + DropDownList3.SelectedValue + "'                                         ";
        }



        sql += "                                  group by t.fab,                                    " +
"                                           t.process,                                " +
"                                           t.layer,                                  " +
"                                           t.tool_version) t2) t4                    " +
"                  group by t4.fab, t4.process, t4.layer) t5) t6                      ";
        
        DataSet ds_query = new DataSet();
        ds_query = func.get_dataSet_access(sql, connY);

        GridView1.DataSource = ds_query.Tables[0];

        GridView1.DataBind();

        DataTable DT_QUERY = new DataTable();

        DT_QUERY = ds_query.Tables[0];

        return DT_QUERY;

    }
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        Bind_data();
    }
    protected void btnExport_Click1(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }

        }

    }


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList1_OnTextChanged();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList2_OnTextChanged();
    }
}
