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
using System.IO;
using System.Data.OracleClient;
using System.Runtime.InteropServices;
using Ionic.Zip;
using System.Net;
using System.Net.Mail;
using Innolux.Portal.Common;


public partial class CF_AOI_FILE_NG_PARSER2 : System.Web.UI.Page
{
    #region EDA_FILE_NG_VARIABLE

    ArrayList arlist_temp1 = new ArrayList();

    public class EDA_FILE_NG
    {
        private string _SN;
        public string SN
        {
            set { _SN = value; }
            get { return _SN; }
        }

        private string _GLASSID_PREFIX;
        public string GLASSID_PREFIX
        {
            set { _GLASSID_PREFIX = value; }
            get { return _GLASSID_PREFIX; }
        }
        private string _GLASSID_PREFIX_PRE;
        public string GLASSID_PREFIX_PRE
        {
            set { _GLASSID_PREFIX_PRE = value; }
            get { return _GLASSID_PREFIX_PRE; }
        }
        private string _SUBEQID;
        public string SUBEQID
        {
            set { _SUBEQID = value; }
            get { return _SUBEQID; }
        }

        private string _SUBEQID_PRE;
        public string SUBEQID_PRE
        {
            set { _SUBEQID_PRE = value; }
            get { return _SUBEQID_PRE; }
        }

        private string _DATETIME;
        public string DATETIME
        {
            set { _DATETIME = value; }
            get { return _DATETIME; }
        }
        private string _RECIPE;
        public string RECIPE
        {
            set { _RECIPE = value; }
            get { return _RECIPE; }
        }


    }
    #endregion


    string dt_end = "";
    string dt_start = "";
    string yesturday_detail = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
    string today_detail1 = DateTime.Now.AddDays(+0).ToString("yyyyMMddHHmmss");
    EDA_FILE_NG _EDA_FILE_NG = new EDA_FILE_NG();
    string tmp_count = "0";
    DataTable dt_filter = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            txtEstimateSTARTTIME.SelectedDate = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd"));
            initial(null, null);
        
        }

     

    }

    protected void initial(object sender, EventArgs e)

    { 
      
       #region netdrive
        NetworkDrive oNetDrive = new NetworkDrive();
        oNetDrive.LocalDrive = "M:";
        oNetDrive.Persistent = true;
        oNetDrive.SaveCredentials = true;
        oNetDrive.ShareName = @"\\172.16.12.62\cf";
        dt_start = Convert.ToDateTime(txtEstimateSTARTTIME.SelectedDate.Value.AddDays(+0).ToString("yyyy/MM/dd")).ToString("yyyyMMdd");
        dt_end = Convert.ToDateTime(txtEstimateSTARTTIME.SelectedDate.Value.AddDays(+1).ToString("yyyy/MM/dd")).ToString("yyyyMMdd");


        try
        {
            oNetDrive.MapDrive(@"T1FAB\t1eda", "CIMabc123");

            Parser_tmp_directory_file(oNetDrive.LocalDrive + "\\T1\\" + DropDownList1.SelectedValue.ToString()+ "\\EDANG\\", "*.TXT", -360);
            Delete_tmp_directory(HttpContext.Current.Server.MapPath(".") + "\\NG_FILE\\", -3);
            oNetDrive.UnMapDrive(oNetDrive.LocalDrive, true);
        }
        catch (Exception)
        {

            oNetDrive.UnMapDrive(oNetDrive.LocalDrive, true);
        }
        finally
        {
            //oNetDrive.UnMapDrive();
        }
      

        //oNetDrive.MapDrive(@"T1FAB\t1eda", "CIMabc123");
        #endregion
        
        //oNetDrive.UnMapDrive();
        //Delete_tmp_directory_file(HttpContext.Current.Server.MapPath(".") + "\\NG_FILE\\", "*.TXT", -3);

    
    }

    public void Parser_tmp_directory_file(string file_path, string file_type, Double dayAgo)
    {



        //DirectoryInfo dir = new DirectoryInfo(Server.MapPath(".") + "\\CF\\Save_file\\"); 
        DirectoryInfo dir = new DirectoryInfo(file_path);
        // FileInfo[] files = dir.GetFiles("*.xls"); 
        //FileInfo[] files = dir.GetFiles(file_type); 

        //Destination = @"http:\\10.56.131.22\LCM_EDAFILE\DfSender_LCM4\COMPRESSED_FILE";
        // Destination = @"Z:\";
        //Destination = @"\\10.56.195.215\Shipping\DfSender_LCM4\COMPRESSED_FILE";
        string[] tmp_name;
        DirectoryInfo[] subdir = dir.GetDirectories();

        DataTable dt_eda_ng = new DataTable();


        dt_eda_ng.Columns.Add("PRODUCT_CODE", typeof(String));
        dt_eda_ng.Columns.Add("SUBEQID", typeof(String));
        dt_eda_ng.Columns.Add("RECIPE", typeof(String));
        dt_eda_ng.Columns.Add("DATETIME", typeof(String));




        for (int i = 0; i <= subdir.Length - 1; i++)
        {
            
            if (Convert.ToInt32(subdir[i].Name.ToString()) >= Convert.ToInt32(dt_start) && Convert.ToInt32(subdir[i].Name.ToString()) <= Convert.ToInt32(dt_end))
            {
                FileInfo[] files = subdir[i].GetFiles(file_type);

                for (int j = 0; j <= files.Length - 1; j++)
                {
                    
                     if (files[j].CreationTime > DateTime.Now.AddDays(dayAgo))
                    {
                        //files[j].CopyTo(Destination + "\\" + files[j].Name.ToString(), true);


                        tmp_name = files[j].Name.ToString().Replace(".TXT", "").Split('_');

                        _EDA_FILE_NG.GLASSID_PREFIX = tmp_name[0].Substring(0, 4).ToString();
                        _EDA_FILE_NG.SUBEQID = tmp_name[1].ToString();
                        _EDA_FILE_NG.DATETIME = tmp_name[2].ToString().Substring(0, 8);

                        //   T1\AOI\EDANG

                        arlist_temp1 = func.FileToArray(file_path + "\\" + subdir[i].ToString() + "\\" + files[j].Name);


                        for (int k = 0; k < arlist_temp1.Count - 1; k++)
                        {

                            //if (arlist_temp1[k].ToString().IndexOf("RECIPE:") > 0)
                            if (k == 4)
                            {
                                string[] aaaaa = arlist_temp1[k].ToString().Split(':');

                                _EDA_FILE_NG.RECIPE = aaaaa[1];
                            }

                        }



                        if (!(_EDA_FILE_NG.GLASSID_PREFIX.Equals(_EDA_FILE_NG.GLASSID_PREFIX_PRE) && _EDA_FILE_NG.SUBEQID.Equals(_EDA_FILE_NG.SUBEQID_PRE)))
                        {

                            DataRow dr;


                            dr = dt_eda_ng.NewRow();

                            dr[0] = _EDA_FILE_NG.GLASSID_PREFIX;
                            dr[1] = _EDA_FILE_NG.SUBEQID;
                            dr[2] = _EDA_FILE_NG.RECIPE;
                            dr[3] = _EDA_FILE_NG.DATETIME;

                            dt_eda_ng.Rows.Add(dr);

                            // Response.Write(_EDA_FILE_NG.GLASSID_PREFIX + " " + _EDA_FILE_NG.SUBEQID + " " + _EDA_FILE_NG.RECIPE +" "+ _EDA_FILE_NG.DATETIME + "<BR>");
                        }
                        _EDA_FILE_NG.GLASSID_PREFIX_PRE = tmp_name[0].Substring(0, 4).ToString();
                        _EDA_FILE_NG.SUBEQID_PRE = tmp_name[1].ToString();

                        //Upload(files[j].FullName, ftpServerIP + @"/" + subdir[i].Name.ToString() + @"/COMPRESSED_FILE/", Account, ftppassword);

                        // File.Copy(files[j].,Destination);
                    }




                    // files[j].Delete();

                }
            }


        }
        DataView dv = dt_eda_ng.DefaultView;

        dt_filter = dv.ToTable(true, "PRODUCT_CODE", "SUBEQID", "RECIPE", "DATETIME");
        GridView1.DataSource = dt_filter;

        GridView1.DataBind();
        tmp_count = dt_filter.Rows.Count.ToString();

        Label1.Text = tmp_count;

    }

    public void Delete_tmp_directory_file(string file_path, string file_type, Double dayAgo)
    {
        //DirectoryInfo dir = new DirectoryInfo(Server.MapPath(".") + "\\CF\\Save_file\\"); 
        DirectoryInfo dir = new DirectoryInfo(file_path);
        // FileInfo[] files = dir.GetFiles("*.xls"); 
        //FileInfo[] files = dir.GetFiles(file_type); 

        //Destination = @"http:\\10.56.131.22\LCM_EDAFILE\DfSender_LCM4\COMPRESSED_FILE";
        // Destination = @"Z:\";
        //Destination = @"\\10.56.195.215\Shipping\DfSender_LCM4\COMPRESSED_FILE";

        DirectoryInfo[] subdir = dir.GetDirectories();

        for (int i = 0; i <= subdir.Length - 1; i++)
        {

            FileInfo[] files = subdir[i].GetFiles(file_type);

            for (int j = 0; j <= files.Length - 1; j++)
            {
                if (files[j].CreationTime < DateTime.Now.AddDays(dayAgo))
                {
                    //files[j].CopyTo(Destination + "\\" + files[j].Name.ToString(), true);



                    files[j].Delete();

                    //Upload(files[j].FullName, ftpServerIP + @"/" + subdir[i].Name.ToString() + @"/COMPRESSED_FILE/", Account, ftppassword);

                    // File.Copy(files[j].,Destination);
                }


                // files[j].Delete();

            }

        }

        //Int32 counter = 0;
        // Display the name of all the files. 
        #region MyRegion
        //foreach (FileInfo file in files) 
        //{ 
        // counter = counter + 1; 
        // Response.Write(counter + "."); 

        // Response.Write("Name: " + file.Name + " "); 
        // Response.Write("<br/>"); 
        // Response.Write("Size: " + file.Length.ToString()); 
        // Response.Write("<br/>"); 
        //} 
        #endregion



    }

    protected void Delete_tmp_directory(string directory_path, double days)
    {
        // string strFolderPath = Server.MapPath(".") + "\\FILE\\T1ARRAY\\";

        string strFolderPath = directory_path;


        DirectoryInfo DIFO = new DirectoryInfo(strFolderPath);

        DirectoryInfo[] DIFO_SUB = DIFO.GetDirectories();

        for (int i = 0; i <= DIFO_SUB.Length - 1; i++)
        {
            if (DIFO_SUB[i].CreationTime < DateTime.Now.AddDays(days))
            {
                DIFO_SUB[i].Delete(true);
            }
        }




    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        GridView gv = new GridView();

        initial(null, null);

        //Label1.Text = ds_temp.Tables[0].Rows.Count.ToString();
        gv.DataSource = dt_filter;
        gv.DataBind();
        ExportExcel(gv);
    }


    private void ExportExcel(GridView SeriesValuesDataGrid)
    {

        string filename = "";
        string today_detail_char = DateTime.Now.AddDays(+0).ToString("yyyy/MM/ddHHmmss").Replace("/", "");
        filename = "T1CF_AOI_NG_History_" + today_detail_char + ".xls";
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

    protected void Button2_Click(object sender, EventArgs e)
    {
       initial(null,null);
    }
}
