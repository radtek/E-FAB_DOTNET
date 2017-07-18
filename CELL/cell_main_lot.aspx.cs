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
using System.Data.OracleClient;

public partial class CELL_cell_main_lot : System.Web.UI.Page
{

    string conn = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_ARY_OLE"];
    string conn1 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ODS_CEL_OLE_STD"];
    string conn2 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_POEE1"];
    string conn3 = System.Configuration.ConfigurationSettings.AppSettings["DBCONN_ALCS"];
    string today = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd");

    string today_minus17 = DateTime.Now.AddDays(-17).ToString("yyyy/MM/dd");

    string today_detail = DateTime.Now.AddDays(+0).ToString("yyyy/MM/dd HH:mm");
    string sql_temp = "";
    string sql_temp1 = "";
    string sql_temp2 = "";
    string sql_temp3 = "";
    string sql_temp4 = "";
    string sql_temp5 = "";
    DataSet ds_temp = new DataSet();
    DataSet ds_temp1 = new DataSet(); 
    
    protected void Page_Load(object sender, EventArgs e)
    {


        CallingOracleStoredProc("oscar");


        func.write_log("T1Cell_Main_LOT ", Server.MapPath("..\\") + "\\LOG\\", "log");


        //javascript 語法填入 字串 
        string frmClose = @"<script language = javascript>window.top.opener=null;window.top.open('','_parent','');window.top.close(this);</script>";
        //呼叫 javascript 
        this.Page.RegisterStartupScript("", frmClose);


    }

    public void CallingOracleStoredProc(string lot_id)
    {

        using (OracleConnection objConn = new OracleConnection("Data Source=ODS_CEL; User ID=innrpt; Password=bamboocel"))
        {

            OracleCommand objCmd = new OracleCommand();

            objCmd.Connection = objConn;

            objCmd.CommandText = "ODS_TCL_OSCAR.main_lot"; // "package.procedure" 

            objCmd.CommandType = CommandType.StoredProcedure;

            //objCmd.Parameters.Add("vLot_id_s", OracleType.VarChar).Value = lot_id;

            // objCmd.Parameters.Add("pout_count", OracleType.Number).Direction = ParameterDirection.Output; 



            try
            {

                objConn.Open();

                objCmd.ExecuteNonQuery();

                //System.Console.WriteLine("Number of employees in department 20 is {0}", objCmd.Parameters["pout_count"].Value); 

            }

            catch (Exception ex)
            {

                System.Console.WriteLine("Exception: {0}", ex.ToString());

            }



            objConn.Close();

        }

    } 

}
