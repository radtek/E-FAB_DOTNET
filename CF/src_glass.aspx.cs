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

public partial class CF_src_glass : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CFT"];
    DataSet ds_temp = new DataSet();
    GridView gv_test = new GridView();
    DataTable DT_NEW = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected System.Data.DataTable bind_data()
    {
        ArrayList glass_id_set = new ArrayList();



        if (TextBox1.Text=="")
        {
        }
        else
        {
            glass_id_set.Add(TextBox1.Text.ToString());
        }


        if (TextBox2.Text=="")
        {
        }
        else
        {
            glass_id_set.Add(TextBox2.Text.ToString());
        }



        if (TextBox3.Text=="")
        {
        }
        else
        {
            glass_id_set.Add(TextBox3.Text.ToString());
        }




        if (TextBox4.Text=="")
        {
        }
        else
        {
            glass_id_set.Add(TextBox4.Text.ToString());
        }




        if (TextBox5.Text=="")
        {
        }
        else
        {
            glass_id_set.Add(TextBox5.Text.ToString());
        }
       

        string sql_str = "";

        string combind = " union all ";



        for (int i = 0; i < glass_id_set.Count; i++)
        {

            if (i > 0)
            {
                sql_str = sql_str + combind;
            }
                sql_str= sql_str+"select '" + glass_id_set[i] + "' as glass_id , t.glass_id as mid_glass_id,t.lot_id,t.src_glass_id,t.lotstart_dttm,t.lotstart_lotid from substrate t   " +
"where t.src_glass_id in (                                                                     " +
"select t.src_glass_id from substrate t                                                        " +
"where t.glass_id in ('" + glass_id_set[i] + "')                                                        " +
")                                                                                             " +
"and t.glass_id not in ('" + glass_id_set[i] + "')                                                      ";



          

        }


        ds_temp = func.get_dataSet_access(sql_str, conn);




        

        System.Data.DataTable dt_Adjust = new System.Data.DataTable();

        dt_Adjust = ds_temp.Tables[0];

        DT_NEW = ds_temp.Tables[0];

        GridView1.DataSource = dt_Adjust;
       
        GridView1.DataBind();

       
        
        return dt_Adjust;

       // ds_temp.Clear();

    }

    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        bind_data();
    }
    protected void btnExport_Click1(object sender, EventArgs e)
    {
       GridView gv = new GridView();

       

        gv.DataSource = bind_data();
       //gv.DataSource = DT_NEW;

        //gv_test
        gv.DataBind();
        ExportExcel(gv);

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.RowIndex != -1)
        //    {
        //        int id = e.Row.RowIndex + 1;
        //        e.Row.Cells[0].Text = id.ToString();
        //    }

        //}

    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
    }


    private void ExportExcel(GridView SeriesValuesDataGrid)
    {
        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader("content-disposition", "attachment;filename=rework_turn.xls");

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
