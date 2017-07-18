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

public partial class TP_TP_balance_detail : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CFT"];

    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE"];
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string shop = "";
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["shop"] = Request.QueryString["shop"];
            Session["date1"] = Request.QueryString["date1"];
            Session["prodid"] = Request.QueryString["prodid"];

            bind_data1();


        }
    }

    public DataSet bind_data1()
    {

        shop = Session["shop"].ToString();

    





          
           
              if (shop.Equals("T1CF"))
              {

                  string sql_temp1 = " select tt.shop,                                                                  " +
       "        tt.lot_id,                                                                " +
       "        ot1.glass_id,                                                             " +
       "        tt.input_product,                                                         " +
       "        tt.prod_name,                                                             " +
       "        tt.lot_type,                                                              " +
       "        tt.step_seq,                                                              " +
       "        tt.step_desc,                                                             " +
       "        tt.glass_qty,                                                             " +
       "        ot1.cst_id,                                                               " +
       "        ot1.vendor_lot_id                                                         " +
       "   from lot tt, substrate ot1                                                     " +
       "  where tt.lot_id in (                                                            " +
       "                                                                                  " +
       "                      select t.lot_id                                             " +
       "                        from cut_time_wip t                                       " +
       "                       where t.cut_date like '" + Session["date1"].ToString().Substring(0, 6) + "__'                           " +
       "                         and t.cut_time = '070000'                                " +
       "                         and t.prod_name = '" + Session["prodid"] + "'                            " +
       "                         and t.lot_id not in (                                    " +
       "                                                                                  " +
       "                                              select t.lot_id                     " +
       "                                                from cut_time_wip t               " +
       "                                               where t.cut_date = '" + Session["date1"].ToString() + "'      " +
       "                                                 and t.cut_time = '070000'        " +
       "                                                 and t.prod_name = '" + Session["prodid"].ToString() + "'))  " +
       "    and tt.prod_name not in ('" + Session["prodid"] + "')                                         " +
       "                                                                                  " +
       "    and tt.lot_id = ot1.lot_id                                                    ";
                  sql_temp1 = "select rownum,tt.* from (" + sql_temp1 + ") tt";

                  ds_temp1 = func.get_dataSet_access(sql_temp1, conn);
              }
               if (shop.Equals("T0Array"))
              {

                  string sql_temp1 = " select tt.shop,                                                                  " +
       "        tt.lot_id,                                                                " +
       "        ot1.glass_id,                                                             " +
       "        tt.input_product,                                                         " +
       "        tt.prod_name,                                                             " +
       "        tt.lot_type,                                                              " +
       "        tt.step_seq,                                                              " +
       "        tt.step_desc,                                                             " +
       "        tt.glass_qty,                                                             " +
       "        ot1.cst_id,                                                               " +
       "        ot1.vendor_lot_id                                                         " +
       "   from lot tt, substrate ot1                                                     " +
       "  where tt.lot_id in (                                                            " +
       "                                                                                  " +
       "                      select t.lot_id                                             " +
       "                        from cut_time_wip t                                       " +
       "                       where t.cut_date like '" + Session["date1"].ToString().Substring(0, 6) + "__'                           " +
       "                         and t.cut_time = '070000'                                " +
       "                         and t.prod_name = '" + Session["prodid"] + "'                            " +
       "                         and t.lot_id not in (                                    " +
       "                                                                                  " +
       "                                              select t.lot_id                     " +
       "                                                from cut_time_wip t               " +
       "                                               where t.cut_date = '" + Session["date1"].ToString() + "'      " +
       "                                                 and t.cut_time = '070000'        " +
       "                                                 and t.prod_name = '" + Session["prodid"].ToString() + "'))  " +
       "    and tt.prod_name not in ('" + Session["prodid"] + "')                                         " +
       "                                                                                  " +
       "    and tt.lot_id = ot1.lot_id                                                    ";
                  sql_temp1 = "select rownum,tt.* from (" + sql_temp1 + ") tt";

                   ds_temp1 = func.get_dataSet_access(sql_temp1, conn);
              }

         if (shop.Equals("T1Array"))
              {

                  string sql_temp1 = " select tt.shop,                                                                  " +
       "        tt.lot_id,                                                                " +
       "        ot1.glass_id,                                                             " +
       "        tt.input_product,                                                         " +
       "        tt.prod_name,                                                             " +
       "        tt.lot_type,                                                              " +
       "        tt.step_seq,                                                              " +
       "        tt.step_desc,                                                             " +
       "        tt.glass_qty,                                                             " +
       "        ot1.cst_id,                                                               " +
       "        ot1.vendor_lot_id                                                         " +
       "   from lot tt, substrate ot1                                                     " +
       "  where tt.lot_id in (                                                            " +
       "                                                                                  " +
       "                      select t.lot_id                                             " +
       "                        from cut_time_wip t                                       " +
       "                       where t.cut_date like '" + Session["date1"].ToString().Substring(0, 6) + "__'                           " +
       "                         and t.cut_time = '070000'                                " +
       "                         and t.prod_name = '" + Session["prodid"] + "'                            " +
       "                         and t.lot_id not in (                                    " +
       "                                                                                  " +
       "                                              select t.lot_id                     " +
       "                                                from cut_time_wip t               " +
       "                                               where t.cut_date = '" + Session["date1"].ToString() + "'      " +
       "                                                 and t.cut_time = '070000'        " +
       "                                                 and t.prod_name = '" + Session["prodid"].ToString() + "'))  " +
       "    and tt.prod_name not in ('" + Session["prodid"] + "')                                         " +
       "                                                                                  " +
       "    and tt.lot_id = ot1.lot_id                                                    ";
                  sql_temp1 = "select rownum,tt.* from (" + sql_temp1 + ") tt";
     
             ds_temp1 = func.get_dataSet_access(sql_temp1, conn);

              }

            GridView1.DataSource = ds_temp1;
            GridView1.DataBind();

            // doChart1(string sql);

         
      
        return ds_temp1;
      


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

            //#region link
            //string link = DataBinder.Eval(e.Row.DataItem, "glass_qty").ToString();
            //if (link != null && link.Length > 0)
            //{
            //    // "FindControl" borrowed directly from DOK 
            //    HyperLink hlParent = (HyperLink)e.Row.FindControl("hlParent");
            //    if (hlParent != null)
            //    {
            //        // Do some manipulation of the link value  
            //        string newLink = "https://" + link;

            //        // Set the Url of the HyperLink   
            //        hlParent.NavigateUrl = newLink;
            //    }
            //} 


            //#endregion



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
}
