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

public partial class _2011_prize : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_PARS1_OLE_ONDUTY"];
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    string today_minus17 = DateTime.Now.AddDays(-17).ToString("yyyy/MM/dd");

    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm");
    string sql_temp = "";
    string sql_temp1 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
          
            sql_temp = " select * from prize  t        " +
" where t.emp_name in (         " +
"                               " +
" '呂福枝',                     " +
" '戴佳穎',                     " +
" '徐世豪',                     " +
" '邱威萁',                     " +
" '廖文誌',                     " +
" '陳信凱',                     " +
" '王義勝',                     " +
" '徐琬茹',                     " +
" '王俊雅',                     " +
" '許琬羚',                     " +
" '廖千慧',                     " +
" '陳歆宜',                     " +
" '邱紹棟',                     " +
" '曾秀文',                     " +
" '謝金錞',                     " +
" '顧嘉偉',                     " +
" '鄧進良',                     " +
" '江俊儀',                     " +
" '郭書辰',                     " +
" '陳榮光',                     " +
" '吳政憲',                     " +
" '陳俊成',                     " +
" '李連旺',                     " +
" '邱政豪',                     " +
" '洪榮祥',                     " +
" '李震宇',                     " +
" '何雅茜',                     " +
" '謝昌憲',                     " +
" '吳福來',                     " +
" '楊潤騰',                     " +
" '謝志沛',                     " +
" '鄧炳辰',                     " +
" '林世賢',                     " +
" '林建利',                     " +
" '林韋旬',                     " +
" '林國照',                     " +
" '廖建賀',                     " +
" '朱天榮',                     " +
" '陳宇東',                     " +
" '邱錦宏',                     " +
" '林豐裕',                     " +
" '黃紹瑋',                     " +
" '張遠對',                     " +
" '許孟儒',                     " +
" '程秀真',                     " +
" '高振瑋',                     " +
" '陳國麟',                     " +
" '陳耀男',                     " +
" '李錦政',                     " +
" '賴岳汶',                     " +
" '許勝凱',                     " +
" '王盈月',                     " +
" '潘廷勇',                     " +
" '李榮華',                     " +
" '唐世璋',                     " +
" '陳春璉',                     " +
" '孟桂珠',                     " +
" '楊斯猛',                     " +
" '陳鏞企',                     " +
" '陳維虎',                     " +
" '張鈞揚',                     " +
" '楊通達',                     " +
" '黃昱彰',                     " +
" '陳永興',                     " +
" '郭鈞偉',                     " +
" '劉學倫',                     " +
" '黃文欽',                     " +
" '吳宗憲',                     " +
" '林詠欽',                     " +
" '鄧建松',                     " +
" '吳泳潔',                     " +
" '薛富中',                     " +
" '黃國泓',                     " +
" '陶博文',                     " +
" '李仁志',                     " +
" '黃建成',                     " +
" '蕭一心',                     " +
" '吳錫佳',                     " +
" '魏武慶',                     " +
" '胡慶祥',                     " +
" '蔡丞',                       " +
" '周子瑋',                     " +
" '錢明增',                     " +
" '楊綠淵',                     " +
" '謝佳波',                     " +
" '吳培聚',                     " +
" '梁政宏',                     " +
" '許緩如',                     " +
" '李林宏',                     " +
" '鍾文順',                     " +
" '沈建名',                     " +
" '陳永順',                     " +
" '黃碩彥',                     " +
" '蕭志賢',                     " +
" '張宏民',                     " +
" '施淙堯',                     " +
" '林育偉',                     " +
" '楊侑橘',                     " +
" '邱冠綸',                     " +
" '陳盈呈',                     " +
" '陳瑋翔',                     " +
" '林志湳',                     " +
" '林文忠',                     " +
" '李信明',                     " +
" '曾武舉',                     " +
" '郭峻菁',                     " +
" '翁兆慶',                     " +
" '謝正一',                     " +
" '范整泓',                     " +
" '江志偉',                     " +
" '莊祐典',                     " +
" '陳和謙',                     " +
" '葉靜如',                     " +
" '王棠業',                     " +
" '沈祥  ',                     " +
" '蔡宗穎',                     " +
" '莊智翔',                     " +
" '廖珈琳',                     " +
" '張元碩',                     " +
" '鄭光智',                     " +
" '吳采蓁',                     " +
" '林志華',                     " +
" '張義欣',                     " +
" '林校平',                     " +
" '林錫斌',                     " +
" '蘇智宏',                     " +
" '王詩閔',                     " +
" '陳煥斌',                     " +
" '張珍煒',                     " +
" '黃政勛',                     " +
" '許文龍',                     " +
" '陳建霖',                     " +
" '張維娜',                     " +
" '商淵河',                     " +
" '王中強',                     " +
" '吳豐全',                     " +
" '陳世民',                     " +
" '康家瑋',                     " +
" '彭建銘',                     " +
" '王健',                       " +
" '王希宸',                     " +
" '楊建宏',                     " +
" '陳景耀',                     " +
" '江冠賢',                     " +
" '蕭志成',                     " +
" '吳振銓',                     " +
" '游政昇',                     " +
" '楊政義',                     " +
" '謝元昊',                     " +
" '許振哲',                     " +
" '鄭家銘',                     " +
" '黃信銓',                     " +
" '吳洪鑑',                     " +
" '戴鈺卜',                     " +
" '劉志民',                     " +
" '吳明興',                     " +
" '霍錦霖',                     " +
" '宋立文',                     " +
" '曾文超',                     " +
" '邱兆民',                     " +
" '黃俊凱',                     " +
" '梁世忠',                     " +
" '黃智勇',                     " +
" '謝祥昶',                     " +
" '黃世羽',                     " +
" '許智信',                     " +
" '楊大慶',                     " +
" '陳憲煇',                     " +
" '湯為智',                     " +
" '吳宇隆',                     " +
" '張振興',                     " +
" '林益誠',                     " +
" '李建發',                     " +
" '陳士堯',                     " +
" '陳弘淳',                     " +
" '徐瑋璟',                     " +
" '吳仲傑',                     " +
" '黃琛徽',                     " +
" '楊迪凱',                     " +
" '洪嘉成',                     " +
" '陳峰志',                     " +
" '古長麟',                     " +
" '劉晏廷',                     " +
" '莊尚霖',                     " +
" '李世吉',                     " +
" '林永鋒',                     " +
" '李宗豪',                     " +
" '劉純忠',                     " +
" '楊邱閔',                     " +
" '陳俊堂',                     " +
" '黃弘毅',                     " +
" '李進德',                     " +
" '葉木水',                     " +
" '林炳宏',                     " +
" '羅宇舜',                     " +
" '黃煜仁',                     " +
" '李翊聖',                     " +
" '趙碩彥',                     " +
" '徐秉岳',                     " +
" '鄧建松'                      " +
"                               " +
"                               " +
" )   order by prize_name                           ";
            Bind_data(sql_temp, conn);

        }






    }


    public DataSet Bind_data(string sqlX, string connx)
    {
        sql_temp = sqlX;




        ds_temp = func.get_dataSet_access(sql_temp, connx);



        GridView1.DataSource = ds_temp.Tables[0];


        GridView1.DataBind();



        return ds_temp;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {




        sql_temp = " select * from prize t     " +
" where t.emp_name='" + txt_name.Text + "'  order by prize_name";

        if (txt_name.Text.Equals(""))
        {
            sql_temp = " select * from prize t   order by  prize_name   ";
        
        }
        GridView gv = new GridView();
        gv.DataSource = Bind_data(sql_temp, conn);
        gv.DataBind();
        ExportExcel(gv);




    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control); 
    }

    private void ExportExcel(GridView SeriesValuesDataGrid)
    {
        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader("content-disposition", "attachment;filename=2011_prize.xls");

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

    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        sql_temp = " select * from prize t     " +
" where t.emp_name='" + txt_name .Text+ "' order by prize_name ";

        if (txt_name.Text.Equals(""))
        {
            sql_temp = " select * from prize t order by prize_name  ";
        
        }

        Bind_data(sql_temp, conn);
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string strTaskID = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

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
            // Double priceX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "price"));
            // Int32 priceX_top = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "avg_hot_price")); 
            // Int32 priceX_cur = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current_price")); 
            //string report_id = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            //string[] StrAry = report_id.Split('_');


            //string report_id1 = DataBinder.Eval(e.Row.DataItem, "report_id").ToString();
            //string endtime1 = DataBinder.Eval(e.Row.DataItem, "endtime").ToString();
            //string[] StrAry1 = report_id1.Split('_');


            //Int32 pricexx = Convert.ToInt32(price1); 



            //if (StrAry[1] == "DAILY" && Convert.ToInt32(endtime.ToString().Substring(9, 2)) >= 8)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[1].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[3].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //    e.Row.Cells[6].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}


            //if (StrAry1[1] == "NOON" && Convert.ToInt32(endtime1.ToString().Substring(9, 4)) >= 1530)
            ////e.Row.Cells[0].BackColor = Color.Yellow; 
            //{
            //    e.Row.Cells[0].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[1].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[3].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[4].Style.Add("background-color", "#95CAFF");
            //    e.Row.Cells[5].Style.Add("background-color", "#95CAFF");

            //    e.Row.Cells[6].Style.Add("background-color", "#95CAFF");
            //    //e.Row.Cells[4].Style.Add("background-color", "#FFFF80");
            //    //e.Row.Cells[5].Style.Add("background-color", "#FFFF80");

            //}
            //if (countX >= 3)
            //    e.Row.Cells[2].Style.Add("background-color", "#95CAFF");
            //if (countX == 2)
            //    e.Row.Cells[2].Style.Add("background-color", "#FFFFB3");

            //if (Convert.ToDouble(pp) > priceX)
            //    e.Row.Cells[4].Style.Add("background-color", "#FF9DFF");


            //if (Flag_satus == "Cancel") 
            // e.Row.Cells[6].Style.Add("background-color", "#FF9DFF"); 
            if (e.Row.RowIndex != -1)
            {
                int RN = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = RN.ToString();
            }

        }
    } 

}
