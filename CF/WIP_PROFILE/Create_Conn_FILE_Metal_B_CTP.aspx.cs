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

public partial class CF_WIP_PROFILE_Create_Conn_FILE_Metal_B_CTP : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_CFT"];
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");
    string today_hour = DateTime.Now.AddDays(+0).ToString("HH");
    string today_min = DateTime.Now.AddDays(+0).ToString("mm");

    string temp = "";
    string temp_header = @"<%                                                   <BR>                    
   'Set common parameter                                 <BR>
    stage_height = 25                                 <BR>      
    step_T0_height = 20                               <BR>
                                                      <BR>
    'Set Color for Step pooling                       <BR>
    ReDim  step_color_pooling(6)                      <BR>
    step_color_pooling(0)='#0099FF'                   <BR>
    step_color_pooling(1)='#57A7F9'                   <BR>
    step_color_pooling(2)='#81BDFA'                   <BR>
                                                      <BR>
    step_color_pooling(3)='#81BDFA'                   <BR>
    step_color_pooling(4)='#57A7F9'                   <BR>
    step_color_pooling(5)='#0099FF'                   <BR>
    step_color_pooling(6)='#FFFFFF'                   <BR>
                                                      <BR>
    'Set color                                        <BR>
    ReDim  item_color(51,1)                           <BR>
    item_color(0,0) ='PROCSTATUS_OnHold'              <BR>
    item_color(0,1) ='#FF66FF'                        <BR>
    item_color(1,0) ='PROCSTATUS_Processing'          <BR>
    item_color(1,1) ='#33FF66'                        <BR>
    item_color(2,0) ='PROCSTATUS_Waiting'             <BR>
    item_color(2,1) ='#FFFF00'                        <BR>
                                                      <BR>
    item_color(3,0) ='LOTTYPE_Dense'                  <BR>
    item_color(3,1) ='#0099CC'                        <BR>
    item_color(4,0) ='LOTTYPE_Engineer'               <BR>
    item_color(4,1) ='#FF66FF'                        <BR>
    item_color(5,0) ='LOTTYPE_MQC'                    <BR>
    item_color(5,1) ='#FFFF00'                        <BR>
    item_color(6,0) ='LOTTYPE_Product'                <BR>
    item_color(6,1) ='#33FF66'                        <BR>
    item_color(7,0) ='LOTTYPE_PPBox'                  <BR>
    item_color(7,1) ='#FF9900'                        <BR>
                                                      <BR>
    item_color(8,0) ='PRIORITY_Hot'                   <BR>
    item_color(8,1) ='#FF66FF'                        <BR>
    item_color(9,0) ='PRIORITY_Normal'                <BR>
    item_color(9,1) ='#33FF66'                        <BR>
                                                      <BR>
    item_color(10,0) ='LOCATION_InEQP'                <BR>
    item_color(10,1) ='#33FF66'                       <BR>
    item_color(11,0) ='LOCATION_InStock'              <BR>
    item_color(11,1) ='#FFFF00'                       <BR>
    item_color(12,0) ='LOCATION_Others'               <BR>
    item_color(12,1) ='#CCCCFF'                       <BR>
                                                      <BR>
                                                      <BR>
    item_color(13,0) ='PRODUCT_REWORK'                <BR>
    item_color(13,1) ='#FF00FF'                       <BR>
    item_color(14,0) ='PRODUCT_SOURCE'                <BR>
    item_color(14,1) ='#0099CC'                       <BR>
    item_color(15,0) ='PRODUCT_MQC'                   <BR>
    item_color(15,1) ='#FFFF00'                       <BR>
                                                      <BR>
    item_color(16,0) ='PRODUCT_091Q'                  <BR>
    item_color(16,1) ='#00FFFF'                       <BR>
    item_color(17,0) ='PRODUCT_430A1F'                <BR>
    item_color(17,1) ='#9966FF'                       <BR>
    item_color(18,0) ='PRODUCT_500C1F'                <BR>
    item_color(18,1) ='#CCCCFF'                       <BR>
    item_color(19,0) ='PRODUCT_700J1F'                <BR>
    item_color(19,1) ='#FF9966'                       <BR>
    item_color(20,0) ='PRODUCT_700D1F'                <BR>
    item_color(20,1) ='#CCCF00'                       <BR>
    item_color(21,0) ='PRODUCT_800D1F'                <BR>
    item_color(21,1) ='orange'                        <BR>
    item_color(22,0) ='PRODUCT_900A1F'                <BR>
    item_color(22,1) ='#22FF00'                       <BR>
    item_color(23,0) ='PRODUCT_970B1F'                <BR>
    item_color(23,1) ='tomato'                        <BR>
    item_color(24,0) ='PRODUCT_Rework'                <BR>
    item_color(24,1) ='aquamarine'                    <BR>
                                                      <BR>
    item_color(25,0) ='PRODUCT_970D1F'                <BR>
    item_color(25,1) ='blueviolet'                    <BR>
                                                      <BR>
    item_color(26,0) ='PRODUCT_AZBA1F'                <BR>
    item_color(26,1) ='lightseagreen'                 <BR>
                                                      <BR>
    item_color(27,0) ='PRODUCT_BCFA1F'                <BR>
    item_color(27,1) ='#802040'                       <BR>
                                                      <BR>
    item_color(28,0) ='PRODUCT_BCZB1F'                <BR>
    item_color(28,1) ='rosybrown'                     <BR>
                                                      <BR>
    item_color(29,0) ='PRODUCT_BCZC1F'                <BR>
    item_color(29,1) ='mediumslateblue'               <BR>
                                                      <BR>
    item_color(30,0) ='PRODUCT_AIZG1F'                <BR>
    item_color(30,1) ='pink'                          <BR>
                                                      <BR>
    item_color(31,0) ='PRODUCT_AIZH1F'                <BR>
    item_color(31,1) ='lightgrey'                     <BR>
                                                      <BR>
    item_color(32,0) ='PRODUCT_06QJ'                  <BR>
    item_color(32,1) ='mistyrose'                     <BR>
                                                      <BR>
    item_color(33,0) ='PRODUCT_700B1F'                <BR>
    item_color(33,1) ='palevioletred'                 <BR>
                                                      <BR>
    item_color(34,0) ='PRODUCT_700C1F'                <BR>
    item_color(34,1) ='yellowgreen'                   <BR>
                                                      <BR>
    item_color(35,0) ='PRODUCT_800C1F'                <BR>
    item_color(35,1) ='#6619338'                      <BR>
                                                      <BR>
    item_color(36,0) ='PRODUCT_AGZP1F'                <BR>
    item_color(36,1) ='#808040'                       <BR>
                                                      <BR>
    item_color(37,0) ='PRODUCT_AIZK1F'                <BR>
    item_color(37,1) ='#25BE38'                       <BR>
                                                      <BR>
    item_color(38,0) ='PRODUCT_AIZL1F'                <BR>
    item_color(38,1) ='#FFBBFF'                       <BR>
                                                      <BR>
    item_color(39,0) ='PRODUCT_AHEA1F'                <BR>
    item_color(39,1) ='#4af7ee'                       <BR>
                                                      <BR>
    item_color(40,0) ='PRODUCT_AEFA1F'                <BR>
    item_color(40,1) ='#3f4581'                       <BR>
                                                      <BR>
    item_color(41,0) ='PRODUCT_BCZA1F'                <BR>
    item_color(41,1) ='#aa5b60'                       <BR>
                                                      <BR>
    item_color(42,0) ='LOTTYPE_CFENGINEER_Abnormal'   <BR>
    item_color(42,1) ='#aa'                           <BR>
    item_color(43,0) ='LOTTYPE_CFENGINEER_Risk'       <BR>
    item_color(43,1) ='#4080ff'                       <BR>
    item_color(44,0) ='LOTTYPE_CFENGINEER_TD'         <BR>
    item_color(44,1) ='#f15262'                       <BR>
                                                      <BR>
    item_color(45,0) ='PRODUCT_BCZA1F'                <BR>
    item_color(45,1) ='#0000FF'                       <BR>
    item_color(46,0) ='PRODUCT_BCZA1F'                <BR>
    item_color(46,1) ='#A52A2A'                       <BR>
    item_color(47,0) ='PRODUCT_0ASG'                  <BR>
    item_color(47,1) ='#ff0080'                       <BR>
    item_color(48,0) ='PRODUCT_ADZA1F'                <BR>
    item_color(48,1) ='#FFCC00'                       <BR>
    item_color(49,0) ='PRODUCT_AGZQ1F'                <BR>
    item_color(49,1) ='#FF8000'                       <BR>
    item_color(50,0) ='PRODUCT_AZAA1F'                <BR>
    item_color(50,1) ='#408080'                       <BR>
    item_color(51,0) ='PRODUCT_500B1F'                <BR>
    item_color(51,1) ='#6699FF'                       <BR>
                                                      <BR>
                                                      <BR>
    ' Set step data                                   <BR>                 ";

    string middle = "";
    string sql2 = "";
    string sql3 = "";
    string sql4 = "";
    string sql5 = "";
    string temp3 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet();
    DataSet ds_temp2 = new DataSet();
    string sql = "";
    ArrayList arlist_temp1 = new ArrayList();
    ArrayList arlist_temp2 = new ArrayList();


    protected void Page_Load(object sender, EventArgs e)
    {
        //add_step("2510_CFIF", "2580_CFIF_BUF");
        add_step("T4A630_PAS_PS_OAL", "T4A630_PAS_PS_RCL");
        //delete_step("2580_CFIF_BUF");
        create_code();

    }

    public void delete_step(string delete_step)
    {



        sql2 = " select t.sn,t.stepname from cf_wipprofile_metal_b_ctp t " +
" where t.stepname='" + delete_step + "'                      ";


        ds_temp1 = func.get_dataSet_access(sql2, conn);






        Int32 delete_seq = Convert.ToInt32(ds_temp1.Tables[0].Rows[0]["sn"]);

        sql4 = "select  t.sn,t.stepname  from cf_wipprofile_metal_b_ctp t   " +
        " where t.sn>" + delete_seq + "                      ";

        ds_temp2 = func.get_dataSet_access(sql4, conn);


        for (int i = 0; i <= ds_temp2.Tables[0].Rows.Count - 1; i++)
        {
            Int32 down_sn = Convert.ToInt32(ds_temp2.Tables[0].Rows[i]["sn"]) - 1;

            sql5 = " update cf_wipprofile_metal_b_ctp t     " +
"   set t.sn =" + down_sn + "               " +
"      where t.sn=" + ds_temp2.Tables[0].Rows[i]["sn"] + "              ";

            func.get_sql_execute(sql5, conn);

        }

        sql3 = " delete cf_wipprofile_metal_b_ctp t " +
"  where t.stepname='" + delete_step + "'          ";

        func.get_sql_execute(sql3, conn);

    }

    public void add_step(string target_step, string add_step)
    {

        sql2 = " select t.sn,t.stepname from cf_wipprofile_metal_b_ctp t " +
  " where t.stepname='" + target_step + "'                      ";


        ds_temp1 = func.get_dataSet_access(sql2, conn);


        sql3 = " select t.sn,t.stepname from cf_wipprofile_metal_b_ctp t" +
"             " +
"       where sn>" + ds_temp1.Tables[0].Rows[0]["sn"] + "          ";

        ds_temp2 = func.get_dataSet_access(sql3, conn);

        for (int i = 0; i <= ds_temp2.Tables[0].Rows.Count - 1; i++)
        {

            Int32 up_sn = Convert.ToInt32(ds_temp2.Tables[0].Rows[i]["SN"]) + 1;

            sql5 = " update cf_wipprofile_metal_b_ctp t     " +
 "   set t.sn =" + up_sn + "               " +
 "      where t.stepname='" + ds_temp2.Tables[0].Rows[i]["stepname"] + "'              ";

            func.get_sql_execute(sql5, conn);

        }







        Int32 counter = Convert.ToInt32(ds_temp1.Tables[0].Rows[0]["sn"]) + 1;

        sql4 = " insert into cf_wipprofile_metal_b_ctp  " +
 "   (sn, stepname)                  " +
 " values                            " +
 "   (" + counter + ", '" + add_step + "')                   ";

        func.get_sql_execute(sql4, conn);
    }

    public void create_code()
    {

        sql = "select t.sn,t.stepname from cf_wipprofile_metal_b_ctp t order by t.sn";

        ds_temp = func.get_dataSet_access(sql, conn);

        string temp2 = "";

        for (int i = 0; i <= ds_temp.Tables[0].Rows.Count - 1; i++)
        {
            int j = i + 1;
            int k = 2 * i;
            int l = 2 * i + 1;
            if (i == 0)
            {
                temp = @"step_name_T0_TTLlot_TTLqty(" + i + ",0)=  '" + ds_temp.Tables[0].Rows[i]["stepname"] + "_Stocker' <BR>" +
                  " step_name_T0_TTLlot_TTLqty(" + i + ",1)=  '" + ds_temp.Tables[0].Rows[i]["stepname"] + "' <BR>" +
                   " step_name_T0_TTLlot_TTLqty(" + j + ",0)=  '" + ds_temp.Tables[0].Rows[i]["stepname"] + "_Inline' <BR>" +
                    " step_name_T0_TTLlot_TTLqty(" + j + ",1)=  '" + ds_temp.Tables[0].Rows[i]["stepname"] + "' <BR><BR>";

            }

            else
            {
                temp = @"step_name_T0_TTLlot_TTLqty(" + k + ",0)=  '" + ds_temp.Tables[0].Rows[i]["stepname"] + "_Stocker' <BR>" +
                 " step_name_T0_TTLlot_TTLqty(" + k + ",1)=  '" + ds_temp.Tables[0].Rows[i]["stepname"] + "' <BR>" +
                  " step_name_T0_TTLlot_TTLqty(" + l + ",0)=  '" + ds_temp.Tables[0].Rows[i]["stepname"] + "_Inline' <BR>" +
                   " step_name_T0_TTLlot_TTLqty(" + l + ",1)=  '" + ds_temp.Tables[0].Rows[i]["stepname"] + "' <BR><BR>";

            }


            temp2 = temp2 + temp;
            middle = " ReDim step_name_T0_TTLlot_TTLqty(" + l + ",9)  <BR>  " +
"     for i = 0 to " + l + " <BR>  " + "                    " +
"     	step_name_T0_TTLlot_TTLqty(i,2)=0    <BR>  " +
"     	step_name_T0_TTLlot_TTLqty(i,3)=0    <BR> " +
"     	step_name_T0_TTLlot_TTLqty(i,4)=0    <BR> " +
"     	step_name_T0_TTLlot_TTLqty(i,5)=0    <BR> " +
"     	step_name_T0_TTLlot_TTLqty(i,6)=1    <BR> " +
"     	step_name_T0_TTLlot_TTLqty(i,7)=1    <BR>" +
"     	step_name_T0_TTLlot_TTLqty(i,8)=0    <BR>" +
"     	step_name_T0_TTLlot_TTLqty(i,9)=0    <BR> " +
"     next                                   <BR><BR> ";

        }

        string abcd = middle + temp2;
        Response.Write(abcd.Replace("'", "&quot;"));

    }
}
